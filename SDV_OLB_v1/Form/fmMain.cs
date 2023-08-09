using HalconDotNet;
using SDV_OLB_v1.ClsProcess;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SDV_OLB_v1
{
    public partial class fmMain : Form
    {
        string _pathVisionDB = "D:/RTC_Project/SDV OLB/SDV_OLB_v1/SDV_OLB_v1/bin/Debug/VisionDB.db";
        bool _camConect1 = false;
        bool _camConect2 = false;
        bool _isLiveCam1 = false;
        bool _isLiveCam2 = false;

        //----------------------------------------------------------------------------------//

        List<CamSetting> _lstCamSettings = new List<CamSetting>();
        CamSetting _camSetting1 = new CamSetting();
        CamSetting _camSetting2 = new CamSetting();
        //########################## Frame and Himage

        public HTuple _frameGrabber1 = null;
        public HTuple _frameGrabber2 = null;
        public static HTuple _frameGrabber1trans;
        public static HTuple _frameGrabber2trans;
        public HWindow _Window11;
        public HWindow _Window12;


        //----------------------------------------------------------------------------------//
        //########  set color
        Color colorCamConnected = Color.FromArgb(111, 174, 70);
        Color colorCamDisConnected = Color.Red;

        public fmMain()
        {
            InitializeComponent();
        }

        void loadCamSetting()
        {
            _lstCamSettings.Clear();
            DataTable dt = Lib.GetTableData("select * from CameraSetting",_pathVisionDB);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                CamSetting c = new CamSetting();
                c.CamIndex = Lib.ToInt(dt.Rows[i]["CamIndex"]);
                c.ExposureTime = Lib.ToInt(dt.Rows[i]["ExposureTime"]);
                c.Gain = Lib.ToInt(dt.Rows[i]["Gain"]);
                c.Interface = dt.Rows[i]["Interface"].ToString();
                c.Device = dt.Rows[i]["Device"].ToString();
                c.CalibScale = Lib.ToDouble(dt.Rows[i]["CalibScale"]);
                _lstCamSettings.Add(c);
            }
            try
            {
                _camSetting1 = _lstCamSettings.FirstOrDefault(o => o.CamIndex == 1);
                _camSetting2 = _lstCamSettings.FirstOrDefault(o => o.CamIndex == 2);
                if (_frameGrabber1 != null)
                {
                    HOperatorSet.SetFramegrabberParam(_frameGrabber1, "ExposureTimeRaw", _camSetting1.ExposureTime);// 27000);//
                    HOperatorSet.SetFramegrabberParam(_frameGrabber1, "GainRaw", _camSetting1.Gain);// 0);//
                }

                if (_frameGrabber2 != null)
                {
                    HOperatorSet.SetFramegrabberParam(_frameGrabber2, "ExposureTimeRaw", _camSetting2.ExposureTime);// 27000);//
                    HOperatorSet.SetFramegrabberParam(_frameGrabber2, "GainRaw", _camSetting2.Gain);// 0);//
                }
            }
            catch (Exception ex)
            {

            }
        }


        void WindowControlLoad()
        {
            _Window11 = WindowControl11.HalconWindow;
            _Window12 = WindowControl12.HalconWindow;


            WindowControl11.MouseWheel += my_MouseWheel11;
            WindowControl12.MouseWheel += my_MouseWheel12;

        }
        private void my_MouseWheel11(object sender, MouseEventArgs e)
        {
            Point pt = WindowControl11.Location;
            MouseEventArgs newe = new MouseEventArgs(e.Button, e.Clicks, e.X - pt.X, e.Y - pt.Y, e.Delta);
            WindowControl11.HSmartWindowControl_MouseWheel(sender, newe);
        }
        private void my_MouseWheel12(object sender, MouseEventArgs e)
        {
            Point pt = WindowControl12.Location;
            MouseEventArgs newe = new MouseEventArgs(e.Button, e.Clicks, e.X - pt.X, e.Y - pt.Y, e.Delta);
            WindowControl12.HSmartWindowControl_MouseWheel(sender, newe);
        }

        private void connectAllCameraAsync()
        {
            Task task1 = Task.Factory.StartNew(() =>
            {
                try
                {
                    string _interfaceName = _camSetting1.Interface;
                    string _deviceName = _camSetting1.Device;
                    HOperatorSet.OpenFramegrabber(_interfaceName, 0, 0, 0, 0, 0, 0
                            , "progressive"
                            , -1
                            , "default"
                            , "num_buffers=2"
                            , "default", _interfaceName == "File" ? _deviceName : "default"
                            , _interfaceName == "File" ? "default" : _deviceName
                            , 0
                            , -1
                            , out _frameGrabber1);
                    _frameGrabber1trans = _frameGrabber1;

                    //HOperatorSet.GetFramegrabberParam(_frameGrabber1, "num_buffers", out HTuple Value);
                    //HOperatorSet.SetFramegrabberParam(_frameGrabber1, "ExposureTime", 27000);
                    //HOperatorSet.SetFramegrabberParam(_frameGrabber1, "Gain", 18);
                    //HOperatorSet.SetFramegrabberParam(_frameGrabber1, "UserSetSelector", "UserSet1");

                    string ImgType;
                    HOperatorSet.GrabImage(out HObject hObject, _frameGrabber1);
                    HImage _Img = new HImage(hObject);
                    _Img.GetImagePointer1(out ImgType, out int ImgWidth, out int ImgHeight);

                    _Window11.SetPart(0, 0, ImgHeight - 1, ImgWidth - 1);
                    _Window12.SetPart(0, 0, ImgHeight - 1, ImgWidth - 1);

                    _Img.Dispose();
                    hObject.Dispose();
                    btnCam1.BackColor = colorCamConnected;
                    _camConect1 = true;
                    btnSettingCam1.Invoke((MethodInvoker)delegate
                    {
                        btnSettingCam1.Enabled = false;
                    });

                }
                catch (Exception ex)
                {
                    _camConect1 = false;
                    btnCam1.BackColor = colorCamDisConnected;
                }
            });
            Task task2 = Task.Factory.StartNew(() =>
            {
                try
                {
                    string _interfaceName = _camSetting2.Interface;
                    string _deviceName = _camSetting2.Device;
                    HOperatorSet.OpenFramegrabber(_interfaceName, 0, 0, 0, 0, 0, 0
                            , "progressive"
                            , -1
                            , "default"
                            , "num_buffers=2"
                            , "default"
                            , "default"
                            , _deviceName
                            , 0
                            , -1
                            , out _frameGrabber2);
                    _frameGrabber2trans = _frameGrabber2;
                    //HOperatorSet.GetFramegrabberParam(_frameGrabber2, "num_buffers", out HTuple Value);
                    //HOperatorSet.SetFramegrabberParam(_frameGrabber2, "ExposureTime", 41000);
                    //HOperatorSet.SetFramegrabberParam(_frameGrabber2, "Gain", 18);
                    //HOperatorSet.SetFramegrabberParam(_frameGrabber2, "UserSetSelector", "UserSet1");


                    btnCam2.BackColor = colorCamConnected;
                    _camConect2 = true;
                    btnSettingCam2.Invoke((MethodInvoker)delegate
                    {
                        btnSettingCam2.Enabled = false;
                    });
                }
                catch (Exception ex)
                {
                    _camConect2 = false;
                    btnCam2.BackColor = colorCamDisConnected;
                }
            });
        }

            private void fmMain_Load(object sender, EventArgs e)
        {
            GlobVar.RTCVision = new cSystemTypes();
            GlobVar.RTCVision.ReadDefault();
            Lib.IsConnected = false;
            Lib.FileNameJob = AppDomain.CurrentDomain.BaseDirectory + "Program" + Path.DirectorySeparatorChar + "VisionDB.db";
            loadCamSetting();
            //directoryFolder();
            //configLoad();
            //loadDataSpec();
            //loadcShowSpec();
            WindowControlLoad();
            //setDrawAndLineWidth();
            connectAllCameraAsync();
        }
    }
}

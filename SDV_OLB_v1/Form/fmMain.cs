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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace SDV_OLB_v1
{
    public partial class fmMain : Form
    {
        string _pathVisionDB = "D:/RTC_Project/SDV OLB/SDV_OLB_v1/SDV_OLB_v1/bin/Debug/VisionDB.db";
        bool _camConect1 = false;
        bool _camConect2 = false;
        bool _isLiveCam1 = false;
        bool _isLiveCam2 = false;
        bool _isManual = false;
        bool _isConnectPLC1 = false;
        bool _isConnectPLC2 = false;
        bool _isAuto = false;
        public HTuple _socketPLC;
        //----------------------------------------------------------------------------------//
        PLC3eClient plcCAM1 = null;
        PLC3eClient plcCAM2 = null;
        List<CamSetting> _lstCamSettings = new List<CamSetting>();
        CamSetting _camSetting1 = new CamSetting();
        CamSetting _camSetting2 = new CamSetting();

        cDeviceSetting _cDevice = new cDeviceSetting();

        //########################## Frame and Himage

        public HTuple _frameGrabber1 = null;
        public HTuple _frameGrabber2 = null;
        public static HTuple _frameGrabber1trans;
        public static HTuple _frameGrabber2trans;
        public HWindow _Window11;
        public HWindow _Window12;
        HObject _ImageManual1 = null;
        HObject _ImageManual2;
        HObject ImgReadfolder;



        //----------------------------------------------------------------------------------//
        //########  set color
        Color colorCamConnected = Color.FromArgb(111, 174, 70);
        Color colorCamDisConnected = Color.Red;
        //####### Data trans Form
        public static int sendsettingcam = 0;
        public static int sendsettingcamDetect = 0;
        public static bool showroi = false;
        public static bool saveImgGraphics = false;
        public static bool allRsOK = false;
        public static bool saveImage = true;
        public fmMain()
        {
            InitializeComponent();
        }


        private HObject snap(HWindow _window, HTuple framGraber, HObject _Image, int IndexCam, int position)
        {
            if (framGraber != null)
            {
                try
                {
                    //_Img.Dispose();
                    HOperatorSet.GrabImage(out _Image, framGraber);
                    HOperatorSet.GetImageSize(_Image, out HTuple _width, out HTuple _heigh);
                    _window.SetPart(new HTuple(0), new HTuple(0), _heigh, _width);
                    _Image.DispObj(_window);

                }
                catch
                {

                }
                //_Img = _frameGrabber.GrabImage();
                return _Image;

            }
            else return new HObject();

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

        public void loadcDevicePLC()
        {
            // Load Data Cam 1
            DataTable dttb1 = Lib.GetTableData(string.Format(@"select * from SettingPLC where Cam ={0}", 1),_pathVisionDB);
            if (dttb1.Rows.Count > 0)
            {
                _cDevice.IPAdress = dttb1.Rows[0]["IPAdress"].ToString();
                _cDevice.Port1 = Lib.ToInt(dttb1.Rows[0]["Port1"]);
                _cDevice.Port2 = Lib.ToInt(dttb1.Rows[0]["Port2"]);
                _cDevice.Ready1 = Lib.ToInt(dttb1.Rows[0]["Ready"]);
                _cDevice.ImageOK1 = Lib.ToInt(dttb1.Rows[0]["ImageOK"]);//
                _cDevice.TopL1 = Lib.ToInt(dttb1.Rows[0]["TopL"]);
                _cDevice.TopR1 = Lib.ToInt(dttb1.Rows[0]["TopR"]);
                _cDevice.BotL1 = Lib.ToInt(dttb1.Rows[0]["BotL"]);
                _cDevice.BotR1 = Lib.ToInt(dttb1.Rows[0]["BotR"]);
                _cDevice.Left1 = Lib.ToInt(dttb1.Rows[0]["Left"]);
                _cDevice.Right1 = Lib.ToInt(dttb1.Rows[0]["Right"]);
                _cDevice.Result1 = Lib.ToInt(dttb1.Rows[0]["Result"]);
                _cDevice.Bubble1 = Lib.ToInt(dttb1.Rows[0]["Bubble"]);
                _cDevice.CompleteImage1 = Lib.ToInt(dttb1.Rows[0]["CompleteImage"]);
                _cDevice.MarkNG1 = Lib.ToInt(dttb1.Rows[0]["MarkNG"]);
                _cDevice.Triger1 = Lib.ToInt(dttb1.Rows[0]["Triger"]);
                _cDevice.Position1 = Lib.ToInt(dttb1.Rows[0]["Position"]);
                _cDevice.CompleteResult1 = Lib.ToInt(dttb1.Rows[0]["CompleteResult"]);
                _cDevice.LenghL1 = Lib.ToInt(dttb1.Rows[0]["Lengh"]);
                _cDevice.LenghR1 = Lib.ToInt(dttb1.Rows[0]["Width"]);
                _cDevice.ScoreL1 = Lib.ToInt(dttb1.Rows[0]["ScoreL"]);
                _cDevice.ScoreR1 = Lib.ToInt(dttb1.Rows[0]["ScoreR"]);
                _cDevice.DataBarcode1 = Lib.ToInt(dttb1.Rows[0]["DataBarcode"]);
                _cDevice.ScoreInput = Lib.ToInt(dttb1.Rows[0]["ScoreInput"]);
            }

            DataTable dttb2 = Lib.GetTableData(string.Format(@"select * from SettingPLC where Cam ={0}", 2),_pathVisionDB);
            if (dttb2.Rows.Count > 0)
            {
                _cDevice.Ready2 = Lib.ToInt(dttb2.Rows[0]["Ready"]);
                _cDevice.ImageOK2 = Lib.ToInt(dttb2.Rows[0]["ImageOK"]);//
                _cDevice.TopL2 = Lib.ToInt(dttb2.Rows[0]["TopL"]);
                _cDevice.TopR2 = Lib.ToInt(dttb2.Rows[0]["TopR"]);
                _cDevice.BotL2 = Lib.ToInt(dttb2.Rows[0]["BotL"]);
                _cDevice.BotR2 = Lib.ToInt(dttb2.Rows[0]["BotR"]);
                _cDevice.Left2 = Lib.ToInt(dttb2.Rows[0]["Left"]);
                _cDevice.Right2 = Lib.ToInt(dttb2.Rows[0]["Right"]);
                _cDevice.Result2 = Lib.ToInt(dttb2.Rows[0]["Result"]);
                _cDevice.Bubble2 = Lib.ToInt(dttb2.Rows[0]["Bubble"]);
                _cDevice.CompleteImage2 = Lib.ToInt(dttb2.Rows[0]["CompleteImage"]);
                _cDevice.MarkNG2 = Lib.ToInt(dttb2.Rows[0]["MarkNG"]);
                //TODO: 02/07/2021 Tạm fix để test
                _cDevice.Triger2 = Lib.ToInt(dttb2.Rows[0]["Triger"]);
                _cDevice.Position2 = Lib.ToInt(dttb2.Rows[0]["Position"]);
                _cDevice.CompleteResult2 = Lib.ToInt(dttb2.Rows[0]["CompleteResult"]);
                _cDevice.LenghL2 = Lib.ToInt(dttb2.Rows[0]["Lengh"]);
                _cDevice.LenghR2 = Lib.ToInt(dttb2.Rows[0]["Width"]);
                _cDevice.ScoreL2 = Lib.ToInt(dttb2.Rows[0]["ScoreL"]);
                _cDevice.ScoreR2 = Lib.ToInt(dttb2.Rows[0]["ScoreR"]);
                _cDevice.DataBarcode2 = Lib.ToInt(dttb2.Rows[0]["DataBarcode"]);
            }
            // Load Data Cam 2
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

        void connectPLCCam1()
        {
            string errMessage = string.Empty;
            try
            {
                plcCAM1 = new PLC3eClient(_cDevice.IPAdress, _cDevice.Port1);
                if (!plcCAM1.Connected) { errMessage = $"Connect PLC with Port {_cDevice.Port1} is error"; }
                //HOperatorSet.OpenSocketConnect(_cDevice.IPAdress, _cDevice.Port1, new HTuple("protocol", "timeout"), new HTuple("TCP4", 1000), out _socketPLC);
                else
                {
                    btnPLCconnect.BackColor = Color.Yellow;
                    _isConnectPLC1 = true;
                }

            }
            catch
            {
                errMessage = $"Connect PLC with Port {_cDevice.Port1} is error";
                _isConnectPLC1 = false;
            }


            if (errMessage != string.Empty && !_isAuto)
                MessageBox.Show(errMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        public void connectPLCCam2()
        {
            string errMessage = string.Empty;
            try
            {
                plcCAM2 = new PLC3eClient(_cDevice.IPAdress, _cDevice.Port2);
                if (!plcCAM2.Connected) { errMessage = errMessage == string.Empty ? $"Connect PLC with Port {_cDevice.Port2} is error" : errMessage + $"\nConnect PLC with Port {_cDevice.Port2} is error"; }
                else
                {
                    btnPLCconnect.BackColor = colorCamConnected;
                    _isConnectPLC2 = true;
                }
            }
            catch
            {
                errMessage = errMessage == string.Empty ? $"Connect PLC with Port {_cDevice.Port2} is error" : errMessage + $"\nConnect PLC with Port {_cDevice.Port2} is error";
                _isConnectPLC2 = false;
            }
            if (errMessage != string.Empty && !_isAuto)
                MessageBox.Show(errMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //try
            //{
            //    HOperatorSet.OpenSocketConnect(_cDevice.IPAdress, _cDevice.Port2, new HTuple("protocol", "timeout"), new HTuple("TCP4", 300.0), out _socketPLC2);
            //    
            //}
            //catch (Exception)
            //{

            //}
        }
        private void disconnectPLC()
        {

            try
            {
                if (_isConnectPLC1)
                {
                    if (plcCAM1.CloseSecket())
                    {
                        plcCAM1 = null;
                        _isConnectPLC1 = false;
                        btnPLCconnect.BackColor = Color.HotPink;
                    }
                }
                if (_isConnectPLC2)
                {
                    if (plcCAM2.CloseSecket())
                    {
                        plcCAM2 = null;
                        _isConnectPLC2 = false;
                        btnPLCconnect.BackColor = colorCamDisConnected;
                    }
                }
            }
            catch (Exception)
            {

            }

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

        private void btnSnapCam1_Click(object sender, EventArgs e)
        {
            HObject _ImgRun;
            if (_frameGrabber1 != null)
            {
                if (_ImageManual1 != null) _ImageManual1.Dispose();

            }
            //_ImgRun = snap(_Window11, _frameGrabber1, _ImageManual1, 0, 0);

            if (_isManual) { _ImgRun = ImgReadfolder; }
            else { _ImgRun = snap(_Window11, _frameGrabber1, _ImageManual1, 0, 0); }
            //RunCodeGetresult(1, _ImgRun, 1, 0, "");
            if (_ImgRun != null) _ImgRun.Dispose();
        }

        private void btnSnapCam2_Click(object sender, EventArgs e)
        {
            HObject _ImgRun;
            if (_frameGrabber2 != null)
            {
                if (_ImageManual2 != null) _ImageManual2.Dispose();
                //_ImgRun = snap(_Window21, _frameGrabber2, _ImageManual2, 0, 0);
            }
            if (_isManual) { _ImgRun = ImgReadfolder; }
            else { _ImgRun = snap(_Window12, _frameGrabber2, _ImageManual2, 0, 0); }
            //RunCodeGetresult(2, _ImgRun, 2, 0, "");
            if (_ImgRun != null) _ImgRun.Dispose();

        }

        private void btnPLCconnect_Click(object sender, EventArgs e)
        {
            try
            {
                if (!_isConnectPLC1)
                {
                    connectPLCCam1();
                }
                if (!_isConnectPLC2)
                {
                    connectPLCCam2();
                }
                else
                {
                    disconnectPLC();
                }

            }
            catch (Exception ex)
            {

            }
        }

        private void btnSettingCam1_Click(object sender, EventArgs e)
        {
            sendsettingcam = 1;
            fmLogin frm = new fmLogin();
            if (frm.ShowDialog() == DialogResult.OK) { }
            {
                frmCamTrain _trainCam = new frmCamTrain(this);
                _trainCam.Show();
            }
        }
    }
}

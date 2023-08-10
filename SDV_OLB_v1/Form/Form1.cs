using System.Text.RegularExpressions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HalconDotNet;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using SDV_OLB_v1.ClsProcess;

namespace SDV_OLB_v1
{
    public partial class Form1 : Form
    {
        Thread _threadlive;
        HWindow _Window;
        HWindow WindowTrain;
        HImage Img;
        HImage ImgLive;
        HImage _Imgtrain;
        int _width;
        int _height;
        int _exposuretime;
        int _gain;
        int _timeout;
        string _pathVisionDB = "D:/RTC_Project/SDV_OLB/SDV_OLB_v1/SDV_OLB_v1/bin/x64/Debug/VisionDB.db";
        HFramegrabber Framgraber;
        HFramegrabber Framgraber2;
        public Form1()
        {
            InitializeComponent();
        }
        private List<string> getAvilableInterface()
        {
            List<string> avilabeInterface = new List<string>();

            string halconroot = Environment.GetEnvironmentVariable("HALCONROOT");
            string halconarch = Environment.GetEnvironmentVariable("HALCONARCH");
            string a = halconroot + "/bin/" + halconarch;

            var acquisitionInterface = Directory.EnumerateFiles(a, "hacq*.dll");
            foreach (var item in acquisitionInterface)
            {
                string interfacename = Regex.Match(item, "hAcq(.+)(?:\\.dll)").Groups[1].Value;
                HTuple device;
                try
                {
                    HInfo.InfoFramegrabber(interfacename, "info_boards", out device);
                    //HInfo.InfoFramegrabber(interfacename, "device", out device);

                    if (device.Length > 0)
                    {
                        avilabeInterface.Add(interfacename);
                    }
                }
                catch (Exception ex)
                {

                }

            }
            return avilabeInterface;
        }

        private void my_MouseWheel(object sender, MouseEventArgs e)
        {
            Point pt = WindowControl.Location;
            MouseEventArgs newe = new MouseEventArgs(e.Button, e.Clicks, e.X - pt.X, e.Y - pt.Y, e.Delta);
            WindowControl.HSmartWindowControl_MouseWheel(sender, newe);
        }
        private void WindowControl_Load(object sender, EventArgs e)
        {
            _Window = WindowControl.HalconWindow;
            this.MouseWheel += my_MouseWheel;
        }

        void connectCam(string _interfacename, string _device)
        {

            _exposuretime = decimal.ToInt32(nbExTime.Value);
            _gain = decimal.ToInt32(nbGain.Value);

            if (Framgraber != null)
            {
                Framgraber.Dispose();
            }

            Framgraber = new HFramegrabber(_interfacename, 0, 0, 0, 0, 0, 0, "progressive",
            -1, "default", -1, "default", _interfacename == "File" ? _device : "default", _interfacename == "File" ? "default" : _device, 0, -1);
            if (_interfacename == "GigEVision2")
            {
                HOperatorSet.SetFramegrabberParam(Framgraber, "ExposureTimeAbs", _exposuretime);
                HOperatorSet.SetFramegrabberParam(Framgraber, "GainRaw", _gain);
                HOperatorSet.SetFramegrabberParam(Framgraber, "grab_timeout", 60000);
            }

            Img = Framgraber.GrabImageAsync(1);
            Img.GetImagePointer1(out HTuple typeImg, out HTuple WidthImg, out HTuple HeightImg);
            HTuple a = 0;
            _Window.SetPart(a, a, HeightImg - 1, WidthImg - 1);
            Img.Dispose();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            autoDetect();
        }

        void autoDetect()
        {
            cbxInterface.Items.Clear();
            List<string> interfacesname = getAvilableInterface();
            foreach (var item in interfacesname)
            {
                cbxInterface.Items.Add(item);
            }
            if (interfacesname.Count > 0)
            {
                cbxInterface.SelectedIndex = interfacesname.Count - 1;
            }
        }

        private void cbxInterface_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbxCamera.Items.Clear();
            string interfacename = cbxInterface.SelectedItem.ToString();
            HTuple device;
            HInfo.InfoFramegrabber(interfacename, "device", out device);
            if (interfacename == "GenICamTL")
            {
                device = null;
                HOperatorSet.InfoFramegrabber(interfacename, "info_boards", out HTuple Information, out device);

                //GetDeviceCam(interfacename);
            }
            for (int i = 0; i < device.Length; i++)
            {
                cbxCamera.Items.Add(device[i].S);
            }
            if (cbxCamera.Items.Count == 0)
            {
                cbxCamera.Items.Add(Application.StartupPath + "/images");// SUA LAI
            }
            else
            {
                cbxCamera.SelectedIndex = device.Length - 1;
            }
        }

        private void btnTestConnect_Click_1(object sender, EventArgs e)
        {
            string _interface = cbxInterface.Text;
            string _device = cbxCamera.Text;
            try
            {
                if (btnTestConnect.Text.ToLower() == "connection".ToLower())
                {
                    connectCam(_interface, _device);
                    btnTestConnect.BackColor = Color.Green;
                    btnTestConnect.Text = "Connected";
                    //pnFindCam.Enabled = false;
                }
                else
                {
                    HOperatorSet.CloseFramegrabber(Framgraber);
                    btnTestConnect.Text = "Connection";
                    pnFindCam.Enabled = true;
                    btnTestConnect.BackColor = Color.Lavender;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Conect Error :" + ex);
            }

        }
        void live()
        {
            if (Framgraber == null)
            {
                MessageBox.Show("Please connect Camera", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            while (true)
            {
                Thread.Sleep(200);
                try
                {
                    HOperatorSet.GrabImage(out HObject ImgLiv, Framgraber);
                    ImgLive = new HImage(ImgLiv);
                    ImgLive.DispObj(_Window);
                }
                catch (Exception)
                {

                }
            }
        }
        void loadCamera()
        {
            lbCurrentInterface.Text = cbxInterface.Text;
            lbCurrentDevice.Text = cbxCamera.Text;
        }
        void getParameterCam()
        {
            int _camIndex = Convert.ToInt32(cbxCamIndex.SelectedItem);
            DataTable dt = Lib.GetTableData(string.Format(@"select * from CameraSetting where CamIndex = {0}", _camIndex), _pathVisionDB);
            nbExTime.Value = Lib.ToDecimal(dt.Rows[0]["ExposureTime"]);
            nbGain.Value  = Lib.ToDecimal(dt.Rows[0]["Gain"]);
            nbTimeout.Value = Lib.ToDecimal(dt.Rows[0]["Timeout"]);
        }
        void saveParameterCam()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(cbxCamera.SelectedItem.ToString()))
                {
                    MessageBox.Show("Please choose Cam  before  Save!!!");
                    return;
                }
                if (cbxInterface.SelectedItem.ToString() == "")
                {
                    MessageBox.Show("Please choose Cam  before  Save!!!");
                    return;
                }

                string _interfacename = cbxInterface.SelectedItem.ToString();
                string _device = cbxCamera.SelectedItem.ToString();
                decimal exposuretime = nbExTime.Value;
                decimal gain = nbGain.Value;
                decimal timeout = nbTimeout.Value;
                int _camIndex = Convert.ToInt32(cbxCamIndex.SelectedItem);

                DataTable dt = Lib.GetTableData(string.Format(@"select * from CameraSetting where CamIndex = {0}", _camIndex), _pathVisionDB);
                string datasave = "";
                if (dt.Rows.Count > 0)
                {
                    datasave = string.Format(@"update CameraSetting set Interface = '{0}',Device = '{1}', ExposureTime = {2}, Gain = {3}, Timeout ={4} where CamIndex = {5}", _interfacename, _device, exposuretime, gain, timeout, _camIndex);
                }
                else
                {
                    datasave = string.Format(@"insert into CameraSetting (Interface, Device, ExposureTime,Gain,Timeout,CamIndex) values ({0}, {1},{2},{3},{4},{5})", _interfacename, _device, exposuretime, gain, timeout, _camIndex);
                }

                Lib.ExecuteQuery(datasave, _pathVisionDB);
                MessageBox.Show("Save Success");
            }
            catch { 

            }

        }


        private void btnLiveCam_Click(object sender, EventArgs e)
        {
            if (Framgraber == null)
            {
                MessageBox.Show("Please connect Camera before Snap.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (btnLiveCam.Text.ToLower() == "live")
            {
                _threadlive = new Thread(new ThreadStart(live));
                _threadlive.IsBackground = true;
                _threadlive.Start();
                btnLiveCam.Text = "Stop";

            }
            else
            {

                HOperatorSet.CloseFramegrabber(Framgraber);
                _threadlive.Interrupt();
                btnLiveCam.Text = "Live";
            }
        }




        private void btnSaveCam_Click(object sender, EventArgs e)
        {
            saveParameterCam();
        }

        private void cbxCamIndex_SelectedIndexChanged(object sender, EventArgs e)
        {
            getParameterCam();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
using HalconDotNet;
using SDV_OLB_v1.ClsProcess;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SDV_OLB_v1
{
    public partial class frmCamTrain : Form
    {

        //------------------------------------------/////
        private HWindow _WindowTrain;
        string _pathVisionDB = "D:/RTC_Project/SDV OLB/SDV_OLB_v1/SDV_OLB_v1/bin/Debug/VisionDB.db";
        public fmMain _frmMain;
        public static HTuple _framGraber1;
        public static HTuple _framGraber2;
        HDevEngine _procderduce = new HDevEngine();
        string _procedurePath = Application.StartupPath + @"\vision\HalconProcedures";
        //-----------------------------------------/////

        void loadDataTrain()
        {
            int indexCam = Lib.ToInt(cbxCam.SelectedItem);
            DataTable camSetting = Lib.GetTableData(string.Format("select * from CameraSetting where CamIndex = {0}", indexCam),_pathVisionDB);
            if (camSetting.Rows.Count > 0)
            {
                nbrExposure.Value = Lib.ToDecimal(camSetting.Rows[0]["ExposureTime"]);
                nbrAgain.Value = Lib.ToDecimal(camSetting.Rows[0]["Gain"]);
            }

            int indexRegion = Lib.ToInt(cbxRegion.SelectedItem);

            DataTable dt = Lib.GetTableData(string.Format("select * from TrainCam where Program = '{0}' " +
                "and IndexCam = {1} and TypeRegion = {2} and Region = {3}", 1, indexCam, 1, indexRegion), _pathVisionDB);
            if (dt.Rows.Count > 0)
            {
                txtX1.Value = Lib.ToDecimal(dt.Rows[0]["X1"]);
                txtY1.Value = Lib.ToDecimal(dt.Rows[0]["Y1"]);
                txtX2.Value = Lib.ToDecimal(dt.Rows[0]["X2"]);
                txtY2.Value = Lib.ToDecimal(dt.Rows[0]["Y2"]);
                txtTheTa.Value = Lib.ToDecimal(dt.Rows[0]["TheTa"]);

                //txtMinScore.Value = Lib.ToDecimal(dt.Rows[0]["MinScore"]);
            }
            else
            {
                txtX1.Value = 0;
                txtY1.Value = 0;
                txtX2.Value = 0;
                txtY2.Value = 0;
                txtTheTa.Value = 0;
            }
        }

        private void settingcamfromMain(int cam)
        {
            if (cam == 1)
            {
                cam = 1;
                //trigger = 1;
                //bitcompprocess = 1;
                cbxCam.SelectedItem = "1";
            }
            else if (cam == 2)
            {
                cam = 2;
                //trigger = 2;
                //bitcompprocess = 1;
                cbxCam.SelectedItem = "2";
            }
            //if (cam < 5 && cam > 0)
            //{
            //    cbxCam.Enabled = false;
            //}
            //else if (cam == 5)
            //{
            //    cbxCam.Enabled = true;
            //}

            //if (cam == 1 || cam == 3)
            //{
            //    lstPoisitionMain = new List<string> { "Left", "PatternLeft", "PatternLeft01" };
            //}
            //else if (cam == 2 || cam == 4)
            //{
            //    lstPoisitionMain = new List<string> { "Right", "PatternRight", "PatternRight01" };
            //}
            //else if (cam == 5)
            //{
            //    lstPoisitionMain = new List<string> { "Left", "PatternLeft", "PatternLeft01", "Right", "PatternRight", "PatternRight01" };
            //}
            //cbxPosistion.DataSource = lstPoisitionMain;
        }
        void loadModel()
        {
            string sql = $"select * from Program";
            DataTable dt = Lib.GetTableData(sql,_pathVisionDB);

        }
        public frmCamTrain()
        {
            InitializeComponent();
        }

        public frmCamTrain(fmMain frMmain)
        {
            InitializeComponent();
            _frmMain = frMmain;
        }

        private void frmCamTrain_Load(object sender, EventArgs e)
        {
            _WindowTrain.SetColor("green");
            _WindowTrain.SetDraw("margin");
            _WindowTrain.SetLineWidth(1);
            cbxCam.SelectedIndex = 1;
            cbxPosistion.SelectedIndex = 1;
            int settingcam = fmMain.sendsettingcam;
            settingcamfromMain(settingcam);
            loadDataTrain();
            loadModel();
            _framGraber1 = fmMain._frameGrabber1trans;
            _framGraber2 = fmMain._frameGrabber2trans;
            _procderduce.SetProcedurePath(_procedurePath);
            //loadHdevProcedure();
            //loadnumberPoint();
            //LoadSettingParamCheckKeo();
            //cbxModel.SelectedIndex = 0;
            //loadDataVacant(0);
            //loadDataVacant(1);
            //configLoad();
        }

        private void WH_Load(object sender, EventArgs e)
        {
            _WindowTrain = WH.HalconWindow;
            this.MouseWheel += my_MouseWheel;
        }
        private void my_MouseWheel(object sender, MouseEventArgs e)
        {
            Point pt = WH.Location;
            MouseEventArgs newe = new MouseEventArgs(e.Button, e.Clicks, e.X - pt.X, e.Y - pt.Y, e.Delta);
            WH.HSmartWindowControl_MouseWheel(sender, newe);
        }
    }
}

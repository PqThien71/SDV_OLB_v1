using HalconDotNet;
using SDV_OLB_v1.ClsProcess;
using SDV_OLB_v1.ClassSave;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Web.UI.WebControls;

namespace SDV_OLB_v1
{
    public partial class frmCamTrain : Form
    {

        //------------------------------------------/////
        double X1, X2, Y1, Y2, Theta;



        private HWindow _WindowTrain;
        string _pathVisionDB = "D:/RTC_Project/SDV_OLB/SDV_OLB_v1/SDV_OLB_v1/bin/x64/Debug/VisionDB.db";
        public fmMain _frmMain;
        public static HTuple _framGraber1;
        public static HTuple _framGraber2;
        List<cKeoSetting> lstParamSettingGlue = new List<cKeoSetting>();
        List<cBubbleSetting> lstParamSettingBubble = new List<cBubbleSetting>();
        private HDrawingObject _drawing_object_Region;
        HObject imageSnap = null;

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
        private void loadnumberPoint()
        {
            DataTable dt = Lib.GetTableData(@"select * from SpecSetting ",_pathVisionDB);
            if (dt.Rows.Count > 0)
            {
                nbrNumberPoint.Value = Lib.ToDecimal(dt.Rows[0]["NumberPoint"]);
            }
        }
        cHdevProcedure cHdevPro = new cHdevProcedure();
        public void loadHdevProcedure()
        {
            cHdevPro.HdevProRecPLC = new HDevProcedure("Melsoft_3E_Revc");
            cHdevPro.HdevProSendPLC = new HDevProcedure("Melsoft_3E_Send");
            cHdevPro.HdevProCheckBOT = new HDevProcedure("Check_Keo_Bot");
            cHdevPro.HdevProCheckTOP = new HDevProcedure("Check_Keo_Top");
            cHdevPro.HdevProCheckLEFT = new HDevProcedure("Check_Keo_Left");
            cHdevPro.HdevProCheckRIGHT = new HDevProcedure("Check_Keo_Right");
            //cHdevPro.HdevProCheckVacantUnder = new HDevProcedure("Check_Vancant_Glue");
            //cHdevPro.HdevProCheckVacantOver = new HDevProcedure("Blob");
        }

        private void LoadSettingParamCheckKeo()
        {
            try
            {
                lstParamSettingBubble.Clear();
                lstParamSettingGlue.Clear();
                DataTable dttb = Lib.GetTableData("select * from SettingParamCheckKeo ",_pathVisionDB);
                int count = dttb.Rows.Count;
                if (count == 0) return;

                for (int i = 0; i < count; i++)
                {
                    if (dttb.Rows.Count > 0)
                    {
                        cBubbleSetting _cBubbleSetting = new cBubbleSetting();
                        cKeoSetting _cKeoSetting = new cKeoSetting();

                        _cBubbleSetting.CamType = dttb.Rows[i]["CamType"].ToString();
                        _cBubbleSetting.MaskWidth = Convert.ToInt32(dttb.Rows[i]["MaskWidth"]);
                        _cBubbleSetting.MaskHeight = Convert.ToInt32(dttb.Rows[i]["MaskHeight"]);
                        _cBubbleSetting.ValueOffset = Convert.ToInt32(dttb.Rows[i]["ValueOffset"]);
                        _cBubbleSetting.LightDark = dttb.Rows[i]["LightDark"].ToString();
                        _cBubbleSetting.MinSize = Convert.ToDouble(dttb.Rows[i]["MinSize"]);
                        _cBubbleSetting.MaxSize = Convert.ToDouble(dttb.Rows[i]["MaxSize"]);


                        _cKeoSetting.CamType = dttb.Rows[i]["CamType"].ToString();
                        _cKeoSetting.MaskWidthTop = Convert.ToInt32(dttb.Rows[i]["MaskWidthTop"]);
                        _cKeoSetting.MaskHeightTop = Convert.ToInt32(dttb.Rows[i]["MaskHeightTop"]);
                        _cKeoSetting.ValueOffsetTop = Convert.ToInt32(dttb.Rows[i]["ValueOffsetTop"]);

                        _cKeoSetting.MaskWidthBot = Convert.ToInt32(dttb.Rows[i]["MaskWidthBot"]);
                        _cKeoSetting.MaskHeightBot = Convert.ToInt32(dttb.Rows[i]["MaskHeightBot"]);
                        _cKeoSetting.ValueOffsetBot = Convert.ToInt32(dttb.Rows[i]["ValueOffsetBot"]);

                        _cKeoSetting.MaskWidthLeft = Convert.ToInt32(dttb.Rows[i]["MaskWidthLeft"]);
                        _cKeoSetting.MaskHeightLeft = Convert.ToInt32(dttb.Rows[i]["MaskHeightLeft"]);
                        _cKeoSetting.ValueOffsetLeft = Convert.ToInt32(dttb.Rows[i]["ValueOffsetLeft"]);

                        _cKeoSetting.MaskWidthRight = Convert.ToInt32(dttb.Rows[i]["MaskWidthRight"]);
                        _cKeoSetting.MaskHeightRight = Convert.ToInt32(dttb.Rows[i]["MaskHeightRight"]);
                        _cKeoSetting.ValueOffsetRight = Convert.ToInt32(dttb.Rows[i]["ValueOffsetRight"]);
                        _cKeoSetting.PositionCheckBot = Convert.ToInt32(dttb.Rows[i]["PositionCheckBot"]);
                        _cKeoSetting.PositionCheckTop = Convert.ToInt32(dttb.Rows[i]["PositionCheckTop"]);
                        _cKeoSetting.PositionCheckLeft = Convert.ToInt32(dttb.Rows[i]["PositionCheckLeft"]);
                        _cKeoSetting.PositionCheckRight = Convert.ToInt32(dttb.Rows[i]["PositionCheckRight"]);
                        _cKeoSetting.MinScore = Convert.ToDouble(dttb.Rows[i]["MinScore"]);

                        _cKeoSetting.BlobMin = Convert.ToInt32(dttb.Rows[i]["BlobMin"]);
                        _cKeoSetting.BlobMax = Convert.ToInt32(dttb.Rows[i]["BlobMax"]);
                        _cKeoSetting.BlobHeightMin = Convert.ToInt32(dttb.Rows[i]["BlobHeightMin"]);
                        _cKeoSetting.BlobHeightMax = Convert.ToInt32(dttb.Rows[i]["BlobHeightMax"]);

                        lstParamSettingBubble.Add(_cBubbleSetting);
                        lstParamSettingGlue.Add(_cKeoSetting);
                    }
                }
            }
            catch (Exception)
            {


            }
            UpdateParamSetting();
        }
        private void UpdateParamSetting()
        {
            string camTypeRef = cbxCam.SelectedItem.ToString() + cbxPosistion.SelectedItem.ToString();

            cKeoSetting keoSetting = new cKeoSetting();
            cBubbleSetting bubbleSetting = new cBubbleSetting();
            keoSetting = lstParamSettingGlue.FirstOrDefault(o => o.CamType == camTypeRef);
            bubbleSetting = lstParamSettingBubble.FirstOrDefault(o => o.CamType == camTypeRef);

            if (keoSetting != null)
            {
                nbrMaskWidth.Value = Lib.ToDecimal(bubbleSetting.MaskWidth);
                nbrMaskHeight.Value = Lib.ToDecimal(bubbleSetting.MaskWidth);
                nbrValueOffset.Value = Lib.ToDecimal(bubbleSetting.ValueOffset);
                nbrMaxSize.Value = Lib.ToDecimal(bubbleSetting.MaxSize);
                nbrMinSize.Value = Lib.ToDecimal(bubbleSetting.MinSize);
                cbLightDark.SelectedItem = bubbleSetting.LightDark.ToString().Trim();

                nbrMaskWidthTop.Value = Lib.ToDecimal(keoSetting.MaskWidthTop);
                nbrMaskHeightTop.Value = Lib.ToDecimal(keoSetting.MaskHeightTop);
                nbrValueOffsetTop.Value = Lib.ToDecimal(keoSetting.ValueOffsetTop);

                nbrMaskWidthBot.Value = Lib.ToDecimal(keoSetting.MaskWidthBot);
                nbrMaskHeightBot.Value = Lib.ToDecimal(keoSetting.MaskHeightBot);
                nbrValueOffsetBot.Value = Lib.ToDecimal(keoSetting.ValueOffsetBot);

                nbrMaskWidthLeft.Value = Lib.ToDecimal(keoSetting.MaskWidthLeft);
                nbrMaskHeightLeft.Value = Lib.ToDecimal(keoSetting.MaskHeightLeft);
                nbrValueOffsetLeft.Value = Lib.ToDecimal(keoSetting.ValueOffsetLeft);

                nbrMaskWidthRight.Value = Lib.ToDecimal(keoSetting.MaskWidthRight);
                nbrMaskHeightRight.Value = Lib.ToDecimal(keoSetting.MaskHeightRight);
                nbrValueOffsetRight.Value = Lib.ToDecimal(keoSetting.ValueOffsetRight);
                cbxPositionCheckBot.SelectedIndex = Lib.ToInt(keoSetting.PositionCheckBot) - 1;
                cbxPositionCheckTop.SelectedIndex = Lib.ToInt(keoSetting.PositionCheckTop) - 1;

                cbxPositionCheckLeft.SelectedIndex = Lib.ToInt(keoSetting.PositionCheckLeft) - 1;
                cbxPositionCheckRight.SelectedIndex = Lib.ToInt(keoSetting.PositionCheckRight) - 1;

                nbrMinScore.Value = Lib.ToDecimal(keoSetting.MinScore);

                nbrBlobMin.Value = Lib.ToDecimal(keoSetting.BlobMin);
                nbrBlobMax.Value = Lib.ToDecimal(keoSetting.BlobMax);
                nbrBlobHeightMin.Value = Lib.ToDecimal(keoSetting.BlobHeightMin);
                nbrBlobHeightMax.Value = Lib.ToDecimal(keoSetting.BlobHeightMax);
            }

        }

        private void loadDataVacant(int eRegion)
        {

            int ePosition = -1;
            int indexCam = Lib.ToInt(cbxCam.SelectedItem);

            string position = Lib.ToString(cbxPosistion.SelectedItem);
            if (position == "Left")
            {
                ePosition = 0;
            }
            else if (position == "Right")
            {
                ePosition = 1;
            }
            //if (isOver)
            //{
            //    eRegion = 0;
            //}
            //else if (isUnder)
            //{
            //    eRegion = 1;
            //}

            DataTable dt = Lib.GetTableData(string.Format("select * from CheckVacant where " +
                "CamIndex = {0} and Position = '{1}' and Region = '{2}'", indexCam, ePosition, eRegion),_pathVisionDB);

            //int indexCam = Lib.ToInt(cbxCam.SelectedItem);
            if (eRegion == 0)
            {
                if (dt.Rows.Count > 0)
                {
                    nbrValuePixel.Value = Lib.ToDecimal(dt.Rows[0]["ValueOffset"]);
                    nbrFilterBlobMin.Value = Lib.ToDecimal(dt.Rows[0]["FilBlobMin"]);
                    nbrFilterBlobMax.Value = Lib.ToDecimal(dt.Rows[0]["FilBlobMax"]);
                    nbrAreaMinCompare.Value = Lib.ToDecimal(dt.Rows[0]["AreaMin"]);
                    nbrAreMaxCompare.Value = Lib.ToDecimal(dt.Rows[0]["AreaMax"]);

                    //txtMinScore.Value = Lib.ToDecimal(dt.Rows[0]["MinScore"]);
                }
                else
                {
                    nbrValuePixel.Value = 0;
                    nbrFilterBlobMin.Value = 0;
                    nbrFilterBlobMax.Value = 0;
                    nbrAreaMinCompare.Value = 0;
                    nbrAreMaxCompare.Value = 0;
                }
            }
            else
            {
                if (dt.Rows.Count > 0)
                {
                    nbrThreshMinOver.Value = Lib.ToDecimal(dt.Rows[0]["FilBlobMin"]);
                    nbrThreshMaxOver.Value = Lib.ToDecimal(dt.Rows[0]["FilBlobMax"]);
                    nbrAreaMinOVer.Value = Lib.ToDecimal(dt.Rows[0]["AreaMin"]);
                    nbrAreaMaxOver.Value = Lib.ToDecimal(dt.Rows[0]["AreaMax"]);
                    nbrSpecAreaMin.Value = Lib.ToDecimal(dt.Rows[0]["ValueOffset"]);

                    //txtMinScore.Value = Lib.ToDecimal(dt.Rows[0]["MinScore"]);
                }
                else
                {
                    nbrThreshMinOver.Value = 0;
                    nbrThreshMaxOver.Value = 0;
                    nbrAreaMinOVer.Value = 0;
                    nbrAreaMaxOver.Value = 0;
                    nbrSpecAreaMin.Value = 0;
                }
            }


        }
        void creatModelRegion(double x1, double y1, double theta, double x2, double y2)
        {
            if (_drawing_object_Region != null)
            {
                _drawing_object_Region.Dispose();
                _drawing_object_Region = null;
            }

            _drawing_object_Region = HDrawingObject.CreateDrawingObject(HDrawingObject.HDrawingObjectType.RECTANGLE2, x1, y1, theta, x2, y2);
            _drawing_object_Region.SetDrawingObjectParams("color", "red");
            _drawing_object_Region.OnDrag(GetPosition);
            _drawing_object_Region.OnResize(GetPosition);
            _WindowTrain.AttachDrawingObjectToWindow(_drawing_object_Region);
        }
        private void GetPosition(HDrawingObject dobj, HWindow hwin, string type)
        {
            HRegion region = new HRegion(dobj.GetDrawingObjectIconic());
            HOperatorSet.RegionFeatures(region, new HTuple(new string[] { "row", "column", "rect2_len1", "rect2_len2", "phi" }), out HTuple values);

            double[] arrPosition = values.ToDArr();
            txtX1.Value = Lib.ToDecimal(arrPosition[0]);
            txtY1.Value = Lib.ToDecimal(arrPosition[1]);
            txtX2.Value = Lib.ToDecimal(arrPosition[2]);
            txtY2.Value = Lib.ToDecimal(arrPosition[3]);
            txtTheTa.Value = Lib.ToDecimal(arrPosition[4]);

            X1 = Convert.ToDouble(txtX1.Value);
            Y1 = Convert.ToDouble(txtY1.Value);
            X2 = Convert.ToDouble(txtX2.Value);
            Y2 = Convert.ToDouble(txtY2.Value);
            Theta = Convert.ToDouble(txtTheTa.Value);
        }
        private void configLoad()
        {
            DataTable dt = Lib.GetTableData("select * from Config ",_pathVisionDB);
            ckbUsingPattern2.Checked = Lib.ToInt(dt.Rows[0]["UsingPattern2"]) == 1 ? true : false;
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
        private void btnSnap_Click(object sender, EventArgs e)
        {
            int IndexCam = Convert.ToInt16(cbxCam.SelectedItem);
            snapImage(IndexCam, 0);
        }

        private void snapImage(int IndexCam, int Posttion)
        {
            HTuple _Frambraber = null;
            if (IndexCam == 1)
            {
                _Frambraber = _framGraber1;

            }
            if (IndexCam == 2)
            {
                _Frambraber = _framGraber2;
            }
            if (_Frambraber == null) return;
            try
            {
                imageSnap.Dispose();
            }
            catch
            {

            }
            //_Img = _frameGrabber.GrabImage();
            HOperatorSet.GrabImage(out imageSnap, _Frambraber);
            HOperatorSet.RotateImage(imageSnap, out imageSnap, 180, "constant");
            HOperatorSet.GetImageSize(imageSnap, out HTuple _width, out HTuple _height);
            _WindowTrain.SetPart(new HTuple(0), new HTuple(0), _height, _width);
            //SmartSetPart(image, WindowControl);
            imageSnap.DispObj(_WindowTrain);
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
            loadHdevProcedure();
            loadnumberPoint();
            LoadSettingParamCheckKeo();
            cbxModel.SelectedIndex = 0;
            loadDataVacant(0);
            loadDataVacant(1);
            configLoad();
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

        private void newRegion_Click(object sender, EventArgs e)
        {
            if (_drawing_object_Region != null)
            {
                _WindowTrain.DetachDrawingObjectFromWindow(_drawing_object_Region);
                _drawing_object_Region.Dispose();
                _drawing_object_Region = null;
            }
            creatModelRegion(100, 100, 0, 100, 100);
        }
    }
}

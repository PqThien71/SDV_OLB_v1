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
        List<CamSetting> _lstCamSettings = new List<CamSetting>();
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
        }



        private void fmMain_Load(object sender, EventArgs e)
        {
            GlobVar.RTCVision = new cSystemTypes();
            GlobVar.RTCVision.ReadDefault();
            Lib.IsConnected = false;
            Lib.FileNameJob = AppDomain.CurrentDomain.BaseDirectory + "Program" + Path.DirectorySeparatorChar + "VisionDB.db";

            loadCamSetting();
        }
    }
}

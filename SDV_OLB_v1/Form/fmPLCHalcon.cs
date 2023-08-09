using HalconDotNet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System;
using HalconDotNet;
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
    public partial class fmPLCHalcon : Form
    {
        fmMain _fmMain;

        HTuple _PLC_Socket;

        cHdevProcedure cHdevPro = new cHdevProcedure();
        public void loadHdevProcedure()
        {
            cHdevPro.HdevProRecPLC = new HDevProcedure("Melsoft_3E_Revc");
            cHdevPro.HdevProSendPLC = new HDevProcedure("Melsoft_3E_Send");
        }


        public fmPLCHalcon()
        {
            InitializeComponent();
        }

        private void fmPLCHalcon_Load(object sender, EventArgs e)
        {
            loadHdevProcedure();
            _PLC_Socket = _fmMain._socketPLC;
            if (_PLC_Socket.Length > 0)
            {
                btnConnect.BackColor = Color.FromArgb(111, 174, 70);
            }
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (_PLC_Socket.Length > 0)
            {
                HOperatorSet.CloseSocket(_PLC_Socket);
                _PLC_Socket = null;
            }
            try
            {
                HOperatorSet.OpenSocketConnect(txtIpPlc.Text, Convert.ToInt32(txtPort.Text), new HTuple("protocol", "timeout"), new HTuple("TCP4", Convert.ToInt32(txtTimeOut.Text)), out _PLC_Socket);
                btnConnect.BackColor = Color.Green;
            }
            catch (Exception)
            {

            }
        }
    }
}

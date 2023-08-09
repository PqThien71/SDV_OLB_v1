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
    public partial class fmLogin : Form
    {
        string _pass = "";
        string _path = Application.StartupPath + "/Pass/Pass.txt";

        private void getDataPassWord()
        {
            if (!File.Exists(_path))
            {
                File.Create(_path);
            }
            try
            {
                _pass = File.ReadAllText(Application.StartupPath + "/Pass/Pass.txt");
            }
            catch (Exception)
            {

            }

        }
        public fmLogin()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            lbNote.Text = "";
            this.Close();
        }

        private void fmLogin_Load(object sender, EventArgs e)
        {
            getDataPassWord();
        }

        private void btLogin_Click(object sender, EventArgs e)
        {
            if (txtPassword.Text == _pass)
            {
                this.DialogResult = DialogResult.OK;
                lbNote.Text = "";
                this.Close();
            }
            else
            {
                txtPassword.Text = "";
                lbNote.Text = "Wrong Password";
            }
        }
        private void txtPassword_TextChanged(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtPassword.Text == _pass)
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    txtPassword.Text = "";
                }
            }
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btLogin.PerformClick();
            }
        }
    }
}

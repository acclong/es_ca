using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ES.CA_ManagementUI
{
    public partial class frmHSMInputPassword : Form
    {
        private string _default = "";

        public string Message
        {
            set { lblMess.Text = value; }
        }

        public string DefaultPassword
        {
            set { _default = value; }
        }

        public string InputPassword
        {
            get { return txtPassword.Text; }
        }

        public frmHSMInputPassword(string message)
        {
            InitializeComponent();
            lblMess.Text = message;
        }

        private void frmInputPassword_Load(object sender, EventArgs e)
        {
            if (_default == "")
                btnDefault.Enabled = false;
            else
                btnDefault.Enabled = true;
        }

        private void btnDefault_Click(object sender, EventArgs e)
        {
            txtPassword.Text = ESLogin.StringCryptor.DecryptString(_default);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //Kiểm tra ràng buộc
            if (!clsShare.CheckStringHSM(InputPassword, 4, 32))
            {
                clsShare.Message_Error("Mã PIN chỉ chứa ký tự không dấu và độ dài từ 4 đến 32 ký tự.\nHãy kiểm tra lại!");
                return;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

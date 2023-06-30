using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace ESLogin
{
    public partial class frmLoginFull : Form
    {
        public ClsLogin mLogin = new ClsLogin();
        public string mConnTitle;
        public bool mOk = false;
        public bool IsAdmin = false;
        public bool mUseSQLConnection;
        private string sOld;

        public frmLoginFull()
        {
            InitializeComponent();
        }

        private void frmLoginFull_Load(object sender, EventArgs e)
        {
            btnOption_Click(sender, e);
            txtServerName.Text = mLogin.ServerName;
            txtDBName.Text = mLogin.DBName;

            txtUserName.Text = mLogin.UserName;
            sOld = mLogin.UserName;
            if (mLogin.DispPassword)
                txtPassword.Text = (string)mLogin.Password;

            lbBuiltVersion.Text = "Phiên bản " + mLogin.Version;
            lbResult.Text = mLogin.infolicense;
            mOk = false;
        }

        private void txtPassword_Enter(object sender, EventArgs e)
        {
            txtPassword.SelectAll();
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnLogin_Click(sender, e);
        }

        private void txtUserName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnLogin_Click(sender, e);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (mLogin.bHasLicense == false)
            {
                MessageBox.Show("Chưa được cấp phép hoặc đã hết hạn sử dụng!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string sServerName, sDBName;
            sServerName = txtServerName.Text.Trim();
            sDBName = txtDBName.Text.Trim();
            if (sServerName.Length == 0)
            {
                MessageBox.Show("Tên máy chủ chưa được nhập!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtServerName.Focus();
                return;
            }
            if (sDBName.Length == 0)
            {
                MessageBox.Show("Tên CSDL chưa được nhập!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtDBName.Focus();
                return;
            }
            
            mLogin.ServerName = sServerName;
            mLogin.DBName = sDBName;
            if (mUseSQLConnection)
            {
                if (chk_Qtri.Checked == false)
                {
                    mLogin.ConnectToSQL_SQLConnection_security(mLogin.ServerName, mLogin.DBName, txtUserName.Text.Trim(), txtPassword.Text);
                    IsAdmin = false;
                }
                else
                {
                    mLogin.ConnectToSQl_SQLConnection(mLogin.ServerName, mLogin.DBName, txtUserName.Text.Trim(), txtPassword.Text);
                    IsAdmin = true;
                }
                if (!mLogin.IsSQLConnected())
                    MessageBox.Show("Không kết nối được với máy chủ dữ liệu!\n" + "Lỗi: " + mLogin.m_sLastError, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    mOk = true;
                    mLogin.UserName = txtUserName.Text.Trim();
                    mLogin.Password = txtPassword.Text.Trim();
                    this.Close();
                }
            }
        }

        private void btnOption_Click(object sender, EventArgs e)
        {
            if (pnlMayChu.Visible == false)
            {
                this.Height = this.Height + pnlMayChu.Height;
                pnlMayChu.Visible = true;
            }
            else
            {
                this.Height = this.Height - pnlMayChu.Height;
                pnlMayChu.Visible = false;
            }
        }

        private void lbESOLUTIONS_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.e-solutions.com.vn/");
        }

    }
}

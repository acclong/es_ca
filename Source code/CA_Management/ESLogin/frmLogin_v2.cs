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
    public partial class frmLogin_v2 : Form
    {
        public ClsLogin mDB = new ClsLogin();
        public string mConnTitle;
        public bool mOk = false;
        public bool IsAdmin = false;
        public bool mUseSQLConnection;
        private string sOld;

        public frmLogin_v2()
        {
            InitializeComponent();
        }

        private void frmLogin_v2_Load(object sender, EventArgs e)
        {
            txtUserName.Text = mDB.UserName;
            sOld = mDB.UserName;
            if (mDB.DispPassword)
                txtPassword.Text = (string)mDB.Password;

            lbBuiltVersion.Text = "Phiên bản " + mDB.Version;
            lbResult.Text = mDB.infolicense;
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
            if (mDB.bHasLicense == false)
            {
                MessageBox.Show("Chưa được cấp phép hoặc đã hết hạn sử dụng!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string sServerName, sDBName;
            sServerName = mDB.ServerName.Trim();
            sDBName = mDB.DBName.Trim();
            if (sServerName.Length == 0)
            {
                MessageBox.Show("Tên máy chủ chưa được nhập !", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (sDBName.Length == 0)
            {
                MessageBox.Show("Tên CSDL chưa được nhập !", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            mDB.ServerName = sServerName;
            mDB.DBName = sDBName;
            
            if (mUseSQLConnection)
            {
                mDB.ConnectToSQl_SQLConnection(mDB.ServerName, mDB.DBName, txtUserName.Text.Trim(), txtPassword.Text.Trim());
                IsAdmin = true;
                if (!mDB.IsSQLConnected())
                {
                    mDB.ConnectToSQL_SQLConnection_security(mDB.ServerName, mDB.DBName, txtUserName.Text.Trim(), txtPassword.Text.Trim());
                    IsAdmin = false;
                }
                if (!mDB.IsSQLConnected())
                {
                    MessageBox.Show("Không thể kết nối máy chủ dữ liệu!" + "\nLỗi: " + mDB.m_sLastError, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    mOk = true;
                    mDB.UserName = txtUserName.Text.Trim();
                    mDB.Password = txtPassword.Text.Trim();
                    this.Close();
                }
            }            
        }
    }
}

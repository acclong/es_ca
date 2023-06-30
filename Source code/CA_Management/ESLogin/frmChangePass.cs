using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ESLogin
{
    public partial class frmChangePass : Form
    {
        public string userName;
        private BUS_UserManagement clsQT = new BUS_UserManagement();

        public frmChangePass()
        {
            InitializeComponent();
        }

        private void frmChangePass_Load(object sender, EventArgs e)
        {
            grbUser.Text = clsSharing.getUsername();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            userName = clsSharing.getUsername();
            if (userName == "sa")
            {
                MessageBox.Show("Bạn không thể đổi mật khẩu của tài khoản SA.", "Quản trị người dùng", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            //lấy thông tin về password hiện tại của user
            DataTable dt = clsQT.Q_USER_SelectByUsername(clsSharing.userName);

            if (dt == null || dt.Rows.Count == 0)
            {
                MessageBox.Show("Không lấy được thông tin về người dùng!", "Quản trị người dùng", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string oldPass = dt.Rows[0]["Passwords"].ToString();
            oldPass = StringCryptor.DecryptString(oldPass);

            if (txtOldPwd.Text != oldPass)
            {
                MessageBox.Show("Bạn phải nhập đúng password cũ", "Quản trị người dùng", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtOldPwd.Focus();
                return;
            }
            if (txtNewPwd1.Text != txtNewPwd2.Text)
            {
                MessageBox.Show("Hai password mới nhập lại không giống nhau", "Quản trị người dùng", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtNewPwd1.Focus();
                return;
            }

            try
            {
                string newPass = StringCryptor.EncryptString(txtNewPwd1.Text);
                clsQT.Q_USER_ChangePassword(clsSharing.userName, newPass);
                MessageBox.Show("Thay đổi mật khẩu thành công!", "Quản trị người dùng", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}

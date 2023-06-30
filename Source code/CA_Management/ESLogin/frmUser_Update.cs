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
    public partial class frmUser_Update : Form
    {
        private BUS_UserManagement clsQT = new BUS_UserManagement();
        private bool isAdd;
        private string _username;

        public frmUser_Update()
        {
            InitializeComponent();
            isAdd = true;
        }

        public frmUser_Update(string username)
        {
            InitializeComponent();
            _username = username;
            isAdd = false;
        }

        private void frmUser_Update_Load(object sender, EventArgs e)
        {
            try
            {
                if (isAdd == false)
                {
                    DataTable dt;
                    try
                    {
                        dt = clsQT.Q_USER_SelectByUsername(_username);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi trong quá trình lấy dữ liệu người dùng: " + ex.Message, "Quản trị người dùng", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    if (dt == null || dt.Rows.Count == 0) return;

                    string _pass = StringCryptor.DecryptString(dt.Rows[0]["PASSWORDS"].ToString());

                    txtUsername.Text = dt.Rows[0]["USERNAME"].ToString().Trim();
                    txtFullname.Text = dt.Rows[0]["FULLNAME"].ToString().Trim();
                    txtDescript.Text = dt.Rows[0]["DESCRIPT"].ToString().Trim();
                    txtPass.Text = _pass;
                    txtPassRe.Text = _pass;

                    txtUsername.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi " + ex.Message, "Quản trị người dùng", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (checkInput() == false) return;

            //kiểm tra xem user đã tồn tại hay chưa
            bool isOK = true;
            if (isAdd == true)
            {
                DataTable dt = clsQT.Q_USER_SelectByUsername(txtUsername.Text.Trim());
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (txtUsername.Text.Trim() == dt.Rows[i]["UserID"].ToString().Trim())
                    {
                        switch (MessageBox.Show("Người dùng này đã tồn tại. Bạn muốn ghi đè thông tin không?", "Quản trị người dùng", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
                        {
                            case DialogResult.Yes:
                                isOK = true;
                                break;
                            case DialogResult.No:
                                isOK = false;
                                break;
                        }
                        break;
                    }
                }
            }

            if (isOK == true)
            {
                try
                {
                    //mã hóa mật khẩu???????????
                    string _pass = StringCryptor.EncryptString(txtPass.Text);

                    clsQT.Q_USER_InsertUpdate(txtUsername.Text.Trim(), txtFullname.Text, _pass, txtDescript.Text);
                    MessageBox.Show("Cập nhật người dùng thành công!", "Quản trị người dùng", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi cập nhật người dùng: " + ex.Message, "Quản trị người dùng", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool checkInput()
        {
            if (string.IsNullOrEmpty(txtUsername.Text))
            {
                MessageBox.Show("Tên đăng nhập không được để trống!", "Quản trị người dùng", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsername.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtFullname.Text))
            {
                MessageBox.Show("Tên người dùng không được để trống!", "Quản trị người dùng", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtFullname.Focus();
                return false;
            }

            if (txtPass.Text != txtPassRe.Text)
            {
                MessageBox.Show("Mật khẩu nhập lại không khớp, hãy kiểm tra lại!", "Quản trị người dùng", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPass.Focus();
                return false;
            }

            return true;
        }
    }
}

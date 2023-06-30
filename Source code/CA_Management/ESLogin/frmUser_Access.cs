using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace ESLogin
{
    public partial class frmUser_Access : Form
    {
        public string ID_chuongtrinh;
        public SqlConnection gconn;
        public string ID_ACCess;
        public bool IsAdmin = false;
        public ClsLogin phanquyen;

        public frmUser_Access()
        {
            InitializeComponent();
        }

        private int check_login(string userId)
        {
            DataTable dtUser = new DataTable();
            if (gconn.State == ConnectionState.Closed)
                gconn.Open();

            SqlCommand cm = new SqlCommand();
            cm.CommandType = CommandType.Text;
            cm.CommandText = "SELECT name FROM   [master].[dbo].[syslogins] where isntname = 0 and name= N'" + userId + "'";
            cm.Connection = gconn;
            SqlDataAdapter adp = new SqlDataAdapter(cm);
            if (dtUser.Rows.Count > 0)
                dtUser.Clear();
            adp.Fill(dtUser);

            if (dtUser.Rows.Count > 0)
            {
                return 0;
            }

            return 1;
        }

        private bool dangnhap()
        {
            return phanquyen.dangnhap(phanquyen.ServerName, phanquyen.DBName, txtUser.Text.Trim(), txtNewPwd1.Text);
        }

        private void frmUser_Access_Load(object sender, EventArgs e)
        {
            try
            {
                if (gconn.State == ConnectionState.Closed)
                    gconn.Open();
                SqlCommand cm = new SqlCommand();
                DataTable ds = new DataTable();
                cm.CommandType = CommandType.StoredProcedure;
                cm.CommandText = "sp_PQ_select_Q_ACCESS";
                cm.Connection = gconn;

                SqlParameter p = new SqlParameter();
                p.ParameterName = "@ACCESSID";
                p.Direction = ParameterDirection.Input;
                p.SqlDbType = SqlDbType.VarChar;
                p.Value = ID_ACCess;
                cm.Parameters.Add(p);

                SqlDataAdapter adp = new SqlDataAdapter(cm);

                adp.Fill(ds);
                if (ds.Rows.Count > 0)
                {
                    txtUser.Text = ds.Rows[0]["USERNAME"].ToString();
                    txtNewPwd1.Text = StringCryptor.DecryptString(ds.Rows[0]["PASSWORD"].ToString());
                    txtNewPwd2.Text = StringCryptor.DecryptString(ds.Rows[0]["PASSWORD"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (IsAdmin)
            {
                btnSave.Enabled = true;
                txtUser.ReadOnly = false;
                txtNewPwd1.ReadOnly = false;
                txtNewPwd2.ReadOnly = false;
            }
            else
            {
                btnSave.Enabled = false;
                txtUser.ReadOnly = true;
                txtNewPwd1.ReadOnly = true;
                txtNewPwd2.ReadOnly = true;
            }
        }

        //Tạo tài khoảng SQL mặc định của chương trình
        private void btn_Login_default_Click(Object sender, EventArgs e)
        {
            int login = check_login(phanquyen.MvarUsername_default);

            try
            {
                if (gconn.State == ConnectionState.Closed)
                    gconn.Open();
                SqlCommand cm = new SqlCommand();
                cm.CommandType = CommandType.StoredProcedure;
                cm.CommandText = "sp_PQ_Login_public";
                cm.Connection = gconn;

                SqlParameter p = new SqlParameter();
                p.ParameterName = "@LOGINNAME";
                p.Direction = ParameterDirection.Input;
                p.SqlDbType = SqlDbType.VarChar;
                p.Value = phanquyen.MvarUsername_default;
                cm.Parameters.Add(p);

                p = new SqlParameter();
                p.ParameterName = "@PASSWORD";
                p.Direction = ParameterDirection.Input;
                p.SqlDbType = SqlDbType.VarChar;
                p.Value = phanquyen.MvarPassword_default;
                cm.Parameters.Add(p);

                p = new SqlParameter();
                p.ParameterName = "@Insert_User_sql";
                p.Direction = ParameterDirection.Input;
                p.SqlDbType = SqlDbType.Bit;
                p.Value = login == 0 ? 0 : 1;
                cm.Parameters.Add(p);

                cm.ExecuteNonQuery();

                MessageBox.Show("Cập nhật thành công!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                btn_Login_default.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Lưu thông tin đăng nhập SQL mặc định vào bảng Q_ACCESS
        private void  btnSave_Click(Object sender, EventArgs e)
        {
            if (txtUser.Text.Trim() == "")
            {
                MessageBox.Show("Bạn phải nhập Tên login vào", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (txtNewPwd1.Text != txtNewPwd2.Text)
            {
                MessageBox.Show("Hai mật khẩu không giống nhau. Bạn phải nhập lại!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            bool dnhap = dangnhap();
            int login = check_login(txtUser.Text.Trim());
            if (dnhap == false & login == 0)
            {
                MessageBox.Show("Login này đã tồn tại. Bạn phải nhập đúng passwords của nó!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                if (gconn.State == ConnectionState.Closed)
                    gconn.Open();
                SqlCommand cm = new SqlCommand();
                DataTable ds = new DataTable();
                cm.CommandType = CommandType.StoredProcedure;
                cm.CommandText = "sp_PQ_INSERT_Q_ACCESS";
                cm.Connection = gconn;

                SqlParameter p = new SqlParameter();
                p.ParameterName = "@ACCESSID";
                p.Direction = ParameterDirection.Input;
                p.SqlDbType = SqlDbType.VarChar;
                p.Value = ID_ACCess;
                cm.Parameters.Add(p);

                p = new SqlParameter();
                p.ParameterName = "@LOGINNAME";
                p.Direction = ParameterDirection.Input;
                p.SqlDbType = SqlDbType.VarChar;
                p.Value = txtUser.Text;
                cm.Parameters.Add(p);

                p = new SqlParameter();
                p.ParameterName = "@PASSWORD";
                p.Direction = ParameterDirection.Input;
                p.SqlDbType = SqlDbType.VarChar;
                p.Value = StringCryptor.EncryptString(txtNewPwd1.Text);
                cm.Parameters.Add(p);

                p = new SqlParameter();
                p.ParameterName = "@PASSWORD_SQL";
                p.Direction = ParameterDirection.Input;
                p.SqlDbType = SqlDbType.VarChar;
                p.Value = txtNewPwd1.Text;
                cm.Parameters.Add(p);

                p = new SqlParameter();
                p.ParameterName = "@Insert_User_sql";
                p.Direction = ParameterDirection.Input;
                p.SqlDbType = SqlDbType.Bit;
                p.Value = login == 0 ? 0 : 1;
                cm.Parameters.Add(p);

                cm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            MessageBox.Show("Cập nhật thành công!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }

        private void btnExit_Click(Object sender, EventArgs e)
        {
            this.Close();
        }
	
    }
}

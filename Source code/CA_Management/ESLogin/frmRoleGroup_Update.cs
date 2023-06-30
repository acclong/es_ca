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
    public partial class frmRoleGroup_Update : Form
    {
        private BUS_UserManagement clsQT = new BUS_UserManagement();

        private short __roleID;
        private string __roleName, __Descript;
        private bool isAdd;

        public frmRoleGroup_Update()
        {
            InitializeComponent();
            isAdd = true;
        }

        public frmRoleGroup_Update(short roleID, string roleName, string Descript)
        {
            InitializeComponent();
            isAdd = false;
            __roleID = roleID;
            __roleName = roleName;
            __Descript = Descript;
        }

        private void frmRoleGroup_Update_Load(object sender, EventArgs e)
        {
            if (isAdd == false)
            {
                txtName.Text = __roleName;
                txtDes.Text = __Descript;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            
        }

        private bool checkInput()
        {
            if (string.IsNullOrEmpty(txtName.Text) == true)
            {
                MessageBox.Show("Tên nhóm quyền không được để trống!", "ES - Monitoring", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtName.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtDes.Text) == true)
            {
                MessageBox.Show("Mô tả nhóm quyền không được để trống!", "ES - Monitoring", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtDes.Focus();
                return false;
            }

            //kiểm tra xem tên nhóm quyền này đã tồn tại hay chưa
            DataTable dt = clsQT.Q_ROLE_SelectByProgID(clsSharing.sProgramID);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["RoleName"].ToString().Trim() == txtName.Text.Trim())
                {
                    MessageBox.Show("Tên nhóm quyền đã tồn tại, hãy nhập tên khác!", "ES - Monitoring", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtName.Focus();
                    return false;
                }
            }

            return true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (checkInput() == false) return;

                clsQT.Q_ROLE_InsertUpdate(__roleID, txtName.Text, txtDes.Text, clsSharing.sProgramID);

                MessageBox.Show("Cập nhật nhóm quyền thành công!", "ES - Monitoring", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi trong quá trình ghi dữ liệu: " + ex.Message, "ES - Monitoring", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}

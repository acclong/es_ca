using ES.CA_ManagementBUS;
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
    public partial class frmThemSuaNguoiDung : Form
    {
        #region Var

        private int _iUserId;
        BUSQuanTri _bus = new BUSQuanTri();
        DataTable _dtUser = new DataTable();

        public int UserID
        {
            set { _iUserId = value; }
            get { return _iUserId; }
        }

        #endregion

        public frmThemSuaNguoiDung()
        {
            InitializeComponent();
        }

        private void frmThemSuaNguoiDung_Load(object sender, EventArgs e)
        {
            try
            {
                InitcboStatus();
                InitcboChungThuSo();

                if (UserID != -1)
                {
                    this.Text = "Sửa thông tin người dùng";

                    // lấy dữ liệu từ database
                    _dtUser = _bus.CA_User_SelectByUserID(UserID);

                    txtUserID.Text = _dtUser.Rows[0]["UserID"].ToString();
                    txtUserName.Text = _dtUser.Rows[0]["Name"].ToString();
                    txtCMND.Text = _dtUser.Rows[0]["CMND"].ToString();
                    txtUnitID.Text = _dtUser.Rows[0]["UnitID"].ToString();
                    txtUnitName.Text = _dtUser.Rows[0]["UnitName"].ToString();
                    txtDescription.Text = _dtUser.Rows[0]["Description"].ToString();
                    txtEmail.Text = _dtUser.Rows[0]["Email"].ToString();
                    cboStatus.SelectedIndex = Convert.ToInt32(_dtUser.Rows[0]["Status"]);
                    if (_dtUser.Rows[0]["CertID"].ToString() != "")
                        cboChungThuSo.SelectedValue = _dtUser.Rows[0]["CertID"];
                    else
                        cboChungThuSo.SelectedValue = -1;
                    if (_dtUser.Rows[0]["ValidFrom"] != DBNull.Value)
                        dpkValidFrom.Value = Convert.ToDateTime(_dtUser.Rows[0]["ValidFrom"].ToString());
                    else
                        dpkValidFrom.Value = DateTime.Now;
                    if (_dtUser.Rows[0]["ValidTo"] == DBNull.Value)
                        chkVaildTo.Checked = false;
                    else
                    {
                        dpkValidTo.Value = Convert.ToDateTime(_dtUser.Rows[0]["ValidTo"].ToString());
                        chkVaildTo.Checked = true;
                    }

                    this.ActiveControl = dpkValidFrom;
                    txtCMND.Enabled = false;
                    //txtUserName.Enabled = false;

                }
                else
                {
                    this.ActiveControl = txtUserName;
                    this.Text = "Thêm thông tin người dùng";
                }


            }
            catch (Exception ex)
            {
                clsShare.Message_Error(ex);
            }
        }

        #region Init

        private void InitcboChungThuSo()
        {
            //DataTable dtsnu = _bus.CA_Certificate_SelectBy_NotUse();
            DataTable dtsnu = _bus.CA_Certificate_SelectBy_NotUse(UserID);
            DataTable dtsui = null;

            DataRow dr = dtsnu.NewRow();
            dr["CertID"] = -1;
            dr["NameCN"] = "[Không có]";
            dtsnu.Rows.InsertAt(dr, 0);

            cboChungThuSo.DataSource = dtsnu;
            cboChungThuSo.DisplayMember = "NameCN";
            cboChungThuSo.ValueMember = "CertID";

            if (UserID != -1)
            {
                dtsui = _bus.CA_User_SelectByUserID(_iUserId);
                if (dtsui.Rows[0]["CertID"].ToString() != "")
                {
                    string certID = dtsui.Rows[0]["CertID"].ToString();
                    for (int i = 0; i < dtsnu.Rows.Count; i++)
                    {
                        if (dtsnu.Rows[i]["CertID"].ToString() == certID)
                        {
                            cboChungThuSo.SelectedIndex = i;
                            break;
                        }
                    }
                }
            }
            //cboChungThuSo.SelectedIndex = 0;
        }

        private void InitcboStatus()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Id", typeof(int));
            dt.Columns.Add("Name", typeof(string));

            DataRow dr = dt.NewRow();
            dr["Id"] = 0;
            dr["Name"] = "Không hiệu lực";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["Id"] = 1;
            dr["Name"] = "Hiệu lực";
            dt.Rows.Add(dr);

            cboStatus.DataSource = dt;
            cboStatus.DisplayMember = "Name";
            cboStatus.ValueMember = "Id";
            cboStatus.SelectedIndex = 1;
        }

        #endregion

        #region Controls

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // lấy giá trị biến tạm thời
                string Name = txtUserName.Text.Trim();
                string CMND = txtCMND.Text.Trim();
                string email = txtEmail.Text.Trim();
                int unitID = Convert.ToInt32(txtUnitID.Text);
                int certID = Convert.ToInt32(cboChungThuSo.SelectedValue);

                string Description = txtDescription.Text;
                int Status = Convert.ToInt32(cboStatus.SelectedValue);
                DateTime ValidFrom = dpkValidFrom.Value;
                DateTime ValidTo = chkVaildTo.Checked ? dpkValidTo.Value : DateTime.MaxValue;
                if (Status == 0)
                {
                    ValidFrom = DateTime.MaxValue;
                    ValidTo = DateTime.MaxValue;
                }
                string UserModified = clsShare.sUserName;

                // Kiểm tra dữ liệu đầu vào
                if (Name == "" || ValidFrom == null || Status == -1 || CMND == "" || unitID == -1)
                {
                    clsShare.Message_Error("Các trường có dấu * không được phép để trống!");
                    if (UserID != -1) this.ActiveControl = dpkValidFrom;
                    else this.ActiveControl = txtUserName;
                    return;
                }
                if (chkVaildTo.Checked)
                    if (ValidTo != null)
                        if (DateTime.Compare(ValidTo, ValidFrom) == -1)
                        {
                            clsShare.Message_Error("Ngày hết hiệu lực không được nhỏ hơn Ngày có hiệu lực!");
                            if (UserID != -1) this.ActiveControl = dpkValidFrom;
                            else this.ActiveControl = txtUserName;
                            return;
                        }

                // Xác nhận thông tin thu hồi hiệu lực user
                if (Status == 0)
                    if (clsShare.Message_WarningYN("Bạn có chắc chắn lưu thông tin KHÔNG HIỆU LỰC cho User này không?") == false)
                    {
                        if (UserID != -1) this.ActiveControl = dpkValidFrom;
                        else this.ActiveControl = txtUserName;
                        return;
                    }

                // Kiểm tra tên
                //Edited by Toantk on 23/4/2015
                //Chuyển hàm kiểm tra vào Business
                int userID = _bus.CA_User_HasCMND(CMND);
                if (UserID != -1 && userID != 0)
                {
                    if (UserID != userID)
                    {
                        clsShare.Message_Error("Số chứng minh nhân dân đã tồn tại. Vui lòng nhập lại Số chứng minh nhân dân!");
                        if (UserID != -1) this.ActiveControl = dpkValidFrom;
                        else this.ActiveControl = txtUserName;
                        return;
                    }
                }
                else if (UserID == -1 && userID != 0)
                {
                    clsShare.Message_Error("Số chứng minh nhân dân đã tồn tại. Vui lòng nhập lại Số chứng minh nhân dân!");
                    if (UserID != -1) this.ActiveControl = dpkValidFrom;
                    else this.ActiveControl = txtUserName;
                    return;
                }

                // cập nhật vào cơ sở dữ liệu
                _bus.CA_User_InsertUpdate(UserID, Name, CMND, Status, ValidFrom, ValidTo, email, UserModified, unitID, Description, certID);

                clsShare.Message_Info("Cập nhật người dùng thành công!");

                // Định dạng lại các controls
                if (UserID == -1)
                {
                    this.ActiveControl = txtUserName;

                    txtUserName.Clear();
                    txtCMND.Clear();
                    txtUnitName.Clear();
                    cboChungThuSo.SelectedIndex = 0;
                    cboStatus.SelectedIndex = 0;
                    txtDescription.Clear();
                    chkVaildTo.Checked = false;

                    dpkValidFrom.Value = DateTime.Now;
                    dpkValidTo.Value = DateTime.Now;
                    dpkValidTo.Visible = false;
                }
                else
                {
                    this.ActiveControl = dpkValidFrom;
                }
            }
            catch (Exception ex)
            {
                clsShare.Message_Error(ex);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSeach_Click(object sender, EventArgs e)
        {
            frmLocDonViCA frm = new frmLocDonViCA(this);
            frm.ShowDialog();
        }

        private void chkVaildTo_CheckedChanged(object sender, EventArgs e)
        {
            dpkValidTo.Visible = chkVaildTo.Checked;
        }

        private void cboStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboStatus.Items.Count > 0)
                if (cboStatus.SelectedIndex == 0)
                {
                    dpkValidFrom.Enabled = false;
                    dpkValidTo.Enabled = false;
                    chkVaildTo.Enabled = false;
                }
                else
                {
                    dpkValidFrom.Enabled = true;
                    dpkValidTo.Enabled = true;
                    chkVaildTo.Enabled = true;
                }
        }
        #endregion

        #region Event
        public void CapNhatDuLieu(string unitID, string unitNotation)
        {
            txtUnitName.Text = unitNotation;
            txtUnitID.Text = unitID;
        }

        private void frmThemSuaNguoiDung_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                btnExit_Click(null, null);
            }
        }
        #endregion

    }
}

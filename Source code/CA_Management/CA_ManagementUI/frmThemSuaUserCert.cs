using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ES.CA_ManagementBUS;
using Telerik.WinControls.Data;
using Telerik.WinControls.UI;
using Telerik.WinControls.Layouts;
using Telerik.WinControls;

namespace ES.CA_ManagementUI
{
    public partial class frmThemSuaUserCert : Form
    {
        BUSQuanTri _bus = new BUSQuanTri();
        DataTable _dtUser = new DataTable();
        DataTable _dtCert = new DataTable();

        int _id_UserCert = -1;
        int _userID = -1;
        int _CertID = -1;

        public int ID_UserCert
        {
            set { _id_UserCert = value; }
            get { return _id_UserCert; }
        }

        public frmThemSuaUserCert()
        {
            InitializeComponent();
        }

        private void frmThemSuaUserDonVi_Load(object sender, EventArgs e)
        {
            try
            {
                //Khởi tạo ListViews
                rlvUser.AllowArbitraryItemHeight = true;
                rlvUser.AllowArbitraryItemWidth = true;
                rlvUser.VisualItemCreating += rlvUser_VisualItemCreating;
                rlvUser.VisualItemFormatting += rlvUser_VisualItemFormatting;
                //
                rlvCert.AllowArbitraryItemHeight = true;
                rlvCert.AllowArbitraryItemWidth = true;
                rlvCert.VisualItemCreating += rlvCert_VisualItemCreating;
                rlvCert.VisualItemFormatting += rlvCert_VisualItemFormatting;

                //Khởi tạo dropdownlist
                InitdrpUserGroup();
                InitdrpCertGroup();
                InitCboCertType();
                cboUserName.Enabled = false;
                cboCert.Enabled = false;

                //Lấy dữ liệu và đổ vào control
                LoadData();

                //Thêm sự kiện cho các controls
                drpUserGroup.SelectedIndexChanged += drpUserGroup_SelectedIndexChanged;
                drpCertGroup.SelectedIndexChanged += drpCertGroup_SelectedIndexChanged;
                tbUserFilter.TextChanged += tbUserFilter_TextChanged;
                txtCertFilter.TextChanged += txtCertFilter_TextChanged;
                rlvUser.SelectedIndexChanged += rlvUser_SelectedIndexChanged;
                rlvCert.SelectedIndexChanged += rlvCert_SelectedIndexChanged;

                //Lấy dữ liệu nếu sửa
                if (ID_UserCert != -1)
                {
                    // lấy dữ liệu từ database
                    DataTable dt = _bus.CA_CertificateUser_SelectByID_CertUser(ID_UserCert);

                    //Đổ lên controls
                    txtID_UserCert.Text = ID_UserCert.ToString();
                    _userID = Convert.ToInt32(dt.Rows[0]["UserID"]);
                    _CertID = Convert.ToInt32(dt.Rows[0]["CertID"]);
                    cboCertType.SelectedIndex = Convert.ToInt32(dt.Rows[0]["Type"]);
                    dpkValidFrom.Value = Convert.ToDateTime(dt.Rows[0]["ValidFrom"].ToString());
                    if (dt.Rows[0]["ValidTo"] == DBNull.Value)
                        chkVaildTo.Checked = false;
                    else
                    {
                        dpkValidTo.Value = Convert.ToDateTime(dt.Rows[0]["ValidTo"].ToString());
                        chkVaildTo.Checked = true;
                    }

                    //Thiết lập lựa chọn trên ListView
                    foreach (ListViewDataItem item in rlvUser.Items)
                        if ((int)item.Value == _userID)
                        {
                            rlvUser.SelectedItems.Clear();
                            rlvUser.SelectedItem = item;
                            break;
                        }

                    foreach (ListViewDataItem item in rlvCert.Items)
                        if ((int)item.Value == _CertID)
                        {
                            rlvCert.SelectedItems.Clear();
                            rlvCert.SelectedItem = item;
                            break;
                        }

                    // không cho thay đổi ngày bắt đầu
                    dpkValidFrom.Enabled = false;

                    // khóa sự kiện chọn thay đổi
                    rlvUser.SelectedIndexChanged -= rlvUser_SelectedIndexChanged;
                    rlvCert.SelectedIndexChanged -= rlvCert_SelectedIndexChanged;
                }
                else
                {
                    //Thiết lập lựa chọn User mặc định
                    rlvUser.SelectedIndex = 0;
                    _userID = (int)rlvUser.SelectedItem.Value;
                    cboUserName.SelectedValue = _userID;

                    //Thiết lập lựa chọn Cert mặc định
                    rlvCert.SelectedIndex = 0;
                    _CertID = (int)rlvCert.SelectedItem.Value;
                    cboCert.SelectedValue = _CertID;
                }
            }
            catch (Exception ex)
            {
                clsShare.Message_Error(ex);
            }
        }

        private void InitCboCertType()
        {
            string[] array = { "Cá nhân", "Cá nhân trong doanh nghiệp", "Doanh nghiệp" };
            cboCertType.Items.AddRange(array);
            cboCertType.SelectedIndex = 0;
        }

        private void InitdrpUserGroup()
        {
            string[] array = { "[Không]", "Trạng thái", "Ngày hiệu lực", "Ngày hết hiệu lực" };
            drpUserGroup.Items.AddRange(array);
            drpUserGroup.SelectedIndex = 0;
        }

        private void InitdrpCertGroup()
        {
            string[] array = { "[Không]", "Trạng thái", "Ngày hiệu lực", "Ngày hết hiệu lực" };
            drpCertGroup.Items.AddRange(array);
            drpCertGroup.SelectedIndex = 0;
        }

        private void chkVaildTo_CheckedChanged(object sender, EventArgs e)
        {
            dpkValidTo.Visible = chkVaildTo.Checked;
        }

        private void drpUserGroup_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            try
            {
                this.rlvUser.GroupDescriptors.Clear();
                switch (drpUserGroup.Text)
                {
                    case "[Không]":
                        rlvUser.EnableGrouping = false;
                        rlvUser.ShowGroups = false;
                        break;
                    case "Trạng thái":
                        rlvUser.GroupDescriptors.Add(new GroupDescriptor(
                            new SortDescriptor[] { new SortDescriptor("StatusName", ListSortDirection.Ascending) }));
                        rlvUser.EnableGrouping = true;
                        rlvUser.ShowGroups = true;
                        break;
                    case "Ngày hiệu lực":
                        rlvUser.GroupDescriptors.Add(new GroupDescriptor(
                            new SortDescriptor[] { new SortDescriptor("ValidFromDate", ListSortDirection.Ascending) }));
                        rlvUser.EnableGrouping = true;
                        rlvUser.ShowGroups = true;
                        break;
                    case "Ngày hết hiệu lực":
                        rlvUser.GroupDescriptors.Add(new GroupDescriptor(
                            new SortDescriptor[] { new SortDescriptor("ValidToDate", ListSortDirection.Ascending) }));
                        rlvUser.EnableGrouping = true;
                        rlvUser.ShowGroups = true;
                        break;
                }
            }
            catch (Exception ex)
            {
                clsShare.Message_Error(ex);
            }
        }

        private void drpCertGroup_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            try
            {
                this.rlvCert.GroupDescriptors.Clear();
                switch (drpCertGroup.Text)
                {
                    case "[Không]":
                        rlvCert.EnableGrouping = false;
                        rlvCert.ShowGroups = false;
                        break;
                    case "Trạng thái":
                        rlvCert.GroupDescriptors.Add(new GroupDescriptor(
                            new SortDescriptor[] { new SortDescriptor("StatusName", ListSortDirection.Ascending) }));
                        rlvCert.EnableGrouping = true;
                        rlvCert.ShowGroups = true;
                        break;
                    case "Ngày hiệu lực":
                        rlvCert.GroupDescriptors.Add(new GroupDescriptor(
                            new SortDescriptor[] { new SortDescriptor("ValidFromDate", ListSortDirection.Ascending) }));
                        rlvCert.EnableGrouping = true;
                        rlvCert.ShowGroups = true;
                        break;
                    case "Ngày hết hiệu lực":
                        rlvCert.GroupDescriptors.Add(new GroupDescriptor(
                            new SortDescriptor[] { new SortDescriptor("ValidToDate", ListSortDirection.Ascending) }));
                        rlvCert.EnableGrouping = true;
                        rlvCert.ShowGroups = true;
                        break;
                }
            }
            catch (Exception ex)
            {
                clsShare.Message_Error(ex);
            }
        }

        private void tbUserFilter_TextChanged(object sender, EventArgs e)
        {
            try
            {
                rlvUser.FilterDescriptors.Clear();

                if (String.IsNullOrEmpty(this.tbUserFilter.Text))
                {
                    rlvUser.EnableFiltering = false;
                }
                else
                {
                    rlvUser.FilterDescriptors.LogicalOperator = FilterLogicalOperator.Or;
                    rlvUser.FilterDescriptors.Add("Name", FilterOperator.Contains, this.tbUserFilter.Text);
                    rlvUser.FilterDescriptors.Add("CMND", FilterOperator.Contains, this.tbUserFilter.Text);
                    rlvUser.EnableFiltering = true;
                }
            }
            catch (Exception ex)
            {
                clsShare.Message_Error(ex);
            }
        }

        private void txtCertFilter_TextChanged(object sender, EventArgs e)
        {
            try
            {
                rlvCert.FilterDescriptors.Clear();

                if (String.IsNullOrEmpty(this.txtCertFilter.Text))
                {
                    rlvCert.EnableFiltering = false;
                }
                else
                {
                    rlvCert.FilterDescriptors.LogicalOperator = FilterLogicalOperator.Or;
                    rlvCert.FilterDescriptors.Add("MaDV", FilterOperator.Contains, this.txtCertFilter.Text);
                    rlvCert.FilterDescriptors.Add("Name", FilterOperator.Contains, this.txtCertFilter.Text);
                    rlvCert.FilterDescriptors.Add("Notation", FilterOperator.Contains, this.txtCertFilter.Text);
                    rlvCert.EnableFiltering = true;
                }
            }
            catch (Exception ex)
            {
                clsShare.Message_Error(ex);
            }
        }

        private void rlvUser_VisualItemCreating(object sender, ListViewVisualItemCreatingEventArgs e)
        {
            if (!(e.VisualItem is BaseListViewGroupVisualItem))
            {
                e.VisualItem = new clsShare.UserListVisualItem();
            }
        }

        private void rlvCert_VisualItemCreating(object sender, ListViewVisualItemCreatingEventArgs e)
        {
            if (!(e.VisualItem is BaseListViewGroupVisualItem))
            {
                e.VisualItem = new clsShare.CertListVisualItem();
            }
        }

        private void rlvUser_VisualItemFormatting(object sender, ListViewVisualItemEventArgs e)
        {
            try
            {
                //Lấy Group Header
                SimpleListViewGroupVisualItem groupItem = e.VisualItem as SimpleListViewGroupVisualItem;
                if (groupItem != null)
                {
                    //Nếu ko nhóm thì return
                    if (rlvUser.GroupDescriptors.Count == 0)
                        return;
                    //Định dạng ngày
                    DateTime date = new DateTime();
                    if (DateTime.TryParse(groupItem.Text, out date))
                        groupItem.Text = date.ToString("dd/MM/yyyy");
                }
            }
            catch (Exception ex)
            {
                clsShare.Message_Error(ex);
            }
        }

        private void rlvCert_VisualItemFormatting(object sender, ListViewVisualItemEventArgs e)
        {
            try
            {
                //Lấy Group Header
                SimpleListViewGroupVisualItem item = e.VisualItem as SimpleListViewGroupVisualItem;
                if (item != null)
                {
                    //Nếu ko nhóm thì return
                    if (rlvCert.GroupDescriptors.Count == 0)
                        return;
                    //Định dạng ngày
                    DateTime date = new DateTime();
                    if (DateTime.TryParse(item.Text, out date))
                        item.Text = date.ToString("dd/MM/yyyy");
                }
            }
            catch (Exception ex)
            {
                clsShare.Message_Error(ex);
            }
        }

        void rlvUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                _userID = (int)rlvUser.SelectedItem.Value;
                cboUserName.SelectedValue = _userID;
            }
            catch (Exception ex)
            {
                clsShare.Message_Error(ex);
            }
        }

        void rlvCert_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                _CertID = (int)rlvCert.SelectedItem.Value;
                cboCert.SelectedValue = _CertID;
            }
            catch (Exception ex)
            {
                clsShare.Message_Error(ex);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // lấy giá trị biến tạm thời, kiểm tra dữ liệu đầu vào
                if (_userID == -1 || _CertID == -1)
                {
                    clsShare.Message_Error("Các trường có dấu * không được phép để trống!");
                    return;
                }

                int iCertType = cboCertType.SelectedIndex;
                DateTime validFrom = dpkValidFrom.Value;
                DateTime validTo = chkVaildTo.Checked ? dpkValidTo.Value : DateTime.MaxValue;

                if (chkVaildTo.Checked && dpkValidFrom.Value != null)
                    if (DateTime.Compare(validTo, validFrom) == -1)
                    {
                        clsShare.Message_Error("Ngày hết hiệu lực không được nhỏ hơn Ngày có hiệu lực!");
                        return;
                    }

                // Kiểm tra người dùng đã liên kết với chứng thư số hay chưa
                DataTable dt = _bus.CA_CertificateUser_SelectBy_CertID_UserID(_userID, _CertID);
                if (dt.Rows.Count > 0)
                {
                    int idUserProgtemp = Convert.ToInt32(dt.Rows[0]["ID_CertUser"]);
                    DateTime expirateDate = dt.Rows[0]["ValidTo"] == DBNull.Value ? DateTime.MaxValue : Convert.ToDateTime(dt.Rows[0]["ValidTo"]);
                    if (ID_UserCert != -1)
                    {
                        // Trường hợp không sửa khi hết hạn
                        if (DateTime.Compare(DateTime.Now, expirateDate) == 1)
                        {
                            clsShare.Message_Error("Liên kết đã hết hạn. Vui lòng thêm mới liên kết người dùng - chứng thư số!");
                            return;
                        }
                    }
                    else
                    {
                        // Người dùng vẫn còn thời hạn
                        if (DateTime.Compare(DateTime.Now, expirateDate) != 1)
                        {
                            clsShare.Message_Error("Người dùng [" + rlvUser.SelectedItem.Text + "] đã được đăng ký trong chứng thư số ["
                            + rlvCert.SelectedItem.Text + "]!");
                            return;
                        }
                        // Ngày bắt đầu lớn hơn ngày kết thúc trước
                        else if (DateTime.Compare(expirateDate, validFrom) == 1)
                        {
                            clsShare.Message_Error("Thời gian hiệu lực vi phạm ràng buộc. Vui lòng sửa lại ngày hiệu lực!");
                            return;
                        }
                    }
                }

                // lưu vào update DB
                _bus.CA_CertificateUser_InsertUpdate(ID_UserCert, _userID, _CertID, validFrom, validTo, iCertType, clsShare.sUserName);
                clsShare.Message_Info("Cập nhật liên kết người dùng - chứng thư số thành công!");
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

        private void LoadData()
        {
            //Bảng danh sách người dùng
            _dtUser = _bus.CA_User_SelectAllWithDate();
            //Đổ vào ListView
            rlvUser.DataSource = _bus.AddDateCol(_dtUser);
            rlvUser.DisplayMember = "Name";
            rlvUser.ValueMember = "UserID";
            //Đổ vào cbo
            cboUserName.DataSource = _dtUser;
            cboUserName.DisplayMember = "Name";
            cboUserName.ValueMember = "UserID";

            //Bảng danh sách chứng thư số
            _dtCert = _bus.CA_Certificate_SelectAll();
            //Đổ vào ListView
            rlvCert.DataSource = _bus.AddDateCol(_dtCert);
            rlvCert.DisplayMember = "NameCN";
            rlvCert.ValueMember = "CertID";
            //Đổ vào cbo
            cboCert.DataSource = _dtCert;
            cboCert.DisplayMember = "NameCN";
            cboCert.ValueMember = "CertID";
        }
    }
}
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
    public partial class frmThemSuaUserDonVi : Form
    {
        BUSQuanTri _bus = new BUSQuanTri();
        DataTable _dtUser = new DataTable();
        DataTable _dtUnit = new DataTable();

        int _id_UserUnit = -1;
        int _userID = -1;
        int _unitID = -1;

        public int ID_UserUnit
        {
            set { _id_UserUnit = value; }
            get { return _id_UserUnit; }
        }

        public frmThemSuaUserDonVi()
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
                rlvUnit.AllowArbitraryItemHeight = true;
                rlvUnit.AllowArbitraryItemWidth = true;
                rlvUnit.VisualItemCreating += rlvUnit_VisualItemCreating;
                rlvUnit.VisualItemFormatting += rlvUnit_VisualItemFormatting;

                //Khởi tạo dropdownlist
                InitdrpUserGroup();
                InitdrpUnitGroup();
                cboUserName.Enabled = false;
                cboUnit.Enabled = false;

                //Lấy dữ liệu và đổ vào control
                LoadData();

                //Thêm sự kiện cho các controls
                drpUserGroup.SelectedIndexChanged += drpUserGroup_SelectedIndexChanged;
                drpUnitGroup.SelectedIndexChanged += drpUnitGroup_SelectedIndexChanged;
                tbUserFilter.TextChanged += tbUserFilter_TextChanged;
                tbUnitFilter.TextChanged += tbUnitFilter_TextChanged;
                rlvUser.SelectedIndexChanged += rlvUser_SelectedIndexChanged;
                rlvUnit.SelectedIndexChanged += rlvUnit_SelectedIndexChanged;

                //Lấy dữ liệu nếu sửa
                if (ID_UserUnit != -1)
                {
                    // lấy dữ liệu từ database
                    DataTable dt = _bus.CA_UserUnit_SelectByID(ID_UserUnit);

                    //Đổ lên controls
                    txtID_UserUnit.Text = ID_UserUnit.ToString();
                    _userID = Convert.ToInt32(dt.Rows[0]["UserID"]);
                    _unitID = Convert.ToInt32(dt.Rows[0]["UnitID"]);
                    txtDepartment.Text = dt.Rows[0]["Department"].ToString();
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

                    foreach (ListViewDataItem item in rlvUnit.Items)
                        if ((int)item.Value == _unitID)
                        {
                            rlvUnit.SelectedItems.Clear();
                            rlvUnit.SelectedItem = item;
                            break;
                        }

                    // không cho thay đổi ngày bắt đầu
                    dpkValidFrom.Enabled = false;

                    // khóa sự kiện chọn thay đổi
                    rlvUser.SelectedIndexChanged -= rlvUser_SelectedIndexChanged;
                    rlvUnit.SelectedIndexChanged -= rlvUnit_SelectedIndexChanged;
                }
                else
                {
                    //Thiết lập lựa chọn User mặc định
                    rlvUser.SelectedIndex = 0;
                    _userID = (int)rlvUser.SelectedItem.Value;
                    cboUserName.SelectedValue = _userID;

                    //Thiết lập lựa chọn Unit mặc định
                    rlvUnit.SelectedIndex = 0;
                    _unitID = (int)rlvUnit.SelectedItem.Value;
                    cboUnit.SelectedValue = _unitID;
                }
            }
            catch (Exception ex)
            {
                clsShare.Message_Error(ex);
            }
        }

        private void InitdrpUserGroup()
        {
            string[] array = { "[Không]", "Trạng thái", "Ngày hiệu lực", "Ngày hết hiệu lực" };
            drpUserGroup.Items.AddRange(array);
            drpUserGroup.SelectedIndex = 0;
        }

        private void InitdrpUnitGroup()
        {
            string[] array = { "[Không]", "Trạng thái", "Loại đơn vị", "Ngày hiệu lực", "Ngày hết hiệu lực" };
            drpUnitGroup.Items.AddRange(array);
            drpUnitGroup.SelectedIndex = 0;
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

        private void drpUnitGroup_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            try
            {
                this.rlvUnit.GroupDescriptors.Clear();
                switch (drpUnitGroup.Text)
                {
                    case "[Không]":
                        rlvUnit.EnableGrouping = false;
                        rlvUnit.ShowGroups = false;
                        break;
                    case "Trạng thái":
                        rlvUnit.GroupDescriptors.Add(new GroupDescriptor(
                            new SortDescriptor[] { new SortDescriptor("StatusName", ListSortDirection.Ascending) }));
                        rlvUnit.EnableGrouping = true;
                        rlvUnit.ShowGroups = true;
                        break;
                    case "Loại đơn vị":
                        rlvUnit.GroupDescriptors.Add(new GroupDescriptor(
                            new SortDescriptor[] { new SortDescriptor("UnitType", ListSortDirection.Ascending) }));
                        rlvUnit.EnableGrouping = true;
                        rlvUnit.ShowGroups = true;
                        break;
                    case "Ngày hiệu lực":
                        rlvUnit.GroupDescriptors.Add(new GroupDescriptor(
                            new SortDescriptor[] { new SortDescriptor("ValidFromDate", ListSortDirection.Ascending) }));
                        rlvUnit.EnableGrouping = true;
                        rlvUnit.ShowGroups = true;
                        break;
                    case "Ngày hết hiệu lực":
                        rlvUnit.GroupDescriptors.Add(new GroupDescriptor(
                            new SortDescriptor[] { new SortDescriptor("ValidToDate", ListSortDirection.Ascending) }));
                        rlvUnit.EnableGrouping = true;
                        rlvUnit.ShowGroups = true;
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

        private void tbUnitFilter_TextChanged(object sender, EventArgs e)
        {
            try
            {
                rlvUnit.FilterDescriptors.Clear();

                if (String.IsNullOrEmpty(this.tbUnitFilter.Text))
                {
                    rlvUnit.EnableFiltering = false;
                }
                else
                {
                    rlvUnit.FilterDescriptors.LogicalOperator = FilterLogicalOperator.Or;
                    rlvUnit.FilterDescriptors.Add("MaDV", FilterOperator.Contains, this.tbUnitFilter.Text);
                    rlvUnit.FilterDescriptors.Add("Name", FilterOperator.Contains, this.tbUnitFilter.Text);
                    rlvUnit.FilterDescriptors.Add("Notation", FilterOperator.Contains, this.tbUnitFilter.Text);
                    rlvUnit.EnableFiltering = true;
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

        private void rlvUnit_VisualItemCreating(object sender, ListViewVisualItemCreatingEventArgs e)
        {
            if (!(e.VisualItem is BaseListViewGroupVisualItem))
            {
                e.VisualItem = new clsShare.UnitListVisualItem();
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

        private void rlvUnit_VisualItemFormatting(object sender, ListViewVisualItemEventArgs e)
        {
            try
            {
                //Lấy Group Header
                SimpleListViewGroupVisualItem item = e.VisualItem as SimpleListViewGroupVisualItem;
                if (item != null)
                {
                    //Nếu ko nhóm thì return
                    if (rlvUnit.GroupDescriptors.Count == 0)
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

        void rlvUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                _unitID = (int)rlvUnit.SelectedItem.Value;
                cboUnit.SelectedValue = _unitID;
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
                if (_userID == -1 || _unitID == -1)
                {
                    clsShare.Message_Error("Các trường có dấu * không được phép để trống!");
                    return;
                }

                string department = txtDepartment.Text.Trim();
                DateTime validFrom = dpkValidFrom.Value;
                DateTime validTo = chkVaildTo.Checked ? dpkValidTo.Value : DateTime.MaxValue;

                if (chkVaildTo.Checked && dpkValidFrom.Value != null)
                    if (DateTime.Compare(validTo, validFrom) == -1)
                    {
                        clsShare.Message_Error("Ngày hết hiệu lực không được nhỏ hơn Ngày có hiệu lực!");
                        return;
                    }

                // Kiểm tra người dùng đã liên kết với đơn vị hay chưa
                DataTable dt = _bus.CA_UserUnit_SelectByUserID_UnitID(_userID, _unitID);
                if (dt.Rows.Count > 0)
                {
                    int idUserProgtemp = Convert.ToInt32(dt.Rows[0]["ID_UserUnit"]);
                    DateTime expirateDate = dt.Rows[0]["ValidTo"] == DBNull.Value ? DateTime.MaxValue : Convert.ToDateTime(dt.Rows[0]["ValidTo"]);
                    if (ID_UserUnit != -1)
                    {
                        // Trường hợp không sửa khi hết hạn
                        if (DateTime.Compare(DateTime.Now, expirateDate) == 1)
                        {
                            clsShare.Message_Error("Liên kết đã hết hạn. Vui lòng thêm mới liên kết người dùng - đơn vị!");
                            return;
                        }
                    }
                    else
                    {
                        // Người dùng vẫn còn thời hạn
                        if (DateTime.Compare(DateTime.Now, expirateDate) != 1)
                        {
                            clsShare.Message_Error("Người dùng [" + rlvUser.SelectedItem.Text + "] đã được đăng ký trong đơn vị ["
                            + rlvUnit.SelectedItem.Text + "]!");
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
                _bus.CA_UserUnit_InsertUpdate(ID_UserUnit, _userID, _unitID, department, validFrom, validTo, clsShare.sUserName);
                clsShare.Message_Info("Cập nhật liên kết người dùng - đơn vị thành công!");
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

            //Bảng danh sách đơn vị
            _dtUnit = _bus.CA_Unit_SelectAll();
            //Đổ vào ListView
            rlvUnit.DataSource = _bus.AddDateCol(_dtUnit);
            rlvUnit.DisplayMember = "Name";
            rlvUnit.ValueMember = "UnitID";
            //Đổ vào cbo
            cboUnit.DataSource = _dtUnit;
            cboUnit.DisplayMember = "Name";
            cboUnit.ValueMember = "UnitID";
        }
    }
}

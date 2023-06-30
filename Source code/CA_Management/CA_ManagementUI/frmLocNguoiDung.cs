using ES.CA_ManagementBUS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls.Data;
using Telerik.WinControls.UI;

namespace ES.CA_ManagementUI
{
    public partial class frmLocNguoiDung : Form
    {
        BUSQuanTri _bus = new BUSQuanTri();
        DataTable _dtUser = new DataTable();
        string _userName = "";
        int _userID = -1;

        public int UserID
        {
            get { return _userID; }
            set { _userID = value; }
        }

        public string UserName
        {
            get { return _userName; }
            set { _userName = value; }
        }

        int _id_UserProg = -1;
        private frmThemSuaUserHeThong _frmOut;
        private frmThemSuaUyQuyen _frmOutUyQuyen;
        public int ID_UserProg
        {
            set { _id_UserProg = value; }
            get { return _id_UserProg; }
        }
        public bool bFrmUyQuyen;
        public bool bUyQuyen;
        public bool bNhanUyQuyen;

        public frmLocNguoiDung()
        {
            InitializeComponent();
        }

        public frmLocNguoiDung(frmThemSuaUserHeThong frmThemSuaUserHeThong)
        {
            InitializeComponent();
            // TODO: Complete member initialization
            this._frmOut = frmThemSuaUserHeThong;
        }

        public frmLocNguoiDung(frmThemSuaUyQuyen frmThemSuaUyQuyen)
        {
            InitializeComponent();
            // TODO: Complete member initialization
            this._frmOutUyQuyen = frmThemSuaUyQuyen;
        }

        private void InitdrpUserGroup()
        {
            string[] array = { "[Không]", "Trạng thái", "Ngày hiệu lực", "Ngày hết hiệu lực" };
            drpUserGroup.Items.AddRange(array);
            drpUserGroup.SelectedIndex = 0;
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
                    default:
                        rlvUser.EnableGrouping = false;
                        rlvUser.ShowGroups = false;
                        break;
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

        void rlvUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //_userID = (int)rlvUser.SelectedItem.Value;
                //cboUserName.SelectedValue = _userID;
            }
            catch (Exception ex)
            {
                clsShare.Message_Error(ex);
            }
        }

        private void LoadData()
        {
            //Bảng danh sách người dùng
            _dtUser = _bus.CA_User_SelectAllWithDate();
            //Đổ vào ListView
            rlvUser.DataSource = _bus.AddDateCol(_dtUser);

            rlvUser.DisplayMember = "Name";
            rlvUser.ValueMember = "UserID";
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

        private void frmLocNguoiDung_Load(object sender, EventArgs e)
        {
            try
            {
                //Khởi tạo ListViews
                rlvUser.AllowArbitraryItemHeight = true;
                rlvUser.AllowArbitraryItemWidth = true;
                rlvUser.VisualItemCreating += rlvUser_VisualItemCreating;
                rlvUser.VisualItemFormatting += rlvUser_VisualItemFormatting;


                //Khởi tạo dropdownlist
                InitdrpUserGroup();


                //Lấy dữ liệu và đổ vào control
                LoadData();

                //Thêm sự kiện cho các controls
                drpUserGroup.SelectedIndexChanged += drpUserGroup_SelectedIndexChanged;
                tbUserFilter.TextChanged += tbUserFilter_TextChanged;
                rlvUser.SelectedIndexChanged += rlvUser_SelectedIndexChanged;

                {
                    //Thiết lập lựa chọn User mặc định
                    rlvUser.SelectedIndex = 0;
                    _userID = (int)rlvUser.SelectedItem.Value;

                }
            }
            catch (Exception ex)
            {
                clsShare.Message_Error(ex);
            }
        }

        private void rlvUser_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                _userID = Convert.ToInt32(rlvUser.SelectedItem.Value);
                _userName = rlvUser.SelectedItem.Text;
                if (bFrmUyQuyen)
                {
                    if (bUyQuyen)
                    {
                        _frmOutUyQuyen.CapNhatDuLieuUserUyQuyen(rlvUser.SelectedItem.Text, Convert.ToInt32(rlvUser.SelectedItem.Value));
                    }
                    else
                        _frmOutUyQuyen.CapNhatDuLieuUserNhanUyQuyen(rlvUser.SelectedItem.Text, Convert.ToInt32(rlvUser.SelectedItem.Value));
                }
                else
                    _frmOut.CapNhatDuLieuUser(rlvUser.SelectedItem.Text, Convert.ToInt32(rlvUser.SelectedItem.Value));
            }
            catch (Exception ex)
            { }
            this.Close();
        }

        private void commandBarButton1_Click(object sender, EventArgs e)
        {
            try
            {
                frmThemSuaNguoiDung frm = new frmThemSuaNguoiDung();
                frm.UserID = -1;
                frm.ShowDialog();

                // load lại dữ liệu
                LoadData();
            }
            catch (Exception ex)
            {
                clsShare.Message_Error(ex);
            }
        }

        private void tbUserFilter_Click(object sender, EventArgs e)
        {

        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            try
            {
                frmThemSuaNguoiDung frm = new frmThemSuaNguoiDung();
                frm.UserID = -1;
                frm.ShowDialog();

                // load lại dữ liệu
                LoadData();
            }
            catch (Exception ex)
            {
                clsShare.Message_Error(ex);
            }
        }


    }
}

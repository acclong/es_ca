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
    public partial class frmLocHeThongTichHop : Form
    {
        BUSQuanTri _bus = new BUSQuanTri();
        DataTable _dtProg = new DataTable();

        int _id_UserProg = -1;
        int _progID = -1;
        string _userProgName = "";
        private frmThemSuaUserHeThong _frmOut;
        private frmThemSuaUnitHeThong _frmUnitHeThong;

        public int ID_UserProg
        {
            set { _id_UserProg = value; }
            get { return _id_UserProg; }
        }

        public frmLocHeThongTichHop()
        {
            InitializeComponent();
        }

        public frmLocHeThongTichHop(frmThemSuaUserHeThong frmThemSuaUserHeThong)
        {
            InitializeComponent();
            // TODO: Complete member initialization
            this._frmOut = frmThemSuaUserHeThong;
        }

        public frmLocHeThongTichHop(frmThemSuaUnitHeThong frmThemSuaUnitHeThong)
        {
            InitializeComponent();
            this._frmUnitHeThong = frmThemSuaUnitHeThong;
        }

        private void frmLocHeThongTichHop_Load(object sender, EventArgs e)
        {
            try
            {
                //Khởi tạo ListViews
                rlvProg.AllowArbitraryItemHeight = true;
                rlvProg.AllowArbitraryItemWidth = true;
                rlvProg.VisualItemCreating += rlvProg_VisualItemCreating;
                rlvProg.VisualItemFormatting += rlvProg_VisualItemFormatting;

                //Khởi tạo dropdownlist
                InitdrpProgGroup();

                //Lấy dữ liệu và đổ vào control
                LoadData();

                //Thêm sự kiện cho các controls
                //rlvProg.SelectedIndexChanged += rlvProg_SelectedIndexChanged;

            }
            catch (Exception ex)
            {
                clsShare.Message_Error(ex);
            }
        }

        private void InitdrpProgGroup()
        {
            string[] array = { "[Không]", "Trạng thái" };
            drpProgGroup.Items.AddRange(array);
            drpProgGroup.SelectedIndex = 0;
        }

        private void rlvProg_VisualItemCreating(object sender, ListViewVisualItemCreatingEventArgs e)
        {
            if (!(e.VisualItem is BaseListViewGroupVisualItem))
            {
                e.VisualItem = new clsShare.ProgListVisualItem();
            }
        }

        private void rlvProg_VisualItemFormatting(object sender, ListViewVisualItemEventArgs e)
        {
            try
            {
                //Lấy Group Header
                SimpleListViewGroupVisualItem item = e.VisualItem as SimpleListViewGroupVisualItem;
                if (item != null)
                {
                    //Nếu ko nhóm thì return
                    if (rlvProg.GroupDescriptors.Count == 0)
                        return;
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
                this.rlvProg.GroupDescriptors.Clear();
                switch (drpProgGroup.Text)
                {
                    case "[Không]":
                        rlvProg.EnableGrouping = false;
                        rlvProg.ShowGroups = false;
                        break;
                    case "Trạng thái":
                        rlvProg.GroupDescriptors.Add(new GroupDescriptor(
                            new SortDescriptor[] { new SortDescriptor("StatusName", ListSortDirection.Ascending) }));
                        rlvProg.EnableGrouping = true;
                        rlvProg.ShowGroups = true;
                        break;
                    case "Loại đơn vị":
                        rlvProg.GroupDescriptors.Add(new GroupDescriptor(
                            new SortDescriptor[] { new SortDescriptor("UnitType", ListSortDirection.Ascending) }));
                        rlvProg.EnableGrouping = true;
                        rlvProg.ShowGroups = true;
                        break;
                    case "Ngày hiệu lực":
                        rlvProg.GroupDescriptors.Add(new GroupDescriptor(
                            new SortDescriptor[] { new SortDescriptor("ValidFromDate", ListSortDirection.Ascending) }));
                        rlvProg.EnableGrouping = true;
                        rlvProg.ShowGroups = true;
                        break;
                    case "Ngày hết hiệu lực":
                        rlvProg.GroupDescriptors.Add(new GroupDescriptor(
                            new SortDescriptor[] { new SortDescriptor("ValidToDate", ListSortDirection.Ascending) }));
                        rlvProg.EnableGrouping = true;
                        rlvProg.ShowGroups = true;
                        break;
                }
            }
            catch (Exception ex)
            {
                clsShare.Message_Error(ex);
            }
        }

        void rlvProg_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                _progID = (int)rlvProg.SelectedItem.Value;
                LoadUserDataFromProgram(_progID);
            }
            catch (Exception ex)
            {
                clsShare.Message_Error(ex);
            }
        }

        private void LoadUserDataFromProgram(int progID)
        {
            try
            {
                ////Thông tin truy cập hệ thống
                //DataTable dtProg = _bus.CA_Program_SelectByProgID(progID);
                //string serverName = dtProg.Rows[0]["ServerName"].ToString();
                //string dbName = dtProg.Rows[0]["DBName"].ToString();
                //string userDB = dtProg.Rows[0]["UserDB"].ToString();
                //string passwordDB = dtProg.Rows[0]["PasswordDB"].ToString();
                //string tableUser = dtProg.Rows[0]["TableUser"].ToString();
                //string colummUserID = dtProg.Rows[0]["ColummUserID"].ToString();
                //string colummUserName = dtProg.Rows[0]["ColummUserName"].ToString();

                //if (serverName != "" && dbName != "" && userDB != "" && tableUser != "" && colummUserID != "" && colummUserName != "")
                //{
                //    //Lấy bảng dữ liệu
                //    BUSQuanTri busProg = new BUSQuanTri(serverName, dbName, userDB, passwordDB);
                //    DataTable dtUserProgName = busProg.HT_HeThong_SelectUser(tableUser, colummUserID, colummUserName);

                //}
                //else
                //{ }
            }
            catch (Exception ex)
            {
                clsShare.Message_Error("Không thể truy cập dữ liệu người dùng của hệ thống. Hãy kiểm tra lại thông tin kết nối!\n\n" + ex.Message);
            }
        }

        private void LoadData()
        {
            //Bảng danh sách đơn vị
            _dtProg = _bus.CA_Program_SelectAll();
            //Đổ vào ListView
            rlvProg.DataSource = _dtProg;
            rlvProg.DisplayMember = "Name";
            rlvProg.ValueMember = "ProgID";
        }

        private void frmLocHeThongTichHop_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (_frmOut != null)
                {
                    _frmOut.CapNhatDuLieuHeThong(rlvProg.SelectedItem.Text, Convert.ToInt32(rlvProg.SelectedItem.Value));
                }
                if (_frmUnitHeThong != null)
                {
                    _frmUnitHeThong.CapNhatDuLieuHeThong(rlvProg.SelectedItem.Text, Convert.ToInt32(rlvProg.SelectedItem.Value));
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                this.Close();
            }
        }

        private void txtProgFilter_TextChanged(object sender, EventArgs e)
        {
            
            try
            {
                rlvProg.FilterDescriptors.Clear();

                if (String.IsNullOrEmpty(this.txtProgFilter.Text))
                {
                    rlvProg.EnableFiltering = false;
                }
                else
                {
                    rlvProg.FilterDescriptors.LogicalOperator = FilterLogicalOperator.Or;
                    rlvProg.FilterDescriptors.Add("Name", FilterOperator.Contains, this.txtProgFilter.Text);
                    //rlvProg.FilterDescriptors.Add("CMND", FilterOperator.Contains, this.txtProgFilter.Text);
                    rlvProg.EnableFiltering = true;
                }
            }
            catch (Exception ex)
            {
                clsShare.Message_Error(ex);
            }
        }

        private void btnAddProg_Click(object sender, EventArgs e)
        {
            try
            {
                frmThemSuaHeThongTichHop frm = new frmThemSuaHeThongTichHop();
                // truyền tham số
                frm._iProgID = -1;
                frm.ShowDialog();

                // load lại dữ liệu
                LoadData();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }
}

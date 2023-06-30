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
    public partial class frmLocDonViCA : Form
    {
        #region Var
        
        BUSQuanTri _bus = new BUSQuanTri();
        DataTable _dtUnit = new DataTable();
        int _unitID = -1;
        frmThemSuaNguoiDung _frmOut;
        private frmThemSuaUserHeThong _frmUserHeThong;
        private frmThemSuaUnitHeThong _frmUnitHeThong;

        #endregion

        public frmLocDonViCA()
        {
            InitializeComponent();
        }

        public frmLocDonViCA(frmThemSuaNguoiDung frmIn)
        {
            InitializeComponent();
            _frmOut = frmIn;
        }

        public frmLocDonViCA(frmThemSuaUserHeThong frmThemSuaUserHeThong)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this._frmUserHeThong = frmThemSuaUserHeThong;
        }

        public frmLocDonViCA(frmThemSuaUnitHeThong frmThemSuaUnitHeThong)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this._frmUnitHeThong = frmThemSuaUnitHeThong;
        }

        #region Init
        
        private void InitdrpUnitGroup()
        {
            string[] array = { "[Không]", "Trạng thái", "Loại đơn vị", "Ngày hiệu lực", "Ngày hết hiệu lực" };
            drpUnitGroup.Items.AddRange(array);
            drpUnitGroup.SelectedIndex = 0;
        }

        #endregion

        private void rlvUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                _unitID = (int)rlvUnit.SelectedItem.Value;
                //cboUnit.SelectedValue = _unitID;
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

        private void rlvUnit_VisualItemCreating(object sender, ListViewVisualItemCreatingEventArgs e)
        {
            if (!(e.VisualItem is BaseListViewGroupVisualItem))
            {
                e.VisualItem = new clsShare.UnitListVisualItem();
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

        private void LoadData()
        {
            //Bảng danh sách đơn vị
            _dtUnit = _bus.CA_Unit_SelectAll();

            //Đổ vào ListView
            rlvUnit.DataSource = _bus.AddDateCol(_dtUnit);
            rlvUnit.DisplayMember = "Name";
            rlvUnit.ValueMember = "UnitID";
        }

        private void frmLocDonViCA_Load(object sender, EventArgs e)
        {
            try
            {
                //Khởi tạo ListViews
                rlvUnit.AllowArbitraryItemHeight = true;
                rlvUnit.AllowArbitraryItemWidth = true;
                rlvUnit.VisualItemCreating += rlvUnit_VisualItemCreating;
                rlvUnit.VisualItemFormatting += rlvUnit_VisualItemFormatting;

                //Khởi tạo dropdownlist
                InitdrpUnitGroup();

                //Lấy dữ liệu và đổ vào control
                LoadData();

                //Thêm sự kiện cho các controls
                drpUnitGroup.SelectedIndexChanged += drpUnitGroup_SelectedIndexChanged;
                tbUnitFilter.TextChanged += tbUnitFilter_TextChanged;
                rlvUnit.SelectedIndexChanged += rlvUnit_SelectedIndexChanged;
            }
            catch (Exception ex)
            {
                clsShare.Message_Error(ex);
            }
        }

        private void rlvUnit_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (_frmUserHeThong != null)
                {
                    _frmUserHeThong.CapNhatDuLieuUnit(rlvUnit.SelectedItem.Text, Convert.ToInt32(rlvUnit.SelectedItem.Value));
                }

                if (_frmUnitHeThong != null)
                {
                    _frmUnitHeThong.CapNhatDuLieuUnit(rlvUnit.SelectedItem.Text, Convert.ToInt32(rlvUnit.SelectedItem.Value));
                }

                if (_frmOut != null)
                {
                    _frmOut.CapNhatDuLieu(rlvUnit.SelectedItem.Value.ToString(), rlvUnit.SelectedItem.Text);
                }
            }
            catch (Exception ex)
            {
                clsShare.Message_Error(ex.Message);
            }
            finally
            {
                this.Close();
            }
        }
    }
}

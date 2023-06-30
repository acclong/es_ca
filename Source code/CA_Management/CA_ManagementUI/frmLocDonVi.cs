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
    public partial class frmLocDonVi : Form
    {
        DataTable _dtUnitChua = new DataTable();

        #region Var
        
        private int _typeUnit;

        public int TypeUnit
        {
            get { return _typeUnit; }
            set { _typeUnit = value; }
        }


        private string _unitMa;

        public string UnitMa
        {
            get { return _unitMa; }
            set { _unitMa = value; }
        }

        private string _unitName;

        public string UnitName
        {
            get { return _unitName; }
            set { _unitName = value; }
        }

        private string _unitNotation;

        public string UnitNotation
        {
            get { return _unitNotation; }
            set { _unitNotation = value; }
        }

        BUSQuanTri _bus = new BUSQuanTri();
        DataTable _dtUnit = new DataTable();
        DataTable _dtUnitType = new DataTable();

        #endregion

        public frmLocDonVi()
        {
            InitializeComponent();
        }
        frmThemSuaDonVi frmOut;
        public frmLocDonVi(frmThemSuaDonVi frmIn)
        {
            InitializeComponent();
            frmOut = frmIn;
        }

        #region Init

        private void InitdrpUnitGroup()
        {
            string[] array = { "[Không]", "Trạng thái", "Loại đơn vị", "Ngày hiệu lực", "Ngày hết hiệu lực" };
            //drpUnitGroup.Items.AddRange(array);
            //drpUnitGroup.SelectedIndex = 0;
        }

        private void InitUnit()
        {
            try
            {
                //Lấy danh sách đơn vị từ CSDL_Chung
                string sp_Select = _dtUnitType.Rows[_typeUnit]["SP_Select"].ToString();

                _dtUnitChua = _bus.S_DonVi_SelectAll(sp_Select);
                DataTable dtUnitCa = _bus.CA_Unit_SelectAll();

                List<DataRow> A = new List<DataRow>();

                foreach (DataRow item in _dtUnitChua.Rows)
                {
                    foreach (DataRow item1 in dtUnitCa.Rows)
                    {
                        if (item["MaDV"].ToString() == item1["MaDV"].ToString())
                        {
                            A.Add(item);
                            //dtUnitChung.Rows.Remove(item);
                            break;
                        }
                    }
                }

                foreach (DataRow item in A)
                {
                    _dtUnitChua.Rows.Remove(item);
                }

                rlvUnit.DataSource = _dtUnitChua;
                rlvUnit.DisplayMember = "Name";
                rlvUnit.ValueMember = "MaDV";

            }
            catch (Exception ex)
            {
                clsShare.Message_Error(ex);
            }
        }

        #endregion

        private void cboTypeUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //Lấy danh sách đơn vị từ CSDL_Chung
                string sp_Select = _dtUnitType.Rows[_typeUnit]["SP_Select"].ToString();

                _dtUnitChua = _bus.S_DonVi_SelectAll(sp_Select);
                DataTable dtUnitCa = _bus.CA_Unit_SelectAll();

                foreach (DataRow item in _dtUnitChua.Rows)
                {
                    foreach (DataRow item1 in dtUnitCa.Rows)
                    {
                        if (item["MaDV"] == item1["MaDV"])
                        {
                            _dtUnitChua.Rows.Remove(item);
                            break;
                        }
                    }
                }

                rlvUnit.DataSource = _dtUnitChua;
                rlvUnit.DisplayMember = "Name";
                rlvUnit.ValueMember = "MaDV";
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
                //switch (drpUnitGroup.Text)
                //{
                //    case "[Không]":
                //        rlvUnit.EnableGrouping = false;
                //        rlvUnit.ShowGroups = false;
                //        break;
                //    case "Trạng thái":
                //        rlvUnit.GroupDescriptors.Add(new GroupDescriptor(
                //            new SortDescriptor[] { new SortDescriptor("StatusName", ListSortDirection.Ascending) }));
                //        rlvUnit.EnableGrouping = true;
                //        rlvUnit.ShowGroups = true;
                //        break;
                //    case "Loại đơn vị":
                //        rlvUnit.GroupDescriptors.Add(new GroupDescriptor(
                //            new SortDescriptor[] { new SortDescriptor("UnitType", ListSortDirection.Ascending) }));
                //        rlvUnit.EnableGrouping = true;
                //        rlvUnit.ShowGroups = true;
                //        break;
                //    case "Ngày hiệu lực":
                //        rlvUnit.GroupDescriptors.Add(new GroupDescriptor(
                //            new SortDescriptor[] { new SortDescriptor("ValidFromDate", ListSortDirection.Ascending) }));
                //        rlvUnit.EnableGrouping = true;
                //        rlvUnit.ShowGroups = true;
                //        break;
                //    case "Ngày hết hiệu lực":
                //        rlvUnit.GroupDescriptors.Add(new GroupDescriptor(
                //            new SortDescriptor[] { new SortDescriptor("ValidToDate", ListSortDirection.Ascending) }));
                //        rlvUnit.EnableGrouping = true;
                //        rlvUnit.ShowGroups = true;
                //        break;
                //}
            }
            catch (Exception ex)
            {
                clsShare.Message_Error(ex);
            }
        }

        private void rlvUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //txtMaDv.Text = rlvUnit.SelectedItem.Value.ToString();
                //txtUnitName.Text = rlvUnit.SelectedItem.Text;
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
                    rlvUnit.FilterDescriptors.Add("TenTat", FilterOperator.Contains, this.tbUnitFilter.Text);
                    rlvUnit.EnableFiltering = true;
                }
            }
            catch (Exception ex)
            {
                clsShare.Message_Error(ex);
            }
        }

        private void frmLocDonVi_Load(object sender, EventArgs e)
        {
            //Khởi tạo ListViews
            rlvUnit.AllowArbitraryItemHeight = true;
            rlvUnit.AllowArbitraryItemWidth = true;
            rlvUnit.VisualItemCreating += rlvUnit_VisualItemCreating;
            rlvUnit.VisualItemFormatting += rlvUnit_VisualItemFormatting;

            //rlvUnit.AllowArbitraryItemHeight = true;
            //rlvUnit.AllowArbitraryItemWidth = true;
            //rlvUnit.VisualItemCreating += rlvUnit_VisualItemCreating;

            // lấy datatable unit từ db
            _dtUnitType = _bus.CA_UnitType_SelectAll();
            //_dtUnitType = _bus.CA_Certificate_SelectAll();

            InitUnit();
            InitdrpUnitGroup();

            //Thêm sự kiện cho các controls
            //drpUnitGroup.SelectedIndexChanged += drpUnitGroup_SelectedIndexChanged;
            tbUnitFilter.TextChanged += tbUnitFilter_TextChanged;
            rlvUnit.SelectedIndexChanged += rlvUnit_SelectedIndexChanged;
        }

        private void rlvUnit_VisualItemCreating(object sender, ListViewVisualItemCreatingEventArgs e)
        {
            if (!(e.VisualItem is BaseListViewGroupVisualItem))
            {
                e.VisualItem = new clsShare.DonViListVisualItem();
            }
        }

        private void rlvUnit_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                foreach (DataRow item in _dtUnitChua.Rows)
                {
                    if (item["MaDV"].ToString() == rlvUnit.SelectedItem.Value.ToString())
                    {
                        frmOut.capNhatDuLieu(rlvUnit.SelectedItem.Value.ToString(), rlvUnit.SelectedItem.Text, item["TenTat"].ToString());
                        //_unitMa = rlvUnit.SelectedItem.Value.ToString();
                        //_unitName = rlvUnit.SelectedItem.Text;
                        //_unitNotation = item["TenTat"].ToString();
                        break;
                    }
                }

                this.Close();
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

        private void drpUnitGroup_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangingCancelEventArgs e)
        {

        }

        private void tbUnitFilter_TextChanged(object sender, Telerik.WinControls.TextChangingEventArgs e)
        {

        }

    }
}

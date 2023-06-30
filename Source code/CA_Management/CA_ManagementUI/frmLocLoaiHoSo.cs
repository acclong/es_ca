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
    public partial class frmLocLoaiHoSo : Form
    {
        BUSQuanTri _bus = new BUSQuanTri();
        DataTable _dtProfileType = new DataTable();
        public int _ProfileTypeID = -1;
        public string _ProfileTypeName = "";

        public frmLocLoaiHoSo()
        {
            InitializeComponent();
        }

        private void frmLocLoaiHoSo_Load(object sender, EventArgs e)
        {
            try
            {
                //Khởi tạo ListViews
                rlvProfileType.AllowArbitraryItemHeight = true;
                rlvProfileType.AllowArbitraryItemWidth = true;
                rlvProfileType.VisualItemCreating += rlvProfileType_VisualItemCreating;
                rlvProfileType.VisualItemFormatting += rlvProfileType_VisualItemFormatting;

                //Khởi tạo dropdownlist
                InitdrpProfileTypeGroup();

                //Thêm sự kiện cho các controls
                drpProfileTypeGroup.SelectedIndexChanged += drpProfileTypeGroup_SelectedIndexChanged;
                txtProfileTypeFilter.TextChanged += txtProfileTypeFilter_TextChanged;
                rlvProfileType.DoubleClick += new System.EventHandler(this.rlvProfileType_DoubleClick);

                LoadData();

                // gán giá trị cho rlvProfileType
                if(_ProfileTypeID != -1)
                    foreach (ListViewDataItem item in rlvProfileType.Items)
                        if ((int)item.Value == _ProfileTypeID)
                        {
                            rlvProfileType.SelectedItems.Clear();
                            rlvProfileType.SelectedItem = item;
                            break;
                        }
            }
            catch (Exception ex)
            {
                clsShare.Message_Error(ex);
            }
        }

        private void InitdrpProfileTypeGroup()
        {
            string[] array = { "[Không]", "Ngày áp dụng", "Ngày kết thúc" };
            drpProfileTypeGroup.Items.AddRange(array);
            drpProfileTypeGroup.SelectedIndex = 0;
        }

        private void drpProfileTypeGroup_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            try
            {
                this.rlvProfileType.GroupDescriptors.Clear();
                switch (drpProfileTypeGroup.Text)
                {
                    case "[Không]":
                        rlvProfileType.EnableGrouping = false;
                        rlvProfileType.ShowGroups = false;
                        break;
                    case "Trạng thái":
                        rlvProfileType.GroupDescriptors.Add(new GroupDescriptor(
                            new SortDescriptor[] { new SortDescriptor("StatusName", ListSortDirection.Ascending) }));
                        rlvProfileType.EnableGrouping = true;
                        rlvProfileType.ShowGroups = true;
                        break;
                    case "Ngày áp dụng":
                        rlvProfileType.GroupDescriptors.Add(new GroupDescriptor(
                            new SortDescriptor[] { new SortDescriptor("DateStart", ListSortDirection.Ascending) }));
                        rlvProfileType.EnableGrouping = true;
                        rlvProfileType.ShowGroups = true;
                        break;
                    case "Ngày kết thúc":
                        rlvProfileType.GroupDescriptors.Add(new GroupDescriptor(
                            new SortDescriptor[] { new SortDescriptor("DateEnd", ListSortDirection.Ascending) }));
                        rlvProfileType.EnableGrouping = true;
                        rlvProfileType.ShowGroups = true;
                        break;
                }
            }
            catch (Exception ex)
            {
                clsShare.Message_Error(ex);
            }
        }

        private void txtProfileTypeFilter_TextChanged(object sender, EventArgs e)
        {
            try
            {
                rlvProfileType.FilterDescriptors.Clear();

                if (String.IsNullOrEmpty(this.txtProfileTypeFilter.Text))
                {
                    rlvProfileType.EnableFiltering = false;
                }
                else
                {
                    rlvProfileType.FilterDescriptors.LogicalOperator = FilterLogicalOperator.Or;
                    rlvProfileType.FilterDescriptors.Add("Name", FilterOperator.Contains, this.txtProfileTypeFilter.Text);
                    rlvProfileType.EnableFiltering = true;
                }
            }
            catch (Exception ex)
            {
                clsShare.Message_Error(ex);
            }
        }

        private void rlvProfileType_VisualItemCreating(object sender, ListViewVisualItemCreatingEventArgs e)
        {
            if (!(e.VisualItem is BaseListViewGroupVisualItem))
            {
                e.VisualItem = new clsShare.ProfileTypeListVisualItem();
            }
        }

        private void rlvProfileType_VisualItemFormatting(object sender, ListViewVisualItemEventArgs e)
        {
            try
            {
                //Lấy Group Header
                SimpleListViewGroupVisualItem item = e.VisualItem as SimpleListViewGroupVisualItem;
                if (item != null)
                {
                    //Nếu ko nhóm thì return
                    if (rlvProfileType.GroupDescriptors.Count == 0)
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

        private void rlvProfileType_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                _ProfileTypeID = (int)rlvProfileType.SelectedItem.Value;
                _ProfileTypeName = rlvProfileType.SelectedItem.Text;
                this.Close();
            }
            catch (Exception ex)
            {
                clsShare.Message_Error(ex);
            }
        }
        
        //Thêm cột ngày vào datatable để nhóm
        private DataTable AddDateCol(DataTable dt)
        {
            DataColumn dc = new DataColumn("DateStartDate", typeof(DateTime));
            dt.Columns.Add(dc);
            dc = new DataColumn("DateEndDate", typeof(DateTime));
            dt.Columns.Add(dc);

            foreach (DataRow dr in dt.Rows)
            {
                if (dr["DateStart"] != DBNull.Value)
                    dr["DateStartDate"] = Convert.ToDateTime(dr["DateStart"]).Date;
                if (dr["DateEnd"] != DBNull.Value)
                    dr["DateEndDate"] = Convert.ToDateTime(dr["DateEnd"]).Date;
            }

            return dt;
        }

        private void LoadData()
        {
            _dtProfileType = _bus.FL_ProfileType_SelectAll();
            //Đổ vào ListView
            rlvProfileType.DataSource = AddDateCol(_dtProfileType);
            rlvProfileType.DisplayMember = "Name";
            rlvProfileType.ValueMember = "ProfileTypeID";
        } 
    }
}

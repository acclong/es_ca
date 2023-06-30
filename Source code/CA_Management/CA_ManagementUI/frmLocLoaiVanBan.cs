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
    public partial class frmLocLoaiVanBan : Form
    {
        BUSQuanTri _bus = new BUSQuanTri();
        DataTable _dtFileType = new DataTable();
        public int _FileTypeID = -1;
        public string _FileTypeName = "";
        public frmLocLoaiVanBan()
        {
            InitializeComponent();
        }

        private void frmLocLoaiVanBan_Load(object sender, EventArgs e)
        {
            try
            {
                //Khởi tạo ListViews
                rlvFileType.AllowArbitraryItemHeight = true;
                rlvFileType.AllowArbitraryItemWidth = true;
                rlvFileType.VisualItemCreating += rlvFileType_VisualItemCreating;
                rlvFileType.VisualItemFormatting += rlvFileType_VisualItemFormatting;

                //Khởi tạo dropdownlist
                InitdrpFileTypeGroup();

                //Thêm sự kiện cho các controls
                drpFileTypeGroup.SelectedIndexChanged += drpFileTypeGroup_SelectedIndexChanged;
                tbFileTypeFilter.TextChanged += tbFileTypeFilter_TextChanged;
                rlvFileType.DoubleClick += new System.EventHandler(this.rlvFileType_DoubleClick);

                LoadData();

                // gán giá trị cho rlvFileType
                if (_FileTypeID != -1)
                    foreach (ListViewDataItem item in rlvFileType.Items)
                        if ((int)item.Value == _FileTypeID)
                        {
                            rlvFileType.SelectedItems.Clear();
                            rlvFileType.SelectedItem = item;
                            break;
                        }
            }
            catch (Exception ex)
            {
                clsShare.Message_Error(ex);
            }
        }

        private void InitdrpFileTypeGroup()
        {
            string[] array = { "[Không]", "Ngày áp dụng", "Ngày kết thúc" };
            drpFileTypeGroup.Items.AddRange(array);
            drpFileTypeGroup.SelectedIndex = 0;
        }

        private void drpFileTypeGroup_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            try
            {
                this.rlvFileType.GroupDescriptors.Clear();
                switch (drpFileTypeGroup.Text)
                {
                    case "[Không]":
                        rlvFileType.EnableGrouping = false;
                        rlvFileType.ShowGroups = false;
                        break;
                    case "Trạng thái":
                        rlvFileType.GroupDescriptors.Add(new GroupDescriptor(
                            new SortDescriptor[] { new SortDescriptor("StatusName", ListSortDirection.Ascending) }));
                        rlvFileType.EnableGrouping = true;
                        rlvFileType.ShowGroups = true;
                        break;
                    case "Ngày áp dụng":
                        rlvFileType.GroupDescriptors.Add(new GroupDescriptor(
                            new SortDescriptor[] { new SortDescriptor("DateStart", ListSortDirection.Ascending) }));
                        rlvFileType.EnableGrouping = true;
                        rlvFileType.ShowGroups = true;
                        break;
                    case "Ngày kết thúc":
                        rlvFileType.GroupDescriptors.Add(new GroupDescriptor(
                            new SortDescriptor[] { new SortDescriptor("DateEnd", ListSortDirection.Ascending) }));
                        rlvFileType.EnableGrouping = true;
                        rlvFileType.ShowGroups = true;
                        break;
                }
            }
            catch (Exception ex)
            {
                clsShare.Message_Error(ex);
            }
        }

        private void tbFileTypeFilter_TextChanged(object sender, EventArgs e)
        {
            try
            {
                rlvFileType.FilterDescriptors.Clear();

                if (String.IsNullOrEmpty(this.tbFileTypeFilter.Text))
                {
                    rlvFileType.EnableFiltering = false;
                }
                else
                {
                    rlvFileType.FilterDescriptors.LogicalOperator = FilterLogicalOperator.Or;
                    rlvFileType.FilterDescriptors.Add("Name", FilterOperator.Contains, this.tbFileTypeFilter.Text);
                    rlvFileType.EnableFiltering = true;
                }
            }
            catch (Exception ex)
            {
                clsShare.Message_Error(ex);
            }
        }

        private void rlvFileType_VisualItemCreating(object sender, ListViewVisualItemCreatingEventArgs e)
        {
            if (!(e.VisualItem is BaseListViewGroupVisualItem))
            {
                e.VisualItem = new clsShare.FileTypeListVisualItem();
            }
        }

        private void rlvFileType_VisualItemFormatting(object sender, ListViewVisualItemEventArgs e)
        {
            try
            {
                //Lấy Group Header
                SimpleListViewGroupVisualItem groupItem = e.VisualItem as SimpleListViewGroupVisualItem;
                if (groupItem != null)
                {
                    //Nếu ko nhóm thì return
                    if (rlvFileType.GroupDescriptors.Count == 0)
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

        private void rlvFileType_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                _FileTypeID = (int)rlvFileType.SelectedItem.Value;
                _FileTypeName = rlvFileType.SelectedItem.Text;
                this.Close();
            }
            catch (Exception ex)
            {
                clsShare.Message_Error(ex);
            }
        }

        private void LoadData()
        {
            //Bảng danh sách người dùng
            _dtFileType = _bus.FL_FileType_SelectAll();
            //Đổ vào ListView
            rlvFileType.DataSource = AddDateCol(_dtFileType);
            rlvFileType.DisplayMember = "Name";
            rlvFileType.ValueMember = "FileTypeID";
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
    }
}

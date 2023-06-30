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
    public partial class frmThemSuaLoaiVBLoaiHoSo : Form
    {
        BUSQuanTri _bus = new BUSQuanTri();
        DataTable _dtFileType = new DataTable();
        DataTable _dtProfileType = new DataTable();

        int _id_FileTypeProfileType = -1;
        int _FileTypeID = -1;
        int _ProfileTypeID = -1;

        public int ID_FileTypeProfileType
        {
            set { _id_FileTypeProfileType = value; }
            get { return _id_FileTypeProfileType; }
        }

        public int FileTypeID
        {
            set { _FileTypeID = value; }
            get { return _FileTypeID; }
        }

        public int ProfileTypeID
        {
            set { _ProfileTypeID = value; }
            get { return _ProfileTypeID; }
        }

        public frmThemSuaLoaiVBLoaiHoSo()
        {
            InitializeComponent();
        }

        private void frmThemSuaLoaiVBLoaiHoSo_Load(object sender, EventArgs e)
        {
            try
            {
                dpkDateStart.Select();
                if (ID_FileTypeProfileType != -1)
                {
                    // lấy dữ liệu từ database
                    DataTable dt = _bus.FL_FileProfile_SelectByIDFileProfile(ID_FileTypeProfileType);

                    //Đổ lên controls
                    txtID_FileTypeProfileType.Text = ID_FileTypeProfileType.ToString();
                    FileTypeID = Convert.ToInt32(dt.Rows[0]["FileTypeID"]);
                    ProfileTypeID = Convert.ToInt32(dt.Rows[0]["ProfileTypeID"]);
                    txtFileType.Text = dt.Rows[0]["FileTypeName"].ToString();
                    btnSearchFileType.Enabled = false;
                    txtProfileType.Text = dt.Rows[0]["ProfileTypeName"].ToString();
                    btnSearchProfileType.Enabled = false;
                    dpkDateStart.Value = Convert.ToDateTime(dt.Rows[0]["DateStart"].ToString());
                    if (dt.Rows[0]["DateEnd"] == DBNull.Value)
                        chkDateEnd.Checked = false;
                    else
                    {
                        dpkDateEnd.Value = Convert.ToDateTime(dt.Rows[0]["DateEnd"].ToString());
                        chkDateEnd.Checked = true;
                    }
                }
                #region code cũ
                ////Khởi tạo ListViews
                //rlvFileType.AllowArbitraryItemHeight = true;
                //rlvFileType.AllowArbitraryItemWidth = true;
                //rlvFileType.VisualItemCreating += rlvFileType_VisualItemCreating;
                //rlvFileType.VisualItemFormatting += rlvFileType_VisualItemFormatting;
                ////
                //rlvProfileType.AllowArbitraryItemHeight = true;
                //rlvProfileType.AllowArbitraryItemWidth = true;
                //rlvProfileType.VisualItemCreating += rlvProfileType_VisualItemCreating;
                //rlvProfileType.VisualItemFormatting += rlvProfileType_VisualItemFormatting;

                ////Khởi tạo dropdownlist
                //InitdrpFileTypeGroup();
                //InitdrpProfileTypeGroup();
                //cboFileType.Enabled = false;
                //cboProfileType.Enabled = false;

                ////Lấy dữ liệu và đổ vào control
                //LoadData();

                ////Thêm sự kiện cho các controls
                //drpFileTypeGroup.SelectedIndexChanged += drpFileTypeGroup_SelectedIndexChanged;
                //drpProfileTypeGroup.SelectedIndexChanged += drpProfileTypeGroup_SelectedIndexChanged;
                //tbFileTypeFilter.TextChanged += tbFileTypeFilter_TextChanged;
                //txtProfileTypeFilter.TextChanged += txtProfileTypeFilter_TextChanged;
                //rlvFileType.SelectedIndexChanged += rlvFileType_SelectedIndexChanged;
                //rlvProfileType.SelectedIndexChanged += rlvProfileType_SelectedIndexChanged;

                ////Lấy dữ liệu nếu sửa
                //if (ID_FileTypeProfileType != -1)
                //{
                //    // lấy dữ liệu từ database
                //    DataTable dt = _bus.FL_FileProfile_SelectByIDFileProfile(ID_FileTypeProfileType);

                //    //Đổ lên controls
                //    txtID_FileTypeProfileType.Text = ID_FileTypeProfileType.ToString();
                //    _FileTypeID = Convert.ToInt32(dt.Rows[0]["FileTypeID"]);
                //    _ProfileTypeID = Convert.ToInt32(dt.Rows[0]["ProfileTypeID"]);
                //    cboFileType.SelectedValue = _FileTypeID;
                //    cboProfileType.SelectedValue = _ProfileTypeID;
                //    dpkDateStart.Value = Convert.ToDateTime(dt.Rows[0]["DateStart"].ToString());
                //    if (dt.Rows[0]["DateEnd"] == DBNull.Value)
                //        chkDateEnd.Checked = false;
                //    else
                //    {
                //        dpkDateEnd.Value = Convert.ToDateTime(dt.Rows[0]["DateEnd"].ToString());
                //        chkDateEnd.Checked = true;
                //    }

                //    //Thiết lập lựa chọn trên ListView
                //    foreach (ListViewDataItem item in rlvFileType.Items)
                //        if ((int)item.Value == _FileTypeID)
                //        {
                //            rlvFileType.SelectedItems.Clear();
                //            rlvFileType.SelectedItem = item;
                //            break;
                //        }

                //    foreach (ListViewDataItem item in rlvProfileType.Items)
                //        if ((int)item.Value == _ProfileTypeID)
                //        {
                //            rlvProfileType.SelectedItems.Clear();
                //            rlvProfileType.SelectedItem = item;
                //            break;
                //        }

                //    // không cho thay đổi ngày bắt đầu
                //    dpkDateStart.Enabled = false;

                //    // khóa sự kiện chọn thay đổi
                //    rlvFileType.SelectedIndexChanged -= rlvFileType_SelectedIndexChanged;
                //    rlvProfileType.SelectedIndexChanged -= rlvProfileType_SelectedIndexChanged;
                //}
                //else
                //{
                //    //Thiết lập lựa chọn FileType mặc định
                //    rlvFileType.SelectedIndex = 0;
                //    _FileTypeID = (int)rlvFileType.SelectedItem.Value;
                //    cboFileType.SelectedValue = _FileTypeID;

                //    //Thiết lập lựa chọn ProfileType mặc định
                //    rlvProfileType.SelectedIndex = 0;
                //    _ProfileTypeID = (int)rlvProfileType.SelectedItem.Value;
                //    cboProfileType.SelectedValue = _ProfileTypeID;
                //}
                #endregion
            }
            catch (Exception ex)
            {
                clsShare.Message_Error(ex);
            }
        }

        private void chkDateEnd_CheckedChanged(object sender, EventArgs e)
        {
            dpkDateEnd.Visible = chkDateEnd.Checked;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // lấy giá trị biến tạm thời, kiểm tra dữ liệu đầu vào
                if (FileTypeID == -1 || ProfileTypeID == -1)
                {
                    clsShare.Message_Error("Các trường có dấu * không được phép để trống!");
                    return;
                }

                DateTime dateStart = dpkDateStart.Value.Date;
                DateTime dateEnd = chkDateEnd.Checked ? dpkDateEnd.Value.Date : DateTime.MaxValue;

                if (chkDateEnd.Checked && dpkDateStart.Value != null)
                    if (DateTime.Compare(dateEnd, dateStart) == -1)
                    {
                        clsShare.Message_Error("Ngày kết thúc không được nhỏ hơn Ngày áp dụng!");
                        return;
                    }

                // thêm sửa thời gian không chồng lấn
                DataTable dtFileProfile = _bus.FL_FileProfile_FileTypeID_ProfileTypeID(FileTypeID, ProfileTypeID);

                if (!KiemTraHieuLuc(dtFileProfile, 3, ID_FileTypeProfileType, dateStart, dateEnd))
                {
                    clsShare.Message_Error("Liên kết đang được cập nhật bị chồng lẫn về thời gian. Vui lòng kiểm tra lại!");
                    return;
                }
                else
                {
                    // lưu vào update DB
                    _bus.FL_FileProfile_InsertUpdate(ID_FileTypeProfileType, FileTypeID, ProfileTypeID, dateStart, dateEnd, clsShare.sUserName);
                    clsShare.Message_Info("Cập nhật liên kết loại hồ sơ - loại văn bản thành công!");
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

        private void btnSearchProfileType_Click(object sender, EventArgs e)
        {
            try
            {
                frmLocLoaiHoSo frm = new frmLocLoaiHoSo();
                if (ProfileTypeID != -1)
                {
                    frm._ProfileTypeID = ProfileTypeID;
                    frm._ProfileTypeName = txtProfileType.Text;
                }
                frm.ShowDialog();
                ProfileTypeID = frm._ProfileTypeID;
                txtProfileType.Text = frm._ProfileTypeName;
            }
            catch (Exception ex)
            {
                clsShare.Message_Error(ex.Message);
            }
        }

        private void btnSearchFileType_Click(object sender, EventArgs e)
        {
            try
            {
                frmLocLoaiVanBan frm = new frmLocLoaiVanBan();
                if (FileTypeID != -1)
                {
                    frm._FileTypeID = FileTypeID;
                    frm._FileTypeName = txtFileType.Text;
                }
                frm.ShowDialog();
                FileTypeID = frm._FileTypeID;
                txtFileType.Text = frm._FileTypeName;
            }
            catch (Exception ex)
            {
                clsShare.Message_Error(ex.Message);
            }
        }

        /// <summary>
        /// kiểm tra hiệu lực các giá trị thêm mới muốn insert vào csdl
        /// </summary>
        /// <param name="dt">bảng dữ liệu bao gồm ngày bắt đầu và kết thúc, id của bảng là col 0</param>
        /// <param name="index">chỉ số ngày bắt đầu, ngày kết thúc là index + 1</param>
        /// <param name="id">kiểm tra thêm mới hoặc sửa thông tin</param>
        /// <param name="begin">thời gian bắt đầu mới</param>
        /// <param name="end">thời gian kết thúc mới</param>
        /// <returns></returns>
        private bool KiemTraHieuLuc(DataTable dt, int index, int id, DateTime begin, DateTime end)
        {
            if (dt.Rows.Count > 0)
            {
                if (id == -1)
                {
                    for (int i = (dt.Rows.Count - 1); i >= 0; i--)
                    {
                        // kiểm tra ngày bắt đầu nào lớn hơn giá trị ngày kết thúc mới
                        if (Convert.ToDateTime(dt.Rows[i][index]) > end)
                        {
                            // ngày kết thúc bé hơn ngày bắt đầu đầu tiên
                            if (i == dt.Rows.Count - 1)
                                return true;
                            // ngày kết thúc bé hơn ngày bắt đầu của 1 dòng trên và lớn hơn ngày bắt đầu dòng dưới
                            else if (FormatEndDate(dt.Rows[i][index + 1]) < begin)
                                return true;
                            else
                                return false;
                        }
                    }
                    // nếu ngày kết thúc mới không nhỏ hơn ngày bắt đầu nào
                    if (FormatEndDate(dt.Rows[0][index + 1]) < begin)
                        return true;
                    else
                        return false;
                }
                else
                {
                    if (dt.Rows.Count == 1)
                        return true;
                    else
                        for (int i = 0; i < dt.Rows.Count; i++)
                            if (Convert.ToInt32(dt.Rows[i][0]) == id)
                            {
                                if (i == 0 && FormatEndDate(dt.Rows[i + 1][index + 1]) < begin)
                                    return true;
                                else if (i == (dt.Rows.Count - 1) && Convert.ToDateTime(dt.Rows[i - 1][index]) > end)
                                    return true;
                                else if (i != (dt.Rows.Count - 1) && i != 0 &&
                                    Convert.ToDateTime(dt.Rows[i - 1][index]) > end &&
                                    FormatEndDate(dt.Rows[i + 1][index + 1]) < begin)
                                    return true;
                                else
                                    return false;
                            }
                    return false;
                }
            }
            else
                return true;
        }

        private DateTime FormatEndDate(object end)
        {
            if (end == null)
                return DateTime.MaxValue;
            else
                return Convert.ToDateTime(end);
        }

        #region code cũ
        //private void InitdrpFileTypeGroup()
        //{
        //    string[] array = { "[Không]", "Trạng thái", "Ngày áp dụng", "Ngày kết thúc" };
        //    drpFileTypeGroup.Items.AddRange(array);
        //    drpFileTypeGroup.SelectedIndex = 0;
        //}

        //private void InitdrpProfileTypeGroup()
        //{
        //    string[] array = { "[Không]", "Trạng thái", "Ngày áp dụng", "Ngày kết thúc" };
        //    drpProfileTypeGroup.Items.AddRange(array);
        //    drpProfileTypeGroup.SelectedIndex = 0;
        //}

        //private void drpFileTypeGroup_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        //{
        //    try
        //    {
        //        this.rlvFileType.GroupDescriptors.Clear();
        //        switch (drpFileTypeGroup.Text)
        //        {
        //            case "[Không]":
        //                rlvFileType.EnableGrouping = false;
        //                rlvFileType.ShowGroups = false;
        //                break;
        //            case "Trạng thái":
        //                rlvFileType.GroupDescriptors.Add(new GroupDescriptor(
        //                    new SortDescriptor[] { new SortDescriptor("StatusName", ListSortDirection.Ascending) }));
        //                rlvFileType.EnableGrouping = true;
        //                rlvFileType.ShowGroups = true;
        //                break;
        //            case "Ngày áp dụng":
        //                rlvFileType.GroupDescriptors.Add(new GroupDescriptor(
        //                    new SortDescriptor[] { new SortDescriptor("DateStart", ListSortDirection.Ascending) }));
        //                rlvFileType.EnableGrouping = true;
        //                rlvFileType.ShowGroups = true;
        //                break;
        //            case "Ngày kết thúc":
        //                rlvFileType.GroupDescriptors.Add(new GroupDescriptor(
        //                    new SortDescriptor[] { new SortDescriptor("DateEnd", ListSortDirection.Ascending) }));
        //                rlvFileType.EnableGrouping = true;
        //                rlvFileType.ShowGroups = true;
        //                break;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        clsShare.Message_Error(ex);
        //    }
        //}

        //private void drpProfileTypeGroup_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        //{
        //    try
        //    {
        //        this.rlvProfileType.GroupDescriptors.Clear();
        //        switch (drpProfileTypeGroup.Text)
        //        {
        //            case "[Không]":
        //                rlvProfileType.EnableGrouping = false;
        //                rlvProfileType.ShowGroups = false;
        //                break;
        //            case "Trạng thái":
        //                rlvProfileType.GroupDescriptors.Add(new GroupDescriptor(
        //                    new SortDescriptor[] { new SortDescriptor("StatusName", ListSortDirection.Ascending) }));
        //                rlvProfileType.EnableGrouping = true;
        //                rlvProfileType.ShowGroups = true;
        //                break;
        //            case "Ngày áp dụng":
        //                rlvProfileType.GroupDescriptors.Add(new GroupDescriptor(
        //                    new SortDescriptor[] { new SortDescriptor("DateStart", ListSortDirection.Ascending) }));
        //                rlvProfileType.EnableGrouping = true;
        //                rlvProfileType.ShowGroups = true;
        //                break;
        //            case "Ngày kết thúc":
        //                rlvProfileType.GroupDescriptors.Add(new GroupDescriptor(
        //                    new SortDescriptor[] { new SortDescriptor("DateEnd", ListSortDirection.Ascending) }));
        //                rlvProfileType.EnableGrouping = true;
        //                rlvProfileType.ShowGroups = true;
        //                break;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        clsShare.Message_Error(ex);
        //    }
        //}

        //private void tbFileTypeFilter_TextChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        rlvFileType.FilterDescriptors.Clear();

        //        if (String.IsNullOrEmpty(this.tbFileTypeFilter.Text))
        //        {
        //            rlvFileType.EnableFiltering = false;
        //        }
        //        else
        //        {
        //            rlvFileType.FilterDescriptors.LogicalOperator = FilterLogicalOperator.Or;
        //            rlvFileType.FilterDescriptors.Add("Name", FilterOperator.Contains, this.tbFileTypeFilter.Text);
        //            rlvFileType.EnableFiltering = true;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        clsShare.Message_Error(ex);
        //    }
        //}

        //private void txtProfileTypeFilter_TextChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        rlvProfileType.FilterDescriptors.Clear();

        //        if (String.IsNullOrEmpty(this.txtProfileTypeFilter.Text))
        //        {
        //            rlvProfileType.EnableFiltering = false;
        //        }
        //        else
        //        {
        //            rlvProfileType.FilterDescriptors.LogicalOperator = FilterLogicalOperator.Or;
        //            rlvProfileType.FilterDescriptors.Add("Name", FilterOperator.Contains, this.txtProfileTypeFilter.Text);
        //            rlvProfileType.EnableFiltering = true;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        clsShare.Message_Error(ex);
        //    }
        //}

        //private void rlvFileType_VisualItemCreating(object sender, ListViewVisualItemCreatingEventArgs e)
        //{
        //    if (!(e.VisualItem is BaseListViewGroupVisualItem))
        //    {
        //        e.VisualItem = new clsShare.FileTypeListVisualItem();
        //    }
        //}

        //private void rlvProfileType_VisualItemCreating(object sender, ListViewVisualItemCreatingEventArgs e)
        //{
        //    if (!(e.VisualItem is BaseListViewGroupVisualItem))
        //    {
        //        e.VisualItem = new clsShare.ProfileTypeListVisualItem();
        //    }
        //}

        //private void rlvFileType_VisualItemFormatting(object sender, ListViewVisualItemEventArgs e)
        //{
        //    try
        //    {
        //        //Lấy Group Header
        //        SimpleListViewGroupVisualItem groupItem = e.VisualItem as SimpleListViewGroupVisualItem;
        //        if (groupItem != null)
        //        {
        //            //Nếu ko nhóm thì return
        //            if (rlvFileType.GroupDescriptors.Count == 0)
        //                return;
        //            //Định dạng ngày
        //            DateTime date = new DateTime();
        //            if (DateTime.TryParse(groupItem.Text, out date))
        //                groupItem.Text = date.ToString("dd/MM/yyyy");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        clsShare.Message_Error(ex);
        //    }
        //}

        //private void rlvProfileType_VisualItemFormatting(object sender, ListViewVisualItemEventArgs e)
        //{
        //    try
        //    {
        //        //Lấy Group Header
        //        SimpleListViewGroupVisualItem item = e.VisualItem as SimpleListViewGroupVisualItem;
        //        if (item != null)
        //        {
        //            //Nếu ko nhóm thì return
        //            if (rlvProfileType.GroupDescriptors.Count == 0)
        //                return;
        //            //Định dạng ngày
        //            DateTime date = new DateTime();
        //            if (DateTime.TryParse(item.Text, out date))
        //                item.Text = date.ToString("dd/MM/yyyy");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        clsShare.Message_Error(ex);
        //    }
        //}

        //void rlvFileType_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        _FileTypeID = (int)rlvFileType.SelectedItem.Value;
        //        cboFileType.SelectedValue = _FileTypeID;
        //    }
        //    catch (Exception ex)
        //    {
        //        clsShare.Message_Error(ex);
        //    }
        //}

        //void rlvProfileType_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        _ProfileTypeID = (int)rlvProfileType.SelectedItem.Value;
        //        cboProfileType.SelectedValue = _ProfileTypeID;
        //    }
        //    catch (Exception ex)
        //    {
        //        clsShare.Message_Error(ex);
        //    }
        //}

        ////Thêm cột ngày vào datatable để nhóm
        //private DataTable AddDateCol(DataTable dt)
        //{
        //    DataColumn dc = new DataColumn("DateStartDate", typeof(DateTime));
        //    dt.Columns.Add(dc);
        //    dc = new DataColumn("DateEndDate", typeof(DateTime));
        //    dt.Columns.Add(dc);

        //    foreach (DataRow dr in dt.Rows)
        //    {
        //        if (dr["DateStart"] != DBNull.Value)
        //            dr["DateStartDate"] = Convert.ToDateTime(dr["DateStart"]).Date;
        //        if (dr["DateEnd"] != DBNull.Value)
        //            dr["DateEndDate"] = Convert.ToDateTime(dr["DateEnd"]).Date;
        //    }

        //    return dt;
        //}

        //private void LoadData()
        //{
        //    //Bảng danh sách người dùng
        //    _dtFileType = _bus.FL_FileType_SelectAll();
        //    //Đổ vào ListView
        //    rlvFileType.DataSource = AddDateCol(_dtFileType);
        //    rlvFileType.DisplayMember = "Name";
        //    rlvFileType.ValueMember = "FileTypeID";
        //    //Đổ vào cbo
        //    cboFileType.DataSource = _dtFileType;
        //    cboFileType.DisplayMember = "Name";
        //    cboFileType.ValueMember = "FileTypeID";

        //    //Bảng danh sách chứng thư số
        //    _dtProfileType = _bus.FL_ProfileType_SelectAll();
        //    //Đổ vào ListView
        //    rlvProfileType.DataSource = AddDateCol(_dtProfileType);
        //    rlvProfileType.DisplayMember = "Name";
        //    rlvProfileType.ValueMember = "ProfileTypeID";
        //    //Đổ vào cbo
        //    cboProfileType.DataSource = _dtProfileType;
        //    cboProfileType.DisplayMember = "Name";
        //    cboProfileType.ValueMember = "ProfileTypeID";
        //} 
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ES.CA_ManagementBUS;
using Telerik.WinControls.UI;
using Telerik.WinControls.Data;
using C1.Win.C1FlexGrid;

namespace ES.CA_ManagementUI
{
    public partial class ucLienKetUserDonVi : UserControl
    {        
        public ucLienKetUserDonVi()
        {
            InitializeComponent();
        }

        private void ucLienKetUserDonVi_Load(object sender, EventArgs e)
        {
            try
            {
                LoadData();
                InitRgvNguoiDung();

                // Thêm sự kiện
                rgvUserDonVi.KeyDown += new KeyEventHandler(clsShare.C1FlexGrid_KeyDown);
            }
            catch (Exception ex)
            {
                clsShare.Message_Error(ex);
            }
        }

        private void rgvUserDonVi_GroupByChanged(object sender, GridViewCollectionChangedEventArgs e)
        {
            if (rgvUserDonVi.GroupDescriptors.Count() == 2)
                rgvUserDonVi.GroupDescriptors.RemoveAt(0);
            if (rgvUserDonVi.GroupDescriptors.Count == 1 && rgvUserDonVi.GroupDescriptors[0].GroupNames.Count == 2)
                rgvUserDonVi.GroupDescriptors[0].GroupNames.RemoveAt(0);
        }

        private void rgvUserDonVi_DoubleClick(object sender, EventArgs e)
        {
            if (rgvUserDonVi.MasterView.CurrentRow != null)
            {
                btnEdit_Click(null, null);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmThemSuaUserDonVi frm = new frmThemSuaUserDonVi();
            frm.ShowDialog();
            // load lại dữ liệu
            LoadData();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                frmThemSuaUserDonVi frm = new frmThemSuaUserDonVi();
                if (rgvUserDonVi.MasterView.CurrentRow != null)
                {
                    frm.ID_UserUnit = Convert.ToInt32(rgvUserDonVi.CurrentRow.Cells["ID_UserUnit"].Value);
                    frm.ShowDialog();

                    // load lại dữ liệu
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                clsShare.Message_Error(ex);
            }
        }

        private void InitRgvNguoiDung()
        {
            // cấu hình radGrid
            rgvUserDonVi.MasterTemplate.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;

            rgvUserDonVi.EnableFiltering = true;
            rgvUserDonVi.MasterTemplate.ShowHeaderCellButtons = true;
            rgvUserDonVi.MasterTemplate.ShowFilteringRow = false;

            //Cho phép group
            rgvUserDonVi.AllowDragToGroup = true;
            rgvUserDonVi.ShowGroupPanel = true;

            //Tên cột
            string[] arrName = { "ID_UserUnit", "UnitID", "UnitName", "UserID", "UserName", "Department", "ValidFrom", "ValidTo" };
            string[] arrHeader = { "ID", "UnitID", "Đơn vị", "UserID", "Người dùng", "Bộ phận", "Hiệu lực từ", "Hiệu lực đến" };
            for (int i = 0; i < arrName.Length; i++)
            {
                rgvUserDonVi.Columns[i].Name = arrName[i];
                rgvUserDonVi.Columns[i].HeaderText = arrHeader[i];

                if (i == 0 || i == 6 || i == 7)
                    rgvUserDonVi.Columns[i].TextAlignment = ContentAlignment.MiddleCenter;
                else
                    rgvUserDonVi.Columns[i].TextAlignment = ContentAlignment.MiddleLeft;
                // Kích thước cột
                if (i == 0)
                    rgvUserDonVi.Columns[i].Width = 7;
                else if (i == 6 || i == 7)
                    rgvUserDonVi.Columns[i].Width = 20;
                // Format cột ngày
                if (i == 6 || i == 7)
                    rgvUserDonVi.Columns[i].FormatString = "{0: dd/MM/yyyy}";
                // Ẩn cột
                if (i == 1 || i == 3)
                    rgvUserDonVi.Columns[i].IsVisible = false;
                //Cho phép filter
                if (i == 0)
                    rgvUserDonVi.Columns[i].AllowFiltering = false;
                //Cho phép nhóm
                if (i != 2 && i != 4)
                    rgvUserDonVi.Columns[i].AllowGroup = false;
            }

            //Nhóm theo đơn vị
            rgvUserDonVi.GroupDescriptors.Clear();
            GroupDescriptor descriptor = new GroupDescriptor();
            descriptor.GroupNames.Add("UnitName", ListSortDirection.Ascending);
            rgvUserDonVi.GroupDescriptors.Add(descriptor);
            rgvUserDonVi.MasterTemplate.ExpandAllGroups();
        }

        private void LoadData()
        {
            // lấy dữ liệu từ database
            BUSQuanTri bus = new BUSQuanTri();
            rgvUserDonVi.DataSource = bus.CA_UserUnit_SelectAll();
        }
    }
}

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
using System.Security.Cryptography.X509Certificates;
using Telerik.WinControls.Data;
namespace ES.CA_ManagementUI
{
    public partial class ucLienKetUserCert : UserControl
    {
        BUSQuanTri _bus = new BUSQuanTri();
        public ucLienKetUserCert()
        {
            InitializeComponent();
        }

        private void ucPhanQuyenCertUser_Load(object sender, EventArgs e)
        {
            try
            {
                LoadData();
                InitRgvUserCert();

                // Thêm sự kiện KeyDown
                rgvUserCert.KeyDown += new KeyEventHandler(clsShare.C1FlexGrid_KeyDown);
            }
            catch (Exception ex)
            {
                clsShare.Message_Error(ex);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                frmThemSuaUserCert frm = new frmThemSuaUserCert();
                frm.ID_UserCert = -1;
                frm.ShowDialog();

                LoadData();
            }
            catch (Exception ex)
            {
                clsShare.Message_Error(ex);
            }

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                // kiểm tra xem có chọn dòng header group
                if (rgvUserCert.CurrentRow.GetType() == typeof(GridViewGroupRowInfo))
                    return;

                // show frm
                frmThemSuaUserCert frm = new frmThemSuaUserCert();
                frm.ID_UserCert = Convert.ToInt32(rgvUserCert.CurrentRow.Cells["ID_UserCert"].Value);
                frm.ShowDialog();

                LoadData();
            }
            catch (Exception ex)
            {
                clsShare.Message_Error(ex);
            }

        }

        private void rgvUserCert_CommandCellClick(object sender, EventArgs e)
        {
            try
            {
                // lấy giá trị CertAuthID tương ứng
                GridCommandCellElement gcce = sender as GridCommandCellElement;
                int iCertID = Convert.ToInt32(gcce.RowInfo.Cells["CertID"].Value);

                // lấy dữ liệu từ db
                byte[] rawData = _bus.CA_Certificate_SelectRawDataByID(iCertID);
                // show thông tin Certificate
                X509Certificate2 cert = new X509Certificate2(rawData);
                X509Certificate2UI.DisplayCertificate(cert);
            }
            catch (Exception ex)
            {
                clsShare.Message_Error(ex);
            }
        }

        private void rgvUserCert_GroupByChanged(object sender, GridViewCollectionChangedEventArgs e)
        {
            if (rgvUserCert.GroupDescriptors.Count() == 2)
                rgvUserCert.GroupDescriptors.RemoveAt(0);
            if (rgvUserCert.GroupDescriptors.Count == 1 && rgvUserCert.GroupDescriptors[0].GroupNames.Count == 2)
                rgvUserCert.GroupDescriptors[0].GroupNames.RemoveAt(0);
        }

        private void InitRgvUserCert()
        {
            // cấu hình radGrid
            rgvUserCert.MasterTemplate.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;

            rgvUserCert.EnableFiltering = true;
            rgvUserCert.MasterTemplate.ShowHeaderCellButtons = true;
            rgvUserCert.MasterTemplate.ShowFilteringRow = false;

            //Cho phép group
            rgvUserCert.AllowDragToGroup = true;
            rgvUserCert.ShowGroupPanel = true;

            //Tên cột
            string[] arrName = { "ID_UserCert", "UserID", "UserName", "CertID", "CertificateName", "Type", "TypeName", "ValidFrom", "ValidTo" };
            string[] arrHeaders = { "ID", "UserID", "Tên người dùng", "CertID", "Tên chứng thư", "Type", "Loại Chứng thư", "Hiệu lực từ", "Hiệu lực đến" };

            for (int i = 0; i < arrName.Length; i++)
            {
                // Tên và Header
                rgvUserCert.Columns[i].Name = arrName[i];
                rgvUserCert.Columns[i].HeaderText = arrHeaders[i];
                // Kích thước cột
                if (i == 0)
                    rgvUserCert.Columns[i].Width = 7;
                else if (i == 2)
                    rgvUserCert.Columns[i].Width = 30;
                else if (i == 6)
                    rgvUserCert.Columns[i].Width = 30;
                else if (i == 7 || i == 8)
                {
                    rgvUserCert.Columns[i].FormatString = "{0: dd/MM/yyyy}";
                    rgvUserCert.Columns[i].Width = 18;
                }
                // Căn lề
                if (i == 2 || i == 4)
                    rgvUserCert.Columns[i].TextAlignment = ContentAlignment.MiddleLeft;
                else
                    rgvUserCert.Columns[i].TextAlignment = ContentAlignment.MiddleCenter;
                // ẩn các cột ID
                if (i == 1 || i == 3 || i == 5)
                    rgvUserCert.Columns[i].IsVisible = false;
            }

            //Thêm cột view certificate button
            GridViewCommandColumn gvCol = new GridViewCommandColumn();
            gvCol.Name = "CommandColumn";
            gvCol.HeaderText = "Xem";
            //gvCol.FieldName = "RawData";
            gvCol.UseDefaultText = true;
            gvCol.DefaultText = "Xem chứng thư";
            gvCol.TextAlignment = ContentAlignment.MiddleCenter;
            gvCol.Width = 20;
            rgvUserCert.MasterTemplate.Columns.Add(gvCol);
            rgvUserCert.CommandCellClick += new CommandCellClickEventHandler(rgvUserCert_CommandCellClick);

            //Nhóm theo đơn vị
            rgvUserCert.GroupDescriptors.Clear();
            GroupDescriptor descriptor = new GroupDescriptor();
            descriptor.GroupNames.Add("TypeName", ListSortDirection.Ascending);
            rgvUserCert.GroupDescriptors.Add(descriptor);
            rgvUserCert.MasterTemplate.ExpandAllGroups();
        }

        private void LoadData()
        {
            rgvUserCert.DataSource = _bus.CA_CertificateUser_SelectAll();
        }
    }
}

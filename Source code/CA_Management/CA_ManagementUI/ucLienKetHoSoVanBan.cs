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
    public partial class ucLienKetHoSoVanBan : UserControl
    {
        BUSQuanTri _bus = new BUSQuanTri();

        public ucLienKetHoSoVanBan()
        {
            InitializeComponent();
        }

        private void ucLienKetHoSoVanBan_Load(object sender, EventArgs e)
        {
            try
            {
                LoadData();
                InitRgvFileProfile();
                // chỉnh sửa kích thước panel
                clsShare.FormatWidthComboBoxInPanel(pnlHeader);

                // Thêm sự kiện KeyDown
                cfgFileProfile.KeyDown += new KeyEventHandler(clsShare.C1FlexGrid_KeyDown);
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
                frmThemSuaLoaiVBLoaiHoSo frm = new frmThemSuaLoaiVBLoaiHoSo();
                frm.ID_FileTypeProfileType = -1;
                frm.ShowDialog();

                LoadData();
                InitRgvFileProfile();
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
                if (cfgFileProfile.Row == cfgFileProfile.RowSel)
                {
                    // show frm
                    frmThemSuaLoaiVBLoaiHoSo frm = new frmThemSuaLoaiVBLoaiHoSo();
                    frm.ID_FileTypeProfileType = Convert.ToInt32(cfgFileProfile.Rows[cfgFileProfile.Row]["ID_FileProfile"]);
                    frm.ShowDialog();

                    LoadData();
                    InitRgvFileProfile();
                }
            }
            catch (Exception ex)
            {
                clsShare.Message_Error(ex);
            }
        }

        private void cfgFileProfile_DoubleClick(object sender, EventArgs e)
        {
            btnEdit_Click(sender, e);
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                LoadData();
            }
            catch (Exception ex)
            {
                clsShare.Message_Error(ex);
            }
        }

        private void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                dpkDate.Visible = (!chkSelectAll.Checked);
                clsShare.FormatWidthComboBoxInPanel(pnlHeader);
            }
            catch (Exception ex)
            {
                clsShare.Message_Error(ex);
            }
        }

        private void InitRgvFileProfile()
        {
            cfgFileProfile.Clear(ClearFlags.Style);
            //Tên cột
            string[] arrName = {"", "STT", "ID_FileProfile", "ProfileTypeID", "ProfileType",
                                   "FileTypeID", "FileType", "Notation", "DateStart", "DateEnd"};
            string[] arrCaption = {"", "STT", "ID_FileProfile", "ProfileTypeID", "Loại hồ sơ",
                                   "FileTypeID", "Loại văn bản", "Ký hiệu văn bản", "Ngày áp dụng", "Ngày kết thúc" };

            for (int i = 0; i < arrName.Length; i++)
            {
                // Tên và Header
                cfgFileProfile.Cols[i].Name = arrName[i];
                cfgFileProfile.Cols[i].Caption = arrCaption[i];
                switch (i)
                {
                    case 0: cfgFileProfile.Cols[i].Width = 25; break;
                    case 1: cfgFileProfile.Cols[i].Width = 40; break;
                    case 4:
                    case 6:
                    case 7: cfgFileProfile.Cols[i].Width = 200; break;
                    case 8:
                    case 9: cfgFileProfile.Cols[i].Width = 120; break;
                }
                // Căn lề
                if (i == 4 || i == 6 || i == 7)
                    cfgFileProfile.Cols[i].TextAlign = TextAlignEnum.LeftCenter;
                else
                    cfgFileProfile.Cols[i].TextAlign = TextAlignEnum.CenterCenter;
                cfgFileProfile.Rows[0].TextAlign = TextAlignEnum.CenterCenter;
                // định dạng và filter
                if (i == 8 || i == 9)
                {
                    cfgFileProfile.Cols[i].Format = "dd/MM/yyyy";
                    cfgFileProfile.Cols[i].AllowFiltering = AllowFiltering.ByCondition;
                }
                else if(i != 0)
                    cfgFileProfile.Cols[i].AllowFiltering = AllowFiltering.ByValue;
                // ẩn các cột ID
                if (i == 1 || i == 2 || i == 3 || i == 5)
                    cfgFileProfile.Cols[i].Visible = false;
            }

            //Merge
            cfgFileProfile.AllowMerging = AllowMergingEnum.RestrictRows;
            cfgFileProfile.Cols[4].AllowMerging = true;
            cfgFileProfile.AutoGenerateColumns = false;
        }

        private void LoadData()
        {
            cfgFileProfile.DataSource = _bus.FL_FileProfile_SelectBy_Date_Search(chkSelectAll.Checked ? DateTime.MinValue : dpkDate.Value.Date, txtSeach.Text);
        }
    }
}

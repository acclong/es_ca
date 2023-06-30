using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls.UI;
using ES.CA_ManagementBUS;
using Telerik.WinControls;
using C1.Win.C1FlexGrid;

namespace ES.CA_ManagementUI
{
    public partial class ucDanhMucLoaiVanBan : UserControl
    {
        BUSQuanTri _bus = new BUSQuanTri();

        public ucDanhMucLoaiVanBan()
        {
            InitializeComponent();
        }

        private void ucDanhMucLoaiVanBan_Load(object sender, EventArgs e)
        {
            try
            {
                dpkDate.Value = DateTime.Now;
                LoadData();
                InitrgvFileType();
                clsShare.FormatWidthComboBoxInPanel(pnlHeader);

                // Thêm sự kiện KeyDown
                cfgTypeFile.KeyDown += new KeyEventHandler(clsShare.C1FlexGrid_KeyDown);
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
                frmThemSuaLoaiVanBan frm = new frmThemSuaLoaiVanBan();
                frm.FileTypeId = -1;
                frm.ShowDialog();

                // load lại dữ liệu
                LoadData();
                InitrgvFileType();
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
                if (cfgTypeFile.Row == cfgTypeFile.RowSel)
                {
                    frmThemSuaLoaiVanBan frm = new frmThemSuaLoaiVanBan();
                    frm.FileTypeId = Convert.ToInt32(cfgTypeFile.Rows[cfgTypeFile.Row]["FileTypeID"]);
                    frm.ShowDialog();

                    // load lại dữ liệu
                    LoadData();
                    InitrgvFileType();
                }
            }
            catch (Exception ex)
            {
                clsShare.Message_Error(ex);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (cfgTypeFile.Row == cfgTypeFile.RowSel)
                {
                    int iFileTypeId = Convert.ToInt32(cfgTypeFile.Rows[cfgTypeFile.Row]["FileTypeID"]);
                    //Edited by Toantk on 23/4/2015
                    //Chuyển kiểm tra điều kiện vào lớp Business
                    if (_bus.FL_File_LoaiFileDangSuDung(iFileTypeId))
                    {
                        clsShare.Message_Warning("Không thể xóa do loại văn bản đã được sử dụng!");
                        return;
                    }
                    if (clsShare.Message_WarningYN("Bạn có chắc chắn XÓA bản ghi này không?"))
                    {
                        _bus.FL_FileType_DeleteByFileTypeID(iFileTypeId);
                        LoadData();
                        InitrgvFileType();
                    }
                }
            }
            catch (Exception ex)
            {
                clsShare.Message_Error(ex);
            }
        }

        private void cfgTypeFile_DoubleClick(object sender, EventArgs e)
        {
            btnEdit_Click(sender, e);
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                LoadData();
                InitrgvFileType();
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

        private void InitrgvFileType()
        {
            //Thêm trường STT và ẩn cột ID
            string[] arrName = { "", "FileTypeID", "Name", "Notation", "UnitType", "UnitName",
                                   "DateType", "DateStart", "DateEnd" };
            string[] arrCaption = { "", "ID", "Tên loại văn bản", "Ký hiệu", "UnitType", "Loại đơn vị",
                                      "Loại thời gian", "Ngày áp dụng", "Ngày kết thúc" };

            for (int i = 0; i < arrName.Length; i++)
            {
                // tên và header
                cfgTypeFile.Cols[i].Name = arrName[i];
                cfgTypeFile.Cols[i].Caption = arrCaption[i];
                // căn lề
                if (i == 1 || i == 5 || i == 6 || i == 7 || i == 8)
                    cfgTypeFile.Cols[i].TextAlign = TextAlignEnum.CenterCenter;
                else
                    cfgTypeFile.Cols[i].TextAlign = TextAlignEnum.LeftCenter;
                cfgTypeFile.Rows[0].TextAlign = TextAlignEnum.CenterCenter;
                // kích thước cột
                switch (i)
                {
                    case 0: cfgTypeFile.Cols[i].Width = 25; break;
                    case 1: cfgTypeFile.Cols[i].Width = 50; break;
                    case 2: cfgTypeFile.Cols[i].Width = 300; break;
                    case 3: 
                    case 5:
                    case 6:
                    case 7:
                    case 8: cfgTypeFile.Cols[i].Width = 120; break;
                }
                // format cột
                if (i == 7 || i == 8)
                {
                    cfgTypeFile.Cols[i].Format = "dd/MM/yyyy";
                    cfgTypeFile.Cols[i].AllowFiltering = AllowFiltering.ByCondition;
                }
                else if (i != 0)
                    cfgTypeFile.Cols[i].AllowFiltering = AllowFiltering.ByValue;

                if (i == 4)
                    cfgTypeFile.Cols[i].Visible = false;
            }
        }

        private void LoadData()
        {
            // lấy dữ liệu từ database
            cfgTypeFile.DataSource = _bus.FL_FileType_SelectByDateSearch(chkSelectAll.Checked ? DateTime.MinValue : dpkDate.Value.Date, txtSeach.Text.Trim());
        }
    }
}

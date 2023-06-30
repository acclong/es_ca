using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ES.CA_ManagementBUS;
using C1.Win.C1FlexGrid;

namespace ES.CA_ManagementUI
{
    public partial class ucDanhMucLoaiHoSoNew : UserControl
    {
        #region Var
        private BUSQuanTri _bus = new BUSQuanTri();
        #endregion

        public ucDanhMucLoaiHoSoNew()
        {
            InitializeComponent();
        }
        private void ucDanhMucLoaiHoSoNew_Load(object sender, EventArgs e)
        {
            try
            {
                LoadDataFileType();
                //InitCbo();
                InitCfgProfileType();
                InitCfgFileProfile();
                // chỉnh sửa kích thước panel
                clsShare.FormatWidthComboBoxInPanel(pnlHeader);

                //Thêm sửa KeyDown
                cfgFileProfile.KeyDown += new KeyEventHandler(clsShare.C1FlexGrid_KeyDown);
                cfgTypeProfile.KeyDown += new KeyEventHandler(clsShare.C1FlexGrid_KeyDown);
            }
            catch (Exception ex)
            {
                clsShare.Message_Error(ex.Message);
            }
        }

        #region Init
        private void InitCfgProfileType()
        {
            //cấu hình cột
            cfgTypeProfile.ExtendLastCol = true;
            cfgTypeProfile.Cols.Fixed = 1;
            cfgTypeProfile.AllowSorting = AllowSortingEnum.SingleColumn;

            //Thêm trường STT và ẩn cột ID
            string[] arrName = { "", "STT", "ProfileTypeID", "Name", "UnitType", "UnitName", "DateType", "DateStart", "DateEnd","DateTypeValue" };
            string[] arrCaption = { "", "STT", "ID", "Tên loại Hồ sơ",  "UnitType", "Loại đơn vị", "Loại thời gian", "Ngày áp dụng", "Ngày kết thúc","DateTypeValue" };

            #region For
            for (int i = 0; i < arrName.Length; i++)
            {
                // tên và header
                cfgTypeProfile.Cols[i].Name = arrName[i];
                cfgTypeProfile.Cols[i].Caption = arrCaption[i];
                cfgTypeProfile.Cols[i].TextAlignFixed = TextAlignEnum.CenterCenter;

                // căn lề
                if (i == 3)
                    cfgTypeProfile.Cols[i].TextAlign = TextAlignEnum.LeftCenter;
                else
                    cfgTypeProfile.Cols[i].TextAlign = TextAlignEnum.CenterCenter;
                // kích thước cột
                switch (i)
                {
                    case 0: cfgTypeProfile.Cols[i].Width = 25; break;
                    case 1: cfgTypeProfile.Cols[i].Width = 40; break;
                    case 3: cfgTypeProfile.Cols[i].Width = 150; break;
                    case 5:
                    case 6: cfgTypeProfile.Cols[i].Width = 100; break;
                    case 7:
                    case 8: cfgTypeProfile.Cols[i].Width = 100; break;
                }
                // format cột
                if (i == 7 || i == 8)
                {
                    cfgTypeProfile.Cols[i].Format = "dd/MM/yyyy";
                    cfgTypeProfile.Cols[i].AllowFiltering = AllowFiltering.ByCondition;
                }
                else if (i != 0)
                    cfgTypeProfile.Cols[i].AllowFiltering = AllowFiltering.ByValue;

                // ẩn cột
                if (i == 2 || i == 4 || i == 9)
                    cfgTypeProfile.Cols[i].Visible = false;

                // Edit
                if (i == 1)
                {
                    cfgTypeProfile.Cols[i].AllowEditing = false;
                }
                else
                {
                    cfgTypeProfile.Cols[i].AllowEditing = true;
                }
            }
            #endregion

            cfgTypeProfile.AutoGenerateColumns = false;
        }

        private void InitCfgFileProfile()
        {
            cfgFileProfile.AllowMerging = AllowMergingEnum.RestrictRows;

            //Tên cột
            string[] arrName = {"", "STT", "ID_FileProfile", "ProfileTypeID", "ProfileType",
                                   "FileTypeID", "FileType", "Notation", "DateStart", "DateEnd"};
            string[] arrCaption = {"", "STT", "ID_FileProfile", "ProfileTypeID", "Loại hồ sơ",
                                   "FileTypeID", "Loại văn bản", "Ký hiệu văn bản", "Ngày áp dụng", "Ngày kết thúc" };

            #region For
            for (int i = 0; i < arrName.Length; i++)
            {
                // Tên và Header
                cfgFileProfile.Cols[i].Name = arrName[i];
                cfgFileProfile.Cols[i].Caption = arrCaption[i];
                switch (i)
                {
                    case 0: cfgFileProfile.Cols[i].Width = 25; break;
                    case 1: cfgFileProfile.Cols[i].Width = 40; break;
                    case 6: cfgFileProfile.Cols[i].Width = 200; break;
                    case 4:
                    case 7: cfgFileProfile.Cols[i].Width = 100; break;
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
                else if (i != 0)
                    cfgFileProfile.Cols[i].AllowFiltering = AllowFiltering.Default;

                // ẩn các cột ID
                if (i == 2 || i == 3 || i == 4 || i == 5)
                    cfgFileProfile.Cols[i].Visible = false;

                switch (arrName[i])
                {
                    case "ProfileType":
                        cfgFileProfile.Cols[i].AllowMerging = true;
                        break;
                    default:
                        cfgFileProfile.Cols[i].AllowMerging = false;
                        break;
                }
            }
            #endregion

            cfgFileProfile.Cols["FileType"].Width = 200;
            cfgFileProfile.AutoGenerateColumns = false;
        }
        #endregion

        #region Data
        private void LoadDataFileType()
        {
            DataTable dt = _bus.FL_ProfileType_SelectByDateSearch(chkSelectAll.Checked ? DateTime.MinValue : dpkDate.Value.Date, txtSeach.Text);
            cfgTypeProfile.DataSource = dt;
            cfgTypeProfile_Click(null, null);
        }
        #endregion

        #region Events
        private void cfgTypeProfile_Click(object sender, EventArgs e)
        {
            try
            {
                if (cfgTypeProfile.Row > 0)
                {
                    int ProfileTypeID = Convert.ToInt32(cfgTypeProfile.Rows[cfgTypeProfile.Row]["ProfileTypeID"]);
                    DataTable dtFileProfile = _bus.FL_FileProfile_SelectByProfileTypeID_Date_Search(ProfileTypeID, chkSelectAll.Checked ? DateTime.MinValue : dpkDate.Value.Date, "");
                    cfgFileProfile.DataSource = dtFileProfile;
                }
            }
            catch (Exception ex)
            {
                clsShare.Message_Error(ex.Message);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                LoadDataFileType();
            }
            catch (Exception ex)
            {
                clsShare.Message_Error(ex.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (cfgTypeProfile.Row > 0)
                {
                    int iProfileTypeID = Convert.ToInt32(cfgTypeProfile.Rows[cfgTypeProfile.Row]["ProfileTypeID"].ToString());
                    //ToanTK 24/5/2016: Bỏ phần edit trên grid
                    if (!_bus.FL_FileProfile_LoaiHoSoDangSuDung(iProfileTypeID))
                    {
                        if (clsShare.Message_QuestionYN("Bạn có chắc chắn muốn xóa bản ghi này không?"))
                        {
                            if (!_bus.FL_ProfileType_DeleteByProfileTypeID(iProfileTypeID))
                            {
                                clsShare.Message_Error("Xảy ra lỗi trong quá trình xóa bản ghi.");
                            }
                            else
                            {
                                clsShare.Message_Info("Xóa bản ghi thành công!");
                                btnRefresh_Click(null, null);
                            }
                        }
                    }
                    else
                    {
                        clsShare.Message_Warning("Không thể xóa do loại hồ sơ đang được sử dụng!");
                    }
                }
            }
            catch (Exception ex)
            {
                clsShare.Message_Error(ex.Message);
            }
        }

        //Toantk 24/5/2016: sửa lại bắn form thêm sửa, ko edit trực tiếp trên grid
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                frmThemSuaLoaiHoSo frm = new frmThemSuaLoaiHoSo();
                frm.ProfileTypeId = -1;
                frm.ShowDialog();

                // load lại dữ liệu
                btnRefresh_Click(null, null);
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
                if (cfgTypeProfile.Row == cfgTypeProfile.RowSel)
                {
                    frmThemSuaLoaiHoSo frm = new frmThemSuaLoaiHoSo();
                    frm.ProfileTypeId = Convert.ToInt32(cfgTypeProfile.Rows[cfgTypeProfile.Row]["ProfileTypeId"]);
                    frm.ShowDialog();

                    // load lại dữ liệu
                    btnRefresh_Click(null, null);
                }
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
        #endregion

    }
}

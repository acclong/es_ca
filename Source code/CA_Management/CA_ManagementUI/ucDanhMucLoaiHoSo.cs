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
    public partial class ucDanhMucLoaiHoSo : UserControl
    {
        BUSQuanTri _bus = new BUSQuanTri();

        public ucDanhMucLoaiHoSo()
        {
            InitializeComponent();
        }

        private void ucDanhMucLoaiHoSo_Load(object sender, EventArgs e)
        {
            try
            {
                LoadData();
                InitrgvProfileType();

                cfgTypeProfile.KeyDown += new KeyEventHandler(clsShare.C1FlexGrid_KeyDown);
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
                frmThemSuaLoaiHoSo frm = new frmThemSuaLoaiHoSo();
                frm.ProfileTypeId = -1;
                frm.ShowDialog();

                // load lại dữ liệu
                LoadData();
                InitrgvProfileType();
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
                    frm.ProfileTypeId = Convert.ToInt32(cfgTypeProfile.Rows[cfgTypeProfile.Row]["ProfileTypeID"]);
                    frm.ShowDialog();

                    LoadData();
                    InitrgvProfileType();
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
                if (cfgTypeProfile.Row == cfgTypeProfile.RowSel)
                {
                    int iProfileTypeId = Convert.ToInt32(cfgTypeProfile.Rows[cfgTypeProfile.Row]["ProfileTypeID"]);
                    //Edited by Toantk on 23/4/2015
                    //Chuyển kiểm tra điều kiện vào lớp Business
                    if (_bus.FL_FileProfile_LoaiHoSoDangSuDung(iProfileTypeId))
                    {
                        clsShare.Message_Warning("Không thể xóa do loại hồ sơ đã được sử dụng!");
                        return;
                    }
                    if (clsShare.Message_WarningYN("Bạn có chắc chắn XÓA bản ghi này không?"))
                    {
                        _bus.FL_ProfileType_DeleteByProfileTypeID(iProfileTypeId);

                        LoadData();
                        InitrgvProfileType();
                    }
                }
            }
            catch (Exception ex)
            {
                clsShare.Message_Error(ex);
            }
        }

        private void cfgTypeProfile_DoubleClick(object sender, EventArgs e)
        {
            btnEdit_Click(sender, e);
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                LoadData();
                InitrgvProfileType();
            }
            catch (Exception ex)
            {
                clsShare.Message_Error(ex);
            }
        }

        private void InitrgvProfileType()
        {
            //Thêm trường STT và ẩn cột ID
            string[] arrName = { "", "STT", "ProfileTypeID", "Name", "UnitType", "UnitName",
                                   "DateType", "DateStart", "DateEnd","DateTypeValue" };
            string[] arrCaption = { "", "STT", "ID", "Tên loại Hồ sơ",  "UnitType", "Loại đơn vị",
                                      "Loại thời gian", "Ngày áp dụng", "Ngày kết thúc","DateTypeValue" };

            for (int i = 0; i < arrName.Length; i++)
            {
                // tên và header
                cfgTypeProfile.Cols[i].Name = arrName[i];
                cfgTypeProfile.Cols[i].Caption = arrCaption[i];
                // căn lề
                if (i == 3)
                    cfgTypeProfile.Cols[i].TextAlign = TextAlignEnum.LeftCenter;
                else
                    cfgTypeProfile.Cols[i].TextAlign = TextAlignEnum.CenterCenter;
                cfgTypeProfile.Rows[0].TextAlign = TextAlignEnum.CenterCenter;
                // kích thước cột
                switch (i)
                {
                    case 0: cfgTypeProfile.Cols[i].Width = 25; break;
                    case 1: cfgTypeProfile.Cols[i].Width = 40; break;
                    case 3: cfgTypeProfile.Cols[i].Width = 300; break;
                    case 5:
                    case 6: cfgTypeProfile.Cols[i].Width = 200; break;
                    case 7:
                    case 8: cfgTypeProfile.Cols[i].Width = 150; break;
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
            }
        }

        private void LoadData()
        {
            // lấy dữ liệu từ database
            cfgTypeProfile.DataSource = _bus.FL_ProfileType_SelectByDateSearch(dpkDate.Value.Date, txtSeach.Text.Trim());
        }

        #region code cũ
        //private void btnEdit_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        frmThemSuaLoaiHoSo frm = new frmThemSuaLoaiHoSo();
        //        if (rgvProfileType.MasterView.CurrentRow != null)
        //        {
        //            frm.ProFileTypeId = Convert.ToInt32(rgvProfileType.CurrentRow.Cells["ProfileTypeID"].Value);
        //            frm.ShowDialog();

        //            // load lại dữ liệu
        //            LoadData();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        clsShare.Message_Error(ex);
        //    }
        //}

        //private void rgvProfileType_DoubleClick(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (rgvProfileType.MasterView.CurrentRow != null)
        //        {
        //            btnEdit_Click(null, null);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        clsShare.Message_Error(ex);
        //    }
        //}

        //private void btnDelete_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (rgvProfileType.MasterView.CurrentRow != null)
        //        {
        //            int iProfileTypeId = Convert.ToInt32(rgvProfileType.CurrentRow.Cells["ProfileTypeID"].Value);
        //            //Edited by Toantk on 23/4/2015
        //            //Chuyển kiểm tra điều kiện vào lớp Business
        //            if (_bus.FL_FileProfile_LoaiHoSoDangSuDung(iProfileTypeId))
        //            {
        //                clsShare.Message_Warning("Không thể xóa do loại hồ sơ đã được sử dụng!");
        //                return;
        //            }
        //            if (clsShare.Message_WarningYN("Bạn có chắc chắn XÓA bản ghi này không?"))
        //            {
        //                _bus.FL_ProfileType_DeleteByProfileTypeID(iProfileTypeId);
        //                LoadData();
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        clsShare.Message_Error(ex);
        //    }
        //}
        //private void InitrgvProfileType()
        //{
        //    // cấu hình radGrid
        //    rgvProfileType.MasterTemplate.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;
        //    rgvProfileType.AllowEditRow = false;
        //    rgvProfileType.EnableFiltering = true;
        //    rgvProfileType.MasterTemplate.ShowHeaderCellButtons = true;
        //    rgvProfileType.MasterTemplate.ShowFilteringRow = false;

        //    //Thêm trường STT và ẩn cột ID
        //    string[] arrName = { "STT", "ProfileTypeID", "Name", "DateStart", "DateEnd" };
        //    string[] arrHeader = { "STT", "ID", "Tên loại Hồ sơ", "Ngày áp dụng", "Ngày kết thúc" };

        //    for (int i = 0; i < arrName.Length; i++)
        //    {
        //        // tên và header
        //        rgvProfileType.Columns[i].Name = arrName[i];
        //        rgvProfileType.Columns[i].HeaderText = arrHeader[i];
        //        // căn lề
        //        if (i == 2 )
        //            rgvProfileType.Columns[i].TextAlignment = ContentAlignment.MiddleLeft;
        //        else
        //            rgvProfileType.Columns[i].TextAlignment = ContentAlignment.MiddleCenter;
        //        // kích thước cột
        //        switch (i)
        //        {
        //            case 0: rgvProfileType.Columns[i].Width = 7; break;
        //            case 3:
        //            case 4: rgvProfileType.Columns[i].Width = 20; break;
        //        }
        //        // format cột
        //        if (i == 3 || i == 4)
        //            rgvProfileType.Columns[i].FormatString = "{0: dd/MM/yyyy}";
        //        // ẩn cột
        //        if (i == 1)
        //            rgvProfileType.Columns[i].IsVisible = false;

        //        if (i == 0)
        //            rgvProfileType.Columns[i].AllowFiltering = false;
        //    }
        //}

        //private void LoadData()
        //{
        //    // lấy dữ liệu từ database
        //    rgvProfileType.DataSource = _bus.FL_ProfileType_SelectAll();
        //}
        #endregion
    }
}

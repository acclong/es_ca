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
using C1.Win.C1FlexGrid;

namespace ES.CA_ManagementUI
{
    public partial class ucDanhMucLoaiVBLienQuan : UserControl
    {
        BUSQuanTri _bus = new BUSQuanTri();

        public ucDanhMucLoaiVBLienQuan()
        {
            InitializeComponent();
        }

        private void ucDanhMucLoaiVBLienQuan_Load(object sender, EventArgs e)
        {
            try
            {
                LoadData();
                InitrgvRelationType();
                clsShare.FormatWidthComboBoxInPanel(pnlHeader);

                cfgFileRelationType.KeyDown += new KeyEventHandler(clsShare.C1FlexGrid_KeyDown);
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
                frmThemSuaLoaiFileLienQuan frm = new frmThemSuaLoaiFileLienQuan();
                frm.RelationTypeId = -1;
                frm.ShowDialog();

                // load lại dữ liệu
                LoadData();
                InitrgvRelationType();
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
                frmThemSuaLoaiFileLienQuan frm = new frmThemSuaLoaiFileLienQuan();
                if (cfgFileRelationType.Row == cfgFileRelationType.RowSel)
                {
                    frm.RelationTypeId = Convert.ToInt32(cfgFileRelationType.Rows[cfgFileRelationType.Row]["RelationTypeID"]);
                    frm.ShowDialog();

                    // load lại dữ liệu
                    LoadData();
                    InitrgvRelationType();
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
                if (cfgFileRelationType.Row == cfgFileRelationType.RowSel)
                {
                    int iRelationTypeId = Convert.ToInt32(cfgFileRelationType.Rows[cfgFileRelationType.Row]["RelationTypeID"]);
                    //Edited by Toantk on 23/4/2015
                    //Chuyển kiểm tra điều kiện vào lớp Business
                    if (_bus.FL_FileRelation_LoaiQuanHeDangSuDung(iRelationTypeId))
                    {
                        clsShare.Message_Warning("Không thể xóa do loại văn bản liên quan đã được sử dụng!");
                        return;
                    }
                    if (clsShare.Message_WarningYN("Bạn có chắc chắn XÓA bản ghi này không?"))
                    {
                        _bus.FL_RelationType_DeleteByRelationTypeID(iRelationTypeId);
                        LoadData();
                        InitrgvRelationType();
                    }
                }
            }
            catch (Exception ex)
            {
                clsShare.Message_Error(ex);
            }
        }

        private void cfgFileRelationType_DoubleClick(object sender, EventArgs e)
        {
            btnEdit_Click(sender, e);
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                LoadData();
                InitrgvRelationType();
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

        private void InitrgvRelationType()
        {
            //Thêm trường STT và ẩn cột ID
            string[] arrName = {"", "STT", "RelationTypeID", "Name", "DateStart", "DateEnd" };
            string[] arrCaption = {"", "STT", "ID", "Tên loại liên quan", "Ngày áp dụng", "Ngày kết thúc" };

            for (int i = 0; i < arrName.Length; i++)
            {
                // tên và header
                cfgFileRelationType.Cols[i].Name = arrName[i];
                cfgFileRelationType.Cols[i].Caption = arrCaption[i];
                // căn lề
                if (i == 3)
                    cfgFileRelationType.Cols[i].TextAlign = TextAlignEnum.LeftCenter;
                else
                    cfgFileRelationType.Cols[i].TextAlign = TextAlignEnum.CenterCenter;
                cfgFileRelationType.Rows[0].TextAlign = TextAlignEnum.CenterCenter;
                // kích thước cột
                switch (i)
                {
                    case 0: cfgFileRelationType.Cols[i].Width = 25; break;
                    case 1: cfgFileRelationType.Cols[i].Width = 40; break;
                    case 3: cfgFileRelationType.Cols[i].Width = 350; break;
                    case 4:
                    case 5: cfgFileRelationType.Cols[i].Width = 150; break;
                }
                // format cột
                if (i == 4 || i == 5)
                {
                    cfgFileRelationType.Cols[i].Format = "dd/MM/yyyy";
                    cfgFileRelationType.Cols[i].AllowFiltering = AllowFiltering.ByCondition;
                }
                else if (i != 0)
                    cfgFileRelationType.Cols[i].AllowFiltering = AllowFiltering.ByValue;
                // ẩn cột
                if (i == 2)
                    cfgFileRelationType.Cols[i].Visible = false;
            }
        }

        private void LoadData()
        {
            // lấy dữ liệu từ database
            cfgFileRelationType.DataSource = _bus.FL_RelationType_SelectByDateSearch(chkSelectAll.Checked ? DateTime.MinValue : dpkDate.Value.Date, txtSeach.Text.Trim());
        }
    }
}

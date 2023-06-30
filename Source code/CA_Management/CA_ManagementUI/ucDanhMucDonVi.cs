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
using C1.Win.C1FlexGrid;
using Telerik.WinControls.Data;

namespace ES.CA_ManagementUI
{
    public partial class ucDanhMucDonVi : UserControl
    {
        public ucDanhMucDonVi()
        {
            InitializeComponent();
        }

        private void ucDanhMucDonVi_Load(object sender, EventArgs e)
        {
            try
            {
                AddContextMenu_C1FlexGrid(ref cfgDonVi);
                LoadData();
                InitrgvDonVi();
                InitcboTrangThai();
                InitcboLoaiDonVi();

                cfgDonVi.KeyDown += new KeyEventHandler(clsShare.C1FlexGrid_KeyDown);
            }
            catch (Exception ex)
            {
                clsShare.Message_Error(ex);
            }
        }

        #region Var

        private ContextMenuStrip _contextMenu = new ContextMenuStrip();
        private ToolStripMenuItem _tspItem;

        #endregion

        #region Init

        private void InitcboLoaiDonVi()
        {
            BUSQuanTri bus = new BUSQuanTri();
            DataTable dt = bus.CA_UnitType_SelectAll();

            DataRow dr = dt.NewRow();
            dr["UnitTypeID"] = -1;
            dr["Name"] = "Tất cả";
            dt.Rows.InsertAt(dr, 0);

            cboLoaiDonVi.DataSource = dt;
            cboLoaiDonVi.DisplayMember = "Name";
            cboLoaiDonVi.ValueMember = "UnitTypeID";
            cboLoaiDonVi.SelectedIndex = 0;
        }

        private void InitcboTrangThai()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("Name", typeof(string));

            DataRow dr = dt.NewRow();
            dr["ID"] = -1;
            dr["Name"] = "Tất cả";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["ID"] = 1;
            dr["Name"] = "Hiệu Lực";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["ID"] = 0;
            dr["Name"] = "Không hiệu lực";
            dt.Rows.Add(dr);

            cboTrangThai.DataSource = dt;
            cboTrangThai.DisplayMember = "Name";
            cboTrangThai.ValueMember = "ID";

            cboTrangThai.SelectedIndex = 0;
        }

        private void InitrgvDonVi()
        {

            cfgDonVi.ExtendLastCol = true;
            cfgDonVi.Cols.Fixed = 1;
            cfgDonVi.Cols[0].Width = 25;
            cfgDonVi.AllowMerging = AllowMergingEnum.RestrictRows;
            cfgDonVi.AllowSorting = AllowSortingEnum.SingleColumn;

            string[] arrName = { "STT", "UnitType", "MaDV", "Name", "Notation", "Status", "StatusName", "ValidFrom",
                                   "ValidTo", "UnitTypeID", "UnitID", "ParentID", "ParentNotation", "MienId" };
            string[] arrHeader = { "STT", "Loại đơn vị", "Mã đơn vị", "Tên đơn vị", "Ký hiệu", "Status", "Trạng thái", "Ngày có hiệu lực",
                                     "Ngày hết hiệu lực", "UnitTypeID", "UnitID", "ParentID", "Đơn vị cha", "Miền" };

            for (int j = 0; j < arrName.Length; j++)
            {
                int i = j + 1;
                
                #region C1
                // tên và header
                cfgDonVi.Cols[i].Name = arrName[j];
                cfgDonVi.Cols[i].Caption = arrHeader[j];

                // căn lề
                if (i == 1 || i == 3 || i == 7 || i == 8 || i == 9)
                    cfgDonVi.Cols[i].TextAlign = TextAlignEnum.CenterCenter;
                else
                    cfgDonVi.Cols[i].TextAlign = TextAlignEnum.LeftCenter;

                // kích thước cột
                switch (i)
                {
                    case 1: cfgDonVi.Cols[i].Width = 50; break;
                    case 3: cfgDonVi.Cols[i].Width = 100; break;
                    case 4: cfgDonVi.Cols[i].Width = 250; break;
                    case 7: cfgDonVi.Cols[i].Width = 100; break;
                    case 5: cfgDonVi.Cols[i].Width = 100; break;
                    case 8: cfgDonVi.Cols[i].Width = 100; break;
                    case 9: cfgDonVi.Cols[i].Width = 100; break;
                    case 13: cfgDonVi.Cols[i].Width = 100; break;
                    case 2: cfgDonVi.Cols[i].Width = 200; break;
                }
                // format cột
                if (i == 9 || i == 8)
                    cfgDonVi.Cols[i].Format = "dd/MM/yyyy";

                // ẩn các cột không cần thiết
                if (i == 11 || i == 6 || i == 10 || i == 13 || i == 12)
                    cfgDonVi.Cols[i].Visible = false;

                if (i == 8 || i == 9)
                    cfgDonVi.Cols[i].AllowFiltering = AllowFiltering.ByCondition;
                else
                    cfgDonVi.Cols[i].AllowFiltering = AllowFiltering.Default;

                switch (arrName[j])
                {
                    case "UnitType":
                        cfgDonVi.Cols[i].AllowMerging = true;
                        break;
                    default:
                        cfgDonVi.Cols[i].AllowMerging = false;
                        break;
                }

                #endregion
            }

            // căn giừa hàng đầu
            cfgDonVi.Rows[0].TextAlign = TextAlignEnum.CenterCenter;
            //cfgDonVi.Rows[0].Style.Font = new Font("Times New Roman", 11, FontStyle.Bold);

            // gop lai
            #region Gop cot
            //int m = 1;
            //int n = 1;
            //cfgDonVi.AllowMerging = AllowMergingEnum.Custom;
            //for (; m < cfgDonVi.Rows.Count; m++)
            //{
            //    if (cfgDonVi.Rows[m]["UnitType"].ToString() != cfgDonVi.Rows[m - 1]["UnitType"].ToString())
            //    {
            //        if (n != m)
            //        {
            //            cfgDonVi.MergedRanges.Add(cfgDonVi.GetCellRange(n, 2, m - 1, 2));
            //            n = m;
            //        }
            //    }
            //}
            //if (m != n)
            //{
            //    cfgDonVi.MergedRanges.Add(cfgDonVi.GetCellRange(n, 2, m - 1, 2));
            //}
            #endregion

            cfgDonVi.Cols["STT"].Visible = false;
            cfgDonVi.Cols["UnitType"].Visible = true;
        }

        private void AddContextMenu_C1FlexGrid(ref C1FlexGrid c1Grid)
        {
            //Thêm Lệnh
            _tspItem = new ToolStripMenuItem();
            _tspItem.Name = "LogUnit";
            _tspItem.Text = "Lịch sử đơn vị";
            _contextMenu.Items.Add(_tspItem);

            //Add vào grid
            c1Grid.ContextMenuStrip = _contextMenu;
            //Add sự kiện
            _contextMenu.ItemClicked += new ToolStripItemClickedEventHandler(contextMenu_ItemClicked);
        }

        #endregion

        #region Data
        private void LoadData()
        {
            // lấy dữ liệu từ database
            BUSQuanTri bus = new BUSQuanTri();
            cfgDonVi.DataSource = bus.CA_Unit_SelectAll();
        }
        #endregion

        #region Controls

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                frmThemSuaDonVi frm = new frmThemSuaDonVi();
                frm.UnitID = -1;
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
                if (cfgDonVi.Row > 0)
                {
                    frmThemSuaDonVi frm = new frmThemSuaDonVi();

                    frm.UnitID = Convert.ToInt32(cfgDonVi.Rows[cfgDonVi.Row]["UnitID"]);
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

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                BUSQuanTri bus = new BUSQuanTri();
                if (clsShare.Message_WarningYN("Bạn có chắc muốn xóa đơn vị [" + cfgDonVi.Rows[cfgDonVi.Row]["Name"] + "] không?"))
                {
                    if (bus.CA_Unit_DeleteBy_UnitID(Convert.ToInt32(cfgDonVi.Rows[cfgDonVi.Row]["UnitID"])))
                        clsShare.Message_Info("Xóa đơn vị thành công!");
                    else
                        clsShare.Message_Warning("Không thể xóa do đơn vị đã được sử dụng!");
                }
                btnRefresh_Click(null, null);
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
                BUSQuanTri bus = new BUSQuanTri();
                DataTable dt = bus.CA_Unit_SelectBy_UnitTypeID_Status(Convert.ToInt32(cboLoaiDonVi.SelectedValue), Convert.ToInt32(cboTrangThai.SelectedValue));

                #region Lấy thông tin bộ lọc cũ

                //ColumnFilter colFilterForm = new ColumnFilter();
                //if (cfgDonVi.Cols[8].Filter != null)
                //{
                //    colFilterForm.ValueFilter.ShowValues = ((ColumnFilter)(cfgDonVi.Cols["ValidFrom"].Filter)).ValueFilter.ShowValues;
                //}

                //ColumnFilter colFilterTo = new ColumnFilter();
                //if (cfgDonVi.Cols[9].Filter != null)
                //{
                //    colFilterTo = (ColumnFilter)(cfgDonVi.Cols[9].Filter);
                //}

                #endregion

                cfgDonVi.DataSource = dt;
                InitrgvDonVi();

                #region Gộp ô

                //int m = 1;
                //int n = 1;
                //cfgDonVi.AllowMerging = AllowMergingEnum.Custom;
                //for (; m < cfgDonVi.Rows.Count; m++)
                //{
                //    if (cfgDonVi.Rows[m]["UnitType"].ToString() != cfgDonVi.Rows[m - 1]["UnitType"].ToString())
                //    {
                //        if (n != m)
                //        {
                //            cfgDonVi.MergedRanges.Add(cfgDonVi.GetCellRange(n, 2, m - 1, 2));
                //            n = m;
                //        }
                //    }
                //}
                //if (m != n)
                //{
                //    cfgDonVi.MergedRanges.Add(cfgDonVi.GetCellRange(n, 2, m - 1, 2));
                //}

                #endregion

                #region Ân hiển STT, Loại đơn vị

                if (Convert.ToInt32(cboLoaiDonVi.SelectedValue) != -1)
                {
                    cfgDonVi.Cols["STT"].Visible = true;
                    cfgDonVi.Cols["UnitType"].Visible = false;
                }
                else
                {
                    cfgDonVi.Cols["STT"].Visible = false;
                    cfgDonVi.Cols["UnitType"].Visible = true;
                }

                #endregion

                //if (colFilterForm.ValueFilter.ShowValues != null)
                //    cfgDonVi.Cols[8].Filter = colFilterForm;
                //if (colFilterTo.ValueFilter.ShowValues != null)
                //    cfgDonVi.Cols[9].Filter = colFilterTo;

            }
            catch (Exception ex)
            {
                clsShare.Message_Error(ex.Message);
            }
        }

        #endregion

        #region Event

        private void cfgDonVi_DoubleClick(object sender, EventArgs e)
        {
            var ht = cfgDonVi.HitTest(0, 0);
            int x = cfgDonVi.MouseRow;
            //if (ht.Row != -1 )
            if (cfgDonVi.Row == cfgDonVi.RowSel)
            {
                //if (cfgDonVi.RowSel == cfgDonVi.Row)
                btnEdit_Click(null, null);
            }

        }

        private void contextMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            try
            {
                ToolStripItem item = e.ClickedItem;

                C1FlexGrid c1Grid = (C1FlexGrid)((ContextMenuStrip)sender).SourceControl;
                if (c1Grid != null)
                {
                    int ilc = c1Grid.Selection.LeftCol;
                    int irc = c1Grid.Selection.RightCol;
                    int ibr = c1Grid.Selection.BottomRow;
                    int itr = c1Grid.Selection.TopRow;

                    if (ilc == 0 && irc == 0 && ibr == -1 && itr == -1 || ibr != itr || ibr == 0)
                        return;

                    //int iFileID = Convert.ToInt32(cfgFile.Rows[ibr]["FileID"]);

                    //Xử lý khi click vào ô tương ứng
                    if (item.Name == "LogUnit")
                    {
                        frmXemLogDanhMuc frm = new frmXemLogDanhMuc();
                        frm.Text = "Lịch sử log đơn vị";
                        frm.Log = frmXemLogDanhMuc.TypeLog.Unit;
                        int unitID = Convert.ToInt32(cfgDonVi.Rows[ibr]["UnitID"]);
                        frm.UnitID = unitID;
                        frm.ShowDialog();
                    }
                }
            }
            catch (Exception ex)
            {
                clsShare.Message_Error(ex);
            }
        }

        #endregion

    }
}

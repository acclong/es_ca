using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;
using ES.CA_ManagementBUS;

namespace ES.CA_ManagementUI
{
    public partial class ucLienKetDonViHeThong : UserControl
    {
        #region Var
        private BUSQuanTri _bus = new BUSQuanTri();

        private ContextMenuStrip _contextMenu = new ContextMenuStrip();
        private ToolStripMenuItem _tspItem;

        private ComboBox _cbbStatus = new ComboBox();
        private DataTable _OldData = new DataTable();
        private bool _isEdit = false;
        private bool _isUpdate = false;
        private int _rowBefore = 0;
        #endregion

        public ucLienKetDonViHeThong()
        {
            InitializeComponent();
        }

        private void ucLienKetDonViHeThong_Load(object sender, EventArgs e)
        {
            try
            {
                AddContextMenu_C1FlexGrid(ref cfgUnitProg);
                LoadData();
                InitCboTrangThai();
                InitCboHeThong();
                InitCfgUnitProg();
                InitCbbStatus();

                // Thêm sự kiên
                cfgUnitProg.KeyDown += new KeyEventHandler(clsShare.C1FlexGrid_KeyDown);
            }
            catch (Exception ex)
            {
                clsShare.Message_Error(ex.Message);
            }
        }

        #region Init
        private void InitCboHeThong()
        {
            BUSQuanTri bus = new BUSQuanTri();
            DataTable dt = bus.CA_Program_SelectAll();
            DataRow dr = dt.NewRow();
            dr["ProgID"] = -1;
            dr["Name"] = "Tất cả";
            dt.Rows.InsertAt(dr, 0);

            cboHeThong.DataSource = dt;
            cboHeThong.DisplayMember = "Name";
            cboHeThong.ValueMember = "ProgID";
            cboHeThong.SelectedIndex = 0;
        }

        private void InitCboTrangThai()
        {

            DataTable dt = new DataTable();
            dt.Columns.Add("Id", typeof(int));
            dt.Columns.Add("Name", typeof(string));

            DataRow dr = dt.NewRow();
            dr["Id"] = -1;
            dr["Name"] = "Tất cả";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["Id"] = 0;
            dr["Name"] = "Không check CA";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["Id"] = 1;
            dr["Name"] = "Có check CA";
            dt.Rows.Add(dr);

            cboTrangThai.DataSource = dt;
            cboTrangThai.ValueMember = "Id";
            cboTrangThai.DisplayMember = "Name";
        }

        private void InitCfgUnitProg()
        {
            //cfgUnitProg.Clear(ClearFlags.Style);
            //cfgUnitProg.MergedRanges.Clear();

            // cấu hình radGrid
            cfgUnitProg.ExtendLastCol = true;
            cfgUnitProg.Cols.Fixed = 1;
            cfgUnitProg.Cols[0].Width = 25;
            cfgUnitProg.AllowFiltering = true;
            cfgUnitProg.AllowEditing = false;
            cfgUnitProg.AllowMerging = AllowMergingEnum.RestrictRows;
            cfgUnitProg.AllowSorting = AllowSortingEnum.SingleColumn;

            string[] arrName = {"STT", "ID_UnitProgram", "ProgID", "ProgName","UnitID","UnitName","UnitNotation","UnitTypeName" 
                                   ,"Status", "StatusName", "UserModified", "DateModified" };
            string[] arrHeader = {"STT", "ID", "ProgID", "Hệ thống","UnitID","Tên đơn vị","Đơn vị","Loại đơn vị"
                                     , "Type", "Trạng thái", "Người sửa", "Thời gian sửa" };

            #region For

            for (int i = 0; i < arrHeader.Length; i++)
            {
                #region C1

                // tên cột và header
                cfgUnitProg.Cols[i + 1].Name = arrName[i];
                cfgUnitProg.Cols[i + 1].Caption = arrHeader[i];

                // căn lề
                switch (arrName[i])
                {
                    case "STT":
                    case "UserModified":
                    case "DateModified":
                    case "StatusName":
                        cfgUnitProg.Cols[i + 1].TextAlign = TextAlignEnum.CenterCenter;
                        break;
                    default:
                        cfgUnitProg.Cols[i + 1].TextAlign = TextAlignEnum.LeftCenter;
                        break;
                }

                // ẩn các cột
                switch (arrName[i])
                {
                    case "ID_UnitProgram":
                    case "ProgID":
                    case "UnitID":
                    case "UnitName":
                    case "Status":
                        cfgUnitProg.Cols[i + 1].Visible = false;
                        break;
                }

                // tạo Filter và định dạng ngày tháng
                switch (arrName[i])
                {
                    case "DateModified":
                        cfgUnitProg.Cols[i + 1].AllowFiltering = AllowFiltering.ByCondition;
                        cfgUnitProg.Cols[i + 1].Format = "dd/MM/yyyy HH:mm:ss ";
                        break;
                    default:
                        cfgUnitProg.Cols[i + 1].AllowFiltering = AllowFiltering.ByValue;
                        break;
                }

                // Edit
                switch (arrName[i])
                {
                    case "Status":
                    case "StatusName":
                        cfgUnitProg.Cols[i + 1].AllowEditing = true;
                        break;
                    default:
                        cfgUnitProg.Cols[i + 1].AllowEditing = false;
                        break;
                }

                // Merged
                switch (arrName[i])
                {
                    case "ProgName":
                        cfgUnitProg.Cols[i + 1].AllowMerging = true;
                        break;
                    default:
                        cfgUnitProg.Cols[i + 1].AllowMerging = false;
                        break;
                }

                // Editor
                switch (arrName[i])
                {
                    case "StatusName":
                        cfgUnitProg.Cols[i + 1].Editor = _cbbStatus;
                        break;
                }

                #endregion
            }

            #endregion

            // kích thước cột
            cfgUnitProg.Cols["STT"].Width = 50;
            cfgUnitProg.Cols["ProgName"].Width = 200;
            cfgUnitProg.Cols["UnitNotation"].Width = 200;
            cfgUnitProg.Cols["UnitTypeName"].Width = 150;
            cfgUnitProg.Cols["StatusName"].Width = 150;
            cfgUnitProg.Cols["UserModified"].Width = 100;
            cfgUnitProg.Cols["DateModified"].Width = 100;

            // căn giừa hàng đầu
            cfgUnitProg.Rows[0].TextAlign = TextAlignEnum.CenterCenter;

            // gộp hàng các dòng dòng giống nhau
            #region Code cũ
            //cfgUnitProg.AllowMerging = AllowMergingEnum.Custom;
            //int k;
            //int j = 1;
            //for (k = 2; k < cfgUnitProg.Rows.Count; k++)
            //{
            //    if (cfgUnitProg.Rows[k]["ProgName"].ToString() != cfgUnitProg.Rows[k - 1]["ProgName"].ToString())
            //    {
            //        if (j != k)
            //        {
            //            cfgUnitProg.MergedRanges.Add(cfgUnitProg.GetCellRange(j, 4, k - 1, 4));
            //            j = k;
            //        }
            //    }

            //}
            //if (j != k)
            //{
            //    cfgUnitProg.MergedRanges.Add(cfgUnitProg.GetCellRange(j, 4, k - 1, 4));
            //}
            #endregion

            if (Convert.ToInt32(cboHeThong.SelectedValue) == -1)
            {
                cfgUnitProg.Cols["STT"].Visible = false;
                cfgUnitProg.Cols["ProgName"].Visible = true;
            }
            else
            {
                cfgUnitProg.Cols["STT"].Visible = true;
                cfgUnitProg.Cols["ProgName"].Visible = false;
            }

            //them su kien
            //cfgUnitProg.SelChange += cfgUnitProg_SelChange;
        }

        private void AddContextMenu_C1FlexGrid(ref C1FlexGrid c1Grid)
        {
            //Thêm Lệnh
            _tspItem = new ToolStripMenuItem();
            _tspItem.Name = "LogUnitProg";
            _tspItem.Text = "Lịch sử đơn vị - hệ thống";
            _contextMenu.Items.Add(_tspItem);

            //Add vào grid
            c1Grid.ContextMenuStrip = _contextMenu;
            //Add sự kiện
            _contextMenu.ItemClicked += new ToolStripItemClickedEventHandler(contextMenu_ItemClicked);
        }

        private void InitCbbStatus()
        {
            DataTable dtStatus = new DataTable();
            DataColumn dc = new DataColumn("Value", typeof(int));
            dtStatus.Columns.Add(dc);
            dc = new DataColumn("Name", typeof(string));
            dtStatus.Columns.Add(dc);

            DataRow dr = dtStatus.NewRow();
            dr["Value"] = 1;
            dr["Name"] = "Có check CA";
            dtStatus.Rows.Add(dr);

            dr = dtStatus.NewRow();
            dr["Value"] = 0;
            dr["Name"] = "Không check CA";
            dtStatus.Rows.Add(dr);

            _cbbStatus.DataSource = dtStatus;
            _cbbStatus.DisplayMember = "Name";
            _cbbStatus.ValueMember = "Value";
        }
        #endregion

        #region Data
        private void LoadData()
        {
            BUSQuanTri bus = new BUSQuanTri();
            DataTable dt = bus.CA_UnitProgram_SelectBy_ProgID_Status_Seach(-1, "", -1);
            cfgUnitProg.DataSource = dt;
            _isEdit = false;
        }
        #endregion

        #region Controls
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                BUSQuanTri bus = new BUSQuanTri();
                int Status = Convert.ToInt32(cboTrangThai.SelectedValue);
                int ProgID = Convert.ToInt32(cboHeThong.SelectedValue);
                string Seach = txtTimKiem.Text;
                DataTable dt = bus.CA_UnitProgram_SelectBy_ProgID_Status_Seach(Status, Seach, ProgID);
                cfgUnitProg.DataSource = dt;

                InitCfgUnitProg();
            }
            catch (Exception ex)
            {
                clsShare.Message_Error(ex.Message);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                frmThemSuaUnitHeThong frm = new frmThemSuaUnitHeThong();
                frm.ShowDialog();
                LoadData();
            }
            catch (Exception ex)
            {
                clsShare.Message_Error(ex.Message);
            }
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            try
            {
                if (!cfgUnitProg.AllowEditing)
                {
                    btnCapNhat.Text = "Dừng cập nhật";
                    cfgUnitProg.AllowEditing = true;
                    _isUpdate = true;
                    _OldData = (cfgUnitProg.DataSource as DataTable).Copy();
                    //cfgUnitProg.DoubleClick -= cfgUnitProg_DoubleClick;
                    //cfgUnitProg.AfterEdit += cfgUnitProg_AfterEdit;
                    //cfgUnitProg.SelChange += cfgUnitProg_SelChange;
                }
                else
                {
                    cfgUnitProg.AllowEditing = false;
                    _isUpdate = false;

                    btnCapNhat.Text = "Cập nhật";

                    if (clsShare.Message_QuestionYN("Bạn có muốn lưu thay dổi không?"))
                    {
                        if (_bus.CA_UnitProgram_InsertUpdate(_OldData, (DataTable)cfgUnitProg.DataSource, clsShare.sUserName))
                        {
                            clsShare.Message_Info("Cập nhật thành công!");
                        }
                        else
                        {
                            clsShare.Message_Error("Cập nhật thất bại!");
                        }
                        LoadData();
                    }
                }
            }
            catch (Exception ex)
            {
                clsShare.Message_Error(ex.Message);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (cfgUnitProg.Row >= 0)
                {
                    frmThemSuaUnitHeThong frm = new frmThemSuaUnitHeThong();
                    frm.ID_UnitProg = Convert.ToInt32(cfgUnitProg.Rows[cfgUnitProg.Row]["ID_UnitProgram"]);
                    frm.ShowDialog();
                    //cfgUnitProg.SelChange -= cfgUnitProg_SelChange;
                    LoadData();
                    //cfgUnitProg.SelChange += cfgUnitProg_SelChange;
                }
            }
            catch (Exception ex)
            {
                clsShare.Message_Error(ex.Message);
            }
        }
        #endregion

        #region Event
        private void cfgUnitProg_DoubleClick(object sender, EventArgs e)
        {
            if (cfgUnitProg.Row > 0 & cfgUnitProg.Row == cfgUnitProg.RowSel && !_isUpdate)
            {
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
                    if (item.Name == "LogUnitProg")
                    {
                        frmXemLogDanhMuc frm = new frmXemLogDanhMuc();
                        frm.Text = "Lịch sử log đơn vị - hệ thống";
                        frm.Log = frmXemLogDanhMuc.TypeLog.UnitProg;
                        int id_UnitProgram = Convert.ToInt32(cfgUnitProg.Rows[ibr]["ID_UnitProgram"]);
                        frm.ID_UnitProg = id_UnitProgram;
                        frm.ShowDialog();
                    }
                }
            }
            catch (Exception ex)
            {
                clsShare.Message_Error(ex);
            }
        }

        private void cfgUnitProg_SelChange(object sender, EventArgs e)
        {
            try
            {
                if (_rowBefore != 0 && _rowBefore != cfgUnitProg.Row)
                {
                    if (_isEdit)
                    {
                        if (clsShare.Message_QuestionYN("Bạn có muốn lưu thay đổi không?"))
                        {
                            int id_UnitProg = Convert.ToInt32(cfgUnitProg.Rows[_rowBefore]["ID_UnitProgram"]);
                            int progID = Convert.ToInt32(cfgUnitProg.Rows[_rowBefore]["ProgID"]);
                            int unitID = Convert.ToInt32(cfgUnitProg.Rows[_rowBefore]["UnitID"]);
                            int status = Convert.ToInt32(cfgUnitProg.Rows[_rowBefore]["Status"]);

                            // Luu thong tin
                            if (_bus.CA_UnitProgram_InsertUpdate(id_UnitProg, progID, unitID, status, clsShare.sUserName))
                            {
                                clsShare.Message_Info("Cập nhật hệ thống thành công!");
                            }
                            else
                            {
                                clsShare.Message_Error("Cập nhật thất bại!");
                            }
                            //cfgUnitProg.SelChange -= cfgUnitProg_SelChange;
                            LoadData();
                            //cfgUnitProg.SelChange += cfgUnitProg_SelChange;
                        }
                        _isEdit = false;
                    }
                }
                _rowBefore = cfgUnitProg.Row;
            }
            catch (Exception ex)
            {
                clsShare.Message_Error(ex);
            }
        }

        private void cfgUnitProg_AfterEdit(object sender, RowColEventArgs e)
        {
            try
            {
                //if (cfgUnitProg.Cols[e.Row].Name == "StatusName")
                if (cfgUnitProg.Cols[e.Col].Name == "Status" || cfgUnitProg.Cols[e.Col].Name == "StatusName")
                {
                    int oldStatus = Convert.ToInt32(cfgUnitProg.Rows[e.Row]["Status"]);
                    int newStatus = Convert.ToInt32(_cbbStatus.SelectedValue);
                    if (oldStatus != newStatus)
                    {
                        _isEdit = true;
                        cfgUnitProg.Rows[e.Row]["Status"] = newStatus;
                    }
                }
            }
            catch (Exception ex)
            {
                clsShare.Message_Error(ex.Message);
            }
        }
        #endregion

    }
}

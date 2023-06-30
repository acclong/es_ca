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
    public partial class ucLienKetUserHeThong : UserControl
    {
        #region Var
        BUSQuanTri _bus = new BUSQuanTri();

        DataTable _dtUserProgram, _dtQuyenUnit;

        private bool _updating = false;
        private ContextMenuStrip _contextMenu = new ContextMenuStrip();
        private ToolStripMenuItem _tspItem;

        private ComboBox _cbbSignType = new ComboBox();

        private int _id_userprog;
        private bool _hasTrvChange = false;  //Biến theo dõi thay đổi check treeview
        #endregion

        public ucLienKetUserHeThong()
        {
            InitializeComponent();
        }

        private void ucPhanQuyenUserHeThong_Load(object sender, EventArgs e)
        {
            try
            {
                InitCboHeThong();

                LoadData(-1, "");
                //LoadDataTrvUnit();
                LoadDataGridUnit();
                InitCbb();

                InitCfgUserProg();
                InitCfgUnit();

                AddContextMenu_C1FlexGrid(ref cfgUserProg);

                cfgUserProg.KeyDown += new KeyEventHandler(clsShare.C1FlexGrid_KeyDown);
                cfgUnit.KeyDown += new KeyEventHandler(clsShare.C1FlexGrid_KeyDown);
            }
            catch (Exception ex)
            {
                clsShare.Message_Error(ex);
            }
        }

        #region Init

        private void InitCboHeThong()
        {
            DataTable dt = _bus.CA_Program_SelectAll();

            DataRow dr = dt.NewRow();
            dr["ProgID"] = -1;
            dr["Name"] = "Tất cả";
            dt.Rows.InsertAt(dr, 0);

            cboHeThong.DataSource = dt;
            cboHeThong.DisplayMember = "Name";
            cboHeThong.ValueMember = "ProgID";
            cboHeThong.SelectedIndex = 0;
        }

        private void InitCfgUserProg()
        {
            cfgUserProg.SelChange -= cfgUserProg_SelChange;
            //cfgUserProg.Clear();
            cfgUserProg.DataSource = _dtUserProgram;

            // cấu hình radGrid
            cfgUserProg.ExtendLastCol = true;
            cfgUserProg.Cols.Fixed = 1;
            cfgUserProg.Cols[0].Width = 25;
            cfgUserProg.AllowFiltering = true;
            cfgUserProg.AllowMerging = AllowMergingEnum.RestrictRows;
            cfgUserProg.AllowSorting = AllowSortingEnum.SingleColumn;

            //cfgUserProg.MergedRanges.Clear();

            string[] arrName = { "STT", "ID_UserProg", "ProgID", "ProgName", "UserID", "UserName", "UserProgName", "UnitNotation", "ValidFrom", "ValidTo" };
            string[] arrHeader = { "STT", "ID", "ProgID", "Hệ thống", "UserID", "Người dùng", "Tên đăng nhập", "Đơn vị", "Hiệu lực từ", "Hiệu lực đến" };

            #region For
            for (int i = 0; i < arrHeader.Length; i++)
            {
                // tên cột và header
                cfgUserProg.Cols[i + 1].Name = arrName[i];
                cfgUserProg.Cols[i + 1].Caption = arrHeader[i];
                cfgUserProg.Cols[i + 1].TextAlignFixed = TextAlignEnum.CenterCenter;
                cfgUserProg.Cols[i + 1].AllowMerging = true;

                // căn lề
                switch (arrName[i])
                {
                    case "STT":
                    case "ValidFrom":
                    case "ValidTo":
                    case "TypeName":
                        cfgUserProg.Cols[i + 1].TextAlign = TextAlignEnum.CenterCenter;
                        break;
                    default:
                        cfgUserProg.Cols[i + 1].TextAlign = TextAlignEnum.LeftCenter;
                        break;
                }

                // Gộp
                switch (arrName[i])
                {
                    case "ProgName":
                        cfgUserProg.Cols[i + 1].AllowMerging = true;
                        break;
                    default:
                        cfgUserProg.Cols[i + 1].AllowMerging = false;
                        break;
                }

                // ẩn các cột
                switch (arrName[i])
                {
                    case "STT":
                    case "ID_UserProg":
                    case "ProgID":
                    case "UserID":
                    case "UnitID":
                    case "UnitName":
                    case "Type":
                        cfgUserProg.Cols[i + 1].Visible = false;
                        break;
                }

                // tạo Filter và định dạng ngày tháng
                switch (arrName[i])
                {
                    case "ValidFrom":
                    case "ValidTo":
                        cfgUserProg.Cols[i + 1].AllowFiltering = AllowFiltering.ByCondition;
                        cfgUserProg.Cols[i + 1].Format = "dd/MM/yyyy";
                        break;
                    default:
                        cfgUserProg.Cols[i + 1].AllowFiltering = AllowFiltering.Default;
                        break;
                }
            }

            #endregion

            // kích thước cột
            cfgUserProg.Cols["STT"].Width = 50;
            cfgUserProg.Cols["ProgName"].Width = 200;
            cfgUserProg.Cols["UserName"].Width = 150;
            cfgUserProg.Cols["UserProgName"].Width = 100;
            cfgUserProg.Cols["UnitNotation"].Width = 100;
            cfgUserProg.Cols["ValidFrom"].Width = 130;
            cfgUserProg.Cols["ValidTo"].Width = 130;

            // An hien STT va He thong
            if (Convert.ToInt32(cboHeThong.SelectedValue) != -1)
            {
                cfgUserProg.Cols["ProgName"].Visible = false;
                cfgUserProg.Cols["STT"].Visible = true;

                cfgUserProg.Cols["UserName"].Width = 200;
                cfgUserProg.Cols["UserProgName"].Width = 150;
            }
            else
            {
                cfgUserProg.Cols["ProgName"].Visible = true;
                cfgUserProg.Cols["STT"].Visible = false;

                cfgUserProg.Cols["UserName"].Width = 150;
                cfgUserProg.Cols["UserProgName"].Width = 100;
            }

            //Thêm sự kiện
            cfgUserProg.SelChange += cfgUserProg_SelChange;
        }

        private void InitCfgUnit()
        {
            cfgUnit.AllowAddNew = false;
            cfgUnit.AllowEditing = true;
            cfgUnit.AllowFiltering = true;

            string[] arrName = { "UnitTypeID", "UnitTypeName", "UnitID", "UnitMaDV", "UnitName", "UnitNotation", "Check", "SignID","Sign" };
            string[] arrHeader = { "UnitTypeID", "Loại đơn vị", "UnitID", "UnitMaDV", "UnitName", "Đơn vị", " ", "SignID", "Loại chữ ký" };

            #region For
            for (int i = 0; i < arrName.Length; i++)
            {
                // tên cột và header
                cfgUnit.Cols[i + 1].Name = arrName[i];
                cfgUnit.Cols[i + 1].Caption = arrHeader[i];
                cfgUnit.Cols[i + 1].TextAlignFixed = TextAlignEnum.CenterCenter;

                // căn lề
                switch (arrName[i])
                {
                    case "Check":
                    case "Sign":
                        cfgUnit.Cols[i + 1].TextAlign = TextAlignEnum.CenterCenter;
                        break;
                    default:
                        cfgUnit.Cols[i + 1].TextAlign = TextAlignEnum.LeftCenter;
                        break;
                }

                // Sua
                switch (arrName[i])
                {
                    case "SignID":
                    case "Sign":
                    case "Check":
                        cfgUnit.Cols[i + 1].AllowEditing = true;
                        break;
                    default:
                        cfgUnit.Cols[i + 1].AllowEditing = false;
                        break;
                }

                // ẩn các cột
                switch (arrName[i])
                {
                    case "UnitTypeID":
                    case "UnitTypeName":
                    case "UnitID":
                    case "SignID":
                    case "UnitName":
                    case "UnitMaDV":
                        cfgUnit.Cols[i + 1].Visible = false;
                        break;
                }

                // Editor
                switch (arrName[i])
                {
                    case "Sign":
                        cfgUnit.Cols[i + 1].Editor = _cbbSignType;
                        break;

                }
            }
            #endregion

            // kích thước cột
            cfgUnit.Cols["Check"].Width = 35;
            cfgUnit.Cols["UnitTypeName"].Width = 100;
            cfgUnit.Cols["UnitNotation"].Width = 150;
            cfgUnit.Cols["Sign"].Width = 100;

            cfgUnit.Cols["Check"].Move(1);

            //Group theo Loại đơn vị
            cfgUnit.Tree.Column = 1;
            cfgUnit.Subtotal(C1.Win.C1FlexGrid.AggregateEnum.None, 0, 3, 3, 3, "{0}");
            cfgUnit.AutoSizeCols();
        }

        private void InitCbb()
        {
            DataTable dtCbb = _bus.CA_SignatureType_SelectAll();
            _cbbSignType.DataSource = dtCbb;
            _cbbSignType.DisplayMember = "Name";
            _cbbSignType.ValueMember = "SignatureTypeID";
        }

        private void AddContextMenu_C1FlexGrid(ref C1FlexGrid c1Grid)
        {
            //Thêm Lệnh
            _tspItem = new ToolStripMenuItem();
            _tspItem.Name = "LogUserProg";
            _tspItem.Text = "Lịch sử người dùng - hệ thống";
            _contextMenu.Items.Add(_tspItem);

            //Add vào grid
            c1Grid.ContextMenuStrip = _contextMenu;
            //Add sự kiện
            _contextMenu.ItemClicked += new ToolStripItemClickedEventHandler(contextMenu_ItemClicked);
        }
        #endregion

        #region Data
        private void LoadData(int progID, string seach)
        {
            // lấy dữ liệu từ database
            DataSet ds = _bus.CA_UserProgram_SelectBy_ProgID_SignTypeID_Seach(progID, -1, seach);
            _dtUserProgram = ds.Tables[0];
            _dtQuyenUnit = ds.Tables[1];
        }

        #region tree view
        //public void LoadDataTrvUnit()
        //{
        //    DataTable dt_UnitType = _bus.CA_UnitType_SelectAll();
        //    trvUnit.CheckBoxes = true;
        //    foreach (DataRow item in dt_UnitType.Rows)
        //    {
        //        int unitTypeid = Convert.ToInt32(item["UnitTypeID"]);

        //        TreeNode tn = new TreeNode();
        //        tn.Name = unitTypeid.ToString();
        //        tn.Text = item["Name"].ToString();
        //        tn.Checked = false;

        //        DataTable dt_UnitByType = _bus.CA_Unit_SelectBy_UnitTypeID(unitTypeid);

        //        foreach (DataRow item1 in dt_UnitByType.Rows)
        //        {
        //            //TreeNode tnChild = new TreeNode();
        //            //tnChild.Text = item1["Notation"].ToString();
        //            //tnChild.Name = item1["UnitID"].ToString();
        //            //tnChild.Checked = false;
        //            tn.Nodes.Add(item1["UnitID"].ToString(), item1["Notation"].ToString());
        //            tn.Nodes[(tn.Nodes.Count - 1)].Tag = item1["UnitID"].ToString();
        //        }
        //        trvUnit.Nodes.Add(tn);
        //    }
        //}
        #endregion

        public void LoadDataGridUnit()
        {
            DataTable dtUnitFull = _bus.CA_Unit_SelectByAll_UserUnit();
            DataColumn dc = new DataColumn("Check", typeof(bool));
            dc.DefaultValue = false;
            dtUnitFull.Columns.Add(dc);
            dc = new DataColumn("SignID", typeof(int));
            dtUnitFull.Columns.Add(dc);
            dc = new DataColumn("Sign", typeof(string));
            dtUnitFull.Columns.Add(dc);
            dc.DefaultValue = -1;

            cfgUnit.DataSource = dtUnitFull;
        }
        #endregion

        #region Controls
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                frmThemSuaUserHeThong frm = new frmThemSuaUserHeThong();
                frm.ID_UserProg = -1;
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
                if (cfgUserProg.Row == cfgUserProg.RowSel)
                {
                    //// kiểm tra xem có chọn dòng header group
                    //if (rgvUserProg.CurrentRow.GetType() == typeof(GridViewGroupRowInfo))
                    //    return;

                    // show frm
                    frmThemSuaUserHeThong frm = new frmThemSuaUserHeThong();
                    frm.ID_UserProg = Convert.ToInt32(cfgUserProg.Rows[cfgUserProg.Row]["ID_UserProg"]);
                    frm.ShowDialog();

                    //frmThemSuaUserHeThongNew frm = new frmThemSuaUserHeThongNew();
                    //frm.UserID = Convert.ToInt32(cfgUserProg.Rows[cfgUserProg.Row]["UserID"]);
                    //frm.ShowDialog();

                    btnRefresh_Click(null, null);
                }
            }
            catch (Exception ex)
            {
                clsShare.Message_Error(ex);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                int progID = Convert.ToInt32(cboHeThong.SelectedValue);
                string seach = txtTimKiem.Text;

                LoadData(progID, seach);
                InitCfgUserProg();

                //// Lay gia tri cua filter
                //ColumnFilter colFilterTo = new ColumnFilter();
                //if (cfgUserProg.Cols["ValidTo"].Filter != null)
                //{
                //    colFilterTo.ValueFilter.ShowValues = ((ColumnFilter)(cfgUserProg.Cols["ValidTo"].Filter)).ValueFilter.ShowValues;
                //}

                //ColumnFilter colFilterFrom = new ColumnFilter();
                //if (cfgUserProg.Cols["ValidFrom"].Filter != null)
                //{
                //    colFilterFrom.ValueFilter.ShowValues = ((ColumnFilter)(cfgUserProg.Cols["ValidFrom"].Filter)).ValueFilter.ShowValues;
                //}

                // gan du lieu cho grid

                //// gan du fiter
                //if (colFilterTo.ValueFilter.ShowValues != null)
                //    cfgUserProg.Cols["ValidTo"].Filter = colFilterTo;

                //if (colFilterFrom.ValueFilter.ShowValues != null)
                //    cfgUserProg.Cols["ValidFrom"].Filter = colFilterFrom;
            }
            catch (Exception ex)
            {
                clsShare.Message_Error(ex.Message);
            }
        }
        
        #region trê view
        //private void trvUnit_AfterCheck(object sender, TreeViewEventArgs e)
        //{
        //    if (!_updating)
        //    {
        //        _updating = true;
        //        TreeNode node = e.Node;
        //        bool c = node.Checked;

        //        ApplyCheck(node, c);

        //        while (node.Parent != null)
        //        {
        //            node = node.Parent;
        //            node.Checked = c;
        //        }
        //        _updating = false;
        //    }

        //    _hasTrvChange = true;
        //}

        //private void ApplyCheck(TreeNode node, bool c)
        //{
        //    foreach (TreeNode child in node.Nodes)
        //    {
        //        child.Checked = c;
        //        ApplyCheck(child, c);
        //    }
        //}
        #endregion

        private void cfgUserProg_DoubleClick(object sender, EventArgs e)
        {
            if (cfgUserProg.Row == cfgUserProg.RowSel)
            {
                //btnEdit_Click(sender, e);
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
                    if (item.Name == "LogUserProg")
                    {
                        frmXemLogDanhMuc frm = new frmXemLogDanhMuc();
                        frm.Text = "Lịch sử log người dùng - hệ thống";
                        frm.Log = frmXemLogDanhMuc.TypeLog.UserProg;
                        int id_UserProgram = Convert.ToInt32(cfgUserProg.Rows[ibr]["ID_UserProg"]);
                        frm.ID_UserProg = id_UserProgram;
                        frm.ShowDialog();
                    }
                }
            }
            catch (Exception ex)
            {
                clsShare.Message_Error(ex);
            }
        }

        private void cfgUserProg_SelChange(object sender, EventArgs e)
        {
            try
            {
                if (_hasTrvChange && clsShare.Message_QuestionYN("Bạn có muốn lưu thay đổi quyền đơn vị không?"))
                {
                    // Check nhap du lieu
                    //Toantk 21/10/2015: bỏ kiểm tra loại chữ ký vì không cần cập nhật
                    DataRow[] dr_trong = ((DataTable)cfgUnit.DataSource).Select("Check = true");
                    //foreach (DataRow item in dr_trong)
                    //{
                    //    if (item["Sign"].ToString() == "")
                    //    {
                    //        clsShare.Message_Warning("Bạn chưa chọn loại chữ ký cho đơn vị!");
                    //        //e.Cancel = false;
                    //        return;
                    //    }
                    //}

                    // Lưu thay đổi vào bảng _dtQuyenUnit
                    List<TreeNode> listCheck = new List<TreeNode>();
                    List<int> unitID = new List<int>();
                    List<int> unitSign = new List<int>();
                    List<bool> unitEnable = new List<bool>();

                    //Xóa bản ghi hiện tại của _id_userprog
                    string select = "ID_UserProg = " + _id_userprog.ToString();
                    DataRow[] dt_Quyen_remove = _dtQuyenUnit.Select(select);
                    foreach (DataRow item in dt_Quyen_remove)
                    {
                        _dtQuyenUnit.Rows.Remove(item);
                    }

                    // Thêm bản ghi được tích grid
                    DataRow[] drCheck = ((DataTable)cfgUnit.DataSource).Select("Check = true");
                    foreach (DataRow item in drCheck)
                    {
                        DataRow dr = _dtQuyenUnit.NewRow();
                        dr["ID_UserProg"] = _id_userprog;
                        dr["UnitID"] = item["UnitID"];
                        dr["Notation"] = item["UnitNotation"];
                        dr["SignName"] = item["Sign"];
                        dr["SignatureTypeID"] = item["SignID"];
                        _dtQuyenUnit.Rows.Add(dr);
                        unitID.Add(Convert.ToInt32(item["UnitID"]));
                        unitSign.Add(Convert.ToInt32(item["SignID"]));
                        unitEnable.Add(true);
                    }

                    #region Lưu db
                    //Lưu db
                    DataRow[] drRowNew = _dtUserProgram.Select("ID_UserProg=" + _id_userprog);
                    if (drRowNew.Count() > 0)
                    {
                        int userID = Convert.ToInt32(drRowNew[0]["UserID"]);
                        int progID = Convert.ToInt32(drRowNew[0]["ProgID"]);
                        string userProgName = drRowNew[0]["UserProgName"].ToString();
                        DateTime validFrom = drRowNew[0]["ValidFrom"].ToString() != "" ? Convert.ToDateTime(drRowNew[0]["ValidFrom"]) : DateTime.MaxValue;
                        DateTime vaildTo = drRowNew[0]["ValidTo"].ToString() != "" ? Convert.ToDateTime(drRowNew[0]["ValidTo"]) : DateTime.MaxValue;

                        if (_bus.CA_UserProg_UnitPhanQuyen_InsertUpdate_Array(_id_userprog, userID, progID, userProgName, validFrom, vaildTo
                                                             , unitID.ToArray(), unitEnable.ToArray(), unitSign.ToArray(), clsShare.sUserName))
                            clsShare.Message_Info("Cập nhật liên kết người dùng - hệ thống thành công!");
                        else
                            clsShare.Message_Error("Cập nhật liên kết người dùng - hệ thống thất bại!");
                    }
                    #endregion
                }



                //Reset treeview ve false
                DataRow[] dr_romovecheck = ((DataTable)cfgUnit.DataSource).Select("Check = true OR Sign <> ''");
                foreach (DataRow item in dr_romovecheck)
                {
                    item["Check"] = false;
                    item["Sign"] = "";
                    item["SignID"] = -1;
                }

                #region Chon lai ID_UserProg moi
                // Lấy ID_UserProg được chọn
                if (cfgUserProg.Row > 0 && cfgUserProg.Rows.Count > 1)
                {
                    _id_userprog = Convert.ToInt32(cfgUserProg.Rows[cfgUserProg.Row]["ID_UserProg"]);
                    string select = "ID_UserProg = " + _id_userprog.ToString();

                    // Kiem tra neu khong co dong vi thi khong kiem tra tiep
                    DataRow[] dt_Quyen = _dtQuyenUnit.Select(select);
                    if (dt_Quyen.Count() > 0)
                    {
                        // dung grid
                        DataTable dt_IdUserProg = (DataTable)cfgUnit.DataSource;
                        // duyet tung don vi duoc pha  quyen
                        foreach (DataRow item in dt_Quyen)
                        {
                            if (item["UnitID"].ToString() != "")
                            {
                                DataRow[] dr = dt_IdUserProg.Select("UnitID = " + item["UnitID"].ToString());
                                if (dr.Count()>0)
                                {
                                    dr[0]["Check"] = true;
                                    dr[0]["Sign"] = item["SignName"];
                                    dr[0]["SignID"] = item["SignatureTypeID"];
                                }
                            }
                        }
                    }
                }
                //Reset lại thông tin các dòng nhóm đơn vị
                foreach (Node n in cfgUnit.Nodes)
                {
                    n.Row["Sign"] = "";
                }
                #endregion

                //Set lại trạng thái
                _hasTrvChange = false;
            }
            catch (Exception ex)
            {
                clsShare.Message_Error(ex);
            }
        }

        private void cfgUnit_AfterEdit(object sender, RowColEventArgs e)
        {
            try
            {
                _hasTrvChange = true;
                switch (cfgUnit.Cols[e.Col].Name)
                {
                    case "Sign":
                        Row r = cfgUnit.Rows[e.Row];
                        if (r.IsNode)
                        {
                            //Toantk 8/6/2016: Nếu là node nhóm loại đơn vị thì đổi cho tất cả đơn vị cùng loại luôn
                            foreach (DataRow dr in ((DataTable)cfgUnit.DataSource).Rows)
                            {
                                if (dr["UnitTypeName"].ToString() == r.Node.Data.ToString())
                                {
                                    dr["SignID"] = _cbbSignType.SelectedValue;
                                    dr["Sign"] = _cbbSignType.Text;
                                    dr["Check"] = true;
                                }
                            }
                        }
                        else
                        {
                            r["SignID"] = _cbbSignType.SelectedValue;
                            r["Check"] = true;
                        }
                        break;
                    default:
                        break;
                }
            }
            catch(Exception ex)
            {
                clsShare.Message_Error(ex.Message);
            }
        }

        private void lblUpdateBiddingUser_Click(object sender, EventArgs e)
        {
            frmBidding_CapNhatUser frm = new frmBidding_CapNhatUser();
            frm.ShowDialog();
        }
        #endregion
    }
}

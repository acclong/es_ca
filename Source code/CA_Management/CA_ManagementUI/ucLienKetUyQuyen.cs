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
    public partial class ucLienKetUyQuyen : UserControl
    {
        #region Var
        BUSQuanTri _bus = new BUSQuanTri();

        DataTable _dtUserProgram, _dtQuyenUnit;

        private bool _updating = false;
        private ContextMenuStrip _contextMenu = new ContextMenuStrip();
        private ToolStripMenuItem _tspItem;

        private ComboBox _cbbSignType = new ComboBox();

        private int _id_userprog = -1;
        private int _rowBefore = 0;
        private bool _hasTrvChange = false;  //Biến theo dõi thay đổi check treeview
        #endregion
        public ucLienKetUyQuyen()
        {
            InitializeComponent();
        }

        private void ucLienKetUyQuyen_Load(object sender, EventArgs e)
        {
            try
            {
                InitCboNguoiDung();
                InitCboLoaiChuKi();
                InitCbb();

                LoadData(-1);
                LoadDataGridUnit(-1, -1);
                //LoadDataTrvUnit();
                InitCfgUyQuyen();

                InitCfgUnit();

                //AddContextMenu_C1FlexGrid(ref cfgUyQuyen);
                // Thêm sự kiện keydown
                cfgUyQuyen.KeyDown += new KeyEventHandler(clsShare.C1FlexGrid_KeyDown);
                cfgUnit.KeyDown += new KeyEventHandler(clsShare.C1FlexGrid_KeyDown);
            }
            catch (Exception ex)
            {
                clsShare.Message_Error(ex);
            }
        }

        #region Init
        private void InitCboLoaiChuKi()
        {
            DataTable dt = _bus.CA_SignatureType_SelectAll();

            DataRow dr = dt.NewRow();
            dr["SignatureTypeID"] = -1;
            dr["Name"] = "Tất cả";
            dt.Rows.InsertAt(dr, 0);
        }

        private void InitCboNguoiDung()
        {
            DataTable dt = _bus.CA_User_SelectAllWithDate();

            DataRow dr = dt.NewRow();
            dr["UserID"] = -1;
            dr["Name"] = "Tất cả";
            dt.Rows.InsertAt(dr, 0);

            cboNguoiDung.DataSource = dt;
            cboNguoiDung.SelectedIndex = 0;
            cboNguoiDung.DisplayMember = "Name";
            cboNguoiDung.ValueMember = "UserID";
        }

        private void InitCfgUyQuyen()
        {
            cfgUyQuyen.SelChange -= cfgUyQuyen_SelChange;
            //cfgUserProg.Clear();
            cfgUyQuyen.DataSource = _dtUserProgram;

            // cấu hình radGrid
            cfgUyQuyen.ExtendLastCol = true;
            cfgUyQuyen.Cols.Fixed = 1;
            cfgUyQuyen.Cols[1].Width = 25;
            cfgUyQuyen.AllowFiltering = true;
            cfgUyQuyen.AllowMerging = AllowMergingEnum.RestrictCols;
            cfgUyQuyen.AllowMergingFixed = AllowMergingEnum.RestrictCols;
            cfgUyQuyen.AllowSorting = AllowSortingEnum.SingleColumn;

            string[] arrName = { "NguoiUyQuyen", "NguoiNhanUyQuyen", "PRName", "UserProgName", "ValidFrom", "ValidTo"
                                   , "ID_UyQuyen", "ProgName", "UserUyQuyenID", "UserDuocUyQuyenID","ProgID" };
            string[] arrHeader = { "Người ủy quyền", "Người nhận ủy quyền", "Hệ thống", "Tên đăng nhập", "Hiệu lực từ", "Hiệu lực đến"
                                     , "ID_UyQuyen", "ProgName", "UserUyQuyenID", "UserDuocUyQuyenID", "ProgID" };

            #region For
            for (int i = 0; i < arrHeader.Length; i++)
            {
                cfgUyQuyen.Cols[0].AllowMerging = true;
                cfgUyQuyen.Cols[1].AllowMerging = true;
                if (i > 1)
                {
                    cfgUyQuyen.Cols[i].AllowMerging = false;
                }
                // tên cột và header
                cfgUyQuyen.Cols[i + 1].Name = arrName[i];
                cfgUyQuyen.Cols[i + 1].Caption = arrHeader[i];
                cfgUyQuyen.Cols[i + 1].TextAlignFixed = TextAlignEnum.CenterCenter;
                cfgUyQuyen.Cols[i + 1].AllowMerging = true;

                // tạo Filter và định dạng ngày tháng
                switch (arrName[i])
                {
                    case "ValidFrom":
                    case "ValidTo":
                        cfgUyQuyen.Cols[i + 1].AllowFiltering = AllowFiltering.ByCondition;
                        cfgUyQuyen.Cols[i + 1].Format = "dd/MM/yyyy HH:mm:ss";
                        break;
                    default:
                        cfgUyQuyen.Cols[i + 1].AllowFiltering = AllowFiltering.Default;
                        break;
                }

                // Hide
                switch (arrName[i])
                {
                    case "UserUyQuyenID":
                    case "UserDuocUyQuyenID":
                    case "ProgID":
                        cfgUyQuyen.Cols[i + 1].Visible = false;
                        break;
                    default:
                        cfgUyQuyen.Cols[i + 1].Visible = true;
                        break;
                }
            }

            #endregion

            // kích thước cột
            cfgUyQuyen.Cols["NguoiUyQuyen"].Width = 120;
            cfgUyQuyen.Cols["NguoiNhanUyQuyen"].Width = 150;
            cfgUyQuyen.Cols["PRName"].Width = 200;
            cfgUyQuyen.Cols["UserProgName"].Width = 120;
            cfgUyQuyen.Cols["ValidFrom"].Width = 150;
            cfgUyQuyen.Cols["ValidTo"].Width = 150;
            cfgUyQuyen.Cols["ID_UyQuyen"].Visible = false;
            cfgUyQuyen.Cols["ProgName"].Visible = false;

            //Thêm sự kiện
            cfgUyQuyen.SelChange += cfgUyQuyen_SelChange;
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

        private void InitCbb()
        {
            DataTable dtCbb = _bus.CA_SignatureType_SelectAll();
            _cbbSignType.DataSource = dtCbb;
            _cbbSignType.DisplayMember = "Name";
            _cbbSignType.ValueMember = "SignatureTypeID";
        }

        private void InitCfgUnit()
        {
            cfgUnit.AllowAddNew = false;
            cfgUnit.AllowEditing = true;

            string[] arrName = { "UnitTypeID", "UnitTypeName", "UnitID", "UnitName", "UnitNotation", "Sign", "Check", "SignID", "UnitMaDV" };
            string[] arrHeader = { "UnitTypeID", "Loại đơn vị", "UnitID", "UnitName", "Đơn vị", "Loại chữ ký", " ", "SignID", "UnitMaDV" };

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
                    //case "Check":
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
                    //case "Sign":
                    //case "SignID":
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

                //// Editor
                //switch (arrName[i])
                //{
                //    case "Sign":
                //        cfgUnit.Cols[i + 1].Editor = _cbbSignType;
                //        break;

                //}
            }
            #endregion

            // kích thước cột
            cfgUnit.Cols["Check"].Width = 35;
            cfgUnit.Cols["UnitTypeName"].Width = 100;
            cfgUnit.Cols["UnitNotation"].Width = 150;
            cfgUnit.Cols["Sign"].Width = 100;

            cfgUnit.Cols["Check"].Move(1);

            CellStyle cs = cfgUnit.Styles.Add("Check");
            cs.DataType = typeof(Boolean);
            cs.ImageAlign = ImageAlignEnum.CenterCenter;
            cfgUnit.Cols["Check"].Style = cfgUnit.Styles["Check"];

            //Group theo Loại đơn vị
            cfgUnit.Tree.Column = 1;
            cfgUnit.Subtotal(C1.Win.C1FlexGrid.AggregateEnum.None, 0, 3, 3, 3, "{0}");
            cfgUnit.AutoSizeCols();
        }
        #endregion

        #region Data
        private void LoadData(int User_UyQuyen)
        {
            try
            {
                // lấy dữ liệu từ database
                DataTable dt = _bus.CA_UyQuyen_SelectBy_NguoiUyQuyen(User_UyQuyen);
                if (txtTimKiem.Text != "" && txtTimKiem.Text != null)
                {
                    DataView dv = dt.DefaultView;
                    dv.RowFilter = "NguoiUyQuyen LIKE '%" + txtTimKiem.Text + "%'";
                    //dt.Select("NguoiUyQuyen LIKE '" + txtTimKiem.Text + "'");
                    dt = dv.ToTable();
                }
                //_dtUserProgram = ds.Tables[0];
                _dtUserProgram = dt;
            }
            catch (Exception ex)
            {
                clsShare.Message_Error("Có lỗi xảy ra trong quá trình tải dữ liệu\r\n" + ex.ToString());
            }
        }

        public void LoadDataGridUnit(int userID, int progID)
        {
            DataTable dtUnitFull = _bus.CA_Unit_SelectBy_UserIDQuyen(userID, progID);
            DataColumn dc = new DataColumn("Check", typeof(bool));
            dc.DefaultValue = false;
            dtUnitFull.Columns.Add(dc);

            cfgUnit.DataSource = dtUnitFull;
        }

        public void LoadDataGridUnit()
        {
            DataTable dtUnitFull = _bus.CA_Unit_SelectByAll_UserUnit();
            DataColumn dc = new DataColumn("Check", typeof(bool));
            dc.DefaultValue = false;
            dtUnitFull.Columns.Add(dc);
            //dc = new DataColumn("Sign", typeof(string));
            //dtUnitFull.Columns.Add(dc);
            //dc = new DataColumn("SignID", typeof(int));
            //dtUnitFull.Columns.Add(dc);
            //dc.DefaultValue = -1;

            cfgUnit.DataSource = dtUnitFull;
        }

        public void LoadDataTrvUnit()
        {
            DataTable dt_UnitType = _bus.CA_UnitType_SelectAll();
            //trvUnit.CheckBoxes = true;
            foreach (DataRow item in dt_UnitType.Rows)
            {
                int unitTypeid = Convert.ToInt32(item["UnitTypeID"]);

                TreeNode tn = new TreeNode();
                tn.Name = unitTypeid.ToString();
                tn.Text = item["Name"].ToString();
                tn.Checked = false;

                DataTable dt_UnitByType = _bus.CA_Unit_SelectBy_UnitTypeID(unitTypeid);

                foreach (DataRow item1 in dt_UnitByType.Rows)
                {
                    //TreeNode tnChild = new TreeNode();
                    //tnChild.Text = item1["Notation"].ToString();
                    //tnChild.Name = item1["UnitID"].ToString();
                    //tnChild.Checked = false;
                    tn.Nodes.Add(item1["UnitID"].ToString(), item1["Notation"].ToString());
                    tn.Nodes[(tn.Nodes.Count - 1)].Tag = item1["UnitID"].ToString();
                }
                //trvUnit.Nodes.Add(tn);
            }
        }
        #endregion

        #region Controls
        // Button ----------------------------------
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadData(int.Parse(cboNguoiDung.SelectedValue.ToString()));
            InitCfgUyQuyen();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                frmThemSuaUyQuyen frm = new frmThemSuaUyQuyen();
                frm.bInsertUpdate = true;
                frm.ShowDialog();

                // load lại dữ liệu
                btnRefresh_Click(null, null);
                frm.Dispose();
            }
            catch (Exception ex)
            {
                clsShare.Message_Error(ex);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            frmThemSuaUyQuyen frm = new frmThemSuaUyQuyen();
            frm.bInsertUpdate = false;
            frm._id_UyQuyen = int.Parse(cfgUyQuyen.Rows[cfgUyQuyen.Row]["ID_UyQuyen"].ToString());
            frm._str_UserUyQuyen = cfgUyQuyen.Rows[cfgUyQuyen.Row]["NguoiUyQuyen"].ToString();
            frm._str_UserDuocUyQuyen = cfgUyQuyen.Rows[cfgUyQuyen.Row]["NguoiNhanUyQuyen"].ToString();
            frm.ShowDialog();
            //
            btnRefresh_Click(null, null);
            frm.Dispose();
        }

        // Grid Uy Quyen ---------------------------
        private void cfgUyQuyen_SelChange(object sender, EventArgs e)
        {
            try
            {
                if (_rowBefore != cfgUyQuyen.Row)
                {
                    if (_hasTrvChange && clsShare.Message_QuestionYN("Bạn có muốn lưu thay đổi quyền đơn vị không?"))
                    {
                        #region Check nhap du lieu
                        // Check nhap du lieu
                        DataRow[] dr_trong = ((DataTable)cfgUnit.DataSource).Select("Check = true");
                        foreach (DataRow item in dr_trong)
                        {
                            if (item["Sign"].ToString() == "")
                            {
                                clsShare.Message_Warning("Bạn chưa chọn loại chữ ký cho đơn vị!");
                                //e.Cancel = false;
                                return;
                            }
                        }
                        #endregion

                        //Lưu thay đổi vào bảng _dtQuyenUnit
                        //List<TreeNode> listCheck = new List<TreeNode>();
                        List<int> unitID = new List<int>();
                        List<bool> unitEnable = new List<bool>();
                        List<int> unitSign = new List<int>();

                        #region Xóa bản ghi hiện tại của _id_userprog
                        //Xóa bản ghi hiện tại của _id_userprog
                        string select = "ID_UyQuyen = " + _id_userprog.ToString();
                        DataRow[] dt_Quyen_remove = _dtQuyenUnit.Select(select);
                        foreach (DataRow item in dt_Quyen_remove)
                        {
                            unitID.Add(Convert.ToInt32(item["UnitID"]));
                            unitSign.Add(Convert.ToInt32(item["SignatureTypeID"]));
                            unitEnable.Add(false);
                            _dtQuyenUnit.Rows.Remove(item);
                        }

                        _bus.CA_UyQuyen_QuyenUnit_IsertUpdate(_id_userprog, unitID.ToArray(), unitEnable.ToArray(), unitSign.ToArray(), clsShare.sUserName);
                        unitID = new List<int>();
                        unitSign = new List<int>();
                        unitEnable = new List<bool>();
                        #endregion

                        #region Code cu
                        ////Thêm bản ghi mới theo node checked
                        //foreach (TreeNode item in trvUnit.Nodes)
                        //    foreach (TreeNode item1 in item.Nodes)
                        //        if (item1.Checked)
                        //        {
                        //            listCheck.Add(item1);
                        //            DataRow dr = _dtQuyenUnit.NewRow();
                        //            dr["ID_UyQuyen"] = _id_userprog;
                        //            dr["UnitID"] = item1.Tag;
                        //            dr["Notation"] = item1.Text;
                        //            _dtQuyenUnit.Rows.Add(dr);

                        //            unitID.Add(Convert.ToInt16(item1.Tag));
                        //            unitEnable.Add(true);
                        //        }
                        #endregion

                        #region Thêm bản ghi được tích grid
                        // Thêm bản ghi được tích grid
                        DataRow[] drCheck = ((DataTable)cfgUnit.DataSource).Select("Check = true");
                        foreach (DataRow item in drCheck)
                        {
                            unitID.Add(Convert.ToInt32(item["UnitID"]));
                            unitSign.Add(Convert.ToInt32(item["SignID"]));
                            unitEnable.Add(true);
                        }
                        #endregion

                        #region Luu db
                        //Lưu db
                        DataRow[] drRowNew = _dtUserProgram.Select("ID_UyQuyen=" + _id_userprog);
                        if (drRowNew.Count() > 0)
                        {
                            int userID = Convert.ToInt32(drRowNew[0]["ID_UyQuyen"]);

                            if (_bus.CA_UyQuyen_QuyenUnit_IsertUpdate(_id_userprog, unitID.ToArray(), unitEnable.ToArray(), unitSign.ToArray(), clsShare.sUserName))
                                clsShare.Message_Info("Cập nhật liên kết người dùng - hệ thống thành công!");
                            else
                                clsShare.Message_Error("Cập nhật liên kết người dùng - hệ thống thất bại!");
                        }
                        #endregion
                    }

                    #region Code cũ
                    ////Reset grid
                    //DataRow[] dr_romovecheck = ((DataTable)cfgUnit.DataSource).Select("Check = true or Sign <> '' ");
                    //foreach (DataRow item in dr_romovecheck)
                    //{
                    //    item["Check"] = false;
                    //    item["Sign"] = "";
                    //    item["SignID"] = -1;
                    //}
                    #endregion

                    #region Lấy ID_UserProg được chọn
                    // Lấy ID_UserProg được chọn
                    if (cfgUyQuyen.Row > 0 && cfgUyQuyen.Rows.Count > 1)
                    {

                        _id_userprog = Convert.ToInt32(cfgUyQuyen.Rows[cfgUyQuyen.Row]["ID_UyQuyen"]);
                        int userUyQuyenID = Convert.ToInt32(cfgUyQuyen.Rows[cfgUyQuyen.Row]["UserUyQuyenID"]);
                        int progID = Convert.ToInt32(cfgUyQuyen.Rows[cfgUyQuyen.Row]["ProgID"]);

                        LoadDataGridUnit(userUyQuyenID, progID);
                        InitCfgUnit();

                        //Lấy lại thông tin quyền
                        _dtQuyenUnit = _bus.CA_UyQuyen_QuyenUnit_SelectBy_ID_UyQuyen(int.Parse(cfgUyQuyen.Rows[cfgUyQuyen.Row]["ID_UyQuyen"].ToString()));
                        //string select = "ID_UyQuyen = " + _id_userprog.ToString();//

                        #region Kiem tra neu khong co don vi thi khong kiem tra tiep
                        // Kiem tra neu khong co don vi thi khong kiem tra tiep
                        //DataRow[] dt_Quyen = _dtQuyenUnit.Select(select);
                        if (_dtQuyenUnit.Rows.Count > 0)//
                        {
                            #region Code cu
                            //Duyệt bảng filter và check Đơn vị tương ứng trên treeview
                            //foreach (DataRow item in dt_Quyen)
                            //{
                            //    TreeNode[] lstn = trvUnit.Nodes.Find(item["UnitID"].ToString(), true);
                            //    foreach (TreeNode item1 in lstn)
                            //    {
                            //        if (item1.Text == item["Notation"].ToString())
                            //        {
                            //            item1.Checked = true;
                            //        }
                            //    }
                            //    //if (lstn.Count() > 0 & lstn[0].Text == item["Notation"].ToString())
                            //    //    lstn[0].Checked = true;
                            //}
                            #endregion

                            // dung grid
                            DataTable dt_IdUserProg = (DataTable)cfgUnit.DataSource;

                            // duyet tung don vi duoc pha  quyen
                            foreach (DataRow item in _dtQuyenUnit.Rows)//
                            {
                                if (item["UnitID"].ToString() != "")
                                {
                                    DataRow[] dr = dt_IdUserProg.Select("UnitID = " + item["UnitID"].ToString());
                                    if (dr.Count() > 0)
                                    {
                                        dr[0]["Check"] = true;
                                        dr[0]["Sign"] = item["SignName"];
                                        dr[0]["SignID"] = item["SignatureTypeID"];
                                    }
                                }
                            }
                        }
                        #endregion
                    }
                    #endregion

                    //Set lại trạng thái
                    _hasTrvChange = false;
                }

                // lưu vị trí của dòng hiện tại
                _rowBefore = cfgUyQuyen.Row;
            }
            catch (Exception ex)
            {
                clsShare.Message_Error(ex);
            }
        }

        private void cfgUyQuyen_DoubleClick(object sender, EventArgs e)
        {
            btnEdit_Click(null, null);
        }

        // Grid Unit -------------------------------
        private void cfgUnit_AfterEdit(object sender, RowColEventArgs e)
        {
            try
            {
                _hasTrvChange = true;
                switch (cfgUnit.Cols[e.Col].Name)
                {
                    case "Sign":
                        cfgUnit.Rows[e.Row]["SignID"] = _cbbSignType.SelectedValue;
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                clsShare.Message_Error(ex.Message);
            }
        }

        private void ApplyCheck(TreeNode node, bool c)
        {
            foreach (TreeNode child in node.Nodes)
            {
                child.Checked = c;
                ApplyCheck(child, c);
            }
        }

        private void trvUnit_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (!_updating)
            {
                _updating = true;
                TreeNode node = e.Node;
                bool c = node.Checked;

                ApplyCheck(node, c);

                while (node.Parent != null)
                {
                    node = node.Parent;
                    node.Checked = c;
                }
                _updating = false;
            }

            _hasTrvChange = true;
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
                        int id_UserProgram = Convert.ToInt32(cfgUyQuyen.Rows[ibr]["ID_UserProg"]);
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
        #endregion
    }
}
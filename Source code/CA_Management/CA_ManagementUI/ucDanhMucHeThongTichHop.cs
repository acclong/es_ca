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
using Telerik.WinControls.UI;

namespace ES.CA_ManagementUI
{
    public partial class ucDanhMucHeThongTichHop : UserControl
    {
        public ucDanhMucHeThongTichHop()
        {
            InitializeComponent();
        }

        private void ucDanhMucHeThongTichHop_Load(object sender, EventArgs e)
        {
            try
            {
                AddContextMenu_C1FlexGrid(ref cfgDanhMuc);
                LoadData();
                InitRgvDanhMuc();

                // Thêm sự kiên KeyDown
                cfgDanhMuc.KeyDown += new KeyEventHandler(clsShare.C1FlexGrid_KeyDown);
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
        private void InitRgvDanhMuc()
        {
            // cấu hình radGrid
            cfgDanhMuc.ExtendLastCol = true;
            cfgDanhMuc.Cols.Fixed = 1;
            cfgDanhMuc.Cols[0].Width = 25;
            cfgDanhMuc.AllowFiltering = true;

            //Edited by Toantk on 16/4/2015
            //Thêm trường STT và ẩn cột ID
            string[] arrName = { "STT", "ProgID", "ProgName", "Name", "Notation", "Status", "StatusName", "ServerName", "DBName",
                                      "UserDB", "PasswordDB", "QueryUser" };
            string[] arrHeader = { "STT", "ProgID", "Mã hệ thống", "Tên hệ thống", "Ký hiệu", "Status", "Trạng thái", "Máy chủ",
                                     "Cơ sở dữ liệu", "Tên đăng nhập", "Mật khẩu", "QueryUser" };

            for (int i = 0; i < arrName.Length; i++)
            {

                #region C1
                // tên cột và header
                cfgDanhMuc.Cols[i + 1].Name = arrName[i];
                cfgDanhMuc.Cols[i + 1].Caption = arrHeader[i];
                // căn lề
                if (i == 0 || i == 6)
                    cfgDanhMuc.Cols[i + 1].TextAlign = TextAlignEnum.CenterCenter;
                else
                    cfgDanhMuc.Cols[i + 1].TextAlign = TextAlignEnum.LeftCenter;
                // ẩn các cột
                if ((i > 8 && i != 11) || i == 5 || i == 1 || i == 2)
                    cfgDanhMuc.Cols[i + 1].Visible = false;
                // kích thước cột
                switch (i)
                {
                    case 0: cfgDanhMuc.Cols[i + 1].Width = 50; break;
                    case 4: cfgDanhMuc.Cols[i + 1].Width = 100; break;
                    case 6: cfgDanhMuc.Cols[i + 1].Width = 150; break;
                    case 2: cfgDanhMuc.Cols[i + 1].Width = 150; break;
                    case 3: cfgDanhMuc.Cols[i + 1].Width = 200; break;
                    case 7: cfgDanhMuc.Cols[i + 1].Width = 100; break;
                    case 8: cfgDanhMuc.Cols[i + 1].Width = 100; break;
                    case 11: cfgDanhMuc.Cols[i + 1].Width = 300; break;
                }

                if (i == 6)
                {
                    cfgDanhMuc.Cols[i + 1].AllowFiltering = AllowFiltering.Default;
                    ColumnFilter colFilter = new ColumnFilter();
                    colFilter.ValueFilter.ShowValues = new string[] { "Hiệu lực" };
                    cfgDanhMuc.Cols[i + 1].Filter = colFilter;
                    //cfgDanhMuc.Cols[i].Filter = new ColumnFilter().ValueFilter.ShowValues = new string[] { "Hiệu lực" };

                }
                else
                    cfgDanhMuc.Cols[i + 1].AllowFiltering = AllowFiltering.Default;
                #endregion
            }

            // căn giừa hàng đầu
            cfgDanhMuc.Rows[0].TextAlign = TextAlignEnum.CenterCenter;
            //cfgDanhMuc.Rows[0].Style.Font = new Font("Times New Roman", 11, FontStyle.Bold);
        }

        private void AddContextMenu_C1FlexGrid(ref C1FlexGrid c1Grid)
        {
            //Thêm Lệnh
            _tspItem = new ToolStripMenuItem();
            _tspItem.Name = "LogProg";
            _tspItem.Text = "Lịch sử hệ thống";
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
            DataTable dt = bus.CA_Program_SelectAll();
            cfgDanhMuc.DataSource = dt;
        }
        #endregion

        #region Controls
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                ColumnFilter colFilter = new ColumnFilter();
                if (cfgDanhMuc.Cols[7].Filter != null)
                {
                    colFilter = (ColumnFilter)(cfgDanhMuc.Cols[7].Filter);
                }

                frmThemSuaHeThongTichHop frm = new frmThemSuaHeThongTichHop();
                // truyền tham số
                frm._iProgID = -1;
                frm.ShowDialog();

                // load lại dữ liệu
                LoadData();
                InitRgvDanhMuc();

                if (colFilter.ValueFilter.ShowValues != null)
                    cfgDanhMuc.Cols[7].Filter = colFilter;
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
                if (cfgDanhMuc.Row > 0)
                {
                    ColumnFilter colFilter = new ColumnFilter();
                    if (cfgDanhMuc.Cols[7].Filter != null)
                    {
                        colFilter = (ColumnFilter)(cfgDanhMuc.Cols[7].Filter);
                    }

                    frmThemSuaHeThongTichHop frm = new frmThemSuaHeThongTichHop();

                    // truyền tham số xang frm
                    if (cfgDanhMuc.Row > 0)
                        frm._iProgID = Convert.ToInt32(cfgDanhMuc.Rows[cfgDanhMuc.Row]["ProgID"]);
                    else
                        frm._iProgID = -1;
                    frm.ShowDialog();

                    // load lại dữ liệu
                    LoadData();
                    InitRgvDanhMuc();

                    if (colFilter.ValueFilter.ShowValues != null)
                        cfgDanhMuc.Cols[7].Filter = colFilter;
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

                int ProgID = Convert.ToInt16(cfgDanhMuc.Rows[cfgDanhMuc.Row]["ProgID"]);

                if (clsShare.Message_WarningYN("Bạn có chắc muốn xóa hệ thống [" + cfgDanhMuc.Rows[cfgDanhMuc.Row]["Name"].ToString() + "] không?"))
                {
                    bool kq = bus.CA_Program_DeleteByProgID(ProgID);

                    if (kq)
                    {
                        // Thông báo kết quả thành công
                        clsShare.Message_Info("Xóa hệ thống thành công!");

                        // Lấy Filter
                        ColumnFilter colFilter = new ColumnFilter();
                        if (cfgDanhMuc.Cols[7].Filter != null)
                        {
                            colFilter = (ColumnFilter)(cfgDanhMuc.Cols[7].Filter);
                        }

                        // load lại dữ liệu
                        LoadData();

                        // Gán lạ Filter
                        if (colFilter.ValueFilter.ShowValues != null)
                            cfgDanhMuc.Cols[7].Filter = colFilter;
                    }
                    else
                    {
                        clsShare.Message_Error("Hệ thống không được phép xóa");
                    }
                }
            }
            catch (Exception ex)
            {
                clsShare.Message_Error(ex.Message);
            }
        }
        #endregion

        #region Event
        private void cfgDanhMuc_DoubleClick(object sender, EventArgs e)
        {
            var ht = cfgDanhMuc.HitTest();
            if (cfgDanhMuc.Row == cfgDanhMuc.RowSel)
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

                    //Xử lý khi click vào ô tương ứng
                    if (item.Name == "LogProg")
                    {
                        frmXemLogDanhMuc frm = new frmXemLogDanhMuc();
                        frm.Text = "Lịch sử log hệ thống tích hợp";
                        frm.Log = frmXemLogDanhMuc.TypeLog.Program;
                        int progID = Convert.ToInt32(cfgDanhMuc.Rows[ibr]["ProgID"]);
                        frm.ProgID = progID;
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

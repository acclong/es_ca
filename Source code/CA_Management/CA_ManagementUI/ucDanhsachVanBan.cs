using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls.UI;
using ES.CA_ManagementBUS;
using Telerik.WinControls.Data;
using C1.Win.C1FlexGrid;
using esDigitalSignature.Library;

namespace ES.CA_ManagementUI
{
    public partial class ucDanhsachVanBan : UserControl
    {
        BUSQuanTri _bus = new BUSQuanTri();
        private ContextMenuStrip _contextMenu = new ContextMenuStrip();
        private ToolStripMenuItem _tspItem;
        public ucDanhsachVanBan()
        {
            InitializeComponent();
        }

        private void ucDanhSachVanBan_Load(object sender, EventArgs e)
        {
            try
            {
                AddContextMenu_C1FlexGrid(ref cfgFile);
                InitCboTypeUnit();
                dpkToDate.Value = DateTime.Now;
                dpkFromDate.Value = DateTime.Now.AddMonths(-1);
                LoadData();
                InitrgvFile();

                // Thêm sự kiện KeyDown
                cfgFile.KeyDown += new KeyEventHandler(clsShare.C1FlexGrid_KeyDown);
            }
            catch (Exception ex)
            {
                clsShare.Message_Error(ex);
            }
        }

        private void InitCboTypeUnit()
        {
            DataTable dt = _bus.CA_UnitType_SelectAll();
            cboTypeUnit.DataSource = dt;
            cboTypeUnit.DisplayMember = "Name";
            cboTypeUnit.ValueMember = "UnitTypeID";
            this.cboTypeUnit.SelectedIndexChanged += new System.EventHandler(this.cboTypeUnit_SelectedIndexChanged);
            cboTypeUnit.SelectedValue = 3;
        }

        private void cboTypeUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int iTypeUnit = Convert.ToInt32(cboTypeUnit.SelectedValue);
                cboUnit.DataSource = _bus.CA_Unit_SelectBy_UnitTypeID_Status(iTypeUnit, -1);
                cboUnit.DisplayMember = "Notation";
                cboUnit.ValueMember = "UnitID";
                cboUnit.SelectedIndex = 0;
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
                if (dpkFromDate.Value > dpkToDate.Value)
                {
                    clsShare.Message_Error("Ngày bắt đầu phải nhỏ hơn ngày kết thúc.\nHãy kiểm tra lại!");
                    return;
                }
                LoadData();
                InitrgvFile();
            }
            catch (Exception ex)
            {
                clsShare.Message_Error(ex);
            }
        }

        private void InitrgvFile()
        {
            cfgFile.AllowSorting = AllowSortingEnum.SingleColumn;

            //Thêm trường STT và ẩn cột ID
            string[] arrName = {"", "STT", "FileID", "FileNumber", "FilePath", "FileHash", "FileTypeID",
                                   "FileType", "UnitID", "UnitName", "FileDate", "Status", "StatusName", "Description"};
            string[] arrCaption = {"", "STT", "FileID", "Số văn bản", "Đường dẫn văn bản", "Chuỗi Hash", "FileTypeID",
                                   "Loại văn bản", "UnitID", "Đơn vị", "Ngày", "Status", "Trạng thái", "Miêu tả"};

            for (int i = 0; i < arrName.Length; i++)
            {
                // tên và header
                cfgFile.Cols[i].Name = arrName[i];
                cfgFile.Cols[i].Caption = arrCaption[i];
                // căn lề
                if (i == 1 || i == 12)
                    cfgFile.Cols[i].TextAlign = TextAlignEnum.CenterCenter;
                else
                    cfgFile.Cols[i].TextAlign = TextAlignEnum.LeftCenter;
                cfgFile.Rows[0].TextAlign = TextAlignEnum.CenterCenter;
                // kích thước cột
                if (i == 0)
                    cfgFile.Cols[i].Width = 25;
                else if (i == 1)
                    cfgFile.Cols[i].Width = 50;
                else if (i == 3)
                    cfgFile.Cols[i].Width = 300;
                else if (i == 4)
                    cfgFile.Cols[i].Width = 500;
                else if (i == 7)
                    cfgFile.Cols[i].Width = 200;
                else if (i == 9 || i == 10 || i == 12)
                    cfgFile.Cols[i].Width = 90;
                // format cột
                if (i == 10)
                {
                    cfgFile.Cols[i].Format = "dd/MM/yyyy";
                    cfgFile.Cols[i].AllowFiltering = AllowFiltering.ByCondition;
                }
                else if (i != 0)
                    cfgFile.Cols[i].AllowFiltering = AllowFiltering.ByValue;
                // ẩn các cột không cần thiết
                if (i == 2 || i == 5 || i == 6 || i == 8 || i == 11)
                    cfgFile.Cols[i].Visible = false;
            }
        }

        private void LoadData()
        {
            int iUnit = Convert.ToInt32(cboUnit.SelectedValue);
            DateTime FromDate = dpkFromDate.Value;
            DateTime ToDate = dpkToDate.Value;
            cfgFile.DataSource = _bus.FL_File_SelectByUnitID_FromDateToDate(iUnit, FromDate, ToDate);
        }

        private void AddContextMenu_C1FlexGrid(ref C1FlexGrid c1Grid)
        {
            //Thêm Lệnh
            _tspItem = new ToolStripMenuItem();
            _tspItem.Name = "LogTrangThai";
            _tspItem.Text = "Lịch sử trạng thái";
            _contextMenu.Items.Add(_tspItem);

            //
            _tspItem = new ToolStripMenuItem();
            _tspItem.Name = "LogKy";
            _tspItem.Text = "Lịch sử ký";
            _contextMenu.Items.Add(_tspItem);

            //
            _tspItem = new ToolStripMenuItem();
            _tspItem.Name = "VanBanThayThe";
            _tspItem.Text = "Văn bản liên quan";
            _contextMenu.Items.Add(_tspItem);

            //
            _tspItem = new ToolStripMenuItem();
            _tspItem.Name = "LayFile";
            _tspItem.Text = "Lấy văn bản";
            _contextMenu.Items.Add(_tspItem);

            //Add vào grid
            c1Grid.ContextMenuStrip = _contextMenu;
            //Add sự kiện
            _contextMenu.ItemClicked += new ToolStripItemClickedEventHandler(contextMenu_ItemClicked);
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

                    int iFileID = Convert.ToInt32(cfgFile.Rows[ibr]["FileID"]);

                    //Xử lý khi click vào ô tương ứng
                    if (item.Name == "LogTrangThai")
                    {
                        frmXemLogVB frm = new frmXemLogVB();
                        frm.Text = "Lịch sử trạng thái văn bản";
                        frm.FileID = iFileID;
                        frm.SignStatus = 0;
                        frm.ShowDialog();
                    }
                    if (item.Name == "LogKy")
                    {
                        frmXemLogVB frm = new frmXemLogVB();
                        frm.Text = "Lịch sử ký văn bản";
                        frm.FileID = iFileID;
                        frm.SignStatus = 1;
                        frm.ShowDialog();
                    }
                    if (item.Name == "VanBanThayThe")
                    {
                        frmXemVBThayTheNew frm = new frmXemVBThayTheNew();
                        frm.FileID = iFileID;
                        frm.ShowDialog();
                    }
                    if (item.Name == "LayFile")
                    {
                        SaveFileDialog sfd = new SaveFileDialog();
                        string fileName = _bus.Q_CONFIG_GetRootFile() + cfgFile.Rows[ibr]["FilePath"].ToString();
                        sfd.FileName = Path.GetFileName(fileName);
                        sfd.Filter = "(" + Path.GetExtension(fileName) + ")|*" + Path.GetExtension(fileName);

                        if (sfd.ShowDialog() == DialogResult.OK)
                        {
                            //Read file to byte array
                            FileStream stream = File.OpenRead(fileName);
                            byte[] fileBytes = new byte[stream.Length];
                            stream.Read(fileBytes, 0, fileBytes.Length);
                            stream.Close();

                            //Lưu file
                            using (Stream file = File.OpenWrite(sfd.FileName))
                            {
                                file.Write(fileBytes, 0, fileBytes.Length);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clsShare.Message_Error(ex);
            }
        }

        #region code cũ
        //private void rgvFile_CommandCellClick(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        int fileID;
        //        // lấy giá trị CertAuthID tương ứng
        //        GridCommandCellElement gcce = sender as GridCommandCellElement;
        //        if (gcce.ColumnInfo.HeaderText == "Log VB")
        //        {
        //            fileID = Convert.ToInt32(gcce.RowInfo.Cells["FileID"].Value);
        //            frmXemLogVB frm = new frmXemLogVB();
        //            frm.Text = "Lịch sử trạng thái văn bản";
        //            frm.FileID = fileID;
        //            frm.SignStatus = 0;
        //            frm.ShowDialog();
        //        }
        //        else if (gcce.ColumnInfo.HeaderText == "Log ký")
        //        {
        //            fileID = Convert.ToInt32(gcce.RowInfo.Cells["FileID"].Value);
        //            frmXemLogVB frm = new frmXemLogVB();
        //            frm.Text = "Lịch sử ký văn bản";
        //            frm.Width = 1200;
        //            frm.FileID = fileID;
        //            frm.SignStatus = 1;
        //            frm.ShowDialog();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        clsShare.Message_Error(ex);
        //    }
        //}
        #endregion
    }
}

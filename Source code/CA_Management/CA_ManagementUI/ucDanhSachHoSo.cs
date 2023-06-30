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
using System.IO;
using System.Globalization;

namespace ES.CA_ManagementUI
{
    public partial class ucDanhSachHoSo : UserControl
    {
        BUSQuanTri _bus = new BUSQuanTri();
        private ContextMenuStrip _contextMenu = new ContextMenuStrip();
        private ToolStripMenuItem _tspItem;

        public ucDanhSachHoSo()
        {
            InitializeComponent();
        }

        private void ucDanhSachHoSo_Load(object sender, EventArgs e)
        {
            try
            {
                // khai báo combobox
                InitcboUnitType();
                InitcboDateType();
                // chỉnh panel
                clsShare.FormatWidthComboBoxInPanel(pnlHeader);
                // thêm sự kiện download
                AddContextMenu_C1FlexGrid(ref cfgFile);

                LoadDataDefault();
                InitrgvFile();

                // Thêm sự kiện KeyDown
                cfgFile.KeyDown += new KeyEventHandler(clsShare.C1FlexGrid_KeyDown);
            }
            catch (Exception ex)
            {
                clsShare.Message_Error(ex);
            }
        }

        private void InitcboUnitType()
        {
            DataTable dt = _bus.CA_UnitType_SelectAll();

            DataRow dr = dt.NewRow();
            dr["Name"] = "--Chọn loại đơn vị--";
            dr["UnitTypeID"] = -1;
            dt.Rows.InsertAt(dr, 0);

            cboTypeUnit.DataSource = dt;
            cboTypeUnit.DisplayMember = "Name";
            cboTypeUnit.ValueMember = "UnitTypeID";
            this.cboTypeUnit.SelectedIndexChanged += new System.EventHandler(this.cboTypeUnit_SelectedIndexChanged);
            cboTypeUnit.SelectedIndex = 0;
        }

        private void InitcboNam()
        {
            cboYear.Items.Clear();
            string[] array = new string[50];
            for (int i = 0; i < 50; i++)
            {
                array[i] = (2010 + i).ToString();
            }
            cboYear.Items.AddRange(array);
            cboYear.SelectedIndex = DateTime.Now.Year - 2010;
        }

        private void InitcboThang(char type)
        {
            if (cboMonth.DataSource != null)
                cboMonth.DataSource = null;
            else
                cboMonth.Items.Clear();
            if (type == 'm')
            {
                string[] array = new string[12];
                for (int i = 0; i < 12; i++)
                {
                    array[i] = (1 + i).ToString();
                }
                cboMonth.Items.AddRange(array);
                cboMonth.SelectedIndex = DateTime.Now.Month - 1;
                lblMonth.Text = "Tháng";
            }
            else if (type == 'q')
            {
                string[] array = new string[4];
                for (int i = 0; i < 4; i++)
                {
                    array[i] = (1 + i).ToString();
                }
                cboMonth.Items.AddRange(array);
                cboMonth.SelectedIndex = (DateTime.Now.Month / 3) - 1;
                lblMonth.Text = "Quý";
            }
            else if (type == 'w')
            {
                int year = Convert.ToInt32(cboYear.SelectedItem);
                cboMonth.Items.AddRange(getWeak(year).ToArray());
                Calendar cal = DateTimeFormatInfo.CurrentInfo.Calendar;
                cboMonth.SelectedIndex = cal.GetWeekOfYear(DateTime.Now, DateTimeFormatInfo.CurrentInfo.CalendarWeekRule,
                                                        DateTimeFormatInfo.CurrentInfo.FirstDayOfWeek) - 1;
            }
        }

        private void InitcboDateType()
        {
            DataTable dtDateType = new DataTable();
            dtDateType.Columns.Add("DateType", typeof(int));
            dtDateType.Columns.Add("Name", typeof(string));

            string[] array = { "Ngày", "Tuần", "Tháng", "Quý", "Năm" };
            for (int i = 0; i < array.Length; i++)
            {
                DataRow dr = dtDateType.NewRow();
                dr["DateType"] = i + 1;
                dr["Name"] = array[i];
                dtDateType.Rows.Add(dr);
            }

            cboDateType.DataSource = dtDateType;
            cboDateType.DisplayMember = "Name";
            cboDateType.ValueMember = "DateType";
            cboDateType.SelectedIndex = 0;
            this.cboDateType.SelectedIndexChanged += new System.EventHandler(this.cboDateType_SelectedIndexChanged);
        }

        private void InitcboUint(int iTypeUnit)
        {
            DataTable dtUnit = _bus.CA_Unit_SelectBy_UnitTypeID_Status(iTypeUnit, -1);

            DataRow drUnit = dtUnit.NewRow();
            drUnit["Notation"] = "--Tất cả--";
            drUnit["UnitID"] = -1;
            dtUnit.Rows.InsertAt(drUnit, 0);

            cboUnit.DataSource = dtUnit;
            cboUnit.DisplayMember = "Notation";
            cboUnit.ValueMember = "UnitID";
            cboUnit.SelectedIndex = 0;
        }

        private void InitcboProfileType(int iTypeUnit, int iDateType, DateTime date)
        {
            DataTable dtProfileType = _bus.FL_ProfileType_SelectByUnitType_DateType_Date(iTypeUnit, iDateType, date);

            if (dtProfileType.Rows.Count > 0)
            {
                DataRow drProfileType = dtProfileType.NewRow();
                drProfileType["Name"] = "--Tất cả--";
                drProfileType["ProfileTypeID"] = -1;
                dtProfileType.Rows.InsertAt(drProfileType, 0);

                cboProfileType.DataSource = dtProfileType;
                cboProfileType.DisplayMember = "Name";
                cboProfileType.ValueMember = "ProfileTypeID";
                cboProfileType.SelectedIndex = 0;
            }
            else
            {
                cboProfileType.DataSource = dtProfileType;
                cboProfileType.DisplayMember = "Name";
                cboProfileType.ValueMember = "ProfileTypeID";
            }
        }

        private void cboTypeUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int iTypeUnit = Convert.ToInt32(cboTypeUnit.SelectedValue);
                int iDateType = Convert.ToInt32(cboDateType.SelectedValue);
                if (iTypeUnit == -1)
                {
                    // ẩn control khi chưa chọn loại đơn vị
                    pnlTime.Visible = false;
                    lblUnit.Enabled = false;
                    cboUnit.Enabled = false;
                    lblDateType.Enabled = false;
                    cboDateType.Enabled = false;
                    lblProfileType.Enabled = false;
                    cboProfileType.Enabled = false;
                }
                else
                {
                    // thay đổi control
                    pnlTime.Visible = true;
                    lblUnit.Enabled = true;
                    cboUnit.Enabled = true;
                    lblDateType.Enabled = true;
                    cboDateType.Enabled = true;
                    lblProfileType.Enabled = true;
                    cboProfileType.Enabled = true;

                    // khai báo đơn vị
                    InitcboUint(iTypeUnit);

                    // hiển thị mặc định datetimepicker ngày
                    EditTime(iDateType);

                    // khai báo loại hồ sơ
                    InitcboProfileType(iTypeUnit, iDateType, DateTime.Now);

                    // sắp xếp combobox
                    clsShare.FormatWidthComboBoxInPanel(pnlTime);
                }
            }
            catch (Exception ex)
            {
                clsShare.Message_Error(ex);
            }
        }

        private void cboDateType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int iTypeUnit = Convert.ToInt32(cboTypeUnit.SelectedValue);
                int iDateType = Convert.ToInt32(cboDateType.SelectedValue);

                // hiển thị ngày, tháng, năm tương ứng với hồ sơ
                EditTime(iDateType);

                // khai báo loại hồ sơ
                InitcboProfileType(iTypeUnit, iDateType, DateTime.Now);

                // sắp xếp combobox
                clsShare.FormatWidthComboBoxInPanel(pnlTime);
            }
            catch (Exception ex)
            {
                clsShare.Message_Error(ex);
            }
        }

        private void cboYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int iTypeUnit = Convert.ToInt32(cboTypeUnit.SelectedValue);
                int iDateType = Convert.ToInt32(cboDateType.SelectedValue);
                DateTime date;
                if (iDateType == 5)
                    date = Convert.ToDateTime(cboYear.SelectedItem + "-01-01");
                else if (iDateType == 4)
                    date = Convert.ToDateTime(cboYear.SelectedItem + "-" + (Convert.ToInt32(cboMonth.SelectedItem) * 3 - 2) + "-1");
                else if (iDateType == 3)
                    date = Convert.ToDateTime(cboYear.SelectedItem + "-" + cboMonth.SelectedItem + "-1");
                else
                {
                    string day = cboMonth.SelectedItem.ToString().Split(' ')[4];
                    date = Convert.ToDateTime(day.Split('/')[2] + "-" + day.Split('/')[1] + "-" + day.Split('/')[0]);
                }

                // khai báo loại hồ sơ
                InitcboProfileType(iTypeUnit, iDateType, date);

                // sắp xếp combobox
                clsShare.FormatWidthComboBoxInPanel(pnlTime);
            }
            catch (Exception ex)
            {
                clsShare.Message_Error(ex);
            }
        }

        private void cboMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int iTypeUnit = Convert.ToInt32(cboTypeUnit.SelectedValue);
                int iDateType = Convert.ToInt32(cboDateType.SelectedValue);
                DateTime date;
                if (iDateType == 5)
                    date = Convert.ToDateTime(cboYear.SelectedItem + "-01-01");
                else if (iDateType == 4)
                    date = Convert.ToDateTime(cboYear.SelectedItem + "-" + ((int)cboMonth.SelectedItem * 3 - 2) + "-1");
                else if (iDateType == 3)
                    date = Convert.ToDateTime(cboYear.SelectedItem + "-" + cboMonth.SelectedItem + "-1");
                else
                {
                    string day = cboMonth.SelectedItem.ToString().Split(' ')[4];
                    date = Convert.ToDateTime(day.Split('/')[2] + "-" + day.Split('/')[1] + "-" + day.Split('/')[0]);
                }

                // khai báo loại hồ sơ
                InitcboProfileType(iTypeUnit, iDateType, date);

                // sắp xếp combobox
                clsShare.FormatWidthComboBoxInPanel(pnlTime);
            }
            catch (Exception ex)
            {
                clsShare.Message_Error(ex);
            }
        }

        private void dpkDate_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                int iTypeUnit = Convert.ToInt32(cboTypeUnit.SelectedValue);
                int iDateType = Convert.ToInt32(cboDateType.SelectedValue);
                DateTime date = dpkDate.Value;

                // khai báo loại hồ sơ
                InitcboProfileType(iTypeUnit, iDateType, date);

                // sắp xếp combobox
                clsShare.FormatWidthComboBoxInPanel(pnlTime);
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
                if (Convert.ToInt32(cboTypeUnit.SelectedValue) != -1)
                {
                    LoadData();
                    InitrgvFile();
                }
                else
                {
                    LoadDataDefault();
                    InitrgvFile();
                }
            }
            catch (Exception ex)
            {
                clsShare.Message_Error(ex);
            }
        }

        private void lblDownLoad_Click(object sender, EventArgs e)
        {
            try
            {
                if (cfgFile.Rows.Count == 1)
                    return;

                FolderBrowserDialog fbd = new FolderBrowserDialog();
                fbd.Description = "Chọn thư mục lưu văn bản";
                DialogResult result = fbd.ShowDialog();
                string path = fbd.SelectedPath;

                if (path == "")
                    return;

                for (int i = 1; i < cfgFile.Rows.Count; i++)
                {
                    if (Convert.ToInt32(cfgFile.Rows[i]["Status"]) != 1)
                        continue;
                    string fileName = _bus.Q_CONFIG_GetRootFile() + cfgFile.Rows[i]["FilePath"].ToString();
                    //Read file to byte array
                    FileStream stream = File.OpenRead(fileName);
                    byte[] fileBytes = new byte[stream.Length];
                    stream.Read(fileBytes, 0, fileBytes.Length);
                    stream.Close();

                    //Lưu file
                    using (Stream file = File.OpenWrite(path + "\\" + Path.GetFileName(fileName)))
                    {
                        file.Write(fileBytes, 0, fileBytes.Length);
                    }
                }
                clsShare.Message_Info("Lấy văn bản thành công!");
            }
            catch (Exception ex)
            {
                clsShare.Message_Error(ex);
            }
        }

        private void InitrgvFile()
        {
            cfgFile.AllowMerging = AllowMergingEnum.RestrictRows;
            cfgFile.ExtendLastCol = true;
            cfgFile.AllowSorting = AllowSortingEnum.SingleColumn;

            //Thêm trường STT và ẩn cột ID
            string[] arrName = {"", "UnitID", "UnitName", "ProfileTypeID", "ProfileTypeName", "FileID", "FileNumber",
                                   "FilePath", "FileDate", "FileTypeID", "FileTypeName", "ID_FileProfile", "DateType",
                                   "Status", "StatusName", "Description" };
            string[] arrCaption = {"", "UnitID", "Đơn vị", "ProfileTypeID", "Loại hồ sơ", "FileID", "Số văn bản",
                                   "Đường dẫn văn bản", "Ngày", "FileTypeID", "Loại văn bản", "ID_FileProfile", "Loại thời gian",
                                   "Status", "Trạng thái", "Miêu tả" };

            for (int i = 0; i < arrName.Length; i++)
            {
                // tên và header
                cfgFile.Cols[i].Name = arrName[i];
                cfgFile.Cols[i].Caption = arrCaption[i];
                // căn lề
                if (i == 8 || i == 14)
                    cfgFile.Cols[i].TextAlign = TextAlignEnum.CenterCenter;
                else
                    cfgFile.Cols[i].TextAlign = TextAlignEnum.LeftCenter;
                cfgFile.Rows[0].TextAlign = TextAlignEnum.CenterCenter;
                // kích thước cột
                if (i == 0)
                    cfgFile.Cols[i].Width = 25;
                else if (i == 2 || i == 4)
                    cfgFile.Cols[i].Width = 150;
                else if (i == 6 || i == 10)
                    cfgFile.Cols[i].Width = 250;
                else if (i == 7)
                    cfgFile.Cols[i].Width = 550;
                else if (i == 8 || i == 14)
                    cfgFile.Cols[i].Width = 100;
                else if (i == 15)
                    cfgFile.Cols[i].Width = 200;
                // format cột
                if (i == 8)
                {
                    cfgFile.Cols[i].Format = "dd/MM/yyyy";
                    cfgFile.Cols[i].AllowFiltering = AllowFiltering.ByCondition;
                }
                else if (i != 0)
                    cfgFile.Cols[i].AllowFiltering = AllowFiltering.ByValue;
                // ẩn các cột không cần thiết
                if (i == 1 || i == 3 || i == 5 || i == 7 || i == 9 || i == 11 || i == 12 || i == 13 || i == 14)
                    cfgFile.Cols[i].Visible = false;

                #region Merged
                //switch (arrName[i])
                //{
                //    case "UnitName":
                //    case "ProfileTypeName":
                //        cfgFile.Cols[i].AllowMerging = true;
                //        break;
                //}
                #endregion
            }

            cfgFile.Cols["FileTypeName"].Move(5);

            // ẩn cột hoặc merged
            cfgFile.AllowMerging = AllowMergingEnum.Custom;

            if (Convert.ToInt32(cboProfileType.SelectedValue) != -1)
                cfgFile.Cols[4].Visible = false;
            else
            {
                // Merged
                cfgFile.MergedRanges.Clear();
                for (int m = 2, n = 1; m < cfgFile.Rows.Count; m++)
                {
                    if (Convert.ToInt32(cfgFile.Rows[m][3]) != Convert.ToInt32(cfgFile.Rows[m - 1][3]))
                    {
                        if (n != m)
                        {
                            cfgFile.MergedRanges.Add(cfgFile.GetCellRange(n, 4, m - 1, 4));
                            n = m;
                        }
                    }
                    if (m == cfgFile.Rows.Count - 1)
                    {
                        cfgFile.MergedRanges.Add(cfgFile.GetCellRange(n, 4, m, 4));
                    }
                }
            }

            if (Convert.ToInt32(cboUnit.SelectedValue) != -1)
                cfgFile.Cols[2].Visible = false;
            else
            {
                if (Convert.ToInt32(cboProfileType.SelectedValue) != -1)
                    cfgFile.MergedRanges.Clear();
                for (int m = 2, n = 1; m < cfgFile.Rows.Count; m++)
                {
                    if (Convert.ToInt32(cfgFile.Rows[m][1]) != Convert.ToInt32(cfgFile.Rows[m - 1][1]))
                    {
                        if (n != m)
                        {
                            cfgFile.MergedRanges.Add(cfgFile.GetCellRange(n, 2, m - 1, 2));
                            n = m;
                        }
                    }
                    if (m == cfgFile.Rows.Count - 1)
                    {
                        cfgFile.MergedRanges.Add(cfgFile.GetCellRange(n, 2, m, 2));
                    }
                }
            }
        }

        private void LoadData()
        {
            int iUnit = Convert.ToInt32(cboUnit.SelectedValue);
            int iProfileTypeID = Convert.ToInt32(cboProfileType.SelectedValue);
            int iUnitType = Convert.ToInt32(cboTypeUnit.SelectedValue);
            int iDateType = Convert.ToInt32(cboDateType.SelectedValue);
            DateTime date;
            if (iDateType == 5)
                date = Convert.ToDateTime(cboYear.SelectedItem + "-01-01");
            else if (iDateType == 4)
                date = Convert.ToDateTime(cboYear.SelectedItem + "-" + (Convert.ToInt32(cboMonth.SelectedItem) * 3 - 2) + "-1");
            else if (iDateType == 3)
                date = Convert.ToDateTime(cboYear.SelectedItem + "-" + cboMonth.SelectedItem + "-1");
            else if (iDateType == 2)
            {
                string day = cboMonth.SelectedItem.ToString().Split(' ')[4];
                date = Convert.ToDateTime(day.Split('/')[2] + "-" + day.Split('/')[1] + "-" + day.Split('/')[0]);
            }
            else
                date = dpkDate.Value;
            cfgFile.DataSource = _bus.FL_File_SelectProfileTypeID_DateType_UnitID_UnitType_Date(iProfileTypeID, iUnitType, iUnit, iDateType, date);
        }

        private void LoadDataDefault()
        {
            string[] arrName = {"UnitID", "UnitName", "ProfileTypeID", "ProfileTypeName", "FileID", "FileNumber",
                                   "FilePath", "FileDate", "FileTypeID", "FileTypeName", "ID_FileProfile", "DateType",
                                   "Status", "StatusName", "Description" };
            DataTable dt = new DataTable();
            for (int i = 0; i < arrName.Length; i++)
            {
                dt.Columns.Add(arrName[i], typeof(string));
            }
            cfgFile.DataSource = dt;
        }

        private void EditTime(int iDateType)
        {
            if (iDateType == 5)
            {
                this.cboYear.SelectedIndexChanged -= new System.EventHandler(this.cboYear_SelectedIndexChanged);
                lblYear.Visible = true;
                cboYear.Visible = true;
                lblMonth.Visible = false;
                cboMonth.Visible = false;
                lblDate.Visible = false;
                dpkDate.Visible = false;
                // khai báo cbo thời gian
                InitcboNam();
                this.cboYear.SelectedIndexChanged += new System.EventHandler(this.cboYear_SelectedIndexChanged);
            }
            else if (iDateType == 4)
            {
                this.cboYear.SelectedIndexChanged -= new System.EventHandler(this.cboYear_SelectedIndexChanged);
                this.cboMonth.SelectedIndexChanged -= new System.EventHandler(this.cboMonth_SelectedIndexChanged);
                lblYear.Visible = true;
                cboYear.Visible = true;
                lblMonth.Visible = true;
                cboMonth.Visible = true;
                lblDate.Visible = false;
                dpkDate.Visible = false;
                // khai báo cbo thời gian
                InitcboNam();
                InitcboThang('q');
                this.cboYear.SelectedIndexChanged += new System.EventHandler(this.cboYear_SelectedIndexChanged);
                this.cboMonth.SelectedIndexChanged += new System.EventHandler(this.cboMonth_SelectedIndexChanged);
            }
            else if (iDateType == 3)
            {
                this.cboYear.SelectedIndexChanged -= new System.EventHandler(this.cboYear_SelectedIndexChanged);
                this.cboMonth.SelectedIndexChanged -= new System.EventHandler(this.cboMonth_SelectedIndexChanged);
                lblYear.Visible = true;
                cboYear.Visible = true;
                lblMonth.Visible = true;
                cboMonth.Visible = true;
                lblDate.Visible = false;
                dpkDate.Visible = false;
                // khai báo cbo thời gian
                InitcboNam();
                InitcboThang('m');
                this.cboYear.SelectedIndexChanged += new System.EventHandler(this.cboYear_SelectedIndexChanged);
                this.cboMonth.SelectedIndexChanged += new System.EventHandler(this.cboMonth_SelectedIndexChanged);
            }
            else if (iDateType == 2)
            {
                this.cboYear.SelectedIndexChanged -= new System.EventHandler(this.cboYear_SelectedIndexChanged);
                this.cboMonth.SelectedIndexChanged -= new System.EventHandler(this.cboMonth_SelectedIndexChanged);
                lblYear.Visible = true;
                cboYear.Visible = true;
                lblMonth.Visible = true;
                cboMonth.Visible = true;
                lblDate.Visible = false;
                dpkDate.Visible = false;
                // khai báo cbo thời gian
                InitcboNam();
                InitcboThang('w');
                this.cboYear.SelectedIndexChanged += new System.EventHandler(this.cboYear_SelectedIndexChanged);
                this.cboMonth.SelectedIndexChanged += new System.EventHandler(this.cboMonth_SelectedIndexChanged);
            }
            else
            {
                this.dpkDate.ValueChanged -= new System.EventHandler(this.dpkDate_ValueChanged);
                lblYear.Visible = false;
                cboYear.Visible = false;
                lblMonth.Visible = false;
                cboMonth.Visible = false;
                lblDate.Visible = true;
                dpkDate.Visible = true;
                this.dpkDate.ValueChanged += new System.EventHandler(this.dpkDate_ValueChanged);
            }
        }

        private void AddContextMenu_C1FlexGrid(ref C1FlexGrid c1Grid)
        {
            //Thêm Lệnh
            //_tspItem = new ToolStripMenuItem();
            //_tspItem.Name = "Download";
            //_tspItem.Text = "Lấy văn bản về máy";
            //_contextMenu.Items.Add(_tspItem);

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
            _tspItem.Text = "Văn bản thay thế";
            _contextMenu.Items.Add(_tspItem);

            //
            _tspItem = new ToolStripMenuItem();
            _tspItem.Name = "Download";
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

                    if (ibr == -1 && itr == -1)
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
                        int status = Convert.ToInt32(cfgFile.Rows[ibr]["Status"]);
                        frmXemVBThayTheNew frm = new frmXemVBThayTheNew();
                        frm.FileID = iFileID;
                        frm.ShowDialog();
                        //}
                    }

                    if (item.Name == "Download")
                    {
                        if (itr == ibr)
                        {
                            SaveFileDialog sfd = new SaveFileDialog();
                            string fileName = _bus.Q_CONFIG_GetRootFile() + cfgFile.Rows[itr]["FilePath"].ToString();
                            sfd.FileName = Path.GetFileName(fileName);
                            if (sfd.ShowDialog() == DialogResult.OK)
                            {
                                FileStream stream = File.OpenRead(fileName);
                                byte[] fileBytes = new byte[stream.Length];
                                stream.Read(fileBytes, 0, fileBytes.Length);
                                stream.Close();

                                //Lưu file
                                using (Stream file = File.OpenWrite(sfd.FileName))
                                {
                                    file.Write(fileBytes, 0, fileBytes.Length);
                                }
                                clsShare.Message_Info("Lấy văn bản thành công!");
                            }
                            return;
                        }

                        FolderBrowserDialog fbd = new FolderBrowserDialog();
                        fbd.Description = "Chọn thư mục lưu văn bản";
                        DialogResult result = fbd.ShowDialog();
                        string path = fbd.SelectedPath;

                        if (path == "")
                            return;

                        for (int i = itr; i <= ibr; i++)
                        {
                            if (Convert.ToInt32(cfgFile.Rows[i]["Status"]) != 1)
                                continue;
                            string fileName = _bus.Q_CONFIG_GetRootFile() + cfgFile.Rows[i]["FilePath"].ToString();
                            //Read file to byte array
                            FileStream stream = File.OpenRead(fileName);
                            byte[] fileBytes = new byte[stream.Length];
                            stream.Read(fileBytes, 0, fileBytes.Length);
                            stream.Close();

                            //Lưu file
                            using (Stream file = File.OpenWrite(path + "\\" + Path.GetFileName(fileName)))
                            {
                                file.Write(fileBytes, 0, fileBytes.Length);
                            }
                        }
                        clsShare.Message_Info("Lấy văn bản thành công!");
                    }
                }
            }
            catch (Exception ex)
            {
                clsShare.Message_Error(ex);
            }
        }

        private List<string> getWeak(int year)
        {
            var jan = new DateTime(year, 1, 1);
            //beware different cultures, see other answers
            var startOfFirstWeek = jan.AddDays(1 - (int)(jan.DayOfWeek));
            var weeks =
                Enumerable
                    .Range(0, 54)
                    .Select(i => new
                    {
                        weekStart = startOfFirstWeek.AddDays(i * 7)
                    })
                    .TakeWhile(x => x.weekStart.Year <= jan.Year)
                    .Select(x => new
                    {
                        x.weekStart,
                        weekFinish = x.weekStart.AddDays(6)
                    })
                    .SkipWhile(x => x.weekFinish < jan.AddDays(1))
                    .Select((x, i) => new
                    {
                        x.weekStart,
                        x.weekFinish,
                        weekNum = i + 1
                    });
            var ListNum = (from w in weeks select w.weekNum).ToList();
            var ListWeekStart = (from w in weeks select w.weekStart).ToList();
            var ListWeekEnd = (from w in weeks select w.weekFinish).ToList();

            List<string> list = new List<string>();
            for (int i = 0; i < ListNum.Count; i++)
            {
                string tuan = "Tuần " + ListNum[i] + " từ ngày " + ListWeekStart[i].ToString("dd/MM/yyyy") + " đến ngày " + ListWeekEnd[i].ToString("dd/MM/yyyy");
                list.Add(tuan);
            }

            return list;
        }
    }
}

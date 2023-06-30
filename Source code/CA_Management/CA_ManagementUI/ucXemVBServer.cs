using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using ES.CA_ManagementBUS;
using Telerik.WinControls.UI;
using C1.Win.C1FlexGrid;
using System.Security.Cryptography.X509Certificates;

namespace ES.CA_ManagementUI
{
    public partial class ucXemVBServer : UserControl
    {
        BUSQuanTri _bus = new BUSQuanTri();
        DataTable _daSource = null;

        public DataTable DaSource
        {
            get { return _daSource; }
            set { _daSource = value; }
        }

        private int LoaiVanBan
        {
            get { return Convert.ToInt32(cboLoaiVanBan.SelectedValue); }
            set { cboLoaiVanBan.SelectedValue = value; }
        }

        public int DonVi
        {
            get { return Convert.ToInt32(cboDonVi.SelectedValue); }
            set { cboDonVi.SelectedValue = value; }
        }

        public ucXemVBServer()
        {
            InitializeComponent();
        }

        private void ucXemVBServer_Load(object sender, EventArgs e)
        {
            try
            {
                dpkBegin.Format = DateTimePickerFormat.Custom;
                dpkEnd.Format = DateTimePickerFormat.Custom;

                InitLoaiVB();
                InitDonViByVB(LoaiVanBan);
                InitGridVanBan();
                LoadDataToGrid();

                // Thêm sự kiện KeyDown
                cfgVanBan1.KeyDown += new KeyEventHandler(clsShare.C1FlexGrid_KeyDown);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// lấy dư liệu lên combobox Loại Văn Bản
        /// </summary>
        private void InitLoaiVB()
        {
            DataTable da = _bus.FL_FileType_SelectAll();
            cboLoaiVanBan.ValueMember = "FileTypeID";
            cboLoaiVanBan.DisplayMember = "Name";
            cboLoaiVanBan.DataSource = da;
            cboLoaiVanBan.SelectedIndex = 0;
        }

        /// <summary>
        /// Lấy dữ liệu lên combobox Đơn Vị theo Loại Văn Bản
        /// </summary>
        /// <param name="idLoaiVanBan">Id Loại văn bản cần lọc</param>
        private void InitDonViByVB(int idLoaiVanBan)
        {
            cboDonVi.ValueMember = "UnitID";
            cboDonVi.DisplayMember = "Notation";
            cboDonVi.DataSource = _bus.CA_Unit_SelectByFileTypeID(idLoaiVanBan);
            cboDonVi.SelectedIndex = 0;
        }

        /// <summary>
        /// Chèn dữ liệu và cấu hình cho grid 
        /// </summary>
        private void InitGridVanBan()
        {
            //cấu hình grid
            cfgVanBan1.Cols.Fixed = 0;
            cfgVanBan1.AllowSorting = AllowSortingEnum.SingleColumn;

            //Định dạng cho các cột                
            string[] arrname1 = { "STT", "FileNumber", "FilePath", "StatusName", "Description", "FileID", "LogTrangThai", 
                                        "LogKy", "VanBanHuy", "LayFile", "Status" };
            string[] arrheader = { "STT", "Số văn bản", "Tên file", "Trạng thái", "Mô tả", "FileID", "Log trạng thái", 
                                         "Log ký", "Văn bản thay thế", "Lấy file", "Status" };

            for (int i = 0; i < arrname1.Length; i++)
            {
                // tên và header
                cfgVanBan1.Cols[i].Name = arrname1[i];
                //cfgVanBan1.Cols[i].UserData = arrname[i];
                cfgVanBan1.Cols[i].Caption = arrheader[i];
                cfgVanBan1.Cols[i].TextAlign = TextAlignEnum.CenterCenter;
                cfgVanBan1.Cols[i].AllowEditing = true;

                // căn lề
                if (i == 0 || i == 3 || i == 7 || i == 8 || i == 9)
                {
                    cfgVanBan1.Cols[i].TextAlign = TextAlignEnum.CenterCenter;
                    cfgVanBan1.Cols[i].AllowEditing = true;
                }
                else
                {
                    cfgVanBan1.Cols[i].TextAlign = TextAlignEnum.LeftCenter;
                    cfgVanBan1.Cols[i].AllowEditing = false;
                }

            }

            cfgVanBan1.Cols["FileID"].Visible = false;
            cfgVanBan1.Cols["Status"].Visible = false;

            //định chiều rộng của cột
            cfgVanBan1.Cols[0].Width = 50;
            cfgVanBan1.Cols[1].Width = 200;
            cfgVanBan1.Cols[2].Width = 200;
            cfgVanBan1.Cols[3].Width = 90;
            cfgVanBan1.Cols[4].Width = 150;
            cfgVanBan1.Cols[5].Width = 90;
            cfgVanBan1.Cols[6].Width = 100;
            cfgVanBan1.Cols[7].Width = 90;
            cfgVanBan1.Cols[8].Width = 90;
            cfgVanBan1.Cols[9].Width = 90;

            //ke chan cho chu
            cfgVanBan1.Cols[6].Style.Font = new Font("Times New Roman", 10, FontStyle.Underline);
            cfgVanBan1.Cols[7].Style.Font = new Font("Times New Roman", 10, FontStyle.Underline);
            cfgVanBan1.Cols[8].Style.Font = new Font("Times New Roman", 10, FontStyle.Underline);
            cfgVanBan1.Cols[9].Style.Font = new Font("Times New Roman", 10, FontStyle.Underline);

            //tiêu đề cân giữa
            cfgVanBan1.Rows[0].TextAlign = TextAlignEnum.CenterCenter;
        }

        private void cboLoaiVanBan_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                InitDonViByVB(Convert.ToInt32(cboLoaiVanBan.SelectedValue));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                LoadDataToGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cfgVanBan1_Click(object sender, EventArgs e)
        {
            try
            {
                if (((DataTable)cfgVanBan1.DataSource).Rows.Count > 0)
                {
                    DataRow dr = _daSource.Rows[cfgVanBan1.Row - 1];
                    int iFileID = Convert.ToInt32(dr["FileID"]);

                    //Xử lý khi click vào ô tương ứng
                    if (cfgVanBan1.Col == 6)
                    {
                        frmXemLogStatusServer frm = new frmXemLogStatusServer(iFileID);
                        frm.ShowDialog();
                    }
                    if (cfgVanBan1.Col == 7)
                    {
                        frmXemLogKy frm = new frmXemLogKy(iFileID);
                        frm.ShowDialog();
                    }
                    if (cfgVanBan1.Col == 8)
                    {
                        int status = Convert.ToInt32(dr["Status"]);
                        if (status == 4)
                        {
                            frmXemVBThayTheNew frm = new frmXemVBThayTheNew();
                            frm.FileID = iFileID;
                            frm.ShowDialog();
                        }
                        else
                        {
                            //MessageBox.Show("khong duoc xem");
                        }

                    }
                    if (cfgVanBan1.Col == 9)
                    {
                        SaveFileDialog sfd = new SaveFileDialog();
                        string fileName = _bus.Q_CONFIG_GetRootFile() + dr["FilePath"].ToString();
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
                clsShare.Message_Error("Lỗi trong quá trình lấy dữ liệu:\r\n" + ex.Message);
            }
        }

        private void LoadDataToGrid()
        {
            //Lấy dữ liệu
            DataTable da = _bus.FL_File_SelectByFileTypeId_UnitId(LoaiVanBan, DonVi, dpkBegin.Value.Date, dpkEnd.Value.Date);

            //Thêm các cột để Click
            da.Columns.Add("LogTrangThai", typeof(string));
            da.Columns.Add("LogKy", typeof(string));
            da.Columns.Add("VanBanHuy", typeof(string));
            da.Columns.Add("LayFile", typeof(string));

            foreach (DataRow item in da.Rows)
            {
                item["LogTrangThai"] = "Xem trạng thái";
                item["LogKy"] = "Xem log ký";
                item["VanBanHuy"] = "Xem văn bản";
                item["LayFile"] = "Lấy file";
            }

            //Đổ dữ liệu
            _daSource = da;
            cfgVanBan1.DataSource = da;
        }
    }
}

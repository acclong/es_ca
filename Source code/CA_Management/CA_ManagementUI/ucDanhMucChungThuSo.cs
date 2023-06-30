using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Security.Permissions;
using Telerik.WinControls;
using Telerik.WinControls.UI;
using C1.Win.C1FlexGrid;
using ES.CA_ManagementBUS;

namespace ES.CA_ManagementUI
{
    public partial class ucDanhMucChungThuSo : UserControl
    {
        #region Var

        private ContextMenuStrip _contextMenu = new ContextMenuStrip();
        private ToolStripMenuItem _tspItem;

        #endregion

        BUSQuanTri _bus = new BUSQuanTri();

        public ucDanhMucChungThuSo()
        {
            InitializeComponent();
        }

        private void ucDanhMucChungThuSo_Load(object sender, EventArgs e)
        {
            try
            {
                AddContextMenu_C1FlexGrid(ref cfgCertificates);
                LoadData();
                InitCboStatus();
                InitRgvCertificates();

                // Thêm sự kiên KeyDown
                cfgCertificates.KeyDown += new KeyEventHandler(clsShare.C1FlexGrid_KeyDown);
            }
            catch (Exception ex)
            {
                clsShare.Message_Error(ex);
            }
        }

        #region Init

        private void InitCboStatus()
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

            cboStatus.DataSource = dt;
            cboStatus.DisplayMember = "Name";
            cboStatus.ValueMember = "ID";

            cboStatus.SelectedIndex = 0;
        }

        private void InitRgvCertificates()
        {
            //cấu hình cột
            cfgCertificates.ExtendLastCol = true;
            cfgCertificates.Cols.Count = 13;
            cfgCertificates.Cols.Fixed = 1;
            cfgCertificates.AllowSorting = AllowSortingEnum.SingleColumn;

            cfgCertificates.Cols[0].Width = 20;

            //Edited by Toantk on 16/4/2015
            //Thêm trường STT và ẩn cột ID
            string[] arrName = new string[] { "STT", "CertID", "NameCN", "Serial", "Status","Issuer", "IssuerID",  "Status_Text", "CertType", "ValidFrom", "ValidTo", "XemChungThu" };
            string[] arrHeader = new string[] { "STT", "CertID", "Tên chứng thư", "Số Serial", "Status", "Nhà cung cấp", "IssuerID", "Trạng thái", "Loại", "Hiệu lực từ", "Hiệu lực đến","Xem chứng thư" };
            
            for (int i = 0; i < arrName.Length; i++)
            {

                #region C1
                // tên và header
                cfgCertificates.Cols[i + 1].Name = arrName[i];
                cfgCertificates.Cols[i + 1].Caption = arrHeader[i];
                // căn lề cột
                if (i == 2 || i == 3 || i == 5)
                    cfgCertificates.Cols[i + 1].TextAlign = TextAlignEnum.LeftCenter;
                else
                    cfgCertificates.Cols[i + 1].TextAlign = TextAlignEnum.CenterCenter;
                // kích thước cột và format
                switch (i)
                {
                    case 0: cfgCertificates.Cols[i + 1].Width = 40; break;
                    case 2: cfgCertificates.Cols[i + 1].Width = 250; break;
                    case 3: cfgCertificates.Cols[i + 1].Width = 230; break;
                    case 7: cfgCertificates.Cols[i + 1].Width = 90; break;
                    case 5: cfgCertificates.Cols[i + 1].Width = 100; break;
                    case 8:
                    case 9: 
                    case 10: cfgCertificates.Cols[i + 1].Width = 90; break;
                    case 11: cfgCertificates.Cols[i + 1].Width = 100; break;
                }
                if (i == 9 || i == 10)
                {
                    cfgCertificates.Cols[i + 1].Format = "dd/MM/yyyy";
                    cfgCertificates.Cols[i + 1].AllowFiltering = AllowFiltering.ByCondition;
                }
                else
                {
                    cfgCertificates.Cols[i + 1].AllowFiltering = AllowFiltering.ByValue;
                }

                //Edited by Toantk on 16/4/2015
                if (i == 11)
                    cfgCertificates.Cols[i + 1].Style.Font = new Font("Times New Roman", 10, FontStyle.Underline);

                #endregion
            }

            // ẩn các cột không cần thiết
            cfgCertificates.Cols["CertID"].Visible = false;
            cfgCertificates.Cols["Status"].Visible = false;
            cfgCertificates.Cols["IssuerID"].Visible = false;

            cfgCertificates.Cols["Issuer"].AllowFiltering = AllowFiltering.ByValue;

            // căn giừa hàng đầu
            cfgCertificates.Rows[0].TextAlign = TextAlignEnum.CenterCenter;

        }

        private void AddContextMenu_C1FlexGrid(ref C1FlexGrid c1Grid)
        {
            //Thêm Lệnh
            _tspItem = new ToolStripMenuItem();
            _tspItem.Name = "LogCert";
            _tspItem.Text = "Lịch sử chứng thư số";
            _contextMenu.Items.Add(_tspItem);

            //Thêm Lệnh
            _tspItem = new ToolStripMenuItem();
            _tspItem.Name = "ViewCert";
            _tspItem.Text = "Xem chứng thư số";
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
            //DataTable dt = _bus.CA_Certificate_SelectAll();
            DataTable dt = _bus.CA_Certificate_SelectBy_Seach_Status("",-1);
            dt.Columns.Add("XemChungThu", typeof(string));
            foreach (DataRow item in dt.Rows)
            {
                item["XemChungThu"] = "Xem chứng thư";
            }
            cfgCertificates.DataSource = dt;
        }

        #endregion

        #region Event

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                frmThemSuaChungThuSo frm = new frmThemSuaChungThuSo();
                frm.Text = "Thêm chứng thư số";
                frm.CertID = 0;
                frm.ShowDialog();

                btnRefresh_Click(null, null);
            }
            catch (Exception ex)
            {
                clsShare.Message_Error(ex);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                if (cfgCertificates.Row == cfgCertificates.RowSel)
                    if (cfgCertificates.Row > 0)
                    {
                        frmThemSuaChungThuSo frm = new frmThemSuaChungThuSo();
                        frm.Text = "Sửa chứng thư số";
                        frm.CertID = Convert.ToInt32(cfgCertificates.Rows[cfgCertificates.Row]["CertID"]);
                        frm.ShowDialog();

                        btnRefresh_Click(null,null);

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
                bool check = false;
                string info = "Bạn có chắc muốn xóa chứng thư [" + cfgCertificates.Rows[cfgCertificates.Row]["NameCN"] + "] không?";
                //DialogResult result = MessageBox.Show(info, "Xóa chứng thư số", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (clsShare.Message_WarningYN(info))
                {
                    int certID = Convert.ToInt16(cfgCertificates.Rows[cfgCertificates.Row]["CertID"]);
                    if (bus.CA_Certificate_DeleteBy_CertID(certID))
                    {
                        clsShare.Message_Info("Xóa chứng thư số thành công!");
                        btnRefresh_Click(null, null);
                    }
                    else
                    {
                        clsShare.Message_Warning("Không thể xóa do chứng thư đã được sử dụng!");
                    }
                }
            }catch(Exception ex)
            {
                clsShare.Message_Error(ex.Message);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = _bus.CA_Certificate_SelectBy_Seach_Status(txtSeach.Text, Convert.ToInt32(cboStatus.SelectedValue));
                dt.Columns.Add("XemChungThu", typeof(string));
                foreach (DataRow item in dt.Rows)
                {
                    item["XemChungThu"] = "Xem chứng thư";
                }
                cfgCertificates.DataSource = dt;
            }
            catch (Exception ex) { clsShare.Message_Error(ex.Message); }
        }

        private void cfgCertificates_DoubleClick(object sender, EventArgs e)
        {
            if (cfgCertificates.Row == cfgCertificates.RowSel)
			{
				if (cfgCertificates.Cols[cfgCertificates.Col].Name == "XemChungThu")
                {
                    // lấy giá trị CertAuthID tương ứng
                    int iCertID = Convert.ToInt32(cfgCertificates.Rows[cfgCertificates.Row]["CertID"]);

                    // lấy dữ liệu từ db
                    byte[] rawData = _bus.CA_Certificate_SelectRawDataByID(iCertID);
                    // show thông tin Certificate
                    X509Certificate2 cert = new X509Certificate2(rawData);
                    X509Certificate2UI.DisplayCertificate(cert);
                }
				else
					btnSua_Click(null, null);
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
                    if (item.Name == "LogCert")
                    {
                        _contextMenu.Close();
                        frmXemLogDanhMuc frm = new frmXemLogDanhMuc();
                        frm.Text = "Lịch sử log chứng thư số";
                        frm.Log = frmXemLogDanhMuc.TypeLog.Cert;
                        int certID = Convert.ToInt32(cfgCertificates.Rows[ibr]["CertID"]);
                        frm.CertID = certID;
                        frm.ShowDialog();
                    }
                    else if (item.Name == "ViewCert")
                    {
                        _contextMenu.Close();
                        // lấy giá trị CertAuthID tương ứng
                        int iCertID = Convert.ToInt32(cfgCertificates.Rows[cfgCertificates.Row]["CertID"]);

                        // lấy dữ liệu từ db
                        byte[] rawData = _bus.CA_Certificate_SelectRawDataByID(iCertID);
                        // show thông tin Certificate
                        X509Certificate2 cert = new X509Certificate2(rawData);
                        X509Certificate2UI.DisplayCertificate(cert);
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

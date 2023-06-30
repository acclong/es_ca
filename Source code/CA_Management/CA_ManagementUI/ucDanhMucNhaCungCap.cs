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
    public partial class ucDanhMucNhaCungCap : UserControl
    {
        BUSQuanTri _bus = new BUSQuanTri();

        public ucDanhMucNhaCungCap()
        {
            InitializeComponent();
        }

        private void ucCauHinhNhaCungCap_Load(object sender, EventArgs e)
        {
            try
            {
                LoadData();
                InitRgvCertificates();

                // Thêm sự kiện KeyDown
                cfgCertificates.KeyDown += new KeyEventHandler(clsShare.C1FlexGrid_KeyDown);
            }
            catch (Exception ex)
            {
                clsShare.Message_Error(ex);
            }
        }

        #region Init

        private void InitRgvCertificates()
        {
            //cấu hình cột
            cfgCertificates.ExtendLastCol = true;
            cfgCertificates.Cols.Fixed = 1;
            cfgCertificates.Cols[0].Width = 25;

            //Edited by Toantk on 16/4/2015
            //Thêm trường STT và ẩn cột ID
            string[] arrName = new string[] { "STT", "CertAuthID", "NameCN", "Serial", "Subject", "ValidFrom", "ValidTo", "CRL_URL", "Xem" };
            string[] arrHeader = new string[] { "STT", "CertAuthID", "Tên nhà cung cấp", "Số Serial", "Subject", "Hiệu lực từ", "Hiệu lực đến", "Crl url", "Xem" };

            for (int i = 0; i < arrName.Length; i++)
            {
                #region C1
                // Tên và Header
                cfgCertificates.Cols[i + 1].Name = arrName[i];
                cfgCertificates.Cols[i + 1].Caption = arrHeader[i];
                // Căn lề cột
                if (i == 2 || i == 3 || i == 7)
                    cfgCertificates.Cols[i + 1].TextAlign = TextAlignEnum.LeftCenter;
                else
                    cfgCertificates.Cols[i + 1].TextAlign = TextAlignEnum.CenterCenter;
                // Kích thước và format cột
                if (i == 0)
                    cfgCertificates.Cols[i + 1].Width = 50;
                if (i == 2)
                    cfgCertificates.Cols[i + 1].Width = 200;
                if (i == 3)
                    cfgCertificates.Cols[i + 1].Width = 250;
                if (i == 7)
                    cfgCertificates.Cols[i + 1].Width = 200;
                if (i == 8)
                    cfgCertificates.Cols[i + 1].Width = 75;
                if (i == 5 || i == 6)
                {
                    cfgCertificates.Cols[i + 1].Format = "dd/MM/yyyy";
                    cfgCertificates.Cols[i + 1].Width = 100;
                    cfgCertificates.Cols[i + 1].AllowFiltering = AllowFiltering.ByCondition;
                }
                else
                {
                    cfgCertificates.Cols[i + 1].AllowFiltering = AllowFiltering.ByValue;
                }
                if (i == 8)
                    cfgCertificates.Cols[i + 1].Style.Font = new Font("Times New Roman", 10, FontStyle.Underline);

                #endregion
            }

            cfgCertificates.Cols["ValidTo"].Format = "dd/MM/yyyy";
            cfgCertificates.Cols["CertAuthID"].Visible = false;
            cfgCertificates.Cols["Subject"].Visible = false;

            //cfgCertificates.Cols["ValidTo"].AllowFiltering = AllowFiltering.Default; 
            //cfgCertificates.Cols["ValidFrom"].AllowFiltering = AllowFiltering.Default; 

            // căn giừa hàng đầu
            cfgCertificates.Rows[0].TextAlign = TextAlignEnum.CenterCenter;
            //cfgCertificates.Rows[0].Style.Font = new Font("Times New Roman", 11, FontStyle.Bold);
        }

        #endregion

        #region Data

        private void LoadData()
        {
            DataTable dt = _bus.CA_CertificationAuthority_SelectAll();
            dt.Columns.Add("Xem", typeof(string));
            foreach (DataRow item in dt.Rows)
            {
                item["Xem"] = "Xem chứng thư";
            }
            cfgCertificates.DataSource = dt;
        }

        private void Click()
        {
            try
            {
                if (cfgCertificates.Row == cfgCertificates.RowSel)
                    if (cfgCertificates.Col == 9)
                    {

                        // lấy giá trị CertAuthID tương ứng
                        int iCertAuthID = Convert.ToInt32(cfgCertificates.Rows[cfgCertificates.Row]["CertAuthID"]);

                        // lấy dữ liệu từ db
                        DataTable dtRawData = _bus.CA_CertificationAuthority_SelectRawDataByID(iCertAuthID);

                        // show thông tin Certificate
                        byte[] rawData = (byte[])dtRawData.Rows[0]["RawData"];
                        X509Certificate2 cert = new X509Certificate2(rawData);
                        X509Certificate2UI.DisplayCertificate(cert);
                    }
            }
            catch (Exception ex)
            {
                clsShare.Message_Error(ex);
            }
        }

        #endregion

        #region Controls

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                //ColumnFilter colFilterTo = new ColumnFilter();
                //if (cfgCertificates.Cols["ValidTo"].Filter != null)
                //{
                //    colFilterTo = ((ColumnFilter)(cfgCertificates.Cols["ValidTo"].Filter));
                //}

                //ColumnFilter colFilterFrom = new ColumnFilter();
                //if (cfgCertificates.Cols["ValidFrom"].Filter != null)
                //{
                //    colFilterFrom = ((ColumnFilter)(cfgCertificates.Cols["ValidFrom"].Filter));
                //}

                frmThemSuaNhaCungCapCA frm = new frmThemSuaNhaCungCapCA();
                frm.Text = "Thêm nhà cung cấp CA";
                frm.CertAuthID = 0;
                frm.ShowDialog();

                LoadData();

                //if (colFilterTo.ValueFilter.ShowValues != null)
                //    cfgCertificates.Cols["ValidTo"].Filter = colFilterTo;

                //if (colFilterFrom.ValueFilter.ShowValues != null)
                //    cfgCertificates.Cols["ValidFrom"].Filter = colFilterFrom;
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
                        //ColumnFilter colFilterTo = new ColumnFilter();
                        //if (cfgCertificates.Cols["ValidTo"].Filter != null)
                        //{
                        //    colFilterTo = ((ColumnFilter)(cfgCertificates.Cols["ValidTo"].Filter));
                        //}

                        //ColumnFilter colFilterFrom = new ColumnFilter();
                        //if (cfgCertificates.Cols["ValidFrom"].Filter != null)
                        //{
                        //    colFilterFrom = ((ColumnFilter)(cfgCertificates.Cols["ValidFrom"].Filter));
                        //}

                        frmThemSuaNhaCungCapCA frm = new frmThemSuaNhaCungCapCA();
                        frm.Text = "Sửa nhà cung cấp CA";
                        //frm.CertAuthID = Convert.ToInt32(rgvCertificates.SelectedRows[0].Cells["CertAuthID"].Value);
                        frm.CertAuthID = Convert.ToInt32(cfgCertificates.Rows[cfgCertificates.Row]["CertAuthID"]);
                        frm.ShowDialog();

                        LoadData();

                        //if (colFilterTo.ValueFilter.ShowValues != null)
                        //    cfgCertificates.Cols["ValidTo"].Filter = colFilterTo;

                        //if (colFilterFrom.ValueFilter.ShowValues != null)
                        //    cfgCertificates.Cols["ValidFrom"].Filter = colFilterFrom;
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
                if (clsShare.Message_WarningYN("Bạn có chắc muốn xóa nhà cung cấp chứng thư [" + cfgCertificates.Rows[cfgCertificates.Row]["NameCN"] + "] không?"))
                {
                    //ColumnFilter colFilterTo = new ColumnFilter();
                    //if (cfgCertificates.Cols["ValidTo"].Filter != null)
                    //{
                    //    colFilterTo.ValueFilter.ShowValues = ((ColumnFilter)(cfgCertificates.Cols["ValidTo"].Filter)).ValueFilter.ShowValues;
                    //}

                    //ColumnFilter colFilterFrom = new ColumnFilter();
                    //if (cfgCertificates.Cols["ValidFrom"].Filter != null)
                    //{
                    //    colFilterFrom.ValueFilter.ShowValues = ((ColumnFilter)(cfgCertificates.Cols["ValidFrom"].Filter)).ValueFilter.ShowValues;
                    //}

                    int certAuthID = Convert.ToInt16(cfgCertificates.Rows[cfgCertificates.Row]["CertAuthID"]);
                    if (bus.CA_CertificationAuthority_DeleteBy_CertAuthID(certAuthID))
                    {

                        clsShare.Message_Info("Xóa nhà cung cấp chứng thư thành công!");
                        //clsShare.
                        LoadData();

                        //if (colFilterTo.ValueFilter.ShowValues != null)
                        //    cfgCertificates.Cols["ValidTo"].Filter = colFilterTo;

                        //if (colFilterFrom.ValueFilter.ShowValues != null)
                        //    cfgCertificates.Cols["ValidFrom"].Filter = colFilterFrom;
                    }
                    else
                    {
                        clsShare.Message_Warning("Không thể xóa do nhà cung cấp chứng thư đã được sử dụng!");
                    }
                }
            }
            catch(Exception ex)
            {
                clsShare.Message_Error(ex.Message);
            }
        }

        #endregion

        #region Event
        private void cfgCertificates_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (cfgCertificates.Col == 9)
                {
                    // lấy giá trị CertAuthID tương ứng
                    int iCertAuthID = Convert.ToInt32(cfgCertificates.Rows[cfgCertificates.Row]["CertAuthID"]);

                    // lấy dữ liệu từ db
                    DataTable dtRawData = _bus.CA_CertificationAuthority_SelectRawDataByID(iCertAuthID);

                    // show thông tin Certificate
                    byte[] rawData = (byte[])dtRawData.Rows[0]["RawData"];
                    X509Certificate2 cert = new X509Certificate2(rawData);
                    X509Certificate2UI.DisplayCertificate(cert);
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

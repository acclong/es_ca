using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Data;
using ClassBUS;
using ClassDAL;
using esDigitalSignature;
using System.IO;
using esDigitalSignature.Library;
using System.Text;

namespace WebSignTest
{
    public partial class _Default : Page
    {
        //Lưu ý: Cần ném vào hàm Contant
        //Tham số cho quá trình ký

        string[] arrXNSK = { "FileID", "ObjNull", "ConnectString", "StoreName", "FileUploadID", "DVI_XNHAN" };
        string[] arrSMOV = { "FileID", "ObjNull", "ConnectString", "StoreName", "PDKy_ID", "FileNumber", "FileType", "Status", "FileDate" };
        string[] arrULSK = { "FileData", "FilePath", "FileMaDV", "FileDate", "Description", "ConnectString", "StoreName",
                                   "NM_ID", "Nam", "Thang", "FileTypeID", "FileID", "FilePath", "FileName", "Nguoi_Upload", "TrangThai_PheDuyet" };
        string[] arrDKCA = { "FileData", "FilePath", "ConnectString", "StoreName", "UserModified", "Status", "FileConfirm", "Registration_ID", "lstRegID" };

        public enum TypeLink
        {
            Internet,
            WLAN
        }

        public enum TypeSignOverWeb
        {
            HaveFileID,
            CreateFileServer,
            SaveFileInDB
        }

        private enum TypeOutputToDB
        {
            CreateFile,
            Base64,
            FileDataSign,
            FileID,
            FilePath,
            FileName,
            Other
        }

        int iTimeout = 10;
        int iMaxFile = 10;
        string sProtocolName = "TTDSignApp";

        public string connectString = ConfigurationManager.ConnectionStrings["File"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                lblErr.Text = "";
                if (!IsPostBack)
                {
                    RefreshSMOV();
                    RefreshBKN();
                    RefreshDKCA();
                }
            }
            catch (Exception ex)
            {
                lblErr.Text = ex.Message;
            }
        }

        #region SMOV
        protected void btnSign_Click(object sender, EventArgs e)
        {
            if (btnSign.Text == "Ký nào")
            {
                if (SignSMOV())
                    btnSign.Text = "Refresh";
            }
            else
            {
                btnSign.Text = "Ký nào";
                RefreshSMOV();
            }
        }

        public void RefreshSMOV()
        {
            // lấy thông tin file từ db
            lblErr.Text = "";
            Database db = new SqlDatabase(connectString);
            SqlConnection conn = new SqlConnection(connectString);
            DataTable dtFile = new DataTable();
            conn.Open();
            try
            {
                string sqlSelect = "Select FileID, 5632 as PDK_ID, FileNumber, 204 as FileType from FL_File where FileID = 3054";
                dtFile.Load(db.ExecuteReader(CommandType.Text, sqlSelect));
                conn.Close();
            }
            catch (Exception ex)
            {
                if (conn != null && conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                lblErr.Text = ex.Message;
            }

            grvFile.DataSource = dtFile;
            grvFile.DataBind();
        }

        private bool SignSMOV()
        {
            try
            {
                //Tạo bảng đầu vào
                string[] arrSMOV = { "FileID", "PDKy_ID", "FileNumber", "FileType", "Status" };
                DataTable dtInput = new DataTable();
                for (int i = 0; i < arrSMOV.Length; i++)
                {
                    dtInput.Columns.Add(arrSMOV[i]);
                }
                //Lấy fileID cần ký
                foreach (GridViewRow row in grvFile.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        // Access the CheckBox
                        CheckBox cb = row.Cells[0].FindControl("SelectedFile") as CheckBox;
                        if (cb != null && cb.Checked)
                        {
                            DataRow dr = dtInput.NewRow();
                            dr[0] = row.Cells[1].Text;
                            dr[1] = row.Cells[2].Text;
                            dr[2] = row.Cells[3].Text;
                            dr[3] = row.Cells[4].Text;
                            dr[4] = 1;
                            dtInput.Rows.Add(dr);
                        }
                    }
                }
                if (dtInput.Rows.Count == 0)
                {
                    lblErr.Text = "Quá trình khởi tạo ký thất bại!\nChưa chọn văn bản để ký!!";
                    return false;
                }
                return true;
                //if (SendDataToClientForSign(dtInput, "donvi_xacnhan", "SMOV", "SMOV"))
                //    return true;
                //else
                //    return false;
            }
            catch (Exception ex)
            {
                lblErr.Text = ex.Message;
                return false;
            }
        }
        #endregion

        #region BKN
        protected void btnSignBKN_Click(object sender, EventArgs e)
        {
            if (btnSignBKN.Text == "Ký nào")
            {
                if (SignBKN())
                    btnSignBKN.Text = "Refresh";
            }
            else
            {
                btnSignBKN.Text = "Ký nào";
                RefreshBKN();
            }
        }

        public void RefreshBKN()
        {
            // lấy thông tin file từ db
            lblErr.Text = "";
            txtMa_NM.Text = "G25600";
            txtFileID.Text = "4369";
            //txtMa_NM.Text = "G21900";
            //txtMa_NM.Text = "4370";
            Ngay.TodaysDate = new DateTime(2014, 7, 8);
        }

        private bool SignBKN()
        {
            try
            {
                //Tạo bảng đầu vào
                string[] arrBangKeNgay = { "ConnectString", "StoreName", "Ma_NM", "Ngay", "LanXacThuc", "NM_XacThuc", "NM_LyDo",
                                             "NM_NguoiXacThuc", "NM_NgayXacThuc", "User" };
                //Bảng sẽ nhiều hơn phần cập nhật cơ sở dư liệu sau khi ký trường fileID
                DataTable dtInputDB = CreateInputDB();
                DataTable dtInputSign = CreateInputSign();

                DataRow drSign = dtInputSign.NewRow();
                drSign[0] = null;
                drSign[1] = "";
                drSign[2] = Convert.ToInt32(txtFileID.Text);
                drSign[3] = 0;
                dtInputSign.Rows.Add(drSign);

                //Lấy fileID cần ký
                for (int i = 0; i < arrBangKeNgay.Length; i++)
                {
                    DataRow dr = dtInputDB.NewRow();
                    dr[0] = arrBangKeNgay[i];
                    dr[2] = (int)TypeOutputToDB.Other;
                    dr[3] = 0;

                    if(i == 0)
                        dr[1] = ConfigurationManager.ConnectionStrings["TinhToanThanhToan"].ConnectionString;
                    else if (i == 1)
                        dr[1] = "TT_XacThucBangKeAdd_Web_NM";
                    else if (i == 2)
                        dr[1] = txtMa_NM.Text;
                    else if (i == 3)
                        dr[1] = Ngay.SelectedDate;
                    else if (i == 4)
                        dr[1] = 1;
                    else if (i == 5)
                        dr[1] = true;
                    else if (i == 6)
                        dr[1] = "";
                    else if (i == 7)
                        dr[1] = "donviCA";
                    else if (i == 8)
                        dr[1] = DateTime.Now;
                    else if (i == 9)
                        dr[1] = "donviCA";
                    dtInputDB.Rows.Add(dr);
                }

                if (dtInputDB.Rows.Count == 0)
                {
                    lblErr.Text = "Quá trình khởi tạo ký thất bại!\nChưa chọn văn bản để ký!!";
                    return false;
                }
                string typeSign = TypeLink.WLAN.ToString() + "." + TypeSignOverWeb.HaveFileID.ToString() + "." + FileTypes.BangKeDoanhThuThiTruongNgay.ToString();
                if (SendDataToClientForSign(dtInputSign, dtInputDB, "donvi_CA", "WebTTD", typeSign))
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                lblErr.Text = ex.Message;
                return false;
            }
        }
        #endregion

        #region DKCA
        protected void btnSignDKCA_Click(object sender, EventArgs e)
        {
            if (btnSign.Text == "Ký nào")
            {
                if (SignDKCA())
                    btnSignDKCA.Text = "Refresh";
            }
            else
            {
                btnSignDKCA.Text = "Ký nào";
                RefreshDKCA();
            }
        }

        public void RefreshDKCA()
        {
            lblContent.Text = "Điều khó tin nổi là Bồ Đào Nha lọt vào được chung kết Euro 2016 và sẽ chơi trận tranh chức vô địch với chủ nhà Pháp, đã trở thành hiện thực. Bồ Đào Nha, nhìn về tổng thể, là một đội bóng nghèo nàn hơn nhiều đội ở Euro năm nay, và điều duy nhất khiến đội tuyển này được quan tâm là bởi Cristiano Ronaldo.";
        }

        private bool SignDKCA()
        {
            try
            {
                //Tạo bảng đầu vào
                DataTable dtInputDB = CreateInputDB();
                DataTable dtInputSign = CreateInputSign();

                string[] arrDangKyCA = { "ConnectString", "StoreName", "UserModified", "Status", "FileConfirm", "Registration_ID", "RegID" };

                DataRow drSign = dtInputSign.NewRow();
                drSign[0] = Common.ConvertTextToPDF(lblContent.Text);
                drSign[1] = "UploadFileSK" + DateTime.Now.ToString("yyyyMMddHHmmssffff") + ".pdf";
                drSign[2] = -1;
                drSign[3] = 0;
                dtInputSign.Rows.Add(drSign);

                for (int i = 0; i < arrDangKyCA.Length; i++)
                {
                    DataRow dr = dtInputDB.NewRow();
                    dr[0] = arrDangKyCA[i];
                    dr[2] = (int)TypeOutputToDB.Other;
                    dr[3] = 0;

                    if (i == 0)
                        dr[1] = ConfigurationManager.ConnectionStrings["File"].ConnectionString;
                    else if (i == 1)
                        dr[1] = "CA_Registration_InsertNew_UpdateDetail";
                    else if (i == 2)
                        dr[1] = "donviCA";
                    else if (i == 3)
                        dr[1] = 2;
                    else if (i == 4)
                    {
                        dr[1] = "";
                        dr[2] = (int)TypeOutputToDB.FileDataSign;
                    }
                    else if (i == 5)
                        dr[1] = -1;
                    else if (i == 6)
                        dr[1] = "60;61;62";
                    dtInputDB.Rows.Add(dr);
                }

                if (dtInputDB.Rows.Count == 0)
                {
                    lblErr.Text = "Quá trình khởi tạo ký thất bại!\nChưa chọn văn bản để ký!!";
                    return false;
                }

                string typeSign = TypeLink.WLAN.ToString() + "." + TypeSignOverWeb.SaveFileInDB.ToString() + ".";

                if (SendDataToClientForSign(dtInputSign, dtInputDB, "donvi_CA", "WebTTD", typeSign))
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                lblErr.Text = ex.Message;
                return false;
            }
        }
        #endregion

        #region ULSK
        protected void btnSignULSK_Click(object sender, EventArgs e)
        {
            if (btnSign.Text == "Ký nào")
            {
                if (SignULSK())
                    btnSignULSK.Text = "Refresh";
            }
            else
            {
                btnSignULSK.Text = "Ký nào";
            }
        }

        private bool SignULSK()
        {
            try
            {
                //Tạo bảng đầu vào
                DataTable dtInputDB = CreateInputDB();
                DataTable dtInputSign = CreateInputSign();

                DataRow drSign = dtInputSign.NewRow();
                drSign[0] = FileUpload1.FileBytes;
                drSign[1] = FileUpload1.FileName;
                drSign[2] = -1;
                drSign[3] = 0;
                dtInputSign.Rows.Add(drSign);

                string extension = Path.GetExtension(FileUpload1.FileName);

                DataRow drFileID = dtInputDB.NewRow();
                drFileID[0] = "FileTypeID";
                drFileID[1] = 100;
                drFileID[2] = (int)TypeOutputToDB.CreateFile;
                drFileID[3] = 0;
                dtInputDB.Rows.Add(drFileID);
                DataRow drFileMaDV = dtInputDB.NewRow();
                drFileMaDV[0] = "FileMaDV";
                drFileMaDV[1] = "G25600";
                drFileID[2] = (int)TypeOutputToDB.CreateFile;
                drFileMaDV[3] = 0;
                dtInputDB.Rows.Add(drFileMaDV);
                DataRow drFileDate = dtInputDB.NewRow();
                drFileDate[0] = "FileDate";
                drFileDate[1] = "2016-07-01";
                drFileID[2] = (int)TypeOutputToDB.CreateFile;
                drFileDate[3] = 0;
                dtInputDB.Rows.Add(drFileDate);
                DataRow drDescription = dtInputDB.NewRow();
                drDescription[0] = "Description";
                drDescription[1] = "";
                drFileID[2] = (int)TypeOutputToDB.CreateFile;
                drDescription[3] = 0;
                dtInputDB.Rows.Add(drDescription);
                DataRow drFileName = dtInputDB.NewRow();
                drFileName[0] = "FileName";
                drFileName[1] = "UploadfileCA" + extension;
                drFileID[2] = (int)TypeOutputToDB.CreateFile;
                drFileName[3] = 0;
                dtInputDB.Rows.Add(drFileName);

                string[] arrDangKyCA = { "ConnectString", "StoreName", "NM_ID", "Nam", "Thang", "FileTypeID", "FileID",
                                           "FilePath", "FileName", "Nguoi_Upload" };

                for (int i = 0; i < arrDangKyCA.Length; i++)
                {
                    DataRow dr = dtInputDB.NewRow();
                    dr[0] = arrDangKyCA[i];
                    dr[2] = (int)TypeOutputToDB.Other;
                    dr[3] = 1;
                    if (i == 0)
                        dr[1] = ConfigurationManager.ConnectionStrings["WebTTD"].ConnectionString;
                    else if (i == 1)
                        dr[1] = "MO_SuKienTTD_FileUpload_DonViInsert";
                    else if (i == 2)
                        dr[1] = 93;
                    else if (i == 3)
                        dr[1] = 2016;
                    else if (i == 4)
                        dr[1] = 7;
                    else if (i == 5)
                        dr[1] = 100;
                    else if (i == 6)
                    {
                        dr[1] = "";
                        dr[2] = (int)TypeOutputToDB.FileID;
                    }
                    else if (i == 7)
                    {
                        dr[1] = "";
                        dr[2] = (int)TypeOutputToDB.FilePath;
                    }
                    else if (i == 8)
                    {
                        dr[1] = "";
                        dr[2] = (int)TypeOutputToDB.FileName;
                    }
                    else if (i == 9)
                        dr[1] = "donviCA";
                    dtInputDB.Rows.Add(dr);
                }

                if (dtInputDB.Rows.Count == 0)
                {
                    lblErr.Text = "Quá trình khởi tạo ký thất bại!\nChưa chọn văn bản để ký!!";
                    return false;
                }

                string typeSign = TypeLink.WLAN.ToString() + "." + TypeSignOverWeb.CreateFileServer.ToString() + "." + FileTypes.BangKeDoanhThuThiTruongNgay.ToString();

                byte[] filedata = Common.ConvertTextToPDF(lblContent.Text);
                if (SendDataToClientForSign(dtInputSign, dtInputDB, "donvi_CA", "WebTTD", typeSign))
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                lblErr.Text = ex.Message;
                return false;
            }
        }
        #endregion

        //Hàm ký cho website gọi app tại client
        //dtInput là các trường cần thiết cho cập nhật cơ sở dữ liệu
        private DataTable CreateInputDB()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ColumnName");
            dt.Columns.Add("ColumnValue");
            dt.Columns.Add("ColumnType");
            dt.Columns.Add("Obj");
            return dt;
        }

        private DataTable CreateInputSign()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("FileData", typeof(byte[]));
            dt.Columns.Add("FilePath", typeof(string));
            dt.Columns.Add("FileID", typeof(int));
            dt.Columns.Add("Obj", typeof(int));
            return dt;
        }

        private bool SendDataToClientForSign(DataTable dtInputSign, DataTable dtInputDB, string userName, string program, string typeSign)
        {
            //Kiểm tra số file
            if (dtInputSign.Rows.Count > iMaxFile)
            {
                lblErr.Text = "Quá trình khởi tạo ký thất bại!\nKhông được phép chọn quá 7 văn bản để ký!!";
                return false;
            }

            //Mã hóa thông tin gửi xuống app ký
            string strKey = Guid.NewGuid().ToString();
            string strVersion = ConfigurationManager.AppSettings["VersionAppSign"];
            string primaryString = strKey + "@~" + userName + "@~" + program + "@~" + dtInputSign.Rows.Count + "@~" + typeSign + "@~" + strVersion;
            ES_Encrypt enc = new ES_Encrypt();
            string sHref = enc.EncryptString(primaryString);

            //Cập nhật cơ sở dữ liệu liên kết 
            DAL_SqlConnector.ConnectionString = connectString;
            BUSFile bus = new BUSFile();
            string strErr = "";
            if (!bus.UpdateInfoToSign(dtInputSign, dtInputDB, strKey, userName, program, ref strErr))
            {
                lblErr.Text = "Quá trình khởi tạo ký thất bại!\nLỗi cập nhật cơ sở dữ liệu!\n" + strErr;
                return false;
            }

            //Gán lệnh gọi Sign app cho HtmlAnchor                    
            aSign.HRef = sProtocolName + ":" + sHref;

            //Gọi hàm ký javascript
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "none", "window.onload = function () { performWinAppSign(" + iTimeout.ToString() + "); };", true);
            return true;
        }
    }
}
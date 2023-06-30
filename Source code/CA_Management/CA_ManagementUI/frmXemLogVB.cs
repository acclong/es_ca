using C1.Win.C1FlexGrid;
using ES.CA_ManagementBUS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls.UI;

namespace ES.CA_ManagementUI
{
    public partial class frmXemLogVB : Form
    {
        BUSQuanTri _bus = new BUSQuanTri();
        private int _iSignStatus, _fileID;
        public frmXemLogVB()
        {
            InitializeComponent();
        }

        public int SignStatus
        {
            set { _iSignStatus = value; }
            get { return _iSignStatus; }
        }

        public int FileID
        {
            set { _fileID = value; }
            get { return _fileID; }
        }

        private void frmXemLogVB_Load(object sender, EventArgs e)
        {
            try
            {
                if (SignStatus == 0)
                {
                    LoadDataLogFileStatus();
                    InitLogFileStatus();
                }
                else if (SignStatus == 1)
                {
                    LoadDataLogFileSignature();
                    InitLogFileSignature();
                }
            }
            catch (Exception ex)
            {
                clsShare.Message_Error(ex);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cfgFile_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (cfgFile.Row == cfgFile.RowSel && cfgFile.Col == cfgFile.ColSel && cfgFile.Col == 6)
                {
                    int iCertID = Convert.ToInt32(cfgFile.Rows[cfgFile.Row]["CertID"]);

                    // lấy dữ liệu từ db
                    byte[] rawData = _bus.CA_Certificate_SelectRawDataByID(iCertID);
                    // show thông tin Certificate
                    X509Certificate2 cert = new X509Certificate2(rawData);
                    X509Certificate2UI.DisplayCertificate(cert);
                }
            }
            catch (Exception ex)
            {
                clsShare.Message_Error(ex);
            }
        }

        #region sự kiện telerick
        //private void rgvCertificates_CommandCellClick(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        // lấy giá trị CertAuthID tương ứng
        //        GridCommandCellElement gcce = sender as GridCommandCellElement;
        //        int iCertID = Convert.ToInt32(gcce.RowInfo.Cells["CertID"].Value);

        //        // lấy dữ liệu từ db
        //        DataTable dtRawData = _bus.CA_Certificate_SelectRawDataByID(iCertID);

        //        // show thông tin Certificate
        //        byte[] rawData = (byte[])dtRawData.Rows[0]["RawData"];
        //        X509Certificate2 cert = new X509Certificate2(rawData);
        //        X509Certificate2UI.DisplayCertificate(cert);
        //    }
        //    catch (Exception ex)
        //    {
        //        clsShare.Message_Error(ex);
        //    }
        //}
        #endregion

        private void InitLogFileStatus()
        {
            //Thêm trường STT và ẩn cột ID
            string[] arrName = { "", "STT", "ID_Log", "FileID", "FileNumber", "Status", "StatusName", "Reason", "UserModified", "DateModified" };
            string[] arrCaption = { "", "STT", "ID_Log", "FileID", "Số văn bản", "Status", "Trạng thái", "Lý do", "Người thay đổi", "TG thay đổi" };

            for (int i = 0; i < arrName.Length; i++)
            {
                // tên và header
                cfgFile.Cols[i].Name = arrName[i];
                cfgFile.Cols[i].Caption = arrCaption[i];
                // căn lề
                if (i == 1 || i == 6)
                    cfgFile.Cols[i].TextAlign = TextAlignEnum.CenterCenter;
                else
                    cfgFile.Cols[i].TextAlign = TextAlignEnum.LeftCenter;
                cfgFile.Rows[0].TextAlign = TextAlignEnum.CenterCenter;
                // kích thước cột
                if (i == 0)
                    cfgFile.Cols[i].Width = 25;
                else if (i == 1)
                    cfgFile.Cols[i].Width = 40;
                else if (i == 4 || i == 7)
                    cfgFile.Cols[i].Width = 300;
                else if (i == 6)
                    cfgFile.Cols[i].Width = 120;
                else if (i == 8 || i == 9)
                    cfgFile.Cols[i].Width = 150;
                // ẩn các cột không cần thiết
                if (i == 2 || i == 3 || i == 5)
                    cfgFile.Cols[i].Visible = false;
                // định dạng ngày tháng
                if (i == 9)
                    cfgFile.Cols[i].Format = "HH:mm:ss dd/MM/yyyy";
            }
        }

        private void LoadDataLogFileStatus()
        {
            cfgFile.DataSource = _bus.FL_LogFileStatus_SelectByFileID(FileID);
        }

        private void InitLogFileSignature()
        {
            cfgFile.AutoGenerateColumns = true;

            //Thêm trường STT và ẩn cột ID
            string[] arrName = { "", "STT", "ID_Log", "FileID", "FileNumber", "CertID", "NameCN", "SignTime", "Verify",
                                    "ReceivedTime", "Action", "BackupPath", "UserModified", "DateModified","LanXuLy"};
            string[] arrCaption = {"", "STT", "ID_Log", "FileID", "Số văn bản", "CertID", "Người ký", "Thời gian ký", "Xác thực",
                                       "Thời gian kiểm tra", "Loại", "Đường dẫn backup", "Người cập nhật", "Ngày cập nhật","Lần xử lí" };

            #region For
            for (int i = 0; i < arrName.Length; i++)
            {
                // tên và header
                cfgFile.Cols[i].Caption = arrCaption[i];
                cfgFile.Cols[i].Name = arrName[i];
                cfgFile.Cols[i].TextAlignFixed = TextAlignEnum.CenterCenter;

                // căn lề
                if (i == 1 || i == 8 || i == 15)
                    cfgFile.Cols[i].TextAlign = TextAlignEnum.CenterCenter;
                else
                    cfgFile.Cols[i].TextAlign = TextAlignEnum.LeftCenter;

                // kích thước cột
                if (i == 0)
                    cfgFile.Cols[i].Width = 25;
                else if (i == 1)
                    cfgFile.Cols[i].Width = 40;
                else if (i == 4)
                    cfgFile.Cols[i].Width = 180;
                else if (i == 6 || i == 11)
                    cfgFile.Cols[i].Width = 200;
                else if (i == 7 || i == 12 || i == 13)
                    cfgFile.Cols[i].Width = 120;
                else if (i == 8 || i == 10 || i == 14)
                    cfgFile.Cols[i].Width = 80;
                // định dạng ngày tháng
                if (i == 7 || i == 13)
                    cfgFile.Cols[i].Format = "dd/MM/yyyy HH:mm:ss";
                // font chữ
                if (i == 6)
                    cfgFile.Cols[i].Style.Font = new Font("Times New Roman", 10, FontStyle.Underline);
                // ẩn các cột không cần thiết
                if (i == 2 || i == 3 || i == 5 || i == 9)
                    cfgFile.Cols[i].Visible = false;
                ////Cho phép filter
                //if (i == 0)
                //    rgvLogFile.Columns[i].AllowFiltering = false;
            }
            #endregion

            cfgFile.Cols["LanXuLy"].Move(7);
            cfgFile.Cols["BackupPath"].Move(14);
            cfgFile.Cols["ReceivedTime"].Move(9);
        }

        private void LoadDataLogFileSignature()
        {
            DataTable dt = _bus.FL_LogFileSignature_SelectByFileID(FileID);
            cfgFile.DataSource = dt;
        }
    }
}

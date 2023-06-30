using C1.Win.C1FlexGrid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using esDigitalSignature;
using System.IO;
using System.Security.Cryptography.X509Certificates;

namespace ES.CA_ManagementUI
{
    public partial class frmXemDanhSachChuKy : Form
    {
        string _pathFile;
        DataTable _dtSource  = new DataTable();
        public string PathFile
        {
            set { _pathFile = value; }
            get { return _pathFile; }
        }
        public frmXemDanhSachChuKy()
        {
            InitializeComponent();
        }

        private void frmDanhSachChungThuSo_Load(object sender, EventArgs e)
        {
            try
            {
                LoadData();
                InitcfgVanBan();
            }
            catch (Exception ex)
            {
                clsShare.Message_Error(ex.Message);
            }
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow[] drArray = _dtSource.Select("CheckBox = true");
                if (drArray.Length == 0)
                {
                    clsShare.Message_Error("Bạn phải chọn ít nhất 1 chữ ký để xóa!");
                    return;
                }

                if (!clsShare.Message_QuestionYN("Bạn có chắc chắn muốn xóa chữ ký số trong văn bản?"))
                    return;

                foreach (DataRow dr in drArray)
                {
                    List<ESignature> list = new List<ESignature>();
                    string nameCN = dr[1].ToString();
                    DateTime dt = Convert.ToDateTime(dr[2]);
                    string type = Path.GetExtension(PathFile);

                    using (ESDigitalSignatureManager osm = new ESDigitalSignatureManager(File.ReadAllBytes(PathFile), Path.GetExtension(PathFile)))
                    {
                        list = osm.Signatures;
                        for (int i = 0; i < list.Count; i++)
                        {
                            if (list[i].Signer.GetNameInfo(X509NameType.DnsName, false) == nameCN && list[i].SigningTime == dt)
                                osm.RemoveSignature(list[i]);
                        }
                    }

                    #region Old Code
                    //if (type == ".docx" || type == ".xlsx")
                    //{
                    //    using (OfficeDigitalSignatureManager osm = new OfficeDigitalSignatureManager(PathFile))
                    //    {
                    //        list = osm.Signatures;
                    //        for (int i = 0; i < list.Count; i++)
                    //        {
                    //            if (list[i].Signer.GetNameInfo(X509NameType.DnsName, false) == nameCN && list[i].SigningTime == dt)
                    //                osm.RemoveSignature(list[i]);
                    //        }
                    //    }
                    //}
                    //else if (type == ".pdf")
                    //{
                    //    using (PdfDigitalSignatureManager psm = new PdfDigitalSignatureManager(PathFile))
                    //    {
                    //        list = psm.Signatures;
                    //        for (int i = 0; i < list.Count; i++)
                    //        {
                    //            if (list[i].Signer.GetNameInfo(X509NameType.DnsName, false) == nameCN && list[i].SigningTime == dt)
                    //                psm.RemoveSignature(list[i]);
                    //        }
                    //    }
                    //}
                    //else if (type == ".xml" || type == ".bid")
                    //{
                    //    using (XmlDigitalSignatureManager xsm = new XmlDigitalSignatureManager(PathFile))
                    //    {
                    //        list = xsm.Signatures;
                    //        for (int i = 0; i < list.Count; i++)
                    //        {
                    //            if (list[i].Signer.GetNameInfo(X509NameType.DnsName, false) == nameCN && list[i].SigningTime == dt)
                    //                xsm.RemoveSignature(list[i]);
                    //        }
                    //    }
                    //}
                    #endregion
                }
                LoadData();
                InitcfgVanBan();
            }
            catch (Exception ex)
            {
                clsShare.Message_Error(ex.Message);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cfgVanBan_Click(object sender, EventArgs e)
        {
            try
            {
                if (cfgVanBan.Row > 0 && cfgVanBan.Col == 5)
                {
                    DataRow dr = _dtSource.Rows[cfgVanBan.Row - 1];
                    List<ESignature> list = new List<ESignature>();
                    byte[] rawData = null;
                    string nameCN = dr[1].ToString();
                    DateTime dt = Convert.ToDateTime(dr[2]);
                    string type = Path.GetExtension(PathFile);

                    using (ESDigitalSignatureManager osm = new ESDigitalSignatureManager(File.ReadAllBytes(PathFile), Path.GetExtension(PathFile)))
                    {
                        list = osm.Signatures;
                        for (int i = 0; i < list.Count; i++)
                        {
                            if (list[i].Signer.GetNameInfo(X509NameType.DnsName, false) == nameCN && list[i].SigningTime == dt)
                                rawData = list[i].Signer.RawData;
                        }
                    }

                    #region Old Code
                    //if ( type == ".docx" || type == ".xlsx")
                    //{
                    //    using (OfficeDigitalSignatureManager osm = new OfficeDigitalSignatureManager(PathFile))
                    //    {
                    //        list = osm.Signatures;
                    //        for (int i = 0; i < list.Count; i++)
                    //        {
                    //            if (list[i].Signer.GetNameInfo(X509NameType.DnsName, false) == nameCN && list[i].SigningTime == dt)
                    //                rawData = list[i].Signer.RawData;
                    //        }
                    //    }
                    //}
                    //else if (type == ".pdf")
                    //{
                    //    using (PdfDigitalSignatureManager psm = new PdfDigitalSignatureManager(PathFile))
                    //    {
                    //        list = psm.Signatures;
                    //        for (int i = 0; i < list.Count; i++)
                    //        {
                    //            if (list[i].Signer.GetNameInfo(X509NameType.DnsName, false) == nameCN && list[i].SigningTime == dt)
                    //                rawData = list[i].Signer.RawData;
                    //        }
                    //    }
                    //}
                    //else if (type == ".xml" || type == ".bid")
                    //{
                    //    using (XmlDigitalSignatureManager xsm = new XmlDigitalSignatureManager(PathFile))
                    //    {
                    //        list = xsm.Signatures;
                    //        for (int i = 0; i < list.Count; i++)
                    //        {
                    //            if (list[i].Signer.GetNameInfo(X509NameType.DnsName, false) == nameCN && list[i].SigningTime == dt)
                    //                rawData = list[i].Signer.RawData;
                    //        }
                    //    }
                    //}
                    #endregion

                    X509Certificate2 cert = new X509Certificate2(rawData);
                    X509Certificate2UI.DisplayCertificate(cert);
                }
            }
            catch (Exception ex)
            {
                clsShare.Message_Error("Lỗi trong quá trình lấy dữ liệu:\r\n" + ex.Message);
            }
        }

        private void InitcfgVanBan()
        {
            try
            {
                // cấu hình cho cfg
                cfgVanBan.Cols.Fixed = 0;
                cfgVanBan.AllowEditing = true;
                string[] arrName = new string[] { "CheckBox", "NameCN", "SignTime", "Verify", "Show" };
                string[] arrCaption = new string[] { "", "Người ký", "Thời gian ký", "Xác thực", "Xem chứng thư" };

                for (int i = 0; i < arrName.Length; i++)
                {
                    cfgVanBan.Cols[i].Name = arrName[i];
                    cfgVanBan.Cols[i].Caption = arrCaption[i];
                    if (i == 0)
                    {
                        cfgVanBan.Cols[i].TextAlign = TextAlignEnum.CenterCenter;
                        cfgVanBan.Cols[i].AllowEditing = true;
                    }
                    else
                    {
                        cfgVanBan.Cols[i].TextAlign = TextAlignEnum.LeftCenter;
                        cfgVanBan.Cols[i].AllowEditing = false;
                    }
                    if (i == 0)
                        cfgVanBan.Cols[i].Width = 30;                    
                    if(i == 1)
                        cfgVanBan.Cols[i].Width = 380;
                    else if(i == 2)
                        cfgVanBan.Cols[i].Width = 120;
                    else if (i == 3)
                        cfgVanBan.Cols[i].Width = 60;
                    else if (i == 4)
                        cfgVanBan.Cols[i].Width = 220;
                    if (i == 2)
                        cfgVanBan.Cols[i].Format = "dd/MM/yyyy HH:mm:ss";

                }
                // Style
                cfgVanBan.Cols[4].Style.Font = new Font("Times New Roman", 10, FontStyle.Underline);
                cfgVanBan.Cols[4].Style.TextAlign = TextAlignEnum.CenterCenter;
                // căn giừa hàng đầu
                cfgVanBan.Rows[0].TextAlign = TextAlignEnum.CenterCenter;
            }
            catch (Exception ex)
            {
                clsShare.Message_Error("Lỗi trong quá trình lấy dữ liệu:\r\n" + ex.Message);
            }
        }

        private void LoadData()
        {
            _dtSource.Rows.Clear();

            if (_dtSource.Columns.Count == 0)
            {
                _dtSource.Columns.Add("CheckBox", typeof(bool));
                _dtSource.Columns.Add("NameCN", typeof(string));
                _dtSource.Columns.Add("SignTime", typeof(DateTime));
                _dtSource.Columns.Add("Verify", typeof(string));
                //_dtSource.Columns.Add("SignCreator", typeof(string));
                _dtSource.Columns.Add("Show", typeof(string));
            }

            List<ESignature> list = new List<ESignature>();
            string type = Path.GetExtension(PathFile);



            using (ESDigitalSignatureManager xsm = new ESDigitalSignatureManager(File.ReadAllBytes(PathFile), Path.GetExtension(PathFile)))
            {
                list = xsm.Signatures;
                for (int i = 0; i < list.Count; i++)
                {
                    DataRow dr = _dtSource.NewRow();
                    dr[0] = false;
                    dr[1] = list[i].Signer.GetNameInfo(X509NameType.DnsName, false);
                    dr[2] = list[i].SigningTime;
                    dr[3] = list[i].Verify;
                    dr[4] = "Xem";
                    _dtSource.Rows.Add(dr);
                }
            }

            #region Old Code
            //if (type == ".doc" || type == ".docx" || type == ".xls" || type == ".xlsx")
            //{
            //    using (OfficeDigitalSignatureManager osm = new OfficeDigitalSignatureManager(PathFile))
            //    {
            //        list = osm.Signatures;
            //        for (int i = 0; i < list.Count; i++)
            //        {
            //            DataRow dr = _dtSource.NewRow();
            //            dr[0] = false;
            //            dr[1] = list[i].Signer.GetNameInfo(X509NameType.DnsName, false);
            //            dr[2] = list[i].SigningTime;
            //            dr[3] = list[i].Verify;
            //            dr[5] = "Xem";
            //            _dtSource.Rows.Add(dr);
            //        }
            //    }
            //}
            //else if (type == ".pdf")
            //{
            //    using (PdfDigitalSignatureManager psm = new PdfDigitalSignatureManager(PathFile))
            //    {
            //        list = psm.Signatures;
            //        for (int i = 0; i < list.Count; i++)
            //        {
            //            DataRow dr = _dtSource.NewRow();
            //            dr[0] = false;
            //            dr[1] = list[i].Signer.GetNameInfo(X509NameType.DnsName, false);
            //            dr[2] = list[i].SigningTime;
            //            dr[3] = list[i].Verify;
            //            //dr[4] = list[i].SignatureCreator;
            //            dr[5] = "Xem";
            //            _dtSource.Rows.Add(dr);
            //        }
            //    }
            //}
            //else if (type == ".xml" || type == ".bid")
            //{
            //    using (XmlDigitalSignatureManager xsm = new XmlDigitalSignatureManager(PathFile))
            //    {
            //        list = xsm.Signatures;
            //        for (int i = 0; i < list.Count; i++)
            //        {
            //            DataRow dr = _dtSource.NewRow();
            //            dr[0] = false;
            //            dr[1] = list[i].Signer.GetNameInfo(X509NameType.DnsName, false);
            //            dr[2] = list[i].SigningTime;
            //            dr[3] = list[i].Verify;
            //            dr[4] = list[i].SignatureCreator;
            //            dr[5] = "Xem";
            //            _dtSource.Rows.Add(dr);
            //        }
            //    }
            //}
            #endregion

            cfgVanBan.DataSource = _dtSource;
        }
    }
}

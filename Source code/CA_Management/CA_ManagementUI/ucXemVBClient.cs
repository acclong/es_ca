using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;
using esDigitalSignature;
using System.IO;
using System.Security.Cryptography.X509Certificates;

namespace ES.CA_ManagementUI
{
    public partial class ucXemVBClient : UserControl
    {
        List<string> _fullPathFile = new List<string>();
        DataTable _dtSource = new DataTable();

        public ucXemVBClient()
        {
            InitializeComponent();
        }

        private void ucXemVBClient_Load(object sender, EventArgs e)
        {
            try
            {
                LoadData();
                InitcfgVanBan();

                // Thêm sự kiện KeyDown
                cfgVanBan.KeyDown += new KeyEventHandler(clsShare.C1FlexGrid_KeyDown);
            }
            catch (Exception ex)
            {
                clsShare.Message_Error(ex.Message);
            }
        }

        private void btnAddFile_Click(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog ofd = new OpenFileDialog())
                {
                    ofd.Filter = "(*.docx;*.xlsx)|*.docx;*.xlsx|" +
                                    "(*.pdf)|*.pdf|" +
                                    "(*.xml;*.bid;)|*.xml;*.bid";
                    ofd.Multiselect = true;

                    if (ofd.ShowDialog() == DialogResult.OK)
                    {
                        foreach (String file in ofd.FileNames)
                        {
                            _fullPathFile.Add(file);
                        }
                        LoadData();
                        InitcfgVanBan();
                    }
                }
            }
            catch (Exception ex)
            {
                clsShare.Message_Error(ex.Message);
            }
        }

        private void btnDelFile_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow[] drArray = _dtSource.Select("CheckBox = true");
                if(drArray.Length == 0)
                {
                    clsShare.Message_Error("Bạn phải chọn ít nhất 1 văn bản để bỏ khỏi danh sách văn bản!");
                    return;
                }
                foreach (DataRow dr in drArray)
                {
                    _fullPathFile.Remove(Convert.ToString(dr[2]));
                }
                LoadData();
                InitcfgVanBan();
            }
            catch (Exception ex)
            {
                clsShare.Message_Error("Lỗi trong quá trình lấy dữ liệu:\r\n" + ex.Message);
            }
        }

        private void btnCompare_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow[] drArray = _dtSource.Select("CheckBox = true");
                if(drArray.Length != 2)
                {
                    clsShare.Message_Error("Bạn phải chọn 2 văn bản để so sánh chuỗi hash!");
                    return;
                }
                if (drArray.Length == 2)
                {
                    int icompare = String.Compare(drArray[0]["Hash"].ToString(), drArray[1]["Hash"].ToString());
                    string sCompare = "Văn bản 1: " + drArray[0]["Hash"].ToString() +
                                        "\nVăn bản 2: " + drArray[1]["Hash"].ToString();
                    if (icompare == 0)
                        clsShare.Message_Info(sCompare+"\n\nHai văn bản có nội dung giống nhau!");
                    else
                        clsShare.Message_Info(sCompare + "\n\nHai văn bản có nội dung khác nhau!");
                }
            }
            catch (Exception ex)
            {
                clsShare.Message_Error("Lỗi trong quá trình lấy dữ liệu:\r\n" + ex.Message);
            }
        }

        private void btnSign_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow[] drArray = _dtSource.Select("CheckBox = true");
                if(drArray.Length == 0)
                {
                    clsShare.Message_Error("Bạn phải chọn ít nhất 1 văn bản để ký!");
                    return;
                }

                X509Certificate2 cert = Common.SelectCertificateFromStore("Danh sách chứng thư số", "Chọn chứng thư số của bạn", this.Handle);
                if(cert == null)
                {
                    clsShare.Message_Warning("Quá trình ký bị hủy bởi người dùng!");
                    return;
                }

                Cursor.Current = Cursors.WaitCursor;

                foreach (DataRow dr in drArray)
                {
                    string type = Path.GetExtension(dr[2].ToString());

                    using (ESDigitalSignatureManager osm = new ESDigitalSignatureManager(File.ReadAllBytes(dr[2].ToString()), Path.GetExtension(dr[2].ToString())))
                    {
                        osm.Sign(cert);
                    }

                    #region Old Code
                    //if (type == ".doc" || type == ".docx" || type == ".xls" || type == ".xlsx")
                    //{
                    //    using (OfficeDigitalSignatureManager osm = new OfficeDigitalSignatureManager(dr[2].ToString()))
                    //    {
                    //        osm.Sign(cert);
                    //    }
                    //}
                    //else if (type == ".pdf")
                    //{
                    //    using (PdfDigitalSignatureManager psm = new PdfDigitalSignatureManager(dr[2].ToString()))
                    //    {
                    //        PdfSignatureField field = new PdfSignatureField();
                    //        psm.Sign(cert, field);
                    //    }
                    //}
                    //else if (type == ".xml" || type == ".bid")
                    //{
                    //    using (XmlDigitalSignatureManager xsm = new XmlDigitalSignatureManager(dr[2].ToString()))
                    //    {
                    //        xsm.Sign(cert);
                    //    }
                    //}
                    #endregion
                }

                Cursor.Current = Cursors.Default;

                LoadData();
                InitcfgVanBan();
            }
            catch (Exception ex)
            {
                clsShare.Message_Error(ex.Message);
            }
        }

        private void cfgVanBan_Click(object sender, EventArgs e)
        {
            try
            {
                if (cfgVanBan.Row > 0 && cfgVanBan.Col == 5)
                {
                    DataRow dr = _dtSource.Rows[cfgVanBan.Row - 1];
                    frmXemDanhSachChuKy frm = new frmXemDanhSachChuKy();
                    frm.PathFile = dr[2].ToString();
                    frm.ShowDialog();
                    LoadData();
                    InitcfgVanBan();
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
                string[] arrName = new string[] { "CheckBox", "FileName", "FilePath", "Hash", "NumbSign", "List" };
                string[] arrCaption = new string[] { "", "Tên văn bản", "Đường dẫn", "Chuỗi Hash","Số lượng chữ ký" , "Danh Sách chữ ký" };

                for (int i = 0; i < arrName.Length; i++)
                {
                    cfgVanBan.Cols[i].Name = arrName[i];
                    cfgVanBan.Cols[i].Caption = arrCaption[i];
                    // căn lề
                    if (i == 0)
                    {
                        cfgVanBan.Cols[i].TextAlign = TextAlignEnum.CenterCenter;
                        cfgVanBan.Cols[i].AllowEditing = true;
                    }
                    else
                    {
                        cfgVanBan.Cols[i].TextAlign = TextAlignEnum.LeftCenter;
                        cfgVanBan.Cols[i].AllowEditing = false;
                        //
                        cfgVanBan.Cols[i].AllowFiltering = AllowFiltering.ByValue;
                    }
                }
                // kích thước cột
                cfgVanBan.Cols[0].Width = 30;
                cfgVanBan.Cols[1].Width = 300;
                cfgVanBan.Cols[2].Width = 330;
                cfgVanBan.Cols[3].Width = 390;
                cfgVanBan.Cols[4].Width = 90;
                cfgVanBan.Cols[5].Width = 100;
                // font
                cfgVanBan.Cols[5].Style.Font = new Font("Times New Roman", 10, FontStyle.Underline);
                // căn giữa
                cfgVanBan.Cols[4].Style.TextAlign = TextAlignEnum.RightCenter;
                cfgVanBan.Cols[5].Style.TextAlign = TextAlignEnum.CenterCenter;
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
                _dtSource.Columns.Add("FileName", typeof(string));
                _dtSource.Columns.Add("FilePath", typeof(string));
                _dtSource.Columns.Add("Hash", typeof(string));
                _dtSource.Columns.Add("NumbSign", typeof(string));
                _dtSource.Columns.Add("List", typeof(string));
                cfgVanBan.DataSource = _dtSource;
            }
            else
            {
                if (_fullPathFile.Count != 0)
                    for (int i = 0; i < _fullPathFile.Count; i++)
                    {
                        DataRow dr = _dtSource.NewRow();
                        dr[0] = false;
                        dr[1] = Path.GetFileName(_fullPathFile[i]);
                        dr[2] = Path.GetFullPath(_fullPathFile[i]);
                        string hash = "";
                        int numbSign = 0;
                        GetHash(_fullPathFile[i], ref hash, ref numbSign);
                        dr[3] = hash;
                        dr[4] = numbSign;
                        dr[5] = "Xem chữ ký";
                        _dtSource.Rows.Add(dr);
                    }
                cfgVanBan.DataSource = _dtSource;
            }
        }

        private void GetHash(string fullPath, ref string hash, ref int numbSign)
        {
            string type = Path.GetExtension(fullPath);

            ESDigitalSignatureManager osm = new ESDigitalSignatureManager(File.ReadAllBytes(fullPath), Path.GetExtension(fullPath));
            numbSign = osm.Signatures.Count;
            hash = BitConverter.ToString(osm.GetHashValue());
            osm.Dispose();

            #region Old Code
            //if (type == ".doc" || type == ".docx" || type == ".xls" || type == ".xlsx")
            //{
            //    OfficeDigitalSignatureManager osm = new OfficeDigitalSignatureManager(fullPath);
            //    numbSign = osm.Signatures.Count;
            //    hash = BitConverter.ToString(osm.GetHashValue());
            //    osm.Dispose();
            //}
            //else if (type == ".pdf")
            //{
            //    PdfDigitalSignatureManager psm = new PdfDigitalSignatureManager(fullPath);
            //    numbSign = psm.Signatures.Count;
            //    hash = BitConverter.ToString(psm.GetHashValue());
            //    psm.Dispose();
            //}
            //else if (type == ".xml" || type == ".bid")
            //{
            //    XmlDigitalSignatureManager xsm = new XmlDigitalSignatureManager(fullPath);
            //    numbSign = xsm.Signatures.Count;
            //    hash = BitConverter.ToString(xsm.GetHashValue());
            //    xsm.Dispose();
            //}
            #endregion
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Security.Cryptography.X509Certificates;
using X509Certificate2 = System.Security.Cryptography.X509Certificates.X509Certificate2;
using System.IO;
using esDigitalSignature;
using WebService = A0ServiceDemo.A0SignatureService;
using esDigitalSignature.Library;
using System.Data.SqlClient;

namespace A0ServiceDemo
{
    public partial class SignBase64 : Form
    {
        int iFileType = 1;  //1: Office; 2: PDF; 3: Xml

        public SignBase64()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            txtSeri.Text = "‎5401DBF72B8A7CCBE6FA70BDD7293D10";
        }

        private void txtSeri_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtSeri.Text == "")
                    return;
                char[] aPath = txtSeri.Text.Trim().ToUpper().ToCharArray();
                string seri = "";
                for (int i = 0; i < aPath.Length; i++)
                {
                    if (aPath[i] == ' ')
                        continue;
                    else
                        seri += aPath[i];
                }
                txtSeri.Text = seri;
            }
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            OpenFileDialog OFD = new OpenFileDialog
            {
                Filter = @"Office document (*.xlsx;*.xls;*.doc;*.docx)|*.xlsx;*.xls;*.doc;*.docx|PDF file (*.pdf)|*.pdf|XML file (*.xml;*.bid)|*.xml;*.bid",
                Multiselect = false,
                Title = @"Chọn tập tin"
            };
            if (OFD.ShowDialog() == DialogResult.Cancel)
                return;
            else
                txtPath.Text = OFD.FileName;
            OFD.Dispose();
        }

        private void btnSign_Click(object sender, EventArgs e)
        {
            if (txtPath.Text == "")
            {
                MessageBox.Show("Chưa có file để ký!");
                return;
            }

            try
            {
                //Tham số
                //File
                string base64 = Common.ConvertFileToBase64(txtPath.Text);
                string fileType = Path.GetExtension(txtPath.Text).ToLower();

                //Gọi hàm ký server
                WebService.CAService sc = new WebService.CAService();
                //base64 = sc.SignFileByBase64(fileType, base64, "ninhtq", 1, "123456");

                //Lưu file
                Common.ConvertBase64ToFile(base64, txtPath.Text);

                MessageBox.Show("Ký file thành công!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ký file thất bại \n\n" + ex.Message, "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnVerify_Click(object sender, EventArgs e)
        {
            if (txtPath.Text == "")
            {
                MessageBox.Show("Chưa có file để ký!");
                return;
            }

            //Lấy certificate
            X509Certificate2 cert = new X509Certificate2();
            if (!txtSeri.Text.Contains(':'))
                cert = Common.GetCertificateBySerial(txtSeri.Text);
            else
                cert = Common.GetCertificateByFile(txtSeri.Text);

            try
            {
                //string sTypeFile = Path.GetExtension(txtPath.Text).ToLower();
                //if (sTypeFile == ".pdf")
                //{
                //    // Verify file PDF
                //    using (PdfDigitalSignatureManager dsm = new PdfDigitalSignatureManager(txtPath.Text))
                //    {
                //        VerifyResult s = dsm.VerifySignatures();
                //        List<ESignature> lst = dsm.Signatures;

                //        MessageBox.Show("Result: " + s.ToString(), "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    }
                //}
                //else if (sTypeFile == ".doc" || sTypeFile == ".docx" || sTypeFile == ".xls" || sTypeFile == ".xlsx")
                //{
                //    // Verify file Office
                //    using (OfficeDigitalSignatureManager dsm = new OfficeDigitalSignatureManager(txtPath.Text))
                //    {
                //        VerifyResult s = dsm.VerifySignatures();
                //        List<ESignature> lst = dsm.Signatures;

                //        //Kiểm tra theo 1 cert
                //        s = dsm.VerifySignatures(cert);

                //        //Xác thực certificate
                //        X509ChainStatus certificateStatus = new X509ChainStatus();
                //        bool isValid = lst[0].ValidateCertificate(out certificateStatus);

                //        MessageBox.Show("Result: " + s.ToString(), "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    }
                //}
                //else if (sTypeFile == ".xml" || sTypeFile == ".bid")
                //{
                //    // Verify file PDF
                //    using (XmlDigitalSignatureManager dsm = new XmlDigitalSignatureManager(txtPath.Text))
                //    {
                //        VerifyResult s = dsm.VerifySignatures();
                //        List<ESignature> lst = dsm.Signatures;

                //        MessageBox.Show("Result: " + s.ToString(), "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    }
                //}
                //else
                //    MessageBox.Show("Không hỗ trợ định dạng file!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Verify thất bại \n\n" + ex.Message, "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //FillHassh();
            //Validate();
            SignFileDetails();

            ////Lấy certificate
            //X509Certificate2 cert = new X509Certificate2();
            //if (!txtSeri.Text.Contains(':'))
            //    cert = Common.GetCertificateBySerial(txtSeri.Text);
            //else
            //    cert = Common.GetCertificateByFile(txtSeri.Text);

            //CertificateStatus stt;
            //bool kq = sc.ValidateCertificateInCA_Now(cert.RawData, out stt);

            //X509ChainStatus status = new X509ChainStatus();
            //status.Status = (X509ChainStatusFlags)stt.Status;
            //status.StatusInformation = stt.StatusInformation;

            //CertificateTtdStatus ttd;
            //kq = sc.ValidateCertificateInTTD_Now("BiddingClient", cert.RawData, out ttd);
            //MessageBox.Show(ttd.StatusInformation);
        }

        private void FillHassh()
        {
            //Gọi hàm ký server
            WebService.CAService sc = new WebService.CAService();

            //DataTable table = new DataTable();
            //using (SqlConnection connection = new SqlConnection("Data Source=(local);Initial Catalog=AdventureWorks2014;Integrated Security=SSPI"))
            //using (SqlCommand cmd = new SqlCommand("SELECT BusinessEntityID AS ID, FirstName, MiddleName, LastName FROM Person.Person", connection))
            //using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
            //{
            //    adapter.Fill(table);
            //}

            //WebService.FileInfoCA fileCA = sc.GetFileInfo(88);
            //using (ESDigitalSignatureManager dsm = new ESDigitalSignatureManager(fileCA.FilePath))
            //{
            //    byte[] hash = dsm.GetHashValue();
            //    sc.SetFileStatus_WithHash(88, (int)FileStatus.LuuFile, hash, "Set hash để test", "TTTT_A0", "sonnc1");
            //}
        }

        private void Validate()
        {
            //Lấy certificate
            X509Certificate2 cert = new X509Certificate2();
            if (!txtSeri.Text.Contains(':'))
                cert = Common.GetCertificateBySerial(txtSeri.Text);
            else
                cert = Common.GetCertificateByFile(txtSeri.Text);

            WebService.CAService sc = new WebService.CAService();

            WebService.CertificateStatus_CA stt;
            bool kq = sc.ValidateCertificateInCA_Now(cert.RawData, out stt);

            X509ChainStatus status = new X509ChainStatus();
            status.Status = (X509ChainStatusFlags)stt.Status;
            status.StatusInformation = stt.StatusInformation;

            //CertificateTtdStatus ttd;
            //kq = sc.ValidateCertificateInTTD_Now("BiddingClient", cert.RawData, out ttd);
            //MessageBox.Show(ttd.StatusInformation);
        }

        private void SignFileDetails()
        {
            //WebService.CAService sc = new WebService.CAService();

            //DataTable dt = new DataTable("dt");
            //A0ServiceDemo.A0SignatureService.CAExitCode dsa = sc.SignFilesByID_ReturnDetail5("1;", "TTTT_A0", "a0_kylap", "123456", ref dt);
        }
    }
}

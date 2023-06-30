using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Security.Cryptography.X509Certificates;
using System.IO;
using esDigitalSignature;
using System.Security.Cryptography;
using System.Security;
using Excel = Microsoft.Office.Interop.Excel;
using System.Globalization;
//using esDigitalSignature.eToken;
using esDigitalSignature.Library;
using System.Data.SqlClient;
using Org.BouncyCastle.Ocsp;
using BC = Org.BouncyCastle;

namespace DemoSign
{
    public partial class Sign : Form
    {
        int iFileType = 1;  //1: Office; 2: PDF; 3: Xml
        string[] filePaths;
        X509Certificate2 cert;

        public Sign()
        {
            InitializeComponent();
        }

        private void Sign_Load(object sender, EventArgs e)
        {
            txtSeri.Text = "‎‎5401DBF72B8A7CCBE6FA70BDD7293D10";
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

        private void rbOffice_CheckedChanged(object sender, EventArgs e)
        {
            if (rbOffice.Checked)
                iFileType = 1;
        }

        private void rbPdf_CheckedChanged(object sender, EventArgs e)
        {
            if (rbPdf.Checked)
                iFileType = 2;
        }

        private void rbXml_CheckedChanged(object sender, EventArgs e)
        {
            if (rbXml.Checked)
                iFileType = 3;
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            if (iFileType == 1)
            {
                //office
                OpenFileDialog OFD = new OpenFileDialog
                {
                    Filter = @"Office document (*.xlsx;*.xls;*.doc;*.docx)|*.xlsx;*.xls;*.doc;*.docx",
                    Multiselect = true,
                    Title = @"Chọn tập tin"
                };
                if (OFD.ShowDialog() == DialogResult.Cancel)
                    return;
                else
                {
                    txtPath.Text = OFD.FileName;
                    filePaths = OFD.FileNames;
                }
                OFD.Dispose();
            }
            else if (iFileType == 2)
            {
                //Ký pdf
                OpenFileDialog OFD = new OpenFileDialog
                {
                    Filter = @"PDF file (*.pdf)|*.pdf",
                    Multiselect = true,
                    Title = @"Chọn tập tin"
                };
                if (OFD.ShowDialog() == DialogResult.Cancel)
                    return;
                else
                {
                    txtPath.Text = OFD.FileName;
                    filePaths = OFD.FileNames;
                }
                OFD.Dispose();
            }
            else if (iFileType == 3)
            {
                //Ký pdf
                OpenFileDialog OFD = new OpenFileDialog
                {
                    Filter = @"XML file (*.xml;*.bid)|*.xml;*.bid",
                    Multiselect = true,
                    Title = @"Chọn tập tin"
                };
                if (OFD.ShowDialog() == DialogResult.Cancel)
                    return;
                else
                {
                    txtPath.Text = OFD.FileName;
                    filePaths = OFD.FileNames;
                }
                OFD.Dispose();
            }
        }

        private void btnFileCRT_Click(object sender, EventArgs e)
        {
            OpenFileDialog OFD = new OpenFileDialog
            {
                Filter = @"Tập tin (*.pem;*.crt;*.cer)|*.pem;*.crt;*.cer",
                Multiselect = false,
                Title = @"Chọn tập tin"
            };
            if (OFD.ShowDialog() == DialogResult.Cancel)
                return;
            else
                txtSeri.Text = OFD.FileName;
            OFD.Dispose();
        }

        private void btnSign_Click(object sender, EventArgs e)
        {
            if (txtSeri.Text == "")
            {
                MessageBox.Show("Chưa có certificate!");
                return;
            }

            //Lấy certificate
            if (!txtSeri.Text.Contains(':'))
                cert = Common.GetCertificateBySerial(txtSeri.Text);
            else
                cert = Common.GetCertificateByFile(txtSeri.Text);

            //MessageBox.Show(cert.Verify().ToString());
            //X509ChainStatus result;
            //bool valid = Common.ValidateCertificate(cert, new DateTime(2015, 5, 22), out result);
            //MessageBox.Show(result.StatusInformation);

            if (txtPath.Text == "")
            {
                MessageBox.Show("Chưa có file để ký!");
                return;
            }

            try
            {
                if (iFileType == 4)
                {
                    string text = txtPath.Text;
                    byte[] mess = Encoding.Unicode.GetBytes(text);
                    byte[] sign = Common.Sign(mess, cert);
                    txtPath.Text = txtPath.Text + "\r\n" + Convert.ToBase64String(sign);
                    MessageBox.Show(txtPath.Text);
                    return;
                }

                textBox1.Text += Environment.NewLine + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + ": Start load CRYPTOKI ...";

                /********************************************/
                Common.TSAServerUri = "http://localhost:3921/Default.aspx";// "http://localhost:8080";// "http://tsa.vnpt-ca.vn/";

                // Ký bằng USB Token
                //Common.ConnectUsbToken(cert, "es@123456");
                //Duyệt và ký từng file
                foreach (string strPath in filePaths)
                {
                    using (ESDigitalSignatureManager dsm = new ESDigitalSignatureManager(File.ReadAllBytes(strPath), Path.GetExtension(strPath)))
                    {
                        dsm.Sign(cert);
                        File.WriteAllBytes(strPath, dsm.ToArray());
                    }
                }

                //// Ký bằng HSM
                ////Khởi tạo giao tiếp HSM
                //HSMServiceProvider.Initialize(Common.CRYPTOKI);

                //textBox1.Text += Environment.NewLine + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + ": CRYPTOKI loaded.";

                ////Mở session và đăng nhập
                //HSMServiceProvider provider = new HSMServiceProvider(txtSlotSerial.Text, HSMLoginRole.User, txtSlotPIN.Text);

                //textBox1.Text += Environment.NewLine + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + ": Opened session and logged in.";

                ////Khởi tạo private key dùng để ký
                //provider.LoadPrivateKeyByLABEL(txtKeyLabel.Text);

                //textBox1.Text += Environment.NewLine + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + ": Private key loaded.";

                //foreach (string strPath in filePaths)
                //{
                //    using (ESDigitalSignatureManager dsm = new ESDigitalSignatureManager(File.ReadAllBytes(strPath), Path.GetExtension(strPath)))
                //    {
                //        dsm.Sign(cert, provider);
                //        File.WriteAllBytes(strPath, dsm.ToArray());
                //    }
                //}

                //textBox1.Text += Environment.NewLine + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + ": Sign success.";

                ////Đóng session
                //provider.Dispose();

                ////Đóng giao tiếp HSM
                //HSMServiceProvider.Finalize();
                /********************************************/

                textBox1.Text += Environment.NewLine + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + ": Finished.";

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
                if (iFileType == 4)
                {
                    string[] text = txtPath.Text.Split(new string[] { "\r\n" }, StringSplitOptions.None);
                    byte[] mess = Encoding.Unicode.GetBytes(text[0]);
                    byte[] sign = Convert.FromBase64String(text[1]);
                    MessageBox.Show(Common.Verify(mess, sign, cert).ToString());
                    return;
                }

                using (ESDigitalSignatureManager dsm = new ESDigitalSignatureManager(File.ReadAllBytes(txtPath.Text), Path.GetExtension(txtPath.Text)))
                {
                    VerifyResult s = dsm.VerifySignatures();
                    List<ESignature> lst = dsm.Signatures;

                    ////Kiểm tra theo 1 cert
                    //s = dsm.VerifySignatures(cert);

                    //Xác thực certificate
                    X509ChainStatus certificateStatus;
                    bool isValid = lst[0].ValidateCertificate(out certificateStatus);

                    MessageBox.Show("Result: " + s.ToString(), "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Verify thất bại \n\n" + ex.Message, "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void rbString_CheckedChanged(object sender, EventArgs e)
        {
            if (rbString.Checked)
                iFileType = 4;
        }

        private void btnSelCert_Click(object sender, EventArgs e)
        {
            try
            {
                cert = Common.SelectCertificateFromStore("Thư viện ký ES", "Chọn một chứng thư số", this.Handle);
                if (cert != null)
                    txtSeri.Text = cert.SerialNumber;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnGetHash_Click(object sender, EventArgs e)
        {
            try
            {
                using (ESDigitalSignatureManager dsm = new ESDigitalSignatureManager(File.ReadAllBytes(txtPath.Text), Path.GetExtension(txtPath.Text)))
                {
                    byte[] hash = dsm.GetHashValue();
                    textBox1.Text += Environment.NewLine + BitConverter.ToString(hash);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                TestCheckRevocation();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void KyChuoi()
        {
            //Lấy certificate
            if (!txtSeri.Text.Contains(':'))
                cert = Common.GetCertificateBySerial(txtSeri.Text);
            else
                cert = Common.GetCertificateByFile(txtSeri.Text);

            //Ký chuỗi
            string login = "ppc_toantk;pas_sword";
            byte[] mess = Encoding.ASCII.GetBytes(login);
            textBox1.Text = Common.ConvertBytesToHex(Common.Sign(mess, cert));
            System.Threading.Thread.Sleep(5000);

            ////Encrypt
            ////decode string
            //byte[] input = Encoding.ASCII.GetBytes(txtPath.Text);
            ////mã hóa chuỗi data
            //byte[] en = Common.Encrypt(input, cert);
            ////convert
            //textBox1.Text = Common.ConvertBytesToHex(en);

            //Decrypt
            //convert
            byte[] input = Common.ConvertHexToByte(txtPath.Text);
            //giải mã chuỗi data
            byte[] en = Common.Decrypt(input, cert);
            //encode
            textBox1.Text = Encoding.ASCII.GetString(en);
        }

        private void XoaChuKy()
        {
            using (ESDigitalSignatureManager dsm = new ESDigitalSignatureManager(File.ReadAllBytes(txtPath.Text), Path.GetExtension(txtPath.Text)))
            {
                dsm.RemoveSignature(dsm.Signatures[0]);
                File.WriteAllBytes(txtPath.Text, dsm.ToArray());
            }
        }

        private void TestService()
        {
            ////in function
            ////Khởi tạo kết nối
            //PKCS11.Initialize("cryptoki.dll");
            //PKCS11.Slot[] slots = PKCS11.GetSlotList(true);
            //PKCS11.Slot slot = slots[1];

            ////in for loop
            ////Mở session
            //PKCS11.Session session = PKCS11.OpenSession(slot,
            //  PKCS11.CKF_RW_SESSION | PKCS11.CKF_SERIAL_SESSION);
            //session.Login(PKCS11.CKU_USER, "123456");
            ////Tìm key
            //PKCS11.Object PIkey = session.FindObjects(new PKCS11.Attribute[]  {
            //        new PKCS11.Attribute(PKCS11.CKA_CLASS, PKCS11.CKO_PRIVATE_KEY),
            //        new PKCS11.Attribute(PKCS11.CKA_LABEL, "NinhtqPI")
            //      })[0];
            ////Ký
            //PKCS11.Mechanism signMech = new PKCS11.Mechanism(PKCS11.CKM_RSA_PKCS, null);

            ////in Thread
            //byte[] ArrSigned = session.Sign(signMech, PIkey, Encoding.ASCII.GetBytes("RandomTextForTest"));


            ////Đóng session
            //session.Logout();
            //session.Close();

            ////Kết thúc
            //PKCS11.Finalize();

            //in function
            HSMServiceProvider.Initialize(Common.CRYPTOKI);

            //in for loop
            HSMServiceProvider provider = new HSMServiceProvider("", HSMLoginRole.User, "123456");
        }

        private void TestLoginExxception()
        {
            try
            {
                //in function
                HSMServiceProvider.Initialize(Common.CRYPTOKI);

                //in for loop
                HSMServiceProvider provider = new HSMServiceProvider("57257", HSMLoginRole.User, "1234567");
            }
            catch
            {

            }
        }

        private void TestGenKey()
        {
            HSMServiceProvider hsm = new HSMServiceProvider(Common.CRYPTOKI);
            hsm.Login("84324", HSMLoginRole.User, "123456");
            hsm.GenerateKeyPairAndRequest(HSMKeyPairType.RSA, "MAI DINH LOI", "LoimdPUB", "LoimdPRV", "LoimdREQ");
        }

        private void TestEnumClass()
        {
            int i = (int)HSMObjectClass.PUBLIC_KEY;
            i = (int)HSMObjectClass.PRIVATE_KEY;
            i = (int)HSMObjectClass.CERTIFICATE;
            i = (int)HSMObjectClass.CERTIFICATE_REQUEST;
        }

        private void TestLoginEx()
        {
            try
            {
                Common.CRYPTOKI = Path.Combine(Application.StartupPath, "cryptoki.dll");
                HSMServiceProvider.Initialize(Common.CRYPTOKI);
                //Thử đăng nhập
                using (HSMServiceProvider hsm = new HSMServiceProvider("57257", HSMLoginRole.User, txtPath.Text))
                {
                    hsm.LoadPrivateKeyByID(new byte[] { 12, 12 });
                }
                HSMServiceProvider.Finalize();
            }
            catch (Exception ex)
            {
                CAExitCode exit = GetErrorCodeFromString(ex.Message);
                HSMServiceProvider.Finalize();
                MessageBox.Show(exit.ToString());
            }
        }

        private CAExitCode GetErrorCodeFromString(string exMessage)
        {
            switch (exMessage)
            {
                case "HSM_PinIncorrect":
                    return CAExitCode.HSM_PinIncorrect;
                case "HSM_LoginFailed":
                    return CAExitCode.HSM_LoginFailed;
                case "HSM_DuplicateKey":
                    return CAExitCode.HSM_DuplicateKey;
                case "HSM_KeyTypeNotSupported":
                    return CAExitCode.HSM_KeyTypeNotSupported;
                case "HSM_KeyNotFound":
                    return CAExitCode.HSM_KeyNotFound;
                case "HSM_PinLocked":
                    return CAExitCode.HSM_PinLocked;
                case "HSM_PinExpired":
                    return CAExitCode.HSM_PinExpired;
                default:
                    return CAExitCode.Error;
            }
        }

        private void TestBase64ToFile()
        {
            string base64 = Common.ConvertFileToBase64(@"C:\Users\TranKim\Desktop\CA\Test\File\Test_LargeFile_PDF_10MB.pdf");
            File.WriteAllText(@"C:\Users\TranKim\Desktop\CA\Test\File\Test_LargeFile_PDF_10MB.txt", base64);
        }

        private void GetListSlot()
        {
            //string text = "";

            //PKCS11.Initialize(Common.CRYPTOKI);
            //PKCS11.Slot[] slots = PKCS11.GetSlotList(true);
            //foreach (PKCS11.Slot slot in slots)
            //{
            //    text += "Slot ID: " + slot.Id.ToString() + "; Label: " + slot.GetTokenInfo().label + "\r\n";
            //}
            //PKCS11.Finalize();

            //MessageBox.Show(text);
        }

        private void TestCRL()
        {
            if (txtSeri.Text == "")
            {
                MessageBox.Show("Chưa có certificate!");
                return;
            }

            //Lấy certificate
            if (!txtSeri.Text.Contains(':'))
                cert = Common.GetCertificateBySerial(txtSeri.Text);
            else
                cert = Common.GetCertificateByFile(txtSeri.Text);

            //Xác thực certificate
            X509ChainStatus certificateStatus;
            bool isValid = Common.ValidateCertificate(cert, DateTime.Now, out certificateStatus);

            MessageBox.Show("Result: " + certificateStatus.StatusInformation, "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void SaveHash()
        {
            SqlConnection sqlcon = new SqlConnection("Data Source=192.168.0.48;Initial Catalog=CA;User ID=sa;Password=sa123;");
            sqlcon.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = textBox1.Text;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = sqlcon;

            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);

            int i = 0;
            textBox1.Text = "STT\tFileID\tKetqua";
            foreach (DataRow dr in dt.Rows)
            {
                int fileID = Convert.ToInt32(dr["FileID"]);
                string filePath = dr["FilePath"].ToString();
                string fileExtension = Path.GetExtension(filePath);

                try
                {
                    // ghi status + ghi log
                    using (ESDigitalSignatureManager dsm = new ESDigitalSignatureManager(File.ReadAllBytes(filePath), fileExtension))
                    {
                        //UPDATE dbo.FL_File SET FileHash = 0x57FBB5D5917F557FABB7ACA91BA9C31AC7342E45 WHERE FileID = 52
                        SqlCommand cmd1 = new SqlCommand();
                        cmd1.CommandText = "UPDATE dbo.FL_File SET FileHash = 0x" + Common.ConvertBytesToHex(dsm.GetHashValue()) + " WHERE FileID = " + fileID.ToString();
                        cmd1.CommandType = CommandType.Text;
                        cmd1.Connection = sqlcon;
                        cmd1.ExecuteNonQuery();
                    }

                    textBox1.Text += Environment.NewLine + i.ToString() + "\t" + fileID.ToString() + "\tSuccess";
                }
                catch (Exception ex)
                {
                    textBox1.Text += Environment.NewLine + i.ToString() + "\t" + fileID.ToString() + "\t" + ex.Message;
                }

                i++;
            }

            sqlcon.Close();
        }

        private void SavePDF()
        {
            string txt =
                "Cộng hòa Xã hội Chủ nghĩa Việt Nam" +
                "\nĐộc lập - Tự do - Hạnh phúc" +
                "\n---------------------------" +
                "\n\nPHIẾU ĐĂNG KÝ";
            File.WriteAllBytes(txtPath.Text, Common.ConvertTextToPDF(txt));
        }

        private void EnDecryptHSM()
        {
            //Lấy certificate
            if (!txtSeri.Text.Contains(':'))
                cert = Common.GetCertificateBySerial(txtSeri.Text);
            else
                cert = Common.GetCertificateByFile(txtSeri.Text);

            ////Encrypt chuỗi
            //string connectionString = @"192.168.0.2\SQL2008R2E";
            //byte[] mess = Encoding.ASCII.GetBytes(connectionString);
            ////mã hóa chuỗi data
            //byte[] en = Common.Encrypt(mess, cert);
            ////convert
            //textBox1.Text = Common.ConvertBytesToHex(en);

            ////Decrypt
            //using (HSMServiceProvider hsm = new HSMServiceProvider(Common.CRYPTOKI))
            //{
            //    hsm.Login(txtSlotSerial.Text, HSMLoginRole.User, txtSlotPIN.Text);
            //    hsm.LoadPrivateKeyByLABEL(txtKeyLabel.Text);

            //    //convert
            //    byte[] input = Common.ConvertHexToByte(textBox1.Text);
            //    //giải mã chuỗi data
            //    byte[] de = Common.Decrypt(input, hsm);
            //    //encode
            //    txtPath.Text = Encoding.ASCII.GetString(de);
            //}

            //Decrypt
            //convert
            byte[] input = Common.ConvertHexToByte(textBox1.Text);
            //giải mã chuỗi data
            byte[] de = Common.Decrypt(input, cert);
            //encode
            txtPath.Text = Encoding.ASCII.GetString(de);
        }

        private void TestValidateCert()
        {
            //Lấy certificate
            if (!txtSeri.Text.Contains(':'))
                cert = Common.GetCertificateBySerial(txtSeri.Text);
            else
                cert = Common.GetCertificateByFile(txtSeri.Text);

            //Xác thực chứng thư với nhà cung cấp CA
            X509ChainStatus certificateStatus;
            Common.ValidateCertificate(cert, DateTime.Now, out certificateStatus);

            textBox1.Text = certificateStatus.Status.ToString() + ": " + certificateStatus.StatusInformation;
        }

        private void TestGetCertInfo()
        {
            //Lấy certificate
            if (!txtSeri.Text.Contains(':'))
                cert = Common.GetCertificateBySerial(txtSeri.Text);
            else
                cert = Common.GetCertificateByFile(txtSeri.Text);

            string text = "";
            text += "Tên người dùng: " + cert.GetNameInfo(X509NameType.DnsName, false);
            text += Environment.NewLine + "CMND\\MST: " + Common.GetCertIDNumber(cert.Subject);
            text += Environment.NewLine + "Email: " + cert.GetNameInfo(X509NameType.EmailName, false);

            textBox1.Text = text;
        }

        private void TestCheckRevocation()
        {
            //if (txtSeri.Text == "")
            //{
            //    MessageBox.Show("Chưa có certificate!");
            //    return;
            //}

            ////Lấy certificate
            //if (!txtSeri.Text.Contains(':'))
            //    cert = Common.GetCertificateBySerial(txtSeri.Text);
            //else
            //    cert = Common.GetCertificateByFile(txtSeri.Text);

            //Common.GetEncoded(cert);

            SqlConnection sqlConnection1 = new SqlConnection("Server=SVR03;Database=CA;User Id=sa;Password=sa123;");
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;

            cmd.CommandText = "SELECT NAmeCN, RawData FROM CA_Certificate";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = sqlConnection1;

            sqlConnection1.Open();

            reader = cmd.ExecuteReader();
            // Data is accessible through the DataReader object here.
            try
            {
                while (reader.Read())
                {
                    X509Certificate2 cert = new X509Certificate2((byte[])reader["RawData"]);
                    X509ChainStatus stt = new X509ChainStatus();
                    Common.ValidateCertificate(cert, DateTime.Now, out stt);
                    textBox1.Text += reader["NAmeCN"] + ": " + stt.StatusInformation + Environment.NewLine;
                }
            }
            finally
            {
                // Always call Close when done reading.
                reader.Close();
            }

            sqlConnection1.Close();
        }

        private void btnEncrypt_Click(object sender, EventArgs e)
        {
            //Lấy certificate
            if (!txtSeri.Text.Contains(':'))
                cert = Common.GetCertificateBySerial(txtSeri.Text);
            else
                cert = Common.GetCertificateByFile(txtSeri.Text);

            //Encrypt chuỗi
            //txtPath.Text = @"192.168.0.2\SQL2008R2E";
            byte[] mess = Encoding.ASCII.GetBytes(txtPath.Text);
            //mã hóa chuỗi data
            byte[] en = Common.Encrypt(mess, cert);
            //convert
            textBox1.Text = Common.ConvertBytesToHex(en);
        }

        private void btnDecrypt_Click(object sender, EventArgs e)
        {
            //Lấy certificate
            if (!txtSeri.Text.Contains(':'))
                cert = Common.GetCertificateBySerial(txtSeri.Text);
            else
                cert = Common.GetCertificateByFile(txtSeri.Text);

            //Decrypt
            using (HSMServiceProvider hsm = new HSMServiceProvider(Common.CRYPTOKI))
            {
                hsm.Login(txtSlotSerial.Text, HSMLoginRole.User, txtSlotPIN.Text);
                hsm.LoadPrivateKeyByLABEL(txtKeyLabel.Text);

                //convert
                byte[] input = Common.ConvertHexToByte(textBox1.Text);
                //giải mã chuỗi data
                byte[] de = Common.Decrypt(input, hsm);
                //encode
                txtPath.Text = Encoding.ASCII.GetString(de);
            }

            ////Decrypt
            ////convert
            //byte[] input = Common.ConvertHexToByte(textBox1.Text);
            ////giải mã chuỗi data
            //byte[] de = Common.Decrypt(input, cert);
            ////encode
            //txtPath.Text = Encoding.ASCII.GetString(de);
        }

    }
}

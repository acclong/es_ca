using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using esDigitalSignature;
using System.Security.Cryptography.X509Certificates;
using System.Security.Permissions;
using System.Security;
using System.IO;

namespace DemoWeb
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                string filePath = Server.MapPath("~" + txtPath.Text);
                Common.CRYPTOKI = Server.MapPath("~" + TextBox2.Text);
                string certPath = Server.MapPath(@"~\Files\Ninhtq_Cert.crt");

                string sTypeFile = Path.GetExtension(filePath).ToLower();
                X509Certificate2 cert = getCertByFile(certPath);

                if (sTypeFile == ".pdf")
                {
                    //Ký pdf
                    string sNameFile = Path.GetFileNameWithoutExtension(filePath);
                    string sPathDestination = Path.GetDirectoryName(filePath) + "\\" + sNameFile + "_signed.pdf";

                    //// Ký bằng USB Token
                    //PdfSignatureField field = new PdfSignatureField(PdfSignatureField.PositionOffsetEnum.UpperLeft, new Point(10, 10), 200, 50);
                    //PdfDigitalSignatureManager pdsm = new PdfDigitalSignatureManager();
                    //pdsm.SignPdfFile(txtPath.Text, sPathDestination, cert, field);

                    //Khởi tạo giao tiếp HSM và đăng nhập
                    HSMServiceProvider provider = new HSMServiceProvider(Common.CRYPTOKI);
                    provider.Login(2, HSMLoginRole.User, "123456");
                    //Khởi tạo private key dùng để ký
                    provider.LoadPrivateKeyByLABEL("NinhtqPI");
                    //Kí file office
                    using (PdfDigitalSignatureManager dsm = new PdfDigitalSignatureManager(File.ReadAllBytes(filePath)))
                    {
                        dsm.Sign(cert, provider);
                    }
                    //Đóng giao tiếp HSM
                    provider.Logout();
                    provider.Dispose();
                }
                else if (sTypeFile == ".doc" || sTypeFile == ".docx" || sTypeFile == ".xls" || sTypeFile == ".xlsx")
                {
                    //// Ký bằng USB Token
                    //OfficeDigitalSignatureManager odsm = new OfficeDigitalSignatureManager();
                    //odsm.SignOfficeFile(txtPath.Text, txtPath.Text, cert);

                    //Khởi tạo giao tiếp HSM và đăng nhập
                    HSMServiceProvider provider = new HSMServiceProvider(Common.CRYPTOKI);
                    provider.Login(2, HSMLoginRole.User, "123456");
                    //Khởi tạo private key dùng để ký
                    provider.LoadPrivateKeyByLABEL("NinhtqPI");
                    //Kí file office
                    using (OfficeDigitalSignatureManager dsm = new OfficeDigitalSignatureManager(File.ReadAllBytes(filePath)))
                    {
                        dsm.Sign(cert, provider);
                    }
                    //Đóng giao tiếp HSM
                    provider.Logout();
                    provider.Dispose();
                }
                else if (sTypeFile == ".xml" || sTypeFile == ".bid")
                {
                    //// Ký bằng USB Token
                    //XmlDigitalSignatureManager xdsm = new XmlDigitalSignatureManager();
                    //xdsm.SignXmlFile(txtPath.Text, txtPath.Text, cert);

                    //Khởi tạo giao tiếp HSM và đăng nhập
                    HSMServiceProvider provider = new HSMServiceProvider(Common.CRYPTOKI);
                    provider.Login(2, HSMLoginRole.User, "123456");
                    //Khởi tạo private key dùng để ký
                    provider.LoadPrivateKeyByLABEL("NinhtqPI");
                    //Kí file office
                    using (XmlDigitalSignatureManager dsm = new XmlDigitalSignatureManager(File.ReadAllBytes(filePath)))
                    {
                        dsm.Sign(cert, provider);
                    }
                    //Đóng giao tiếp HSM
                    provider.Logout();
                    provider.Dispose();
                }
                else
                    Label1.Text = "Không hỗ trợ định dạng file!";

                Label1.Text = "OK";
            }
            catch (Exception ex)
            {
                Label1.Text = ex.ToString();
            }
        }

        private X509Certificate2 getCertBySNo(string SerialNo)
        {
            // Access Personal (MY) certificate store of current user
            X509Store my = new X509Store();
            my.Open(OpenFlags.ReadOnly);

            // Find the certificate we'll use to sign            
            foreach (X509Certificate2 cert in my.Certificates)
            {
                if (cert.SerialNumber.Equals(SerialNo, StringComparison.InvariantCultureIgnoreCase))
                {
                    return cert;
                }

            }
            return null;
        }

        private X509Certificate2 getCertByFile(string crtPath)
        {
            X509Certificate2 cert = new X509Certificate2(crtPath);
            return cert;
        }
    }
}

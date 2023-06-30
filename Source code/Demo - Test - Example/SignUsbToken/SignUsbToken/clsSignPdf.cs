using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using iTextSharp.text.pdf;
using System.IO;
using iTextSharp.text.pdf.security;
using System.Security.Cryptography.X509Certificates;

namespace SignUsbToken
{
    class clsSignPdf
    {
        private byte[] ImageToByte(System.Drawing.Image img)
        {
            System.Drawing.ImageConverter converter = new System.Drawing.ImageConverter();
            return (byte[])converter.ConvertTo(img, typeof(byte[]));
        }

        //ký file
        public bool signPdfFile(string sourceDocument, string destinationPath, X509Certificate2 cert, string reason, string location, string contact)
        {
            try
            {
                Org.BouncyCastle.X509.X509CertificateParser cp = new Org.BouncyCastle.X509.X509CertificateParser();
                Org.BouncyCastle.X509.X509Certificate[] chain = new Org.BouncyCastle.X509.X509Certificate[] { cp.ReadCertificate(cert.RawData) };

                // reader and stamper
                PdfReader reader = new PdfReader(sourceDocument);
                using (FileStream fout = new FileStream(destinationPath, FileMode.Create, FileAccess.ReadWrite))
                {
                    using (PdfStamper stamper = PdfStamper.CreateSignature(reader, fout, '\0'))
                    {
                        // appearance
                        PdfSignatureAppearance appearance = stamper.SignatureAppearance;

                        //System.Drawing.Image img = (System.Drawing.Image)CA_Core.Properties.Resources.ResourceManager.GetObject(ImageName);
                        //appearance.Image = iTextSharp.text.Image.GetInstance(ImageToByte(img));
                        //appearance.SignatureRenderingMode = PdfSignatureAppearance.RenderingMode.GRAPHIC_AND_DESCRIPTION;
                        //appearance.SignatureGraphic = iTextSharp.text.Image.GetInstance(ImageToByte(img));
                        appearance.Reason = reason;
                        appearance.Contact = contact;
                        appearance.Location = location;
                        appearance.SetVisibleSignature(new iTextSharp.text.Rectangle(20, 800, 170, 850), 1, "VCGM");
                        // digital signature
                        IExternalSignature es = new X509Certificate2Signature(cert, "SHA-1");
                        MakeSignature.SignDetached(appearance, es, chain, null, null, null, 0, CryptoStandard.CMS);
                        stamper.Close();
                    }
                }

                return true;
            }
            catch
            {
                //xóa file nếu đã tạo
                if (File.Exists(destinationPath))
                    File.Delete(destinationPath);
                return false;
            }
        }

        // Xác thực chữ ký
        public bool verifyPdfSignature(string pdfFile, X509Certificate2 cert, ref string err)
        {
            try
            {
                Org.BouncyCastle.X509.X509CertificateParser cp = new Org.BouncyCastle.X509.X509CertificateParser();
                Org.BouncyCastle.X509.X509Certificate pdfCert = cp.ReadCertificate(cert.RawData);
                Org.BouncyCastle.X509.X509Certificate[] chain = new Org.BouncyCastle.X509.X509Certificate[] { pdfCert };

                PdfReader reader = new PdfReader(pdfFile);
                AcroFields af = reader.AcroFields;
                var names = af.GetSignatureNames();

                if (names.Count == 0)
                {
                    err = "No Signature present in pdf file.";
                    return false;
                }

                foreach (string name in names)
                {
                    if (!af.SignatureCoversWholeDocument(name))
                    {
                        err = string.Format("The signature: {0} does not covers the whole document.", name);
                        return false;
                    }

                    PdfPKCS7 pk = af.VerifySignature(name);
                    var cal = pk.SignDate;
                    var pkc = pk.Certificates;

                    if (!pk.Verify())
                    {
                        err = "The signature could not be verified.";
                        return false;
                    }

                    //var fails = CertificateVerification.VerifyCertificates(pkc, chain, null, cal);
                    //if (fails != null)
                    //{
                    //    err = "The file is not signed using the specified key-pair.";
                    //    return false;
                    //}

                    //MinhĐN: kiểm tra trong danh sách các chữ ký, có chữ ký nào là của cert tương ứng ko
                    for (int i = 0; i < pkc.Length; i++)
                    {
                        Org.BouncyCastle.X509.X509Certificate pdfSignCert = pkc[i];
                        if (pdfSignCert.SerialNumber == pdfCert.SerialNumber) return true;
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                err = ex.Message;
                return false;
            }
        }

        public void viewAllCert(string pdfFile)
        {
            PdfReader reader = new PdfReader(pdfFile);
            AcroFields af = reader.AcroFields;
            var names = af.GetSignatureNames();

            if (names.Count == 0)
            {
                System.Windows.Forms.MessageBox.Show("No Signature present in pdf file.");
                return;
            }

            foreach (string name in names)
            {
                PdfPKCS7 pk = af.VerifySignature(name);
                var cal = pk.SignDate;
                var pkc = pk.Certificates;

                for (int i = 0; i < pkc.Length; i++)
                {
                    X509Certificate2 certificate = new X509Certificate2();
                    certificate.Import(pkc[i].GetEncoded());
                    //Cert_Lib.showCert(certificate);
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography.X509Certificates;
//using esDigitalSignature.iTextSharp.text.pdf;
//using esDigitalSignature.iTextSharp.text.pdf.security;

namespace DemoSign
{
    class clsSignPdf
    {
        //private byte[] ImageToByte(System.Drawing.Image img)
        //{
        //    System.Drawing.ImageConverter converter = new System.Drawing.ImageConverter();
        //    return (byte[])converter.ConvertTo(img, typeof(byte[]));
        //}

        ////ký file
        //public bool SignPdfFile(string sourceDocument, string destinationPath, X509Certificate2 cert, string reason, string location, string contact)
        //{
        //    FileStream signedFile = new FileStream(destinationPath, FileMode.Create, FileAccess.ReadWrite);

        //    try
        //    {
        //        Org.BouncyCastle.X509.X509CertificateParser cp = new Org.BouncyCastle.X509.X509CertificateParser();
        //        Org.BouncyCastle.X509.X509Certificate[] chain = new Org.BouncyCastle.X509.X509Certificate[] { cp.ReadCertificate(cert.RawData) };
        //        //TOANTK: fix cứng HashAlgorithm = "SHA-1"
        //        IExternalSignature externalSignature = new X509Certificate2Signature(cert, "SHA-1");

        //        // reader and stamper
        //        PdfReader pdfReader = new PdfReader(sourceDocument);
        //        PdfStamper pdfStamper = PdfStamper.CreateSignature(pdfReader, signedFile, '\0', null, true);

        //        // appearance
        //        PdfSignatureAppearance signatureAppearance = pdfStamper.SignatureAppearance;
        //        signatureAppearance.Reason = reason;
        //        signatureAppearance.Contact = contact;
        //        signatureAppearance.Location = location;
        //        //signatureAppearance.SetVisibleSignature(new iTextSharp.text.Rectangle(220, 700, 370, 750), 1, null);
        //        signatureAppearance.SetVisibleSignature(new esDigitalSignature.iTextSharp.text.Rectangle(220, 700, 370, 750), 1, null);

        //        //digital signature
        //        MakeSignature.SignDetached(signatureAppearance, externalSignature, chain, null, null, null, 0, CryptoStandard.CMS);

        //        pdfStamper.Close();

        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        //xóa file nếu đã tạo
        //        signedFile.Close();
        //        if (File.Exists(destinationPath))
        //            File.Delete(destinationPath);
        //        throw ex;
        //    }
        //}

        //// Xác thực chữ ký
        //public bool VerifyPdfSignature(string pdfFile, X509Certificate2 cert, ref string err)
        //{
        //    try
        //    {
        //        Org.BouncyCastle.X509.X509CertificateParser cp = new Org.BouncyCastle.X509.X509CertificateParser();
        //        Org.BouncyCastle.X509.X509Certificate pdfCert = cp.ReadCertificate(cert.RawData);
        //        Org.BouncyCastle.X509.X509Certificate[] chain = new Org.BouncyCastle.X509.X509Certificate[] { pdfCert };

        //        PdfReader reader = new PdfReader(pdfFile);
        //        AcroFields af = reader.AcroFields;
        //        var names = af.GetSignatureNames();

        //        if (names.Count == 0)
        //        {
        //            err = "No Signature present in pdf file.";
        //            return false;
        //        }

        //        foreach (string name in names)
        //        {
        //            if (!af.SignatureCoversWholeDocument(name))
        //            {
        //                err = string.Format("The signature: {0} does not covers the whole document.", name);
        //                return false;
        //            }

        //            PdfPKCS7 pk = af.VerifySignature(name);
        //            var cal = pk.SignDate;
        //            var pkc = pk.Certificates;

        //            if (!pk.Verify())
        //            {
        //                err = "The signature could not be verified.";
        //                return false;
        //            }

        //            //var fails = CertificateVerification.VerifyCertificates(pkc, chain, null, cal);
        //            //if (fails != null)
        //            //{
        //            //    err = "The file is not signed using the specified key-pair.";
        //            //    return false;
        //            //}

        //            //MinhĐN: kiểm tra trong danh sách các chữ ký, có chữ ký nào là của cert tương ứng ko
        //            for (int i = 0; i < pkc.Length; i++)
        //            {
        //                Org.BouncyCastle.X509.X509Certificate pdfSignCert = pkc[i];
        //                if (pdfSignCert.SerialNumber == pdfCert.SerialNumber) return true;
        //            }
        //        }

        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        err = ex.Message;
        //        return false;
        //    }
        //}

        ////Xác thực tất cả chữ kí
        //public bool VerifyAllSignatures(string pdfFile, ref string err)
        //{
        //    try
        //    {
        //        //Org.BouncyCastle.X509.X509CertificateParser cp = new Org.BouncyCastle.X509.X509CertificateParser();
        //        //Org.BouncyCastle.X509.X509Certificate pdfCert = cp.ReadCertificate(cert.RawData);
        //        //Org.BouncyCastle.X509.X509Certificate[] chain = new Org.BouncyCastle.X509.X509Certificate[] { pdfCert };

        //        PdfReader reader = new PdfReader(pdfFile);
        //        AcroFields af = reader.AcroFields;
        //        var names = af.GetSignatureNames();

        //        if (names.Count == 0)
        //        {
        //            err = "No Signature present in pdf file.";
        //            return false;
        //        }

        //        foreach (string name in names)
        //        {
        //            //if (!af.SignatureCoversWholeDocument(name))
        //            //{
        //            //    err = string.Format("The signature: {0} does not covers the whole document.", name);
        //            //    return false;
        //            //}

        //            PdfPKCS7 pk = af.VerifySignature(name);
        //            var cal = pk.SignDate;
        //            var pkc = pk.Certificates;

        //            if (!pk.Verify())
        //            {
        //                err = "The signature could not be verified.";
        //                return false;
        //            }

        //            //var fails = CertificateVerification.VerifyCertificates(pkc, chain, null, cal);
        //            //if (fails != null)
        //            //{
        //            //    err = "The file is not signed using the specified key-pair.";
        //            //    return false;
        //            //}
        //        }

        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        err = ex.Message;
        //        return false;
        //    }
        //}

        //public void ViewAllCert(string pdfFile)
        //{
        //    PdfReader reader = new PdfReader(pdfFile);
        //    AcroFields af = reader.AcroFields;
        //    var names = af.GetSignatureNames();

        //    if (names.Count == 0)
        //    {
        //        System.Windows.Forms.MessageBox.Show("No Signature present in pdf file.");
        //        return;
        //    }

        //    foreach (string name in names)
        //    {
        //        PdfPKCS7 pk = af.VerifySignature(name);
        //        var cal = pk.SignDate;
        //        var pkc = pk.Certificates;

        //        for (int i = 0; i < pkc.Length; i++)
        //        {
        //            X509Certificate2 certificate = new X509Certificate2();
        //            certificate.Import(pkc[i].GetEncoded());
        //            //Cert_Lib.showCert(certificate);
        //        }
        //    }
        //}
    }
}

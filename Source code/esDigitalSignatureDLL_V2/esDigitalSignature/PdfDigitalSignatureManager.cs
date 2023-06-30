using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using esDigitalSignature.iTextSharp.text.pdf;
using esDigitalSignature.iTextSharp.text.pdf.security;
using esDigitalSignature.iTextSharp.text;

namespace esDigitalSignature
{
    /// <summary>
    /// Lớp quản lý ký và chữ ký số trên file PDF. Cần dispose khi không sử dụng để giải phóng file.
    /// </summary>
    public class PdfDigitalSignatureManager : IDigitalSignatureManagerBase
    {
        #region Public members - Created by Toantk on 9/5/2015
        /// <summary>
        /// Gói file
        /// </summary>
        private PdfReader Reader
        {
            get { return _reader; }
        }

        /// <summary>
        /// Các chữ ký trên file
        /// </summary>
        public List<ESignature> Signatures
        {
            get
            {
                // ensure signatures are loaded from origin
                EnsureSignatures();

                // Return
                return _signatures;
            }
        }

        /// <summary>
        /// File đã được ký hay chưa?
        /// </summary>
        public bool IsSigned
        {
            get
            {
                EnsureSignatures();
                return (_signatures.Count > 0);
            }
        }

        /// <summary>
        /// Khởi tạo với dữ liệu file - đọc gói file và load chữ ký trên file
        /// </summary>
        /// <param name="fileData"></param>
        public PdfDigitalSignatureManager(byte[] fileData)
        {
            _byteStream = new MemoryStream(fileData);

            _reader = new PdfReader(_byteStream);
            EnsureSignatures();
        }

        /// <summary>
        /// Sign PDF File (word, excel) bằng USB Token với trường hiển thị mặc định
        /// </summary>
        /// <param name="certificate">Certificate ký</param>
        /// <returns></returns>
        public void Sign(X509Certificate2 certificate)
        {
            //Tạo field mặc định để không bị trùng
            //Toantk 24/5/2016: sửa lại kích thước Signature field
            Sign(certificate, 0, 0 + this.Signatures.Count * 20, 590, 20);
        }

        public void Sign(X509Certificate2 certificate, int totalSign = 1, int position = 1)
        {
            totalSign = totalSign < 1 || totalSign > 3 ? 1 : totalSign;
            if (totalSign == 1)
            {
                Sign(certificate, 0, 0 + this.Signatures.Count * 20, 590, 20);
                return;
            }

            position = position < 1 || position > 3 ? 1 : position;

            var count = this.Signatures.Where(z => z.posision == position).Count();
            int x = 0, width = 290, y = count;
            
            switch (totalSign)
            {
                case 2:

                    width = 290;

                    if (position == 2)
                    {
                        x = 295;
                    }

                    break;
                case 3:
                    width = 195;

                    if (position == 2)
                    {
                        x = 200;
                    }
                    if (position == 3)
                    {
                        x = 400;
                    }
                    break;
            }

            Sign(certificate, x, 0 + y * 20, width, 20);
        }

        /// <summary>
        /// Sign PDF File (word, excel) bằng USB Token
        /// </summary>
        /// <param name="certificate">Certificate ký</param>
        /// <param name="llx">Trường hiển thị chữ ký: góc dưới trái (x)</param>
        /// <param name="lly">Trường hiển thị chữ ký: góc dưới trái (y)</param>
        /// <param name="width">Trường hiển thị chữ ký: độ rộng</param>
        /// <param name="height">Trường hiển thị chữ ký: độ cao</param>
        /// <returns></returns>
        public void Sign(X509Certificate2 certificate, int llx, int lly, int width, int height)
        {
            if (certificate == null) throw new ArgumentNullException("certificate");

            //Tạo file tạm
            _byteStream = new MemoryStream();
            // reader and stamper
            // @param append if <CODE>true</CODE> the signature and all the other content will be added as a
            // new revision thus not invalidating existing signatures
            PdfStamper pdfStamper = PdfStamper.CreateSignature(_reader, _byteStream, '\0', null, true);

            //Certificate
            Org.BouncyCastle.X509.X509CertificateParser cp = new Org.BouncyCastle.X509.X509CertificateParser();
            Org.BouncyCastle.X509.X509Certificate[] chain = new Org.BouncyCastle.X509.X509Certificate[] { cp.ReadCertificate(certificate.RawData) };
            //TOANTK: fix cứng HashAlgorithm = "SHA-1"
            IExternalSignature externalSignature = new X509Certificate2Signature(certificate, Common.HashAlgorithm);

            //Toantk 17/8/2016: thêm timestamp
            ITSAClient tsaClient = new TSAClientBouncyCastle(Common.TSAServerUri, null, null, 4096, Common.HashAlgorithm);

            // appearance
            PdfSignatureAppearance signatureAppearance = pdfStamper.SignatureAppearance;
            signatureAppearance.SetVisibleSignature(new Rectangle(llx, lly, llx + width, lly + height), 1, null);

            //digital signature
            MakeSignature.SignDetached(signatureAppearance, externalSignature, chain, null, null, tsaClient, 0, CryptoStandard.CMS);

            //Save to reader
            _reader.Close();
            _reader = new PdfReader(_byteStream.ToArray());

            //Load lại chữ ký
            _signatures = null;
            EnsureSignatures();
        }

        /// <summary>
        /// Sign PDF File (word, excel) bằng HSM với trường hiển thị mặc định
        /// </summary>
        /// <param name="certificate">Certificate ký</param>
        /// <param name="providerHSM">Giao tiếp HSM đã đăng nhập session và load private key. Nếu PrivateKey = null sẽ lấy từ HSM qua Certificate tương ứng</param>
        /// <returns></returns>
        public void Sign(X509Certificate2 certificate, HSMServiceProvider providerHSM)
        {
            //Tạo field mặc định để không bị trùng
            //Toantk 8/8/2016: sửa lại kích thước Signature field như USB
            Sign(certificate, 0, 0 + this.Signatures.Count * 20, 590, 20, providerHSM);
        }

        public void Sign(X509Certificate2 certificate, HSMServiceProvider providerHSM, int totalSign = 1, int position = 1)
        {
            totalSign = totalSign < 1 || totalSign > 3 ? 1 : totalSign;
            if (totalSign == 1)
            {
                Sign(certificate, 0, 0 + this.Signatures.Count * 20, 590, 20, providerHSM);
                return;
            }

            position = position < 1 || position > 3 ? 1 : position;

            var count = this.Signatures.Where(z => z.posision == position).Count();
            int x = 0, width = 290, y = count;

            switch (totalSign)
            {
                case 2:

                    width = 290;

                    if (position == 2)
                    {
                        x = 295;
                    }

                    break;
                case 3:
                    width = 195;

                    if (position == 2)
                    {
                        x = 200;
                    }
                    if (position == 3)
                    {
                        x = 400;
                    }
                    break;
            }

            Sign(certificate, x, 0 + y * 20, width, 20, providerHSM);
        }

        /// <summary>
        /// Sign PDF File (word, excel) bằng HSM
        /// </summary>
        /// <param name="certificate">Certificate ký</param>
        /// <param name="llx">Trường hiển thị chữ ký: góc dưới trái (x)</param>
        /// <param name="lly">Trường hiển thị chữ ký: góc dưới trái (y)</param>
        /// <param name="width">Trường hiển thị chữ ký: độ rộng</param>
        /// <param name="height">Trường hiển thị chữ ký: độ cao</param>
        /// <param name="providerHSM">Giao tiếp HSM đã đăng nhập session và load private key. Nếu PrivateKey = null sẽ lấy từ HSM qua Certificate tương ứng</param>
        /// <returns></returns>
        public void Sign(X509Certificate2 certificate, int llx, int lly, int width, int height, HSMServiceProvider providerHSM)
        {
            if (certificate == null) throw new ArgumentNullException("certificate");

            //Tạo file tạm
            _byteStream = new MemoryStream();
            // reader and stamper
            // @param append if <CODE>true</CODE> the signature and all the other content will be added as a
            // new revision thus not invalidating existing signatures
            PdfStamper pdfStamper = PdfStamper.CreateSignature(_reader, _byteStream, '\0', null, true);

            //Certificate
            Org.BouncyCastle.X509.X509CertificateParser cp = new Org.BouncyCastle.X509.X509CertificateParser();
            Org.BouncyCastle.X509.X509Certificate[] chain = new Org.BouncyCastle.X509.X509Certificate[] { cp.ReadCertificate(certificate.RawData) };
            //TOANTK: fix cứng HashAlgorithm = "SHA-1"
            IExternalSignature externalSignature = new X509Certificate2Signature(certificate, Common.HashAlgorithm, providerHSM);

            //Toantk 17/8/2016: thêm timestamp
            ITSAClient tsaClient = new TSAClientBouncyCastle(Common.TSAServerUri, null, null, 4096, Common.HashAlgorithm);

            // appearance
            PdfSignatureAppearance signatureAppearance = pdfStamper.SignatureAppearance;
            ////TOANTK: thời điểm ký. Mặc định = DateTime.Now
            //signatureAppearance.SignDate = DateTime.Now;
            signatureAppearance.SetVisibleSignature(new Rectangle(llx, lly, llx + width, lly + height), 1, null);

            //digital signature
            MakeSignature.SignDetached(signatureAppearance, externalSignature, chain, null, null, tsaClient, 0, CryptoStandard.CMS, providerHSM);

            //Save to reader
            _reader.Close();
            _reader = new PdfReader(_byteStream.ToArray());

            //Load lại chữ ký
            _signatures = null;
            EnsureSignatures();
        }

        /// <summary>
        /// Xác thực tất cả các chữ ký - chạy xác thực trên mỗi chữ ký
        /// </summary>
        /// <returns></returns>
        public VerifyResult VerifySignatures()
        {
            if (_reader == null) throw new ArgumentNullException("_reader");

            AcroFields af = _reader.AcroFields;
            var names = af.GetSignatureNames();

            if (names.Count == 0)
                return VerifyResult.NotSigned;

            foreach (string name in names)
            {
                PdfPKCS7 pkcs = af.VerifySignature(name);

                if (pkcs.SigningCertificate == null)
                    return VerifyResult.CertificateRequired;

                if (!pkcs.Verify())
                    return VerifyResult.InvalidSignature;
            }

            return VerifyResult.Success;
        }

        /// <summary>
        /// Xác thực file có được ký bởi certificate
        /// </summary>
        /// <param name="certificate"></param>
        /// <returns></returns>
        public VerifyResult VerifySignatures(X509Certificate2 certificate)
        {
            VerifyResult result = VerifyResult.NotSigned;

            if (_reader == null) throw new ArgumentNullException("_reader");

            AcroFields af = _reader.AcroFields;
            var names = af.GetSignatureNames();

            foreach (string name in names)
            {
                PdfPKCS7 pkcs = af.VerifySignature(name);

                if (pkcs.SigningCertificate == null)
                    result = VerifyResult.CertificateRequired;
                else
                {

                    if (pkcs.SigningCertificate.GetEncoded().SequenceEqual(certificate.RawData) && pkcs.Verify())
                        result = VerifyResult.Success;
                    else
                        result = VerifyResult.InvalidSignature;
                }

                if (result == VerifyResult.Success)
                    return result;
            }

            return result;
        }

        /// <summary>
        /// Lấy chuỗi Hash value của file
        /// </summary>
        /// <returns></returns>
        public byte[] GetHashValue()
        {
            Org.BouncyCastle.Crypto.IDigest messageDigest = Org.BouncyCastle.Security.DigestUtilities.GetDigest(Common.HashAlgorithm);
            int pageCount = _reader.NumberOfPages;
            for (int i = 1; i <= pageCount; i++)
            {
                byte[] buf = _reader.GetPageContent(i);
                messageDigest.BlockUpdate(buf, 0, buf.Length);
            }

            byte[] hash = new byte[messageDigest.GetDigestSize()];
            messageDigest.DoFinal(hash, 0);

            return hash;
        }

        /// <summary>
        /// Xóa một chữ ký trên file
        /// </summary>
        /// <param name="signatureES"></param>
        public void RemoveSignature(ESignature signatureES)
        {
            if (signatureES == null)
                throw new ArgumentNullException("signatureES");

            // empty?
            if (!IsSigned)      // calls EnsureSignatures for us
                return;

            // find the signature
            int index = GetSignatureIndex(signatureES);
            if (index < 0)
                return;

            //Tạo file tạm
            _byteStream = new MemoryStream();
            PdfStamper tempStamper = new PdfStamper(_reader, _byteStream);

            try
            {
                // Xóa trên file
                if (_reader == null) throw new ArgumentNullException("_reader");

                AcroFields af = _reader.AcroFields;
                var names = af.GetSignatureNames();

                af.RemoveField(names[index]);
                _reader.RemoveUnusedObjects();

                //Save
                tempStamper.Writer.CloseStream = false;
                tempStamper.Close();
                //Save to reader
                _reader.Close();
                _reader = new PdfReader(_byteStream.ToArray());
            }
            finally
            {
                // update internal variables
                _signatures.RemoveAt(index);
            }
        }

        /// <summary>
        /// Xóa tất cả chữ ký trên file
        /// </summary>
        public void RemoveAllSignature()
        {
            EnsureSignatures();

            //Tạo file tạm
            _byteStream = new MemoryStream();
            PdfStamper tempStamper = new PdfStamper(_reader, _byteStream);

            try
            {
                // Xóa trên file
                if (_reader == null) throw new ArgumentNullException("_reader");

                AcroFields af = _reader.AcroFields;
                var names = af.GetSignatureNames();

                foreach (string name in names)
                {
                    af.RemoveField(name);
                    _reader.RemoveUnusedObjects();
                }

                //Save
                tempStamper.Writer.CloseStream = false;
                tempStamper.Close();
                //Save to reader
                _reader.Close();
                _reader = new PdfReader(_byteStream.ToArray());
            }
            finally
            {
                // update internal variables
                _signatures.Clear();
            }
        }

        /// <summary>
        /// Trả về dữ liệu file sau khi ký. Nếu hàm ký bắn exception thì không sử dụng hàm này.
        /// </summary>
        /// <returns></returns>
        public byte[] ToArray()
        {
            return _byteStream.ToArray();
        }

        /// <summary>
        /// Đóng file và giải phóng tài nguyên
        /// </summary>
        public void Dispose()
        {
            _signatures = null;
            if (_reader != null)
                _reader.Close();
            if (_byteStream != null)
                _byteStream.Close();
        }
        #endregion

        #region Private members
        private MemoryStream _byteStream;
        private PdfReader _reader;
        private List<ESignature> _signatures;

        //Lookup the index of the signature object in the _signatures array by the name of the part
        private int GetSignatureIndex(ESignature signature)
        {
            EnsureSignatures();
            for (int i = 0; i < _signatures.Count; i++)
            {
                if (_signatures[i].Signer.SerialNumber == signature.Signer.SerialNumber && _signatures[i].SigningTime == signature.SigningTime
                     && _signatures[i].Verify == signature.Verify)
                    return i;
            }
            return -1;      // not found
        }

        // load signatures from container
        private void EnsureSignatures()
        {
            if (_signatures == null)
            {
                _signatures = new List<ESignature>();

                if (_reader == null) throw new ArgumentNullException("_reader");

                // Create the DigitalSignature Manager
                AcroFields af = _reader.AcroFields;

                var names = af.GetSignatureNames();

                foreach (string name in names)
                {
                    PdfPKCS7 pkcs = af.VerifySignature(name);
                    X509Certificate2 certificate = new X509Certificate2(pkcs.SigningCertificate.GetEncoded());
                    DateTime time = pkcs.SignDate;
                    VerifyResult result = VerifyResult.NotSigned;
                    if (pkcs.SigningCertificate == null)
                        result = VerifyResult.CertificateRequired;
                    if (!pkcs.Verify())
                        result = VerifyResult.InvalidSignature;
                    else
                        result = VerifyResult.Success;

                    ESignature sig = new ESignature(certificate, time, result);
                    var position = af.GetFieldPositions(name);
                    //truong hop co 3 cot
                    if (position[0].position.Width < 200)
                    {
                        if (position[0].position.Left == 0)
                        {
                            sig.posision = 1;
                        }

                        if (position[0].position.Left > 195 && position[0].position.Left < 400)
                        {
                            sig.posision = 2;
                        }

                        if (position[0].position.Left > 390)
                        {
                            sig.posision = 3;
                        }
                    }
                    else
                    {
                        //Truong hop co 2 cot
                        if (position[0].position.Left == 0)
                        {
                            sig.posision = 1;
                        }

                        if (position[0].position.Left >= 295)
                        {
                            sig.posision = 2;
                        }
                    }
                    _signatures.Add(sig);
                }
            }
        }
        #endregion

        //#region Static members
        ///// <summary>
        ///// Sign PDF File bằng USB Token
        ///// </summary>
        ///// <param name="sourcePath">Đường dẫn file để ký</param>
        ///// <param name="destinationPath">Đường dẫn file sau khi ký. Nếu destinationPath = sourcePath: ghi đè file gốc</param>
        ///// <param name="cert">Certificate ký</param>
        ///// <param name="field">Cấu hình khung hiển thị chữ kí hoặc null để không hiển thị</param>
        //public void SignPdfFile(string sourcePath, string destinationPath, X509Certificate2 cert, PdfSignatureField field)
        //{
        //    //Tạo file tạm
        //    string tempPath = Path.GetTempFileName();
        //    FileStream tempFile = new FileStream(tempPath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
        //    // reader and stamper
        //    PdfReader pdfReader = new PdfReader(sourcePath);
        //    PdfStamper pdfStamper = PdfStamper.CreateSignature(pdfReader, tempFile, '\0', null, true);

        //    try
        //    {
        //        //Certificate
        //        Org.BouncyCastle.X509.X509CertificateParser cp = new Org.BouncyCastle.X509.X509CertificateParser();
        //        Org.BouncyCastle.X509.X509Certificate[] chain = new Org.BouncyCastle.X509.X509Certificate[] { cp.ReadCertificate(cert.RawData) };
        //        //TOANTK: fix cứng HashAlgorithm = "SHA-1"
        //        IExternalSignature externalSignature = new X509Certificate2Signature(cert, Common.HashAlgorithm);

        //        // appearance
        //        PdfSignatureAppearance signatureAppearance = pdfStamper.SignatureAppearance;
        //        if (field != null)
        //        {
        //            signatureAppearance.Reason = field.Reason;
        //            signatureAppearance.Location = field.Location;
        //            signatureAppearance.Contact = field.Contact;
        //            signatureAppearance.SetVisibleSignature(field, 1, null);
        //        }

        //        //digital signature
        //        MakeSignature.SignDetached(signatureAppearance, externalSignature, chain, null, null, null, 0, CryptoStandard.CMS);

        //        //Copy vào file đích
        //        File.Copy(tempPath, destinationPath, true);
        //    }
        //    finally
        //    {
        //        //Đóng file
        //        pdfReader.Close();
        //        pdfStamper.Close();

        //        //Xóa file tạm
        //        if (tempFile != null)
        //            tempFile.Dispose();
        //        File.Delete(tempPath);
        //    }
        //}

        ///// <summary>
        ///// Sign Office File (word, excel) bằng HSM
        ///// </summary>
        ///// <param name="sourcePath">Đường dẫn file để ký</param>
        ///// <param name="destinationPath">Đường dẫn file sau khi ký. Nếu destinationPath = sourcePath: ghi đè file gốc</param>
        ///// <param name="cert">Certificate ký</param>
        ///// <param name="field">Cấu hình khung hiển thị chữ kí hoặc null để không hiển thị</param>
        ///// <param name="providerHSM">Giao tiếp HSM đã đăng nhập session và load private key. Nếu PrivateKey = null sẽ lấy từ HSM qua Certificate tương ứng</param>
        //public void SignPdfFile(string sourcePath, string destinationPath, X509Certificate2 cert, PdfSignatureField field, HSMServiceProvider providerHSM)
        //{
        //    //Tạo file tạm
        //    string tempPath = Path.GetTempFileName();
        //    FileStream tempFile = new FileStream(tempPath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
        //    // reader and stamper
        //    PdfReader pdfReader = new PdfReader(sourcePath);
        //    PdfStamper pdfStamper = PdfStamper.CreateSignature(pdfReader, tempFile, '\0', null, true);

        //    try
        //    {
        //        //Certificate
        //        Org.BouncyCastle.X509.X509CertificateParser cp = new Org.BouncyCastle.X509.X509CertificateParser();
        //        Org.BouncyCastle.X509.X509Certificate[] chain = new Org.BouncyCastle.X509.X509Certificate[] { cp.ReadCertificate(cert.RawData) };
        //        //TOANTK: fix cứng HashAlgorithm = "SHA-1"
        //        IExternalSignature externalSignature = new X509Certificate2Signature(cert, Common.HashAlgorithm, providerHSM);

        //        // appearance
        //        PdfSignatureAppearance signatureAppearance = pdfStamper.SignatureAppearance;
        //        if (field != null)
        //        {
        //            ////TOANTK: thời điểm ký. Mặc định = DateTime.Now
        //            //signatureAppearance.SignDate = DateTime.Now;
        //            signatureAppearance.Reason = field.Reason;
        //            signatureAppearance.Location = field.Location;
        //            signatureAppearance.Contact = field.Contact;
        //            signatureAppearance.SetVisibleSignature(field, 1, null);
        //        }

        //        //digital signature
        //        MakeSignature.SignDetached(signatureAppearance, externalSignature, chain, null, null, null, 0, CryptoStandard.CMS, providerHSM);

        //        //Copy vào file đích
        //        File.Copy(tempPath, destinationPath, true);
        //    }
        //    finally
        //    {
        //        //Đóng file
        //        pdfReader.Close();
        //        pdfStamper.Close();

        //        //Xóa file tạm
        //        if (tempFile != null)
        //            tempFile.Dispose();
        //        File.Delete(tempPath);
        //    }
        //}

        ///// <summary>
        ///// Xác thực tất cả các chữ ký trên file
        ///// </summary>
        ///// <param name="filePath"></param>
        ///// <returns></returns>
        //public VerifyResult VerifySignatures(string filePath)
        //{
        //    PdfReader reader = new PdfReader(filePath);
        //    AcroFields af = reader.AcroFields;
        //    var names = af.GetSignatureNames();

        //    if (names.Count == 0)
        //        return VerifyResult.NotSigned;

        //    foreach (string name in names)
        //    {
        //        PdfPKCS7 pkcs = af.VerifySignature(name);

        //        if (pkcs.SigningCertificate == null)
        //            return VerifyResult.CertificateRequired;

        //        if (!pkcs.Verify())
        //            return VerifyResult.InvalidSignature;
        //    }

        //    return VerifyResult.Success;
        //}

        ///// <summary>
        ///// Xác thực file có được ký bởi certificate
        ///// </summary>
        ///// <param name="filePath"></param>
        ///// <param name="certificate"></param>
        ///// <returns></returns>
        //public VerifyResult VerifySignatures(string filePath, X509Certificate2 certificate)
        //{
        //    VerifyResult result = VerifyResult.NotSigned;

        //    PdfReader reader = new PdfReader(filePath);
        //    AcroFields af = reader.AcroFields;
        //    var names = af.GetSignatureNames();

        //    foreach (string name in names)
        //    {
        //        PdfPKCS7 pkcs = af.VerifySignature(name);

        //        if (pkcs.SigningCertificate == null)
        //            result = VerifyResult.CertificateRequired;
        //        else
        //        {

        //            if (pkcs.SigningCertificate.GetEncoded().SequenceEqual(certificate.RawData) && pkcs.Verify())
        //                result = VerifyResult.Success;
        //            else
        //                result = VerifyResult.InvalidSignature;
        //        }

        //        if (result == VerifyResult.Success)
        //            return result;
        //    }

        //    return result;
        //}

        ///// <summary>
        ///// Xác thực từng chữ ký và trả về danh sách các chữ ký trên file
        ///// </summary>
        ///// <param name="filePath"></param>
        ///// <returns></returns>
        //public List<ESignature> VerifyAndGetAllSignatures(string filePath)
        //{
        //    List<ESignature> lstSig = new List<ESignature>();

        //    PdfReader reader = new PdfReader(filePath);
        //    AcroFields af = reader.AcroFields;
        //    var names = af.GetSignatureNames();

        //    foreach (string name in names)
        //    {
        //        PdfPKCS7 pkcs = af.VerifySignature(name);
        //        X509Certificate2 certificate = new X509Certificate2(pkcs.SigningCertificate.GetEncoded());
        //        DateTime time = pkcs.SignDate;
        //        string creator = pkcs.SignCreator;
        //        VerifyResult result = VerifyResult.NotSigned;
        //        if (pkcs.SigningCertificate == null)
        //            result = VerifyResult.CertificateRequired;
        //        if (!pkcs.Verify())
        //            result = VerifyResult.InvalidSignature;
        //        else
        //            result = VerifyResult.Success;

        //        ESignature sig = new ESignature(certificate, time, creator, result);
        //        lstSig.Add(sig);
        //    }

        //    return lstSig;
        //}
        //#endregion

        //private void checkRevocation(PdfPKCS7 pkcs, Org.BouncyCastle.X509.X509Certificate signCert, 
        //    Org.BouncyCastle.X509.X509Certificate issuerCert, DateTime date)
        //{
        //    List<BasicOcspResp> ocsps = new List<BasicOcspResp>();
        //    if (pkcs.Ocsp != null)
        //        ocsps.Add(pkcs.Ocsp);
        //    OcspVerifier ocspVerifier = new OcspVerifier(null, ocsps);
        //    List<VerificationOK> verification = ocspVerifier.Verify(signCert, issuerCert, date);
        //    if (verification.Count() == 0)
        //    {
        //        List<Org.BouncyCastle.X509.X509Crl> crls = new List<Org.BouncyCastle.X509.X509Crl>();
        //        if (pkcs.CRLs != null)
        //        {
        //            foreach (Org.BouncyCastle.X509.X509Crl crl in pkcs.CRLs)
        //                crls.Add(crl);
        //        }
        //        CrlVerifier crlVerifier = new CrlVerifier(null, crls);
        //        verification.AddRange(crlVerifier.Verify(signCert, issuerCert, date));
        //    }
        //    if (verification.Count() == 0)
        //    {
        //        Console.WriteLine("The signing certificate couldn't be verified");
        //    }
        //    else
        //    {
        //        foreach (VerificationOK v in verification)
        //            Console.WriteLine(v);
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
    }
}

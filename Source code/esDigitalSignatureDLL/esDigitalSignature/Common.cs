using esDigitalSignature.OfficePackage;
using esDigitalSignature.OfficePackage.Cryptography.Xml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using esDigitalSignature.iTextSharp.text.pdf;
using esDigitalSignature.iTextSharp.text;

namespace esDigitalSignature
{
    /// <summary>
    /// Lớp chứa các thành phần dùng chung
    /// </summary>
    public class Common
    {
        /// <summary>
        /// Tên phiên bản ký file để nhúng vào trường SignatureCreator của chữ ký.
        /// </summary>
        public static string VERSION = "esDigitalSignature 1.0.0 \u00a92014-2015 NLDC";

        /// <summary>
        /// Đường dẫn thư viện CRYPTOKI để kết nối với HSM.
        /// Nếu chỉ có tên dll thì hàm LoadLibrary tìm theo đường dẫn của biến PATH trong Environment Variable.
        /// </summary>
        public static string CRYPTOKI = "cryptoki.dll"; //@"C:\Program Files\SafeNet\Protect Toolkit 5\Protect Toolkit C SDK\bin\HSM\cryptoki.dll";
        /// <summary>
        /// Biến Environment Variable để thiết lập chế độ làm việc của HSM
        /// </summary>
        public static string ET_PTKC_GENERAL_LIBRARY_MODE = "ET_PTKC_GENERAL_LIBRARY_MODE";
        /// <summary>
        /// Chế độ HSM bình thường: nhìn thấy các slot logic
        /// </summary>
        public static string NORMAL = "NORMAL";
        /// <summary>
        /// Chế độ HSM cân bằng tải (Work Load Distribution): nhìn thấy các slot ảo được cấu hình trong registry
        /// </summary>
        public static string WLD = "WLD";


        /// <summary>
        /// Giải thuật băm. Thuật toán mã hóa RSA sẽ băm dữ liệu thành chuỗi hash trước khi ký
        /// </summary>
        public static string HashAlgorithm = "SHA1";

        #region Convert
        /// <summary>
        /// Tạo mảng byte từ chuỗi ký tự hexa. Hai ký tự hexa tạo thành 1 byte.
        /// </summary>
        /// <param name="hexString">string to convert to byte array</param>
        /// <returns>byte array, in the same left-to-right order as the hexString</returns>
        public static byte[] ConvertHexToByte(string hexString)
        {
            string newString = "";
            char c;
            // remove all none A-F, 0-9, characters
            for (int i = 0; i < hexString.Length; i++)
            {
                c = hexString[i];
                if (IsHexDigit(c))
                    newString += c;
            }
            // if odd number of characters, discard last character
            if (newString.Length % 2 != 0)
                newString = newString.Substring(0, newString.Length - 1);

            int byteLength = newString.Length / 2;
            byte[] bytes = new byte[byteLength];
            string hex;
            int j = 0;
            for (int i = 0; i < bytes.Length; i++)
            {
                hex = new String(new Char[] { newString[j], newString[j + 1] });
                bytes[i] = HexToByte(hex);
                j = j + 2;
            }
            return bytes;
        }

        /// <summary>
        /// Tạo chuỗi ký tự Hexa từ mảng byte. Mỗi byte tương đương 2 kí tự hexa.
        /// </summary>
        /// <param name="bytes">Byte array</param>
        /// <returns></returns>
        public static string ConvertBytesToHex(byte[] bytes)
        {
            string hexString = "";
            for (int i = 0; i < bytes.Length; i++)
            {
                hexString += bytes[i].ToString("X2");
            }
            return hexString;
        }

        /// <summary>
        /// Tạo chuỗi kí tự Base 64 từ nội dung file.
        /// </summary>
        /// <param name="filePath">Đường dẫn file</param>
        /// <returns></returns>
        public static string ConvertFileToBase64(string filePath)
        {
            // convert to byte array
            byte[] fileContent = File.ReadAllBytes(filePath);

            // convert to base64 from array byte
            return Convert.ToBase64String(fileContent, 0, fileContent.Length);
        }

        /// <summary>
        /// Tạo file từ chuỗi kí tự Base 64.
        /// </summary>
        /// <param name="base64">Chuỗi Base 64</param>
        /// <param name="filePath">Đường dẫn lưu file</param>
        public static void ConvertBase64ToFile(string base64, string filePath)
        {
            // convert to byte array
            byte[] fileContent = Convert.FromBase64String(base64);

            // create file excel in client
            File.WriteAllBytes(filePath, fileContent);
        }

        /// <summary>
        /// Tạo file PDF từ chuỗi text.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static byte[] ConvertTextToPDF(string text)
        {
            using (MemoryStream tempStream = new MemoryStream())
            {
                Document doc = new Document(iTextSharp.text.PageSize.A4, 60, 20, 20, 20);
                BaseFont baseFont = BaseFont.CreateFont("c:/windows/fonts/times.ttf", BaseFont.IDENTITY_H, true, true, null, null);
                baseFont.Subset = false;
                Font font = new Font(baseFont, 14);

                PdfWriter wri = PdfWriter.GetInstance(doc, tempStream);
                doc.Open();//Open Document to write

                Paragraph paragraph = new Paragraph(text, font);
                doc.Add(paragraph);
                
                doc.Close(); //Close document
                return tempStream.ToArray();
            }
        }
        #endregion

        #region Certificate
        /// <summary>
        /// Hiển thị danh sách chứng thư trong Store và lựa chọn. Trả về null nếu hủy chọn.
        /// </summary>
        /// <param name="title">The title of the dialog box.</param>
        /// <param name="message">A descriptive message to guide the user. The message is displayed in the dialog box.</param>
        /// <param name="hwndParent">A handle to the parent window to use for the display dialog box. [IntPtr.Zero] for no parent window.</param>
        /// <returns></returns>
        public static X509Certificate2 SelectCertificateFromStore(string title, string message, IntPtr hwndParent)
        {
            // Access Personal (MY) certificate store of current user
            X509Store my = new X509Store();
            my.Open(OpenFlags.ReadOnly | OpenFlags.OpenExistingOnly);

            // Find the certificate we'll use to sign 
            //Edited by Toantk on 21/5/2015
            //Thêm hwndParent để disable cửa sổ cha (~ ShowDialog)
            X509Certificate2Collection sel = X509Certificate2UI.SelectFromCollection(my.Certificates, title, message, X509SelectionFlag.SingleSelection, hwndParent);
            if (sel.Count > 0)
                return sel[0];
            else
                return null;
        }

        /// <summary>
        /// Lấy X509 certificate từ Windows Certificate Store theo serial number.
        /// </summary>
        /// <param name="serialNumber"></param>
        /// <returns></returns>
        public static X509Certificate2 GetCertificateBySerial(string serialNumber)
        {
            // Access Personal (MY) certificate store of current user
            X509Store my = new X509Store();
            my.Open(OpenFlags.ReadOnly);

            // Find the certificate we'll use to sign            
            foreach (X509Certificate2 cert in my.Certificates)
            {
                if (cert.SerialNumber.Equals(serialNumber, StringComparison.InvariantCultureIgnoreCase))
                {
                    return cert;
                }
            }

            return null;
        }

        /// <summary>
        /// Lấy X509 certificate từ file định dạng PEM (*.pem, *.cer, *.crt).
        /// </summary>
        /// <param name="crtPath"></param>
        /// <returns></returns>
        public static X509Certificate2 GetCertificateByFile(string crtPath)
        {
            return new X509Certificate2(crtPath);
        }

        /// <summary>
        /// Tạo kết nối với USB Token - kết nối được giữ trong certificate
        /// </summary>
        /// <param name="certificate">Chứng thư số gắn với token</param>
        /// <param name="password">Mật khẩu đăng nhập Token</param>
        /// <returns></returns>
        public static void ConnectUsbToken(X509Certificate2 certificate, string password)
        {
            if (certificate == null)
                throw new ArgumentNullException("certificate");

            //Convert password to SecureString
            SecureString pwd;
            if (String.IsNullOrEmpty(password))
                pwd = null;
            else
            {
                char[] scPwd = password.ToCharArray();
                unsafe
                {
                    fixed (char* pChars = scPwd)
                    {
                        pwd = new SecureString(pChars, scPwd.Length);
                    }
                }
            }

            //Lấy PrivateKey
            // we only release this key if we obtain it
            AsymmetricAlgorithm key = null;
            bool ownKey = false;
            if (certificate.HasPrivateKey)
            {
                key = certificate.PrivateKey;
            }
            else
            {
                ownKey = true;
                key = GetPrivateKeyForSigning(certificate);
            }

            try
            {
                //Khởi tạo ServiceProvider kết nối với USB Token
                if (key is System.Security.Cryptography.DSA)
                {
                    // make new CSP parameters based on parameters from current private key but throw in password
                    CspParameters cspParams = new CspParameters(
                        ((DSACryptoServiceProvider)key).CspKeyContainerInfo.ProviderType,
                        ((DSACryptoServiceProvider)key).CspKeyContainerInfo.ProviderName,
                        ((DSACryptoServiceProvider)key).CspKeyContainerInfo.KeyContainerName,
                        null,
                        pwd);

                    // make RSA crypto provider based on given CSP parameters
                    var dsaCsp = new DSACryptoServiceProvider(cspParams);
                    // set modified RSA crypto provider back
                    certificate.PrivateKey = dsaCsp;
                }
                else if (key is System.Security.Cryptography.RSA)
                {
                    // make new CSP parameters based on parameters from current private key but throw in password
                    //ProviderName xem thêm HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Cryptography\Defaults\Provider.
                    CspParameters cspParams = new CspParameters(
                        ((RSACryptoServiceProvider)key).CspKeyContainerInfo.ProviderType,
                        ((RSACryptoServiceProvider)key).CspKeyContainerInfo.ProviderName,
                        ((RSACryptoServiceProvider)key).CspKeyContainerInfo.KeyContainerName,
                        null,
                        pwd);

                    // make RSA crypto provider based on given CSP parameters
                    var rsaCsp = new RSACryptoServiceProvider(cspParams);
                    // set modified RSA crypto provider back
                    certificate.PrivateKey = rsaCsp;
                }
                else
                {
                    throw new CryptographicException(SecurityResources.GetResourceString("DigitalSignature_CreatedCspFailed"));
                }
            }
            finally
            {
                if (key != null && ownKey)
                    ((IDisposable)key).Dispose();
            }
        }

        /// <summary>
        /// Kiểm tra trạng thái chứng thư số
        /// </summary>
        /// <param name="certificate">Chứng thư cần kiểm tra</param>
        /// <param name="validateTime">Thời điểm cần kiểm tra</param>
        /// <param name="certificateStatus">Trả về chi tiết trạng thái</param>
        /// <returns></returns>
        public static bool ValidateCertificate(X509Certificate2 certificate, DateTime validateTime,
            out X509ChainStatus certificateStatus)
        {
            bool result = true;
            certificateStatus = new X509ChainStatus();

            X509Chain chain = new X509Chain();
            //chain.ChainPolicy.
            chain.ChainPolicy.VerificationTime = validateTime;
            chain.ChainPolicy.RevocationFlag = X509RevocationFlag.ExcludeRoot;
            chain.ChainPolicy.RevocationMode = X509RevocationMode.Online;
            chain.ChainPolicy.VerificationFlags = X509VerificationFlags.IgnoreEndRevocationUnknown | X509VerificationFlags.IgnoreCertificateAuthorityRevocationUnknown | X509VerificationFlags.IgnoreRootRevocationUnknown;

            result = chain.Build(certificate);
                        
            if (!result)
            {
                foreach (X509ChainStatus status in chain.ChainStatus)
                {
                    //// Kiểm tra nếu không check được Revocation thì chuyển sang check offline
                    //if (status.Status == X509ChainStatusFlags.OfflineRevocation || status.Status == X509ChainStatusFlags.RevocationStatusUnknown)
                    //    return ValidateCertificateOffline(certificate, validateTime, out certificateStatus);
                    //else
                    {
                        certificateStatus.Status |= status.Status;
                        certificateStatus.StatusInformation += status.StatusInformation;
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Lấy số CMND/MST của người dùng chứng thư.
        /// </summary>
        /// <param name="subject"></param>
        /// <returns></returns>
        public static string GetCertIDNumber(string subject)
        {
            List<string> lst = Parse(subject, "0.9.2342.19200300.100.1.1");
            if (lst == null || lst.Count == 0)
                return "";

            string[] id_no = lst[0].Split(new char[]{':'});
            if (id_no.Length == 1)
                return id_no[0];
            else if (id_no.Length == 2)
                return id_no[1];
            else
                return "";
        }
        #endregion

        #region Sign/Verify/Encrypt/Decrypt

        /// <summary>
        /// Ký chuỗi dữ liệu và trả về chuỗi chữ ký bằng USB Token.
        /// </summary>
        /// <param name="message">Dữ liệu cần ký</param>
        /// <param name="signer">Chứng thư ký</param>
        /// <returns></returns>
        public static byte[] Sign(byte[] message, X509Certificate2 signer)
        {
            // we only release this key if we obtain it
            AsymmetricAlgorithm key = null;
            bool ownKey = false;
            if (signer.HasPrivateKey)
            {
                key = signer.PrivateKey;
            }
            else
            {
                ownKey = true;
                key = GetPrivateKeyForSigning(signer);
            }

            try
            {
                //TOANTK: Kí mess (chưa băm) bằng USB Token
                if (key is RSACryptoServiceProvider)
                {
                    RSACryptoServiceProvider rsa = (RSACryptoServiceProvider)key;
                    return rsa.SignData(message, CryptoConfig.MapNameToOID(HashAlgorithm));
                }
                else if (key is DSACryptoServiceProvider)
                {
                    DSACryptoServiceProvider dsa = (DSACryptoServiceProvider)key;
                    return dsa.SignData(message, 0, message.Length);
                }
                else
                {
                    throw new CryptographicException("DLL_CryptographyAlgorithmNotSupported");
                }
            }
            finally
            {
                if (key != null && ownKey)
                    ((IDisposable)key).Dispose();
            }
        }

        /// <summary>
        /// Ký chuỗi dữ liệu và trả về chuỗi chữ ký bằng HSM.
        /// </summary>
        /// <param name="message">Dữ liệu cần ký</param>
        /// <param name="providerHSM">HSMServiceProvider đã đăng nhập và load private key để ký</param>
        /// <returns></returns>
        public static byte[] Sign(byte[] message, HSMServiceProvider providerHSM)
        {
            //TOANTK: Kí mess (chưa băm) bằng HSM
            //băm
            HashAlgorithm hashAlg = (HashAlgorithm)CryptoConfig.CreateFromName(HashAlgorithm);
            byte[] hashVal = hashAlg.ComputeHash(message);

            //Kí chuỗi hash
            string sOID = CryptoConfig.MapNameToOID(hashAlg.ToString());
            return providerHSM.SignHash(hashAlg.Hash, sOID);
        }

        /// <summary>
        /// Kiểm tra tính toàn vẹn của chữ ký số bằng cách giải mã chữ ký sử dụng Public Key và so sánh với dữ liệu được ký.
        /// </summary>
        /// <param name="message">Dữ liệu được ký</param>
        /// <param name="signature">Chữ ký để kiểm tra</param>
        /// <param name="certificate">Chứng thư ký</param>
        /// <returns></returns>
        public static bool Verify(byte[] message, byte[] signature, X509Certificate2 certificate)
        {
            //TOANTK: Kí mess (chưa băm) bằng USB Token
            if (certificate.PublicKey.Key is RSACryptoServiceProvider)
            {
                RSACryptoServiceProvider rsa = (RSACryptoServiceProvider)certificate.PublicKey.Key;
                return rsa.VerifyData(message, CryptoConfig.MapNameToOID(HashAlgorithm), signature);
            }
            else if (certificate.PublicKey.Key is DSACryptoServiceProvider)
            {
                DSACryptoServiceProvider dsa = (DSACryptoServiceProvider)certificate.PublicKey.Key;
                return dsa.VerifyData(message, signature);
            }
            else
            {
                throw new CryptographicException("DLL_CryptographyAlgorithmNotSupported");
            }
        }

        /// <summary>
        /// Mã hóa chuỗi bằng Public Key (chỉ hỗ trợ RSA)
        /// </summary>
        /// <param name="message"></param>
        /// <param name="certificate"></param>
        /// <returns></returns>
        public static byte[] Encrypt(byte[] message, X509Certificate2 certificate)
        {
            if (certificate.PublicKey.Key is RSACryptoServiceProvider)
            {
                RSACryptoServiceProvider rsa = (RSACryptoServiceProvider)certificate.PublicKey.Key;
                return rsa.Encrypt(message, false);
            }
            else
            {
                throw new CryptographicException("DLL_CryptographyAlgorithmNotSupported");
            }
        }

        /// <summary>
        /// Giải mã chuỗi bằng Privte Key (chỉ hỗ trợ RSA) trên USB Token
        /// </summary>
        /// <param name="message"></param>
        /// <param name="certificate"></param>
        /// <returns></returns>
        public static byte[] Decrypt(byte[] message, X509Certificate2 certificate)
        {
            // we only release this key if we obtain it
            AsymmetricAlgorithm key = null;
            bool ownKey = false;
            if (certificate.HasPrivateKey)
            {
                key = certificate.PrivateKey;
            }
            else
            {
                ownKey = true;
                key = GetPrivateKeyForSigning(certificate);
            }

            try
            {
                //TOANTK: Giải mã mess bằng USB Token
                if (key is RSACryptoServiceProvider)
                {
                    RSACryptoServiceProvider rsa = (RSACryptoServiceProvider)key;
                    
                    return rsa.Decrypt(message, false);
                }
                else
                {
                    throw new CryptographicException("DLL_CryptographyAlgorithmNotSupported");
                }
            }
            finally
            {
                if (key != null && ownKey)
                    ((IDisposable)key).Dispose();
            }
        }

        /// <summary>
        /// Giải mã chuỗi bằng Privte Key (chỉ hỗ trợ RSA) trên HSM
        /// </summary>
        /// <param name="message">Tin nhắn mã hóa</param>
        /// <param name="providerHSM">HSMServiceProvider đã đăng nhập và load private key để giải mã</param>
        /// <returns></returns>
        public static byte[] Decrypt(byte[] message, HSMServiceProvider providerHSM)
        {
            return providerHSM.DecryptMessage(message);
        }

        /// <summary>
        /// Mã hóa chuỗi bằng Public Key của HSM. Thêm 1 byte chỉ độ dài chuỗi.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="certificate"></param>
        /// <returns></returns>
        public static byte[] Encrypt_WithLength(byte[] message, X509Certificate2 certificate)
        {
            if (message.Length > byte.MaxValue)
                throw new CryptographicException("DLL_MessageTooLong");

            if (certificate.PublicKey.Key is RSACryptoServiceProvider)
            {
                RSACryptoServiceProvider rsa = (RSACryptoServiceProvider)certificate.PublicKey.Key;
                //return rsa.Encrypt(message, false);
                byte[] bEnc = rsa.Encrypt(message, false);

                //Thêm 1 byte đầu chỉ độ dài chuỗi đc mã hóa
                byte[] bOutput = new byte[bEnc.Length + 1];
                bOutput[0] = Convert.ToByte(message.Length);
                for (int i = 0; i < bEnc.Length; i++)
                {
                    bOutput[i + 1] = bEnc[i];
                }
                return bOutput;
            }
            else
            {
                throw new CryptographicException("DLL_CryptographyAlgorithmNotSupported");
            }
        }

        /// <summary>
        /// Giải mã chuỗi bằng Privte Key (chỉ hỗ trợ RSA) trên HSM. Có 1 byte chỉ độ dài chuỗi.
        /// </summary>
        /// <param name="message">Tin nhắn mã hóa</param>
        /// <param name="providerHSM">HSMServiceProvider đã đăng nhập và load private key để giải mã</param>
        /// <returns></returns>
        public static byte[] Decrypt_WithLength(byte[] message, HSMServiceProvider providerHSM)
        {
            //return providerHSM.DecryptMessage(message);

            //Input: bỏ byte đầu của chuỗi
            byte[] bInput = new byte[message.Length - 1];
            for (int i = 0; i < bInput.Length; i++)
            {
                bInput[i] = message[i + 1];
            }
            //Output: Lấy 1 byte đầu của chuỗi chỉ độ dài chuỗi đc mã hóa
            byte[] bOutput = new byte[Convert.ToInt32(message[0])];
            //Giải mã
            byte[] bDec = providerHSM.DecryptMessage(bInput);
            //Chỉ lấy phần độ dài được mã hóa
            for (int i = 0; i < bOutput.Length; i++)
            {
                bOutput[i] = bDec[i];
            }
            //Trả về
            return bOutput;
        }
        #endregion

        #region Private Members
        internal static object CreateTransformFromName(string name)
        {
            object obj = null;

            switch (name)
            {
                case SignedXml.XmlDsigC14NTransformUrl:
                    obj = new esDigitalSignature.OfficePackage.Cryptography.Xml.XmlDsigC14NTransform();
                    break;
                case SignedXml.XmlDsigC14NWithCommentsTransformUrl:
                    obj = new esDigitalSignature.OfficePackage.Cryptography.Xml.XmlDsigC14NWithCommentsTransform();
                    break;
                case SignedXml.XmlDsigExcC14NTransformUrl:
                    obj = new esDigitalSignature.OfficePackage.Cryptography.Xml.XmlDsigExcC14NTransform();
                    break;
                case SignedXml.XmlDsigExcC14NWithCommentsTransformUrl:
                    obj = new esDigitalSignature.OfficePackage.Cryptography.Xml.XmlDsigExcC14NWithCommentsTransform();
                    break;
                case SignedXml.XmlDsigBase64TransformUrl:
                    obj = new esDigitalSignature.OfficePackage.Cryptography.Xml.XmlDsigBase64Transform();
                    break;
                case SignedXml.XmlDsigXPathTransformUrl:
                    obj = new esDigitalSignature.OfficePackage.Cryptography.Xml.XmlDsigXPathTransform();
                    break;
                case SignedXml.XmlDsigXsltTransformUrl:
                    obj = new esDigitalSignature.OfficePackage.Cryptography.Xml.XmlDsigXsltTransform();
                    break;
                case SignedXml.XmlDsigEnvelopedSignatureTransformUrl:
                    obj = new esDigitalSignature.OfficePackage.Cryptography.Xml.XmlDsigEnvelopedSignatureTransform();
                    break;
                case SignedXml.XmlDecryptionTransformUrl:
                    obj = new esDigitalSignature.OfficePackage.Cryptography.Xml.XmlDecryptionTransform();
                    break;
                case SignedXml.XmlLicenseTransformUrl:
                    obj = new esDigitalSignature.OfficePackage.Cryptography.Xml.XmlLicenseTransform();
                    break;
                default:
                    break;
            }

            return obj;
        }

        /// <summary>
        /// Returns true is c is a hexadecimal digit (A-F, a-f, 0-9)
        /// </summary>
        /// <param name="c">Character to test</param>
        /// <returns>true if hex digit, false if not</returns>
        private static bool IsHexDigit(Char c)
        {
            int numChar;
            int numA = Convert.ToInt32('A');
            int num1 = Convert.ToInt32('0');
            c = Char.ToUpper(c);
            numChar = Convert.ToInt32(c);
            if (numChar >= numA && numChar < (numA + 6))
                return true;
            if (numChar >= num1 && numChar < (num1 + 10))
                return true;
            return false;
        }

        /// <summary>
        /// Converts 1 or 2 character string into equivalant byte value
        /// </summary>
        /// <param name="hex">1 or 2 character string</param>
        /// <returns>byte</returns>
        private static byte HexToByte(string hex)
        {
            if (hex.Length > 2 || hex.Length <= 0)
                throw new ArgumentException("DLL_HexLengthMustBe1or2");
            byte newByte = byte.Parse(hex, System.Globalization.NumberStyles.HexNumber);
            return newByte;
        }

        private static List<string> Parse(string data, string delimiter)
        {
            if (data == null) return null;
            if (!delimiter.EndsWith("=")) delimiter = delimiter + "=";
            if (!data.Contains(delimiter)) return null;
            //base case
            var result = new List<string>();
            int start = data.IndexOf(delimiter) + delimiter.Length;
            int length = data.IndexOf(',', start) - start;
            if (length == 0) return null; //the group is empty
            if (length > 0)
            {
                result.Add(data.Substring(start, length));
                //only need to recurse when the comma was found, because there could be more groups
                var rec = Parse(data.Substring(start + length), delimiter);
                if (rec != null) result.AddRange(rec); //can't pass null into AddRange() :(
            }
            else //no comma found after current group so just use the whole remaining string
            {
                result.Add(data.Substring(start));
            }
            return result;
        }

        /// <summary>
        /// lookup the private key using the given identity
        /// </summary>
        /// <param name="signer">X509Cert</param>
        /// <returns>IDisposable asymmetric algorithm that serves as a proxy to the private key.  Caller must dispose
        /// of properly.</returns>
        private static AsymmetricAlgorithm GetPrivateKeyForSigning(X509Certificate2 signer)
        {
            // if the certificate does not actually contain the key, we need to look it up via ThumbPrint
            Invariant.Assert(!signer.HasPrivateKey);

            // look for appropriate certificates
            X509Store store = new X509Store(StoreLocation.CurrentUser);

            try
            {
                store.Open(OpenFlags.OpenExistingOnly | OpenFlags.ReadOnly);

                X509Certificate2Collection collection = (X509Certificate2Collection)store.Certificates;

                collection = collection.Find(X509FindType.FindByThumbprint, signer.Thumbprint, true);
                if (collection.Count > 0)
                {
                    if (collection.Count > 1)
                        throw new CryptographicException("DLL_DuplicateCertificate");

                    signer = collection[0];
                }
                else
                    throw new CryptographicException("DLL_CannotLocateCertificate");
            }
            finally
            {
                store.Close();
            }

            // get the corresponding AsymmetricAlgorithm
            return signer.PrivateKey;
        }

        /// <summary>
        /// Kiểm tra trạng thái chứng thư số trong cache
        /// </summary>
        /// <param name="certificate">Chứng thư cần kiểm tra</param>
        /// <param name="validateTime">Thời điểm cần kiểm tra</param>
        /// <param name="certificateStatus">Trả về chi tiết trạng thái</param>
        /// <returns></returns>
        private static bool ValidateCertificateOffline(X509Certificate2 certificate, DateTime validateTime,
            out X509ChainStatus certificateStatus)
        {
            bool result = true;
            certificateStatus = new X509ChainStatus();

            X509Chain chain = new X509Chain();
            chain.ChainPolicy.VerificationTime = validateTime;
            chain.ChainPolicy.RevocationMode = X509RevocationMode.NoCheck;

            result = chain.Build(certificate);

            if (!result)
            {
                foreach (X509ChainStatus status in chain.ChainStatus)
                {
                    certificateStatus.Status |= status.Status;
                    certificateStatus.StatusInformation += status.StatusInformation;
                }
            }

            return result;
        }
        #endregion
    }
}

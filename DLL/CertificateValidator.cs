using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;

namespace esDigitalSignature
{
    /// <summary>
    /// Chế độ kiểm tra revocation
    /// </summary>
    public enum CRevocationMode
    {
        /// <summary>
        /// Không kiểm tra
        /// </summary>
        NoCheck = 0,

        /// <summary>
        /// Check online bằng link CRL trong certificate
        /// </summary>
        CertificateUrl = 1,

        /// <summary>
        /// Check online bằng link CRL tùy chỉnh
        /// </summary>
        CustomUrl = 2,

        /// <summary>
        /// Check offine bằng file CRL
        /// </summary>
        CustomCRLFile = 3
    }

    /// <summary>
    /// Kết quả xác thực - trạng thái chứng thư số
    /// </summary>
    public enum CValidationStatus
    {
        /// <summary>
        /// Hợp lệ
        /// </summary>
        Valid,

        /// <summary>
        /// Chưa được cấp phát
        /// </summary>
        NotIssue,

        /// <summary>
        /// Hết hạn
        /// </summary>
        Expired,

        /// <summary>
        /// Không thể kiểm tra trạng thái thu hồi
        /// </summary>
        RevocationStatusUnknown,

        /// <summary>
        /// Đã thu hồi
        /// </summary>
        Revoked,

        /// <summary>
        /// Không thể xác thực
        /// </summary>
        ValidationStatusUnknown
    }

    //Example:
    //CertificateValidator validator = new CertificateValidator();
    //validator.VerificationTime = _signingTime;
    //validator.RevocationMode = CRevocationMode.CertificateUrl;
    //validator.CrlLink = "";
    //return validator.Build(_signer);
    public class CertificateValidator
    {
        /*
         * 
         * *********Properties*********
         * 
         */
        /// <summary>
        /// Thời điểm cần xác thực (local time). Mặc định = DateTime.Now
        /// </summary>
        public DateTime VerificationTime
        {
            get { return _verificationTime; }
            set { _verificationTime = value; }
        }

        /// <summary>
        /// Lấy hoặc thiết lập chế độ kiểm tra revocation. Mặc định = CRevocationMode.CertificateUrl
        /// </summary>
        public CRevocationMode RevocationMode
        {
            get { return _revocationMode; }
            set { _revocationMode = value; }
        }

        /// <summary>
        /// Lấy hoặc thiết lập địa chỉ CRL file
        /// </summary>
        public string CrlLink
        {
            get { return _crlLink; }
            set { _crlLink = value; }
        }

        /// <summary>
        /// Kết quả xác thực - trạng thái chứng thư số
        /// </summary>
        public CValidationStatus ValidationStatus
        {
            get { return _validationStatus; }
        }

        /// <summary>
        /// Ngày thu hồi. Mặc định = DateTime.MinValue
        /// </summary>
        public DateTime RevocationDate
        {
            get { return _revocationDate; }
        }

        /// <summary>
        /// Chuỗi chứng thư số (user certificate - certificate authority (CA) - Root CA)
        /// </summary>
        public List<X509Certificate2> CertificateChain
        {
            get { return _certificateChain; }
        }

        /*
         * 
         * *********Methods*********
         * 
         */
        /// <summary>
        /// Kiểm tra xác thực certificate và lấy chuỗi chứng thư. Trả về true nếu thành công, false nếu có lỗi.
        /// Kết quả kiểm tra: ValidationStatus, RevocationDate và CertificateChain
        /// </summary>
        /// <param name="certificate"></param>
        /// <returns></returns>
        public CValidationStatus Build(X509Certificate2 certificate)
        {
            try
            {
                _validationStatus = ValidateCertificate(certificate);
                _certificateChain = GetCertificateChain(certificate);

                return _validationStatus;
            }
            catch
            {
                _validationStatus = CValidationStatus.ValidationStatusUnknown;
                return _validationStatus;
            }
        }

        /// <summary>
        /// Kiểm tra certificate có trong danh sách thu hồi hay không?
        /// </summary>
        /// <param name="certificate">Chứng thư cần kiểm tra</param>
        /// <param name="crlInfo">Thông tin CRL</param>
        /// <param name="revocationDate">Nếu có trả về ngày thu hồi, nếu không trả về DateTime.MinValue</param>
        /// <returns></returns>
        public bool IsCertificateInCRL(X509Certificate2 certificate, CRL_Info crlInfo, out DateTime revocationDate)
        {
            if (!crlInfo.Issuer.RawData.SequenceEqual(certificate.IssuerName.RawData))
                throw new CryptographicException("CrlIssuerNotMatch");

            foreach (CRL_Entry entry in crlInfo.CrlEntries)
            {
                if (certificate.SerialNumber == entry.SerialNumber)
                {
                    revocationDate = entry.RevocationDate;
                    return true;
                }
            }

            revocationDate = DateTime.MinValue;
            return false;
        }

        /// <summary>
        /// Lấy thông tin CRL
        /// </summary>
        /// <param name="crlLink">Đường dẫn CRL file (web url hoặc local path)</param>
        /// <returns></returns>
        public CRL_Info GetCRlFromLink(string crlLink)
        {
            byte[] rgRawCrl;
            if (crlLink.Contains("http") || crlLink.Contains("ftp"))
            {
                WebClient wc = new WebClient();
                rgRawCrl = wc.DownloadData(crlLink);
            }
            else
                rgRawCrl = File.ReadAllBytes(crlLink);

            IntPtr phCertStore = IntPtr.Zero;
            IntPtr pvContext = IntPtr.Zero;
            GCHandle hCrlData = new GCHandle();
            GCHandle hCryptBlob = new GCHandle();
            try
            {
                hCrlData = GCHandle.Alloc(rgRawCrl, GCHandleType.Pinned);
                WinCrypt32.CRYPTOAPI_BLOB stCryptBlob;
                stCryptBlob.cbData = rgRawCrl.Length;
                stCryptBlob.pbData = hCrlData.AddrOfPinnedObject();
                hCryptBlob = GCHandle.Alloc(stCryptBlob, GCHandleType.Pinned);

                if (!WinCrypt32.CryptQueryObject(
                    WinCrypt32.CERT_QUERY_OBJECT_BLOB,
                    hCryptBlob.AddrOfPinnedObject(),
                    WinCrypt32.CERT_QUERY_CONTENT_FLAG_CRL,
                    WinCrypt32.CERT_QUERY_FORMAT_FLAG_BINARY,
                    0,
                    IntPtr.Zero,
                    IntPtr.Zero,
                    IntPtr.Zero,
                    ref phCertStore,
                    IntPtr.Zero,
                    ref pvContext
                    ))
                {
                    throw new Win32Exception(Marshal.GetLastWin32Error());
                }

                WinCrypt32.CRL_CONTEXT stCrlContext = (WinCrypt32.CRL_CONTEXT)Marshal.PtrToStructure(pvContext, typeof(WinCrypt32.CRL_CONTEXT));
                WinCrypt32.CRL_INFO stCrlInfo = (WinCrypt32.CRL_INFO)Marshal.PtrToStructure(stCrlContext.pCrlInfo, typeof(WinCrypt32.CRL_INFO));

                return new CRL_Info(stCrlInfo);
            }
            finally
            {
                if (hCrlData.IsAllocated) hCrlData.Free();
                if (hCryptBlob.IsAllocated) hCryptBlob.Free();
                if (!pvContext.Equals(IntPtr.Zero))
                {
                    WinCrypt32.CertFreeCRLContext(pvContext);
                }
            }
        }

        /*
         * 
         * Private members
         * 
         */
        //Xác thực
        private CValidationStatus ValidateCertificate(X509Certificate2 certificate)
        {
            //Kiểm tra thời hạn chứng thư
            if (_verificationTime < certificate.NotBefore)
                return CValidationStatus.NotIssue;

            if (_verificationTime > certificate.NotAfter)
                return CValidationStatus.Expired;

            if (_revocationMode == CRevocationMode.NoCheck)
                return CValidationStatus.Valid;

            //Kiểm tra chứng thư có bị thu hồi
            try
            {
                //Lấy link CRL
                if (_revocationMode == CRevocationMode.CertificateUrl)
                    _crlLink = GetBaseCrlUrl(certificate);

                //Lấy thông tin CRL
                CRL_Info crl = GetCRlFromLink(_crlLink);
                //Kiểm tra chứng thư có trong CRL hay không
                if (IsCertificateInCRL(certificate, crl, out _revocationDate))
                    if (_verificationTime >= _revocationDate)
                        return CValidationStatus.Revoked;

                //Kiểm tra DeltaCRL
                if (_revocationMode == CRevocationMode.CertificateUrl)
                {
                    string deltaUrl = GetDeltaCrlUrl(crl.StCrlInfo);
                    if (!string.IsNullOrEmpty(deltaUrl))
                    {
                        //Lấy thông tin CRL
                        CRL_Info deltaCrl = GetCRlFromLink(deltaUrl);
                        //Kiểm tra chứng thư có trong CRL hay không
                        if (IsCertificateInCRL(certificate, deltaCrl, out _revocationDate))
                            if (_verificationTime >= _revocationDate)
                                return CValidationStatus.Revoked;
                    }
                }
            }
            catch
            {
                return CValidationStatus.RevocationStatusUnknown;
            }

            return CValidationStatus.Valid;
        }

        //Lấy chuỗi chứng thư trong certificate
        private static List<X509Certificate2> GetCertificateChain(X509Certificate2 certificate)
        {
            List<X509Certificate2> lst = new List<X509Certificate2>();

            X509Chain chain = new X509Chain();
            chain.ChainPolicy.RevocationMode = X509RevocationMode.Online;
            chain.ChainPolicy.RevocationFlag = X509RevocationFlag.EntireChain;
            chain.ChainPolicy.VerificationFlags = X509VerificationFlags.NoFlag;

            chain.Build(certificate);
            foreach (X509ChainElement c in chain.ChainElements)
                lst.Add(c.Certificate);

            return lst;
        }

        private static string GetBaseCrlUrl(X509Certificate2 cert)
        {
            try
            {
                return (from X509Extension extension in cert.Extensions
                        where extension.Oid.Value.Equals(CERT_CRL_EXTENSION)
                        select GetCrlUrlFromExtension(extension)).Single();
            }
            catch
            {
                return null;
            }
        }

        private static string GetDeltaCrlUrl(WinCrypt32.CRL_INFO stCrlInfo)
        {
            IntPtr rgExtension = stCrlInfo.rgExtension;
            X509Extension deltaCrlExtension = null;

            for (int i = 0; i < stCrlInfo.cExtension; i++)
            {
                WinCrypt32.CERT_EXTENSION stCrlExt = (WinCrypt32.CERT_EXTENSION)Marshal.PtrToStructure(rgExtension, typeof(WinCrypt32.CERT_EXTENSION));

                if (stCrlExt.Value.pbData != IntPtr.Zero && stCrlExt.pszObjId == CRL_CRL_EXTENSION)
                {
                    byte[] rawData = new byte[stCrlExt.Value.cbData];
                    Marshal.Copy(stCrlExt.Value.pbData, rawData, 0, rawData.Length);
                    deltaCrlExtension = new X509Extension(stCrlExt.pszObjId, rawData, stCrlExt.fCritical);
                    break;
                }

                rgExtension = (IntPtr)(rgExtension.ToInt64() + Marshal.SizeOf(typeof(WinCrypt32.CERT_EXTENSION)));
            }
            if (deltaCrlExtension == null)
            {
                return null;
            }
            return GetCrlUrlFromExtension(deltaCrlExtension);
        }

        private static string GetCrlUrlFromExtension(X509Extension extension)
        {
            try
            {
                Regex rx = new Regex("http://.*crl");
                string raw = new AsnEncodedData(extension.Oid, extension.RawData).Format(false);
                return rx.Match(raw).Value;
            }
            catch
            {
                return null;
            }
        }

        private const string CERT_CRL_EXTENSION = "2.5.29.31";
        private const string CRL_CRL_EXTENSION = "2.5.29.46";

        //Tham số check
        DateTime _verificationTime = DateTime.Now;
        CRevocationMode _revocationMode = CRevocationMode.CertificateUrl;
        string _crlLink;

        //Kết quả
        CValidationStatus _validationStatus;
        DateTime _revocationDate;
        List<X509Certificate2> _certificateChain;
    }

    /// <summary>
    /// Thông tin file CRL
    /// </summary>
    public class CRL_Info
    {
        private WinCrypt32.CRL_INFO _stCrlInfo;
        private X500DistinguishedName _issuer;
        private DateTime _thisUpdate;
        private DateTime _nextUpdate;
        private CRL_Entry[] _crlEntries;

        /// <summary>
        /// Win32 Struct
        /// </summary>
        internal WinCrypt32.CRL_INFO StCrlInfo
        {
            get { return _stCrlInfo; }
        }

        /// <summary>
        /// Người phát hành
        /// </summary>
        public X500DistinguishedName Issuer
        {
            get { return _issuer; }
        }

        /// <summary>
        /// Ngày phát hành
        /// </summary>
        public DateTime ThisUpdate
        {
            get { return _thisUpdate; }
        }

        /// <summary>
        /// Ngày sẽ phát hành CRL kế tiếp
        /// </summary>
        public DateTime NextUpdate
        {
            get { return _nextUpdate; }
        }

        /// <summary>
        /// Danh sách revoked certificates
        /// </summary>
        public CRL_Entry[] CrlEntries
        {
            get { return _crlEntries; }
        }

        internal CRL_Info(WinCrypt32.CRL_INFO stCrlInfo)
        {
            _stCrlInfo = stCrlInfo;
            _issuer = new X500DistinguishedName(WinCrypt32.BLOBToByteArray(stCrlInfo.Issuer));
            _thisUpdate = WinCrypt32.FILETIMEToDateTime(stCrlInfo.ThisUpdate);
            _nextUpdate = WinCrypt32.FILETIMEToDateTime(stCrlInfo.NextUpdate);
            _crlEntries = new CRL_Entry[stCrlInfo.cCRLEntry];

            //Lấy danh sách revoked certificates
            IntPtr rgCrlEntry = stCrlInfo.rgCRLEntry;
            for (int i = 0; i < stCrlInfo.cCRLEntry; i++)
            {
                //Get from pointer
                WinCrypt32.CRL_ENTRY stCrlEntry = (WinCrypt32.CRL_ENTRY)Marshal.PtrToStructure(rgCrlEntry, typeof(WinCrypt32.CRL_ENTRY));

                //Get info
                _crlEntries[i].SerialNumber = WinCrypt32.BLOBToSerial(stCrlEntry.SerialNumber);
                _crlEntries[i].RevocationDate = WinCrypt32.FILETIMEToDateTime(stCrlEntry.RevocationDate);

                //Next entry pointer
                rgCrlEntry = (IntPtr)(rgCrlEntry.ToInt64() + Marshal.SizeOf(typeof(WinCrypt32.CRL_ENTRY)));
            }
        }
    }

    /// <summary>
    /// Thông tin chứng thư bị thu hồi (serial, revocation date)
    /// </summary>
    public struct CRL_Entry
    {
        public string SerialNumber;
        public DateTime RevocationDate;
    }
}

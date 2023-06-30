using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace esDigitalSignature
{
    /// <summary>
    /// Lớp quản lý ký và chữ ký số trên file. Cần dispose khi không sử dụng để giải phóng bộ nhớ.
    /// <para>Hỗ trợ các định dạng PDF, Office (2007 trở lên) và XML</para>
    /// </summary>
    public class ESDigitalSignatureManager : IDisposable
    {
        private IDigitalSignatureManagerBase _dsm;

        /// <summary>
        /// Các chữ ký trên file
        /// </summary>
        public List<ESignature> Signatures
        {
            get {   return _dsm.Signatures; }
        }

        /// <summary>
        /// File đã được ký hay chưa?
        /// </summary>
        public bool IsSigned
        {
            get {   return _dsm.IsSigned;}
        }

        /// <summary>
        /// Khởi tạo với dữ liệu file - đọc gói file và load chữ ký trên file
        /// </summary>
        /// <param name="fileData">Dữ liệu file</param>
        /// <param name="fileExtension">Định dạng file (.pdf,.docx,.xlsx,.xml hoặc .bid)</param>
        public ESDigitalSignatureManager(byte[] fileData, string fileExtension)
        {
            fileExtension = fileExtension.ToLower();
            if (fileExtension == ".pdf")
            {
                _dsm = new PdfDigitalSignatureManager(fileData);
            }
            else if (fileExtension == ".docx" || fileExtension == ".xlsx")
            {
                _dsm = new OfficeDigitalSignatureManager(fileData);
            }
            else if (fileExtension == ".xml" || fileExtension == ".bid")
            {
                _dsm = new XmlDigitalSignatureManager(fileData);
            }
            else
                throw new Exception("DLL_FileExtensionNotSupported");
        }

        /// <summary>
        /// Ký file bằng USB Token
        /// </summary>
        /// <param name="certificate">Certificate ký</param>
        /// <returns></returns>
        public void Sign(X509Certificate2 certificate)
        {
            _dsm.Sign(certificate);
        }

        /// <summary>
        /// Ký file bằng HSM
        /// </summary>
        /// <param name="certificate">Certificate ký</param>
        /// <param name="providerHSM">Giao tiếp HSM đã đăng nhập session và load private key. Nếu PrivateKey = null sẽ lấy từ HSM qua Certificate tương ứng</param>
        /// <returns></returns>
        public void Sign(X509Certificate2 certificate, HSMServiceProvider providerHSM)
        {
            _dsm.Sign(certificate, providerHSM);
        }

        /// <summary>
        /// Xác thực tất cả các chữ ký - chạy xác thực trên mỗi chữ ký
        /// </summary>
        /// <returns></returns>
        public VerifyResult VerifySignatures()
        {
            return _dsm.VerifySignatures();
        }

        /// <summary>
        /// Xác thực file có được ký bởi certificate
        /// </summary>
        /// <param name="certificate"></param>
        /// <returns></returns>
        public VerifyResult VerifySignatures(X509Certificate2 certificate)
        {
            return _dsm.VerifySignatures(certificate);
        }

        /// <summary>
        /// Lấy chuỗi Hash value của file
        /// </summary>
        /// <returns></returns>
        public byte[] GetHashValue()
        {
            return _dsm.GetHashValue();
        }

        /// <summary>
        /// Xóa một chữ ký trên file
        /// </summary>
        /// <param name="signatureES"></param>
        public void RemoveSignature(ESignature signatureES)
        {
            _dsm.RemoveSignature(signatureES);
        }

        /// <summary>
        /// Xóa tất cả chữ ký trên file
        /// </summary>
        public void RemoveAllSignature()
        {
            _dsm.RemoveAllSignature();
        }

        /// <summary>
        /// Trả về dữ liệu file sau khi ký. Nếu hàm ký bắn exception thì không sử dụng hàm này.
        /// </summary>
        /// <returns></returns>
        public byte[] ToArray()
        {
            return _dsm.ToArray();
        }

        /// <summary>
        /// Đóng file và giải phóng tài nguyên
        /// </summary>
        public void Dispose()
        {
            _dsm.Dispose();
        }
    }
}

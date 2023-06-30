using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace esDigitalSignature
{
    /// <summary>
    /// Giao diện DigitalSignatureManager chung
    /// </summary>
    public interface IDigitalSignatureManagerBase : IDisposable
    {
        /// <summary>
        /// Các chữ ký trên file
        /// </summary>
        List<ESignature> Signatures
        {
            get;
        }

        /// <summary>
        /// File đã được ký hay chưa?
        /// </summary>
        bool IsSigned
        {
            get;
        }

        /// <summary>
        /// Ký file bằng USB Token
        /// </summary>
        /// <param name="certificate">Certificate ký</param>
        /// <returns></returns>
        void Sign(X509Certificate2 certificate);

        /// <summary>
        /// Ký file bằng HSM
        /// </summary>
        /// <param name="certificate">Certificate ký</param>
        /// <param name="providerHSM">Giao tiếp HSM đã đăng nhập session và load private key. Nếu PrivateKey = null sẽ lấy từ HSM qua Certificate tương ứng</param>
        /// <returns></returns>
        void Sign(X509Certificate2 certificate, HSMServiceProvider providerHSM);

        /// <summary>
        /// Xác thực tất cả các chữ ký - chạy xác thực trên mỗi chữ ký
        /// </summary>
        /// <returns></returns>
        VerifyResult VerifySignatures();

        /// <summary>
        /// Xác thực file có được ký bởi certificate
        /// </summary>
        /// <param name="certificate"></param>
        /// <returns></returns>
        VerifyResult VerifySignatures(X509Certificate2 certificate);

        /// <summary>
        /// Lấy chuỗi Hash value của file
        /// </summary>
        /// <returns></returns>
        byte[] GetHashValue();

        /// <summary>
        /// Xóa một chữ ký trên file
        /// </summary>
        /// <param name="signatureES"></param>
        void RemoveSignature(ESignature signatureES);

        /// <summary>
        /// Xóa tất cả chữ ký trên file
        /// </summary>
        void RemoveAllSignature();

        /// <summary>
        /// Trả về dữ liệu file sau khi ký. Nếu hàm ký bắn exception thì không sử dụng hàm này.
        /// </summary>
        /// <returns></returns>
        byte[] ToArray();
    }
}

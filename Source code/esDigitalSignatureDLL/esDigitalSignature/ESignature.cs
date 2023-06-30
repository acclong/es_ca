using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace esDigitalSignature
{
    /// <summary>
    /// VerifyResult
    /// </summary>
    public enum VerifyResult : int
    {
        /// <summary>
        /// Verification succeeded
        /// </summary>
        [Description("Chữ ký hợp lệ.")]
        Success,               // signature valid

        /// <summary>
        /// Signature was invalid (tampering detected)
        /// </summary>
        [Description("Chữ ký không hợp lệ: Nội dung văn bản đã bị thay đổi.")]
        InvalidSignature,      // hash incorrect

        /// <summary>
        /// Certificate was not embedded in container and caller did not supply one
        /// </summary>
        [Description("Chữ ký không hợp lệ: Không tìm thấy chứng thư đính kèm.")]
        CertificateRequired,   // no certificate is embedded in container - caller must provide one

        /// <summary>
        /// Certificate was invalid (perhaps expired?)
        /// </summary>
        [Description("Chữ ký không hợp lệ: Chứng thư ký không xác thực được.")]
        InvalidCertificate,    // certificate problem - verify does not fully verify cert

        /// <summary>
        /// PackagePart was missing - signature invalid
        /// </summary>
        [Description("Chữ ký không hợp lệ: Cấu trúc chữ ký không đúng.")]
        ReferenceNotFound,     // signature failed because a part is missing

        /// <summary>
        /// Package not signed
        /// </summary>
        [Description("Văn bản chưa được ký.")]
        NotSigned               // no signatures were found
    }

    /// <summary>
    /// Lớp chứa thông tin chữ ký trên file
    /// </summary>
    public class ESignature
    {
        X509Certificate2 _signer;
        DateTime _signingTime;
        VerifyResult _verify;

        /// <summary>
        /// Lấy chứng thư ký
        /// </summary>
        public X509Certificate2 Signer
        {
            get { return _signer; }
        }

        /// <summary>
        /// Thời điểm ký
        /// </summary>
        public DateTime SigningTime
        {
            get { return _signingTime; }
        }

        /// <summary>
        /// Lấy kết quả verify chữ ký (không xác thực chứng thư)
        /// </summary>
        public VerifyResult Verify
        {
            get { return _verify; }
        }

        /// <summary>
        /// Khởi tạo và truyền các giá trị lấy từ file
        /// </summary>
        /// <param name="signer"></param>
        /// <param name="signingTime"></param>
        /// <param name="signatureCreator"></param>
        /// <param name="verify"></param>
        public ESignature(X509Certificate2 signer, DateTime signingTime, VerifyResult verify)
        {
            _signer = signer;
            _signingTime = signingTime;
            _verify = verify;
        }

        /// <summary>
        /// Kiểm tra trạng thái chứng thư số tại thời điểm ký
        /// </summary>
        /// <param name="certificateStatus">Trả về chi tiết trạng thái</param>
        /// <returns></returns>
        public bool ValidateCertificate(out X509ChainStatus certificateStatus)
        {
            return Common.ValidateCertificate(_signer, _signingTime, out certificateStatus);
        }
    }
}

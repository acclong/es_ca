using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace esDigitalSignature.Library
{
    /// <summary>
    /// Kết quả ký file. Dùng cho web service trả về.
    /// </summary>
    public class SignFileResult
    {
        /// <summary>
        /// FileID
        /// </summary>
        public int FileID { get; set; }
        /// <summary>
        /// Kết quả
        /// </summary>
        public string Result { get; set; }
    }

    /// <summary>
    /// Trạng thái chứng thư convert từ X509ChainStatus. Dùng cho web service trả về.
    /// </summary>
    public struct CertificateStatus_CA
    {
        /// <summary>
        /// Convert từ System.Security.Cryptography.X509Certificates.X509ChainStatusFlags
        /// </summary>
        public int Status;
        /// <summary>
        /// Specifies a description of the System.Security.Cryptography.X509Certificates.X509Chain.ChainStatus
        /// </summary>
        public string StatusInformation;
    }

    /// <summary>
    /// Trạng thái chứng thư trong hệ thống Thị trường điện. Dùng cho web service trả về.
    /// </summary>
    public struct CertificateStatus_TTD
    {
        /// <summary>
        /// Convert từ System.Security.Cryptography.X509Certificates.X509ChainStatusFlags
        /// </summary>
        public int Status;
        /// <summary>
        /// Specifies a description of the System.Security.Cryptography.X509Certificates.X509Chain.ChainStatus
        /// </summary>
        public string StatusInformation;
    }

    /// <summary>
    /// Thông tin văn bản từ CSDL. Dùng cho web service trả về.
    /// </summary>
    public struct FileInfoCA
    {
        /// <summary>
        /// ID văn bản trong csdl
        /// </summary>
        public int FileID;
        /// <summary>
        /// Số hiệu văn bản
        /// </summary>
        public string FileNumber;
        /// <summary>
        /// Đường dẫn văn bản
        /// </summary>
        public string FilePath;
        /// <summary>
        /// Mã loại văn bản
        /// </summary>
        public int FileTypeID;
        /// <summary>
        /// Mã đơn vị mà nội dung văn bản nhắm đến
        /// </summary>
        public string MaDV;
        /// <summary>
        /// Ngày đầu kỳ mà nội dung văn bản nhắm đến. Ví dụ: bảng kê tháng thì lấy ngày đầu tháng.
        /// </summary>
        public DateTime FileDate;
        /// <summary>
        /// Trạng thái văn bản. 0: khởi tạo; 1: lưu file; 2: đang ký; 4: đã bị thay thế.
        /// </summary>
        public int Status;
        /////// <summary>
        /////// Công bố hay chưa.
        /////// </summary>
        ////public bool Published;
        /// <summary>
        /// Mô tả
        /// </summary>
        public string Description;
    }

    /// <summary>
    /// Trạng thái chứng thư trong hệ thống Thị trường điện
    /// </summary>
    public struct VCGMChainStatus
    {
        /// <summary>
        /// Trạng thái của chuỗi thông tin chứng thư lấy từ CSDL
        /// </summary>
        public VCGMChainStatusFlags Status;
        /// <summary>
        /// Mô tả chi tiết
        /// </summary>
        public string StatusInformation;
    }
}

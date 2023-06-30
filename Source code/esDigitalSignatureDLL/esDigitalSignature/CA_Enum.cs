using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace esDigitalSignature.Library
{
    /// <summary>
    /// Kiểu đối tượng HSM trong DB
    /// </summary>
    public enum HSMObjectTypeDB
    {
        /// <summary>
        /// 
        /// </summary>
        CERTIFICATE = 1,
        /// <summary>
        /// 
        /// </summary>
        PUBLIC_KEY = 2,
        /// <summary>
        /// 
        /// </summary>
        PRIVATE_KEY = 3,
        /// <summary>
        /// 
        /// </summary>
        CERTIFICATE_REQUEST = 4,
    }

    /// <summary>
    /// Kết quả xác thực chứng thư số trong hệ thống TTĐ
    /// </summary>
    public enum VCGMChainStatusFlags
    {
        /// <summary>
        /// OK
        /// </summary>
        NoError = 0,
        /// <summary>
        /// Không tìm thấy chứng thư
        /// </summary>
        CertificateNotFound = 1,
        /// <summary>
        /// Trạng thái chứng thư không có hiệu lực
        /// </summary>
        CertificateNotValid = 2,
        /// <summary>
        /// Chứng thư chưa được cấp phát
        /// </summary>
        CertificateNotIssue = 3,
        /// <summary>
        /// Chứng thư hết hạn
        /// </summary>
        CertificateExpired = 4,
        //Edited by Toantk on 10/7/2014
        //Bỏ trạng thái liên kết chứng thư - người dùng vì đã liên kết trực tiếp trong bảng User
        ///// <summary>
        ///// Không tìm thấy liên kết chứng thư - người dùng
        ///// </summary>
        //CertUserLinkNotFound = 5,
        ///// <summary>
        ///// Liên kết chứng thư - người dùng chưa được cấp phát
        ///// </summary>
        //CertUserLinkNotIssue = 6,
        ///// <summary>
        ///// Liên kết chứng thư - người dùng hết hạn
        ///// </summary>
        //CertUserLinkExpired = 7,
        /// <summary>
        /// Không tìm thấy người dùng
        /// </summary>
        UserNotFound = 8,
        /// <summary>
        /// Trạng thái người dùng không có hiệu lực
        /// </summary>
        UserNotValid = 9,
        /// <summary>
        /// Người dùng chưa được cấp phát
        /// </summary>
        UserNotIssue = 10,
        /// <summary>
        /// NGười dùng hết hạn
        /// </summary>
        UserExpired = 11,
        /// <summary>
        /// Không tìm thấy liên kết người dùng - chương trình
        /// </summary>
        UserProgLinkNotFound = 12,
        /// <summary>
        /// Liên kết người dùng - chương trình chưa được cấp phát
        /// </summary>
        UserProgNotIssue = 13,
        /// <summary>
        /// Liên kết người dùng - chương trình hết hạn
        /// </summary>
        UserProgExpired = 14,
        /// <summary>
        /// Không thể kiểm tra trạng thái do có lỗi
        /// </summary>
        StatusUnknown = 15,
    }

    /// <summary>
    /// Tên mã hệ thống được tích hợp
    /// </summary>
    public enum ProgName
    {
        /// <summary>
        /// Chào giá server
        /// </summary>
        BiddingServer = 1,
        /// <summary>
        /// Tính toán thanh toán
        /// </summary>
        TTTT_A0 = 3,
        /// <summary>
        /// Trang web nội bộ TTĐ
        /// </summary>
        WebTTD = 6,
        /// <summary>
        /// Trang web smov.vn
        /// </summary>
        SMOV = 7,
        /// <summary>
        /// Phần mềm xử lý số liệu đo đếm
        /// </summary>
        MDMSP = 8,
        /// <summary>
        /// Hệ thống điều độ điện tử DIM
        /// </summary>
        DIM = 9,
    }

    /// <summary>
    /// Loại đơn vị
    /// </summary>
    public enum UnitTypes
    {
        /// <summary>
        /// 
        /// </summary>
        DieuDo = 1,
        /// <summary>
        /// 
        /// </summary>
        MuaBanDien = 2,
        /// <summary>
        /// 
        /// </summary>
        DonViPhatDien = 3,
        /// <summary>
        /// 
        /// </summary>
        NhaMayDien = 4,
        /// <summary>
        /// 
        /// </summary>
        DienLuc = 5,
        /// <summary>
        /// 
        /// </summary>
        TruyenTai = 6,
        /// <summary>
        /// 
        /// </summary>
        GENCO = 7
    }

    /// <summary>
    /// Loại thời gian của văn bản
    /// </summary>
    public enum DateType
    {
        /// <summary>
        /// Ngày
        /// </summary>
        Ngay = 1,
        /// <summary>
        /// Tuần
        /// </summary>
        Tuan = 2,
        /// <summary>
        /// Tháng
        /// </summary>
        Thang = 3,
        /// <summary>
        /// Quý
        /// </summary>
        Quy = 4,
        /// <summary>
        /// Năm
        /// </summary>
        Nam = 5,
    }

    /// <summary>
    /// Loại chữ ký trên văn bản
    /// </summary>
    public enum FileSignatureType
    {
        /// <summary>
        /// NLDC ký lập
        /// </summary>
        NLDCKyLap = 1,
        /// <summary>
        /// NLDC xác nhận
        /// </summary>
        NLDCXacNhan = 2,
        /// <summary>
        /// EPTC xác nhận
        /// </summary>
        EPTCXacNhan = 3,
        /// <summary>
        /// Đơn vị xác nhận
        /// </summary>
        DonViXacNhan = 4
    }

    /// <summary>
    /// Loại văn bản liên quan
    /// </summary>
    public enum FileRelationType
    {
        /// <summary>
        /// Văn bản thay thế (File1 thay thế File2)
        /// </summary>
        VanBanThayThe = 1,
        /// <summary>
        /// Giải quyết kế hoạch năm (File1 giải quyết File2)
        /// </summary>
        GiaiQuyetKHNam = 2,
        /// <summary>
        /// Giải quyết kế hoạch tháng (File1 giải quyết File2)
        /// </summary>
        GiaiQuyetKHThang = 3,
        /// <summary>
        /// Giải quyết Phiếu công tác nguồn (File1 giải quyết File2)
        /// </summary>
        GiaiQuyetPCTNguon = 4,
        /// <summary>
        /// Giải quyết Phiếu thí nghiệm nguồn (File1 giải quyết File2)
        /// </summary>
        GiaiQuyetPTNNguon = 5,
        /// <summary>
        /// Giải quyết Phiếu công tác lưới (File1 giải quyết File2)
        /// </summary>
        GiaiQuyetPCTLuoi = 6,
    }

    /// <summary>
    /// 
    /// </summary>
    public enum FileTypes
    {
        /// <summary>
        /// 
        /// </summary>
        BanChaoNgayToi = 1,
        /// <summary>
        /// 
        /// </summary>
        BanChaoGioToi = 13,
        /// <summary>
        /// 
        /// </summary>
        BanChaoMacDinh = 14,
        /// <summary>
        /// 
        /// </summary>
        BangKeDoanhThuThiTruongNgay = 2,
        /// <summary>
        /// 
        /// </summary>
        BangKeDoanhThuThiTruongThang = 10,
        /// <summary>
        /// 
        /// </summary>
        BangKeChenhLechSanLuongThang = 12,
        /// <summary>
        /// 
        /// </summary>
        XacNhanSuKien = 100,
        /// <summary>
        /// 
        /// </summary>
        SuKienDIM = 101,
        /// <summary>
        /// 
        /// </summary>
        SuKienChung = 102,
        /// <summary>
        /// 
        /// </summary>
        SuKienMucNuocGioiHan = 103,
        /// <summary>
        /// SMOV: Đăng ký kế hoạch năm
        /// </summary>
        KeHoachNam_DangKy = 200,
        /// <summary>
        /// SMOV: Giải quyết kế hoạch năm
        /// </summary>
        KeHoachNam_GiaiQuyet = 201,
        /// <summary>
        /// SMOV: Đăng ký kế hoạch tháng
        /// </summary>
        KeHoachThang_DangKy = 202,
        /// <summary>
        /// SMOV: Giải quyết kế hoạch tháng
        /// </summary>
        KeHoachThang_GiaiQuyet = 203,
        /// <summary>
        /// SMOV: Phiếu đăng ký công tác nguồn
        /// </summary>
        PhieuCongTacNguon_DangKy = 204,
        /// <summary>
        /// SMOV: Phiếu giải quyết công tác nguồn
        /// </summary>
        PhieuCongTacNguon_GiaiQuyet = 205,
        /// <summary>
        /// SMOV: Phiếu đăng ký thí nghiệm nguồn
        /// </summary>
        PhieuThiNghiemNguon_DangKy = 206,
        /// <summary>
        /// SMOV: Phiếu giải quyết thí nghiệm nguồn
        /// </summary>
        PhieuThiNghiemNguon_GiaiQuyet = 207,
        /// <summary>
        /// SMOV: Phiếu đăng ký công tác lưới
        /// </summary>
        PhieuCongTacLuoi_DangKy = 208,
        /// <summary>
        /// SMOV: Phiếu giải quyết công tác lưới
        /// </summary>
        PhieuCongTacLuoi_GiaiQuyet = 209,
    }

    /// <summary>
    /// 
    /// </summary>
    public enum FileStatus
    {
        /// <summary>
        /// Khởi tạo bản ghi
        /// </summary>
        KhoiTao = 0,
        /// <summary>
        /// Đã lưu file lên server
        /// </summary>
        LuuFile = 1,
        /// <summary>
        /// Đang trong quá trình ký file
        /// </summary>
        QuaTrinhKy = 2,
        /// <summary>
        /// Đã bị thay thế bởi một file khác
        /// </summary>
        DaBiThayThe = 4,
    }

    /// <summary>
    /// 
    /// </summary>
    public enum FileSignActions : int
    {
        /// <summary>
        /// sign
        /// </summary>
        AddSignature = 1,
        /// <summary>
        /// Remove sinature
        /// </summary>
        RemoveSignature = 2,
    }

    /// <summary>
    /// 
    /// </summary>
    public enum FileSignResults : int
    {
        /// <summary>
        /// Không tìm thấy bản ghi file
        /// </summary>
        FileNotFound = 1,
        /// <summary>
        /// File chưa được lưu hoặc không có hash data
        /// </summary>
        FileNotSaved = 2,
        /// <summary>
        /// File đã bị thay thế
        /// </summary>
        FileReplaced = 3,
        /// <summary>
        /// File đã được ký bởi cùng đơn vị và loại chữ ký
        /// </summary>
        FileSignedByUnit = 4,
        /// <summary>
        /// File đang trong quá trình ký bởi người khác
        /// </summary>
        FileInSignProgress = 5,
        /// <summary>
        /// Chuỗi Hash không khớp
        /// </summary>
        HashNotMatch = 6,
        /// <summary>
        /// Chữ ký không hợp lệ hoặc nội dung file đã bị chỉnh sửa
        /// </summary>
        InvalidSignature = 7,
        /// <summary>
        /// Chứng thư số không hợp lệ với CA
        /// </summary>
        InvalidCertificate_CA = 8,
        /// <summary>
        /// Chứng thư số không hợp lệ với TTD
        /// </summary>
        InvalidCertificate_TTD = 9,
        /// <summary>
        /// Hết thời gian chờ lưu file
        /// </summary>
        SaveTimeOut = 10,
        /// <summary>
        /// Không có chữ ký
        /// </summary>
        NotSigned = 11,
        /// <summary>
        /// Người dùng không có quyền ký
        /// </summary>
        UserNotAllow = 12,
        /// <summary>
        /// Loại chữ ký đã tồn tại
        /// </summary>
        HasSignType = 13,
        /// <summary>
        /// Phiên ký có các loại file khác nhau
        /// </summary>
        DifferentFileExtensions = 14,
        /// <summary>
        /// Quá trình ký file thất bại
        /// </summary>
        FileSignFailed = 98,
        /// <summary>
        /// Ký file thành công
        /// </summary>
        Success = 99,
    }

    /// <summary>
    /// 
    /// </summary>
    public enum CACommand : int
    {
        /// <summary>
        /// Không thực hiện
        /// </summary>
        None = 0,
        /// <summary>
        /// Ký 01 file trên server
        /// </summary>
        SignFileByID = 1,
        /// <summary>
        /// Ký nhiều file trên server, tuần tự
        /// </summary>
        SignFilesByIDs = 2,
        /// <summary>
        /// Kiểm tra đăng nhập slot HSM
        /// </summary>
        CheckLoginHSM = 3,
        /// <summary>
        /// Ký nhiều file trên server, song song
        /// </summary>
        SignFilesByID_Thread = 4,
        /// <summary>
        /// Ký nhiều file và ghi log ra file text
        /// </summary>
        SignFilesByID_LogFile = 5,
        /// <summary>
        /// Đổi PIN slot
        /// </summary>
        ChangePIN = 6,
    }

    /// <summary>
    /// Chi tiết mã lỗi ra đây
    /// </summary>
    public enum CAExitCode : int
    {
        /// <summary>
        /// Giá trị khởi tạo
        /// </summary>
        None = -1,
        /// <summary>
        /// Thành công
        /// </summary>
        Success = 0,
        /// <summary>
        /// Lỗi
        /// </summary>
        Error = 100,
        /// <summary>
        /// Lệnh không tồn tại
        /// </summary>
        UnrecognizedCommand = 1,
        /// <summary>
        /// Tham số không đúng
        /// </summary>
        BadArgument = 2,
        /// <summary>
        /// Không tìm thấy file
        /// </summary>
        WS_FileNotFound = 11,
        /// <summary>
        /// File chưa được lưu
        /// </summary>
        WS_FileNotSaved = 12,
        /// <summary>
        /// Không tìm thấy chứng thư ký
        /// </summary>
        WS_CertificateNotFoundOrInvalid = 13,
        /// <summary>
        /// Chứng thư ký nhiều hơn MỘT
        /// </summary>
        WS_CertificateMoreThanOne = 14,
        /// <summary>
        /// Không tìm thấy Private Key
        /// </summary>
        WS_PrivateKeyNotFound = 15,
        /// <summary>
        /// Private Key nhiều hơn MỘT
        /// </summary>
        WS_PrivateKeyMoreThanOne = 16,
        /// <summary>
        /// File đang trong quá trình ký bởi người khác
        /// </summary>
        WS_FileInSign = 17,
        /// <summary>
        /// File đã bị thay thế
        /// </summary>
        WS_FileReplaced = 18,
        /// <summary>
        /// Chứng thư số không hợp lệ với nhà cung cấp CA
        /// </summary>
        WS_CertificateInvalidInCA = 19,
        /// <summary>
        /// Hết thời gian lưu file
        /// </summary>
        WS_SaveFileTimeOut = 20,
        /// <summary>
        /// Hết thời gian timeout
        /// </summary>
        WS_Timeout = 21,
        /// <summary>
        /// App Service trả về kết quả output cho từng file sai định dạng
        /// </summary>
        WS_BadFormatOuputConsole = 22,
        /// <summary>
        /// Không tìm thấy slot của người dùng trong csdl
        /// </summary>
        WS_UserSlotNotFound = 23,
        /// <summary>
        /// Sai mã PIN khi đăng nhập HSM
        /// </summary>
        HSM_PinIncorrect = 51,
        /// <summary>
        /// Đăng nhập HSM không thành công
        /// </summary>
        HSM_LoginFailed = 52,
        /// <summary>
        /// Key trong HSM bị trùng
        /// </summary>
        HSM_DuplicateKey = 53,
        /// <summary>
        /// Loại Key trong HSM không được hỗ trợ
        /// </summary>
        HSM_KeyTypeNotSupported = 54,
        /// <summary>
        /// Không tìm thấy Key trong HSM
        /// </summary>
        HSM_KeyNotFound = 55,
        /// <summary>
        /// PIN bị khóa do nhập sai 3 lần liên tiếp
        /// </summary>
        HSM_PinLocked = 56,
        /// <summary>
        /// PIN hết hiệu lực
        /// </summary>
        HSM_PinExpired = 57,
        /// <summary>
        /// Độ dài PIN không đúng
        /// </summary>
        HSM_PinLenRange = 58,
    }
}

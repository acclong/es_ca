using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Security.Cryptography.X509Certificates;
using System.IO;
using System.Data;
using System.Configuration;
using System.Diagnostics;
using System.Text;
using ES.CA_WebServiceDAL;
using ES.CA_WebServiceBUS;
using esDigitalSignature;
using esDigitalSignature.Library;
using System.Security.Principal;
using System.Threading;

namespace ES.CA_WebService
{
    /// <summary>
    /// Summary description for CAService
    /// </summary>
    [WebService(Namespace = "http://ca-nldc.vn/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.None)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class CAService : System.Web.Services.WebService
    {

        #region Chứng thư số và xác thực
        [WebMethod(Description = "Kiểm tra xem có cần check CA với đơn vị hay không.")]
        public bool CheckUnitInCASystem_Now(int UnitTypeID, string maDV, string programName)
        {
            //CSDL
            DAL_SqlConnector.ConnectionString = GetDatabaseString();
            BUSQuanTri bus = new BUSQuanTri();

            DataTable dt = bus.CA_UnitProgram_SelectByMaDV_ProgName(UnitTypeID, maDV, programName);
            if (dt.Rows.Count > 0)
                return true;
            else
                return false;
        }

        [WebMethod(Description = "Kiểm tra xem có cần check CA với các đơn vị hay không.")]
        public bool CheckUnitInCASystem_Now_NhieuDV(int UnitTypeID, List<string> lstMaDV, string programName)
        {
            //CSDL
            DAL_SqlConnector.ConnectionString = GetDatabaseString();
            BUSQuanTri bus = new BUSQuanTri();

            DataTable dt = bus.CA_UnitProgram_SelectByMaDV_ProgName(UnitTypeID, lstMaDV, programName);
            if (dt.Rows.Count > 0)
                return true;
            else
                return false;
        }

        [WebMethod(Description = "Trả về danh sách đơn vị. Trường [IsCheckCA] để kiểm tra xem có cần check CA với đơn vị hay không.")]
        public DataTable CheckUnitMULTI_InCASystem_Now(int UnitTypeID, string arrMaDV, string programName)
        {
            //CSDL
            DAL_SqlConnector.ConnectionString = GetDatabaseString();
            BUSQuanTri bus = new BUSQuanTri();

            return bus.CA_UnitProgram_SelectBy_ArrayMaDV_Prog(UnitTypeID, arrMaDV, programName);
        }

        [WebMethod(Description = "Lấy danh sách chứng thư hợp lệ của người dùng trong hệ thống. Trả về mảng RawData của các chứng thư.")]
        public List<byte[]> GetCertificateFromProgUser_Now(string programName, string userName)
        {
            //CSDL
            DAL_SqlConnector.ConnectionString = GetDatabaseString();
            BUSQuanTri bus = new BUSQuanTri();

            List<byte[]> arrRawData = new List<byte[]>();

            //Lấy thông tin certificate
            DataTable dtCert = bus.CA_Certificate_SelectForValid(programName, userName);
            foreach (DataRow dr in dtCert.Rows)
            {
                arrRawData.Add((byte[])dr["RawData"]);
            }

            return arrRawData;
        }

        [WebMethod(Description = "Lấy danh sách chứng thư hợp lệ của người dùng trong hệ thống. Trả về mảng Hex của các chứng thư.")]
        public List<string> GetCertificateFromProgUser_Now_Hex(string programName, string userName)
        {
            //CSDL
            DAL_SqlConnector.ConnectionString = GetDatabaseString();
            BUSQuanTri bus = new BUSQuanTri();

            List<string> arrHex = new List<string>();

            //Lấy thông tin certificate
            DataTable dtCert = bus.CA_Certificate_SelectForValid(programName, userName);
            foreach (DataRow dr in dtCert.Rows)
            {
                byte[] rawData = (byte[])dr["RawData"];
                arrHex.Add(Common.ConvertBytesToHex(rawData));
            }

            return arrHex;
        }

        [WebMethod(Description = "Lấy thông tin chứng thư số của User. Chỉ áp dụng cho user thuộc A0.")]
        public DataTable GetUserCerticate(string programName, string userName)
        {
            try
            {
                //CSDL
                DAL_SqlConnector.ConnectionString = GetDatabaseString();
                BUSQuanTri bus = new BUSQuanTri();

                //Lấy thông tin certificate
                DataTable dtCert = bus.CA_Certificate_SelectForSign(programName, userName);
                return dtCert;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [WebMethod(Description = "Xác thực chứng thư với nhà cung cấp CA ở thời điểm hiện tại. Trả về CertificateStatus để convert thành X509ChainStatus")]
        public bool ValidateCertificateInCA_Now(byte[] certRawData, out CertificateStatus_CA certificateStatus)
        {
            certificateStatus = new CertificateStatus_CA();
            X509ChainStatus chainStatus = new X509ChainStatus();
            bool ret = true;

            //Toantk: tạm thời bỏ do máy 100 không check được
            //X509Certificate2 cert = new X509Certificate2(certRawData);
            //ret = Common.ValidateCertificate(cert, DateTime.Now, out chainStatus);
            certificateStatus.Status = (int)chainStatus.Status;
            certificateStatus.StatusInformation = chainStatus.StatusInformation;

            return ret;
        }

        [WebMethod(Description = "Xác thực chứng thư với nhà cung cấp CA ở thời điểm quá khứ. Trả về CertificateStatus để convert thành X509ChainStatus.")]
        public bool ValidateCertificateInCA_Date(byte[] certRawData, DateTime validateTime, out CertificateStatus_CA certificateStatus)
        {
            certificateStatus = new CertificateStatus_CA();
            X509ChainStatus chainStatus;
            bool ret;

            X509Certificate2 cert = new X509Certificate2(certRawData);
            ret = Common.ValidateCertificate(cert, validateTime, out chainStatus);
            certificateStatus.Status = (int)chainStatus.Status;
            certificateStatus.StatusInformation = chainStatus.StatusInformation;

            return ret;
        }

        [WebMethod(Description = "Xác thực chứng thư với Thị trường điện ở thời điểm hiện tại")]
        public bool ValidateCertificateInTTD_Now(string programName, string userName, byte[] certRawData, out CertificateStatus_TTD certificateStatus)
        {
            certificateStatus = new CertificateStatus_TTD();
            VCGMChainStatus chainStatus;
            bool ret;
            //Khởi tạo + CSDL
            DAL_SqlConnector.ConnectionString = GetDatabaseString();
            BUSQuanTri bus = new BUSQuanTri();

            //Lấy chuỗi thông tin chứng thư
            X509Certificate2 cert = new X509Certificate2(certRawData);
            DataTable dtChain = bus.CA_Certificate_SelectChainByCertProg(programName, userName, cert.SerialNumber);
            //Xác thực chuỗi
            ret = ValidateCertificateInVCGM(dtChain, DateTime.Now, out chainStatus);
            certificateStatus.Status = (int)chainStatus.Status;
            certificateStatus.StatusInformation = chainStatus.StatusInformation;

            return ret;
        }


        [WebMethod(Description = "Xác thực chứng thư với Thị trường điện ở thời điểm hiện tại")]
        public bool ValidateCertificateInTTD_Now(string programName, string MaDV, int UnitTypeId, byte[] certRawData, out CertificateStatus_TTD certificateStatus, out string userName)
        {
            certificateStatus = new CertificateStatus_TTD();
            VCGMChainStatus chainStatus;
            bool ret;
            //Khởi tạo + CSDL
            DAL_SqlConnector.ConnectionString = GetDatabaseString();
            BUSQuanTri bus = new BUSQuanTri();

            //Lấy chuỗi thông tin chứng thư
            X509Certificate2 cert = new X509Certificate2(certRawData);
            DataTable dtChain = bus.CA_Certificate_SelectChainByCertProgUnit(programName, cert.SerialNumber, MaDV, UnitTypeId);
            //Xác thực chuỗi
            ret = ValidateCertificateInVCGM(dtChain, DateTime.Now, out chainStatus);
            certificateStatus.Status = (int)chainStatus.Status;
            certificateStatus.StatusInformation = chainStatus.StatusInformation;
            userName = dtChain.Rows[0]["UserProgName"].ToString();
            return ret;
        }

        //[WebMethod]
        //public int ValidateCertificateInCA(string programName, string userName, DateTime validateTime)
        //{
        //    int flag = 0;
        //    return flag;
        //}

        //[WebMethod]
        //public int ValidateCertificateInTTD(string programName, string userName, DateTime validateTime)
        //{
        //    int flag = 0;
        //    return flag;
        //}
        #endregion

        #region HSM

        [WebMethod(Description = "Kiểm tra có đăng nhập được HSM hay không.")]
        public int CheckLoginHSM(string programName, string userName, string password, ref string strError)
        {
            try
            {
                //CSDL
                DAL_SqlConnector.ConnectionString = GetDatabaseString();
                BUSQuanTri bus = new BUSQuanTri();

                //Lấy thông tin certificate
                DataTable dtCert = bus.CA_Certificate_SelectForSign(programName, userName);
                X509Certificate2 cert = new X509Certificate2((byte[])dtCert.Rows[0]["RawData"]);
                int certID = Convert.ToInt32(dtCert.Rows[0]["CertID"]);

                ////Xác thực chứng thư với nhà cung cấp CA
                //X509ChainStatus certificateStatus;
                //if (!Common.ValidateCertificate(cert, DateTime.Now, out certificateStatus))
                //    throw new Exception("WS_Kiểm tra chứng thư số với nhà cung cấp CA không hợp lệ.");

                //Lấy key trong HSM
                //LƯU Ý: dùng hàm HSM_WLDObject_SelectPrivateKeyByCertID nếu chạy ở chế độ WLD
                DataTable dtPK = bus.HSM_Object_SelectPrivateKeyByCertID(Convert.ToInt32(dtCert.Rows[0]["CertID"]));
                string slotSerial = dtPK.Rows[0]["SlotSerial"].ToString();
                byte[] keyID = (byte[])dtPK.Rows[0]["CKA_ID"];

                //Khởi tạo giao tiếp HSM và đăng nhập
                using (HSMServiceProvider provider = new HSMServiceProvider())
                {
                    // Đăng nhập và load PrivateKey
                    HSMReturnValue loginResult = provider.Login(slotSerial, HSMLoginRole.User, password);
                    switch (loginResult)
                    {
                        case HSMReturnValue.OK:
                            break;
                        case HSMReturnValue.PIN_LEN_RANGE:
                            {
                                strError = "WS_Đăng nhập HSM không thành công: Độ dài mã PIN không đúng.";
                                return (int)CAExitCode.HSM_PinLenRange;
                            }
                        case HSMReturnValue.PIN_INCORRECT:
                            {
                                strError = "WS_Đăng nhập HSM không thành công: Sai mã PIN.";
                                return (int)CAExitCode.HSM_PinIncorrect;
                            }
                        case HSMReturnValue.PIN_LOCKED:
                            {
                                strError = "WS_Đăng nhập HSM không thành công: Mã PIN đã bị khóa.";
                                return (int)CAExitCode.HSM_PinLocked;
                            }
                        default:
                            {
                                strError = "WS_Đăng nhập HSM không thành công: " + loginResult.ToString() + ".";
                                return (int)CAExitCode.HSM_LoginFailed;
                            }
                    }
                    provider.LoadPrivateKeyByID(keyID);
                }

                strError = "Thành công.";
                return (int)CAExitCode.Success;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return (int)CAExitCode.Error;
            }
        }

        [WebMethod(Description = "Kiểm tra có đăng nhập được HSM hay không. Trả về thông tin HSM.")]
        public int CheckLoginHSM_ReturnInfo(string programName, string userName, string password, ref string slotSerial, ref byte[] keyID, ref string strError)
        {
            try
            {
                //CSDL
                DAL_SqlConnector.ConnectionString = GetDatabaseString();
                BUSQuanTri bus = new BUSQuanTri();

                //Lấy thông tin certificate
                DataTable dtCert = bus.CA_Certificate_SelectForSign(programName, userName);
                X509Certificate2 cert = new X509Certificate2((byte[])dtCert.Rows[0]["RawData"]);
                int certID = Convert.ToInt32(dtCert.Rows[0]["CertID"]);

                ////Xác thực chứng thư với nhà cung cấp CA
                //X509ChainStatus certificateStatus;
                //if (!Common.ValidateCertificate(cert, DateTime.Now, out certificateStatus))
                //    throw new Exception("WS_Kiểm tra chứng thư số với nhà cung cấp CA không hợp lệ.");

                //Lấy key trong HSM
                //LƯU Ý: dùng hàm HSM_WLDObject_SelectPrivateKeyByCertID nếu chạy ở chế độ WLD
                DataTable dtPK = bus.HSM_Object_SelectPrivateKeyByCertID(Convert.ToInt32(dtCert.Rows[0]["CertID"]));
                slotSerial = dtPK.Rows[0]["SlotSerial"].ToString();
                keyID = (byte[])dtPK.Rows[0]["CKA_ID"];

                //Khởi tạo giao tiếp HSM và đăng nhập
                using (HSMServiceProvider provider = new HSMServiceProvider())
                {
                    // Đăng nhập và load PrivateKey
                    HSMReturnValue loginResult = provider.Login(slotSerial, HSMLoginRole.User, password);
                    switch (loginResult)
                    {
                        case HSMReturnValue.OK:
                            break;
                        case HSMReturnValue.PIN_LEN_RANGE:
                            {
                                strError = "WS_Đăng nhập HSM không thành công: Độ dài mã PIN không đúng.";
                                return (int)CAExitCode.HSM_PinLenRange;
                            }
                        case HSMReturnValue.PIN_INCORRECT:
                            {
                                strError = "WS_Đăng nhập HSM không thành công: Sai mã PIN.";
                                return (int)CAExitCode.HSM_PinIncorrect;
                            }
                        case HSMReturnValue.PIN_LOCKED:
                            {
                                strError = "WS_Đăng nhập HSM không thành công: Mã PIN đã bị khóa.";
                                return (int)CAExitCode.HSM_PinLocked;
                            }
                        default:
                            {
                                strError = "WS_Đăng nhập HSM không thành công: " + loginResult.ToString() + ".";
                                return (int)CAExitCode.HSM_LoginFailed;
                            }
                    }
                    provider.LoadPrivateKeyByID(keyID);
                }

                strError = "Thành công.";
                return (int)CAExitCode.Success;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return (int)CAExitCode.Error;
            }
        }

        [WebMethod(Description = "Đổi mã PIN HSM của người dùng A0")]
        public int ChangePassSlot(string programName, string userName, string oldPIN, string newPIN)
        {
            //Results
            int ret;

            // Use ProcessStartInfo class
            ProcessStartInfo processInfo = new ProcessStartInfo();
            processInfo.CreateNoWindow = false;
            processInfo.UseShellExecute = false;
            processInfo.FileName = Server.MapPath(@"~\CA_App\CA_AppService.exe");
            processInfo.WindowStyle = ProcessWindowStyle.Hidden;
            processInfo.Arguments = "-ac" + ((int)CACommand.ChangePIN).ToString() + " -pn" + programName + " -up" + userName + " -pw" + oldPIN + " -nw" + newPIN;

            // Start the process with the info we specified.
            // Call WaitForExit and then the using statement will close.
            using (Process process = Process.Start(processInfo))
            {
                process.WaitForExit();
                ret = process.ExitCode;
            }

            return ret;
        }

        [WebMethod(Description = "Ký chuỗi dữ liệu bằng HSM.")]
        public byte[] SignHSM(string slotSerial, byte[] keyID, string password, byte[] message, ref string strError)
        {
            try
            {
                //Khởi tạo giao tiếp HSM và đăng nhập
                using (HSMServiceProvider provider = new HSMServiceProvider())
                {
                    // Đăng nhập và 
                    HSMReturnValue loginResult = provider.Login(slotSerial, HSMLoginRole.User, password);
                    if (loginResult != HSMReturnValue.OK)
                    {
                        strError = "WS_Đăng nhập HSM không thành công: " + loginResult.ToString() + ".";
                        return null;
                    }
                    //load PrivateKey và ký
                    provider.LoadPrivateKeyByID(keyID);
                    byte[] output = Common.Sign(message, provider);

                    strError = "Thành công.";
                    return output;
                }
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return null;
            }
        }

        [WebMethod(Description = "Giải mã chuỗi dữ liệu bằng HSM.")]
        public byte[] DecryptHSM(string slotSerial, byte[] keyID, string password, byte[] message, ref string strError)
        {
            try
            {
                //Khởi tạo giao tiếp HSM và đăng nhập
                using (HSMServiceProvider provider = new HSMServiceProvider())
                {
                    // Đăng nhập và 
                    HSMReturnValue loginResult = provider.Login(slotSerial, HSMLoginRole.User, password);
                    if (loginResult != HSMReturnValue.OK)
                    {
                        strError = "WS_Đăng nhập HSM không thành công: " + loginResult.ToString() + ".";
                        return null;
                    }
                    //load PrivateKey và ký
                    provider.LoadPrivateKeyByID(keyID);

                    byte[] output = Common.Decrypt(message, provider);

                    strError = "Thành công.";
                    return output;
                }
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return null;
            }
        }
        #endregion

        #region Xử lý file văn bản
        /// <summary>
        /// Khởi tạo văn bản với số văn bản và đường dẫn văn bản
        /// </summary>
        /// <param name="programName">Tên mã chương trình tạo file</param>
        /// <param name="userName">Tên người dùng trong chương trình tạo file</param>
        /// <param name="FileTypeID">Loại file</param>
        /// <param name="FileMaDV">Mã đơn vị trong nội dung file</param>
        /// <param name="FileDate">Ngày trong nội dung file</param>
        /// <returns>Thông tin file mới khởi tạo</returns>
        [WebMethod(Description = "Khởi tạo văn bản cùng với đánh số văn bản và tạo đường dẫn văn bản. Ghi log thay đổi trạng thái.")]
        public FileInfoCA? CreateAndGetFileInfo(string programName, string userName, int FileTypeID, string FileMaDV, DateTime FileDate,
            string FileName, string Description, ref string strError)
        {
            try
            {
                // Khởi tạo
                FileInfoCA fi = new FileInfoCA();
                DAL_SqlConnector.ConnectionString = GetDatabaseString();
                BUSQuanTri bus = new BUSQuanTri();

                // Insert và select thông tin của file vừa insert + ghi log
                DataTable dt = bus.FL_File_InsertNewFile(programName, userName, FileTypeID, FileMaDV, FileDate, FileName, Description);

                // lấy thông tin vào fi
                fi.FileID = Convert.ToInt32(dt.Rows[0]["FileID"]);
                fi.FileNumber = dt.Rows[0]["FileNumber"].ToString();
                fi.FilePath = dt.Rows[0]["FilePath"].ToString();
                fi.FileTypeID = Convert.ToInt32(dt.Rows[0]["FileTypeID"]);
                fi.FileDate = Convert.ToDateTime(dt.Rows[0]["FileDate"]);
                fi.MaDV = dt.Rows[0]["MaDV"].ToString();
                fi.Status = Convert.ToInt32(dt.Rows[0]["Status"]);
                //fi.Published = Convert.ToBoolean(dt.Rows[0]["Published"]);
                fi.Description = dt.Rows[0]["Description"].ToString();

                //Tạo folder nếu chưa có
                //System.IO.FileInfo file = new System.IO.FileInfo(fi.FilePath);
                string dir = Path.GetDirectoryName(fi.FilePath);
                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);

                strError = "Success";
                return fi;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return null;
            }
        }

        [WebMethod(Description = "Lấy thông tin file theo FileID")]
        public FileInfoCA? GetFileInfo(int fileID, ref string strError)
        {
            try
            {
                // Khởi tạo
                FileInfoCA fi = new FileInfoCA();
                DAL_SqlConnector.ConnectionString = GetDatabaseString();
                BUSQuanTri bus = new BUSQuanTri();

                // Insert và select thông tin của file vừa insert
                DataTable dt = bus.FL_File_SelectByFileID(fileID);
                if (dt.Rows.Count < 1)
                {
                    strError = "Không tìm thấy file.";
                    return null;
                }

                // lấy thông tin vào fi
                fi.FileID = Convert.ToInt32(dt.Rows[0]["FileID"]);
                fi.FileNumber = dt.Rows[0]["FileNumber"].ToString();
                fi.FilePath = dt.Rows[0]["FilePath"].ToString();
                fi.FileTypeID = Convert.ToInt32(dt.Rows[0]["FileTypeID"]);
                fi.FileDate = Convert.ToDateTime(dt.Rows[0]["FileDate"]);
                fi.MaDV = dt.Rows[0]["MaDV"].ToString();
                fi.Status = Convert.ToInt32(dt.Rows[0]["Status"]);
                //fi.Published = Convert.ToBoolean(dt.Rows[0]["Published"]);
                fi.Description = dt.Rows[0]["Description"].ToString();

                return fi;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return null;
            }
        }

        [WebMethod(Description = "Thiết lập trạng thái với chuỗi HASH sau khi lưu file và ghi log thay đổi trạng thái.")]
        public bool SaveFile_WithHash(int fileID, byte[] fileData, string programName, string userName, ref string strError)
        {
            try
            {
                DAL_SqlConnector.ConnectionString = GetDatabaseString();
                BUSQuanTri bus = new BUSQuanTri();

                //Lấy filePath
                DataTable dt = bus.FL_File_SelectByFileID(fileID);
                if (dt.Rows.Count < 1)
                {
                    strError = "WS_Không tìm thấy file.";
                    return false;
                }
                string filePath = dt.Rows[0]["FilePath"].ToString();
                string fileExtension = Path.GetExtension(filePath);

                //Lưu file
                File.WriteAllBytes(filePath, fileData);

                // ghi status + ghi log
                using (ESDigitalSignatureManager dsm = new ESDigitalSignatureManager(fileData, fileExtension))
                {
                    bus.FL_File_UpdateStatus_WithHash(fileID, (int)FileStatus.LuuFile, dsm.GetHashValue(), "Hoàn thành lưu file", programName, userName);
                }

                return true;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return false;
            }
        }

        [WebMethod(Description = "Thiết lập trạng thái cho file và ghi log thay đổi trạng thái.")]
        public bool SetFileStatus(int fileID, int fileStatus, string reason, string programName, string userName)
        {
            DAL_SqlConnector.ConnectionString = GetDatabaseString();
            BUSQuanTri bus = new BUSQuanTri();
            // ghi status + ghi log
            bus.FL_File_UpdateStatus(fileID, fileStatus, reason, programName, userName);
            return true;
        }

        [WebMethod(Description = "Ghi log ký/xóa chữ ký trên file.")]
        public bool LogFileSignature(int fileID, byte[] certRawData, DateTime signTime, int verify, int fileSignActions,
            string programName, string userName)
        {
            try
            {
                DAL_SqlConnector.ConnectionString = GetDatabaseString();
                BUSQuanTri bus = new BUSQuanTri();
                // ghi log
                X509Certificate2 cert = new X509Certificate2(certRawData);
                bus.FL_LogFileSignature_Insert(fileID, cert.SerialNumber, signTime, verify, fileSignActions, programName, userName);
                return true;
            }
            catch
            {
                return false;
            }
        }

        [WebMethod(Description = "Lấy file mới nhất từ ngày đến ngày có status là 1, 2 theo Ma_NM.")]
        public DataTable GetFL_File_SelectFileID(DateTime TuNgay, DateTime DenNgay, string Ma_NM, int FileTypeID)
        {
            try
            {
                //CSDL
                DAL_SqlConnector.ConnectionString = GetDatabaseString();
                BUSQuanTri bus = new BUSQuanTri();

                //Lấy thông tin certificate
                //Edited by Toantk on 22/6/2015
                //Đổi tên store và bỏ biến UnitTypeID thay bằng lấy theo FileTypeID
                DataTable dtCert = bus.FL_File_SelectFileID_ByNgayMaNMType(TuNgay, DenNgay, Ma_NM, FileTypeID);
                return dtCert;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [WebMethod(Description = "Cập nhật văn bản liên quan. Cập nhật trạng thái FileID_B nếu FileRelationTypeID = 1 (VanBanThayThe).")]
        public bool FL_FileRelation_Insert(int FileID_A, int FileID_B, int FileRelationTypeID, string programName, string userName)
        {
            DAL_SqlConnector.ConnectionString = GetDatabaseString();
            BUSQuanTri bus = new BUSQuanTri();
            // ghi status + ghi log
            bus.FL_FileRelation_Insert(FileID_A, FileID_B, FileRelationTypeID, programName, userName);
            return true;
        }

        [WebMethod(Description = "Kiểm tra chữ ký, khởi tạo và lưu văn bản có sẵn vào hệ thống.")]
        public FileInfoCA? CreateAndSaveFile(string programName, string userName, int FileTypeID, string FileMaDV, DateTime FileDate,
            string FileName, byte[] fileData, string Description, ref string strError)
        {
            try
            {
                //Khởi tạo
                DAL_SqlConnector.ConnectionString = GetDatabaseString();
                BUSQuanTri bus = new BUSQuanTri();
                ESignature signature;
                FileInfoCA fi;
                int signType = -1;

                #region Kiểm tra chữ ký
                using (ESDigitalSignatureManager dsm = new ESDigitalSignatureManager(fileData, Path.GetExtension(FileName)))
                {
                    //Lấy chữ ký cuối cùng
                    if (dsm.Signatures.Count < 1)
                    {
                        strError = "Không tìm thấy chữ ký trên file.";
                        return null;
                    }
                    signature = dsm.Signatures[dsm.Signatures.Count - 1];

                    //Kiểm tra tính toàn vẹn chữ ký
                    if (signature.Verify != VerifyResult.Success)
                    {
                        strError = "Chữ ký không hợp lệ: " + signature.Verify.ToString();
                        return null;
                    }

                    //Kiểm tra hiệu lực chứng thư với CA
                    CertificateStatus_CA statusCA;
                    if (!this.ValidateCertificateInCA_Now(signature.Signer.RawData, out statusCA))
                    {
                        strError = "Chứng thư không được xác thực bởi nhà cung cấp CA.";
                        return null;
                    }
                    //Kiểm tra hiệu lực chứng thư trong TTĐ
                    CertificateStatus_TTD statusTTD;
                    if (!this.ValidateCertificateInTTD_Now(programName, userName, signature.Signer.RawData, out statusTTD))
                    {
                        strError = "Chứng thư không được xác thực bởi hệ thống chữ ký số TTD: " + statusTTD.StatusInformation.ToString();
                        return null;
                    }
                }
                #endregion

                #region Khởi tạo văn bản, lưu file và chữ ký
                //Khởi tạo
                DataTable dt = bus.FL_File_InsertNewFile(programName, userName, FileTypeID, FileMaDV, FileDate, FileName, Description);
                if (dt.Rows.Count < 1)
                    return null;
                else
                {
                    fi = new FileInfoCA();
                    fi.FileID = Convert.ToInt32(dt.Rows[0]["FileID"]);
                    fi.FileNumber = dt.Rows[0]["FileNumber"].ToString();
                    fi.FilePath = dt.Rows[0]["FilePath"].ToString();
                    fi.FileTypeID = Convert.ToInt32(dt.Rows[0]["FileTypeID"]);
                    fi.FileDate = Convert.ToDateTime(dt.Rows[0]["FileDate"]);
                    fi.MaDV = dt.Rows[0]["MaDV"].ToString();
                    fi.Status = Convert.ToInt32(dt.Rows[0]["Status"]);
                    fi.Description = dt.Rows[0]["Description"].ToString();

                    signType = Convert.ToInt32(dt.Rows[0]["QuyenTaoFile"]);
                }
                //Tạo folder nếu chưa có
                string dir = Path.GetDirectoryName(fi.FilePath);
                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);
                //Lưu file
                if (!this.SaveFile_WithHash(fi.FileID, fileData, programName, userName, ref strError))
                    return null;
                //Ghi log ký
                bus.FL_File_UpdateForLogSign(fi.FileID, signature.Signer.SerialNumber, signature.SigningTime, (int)signature.Verify,
                    signType, (int)FileSignActions.AddSignature, fi.FilePath, "Hoàn thành lưu file có chữ ký", programName, userName);
                #endregion

                return fi;
            }
            catch (Exception ex)
            {
                strError = "Lỗi kiểm tra và lưu văn bản: " + ex.Message;
                return null;
            }
        }

        [WebMethod(Description = "Kiểm tra chữ ký, khởi tạo và lưu văn bản có sẵn vào hệ thống.")]
        public FileInfoCA? CreateAndSaveFileForBidding(string programName, int FileTypeID, string FileMaDV, DateTime FileDate,
            string FileName, byte[] fileData, string Description, ref string strError)
        {
            try
            {
                //Khởi tạo
                DAL_SqlConnector.ConnectionString = GetDatabaseString();
                BUSQuanTri bus = new BUSQuanTri();
                ESignature signature;
                FileInfoCA fi;
                int signType = -1;
                string userName = "";

                #region Kiểm tra chữ ký
                using (ESDigitalSignatureManager dsm = new ESDigitalSignatureManager(fileData, Path.GetExtension(FileName)))
                {
                    //Lấy chữ ký cuối cùng
                    if (dsm.Signatures.Count < 1)
                    {
                        strError = "Không tìm thấy chữ ký trên file.";
                        return null;
                    }
                    signature = dsm.Signatures[dsm.Signatures.Count - 1];

                    //Kiểm tra tính toàn vẹn chữ ký
                    if (signature.Verify != VerifyResult.Success)
                    {
                        strError = "Chữ ký không hợp lệ: " + signature.Verify.ToString();
                        return null;
                    }

                    //Kiểm tra hiệu lực chứng thư với CA
                    CertificateStatus_CA statusCA;
                    if (!this.ValidateCertificateInCA_Now(signature.Signer.RawData, out statusCA))
                    {
                        strError = "Chứng thư không được xác thực bởi nhà cung cấp CA.";
                        return null;
                    }
                    //Kiểm tra hiệu lực chứng thư trong TTĐ
                    CertificateStatus_TTD statusTTD;
                    if (!this.ValidateCertificateInTTD_Now(programName, FileMaDV, (int)UnitTypes.DonViPhatDien, signature.Signer.RawData, out statusTTD, out userName))
                    {
                        strError = "Chứng thư không được xác thực bởi hệ thống chữ ký số TTD: " + statusTTD.StatusInformation.ToString();
                        return null;
                    }
                }
                #endregion

                #region Khởi tạo văn bản, lưu file và chữ ký
                //Khởi tạo
                DataTable dt = bus.FL_File_InsertNewFile(programName, userName, FileTypeID, FileMaDV, FileDate, FileName, Description);
                if (dt.Rows.Count < 1)
                    return null;
                else
                {
                    fi = new FileInfoCA();
                    fi.FileID = Convert.ToInt32(dt.Rows[0]["FileID"]);
                    fi.FileNumber = dt.Rows[0]["FileNumber"].ToString();
                    fi.FilePath = dt.Rows[0]["FilePath"].ToString();
                    fi.FileTypeID = Convert.ToInt32(dt.Rows[0]["FileTypeID"]);
                    fi.FileDate = Convert.ToDateTime(dt.Rows[0]["FileDate"]);
                    fi.MaDV = dt.Rows[0]["MaDV"].ToString();
                    fi.Status = Convert.ToInt32(dt.Rows[0]["Status"]);
                    fi.Description = dt.Rows[0]["Description"].ToString();

                    signType = Convert.ToInt32(dt.Rows[0]["QuyenTaoFile"]);
                }
                //Tạo folder nếu chưa có
                string dir = Path.GetDirectoryName(fi.FilePath);
                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);
                //Lưu file
                if (!this.SaveFile_WithHash(fi.FileID, fileData, programName, userName, ref strError))
                    return null;
                //Ghi log ký
                bus.FL_File_UpdateForLogSign(fi.FileID, signature.Signer.SerialNumber, signature.SigningTime, (int)signature.Verify,
                    signType, (int)FileSignActions.AddSignature, fi.FilePath, "Hoàn thành lưu file có chữ ký", programName, userName);
                #endregion

                return fi;
            }
            catch (Exception ex)
            {
                strError = "Lỗi kiểm tra và lưu văn bản: " + ex.Message;
                return null;
            }
        }
        #endregion

        #region Ký file văn bản
        //Hàm ký nhiều file
        [WebMethod(Description = "Ký nhiều file trong hệ thống quản lý file: tuần tự, trực tiếp từ CAService", MessageName = "1.SignFilesByID_ReturnDetail_NoApp")]
        public int SignFilesByID_ReturnDetails6(int[] fileIDs, string programName, string userName, string password,
            ref DataTable dtResult, ref string strError)
        {
            //Results
            int ret = (int)CAExitCode.Error;    //Nếu tất cả ko ký được -> Error; nếu ít nhất 1 thằng thành công -> Success
            dtResult = createTableSignResult();
            strError = "";
            HSMServiceProvider provider = new HSMServiceProvider();

            try
            {
                ////Thư viện HSM
                //Common.CRYPTOKI = GetCryptokiDLL();
                //CSDL
                DAL_SqlConnector.ConnectionString = GetDatabaseString();
                BUSQuanTri bus = new BUSQuanTri();
                //Tham số
                X509Certificate2 cert;  //Chứng thư ký

                #region Lấy thông tin để ký
                //Lấy thông tin certificate
                DataTable dtCert = bus.CA_Certificate_SelectForSign(programName, userName);
                cert = new X509Certificate2((byte[])dtCert.Rows[0]["RawData"]);
                int certID = Convert.ToInt32(dtCert.Rows[0]["CertID"]);

                ////Xác thực chứng thư với nhà cung cấp CA
                //X509ChainStatus certificateStatus;
                //if (!Common.ValidateCertificate(cert, DateTime.Now, out certificateStatus))
                //    throw new Exception("WS_Kiểm tra chứng thư số với nhà cung cấp CA không hợp lệ.");

                //Lấy key trong HSM
                //BUILD: dùng hàm HSM_WLDObject_SelectPrivateKeyByCertID nếu chạy ở chế độ WLD
                DataTable dtPK = bus.HSM_Object_SelectPrivateKeyByCertID(certID);
                string slotSerial = dtPK.Rows[0]["SlotSerial"].ToString();
                byte[] keyID = (byte[])dtPK.Rows[0]["CKA_ID"];

                // Đăng nhập và load PrivateKey
                HSMReturnValue loginResult = provider.Login(slotSerial, HSMLoginRole.User, password);
                switch (loginResult)
                {
                    case HSMReturnValue.OK:
                        break;
                    case HSMReturnValue.PIN_LEN_RANGE:
                        {
                            strError = "WS_Đăng nhập HSM không thành công: Độ dài mã PIN không đúng.";
                            return (int)CAExitCode.HSM_PinLenRange;
                        }
                    case HSMReturnValue.PIN_INCORRECT:
                        {
                            strError = "WS_Đăng nhập HSM không thành công: Sai mã PIN.";
                            return (int)CAExitCode.HSM_PinIncorrect;
                        }
                    case HSMReturnValue.PIN_LOCKED:
                        {
                            strError = "WS_Đăng nhập HSM không thành công: Mã PIN đã bị khóa.";
                            return (int)CAExitCode.HSM_PinLocked;
                        }
                    default:
                        {
                            strError = "WS_Đăng nhập HSM không thành công: " + loginResult.ToString() + ".";
                            return (int)CAExitCode.HSM_LoginFailed;
                        }
                }
                provider.LoadPrivateKeyByID(keyID);

                //Chuyển mảng FileID thành string
                string arrFileIDs = "";
                foreach (int fileID in fileIDs)
                    arrFileIDs += fileID.ToString() + ";";
                //xin lệnh ký và thông tin file ký
                bool okToSign = bus.FL_File_SelectForAllowSign_Array(arrFileIDs, programName, userName, ref dtResult);
                dtResult.TableName = "RESULTS";
                int soDuocKy = 0;
                int soThanhCong = 0;
                #endregion

                #region Ký tuần tự
                for (int index = 0; index < dtResult.Rows.Count; index++)
                {
                    //////remove chữ ký
                    ////int fileID1 = Convert.ToInt32(dtResult.Rows[index]["FileID"]);
                    ////string filePath1 = bus.FL_File_GetFilePathForSign(fileID1);
                    ////byte[] fileData1 = File.ReadAllBytes(filePath1);
                    ////string fileExtension1 = Path.GetExtension(filePath1);

                    ////using (ESDigitalSignatureManager dsm = new ESDigitalSignatureManager(fileData1, fileExtension1))
                    ////{
                    ////    dsm.RemoveAllSignature();
                    ////    File.WriteAllBytes(filePath1, dsm.ToArray());
                    ////}

                    ////continue;

                    try
                    {
                        if (Convert.ToBoolean(dtResult.Rows[index]["OKtoSign"]) == true)
                        {
                            // Lấy dữ liệu
                            soDuocKy++;
                            int fileID = Convert.ToInt32(dtResult.Rows[index]["FileID"]);
                            int signType = Convert.ToInt32(dtResult.Rows[index]["QuyenUnit_Type"]);
                            string filePath = dtResult.Rows[index]["FilePath"].ToString();
                            byte[] fileData = File.ReadAllBytes(filePath);
                            string fileExtension = Path.GetExtension(filePath);

                            //byte[] ArrSigned = provider.SignData(Encoding.ASCII.GetBytes("RandomTextForTest"));
                            using (ESDigitalSignatureManager dsm = new ESDigitalSignatureManager(fileData, fileExtension))
                            {
                                //Ký và lấy chữ ký mới nhất
                                dsm.Sign(cert, provider);
                                ESignature signature = dsm.Signatures[dsm.Signatures.Count - 1];

                                //Kiểm tra thời gian chờ trước khi cập nhật
                                if (bus.FL_File_SelectForSaveSign(Convert.ToInt32(dtResult.Rows[index]["ID_StatusLog"])))
                                {
                                    // Lưu vào file đích
                                    File.WriteAllBytes(filePath, dsm.ToArray());

                                    //Ghi log
                                    bus.FL_File_UpdateForLogSign(fileID, signature.Signer.SerialNumber, signature.SigningTime, (int)signature.Verify,
                                        signType, (int)FileSignActions.AddSignature, filePath, "Hoàn thành ký file qua HSM", programName, userName);

                                    //Báo thành công
                                    dtResult.Rows[index]["SignResults"] = (int)FileSignResults.Success;
                                    dtResult.Rows[index]["SignDetails"] = "Ký văn bản thành công.";

                                    //Cập nhật có xác nhận hay không
                                    dtResult.Rows[index]["XacNhan"] = bus.FL_FileType_QuyenXacNhan_CheckByFileID_CertID(fileID, signature.Signer.SerialNumber);

                                    // Chỉ cần có file thành công thì trả về true
                                    ret = (int)CAExitCode.Success;
                                    soThanhCong++;
                                }
                                else
                                {
                                    dtResult.Rows[index]["SignResults"] = (int)FileSignResults.SaveTimeOut;
                                    dtResult.Rows[index]["SignDetails"] = "Ký văn bản không thành công: Hết thời gian chờ ký (" + dtResult.Rows[index]["WaitSaveOff"].ToString() + " giây).";
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        //bus.FL_File_UpdateStatus(fileID, (int)FileStatus.LuuFile, "Hủy ký", programName, userName);
                        dtResult.Rows[index]["SignResults"] = (int)FileSignResults.FileSignFailed;
                        dtResult.Rows[index]["SignDetails"] = "Ký văn bản không thành công: " + ex.Message;
                    }
                }
                #endregion

                //Kết thúc
                strError = "Tổng số văn bản: " + dtResult.Rows.Count.ToString() + ". Được phép ký: " + soDuocKy.ToString()
                    + ". Ký thành công: " + soThanhCong.ToString() + ".";
                return ret;
            }
            catch (Exception ex)
            {
                //Kết thúc
                strError = ex.Message;
                return (int)CAExitCode.Error;
            }
            finally
            {
                provider.Dispose();
            }
        }

        [WebMethod(Description = "Ký nhiều file trong hệ thống quản lý file: tuần tự, trực tiếp từ CAService", MessageName = "2.SignFilesByID_ReturnDetail_NoApp")]
        public int SignFilesByID_ReturnDetails6(string arrFileIDs, string programName, string userName, string password,
            ref DataTable dtResult, ref string strError)
        {
            //Results
            int ret = (int)CAExitCode.Error;    //Nếu tất cả ko ký được -> Error; nếu ít nhất 1 thằng thành công -> Success
            dtResult = createTableSignResult();
            strError = "";
            HSMServiceProvider provider = new HSMServiceProvider();

            try
            {
                ////Thư viện HSM
                //Common.CRYPTOKI = GetCryptokiDLL();
                //CSDL
                DAL_SqlConnector.ConnectionString = GetDatabaseString();
                BUSQuanTri bus = new BUSQuanTri();
                //Tham số
                X509Certificate2 cert;  //Chứng thư ký

                #region Lấy thông tin để ký
                //Lấy thông tin certificate
                DataTable dtCert = bus.CA_Certificate_SelectForSign(programName, userName);
                cert = new X509Certificate2((byte[])dtCert.Rows[0]["RawData"]);
                int certID = Convert.ToInt32(dtCert.Rows[0]["CertID"]);

                ////Xác thực chứng thư với nhà cung cấp CA
                //X509ChainStatus certificateStatus;
                //if (!Common.ValidateCertificate(cert, DateTime.Now, out certificateStatus))
                //    throw new Exception("WS_Kiểm tra chứng thư số với nhà cung cấp CA không hợp lệ.");

                //Lấy key trong HSM
                //BUILD: dùng hàm HSM_WLDObject_SelectPrivateKeyByCertID nếu chạy ở chế độ WLD
                DataTable dtPK = bus.HSM_Object_SelectPrivateKeyByCertID(certID);
                string slotSerial = dtPK.Rows[0]["SlotSerial"].ToString();
                byte[] keyID = (byte[])dtPK.Rows[0]["CKA_ID"];

                // Đăng nhập và load PrivateKey
                HSMReturnValue loginResult = provider.Login(slotSerial, HSMLoginRole.User, password);
                switch (loginResult)
                {
                    case HSMReturnValue.OK:
                        break;
                    case HSMReturnValue.PIN_LEN_RANGE:
                        {
                            strError = "WS_Đăng nhập HSM không thành công: Độ dài mã PIN không đúng.";
                            return (int)CAExitCode.HSM_PinLenRange;
                        }
                    case HSMReturnValue.PIN_INCORRECT:
                        {
                            strError = "WS_Đăng nhập HSM không thành công: Sai mã PIN.";
                            return (int)CAExitCode.HSM_PinIncorrect;
                        }
                    case HSMReturnValue.PIN_LOCKED:
                        {
                            strError = "WS_Đăng nhập HSM không thành công: Mã PIN đã bị khóa.";
                            return (int)CAExitCode.HSM_PinLocked;
                        }
                    default:
                        {
                            strError = "WS_Đăng nhập HSM không thành công: " + loginResult.ToString() + ".";
                            return (int)CAExitCode.HSM_LoginFailed;
                        }
                }
                provider.LoadPrivateKeyByID(keyID);

                //xin lệnh ký và thông tin file ký
                bool okToSign = bus.FL_File_SelectForAllowSign_Array(arrFileIDs, programName, userName, ref dtResult);
                if (!okToSign)
                    throw new Exception("WS_Văn bản không được phép ký.");
                dtResult.TableName = "RESULTS";
                int soDuocKy = 0;
                int soThanhCong = 0;
                #endregion

                #region Ký tuần tự
                for (int index = 0; index < dtResult.Rows.Count; index++)
                {
                    //////remove chữ ký
                    ////int fileID1 = Convert.ToInt32(dtResult.Rows[index]["FileID"]);
                    ////string filePath1 = bus.FL_File_GetFilePathForSign(fileID1);
                    ////byte[] fileData1 = File.ReadAllBytes(filePath1);
                    ////string fileExtension1 = Path.GetExtension(filePath1);

                    ////using (ESDigitalSignatureManager dsm = new ESDigitalSignatureManager(fileData1, fileExtension1))
                    ////{
                    ////    dsm.RemoveAllSignature();
                    ////    File.WriteAllBytes(filePath1, dsm.ToArray());
                    ////}

                    ////continue;

                    try
                    {
                        if (Convert.ToBoolean(dtResult.Rows[index]["OKtoSign"]) == true)
                        {
                            // Lấy dữ liệu
                            soDuocKy++;
                            int fileID = Convert.ToInt32(dtResult.Rows[index]["FileID"]);
                            int signType = Convert.ToInt32(dtResult.Rows[index]["QuyenUnit_Type"]);
                            string filePath = dtResult.Rows[index]["FilePath"].ToString();
                            byte[] fileData = File.ReadAllBytes(filePath);
                            string fileExtension = Path.GetExtension(filePath);

                            //byte[] ArrSigned = provider.SignData(Encoding.ASCII.GetBytes("RandomTextForTest"));
                            using (ESDigitalSignatureManager dsm = new ESDigitalSignatureManager(fileData, fileExtension))
                            {
                                //Kiểm tra chữ ký
                                //PA1: nếu đã có chữ ký của cert thì xóa và ký lại
                                //PA2 (chọn): nếu đã có chữ ký của cert thì ko ký và cập nhật chữ ký này vào db

                                dsm.Sign(cert, provider);
                                ESignature signature = dsm.Signatures[dsm.Signatures.Count - 1];

                                //Kiểm tra thời gian chờ trước khi cập nhật
                                if (bus.FL_File_SelectForSaveSign(Convert.ToInt32(dtResult.Rows[index]["ID_StatusLog"])))
                                {
                                    // Lưu vào file đích
                                    File.WriteAllBytes(filePath, dsm.ToArray());

                                    //Ghi log
                                    bus.FL_File_UpdateForLogSign(fileID, signature.Signer.SerialNumber, signature.SigningTime, (int)signature.Verify,
                                        signType, (int)FileSignActions.AddSignature, filePath, "Hoàn thành ký file qua HSM", programName, userName);

                                    //Báo thành công
                                    dtResult.Rows[index]["SignResults"] = (int)FileSignResults.Success;
                                    dtResult.Rows[index]["SignDetails"] = "Ký văn bản thành công.";

                                    //Cập nhật có xác nhận hay không
                                    dtResult.Rows[index]["XacNhan"] = bus.FL_FileType_QuyenXacNhan_CheckByFileID_CertID(fileID, signature.Signer.SerialNumber);

                                    // Chỉ cần có file thành công thì trả về true
                                    ret = (int)CAExitCode.Success;
                                    soThanhCong++;
                                }
                                else
                                {
                                    dtResult.Rows[index]["SignResults"] = (int)FileSignResults.SaveTimeOut;
                                    dtResult.Rows[index]["SignDetails"] = "Ký văn bản không thành công: Hết thời gian chờ ký (" + dtResult.Rows[index]["WaitSaveOff"].ToString() + " giây).";
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        //bus.FL_File_UpdateStatus(fileID, (int)FileStatus.LuuFile, "Hủy ký", programName, userName);
                        dtResult.Rows[index]["SignResults"] = (int)FileSignResults.FileSignFailed;
                        dtResult.Rows[index]["SignDetails"] = "Ký văn bản không thành công: " + ex.Message;
                    }
                }
                #endregion

                //Kết thúc
                strError = "Tổng số văn bản: " + dtResult.Rows.Count.ToString() + ". Được phép ký: " + soDuocKy.ToString()
                    + ". Ký thành công: " + soThanhCong.ToString() + ".";
                return ret;

                #region Comment phần Thread
                ////tối đa là xxx thread, số lượng file ký sẽ được truyền vào thread
                //int threadDone = 0;
                //int maxThread = 10;
                //if (dtResult.Rows.Count < maxThread)
                //    maxThread = 1;
                //int filePerThread = (int)Math.Ceiling((double)dtResult.Rows.Count / maxThread);

                ////Tạo từng thread
                //for (int i = 0; i < maxThread; i++)
                //{
                //    int j = i;
                //    int countFile = 0;

                //    try
                //    {
                //        //Mở session và load PrivateKey
                //        HSMServiceProvider provider = new HSMServiceProvider(slotSerial, HSMLoginRole.User, password);
                //        provider.LoadPrivateKeyByID(keyID);

                //        #region Thread
                //        BackgroundWorker bgw = new BackgroundWorker();
                //        bgw.DoWork += delegate
                //        {

                //            for (int count = 0; count < filePerThread; count++)
                //            {
                //                int index = filePerThread * j + count;
                //                if (index >= dtResult.Rows.Count) return;

                //                //string tempFile = Path.GetTempFileName();
                //                //string tempPath = tempFile;
                //                ESignature signature;

                //                try
                //                {
                //                    if (Convert.ToBoolean(dtResult.Rows[index]["OKtoSign"]) == true)
                //                    {
                //                        int fileID = Convert.ToInt32(dtResult.Rows[index]["FileID"]);
                //                        string filePath = bus.FL_File_GetFilePathForSign(fileID);

                //                        //copy ra file tạm
                //                        //tempPath = tempFile + Path.GetExtension(filePath);
                //                        //File.Copy(filePath, tempPath, true);
                //                        byte[] fileData = File.ReadAllBytes(filePath);
                //                        string fileExtension = Path.GetExtension(filePath);

                //                        //byte[] ArrSigned = provider.SignData(Encoding.ASCII.GetBytes("RandomTextForTest"));
                //                        using (ESDigitalSignatureManager dsm = new ESDigitalSignatureManager(fileData, fileExtension))
                //                        {
                //                            //Kiểm tra chữ ký
                //                            //PA1: nếu đã có chữ ký của cert thì xóa và ký lại
                //                            //PA2 (chọn): nếu đã có chữ ký của cert thì ko ký và cập nhật chữ ký này vào db

                //                            dsm.Sign(cert, provider);
                //                            signature = dsm.Signatures[dsm.Signatures.Count - 1];

                //                            //Kiểm tra thời gian chờ trước khi cập nhật
                //                            if (bus.FL_File_SelectForSaveSign(Convert.ToInt32(dtResult.Rows[index]["ID_StatusLog"])))
                //                            {
                //                                // Lưu vào file đích
                //                                //File.Copy(tempPath, filePath, true);
                //                                File.WriteAllBytes(filePath, dsm.ToArray());

                //                                //Ghi log
                //                                bus.FL_File_UpdateForLogSign(fileID, signature.Signer.SerialNumber, signature.SigningTime, (int)signature.Verify,
                //                                    "", (int)FileSignActions.AddSignature, filePath, "Hoàn thành ký file qua HSM", programName, userName);

                //                                //Truyền transaction vào bus.FL_File_UpdateForLogSign
                //                                //File.Copy
                //                                //transaction.Commit()

                //                                //Báo thành công
                //                                dtResult.Rows[index]["SignResults"] = (int)FileSignResults.Success;
                //                                dtResult.Rows[index]["SignDetails"] = "Ký văn bản thành công.";

                //                                /*Cập nhật vào db của hệ thống riêng ở đây*/
                //                            }
                //                            else
                //                            {
                //                                //bus.FL_File_UpdateStatus(fileID, (int)FileStatus.LuuFile, "Hủy ký", programName, userName);
                //                                dtResult.Rows[index]["SignResults"] = (int)FileSignResults.SaveTimeOut;
                //                                dtResult.Rows[index]["SignDetails"] = "Ký văn bản thất bại: Hết thời gian chờ lưu file.";
                //                            }
                //                        }
                //                    }
                //                    else
                //                    {
                //                        //Nếu có 1 thằng không được ký (do 1 lỗi nào đó), trạng thái đã có sẵn, chỉ cần cập nhật lại kết quả chung thôi
                //                        ret = CAExitCode.Error;
                //                    }
                //                }
                //                catch (Exception ex)
                //                {
                //                    ret = GetErrorCodeFromString(ex.Message);

                //                    //bus.FL_File_UpdateStatus(fileID, (int)FileStatus.LuuFile, "Hủy ký", programName, userName);
                //                    dtResult.Rows[index]["SignResults"] = (int)FileSignResults.FileSignFailed;
                //                    dtResult.Rows[index]["SignDetails"] = "Ký văn bản thất bại: " + ex.Message;
                //                }
                //                //finally
                //                //{
                //                //    // Xóa các file tạm
                //                //    try
                //                //    {
                //                //        File.Delete(tempFile);
                //                //        File.Delete(tempPath);
                //                //    }
                //                //    catch { }
                //                //}
                //            }
                //        };
                //        bgw.RunWorkerCompleted += delegate
                //        {
                //            threadDone++;
                //            countFile++;

                //            if (countFile == filePerThread)
                //            {
                //                provider.Dispose();
                //            }
                //            //Đã ký hết các file
                //            if (threadDone == maxThread)
                //            {
                //                //Kết thúc
                //                HSMServiceProvider.Finalize();

                //                //export ra file text
                //                Process curProcess = Process.GetCurrentProcess();
                //                string directoryLog = Path.Combine(Application.StartupPath, "Result");
                //                string fileLog = Path.Combine(directoryLog, curProcess.Id.ToString() + ".txt");

                //                if (!Directory.Exists(directoryLog))
                //                    Directory.CreateDirectory(directoryLog);

                //                if (File.Exists(fileLog))
                //                    File.Delete(fileLog);

                //                File.Create(fileLog).Dispose();

                //                dtResult.WriteXml(fileLog, XmlWriteMode.WriteSchema);

                //                ExitWithCode(ret);
                //            }
                //            bgw.Dispose();
                //        };
                //        bgw.RunWorkerAsync();
                //        #endregion

                //        #region Minhdn 18/8/2015
                //        //for (int count = 0; count < filePerThread; count++)
                //        //{
                //        //    int index = filePerThread * j + count;
                //        //    if (index >= dtResult.Rows.Count) return;

                //        //    string tempFile = Path.GetTempFileName();
                //        //    string tempPath = tempFile;
                //        //    ESignature signature;

                //        //    try
                //        //    {
                //        //        if (Convert.ToBoolean(dtResult.Rows[index]["OKtoSign"]) == true)
                //        //        {
                //        //            int fileID = Convert.ToInt32(dtResult.Rows[index]["FileID"]);
                //        //            string filePath = bus.FL_File_GetFilePathForSign(fileID);
                //        //            tempPath = tempFile + Path.GetExtension(filePath);

                //        //            //copy ra file tạm
                //        //            File.Copy(filePath, tempPath, true);

                //        //            //byte[] ArrSigned = provider.SignData(Encoding.ASCII.GetBytes("RandomTextForTest"));
                //        //            using (ESDigitalSignatureManager dsm = new ESDigitalSignatureManager(tempPath))
                //        //            {
                //        //                //Kiểm tra chữ ký
                //        //                //PA1: nếu đã có chữ ký của cert thì xóa và ký lại
                //        //                //PA2 (chọn): nếu đã có chữ ký của cert thì ko ký và cập nhật chữ ký này vào db

                //        //                int curSignCount = dsm.Signatures.Count;
                //        //                dsm.Sign(cert, provider);

                //        //                using (ESDigitalSignatureManager dsm1 = new ESDigitalSignatureManager(tempPath))
                //        //                {
                //        //                    if (dsm1.Signatures.Count == curSignCount)
                //        //                    {
                //        //                        dtResult.Rows[index]["SignResults"] = (int)FileSignResults.FileSignFailed;
                //        //                        dtResult.Rows[index]["SignDetails"] = "Ký văn bản thất bại";
                //        //                        continue;
                //        //                    }
                //        //                    else
                //        //                    {
                //        //                        signature = dsm1.Signatures[curSignCount];
                //        //                    }
                //        //                }
                //        //            }

                //        //            if (bus.FL_File_SelectForSaveSign(Convert.ToInt32(dtResult.Rows[index]["ID_StatusLog"])))
                //        //            {
                //        //                //Copy vào file đích
                //        //                File.Copy(tempPath, filePath, true);

                //        //                //Ghi log
                //        //                bus.FL_File_UpdateForLogSign(fileID, signature.Signer.SerialNumber, signature.SigningTime, (int)signature.Verify,
                //        //                    signature.SignatureCreator, (int)FileSignActions.AddSignature, filePath, "Hoàn thành ký file qua HSM", programName, userName);

                //        //                //Truyền transaction vào bus.FL_File_UpdateForLogSign
                //        //                //File.Copy
                //        //                //transaction.Commit()

                //        //                //Báo thành công
                //        //                dtResult.Rows[index]["SignResults"] = (int)FileSignResults.Success;
                //        //                dtResult.Rows[index]["SignDetails"] = "Ký văn bản thành công.";

                //        //                /*Cập nhật vào db của hệ thống riêng ở đây*/
                //        //            }
                //        //            else
                //        //            {
                //        //                //Kiểm tra thời gian chờ trước khi cập nhật
                //        //                //bus.FL_File_UpdateStatus(fileID, (int)FileStatus.LuuFile, "Hủy ký", programName, userName);
                //        //                dtResult.Rows[index]["SignResults"] = (int)FileSignResults.SaveTimeOut;
                //        //                dtResult.Rows[index]["SignDetails"] = "Ký văn bản thất bại: Hết thời gian chờ lưu file.";
                //        //            }
                //        //        }
                //        //        else
                //        //        {
                //        //            //Nếu có 1 thằng không được ký (do 1 lỗi nào đó), trạng thái đã có sẵn, chỉ cần cập nhật lại kết quả chung thôi
                //        //            ret = CAExitCode.Error;
                //        //        }
                //        //    }
                //        //    catch (Exception ex)
                //        //    {
                //        //        ret = GetErrorCodeFromString(ex.Message);

                //        //        //bus.FL_File_UpdateStatus(fileID, (int)FileStatus.LuuFile, "Hủy ký", programName, userName);
                //        //        dtResult.Rows[index]["SignResults"] = (int)FileSignResults.FileSignFailed;
                //        //        dtResult.Rows[index]["SignDetails"] = "Ký văn bản thất bại: " + ex.Message;
                //        //    }
                //        //    finally
                //        //    {
                //        //        // Xóa các file tạm
                //        //        try
                //        //        {
                //        //            File.Delete(tempFile);
                //        //            File.Delete(tempPath);
                //        //        }
                //        //        catch { }
                //        //    }
                //        //}

                //        //threadDone++;
                //        //countFile++;

                //        //if (countFile == filePerThread)
                //        //{
                //        //    provider.Dispose();
                //        //}
                //        ////Đã ký hết các file
                //        //if (threadDone == maxThread)
                //        //{
                //        //    //Kết thúc
                //        //    HSMServiceProvider.Finalize();

                //        //    //export ra file text
                //        //    Process curProcess = Process.GetCurrentProcess();
                //        //    string directoryLog = Path.Combine(Application.StartupPath, "Result");
                //        //    string fileLog = Path.Combine(directoryLog, curProcess.Id.ToString() + ".txt");

                //        //    if (!Directory.Exists(directoryLog))
                //        //        Directory.CreateDirectory(directoryLog);

                //        //    if (File.Exists(fileLog))
                //        //        File.Delete(fileLog);

                //        //    File.Create(fileLog).Dispose();

                //        //    dtResult.WriteXml(fileLog, XmlWriteMode.WriteSchema);

                //        //    ExitWithCode(ret);
                //        //}
                //        #endregion
                //    }
                //    catch
                //    {
                //        //Lỗi thì vẫn chạy thread khác
                //    }
                //}
                #endregion
            }
            catch (Exception ex)
            {
                //Kết thúc
                strError = ex.Message;
                return (int)CAExitCode.Error;
            }
            finally
            {
                provider.Dispose();
            }
        }

        //[WebMethod(Description = "Ký nhiều file trong hệ thống quản lý file: tuần tự, trực tiếp từ CAService", MessageName = "2.SignFilesByID_ReturnDetail_NoApp")]
        //public int SignFilesByID_ReturnDetails6(string arrFileIDs, string programName, string userName, string password,
        //    ref DataTable dtResult, ref string strError)
        //{
        //    //Results
        //    int ret = (int)CAExitCode.Error;    //Nếu tất cả ko ký được -> Error; nếu ít nhất 1 thằng thành công -> Success
        //    dtResult = createTableSignResult();
        //    strError = "";
        //    HSMServiceProvider provider = new HSMServiceProvider();

        //    try
        //    {
        //        ////Thư viện HSM
        //        //Common.CRYPTOKI = GetCryptokiDLL();
        //        //CSDL
        //        //DAL_SqlConnector.ConnectionString = GetDatabaseString();
        //        BUSQuanTri bus = new BUSQuanTri();
        //        //Tham số
        //        X509Certificate2 cert;  //Chứng thư ký

        //        #region Lấy thông tin để ký
        //        //Lấy thông tin certificate
        //        DataTable dtCert = bus.CA_Certificate_SelectForSign(programName, userName);
        //        cert = new X509Certificate2((byte[])dtCert.Rows[0]["RawData"]);
        //        int certID = Convert.ToInt32(dtCert.Rows[0]["CertID"]);

        //        ////Xác thực chứng thư với nhà cung cấp CA
        //        //X509ChainStatus certificateStatus;
        //        //if (!Common.ValidateCertificate(cert, DateTime.Now, out certificateStatus))
        //        //    throw new Exception("WS_Kiểm tra chứng thư số với nhà cung cấp CA không hợp lệ.");

        //        //Lấy key trong HSM
        //        //BUILD: dùng hàm HSM_WLDObject_SelectPrivateKeyByCertID nếu chạy ở chế độ WLD
        //        DataTable dtPK = bus.HSM_Object_SelectPrivateKeyByCertID(certID);
        //        string slotSerial = dtPK.Rows[0]["SlotSerial"].ToString();
        //        byte[] keyID = (byte[])dtPK.Rows[0]["CKA_ID"];

        //        // Đăng nhập và load PrivateKey
        //        HSMReturnValue loginResult = provider.Login(slotSerial, HSMLoginRole.User, password);
        //        switch (loginResult)
        //        {
        //            case HSMReturnValue.OK:
        //                break;
        //            case HSMReturnValue.PIN_LEN_RANGE:
        //                {
        //                    strError = "WS_Đăng nhập HSM không thành công: Độ dài mã PIN không đúng.";
        //                    return (int)CAExitCode.HSM_PinLenRange;
        //                }
        //            case HSMReturnValue.PIN_INCORRECT:
        //                {
        //                    strError = "WS_Đăng nhập HSM không thành công: Sai mã PIN.";
        //                    return (int)CAExitCode.HSM_PinIncorrect;
        //                }
        //            case HSMReturnValue.PIN_LOCKED:
        //                {
        //                    strError = "WS_Đăng nhập HSM không thành công: Mã PIN đã bị khóa.";
        //                    return (int)CAExitCode.HSM_PinLocked;
        //                }
        //            default:
        //                {
        //                    strError = "WS_Đăng nhập HSM không thành công: " + loginResult.ToString() + ".";
        //                    return (int)CAExitCode.HSM_LoginFailed;
        //                }
        //        }
        //        provider.LoadPrivateKeyByID(keyID);

        //        //xin lệnh ký và thông tin file ký
        //        bool okToSign = bus.FL_File_SelectForAllowSign_Array(arrFileIDs, programName, userName, ref dtResult);
        //        if (!okToSign)
        //            throw new Exception("WS_Văn bản không được phép ký.");
        //        dtResult.TableName = "RESULTS";
        //        int soDuocKy = 0;
        //        int soThanhCong = 0;
        //        #endregion

        //        #region Ký tuần tự
        //        for (int index = 0; index < dtResult.Rows.Count; index++)
        //        {
        //            //////remove chữ ký
        //            ////int fileID1 = Convert.ToInt32(dtResult.Rows[index]["FileID"]);
        //            ////string filePath1 = bus.FL_File_GetFilePathForSign(fileID1);
        //            ////byte[] fileData1 = File.ReadAllBytes(filePath1);
        //            ////string fileExtension1 = Path.GetExtension(filePath1);

        //            ////using (ESDigitalSignatureManager dsm = new ESDigitalSignatureManager(fileData1, fileExtension1))
        //            ////{
        //            ////    dsm.RemoveAllSignature();
        //            ////    File.WriteAllBytes(filePath1, dsm.ToArray());
        //            ////}

        //            ////continue;

        //            try
        //            {
        //                if (Convert.ToBoolean(dtResult.Rows[index]["OKtoSign"]) == true)
        //                {
        //                    // Lấy dữ liệu
        //                    soDuocKy++;
        //                    int fileID = Convert.ToInt32(dtResult.Rows[index]["FileID"]);
        //                    int signType = Convert.ToInt32(dtResult.Rows[index]["QuyenUnit_Type"]);
        //                    string filePath = dtResult.Rows[index]["FilePath"].ToString();
        //                    byte[] fileData = File.ReadAllBytes(filePath);
        //                    string fileExtension = Path.GetExtension(filePath);

        //                    //byte[] ArrSigned = provider.SignData(Encoding.ASCII.GetBytes("RandomTextForTest"));
        //                    using (ESDigitalSignatureManager dsm = new ESDigitalSignatureManager(fileData, fileExtension))
        //                    {
        //                        //Kiểm tra chữ ký
        //                        //PA1: nếu đã có chữ ký của cert thì xóa và ký lại
        //                        //PA2 (chọn): nếu đã có chữ ký của cert thì ko ký và cập nhật chữ ký này vào db

        //                        dsm.Sign(cert, provider);
        //                        ESignature signature = dsm.Signatures[dsm.Signatures.Count - 1];

        //                        //Kiểm tra thời gian chờ trước khi cập nhật
        //                        if (bus.FL_File_SelectForSaveSign(Convert.ToInt32(dtResult.Rows[index]["ID_StatusLog"])))
        //                        {
        //                            // Lưu vào file đích
        //                            File.WriteAllBytes(filePath, dsm.ToArray());

        //                            //Ghi log
        //                            bus.FL_File_UpdateForLogSign(fileID, signature.Signer.SerialNumber, signature.SigningTime, (int)signature.Verify,
        //                                signType, (int)FileSignActions.AddSignature, filePath, "Hoàn thành ký file qua HSM", programName, userName);

        //                            //Báo thành công
        //                            dtResult.Rows[index]["SignResults"] = (int)FileSignResults.Success;
        //                            dtResult.Rows[index]["SignDetails"] = "Ký văn bản thành công.";

        //                            //Cập nhật có xác nhận hay không
        //                            dtResult.Rows[index]["XacNhan"] = bus.FL_FileType_QuyenXacNhan_CheckByFileID_CertID(fileID, signature.Signer.SerialNumber);

        //                            // Chỉ cần có file thành công thì trả về true
        //                            ret = (int)CAExitCode.Success;
        //                            soThanhCong++;
        //                        }
        //                        else
        //                        {
        //                            dtResult.Rows[index]["SignResults"] = (int)FileSignResults.SaveTimeOut;
        //                            dtResult.Rows[index]["SignDetails"] = "Ký văn bản không thành công: Hết thời gian chờ ký (" + dtResult.Rows[index]["WaitSaveOff"].ToString() + " giây).";
        //                        }
        //                    }
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                //bus.FL_File_UpdateStatus(fileID, (int)FileStatus.LuuFile, "Hủy ký", programName, userName);
        //                dtResult.Rows[index]["SignResults"] = (int)FileSignResults.FileSignFailed;
        //                dtResult.Rows[index]["SignDetails"] = "Ký văn bản không thành công: " + ex.Message;
        //            }
        //        }
        //        #endregion

        //        //Kết thúc
        //        strError = "Tổng số văn bản: " + dtResult.Rows.Count.ToString() + ". Được phép ký: " + soDuocKy.ToString()
        //            + ". Ký thành công: " + soThanhCong.ToString() + ".";
        //        return ret;

        //        #region Comment phần Thread
        //        ////tối đa là xxx thread, số lượng file ký sẽ được truyền vào thread
        //        //int threadDone = 0;
        //        //int maxThread = 10;
        //        //if (dtResult.Rows.Count < maxThread)
        //        //    maxThread = 1;
        //        //int filePerThread = (int)Math.Ceiling((double)dtResult.Rows.Count / maxThread);

        //        ////Tạo từng thread
        //        //for (int i = 0; i < maxThread; i++)
        //        //{
        //        //    int j = i;
        //        //    int countFile = 0;

        //        //    try
        //        //    {
        //        //        //Mở session và load PrivateKey
        //        //        HSMServiceProvider provider = new HSMServiceProvider(slotSerial, HSMLoginRole.User, password);
        //        //        provider.LoadPrivateKeyByID(keyID);

        //        //        #region Thread
        //        //        BackgroundWorker bgw = new BackgroundWorker();
        //        //        bgw.DoWork += delegate
        //        //        {

        //        //            for (int count = 0; count < filePerThread; count++)
        //        //            {
        //        //                int index = filePerThread * j + count;
        //        //                if (index >= dtResult.Rows.Count) return;

        //        //                //string tempFile = Path.GetTempFileName();
        //        //                //string tempPath = tempFile;
        //        //                ESignature signature;

        //        //                try
        //        //                {
        //        //                    if (Convert.ToBoolean(dtResult.Rows[index]["OKtoSign"]) == true)
        //        //                    {
        //        //                        int fileID = Convert.ToInt32(dtResult.Rows[index]["FileID"]);
        //        //                        string filePath = bus.FL_File_GetFilePathForSign(fileID);

        //        //                        //copy ra file tạm
        //        //                        //tempPath = tempFile + Path.GetExtension(filePath);
        //        //                        //File.Copy(filePath, tempPath, true);
        //        //                        byte[] fileData = File.ReadAllBytes(filePath);
        //        //                        string fileExtension = Path.GetExtension(filePath);

        //        //                        //byte[] ArrSigned = provider.SignData(Encoding.ASCII.GetBytes("RandomTextForTest"));
        //        //                        using (ESDigitalSignatureManager dsm = new ESDigitalSignatureManager(fileData, fileExtension))
        //        //                        {
        //        //                            //Kiểm tra chữ ký
        //        //                            //PA1: nếu đã có chữ ký của cert thì xóa và ký lại
        //        //                            //PA2 (chọn): nếu đã có chữ ký của cert thì ko ký và cập nhật chữ ký này vào db

        //        //                            dsm.Sign(cert, provider);
        //        //                            signature = dsm.Signatures[dsm.Signatures.Count - 1];

        //        //                            //Kiểm tra thời gian chờ trước khi cập nhật
        //        //                            if (bus.FL_File_SelectForSaveSign(Convert.ToInt32(dtResult.Rows[index]["ID_StatusLog"])))
        //        //                            {
        //        //                                // Lưu vào file đích
        //        //                                //File.Copy(tempPath, filePath, true);
        //        //                                File.WriteAllBytes(filePath, dsm.ToArray());

        //        //                                //Ghi log
        //        //                                bus.FL_File_UpdateForLogSign(fileID, signature.Signer.SerialNumber, signature.SigningTime, (int)signature.Verify,
        //        //                                    "", (int)FileSignActions.AddSignature, filePath, "Hoàn thành ký file qua HSM", programName, userName);

        //        //                                //Truyền transaction vào bus.FL_File_UpdateForLogSign
        //        //                                //File.Copy
        //        //                                //transaction.Commit()

        //        //                                //Báo thành công
        //        //                                dtResult.Rows[index]["SignResults"] = (int)FileSignResults.Success;
        //        //                                dtResult.Rows[index]["SignDetails"] = "Ký văn bản thành công.";

        //        //                                /*Cập nhật vào db của hệ thống riêng ở đây*/
        //        //                            }
        //        //                            else
        //        //                            {
        //        //                                //bus.FL_File_UpdateStatus(fileID, (int)FileStatus.LuuFile, "Hủy ký", programName, userName);
        //        //                                dtResult.Rows[index]["SignResults"] = (int)FileSignResults.SaveTimeOut;
        //        //                                dtResult.Rows[index]["SignDetails"] = "Ký văn bản thất bại: Hết thời gian chờ lưu file.";
        //        //                            }
        //        //                        }
        //        //                    }
        //        //                    else
        //        //                    {
        //        //                        //Nếu có 1 thằng không được ký (do 1 lỗi nào đó), trạng thái đã có sẵn, chỉ cần cập nhật lại kết quả chung thôi
        //        //                        ret = CAExitCode.Error;
        //        //                    }
        //        //                }
        //        //                catch (Exception ex)
        //        //                {
        //        //                    ret = GetErrorCodeFromString(ex.Message);

        //        //                    //bus.FL_File_UpdateStatus(fileID, (int)FileStatus.LuuFile, "Hủy ký", programName, userName);
        //        //                    dtResult.Rows[index]["SignResults"] = (int)FileSignResults.FileSignFailed;
        //        //                    dtResult.Rows[index]["SignDetails"] = "Ký văn bản thất bại: " + ex.Message;
        //        //                }
        //        //                //finally
        //        //                //{
        //        //                //    // Xóa các file tạm
        //        //                //    try
        //        //                //    {
        //        //                //        File.Delete(tempFile);
        //        //                //        File.Delete(tempPath);
        //        //                //    }
        //        //                //    catch { }
        //        //                //}
        //        //            }
        //        //        };
        //        //        bgw.RunWorkerCompleted += delegate
        //        //        {
        //        //            threadDone++;
        //        //            countFile++;

        //        //            if (countFile == filePerThread)
        //        //            {
        //        //                provider.Dispose();
        //        //            }
        //        //            //Đã ký hết các file
        //        //            if (threadDone == maxThread)
        //        //            {
        //        //                //Kết thúc
        //        //                HSMServiceProvider.Finalize();

        //        //                //export ra file text
        //        //                Process curProcess = Process.GetCurrentProcess();
        //        //                string directoryLog = Path.Combine(Application.StartupPath, "Result");
        //        //                string fileLog = Path.Combine(directoryLog, curProcess.Id.ToString() + ".txt");

        //        //                if (!Directory.Exists(directoryLog))
        //        //                    Directory.CreateDirectory(directoryLog);

        //        //                if (File.Exists(fileLog))
        //        //                    File.Delete(fileLog);

        //        //                File.Create(fileLog).Dispose();

        //        //                dtResult.WriteXml(fileLog, XmlWriteMode.WriteSchema);

        //        //                ExitWithCode(ret);
        //        //            }
        //        //            bgw.Dispose();
        //        //        };
        //        //        bgw.RunWorkerAsync();
        //        //        #endregion

        //        //        #region Minhdn 18/8/2015
        //        //        //for (int count = 0; count < filePerThread; count++)
        //        //        //{
        //        //        //    int index = filePerThread * j + count;
        //        //        //    if (index >= dtResult.Rows.Count) return;

        //        //        //    string tempFile = Path.GetTempFileName();
        //        //        //    string tempPath = tempFile;
        //        //        //    ESignature signature;

        //        //        //    try
        //        //        //    {
        //        //        //        if (Convert.ToBoolean(dtResult.Rows[index]["OKtoSign"]) == true)
        //        //        //        {
        //        //        //            int fileID = Convert.ToInt32(dtResult.Rows[index]["FileID"]);
        //        //        //            string filePath = bus.FL_File_GetFilePathForSign(fileID);
        //        //        //            tempPath = tempFile + Path.GetExtension(filePath);

        //        //        //            //copy ra file tạm
        //        //        //            File.Copy(filePath, tempPath, true);

        //        //        //            //byte[] ArrSigned = provider.SignData(Encoding.ASCII.GetBytes("RandomTextForTest"));
        //        //        //            using (ESDigitalSignatureManager dsm = new ESDigitalSignatureManager(tempPath))
        //        //        //            {
        //        //        //                //Kiểm tra chữ ký
        //        //        //                //PA1: nếu đã có chữ ký của cert thì xóa và ký lại
        //        //        //                //PA2 (chọn): nếu đã có chữ ký của cert thì ko ký và cập nhật chữ ký này vào db

        //        //        //                int curSignCount = dsm.Signatures.Count;
        //        //        //                dsm.Sign(cert, provider);

        //        //        //                using (ESDigitalSignatureManager dsm1 = new ESDigitalSignatureManager(tempPath))
        //        //        //                {
        //        //        //                    if (dsm1.Signatures.Count == curSignCount)
        //        //        //                    {
        //        //        //                        dtResult.Rows[index]["SignResults"] = (int)FileSignResults.FileSignFailed;
        //        //        //                        dtResult.Rows[index]["SignDetails"] = "Ký văn bản thất bại";
        //        //        //                        continue;
        //        //        //                    }
        //        //        //                    else
        //        //        //                    {
        //        //        //                        signature = dsm1.Signatures[curSignCount];
        //        //        //                    }
        //        //        //                }
        //        //        //            }

        //        //        //            if (bus.FL_File_SelectForSaveSign(Convert.ToInt32(dtResult.Rows[index]["ID_StatusLog"])))
        //        //        //            {
        //        //        //                //Copy vào file đích
        //        //        //                File.Copy(tempPath, filePath, true);

        //        //        //                //Ghi log
        //        //        //                bus.FL_File_UpdateForLogSign(fileID, signature.Signer.SerialNumber, signature.SigningTime, (int)signature.Verify,
        //        //        //                    signature.SignatureCreator, (int)FileSignActions.AddSignature, filePath, "Hoàn thành ký file qua HSM", programName, userName);

        //        //        //                //Truyền transaction vào bus.FL_File_UpdateForLogSign
        //        //        //                //File.Copy
        //        //        //                //transaction.Commit()

        //        //        //                //Báo thành công
        //        //        //                dtResult.Rows[index]["SignResults"] = (int)FileSignResults.Success;
        //        //        //                dtResult.Rows[index]["SignDetails"] = "Ký văn bản thành công.";

        //        //        //                /*Cập nhật vào db của hệ thống riêng ở đây*/
        //        //        //            }
        //        //        //            else
        //        //        //            {
        //        //        //                //Kiểm tra thời gian chờ trước khi cập nhật
        //        //        //                //bus.FL_File_UpdateStatus(fileID, (int)FileStatus.LuuFile, "Hủy ký", programName, userName);
        //        //        //                dtResult.Rows[index]["SignResults"] = (int)FileSignResults.SaveTimeOut;
        //        //        //                dtResult.Rows[index]["SignDetails"] = "Ký văn bản thất bại: Hết thời gian chờ lưu file.";
        //        //        //            }
        //        //        //        }
        //        //        //        else
        //        //        //        {
        //        //        //            //Nếu có 1 thằng không được ký (do 1 lỗi nào đó), trạng thái đã có sẵn, chỉ cần cập nhật lại kết quả chung thôi
        //        //        //            ret = CAExitCode.Error;
        //        //        //        }
        //        //        //    }
        //        //        //    catch (Exception ex)
        //        //        //    {
        //        //        //        ret = GetErrorCodeFromString(ex.Message);

        //        //        //        //bus.FL_File_UpdateStatus(fileID, (int)FileStatus.LuuFile, "Hủy ký", programName, userName);
        //        //        //        dtResult.Rows[index]["SignResults"] = (int)FileSignResults.FileSignFailed;
        //        //        //        dtResult.Rows[index]["SignDetails"] = "Ký văn bản thất bại: " + ex.Message;
        //        //        //    }
        //        //        //    finally
        //        //        //    {
        //        //        //        // Xóa các file tạm
        //        //        //        try
        //        //        //        {
        //        //        //            File.Delete(tempFile);
        //        //        //            File.Delete(tempPath);
        //        //        //        }
        //        //        //        catch { }
        //        //        //    }
        //        //        //}

        //        //        //threadDone++;
        //        //        //countFile++;

        //        //        //if (countFile == filePerThread)
        //        //        //{
        //        //        //    provider.Dispose();
        //        //        //}
        //        //        ////Đã ký hết các file
        //        //        //if (threadDone == maxThread)
        //        //        //{
        //        //        //    //Kết thúc
        //        //        //    HSMServiceProvider.Finalize();

        //        //        //    //export ra file text
        //        //        //    Process curProcess = Process.GetCurrentProcess();
        //        //        //    string directoryLog = Path.Combine(Application.StartupPath, "Result");
        //        //        //    string fileLog = Path.Combine(directoryLog, curProcess.Id.ToString() + ".txt");

        //        //        //    if (!Directory.Exists(directoryLog))
        //        //        //        Directory.CreateDirectory(directoryLog);

        //        //        //    if (File.Exists(fileLog))
        //        //        //        File.Delete(fileLog);

        //        //        //    File.Create(fileLog).Dispose();

        //        //        //    dtResult.WriteXml(fileLog, XmlWriteMode.WriteSchema);

        //        //        //    ExitWithCode(ret);
        //        //        //}
        //        //        #endregion
        //        //    }
        //        //    catch
        //        //    {
        //        //        //Lỗi thì vẫn chạy thread khác
        //        //    }
        //        //}
        //        #endregion
        //    }
        //    catch (Exception ex)
        //    {
        //        //Kết thúc
        //        strError = ex.Message;
        //        return (int)CAExitCode.Error;
        //    }
        //    finally
        //    {
        //        provider.Dispose();
        //    }
        //}

        [WebMethod(Description = "Ký văn bản qua client: lấy dữ liệu gửi về client.", MessageName = "3.SignFilesByID_SendToClient")]
        public bool SignFilesByID_SendToClient(string arrFileIDs, string programName, string userName,
            ref DataTable dtResult, ref string strError)
        {
            //Results
            Dictionary<int, byte[]> dicFileData = new Dictionary<int, byte[]>();
            dtResult = createTableSignResult();
            strError = "";

            try
            {
                if (arrFileIDs.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries).Length > 10)
                    throw new Exception("Cannot sign for more 10 files.");

                //CSDL
                //DAL_SqlConnector.ConnectionString = GetDatabaseString();
                BUSQuanTri bus = new BUSQuanTri();

                //Kiểm tra trạng thái file và thiết lập trạng thái chờ
                bool okToSign = bus.FL_File_SelectForAllowSign_Array(arrFileIDs, programName, userName, ref dtResult);

                if (okToSign)
                {
                    string fileExt = "";
                    //Lấy dữ liệu file
                    foreach (DataRow dr in dtResult.Rows)
                    {
                        if (Convert.ToBoolean(dr["OKtoSign"]))
                        {
                            int fileID = Convert.ToInt32(dr["FileID"]);
                            string filePath = dr["FilePath"].ToString();
                            if (fileExt == "")
                                fileExt = Path.GetExtension(filePath).ToLower();

                            dr["FileData"] = File.ReadAllBytes(filePath);
                        }
                    }

                    //Nếu có ít nhất 01 file được phép ký thì Trả về true kèm theo file extension
                    strError = fileExt;
                    return true;
                }
                else
                {
                    strError = "Các văn bản không được phép ký.";
                    return false;
                }
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return false;
            }
        }

        [WebMethod(Description = "Ký văn bản qua client: nhận dữ liệu và xác thực.", MessageName = "4.SignFilesByID_ReceiveFromClient")]
        public bool SignFilesByID_ReceiveFromClient(string programName, string userName,
            ref DataTable dtResult, ref string strError)
        {
            //Results
            bool ret = false;   //Trả về true nếu có ít nhất 01 file thành công
            //Toantk: không khởi tạo lại dtResult
            //dtResult = createTableSignResult();
            strError = "Tổng số văn bản: xx. Ký thành công: xx.";

            try
            {
                //CSDL
                //DAL_SqlConnector.ConnectionString = GetDatabaseString();
                BUSQuanTri bus = new BUSQuanTri();
                int soDuocKy = 0;
                int soThanhCong = 0;

                #region Duyệt và check từng file
                //Thông tin chứng thư. Chỉ cho phép 01 chứng thư ký
                X509Certificate2 certificate = null;
                Tuple<bool, CertificateStatus_CA, bool, CertificateStatus_TTD> cert_status = null;

                for (int i = 0; i < dtResult.Rows.Count; i++)
                {
                    try
                    {
                        if (Convert.ToBoolean(dtResult.Rows[i]["OKtoSign"]))
                        {
                            //Lấy thông tin
                            soDuocKy++;
                            int fileID = Convert.ToInt32(dtResult.Rows[i]["FileID"]);
                            int signType = Convert.ToInt32(dtResult.Rows[i]["QuyenUnit_Type"]);
                            int id_StatusLog = Convert.ToInt32(dtResult.Rows[i]["ID_StatusLog"]);

                            //Nếu đã có kết quả từ chương trình đẩy sang
                            if (dtResult.Rows[i]["SignResults"] != DBNull.Value)
                            {
                                goto LuuStatus;
                            }

                            //Lấy thông tin
                            string filePath = dtResult.Rows[i]["FilePath"].ToString();
                            byte[] fileHash = (byte[])dtResult.Rows[i]["FileHash"];
                            byte[] fileData = (byte[])dtResult.Rows[i]["FileData"];

                            using (ESDigitalSignatureManager dsm = new ESDigitalSignatureManager(fileData, Path.GetExtension(filePath)))
                            {
                                #region Kiểm tra file
                                //Toantk 13/9/2015: Tạm thời bỏ so sánh hash do chuỗi hash office qua applet ko khớp
                                //So sánh hash
                                if (!dsm.GetHashValue().SequenceEqual(fileHash))
                                {
                                    //Lưu bảng kết quả
                                    dtResult.Rows[i]["SignResults"] = (int)FileSignResults.HashNotMatch;
                                    dtResult.Rows[i]["SignDetails"] = "Nội dung văn bản đã bị thay đổi.";

                                    goto LuuStatus;
                                }

                                //Lấy chữ ký cuối cùng
                                if (dsm.Signatures.Count < 1)
                                {
                                    //Lưu bảng kết quả
                                    dtResult.Rows[i]["SignResults"] = (int)FileSignResults.NotSigned;
                                    dtResult.Rows[i]["SignDetails"] = "Không tìm thấy chữ ký.";

                                    goto LuuStatus;
                                }
                                ESignature signature = dsm.Signatures[dsm.Signatures.Count - 1];

                                //Kiểm tra có trùng chứng thư ko
                                for (int a = 0; a < dsm.Signatures.Count - 1; a++)
                                {
                                    if (dsm.Signatures[a].Signer.SerialNumber == signature.Signer.SerialNumber)
                                    {
                                        //Lưu bảng kết quả
                                        //Toantk 6/11/2015: sửa trả về thành công để có thể cập nhật db Thanh toán nhưng ko lưu file này
                                        //Sau này sửa kết quả = FileSignedByUnit
                                        dtResult.Rows[i]["SignResults"] = (int)FileSignResults.Success;
                                        dtResult.Rows[i]["SignDetails"] = "Chữ ký bởi người dùng đã có trên văn bản lúc " + dsm.Signatures[a].SigningTime.ToString("dd/MM/yyyy HH:mm:ss") + ".";

                                        //Cập nhật có xác nhận hay không
                                        dtResult.Rows[i]["XacNhan"] = bus.FL_FileType_QuyenXacNhan_CheckByFileID_CertID(fileID, dsm.Signatures[a].Signer.SerialNumber);

                                        // Chỉ cần có file thành công thì trả về true
                                        ret = true;
                                        soThanhCong++;

                                        goto LuuStatus;
                                    }
                                }

                                //Kiểm tra tính toàn vẹn chữ ký
                                if (signature.Verify != VerifyResult.Success)
                                {
                                    //Lưu bảng kết quả
                                    dtResult.Rows[i]["SignResults"] = (int)FileSignResults.InvalidSignature;
                                    dtResult.Rows[i]["SignDetails"] = "Chữ ký không hợp lệ: " + signature.Verify.ToString();

                                    goto LuuStatus;
                                }

                                //Lưu chứng thư ký
                                if (certificate == null)
                                {
                                    certificate = signature.Signer;
                                    ////Kiểm tra hiệu lực chứng thư với CA
                                    //CertificateStatus_CA statusCA;
                                    //bool bCA = this.ValidateCertificateInCA_Now(certificate.RawData, out statusCA);
                                    //Kiểm tra hiệu lực chứng thư trong TTĐ
                                    CertificateStatus_TTD statusTTD;
                                    bool bTTD = this.ValidateCertificateInTTD_Now(programName, userName, certificate.RawData, out statusTTD);
                                    //cert_status = Tuple.Create(bCA, statusCA, bTTD, statusTTD);
                                    cert_status = Tuple.Create(true, new CertificateStatus_CA(), bTTD, statusTTD);
                                }
                                //Nếu có chứng thư khác thì bắn lỗi
                                if (!signature.Signer.SerialNumber.Equals(certificate.SerialNumber))
                                {
                                    //Lưu kết quả tất cả bản ghi
                                    foreach (DataRow dr in dtResult.Rows)
                                        if (Convert.ToBoolean(dr["OKtoSign"]))
                                        {
                                            dr["SignResults"] = (int)FileSignResults.FileSignFailed;
                                            dr["SignDetails"] = "Chứng thư ký trong một phiên không khớp.";
                                        }
                                    //Chạy lại từ đầu
                                    i = 0;
                                    continue;
                                }
                                //Kiểm tra trạng thái
                                if (!cert_status.Item1)
                                {
                                    //Lưu bảng kết quả
                                    dtResult.Rows[i]["SignResults"] = (int)FileSignResults.InvalidCertificate_CA;
                                    dtResult.Rows[i]["SignDetails"] = "Xác thực chứng thư CA thất bại: " + cert_status.Item2.StatusInformation.ToString();

                                    goto LuuStatus;
                                }
                                if (!cert_status.Item3)
                                {
                                    //Lưu bảng kết quả
                                    dtResult.Rows[i]["SignResults"] = (int)FileSignResults.InvalidCertificate_TTD;
                                    dtResult.Rows[i]["SignDetails"] = "Xác thực chứng thư TTĐ thất bại: " + cert_status.Item4.StatusInformation.ToString();

                                    goto LuuStatus;
                                }
                                #endregion

                                #region Kiểm tra thời gian chờ và lưu
                                if (bus.FL_File_SelectForSaveSign(id_StatusLog))
                                {
                                    // Lưu vào file đích
                                    File.WriteAllBytes(filePath, dsm.ToArray());

                                    //Ghi log
                                    bus.FL_File_UpdateForLogSign(fileID, signature.Signer.SerialNumber, signature.SigningTime, (int)signature.Verify,
                                        signType, (int)FileSignActions.AddSignature, filePath, "Hoàn thành ký file qua web", programName, userName);

                                    //Báo thành công
                                    dtResult.Rows[i]["SignResults"] = (int)FileSignResults.Success;
                                    dtResult.Rows[i]["SignDetails"] = "Ký văn bản thành công.";

                                    //Cập nhật có xác nhận hay không
                                    dtResult.Rows[i]["XacNhan"] = bus.FL_FileType_QuyenXacNhan_CheckByFileID_CertID(fileID, signature.Signer.SerialNumber);

                                    // Chỉ cần có file thành công thì trả về true
                                    ret = true;
                                    soThanhCong++;
                                }
                                else
                                {
                                    dtResult.Rows[i]["SignResults"] = (int)FileSignResults.SaveTimeOut;
                                    dtResult.Rows[i]["SignDetails"] = "Ký văn bản không thành công: Hết thời gian chờ ký (" + dtResult.Rows[i]["WaitSaveOff"].ToString() + " giây).";
                                }
                                #endregion
                            }

                        LuuStatus:
                            FileSignResults results = (FileSignResults)Convert.ToInt32(dtResult.Rows[i]["SignResults"]);
                            if (results != FileSignResults.Success)
                            {
                                //Trả trạng thái nếu chưa hết thời gian chờ
                                if (bus.FL_File_SelectForSaveSign(id_StatusLog))
                                    bus.FL_File_UpdateStatus(fileID, (int)FileStatus.LuuFile, "Hủy ký do " + dtResult.Rows[i]["SignDetails"].ToString(),
                                        programName, userName);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        dtResult.Rows[i]["SignResults"] = (int)FileSignResults.FileSignFailed;
                        dtResult.Rows[i]["SignDetails"] = "Ký văn bản không thành công: " + ex.Message;
                    }
                    // Giải phóng dữ liệu truyền qua web serivce
                    dtResult.Rows[i]["FileHash"] = DBNull.Value;
                    dtResult.Rows[i]["FileData"] = DBNull.Value;
                }
                #endregion

                //Kết thúc
                strError = "Tổng số văn bản: " + dtResult.Rows.Count.ToString() + ". Được phép ký: " + soDuocKy.ToString()
                    + ". Ký thành công: " + soThanhCong.ToString() + ".";
                return ret;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return false;
            }
        }

        //Các hàm hỗ trợ ký file qua web-client
        [WebMethod(Description = "Ký bước 1: Kiểm tra trạng thái nhiều file có cho phép ký hay không và ghi log. Trả về thông tin file và ID_Log phiên ký.")]
        public bool FL_File_SelectForAllowSign_Array(string arrFileID, string programName, string userName, ref DataTable dtFileInfo)
        {
            DAL_SqlConnector.ConnectionString = GetDatabaseString();
            BUSQuanTri bus = new BUSQuanTri();

            return bus.FL_File_SelectForAllowSign_Array(arrFileID, programName, userName, ref dtFileInfo);
        }

        [WebMethod(Description = "Ký bước 2: Xét phiên log file xem có cho phép lưu file hay không.")]
        public bool FL_File_SelectForSaveSign(int id_StatusLog)
        {
            DAL_SqlConnector.ConnectionString = GetDatabaseString();
            BUSQuanTri bus = new BUSQuanTri();

            return bus.FL_File_SelectForSaveSign(id_StatusLog);
        }

        [WebMethod(Description = "Ký bước 3: Cập nhật log ký/xóa chữ ký và thiết lập trạng thái về LuuFile.")]
        public bool FL_File_UpdateForLogSign(int fileID, string certSerial, DateTime signTime, int verify, int signatureType,
            int fileSignActions, string backupPath, string reason, string programName, string userName)
        {
            DAL_SqlConnector.ConnectionString = GetDatabaseString();
            BUSQuanTri bus = new BUSQuanTri();

            bus.FL_File_UpdateForLogSign(fileID, certSerial, signTime, verify, signatureType, fileSignActions, backupPath, reason, programName, userName);
            return true;
        }

        //public int RemoveSignatureFileByID(string programName, string userName, int fileID, ref string strError)
        //{
        //    //Kiểm tra quyền xóa và lock phiên

        //    //Lấy file ở đường dẫn hiện tại chuyển qua back up

        //    //Xóa chữ ký

        //    //Cập nhật log ký vào db, cập nhật đường dẫn backup và đường dẫn chính

        //    //Results
        //    int ret = (int)CAExitCode.Error;
        //    DataTable dtResult = createTableSignResult();
        //    strError = "";

        //    try
        //    {
        //        ////Thư viện HSM
        //        //Common.CRYPTOKI = GetCryptokiDLL();
        //        //CSDL
        //        //DAL_SqlConnector.ConnectionString = GetDatabaseString();
        //        BUSQuanTri bus = new BUSQuanTri();
        //        //Tham số
        //        X509Certificate2 cert;  //Chứng thư ký

        //        //Kiểm tra quyền xóa và lock phiên
        //        //xin lệnh ký và thông tin file ký
        //        bool okToSign = bus.FL_File_SelectForRemoveSign(fileID, programName, userName, ref dtResult);
        //        dtResult.TableName = "RESULTS";

        //        if (okToSign == true)
        //        {
        //            // Lấy dữ liệu
        //            fileID = Convert.ToInt32(dtResult.Rows[0]["FileID"]);
        //            int signType = Convert.ToInt32(dtResult.Rows[0]["QuyenUnit_Type"]);
        //            string filePath = dtResult.Rows[0]["FilePath"].ToString();
        //            string filePath_Backup = dtResult.Rows[0]["FilePath_Backup"].ToString();
        //            byte[] fileData = File.ReadAllBytes(filePath);
        //            string fileExtension = Path.GetExtension(filePath);

        //            using (ESDigitalSignatureManager dsm = new ESDigitalSignatureManager(fileData, fileExtension))
        //            {
        //                //Lấy file ở đường dẫn hiện tại chuyển qua back up
        //                File.WriteAllBytes(filePath_Backup, dsm.ToArray());

        //                //Duyệt từng chữ ký và xóa
        //                foreach (DataRow dr in dtResult.Rows)
        //                {
        //                    X509Certificate2 certificate = new X509Certificate2((byte[])dr["RawData"]);
        //                    DateTime time = Convert.ToDateTime(dr["SigningTime"]);
        //                    VerifyResult result = (VerifyResult)Convert.ToInt32(dr["Verify"]);
        //                    ESignature sig = new ESignature(certificate, time, result);

        //                    dsm.RemoveSignature(sig);
        //                }

        //                //Lưu file, Cập nhật log ký vào db, cập nhật đường dẫn backup và đường dẫn chính
        //                //Kiểm tra thời gian chờ trước khi cập nhật
        //                if (bus.FL_File_SelectForSaveSign(Convert.ToInt32(dtResult.Rows[0]["ID_StatusLog"])))
        //                {
        //                    // Lưu vào file đích
        //                    File.WriteAllBytes(filePath, dsm.ToArray());

        //                    //Ghi log
        //                    bus.FL_File_UpdateForLogSign(fileID, signature.Signer.SerialNumber, signature.SigningTime, (int)signature.Verify,
        //                        signType, (int)FileSignActions.RemoveSignature, filePath, "Xóa chữ ký trên văn bản", programName, userName);

        //                    //Báo thành công
        //                    dtResult.Rows[0]["SignResults"] = (int)FileSignResults.Success;
        //                    dtResult.Rows[0]["SignDetails"] = "Xóa chữ ký thành công.";
        //                    // Chỉ cần có file thành công thì trả về true
        //                    ret = (int)CAExitCode.Success;
        //                }
        //                else
        //                {
        //                    dtResult.Rows[0]["SignResults"] = (int)FileSignResults.SaveTimeOut;
        //                    dtResult.Rows[0]["SignDetails"] = "Xóa chữ ký thất bại: Hết thời gian chờ (" + dtResult.Rows[0]["WaitSaveOff"].ToString() + " giây).";
        //                }
        //            }
        //        }

        //        //Kết thúc
        //        strError = "Tổng số văn bản: " + dtResult.Rows.Count.ToString() + ". Được phép ký: " + soDuocKy.ToString()
        //            + ". Ký thành công: " + soThanhCong.ToString() + ".";
        //        return ret;
        //    }
        //    catch (Exception ex)
        //    {
        //        //Kết thúc
        //        strError = ex.Message;
        //        return (int)CAExitCode.Error;
        //    }
        //}
        #endregion

        #region Private member

        private bool ValidateCertificateInVCGM(DataTable dtChain, DateTime verificationTime, out VCGMChainStatus certificateStatus)
        {
            certificateStatus = new VCGMChainStatus();

            try
            {
                //Chứng thư
                if (dtChain.Rows.Count == 0)
                {
                    certificateStatus.Status |= VCGMChainStatusFlags.CertificateNotFound;
                    certificateStatus.StatusInformation += "Không tìm thấy chứng thư.\r\n";
                    return false;
                }
                else
                {
                    if (Convert.ToBoolean(dtChain.Rows[0]["Cert_Status"]) == false)
                    {
                        certificateStatus.Status |= VCGMChainStatusFlags.CertificateNotValid;
                        certificateStatus.StatusInformation += "Trạng thái chứng thư không có hiệu lực.\r\n";
                    }
                    // Toantk 8/9/2015: Bỏ check thời hạn vì hàm ValidateCertificateInCA đã check rồi
                    //if (Convert.ToDateTime(dtChain.Rows[0]["Cert_ValidFrom"]) > verificationTime)
                    //{
                    //    certificateStatus.Status |= VCGMChainStatusFlags.CertificateNotIssue;
                    //    certificateStatus.StatusInformation += "Chứng thư chưa được cấp phát.\r\n";
                    //}
                    //if (dtChain.Rows[0]["Cert_ValidTo"] != DBNull.Value && Convert.ToDateTime(dtChain.Rows[0]["Cert_ValidTo"]) < verificationTime)
                    //{
                    //    certificateStatus.Status |= VCGMChainStatusFlags.CertificateExpired;
                    //    certificateStatus.StatusInformation += "Chứng thư hết hạn.\r\n";
                    //}
                }

                //Edited by Toantk on 10/7/2014
                //Bỏ trạng thái liên kết chứng thư - người dùng vì đã liên kết trực tiếp trong bảng User
                ////Liên kết cert - user
                //if (dtChain.Rows[0]["ID_CertUser"] == DBNull.Value)
                //{
                //    certificateStatus.Status |= VCGMChainStatusFlags.CertUserLinkNotFound;
                //    certificateStatus.StatusInformation += "Không tìm thấy liên kết chứng thư - người dùng.\r\n";
                //    return false;
                //}
                //else
                //{
                //    if (Convert.ToDateTime(dtChain.Rows[0]["CertUser_ValidFrom"]) > verificationTime)
                //    {
                //        certificateStatus.Status |= VCGMChainStatusFlags.CertUserLinkNotIssue;
                //        certificateStatus.StatusInformation += "Liên kết chứng thư - người dùng chưa được cấp phát.\r\n";
                //    }
                //    if (dtChain.Rows[0]["CertUser_ValidTo"] != DBNull.Value && Convert.ToDateTime(dtChain.Rows[0]["CertUser_ValidTo"]) < verificationTime)
                //    {
                //        certificateStatus.Status |= VCGMChainStatusFlags.CertUserLinkExpired;
                //        certificateStatus.StatusInformation += "Liên kết chứng thư - người dùng hết hạn.\r\n";
                //    }
                //}

                //User
                if (dtChain.Rows[0]["UserID"] == DBNull.Value)
                {
                    certificateStatus.Status |= VCGMChainStatusFlags.UserNotFound;
                    certificateStatus.StatusInformation += "Không tìm thấy người dùng.\r\n";
                    return false;
                }
                else
                {
                    if (Convert.ToBoolean(dtChain.Rows[0]["User_Status"]) == false)
                    {
                        certificateStatus.Status |= VCGMChainStatusFlags.UserNotValid;
                        certificateStatus.StatusInformation += "Trạng thái người dùng không có hiệu lực.\r\n";
                    }
                    if (dtChain.Rows[0]["User_ValidFrom"] != DBNull.Value && Convert.ToDateTime(dtChain.Rows[0]["User_ValidFrom"]) > verificationTime)
                    {
                        certificateStatus.Status |= VCGMChainStatusFlags.UserNotIssue;
                        certificateStatus.StatusInformation += "Người dùng chưa được cấp phát.\r\n";
                    }
                    if (dtChain.Rows[0]["User_ValidTo"] != DBNull.Value && Convert.ToDateTime(dtChain.Rows[0]["User_ValidTo"]) < verificationTime)
                    {
                        certificateStatus.Status |= VCGMChainStatusFlags.UserExpired;
                        certificateStatus.StatusInformation += "Người dùng hết hạn.\r\n";
                    }
                }

                //Liên kết prog - user
                if (dtChain.Rows[0]["ID_UserProg"] == DBNull.Value)
                {
                    certificateStatus.Status |= VCGMChainStatusFlags.UserProgLinkNotFound;
                    certificateStatus.StatusInformation += "Không tìm thấy phân quyền người dùng - chương trình.\r\n";
                    return false;
                }
                else
                {
                    if (Convert.ToDateTime(dtChain.Rows[0]["UserProg_ValidFrom"]) > verificationTime)
                    {
                        certificateStatus.Status |= VCGMChainStatusFlags.UserProgNotIssue;
                        certificateStatus.StatusInformation += "Phân quyền người dùng - chương trình chưa được cấp phát.\r\n";
                    }
                    if (dtChain.Rows[0]["UserProg_ValidTo"] != DBNull.Value && Convert.ToDateTime(dtChain.Rows[0]["UserProg_ValidTo"]) < verificationTime)
                    {
                        certificateStatus.Status |= VCGMChainStatusFlags.UserProgExpired;
                        certificateStatus.StatusInformation += "Phân quyền người dùng - chương trình hết hạn.\r\n";
                    }
                }
            }
            catch (Exception ex)
            {
                string str = ex.Message;
                certificateStatus.Status |= VCGMChainStatusFlags.StatusUnknown;
                certificateStatus.StatusInformation += "Không thể kiểm tra trạng thái do có lỗi.\r\n";
            }

            //Trả về
            if (certificateStatus.Status == VCGMChainStatusFlags.NoError)
                return true;
            else
                return false;
        }

        //private string GetCryptokiDLL()
        //{
        //    //string dllKey = Environment.Is64BitProcess ? "cryptoki64" : "cryptoki32";
        //    //string dllPath = Server.MapPath("~") + ConfigurationManager.ConnectionStrings[dllKey].ConnectionString;
        //    //return dllPath;
        //    return "cryptoki.dll";
        //}

        private string GetDatabaseString()
        {
            return ConfigurationManager.ConnectionStrings["database"].ConnectionString;
        }

        private DataTable createTableSignResult()
        {
            DataTable dt = new DataTable("SignResult");
            dt.Columns.Add("FileID", typeof(int));
            return dt;
        }

        private static void SaveLog(string logPath, string message)
        {
            using (StreamWriter file = new StreamWriter(logPath, true))
            {
                file.WriteLine("{0} - {1}", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss.sss"), message);
            }
        }

        static CAService()
        {
            string logPath = ConfigurationManager.ConnectionStrings["log_path"].ConnectionString;
            string cryptoki = ConfigurationManager.ConnectionStrings["cryptoki"].ConnectionString;
            string database = ConfigurationManager.ConnectionStrings["database"].ConnectionString;

            try
            {
                //Thư viện HSM
                Common.CRYPTOKI = cryptoki;
                //CSDL
                DAL_SqlConnector.ConnectionString = database;
                BUSQuanTri bus = new BUSQuanTri();

                ////Toantk 14/8/2015: set Temporary Environment Variable để thiết lập app HSM chạy ở chế độ NORMAL hay WLD
                string mode = bus.Q_CONFIG_GetHSM_MODE(); //Lấy từ Q_CONFIG
                Environment.SetEnvironmentVariable(Common.ET_PTKC_GENERAL_LIBRARY_MODE, mode, EnvironmentVariableTarget.Process);

                HSMServiceProvider.Finalize();
                HSMServiceProvider.Initialize(Common.CRYPTOKI);

                SaveLog(logPath, "Application pool started: Success.");
            }
            catch (Exception ex)
            {
                SaveLog(logPath, "Application pool started: " + ex.Message);
            }
        }
        #endregion

        #region Test function
        [WebMethod(Description = "Gọi hàm này ngay sau khi Recycle Application Pool của web service.")]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public string SaveFile_Test(string path)
        {
            try
            {
                //path=\\10.8.48.4\CAThiTruongDien\test_20160225.txt
                string text = "File này được lưu từ CAWebService để kiểm tra kết nối với thư mục.";
                File.WriteAllText(path, text, Encoding.UTF8);
                return "OK";
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        [WebMethod(Description = "Chỉ để test code: Kiểm tra có đăng nhập được HSM hay không.")]
        public string CheckLoginHSM_TestLoginFailed(string programName, string userName, string password)
        {
            ////Results
            //int ret;

            //// Use ProcessStartInfo class
            //ProcessStartInfo processInfo = new ProcessStartInfo();
            //processInfo.CreateNoWindow = false;
            //processInfo.UseShellExecute = false;
            //processInfo.FileName = Server.MapPath(@"~\CA_App\CA_AppService.exe");
            //processInfo.WindowStyle = ProcessWindowStyle.Hidden;
            //processInfo.Arguments = "-ac" + ((int)CACommand.CheckLoginHSM).ToString() + " -pn" + programName + " -up" + userName + " -pw" + password;

            //// Start the process with the info we specified.
            //// Call WaitForExit and then the using statement will close.
            //using (Process process = Process.Start(processInfo))
            //{
            //    process.WaitForExit();
            //    ret = process.ExitCode;
            //}

            //return ret;

            //mã trả về
            HSMReturnValue ret = HSMReturnValue.OK;

            try
            {
                ////Thư viện HSM
                //Common.CRYPTOKI = GetCryptokiDLL();
                //CSDL
                DAL_SqlConnector.ConnectionString = GetDatabaseString();
                BUSQuanTri bus = new BUSQuanTri();

                //Lấy thông tin certificate
                DataTable dtCert = bus.CA_Certificate_SelectForSign(programName, userName);
                X509Certificate2 cert = new X509Certificate2((byte[])dtCert.Rows[0]["RawData"]);
                int certID = Convert.ToInt32(dtCert.Rows[0]["CertID"]);

                ////Xác thực chứng thư với nhà cung cấp CA
                //X509ChainStatus certificateStatus;
                //if (!Common.ValidateCertificate(cert, DateTime.Now, out certificateStatus))
                //{
                //    return certificateStatus.StatusInformation;
                //}

                //Lấy key trong HSM
                //LƯU Ý: dùng hàm HSM_WLDObject_SelectPrivateKeyByCertID nếu chạy ở chế độ WLD
                DataTable dtPK = bus.HSM_Object_SelectPrivateKeyByCertID(Convert.ToInt32(dtCert.Rows[0]["CertID"]));
                string slotSerial = dtPK.Rows[0]["SlotSerial"].ToString();
                byte[] keyID = (byte[])dtPK.Rows[0]["CKA_ID"];

                //Khởi tạo giao tiếp HSM và đăng nhập
                using (HSMServiceProvider provider = new HSMServiceProvider())
                {
                    ret = provider.Login(slotSerial, HSMLoginRole.User, password);
                    provider.LoadPrivateKeyByID(keyID);
                    provider.Logout();
                }

                return ret.ToString();
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        #endregion

        #region Old
        //[WebMethod(Description = "Ký file trong hệ thống quản lý file. Ghi log ký file")]
        //public CAExitCode SignFileByID(int fileID, string programName, string userName, string password)
        //{
        //    //Results
        //    CAExitCode ret;

        //    // Use ProcessStartInfo class
        //    ProcessStartInfo processInfo = new ProcessStartInfo();
        //    processInfo.CreateNoWindow = true;
        //    processInfo.UseShellExecute = false;
        //    processInfo.WindowStyle = ProcessWindowStyle.Hidden;
        //    processInfo.FileName = Server.MapPath(@"~\CA_App\CA_AppService.exe"); //@"D:\Code\Visual Studio\CA_WebService\CA_AppService\bin\Debug\CA_AppService.exe";//
        //    //processInfo.FileName = @"D:\Code\Visual Studio\CA-VCGM\CA_WebService\CA_WebService\CA_App\CA_AppService.exe";
        //    processInfo.Arguments =
        //         "-ac" + ((int)CACommand.SignFileByID).ToString() +
        //        " -fi" + fileID.ToString() +
        //        " -pn" + programName +
        //        " -up" + userName +
        //        " -pw" + password;


        //    // Start the process with the info we specified.
        //    // Call WaitForExit and then the using statement will close.
        //    using (Process process = Process.Start(processInfo))
        //    {
        //        process.WaitForExit();
        //        try
        //        {
        //            ret = (CAExitCode)process.ExitCode;
        //        }
        //        catch
        //        {
        //            //loi khong xac dinh
        //            ret = CAExitCode.Error;
        //        }
        //    }

        //    return ret;

        //    #region Old
        //    ////Thư viện HSM
        //    //Common.CRYPTOKI = GetCryptokiDLL();
        //    ////CSDL
        //    //DAL_SqlConnector.ConnectionString = GetDatabaseString();
        //    //BUSQuanTri bus = new BUSQuanTri();

        //    ////Lấy đường dẫn file
        //    //string filePath = bus.FL_File_GetFilePathForSign(fileID);

        //    ////Lấy thông tin certificate
        //    //DataTable dtCert = bus.CA_Certificate_SelectForSign(programName, userName);
        //    //int certID = Convert.ToInt32(dtCert.Rows[0]["CertID"]);
        //    //X509Certificate2 cert = new X509Certificate2((byte[])dtCert.Rows[0]["RawData"]);

        //    ////Lấy key trong HSM
        //    ////LƯU Ý: dùng hàm HSM_WLDObject_SelectPrivateKeyByCertID nếu chạy ở chế độ WLD
        //    //DataTable dtPK = bus.HSM_Object_SelectPrivateKeyByCertID(Convert.ToInt32(dtCert.Rows[0]["CertID"]));
        //    //int slotID = Convert.ToInt32(dtPK.Rows[0]["SlotID"]);
        //    //byte[] keyID = (byte[])dtPK.Rows[0]["CKA_ID"];

        //    ////Khởi tạo giao tiếp HSM và đăng nhập
        //    //HSMServiceProvider provider = new HSMServiceProvider(Common.CRYPTOKI);
        //    //provider.Login(slotID, HSMLoginRole.User, password);

        //    ////Ký file
        //    //try
        //    //{
        //    //    //Khởi tạo private key dùng để ký
        //    //    provider.LoadPrivateKeyByID(keyID);

        //    //    using (ESDigitalSignatureManager dsm = new ESDigitalSignatureManager(filePath))
        //    //    {
        //    //        dsm.Sign(filePath, cert, provider);
        //    //        //Lấy chữ ký mới nhất
        //    //        ESignature signature = dsm.Signatures[dsm.Signatures.Count() - 1];
        //    //        //Ghi log
        //    //        bus.FL_LogFileSignature_Insert(fileID, cert.SerialNumber, signature.SigningTime, (int)signature.Verify, signature.SignatureCreator,
        //    //            (int)FileSignActions.ThemChuKy, programName, userName);
        //    //    }
        //    //}
        //    //finally
        //    //{
        //    //    //Đóng giao tiếp HSM
        //    //    provider.Logout();
        //    //    provider.Dispose();
        //    //}
        //    #endregion
        //}

        //[WebMethod(Description = "Ký nhiều file trong hệ thống quản lý file. Ghi log ký file", MessageName = "InputArray")]
        //public CAExitCode SignFilesByID(int[] fileIDs, string programName, string userName, string password)
        //{
        //    //Results
        //    CAExitCode ret;

        //    //Chuyển mảng FileID thành string
        //    string arrFileIDs = "";
        //    foreach (int fileID in fileIDs)
        //        arrFileIDs += fileID.ToString() + ";";

        //    // Use ProcessStartInfo class
        //    ProcessStartInfo processInfo = new ProcessStartInfo();
        //    processInfo.CreateNoWindow = false;
        //    processInfo.UseShellExecute = false;
        //    processInfo.FileName = Server.MapPath(@"~\CA_App\CA_AppService.exe");
        //    processInfo.WindowStyle = ProcessWindowStyle.Hidden;
        //    processInfo.Arguments = "-ac" + ((int)CACommand.SignFilesByIDs).ToString() + " -fl" + arrFileIDs
        //        + " -pn" + programName + " -up" + userName + " -pw" + password;

        //    // Start the process with the info we specified.
        //    // Call WaitForExit and then the using statement will close.
        //    using (Process process = Process.Start(processInfo))
        //    {
        //        process.WaitForExit();
        //        try
        //        {
        //            ret = (CAExitCode)process.ExitCode;
        //        }
        //        catch
        //        {
        //            //loi khong xac dinh
        //            ret = CAExitCode.Error;
        //        }
        //    }

        //    return ret;

        //    #region Old
        //    ////Thư viện HSM
        //    //Common.CRYPTOKI = GetCryptokiDLL();
        //    ////CSDL
        //    //DAL_SqlConnector.ConnectionString = GetDatabaseString();
        //    //BUSQuanTri bus = new BUSQuanTri();
        //    ////Kết quả
        //    //DataTable dtResults = new DataTable();
        //    //DataColumn dc = new DataColumn("FileID", typeof(int));
        //    //dtResults.Columns.Add(dc);
        //    //dc = new DataColumn("Result", typeof(string));
        //    //dtResults.Columns.Add(dc);

        //    ////Lấy đường dẫn file
        //    //Dictionary<int, string> filePaths = bus.FL_File_GetFilePaths(fileIDs);

        //    ////Lấy thông tin certificate
        //    //DataTable dtCert = bus.CA_Certificate_SelectForSign(programName, userName);
        //    //int certID = Convert.ToInt32(dtCert.Rows[0]["CertID"]);
        //    //X509Certificate2 cert = new X509Certificate2((byte[])dtCert.Rows[0]["RawData"]);

        //    ////Lấy key trong HSM
        //    ////LƯU Ý: dùng hàm HSM_WLDObject_SelectPrivateKeyByCertID nếu chạy ở chế độ WLD
        //    //DataTable dtPK = bus.HSM_Object_SelectPrivateKeyByCertID(Convert.ToInt32(dtCert.Rows[0]["CertID"]));
        //    //int slotID = Convert.ToInt32(dtPK.Rows[0]["SlotID"]);
        //    //byte[] keyID = (byte[])dtPK.Rows[0]["CKA_ID"];

        //    ////Khởi tạo giao tiếp HSM và đăng nhập
        //    //HSMServiceProvider provider = new HSMServiceProvider(Common.CRYPTOKI);
        //    //provider.Login(slotID, HSMLoginRole.User, password);
        //    ////Khởi tạo private key dùng để ký
        //    //provider.LoadPrivateKeyByID(keyID);

        //    ////Ký file
        //    //foreach (int fileID in fileIDs)
        //    //    try
        //    //    {
        //    //        string filePath = filePaths[fileID];
        //    //        if (filePath.Contains("Failed: "))
        //    //        {
        //    //            DataRow dr = dtResults.NewRow();
        //    //            dr["FileID"] = fileID; dr["Result"] = filePath;
        //    //            dtResults.Rows.Add(dr);
        //    //            break;
        //    //        }

        //    //        using (ESDigitalSignatureManager dsm = new ESDigitalSignatureManager(filePath))
        //    //        {
        //    //            dsm.Sign(filePath, cert, provider);
        //    //            //Lấy chữ ký mới nhất
        //    //            ESignature signature = dsm.Signatures[dsm.Signatures.Count() - 1];
        //    //            //Ghi log
        //    //            bus.FL_LogFileSignature_Insert(fileID, cert.SerialNumber, signature.SigningTime, (int)signature.Verify, signature.SignatureCreator,
        //    //                (int)FileSignActions.ThemChuKy, programName, userName);
        //    //        }

        //    //        DataRow dr1 = dtResults.NewRow();
        //    //        dr1["FileID"] = fileID; dr1["Result"] = "Succeed";
        //    //        dtResults.Rows.Add(dr1);
        //    //    }
        //    //    catch (Exception ex)
        //    //    {
        //    //        DataRow dr = dtResults.NewRow();
        //    //        dr["FileID"] = fileID; dr["Result"] = "Failed: " + ex.Message;
        //    //        dtResults.Rows.Add(dr);
        //    //    }

        //    ////Đóng giao tiếp HSM
        //    //provider.Logout();
        //    //provider.Dispose();

        //    //return dtResults;
        //    #endregion
        //}

        //[WebMethod(Description = "Ký nhiều file trong hệ thống quản lý file. Ghi log ký file", MessageName = "InputString")]
        //public CAExitCode SignFilesByID(string arrFileIDs, string programName, string userName, string password)
        //{
        //    //Results
        //    CAExitCode ret;

        //    // Use ProcessStartInfo class
        //    ProcessStartInfo processInfo = new ProcessStartInfo();
        //    processInfo.CreateNoWindow = false;
        //    processInfo.UseShellExecute = false;
        //    processInfo.FileName = Server.MapPath(@"~\CA_App\CA_AppService.exe");
        //    processInfo.WindowStyle = ProcessWindowStyle.Hidden;
        //    processInfo.Arguments = "-ac" + ((int)CACommand.SignFilesByIDs).ToString() + " -fl" + arrFileIDs
        //        + " -pn" + programName + " -up" + userName + " -pw" + password;

        //    // Start the process with the info we specified.
        //    // Call WaitForExit and then the using statement will close.
        //    using (Process process = Process.Start(processInfo))
        //    {
        //        process.WaitForExit();
        //        try
        //        {
        //            ret = (CAExitCode)process.ExitCode;
        //        }
        //        catch
        //        {
        //            //loi khong xac dinh
        //            ret = CAExitCode.Error;
        //        }
        //    }

        //    return ret;
        //}

        //[WebMethod(Description = "Ký nhiều file trong hệ thống quản lý file. Ghi log ký file. Trả về kết quả từng file", MessageName = "ReturnStringArray")]
        //public CAExitCode SignFilesByID_ReturnDetail1(string arrFileIDs, string programName, string userName, string password, ref List<string> lstResult)
        //{
        //    //Results
        //    CAExitCode ret = CAExitCode.None;
        //    List<string> lstTemp = new List<string> { };

        //    // Use ProcessStartInfo class
        //    ProcessStartInfo processInfo = new ProcessStartInfo();
        //    processInfo.CreateNoWindow = false;
        //    processInfo.UseShellExecute = false;
        //    processInfo.RedirectStandardOutput = true;
        //    processInfo.FileName = Server.MapPath(@"~\CA_App\CA_AppService.exe");
        //    processInfo.WindowStyle = ProcessWindowStyle.Hidden;
        //    processInfo.Arguments = "-ac" + ((int)CACommand.SignFilesByIDs).ToString() + " -fl" + arrFileIDs
        //        + " -pn" + programName + " -up" + userName + " -pw" + password + " -odTrue";

        //    // Start the process with the info we specified.
        //    // Call WaitForExit and then the using statement will close.
        //    lstResult.Clear();
        //    using (Process process = Process.Start(processInfo))
        //    {
        //        process.Start();

        //        process.OutputDataReceived += (s, e) =>
        //        {
        //            lock (lstTemp)
        //            {
        //                if (string.IsNullOrEmpty(e.Data) == false)
        //                {
        //                    lstTemp.Add(e.Data);
        //                }
        //            }
        //        };
        //        process.BeginOutputReadLine();
        //        process.WaitForExit();

        //        lstResult = lstTemp;

        //        //////process.WaitForExit();

        //        //////while (!process.StandardOutput.EndOfStream)
        //        //////{
        //        //////    string line = process.StandardOutput.ReadLine();
        //        //////    lstResult.Add(line);
        //        //////    // do something with line
        //        //////}

        //        //////process.StandardOutput.Close();

        //        //không lỗi
        //        if (ret == CAExitCode.None)
        //        {
        //            try
        //            {
        //                ret = (CAExitCode)process.ExitCode;
        //            }
        //            catch
        //            {
        //                //loi khong xac dinh
        //                ret = CAExitCode.Error;
        //            }
        //        }
        //    }

        //    return ret;
        //}

        //[WebMethod(Description = "Ký nhiều file trong hệ thống quản lý file. Ghi log ký file. Trả về kết quả từng file", MessageName = "ReturnClassSignFileResult")]
        //public CAExitCode SignFilesByID_ReturnDetail2(string arrFileIDs, string programName, string userName, string password, ref List<SignFileResult> lstResult)
        //{
        //    //Results
        //    CAExitCode ret = CAExitCode.None;
        //    List<SignFileResult> lstTemp = new List<SignFileResult> { };

        //    // Use ProcessStartInfo class
        //    ProcessStartInfo processInfo = new ProcessStartInfo();
        //    processInfo.CreateNoWindow = false;
        //    processInfo.UseShellExecute = false;
        //    processInfo.RedirectStandardOutput = true;
        //    processInfo.FileName = Server.MapPath(@"~\CA_App\CA_AppService.exe");
        //    processInfo.WindowStyle = ProcessWindowStyle.Hidden;
        //    processInfo.Arguments = "-ac" + ((int)CACommand.SignFilesByIDs).ToString() + " -fl" + arrFileIDs
        //        + " -pn" + programName + " -up" + userName + " -pw" + password + " -odTrue";

        //    // Start the process with the info we specified.
        //    // Call WaitForExit and then the using statement will close.
        //    lstResult.Clear();
        //    using (Process process = Process.Start(processInfo))
        //    {
        //        process.Start();

        //        process.OutputDataReceived += (s, e) =>
        //        {
        //            lock (lstTemp)
        //            {
        //                if (string.IsNullOrEmpty(e.Data) == false)
        //                {
        //                    string[] res = e.Data.Split(new string[] { CA_Variable.SignFile_Result_Separator }, StringSplitOptions.RemoveEmptyEntries);
        //                    if (res.Length == 2)
        //                    {
        //                        SignFileResult cs = new SignFileResult();
        //                        cs.FileID = Convert.ToInt16(res[0]);
        //                        cs.Result = res[1];
        //                        lstTemp.Add(cs);
        //                    }
        //                    else
        //                    {
        //                        ret = CAExitCode.WS_BadFormatOuputConsole;
        //                    }
        //                }
        //            }
        //        };
        //        process.BeginOutputReadLine();
        //        process.WaitForExit();

        //        lstResult = lstTemp;

        //        ////process.WaitForExit();

        //        ////while (!process.StandardOutput.EndOfStream)
        //        ////{
        //        ////    try
        //        ////    {
        //        ////        string line = process.StandardOutput.ReadLine();
        //        ////        string[] res = line.Split(new string[] { CA_Variable.SignFile_Result_Separator }, StringSplitOptions.RemoveEmptyEntries);
        //        ////        if (res.Length == 2)
        //        ////        {
        //        ////            SignFileResult cs = new SignFileResult();
        //        ////            cs.FileID = Convert.ToInt16(res[0]);
        //        ////            cs.Result = res[1];
        //        ////            lstResult.Add(cs);
        //        ////        }
        //        ////        else
        //        ////        {
        //        ////            ret = CAExitCode.WS_BadFormatOuputConsole;
        //        ////        }
        //        ////    }
        //        ////    catch (Exception ex)
        //        ////    {
        //        ////        ret = CAExitCode.WS_BadFormatOuputConsole;
        //        ////    }
        //        ////}

        //        ////process.StandardOutput.Close();

        //        //không lỗi
        //        if (ret == CAExitCode.None)
        //        {
        //            try
        //            {
        //                ret = (CAExitCode)process.ExitCode;
        //            }
        //            catch
        //            {
        //                //loi khong xac dinh
        //                ret = CAExitCode.Error;
        //            }
        //        }
        //    }

        //    return ret;
        //}

        //[WebMethod(Description = "Ký nhiều file trong hệ thống quản lý file. Ghi log ký file. Trả về kết quả từng file", MessageName = "1.ReturnDataTableNoneThread")]
        //public CAExitCode SignFilesByID_ReturnDetail3(string arrFileIDs, string programName, string userName, string password, ref DataTable dtResult)
        //{
        //    //Results
        //    CAExitCode ret = CAExitCode.None;
        //    DataTable dtTemp = createTableSignResult();

        //    // Use ProcessStartInfo class
        //    ProcessStartInfo processInfo = new ProcessStartInfo();
        //    processInfo.CreateNoWindow = false;
        //    processInfo.UseShellExecute = false;
        //    processInfo.RedirectStandardOutput = true;
        //    processInfo.FileName = Server.MapPath(@"~\CA_App\CA_AppService.exe");
        //    processInfo.WindowStyle = ProcessWindowStyle.Hidden;
        //    processInfo.Arguments = "-ac" + ((int)CACommand.SignFilesByIDs).ToString() + " -fl" + arrFileIDs
        //        + " -pn" + programName + " -up" + userName + " -pw" + password + " -odTrue";

        //    // Start the process with the info we specified.
        //    // Call WaitForExit and then the using statement will close.
        //    using (Process process = Process.Start(processInfo))
        //    {
        //        process.Start();

        //        process.OutputDataReceived += (s, e) =>
        //        {
        //            lock (dtTemp)
        //            {
        //                if (string.IsNullOrEmpty(e.Data) == false)
        //                {
        //                    string[] res = e.Data.Split(new string[] { CA_Variable.SignFile_Result_Separator }, StringSplitOptions.RemoveEmptyEntries);
        //                    if (res.Length == 2)
        //                    {
        //                        dtTemp.Rows.Add(res[0], res[1]);
        //                    }
        //                    else
        //                    {
        //                        ret = CAExitCode.WS_BadFormatOuputConsole;
        //                    }
        //                }
        //            }
        //        };
        //        process.BeginOutputReadLine();
        //        process.WaitForExit();

        //        dtResult = dtTemp;

        //        ////while (!process.StandardOutput.EndOfStream)
        //        ////{
        //        ////    try
        //        ////    {
        //        ////        string line = process.StandardOutput.ReadLine();
        //        ////        string[] res = line.Split(new string[] { CA_Variable.SignFile_Result_Separator }, StringSplitOptions.RemoveEmptyEntries);
        //        ////        if (res.Length == 2)
        //        ////        {
        //        ////            dtResult.Rows.Add(res[0], res[1]);
        //        ////        }
        //        ////        else
        //        ////        {
        //        ////            ret = CAExitCode.WS_BadFormatOuputConsole;
        //        ////        }
        //        ////    }
        //        ////    catch (Exception ex)
        //        ////    {
        //        ////        ret = CAExitCode.WS_BadFormatOuputConsole;
        //        ////    }
        //        ////}

        //        ////process.WaitForExit();

        //        ////process.StandardOutput.Close();

        //        //không lỗi
        //        if (ret == CAExitCode.None)
        //        {
        //            try
        //            {
        //                ret = (CAExitCode)process.ExitCode;
        //            }
        //            catch
        //            {
        //                //loi khong xac dinh
        //                ret = CAExitCode.Error;
        //            }
        //        }
        //    }

        //    return ret;
        //}

        //[WebMethod(Description = "Ký nhiều file trong hệ thống quản lý file. Ghi log ký file. Trả về kết quả từng file", MessageName = "2.ReturnDataTableNoneThread")]
        //public CAExitCode SignFilesByID_ReturnDetail3(int[] fileIDs, string programName, string userName, string password, ref DataTable dtResult)
        //{
        //    //Results
        //    CAExitCode ret = CAExitCode.None;
        //    DataTable dtTemp = createTableSignResult();

        //    //Chuyển mảng FileID thành string
        //    string arrFileIDs = "";
        //    foreach (int fileID in fileIDs)
        //        arrFileIDs += fileID.ToString() + ";";

        //    // Use ProcessStartInfo class
        //    ProcessStartInfo processInfo = new ProcessStartInfo();
        //    processInfo.CreateNoWindow = false;
        //    processInfo.UseShellExecute = false;
        //    processInfo.RedirectStandardOutput = true;
        //    processInfo.FileName = Server.MapPath(@"~\CA_App\CA_AppService.exe");
        //    processInfo.WindowStyle = ProcessWindowStyle.Hidden;
        //    processInfo.Arguments = "-ac" + ((int)CACommand.SignFilesByIDs).ToString() + " -fl" + arrFileIDs
        //        + " -pn" + programName + " -up" + userName + " -pw" + password + " -odTrue";

        //    // Start the process with the info we specified.
        //    // Call WaitForExit and then the using statement will close.
        //    using (Process process = Process.Start(processInfo))
        //    {
        //        process.Start();

        //        process.OutputDataReceived += (s, e) =>
        //        {
        //            lock (dtTemp)
        //            {
        //                if (string.IsNullOrEmpty(e.Data) == false)
        //                {
        //                    string[] res = e.Data.Split(new string[] { CA_Variable.SignFile_Result_Separator }, StringSplitOptions.RemoveEmptyEntries);
        //                    if (res.Length == 2)
        //                    {
        //                        dtTemp.Rows.Add(res[0], res[1]);
        //                    }
        //                    else
        //                    {
        //                        ret = CAExitCode.WS_BadFormatOuputConsole;
        //                    }
        //                }
        //            }
        //        };
        //        process.BeginOutputReadLine();
        //        process.WaitForExit();

        //        dtResult = dtTemp;

        //        //không lỗi
        //        if (ret == CAExitCode.None)
        //        {
        //            try
        //            {
        //                ret = (CAExitCode)process.ExitCode;
        //            }
        //            catch
        //            {
        //                //loi khong xac dinh
        //                ret = CAExitCode.Error;
        //            }
        //        }
        //    }

        //    return ret;
        //}

        //[WebMethod(Description = "Ký nhiều file trong hệ thống quản lý file. Ghi log ký file. Trả về kết quả từng file", MessageName = "1.ReturnDataTableNoneThreadWithTimeout")]
        //public CAExitCode SignFilesByID_ReturnDetail3(string arrFileIDs, string programName, string userName, string password, int timeOut, ref DataTable dtResult)
        //{
        //    //Results
        //    CAExitCode ret = CAExitCode.None;
        //    DataTable dtTemp = createTableSignResult();

        //    // Use ProcessStartInfo class
        //    ProcessStartInfo processInfo = new ProcessStartInfo();
        //    processInfo.CreateNoWindow = false;
        //    processInfo.UseShellExecute = false;
        //    processInfo.RedirectStandardOutput = true;
        //    processInfo.FileName = Server.MapPath(@"~\CA_App\CA_AppService.exe");
        //    processInfo.WindowStyle = ProcessWindowStyle.Hidden;
        //    processInfo.Arguments = "-ac" + ((int)CACommand.SignFilesByIDs).ToString() + " -fl" + arrFileIDs
        //        + " -pn" + programName + " -up" + userName + " -pw" + password + " -to" + timeOut.ToString() + " -odTrue";

        //    // Start the process with the info we specified.
        //    // Call WaitForExit and then the using statement will close.
        //    using (Process process = Process.Start(processInfo))
        //    {
        //        process.Start();

        //        process.OutputDataReceived += (s, e) =>
        //        {
        //            lock (dtTemp)
        //            {
        //                if (string.IsNullOrEmpty(e.Data) == false)
        //                {
        //                    string[] res = e.Data.Split(new string[] { CA_Variable.SignFile_Result_Separator }, StringSplitOptions.RemoveEmptyEntries);
        //                    if (res.Length == 2)
        //                    {
        //                        dtTemp.Rows.Add(res[0], res[1]);
        //                    }
        //                    else
        //                    {
        //                        ret = CAExitCode.WS_BadFormatOuputConsole;
        //                    }
        //                }
        //            }
        //        };
        //        process.BeginOutputReadLine();
        //        process.WaitForExit();

        //        dtResult = dtTemp;

        //        //không lỗi
        //        if (ret == CAExitCode.None)
        //        {
        //            try
        //            {
        //                ret = (CAExitCode)process.ExitCode;
        //            }
        //            catch
        //            {
        //                //loi khong xac dinh
        //                ret = CAExitCode.Error;
        //            }
        //        }
        //    }

        //    return ret;
        //}

        //[WebMethod(Description = "Ký nhiều file trong hệ thống quản lý file. Ghi log ký file. Trả về kết quả từng file", MessageName = "2.ReturnDataTableNoneThreadWithTimeout")]
        //public CAExitCode SignFilesByID_ReturnDetail3(int[] fileIDs, string programName, string userName, string password, int timeOut, ref DataTable dtResult)
        //{
        //    //Results
        //    CAExitCode ret = CAExitCode.None;
        //    DataTable dtTemp = createTableSignResult();

        //    //Chuyển mảng FileID thành string
        //    string arrFileIDs = "";
        //    foreach (int fileID in fileIDs)
        //        arrFileIDs += fileID.ToString() + ";";

        //    // Use ProcessStartInfo class
        //    ProcessStartInfo processInfo = new ProcessStartInfo();
        //    processInfo.CreateNoWindow = false;
        //    processInfo.UseShellExecute = false;
        //    processInfo.RedirectStandardOutput = true;
        //    processInfo.FileName = Server.MapPath(@"~\CA_App\CA_AppService.exe");
        //    processInfo.WindowStyle = ProcessWindowStyle.Hidden;
        //    processInfo.Arguments = "-ac" + ((int)CACommand.SignFilesByIDs).ToString() + " -fl" + arrFileIDs
        //        + " -pn" + programName + " -up" + userName + " -pw" + password + " -to" + timeOut.ToString() + " -odTrue";

        //    // Start the process with the info we specified.
        //    // Call WaitForExit and then the using statement will close.
        //    using (Process process = Process.Start(processInfo))
        //    {
        //        process.Start();

        //        process.OutputDataReceived += (s, e) =>
        //        {
        //            lock (dtTemp)
        //            {
        //                if (string.IsNullOrEmpty(e.Data) == false)
        //                {
        //                    string[] res = e.Data.Split(new string[] { CA_Variable.SignFile_Result_Separator }, StringSplitOptions.RemoveEmptyEntries);
        //                    if (res.Length == 2)
        //                    {
        //                        dtTemp.Rows.Add(res[0], res[1]);
        //                    }
        //                    else
        //                    {
        //                        ret = CAExitCode.WS_BadFormatOuputConsole;
        //                    }
        //                }
        //            }
        //        };
        //        process.BeginOutputReadLine();
        //        process.WaitForExit();

        //        dtResult = dtTemp;

        //        //không lỗi
        //        if (ret == CAExitCode.None)
        //        {
        //            try
        //            {
        //                ret = (CAExitCode)process.ExitCode;
        //            }
        //            catch
        //            {
        //                //loi khong xac dinh
        //                ret = CAExitCode.Error;
        //            }
        //        }
        //    }

        //    return ret;
        //}

        //[WebMethod(Description = "Ký nhiều file trong hệ thống quản lý file, sử dụng Thread. Ghi log ký file. Trả về kết quả từng file", MessageName = "1.ReturnDataTableUsingThread")]
        //public CAExitCode SignFilesByID_ReturnDetail4(string arrFileIDs, string programName, string userName, string password, ref DataTable dtResult)
        //{
        //    //Results
        //    CAExitCode ret = CAExitCode.None;
        //    DataTable dtTemp = createTableSignResult();

        //    // Use ProcessStartInfo class
        //    ProcessStartInfo processInfo = new ProcessStartInfo();
        //    processInfo.CreateNoWindow = false;
        //    processInfo.UseShellExecute = false;
        //    processInfo.RedirectStandardOutput = true;
        //    processInfo.FileName = Server.MapPath(@"~\CA_App\CA_AppService.exe");
        //    processInfo.WindowStyle = ProcessWindowStyle.Hidden;
        //    processInfo.Arguments = "-ac" + ((int)CACommand.SignFilesByID_Thread).ToString() + " -fl" + arrFileIDs
        //        + " -pn" + programName + " -up" + userName + " -pw" + password + " -odTrue";

        //    // Start the process with the info we specified.
        //    // Call WaitForExit and then the using statement will close.
        //    using (Process process = Process.Start(processInfo))
        //    {
        //        process.Start();

        //        process.OutputDataReceived += (s, e) =>
        //        {
        //            lock (dtTemp)
        //            {
        //                if (string.IsNullOrEmpty(e.Data) == false)
        //                {
        //                    string[] res = e.Data.Split(new string[] { CA_Variable.SignFile_Result_Separator }, StringSplitOptions.RemoveEmptyEntries);
        //                    if (res.Length == 2)
        //                    {
        //                        dtTemp.Rows.Add(res[0], res[1]);
        //                    }
        //                    else
        //                    {
        //                        ret = CAExitCode.WS_BadFormatOuputConsole;
        //                    }
        //                }
        //            }
        //        };
        //        process.BeginOutputReadLine();
        //        process.WaitForExit();

        //        dtResult = dtTemp;

        //        ////while (!process.StandardOutput.EndOfStream)
        //        ////{
        //        ////    try
        //        ////    {
        //        ////        string line = process.StandardOutput.ReadLine();
        //        ////        string[] res = line.Split(new string[] { CA_Variable.SignFile_Result_Separator }, StringSplitOptions.RemoveEmptyEntries);
        //        ////        if (res.Length == 2)
        //        ////        {
        //        ////            dtResult.Rows.Add(res[0], res[1]);
        //        ////        }
        //        ////        else
        //        ////        {
        //        ////            ret = CAExitCode.WS_BadFormatOuputConsole;
        //        ////        }
        //        ////    }
        //        ////    catch (Exception ex)
        //        ////    {
        //        ////        ret = CAExitCode.WS_BadFormatOuputConsole;
        //        ////    }
        //        ////}

        //        ////process.WaitForExit();

        //        ////process.StandardOutput.Close();

        //        //không lỗi
        //        if (ret == CAExitCode.None)
        //        {
        //            try
        //            {
        //                ret = (CAExitCode)process.ExitCode;
        //            }
        //            catch
        //            {
        //                //loi khong xac dinh
        //                ret = CAExitCode.Error;
        //            }
        //        }
        //    }

        //    return ret;
        //}

        //[WebMethod(Description = "Ký nhiều file trong hệ thống quản lý file, sử dụng Thread. Ghi log ký file. Trả về kết quả từng file", MessageName = "2.ReturnDataTableUsingThread")]
        //public CAExitCode SignFilesByID_ReturnDetail4(int[] fileIDs, string programName, string userName, string password, ref DataTable dtResult)
        //{
        //    //Results
        //    CAExitCode ret = CAExitCode.None;
        //    DataTable dtTemp = createTableSignResult();

        //    //Chuyển mảng FileID thành string
        //    string arrFileIDs = "";
        //    foreach (int fileID in fileIDs)
        //        arrFileIDs += fileID.ToString() + ";";

        //    // Use ProcessStartInfo class
        //    ProcessStartInfo processInfo = new ProcessStartInfo();
        //    processInfo.CreateNoWindow = false;
        //    processInfo.UseShellExecute = false;
        //    processInfo.RedirectStandardOutput = true;
        //    processInfo.FileName = Server.MapPath(@"~\CA_App\CA_AppService.exe");
        //    processInfo.WindowStyle = ProcessWindowStyle.Hidden;
        //    processInfo.Arguments = "-ac" + ((int)CACommand.SignFilesByID_Thread).ToString() + " -fl" + arrFileIDs
        //        + " -pn" + programName + " -up" + userName + " -pw" + password + " -odTrue";

        //    // Start the process with the info we specified.
        //    // Call WaitForExit and then the using statement will close.
        //    using (Process process = Process.Start(processInfo))
        //    {
        //        process.Start();

        //        process.OutputDataReceived += (s, e) =>
        //        {
        //            lock (dtTemp)
        //            {
        //                if (string.IsNullOrEmpty(e.Data) == false)
        //                {
        //                    string[] res = e.Data.Split(new string[] { CA_Variable.SignFile_Result_Separator }, StringSplitOptions.RemoveEmptyEntries);
        //                    if (res.Length == 2)
        //                    {
        //                        dtTemp.Rows.Add(res[0], res[1]);
        //                    }
        //                    else
        //                    {
        //                        ret = CAExitCode.WS_BadFormatOuputConsole;
        //                    }
        //                }
        //            }
        //        };
        //        process.BeginOutputReadLine();
        //        process.WaitForExit();

        //        dtResult = dtTemp;

        //        //không lỗi
        //        if (ret == CAExitCode.None)
        //        {
        //            try
        //            {
        //                ret = (CAExitCode)process.ExitCode;
        //            }
        //            catch
        //            {
        //                //loi khong xac dinh
        //                ret = CAExitCode.Error;
        //            }
        //        }
        //    }

        //    return ret;
        //}

        //[WebMethod(Description = "Ký nhiều file trong hệ thống quản lý file, sử dụng Thread. Ghi log ký file. Trả về kết quả từng file", MessageName = "1.ReturnDataTableUsingThreadWithTimeout")]
        //public CAExitCode SignFilesByID_ReturnDetail4(string arrFileIDs, string programName, string userName, string password, int timeOut, ref DataTable dtResult)
        //{
        //    //Results
        //    CAExitCode ret = CAExitCode.None;
        //    DataTable dtTemp = createTableSignResult();

        //    // Use ProcessStartInfo class
        //    ProcessStartInfo processInfo = new ProcessStartInfo();
        //    processInfo.CreateNoWindow = false;
        //    processInfo.UseShellExecute = false;
        //    processInfo.RedirectStandardOutput = true;
        //    processInfo.FileName = Server.MapPath(@"~\CA_App\CA_AppService.exe");
        //    processInfo.WindowStyle = ProcessWindowStyle.Hidden;
        //    processInfo.Arguments = "-ac" + ((int)CACommand.SignFilesByID_Thread).ToString() + " -fl" + arrFileIDs
        //        + " -pn" + programName + " -up" + userName + " -pw" + password + " -to" + timeOut.ToString() + " -odTrue";

        //    // Start the process with the info we specified.
        //    // Call WaitForExit and then the using statement will close.
        //    using (Process process = Process.Start(processInfo))
        //    {
        //        process.Start();

        //        process.OutputDataReceived += (s, e) =>
        //        {
        //            lock (dtTemp)
        //            {
        //                if (string.IsNullOrEmpty(e.Data) == false)
        //                {
        //                    string[] res = e.Data.Split(new string[] { CA_Variable.SignFile_Result_Separator }, StringSplitOptions.RemoveEmptyEntries);
        //                    if (res.Length == 2)
        //                    {
        //                        dtTemp.Rows.Add(res[0], res[1]);
        //                    }
        //                    else
        //                    {
        //                        ret = CAExitCode.WS_BadFormatOuputConsole;
        //                    }
        //                }
        //            }
        //        };
        //        process.BeginOutputReadLine();
        //        process.WaitForExit();

        //        dtResult = dtTemp;

        //        //không lỗi
        //        if (ret == CAExitCode.None)
        //        {
        //            try
        //            {
        //                ret = (CAExitCode)process.ExitCode;
        //            }
        //            catch
        //            {
        //                //loi khong xac dinh
        //                ret = CAExitCode.Error;
        //            }
        //        }
        //    }

        //    return ret;
        //}

        //[WebMethod(Description = "Ký nhiều file trong hệ thống quản lý file, sử dụng Thread. Ghi log ký file. Trả về kết quả từng file", MessageName = "2.ReturnDataTableUsingThreadWithTimeout")]
        //public CAExitCode SignFilesByID_ReturnDetail4(int[] fileIDs, string programName, string userName, string password, int timeOut, ref DataTable dtResult)
        //{
        //    //Results
        //    CAExitCode ret = CAExitCode.None;
        //    DataTable dtTemp = createTableSignResult();

        //    //Chuyển mảng FileID thành string
        //    string arrFileIDs = "";
        //    foreach (int fileID in fileIDs)
        //        arrFileIDs += fileID.ToString() + ";";

        //    // Use ProcessStartInfo class
        //    ProcessStartInfo processInfo = new ProcessStartInfo();
        //    processInfo.CreateNoWindow = false;
        //    processInfo.UseShellExecute = false;
        //    processInfo.RedirectStandardOutput = true;
        //    processInfo.FileName = Server.MapPath(@"~\CA_App\CA_AppService.exe");
        //    processInfo.WindowStyle = ProcessWindowStyle.Hidden;
        //    processInfo.Arguments = "-ac" + ((int)CACommand.SignFilesByID_Thread).ToString() + " -fl" + arrFileIDs
        //        + " -pn" + programName + " -up" + userName + " -pw" + password + " -to" + timeOut.ToString() + " -odTrue";

        //    // Start the process with the info we specified.
        //    // Call WaitForExit and then the using statement will close.
        //    using (Process process = Process.Start(processInfo))
        //    {
        //        process.Start();

        //        process.OutputDataReceived += (s, e) =>
        //        {
        //            lock (dtTemp)
        //            {
        //                if (string.IsNullOrEmpty(e.Data) == false)
        //                {
        //                    string[] res = e.Data.Split(new string[] { CA_Variable.SignFile_Result_Separator }, StringSplitOptions.RemoveEmptyEntries);
        //                    if (res.Length == 2)
        //                    {
        //                        dtTemp.Rows.Add(res[0], res[1]);
        //                    }
        //                    else
        //                    {
        //                        ret = CAExitCode.WS_BadFormatOuputConsole;
        //                    }
        //                }
        //            }
        //        };
        //        process.BeginOutputReadLine();
        //        process.WaitForExit();

        //        dtResult = dtTemp;

        //        //không lỗi
        //        if (ret == CAExitCode.None)
        //        {
        //            try
        //            {
        //                ret = (CAExitCode)process.ExitCode;
        //            }
        //            catch
        //            {
        //                //loi khong xac dinh
        //                ret = CAExitCode.Error;
        //            }
        //        }
        //    }

        //    return ret;
        //}

        //[WebMethod(Description = "Ký nhiều file trong hệ thống quản lý file, sử dụng Thread. Ghi log ký file. Trả về kết quả từng file vào file text", MessageName = "1.ReturnDataTableUsingThread_TextLog")]
        //public CAExitCode SignFilesByID_ReturnDetail5(int[] fileIDs, string programName, string userName, string password, ref DataTable dtResult)
        //{
        //    //Results
        //    CAExitCode ret = CAExitCode.None;
        //    dtResult.TableName = "RESULTS";

        //    //Chuyển mảng FileID thành string
        //    string arrFileIDs = "";
        //    foreach (int fileID in fileIDs)
        //        arrFileIDs += fileID.ToString() + ";";

        //    // Use ProcessStartInfo class
        //    ProcessStartInfo processInfo = new ProcessStartInfo();
        //    processInfo.CreateNoWindow = false;
        //    processInfo.UseShellExecute = false;
        //    processInfo.RedirectStandardOutput = true;
        //    processInfo.FileName = Server.MapPath(@"~\CA_App\CA_AppService.exe");
        //    processInfo.WindowStyle = ProcessWindowStyle.Hidden;
        //    processInfo.Arguments = "-ac" + ((int)CACommand.SignFilesByID_LogFile).ToString() + " -fl" + arrFileIDs
        //        + " -pn" + programName + " -up" + userName + " -pw" + password;

        //    // Start the process with the info we specified.
        //    // Call WaitForExit and then the using statement will close.
        //    using (Process process = Process.Start(processInfo))
        //    {
        //        process.WaitForExit();

        //        //file log
        //        string fileResult = Server.MapPath(@"~\CA_App\Result\" + process.Id.ToString() + ".txt");
        //        if (File.Exists(fileResult))
        //        {
        //            dtResult.ReadXml(fileResult);
        //            File.Delete(fileResult);
        //        }

        //        //không lỗi
        //        if (ret == CAExitCode.None)
        //        {
        //            try
        //            {
        //                ret = (CAExitCode)process.ExitCode;
        //            }
        //            catch
        //            {
        //                //loi khong xac dinh
        //                ret = CAExitCode.Error;
        //            }
        //        }
        //    }

        //    return ret;
        //}

        //[WebMethod(Description = "Ký nhiều file trong hệ thống quản lý file, sử dụng Thread. Ghi log ký file. Trả về kết quả từng file vào file text", MessageName = "2.ReturnDataTableUsingThread_TextLog")]
        //public CAExitCode SignFilesByID_ReturnDetail5(string arrFileIDs, string programName, string userName, string password, ref DataTable dtResult)
        //{
        //    //Results
        //    CAExitCode ret = CAExitCode.None;
        //    dtResult.TableName = "RESULTS";

        //    // Use ProcessStartInfo class
        //    //using (new Impersonation("192.168.0.48", "administrator", "es@123"))
        //    //{
        //    ProcessStartInfo processInfo = new ProcessStartInfo();
        //    processInfo.CreateNoWindow = false;
        //    processInfo.UseShellExecute = false;
        //    processInfo.RedirectStandardOutput = true;
        //    processInfo.FileName = Server.MapPath(@"~\CA_App\CA_AppService.exe");
        //    processInfo.WindowStyle = ProcessWindowStyle.Hidden;
        //    processInfo.Arguments = "-ac" + ((int)CACommand.SignFilesByID_LogFile).ToString() + " -fl" + arrFileIDs
        //        + " -pn" + programName + " -up" + userName + " -pw" + password;

        //    // Start the process with the info we specified.
        //    // Call WaitForExit and then the using statement will close.
        //    using (Process process = Process.Start(processInfo))
        //    {
        //        process.WaitForExit();

        //        //file log
        //        string fileResult = Server.MapPath(@"~\CA_App\Result\" + process.Id.ToString() + ".txt");
        //        if (File.Exists(fileResult))
        //        {
        //            dtResult.ReadXml(fileResult);
        //            File.Delete(fileResult);
        //        }

        //        //không lỗi
        //        if (ret == CAExitCode.None)
        //        {
        //            try
        //            {
        //                ret = (CAExitCode)process.ExitCode;
        //            }
        //            catch
        //            {
        //                //loi khong xac dinh
        //                ret = CAExitCode.Error;
        //            }
        //        }

        //        writeLog("Process return code is " + process.ExitCode.ToString());
        //    }
        //    //}

        //    return ret;
        //}

        //[WebMethod(Description = "Ký nhiều file trong hệ thống quản lý file, sử dụng Thread. Ghi log ký file. Trả về kết quả từng file vào file text", MessageName = "1.ReturnDataTableUsingThreadWithTimeout_TextLog")]
        //public CAExitCode SignFilesByID_ReturnDetail5(int[] fileIDs, string programName, string userName, string password, int timeOut, ref DataTable dtResult)
        //{
        //    //Results
        //    CAExitCode ret = CAExitCode.None;
        //    dtResult.TableName = "RESULTS";

        //    //Chuyển mảng FileID thành string
        //    string arrFileIDs = "";
        //    foreach (int fileID in fileIDs)
        //        arrFileIDs += fileID.ToString() + ";";

        //    // Use ProcessStartInfo class
        //    ProcessStartInfo processInfo = new ProcessStartInfo();
        //    processInfo.CreateNoWindow = false;
        //    processInfo.UseShellExecute = false;
        //    processInfo.RedirectStandardOutput = true;
        //    processInfo.FileName = Server.MapPath(@"~\CA_App\CA_AppService.exe");
        //    processInfo.WindowStyle = ProcessWindowStyle.Hidden;
        //    processInfo.Arguments = "-ac" + ((int)CACommand.SignFilesByID_LogFile).ToString() + " -fl" + arrFileIDs
        //        + " -pn" + programName + " -up" + userName + " -pw" + password + " -to" + timeOut.ToString();

        //    // Start the process with the info we specified.
        //    // Call WaitForExit and then the using statement will close.
        //    using (Process process = Process.Start(processInfo))
        //    {
        //        process.WaitForExit();

        //        //file log
        //        string fileResult = Server.MapPath(@"~\CA_App\Result\" + process.Id.ToString() + ".txt");
        //        if (File.Exists(fileResult))
        //        {
        //            dtResult.ReadXml(fileResult);
        //            File.Delete(fileResult);
        //        }

        //        //không lỗi
        //        if (ret == CAExitCode.None)
        //        {
        //            try
        //            {
        //                ret = (CAExitCode)process.ExitCode;
        //            }
        //            catch
        //            {
        //                //loi khong xac dinh
        //                ret = CAExitCode.Error;
        //            }
        //        }
        //    }

        //    return ret;
        //}

        //[WebMethod(Description = "Ký nhiều file trong hệ thống quản lý file, sử dụng Thread. Ghi log ký file. Trả về kết quả từng file vào file text", MessageName = "2.ReturnDataTableUsingThreadWithTimeout_TextLog")]
        //public CAExitCode SignFilesByID_ReturnDetail5(string arrFileIDs, string programName, string userName, string password, int timeOut, ref DataTable dtResult)
        //{
        //    //Results
        //    CAExitCode ret = CAExitCode.None;
        //    dtResult.TableName = "RESULTS";

        //    // Use ProcessStartInfo class
        //    ProcessStartInfo processInfo = new ProcessStartInfo();
        //    processInfo.CreateNoWindow = false;
        //    processInfo.UseShellExecute = false;
        //    processInfo.RedirectStandardOutput = true;
        //    processInfo.FileName = Server.MapPath(@"~\CA_App\CA_AppService.exe");
        //    processInfo.WindowStyle = ProcessWindowStyle.Hidden;
        //    processInfo.Arguments = "-ac" + ((int)CACommand.SignFilesByID_LogFile).ToString() + " -fl" + arrFileIDs
        //        + " -pn" + programName + " -up" + userName + " -pw" + password + " -to" + timeOut.ToString();

        //    // Start the process with the info we specified.
        //    // Call WaitForExit and then the using statement will close.
        //    using (Process process = Process.Start(processInfo))
        //    {
        //        process.WaitForExit();

        //        //file log
        //        string fileResult = Server.MapPath(@"~\CA_App\Result\" + process.Id.ToString() + ".txt");
        //        if (File.Exists(fileResult))
        //        {
        //            dtResult.ReadXml(fileResult);
        //            File.Delete(fileResult);
        //        }

        //        //không lỗi
        //        if (ret == CAExitCode.None)
        //        {
        //            try
        //            {
        //                ret = (CAExitCode)process.ExitCode;
        //            }
        //            catch
        //            {
        //                //loi khong xac dinh
        //                ret = CAExitCode.Error;
        //            }
        //        }
        //    }

        //    return ret;
        //}

        //[WebMethod(Description = "Ký bước 1: Kiểm tra trạng thái file có cho phép ký hay không và ghi log. Trả về thông tin file và ID_Log phiên ký.")]
        //public bool FL_File_SelectForAllowSign(int fileID, string programName, string userName, out DataTable dtFileInfo)
        //{
        //    DAL_SqlConnector.ConnectionString = GetDatabaseString();
        //    BUSQuanTri bus = new BUSQuanTri();

        //    return bus.FL_File_SelectForAllowSign(fileID, programName, userName, out dtFileInfo);
        //}

        //[WebMethod(Description = "Ký file ở client thông qua chuỗi base64.")]
        //public string SignFileByBase64(string fileType, string base64, string userProg, int progID, string password)
        //{
        //    //Thư viện HSM
        //    Common.CRYPTOKI = GetCryptokiDLL();
        //    //CSDL
        //    DAL_SqlConnector.ConnectionString = GetDatabaseString();
        //    BUSQuanTri bus = new BUSQuanTri();

        //    //Tạo file tạm
        //    string tempPath = Path.GetTempFileName() + fileType;
        //    Common.ConvertBase64ToFile(base64, tempPath);

        //    //Lấy Certificate
        //    DataTable dtCert = bus.WS_Certificate_SelectByUserProg(progID, userProg);
        //    if (dtCert.Rows.Count < 1)
        //        throw new Exception("WS_CertificateNotFoundOrInvalid");
        //    if (dtCert.Rows.Count > 1)
        //        throw new Exception("WS_CertificateMoreThanOne");
        //    X509Certificate2 cert = new X509Certificate2((byte[])dtCert.Rows[0]["RawData"]);

        //    //Lấy thông tin private key
        //    DataTable dtPK = bus.WS_HSM_Object_SelectPrivateKeyByCertID(Convert.ToInt32(dtCert.Rows[0]["CertID"]));
        //    if (dtPK.Rows.Count < 1)
        //        throw new Exception("WS_PrivateKeyNotFoundOrInvalid");
        //    if (dtPK.Rows.Count > 1)
        //        throw new Exception("WS_PrivateKeyMoreThanOne");
        //    int slotID = Convert.ToInt32(dtPK.Rows[0]["SlotID"]);
        //    string userPIN = dtPK.Rows[0]["User_PIN"].ToString();
        //    string keyLabel = dtPK.Rows[0]["Label"].ToString();

        //    try
        //    {
        //        //Khởi tạo giao tiếp HSM và đăng nhập
        //        HSMServiceProvider provider = new HSMServiceProvider(Common.CRYPTOKI);
        //        provider.Login(slotID, HSMLoginRole.User, userPIN);
        //        //Khởi tạo private key dùng để ký
        //        provider.LoadPrivateKeyByLABEL(keyLabel);

        //        using (ESDigitalSignatureManager dsm = new ESDigitalSignatureManager(tempPath))
        //        {
        //            dsm.Sign(tempPath, cert, provider);
        //        }

        //        //Đóng giao tiếp HSM
        //        provider.Logout();
        //        provider.Dispose();

        //        return Common.ConvertFileToBase64(tempPath);
        //    }
        //    finally
        //    {
        //        File.Delete(tempPath);
        //    }
        //}

        //[WebMethod(Description = "Ký file ở server thông qua file path.")]
        //public bool SignFileByLink(string sourcePath, int progID, string userProg, string password)
        //{
        //    //Thư viện HSM
        //    Common.CRYPTOKI = GetCryptokiDLL();
        //    //CSDL
        //    DAL_SqlConnector.ConnectionString = GetDatabaseString();
        //    BUSQuanTri bus = new BUSQuanTri();

        //    //Lấy Certificate
        //    DataTable dtCert = bus.WS_Certificate_SelectByUserProg(progID, userProg);
        //    if (dtCert.Rows.Count < 1)
        //        throw new Exception("WS_CertificateNotFoundOrInvalid");
        //    if (dtCert.Rows.Count > 1)
        //        throw new Exception("WS_CertificateMoreThanOne");
        //    X509Certificate2 cert = new X509Certificate2((byte[])dtCert.Rows[0]["RawData"]);

        //    //Lấy thông tin private key
        //    DataTable dtPK = bus.WS_HSM_Object_SelectPrivateKeyByCertID(Convert.ToInt32(dtCert.Rows[0]["CertID"]));
        //    if (dtPK.Rows.Count < 1)
        //        throw new Exception("WS_PrivateKeyNotFoundOrInvalid");
        //    if (dtPK.Rows.Count > 1)
        //        throw new Exception("WS_PrivateKeyMoreThanOne");
        //    int slotID = Convert.ToInt32(dtPK.Rows[0]["SlotID"]);
        //    string userPIN = dtPK.Rows[0]["User_PIN"].ToString();
        //    string keyLabel = dtPK.Rows[0]["Label"].ToString();

        //    //Xác đinh loại file
        //    sourcePath = Server.MapPath(sourcePath);

        //    try
        //    {
        //        //Khởi tạo giao tiếp HSM và đăng nhập
        //        HSMServiceProvider provider = new HSMServiceProvider(Common.CRYPTOKI);
        //        provider.Login(slotID, HSMLoginRole.User, userPIN);
        //        //Khởi tạo private key dùng để ký
        //        provider.LoadPrivateKeyByLABEL(keyLabel);

        //        using (ESDigitalSignatureManager dsm = new ESDigitalSignatureManager(File.ReadAllBytes(sourcePath), Path.GetExtension(sourcePath)))
        //        {
        //            dsm.Sign(cert, provider);
        //        }

        //        //Đóng giao tiếp HSM
        //        provider.Logout();
        //        provider.Dispose();

        //        return true;
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //}

        //[WebMethod(Description = "Ký nhiều file ở server thông qua danh sách file path.")]
        //public string SignFilesByLink(List<string> aSourcePath, List<string> aDestinationPath, string userProg, int progID)
        //{
        //    string result = "";
        //    //Thư viện HSM
        //    Common.CRYPTOKI = GetCryptokiDLL();
        //    //CSDL
        //    DAL_SqlConnector.ConnectionString = GetDatabaseString();
        //    DALQuanTri dal = new DALQuanTri();

        //    //Lấy Certificate
        //    DataTable dtCert = dal.WS_Certificate_SelectByUserProg(progID, userProg);
        //    if (dtCert.Rows.Count < 1)
        //        throw new Exception("WS_CertificateNotFoundOrInvalid");
        //    if (dtCert.Rows.Count > 1)
        //        throw new Exception("WS_CertificateMoreThanOne");
        //    X509Certificate2 cert = new X509Certificate2((byte[])dtCert.Rows[0]["RawData"]);

        //    //Lấy thông tin private key
        //    DataTable dtPK = dal.WS_HSM_Object_SelectPrivateKeyByCertID(Convert.ToInt32(dtCert.Rows[0]["CertID"]));
        //    if (dtPK.Rows.Count < 1)
        //        throw new Exception("WS_PrivateKeyNotFoundOrInvalid");
        //    if (dtPK.Rows.Count > 1)
        //        throw new Exception("WS_PrivateKeyMoreThanOne");
        //    int slotID = Convert.ToInt32(dtPK.Rows[0]["SlotID"]);
        //    string userPIN = dtPK.Rows[0]["User_PIN"].ToString();
        //    string keyLabel = dtPK.Rows[0]["Label"].ToString();

        //    //kiểm tra Số lượng mảng đầu vào và đầu ra
        //    if (aSourcePath.Count != aDestinationPath.Count)
        //        throw new Exception("NumberOfFileUnbalanced");

        //    //Khởi tạo giao tiếp HSM và đăng nhập
        //    HSMServiceProvider provider = new HSMServiceProvider(Common.CRYPTOKI);
        //    provider.Login(slotID, HSMLoginRole.User, userPIN);
        //    //Khởi tạo private key dùng để ký
        //    provider.LoadPrivateKeyByLABEL(keyLabel);

        //    for (int i = 0; i < aSourcePath.Count; i++)
        //    {
        //        //Kiểm tra tên file
        //        if (Path.GetFileName(aSourcePath[i]) != Path.GetFileName(aDestinationPath[i]))
        //        {
        //            result += "Error file " + aSourcePath[i] + "\n";
        //            continue;
        //        }

        //        //Xác đinh loại file
        //        string fileType = Path.GetExtension(aSourcePath[i]).ToLower();
        //        string sourcePath = Server.MapPath(aSourcePath[i]);
        //        string destinationPath = Server.MapPath(aDestinationPath[i]);

        //        try
        //        {
        //            using (ESDigitalSignatureManager dsm = new ESDigitalSignatureManager(sourcePath))
        //            {
        //                dsm.Sign(destinationPath, cert, provider);
        //            }
        //        }
        //        catch
        //        {
        //            result += "Error file " + aSourcePath[i] + "\n";
        //        }
        //    }

        //    //Đóng giao tiếp HSM
        //    provider.Logout();
        //    provider.Dispose();

        //    return result;
        //}
        #endregion
    }
}

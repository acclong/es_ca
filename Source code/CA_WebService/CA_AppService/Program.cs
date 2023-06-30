using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;
using System.Security.Cryptography.X509Certificates;
using System.ComponentModel;
using System.Configuration;
using ES.CA_WebServiceBUS;
using ES.CA_WebServiceDAL;
using esDigitalSignature;
using System.Windows.Forms;

namespace CA_AppService
{
    class Program
    {
        [STAThread]
        static void Main()
        {
            //Toantk: chạy HSM ở chế độ NORMAL để đổi mã PIN
            Environment.SetEnvironmentVariable(Common.ET_PTKC_GENERAL_LIBRARY_MODE, Common.NORMAL, EnvironmentVariableTarget.Process);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new frmMain());
            Application.Run(new frmTest());
        }

        //#region Arguments: các biến để truyền vào từ tham số của app
        ///// <summary>
        ///// Lệnh
        ///// </summary>
        //static CACommand a_Action = CACommand.None;
        ///// <summary>
        ///// FileID trong hệ thống CA
        ///// </summary>
        //static int a_fileID = -1;
        ///// <summary>
        ///// Danh sách FileID, phân cách bởi ký tự ';'
        ///// </summary>
        //static string a_fileIDs = "-1";
        ///// <summary>
        ///// Tên chương trình gọi lệnh
        ///// </summary>
        //static string a_progName = string.Empty;
        ///// <summary>
        ///// Tên người dùng trong chương trình gọi lệnh
        ///// </summary>
        //static string a_userProgName = string.Empty;
        ///// <summary>
        ///// Mật khẩu slot HSM tương ứng với người dùng
        ///// </summary>
        //static string a_passwordHSM = string.Empty;
        //#endregion

        //static void Main(string[] args)
        //{
        //    //System.Windows.Forms.MessageBox.Show("Anh bắt đầu đây các chú");
        //    //return;

        //    ProcessArguments(args);
        //}

        ///// <summary>
        ///// Xử lý tham số truyền vào
        ///// </summary>
        ///// <param name="args"></param>
        //private static void ProcessArguments(string[] args)
        //{
        //    /* xử lý tham số truyền vào ở đây
        //     * quy định tên argument sẽ là:
        //     * -ac: Action
        //     * -fi: FileID
        //     * -fl: Nhiều FileID, phân cách bởi ký tự ';'
        //     * -fn: FileName
        //     * -fp: FilePath
        //     * -up: User trong chương trình
        //     * -pn: program name
        //     * -pw: password HSM
        //     */

        //    try
        //    {
        //        //args = Environment.GetCommandLineArgs();
        //        for (int i = 0; i < args.Length; i++)
        //        {
        //            if (args[i].Contains("-ac"))
        //            {
        //                int _action = Convert.ToInt32(args[i].Substring(3));
        //                a_Action = (CACommand)_action;
        //            }
        //            else if (args[i].Contains("-fi"))
        //            {
        //                a_fileID = Convert.ToInt32(args[i].Substring(3));
        //            }
        //            else if (args[i].Contains("-fl"))
        //            {
        //                a_fileIDs = args[i].Substring(3);
        //            }
        //            else if (args[i].Contains("-pn"))
        //            {
        //                a_progName = args[i].Substring(3);
        //            }
        //            else if (args[i].Contains("-up"))
        //            {
        //                a_userProgName = args[i].Substring(3);
        //            }
        //            else if (args[i].Contains("-pw"))
        //            {
        //                a_passwordHSM = args[i].Substring(3);
        //            }
        //            else
        //            {
        //                ExitWithCode(CAExitCode.BadArgument);
        //            }
        //        }
        //    }
        //    catch
        //    {
        //        ExitWithCode(CAExitCode.BadArgument);
        //    }

        //    try
        //    {
        //        switch (a_Action)
        //        {
        //            case CACommand.None:
        //                ExitWithCode(CAExitCode.Success);
        //                return;
        //            case CACommand.SignFileByID:
        //                SignFileByID(a_fileID, a_progName, a_userProgName, a_passwordHSM);
        //                break;
        //            case CACommand.SignFilesByIDs:
        //                SignFilesByIDs(a_fileIDs, a_progName, a_userProgName, a_passwordHSM);
        //                break;
        //            case CACommand.CheckLoginHSM:
        //                CheckLoginHSM(a_progName, a_userProgName, a_passwordHSM);
        //                break;
        //            default:
        //                ExitWithCode(CAExitCode.UnrecognizedCommand);
        //                break;
        //        }
        //    }
        //    catch(Exception ex)
        //    {
        //        //System.Windows.Forms.MessageBox.Show(ex.ToString());
        //        return;
        //    }
        //}

        //private static void SignFileByID(int fileID, string programName, string userName, string password)
        //{
        //    //System.Windows.Forms.MessageBox.Show("bắt đầu ký");
        //    //mã trả về
        //    CAExitCode _eCode = CAExitCode.Success;

        //    try
        //    {
        //        //Thư viện HSM
        //        Common.CRYPTOKI = GetCryptokiDLL();
        //        //CSDL
        //        DAL_SqlConnector.ConnectionString = GetDatabaseString();
        //        BUSQuanTri bus = new BUSQuanTri();
        //        //Tham số
        //        X509Certificate2 cert;  //Chứng thư ký
        //        string filePath = "";   //Đường dẫn file
        //        ESignature signature;   //Lưu chữ ký vừa tạo

        //        //System.Windows.Forms.MessageBox.Show("thông tin người ký");
        //        #region Lấy thông tin người ký
        //        //Lấy thông tin certificate
        //        DataTable dtCert = bus.CA_Certificate_SelectForSign(programName, userName);
        //        cert = new X509Certificate2((byte[])dtCert.Rows[0]["RawData"]);
        //        int certID = Convert.ToInt32(dtCert.Rows[0]["CertID"]);

        //        //BUILD: Tạm thời comment để chạy cho Ninhtq
        //        ////Xác thực chứng thư với nhà cung cấp CA
        //        //X509ChainStatus certificateStatus;
        //        //if (!Common.ValidateCertificate(cert, DateTime.Now, out certificateStatus))
        //        //    ExitWithCode(CAExitCode.WS_CertificateInvalidInCA);

        //        //Lấy key trong HSM
        //        //BUILD: dùng hàm HSM_WLDObject_SelectPrivateKeyByCertID nếu chạy ở chế độ WLD
        //        DataTable dtPK = bus.HSM_Object_SelectPrivateKeyByCertID(certID);
        //        string slotSerial = dtPK.Rows[0]["SlotSerial"].ToString();
        //        byte[] keyID = (byte[])dtPK.Rows[0]["CKA_ID"];
        //        #endregion

        //        //System.Windows.Forms.MessageBox.Show("lấy thông tin file");
        //        #region Lấy thông tin file
        //        //Kiểm tra và thiết lập trạng thái và lấy thông tin file
        //        DataTable dtFile = new DataTable();
        //        bool isOK = bus.FL_File_SelectForSign(fileID, programName, userName, out dtFile);

        //        //Nếu không được ký thì trả về trạng thái
        //        if (!isOK)
        //        {
        //            FileStatus status = (FileStatus)Convert.ToInt32(dtFile.Rows[0]["Status"]);
        //            if (status == FileStatus.KhoiTao)
        //                throw new Exception("WS_FileNotSaved");
        //            if (status == FileStatus.QuaTrinhKy)
        //                throw new Exception("WS_FileInSign");
        //            if (status == FileStatus.DaBiThayThe)
        //                throw new Exception("WS_FileReplaced");
        //        }

        //        //Lưu đường dẫn file và phiên ghi log
        //        filePath = dtFile.Rows[0]["FilePath"].ToString();
        //        int id_StatusLog = Convert.ToInt32(dtFile.Rows[0]["ID_StatusLog"]);
        //        //Tạo file tạm với extension
        //        string tempPath = Path.GetTempFileName() + Path.GetExtension(filePath);
        //        #endregion

        //        //System.Windows.Forms.MessageBox.Show("ký");
        //        #region Quá trình ký
        //        try
        //        {
        //            //tạo HSM
        //            //System.Windows.Forms.MessageBox.Show("tạo HSM");

        //            System.Windows.Forms.MessageBox.Show(Common.CRYPTOKI);

        //            //Khởi tạo giao tiếp HSM
        //            using (HSMServiceProvider provider = new HSMServiceProvider(Common.CRYPTOKI))
        //            {
        //                //Đăng nhập
        //                //System.Windows.Forms.MessageBox.Show("đăng nhập HSM");
        //                provider.Login(slotSerial, HSMLoginRole.User, password);
        //                //Khởi tạo private key dùng để ký
        //                provider.LoadPrivateKeyByID(keyID);

        //                //copy vào file tạm để xử lý
        //                File.Copy(filePath, tempPath, true);

        //                //Khởi tạo giao tiếp với file tạm và ký
        //                //System.Windows.Forms.MessageBox.Show("ký sử dụng DSM");
        //                using (ESDigitalSignatureManager dsm = new ESDigitalSignatureManager(tempPath))
        //                {
        //                    //Ký file
        //                    dsm.Sign(cert, provider);
        //                    //Lấy chữ ký mới nhất
        //                    signature = dsm.Signatures[dsm.Signatures.Count() - 1];
        //                }

        //                //Kiểm tra thời gian chờ
        //                if (bus.FL_File_SelectForSaveSign(id_StatusLog))
        //                {
        //                    //Copy vào file đích
        //                    File.Copy(tempPath, filePath, true);
        //                    //Ghi log
        //                    bus.FL_File_UpdateForLogSign(fileID, cert.SerialNumber, signature.SigningTime, (int)signature.Verify, signature.SignatureCreator,
        //                        (int)FileSignActions.AddSignature, "Hoàn thành ký file qua HSM", programName, userName);
        //                }
        //                else
        //                    _eCode = CAExitCode.WS_SaveFileTimeOut;

        //                //Đăng xuất
        //                provider.Logout();
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            //Trả trạng thái
        //            bus.FL_File_UpdateStatus(fileID, (int)FileStatus.LuuFile, "Trả trạng thái do lỗi khi ký file qua HSM", programName, userName);
        //            throw ex;
        //        }
        //        finally
        //        {
        //            //Xóa file tạm
        //            if (File.Exists(tempPath))
        //                File.Delete(tempPath);
        //        }
        //        #endregion
        //    }
        //    catch (Exception ex)
        //    {
        //        _eCode = GetErrorCodeFromString(ex.Message);
        //        System.Windows.Forms.MessageBox.Show(ex.ToString());
        //        //using (StreamWriter file = new StreamWriter(Path.Combine(Directory.GetCurrentDirectory(), "log.txt"), true))
        //        //{
        //        //    file.WriteLine(ex.ToString());
        //        //}
        //    }

        //    ExitWithCode(_eCode);
        //}

        //private static void SignFilesByIDs(string arFileID, string programName, string userName, string password)
        //{
        //    //mã trả về
        //    CAExitCode _eCode = CAExitCode.Success;

        //    try
        //    {
        //        //Thư viện HSM
        //        Common.CRYPTOKI = GetCryptokiDLL();
        //        //CSDL
        //        DAL_SqlConnector.ConnectionString = GetDatabaseString();
        //        BUSQuanTri bus = new BUSQuanTri();
        //        //Tham số
        //        X509Certificate2 cert;  //Chứng thư ký
        //        //string filePath = "";   //Đường dẫn file
        //        //ESignature signature;   //Lưu chữ ký vừa tạo

        //        #region Lấy thông tin người ký
        //        //Lấy thông tin certificate
        //        DataTable dtCert = bus.CA_Certificate_SelectForSign(programName, userName);
        //        cert = new X509Certificate2((byte[])dtCert.Rows[0]["RawData"]);
        //        int certID = Convert.ToInt32(dtCert.Rows[0]["CertID"]);

        //        //BUILD: Tạm thời comment để chạy cho Ninhtq
        //        ////Xác thực chứng thư với nhà cung cấp CA
        //        //X509ChainStatus certificateStatus;
        //        //if (!Common.ValidateCertificate(cert, DateTime.Now, out certificateStatus))
        //        //    ExitWithCode(CAExitCode.WS_CertificateInvalidInCA);

        //        //Lấy key trong HSM
        //        //BUILD: dùng hàm HSM_WLDObject_SelectPrivateKeyByCertID nếu chạy ở chế độ WLD
        //        DataTable dtPK = bus.HSM_Object_SelectPrivateKeyByCertID(certID);
        //        string slotSerial = dtPK.Rows[0]["SlotSerial"].ToString();
        //        byte[] keyID = (byte[])dtPK.Rows[0]["CKA_ID"];
        //        #endregion

        //        //Khởi tạo giao tiếp HSM và đăng nhập
        //        HSMServiceProvider provider = new HSMServiceProvider(Common.CRYPTOKI);
        //        provider.Login(slotSerial, HSMLoginRole.User, password);
        //        //Khởi tạo private key dùng để ký
        //        provider.LoadPrivateKeyByID(keyID);

        //        //Duyệt từng fileID và ký
        //        try
        //        {
        //            string[] fileIDs = arFileID.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);

        //            //tạo Dictionary chứa kết quả
        //            int fileSignedCount = 0;
        //            Dictionary<int, string> dicResults = new Dictionary<int, string> { };
        //            foreach (string file in fileIDs)
        //            {
        //                int fileID = Int32.Parse(file);
        //                dicResults.Add(fileID, "");
        //            }

        //            //ký file
        //            foreach (string file in fileIDs)
        //            {
        //                int fileID = Int32.Parse(file);

        //                //Lấy đường dẫn file
        //                string filePath = bus.FL_File_GetFilePathForSign(fileID);

        //                BackgroundWorker bgw = new BackgroundWorker();
        //                bgw.DoWork += delegate
        //                {
        //                    try
        //                    {
        //                        using (ESDigitalSignatureManager dsm = new ESDigitalSignatureManager(filePath))
        //                        {
        //                            //Ký
        //                            dsm.Sign(cert, provider);
        //                            //Lưu kết quả vào Dictionary
        //                            lock (dicResults)
        //                            {
        //                                dicResults[fileID] = "Success";
        //                            }

        //                            //Lấy chữ ký mới nhất
        //                            ESignature signature = dsm.Signatures[dsm.Signatures.Count() - 1];
        //                            //Ghi log
        //                            bus.FL_LogFileSignature_Insert(fileID, cert.SerialNumber, signature.SigningTime, (int)signature.Verify, signature.SignatureCreator,
        //                                (int)FileSignActions.AddSignature, programName, userName);
        //                        }
        //                    }
        //                    catch (Exception ex)
        //                    {
        //                        //Lưu kết quả vào Dictionary
        //                        lock (dicResults)
        //                        {
        //                            dicResults[fileID] = "Error: " + ex.Message;
        //                        }
        //                    }
        //                };
        //                bgw.RunWorkerCompleted += delegate
        //                {
        //                    fileSignedCount++;
        //                    //Đã ký hết các file
        //                    if (fileSignedCount == dicResults.Count)
        //                    {
        //                        //Đóng giao tiếp HSM
        //                        provider.Logout();
        //                        provider.Dispose();
        //                    }
        //                    bgw.Dispose();
        //                };
        //                bgw.RunWorkerAsync();

        //                /*Danh sách fileID lỗi được lưu trong dic. Cần ghi log thì đưa ra đây*/
        //            }
        //        }
        //        finally
        //        {
        //            //if (!provider.IsDisposed)
        //            //{
        //            //    //delay 1 thời gian (theo timeout), sau đó mới dispose
        //            //    System.Threading.Thread.Sleep(5000);
        //            //    provider.Logout();
        //            //    provider.Dispose();
        //            //}
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _eCode = GetErrorCodeFromString(ex.Message);
        //    }

        //    ExitWithCode(_eCode);

        //    #region Single thread
        //    ////mã trả về
        //    //CAExitCode _eCode = CAExitCode.Success;

        //    //try
        //    //{
        //    //    //Thư viện HSM
        //    //    Common.CRYPTOKI = GetCryptokiDLL();
        //    //    //CSDL
        //    //    DAL_SqlConnector.ConnectionString = GetDatabaseString();
        //    //    BUSQuanTri bus = new BUSQuanTri();

        //    //    //Lấy thông tin certificate
        //    //    DataTable dtCert = bus.CA_Certificate_SelectForSign(programName, userName);
        //    //    int certID = Convert.ToInt32(dtCert.Rows[0]["CertID"]);
        //    //    X509Certificate2 cert = new X509Certificate2((byte[])dtCert.Rows[0]["RawData"]);

        //    //    //Lấy key trong HSM
        //    //    //LƯU Ý: dùng hàm HSM_WLDObject_SelectPrivateKeyByCertID nếu chạy ở chế độ WLD
        //    //    DataTable dtPK = bus.HSM_Object_SelectPrivateKeyByCertID(Convert.ToInt32(dtCert.Rows[0]["CertID"]));
        //    //    int slotID = Convert.ToInt32(dtPK.Rows[0]["SlotID"]);
        //    //    byte[] keyID = (byte[])dtPK.Rows[0]["CKA_ID"];

        //    //    //Khởi tạo giao tiếp HSM và đăng nhập
        //    //    HSMServiceProvider provider = new HSMServiceProvider(Common.CRYPTOKI);
        //    //    provider.Login(slotID, HSMLoginRole.User, password);
        //    //    //Khởi tạo private key dùng để ký
        //    //    provider.LoadPrivateKeyByID(keyID);

        //    //    //Duyệt từng fileID và ký
        //    //    try
        //    //    {
        //    //        string[] fileIDs = arFileID.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
        //    //        foreach (string file in fileIDs)
        //    //        {
        //    //            int fileID = Int32.Parse(file);

        //    //            //Lấy đường dẫn file
        //    //            string filePath = bus.FL_File_GetFilePathForSign(fileID);

        //    //            using (ESDigitalSignatureManager dsm = new ESDigitalSignatureManager(filePath))
        //    //            {
        //    //                dsm.Sign(filePath, cert, provider);
        //    //                //Lấy chữ ký mới nhất
        //    //                ESignature signature = dsm.Signatures[dsm.Signatures.Count() - 1];
        //    //                //Ghi log
        //    //                bus.FL_LogFileSignature_Insert(fileID, cert.SerialNumber, signature.SigningTime, (int)signature.Verify, signature.SignatureCreator,
        //    //                    (int)FileSignActions.AddSignature, programName, userName);
        //    //            }
        //    //        }
        //    //    }
        //    //    finally
        //    //    {
        //    //        //Đóng giao tiếp HSM
        //    //        provider.Logout();
        //    //        provider.Dispose();
        //    //    }
        //    //}
        //    //catch (Exception ex)
        //    //{
        //    //    _eCode = GetErrorCodeFromString(ex.Message);
        //    //}

        //    //ExitWithCode(_eCode);
        //    #endregion
        //}

        //private static void CheckLoginHSM(string a_progName, string a_userProgName, string a_passwordHSM)
        //{
        //    //mã trả về
        //    CAExitCode _eCode = CAExitCode.Success;

        //    try
        //    {
        //        //Thư viện HSM
        //        Common.CRYPTOKI = GetCryptokiDLL();
        //        //CSDL
        //        DAL_SqlConnector.ConnectionString = GetDatabaseString();
        //        BUSQuanTri bus = new BUSQuanTri();

        //        //Lấy thông tin certificate
        //        DataTable dtCert = bus.CA_Certificate_SelectForSign(a_progName, a_userProgName);

        //        //Lấy key trong HSM
        //        //LƯU Ý: dùng hàm HSM_WLDObject_SelectPrivateKeyByCertID nếu chạy ở chế độ WLD
        //        DataTable dtPK = bus.HSM_Object_SelectPrivateKeyByCertID(Convert.ToInt32(dtCert.Rows[0]["CertID"]));
        //        string slotSerial = dtPK.Rows[0]["SlotSerial"].ToString();
        //        byte[] keyID = (byte[])dtPK.Rows[0]["CKA_ID"];

        //        //Khởi tạo giao tiếp HSM và đăng nhập
        //        using (HSMServiceProvider provider = new HSMServiceProvider(Common.CRYPTOKI))
        //        {
        //            provider.Login(slotSerial, HSMLoginRole.User, a_passwordHSM);
        //            //Khởi tạo private key dùng để ký
        //            provider.LoadPrivateKeyByID(keyID);
        //            //Đóng giao tiếp HSM
        //            provider.Logout();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _eCode = GetErrorCodeFromString(ex.Message);
        //    }

        //    ExitWithCode(_eCode);
        //}

        //private static void ExitWithCode(CAExitCode code)
        //{
        //    Environment.Exit((int)code);
        //}

        //private static CAExitCode GetErrorCodeFromString(string exMessage)
        //{
        //    switch (exMessage)
        //    {
        //        case "WS_FileNotFound":
        //            return CAExitCode.WS_FileNotFound;
        //        case "WS_FileNotSaved":
        //            return CAExitCode.WS_FileNotSaved;
        //        case "WS_CertificateNotFoundOrInvalid":
        //            return CAExitCode.WS_CertificateNotFoundOrInvalid;
        //        case "WS_CertificateMoreThanOne":
        //            return CAExitCode.WS_CertificateMoreThanOne;
        //        case "WS_PrivateKeyNotFound":
        //            return CAExitCode.WS_PrivateKeyNotFound;
        //        case "WS_PrivateKeyMoreThanOne":
        //            return CAExitCode.WS_PrivateKeyMoreThanOne;
        //        case "WS_FileInSign":
        //            return CAExitCode.WS_FileInSign;
        //        case "WS_FileReplaced":
        //            return CAExitCode.WS_FileReplaced;
        //        case "WS_CertificateInvalidInCA":
        //            return CAExitCode.WS_CertificateInvalidInCA;
        //        case "WS_SaveFileTimeOut":
        //            return CAExitCode.WS_SaveFileTimeOut;
        //        default:
        //            return CAExitCode.Error;
        //    }
        //}

        //private static string GetCryptokiDLL()
        //{
        //    string localPath = "";
        //    if (Environment.Is64BitProcess)
        //        localPath = @"cryptoki_win64\cryptoki.dll";
        //    else
        //        localPath = @"cryptoki_win32\cryptoki.dll";
        //    string dllPath = Path.Combine(ConfigurationManager.AppSettings["CRYPTOKI_Path"], localPath);
        //    return dllPath;
        //}

        //private static string GetDatabaseString()
        //{
        //    return ConfigurationManager.ConnectionStrings["Database_CA"].ConnectionString; //"Data Source=192.168.0.48;Initial Catalog=CA;User ID=cadev;Password=cadev123;";
        //}
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Windows.Forms;
using ES.CA_WebServiceBUS;
using ES.CA_WebServiceDAL;
using esDigitalSignature;
using esDigitalSignature.eToken;
using esDigitalSignature.Library;

namespace CA_AppService
{
    public partial class frmMain : Form
    {
        private int runner = 0;
        DateTime startTime;

        public frmMain()
        {
            //try
            //{
            //    HSMServiceProvider.Initialize(Common.CRYPTOKI);
            //    //Thử đăng nhập
            //    using (HSMServiceProvider hsm = new HSMServiceProvider("57257", HSMLoginRole.User, "1234567"))
            //    {
            //        //hsm.LoadPrivateKeyByID(keyID);
            //    }
            //    HSMServiceProvider.Finalize();
            //}
            //catch (Exception ex)
            //{
            //    CAExitCode exit = GetErrorCodeFromString(ex.Message);
            //    HSMServiceProvider.Finalize();
            //}
            //return;


            startTime = DateTime.Now;
            InitializeComponent();

            this.Visible = false;

            string[] args = Environment.GetCommandLineArgs();
            ProcessArguments(args);
        }

        #region Arguments: các biến để truyền vào từ tham số của app
        /// <summary>
        /// Lệnh
        /// </summary>
        CACommand a_Action = CACommand.None;
        /// <summary>
        /// FileID trong hệ thống CA
        /// </summary>
        int a_fileID = -1;
        /// <summary>
        /// Danh sách FileID, phân cách bởi ký tự ';'
        /// </summary>
        string a_fileIDs = "-1";
        /// <summary>
        /// Tên chương trình gọi lệnh
        /// </summary>
        string a_progName = string.Empty;
        /// <summary>
        /// Tên người dùng trong chương trình gọi lệnh
        /// </summary>
        string a_userProgName = string.Empty;
        /// <summary>
        /// Mật khẩu slot HSM tương ứng với người dùng
        /// </summary>
        string a_passwordHSM = string.Empty;
        /// <summary>
        /// Mật khẩu slot HSM mới
        /// </summary>
        string a_newPinHSM = string.Empty;
        /// <summary>
        /// Chương tình có ghi ra output
        /// </summary>
        bool a_Output = false;
        /// <summary>
        /// thời gian time out của chương trình, mặc định là 300s
        /// </summary>
        int a_Timeout = 300;
        #endregion

        /// <summary>
        /// Xử lý tham số truyền vào
        /// </summary>
        /// <param name="args"></param>
        private void ProcessArguments(string[] args)
        {
            /* xử lý tham số truyền vào ở đây
             * quy định tên argument sẽ là:
             * -ac: Action
             * -fi: FileID
             * -fl: Nhiều FileID, phân cách bởi ký tự ';'
             * -fn: FileName
             * -fp: FilePath
             * -up: User trong chương trình
             * -pn: program name
             * -pw: password HSM
             * -od: output detail
             */

            try
            {
                //args = Environment.GetCommandLineArgs();
                for (int i = 1; i < args.Length; i++)
                {
                    if (args[i].Contains("-ac"))
                    {
                        int _action = Convert.ToInt32(args[i].Substring(3));
                        a_Action = (CACommand)_action;
                    }
                    else if (args[i].Contains("-fi"))
                    {
                        a_fileID = Convert.ToInt32(args[i].Substring(3));
                    }
                    else if (args[i].Contains("-fl"))
                    {
                        a_fileIDs = args[i].Substring(3);
                    }
                    else if (args[i].Contains("-pn"))
                    {
                        a_progName = args[i].Substring(3);
                    }
                    else if (args[i].Contains("-up"))
                    {
                        a_userProgName = args[i].Substring(3);
                    }
                    else if (args[i].Contains("-pw"))
                    {
                        a_passwordHSM = args[i].Substring(3);
                    }
                    else if (args[i].Contains("-nw"))
                    {
                        a_newPinHSM = args[i].Substring(3);
                    }
                    else if (args[i].Contains("-od"))
                    {
                        a_Output = Convert.ToBoolean(args[i].Substring(3));
                    }
                    else if (args[i].Contains("-to"))
                    {
                        a_Timeout = Convert.ToInt32(args[i].Substring(3));
                    }
                    else
                    {
                        ExitWithCode(CAExitCode.BadArgument);
                    }
                }
            }
            catch
            {
                ExitWithCode(CAExitCode.BadArgument);
            }

            try
            {
                tmrTimeout.Enabled = true;
                tmrTimeout.Interval = 1000;
                runner = 0;
                tmrTimeout.Start();

                switch (a_Action)
                {
                    case CACommand.None:
                        ExitWithCode(CAExitCode.Success);
                        return;
                    //case CACommand.SignFileByID:
                    //    SignFileByID(a_fileID, a_progName, a_userProgName, a_passwordHSM);
                    //    break;
                    //case CACommand.SignFilesByIDs:
                    //    SignFilesByIDs(a_fileIDs, a_progName, a_userProgName, a_passwordHSM);
                    //    break;
                    //case CACommand.SignFilesByID_Thread:
                    //    SignFilesByIDs_Thread3(a_fileIDs, a_progName, a_userProgName, a_passwordHSM);
                    //    //SignFilesByIDs_Thread_WriteLogFile(a_fileIDs, a_progName, a_userProgName, a_passwordHSM);
                    //    break;
                    case CACommand.CheckLoginHSM:
                        CheckLoginHSM(a_progName, a_userProgName, a_passwordHSM);
                        break;
                    case CACommand.ChangePIN:
                        ChangePIN(a_progName, a_userProgName, a_passwordHSM, a_newPinHSM);
                        break;
                    default:
                        ExitWithCode(CAExitCode.UnrecognizedCommand);
                        break;
                }
            }
            catch (Exception ex)
            {
                //System.Windows.Forms.MessageBox.Show(ex.ToString());
                ExitWithCode(CAExitCode.Error);
                return;
            }
        }

        #region Old
        ///// <summary>
        ///// Ký 1 file
        ///// </summary>
        ///// <param name="fileID"></param>
        ///// <param name="programName"></param>
        ///// <param name="userName"></param>
        ///// <param name="password"></param>
        //private void SignFileByID(int fileID, string programName, string userName, string password)
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

        //        //Xác thực chứng thư với nhà cung cấp CA
        //        X509ChainStatus certificateStatus;
        //        if (!Common.ValidateCertificate(cert, DateTime.Now, out certificateStatus))
        //            ExitWithCode(CAExitCode.WS_CertificateInvalidInCA);

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

        //            //System.Windows.Forms.MessageBox.Show(Common.CRYPTOKI);

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
        //                //if (bus.FL_File_SelectForSaveSign(id_StatusLog))
        //                //{
        //                //Copy vào file đích
        //                File.Copy(tempPath, filePath, true);
        //                //Ghi log
        //                bus.FL_File_UpdateForLogSign(fileID, cert.SerialNumber, signature.SigningTime, (int)signature.Verify, signature.SignatureCreator,
        //                    (int)FileSignActions.AddSignature, "", "Hoàn thành ký file qua HSM", programName, userName);
        //                //}
        //                //else
        //                //    _eCode = CAExitCode.WS_SaveFileTimeOut;

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
        //        //System.Windows.Forms.MessageBox.Show(ex.ToString());
        //        //using (StreamWriter file = new StreamWriter(Path.Combine(Directory.GetCurrentDirectory(), "log.txt"), true))
        //        //{
        //        //    file.WriteLine(ex.ToString());
        //        //}
        //    }

        //    ExitWithCode(_eCode);
        //}

        ///// <summary>
        ///// Ký tuần tự nhiều file
        ///// </summary>
        ///// <param name="arFileID"></param>
        ///// <param name="programName"></param>
        ///// <param name="userName"></param>
        ///// <param name="password"></param>
        //private void SignFilesByIDs(string arFileID, string programName, string userName, string password)
        //{
        //    #region Single thread
        //    //mã trả về
        //    CAExitCode _eCode = CAExitCode.Success;

        //    try
        //    {
        //        //Thư viện HSM
        //        Common.CRYPTOKI = GetCryptokiDLL();
        //        //CSDL
        //        DAL_SqlConnector.ConnectionString = GetDatabaseString();
        //        BUSQuanTri bus = new BUSQuanTri();
        //        X509Certificate2 cert;

        //        #region Lấy thông tin người ký
        //        //Lấy thông tin certificate
        //        DataTable dtCert = bus.CA_Certificate_SelectForSign(programName, userName);
        //        cert = new X509Certificate2((byte[])dtCert.Rows[0]["RawData"]);
        //        int certID = Convert.ToInt32(dtCert.Rows[0]["CertID"]);

        //        //Xác thực chứng thư với nhà cung cấp CA
        //        X509ChainStatus certificateStatus;
        //        if (!Common.ValidateCertificate(cert, DateTime.Now, out certificateStatus))
        //            ExitWithCode(CAExitCode.WS_CertificateInvalidInCA);

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
        //            string[] fileIDs = arFileID.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
        //            foreach (string file in fileIDs)
        //            {

        //                int fileID = Int32.Parse(file);
        //                try
        //                {
        //                    //Lấy đường dẫn file
        //                    string filePath = bus.FL_File_GetFilePathForSign(fileID);

        //                    //Tạo file tạm với extension
        //                    string tempPath = Path.GetTempFileName() + Path.GetExtension(filePath);

        //                    //copy vào file tạm để xử lý
        //                    File.Copy(filePath, tempPath, true);

        //                    using (ESDigitalSignatureManager dsm = new ESDigitalSignatureManager(tempPath))
        //                    {
        //                        dsm.Sign(cert, provider);
        //                        //Lấy chữ ký mới nhất
        //                        ESignature signature = dsm.Signatures[dsm.Signatures.Count() - 1];
        //                        //Ghi log
        //                        bus.FL_LogFileSignature_Insert(fileID, cert.SerialNumber, signature.SigningTime, (int)signature.Verify, signature.SignatureCreator,
        //                            (int)FileSignActions.AddSignature, programName, userName);
        //                    }

        //                    //Copy vào file đích
        //                    File.Copy(tempPath, filePath, true);

        //                    //Xóa file tạm
        //                    File.Delete(tempPath);

        //                    Console.WriteLine(fileID.ToString() + CA_Variable.SignFile_Result_Separator + "Success");
        //                }
        //                catch (Exception ex)
        //                {
        //                    Console.WriteLine(fileID.ToString() + CA_Variable.SignFile_Result_Separator + "Error: " + ex.Message);
        //                }
        //            }
        //        }
        //        finally
        //        {
        //            //Đóng giao tiếp HSM
        //            provider.Logout();
        //            provider.Dispose();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _eCode = GetErrorCodeFromString(ex.Message);
        //    }

        //    ExitWithCode(_eCode);
        //    #endregion
        //}

        ///// <summary>
        ///// Mở mỗi file là 1 thread, mỗi thread mở 1 session (test)
        ///// </summary>
        ///// <param name="arFileID"></param>
        ///// <param name="programName"></param>
        ///// <param name="userName"></param>
        ///// <param name="password"></param>
        //private void SignFilesByIDs_Thread1(string arFileID, string programName, string userName, string password)
        //{
        //    ////test
        //    //string file = "";
        //    //for (int j = 1; j <= 2000; j++)
        //    //{
        //    //    if (file != "") file += ";";
        //    //    file += j.ToString();
        //    //}

        //    //arFileID = file;

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

        //        //Xác thực chứng thư với nhà cung cấp CA
        //        X509ChainStatus certificateStatus;
        //        if (!Common.ValidateCertificate(cert, DateTime.Now, out certificateStatus))
        //            ExitWithCode(CAExitCode.WS_CertificateInvalidInCA);

        //        //Lấy key trong HSM
        //        //BUILD: dùng hàm HSM_WLDObject_SelectPrivateKeyByCertID nếu chạy ở chế độ WLD
        //        DataTable dtPK = bus.HSM_Object_SelectPrivateKeyByCertID(certID);
        //        string slotSerial = dtPK.Rows[0]["SlotSerial"].ToString();
        //        byte[] keyID = (byte[])dtPK.Rows[0]["CKA_ID"];
        //        #endregion

        //        //Khởi tạo kết nối
        //        PKCS11.Initialize(Common.CRYPTOKI);
        //        PKCS11.Slot[] slots = PKCS11.GetSlotList(true);
        //        PKCS11.Slot slot = slots[3];

        //        //Duyệt từng fileID và ký
        //        string[] fileIDs = arFileID.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

        //        //tạo Dictionary chứa kết quả
        //        int fileSignedCount = 0;
        //        DataTable dtFile = createDataTableFiles();

        //        foreach (string _fileID in fileIDs)
        //        {
        //            try
        //            {
        //                DataRow r = dtFile.NewRow();
        //                r["FileID"] = _fileID;
        //                string _filePath = ""; //bus.FL_File_GetFilePathForSign(int.Parse(_fileID));
        //                string tempFile = "";// Path.GetTempFileName();

        //                r["FileSourcePath"] = _filePath;
        //                r["FileTempSysPath"] = tempFile;
        //                r["FileTempSignPath"] = tempFile + Path.GetExtension(_filePath);
        //                dtFile.Rows.Add(r);
        //            }
        //            catch (Exception ex)
        //            {
        //                Console.WriteLine(_fileID + CA_Variable.SignFile_Result_Separator + "Error: " + ex.Message);
        //            }
        //        }
        //        try
        //        {

        //            //ký file
        //            for (int i = 0; i < dtFile.Rows.Count; i++)
        //            {
        //                int j = i;

        //                BackgroundWorker bgw = new BackgroundWorker();
        //                bgw.DoWork += delegate
        //                {
        //                    try
        //                    {
        //                        int fileID = Convert.ToInt32(dtFile.Rows[j]["FileID"]);
        //                        string tempPath = dtFile.Rows[j]["FileTempSignPath"].ToString();
        //                        string filePath = dtFile.Rows[j]["FileSourcePath"].ToString();
        //                        string temp = dtFile.Rows[j]["FileTempSysPath"].ToString();

        //                        //File.Copy(filePath, tempPath, true);

        //                        //Mở session
        //                        PKCS11.Session session = PKCS11.OpenSession(slot,
        //                          PKCS11.CKF_RW_SESSION | PKCS11.CKF_SERIAL_SESSION);
        //                        session.Login(PKCS11.CKU_USER, "123456");

        //                        //Tìm key
        //                        PKCS11.Object PIkey = session.FindObjects(new PKCS11.Attribute[]  {
        //            new PKCS11.Attribute(PKCS11.CKA_CLASS, PKCS11.CKO_PRIVATE_KEY),
        //            new PKCS11.Attribute(PKCS11.CKA_LABEL, "NinhtqPI")
        //          })[0];

        //                        //Ký
        //                        PKCS11.Mechanism signMech = new PKCS11.Mechanism(PKCS11.CKM_RSA_PKCS, null);
        //                        byte[] ArrSigned = session.Sign(signMech, PIkey, Encoding.ASCII.GetBytes("RandomTextForTest"));

        //                        //Đóng session
        //                        session.Logout();
        //                        session.Close();

        //                        //File.Copy(tempPath, filePath, true);

        //                        //xóa các file tạm?
        //                        //File.Delete(temp);
        //                        //File.Delete(tempPath);

        //                        Console.WriteLine(fileID.ToString() + CA_Variable.SignFile_Result_Separator + "Success");
        //                    }
        //                    catch (Exception ex)
        //                    {
        //                        _eCode = GetErrorCodeFromString(ex.Message);

        //                        //Console.WriteLine(fileID.ToString() + CA_Variable.SignFile_Result_Separator + "Error: " + ex.Message);
        //                        Console.WriteLine(dtFile.Rows[j]["FileID"].ToString() + CA_Variable.SignFile_Result_Separator + "Error: " + ex.Message);
        //                    }
        //                };
        //                bgw.RunWorkerCompleted += delegate
        //                {
        //                    fileSignedCount++;
        //                    //Đã ký hết các file
        //                    if (fileSignedCount == dtFile.Rows.Count)
        //                    {
        //                        //Kết thúc
        //                        PKCS11.Finalize();

        //                        ExitWithCode(_eCode);
        //                    }
        //                    bgw.Dispose();
        //                };
        //                bgw.RunWorkerAsync();
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            throw new Exception(ex.Message, ex);
        //        }
        //        finally
        //        {

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _eCode = GetErrorCodeFromString(ex.Message);
        //        ExitWithCode(_eCode);
        //        //MessageBox.Show(ex.ToString());
        //    }
        //}

        ///// <summary>
        ///// Mở 30 thread, dùng chung HSMServiceProvider, mỗi thread ký tuần tự các file trong đó (test)
        ///// </summary>
        ///// <param name="arFileID"></param>
        ///// <param name="programName"></param>
        ///// <param name="userName"></param>
        ///// <param name="password"></param>
        //private void SignFilesByIDs_Thread2(string arFileID, string programName, string userName, string password)
        //{
        //    ////test
        //    //string file = "";
        //    //for (int j = 1; j <= 2000; j++)
        //    //{
        //    //    if (file != "") file += ";";
        //    //    file += j.ToString();
        //    //}

        //    //arFileID = file;

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

        //        //Xác thực chứng thư với nhà cung cấp CA
        //        X509ChainStatus certificateStatus;
        //        if (!Common.ValidateCertificate(cert, DateTime.Now, out certificateStatus))
        //            ExitWithCode(CAExitCode.WS_CertificateInvalidInCA);

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
        //        string[] fileIDs = arFileID.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

        //        //tạo Dictionary chứa kết quả
        //        int fileSignedCount = 0;
        //        DataTable dtFile = createDataTableFiles();

        //        foreach (string _fileID in fileIDs)
        //        {
        //            try
        //            {
        //                DataRow r = dtFile.NewRow();
        //                r["FileID"] = _fileID;
        //                string _filePath = "";// bus.FL_File_GetFilePathForSign(int.Parse(_fileID));
        //                string tempFile = "";// Path.GetTempFileName();

        //                r["FileSourcePath"] = _filePath;
        //                r["FileTempSysPath"] = tempFile;
        //                r["FileTempSignPath"] = tempFile + Path.GetExtension(_filePath);
        //                dtFile.Rows.Add(r);
        //            }
        //            catch (Exception ex)
        //            {
        //                Console.WriteLine(_fileID + CA_Variable.SignFile_Result_Separator + "Error: " + ex.Message);
        //            }
        //        }
        //        try
        //        {
        //            //tối đa là 30 thread, số lượng file ký sẽ được truyền vào thread
        //            int maxThread = 30;
        //            int filePerThread = (int)Math.Ceiling((double)dtFile.Rows.Count / maxThread);
        //            for (int i = 0; i < maxThread; i++)
        //            {
        //                int j = i;

        //                BackgroundWorker bgw = new BackgroundWorker();
        //                bgw.DoWork += delegate
        //                {

        //                    for (int count = 1; count <= filePerThread; count++)
        //                    {
        //                        int index = filePerThread * j + count;
        //                        if (index >= dtFile.Rows.Count) return;

        //                        try
        //                        {
        //                            int fileID = Convert.ToInt32(dtFile.Rows[index]["FileID"]);
        //                            string tempPath = dtFile.Rows[index]["FileTempSignPath"].ToString();
        //                            string filePath = dtFile.Rows[index]["FileSourcePath"].ToString();
        //                            string temp = dtFile.Rows[index]["FileTempSysPath"].ToString();

        //                            //File.Copy(filePath, tempPath, true);

        //                            provider.SignData(Encoding.ASCII.GetBytes("RandomTextForTest"));

        //                            //File.Copy(tempPath, filePath, true);

        //                            //xóa các file tạm?
        //                            //File.Delete(temp);
        //                            //File.Delete(tempPath);

        //                            Console.WriteLine(fileID.ToString() + CA_Variable.SignFile_Result_Separator + "Success");
        //                        }
        //                        catch (Exception ex)
        //                        {
        //                            _eCode = GetErrorCodeFromString(ex.Message);

        //                            //Console.WriteLine(fileID.ToString() + CA_Variable.SignFile_Result_Separator + "Error: " + ex.Message);
        //                            Console.WriteLine(dtFile.Rows[index]["FileID"].ToString() + CA_Variable.SignFile_Result_Separator + "Error: " + ex.Message);
        //                        }
        //                    }

        //                };
        //                bgw.RunWorkerCompleted += delegate
        //                {
        //                    fileSignedCount++;
        //                    //Đã ký hết các file
        //                    if (fileSignedCount == maxThread)
        //                    {
        //                        //Đóng giao tiếp HSM
        //                        provider.Logout();
        //                        provider.Dispose();

        //                        ExitWithCode(_eCode);
        //                    }
        //                    bgw.Dispose();
        //                };
        //                bgw.RunWorkerAsync();
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            throw new Exception(ex.Message, ex);
        //        }
        //        finally
        //        {

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _eCode = GetErrorCodeFromString(ex.Message);
        //        ExitWithCode(_eCode);
        //        //MessageBox.Show(ex.ToString());
        //    }
        //}

        ///// <summary>
        ///// Mở 30 thread, mỗi thread mở 1 session, ký tất cả các file trong session đó
        ///// </summary>
        ///// <param name="arFileID"></param>
        ///// <param name="programName"></param>
        ///// <param name="userName"></param>
        ///// <param name="password"></param>
        //private void SignFilesByIDs_Thread3(string arFileID, string programName, string userName, string password)
        //{
        //    ////test
        //    //string file = "";
        //    //for (int j = 52; j <= 1267; j++)
        //    //{
        //    //    if (file != "") file += ";";
        //    //    file += j.ToString();
        //    //}

        //    //arFileID = file;

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

        //        //Xác thực chứng thư với nhà cung cấp CA
        //        X509ChainStatus certificateStatus;
        //        if (!Common.ValidateCertificate(cert, DateTime.Now, out certificateStatus))
        //            ExitWithCode(CAExitCode.WS_CertificateInvalidInCA);

        //        //Lấy key trong HSM
        //        //BUILD: dùng hàm HSM_WLDObject_SelectPrivateKeyByCertID nếu chạy ở chế độ WLD
        //        DataTable dtPK = bus.HSM_Object_SelectPrivateKeyByCertID(certID);
        //        string slotSerial = dtPK.Rows[0]["SlotSerial"].ToString();
        //        byte[] keyID = (byte[])dtPK.Rows[0]["CKA_ID"];
        //        #endregion

        //        //Khởi tạo kết nối HSM
        //        HSMServiceProvider.Initialize(Common.CRYPTOKI);

        //        //Duyệt từng fileID và ký
        //        string[] fileIDs = arFileID.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

        //        //tạo Dictionary chứa kết quả
        //        int fileSignedCount = 0;
        //        DataTable dtFile = createDataTableFiles();

        //        //xác định các file thực sự cần để ký
        //        foreach (string _fileID in fileIDs)
        //        {
        //            try
        //            {
        //                DataRow r = dtFile.NewRow();
        //                r["FileID"] = _fileID;
        //                string _filePath = bus.FL_File_GetFilePathForSign(int.Parse(_fileID));
        //                string tempFile = Path.GetTempFileName();

        //                r["FileSourcePath"] = _filePath;
        //                r["FileTempSysPath"] = tempFile;
        //                r["FileTempSignPath"] = tempFile + Path.GetExtension(_filePath);
        //                dtFile.Rows.Add(r);
        //            }
        //            catch (Exception ex)
        //            {
        //                Console.WriteLine(_fileID + CA_Variable.SignFile_Result_Separator + "Error: " + ex.Message);
        //            }
        //        }

        //        //bắt đầu ký
        //        try
        //        {
        //            //tối đa là xxx thread, số lượng file ký sẽ được truyền vào thread
        //            int maxThread = 20;
        //            int filePerThread = (int)Math.Ceiling((double)dtFile.Rows.Count / maxThread);
        //            for (int i = 0; i < maxThread; i++)
        //            {
        //                int j = i;
        //                int countFile = 0;

        //                try
        //                {
        //                    //Mở session
        //                    HSMServiceProvider provider = new HSMServiceProvider(slotSerial, HSMLoginRole.User, password);

        //                    provider.LoadPrivateKeyByID(keyID);

        //                    BackgroundWorker bgw = new BackgroundWorker();
        //                    bgw.DoWork += delegate
        //                    {

        //                        for (int count = 1; count <= filePerThread; count++)
        //                        {
        //                            int index = filePerThread * j + count;
        //                            if (index >= dtFile.Rows.Count) return;

        //                            try
        //                            {
        //                                int fileID = Convert.ToInt32(dtFile.Rows[index]["FileID"]);
        //                                string tempPath = dtFile.Rows[index]["FileTempSignPath"].ToString();
        //                                string filePath = dtFile.Rows[index]["FileSourcePath"].ToString();
        //                                string temp = dtFile.Rows[index]["FileTempSysPath"].ToString();

        //                                /////Ký*********************************
        //                                File.Copy(filePath, tempPath, true);

        //                                //byte[] ArrSigned = provider.SignData(Encoding.ASCII.GetBytes("RandomTextForTest"));
        //                                using (ESDigitalSignatureManager dsm = new ESDigitalSignatureManager(tempPath))
        //                                {
        //                                    dsm.Sign(cert, provider);
        //                                    //dsm.RemoveAllSignature();
        //                                }

        //                                File.Copy(tempPath, filePath, true);

        //                                ////xóa các file tạm?
        //                                File.Delete(temp);
        //                                File.Delete(tempPath);
        //                                ///*****************************************
        //                                ///////Bỏ chữ ký*******************************
        //                                //using (ESDigitalSignatureManager dsm = new ESDigitalSignatureManager(filePath))
        //                                //{
        //                                //    dsm.RemoveAllSignature();
        //                                //}
        //                                ///////********************************************

        //                                Console.WriteLine(fileID.ToString() + CA_Variable.SignFile_Result_Separator + "Success");
        //                            }
        //                            catch (Exception ex)
        //                            {
        //                                _eCode = GetErrorCodeFromString(ex.Message);

        //                                //Console.WriteLine(fileID.ToString() + CA_Variable.SignFile_Result_Separator + "Error: " + ex.Message);
        //                                Console.WriteLine(dtFile.Rows[index]["FileID"].ToString() + CA_Variable.SignFile_Result_Separator + "Error " + index.ToString() + ": " + ex.Message);
        //                            }
        //                        }
        //                    };
        //                    bgw.RunWorkerCompleted += delegate
        //                    {
        //                        fileSignedCount++;
        //                        countFile++;

        //                        if (countFile == filePerThread)
        //                        {
        //                            provider.Dispose();
        //                        }
        //                        //Đã ký hết các file
        //                        if (fileSignedCount == maxThread)
        //                        {
        //                            //Kết thúc
        //                            HSMServiceProvider.Finalize();

        //                            ExitWithCode(_eCode);
        //                        }
        //                        bgw.Dispose();
        //                    };
        //                    bgw.RunWorkerAsync();
        //                }
        //                catch
        //                {

        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            throw new Exception(ex.Message, ex);
        //        }
        //        finally
        //        {

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _eCode = GetErrorCodeFromString(ex.Message);
        //        ExitWithCode(_eCode);
        //        //MessageBox.Show(ex.ToString());
        //    }
        //}

        ///// <summary>
        ///// Sử dụng thread, ghi kết quả (là datatable) vào file text để webservice đọc
        ///// </summary>
        ///// <param name="arFileID"></param>
        ///// <param name="programName"></param>
        ///// <param name="userName"></param>
        ///// <param name="password"></param>
        //private void SignFilesByIDs_Thread_WriteLogFile(string arFileID, string programName, string userName, string password)
        //{
        //    //////test
        //    ////string file = "";
        //    ////for (int j = 52; j <= 1267; j++)
        //    ////{
        //    ////    if (file != "") file += ";";
        //    ////    file += j.ToString();
        //    ////}

        //    ////arFileID = file;

        //    DateTime startTime = DateTime.Now;

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
        //        DataTable dtFile = new DataTable("dtFile");       //Bảng chứa thông tin các file ký và kết quả ký

        //        #region Lấy thông tin người ký
        //        //Lấy thông tin certificate
        //        DataTable dtCert = bus.CA_Certificate_SelectForSign(programName, userName);
        //        cert = new X509Certificate2((byte[])dtCert.Rows[0]["RawData"]);
        //        int certID = Convert.ToInt32(dtCert.Rows[0]["CertID"]);

        //        //Toantk 26/8/2015: Tạm thời bỏ vì máy chủ không check được revocation
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

        //        //Khởi tạo kết nối HSM
        //        HSMServiceProvider.Initialize(Common.CRYPTOKI);

        //        //Thử đăng nhập
        //        using (HSMServiceProvider hsm = new HSMServiceProvider(slotSerial, HSMLoginRole.User, password))
        //        {
        //            hsm.LoadPrivateKeyByID(keyID);
        //        }

        //        //xin lệnh ký và thông tin file ký
        //        bool okToSign = bus.FL_File_SelectForAllowSign_Array(arFileID, programName, userName, ref dtFile);
        //        dtFile.TableName = "RESULTS";


        //        /*TEST*/

        //        //Mở session và load PrivateKey
        //        HSMServiceProvider provider = new HSMServiceProvider(slotSerial, HSMLoginRole.User, password);
        //        provider.LoadPrivateKeyByID(keyID);

        //        for (int index = 0; index < dtFile.Rows.Count; index++)
        //        {
        //            //////remove chữ ký
        //            ////int fileID1 = Convert.ToInt32(dtFile.Rows[index]["FileID"]);
        //            ////string filePath1 = bus.FL_File_GetFilePathForSign(fileID1);
        //            ////byte[] fileData1 = File.ReadAllBytes(filePath1);
        //            ////string fileExtension1 = Path.GetExtension(filePath1);

        //            ////using (ESDigitalSignatureManager dsm = new ESDigitalSignatureManager(fileData1, fileExtension1))
        //            ////{
        //            ////    dsm.RemoveAllSignature();
        //            ////    File.WriteAllBytes(filePath1, dsm.ToArray());
        //            ////}

        //            ////continue;

        //            //string tempFile = Path.GetTempFileName();
        //            //string tempPath = tempFile;
        //            ESignature signature;

        //            try
        //            {
        //                if (Convert.ToBoolean(dtFile.Rows[index]["OKtoSign"]) == true)
        //                {
        //                    int fileID = Convert.ToInt32(dtFile.Rows[index]["FileID"]);
        //                    string filePath = bus.FL_File_GetFilePathForSign(fileID);

        //                    //copy ra file tạm
        //                    //tempPath = tempFile + Path.GetExtension(filePath);
        //                    //File.Copy(filePath, tempPath, true);
        //                    byte[] fileData = File.ReadAllBytes(filePath);
        //                    string fileExtension = Path.GetExtension(filePath);

        //                    //byte[] ArrSigned = provider.SignData(Encoding.ASCII.GetBytes("RandomTextForTest"));
        //                    using (ESDigitalSignatureManager dsm = new ESDigitalSignatureManager(fileData, fileExtension))
        //                    {
        //                        //Kiểm tra chữ ký
        //                        //PA1: nếu đã có chữ ký của cert thì xóa và ký lại
        //                        //PA2 (chọn): nếu đã có chữ ký của cert thì ko ký và cập nhật chữ ký này vào db

        //                        dsm.Sign(cert, provider);
        //                        signature = dsm.Signatures[dsm.Signatures.Count - 1];

        //                        //Kiểm tra thời gian chờ trước khi cập nhật
        //                        if (bus.FL_File_SelectForSaveSign(Convert.ToInt32(dtFile.Rows[index]["ID_StatusLog"])))
        //                        {
        //                            // Lưu vào file đích
        //                            //File.Copy(tempPath, filePath, true);
        //                            File.WriteAllBytes(filePath, dsm.ToArray());

        //                            //Ghi log
        //                            bus.FL_File_UpdateForLogSign(fileID, signature.Signer.SerialNumber, signature.SigningTime, (int)signature.Verify,
        //                                1, (int)FileSignActions.AddSignature, filePath, "Hoàn thành ký file qua HSM", programName, userName);

        //                            //Truyền transaction vào bus.FL_File_UpdateForLogSign
        //                            //File.Copy
        //                            //transaction.Commit()

        //                            //Báo thành công
        //                            dtFile.Rows[index]["SignResults"] = (int)FileSignResults.Success;
        //                            dtFile.Rows[index]["SignDetails"] = "Ký văn bản thành công.";

        //                            /*Cập nhật vào db của hệ thống riêng ở đây*/
        //                        }
        //                        else
        //                        {
        //                            //bus.FL_File_UpdateStatus(fileID, (int)FileStatus.LuuFile, "Hủy ký", programName, userName);
        //                            dtFile.Rows[index]["SignResults"] = (int)FileSignResults.SaveTimeOut;
        //                            dtFile.Rows[index]["SignDetails"] = "Ký văn bản thất bại: Hết thời gian chờ lưu file.";
        //                        }
        //                    }
        //                }
        //                else
        //                {
        //                    //Nếu có 1 thằng không được ký (do 1 lỗi nào đó), trạng thái đã có sẵn, chỉ cần cập nhật lại kết quả chung thôi
        //                    _eCode = CAExitCode.Error;
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                _eCode = GetErrorCodeFromString(ex.Message);

        //                //bus.FL_File_UpdateStatus(fileID, (int)FileStatus.LuuFile, "Hủy ký", programName, userName);
        //                dtFile.Rows[index]["SignResults"] = (int)FileSignResults.FileSignFailed;
        //                dtFile.Rows[index]["SignDetails"] = "Ký văn bản thất bại: " + ex.Message;
        //            }
        //        }

        //        //Kết thúc
        //        HSMServiceProvider.Finalize();

        //        //export ra file text
        //        Process curProcess = Process.GetCurrentProcess();
        //        string directoryLog = Path.Combine(Application.StartupPath, "Result");
        //        string fileLog = Path.Combine(directoryLog, curProcess.Id.ToString() + ".txt");

        //        if (!Directory.Exists(directoryLog))
        //            Directory.CreateDirectory(directoryLog);

        //        if (File.Exists(fileLog))
        //            File.Delete(fileLog);

        //        File.Create(fileLog).Dispose();

        //        dtFile.WriteXml(fileLog, XmlWriteMode.WriteSchema);

        //        MessageBox.Show((DateTime.Now - startTime).TotalSeconds.ToString());

        //        ExitWithCode(_eCode);

        //        return;

        //        /*END TEST*/

        //        #region Comment phần Thread
        //        ////tối đa là xxx thread, số lượng file ký sẽ được truyền vào thread
        //        //int threadDone = 0;
        //        //int maxThread = 10;
        //        //if (dtFile.Rows.Count < maxThread)
        //        //    maxThread = 1;
        //        //int filePerThread = (int)Math.Ceiling((double)dtFile.Rows.Count / maxThread);

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
        //        //                if (index >= dtFile.Rows.Count) return;

        //        //                //string tempFile = Path.GetTempFileName();
        //        //                //string tempPath = tempFile;
        //        //                ESignature signature;

        //        //                try
        //        //                {
        //        //                    if (Convert.ToBoolean(dtFile.Rows[index]["OKtoSign"]) == true)
        //        //                    {
        //        //                        int fileID = Convert.ToInt32(dtFile.Rows[index]["FileID"]);
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
        //        //                            if (bus.FL_File_SelectForSaveSign(Convert.ToInt32(dtFile.Rows[index]["ID_StatusLog"])))
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
        //        //                                dtFile.Rows[index]["SignResults"] = (int)FileSignResults.Success;
        //        //                                dtFile.Rows[index]["SignDetails"] = "Ký văn bản thành công.";

        //        //                                /*Cập nhật vào db của hệ thống riêng ở đây*/
        //        //                            }
        //        //                            else
        //        //                            {
        //        //                                //bus.FL_File_UpdateStatus(fileID, (int)FileStatus.LuuFile, "Hủy ký", programName, userName);
        //        //                                dtFile.Rows[index]["SignResults"] = (int)FileSignResults.SaveTimeOut;
        //        //                                dtFile.Rows[index]["SignDetails"] = "Ký văn bản thất bại: Hết thời gian chờ lưu file.";
        //        //                            }
        //        //                        }
        //        //                    }
        //        //                    else
        //        //                    {
        //        //                        //Nếu có 1 thằng không được ký (do 1 lỗi nào đó), trạng thái đã có sẵn, chỉ cần cập nhật lại kết quả chung thôi
        //        //                        _eCode = CAExitCode.Error;
        //        //                    }
        //        //                }
        //        //                catch (Exception ex)
        //        //                {
        //        //                    _eCode = GetErrorCodeFromString(ex.Message);

        //        //                    //bus.FL_File_UpdateStatus(fileID, (int)FileStatus.LuuFile, "Hủy ký", programName, userName);
        //        //                    dtFile.Rows[index]["SignResults"] = (int)FileSignResults.FileSignFailed;
        //        //                    dtFile.Rows[index]["SignDetails"] = "Ký văn bản thất bại: " + ex.Message;
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

        //        //                dtFile.WriteXml(fileLog, XmlWriteMode.WriteSchema);

        //        //                ExitWithCode(_eCode);
        //        //            }
        //        //            bgw.Dispose();
        //        //        };
        //        //        bgw.RunWorkerAsync();
        //        //        #endregion

        //        //        #region Minhdn 18/8/2015
        //        //        //for (int count = 0; count < filePerThread; count++)
        //        //        //{
        //        //        //    int index = filePerThread * j + count;
        //        //        //    if (index >= dtFile.Rows.Count) return;

        //        //        //    string tempFile = Path.GetTempFileName();
        //        //        //    string tempPath = tempFile;
        //        //        //    ESignature signature;

        //        //        //    try
        //        //        //    {
        //        //        //        if (Convert.ToBoolean(dtFile.Rows[index]["OKtoSign"]) == true)
        //        //        //        {
        //        //        //            int fileID = Convert.ToInt32(dtFile.Rows[index]["FileID"]);
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
        //        //        //                        dtFile.Rows[index]["SignResults"] = (int)FileSignResults.FileSignFailed;
        //        //        //                        dtFile.Rows[index]["SignDetails"] = "Ký văn bản thất bại";
        //        //        //                        continue;
        //        //        //                    }
        //        //        //                    else
        //        //        //                    {
        //        //        //                        signature = dsm1.Signatures[curSignCount];
        //        //        //                    }
        //        //        //                }
        //        //        //            }

        //        //        //            if (bus.FL_File_SelectForSaveSign(Convert.ToInt32(dtFile.Rows[index]["ID_StatusLog"])))
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
        //        //        //                dtFile.Rows[index]["SignResults"] = (int)FileSignResults.Success;
        //        //        //                dtFile.Rows[index]["SignDetails"] = "Ký văn bản thành công.";

        //        //        //                /*Cập nhật vào db của hệ thống riêng ở đây*/
        //        //        //            }
        //        //        //            else
        //        //        //            {
        //        //        //                //Kiểm tra thời gian chờ trước khi cập nhật
        //        //        //                //bus.FL_File_UpdateStatus(fileID, (int)FileStatus.LuuFile, "Hủy ký", programName, userName);
        //        //        //                dtFile.Rows[index]["SignResults"] = (int)FileSignResults.SaveTimeOut;
        //        //        //                dtFile.Rows[index]["SignDetails"] = "Ký văn bản thất bại: Hết thời gian chờ lưu file.";
        //        //        //            }
        //        //        //        }
        //        //        //        else
        //        //        //        {
        //        //        //            //Nếu có 1 thằng không được ký (do 1 lỗi nào đó), trạng thái đã có sẵn, chỉ cần cập nhật lại kết quả chung thôi
        //        //        //            _eCode = CAExitCode.Error;
        //        //        //        }
        //        //        //    }
        //        //        //    catch (Exception ex)
        //        //        //    {
        //        //        //        _eCode = GetErrorCodeFromString(ex.Message);

        //        //        //        //bus.FL_File_UpdateStatus(fileID, (int)FileStatus.LuuFile, "Hủy ký", programName, userName);
        //        //        //        dtFile.Rows[index]["SignResults"] = (int)FileSignResults.FileSignFailed;
        //        //        //        dtFile.Rows[index]["SignDetails"] = "Ký văn bản thất bại: " + ex.Message;
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

        //        //        //    dtFile.WriteXml(fileLog, XmlWriteMode.WriteSchema);

        //        //        //    ExitWithCode(_eCode);
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
        //        _eCode = GetErrorCodeFromString(ex.Message);
        //        ExitWithCode(_eCode);
        //    }
        //}
        #endregion

        private void ChangePIN(string programName, string userName, string oldPIN, string newPIN)
        {
            //mã trả về
            CAExitCode _eCode = CAExitCode.Success;
            DataTable dtPK = new DataTable();

            try
            {
                //CSDL
                DAL_SqlConnector.ConnectionString = GetDatabaseString();
                BUSQuanTri bus = new BUSQuanTri();

                //Lấy thông tin certificate
                DataTable dtCert = bus.CA_Certificate_SelectByProgUser(programName, userName);
                if (dtCert.Rows.Count < 1)
                    ExitWithCode(CAExitCode.WS_CertificateNotFoundOrInvalid);
                else if (dtCert.Rows.Count > 1)
                    ExitWithCode(CAExitCode.WS_CertificateMoreThanOne);

                //Lấy danh sách cert -> object -> slot
                dtPK = bus.HSM_Slot_SelectFormObjectByCertID(Convert.ToInt32(dtCert.Rows[0]["CertID"]));
                if (dtPK.Rows.Count < 1)
                    _eCode = CAExitCode.WS_UserSlotNotFound;

                //Kết nối HSM
                HSMServiceProvider.Initialize(Common.CRYPTOKI);
                foreach (DataRow dr in dtPK.Rows)
                {
                    int slotID = Convert.ToInt32(dr["SlotID"]);
                    string serial = dr["SlotSerial"].ToString();

                    using (HSMServiceProvider hsm = new HSMServiceProvider())
                    {
                        //Đăng nhập
                        HSMReturnValue ret = hsm.Login(serial, HSMLoginRole.User, oldPIN);
                        if (ret == HSMReturnValue.PIN_LEN_RANGE)
                            ExitWithCode(CAExitCode.HSM_PinLenRange);
                        else if (ret == HSMReturnValue.PIN_INCORRECT)
                            ExitWithCode(CAExitCode.HSM_PinIncorrect);
                        else if (ret == HSMReturnValue.PIN_LOCKED)
                            ExitWithCode(CAExitCode.HSM_PinLocked);
                        else if (ret != HSMReturnValue.OK)
                            ExitWithCode(CAExitCode.Error);
                        //Đổi mật khẩu
                        ret = hsm.ChangeSlotPIN(oldPIN, newPIN);
                        if (ret != HSMReturnValue.OK)
                            ExitWithCode(CAExitCode.Error);
                        //Lưu db
                        bus.HSM_Slot_UpdateUserPIN(slotID, ESLogin.StringCryptor.EncryptString(newPIN));
                    }
                }

                ExitWithCode(_eCode);
            }
            catch (Exception ex)
            {
                //Nếu lỗi thì thử hồi lại mật khẩu cũ
                foreach (DataRow dr in dtPK.Rows)
                {
                    int slotID = Convert.ToInt32(dr["SlotID"]);
                    string serial = dr["SlotSerial"].ToString();

                    using (HSMServiceProvider hsm = new HSMServiceProvider())
                    {
                        //Đăng nhập
                        HSMReturnValue ret = hsm.Login(serial, HSMLoginRole.User, newPIN);
                        if (ret == HSMReturnValue.OK)
                            ret = hsm.ChangeSlotPIN(newPIN, oldPIN);
                    }
                }

                ExitWithCode(CAExitCode.Error);
            }
            finally
            {
                HSMServiceProvider.Finalize();
            }
        }

        /// <summary>
        /// Kiểm tra xem User và password có đúng hay không
        /// </summary>
        /// <param name="a_progName"></param>
        /// <param name="a_userProgName"></param>
        /// <param name="a_passwordHSM"></param>
        private void CheckLoginHSM(string a_progName, string a_userProgName, string a_passwordHSM)
        {
            //mã trả về
            HSMReturnValue ret = HSMReturnValue.OK;

            try
            {
                //Thư viện HSM
                Common.CRYPTOKI = GetCryptokiDLL();
                //CSDL
                DAL_SqlConnector.ConnectionString = GetDatabaseString();
                BUSQuanTri bus = new BUSQuanTri();

                //Lấy thông tin certificate
                DataTable dtCert = bus.CA_Certificate_SelectForSign(a_progName, a_userProgName);
                X509Certificate2 cert = new X509Certificate2((byte[])dtCert.Rows[0]["RawData"]);
                int certID = Convert.ToInt32(dtCert.Rows[0]["CertID"]);

                ////Xác thực chứng thư với nhà cung cấp CA
                //X509ChainStatus certificateStatus;
                //if (!Common.ValidateCertificate(cert, DateTime.Now, out certificateStatus))
                //{
                //    MessageBox.Show(certificateStatus.StatusInformation);
                //    ExitWithCode(CAExitCode.WS_CertificateInvalidInCA);
                //}

                //Lấy key trong HSM
                //LƯU Ý: dùng hàm HSM_WLDObject_SelectPrivateKeyByCertID nếu chạy ở chế độ WLD
                DataTable dtPK = bus.HSM_Object_SelectPrivateKeyByCertID(Convert.ToInt32(dtCert.Rows[0]["CertID"]));
                string slotSerial = dtPK.Rows[0]["SlotSerial"].ToString();
                byte[] keyID = (byte[])dtPK.Rows[0]["CKA_ID"];

                //Khởi tạo giao tiếp HSM và đăng nhập
                using (HSMServiceProvider provider = new HSMServiceProvider(Common.CRYPTOKI))
                {
                    ret = provider.Login(slotSerial, HSMLoginRole.User, a_passwordHSM);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                Environment.Exit(1989);
            }

            Environment.Exit((int)ret);
        }

        /// <summary>
        /// Dừng app và trả về ExitCode
        /// </summary>
        /// <param name="code"></param>
        private void ExitWithCode(CAExitCode code)
        {
            //MessageBox.Show((DateTime.Now - startTime).TotalSeconds.ToString());
            int ret = (int)code;
            Environment.Exit((int)code);
        }

        /// <summary>
        /// Trả về mã lỗi từ Exception
        /// </summary>
        /// <param name="exMessage"></param>
        /// <returns></returns>
        private CAExitCode GetErrorCodeFromString(string exMessage)
        {
            switch (exMessage)
            {
                case "WS_FileNotFound":
                    return CAExitCode.WS_FileNotFound;
                case "WS_FileNotSaved":
                    return CAExitCode.WS_FileNotSaved;
                case "WS_CertificateNotFoundOrInvalid":
                    return CAExitCode.WS_CertificateNotFoundOrInvalid;
                case "WS_CertificateMoreThanOne":
                    return CAExitCode.WS_CertificateMoreThanOne;
                case "WS_PrivateKeyNotFound":
                    return CAExitCode.WS_PrivateKeyNotFound;
                case "WS_PrivateKeyMoreThanOne":
                    return CAExitCode.WS_PrivateKeyMoreThanOne;
                case "WS_FileInSign":
                    return CAExitCode.WS_FileInSign;
                case "WS_FileReplaced":
                    return CAExitCode.WS_FileReplaced;
                case "WS_CertificateInvalidInCA":
                    return CAExitCode.WS_CertificateInvalidInCA;
                case "WS_SaveFileTimeOut":
                    return CAExitCode.WS_SaveFileTimeOut;
                case "HSM_PinIncorrect":
                    return CAExitCode.HSM_PinIncorrect;
                case "HSM_LoginFailed":
                    return CAExitCode.HSM_LoginFailed;
                case "HSM_DuplicateKey":
                    return CAExitCode.HSM_DuplicateKey;
                case "HSM_KeyTypeNotSupported":
                    return CAExitCode.HSM_KeyTypeNotSupported;
                case "HSM_KeyNotFound":
                    return CAExitCode.HSM_KeyNotFound;
                case "HSM_PinLocked":
                    return CAExitCode.HSM_PinLocked;
                case "HSM_PinExpired":
                    return CAExitCode.HSM_PinExpired;

                default:
                    return CAExitCode.Error;
            }
        }

        /// <summary>
        /// Convert String thành enum FileSignResults
        /// </summary>
        /// <param name="exMessage"></param>
        /// <returns></returns>
        private FileSignResults GetResultFromString(string exMessage)
        {
            switch (exMessage)
            {
                case "FileNotFound":
                    return FileSignResults.FileNotFound;
                case "FileNotSaved":
                    return FileSignResults.FileNotSaved;
                case "FileReplaced":
                    return FileSignResults.FileReplaced;
                case "FileInSignProgress":
                    return FileSignResults.FileInSignProgress;
                case "FileSignedByUnit":
                    return FileSignResults.FileSignedByUnit;
                case "FileSignFailed":
                    return FileSignResults.FileSignFailed;
                case "SaveTimeOut":
                    return FileSignResults.SaveTimeOut;
                case "Success":
                    return FileSignResults.Success;
                case "InvalidSignature":
                    return FileSignResults.InvalidSignature;
                case "NotSigned":
                    return FileSignResults.NotSigned;
                default:
                    return FileSignResults.Success;
            }
        }

        /// <summary>
        /// Lấy đường dẫn CRYPTOKI
        /// </summary>
        /// <returns></returns>
        private string GetCryptokiDLL()
        {
            //string localPath = "";
            //if (Environment.Is64BitProcess)
            //    localPath = @"cryptoki_win64\cryptoki.dll";
            //else
            //    localPath = @"cryptoki_win32\cryptoki.dll";
            //string dllPath = Path.Combine(Application.StartupPath, localPath);
            return "cryptoki.dll";
        }

        /// <summary>
        /// Lấy chuỗi kết nối DB
        /// </summary>
        /// <returns></returns>
        private string GetDatabaseString()
        {
            //return "Data Source=10.8.32.30;Initial Catalog=CA;User ID=sa;Password=sa123;";
            return ConfigurationManager.ConnectionStrings["Database_CA"].ConnectionString;
        }

        private void tmrTimeout_Tick(object sender, EventArgs e)
        {
            try
            {
                runner++;

                if (runner >= a_Timeout)
                {
                    ExitWithCode(CAExitCode.WS_Timeout);
                }
            }
            catch
            {
                ExitWithCode(CAExitCode.Error);
            }
        }

        private DataTable createDataTableFiles()
        {
            DataTable dt = new DataTable("FILES");
            dt.Columns.Add("FileID");
            dt.Columns.Add("FileSourcePath");//file nguồn
            dt.Columns.Add("FileTempSysPath");//file tạm sinh bởi hệ thống
            dt.Columns.Add("FileTempSignPath");//file copy từ file source với tên file tạm
            return dt;
        }

        private void writeLog(string Message)
        {
            try
            {
                //lấy đường dẫn thư mục
                string logFolder = Path.Combine(Application.StartupPath, "Logs");
                if (!Directory.Exists(logFolder))
                    Directory.CreateDirectory(logFolder);

                string logFile = Path.Combine(logFolder, "logCA.log");
                using (StreamWriter sw = File.AppendText(logFile))
                {
                    sw.WriteLine("- {0}: {1}", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"), Message);
                }
            }
            catch
            {
            }
        }
    }
}


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Windows.Forms;
using ES.CA_WebServiceBUS;
using ES.CA_WebServiceDAL;
using esDigitalSignature;
using esDigitalSignature.Library;

namespace CA_AppService
{
    public partial class frmTest : Form
    {
        private int runner = 0;
        DateTime startTime;

        public frmTest()
        {
            InitializeComponent();

            this.Height = 280 - 150;
            imgLoading.Visible = false;
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
        /// Chương tình có ghi ra output
        /// </summary>
        bool a_Output = false;
        /// <summary>
        /// thời gian time out của chương trình, mặc định là 300s
        /// </summary>
        int a_Timeout = 300;
        #endregion

        private void ExitWithCode(CAExitCode code)
        {
            this.Height = 280 - 150;
            imgLoading.Visible = false;
            MessageBox.Show((DateTime.Now - startTime).TotalSeconds.ToString());
            //Environment.Exit((int)code);
        }

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
                default:
                    return CAExitCode.Error;
            }
        }

        private string GetCryptokiDLL()
        {
            string localPath = "";
            if (Environment.Is64BitProcess)
                localPath = @"cryptoki_win64\cryptoki.dll";
            else
                localPath = @"cryptoki_win32\cryptoki.dll";
            string dllPath = Path.Combine(Application.StartupPath, localPath);
            return dllPath;
        }

        private string GetDatabaseString()
        {
            return "Data Source=192.168.0.48;Initial Catalog=CA;User ID=cadev;Password=cadev123;";
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

        private void btnSign_Click(object sender, EventArgs e)
        {
            startTime = DateTime.Now;

            //test
            string file = "";
            for (int j = 52; j <= 1267; j++)
            {
                if (file != "") file += ";";
                file += j.ToString();
            }

            string arFileID = file;

            //mã trả về
            CAExitCode _eCode = CAExitCode.Success;

            try
            {
                //Thư viện HSM
                Common.CRYPTOKI = GetCryptokiDLL();
                //CSDL
                DAL_SqlConnector.ConnectionString = GetDatabaseString();
                BUSQuanTri bus = new BUSQuanTri();
                //Tham số
                X509Certificate2 cert;  //Chứng thư ký
                //string filePath = "";   //Đường dẫn file
                //ESignature signature;   //Lưu chữ ký vừa tạo

                #region Lấy thông tin người ký
                //Lấy thông tin certificate
                DataTable dtCert = bus.CA_Certificate_SelectForSign("TTTT_A0", "sonnc1");
                cert = new X509Certificate2((byte[])dtCert.Rows[0]["RawData"]);
                int certID = Convert.ToInt32(dtCert.Rows[0]["CertID"]);

                //BUILD: Tạm thời comment để chạy cho Ninhtq
                ////Xác thực chứng thư với nhà cung cấp CA
                //X509ChainStatus certificateStatus;
                //if (!Common.ValidateCertificate(cert, DateTime.Now, out certificateStatus))
                //    ExitWithCode(CAExitCode.WS_CertificateInvalidInCA);

                //Lấy key trong HSM
                //BUILD: dùng hàm HSM_WLDObject_SelectPrivateKeyByCertID nếu chạy ở chế độ WLD
                DataTable dtPK = bus.HSM_Object_SelectPrivateKeyByCertID(certID);
                string slotSerial = dtPK.Rows[0]["SlotSerial"].ToString();
                byte[] keyID = (byte[])dtPK.Rows[0]["CKA_ID"];
                #endregion

                //Khởi tạo kết nối HSM
                HSMServiceProvider.Initialize(Common.CRYPTOKI);

                //Duyệt từng fileID và ký
                string[] fileIDs = arFileID.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

                //tạo Dictionary chứa kết quả
                int fileSignedCount = 0;
                DataTable dtFile = createDataTableFiles();

                foreach (string _fileID in fileIDs)
                {
                    try
                    {
                        DataRow r = dtFile.NewRow();
                        r["FileID"] = _fileID;
                        string _filePath = bus.FL_File_GetFilePathForSign(int.Parse(_fileID));
                        string tempFile = Path.GetTempFileName();

                        r["FileSourcePath"] = _filePath;
                        r["FileTempSysPath"] = tempFile;
                        r["FileTempSignPath"] = tempFile + Path.GetExtension(_filePath);
                        dtFile.Rows.Add(r);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(_fileID + CA_Variable.SignFile_Result_Separator + "Error: " + ex.Message);
                    }
                }
                try
                {
                    //tối đa là 30 thread, số lượng file ký sẽ được truyền vào thread
                    int maxThread = Convert.ToInt32(txtThread.Text.Trim());
                    int filePerThread = (int)Math.Ceiling((double)dtFile.Rows.Count / maxThread);
                    for (int i = 0; i < maxThread; i++)
                    {
                        int j = i;
                        int countFile = 0;

                        try
                        {
                            //Mở session
                            HSMServiceProvider provider = new HSMServiceProvider(slotSerial, HSMLoginRole.User, "123456");

                            provider.LoadPrivateKeyByID(keyID);

                            BackgroundWorker bgw = new BackgroundWorker();
                            bgw.DoWork += delegate
                            {

                                for (int count = 1; count <= filePerThread; count++)
                                {
                                    int index = filePerThread * j + count;
                                    if (index >= dtFile.Rows.Count) return;

                                    try
                                    {
                                        int fileID = Convert.ToInt32(dtFile.Rows[index]["FileID"]);
                                        //string tempPath = dtFile.Rows[index]["FileTempSignPath"].ToString();
                                        string filePath = dtFile.Rows[index]["FileSourcePath"].ToString();
                                        //string temp = dtFile.Rows[index]["FileTempSysPath"].ToString();

                                        /////Ký*********************************
                                        //File.Copy(filePath, tempPath, true);
                                        byte[] fileData = File.ReadAllBytes(filePath);
                                        string fileExtension = Path.GetExtension(filePath);

                                        //byte[] ArrSigned = provider.SignData(Encoding.ASCII.GetBytes("RandomTextForTest"));
                                        using (ESDigitalSignatureManager dsm = new ESDigitalSignatureManager(fileData, fileExtension))
                                        {
                                            dsm.Sign(cert, provider);
                                            File.WriteAllBytes(filePath, dsm.ToArray());
                                            //dsm.RemoveAllSignature();
                                        }

                                        //File.Copy(tempPath, filePath, true);

                                        ////xóa các file tạm?
                                        //File.Delete(temp);
                                        //File.Delete(tempPath);
                                        ///*****************************************
                                        ///////Bỏ chữ ký*******************************
                                        //using (ESDigitalSignatureManager dsm = new ESDigitalSignatureManager(filePath))
                                        //{
                                        //    dsm.RemoveAllSignature();
                                        //}
                                        ///////********************************************

                                        Console.WriteLine(fileID.ToString() + CA_Variable.SignFile_Result_Separator + "Success");
                                    }
                                    catch (Exception ex)
                                    {
                                        _eCode = GetErrorCodeFromString(ex.Message);

                                        //Console.WriteLine(fileID.ToString() + CA_Variable.SignFile_Result_Separator + "Error: " + ex.Message);
                                        Console.WriteLine(dtFile.Rows[index]["FileID"].ToString() + CA_Variable.SignFile_Result_Separator + "Error " + index.ToString() + ": " + ex.Message);
                                    }
                                }
                            };
                            bgw.RunWorkerCompleted += delegate
                            {
                                fileSignedCount++;
                                countFile++;

                                if (countFile == filePerThread)
                                {
                                    provider.Dispose();
                                }
                                //Đã ký hết các file
                                if (fileSignedCount == maxThread)
                                {
                                    //Kết thúc
                                    HSMServiceProvider.Finalize();

                                    ExitWithCode(_eCode);
                                }
                                bgw.Dispose();
                            };
                            bgw.RunWorkerAsync();
                        }
                        catch
                        {

                        }
                    }

                    this.Height = 280;
                    imgLoading.Visible = true;
                    //Application.DoEvents();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message, ex);
                }
                finally
                {

                }
            }
            catch (Exception ex)
            {
                _eCode = GetErrorCodeFromString(ex.Message);
                ExitWithCode(_eCode);
                //MessageBox.Show(ex.ToString());
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            startTime = DateTime.Now;

            //test
            string file = "";
            for (int j = 52; j <= 1267; j++)
            {
                if (file != "") file += ";";
                file += j.ToString();
            }

            string arFileID = file;

            //mã trả về
            CAExitCode _eCode = CAExitCode.Success;

            try
            {
                //Thư viện HSM
                Common.CRYPTOKI = GetCryptokiDLL();
                //CSDL
                DAL_SqlConnector.ConnectionString = GetDatabaseString();
                BUSQuanTri bus = new BUSQuanTri();
                //Tham số
                X509Certificate2 cert;  //Chứng thư ký

                //Khởi tạo kết nối HSM
                HSMServiceProvider.Initialize(Common.CRYPTOKI);

                //Duyệt từng fileID và ký
                string[] fileIDs = arFileID.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

                //tạo Dictionary chứa kết quả
                int fileSignedCount = 0;
                DataTable dtFile = createDataTableFiles();

                foreach (string _fileID in fileIDs)
                {
                    try
                    {
                        DataRow r = dtFile.NewRow();
                        r["FileID"] = _fileID;
                        string _filePath = bus.FL_File_GetFilePathForSign(int.Parse(_fileID));
                        string tempFile = Path.GetTempFileName();

                        r["FileSourcePath"] = _filePath;
                        r["FileTempSysPath"] = tempFile;
                        r["FileTempSignPath"] = tempFile + Path.GetExtension(_filePath);

                        dtFile.Rows.Add(r);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(_fileID + CA_Variable.SignFile_Result_Separator + "Error: " + ex.Message);
                    }
                }
                try
                {
                    //tối đa là 30 thread, số lượng file ký sẽ được truyền vào thread
                    int maxThread = Convert.ToInt32(txtThread.Text.Trim());
                    int filePerThread = (int)Math.Ceiling((double)dtFile.Rows.Count / maxThread);
                    for (int i = 0; i < maxThread; i++)
                    {
                        int j = i;

                        try
                        {
                            BackgroundWorker bgw = new BackgroundWorker();
                            bgw.DoWork += delegate
                            {

                                for (int count = 1; count <= filePerThread; count++)
                                {
                                    int index = filePerThread * j + count;
                                    if (index >= dtFile.Rows.Count) return;

                                    try
                                    {
                                        int fileID = Convert.ToInt32(dtFile.Rows[index]["FileID"]);
                                        string tempPath = dtFile.Rows[index]["FileTempSignPath"].ToString();
                                        string filePath = dtFile.Rows[index]["FileSourcePath"].ToString();
                                        string temp = dtFile.Rows[index]["FileTempSysPath"].ToString();

                                        ///////Bỏ chữ ký*******************************
                                        byte[] fileData = File.ReadAllBytes(filePath);
                                        string fileExtension = Path.GetExtension(filePath);

                                        using (ESDigitalSignatureManager dsm = new ESDigitalSignatureManager(fileData, fileExtension))
                                        {
                                            dsm.RemoveAllSignature();
                                            File.WriteAllBytes(filePath, dsm.ToArray());
                                        }
                                        ///////********************************************

                                        Console.WriteLine(fileID.ToString() + CA_Variable.SignFile_Result_Separator + "Success");
                                    }
                                    catch (Exception ex)
                                    {
                                        _eCode = GetErrorCodeFromString(ex.Message);

                                        //Console.WriteLine(fileID.ToString() + CA_Variable.SignFile_Result_Separator + "Error: " + ex.Message);
                                        Console.WriteLine(dtFile.Rows[index]["FileID"].ToString() + CA_Variable.SignFile_Result_Separator + "Error " + index.ToString() + ": " + ex.Message);
                                    }
                                }
                            };
                            bgw.RunWorkerCompleted += delegate
                            {
                                fileSignedCount++;

                                //Đã ký hết các file
                                if (fileSignedCount == maxThread)
                                {
                                    //Kết thúc
                                    HSMServiceProvider.Finalize();

                                    ExitWithCode(_eCode);
                                }
                                bgw.Dispose();
                            };
                            bgw.RunWorkerAsync();
                        }
                        catch
                        {

                        }
                    }

                    this.Height = 280;
                    imgLoading.Visible = true;
                    //Application.DoEvents();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message, ex);
                }
                finally
                {

                }
            }
            catch (Exception ex)
            {
                _eCode = GetErrorCodeFromString(ex.Message);
                ExitWithCode(_eCode);
                //MessageBox.Show(ex.ToString());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //CSDL
            //DAL_SqlConnector.ConnectionString = GetDatabaseString();
            BUSQuanTri bus = new BUSQuanTri(ConfigurationManager.ConnectionStrings["Database_CA"].ConnectionString);

            List<string> arrHex = new List<string>();

            //Lấy thông tin certificate
            DataTable dtCert = bus.CA_Certificate_SelectForValid("DIM", "G27500");
            foreach (DataRow dr in dtCert.Rows)
            {
                byte[] rawData = (byte[])dr["RawData"];
                arrHex.Add(Common.ConvertBytesToHex(rawData));
            }

            return;
        }
    }
}


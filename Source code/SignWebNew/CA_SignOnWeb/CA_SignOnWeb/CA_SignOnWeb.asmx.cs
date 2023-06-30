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
using System.Security.Principal;
using System.Threading;
using System.Data.SqlClient;
using System.Security.Permissions;
using System.Web.Security;
using System.Windows.Forms;
using ES.CA_SignOnWeb_DAL;
using ES.CA_SignOnWeb_BUS;
using esDigitalSignature;
using esDigitalSignature.Library;
using System.DirectoryServices;
using System.Transactions;

namespace ES.CA_SignOnWeb
{
    /// <summary>
    /// Summary description for CAService
    /// </summary>
    [WebService(Namespace = "http://ca-nldc.vn/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.None)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class CA_SignOnWeb : System.Web.Services.WebService
    {
        private enum TypeLink
        {
            Internet,
            WLAN
        }

        private enum TypeSignOverWeb
        {
            CreateFileServer,
            HaveFileID,
            SaveFileInDB
        }

        private enum TypeOutputToDB
        {
            CreateFile,
            Base64,
            FileDataSign,
            FileID,
            FilePath,
            FileName,
            Other
        }

        #region Đăng nhập Webservice
        [WebMethod(Description = "Đăng nhập Webservice")]
        public bool LogIn(string username, string password)
        {
            try
            {
                DateTime defaultTime = new DateTime(2012, 7, 1);
                DateTime nowTime = DateTime.Now;
                TimeSpan ts = nowTime - defaultTime;
                double iTime = ts.TotalSeconds;

                ES_Encrypt enc = new ES_Encrypt();
                double iTimePass = 0;
                if (Double.TryParse(enc.DecryptString(password), out iTimePass))
                {
                    if (Math.Abs(iTimePass - iTime) < 300)
                    {
                        FormsAuthentication.SetAuthCookie(username, false);
                        return true;
                    }
                }

                return false;
            }
            catch
            {
                return false;
            }
        }

        [WebMethod(Description = "Đăng xuất Webservice")]
        [PrincipalPermission(SecurityAction.Demand, Authenticated = true)]
        public bool LogOut(string key)
        {
            if (HttpContext.Current.User.Identity.IsAuthenticated)
                FormsAuthentication.SignOut();
            try
            {
                DAL_SqlConnector.ConnectionString = GetDatabaseString();
                BUSQuanTri bus = new BUSQuanTri();
                bus.CA_DataSign_CA_DataSignForDB_DeleteByKey(key);
            }
            catch { }
            return true;
        }
        #endregion

        [WebMethod(Description = "Hàm lấy thông tin để ký")]
        [PrincipalPermission(SecurityAction.Demand, Authenticated = true)]
        public bool GetInfoToSign(string SignMessage, ref string strErr, out DataTable dtResult)
        {
            try
            {
                #region Kiểm tra chuỗi string đầu vào và lấy thông tin xin ký
                //Xóa dữ liệu bảng đầu vào
                dtResult = new DataTable("RESULTS");
                //Giải mã chuỗi khi nhận
                ES_Encrypt enc = new ES_Encrypt();
                string strDecryptMessage = enc.DecryptString(SignMessage);
                string[] arrayDecrypt = strDecryptMessage.Split('@');

                //Kiểm tra thông tin
                if (arrayDecrypt.Length != 5)
                {
                    strErr = "Thông tin ký không đúng!";
                    goto ReturnFalse;
                }

                //CSDL
                DAL_SqlConnector.ConnectionString = GetDatabaseString();
                BUSQuanTri bus = new BUSQuanTri();

                //Lấy danh sách file
                string key = arrayDecrypt[4];
                DataTable dtFile = bus.CA_DataSign_SelectByKey(key);

                //Kiểm tra số file ký
                int NumberFile = 0;
                if (Int32.TryParse(arrayDecrypt[2], out NumberFile) && dtFile.Rows.Count != NumberFile)
                {
                    strErr = "Sai lệch số văn bản thực hiện ký!";
                    goto ReturnFalse;
                }
                #endregion

                #region Lấy thông tin trả về cho app tại client
                //Kiểm tra loại ký
                string userName = arrayDecrypt[0];
                string programName = arrayDecrypt[1];
                string typeSign = arrayDecrypt[3];
                if (typeSign.Split('.')[1] == TypeSignOverWeb.CreateFileServer.ToString())
                    goto ToCreateFileServer;
                else if (typeSign.Split('.')[1] == TypeSignOverWeb.HaveFileID.ToString())
                    goto ToHaveFileID;
                else if (typeSign.Split('.')[1] == TypeSignOverWeb.SaveFileInDB.ToString())
                    goto ToCreateFileServer;
                else
                {
                    strErr = "Sai lệnh thông tin loại ký!";
                    goto ReturnFalse;
                }

            ToHaveFileID:
                {
                    string arrFileIDs = "";
                    for (int i = 0; i < dtFile.Rows.Count; i++)
                    {
                        arrFileIDs += dtFile.Rows[i]["FileID"].ToString() + ";";
                    }
                    //Kiểm tra trạng thái file và thiết lập trạng thái chờ
                    if (bus.FL_File_SelectForAllowSign_Array_New(arrFileIDs, programName, userName, ref dtResult))
                    {
                        dtResult.Columns.Add("ExtensionFile");
                        //Lấy dữ liệu file
                        foreach (DataRow dr in dtResult.Rows)
                        {
                            if (Convert.ToBoolean(dr["OKtoSign"]))
                            {
                                string filePath = dr["FilePath"].ToString();
                                dr["ExtensionFile"] = Path.GetExtension(filePath).ToLower();
                                dr["FileData"] = File.ReadAllBytes(filePath);
                            }
                        }
                    }
                    else
                    {
                        strErr = "Kiểm tra trạng thái file thực hiện ký thất bại!\nHãy chờ 2 phút để thực hiện lại quá trình ký!!";
                        goto ReturnFalse;
                    }
                    goto ReturnTrue;
                }

            ToCreateFileServer:
                {
                    //Tạo bảng kết quả
                    dtResult.Columns.Add("FilePath", typeof(string));
                    dtResult.Columns.Add("FileData", typeof(byte[]));
                    dtResult.Columns.Add("OKtoSign", typeof(bool));
                    dtResult.Columns.Add("ExtensionFile", typeof(string));

                    foreach (DataRow dr in dtFile.Rows)
                    {
                        DataRow drResult = dtResult.NewRow();
                        drResult["OKtoSign"] = true;
                        drResult["FilePath"] = dr["FilePath"];
                        drResult["ExtensionFile"] = Path.GetExtension(dr["FilePath"].ToString());
                        drResult["FileData"] = dr["FileData"];
                        dtResult.Rows.Add(drResult);
                    }
                    goto ReturnTrue;
                }

            ReturnFalse:
                {
                    dtResult = null;
                    return false;
                }

            ReturnTrue:
                return true;
                #endregion
            }
            catch (Exception ex)
            {
                strErr = "Quá trình ký thất bại!\n" + ex.Message;
                dtResult = null;
                return false;
            }
        }

        [WebMethod(Description = "Xác thực các văn bản ký đã có FileID trong hệ thống và cập nhật cơ sở dữ liệu")]
        [PrincipalPermission(SecurityAction.Demand, Authenticated = true)]
        public bool SaveFile(DataTable dtInFo, string SignMessage, ref DataTable dtResult, ref string strError)
        {
            ES_Encrypt enc = new ES_Encrypt();
            string strDecryptMessage = enc.DecryptString(SignMessage);
            string[] arrayDecrypt = strDecryptMessage.Split('$');

            //Kiểm tra thông tin
            if (arrayDecrypt.Length != 4)
            {
                strError = "Thông tin ký không chính xác!";
                return false;
            }

            string key = arrayDecrypt[0];
            string userName = arrayDecrypt[1];
            string programName = arrayDecrypt[2];
            string typeSign = arrayDecrypt[3];

            //Results
            bool ret = false;
            //Trả về true nếu có ít nhất 01 file thành công
            strError = "Tổng số văn bản: xx. Ký thành công: xx.";
            //Tạo bảng Result
            dtResult = new DataTable("RESULT");
            dtResult.Columns.Add("FileID", typeof(int));
            dtResult.Columns.Add("FileName", typeof(string));
            dtResult.Columns.Add("SignResults", typeof(int));
            dtResult.Columns.Add("SignDetails", typeof(string));
            try
            {
                //CSDL
                DAL_SqlConnector.ConnectionString = GetDatabaseString();
                BUSQuanTri bus = new BUSQuanTri();
                int soDuocKy = 0;
                int soThanhCong = 0;

                DataTable dtFile = bus.CA_DataSign_FL_File_SelectByKey(key);
                if (dtInFo.Rows.Count != dtFile.Rows.Count)
                {
                    strError = "Thông tin xác thực để ký không chính xác!";
                    return ret;
                }

                #region Duyệt, check và cập nhật cơ sở dữ liệu từng file
                //Thông tin chứng thư. Chỉ cho phép 01 chứng thư ký
                X509Certificate2 certificate = null;
                Tuple<bool, CertificateStatus_CA, bool, CertificateStatus_TTD> cert_status = null;

                for (int i = 0; i < dtInFo.Rows.Count; i++)
                {
                    try
                    {
                        if (Convert.ToBoolean(dtInFo.Rows[i]["OKtoSign"]))
                        {
                            //Lấy thông tin
                            soDuocKy++;
                            int fileID = Convert.ToInt32(dtFile.Rows[i]["FileID"]);
                            byte[] fileHash = (byte[])dtFile.Rows[i]["FileHash"];
                            string filePath = dtFile.Rows[i]["FilePath"].ToString();

                            int signType = Convert.ToInt32(dtInFo.Rows[i]["QuyenUnit_Type"]);
                            int id_StatusLog = Convert.ToInt32(dtInFo.Rows[i]["ID_StatusLog"]);
                            byte[] fileData = (byte[])dtInFo.Rows[i]["FileData"];
                            FileInfoCA fi = new FileInfoCA();

                            //Gán giá trị cho bảng kết quả
                            DataRow drResult = dtResult.NewRow();
                            drResult["FileID"] = fileID;
                            drResult["FileName"] = Path.GetFileName(filePath);
                            dtResult.Rows.Add(drResult);

                            using (ESDigitalSignatureManager dsm = new ESDigitalSignatureManager(fileData, Path.GetExtension(filePath)))
                            {
                                #region Kiểm tra file
                                //Ninhtq: 21/06/2016 Sửa so sánh fileHash với cơ sở dữ liệu
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
                                        bus.FL_FileType_QuyenXacNhan_CheckByFileID_CertID(fileID, dsm.Signatures[a].Signer.SerialNumber);

                                        // Chỉ cần có file thành công thì trả về true
                                        ret = true;
                                        soThanhCong++;

                                        goto LuuDB;
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
                                //Nếu có chứng thư khác thì bắn lỗi -- kiểm tra tất cả các file có phải do 1 chứng thư số ký
                                if (!signature.Signer.SerialNumber.Equals(certificate.SerialNumber))
                                {
                                    //Lưu kết quả tất cả bản ghi
                                    foreach (DataRow dr in dtInFo.Rows)
                                        if (Convert.ToBoolean(dr["OKtoSign"]))
                                        {
                                            dtResult.Rows[i]["SignResults"] = (int)FileSignResults.FileSignFailed;
                                            dtResult.Rows[i]["SignDetails"] = "Chứng thư ký trong một phiên không khớp.";
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
                                    bus.FL_FileType_QuyenXacNhan_CheckByFileID_CertID(fileID, signature.Signer.SerialNumber);

                                    // Chỉ cần có file thành công thì trả về true
                                    ret = true;
                                    soThanhCong++;
                                }
                                else
                                {
                                    dtResult.Rows[i]["SignResults"] = (int)FileSignResults.SaveTimeOut;
                                    dtResult.Rows[i]["SignDetails"] = "Ký văn bản không thành công: Hết thời gian chờ ký!";
                                }
                                #endregion
                            }

                        LuuDB:
                            //Ninhtq: 28/06/2016 Cập nhật cơ sở dữ liệu riêng các hệ thống
                            DataTable dt = bus.CA_DataSignForDB_SelectByKeyObj(key, (int)TypeOutputToDB.Other,
                                fileID.ToString(), (int)TypeSignOverWeb.HaveFileID);
                            if (!UpadateDatabaseAfterSign(dt, fi, fileData, ref strError))
                            {
                                //Lưu bảng kết quả
                                dtResult.Rows[i]["SignResults"] = (int)FileSignResults.FileSignFailed;
                                dtResult.Rows[i]["SignDetails"] = "Cập nhật cơ sở dữ liệu thất bại. " + strError;

                                goto LuuStatus;
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
                }
                #endregion

                //Kết thúc
                strError = "Tổng số văn bản: " + dtInFo.Rows.Count.ToString() + ". Được phép ký: " + soDuocKy.ToString()
                    + ". Ký thành công: " + soThanhCong.ToString() + ".";
                return ret;
            }
            catch (Exception ex)
            {
                strError = "Quá trình ký thất bại! " + ex.Message;
                return false;
            }
        }

        [WebMethod(Description = "Xác thực các văn bản ký đã chưa có FileID trong hệ thống và cập nhật cơ sở dữ liệu")]
        [PrincipalPermission(SecurityAction.Demand, Authenticated = true)]
        public bool CreateAndSaveFileInDB(DataTable dtInFo, string SignMessage, ref DataTable dtResult, ref string strError)
        {
            ES_Encrypt enc = new ES_Encrypt();
            string strDecryptMessage = enc.DecryptString(SignMessage);
            string[] arrayDecrypt = strDecryptMessage.Split('$');

            //Kiểm tra thông tin
            if (arrayDecrypt.Length != 4)
            {
                strError = "Thông tin ký không chính xác!";
                return false;
            }

            string key = arrayDecrypt[0];
            string userName = arrayDecrypt[1];
            string programName = arrayDecrypt[2];
            string typeSign = arrayDecrypt[3];
            //Results
            bool ret = false;
            //Trả về true nếu có ít nhất 01 file thành công
            strError = "";
            //Tạo bảng Result
            dtResult = new DataTable("RESULT");
            dtResult.Columns.Add("FileID", typeof(int));
            dtResult.Columns.Add("FileName", typeof(string));
            dtResult.Columns.Add("SignResults", typeof(int));
            dtResult.Columns.Add("SignDetails", typeof(string));
            try
            {
                //CSDL
                DAL_SqlConnector.ConnectionString = GetDatabaseString();
                BUSQuanTri bus = new BUSQuanTri();
                int soDuocKy = 0;
                int soThanhCong = 0;

                DataTable dtFile = bus.CA_DataSign_SelectByKey(key);
                if (dtInFo.Rows.Count != dtFile.Rows.Count)
                {
                    strError = "Thông tin xác thực để ký không chính xác!";
                    return ret;
                }

                #region Duyệt, check và cập nhật cơ sở dữ liệu từng file
                X509Certificate2 certificate = null;
                Tuple<bool, CertificateStatus_CA, bool, CertificateStatus_TTD> cert_status = null;
                for (int i = 0; i < dtInFo.Rows.Count; i++)
                {
                    try
                    {
                        if (Convert.ToBoolean(dtInFo.Rows[i]["OKtoSign"]))
                        {
                            soDuocKy++;
                            byte[] fileData = (byte[])dtFile.Rows[i]["FileData"];
                            string fileExtension = Path.GetExtension(dtFile.Rows[i]["FilePath"].ToString());
                            byte[] fileDataSign = (byte[])dtInFo.Rows[i]["FileData"];
                            byte[] fileHash;
                            FileInfoCA fi = new FileInfoCA();

                            //Gán giá trị cho bảng kết quả
                            DataRow drResult = dtResult.NewRow();
                            drResult["FileName"] = dtFile.Rows[i]["FilePath"];
                            dtResult.Rows.Add(drResult);

                            using (ESDigitalSignatureManager dsm = new ESDigitalSignatureManager(fileData, fileExtension))
                            {
                                fileHash = dsm.GetHashValue();
                            }

                            //kiểm tra file
                            using (ESDigitalSignatureManager dsmFileSign = new ESDigitalSignatureManager(fileDataSign, fileExtension))
                            {
                                #region Kiểm tra file
                                //Ninhtq: 21/06/2016 Sửa so sánh fileHash với cơ sở dữ liệu
                                //So sánh hash
                                if (!dsmFileSign.GetHashValue().SequenceEqual(fileHash))
                                {
                                    //Lưu bảng kết quả
                                    dtResult.Rows[i]["SignResults"] = (int)FileSignResults.HashNotMatch;
                                    dtResult.Rows[i]["SignDetails"] = "Nội dung văn bản đã bị thay đổi.";
                                    goto NextTurn;
                                }

                                //Lấy chữ ký cuối cùng
                                if (dsmFileSign.Signatures.Count < 1)
                                {
                                    //Lưu bảng kết quả
                                    dtResult.Rows[i]["SignResults"] = (int)FileSignResults.NotSigned;
                                    dtResult.Rows[i]["SignDetails"] = "Không tìm thấy chữ ký.";
                                    goto NextTurn;
                                }
                                ESignature signature = dsmFileSign.Signatures[dsmFileSign.Signatures.Count - 1];

                                //Không kiểm tra có trùng chứng thư ko vì đầy là file được tạo mới hoặc do client upload
                                //Kiểm tra tính toàn vẹn chữ ký
                                if (signature.Verify != VerifyResult.Success)
                                {
                                    //Lưu bảng kết quả
                                    dtResult.Rows[i]["SignResults"] = (int)FileSignResults.InvalidSignature;
                                    dtResult.Rows[i]["SignDetails"] = "Chữ ký không hợp lệ: " + signature.Verify.ToString();
                                    goto NextTurn;
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

                                //Kiểm tra trạng thái
                                if (!cert_status.Item1)
                                {
                                    //Lưu bảng kết quả
                                    dtResult.Rows[i]["SignResults"] = (int)FileSignResults.InvalidCertificate_CA;
                                    dtResult.Rows[i]["SignDetails"] = "Xác thực chứng thư CA thất bại: " + cert_status.Item2.StatusInformation.ToString();
                                    goto NextTurn;
                                }
                                if (!cert_status.Item3)
                                {
                                    //Lưu bảng kết quả
                                    dtResult.Rows[i]["SignResults"] = (int)FileSignResults.InvalidCertificate_TTD;
                                    dtResult.Rows[i]["SignDetails"] = "Xác thực chứng thư TTĐ thất bại: " + cert_status.Item4.StatusInformation.ToString();
                                    goto NextTurn;
                                }
                                #endregion

                                //Báo thành công
                                dtResult.Rows[i]["SignResults"] = (int)FileSignResults.Success;
                                dtResult.Rows[i]["SignDetails"] = "Ký văn bản thành công.";
                                soThanhCong++;
                            }

                            //Ninhtq: 28/06/2016 Cập nhật cơ sở dữ liệu riêng các hệ thống
                            DataTable dt = bus.CA_DataSignForDB_SelectByKeyObj(key, (int)TypeOutputToDB.Other,
                                dtFile.Rows[i]["FilePath"].ToString(), (int)TypeSignOverWeb.SaveFileInDB);
                            //Lưu file vào cơ sở dư liệu
                            if (!UpadateDatabaseAfterSign(dt, fi, fileDataSign, ref strError))
                            {
                                //Lưu bảng kết quả
                                dtResult.Rows[i]["SignResults"] = (int)FileSignResults.FileSignFailed;
                                dtResult.Rows[i]["SignDetails"] = "Cập nhật cơ sở dữ liệu thất bại. " + strError;
                                goto NextTurn;
                            }
                            ret = true;
                        NextTurn:
                            continue;
                        }
                    }
                    catch (Exception ex)
                    {
                        dtResult.Rows[i]["SignResults"] = (int)FileSignResults.FileSignFailed;
                        dtResult.Rows[i]["SignDetails"] = "Ký văn bản không thành công: " + ex.Message;
                    }
                }
                #endregion

                //Kết thúc
                strError = "Tổng số văn bản: " + dtInFo.Rows.Count.ToString() + ". Được phép ký: " + soDuocKy.ToString()
                    + ". Ký thành công: " + soThanhCong.ToString() + ".";
                return ret;
            }
            catch (Exception ex)
            {
                strError = "Quá trình ký thất bại! " + ex.Message;
                return false;
            }
        }

        [WebMethod(Description = "Xác thực các văn bản ký được upload lên hệ thống và cập nhật cơ sở dữ liệu")]
        [PrincipalPermission(SecurityAction.Demand, Authenticated = true)]
        public bool CreateAndSaveFileInServer(DataTable dtInFo, string SignMessage, ref DataTable dtResult,
            ref string strError)
        {
            ES_Encrypt enc = new ES_Encrypt();
            string strDecryptMessage = enc.DecryptString(SignMessage);
            string[] arrayDecrypt = strDecryptMessage.Split('$');

            //Kiểm tra thông tin
            if (arrayDecrypt.Length != 4)
            {
                strError = "Thông tin ký không chính xác!";
                return false;
            }

            string key = arrayDecrypt[0];
            string userName = arrayDecrypt[1];
            string programName = arrayDecrypt[2];
            string typeSign = arrayDecrypt[3];
            //Results
            bool ret = false;
            //Trả về true nếu có ít nhất 01 file thành công
            strError = "";
            //Tạo bảng Result
            dtResult = new DataTable("RESULT");
            dtResult.Columns.Add("FileID", typeof(int));
            dtResult.Columns.Add("FileName", typeof(string));
            dtResult.Columns.Add("SignResults", typeof(int));
            dtResult.Columns.Add("SignDetails", typeof(string));
            try
            {
                //CSDL
                DAL_SqlConnector.ConnectionString = GetDatabaseString();
                BUSQuanTri bus = new BUSQuanTri();
                int soDuocKy = 0;
                int soThanhCong = 0;

                DataTable dtFile = bus.CA_DataSign_SelectByKey(key);
                if (dtInFo.Rows.Count != dtFile.Rows.Count)
                {
                    strError = "Thông tin xác thực để ký không chính xác!";
                    return ret;
                }

                #region Duyệt, check và cập nhật cơ sở dữ liệu từng file
                X509Certificate2 certificate = null;
                Tuple<bool, CertificateStatus_CA, bool, CertificateStatus_TTD> cert_status = null;
                for (int i = 0; i < dtInFo.Rows.Count; i++)
                {
                    try
                    {
                        if (Convert.ToBoolean(dtInFo.Rows[i]["OKtoSign"]))
                        {
                            #region Lấy thông tin
                            soDuocKy++;
                            byte[] fileData = (byte[])dtFile.Rows[i]["FileData"];
                            string fileExtension = Path.GetExtension(dtFile.Rows[i]["FilePath"].ToString());
                            byte[] fileDataSign = (byte[])dtInFo.Rows[i]["FileData"];
                            byte[] fileHash;
                            FileInfoCA fi = new FileInfoCA();

                            using (ESDigitalSignatureManager dsm = new ESDigitalSignatureManager(fileData, fileExtension))
                            {
                                fileHash = dsm.GetHashValue();
                            }

                            int fileTypeID = 0;
                            string fileMaDV = "";
                            string fileName = "";
                            string description = "";
                            DateTime fileDate = DateTime.Now;

                            //lấy chuỗi base64 của file ban đầu
                            DataTable dtTempSign = bus.CA_DataSignForDB_SelectByKeyObj(key, (int)TypeOutputToDB.CreateFile,
                                dtFile.Rows[i]["FilePath"].ToString(),(int)TypeSignOverWeb.CreateFileServer);

                            foreach (DataRow dr in dtTempSign.Rows)
                            {
                                if (dr[2].ToString() == "FileTypeID")
                                {
                                    if (!Int32.TryParse(dr[3].ToString(), out fileTypeID))
                                    {
                                        //Gán giá trị cho bảng kết quả
                                        DataRow drResult = dtResult.NewRow();
                                        drResult["FileName"] = dtFile.Rows[i]["FilePath"].ToString();
                                        drResult["SignResults"] = (int)FileSignResults.FileSignFailed;
                                        drResult["SignDetails"] = "Không tìm thấy thông tin cập nhật cơ sở dữ liệu";
                                        dtResult.Rows.Add(drResult);
                                        goto NextTurn;
                                    }
                                }
                                else if (dr[2].ToString() == "FileMaDV")
                                    fileMaDV = dr[3].ToString();
                                else if (dr[2].ToString() == "FileName")
                                    fileName = dr[3].ToString();
                                else if (dr[2].ToString() == "Description")
                                    description = dr[3].ToString();
                                else if (dr[2].ToString() == "FileDate")
                                {
                                    if (!DateTime.TryParse(dr[3].ToString(), out fileDate))
                                    {
                                        //Gán giá trị cho bảng kết quả
                                        DataRow drResult = dtResult.NewRow();
                                        drResult["FileName"] = dtInFo.Rows[i]["FilePathClient"].ToString();
                                        drResult["SignResults"] = (int)FileSignResults.FileSignFailed;
                                        drResult["SignDetails"] = "Không tìm thấy thông tin cập nhật cơ sở dữ liệu";
                                        dtResult.Rows.Add(drResult);
                                        goto NextTurn;
                                    }
                                }
                            }
                            //Gán giá trị cho bảng kết quả
                            DataRow drNew = dtResult.NewRow();
                            drNew["FileName"] = fileName;
                            dtResult.Rows.Add(drNew);
                            #endregion

                            //Kiểm tra file
                            //Lưu ý fileName có đuôi giống đuôi file đơn vị gửi
                            using (ESDigitalSignatureManager dsmFileSign = new ESDigitalSignatureManager(fileDataSign, Path.GetExtension(fileName)))
                            {
                                #region Kiểm tra file
                                //Ninhtq: 21/06/2016 Sửa so sánh fileHash với cơ sở dữ liệu
                                //So sánh hash
                                if (!dsmFileSign.GetHashValue().SequenceEqual(fileHash))
                                {
                                    //Lưu bảng kết quả
                                    dtResult.Rows[i]["SignResults"] = (int)FileSignResults.HashNotMatch;
                                    dtResult.Rows[i]["SignDetails"] = "Nội dung văn bản đã bị thay đổi.";
                                    goto NextTurn;
                                }
                                //Lấy chữ ký cuối cùng
                                if (dsmFileSign.Signatures.Count < 1)
                                {
                                    //Lưu bảng kết quả
                                    dtResult.Rows[i]["SignResults"] = (int)FileSignResults.NotSigned;
                                    dtResult.Rows[i]["SignDetails"] = "Không tìm thấy chữ ký.";
                                    goto NextTurn;
                                }
                                ESignature signature = dsmFileSign.Signatures[dsmFileSign.Signatures.Count - 1];

                                //Không kiểm tra có trùng chứng thư ko vì đầy là file được tạo mới hoặc do client upload
                                //Kiểm tra tính toàn vẹn chữ ký
                                if (signature.Verify != VerifyResult.Success)
                                {
                                    //Lưu bảng kết quả
                                    dtResult.Rows[i]["SignResults"] = (int)FileSignResults.InvalidSignature;
                                    dtResult.Rows[i]["SignDetails"] = "Chữ ký không hợp lệ: " + signature.Verify.ToString();
                                    goto NextTurn;
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

                                //Kiểm tra trạng thái
                                if (!cert_status.Item1)
                                {
                                    //Lưu bảng kết quả
                                    dtResult.Rows[i]["SignResults"] = (int)FileSignResults.InvalidCertificate_CA;
                                    dtResult.Rows[i]["SignDetails"] = "Xác thực chứng thư CA thất bại: " + cert_status.Item2.StatusInformation.ToString();
                                    goto NextTurn;
                                }
                                if (!cert_status.Item3)
                                {
                                    //Lưu bảng kết quả
                                    dtResult.Rows[i]["SignResults"] = (int)FileSignResults.InvalidCertificate_TTD;
                                    dtResult.Rows[i]["SignDetails"] = "Xác thực chứng thư TTĐ thất bại: " + cert_status.Item4.StatusInformation.ToString();
                                    goto NextTurn;
                                }
                                #endregion

                                //Không kiểm tra thời gian ký do file được tạo mới trong hệ thống
                                #region Khởi tạo văn bản, lưu file và chữ ký
                                DataTable dtFileNew = bus.FL_File_InsertNewFile(programName, userName, fileTypeID, fileMaDV, fileDate, fileName, description);
                                if (dtFileNew.Rows.Count < 1)
                                {
                                    dtResult.Rows[i]["SignResults"] = (int)FileSignResults.FileSignFailed;
                                    dtResult.Rows[i]["SignDetails"] = "Tạo mới văn bản thất bại!";
                                }
                                else
                                {
                                    fi = new FileInfoCA();
                                    fi.FileID = Convert.ToInt32(dtFileNew.Rows[0]["FileID"]);
                                    fi.FileNumber = dtFileNew.Rows[0]["FileNumber"].ToString();
                                    fi.FilePath = dtFileNew.Rows[0]["FilePath"].ToString();
                                    fi.FileTypeID = Convert.ToInt32(dtFileNew.Rows[0]["FileTypeID"]);
                                    fi.FileDate = Convert.ToDateTime(dtFileNew.Rows[0]["FileDate"]);
                                    fi.MaDV = dtFileNew.Rows[0]["MaDV"].ToString();
                                    fi.Status = Convert.ToInt32(dtFileNew.Rows[0]["Status"]);
                                    fi.Description = dtFileNew.Rows[0]["Description"].ToString();
                                    
                                    int signType = Convert.ToInt32(dtFileNew.Rows[0]["QuyenTaoFile"]);
                                    //Tạo folder nếu chưa có
                                    string dir = Path.GetDirectoryName(fi.FilePath);
                                    if (!Directory.Exists(dir))
                                        Directory.CreateDirectory(dir);
                                    //Lưu file
                                    if (!this.SaveFile_WithHash(fi.FileID, fileDataSign, programName, userName, ref strError))
                                    {
                                        dtResult.Rows[i]["SignResults"] = (int)FileSignResults.FileSignFailed;
                                        dtResult.Rows[i]["SignDetails"] = "Lưu văn bản thất bại! " + strError;
                                        goto NextTurn;
                                    }
                                    //Ghi log ký
                                    bus.FL_File_UpdateForLogSign(fi.FileID, signature.Signer.SerialNumber, signature.SigningTime, (int)signature.Verify,
                                        signType, (int)FileSignActions.AddSignature, fi.FilePath, "Hoàn thành lưu file có chữ ký", programName, userName);
                                    //Báo thành công
                                    dtResult.Rows[i]["SignResults"] = (int)FileSignResults.Success;
                                    dtResult.Rows[i]["SignDetails"] = "Ký văn bản thành công.";
                                    soThanhCong++;
                                }
                                #endregion
                            }

                            //Ninhtq: 28/06/2016 Cập nhật cơ sở dữ liệu riêng các hệ thống
                            DataTable dt = bus.CA_DataSignForDB_SelectByKeyObj(key, (int)TypeOutputToDB.Other,
                                dtFile.Rows[i]["FilePath"].ToString(), (int)TypeSignOverWeb.CreateFileServer);
                            if (!UpadateDatabaseAfterSign(dt, fi, fileDataSign, ref strError))
                            {
                                //Lưu bảng kết quả
                                dtResult.Rows[i]["SignResults"] = (int)FileSignResults.FileSignFailed;
                                dtResult.Rows[i]["SignDetails"] = "Cập nhật cơ sở dữ liệu thất bại. " + strError;
                                goto NextTurn;
                            }
                            ret = true;
                        NextTurn:
                            continue;
                        }
                    }
                    catch (Exception ex)
                    {
                        dtResult.Rows[i]["SignResults"] = (int)FileSignResults.FileSignFailed;
                        dtResult.Rows[i]["SignDetails"] = "Ký văn bản không thành công: " + ex.Message;
                    }
                }
                #endregion

                return ret;
            }
            catch (Exception ex)
            {
                strError = "Quá trình ký thất bại! " + ex.Message;
                return false;
            }
        }

        //Hàm hỗ trợ
        private string GetDatabaseString()
        {
            return ConfigurationManager.ConnectionStrings["CA"].ConnectionString;
            //if (idb == 1)
            //    return ConfigurationManager.ConnectionStrings["BCSX_Internet"].ConnectionString;
            //else if (idb == 2)
            //    return ConfigurationManager.ConnectionStrings["WebTTD"].ConnectionString;
            //else if (idb == 3)
            //    return ConfigurationManager.ConnectionStrings["TTTT"].ConnectionString;
            //else if (idb == 4)
            //    return ConfigurationManager.ConnectionStrings["SMS"].ConnectionString;
            //else
            //    return ConfigurationManager.ConnectionStrings["CA"].ConnectionString;
        }

        private bool ValidateCertificateInTTD_Now(string programName, string userName, byte[] certRawData, out CertificateStatus_TTD certificateStatus)
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

        private bool SaveFile_WithHash(int fileID, byte[] fileData, string programName, string userName, ref string strError)
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

        private bool UpadateDatabaseAfterSign(DataTable dtDataSignForDB, FileInfoCA fi, byte[] FileDataSign, ref string strErr)
        {
            try
            {
                if(dtDataSignForDB.Rows.Count == 0)
                {
                    strErr = "Không tìm thấy thông tin cập nhật dữ liệu!";
                    return false;
                }
                dtDataSignForDB.Columns.Add("GroupID");
                int igroup = 0;
                for (int i = 0, j = 0; i < dtDataSignForDB.Rows.Count; i++)
                {
                    if (dtDataSignForDB.Rows[i][2].ToString() == "ConnectString")
                        igroup++;
                    dtDataSignForDB.Rows[i]["GroupID"] = igroup;
                }

                try
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        for (int i = 1; i <= igroup; i++)
                        {
                            DataRow[] arrdr = dtDataSignForDB.Select("GroupID = " + i);
                            string stringConnection = arrdr[0][3].ToString();
                            string StoreName = arrdr[1][3].ToString();

                            using (SqlConnection connection = new SqlConnection(stringConnection))
                            {
                                connection.Open();
                                SqlCommand cmd = new SqlCommand();
                                cmd.CommandText = StoreName;
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Connection = connection;
                                SqlCommandBuilder.DeriveParameters(cmd);

                                for (int k = 2; k < arrdr.Length; k++)
                                {
                                    if (Convert.ToInt32(arrdr[k][4]) == (int)TypeOutputToDB.Base64)
                                        cmd.Parameters[k - 1].Value = Convert.FromBase64String(arrdr[k][3].ToString());
                                    else if (Convert.ToInt32(arrdr[k][4]) == (int)TypeOutputToDB.FileID)
                                        cmd.Parameters[k - 1].Value = fi.FileID;
                                    else if (Convert.ToInt32(arrdr[k][4]) == (int)TypeOutputToDB.FilePath)
                                        cmd.Parameters[k - 1].Value = fi.FilePath;
                                    else if (Convert.ToInt32(arrdr[k][4]) == (int)TypeOutputToDB.FileName)
                                        cmd.Parameters[k - 1].Value = fi.FilePath;
                                    else if (Convert.ToInt32(arrdr[k][4]) == (int)TypeOutputToDB.FileDataSign)
                                        cmd.Parameters[k - 1].Value = FileDataSign;
                                    else
                                        cmd.Parameters[k - 1].Value = arrdr[k][3] ?? (object)DBNull.Value;
                                }

                                cmd.ExecuteNonQuery();
                                connection.Close();
                            }
                        }
                        scope.Complete();
                    }
                    return true;
                }
                catch(TransactionAbortedException ex)
                {
                    strErr = ex.Message;
                    return false;
                }
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return false;
            }
        }

        private bool UpadateDatabaseAfterSign(DataTable dtDataSignForDB, ref string strErr)
        {
            try
            {
                if (dtDataSignForDB.Rows.Count == 0)
                {
                    strErr = "Không tìm thấy thông tin cập nhật dữ liệu!";
                    return false;
                }
                dtDataSignForDB.Columns.Add("GroupID");
                int igroup = 0;
                for (int i = 0, j = 0; i < dtDataSignForDB.Rows.Count; i++)
                {
                    if (dtDataSignForDB.Rows[i][2].ToString() == "ConnectString")
                        igroup++;
                    dtDataSignForDB.Rows[i]["GroupID"] = igroup;
                }

                try
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        for (int i = 1; i <= igroup; i++)
                        {
                            DataRow[] arrdr = dtDataSignForDB.Select("GroupID = " + i);
                            string stringConnection = arrdr[0][3].ToString();
                            string StoreName = arrdr[1][3].ToString();

                            using (SqlConnection connection = new SqlConnection(stringConnection))
                            {
                                connection.Open();
                                SqlCommand cmd = new SqlCommand();
                                cmd.CommandText = StoreName;
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Connection = connection;
                                SqlCommandBuilder.DeriveParameters(cmd);

                                for (int k = 2; k < arrdr.Length; k++)
                                {
                                    if (Convert.ToInt32(arrdr[k][4]) == (int)TypeOutputToDB.Base64)
                                        Convert.FromBase64String(arrdr[k][3].ToString());
                                    else
                                        cmd.Parameters[k - 1].Value = arrdr[k][3] ?? (object)DBNull.Value;
                                }

                                cmd.ExecuteNonQuery();
                                connection.Close();
                            }
                        }
                        scope.Complete();
                    }
                    return true;
                }
                catch (TransactionAbortedException ex)
                {
                    strErr = ex.Message;
                    return false;
                }
            }
            catch (Exception ex)
            {
                strErr = ex.Message;
                return false;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using esDigitalSignature;
using esDigitalSignature.Library;
using WebService = WebTestSign.A0SignatureService;
using System.Security.Cryptography.X509Certificates;

namespace WebTestSign
{
    public partial class _Default : Page
    {
        int AppletInitTimeout = 10;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        //Button gắn sự kiện này phải có thuộc tính: CssClass="essign_btnReady"
        protected void btnReady_Click(object sender, EventArgs e)
        {
            //Thông tin chung
            string programName = "WebTTD";
            string userName = "eptc_xacnhan";

            //Thông tin file ký
            string arrFileID = txtPath.Text;
            WebService.CAService sc = new WebService.CAService();
            DataTable dtResults = new DataTable("RESULTS");

            //Gửi lệnh ký về client
            string strError = "";
            bool ret = DigSig_SendToClient(sc, arrFileID, programName, userName, ref dtResults, ref strError);
            //lblOutput.Text = strError;

            /* Hiển thị chi tiết lỗi từng file ở đây */
            Session["dtFileInfo"] = dtResults;
            grvListFile.DataSource = dtResults;
            grvListFile.DataBind();
        }

        //Button gắn sự kiện này phải có thuộc tính: CssClass="essign_btnUpload"
        //Textbox gắn sự kiện này phải có thuộc tính: CssClass="essign_txtBase64"
        //Add-on gọi click btnUpload khi countdown = 0 hoặc đọc được file upload.ess
        protected void btnUpload_Click(object sender, EventArgs e)
        {
            //Thông tin chung
            string programName = "WEBTTD";
            string userName = "phalai";

            //Thông tin file ký
            DataTable dtFileInfo = (DataTable)Session["dtFileInfo"];
            WebService.CAService sc = new WebService.CAService();

            //Nhận file đã ký từ client
            //lblOutput.Text = DigSig_ReceiveFromClient(sc, programName, userName, ref dtFileInfo);

            /* Hiển thị chi tiết lỗi từng file ở đây */
            Session["dtFileInfo"] = dtFileInfo;
            grvListFile.DataSource = dtFileInfo;
            grvListFile.DataBind();
        }

        //private string DigSig_SendToClient(WebService.CAService ws, string arrFileID, string programName, string userName, ref DataTable dtFileInfo)
        //{
        //    #region Gửi lệnh ký qua client
        //    //Kiểm tra có add-ons hay chưa
        //    if (txtHasAddons.Text == "Addons_SignWeb_NLDC")
        //    {
        //        try
        //        {
        //            //Kiểm tra trạng thái file và thiết lập trạng thái chờ
        //            bool okToSign = ws.FL_File_SelectForAllowSign_Array(arrFileID, programName, userName, out dtFileInfo);

        //            if (okToSign)
        //            {
        //                //Khóa nút ký
        //                btnReady.Enabled = false;

        //                //lấy base64
        //                string fileExt = "";
        //                string fileBase64 = "";
        //                foreach (DataRow dr in dtFileInfo.Rows)
        //                {
        //                    if (Convert.ToBoolean(dr["OKtoSign"]))
        //                    {
        //                        fileExt += System.IO.Path.GetExtension(dr["FilePath"].ToString()) + ";";
        //                        fileBase64 += Common.ConvertFileToBase64(dr["FilePath"].ToString()) + ";";
        //                    }
        //                }

        //                //Gán lệnh gọi Sign app cho HtmlAnchor
        //                aSign.HRef = "ESSignProtocol:" + fileExt;
        //                txtBase64.Text = fileBase64 + ";";

        //                return "Hệ thống đang xử lý ......";
        //            }
        //            else
        //            {
        //                txtBase64.Text = "HUY";
        //                return "Các văn bản không được phép ký.";
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            txtBase64.Text = "HUY";
        //            return "Lỗi trong quá trình lấy thông tin văn bản: " + ex.Message;
        //        }
        //    }
        //    else
        //    {
        //        return "Trình duyệt chưa hỗ trợ ký online. Hãy download công cụ hỗ trợ TẠI ĐÂY và cài đặt theo hướng dẫn (chỉ hỗ trợ Firefox).";
        //    }
        //    #endregion
        //}

        //private string DigSig_ReceiveFromClient(WebService.CAService ws, string programName, string userName, ref DataTable dtFileInfo)
        //{
        //    #region Nhận kết quả từ client
        //    try
        //    {
        //        //Lấy base64 từ textbox ẩn
        //        string fileBase64 = txtBase64.Text;

        //        if (fileBase64 == "HUY")
        //        {
        //            #region Client hủy ký
        //            //Trả lại trạng thái cho file
        //            for (int i = 0; i < dtFileInfo.Rows.Count; i++)
        //            {
        //                if (Convert.ToBoolean(dtFileInfo.Rows[i]["OKtoSign"]))
        //                {
        //                    ws.SetFileStatus(Convert.ToInt32(dtFileInfo.Rows[i]["FileID"]), (int)FileStatus.LuuFile, "Hủy ký do client.", programName, userName);
        //                    dtFileInfo.Rows[i]["SignResults"] = (int)FileSignResults.FileSignFailed;
        //                    dtFileInfo.Rows[i]["SignDetails"] = "Ký văn bản thất bại hoặc bị hủy bởi người dùng.";
        //                }
        //            }
        //            #endregion

        //            return "Ký văn bản thất bại hoặc bị hủy bởi người dùng!";
        //        }
        //        else
        //        {
        //            //Lưu file vào hệ thống và trả trạng thái ký cho file
        //            string[] arrBase64 = fileBase64.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
        //            string[] arrExt = aSign.HRef.Substring(15).Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

        //            //Khớp số lượng file gửi về server
        //            if (arrBase64.Length != arrExt.Length)
        //            {
        //                #region Chuỗi base64 trả về không khớp
        //                //Trả lại trạng thái cho file
        //                for (int i = 0; i < dtFileInfo.Rows.Count; i++)
        //                {
        //                    if (Convert.ToBoolean(dtFileInfo.Rows[i]["OKtoSign"]))
        //                    {
        //                        ws.SetFileStatus(Convert.ToInt32(dtFileInfo.Rows[i]["FileID"]), (int)FileStatus.LuuFile,
        //                            "Hủy ký do convert nội dung file.", programName, userName);
        //                        dtFileInfo.Rows[i]["SignResults"] = (int)FileSignResults.FileSignFailed;
        //                        dtFileInfo.Rows[i]["SignDetails"] = "Ký văn bản thất bại: Nội dung trả về không đúng.";
        //                    }
        //                }
        //                #endregion

        //                return "Ký văn bản thất bại: Nội dung trả về không đúng.";
        //            }
        //            else
        //            {
        //                #region Duyệt và check từng file
        //                //Biến chạy cho mảng arrBase64
        //                int i64 = 0;
        //                //Danh sách serial của các chứng thư đã được kiểm tra
        //                Dictionary<string, Tuple<bool, WebService.CertificateStatus_CA, bool, WebService.CertificateStatus_TTD>> lstCertStatus
        //                    = new Dictionary<string, Tuple<bool, WebService.CertificateStatus_CA, bool, WebService.CertificateStatus_TTD>>();

        //                for (int i = 0; i < dtFileInfo.Rows.Count; i++)
        //                {
        //                    if (Convert.ToBoolean(dtFileInfo.Rows[i]["OKtoSign"]))
        //                    {
        //                        int fileID = Convert.ToInt32(dtFileInfo.Rows[i]["FileID"]);
        //                        string tempFile = Path.GetTempFileName();
        //                        string tempPath = tempFile;
        //                        ESignature signature;

        //                        try
        //                        {
        //                            //Lấy thông tin
        //                            int id_StatusLog = Convert.ToInt32(dtFileInfo.Rows[i]["ID_StatusLog"]);
        //                            string filePath = dtFileInfo.Rows[i]["FilePath"].ToString();
        //                            byte[] fileHash = (byte[])dtFileInfo.Rows[i]["FileHash"];
        //                            tempPath = tempFile + Path.GetExtension(filePath);

        //                            //Lưu file tạm
        //                            Common.ConvertBase64ToFile(arrBase64[i64], tempPath);

        //                            #region Kiểm tra file
        //                            using (ESDigitalSignatureManager dsm = new ESDigitalSignatureManager(tempPath))
        //                            {
        //                                //So sánh hash
        //                                if (!dsm.GetHashValue().SequenceEqual(fileHash))
        //                                {
        //                                    ws.SetFileStatus(fileID, (int)FileStatus.LuuFile, "Hủy ký do kiểm tra hash thất bại.", programName, userName);
        //                                    dtFileInfo.Rows[i]["SignResults"] = (int)FileSignResults.HashNotMatch;
        //                                    dtFileInfo.Rows[i]["SignDetails"] = "So sánh chuỗi hash thất bại.";
        //                                    continue;
        //                                }

        //                                //Lấy chữ ký cuối cùng
        //                                if (dsm.Signatures.Count < 1)
        //                                {
        //                                    ws.SetFileStatus(fileID, (int)FileStatus.LuuFile, "Hủy ký do không tìm thấy chữ ký.", programName, userName);
        //                                    dtFileInfo.Rows[i]["SignResults"] = (int)FileSignResults.NotSigned;
        //                                    dtFileInfo.Rows[i]["SignDetails"] = "Không tìm thấy chữ ký.";
        //                                    continue;
        //                                }
        //                                else
        //                                    signature = dsm.Signatures[dsm.Signatures.Count - 1];
                                        
        //                                //Kiểm tra tính toàn vẹn chữ ký
        //                                if (signature.Verify != VerifyResult.Success)
        //                                {
        //                                    ws.SetFileStatus(fileID, (int)FileStatus.LuuFile, "Hủy ký do kiểm tra chữ ký thất bại.", programName, userName);
        //                                    dtFileInfo.Rows[i]["SignResults"] = (int)FileSignResults.InvalidSignature;
        //                                    dtFileInfo.Rows[i]["SignDetails"] = "Kiểm tra chữ ký thất bại: " + signature.Verify.ToString();
        //                                    continue;
        //                                }

        //                                //Check chứng thư nếu chưa lưu vào Dictionary
        //                                X509Certificate2 cert = signature.Signer;
        //                                if (!lstCertStatus.ContainsKey(cert.SerialNumber))
        //                                {
        //                                    //Kiểm tra hiệu lực chứng thư với CA
        //                                    WebService.CertificateStatus_CA statusCA;
        //                                    bool bCA = ws.ValidateCertificateInCA_Now(cert.RawData, out statusCA);
        //                                    //Kiểm tra hiệu lực chứng thư trong TTĐ
        //                                    WebService.CertificateStatus_TTD statusTTD;
        //                                    bool bTTD = ws.ValidateCertificateInTTD_Now(programName, userName, cert.RawData, out statusTTD);

        //                                    lstCertStatus.Add(cert.SerialNumber, Tuple.Create(bCA, statusCA, bTTD, statusTTD));
        //                                }
        //                                //Kiểm tra trạng thái
        //                                Tuple<bool, WebService.CertificateStatus_CA, bool, WebService.CertificateStatus_TTD> certStatus
        //                                    = lstCertStatus[cert.SerialNumber];
        //                                if (!certStatus.Item1)
        //                                {
        //                                    ws.SetFileStatus(fileID, (int)FileStatus.LuuFile, "Hủy ký do xác thực chứng thư với CA thất bại.", programName, userName);
        //                                    dtFileInfo.Rows[i]["SignResults"] = (int)FileSignResults.InvalidCertificate_CA;
        //                                    dtFileInfo.Rows[i]["SignDetails"] = "Xác thực chứng thư CA thất bại: " + certStatus.Item2.StatusInformation.ToString();
        //                                    continue;
        //                                }
        //                                if (!certStatus.Item3)
        //                                {
        //                                    ws.SetFileStatus(fileID, (int)FileStatus.LuuFile, "Hủy ký do xác thực chứng thư với TTĐ thất bại.", programName, userName);
        //                                    dtFileInfo.Rows[i]["SignResults"] = (int)FileSignResults.InvalidCertificate_TTD;
        //                                    dtFileInfo.Rows[i]["SignDetails"] = "Xác thực chứng thư TTĐ thất bại: " + certStatus.Item4.StatusInformation.ToString();
        //                                    continue;
        //                                }
        //                            }
        //                            #endregion

        //                            #region Kiểm tra thời gian chờ và lưu
        //                            if (ws.FL_File_SelectForSaveSign(id_StatusLog))
        //                            {
        //                                //Copy vào file đích
        //                                File.Copy(tempPath, filePath, true);
        //                                //Ghi log
        //                                ws.FL_File_UpdateForLogSign(fileID, signature.Signer.SerialNumber, signature.SigningTime, (int)signature.Verify,
        //                                    signature.SignatureCreator, (int)FileSignActions.AddSignature, "Hoàn thành ký file qua HSM", programName, userName);
        //                                //Báo thành công
        //                                dtFileInfo.Rows[i]["SignResults"] = (int)FileSignResults.Success;
        //                                dtFileInfo.Rows[i]["SignDetails"] = "Ký văn bản thành công.";

        //                                /*Cập nhật vào db của hệ thống riêng ở đây*/
        //                            }
        //                            else
        //                            {
        //                                //Toantk 24/6/2015: Check trong hàm SetFileStatus nếu đang trong phiên ký thì ko được cập nhật
        //                                //vì có thể chèn vào phiên của người khác
        //                                //ws.SetFileStatus(fileID, (int)FileStatus.LuuFile, "Hủy ký", programName, userName);
        //                                dtFileInfo.Rows[i]["SignResults"] = (int)FileSignResults.SaveTimeOut;
        //                                dtFileInfo.Rows[i]["SignDetails"] = "Ký văn bản thất bại: Hết thời gian chờ lưu file.";
        //                            }
        //                            #endregion
        //                        }
        //                        catch (Exception ex)
        //                        {
        //                            //Toantk 24/6/2015: Check trong hàm SetFileStatus nếu đang trong phiên ký thì ko được cập nhật
        //                            //vì có thể chèn vào phiên của người khác
        //                            //ws.SetFileStatus(fileID, (int)FileStatus.LuuFile, "Hủy ký do Exception.", programName, userName);
        //                            dtFileInfo.Rows[i]["SignResults"] = (int)FileSignResults.FileSignFailed;
        //                            dtFileInfo.Rows[i]["SignDetails"] = "Ký văn bản thất bại: " + ex.Message;
        //                        }
        //                        finally
        //                        {
        //                            //Xóa file tạm
        //                            File.Delete(tempFile);
        //                            File.Delete(tempPath);
        //                            i64++;
        //                        }
        //                    }
        //                }
        //                #endregion

        //                return "Quá trình ký văn bản kết thúc.";
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return "Ký văn bản thất bại: " + ex.Message;
        //    }
        //    finally
        //    {
        //        //Enable button Ready
        //        txtBase64.Text = "";
        //        btnReady.Text = "Ký văn bản";
        //        btnReady.Enabled = true;
        //    }
        //    #endregion
        //}

        /* === Thêm đoạn code này vào để ký qua windows app ===*/
        /// <summary>
        /// Gửi dữ liệu file về client và ký qua app
        /// </summary>
        private bool DigSig_SendToClient(WebService.CAService ws, string arrFileID, string programName, string userName,
            ref DataTable dtResults, ref string strError)
        {
            try
            {
                //Mã để app kiểm tra lệnh được gọi từ web

                //Kiểm tra đồng bộ thời gian do thời gian ký trên file lấy từ client
                DateTime dtmClient = Convert.ToDateTime(txtClientTime.Text);
                TimeSpan diff = DateTime.Now - dtmClient;
                if (Math.Abs(diff.TotalSeconds) > 300)
                {
                    strError = "Thời gian client không đồng bộ.";
                    return false;
                }

                //Trả về client
                string fileBase64 = "123456";

                //Gán lệnh gọi Sign app cho HtmlAnchor
                txtBase64.Text = fileBase64.Remove(fileBase64.Length - 1);
                aSign.HRef = "NLDCSignProtocol:" + arrFileID + "|~" + programName + "|~" + userName;

                //Gọi hàm ký javascript
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "none", "window.onload = function () { performWinAppSign(" + AppletInitTimeout.ToString() + "); };", true);

                strError = "Hệ thống đang xử lý ......";
                return true;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return false;
            }
        }

        /// <summary>
        /// Nhận kết quả từ client
        /// </summary>
        private bool DigSig_ReceiveFromClient(WebService.CAService ws, string programName, string userName,
            ref DataTable dtResults, ref string strError)
        {
            try
            {
                //Lấy base64 từ textbox ẩn
                string fileBase64 = txtBase64.Text;

                if (fileBase64 == "")
                {
                    //Trả lại trạng thái cho file
                    foreach (DataRow dr in dtResults.Rows)
                        if (Convert.ToBoolean(dr["OKtoSign"]))
                        {
                            dr["SignResults"] = (int)FileSignResults.FileSignFailed;
                            dr["SignDetails"] = "Ký văn bản lỗi client hoặc bị hủy bởi người dùng.";
                        }
                }
                else
                {
                    //Lưu dữ liệu file mới và chuyển web service xử lý
                    string[] arrBase64 = fileBase64.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    int i = 0;
                    //Trả lại trạng thái cho file
                    foreach (DataRow dr in dtResults.Rows)
                        if (Convert.ToBoolean(dr["OKtoSign"]) && i < arrBase64.Length)
                        {
                            dr["FileData"] = Convert.FromBase64String(arrBase64[i]);
                            i++;
                        }
                }

                return false;//ws.SignFilesByID_ReceiveFromClient(programName, userName, ref dtResults, ref strError);
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return false;
            }
            finally
            {
                txtBase64.Text = strError;//txtBase64.Text = "";
            }
        }
        /* === Thêm đoạn code này vào để ký qua windows app ===*/
    }
}
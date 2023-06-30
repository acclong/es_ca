using esDigitalSignature;
using esDigitalSignature.Library;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using Telerik.Web.UI;
using WebService = WebTestApplet.A0SignatureService;

namespace WebTestApplet
{
    public partial class _Default : Page
    {
        string AppletTokenDll = "vnptca_p11_v6.dll,viettel-ca_v4.dll";//"vnpt-ca_csp11.dll,VNPT-CA_v34.dll,vnptca_p11_v6.dll,vnpt-ca_cl_v1.dll,vnptcamobile.dll,gclib.dll,viettel-ca_v4.dll";
        int AppletInitTimeout = 10;

        protected void Page_Load(object sender, EventArgs e)
        {
            //string fileBase64 = Common.ConvertFileToBase64(@"C:\Users\TranKim\Desktop\CA\Test\File\Test_LargeFile_PDF_10MB.pdf");
            //txtBase64.Text = fileBase64;
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
            //bool ret = DigSig_SendToClient(sc, arrFileID, programName, userName, ref dtResults, ref strError);
            //lblOutput.Text = strError;

            lblOutput.Text = TestDownloadFile();
            //lblOutput.Text = TestLoadCert();
            //lblOutput.Text = UploadAndSign();

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
            string userName = "eptc_xacnhan";

            //Thông tin file ký
            DataTable dtFileInfo = (DataTable)Session["dtFileInfo"];
            WebService.CAService sc = new WebService.CAService();

            //Nhận file đã ký từ client
            string strError = "";
            //bool ret = DigSig_ReceiveFromClient(sc, programName, userName, ref dtFileInfo, ref strError);
            //lblOutput.Text = strError;
            
            lblOutput.Text = TestUploadFile();
            //lblOutput.Text = TestUploadCert();
            //lblOutput.Text = ReceiveFile();

            /* Hiển thị chi tiết lỗi từng file ở đây */
            Session["dtFileInfo"] = dtFileInfo;
            grvListFile.DataSource = dtFileInfo;
            grvListFile.DataBind();
        }

        /* === Thêm đoạn code này vào để lấy file server qua web service và ký applet ===*/
        /// <summary>
        /// Gửi dữ liệu file về client và ký qua applet
        /// </summary>
        private bool DigSig_SendToClient(WebService.CAService ws, string arrFileID, string programName, string userName,
            ref DataTable dtResults, ref string strError)
        {
            try
            {
                //Kiểm tra đồng bộ thời gian do thời gian ký trên file lấy từ client
                DateTime dtmClient = Convert.ToDateTime(txtClientTime.Text);
                TimeSpan diff = DateTime.Now - dtmClient;
                if (Math.Abs(diff.TotalSeconds) > 300)
                {
                    strError = "Thời gian client không đồng bộ.";
                    return false;
                }

                //Xin lệnh ký và lấy dữ liệu file. Nếu được ký thì strError trả về file extensions.
                if (!ws.SignFilesByID_SendToClient(arrFileID, programName, userName, ref dtResults, ref strError))
                    return false;

                //Trả về client
                string fileBase64 = "";
                foreach (DataRow dr in dtResults.Rows)
                    if (Convert.ToBoolean(dr["OKtoSign"]))
                    {
                        fileBase64 += Convert.ToBase64String((byte[])dr["FileData"]) + ";";
                        dr["FileData"] = DBNull.Value;
                    }

                //Gán lệnh gọi Sign app cho HtmlAnchor
                txtBase64.Text = fileBase64.Remove(fileBase64.Length - 1);
                //Sinh thẻ applet
                LiteralControl ctrlApplet = new LiteralControl();
                ctrlApplet.Text = @"
                    <applet archive='VnptTokenApplet.jar' 
				            name='VNPTCA Token Applet' id='vnptTokenApplet' 
				            code='com.vnpt.VnptTokenApplet.class' height='0' width='0'> 
			            <param name='separate_jvm' value='true' /> 
			            <param name='dll' value='" + AppletTokenDll + @"' />	
			            <a href='http://java.sun.com/webapps/getjava/BrowserRedirect?host=java.com' target='_blank'>JRE Download</a><br/> 
		            </applet>
                ";
                this.divDigSig.Controls.Add(ctrlApplet);
                //Gọi hàm ký javascript
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "none", "window.onload = function () { performAppletSign(" + AppletInitTimeout.ToString()
                    + ", '" + strError + "'); };", true);

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

                return ws.SignFilesByID_ReceiveFromClient(programName, userName, ref dtResults, ref strError);
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
        /* === Thêm đoạn code này vào để lấy file server qua web service và ký applet ===*/

        #region Lấy file server trực tiếp về client để ký và đẩy lại server
        private string TestDownloadFile()
        {
            string txt =
                "Cộng hòa Xã hội Chủ nghĩa Việt Nam" +
                "\nĐộc lập - Tự do - Hạnh phúc" +
                "\n---------------------------" +
                "\n\nPHIẾU ĐĂNG KÝ";
            byte[] data = Common.ConvertTextToPDF(txt);
            string fileBase64 = Convert.ToBase64String(data);

            //Ký office/pdf/xml
            //string fileBase64 = Common.ConvertFileToBase64(@"C:\Users\TranKim\Desktop\CA\Test\CG_G25600_2015_03_03#D.bid") + "";
            txtBase64.Text = fileBase64;
            //Sinh thẻ applet
            LiteralControl ctrlApplet = new LiteralControl();
            ctrlApplet.Text = @"
                    <applet archive='VnptTokenApplet.jar' 
				            name='VNPTCA Token Applet' id='vnptTokenApplet' 
				            code='com.vnpt.VnptTokenApplet.class' height='0' width='0'> 
			            <param name='separate_jvm' value='true' /> 
			            <param name='dll' value='vnpt-ca_csp11.dll,VNPT-CA_v34.dll,vnptca_p11_v6.dll,vnpt-ca_cl_v1.dll,vnptcamobile.dll,gclib.dll,viettel-ca_v4.dll' />	
			            <a href='http://java.sun.com/webapps/getjava/BrowserRedirect?host=java.com' target='_blank'>JRE Download</a><br/> 
		            </applet>
                ";
            this.divDigSig.Controls.Add(ctrlApplet);
            //Gọi hàm ký javascript
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "none", "window.onload = function () { performAppletSign(10, '.pdf'); };", true); //Sys.Application.add_load(signDocxBase64);

            return "Hệ thống đang xử lý ......";
        }

        private string TestUploadFile()
        {
            //Lấy base64 từ textbox ẩn
            string fileBase64 = txtBase64.Text.Remove(txtBase64.Text.Length - 1);
            //Lưu file tạm office/pdf/xml
            try
            {
                Common.ConvertBase64ToFile(fileBase64, @"C:\Users\TranKim\Desktop\CA_Test\TestSavePDF5.pdf");
            }
            catch { }

            return "Quá trình ký văn bản kết thúc.";
        }
        #endregion

        #region lấy cert trong token ở client và đẩy lên server
        private string TestLoadCert()
        {
            txtBase64.Text = "";
            //Sinh thẻ applet
            LiteralControl ctrlApplet = new LiteralControl();
            ctrlApplet.Text = @"
                    <applet archive='VnptTokenApplet.jar' 
				            name='VNPTCA Token Applet' id='vnptTokenApplet' 
				            code='com.vnpt.VnptTokenApplet.class' height='0' width='0'> 
			            <param name='separate_jvm' value='true' /> 
			            <param name='dll' value='vnpt-ca_csp11.dll,VNPT-CA_v34.dll,vnptca_p11_v6.dll,vnpt-ca_cl_v1.dll,vnptcamobile.dll,gclib.dll,viettel-ca_v4.dll' />	
			            <a href='http://java.sun.com/webapps/getjava/BrowserRedirect?host=java.com' target='_blank'>JRE Download</a><br/> 
		            </applet>
                ";
            this.divDigSig.Controls.Add(ctrlApplet);
            //Gọi hàm loadCert
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "none", "window.onload = function () { performAppletSign(10, '.cer'); };", true); //Sys.Application.add_load(signDocxBase64);

            return "Hệ thống đang xử lý ......";
        }

        private string TestUploadCert()
        {
            //Lấy base64 từ textbox ẩn
            string fileBase64 = txtBase64.Text.Remove(txtBase64.Text.Length - 1);
            //Lưu file tạm office/pdf/xml
            try
            {
                byte[] rawData = Convert.FromBase64String(fileBase64);
                X509Certificate2 cert = new X509Certificate2(rawData);
                txtPath.Text = cert.Subject;
            }
            catch { }

            return "Quá trình ký văn bản kết thúc.";
        }
        #endregion

        //Ký client qua popup
        protected void btnPopup_Click(object sender, EventArgs e)
        {
            RadWindow window1 = new RadWindow();
            string url = @"puThanhToan_XacNhanBangKeChenhLechSLChot.aspx";
            window1.NavigateUrl = ResolveUrl(url);
            window1.ID = "RadWindow1";
            window1.VisibleOnPageLoad = true; // Set this property to True for showing window from code   
            window1.Modal = true;
            window1.Behaviors = WindowBehaviors.Close;
            window1.DestroyOnClose = true;
            window1.Width = Unit.Pixel(620);
            window1.Height = Unit.Pixel(500);
            window1.OnClientClose = "clientClose";
            RadWindowManager1.Windows.Clear();
            RadWindowManager1.Windows.Add(window1);
        }

        #region ký file ở client và đẩy lên server
        protected string UploadAndSign()
        {
            //Sinh thẻ applet
            LiteralControl ctrlApplet = new LiteralControl();
            ctrlApplet.Text = @"
                    <applet archive='VnptTokenApplet.jar' 
				            name='VNPTCA Token Applet' id='vnptTokenApplet' 
				            code='com.vnpt.VnptTokenApplet.class' height='0' width='0'> 
			            <param name='separate_jvm' value='true' /> 
			            <param name='dll' value='vnpt-ca_csp11.dll,VNPT-CA_v34.dll,vnptca_p11_v6.dll,vnpt-ca_cl_v1.dll,vnptcamobile.dll,gclib.dll,viettel-ca_v4.dll' />	
			            <a href='http://java.sun.com/webapps/getjava/BrowserRedirect?host=java.com' target='_blank'>JRE Download</a><br/> 
		            </applet>
                ";
            this.divDigSig.Controls.Add(ctrlApplet);
            //Gọi hàm ký javascript
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "none", "window.onload = function () { performAppletSign(10, '.pdf'); };", true); //Sys.Application.add_load(signDocxBase64);

            return "Hệ thống đang xử lý ......";
        }

        private string ReceiveFile()
        {
           //Lưu file tạm office/pdf/xml
            try
            {
                //Lấy base64 từ textbox ẩn
                string fileBase64 = txtBase64.Text.Remove(txtBase64.Text.Length - 1);
            
                Common.ConvertBase64ToFile(fileBase64, @"C:\Users\TranKim\Desktop\CA\Test\TestUploadAndSign_1.pdf");
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

            return "Quá trình ký văn bản kết thúc.";
        }
        #endregion

        protected void Button2_Click(object sender, EventArgs e)
        {
            string url = @"\\10.8.48.4\CAThiTruongDien\ThanhToan\ChenhSanLuongThang\A_LUOI\2015\G25600_SLDD_1.2015_03022015101006338.pdf";
            string strUser = "ca_user";
                string strServer = "vietpool";
                string strPass = "CA@TTD$123";

                using (new ImpersonateUser(strUser, strServer, strPass))
                {
                    FileInfo fi = new FileInfo(url);

                    sendFileToClient(File.ReadAllBytes(url), fi.Name, fileType.XML);
                }
        }

        public enum fileType
        {
            Excel,
            XML,
            ZIP,
            Pdf,
            Text
        }

        void sendFileToClient(byte[] array, string fName, fileType fType)
        {

            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.BufferOutput = true;

            if (fType == fileType.XML)
                HttpContext.Current.Response.ContentType = "text/xml";
            else if (fType == fileType.Excel)
                HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            else if (fType == fileType.ZIP)
                HttpContext.Current.Response.ContentType = "application/zip";
            else if (fType == fileType.Pdf)
                HttpContext.Current.Response.ContentType = "application/pdf";
            else
                HttpContext.Current.Response.ContentType = "text/plain";

            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + fName);
            HttpContext.Current.Response.AddHeader("content-length", array.Length.ToString());
            HttpContext.Current.Response.CacheControl = "public";
            // writes buffer to OutputStream
            HttpContext.Current.Response.BinaryWrite(array);
            //HttpContext.Current.Response.OutputStream.Write(array, 0, array.Length);
            // HttpContext.Current.Response.End();

            HttpContext.Current.Response.Flush(); // Sends all currently buffered output to the client.
            HttpContext.Current.Response.SuppressContent = true;  // Gets or sets a value indicating whether to send HTTP content to the client.
            HttpContext.Current.ApplicationInstance.CompleteRequest(); // Causes ASP.NET to bypass all events and filtering in the HTTP pipeline chain of execution and directly execute the EndRequest event.
        }
    }
}
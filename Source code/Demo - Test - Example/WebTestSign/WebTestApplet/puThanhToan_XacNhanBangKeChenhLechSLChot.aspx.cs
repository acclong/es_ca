using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.IO;
using System.Collections;
using esDigitalSignature;
using esDigitalSignature.Library;
using System.Security.Cryptography.X509Certificates;

namespace WebTTD.UserControls.Popup
{
    public partial class puThanhToan_XacNhanBangKeChenhLechSLChot : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void btnOK_Click(object sender, EventArgs e)
        {
            lblErr.Text = TestLoadCert();
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            try
            {
                lblErr.Text = TestUploadCert();
                
            }
            catch (Exception ex)
            {
                lblErr.Text = "Xảy ra lỗi khi ký file sự kiện: " + ex.Message;
            }
        }

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
                btnOK.Text = cert.Subject;
            }
            catch { }

            return "Quá trình ký văn bản kết thúc.";
        }
    }
}
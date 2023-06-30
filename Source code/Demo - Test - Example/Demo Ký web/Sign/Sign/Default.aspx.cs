using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Net;
using System.Windows.Forms;
using System.Threading;
using System.Text;

namespace Sign
{
    public partial class _Default : System.Web.UI.Page
    {
        // tham số mặc định của trang
        string dataClipboard = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            txtPath.Text = "file\\Ninhtq_Lịch.xlsx";
        }

        protected void btnDownload_Click(object sender, EventArgs e)
        {
            DownloadFile(txtPath.Text, txtPath.Text.Split('\\').Last());
        }

        protected void btnSign_Click(object sender, EventArgs e)
        {
            try
            {
                // kiểm tra seerialNumber đã có hay chưa
                if (txtSerialNumber.Text == "")
                {                    
                    return;
                }
                // bắt đầu click nút ký lần 1: chuyển file thành base64 đẩy vào textbox
                if (btnSign.Text == "Sign")
                {
                    dataClipboard = "";
                    string pathFile = txtPath.Text;
                    string fileName = pathFile.Split('\\').Last();
                    string data = ExcelToBase64(pathFile);
                    string serial = txtSerialNumber.Text;
                    dataClipboard += "serial:" + serial;
                    dataClipboard += ";fileName:" + fileName;
                    dataClipboard += ";data:" + data;
                    txtDataSigned.Text = dataClipboard;
                    btnSign.Text = "Ready To Sign";
                }
                else if (btnSign.Text == "Ready To Sign")// bắt đầu click nút ký lần 2: gọi add ons
                {
                    txtDataSigned.Text = "";
                    btnSign.Text = "Signing.....";
                }
                else if (btnSign.Text == "Signing.....")// bắt đầu click nút ký lần 3: chuyển file lên server
                {
                    btnSign.Text = "Sign";
                    if (txtDataSigned.Text != "")
                    {
                        string sDataSigned = txtDataSigned.Text;
                        string[] aPartDataSigned = sDataSigned.Split(' ');
                        if (aPartDataSigned[0] == "ESVCGM")
                        {
                            string dataSigned = aPartDataSigned[2];
                            string fileNameSigned = aPartDataSigned[1];
                            string pathSigned = Server.MapPath("fileUpLoad\\");
                            Base64ToExcel(dataSigned, fileNameSigned, pathSigned);
                            txtDataSigned.Text = "";
                            MessageBox.Show("Ký file thành công!!");
                        }
                    }
                }
            }
            catch { }
        }

        // Download file về xem
        private void DownloadFile(string sFilePath, string sFileName)
        {
            try
            {
                HttpContext.Current.Response.ContentType = "APPLICATION/OCTET-STREAM";
                String Header = "Attachment; Filename=" + sFileName;
                HttpContext.Current.Response.AppendHeader("Content-Disposition", Header);
                System.IO.FileInfo Dfile = new System.IO.FileInfo(HttpContext.Current.Server.MapPath(sFilePath));
                HttpContext.Current.Response.WriteFile(Dfile.FullName);
                HttpContext.Current.Response.End();
            }
            catch (Exception ex) { }
        }

        // đẩy file excel thành chuỗi base 64
        private string ExcelToBase64(string path)
        {
            try
            {
                // convert to array byte
                byte[] fileContent = null;
                System.IO.FileStream fs = new System.IO.FileStream(Server.MapPath(path), System.IO.FileMode.Open, System.IO.FileAccess.Read);
                System.IO.BinaryReader binaryReader = new System.IO.BinaryReader(fs);
                long byteLength = new System.IO.FileInfo(Server.MapPath(path)).Length;
                fileContent = binaryReader.ReadBytes((Int32)byteLength);
                fs.Close();
                fs.Dispose();
                binaryReader.Close();

                // convert to base64 from array byte
                string dataBase64 = System.Convert.ToBase64String(fileContent, 0, fileContent.Length);

                return dataBase64;
            }
            catch (Exception ex) { }
            return null;
        }

        // chuyển ngược lại từ base64 thành file excel
        private bool Base64ToExcel(string base64, string fileName, string path)
        {
            try
            {
                // convert to byte array
                byte[] fileContent = Convert.FromBase64String(base64);

                // create file excel in client
                File.WriteAllBytes(path + fileName, fileContent);
                return true;
            }
            catch { }
            return false;
        }
    }
}

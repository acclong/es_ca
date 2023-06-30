using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using esDigitalSignature;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Runtime.InteropServices;
//using TestStack.White.UIItems.WindowItems;
//using TestStack.White.UIItems;
//using TestStack.White.Factory;
//using TestStack.White.WebBrowser;
//using TestStack.White.UIItems.Finders;
using OpenQA.Selenium;
using OpenQA.Selenium.IE;

namespace NLDC_SignOnWeb
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            //Disable web browser trong quá trình ký
            IntPtr browserHandle = GetForegroundWindow();
            //EnableWindow(browserHandle, false);

            try
            {
                //uint processId = 11516;
                //GetWindowThreadProcessId(browserHandle, out processId);

                //TestStack.White.Application app = TestStack.White.Application.Attach((int)processId);
                ////Window[] windows = app.GetWindows().ToArray();
                //Window window = app.GetWindow("Home Page - My ASP.NET Application - Mozilla Firefox", InitializeOption.NoCache);

                //// find control and input text ...
                //IUIItem[] textBoxes = window.GetMultiple(SearchCriteria.ByControlType( System.Windows.Automation.ControlType. ) );
                //TestStack.White.UIItems.TextBox searchTextBox = textBoxes[1] as TestStack.White.UIItems.TextBox;
                //if ( searchTextBox != null )
                //    searchTextBox.Text = "search phrase";
                
                //Button button = windows[0].get.Get<Button>("save"); button.Click();
                //IUIItem txtBase64 = window.Items[10]; 
                //window.Get<TestStack.White.UIItems.TextBox>(TestStack.White.UIItems.Finders.SearchCriteria.
                //IUIItem btnUpload = window.Items[65];
                //string tmp = txtBase64.Name;
                //int count = window.Items.Count;
                //for (int i = 50; i < count; i++)
                //{
                //    IUIItem control = window.Items[i];
                //    tmp += i.ToString() + ". " + control.Name + ";";
                //}

                //MessageBox.Show(tmp);
                //txtBase64.SetValue("apptowweb");

                IWebDriver driver = new OpenQA.Selenium.IE.InternetExplorerDriver();
                driver.Navigate().GoToUrl("https://www.google.com.vn/");
                IWebElement inputTextUser = driver.FindElement(By.ClassName("gsfi"));
                MessageBox.Show(inputTextUser.GetAttribute("value"));
                inputTextUser.Clear();
                inputTextUser.SendKeys("This text fill from app");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            //EnableWindow(browserHandle, true);            

            ////Thư mục AppData
            //string uploadPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\esSignOnWeb\data\upload.ess";
            //string downloadPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\esSignOnWeb\data\download.ess";
            //string logPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\esSignOnWeb\log.txt";
            ////Đường dẫn các file tạm
            //List<string> tempPath = new List<string>();
            ////Ghi log
            //SaveLog(logPath, "----- BEGIN SESSION -----");

            //try
            //{
            //    string[] fileBase64;    //Chứa nội dung của các file
            //    string[] fileExt;       //Chứa đuôi file

            //    //Lấy tham số dạng ESSignProtocol:[fileExt1;fileExt2;]
            //    string[] param = args[0].Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
            //    if (param.Length != 2)
            //    {
            //        MessageBox.Show("Tham số chương trình không đúng!", "NLDC - Ứng dụng chữ ký số trên website", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //        SaveToUploadFile(uploadPath, "HUY");
            //        SaveLog(logPath, "HUY: Tham số chương trình không đúng.");
            //        return;
            //    }
            //    else
            //    {
            //        //Tách lấy mảng extension tương ứng với nhiều file
            //        SaveLog(logPath, param[1]);
            //        fileExt = param[1].Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            //        //Tạo các file tạm
            //        for (int i = 0; i < fileExt.Length; i++)
            //        {
            //            tempPath.Add(Path.GetTempFileName() + fileExt[i]);
            //        }
            //    }

            //    //Đọc Base64 từ file và convert to file tạm
            //    fileBase64 = File.ReadAllText(downloadPath).Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            //    if (fileBase64.Length != fileExt.Length)
            //    {
            //        MessageBox.Show("Tham số chương trình không đúng!", "NLDC - Ứng dụng chữ ký số trên website", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //        SaveToUploadFile(uploadPath, "HUY");
            //        SaveLog(logPath, "HUY: Mảng extension và mảng nội dung không bằng nhau.");
            //        return;
            //    }
            //    for (int i = 0; i < fileExt.Length; i++)
            //    {
            //        Common.ConvertBase64ToFile(fileBase64[i], tempPath[i]);
            //        SaveLog(logPath, "Lưu temp: " + tempPath[i]);
            //    }
            //    File.Delete(downloadPath);

            //    //Chọn chữ ký
            //    X509Certificate2 cert = Common.SelectCertificateFromStore("Danh sách chữ ký", "Hãy chọn chứng thư số của bạn", IntPtr.Zero);
            //    SetForegroundWindow(browserHandle);
            //    if (cert == null)
            //    {
            //        //Set nội dung file upload là "HUY"
            //        SaveToUploadFile(uploadPath, "HUY");
            //        SaveLog(logPath, "HUY: Tiến trình ký bị hủy bởi người dùng.");
            //        return;
            //    }

            //    //Ký file
            //    foreach (string path in tempPath)
            //    {
            //        using (ESDigitalSignatureManager dsm = new ESDigitalSignatureManager(File.ReadAllBytes(path), Path.GetExtension(path)))
            //        {
            //            dsm.Sign(cert);
            //            SaveLog(logPath, "Ký: " + path);
            //        }
            //    }

            //    //Convert thành base64
            //    string base64 = "";
            //    foreach (string path in tempPath)
            //    {
            //        base64 += Common.ConvertFileToBase64(path) + ";";
            //        SaveLog(logPath, "Lưu base64: " + path);
            //    }

            //    //Kiểm tra dung lượng Max=10MB
            //    if (System.Text.ASCIIEncoding.ASCII.GetByteCount(base64) > 10485760)
            //    {
            //        //Set nội dung file upload là "HUY"
            //        SaveToUploadFile(uploadPath, "HUY");
            //        SaveLog(logPath, "HUY: File lớn hơn 10MB.");
            //        return;
            //    }

            //    //lưu vào file upload
            //    SaveToUploadFile(uploadPath, base64);

            //    ////Đẩy base64 lên web page và ấn btnUpload
            //    //if (browser == "firefox")
            //    //{
            //    //    //Dùng ES Sign Add-on
            //    //}
            //    //else if (browser == "chrome")
            //    //{
            //    //    //Dùng ES Sign Add-on
            //    //}
            //    //else if (browser == "IE")
            //    //{

            //    //}

            //    //Console.WriteLine("\nPress any key to continue...");
            //    //Console.ReadKey();
            //    SaveLog(logPath, "Succeed");
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Ký file thất bại: Lỗi trong quá trình ký!\n\n" + ex.Message, "NLDC - Ứng dụng ký số trên website",
            //        MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    SaveToUploadFile(uploadPath, "HUY");
            //    SaveLog(logPath, "HUY: " + ex.Message);
            //    return;
            //}
            //finally
            //{
            //    //Enable web browser
            //    EnableWindow(browserHandle, true);
            //    SetForegroundWindow(browserHandle);
            //    //Xóa file tạm
            //    foreach (string path in tempPath)
            //    {
            //        if (File.Exists(path))
            //            File.Delete(path);
            //        string tempFile = Path.GetDirectoryName(path) + "\\" + Path.GetFileNameWithoutExtension(path);
            //        if (File.Exists(tempFile))
            //            File.Delete(tempFile);
            //    }
                
            //    SaveLog(logPath, "----- END SESSION -----\n\n");
            //}
        }

        private static void SaveLog(string fileLog, string text)
        {
            using (StreamWriter file = new StreamWriter(fileLog, true))
            {
                file.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss.sss") + " # " + text);
            }
        }

        private static void SaveToUploadFile(string fileUpload, string text)
        {
            File.WriteAllText(fileUpload, text);
        }

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool EnableWindow(IntPtr hWnd, bool bEnable);

        [DllImport("user32.dll")]
        static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

        [DllImport("user32.dll", SetLastError = true)]
        static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint processId);
    }
}

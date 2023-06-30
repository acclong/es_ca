using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using esDigitalSignature;
using NDde.Client;
using SHDocVw;
using System.IO;

namespace esSignOnWeb
{
    class Program
    {
        static void Main(string[] args)
        {
            string fileBase64 = "test=base64";
            string fileName = "Bangke.xlsx";
            string browser = "IE";

            Console.WriteLine("Raw command-line: \n\t" + Environment.CommandLine);

            foreach (string item in args)
            {
                Console.WriteLine(item);
            }

            ////Lấy tham số [base64_of_file] [filename] (2 tham số cách nhau bởi space)
            //if (args.Length != 2)
            //{
            //    MessageBox.Show("Tham số chương trình không đúng!", "NLDC - Ứng dụng chữ ký số trên website", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}
            //fileBase64 = args[0];
            //fileName = args[1];
            //browser = args[3];

            //Convert Base64 to file

            //Chọn chữ ký
            //Common.SelectCertificateFromStore("Danh sách chữ ký", "Hãy chọn chứng thư số của bạn");

            //Ký file

            //Convert signed file to base64

            //Đẩy base64 lên web page và ấn btnUpload
            if (browser == "IE")
                PushBase64ToIE(fileBase64);

            //string tmp = GetBrowserURL("firefox");
            //Console.WriteLine(tmp);

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        private static string GetBrowserURL(string browser)
        {
            try
            {
                DdeClient dde = new DdeClient(browser, "WWW_GetWindowInfo");
                dde.Connect();
                string url = dde.Request("ctl00$MainContent$txtBase64", int.MaxValue);
                string[] text = url.Split(new string[] { "\",\"" }, StringSplitOptions.RemoveEmptyEntries);
                dde.Disconnect();
                return text[0].Substring(1);
            }
            catch
            {
                return null;
            }
        }

        private static void PushBase64ToIE(string base64)
        {
            InternetExplorer ie = new InternetExplorer();

            string filename;

            filename = Path.GetFileNameWithoutExtension(ie.FullName).ToLower();

            if (filename.Equals("iexplore"))
                Console.WriteLine("Web Site   : {0}", ie.LocationURL);

            if (filename.Equals("explorer"))
                Console.WriteLine("Hard Drive : {0}", ie.LocationURL);
        }
    }
}

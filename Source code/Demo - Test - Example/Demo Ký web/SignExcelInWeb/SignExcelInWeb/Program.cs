using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using Microsoft.VisualBasic.FileIO;

namespace SignExcelInWeb
{
    class Program
    {
        static SignOffice SO = new SignOffice();
        static string pathSignedFolder, txtFinish, buttonFinish, serialNumb, fileName;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new SignForm());
            SignForm_Load();
        }

        static void SignForm_Load()
        {
            try
            {
                // khởi tạo các giá trị 
                pathSignedFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Mozilla\\Firefox\\";
                string sDataConfig = Clipboard.GetText();
                string[] DataConfig = sDataConfig.Split(';');
                serialNumb = DataConfig[0].Split(':')[1];
                fileName = DataConfig[1].Split(':')[1];
                string dataFile = DataConfig[2].Split(':')[1];
                // khởi tạo file ký
                Base64ToExcel(dataFile, fileName, pathSignedFolder);
                // ký file cần ký
                X509Certificate2 cert = SO.getCertBySNo(serialNumb);
                SO.signOfficeFileUsingPDSM(pathSignedFolder + fileName, cert);
                // đẩy dữ liệu vào clipboard chờ add ons đẩy lên website
                Clipboard.Clear();
                string base64 = ExcelToBase64(pathSignedFolder + fileName);
                Clipboard.SetText("ESVCGM " + fileName + " " + base64);
                // xóa file cần ký
                FileSystem.DeleteFile(pathSignedFolder + fileName, UIOption.OnlyErrorDialogs, RecycleOption.DeletePermanently);
                string clipboard = Clipboard.GetText();
            }
            catch
            {
                Clipboard.Clear();
                string base64 = ExcelToBase64(pathSignedFolder + fileName);
                Clipboard.SetText("ESVCGM ");
                // xóa file cần ký
                FileSystem.DeleteFile(pathSignedFolder + fileName, UIOption.OnlyErrorDialogs, RecycleOption.DeletePermanently);
                string clipboard = Clipboard.GetText();
            }
        }

        //chuyển file excel thành chuỗi string base64
        static string ExcelToBase64(string pathFile)
        {
            try
            {
                // convert to array byte
                byte[] fileContent = null;
                System.IO.FileStream fs = new System.IO.FileStream(pathFile, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                System.IO.BinaryReader binaryReader = new System.IO.BinaryReader(fs);
                long byteLength = new System.IO.FileInfo(pathFile).Length;
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
        static bool Base64ToExcel(string base64, string fileName, string path)
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

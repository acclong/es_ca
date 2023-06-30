using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace AppUpdate
{
    public partial class frmUpdate : Form
    {
        private double total, index, ktotal, kindex;
        private int iComplete = 0;
        private string keyUpdate = "NLDC";
        private string KeyPath = "Software\\NLDC";
        private string NameUpdate = "AppSign";


        public string pathFTP = "";
        public string localPathDownload = "";
        public string localInstall = "";
        public string userNameFTP = "";
        public string passwordFTP = "";
        public string appName = "";
        public bool bComplete = false;
        public string sSaveInputAppSign = "";

        public frmUpdate()
        {
            InitializeComponent();
        }

        public frmUpdate(string strSave)
        {
            InitializeComponent();

            try
            {
                string strCheck = strSave.Split('#').First();
                string version = strSave.Split('#')[1];
                sSaveInputAppSign = strSave.Split('#').Last();
                if (strCheck == keyUpdate)
                {
                    lblShow.Text = "Khởi tạo quá trình cập nhật";
                    //Lấy các giá trị đầu vào
                    pathFTP = ReadRegistry("PathFile", KeyPath) + version + ".upd";
                    localPathDownload = ReadRegistry("LocalPathDownload", KeyPath);
                    localInstall = ReadRegistry("LocalInstall", KeyPath);
                    userNameFTP = ReadRegistry("UserNameFTP", KeyPath);
                    passwordFTP = ReadRegistry("PasswordFTP", KeyPath);
                    appName = ReadRegistry("AppName", KeyPath);
                    //Chạy cập nhật
                    backgroundWorker.RunWorkerAsync();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Cập nhật phần mềm ký lỗi!", ex.Message);
            }
        }

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                if (iComplete == 0)
                    Downloadfile(pathFTP, userNameFTP, passwordFTP, localPathDownload);
                else if (iComplete == 1)
                    InstallFile(localPathDownload + "\\" + Path.GetFileName(pathFTP), localInstall);
            }
            catch
            {
                iComplete = 99;
                MessageBox.Show("Download file cập nhật không thành công.\n\n Hãy thử lại!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (iComplete == 0)
            {
                prgUpdate.Value = e.ProgressPercentage;
                kindex = index / 1024;
                ktotal = total / 1024;
                lblShow.Text = "Downloading ... " + String.Format("{0}", kindex) + " / " + String.Format("{0}", ktotal + " kb");
            }
            else if (iComplete == 1)
            {
                prgUpdate.Value = e.ProgressPercentage;
                lblShow.Text = "Installing ... " + e.ProgressPercentage + " %";
            }
        }

        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (iComplete == 1)
            {
                prgUpdate.Value = 0;
                backgroundWorker.RunWorkerAsync();
                lblShow.Text = "";
            }
            else if (iComplete == 2)
            {
                //Chạy file bat nếu có
                var txtFiles = Directory.EnumerateFiles(localInstall, "*.bat");
                foreach (string currentFile in txtFiles)
                {
                    System.Diagnostics.Process proc = new System.Diagnostics.Process();
                    proc.StartInfo.FileName = currentFile;
                    proc.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                    proc.StartInfo.CreateNoWindow = true;
                    proc.Start();
                    proc.WaitForExit();
                }

                if (chkContinue.Checked)
                {
                    //Chạy app ký
                    ProcessStartInfo startInfo = new ProcessStartInfo();
                    startInfo.FileName = localInstall + "\\" + appName;
                    startInfo.Arguments = sSaveInputAppSign;
                    Process.Start(startInfo);
                    this.Close();
                }
            }
            else
            {
                lblShow.Text = "Cập nhật chương trình ký không thành công!";
                System.Threading.Thread.Sleep(3000);
                this.Close();
            }
        }

        public int Downloadfile(string PathFileDownload, string UserName, string Password, string LocalPath)
        {
            string sDownloadMode = PathFileDownload.Split(':').First();
            if (sDownloadMode == "ftp")
            {
                #region download by ftp
                FtpWebRequest requestFileDownload;
                try
                {
                    requestFileDownload = (FtpWebRequest)WebRequest.Create(PathFileDownload);
                    requestFileDownload.Method = WebRequestMethods.Ftp.GetFileSize;
                    requestFileDownload.Credentials = new NetworkCredential(UserName, Password);

                    FtpWebResponse responseFileDownload = (FtpWebResponse)requestFileDownload.GetResponse();
                    total = responseFileDownload.ContentLength;
                    index = 0;
                    responseFileDownload.Close();

                    requestFileDownload = (FtpWebRequest)WebRequest.Create(PathFileDownload);
                    requestFileDownload.Method = WebRequestMethods.Ftp.DownloadFile;
                    requestFileDownload.Credentials = new NetworkCredential(UserName, Password);

                    using (responseFileDownload = (FtpWebResponse)requestFileDownload.GetResponse())
                    {
                        using (Stream responseStream = responseFileDownload.GetResponseStream())
                        {
                            string FileNameLocal = LocalPath + "\\" + Path.GetFileName(PathFileDownload);
                            using (FileStream writeStream = new FileStream(FileNameLocal, FileMode.Create))
                            {
                                int Length = 2048;
                                Byte[] buffer = new Byte[Length];
                                int bytesRead = responseStream.Read(buffer, 0, Length);

                                while (bytesRead > 0)
                                {
                                    //ghi dư liệu
                                    writeStream.Write(buffer, 0, bytesRead);
                                    index += bytesRead;
                                    bytesRead = responseStream.Read(buffer, 0, Length);
                                    //hiển thị progressBar
                                    int iProgressPercentage = (int)(index * 100 / total);
                                    backgroundWorker.ReportProgress(iProgressPercentage);

                                    System.Threading.Thread.Sleep(2);
                                }
                                iComplete = 1;
                            }
                        }
                    }

                    requestFileDownload = null;
                }
                catch
                {
                    iComplete = 99;
                    requestFileDownload = null;
                }
                #endregion
            }
            else if (sDownloadMode == "http")
            {
                #region download by http
                HttpWebRequest requestHTTP;
                try
                {
                    // lấy kích thước file
                    requestHTTP = (HttpWebRequest)WebRequest.Create(PathFileDownload);
                    using (HttpWebResponse responseHTTP = (HttpWebResponse)requestHTTP.GetResponse())
                    {
                        total = responseHTTP.ContentLength;
                        index = 0;
                        responseHTTP.Close();

                        // download file
                        requestHTTP = (HttpWebRequest)WebRequest.Create(PathFileDownload);
                        using (Stream responseStream = responseHTTP.GetResponseStream())
                        {
                            string FileNameLocal = LocalPath + "\\" + Path.GetFileName(PathFileDownload);
                            using (FileStream writeStream = new FileStream(FileNameLocal, FileMode.Create))
                            {
                                int Length = 2048;
                                Byte[] buffer = new Byte[Length];
                                int bytesRead = responseStream.Read(buffer, 0, Length);

                                while (bytesRead > 0)
                                {
                                    //ghi dư liệu
                                    writeStream.Write(buffer, 0, bytesRead);
                                    index += bytesRead;
                                    bytesRead = responseStream.Read(buffer, 0, Length);
                                    //hiển thị progressBar
                                    int iProgressPercentage = (int)(index * 100 / total);
                                    backgroundWorker.ReportProgress(iProgressPercentage);

                                    System.Threading.Thread.Sleep(2);
                                }
                                iComplete = 1;
                            }
                        }
                    }
                    requestHTTP = null;
                }
                catch
                {
                    iComplete = 99;
                    requestHTTP = null;
                }
                #endregion
            }
            return iComplete;
        }

        public int InstallFile(string FileInstall, string localInstall)
        {
            string PathTemp = Directory.GetParent(localInstall).FullName + "Temp";
            try
            {
                //Tạo file temp
                Directory.CreateDirectory(PathTemp);
                backgroundWorker.ReportProgress(10);

                //Copy tất cả file trong forlder cài đặt vào file temp
                CopyDirectory(localInstall, PathTemp, true);
                backgroundWorker.ReportProgress(30);

                //Giải nén file update
                DecompressToDirectory(FileInstall, localInstall);
                backgroundWorker.ReportProgress(80);

                //Xóa các file không cần thiết
                File.Delete(FileInstall);
                DeleteDirectory(PathTemp);
                Directory.Delete(PathTemp);
                backgroundWorker.ReportProgress(100);

                iComplete = 2;
            }
            catch (Exception ex)
            {
                string strError = ex.Message;
                if (Directory.Exists(PathTemp) && Directory.GetFiles(PathTemp).Length > 0)
                {
                    CopyDirectory(PathTemp, localInstall, true);
                    DeleteDirectory(PathTemp);
                    Directory.Delete(PathTemp);
                    File.Delete(FileInstall);
                }
                iComplete = 99;
            }

            return iComplete;
        }

        public void CompressFile(string sDir, string sRelativePath, GZipStream zipStream)
        {
            //Compress file name
            char[] chars = sRelativePath.ToCharArray();
            zipStream.Write(BitConverter.GetBytes(chars.Length), 0, sizeof(int));
            foreach (char c in chars)
                zipStream.Write(BitConverter.GetBytes(c), 0, sizeof(char));

            //Compress file content
            byte[] bytes = File.ReadAllBytes(Path.Combine(sDir, sRelativePath));
            zipStream.Write(BitConverter.GetBytes(bytes.Length), 0, sizeof(int));
            zipStream.Write(bytes, 0, bytes.Length);
        }

        public bool DecompressFile(string sDir, GZipStream zipStream)
        {
            //Decompress file name
            byte[] bytes = new byte[sizeof(int)];
            int Readed = zipStream.Read(bytes, 0, sizeof(int));
            if (Readed < sizeof(int))
                return false;

            int iNameLen = BitConverter.ToInt32(bytes, 0);
            bytes = new byte[sizeof(char)];
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < iNameLen; i++)
            {
                zipStream.Read(bytes, 0, sizeof(char));
                char c = BitConverter.ToChar(bytes, 0);
                sb.Append(c);
            }
            string sFileName = sb.ToString();

            //Decompress file content
            bytes = new byte[sizeof(int)];
            zipStream.Read(bytes, 0, sizeof(int));
            int iFileLen = BitConverter.ToInt32(bytes, 0);

            bytes = new byte[iFileLen];
            zipStream.Read(bytes, 0, bytes.Length);

            string sFilePath = Path.Combine(sDir, sFileName);
            string sFinalDir = Path.GetDirectoryName(sFilePath);
            if (!Directory.Exists(sFinalDir))
                Directory.CreateDirectory(sFinalDir);

            using (FileStream outFile = new FileStream(sFilePath, FileMode.Create, FileAccess.Write, FileShare.None))
                outFile.Write(bytes, 0, iFileLen);

            return true;
        }

        public void CompressDirectory(string sInDir, string sOutFile)
        {
            string[] sFiles = Directory.GetFiles(sInDir, "*.*", SearchOption.AllDirectories);
            int iDirLen = sInDir[sInDir.Length - 1] == Path.DirectorySeparatorChar ? sInDir.Length : sInDir.Length + 1;

            using (FileStream outFile = new FileStream(sOutFile, FileMode.Create, FileAccess.Write, FileShare.None))
            using (GZipStream str = new GZipStream(outFile, CompressionMode.Compress))
                foreach (string sFilePath in sFiles)
                {
                    string sRelativePath = sFilePath.Substring(iDirLen);
                    CompressFile(sInDir, sRelativePath, str);
                }
        }

        public void DecompressToDirectory(string sCompressedFile, string sDir)
        {
            using (FileStream inFile = new FileStream(sCompressedFile, FileMode.Open, FileAccess.Read, FileShare.None))
            using (GZipStream zipStream = new GZipStream(inFile, CompressionMode.Decompress, true))
                while (DecompressFile(sDir, zipStream)) ;
        }

        public bool CopyDirectory(string SourcePath, string DestinationPath, bool overwriteexisting)
        {
            bool ret = true;
            try
            {
                if (Directory.Exists(SourcePath))
                {
                    if (Directory.Exists(DestinationPath) == false)
                        Directory.CreateDirectory(DestinationPath);

                    foreach (string fls in Directory.GetFiles(SourcePath))
                    {
                        FileInfo flinfo = new FileInfo(fls);
                        flinfo.CopyTo(DestinationPath + "\\" + flinfo.Name, overwriteexisting);
                    }
                    foreach (string drs in Directory.GetDirectories(SourcePath))
                    {
                        DirectoryInfo drinfo = new DirectoryInfo(drs);
                        if (CopyDirectory(drs, DestinationPath + "\\" + drinfo.Name, overwriteexisting) == false)
                            ret = false;
                    }
                }
                else
                {
                    ret = false;
                }
            }
            catch (Exception ex)
            {
                string strError = ex.Message;
                ret = false;
            }
            return ret;
        }

        public bool DeleteDirectory(string SourcePath)
        {
            bool ret = true;
            try
            {
                foreach (string fls in Directory.GetFiles(SourcePath))
                {
                    FileInfo flinfo = new FileInfo(fls);
                    flinfo.Delete();
                }
                foreach (string drs in Directory.GetDirectories(SourcePath))
                {
                    DirectoryInfo drinfo = new DirectoryInfo(drs);
                    DeleteDirectory(drinfo.FullName);
                    drinfo.Delete();
                }
            }
            catch (Exception ex)
            {
                string strError = ex.Message;
                ret = false;
            }
            return ret;
        }

        // code đọc registry trong CurrentUser - Ninhtq
        public string ReadRegistry(string KeyName, string SubKey)
        {
            // Opening the registry key
            RegistryKey rk = Registry.CurrentUser;
            // Open a subKey as read-only
            RegistryKey sk1 = rk.OpenSubKey(SubKey);
            // If the RegistrySubKey doesn't exist -> (null)
            if (sk1 == null)
            {
                return null;
            }
            else
            {
                try
                {
                    // If the RegistryKey exists I get its value
                    // or null is returned.
                    return (string)sk1.GetValue(KeyName.ToUpper());
                }
                catch
                {
                    return null;
                }
            }
        }
    }
}

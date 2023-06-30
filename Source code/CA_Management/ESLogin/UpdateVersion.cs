using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;
using Microsoft.Win32;
using System.Data.SqlClient;
using System.Threading;

namespace ESLogin
{
    public partial class UpdateVersion : Form
    {
        private Int64 total, index, ktotal, kindex;

        public bool complete = false;
        public string ServerName;
        public string fileName = null, localPath = null;

        public UpdateVersion()
        {
            InitializeComponent();
        }

        private void UpdateVersion_Load(object sender, EventArgs e)
        {
            backgroundWorkerDownload.RunWorkerAsync();
        }
        
        private void backgroundWorkerDownload_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                string sDownloadMode = fileName.Split(':').First();
                if (sDownloadMode == "ftp")
                {
                    #region download by ftp
                    FtpWebRequest requestFileDownload = (FtpWebRequest)WebRequest.Create(fileName);
                    //requestFileDownload.Credentials = new NetworkCredential("", "");
                    requestFileDownload.Method = WebRequestMethods.Ftp.GetFileSize;

                    FtpWebResponse responseFileDownload = (FtpWebResponse)requestFileDownload.GetResponse();
                    total = responseFileDownload.ContentLength;
                    index = 0;
                    responseFileDownload.Close();

                    requestFileDownload = (FtpWebRequest)WebRequest.Create(fileName);
                    //requestFileDownload.Credentials = new NetworkCredential("", "");
                    requestFileDownload.Method = WebRequestMethods.Ftp.DownloadFile;

                    responseFileDownload = (FtpWebResponse)requestFileDownload.GetResponse();
                    Stream responseStream = responseFileDownload.GetResponseStream();
                    fileName = fileName.Split('/').Last();
                    FileStream writeStream = new FileStream(localPath + "\\" + fileName, FileMode.Create);

                    int Length = 2048;
                    Byte[] buffer = new Byte[Length];
                    int bytesRead = responseStream.Read(buffer, 0, Length);

                    while (bytesRead > 0)
                    {
                        writeStream.Write(buffer, 0, bytesRead);
                        index += bytesRead;
                        bytesRead = responseStream.Read(buffer, 0, Length);
                        int iProgressPercentage = (int)(index * 100 / total);
                        backgroundWorkerDownload.ReportProgress(iProgressPercentage);
                        System.Threading.Thread.Sleep(2);
                    }
                    complete = true;
                    responseStream.Close();
                    writeStream.Close();

                    requestFileDownload = null;
                    responseFileDownload = null;
                    #endregion
                }
                else if (sDownloadMode == "http")
                {
                    #region download by http
                    // lấy kích thước file
                    HttpWebRequest requestHTTP = (HttpWebRequest)WebRequest.Create(fileName);
                    HttpWebResponse responseHTTP = (HttpWebResponse)requestHTTP.GetResponse();
                    total = responseHTTP.ContentLength;
                    index = 0;
                    responseHTTP.Close();

                    // download file
                    requestHTTP = (HttpWebRequest)WebRequest.Create(fileName);
                    responseHTTP = (HttpWebResponse)requestHTTP.GetResponse();
                    Stream responseStream = responseHTTP.GetResponseStream();
                    fileName = fileName.Split('/').Last();
                    FileStream writeStream = new FileStream(localPath + "\\" + fileName, FileMode.Create);

                    int Length = 2048;
                    Byte[] buffer = new Byte[Length];
                    int bytesRead = responseStream.Read(buffer, 0, Length);

                    while (bytesRead > 0)
                    {
                        writeStream.Write(buffer, 0, bytesRead);
                        index += bytesRead;
                        bytesRead = responseStream.Read(buffer, 0, Length);
                        int iProgressPercentage = (int)(index * 100 / total);
                        backgroundWorkerDownload.ReportProgress(iProgressPercentage);
                        System.Threading.Thread.Sleep(2);
                    }
                    complete = true;
                    responseStream.Close();
                    writeStream.Close();

                    responseHTTP = null;
                    requestHTTP = null;
                    #endregion
                }
            }
            catch
            {
                complete = false;
                MessageBox.Show("Download file cập nhật không thành công.\n\n Hãy thử lại!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void backgroundWorkerDownload_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBarUpdate.Value = e.ProgressPercentage;
            kindex = index / 1024;
            ktotal = total / 1024;
            lbdownPrecent.Text = String.Format("{0}", kindex) + " / " + String.Format("{0}", ktotal + " kb");
        }

        private void backgroundWorkerDownload_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.Close();
        }
    }
}

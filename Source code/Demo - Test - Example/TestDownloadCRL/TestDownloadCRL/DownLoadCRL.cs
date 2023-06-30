using ServiceDebuggerHelper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Timers;
using System.IO;

namespace TestDownloadCRL
{
    public partial class DownLoadCRL : ServiceBase, IDebuggableService
    {
        System.Timers.Timer timer;
        string sPathSave = "";
        string sPeriod = "1";
        string sPathConfig = "";
        string sConnectString = "";
        DataTable dt = new DataTable();
        Help Help = new Help();
        public DownLoadCRL()
        {
            InitializeComponent();
            CanPauseAndContinue = true;
        }

        protected override void OnStart(string[] args)
        {
            // đọc giá trị từ file config
            sPathConfig = AppDomain.CurrentDomain.BaseDirectory + "ConfigDB.xml";
            Help.ReadConfigDB(sPathConfig, ref sConnectString);

            // khởi tạo và chạy timer 
            timer = new System.Timers.Timer(2000);
            timer.Elapsed += OnTimedEvent;
            timer.Enabled = true;
        }

        protected override void OnStop()
        {
            // tắt timer
            timer.Enabled = false;
        }

        protected override void OnPause()
        {

        }

        protected override void OnContinue()
        {

        }

        /****************************************/

        #region IDebuggableService Members

        public void Start(string[] args)
        {
            OnStart(args);
        }

        public void StopService()
        {
            OnStop();
        }

        public void Pause()
        {
            OnPause();
        }

        public void Continue()
        {
            OnContinue();
        }

        #endregion

        /****************************************/
        
        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            Run();
        }

        public void Run()
        {
            bool state;
            clsDB db = new clsDB(sConnectString);

            // lấy giá trị biến sPathSave và sPeriod
            dt = db.Q_CONFIG_SelectAll();
            if (dt.Rows.Count == 0)
            {
                state = false;
                goto Result;
            }
            else
            {
                sPathSave = dt.Rows[0]["Value"].ToString();
                sPeriod = dt.Rows[1]["Value"].ToString();
            }

            // lấy danh sách link CRL và download
            dt = db.CA_CertificationAuthority_SelectCRL();
            if (dt.Rows.Count == 0)
            {
                state = false;
                goto Result;
            }
            int iDownload = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string link = dt.Rows[i]["CRL_URL"].ToString();
                try
                {
                    if (!Help.DownloadFile(link, sPathSave))
                    {
                        iDownload++;
                    }
                }
                catch
                {
                    iDownload++;
                }
            }            
            if (iDownload > 0)
                state = false;
            else
                state = true;
        Result:
            // Nếu false thì download lại sau 30s
            if (state == false)
                sPeriod = "0.5";
            timer.Interval = 60000 * Convert.ToDouble(sPeriod);
        }
    }
}

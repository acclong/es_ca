using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Windows.Forms;
using ServiceDebuggerHelper;

namespace TestDownloadCRL
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            if (args.Length > 0 && args[0].ToLower().Equals("/debug"))
            {
                Application.Run(new ServiceRunner(new DownLoadCRL()));
            }
            else
            {
                System.ServiceProcess.ServiceBase[] ServicesToRun = new System.ServiceProcess.ServiceBase[]
                    {
                        new DownLoadCRL()
                    };

                System.ServiceProcess.ServiceBase.Run(ServicesToRun);
            }
        }
    }
}

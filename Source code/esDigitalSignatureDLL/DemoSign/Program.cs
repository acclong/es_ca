using esDigitalSignature;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DemoSign
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Toantk 14/8/2015: set Temporary Environment Variable để thiết lập app HSM chạy ở chế độ NORMAL hay WLD
            Environment.SetEnvironmentVariable(Common.ET_PTKC_GENERAL_LIBRARY_MODE, Common.NORMAL, EnvironmentVariableTarget.Process);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}

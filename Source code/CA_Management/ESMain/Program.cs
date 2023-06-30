using esDigitalSignature;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ESMain
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Toantk 14/8/2015: set Temporary Environment Variable để thiết lập app HSM chạy ở chế độ NORMAL
            Environment.SetEnvironmentVariable(Common.ET_PTKC_GENERAL_LIBRARY_MODE, Common.NORMAL, EnvironmentVariableTarget.Process);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());
            try
            {
                ModMain Call = new ModMain();
                //Application.Run(new frmMain_v2());
            }
            catch (Exception err)
            {
                MessageBox.Show(err.ToString());
            }
        }
    }
}

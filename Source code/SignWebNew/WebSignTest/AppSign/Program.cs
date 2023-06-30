using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace AppSign
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        
        ////[STAThread]
        ////static void Main()
        ////{
        ////    Application.EnableVisualStyles();
        ////    Application.SetCompatibleTextRenderingDefault(false);
        ////    Application.Run(new MainForm());
        ////}

        [STAThread]
        static void Main(string[] args)
        {
            IntPtr browserHandle = new IntPtr();
            try
            {
                //Khóa trình duyệt -- Disable web browser trong quá trình ký
                browserHandle = GetForegroundWindow();
                EnableWindow(browserHandle, false);

                //Chạy chương trình ký
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainForm(args[0]));
           }
            catch (Exception ex)
            {
                MessageBox.Show("Chương trình ký lỗi rồi nhé!");
            }
            finally
            {
                //Mở khóa trình duyệt
                EnableWindow(browserHandle, true);
            }
        }

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool EnableWindow(IntPtr hWnd, bool bEnable);
    }
}

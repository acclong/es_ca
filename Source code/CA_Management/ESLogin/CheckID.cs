using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management;

namespace ESLogin
{
    public class CheckID
    {
        public static string GetSystemInfo()
        {
            string SystemInfo = "";
            string sCpuID = GetIdentifier("Win32_Processor", "ProcessorId").Trim();
            string sDiskID = GetIdentifier("Win32_BaseBoard", "SerialNumber").Trim();

            char[] cComID = (sCpuID + sDiskID).ToCharArray();
            for (int i = 0; i < cComID.Length; i++)
            {
                SystemInfo += cComID[i];
                if (i % 5 == 4 && i < cComID.Length - 1)
                    SystemInfo += "-";
            }

            return SystemInfo;
        }

        //Return a hardware identifier
        private static string GetIdentifier(string wmiClass, string wmiProperty)
        {
            string result = "";
            ManagementClass mc = new ManagementClass(wmiClass);
            ManagementObjectCollection moc = mc.GetInstances();
            foreach (ManagementObject mo in moc)
            {
                //Only get the first one
                try
                {
                    result = mo[wmiProperty].ToString();
                    break;
                }
                catch { }
            }
            return result;
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace esDigitalSignature
{
    public static class LogesDigitalSignature
    {
        private const string FILE_NAME = @"esDigitalSignature.txt";

        public static void WriteLog(string text)
        {
            try
            {
                string path = AppDomain.CurrentDomain.BaseDirectory;
                using (FileStream fs = new FileStream($"{path}\\logs\\Info\\{FILE_NAME}", FileMode.OpenOrCreate, FileAccess.Write))
                {
                    StreamWriter m_streamWriter = new StreamWriter(fs);
                    // Write to the file using StreamWriter class
                    m_streamWriter.BaseStream.Seek(0, SeekOrigin.End);
                    m_streamWriter.WriteLine("{0}: {1}", DateTime.Now.ToLongTimeString(), text);
                    m_streamWriter.Flush();
                }
            }
            catch
            { }
        }
    }
}

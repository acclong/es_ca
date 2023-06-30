using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.ComponentModel;
using System.IO;

namespace ValidateCert
{
    /// <summary>
    /// x509 certificate utilities
    /// Richard Ginzburg - richard@ginzburgconsulting.com
    /// </summary>
    public static class CertUtil
    {
        private const string CERT_CRL_EXTENSION = "2.5.29.31";
        private const string CRL_CRL_EXTENSION = "2.5.29.46";

        public static bool IsCertificateInCrl(X509Certificate2 cert)
        {
            GetCertificateChain(cert);
            string certCrlUrl = GetBaseCrlUrl(cert);
            return IsCertificateInCrl(cert, certCrlUrl);
            //return IsCertificateInCrl(cert, "C:\\Users\\TranKim\\Desktop\\CA\\vnptca.crl");
        }

        public static bool IsCertificateInCrl(X509Certificate2 cert, string url)
        {
            byte[] rgRawCrl;
            if (url.Contains("http") || url.Contains("ftp"))
            {
                WebClient wc = new WebClient();
                rgRawCrl = wc.DownloadData(url);
            }
            else
                rgRawCrl = File.ReadAllBytes(url);

            IntPtr phCertStore = IntPtr.Zero;
            IntPtr pvContext = IntPtr.Zero;
            GCHandle hCrlData = new GCHandle();
            GCHandle hCryptBlob = new GCHandle();
            try
            {
                hCrlData = GCHandle.Alloc(rgRawCrl, GCHandleType.Pinned);
                WinCrypt32.CRYPTOAPI_BLOB stCryptBlob;
                stCryptBlob.cbData = rgRawCrl.Length;
                stCryptBlob.pbData = hCrlData.AddrOfPinnedObject();
                hCryptBlob = GCHandle.Alloc(stCryptBlob, GCHandleType.Pinned);

                if (!WinCrypt32.CryptQueryObject(
                    WinCrypt32.CERT_QUERY_OBJECT_BLOB,
                    hCryptBlob.AddrOfPinnedObject(),
                    WinCrypt32.CERT_QUERY_CONTENT_FLAG_CRL,
                    WinCrypt32.CERT_QUERY_FORMAT_FLAG_BINARY,
                    0,
                    IntPtr.Zero,
                    IntPtr.Zero,
                    IntPtr.Zero,
                    ref phCertStore,
                    IntPtr.Zero,
                    ref pvContext
                    ))
                {
                    throw new Win32Exception(Marshal.GetLastWin32Error());
                }

                WinCrypt32.CRL_CONTEXT stCrlContext = (WinCrypt32.CRL_CONTEXT)Marshal.PtrToStructure(pvContext, typeof(WinCrypt32.CRL_CONTEXT));
                WinCrypt32.CRL_INFO stCrlInfo = (WinCrypt32.CRL_INFO)Marshal.PtrToStructure(stCrlContext.pCrlInfo, typeof(WinCrypt32.CRL_INFO));

                if (IsCertificateInCrl(cert, stCrlInfo))
                {
                    return true;
                }
                else
                {
                    url = GetDeltaCrlUrl(stCrlInfo);
                    if (!string.IsNullOrEmpty(url))
                    {
                        return IsCertificateInCrl(cert, url);
                    }
                }
            }
            finally
            {
                if (hCrlData.IsAllocated) hCrlData.Free();
                if (hCryptBlob.IsAllocated) hCryptBlob.Free();
                if (!pvContext.Equals(IntPtr.Zero))
                {
                    WinCrypt32.CertFreeCRLContext(pvContext);
                }
            }

            return false;
        }

        private static bool IsCertificateInCrl(X509Certificate2 cert, WinCrypt32.CRL_INFO stCrlInfo)
        {
            IntPtr rgCrlEntry = stCrlInfo.rgCRLEntry;

            for (int i = 0; i < stCrlInfo.cCRLEntry; i++)
            {
                WinCrypt32.CRL_ENTRY stCrlEntry = (WinCrypt32.CRL_ENTRY)Marshal.PtrToStructure(rgCrlEntry, typeof(WinCrypt32.CRL_ENTRY));

                //Get serial
                string serial = WinCrypt32.ConvertBLOBToSerial(stCrlEntry.SerialNumber);

                //Get RevocationDate
                DateTime date = WinCrypt32.ConvertFILETIMEToDate(stCrlEntry.RevocationDate);

                if (cert.SerialNumber == serial)
                {
                    return true;
                }

                //Next entry pointer
                rgCrlEntry = (IntPtr)(rgCrlEntry.ToInt64() + Marshal.SizeOf(typeof(WinCrypt32.CRL_ENTRY)));
            }
            return false;
        }

        /*
         * 
         * Get Info
         * 
         */
        private static string GetBaseCrlUrl(X509Certificate2 cert)
        {
            try
            {
                return (from X509Extension extension in cert.Extensions
                        where extension.Oid.Value.Equals(CERT_CRL_EXTENSION)
                        select GetCrlUrlFromExtension(extension)).Single();
            }
            catch
            {
                return null;
            }
        }

        private static string GetDeltaCrlUrl(WinCrypt32.CRL_INFO stCrlInfo)
        {
            IntPtr rgExtension = stCrlInfo.rgExtension;
            X509Extension deltaCrlExtension = null;

            for (int i = 0; i < stCrlInfo.cExtension; i++)
            {
                WinCrypt32.CERT_EXTENSION stCrlExt = (WinCrypt32.CERT_EXTENSION)Marshal.PtrToStructure(rgExtension, typeof(WinCrypt32.CERT_EXTENSION));

                if (stCrlExt.Value.pbData != IntPtr.Zero && stCrlExt.pszObjId == CRL_CRL_EXTENSION)
                {
                    byte[] rawData = new byte[stCrlExt.Value.cbData];
                    Marshal.Copy(stCrlExt.Value.pbData, rawData, 0, rawData.Length);
                    deltaCrlExtension = new X509Extension(stCrlExt.pszObjId, rawData, stCrlExt.fCritical);
                    break;
                }

                rgExtension = (IntPtr)((Int32)rgExtension + Marshal.SizeOf(typeof(WinCrypt32.CERT_EXTENSION)));
            }
            if (deltaCrlExtension == null)
            {
                return null;
            }
            return GetCrlUrlFromExtension(deltaCrlExtension);
        }

        private static string GetCrlUrlFromExtension(X509Extension extension)
        {
            try
            {
                Regex rx = new Regex("http://.*crl");
                string raw = new AsnEncodedData(extension.Oid, extension.RawData).Format(false);
                return rx.Match(raw).Value;
            }
            catch
            {
                return null;
            }
        }

        private static List<X509Certificate2> GetCertificateChain(X509Certificate2 cert)
        {
            List<X509Certificate2> lst = new List<X509Certificate2>();

            X509Chain chain = new X509Chain();
            chain.ChainPolicy.RevocationMode = X509RevocationMode.Online;
            chain.ChainPolicy.RevocationFlag = X509RevocationFlag.EntireChain;
            chain.ChainPolicy.VerificationFlags = X509VerificationFlags.NoFlag;

            chain.Build(cert);
            foreach (X509ChainElement c in chain.ChainElements)
                lst.Add(c.Certificate);

            return lst;
        }
    }
}

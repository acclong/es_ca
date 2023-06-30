using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace ValidateCert
{
    /// <summary>
    /// Custom x509 certificate validator
    /// Richard Ginzburg - richard@ginzburgconsulting.com
    /// </summary>
    public class MyX509Validator
    {
        public static void Validate(X509Certificate2 certificate)
        {
            //var myChainPolicy = new X509ChainPolicy
            //{
            //    RevocationMode = X509RevocationMode.Online,
            //    RevocationFlag = X509RevocationFlag.EntireChain,
            //    VerificationFlags = X509VerificationFlags.NoFlag,
            //    UrlRetrievalTimeout = new TimeSpan(0, 0, 10),
            //    VerificationTime = DateTime.Now
            //};
            //var chain = new X509Chain(true) { ChainPolicy = myChainPolicy };

            //if (!chain.Build(certificate))
            //    throw new CryptographicException("Certificate validation failed when building chain");

            if (CertUtil.IsCertificateInCrl(certificate))
                throw new CryptographicException("Certificate is REVOKED by CRL");
            else
                throw new CryptographicException("Certificate is NOT REVOKED by CRL");
        }
    }
}

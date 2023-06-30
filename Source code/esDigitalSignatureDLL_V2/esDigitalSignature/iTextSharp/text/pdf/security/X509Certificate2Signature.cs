using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;

namespace esDigitalSignature.iTextSharp.text.pdf.security
{
    /// <summary>
    /// Creates a signature using a X509Certificate2. It supports smartcards without 
    /// exportable private keys.
    /// </summary>
    internal class X509Certificate2Signature : IExternalSignature
    {
        /// <summary>
        /// The certificate with the private key
        /// </summary>
        private X509Certificate2 certificate;
        /** The hash algorithm. */
        private String hashAlgorithm;
        /** The encryption algorithm (obtained from the private key) */
        private String encryptionAlgorithm;

        /// <summary>
        /// Creates a signature using a X509Certificate2. It supports smartcards without 
        /// exportable private keys.
        /// </summary>
        /// <param name="certificate">The certificate with the private key</param>
        /// <param name="hashAlgorithm">The hash algorithm for the signature. As the Windows CAPI is used
        /// to do the signature the only hash guaranteed to exist is SHA-1</param>
        public X509Certificate2Signature(X509Certificate2 certificate, String hashAlgorithm)
        {
            //TOANTK: Get private key from Certificate Store nếu chưa có
            if (!certificate.HasPrivateKey)
            {
                certificate.PrivateKey = GetPrivateKeyForSigning(certificate);
            }
            
            this.certificate = certificate;
            this.hashAlgorithm = DigestAlgorithms.GetDigest(DigestAlgorithms.GetAllowedDigests(hashAlgorithm));            

            if (!certificate.HasPrivateKey)
                throw new ArgumentException("No private key.");
            if (certificate.PrivateKey is RSACryptoServiceProvider)
                encryptionAlgorithm = "RSA";
            else if (certificate.PrivateKey is DSACryptoServiceProvider)
                encryptionAlgorithm = "DSA";
            else
                throw new ArgumentException("Unknown encryption algorithm " + certificate.PrivateKey);
        }

        /// <summary>
        /// Creates a signature using a X509Certificate2. It supports HSM.
        /// </summary>
        /// <param name="certificate">The certificate with the private key</param>
        /// <param name="hashAlgorithm">The hash algorithm for the signature. As the Windows CAPI is used
        /// to do the signature the only hash guaranteed to exist is SHA-1</param>
        /// <param name="providerHSM">Giao tiếp HSM đã khởi tạo session đăng nhập</param>
        public X509Certificate2Signature(X509Certificate2 certificate, String hashAlgorithm, HSMServiceProvider providerHSM)
        {
            this.certificate = certificate;
            this.hashAlgorithm = DigestAlgorithms.GetDigest(DigestAlgorithms.GetAllowedDigests(hashAlgorithm));

            AsymmetricAlgorithm key = null;
            bool ownKey = false;
            //TOANTK: Get PRIVATE KEY
            if (certificate.HasPrivateKey)
            {
                key = certificate.PrivateKey;
            }
            else
            {
                ownKey = true;
                key = GetPrivateKeyForSigning(certificate, providerHSM);
            }

            try
            {
                //ToanTK: check PRIVATE KEY để lấy giải thuật ký (RSA/DSA)
                if (key is RSACryptoServiceProvider || key is esDigitalSignature.HSM_RSA)
                    encryptionAlgorithm = "RSA";
                else if (key is DSACryptoServiceProvider || key is esDigitalSignature.HSM_DSA)
                    encryptionAlgorithm = "DSA";
                else
                    throw new ArgumentException("Unknown encryption algorithm " + certificate.PrivateKey);
            }
            finally
            {
                if (key != null && ownKey)
                    ((IDisposable)key).Dispose();
            }
        }

        public virtual byte[] Sign(byte[] message)
        {
            //TOANTK: Kí mess (chưa băm) bằng USB Token
            if (certificate.PrivateKey is RSACryptoServiceProvider)
            {
                RSACryptoServiceProvider rsa = (RSACryptoServiceProvider)certificate.PrivateKey;
                return rsa.SignData(message, hashAlgorithm);
            }
            else
            {
                DSACryptoServiceProvider dsa = (DSACryptoServiceProvider)certificate.PrivateKey;
                return dsa.SignData(message);
            }
        }

        public virtual byte[] Sign(byte[] message, HSMServiceProvider providerHSM)
        {
            //TOANTK: Kí mess (chưa băm) bằng HSM
            //băm
            HashAlgorithm hashAlg = (HashAlgorithm)CryptoConfig.CreateFromName(hashAlgorithm);
            byte[] hashVal = hashAlg.ComputeHash(message);

            //Kí chuỗi hash
            string sOID = CryptoConfig.MapNameToOID(hashAlg.ToString());
            return providerHSM.SignHash(hashAlg.Hash, sOID);
        }

        /**
         * Returns the hash algorithm.
         * @return  the hash algorithm (e.g. "SHA-1", "SHA-256,...")
         * @see com.itextpdf.text.pdf.security.ExternalSignature#getHashAlgorithm()
         */
        public virtual String GetHashAlgorithm()
        {
            return hashAlgorithm;
        }

        /**
         * Returns the encryption algorithm used for signing.
         * @return the encryption algorithm ("RSA" or "DSA")
         * @see com.itextpdf.text.pdf.security.ExternalSignature#getEncryptionAlgorithm()
         */
        public virtual String GetEncryptionAlgorithm()
        {
            return encryptionAlgorithm;
        }

        /*
         * 
         * TOANTK thêm hàm lấy private key nếu không có trong X509Certificate2
         * 
         */
        /// <summary>
        /// lookup the private key using the given identity
        /// </summary>
        /// <param name="signer">X509Cert</param>
        /// <returns>IDisposable asymmetric algorithm that serves as a proxy to the private key.  Caller must dispose
        /// of properly.</returns>
        private static AsymmetricAlgorithm GetPrivateKeyForSigning(X509Certificate2 signer)
        {
            // look for appropriate certificates
            X509Store store = new X509Store(StoreLocation.CurrentUser);

            try
            {
                store.Open(OpenFlags.OpenExistingOnly | OpenFlags.ReadOnly);

                X509Certificate2Collection collection = (X509Certificate2Collection)store.Certificates;

                collection = collection.Find(X509FindType.FindByThumbprint, signer.Thumbprint, true);
                if (collection.Count > 0)
                {
                    if (collection.Count > 1)
                        throw new CryptographicException("DuplicateCertificateOnStore");

                    signer = collection[0];
                }
                else
                    throw new CryptographicException("CannotLocateCertificateOnStore");
            }
            finally
            {
                store.Close();
            }

            // get the corresponding AsymmetricAlgorithm
            return signer.PrivateKey;
        }

        /// <summary>
        /// lookup the private key using the given identity
        /// </summary>
        /// <param name="signer">X509Cert</param>
        /// <returns>IDisposable asymmetric algorithm that serves as a proxy to the private key.  Caller must dispose
        /// of properly.</returns>
        private static AsymmetricAlgorithm GetPrivateKeyForSigning(X509Certificate2 signer, HSMServiceProvider providerHSM)
        {
            //TOANTK: get the corresponding AsymmetricAlgorithm nếu chưa khởi tạo private key
            if (providerHSM.PrivateKey != null)
                return providerHSM.PrivateKey;
            else
            {
                return providerHSM.LoadPrivateKeyByCertificate(signer);
            }
        }
    }
}

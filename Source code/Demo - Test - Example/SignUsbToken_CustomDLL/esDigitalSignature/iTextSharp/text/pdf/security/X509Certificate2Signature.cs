using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;

namespace esDigitalSignature.iTextSharp.text.pdf.security {
    /// <summary>
    /// Creates a signature using a X509Certificate2. It supports smartcards without 
    /// exportable private keys.
    /// </summary>
    public class X509Certificate2Signature : IExternalSignature {
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
        public X509Certificate2Signature(X509Certificate2 certificate, String hashAlgorithm) {
            this.certificate = certificate;
            this.hashAlgorithm = DigestAlgorithms.GetDigest(DigestAlgorithms.GetAllowedDigests(hashAlgorithm));

            //ToanTK: check PRIVATE KEY để lấy giải thuật ký (RSA/DSA)
            //if (!certificate.HasPrivateKey)
            //    throw new ArgumentException("No private key.");
            //if (certificate.PrivateKey is RSACryptoServiceProvider)
            //    encryptionAlgorithm = "RSA";
            //else if (certificate.PrivateKey is DSACryptoServiceProvider)
            //    encryptionAlgorithm = "DSA";
            //else
            //    throw new ArgumentException("Unknown encryption algorithm " + certificate.PrivateKey);
            encryptionAlgorithm = "RSA";
        }

        public virtual byte[] Sign(byte[] message) {
            ////TOANTK: Kí mess (chưa băm) bằng USB Token
            //if (certificate.PrivateKey is RSACryptoServiceProvider)
            //{
            //    RSACryptoServiceProvider rsa = (RSACryptoServiceProvider)certificate.PrivateKey;
            //    return rsa.SignData(message, hashAlgorithm);
            //}
            //else
            //{
            //    DSACryptoServiceProvider dsa = (DSACryptoServiceProvider)certificate.PrivateKey;
            //    return dsa.SignData(message);
            //}

            //TOANTK: Kí mess (chưa băm) bằng HSM
            //Băm
            string XmlDsigRSASHA1Url = "http://www.w3.org/2000/09/xmldsig#rsa-sha1";
            SignatureDescription signatureDescription = CryptoConfig.CreateFromName(XmlDsigRSASHA1Url) as SignatureDescription;
            if (signatureDescription == null)
                throw new CryptographicException("Cryptography_Xml_SignatureDescriptionNotCreated");
            HashAlgorithm hashAlg = signatureDescription.CreateDigest();
            if (hashAlg == null)
                throw new CryptographicException("Cryptography_Xml_CreateHashAlgorithmFailed");
            byte[] hashVal = hashAlg.ComputeHash(message);
            //Kí chuỗi hash
            string sOID = CryptoConfig.MapNameToOID(hashAlg.ToString());
            return HSMSignatureProcessor.SignHash(hashAlg.Hash, sOID);
        }

        /**
         * Returns the hash algorithm.
         * @return  the hash algorithm (e.g. "SHA-1", "SHA-256,...")
         * @see com.itextpdf.text.pdf.security.ExternalSignature#getHashAlgorithm()
         */
        public virtual String GetHashAlgorithm() {
            return hashAlgorithm;
        }
        
        /**
         * Returns the encryption algorithm used for signing.
         * @return the encryption algorithm ("RSA" or "DSA")
         * @see com.itextpdf.text.pdf.security.ExternalSignature#getEncryptionAlgorithm()
         */
        public virtual String GetEncryptionAlgorithm() {
            return encryptionAlgorithm;
        }
    }
}

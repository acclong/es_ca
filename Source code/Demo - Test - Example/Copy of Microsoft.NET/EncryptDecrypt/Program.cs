using System;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Security.Cryptography;

namespace EncryptDecrypt
{
    /**
     * The following sample shows how to encrypt and decrypt a string using .NET Framework security system interface.
     * You must have a connected token with a certificate on it.
     * This sample tries to find a certificate which its provider is eToken.
    */
    class Program
    {
        static String dataToEncrypt = "1234567890"; // Original string to encrypt.
		
		/**
         * Main function for encrypt/decrypt sample
         */
        static void Main(string[] args)
        {
            EncryptDecryptUsingDotNETSecuritySystemInterface();
        }

        /**
         * The following sample shows how to encrypt and decrypt a string using .NET Framework security system interface.
         * You must have a connected token with a certificate on it.
         * This sample tries to find a certificate which its provider is eToken.
         */
        static void EncryptDecryptUsingDotNETSecuritySystemInterface()
        {
            // Look for a certificate
            X509Certificate2 x509Cert = FindFirstCertificate();
            if (x509Cert == null)
            {
                Console.WriteLine("Please connect a token with certificate and try again.");
                return;
            }
            Console.WriteLine(String.Format("Original string: {0}", dataToEncrypt));

            // Encrypt the original string
            byte[] enc = Encrypt(x509Cert, Encoding.ASCII.GetBytes(dataToEncrypt));
            if (enc != null)
            {
                Console.WriteLine(String.Format("Encrypted string: {0}", Convert.ToBase64String(enc)));

                // Decrypt the encrypted string
                byte[] dec = Decrypt(x509Cert, enc);
                if (dec != null)
                {
                    Console.WriteLine(String.Format("Decrypted string: {0}", Encoding.ASCII.GetString(dec)));
                }
            }
        }

        
        /**
         * This function search the certificate store and try to find the first certificate which it provider is 
         * "eToken Base Cryptographic Provider".
         */
        static public X509Certificate2 FindFirstCertificate()
        {
            try
            {
                X509Store store;
                X509Certificate2 selectedCert = null;               

                store = new X509Store(StoreName.My, StoreLocation.CurrentUser);
                store.Open(OpenFlags.ReadOnly | OpenFlags.OpenExistingOnly);

                foreach (X509Certificate2 cert in store.Certificates)
                {
                    try
                    {
                       
                        RSACryptoServiceProvider csp = (RSACryptoServiceProvider)cert.PrivateKey;
                        if (csp != null)
                        {
                            // make sure that the certificate has private key and the provider is eToken.
                            if (csp.CspKeyContainerInfo.ProviderName != "eToken Base Cryptographic Provider")
                            {                              
                                continue;
                            }
                        }
                        else
                        {
                            // Private key not found, filtering certificate
                            continue;
                        }
                        selectedCert = cert;
                    }
                    catch (Exception )
                    {
                    }
                }
                store.Close();             
                return selectedCert;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }           
        }
        
        /**
         * This function encrypt a given bytes using a X509 certificate.
         */
        static byte[] Encrypt(X509Certificate2 cert, byte[] data)
        {           
            try
            {
                RSACryptoServiceProvider rsa;

                if (cert != null)
                {
                    rsa = (RSACryptoServiceProvider)cert.PublicKey.Key;
                    byte[] enc = Encrypt(rsa, data);
                    return enc;
                }
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        /**
         * This function decrypt a given encrypted bytes using a X509 certificate.
         */
        static byte[] Decrypt(X509Certificate2 cert, byte[] enc)
        {           
            try
            {
                RSACryptoServiceProvider rsa;

                if (cert != null)
                {
                    rsa = (RSACryptoServiceProvider)cert.PrivateKey;
                    byte[] data = Decrypt(rsa, enc);
                    return data;
                }
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }           
        }

        static byte[] Encrypt(RSA rsa, byte[] input)
        {

            // by default this will create a 128 bits AES (Rijndael) object
            SymmetricAlgorithm sa = SymmetricAlgorithm.Create();
            ICryptoTransform ct = sa.CreateEncryptor();
            byte[] encrypt = ct.TransformFinalBlock(input, 0, input.Length);

            RSAPKCS1KeyExchangeFormatter fmt = new RSAPKCS1KeyExchangeFormatter(rsa);
            byte[] keyex = fmt.CreateKeyExchange(sa.Key);

            // return the key exchange, the IV (public) and encrypted data
            byte[] result = new byte[keyex.Length + sa.IV.Length + encrypt.Length];
            Buffer.BlockCopy(keyex, 0, result, 0, keyex.Length);
            Buffer.BlockCopy(sa.IV, 0, result, keyex.Length, sa.IV.Length);
            Buffer.BlockCopy(encrypt, 0, result, keyex.Length + sa.IV.Length, encrypt.Length);
            return result;
        }

        static byte[] Decrypt(RSA rsa, byte[] input)
        {
            // by default this will create a 128 bits AES (Rijndael) object
            SymmetricAlgorithm sa = SymmetricAlgorithm.Create();

            byte[] keyex = new byte[rsa.KeySize >> 3];
            Buffer.BlockCopy(input, 0, keyex, 0, keyex.Length);

            RSAPKCS1KeyExchangeDeformatter def = new RSAPKCS1KeyExchangeDeformatter(rsa);
            byte[] key = def.DecryptKeyExchange(keyex);

            byte[] iv = new byte[sa.IV.Length];
            Buffer.BlockCopy(input, keyex.Length, iv, 0, iv.Length);

            ICryptoTransform ct = sa.CreateDecryptor(key, iv);
            byte[] decrypt = ct.TransformFinalBlock(input, keyex.Length + iv.Length, input.Length - (keyex.Length + iv.Length));
            return decrypt;
        }

    }


}

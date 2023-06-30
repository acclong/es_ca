using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using esDigitalSignature.eToken;

namespace esDigitalSignature
{
    public class HSMSignatureProcessor
    {
        public static byte[] SignData(byte[] dataToSign)
        {
            PKCS11.Finalize();
            PKCS11.Initialize("cryptoki.dll");
            PKCS11.Slot[] slots = PKCS11.GetSlotList(true);
            if (slots.Length > 0)
            {                
                PKCS11.Slot slot = new PKCS11.Slot();
                foreach (PKCS11.Slot slot1 in slots)
                {
                    if (slot1.id == 2)
                    {
                        slot = slot1;
                        break;
                    }
                }

                PKCS11.Session session = PKCS11.OpenSession(slot,
                  PKCS11.CKF_RW_SESSION | PKCS11.CKF_SERIAL_SESSION);

                session.Login(PKCS11.CKU_USER, "123456");

                //PKCS11.Object[] certificates = session.FindObjects(new PKCS11.Attribute[]  {
                //    new PKCS11.Attribute(PKCS11.CKA_TOKEN, true),
                //    new PKCS11.Attribute(PKCS11.CKA_CLASS, PKCS11.CKO_CERTIFICATE)
                //  });

                //foreach (PKCS11.Object certificate in certificates)
                //{
                //    byte[] value = (byte[])certificate.Get(session, PKCS11.CKA_VALUE);
                //    X509Certificate2 x509 = new X509Certificate2(value);
                //    Console.WriteLine(x509.Subject);
                //}

                PKCS11.Object[] PIkeys = session.FindObjects(new PKCS11.Attribute[]  {
                    new PKCS11.Attribute(PKCS11.CKA_CLASS, PKCS11.CKO_PRIVATE_KEY),
                    new PKCS11.Attribute(PKCS11.CKA_LABEL, "NinhtqPI")
                  });

                if (PIkeys.Count() > 0)
                {
                    //TOANTK: fix cứng PKCS11.CKM_RSA_PKCS
                    PKCS11.Mechanism signMech = new PKCS11.Mechanism(PKCS11.CKM_RSA_PKCS, null);
                    byte[] SignedArray = session.Sign(signMech, PIkeys[0], dataToSign);

                    PKCS11.Finalize();
                    return SignedArray;
                }
                else
                {
                    PKCS11.Finalize();
                    throw new Exception("Can not find the key!");
                }
            }
            else
            {
                PKCS11.Finalize();
                throw new Exception("Can not find any slot!");
            }
        }

        public static int VerifyData(byte[] dataToSign, byte[] signature)
        {
            PKCS11.Finalize();
            PKCS11.Initialize("cryptoki.dll");
            PKCS11.Slot[] slots = PKCS11.GetSlotList(true);
            if (slots.Length > 0)
            {
                PKCS11.Slot slot = slots[2];
                PKCS11.Session session = PKCS11.OpenSession(slot,
                  PKCS11.CKF_RW_SESSION | PKCS11.CKF_SERIAL_SESSION);

                session.Login(PKCS11.CKU_USER, "123456");

                PKCS11.Object[] PUkeys = session.FindObjects(new PKCS11.Attribute[]  {
                    new PKCS11.Attribute(PKCS11.CKA_CLASS, PKCS11.CKO_PUBLIC_KEY),
                    new PKCS11.Attribute(PKCS11.CKA_LABEL, "NinhtqCert")
                  });

                if (PUkeys.Count() > 0)
                {
                    PKCS11.Mechanism signMech = new PKCS11.Mechanism(PKCS11.CKM_SHA1_RSA_PKCS, null);
                    int result = session.Verify(signMech, PUkeys[0], dataToSign, signature);

                    PKCS11.Finalize();
                    return result;
                }
                else
                {
                    PKCS11.Finalize();
                    throw new Exception("Can not find the key!");
                }
            }
            else
            {
                PKCS11.Finalize();
                throw new Exception("Can not find any slot!");
            }
        }

        public static byte[] SignHash(byte[] hashvalue, string oid)
        {
            //Constant
            byte[] sha1AlgoID = PKCS11.DigestAlgoID[oid];

            //Tạo DigestInfo = HashAlgorithmID || HashVaue (see EMSA-PKCS-v1.5, PKCS #1 v2.2 RSA Cryptography Standart)
            byte[] digestInfo = new byte[sha1AlgoID.Length + hashvalue.Length];
            sha1AlgoID.CopyTo(digestInfo, 0);
            hashvalue.CopyTo(digestInfo, sha1AlgoID.Length);


            //Kí chuỗi DigestInfo bằng CKM_RSA_PKCS
            return HSMSignatureProcessor.SignData(digestInfo);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Security.Cryptography;
using System.Text;

namespace TestCSPSmartCard
{
    class Program
    {
        static unsafe void Main(string[] args)
        {
            const string xmlPubKey =
                @"<RSAKeyValue><Modulus>jyDy7cVpXHpMHG3odjcUmIZaQvCAT+dPQTfuoba0fUX/M9Nii609oxEswPk4D11MnZmv7f5mG456/I+Bf4r8KgQPBKMGTRaNd0wuMQOKvG9gwolEhL+jBIkmpodUK1+99qj7e1k4i4sB/k9TdecyJF4skYiMxR95bQu9PyjpUfc=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";

            try
            {
                //Nhập mật khẩu và tạo kết nối CSP với token
                SecureString pwd;
                char[] scPwd = { 'e', 's', '@', '1', '2', '3', '4', '5', '6' };
                fixed(char* pChars = scPwd)       
                {   
                    pwd = new SecureString(pChars, scPwd.Length);       
                } 


                CspParameters csp = 
                    new CspParameters(1,
                        "Viettel Group CSP V4.0",       //HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Cryptography\Defaults\Provider.
                        "ĐẶNG MINH SANG - CÁN BỘ KỸ THUẬT - CÔNG TY CỔ PHẦN GIẢI PHÁP QUẢN LÝ NĂNG LƯỢNG",
                        new System.Security.AccessControl.CryptoKeySecurity(),
                        pwd);

                RSACryptoServiceProvider rsaCsp = new RSACryptoServiceProvider(csp);
                AsymmetricSignatureFormatter rsaSign = new RSAPKCS1SignatureFormatter(rsaCsp);
                rsaSign.SetHashAlgorithm("SHA1");
                
                //Data to sign
                byte[] toSign = new byte[20];
                Random rnd = new Random((int)DateTime.Now.Ticks);
                rnd.NextBytes(toSign);
                Console.WriteLine("Data to sign : " + BitConverter.ToString(toSign));

                //Ký
                byte[] signature = rsaSign.CreateSignature(toSign);
                Console.WriteLine();
                Console.WriteLine("Signature: " + BitConverter.ToString(signature));

                //Data to sign
                byte[] toSign2 = new byte[20];
                Random rnd2 = new Random((int)DateTime.Now.Ticks);
                rnd.NextBytes(toSign2);
                Console.WriteLine("Data to sign : " + BitConverter.ToString(toSign2));

                //Ký
                byte[] signature2 = rsaSign.CreateSignature(toSign2);
                Console.WriteLine();
                Console.WriteLine("Signature: " + BitConverter.ToString(signature2));

                //RSACryptoServiceProvider rsaCsp2 = new RSACryptoServiceProvider();
                //rsaCsp2.FromXmlString(xmlPubKey);
                
                //RSAPKCS1SignatureDeformatter rsaVerify = new RSAPKCS1SignatureDeformatter(rsaCsp2);
                //rsaVerify.SetHashAlgorithm("SHA1");
                //bool verified = rsaVerify.VerifySignature(toSign, signature);

                //Console.WriteLine();
                //Console.WriteLine("Signature verified [{0}]", verified);

                Console.WriteLine("Press any key to stop...");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Crypto error: " + ex.Message);
            }
        }
    }
}

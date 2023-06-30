/////////////////////////////////////////////////////////////////////////////
// eToken SDK Sample
// 
//  LICENSE AGREEMENT:
//  1. COPYRIGHTS AND TRADEMARKS
//  The eTokenTM system and its documentation are copyright (C) 1985 to present,
//  by Aladdin Knowledge Systems Ltd. All rights reserved.
//
//  eToken is a trademark and ALADDIN KNOWLEDGE SYSTEMS LTD is a registered trademark 
//  of Aladdin Knowledge Systems Ltd. All  other  trademarks,  brands,  and product 
//  names used in this guide are trademarks of their respective owners.
//
//  2. Title & Ownership
//  THIS IS A LICENSE AGREEMENT AND NOT AN AGREEMENT FOR SALE. 
//  The Code IS NOT FOR SALE and is and shall remain as Aladdin's sole property. 
//  All right, title and interest in and to the Code, including associated 
//  intellectual property rights, in and to the Code are and will remain with Aladdin.
//
//  3.   Disclaimer of Warranty
//  THE CODE CONSTITUTES A CODE SAMPLE AND IS NOT A COMPLETE PRODUCT AND MAY CONTAIN 
//  DEFECTS, AND PRODUCE UNINTENDED OR ERRONEOUS RESULTS. THE CODE IS PROVIDED "AS IS", 
//  WITHOUT WARRANTY OF ANY KIND. ALADDIN DISCLAIMS ALL WARRANTIES, EITHER EXPRESS OR 
//  IMPLIED, INCLUDING BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY 
//  AND FITNESS FOR A PARTICULAR PURPOSE.
//  The entire risk arising out of the use or performance of the Code remains with you.
//
//  4.   No Liability For Damages
//  Without derogating from the above, in no event shall Aladdin be liable for any damages 
//  whatsoever (including, without limitation, damages for loss of business profits, business 
//  interruption, loss of business information, or other pecuniary loss) arising out of the 
//  use of or inability to use the Code, even if Aladdin has been advised of the possibility 
//  of such damages. Your sole recourse in the event of any dissatisfaction with the Code is 
//  to stop using it and return it.
/////////////////////////////////////////////////////////////////////////////

using System;
using System.Security.Cryptography.X509Certificates;
using eToken;


namespace CertEnum
{
    /*
      * This demo loads and display all certificates found on SafeNet
      * token.<p />
      * For executing this demo you need SAC 8.1 SP1 or newer
      * installed on your machine.                                   
    */
    class Program
    {

        /**
         * Main function for display certificates sample
         */
        static void Main(string[] args)
        {
            DisplayCertificates();
        }

        /*
          * This demo loads and display all certificates found on SafeNet
          * token.<p />
          * For executing this demo you need SAC 8.1 SP1 or newer
          * installed on your machine.                                   
        */
        static void DisplayCertificates()
        {
            PKCS11.Initialize("cryptoki.dll");
            PKCS11.Slot[] slots = PKCS11.GetSlotList(true);
            if (slots.Length > 0)
            {
                PKCS11.Slot slot = slots[1];
                PKCS11.Session session = PKCS11.OpenSession(slot,
                  PKCS11.CKF_RW_SESSION | PKCS11.CKF_SERIAL_SESSION);

                session.Login(PKCS11.CKU_USER, "123456");

                //PKCS11.Object[] certificates = session.FindObjects(new PKCS11.Attribute[]  {
                //    new PKCS11.Attribute(PKCS11.CKA_TOKEN, true),
                //    new PKCS11.Attribute(PKCS11.CKA_CLASS, PKCS11.CKO_CERTIFICATE),
                //    new PKCS11.Attribute(PKCS11.CKA_CERTIFICATE_CATEGORY, 0),
                //  });

                PKCS11.Object[] certificates = session.FindObjects(new PKCS11.Attribute[]  {
                    new PKCS11.Attribute(PKCS11.CKA_TOKEN, true),
                    new PKCS11.Attribute(PKCS11.CKA_CLASS, PKCS11.CKO_CERTIFICATE)
                  });

                foreach (PKCS11.Object certificate in certificates)
                {
                    byte[] value = (byte[])certificate.Get(session, PKCS11.CKA_VALUE);
                    X509Certificate2 x509 = new X509Certificate2(value);
                    Console.WriteLine(x509.Subject);
                }
            }
            else
            {
                Console.WriteLine("Please connect a token and try again.");
            }
        }
    }
}

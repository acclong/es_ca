using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Packaging;
using System.Security.Cryptography.X509Certificates;
using eToken;

namespace CertEnum
{
    class clsSignWordExcel
    {
        private void SignAllParts(Package package, X509Certificate certificate)
        {
            if (package == null) throw new ArgumentNullException("SignAllParts(package)");
            List<Uri> PartstobeSigned = new List<Uri>();
            List<PackageRelationshipSelector> SignableReleationships = new List<PackageRelationshipSelector>();

            foreach (PackageRelationship relationship in package.GetRelationships())
            {
                // Pass the releationship of the root. This is decided based on the RT_OfficeDocument 
                CreateListOfSignableItems(relationship, PartstobeSigned, SignableReleationships);
            }
            // Create the DigitalSignature Manager
            PackageDigitalSignatureManager dsm = new PackageDigitalSignatureManager(package);
            dsm.CertificateOption = CertificateEmbeddingOption.InSignaturePart;
            dsm.Sign(PartstobeSigned, certificate, SignableReleationships);
        }

        private void CreateListOfSignableItems(PackageRelationship relationship, List<Uri> PartstobeSigned, List<PackageRelationshipSelector> SignableReleationships)
        {
            // This function adds the releation to SignableReleationships. And then it gets the part based on the releationship. Parts URI gets added to the PartstobeSigned list.
            PackageRelationshipSelector selector = new PackageRelationshipSelector(relationship.SourceUri, PackageRelationshipSelectorType.Id, relationship.Id);
            SignableReleationships.Add(selector);
            if (relationship.TargetMode == TargetMode.Internal)
            {
                PackagePart part = relationship.Package.GetPart(PackUriHelper.ResolvePartUri(relationship.SourceUri, relationship.TargetUri));
                if (PartstobeSigned.Contains(part.Uri) == false)
                {
                    PartstobeSigned.Add(part.Uri);
                    // GetRelationships Function: Returns a Collection Of all the releationships that are owned by the part.
                    foreach (PackageRelationship childRelationship in part.GetRelationships())
                    {
                        CreateListOfSignableItems(childRelationship, PartstobeSigned, SignableReleationships);
                    }
                }
            }
        }

        /// <summary>
        /// Sign Excel File using PackageDigitalSignatureManager
        /// </summary>
        /// <param name="filePath">đường dẫn file</param>
        /// <param name="cert">Certificate tương ứng</param>
        /// <returns></returns>
        public bool signOfficeFileUsingPDSM(string filePath, X509Certificate2 cert)
        {
            bool bSignOk = false;
            // Open the Package    
            using (Package package = Package.Open(filePath))
            {
                SignAllParts(package, cert);
                bSignOk = true;
            }
            return bSignOk;
        }

        public X509Certificate2 DisplayCertificates()
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

                X509Certificate2 x509 = new X509Certificate2();

                foreach (PKCS11.Object certificate in certificates)
                {
                    byte[] value = (byte[])certificate.Get(session, PKCS11.CKA_VALUE);
                    x509 = new X509Certificate2(value);
                    //Console.WriteLine(x509.Subject);
                }
                return x509;
            }
            else
            {
                Console.WriteLine("Please connect a token and try again.");
                return null;
            }
        }
    }
}

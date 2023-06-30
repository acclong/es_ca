using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography.X509Certificates;
using SignPackage;

namespace SignUsbToken
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
            try
            {
                dsm.Sign(PartstobeSigned, certificate, SignableReleationships);
            }
            catch (Exception ex)
            {
                dsm.RemoveAllSignatures();
                throw ex;
            }
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

        public string VerifyAllSignatures(string filePath)
        {
            using (Package package = Package.Open(filePath))
            {
                if (package == null) throw new ArgumentNullException("VerifyAllSignatures(package)");
                // Create the DigitalSignature Manager
                PackageDigitalSignatureManager dsm = new PackageDigitalSignatureManager(package);
                VerifyResult result = dsm.VerifySignatures(false);
                return result.ToString();
            }
        }
    }
}

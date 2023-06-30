using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography.X509Certificates;
using esDigitalSignature.OfficePackage;
using System.IO;

namespace esDigitalSignature
{
    /// <summary>
    /// Lớp quản lý ký và chữ ký số trên file Office Document. Cần dispose khi không sử dụng để giải phóng file.
    /// <para>Lưu ý: Không hỗ trợ Office 97-2003 Document</para>
    /// </summary>
    public class OfficeDigitalSignatureManager : IDigitalSignatureManagerBase
    {
        #region Public members - Created by Toantk on 7/5/2015
        /// <summary>
        /// Gói file
        /// </summary>
        public Package Package
        {
            get { return _package; }
        }

        /// <summary>
        /// Các chữ ký trên file
        /// </summary>
        public List<ESignature> Signatures
        {
            get
            {
                // ensure signatures are loaded from origin
                EnsureSignatures();

                // Return
                return _signatures;
            }
        }

        /// <summary>
        /// File đã được ký hay chưa?
        /// </summary>
        public bool IsSigned
        {
            get
            {
                EnsureSignatures();
                return (_signatures.Count > 0);
            }
        }

        /// <summary>
        /// Khởi tạo với dữ liệu file - đọc gói file và load chữ ký trên file
        /// </summary>
        /// <param name="fileData"></param>
        public OfficeDigitalSignatureManager(byte[] fileData)
        {
            _byteStream = new MemoryStream();
            _byteStream.Write(fileData, 0, fileData.Length);
            _byteStream.Position = 0;

            _package = Package.Open(_byteStream, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            EnsureSignatures();
        }

        /// <summary>
        /// Sign Office File (word, excel) bằng USB Token
        /// </summary>
        /// <param name="certificate">Certificate ký</param>
        /// <returns></returns>
        public void Sign(X509Certificate2 certificate)
        {
            if (certificate == null) throw new ArgumentNullException("certificate");

            //Sign on the Package
            ESignature signature = SignAllParts(_package, certificate);

            //Load lại chữ ký
            _signatures.Add(signature);
        }

        /// <summary>
        /// Sign Office File (word, excel) bằng HSM
        /// </summary>
        /// <param name="certificate">Certificate ký</param>
        /// <param name="providerHSM">Giao tiếp HSM đã đăng nhập session và load private key. Nếu PrivateKey = null sẽ lấy từ HSM qua Certificate tương ứng</param>
        /// <returns></returns>
        public void Sign(X509Certificate2 certificate, HSMServiceProvider providerHSM)
        {
            if (certificate == null) throw new ArgumentNullException("certificate");

            //Sign on the Package
            ESignature signature = SignAllParts(_package, certificate, providerHSM);

            //Load lại chữ ký
            _signatures.Add(signature);
        }

        /// <summary>
        /// Xác thực tất cả các chữ ký - chạy xác thực trên mỗi chữ ký
        /// </summary>
        /// <returns></returns>
        public VerifyResult VerifySignatures()
        {
            if (_package == null) throw new ArgumentNullException("_package");
            // Create the DigitalSignature Manager
            PackageDigitalSignatureManager dsm = new PackageDigitalSignatureManager(_package);
            return dsm.VerifySignatures(false);
        }

        /// <summary>
        /// Xác thực file có được ký bởi certificate
        /// </summary>
        /// <param name="certificate"></param>
        /// <returns></returns>
        public VerifyResult VerifySignatures(X509Certificate2 certificate)
        {
            VerifyResult result = VerifyResult.NotSigned;

            if (_package == null) throw new ArgumentNullException("_package");
            // Create the DigitalSignature Manager
            PackageDigitalSignatureManager dsm = new PackageDigitalSignatureManager(_package);
            foreach (PackageDigitalSignature signature in dsm.Signatures)
            {
                result = signature.Verify(certificate);
                if (result == VerifyResult.Success)
                    return result;
            }

            return result;
        }

        /// <summary>
        /// Lấy chuỗi Hash value của file
        /// </summary>
        /// <returns></returns>
        public byte[] GetHashValue()
        {
            if (_package == null) throw new ArgumentNullException("_package");
            List<Uri> PartstobeSigned = new List<Uri>();
            List<PackageRelationshipSelector> SignableReleationships = new List<PackageRelationshipSelector>();

            foreach (PackageRelationship relationship in _package.GetRelationships())
            {
                // Pass the releationship of the root. This is decided based on the RT_OfficeDocument 
                CreateListOfSignableItems(relationship, PartstobeSigned, SignableReleationships);
            }
            //Order lại để giống nhau giữa các phiên bản chữ ký
            PartstobeSigned = PartstobeSigned.OrderBy(o => o.OriginalString).ToList();
            SignableReleationships = SignableReleationships.OrderBy(o => o.SourceUri.OriginalString).ThenBy(o => o.SelectionCriteria).ToList();

            // Create the DigitalSignature Manager
            PackageDigitalSignatureManager dsm = new PackageDigitalSignatureManager(_package);
            dsm.CertificateOption = CertificateEmbeddingOption.InSignaturePart;

            return dsm.GetHashValue(PartstobeSigned, SignableReleationships);
        }

        /// <summary>
        /// Xóa một chữ ký trên file
        /// </summary>
        /// <param name="signatureES"></param>
        public void RemoveSignature(ESignature signatureES)
        {
            if (signatureES == null)
                throw new ArgumentNullException("signatureES");

            // empty?
            if (!IsSigned)      // calls EnsureSignatures for us
                return;

            // find the signature
            int index = GetSignatureIndex(signatureES);
            if (index < 0)
                return;

            try
            {
                // Xóa trên file
                PackageDigitalSignatureManager dsm = new PackageDigitalSignatureManager(_package);
                dsm.RemoveSignature(dsm.Signatures[index].SignaturePart.Uri);
                // save
                _package.Close();
                _package = Package.Open(_byteStream, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            }
            finally
            {
                // ensure it is actually removed from the list
                _signatures.RemoveAt(index);
            }
        }

        /// <summary>
        /// Xóa tất cả chữ ký trên file
        /// </summary>
        public void RemoveAllSignature()
        {
            EnsureSignatures();

            try
            {
                // Xóa trên file
                PackageDigitalSignatureManager dsm = new PackageDigitalSignatureManager(_package);
                dsm.RemoveAllSignatures();
                // save
                _package.Close();
                _package = Package.Open(_byteStream, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            }
            finally
            {
                // update internal variables
                _signatures.Clear();
            }
        }

        /// <summary>
        /// Trả về dữ liệu file sau khi ký. Nếu hàm ký bắn exception thì không sử dụng hàm này.
        /// </summary>
        /// <returns></returns>
        public byte[] ToArray()
        {
            return _byteStream.ToArray();
        }

        /// <summary>
        /// Đóng file và giải phóng tài nguyên
        /// </summary>
        public void Dispose()
        {
            _signatures = null;
            if (_package != null)
                _package.Close();
            if (_byteStream != null)
                _byteStream.Close();
        }
        #endregion

        #region Private members
        private MemoryStream _byteStream;
        private Package _package;
        private List<ESignature> _signatures;

        private ESignature SignAllParts(Package package, X509Certificate certificate)
        {
            if (package == null) throw new ArgumentNullException("package");

            List<Uri> PartstobeSigned = new List<Uri>();
            List<PackageRelationshipSelector> SignableReleationships = new List<PackageRelationshipSelector>();
            PackageDigitalSignature signature = null;

            foreach (PackageRelationship relationship in package.GetRelationships())
            {
                // Pass the releationship of the root. This is decided based on the RT_OfficeDocument 
                CreateListOfSignableItems(relationship, PartstobeSigned, SignableReleationships);
            }

            // Create the DigitalSignature Manager
            PackageDigitalSignatureManager dsm = new PackageDigitalSignatureManager(package);
            dsm.CertificateOption = CertificateEmbeddingOption.InSignaturePart;

            signature = dsm.Sign(PartstobeSigned, certificate, SignableReleationships);

            // return
            ESignature sig = new ESignature((X509Certificate2)signature.Signer, signature.SigningTime, signature.Verify());
            return sig;
        }

        private ESignature SignAllParts(Package package, X509Certificate certificate, HSMServiceProvider providerHSM)
        {
            if (package == null) throw new ArgumentNullException("package");

            List<Uri> PartstobeSigned = new List<Uri>();
            List<PackageRelationshipSelector> SignableReleationships = new List<PackageRelationshipSelector>();
            PackageDigitalSignature signature = null;

            foreach (PackageRelationship relationship in package.GetRelationships())
            {
                // Pass the releationship of the root. This is decided based on the RT_OfficeDocument 
                CreateListOfSignableItems(relationship, PartstobeSigned, SignableReleationships);
            }

            // Create the DigitalSignature Manager
            PackageDigitalSignatureManager dsm = new PackageDigitalSignatureManager(package);
            dsm.CertificateOption = CertificateEmbeddingOption.InSignaturePart;

            signature = dsm.Sign(PartstobeSigned, certificate, SignableReleationships, providerHSM);

            // return
            ESignature sig = new ESignature((X509Certificate2)signature.Signer, signature.SigningTime, signature.Verify());
            return sig;
        }

        private void CreateListOfSignableItems(PackageRelationship relationship, List<Uri> PartstobeSigned, List<PackageRelationshipSelector> SignableReleationships)
        {
            // except for Signature releationships and properties releationships.
            if (relationship.RelationshipType != PackageDigitalSignatureManager.OriginToSignatureRelationshipType
                && relationship.RelationshipType != PackageDigitalSignatureManager.SignatureOriginRelationshipType
                && relationship.RelationshipType != PackageDigitalSignatureManager.CorePropertiesRelationshipType
                && relationship.RelationshipType != PackageDigitalSignatureManager.ExtendedPropertiesRelationshipType)
            {
                // This function adds the releation to SignableReleationships. And then it gets the part based on the releationship.
                // Parts URI gets added to the PartstobeSigned list.
                PackageRelationshipSelector selector = new PackageRelationshipSelector(relationship.SourceUri, PackageRelationshipSelectorType.Id, relationship.Id);
                SignableReleationships.Add(selector);
                if (relationship.TargetMode == TargetMode.Internal)
                {
                    PackagePart part = relationship.Package.GetPart(PackUriHelper.ResolvePartUri(relationship.SourceUri, relationship.TargetUri));
                    if (PartstobeSigned.Contains(part.Uri) == false)
                    {
                        PartstobeSigned.Add(part.Uri);
                        // GetRelationships Function: Returns a Collection Of all the releationships that are owned by the part
                        // except for Signature releationships and properties releationships.
                        foreach (PackageRelationship childRelationship in part.GetRelationships())
                        {
                            if (childRelationship.RelationshipType != PackageDigitalSignatureManager.OriginToSignatureRelationshipType
                                && childRelationship.RelationshipType != PackageDigitalSignatureManager.SignatureOriginRelationshipType
                                && relationship.RelationshipType != PackageDigitalSignatureManager.CorePropertiesRelationshipType
                                && relationship.RelationshipType != PackageDigitalSignatureManager.ExtendedPropertiesRelationshipType)
                                CreateListOfSignableItems(childRelationship, PartstobeSigned, SignableReleationships);
                        }
                    }
                }
            }
        }

        //Lookup the index of the signature object in the _signatures array by the name of the part
        private int GetSignatureIndex(ESignature signature)
        {
            EnsureSignatures();
            for (int i = 0; i < _signatures.Count; i++)
            {
                if (_signatures[i].Signer.SerialNumber == signature.Signer.SerialNumber && _signatures[i].SigningTime == signature.SigningTime
                     && _signatures[i].Verify == signature.Verify)
                    return i;
            }
            return -1;      // not found
        }

        // load signatures from container
        private void EnsureSignatures()
        {
            if (_signatures == null)
            {
                _signatures = new List<ESignature>();

                if (_package == null) throw new ArgumentNullException("_package");
                // Create the DigitalSignature Manager
                PackageDigitalSignatureManager dsm = new PackageDigitalSignatureManager(_package);
                foreach (PackageDigitalSignature signature in dsm.Signatures)
                {
                    ESignature sig = new ESignature((X509Certificate2)signature.Signer, signature.SigningTime, signature.Verify());
                    _signatures.Add(sig);
                }
            }
        }
        #endregion

        //#region Static members
        ///// <summary>
        ///// Sign Office File (word, excel) bằng USB Token
        ///// </summary>
        ///// <param name="sourcePath">Đường dẫn file để ký</param>
        ///// /// <param name="destinationPath">Đường dẫn file sau khi ký. Nếu destinationPath = sourcePath: ghi đè file gốc</param>
        ///// <param name="cert">Certificate ký</param>
        ///// <returns></returns>
        //public void SignOfficeFile(string sourcePath, string destinationPath, X509Certificate2 cert)
        //{
        //    //Tạo file tạm
        //    string tempPath = Path.GetTempFileName();
        //    //Copy vào file tạm để xử lý
        //    File.Copy(sourcePath, tempPath, true);

        //    try
        //    {
        //        // Open the Package and sign trên file tạm
        //        using (Package package = Package.Open(tempPath))
        //        {
        //            SignAllParts(package, cert);
        //        }
        //        //Copy vào file đích
        //        File.Copy(tempPath, destinationPath, true);
        //    }
        //    finally
        //    {
        //        //Xóa file tạm
        //        File.Delete(tempPath);
        //    }
        //}

        ///// <summary>
        ///// Sign Office File (word, excel) bằng HSM
        ///// </summary>
        ///// <param name="sourcePath">Đường dẫn file để ký</param>
        ///// <param name="destinationPath">Đường dẫn file sau khi ký. Nếu destinationPath = sourcePath: ghi đè file gốc</param>
        ///// <param name="cert">Certificate ký</param>
        ///// <param name="providerHSM">Giao tiếp HSM đã đăng nhập session và load private key. Nếu PrivateKey = null sẽ lấy từ HSM qua Certificate tương ứng</param>
        ///// <returns></returns>
        //public void SignOfficeFile(string sourcePath, string destinationPath, X509Certificate2 cert, HSMServiceProvider providerHSM)
        //{
        //    //Tạo file tạm
        //    string tempPath = Path.GetTempFileName();
        //    //Copy vào file tạm để xử lý
        //    File.Copy(sourcePath, tempPath, true);

        //    try
        //    {
        //        // Open the Package and sign trên file tạm
        //        using (Package package = Package.Open(tempPath))
        //        {
        //            SignAllParts(package, cert, providerHSM);
        //        }
        //        //Copy vào file đích
        //        File.Copy(tempPath, destinationPath, true);
        //    }
        //    finally
        //    {
        //        //Xóa file tạm
        //        File.Delete(tempPath);
        //    }
        //}

        ///// <summary>
        ///// Xác thực tất cả các chữ ký trên file
        ///// </summary>
        ///// <param name="filePath"></param>
        ///// <returns></returns>
        //public VerifyResult VerifySignatures(string filePath)
        //{
        //    using (Package package = Package.Open(filePath))
        //    {
        //        if (package == null) throw new ArgumentNullException("package");
        //        // Create the DigitalSignature Manager
        //        PackageDigitalSignatureManager dsm = new PackageDigitalSignatureManager(package);
        //        return dsm.VerifySignatures(false);
        //    }
        //}

        ///// <summary>
        ///// Xác thực file có được ký bởi certificate
        ///// </summary>
        ///// <param name="filePath"></param>
        ///// <param name="certificate"></param>
        ///// <returns></returns>
        //public VerifyResult VerifySignatures(string filePath, X509Certificate2 certificate)
        //{
        //    VerifyResult result = VerifyResult.NotSigned;

        //    using (Package package = Package.Open(filePath))
        //    {
        //        if (package == null) throw new ArgumentNullException("package");
        //        // Create the DigitalSignature Manager
        //        PackageDigitalSignatureManager dsm = new PackageDigitalSignatureManager(package);
        //        foreach (PackageDigitalSignature signature in dsm.Signatures)
        //        {
        //            result = signature.Verify(certificate);
        //            if (result == VerifyResult.Success)
        //                return result;
        //        }
        //    }

        //    return result;
        //}

        ///// <summary>
        ///// Xác thực từng chữ ký và trả về danh sách các chữ ký trên file
        ///// </summary>
        ///// <param name="filePath"></param>
        ///// <returns></returns>
        //public List<ESignature> VerifyAndGetAllSignatures(string filePath)
        //{
        //    List<ESignature> lstSig = new List<ESignature>();
        //    using (Package package = Package.Open(filePath))
        //    {
        //        if (package == null) throw new ArgumentNullException("package");
        //        // Create the DigitalSignature Manager
        //        PackageDigitalSignatureManager dsm = new PackageDigitalSignatureManager(package);
        //        foreach (PackageDigitalSignature signature in dsm.Signatures)
        //        {
        //            ESignature sig = new ESignature((X509Certificate2)signature.Signer, signature.SigningTime, null, signature.Verify());
        //            lstSig.Add(sig);
        //        }
        //    }
        //    return lstSig;
        //}

        /////// <summary>
        /////// Xác thực theo certificate cụ thể
        /////// </summary>
        /////// <param name="filePath"></param>
        /////// <param name="certificate"></param>
        /////// <returns></returns>
        ////public VerifyResult VerifyOneSignatures(string filePath, X509Certificate2 certificate)
        ////{
        ////    return null; certificate.Verify();
        ////}
        //#endregion
    }
}

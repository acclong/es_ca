using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Security;
using esDigitalSignature.OfficePackage;
using System.Security.Cryptography.Xml;
using System.Security.Permissions;
using SignedXml = esDigitalSignature.OfficePackage.Cryptography.Xml.SignedXml;
using Signature = esDigitalSignature.OfficePackage.Cryptography.Xml.Signature;
using DataObject = esDigitalSignature.OfficePackage.Cryptography.Xml.DataObject;
using Reference = esDigitalSignature.OfficePackage.Cryptography.Xml.Reference;
using TransformChain = esDigitalSignature.OfficePackage.Cryptography.Xml.TransformChain;
using KeyInfo = esDigitalSignature.OfficePackage.Cryptography.Xml.KeyInfo;
using System.Collections;
using esDigitalSignature.Xml;

namespace esDigitalSignature
{
    /// <summary>
    /// Lớp quản lý ký và chữ ký số trên file XML. Cần dispose khi không sử dụng để giải phóng file.
    /// </summary>
    public class XmlDigitalSignatureManager : IDigitalSignatureManagerBase
    {
        #region Public members
        /// <summary>
        /// Gói file
        /// </summary>
        private XmlDocument Doc
        {
            get { return _doc; }
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
        public XmlDigitalSignatureManager(byte[] fileData)
        {
            _byteStream = new MemoryStream(fileData);

            _doc = new XmlDocument();
            _doc.PreserveWhitespace = true;
            _doc.Load(_byteStream);
            EnsureSignatures();
        }

        /// <summary>
        /// Sign XML File bằng USB Token
        /// </summary>
        /// <param name="certificate">Certificate ký</param>
        /// <returns></returns>
        public void Sign(X509Certificate2 certificate)
        {
            if (certificate == null) throw new ArgumentNullException("certificate");

            // Sign the XML document.
            XmlDocument DoctobeSigned = CreateSignableDoc(_doc);
            XmlElement signature = XmlSignatureProcessor.Sign(DoctobeSigned, certificate, XTable.Get(XTable.ID.OpcSignatureAttrValue));

            // Append the element to the XML document and save
            _doc.DocumentElement.AppendChild(_doc.ImportNode(signature, true));
            //Toantk 13/8/2015: sửa Encode UTF-8 sang UTF-8 without BOM
            //_doc.Save(_filePath);
            _byteStream = new MemoryStream();
            using (var writer = new XmlTextWriter(_byteStream, new UTF8Encoding(false)))
            {
                _doc.Save(writer);
            }

            //Load lại chữ ký
            _signatures = null;
            EnsureSignatures();
        }

        public void Sign(X509Certificate2 certificate, int totalSign = 1, int position = 1)
        {
            this.Sign(certificate);
        }

        /// <summary>
        /// Sign XML File bằng HSM
        /// </summary>
        /// <param name="certificate">Certificate ký</param>
        /// <param name="providerHSM">Giao tiếp HSM đã đăng nhập session và load private key. Nếu PrivateKey = null sẽ lấy từ HSM qua Certificate tương ứng</param>
        /// <returns></returns>
        public void Sign(X509Certificate2 certificate, HSMServiceProvider providerHSM)
        {
            if (certificate == null) throw new ArgumentNullException("certificate");

            // Sign the XML document.
            XmlDocument DoctobeSigned = CreateSignableDoc(_doc);
            XmlElement signature = XmlSignatureProcessor.Sign(DoctobeSigned, certificate, XTable.Get(XTable.ID.OpcSignatureAttrValue), providerHSM);

            // Append the element to the XML document and save
            _doc.DocumentElement.AppendChild(_doc.ImportNode(signature, true));
            //Toantk 13/8/2015: sửa Encode UTF-8 sang UTF-8 without BOM
            //_doc.Save(_filePath);
            _byteStream = new MemoryStream();
            using (var writer = new XmlTextWriter(_byteStream, new UTF8Encoding(false)))
            {
                _doc.Save(writer);
            }

            //Load lại chữ ký
            _signatures = null;
            EnsureSignatures();
        }

        /// <summary>
        /// Sign XML File bằng HSM
        /// </summary>
        /// <param name="certificate">Certificate ký</param>
        /// <param name="providerHSM">Giao tiếp HSM đã đăng nhập session và load private key. Nếu PrivateKey = null sẽ lấy từ HSM qua Certificate tương ứng</param>
        /// <returns></returns>
        public void Sign(X509Certificate2 certificate, HSMServiceProvider providerHSM, int totalSign = 1, int position = 1)
        {
            if (certificate == null) throw new ArgumentNullException("certificate");

            // Sign the XML document.
            XmlDocument DoctobeSigned = CreateSignableDoc(_doc);
            XmlElement signature = XmlSignatureProcessor.Sign(DoctobeSigned, certificate, XTable.Get(XTable.ID.OpcSignatureAttrValue), providerHSM);

            // Append the element to the XML document and save
            _doc.DocumentElement.AppendChild(_doc.ImportNode(signature, true));
            //Toantk 13/8/2015: sửa Encode UTF-8 sang UTF-8 without BOM
            //_doc.Save(_filePath);
            _byteStream = new MemoryStream();
            using (var writer = new XmlTextWriter(_byteStream, new UTF8Encoding(false)))
            {
                _doc.Save(writer);
            }

            //Load lại chữ ký
            _signatures = null;
            EnsureSignatures();
        }

        /// <summary>
        /// Xác thực tất cả các chữ ký trên file
        /// </summary>
        /// <returns></returns>
        public VerifyResult VerifySignatures()
        {
            //Lấy phần nội dung ko chứa chữ ký
            if (_doc == null) throw new ArgumentNullException("_doc");
            XmlDocument DoctobeSigned = CreateSignableDoc(_doc);

            // Find the "Signature" node and create a new XmlNodeList object.
            XmlNodeList nodeList = _doc.GetElementsByTagName("Signature");

            //No signature was found.
            if (nodeList.Count <= 0)
                return VerifyResult.NotSigned;

            //Verify từng chữ ký
            foreach (XmlNode node in nodeList)
            {
                XmlSignatureProcessor p = new XmlSignatureProcessor(DoctobeSigned, (XmlElement)node);
                VerifyResult r = p.Verify();
                if (r != VerifyResult.Success)
                    return r;
            }

            return VerifyResult.Success;
        }

        /// <summary>
        /// Xác thực file có được ký bởi certificate
        /// </summary>
        /// <param name="certificate"></param>
        /// <returns></returns>
        public VerifyResult VerifySignatures(X509Certificate2 certificate)
        {
            VerifyResult result = VerifyResult.NotSigned;

            //Lấy phần nội dung ko chứa chữ ký
            if (_doc == null) throw new ArgumentNullException("_doc");
            XmlDocument DoctobeSigned = CreateSignableDoc(_doc);

            // Find the "Signature" node and create a new XmlNodeList object.
            XmlNodeList nodeList = _doc.GetElementsByTagName("Signature");

            //Verify từng chữ ký
            foreach (XmlNode node in nodeList)
            {
                XmlSignatureProcessor p = new XmlSignatureProcessor(DoctobeSigned, (XmlElement)node);
                result = p.Verify(certificate);
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
            //Lấy phần nội dung ko chứa chữ ký
            XmlDocument DoctobeSigned = CreateSignableDoc(_doc);
            return XmlSignatureProcessor.GetHashValue(DoctobeSigned);
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
                // Find the "Signature" node and create a new XmlNodeList object.
                XmlNodeList nodeList = _doc.GetElementsByTagName("Signature");
                //Remove chữ ký
                _doc.DocumentElement.RemoveChild(nodeList[index]);
                //Save
                _byteStream = new MemoryStream();
                using (var writer = new XmlTextWriter(_byteStream, new UTF8Encoding(false)))
                {
                    _doc.Save(writer);
                }

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
                // Find the "Signature" node and create a new XmlNodeList object.
                XmlNodeList nodeList = _doc.GetElementsByTagName("Signature");
                //Remove từng chữ ký
                while (nodeList.Count != 0)
                {
                    _doc.DocumentElement.RemoveChild(nodeList[0]);
                }
                //Save
                _byteStream = new MemoryStream();
                using (var writer = new XmlTextWriter(_byteStream, new UTF8Encoding(false)))
                {
                    _doc.Save(writer);
                }
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
            if (_doc != null)
                _doc = null;
            if (_byteStream != null)
                _byteStream.Close();
        }
        #endregion

        #region Private members
        private MemoryStream _byteStream;
        private XmlDocument _doc;
        private List<ESignature> _signatures;

        //Lấy phần nội dung ko chứa chữ ký
        private XmlDocument CreateSignableDoc(XmlDocument doc)
        {
            //Tạo xmlDoc để xử lý riêng vì các biến XML đều truyền dạng tham chiếu
            XmlDocument xmlDoc = new XmlDocument();
            foreach (XmlNode node in doc.ChildNodes)
            {
                xmlDoc.AppendChild(xmlDoc.ImportNode(node, true));
            }

            // Find the "Signature" node and create a new XmlNodeList object.
            XmlNodeList nodeList = xmlDoc.GetElementsByTagName("Signature");

            //Remove từng chữ ký
            while (nodeList.Count != 0)
            {
                xmlDoc.DocumentElement.RemoveChild(nodeList[0]);
            }

            return xmlDoc;
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

                if (_doc == null) throw new ArgumentNullException("_doc");
                //Lấy phần nội dung ko chứa chữ ký
                XmlDocument DoctobeSigned = CreateSignableDoc(_doc);
                // Find the "Signature" node and create a new XmlNodeList object.
                XmlNodeList nodeList = _doc.GetElementsByTagName("Signature");
                foreach (XmlNode node in nodeList)
                {
                    XmlSignatureProcessor p = new XmlSignatureProcessor(DoctobeSigned, (XmlElement)node);
                    ESignature sig = new ESignature(p.Signer, p.SigningTime, p.Verify());
                    _signatures.Add(sig);
                }
            }
        }
        #endregion

        //#region Static members
        ///// <summary>
        ///// Sign XML File bằng USB Token
        ///// </summary>
        ///// <param name="sourcePath">Đường dẫn file để ký</param>
        ///// /// <param name="destinationPath">Đường dẫn file sau khi ký. Nếu destinationPath = sourcePath: ghi đè file gốc</param>
        ///// <param name="cert">Certificate ký</param>
        ///// <returns></returns>
        //public void SignXmlFile(string sourcePath, string destinationPath, X509Certificate2 cert)
        //{
        //    //Tạo file tạm
        //    string tempPath = Path.GetTempFileName();
        //    //Copy vào file tạm để xử lý
        //    File.Copy(sourcePath, tempPath, true);

        //    try
        //    {
        //        // Create a new XML document and load the XML file
        //        XmlDocument xmlDoc = new XmlDocument();
        //        xmlDoc.PreserveWhitespace = true;
        //        xmlDoc.Load(tempPath);

        //        // Sign the XML document.
        //        XmlElement signature = XmlSignatureProcessor.Sign(xmlDoc, cert, XTable.Get(XTable.ID.OpcSignatureAttrValue));

        //        // Append the element to the XML document and save
        //        xmlDoc.DocumentElement.AppendChild(xmlDoc.ImportNode(signature, true));
        //        xmlDoc.Save(tempPath);

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
        ///// Sign XML File bằng HSM
        ///// </summary>
        ///// <param name="sourcePath">Đường dẫn file để ký</param>
        ///// <param name="destinationPath">Đường dẫn file sau khi ký. Nếu destinationPath = sourcePath: ghi đè file gốc</param>
        ///// <param name="cert">Certificate ký</param>
        ///// <param name="providerHSM">Giao tiếp HSM đã đăng nhập session và load private key. Nếu PrivateKey = null sẽ lấy từ HSM qua Certificate tương ứng</param>
        ///// <returns></returns>
        //public void SignXmlFile(string sourcePath, string destinationPath, X509Certificate2 cert, HSMServiceProvider providerHSM)
        //{
        //    //Tạo file tạm
        //    string tempPath = Path.GetTempFileName();
        //    //Copy vào file tạm để xử lý
        //    File.Copy(sourcePath, tempPath, true);

        //    try
        //    {
        //        // Create a new XML document and load the XML file
        //        XmlDocument xmlDoc = new XmlDocument();
        //        xmlDoc.PreserveWhitespace = true;
        //        xmlDoc.Load(tempPath);

        //        // Sign the XML document.
        //        XmlElement signature = XmlSignatureProcessor.Sign(xmlDoc, cert, XTable.Get(XTable.ID.OpcSignatureAttrValue), providerHSM);

        //        // Append the element to the XML document and save
        //        xmlDoc.DocumentElement.AppendChild(xmlDoc.ImportNode(signature, true));
        //        xmlDoc.Save(tempPath);

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
        //    // Create a new XML document and load the XML file
        //    XmlDocument xmlDoc = new XmlDocument();
        //    xmlDoc.PreserveWhitespace = true;
        //    xmlDoc.Load(filePath);

        //    // Find the "Signature" node and create a new XmlNodeList object.
        //    XmlNodeList nodeList = xmlDoc.GetElementsByTagName("Signature");

        //    //No signature was found.
        //    if (nodeList.Count <= 0)
        //        return VerifyResult.NotSigned;

        //    //Verify từng chữ ký
        //    foreach (XmlNode node in nodeList)
        //    {
        //        XmlSignatureProcessor p = new XmlSignatureProcessor(xmlDoc, (XmlElement)node);
        //        VerifyResult r = p.Verify();
        //        if (r != VerifyResult.Success)
        //            return r;
        //    }

        //    return VerifyResult.Success;
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

        //    // Create a new XML document and load the XML file
        //    XmlDocument xmlDoc = new XmlDocument();
        //    xmlDoc.PreserveWhitespace = true;
        //    xmlDoc.Load(filePath);

        //    // Find the "Signature" node and create a new XmlNodeList object.
        //    XmlNodeList nodeList = xmlDoc.GetElementsByTagName("Signature");

        //    //Verify từng chữ ký
        //    foreach (XmlNode node in nodeList)
        //    {
        //        XmlSignatureProcessor p = new XmlSignatureProcessor(xmlDoc, (XmlElement)node);
        //        result = p.Verify(certificate);
        //        if (result == VerifyResult.Success)
        //            return result;
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
        //    // Create a new XML document and load the XML file
        //    XmlDocument xmlDoc = new XmlDocument();
        //    xmlDoc.PreserveWhitespace = true;
        //    xmlDoc.Load(filePath);

        //    List<ESignature> lstSig = new List<ESignature>();

        //    // Find the "Signature" node and create a new XmlNodeList object.
        //    XmlNodeList nodeList = xmlDoc.GetElementsByTagName("Signature");
        //    foreach (XmlNode node in nodeList)
        //    {
        //        XmlSignatureProcessor p = new XmlSignatureProcessor(xmlDoc, (XmlElement)node);
        //        ESignature sig = new ESignature(p.Signer, p.SigningTime, null, p.Verify());
        //        lstSig.Add(sig);
        //    }

        //    return lstSig;
        //}
        //#endregion
    }
}

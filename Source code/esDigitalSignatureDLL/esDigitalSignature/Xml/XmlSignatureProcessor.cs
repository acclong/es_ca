using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Xml;
using System.Security;
using System.Security.Permissions;
using System.Security.Cryptography.Xml;
using System.Collections;
using esDigitalSignature.OfficePackage;
using SignedXml = esDigitalSignature.OfficePackage.Cryptography.Xml.SignedXml;
using DataObject = esDigitalSignature.OfficePackage.Cryptography.Xml.DataObject;
using Reference = esDigitalSignature.OfficePackage.Cryptography.Xml.Reference;
using KeyInfo = esDigitalSignature.OfficePackage.Cryptography.Xml.KeyInfo;
using XTable = esDigitalSignature.OfficePackage.XTable;
using XmlDsigEnvelopedSignatureTransform = esDigitalSignature.OfficePackage.Cryptography.Xml.XmlDsigEnvelopedSignatureTransform;
using XmlDsigC14NTransform = esDigitalSignature.OfficePackage.Cryptography.Xml.XmlDsigC14NTransform;

namespace esDigitalSignature.Xml
{
    public class XmlSignatureProcessor
    {
        /// <summary>
        /// Constructor - called from public constructor as well as static Sign() method
        /// </summary>
        /// <param name="doc">the document being protected by this signature</param>
        internal XmlSignatureProcessor(XmlDocument doc)
        {
            Invariant.Assert(doc != null);

            _signedXml = new SignedXml(doc);
        }

        /// <summary>
        /// Constructor - called from public constructor as well as static Sign() method
        /// </summary>
        /// <param name="doc">the document being protected by this signature</param>
        /// <param name="signature">a signature node of the document</param>
        internal XmlSignatureProcessor(XmlDocument doc, XmlElement signature)
        {
            Invariant.Assert(doc != null);
            Invariant.Assert(signature != null);

            _signedXml = new SignedXml(doc);
            _signature = signature;
            // Load the <signature> node.  
            _signedXml.LoadXml(_signature);
        }

        /// <summary>
        /// Factory method that creates a new PackageDigitalSignature
        /// </summary>
        internal static XmlElement Sign(XmlDocument doc, X509Certificate2 signer, String signatureId)
        {
            // create
            XmlSignatureProcessor p = new XmlSignatureProcessor(doc);

            // and sign
            return p.Sign(signer, signatureId);
        }

        /// <summary>
        /// verify the signature
        /// </summary>
        /// <param name="doc">the document has the signatures</param>
        internal VerifyResult Verify()
        {
            if (Signer == null)
                return VerifyResult.CertificateRequired;

            return Verify(Signer);
        }

        /// <summary>
        /// Verify the given signature
        /// </summary>
        /// <param name="signer">certificate to use (ignores any embedded cert)</param>
        /// <returns>true if the data stream has not been altered since it was signed</returns>
        /// <SecurityNote>
        ///     Critical - 1) Elevate to unrestricted to work around a feature in the .NET XML libraries.
        ///              - 2) We are calling GenerateDigestValueNode which is SecurityCritical due to the Transform parameter.
        ///     TreatAsSafe - 1) This is to work around a feature in the Xml layer.  The assert makes it possible for the XML
        ///                      layer to perform a transform on the data "under the covers".
        ///                      (http://bugcheck/default.asp?URL=/bugs/SQLBUDefectTracking/392346.asp)
        ///                   2) The one parameter of concern (Transform) is trusted.  The reasoning is that we get the
        ///                      instance from trusted sources.  The Transform is obtained from a trusted method
        ///                      (DigitalSignatureProcessor.StringToTranform) that only creates built-in .NET Transform
        ///                      instances which are safe XML Transforms.
        /// </SecurityNote>
        [SecurityCritical, SecurityTreatAsSafe]
        internal VerifyResult Verify(X509Certificate2 signer)
        {
            Invariant.Assert(signer != null);

            bool result = false;
            (new PermissionSet(PermissionState.Unrestricted)).Assert();
            try
            {
                // verify "standard" XmlSignature portions
                result = _signedXml.CheckSignature(signer, true);
            }
            finally
            {
                PermissionSet.RevertAssert();
            }

            if (result)
                return VerifyResult.Success;
            else
                return VerifyResult.InvalidSignature;
        }

        /// <summary>
        /// Obtain the author's identity in X509 Certificate form
        /// </summary>
        /// <returns>Authors identity as a certificate in X509 form, or null if none found</returns>
        internal X509Certificate2 Signer
        {
            get
            {
                // lazy init when loading existing cert - Sign may have assigned this for us
                if (_certificate == null)
                {
                    IEnumerator keyInfoClauseEnum = _signedXml.KeyInfo.GetEnumerator(typeof(KeyInfoX509Data));
                    while (keyInfoClauseEnum.MoveNext())
                    {
                        KeyInfoX509Data x509Data = (KeyInfoX509Data)keyInfoClauseEnum.Current;
                        foreach (X509Certificate2 cert in x509Data.Certificates)
                        {
                            // just take the first one for now
                            _certificate = cert;
                            break;
                        }

                        // just need one for now
                        if (_certificate != null)
                            break;
                    }
                }

                return _certificate;    // may be null
            }
        }

        /// <summary>
        /// Time that the signature was created
        /// </summary>
        /// <returns>Time of signing if available, or DateTime.MinValue if signature was not time-stamped</returns>
        internal DateTime SigningTime
        {
            get
            {
                // lazy init
                ParsePackageDataObject();
                return _signingTime;
            }
        }

        internal String TimeFormat
        {
            get
            {
                // lazy init
                ParsePackageDataObject();
                return _signingTimeFormat;
            }
        }

        //------------------------------------------------------
        //
        //  Private Methods
        //
        //------------------------------------------------------

        /// <summary>
        /// Create a new Xml Signature
        /// </summary>
        /// <param name="signer">Identity of the author</param>
        /// <param name="signatureId">Id attribute of the new Xml Signature</param>
        private XmlElement Sign(X509Certificate2 signer, String signatureId)
        {
            // grab hash algorithm as this may change in the future
            _hashAlgorithmName = _defaultHashAlgorithm;
            _timeFormat = _defaultTimeFormat;

            // we only release this key if we obtain it
            AsymmetricAlgorithm key = null;
            bool ownKey = false;
            if (signer.HasPrivateKey)
            {
                key = signer.PrivateKey;
            }
            else
            {
                ownKey = true;
                key = GetPrivateKeyForSigning(signer);
            }

            try
            {
                // Create a SignedXml object.
                _signedXml.SigningKey = key;
                _signedXml.Signature.Id = signatureId;
                _signedXml.KeyInfo = GenerateKeyInfo(signer.PublicKey.Key, signer);

                // Package object tag
                // convert from string to class and ensure we dispose
                using (HashAlgorithm hashAlgorithm = GetHashAlgorithm(_hashAlgorithmName))
                {
                    // inform caller if hash algorithm is unknown
                    if (hashAlgorithm == null)
                        throw new InvalidOperationException("UnsupportedHashAlgorithm");

                    _signedXml.AddObject(GenerateObjectTag(hashAlgorithm, _timeFormat, signatureId));
                }

                // Create a reference for content to be signed.
                Reference reference = new Reference();
                reference.Uri = "";
                // Add an enveloped transformation to the reference.
                XmlDsigEnvelopedSignatureTransform env = new XmlDsigEnvelopedSignatureTransform();
                reference.AddTransform(env);
                // Add the reference to the SignedXml object.
                _signedXml.AddReference(reference);

                //Edited by Toantk on 14/5/2015
                // add reference from SignedInfo to Package object tag
                Reference objectReference = new Reference(XTable.Get(XTable.ID.OpcLinkAttrValue));
                objectReference.Type = XTable.Get(XTable.ID.W3CSignatureNamespaceRoot) + "Object";
                objectReference.DigestMethod = _hashAlgorithmName;
                _signedXml.AddReference(objectReference);

                // Compute the signature
                (new PermissionSet(PermissionState.Unrestricted)).Assert();
                try
                {
                    _signedXml.ComputeSignature();
                }
                finally
                {
                    PermissionSet.RevertAssert();
                }
            }
            finally
            {
                if (key != null && ownKey)
                    ((IDisposable)key).Dispose();
            }

            // Get the XML representation of the signature and save 
            // it to an XmlElement object.
            _signature = _signedXml.GetXml();
            return _signature;
        }

        /// <summary>
        /// lookup the private key using the given identity
        /// </summary>
        /// <param name="signer">X509Cert</param>
        /// <returns>IDisposable asymmetric algorithm that serves as a proxy to the private key.  Caller must dispose
        /// of properly.</returns>
        private static AsymmetricAlgorithm GetPrivateKeyForSigning(X509Certificate2 signer)
        {
            // if the certificate does not actually contain the key, we need to look it up via ThumbPrint
            Invariant.Assert(!signer.HasPrivateKey);

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
                        throw new CryptographicException("DigSigDuplicateCertificate");

                    signer = collection[0];
                }
                else
                    throw new CryptographicException("DigSigCannotLocateCertificate");
            }
            finally
            {
                store.Close();
            }

            // get the corresponding AsymmetricAlgorithm
            return signer.PrivateKey;
        }

        private KeyInfo GenerateKeyInfo(AsymmetricAlgorithm key, X509Certificate2 signer)
        {
            // KeyInfo section
            KeyInfo keyInfo = new KeyInfo();
            KeyInfoName keyInfoName = new KeyInfoName();
            keyInfoName.Value = signer.Subject;
            keyInfo.AddClause(keyInfoName);               // human readable Principal name

            //TOANTK: check chuẩn RSA hay DSA của PRIVATE KEY và lưu vào KeyInfo
            // Include the public key information (if we are familiar with the algorithm type)
            if (key is RSA)
                keyInfo.AddClause(new RSAKeyValue((RSA)key));    // RSA key parameters
            else
            {
                if (key is DSA)
                    keyInfo.AddClause(new DSAKeyValue((DSA)key));    // DSA
                else
                {
                    //throw new ArgumentException("CertificateKeyTypeNotSupported", "signer");
                }
            }

            // the actual X509 cert
            keyInfo.AddClause(new KeyInfoX509Data(signer));

            return keyInfo;
        }

        /// <summary>
        /// Convert algorithm name to object
        /// </summary>
        /// <param name="hashAlgorithmName">fully specified name</param>
        /// <returns>HashAlgorithm object or null if it does not map to a supported hash type</returns>
        /// <remarks>Caller is responsible for calling Dispose() on returned object</remarks>
        private static HashAlgorithm GetHashAlgorithm(String hashAlgorithmName)
        {
            Object o = CryptoConfig.CreateFromName(hashAlgorithmName);
            HashAlgorithm algorithm = o as HashAlgorithm;

            // In the case that we get a valid crypto object that is not a hash algorithm
            // we should attempt to dispose it if it offers IDisposable.
            if (algorithm == null && o != null)
            {
                IDisposable disposable = o as IDisposable;
                if (disposable != null)
                    disposable.Dispose();
            }

            return algorithm;
        }

        private DataObject GenerateObjectTag(
                HashAlgorithm hashAlgorithm,
                string timeFormat,
                String signatureId)
        {
            //TOANTK: thời điểm ký --> ghép vào data để băm
            XmlDocument xDoc = new XmlDocument();
            xDoc.AppendChild(xDoc.CreateNode(XmlNodeType.Element, "root", "namespace")); // dummy root
            xDoc.DocumentElement.AppendChild(XmlSignatureProperties.AssembleSignatureProperties(xDoc, DateTime.Now, timeFormat, signatureId));

            DataObject dataObject = new DataObject();
            dataObject.Data = xDoc.DocumentElement.ChildNodes;
            dataObject.Id = XTable.Get(XTable.ID.OpcAttrValue);

            return dataObject;
        }

        /// <summary>
        /// get all signatures and verify on each signature
        /// </summary>
        /// <param name="doc">the document has the signatures</param>
        private List<ESignature> GetAllSignatures(XmlDocument doc)
        {
            List<ESignature> lstSig = new List<ESignature>();

            // Check arguments. 
            if (doc == null)
                throw new ArgumentException("NothingToVerify");

            // Find the "Signature" node and create a new 
            // XmlNodeList object.
            XmlNodeList nodeList = doc.GetElementsByTagName("Signature");

            //No signature was found.
            if (nodeList.Count <= 0)
                return lstSig;

            foreach (XmlNode node in nodeList)
            {
                VerifyResult result = VerifyResult.Success;

                // Create a new SignedXml object and pass it the XML document class.
                _signedXml = new SignedXml(doc);

                // Load the first <signature> node.  
                _signedXml.LoadXml((XmlElement)node);

                //Get info
                ParsePackageDataObject();

                // Check the signature and return the result. 
                if (!_signedXml.CheckSignature())
                    if (_signedXml.SigningKey == null)
                        result = VerifyResult.CertificateRequired;
                    else
                        result = VerifyResult.InvalidSignature;

                ESignature sig = new ESignature(Signer, _signingTime, result);
                lstSig.Add(sig);
            }

            return lstSig;
        }

        /// <summary>
        /// Full parse of the Package-specific Object tag
        /// </summary>
        /// <remarks>Side effect of updating _signingTime, _signingTimeFormat, 
        /// _partManifest, _partEntryManifest and _relationshipManifest</remarks>
        /// <exception cref="XmlException">throws if markup does not match OPC spec</exception>
        private void ParsePackageDataObject()
        {
            if (!_dataObjectParsed)
            {
                // find the package-specific Object tag
                XmlNodeList nodeList = GetPackageDataObject().Data;

                // The legal parent is a "Package" Object tag with 1 children
                // <SignatureProperties>
                if (nodeList.Count != 1)
                    throw new XmlException("XmlSignatureParseError");

                // get a NodeReader that allows us to easily and correctly skip comments
                XmlReader reader = new XmlNodeReader(nodeList[0].ParentNode);

                // parse the <Object> tag - ensure that it is in the correct namespace
                reader.Read();  // enter the Object tag
                if (String.CompareOrdinal(reader.NamespaceURI, SignedXml.XmlDsigNamespaceUrl) != 0)
                    throw new XmlException("XmlSignatureParseError");

                string signaturePropertiesTagName = XTable.Get(XTable.ID.SignaturePropertiesTagName);
                string manifestTagName = XTable.Get(XTable.ID.ManifestTagName);
                bool signaturePropertiesTagFound = false;
                while (reader.Read() && (reader.NodeType == XmlNodeType.Element))
                {
                    if (reader.MoveToContent() == XmlNodeType.Element
                        && (String.CompareOrdinal(reader.NamespaceURI, SignedXml.XmlDsigNamespaceUrl) == 0)
                        && reader.Depth == 1)
                    {
                        if (!signaturePropertiesTagFound && String.CompareOrdinal(reader.LocalName, signaturePropertiesTagName) == 0)
                        {
                            signaturePropertiesTagFound = true;

                            // parse the <SignatureProperties> tag
                            _signingTime = XmlSignatureProperties.ParseSigningTime(
                                reader, _signedXml.Signature.Id, out _signingTimeFormat);

                            continue;
                        }
                    }

                    throw new XmlException("XmlSignatureParseError");
                }

                // these must both exist on exit
                if (!(signaturePropertiesTagFound))
                    throw new XmlException("XmlSignatureParseError");

                _dataObjectParsed = true;
            }
        }

        /// <summary>
        /// Finds and return the package-specific Object tag
        /// </summary>
        /// <returns></returns>
        private DataObject GetPackageDataObject()
        {
            // look for the Package-specific object tag
            String opcId = XTable.Get(XTable.ID.OpcAttrValue);
            DataObject returnValue = null;
            foreach (DataObject dataObject in _signedXml.Signature.ObjectList)
            {
                if (String.CompareOrdinal(dataObject.Id, opcId) == 0)
                {
                    // duplicates not allowed
                    if (returnValue != null)
                        throw new XmlException("SignatureObjectIdMustBeUnique");

                    returnValue = dataObject;
                }
            }

            // Package object tag required
            if (returnValue != null)
                return returnValue;
            else
                throw new XmlException("PackageSignatureObjectTagRequired");
        }

        #region HSM Sign and Get hash
        /// <summary>
        /// Factory method that creates a new PackageDigitalSignature
        /// </summary>
        internal static XmlElement Sign(XmlDocument doc, X509Certificate2 signer, String signatureId, HSMServiceProvider providerHSM)
        {
            // create
            XmlSignatureProcessor p = new XmlSignatureProcessor(doc);

            // and sign
            return p.Sign(signer, signatureId, providerHSM);
        }

        /// <summary>
        /// Create a new Xml Signature
        /// </summary>
        /// <param name="signer">Identity of the author</param>
        /// <param name="signatureId">Id attribute of the new Xml Signature</param>
        /// <param name="providerHSM">HSM PKCS11 Interface - require session loged in</param>
        private XmlElement Sign(X509Certificate2 signer, String signatureId, HSMServiceProvider providerHSM)
        {
            // grab hash algorithm as this may change in the future
            _hashAlgorithmName = _defaultHashAlgorithm;
            _timeFormat = _defaultTimeFormat;

            // we only release this key if we obtain it
            AsymmetricAlgorithm key = null;
            bool ownKey = false;
            if (signer.HasPrivateKey)
            {
                key = signer.PrivateKey;
            }
            else
            {
                ownKey = true;
                key = GetPrivateKeyForSigning(signer, providerHSM);
            }

            try
            {
                // Create a SignedXml object.
                _signedXml.SigningKey = key;
                _signedXml.Signature.Id = signatureId;
                _signedXml.KeyInfo = GenerateKeyInfo(key, signer);

                // Package object tag
                // convert from string to class and ensure we dispose
                using (HashAlgorithm hashAlgorithm = GetHashAlgorithm(_hashAlgorithmName))
                {
                    // inform caller if hash algorithm is unknown
                    if (hashAlgorithm == null)
                        throw new InvalidOperationException("UnsupportedHashAlgorithm");

                    _signedXml.AddObject(GenerateObjectTag(hashAlgorithm, _timeFormat, signatureId));
                }

                // Create a reference to be signed.
                Reference reference = new Reference();
                reference.Uri = "";
                // Add an enveloped transformation to the reference.
                XmlDsigEnvelopedSignatureTransform env = new XmlDsigEnvelopedSignatureTransform();
                reference.AddTransform(env);
                // Add the reference to the SignedXml object.
                _signedXml.AddReference(reference);

                //Edited by Toantk on 14/5/2015
                // add reference from SignedInfo to Package object tag
                Reference objectReference = new Reference(XTable.Get(XTable.ID.OpcLinkAttrValue));
                objectReference.Type = XTable.Get(XTable.ID.W3CSignatureNamespaceRoot) + "Object";
                objectReference.DigestMethod = _hashAlgorithmName;
                _signedXml.AddReference(objectReference);

                // Compute the signature
                (new PermissionSet(PermissionState.Unrestricted)).Assert();
                try
                {
                    _signedXml.ComputeSignature(providerHSM);
                }
                finally
                {
                    PermissionSet.RevertAssert();
                }
            }
            finally
            {
                if (key != null && ownKey)
                    ((IDisposable)key).Dispose();
            }

            // Get the XML representation of the signature and save 
            // it to an XmlElement object.
            _signature = _signedXml.GetXml();
            return _signature;
        }

        /// <summary>
        /// lookup the private key using the given identity
        /// </summary>
        /// <param name="signer">X509Cert</param>
        /// <returns>IDisposable asymmetric algorithm that serves as a proxy to the private key.  Caller must dispose
        /// of properly.</returns>
        private static AsymmetricAlgorithm GetPrivateKeyForSigning(X509Certificate2 signer, HSMServiceProvider providerHSM)
        {
            // if the certificate does not actually contain the key, we need to look it up via ThumbPrint
            Invariant.Assert(!signer.HasPrivateKey);

            //TOANTK: get the corresponding AsymmetricAlgorithm nếu chưa khởi tạo private key
            if (providerHSM.PrivateKey != null)
                return providerHSM.PrivateKey;
            else
            {
                return providerHSM.LoadPrivateKeyByCertificate(signer);
            }
        }

        /// <summary>
        /// Factory method that creates a hash value from data
        /// </summary>
        [SecurityCritical, SecurityTreatAsSafe]
        internal static byte[] GetHashValue(XmlDocument doc)
        {
            // create
            XmlSignatureProcessor p = new XmlSignatureProcessor(doc);

            // and sign
            return p.GetHashValue();
        }

        /// <summary>
        /// Create a hash value from data
        /// </summary>
        /// <param name="parts">the data being hashed</param>
        /// <param name="relationshipSelectors">possibly null collection of relationshipSelectors that represent the
        /// relationships that are to be hashed</param>
        [SecurityCritical, SecurityTreatAsSafe]
        public byte[] GetHashValue()
        {
            // grab hash algorithm as this may change in the future
            _hashAlgorithmName = _defaultHashAlgorithm;

            // Package object tag
            // convert from string to class and ensure we dispose
            using (HashAlgorithm hashAlgorithm = GetHashAlgorithm(_hashAlgorithmName))
            {
                // inform caller if hash algorithm is unknown
                if (hashAlgorithm == null)
                    throw new InvalidOperationException("UnsupportedHashAlgorithm");

                _signedXml.AddObject(GenerateContentTag(hashAlgorithm));
            }

            // Create a reference to be signed.
            Reference reference = new Reference();
            reference.Uri = "";
            // Add an enveloped transformation to the reference.
            XmlDsigEnvelopedSignatureTransform env = new XmlDsigEnvelopedSignatureTransform();
            reference.AddTransform(env);
            // Add the reference to the SignedXml object.
            _signedXml.AddReference(reference);

            (new PermissionSet(PermissionState.Unrestricted)).Assert();
            try
            {
                return _signedXml.GetHashValue();
            }
            finally
            {
                PermissionSet.RevertAssert();
            }
        }

        //Tạo Tag phần nội dung - không bao gồm chữ ký
        private DataObject GenerateContentTag(HashAlgorithm hashAlgorithm)
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.AppendChild(xDoc.CreateNode(XmlNodeType.Element, "root", "namespace")); // dummy root

            DataObject dataObject = new DataObject();
            dataObject.Data = xDoc.DocumentElement.ChildNodes;
            dataObject.Id = XTable.Get(XTable.ID.OpcAttrValue);

            return dataObject;
        }
        #endregion

        //------------------------------------------------------
        //
        //  Private Fields
        //
        //------------------------------------------------------
        private SignedXml _signedXml;         // our format friend
        private String _hashAlgorithmName;     // first hash algorithm obtained - considered to be the setting for the entire signature
        private string _timeFormat;
        private XmlElement _signature;         // parsed from part or newly created

        // OPC Object tag parsing - once parsed, all fields in this section are considered viable
        private X509Certificate2 _certificate;
        private bool _dataObjectParsed;          // true if package-specific data Object tag has been parsed
        private DateTime _signingTime;               // cached value
        private String _signingTimeFormat;         // format string

        private static readonly String _defaultHashAlgorithm = SignedXml.XmlDsigSHA1Url;
        private static readonly String _defaultTimeFormat = XmlSignatureProperties.DefaultDateTimeFormat;
    }
}

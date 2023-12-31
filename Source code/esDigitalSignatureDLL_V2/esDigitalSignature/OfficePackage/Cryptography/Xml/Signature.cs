// ==++==
// 
//   Copyright (c) Microsoft Corporation.  All rights reserved.
// 
// ==--==
// <OWNER>[....]</OWNER>
// 

//
// Signature.cs
// 
// 21 [....] 2000
// 

namespace esDigitalSignature.OfficePackage.Cryptography.Xml
{
    using System;
    using System.Collections;
    using System.Xml;
    using System.Security.Cryptography.Xml;
    using System.Security.Cryptography;

    [System.Security.Permissions.HostProtection(MayLeakOnAbort = true)]
    internal class Signature {
        private string m_id;
        private SignedInfo m_signedInfo;
        private byte[] m_signatureValue;
        private string m_signatureValueId;
        private KeyInfo m_keyInfo;
        private IList m_embeddedObjects;
        private CanonicalXmlNodeList m_referencedItems;
        private SignedXml m_signedXml = null;

        internal SignedXml SignedXml {
            get { return m_signedXml; }
            set { m_signedXml = value; }
        }

        //
        // public constructors
        //

        public Signature() {
            m_embeddedObjects = new ArrayList();
            m_referencedItems = new CanonicalXmlNodeList();
        }

        //
        // public properties
        //

        public string Id {
            get { return m_id; }
            set { m_id = value; }
        }

        public SignedInfo SignedInfo {
            get { return m_signedInfo; }
            set { 
                m_signedInfo = value;
                if (this.SignedXml != null && m_signedInfo != null)
                    m_signedInfo.SignedXml = this.SignedXml;
            }
        }

        public byte[] SignatureValue {
            get { return m_signatureValue; }
            set { m_signatureValue = value; }
        }

        public KeyInfo KeyInfo {
            get {
                if (m_keyInfo == null)
                    m_keyInfo = new KeyInfo();
                return m_keyInfo;
            }
            set { m_keyInfo = value; }
        }

        public IList ObjectList {
            get { return m_embeddedObjects; }
            set { m_embeddedObjects = value; }
        }

        internal CanonicalXmlNodeList ReferencedItems {
            get { return m_referencedItems; }
        }

        //
        // public methods
        //

        public XmlElement GetXml() {
            XmlDocument document = new XmlDocument();
            document.PreserveWhitespace = true;
            return GetXml(document);
        }

        internal XmlElement GetXml (XmlDocument document) {
            // Create the Signature
            XmlElement signatureElement = (XmlElement)document.CreateElement("Signature", SignedXml.XmlDsigNamespaceUrl);
            if (!String.IsNullOrEmpty(m_id))
                signatureElement.SetAttribute("Id", m_id);

            // Add the SignedInfo
            if (m_signedInfo == null)
                throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Xml_SignedInfoRequired"));

            signatureElement.AppendChild(m_signedInfo.GetXml(document));

            // Add the SignatureValue
            if (m_signatureValue == null)
                throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Xml_SignatureValueRequired"));

            XmlElement signatureValueElement = document.CreateElement("SignatureValue", SignedXml.XmlDsigNamespaceUrl);
            signatureValueElement.AppendChild(document.CreateTextNode(Convert.ToBase64String(m_signatureValue)));
            if (!String.IsNullOrEmpty(m_signatureValueId))
                signatureValueElement.SetAttribute("Id", m_signatureValueId);
            signatureElement.AppendChild(signatureValueElement);

            // Add the KeyInfo
            if (this.KeyInfo.Count > 0)
                signatureElement.AppendChild(this.KeyInfo.GetXml(document));

            // Add the Objects
            foreach (Object obj in m_embeddedObjects) {
                DataObject dataObj = obj as DataObject;
                if (dataObj != null) {
                    signatureElement.AppendChild(dataObj.GetXml(document));
                }
            }

            return signatureElement;
        }

        public void LoadXml(XmlElement value) {
             // Make sure we don't get passed null
            if (value == null)
                throw new ArgumentNullException("value");

            // Signature
            XmlElement signatureElement = value;
            if (!signatureElement.LocalName.Equals("Signature"))
                throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Xml_InvalidElement"), "Signature");

            // Id attribute -- optional
            m_id = Utils.GetAttribute(signatureElement, "Id", SignedXml.XmlDsigNamespaceUrl);

            XmlNamespaceManager nsm = new XmlNamespaceManager(value.OwnerDocument.NameTable);
            nsm.AddNamespace("ds", SignedXml.XmlDsigNamespaceUrl);

            // SignedInfo
            XmlElement signedInfoElement = signatureElement.SelectSingleNode("ds:SignedInfo", nsm) as XmlElement;
            if (signedInfoElement == null)
              throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Xml_InvalidElement"),"SignedInfo");

            this.SignedInfo = new SignedInfo();
            this.SignedInfo.LoadXml(signedInfoElement);

            // SignatureValue
            XmlElement signatureValueElement = signatureElement.SelectSingleNode("ds:SignatureValue", nsm) as XmlElement;
            if (signatureValueElement == null)
                throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Xml_InvalidElement"),"SignedInfo/SignatureValue");
            m_signatureValue = Convert.FromBase64String(Utils.DiscardWhiteSpaces(signatureValueElement.InnerText));
            m_signatureValueId = Utils.GetAttribute(signatureValueElement, "Id", SignedXml.XmlDsigNamespaceUrl);

            XmlNodeList keyInfoNodes = signatureElement.SelectNodes("ds:KeyInfo", nsm);
            m_keyInfo = new KeyInfo();
            if (keyInfoNodes != null) {
                foreach(XmlNode node in keyInfoNodes) {
                    XmlElement keyInfoElement = node as XmlElement;
                    if (keyInfoElement != null)
                        m_keyInfo.LoadXml(keyInfoElement);
                }
            }

            XmlNodeList objectNodes = signatureElement.SelectNodes("ds:Object", nsm);
            m_embeddedObjects.Clear();
            if (objectNodes != null) {
                foreach(XmlNode node in objectNodes) {
                    XmlElement objectElement = node as XmlElement;
                    if (objectElement != null) {
                        DataObject dataObj = new DataObject();
                        dataObj.LoadXml(objectElement);
                        m_embeddedObjects.Add(dataObj);
                    }
                }
            }

            // Select all elements that have Id attributes
            //Edited by Toantk on 15/5/2015
            //Change Xpath expression to "descendant-or-self::node()" for selecting only nodes belong to signatureElement
            XmlNodeList nodeList = signatureElement.SelectNodes("descendant-or-self::node()[@Id]", nsm);
            if (nodeList != null) {
                foreach (XmlNode node in nodeList) {
                    m_referencedItems.Add(node);
                }
            }
        }

        public void AddObject(DataObject dataObject) {
            m_embeddedObjects.Add(dataObject);
        }
    }
}


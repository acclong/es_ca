//-----------------------------------------------------------------------------
//
// <copyright file="XmlSignatureProperties.cs" company="Microsoft">
//    Copyright (C) Microsoft Corporation.  All rights reserved.
// </copyright>
//
// Description:
//  Helper for XmlDigitalSignatureProcessor.
//  Generates and consumes Metro-compliant SignatureProperties element within an
//  XmlDSig signature.
//
// History:
//  01/25/2004: BruceMac: First iteration
//  12/02/2005: BruceMac: Security Mitigations and support short form for UTC dates "Z"
//
//-----------------------------------------------------------------------------

using System;
using System.Collections;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Security.Cryptography.Xml;
using System.Security.Cryptography.X509Certificates;
using System.Security.Permissions;
using System.Xml;
using System.IO;
using System.Windows;
using System.IO.Packaging;
using System.Diagnostics;
using MS.Internal.WindowsBase;
using SignedXml = esDigitalSignature.OfficePackage.Cryptography.Xml.SignedXml;

namespace esDigitalSignature.OfficePackage
{
    /// <summary>
    /// Signature Handler implementation that follows the Feb 12, 2002 W3C DigSig Recommendation
    /// </summary>
    /// <remarks>See: http://www.w3.org/TR/2002/REC-xmldsig-core-20020212/ for details</remarks>
    static internal class XmlSignatureProperties
    {
        //-----------------------------------------------------------------------------
        //
        // Internal Properties
        //
        //-----------------------------------------------------------------------------
        /// <summary>
        /// Signature Time Format - default to most descriptive form - in Xml syntax
        /// </summary>
        internal static String DefaultDateTimeFormat
        {
            get
            {
                //Toantk 14/5/2015: Change TimeFormat string from "YYYY-MM-DDThh:mm:ss.sTZD" to "YYYY-MM-DDThh:mm:ssTZD"
                return _dateTimePatternMap[1].Format;
            }
        }

        //-----------------------------------------------------------------------------
        //
        // Internal Methods
        //
        //-----------------------------------------------------------------------------
        /// <summary>
        /// Verify the given string is a legal Xml format
        /// </summary>
        /// <param name="candidateFormat">xml format to verify</param>
        /// <returns>true if legal</returns>
        internal static bool LegalFormat(String candidateFormat)
        {
            if (candidateFormat == null)
                throw new ArgumentNullException("candidateFormat");

            return (GetIndex(candidateFormat) != -1);
        }

        /// <summary>
        /// Obtain a W3C formatted SigningTime (equivalent to TimeStamp)
        /// </summary>
        /// <param name="xDoc">xml document we are building</param>
        /// <param name="dateTime">time to persist</param>
        /// <param name="signatureId">id of new signature</param>
        /// <param name="xmlDateTimeFormat">format to use - must be Xml date format legal syntax</param>
        /// <returns>given writer with SignatureProperties xml added to it</returns>
        /// <remarks>format matches that described in http://www.w3.org/TR/NOTE-datetime </remarks>
        /// <example>
        /// <Object Id="Package">
        ///     <SignatureProperties>
        ///         <SignatureProperty Target="#signatureId">
        ///             <SignatureTime>
        ///                 <Format>YYYY-MM-DDThh:mm:ssTZD</Format>
        ///                 <Value>1997-07-16T19:20:30.45+01:00</Value>
        ///             </SignatureTime>
        ///         </SignatureProperty>
        ///     </SignatureProperties>
        /// </Object>
        /// </example>
        internal static XmlElement AssembleSignatureProperties(
            XmlDocument xDoc, 
            DateTime dateTime, 
            String xmlDateTimeFormat,
            String signatureId)
        {
            Invariant.Assert(xDoc != null);
            Invariant.Assert(signatureId != null);

            // check for null format - use default if null
            if (xmlDateTimeFormat == null)
            {
                xmlDateTimeFormat = DefaultDateTimeFormat;
            }

            string[] dateTimeFormats = ConvertXmlFormatStringToDateTimeFormatString(xmlDateTimeFormat);

            // <SignatureProperties>
            XmlElement signatureProperties = xDoc.CreateElement(XTable.Get(XTable.ID.SignaturePropertiesTagName),
                SignedXml.XmlDsigNamespaceUrl);

            // <SignatureProperty Id="idSignatureTime" Target="#signatureId">
            XmlElement signatureProperty = xDoc.CreateElement(XTable.Get(XTable.ID.SignaturePropertyTagName), 
                SignedXml.XmlDsigNamespaceUrl);
            signatureProperties.AppendChild(signatureProperty);            
            XmlAttribute idAttr = xDoc.CreateAttribute(XTable.Get(XTable.ID.SignaturePropertyIdAttrName));
            idAttr.Value = XTable.Get(XTable.ID.SignaturePropertyIdAttrValue);
            signatureProperty.Attributes.Append(idAttr);
            XmlAttribute targetAttr = xDoc.CreateAttribute(XTable.Get(XTable.ID.TargetAttrName));
            targetAttr.Value = "#" + signatureId;
            signatureProperty.Attributes.Append(targetAttr);

            // <SignatureTime>
            XmlElement signatureTime = xDoc.CreateElement(XTable.Get(XTable.ID.SignatureTimeTagName),
                XTable.Get(XTable.ID.OpcSignatureNamespace));
            XmlElement signatureTimeFormat = xDoc.CreateElement(XTable.Get(XTable.ID.SignatureTimeFormatTagName),
                XTable.Get(XTable.ID.OpcSignatureNamespace));
            XmlElement signatureTimeValue = xDoc.CreateElement(XTable.Get(XTable.ID.SignatureTimeValueTagName),
                XTable.Get(XTable.ID.OpcSignatureNamespace));

            signatureTimeFormat.AppendChild(xDoc.CreateTextNode(xmlDateTimeFormat));
            //Edited by Toantk on 14/5/2015
            //Change TimeFormat used to convert from Local format to UTC format ("yyyy-MM-ddTHH:mm:sszzz" to "yyyy-MM-ddTHH:mm:ssZ")
            signatureTimeValue.AppendChild(xDoc.CreateTextNode(DateTimeToXmlFormattedTime(dateTime, dateTimeFormats[1])));

            signatureTime.AppendChild(signatureTimeFormat);
            signatureTime.AppendChild(signatureTimeValue);

            signatureProperty.AppendChild(signatureTime);

            return signatureProperties;
        }

        //Toantk 30/8/2015
        internal static XmlElement AssembleSignatureOfficeProperties(
            XmlDocument xDoc,
            string hashAlgorithm,
            String signatureId)
        {
            Invariant.Assert(xDoc != null);
            Invariant.Assert(signatureId != null);

            // <SignatureProperties>
            XmlElement signatureProperties = xDoc.CreateElement(XTable.Get(XTable.ID.SignaturePropertiesTagName),
                SignedXml.XmlDsigNamespaceUrl);

            // <SignatureProperty Id="idOfficeV1Details" Target="#signatureId">
            XmlElement signatureProperty = xDoc.CreateElement(XTable.Get(XTable.ID.SignaturePropertyTagName),
                SignedXml.XmlDsigNamespaceUrl);
            signatureProperties.AppendChild(signatureProperty);
            XmlAttribute idAttr = xDoc.CreateAttribute(XTable.Get(XTable.ID.SignaturePropertyIdAttrName));
            idAttr.Value = XTable.Get(XTable.ID.SignaturePropertyIdOfficeAttrValue);
            signatureProperty.Attributes.Append(idAttr);
            XmlAttribute targetAttr = xDoc.CreateAttribute(XTable.Get(XTable.ID.TargetAttrName));
            targetAttr.Value = "#" + signatureId;
            signatureProperty.Attributes.Append(targetAttr);

            // <SignatureInfoV1>
            XmlElement signatureInfoV1 = xDoc.CreateElement(XTable.Get(XTable.ID.SignatureInfoV1TagName),
                XTable.Get(XTable.ID.OpcSignatureOfficeNamespace));
            XmlElement manifestHashAlgorithm = xDoc.CreateElement(XTable.Get(XTable.ID.ManifestHashAlgorithmTagName),
                XTable.Get(XTable.ID.OpcSignatureOfficeNamespace));

            manifestHashAlgorithm.AppendChild(xDoc.CreateTextNode(hashAlgorithm));

            signatureInfoV1.AppendChild(manifestHashAlgorithm);

            signatureProperty.AppendChild(signatureInfoV1);

            return signatureProperties;
        }

        //Toantk 10/8/2016
        internal static XmlElement AssembleSignatureQualifyingProperties(
            XmlDocument xDoc,
            String hashAlgorithm,
            String signatureId,
            DateTime dateTime, 
            String xmlDateTimeFormat,
            X509Certificate2 certificate)
        {
            Invariant.Assert(xDoc != null);
            Invariant.Assert(signatureId != null);

            // check for null format - use default if null
            if (xmlDateTimeFormat == null)
            {
                xmlDateTimeFormat = DefaultDateTimeFormat;
            }
            string[] dateTimeFormats = ConvertXmlFormatStringToDateTimeFormatString(xmlDateTimeFormat);

            //<Object>
            //  <xd:QualifyingProperties xmlns:xd="http://uri.etsi.org/01903/v1.3.2#" Target="">
            //    <xd:SignedProperties Id="idSignedProperties">
            //      <xd:SignedSignatureProperties>
            //        <xd:SigningTime />
            //        <xd:SigningCertificate>
            //          <xd:Cert>
            //            <xd:CertDigest>
            //              <DigestMethod Algorithm="http://www.w3.org/2000/09/xmldsig#sha1" />
            //              <DigestValue />
            //            </xd:CertDigest>
            //            <xd:IssuerSerial>
            //              <X509IssuerName />
            //              <X509SerialNumber />
            //            </xd:IssuerSerial>
            //          </xd:Cert>
            //        </xd:SigningCertificate>
            //		  <xd:SignaturePolicyIdentifier>
			//          <xd:SignaturePolicyImplied />
		    //        </xd:SignaturePolicyIdentifier>
            //      </xd:SignedSignatureProperties>
            //    </xd:SignedProperties>
            //    <xd:UnsignedProperties>
            //      <xd:UnsignedSignatureProperties />
            //    </xd:UnsignedProperties>
            //  </xd:QualifyingProperties>
            //</Object>

            // <QualifyingProperties xmlns:xd="http://uri.etsi.org/01903/v1.3.2#" Target="">
            XmlElement qualifyingProperties = xDoc.CreateElement(XTable.Get(XTable.ID.XadesQualifyingPropertiesTagName), XTable.Get(XTable.ID.XadesSignatureNamespace));
            qualifyingProperties.SetAttribute("xmlns", SignedXml.XmlDsigNamespaceUrl);
            XmlAttribute targetAttr = xDoc.CreateAttribute(XTable.Get(XTable.ID.TargetAttrName));
            targetAttr.Value = "#" + signatureId;
            qualifyingProperties.Attributes.Append(targetAttr);

            // <SignedProperties Id="idSignedProperties">
            XmlElement signedProperties = xDoc.CreateElement(XTable.Get(XTable.ID.XadesSignedPropertiesTagName), XTable.Get(XTable.ID.XadesSignatureNamespace));
            XmlAttribute idAttr = xDoc.CreateAttribute(XTable.Get(XTable.ID.SignaturePropertyIdAttrName));
            idAttr.Value = XTable.Get(XTable.ID.XadesSignedPropertiesIdAttrValue);
            signedProperties.Attributes.Append(idAttr);
            qualifyingProperties.AppendChild(signedProperties);

            // <SignedSignatureProperties>
            XmlElement signedSignatureProperties = xDoc.CreateElement(XTable.Get(XTable.ID.XadesSignedSignaturePropertiesTagName), XTable.Get(XTable.ID.XadesSignatureNamespace));
            signedProperties.AppendChild(signedSignatureProperties);

            // <SigningTime>2016-01-26T09:55:09Z</SigningTime>
            XmlElement signingTime = xDoc.CreateElement(XTable.Get(XTable.ID.XadesSigningTimeTagName), XTable.Get(XTable.ID.XadesSignatureNamespace));
            signingTime.InnerText = DateTimeToXmlFormattedTime(dateTime, dateTimeFormats[1]);
            signedSignatureProperties.AppendChild(signingTime);

            // <SigningCertificate>            
            XmlElement signingCertificate = xDoc.CreateElement(XTable.Get(XTable.ID.XadesSigningCertificateTagName), XTable.Get(XTable.ID.XadesSignatureNamespace));
            signedSignatureProperties.AppendChild(signingCertificate);

            // <Cert>
            XmlElement cert = xDoc.CreateElement(XTable.Get(XTable.ID.XadesCertTagName), XTable.Get(XTable.ID.XadesSignatureNamespace));
            signingCertificate.AppendChild(cert);

            // <CertDigest>
            XmlElement certDigest = xDoc.CreateElement(XTable.Get(XTable.ID.XadesCertDigestTagName), XTable.Get(XTable.ID.XadesSignatureNamespace));
            cert.AppendChild(certDigest);

            // <DigestMethod>
            XmlElement digestMethod = xDoc.CreateElement(XTable.Get(XTable.ID.DigestMethodTagName), SignedXml.XmlDsigNamespaceUrl);
            XmlAttribute digestAlgorithmAttr = xDoc.CreateAttribute(XTable.Get(XTable.ID.AlgorithmAttrName));
            digestAlgorithmAttr.Value = hashAlgorithm;
            digestMethod.Attributes.Append(digestAlgorithmAttr);
            certDigest.AppendChild(digestMethod);

            // <DigestValue>
            XmlElement digestValue = xDoc.CreateElement(XTable.Get(XTable.ID.DigestValueTagName), SignedXml.XmlDsigNamespaceUrl);
            HashAlgorithm hashAlgorithmObject = XmlDigitalSignatureProcessor.GetHashAlgorithm(hashAlgorithm);
            XmlText digestValueText = xDoc.CreateTextNode(Convert.ToBase64String(hashAlgorithmObject.ComputeHash(certificate.RawData)));
            digestValue.AppendChild(digestValueText);
            certDigest.AppendChild(digestValue);

            // <IssuerSerial>
            XmlElement issuerSerial = xDoc.CreateElement(XTable.Get(XTable.ID.XadesIssuerSerialTagName), XTable.Get(XTable.ID.XadesSignatureNamespace));
            cert.AppendChild(issuerSerial);

            // <X509IssuerName>
            XmlElement x509IssuerName = xDoc.CreateElement(XTable.Get(XTable.ID.XadesX509IssuerNameTagName), SignedXml.XmlDsigNamespaceUrl);
            x509IssuerName.InnerText = certificate.Issuer;
            issuerSerial.AppendChild(x509IssuerName);

            // <X509SerialNumber>
            XmlElement x509SerialNumber = xDoc.CreateElement(XTable.Get(XTable.ID.XadesX509SerialNumberTagName), SignedXml.XmlDsigNamespaceUrl);
            x509SerialNumber.InnerText = new System.Numerics.BigInteger(certificate.GetSerialNumber()).ToString(CultureInfo.InvariantCulture);
            issuerSerial.AppendChild(x509SerialNumber);
            
            // <SignaturePolicyIdentifier>            
            XmlElement signaturePolicyIdentifier = xDoc.CreateElement(XTable.Get(XTable.ID.XadesSignaturePolicyIdentifierTagName), XTable.Get(XTable.ID.XadesSignatureNamespace));
            signedSignatureProperties.AppendChild(signaturePolicyIdentifier);

            // <SignaturePolicyImplied>            
            XmlElement signaturePolicyImplied = xDoc.CreateElement(XTable.Get(XTable.ID.XadesSignaturePolicyImpliedTagName), XTable.Get(XTable.ID.XadesSignatureNamespace));
            signaturePolicyIdentifier.AppendChild(signaturePolicyImplied);

            // <UnsignedProperties>
            XmlElement unsignedProperties = xDoc.CreateElement(XTable.Get(XTable.ID.XadesUnsignedPropertiesTagName), XTable.Get(XTable.ID.XadesSignatureNamespace));
            qualifyingProperties.AppendChild(unsignedProperties);

            // <UnsignedSignatureProperties />          
            XmlElement unsignedSignatureProperties = xDoc.CreateElement(XTable.Get(XTable.ID.XadesUnsignedSignaturePropertiesTagName), XTable.Get(XTable.ID.XadesSignatureNamespace));
            unsignedProperties.AppendChild(unsignedSignatureProperties);

            return qualifyingProperties;
        }

        //Toantk 17/8/2016
        internal static XmlElement AssembleSignatureTimeStamp(
            XmlDocument xDoc,
            String transformName,
            String timestampHash)
        {
            Invariant.Assert(xDoc != null);

            //<xd:SignatureTimeStamp>
            //  <CanonicalizationMethod Algorithm="http://www.w3.org/TR/2001/REC-xml-c14n-20010315"/>
            //  <xd:EncapsulatedTimeStamp></xd:EncapsulatedTimeStamp>
            //</xd:SignatureTimeStamp>

            //<xd:SignatureTimeStamp>
            XmlElement signatureTimeStamp = xDoc.CreateElement(XTable.Get(XTable.ID.XadesSignatureTimeStampTagName), XTable.Get(XTable.ID.XadesSignatureNamespace));
            
            //  <CanonicalizationMethod Algorithm="http://www.w3.org/TR/2001/REC-xml-c14n-20010315"/>
            XmlElement canonicalizationMethod = xDoc.CreateElement(XTable.Get(XTable.ID.XadesCanonicalizationMethodTagName), SignedXml.XmlDsigNamespaceUrl);
            XmlAttribute digestAlgorithmAttr = xDoc.CreateAttribute(XTable.Get(XTable.ID.AlgorithmAttrName));
            digestAlgorithmAttr.Value = transformName;
            canonicalizationMethod.Attributes.Append(digestAlgorithmAttr);
            signatureTimeStamp.AppendChild(canonicalizationMethod);

            //  <xd:EncapsulatedTimeStamp></xd:EncapsulatedTimeStamp>
            XmlElement encapsulatedTimeStamp = xDoc.CreateElement(XTable.Get(XTable.ID.XadesEncapsulatedTimeStampTagName), XTable.Get(XTable.ID.XadesSignatureNamespace));
            encapsulatedTimeStamp.InnerText = timestampHash;
            signatureTimeStamp.AppendChild(encapsulatedTimeStamp);

            return signatureTimeStamp;
        }

        /// <summary>
        /// Parse the xml and determine the signing time
        /// </summary>
        /// <param name="reader">NodeReader positioned at the SignatureProperties tag</param>
        /// <param name="signatureId">value of the Id attribute on the Signature tag</param>
        /// <param name="timeFormat">format found</param>
        /// <exception cref="XmlException">illegal format</exception>
        /// <returns>signing time</returns>
        internal static DateTime ParseSigningTime(XmlReader reader, string signatureId, out String timeFormat)
        {
            if (reader == null)
                throw new ArgumentNullException("reader");

            bool signatureTimePropertyFound = false;
            bool signatureTimeIdFound = false;
            string w3cSignatureNameSpace = SignedXml.XmlDsigNamespaceUrl;

            // <SignatureProperty> tag
            string signaturePropertyTag = XTable.Get(XTable.ID.SignaturePropertyTagName);
            string signaturePropertiesTag = XTable.Get(XTable.ID.SignaturePropertiesTagName);

            //initializing to a dummy value
            DateTime signingTime = DateTime.Now;
            timeFormat = null;
            
            while (reader.Read())
            {
                //Looking for <SignatureProperty> tag
                if (reader.MoveToContent() == XmlNodeType.Element
                    && (String.CompareOrdinal(reader.NamespaceURI, w3cSignatureNameSpace) == 0)
                    && (String.CompareOrdinal(reader.LocalName, signaturePropertyTag) == 0)
                    && reader.Depth == 2)
                {
                    //Verify Attributes
                    //Look for well-defined Id attribute and if it is present 
                    if (VerifyIdAttribute(reader))
                    {
                        //If we encounter more than one <SignatureProperty> tag with the expected
                        //id, then its an error.
                        if (signatureTimeIdFound)
                            throw new XmlException("PackageSignatureCorruption");
                        else
                            signatureTimeIdFound = true;
                        
                        //VerifyTargetAttribute will return false, if the Target attribute is missing
                        //or contains an incorrect value.
                        if(VerifyTargetAttribute(reader, signatureId))
                        {
                            signingTime = ParseSignatureTimeTag(reader, out timeFormat);
                            signatureTimePropertyFound = true;
                        }
                    }
                }
                else
                    //Expected <SignatureProperty> tag not found.
                    //Look for end tag corresponding to </SignatureProperty> or 
                    //if these are other custom defined properties, then anything with
                    //depth greater than 2 should be ignored as these can be nested elements.
                    if (((String.CompareOrdinal(signaturePropertyTag, reader.LocalName) == 0
                        && (reader.NodeType == XmlNodeType.EndElement)))
                        || reader.Depth > 2)
                        continue;
                    else
                        //If we find the end tag for </SignatureProperties> then we can stop parsing
                        if ((String.CompareOrdinal(signaturePropertiesTag, reader.LocalName) == 0
                        && (reader.NodeType == XmlNodeType.EndElement)))
                            break;
                        else
                            throw new XmlException("RequiredTagNotFound: " + signaturePropertyTag);
            }

            //We did find one or more <SignatureProperty> tags but there were none that
            //defined the id attribute and target attribute and <SignatureTime> element tag correctly.
            if(!signatureTimePropertyFound)
                throw new XmlException("PackageSignatureCorruption");

            return signingTime;
        }

       
        //-----------------------------------------------------------------------------
        //
        // Private Methods
        //
        //-----------------------------------------------------------------------------

        /// <summary>
        /// Parse the SignatureTime tag
        /// </summary>
        /// <param name="reader">NodeReader positioned at the SignatureProperty tag</param>
        /// <param name="timeFormat">format found</param>
        /// <exception cref="XmlException">illegal format</exception>
        /// <returns>signing time</returns>
        private static DateTime ParseSignatureTimeTag(XmlReader reader, out String timeFormat)
        {
            //There are no attributes on all the three tags that we parse in this method
            //<SignatureTime>, <Format>, <Value>
            int expectedAttributeCount = 0;

            string opcSignatureNameSpace = XTable.Get(XTable.ID.OpcSignatureNamespace);
            string signaturePropertyTag = XTable.Get(XTable.ID.SignaturePropertyTagName);
            string signatureTimeTag = XTable.Get(XTable.ID.SignatureTimeTagName);
            string timeValueTagName = XTable.Get(XTable.ID.SignatureTimeValueTagName);
            string timeFormatTagName = XTable.Get(XTable.ID.SignatureTimeFormatTagName);

            // <SignatureTime> must be one of <Format> or <Time>
            timeFormat = null;
            string timeValue = null;

            //Look for <SignatureTime> Tag
            if (reader.Read()
                && reader.MoveToContent() == XmlNodeType.Element
                && (String.CompareOrdinal(reader.NamespaceURI, opcSignatureNameSpace) == 0)
                && (String.CompareOrdinal(reader.LocalName, signatureTimeTag) == 0)
                && reader.Depth == 3
                && PackagingUtilities.GetNonXmlnsAttributeCount(reader) == expectedAttributeCount)
            {
                while (reader.Read())
                {
                    if (String.CompareOrdinal(reader.NamespaceURI, opcSignatureNameSpace) == 0
                        && reader.MoveToContent() == XmlNodeType.Element
                        && reader.Depth == 4)
                    {
                        // which tag do we have?
                        if ((String.CompareOrdinal(reader.LocalName, timeValueTagName) == 0)
                            && PackagingUtilities.GetNonXmlnsAttributeCount(reader) == expectedAttributeCount)
                        {
                            if (timeValue == null
                                && reader.Read()
                                && reader.MoveToContent() == XmlNodeType.Text
                                && reader.Depth == 5)
                            {
                                //After reading the content, the reader progresses to the next element.
                                //So after this method is called, the reader is positioned at the
                                //EndElement corresponding to Value tag - </Value>
                                // Note: ReadContentAsString will return String.Empty but never null
                                timeValue = reader.ReadContentAsString();
                                Debug.Assert(reader.NodeType == XmlNodeType.EndElement);
                            }
                            else
                                //This would happen if we found more than one Value tags or if there
                                //are other nested elements of if they are of a different XmlNodeType type
                                throw new XmlException("PackageSignatureCorruption");

                        }
                        else if ((String.CompareOrdinal(reader.LocalName, timeFormatTagName) == 0)
                                 && PackagingUtilities.GetNonXmlnsAttributeCount(reader) == expectedAttributeCount)
                        {
                            if (timeFormat == null
                                && reader.Read()
                                && reader.MoveToContent() == XmlNodeType.Text
                                && reader.Depth == 5)
                            {
                                //After reading the content, the reader progresses to the next element.
                                //So after this method is called, the reader is positioned at the
                                //EndElement corresponding to Format tag - </Format>
                                // Note: ReadContentAsString will return String.Empty but never null
                                timeFormat = reader.ReadContentAsString();
                                Debug.Assert(reader.NodeType == XmlNodeType.EndElement);
                            }
                            else
                                //This would happen if we found more than one Format tags or if there
                                //are other nested elements of if they are of a different XmlNodeType type
                                throw new XmlException("PackageSignatureCorruption");
                        }
                        else
                            //If we encounter any tag other than <Format> or <Time> nested within the <SignatureTime> tag
                            throw new XmlException("PackageSignatureCorruption");
                    }
                    else
                        //If we have encountered the end tag for the <SignatureTime> tag
                        //then we are done parsing the tag, and we can stop the parsing.
                        if (String.CompareOrdinal(signatureTimeTag, reader.LocalName) == 0
                        && (reader.NodeType == XmlNodeType.EndElement))
                        {
                            //We must find a  </SignatureProperty> tag at this point, 
                            //else it could be that there are more SignatureTime or  
                            //other tags nested here and that is an error.
                            if (reader.Read()
                                && reader.MoveToContent() == XmlNodeType.EndElement
                                && String.CompareOrdinal(signaturePropertyTag, reader.LocalName) == 0)
                                break;
                            else
                                throw new XmlException("PackageSignatureCorruption");
                        }
                        else
                            // if we do not find the nested elements as expected
                            throw new XmlException("PackageSignatureCorruption");
                }
            }
            else
                throw new XmlException("RequiredTagNotFound: " + signatureTimeTag);


            // generate an equivalent DateTime object
            if (timeValue != null && timeFormat != null)
                return XmlFormattedTimeToDateTime(timeValue, timeFormat);
            else
                throw new XmlException("PackageSignatureCorruption");
        }
        
        /// <summary>
        /// DateTime to XML Format
        /// </summary>
        /// <param name="dt">date time to convert</param>
        /// <param name="format">format to use - specified in DateTime syntax</param>
        /// <returns>opc-legal string suitable for embedding in XML digital signatures</returns>
        private static String DateTimeToXmlFormattedTime(DateTime dt, string format)
        {
            DateTimeFormatInfo formatter = new DateTimeFormatInfo();
            formatter.FullDateTimePattern = format;
            //Edited by Toantk on 14/5/2015
            //Add 'if' statement to convert Time to UniversalTime before converting to UTC format; otherwise, keep old code.
            if (format.Contains("Z"))
                return dt.ToUniversalTime().ToString(format, formatter);
            else
                return dt.ToString(format, formatter);
        }

        /// <summary>
        /// XML Format time string to DateTime
        /// </summary>
        /// <param name="s">string to parse</param>
        /// <param name="format">format to use - specified in Xml Signature date syntax</param>
        /// <exception cref="XmlException">Format does not match the given string</exception>
        /// <returns>DateTime</returns>
        private static DateTime XmlFormattedTimeToDateTime(String s, String format)
        {
            // convert Xml syntax to equivalent DateTime syntax
            string[] legalFormats = ConvertXmlFormatStringToDateTimeFormatString(format);

            // the default formatter is culture-invariant (which is what we want)
            DateTimeFormatInfo formatter = new DateTimeFormatInfo();
            formatter.FullDateTimePattern = format;
            return DateTime.ParseExact(s, legalFormats, formatter, 
                DateTimeStyles.NoCurrentDateDefault 
                | DateTimeStyles.AllowLeadingWhite 
                | DateTimeStyles.AllowTrailingWhite);
        }

        /// <summary>
        /// Get index of the row that matches the given format
        /// </summary>
        /// <param name="format">format to lookup</param>
        /// <returns>-1 if not found</returns>
        private static int GetIndex(String format)
        {
            for (int i = 0; i < _dateTimePatternMap.GetLength(0); i++)
            {
                if (String.CompareOrdinal(_dateTimePatternMap[i].Format, format) == 0)
                {
                    return i;
                }
            }
            return -1;
        }

        /// <summary>
        /// Convert Xml format syntax to DateTime format syntax
        /// </summary>
        /// <param name="format"></param>
        /// <returns></returns>
        private static string[] ConvertXmlFormatStringToDateTimeFormatString(String format)
        {
            return _dateTimePatternMap[GetIndex(format)].Patterns; 
        }
                
        /// <summary>
        /// Verify if the SignatureProperty tag has a valid Id attribute
        /// </summary>
        /// <param name="reader">NodeReader positioned at the SignatureProperty tag</param>
        /// <returns>true, if Id attribute is present and has the correct value, else false</returns>
        private static bool VerifyIdAttribute(XmlReader reader)
        {
            string idAttrValue = reader.GetAttribute(XTable.Get(XTable.ID.SignaturePropertyIdAttrName));

            if(idAttrValue!=null 
                && (String.CompareOrdinal(idAttrValue,XTable.Get(XTable.ID.SignaturePropertyIdAttrValue)) == 0))
                return true;
            else
                return false;
        }

        /// <summary>
        /// Verify if the mandatory Target attribute exists on the SignatureProperty tag
        /// </summary>
        /// <param name="reader">NodeReader positioned at the SignatureProperty tag</param>
        /// <param name="signatureId">value of the Id attribute on the Signature tag</param>
        /// <returns>true, if Target attribute is present and has the correct value, else false</returns>
        private static bool VerifyTargetAttribute(XmlReader reader, string signatureId)
        {
            string idTargetValue = reader.GetAttribute(XTable.Get(XTable.ID.TargetAttrName));

            if (idTargetValue != null)
            {
                //whether there is an Id attribute on the <Signature> tag or no,
                //an empty Target attribute on <SignatureProperty> tag, is allowed.
                //Empty string means current document 
                if (String.CompareOrdinal(idTargetValue, String.Empty) == 0)
                    return true;
                else
                {
                    //If the Target attribute has a non-empty string then
                    //it must match the <Signature> tag Id attribute value
                    if (signatureId != null && String.CompareOrdinal(idTargetValue, "#" + signatureId) == 0)
                        return true;
                    else
                        return false;
                }
            }
            else
                return false;
        }

        //-----------------------------------------------------------------------------
        //
        // Private Fields
        //
        //-----------------------------------------------------------------------------

        // This is a mapping between time formats allowed by Opc spec (taken from 
        // http://www.w3.org/TR/NOTE-datetime) and the equivalent formatting string 
        // expected by the DateTimeFormatInfo class.
        private struct TimeFormatMapEntry
        {
            public TimeFormatMapEntry(string xmlFormatString, string[] dateTimePatterns)
            {
                _xmlFormatString = xmlFormatString;
                _dateTimePatterns = dateTimePatterns;
            }

            public string   Format { get { return _xmlFormatString; }}
            public string[] Patterns { get { return _dateTimePatterns; }}

            private string      _xmlFormatString;
            private string[]    _dateTimePatterns;
        };

        private static readonly TimeFormatMapEntry[] _dateTimePatternMap = 
        {
            // Opc Spec value                                   Equivalent DateTimePattern(s)
            new TimeFormatMapEntry("YYYY-MM-DDThh:mm:ss.sTZD",  new string[] {"yyyy-MM-ddTHH:mm:ss.fzzz",   "yyyy-MM-ddTHH:mm:ss.fZ"}),
            new TimeFormatMapEntry("YYYY-MM-DDThh:mm:ssTZD",    new string[] {"yyyy-MM-ddTHH:mm:sszzz",     "yyyy-MM-ddTHH:mm:ssZ"}),
            new TimeFormatMapEntry("YYYY-MM-DDThh:mmTZD",       new string[] {"yyyy-MM-ddTHH:mmzzz",        "yyyy-MM-ddTHH:mmZ"}),
            new TimeFormatMapEntry("YYYY-MM-DD",                new string[] {"yyyy-MM-dd"}),
            new TimeFormatMapEntry("YYYY-MM",                   new string[] {"yyyy-MM"}),
            new TimeFormatMapEntry("YYYY",                      new string[] {"yyyy"}),
        };
    }
  }

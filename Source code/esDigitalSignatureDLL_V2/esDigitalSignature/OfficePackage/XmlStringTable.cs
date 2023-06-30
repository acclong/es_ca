//-----------------------------------------------------------------------------
//
// <copyright file="XmlSignatureStringTable.cs" company="Microsoft">
//    Copyright (C) Microsoft Corporation.  All rights reserved.
// </copyright>
//
// Description:
//  Static helper class that serves up Xml strings used to generate and parse
//  XmlDigitalSignatures.
//
// History:
//  01/30/2005: BruceMac: Initial implementation.
//
//-----------------------------------------------------------------------------

using System;
using System.Xml;
using System.Diagnostics;

namespace esDigitalSignature.OfficePackage
{
    /// <summary>
    /// Xml and string table
    /// </summary>
    /// <remarks>See: http://www.w3.org/TR/2002/REC-xmldsig-core-20020212/ for details</remarks>
    internal static class XTable
    {
        /// <summary>
        /// Get string given the ID
        /// </summary>
        /// <param name="i">string id</param>
        /// <returns>string</returns>
        internal static String Get(ID i)
        {
            // catch mismatch in table editing
            Debug.Assert((int)ID._IllegalValue == _table.Length - 1, "ID enumeration should match TableEntry array - verify edits are matching");
            Debug.Assert(i >= ID.OpcSignatureNamespace && i < ID._IllegalValue, "string index out of bounds");
            Debug.Assert(i == _table[(int)i].id, "table enum out of order - verify order");
            return _table[(int)i].s;
        }

        /// <summary>
        /// One-to-One mapping from enum to string - this must be kept in [....] with _table
        /// </summary>
        internal enum ID : int
        {
            OpcSignatureNamespace,
            OpcSignatureNamespacePrefix,
            OpcSignatureNamespaceAttribute,
            W3CSignatureNamespaceRoot,
            RelationshipsTransformName,
            //SignatureNamespacePrefix,
            SignatureTagName,
            OpcSignatureAttrValue,
            SignedInfoTagName,
            ReferenceTagName,
            //            CanonicalizationMethodTagName,
            SignatureMethodTagName,
            ObjectTagName,
            KeyInfoTagName,
            ManifestTagName,
            TransformTagName,
            TransformsTagName,
            AlgorithmAttrName,
            SourceIdAttrName,
            OpcAttrValue,
            OpcLinkAttrValue,
            TargetAttrName,
            SignatureValueTagName,
            UriAttrName,
            DigestMethodTagName,
            DigestValueTagName,
            SignaturePropertiesTagName,
            SignaturePropertyTagName,
            SignatureTimeTagName,
            SignatureTimeFormatTagName,
            SignatureTimeValueTagName,
            RelationshipsTagName,
            RelationshipReferenceTagName,
            RelationshipsGroupReferenceTagName,
            SourceTypeAttrName,
            SignaturePropertyIdAttrName,
            SignaturePropertyIdAttrValue,
            //Toantk 30/8/2015
            OpcSignatureOfficeNamespace,
            OpcOfficeAttrValue,
            OpcOfficeLinkAttrValue,
            SignatureInfoV1TagName,
            ManifestHashAlgorithmTagName,
            SignaturePropertyIdOfficeAttrValue,
            //Toantk 15/8/2016
            XadesSignatureNamespace,
            XadesSignatureNamespaceRoot,
            XadesSignedPropertiesIdAttrValue,
            XadesSignedPropertiesIdLink,
            XadesQualifyingPropertiesTagName,
            XadesSignedPropertiesTagName,
            XadesSignedSignaturePropertiesTagName,
            XadesSigningTimeTagName,
            XadesSigningCertificateTagName,
            XadesCertTagName,
            XadesCertDigestTagName,
            XadesIssuerSerialTagName,
            XadesX509IssuerNameTagName,
            XadesX509SerialNumberTagName,
            XadesSignaturePolicyIdentifierTagName,
            XadesSignaturePolicyImpliedTagName,
            XadesUnsignedPropertiesTagName,
            XadesUnsignedSignaturePropertiesTagName,
            XadesSignatureTimeStampTagName,
            XadesCanonicalizationMethodTagName,
            XadesEncapsulatedTimeStampTagName,
            //            PackageRelationshipPartName,
            _IllegalValue                   // sentinel - not a legal value - equivalent to "not set" or "null"

        }

        //-----------------------------------------------------------------------------
        // private members
        //-----------------------------------------------------------------------------
        internal struct TableEntry
        {
            internal XTable.ID id;
            internal string s;

            internal TableEntry(XTable.ID index, string str) { id = index; s = str; }
        };

        private static readonly TableEntry[] _table = new TableEntry[]
        {
            new TableEntry( ID.OpcSignatureNamespace,               "http://schemas.openxmlformats.org/package/2006/digital-signature" ),
            new TableEntry( ID.OpcSignatureNamespacePrefix,         "opc" ),
            new TableEntry( ID.OpcSignatureNamespaceAttribute,      "xmlns:opc" ),
            new TableEntry( ID.W3CSignatureNamespaceRoot,           "http://www.w3.org/2000/09/xmldsig#" ),
            new TableEntry( ID.RelationshipsTransformName,          "http://schemas.openxmlformats.org/package/2006/RelationshipTransform" ),
//            new TableEntry( ID.SignatureNamespacePrefix,            "ds" ),
            new TableEntry( ID.SignatureTagName,                    "Signature" ),
            new TableEntry( ID.OpcSignatureAttrValue,               "SignatureIdValue" ),
            new TableEntry( ID.SignedInfoTagName,                   "SignedInfo" ),
            new TableEntry( ID.ReferenceTagName,                    "Reference" ),
//            new TableEntry( ID.CanonicalizationMethodTagName,     "CanonicalizationMethod" ),
            new TableEntry( ID.SignatureMethodTagName,              "SignatureMethod" ),
            new TableEntry( ID.ObjectTagName,                       "Object" ),
            new TableEntry( ID.KeyInfoTagName,                      "KeyInfo" ),
            new TableEntry( ID.ManifestTagName,                     "Manifest" ),
            new TableEntry( ID.TransformTagName,                    "Transform" ),
            new TableEntry( ID.TransformsTagName,                   "Transforms" ),
            new TableEntry( ID.AlgorithmAttrName,                   "Algorithm" ),
            new TableEntry( ID.SourceIdAttrName,                    "SourceId" ),
            new TableEntry( ID.OpcAttrValue,                        "idPackageObject" ),
            new TableEntry( ID.OpcLinkAttrValue,                    "#idPackageObject" ),
            new TableEntry( ID.TargetAttrName,                      "Target" ),
            new TableEntry( ID.SignatureValueTagName,               "SignatureValue" ),
            new TableEntry( ID.UriAttrName,                         "URI" ),
            new TableEntry( ID.DigestMethodTagName,                 "DigestMethod" ),
            new TableEntry( ID.DigestValueTagName,                  "DigestValue" ),
            new TableEntry( ID.SignaturePropertiesTagName,          "SignatureProperties" ),
            new TableEntry( ID.SignaturePropertyTagName,            "SignatureProperty" ),
            new TableEntry( ID.SignatureTimeTagName,                "SignatureTime" ),
            new TableEntry( ID.SignatureTimeFormatTagName,          "Format" ),
            new TableEntry( ID.SignatureTimeValueTagName,           "Value" ),
            new TableEntry( ID.RelationshipsTagName,                "Relationships" ),
            new TableEntry( ID.RelationshipReferenceTagName,        "RelationshipReference" ),
            new TableEntry( ID.RelationshipsGroupReferenceTagName,  "RelationshipsGroupReference" ),
            new TableEntry( ID.SourceTypeAttrName,                  "SourceType" ),
            new TableEntry( ID.SignaturePropertyIdAttrName,         "Id" ),
            new TableEntry( ID.SignaturePropertyIdAttrValue,        "idSignatureTime" ),
            //Toantk 30/8/2015
            new TableEntry( ID.OpcSignatureOfficeNamespace,               "http://schemas.microsoft.com/office/2006/digsig" ),
            new TableEntry( ID.OpcOfficeAttrValue,                        "idOfficeObject" ),
            new TableEntry( ID.OpcOfficeLinkAttrValue,                    "#idOfficeObject" ),
            new TableEntry( ID.SignatureInfoV1TagName,                    "SignatureInfoV1" ),
            new TableEntry( ID.ManifestHashAlgorithmTagName,                    "ManifestHashAlgorithm" ),
            new TableEntry( ID.SignaturePropertyIdOfficeAttrValue,        "idOfficeV1Details" ),
            //Toantk 15/8/2016
            new TableEntry( ID.XadesSignatureNamespace,           "http://uri.etsi.org/01903/v1.3.2#" ),
            new TableEntry( ID.XadesSignatureNamespaceRoot,           "http://uri.etsi.org/01903#" ),
            new TableEntry( ID.XadesSignedPropertiesIdAttrValue,        "idSignedProperties" ),
            new TableEntry( ID.XadesSignedPropertiesIdLink,        "#idSignedProperties" ),
            new TableEntry( ID.XadesQualifyingPropertiesTagName,           "xd:QualifyingProperties" ),
            new TableEntry( ID.XadesSignedPropertiesTagName,           "xd:SignedProperties" ),
            new TableEntry( ID.XadesSignedSignaturePropertiesTagName,           "xd:SignedSignatureProperties" ),
            new TableEntry( ID.XadesSigningTimeTagName,           "xd:SigningTime" ),
            new TableEntry( ID.XadesSigningCertificateTagName,           "xd:SigningCertificate" ),
            new TableEntry( ID.XadesCertTagName,           "xd:Cert" ),
            new TableEntry( ID.XadesCertDigestTagName,           "xd:CertDigest" ),
            new TableEntry( ID.XadesIssuerSerialTagName,           "xd:IssuerSerial" ),
            new TableEntry( ID.XadesX509IssuerNameTagName,           "X509IssuerName" ),
            new TableEntry( ID.XadesX509SerialNumberTagName,           "X509SerialNumber" ),
            new TableEntry( ID.XadesSignaturePolicyIdentifierTagName,           "xd:SignaturePolicyIdentifier" ),
            new TableEntry( ID.XadesSignaturePolicyImpliedTagName,           "xd:SignaturePolicyImplied" ),
            new TableEntry( ID.XadesUnsignedPropertiesTagName,           "xd:UnsignedProperties" ),
            new TableEntry( ID.XadesUnsignedSignaturePropertiesTagName,           "xd:UnsignedSignatureProperties" ),
            new TableEntry( ID.XadesSignatureTimeStampTagName,           "xd:SignatureTimeStamp" ),
            new TableEntry( ID.XadesCanonicalizationMethodTagName,           "CanonicalizationMethod" ),
            new TableEntry( ID.XadesEncapsulatedTimeStampTagName,           "xd:EncapsulatedTimeStamp" ),

            new TableEntry( ID._IllegalValue,                       "" ),
        };
    }
}


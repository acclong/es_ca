﻿using System;

namespace esDigitalSignature.Package
{
    [Flags]
    internal enum PackageXmlEnum
    {
        NotDefined,
        XmlSchemaInstanceNamespace,
        XmlSchemaInstanceNamespacePrefix,
        XmlNamespacePrefix,
        PackageCorePropertiesNamespace,
        DublinCorePropertiesNamespace,
        DublinCoreTermsNamespace,
        DublinCorePropertiesNamespacePrefix,
        DublincCoreTermsNamespacePrefix,
        CoreProperties,
        Type,
        Creator,
        Identifier,
        Title,
        Subject,
        Description,
        Language,
        Created,
        Modified,
        ContentType,
        Keywords,
        Category,
        Version,
        LastModifiedBy,
        ContentStatus,
        Revision,
        LastPrinted
    }
}



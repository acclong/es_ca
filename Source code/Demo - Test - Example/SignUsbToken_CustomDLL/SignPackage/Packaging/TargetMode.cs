//-----------------------------------------------------------------------------
//
// <copyright file="TargetMode.cs" company="Microsoft">
//    Copyright (C) Microsoft Corporation.  All rights reserved.
// </copyright>
//
// Description:
//  TargetMode enumeration is used as an indication mechanism to interpret the 
//  "base" uri of for the PackageRelationship target Uri. 
//  Possible values - 
//              0: "Internal" - target points to a part within the package
//                              PackageRelationship target uri must be relative.
//              1: "External" - target points to an external resource. The
//                              resource can be relative to the package entity
//                              or an absolute URI
// History:
//  10/25/2004: SarjanaS: Initial creation. 
//
//-----------------------------------------------------------------------------

namespace SignPackage
{
    /// <summary>
    ///  The TargetMode enumeration is used to interpret the 
    ///  "base" uri for the PackageRelationship target Uri. 
    ///  Possible values - 
    ///              0: "Internal" - target points to a part within the package
    ///                              PackageRelationship target uri must be relative.
    ///              1: "External" - target points to an external resource. The
    ///                              resource can be relative to the package entity
    ///                              or an absolute URI
    /// </summary>
    public enum TargetMode : int
    {
            /// <summary>
            /// TargetMode is "Internal".
            /// PackageRelationship target points to a part within the package
            /// PackageRelationship target uri must be relative.
            /// </summary>
            Internal = 0,

            /// <summary>
            /// TargetMode is "External".
            /// PackageRelationship target points to an external resource. 
            /// PackageRelationship target uri can be relative or absolute.
            /// The resource can be relative to the package entity or an absolute URI.
            /// </summary>
            External = 1,
    }
}

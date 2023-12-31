//-----------------------------------------------------------------------------
//
// <copyright file="PackagePartCollection.cs" company="Microsoft">
//    Copyright (C) Microsoft Corporation.  All rights reserved.
// </copyright>
//
// Description:
//  This is a base abstract class for PackagePartCollection. This is a part of the 
//  MMCF Packaging Layer
//
// History:
//  01/03/2004: SarjanaS: Initial creation. [Stubs only]
//  03/01/2004: SarjanaS: Implemented the functionality for all the members.
//-----------------------------------------------------------------------------

// Allow use of presharp warning numbers [6506] unknown to the compiler
#pragma warning disable 1634, 1691

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace esDigitalSignature.Package
{
    /// <summary>
    /// This class is used to get an enumerator for the Parts in a container. 
    /// This is a part of the Packaging Layer APIs
    /// </summary>   
    public class PackagePartCollection : IEnumerable<PackagePart>
    {
        //------------------------------------------------------
        //
        //  Public Constructors
        //
        //------------------------------------------------------
        // None
        //------------------------------------------------------
        //
        //  Public Properties
        //
        //------------------------------------------------------
        // None
        //------------------------------------------------------
        //
        //  Public Methods
        //
        //------------------------------------------------------        
        
        #region Public Methods

        /// <summary>
        /// Returns an enumerator over all the Parts in the container
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }



        /// <summary>
        /// Returns an enumerator over all the Parts in the container
        /// </summary>
        /// <returns></returns>
        IEnumerator<PackagePart> IEnumerable<PackagePart>.GetEnumerator()
        {
            return GetEnumerator();          
        }


        /// <summary>
        /// Returns an enumerator over all the Parts in the Container
        /// </summary>
        /// <returns></returns>
        public IEnumerator<PackagePart> GetEnumerator()
        {
            //PRESHARP:Warning 6506 Parameter to this public method must be validated:  A null-dereference can occur here.
            //The Dictionary.Values property always returns a collection, even if empty. It never returns a null.
#pragma warning disable 6506
            return _partList.Values.GetEnumerator();
#pragma warning restore 6506
        }
        #endregion Public Methods

        //------------------------------------------------------
        //
        //  Public Events
        //
        //------------------------------------------------------
        // None
        //------------------------------------------------------
        //
        //  Internal Constructors
        //
        //------------------------------------------------------

        #region Internal Constructor

        internal PackagePartCollection(SortedList<PackUriHelper.ValidatedPartUri, PackagePart> partList)
        {
            Debug.Assert(partList != null, "partDictionary parameter cannot be null");
            _partList = partList;
        }

        #endregion Internal Constructor
      
        //------------------------------------------------------
        //
        //  Internal Properties
        //
        //------------------------------------------------------
        // None
        //------------------------------------------------------
        //
        //  Internal Methods
        //
        //------------------------------------------------------
        // None
        //------------------------------------------------------
        //
        //  Internal Events
        //
        //------------------------------------------------------
        // None
        //------------------------------------------------------
        //
        //  Private Methods
        //
        //------------------------------------------------------
        // None
        //------------------------------------------------------
        //
        //  Private Fields
        //
        //------------------------------------------------------

        #region Private Members

        private SortedList<PackUriHelper.ValidatedPartUri, PackagePart> _partList;

        #endregion Private Members
    }
}

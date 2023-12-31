//-----------------------------------------------------------------------------
//-------------   *** WARNING ***
//-------------    This file is part of a legally monitored development project.  
//-------------    Do not check in changes to this project.  Do not raid bugs on this
//-------------    code in the main PS database.  Do not contact the owner of this
//-------------    code directly.  Contact the legal team at �ZSLegal� for assistance.
//-------------   *** WARNING ***
//-----------------------------------------------------------------------------

//-----------------------------------------------------------------------------
//
// <copyright file="ZipFileInfo.cs" company="Microsoft">
//    Copyright (C) Microsoft Corporation.  All rights reserved.
// </copyright>
//
// Description:
//  This is an internal class that enables interactions with Zip archives
//  for OPC scenarios 
//
// History:
//  11/19/2004: IgorBel: Initial creation.
//
//-----------------------------------------------------------------------------

using System;
using System.IO;
using System.Diagnostics;

namespace esDigitalSignature.OfficePackage
{
    internal sealed class ZipFileInfo
    {
        //------------------------------------------------------
        //
        //  Public Members
        //
        //------------------------------------------------------
        // None 

        //------------------------------------------------------
        //
        //  Internal Constructors
        //
        //------------------------------------------------------
        // None

        //------------------------------------------------------
        //
        // Internal API Methods (although these methods are marked as 
        // Internal they are part of the internal ZIP IO API surface 
        //
        //------------------------------------------------------
        internal Stream GetStream(FileMode mode, FileAccess access)
        {
            CheckDisposed();
            return _fileBlock.GetStream(mode, access);
        }
    
        //------------------------------------------------------
        //
        //  Internal Properties
        //
        //------------------------------------------------------
        internal string Name
        {
            get
            {
                CheckDisposed();            
                return _fileBlock.FileName;
            }
        }

        internal ZipArchive ZipArchive
        {
            get
            {
                CheckDisposed();            
                return _zipArchive;
            }
        }

        internal CompressionMethodEnum CompressionMethod
        {
            get
            {
                CheckDisposed();            
                return _fileBlock.CompressionMethod;
            }
        }   
                         
        internal DateTime LastModFileDateTime 
        {
            get
            {
                CheckDisposed();            
                return ZipIOBlockManager.FromMsDosDateTime(_fileBlock.LastModFileDateTime);                
            }
        }            
                
#if false
        internal bool EncryptedFlag
        {
            get
            {
                CheckDisposed();            
                return _fileBlock.EncryptedFlag;                
            }
        }                
#endif
        
        internal DeflateOptionEnum DeflateOption
        {
            get
            {
                CheckDisposed();            
                return _fileBlock.DeflateOption;
            }
        }                
#if false
        internal bool StreamingCreationFlag
        {
            get
            {
                CheckDisposed();            
                return _fileBlock.StreamingCreationFlag;
            }
        }                
#endif
        // This ia Directory flag based on the informtion from the central directory 
        // at the moment we have only provide reliable value for the files authored in MS-DOS 
        // The upper byte of version made by indicating (OS) must be == 0 (MS-DOS)
        // for the other cases (OSes) we will return false 
        internal bool FolderFlag
        {
            get
            {
                CheckDisposed();            
                return _fileBlock.FolderFlag;
            }
        }                

        // This ia Directory flag based on the informtion from the central directory 
        // at the moment we have only provide reliable value for the files authored in MS-DOS 
        // The upper byte of version made by indicating (OS) must be == 0 (MS-DOS)
        // for the other cases (OSes) we will return false 
        internal bool VolumeLabelFlag
        {
            get
            {
                CheckDisposed();            
                return _fileBlock.VolumeLabelFlag;
            }
        }                
        
        //------------------------------------------------------
        // Internal NON API Constructor (this constructor is marked as internal 
        // and isNOT part of the ZIP IO API surface) 
        //  It supposed to be called only by the ZipArchive class 
        //------------------------------------------------------
        internal ZipFileInfo(ZipArchive zipArchive, ZipIOLocalFileBlock fileBlock)
        {
            Debug.Assert((fileBlock != null) && (zipArchive != null));
            _fileBlock = fileBlock;
            _zipArchive = zipArchive;
#if DEBUG
            // validate that date time is legal
            DateTime dt = LastModFileDateTime;
#endif
        }

        //------------------------------------------------------
        // Internal NON API property to be used to map FileInfo back to a block that needs to be deleted 
        // (this prperty is marked as internal and isNOT part of the ZIP IO API surface)         
        //  It supposed to be called only by the ZipArchive class 
        //------------------------------------------------------
        internal ZipIOLocalFileBlock LocalFileBlock
        {
            get
            {
                return _fileBlock;
            }
        }
        //------------------------------------------------------
        //
        //  Private Methods
        //
        //------------------------------------------------------
        private void CheckDisposed()
        {
            _fileBlock.CheckDisposed();
        }

        //------------------------------------------------------
        //
        //  Private Properties
        //
        //------------------------------------------------------

        //------------------------------------------------------
        //
        //  Private Fields 
        //
        //------------------------------------------------------        
        private ZipIOLocalFileBlock _fileBlock;
        private ZipArchive _zipArchive;
    }
}

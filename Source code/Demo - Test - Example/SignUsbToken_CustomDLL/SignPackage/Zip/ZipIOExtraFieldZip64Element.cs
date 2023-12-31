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
// <copyright file="ZipIoExtraFieldZip64Element.cs" company="Microsoft">
//    Copyright (C) Microsoft Corporation.  All rights reserved.
// </copyright>
//
// Description:
//  This is an internal class that helps managing/parsing the 
//  ZIP64 Extended Information Extra Field (0x0001): for OPC scenarios 
//  In order to isolate this from IO and streams, it deals with the data in the Byte[] form 
//
// History:
//  05/16/2005: IgorBel: Initial creation.
//
//-----------------------------------------------------------------------------

using System;
using System.IO;
using System.Diagnostics;
using System.Windows;
using MS.Internal.WindowsBase;

namespace SignPackage
{
    internal enum ZipIOZip64ExtraFieldUsage
    {
        None                         = 0,    
        UncompressedSize    = 1,
        CompressedSize          = 2,
        OffsetOfLocalHeader     = 4,
        DiskNumber                 = 8,
    }
    
    /// <summary>
    /// This class is used to represent and parse the the 
    /// ZIP64 Extended Information Extra Field (0x0001)
    /// In order to isolate this from IO and streams it deals with the data in the Byte[] form 
    /// </summary>                
    internal class ZipIOExtraFieldZip64Element : ZipIOExtraFieldElement
    {
        // creates brand new empty ZIP 64 extra field element 
        internal static ZipIOExtraFieldZip64Element CreateNew()
        {
            ZipIOExtraFieldZip64Element newElement = new ZipIOExtraFieldZip64Element();
                
            return newElement;
        }

        // extracts ZIP 64 extra field element from a given byte array 
        internal override void ParseDataField(BinaryReader reader, UInt16 size)
        {
            Debug.Assert(reader != null);

            if ((_zip64ExtraFieldUsage & ZipIOZip64ExtraFieldUsage.UncompressedSize) != 0)
            {
                _uncompressedSize = reader.ReadUInt64();

                if (size < sizeof(UInt64))
                {
                    throw new FileFormatException("CorruptedData");
                }

                size -= sizeof(UInt64);
            }

            if ((_zip64ExtraFieldUsage & ZipIOZip64ExtraFieldUsage.CompressedSize) != 0)
            {
                _compressedSize = reader.ReadUInt64();

                if (size < sizeof(UInt64))
                {
                    throw new FileFormatException("CorruptedData");
                }

                size -= sizeof(UInt64);
            }

            if ((_zip64ExtraFieldUsage & ZipIOZip64ExtraFieldUsage.OffsetOfLocalHeader) != 0)
            {
                _offsetOfLocalHeader = reader.ReadUInt64();

                if (size < sizeof(UInt64))
                {
                    throw new FileFormatException("CorruptedData");
                }

                size -= sizeof(UInt64);
            }

            if ((_zip64ExtraFieldUsage & ZipIOZip64ExtraFieldUsage.DiskNumber) != 0)
            {
                _diskNumber = reader.ReadUInt32();

                if (size < sizeof(UInt32))
                {
                    throw new FileFormatException("CorruptedData");
                }

                size -= sizeof(UInt32);
            }

            if (size != 0)
            {
                throw new FileFormatException("CorruptedData");
            }

            Validate();
        }

        internal override void Save(BinaryWriter writer)
        {
            // if it is == 0 we shouldn't be persisting this 
            Debug.Assert(SizeField>0);
                
            writer.Write(_constantFieldId);
            writer.Write(SizeField);

            if ((_zip64ExtraFieldUsage & ZipIOZip64ExtraFieldUsage.UncompressedSize) != 0)
            {
                writer.Write(_uncompressedSize);
            }
            
            if ((_zip64ExtraFieldUsage  & ZipIOZip64ExtraFieldUsage.CompressedSize) != 0)
            {
                writer.Write(_compressedSize);
            }

            if ((_zip64ExtraFieldUsage  & ZipIOZip64ExtraFieldUsage.OffsetOfLocalHeader) != 0)
            {
                writer.Write(_offsetOfLocalHeader);
            }

            if ((_zip64ExtraFieldUsage  & ZipIOZip64ExtraFieldUsage.DiskNumber) != 0)
            {
                writer.Write(_diskNumber);
            }
        }

        // This property calculates size of the field on disk (how many bytes need to be allocated on the disk)
        internal override UInt16 Size
        {
            get
            {
                if (SizeField == 0) // we do not need to save this, if it is empty 
                {
                    return 0;
                }
                else
                {
                    return checked((UInt16) (SizeField + MinimumSize));
                }
            }
        }            

        // This property calculates the value of the size record whch holds the size without the Id and without the size itself.
        // we are always guranteed that   Size == SizeField + 2 * sizeof(UInt16))
        internal override UInt16 SizeField
        {
            get
            {
                UInt16 size = 0;
                
                if ((_zip64ExtraFieldUsage & ZipIOZip64ExtraFieldUsage.UncompressedSize) != 0)
                {
                    size += sizeof(UInt64);
                }

                if ((_zip64ExtraFieldUsage & ZipIOZip64ExtraFieldUsage.CompressedSize) != 0)
                {
                    size += sizeof(UInt64);
                }

                if ((_zip64ExtraFieldUsage & ZipIOZip64ExtraFieldUsage.OffsetOfLocalHeader) != 0)
                {
                    size += sizeof(UInt64);
                }

                if ((_zip64ExtraFieldUsage & ZipIOZip64ExtraFieldUsage.DiskNumber) != 0)
                {
                    size += sizeof(UInt32);
                }

                return size;
            }
        }

        static internal UInt16 ConstantFieldId
        {
            get
            {
                return _constantFieldId; 
            }
        }
 
        internal long UncompressedSize
        {
            get
            {
                // we should never be asked to provide such value, if we do not have it  
                Debug.Assert((_zip64ExtraFieldUsage & ZipIOZip64ExtraFieldUsage.UncompressedSize) != 0);
                return (long)_uncompressedSize; 
            }
            set
            {
                Debug.Assert(value >=0 );

                _zip64ExtraFieldUsage |= ZipIOZip64ExtraFieldUsage.UncompressedSize;
                _uncompressedSize = (UInt64)value; 
            }
        }
        
        internal long CompressedSize
        {
            get
            {
                // we should never be asked to provide such value, if we do not have it  
                Debug.Assert((_zip64ExtraFieldUsage & ZipIOZip64ExtraFieldUsage.CompressedSize) != 0);
                return (long)_compressedSize; 
            }
            set
            {
                Debug.Assert(value >=0 );
                
                _zip64ExtraFieldUsage |= ZipIOZip64ExtraFieldUsage.CompressedSize;
                _compressedSize = (UInt64)value;             
            }
        }
        
        internal long OffsetOfLocalHeader
        {
            get
            {
                // we should never be asked to provide such value, if we do not have it  
                Debug.Assert((_zip64ExtraFieldUsage & ZipIOZip64ExtraFieldUsage.OffsetOfLocalHeader) != 0);
                return (long)_offsetOfLocalHeader; 
            }
            set
            {
                Debug.Assert(value >=0 );
                
                _zip64ExtraFieldUsage |= ZipIOZip64ExtraFieldUsage.OffsetOfLocalHeader;
                _offsetOfLocalHeader = (UInt64)value;             
            }
        }

        internal UInt32 DiskNumber
        {
            get
            {
                // we should never be asked to provide such value, if we do not have it  
                Debug.Assert((_zip64ExtraFieldUsage & ZipIOZip64ExtraFieldUsage.DiskNumber) != 0);
                return _diskNumber;
            }
            // set{}  for now we do not need to support set on this property
            // the only reason to set is an attempt to produce multi disk archives 
        }

        internal ZipIOZip64ExtraFieldUsage Zip64ExtraFieldUsage
        {
            get
            {
                return _zip64ExtraFieldUsage;
            }
            set 
            {
                _zip64ExtraFieldUsage = value;
            }
        }

        //------------------------------------------------------
        //
        //  Private Constructor 
        //
        //------------------------------------------------------
        internal ZipIOExtraFieldZip64Element() : base(_constantFieldId)
        {
            _zip64ExtraFieldUsage = ZipIOZip64ExtraFieldUsage.None;
        }

        //------------------------------------------------------
        //
        //  Private Methods 
        //
        //------------------------------------------------------
        private void Validate ()
        {
            // throw if we got any negative values 
            if ((_compressedSize >= Int64.MaxValue) || 
                 (_uncompressedSize >= Int64.MaxValue) || 
                 (_offsetOfLocalHeader >= Int64.MaxValue))
            {
                throw new NotSupportedException("Zip64StructuresTooLarge"); 
            }

            // throw if disk number isn't == 0 
            if (_diskNumber != 0)
            {
                throw new NotSupportedException("NotSupportedMultiDisk");
            }
        }

        //------------------------------------------------------
        //
        //  Private fields 
        //
        //------------------------------------------------------
        private const UInt16 _constantFieldId = 0x01;

        private UInt64 _uncompressedSize;
        private UInt64 _compressedSize;
        private UInt64 _offsetOfLocalHeader;
        private UInt32 _diskNumber;
        
        private ZipIOZip64ExtraFieldUsage _zip64ExtraFieldUsage;
    }
}

            

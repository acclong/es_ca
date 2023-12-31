//-----------------------------------------------------------------------------
//
// <copyright file="ITrackingMemoryStreamFactory.cs" company="Microsoft">
//    Copyright (C) Microsoft Corporation.  All rights reserved.
// </copyright>
//
// Description:
//  This is an internal interface that enables the following channels of communications 
//          a) provides CompressStream and FileItemStream with an ability to construct TrackingMemoryStreams
//          b) it used by the TrackingMemoryStreams, to report their memory usage 
//          b) it used by the Mode Enforcing Streams to report end of operation (that might require an Auto Flush)
//
// History:
//  05/24/2005: IgorBel: Initial creation.
//  11/08/2005: BruceMac: Change namespace
//
//-----------------------------------------------------------------------------

using System;
using System.IO;

namespace esDigitalSignature.OfficePackage
{   
    /// <summary>
    /// ITrackingMemoryStreamFactory interface is used to enable various modules 
    /// to notify that more memory were possibly allocated and 
    /// auto flush may be required 
    /// </summary>
    internal interface ITrackingMemoryStreamFactory
    {
        /// <summary>
        /// This is the equivalent of the default MemoryStream constructor. Callers should treat resulting MemoryStream 
        /// exactly as the System.IO.MemoryStream
        /// </summary>        
        MemoryStream Create();

        /// <summary>
        /// This is the equivalent of the MemoryStream constructor which takes int capacity parameter. 
        /// Callers should treat resulting MemoryStream excatly as the System.Io.MemoryStream
        /// </summary>        
        MemoryStream Create(int capacity);        

        /// <summary>
        /// This function can be safely called from any context it will not result in any significant operation except 
        /// some integer accumulation math. It is ok to call this in a middle of Read/Write/Flush/Seek/SetLength or whatever 
        /// </summary>        
        void ReportMemoryUsageDelta(int delta);

    }
} 

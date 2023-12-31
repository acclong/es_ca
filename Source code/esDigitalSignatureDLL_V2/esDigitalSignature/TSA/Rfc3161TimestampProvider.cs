﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using Org.BouncyCastle.Tsp;
using System.IO;
using System.Security.Cryptography;

namespace esDigitalSignature.TSA
{
    /// <summary>
    /// Timestamp provided via teh RFC3161 protocol.
    /// </summary>
    /// <remarks>
    /// Get a timestamp via the HTTP protocol.
    /// </remarks>
    internal class Rfc3161TimestampProvider : ITimestampProvider
    {
        private Uri address;

        /// <summary>
        /// Constuctor that has the Fedict TSA as destination.
        /// </summary>
        /// <remarks>
        /// You may only use this when you have the explicit agreement of Fedict. 
        /// </remarks>
        public Rfc3161TimestampProvider()
        {
            address = new Uri("http://tsa.belgium.be/connect");
        }

        /// <summary>
        /// Constructor that accept the address of the TSA.
        /// </summary>
        /// <param name="address">The url of the TSA</param>
        public Rfc3161TimestampProvider(Uri address)
        {
            this.address = address;
        }

        /// <summary>
        /// Gets a timestamp of the provided address via the RFC3161.
        /// </summary>
        /// <param name="hash">The has to get the timestamp from</param>
        /// <param name="digestMethod">The algorithm used to calculate the hash</param>
        /// <returns>The timestamp token in binary (encoded) format</returns>
        /// <exception cref="WebException">When the TSA returned a http-error</exception>
        /// <exception cref="TspValidationException">When the TSA returns an invalid timestamp response</exception>
        public byte[] GetTimestampFromDocumentHash(byte[] hash, string digestMethod)
        {
            String digestOid = CryptoConfig.MapNameToOID(CryptoConfig.CreateFromName(digestMethod).GetType().ToString());

            TimeStampRequestGenerator tsprg = new TimeStampRequestGenerator();
            tsprg.SetCertReq(true);
            TimeStampRequest tspr = tsprg.Generate(digestOid, hash);
            byte[] tsprBytes = tspr.GetEncoded();

            WebRequest post = WebRequest.Create(address);
            post.ContentType = "application/timestamp-query";
            post.Method = "POST";
            post.ContentLength = tsprBytes.Length;
            WebResponse response = null;
            try 
            {
                using (Stream postStream = post.GetRequestStream())
                {
                    postStream.Write(tsprBytes, 0, tsprBytes.Length);
                } 
                response = post.GetResponse();
            }
            catch (WebException ex)
            {
                return null;
                //throw new CryptographicException("TSA - " + ex.Message);
            }
            if (response.ContentType != "application/timestamp-reply")
            {
                //throw new CryptographicException("TSA - Server response with invalid content type: " + response.ContentType);
                return null;
            }
            Stream responseStream = response.GetResponseStream();

            TimeStampResponse tsResponse = new TimeStampResponse(responseStream);
            tsResponse.Validate(tspr);

            return tsResponse.TimeStampToken.GetEncoded();
        }
    }
}

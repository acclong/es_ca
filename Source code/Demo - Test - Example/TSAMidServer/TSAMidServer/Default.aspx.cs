using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TSAMidServer
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Transfer POST request to main TSA server
            WebRequest tsaPost = WebRequest.Create(ConfigurationManager.AppSettings["TSAServerUri"]);
            tsaPost.ContentType = "application/timestamp-query";
            tsaPost.Method = "POST";
            tsaPost.ContentLength = Request.ContentLength;
            WebResponse tsaResponse = null;
            try 
            {
                //copy input data and get response from TSA server
                using (Stream postStream = tsaPost.GetRequestStream())
                {
                    Request.InputStream.CopyTo(postStream);
                } 
                tsaResponse = tsaPost.GetResponse();

                //Prepare response
                Response.ContentType = tsaResponse.ContentType;
                Response.Clear();
                Response.BufferOutput = true;
                //Copy headers and output data
                foreach (string key in tsaResponse.Headers.AllKeys)
                {
                    if (key != "Server")
                        Response.AddHeader(key, tsaResponse.Headers[key]);
                }
                tsaResponse.GetResponseStream().CopyTo(Response.OutputStream);
                // Send the output to the client.
                Response.Flush();
            }
            catch (WebException ex)
            {
                Response.ContentType = "text/html";
                Response.Clear();
                Response.BufferOutput = true;
                if (ex.Response == null)
                    Response.StatusCode = (int)HttpStatusCode.NotFound;
                else
                    Response.StatusCode = (int)((HttpWebResponse)ex.Response).StatusCode;
                Response.Write(Response.Status);                
                Response.Flush();
            }
                        
        }
    }
}
using System;
using System.IO;
using System.Web;
using System.Drawing;
using EBird.Common;
using EBird.BusinessEntity;
using EBird.DataAccess;
using EBird.DocRepo;


namespace EBird.Web.App.handler
{
    /// <summary>
    /// Summary description for VideoHandler
    /// </summary>
    public class VideoHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            int docID = 0;
            DocumentDA objDocDA = new DocumentDA();
            if (context.Request["ID"] != null)
            {
                docID = Convert.ToInt32(context.Request["ID"]);

                DocAccess objDocAccess = new DocAccess();

                byte[] fileContents = objDocAccess.FileToByteArray(docID);
                if (fileContents != null)
                {
                    context.Response.AppendHeader("Content-Type", "video/mp4");
                    context.Response.AppendHeader("Accept-Ranges", "bytes");
                    context.Response.OutputStream.Write(fileContents, 0, fileContents.Length);
                    context.Response.Flush();
                }
                else
                {
                    context.Response.ContentType = "image/jpeg";
                    if (File.Exists(Constant.DocRepRoot + "blankVideoTN.png"))
                        context.Response.WriteFile(Constant.DocRepRoot + "blankVideoTN.png");
                    else
                        context.Response.WriteFile(Constant.DocRepRoot + "NoDocs.png");
                }
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
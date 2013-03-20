using System;
using System.IO;
using System.Web;
using System.Drawing;
using EBird.Common;
using EBird.BusinessEntity;
using EBird.DataAccess;

namespace EBird.Web.App.handler
{
    /// <summary>
    /// Summary description for showImage
    /// </summary>
    public class showImage : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            int docID = 0;
            DocumentDA objDocDA = new DocumentDA();
            if (context.Request["ID"] != null)
                docID = Convert.ToInt32(context.Request["ID"]);

            context.Response.ContentType = "image/jpeg";
            string rtnFilePath = objDocDA.GetDocumentPath(docID);
            if (rtnFilePath != "")
            {
                if (File.Exists(Constant.DocRepoClient + rtnFilePath))
                    context.Response.WriteFile(Constant.DocRepoClient + rtnFilePath);
                else
                    context.Response.WriteFile(Constant.DocRepRoot + "NoDocs.png");
            }
            else
                context.Response.WriteFile(Constant.DocRepRoot + "NoDocs.png");
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
using System;
using System.IO;
using System.Web;
using System.Drawing;
using System.Collections.Generic;
using EBird.Common;
using EBird.BusinessEntity;
using EBird.DataAccess;

namespace EBird.Web.App.handler
{
    /// <summary>
    /// Summary description for imageResized
    /// </summary>
    public class imageResized : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            int docID = 0;
            bool isResizeRequired = false;

            DocumentDA objDocDA = new DocumentDA();
            if (context.Request["ID"] != null)
                docID = Convert.ToInt32(context.Request["ID"]);
            string rtnFilePath = objDocDA.GetDocumentPath(docID);
             
            if (rtnFilePath != "")
            {
                if (File.Exists(Constant.DocRepoClient + rtnFilePath))
                {
                    rtnFilePath = Constant.DocRepoClient + rtnFilePath;
                    isResizeRequired = true;
                }
                else
                    rtnFilePath = Constant.DocRepRoot + "NoDocs.png";
            }
            else
                rtnFilePath = Constant.DocRepRoot + "NoDocs.png";

            context.Response.ContentType = "image/jpeg";

            if (isResizeRequired)
            {
                List<DocumentBE> objDoc = objDocDA.GetDocumentDA(docID);
                if (objDoc.Count > 0)
                {
                    if (!objDoc[0].isResized)
                    {
                        ImageUtility objImgUtil = new ImageUtility();
                        System.Drawing.Image original = System.Drawing.Image.FromFile(rtnFilePath);
                        System.Drawing.Image resized = objImgUtil.ResizeImage(original, Common.Constant.LogoSize);

                        switch ((Path.GetExtension(objDoc[0].SavedFileName).Substring(1)).ToUpper())
                        {
                            case "PNG":
                                resized.Save(rtnFilePath, System.Drawing.Imaging.ImageFormat.Png);
                                break;
                            case "JPG":
                                resized.Save(rtnFilePath, System.Drawing.Imaging.ImageFormat.Jpeg);
                                break;
                            case "GIF":
                                resized.Save(rtnFilePath, System.Drawing.Imaging.ImageFormat.Gif);
                                break;
                        }
                        objDocDA.SaveDocumentDA(docID, "isResized", "1");
                    }
                }
            }
            
            context.Response.WriteFile(rtnFilePath);
        
            //ImageUtility objImageUtility = new ImageUtility();
            //if (!File.Exists(Server.MapPath(p2)))
            //{
            //    System.Drawing.Image original = System.Drawing.Image.FromFile(Server.MapPath(p1));
            //    System.Drawing.Image resized = objImageUtility.ResizeImage(original, new Size(60, 60));
            //    resized.Save(Server.MapPath(p2), System.Drawing.Imaging.ImageFormat.Png);
            //}
            //return p2;

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
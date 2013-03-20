using System;
using System.IO;
using System.Web;
using System.Drawing;
using EBird.Common;
using EBird.BusinessEntity;
using EBird.DataAccess;

namespace EBird.DocRepo
{
    //public class ImageHandler : IHttpHandler
    //{
    //    /// <summary>
    //    /// You will need to configure this handler in the web.config file of your 
    //    /// web and register it with IIS before being able to use it. For more information
    //    /// see the following link: http://go.microsoft.com/?linkid=8101007
    //    /// </summary>
    //    #region IHttpHandler Members

    //    public bool IsReusable
    //    {
    //        // Return false in case your Managed Handler cannot be reused for another request.
    //        // Usually this would be false in case you have some state information preserved per request.
    //        get { return true; }
    //    }

    //    public void ProcessRequest(HttpContext context)
    //    {
    //        context.Response.Clear();

    //        if (!String.IsNullOrEmpty(context.Request.QueryString["id"]))
    //        {
    //            int id = Int32.Parse(context.Request.QueryString["id"]);

    //            // Now you have the id, do what you want with it, to get the right image 
    //            // More than likely, just pass it to the method, that builds the image 
    //            Image image = GetImage(id);

    //            // Of course set this to whatever your format is of the image 
    //            context.Response.ContentType = "image/jpeg";
    //            // Save the image to the OutputStream 
    //            image.Save(context.Response.OutputStream, ImageFormat.Jpeg);
    //        }
    //        else
    //        {
    //            context.Response.ContentType = "text/html";
    //            context.Response.Write("<p>Need a valid id</p>");
    //        }
    //    }

    //    private Image GetImage(int id)
    //    {
    //        // Not sure how you are building your MemoryStream 
    //        // Once you have it, you just use the Image class to  
    //        // create the image from the stream. 
    //        MemoryStream stream = new MemoryStream();
    //        return Image.FromStream(stream);
    //    } 

    //    #endregion
    //}

    public class Thumbnail : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            DocumentDA objDocDA = new DocumentDA();
            string rtnFilePath = objDocDA.GetUserProfileImage(Convert.ToInt32(context.Session[""]));  
            if (rtnFilePath != "")
            {
                if (File.Exists(Constant.DocRepRoot + rtnFilePath))
                {
                    context.Response.ContentType = "image/jpeg";
                    context.Response.WriteFile(Constant.DocRepRoot + rtnFilePath);
                }
                else
                    throw new HttpException(404, "Invalid photo name.");
            }
        }
        
        public bool IsReusable
        {
            get { return true; }
        }
    }

    public class MyProfilePhoto : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            DocumentDA objDocDA = new DocumentDA();
            string rtnFilePath = objDocDA.GetUserProfileImage(Convert.ToInt32(context.Session[""]));
            if (rtnFilePath != "")
            {
                if (File.Exists(Constant.DocRepRoot + rtnFilePath))
                {
                    context.Response.ContentType = "image/jpeg";
                    context.Response.WriteFile(Constant.DocRepRoot + rtnFilePath);
                }
                else
                    throw new HttpException(404, "Invalid photo name.");
            }
        }

        public bool IsReusable
        {
            get { return true; }
        }
    }

}

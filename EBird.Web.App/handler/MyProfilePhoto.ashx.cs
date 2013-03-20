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
    /// Summary description for MyProfilePhoto
    /// </summary>
    public class MyProfilePhoto : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            int usrID = 0;
            int pID = 0;
            string rtnFilePath = string.Empty;

            DocumentDA objDocDA = new DocumentDA();
            if (context.Request["ID"] != null)
                usrID = Convert.ToInt32(context.Request["ID"]);
            else if (context.Request["pID"] != null)
                pID = Convert.ToInt32(context.Request["pID"]);

            context.Response.ContentType = "image/jpeg";
            if (pID != 0)
                rtnFilePath = objDocDA.GetPresenterProfileImage(pID);
            else
                rtnFilePath = objDocDA.GetUserProfileImage(usrID);

            if (rtnFilePath != string.Empty)
            {
                if (File.Exists(Constant.DocRepoClient + rtnFilePath))
                    context.Response.WriteFile(Constant.DocRepoClient + rtnFilePath);
                else
                    context.Response.WriteFile(Constant.DocRepRoot + "profileImgMissing.png");
            }
            else
                context.Response.WriteFile(Constant.DocRepRoot + "profileImg.png");
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
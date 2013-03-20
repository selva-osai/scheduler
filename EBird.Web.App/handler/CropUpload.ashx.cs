using System;
using System.Web;
using Telerik.Web.UI;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Data.SqlClient;
using System.IO;
using EBird.DocRepo;
using EBird.BusinessEntity;
using EBird.DataAccess;
using System.Web.UI.WebControls;


namespace EBird.Web.App.handler
{
    /// <summary>
    /// Summary description for CropUpLoad
    /// </summary>

    public class CropUpLoad : AsyncUploadHandler, System.Web.SessionState.IRequiresSessionState
    {
        protected override IAsyncUploadResult Process(UploadedFile file, HttpContext context, IAsyncUploadConfiguration configuration, string tempFileName)
        {
            telerikAsyncUploadResult result = CreateDefaultUploadResult<telerikAsyncUploadResult>(file);
            string folderType = "temp";
            DocAccess objDoc = new DocAccess();

            result.DocumentID = 0;
            result.DocumentID = objDoc.saveFiles(file, folderType, Convert.ToInt32(context.Session["ClientID"]), Convert.ToInt32(context.Session["UserID"]), 0, Convert.ToInt32(context.Session["WebinarID"]));
            return result;
        }
    }
}
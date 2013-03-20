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
    /// Summary description for VideoUploadT1
    /// </summary>
   
    public class VideoUploadT1 : AsyncUploadHandler, System.Web.SessionState.IRequiresSessionState
    {

        protected override IAsyncUploadResult Process(UploadedFile file, HttpContext context, IAsyncUploadConfiguration configuration, string tempFileName)
        {
            // Call the base Process method to save the file to the temporary folder
            //base.Process(file, context, configuration, tempFileName);

            // Populate the default (base) result into an object of type telerikAsyncUploadResult
            telerikAsyncUploadResult result = CreateDefaultUploadResult<telerikAsyncUploadResult>(file);

            int userID = -1;
            int clientID = -1;
            string folderType = string.Empty;
            DocAccess objDoc = new DocAccess();
            //Follwoing is commented as the custom config values are not getting passed.
            //telerikUploadConfig getConfig = configuration as telerikUploadConfig;
            //if (getConfig != null)
            //{
            //    userID = getConfig.ActionID;
            //    clientID = getConfig.ClientID;
            //    folderType = getConfig.FolderType;
            //    result.DocumentID = objDoc.saveFiles(file, folderType, clientID, userID);
            //}
            result.DocumentID = 0;
            if (context.Request["uid"] != null)
                userID = 1;
            string refURL = context.Request.UrlReferrer.ToString().ToUpper(); 
            if (refURL.IndexOf("BIO") > 0)
                folderType = "PROFILE";
            if (refURL.IndexOf("AUDIFEATURE_CONTENT") > 0)
                folderType = "PRESENTATION";
            if (refURL.IndexOf("SNAPSITE") > 0)
                folderType = "SS";

            if (folderType == string.Empty)
            {
                if (context.Request["ftyp"] != null)
                {
                    if (Convert.ToString(context.Request["ftyp"]) == "u")
                        folderType = "PROFILE";
                    else
                        folderType = "PRESENTATION";
                }
                else if (context.Session["FolderType"] != null)
                    folderType = Convert.ToString(context.Session["FolderType"]);
            }

            if (folderType != string.Empty)
            {
                if (userID == -1)
                    result.DocumentID = objDoc.saveFiles(file, folderType, Convert.ToInt32(context.Session["ClientID"]), Convert.ToInt32(context.Session["UserID"]), 0, Convert.ToInt32(context.Session["WebinarID"]));
                else
                    result.DocumentID = objDoc.saveFiles(file, folderType, Convert.ToInt32(context.Session["ClientID"]), Convert.ToInt32(context.Session["UserID"]), Convert.ToInt32(context.Session["UserID"]), 0);
                //if (folderType != "PRESENTATION")
                //{
                //    DocAccess objDocAccess = new DocAccess();
                //    objDocAccess.resizeImage(result.DocumentID, Convert.ToInt32(context.Session["ClientID"]));
                //}
            }
            return result;
        }

        public int InsertImage(UploadedFile file, int userID)
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["TelerikConnectionString35"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string cmdText = "INSERT INTO AsyncUploadImages VALUES(@ImageData, @ImageName, @UserID) SET @Identity = SCOPE_IDENTITY()";
                SqlCommand cmd = new SqlCommand(cmdText, conn);

                byte[] imageData = GetImageBytes(file.InputStream);

                SqlParameter identityParam = new SqlParameter("@Identity", SqlDbType.Int, 0, "ImageID");
                identityParam.Direction = ParameterDirection.Output;

                cmd.Parameters.AddWithValue("@ImageData", imageData);
                cmd.Parameters.AddWithValue("@ImageName", file.GetName());
                cmd.Parameters.AddWithValue("@UserID", userID);

                cmd.Parameters.Add(identityParam);

                conn.Open();
                cmd.ExecuteNonQuery();

                return (int)identityParam.Value;
            }
        }

        public byte[] GetImageBytes(Stream stream)
        {
            byte[] buffer;

            using (Bitmap image = ResizeImage(stream))
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    image.Save(ms, ImageFormat.Jpeg);

                    //return the current position in the stream at the beginning
                    ms.Position = 0;

                    buffer = new byte[ms.Length];
                    ms.Read(buffer, 0, (int)ms.Length);
                    return buffer;
                }
            }
        }

        public Bitmap ResizeImage(Stream stream)
        {
            System.Drawing.Image originalImage = Bitmap.FromStream(stream);

            int height = 500;
            int width = 500;

            double ratio = Math.Min(originalImage.Width, originalImage.Height) / (double)Math.Max(originalImage.Width, originalImage.Height);

            if (originalImage.Width > originalImage.Height)
            {
                height = Convert.ToInt32(height * ratio);
            }
            else
            {
                width = Convert.ToInt32(width * ratio);
            }

            Bitmap scaledImage = new Bitmap(width, height);

            using (Graphics g = Graphics.FromImage(scaledImage))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.DrawImage(originalImage, 0, 0, width, height);

                return scaledImage;
            }

        }

    }
}
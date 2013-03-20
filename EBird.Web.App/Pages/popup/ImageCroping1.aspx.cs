using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using EBird.BusinessEntity;
using EBird.Common;
using EBird.DataAccess;
using EBird.DocRepo;
using Telerik.Web.UI;
using SD = System.Drawing;
using System.Collections;

namespace EBird.Web.App.Pages.popup
{
    public partial class ImageCroping1 : System.Web.UI.Page
    {
        WebinarDA objWebinarDA = new WebinarDA();
        EBirdUtility objUtil = new EBirdUtility();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (Request["ID"] != null)
                {
                    hWebinarID.Value = Request["ID"].ToString();
                    //setImageForCroping();
                    popImages();
                }
            }
        }

        private void popImages()
        {
            List<WebinarResource> objRes = objWebinarDA.getRegFormResoures(Convert.ToInt32(hWebinarID.Value));
            rpLogoThumb.DataSource = objRes;
            rpLogoThumb.DataBind();

        }

        private void setImageForCroping(int imgDocID)
        {
            DocumentDA objDocDA = new DocumentDA();
            List<DocumentBE> objDocBE = objDocDA.GetDocumentDA(imgDocID);
            if (objDocBE.Count > 0)
            {
                imgCrop.ImageUrl = "~/handler/showImage.ashx?ID=" + Convert.ToString(imgDocID);
                hUploadedFName.Value = objDocBE[0].SavedFileName;
                //imgCrop.ImageUrl = Constant.ClientURL + objDocBE[0].ClientID.ToString() + "/logo/" + objDocBE[0].SavedFileName;
                hImgType.Value = objDocBE[0].Category;
            }
        }

        protected void btnCrop_Click(object sender, EventArgs e)
        {
            string ImageName = hUploadedFName.Value;
            int w = Convert.ToInt32(W.Value);
            int h = Convert.ToInt32(H.Value);
            int x = Convert.ToInt32(X.Value);
            int y = Convert.ToInt32(Y.Value);

            string path = Constant.DocRepoClient + Session["ClientID"].ToString() + "\\temp\\";

            byte[] CropImage = Crop(path + ImageName, w, h, x, y);
            using (MemoryStream ms = new MemoryStream(CropImage, 0, CropImage.Length))
            {
                ms.Write(CropImage, 0, CropImage.Length);
                using (SD.Image CroppedImage = SD.Image.FromStream(ms, true))
                {
                    string SaveTo = path + "crop" + ImageName;
                    CroppedImage.Save(SaveTo, CroppedImage.RawFormat);
                    //dvCropPanel.Visible = false;
                    //pnlCropped.Visible = true;
                    imgCropped.ImageUrl = Constant.ClientURL + Session["ClientID"].ToString() + "/temp/crop" + ImageName;
                }
            }
        }

        static byte[] Crop(string Img, int Width, int Height, int X, int Y)
        {
            try
            {
                using (SD.Image OriginalImage = SD.Image.FromFile(Img))
                {
                    using (SD.Bitmap bmp = new SD.Bitmap(Width, Height))
                    {
                        bmp.SetResolution(OriginalImage.HorizontalResolution, OriginalImage.VerticalResolution);
                        using (SD.Graphics Graphic = SD.Graphics.FromImage(bmp))
                        {
                            Graphic.SmoothingMode = SmoothingMode.AntiAlias;
                            Graphic.InterpolationMode = InterpolationMode.HighQualityBicubic;
                            Graphic.PixelOffsetMode = PixelOffsetMode.HighQuality;
                            Graphic.DrawImage(OriginalImage, new SD.Rectangle(0, 0, Width, Height), X, Y, Width, Height, SD.GraphicsUnit.Pixel);
                            MemoryStream ms = new MemoryStream();
                            bmp.Save(ms, OriginalImage.RawFormat);
                            return ms.GetBuffer();
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                throw (Ex);
            }
        }

        public void aupLogo_FileUploaded(object sender, FileUploadedEventArgs e)
        {
            telerikAsyncUploadResult result = e.UploadResult as telerikAsyncUploadResult;
            if (result != null)
            {
                if (result.DocumentID > 0)
                {
                    Session["CropImgID"] = result.DocumentID;

                    //WebinarResource objWebinarResource = new WebinarResource();
                    //objWebinarResource.WebinarID = Convert.ToInt32(hWebinarID.Value);
                    //objWebinarResource.DocID = result.DocumentID;
                    //objWebinarResource.ResourceType = (rcmbHeader.SelectedValue == "Banner" ? "Banner" : "Logo");
                    //objWebinarResource.ResourceTitle = e.File.FileName;
                    //objWebinarResource.ResourceValue = "";
                    //objWebinarResource.IsBriefcase = false;
                    //objWebinarDA.InsertWebinarResources(objWebinarResource);
                }
            }
        }

        protected void lnkUpdateLogo_Click(object sender, EventArgs e)
        {
            // setImageForCroping();
        }

        protected void rpLogoThumb_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                ImageButton img1 = (ImageButton)e.Item.FindControl("imgThumb");
                if (img1 != null)
                {
                    img1.ImageUrl = "~/handler/showImage.ashx?ID=" + Convert.ToString(DataBinder.Eval(e.Item.DataItem, "DocID"));
                    img1.CommandArgument = Convert.ToString(DataBinder.Eval(e.Item.DataItem, "DocID")) + ":" + Convert.ToString(DataBinder.Eval(e.Item.DataItem, "ResourceTitle"));
                    if (Convert.ToString(DataBinder.Eval(e.Item.DataItem, "ResourceType")) == "Logo")
                        img1.Width = (Unit)(120);
                    else
                        img1.Width = (Unit)(300);
                }
            }
        }

        protected void rpLogoThumb_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandArgument != "")
            {
                ArrayList arr = objUtil.StringToArrayList(e.CommandArgument.ToString(), new char[] { ':' });
                if (arr.Count > 1)
                {
                    imgCrop.ImageUrl = "~/handler/showImage.ashx?ID=" + arr[0].ToString();
                    hUploadedFName.Value = arr[1].ToString();
                }
            }
        }

    }
}
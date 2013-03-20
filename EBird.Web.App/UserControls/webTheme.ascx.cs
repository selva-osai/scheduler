using System;
using System.Collections.Generic;
using System.IO;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using EBird.BusinessEntity;
using EBird.Common;
using EBird.DataAccess;
using EBird.DocRepo;
using Telerik.Web.UI;

namespace EBird.Web.App.UserControls
{
    public partial class webTheme : System.Web.UI.UserControl
    {
        WebinarBE objWebinarBE = new WebinarBE();
        EBirdUtility objUtil = new EBirdUtility();
        WebinarDA objWebinarDA = new WebinarDA();
        EBErrorMessages objError = new EBErrorMessages();
        static private int _logoCount;
        public string WebinarID
        {
            get
            {
                return hWebinarID.Value;
            }
            set
            {
                hWebinarID.Value = value;
            }
        }

        static public int logoCount
        {
            get
            {
                return _logoCount;
            }
            set
            {
                _logoCount = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                rcmbHeader.Text = "-Select Header-";
                getThemeDetails();
                getThemeLayouts();
                setUploadConfigvalues();
                //aupLogo.HttpHandlerUrl = "~/handler/VideoUploadT1.ashx";
            }
        }

        private void getThemeLayouts()
        {
            DirectoryInfo objDir = new DirectoryInfo(System.Web.HttpContext.Current.Server.MapPath("~/") + "Images\\Theme");
            rpThemeCarousel.DataSource = objDir.GetFiles("layout*.png"); 
            rpThemeCarousel.DataBind();  
        }

        protected string getThemeID(object layoutFileName)
        {
            string FName = layoutFileName.ToString();
            FName = FName.Replace(".png", "");
            FName = FName.Replace("layout", "");
            return FName;
        }

        private void setUploadConfigvalues()
        {
            telerikUploadConfig config = aupLogo.CreateDefaultUploadConfiguration<telerikUploadConfig>();
            // Populate any additional fields
            config.ActionID = Convert.ToInt32(Session["UserID"]);
            config.ClientID = Convert.ToInt32(Session["ClientID"]);
            config.FolderType = "LOGO";
            // The upload configuration will be available in the handler
            aupLogo.UploadConfiguration = config;
            //since config calue is not getting passed, the foldertype also pass thru session value
            Session["FolderType"] = "LOGO";
        }

        public void saveWebTheme()
        {
            WebinarTheme objTheme = new WebinarTheme();
            //objTheme.PriThemeColor = System.Drawing.ColorTranslator.ToHtml(rcolorPri.SelectedColor);
            //objTheme.SecThemeColor = System.Drawing.ColorTranslator.ToHtml(rcolorSec.SelectedColor);
            //objTheme.ThemeFontName = txtFontName.Text;
            objTheme.ThemeLayoutID  = Convert.ToInt32(hSelThemeID.Value);
            objTheme.WebinarID = Convert.ToInt32(hWebinarID.Value);  
            objWebinarDA.SaveWebinarTheme(objTheme);

        }

        private void getThemeDetails()
        {
            if (hWebinarID.Value != "")
            {
                List<WebinarTheme> objWebTheme = objWebinarDA.getWebinarTheme(Convert.ToInt32(hWebinarID.Value));
                if (objWebTheme.Count > 0)
                {
                    rcmbHeader.SelectedValue = objWebTheme[0].HeaderType;
                    hSelThemeID.Value = objWebTheme[0].ThemeLayoutID.ToString();  
                    //System.Drawing.Color c1;
                    //if (objWebTheme[0].PriThemeColor != "")
                    //{
                    //    c1 = System.Drawing.ColorTranslator.FromHtml(objWebTheme[0].PriThemeColor);
                    //    rcolorPri.SelectedColor = c1;
                    //}
                    //if (objWebTheme[0].SecThemeColor != "")
                    //{
                    //    c1 = System.Drawing.ColorTranslator.FromHtml(objWebTheme[0].SecThemeColor);
                    //    rcolorSec.SelectedColor = c1;
                    //}
                    //txtFontName.Text = objWebTheme[0].ThemeFontName;

                    popLogo();
                }
            }
        }

        private void popLogo()
        {
            List<WebinarResource> objRes = objWebinarDA.getRegFormResoures(Convert.ToInt32(hWebinarID.Value));
            if (objRes.Count > 0)
            {
                logocanvas.Visible = true;
                rpLogo.DataSource = objRes;
                rpLogo.DataBind();
            }
            _logoCount = objRes.Count;
            setMaxFileCount();
        }

        private void showPicture(int docID, int imgCount, string imgType)
        {
            //RadBinaryImage rimg = (RadBinaryImage)this.FindControl("Thumbnail" + imgCount.ToString());
            RadBinaryImage rimg = (RadBinaryImage)this.FindControl("Thumbnail1");
            if (rimg != null)
            {
                if (imgType.ToUpper() == "LOGO")
                {
                    rimg.Width = Unit.Pixel(200);
                    rimg.Height = Unit.Pixel(150);
                }
                else
                {
                    rimg.Width = Unit.Pixel(750);
                    rimg.Height = Unit.Pixel(180);
                }
                DocAccess objDocAccess = new DocAccess();
                rimg.DataValue = objDocAccess.FileToByteArray(docID);
            }
        }

        private void updateHeader(string headerType)
        {
            int docID = 0;
            DocAccess objDocAccess = new DocAccess();
            WebinarDA objWebinarDA = new WebinarDA();
            switch (headerType)
            {
                case "Logo1":
                    docID = objDocAccess.saveFiles(Request, "Logo", Convert.ToInt32(Session["ClientID"]), Convert.ToInt32(Session["UserID"]));
                    objWebinarDA.SaveWebinarTheme("Logo1", docID, Convert.ToInt32(hWebinarID.Value));
                    break;
                case "Logo2":
                    docID = objDocAccess.saveFiles(Request, "Logo", Convert.ToInt32(Session["ClientID"]), Convert.ToInt32(Session["UserID"]));
                    objWebinarDA.SaveWebinarTheme("Logo2", docID, Convert.ToInt32(hWebinarID.Value));
                    break;
                case "Banner":
                    docID = objDocAccess.saveFiles(Request, "Logo", Convert.ToInt32(Session["ClientID"]), Convert.ToInt32(Session["UserID"]));
                    objWebinarDA.SaveWebinarTheme("BANNER", docID, Convert.ToInt32(hWebinarID.Value));
                    break;
            }
            getThemeDetails();
        }

        protected void rpLogo_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {            
                string DocID = ((HiddenField)e.Item.FindControl("hDocID")).Value;
                string ResourceType = ((HiddenField)e.Item.FindControl("hResourceType")).Value;
                Image rimg = ((Image)e.Item.FindControl("imgLogo"));

                //RadBinaryImage rimg = (RadBinaryImage)e.Item.FindControl("Thumbnail1");
                if (rimg != null)
                {
                    if (ResourceType.ToUpper() == "LOGO")
                    {
                        rimg.Width = Unit.Pixel(130);
                        rimg.Height = Unit.Pixel(100);
                    }
                    else
                    {
                        rimg.Width = Unit.Pixel(700);
                        rimg.Height = Unit.Pixel(110);
                    }
                    rimg.ImageUrl = "~/handler/showImage.ashx?ID=" + DocID;
                    //DocAccess objDocAccess = new DocAccess();
                    //rimg.DataValue = objDocAccess.FileToByteArray(Convert.ToInt32(DocID));
                }
            }

        }

        //protected void aupLogo_FileUploaded(object sender, FileUploadedEventArgs e)
        //{
        //    RadBinaryImage rimg = (RadBinaryImage)this.FindControl("Thumbnail" + hCurrentImg.Value);
        //    if (rimg != null)
        //    {
        //        rimg.Width = Unit.Pixel(200);
        //        rimg.Height = Unit.Pixel(150);
        //        using (Stream stream = e.File.InputStream)
        //        {
        //            byte[] imageData = new byte[stream.Length];
        //            stream.Read(imageData, 0, (int)stream.Length);
        //            rimg.DataValue = imageData;
        //        }
        //        int curr = Convert.ToInt32(hCurrentImg.Value);
        //        if (curr < 4)
        //        {
        //            hCurrentImg.Value = (curr + 1).ToString();
        //            if (curr >= 4)
        //                aupLogo.Visible = false; 
        //        }
        //    }
        //}

        public void aupLogo_FileUploaded(object sender, FileUploadedEventArgs e)
        {
            telerikAsyncUploadResult result = e.UploadResult as telerikAsyncUploadResult;
            if (result != null)
            {
                if (result.DocumentID > 0)
                {
                    int idx = Convert.ToInt32(hCurrentImg.Value);
                    if (rcmbHeader.SelectedIndex != 1)
                        idx = 1;
                    WebinarResource objWebinarResource = new WebinarResource();
                    objWebinarResource.WebinarID = Convert.ToInt32(hWebinarID.Value);
                    objWebinarResource.DocID = result.DocumentID;
                    objWebinarResource.ResourceType = rcmbHeader.SelectedValue;
                    objWebinarResource.ResourceOrder = idx;
                    objWebinarDA.InsertRegFormResources(objWebinarResource);
                    ++idx;
                    hCurrentImg.Value = idx.ToString();
                }
            }
        }

        protected void rcmbHeader_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            hCurrentImg.Value = "1";
            setMaxFileCount();
            //rcmbHeader.SelectedValue = e.Value ;
            //rcmbHeader.Text = e.Text;
        }

        private void setMaxFileCount()
        {
            switch (rcmbHeader.SelectedIndex)
            {
                case 0:
                    aupLogo.MaxFileInputsCount = 1 - _logoCount;
                    break;
                case 1:
                    aupLogo.MaxFileInputsCount = Constant.MaxLogoCount - _logoCount;
                    break;
                case 2:
                    aupLogo.MaxFileInputsCount = 1 - _logoCount;
                    break;
            }
        }

        protected void rpLogo_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandArgument != "")
            {
                objWebinarDA.DeleteRegFormResources(Convert.ToInt32(hWebinarID.Value), Convert.ToInt32(e.CommandArgument));
                DocAccess objDoc = new DocAccess();
                objDoc.removeDocumentFromRepositary(Convert.ToInt32(e.CommandArgument), Convert.ToInt32(Session["ClientID"]), "Logo");
                popLogo();
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using EBird.BusinessEntity;
using EBird.Common;
using EBird.DataAccess;
using EBird.DocRepo;
using Telerik.Web.UI;

namespace EBird.Web.App.UserControls
{
    public partial class mysnapsite : System.Web.UI.UserControl
    {
        WebinarBE objWebinarBE = new WebinarBE();
        EBirdUtility objUtil = new EBirdUtility();
        WebinarDA objWebinarDA = new WebinarDA();
        SnapSiteDA objSnapSiteDA = new SnapSiteDA();

        EBErrorMessages objError = new EBErrorMessages();
        static int ImgID;
        bool boolCampTrackerEmail = false;

        static private int _logoCount;

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
                popPublishWebinars();
                InitDataPopulation();
                hlnkSS.NavigateUrl = "/Pages/MySnapSite/" + Session["UserID"].ToString();
            }
        }

        private void InitDataPopulation()
        {
            getSSDetail();
            getThemeLayouts();
        }

        protected void rcmbHeader_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            hCurrentImg.Value = "1";
            setMaxFileCount();
        }

        private void setMaxFileCount()
        {
            switch (rcmbHeader.SelectedIndex)
            {
                //case 0:
                //    aupLogo.MaxFileInputsCount = 1 - _logoCount;
                //    break;
                case 0:
                    aupLogo.MaxFileInputsCount = Constant.MaxLogoCount - _logoCount;
                    Session["FolderType"] = "LOGO";
                    break;
                case 1:
                    aupLogo.MaxFileInputsCount = 1 - _logoCount;
                    Session["FolderType"] = "BANNER";
                    break;
            }

            if (aupLogo.MaxFileInputsCount <= 0)
                aupLogo.Visible = false;
            else
                aupLogo.Visible = true;
        }

        public void aupLogo_FileUploaded(object sender, FileUploadedEventArgs e)
        {
            telerikAsyncUploadResult result = e.UploadResult as telerikAsyncUploadResult;
            if (result != null)
            {
                if (result.DocumentID > 0)
                {
                    WebinarResource objWebinarResource = new WebinarResource();
                    objWebinarResource.WebinarID = Convert.ToInt32(hWebinarID.Value);
                    objWebinarResource.DocID = result.DocumentID;
                    objWebinarResource.ResourceType = (rcmbHeader.SelectedValue == "BANNER" ? "BANNER" : "Logo");
                    objWebinarResource.ResourceTitle = e.File.FileName;
                    objWebinarResource.ResourceValue = "";
                    objWebinarResource.IsBriefcase = false;
                    objWebinarDA.InsertWebinarResources(objWebinarResource);
                }
            }
        }

        protected void rpLogo_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                string DocID = ((HiddenField)e.Item.FindControl("hDocID")).Value;
                string ResourceType = ((HiddenField)e.Item.FindControl("hResourceType")).Value;
                setResizedLogoURL(Convert.ToInt32(DocID));

                HtmlGenericControl dvL = (HtmlGenericControl)e.Item.FindControl("dvLogoContainer");
                //HtmlGenericControl dvB = (HtmlGenericControl)e.Item.FindControl("dvBannerContainer");

                //Image rimg1 = ((Image)e.Item.FindControl("imgBanner"));
                Image rimg = ((Image)e.Item.FindControl("imgLogo"));
                //if (ResourceType.ToUpper() == "BANNER")
                //{
                //    dvB.Visible = true;
                //    dvL.Visible = false;
                //    if (rimg1 != null)
                //    {
                //        rimg1.ImageUrl = "~/handler/showImage.ashx?ID=" + DocID;
                //        rimg1.Attributes.Add("Name", DocID);
                //        hdynImgIDs.Value += ImgID + ",";
                //        ++ImgID;
                //    }
                //}
                //else
                //{
                //  dvB.Visible = false;
                dvL.Visible = true;
                if (rimg != null)
                {
                    rimg.ImageUrl = "~/handler/showImage.ashx?ID=" + DocID;
                    rimg.Attributes.Add("Name", DocID);
                    hdynImgIDs.Value += ImgID + ",";
                    ++ImgID;
                }
                //}
            }
        }

        private void setResizedLogoURL(int docID)
        {
            bool isResizeRequired = false;
            DocumentDA objDocDA = new DocumentDA();
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

            if (isResizeRequired)
            {
                List<DocumentBE> objDoc = objDocDA.GetDocumentDA(docID);
                if (objDoc.Count > 0)
                {
                    if (!objDoc[0].isResized)
                    {

                        ImageUtility objImgUtil = new ImageUtility();
                        System.Drawing.Image original = System.Drawing.Image.FromFile(rtnFilePath);
                        System.Drawing.Image resized = objImgUtil.ResizeImage(original, (objDoc[0].Category.ToUpper() == "LOGO" ? Common.Constant.LogoSize : Common.Constant.BannerSize));
                        string SavedFileName = "";

                        switch ((Path.GetExtension(objDoc[0].SavedFileName).Substring(1)).ToUpper())
                        {
                            case "PNG":
                                SavedFileName = docID.ToString() + "_rs.png";
                                resized.Save(Constant.DocRepoClient + Session["ClientID"].ToString() + "\\Logo\\" + SavedFileName, System.Drawing.Imaging.ImageFormat.Png);
                                break;
                            case "JPG":
                                SavedFileName = docID.ToString() + "_rs.jpg";
                                resized.Save(Constant.DocRepoClient + Session["ClientID"].ToString() + "\\Logo\\" + SavedFileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                                break;
                            case "GIF":
                                SavedFileName = docID.ToString() + "_rs.gif";
                                resized.Save(Constant.DocRepoClient + Session["ClientID"].ToString() + "\\Logo\\" + SavedFileName, System.Drawing.Imaging.ImageFormat.Gif);
                                break;
                        }
                        objDocDA.SaveDocumentDA(docID, "isResized", "1");
                        objDocDA.SaveDocumentDA(docID, "SavedFileName", "'" + SavedFileName + "'");
                    }
                }
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

        private void popLogo()
        {
            string headerType = rcmbHeader.SelectedValue;
            List<WebinarResource> objRes = objWebinarDA.getRegFormResoures(Convert.ToInt32(hWebinarID.Value));
            if (objRes.Count > 0)
            {
                hdynImgIDs.Value = "";
                ImgID = 0;
                logocanvas.Visible = true;
            }
            else
                rcmbHeader.SelectedValue = "Logo";
            if (headerType.ToUpper() == "LOGO")
            {
                phLogo.Visible = true;
                phBanner.Visible = false;
                rpLogo.DataSource = objRes;
                rpLogo.DataBind();
            }
            else
            {
                phLogo.Visible = false;
                phBanner.Visible = true;
                imgBanner.ImageUrl = "~/handler/showImage.ashx?ID=" + objRes[0].DocID.ToString();
                hBannerDocID.Value = objRes[0].DocID.ToString();
            }
            _logoCount = objRes.Count;
            hLogoCount.Value = _logoCount.ToString();
            setMaxFileCount();
        }

        protected void lnkUpdateLogo_Click(object sender, EventArgs e)
        {
            #region theme, header type
            popLogo();
            #endregion
        }

        protected void rpThemeCarousel_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                HiddenField hFld = ((HiddenField)e.Item.FindControl("hThemeLayoutID"));
                if (hFld != null)
                {
                    string thID = getThemeID(hFld.Value);
                    HtmlImage hImg = ((HtmlImage)e.Item.FindControl("imgTheme"));
                    hImg.Src = "~/Images/Theme/" + hFld.Value;
                    hImg.Attributes.Add("data-themeurl", thID);

                    if (thID == hSelThemeID.Value)
                    {
                        hImg.Attributes.Remove("class");
                        hImg.Attributes.Add("class", "previewBtn selected");
                    }
                }
            }
        }

        protected string getThemeID(object layoutFileName)
        {
            string FName = layoutFileName.ToString();
            FName = FName.Replace(".png", "");
            FName = FName.Replace("layout", "");
            return FName;
        }

        private void getThemeLayouts()
        {
            DirectoryInfo objDir = new DirectoryInfo(System.Web.HttpContext.Current.Server.MapPath("~/") + "Images\\Theme");
            FileInfo[] fi = objDir.GetFiles("layout*.png");
            FileInfo[] fi1 = objDir.GetFiles("layout*.png");

            for (int idx = 0; idx < fi.Length; idx++)
            {
                if (getThemeID(fi[idx].Name) == hSelThemeID.Value)
                {
                    int nxSt = 0;
                    for (int idx1 = idx; idx1 < fi.Length; idx1++)
                    {
                        fi1[nxSt] = fi[idx1];
                        nxSt++;
                    }
                    for (int idx1 = 0; idx1 < idx; idx1++)
                    {
                        fi1[nxSt] = fi[idx1];
                        nxSt++;
                    }
                    break;
                }
            }
            rpThemeCarousel.DataSource = fi1;
            rpThemeCarousel.DataBind();
        }

        private void getSSDetail()
        {
            List<SnapSiteBO> objSS = objSnapSiteDA.GetMySnapSiteDetailsDA(Convert.ToInt32(Session["UserID"]));
            if (objSS.Count > 0)
            {
                rcmbHeader.SelectedValue = objSS[0].HeaderType;
                txtSSTitle.Text = objSS[0].Title;
                redtDesc.Content = objSS[0].Description;
                hSelThemeID.Value = objSS[0].SkinID.ToString();
                chkDisableSS.Checked = !objSS[0].IsEnabled;
                chkFB.Checked = objSS[0].IsFacebook;
                chkTwit.Checked = objSS[0].IsTwitter;
                chkLIn.Checked = objSS[0].IsLinkedIn;
            }
        }
        /* Public Webinar */

        private void popPublishWebinars()
        {
            List<WebinarBE> objWebinarBE = new List<WebinarBE>();
            //if (Session["Role"].ToString() == "Admin")
            //    objWebinarBE = objWebinarDA.GetMyCompanyRecycleWebinarListDA(Convert.ToInt32(Session["ClientID"]));
            //else
            objWebinarBE = objSnapSiteDA.GetMyPublicWebinarListDA(Convert.ToInt32(Session["UserID"]));
            tgrdWebinarList.DataSource = objWebinarBE;
            tgrdWebinarList.DataBind();
            if (objWebinarBE.Count > 0)
                btnPublish.Enabled = true;
            else
                btnPublish.Enabled = false;
        }

        protected void tgrdWebinarList_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "RebindGrid":
                    popPublishWebinars();
                    tgrdWebinarList.MasterTableView.Rebind();
                    break;
                case "Sort":
                    popPublishWebinars();
                    tgrdWebinarList.MasterTableView.Rebind();
                    break;
                case "Page":
                    popPublishWebinars();
                    tgrdWebinarList.MasterTableView.Rebind();
                    break;
            }
        }

        protected void tgrdWebinarList_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            string dtValue = "";
            string sTime = "";
            if (e.Item is GridDataItem)
            {
                GridDataItem dataBoundItem = e.Item as GridDataItem;

                Label lbl = (Label)e.Item.FindControl("lblDateTime");
                dtValue = dataBoundItem["startDate"].Text;
                sTime = dataBoundItem["StartTime"].Text;
                //eTime = dataBoundItem["EndTime"].Text;
                lbl.Text = Convert.ToDateTime(dtValue).ToString("MMM dd, yyyy") + " - " + Convert.ToDateTime(sTime).ToString("h:mm tt");

                if (Convert.ToBoolean(dataBoundItem["isSSPublished"].Text))
                {
                    CheckBox chk = (CheckBox)dataBoundItem.FindControl("chkPublish");
                    chk.Checked = true;
                }
            }
        }

        protected void btnPublish_Click(object sender, EventArgs e)
        {
            HiddenField hFld;
            CheckBox chk1;
            string pubList = string.Empty;
            string unpubList = string.Empty;
            SnapSiteBO objSS = new SnapSiteBO();
            objSS.HeaderType = rcmbHeader.SelectedValue;
            objSS.Title = txtSSTitle.Text;
            objSS.Description = redtDesc.Content;
            objSS.SkinID = Convert.ToInt32(hSelThemeID.Value);
            objSS.IsEnabled = !chkDisableSS.Checked;
            objSS.IsFacebook = chkFB.Checked;
            objSS.IsTwitter = chkTwit.Checked;
            objSS.IsLinkedIn = chkLIn.Checked;
            objSS.UserID = Convert.ToInt32(Session["UserID"]);
            objSnapSiteDA.SaveSnapSiteSetting(objSS);

            foreach (GridDataItem item in tgrdWebinarList.Items)
            {
                hFld = (HiddenField)item.FindControl("hWebinarID");
                chk1 = (CheckBox)item.FindControl("chkPublish");
                if (chk1.Checked)
                    pubList = pubList + hFld.Value + ",";
                else
                    unpubList = unpubList + hFld.Value + ",";

            }
            objSnapSiteDA.PublishWebinarToSnapSite(pubList + "0", unpubList + "0");
            popPublishWebinars();
            getThemeLayouts();
            lblError.Text = "Saved successfully";
        }
    }
}
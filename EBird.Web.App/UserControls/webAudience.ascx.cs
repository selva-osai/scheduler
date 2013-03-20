using System;
using System.IO;
using System.Web.UI.HtmlControls;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Telerik.Web.UI;
using System.Web.UI.WebControls;
using EBird.BusinessEntity;
using EBird.Common;
using EBird.DataAccess;
using EBird.DocRepo;
using System.Drawing;
using System.Drawing.Drawing2D;


namespace EBird.Web.App.UserControls
{
    public partial class webAudience : System.Web.UI.UserControl
    {
        WebinarBE objWebinarBE = new WebinarBE();
        EBirdUtility objUtil = new EBirdUtility();
        WebinarDA objWebinarDA = new WebinarDA();
        EBErrorMessages objError = new EBErrorMessages();
        DocumentDA objDoc = new DocumentDA();
        bool _isWebinarPast = false;
        int rowCount = 1;

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

        public bool isWebinarPast
        {
            get
            {
                return _isWebinarPast;
            }
            set
            {
                _isWebinarPast = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                audTabLoad();
                disableUpdates();
            }
        }

        public void audTabLoad()
        {
            Context.Session.Remove("FolderType");
            Session["FolderType"] = "PRESENTATION";
            initializeAudiComponents();
            //resizeCompIcon();
            getAudienceDetail();
            getAudBackGround1();
            popResource(); // popPresentation();
        }

        // MARKED FOR DELETION
        //private void popPresentation()
        //{
        //    List<WebinarPresentations> objPresentation = objWebinarDA.getWebinarAudiPresentation(Convert.ToInt32(hWebinarID.Value));
        //    if (objPresentation.Count > 0)
        //    {
        //        rlstPPT.Visible = true;
        //        string itmVal = string.Empty;
        //        for (int idx = 0; idx < objPresentation.Count; idx++)
        //        {

        //            RadListBoxItem item = new RadListBoxItem(objPresentation[idx].FileName);
        //            item.ImageUrl = "~/images/icons/" + Path.GetExtension(objPresentation[idx].FileName).Substring(1) + "16.png";
        //            rlstPPT.Items.Add(item);
        //        }
        //    }

        //}

        private void popResource()
        {
            if (hWebinarID.Value != "0" || hWebinarID.Value != "")
            {
                List<WebinarResource> objWRes = objWebinarDA.getWebinarResoures(Convert.ToInt32(hWebinarID.Value), "'Presentation'");
                tgrdPPTList.DataSource = objWRes;
                tgrdPPTList.DataBind();
                chkBriefcaseFeatureChecked();
            }
        }

        protected void rlst_OnItemDataBound(object sender, RadListBoxItemEventArgs e)
        {
            //System.Data.DataRowView dataItem = e.Item.DataItem as System.Data.DataRowView;
            Label lbl1 = (Label)e.Item.FindControl("lblFileName");
            if (lbl1 != null)
            {

                HtmlTableRow tr1 = (HtmlTableRow)e.Item.FindControl("rwFile");
                if (rowCount % 2 == 0)
                    tr1.Attributes.Add("bgcolor", "#f7f7f7");
                else
                    tr1.Attributes.Add("bgcolor", "#EEF5F9");
                System.Web.UI.WebControls.Image img1 = (System.Web.UI.WebControls.Image)e.Item.FindControl("imgFileTyp");
                if (img1 != null)
                {
                    img1.ImageUrl = "~/images/icons/" + Path.GetExtension(lbl1.Text).Substring(1) + "16.png";
                }
                rowCount++;
            }
            //e.Item.Value = dataItem["UIDRef"].ToString();
        }

        private void resizeCompIcon()
        {

            //cmpSlide.Src = getComponentSrc("~/Images/Components1/Download1b.png", "~/Images/Components/Download1b.png");

            //cmpChat.Src = getComponentSrc("~/images/Components1/Group_Chat1.png", "~/images/Components/Group_Chat1b.png");

            //cmpQA.Src = getComponentSrc("~/images/Components1/QA1b.png", "~/images/Components/QA1b.png");

            //cmpWiki.Src = getComponentSrc("~/images/Components1/Wiki1b.png", "~/images/Components/Wiki1b.png");

            //cmpContent.Src = getComponentSrc("~/images/Components1/Briefcase1b.png", "~/images/Components/Briefcase1b.png");

            //cmpMedia.Src = getComponentSrc("~/images/Components1/MPlayer1b.png", "~/images/Components/MPlayer1b.png");

            //cmpFriend.Src = getComponentSrc("~/images/Components1/Email1b.png", "~/images/Components/Email1b.png");

            //cmpBio.Src = getComponentSrc("~/images/Components1/Speakerbio1b.png", "~/images/Components/Speakerbio1b.png");

            //cmpGoogle.Src = getComponentSrc("~/images/Components1/Search1b.png", "~/images/Components/Search1b.png");

            //cmpFB.Src = getComponentSrc("~/images/Components1/Facebook1b.png", "~/images/Components/Facebook1b.png");

            //cmpTweeter.Src = getComponentSrc("~/images/Components1/Twitter1b.png", "~/images/Components/Twitter1b.png");

            //cmpLI.Src = getComponentSrc("~/images/Components1/LinkedIn1b.png", "~/images/Components/LinkedIn1b.png");

        }

        private string getComponentSrc(string p1, string p2)
        {
            ImageUtility objImageUtility = new ImageUtility();
            if (!File.Exists(Server.MapPath(p2)))
            {
                System.Drawing.Image original = System.Drawing.Image.FromFile(Server.MapPath(p1));
                System.Drawing.Image resized = objImageUtility.ResizeImage(original, new Size(60, 60));
                resized.Save(Server.MapPath(p2), System.Drawing.Imaging.ImageFormat.Png);
            }
            return p2;
        }

        private void initializeAudiComponents()
        {
            Label lbl;
            CheckBox chk;

            if (Convert.ToString(Session["PackageSubscribed"]) == "Custom")
            {
                ClientDA objClientDA = new ClientDA();
                List<ConfigParameterBE> objConfigParameterBE = objClientDA.GetClientConfigIDsDA(Convert.ToInt32(Session["ClientID"]));
                for (int idx = 0; idx < objConfigParameterBE.Count; idx++)
                {
                    chk = (CheckBox)FindControl("chkConfig" + Convert.ToString(objConfigParameterBE[idx].ConfigID));
                    if (chk != null)
                    {
                        chk.Enabled = true;
                        chk.ForeColor = System.Drawing.Color.Black;
                        HyperLink hlnk = (HyperLink)FindControl("lbtnConfig" + Convert.ToString(objConfigParameterBE[idx].ConfigID));
                        if (hlnk != null)
                            hlnk.Enabled = true;
                    }
                    lbl = (Label)this.FindControl("lblConfig" + Convert.ToString(objConfigParameterBE[idx].ConfigID));
                    if (lbl != null)
                        lbl.ForeColor = System.Drawing.Color.Black;
                }
            }
            else
            {
                MasterDA objMasterDA = new MasterDA();
                List<PackageFeature> objPkg = objMasterDA.GetPackageFeatures(Convert.ToString(Session["PackageSubscribed"]));
                foreach (PackageFeature pf in objPkg)
                {
                    lbl = (Label)this.FindControl("lblConfig" + pf.FeatureID.ToString());
                    if (lbl != null)
                        lbl.ForeColor = System.Drawing.Color.Black;
                    chk = (CheckBox)this.FindControl("chkConfig" + pf.FeatureID.ToString());
                    if (chk != null)
                    {
                        chk.Enabled = true;
                        chk.ForeColor = System.Drawing.Color.Black;
                    }
                }
            }
        }

        private void getAudienceDetail()
        {

            WebinarAudience objWebAudi = objWebinarDA.getWebinarAudience(Convert.ToInt32(hWebinarID.Value));
            if (objWebAudi != null)
            {
                hSelBgID.Value = objWebAudi.AudienceViewBackground;
                enableCheckBox(objWebAudi.Chat);
                enableCheckBox(objWebAudi.Content);
                enableCheckBox(objWebAudi.Download);
                enableCheckBox(objWebAudi.Email);
                enableCheckBox(objWebAudi.FaceBook);
                enableCheckBox(objWebAudi.LinkedIn);
                enableCheckBox(objWebAudi.Search);
                enableCheckBox(objWebAudi.SpeakerBio);
                enableCheckBox(objWebAudi.SubmitQuestion);
                enableCheckBox(objWebAudi.Twitter);
                enableCheckBox(objWebAudi.Video);
                enableCheckBox(objWebAudi.Wiki);
            }
        }

        private void enableCheckBox(int featureID)
        {
            if (featureID != 0)
            {
                CheckBox chk;
                chk = (CheckBox)this.FindControl("chkConfig" + featureID.ToString());
                if (chk != null)
                {
                    if (chk.Enabled)
                    {
                        chk.Checked = true;
                        HyperLink lbtn1 = (HyperLink)this.FindControl("lbtnConfig" + featureID.ToString());
                        if (lbtn1 != null)
                        {
                            if (lbtn1.Enabled)
                                lbtn1.Visible = true;
                        }
                        //HtmlImage img1 = (HtmlImage)this.FindControl("imgConfig" + featureID.ToString());
                        //if (img1 != null)
                        //    img1.Visible = true;
                    }
                    else
                        chk.Checked = false;
                }
            }
        }

        private void getAudBackGround()
        {

            DirectoryInfo objDir = new DirectoryInfo(System.Web.HttpContext.Current.Server.MapPath("~/") + "Images\\backgrounds");
            FileInfo[] files = null;
            files = objDir.GetFiles("1*.png");
            int idx = 10;
            foreach (FileInfo ff in files)
            {
                ImageUtility objImageUtility = new ImageUtility();

                System.Drawing.Image original = System.Drawing.Image.FromFile(Server.MapPath("~/images/backgrounds/" + ff.Name));

                System.Drawing.Image resized = objImageUtility.ResizeImage(original, new Size(180, 120));
                resized.Save(Server.MapPath("~/images/backgrounds/audibg" + idx.ToString() + ".png"), System.Drawing.Imaging.ImageFormat.Png);

                System.Drawing.Image resized1 = objImageUtility.ResizeImage(original, new Size(720, 480));
                resized1.Save(Server.MapPath("~/images/backgrounds/pre_audibg" + idx.ToString() + ".png"), System.Drawing.Imaging.ImageFormat.Png);

                idx++;

            }
            rpAudBGCarousel.DataSource = objDir.GetFiles("audibg*.png");
            rpAudBGCarousel.DataBind();
        }

        private void getAudBackGround1()
        {
            DirectoryInfo objDir = new DirectoryInfo(System.Web.HttpContext.Current.Server.MapPath("~/") + "Images\\backgrounds");
            FileInfo[] fi = objDir.GetFiles("audibg*.png");
            FileInfo[] fi1 = objDir.GetFiles("audibg*.png");

            for (int idx = 0; idx < fi.Length; idx++)
            {
                if (getBackGroundID(fi[idx].Name) == hSelBgID.Value)
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
            rpAudBGCarousel.DataSource = fi1;
            rpAudBGCarousel.DataBind();
        }

        public void aupPresentation_FileUploaded(object sender, FileUploadedEventArgs e)
        {
            Session["FolderType"] = "PRESENTATION";
            telerikAsyncUploadResult result = e.UploadResult as telerikAsyncUploadResult;
            if (result != null)
            {
                if (result.DocumentID > 0)
                {
                    WebinarResource objWebinarResource = new WebinarResource();
                    objWebinarResource.WebinarID = Convert.ToInt32(hWebinarID.Value);
                    objWebinarResource.DocID = result.DocumentID;
                    objWebinarResource.ResourceType = "Presentation";
                    objWebinarResource.ResourceTitle = e.File.FileName;
                    objWebinarResource.ResourceValue = "";
                    objWebinarResource.IsBriefcase = false; //chkAddtoBC.Checked;
                    if (objWebinarDA.InsertWebinarResources(objWebinarResource) == 0)
                    {
                        DocAccess objDocAccess = new DocAccess();
                        objDocAccess.removeDocumentFromRepositary(result.DocumentID,Convert.ToInt32(Session["ClientID"]),"WebinarDocs");
                    }

                    //if (chkAddtoBC.Checked && !chkConfig8.Checked)
                    //{
                    //    objWebinarDA.SaveWebinarAudience(Convert.ToInt32(hWebinarID.Value), "Content", (Convert.ToInt32(ClientConfigMaster.Audi_Component_Content)).ToString());
                    //    chkConfig8.Checked = true;
                    //}
                    // popResource(); // popPresentation();
                }
            }

        }

        protected void lnkUploadPPT_Click(object sender, EventArgs e)
        {
            Session["FolderType"] = "PRESENTATION";
            popResource();
        }

        public void saveAudienceComponent()
        {
            WebinarAudience objWebinarAudience = new WebinarAudience();
            if (hSelBgID.Value != "")
                objWebinarAudience.AudienceViewBackground = hSelBgID.Value;
            else
                objWebinarAudience.AudienceViewBackground = "None";
            objWebinarAudience.Download = getCheckFeatureID(Convert.ToInt32(ClientConfigMaster.Audi_Component_Download_Slides));
            objWebinarAudience.Chat = getCheckFeatureID(Convert.ToInt32(ClientConfigMaster.Audi_Component_Group_Chat));
            objWebinarAudience.SubmitQuestion = getCheckFeatureID(Convert.ToInt32(ClientConfigMaster.Audi_Component_Submit_Question));
            objWebinarAudience.Wiki = getCheckFeatureID(Convert.ToInt32(ClientConfigMaster.Audi_Component_Wikipedia));
            objWebinarAudience.Email = getCheckFeatureID(Convert.ToInt32(ClientConfigMaster.Audi_Component_EmailFriend));
            objWebinarAudience.WebinarID = Convert.ToInt32(hWebinarID.Value);
            objWebinarDA.SaveWebinarAudience(objWebinarAudience);
            getAudBackGround1();
        }

        private int getCheckFeatureID(int featureID)
        {
            int rtnVal = 0;
            if (featureID != 0)
            {
                CheckBox chk;
                chk = (CheckBox)this.FindControl("chkConfig" + featureID.ToString());
                if (chk != null)
                {
                    if (chk.Checked)
                        rtnVal = featureID;
                }
            }
            return rtnVal;
        }

        protected string getBackGroundID(object bgFileName)
        {
            string FName = bgFileName.ToString();
            FName = FName.Replace(".png", "");
            FName = FName.Replace("audibg", "");
            return FName;
        }

        protected void rpAudBGCarousel_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                HiddenField hFld = ((HiddenField)e.Item.FindControl("hBackGroundID"));
                if (hFld != null)
                {
                    string bgID = getBackGroundID(hFld.Value);
                    HtmlImage hImg = ((HtmlImage)e.Item.FindControl("imgBG"));

                    hImg.Src = "~/Images/backgrounds/" + hFld.Value;
                    hImg.Attributes.Add("data-themeurl", "/Images/backgrounds/pre_" + hFld.Value);
                    hImg.Attributes.Add("data-docID", bgID);
                    if (bgID == hSelBgID.Value && hImg != null)
                    {
                        hImg.Attributes.Remove("class");
                        hImg.Attributes.Add("class", "previewBtn selected");
                    }
                }
            }
        }

        protected void tgrdPPTList_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                //Get the instance of the right type
                GridDataItem dataBoundItem = e.Item as GridDataItem;
                //Check the formatting condition
                Label lbl1 = (Label)dataBoundItem.FindControl("lblOrder");
                lbl1.Text = rowCount.ToString();

                lbl1 = (Label)dataBoundItem.FindControl("lblFileName");
                lbl1.Text = dataBoundItem["ResourceTitle"].Text;

                string fileExt = "";
                System.Web.UI.WebControls.Image img1 = (System.Web.UI.WebControls.Image)dataBoundItem.FindControl("imgFileTyp");
                if (img1 != null)
                {
                    fileExt = Path.GetExtension(lbl1.Text).Substring(1);
                    img1.ImageUrl = "~/images/icons/" + fileExt + "16.png";
                }

                string sPath = objDoc.GetDocumentPath(Convert.ToInt32(dataBoundItem["DocID"].Text), true);
                if (File.Exists(sPath))
                {
                    FileInfo fi = new FileInfo(sPath);

                    lbl1 = (Label)dataBoundItem.FindControl("lblFileSize");
                    if (fi != null)
                        lbl1.Text = objUtil.FormatFileSize(fi.Length);
                    else
                        lbl1.Text = "-";
                }
                AppClass.SSDocuments objPPTDoc = new AppClass.SSDocuments();

                lbl1 = (Label)dataBoundItem.FindControl("lblSlideCount");
                if (fileExt.ToUpper() == "PPT")
                {
                    lbl1.Text = objPPTDoc.GetPPTSlideCount(sPath).ToString();
                }
                else if (fileExt.ToUpper() == "PPTX")
                {
                    lbl1.Text = objPPTDoc.PPTGetSlideCount(sPath).ToString();
                }
                else
                    lbl1.Text = "-";
                ImageButton ibtn;
                //bool rtnBool = (dataBoundItem["IsBriefcase"].Text == "" ? false : Convert.ToBoolean(dataBoundItem["IsBriefcase"].Text));

                if (((EBird.BusinessEntity.WebinarResource)(e.Item.DataItem)).IsBriefcase)
                {
                    ibtn = (ImageButton)dataBoundItem.FindControl("btnChecked");
                    ibtn.Visible = true;
                }
                else
                {
                    ibtn = (ImageButton)dataBoundItem.FindControl("btnUnChecked");
                    ibtn.Visible = true;
                }

                //btnUnChecked
                //
                //chkIsBriefcase
                // ResourceID
                if (_isWebinarPast)
                {
                    ibtn.Enabled = false;
                    ibtn = (ImageButton)dataBoundItem.FindControl("btnDelete");
                    ibtn.Visible = false;
                }
                rowCount++;
            }
        }

        protected void tgrdPPTList_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "DEL":
                    DocAccess objDoc = new DocAccess();
                    objWebinarDA.DeleteRegFormResources(Convert.ToInt32(hWebinarID.Value), Convert.ToInt32(e.CommandArgument));
                    objDoc.removeDocumentFromRepositary(Convert.ToInt32(e.CommandArgument), Convert.ToInt32(Session["ClientID"]), "WebinarDocs");
                    break;
                case "UNCHK":
                    objWebinarDA.UpdateBriefcaseDocStatus(Convert.ToInt32(e.CommandArgument), Convert.ToInt32(hWebinarID.Value), true);
                    break;
                case "CHK":
                    objWebinarDA.UpdateBriefcaseDocStatus(Convert.ToInt32(e.CommandArgument), Convert.ToInt32(hWebinarID.Value), false);
                    break;
            }
            popResource();
        }

        #region drag & drop

        protected void tgrdPPTList_RowDrop(object sender, GridDragDropEventArgs e)
        {
            if (string.IsNullOrEmpty(e.HtmlElement))
            {
                if (e.DestDataItem != null && e.DestDataItem.OwnerGridID == tgrdPPTList.ClientID)
                {
                    //reorder items in grid
                    IList<WebinarResource1> WebinarResource = getPresentationList();
                    IList<WebinarResource1> webinarResource = getPresentationList();

                    WebinarResource1 res = getPresentationList(webinarResource, (int)e.DestDataItem.GetDataKeyValue("ResourceID"));

                    int destinationIndex = webinarResource.IndexOf(res);

                    if (e.DropPosition == GridItemDropPosition.Above && e.DestDataItem.ItemIndex > e.DraggedItems[0].ItemIndex)
                    {
                        destinationIndex -= 1;
                    }
                    if (e.DropPosition == GridItemDropPosition.Below && e.DestDataItem.ItemIndex < e.DraggedItems[0].ItemIndex)
                    {
                        destinationIndex += 1;
                    }

                    List<WebinarResource1> QAToMove = new List<WebinarResource1>();
                    foreach (GridDataItem draggedItem in e.DraggedItems)
                    {
                        WebinarResource1 tmpOrder = getPresentationList(webinarResource, (int)draggedItem.GetDataKeyValue("ResourceID"));
                        if (tmpOrder != null)
                            QAToMove.Add(tmpOrder);
                    }

                    foreach (WebinarResource1 qaToMove in QAToMove)
                    {
                        webinarResource.Remove(qaToMove);
                        webinarResource.Insert(destinationIndex, qaToMove);
                    }

                    for (int idx = 0; idx < webinarResource.Count; idx++)
                    {
                        //(int resID, int resOrder)
                        objWebinarDA.UpdateWebinarResourceOrder(webinarResource[idx].ResourceID, idx + 1);
                    }
                    popResource();
                }
            }
        }

        protected IList<WebinarResource1> getPresentationList()
        {
            IList<WebinarResource1> objIList = new List<WebinarResource1>();
            List<WebinarResource> objList = objWebinarDA.getWebinarResoures(Convert.ToInt32(hWebinarID.Value), "'Presentation'");
            for (int idx = 0; idx < objList.Count; idx++)
            {
                objIList.Add(new WebinarResource1(objList[idx].ResourceID, objList[idx].ResourceOrder, objList[idx].ResourceTitle));

            }
            return objIList;
        }

        private static WebinarResource1 getPresentationList(IEnumerable<WebinarResource1> ResToSearchIn, int resId)
        {
            foreach (WebinarResource1 WebRes in ResToSearchIn)
            {
                if (WebRes.ResourceID == resId)
                {
                    return WebRes;
                }
            }
            return null;
        }

        #endregion

        private void chkBriefcaseFeatureChecked()
        {

            if (objWebinarDA.GetBriefcaseDocCount(Convert.ToInt32(hWebinarID.Value), Convert.ToInt32(ClientConfigMaster.Audi_Component_Content)) > 0)
            {
                chkConfig8.Checked = true;
                if (chkConfig8.Enabled)
                    lbtnConfig8.Visible = true;
            }
            else
            {
                chkConfig8.Checked = false;
                lbtnConfig8.Visible = false;
            }

        }

        public void disableUpdates()
        {
            if (_isWebinarPast)
            {
                aupPresentation.Visible = false;
                chkConfig13.Enabled = false;
                lbtnConfig13.Text = "[View]";
                chkConfig11.Enabled = false;
                chkConfig12.Enabled = false;
                chkConfig14.Enabled = false;
                chkConfig8.Enabled = false;
                lbtnConfig8.Text = "[View]";
                chkConfig15.Enabled = false;
                lbtnConfig15.Text = "[View]";
                chkConfig19.Enabled = false;
                lbtnConfig19.Text = "[View]";
                chkConfig16.Enabled = false;
                lbtnConfig16.Text = "[View]";
                chkConfig9.Enabled = false;
                chkConfig17.Enabled = false;
                lbtnConfig17.Text = "[View]";
            }
        }
    }
}
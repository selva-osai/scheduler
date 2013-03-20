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

namespace EBird.Web.App.Pages.popup
{
    public partial class audiFeature_Content : System.Web.UI.Page
    {
        WebinarBE objWebinarBE = new WebinarBE();
        EBirdUtility objUtil = new EBirdUtility();
        WebinarDA objWebinarDA = new WebinarDA();
        EBErrorMessages objError = new EBErrorMessages();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                Session["Web_Tab"] = "3";
                if (Request["ID"] != null)
                {
                    hWebinarID.Value = Request["ID"].ToString();
                    //WebinarAudience webAudi = objWebinarDA.getWebinarAudience(Convert.ToInt32(hWebinarID.Value));
                    //if (webAudi.Content != 0)
                    if (Request["flg"].ToString() == "1")
                    {
                        Session["FolderType"] = "PRESENTATION";
                        popResource();
                    }
                    else
                    {
                        phConfig.Visible = false;
                        phDisableFeature.Visible = true;
                        lblDisableFeature.Text = objError.getMessage("WB1001");
                        dvOutline.Attributes.Remove("class");
                        dvOutline.Attributes.Add("class", "regOutline");
                    }
                }
            }
        }

        private void popResource()
        {
            if (hWebinarID.Value != "0" || hWebinarID.Value != "")
            {
                tgrdResourceList.DataSource = objWebinarDA.getWebinarResoures(Convert.ToInt32(hWebinarID.Value));
                tgrdResourceList.DataBind();
            }
        }

        protected void btnAddURL_Click(object sender, EventArgs e)
        {
            lblURLError.Text = "";
            if (txtURL.Text.Trim() == "" || txtURLName.Text.Trim() == "")
            {
                lblURLError.Text = "URL and URL Name cannot be empty";
            }
            //else if (!System.Text.RegularExpressions.Regex.IsMatch(txtURL.Text, "^(ht|f)tp(s?)/://[0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*(:(0-9)*)*(\/?)([a-zA-Z0-9\-\.\?\,\'\/\\\+&amp;%\$#_]*)?$"))
            //{
            //    lblURLError.Text = "Invalid URL";
            //}
            else
            {
                if (hWebinarID.Value != "0" || hWebinarID.Value != "")
                {
                    WebinarResource objWebinarResource = new WebinarResource();
                    objWebinarResource.WebinarID = Convert.ToInt32(hWebinarID.Value);
                    objWebinarResource.DocID = 0;
                    objWebinarResource.ResourceType = "URL";
                    objWebinarResource.ResourceTitle = txtURLName.Text;
                    objWebinarResource.ResourceValue = txtURL.Text;
                    objWebinarResource.IsBriefcase = true;
                    if (objWebinarDA.InsertWebinarResources(objWebinarResource) == 0)
                        lblURLError.Text = "URL and URL Name already exist.";
                    else
                    {
                        txtURL.Text = "";
                        txtURLName.Text = "";
                        popResource();
                    }
                }
            }
        }

        public void aupContent_FileUploaded(object sender, FileUploadedEventArgs e)
        {
            telerikAsyncUploadResult result = e.UploadResult as telerikAsyncUploadResult;
            if (result != null)
            {
                if (result.DocumentID > 0)
                {
                    WebinarResource objWebinarResource = new WebinarResource();
                    objWebinarResource.WebinarID = Convert.ToInt32(hWebinarID.Value);
                    objWebinarResource.DocID = result.DocumentID;
                    objWebinarResource.ResourceType = "Content Doc";
                    objWebinarResource.ResourceTitle = e.File.FileName;
                    objWebinarResource.ResourceValue = "";
                    objWebinarResource.IsBriefcase = true;
                    objWebinarDA.InsertWebinarResources(objWebinarResource);
                    popResource();
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            //if (Request["flg"].ToString() == "1")
            //    objWebinarDA.SaveWebinarAudience(Convert.ToInt32(hWebinarID.Value), "Content", (Convert.ToInt32(ClientConfigMaster.Audi_Component_Content)).ToString());     
            //else
            if (Request["flg"].ToString() != "1")
            {
                // make the content feature ID to 0, in webinaraudience table
                objWebinarDA.SaveWebinarAudience(Convert.ToInt32(hWebinarID.Value), "Content", "0");
                // Delete all records with type 'URL' and make isbriefcase = 0 for presentation, return no. of 'Content Doc' record count
                if (objWebinarDA.DeleteContentFromBriefcase(Convert.ToInt32(hWebinarID.Value)) > 0)
                {
                    DocAccess objDoc = new DocAccess();
                    objDoc.DeleteBriefcaseContentDocs(Convert.ToInt32(hWebinarID.Value), Convert.ToInt32(Session["ClientID"]));
                }
            }
            Session["Web_Tab"] = "3";
            hModalStatusFlg.Value = "1";
            //if (!ClientScript.IsStartupScriptRegistered("alert")) { Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "CloseAndReload();", true); }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Session["Web_Tab"] = "3";
            hModalStatusFlg.Value = "1";
            //if (!ClientScript.IsStartupScriptRegistered("alert")) { Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "CloseAndReload();", true); }
        }

        protected void tgrdResourceList_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.CommandName == "delResource")
            {
                // based on resourceID, get resourceType
                List<WebinarResource> objRes = objWebinarDA.getWebinarResouresByID(Convert.ToInt32(e.CommandArgument));
                if (objRes.Count > 0)
                {
                    if (objRes[0].ResourceType == "Content Doc")
                    {
                        DocAccess objDA = new DocAccess();
                        objDA.removeDocumentFromRepositary(objRes[0].DocID, Convert.ToInt32(Session["ClientID"]), "WebinarDocs");
                        objWebinarDA.DeleteRegFormResources(Convert.ToInt32(hWebinarID.Value), objRes[0].DocID);
                    }
                    else
                        objWebinarDA.DeleteRegFormResources(Convert.ToInt32(e.CommandArgument));

                    popResource();
                }
            }
         
        }

        protected void tgrdResourceList_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem dataBoundItem = e.Item as GridDataItem;
                WebinarResource objRes = (WebinarResource)(e.Item.DataItem);

                if (objRes.ResourceType == "Presentation")
                {
                    ImageButton iBtn = (ImageButton)dataBoundItem.FindControl("btnDelete");
                    iBtn.Visible = false;
                }
                if (objRes.ResourceType == "URL")
                {
                    HyperLink lnk1 = (HyperLink)dataBoundItem.FindControl("urlLink");
                    string urlVal = objRes.ResourceValue;
                    if (urlVal.Substring(0, 4).ToLower() != "http")
                        lnk1.NavigateUrl = "http://" + urlVal;
                    else
                        lnk1.NavigateUrl = urlVal;
                    lnk1.Text = objRes.ResourceTitle;
                    lnk1.Visible = true;
                }
                else
                {
                    Label lbl1 = (Label)dataBoundItem.FindControl("lblNoLink");
                    lbl1.Text = objRes.ResourceTitle;
                    lbl1.Visible = true;
                }
            }
        }

        protected void lnkUploadContent_Click(object sender, EventArgs e)
        {
            Session["FolderType"] = "PRESENTATION";
            popResource(); 
        }

    }
}
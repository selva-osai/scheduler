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
    public partial class audiFeature_Video : System.Web.UI.Page
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
                    }
                }
            }
        }

        private void popResource()
        {
            if (hWebinarID.Value != "0" || hWebinarID.Value != "")
            {
                tgrdResourceList.DataSource = objWebinarDA.getWebinarResoures(Convert.ToInt32(hWebinarID.Value), "'Video'");
                tgrdResourceList.DataBind();
            }
        }

        public void aupVideo_FileUploaded(object sender, FileUploadedEventArgs e)
        {
            telerikAsyncUploadResult result = e.UploadResult as telerikAsyncUploadResult;
            if (result != null)
            {
                if (result.DocumentID > 0)
                {
                    WebinarResource objWebinarResource = new WebinarResource();
                    objWebinarResource.WebinarID = Convert.ToInt32(hWebinarID.Value);
                    objWebinarResource.DocID = result.DocumentID;
                    objWebinarResource.ResourceType = "Video";
                    objWebinarResource.ResourceTitle = e.File.FileName;
                    objWebinarResource.ResourceValue = "";
                    objWebinarResource.IsBriefcase = false;
                    objWebinarDA.InsertWebinarResources(objWebinarResource);
                    popResource();
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (Request["flg"].ToString() == "1")
                objWebinarDA.SaveWebinarAudience(Convert.ToInt32(hWebinarID.Value), "Video", (Convert.ToInt32(ClientConfigMaster.Audi_Component_Upload_Video)).ToString());
            else
                objWebinarDA.SaveWebinarAudience(Convert.ToInt32(hWebinarID.Value), "Video", "0");

            if (!ClientScript.IsStartupScriptRegistered("alert")) { Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "CloseAndReload();", true); }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            if (!ClientScript.IsStartupScriptRegistered("alert")) { Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "CloseAndReload();", true); }
        }
    }
}
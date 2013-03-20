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
    public partial class audiFeature_Search : System.Web.UI.Page
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
                        popSearch();
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

        private void popSearch()
        {
            List<WebinarSearchSettings> objSS = objWebinarDA.getWebinarSearchSettings(Convert.ToInt32(hWebinarID.Value));
            if (objSS.Count > 0)
            {
                chkSearchB.Checked = objSS[0].isBing;
                chkSearchY.Checked = objSS[0].isYahoo;
                chkSearchG.Checked = objSS[0].isGoogle;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            //if (Request["flg"].ToString() == "1")
            //{
                WebinarSearchSettings objWebinarSearchSettings = new WebinarSearchSettings();
                objWebinarSearchSettings.WebinarID = Convert.ToInt32(hWebinarID.Value);
                objWebinarSearchSettings.isGoogle = chkSearchG.Checked;
                objWebinarSearchSettings.isBing = chkSearchB.Checked;
                objWebinarSearchSettings.isYahoo = chkSearchY.Checked;
                objWebinarDA.saveWebinarSearchSettings(objWebinarSearchSettings);

                //objWebinarDA.SaveWebinarAudience(Convert.ToInt32(hWebinarID.Value), "Search", (Convert.ToInt32(ClientConfigMaster.Audi_Component_Search)).ToString());
                //if (!ClientScript.IsStartupScriptRegistered("alert")) { Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "CloseAndReload();", true); }
            //}
            //else
            //{
            //    objWebinarDA.SaveWebinarAudience(Convert.ToInt32(hWebinarID.Value), "Search", "0");
            //    //if (!ClientScript.IsStartupScriptRegistered("alert")) { Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "CloseAndReload();", true); }
            //}
                Session["Web_Tab"] = "3";
            hModalStatusFlg.Value = "1";
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            //if (!ClientScript.IsStartupScriptRegistered("alert")) { Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "CloseAndReload();", true); }
            Session["Web_Tab"] = "3";
            hModalStatusFlg.Value = "1";
        }
    }
}
using System;
using System.Collections.Generic;
using EBird.BusinessEntity;
using EBird.Common;
using EBird.DataAccess;

namespace EBird.Web.App.Pages.popup
{
    public partial class audiFeature_Twitter : System.Web.UI.Page
    {
        SocialMediaDA objSocialMediaDA = new SocialMediaDA();
        EBErrorMessages objError = new EBErrorMessages();
        WebinarDA objWebinarDA = new WebinarDA();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (Request["ID"] != null)
                {
                    Session["Web_Tab"] = "3";
                    hWebinarID.Value = Request["ID"].ToString();
                    if (Request["flg"].ToString() == "1")
                    {
                        popDetails();
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

        private void popDetails()
        {
            List<TwitterSettingBO> objTW = objSocialMediaDA.GetTwitterSettingDA(Convert.ToInt32(hWebinarID.Value));
            if (objTW.Count > 0)
            {
                rcmbAcct.SelectedValue = objTW[0].dispFromAcct;
                txtHeaderTitle.Text = objTW[0].headerTitle;
                txtHashTag.Text = objTW[0].tweetHashtags;
                txtDisplayAcct.Text = objTW[0].dispFromAcct;
                txtDisplayKeywords.Text = objTW[0].filterKeywords;
                chkUserTweet.Checked = objTW[0].isUserTweet;
                txtEndUseHashtag.Text = objTW[0].userHashtags;
                txtEndUseURL.Text = objTW[0].userTextURL;
            }
            else
            {
                //rcmbAcct.Items.Add(new Telerik.Web.UI.RadComboBoxItem("Default", "Default"));
                rcmbAcct.Text = "Twitter Account";
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            TwitterSettingBO objTW = new TwitterSettingBO();
            objTW.dispFromAcct = rcmbAcct.SelectedValue;
            objTW.headerTitle = txtHeaderTitle.Text;
            objTW.tweetHashtags = txtHashTag.Text;
            objTW.dispFromAcct = txtDisplayAcct.Text;
            objTW.filterKeywords = txtDisplayKeywords.Text;
            objTW.isUserTweet = chkUserTweet.Checked;
            objTW.userHashtags = txtEndUseHashtag.Text;
            objTW.userTextURL = txtEndUseURL.Text;
            objTW.WebinarID = Convert.ToInt32(hWebinarID.Value);
            if (Request["flg"].ToString() == "1")
                objSocialMediaDA.SaveTwitterSettingDA(objTW, false);
            else 
                objSocialMediaDA.SaveTwitterSettingDA(objTW, true);
                //objWebinarDA.SaveWebinarAudience(Convert.ToInt32(hWebinarID.Value), "Twitter", (Convert.ToInt32(ClientConfigMaster.Audi_Component_Twitter)).ToString());
               // if (!ClientScript.IsStartupScriptRegistered("alert")) { Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "CloseAndReload();", true); }
            //}
            //else
            //{
            //    objWebinarDA.SaveWebinarAudience(Convert.ToInt32(hWebinarID.Value), "Twitter", "0");
            //   // if (!ClientScript.IsStartupScriptRegistered("alert")) { Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "CloseAndReload();", true); }
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
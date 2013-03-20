using System;
using System.Collections.Generic;
using EBird.BusinessEntity;
using EBird.Common;
using EBird.DataAccess;

namespace EBird.Web.App.Pages.popup
{
    public partial class audiFeature_LinkedIn : System.Web.UI.Page
    {
        SocialMediaDA objSocialMediaDA = new SocialMediaDA();
        EBErrorMessages objError = new EBErrorMessages();
        WebinarDA objWebinarDA = new WebinarDA();

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
            List<LinkedInSettingBO> objLI = objSocialMediaDA.GetLinkedInSettingDA(Convert.ToInt32(hWebinarID.Value));
            if (objLI.Count > 0)
            {
                chkLike.Checked = objLI[0].isLikeUnlike;
                chkComments.Checked = objLI[0].isComments;
                chkSearch.Checked = objLI[0].isSearch;
                chkNetwork.Checked = objLI[0].isFilterNetwork;
                txtMinUpdates.Text = objLI[0].checkInterval.ToString();
                txtMsgCount.Text = objLI[0].messageReturn.ToString();
                txtInviteSub.Text = objLI[0].defaultInviteSubject;
                txtInviteMsg.Text = objLI[0].defaultInviteMessage;
                chkAllowNetwork.Checked = objLI[0].isNetworkUpdate;
                chkAllowInvitation.Checked = objLI[0].isInvitation;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

            int MsgCount = 0;
            int intValue = 0;

            if (txtMsgCount.Text.Trim() != "")
                MsgCount = Convert.ToInt32(txtMsgCount.Text);
            if (txtMinUpdates.Text.Trim() != "")
                intValue = Convert.ToInt32(txtMinUpdates.Text);

            LinkedInSettingBO objLI = new LinkedInSettingBO();

            //defaultInviteSubject,defaultInviteMessage,isNetworkUpdate,isInvitation,webinarID
            objLI.isLikeUnlike = chkLike.Checked;
            objLI.isComments = chkComments.Checked;
            objLI.isSearch = chkSearch.Checked;
            objLI.isFilterNetwork = chkNetwork.Checked;
            objLI.checkInterval = intValue;
            objLI.messageReturn = MsgCount;
            objLI.defaultInviteSubject = txtInviteSub.Text;
            objLI.defaultInviteMessage = txtInviteMsg.Text;
            objLI.isNetworkUpdate = chkAllowNetwork.Checked;
            objLI.isInvitation = chkAllowInvitation.Checked;
            objLI.WebinarID = Convert.ToInt32(hWebinarID.Value);
            if (Request["flg"].ToString() == "1")
                objSocialMediaDA.SaveLinkedInSettingDA(objLI, false);
            else
                objSocialMediaDA.SaveLinkedInSettingDA(objLI, true);

            //objWebinarDA.SaveWebinarAudience(Convert.ToInt32(hWebinarID.Value), "LinkedIn", (Convert.ToInt32(ClientConfigMaster.Audi_Component_LinkedIn)).ToString());
            //if (!ClientScript.IsStartupScriptRegistered("alert")) { Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "CloseAndReload();", true); }
            //}
            //else
            //{
            //    objWebinarDA.SaveWebinarAudience(Convert.ToInt32(hWebinarID.Value), "LinkedIn", "0");
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
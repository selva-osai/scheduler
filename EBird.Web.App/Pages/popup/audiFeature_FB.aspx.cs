using System;
using System.Collections.Generic;
using EBird.BusinessEntity;
using EBird.Common;
using EBird.DataAccess;


namespace EBird.Web.App.Pages.popup
{
    public partial class audiFeature_FB : System.Web.UI.Page
    {
        //WebinarBE objWebinarBE = new WebinarBE();

        //EBirdUtility objUtil = new EBirdUtility();
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
            List<FaceBookSettingBO> objFB = objSocialMediaDA.GetFaceBookSettingDA(Convert.ToInt32(hWebinarID.Value));
            if (objFB.Count > 0)
            {
                chkStatusUpdate.Checked = objFB[0].isStatusUpdate;
                txtDefaultStatus.Text = objFB[0].defaultStatusMessage;
                chkLike.Checked = objFB[0].isLikeUnlike;
                chkComments.Checked = objFB[0].isComments;
                chkFriends.Checked = objFB[0].isFriend;
                chkSearch.Checked = objFB[0].isSearch;
                txtMsgCount.Text = objFB[0].messageReturn.ToString();
                txtMinUpdates.Text = objFB[0].checkInterval.ToString();
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

            FaceBookSettingBO objFB = new FaceBookSettingBO();
            objFB.isStatusUpdate = chkStatusUpdate.Checked;
            objFB.defaultStatusMessage = txtDefaultStatus.Text;
            objFB.isLikeUnlike = chkLike.Checked;
            objFB.isComments = chkComments.Checked;
            objFB.isFriend = chkFriends.Checked;
            objFB.isSearch = chkSearch.Checked;
            objFB.messageReturn = MsgCount;
            objFB.checkInterval = intValue;
            objFB.WebinarID = Convert.ToInt32(hWebinarID.Value);
            if (Request["flg"].ToString() == "1")
                objSocialMediaDA.SaveFaceBookSettingDA(objFB, false);
            else
                objSocialMediaDA.SaveFaceBookSettingDA(objFB, true);

            //objWebinarDA.SaveWebinarAudience(Convert.ToInt32(hWebinarID.Value), "FaceBook", (Convert.ToInt32(ClientConfigMaster.Audi_Component_Facebook)).ToString());
            //if (!ClientScript.IsStartupScriptRegistered("alert")) { Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "CloseAndReload();", true); }
            Session["Web_Tab"] = "3";
            hModalStatusFlg.Value = "1";
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Session["Web_Tab"] = "3";
            hModalStatusFlg.Value = "1";
            //if (!ClientScript.IsStartupScriptRegistered("alert")) { Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "CloseAndReload();", true); }
        }
    }
}
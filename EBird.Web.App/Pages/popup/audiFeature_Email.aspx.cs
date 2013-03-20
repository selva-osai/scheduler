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
    public partial class audiFeature_Email : System.Web.UI.Page
    {
        WebinarBE objWebinarBE = new WebinarBE();
        EBirdUtility objUtil = new EBirdUtility();
        WebinarDA objWebinarDA = new WebinarDA();
        EBErrorMessages objError = new EBErrorMessages();
        EmailDA objEmailDA = new EmailDA();
        List<WebinarEmailBE> objWebinarEmailBE = new List<WebinarEmailBE>();
              //WebinarEmailBE objWebinarEmailBE = new WebinarEmailBE();

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
            objWebinarEmailBE = objEmailDA.GetWebinarEmail(Convert.ToInt32(hWebinarID.Value),"Email a Friend");
            if (objWebinarEmailBE.Count > 0)
            {
                txtSubject.Text = objWebinarEmailBE[0].Subject;
                txtBody.Text = objWebinarEmailBE[0].EmailContent;
            }
            else
            {
                objEmailDA.SaveDefaultWebinarEmailContent(Convert.ToInt32(Session["Client_LanguageID"]), Convert.ToInt32(hWebinarID.Value), "Email a Friend");
                popDetails();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (Request["flg"].ToString() == "1")
            {
                if (txtSubject.Text.Trim() != "" && txtBody.Text.Trim() != "")
                {
                    WebinarEmailBE objWebinarEmailBE1 = new WebinarEmailBE();

                    objWebinarEmailBE1.WebinarID = Convert.ToInt32(hWebinarID.Value);
                    objWebinarEmailBE1.Subject = txtSubject.Text.Trim();
                    objWebinarEmailBE1.EmailContent = txtBody.Text.Trim();
                    objWebinarEmailBE1.RequestType = "Email a Friend";
                    objEmailDA.SaveWebinarEmail(objWebinarEmailBE1);

                    objWebinarDA.SaveWebinarAudience(Convert.ToInt32(hWebinarID.Value), "Email", (Convert.ToInt32(ClientConfigMaster.Audi_Component_EmailFriend)).ToString());
                    if (!ClientScript.IsStartupScriptRegistered("alert")) { Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "CloseAndReload();", true); }
                }
            }
            else
            {
                objWebinarDA.SaveWebinarAudience(Convert.ToInt32(hWebinarID.Value), "Email", "0");
                if (!ClientScript.IsStartupScriptRegistered("alert")) { Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "CloseAndReload();", true); }
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            if (!ClientScript.IsStartupScriptRegistered("alert")) { Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "CloseAndReload();", true); }
        }
    }
}
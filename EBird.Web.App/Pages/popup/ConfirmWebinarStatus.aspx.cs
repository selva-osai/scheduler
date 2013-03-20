using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using EBird.BusinessEntity;
using EBird.Common;
using EBird.DataAccess;
using Telerik.Web.UI;

namespace EBird.Web.App.Pages.popup
{
    public partial class ConfirmWebinarStatus : System.Web.UI.Page
    {
        WebinarBE objWebinarBE = new WebinarBE();
        EBirdUtility objUtil = new EBirdUtility();
        WebinarDA objWebinarDA = new WebinarDA();
        EBErrorMessages objError = new EBErrorMessages();

        protected void Page_Load(object sender, EventArgs e)
        {
            //UpdateWebinarRegStatus
            if (!this.IsPostBack)
            {
                if (Request["s"] != null)
                {
                    hWebinarID.Text = Request["ID"].ToString();
                    if (Request["s"].ToString() == "1")
                    {
                        lblMsg.Text = objError.getMessage("WB0006");
                        btnNo.Visible = true;
                    }
                    else
                    {
                        objWebinarDA.UpdateWebinarRegStatus(Convert.ToInt32(hWebinarID.Text), Convert.ToInt32(Session["UserID"]));
                        lblMsg.Text = objError.getMessage("WB0007");
                        btnNo.Visible = false;
                        btnYes.Text = "OK";
                    }
                }
                else
                {
                    lblMsg.Text = "Unable to resolve the request.. contact administrator";
                    btnNo.Visible = btnYes.Visible = false;  
                }
            }
            
        }

        protected void btnYes_Click(object sender, EventArgs e)
        {
            if (btnYes.Text != "OK") 
                objWebinarDA.UpdateWebinarRegStatus(Convert.ToInt32(hWebinarID.Text), Convert.ToInt32(Session["UserID"]));
           // if (!ClientScript.IsStartupScriptRegistered("alert")) { Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "CloseAndReload();", true); }
            Session["Web_Tab"] = "2";
            hModalStatusFlg.Value = "1";
        }

        protected void btnNo_Click(object sender, EventArgs e)
        {
            Session["Web_Tab"] = "2";
            hModalStatusFlg.Value = "1";
        }
    }
}
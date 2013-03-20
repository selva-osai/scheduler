using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EBird.Common;

namespace EBird.Web.App.Pages
{
    public partial class AdvWebinarSearch : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                Session.Remove("ADV_SEARCH");
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            lblError.Text = "";
            if (txtAdvSearch.Text.Trim() == "" && dtpFromDate.SelectedDate.ToString() == "" && dtpToDate.SelectedDate.ToString() == "")
            {
                lblError.Text = "Atleast one criteria need to entered for search";
            }
            else
            {
                EBirdUtility objUtil = new EBirdUtility();
                EBErrorMessages objError = new EBErrorMessages();
                string dt1 = "", dt2 = "";
                string errMsg = "";

                if (dtpFromDate.SelectedDate.ToString() != "")
                    dt1 = objUtil.FormDBDate(Convert.ToDateTime(dtpFromDate.SelectedDate));
                if (dtpToDate.SelectedDate.ToString() != "")
                    dt2 = objUtil.FormDBDate(Convert.ToDateTime(dtpToDate.SelectedDate));

                if (dt1 != "" && dt2 != "")
                {
                    TimeSpan span = (Convert.ToDateTime(dtpToDate.SelectedDate)).Subtract(Convert.ToDateTime(dtpFromDate.SelectedDate));
                    if (span.Days < 0)
                        errMsg = objError.getMessage("SS0002");
                }
                if (errMsg != "")
                    lblError.Text = errMsg;
                else
                {
                    Session["ADV_SEARCH"] = txtAdvSearch.Text.Trim() + ";" + optSearchOption.SelectedValue + ";" + dtpFromDate.SelectedDate.ToString() + ";" + dtpToDate.SelectedDate.ToString();
                    hModalStatusFlg.Value = "1";
                }
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ////ClientScriptManager cs = Page.ClientScript;
            ////cs.RegisterStartupScript(typeof(Page), "CloseScript_" + UniqueID,
            ////  "onClientClose('1');", true); // Return value 1 to parent 
            //string script = "closeMe();";
            //ScriptManager.RegisterStartupScript(Page, typeof(Page), "someKey", script, true);
            hModalStatusFlg.Value = "1";
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EBird.Web.App.Pages
{
    public partial class AdvUserSearch : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            hModalStatusFlg.Value = "1";
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Session["ADV_USR"] = "A";
            Session["ADV_USR_TXT"] = txtAdvSearch.Text.Trim();
            Session["ADV_USR_EML"] = txtEmailAddress.Text.Trim();
            Session["ADV_USR_ROLE"] = optSearchOption.SelectedValue;
            hModalStatusFlg.Value = "1";
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EBird.Web.App.MasterPages
{
    public partial class masterFullScreen : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UserID"] != null)
                {
                    lblMyName.Text = Session["FullName"].ToString();
                    //if (Session["Role"].ToString() == "SSAdmin")
                        lbtnMyMoonshot.Visible = false;
                    SwitchTab();
                }
                else
                    Response.Redirect("~/default.aspx");
            }
        }

        protected void lbtnMyMoonshot_Click(object sender, EventArgs e)
        {

        }

        protected void lbtnLogout_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("~/default.aspx");
        }

        void SwitchTab()
        {
            try
            {
                switch (Page.GetType().Name.Split('_')[1])
                {
                    case "schedule":
                        lnkWeb.Attributes["class"] = "Current";
                        break;
                    case "recwebinar":
                        lnkRec.Attributes["class"] = "Current";
                        break;
                    case "meet":
                        lnkMeet.Attributes["class"] = "Current";
                        break;
                    case "analytics":
                        lnkAnaly.Attributes["class"] = "Current";
                        break;
                    default:
                        lnkWeb.Attributes["class"] = "Current";
                        break;
                }
            }
            catch (Exception ex)
            {

            }

        }
    }
}
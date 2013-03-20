using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EBird.Web.App.MasterPages
{
    public partial class masterWithNav : System.Web.UI.MasterPage
    {
        protected void Page_Init()
        {
            Response.AppendHeader("Refresh", Convert.ToString((Session.Timeout * 60) + 10) + "; URL=/default.aspx?exp=1");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //if (Session["UserID"] == null)
            //    Response.Redirect("~/default.aspx");
            //if (IsSessionExpired())
            //    Response.Redirect("~/default.aspx"); 
            //Response.AppendHeader("Refresh", "\"" + Session.Timeout.ToString() + ";url=/default.aspx?exp=1");
            if (!IsPostBack)
            {
                //if (Session["UserID"] != null)
                //{
                lblMyName.Text = Session["FullName"].ToString();
                //if (Session["Role"].ToString() == "SSAdmin")
                lbtnMyMoonshot.Visible = true;
                if (Session["Client_DateFormat"] != null)
                    hDateFormat.Value = Session["Client_DateFormat"].ToString();
                else
                    hDateFormat.Value = "MM-dd-yyyy";
                ////         SwitchTab();
                //     }
                //     else
                //         Response.Redirect("~/default.aspx"); 
            }
        }

        protected void lbtnMyMoonshot_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/Settings");
        }

        protected void lbtnLogout_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("~/default.aspx");
        }

        public static bool IsSessionExpired()
        {
            if (HttpContext.Current.Session != null)
            {
                if (HttpContext.Current.Session.IsNewSession)
                {
                    string CookieHeaders = HttpContext.Current.Request.Headers["Cookie"];

                    if ((null != CookieHeaders) && (CookieHeaders.IndexOf("ASP.NET_SessionId") >= 0))
                    {
                        // IsNewSession is true, but session cookie exists,
                        // so, ASP.NET session is expired
                        return true;
                    }
                }
            }
            // Session is not expired and function will return false,
            // could be new session, or existing active session
            return false;
        }
        //void SwitchTab()
        //{
        //    try
        //    {
        //        switch (Page.GetType().Name.Split('_')[1])
        //        {
        //            case "schedule":
        //                lnkWeb.Attributes["class"] = "Current";
        //                break;
        //            case "recwebinar":
        //                lnkRec.Attributes["class"] = "Current";
        //                break;
        //            case "meet":
        //                lnkMeet.Attributes["class"] = "Current";
        //                break;
        //            case "analytics":
        //                lnkAnaly.Attributes["class"] = "Current";
        //                break;
        //            default:
        //                lnkWeb.Attributes["class"] = "Current";
        //                break;
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //    }

        //}
    }
}
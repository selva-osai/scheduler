using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EBird.Web.App.UserControls
{
    public partial class schLPart : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                string url = System.IO.Path.GetFileName(Request.Path);

                switch (Session["Role"].ToString().ToUpper())
                {
                    case "USER":
                        lblLWCap.Text = "Webinar Scheduler";
                        if (url.ToLower().Equals("usermgmt") ||
                            url.ToLower().Equals("acctsettings") ||
                            url.ToLower().Equals("settings"))
                        {
                            lblLWCap.Text = "My Settings";
                            Set1.Visible = true;
                            Set2.Visible = true;
                            Set3.Visible = false;
                        }
                        else
                        {
                            A1.Visible = true;
                            A2.Visible = true;
                            A3.Visible = true;
                            A4.Visible = true;
                            chkPremium();
                        }
                        break;
                    case "ADMIN":
                        lblLWCap.Text = "Webinar Scheduler";
                        if (url.ToLower().Equals("usermgmt") || url.ToLower().Equals("acctsettings") || url.ToLower().Equals("settings"))
                        {
                            lblLWCap.Text = "My Settings";
                            Set1.Visible = true;
                            Set2.Visible = true;
                            Set3.Visible = true;
                        }
                        else
                        {
                            A1.Visible = true;
                            A2.Visible = true;
                            A3.Visible = true;
                            A4.Visible = true;
                            chkPremium();
                        }
                        break;
                    case "SSADMIN":
                        lblLWCap.Text = "Administration";
                        if (url.ToLower().Equals("usermgmt") || url.ToLower().Equals("acctsettings") || url.ToLower().Equals("settings"))
                        {
                            lblLWCap.Text = "My Settings";
                            Set1.Visible = true;
                            Set2.Visible = true;

                        }
                        else
                        {
                            ssl1.Visible = true;
                            ssl2.Visible = true;
                            //ssl3.Visible = true;
                            ssl4.Visible = true;
                            ssl5.Visible = true;
                            ssl6.Visible = true;
                            ssl7.Visible = true;
                        }
                        break;
                    case "AEADMIN":
                        lblLWCap.Text = "Account Executive";
                        if (url.ToLower().Equals("usermgmt") || url.ToLower().Equals("acctsettings") || url.ToLower().Equals("settings"))
                        {
                            lblLWCap.Text = "My Settings";
                            Set1.Visible = true;
                            Set2.Visible = true;
                        }
                        else
                        {
                            ssl1.Visible = true;
                            ssl2.Visible = true;
                            ssl4.Visible = true;
                        }
                        break;
                }
                initializeLinks();
                setSelectMenu(url.ToLower());
            }
        }

        private void chkPremium()
        {
            if (Session["role"].ToString() == "Admin" || Session["role"].ToString() == "User")
            {
                if (Session["PREMIUM_FEATURE"].ToString().IndexOf(",4,") >= 0)
                {
                    A4.Visible = true;
                    sp1.Visible = false;
                }
                else
                {
                    A4.Visible = false;
                    sp1.Visible = true;
                }
            }
        }

        private void setSelectMenu(string url)
        {
            switch (url)
            {
                case "webinar":
                    Session.Remove("WebinarID");
                    A1.Attributes.Remove("href");
                    A1.Attributes.Add("class", "LeftNavActiveItem");
                    A4.Attributes.Add("class", "LeftNavLastItem");
                    break;
                case "registrants":
                    A1.Attributes.Remove("href");
                    A1.Attributes.Add("class", "LeftNavActiveItem");
                    A4.Attributes.Add("class", "LeftNavLastItem");
                    break;
                case "schedule":
                    A2.Attributes.Remove("href");
                    A2.Attributes.Add("class", "LeftNavActiveItem");
                    A4.Attributes.Add("class", "LeftNavLastItem");
                    break;
                case "webinaraction":
                    A2.Attributes.Remove("href");
                    A2.Attributes.Add("class", "LeftNavActiveItem");
                    A4.Attributes.Add("class", "LeftNavLastItem");
                    break;
                case "analytics":
                    A3.Attributes.Remove("href");
                    A3.Attributes.Add("class", "LeftNavActiveItem");
                    A4.Attributes.Add("class", "LeftNavLastItem");
                    break;
                case "snapsite":
                    A4.Attributes.Remove("href");
                    A4.Attributes.Add("class", "LeftNavLastItem LeftNavActiveItem");
                    break;
                case "client":
                    ssl1.Attributes.Remove("href");
                    ssl1.Attributes.Add("class", "LeftNavActiveItem");
                    if (Session["Role"].ToString().ToUpper() == "AEADMIN")
                        ssl4.Attributes.Add("class", "LeftNavLastItem");
                    else
                        ssl5.Attributes.Add("class", "LeftNavLastItem");
                    break;
                case "clientconfig":
                    ssl2.Attributes.Remove("href");
                    ssl2.Attributes.Add("class", "LeftNavActiveItem");
                    if (Session["Role"].ToString().ToUpper() == "AEADMIN")
                        ssl4.Attributes.Add("class", "LeftNavLastItem");
                    else
                        ssl5.Attributes.Add("class", "LeftNavLastItem");
                    break;
                case "themes":
                    //ssl3.Attributes.Remove("href");
                    //ssl3.Attributes.Add("class", "LeftNavActiveItem");
                    ssl5.Attributes.Add("class", "LeftNavLastItem");
                    break;
                case "subscription":
                    ssl4.Attributes.Remove("href");
                    if (Session["Role"].ToString().ToUpper() == "AEADMIN")
                    {
                        ssl4.Attributes.Add("class", "LeftNavLastItem LeftNavActiveItem");
                    }
                    else
                    {
                        ssl4.Attributes.Add("class", "LeftNavActiveItem");
                        ssl5.Attributes.Add("class", "LeftNavLastItem");
                    }
                    break;
                case "emailcontent":
                    ssl5.Attributes.Remove("href");
                    ssl5.Attributes.Add("class", "LeftNavLastItem LeftNavActiveItem");
                    break;
                case "adminmgmt":
                    ssl6.Attributes.Remove("href");
                    ssl6.Attributes.Add("class", "LeftNavActiveItem");
                    ssl5.Attributes.Add("class", "LeftNavLastItem");
                    break;
                case "audit":
                    ssl7.Attributes.Remove("href");
                    ssl7.Attributes.Add("class", "LeftNavActiveItem");
                    ssl5.Attributes.Add("class", "LeftNavLastItem");
                    break;
                case "usermgmt":
                    Set3.Attributes.Remove("href");
                    Set3.Attributes.Add("class", "LeftNavLastItem LeftNavActiveItem");
                    break;
                case "acctsettings":
                    Set2.Attributes.Remove("href");
                    if (Session["Role"].ToString().ToUpper() != "ADMIN")
                    {
                        Set2.Attributes.Add("class", "LeftNavLastItem LeftNavActiveItem");
                    }
                    else
                    {
                        Set2.Attributes.Add("class", "LeftNavActiveItem");
                        Set3.Attributes.Add("class", "LeftNavLastItem");
                    }
                    break;
                case "settings":
                    Set1.Attributes.Remove("href");
                    Set1.Attributes.Add("class", "LeftNavActiveItem");
                    if (Session["Role"].ToString().ToUpper() != "ADMIN")
                        Set2.Attributes.Add("class", "LeftNavLastItem");
                    else
                        Set3.Attributes.Add("class", "LeftNavLastItem");
                    break;
            }
        }

        private void initializeLinks()
        {
            ssl1.Attributes.Add("href", "~/Pages/Client");
            ssl2.Attributes.Add("href", "~/Pages/ClientConfig");
            //ssl3.Attributes.Add("href", "~/Pages/Themes");
            ssl4.Attributes.Add("href", "~/Pages/Subscription");
            ssl5.Attributes.Add("href", "~/Pages/emailcontent");
            ssl6.Attributes.Add("href", "~/Pages/adminmgmt");
            ssl7.Attributes.Add("href", "~/Pages/audit");

            Set1.Attributes.Add("href", "~/Pages/Settings");
            Set2.Attributes.Add("href", "~/Pages/AcctSettings");
            Set3.Attributes.Add("href", "~/Pages/UserMgmt");

            if (Session["role"].ToString() == "Admin" || Session["role"].ToString() == "User")
            {
                A1.Attributes.Add("href", "~/Pages/Webinar");
                A2.Attributes.Add("href", "~/Pages/Schedule");
                A3.Attributes.Add("href", "~/Pages/Analytics");
                if (A4.Visible)
                    A4.Attributes.Add("href", "~/Pages/SnapSite");
            }
        }
    }
}
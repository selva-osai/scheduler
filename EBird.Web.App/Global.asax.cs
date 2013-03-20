using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Routing;


namespace EBird.Web.App
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            //if (applicationStartupComplete) return; 
            try
            {
                RegisterRoutes(RouteTable.Routes);
                object osm = new System.Web.UI.ScriptManager();
            }
            catch (Exception)
            { } 
        }

        public void RegisterRoutes(RouteCollection routes)
        {
            // Register a route for Client
            routes.MapPageRoute(
               "All Client",             // Route name
               "Pages/Client",           // Route URL
               "~/Pages/clientInfo.aspx" // Web page to handle route
            );
            // Register a route for Client config
            routes.MapPageRoute(
               "Client Config",            // Route name
               "Pages/ClientConfig",       // Route URL
               "~/Pages/ClientConfig.aspx" // Web page to handle route
            );
            //Router - Registration page
            routes.MapPageRoute(
               "Webinar Registration",     // Route name
               "Reg/{Id}",        // Route URL
               "~/Pages/Registration_new.aspx" // Web page to handle route
            );
            //Router - Registration Preview page
            routes.MapPageRoute(
               "Webinar Registration Preview",     // Route name
               "Regpreview/{Id}",        // Route URL
               "~/Pages/Registration_preview.aspx" // Web page to handle route
            );
            //Router - Audience Interface
            routes.MapPageRoute(
               "Webinar Waiting Room",     // Route name
               "vwr/{Id}",        // Route URL
               "~/Pages/WaitingRoom.aspx" // Web page to handle route
            );
            //Router - Command Center
            routes.MapPageRoute(
               "Webinar Command Center",     // Route name
               "coc/{Id}",        // Route URL
               "~/Pages/CoCenter.aspx" // Web page to handle route
            );
            //Router - Analytics
            routes.MapPageRoute(
               "Webinar Analytics",     // Route name
               "any/{Id}",        // Route URL
               "~/Pages/analytics.aspx" // Web page to handle route
            );
            // Register a route for My Analytics
            routes.MapPageRoute(
               "My Analytics",             // Route name
               "Pages/Analytics",          // Route URL
               "~/Pages/Analytics.aspx"    // Web page to handle route
            );
            //Router - Preview Interface
            routes.MapPageRoute(
               "Webinar Preview Interface",     // Route name
               "prw/{Id}",        // Route URL
               "~/Pages/PreInterface.aspx" // Web page to handle route
            );

            // Register a route for Theme
            routes.MapPageRoute(
               "Theme management",         // Route name
               "Pages/Themes",             // Route URL
               "~/Pages/ThemeBuild.aspx"   // Web page to handle route
            );

            // register a route for Theme layout preview
            routes.MapPageRoute(
               "Theme layout preview",         // Route name
               "Pages/layoutpreview/{Id}",             // Route URL
               "~/Pages/Themelayout.aspx"   // Web page to handle route
            );

            // Register a route for Subscription
            routes.MapPageRoute(
               "Subscription Log",         // Route name
               "Pages/Subscription",             // Route URL
               "~/Pages/Subscription.aspx"   // Web page to handle route
            );

            // Register a route for Webinar
            routes.MapPageRoute(
               "Webinar List",             // Route name
               "Pages/Webinar",            // Route URL
               "~/Pages/mywebinars.aspx"   // Web page to handle route
            );

            // Register a route for Advance Search Webinar
            routes.MapPageRoute(
               "Webinar Advance Search",             // Route name
               "Pages/AdSearchWebinar",            // Route URL
               "~/Pages/AdvWebinarSearch.aspx"   // Web page to handle route
            );
            // Register a route for Webinar Schedule
            routes.MapPageRoute(
               "Schedule a Webinar",       // Route name
               "Pages/Schedule",           // Route URL
               "~/Pages/schedule.aspx"     // Web page to handle route
            );

            // Register a route for getting all Webinar registrants
            routes.MapPageRoute(
               "Webinar Registrants",       // Route name
               "Pages/Registrants",           // Route URL
               "~/Pages/Registrants.aspx"     // Web page to handle route
            );

            //Admin SnapSite
            routes.MapPageRoute(
               "Admin SnapSite",             // Route name
               "Pages/SnapSite",          // Route URL
               "~/Pages/SnapSite.aspx"    // Web page to handle route
            );

            //My SnapSite
            routes.MapPageRoute(
               "My SnapSite",             // Route name
               "Pages/MySnapSite/{Id}",          // Route URL
               "~/Pages/MySnapSite.aspx"    // Web page to handle route
            );

            // Register a route for My settings
            routes.MapPageRoute(
               "My Settings",             // Route name
               "Pages/Settings",          // Route URL
               "~/Pages/myprofile.aspx"   // Web page to handle route
            );
            
            //My Account Settings
            routes.MapPageRoute(
               "Account settings",        // Route name
               "Pages/AcctSettings",      // Route URL
               "~/Pages/Acct.aspx"        // Web page to handle route
            );

            //User management
            routes.MapPageRoute(
               "User Management",         // Route name
               "Pages/UserMgmt",          // Route URL
               "~/Pages/UserAdmin.aspx"   // Web page to handle route
            );

            // Register a route for Advance Search Webinar
            routes.MapPageRoute(
               "User Advance Search",             // Route name
               "Pages/AdvSearchUser",            // Route URL
               "~/Pages/AdvUserSearch.aspx"   // Web page to handle route
            );

            //Webinar actions
            routes.MapPageRoute(
               "Webinar actions",         // Route name
               "Pages/webinarAction",          // Route URL
               "~/Pages/webinarAction.aspx"   // Web page to handle route
            );

            // register a route for image display
            routes.MapPageRoute(
               "Image Display",         // Route name
               "Pages/logo/{Id}",             // Route URL
               "~/Pages/getLogo.aspx"   // Web page to handle route
            );

            // register a route for default email content
            routes.MapPageRoute(
               "Email Content",         // Route name
               "Pages/emailcontent",             // Route URL
               "~/admin/emailContent.aspx"   // Web page to handle route
            );

            // register a route for default administrator content
            routes.MapPageRoute(
               "Administrator Management",         // Route name
               "Pages/adminmgmt",             // Route URL
               "~/Pages/adminmgmt.aspx"   // Web page to handle route
            );

            // register a route for Audit
            routes.MapPageRoute(
               "Audit",         // Route name
               "Pages/audit",             // Route URL
               "~/Pages/audit.aspx"   // Web page to handle route
            );

            // register a route for Recycle
            routes.MapPageRoute(
               "Recycle",         // Route name
               "Pages/Recycle/{type}",             // Route URL
               "~/Pages/recycle.aspx"   // Web page to handle route
            );

            // register a route for Access Denied
            routes.MapPageRoute(
               "Access Denied",         // Route name
               "Pages/AccessDenied",             // Route URL
               "~/Pages/accessdenied.aspx"   // Web page to handle route
            );

        } 

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EBird.Web.App.Pages.admin
{
    public partial class clientInfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Context.Session == null)
                Response.Redirect("~/default.aspx"); 
            if (!this.IsPostBack)
            {
                if (Session["Role"].ToString().ToUpper() == "USER" || Session["Role"].ToString().ToUpper() == "ADMIN")
                    Response.Redirect("~/Pages/AccessDenied");
            }
        }
    }
}
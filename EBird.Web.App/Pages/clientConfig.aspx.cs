using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EBird.Web.App.Pages
{
    public partial class clientConfig : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (Session["Role"] == null)
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
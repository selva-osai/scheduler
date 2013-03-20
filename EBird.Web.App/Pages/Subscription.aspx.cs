﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EBird.Web.App.Pages
{
    public partial class Subscription : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (Session["Role"].ToString().ToUpper() == "USER" || Session["Role"].ToString().ToUpper() == "ADMIN")
                    Response.Redirect("~/Pages/AccessDenied");
            }
        }
    }
}
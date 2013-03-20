using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EBird.Web.App.Pages
{
    public partial class Themelayout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (Page.RouteData.Values["Id"] != null)
                {
                    string layoutID = Page.RouteData.Values["Id"].ToString();
                    lbllayoutID.Text = layoutID; 
                }
            }
        }
    }
}
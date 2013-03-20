using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EBird.BusinessEntity;
using EBird.DataAccess;

namespace EBird.Web.App.Pages
{
    public partial class recWebinar : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                WebcastDA objWebcastDA = new WebcastDA();

                List<WebinarBO> objWebinarBO = objWebcastDA.GetMyWebcastDA(0);
                gvWeb.DataSource = objWebinarBO;
                gvWeb.DataBind();
            }
        }

        protected void gvWeb_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Title")
            {
                Response.Redirect("~/pages/recPlayback.aspx?ID="+e.CommandArgument.ToString());
            }
        }
    }
}
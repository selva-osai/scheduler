using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EBird.DataAccess;

namespace EBird.Web.App.Pages
{
    public partial class getLogo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (Page.RouteData.Values["Id"] != null)
                {
                    string docID = Page.RouteData.Values["Id"].ToString();
                    DocumentDA obj1 = new DocumentDA();
                    Response.Write("<img src='" + obj1.GetDocumentPath(Convert.ToInt32(docID), true, true) + "'>");
                }
            }
        }
    }
}
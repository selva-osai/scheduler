using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EBird.Web.App.Pages.popup
{
    public partial class previewTheme : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (Request["typ"] != null)
                {
                    tbContainer.Attributes.Remove("style");
                    switch (Request["typ"].ToString())
                    {
                        case "1":
                            imglayout.Src = "/images/layout/prelayout1.png";
                            tbContainer.Attributes.Add("style", "background: #f5f5f5; height:600px");
                            break;
                        case "2":
                            imglayout.Src = "/images/layout/prelayout2.png";
                            tbContainer.Attributes.Add("style", "background: #ffffff; height:600px");
                            break;
                        case "3":
                            imglayout.Src = "/images/layout/prelayout3.png";
                            tbContainer.Attributes.Add("style", "background: #BDD1E9; height:600px");
                            break;
                        default:
                            imglayout.Src = "/images/layout/prelayout1.png";
                            tbContainer.Attributes.Add("style", "background: #f5f5f5; height:600px");
                            break;
                    }
                }
                //lblID.Text = Request["typ"].ToString() + " ID " + Request["ID"].ToString();

                //    lnkStyle.Attributes.Remove("href");
                //    lnkStyle.Attributes.Add("href", "~/Styles/layout1/prelayout1.css");  
            }

        }
    }
}
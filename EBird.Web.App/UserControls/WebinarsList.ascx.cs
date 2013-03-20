using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EBird.Common;

namespace EBird.Web.App.UserControls
{
    public partial class WebinarsList : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {

            }
        }

        protected void btnImg_Click(object sender, EventArgs e)
        {
            presentation objPres = new presentation();
            objPres.pptThumbnail("D:\\NewBegining\\EarlyBird\\Code\\EBird.Web.App\\PrensentationDocs\\11231\\Testing1.pptx", "D:\\NewBegining\\EarlyBird\\Code\\EBird.Web.App\\PrensentationDocs\\11231\\Testing1.jpg", 120, 120);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EBird.BusinessEntity;
using EBird.DataAccess;

namespace EBird.Web.App.Admin
{
    public partial class emailContent : System.Web.UI.Page
    {
        EmailDA objEmailDA = new EmailDA();
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (Session["Role"].ToString().ToUpper() != "SSADMIN")
                    Response.Redirect("~/Pages/AccessDenied");
                else
                {
                    MasterDA objMas = new MasterDA();
                    rcmbMetaTag.DataSource = objMas.getMetaTagList();
                    rcmbMetaTag.DataTextField = "TagName";
                    rcmbMetaTag.DataValueField = "TagID";
                    rcmbMetaTag.DataBind();
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (redtRemEmail.Content.Trim() != "")
            {
                objEmailDA.SaveWebinarEmailDefault(1, rmbEmailType.SelectedValue, redtRemEmail.Content.Trim(), txtsubject.Text);
                lblResult.Text = "Updated successfully";
            }
        }

        protected void rmbEmailType_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            redtRemEmail.Content = "";
            lblResult.Text = "";
            List<WebinarEmailBE> objWebEmailBE = new List<WebinarEmailBE>();
            objWebEmailBE = objEmailDA.GetWebinarEmailDefault(1, rmbEmailType.SelectedValue);
            if (objWebEmailBE.Count > 0)
            {
                redtRemEmail.Content = objWebEmailBE[0].EmailContent;
                txtsubject.Text = objWebEmailBE[0].Subject;
            }
        }
    }
}
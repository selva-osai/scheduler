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
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            UserDA objUserDA = new UserDA();

            List<UserBE> objUserBO = objUserDA.GetAuthenticatedUserDA(txtUserName.Text.Trim(), txtPassword.Text.Trim());
            if (objUserBO.Count > 0)
            {

                if (objUserBO[0].UserStatus == "Active")
                {
                    Session["UserID"] = objUserBO[0].UserID;
                    Session["ClientID"] = objUserBO[0].ClientID;
                    Session["Role"] = objUserBO[0].Role;

                    Session["FullName"] = objUserBO[0].FirstName + " " + objUserBO[0].LastName;
                    if (objUserBO[0].Role == "SSAdmin")
                        Response.Redirect("~/Pages/clientInfo.aspx");
                    else
                        Response.Redirect("~/Pages/mywebinars.aspx");
                }
                else
                {
                    lblMsg.Text = "Please contact your administrator";
                }
            }
            else
                lblMsg.Text = "Invalid username or password...";
        }
    }
}
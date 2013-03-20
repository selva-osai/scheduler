using System;
using System.Collections.Generic;
using EBird.BusinessEntity;
using EBird.DataAccess;

namespace EBird.Web.App.MasterPages
{
    public partial class mainMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UserID"] != null)
                {
                    //lblMyName.Text = Session["FullName"].ToString();
                    //lbtnRegister.Text = "My Snap Session"; 
                    lbtnLogin.Text = "Logout";
                }
            }
        }

        protected void lbtnRegister_Click(object sender, EventArgs e)
        {

        }
        
        protected void lbtnLogin_Click(object sender, EventArgs e)
        {
            //if (lbtnLogin.Text == "Login")
            //{
            //    Response.Redirect("~/Pages/login.aspx");
            //}
            //else
            //{
            //    Session.Abandon();
            //    Response.Redirect("~/default.aspx");        
            //}
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
                        Response.Redirect("~/Pages/Client");
                    else
                        Response.Redirect("~/Pages/Webinar");
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
using System;
using System.Web;
using System.Collections.Generic;
using EBird.BusinessEntity;
using EBird.DataAccess;
using EBird.Common;
using System.Text.RegularExpressions;
using EBird.Email;

namespace EBird.Web.App
{
    public partial class _default1 : System.Web.UI.Page
    {
        UserDA objUserDA = new UserDA();
        EBErrorMessages objErr = new EBErrorMessages();

        protected void Page_Load(object sender, EventArgs e)
        {
            // Response.Write(HttpContext.Current.Server.MapPath("~/DocRepo/"));
            if (!IsPostBack)
            {
                if (Request["exp"] != null)
                {
                    lblError1.Text = "Your Session Expired";
                    InvalidPlaceholder.Visible = true;
                }
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            //string strEnc = string.Empty;
            //List<UserBE> objUserBO1 = objUserDA.GetUserListDA(1);
            //for (int idx = 0; idx < objUserBO1.Count; idx++)
            //{
            //    strEnc = EBirdUtility.Encrypt(objUserBO1[idx].Password);
            //    objUserDA.UpdateUserPasswordEncrypted(objUserBO1[idx].UserID.ToString(), strEnc);
            //}



            List<UserBE> objUserBO = objUserDA.GetAuthenticatedUserDA(txtEmailID.Text.Trim(), EBirdUtility.Encrypt(txtPassword.Text.Trim()));
            if (objUserBO.Count > 0)
            {

                //if (objUserBO[0].UserStatus == "Active")
                if (objUserBO[0].AuthenticationState == "")
                {
                    Session["UserID"] = objUserBO[0].UserID;
                    Session["EmailID"] = txtEmailID.Text.Trim();
                    HttpContext.Current.Session.Add("UserID", objUserBO[0].UserID);
                    Session["ClientID"] = objUserBO[0].ClientID;
                    Session["Role"] = objUserBO[0].Role;

                    ClientDA objClientDA = new ClientDA();

                    List<ClientBE> objClientBE = objClientDA.GetClientDetailDA(objUserBO[0].ClientID);
                    if (objClientBE.Count > 0)
                    {
                        Session["PackageSubscribed"] = objClientBE[0].CurrentPkgSubscribed;
                        Session["Client_LanguageID"] = objClientBE[0].LanguageID;
                        Session["Client_TimeZoneID"] = objClientBE[0].TimeZoneID;
                        Session["Client_DateFormat"] = objClientBE[0].DateFormat;
                    }
                    Session["FullName"] = objUserBO[0].FirstName + " " + objUserBO[0].LastName;
                    if (objUserBO[0].Role == "SSAdmin" || objUserBO[0].Role == "AEAdmin")
                        //lblMsg.Text = "SSAdmin";
                        Response.Redirect("~/Pages/Client");
                    else
                    {
                        Session["PREMIUM_FEATURE"] = "," + objClientDA.GetPremiumFeatureDetailDA(Convert.ToInt32(objUserBO[0].ClientID));
                        Response.Redirect("~/Pages/Webinar");
                    }
                }
                else
                {
                    if (objUserBO[0].AuthenticationState == "UA002" || objUserBO[0].AuthenticationState == "UA003")
                    {
                        phChangePassword.Visible = true;
                        phLogin.Visible = false;
                        InvalidPlaceholder.Visible = false;
                    }
                    else
                    {
                        lblError1.Text = objErr.getMessage(objUserBO[0].AuthenticationState);
                        InvalidPlaceholder.Visible = true;
                    }
                }
            }
            else
            {
                lblError1.Text = objErr.getMessage("SS0004");
                InvalidPlaceholder.Visible = true;
            }
        }

        protected void btnChangePassword_Click(object sender, EventArgs e)
        {
            InvalidPlaceholder.Visible = true;
            if (txtCurrentPassword.Text.Trim() == "" || txtNewPassword.Text.Trim() == "" || txtConfirmPassword.Text.Trim() == "")
            {
                lblError1.Text = "No password input can be empty";
            }
            else
            {
                if (txtNewPassword.Text.Trim() != txtConfirmPassword.Text.Trim())
                {
                    lblError1.Text = "Mismatch of new password and confirm password";
                }
                else
                {
                    Regex objRegEx = new Regex("^.*(?=.{8,})(?=.*\\d)(?=.*[a-z])(?=.*[A-Z]).*$");
                    if (!objRegEx.IsMatch(txtNewPassword.Text))
                    {
                        lblError1.Text = objErr.getMessage("AM0011");
                    }
                    else
                    {
                       string rtnVal = objUserDA.ChangeUserPassword(txtEmailID.Text.Trim(),EBirdUtility.Encrypt(txtCurrentPassword.Text.Trim()), EBirdUtility.Encrypt(txtNewPassword.Text.Trim()));
                       if (rtnVal != "")
                           lblError1.Text = objErr.getMessage(rtnVal);
                       else
                       {
                           int reqID = SaveToEmailJob(txtEmailID.Text.Trim());
                           if (reqID > 0)
                           {
                               EmailApp objEmailing = new EmailApp();
                               objEmailing.SendEmail(reqID);
                           }

                           txtPassword.Text = txtNewPassword.Text.Trim();
                           btnLogin_Click(null, null);
                       }
                    }
                }
            }
        }

        private int SaveToEmailJob(string emailID)
        {
            EmailBE objEmailBE = new EmailBE();
            EmailDA objEmailDA = new EmailDA();
            EmailApp objEmailing = new EmailApp();

            string emlContent = objEmailing.getHTMLFormattedPasswdChangeNotify(emailID);
            objEmailBE.isToEmailRef = false;
            objEmailBE.RequestStatus = "No-delay";
            objEmailBE.RequestType = "Password Changed";
            objEmailBE.Subject = "Password Changed Notification";
            objEmailBE.SubmittedBy = 0;
            objEmailBE.ToEmail = emailID;
            objEmailBE.FromEmail = "support@ebird.com";
            objEmailBE.EmailContent = emlContent;
            return objEmailDA.SaveEmailRequest(objEmailBE);
        }
    }
}
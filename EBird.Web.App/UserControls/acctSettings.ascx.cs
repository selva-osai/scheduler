using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EBird.DataAccess;
using EBird.BusinessEntity;
using EBird.Common;
using Telerik.Web.UI;
using System.Text.RegularExpressions;
using EBird.Email;

namespace EBird.Web.App.UserControls
{
    public partial class acctSettings : System.Web.UI.UserControl
    {
        EBErrorMessages objErr = new EBErrorMessages();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                getSetting();
                if (Session["Role"].ToString().ToUpper() == "AEADMIN" || Session["Role"].ToString().ToUpper() == "SSADMIN")
                {
                    ltrBack.Text = "Return to <a href='Client' class='lnkBtn1'>Client Information</a>";
                    dvEmail.Visible = false;
                    dvTime.Visible = false;
                    btnSave.Visible = false;
                }
            }
        }

        private void getSetting()
        {
            UserDA objUserDA = new UserDA();
            List<UserBE> objUBE = objUserDA.GetUserDetailDA(Convert.ToInt32(Session["UserID"]));
            if (objUBE.Count > 0)
            {
                chkEmailUpdate.Checked = objUBE[0].isEmailWeeklyUpdate;
                rcmbTimeZone.SelectedValue = objUBE[0].TimeZoneID.ToString();
                chkDaylight.Checked = objUBE[0].isAutoDLSave;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            lblMsg.Text = "";
            UserDA objUserDA = new UserDA();
            
            UserBE objUBE1 = new UserBE();
            objUBE1.isEmailWeeklyUpdate = chkEmailUpdate.Checked;
            objUBE1.TimeZoneID = Convert.ToInt32(rcmbTimeZone.SelectedValue);
            objUBE1.isAutoDLSave = chkDaylight.Checked;
            objUBE1.UserID = Convert.ToInt32(Session["UserID"]);

            objUserDA.UpdateUserAcccountSetting(objUBE1);
            
            lblMsg.ForeColor = System.Drawing.Color.Green;
            lblMsg.Text = objErr.getMessage("AM0006");

        }

        protected void btnChangePassword_Click(object sender, EventArgs e)
        {

            if (txtCurrPassword.Text.Trim() == "" || txtNewPassword.Text.Trim() == "" || txtConfirmPassword.Text.Trim() == "")
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
                        UserDA objUserDA = new UserDA();
                        string rtnVal = objUserDA.ChangeUserPassword(Session["EmailID"].ToString(), EBirdUtility.Encrypt(txtCurrPassword.Text.Trim()), EBirdUtility.Encrypt(txtNewPassword.Text.Trim()));
                        if (rtnVal != "")
                            lblError1.Text = objErr.getMessage(rtnVal);
                        else
                        {
                            int reqID = SaveToEmailJob(Session["EmailID"].ToString());
                            if (reqID > 0)
                            {
                                EmailApp objEmailing = new EmailApp();
                                objEmailing.SendEmail(reqID);
                            }
                            lblError1.Text = "Password changed successfully, login using your changed password";
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
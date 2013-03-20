using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EBird.Common;
using EBird.BusinessEntity;
using EBird.DataAccess;
using EBird.Email;

namespace EBird.Web.App.Pages.popup
{
    public partial class PresenterContact : System.Web.UI.Page
    {
        EBirdUtility objUtil = new EBirdUtility();
        EmailApp objEmailing = new EmailApp();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (Request["cmd"] != null)
                {
                    hWebinarID.Value = Request["cmd"].ToString();  
                }
            }
        }

        protected void btnEmail_Click(object sender, EventArgs e)
        {
            if (txtAttendeeEmails.Text != "")
            {
                lblError.ForeColor = System.Drawing.Color.Red;
                bool isEmailsvalid = true;
                System.Collections.ArrayList arrList = objUtil.StringToArrayList(txtAttendeeEmails.Text, new char[] { ';' });
                for (int idx = 0; idx < arrList.Count; idx++)
                {
                    if (!objUtil.isEmail(arrList[idx].ToString()))
                    {
                        isEmailsvalid = false;
                        break;
                    }
                }
                if (isEmailsvalid)
                {
                    int reqID = SaveToEmailJob(txtAttendeeEmails.Text);
                    if (reqID > 0)
                    {
                        objEmailing.SendEmail(reqID, Convert.ToInt32(hWebinarID.Value));
                        hModalStatusFlg.Value = "1";
                    }
                    else
                    {
                        lblError.Text = "Error encountered in emailing";
                    }
                }
                else
                    lblError.Text = "Entered email(s) has invalid email address";
            }
            else
                lblError.Text = "No email address entered";
        }

        protected void btnEmail_Close(object sender, EventArgs e)
        {
            hModalStatusFlg.Value = "1";
        }

        private int SaveToEmailJob(string toEmail)
        {
            EmailBE objEmailBE = new EmailBE();
            EmailDA objEmailDA = new EmailDA();
            objEmailBE.isToEmailRef = false;
            objEmailBE.RequestStatus = "No-delay";
            objEmailBE.RequestType = "Presenter Contact Emailing"; // "Webinar Registrant Emailing";
            objEmailBE.Subject = "Webinar Presenter Contact";
            objEmailBE.SubmittedBy = Convert.ToInt32(Session["UserID"]);
            objEmailBE.ToEmail = toEmail;
            objEmailBE.FromEmail = Session["EmailID"].ToString();
            objEmailBE.EmailContent = objEmailing.getHTMLFormattedWebinarPresenterContact("Presenter Contact", Convert.ToInt32(hWebinarID.Value));
            return objEmailDA.SaveEmailRequest(objEmailBE);
        }

    }
}
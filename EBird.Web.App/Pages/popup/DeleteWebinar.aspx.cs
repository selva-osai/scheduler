using System;
using System.Collections.Generic;
using System.Text;
using EBird.BusinessEntity;
using EBird.Common;
using EBird.DataAccess;

namespace EBird.Web.App.Pages.popup
{
    public partial class DeleteWebinar : System.Web.UI.Page
    {
        WebinarBE objWebinarBE = new WebinarBE();
        EBirdUtility objUtil = new EBirdUtility();
        WebinarDA objWebinarDA = new WebinarDA();
        EBErrorMessages objError = new EBErrorMessages();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (Request["ID"] != null)
                {
                    hWebinarID.Value = Request["ID"].ToString();
                    hModalStatusFlg.Value = "0";
                    if (Request["a"] != null)
                    {
                        phDelete.Visible = true;
                        phEnable.Visible = false;
                    }
                    else
                    {
                        List<WebinarBE> objWeb = objWebinarDA.GetWebinarDetailDA(Convert.ToInt32(hWebinarID.Value));
                        hStartDate.Value =  objWeb[0].StartDate.ToString();
                        hStartTime.Value =  objWeb[0].StartTime;
                        hEndTime.Value = objWeb[0].EndTime;
                        
                        ltrStatus.Text = objWeb[0].WebinarStatus;
                        if (objWeb[0].WebinarStatus == "Active")
                        {
                            lblDelInstruction.Text = "This Webinar will be cancelled and a notification will be sent to all participants.<br>";
                            btnDelWebinar.Text = "Cancel Webinar";
                        }
                        else
                        {
                            lblDelInstruction.Text = "This Webinar will be re-activated.<br><br>";
                            btnDelWebinar.Text = "Activate Webinar";
                        }
                    }
                }
            }

        }

        protected void btnDelWebinar_Click(object sender, EventArgs e)
        {
            if (btnDelWebinar.Text == "Cancel Webinar")
            {
                objWebinarDA.UpdateWebinarStatus(Convert.ToInt32(hWebinarID.Value), "Inactive", Convert.ToInt32(Session["UserID"]));
                hModalStatusFlg.Value = "1";
            }
            else
            {
                if (objWebinarDA.IsWebinarOverlapping(objUtil.FormDBDate(Convert.ToDateTime(hStartDate.Value)), hStartTime.Value, hEndTime.Value, Convert.ToInt32(Session["UserID"]), Convert.ToInt32(hWebinarID.Value)))
                {
                    lblDelInstruction.Text = "<font color=red>" + objError.getMessage("WB0008") + "</font>";
                  //lblError.Text = objError.getMessage("WB0008");
                }
                else
                {
                    objWebinarDA.UpdateWebinarStatus(Convert.ToInt32(hWebinarID.Value), "Active", Convert.ToInt32(Session["UserID"]));
                    hModalStatusFlg.Value = "1";
                }
            }
            
            
            //Page.ClientScript.RegisterStartupScript(Page.GetType(), "my", "function facebook_send_message(to);", true);

            //ClientScript.RegisterStartupScript(Page.GetType(), "Upload Completed", "window.parent.reload();", true);
        }

        protected void btnDelCancel_Click(object sender, EventArgs e)
        {
            hModalStatusFlg.Value = "1";
        }

        private void UpdateToEmails(List<EmailAddressBO> objEmailAddress, string toType, int requestID)
        {
            StringBuilder strToEmail = new StringBuilder();
            EmailTo objEmailTo = new EmailTo();
            EmailDA objEmailDA = new EmailDA();
            for (int idx = 0; idx < objEmailAddress.Count; idx++)
            {
                strToEmail.Append(objEmailAddress[idx].EmailAddress + ",");
            }
            objEmailTo.EmailRequestID = requestID;
            objEmailTo.ToEmails = strToEmail.ToString();
            objEmailTo.ToType = toType;
            objEmailDA.SaveToEmail(objEmailTo);
        }

        private int SaveEmail(string requestType, string subject, string toEmail, string emailContent)
        {
            EmailBE objEmailBE = new EmailBE();
            EmailDA objEmailDA = new EmailDA();
            objEmailBE.isToEmailRef = true;
            objEmailBE.RequestStatus = "No-delay";
            objEmailBE.RequestType = requestType; // "Webinar Registrant Emailing";
            objEmailBE.Subject = subject;         // txtSubject.Text;
            objEmailBE.SubmittedBy = Convert.ToInt32(Session["UserID"]);
            objEmailBE.ToEmail = toEmail;
            objEmailBE.FromEmail = Session["EmailID"].ToString();
            objEmailBE.EmailContent = emailContent;
            return objEmailDA.SaveEmailRequest(objEmailBE);
        }

        protected void btnWebDelConfirm_Click(object sender, EventArgs e)
        {
            objWebinarDA.DeleteWebinar(Convert.ToInt32(hWebinarID.Value), Convert.ToInt32(Session["UserID"]));
            hModalStatusFlg.Value = "1";
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;
using EBird.DataAccess;
using EBird.BusinessEntity;
using EBird.Framework;
using EBird.Common;
using EBird.Report;
using EBird.Email;

namespace EBird.Web.App.Pages.popup
{
    public partial class Notification_EmailTpl : System.Web.UI.Page
    {
        WebinarBE objWebinarBE = new WebinarBE();
        EBirdUtility objUtil = new EBirdUtility();
        WebinarDA objWebinarDA = new WebinarDA();
        EBErrorMessages objError = new EBErrorMessages();
        EmailDA objEmailDA = new EmailDA();
        EmailApp objEmailing = new EmailApp();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (Request["ID"] != null)
                {
                    string sReq = Request["typ"].ToString().Substring(0, 2);
                    hWebinarID.Value = Request["ID"].ToString();
                    string tlValue = "";
                    lblpgCap2.Visible = false;
                    switch (sReq)
                    {
                        case "IN":
                            hReqType.Value = "Webinar Invitation";
                            SetWebinarInvite();
                            break;
                        case "RE":
                            hReqType.Value = "Registrant Reminder Email";
                            SetWebinarReminder();
                            chkOutlook.Visible = false;
                            break;
                        case "CO":
                            hReqType.Value = "Confirmation Email";
                            SetWebinarConfirmationEmail();
                            break;
                        case "FA":
                            hReqType.Value = "Attendee Followup";
                            SetFollowUpAttendeeEmail();
                            chkOutlook.Visible = false;
                            break;
                        case "FU":
                            hReqType.Value = "Non-Attendee Followup";
                            SetFollowUpUnAttendeeEmail();
                            chkOutlook.Visible = false;
                            break;
                    }
                }
            }
        }

        private string getFormedEmailContent(string contentWithTags)
        {
            string rtn = "";
            List<WebinarBE> objWebBE = objWebinarDA.GetWebinarDetailDA(Convert.ToInt32(hWebinarID.Value));
            if (objWebBE.Count > 0)
            {
                WebinarReminderEmailTemplateBO objTpl = new WebinarReminderEmailTemplateBO();

                objTpl.EventDate = objWebBE[0].StartDate.ToShortDateString();
                objTpl.EventTime = objWebBE[0].StartTime;
                objTpl.TimeZoneName = objWebBE[0].TimeZoneID.ToString();  // need to call function to get zone name
                objTpl.WebinarID = objWebBE[0].WebinarID;
                objTpl.WebinarTitle = objWebBE[0].Title;
                objTpl.AudienceURL = "";
                objTpl.RemainingDays = "";
                TemplateMgmt objTemplate = new TemplateMgmt();
                rtn = objTemplate.GetReminderEmail(objTpl, contentWithTags, false);
            }
            return rtn;
        }

        #region Webinar Invite

        private void SetWebinarInvite()
        {
            string rtyp = Request["typ"].ToString();
            int webinarID = Convert.ToInt32(hWebinarID.Value);
            List<WebinarEmailBE> objWBEmail = new List<WebinarEmailBE>();
            objWBEmail = objEmailDA.GetWebinarEmail(webinarID, "Webinar Invitation");
            if (objWBEmail.Count > 0)
            {
                chkSysReq.Checked = objWBEmail[0].IsSystemReq;
                chkOutlook.Checked = objWBEmail[0].IsOutlookLink;
                switch (rtyp)
                {
                    #region Webinar Invitation
                    case "INP": // Webinar Invitation - Preview
                        phEdit.Visible = false;
                        phView.Visible = true;
                        lblpgCap2.Text = objWBEmail[0].RequestType + " - Preview";
                        //ltrEmailContent.Text = getFormedEmailContent(objWBEmail[0].EmailContent);
                        ltrEmailContent.Text = objEmailing.getHTMLFormattedEmailContent(objWBEmail[0].EmailContent, objWBEmail[0].RequestType, webinarID);
                        break;
                    case "INE": //  Webinar Invitation - Edit
                        phEdit.Visible = true;
                        phView.Visible = false;
                        lblpgCap2.Text = objWBEmail[0].RequestType + " - Edit";
                        txtSubject.Text = objWBEmail[0].Subject;
                        redtRemEmail.Content = objWBEmail[0].EmailContent;
                        btnSave.Visible = true;
                        break;
                    case "INI": //  Webinar Invitation - Email me the Invite
                        phEdit.Visible = false;
                        phView.Visible = true;
                        lblpgCap2.Text = objWBEmail[0].RequestType + " - Email me the Invite";
                        //ltrEmailContent.Text = getFormedEmailContent("Subject: " + objWBEmail[0].Subject + "<br><br>" + objWBEmail[0].EmailContent);
                        ltrEmailContent.Text = objEmailing.getHTMLFormattedEmailContent("Subject: " + objWBEmail[0].Subject + "<br><br>" + objWBEmail[0].EmailContent, objWBEmail[0].RequestType, webinarID);

                        btnReview.Text = "Send Email";
                        btnReview.Visible = true;
                        break;
                    case "INR": //  Webinar Invitation - Send for Review
                        phEdit.Visible = false;
                        phView.Visible = true;
                        lblpgCap2.Text = objWBEmail[0].RequestType + " - Send for Review";
                        //ltrEmailContent.Text = getFormedEmailContent(objWBEmail[0].EmailContent);
                        ltrEmailContent.Text = objEmailing.getHTMLFormattedEmailContent(objWBEmail[0].EmailContent, objWBEmail[0].RequestType, webinarID, Session["Client_DateFormat"].ToString());
                        btnReview.Visible = true;
                        txtReviewerEmail.Visible = true;
                        break;
                    #endregion
                }
            }
        }

        #endregion

        #region Reminder email to registrants

        private void SetWebinarReminder()
        {
            string rtyp = Request["typ"].ToString();
            int webinarID = Convert.ToInt32(hWebinarID.Value);
            List<WebinarEmailBE> objWBEmail = new List<WebinarEmailBE>();
            objWBEmail = objEmailDA.GetWebinarEmail(webinarID, "Registrant Reminder Email");
            if (objWBEmail.Count > 0)
            {
                chkSysReq.Checked = objWBEmail[0].IsSystemReq;
                chkOutlook.Checked = objWBEmail[0].IsOutlookLink;
                switch (Request["typ"].ToString())
                {
                    case "REP":
                        phEdit.Visible = false;
                        phView.Visible = true;
                        lblpgCap2.Text = objWBEmail[0].RequestType + " - Preview";
                        //ltrEmailContent.Text = getFormedEmailContent(objWBEmail[0].EmailContent);
                        ltrEmailContent.Text = objEmailing.getHTMLFormattedEmailContent(objWBEmail[0].EmailContent, objWBEmail[0].RequestType, webinarID);
                        break;
                    case "REE":
                        phEdit.Visible = true;
                        phView.Visible = false;
                        lblpgCap2.Text = objWBEmail[0].RequestType + " - Edit";
                        redtRemEmail.Content = objWBEmail[0].EmailContent;
                        txtSubject.Text = objWBEmail[0].Subject;
                        btnSave.Visible = true;
                        break;
                    case "RER":
                        phEdit.Visible = false;
                        phView.Visible = true;
                        lblpgCap2.Text = objWBEmail[0].RequestType + " - Send for Review";
                        //ltrEmailContent.Text = getFormedEmailContent(objWBEmail[0].EmailContent);
                        ltrEmailContent.Text = objEmailing.getHTMLFormattedEmailContent(objWBEmail[0].EmailContent, objWBEmail[0].RequestType, webinarID);
                        btnReview.Visible = true;
                        txtReviewerEmail.Visible = true;
                        break;
                }
            }
        }

        #endregion

        #region Confirmation email
        private void SetWebinarConfirmationEmail()
        {
            string rtyp = Request["typ"].ToString();
            int webinarID = Convert.ToInt32(hWebinarID.Value);
            List<WebinarEmailBE> objWBEmail = new List<WebinarEmailBE>();
            objWBEmail = objEmailDA.GetWebinarEmail(webinarID, "Confirmation Email");
            if (objWBEmail.Count > 0)
            {
                chkSysReq.Checked = objWBEmail[0].IsSystemReq;
                chkOutlook.Checked = objWBEmail[0].IsOutlookLink;
                switch (Request["typ"].ToString())
                {
                    case "COP":
                        phEdit.Visible = false;
                        phView.Visible = true;
                        lblpgCap2.Text = objWBEmail[0].RequestType + " - Preview";
                        //ltrEmailContent.Text = getFormedEmailContent(objWBEmail[0].EmailContent);
                        ltrEmailContent.Text = objEmailing.getHTMLFormattedEmailContent(objWBEmail[0].EmailContent, objWBEmail[0].RequestType, webinarID);
                        break;
                    case "COE":
                        phEdit.Visible = true;
                        phView.Visible = false;
                        lblpgCap2.Text = objWBEmail[0].RequestType + " - Edit";
                        redtRemEmail.Content = objWBEmail[0].EmailContent;
                        txtSubject.Text = objWBEmail[0].Subject;
                        btnSave.Visible = true;
                        break;
                    case "COR":
                        phEdit.Visible = false;
                        phView.Visible = true;
                        lblpgCap2.Text = objWBEmail[0].RequestType + " - Send for Review";
                        //ltrEmailContent.Text = getFormedEmailContent(objWBEmail[0].EmailContent);
                        ltrEmailContent.Text = objEmailing.getHTMLFormattedEmailContent(objWBEmail[0].EmailContent, objWBEmail[0].RequestType, webinarID, Session["Client_DateFormat"].ToString());
                        btnReview.Visible = true;
                        txtReviewerEmail.Visible = true;
                        break;
                }
            }
        }
        #endregion

        #region Follow-Up Email to Attendees
        private void SetFollowUpAttendeeEmail()
        {
            string rtyp = Request["typ"].ToString();
            int webinarID = Convert.ToInt32(hWebinarID.Value);
            List<WebinarEmailBE> objWBEmail = new List<WebinarEmailBE>();
            objWBEmail = objEmailDA.GetWebinarEmail(webinarID, "Attendee Followup");
            if (objWBEmail.Count > 0)
            {
                chkSysReq.Checked = objWBEmail[0].IsSystemReq;
                chkOutlook.Checked = objWBEmail[0].IsOutlookLink;
                switch (Request["typ"].ToString())
                {
                    case "FAP":
                        phEdit.Visible = false;
                        phView.Visible = true;
                        lblpgCap2.Text = objWBEmail[0].RequestType + " - Preview";
                        //ltrEmailContent.Text = getFormedEmailContent(objWBEmail[0].EmailContent);
                        ltrEmailContent.Text = objEmailing.getHTMLFormattedEmailContent(objWBEmail[0].EmailContent, objWBEmail[0].RequestType, webinarID);
                        break;
                    case "FAE":
                        phEdit.Visible = true;
                        phView.Visible = false;
                        lblpgCap2.Text = objWBEmail[0].RequestType + " - Edit";
                        redtRemEmail.Content = objWBEmail[0].EmailContent;
                        txtSubject.Text = objWBEmail[0].Subject;
                        btnSave.Visible = true;
                        break;
                    case "FAR":
                        phEdit.Visible = false;
                        phView.Visible = true;
                        lblpgCap2.Text = objWBEmail[0].RequestType + " - Send for Review";
                        //ltrEmailContent.Text = getFormedEmailContent(objWBEmail[0].EmailContent);
                        ltrEmailContent.Text = objEmailing.getHTMLFormattedEmailContent(objWBEmail[0].EmailContent, objWBEmail[0].RequestType, webinarID);
                        btnReview.Visible = true;
                        txtReviewerEmail.Visible = true;
                        break;
                }
            }
        }
        #endregion

        #region Follow-Up Email to Registrants Who Didn't Attend
        private void SetFollowUpUnAttendeeEmail()
        {
            string rtyp = Request["typ"].ToString();
            int webinarID = Convert.ToInt32(hWebinarID.Value);
            List<WebinarEmailBE> objWBEmail = new List<WebinarEmailBE>();
            objWBEmail = objEmailDA.GetWebinarEmail(webinarID, "Non-Attendee Followup");
            if (objWBEmail.Count > 0)
            {
                chkSysReq.Checked = objWBEmail[0].IsSystemReq;
                chkOutlook.Checked = objWBEmail[0].IsOutlookLink;
                switch (Request["typ"].ToString())
                {
                    case "FUP":
                        phEdit.Visible = false;
                        phView.Visible = true;
                        lblpgCap2.Text = objWBEmail[0].RequestType + " - Preview";
                        //ltrEmailContent.Text = getFormedEmailContent(objWBEmail[0].EmailContent);
                        ltrEmailContent.Text = objEmailing.getHTMLFormattedEmailContent(objWBEmail[0].EmailContent, objWBEmail[0].RequestType, webinarID);
                        break;
                    case "FUE":
                        phEdit.Visible = true;
                        phView.Visible = false;
                        lblpgCap2.Text = objWBEmail[0].RequestType + " - Edit";
                        redtRemEmail.Content = objWBEmail[0].EmailContent;
                        txtSubject.Text = objWBEmail[0].Subject;
                        btnSave.Visible = true;
                        break;
                    case "FUR":
                        phEdit.Visible = false;
                        phView.Visible = true;
                        lblpgCap2.Text = objWBEmail[0].RequestType + " - Send for Review";
                        //ltrEmailContent.Text = getFormedEmailContent(objWBEmail[0].EmailContent);
                        ltrEmailContent.Text = objEmailing.getHTMLFormattedEmailContent(objWBEmail[0].EmailContent, objWBEmail[0].RequestType, webinarID);
                        btnReview.Visible = true;
                        txtReviewerEmail.Visible = true;
                        break;
                }
            }
        }
        #endregion

        //private EmailBE GetWebinarContent(string ReqType, int webinarID)
        //{
        //    EmailBE objEmailBE = new EmailBE();
        //    string tplValue = "";
        //    if (objWBEmail.Count > 0)
        //    {
        //        objEmailBE.EmailContent = objWBEmail[0].EmailContent;
        //        objEmailBE.FromEmail = "";
        //        objEmailBE.RequestType = objWBEmail[0].RequestType;
        //        objEmailBE.Subject = objWBEmail[0].Subject;
        //    }
        //}

        private EmailBE GetWebinarContent(string ReqType, string emailSubject, string tplFileName)
        {
            EmailBE objEmailBE = new EmailBE();

            string tplValue = "";
            hWebinarID.Value = Request["ID"].ToString();
            List<WebinarEmailBE> objWBEmail = objEmailDA.GetWebinarEmail(Convert.ToInt32(hWebinarID.Value), ReqType);
            if (objWBEmail.Count == 0)
            {
                TemplateMgmt objTemplateMgmt = new TemplateMgmt();
                tplValue = objTemplateMgmt.GetReminderEmail(Constant.DocTemplate + tplFileName);
                SaveEmail(tplValue, emailSubject, ReqType, Convert.ToInt32(hWebinarID.Value));
                objEmailBE.EmailContent = tplValue;
                objEmailBE.FromEmail = "";
                objEmailBE.RequestType = ReqType;
                objEmailBE.Subject = emailSubject;
            }
            else
            {
                objEmailBE.EmailContent = objWBEmail[0].EmailContent;
                objEmailBE.FromEmail = "";
                objEmailBE.RequestType = objWBEmail[0].RequestType;
                objEmailBE.Subject = objWBEmail[0].Subject;
            }
            return objEmailBE;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (txtSubject.Text != "" && redtRemEmail.Content != "")
            {
                SaveEmail(redtRemEmail.Content, txtSubject.Text.Trim(), hReqType.Value, Convert.ToInt32(hWebinarID.Value));
                hModalStatusFlg.Value = "1";
            }
            else
                lblError.Text = "Subject and email content cannot be empty";
        }

        protected void btnSendEmail_Click(object sender, EventArgs e)
        {
            if (txtTo.Text != "")
            {
                bool isEmailsvalid = true;
                System.Collections.ArrayList arrList = objUtil.StringToArrayList(txtTo.Text, new char[] { ';' });
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
                    int reqID = SaveToEmailJob(txtTo.Text, "Test");
                    if (reqID > 0)
                    {
                        objEmailing.SendEmail(reqID, Convert.ToInt32(hWebinarID.Value));
                        lblError.ForeColor = System.Drawing.Color.Black;
                        lblError.Text = "Successfully emailed";
                    }
                    else
                    {
                        lblError.ForeColor = System.Drawing.Color.Red;
                        lblError.Text = "Error encountered in emailing";
                    }
                }
                else
                    lblError.Text = "Entered email(s) has invalid email address";
            }
            else
                lblReviewError.Visible = true;
                lblReviewError.Text = "No email address entered";

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            hModalStatusFlg.Value = "1";
        }

        protected void btnReview_Click(object sender, EventArgs e)
        {
            if (btnReview.Text == "Send Email")
            {
                int reqID = SaveToEmailJob(Session["EmailID"].ToString(), "Review");
                if (reqID > 0)
                {
                    EmailApp objEmailing = new EmailApp();
                    objEmailing.SendEmail(reqID, Convert.ToInt32(hWebinarID.Value));
                    lblError.ForeColor = System.Drawing.Color.Black;
                    lblError.Text = "Successfully emailed";
                }
                else
                {
                    lblError.ForeColor = System.Drawing.Color.Red;
                    lblError.Text = "Error encountered in emailing";
                }
            }
            else
            {
                if (txtReviewerEmail.Text != "")
                {
                    bool isEmailsvalid = true;
                    System.Collections.ArrayList arrList = objUtil.StringToArrayList(txtReviewerEmail.Text, new char[] { ';' });
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
                        int reqID = SaveToEmailJob(txtReviewerEmail.Text, "Review");
                        if (reqID > 0)
                        {
                            EmailApp objEmailing = new EmailApp();
                            objEmailing.SendEmail(reqID, Convert.ToInt32(hWebinarID.Value));
                            lblReviewError.ForeColor = System.Drawing.Color.Black;
                            lblReviewError.Text = "Successfully emailed for review";
                            txtReviewerEmail.Text = "";
                        }
                        else
                        {
                            lblReviewError.ForeColor = System.Drawing.Color.Red;
                            lblReviewError.Text = "Error encountered in emailing";
                        }
                    }
                    else
                        lblReviewError.Text = "Entered email(s) has invalid email address";
                }
                else
                    lblReviewError.Text = "No email address entered";
            }
        }

        private void SaveEmail(string emlContent, string emlSubject, string rType, int WebinarID)
        {
            WebinarEmailBE objWebBE1 = new WebinarEmailBE();
            objWebBE1.EmailContent = emlContent;
            objWebBE1.RequestType = rType;
            objWebBE1.Subject = emlSubject;
            objWebBE1.WebinarID = WebinarID;
            //objWebBE1.IsAdditionalWebinar = false;
            objWebBE1.IsOutlookLink = chkOutlook.Checked;
            objWebBE1.IsSystemReq = chkSysReq.Checked;
            objEmailDA.SaveWebinarEmail(objWebBE1);
        }

        private int SaveToEmailJob(string toEmail, string chkTyp)
        {
            EmailBE objEmailBE = new EmailBE();
            EmailDA objEmailDA = new EmailDA();
            objEmailBE.isToEmailRef = false;
            objEmailBE.RequestStatus = "No-delay";
            objEmailBE.RequestType = hReqType.Value; // "Webinar Registrant Emailing";
            objEmailBE.Subject = chkTyp + " : " + txtSubject.Text;
            objEmailBE.SubmittedBy = Convert.ToInt32(Session["UserID"]);
            objEmailBE.ToEmail = toEmail;
            objEmailBE.FromEmail = Session["EmailID"].ToString();
            objEmailBE.EmailContent = objEmailing.getFormedEmailContent(redtRemEmail.Content, hReqType.Value, Convert.ToInt32(hWebinarID.Value)); 
            return objEmailDA.SaveEmailRequest(objEmailBE);
        }

    }
}
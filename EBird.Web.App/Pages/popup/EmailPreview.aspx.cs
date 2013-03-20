using System;
using EBird.Report;
using EBird.BusinessEntity;
using EBird.DataAccess;
using EBird.Framework;
using System.Collections.Generic;
using EBird.Email;

namespace EBird.Web.App.Pages.popup
{
    public partial class EmailPreview : System.Web.UI.Page
    {
        WebinarBE objWebinarBE = new WebinarBE();
        WebinarDA objWebinarDA = new WebinarDA();
        EmailDA objEmailDA = new EmailDA();
        EmailApp objEmailing = new EmailApp();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (Request["ID"] != null)
                {
                    string sReq = Request["typ"].ToString().Substring(0, 2);
                    int WebinarID = Convert.ToInt32(Request["ID"]);
                    string tlValue = "";
                    switch (sReq)
                    {
                        case "IN":
                            SetContent(WebinarID,"Webinar Invitation");
                            break;
                        case "RE":
                            SetContent(WebinarID,"Registrant Reminder Email");
                            break;
                        case "CO":
                            SetContent(WebinarID,"Confirmation Email");
                            break;
                        case "FA":
                            SetContent(WebinarID,"Attendee Followup");
                            break;
                        case "FU":
                            SetContent(WebinarID,"Non-Attendee Followup");
                            break;
                        case "CN":
                            SetContent(WebinarID, "Webinar Cancellation");
                            break;
                    }
                }

            }
        }

        private void SetContent(int webinarID, string sType)
        {
            ReportUtils objRptUtil = new ReportUtils();
            WeeklyReports objRpt = new WeeklyReports();
            List<WebinarEmailBE> objWBEmail = new List<WebinarEmailBE>();

            objWBEmail = objEmailDA.GetWebinarEmail(webinarID, sType);
            if (objWBEmail.Count > 0)
            {
                //ltrContent.Text = getFormedEmailContent(objWBEmail[0].EmailContent, sType, webinarID);
                ltrSubject.Text = "Subject: " + objEmailing.getFormedEmailSubjectLine(objWBEmail[0].Subject, webinarID);
                ltrContent.Text = getContent(objWBEmail[0].EmailContent, sType, webinarID);
                //ltrStyle.Text = objRptUtil.getCSSDefns(objWBEmail[0].ThemeID);
                //if (objWBEmail[0].IsSystemReq)
                //    lblSys.Text = objRptUtil.getSystemRequirement();  
            }
            //ltrHeader.Text = objRptUtil.getEmailRptHeader(webinarID);
            //ltrFooter.Text = objRptUtil.getEmailRptFooter();
        }

        private string getFormedEmailContent(string contentWithTags, string emailType, int webinarID)
        {
            string rtn = "";
            WebinarAllEmailTagsBO objTpl = new WebinarAllEmailTagsBO();
            switch (emailType)
            {
                case "Attendee Followup":
                    rtn = contentWithTags;
                    break;
                case "Confirmation Email":
                    rtn = contentWithTags;
                    break;
                case "Non-Attendee Followup":
                    rtn = contentWithTags;
                    break;
                case "Registrant Reminder Email":
                    List<WebinarBE> objWebBE = objWebinarDA.GetWebinarDetailDA(webinarID);
                    if (objWebBE.Count > 0)
                    {
                        MasterDA objMas = new MasterDA();
                        List<TimeZoneBE> tm =  objMas.getTimeZoneName(objWebBE[0].TimeZoneID);
                        objTpl.EventDate = objWebBE[0].StartDate.ToShortDateString();
                        objTpl.EventTime = objWebBE[0].StartTime;
                        objTpl.TimeZoneName = tm[0].ShortTimeZoneName; 
                        objTpl.WebinarID = objWebBE[0].WebinarID;
                        objTpl.WebinarTitle = objWebBE[0].Title;
                        objTpl.AudienceURL =  "";
                        objTpl.RemainingDays = "";
                    }
                    break;
                case "Webinar Invitation":
                    rtn = contentWithTags;
                    break;
            }
            TemplateMgmt objTemplate = new TemplateMgmt();
            rtn = objTemplate.GetContentForAnyWebinarEmail(objTpl, contentWithTags, false, emailType);
            return rtn;
        }

        private string getContent(string contentWithTags, string emailType, int webinarID)
        {
            return objEmailing.getHTMLFormattedEmailContent(contentWithTags, emailType, webinarID, Session["Client_DateFormat"].ToString());
           // Subject = getFormedEmailSubjectLine(objEmailBE1[0].Subject, webinarID);
        }
    }
}
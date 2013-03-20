    using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using SendGridMail;
using SendGridMail.Transport;
using EBird.Common;
using EBird.DataAccess;
using EBird.BusinessEntity;
using EBird.Report;
using EBird.Framework;

namespace EBird.Email
{

    public class EmailApp
    {
        EmailDA objEmailDA = new EmailDA();
        EmailingLog objEmailingLog = new EmailingLog();
        WebinarDA objWebinarDA = new WebinarDA();
        EBirdUtility objUtil = new EBirdUtility();
        String AudiUrl;

        public void SendEmail(int requestID, int webinarID)
        {

            var message = SendGrid.GetInstance();
            //create an instance of the SMTP transport mechanism
            var transportInstance = SMTP.GetInstance(new System.Net.NetworkCredential(Constant.SMTP_USR, Constant.SMTP_PASS));

            List<EmailBE> objEmailBE1 = objEmailDA.GetRequest(requestID);
            if (objEmailBE1.Count > 0)
            {
                message.Html = getHTMLFormattedEmailContent(objEmailBE1[0].EmailContent, objEmailBE1[0].RequestType, webinarID);
                message.Subject = getFormedEmailSubjectLine(objEmailBE1[0].Subject, webinarID);
                message.From = new MailAddress(objEmailBE1[0].FromEmail);

                if (Constant.isEmailDebug == "1")
                    message.AddTo(Constant.DebugEmail);
                else
                {
                    foreach (EmailTo o in objEmailBE1[0].ToEmailList)
                    {
                        message.AddBcc(o.ToEmails);
                    }
                }
                transportInstance.Deliver(message);
                objEmailDA.UpdateRequestStatus(requestID, "Completed", "Delivered");
                UpdateLog(objEmailBE1[0].RequestType, objEmailBE1[0].FromEmail, objEmailBE1[0].ToEmail, objEmailBE1[0].Subject, "");
            }
        }

        public void SendEmail(int requestID)
        {
            try
            {
                var message = SendGrid.GetInstance();
                //create an instance of the SMTP transport mechanism
                var transportInstance = SMTP.GetInstance(new System.Net.NetworkCredential(Constant.SMTP_USR, Constant.SMTP_PASS));

                List<EmailBE> objEmailBE1 = objEmailDA.GetRequest(requestID);
                if (objEmailBE1.Count > 0)
                {
                    message.Html = objEmailBE1[0].EmailContent;
                    message.Subject = objEmailBE1[0].Subject;
                    message.From = new MailAddress(objEmailBE1[0].FromEmail);

                    if (Constant.isEmailDebug == "1")
                        message.AddTo(Constant.DebugEmail);
                    else
                    {
                        foreach (EmailTo o in objEmailBE1[0].ToEmailList)
                        {
                            message.AddBcc(o.ToEmails);
                        }
                    }
                    transportInstance.Deliver(message);
                    objEmailDA.UpdateRequestStatus(requestID, "Completed", "Delivered");
                    UpdateLog(objEmailBE1[0].RequestType, objEmailBE1[0].FromEmail, objEmailBE1[0].ToEmail, objEmailBE1[0].Subject, "");
                }
            }
            catch (Exception ex)
            {
                //RecordLogToFS
                objUtil.RecordLogToFS(ex.Message);
            }
        }

        public void SendEmail(int requestID, string attachmentpath)
        {
            try
            {
                var message = SendGrid.GetInstance();
                //create an instance of the SMTP transport mechanism
                var transportInstance = SMTP.GetInstance(new System.Net.NetworkCredential(Constant.SMTP_USR, Constant.SMTP_PASS));

                List<EmailBE> objEmailBE1 = objEmailDA.GetRequest(requestID);
                if (objEmailBE1.Count > 0)
                {
                    message.Html = objEmailBE1[0].EmailContent;
                    message.Subject = objEmailBE1[0].Subject;
                    message.From = new MailAddress(objEmailBE1[0].FromEmail);
                    message.AddAttachment(attachmentpath);
                    if (Constant.isEmailDebug == "1")
                        message.AddTo(Constant.DebugEmail);
                    else
                    {
                        foreach (EmailTo o in objEmailBE1[0].ToEmailList)
                        {
                            message.AddBcc(o.ToEmails);
                        }
                    }
                    transportInstance.Deliver(message);
                    objEmailDA.UpdateRequestStatus(requestID, "Completed", "Delivered");
                    UpdateLog(objEmailBE1[0].RequestType, objEmailBE1[0].FromEmail, objEmailBE1[0].ToEmail, objEmailBE1[0].Subject, "");
                }
            }
            catch (Exception ex)
            {
                //RecordLogToFS
                objUtil.RecordLogToFS(ex.Message);
            }
        }

        private void UpdateLog(string mailAction, string mailFrom, string mailTo, string mailSubject, string actionInfo)
        {
            objEmailingLog.MailAction = mailAction;
            objEmailingLog.MailFrom = mailFrom;
            objEmailingLog.MailTo = mailTo;
            objEmailingLog.MailSubject = mailSubject;
            objEmailingLog.ActionInfo = actionInfo;
            objEmailDA.UpdateEmailLog(objEmailingLog);
        }

        public string getFormedEmailSubjectLine(string subjectWithTags, int webinarID)
        {
            string rtnStr = subjectWithTags;
            if (subjectWithTags.IndexOf("##") > 1)
            {
                List<GeneralWebinarTagsBO> objTags = new List<GeneralWebinarTagsBO>();
                ReportDA objRpt = new ReportDA();
                objTags = objRpt.getGeneralWebinarTagValues(webinarID);
                rtnStr = subjectWithTags.Replace("##EVENTTITLE##", objTags[0].WebinarList.Title);
            }
            return rtnStr;
        }

        public string getFormedEmailContent(string contentWithTags, string emailType, int webinarID, string dateformat = "")
        {
            string rtn = "";
            WebinarAllEmailTagsBO objTpl = new WebinarAllEmailTagsBO();
            ReportDA objRpt = new ReportDA();
            List<GeneralWebinarTagsBO> objTags = new List<GeneralWebinarTagsBO>();

            objTags = objRpt.getGeneralWebinarTagValues(webinarID);

            switch (emailType)
            {
                case "Attendee Followup":
                    if (objTags.Count > 0)
                    {
                        objTpl.WebinarTitle = objTags[0].WebinarList.Title;
                        objTpl.AudienceURL = objTags[0].WebinarURLList.AudienceInterfaceURL;
                        objTpl.UserEmail = objTags[0].UserEmail;
                        if (objTags[0].Registrantlist.Fld1 == null)
                            objTpl.RegistrantFirstName = "Registrant Name";
                        else
                            if (objTags[0].Registrantlist.Fld1.Trim() == "")
                                objTpl.RegistrantFirstName = "Registrant Name";
                            else
                                objTpl.RegistrantFirstName = objTags[0].Registrantlist.Fld1;
                    }
                    break;
                case "Confirmation Email":
                    if (objTags.Count > 0)
                    {
                        objTpl.WebinarTitle = objTags[0].WebinarList.Title;
                        objTpl.EventDate = objTags[0].WebinarList.StartDate.ToString(dateformat.Replace("MM", "MMM"));
                        objTpl.EventTime = Convert.ToDateTime(objTags[0].WebinarList.StartTime).ToString("h:mm tt");
                        objTpl.AudienceURL = objTags[0].WebinarURLList.AudienceInterfaceURL;
                        if (objTags[0].Registrantlist.Fld1 == null)
                            objTpl.RegistrantFirstName = "Registrant Name";
                        else
                            if (objTags[0].Registrantlist.Fld1.Trim() == "")
                                objTpl.RegistrantFirstName = "Registrant Name";
                            else
                                objTpl.RegistrantFirstName = objTags[0].Registrantlist.Fld1;
                    }
                    break;
                case "Non-Attendee Followup":
                    if (objTags.Count > 0)
                    {
                        objTpl.WebinarTitle = objTags[0].WebinarList.Title;
                        objTpl.AudienceURL = objTags[0].WebinarURLList.AudienceInterfaceURL;
                        if (objTags[0].Registrantlist.Fld1 == null)
                            objTpl.RegistrantFirstName = "Registrant Name";
                        else
                            if (objTags[0].Registrantlist.Fld1.Trim() == "")
                                objTpl.RegistrantFirstName = "Registrant Name";
                            else
                                objTpl.RegistrantFirstName = objTags[0].Registrantlist.Fld1;
                    }
                    break;
                case "Registrant Reminder Email":
                    if (objTags.Count > 0)
                    {
                        objTpl.WebinarTitle = objTags[0].WebinarList.Title;
                        objTpl.EventDate = objTags[0].WebinarList.StartDate.ToString(dateformat.Replace("MM", "MMM"));
                        objTpl.EventTime = Convert.ToDateTime(objTags[0].WebinarList.StartTime).ToString("h:mm tt");
                        objTpl.AudienceURL = objTags[0].WebinarURLList.AudienceInterfaceURL;
                        objTpl.TimeZoneName = objTags[0].TimeZoneName;
                        objTpl.WebinarID = objTags[0].WebinarList.WebinarID;
                        objTpl.RemainingDays = "";
                        if (objTags[0].Registrantlist.Fld1 == null)
                            objTpl.RegistrantFirstName = "Registrant Name";
                        else
                            if (objTags[0].Registrantlist.Fld1.Trim() == "")
                                objTpl.RegistrantFirstName = "Registrant Name";
                            else
                                objTpl.RegistrantFirstName = objTags[0].Registrantlist.Fld1;
                    }
                    break;
                case "Webinar Invitation":
                    if (objTags.Count > 0)
                    {
                        objTpl.RegistrationURL = objTags[0].WebinarURLList.RegistrationURL;
                        objTpl.WebinarTitle = objTags[0].WebinarList.Title;
                        objTpl.EventDate = objTags[0].WebinarList.StartDate.ToString(dateformat.Replace("MM", "MMM"));
                        objTpl.EventTime = Convert.ToDateTime(objTags[0].WebinarList.StartTime).ToString("h:mm tt");
                        objTpl.Description = objTags[0].WebinarList.Description;
                        objTpl.EndTime = Convert.ToDateTime(objTags[0].WebinarList.EndTime).ToString("h:mm tt");
                        objTpl.TimeZoneName = objTags[0].TimeZoneShortName;
                        AudiUrl = objTags[0].WebinarURLList.AudienceInterfaceURL;
                    }
                    rtn = contentWithTags;
                    break;
                case "Webinar Cancellation":
                    if (objTags.Count > 0)
                    {
                        objTpl.WebinarTitle = objTags[0].WebinarList.Title;
                        objTpl.EventTime = Convert.ToDateTime(objTags[0].WebinarList.StartTime).ToString("h:mm tt");
                        objTpl.EventDate = objTags[0].WebinarList.StartDate.ToString(dateformat.Replace("MM", "MMM"));
                        if (objTags[0].Registrantlist.Fld1 == null)
                            objTpl.RegistrantFirstName = "Registrant Name";
                        else
                            if (objTags[0].Registrantlist.Fld1.Trim() == "")
                                objTpl.RegistrantFirstName = "Registrant Name";
                            else
                            objTpl.RegistrantFirstName = objTags[0].Registrantlist.Fld1;
                    }
                    break;
            }
            EBird.Framework.TemplateMgmt objTemplate = new EBird.Framework.TemplateMgmt();
            rtn = objTemplate.GetContentForAnyWebinarEmail(objTpl, contentWithTags, false, emailType, dateformat);
            return rtn;
        }

        public string getHTMLFormattedEmailContent(string contentWithTags, string emailType, int webinarID, string dateformat="")
        {
            TemplateMgmt objTpl = new TemplateMgmt();
            string strTpl = objTpl.GetGeneralEmailTpl(Constant.DocTemplate + "generalEmail.tpl");
            if (strTpl != "")
            {
                ReportUtils objRptUtil = new ReportUtils();
                WeeklyReports objRpt = new WeeklyReports();
                List<WebinarEmailBE> objWBEmail = new List<WebinarEmailBE>();

                objWBEmail = objEmailDA.GetWebinarEmail(webinarID, emailType);
                strTpl = objRptUtil.getCSSDefns(objWBEmail[0].ThemeID, strTpl);
                //strTpl = strTpl.Replace("##STYLE##", objRptUtil.getCSSDefns(objWBEmail[0].ThemeID));
                strTpl = strTpl.Replace("##HEADER##", objRptUtil.getEmailRptHeader(webinarID));
                strTpl = strTpl.Replace("##CONTENT##", getFormedEmailContent(contentWithTags, emailType, webinarID, dateformat));
                if (objWBEmail[0].IsSystemReq)
                    strTpl = strTpl.Replace("##SYSREQ##", objRptUtil.getSystemRequirement(AudiUrl));
                else
                    strTpl = strTpl.Replace("##SYSREQ##", "");
                strTpl = strTpl.Replace("##FOOTER##", objRptUtil.getEmailRptFooter());
            }
            return strTpl;
        }

        public string getHTMLFormattedWebinarPresenterContact(string emailType, int webinarID)
        {
            TemplateMgmt objTpl = new TemplateMgmt();
            string strTpl = objTpl.GetGeneralEmailTpl(Constant.DocTemplate + "PresenterContact.tpl");
            if (strTpl != "")
            {
                ReportUtils objRptUtil = new ReportUtils();
                //WeeklyReports objRpt = new WeeklyReports();
                List<WebinarTheme> objWBTheme = new List<WebinarTheme>();
                List<WebinarEmailBE> objWBE = objEmailDA.GetWebinarEmailDefault(1, "Presenter Contact");
                
                objWBTheme = objWebinarDA.getWebinarTheme(webinarID);
                strTpl = objRptUtil.getCSSDefns(objWBTheme[0].ThemeLayoutID, strTpl);
                strTpl = strTpl.Replace("##HEADER##", objRptUtil.getEmailRptHeader());
                strTpl = strTpl.Replace("##CONTENT##", objWBE[0].EmailContent);
                strTpl = strTpl.Replace("##FOOTER##", objRptUtil.getEmailRptFooter()); 
            }
            return strTpl;
        }

        public string getHTMLFormattedPasswdChangeNotify(string emailID, int languageID=1)
        {
            TemplateMgmt objTpl = new TemplateMgmt();
            string strTpl = objTpl.GetGeneralEmailTpl(Constant.DocTemplate + "PasswdChange.tpl");
            if (strTpl != "")
            {
                ReportUtils objRptUtil = new ReportUtils();
                UserDA objUDA = new UserDA();
                List<UserBE> objUBE = objUDA.GetUserDetailDA(emailID);
                List<WebinarEmailBE> objWBE = objEmailDA.GetGeneralEmailDefault("Password Changed",languageID);
               
                strTpl = strTpl.Replace("##HEADER##", objRptUtil.getEmailRptHeader());
                strTpl = strTpl.Replace("##CONTENT##", objWBE[0].EmailContent);
                strTpl = strTpl.Replace("##FOOTER##", objRptUtil.getEmailRptFooter());
                strTpl = strTpl.Replace("##PSWDCHANGEDATETIME##", objUBE[0].PasswordChangedOn);
            }
            return strTpl;
        }

        public string getHTMLFormattedNewAccountNotify(string emailID, string passwd, string gendate, int languageID=1)
        {
            TemplateMgmt objTpl = new TemplateMgmt();
            string strTpl = objTpl.GetGeneralEmailTpl(Constant.DocTemplate + "NewUserAccount.tpl");
            if (strTpl != "")
            {
                ReportUtils objRptUtil = new ReportUtils();
                UserDA objUDA = new UserDA();
                List<UserBE> objUBE = objUDA.GetUserDetailDA(emailID);
                List<WebinarEmailBE> objWBE = objEmailDA.GetGeneralEmailDefault("New User Account", languageID);
               
                strTpl = strTpl.Replace("##HEADER##", objRptUtil.getEmailRptHeader());
                strTpl = strTpl.Replace("##CONTENT##", objWBE[0].EmailContent);
                strTpl = strTpl.Replace("##FOOTER##", objRptUtil.getEmailRptFooter());
                strTpl = strTpl.Replace("##ACCTDATETIME##", gendate);
                strTpl = strTpl.Replace("##USEREMAILADDRESS##", emailID);
                strTpl = strTpl.Replace("##PASSWORD##", passwd);
            }
            return strTpl;
        }

        public string getHTMLFormattedGeneralEmail(string reqType,int languageID = 1)
        {
            TemplateMgmt objTpl = new TemplateMgmt();
            string strTpl = objTpl.GetGeneralEmailTpl(Constant.DocTemplate + "generalEmail1.tpl");
            if (strTpl != "")
            {
                ReportUtils objRptUtil = new ReportUtils();
                List<WebinarEmailBE> objWBE = objEmailDA.GetGeneralEmailDefault(reqType, languageID);

                strTpl = strTpl.Replace("##HEADER##", objRptUtil.getEmailRptHeader());
                strTpl = strTpl.Replace("##CONTENT##", objWBE[0].EmailContent);
                strTpl = strTpl.Replace("##FOOTER##", objRptUtil.getEmailRptFooter());
            }
            return strTpl;
        }
    }
}

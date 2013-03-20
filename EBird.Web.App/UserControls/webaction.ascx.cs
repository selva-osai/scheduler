using System;
using System.Text;
using System.Collections.Generic;
using EBird.BusinessEntity;
using EBird.Common;
using EBird.DataAccess;
using System.Collections;
using Microsoft.Office.Interop.Outlook;

namespace EBird.Web.App.UserControls
{
    public partial class webaction : System.Web.UI.UserControl
    {
        WebinarBE objWebinarBE = new WebinarBE();
        EBirdUtility objUtil = new EBirdUtility();
        WebinarDA objWebinarDA = new WebinarDA();
        EBErrorMessages objError = new EBErrorMessages();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (Session["WebinarID"] != null || Request["cmd"] != null)
                {
                    string webinarID = "";
                    char[] separator = new char[] { ',' };
                    if (Session["WebinarID"] != null)
                        webinarID = Session["WebinarID"].ToString();
                    if (Request["cmd"] != null)
                    {
                        webinarID = "URL," + Request["cmd"].ToString();
                        tbHeader.Visible = false;
                    }

                    string[] strSplitArr = webinarID.Split(separator);
                    if (strSplitArr.Length > 1)
                    {
                        hWebinarID.Value = strSplitArr[1];
                        Session["WebinarID"] = strSplitArr[1];  // resetting the session able with ID alone, as this throws error when navigated back to schedule
                        List<WebinarBE> objWebinarBE = objWebinarDA.GetWebinarDetailDA(Convert.ToInt32(strSplitArr[1]));
                        if (objWebinarBE.Count > 0)
                        {
                            lblWebinarTitle.Text = "<b>Webinar Title</b> - " + objWebinarBE[0].Title;
                            lblTime.Text = Convert.ToDateTime(objWebinarBE[0].StartDate).ToString("MMM dd, yyyy") + " - " + Convert.ToDateTime(objWebinarBE[0].StartTime).ToString("h:mm tt");
                            ltrStatus.Text = objWebinarBE[0].WebinarStatus;
                            //Following fields for for webinar savesas section
                            txtWebinarTitle.Text = objWebinarBE[0].Title;
                            //txtDescription.Text = objWebinarBE[0].Description;
                            redtSummary1.Content = objWebinarBE[0].Description;

                        }
                        switch (strSplitArr[0])
                        {
                            case "URL":
                                mvWebAction.SetActiveView(vwURL);
                                getURLs(Convert.ToInt32(hWebinarID.Value));
                                break;
                            case "SCH":
                                mvWebAction.SetActiveView(vwSchSame);
                                //.MinDate = DateTime.Now;
                                
                                rdtStartDate1.DateInput.DisplayDateFormat = Session["Client_DateFormat"].ToString();
                                rdtStartDate1.DateInput.DateFormat = Session["Client_DateFormat"].ToString();
                                rdtStartTime.MinDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

                                CheckReschedulingWebinar();
                                break;
                            case "EML":
                                mvWebAction.SetActiveView(vwEmailRegistrant);
                                getEmailFieldSetting();
                                break;
                            case "OUT":
                                mvWebAction.SetActiveView(vwOutlook);
                                break;
                            case "CAN":
                                mvWebAction.SetActiveView(vwCanWebinar);
                                CheckCancelWebinar();
                                break;
                            case "DEL":
                                mvWebAction.SetActiveView(vwDelWebinar);
                                CheckDeleteWebinar();
                                break;
                        }
                    }
                }
                else
                    mvWebAction.SetActiveView(vwInvalid);
            }
        }

        protected void lbtnBack_Click(object sender, EventArgs e)
        {
            Session["WebinarID"] = hWebinarID.Value;
            Response.Redirect("~/Pages/Schedule");
        }

        #region Reschedule same webinar

        private void CheckReschedulingWebinar()
        {
            if (ltrStatus.Text.ToUpper() == "DRAFT")
            {
                phWebinarSchNotActive.Visible = true;
                phSchSameWebinar.Visible = false;
            }
            else
            {
                phWebinarSchNotActive.Visible = false;
                phSchSameWebinar.Visible = true;
            }
        }

        protected void btnSaveWebinar_Click(object sender, EventArgs e)
        {
            lblWebSaveAsError.Text = "";
            if ((Convert.ToDateTime(rdtStartDate1.SelectedDate) - DateTime.Now).Days < 0)
            {
                lblWebSaveAsError.Text = "Cannot schedule webinar in the past";
            }
            else if ((Convert.ToDateTime(rdtStartDate1.SelectedDate) - DateTime.Now).Days == 0)
            {
                UserDA objUserDA = new UserDA();
                float tm = objUserDA.getTimezoneToServerTimeDiff(Convert.ToInt32(ddlTimeZone.SelectedValue));
                DateTime currentClientPreferDateTime = DateTime.Now.AddHours(-tm);

                if ((Convert.ToDateTime(rdtStartTime.SelectedDate) - Convert.ToDateTime(currentClientPreferDateTime)).Minutes < 0)
                    lblWebSaveAsError.Text = "Cannot schedule webinar past the current time";
            }
            if (lblWebSaveAsError.Text == "")
            {
                if (objWebinarDA.IsWebinarOverlapping(objUtil.FormDBDate(Convert.ToDateTime(rdtStartDate1.SelectedDate)), rdtStartTime.SelectedDate.ToString(), rdEndTime.SelectedDate.ToString(), Convert.ToInt32(Session["UserID"]), Convert.ToInt32(hWebinarID.Value)))
                {
                    lblWebSaveAsError.Text = objError.getMessage("WB0008");
                }
                else
                {
                    WebinarBE objWebinarBE = new WebinarBE();
                    objWebinarBE.WebinarID = Convert.ToInt32(hWebinarID.Value);
                    objWebinarBE.Createdby = Convert.ToInt32(Session["UserID"]);
                    objWebinarBE.Title = txtWebinarTitle.Text;
                    //objWebinarBE.Description = txtDescription.Text;
                    objWebinarBE.Description = redtSummary1.Content;
                    objWebinarBE.StartDate = Convert.ToDateTime(rdtStartDate1.SelectedDate);
                    objWebinarBE.StartTime = Convert.ToDateTime(rdtStartTime.SelectedDate).ToString("HH:mm:ss");
                    objWebinarBE.EndTime = Convert.ToDateTime(rdEndTime.SelectedDate).ToString("HH:mm:ss");
                    objWebinarBE.TimeZoneID = Convert.ToInt32(ddlTimeZone.SelectedValue);

                    objWebinarDA.SaveWeibarScheduleAs(objWebinarBE);

                    Response.Redirect("~/Pages/Webinar");
                }
            }
        }

        #endregion

        #region Webinar URLs
        private void getURLs(int webinarID)
        {
            List<WebinarURLs> objURL = new List<WebinarURLs>();
            objURL = objWebinarDA.GetWebinarURLsDA(webinarID);
            if (objURL.Count > 0)
            {
                if (objURL[0].RegistrationURL != "")
                {
                    chkRegURL.Enabled = true;
                    hlnkReg.Text = Constant.WebinarbaseURL + objURL[0].RegistrationURL;
                    hlnkReg.NavigateUrl = hlnkReg.Text;
                }
                if (objURL[0].PreviewInterfaceURL != "")
                {
                    chkPreURL.Enabled = true;
                    hlnkPre.Text = Constant.WebinarPreviewBaseURL + objURL[0].PreviewInterfaceURL;
                    hlnkPre.NavigateUrl = hlnkPre.Text;
                }

                //if (objURL[0].AudienceInterfaceURL != "")
                //{
                //    chkAudi.Enabled = true;
                //    hlnkAudi.Text = Constant.WebinarViewerBaseURL + objURL[0].AudienceInterfaceURL;
                //    hlnkAudi.NavigateUrl = hlnkAudi.Text;
                //} 
                if (objURL[0].CommandCenterURL != "")
                {
                    chkCC.Enabled = true;
                    hlnkCC.Text = Constant.WebinarCoCBaseURL + objURL[0].CommandCenterURL;
                    hlnkCC.NavigateUrl = hlnkCC.Text;
                }
                if (objURL[0].AnalyticsURL != "")
                {
                    chkAnalysis.Enabled = true;
                    hlnkAnalysis.Text = Constant.WebinarAnalyticsBaseURL + objURL[0].AnalyticsURL;
                    hlnkAnalysis.NavigateUrl = hlnkAnalysis.Text;
                }
            }
        }

        protected void btnEmailURLs_Click(object sender, EventArgs e)
        {
            //if (chkRegURL.Checked || chkPreURL.Checked || chkAudi.Checked || chkCC.Checked || chkAnalysis.Checked)
            if (chkRegURL.Checked || chkPreURL.Checked || chkCC.Checked || chkAnalysis.Checked)
            {
                lblErrorURL.Text = "";
                lblInvalidEmails.Text = "";
                //lblURLmsg.Text = "";

                if (txtEmails.Text.Trim() == "")
                {
                    lblErrorURL.Text = objError.getMessage("GE0002");
                }
                else
                {
                    string invalidEmail = "";
                    ArrayList values = objUtil.StringToArrayList(txtEmails.Text.Trim(), new char[] { ';' });
                    List<EmailAddressBO> objEmailAddress = new List<EmailAddressBO>();

                    for (int idx = 0; idx < values.Count; idx++)
                    {
                        if (!objUtil.isEmail(values[idx].ToString()))
                            invalidEmail += values[idx].ToString() + ", ";
                        else
                        {
                            objEmailAddress.Add(new EmailAddressBO { EmailAddress = values[idx].ToString() });
                        }
                    }
                    if (invalidEmail != "")
                        lblInvalidEmails.Text = "Invalid emails entered " + invalidEmail;
                    else
                    {
                        StringBuilder sb = new StringBuilder("Webinar URLs for webinar <i>" + lblWebinarTitle.Text + "</i><br><br>");
                        if (chkRegURL.Checked)
                            sb.Append("Registration URL<br>" + hlnkReg.Text + "<br><br>");
                        if (chkPreURL.Checked)
                            sb.Append("Preview Interface URL<br>" + hlnkPre.Text + "<br><br>");
                        //if (chkAudi.Checked)
                        //    sb.Append("Audience Interface URL<br>" + hlnkAudi.Text + "<br><br>");
                        if (chkCC.Checked)
                            sb.Append("Command Center URL<br>" + hlnkCC.Text + "<br><br>");
                        if (chkAnalysis.Checked)
                            sb.Append("Analytics URL<br>" + hlnkAnalysis.Text + "<br><br>");

                        int requestID = SaveEmail("Webinar URLs Emailing", "Webinar: " + lblWebinarTitle.Text + " URLs", txtEmails.Text.Trim(), sb.ToString());
                        UpdateToEmails(objEmailAddress, "Email Webinar URLs", requestID);
                        lblError.CssClass = "msgSuccess";
                        lblError.Text = "Successfully emailed the URLs";
                        txtEmails.Text = "";
                        //chkRegURL.Checked = chkPreURL.Checked = chkAudi.Checked = chkCC.Checked = chkAnalysis.Checked = false;
                        chkRegURL.Checked = chkPreURL.Checked = chkCC.Checked = chkAnalysis.Checked = false;
                    }
                }
            }
            else
            {
                lblErrorURL.Text = "At least one URL has to be selected...";
            }
        }
        #endregion

        #region Add to outlook
        protected void btnOutAdd_Click(object sender, EventArgs e)
        {
            try
            {
                List<WebinarBE> objWeb = objWebinarDA.GetWebinarDetailDA(Convert.ToInt32(hWebinarID.Value));
                if (objWeb.Count > 0)
                {

                    _Application olApp = (_Application)new Application();
                    NameSpace mapiNS = olApp.GetNamespace("MAPI");

                    string profile = "";
                    mapiNS.Logon(profile, null, null, null);

                    _AppointmentItem apt = (_AppointmentItem)olApp.CreateItem(OlItemType.olAppointmentItem);

                    // set some properties
                    apt.Subject = objWeb[0].Title;
                    apt.Body = objWeb[0].Description;

                    ArrayList t1 = objUtil.StringToArrayList(objWeb[0].StartTime, new char[] { ':' });
                    if (t1.Count >= 3)
                        apt.Start = new DateTime(objWeb[0].StartDate.Year, objWeb[0].StartDate.Month, objWeb[0].StartDate.Day, Convert.ToInt32(t1[0]), Convert.ToInt32(t1[1]), Convert.ToInt32(t1[2]));

                    ArrayList t2 = objUtil.StringToArrayList(objWeb[0].EndTime, new char[] { ':' });
                    if (t2.Count >= 3)
                        apt.End = new DateTime(objWeb[0].StartDate.Year, objWeb[0].StartDate.Month, objWeb[0].StartDate.Day, Convert.ToInt32(t2[0]), Convert.ToInt32(t2[1]), Convert.ToInt32(t2[2]));

                    apt.ReminderMinutesBeforeStart = 15;           // Number of minutes before the event for the remider
                    apt.BusyStatus = OlBusyStatus.olTentative;     // Makes it appear bold in the calendar

                    apt.AllDayEvent = false;
                    apt.Location = "Web Presentation";
                    apt.Save();
                    lblOutLookMsg.Text = "The webinar successfully added to your MS Outlook Calender";
                    btnOutAdd.Visible = false;
                    btnOutCancel.Visible = false;
                    #region ics method
                    //vCalendar objCal = new vCalendar();
                    //vCalendar.vEvent objEvent = new vCalendar.vEvent();
                    //objEvent.Description = objWeb[0].Description;
                    //objEvent.DTEnd = Convert.ToDateTime(objWeb[0].EndTime);
                    //objEvent.DTStamp = Convert.ToDateTime(objWeb[0].StartTime);
                    //objEvent.DTStart = objWeb[0].StartDate;
                    //objEvent.Location = "Online Presentation";
                    //objEvent.Organizer = objWeb[0].Createdby.ToString();
                    //objEvent.Summary = objWeb[0].Title;
                    //objEvent.UID = hWebinarID.Value;
                    //objEvent.URL = ""; 
                    //vCalendar.vEvents objEvents = new vCalendar.vEvents();
                    //objEvents.Add(objEvent);
                    //objCal.Events = objEvents;
                    //Response.Clear();
                    ///////First, clean-up the response.object
                    //Response.Charset = "";
                    //Response.ContentType = "application/outlook";
                    ///////Set the response mime type for Outlook.
                    //Response.AddHeader("Content-Disposition", "attachment; filename=appointment.ics");
                    ///////Add the disposition header so that you can name the output file.
                    //Response.Write(objCal.ToString());
                    ///////Output the file.
                    //Response.End();
                    #endregion
                }
            }
            catch (System.Exception ex)
            {
                lblOutLookErr.Text = objError.getMessage("GE0001");
            }
        }
        #endregion

        #region Send email to registrants

        private void getEmailFieldSetting()
        {
            btnSendEmail.Enabled = false;
            chkRegistered.Checked = chkAttended.Checked = chkAttendedLive.Checked = chkOnDemand.Checked = chkDidNotAttend.Checked = false;
            List<WebinarBE> objWeb = objWebinarDA.GetWebinarDetailDA(Convert.ToInt32(hWebinarID.Value));
            if (objWeb.Count > 0)
            {
                txtSendMeEmailAddress.Text = Session["EmailID"].ToString();
                chkRegistered.Text = "Registered for Webinar (" + objWeb[0].Registered.ToString() + ")";
                chkAttended.Text = "Attended Webinar (0)";
                chkAttendedLive.Text = "Attended Webinar - Viewed Live (" + objWeb[0].Live.ToString() + ")";
                chkOnDemand.Text = "Attended Webinar - Viewed OnDemand (" + objWeb[0].OnDemand.ToString() + ")";
                chkDidNotAttend.Text = "Registered but did not attend (0)";

                if (objWeb[0].OnDemand < 1)
                {
                    chkOnDemand.Enabled = false;
                    chkOnDemand.ForeColor = System.Drawing.Color.Gray;
                }

                if (objWeb[0].Live < 1)
                {
                    chkAttendedLive.Enabled = false;
                    chkAttendedLive.ForeColor = System.Drawing.Color.Gray;
                }
                if (objWeb[0].Registered < 1)
                {
                    chkRegistered.Enabled = false;
                    chkRegistered.ForeColor = System.Drawing.Color.Gray;
                }
                if (objWeb[0].WebinarStatus != "Completed")
                {
                    chkAttended.Enabled = false;
                    chkAttended.ForeColor = System.Drawing.Color.Gray;
                    chkDidNotAttend.Enabled = false;
                    chkDidNotAttend.ForeColor = System.Drawing.Color.Gray;
                }
            }
            if (chkRegistered.Enabled || chkAttended.Enabled || chkAttendedLive.Enabled || chkOnDemand.Enabled || chkDidNotAttend.Enabled)
                btnSendEmail.Enabled = true;
            //else
            //    lblError.Text = "No registration group has registrant to email";
        }

        protected void btnSendEmail_Click(object sender, EventArgs e)
        {
            lblError.Text = "";
            if (txtSubject.Text == "" || redEmailRegistrants.Content == "")
                lblError.Text = "Subject or email content cannot be empty";
            else
            {
                if (chkDidNotAttend.Checked || chkAttended.Checked || chkAttendedLive.Checked ||
                      chkOnDemand.Checked || chkRegistered.Checked)
                {
                    lblError.Text = "";
                    EmailBE objEmailBE = new EmailBE();
                    EmailDA objEmailDA = new EmailDA();
                    //objEmailBE.isToEmailRef = true;
                    //objEmailBE.RequestStatus = "Queued";
                    //objEmailBE.RequestType = "Webinar Registrant Emailing";
                    //objEmailBE.Subject = txtSubject.Text;
                    //objEmailBE.SubmittedBy = Convert.ToInt32(Session["UserID"]);
                    //if (txtSendMeEmailAddress.Text != "")
                    //    objEmailBE.ToEmail = txtSendMeEmailAddress.Text;
                    //objEmailBE.FromEmail = Session["EmailID"].ToString();
                    //objEmailBE.EmailContent = redEmailRegistrants.Content;
                    //int requestID = objEmailDA.SaveEmailRequest(objEmailBE);

                    int requestID = SaveEmail("Webinar Registrant Emailing", txtSubject.Text.Trim(), txtSendMeEmailAddress.Text.Trim(), redEmailRegistrants.Content);
                    if (requestID != 0)
                    {
                        List<EmailAddressBO> objEmailAddress = new List<EmailAddressBO>();
                        if (chkDidNotAttend.Checked)
                        {
                            objEmailAddress = objEmailDA.GetToEmailAddress(Convert.ToInt32(hWebinarID.Value), "NOTATTENDED");
                            UpdateToEmails(objEmailAddress, "Email Registrant - NotAttended", requestID);
                        }
                        if (chkAttended.Checked)
                        {
                            objEmailAddress = objEmailDA.GetToEmailAddress(Convert.ToInt32(hWebinarID.Value), "ATTENDED");
                            UpdateToEmails(objEmailAddress, "Email Registrant - Attended", requestID);
                        }
                        if (chkAttendedLive.Checked)
                        {
                            objEmailAddress = objEmailDA.GetToEmailAddress(Convert.ToInt32(hWebinarID.Value), "LIVE");
                            UpdateToEmails(objEmailAddress, "Email Registrant - Attended Live", requestID);
                        }
                        if (chkOnDemand.Checked)
                        {
                            objEmailAddress = objEmailDA.GetToEmailAddress(Convert.ToInt32(hWebinarID.Value), "ONDEMAND");
                            UpdateToEmails(objEmailAddress, "Email Registrant - Attended Ondemand", requestID);
                        }
                        if (chkRegistered.Checked)
                        {
                            objEmailAddress = objEmailDA.GetToEmailAddress(Convert.ToInt32(hWebinarID.Value), "REGISTERED");
                            UpdateToEmails(objEmailAddress, "Email Registrant - All registered", requestID);
                        }

                        lblError.Text = "Successfully emailed to selected registrant group";
                        lblError.CssClass = "msgSuccess";
                    }
                }
                else
                {
                    lblError.Text = "At least one registration group has to be selected";
                }
            }
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
        #endregion

        #region Cancel Webinar
        private void CheckCancelWebinar()
        {
            if (ltrStatus.Text.ToUpper() == "DRAFT")
            {
                lblCancelInstruction.Text = "This Webinar will be deleted";
                hlnkCanPreviewEmail.Visible = false;
            }
            else
            {
                lblCancelInstruction.Text = "This Webinar will be deleted and a notification will be sent to all participants.";
                hlnkCanPreviewEmail.Visible = true;
            }
        }

        protected void btnCanWebinar_Click(object sender, EventArgs e)
        {
            objWebinarDA.UpdateWebinarStatus(Convert.ToInt32(hWebinarID.Value), "Inactive", Convert.ToInt32(Session["UserID"]));

            //if (ltrStatus.Text.ToUpper() == "ACTIVE")
            //{
            //    EmailDA objEmailDA = new EmailDA();
            //    int requestID = SaveEmail("Webinar Delete - Registrant notification", "Cancellation of webinar - ", "", "Cancellation of webinar email body content");
            //    List<EmailAddressBO> objEmailAddress = objEmailDA.GetToEmailAddress(Convert.ToInt32(hWebinarID.Value), "REGISTERED");
            //    UpdateToEmails(objEmailAddress, "Email Registrant - All registered", requestID);
            //}
            Response.Redirect("~/Pages/Webinar");
        }

        protected void btnCanCancel_Click(object sender, EventArgs e)
        {
            //lbtnBack_Click(null, null);
            hModalStatusFlg.Value = "1";
            string url = System.IO.Path.GetFileName(Request.Path);
            if (url.ToLower() == "webinaraction")
            {
                Session["WebinarID"] = hWebinarID.Value;
                Response.Redirect("~/Pages/Schedule");
            }
        }

        #endregion

        #region Delete Webinar

        private void CheckDeleteWebinar()
        {

        }

        protected void btnDelWebinar_Click(object sender, EventArgs e)
        {
            objWebinarDA.UpdateWebinarStatus(Convert.ToInt32(hWebinarID.Value), "Deleted", Convert.ToInt32(Session["UserID"]));
            Response.Redirect("~/Pages/Webinar");
        }
        #endregion

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
    }
}
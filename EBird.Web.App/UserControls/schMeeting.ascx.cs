using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using EBird.BusinessEntity;
using EBird.Common;
using EBird.DataAccess;
using Telerik.Web.UI;
using System.Collections.ObjectModel;
using EBird.Email;

namespace EBird.Web.App.UserControls
{
    public partial class schMeeting : System.Web.UI.UserControl
    {
        int tabCnt;
        WebinarBE objWebinarBE = new WebinarBE();
        EBirdUtility objUtil = new EBirdUtility();
        WebinarDA objWebinarDA = new WebinarDA();
        EBErrorMessages objError = new EBErrorMessages();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //rdtStartTime.MinDate = DateTime.Now;

                if (Session["WebinarID"] != null)
                {
                    rdtStartDate.DateInput.DisplayDateFormat = Session["Client_DateFormat"].ToString();
                    rdtStartDate.DateInput.DateFormat = Session["Client_DateFormat"].ToString();

                    rdtEndBy.DateInput.DisplayDateFormat = Session["Client_DateFormat"].ToString();
                    rdtEndBy.DateInput.DateFormat = Session["Client_DateFormat"].ToString();

                    hWebinarID.Value = Session["WebinarID"].ToString();
                    pnlRecurr.Attributes.Add("style", "display:none");
                    // get webinar schedule details
                    getWebinarDetails(Convert.ToInt32(hWebinarID.Value));
                    setWebinarIDToTabs(hWebinarID.Value, false);
                    fvWebTitle.Visible = true;
                    if (Session["PREMIUM_FEATURE"].ToString().IndexOf(",4,") < 0)
                    {
                        spPre.Visible = true;
                        chkEmailRegAPI.Enabled = false;
                    }
                    Session.Remove("WebinarID");
                }
                else
                {
                    pnlRecurr.Attributes.Add("style", "display:none");

                }
                // Commented on 12/04/2012 - J
                //if (objWebinarDA.IsWebinarRequiredPassword(Convert.ToInt32(Session["ClientID"])))
                //{
                //    chkReqPassword.Enabled = true;
                //    dvPass.Visible = true;
                //    //lblReqPassword.Enabled = true;
                //}
                //else
                //{
                //    chkReqPassword.Enabled = false;
                //    chkReqPassword.ForeColor = System.Drawing.Color.Gray;
                //    dvPass.Visible = false;
                //    //lblReqPassword.Enabled = false;
                //    //lblReqPassword.ForeColor = System.Drawing.Color.Gray;    
                //}
                if (Session["Web_Tab"] != null)
                {
                    switch (Session["Web_Tab"].ToString())
                    {
                        case "2":
                            lnkSetupReg_Click(null, null);
                            break;
                        case "3":
                            lnkAudView_Click(null, null);
                            break;
                        case "4":
                            lnkEmailNotify_Click(null, null);
                            break;
                    }

                    Session.Remove("Web_Tab");
                }
                else
                    mvSchedule.SetActiveView(vwWeb);
                rcmbActions.Text = "Webinar Actions";
            }
        }

        private void setWebinarIDToTabs(string sWebinarID, bool isNew)
        {
            hWebinarID.Value = sWebinarID;
            webTheme1.WebinarID = hWebinarID.Value;
            webAudience1.WebinarID = hWebinarID.Value;
            webRegistration1.WebinarID = hWebinarID.Value;
            webEmail1.WebinarID = hWebinarID.Value;
            if (isNew)
            {
                webRegistration1.regTabLoad();
                webAudience1.audTabLoad();
                webEmail1.GetWebinarEmailSettings();
            }
            if (hIsPast.Value == "1")
            {
                webRegistration1.isWebinarPast = true;
                webAudience1.isWebinarPast = true;
                webEmail1.isWebinarPast = true;
                btnNext.Text = "Continue";
                btnSave.Text = "My Webinars";
            }
        }

        private void getWebinarDetails(int webinarID)
        {
            if (webinarID != 0)
            {
                rcmbActions.Visible = true;
                List<WebinarBE> objWebinar = new List<WebinarBE>();
                objWebinar = objWebinarDA.GetWebinarDetailDA(webinarID);
                if (objWebinar.Count > 0)
                {
                    hWebinarStatus.Value = objWebinar[0].WebinarStatus.ToUpper();

                    if (objWebinar[0].WebinarStatus == "COMPLETED" || objWebinar[0].WebinarStatus == "CANCELLED")
                    {
                        btnNext.Visible = btnPrev.Visible = btnSave.Visible = false;
                    }
                    lblWebinarTitle.Text = "<b>Webinar Title</b> - " + objWebinar[0].Title;
                    txtWebinarTitle.Text = objWebinar[0].Title;
                    //txtDescription.Text = objWebinar[0].Description;
                    redtSummary.Content = objWebinar[0].Description;

                    rdtStartTime.DbSelectedDate = Convert.ToDateTime(objWebinar[0].StartDate.ToShortDateString() + ' ' + objWebinar[0].StartTime);
                    rdEndTime.SelectedDate = Convert.ToDateTime(objWebinar[0].EndTime);

                    //rdtStartTime.SelectedDate = Convert.ToDateTime(objWebinar[0].StartTime);
                    //rdEndTime.SelectedDate = Convert.ToDateTime(objWebinar[0].EndTime);

                    rdtStartDate.MinDate = objWebinar[0].StartDate;
                    rdtStartTime.MinDate = objWebinar[0].StartDate;
                    rdtStartDate.SelectedDate = objWebinar[0].StartDate;

                    ddlTimeZone.SelectedValue = objWebinar[0].TimeZoneID.ToString();
                    //Presenters Options 
                    if (objWebinar[0].DeliveryChannel == "1")
                        radioBtnId11.Checked = true;
                    else if (objWebinar[0].DeliveryChannel == "2")
                        radioBtnId22.Checked = true;
                    else if (objWebinar[0].DeliveryChannel == "3")
                        radioBtnId33.Checked = true;
                    else
                    {
                        //Default
                        radioBtnId11.Checked = true;
                        radioBtnId22.Checked = false;
                        radioBtnId33.Checked = false;
                    }
                    chkEmailRegAPI.Checked = objWebinar[0].isPublic;
                    chkRecurrence.Checked = objWebinar[0].isRecurrence;
                    if (objWebinar[0].isRecurrence)
                    {
                        pnlRecurr.Attributes.Add("style", "display: block");
                        popRecurrence();
                    }
                    else
                    {
                        pnlRecurr.Attributes.Add("style", "display: none;");
                    }
                    //if (chkReqPassword.Enabled)
                    //{
                    //    chkReqPassword.Checked = objWebinar[0].isPasswordRequired;
                    //}

                    DateTime edt1 = Convert.ToDateTime(objWebinar[0].StartDate.ToShortDateString() + ' ' + objWebinar[0].EndTime);

                    TimeSpan ts = edt1 - DateTime.Now;
                    //TimeSpan ts = objWebinar[0].StartDate - DateTime.Now;

                    if (ts.Days < 0 || objWebinar[0].WebinarStatus != "Active")
                    {
                        rcmbActions.Items.Add(new RadComboBoxItem("Delete Webinar", "DEL"));
                    }
                    if (ts.Days < 0)
                    {
                        hIsPast.Value = "1";
                        rcmbActions.Items.Remove(3);
                        // after deleting item, the cmb items get re-indexed, so to remove 4 from original order, had use index 3 again
                        rcmbActions.Items.Remove(3);
                    }
                }
                else
                {
                    pnlRecurr.Attributes.Add("style", "display: none;");
                    //rdtStartDate.SelectedDate = DateTime.Parse("08:00:00"); 
                }

            }
        }

        private void popRecurrence()
        {
            if (hWebinarID.Value != "0")
            {
                char[] separator = new char[] { ':' };

                List<WebinarRecurrencyBE> objWebRec = new List<WebinarRecurrencyBE>();
                objWebRec = objWebinarDA.GetWebinarRecurrencyDetail(Convert.ToInt32(hWebinarID.Value));
                if (objWebRec.Count > 0)
                {
                    rbtnDurationType.SelectedValue = objWebRec[0].recurrType;
                    //if (objWebRec[0].endType.ToString().ToUpper() == "NOEND")
                    //    rbtnNoEnddate.Checked = true;
                    if (objWebRec[0].endType.ToString().ToUpper() == "AFTER")
                    {
                        rbtnEndAfter.Checked = true;
                        txtEndAfter.Text = objWebRec[0].endValue;
                    }
                    else
                    {
                        rbtnEndBy.Checked = true;
                        rdtEndBy.SelectedDate = Convert.ToDateTime(objWebRec[0].endValue);
                    }
                    string recurrVal = objWebRec[0].recurrCriteria;
                    string[] str1 = recurrVal.Split(separator);
                    switch (str1[0].ToString())
                    {
                        //case "H":
                        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "OpenPage", "javascript:showOnlyRadioDiv('#ContentPlaceHolder1_schMeeting1_dvehour')", true);
                        //    rcmbHourly.SelectedValue = str1[1].ToString();
                        //    break;
                        case "D":
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "OpenPage", "javascript:showOnlyRadioDiv('#ContentPlaceHolder1_schMeeting1_dveday')", true);
                            if (str1[1].ToString() == "EW")
                                rbtnEW.Checked = true;
                            else
                            {
                                rbtnEday.Checked = true;
                                rcmbEDay.SelectedValue = str1[1].ToString().Substring(2);
                            }
                            break;
                        case "W":
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "OpenPage", "javascript:showOnlyRadioDiv('#ContentPlaceHolder1_schMeeting1_dveweek')", true);
                            rcmbRecurE.SelectedValue = str1[1].ToString();
                            SetValueCheckBoxList(chkWkDay, str1[2].ToString());
                            break;
                        case "M":
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "OpenPage", "javascript:showOnlyRadioDiv('#ContentPlaceHolder1_schMeeting1_dvemonth')", true);
                            if (str1[1].ToString() == "D")
                            {
                                optMDay.Checked = true;
                                rcmbMDay.SelectedValue = str1[2].ToString();
                                rcmbMDay1.SelectedValue = str1[3].ToString();
                            }
                            else
                            {
                                optMDay1.Checked = true;
                                rcmbMTheDay.SelectedValue = str1[2].ToString();
                                rcmbMTheTyp.SelectedValue = str1[3].ToString();
                                rcmbM1.SelectedValue = str1[4].ToString();
                            }
                            break;
                        case "Y":
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "OpenPage", "javascript:showOnlyRadioDiv('#ContentPlaceHolder1_schMeeting1_dveyear')", true);
                            if (str1[1].ToString() == "E")
                            {
                                optY.Checked = true;
                                rcmbY.SelectedValue = str1[2].ToString();
                                rcmbYMonthday.SelectedValue = str1[3].ToString();
                            }
                            else
                            {
                                optY1.Checked = true;
                                rcmbYTheDay.SelectedValue = str1[2].ToString();
                                rcmbYTheTyp.SelectedValue = str1[3].ToString();
                                rcmbMTheMonth.SelectedValue = str1[4].ToString();
                            }
                            break;
                    }
                }
            }
        }

        //private void popWebinarDomain()
        //{
        //    List<WebinarHostBE> objWebHost = new List<WebinarHostBE>();
        //    objWebHost = objWebinarDA.GetWebinarHostDA(Convert.ToInt32(hWebinarID.Value));
        //    chkHotmail.Checked = chkYahoo.Checked = chkGmail.Checked = chkAol.Checked = chksbc.Checked = false;
        //    int c=1, r=1;
        //    for (int idx = 0; idx < objWebHost.Count; idx++)
        //    {
        //        switch (objWebHost[idx].WebinarHost.ToUpper())
        //        {
        //            case "HOTMAIL.COM":
        //                chkHotmail.Checked = true; 
        //                break;
        //            case "YAHOO.COM":
        //                chkYahoo.Checked = true; 
        //                break;
        //            case "GMAIL.COM":
        //                chkGmail.Checked = true;
        //                break;
        //            case "AOL.COM":
        //                chkAol.Checked = true; 
        //                break;
        //            case ".SBCGLOBAL":
        //                chksbc.Checked = true; 
        //                break;
        //            default:
        //                TextBox txt = (TextBox)this.FindControl("txtr" + r.ToString() + "c" + c.ToString());
        //                if ( txt != null)
        //                    txt.Text = objWebHost[idx].WebinarHost;
        //                CheckBox chk = (CheckBox)this.FindControl("chkr" + r.ToString() + "c" + c.ToString());
        //                if ( chk != null)
        //                    chk.Checked = true;
        //                ++r;
        //                ++c;
        //                if (c > 5)
        //                    c = 1;
        //                break;
        //        }
        //    }
        //}

        protected void lnkSchWebinar_Click(object sender, EventArgs e)
        {
            mvSchedule.SetActiveView(vwWeb);
            hActiveTab.Value = "1";
            setTab();

            lblValidationMsg.Text = "";
            dvValidationMsg.Visible = false;
        }

        protected void lnkTheme_Click(object sender, EventArgs e)
        {
            if (hWebinarID.Value != "0")
            {
                mvSchedule.SetActiveView(vwTheme);
                hActiveTab.Value = "2";
                setTab();
            }
            else
            {
                lblValidationMsg.Text = objError.getMessage("WB0005");
                dvValidationMsg.Visible = true;
            }
        }

        protected void lnkAudView_Click(object sender, EventArgs e)
        {
            if (hWebinarID.Value != "0")
            {
                //mvSchedule.SetActiveView(vwAudience);
                hActiveTab.Value = "3";
                setTab();
            }
            else
            {
                lblValidationMsg.Text = objError.getMessage("WB0001");
                dvValidationMsg.Visible = true;
            }
        }

        protected void lnkSetupReg_Click(object sender, EventArgs e)
        {
            if (hWebinarID.Value != "0")
            {
                //mvSchedule.SetActiveView(vwRegistration);
                hActiveTab.Value = "2";
                setTab();
            }
            else
            {
                lblValidationMsg.Text = objError.getMessage("WB0002");
                dvValidationMsg.Visible = true;
            }
        }

        protected void lnkEmailNotify_Click(object sender, EventArgs e)
        {
            if (hWebinarID.Value != "0")
            {
                //mvSchedule.SetActiveView(vwEmail);
                hActiveTab.Value = "4";
                setTab();
            }
            else
            {
                lblValidationMsg.Text = objError.getMessage("WB0003");
                dvValidationMsg.Visible = true;
            }
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            tabCnt = Convert.ToInt32(hActiveTab.Value);
            int webID = Convert.ToInt32(hWebinarID.Value);
            string rtnVal = string.Empty;
            lblError.Text = "";
            dvValidationMsg.Visible = false;
            switch (tabCnt)
            {
                case 1:
                    #region tab 1
                    if (hIsPast.Value == "0")
                    {
                        if (txtWebinarTitle.Text.Trim() != "" && rdtStartDate.SelectedDate.ToString() != "" && rdtStartTime.SelectedDate.ToString() != "" && rdEndTime.SelectedDate.ToString() != "")
                        {
                            int webinarID = 0;
                            string recurrCond = string.Empty;

                            #region Set webinar info for save
                            objWebinarBE.Createdby = Convert.ToInt32(Session["UserID"]);
                            //objWebinarBE.Description = txtDescription.Text;
                            objWebinarBE.Description = redtSummary.Content;
                            objWebinarBE.isRecurrence = chkRecurrence.Checked;
                            
                            //objWebinarBE.StartDate = objUtil.getGMTDateTime(Convert.ToDateTime(rdtStartDate.SelectedDate + " " + rdtStartTime.SelectedDate));

                            //objWebinarBE.StartTime = Convert.ToDateTime(objUtil.getGMTDateTime(Convert.ToDateTime(rdtStartDate.SelectedDate + " " + rdtStartTime.SelectedDate)).ToString("HH:mm:ss")); //ToLongTimeString();
                            //objWebinarBE.EndTime = Convert.ToDateTime(rdEndTime.SelectedDate).ToString("HH:mm:ss"); //.ToLongTimeString();

                            objWebinarBE.StartDate = Convert.ToDateTime(rdtStartDate.SelectedDate);
                            objWebinarBE.StartTime = Convert.ToDateTime(rdtStartTime.SelectedDate).ToString("HH:mm:ss"); //ToLongTimeString();
                            objWebinarBE.EndTime = Convert.ToDateTime(rdEndTime.SelectedDate).ToString("HH:mm:ss"); //.ToLongTimeString();

                            objWebinarBE.TimeZoneID = Convert.ToInt32(ddlTimeZone.SelectedValue);
                            objWebinarBE.Title = txtWebinarTitle.Text;
                            objWebinarBE.WebinarID = Convert.ToInt32(hWebinarID.Value);
                            if (hWebinarID.Value == "0")
                                objWebinarBE.WebinarStatus = "Active";
                            //Presenters Options 
                            if (radioBtnId11.Checked)
                                objWebinarBE.DeliveryChannel = "1";
                            else if (radioBtnId22.Checked)
                                objWebinarBE.DeliveryChannel = "2";
                            else if (radioBtnId33.Checked)
                                objWebinarBE.DeliveryChannel = "3";
                            else
                                objWebinarBE.DeliveryChannel = "0";
                            objWebinarBE.isPublic = chkEmailRegAPI.Checked;

                            #endregion

                            #region recurrence
                            WebinarRecurrencyBE objWebinarRecurrBE = new WebinarRecurrencyBE();

                            if (chkRecurrence.Checked)
                            {
                                //if (rbtnNoEnddate.Checked)
                                //{
                                //    objWebinarRecurrBE.endType = "Noend";
                                //    objWebinarRecurrBE.endValue = "";
                                //}
                                //else 
                                if (rbtnEndAfter.Checked)
                                {
                                    objWebinarRecurrBE.endType = "After";
                                    objWebinarRecurrBE.endValue = txtEndAfter.Text;
                                    if (!objUtil.IsInteger(txtEndAfter.Text.Trim()))
                                        rtnVal = "Number of recurrence should be numeric";
                                }
                                else
                                {
                                    objWebinarRecurrBE.endType = "Endby";
                                    objWebinarRecurrBE.endValue = Convert.ToDateTime(rdtEndBy.SelectedDate).ToShortDateString();
                                    if (!objUtil.isDateGreaterToday(Convert.ToDateTime(rdtEndBy.SelectedDate)))
                                        rtnVal = "End by date cannot be in past";
                                }
                                if (rtnVal == string.Empty)
                                {
                                    switch (rbtnDurationType.SelectedValue)
                                    {
                                        //case "H":
                                        //    recurrCond = "H:" + rcmbHourly.SelectedValue;
                                        //    break;
                                        case "D":
                                            recurrCond = "D:" + (rbtnEday.Checked ? "ED" + rcmbEDay.SelectedValue : "EW");
                                            break;
                                        case "W":
                                            string selWDay = "";
                                            foreach (ListItem li in chkWkDay.Items)
                                            {
                                                if (li.Selected)
                                                    selWDay = selWDay + li.Value + ",";
                                            }
                                            if (selWDay.Length > 0)
                                                recurrCond = "W:" + rcmbRecurE.SelectedValue + ":" + selWDay.Substring(0, selWDay.Length - 1);
                                            else
                                                recurrCond = "W:" + rcmbRecurE.SelectedValue + ":";
                                            break;
                                        case "M":
                                            if (optMDay.Checked)
                                                recurrCond = "M:D:" + rcmbMDay.SelectedValue + ":" + rcmbMDay1.SelectedValue;
                                            else
                                                recurrCond = "M:T:" + rcmbMTheDay.SelectedValue + ":" + rcmbMTheTyp.SelectedValue + ":" + rcmbM1.SelectedValue;
                                            break;
                                        case "Y":
                                            if (optY.Checked)
                                                recurrCond = "Y:E:" + rcmbY.SelectedValue + ":" + rcmbYMonthday.SelectedValue;
                                            else
                                                recurrCond = "Y:T:" + rcmbYTheDay.SelectedValue + ":" + rcmbYTheTyp.SelectedValue + ":" + rcmbMTheMonth.SelectedValue;
                                            break;
                                    }
                                    objWebinarRecurrBE.WebinarID = webinarID;
                                    objWebinarRecurrBE.recurrType = rbtnDurationType.SelectedValue;
                                    objWebinarRecurrBE.recurrCriteria = recurrCond;
                                }
                                //objWebinarDA.UpdateWebinarRecurrence(objWebinarRecurrBE);
                            }
                            #endregion

                            if ((Convert.ToDateTime(rdtStartDate.SelectedDate) - DateTime.Now).Days < 0)
                            {
                                rtnVal = "Cannot schedule webinar in the past";
                            }
                            else if ((Convert.ToDateTime(rdtStartDate.SelectedDate) - DateTime.Now).Days == 0)
                            {
                                UserDA objUserDA = new UserDA();
                                float tm = objUserDA.getTimezoneToServerTimeDiff(Convert.ToInt32(ddlTimeZone.SelectedValue));
                                DateTime currentClientPreferDateTime = DateTime.Now.AddHours(-tm);

                                if ((Convert.ToDateTime(rdtStartTime.SelectedDate) - Convert.ToDateTime(currentClientPreferDateTime)).Minutes < 0)
                                    rtnVal = "Cannot schedule webinar past the current time";
                            }

                            if (rtnVal == "")
                            {
                                if (objWebinarDA.IsWebinarOverlapping(objUtil.FormDBDate(Convert.ToDateTime(rdtStartDate.SelectedDate)), rdtStartTime.SelectedDate.ToString(), rdEndTime.SelectedDate.ToString(), Convert.ToInt32(Session["UserID"]), Convert.ToInt32(hWebinarID.Value)))
                                {
                                    rtnVal = objError.getMessage("WB0008");
                                }
                                else
                                {
                                    webinarID = objWebinarDA.SaveWebinarSchedule(objWebinarBE, objWebinarRecurrBE, Session["EmailID"].ToString());
                                    saveRecurrenceInstances(webinarID);
                                    if (hWebinarID.Value == "0") // this section occurs when new webinars get created
                                    {
                                        hWebinarID.Value = webinarID.ToString();
                                        //setWebinarIDToTabs(hWebinarID.Value, true);
                                        lblWebinarTitle.Text = "<b>Webinar Title</b> - " + txtWebinarTitle.Text;
                                        fvWebTitle.Visible = true;
                                    }
                                }
                            }
                        }
                    }
                    #endregion
                    break;
                case 2:
                    if (hIsPast.Value == "0")
                    {
                        rtnVal = webRegistration1.saveRegistrationInfo();
                    }
                    break;
                case 3:
                    if (hIsPast.Value == "0")
                    {
                        webAudience1.saveAudienceComponent();
                    }
                    break;
                case 4:
                    if (hIsPast.Value == "0")
                    {
                        webEmail1.SaveWebEmail();
                    }
                    break;
            }
            if (rtnVal == string.Empty)
            {
                if (hWebinarID.Value != "0")
                {
                    if (tabCnt < 4)
                        tabCnt = tabCnt + 1;
                    hActiveTab.Value = tabCnt.ToString();
                    setTab();
                }
            }
            else
                lblError.Text = "<br>" + rtnVal;
        }

        private void setNotificationDefaults(int webinarID)
        {
            #region email contents
            WebinarNotification objWebNotify = new WebinarNotification();
            objWebNotify.WebinarID = webinarID;
            objWebNotify.isConfirmEmailAllReg = true;
            objWebNotify.RegConfirmEmailContentID = Convert.ToInt32(Constant.RegConfirmContentID);
            objWebNotify.ReminderEmailContentID = Convert.ToInt32(Constant.ReminderContentID);
            objWebNotify.FollowupAEmailContentID = Convert.ToInt32(Constant.AttendeeFollowUpContentID);
            objWebNotify.FollowupNAEmailContentID = Convert.ToInt32(Constant.NonAttendeeFollowUpContentID);
            objWebNotify.InvitationContentID = Convert.ToInt32(Constant.InvitationContentID);
            objWebinarDA.SaveWebinarNotificationDefault(objWebNotify);
            #endregion

            #region Registrant Updates
            EmailDA objEmailDA = new EmailDA();
            RegistrantUpdateBO objRegistrantUpdateBO = new RegistrantUpdateBO();
            objRegistrantUpdateBO.WebinarID = webinarID;
            objRegistrantUpdateBO.IsRegularUpdate = true;
            objRegistrantUpdateBO.IsUpdateWhenRegister = true;
            objRegistrantUpdateBO.UpdateTime = "08:00:00";
            objRegistrantUpdateBO.updateToEmails = Session["EmailID"].ToString();
            objRegistrantUpdateBO.UpdateWeekday = 2;
            objEmailDA.SaveRegistrantUpdate(objRegistrantUpdateBO);
            #endregion

            // Reminder email setting
            RegistrantEmailSettingBO objEmailSetting = new RegistrantEmailSettingBO();

            #region Hour reminder
            objEmailSetting.intervalType = "H";
            objEmailSetting.intervalValue = 2;
            objEmailSetting.EmailScheduleStatus = "Draft";
            objEmailSetting.SettingType = "Reminder Email";
            objEmailSetting.setID = 0;
            objEmailSetting.webinarID = webinarID;
            objEmailDA.SaveRegistrantEmailSetting(objEmailSetting);
            #endregion

            #region Day reminder
            objEmailSetting.intervalType = "D";
            objEmailSetting.intervalValue = 2;
            objEmailSetting.EmailScheduleStatus = "Draft";
            objEmailSetting.SettingType = "Reminder Email";
            objEmailSetting.setID = 0;
            objEmailSetting.webinarID = webinarID;
            objEmailDA.SaveRegistrantEmailSetting(objEmailSetting);
            #endregion

            #region Week reminder
            objEmailSetting.intervalType = "W";
            objEmailSetting.intervalValue = 1;
            objEmailSetting.EmailScheduleStatus = "Draft";
            objEmailSetting.SettingType = "Reminder Email";
            objEmailSetting.setID = 0;
            objEmailSetting.webinarID = webinarID;
            objEmailDA.SaveRegistrantEmailSetting(objEmailSetting);
            #endregion

            #region Follow-up - Attendees
            objEmailSetting.intervalType = "D";
            objEmailSetting.intervalValue = 2;
            objEmailSetting.EmailScheduleStatus = "Draft";
            objEmailSetting.SettingType = "Follow-up Email - Attended";
            objEmailSetting.setID = 0;
            objEmailSetting.webinarID = webinarID;
            objEmailDA.SaveRegistrantEmailSetting(objEmailSetting);
            #endregion

            #region Follow-up - Non-Attendees
            objEmailSetting.intervalType = "D";
            objEmailSetting.intervalValue = 2;
            objEmailSetting.EmailScheduleStatus = "Draft";
            objEmailSetting.SettingType = "Follow-up Email - Unattended";
            objEmailSetting.setID = 0;
            objEmailSetting.webinarID = webinarID;
            objEmailDA.SaveRegistrantEmailSetting(objEmailSetting);
            #endregion

        }

        protected void btnPrev_Click(object sender, EventArgs e)
        {
            tabCnt = Convert.ToInt32(hActiveTab.Value);
            if (tabCnt <= 4)
                tabCnt = tabCnt - 1;
            hActiveTab.Value = tabCnt.ToString();
            setTab();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (hIsPast.Value == "0")
            {
                webEmail1.SaveWebEmail();
                webRegistration1.CampaignTrackingEmailing();
                webRegistration1.ConnectRegistrationEmailing();
            }
            Response.Redirect("~/Pages/Webinar");
        }

        private void saveRecurrenceInstances(int webinarID)
        {
            string recurrCond = string.Empty;

            int NoTimes = Constant.MaxRecurrenceCount;
            DateTime schDate = Convert.ToDateTime(rdtStartDate.SelectedDate);
            DateTime enDate = Convert.ToDateTime(rdtEndBy.SelectedDate);
            string insDates = string.Empty;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            bool isExit = false;
            int iDW;
            // 0 - Sunday, 6 - Saturday
            iDW = Convert.ToInt32(schDate.DayOfWeek);
            DateTime dt1 = new DateTime();
            switch (rbtnDurationType.SelectedValue)
            {
                #region Day
                case "D":
                    if (rbtnEday.Checked)
                    {
                        for (int idx = 0; idx < NoTimes; idx++)
                        {
                            sb.Append(schDate.AddDays(Convert.ToInt32(rcmbEDay.SelectedValue)).ToShortDateString() + ",");
                            schDate = schDate.AddDays(Convert.ToInt32(rcmbEDay.SelectedValue));
                        }
                    }
                    else
                    {
                        for (int idx = 0; idx < NoTimes; idx++)
                        {
                            if (Convert.ToInt32(schDate.DayOfWeek) == 5) schDate = schDate.AddDays(2);
                            if (Convert.ToInt32(schDate.DayOfWeek) == 6) schDate = schDate.AddDays(1);
                            sb.Append(schDate.AddDays(1).ToShortDateString() + ",");
                            schDate = schDate.AddDays(1);
                        }
                    }
                    break;
                #endregion
                #region week
                case "W":
                    string selWDay = "";
                    int wkDaysCount = (Convert.ToInt32(rcmbRecurE.SelectedValue) - 1) * 7;

                    foreach (ListItem li in chkWkDay.Items)
                    {
                        if (li.Selected) selWDay = selWDay + li.Value + ",";
                    }
                    if (selWDay.Length > 0)
                    {
                        selWDay = selWDay.Substring(0, selWDay.Length - 1);
                        ArrayList arr1 = objUtil.StringToArrayList(selWDay, new char[] { ',' });
                        int stval = 0;
                        int iLoc = 0;
                        int instCount = 0;

                        for (int idx = 0; idx < arr1.Count; idx++)
                        {
                            if (Convert.ToInt32(arr1[idx]) > iDW)
                            {
                                stval = Convert.ToInt32(arr1[idx]);
                                iLoc = idx;
                                break;
                            }
                        }
                        DateTime schDate1 = schDate;
                        if (iLoc != 0)
                        {
                            for (int idx = iLoc; idx < arr1.Count; idx++)
                            {
                                sb.Append(schDate.AddDays(Convert.ToInt32(arr1[idx]) - iDW).ToShortDateString() + ",");
                                schDate1 = schDate.AddDays(Convert.ToInt32(arr1[idx]) - iDW);
                                ++instCount;
                            }
                            schDate = schDate1;
                        }
                        isExit = false;
                        if (instCount < NoTimes)
                        {
                            for (int idx = instCount; idx < NoTimes; idx++)
                            {
                                for (int idx1 = 0; idx1 < arr1.Count; idx1++)
                                {
                                    iDW = (6 - Convert.ToInt32(schDate.DayOfWeek)) + 1;
                                    sb.Append(schDate.AddDays(wkDaysCount + iDW + Convert.ToInt32(arr1[idx1])).ToShortDateString() + ",");
                                    schDate1 = schDate.AddDays(wkDaysCount + iDW + Convert.ToInt32(arr1[idx1]));
                                    ++instCount;
                                    if (instCount >= NoTimes)
                                    {
                                        isExit = true;
                                        break;
                                    }
                                }
                                schDate = schDate1;
                                if (isExit) break;
                            }
                        }
                    }
                    else
                    {
                        wkDaysCount = Convert.ToInt32(rcmbRecurE.SelectedValue) * 7;
                        for (int idx = 0; idx < NoTimes; idx++)
                        {
                            sb.Append(schDate.AddDays(wkDaysCount).ToShortDateString() + ",");
                            schDate = schDate.AddDays(wkDaysCount);
                        }
                    }
                    break;
                #endregion
                #region month
                case "M":
                    if (optMDay.Checked)  // Day of every x month option
                    {
                        for (int idx = 0; idx < NoTimes; idx++)
                        {
                            dt1 = schDate.AddMonths(Convert.ToInt32(rcmbMDay1.SelectedValue));
                            schDate = new DateTime(dt1.Year, dt1.Month, Convert.ToInt32(rcmbMDay.SelectedValue));
                            sb.Append(schDate.ToShortDateString() + ",");
                        }
                    }
                    else  // The first/second.../last day/sun/mon....sat of every x month
                    {
                        for (int idx = 0; idx < NoTimes; idx++)
                        {
                            dt1 = schDate.AddMonths(Convert.ToInt32(rcmbM1.SelectedValue));
                            if (rcmbMTheTyp.SelectedValue == "D")
                            {
                                if (rcmbMTheDay.SelectedValue == "l")
                                    dt1 = objUtil.getLastDayDateOfMonth(dt1.Year, dt1.Month);
                                else
                                    dt1 = new DateTime(dt1.Year, dt1.Month, Convert.ToInt32(rcmbMTheDay.SelectedValue));
                            }
                            else
                            {
                                if (rcmbMTheDay.SelectedValue == "l")
                                    dt1 = objUtil.getlastWeekDayDate(dt1.Year, dt1.Month, objUtil.getDayOftheWeek(Convert.ToInt32(rcmbMTheTyp.SelectedValue)));
                                else
                                    dt1 = objUtil.getWeekDayDate(dt1.Year, dt1.Month, objUtil.getDayOftheWeek(Convert.ToInt32(rcmbMTheTyp.SelectedValue)), Convert.ToInt32(rcmbMTheDay.SelectedValue));
                            }
                            schDate = dt1;
                            sb.Append(schDate.ToShortDateString() + ",");
                        }
                    }
                    break;
                #endregion
                #region year
                case "Y":
                    if (optY.Checked)  // 1/2/3/4 of Jan/feb..Dec option
                    {
                        for (int idx = 0; idx < NoTimes; idx++)
                        {
                            dt1 = schDate.AddYears(1);
                            schDate = new DateTime(dt1.Year, Convert.ToInt32(rcmbY.SelectedValue), Convert.ToInt32(rcmbYMonthday.SelectedValue));
                            sb.Append(schDate.ToShortDateString() + ",");
                        }
                    }
                    else  // The first/second.../last day/sun/mon....sat of every x month
                    {
                        for (int idx = 0; idx < NoTimes; idx++)
                        {
                            dt1 = schDate.AddYears(1);
                            if (rcmbYTheTyp.SelectedValue == "D")
                            {
                                if (rcmbYTheDay.SelectedValue == "l")
                                    dt1 = objUtil.getLastDayDateOfMonth(dt1.Year, Convert.ToInt32(rcmbMTheMonth.SelectedValue));
                                else
                                    dt1 = new DateTime(dt1.Year, Convert.ToInt32(rcmbMTheMonth.SelectedValue), Convert.ToInt32(rcmbYTheDay.SelectedValue));
                            }
                            else
                            {
                                if (rcmbYTheDay.SelectedValue == "l")
                                    dt1 = objUtil.getlastWeekDayDate(dt1.Year, Convert.ToInt32(rcmbMTheMonth.SelectedValue), objUtil.getDayOftheWeek(Convert.ToInt32(rcmbYTheTyp.SelectedValue)));
                                else
                                    dt1 = objUtil.getWeekDayDate(dt1.Year, Convert.ToInt32(rcmbMTheMonth.SelectedValue), objUtil.getDayOftheWeek(Convert.ToInt32(rcmbMTheMonth.SelectedValue)), Convert.ToInt32(rcmbYTheDay.SelectedValue));
                            }
                            schDate = dt1;
                            sb.Append(schDate.ToShortDateString() + ",");
                        }
                    }
                    break;
                #endregion
            }
            string sb1 = sb.ToString();
            if (sb1.Length > 0)
            {
                sb1 = sb1.Substring(0, sb1.Length - 1);
                ArrayList arr1 = objUtil.StringToArrayList(sb1, new char[] { ',' });
                string reOccurSaveMsg = string.Empty;
                //objUtil.RecordLogToFS(sb1);
                if (rbtnEndAfter.Checked)
                {
                    NoTimes = (Convert.ToInt32(txtEndAfter.Text.Trim()) > Constant.MaxRecurrenceCount) ? Constant.MaxRecurrenceCount : Convert.ToInt32(txtEndAfter.Text.Trim());
                    NoTimes = (arr1.Count > NoTimes) ? NoTimes : arr1.Count;

                    for (int idx = 0; idx < NoTimes; idx++)
                    {
                        if (objWebinarDA.IsWebinarOverlapping(objUtil.FormDBDate(Convert.ToDateTime(arr1[idx])), rdtStartTime.SelectedDate.ToString(), rdEndTime.SelectedDate.ToString(), Convert.ToInt32(Session["UserID"]), 0))
                            reOccurSaveMsg += objError.getMessage("WB0008") + " Instance on " + arr1[idx].ToString() + " between " + rdtStartTime.SelectedDate.ToString() + " and " + rdEndTime.SelectedDate.ToString() + "<br>";
                        else
                            objWebinarDA.SaveWebinarRecurrenceInstance(webinarID, idx + 1, Convert.ToDateTime(arr1[idx]), rdtStartTime.SelectedDate.ToString(), rdEndTime.SelectedDate.ToString(), Convert.ToInt32(Session["UserID"]));
                    }
                }
                else
                {
                    int idx = 0;
                    foreach (string s in arr1)
                    {
                        TimeSpan ts = Convert.ToDateTime(s) - Convert.ToDateTime(rdtEndBy.SelectedDate);

                        if (ts.Days <= 0)
                        {
                            //idx++;
                            if (objWebinarDA.IsWebinarOverlapping(objUtil.FormDBDate(Convert.ToDateTime(arr1[idx])), rdtStartTime.SelectedDate.ToString(), rdEndTime.SelectedDate.ToString(), Convert.ToInt32(Session["UserID"]), 0))
                                reOccurSaveMsg += objError.getMessage("WB0008") + " Instance on " + arr1[idx].ToString() + " between " + rdtStartTime.SelectedDate.ToString() + " and " + rdEndTime.SelectedDate.ToString() + "<br>";
                            else
                                objWebinarDA.SaveWebinarRecurrenceInstance(webinarID, idx, Convert.ToDateTime(arr1[idx]), rdtStartTime.SelectedDate.ToString(), rdEndTime.SelectedDate.ToString(), Convert.ToInt32(Session["UserID"]));
                            idx++;
                        }
                        else
                            break;
                        if (idx >= Constant.MaxRecurrenceCount)
                            break;
                    }
                }

                lblError.Text = reOccurSaveMsg;
            }
        }

        private void setTab()
        {
            lblValidationMsg.Text = "";
            dvValidationMsg.Visible = false;

            li1.Attributes["class"] = "One ";
            //li2.Attributes["class"] = "Two";
            li2.Attributes["class"] = "Two";
            li3.Attributes["class"] = "Three";
            li4.Attributes["class"] = "Four";

            switch (Convert.ToInt32(hActiveTab.Value))
            {
                case 1:
                    li1.Attributes["class"] = "One1 Current";
                    mvSchedule.SetActiveView(vwWeb);
                    btnSave.Visible = false;
                    btnPrev.Visible = false;
                    btnNext.Visible = true;
                    break;
                //case 2:
                //    li2.Attributes["class"] = "Two1 Current";
                //    mvSchedule.SetActiveView(vwTheme);
                //    btnSave.Visible = false;
                //    btnPrev.Visible = true;
                //    btnNext.Visible = true;
                //    break;
                case 2:
                    li2.Attributes["class"] = "Two1 Current";
                    mvSchedule.SetActiveView(vwRegistration);
                    btnSave.Visible = false;
                    btnPrev.Visible = true;
                    btnNext.Visible = true;
                    break;
                case 3:
                    li3.Attributes["class"] = "Three1 Current";
                    mvSchedule.SetActiveView(vwAudience);
                    btnSave.Visible = false;
                    btnPrev.Visible = true;
                    btnNext.Visible = true;
                    break;
                case 4:
                    li4.Attributes["class"] = "Four1 Current";
                    mvSchedule.SetActiveView(vwEmail);
                    btnSave.Visible = true;
                    btnPrev.Visible = true;
                    btnNext.Visible = false;
                    break;
            }
        }

        protected void rcmbActions_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            Session["WebinarID"] = rcmbActions.SelectedValue + "," + hWebinarID.Value;
            Response.Redirect("~/Pages/webinarAction");
        }

        public void SetValueCheckBoxList(CheckBoxList cbl, string sValues)
        {
            if (!string.IsNullOrEmpty(sValues))
            {
                ArrayList values = objUtil.StringToArrayList(sValues, new char[] { ',' });
                foreach (ListItem li in cbl.Items)
                {
                    if (values.Contains(li.Value))
                        li.Selected = true;
                    else
                        li.Selected = false;
                }
            }
        }

        // Email codes 

        //private void setCurrTheme()
        //{
        //   List<ThemeMasterBE> objTM = new List<ThemeMasterBE>();
        //   objTM = objWebinarDA.GetWebinarThemeDetails(Convert.ToInt32(hWebinarID.Value));
        //   if (objTM.Count > 0)
        //   {
        //       //for(int idx = 0; idx < objTM.Count;idx++)
        //       //{
        //       //     switch (objTM[idx].ThemeCategory.ToUpper())
        //       //     {
        //       //         case "AUDIENCE":
        //       //             imgAIDefault.Src = Request.ApplicationPath + "images/Theme/" + objTM[idx].ThumbNail;
        //       //             break;
        //       //         case "REGISTRATION" :
        //       //             imgCurrRegTheme.Src = Request.ApplicationPath + "images/Theme/" + objTM[idx].ThumbNail;
        //       //             break;
        //       //         case "NOTIFICATION":
        //       //             imgCurrInviteTheme.Src = Request.ApplicationPath + "images/Theme/" + objTM[idx].ThumbNail;
        //       //             break;
        //       //     }
        //       //}
        //   }
        //}

        //private void popAIList()
        //{
        //    MasterDA objMasterDA = new MasterDA();

        //    //rlstAI.DataSource = objMasterDA.GetThemeListDA("Audience");
        //    //rlstAI.DataBind(); 
        //}

        //private void popRegList()
        //{

        //    MasterDA objMasterDA = new MasterDA();

        //    //rlstReg.DataSource = objMasterDA.GetThemeListDA("Registration");
        //    //rlstReg.DataBind(); 
        //}

        //private void popEmailList()
        //{

        //    MasterDA objMasterDA = new MasterDA();

        //    //rlstInv.DataSource = objMasterDA.GetThemeListDA("Notification");
        //    //rlstInv.DataBind(); 
        //}

        //private void setActiveStep(int tabOrder)
        //{
        //    li1.Attributes.Remove("class");
        //    li2.Attributes.Remove("class");
        //    li3.Attributes.Remove("class");
        //    li4.Attributes.Remove("class");

        //    switch (tabOrder)
        //    {
        //        case 1:
        //            li1.Attributes.Add("class", "One1 Current");
        //            li2.Attributes.Add("class", "Two");
        //            li3.Attributes.Add("class", "Three");
        //            li4.Attributes.Add("class", "Four");   
        //            break;
        //        case 2:
        //            li1.Attributes.Add("class", "One");
        //            li2.Attributes.Add("class", "Two1 Current");
        //            li3.Attributes.Add("class", "Three");
        //            li4.Attributes.Add("class", "Four");   
        //            break;
        //        case 3:
        //            li1.Attributes.Add("class", "One");
        //            li2.Attributes.Add("class", "Two");
        //            li3.Attributes.Add("class", "Three1 Current");
        //            li4.Attributes.Add("class", "Four");   
        //            break;
        //        case 4:
        //            li1.Attributes.Add("class", "One");
        //            li2.Attributes.Add("class", "Two");
        //            li3.Attributes.Add("class", "Three");
        //            li4.Attributes.Add("class", "Four1 Current");   
        //            break;
        //    }
        //}

        //protected string GetClickAudience(object dataItem)
        //{
        //    Int16 EBThemeID = (Int16) DataBinder.Eval(dataItem, "EBThemeID");
        //    // ViewDList(RadWindow)
        //    string reportWindowName = ("ViewAudience.aspx?id="
        //                + (EBThemeID + "&src=myc"));
        //    string clickEventHandlerString = ("javascript:var w = window.radopen(\'"
        //                + (Convert.ToString(("ViewAudience.aspx?id="
        //                    + (EBThemeID + "&src=myc"))) + ("\', \'"
        //                + (reportWindowName + "\'); w.set_modal(true); w.moveTo(250, 150); w.set_behaviors(Telerik.Web.UI.WindowBehaviors.Move +Teler" +
        //                "ik.Web.UI.WindowBehaviors.Close); w.setSize(550, 620); return false;"))));
        //    return clickEventHandlerString;
        //}

        //// Get View DList Click

        //protected string GetClick(object dataItem)
        //{
        //    Int16 EBThemeID = (Int16) DataBinder.Eval(dataItem, "EBThemeID");
        //    // ViewDList(RadWindow)
        //    string reportWindowName = ("ViewRegistration.aspx?id="
        //                + (EBThemeID + "&src=myc"));
        //    string clickEventHandlerString = ("javascript:var w = window.radopen(\'"
        //                + (Convert.ToString(("ViewRegistration.aspx?id="
        //                    + (EBThemeID + "&src=myc"))) + ("\', \'"
        //                + (reportWindowName + "\'); w.set_modal(true); w.moveTo(250, 150); w.set_behaviors(Telerik.Web.UI.WindowBehaviors.Move +Teler" +
        //                "ik.Web.UI.WindowBehaviors.Close); w.setSize(550, 620); return false;"))));
        //    return clickEventHandlerString;
        //}

        //protected void rlstReg_ItemDataBound(object sender, RadListViewItemEventArgs e)
        //{

        //}

        //protected void rlstInv_ItemDataBound(object sender, RadListViewItemEventArgs e)
        //{

        //}

        //protected void rlstAI_ItemDataBound(object sender, RadListViewItemEventArgs e)
        //{
        //    string dtValue = "";
        //    //If e.Item.ItemType = ListItemType.Item Or _
        //    // e.Item.ItemType = ListItemType.AlternatingItem Then

        //    if (e.Item.ItemType ==  RadListViewItemType.DataItem || e.Item.ItemType == RadListViewItemType.AlternatingItem )
        //    {
        //        HyperLink lnkTN = (HyperLink)e.Item.FindControl("hlnkAIThumbnail");
        //        lnkTN.ImageUrl = Request.ApplicationPath + "images/Theme/" + lnkTN.ImageUrl;
        //    }
        //}

        //protected void rlstAI_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        //{

        //}

    }
}
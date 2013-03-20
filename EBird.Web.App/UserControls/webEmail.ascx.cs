using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EBird.DataAccess;
using EBird.BusinessEntity;
using EBird.Common;
using Telerik.Web.UI;
using System.Globalization;

namespace EBird.Web.App.UserControls
{
    public partial class webEmail : System.Web.UI.UserControl
    {
        EmailDA objEmailDA = new EmailDA();
        bool _isWebinarPast = false;

        public string WebinarID
        {
            get
            {
                return hWebinarID.Value;
            }
            set
            {
                hWebinarID.Value = value;
            }
        }

        public bool isWebinarPast
        {
            get
            {
                return _isWebinarPast;
            }
            set
            {
                _isWebinarPast = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                GetWebinarEmailSettings();
                disableUpdates();
            }
        }

        public void GetWebinarEmailSettings()
        {
            // Reminder Emails
            if (hWebinarID.Value != "")
            {
                CheckBox chk;
                RadComboBox rdd1, rdd2;
                HiddenField hRem;
                List<RegistrantEmailSettingBO> objRegSetting = new List<RegistrantEmailSettingBO>();
                objRegSetting = objEmailDA.GetRegistrantEmailSetting(Convert.ToInt32(hWebinarID.Value), "Reminder Email");
                if (objRegSetting.Count > 0)
                {
                    for (int idx = 1; idx <= objRegSetting.Count; idx++)
                    {
                        chk = (CheckBox)this.FindControl("chkRem" + idx.ToString());
                        if (chk != null)
                        {
                            chk.Checked = true;
                            hRem = (HiddenField)this.FindControl("hRem" + idx.ToString());
                            hRem.Value = objRegSetting[idx - 1].setID.ToString();

                            rdd1 = (RadComboBox)this.FindControl("ddRem" + idx.ToString() + "Value");
                            rdd1.SelectedValue = objRegSetting[idx - 1].intervalValue.ToString();

                            rdd2 = (RadComboBox)this.FindControl("ddRem" + idx.ToString() + "Type");
                            rdd2.SelectedValue = objRegSetting[idx - 1].intervalType.ToString();
                        }
                    }
                }

                //Follow-up Email - Attended
                objRegSetting = objEmailDA.GetRegistrantEmailSetting(Convert.ToInt32(hWebinarID.Value), "Follow-up Email - Attended");
                if (objRegSetting.Count > 0)
                {
                    chkFollowAttendee.Checked = true;
                    hFollowAttendee.Value = objRegSetting[0].setID.ToString();
                    ddFollowAttendValue.SelectedValue = objRegSetting[0].intervalValue.ToString();
                    ddFollowAttendType.SelectedValue = objRegSetting[0].intervalType.ToString();
                }

                //Follow-up Email - Non-Attended
                objRegSetting = objEmailDA.GetRegistrantEmailSetting(Convert.ToInt32(hWebinarID.Value), "Follow-up Email - Non-Attended");
                if (objRegSetting.Count > 0)
                {
                    chkFollowNonAttendee.Checked = true;
                    hFollowNonAttendee.Value = objRegSetting[0].setID.ToString();
                    ddFollowNonAttendValue.SelectedValue = objRegSetting[0].intervalValue.ToString();
                    ddFollowNonAttendType.SelectedValue = objRegSetting[0].intervalType.ToString();
                }

                // Registrant Updates
                List<RegistrantUpdateBO> objRegUpdate = objEmailDA.GetRegistrantUpdate(Convert.ToInt32(hWebinarID.Value));
                if (objRegUpdate.Count > 0)
                {
                    chkEmailRegularUpdate.Checked = objRegUpdate[0].IsRegularUpdate;
                    chkEmailWhenRegistered.Checked = objRegUpdate[0].IsUpdateWhenRegister;
                    rtEmailRegularTime.SelectedDate = Convert.ToDateTime(objRegUpdate[0].UpdateTime);
                    ddEmailRegularDay.SelectedValue = objRegUpdate[0].UpdateWeekday.ToString();
                    txtEmailRegularToRedirect.Text = objRegUpdate[0].updateToEmails;
                }

                WebinarDA objWebDA = new WebinarDA();
                List<WebinarNotification> objWebNotify = objWebDA.getWebinarNotification(Convert.ToInt32(hWebinarID.Value));
                if (objWebNotify.Count > 0)
                {

                    chkSendAll.Checked = objWebNotify[0].isConfirmEmailAllReg;
                    hRegConfirmEmailContentID.Value = objWebNotify[0].RegConfirmEmailContentID.ToString();
                    hReminderEmailContentID.Value = objWebNotify[0].ReminderEmailContentID.ToString();
                    hFollowupAEmailContentID.Value = objWebNotify[0].FollowupAEmailContentID.ToString();
                    hFollowupNAEmailContentID.Value = objWebNotify[0].FollowupNAEmailContentID.ToString();
                    //Need to take this part to registration page
                    //hInviteContentID.Value = objWebNotify[0].InvitationContentID.ToString();
                }

                //Feature ID 32 - Webinar Invite
                if (Session["PREMIUM_FEATURE"].ToString().IndexOf(",32,") < 0)
                {
                    spPre1.Visible = true;
                    chkEmailRegularUpdate.Enabled = false;
                    chkEmailWhenRegistered.Enabled = false;
                }
            }
        }

        public string SaveWebEmail()
        {
            RegistrantEmailSettingBO objEmailSetting = new RegistrantEmailSettingBO();
            // Reminder Email
            if (chkRem1.Checked)
            {
                objEmailSetting.intervalType = "M"; //ddRem1Type.SelectedValue;
                objEmailSetting.intervalValue = Convert.ToInt16(ddRem1Value.SelectedValue);
                objEmailSetting.EmailScheduleStatus = "Draft";
                objEmailSetting.SettingType = "Reminder Email";
                objEmailSetting.setID = Convert.ToInt32(hRem1.Value);
                objEmailSetting.webinarID = Convert.ToInt32(hWebinarID.Value);
                objEmailDA.SaveRegistrantEmailSetting(objEmailSetting);
            }
            if (chkRem2.Checked)
            {
                objEmailSetting.intervalType = ddRem2Type.SelectedValue;
                objEmailSetting.intervalValue = Convert.ToInt16(ddRem2Value.SelectedValue);
                objEmailSetting.EmailScheduleStatus = "Draft";
                objEmailSetting.SettingType = "Reminder Email";
                objEmailSetting.setID = Convert.ToInt32(hRem2.Value);
                objEmailSetting.webinarID = Convert.ToInt32(hWebinarID.Value);
                objEmailDA.SaveRegistrantEmailSetting(objEmailSetting);
            }
            if (chkRem3.Checked)
            {
                objEmailSetting.intervalType = ddRem3Type.SelectedValue;
                objEmailSetting.intervalValue = Convert.ToInt16(ddRem3Value.SelectedValue);
                objEmailSetting.EmailScheduleStatus = "Draft";
                objEmailSetting.SettingType = "Reminder Email";
                objEmailSetting.setID = Convert.ToInt32(hRem3.Value);
                objEmailSetting.webinarID = Convert.ToInt32(hWebinarID.Value);
                objEmailDA.SaveRegistrantEmailSetting(objEmailSetting);
            }
            if (chkRem4.Checked)
            {
                objEmailSetting.intervalType = ddRem4Type.SelectedValue;
                objEmailSetting.intervalValue = Convert.ToInt16(ddRem4Value.SelectedValue);
                objEmailSetting.EmailScheduleStatus = "Draft";
                objEmailSetting.SettingType = "Reminder Email";
                objEmailSetting.setID = Convert.ToInt32(hRem4.Value);
                objEmailSetting.webinarID = Convert.ToInt32(hWebinarID.Value);
                objEmailDA.SaveRegistrantEmailSetting(objEmailSetting);
            }
            if (chkRem5.Checked)
            {
                objEmailSetting.intervalType = ddRem5Type.SelectedValue;
                objEmailSetting.intervalValue = Convert.ToInt16(ddRem5Value.SelectedValue);
                objEmailSetting.EmailScheduleStatus = "Draft";
                objEmailSetting.SettingType = "Reminder Email";
                objEmailSetting.setID = Convert.ToInt32(hRem5.Value);
                objEmailSetting.webinarID = Convert.ToInt32(hWebinarID.Value);
                objEmailDA.SaveRegistrantEmailSetting(objEmailSetting);
            }
            // Follow-up Email - attend
            if (chkFollowAttendee.Checked)
            {
                objEmailSetting.intervalType = ddFollowAttendType.SelectedValue;
                objEmailSetting.intervalValue = Convert.ToInt16(ddFollowAttendValue.SelectedValue);
                objEmailSetting.EmailScheduleStatus = "Draft";
                objEmailSetting.SettingType = "Follow-up Email - Attended";
                objEmailSetting.setID = Convert.ToInt32(hFollowAttendee.Value);
                objEmailSetting.webinarID = Convert.ToInt32(hWebinarID.Value);
                objEmailDA.SaveRegistrantEmailSetting(objEmailSetting);
            }
            // Follow-up Email - unattended
            if (chkFollowNonAttendee.Checked)
            {
                objEmailSetting.intervalType = ddFollowNonAttendType.SelectedValue;
                objEmailSetting.intervalValue = Convert.ToInt16(ddFollowNonAttendValue.SelectedValue);
                objEmailSetting.EmailScheduleStatus = "Draft";
                objEmailSetting.SettingType = "Follow-up Email - Unattended";
                objEmailSetting.setID = Convert.ToInt32(hFollowNonAttendee.Value);
                objEmailSetting.webinarID = Convert.ToInt32(hWebinarID.Value);
                objEmailDA.SaveRegistrantEmailSetting(objEmailSetting);
            }
            // Registrant Updates
            string rtnVal = string.Empty;
            if (chkEmailRegularUpdate.Checked || chkEmailWhenRegistered.Checked)
            {
                if (txtEmailRegularToRedirect.Text.Trim() == "")
                    rtnVal = "Email addressing to notify the registration";
                else
                {
                    EBirdUtility objUtil = new EBirdUtility();
                    rtnVal = objUtil.getInvalidEmails(txtEmailRegularToRedirect.Text.Trim(), new char[] { ';' });
                }
            }
            if (rtnVal != string.Empty)
            {
                RegistrantUpdateBO objRegistrantUpdateBO = new RegistrantUpdateBO();
                objRegistrantUpdateBO.WebinarID = Convert.ToInt32(hWebinarID.Value);
                objRegistrantUpdateBO.IsRegularUpdate = chkEmailRegularUpdate.Checked;
                objRegistrantUpdateBO.IsUpdateWhenRegister = chkEmailWhenRegistered.Checked;
                objRegistrantUpdateBO.UpdateTime = Convert.ToDateTime(rtEmailRegularTime.SelectedDate).ToString("HH:mm", CultureInfo.CurrentCulture);
                objRegistrantUpdateBO.updateToEmails = txtEmailRegularToRedirect.Text;
                objRegistrantUpdateBO.UpdateWeekday = Convert.ToInt32(ddEmailRegularDay.SelectedValue);
                objEmailDA.SaveRegistrantUpdate(objRegistrantUpdateBO);
            }
            return rtnVal;
        }

        public void disableUpdates()
        {
            if (_isWebinarPast)
            {
                chkSendAll.Enabled = false;
                ltrSepA1.Visible = false;
                ltrSepA2.Visible = false;
                lbtnEmailEditConf.Visible = false;
                lblEmailReviewConf.Visible = false;

                ltrSepF1.Visible = false;
                ltrSepF2.Visible = false;
                lbtnRemEdit.Visible = false;
                lbtnRemReviewSend.Visible = false;

                ltrSepR1.Visible = false;
                ltrSepR2.Visible = false;
                lbtnFollowAttendEdit.Visible = false;
                lbtnFollowAttendReview.Visible = false;

                ltrSepU1.Visible = false;
                ltrSepU2.Visible = false;
                lbtnUnFollowAttendEdit.Visible = false;
                lbtnUnFollowAttendReview.Visible = false;
            }
        }
    }
}
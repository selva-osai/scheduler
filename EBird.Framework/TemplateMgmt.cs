using System;
using System.IO;
using EBird.BusinessEntity;
using System.ComponentModel;

namespace EBird.Framework
{
    public class TemplateMgmt
    {
        public string GetRegistrationConfirm(RegistrationConfirmTemplateBO objTemplate, string TemplatePath)
        {
            if (File.Exists(TemplatePath))
            {
                string fileContent = File.ReadAllText(TemplatePath);
                fileContent = fileContent.Replace("@@EVENTDATE", objTemplate.EventDate);
                fileContent = fileContent.Replace("@@EVENTIME", objTemplate.EventTime);
                fileContent = fileContent.Replace("@@REGISTRANTID", objTemplate.RegistrantID.ToString());
                fileContent = fileContent.Replace("@@TIMEZONE", objTemplate.TimeZoneName);
                fileContent = fileContent.Replace("@@WEBINARID", objTemplate.WebinarID.ToString());
                fileContent = fileContent.Replace("@@WEBINARTITLE", objTemplate.WebinarTitle);
                return fileContent;
            }
            else
                return "";
        }

        public string GetRegistrationExist(RegistrationExistTemplateBO objTemplate, string TemplatePath)
        {
            if (File.Exists(TemplatePath))
            {
                string fileContent = File.ReadAllText(TemplatePath);
                fileContent = fileContent.Replace("@@EVENTDATE", objTemplate.EventDate);
                fileContent = fileContent.Replace("@@EVENTIME", objTemplate.EventTime);
                fileContent = fileContent.Replace("@@REGISTRANTID", objTemplate.RegistrantID.ToString());
                fileContent = fileContent.Replace("@@TIMEZONE", objTemplate.TimeZoneName);
                fileContent = fileContent.Replace("@@WEBINARID", objTemplate.WebinarID.ToString());
                fileContent = fileContent.Replace("@@WEBINARTITLE", objTemplate.WebinarTitle);
                fileContent = fileContent.Replace("@@REGDATE", objTemplate.RegisteredOn);
                fileContent = fileContent.Replace("@@REGFROMIP", objTemplate.RegisteredFromIP);
                fileContent = fileContent.Replace("@@REGEMAIL", objTemplate.RegistrantEmail);
                return fileContent;
            }
            else
                return "";
        }

        public string GetReferedCollegueConfirm(string TemplatePath)
        {
            if (File.Exists(TemplatePath))
            {
                string fileContent = File.ReadAllText(TemplatePath);
                return fileContent;
            }
            else
                return "";
        }


        public string GetReferedCollegueExist(string refDate, string refIP, string TemplatePath)
        {
            if (File.Exists(TemplatePath))
            {
                string fileContent = File.ReadAllText(TemplatePath);
                fileContent = fileContent.Replace("@@REFDATE", refDate);
                fileContent = fileContent.Replace("@@REFFROMIP", refIP);
                return fileContent;
            }
            else
                return "";
        }

        public string GetReminderEmail(WebinarReminderEmailTemplateBO objTemplate, string ContentInfo, bool isContentFromTemplate)
        {
            string fileContent = "";
            if (isContentFromTemplate)
            {
                if (File.Exists(ContentInfo))
                    fileContent = File.ReadAllText(ContentInfo);
            }
            else
                fileContent = ContentInfo;

            fileContent = fileContent.Replace("@@EVENTDATE", objTemplate.EventDate);
            fileContent = fileContent.Replace("@@EVENTIME", objTemplate.EventTime);
            fileContent = fileContent.Replace("@@REGISTRANTID", objTemplate.RegistrantID.ToString());
            fileContent = fileContent.Replace("@@TIMEZONE", objTemplate.TimeZoneName);
            fileContent = fileContent.Replace("@@WEBINARID", objTemplate.WebinarID.ToString());
            fileContent = fileContent.Replace("@@WEBINARTITLE", objTemplate.WebinarTitle);
            fileContent = fileContent.Replace("##REMINDERDAYS##", objTemplate.RemainingDays);
            fileContent = fileContent.Replace("##CALENDARREMINDER##", objTemplate.CalenderReminder);
            fileContent = fileContent.Replace("##AUDI_URL##", objTemplate.AudienceURL);
            fileContent = fileContent.Replace("@@REGEMAIL", objTemplate.RegistrantEmail);

            return fileContent;
        }

        public string GetReminderEmail(string TemplatePath)
        {
            if (File.Exists(TemplatePath))
            {
                string fileContent = File.ReadAllText(TemplatePath);
                return fileContent;
            }
            else
                return "";
        }

        public string GetContentForAnyWebinarEmail(WebinarAllEmailTagsBO objAllEmailTags, string ContentInfo, bool isContentFromTemplate, string emailType, string dtFormat = "")
        {
            string fileContent = "";
            if (isContentFromTemplate)
            {
                if (File.Exists(ContentInfo))
                    fileContent = File.ReadAllText(ContentInfo);
            }
            else
                fileContent = ContentInfo;

            switch (emailType)
            {
                case "Attendee Followup":
                    fileContent = fileContent.Replace("##FIRSTNAME##", objAllEmailTags.RegistrantFirstName);
                    fileContent = fileContent.Replace("##EVENTTITLE##", objAllEmailTags.WebinarTitle);
                    fileContent = fileContent.Replace("##USEREMAILADDRESS##", objAllEmailTags.UserEmail);
                    fileContent = fileContent.Replace("##AUDI_URL##", "<a href='" + objAllEmailTags.AudienceURL + "'>" + objAllEmailTags.AudienceURL + "</a>");
                    break;
                case "Confirmation Email":
                    fileContent = fileContent.Replace("##FIRSTNAME##", objAllEmailTags.RegistrantFirstName);
                    fileContent = fileContent.Replace("##EVENTDATE##", Convert.ToDateTime(objAllEmailTags.EventDate).ToString(dtFormat.Replace("MM", "MMM")));
                    fileContent = fileContent.Replace("##EVENTTIME##", Convert.ToDateTime(objAllEmailTags.EventTime).ToString("h:mm tt"));
                    //fileContent = fileContent.Replace("##REGISTRANTID##", objAllEmailTags.RegistrantID.ToString());
                    fileContent = fileContent.Replace("##EVENTTIMEZONESHORT##", objAllEmailTags.TimeZoneName);
                    // fileContent = fileContent.Replace("##WEBINARID##", objAllEmailTags.WebinarID.ToString());
                    fileContent = fileContent.Replace("##EVENTTITLE##", objAllEmailTags.WebinarTitle);
                    fileContent = fileContent.Replace("##AUDI_URL##", "<a href='" + objAllEmailTags.AudienceURL + "'>" + objAllEmailTags.AudienceURL + "</a>");
                    break;
                case "Email a Friend":
                    break;
                case "Non-Attendee Followup":
                    fileContent = fileContent.Replace("##FIRSTNAME##", objAllEmailTags.RegistrantFirstName);
                    fileContent = fileContent.Replace("##EVENTTITLE##", objAllEmailTags.WebinarTitle);
                    fileContent = fileContent.Replace("##AUDI_URL##", "<a href='" + objAllEmailTags.AudienceURL + "'>" + objAllEmailTags.AudienceURL + "</a>");
                    break;
                case "Registrant Reminder Email":
                    TimeSpan diff = Convert.ToDateTime(objAllEmailTags.EventDate) - DateTime.Now;
                    Int32 days = diff.Days;
                    string timeduration = "";
                    if (days > 0)
                        timeduration = days.ToString() + " days";
                    else
                    {
                        if (diff.Hours < 2)
                            timeduration = "an hour";
                        else
                            timeduration = (diff.Hours).ToString() + " hours";
                    }
                    fileContent = fileContent.Replace("##FIRSTNAME##", objAllEmailTags.RegistrantFirstName);
                    fileContent = fileContent.Replace("##TIMETOSTART##", timeduration);
                    //fileContent = fileContent.Replace("##EVENTDATE##", objAllEmailTags.EventDate);
                    //fileContent = fileContent.Replace("##EVENTTIME##", objAllEmailTags.EventTime);
                    //fileContent = fileContent.Replace("##REGISTRANTID##", objAllEmailTags.RegistrantID.ToString());
                    //fileContent = fileContent.Replace("##TIMEZONE##", objAllEmailTags.TimeZoneName);
                    //fileContent = fileContent.Replace("##WEBINARID##", objAllEmailTags.WebinarID.ToString());
                    fileContent = fileContent.Replace("##EVENTTITLE##", objAllEmailTags.WebinarTitle);
                    //fileContent = fileContent.Replace("##REMINDERDAYS##", objAllEmailTags.RemainingDays);
                    //fileContent = fileContent.Replace("##CALENDARREMINDER##", objAllEmailTags.CalenderReminder);
                    fileContent = fileContent.Replace("##AUDI_URL##", "<a href='" + objAllEmailTags.AudienceURL + "'>" + objAllEmailTags.AudienceURL + "</a>");
                    //fileContent = fileContent.Replace("##REGEMAIL##", objAllEmailTags.RegistrantEmail);
                    //fileContent = fileContent.Replace("##EVENTTIMEZONESHORT##", objAllEmailTags.TimeZoneName);

                    break;
                case "Webinar Invitation":
                      fileContent = fileContent.Replace("##EVENTTITLE##", objAllEmailTags.WebinarTitle);
                    fileContent = fileContent.Replace("##EVENTDATE##", Convert.ToDateTime(objAllEmailTags.EventDate).ToString(dtFormat.Replace("MM", "MMM")));
                    fileContent = fileContent.Replace("##EVENTTIME##", Convert.ToDateTime(objAllEmailTags.EventTime).ToString("h:mm tt"));
                    fileContent = fileContent.Replace("##EVENTENDTIME##", Convert.ToDateTime(objAllEmailTags.EndTime).ToString("h:mm tt"));
                    fileContent = fileContent.Replace("##REGI_URL##", "<a href='" + objAllEmailTags.RegistrationURL + "'>" + objAllEmailTags.RegistrationURL + "</a>");
                    fileContent = fileContent.Replace("##WEBINARSUMMARY##", objAllEmailTags.Description);
                    fileContent = fileContent.Replace("##EVENTTIMEZONESHORT##", objAllEmailTags.TimeZoneName);
                    fileContent = fileContent.Replace("##USEREMAILADDRESS##", objAllEmailTags.UserEmail);
                    fileContent = fileContent.Replace("##AUDI_URL##", "<a href='" + objAllEmailTags.AudienceURL + "'>" + objAllEmailTags.AudienceURL + "</a>");
                    break;
                case "Webinar Cancellation":
                    fileContent = fileContent.Replace("##EVENTTITLE##", objAllEmailTags.WebinarTitle);
                    fileContent = fileContent.Replace("##EVENTDATE##", Convert.ToDateTime(objAllEmailTags.EventDate).ToString(dtFormat.Replace("MM", "MMM")));
                    fileContent = fileContent.Replace("##EVENTTIME##", Convert.ToDateTime(objAllEmailTags.EventTime).ToString("h:mm tt"));
                    fileContent = fileContent.Replace("##FIRSTNAME##", objAllEmailTags.RegistrantFirstName);
                    break;
            }

            return fileContent;
        }

        public string GetWeeklyStatusReport(string TemplatePath)
        {
            if (File.Exists(TemplatePath))
            {
                string fileContent = File.ReadAllText(TemplatePath);
                return fileContent;
            }
            else
                return "";
        }

        public string GetGeneralEmailTpl(string TemplatePath)
        {
            if (File.Exists(TemplatePath))
            {
                string fileContent = File.ReadAllText(TemplatePath);
                return fileContent;
            }
            else
                return "";
        }

    }

    public enum EmailTypes
    {
        [DescriptionAttribute("Attendee Followup")] AttendeeFU,
        [DescriptionAttribute("Confirmation Email")] ConfirmEmail,
        [DescriptionAttribute("Email a Friend")] EmailFriend,
        [DescriptionAttribute("Non-Attendee Followup")] NonAttendeeFU,
        [DescriptionAttribute("Registrant Reminder Email")] ReminderEmail,
        [DescriptionAttribute("Webinar Invitation")] WebinarInvitation
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EBird.BusinessEntity
{

    public class EmailBE
    {
        public int RequestID { get; set; }
        public string RequestType { get; set; }
        public string Subject { get; set; }
        public string EmailContent { get; set; }
        public string FromEmail { get; set; }
        public string FromName { get; set; }
        public bool isToEmailRef { get; set; }
        public string ToEmail { get; set; }
        public string ToEmailName { get; set; }
        public List<EmailTo> ToEmailList { get; set; }
        public int SubmittedBy { get; set; }
        public string SubmittedOn { get; set; }
        public string ProcessedOn { get; set; }
        public string RequestStatus { get; set; }
    }

    public class EmailTo
    {
        public int EmailRequestID { get; set; }
        public string ToType { get; set; }
        public string ToEmails { get; set; }
    }

    public class EmailAddressBO
    {
        public string EmailAddress { get; set; }
        public string AddresseName { get; set; }
    }

    public class RegistrantEmailSettingBO
    {
        public int setID { get; set; }
        public int webinarID { get; set; }
        public string intervalType { get; set; }
        public int intervalValue { get; set; }
        public string SettingType { get; set; }
        public string EmailScheduleStatus { get; set; }
    }

    public class RegistrantUpdateBO
    {
        public int WebinarID { get; set; }
        public bool IsRegularUpdate { get; set; }
        public int UpdateWeekday { get; set; }
        public string UpdateTime { get; set; }
        public bool IsUpdateWhenRegister { get; set; }
        public string updateToEmails { get; set; }
    }

    public class WebinarEmailBE
    {
        public int WebinarID { get; set; }
        public string RequestType { get; set; }
        public string Subject { get; set; }
        public string EmailContent { get; set; }
        public int ThemeID { get; set; }
        public bool IsSystemReq { get; set; }
        public bool IsOutlookLink { get; set; }
        public bool IsAdditionalWebinar { get; set; }
    }

    public class EmailingLog
    {
        public int LogID { get; set; }
        public string MailAction { get; set; }
        public string MailFrom { get; set; }
        public string MailTo { get; set; }
        public string MailSubject { get; set; }
        public string MailOn { get; set; }
        public string ActionInfo { get; set; }
    }

}

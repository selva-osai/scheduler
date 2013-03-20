using System;

namespace EBird.BusinessEntity
{
    public class RegistrationConfirmTemplateBO
    {
        public int WebinarID { get; set; }
        public string WebinarTitle { get; set; }
        public string EventDate { get; set; }
        public string EventTime { get; set; }
        public string TimeZoneName { get; set; }
        public int RegistrantID { get; set; }
    }

    public class RegistrationExistTemplateBO
    {
        public int WebinarID { get; set; }
        public string WebinarTitle { get; set; }
        public string EventDate { get; set; }
        public string EventTime { get; set; }
        public string TimeZoneName { get; set; }
        public string RegistrantEmail { get; set; }
        public int RegistrantID { get; set; }
        public string RegisteredOn { get; set; }
        public string RegisteredFromIP { get; set; }
    }

    public class WebinarReminderEmailTemplateBO
    {
        public int WebinarID { get; set; }
        public string WebinarTitle { get; set; }
        public string EventDate { get; set; }
        public string EventTime { get; set; }
        public string TimeZoneName { get; set; }
        public string RegistrantEmail { get; set; }
        public int RegistrantID { get; set; }
        public string AudienceURL { get; set; }
        public string CalenderReminder { get; set; }
        public string RemainingDays {get; set;}
    }

    public class WebinarAllEmailTagsBO
    {
        public int WebinarID { get; set; }
        public string WebinarTitle { get; set; }
        public string EventDate { get; set; }
        public string EventTime { get; set; }
        public string TimeZoneName { get; set; }
        public string RegistrantEmail { get; set; }
        public string RegistrantFirstName { get; set; }
        public string RegistrantLastName { get; set; }
        public string RegistrantFullName { get; set; }
        public int RegistrantID { get; set; }
        public string AudienceURL { get; set; }
        public string RegistrationURL { get; set; }
        public string CalenderReminder { get; set; }
        public string RemainingDays { get; set; }
        public string FB2EmailAddress { get; set; }
        public string Description { get; set; }
        public string EndTime { get; set; }
        public string UserEmail { get; set; }
    }
}


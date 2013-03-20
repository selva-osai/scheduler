using System;

namespace EBird.BusinessEntity
{
    public class WebinarBE
    {
        public int WebinarID { get; set; }
        public int ClientID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public int TimeZoneID { get; set; }
        public Boolean isRecurrence { get; set; }
        public string RecurrenceCriteria { get; set; }
        public int Registered { get; set; }
        public int Live { get; set; }
        public int OnDemand { get; set; }
        public int Createdby { get; set; }
        public DateTime CreatedOn { get; set; }
        public int Modifiedby { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string WebinarStatus { get; set; }
        public string DeliveryChannel { get; set; }
        public bool isPublic { get; set; }
        public bool isPasswordRequired { get; set; }
        public string WebinarPassword { get; set; }
        public bool isSSPublished { get; set; }
    }

    public class WebinarRecurrencyBE
    {
        public int WebinarID { get; set; }
        public string recurrType { get; set; }
        public string recurrCriteria { get; set; }
        public string endType { get; set; }
        public string endValue { get; set; }
    }

    public class WebinarURLs
    {
        public int URLID { get; set; }
        public int WebinarID { get; set; }
        public string RegistrationURL { get; set; }
        public string PreviewInterfaceURL { get; set; }
        public string AudienceInterfaceURL { get; set; }
        public string CommandCenterURL { get; set; }
        public string AnalyticsURL { get; set; }
        //public Boolean isEmail { get; set; }
        //public string EmailAddresses { get; set; }
        //public DateTime DateEmailCreated { get; set; }
        //public DateTime EmailSentTime { get; set; }
        //public int SentBy { get; set; }
        //public int EmailCreatedby { get; set; }
    }

    public class PresenterBE
    {
        public int PresenterID { get; set; }
        public int UserID { get; set; }
        public string PresenterName { get; set; }
        public string Title { get; set; }
        public string Organization { get; set; }
        public string Photo { get; set; }
        public string Bio { get; set; }
        public int ImageDocID { get; set; }
        public bool isExternal { get; set; }
        public bool IsEnabled { get; set; }
        public int addedWebinarID { get; set; }
    }

    public class WebinarTheme
    {
        public int WebinarID { get; set; }
        public string HeaderType { get; set; }
        //public int LogoID1 { get; set; }
        //public int LogoID2 { get; set; }
        //public int BannerID { get; set; }
        public string PriThemeColor { get; set; }
        public string SecThemeColor { get; set; }
        public string ThemeFontName { get; set; }
        public int ThemeLayoutID { get; set; }
        public string ThemeName { get; set; }
        public string Shade1 { get; set; }
        public string Shade2 { get; set; }
        public string Shade3 { get; set; }
        public string Shade4 { get; set; }
    }

    public class WebinarRegistration
    {
        public int WebinarID { get; set; }
        public bool isRegistrationEnabled { get; set; }
        public bool isVideoFile { get; set; }
        public bool isAdditionalPresenter { get; set; }
        public bool isAdditionalWebinar { get; set; }
        public string APIEmails { get; set; }
        public bool isConnectRegEmailed { get; set; }
        public string CampaignTrackerEmails { get; set; }
        public bool isCampTrackerEmailed { get; set; }
        public string FormFields { get; set; }
        public string FormFieldRequired { get; set; }
        public int videoFileDocID { get; set; }
        public bool IncludeLogoBanner { get; set; }
        public bool IncludeSummary { get; set; }
        public bool IncludeSpeakerBio { get; set; }
    }

    public class WebinarRegistrationData
    {
        public int RegistrationID { get; set; }
        public int webinarID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public string Industry { get; set; }
        public string Organization { get; set; }
        public string JobTitle { get; set; }
        public string PurchaseTime { get; set; }
        public string PurchaseRole { get; set; }
        public string NoOfEmp { get; set; }
        public string RegisteredOn { get; set; }
        public string RegFromIP { get; set; }
        public bool isAttended { get; set; }
        public bool regConfirmEmailed { get; set; }
        public bool unAttendedEmailed { get; set; }
    }

    public class Registrants
    {
        public int RegistrationID { get; set; }
        public int webinarID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string EmailAddress { get; set; }
        public string Fld1 { get; set; }
        public string Fld2 { get; set; }
        public string Fld3 { get; set; }
        public string Fld4 { get; set; }
        public string Fld5 { get; set; }
        public string Fld6 { get; set; }
        public string Fld7 { get; set; }
        public string Fld8 { get; set; }
        public string Fld9 { get; set; }
        public string Fld10 { get; set; }
        public string Fld11 { get; set; }
        public string Fld12 { get; set; }
        public string Fld13 { get; set; }
        public string Fld14 { get; set; }
        public string Fld15 { get; set; }
        public string Fld16 { get; set; }
        public string Fld17 { get; set; }
        public string Fld18 { get; set; }
        public string Fld19 { get; set; }
        public string Fld20 { get; set; }
        public string RegisteredOn { get; set; }
        public string RegFromIP { get; set; }
        public bool isAttended { get; set; }
        public bool regConfirmEmailed { get; set; }
        public bool unAttendedEmailed { get; set; }
    }

    public class WebinarRegistrants
    {
        public int RegistrantID { get; set; }
        public int webinarID { get; set; }
        public string Field1 { get; set; }
        public string Field2 { get; set; }
        public string Field3 { get; set; }
        public string Field4 { get; set; }
        public string Field5 { get; set; }
        public string Field6 { get; set; }
        public string Field7 { get; set; }
        public string Field8 { get; set; }
        public string Field9 { get; set; }
        public string Field10 { get; set; }
        public string Field11 { get; set; }
        public string Field12 { get; set; }
        public string Field13 { get; set; }
        public string Field14 { get; set; }
        public string Field15 { get; set; }
        public string Field16 { get; set; }
        public string Field17 { get; set; }
        public string Field18 { get; set; }
        public string Field19 { get; set; }
        public string Field20 { get; set; }
        public string RegisteredOn { get; set; }
        public string RegFromIP { get; set; }
        public bool isAttended { get; set; }
        public bool regConfirmEmailed { get; set; }
        public bool unAttendedEmailed { get; set; }
    }

    public class WebinarRegFormFields
    {
        public int FormFieldID { get; set; }
        public int webinarID { get; set; }
        public int FieldID { get; set; }
        public string FieldLabel { get; set; }
        public bool isRequired { get; set; }
    }

    public class WebinarRegFormQA
    {
        private int _qaID;
        private int _webinarID;
        private int _QuestionOrder;
        private string _RegFormQuestion;
        private string _QResponseType;
        private string _QResponseOptions;

        public WebinarRegFormQA()
        {
        }

        public WebinarRegFormQA(int qaID, int qstOrder, string frmQst, string respType)
        {
            _qaID = qaID;
            _QuestionOrder = qstOrder;
            _RegFormQuestion = frmQst;
            _QResponseType = respType;
        }

        public WebinarRegFormQA(int qaID, int webinarID, int qstOrder, string frmQst, string respType, string respOption)
        {
            _qaID = qaID;
            _webinarID = webinarID;
            _QuestionOrder = qstOrder;
            _RegFormQuestion = frmQst;
            _QResponseType = respType;
            _QResponseOptions = respOption;
        }

        public int qaID { get { return _qaID; } set { _qaID = value; } }
        public int webinarID { get { return _webinarID; } set { _webinarID = value; } }
        public int QuestionOrder { get { return _QuestionOrder; } set { _QuestionOrder = value; } }
        public string RegFormQuestion { get { return _RegFormQuestion; } set { _RegFormQuestion = value; } }
        public string QResponseType { get { return _QResponseType; } set { _QResponseType = value; } }
        public string QResponseOptions { get { return _QResponseOptions; } set { _QResponseOptions = value; } }

        //public int qaID { get; set; }
        //public int webinarID { get; set; }
        //public int QuestionOrder { get; set; }
        //public string RegFormQuestion { get; set; }
        //public string QResponseType { get; set; }
        //public string QResponseOptions { get; set; }

    }

    public class WebinarReferCollegue
    {
        public int WebinarID { get; set; }
        public string CollegueFirstName { get; set; }
        public string CollegueLastName { get; set; }
        public string CollegueEmail { get; set; }
        public string RefererFirstName { get; set; }
        public string RefererLastName { get; set; }
        public string RefererEmail { get; set; }
        public string ReferInitiatedIP { get; set; }
        public string EmailedOn { get; set; }
    }

    public class WebinarNotification
    {
        public int WebinarID { get; set; }
        public bool isConfirmEmailAllReg { get; set; }
        public int RegConfirmEmailContentID { get; set; }
        public int ReminderEmailContentID { get; set; }
        public int FollowupAEmailContentID { get; set; }
        public int FollowupNAEmailContentID { get; set; }
        public int InvitationContentID { get; set; }

        //public int ReminderEmailHour { get; set; }
        //public int ReminderEmailDay { get; set; }
        //public int ReminderEmailWeek { get; set; }
        //public string RegListEmailOn { get; set; }
        //public bool isEmailNewReg { get; set; }
        //public string UpdateSendToEmail { get; set; }
        //public string FollowupAttendee { get; set; }
        //public string FollowupNonAttendee { get; set; }

    }

    public class WebinarResource
    {
        public int ResourceID { get; set; }
        public int WebinarID { get; set; }
        public int DocID { get; set; }
        public string ResourceType { get; set; }
        public int ResourceOrder { get; set; }
        public string ResourceTitle { get; set; }
        public string ResourceValue { get; set; }
        public bool IsBriefcase { get; set; }
        public string LogoUrlName { get; set; }
        public string LogoUrl { get; set; }
    }

    // Following class is replica of above class, but created for reordering in audience ppt listing
    // to support IEnumeration, may need to retain this after ensuring no impact

    public class WebinarResource1
    {
        private int _ResourceID;
        private int _WebinarID;
        private int _DocID;
        private string _ResourceType;
        private int _ResourceOrder;
        private string _ResourceTitle;
        private string _ResourceValue;
        private bool _IsBriefcase;

        public WebinarResource1()
        {
        }

        public WebinarResource1(int resID, int resOrder, string resTitle)
        {
            _ResourceID = resID;
            _ResourceOrder = resOrder;
            _ResourceTitle = resTitle;
        }

        public int ResourceID { get { return _ResourceID; } set { _ResourceID = value; } }
        public int WebinarID { get { return _WebinarID; } set { _WebinarID = value; } }
        public int DocID { get { return _DocID; } set { _DocID = value; } }
        public string ResourceType { get { return _ResourceType; } set { _ResourceType = value; } }
        public int ResourceOrder { get { return _ResourceOrder; } set { _ResourceOrder = value; } }
        public string ResourceTitle { get { return _ResourceTitle; } set { _ResourceTitle = value; } }
        public string ResourceValue { get { return _ResourceValue; } set { _ResourceValue = value; } }
        public bool IsBriefcase { get { return _IsBriefcase; } set { _IsBriefcase = value; } }
    }

    public class WebinarHostBE
    {
        public int WebinarID { get; set; }
        public string WebinarHost { get; set; }
    }

    public class WebinarAuditLog
    {
        public int WebinarID { get; set; }
        public string WebinarAction { get; set; }
        public string ActionDetails { get; set; }
        public int ActionByID { get; set; }
        public string ActionBy { get; set; }
        public string ActionedOn { get; set; }
    }

    public class WebinarAudience
    {
        public int WebAudienceID { get; set; }
        public int WebinarID { get; set; }
        public string AudienceViewBgColor { get; set; }
        public int AudienceViewBgImageID { get; set; }
        public string AudienceViewBackground { get; set; }
        public int Content { get; set; }
        public int Email { get; set; }
        public int Download { get; set; }
        public int Video { get; set; }
        public int Chat { get; set; }
        public int SubmitQuestion { get; set; }
        public int SpeakerBio { get; set; }
        public int Wiki { get; set; }
        public int FaceBook { get; set; }
        public int Twitter { get; set; }
        public int LinkedIn { get; set; }
        public int Search { get; set; }
    }

    public class WebinarSearchSettings
    {
        public int WebinarID { get; set; }
        public bool isYahoo { get; set; }
        public bool isBing { get; set; }
        public bool isGoogle { get; set; }
    }

    public class WebinarPresentations
    {
        public int WebpresentationID { get; set; }
        public int WebinarID { get; set; }
        public int DocumentID { get; set; }
        public string FileName { get; set; }
    }

    public class WebinarContentBE
    {
        public int ContentID { get; set; }
        public int WebinarID { get; set; }
        public int LanguageID { get; set; }
        public string ContentType { get; set; }
        public string ContentDescription { get; set; }
    }
}


namespace EBird.BusinessEntity
{
    public class ClientBE
    {
        public int ClientID { get; set; }
        public string ClientName { get; set; }
        public string Address1 { get; set;}
        public string Address2 {get; set;}
        public string City {get; set;}
        public string State { get; set; }
        public int CountryID {get; set;}
        public string PostCode {get; set;}
        public string Phone {get; set;}
        public string Website {get; set;}
        public int IndustryID {get; set;}
        public int AnnualRevID {get; set;}
        public string ClientStatus {get; set;}
        public string CurrentPkgSubscribed {get; set;}
        public int LanguageID {get; set;}
        public string DateFormat {get; set;}
        public int TimeZoneID {get; set;}
        public bool isAutoDLSave { get; set; }
        public int CreatedBy {get; set;}
        public string CreatedOn { get; set; }
        public int ModidiedBy {get; set;}
        public string LastModified {get; set;}
        public int NoOfUsers { get; set; }
        public int NoOfActiveUsers { get; set; }
        public int NoOfWebinars { get; set; }
    }

    public class ContactBE
    {
        public int ContactID { get; set;}
	    public int ClientID { get; set;}
	    public string Contactname { get; set;}
	    public string Phone { get; set;}
	    public string Email { get; set;}
	    public string Department { get; set;}
	    public string Address1 { get; set;}
	    public string Address2 { get; set;}
        public string JobTitle { get; set; }
        public string ContactType { get; set;}
        public string ContactStatus { get; set; }
    }

    public class ConfigParameterBE
    {
        public int ClientID { get; set; }
        public int ConfigID { get; set; }
        public string Category { get; set; }
        public string ConfigName { get; set; }
        public bool IsPremium { get; set; }
        public string FeatureDetails { get; set; }
    }

    // For this release this BE will be used for subscription actions only and extended for audit actions 15/02/13
    
    public class AuditLogBE
    {
        public int AuditlogID { get; set; }
        public int ClientID { get; set; }
        public string ActionType { get; set; }
        public string ActionDetail { get; set; }
        public string ActionDate { get; set; }
        public string ActionBy { get; set; }
        public int ActionByID { get; set; }
        public int ActionID { get; set; }
    }
}
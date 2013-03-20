using System;

namespace EBird.BusinessEntity
{
    public class UserBE
    {
	    public int UserID { get; set; }
        public string EmailID { get; set; }
	    public string Password { get; set; }
	    public string FirstName { get; set; }
	    public string LastName { get; set; }
        public string FullName { get; set; }
	    public string Address { get; set; }
	    public string Telephone { get; set; }
        public string JobTitle { get; set; }
        public string Role { get; set; }
        public string Department { get; set; }
	    public int ClientID { get; set; }
	    public string UserStatus { get; set; }
	    public string RegisterDate { get; set; }
	    public int ApprovedBy { get; set; }
	    public string ApprovedOn { get; set; }
        public bool isPrimary { get; set; }
        public bool isEmailWeeklyUpdate { get; set; }
        public int TimeZoneID { get; set; }
        public int WebinarCount { get; set; }
        public int CreatedBy { get; set; }
        public string PasswordChangedOn { get; set; }
        public string AuthenticationState { get; set; }
        public bool isAutoDLSave { get; set; }
    }
}

using System.Configuration;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace EBird.Common
{
    public class Constant
    {
        public readonly static string EBirdConnectionString = ConfigurationManager.ConnectionStrings["EBirdConnectionString"].ConnectionString;
        public readonly static string DBDateFormat = ConfigurationManager.AppSettings["DBDate"].ToString();
        public readonly static string DocRepoCSS = ConfigurationManager.AppSettings["DocRepoCSS"].ToString();
        public readonly static string DocRepRoot = ConfigurationManager.AppSettings["DocRepoRoot"].ToString();
        public readonly static string DocRepoClient = ConfigurationManager.AppSettings["DocRepoClient"].ToString();
        public readonly static string WebinarbaseURL = ConfigurationManager.AppSettings["WebinarBaseURL"].ToString();
        public readonly static string WebinarViewerBaseURL = ConfigurationManager.AppSettings["WebinarViewerBaseURL"].ToString();
        public readonly static string WebinarPreviewBaseURL = ConfigurationManager.AppSettings["WebinarPreviewBaseURL"].ToString();
        public readonly static string WebinarCoCBaseURL = ConfigurationManager.AppSettings["WebinarCoCBaseURL"].ToString();
        public readonly static string WebinarAnalyticsBaseURL = ConfigurationManager.AppSettings["WebinarAnalyticsBaseURL"].ToString();

        public readonly static string CampTrackInstructionDoc = ConfigurationManager.AppSettings["CampTrackInstruction"].ToString();
        public readonly static string ConnectRegInstructionDoc = ConfigurationManager.AppSettings["ConnectRegInstruction"].ToString();

        public readonly static string CSSURL = ConfigurationManager.AppSettings["CSSURL"].ToString();
        public readonly static string CommonURL = ConfigurationManager.AppSettings["CommonURL"].ToString();
        public readonly static string ClientURL = ConfigurationManager.AppSettings["ClientURL"].ToString();
        public readonly static string BaseURL = ConfigurationManager.AppSettings["BaseURL"].ToString();

        public readonly static string DocTemplate = ConfigurationManager.AppSettings["DocRepoTemplate"].ToString();
        
        public readonly static string InvitationContentID = ConfigurationManager.AppSettings["InvitationContentID"].ToString();
        public readonly static string RegConfirmContentID = ConfigurationManager.AppSettings["RegConfirContentID"].ToString();
        public readonly static string ReminderContentID = ConfigurationManager.AppSettings["ReminderContentID"].ToString();
        public readonly static string AttendeeFollowUpContentID = ConfigurationManager.AppSettings["AttendeeFollowUpContentID"].ToString();
        public readonly static string NonAttendeeFollowUpContentID = ConfigurationManager.AppSettings["NonAttendeeFollowUpContentID"].ToString();
        
        public readonly static int MaxLogoCount = Convert.ToInt32(ConfigurationManager.AppSettings["MaxLogoCount"]);
        public readonly static int MaxPPTCount = Convert.ToInt32(ConfigurationManager.AppSettings["MaxPPTCount"]);
        public readonly static int MaxRecurrenceCount = Convert.ToInt32(ConfigurationManager.AppSettings["MaxRecurrenceCount"]);

        public readonly static Size LogoSize = new Size(180, 80);
        public readonly static Size BannerSize = new Size(600, 100);
        public readonly static Size UserProfileSize = new Size(100, 100);

        //Email related constants

        public readonly static string SMTP_HOST = ConfigurationManager.AppSettings["smtp_host"].ToString(); 
        public readonly static string SMTP_PORT = ConfigurationManager.AppSettings["smpt_port"].ToString();
        public readonly static string SMTP_USR = ConfigurationManager.AppSettings["smtp_usr"].ToString();
        public readonly static string SMTP_PASS = ConfigurationManager.AppSettings["smtp_pass"].ToString();
        public readonly static string isEmailDebug = ConfigurationManager.AppSettings["isEmailDebug"].ToString();
        public readonly static string DebugEmail = ConfigurationManager.AppSettings["DebugEmail"].ToString();

        // Package Feature ID

        public readonly static string ENT_PKG = "4,7,8,9,11,12,13,14,15,16,17,19,20,21,22,23,25,26,27,28,29,30,31,32,34,35,36,37";
        public readonly static string PRO_PKG = "12,13,19,20,21,22,23,25,26,28,29,34,35,36";
        
        // Server Time zone GMT difference

        public readonly static float SRV_TIME_DIFF = Convert.ToSingle(ConfigurationManager.AppSettings["server_gmt_difference"]);

    }

    public class DocFolder
    {
        public enum Type { Profile, Logo, WebinarDocs }
    }

    public enum ClientConfigMaster
    {
        General_Language = 1,
        General_DateFormat = 2,
        General_TimeZone = 3,
        ClinetConfig_SnapSite = 4,
        ClinetConfig_Analytics = 5,
        ClinetConfig_Audience_Settings = 6,
        ClinetConfig_Registration_Settings = 7,
        Audi_Component_Content = 8,
        Audi_Component_EmailFriend = 9,
        Audi_Component_Download_Slides = 10,
        Audi_Component_Group_Chat = 11,
        Audi_Component_Submit_Question = 12,
        Audi_Component_Speaker_Bio = 13,
        Audi_Component_Wikipedia = 14,
        Audi_Component_Facebook = 15,
        Audi_Component_Twitter = 16,
        Audi_Component_LinkedIn = 17,
        Audi_Component_Upload_Video = 18,
        Audi_Component_Search = 19,
        Command_Center_Slide_Organizer = 20,
        Command_Center_Slide_Preview = 21,
        Command_Center_Share_Desktop = 22,
        Command_Center_Audience_Chat = 23,
        Command_Center_Components = 24,
        Command_Center_Share_Desktop1 = 25,
        Command_Center_Webcam = 26,
        Command_Center_Video_Clips = 27,
        Command_Center_Polls = 28,
        Command_Center_URLs = 29,
        Command_Center_Test = 30,
        Command_Center_Survey = 31,
        ClinetConfig_Email_Registrant_Updates = 32,
        ClinetConfig_Schedula_Webinar_Password_Required = 33
    }

    public enum AuditActions
    {
        Client_profile_creation = 1,
        configuration_setting = 2,
        Configuration_updates = 3,
        Package_change = 4,
        User_seat_addition = 5,
        Client_inactive_vs_Active = 6
    }

}

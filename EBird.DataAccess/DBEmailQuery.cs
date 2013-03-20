using EBird.Common;

namespace EBird.DataAccess
{
    public class DBEmailQuery
    {
        private static string dbName = ""; //"368818_EBDEVMYSQL.";

        public readonly static string sqlRequestInsert = "Insert into tblEmailingRequest (mailType,subject,emailContent,fromEmail,fromName,isToEmailRef,ToEmail,ToEmailName,submittedBy,requestStatus) "
            + " values(@mailType,@subject,@emailContent,@fromEmail,@fromName,@isToEmailRef,@ToEmail,@ToEmailName,@submittedBy,@requestStatus)";

        public readonly static string sqlEmailToInsert = "Insert into tblEmailingTo(emailRequestID,ToType,ToEmails) values(@emailRequestID,@ToType,@ToEmails)";

        //DidNotAttend
        public readonly static string sqlNotAttendedRegistrantEmail = "select concat(fld1,' ',fld2) as Registrant, fld3 as Email from tblRegistrants where isAttended=1 and webinarID=@webinarID";
        //Attended
        public readonly static string sqlAttendedRegistrantEmail = "select concat(fld1,' ',fld2) as Registrant, fld3 as Email from tblRegistrants where isAttended=0 and webinarID=@webinarID";
        //AttendedLive
        public readonly static string sqlAttendedLiveRegistrantEmail = "select concat(fld1,' ',fld2) as Registrant, fld3 as Email from tblRegistrants where PostLiveRequestType='Live' and webinarID=@webinarID";
        //OnDemand
        public readonly static string sqlAttendedOnDemandRegistrantEmail = "select concat(fld1,' ',fld2) as Registrant, fld3 as Email from tblRegistrants where PostLiveRequestType='OnDemand' and webinarID=@webinarID";
        //Registered
        public readonly static string sqlRegistrantEmail = "select concat(fld1,' ',fld2) as Registrant, fld3 as Email from tblRegistrants where webinarID=@webinarID";
        //Webinar email settings
        public readonly static string sqlRegistrantEmailSetting = "select * from tblregistrantemailsetting where webinarID=@webinarID and SettingType=@SettingType";

        public readonly static string sqlRegistrantEmailSettingInsert = "Insert into tblregistrantemailsetting (webinarID,intervalType,intervalValue,SettingType,EmailScheduleStatus) "
            + " values(@webinarID,@intervalType,@intervalValue,@SettingType,@EmailScheduleStatus)";

        public readonly static string sqlRegistrantEmailSettingUpdate = "Update tblregistrantemailsetting set webinarID=@webinarID,intervalType=@intervalType,intervalValue=@intervalValue, "
            + "SettingType=@SettingType,EmailScheduleStatus=@EmailScheduleStatus where setID=@setID";

        public readonly static string sqlRegistrantUpdatesAdd = "Insert into tblregistrantupdate(IsRegularUpdate,UpdateWeekday,UpdateTime,isUpdateWhenReg,updateToEmails,WebinarID) "
            + " values(@IsRegularUpdate,@UpdateWeekday,@UpdateTime,@isUpdateWhenReg,@updateToEmails,@WebinarID)";

        public readonly static string sqlRegistrantUpdatesSave = "Update tblregistrantupdate set IsRegularUpdate=@IsRegularUpdate,UpdateWeekday=@UpdateWeekday,UpdateTime=@UpdateTime,"
            + " isUpdateWhenReg=@isUpdateWhenReg,updateToEmails=@updateToEmails where WebinarID=@WebinarID";

        public readonly static string sqlGetRegistrantUpdates = "Select * from tblregistrantupdate where WebinarID=@WebinarID";

        public readonly static string sqlWebinarEmailInsert = "Insert into tblwebinaremail(emailSubject,emailContent,isSystemReq,isCalenderAddition,emailType,webinarID) values(@emailSubject,@emailContent,@isSystemReq,@isCalenderAddition,@emailType,@webinarID)";

        public readonly static string sqlWebinarEmailUpdate = "Update tblwebinaremail set emailSubject=@emailSubject,emailContent=@emailContent,isSystemReq=@isSystemReq,isCalenderAddition=@isCalenderAddition where emailType=@emailType and webinarID=@webinarID";

        public readonly static string sqlWebinarEmailSelect = "Select * from tblwebinaremail where webinarID=@webinarID and emailType=@emailType";

        public readonly static string sqlWebinarEmailDelete = "Delete from tblwebinaremail where webinarID=@webinarID and emailType=@emailType";

        public readonly static string sqlDefaultEmailContentSelect = "select * from tbldefaultemailcontent where languageID=@languageID and emailType=@emailType";

        public readonly static string sqlDefaultEmailContentUpdate = "Update tbldefaultemailcontent set emailContent=@emaillContent,emailSubject=@emailSubject where languageID=@languageID and emailType=@emailType";

        public readonly static string sqlDefaultWebinarEmailContentInsert = "Insert into tblwebinaremail(webinarID,emailSubject,emailContent,emailType) "
                    + "select @webinarID,emailSubject,emailContent,emailType from tbldefaultemailcontent where languageID=@languageID and emailType=@emailType";

    }
}

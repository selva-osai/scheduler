using EBird.Common;

namespace EBird.DataAccess
{
    class DBQuery
    {
        private static string dbName = ""; //"368818_EBDEVMYSQL.";

        public readonly static string sqlUserAuth = "Select * from " + dbName + "tbluseracct where emailID=@userName and userPassword=@userPassword";

        #region Master Data Queries

        public readonly static string sqlConfigMaster = "Select * from " + dbName + "tblconfigmaster order by configID";

        public readonly static string sqlConfigPkgMaster = "Select * from " + dbName + "tblpackagefeature where package=@package order by featureID";

        #endregion

        #region Client Entity Queries
        // Client entity related queries

        public readonly static string sqlClientInsert = "Insert into " + dbName + "tblclient (clientName, address1,address2,city,state,countryID," +
                                "postCode,phone,website,industryID,annualRevID,NoOfUsers,clientStatus,currentPkgSubscribed,createdBy) " +
                                " values(@clientName, @address1,@address2,@city,@state,@countryID,@postCode,@phone,@website,@industryID,@annualRevID,@NoOfUsers,@clientStatus,@currentPkgSubscribed,@createdBy)";

        public readonly static string sqlClientUpdate = "Update " + dbName + "tblclient set clientName=@clientName, address1=@address1, address2=@address2, city=@city, state = @state,countryID = @countryID," +
                                "postCode = @postCode, phone = @phone, website = @website, industryID = @industryID, annualRevID = @annualRevID, NoOfUsers = @NoOfUsers, clientStatus = @clientStatus, currentPkgSubscribed = @currentPkgSubscribed, createdBy = @createdBy where clientID=@clientID ";

        public readonly static string sqlClientStatusUpdate = "Update " + dbName + "tblclient set clientStatus=@clientStatus,modifiedBy=@modifiedBy,lastModified=now() where clientID=@clientID";

        public readonly static string sqlGetPrimaryAdmin = "Select * from " + dbName + "tbluseracct where role='Admin' and clientID=@clientID and IsPrimary=1";

        public readonly static string sqlContactInsert = "Insert into " + dbName + "tblcontacts (ContactName,Phone,Email,Department,Address1,clientID,ContactStatus,jobTitle) "
                    + " values(@ContactName,@Phone,@Email,@Department,@Address1,@clientID,@ContactStatus,@jobTitle)";

        public readonly static string sqlContactUpdate = "Update " + dbName + "tblcontacts set ContactName=@ContactName,Phone=@Phone, Email = @Email, Department = @Department, Address1 = @Address1, clientID = @clientID, ContactStatus = @ContactStatus, jobTitle = @jobTitle where ContactID=@ContactID";

        public readonly static string sqlGetClientContact = "Select * from " + dbName + "tblcontacts where clientID=@clientID";

        public readonly static string sqlClientConfigInsert = "Insert into " + dbName + "tblclientconfigdetail(configID,clientID,isActive) values(@configID,@clientID,1)";

        public readonly static string sqlClientConfigInit = "Insert into " + dbName + "tblclientconfigdetail(configID,clientID) " +
                             "select featureID,@clientID from " + dbName + "tblpackagefeature where package = @package";

        public readonly static string sqlCustomFeatureInsert = "Insert into " + dbName + "tblclientconfigdetail(configID,clientID) " +
                             "select configID,@clientID from " + dbName + "tblconfigmaster where configID in (@featureList)";

        public readonly static string sqlClientPackageUpdate = "Update " + dbName + "tblclient set currentPkgSubscribed = @currentPkgSubscribed where clientID = @clientID";

        public readonly static string sqlPkgHistoryInsert = "Insert into " + dbName + "tblclientpkghistory(clientID,pkgName) values(@clientID,@pkgName)";

        public readonly static string sqlClientConfigDel = "Delete from " + dbName + "tblclientconfigdetail where clientID=@clientID";

        //public readonly static string sqlClientHeader = "select clientID,clientName,currentPkgSubscribed,clientStatus from " + dbName + "tblclient where clientID=@clientID";

        public readonly static string sqlClientConfig = "Select * from " + dbName + "tblclientconfigdetail where clientID=@clientID";

        public readonly static string sqlClientDefaultConfigUpdate = "Update " + dbName + "tblclient set timezoneID = @timezoneID, languageID = @languageID, dateFormat = @dateFormat,isAutoDLSave = @isAutoDLSave where clientID = @clientID";

        //public readonly static string sqlClientSubscription = "select * from " + dbName + "tblclientpkghistory where clientID = @clientID order by subscribedDate";

        public readonly static string sqlClientSubscription = "select pkgHisID,clientID,pkgName,DATE_FORMAT(subscribedDate,'%Y/%m/%d %h:%m %p') as subscribedDate  from " + dbName + "tblclientpkghistory where clientID = 1 order by subscribedDate";

        public readonly static string vwClient = "Select clientID,clientName,_displayName,address1,address2,city,state," + dbName + "tblclient.countryID,postCode,phone,website," + dbName + "tblclient.industryID,annualRevID,NoOfUsers ,clientStatus,currentPkgSubscribed,languageID,dateFormat,timeZoneID,isAutoDLSave,createdBy,createdOn,modifiedBy,lastModified,countryCode,countryName,IndustryName from ((" + dbName + "tblclient join " + dbName + "tblmascountry on ((" + dbName + "tblclient.countryID = " + dbName + "tblmascountry.countryID))) join " + dbName + "tblmasindustry on ((" + dbName + "tblclient.industryID = " + dbName + "tblmasindustry.IndustryID)))";

        public readonly static string vwClientBetweenDates = vwClient + " where createdOn BETWEEN @date1 AND @date2 ";

        #endregion

        #region User Entity Queries
        // User entity related queries

        public readonly static string sqlUserInsert = "Insert into " + dbName + "tbluseracct(emailID,userPassword,FirstName,LastName,Address1,Telephone,Role,clientID,Department,userStatus,isPrimary) "
            + " values(@emailID,@userPassword,@FirstName,@LastName,@Address1,@Telephone,@Role,@clientID,@Department,@userStatus,@isPrimary)";

        public readonly static string sqlUserUpdate = "Update " + dbName + "tbluseracct set emailID=@emailID,FirstName=@FirstName,LastName=@LastName,Address1=@Address1,Telephone = @Telephone,Role = @Role,clientID = @clientID,Department = @Department,userStatus =@userStatus,isPrimary = @isPrimary Where userID=@userID";

        public readonly static string sqlUserProfileUpdate = "Update " + dbName + "tbluseracct set FirstName=@FirstName,LastName=@LastName,Telephone = @Telephone,Department = @Department,jobTitle=@jobTitle Where userID=@userID";

        public readonly static string sqlClientUsers = "Select * from " + dbName + "tbluseracct where clientID=@clientID";

        public readonly static string sqlClientUserDetail = "Select * from " + dbName + "tbluseracct where userID=@userID";

        public readonly static string sqlClientUserDetailEmail = "Select * from " + dbName + "tbluseracct where emailID=@emailID";

        public readonly static string sqlClientUserDelete = "Update " + dbName + "tbluseracct set userstatus='Inactive' where userID=@userID";
        // MiG 11/29/2012
        public readonly static string sqlClientUserUnlockStatus = "Update " + dbName + "tbluseracct set userStatus =@userStatus Where emailID=@emailID";

        public readonly static string sqlClientUserUnlock = "Update " + dbName + "tbluseracct set userstatus='Active' where userID=@userID";

        public readonly static string sqlClientUserCount = "Select count(*) as NoOfAccount,a.NoOfUsers as NoOfSeats from tblclient a, tbluseracct b where a.clientID= b.clientID and a.clientID=@clientID and b.userStatus in ('Active','Inactive') group by a.NoOfUsers";

        public readonly static string sqlUserAcctSettingUpdate = "Update " + dbName + "tbluseracct set isEmailWeekly=@isEmailWeekly,TimeZoneID=@TimeZoneID,isAutoDLSave=@isAutoDLSave where userID=@userID";

        public readonly static string sqlUserAcctAdminCount = "Select count(*) from tbluseracct where Role='Admin' and userStatus='Active' and clientID=@client";

        #endregion

        #region configuration queries

        public readonly static string GetConfigWebinarPassword = "select * from tblclientconfigdetail where clientID=@clientID and configID=@configID";

        #endregion

        #region Webinar - Presenter

        public readonly static string sqlUserPresenter = "Select * from " + dbName + "tblpresenter where userID=@userID";
        public readonly static string sqlUserPresenterID = "Select * from " + dbName + "tblpresenter where presenterID=@userID";

        public readonly static string sqlUserPresenterInsert = "Insert into " + dbName + "tblpresenter(presenterName,title,organization,presenterBio,userID) values(@presenterName,@title,@organization,@presenterBio,@userID)";

        public readonly static string sqlUserPresenterUpdate = "Update " + dbName + "tblpresenter set presenterName=@presenterName,title=@title,organization=@organization,presenterBio=@presenterBio where userID=@userID";

        public readonly static string sqlUserPresenterProfileDocInsert = "Insert into " + dbName + "tblpresenter(imgDocID,userID) values(@imgDocID,@userID)";

        public readonly static string sqlUserPresenterProfileDocUpdate = "Update " + dbName + "tblpresenter set imgDocID=@imgDocID where userID=@userID";

        public readonly static string sqlPresenterInsert = "Insert into tblpresenter (userID,presenterName,title,organization,presenterBio,imgDocID,isExternal) values(@userID,@presenterName,@title,@organization,@presenterBio,@imgDocID,@isExternal)";

        public readonly static string sqlPresenterDetail = "select a.*, a.presenterName as PresenterInfo, b.isEnabled from tblpresenter a, tbladditionalpresenter b"
            + " where a.presenterID = b.presenterID and b.webinarID = @webinarID and a.presenterID = @presenterID";

        public readonly static string sqlPresenterUpdateEnabled = "Update tbladditionalpresenter set isEnabled = @isEnabled where presenterID = @presenterID and webinarID = @webinarID";

        //public readonly static string sqlWebinarPresenterSelect = "Select *,presenterName as PresenterInfo from tblpresenter where userid in (select createdBy from tblwebinars where webinarID=@webinarID) union "
        //    + " select *,presenterName as PresenterInfo from tblpresenter where presenterID in (select presenterID from tbladditionalpresenter where webinarID=@webinarID)";

        public readonly static string sqlWebinarPresenterSelect1 = "select a.*,a.presenterName as PresenterInfo from tblpresenter a, tbladditionalpresenter b where a.presenterID = b.presenterID and b.webinarID = @webinarID";

        public readonly static string sqlWebinarPresenterSelect2 = "select *,presenterName as PresenterInfo from tblpresenter where presenterID in (select presenterID from tbladditionalpresenter where webinarID=@webinarID)";

        #endregion

        #region Webinar - event registration

        public readonly static string sqlWebinarRegistrantInsert = "Insert into tblregistrants(webinarID,fld1,fld2,fld3,fld4,fld5,fld6,fld7,fld8,fld9,fld10,fld11,fld12,fld13,fld14,fld15,"
             + "fld16,fld17,fld18,fld19,fld20,regFromIP) values(@webinarID,@fld1,@fld2,@fld3,@fld4,@fld5,@fld6,@fld7,@fld8,@fld9,@fld10,@fld11,@fld12,@fld13,@fld14,@fld15,"
             + "@fld16,@fld17,@fld18,@fld19,@fld20,@regFromIP)";

        public readonly static string sqlWebinarRegistrantQAInsert = "Insert into tblregistrantqaresponse(regID,qaID,QAResponse) values(@regID,@qaID,@QAResponse)";

        public readonly static string sqlWebinarRegistrantSelectMin = "select regID,fld1 as FirstName,fld2 as LastName, CONCAT(fld2, ', ' ,fld1) as Registrant, fld3 as Email, registeredOn from tblregistrants where webinarID=@webinarID";

        public readonly static string sqlWebinarRegistrantSelect = "select * from tblregistrants where webinarID=@webinarID";

        public readonly static string sqlWebinarRegistrantDetailSelect = "select regID,fld1 as FirstName,fld2 as LastName, CONCAT(fld2, ', ' ,fld1) as Registrant, fld3 as Email, registeredOn, regFromIP from tblregistrants where webinarID=@webinarID and fld3=@registrantEmail";

        public readonly static string sqlWebinarEventRegCountUpdate = "Update tblwebinars set registered = (select count(*) from tblregistrants where webinarID=@webinarID) where webinarID=@webinarID";

        public readonly static string sqlWebinarEventRelatedWebinar = "select * from tblwebinars where webinarID in (select relatedWebinarID from tbladditionalwebinar where webinarID=@webinarID)";

        public readonly static string sqlWebinarEventUpdateVideoFile = "Update tblwebinarregistration set videoFileID=@videoFileID where webinarID=@webinarID";

        #endregion

        #region webinar - Refer Collegue

        public readonly static string sqlGetReferCollegue = "select * from tblrefercolleague where webinarID = @webinarID and colleagueemailID = @collegueEmailID and refereremailID=@referEmailID";

        public readonly static string sqlInsertReferColleque = "Insert into tblrefercolleague(webinarID,colleaguefirstname,colleaguelastname,colleagueemailID,refererfirstname,refererlastname,refereremailID,referInitiatedIP) "
           + " values(@webinarID,@colleaguefirstname,@colleaguelastname,@colleagueemailID,@refererfirstname,@refererlastname,@refereremailID,@referInitiatedIP)";
        #endregion

        public readonly static string sqlWebinarSchInsert = "Insert into tblwebinars(webinarTitle,description,startDate,startTime,endTime,recurrence,timeZoneID,deliveryChannel,isPublic,isPassRequired,webinarPassword,createdBy) "
           + " values(@webinarTitle,@description,@startDate,@startTime,@endTime,@recurrence,@timeZoneID,@deliveryChannel,@isPublic,@isPassRequired,@webinarPassword,@createdBy)";

        public readonly static string sqlWebinarSchUpdate = "Update tblwebinars set webinarTitle=@webinarTitle,description=@description,startDate=@startDate,startTime=@startTime,endTime=@endTime,recurrence=@recurrence,timeZoneID=@timeZoneID,"
           + " deliveryChannel=@deliveryChannel,isPublic=@isPublic,isPassRequired=@isPassRequired,webinarPassword=@webinarPassword where webinarID=@webinarID";

        public readonly static string sqlWebinarUpdateDefaultTheme = "Update tblwebinars set audienceThemeID = (select audienceThemeID from tblclient where clientID = @clientID),"
                    + " registrationThemeID = (select registrationThemeID from tblclient where clientID = @clientID),"
                    + " InvitationThemeID = (select InvitationThemeID from tblclient where clientID = @clientID) where webinarID=@webinarID";

        public readonly static string sqlWebinarThemeDetails = "select * from tblmastheme where EBthemeID in (select audienceThemeID from tblwebinars where webinarID = @webinarID"
                    + " union select registrationThemeID from tblwebinars where webinarID = @webinarID"
                    + " union select InvitationThemeID from tblwebinars where webinarID = @webinarID)";

        public readonly static string sqlWebinarRecurrDetail = "Select * from tblrecurrencedetail where webinarID=@webinarID";

        public readonly static string sqlWebinarRecurrInsert = "Insert into tblrecurrencedetail(recurrType,endType,endValue,recurrCriteria,webinarID) values(@recurrType,@endType,@endValue,@recurrCriteria,@webinarID)";

        public readonly static string sqlWebinarRecurrUpdate = "Update tblrecurrencedetail set recurrType = @recurrType, endType = @endType, endValue = @endValue, recurrCriteria=@recurrCriteria where webinarID = @webinarID";

        public readonly static string sqlGetMyWebinar = "Select * from tblwebinars where createdBy = @createdBy and webinarstatus <> 'Deleted' ";

        public readonly static string sqlGetMyRecycleWebinar = "Select * from tblwebinars where createdBy = @createdBy and webinarstatus = 'Deleted' ";

        public readonly static string sqlGetWebinarDetail = "Select * from tblwebinars where webinarID = @webinarID";

        //public readonly static string sqlWebinarAuditInsert = "Insert into tblauditwebinar(webinarID,action,actionBy) values(@webinarID,@action,@actionBy)";

        public readonly static string sqlGetMyCompanyWebinar = "Select * from tblwebinars where createdBy in (select userID from tbluseracct where clientID=@clientID) and webinarstatus <> 'Deleted' ";

        public readonly static string sqlGetMyCompanyRecycleWebinar = "Select * from tblwebinars where createdBy in (select userID from tbluseracct where clientID=@clientID) and webinarstatus = 'Deleted' ";

        //public readonly static string sqlWebinarDelete = "Update tblwebinars set webinarStatus='Inactive' where webinarID=@webinarID";

        #region snapsite

        public readonly static string sqlGetMyPublicWebinar = "SELECT * FROM tblwebinars where createdBy = @createdBy and isPublic=1 and webinarStatus='Active'";

        #endregion

        #region Webinar - URLs
        public readonly static string sqlGetWebinarURL = "Select * from tblwebinarurl where webinarID=@webinarID";

        public readonly static string sqlInsertWebinarURL = "insert into tblwebinarurl(webinarID,RegURLKey,PreviewURLKey,AudiURLKey,CoCenterURLKey,AnalyticURLKey) values(@webinarID,@RegURLKey,@PreviewURLKey,@AudiURLKey,@CoCenterURLKey,@AnalyticURLKey)";

        public readonly static string sqlGetWebinarRegURL = "SELECT webinarID FROM tblwebinarurl where RegURLKey=@key";

        public readonly static string sqlGetWebinarPreviewURL = "SELECT webinarID FROM tblwebinarurl where PreviewURLKey=@key";

        public readonly static string sqlGetWebinarAudiURL = "SELECT webinarID FROM tblwebinarurl where AudiURLKey=@key";

        public readonly static string sqlGetWebinarCoCURL = "SELECT webinarID FROM tblwebinarurl where CoCenterURLKey=@key";

        #endregion

        public readonly static string sqlGetWebinarHosts = "select * from tblwebinarhost where webinarID=@webinarID";

        public readonly static string sqlGetWebinarHostsExist = "select * from tblwebinarhost where webinarID=@webinarID and domainURL=@domainURL";

        public readonly static string sqlWebinarHostsInsert = "Insert into tblwebinarhost(webinarID, domainURL) values(@webinarID,@domainURL)";

        #region Webinar - Theme

        public readonly static string sqlWebinarThemeInsert = "Insert into tblwebinartheme(webinarID,headerType,thPriColor,thSecColor,thFontName,thLayoutID) values(@webinarID,@headerType,@thPriColor,@thSecColor,@thFontName,@thLayoutID)";

        public readonly static string sqlWebinarThemeSelect = "select * from tblwebinartheme where webinarID=@webinarID";

        public readonly static string sqlWebinarThemeLogo1Update = "update tblwebinartheme set logoID1 = @logoID1, headerType='Logo' where webinarID=@webinarID";

        public readonly static string sqlWebinarThemeLogo2Update = "update tblwebinartheme set logoID2 = @logoID1, headerType='Multi-logo'  where webinarID=@webinarID";

        public readonly static string sqlWebinarThemeBannerUpdate = "update tblwebinartheme set bannerID = @logoID1, headerType='BANNER' where webinarID=@webinarID";

        public readonly static string sqlWebinarThemeUpdate = "update tblwebinartheme set headerType=@headerType, thLayoutID=@thLayoutID where webinarID=@webinarID";

        #endregion

        #region Webinar - audience

        public readonly static string sqlWebinarAudiDefaultInsert = "Insert into tblwebinaraudience(webinarID,audiBgColor,audiBackGround) values(@webinarID,@audiBgColor,@audiBackGround)";

        public readonly static string sqlWebinarAudiSelect = "Select * from tblwebinaraudience where webinarID=@webinarID";

        public readonly static string sqlWebinarAudiFieldUpdate = "Update tblwebinaraudience set ##FieldName = ##FieldValue where webinarID=@webinarID";

        public readonly static string sqlWebinarAudiPresentation = "select a.*,b.OrgFilename,b.SavedFileName from tblwebinarpresentations a, tbldocreference b where a.docID = b.docID and webinarID=@webinarID";

        public readonly static string sqlWebinarAudiComponentUpdate = "update tblwebinaraudience set audiBackGround=@audiBackGround,Download=@Download,chat=@chat,submitQuestion=@submitQuestion,Wiki=@Wiki,Email=@Email where webinarID=@webinarID";

        #endregion

        public readonly static string sqlWebinarRegDefaultInsert = "Insert into tblwebinarregistration(webinarID,isRegEnabled,isVideoFile,isAddPresenter,isAddWebinar,ConnectedAPIEmails,inclLogoBanner,inclSummary,inclSpeakerBio) "
            + " values(@webinarID,@isRegEnabled,@isVideoFile,@isAddPresenter,@isAddWebinar,@ConnectedAPIEmails,@inclLogoBanner,@inclSummary,@inclSpeakerBio)";

        public readonly static string sqlWebinarRegUpdate = "Update tblwebinarregistration set isVideoFile=@isVideoFile,isAddWebinar=@isAddWebinar,ConnectedAPIEmails=@ConnectedAPIEmails,inclLogoBanner=@inclLogoBanner,inclSummary=@inclSummary,inclSpeakerBio=@inclSpeakerBio,campTrackerEmails=@campTrackerEmail where webinarID=@webinarID";

        #region Webinar - Registration

        public readonly static string sqlWebinarRegSelect = "Select * from tblwebinarregistration where webinarID=@webinarID";

        public readonly static string sqlWebinarRegDefaultFormFieldsInsert = "Insert into tblregistrationformfields(webinarID,regfieldID,fieldLabel,isRequired) "
                                        + "select @webinarID,regFormFieldID,FormFieldDisplayName,1 from tblmasregformfield where regFormFieldID in (1,2,3)";

        public readonly static string sqlWebinarRegFormFieldsSelect = "Select * from tblregistrationformfields where webinarID = @webinarID";

        public readonly static string sqlWebinarRegFormFieldsInsert = "Insert into tblregistrationformfields(webinarID,regfieldID,fieldLabel,isRequired) values(@webinarID,@regfieldID,@fieldLabel,@isRequired)";

        public readonly static string sqlWebinarRegFormFieldsUpdate = "Update tblregistrationformfields set fieldLabel=@fieldLabel,isRequired=@isRequired where regfieldID = @regfieldID and webinarID = @webinarID";

        public readonly static string sqlWebinarRegFormFieldsDelete = "Delete from tblregistrationformfields where webinarID=@webinarID";

        public readonly static string sqlWebinarRegistrantFormQASelect = "select * from tblregformqa where webinarID=@webinarID order by QuestionOrder";

        public readonly static string sqlWebinarQAReOrderUpdate = "Update tblregformqa set QuestionOrder=@OrderNo where qaID=@qaID";

        public readonly static string sqlWebinarRegistrantFormQA = "select * from tblregformqa where qaID=@qaID";

        public readonly static string sqlWebinarRegistrantFormQAOrderUpdate = "Update tblregformqa set Questionorder = Questionorder-1 where webinarID=@webinarID and qaID >= @qaID";

        public readonly static string sqlWebinarRegistrantFormqaDelete = "Delete from tblregformqa where qaID = @qaID";

        public readonly static string sqlWebinarRegistrantFormQAInsert = "Insert into tblregformqa(WebinarID,QuestionOrder,RegFormQuestion,QResponseType,QResponseOptions) "
            + " Select @webinarID,coalesce(NULLIF(max(questionorder)+1, ''),1) as questOrder,@RegFormQuestion,@QResponseType,@QResponseOptions from tblregformqa where webinarID=@webinarID1";

        public readonly static string sqlWebinarRegistrantFormQAUpdate = "Update tblregformqa set RegFormQuestion=@RegFormQuestion,QResponseType=@QResponseType,QResponseOptions=@QResponseOptions where qaID=@qaID";

        public readonly static string sqlWebinarRegStatusUpdate = "Update tblwebinarregistration set isRegEnabled=@isRegEnabled where webinarID=@webinarID";

        public readonly static string sqlWebinarResourceLogoSelect = "Select * from tblwebinarregresources where webinarID=@webinarID and resourceType in ('Logo','BANNER') order by resourceOrder";

        public readonly static string sqlWebinarResourceTypeSelect = "Select * from tblwebinarregresources where webinarID=@webinarID and resourceType in (###)  order by resourceOrder";
        
        public readonly static string sqlWebinarResourceIDSelect = "Select * from tblwebinarregresources where regresourceID=@regresourceID";

        public readonly static string sqlWebinarResourceOrderUpdate = "Update tblwebinarregresources set resourceOrder=@resourceOrder where regresourceID=@resID";

        public readonly static string sqlWebinarDocIDSelect = "Select * from tblwebinarregresources where docId=@DocId";

        public readonly static string sqlWebinarResourceInsert = "Insert into tblwebinarregresources(webinarID,docID,resourceType,resourceOrder,ResourceTitle,resourceValue) "
            + " Values(@webinarID,@docID,@resourceType,@resourceOrder,@ResourceTitle,@resourceValue)";

        public readonly static string sqlWebinarResourceInsert1 = "Insert into tblwebinarregresources(webinarID,docID,resourceType,resourceOrder,ResourceTitle,resourceValue,isAddToBriefcase) "
            + "Select @webinarID,@docID,@resourceType,coalesce(NULLIF(max(resourceorder)+1, ''),1), @ResourceTitle,@resourceValue,@isAddToBriefcase from tblwebinarregresources where webinarID=@webinarID1 and resourceType=@resourceType1";

        public readonly static string sqlWebinarResourceLogoOrderUpdate = "Update tblwebinarregresources set resourceOrder=@resourceOrder where webinarID=@webinarID and docID=@docID";

        public readonly static string sqlWebinarResourceDelete = "Delete from tblwebinarregresources where docID=@docID and webinarID=@webinarID";

        public readonly static string sqlWebinarResourceIDDelete = "Delete from tblwebinarregresources where regresourceID=@regresourceID";

        #endregion

        public readonly static string sqlWebinarNotifyDefaultInsert = "Insert into tblwebinarnotification(webinarID,isConfirmEmailAllReg,RegConfirmEmailContentID,ReminderEmailContentID,"
                       + "FollowupAEmailContentID,FollowupNAEmailContentID,invitationContentID) values(@webinarID,@isConfirmEmailAllReg,@RegConfirmEmailContentID,@ReminderEmailContentID,"
                       + "@FollowupAEmailContentID,@FollowupNAEmailContentID,@invitationContentID)";

        public readonly static string sqlWebinarNotifySelect = "Select * from tblwebinarnotification where webinarID=@webinarID";

        #region Master Entity Queries

        public readonly static string sqlGetCountryMaster = "select * from tblmascountry order by countryName";

        public readonly static string sqlGetIndustryMaster = "select * from tblmasindustry order by IndustrtName";

        public readonly static string sqlGetThemeMaster = "Select * from " + dbName + "tblmastheme where EBThemeStatus = 'Active' order by EBThemeSort asc";

        public readonly static string sqlGetThemeMasterByCategory = "Select * from " + dbName + "tblmastheme where EBThemeStatus = 'Active' and ThemeCategory=@ThemeCategory order by EBThemeSort asc";

        public readonly static string sqlGetThemeMasterDetail = "Select * from " + dbName + "tblmastheme where EBThemeID=@EBThemeID";

        public readonly static string sqlGetFeatureMasterDetail = "select a.*,b.featureName,b.isConfig,b.featureCategory from tblpackagefeature a, tblmasfeature b where a.featureID=b.featureID and package=@package";

        public readonly static string sqlGetFeatureMasterDetailByID = "select * from tblmasfeature where featureID=@featureID";

        public readonly static string sqlGetTimeZoneDetail = "Select * from " + dbName + "tblmastimezone where TimeZoneID=@TimeZoneID";

        #endregion

        #region Document Entity Queries

        public readonly static string sqlGetDocumentDetail = "Select * from " + dbName + "tbldocreference where DocID=@DocID";

        public readonly static string sqlDocumentInsert = "Insert into " + dbName + "tbldocreference (ClientID,Category,OrgFileName,savedFileName,InsertedBy) " +
                                                          " values(@ClientID,@Category,@OrgFileName,@savedFileName,@InsertedBy)";

        public readonly static string sqlDocumentUpdate = "Update " + dbName + "tbldocreference set ClientID=@ClientID,Category=@Category,OrgFileName=@OrgFileName,savedFileName=@savedFileName where DocID=@DocID";

        public readonly static string sqlDocumentFieldUpdate = "Update " + dbName + "tbldocreference set ##FieldName = ##FieldValue where DocID=@DocID";

        public readonly static string sqlDocumentDelete = "Delete from tbldocreference where DocID=@DocID";

        public readonly static string sqlProImgRefSelectPresenter = "select * from tblpresenter where imgDocID=@DocID and userID = @userID";

        public readonly static string sqlProImgRefInsertPresenter = "Insert into tblpresenter(imgDocID, userID) values(@DocID,@userID)";

        public readonly static string sqlProImgRefUpdatePresenter = "Update tblpresenter set imgDocID = @DocID where userID = @userID";

        public readonly static string sqlUserProfileImgPath = "select ClientID, Category, SavedFileName from tbldocreference where DocID in (select imgDocID from tblpresenter where userID=@userID)";

        public readonly static string sqlPresenterProfileImgPath = "select ClientID, Category, SavedFileName from tbldocreference where DocID in (select imgDocID from tblpresenter where presenterID=@presenterID)";

        public readonly static string sqlResetPresenterImgRef = "Update tblpresenter set imgDocID=0 where userID=@userID";

        #endregion

        #region webinar search

        public readonly static string sqlWebinarSearchSettingSelect = "Select * from tblwebinarsearchsetting where webinarID=@webinarID";

        public readonly static string sqlWebinarSearchSettingInsert = "Insert into tblwebinarsearchsetting(isYahoo,isBing,isGoogle,webinarID) values(@isYahoo,@isBing,@isGoogle,@webinarID)";

        public readonly static string sqlWebinarSearchSettingUpdate = "Update tblwebinarsearchsetting set isYahoo=@isYahoo,isBing=@isBing,isGoogle=@isGoogle where webinarID=@webinarID";

        #endregion

        #region webinar page contents

        public readonly static string sqlDefaultContentSelect = "select * from tbldefaultcontent where contentType=@contentType and languageID=@languageID";

        public readonly static string sqlWebinarDefaultContentInsert = "Insert into tblwebinarcontent(webinarID,contentType,contentDetail) "
                    + "select @webinarID,contentType,contentDetail from tbldefaultcontent where contentType=@contentType and languageID=@languageID";

        public readonly static string sqlWebinarContentSelect = "select * from tblwebinarcontent where webinarID=@webinarID and contentType=@contentType";

        public readonly static string sqlWebinarContentSave = "Update tblwebinarcontent set contentDetail=@contentDetail where webinarID=@webinarID and contentType=@contentType";

        #endregion

        #region audit
        public readonly static string sqlAduitrecord = "select * from tblauditlog";
        #endregion
    }
}

using EBird.Common;

namespace EBird.DataAccess
{
    public class SocialMediaQuery
    {
        #region facebook
        public readonly static string sqlFBSettingSelect = "select * from tblfacebooksetting where webinarID=@webinarID";

        public readonly static string sqlFBSettingInsert = "Insert into tblfacebooksetting (isStatusUpdate,defaultStatusMessage,isLikeUnlike,isComments,isFriend,isSearch,checkInterval,messageReturn,webinarID) "
                                              + "values(@isStatusUpdate,@defaultStatusMessage,@isLikeUnlike,@isComments,@isFriend,@isSearch,@checkInterval,@messageReturn,@webinarID)";

        public readonly static string sqlFBSettingUpdate = "Update tblfacebooksetting set isStatusUpdate=@isStatusUpdate,defaultStatusMessage=@defaultStatusMessage,isLikeUnlike=@isLikeUnlike,isComments=@isComments,"
            + " isFriend=@isFriend,isSearch=@isSearch,checkInterval=@checkInterval,messageReturn=@messageReturn where webinarID=@webinarID";
        #endregion

        #region linkedIn
        public readonly static string sqlLISettingSelect = "select * from tbllinkedinsetting where webinarID=@webinarID";

        public readonly static string sqlLISettingInsert = "Insert into tbllinkedinsetting (isLikeUnlike,isComments,isSearch,isFilterNetwork,checkInterval,messageReturn,defaultInviteSubject,defaultInviteMessage,isNetworkUpdate,isInvitation,webinarID) "
            + "values(@isLikeUnlike,@isComments,@isSearch,@isFilterNetwork,@checkInterval,@messageReturn,@defaultInviteSubject,@defaultInviteMessage,@isNetworkUpdate,@isInvitation,@webinarID)";

        public readonly static string sqlLISettingUpdate = "Update tbllinkedinsetting set isLikeUnlike=@isLikeUnlike,isComments=@isComments,isSearch=@isSearch,isFilterNetwork=@isFilterNetwork,checkInterval=@checkInterval,messageReturn=@messageReturn,defaultInviteSubject=@defaultInviteSubject,"
            + " defaultInviteMessage=@defaultInviteMessage,isNetworkUpdate=@isNetworkUpdate,isInvitation=@isInvitation where webinarID=@webinarID";
        #endregion

        #region Twitter
        public readonly static string sqlTWSettingSelect = "select * from tbltwittersetting where webinarID=@webinarID";

        public readonly static string sqlTWSettingInsert = "Insert into tbltwittersetting (twitterAcct,headerTitle,tweetHashtags,dispFromAcct,filterKeywords,isUserTweet,userHashtags,userTextURL,webinarID) "
            + " values(@twitterAcct,@headerTitle,@tweetHashtags,@dispFromAcct,@filterKeywords,@isUserTweet,@userHashtags,@userTextURL,@webinarID)";

        public readonly static string sqlTWSettingUpdate = "Update tbltwittersetting set twitterAcct=@twitterAcct,headerTitle=@headerTitle,tweetHashtags=@tweetHashtags,dispFromAcct=@dispFromAcct,"
            + "filterKeywords=@filterKeywords,isUserTweet=@isUserTweet,userHashtags=@userHashtags,userTextURL=@userTextURL where webinarID=@webinarID";
        #endregion

    }
}

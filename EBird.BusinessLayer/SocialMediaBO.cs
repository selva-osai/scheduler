
namespace EBird.BusinessEntity
{
    public class FaceBookSettingBO
    {
        public int WebinarID { get; set; }
        public bool isStatusUpdate { get; set; }
        public string defaultStatusMessage { get; set; }
        public bool isLikeUnlike { get; set; }
        public bool isComments { get; set; }
        public bool isFriend { get; set; }
        public bool isSearch { get; set; }
        public int checkInterval { get; set; }
        public int messageReturn { get; set; }
    }

    public class LinkedInSettingBO
    {
        public int WebinarID { get; set; }
        public bool isLikeUnlike { get; set; }
        public bool isComments { get; set; }
        public bool isSearch { get; set; }
        public bool isFilterNetwork { get; set; }
        public int checkInterval { get; set; }
        public int messageReturn { get; set; }
        public string defaultInviteSubject { get; set; }
        public string defaultInviteMessage { get; set; }
        public bool isNetworkUpdate { get; set; }
        public bool isInvitation { get; set; }
    }

    public class TwitterSettingBO
    {
        public int WebinarID { get; set; }
        public string twitterAcct { get; set; }
        public string headerTitle { get; set; }
        public string tweetHashtags { get; set; }
        public string dispFromAcct { get; set; }
        public string filterKeywords { get; set; }
        public bool isUserTweet { get; set; }
        public string userHashtags { get; set; }
        public string userTextURL { get; set; }
    }
}

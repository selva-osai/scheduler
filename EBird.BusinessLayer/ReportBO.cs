using System;

namespace EBird.BusinessEntity
{

    public class DailyStatusReportBO
    {
        public string userFirstName { get; set; }
        public string userLastName { get; set; }
        public int NoOfWebinar { get; set; }
        public int LastWebinarDaysAway { get; set; }
        public string NextWebinar { get; set; }
    }

    public class WebinarInfoListBO
    {
        public string UpcomingWebinar { get; set; }
        public string When { get; set; }
        public int Registrants { get; set; }
        public string Actions { get; set; }
    }

    public class GeneralWebinarTagsBO
    {
        public WebinarBE WebinarList { get; set; }
        public WebinarURLs WebinarURLList { get; set; }
        public Registrants Registrantlist { get; set; }
        public string TimeZoneShortName { get; set; }
        public string TimeZoneName { get; set; }
        public string UserEmail { get; set; }
    }

    public class themeCSSBO
    {
        public string ThemeName {get; set;}
        public string shade1 { get; set; }
        public string shade2 { get; set; }
        public string shade3 { get; set; }
        public string shade4 { get; set; }
    }
}

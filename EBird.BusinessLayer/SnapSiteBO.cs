using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EBird.BusinessEntity
{
    public class SnapSiteBO
    {
        public int UserID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string HeaderType { get; set; }
        public int SkinID { get; set; }
        public bool IsFacebook { get; set; }
        public bool IsTwitter { get; set; }
        public bool IsLinkedIn { get; set; }
        public bool IsEnabled { get; set; }
    }
}

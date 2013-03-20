using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace EBird.WebServices
{
    [DataContract]
    class PPTSlider
    {
        [DataMember]
        public string pptFileName;
    }
}

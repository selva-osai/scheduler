using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EBird.Web.App.Pagelets
{
    public partial class plDateTime : System.Web.UI.UserControl
    {
        private string _dateTime;
        public string WebinarDateTime
        {
            get
            {
                return _dateTime;
            }
            set
            {
                _dateTime = value;
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                lblDate.Text = _dateTime; 
            }
        }
    }
}
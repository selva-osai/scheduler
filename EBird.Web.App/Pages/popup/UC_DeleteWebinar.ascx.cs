using System;
using System.Collections.Generic;
using System.Text;
using EBird.BusinessEntity;
using EBird.Common;
using EBird.DataAccess;

namespace EBird.Web.App.Pages.popup
{
    public partial class UC_DeleteWebinar : System.Web.UI.UserControl
    {
        WebinarBE objWebinarBE = new WebinarBE();
        EBirdUtility objUtil = new EBirdUtility();
        WebinarDA objWebinarDA = new WebinarDA();
        EBErrorMessages objError = new EBErrorMessages();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnDelWebinar_Click(object sender, EventArgs e)
        {
        }

        protected void btnDelCancel_Click(object sender, EventArgs e)
        {
            //lbtnBack_Click(null, null);
        }
    }
}
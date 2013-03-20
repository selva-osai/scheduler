using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EBird.DataAccess;
using EBird.BusinessEntity;
using EBird.Common;
using Telerik.Web.UI;

namespace EBird.Web.App.UserControls
{
    public partial class audit : System.Web.UI.UserControl
    {
        ClientBE objClientBE = new ClientBE();
        ClientDA objClientDA = new ClientDA();
        EBirdUtility objUtil = new EBirdUtility();
        EBErrorMessages objError = new EBErrorMessages();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                auditPopulate();
            }
        }

        private void auditPopulate()
        {
            tgrdauditList.DataSource = objClientDA.GetAuditrecord();
            tgrdauditList.DataBind();
        }

        protected void tgrdauditList_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            string dtValue = "";
            if (e.Item is GridDataItem)
            {
                //Get the instance of the right type
                GridDataItem dataBoundItem = e.Item as GridDataItem;
                //dtValue = dataBoundItem["ActionDate"].Text;
                dtValue = ((EBird.BusinessEntity.AuditLogBE)(e.Item.DataItem)).ActionDate;
                if (dtValue != "")
                {
                    Label lbl = (Label)e.Item.FindControl("lblActionOn");
                    lbl.Text = Convert.ToDateTime(dtValue).ToString("MMM dd, yyyy h:mm tt");
                }
            }
        }

        protected void tgrdauditList_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "RebindGrid":
                    auditPopulate();
                    tgrdauditList.MasterTableView.Rebind();
                    break;
                case "Sort":
                    auditPopulate();
                    tgrdauditList.MasterTableView.Rebind();
                    break;
                case "Page":
                    auditPopulate();
                    tgrdauditList.MasterTableView.Rebind();
                    break;
            }
        }

    }
}
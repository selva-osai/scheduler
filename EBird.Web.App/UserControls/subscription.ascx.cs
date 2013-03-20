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
    public partial class subscription : System.Web.UI.UserControl
    {
        ClientBE objClientBE = new ClientBE();
        ClientDA objClientDA = new ClientDA();
        EBirdUtility objUtil = new EBirdUtility();
        EBErrorMessages objError = new EBErrorMessages();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (Session["config_clientID"] != null)
                {
                    hClientID.Value = Session["config_clientID"].ToString();
                    Session.Remove("config_clientID");
                    tgrdPkgHistory.DataSource = objClientDA.GetClientSubscription(Convert.ToInt32(hClientID.Value));
                    tgrdPkgHistory.DataBind();
                    getClientHeaderInfo();
                    phClientInfo.Visible = true;
                    phClientList.Visible = false;
                }
                else
                    defaultPopClients();
            }
        }


        private void getClientHeaderInfo()
        {
            if (hClientID.Value != "")
            {
                List<ClientBE> objClientBE = objClientDA.GetClientDetailDA(Convert.ToInt32(hClientID.Value));
                if (objClientBE.Count > 0)
                {
                    lblClient.Text = objClientBE[0].ClientName;
                    lblPkg.Text = objClientBE[0].CurrentPkgSubscribed;
                    if (objClientBE[0].ClientStatus == "Inactive")
                    {
                        imgStatus.ImageUrl = "~/Images/icons/InactiveStatus1.png";
                    }
                    else
                    {
                        imgStatus.ImageUrl = "~/Images/icons/ActiveStatus1.png";
                    }
                }
            }
        }


        private void defaultPopClients()
        {
            List<ClientBE> objClientBE = new List<ClientBE>();
            // objClientBE = objClientDA.GetClientDetailDA(objUtil.FormDBDate(DateTime.Now.AddMonths(-3)), objUtil.FormDBDate(DateTime.Now.AddMonths(3)));
            dpFrom.SelectedDate = DateTime.Now.AddDays(-365);
            dpTo.SelectedDate = DateTime.Now.AddDays(365);
            objClientBE = objClientDA.GetClientDetailDA(objUtil.FormDBDate(DateTime.Now.AddDays(-365)), objUtil.FormDBDate(DateTime.Now.AddDays(365)));
            tgrdClientList.DataSource = objClientBE;
            tgrdClientList.DataBind();
        }

        protected void tgrdClientList_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "View":
                    phClientInfo.Visible = true;
                    phClientList.Visible = false;
                    hClientID.Value = e.CommandArgument.ToString();
                    getClientHeaderInfo();
                    List<AuditLogBE> objAL = objClientDA.GetClientSubscription(Convert.ToInt32(e.CommandArgument));
                    tgrdPkgHistory.DataSource = objAL;
                    tgrdPkgHistory.DataBind(); 
                    break;
                case "RebindGrid":
                    txtSearch.Text = "";
                    rcmbPkgType.SelectedIndex = 0;
                    defaultPopClients();
                    tgrdClientList.MasterTableView.Rebind();
                    break;
                case "Sort":
                    popClients();
                    tgrdClientList.MasterTableView.Rebind();
                    break;
                case "Page":
                    popClients();
                    tgrdClientList.MasterTableView.Rebind();
                    break;
            }

        }

        protected void tgrdClientList_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            string dtValue = "";
            if (e.Item is GridDataItem)
            {
                //Get the instance of the right type
                GridDataItem dataBoundItem = e.Item as GridDataItem;

                //Check the formatting condition
                if (dataBoundItem["clientStatus"].Text == "Inactive")
                {
                    LinkButton lbtn = (LinkButton)dataBoundItem.FindControl("lnkView1");
                    lbtn.CssClass = "lnkInactive"; // System.Drawing.Color.Red; 
                }

                Label lbl = (Label)e.Item.FindControl("lblCreatedOn");
                dtValue = dataBoundItem["CreatedOn"].Text;
                lbl.Text = Convert.ToDateTime(dtValue).ToString("MMMM dd, yyyy");

                //lbl = (Label)e.Item.FindControl("lblWebinarCount");
                //dtValue = dataBoundItem["clientID"].Text;
                //lbl.Text = objClientDA.getWebinarCount(Convert.ToInt32(dtValue)).ToString();
            }
        }

        private void popClients()
        {
            string errMsg = "";

            List<ClientBE> objClientBE = new List<ClientBE>();
            string dt1 = "", dt2 = "";
            lblFilterError.Text = "";
            if (dpFrom.SelectedDate.ToString() != "")
                dt1 = objUtil.FormDBDate(Convert.ToDateTime(dpFrom.SelectedDate));
            if (dpTo.SelectedDate.ToString() != "")
                dt2 = objUtil.FormDBDate(Convert.ToDateTime(dpTo.SelectedDate));
            if (dt1 != "" && dt2 != "")
            {
                TimeSpan span = (Convert.ToDateTime(dpTo.SelectedDate)).Subtract(Convert.ToDateTime(dpFrom.SelectedDate));
                if (span.Days < 0)
                    errMsg = objError.getMessage("SS0002");
            }

            if (errMsg != "")
            {
                lblFilterError.Text = errMsg;
            }
            //else
            //{
                objClientBE = objClientDA.GetClientDetailDA(txtSearch.Text.Trim(), rcmbPkgType.SelectedValue, dt1, dt2);
                tgrdClientList.DataSource = objClientBE;
                tgrdClientList.DataBind();
            //}
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            popClients();
        }

        protected void lbtnBack_Click(object sender, EventArgs e)
        {
            phClientInfo.Visible = false;
            phClientList.Visible = true;
        }

        protected void dpFrom_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
        {
            popClients();
        }

        protected void dpTo_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
        {
            popClients();
        }
    }
}
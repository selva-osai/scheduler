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
    public partial class Theme : System.Web.UI.UserControl
    {
        ClientBE objClientBE = new ClientBE();
        ClientDA objClientDA = new ClientDA();
        EBirdUtility objUtil = new EBirdUtility();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (Session["config_clientID"] != null)
                {
                    hClientID.Value = Session["config_clientID"].ToString();
                    Session.Remove("config_clientID");
                    phClientInfo.Visible = true;
                    phClientList.Visible = false;
                }
                else
                    defaultPopClients();
            }
        }

        private void defaultPopClients()
        {
            List<ClientBE> objClientBE = new List<ClientBE>();
            // objClientBE = objClientDA.GetClientDetailDA(objUtil.FormDBDate(DateTime.Now.AddMonths(-3)), objUtil.FormDBDate(DateTime.Now.AddMonths(3)));
            dpFrom.SelectedDate = DateTime.Now.AddDays(-30);
            dpTo.SelectedDate = DateTime.Now.AddDays(30);
            objClientBE = objClientDA.GetClientDetailDA(objUtil.FormDBDate(DateTime.Now.AddDays(-30)), objUtil.FormDBDate(DateTime.Now.AddDays(30)));
            tgrdClientList.DataSource = objClientBE;
            tgrdClientList.DataBind();
        }

        private void getClientConfigDetail()
        {
            List<ClientBE> objClientBE = objClientDA.GetClientDetailDA(Convert.ToInt32(hClientID.Value));
            if (objClientBE.Count > 0)
            {
                lblClient.Text = objClientBE[0].ClientName;
                lblPkg.Text = objClientBE[0].CurrentPkgSubscribed;

                if (objClientBE[0].ClientStatus == "Inactive")
                    imgStatus.ImageUrl = "~/Images/icons/InactiveStatus1.png";
                else
                    imgStatus.ImageUrl = "~/Images/icons/ActiveStatus1.png";
            }
        }

        protected void tgrdClientList_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.CommandName == "View")
            {
                phClientInfo.Visible = true;
                phClientList.Visible = false;
                hClientID.Value = e.CommandArgument.ToString();
                getClientConfigDetail();
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
            }
        }

        private void clearAll()
        {
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {

        }

        protected void lbtnBack_Click(object sender, EventArgs e)
        {
            clearAll();
            phClientInfo.Visible = false;
            phClientList.Visible = true;
        }

    }
}
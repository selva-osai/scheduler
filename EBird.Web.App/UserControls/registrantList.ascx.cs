using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using EBird.BusinessEntity;
using EBird.Common;
using EBird.DataAccess;
using Telerik.Web.UI;
using System.Web.UI;

namespace EBird.Web.App.UserControls
{
    public partial class registrantList : System.Web.UI.UserControl
    {
        WebinarBE objWebinarBE = new WebinarBE();
        WebinarDA objWebinarDA = new WebinarDA();
        EBirdUtility objUtil = new EBirdUtility();
        EBErrorMessages objError = new EBErrorMessages();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (Session["WebinarID"] != null)
                {
                    hWebinarID.Value = Session["WebinarID"].ToString();
                    hFilterType.Value = Session["RegList"].ToString();

                    List<WebinarBE> objW = objWebinarDA.GetWebinarDetailDA(Convert.ToInt32(hWebinarID.Value));
                    if (objW.Count > 0)
                    {
                        lblWebinarTitle.Text = "<b>Webinar Title</b> - " + objW[0].Title;
                        lblTime.Text = Convert.ToDateTime(objW[0].StartDate).ToString("MMM dd, yyyy") + " - " + Convert.ToDateTime(objW[0].StartTime).ToString("h:mm tt");
                        ltrStatus.Text = objW[0].WebinarStatus;   
                    }
                    //switch (hFilterType.Value.ToUpper())
                    //{
                    //    case "REG":
                    //        lblRegistrantList.Text = "List of Registered Registrants";
                    //        tgrdRegistrantList.DataSource = objWebinarDA.GetWebinarRegistrants(Convert.ToInt32(hWebinarID.Value));   
                    //        break;
                    //    case "LIVE":
                    //        lblRegistrantList.Text = "List of Registrants Attended Live";
                    //        // following is just lace holder and the function call have to be changed for getting the live registrants
                    //        tgrdRegistrantList.DataSource = objWebinarDA.GetWebinarRegistrants(-1);   
                    //        break;
                    //    case "ONDEMAND":
                    //        lblRegistrantList.Text = "List of Registrants Viewed On Demand";
                    //        // following is just lace holder and the function call have to be changed for getting the on demmand registrants
                    //        tgrdRegistrantList.DataSource = objWebinarDA.GetWebinarRegistrants(-1);   
                    //        break;
                    //}
                    //tgrdRegistrantList.DataBind(); 
                    ////popRegistrant(hFilterType.Value);
                    popRegistrant();
                    mvRegistrants.SetActiveView(vwRegistrants); 
                }
                
            }
        }

        private void popRegistrant()
        {
            switch (hFilterType.Value.ToUpper())
            {
                case "REG":
                    lblRegistrantList.Text = "List of Registered Registrants";
                    tgrdRegistrantList.DataSource = objWebinarDA.GetWebinarRegistrants(Convert.ToInt32(hWebinarID.Value));
                    break;
                case "LIVE":
                    lblRegistrantList.Text = "List of Registrants Attended Live";
                    // following is just lace holder and the function call have to be changed for getting the live registrants
                    tgrdRegistrantList.DataSource = objWebinarDA.GetWebinarRegistrants(-1);
                    break;
                case "ONDEMAND":
                    lblRegistrantList.Text = "List of Registrants Viewed On Demand";
                    // following is just lace holder and the function call have to be changed for getting the on demmand registrants
                    tgrdRegistrantList.DataSource = objWebinarDA.GetWebinarRegistrants(-1);
                    break;
            }
            tgrdRegistrantList.DataBind();
        }

        protected void tgrdRegistrantList_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            //switch (hFilterType.Value.ToUpper())
            switch(e.CommandName.ToUpper())
            {
                case "REG":
                    // Set Data Grid Export File Names and Call Export Configuration Function
                    if (e.CommandName == RadGrid.ExportToPdfCommandName)
                    {
                        ConfigureExport();
                        tgrdRegistrantList.ExportSettings.FileName = "RegisteredRegistrants";
                    }
                    if (e.CommandName == RadGrid.ExportToExcelCommandName)
                    {
                        ConfigureExport();
                        tgrdRegistrantList.ExportSettings.FileName = "RegisteredRegistrants";
                    }
                    if (e.CommandName == RadGrid.ExportToWordCommandName)
                    {
                        ConfigureExport();
                        tgrdRegistrantList.ExportSettings.FileName = "RegisteredRegistrants";
                    }
                    if (e.CommandName == RadGrid.ExportToCsvCommandName)
                    {
                        ConfigureExport();
                        tgrdRegistrantList.ExportSettings.FileName = "RegisteredRegistrants";
                    }
                    break;
                case "LIVE":
                    // Set Data Grid Export File Names and Call Export Configuration Function
                    if (e.CommandName == RadGrid.ExportToPdfCommandName)
                    {
                        ConfigureExport();
                        tgrdRegistrantList.ExportSettings.FileName = "RegistrantsAttendedLive";
                    }
                    if (e.CommandName == RadGrid.ExportToExcelCommandName)
                    {
                        ConfigureExport();
                        tgrdRegistrantList.ExportSettings.FileName = "RegistrantsAttendedLive";
                    }
                    if (e.CommandName == RadGrid.ExportToWordCommandName)
                    {
                        ConfigureExport();
                        tgrdRegistrantList.ExportSettings.FileName = "RegistrantsAttendedLive";
                    }
                    if (e.CommandName == RadGrid.ExportToCsvCommandName)
                    {
                        ConfigureExport();
                        tgrdRegistrantList.ExportSettings.FileName = "RegistrantsAttendedLive";
                    }
                    break;
                case "ONDEMAND":
                    // Set Data Grid Export File Names and Call Export Configuration Function
                    if (e.CommandName == RadGrid.ExportToPdfCommandName)
                    {
                        ConfigureExport();
                        tgrdRegistrantList.ExportSettings.FileName = "RegistrantsViewedOnDemand";
                    }
                    if (e.CommandName == RadGrid.ExportToExcelCommandName)
                    {
                        ConfigureExport();
                        tgrdRegistrantList.ExportSettings.FileName = "RegistrantsViewedOnDemand";
                    }
                    if (e.CommandName == RadGrid.ExportToWordCommandName)
                    {
                        ConfigureExport();
                        tgrdRegistrantList.ExportSettings.FileName = "RegistrantsViewedOnDemand";
                    }
                    if (e.CommandName == RadGrid.ExportToCsvCommandName)
                    {
                        ConfigureExport();
                        tgrdRegistrantList.ExportSettings.FileName = "RegistrantsViewedOnDemand";
                    }
                    break;
                case "REBINDGRID":
                    popRegistrant();
                    tgrdRegistrantList.MasterTableView.Rebind();
                    break;
                case "SORT":
                    popRegistrant();
                    tgrdRegistrantList.MasterTableView.Rebind();
                    break;
                case "PAGE":
                    popRegistrant();
                    tgrdRegistrantList.MasterTableView.Rebind();
                    break;
            }

        }

        public void ConfigureExport()
        {
            isExporting = true;
            tgrdRegistrantList.ExportSettings.ExportOnlyData = true;
            tgrdRegistrantList.ExportSettings.IgnorePaging = true;
            tgrdRegistrantList.ExportSettings.OpenInNewWindow = true;

            switch (hFilterType.Value.ToUpper())
            {
                case "REG":
                    tgrdRegistrantList.MasterTableView.Caption = "SnapSession - List of Registered Registrants";
                    break;
                case "LIVE":
                    tgrdRegistrantList.MasterTableView.Caption = "SnapSession - List of Registrants Attended Live";
                    break;
                case "ONDEMAND":
                    tgrdRegistrantList.MasterTableView.Caption = "SnapSession - List of Registrants Viewed On Demand";
                    break;
            }
        }

        //Exporting Var
        bool isExporting = false;

        protected void tgrdRegistrantList_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            string dtValue = "";
            if (e.Item is GridDataItem)
            {
                //Get the instance of the right type
                GridDataItem dataBoundItem = e.Item as GridDataItem;

                Label lbl = (Label)e.Item.FindControl("lblDateTime");
                dtValue = dataBoundItem["RegisteredOn"].Text;
                //lbl.Text = Convert.ToDateTime(dtValue).ToString("F");
                lbl.Text = Convert.ToDateTime(dtValue).ToString("MMM dd, yyyy") + " - " + Convert.ToDateTime(dtValue).ToString("h:mm tt"); 
            }

            if (isExporting)
            {
                if (e.Item.ItemType == GridItemType.AlternatingItem)
                {
                    e.Item.BackColor = System.Drawing.Color.Gainsboro;
                }
                else if (e.Item.ItemType == GridItemType.Header)
                {
                    e.Item.BackColor = System.Drawing.Color.Gainsboro;
                    e.Item.ForeColor = System.Drawing.Color.Firebrick;
                    e.Item.Font.Bold = true;
                    e.Item.Height = Unit.Point(20);
                    e.Item.Font.Size = 11;
                }
                else
                {
                    e.Item.BackColor = System.Drawing.Color.GhostWhite;
                }
            }
        }

        protected void lbtnBack_Click(object sender, EventArgs e)
        {
         //   Session["WebinarID"] = hWebinarID.Value;
            Response.Redirect("~/Pages/Webinar");
        }
    }
}
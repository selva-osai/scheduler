using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using EBird.BusinessEntity;
using EBird.Common;
using EBird.DataAccess;
using Telerik.Web.UI;
using System.Web.UI;
using System.Globalization;

namespace EBird.Web.App.UserControls
{
    public partial class mywebinarlist : System.Web.UI.UserControl
    {
        //WebinarBE objWebinarBE = new WebinarBE();
        WebinarDA objWebinarDA = new WebinarDA();
        EBirdUtility objUtil = new EBirdUtility();
        EBErrorMessages objError = new EBErrorMessages();

        int tabCnt = 1;
        //Exporting Var
        bool isExporting = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                //ci.DateTimeFormat.ShortDatePattern
                
                setmaxMinDates();
                defaultPopWebinars();
                dpFrom.DateInput.DisplayDateFormat = Session["Client_DateFormat"].ToString();
                dpFrom.DateInput.DateFormat = Session["Client_DateFormat"].ToString();
                dpTo.DateInput.DisplayDateFormat = Session["Client_DateFormat"].ToString();
                dpTo.DateInput.DateFormat = Session["Client_DateFormat"].ToString();
                //string shortUsDateFormatString = us.DateTimeFormat.ShortDatePattern; 
                //string shortUsTimeFormatString = us.DateTimeFormat.ShortTimePattern;

            }
        }

        protected void RadAjaxManager1_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            var result = Int32.Parse(e.Argument); // return argument from child 
        }

        private void setmaxMinDates()
        {
            string strDt = objWebinarDA.GetMinMaxWebinarDates(Convert.ToInt32(Session["UserID"]));
            System.Collections.ArrayList arr = objUtil.StringToArrayList(strDt, new char[] { '#' });
            if (arr.Count >= 3)
            {
                hWMinDate.Value = arr[1].ToString();
                hWMaxDate.Value = arr[0].ToString();
                hIsRecycle.Value = arr[2].ToString();
            }
        }

        private void popWebinars(string orderby = "")
        {
            string errMsg = "";
            List<WebinarBE> objWebinarBE = new List<WebinarBE>();
            //WebinarDA objWebinarDA = new WebinarDA();
            string dt1 = "", dt2 = "";
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
            hRegNo.Value = (hRegNo.Value == "" ? "0" : hRegNo.Value);
            hViewNo.Value = (hViewNo.Value == "" ? "0" : hViewNo.Value);
            hDemandNo.Value = (hDemandNo.Value == "" ? "0" : hDemandNo.Value);
            //if (Session["Role"].ToString() == "Admin")
            //if (hSearchType.Value == "S")
            //    objWebinarBE = objWebinarDA.GetMyCompanyWebinarListDA(Convert.ToInt32(Session["ClientID"]), dt1, dt2, txtSearch.Text);
            //else
            //    objWebinarBE = objWebinarDA.GetMyCompanyWebinarListDA(Convert.ToInt32(Session["ClientID"]), dt1, dt2, hSearchText.Value, hSearchField.Value, Convert.ToInt32(hRegNo.Value), Convert.ToInt32(hViewNo.Value), Convert.ToInt32(hDemandNo.Value));
            //else
            if (hSearchType.Value == "S")
                objWebinarBE = objWebinarDA.GetMyWebinarListDA(Convert.ToInt32(Session["UserID"]), dt1, dt2, txtSearch.Text);
            else
                objWebinarBE = objWebinarDA.GetMyWebinarListDA(Convert.ToInt32(Session["UserID"]), dt1, dt2, hSearchText.Value, hSearchField.Value, Convert.ToInt32(hRegNo.Value), Convert.ToInt32(hViewNo.Value), Convert.ToInt32(hDemandNo.Value));

            tgrdWebinarList.DataSource = objWebinarBE;
            tgrdWebinarList.DataBind();
            HideShowAll();
            //if (objWebinarBE.Count == 0)
            //{
            //    LinkButton lbtnShow = FindControlRecursive(this.tgrdWebinarList.MasterTableView, "btnShowAll") as LinkButton;
            //    lbtnShow.Visible = false;
            //}

            
        }

        private Control FindControlRecursive(Control control, string id)
        {
            // Return null if parameter control is null
            if (control == null) return null;

            // Try to find the control at the current level
            Control ctrl = control.FindControl(id);
            if (ctrl == null)
            {
                // Loop through child controls
                foreach (Control child in control.Controls)
                {
                    // Try to find the control at the next level
                    ctrl = FindControlRecursive(child, id);

                    // Stop search when control is found
                    if (ctrl != null) break;
                }
            }
            return ctrl;
        }

        private void defaultPopWebinars()
        {
            List<WebinarBE> objWebinarBE = new List<WebinarBE>();

            //CultureInfo ci = new CultureInfo("en-US");
            //CultureInfo ci = new CultureInfo("de-DE");
            //dpFrom.Culture = ci;

            dpFrom.SelectedDate = DateTime.Now.AddDays(-30);
            dpTo.SelectedDate = DateTime.Now.AddDays(30);

            //string ts = objUtil.FormDBDate(DateTime.Now.AddDays(-30), Session["Client_DateFormat"].ToString());
            //dpFrom.SelectedDate = objUtil.StringToDateTime(ts,"en-US");

            //ts = objUtil.FormDBDate(DateTime.Now.AddDays(30), Session["Client_DateFormat"].ToString());
            //dpTo.SelectedDate = objUtil.StringToDateTime(ts, "en-US");


            string dt1 = "", dt2 = "";
            dt1 = objUtil.FormDBDate(Convert.ToDateTime(dpFrom.SelectedDate));
            dt2 = objUtil.FormDBDate(Convert.ToDateTime(dpTo.SelectedDate));

            if (Session["ADV_SEARCH"] != null)
            {
                //if (Session["Role"].ToString() == "Admin")
                objWebinarBE = objWebinarDA.GetMyWebinarAdvSearchListDA(Convert.ToInt32(Session["UserID"]), dt1, dt2, "", Session["ADV_SEARCH"].ToString());
                //else
                //objWebinarBE = objWebinarDA.GetMyWebinarListDA(Convert.ToInt32(Session["UserID"]), dt1, dt2, "");

                Session.Remove("ADV_SEARCH");
            }
            else
            {
                //if (Session["Role"].ToString() == "Admin")
                //    objWebinarBE = objWebinarDA.GetMyCompanyWebinarListDA(Convert.ToInt32(Session["ClientID"]), dt1, dt2, "");
                //else
                objWebinarBE = objWebinarDA.GetMyWebinarListDA(Convert.ToInt32(Session["UserID"]), dt1, dt2, "");
            }
            tgrdWebinarList.DataSource = objWebinarBE;
            tgrdWebinarList.DataBind();
            HideShowAll();
            //if (objWebinarBE.Count == 0)
            //{
            //    LinkButton lbtnShow = FindControlRecursive(this.tgrdWebinarList.MasterTableView, "btnShowAll") as LinkButton;
            //    lbtnShow.Visible = false;
            //}
        }

        private void HideShowAll()
        {
            if (tgrdWebinarList.Items.Count == 0)
            {
                LinkButton lbtnShow = FindControlRecursive(this.tgrdWebinarList.MasterTableView, "btnShowAll") as LinkButton;
                if (lbtnShow != null)
                    lbtnShow.Visible = false;
                //    this.tgrdWebinarList.MasterTableView.CommandItemSettings.ShowRefreshButton = false;
            }
            //else
            //{
            //    this.tgrdWebinarList.MasterTableView.CommandItemSettings.ShowRefreshButton = true;
            //}
        }

        protected void tgrdWebinarList_ItemCreated(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridCommandItem && hSearchClicked.Value == "0")
            {
                LinkButton lnkbtn = new LinkButton();
                lnkbtn.ID = "btnShowAll";
                lnkbtn.Text = "Show All";
                lnkbtn.CssClass = "lnkBtn1";
                lnkbtn.CommandName = "ShowAll_Command";
                GridCommandItem cmdItem = (GridCommandItem)e.Item; cmdItem.Controls[0].Controls[0].Controls[0].Controls[0].Controls.Add(lnkbtn);

                if (hIsRecycle.Value != "0")
                {
                    Label lblSeparator = new Label();
                    lblSeparator.ForeColor = System.Drawing.Color.Black;
                    lblSeparator.Text = "&nbsp; | &nbsp;";

                    GridCommandItem cmdItemSeparator = (GridCommandItem)e.Item;
                    cmdItemSeparator.Controls[0].Controls[0].Controls[0].Controls[0].Controls.Add(lblSeparator);

                    LinkButton lnkbtnRecycleBin = new LinkButton();
                    lnkbtnRecycleBin.ID = "btnSRecycleBin";
                    //lnkbtnRecycleBin.ForeColor = System.Drawing.Color.Black;
                    lnkbtnRecycleBin.CssClass = "lnkBtn1";
                    lnkbtnRecycleBin.Text = "Recycle Bin";
                    lnkbtnRecycleBin.CommandName = "RecycleBin_Command";
                    GridCommandItem cmdItemRB = (GridCommandItem)e.Item;
                    cmdItemRB.Controls[0].Controls[0].Controls[0].Controls[0].Controls.Add(lnkbtnRecycleBin);
                }
            }

        }

        void ShowAll_Command(Object sender, CommandEventArgs e)
        {

        }

        void RecycleBin_Command(Object sender, CommandEventArgs e)
        {

            // 
        }

        protected void tgrdWebinarList_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "View":
                    Session["WebinarID"] = e.CommandArgument.ToString();
                    Response.Redirect("~/Pages/Schedule");
                    break;
                case "RebindGrid":
                    hSearchType.Value = "S";
                    txtSearch.Text = "";
                    rcmbDateDur.SelectedIndex = 0;
                    hSearchClicked.Value = "0";
                    defaultPopWebinars();
                    tgrdWebinarList.MasterTableView.Rebind();
                    HideShowAll();
                    break;
                case "Sort":
                    popWebinars();
                    tgrdWebinarList.MasterTableView.Rebind();
                    HideShowAll();
                    break;
                case "Page":
                    popWebinars();
                    tgrdWebinarList.MasterTableView.Rebind();
                    break;
                case "Reg":
                    Session["WebinarID"] = e.CommandArgument.ToString();
                    Session["RegList"] = "Reg";
                    Response.Redirect("~/Pages/Registrants");
                    break;
                case "live":
                    Session["WebinarID"] = e.CommandArgument.ToString();
                    Session["RegList"] = "Live";
                    Response.Redirect("~/Pages/Registrants");
                    break;
                case "onDemand":
                    Session["WebinarID"] = e.CommandArgument.ToString();
                    Session["RegList"] = "OnDemand";
                    Response.Redirect("~/Pages/Registrants");
                    break;
                case "ShowAll_Command":
                    txtSearch.Text = "";
                    if (hWMinDate.Value != "")
                        dpFrom.SelectedDate = Convert.ToDateTime(hWMinDate.Value);
                    if (hWMaxDate.Value != "")
                        dpTo.SelectedDate = Convert.ToDateTime(hWMaxDate.Value);
                    popWebinars();
                    break;
                case "RecycleBin_Command":
                    Response.Redirect("~/Pages/Recycle/webinar");
                    break;

            }

            // Set Data Grid Export File Names and Call Export Configuration Function
            if (e.CommandName == RadGrid.ExportToPdfCommandName)
            {
                ConfigureExport();
                tgrdWebinarList.ExportSettings.FileName = "MyWebinars";
            }
            if (e.CommandName == RadGrid.ExportToExcelCommandName)
            {
                ConfigureExport();
                tgrdWebinarList.ExportSettings.FileName = "MyWebinars";
            }
            if (e.CommandName == RadGrid.ExportToWordCommandName)
            {
                ConfigureExport();
                tgrdWebinarList.ExportSettings.FileName = "MyWebinars";
            }
            if (e.CommandName == RadGrid.ExportToCsvCommandName)
            {
                ConfigureExport();
                tgrdWebinarList.ExportSettings.FileName = "MyWebinars";
            }

        }

        public void ConfigureExport()
        {
            isExporting = true;
            tgrdWebinarList.ExportSettings.ExportOnlyData = true;
            tgrdWebinarList.ExportSettings.IgnorePaging = true;
            tgrdWebinarList.ExportSettings.OpenInNewWindow = true;

            tgrdWebinarList.MasterTableView.Caption = "SnapSession - My Webinars";

        }

        protected void tgrdWebinarList_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            string dtValue = "";
            string sTime = "";
            string eTime = "";
            if (e.Item is GridDataItem)
            {
                //Get the instance of the right type
                GridDataItem dataBoundItem = e.Item as GridDataItem;
                string webinarId = dataBoundItem["WebinarID"].Text;
                //Check the formatting condition
                CheckBox chk1 = (CheckBox)dataBoundItem.FindControl("chkInactive");
                chk1.Attributes.Add("data-theme", webinarId);
                if (dataBoundItem["webinarStatus"].Text == "Inactive")
                {
                    chk1.Checked = true;
                    Label lbl1 = (Label)dataBoundItem.FindControl("lblTitle");
                    lbl1.Visible = true;
                    LinkButton lbtn = (LinkButton)dataBoundItem.FindControl("lnkWeb");
                    lbtn.Visible = false;
                }

                Label lbl = (Label)e.Item.FindControl("lblDateTime");
                dtValue = dataBoundItem["startDate"].Text;
                sTime = dataBoundItem["StartTime"].Text;
                eTime = dataBoundItem["EndTime"].Text;
                //lbl.Text = Convert.ToDateTime(dtValue).ToString("MMM dd, yyyy") + "<br>" + Convert.ToDateTime(sTime).ToString("h:mm tt");
                lbl.Text = Convert.ToDateTime(dtValue).ToString((Session["Client_DateFormat"].ToString()).Replace("MM","MMM")) + "<br>" + Convert.ToDateTime(sTime).ToString("h:mm tt");
                
                //Hide Linkbutton(s) if Value = 0; Show Zero text
                LinkButton lbtnReg = (LinkButton)e.Item.FindControl("lnkReg");
                Label lbtnRegLabel = (Label)e.Item.FindControl("lnkRegLabel");
                if (dataBoundItem["registered"].Text == "0")
                {
                    lbtnReg.Visible = false;
                    lbtnRegLabel.Visible = true;
                }

                LinkButton lbtnLive = (LinkButton)e.Item.FindControl("lnkLive");
                Label lbtnLiveLabel = (Label)e.Item.FindControl("lnkLiveLabel");
                if (dataBoundItem["Live"].Text == "0")
                {
                    lbtnLive.Visible = false;
                    lbtnLiveLabel.Visible = true;
                }

                LinkButton lbtnOnDemand = (LinkButton)e.Item.FindControl("lnkOnDemand");
                Label lbtnOnDemandLabel = (Label)e.Item.FindControl("lnkOnDemandLabel");
                if (dataBoundItem["onDemand"].Text == "0")
                {
                    lbtnOnDemand.Visible = false;
                    lbtnOnDemandLabel.Visible = true;
                }

                // Hiding the draft icon, as req changed to webinar becomes active asd soon as saved
                //Image img1 = (Image)e.Item.FindControl("imgDraft");
                //if (dataBoundItem["webinarStatus"].Text != "Active")
                //    img1.Visible = true;

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
        }

        protected void btnAddWebinar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/Schedule");
        }

        protected void lbtnBack_Click(object sender, EventArgs e)
        {
            popWebinars();
        }

        protected void btnPreview_Click(object sender, EventArgs e)
        {
            int ID = 0;
            //objWebinarBE.title = txtTitle.Text;
            //objWebinarBE.description = txtDescription.Text;
            //objWebinarBE.startdate = Convert.ToDateTime(dtpStartdate.SelectedDate);
            //objWebinarBE.starttime = Convert.ToDateTime(dtpStarttime.SelectedDate);
            //objWebinarBE.timeRegion = Convert.ToString(ddTimezone.SelectedItem.Text);
            //objWebinarBE.recurrence = cbRecurrence.Checked;
            //objWebinarBE.clientID = 103;
            //ID = objWebinarDA.addwebinar(objWebinarBE,Session.SessionID);
            //if (ID != 0)
            //    Response.Redirect("mywebinars.aspx?ID=" + ID.ToString());
            //phWebinarInfo.Visible = false;
            //phWebinarList.Visible = true;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            //if (txtSearch.Text != "" || dpFrom.SelectedDate.ToString() != "" || dpTo.SelectedDate.ToString() != "")
            if (txtSearch.Text != "")
            {
                hSearchType.Value = "S";
                hSearchClicked.Value = "1";
                popWebinars();
            }
        }

        protected void btnAdvSearch_Click(object sender, EventArgs e)
        {
            //if (txtSearch.Text != "" || dpFrom.SelectedDate.ToString() != "" || dpTo.SelectedDate.ToString() != "")
            //{
            hSearchType.Value = "A";
            popWebinars();
            //}       
        }

        protected void rcmbDateDur_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (rcmbDateDur.SelectedValue != "")
            {
                dpFrom.Enabled = false;
                dpTo.Enabled = false;

                dpFrom.SelectedDate = DateTime.Now.AddDays(Convert.ToInt16(rcmbDateDur.SelectedValue));
                dpTo.SelectedDate = DateTime.Now;
            }
            else
            {
                dpFrom.Clear();
                dpTo.Clear();
                dpFrom.Enabled = true;
                dpTo.Enabled = true;
            }
        }

        protected string GetWebinarURLsURL(object dataItem)
        {
            string WebcastID = DataBinder.Eval(dataItem, "WebinarID").ToString();
            return string.Format("ViewWebinarURLs.aspx?cmd={0}", Server.UrlEncode(WebcastID));
        }

        protected string GetWebinarURLsClick(object dataItem)
        {
            string WebinarID = DataBinder.Eval(dataItem, "WebinarID").ToString();

            //MyProfile(RadWindow)
            string reportWindowName = "Webinar URLs";
            string clickEventHandlerString = "javascript:var w = window.radopen('ViewWebinarURLs.aspx?cmd=" + WebinarID + "', '" + reportWindowName + "'); w.set_modal(true); w.moveTo(380, 150); w.set_behaviors(Telerik.Web.UI.WindowBehaviors.Move +Telerik.Web.UI.WindowBehaviors.Close); w.setSize(725, 330); return false;";

            //Return OnClick Envent
            return clickEventHandlerString;
        }

        protected void dpFrom_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
        {
            popWebinars();
        }

        protected void dpTo_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
        {
            popWebinars();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using EBird.BusinessEntity;
using EBird.Common;
using EBird.DataAccess;
using Telerik.Web.UI;
using System.Web.UI;
using System.Globalization;

namespace EBird.Web.App.Pages
{
    public partial class recycle : System.Web.UI.Page
    {
        WebinarDA objWebinarDA = new WebinarDA();
        UserDA objUserDA = new UserDA();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                string pgType = (string)RouteData.Values["type"];
                if (pgType == "webinar")
                {
                    phWebinar.Visible = true;
                    popDelWebinars();
                }
                else
                {
                    phUserMgmt.Visible = true;
                    popDelUsers();
                }
                if (Session["Role"].ToString().ToUpper() == "SSADMIN")
                {
                    lblrecycle.Text = "Administrator Recycle Bin";
                    lbtnBack1.Text = "Administrator Management";
                    tgrdUserList.MasterTableView.Columns[3].Visible = false;
                }
                else
                {
                    lblrecycle.Text = "User Management Recycle Bin";
                    lbtnBack1.Text = "User Management";
                }
            }
        }

        private void popDelWebinars()
        {
            List<WebinarBE> objWebinarBE = new List<WebinarBE>();
            //if (Session["Role"].ToString() == "Admin")
            //    objWebinarBE = objWebinarDA.GetMyCompanyRecycleWebinarListDA(Convert.ToInt32(Session["ClientID"]));
            //else
            objWebinarBE = objWebinarDA.GetMyRecycleWebinarListDA(Convert.ToInt32(Session["UserID"]));
            tgrdWebinarList.DataSource = objWebinarBE;
            tgrdWebinarList.DataBind();
            if (objWebinarBE.Count > 0)
                btnRestore.Enabled = true;
            else
                btnRestore.Enabled = false;
        }

        private void popDelUsers()
        {
            List<UserBE> objUserList = objUserDA.GetDeletedUserListDA(Convert.ToInt32(Session["ClientID"]));
            tgrdUserList.DataSource = objUserList;
            tgrdUserList.DataBind();
            if (objUserList.Count > 0)
                btnUserRestore.Enabled = true;
            else
                btnUserRestore.Enabled = false;
        }

        protected void tgrdWebinarList_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "RebindGrid":
                    popDelWebinars();
                    tgrdWebinarList.MasterTableView.Rebind();
                    break;
                case "Sort":
                    popDelWebinars();
                    tgrdWebinarList.MasterTableView.Rebind();
                    break;
                case "Page":
                    popDelWebinars();
                    tgrdWebinarList.MasterTableView.Rebind();
                    break;
            }
        }

        protected void tgrdWebinarList_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            string dtValue = "";
            string sTime = "";
            if (e.Item is GridDataItem)
            {
                GridDataItem dataBoundItem = e.Item as GridDataItem;

                Label lbl = (Label)e.Item.FindControl("lblDateTime");
                dtValue = dataBoundItem["startDate"].Text;
                sTime = dataBoundItem["StartTime"].Text;
                //eTime = dataBoundItem["EndTime"].Text;
                lbl.Text = Convert.ToDateTime(dtValue).ToString("MMM dd, yyyy") + " - " + Convert.ToDateTime(sTime).ToString("h:mm tt");

                lbl = (Label)e.Item.FindControl("lblDelDateTime");
                dtValue = dataBoundItem["modifiedOn"].Text;
                lbl.Text = Convert.ToDateTime(dtValue).ToString("MMM dd, yyyy");
            }
        }

        protected void tgrdUserList_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                //Get the instance of the right type
                GridDataItem dataBoundItem = e.Item as GridDataItem;

                //Check the formatting condition
                Literal ltr = (Literal)dataBoundItem.FindControl("ltrUserName");
                if (dataBoundItem["Role"].Text == "Admin")
                    ltr.Text = dataBoundItem["FirstName"].Text + " " + dataBoundItem["LastName"].Text + " " + "<img src='/images/icons/Admin_16.gif' alt='Adminitrator'>";
                else
                    ltr.Text = dataBoundItem["FirstName"].Text + " " + dataBoundItem["LastName"].Text;
            }
        }

        protected void tgrdUserList_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "RebindGrid":
                    popDelUsers();
                    tgrdUserList.MasterTableView.Rebind();
                    break;
                case "Sort":
                    popDelUsers();
                    tgrdUserList.MasterTableView.Rebind();
                    break;
                case "Page":
                    popDelUsers();
                    tgrdUserList.MasterTableView.Rebind();
                    break;
            }

        }

        protected void lbtnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/Webinar");
        }

        protected void btnRestore_Click(object sender, EventArgs e)
        {
            HiddenField hFld;
            CheckBox chk1;
            bool isChecked = false;
            foreach (GridDataItem item in tgrdWebinarList.Items)
            {
                chk1 = (CheckBox)item.FindControl("chkRestore");
                if (chk1.Checked)
                {
                    hFld = (HiddenField)item.FindControl("hWebinarID");
                    objWebinarDA.UpdateWebinarStatus(Convert.ToInt32(hFld.Value), "Active", Convert.ToInt32(Session["UserID"]));
                    isChecked = true;
                }
            }
            if (isChecked)
                popDelWebinars();
        }

        protected void btnCanCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/Webinar");
        }

        protected void lbtnBack1_Click(object sender, EventArgs e)
        {
            if (Session["Role"].ToString() == "SSAdmin")
                Response.Redirect("~/Pages/adminmgmt");
            else
                Response.Redirect("~/Pages/usermgmt");
        }

        protected void btnUserRestore_Click(object sender, EventArgs e)
        {
            HiddenField hFld, hFld1;
            CheckBox chk1;
            string strUserList = string.Empty;
            string strLockUser = string.Empty;
            int usrCount = 0;
            lblUserError.Text = "";
            foreach (GridDataItem item in tgrdUserList.Items)
            {
                chk1 = (CheckBox)item.FindControl("chkRestore");
                if (chk1.Checked)
                {
                    hFld = (HiddenField)item.FindControl("hUserID");
                    hFld1 = (HiddenField)item.FindControl("hUserStatus");
                    if (hFld1.Value == "Deleted")
                        strUserList = strUserList + hFld.Value + ",";
                    else
                        strLockUser = strLockUser + hFld.Value + ",";
                    usrCount++;
                }
            }

            if (strUserList != string.Empty || strLockUser != string.Empty)
            {
                bool isUpdate = false;
                if (Convert.ToInt32(Session["ClientID"]) != 0)
                {
                    // 0 - NoOfAccount,  1 - NoOfUsers as NoOfSeats
                    int[] usrCount1 = objUserDA.GetClientUserCountDA(Convert.ToInt32(Session["ClientID"]));
                    int reqSeats = usrCount1[0] + usrCount;
                    if (usrCount1[1] - reqSeats < 0)
                    {
                        phInstruct.Visible = true;
                        btnUserRestore.Enabled = false;
                        //btnUserRestore.Text = "Increase Seat & Restore";

                        int stVal = 0;
                        stVal = reqSeats - usrCount1[1];
                        stVal = stVal + (5 - stVal % 5);
                        rcmbSeats.Items.Clear();
                        for (int idx = 1; idx < 6; idx++)
                        {
                            rcmbSeats.Items.Add(new RadComboBoxItem(stVal.ToString(), stVal.ToString()));
                            stVal += 5;
                        }
                    }
                    else
                        isUpdate = true;
                }
                else
                    isUpdate = true;
                if (isUpdate)
                {
                    phInstruct.Visible = false;
                    objUserDA.restoreUserAcct(strUserList + "0", true);
                    objUserDA.restoreUserAcct(strLockUser + "0", false);
                    popDelUsers();
                }
            }
            else
                lblUserError.Text = "No user selected to restore";
        }

        protected void btnUserCancel_Click(object sender, EventArgs e)
        {
            if (Session["Role"].ToString() == "SSAdmin")
                Response.Redirect("~/Pages/adminmgmt");
            else
                Response.Redirect("~/Pages/usermgmt");

        }

        protected void chkSeats_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSeats.Checked)
            {
                btnUserRestore.Enabled = true;
            }
            else
                btnUserRestore.Enabled = false;
        }
    }
}
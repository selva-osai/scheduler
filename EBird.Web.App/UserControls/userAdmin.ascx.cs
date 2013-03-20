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
using EBird.Email;

namespace EBird.Web.App.UserControls
{
    public partial class userAdmin : System.Web.UI.UserControl
    {
        UserDA objUserDA = new UserDA();
        EBErrorMessages objError = new EBErrorMessages();
        //Exporting Var
        bool isExporting = false;
        //string rndPasswd = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (Session["Role"].ToString().ToUpper() == "SSADMIN")
                {
                    ltrBack.Visible = false;
                    tgrdUserList.MasterTableView.Columns[2].Visible = false;
                    tgrdUserList.MasterTableView.Columns[0].ItemStyle.Width = Unit.Percentage(38);
                    tgrdUserList.MasterTableView.Columns[1].ItemStyle.Width = Unit.Percentage(40);
                    //tgrdUserList.MasterTableView.Columns[2].ItemStyle.Width = Unit.Percentage(15);
                    tgrdUserList.MasterTableView.Columns[3].ItemStyle.Width = Unit.Percentage(12);
                    tgrdUserList.MasterTableView.Columns[4].ItemStyle.Width = Unit.Percentage(10);
                }
                if (Session["Role"].ToString().ToUpper() == "USER")
                    Response.Redirect("~/Pages/AccessDenied");
                else
                {

                    if (objUserDA.isDeletedUserExistDA(Convert.ToInt32(Session["ClientID"])))
                        hIsRecycle.Value = "1";
                    else
                        hIsRecycle.Value = "0";
                    popUsers();
                }
                //else
                //{
                //    lblMsg1.Text = "";
                //    btnAddUser.Visible = true;
                //}
                if (Session["Role"].ToString() == "SSAdmin")
                    lbtnBack.Text = "Administrator Management";
                else
                    lbtnBack.Text = "User Management";
            }

        }

        private void popUsers(bool isShowAll = false)
        {
            List<UserBE> objUserBE = new List<UserBE>();
            if (Session["ADV_USR"] != null)
            {
                hSearchType.Value = "A";

                hSearchText.Value = Session["ADV_USR_TXT"].ToString();
                hEmailContain.Value = Session["ADV_USR_EML"].ToString();
                hSearchRole.Value = Session["ADV_USR_ROLE"].ToString();

                Session.Remove("ADV_USR");
                Session.Remove("ADV_USR_TXT");
                Session.Remove("ADV_USR_EML");
                Session.Remove("ADV_USR_ROLE");

            }
            if (hSearchType.Value == "S")
            {
                if (isShowAll)
                    objUserBE = objUserDA.GetAllUserListDA(Convert.ToInt32(Session["ClientID"]), txtSearch.Text.Trim());
                else
                    objUserBE = objUserDA.GetUserListDA(Convert.ToInt32(Session["ClientID"]), txtSearch.Text.Trim(), rcmbStatus.SelectedValue);
            }
            else
            {
                objUserBE = objUserDA.GetUserListDA(Convert.ToInt32(Session["ClientID"]), hSearchText.Value.Trim(), hEmailContain.Value, hSearchRole.Value);
            }
            tgrdUserList.DataSource = objUserBE;
            tgrdUserList.DataBind();
            HideRecycle();
            HideShowAll();

        }

        private void HideRecycle()
        {
            if (hIsRecycle.Value == "0")
            {
                LinkButton lbtnRecycle = FindControlRecursive(this.tgrdUserList.MasterTableView, "btnSRecycleBin") as LinkButton;
                if (lbtnRecycle != null)
                    lbtnRecycle.Visible = false;
                Label lblSep = FindControlRecursive(this.tgrdUserList.MasterTableView, "lblSep") as Label;
                if (lblSep != null)
                    lblSep.Visible = false;
                //    this.tgrdWebinarList.MasterTableView.CommandItemSettings.ShowRefreshButton = false;
            }
            //else
            //{
            //    this.tgrdWebinarList.MasterTableView.CommandItemSettings.ShowRefreshButton = true;
            //}
        }

        private void HideShowAll()
        {
            if (tgrdUserList.Items.Count == 0)
            {
                LinkButton lbtnShow = FindControlRecursive(this.tgrdUserList.MasterTableView, "btnShowAll") as LinkButton;
                if (lbtnShow != null)
                    lbtnShow.Visible = false;
                //    this.tgrdWebinarList.MasterTableView.CommandItemSettings.ShowRefreshButton = false;
            }
            //else
            //{
            //    this.tgrdWebinarList.MasterTableView.CommandItemSettings.ShowRefreshButton = true;
            //}
        }

        protected void tgrdUserList_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "editUser":
                    clearAll();
                    editUserRec(e.CommandArgument.ToString());
                    ltrBack.Visible = false;
                    lbtnBack.Visible = true;
                    lbtnBack_label.Visible = true;
                    if (Session["Role"].ToString().ToUpper() == "SSADMIN")
                    {
                        ltrBack.Visible = false;
                        lbtnBack.Visible = false;
                        lbtnBack_label.Visible = true;
                        lbtnBack_label.Text = "Return to <a href='adminmgmt' class='lnkBtn1'>Administrator Management</a>";
                        phAdmin.Visible = false;
                    }
                    break;
                case "delUser":
                    clearAll();
                    phRecAddEdit.Visible = true;
                    phRecList.Visible = false;
                    hUserID.Value = e.CommandArgument.ToString();
                    hAction.Value = "D";
                    if (Session["Role"].ToString().ToUpper() == "ADMIN")
                        lblCaption.Text = "User Management Delete";
                    else
                        lblCaption.Text = "Administrator Delete";
                    btnSave.Text = "Delete";
                    getUserDetail(Convert.ToInt32(e.CommandArgument), true);
                    btnSave.CausesValidation = false;
                    ltrBack.Visible = false;
                    lbtnBack.Visible = true;
                    lbtnBack_label.Visible = true;
                    // Disable or Enable
                    txtUserFName.Enabled = false;
                    txtUserLName.Enabled = false;
                    txtUserPhone.Enabled = false;
                    txtUserEmail.Enabled = false;
                    txtUserDept.Enabled = false;
                    chkAdmin.Enabled = false;
                    // Required fields
                    Requiredfield_PlaceHolder.Visible = false;
                    txtUserDept_PlaceHolder.Visible = false;
                    txtUserEmail_PlaceHolder.Visible = false;
                    txtUserPhone_PlaceHolder.Visible = false;
                    txtUserLName_PlaceHolder.Visible = false;
                    txtUserFName_PlaceHolder.Visible = false;
                    if (Session["Role"].ToString().ToUpper() == "SSADMIN")
                    {
                        ltrBack.Visible = false;
                        lbtnBack.Visible = false;
                        lbtnBack_label.Visible = true;
                        lbtnBack_label.Text = "Return to <a href='adminmgmt' class='lnkBtn1'>Administrator Management</a>";
                        phAdmin.Visible = false;
                    }
                    break;
                case "RebindGrid":
                    hSearchType.Value = "S";
                    txtSearch.Text = "";
                    rcmbStatus.SelectedIndex = 0;
                    hSearchClicked.Value = "0";
                    popUsers();
                    tgrdUserList.MasterTableView.Rebind();
                    HideShowAll();
                    HideRecycle();
                    break;
                case "Sort":
                    popUsers();
                    tgrdUserList.MasterTableView.Rebind();
                    HideShowAll();
                    HideRecycle();
                    break;
                case "Page":
                    popUsers();
                    tgrdUserList.MasterTableView.Rebind();
                    HideShowAll();
                    HideRecycle();
                    break;
                case "RecycleBin_Command":
                    if (Session["Role"].ToString() == "SSAdmin")
                        Response.Redirect("~/Pages/Recycle/adminmgmt");
                    else
                        Response.Redirect("~/Pages/Recycle/usermgmt");
                    break;
                case "ShowAll_Command":
                    hSearchType.Value = "S";
                    popUsers(true);
                    break;
            }

            // Set Data Grid Export File Names and Call Export Configuration Function
            if (e.CommandName == RadGrid.ExportToPdfCommandName)
            {
                ConfigureExport();
                tgrdUserList.ExportSettings.FileName = "UserManagement";
            }
            if (e.CommandName == RadGrid.ExportToExcelCommandName)
            {
                ConfigureExport();
                tgrdUserList.ExportSettings.FileName = "UserManagement";
            }
            if (e.CommandName == RadGrid.ExportToWordCommandName)
            {
                ConfigureExport();
                tgrdUserList.ExportSettings.FileName = "UserManagement";
            }
            if (e.CommandName == RadGrid.ExportToCsvCommandName)
            {
                ConfigureExport();
                tgrdUserList.ExportSettings.FileName = "UserManagement";
            }

        }

        public void editUserRec(string usrID)
        {
            clearAll();
            phRecAddEdit.Visible = true;
            phRecList.Visible = false;
            hUserID.Value = usrID;
            hAction.Value = "E";
            if (Session["Role"].ToString().ToUpper() == "ADMIN")
                lblCaption.Text = "User Management Edit";
            else
                lblCaption.Text = "Administrator Edit";
            btnSave.Text = "Edit";
            getUserDetail(Convert.ToInt32(usrID));

            ltrBack.Visible = false;
            lbtnBack.Visible = true;
            lbtnBack_label.Visible = true;
            // Disable or Enable
            txtUserFName.Enabled = true;
            txtUserLName.Enabled = true;
            txtUserPhone.Enabled = true;
            txtUserEmail.Enabled = true;
            txtUserDept.Enabled = true;
            chkAdmin.Enabled = true;
            // Required fields
            Requiredfield_PlaceHolder.Visible = true;
            txtUserDept_PlaceHolder.Visible = true;
            txtUserEmail_PlaceHolder.Visible = true;
            txtUserPhone_PlaceHolder.Visible = true;
            txtUserLName_PlaceHolder.Visible = true;
            txtUserFName_PlaceHolder.Visible = true;
        }

        public void ConfigureExport()
        {
            isExporting = true;
            tgrdUserList.ExportSettings.ExportOnlyData = true;
            tgrdUserList.ExportSettings.IgnorePaging = true;
            tgrdUserList.ExportSettings.OpenInNewWindow = true;

            tgrdUserList.MasterTableView.Caption = "SnapSession - User Management";

        }

        private void getUserDetail(int userID, bool isDel = false)
        {
            List<UserBE> objUserBE = new List<UserBE>();
            objUserBE = objUserDA.GetUserDetailDA(userID);
            if (objUserBE.Count > 0)
            {
                dvMsg.Visible = false;
                btnSave.Visible = true;

                txtUserFName.Text = objUserBE[0].FirstName;
                txtUserLName.Text = objUserBE[0].LastName;
                txtUserDept.Text = objUserBE[0].Department;
                txtUserEmail.Text = objUserBE[0].EmailID;
                hCurrEmail.Value = objUserBE[0].EmailID;
                txtUserPhone.Text = objUserBE[0].Telephone;

                if (objUserBE[0].Role == "SSAdmin")
                {
                    phAdmin.Visible = false;
                }
                else
                {
                    //Set Default
                    lblCheckAdmin.Text = "User is an Administrator";

                    if (objUserBE[0].Role == "Admin")
                    {
                        chkAdmin.Checked = true;
                        if (objUserBE[0].isPrimary)
                        {
                            chkAdmin.Enabled = false;
                            lblCheckAdmin.Text = "User is Primary Administrator";
                            lblPrimary.Visible = true;
                        }
                        else
                        {
                            chkAdmin.Enabled = true;
                            lblCheckAdmin.Text = "User is an Administrator";
                            lblPrimary.Visible = false;
                        }
                    }
                    else
                        chkAdmin.Checked = false;
                    hUserStatus.Value = objUserBE[0].UserStatus;

                    if (isDel && !phInstruct.Visible)
                    {
                        if (Session["UserID"].ToString() == userID.ToString())
                        {
                            phInstruct.Visible = true;
                            phAlternate.Visible = false;
                            lblInstruct.Text = objError.getMessage("UA009");
                            btnSave.Enabled = false;
                        }
                        //else if (objUserBE[0].UserStatus != "Active")
                        //{
                        //    phInstruct.Visible = true;
                        //    lblInstruct.Text = objError.getMessage("UA011");
                        //    btnSave.Enabled = false;
                        //}
                        else if (objUserBE[0].isPrimary)
                            alternateAdminSetting();
                    }
                }
            }
        }

        private void alternateAdminSetting()
        {
            phAlternate.Visible = true;
            List<UserBE> objUsrList = objUserDA.GetUserListDA(Convert.ToInt32(Session["ClientID"]), "Admin", Convert.ToInt32(hUserID.Value));
            if (objUsrList.Count > 0)
            {
                lblAlternateAdmin.Text = "";
                rcmbAdmin.Visible = true;
                rcmbAdmin.Items.Clear();
                rcmbAdmin.DataTextField = "FullName";
                rcmbAdmin.DataValueField = "UserID";
                rcmbAdmin.DataSource = objUsrList;
                rcmbAdmin.DataBind();
                rcmbAdmin.Text = "Primary Amin";
            }
            else
            {
                rcmbAdmin.Visible = false;
                lblAlternateAdmin.Text = "- No other administrator available to assign Primary Administrator role";
                btnSave.Enabled = false;
            }
        }

        protected void tgrdUserList_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                //Get the instance of the right type
                GridDataItem dataBoundItem = e.Item as GridDataItem;
                UserBE objUBE = (UserBE)(e.Item.DataItem);

                //Check the formatting condition
                Label ltr = (Label)dataBoundItem.FindControl("ltrUserName");

                if (objUBE.Role == "Admin")
                    ltr.Text = objUBE.FirstName + " " + objUBE.LastName + " " + "<img src='/images/icons/Admin_16.gif' alt='Adminitrator'>";
                else
                    ltr.Text = objUBE.FirstName + " " + objUBE.LastName;

                CheckBox chk = (CheckBox)dataBoundItem.FindControl("chkLock");
                ImageButton ibtn = (ImageButton)dataBoundItem.FindControl("btnDelete");

                if (objUBE.Role == "SSAdmin")
                {
                    chk.Visible = false;
                    Label lblL = (Label)dataBoundItem.FindControl("lblNoLock");
                    if (lblL != null)
                        lblL.Text = "-";
                    ibtn.Visible = false;
                }

                if (objUBE.UserStatus == "Inactive")
                {
                    chk.Checked = true;
                }
                else if (objUBE.UserStatus == "Deleted")
                {
                    ibtn.Visible = false;
                    ibtn = (ImageButton)dataBoundItem.FindControl("btnEdit");
                    ibtn.Visible = false;
                    ltr.ForeColor = System.Drawing.Color.Red;
                    chk.Enabled = false;
                }
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

        protected void tgrdUserList_ItemCreated(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridCommandItem && hSearchClicked.Value == "0")
            {
                //LinkButton lnkbtn = new LinkButton();
                //lnkbtn.ID = "btnShowAll";
                //lnkbtn.Text = "Show All";
                //lnkbtn.CssClass = "lnkBtn1";
                //lnkbtn.CommandName = "ShowAll_Command";
                //GridCommandItem cmdItem = (GridCommandItem)e.Item; cmdItem.Controls[0].Controls[0].Controls[0].Controls[0].Controls.Add(lnkbtn);

                if (hIsRecycle.Value != "0")
                {
                    //Label lblSeparator = new Label();
                    //lblSeparator.ForeColor = System.Drawing.Color.Black;
                    //lblSeparator.Text = "&nbsp; | &nbsp;";
                    //lblSeparator.ID = "lblSep";

                    //GridCommandItem cmdItemSeparator = (GridCommandItem)e.Item;
                    //cmdItemSeparator.Controls[0].Controls[0].Controls[0].Controls[0].Controls.Add(lblSeparator);

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

        void RecycleBin_Command(Object sender, CommandEventArgs e)
        {

        }

        protected void btnFLSearch_Click(object sender, EventArgs e)
        {
            //if (txtSearch.Text != "")
            //{
            hSearchType.Value = "S";
            hSearchClicked.Value = "1";
            popUsers();
            //} 
        }

        protected void btnAdvSearch_Click(object sender, EventArgs e)
        {
            hSearchType.Value = "A";
            popUsers();
        }

        protected void btnAddUser_Click(object sender, EventArgs e)
        {
            dvMsg.Visible = false;
            btnSave.Visible = true;
            lblCaption.Text = "User Management Add";
            btnSave.Text = "Add";
            //Set Default
            lblCheckAdmin.Text = "User is an Administrator";
            phRecAddEdit.Visible = true;
            phRecList.Visible = false;
            ltrBack.Visible = false;
            lbtnBack.Visible = true;
            lbtnBack_label.Visible = true;
            // Disable or Enable
            txtUserFName.Enabled = true;
            txtUserLName.Enabled = true;
            txtUserPhone.Enabled = true;
            txtUserEmail.Enabled = true;
            txtUserDept.Enabled = true;
            chkAdmin.Enabled = true;
            // Required fields
            Requiredfield_PlaceHolder.Visible = true;
            txtUserDept_PlaceHolder.Visible = true;
            txtUserEmail_PlaceHolder.Visible = true;
            txtUserPhone_PlaceHolder.Visible = true;
            txtUserLName_PlaceHolder.Visible = true;
            txtUserFName_PlaceHolder.Visible = true;
            clearAll();
            if (Convert.ToInt32(Session["ClientID"]) != 0)
            {
                int[] usrCount = objUserDA.GetClientUserCountDA(Convert.ToInt32(Session["ClientID"]));
                //int[] Cid = objUserDA.GetClientUserCountDA(Convert.ToInt32(Session["ClientID"]));

                lblCheckAdmin.Visible = true;
                chkAdmin.Visible = true;
                if (usrCount[1] - usrCount[0] <= 0)
                {
                    setAdditionalSeatOption("AM0001");
                }
                else
                {
                    phInstruct.Visible = false;
                    rcmbAdmin.Items.Clear();
                }

            }
            else
            {
                lblCaption.Text = "Administrator Add";
                lbtnBack.Visible = false;
                lbtnBack_label.Text = "Return to <a href='adminmgmt' class='lnkBtn1'>Administrator Management</a>";
                if (Session["Role"].ToString() == "SSAdmin")
                {
                    phAdmin.Visible = false;
                }
            }
        }

        private void setAdditionalSeatOption(string strMsg)
        {
            phInstruct.Visible = true;
            phAlternate.Visible = false;
            lblInstruct.Text = objError.getMessage(strMsg);
            rcmbAdmin.Visible = true;
            //rcmbAdmin.Width = 45;
            chkSeats.Visible = true;
            rcmbAdmin.Items.Clear();
            rcmbAdmin.Items.Add(new RadComboBoxItem("5", "5"));
            rcmbAdmin.Items.Add(new RadComboBoxItem("10", "10"));
            rcmbAdmin.Items.Add(new RadComboBoxItem("15", "15"));
            rcmbAdmin.Items.Add(new RadComboBoxItem("20", "20"));
            rcmbAdmin.Items.Add(new RadComboBoxItem("25", "25"));
            btnSave.Enabled = false;
            chkSeats.Checked = false;
        }

        private void clearAll()
        {
            phAlternate.Visible = false;
            phInstruct.Visible = false;
            btnSave.Enabled = true;
            lblInstruct.Text = "";
            hUserStatus.Value = "Active";
            lblPrimary.Visible = false;
            txtUserFName.Text = "";
            txtUserLName.Text = "";
            txtUserDept.Text = "";
            txtUserEmail.Text = "";
            hCurrEmail.Value = "";
            txtUserPhone.Text = "";
            hUserID.Value = "0";
            hAction.Value = "A";
            chkAdmin.Checked = false;
            chkAdmin.Enabled = true;
            btnSave.Text = "Add";
            btnSave.CausesValidation = true;
        }

        protected void lbtnAdvSearch_Click(object sender, EventArgs e)
        {

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            //phRecAddEdit.Visible = false;
            //phRecList.Visible = true;
            popUsers();
            lbtnBack_Click(null, null);

            if (Convert.ToInt32(Session["ClientID"]) != 0)
            {
                ltrBack.Visible = true;
            }
        }

        private void saveUserRec()
        {
            UserBE objUsr = new UserBE();
            objUsr.EmailID = txtUserEmail.Text;
            objUsr.FirstName = txtUserFName.Text;
            objUsr.LastName = txtUserLName.Text;
            objUsr.Address = "";
            objUsr.Telephone = txtUserPhone.Text.Trim();
            objUsr.Role = (chkAdmin.Checked ? "Admin" : "User");
            objUsr.ClientID = Convert.ToInt32(Session["ClientID"]);
            objUsr.Department = txtUserDept.Text;
            objUsr.UserStatus = hUserStatus.Value;
            objUsr.isPrimary = false;
            objUsr.CreatedBy = Convert.ToInt32(Session["UserID"]);
            objUsr.UserID = Convert.ToInt32(hUserID.Value);
            if (objUsr.ClientID == 0)
            {
                objUsr.Role = "AEAdmin";
            }
            if (hAction.Value == "A")
            {
                string rndPasswd = RandomPassword.Generate();
                objUsr.Password = EBirdUtility.Encrypt(rndPasswd);
                if (objUserDA.AddUserAccountDA(objUsr) > 0)
                {
                    popUsers();
                    // Emailing the account details to user
                    int reqID = SaveToEmailJob(txtUserEmail.Text, rndPasswd);
                    if (reqID > 0)
                    {
                        EmailApp objEmailing = new EmailApp();
                        objEmailing.SendEmail(reqID);
                    }
                    lbtnBack_Click(null, null);
                }
                else
                {
                    dvMsg.Visible = true;
                    lblmsg.Text = objError.getMessage("AM0002");
                }
            }
            if (hAction.Value == "E")
            {
                int PrimaryAdmin = objUsr.UserID;

                if (lblPrimary.Visible)
                {
                    if (chkAdmin.Checked)
                        objUsr.isPrimary = true;
                    else
                        PrimaryAdmin = Convert.ToInt32(rcmbAdmin.SelectedValue);
                }
                if (objUserDA.UpdateUserRecord(objUsr, PrimaryAdmin))
                {
                    popUsers();
                    lbtnBack_Click(null, null);
                }
                else
                {
                    dvMsg.Visible = true;
                    lblmsg.Text = objError.getMessage("AM0012");
                }
            }
        }

        private void IncreaseSeats()
        {
            if (chkSeats.Visible && chkSeats.Checked)
            {
                objUserDA.updateUserSeats(Convert.ToInt32(Session["ClientID"]), Convert.ToInt32(rcmbSeats.SelectedValue), true, Convert.ToInt32(Session["UserID"]));
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            List<UserBE> objUserBE = new List<UserBE>();
            //lblPrimary.Visible = false;
            dvMsg.Visible = false;
            switch (hAction.Value)
            {
                case "A":
                    IncreaseSeats();
                    saveUserRec();
                    break;
                case "E":
                    saveUserRec();
                    break;
                case "D":
                    //objUserBE = objUserDA.GetUserDetailDA(Convert.ToInt32(hUserID.Value));
                    //if (objUserBE.Count > 0)
                    //{
                    if (lblPrimary.Visible && rcmbAdmin.Items.Count == 0)
                    {
                        dvMsg.Visible = true;
                        lblmsg.Text = objError.getMessage("AM0003");
                    }
                    else
                    {
                        int priAdminID = -1;
                        if (lblPrimary.Visible)
                            priAdminID = Convert.ToInt32(rcmbAdmin.SelectedValue);

                        objUserDA.DeleteUserAccountDA(Convert.ToInt32(hUserID.Value), priAdminID);
                        hIsRecycle.Value = "1";
                        popUsers();
                        //phRecAddEdit.Visible = false;
                        //phRecList.Visible = true;
                        lbtnBack_Click(null, null);
                    }
                    //}
                    break;
                case "U":
                    saveUserRec();
                    popUsers();
                    //phRecAddEdit.Visible = false;
                    //phRecList.Visible = true;
                    lbtnBack_Click(null, null);
                    break;
            }

        }

        protected void lbtnBack_Click(object sender, EventArgs e)
        {
            phRecList.Visible = true;
            phRecAddEdit.Visible = false;
            //if (Session["Role"].ToString().ToUpper() != "SSADMIN")
            ltrBack.Visible = false;
            //else
            //    ltrBack.Visible = true;
            lbtnBack.Visible = false;
            lbtnBack_label.Visible = false;
        }

        // MiG 11/29/2012
        public void UpdateLockedStatus(object sender, System.EventArgs e)
        {
            try
            {
                int[] usrCount = objUserDA.GetClientUserCountDA(Convert.ToInt32(Session["ClientID"]));
                //Loop Through Grid
                foreach (GridDataItem item in tgrdUserList.Items)
                {
                    //Set Current Status
                    string CheckboxStatus = "Active";
                    //Get User
                    string strtest = (item["EmailID"].Text);
                    // Get Checkbox Value
                    CheckBox LockCheckBox = (CheckBox)item.FindControl("chkLock");
                    if ((LockCheckBox.Checked))
                        CheckboxStatus = "Inactive";

                    // Check if existing value has changed; if so, update database
                    List<UserBE> objUserBE = new List<UserBE>();
                    objUserBE = objUserDA.GetUserDetailDA(strtest);
                    if (objUserBE.Count > 0)
                    {
                        if (objUserBE[0].UserStatus != CheckboxStatus)
                        {
                            objUserDA.UpdateUserLockedFieldDA(strtest, CheckboxStatus);
                            ClientDA ObjClientDA = new ClientDA();

                            AuditLogBE objAuditlog = new AuditLogBE();
                            objAuditlog.ActionByID = Convert.ToInt32(Session["UserID"]);
                            objAuditlog.ActionID = Convert.ToInt32(AuditActions.Client_inactive_vs_Active);
                            objAuditlog.ActionDetail = "Updated user account " + strtest + " status to " + CheckboxStatus;
                            objAuditlog.ClientID = Convert.ToInt32(Session["ClientID"]);
                            ObjClientDA.SaveAuditRecord(objAuditlog);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        protected void chkSeats_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSeats.Checked)
            {
                btnSave.Enabled = true;
                rcmbSeats.Visible = true;
            }
            else
                btnSave.Enabled = false;
        }

        private int SaveToEmailJob(string emailID, string genPasswd)
        {
            EmailBE objEmailBE = new EmailBE();
            EmailDA objEmailDA = new EmailDA();
            EmailApp objEmailing = new EmailApp();

            string emlContent = objEmailing.getHTMLFormattedNewAccountNotify(emailID, genPasswd, DateTime.Now.ToString());
            objEmailBE.isToEmailRef = false;
            objEmailBE.RequestStatus = "No-delay";
            objEmailBE.RequestType = "New User Account";
            objEmailBE.Subject = "Welcome to SnapSession!";
            objEmailBE.SubmittedBy = Convert.ToInt32(Session["UserID"]);
            objEmailBE.ToEmail = emailID;
            objEmailBE.FromEmail = "support@ebird.com";
            objEmailBE.EmailContent = emlContent;
            return objEmailDA.SaveEmailRequest(objEmailBE);
        }

        protected void chkAdmin_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkAdmin.Checked)
            {
                if (lblPrimary.Visible)
                    alternateAdminSetting();
                else
                {
                    phAlternate.Visible = false;
                    if (objUserDA.getAdminCount(Convert.ToInt32(Session["ClientID"])) < 2)
                    {
                        lblAlternateAdmin.Text = "There should be at least one administrator";
                        btnSave.Enabled = false;
                    }
                }
            }
            //else
            //{

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

    }
}
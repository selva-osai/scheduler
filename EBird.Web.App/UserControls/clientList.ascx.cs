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
using EBird.DocRepo;
using EBird.Email;

namespace EBird.Web.App.UserControls
{
    public partial class clientList : System.Web.UI.UserControl
    {
        ClientBE objClientBE = new ClientBE();
        ClientDA objClientDA = new ClientDA();
        EBirdUtility objUtil = new EBirdUtility();
        EBErrorMessages objError = new EBErrorMessages();
        UserDA objUserDA = new UserDA();

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
                    dvValidationMsg.Visible = false;
                    getClientDetail(Convert.ToInt32(hClientID.Value));
                }
                else
                    defaultPopClients();
                //popClients();
            }
        }

        private void popClients()
        {
            string errMsg = "";

            List<ClientBE> objClientBE = new List<ClientBE>();
            //ClientDA objClientDA = new ClientDA();
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
                //tgrdClientList.DataSource = null;
                //tgrdClientList.DataBind();
            }
            //else
            //{
            objClientBE = objClientDA.GetClientDetailDA(txtSearch.Text.Trim(), rcmbPkgType.SelectedValue, dt1, dt2);
            tgrdClientList.DataSource = objClientBE;
            tgrdClientList.DataBind();
            //}
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
                    getClientDetail(Convert.ToInt32(e.CommandArgument));
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
                    lbtn.CssClass = "lnkInactive";
                    //lbtn.ForeColor = System.Drawing.Color.Red;
                    lbtn.Attributes.Remove(".RadGrid");
                }

                Label lbl = (Label)e.Item.FindControl("lblCreatedOn");
                dtValue = dataBoundItem["CreatedOn"].Text;
                lbl.Text = Convert.ToDateTime(dtValue).ToString("MMM dd, yyyy");

                //lbl = (Label)e.Item.FindControl("lblWebinarCount");
                //dtValue = dataBoundItem["clientID"].Text;
                //lbl.Text = objClientDA.getWebinarCount(Convert.ToInt32(dtValue)).ToString();

            }
        }

        private void getClientDetail(int clientID)
        {
            string modifiedOn = "";
            
            List<ClientBE> objClientBE = new List<ClientBE>();
            objClientBE = objClientDA.GetClientDetailDA(clientID);

            if (objClientBE.Count > 0)
            {
                rcmbStatus.Enabled = true;
                hClientID.Value = clientID.ToString();
                txtClientName.Text = objClientBE[0].ClientName;
                txtAddress.Text = objClientBE[0].Address1;
                txtCity.Text = objClientBE[0].City;
                txtNoUsers.Text = objClientBE[0].NoOfUsers.ToString();
                txtPhone.Text = objClientBE[0].Phone;
                txtPostcode.Text = objClientBE[0].PostCode;
                txtState.Text = objClientBE[0].State;
                txtWebsite.Text = objClientBE[0].Website;
                rcmbCountry.SelectedValue = objClientBE[0].CountryID.ToString();
                rcmbIndustry.SelectedValue = objClientBE[0].IndustryID.ToString();
                rcmbRevenue.SelectedValue = objClientBE[0].AnnualRevID.ToString();
                if (chkPackage.Items.Count > 2)
                {
                    chkPackage.Items.RemoveAt(2);
                }
                if (objClientBE[0].CurrentPkgSubscribed == "Custom")
                {
                    chkPackage.Items.Add(new ListItem("Custom", "Custom"));
                }
                chkPackage.SelectedValue = objClientBE[0].CurrentPkgSubscribed;
                rcmbStatus.SelectedValue = objClientBE[0].ClientStatus;
                hCurrStatus.Value = objClientBE[0].ClientStatus;
                modifiedOn = objClientBE[0].LastModified;
                hCurrPkg.Value = objClientBE[0].CurrentPkgSubscribed;

                //dvStep.Visible = true;

                int[] usrCount = objUserDA.GetClientUserCountDA(clientID);
                if (usrCount[0].ToString() == "1")
                    lblActiveUserCnt.Text = " (" + usrCount[0].ToString() + " Active User)";
                else
                    lblActiveUserCnt.Text = " (" + usrCount[0].ToString() + " Active Users)";
                hActiveUserCnt.Value = usrCount[0].ToString();
            }

            List<ContactBE> objContactBE = new List<ContactBE>();
            objContactBE = objClientDA.GetClientContactDA(clientID);
            if (objContactBE.Count > 0)
            {
                hContactID.Value = objContactBE[0].ContactID.ToString();
                txtContactName.Text = objContactBE[0].Contactname;
                txtContactEmail.Text = objContactBE[0].Email;
                txtContactPhone.Text = objContactBE[0].Phone;
                txtContactDepart.Text = objContactBE[0].Department;
                txtJobTitle.Text = objContactBE[0].JobTitle;
            }

            List<UserBE> objUserBE = new List<UserBE>();
            objUserBE = objUserDA.GetPrimaryAdminDA(clientID);
            if (objUserBE.Count > 0)
            {
                hAdminID.Value = objUserBE[0].UserID.ToString();
                txtAdminFName.Text = objUserBE[0].FirstName;
                txtAdminLName.Text = objUserBE[0].LastName;
                txtAdminEmail.Text = objUserBE[0].EmailID;
                txtAdminPhone.Text = objUserBE[0].Telephone;
                txtAdminDept.Text = objUserBE[0].Department;
            }

            if (hCurrStatus.Value == "Inactive")
            {
                dvAlert.Visible = true;
                System.TimeSpan diffResult = DateTime.Now - Convert.ToDateTime(modifiedOn);
                string[] arrValues = new string[1];
                arrValues[0] = Convert.ToString(diffResult.Days);
                lblInactiveMsg.Text = objError.getMessage("SS0001", arrValues);
                btnSave.Visible = false;
                btnActivate.Visible = true;
                //rcmbStatus.Visible = false;
                PlaceHolderReactivate.Visible = true;
                PlaceHolderActive.Visible = false;
            }
            else
            {
                dvAlert.Visible = false;
                btnSave.Visible = true;
                //btnActivate.Visible = false;
                //rcmbStatus.Visible = true;
                PlaceHolderReactivate.Visible = false;
                PlaceHolderActive.Visible = true;
            }
        }

        protected void btnAddClient_Click(object sender, EventArgs e)
        {
            phClientInfo.Visible = true;
            phClientList.Visible = false;
            clearAll();
            rcmbStatus.Enabled = false;
            rcmbStatus.SelectedIndex = 0;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

            int ID = Convert.ToInt32(hClientID.Value);
            if (ID != 0 && (hCurrStatus.Value != rcmbStatus.SelectedValue))
            {
                objClientDA.UpdateClientStatusDA(ID, rcmbStatus.SelectedValue, Convert.ToInt32(Session["UserID"]));
                popClients();
                lbtnBack_Click(null, null);
            }
            else
            {
                // Business rule check
                if ((ID != 0) && (Convert.ToInt32(txtNoUsers.Text) < Convert.ToInt32(hActiveUserCnt.Value)))
                {
                    dvValidationMsg.Visible = true;
                    lblValidationMsg.Text = objError.getMessage("SS0003");
                }
                else if (ID != 0)
                {
                    if (objUserDA.isUserExistDA(txtAdminEmail.Text.Trim(), Convert.ToInt32(hAdminID.Value)))
                    {
                        dvValidationMsg.Visible = true;
                        lblValidationMsg.Text = objError.getMessage("AM0012");
                    }
                }
                else if (ID == 0)
                {
                    if (objUserDA.isUserExistDA(txtAdminEmail.Text.Trim()))
                    {
                        dvValidationMsg.Visible = true;
                        lblValidationMsg.Text = objError.getMessage("AM0002");
                    }
                }

                if (dvValidationMsg.Visible == false)
                {
                    #region add/update client profile
                    
                    objClientBE.ClientID = ID;
                    objClientBE.ClientName = txtClientName.Text;
                    objClientBE.Address1 = txtAddress.Text;
                    objClientBE.City = txtCity.Text;
                    objClientBE.State = txtState.Text;
                    objClientBE.CountryID = Convert.ToInt32(rcmbCountry.SelectedValue);
                    objClientBE.PostCode = txtPostcode.Text;
                    objClientBE.Phone = txtPhone.Text;
                    objClientBE.Website = txtWebsite.Text;
                    objClientBE.IndustryID = Convert.ToInt32(rcmbIndustry.SelectedValue);
                    objClientBE.AnnualRevID = Convert.ToInt32(rcmbRevenue.SelectedValue);
                    objClientBE.NoOfUsers = Convert.ToInt32(txtNoUsers.Text);
                    objClientBE.CurrentPkgSubscribed = chkPackage.SelectedValue;
                    objClientBE.ClientStatus = rcmbStatus.SelectedValue; // "Active"; 
                    objClientBE.CreatedBy = Convert.ToInt32(Session["UserID"]);
                    ID = objClientDA.SaveClientProfileDA(objClientBE);
                    #endregion

                    #region Creating required folder structure for client
                    if (hClientID.Value == "0" && ID != 0)
                    {
                        DocAccess objDocAccess = new DocAccess();
                        if (!objDocAccess.InitClientFolders(ID))
                            lblFilterError.Text = objError.getMessage("AM0010");
                    }
                    #endregion

                    #region Updating primary contact and administrator
                    if (ID != 0)
                    {
                        lblClientError.Text = "";
                        //Saving Contact
                        ContactBE objContact = new ContactBE();
                        objContact.ClientID = ID;
                        objContact.ContactID = Convert.ToInt32(hContactID.Value);
                        objContact.Contactname = txtContactName.Text;
                        objContact.Phone = txtContactPhone.Text;
                        objContact.Email = txtContactEmail.Text;
                        objContact.Department = txtContactDepart.Text;
                        objContact.JobTitle = txtJobTitle.Text;
                        objClientDA.SaveClientContactDA(objContact);

                        //Saving Primary administrator
                        
                        UserBE objUserBE = new UserBE();

                        objUserBE.UserID = Convert.ToInt32(hAdminID.Value);
                        objUserBE.EmailID = txtAdminEmail.Text;
                        objUserBE.FirstName = txtAdminFName.Text;
                        objUserBE.LastName = txtAdminLName.Text;
                        objUserBE.Address = "";
                        objUserBE.Telephone = txtAdminPhone.Text;
                        objUserBE.Role = "Admin";
                        objUserBE.ClientID = ID;
                        objUserBE.Department = txtAdminDept.Text;
                        objUserBE.UserStatus = "Active";
                        objUserBE.isPrimary = true;
                        objUserBE.CreatedBy = Convert.ToInt32(Session["UserID"]);

                        if (Convert.ToInt32(hAdminID.Value) != 0)
                        {
                            if (!objUserDA.UpdateUserRecord(objUserBE, objUserBE.UserID))
                                lblClientError.Text = objError.getMessage("AM0012");
                        }
                        else
                        {
                            string rndPasswd = RandomPassword.Generate();
                            objUserBE.Password = rndPasswd;

                            if (objUserDA.AddUserAccountDA(objUserBE) > 0)
                            {
                                // Emailing the account details to user
                                int reqID = SaveToEmailJob(txtAdminEmail.Text, rndPasswd);
                                if (reqID > 0)
                                {
                                    EmailApp objEmailing = new EmailApp();
                                    objEmailing.SendEmail(reqID);
                                }
                            }
                        }
                        // Following commented on 2/20/13 - J, as these logics ar moved inside the sp for add/edit client profile
                        //if (hClientID.Value == "0")
                        //    objClientDA.InitClientConfigDA(ID, chkPackage.SelectedValue, false);
                        //else if (hCurrPkg.Value != chkPackage.SelectedValue)
                        //    objClientDA.InitClientConfigDA(ID, chkPackage.SelectedValue, true);

                        //Session["clientID"] = ID.ToString();
                        //Response.Redirect("ClientConfig/");
                        popClients();
                        if (lblClientError.Text == "")
                            lbtnBack_Click(null, null);
                    }
                    #endregion
                    if (lblClientError.Text == "")
                        clearAll();
                }
            }
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

        private void clearAll()
        {
            lblActiveUserCnt.Text = "";
            txtClientName.Text = "";
            txtAddress.Text = "";
            txtCity.Text = "";
            txtState.Text = "";
            rcmbCountry.SelectedValue = "223";
            txtPostcode.Text = "";
            txtPhone.Text = "";
            txtWebsite.Text = "";
            rcmbIndustry.ClearSelection();
            rcmbRevenue.ClearSelection();
            txtNoUsers.Text = "";

            hAdminID.Value = "0";
            hClientID.Value = "0";
            hContactID.Value = "0";

            txtContactName.Text = "";
            txtContactEmail.Text = "";
            txtContactPhone.Text = "";
            txtContactDepart.Text = "";
            txtJobTitle.Text = "";

            txtAdminFName.Text = "";
            txtAdminLName.Text = "";
            txtAdminEmail.Text = "";
            txtAdminPhone.Text = "";
            txtAdminDept.Text = "";

            //dvStep.Visible = false;
            dvValidationMsg.Visible = false;
            dvAlert.Visible = false;
            btnSave.Visible = true;
            //btnActivate.Visible = false;
            //rcmbStatus.Visible = true;
            PlaceHolderReactivate.Visible = false;
            PlaceHolderActive.Visible = true;

            //if custom checkbox is there than remove it
            if (chkPackage.Items.Count > 2)
            {
                chkPackage.Items.RemoveAt(2);
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            popClients();
        }

        protected void lbtnBack_Click(object sender, EventArgs e)
        {
            clearAll();
            phClientInfo.Visible = false;
            phClientList.Visible = true;
            //Session["clientID"] = hClientID.Value;
            //Response.Redirect("ClientConfig/");
        }

        protected void btnActivate_Click(object sender, EventArgs e)
        {
            int ID = Convert.ToInt32(hClientID.Value);
            //if (ID != 0 && (hCurrStatus.Value != rcmbStatus.SelectedValue))
            if (ID != 0)
            {
                rcmbStatus.SelectedValue = "Active";
                objClientDA.UpdateClientStatusDA(ID, rcmbStatus.SelectedValue, Convert.ToInt32(Session["UserID"]));
                //btnActivate.Visible = false;
                PlaceHolderReactivate.Visible = false;
                PlaceHolderActive.Visible = true;
                btnSave.Visible = true;
                dvAlert.Visible = false;
                popClients();
            }
        }

        protected void lnkConfig_Click(object sender, EventArgs e)
        {
            Session["config_clientID"] = hClientID.Value;
            Response.Redirect("~/Pages/ClientConfig/");
        }

        protected void lnkTheme_Click(object sender, EventArgs e)
        {
            Session["config_clientID"] = hClientID.Value;
            Response.Redirect("~/Pages/Themes/");
        }

        protected void lnkSubscription_Click(object sender, EventArgs e)
        {
            Session["config_clientID"] = hClientID.Value;
            Response.Redirect("~/Pages/Subscription/");
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
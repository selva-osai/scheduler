using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EBird.DataAccess;
using EBird.BusinessEntity;
using EBird.Common;
using Telerik.Web.UI;
using System.Globalization;

namespace EBird.Web.App.UserControls
{
    public partial class clientConfigList : System.Web.UI.UserControl
    {
        ClientBE objClientBE = new ClientBE();
        ClientDA objClientDA = new ClientDA();
        EBirdUtility objUtil = new EBirdUtility();
        List<CheckBox> chklist = new List<CheckBox>();
        EBErrorMessages objError = new EBErrorMessages();

        int clientID;

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
                    getClientConfigDetail();
                }
                else
                    defaultPopClients();
            }
        }

        private void defaultPopClients()
        {
            List<ClientBE> objClientBE = new List<ClientBE>();
            dpFrom.SelectedDate = DateTime.Now.AddDays(-365);
            dpTo.SelectedDate = DateTime.Now.AddDays(365);
            objClientBE = objClientDA.GetClientDetailDA(objUtil.FormDBDate(DateTime.Now.AddDays(-365)), objUtil.FormDBDate(DateTime.Now.AddDays(365)));
            tgrdClientList.DataSource = objClientBE;
            tgrdClientList.DataBind();
        }

        private void popPkgdropdown(string selPkg)
        {
            rcmbPkgConfig.Items.Clear();
            if (selPkg == "Custom")
            {
                rcmbPkgConfig.Items.Add(new RadComboBoxItem("Enterprise", "Enterprise"));
                rcmbPkgConfig.Items.Add(new RadComboBoxItem("Professional", "Professional"));
                rcmbPkgConfig.Items.Add(new RadComboBoxItem("Custom", "Custom"));
            }
            else
            {
                rcmbPkgConfig.Items.Add(new RadComboBoxItem("Enterprise", "Enterprise"));
                rcmbPkgConfig.Items.Add(new RadComboBoxItem("Professional", "Professional"));
            }
            rcmbPkgConfig.SelectedValue = selPkg;
        }

        private void getClientConfigDetail()
        {
            if (hClientID.Value != "")
            {
                dvValidationMsg.Visible = false;
                List<ClientBE> objClientBE = objClientDA.GetClientDetailDA(Convert.ToInt32(hClientID.Value));
                if (objClientBE.Count > 0)
                {
                    lblClient.Text = objClientBE[0].ClientName;
                    lblPkg.Text = objClientBE[0].CurrentPkgSubscribed;
                    //rcmbPkgConfig.SelectedValue = objClientBE[0].CurrentPkgSubscribed;
                    popPkgdropdown(objClientBE[0].CurrentPkgSubscribed);
                    rcmbDateFormat.SelectedValue = objClientBE[0].DateFormat;
                    rcmbLanguage.SelectedValue = Convert.ToString(objClientBE[0].LanguageID);
                    rcmbTimeZone.SelectedValue = Convert.ToString(objClientBE[0].TimeZoneID);
                    chkDaylight.Checked = objClientBE[0].isAutoDLSave;
                    
                    if (objClientBE[0].CurrentPkgSubscribed == "Enterprise")
                        chkConfigCC.Checked = true;

                    if (objClientBE[0].ClientStatus == "Inactive")
                    {
                        imgStatus.ImageUrl = "~/Images/icons/InactiveStatus1.png";
                        btnSave.Visible = false;
                        lblMsg.Text = objError.getMessage("AM0008");
                    }
                    else
                    {
                        btnSave.Visible = true;
                        imgStatus.ImageUrl = "~/Images/icons/ActiveStatus1.png";
                        //lblMsg.Text = objError.getMessage("AM0007");
                        lblMsg.Text = "";
                    }
                }

                //setCheckBoxState();

                setAssociateFeatureCheck();
            }
        }

        //---------------------------------------------------------------------------
        //Set the check box checked for all the features associated to the Client ID
        //---------------------------------------------------------------------------
        private void setAssociateFeatureCheck()
        {
            bool isCCChecked = false;
            List<ConfigParameterBE> objConfigParameterBE = objClientDA.GetClientConfigIDsDA(Convert.ToInt32(hClientID.Value));
            hCustFeatureList.Value = "";
            for (int idx = 0; idx < objConfigParameterBE.Count; idx++)
            {
                CheckBox chk = (CheckBox)FindControl("chkConfig" + Convert.ToString(objConfigParameterBE[idx].ConfigID));
                if (chk != null)
                {
                    chk.Checked = true;
                    hCustFeatureList.Value += objConfigParameterBE[idx].ConfigID + ",";
                    if (objConfigParameterBE[idx].ConfigID >=27 && objConfigParameterBE[idx].ConfigID <=31)
                        isCCChecked = true;
                }
            }

            chkConfigCC.Checked = isCCChecked;
        }

        //---------------------------------------------------------------------------
        // Enable all box for selection, if package is custome
        // Else disable them all
        //---------------------------------------------------------------------------
        private void setCheckBoxState()
        {
            bool chkEnable = false;
            if (rcmbPkgConfig.SelectedValue == "Custom")
                chkEnable = true;

            List<ConfigParameterBE> objConfigParameterBE = objClientDA.GetConfigMasterDA();
            for (int idx = 0; idx < objConfigParameterBE.Count; idx++)
            {
                CheckBox chk = (CheckBox)FindControl("chkConfig" + Convert.ToString(objConfigParameterBE[idx].ConfigID));
                if (chk != null)
                {
                    chk.Enabled = chkEnable;
                    //chk.Checked = false;
                }
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
            else
            {
                objClientBE = objClientDA.GetClientDetailDA(txtSearch.Text.Trim(), rcmbPkgType.SelectedValue, dt1, dt2);
                tgrdClientList.DataSource = objClientBE;
                tgrdClientList.DataBind();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            popClients();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                objClientBE.ClientID = Convert.ToInt32(hClientID.Value);
                objClientBE.TimeZoneID = Convert.ToInt32(rcmbTimeZone.SelectedValue);
                objClientBE.LanguageID = Convert.ToInt32(rcmbLanguage.SelectedValue);
                objClientBE.DateFormat = rcmbDateFormat.SelectedValue;
                objClientBE.isAutoDLSave = chkDaylight.Checked;
                objClientDA.SaveDefaultClientConfigDA(objClientBE,Convert.ToInt32(Session["UserID"]));

                string selFeatureIDs = getCheckedFeatured();
                string selPkg = "Custom";

                if (selFeatureIDs == "")
                {
                    lblMsg.Text = "No feature selected";
                }
                else
                {
                    if (selFeatureIDs == Constant.ENT_PKG)
                        selPkg = "Enterprise";
                    else if (selFeatureIDs == Constant.PRO_PKG)
                        selPkg = "Professional";

                    //if (selPkg != "Custom" && lblPkg.Text != selPkg)
                    if (selPkg != "Custom")
                    {
                        objClientDA.UpdateClientPackageFeatureDA(Convert.ToInt32(hClientID.Value), selPkg, Convert.ToInt32(Session["UserID"]));
                        getClientConfigDetail();
                    }
                    else
                    {
                        objClientDA.UpdateClientPackageFeaturesDA(Convert.ToInt32(hClientID.Value), selFeatureIDs, Convert.ToInt32(Session["UserID"]));
                    }
                    Response.Redirect("~/Pages/ClientConfig");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private void clearAll()
        {
            foreach (CheckBox chk in chklist)
            {
                chk.Checked = false;
            }
        }

        protected void lbtnBack_Click(object sender, EventArgs e)
        {
            clearAll();
            phClientInfo.Visible = false;
            phClientList.Visible = true;
        }

        protected void lbtnUpdatePkg_Click(object sender, EventArgs e)
        {

            //lbtnUpdatePkg.Visible = false;

        }

        protected void tgrdClientList_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "View":
                    phClientInfo.Visible = true;
                    phClientList.Visible = false;
                    hClientID.Value = e.CommandArgument.ToString();
                    getClientConfigDetail();
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

        protected void rcmbPkgConfig_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            switch (rcmbPkgConfig.SelectedValue)
            {
                case "Custom":
                    featureChecked(hCustFeatureList.Value + "0");
                    //setAssociateFeatureCheck();
                    //chkConfigCC.Checked = false;
                    break;
                case "Enterprise":
                    featureChecked(Constant.ENT_PKG);
                    chkConfigCC.Checked = true;
                    break;
                case "Professional":
                    featureChecked(Constant.PRO_PKG);
                    chkConfigCC.Checked = true;
                    //chkConfigCC.Checked = false;
                    break;
            }
        }

        private void featureChecked(string featureVal)
        {
            for (int idx = 4; idx < 40; idx++)
            {
                CheckBox chk = (CheckBox)FindControl("chkConfig" + idx.ToString());
                if (chk != null)
                {
                    chk.Checked = false;
                }
            }

            bool isCCChecked = false;
            System.Collections.ArrayList arr = objUtil.StringToArrayList(featureVal, new char[] { ',' });
            for (int idx = 0; idx < arr.Count; idx++)
            {
                CheckBox chk = (CheckBox)FindControl("chkConfig" + arr[idx].ToString());
                if (chk != null)
                {
                    chk.Checked = true;
                    if (Convert.ToInt32(arr[idx]) >= 27 && Convert.ToInt32(arr[idx]) <= 31)
                        isCCChecked = true;
                }
            }
            chkConfigCC.Checked = isCCChecked;
        }

        private string getCheckedFeatured()
        {
            string rtn = string.Empty;
            StringBuilder sb = new StringBuilder();

            for (int idx = 4; idx < 40; idx++)
            {
                CheckBox chk = (CheckBox)FindControl("chkConfig" + idx.ToString());
                if (chk != null)
                {
                    if (chk.Checked)
                        sb.Append(idx.ToString() + ",");
                }
            }
            if (sb.Length > 0)
                return sb.ToString().Substring(0, sb.ToString().Length - 1);
            else
                return "";
        }


        protected void lnkProfile_Click(object sender, EventArgs e)
        {
            Session["config_clientID"] = hClientID.Value;
            Response.Redirect("~/Pages/Client/");
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

        private void popCulture()
        {
            //CultureInfo ci = new CultureInfo();
            foreach (CultureInfo ci in CultureInfo.GetCultures(CultureTypes.InstalledWin32Cultures))
            {
                ddCulture.Items.Add(new RadComboBoxItem(ci.DisplayName));  
            }
        }
    }
}
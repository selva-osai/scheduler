<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="clientList.ascx.cs"
    Inherits="EBird.Web.App.UserControls.clientList" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<link rel="stylesheet" href="/styles/newstyle1.css" />
<style type="text/css">
    .RadComboBoxDropDown_Default .rcbHovered
    {
        background: #EBEBEB;
        color: #C80000;
    }
    .RadCalendar_Default .rcMain .rcRow .rcHover, .RadCalendar_Default .rcRow .rcSelected
    {
        background: #EBEBEB 0 -1700px repeat-x url(Calendar/sprite.gif);
        border-color: #EBEBEB;
    }
    .RadCalendar_Default .rcMain .rcRow .rcHover a, .RadCalendar_Default .rcMain .rcRow .rcSelected a
    {
        color: #C80000;
    }
    .RadCalendarMonthView_Default .rcSelected a, .RadCalendarTimeView_Default td.rcHover a, .RadCalendarTimeView_Default td.rcSelected a
    {
        background: #EBEBEB 0 -1700px repeat-x url(Calendar/sprite.gif);
        color: #C80000;
        border-color: #EBEBEB;
    }
</style>
<asp:ValidationSummary ID="xevs_userAdmin" CssClass="ValidationSummary" HeaderText=""
    runat="server" ShowSummary="False" ShowMessageBox="True" />
<div class="RPart">
    <div class="TopBg">
    </div>
    <div class="widgets">
        <asp:PlaceHolder ID="phClientList" runat="server" Visible="true">
            <%--             <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
                <AjaxSettings>
                    <telerik:AjaxSetting AjaxControlID="tgrdClientList">
                        <UpdatedControls>
                            <telerik:AjaxUpdatedControl ControlID="tgrdClientList" />
                        </UpdatedControls>
                    </telerik:AjaxSetting>
                </AjaxSettings>
            </telerik:RadAjaxManager>--%>
            <div class="Steps1">
                <table border="0" width="100%">
                    <tr>
                        <td align="center">
                            <asp:TextBox ID="txtSearch" Columns="40" MaxLength="40" runat="server" Height="20"
                                Width="270" />
                            <asp:TextBoxWatermarkExtender ID="txtWatermark" runat="server" TargetControlID="txtSearch"
                                WatermarkText="Client Name" WatermarkCssClass="watermarked_EBSearch" />
                        </td>
                        <td>
                            <telerik:RadComboBox ID="rcmbPkgType" runat="server" CssClass="cmbPkg" MarkFirstMatch="True"
                                HighlightTemplatedItems="True" ExpandAnimation-Duration="1000" CollapseAnimation-Duration="1000"
                                CollapseAnimation-Type="OutQuart" NoWrap="True" EnableTheming="True" Font-Italic="False"
                                Skin="Default">
                                <Items>
                                    <telerik:RadComboBoxItem Text="ALL" Value="All" Selected="True"></telerik:RadComboBoxItem>
                                    <telerik:RadComboBoxItem Text="Enterprise" Value="Enterprise"></telerik:RadComboBoxItem>
                                    <telerik:RadComboBoxItem Text="Professional" Value="Professional"></telerik:RadComboBoxItem>
                                    <telerik:RadComboBoxItem Text="Custom" Value="Custom"></telerik:RadComboBoxItem>
                                </Items>
                            </telerik:RadComboBox>
                        </td>
                        <td>
                            <asp:Button ID="btnSearch" CssClass="SubBtn" runat="server" Text="Search" OnClick="btnSearch_Click" />
                        </td>
                        <td align="right" width="50%">
                            <asp:Button ID="btnAddClient" runat="server" CssClass="SubBtn" Text="Add Client"
                                OnClick="btnAddClient_Click" />
                            <!--  &nbsp;<asp:HyperLink runat="server" ID="AdvancedSearch" Text="Advanced Search" NavigateUrl="~/AdministrationSearch.aspx" ForeColor="#1589FF"></asp:HyperLink> -->
                        </td>
                    </tr>
                </table>
            </div>
            <div class="Steps1">
                <table border="0" width="100%">
                    <tr>
                        <td colspan="2" valign="middle" height="21">
                            Filter From&nbsp;<telerik:RadDatePicker ID="dpFrom" Width="120px" runat="server"
                                AutoPostBack="true" OnSelectedDateChanged="dpFrom_SelectedDateChanged" HideAnimation-Duration="800"
                                ShowAnimation-Duration="800">
                            </telerik:RadDatePicker>
                            To&nbsp;<telerik:RadDatePicker ID="dpTo" Width="120px" runat="server" AutoPostBack="true"
                                OnSelectedDateChanged="dpTo_SelectedDateChanged" HideAnimation-Duration="800"
                                ShowAnimation-Duration="800">
                            </telerik:RadDatePicker>
                        </td>
                        <td colspan="2">
                            <asp:Label ID="lblFilterError" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
            <table width="100%" align="center" cellpadding="10" style="min-height: 350px;">
                <tr>
                    <td valign="top">
                        <telerik:RadGrid ID="tgrdClientList" runat="server" GridLines="None" AllowPaging="True"
                            PageSize="15" MasterTableView-EditFormSettings-EditFormType="Template" AllowSorting="True"
                            AutoGenerateColumns="False" ValidationSettings-EnableValidation="false" OnItemCommand="tgrdClientList_ItemCommand"
                            OnItemDataBound="tgrdClientList_ItemDataBound" MasterTableView-NoDetailRecordsText="No active clients available"
                            BorderColor="#D7D7D7">
                            <PagerStyle Mode="NumericPages" PagerTextFormat="Navigate pages {4} Page {0} of {1}, Clients {2} to {3} of {5}" />
                            <ExportSettings HideStructureColumns="true" />
                            <MasterTableView CommandItemDisplay="Top" AutoGenerateColumns="false" AllowFilteringByColumn="False"
                                ShowFooter="false" TableLayout="Auto" Width="100%">
                                <CommandItemSettings ShowAddNewRecordButton="false" ShowExportToWordButton="true"
                                    ShowRefreshButton="true" ShowExportToExcelButton="true" ShowExportToCsvButton="true"
                                    ShowExportToPdfButton="true" />
                                <NoRecordsTemplate>
                                    <center>
                                        <br />
                                        No clients to display<br />
                                        &nbsp;</center>
                                </NoRecordsTemplate>
                                <RowIndicatorColumn>
                                    <HeaderStyle Width="20px" />
                                </RowIndicatorColumn>
                                <ExpandCollapseColumn>
                                    <HeaderStyle Width="20px" />
                                </ExpandCollapseColumn>
                                <Columns>
                                    <telerik:GridBoundColumn DataField="clientID" HeaderText="Client ID">
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn HeaderText="Client Name" UniqueName="ClientName" SortExpression="ClientName"
                                        DataField="ClientName">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkView1" CssClass="lnkBtn1" CommandName="View" Text='<%# Eval("ClientName") %>'
                                                runat="server" CommandArgument='<%# Eval("clientID") %>' CausesValidation="false"></asp:LinkButton>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn DataField="CurrentPkgSubscribed" HeaderText="Platform Edition">
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="NoOfUsers" HeaderText="Seats">
                                        <ItemStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="NoOfWebinars" HeaderText="Webinars" UniqueName="Webinars"
                                        SortExpression="NoOfWebinars">
                                        <ItemStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn HeaderText="Client As Of" UniqueName="ClientAsOf" SortExpression="CreatedOn">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCreatedOn" runat="server"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn DataField="CreatedOn" HeaderText="Client As Of" Visible="false">
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn Visible="false" DataField="clientStatus">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn HeaderText="">
                                        <ItemTemplate>
                                            <img src="~/Images/icons/icoAnalytics.gif" alt="Analytics" runat="server" id="imgAnal" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </telerik:GridTemplateColumn>
                                </Columns>
                            </MasterTableView>
                            <ClientSettings>
                                <Scrolling AllowScroll="false" />
                                <Selecting AllowRowSelect="true" />
                                <ClientEvents OnRowMouseOver="RowMouseOver" OnRowMouseOut="RowMouseOut" />
                            </ClientSettings>
                        </telerik:RadGrid>
                    </td>
                </tr>
            </table>
        </asp:PlaceHolder>
        <asp:PlaceHolder ID="phClientInfo" runat="server" Visible="false">
            <table width="97%" align="center">
                <tr>
                    <td colspan="2">
                        <div class="ui-widget" runat="server" id="dvValidationMsg" visible="false">
                            <div class="ui-state-highlight ui-corner-all" style="margin-top: 20px; padding: 0 .7em;
                                height: 21px; margin-bottom: 10px;">
                                <p>
                                    <%--<span class="ui-icon ui-icon-info" style="float: left; margin-right: .3em;"></span>--%>
                                    <strong>Warning!</strong>&nbsp;&nbsp;<asp:Label ID="lblValidationMsg" runat="server"></asp:Label></p>
                            </div>
                        </div>
                        <div class="ui-widget" runat="server" id="dvAlert" visible="false">
                            <div class="ui-state-error ui-corner-all" style="padding: 0 .7em; height: 21px; margin-bottom: 10px;">
                                <p>
                                    <%--<span class="ui-icon ui-icon-alert" style="float: left; margin-right: .3em;"></span>--%>
                                    <strong>Alert:</strong>
                                    <asp:Label ID="lblInactiveMsg" runat="server"></asp:Label>
                                </p>
                            </div>
                        </div>
                        <div class="Steps" runat="server" id="dvStep" visible="false">
                            <ul>
                                <li id="liTab1" runat="server" class="One1 Current">Client Information</li>
                                <li id="liTab2" runat="server" class="Two">
                                    <asp:LinkButton ID="lnkConfig" runat="server" Text="Client Configuration" OnClick="lnkConfig_Click"
                                        CausesValidation="false"></asp:LinkButton>
                                </li>
                                <li id="liTab3" runat="server" class="Three">
                                    <asp:LinkButton ID="lnkTheme" runat="server" Text="Theme Builder" OnClick="lnkTheme_Click"
                                        CausesValidation="false"></asp:LinkButton>
                                </li>
                                <li id="liTab4" runat="server" class="Four">
                                    <asp:LinkButton ID="lnkSubscription" runat="server" Text="Subscription" OnClick="lnkSubscription_Click"
                                        CausesValidation="false"></asp:LinkButton>
                                </li>
                            </ul>
                            <div class="Clr">
                            </div>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td align="right" style="padding-bottom: 10px;">
                        <asp:Label ID="lbtnBackLabel" runat="server" Font-Bold="False" Text="Return to "></asp:Label>
                        <asp:LinkButton ID="lbtnBack" runat="server" Text="Client Information" CausesValidation="false"
                            CssClass="lnkBtn1" OnClick="lbtnBack_Click"></asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <div class="FormCont Steps" style="padding-bottom: 20px; margin-bottom: 10px">
                            <table width="100%">
                                <tr>
                                    <td align="left" style="padding-bottom: 10px;">
                                        <asp:Label ID="Label1" runat="server" Font-Bold="True" Text="Client Information"
                                            CssClass="frmHeading"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="70%">
                                        <table border="0" width="100%">
                                            <tr>
                                                <td width="20%" style="padding-left: 5px">
                                                    Client Name<span class='EBmsg'>&nbsp;*</span>
                                                </td>
                                                <td width="80%">
                                                    <asp:TextBox ID="txtClientName" runat="server" MaxLength="50" CssClass="textbox_EB"
                                                        Width="250"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="vx_txtEmail" runat="server" CssClass="ValidationSummary"
                                                        ControlToValidate="txtClientName" ErrorMessage="Please enter required field - Client Information: Client Name"
                                                        ForeColor="Red" Display="None" Text="*" />
                                                    <asp:HiddenField ID="hClientID" runat="server" Value="0" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="padding-left: 5px">
                                                    Address<span class='EBmsg'>&nbsp;*</span>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtAddress" runat="server" MaxLength="100" CssClass="textbox_EB"
                                                        Width="250"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="vx_txtAddress" runat="server" CssClass="ValidationSummary"
                                                        ControlToValidate="txtAddress" ErrorMessage="Please enter required field - Client Information: Address"
                                                        ForeColor="Red" Display="None" Text="*" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="padding-left: 5px">
                                                    City<span class='EBmsg'>&nbsp;*</span>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtCity" runat="server" MaxLength="50" CssClass="textbox_EB" Width="200"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="vx_txtCity" runat="server" CssClass="ValidationSummary"
                                                        ControlToValidate="txtCity" ErrorMessage="Please enter required field - Client Information: City"
                                                        ForeColor="Red" Display="None" Text="*" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="padding-left: 5px">
                                                    State/Province/Region<span class='EBmsg'>&nbsp;*</span>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtState" runat="server" MaxLength="50" CssClass="textbox_EB" Width="200"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="vx_txtState" runat="server" CssClass="ValidationSummary"
                                                        ControlToValidate="txtState" ErrorMessage="Please enter required field - Client Information: State/Province/Region"
                                                        ForeColor="Red" Display="None" Text="*" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="padding-left: 5px">
                                                    Country<span class='EBmsg'>&nbsp;*</span>
                                                </td>
                                                <td>
                                                    <telerik:RadComboBox ID="rcmbCountry" runat="server" ShowDropDownOnTextboxClick="True"
                                                        MarkFirstMatch="True" AllowCustomText="False" HighlightTemplatedItems="True"
                                                        ExpandAnimation-Duration="1000" CollapseAnimation-Duration="1000" CollapseAnimation-Type="OutQuart"
                                                        NoWrap="True" Font-Names="Verdana" Font-Size="11px" EnableTheming="True" ForeColor="Black"
                                                        Font-Italic="False" Skin="Default" Width="220" CssClass="rad-combo">
                                                        <Items>
                                                            <telerik:RadComboBoxItem runat="server" Value="1" Text="Afghanistan" />
                                                            <telerik:RadComboBoxItem runat="server" Value="2" Text="Albania" />
                                                            <telerik:RadComboBoxItem runat="server" Value="3" Text="Algeria" />
                                                            <telerik:RadComboBoxItem runat="server" Value="4" Text="American Samoa" />
                                                            <telerik:RadComboBoxItem runat="server" Value="5" Text="Andorra" />
                                                            <telerik:RadComboBoxItem runat="server" Value="6" Text="Angola" />
                                                            <telerik:RadComboBoxItem runat="server" Value="7" Text="Anguilla" />
                                                            <telerik:RadComboBoxItem runat="server" Value="8" Text="Antarctica" />
                                                            <telerik:RadComboBoxItem runat="server" Value="9" Text="Antigua And Barbuda" />
                                                            <telerik:RadComboBoxItem runat="server" Value="10" Text="Argentina" />
                                                            <telerik:RadComboBoxItem runat="server" Value="11" Text="Armenia" />
                                                            <telerik:RadComboBoxItem runat="server" Value="12" Text="Aruba" />
                                                            <telerik:RadComboBoxItem runat="server" Value="13" Text="Australia" />
                                                            <telerik:RadComboBoxItem runat="server" Value="14" Text="Austria" />
                                                            <telerik:RadComboBoxItem runat="server" Value="15" Text="Azerbaijan" />
                                                            <telerik:RadComboBoxItem runat="server" Value="16" Text="Bahamas" />
                                                            <telerik:RadComboBoxItem runat="server" Value="17" Text="Bahrain" />
                                                            <telerik:RadComboBoxItem runat="server" Value="18" Text="Bangladesh" />
                                                            <telerik:RadComboBoxItem runat="server" Value="19" Text="Barbados" />
                                                            <telerik:RadComboBoxItem runat="server" Value="20" Text="Belarus" />
                                                            <telerik:RadComboBoxItem runat="server" Value="21" Text="Belgium" />
                                                            <telerik:RadComboBoxItem runat="server" Value="22" Text="Belize" />
                                                            <telerik:RadComboBoxItem runat="server" Value="23" Text="Benin" />
                                                            <telerik:RadComboBoxItem runat="server" Value="24" Text="Bermuda" />
                                                            <telerik:RadComboBoxItem runat="server" Value="25" Text="Bhutan" />
                                                            <telerik:RadComboBoxItem runat="server" Value="26" Text="Bolivia" />
                                                            <telerik:RadComboBoxItem runat="server" Value="27" Text="Bosnia And Herzegowina" />
                                                            <telerik:RadComboBoxItem runat="server" Value="28" Text="Botswana" />
                                                            <telerik:RadComboBoxItem runat="server" Value="29" Text="Bouvet Island" />
                                                            <telerik:RadComboBoxItem runat="server" Value="30" Text="Brazil" />
                                                            <telerik:RadComboBoxItem runat="server" Value="31" Text="British Indian Ocean Territory" />
                                                            <telerik:RadComboBoxItem runat="server" Value="32" Text="Brunei Darussalam" />
                                                            <telerik:RadComboBoxItem runat="server" Value="33" Text="Bulgaria" />
                                                            <telerik:RadComboBoxItem runat="server" Value="34" Text="Burkina Faso" />
                                                            <telerik:RadComboBoxItem runat="server" Value="35" Text="Burundi" />
                                                            <telerik:RadComboBoxItem runat="server" Value="36" Text="Cambodia" />
                                                            <telerik:RadComboBoxItem runat="server" Value="37" Text="Cameroon" />
                                                            <telerik:RadComboBoxItem runat="server" Value="38" Text="Canada" />
                                                            <telerik:RadComboBoxItem runat="server" Value="39" Text="Cape Verde" />
                                                            <telerik:RadComboBoxItem runat="server" Value="40" Text="Cayman Islands" />
                                                            <telerik:RadComboBoxItem runat="server" Value="41" Text="Central African Republic" />
                                                            <telerik:RadComboBoxItem runat="server" Value="42" Text="Chad" />
                                                            <telerik:RadComboBoxItem runat="server" Value="43" Text="Chile" />
                                                            <telerik:RadComboBoxItem runat="server" Value="44" Text="China" />
                                                            <telerik:RadComboBoxItem runat="server" Value="45" Text="Christmas Island" />
                                                            <telerik:RadComboBoxItem runat="server" Value="46" Text="Cocos (Keeling) Islands" />
                                                            <telerik:RadComboBoxItem runat="server" Value="47" Text="Colombia" />
                                                            <telerik:RadComboBoxItem runat="server" Value="48" Text="Comoros" />
                                                            <telerik:RadComboBoxItem runat="server" Value="49" Text="Congo" />
                                                            <telerik:RadComboBoxItem runat="server" Value="50" Text="Cook Islands" />
                                                            <telerik:RadComboBoxItem runat="server" Value="51" Text="Costa Rica" />
                                                            <telerik:RadComboBoxItem runat="server" Value="52" Text="Cote D'Ivoire" />
                                                            <telerik:RadComboBoxItem runat="server" Value="53" Text="Croatia (Local Name: Hrvatska)" />
                                                            <telerik:RadComboBoxItem runat="server" Value="54" Text="Cuba" />
                                                            <telerik:RadComboBoxItem runat="server" Value="55" Text="Cyprus" />
                                                            <telerik:RadComboBoxItem runat="server" Value="56" Text="Czech Republic" />
                                                            <telerik:RadComboBoxItem runat="server" Value="57" Text="Denmark" />
                                                            <telerik:RadComboBoxItem runat="server" Value="58" Text="Djibouti" />
                                                            <telerik:RadComboBoxItem runat="server" Value="59" Text="Dominica" />
                                                            <telerik:RadComboBoxItem runat="server" Value="60" Text="Dominican Republic" />
                                                            <telerik:RadComboBoxItem runat="server" Value="61" Text="East Timor" />
                                                            <telerik:RadComboBoxItem runat="server" Value="62" Text="Ecuador" />
                                                            <telerik:RadComboBoxItem runat="server" Value="63" Text="Egypt" />
                                                            <telerik:RadComboBoxItem runat="server" Value="64" Text="El Salvador" />
                                                            <telerik:RadComboBoxItem runat="server" Value="65" Text="Equatorial Guinea" />
                                                            <telerik:RadComboBoxItem runat="server" Value="66" Text="Eritrea" />
                                                            <telerik:RadComboBoxItem runat="server" Value="67" Text="Estonia" />
                                                            <telerik:RadComboBoxItem runat="server" Value="68" Text="Ethiopia" />
                                                            <telerik:RadComboBoxItem runat="server" Value="69" Text="Falkland Islands (Malvinas)" />
                                                            <telerik:RadComboBoxItem runat="server" Value="70" Text="Faroe Islands" />
                                                            <telerik:RadComboBoxItem runat="server" Value="71" Text="Fiji" />
                                                            <telerik:RadComboBoxItem runat="server" Value="72" Text="Finland" />
                                                            <telerik:RadComboBoxItem runat="server" Value="73" Text="France" />
                                                            <telerik:RadComboBoxItem runat="server" Value="74" Text="French Guiana" />
                                                            <telerik:RadComboBoxItem runat="server" Value="75" Text="French Polynesia" />
                                                            <telerik:RadComboBoxItem runat="server" Value="76" Text="French Southern Territories" />
                                                            <telerik:RadComboBoxItem runat="server" Value="77" Text="Gabon" />
                                                            <telerik:RadComboBoxItem runat="server" Value="78" Text="Gambia" />
                                                            <telerik:RadComboBoxItem runat="server" Value="79" Text="Georgia" />
                                                            <telerik:RadComboBoxItem runat="server" Value="80" Text="Germany" />
                                                            <telerik:RadComboBoxItem runat="server" Value="81" Text="Ghana" />
                                                            <telerik:RadComboBoxItem runat="server" Value="82" Text="Gibraltar" />
                                                            <telerik:RadComboBoxItem runat="server" Value="83" Text="Greece" />
                                                            <telerik:RadComboBoxItem runat="server" Value="84" Text="Greenland" />
                                                            <telerik:RadComboBoxItem runat="server" Value="85" Text="Grenada" />
                                                            <telerik:RadComboBoxItem runat="server" Value="86" Text="Guadeloupe" />
                                                            <telerik:RadComboBoxItem runat="server" Value="87" Text="Guam" />
                                                            <telerik:RadComboBoxItem runat="server" Value="88" Text="Guatemala" />
                                                            <telerik:RadComboBoxItem runat="server" Value="89" Text="Guinea" />
                                                            <telerik:RadComboBoxItem runat="server" Value="90" Text="Guinea-Bissau" />
                                                            <telerik:RadComboBoxItem runat="server" Value="91" Text="Guyana" />
                                                            <telerik:RadComboBoxItem runat="server" Value="92" Text="Haiti" />
                                                            <telerik:RadComboBoxItem runat="server" Value="93" Text="Heard And Mc Donald Islands" />
                                                            <telerik:RadComboBoxItem runat="server" Value="94" Text="Holy See (Vatican City State)" />
                                                            <telerik:RadComboBoxItem runat="server" Value="95" Text="Honduras" />
                                                            <telerik:RadComboBoxItem runat="server" Value="96" Text="Hong Kong" />
                                                            <telerik:RadComboBoxItem runat="server" Value="97" Text="Hungary" />
                                                            <telerik:RadComboBoxItem runat="server" Value="98" Text="Icel And" />
                                                            <telerik:RadComboBoxItem runat="server" Value="99" Text="India" />
                                                            <telerik:RadComboBoxItem runat="server" Value="100" Text="Indonesia" />
                                                            <telerik:RadComboBoxItem runat="server" Value="101" Text="Iran (Islamic Republic Of)" />
                                                            <telerik:RadComboBoxItem runat="server" Value="102" Text="Iraq" />
                                                            <telerik:RadComboBoxItem runat="server" Value="103" Text="Ireland" />
                                                            <telerik:RadComboBoxItem runat="server" Value="104" Text="Israel" />
                                                            <telerik:RadComboBoxItem runat="server" Value="105" Text="Italy" />
                                                            <telerik:RadComboBoxItem runat="server" Value="106" Text="Jamaica" />
                                                            <telerik:RadComboBoxItem runat="server" Value="107" Text="Japan" />
                                                            <telerik:RadComboBoxItem runat="server" Value="108" Text="Jordan" />
                                                            <telerik:RadComboBoxItem runat="server" Value="109" Text="Kazakhstan" />
                                                            <telerik:RadComboBoxItem runat="server" Value="110" Text="Kenya" />
                                                            <telerik:RadComboBoxItem runat="server" Value="111" Text="Kiribati" />
                                                            <telerik:RadComboBoxItem runat="server" Value="112" Text="Dem People'S Republic Korea" />
                                                            <telerik:RadComboBoxItem runat="server" Value="113" Text="Republic Of Korea" />
                                                            <telerik:RadComboBoxItem runat="server" Value="114" Text="Kuwait" />
                                                            <telerik:RadComboBoxItem runat="server" Value="115" Text="Kyrgyzstan" />
                                                            <telerik:RadComboBoxItem runat="server" Value="116" Text="Lao People'S Dem Republic" />
                                                            <telerik:RadComboBoxItem runat="server" Value="117" Text="Latvia" />
                                                            <telerik:RadComboBoxItem runat="server" Value="118" Text="Lebanon" />
                                                            <telerik:RadComboBoxItem runat="server" Value="119" Text="Lesotho" />
                                                            <telerik:RadComboBoxItem runat="server" Value="120" Text="Liberia" />
                                                            <telerik:RadComboBoxItem runat="server" Value="121" Text="Libyan Arab Jamahiriya" />
                                                            <telerik:RadComboBoxItem runat="server" Value="122" Text="Liechtenstein" />
                                                            <telerik:RadComboBoxItem runat="server" Value="123" Text="Lithuania" />
                                                            <telerik:RadComboBoxItem runat="server" Value="124" Text="Luxembourg" />
                                                            <telerik:RadComboBoxItem runat="server" Value="125" Text="Macau" />
                                                            <telerik:RadComboBoxItem runat="server" Value="126" Text="Macedonia" />
                                                            <telerik:RadComboBoxItem runat="server" Value="127" Text="Madagascar" />
                                                            <telerik:RadComboBoxItem runat="server" Value="128" Text="Malawi" />
                                                            <telerik:RadComboBoxItem runat="server" Value="129" Text="Malaysia" />
                                                            <telerik:RadComboBoxItem runat="server" Value="130" Text="Maldives" />
                                                            <telerik:RadComboBoxItem runat="server" Value="131" Text="Mali" />
                                                            <telerik:RadComboBoxItem runat="server" Value="132" Text="Malta" />
                                                            <telerik:RadComboBoxItem runat="server" Value="133" Text="Marshall Islands" />
                                                            <telerik:RadComboBoxItem runat="server" Value="134" Text="Martinique" />
                                                            <telerik:RadComboBoxItem runat="server" Value="135" Text="Mauritania" />
                                                            <telerik:RadComboBoxItem runat="server" Value="136" Text="Mauritius" />
                                                            <telerik:RadComboBoxItem runat="server" Value="137" Text="Mayotte" />
                                                            <telerik:RadComboBoxItem runat="server" Value="138" Text="Mexico" />
                                                            <telerik:RadComboBoxItem runat="server" Value="139" Text="Micronesia, Federated States" />
                                                            <telerik:RadComboBoxItem runat="server" Value="140" Text="Moldova, Republic Of" />
                                                            <telerik:RadComboBoxItem runat="server" Value="141" Text="Monaco" />
                                                            <telerik:RadComboBoxItem runat="server" Value="142" Text="Mongolia" />
                                                            <telerik:RadComboBoxItem runat="server" Value="143" Text="Montserrat" />
                                                            <telerik:RadComboBoxItem runat="server" Value="144" Text="Morocco" />
                                                            <telerik:RadComboBoxItem runat="server" Value="145" Text="Mozambique" />
                                                            <telerik:RadComboBoxItem runat="server" Value="146" Text="Myanmar" />
                                                            <telerik:RadComboBoxItem runat="server" Value="147" Text="Namibia" />
                                                            <telerik:RadComboBoxItem runat="server" Value="148" Text="Nauru" />
                                                            <telerik:RadComboBoxItem runat="server" Value="149" Text="Nepal" />
                                                            <telerik:RadComboBoxItem runat="server" Value="150" Text="Netherlands" />
                                                            <telerik:RadComboBoxItem runat="server" Value="151" Text="Netherlands Ant Illes" />
                                                            <telerik:RadComboBoxItem runat="server" Value="152" Text="New Caledonia" />
                                                            <telerik:RadComboBoxItem runat="server" Value="153" Text="New Zealand" />
                                                            <telerik:RadComboBoxItem runat="server" Value="154" Text="Nicaragua" />
                                                            <telerik:RadComboBoxItem runat="server" Value="155" Text="Niger" />
                                                            <telerik:RadComboBoxItem runat="server" Value="156" Text="Nigeria" />
                                                            <telerik:RadComboBoxItem runat="server" Value="157" Text="Niue" />
                                                            <telerik:RadComboBoxItem runat="server" Value="158" Text="Norfolk Island" />
                                                            <telerik:RadComboBoxItem runat="server" Value="159" Text="Northern Mariana Islands" />
                                                            <telerik:RadComboBoxItem runat="server" Value="160" Text="Norway" />
                                                            <telerik:RadComboBoxItem runat="server" Value="161" Text="Oman" />
                                                            <telerik:RadComboBoxItem runat="server" Value="162" Text="Pakistan" />
                                                            <telerik:RadComboBoxItem runat="server" Value="163" Text="Palau" />
                                                            <telerik:RadComboBoxItem runat="server" Value="164" Text="Panama" />
                                                            <telerik:RadComboBoxItem runat="server" Value="165" Text="Papua New Guinea" />
                                                            <telerik:RadComboBoxItem runat="server" Value="166" Text="Paraguay" />
                                                            <telerik:RadComboBoxItem runat="server" Value="167" Text="Peru" />
                                                            <telerik:RadComboBoxItem runat="server" Value="168" Text="Philippines" />
                                                            <telerik:RadComboBoxItem runat="server" Value="169" Text="Pitcairn" />
                                                            <telerik:RadComboBoxItem runat="server" Value="170" Text="Poland" />
                                                            <telerik:RadComboBoxItem runat="server" Value="171" Text="Portugal" />
                                                            <telerik:RadComboBoxItem runat="server" Value="172" Text="Puerto Rico" />
                                                            <telerik:RadComboBoxItem runat="server" Value="173" Text="Qatar" />
                                                            <telerik:RadComboBoxItem runat="server" Value="174" Text="Reunion" />
                                                            <telerik:RadComboBoxItem runat="server" Value="175" Text="Romania" />
                                                            <telerik:RadComboBoxItem runat="server" Value="176" Text="Russian Federation" />
                                                            <telerik:RadComboBoxItem runat="server" Value="177" Text="Rwanda" />
                                                            <telerik:RadComboBoxItem runat="server" Value="178" Text="Saint K Itts And Nevis" />
                                                            <telerik:RadComboBoxItem runat="server" Value="179" Text="Saint Lucia" />
                                                            <telerik:RadComboBoxItem runat="server" Value="180" Text="Saint Vincent The Grenadines" />
                                                            <telerik:RadComboBoxItem runat="server" Value="181" Text="Samoa" />
                                                            <telerik:RadComboBoxItem runat="server" Value="182" Text="San Marino" />
                                                            <telerik:RadComboBoxItem runat="server" Value="183" Text="Sao Tome And Principe" />
                                                            <telerik:RadComboBoxItem runat="server" Value="184" Text="Saudi Arabia" />
                                                            <telerik:RadComboBoxItem runat="server" Value="185" Text="Senegal" />
                                                            <telerik:RadComboBoxItem runat="server" Value="186" Text="Seychelles" />
                                                            <telerik:RadComboBoxItem runat="server" Value="187" Text="Sierra Leone" />
                                                            <telerik:RadComboBoxItem runat="server" Value="188" Text="Singapore" />
                                                            <telerik:RadComboBoxItem runat="server" Value="189" Text="Slovakia (Slovak Republic)" />
                                                            <telerik:RadComboBoxItem runat="server" Value="190" Text="Slovenia" />
                                                            <telerik:RadComboBoxItem runat="server" Value="191" Text="Solomon Islands" />
                                                            <telerik:RadComboBoxItem runat="server" Value="192" Text="Somalia" />
                                                            <telerik:RadComboBoxItem runat="server" Value="193" Text="South Africa" />
                                                            <telerik:RadComboBoxItem runat="server" Value="194" Text="South Georgia S Sandwich Is." />
                                                            <telerik:RadComboBoxItem runat="server" Value="195" Text="Spain" />
                                                            <telerik:RadComboBoxItem runat="server" Value="196" Text="Sri Lanka" />
                                                            <telerik:RadComboBoxItem runat="server" Value="197" Text="St. Helena" />
                                                            <telerik:RadComboBoxItem runat="server" Value="198" Text="St. Pierre And Miquelon" />
                                                            <telerik:RadComboBoxItem runat="server" Value="199" Text="Sudan" />
                                                            <telerik:RadComboBoxItem runat="server" Value="200" Text="Suriname" />
                                                            <telerik:RadComboBoxItem runat="server" Value="201" Text="Svalbard Jan Mayen Islands" />
                                                            <telerik:RadComboBoxItem runat="server" Value="202" Text="Sw Aziland" />
                                                            <telerik:RadComboBoxItem runat="server" Value="203" Text="Sweden" />
                                                            <telerik:RadComboBoxItem runat="server" Value="204" Text="Switzerland" />
                                                            <telerik:RadComboBoxItem runat="server" Value="205" Text="Syrian Arab Republic" />
                                                            <telerik:RadComboBoxItem runat="server" Value="206" Text="Taiwan" />
                                                            <telerik:RadComboBoxItem runat="server" Value="207" Text="Tajikistan" />
                                                            <telerik:RadComboBoxItem runat="server" Value="208" Text="United Republic Of Tanzania" />
                                                            <telerik:RadComboBoxItem runat="server" Value="209" Text="Thailand" />
                                                            <telerik:RadComboBoxItem runat="server" Value="210" Text="Togo" />
                                                            <telerik:RadComboBoxItem runat="server" Value="211" Text="Tokelau" />
                                                            <telerik:RadComboBoxItem runat="server" Value="212" Text="Tonga" />
                                                            <telerik:RadComboBoxItem runat="server" Value="213" Text="Trinidad And Tobago" />
                                                            <telerik:RadComboBoxItem runat="server" Value="214" Text="Tunisia" />
                                                            <telerik:RadComboBoxItem runat="server" Value="215" Text="Turkey" />
                                                            <telerik:RadComboBoxItem runat="server" Value="216" Text="Turkmenistan" />
                                                            <telerik:RadComboBoxItem runat="server" Value="217" Text="Turks And Caicos Islands" />
                                                            <telerik:RadComboBoxItem runat="server" Value="218" Text="Tuvalu" />
                                                            <telerik:RadComboBoxItem runat="server" Value="219" Text="Uganda" />
                                                            <telerik:RadComboBoxItem runat="server" Value="220" Text="Ukraine" />
                                                            <telerik:RadComboBoxItem runat="server" Value="221" Text="United Arab Emirates" />
                                                            <telerik:RadComboBoxItem runat="server" Value="222" Text="United Kingdom" />
                                                            <telerik:RadComboBoxItem runat="server" Value="223" Selected="true" Text="United States" />
                                                            <telerik:RadComboBoxItem runat="server" Value="224" Text="United States Minor Is." />
                                                            <telerik:RadComboBoxItem runat="server" Value="225" Text="Uruguay" />
                                                            <telerik:RadComboBoxItem runat="server" Value="226" Text="Uzbekistan" />
                                                            <telerik:RadComboBoxItem runat="server" Value="227" Text="Vanuatu" />
                                                            <telerik:RadComboBoxItem runat="server" Value="228" Text="Venezuela" />
                                                            <telerik:RadComboBoxItem runat="server" Value="229" Text="Viet Nam" />
                                                            <telerik:RadComboBoxItem runat="server" Value="230" Text="Virgin Islands (British)" />
                                                            <telerik:RadComboBoxItem runat="server" Value="231" Text="Virgin Islands (U.S.)" />
                                                            <telerik:RadComboBoxItem runat="server" Value="232" Text="Wallis And Futuna Islands" />
                                                            <telerik:RadComboBoxItem runat="server" Value="233" Text="Western Sahara" />
                                                            <telerik:RadComboBoxItem runat="server" Value="234" Text="Yemen" />
                                                            <telerik:RadComboBoxItem runat="server" Value="235" Text="Yugoslavia" />
                                                            <telerik:RadComboBoxItem runat="server" Value="236" Text="Zaire" />
                                                            <telerik:RadComboBoxItem runat="server" Value="237" Text="Zambia" />
                                                            <telerik:RadComboBoxItem runat="server" Value="238" Text="Zimbabwe" />
                                                        </Items>
                                                    </telerik:RadComboBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="padding-left: 5px">
                                                    Postal Code<span class='EBmsg'>&nbsp;*</span>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtPostcode" runat="server" MaxLength="20" CssClass="textbox_EB"
                                                        Width="100"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="vx_txtPostcode" runat="server" CssClass="ValidationSummary"
                                                        ControlToValidate="txtPostcode" ErrorMessage="Please enter required field - Client Information: Postal Code"
                                                        ForeColor="Red" Display="None" Text="*" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="padding-left: 5px">
                                                    Telephone<span class='EBmsg'>&nbsp;*</span>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtPhone" runat="server" MaxLength="30" CssClass="textbox_EB" Width="150"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="vx_txtPhone" runat="server" CssClass="ValidationSummary"
                                                        ControlToValidate="txtPhone" ErrorMessage="Please enter required field - Client Information: Telephone"
                                                        ForeColor="Red" Display="None" Text="*" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="padding-left: 5px">
                                                    Website<span class='EBmsg'>&nbsp;*</span>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtWebsite" runat="server" MaxLength="300" CssClass="textbox_EB"
                                                        Width="200"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="vx_txtWebsite" runat="server" CssClass="ValidationSummary"
                                                        ControlToValidate="txtWebsite" ErrorMessage="Please enter required field - Client Information: Website"
                                                        ForeColor="Red" Display="None" Text="*" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="padding-left: 5px">
                                                    Industry<span class='EBmsg'>&nbsp;*</span>
                                                </td>
                                                <td>
                                                    <telerik:RadComboBox ID="rcmbIndustry" runat="server" ShowDropDownOnTextboxClick="True"
                                                        MarkFirstMatch="True" AllowCustomText="False" HighlightTemplatedItems="True"
                                                        ExpandAnimation-Duration="1000" CollapseAnimation-Duration="1000" CollapseAnimation-Type="OutQuart"
                                                        NoWrap="True" Font-Names="Verdana" Font-Size="11px" EnableTheming="True" ForeColor="Black"
                                                        Font-Italic="False" Skin="Default" Width="220" CssClass="rad-combo">
                                                        <Items>
                                                            <telerik:RadComboBoxItem runat="server" Value="1" Text="Automotive" />
                                                            <telerik:RadComboBoxItem runat="server" Value="2" Text="Beauty/Personal Care" />
                                                            <telerik:RadComboBoxItem runat="server" Value="3" Text="Careers/Recruiting" />
                                                            <telerik:RadComboBoxItem runat="server" Value="4" Text="Consumer Electronics" />
                                                            <telerik:RadComboBoxItem runat="server" Value="5" Text="Consumer Packages Goods (CPG)" />
                                                            <telerik:RadComboBoxItem runat="server" Value="6" Text="Dating" />
                                                            <telerik:RadComboBoxItem runat="server" Value="7" Text="Education/Training" />
                                                            <telerik:RadComboBoxItem runat="server" Value="8" Text="Entertainment/Events/Tickets" />
                                                            <telerik:RadComboBoxItem runat="server" Value="9" Text="Financial Services" />
                                                            <telerik:RadComboBoxItem runat="server" Value="10" Text="Food/Beverage" />
                                                            <telerik:RadComboBoxItem runat="server" Value="11" Text="Games/Gaming" />
                                                            <telerik:RadComboBoxItem runat="server" Value="12" Text="Health/Fitness/Diet" />
                                                            <telerik:RadComboBoxItem runat="server" Value="13" Text="Healthcare" />
                                                            <telerik:RadComboBoxItem runat="server" Value="14" Text="Home/Garden" />
                                                            <telerik:RadComboBoxItem runat="server" Value="15" Text="Music/Movies/Media/TV" />
                                                            <telerik:RadComboBoxItem runat="server" Value="16" Text="Other Services" />
                                                            <telerik:RadComboBoxItem runat="server" Value="17" Text="Public Services" />
                                                            <telerik:RadComboBoxItem runat="server" Value="18" Text="Publishing" />
                                                            <telerik:RadComboBoxItem runat="server" Value="19" Text="Real Estate" />
                                                            <telerik:RadComboBoxItem runat="server" Value="20" Text="Recruiting/HR" />
                                                            <telerik:RadComboBoxItem runat="server" Value="21" Text="Restaurant" />
                                                            <telerik:RadComboBoxItem runat="server" Value="22" Text="Retail" />
                                                            <telerik:RadComboBoxItem runat="server" Value="23" Text="Technology" />
                                                            <telerik:RadComboBoxItem runat="server" Value="24" Text="Telecommunications/ISP" />
                                                            <telerik:RadComboBoxItem runat="server" Value="25" Text="Travel" />
                                                            <telerik:RadComboBoxItem runat="server" Value="26" Text="Other" />
                                                        </Items>
                                                    </telerik:RadComboBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="padding-left: 5px">
                                                    Annual Revenue<span class='EBmsg'>&nbsp;*</span>
                                                </td>
                                                <td>
                                                    <telerik:RadComboBox ID="rcmbRevenue" runat="server" ShowDropDownOnTextboxClick="True"
                                                        MarkFirstMatch="True" AllowCustomText="False" HighlightTemplatedItems="True"
                                                        ExpandAnimation-Duration="1000" CollapseAnimation-Duration="1000" CollapseAnimation-Type="OutQuart"
                                                        NoWrap="True" Font-Names="Verdana" Font-Size="11px" EnableTheming="True" ForeColor="Black"
                                                        Font-Italic="False" Skin="Default" Width="120" CssClass="rad-combo">
                                                        <Items>
                                                            <telerik:RadComboBoxItem runat="server" Value="1" Text="<$1M" />
                                                            <telerik:RadComboBoxItem runat="server" Value="2" Text="$1M - $5M" />
                                                            <telerik:RadComboBoxItem runat="server" Value="3" Text="$5M - $50M" />
                                                            <telerik:RadComboBoxItem runat="server" Value="4" Text="$50M - $100M" />
                                                            <telerik:RadComboBoxItem runat="server" Value="5" Text="$100M - $250M" />
                                                            <telerik:RadComboBoxItem runat="server" Value="6" Text="$250M - $500M" />
                                                            <telerik:RadComboBoxItem runat="server" Value="7" Text="$500M - $1B" />
                                                            <telerik:RadComboBoxItem runat="server" Value="8" Text="$1B - $2B" />
                                                            <telerik:RadComboBoxItem runat="server" Value="9" Text=">$2B" />
                                                        </Items>
                                                    </telerik:RadComboBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="padding-left: 5px">
                                                    Number of Users<span class='EBmsg'>&nbsp;*</span>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtNoUsers" runat="server" Width="40" MaxLength="4" CssClass="textbox_EB"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="vx_txtNoUsers" runat="server" CssClass="ValidationSummary"
                                                        ControlToValidate="txtNoUsers" ErrorMessage="Please enter required field - Client Information: Number of Users"
                                                        ForeColor="Red" Display="None" Text="*" />
                                                    <asp:Label ID="lblActiveUserCnt" runat="server" Text=""></asp:Label>
                                                    <asp:HiddenField ID="hActiveUserCnt" runat="server" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td width="30%" valign="top" align="left">
                                        <div class="platform-box">
                                            <b>Platform Edition</b><br />
                                            <asp:CheckBoxList ID="chkPackage" runat="server" TextAlign="right" CellPadding="10"
                                                CssClass="cbl">
                                                <asp:ListItem Text="Enterprise" Value="Enterprise" Selected="True" />
                                                <asp:ListItem Text="Professional" Value="Professional" />
                                            </asp:CheckBoxList>
                                            <asp:HiddenField ID="hCurrPkg" Value="" runat="server" />
                                            <b>Profile Status</b><br />
                                            <asp:PlaceHolder ID="PlaceHolderActive" runat="server" Visible="false">
                                                <br />
                                                <telerik:RadComboBox ID="rcmbStatus" runat="server" Width="80" Enabled="false" CssClass="active-dd"
                                                    MarkFirstMatch="True" HighlightTemplatedItems="True" ExpandAnimation-Duration="1000"
                                                    CollapseAnimation-Duration="1000" CollapseAnimation-Type="OutQuart" NoWrap="True"
                                                    EnableTheming="True" Font-Italic="False" Skin="Default">
                                                    <Items>
                                                        <telerik:RadComboBoxItem runat="server" Text="Active" Value="Active" Selected="True" />
                                                        <telerik:RadComboBoxItem runat="server" Text="Inactive" Value="Inactive" />
                                                    </Items>
                                                </telerik:RadComboBox>
                                                <asp:HiddenField ID="hCurrStatus" runat="server" />
                                            </asp:PlaceHolder>
                                            <asp:PlaceHolder ID="PlaceHolderReactivate" runat="server" Visible="false">
                                                <br />
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnActivate" runat="server" Text="Reactivate"
                                                    Visible="false" CssClass="SubBtn" OnClick="btnActivate_Click" CausesValidation="false" />
                                            </asp:PlaceHolder>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <div class="FormCont Steps" style="padding-bottom: 20px; margin-bottom: 10px">
                            <table border="0" width="100%">
                                <tr>
                                    <td align="left" style="padding-bottom: 10px;">
                                        <asp:Label ID="lblCap2" runat="server" Font-Bold="True" Text="Main Contact" CssClass="frmHeading"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="155px" style="padding-left: 5px">
                                        Name<span class='EBmsg'>&nbsp;*</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtContactName" runat="server" MaxLength="50" CssClass="textbox_EB"
                                            Width="200"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="vx_txtContactName" runat="server" CssClass="ValidationSummary"
                                            ControlToValidate="txtContactName" ErrorMessage="Please enter required field - Main Contact: Name"
                                            ForeColor="Red" Display="None" Text="*" />
                                        <asp:HiddenField ID="hContactID" runat="server" Value="0" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding-left: 5px">
                                        Telephone<span class='EBmsg'>&nbsp;*</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtContactPhone" runat="server" MaxLength="30" CssClass="textbox_EB"
                                            Width="150"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="vx_txtContactPhone" runat="server" CssClass="ValidationSummary"
                                            ControlToValidate="txtContactPhone" ErrorMessage="Please enter required field - Main Contact: Telephone"
                                            ForeColor="Red" Display="None" Text="*" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding-left: 5px">
                                        Email Address<span class='EBmsg'>&nbsp;*</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtContactEmail" runat="server" MaxLength="100" CssClass="textbox_EB"
                                            Width="250"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="vx_txtContactEmail" runat="server" CssClass="ValidationSummary"
                                            ControlToValidate="txtContactEmail" ErrorMessage="Please enter required field - Main Contact: Email Address"
                                            ForeColor="Red" Display="None" Text="*" />
                                    </td>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator" runat="server" CssClass="ValidationSummary"
                                        ControlToValidate="txtContactEmail" Display="None" ForeColor="Red" ErrorMessage="Email must be in the form someone@domain.com. Check leading and trailing space(s)."
                                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                </tr>
                                <tr>
                                    <td style="padding-left: 5px">
                                        Department<span class='EBmsg'>&nbsp;*</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtContactDepart" runat="server" MaxLength="50" CssClass="textbox_EB"
                                            Width="200"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="vx_txtContactDepart" runat="server" CssClass="ValidationSummary"
                                            ControlToValidate="txtContactDepart" ErrorMessage="Please enter required field - Main Contact: Department"
                                            ForeColor="Red" Display="None" Text="*" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding-left: 5px">
                                        Job Title<span class='EBmsg'>&nbsp;*</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtJobTitle" runat="server" MaxLength="30" CssClass="textbox_EB"
                                            Width="200"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="vx_txtJobTitle" runat="server" CssClass="ValidationSummary"
                                            ControlToValidate="txtJobTitle" ErrorMessage="Please enter required field - Main Contact: Job Title"
                                            ForeColor="Red" Display="None" Text="*" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <div class="FormSection Steps" style="padding-bottom: 20px; margin-bottom: 10px">
                            <table border="0" width="100%">
                                <tr>
                                    <td align="left" style="padding-bottom: 10px;">
                                        <asp:Label ID="lblCap3" runat="server" Font-Bold="True" Text="Administrator" CssClass="frmHeading"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="155px" style="padding-left: 5px">
                                        First Name<span class='EBmsg'>&nbsp;*</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtAdminFName" runat="server" CssClass="textbox_EB" Width="200"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="vx_txtAdminFName" runat="server" CssClass="ValidationSummary"
                                            ControlToValidate="txtAdminFName" ErrorMessage="Please enter required field - Administrator: First Name"
                                            ForeColor="Red" Display="None" Text="*" />
                                        <asp:HiddenField ID="hAdminID" runat="server" Value="0" />
                                    </td>
                                </tr>
                                <tr>
                                    <td width="23%" style="padding-left: 5px">
                                        Last Name<span class='EBmsg'>&nbsp;*</span>
                                    </td>
                                    <td width="75%">
                                        <asp:TextBox ID="txtAdminLName" runat="server" CssClass="textbox_EB" Width="200"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="vx_txtAdminLName" runat="server" CssClass="ValidationSummary"
                                            ControlToValidate="txtAdminLName" ErrorMessage="Please enter required field - Administrator: Last Name"
                                            ForeColor="Red" Display="None" Text="*" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding-left: 5px">
                                        Telephone<span class='EBmsg'>&nbsp;*</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtAdminPhone" runat="server" CssClass="textbox_EB" Width="150"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="vx_txtAdminPhone" runat="server" CssClass="ValidationSummary"
                                            ControlToValidate="txtAdminPhone" ErrorMessage="Please enter required field - Administrator: Telephone"
                                            ForeColor="Red" Display="None" Text="*" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding-left: 5px">
                                        Email Address<span class='EBmsg'>&nbsp;*</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtAdminEmail" runat="server" CssClass="textbox_EB" Width="250"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="vx_txtAdminEmail" runat="server" CssClass="ValidationSummary"
                                            ControlToValidate="txtAdminEmail" ErrorMessage="Please enter required field - Administrator: Email Address"
                                            ForeColor="Red" Display="None" Text="*" />
                                    </td>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" CssClass="ValidationSummary"
                                        ControlToValidate="txtAdminEmail" Display="None" ForeColor="Red" ErrorMessage="Email must be in the form someone@domain.com. Check leading and trailing space(s)."
                                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                </tr>
                                <tr>
                                    <td style="padding-left: 5px">
                                        Department<span class='EBmsg'>&nbsp;*</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtAdminDept" runat="server" CssClass="textbox_EB" Width="200"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="vx_txtAdminDept" runat="server" CssClass="ValidationSummary"
                                            ControlToValidate="txtAdminDept" ErrorMessage="Please enter required field - Administrator: Department"
                                            ForeColor="Red" Display="None" Text="*" />
                                    </td>
                                </tr>
                            </table>
                            <div class="right">
                                <font color="#A00000">*</font><i><small>Required field</small></i>
                            </div>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Button ID="btnSave" runat="server" Text="&nbsp;Save&nbsp;" CssClass="SubBtn"
                            OnClick="btnSave_Click" />&nbsp;<asp:Label ID="lblClientError" ForeColor="Red" runat="server"></asp:Label>
                        <br />
                        <br />
                    </td>
                </tr>
            </table>
        </asp:PlaceHolder>
    </div>
    <div class="BottBg">
    </div>
    <img src="../Images/blank.gif" height="10" />
    <div class="Clr">
    </div>
</div>

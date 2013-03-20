<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="clientConfigList.ascx.cs"
    Inherits="EBird.Web.App.UserControls.clientConfigList" %>
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
<div class="RPart">
    <div class="TopBg">
    </div>
    <div class="widgets">
        <asp:PlaceHolder ID="phClientList" runat="server" Visible="true">
            <%--            <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
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
                            <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtSearch"
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
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </div>
            <div class="Steps1">
                <table border="0" width="100%">
                    <tr>
                        <td colspan="2" valign="middle" height="21">
                            Fliter From&nbsp;<telerik:RadDatePicker ID="dpFrom" Width="120px" runat="server"
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
                            ValidationSettings-EnableValidation="false" AutoGenerateColumns="False" OnItemCommand="tgrdClientList_ItemCommand"
                            OnItemDataBound="tgrdClientList_ItemDataBound" MasterTableView-NoDetailRecordsText="No activee clients available"
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
                                    <telerik:GridBoundColumn DataField="NoOfWebinars" HeaderText="Webinars" UniqueName="Webinars" SortExpression="NoOfWebinars">
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
                                height: 21px">
                                <p>
                                    <span class="ui-icon ui-icon-info" style="float: left; margin-right: .3em;"></span>
                                    <strong>Warning!</strong>&nbsp;&nbsp;<asp:Label ID="lblValidationMsg" runat="server"></asp:Label></p>
                            </div>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <div class="Steps" runat="server" id="dvStep" visible="false">
                            <ul>
                                <li id="liTab1" runat="server" class="One">
                                    <asp:LinkButton ID="lnkProfile" runat="server" Text="Client Profile" OnClick="lnkProfile_Click"
                                        CausesValidation="false"></asp:LinkButton>
                                </li>
                                <li id="liTab2" runat="server" class="Two Current ">Configuration</li>
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
                        <asp:LinkButton ID="lbtnBack" runat="server" Text="Client Configuration" CausesValidation="false"
                            CssClass="lnkBtn1" OnClick="lbtnBack_Click"></asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <div class="FormCont Steps">
                            <table border="0" width="100%">
                                <tr>
                                    <td>
                                        <asp:Label ID="lblClientName" runat="server" Font-Bold="True" Text="Client Name"
                                            CssClass="frmHeading"></asp:Label><br />
                                        <br />
                                        <asp:Label ID="lblClient" runat="server"></asp:Label>
                                        <asp:HiddenField ID="hClientID" runat="server" />
                                        <asp:HiddenField ID="hCustFeatureList" runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lblPackageSubscribed" runat="server" Font-Bold="True" Text="Package Subscribed"
                                            CssClass="frmHeading"></asp:Label><br />
                                        <br />
                                        <telerik:RadComboBox ID="rcmbPkgConfig" runat="server" CssClass="cmbPkg rad-combo"
                                            AutoPostBack="true" OnSelectedIndexChanged="rcmbPkgConfig_SelectedIndexChanged"
                                            MarkFirstMatch="True" HighlightTemplatedItems="True" ExpandAnimation-Duration="1000"
                                            CollapseAnimation-Duration="1000" CollapseAnimation-Type="OutQuart" NoWrap="True"
                                            EnableTheming="True" Font-Italic="False" Skin="Default">
                                            <Items>
                                                <telerik:RadComboBoxItem Text="Enterprise" Value="Enterprise"></telerik:RadComboBoxItem>
                                                <telerik:RadComboBoxItem Text="Professional" Value="Professional"></telerik:RadComboBoxItem>
                                                <telerik:RadComboBoxItem Text="Custom" Value="Custom"></telerik:RadComboBoxItem>
                                            </Items>
                                        </telerik:RadComboBox>
                                        <br />
                                        <asp:Label ID="lblPkg" runat="server" Visible="false"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblStatus" runat="server" Font-Bold="True" Text="Status" CssClass="frmHeading"></asp:Label><br />
                                        <br />
                                        &nbsp;&nbsp;<asp:Image ImageUrl="~/Images/icons/ActiveStatus1.png" runat="server"
                                            ID="imgStatus" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <div class="FormCont Steps" style="margin-bottom: 10px">
                            <table border="0" width="100%">
                                <tr>
                                    <td align="left" colspan="2" style="padding-bottom: 10px;">
                                        <asp:Label ID="lblCap1" runat="server" Font-Bold="True" Text="Client Default Configuration"
                                            CssClass="frmHeading"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="20%" style="padding-left: 5px">
                                        Language
                                    </td>
                                    <td width="80%">
                                        <telerik:RadComboBox ID="rcmbLanguage" runat="server" Skin="Default" CssClass="rad-combo"
                                            MarkFirstMatch="True" HighlightTemplatedItems="True" ExpandAnimation-Duration="1000"
                                            CollapseAnimation-Duration="1000" CollapseAnimation-Type="OutQuart" NoWrap="True"
                                            EnableTheming="True" Font-Italic="False">
                                            <Items>
                                                <telerik:RadComboBoxItem runat="server" Value="1" Text="English" />
                                                <telerik:RadComboBoxItem runat="server" Value="2" Text="French" />
                                                <telerik:RadComboBoxItem runat="server" Value="3" Text="Spanish" />
                                                <telerik:RadComboBoxItem runat="server" Value="4" Text="Portuguese" />
                                                <telerik:RadComboBoxItem runat="server" Value="5" Text="German" />
                                                <telerik:RadComboBoxItem runat="server" Value="6" Text="Italian" />
                                                <telerik:RadComboBoxItem runat="server" Value="7" Text="Chinese" />
                                                <telerik:RadComboBoxItem runat="server" Value="8" Text="Japanese" />
                                                <telerik:RadComboBoxItem runat="server" Value="9" Text="Korean" />
                                                <telerik:RadComboBoxItem runat="server" Value="10" Text="Russian" />
                                            </Items>
                                        </telerik:RadComboBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding-left: 5px">
                                        Date Format
                                    </td>
                                    <td>
                                        <telerik:RadComboBox ID="rcmbDateFormat" runat="server" Skin="Default" CssClass="rad-combo"
                                            MarkFirstMatch="True" HighlightTemplatedItems="True" ExpandAnimation-Duration="1000"
                                            CollapseAnimation-Duration="1000" CollapseAnimation-Type="OutQuart" NoWrap="True"
                                            EnableTheming="True" Font-Italic="False">
                                            <Items>
                                                <telerik:RadComboBoxItem runat="server" Value="MM-dd-yyyy" Text="MM/DD/YYYY" />
                                                <telerik:RadComboBoxItem runat="server" Value="dd-MM-yyyy" Text="DD/MM/YYYY" />
                                                <telerik:RadComboBoxItem runat="server" Value="yyyy-MM-dd" Text="YYYY/MM/DD" />
                                            </Items>
                                        </telerik:RadComboBox>
                                        &nbsp;&nbsp;
                                        <telerik:RadComboBox ID="ddCulture" runat="server">
                                        </telerik:RadComboBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top" style="padding-left: 5px">
                                        Time Zone
                                    </td>
                                    <td>
                                        <telerik:RadComboBox ID="rcmbTimeZone" runat="server" Skin="Default" Width="435"
                                            CssClass="rad-combo" MarkFirstMatch="True" HighlightTemplatedItems="True" ExpandAnimation-Duration="1000"
                                            CollapseAnimation-Duration="1000" CollapseAnimation-Type="OutQuart" NoWrap="True"
                                            EnableTheming="True" Font-Italic="False">
                                            <Items>
                                                <telerik:RadComboBoxItem runat="server" Value="1" Text="(GMT-12:00) International dateline, west" />
                                                <telerik:RadComboBoxItem runat="server" Value="2" Text="(GMT-11:00) Midway Islands, Samoan Islands" />
                                                <telerik:RadComboBoxItem runat="server" Value="3" Text="(GMT-10:00) Hawaii" />
                                                <telerik:RadComboBoxItem runat="server" Value="4" Text="(GMT-09:00) Alaska" />
                                                <telerik:RadComboBoxItem runat="server" Value="5" Selected="True" Text="(GMT-08:00) Pacific Time (USA or Canada); Tijuana" />
                                                <telerik:RadComboBoxItem runat="server" Value="6" Text="(GMT-07:00) Mountain Time (USA or Canada)" />
                                                <telerik:RadComboBoxItem runat="server" Value="7" Text="(GMT-06:00) Central time (USA or Canada)" />
                                                <telerik:RadComboBoxItem runat="server" Value="8" Text="(GMT-05:00) Eastern time (USA or Canada)" />
                                                <telerik:RadComboBoxItem runat="server" Value="9" Text="(GMT-04:00) Atlantic Time (Canada)" />
                                                <telerik:RadComboBoxItem runat="server" Value="10" Text="(GMT-03:30) Newfoundland" />
                                                <telerik:RadComboBoxItem runat="server" Value="11" Text="(GMT-03:00) Brasilia" />
                                                <telerik:RadComboBoxItem runat="server" Value="12" Text="(GMT-02:00) Mid-Atlantic" />
                                                <telerik:RadComboBoxItem runat="server" Value="13" Text="(GMT-01:00) Azorerne" />
                                                <telerik:RadComboBoxItem runat="server" Value="14" Text="(GMT) Greenwich Mean Time: Dublin, Edinburgh, Lissabon, London" />
                                                <telerik:RadComboBoxItem runat="server" Value="15" Text="(GMT+01:00) Amsterdam, Berlin, Bern, Rom, Stockholm, Wien" />
                                                <telerik:RadComboBoxItem runat="server" Value="16" Text="(GMT+02:00) Athen, Istanbul, Minsk" />
                                                <telerik:RadComboBoxItem runat="server" Value="17" Text="(GMT+03:00) Moscow, St. Petersburg, Volgograd" />
                                                <telerik:RadComboBoxItem runat="server" Value="18" Text="(GMT+03:30) Teheran" />
                                                <telerik:RadComboBoxItem runat="server" Value="19" Text="(GMT+04:00) Abu Dhabi, Muscat" />
                                                <telerik:RadComboBoxItem runat="server" Value="20" Text="(GMT+04:30) Kabul" />
                                                <telerik:RadComboBoxItem runat="server" Value="21" Text="(GMT+05:00) Islamabad, Karachi, Tasjkent" />
                                                <telerik:RadComboBoxItem runat="server" Value="22" Text="(GMT+05:30) Kolkata, Chennai, Mumbai, New Delhi" />
                                                <telerik:RadComboBoxItem runat="server" Value="23" Text="(GMT+05:45) Katmandu" />
                                                <telerik:RadComboBoxItem runat="server" Value="24" Text="(GMT+06:00) Astana, Dhaka" />
                                                <telerik:RadComboBoxItem runat="server" Value="25" Text="(GMT+06:30) Rangoon" />
                                                <telerik:RadComboBoxItem runat="server" Value="26" Text="(GMT+07:00) Bangkok, Hanoi, Djakarta" />
                                                <telerik:RadComboBoxItem runat="server" Value="27" Text="(GMT+08:00) Beijing, Chongjin, SAR Hongkong, Ürümqi" />
                                                <telerik:RadComboBoxItem runat="server" Value="28" Text="(GMT+09:00) Osaka, Sapporo, Tokyo" />
                                                <telerik:RadComboBoxItem runat="server" Value="29" Text="(GMT+09:30) Adelaide" />
                                                <telerik:RadComboBoxItem runat="server" Value="30" Text="(GMT+10:00) Canberra, Melbourne, Sydney" />
                                                <telerik:RadComboBoxItem runat="server" Value="31" Text="(GMT+11:00) Magadan, Solomon Islands, New Caledonien" />
                                                <telerik:RadComboBoxItem runat="server" Value="32" Text="(GMT+12:00) Fiji, Kamtjatka, Marshall Islands" />
                                                <telerik:RadComboBoxItem runat="server" Value="33" Text="(GMT+13:00) Nuku'alofa" />
                                            </Items>
                                        </telerik:RadComboBox>
                                        <br />
                                        <br />
                                        <asp:CheckBox ID="chkDaylight" runat="server" CssClass="configCheckBox" />&nbsp;
                                        <asp:Label ID="lblTimeInstruct" runat="server" Text="Automatically observe Daylight Saving Time. (Does not apply to all time zones)"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <div class="FormCont Steps" style="margin-bottom: 10px">
                            <table border="0" width="100%">
                                <tr>
                                    <td align="left" colspan="2" style="padding-bottom: 10px;">
                                        <asp:Label ID="lblCap2" runat="server" Font-Bold="True" Text="Client Configuration"
                                            CssClass="frmHeading"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding-left: 10px">
                                        <asp:CheckBox ID="chkConfig4" runat="server" value="4" CssClass="configCheckBox" />&nbsp;My SnapSite
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding-left: 10px">
                                        <asp:CheckBox ID="chkConfig37" runat="server" value="5" CssClass="configCheckBox" />&nbsp;Webinar Invite
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding-left: 10px">
                                        <asp:CheckBox ID="chkConfig7" runat="server" value="7" CssClass="configCheckBox" />&nbsp;Advance Registration Features
                                     </td>
                                </tr>
                                <tr>
                                    <td style="padding-left: 10px">
                                        <asp:CheckBox ID="chkConfig32" runat="server" value="32" CssClass="configCheckBox" />&nbsp;Email Registrant Update
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <div class="FormCont Steps" style="margin-bottom: 10px">
                            <table border="0" width="30%">
                                <tr>
                                    <td align="left" colspan="2" style="padding-bottom: 10px;">
                                        <asp:Label ID="Label1" runat="server" Font-Bold="True" Text="Audience Component Configuration"
                                            CssClass="frmHeading"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <img runat="server" id="cmpBio" src="~/images/features/Bio_1.png" alt="" />
                                    </td>
                                    <td>
                                        <table border="0" style="width: 270px">
                                            <tr>
                                                <td class="style1">
                                                    <asp:CheckBox ID="chkConfig13" runat="server" CssClass="chkGen chkAlign1" Text="Presenter Bio"
                                                        value="" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="padding-left: 23px;">
                                                    Allow audience to read presenter bios
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <%--<td>
                                <img runat="server" id="cmpSlide" src="~/images/features/download.png" alt="Download Slide" />
                            </td>
                            <td>
                                <table border="0" style="width: 270px">
                                    <tr>
                                        <td class="style1">
                                            <asp:CheckBox ID="chkConfig10" runat="server" CssClass="chkGen chkAlign1" Enabled="false"
                                                Text="Download Slides" ForeColor="Black" value="" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding-left: 23px;">
                                            Allow audience to download slides
                                        </td>
                                    </tr>
                                </table>
                            </td>--%>
                                    <td>
                                        <img runat="server" id="cmpChat" src="~/images/features/chat.png" alt="Audience Chat" />
                                    </td>
                                    <td>
                                        <table border="0" width="270px">
                                            <tr>
                                                <td>
                                                    <asp:CheckBox ID="chkConfig11" runat="server" CssClass="chkGen chkAlign1" Text="Audience Chat"
                                                        ForeColor="Black" value="" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="padding-left: 23px;">
                                                    Allow audience to chat with others
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <img runat="server" id="cmpQA" src="~/images/features/QA.png" alt="Submit Question" />
                                    </td>
                                    <td>
                                        <table border="0" style="width: 270px">
                                            <tr>
                                                <td class="style1">
                                                    <asp:CheckBox ID="chkConfig12" runat="server" CssClass="chkGen chkAlign1" Text="Submit Question"
                                                        ForeColor="Black" value="" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="padding-left: 23px;">
                                                    Allow audience to type a question
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>
                                        <img runat="server" id="cmpWiki" src="~/images/features/Wiki.png" alt="Wikipedia" />
                                    </td>
                                    <td>
                                        <table border="0" width="270px">
                                            <tr>
                                                <td>
                                                    <asp:CheckBox ID="chkConfig14" runat="server" CssClass="chkGen chkAlign1" Text="Wikipedia"
                                                        ForeColor="Black" value="" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="padding-left: 23px;">
                                                    Allow audience to use Wikipedia
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <img runat="server" id="cmpContent" src="~/images/features/briefcase.png" alt="Briefcase" />
                                    </td>
                                    <td>
                                        <table border="0" style="width: 270px">
                                            <tr>
                                                <td class="style1">
                                                    <asp:CheckBox ID="chkConfig8" runat="server" CssClass="chkGen chkAlign1" Text="Briefcase"
                                                        value="" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="padding-left: 23px;">
                                                    Allow audience to download content
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>
                                        <img runat="server" id="cmpFB" src="~/images/features/facebook.png" alt="Facebook" />
                                    </td>
                                    <td>
                                        <table border="0" style="width: 270px">
                                            <tr>
                                                <td class="style1">
                                                    <asp:CheckBox ID="chkConfig15" runat="server" CssClass="chkGen chkAlign1" Text="Facebook"
                                                        value="" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="padding-left: 23px;">
                                                    Allow access to designated Facebook page
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <img runat="server" id="cmpGoogle" src="~/images/features/Search.png" alt="Search" />
                                    </td>
                                    <td>
                                        <table border="0" style="width: 270px">
                                            <tr>
                                                <td class="style1">
                                                    <asp:CheckBox ID="chkConfig19" runat="server" CssClass="chkGen chkAlign1" Text="Search"
                                                        value="" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="padding-left: 23px;">
                                                    Allow audience to search the web
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>
                                        <img runat="server" id="cmpTweeter" src="~/images/features/twitter.png" alt="Twitter" />
                                    </td>
                                    <td>
                                        <table border="0" style="width: 270px">
                                            <tr>
                                                <td class="style1">
                                                    <asp:CheckBox ID="chkConfig16" runat="server" CssClass="chkGen chkAlign1" Text="Twitter"
                                                        value="" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="padding-left: 23px;">
                                                    Allow access to designated Twitter page
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <img runat="server" id="cmpFriend" src="~/images/features/Friend.png" alt="Share" />
                                    </td>
                                    <td>
                                        <table border="0" style="width: 270px">
                                            <tr>
                                                <td class="style1">
                                                    <asp:CheckBox ID="chkConfig9" runat="server" CssClass="chkGen chkAlign1" Text="Share"
                                                        value="" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="padding-left: 23px;">
                                                    Allow audience to email webinar
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>
                                        <img runat="server" id="cmpLI" src="~/images/features/linkedIn.png" alt="" />
                                    </td>
                                    <td>
                                        <table border="0" style="width: 270px">
                                            <tr>
                                                <td class="style1">
                                                    <asp:CheckBox ID="chkConfig17" runat="server" CssClass="chkGen chkAlign1" Text="LinkedIn"
                                                        value="" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="padding-left: 23px;">
                                                    Allow access to designated LinkedIn page
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <div class="FormCont Steps" style="margin-bottom: 10px">
                            <table width="70%">
                                <tr>
                                    <td align="left" colspan="2" style="padding-bottom: 10px;">
                                        <asp:Label ID="lblCap4" runat="server" Font-Bold="True" Text="Command Center Configuration"
                                            CssClass="frmHeading"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding-left: 10px">
                                        <asp:CheckBox ID="chkConfig20" runat="server" CssClass="configCheckBox" />&nbsp;Slide
                                        Organizer
                                    </td>
                                    <td style="padding-left: 10px">
                                        <asp:CheckBox ID="chkConfigCC" runat="server" CssClass="configCheckBox" />&nbsp;Add
                                        Content
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding-left: 10px">
                                        <asp:CheckBox ID="chkConfig21" runat="server" CssClass="configCheckBox" />&nbsp;Present Slides
                                    </td>
                                    <td style="padding-left: 25px">
                                        <asp:CheckBox ID="chkConfig27" runat="server" CssClass="configCheckBox" />&nbsp;Video
                                        Clips
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding-left: 10px">
                                        <asp:CheckBox ID="chkConfig22" runat="server" CssClass="configCheckBox" />&nbsp;Audience View
                                    </td>
                                    <td style="padding-left: 25px">
                                        <asp:CheckBox ID="chkConfig28" runat="server" CssClass="configCheckBox" />&nbsp;Polls
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding-left: 10px">
                                        <asp:CheckBox ID="chkConfig34" runat="server" CssClass="configCheckBox" />&nbsp;Who's On
                                    </td>
                                    <td style="padding-left: 25px">
                                        <asp:CheckBox ID="chkConfig29" runat="server" CssClass="configCheckBox" />&nbsp;URLs
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding-left: 10px">
                                        <asp:CheckBox ID="chkConfig35" runat="server" CssClass="configCheckBox" />&nbsp;Q&A
                                    </td>
                                    <td style="padding-left: 25px">
                                        <asp:CheckBox ID="chkConfig30" runat="server" CssClass="configCheckBox" />&nbsp;Test
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding-left: 10px">
                                        <asp:CheckBox ID="chkConfig36" runat="server" CssClass="configCheckBox" />&nbsp;Presenter Chat
                                    </td>
                                    <td style="padding-left: 25px">
                                        <asp:CheckBox ID="chkConfig31" runat="server" CssClass="configCheckBox" />&nbsp;Survey
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding-left: 10px">
                                        <asp:CheckBox ID="chkConfig23" runat="server" CssClass="configCheckBox" />&nbsp;Audience
                                        Chat
                                    </td>
                                    <td style="padding-left: 25px">
                                        
                                    </td>
                                </tr>
                                <%--<tr>
                                    <td style="padding-left: 10px">
                                        <asp:CheckBox ID="chkConfig24" runat="server" CssClass="configCheckBox" />&nbsp;Components
                                    </td>
                                    <td style="padding-left: 25px">
                                        
                                    </td>
                                </tr>--%>
                                <tr>
                                    <td style="padding-left: 10px">
                                        <asp:CheckBox ID="chkConfig25" runat="server" CssClass="configCheckBox" />&nbsp;Desktop Share 
                                    </td>
                                    <td style="padding-left: 25px">
                                        
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding-left: 10px">
                                        <asp:CheckBox ID="chkConfig26" runat="server" CssClass="configCheckBox" />&nbsp;Webcam
                                    </td>
                                    <td style="padding-left: 15px">
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        <script type="text/javascript">
                                $("#ContentPlaceHolder1_clientList1_chkConfigCC").live('click', function (e) {
                                    if ($('#ContentPlaceHolder1_clientList1_chkConfigCC').is(':checked')) {

                                        ($('#ContentPlaceHolder1_clientList1_chkConfig27').attr('checked', true));
                                        ($('#ContentPlaceHolder1_clientList1_chkConfig28').attr('checked', true));
                                        ($('#ContentPlaceHolder1_clientList1_chkConfig29').attr('checked', true));
                                        ($('#ContentPlaceHolder1_clientList1_chkConfig30').attr('checked', true));
                                        ($('#ContentPlaceHolder1_clientList1_chkConfig31').attr('checked', true));
                                    }
                                    else {
                                        ($('#ContentPlaceHolder1_clientList1_chkConfig27').attr('checked', false));
                                        ($('#ContentPlaceHolder1_clientList1_chkConfig28').attr('checked', false));
                                        ($('#ContentPlaceHolder1_clientList1_chkConfig29').attr('checked', false));
                                        ($('#ContentPlaceHolder1_clientList1_chkConfig30').attr('checked', false));
                                        ($('#ContentPlaceHolder1_clientList1_chkConfig31').attr('checked', false));
                                    }
                                });

                                $("#ContentPlaceHolder1_clientList1_chkConfig27").live('click', function (e) {
                                    if ($('#ContentPlaceHolder1_clientList1_chkConfigCC').is(':checked')) {

                                        if ($('#ContentPlaceHolder1_clientList1_chkConfig27').is(':checked')) {
                                            ($('#ContentPlaceHolder1_clientList1_chkConfig27').attr('checked', true));
                                        }
                                        else {
                                            ($('#ContentPlaceHolder1_clientList1_chkConfig27').attr('checked', false));
                                        }

                                    }
                                    else {
                                        ($('#ContentPlaceHolder1_clientList1_chkConfig27').attr('checked', false));
                                    }
                                    setParentCheckState();
                                });

                                $("#ContentPlaceHolder1_clientList1_chkConfig28").live('click', function (e) {
                                    if ($('#ContentPlaceHolder1_clientList1_chkConfigCC').is(':checked')) {

                                        if ($('#ContentPlaceHolder1_clientList1_chkConfig28').is(':checked')) {
                                            ($('#ContentPlaceHolder1_clientList1_chkConfig28').attr('checked', true));
                                        }
                                        else {
                                            ($('#ContentPlaceHolder1_clientList1_chkConfig28').attr('checked', false));
                                        }

                                    }
                                    else {
                                        ($('#ContentPlaceHolder1_clientList1_chkConfig28').attr('checked', false));
                                    }
                                    setParentCheckState();
                                });

                                $("#ContentPlaceHolder1_clientList1_chkConfig29").live('click', function (e) {
                                    
                                    if ($('#ContentPlaceHolder1_clientList1_chkConfigCC').is(':checked')) {

                                        if ($('#ContentPlaceHolder1_clientList1_chkConfig29').is(':checked')) {
                                            ($('#ContentPlaceHolder1_clientList1_chkConfig29').attr('checked', true));
                                        }
                                        else {
                                            ($('#ContentPlaceHolder1_clientList1_chkConfig29').attr('checked', false));
                                        }

                                    }
                                    else {
                                        ($('#ContentPlaceHolder1_clientList1_chkConfig29').attr('checked', false));
                                    }
                                    setParentCheckState();
                                });

                                $("#ContentPlaceHolder1_clientList1_chkConfig30").live('click', function (e) {
                                    if ($('#ContentPlaceHolder1_clientList1_chkConfigCC').is(':checked')) {

                                        if ($('#ContentPlaceHolder1_clientList1_chkConfig30').is(':checked')) {
                                            ($('#ContentPlaceHolder1_clientList1_chkConfig30').attr('checked', true));
                                        }
                                        else {
                                            ($('#ContentPlaceHolder1_clientList1_chkConfig30').attr('checked', false));
                                        }

                                    }
                                    else {
                                        ($('#ContentPlaceHolder1_clientList1_chkConfig30').attr('checked', false));
                                    }
                                    setParentCheckState();
                                });

                                $("#ContentPlaceHolder1_clientList1_chkConfig31").live('click', function (e) {
                                    if ($('#ContentPlaceHolder1_clientList1_chkConfigCC').is(':checked')) {

                                        if ($('#ContentPlaceHolder1_clientList1_chkConfig31').is(':checked')) {
                                            ($('#ContentPlaceHolder1_clientList1_chkConfig31').attr('checked', true));
                                        }
                                        else {
                                            ($('#ContentPlaceHolder1_clientList1_chkConfig31').attr('checked', false));
                                        }

                                    }
                                    else {
                                        ($('#ContentPlaceHolder1_clientList1_chkConfig31').attr('checked', false));
                                    }
                                    setParentCheckState();
                                });
                                function setParentCheckState() {
                                    if ($('#ContentPlaceHolder1_clientList1_chkConfig27').attr('checked') ||
                                        $('#ContentPlaceHolder1_clientList1_chkConfig28').attr('checked') ||
                                        $('#ContentPlaceHolder1_clientList1_chkConfig29').attr('checked') ||
                                        $('#ContentPlaceHolder1_clientList1_chkConfig30').attr('checked') ||
                                        $('#ContentPlaceHolder1_clientList1_chkConfig31').attr('checked')) {

                                        ($('#ContentPlaceHolder1_clientList1_chkConfigCC').attr('checked', true));
                                    }
                                    else {
                                        ($('#ContentPlaceHolder1_clientList1_chkConfigCC').attr('checked', false));
                                    }
                                }
                                </script>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Button ID="btnSave" runat="server" Text="&nbsp;Save&nbsp;" CssClass="SubBtn"
                            OnClick="btnSave_Click" />
                        &nbsp;<i>
                            <asp:Label ID="lblMsg" runat="server" ForeColor="Red"></asp:Label></i>
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

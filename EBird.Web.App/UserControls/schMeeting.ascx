<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="schMeeting.ascx.cs"
    Inherits="EBird.Web.App.UserControls.schMeeting" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="webTheme.ascx" TagName="webTheme" TagPrefix="uc1" %>
<%@ Register Src="webAudience.ascx" TagName="webAudience" TagPrefix="uc2" %>
<%@ Register Src="webRegistration.ascx" TagName="webRegistration" TagPrefix="uc3" %>
<%@ Register Src="webEmail.ascx" TagName="webEmail" TagPrefix="uc4" %>
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
        <table width="100%">
            <tr>
                <td>
                    <div class="ui-widget" runat="server" id="dvValidationMsg" visible="false">
                        <div class="ui-state-highlight ui-corner-all" style="margin-top: 0px; height: 21px">
                            <p class="warningMsg">
                                <strong>Warning!</strong>&nbsp;&nbsp;<asp:Label ID="lblValidationMsg" runat="server"></asp:Label>
                            </p>
                        </div>
                    </div>
                </td>
            </tr>
        </table>
        <div class="Steps2" runat="server" id="dvStep">
            <ul id="frmPage">
                <li id="li1" runat="server" class="One1 Current" onclick="javascript:__doPostBack('ctl00$ContentPlaceHolder1$schMeeting1$lnkSchWebinar','');">
                    <asp:LinkButton ID="lnkSchWebinar" runat="server" Text="Schedule" OnClick="lnkSchWebinar_Click"
                        CausesValidation="false"></asp:LinkButton>
                </li>
                <%--                <li id="li2" runat="server" class="Two">
                    <asp:LinkButton ID="lnkTheme" runat="server" Text="Brand & Style" OnClick="lnkTheme_Click"
                        CausesValidation="false"></asp:LinkButton>
                </li>
                --%>
                <li id="li2" runat="server" class="Two" onclick="javascript:__doPostBack('ctl00$ContentPlaceHolder1$schMeeting1$lnkSetupReg','');">
                    <asp:LinkButton ID="lnkSetupReg" runat="server" Text="Registration" OnClick="lnkSetupReg_Click"
                        CausesValidation="false"></asp:LinkButton>
                </li>
                <li id="li3" runat="server" class="Three" onclick="javascript:__doPostBack('ctl00$ContentPlaceHolder1$schMeeting1$lnkAudView','');">
                    <asp:LinkButton ID="lnkAudView" runat="server" Text="Audience Interface" OnClick="lnkAudView_Click"
                        CausesValidation="false"></asp:LinkButton>
                </li>
                <li id="li4" runat="server" class="Four" onclick="javascript:__doPostBack('ctl00$ContentPlaceHolder1$schMeeting1$lnkEmailNotify','');">
                    <asp:LinkButton ID="lnkEmailNotify" runat="server" Text="Email Notification" OnClick="lnkEmailNotify_Click"
                        CausesValidation="false"></asp:LinkButton>
                </li>
            </ul>
            <div class="Clr">
            </div>
        </div>
        <table width="100%" align="center">
            <tr>
                <td>
                    <div id="fvWebTitle" runat="server" visible="false">
                        <table border="0" style="padding: 10px 5px 10px 5px; width: 100%">
                            <tr>
                                <td align="left">
                                    <asp:Label CssClass="WebinarTitle" ID="lblWebinarTitle" runat="server"></asp:Label>
                                </td>
                                <td align="right">
                                    <asp:HiddenField ID="hWebinarID" runat="server" Value="0" />
                                    <asp:HiddenField ID="hWebinarStatus" runat="server" Value="" />
                                    <asp:HiddenField ID="hIsPast" runat="server" Value="0" />
                                    <telerik:RadComboBox ID="rcmbActions" runat="server" Width="150" AutoPostBack="True"
                                        CausesValidation="False" Visible="false" OnSelectedIndexChanged="rcmbActions_SelectedIndexChanged"
                                        MarkFirstMatch="True" HighlightTemplatedItems="True" ExpandAnimation-Duration="1000"
                                        CollapseAnimation-Duration="1000" CollapseAnimation-Type="OutQuart" NoWrap="True"
                                        EnableTheming="True" Font-Italic="False" Skin="Default" EmptyMessage="Webinar Actions">
                                        <Items>
                                            <telerik:RadComboBoxItem runat="server" Text="View Webinar URLs" Value="URL" CssClass="ddStyle1" />
                                            <telerik:RadComboBoxItem runat="server" Text="Schedule Same Webinar" Value="SCH"
                                                CssClass="ddStyle1" />
                                            <telerik:RadComboBoxItem runat="server" Text="Email Registrants" Value="EML" CssClass="ddStyle1" />
                                            <telerik:RadComboBoxItem runat="server" Text="Add to Outlook Calendar" Value="OUT"
                                                CssClass="ddStyle1" />
                                            <telerik:RadComboBoxItem runat="server" Text="Cancel Webinar (Inactive)" Value="CAN"
                                                CssClass="ddStyle1" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
        </table>
        <div class="FormCont">
            <table class="tblNotify">
                <tr>
                    <td valign="top">
                        <asp:MultiView ID="mvSchedule" runat="server">
                            <asp:View ID="vwWeb" runat="server">
                                <table width="100%" align="center">
                                    <tr>
                                        <td colspan="2" valign="top">
                                            <div class="FormCont Steps" style="padding-bottom: 20px; margin-bottom: 10px">
                                                <table width="100%">
                                                    <tr>
                                                        <td align="left" style="padding-bottom: 14px;">
                                                            <asp:Label ID="lblConfirmEmail" runat="server" Font-Bold="True" Text="Webinar Details"
                                                                CssClass="frmHeading" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td valign="top" width="15%" style="padding-left: 5px">
                                                            <asp:Label ID="lblTitle" runat="server" Text="Webinar Title"></asp:Label><span class='EBmsg'>&nbsp;*</span>
                                                        </td>
                                                        <td class="Row LongText" width="85%">
                                                            <asp:TextBox ID="txtWebinarTitle" runat="server" MaxLength="120" Width="572" CssClass="textbox_EB"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="vx_txtWebinarTitle" runat="server" CssClass="ValidationSummary"
                                                                ControlToValidate="txtWebinarTitle" ErrorMessage="Please enter required field - Webinar Title"
                                                                ForeColor="Red" Display="None" Text="*" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td valign="top" style="padding-left: 5px">
                                                            <asp:Label ID="lblDescription" runat="server" Text="Webinar Summary"></asp:Label><span
                                                                class='EBmsg'>&nbsp;*</span>
                                                        </td>
                                                        <td class="Row LongText">
                                                            <div id="dvDesc">
                                                                <telerik:RadEditor runat="server" ID="redtSummary" ToolbarMode="Default" Height="180px"
                                                                    ToolsFile="~/editor/BasicTools2.xml" BorderStyle="None" CssClass="rteditor1"
                                                                    ToolsWidth="100%" EnableResize="False" EditModes="Design" Width="581px">
                                                                    <CssFiles>
                                                                        <telerik:EditorCssFile Value="~/Styles/RTEditor.css" />
                                                                    </CssFiles>
                                                                    <ContextMenus>
                                                                        <telerik:EditorContextMenu Enabled="false" />
                                                                        <telerik:EditorContextMenu TagName="IMG" Enabled="false" />
                                                                        <telerik:EditorContextMenu TagName="A" Enabled="false" />
                                                                        <telerik:EditorContextMenu TagName="TABLE" Enabled="false" />
                                                                    </ContextMenus>
                                                                </telerik:RadEditor>
                                                            </div>
                                                            <asp:RequiredFieldValidator ID="vx_txtDescription" runat="server" CssClass="ValidationSummary"
                                                                ControlToValidate="redtSummary" ErrorMessage="Please enter required field - Description"
                                                                ForeColor="Red" Display="None" Text="*" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td valign="top" style="padding-left: 5px">
                                                            <asp:Label ID="lblStartDate" runat="server">Start Date<span class='EBmsg'>&nbsp;*</span></asp:Label>
                                                        </td>
                                                        <td class="Row LongText">
                                                            <telerik:RadDatePicker ID="rdtStartDate" runat="server" Height="23" Width="100" Skin="Default"
                                                                AutoPostBack="False" HideAnimation-Duration="800" ShowAnimation-Duration="800"
                                                                Calendar-ClientEvents-OnLoad="rdtDate_SetMaxDateToCurrentDate">
                                                            </telerik:RadDatePicker>
                                                            <asp:RequiredFieldValidator ID="vx_rdtStartDate" runat="server" CssClass="ValidationSummary"
                                                                ControlToValidate="rdtStartDate" ErrorMessage="Please enter required field - Start Date"
                                                                ForeColor="Red" Display="None" Text="*" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="padding-left: 5px">
                                                            <asp:Label ID="lblStartTime" runat="server">Start Time<span class='EBmsg'>&nbsp;*</span></asp:Label>
                                                        </td>
                                                        <td class="Row LongText">
                                                            <telerik:RadTimePicker ID="rdtStartTime" runat="server" Height="23" Width="100" Skin="Default"
                                                                ShowAnimation-Type="Fade" AutoPostBack="False" ShowAnimation-Duration="800" HideAnimation-Duration="800">
                                                                <DateInput ID="stTimeInput" runat="server">
                                                                    <ClientEvents OnLoad="onLoadRadStartTime" />
                                                                </DateInput>
                                                                <TimeView ID="tvStartTime" ShowHeader="False" Interval="00:30:00"
                                                                    Columns="6" runat="server" OnClientTimeSelected="stClientTimeSelected">
                                                                </TimeView>
                                                            </telerik:RadTimePicker>
                                                            <asp:RequiredFieldValidator ID="vx_rdtStartTime" runat="server" CssClass="ValidationSummary"
                                                                ControlToValidate="rdtStartTime" ErrorMessage="Please enter required field - Start Time"
                                                                ForeColor="Red" Display="None" Text="*" />
                                                            &nbsp;
                                                            <telerik:RadComboBox ID="ddlTimeZone" runat="server" ShowDropDownOnTextboxClick="True"
                                                                MarkFirstMatch="True" HighlightTemplatedItems="True" ExpandAnimation-Duration="1000"
                                                                CollapseAnimation-Duration="1000" CollapseAnimation-Type="OutQuart" NoWrap="True"
                                                                EnableTheming="True" Font-Italic="False" Skin="Default" Width="464">
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
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="padding-left: 5px">
                                                            <asp:Label ID="lblEndTime" runat="server">End Time<span class='EBmsg'>&nbsp;*</span></asp:Label>
                                                        </td>
                                                        <td>
                                                            <telerik:RadTimePicker ID="rdEndTime" runat="server" Height="23" Width="100" Skin="Default"
                                                                ShowAnimation-Type="Fade" AutoPostBack="False" ShowAnimation-Duration="800" HideAnimation-Duration="800">
                                                                <DateInput ID="enTimeInput" runat="server">
                                                                    <ClientEvents OnLoad="onLoadRadEndTime" />
                                                                </DateInput>
                                                                <TimeView ID="tvEndTime" ShowHeader="False" Interval="00:30:00"
                                                                    Columns="6" runat="server">
                                                                </TimeView>
                                                            </telerik:RadTimePicker>
                                                            <asp:RequiredFieldValidator ID="vx_rdEndTime" runat="server" CssClass="ValidationSummary"
                                                                ControlToValidate="rdEndTime" ErrorMessage="Please enter required field - End Time"
                                                                ForeColor="Red" Display="None" Text="*" />
                                                            <asp:CustomValidator ID="CustomValidator1" EnableClientScript="true" runat="server"
                                                                ControlToValidate="rdEndTime" ClientValidationFunction="validate" />
                                                            <asp:CustomValidator ID="CustomValidator2" EnableClientScript="true" runat="server"
                                                                ControlToValidate="rdtStartTime" ClientValidationFunction="validate" />
                                                        </td>
                                                    </tr>
                                                </table>
                                                <script type="text/javascript">
                                                    var date = new Date();
                                                    function rdtDate_SetMaxDateToCurrentDate(sender, args) {
                                                        var arr = new Array(date.getFullYear(), date.getMonth() + 1, date.getDate());
                                                        sender.set_rangeMinDate(arr);
                                                    }   
                                                </script>
                                                <script type="text/javascript">
                                                //<![CDATA[
                                                    var rdtStartTime;
                                                    var rdEndTime;

                                                    function validate(sender, args) {
                                                        //var stPicker = $find("<%=rdtStartTime.ClientID%>");
                                                        //var view = stPicker.get_timeView();
                                                        //var textBox = stPicker.get_textBox();
                                                        //alert("Text -> " + textBox.value);
                                                        //alert("Timeview -> " + view.getTime());

                                                        var stDate = $find('<%=rdtStartDate.ClientID %>');
                                                        var stTime = $find('<%=rdtStartTime.ClientID %>');
                                                        var enTime = $find('<%=rdEndTime.ClientID %>');

                                                        var Date1 = new Date(stDate.get_textBox().value + ' ' + stTime.get_textBox().value);
                                                        var Date2 = new Date(stDate.get_textBox().value + ' ' + enTime.get_textBox().value);

                                                        // following values bring date part as well and sometime wrong datepart

                                                        //var Date1 = new Date(rdtStartTime.get_selectedDate());
                                                        //var Date2 = new Date(rdEndTime.get_selectedDate());

                                                        if (stTime.get_textBox().value != "" && enTime.get_textBox().value != "") {
                                                            args.IsValid = true;
                                                            if ((Date2 - Date1) < 0) {
                                                                alert("The end time should be greater than the start time!");
                                                                rdEndTime.clear();
                                                                args.IsValid = false;
                                                            }
                                                        }
                                                    }

                                                    function isTodayisSelected() {
                                                        var one_day = 1000 * 60 * 60 * 24;
                                                        //var hours_conv = 1000 * 60 * 60;
                                                        if (stDate.get_textBox().value != '') {
                                                            var _Diff = Math.ceil((stDate.get_textBox().value - date.getDate()) / (one_day));
                                                            if (_Diff == "0")
                                                                return true;
                                                            else
                                                                return false;
                                                        }
                                                        return false;
                                                    }

                                                    function onLoadRadStartTime(sender, args) {
                                                        rdtStartTime = sender;
                                                        var now = new Date();
                                                        var currDate = new Date(now.getTime());
                                                    }

                                                    function onLoadRadEndTime(sender, args) {
                                                        rdEndTime = sender;
                                                    }

                                                    function stClientTimeSelected(sender, e) {
                                                        var oldTime = e.get_oldTime() ? (e.get_oldTime().getHours() + ":" + e.get_oldTime().getMinutes()) : "";
                                                        var newTime = e.get_newTime() ? (e.get_newTime().getHours() + ":" + e.get_newTime().getMinutes()) : "";
                                                    }

                                                     //]]>
                                                </script>
                                                <img src="../Images/blank.gif" height="10" />
                                                <table width="100%">
                                                    <tr>
                                                        <td height="24" style="padding-left: 5px;">
                                                            <asp:CheckBox ID="chkRecurrence" CssClass="configCheckBox" runat="server" />&nbsp;
                                                            <asp:Label ID="lblRec" runat="server" Text="Recurrence"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="padding-left: 5px; padding-bottom: 5px;">
                                                            <asp:Panel ID="pnlRecurr" runat="server">
                                                                <table width="100%" style="border: 1px solid #cecece;" bgcolor="#ffffff">
                                                                    <tr>
                                                                        <td width="18%" style="border-right: 1px solid #e0e0e0;">
                                                                            <asp:RadioButtonList ID="rbtnDurationType" runat="server" RepeatDirection="Vertical"
                                                                                CssClass="rbtnColl1" CellSpacing="5">
                                                                                <%--<asp:ListItem Text="&nbsp;&nbsp;Hourly" Value="H" Selected="True"></asp:ListItem>--%>
                                                                                <asp:ListItem Text="&nbsp;&nbsp;Daily" Value="D" Selected="True"></asp:ListItem>
                                                                                <asp:ListItem Text="&nbsp;&nbsp;Weekly" Value="W"></asp:ListItem>
                                                                                <asp:ListItem Text="&nbsp;&nbsp;Monthly" Value="M"></asp:ListItem>
                                                                                <asp:ListItem Text="&nbsp;&nbsp;Yearly" Value="Y"></asp:ListItem>
                                                                            </asp:RadioButtonList>
                                                                        </td>
                                                                        <td width="82%" valign="top">
                                                                           <%-- <div id="dvehour" runat="server">
                                                                                <table width="100%" cellpadding="10">
                                                                                    <tr>
                                                                                        <td width="5%" style="padding-left: 10px; padding-top: 10px;">
                                                                                            Every
                                                                                            <telerik:RadComboBox ID="rcmbHourly" runat="server" CssClass="rad-combo" Width="40">
                                                                                                <Items>
                                                                                                    <telerik:RadComboBoxItem runat="server" Value="1" Text="1" />
                                                                                                    <telerik:RadComboBoxItem runat="server" Value="2" Text="2" />
                                                                                                    <telerik:RadComboBoxItem runat="server" Value="3" Text="3" />
                                                                                                    <telerik:RadComboBoxItem runat="server" Value="4" Text="4" />
                                                                                                </Items>
                                                                                            </telerik:RadComboBox>
                                                                                            &nbsp;hours
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </div>--%>
                                                                            <div id="dveday" runat="server">
                                                                                <table width="100%">
                                                                                    <tr>
                                                                                        <td style="padding-left: 10px; padding-top: 10px;">
                                                                                            <asp:RadioButton ID="rbtnEday" GroupName="optEveryDay" Checked="true" runat="server" />&nbsp;Every
                                                                                            <telerik:RadComboBox ID="rcmbEDay" runat="server" CssClass="rad-combo" Width="40">
                                                                                                <Items>
                                                                                                    <telerik:RadComboBoxItem runat="server" Value="1" Text="1" />
                                                                                                    <telerik:RadComboBoxItem runat="server" Value="2" Text="2" />
                                                                                                    <telerik:RadComboBoxItem runat="server" Value="3" Text="3" />
                                                                                                    <telerik:RadComboBoxItem runat="server" Value="4" Text="4" />
                                                                                                </Items>
                                                                                            </telerik:RadComboBox>
                                                                                            &nbsp;Day
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td style="padding-left: 10px; padding-top: 5px;">
                                                                                            <asp:RadioButton ID="rbtnEW" GroupName="optEveryDay" runat="server" />&nbsp;Every
                                                                                            Weekday
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </div>
                                                                            <div id="dveweek" style="display: none;" runat="server">
                                                                                <table width="100%">
                                                                                    <tr>
                                                                                        <td style="padding-left: 10px; padding-top: 10px;">
                                                                                            Recur every
                                                                                            <telerik:RadComboBox ID="rcmbRecurE" runat="server" CssClass="rad-combo" Width="40">
                                                                                                <Items>
                                                                                                    <telerik:RadComboBoxItem runat="server" Value="1" Text="1" />
                                                                                                    <telerik:RadComboBoxItem runat="server" Value="2" Text="2" />
                                                                                                    <telerik:RadComboBoxItem runat="server" Value="3" Text="3" />
                                                                                                    <telerik:RadComboBoxItem runat="server" Value="4" Text="4" />
                                                                                                </Items>
                                                                                            </telerik:RadComboBox>
                                                                                            &nbsp;weeks on
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td style="padding-left: 10px; padding-top: 0px;">
                                                                                            <asp:CheckBoxList ID="chkWkDay" runat="server" RepeatDirection="Horizontal" RepeatColumns="4"
                                                                                                CellPadding="10" CellSpacing="10" CssClass="chkAlign11">
                                                                                                <asp:ListItem Text="Sunday" Value="0"></asp:ListItem>
                                                                                                <asp:ListItem Text="Monday" Value="1"></asp:ListItem>
                                                                                                <asp:ListItem Text="Tuesday" Value="2"></asp:ListItem>
                                                                                                <asp:ListItem Text="Wednesday" Value="3"></asp:ListItem>
                                                                                                <asp:ListItem Text="Thursday" Value="4"></asp:ListItem>
                                                                                                <asp:ListItem Text="Friday" Value="5"></asp:ListItem>
                                                                                                <asp:ListItem Text="Saturday" Value="6"></asp:ListItem>
                                                                                            </asp:CheckBoxList>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </div>
                                                                            <div id="dvemonth" style="display: none;" runat="server">
                                                                                <table width="100%">
                                                                                    <tr>
                                                                                        <td style="padding-left: 10px; padding-top: 10px;">
                                                                                            <asp:RadioButton ID="optMDay" GroupName="optMgrp" Checked="true" runat="server" />
                                                                                            Day
                                                                                            <telerik:RadComboBox ID="rcmbMDay" runat="server" CssClass="rad-combo" Width="40">
                                                                                                <Items>
                                                                                                    <telerik:RadComboBoxItem runat="server" Value="1" Text="1" />
                                                                                                    <telerik:RadComboBoxItem runat="server" Value="2" Text="2" />
                                                                                                    <telerik:RadComboBoxItem runat="server" Value="3" Text="3" />
                                                                                                    <telerik:RadComboBoxItem runat="server" Value="4" Text="4" />
                                                                                                </Items>
                                                                                            </telerik:RadComboBox>
                                                                                            &nbsp;of every&nbsp;
                                                                                            <telerik:RadComboBox ID="rcmbMDay1" runat="server" CssClass="rad-combo" Width="40">
                                                                                                <Items>
                                                                                                    <telerik:RadComboBoxItem runat="server" Value="1" Text="1" />
                                                                                                    <telerik:RadComboBoxItem runat="server" Value="2" Text="2" />
                                                                                                    <telerik:RadComboBoxItem runat="server" Value="3" Text="3" />
                                                                                                    <telerik:RadComboBoxItem runat="server" Value="4" Text="4" />
                                                                                                </Items>
                                                                                            </telerik:RadComboBox>
                                                                                            &nbsp;months
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td style="padding-left: 10px">
                                                                                            <asp:RadioButton ID="optMDay1" GroupName="optMgrp" runat="server" />
                                                                                            The&nbsp;
                                                                                            <telerik:RadComboBox ID="rcmbMTheDay" runat="server" CssClass="rad-combo" Width="60">
                                                                                                <Items>
                                                                                                    <telerik:RadComboBoxItem runat="server" Value="1" Text="first" />
                                                                                                    <telerik:RadComboBoxItem runat="server" Value="2" Text="second" />
                                                                                                    <telerik:RadComboBoxItem runat="server" Value="3" Text="third" />
                                                                                                    <telerik:RadComboBoxItem runat="server" Value="4" Text="fourth" />
                                                                                                    <telerik:RadComboBoxItem runat="server" Value="l" Text="last" />
                                                                                                </Items>
                                                                                            </telerik:RadComboBox>
                                                                                            <telerik:RadComboBox ID="rcmbMTheTyp" runat="server" CssClass="rad-combo" Width="75">
                                                                                                <Items>
                                                                                                    <telerik:RadComboBoxItem runat="server" Value="D" Text="day" />
                                                                                                    <telerik:RadComboBoxItem runat="server" Value="0" Text="Sunday" />
                                                                                                    <telerik:RadComboBoxItem runat="server" Value="1" Text="Monday" />
                                                                                                    <telerik:RadComboBoxItem runat="server" Value="2" Text="Tuesday" />
                                                                                                    <telerik:RadComboBoxItem runat="server" Value="3" Text="Wednesday" />
                                                                                                    <telerik:RadComboBoxItem runat="server" Value="4" Text="Thursday" />
                                                                                                    <telerik:RadComboBoxItem runat="server" Value="5" Text="Friday" />
                                                                                                    <telerik:RadComboBoxItem runat="server" Value="6" Text="Saturday" />
                                                                                                </Items>
                                                                                            </telerik:RadComboBox>
                                                                                            &nbsp;of every&nbsp;
                                                                                            <telerik:RadComboBox ID="rcmbM1" runat="server" CssClass="rad-combo" Width="40">
                                                                                                <Items>
                                                                                                    <telerik:RadComboBoxItem runat="server" Value="1" Text="1" />
                                                                                                    <telerik:RadComboBoxItem runat="server" Value="2" Text="2" />
                                                                                                </Items>
                                                                                            </telerik:RadComboBox>
                                                                                            &nbsp;months
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </div>
                                                                            <div id="dveyear" style="display: none;" runat="server">
                                                                                <table width="100%">
                                                                                    <tr>
                                                                                        <td style="padding-left: 10px; padding-top: 10px;">
                                                                                            <asp:RadioButton ID="optY" GroupName="optYgrp" Checked="true" runat="server" />
                                                                                            Every
                                                                                            <telerik:RadComboBox ID="rcmbY" runat="server" CssClass="rad-combo" Width="75">
                                                                                                <Items>
                                                                                                    <telerik:RadComboBoxItem runat="server" Value="1" Text="January" />
                                                                                                    <telerik:RadComboBoxItem runat="server" Value="2" Text="Febrary" />
                                                                                                    <telerik:RadComboBoxItem runat="server" Value="3" Text="March" />
                                                                                                    <telerik:RadComboBoxItem runat="server" Value="4" Text="April" />
                                                                                                    <telerik:RadComboBoxItem runat="server" Value="5" Text="May" />
                                                                                                    <telerik:RadComboBoxItem runat="server" Value="6" Text="June" />
                                                                                                    <telerik:RadComboBoxItem runat="server" Value="7" Text="July" />
                                                                                                    <telerik:RadComboBoxItem runat="server" Value="8" Text="August" />
                                                                                                    <telerik:RadComboBoxItem runat="server" Value="9" Text="September" />
                                                                                                    <telerik:RadComboBoxItem runat="server" Value="10" Text="Octpber" />
                                                                                                    <telerik:RadComboBoxItem runat="server" Value="11" Text="November" />
                                                                                                    <telerik:RadComboBoxItem runat="server" Value="12" Text="December" />
                                                                                                </Items>
                                                                                            </telerik:RadComboBox>
                                                                                            <telerik:RadComboBox ID="rcmbYMonthday" runat="server" CssClass="rad-combo" Width="40">
                                                                                                <Items>
                                                                                                    <telerik:RadComboBoxItem runat="server" Value="1" Text="1" />
                                                                                                    <telerik:RadComboBoxItem runat="server" Value="2" Text="2" />
                                                                                                    <telerik:RadComboBoxItem runat="server" Value="3" Text="3" />
                                                                                                    <telerik:RadComboBoxItem runat="server" Value="4" Text="4" />
                                                                                                </Items>
                                                                                            </telerik:RadComboBox>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td style="padding-left: 10px">
                                                                                            <asp:RadioButton ID="optY1" GroupName="optYgrp" runat="server" />
                                                                                            The&nbsp;
                                                                                            <telerik:RadComboBox ID="rcmbYTheDay" runat="server" CssClass="rad-combo" Width="50">
                                                                                                <Items>
                                                                                                    <telerik:RadComboBoxItem runat="server" Value="1" Text="first" />
                                                                                                    <telerik:RadComboBoxItem runat="server" Value="2" Text="second" />
                                                                                                    <telerik:RadComboBoxItem runat="server" Value="3" Text="third" />
                                                                                                    <telerik:RadComboBoxItem runat="server" Value="4" Text="fourth" />
                                                                                                    <telerik:RadComboBoxItem runat="server" Value="l" Text="last" />
                                                                                                </Items>
                                                                                            </telerik:RadComboBox>
                                                                                            <telerik:RadComboBox ID="rcmbYTheTyp" runat="server" CssClass="rad-combo" Width="80">
                                                                                                <Items>
                                                                                                    <telerik:RadComboBoxItem runat="server" Value="D" Text="day" />
                                                                                                    <telerik:RadComboBoxItem runat="server" Value="0" Text="Sunday" />
                                                                                                    <telerik:RadComboBoxItem runat="server" Value="1" Text="Monday" />
                                                                                                    <telerik:RadComboBoxItem runat="server" Value="2" Text="Tuesday" />
                                                                                                    <telerik:RadComboBoxItem runat="server" Value="3" Text="Wednesday" />
                                                                                                    <telerik:RadComboBoxItem runat="server" Value="4" Text="Thursday" />
                                                                                                    <telerik:RadComboBoxItem runat="server" Value="5" Text="Friday" />
                                                                                                    <telerik:RadComboBoxItem runat="server" Value="6" Text="Saturday" />
                                                                                                </Items>
                                                                                            </telerik:RadComboBox>
                                                                                            &nbsp;of&nbsp;
                                                                                            <telerik:RadComboBox ID="rcmbMTheMonth" runat="server" CssClass="rad-combo" Width="70">
                                                                                                <Items>
                                                                                                    <telerik:RadComboBoxItem runat="server" Value="1" Text="January" />
                                                                                                    <telerik:RadComboBoxItem runat="server" Value="2" Text="Febrary" />
                                                                                                    <telerik:RadComboBoxItem runat="server" Value="3" Text="March" />
                                                                                                    <telerik:RadComboBoxItem runat="server" Value="4" Text="April" />
                                                                                                    <telerik:RadComboBoxItem runat="server" Value="5" Text="May" />
                                                                                                    <telerik:RadComboBoxItem runat="server" Value="6" Text="June" />
                                                                                                    <telerik:RadComboBoxItem runat="server" Value="7" Text="July" />
                                                                                                    <telerik:RadComboBoxItem runat="server" Value="8" Text="August" />
                                                                                                    <telerik:RadComboBoxItem runat="server" Value="9" Text="September" />
                                                                                                    <telerik:RadComboBoxItem runat="server" Value="10" Text="Octpber" />
                                                                                                    <telerik:RadComboBoxItem runat="server" Value="11" Text="November" />
                                                                                                    <telerik:RadComboBoxItem runat="server" Value="12" Text="December" />
                                                                                                </Items>
                                                                                            </telerik:RadComboBox>
                                                                                            &nbsp;months
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </div>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                                <img src="../Images/blank.gif" height="10" alt="" />
                                                                <table width="100%" style="border: 1px solid #cecece; padding-top: 3px; padding-left: 5px;"
                                                                    bgcolor="#ffffff">
                                                                    <tr>
<%--                                                                        <td valign="middle" width="30%">
                                                                            &nbsp;
                                                                            <asp:RadioButton ID="rbtnNoEnddate" CssClass="rbtnColl1" Text="&nbsp;&nbsp;No end date"
                                                                                runat="server" GroupName="endDates" Checked="true" />
                                                                        </td>--%>
                                                                        <td valign="middle" width="30%">
                                                                            <asp:RadioButton ID="rbtnEndAfter" CssClass="rbtnColl1 enrbtnColl1" Text="&nbsp;&nbsp;End after"
                                                                                runat="server" GroupName="endDates" Checked />
                                                                            <asp:TextBox ID="txtEndAfter" CssClass="spintxtbox" runat="server" Width="40"></asp:TextBox>
                                                                        </td>
                                                                        <td valign="middle" width="40%">
                                                                            <asp:RadioButton ID="rbtnEndBy" CssClass="rbtnColl1 enrbtnColl1" runat="server" Text="&nbsp;End by"
                                                                                GroupName="endDates" />
                                                                            <telerik:RadDatePicker ID="rdtEndBy" runat="server" CssClass="endcalpos" Height="20"
                                                                                Width="100" Skin="Default" AutoPostBack="False" HideAnimation-Duration="800"
                                                                                ShowAnimation-Duration="800" Calendar-ClientEvents-OnLoad="rdtDate_SetMaxDateToCurrentDate">
                                                                            </telerik:RadDatePicker>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                                <script src="../js/jquery-spin.js"></script>
                                                                <script>
//                                                                    $("#ContentPlaceHolder1_schMeeting1_rbtnDurationType_0").click(function () {
//                                                                        if ($('#ContentPlaceHolder1_schMeeting1_rbtnDurationType_0').is(':checked'))
//                                                                            showOnlyRadioDiv("#ContentPlaceHolder1_schMeeting1_dvehour");
//                                                                    });
                                                                    $("#ContentPlaceHolder1_schMeeting1_rbtnDurationType_0").click(function () {
                                                                        if ($('#ContentPlaceHolder1_schMeeting1_rbtnDurationType_0').is(':checked'))
                                                                            showOnlyRadioDiv("#ContentPlaceHolder1_schMeeting1_dveday");
                                                                    });
                                                                    $("#ContentPlaceHolder1_schMeeting1_rbtnDurationType_1").click(function () {
                                                                        if ($('#ContentPlaceHolder1_schMeeting1_rbtnDurationType_1').is(':checked'))
                                                                            showOnlyRadioDiv("#ContentPlaceHolder1_schMeeting1_dveweek");
                                                                    });
                                                                    $("#ContentPlaceHolder1_schMeeting1_rbtnDurationType_2").click(function () {
                                                                        if ($('#ContentPlaceHolder1_schMeeting1_rbtnDurationType_2').is(':checked'))
                                                                            showOnlyRadioDiv("#ContentPlaceHolder1_schMeeting1_dvemonth");
                                                                    });
                                                                    $("#ContentPlaceHolder1_schMeeting1_rbtnDurationType_3").click(function () {
                                                                        if ($('#ContentPlaceHolder1_schMeeting1_rbtnDurationType_3').is(':checked'))
                                                                            showOnlyRadioDiv("#ContentPlaceHolder1_schMeeting1_dveyear");
                                                                    });
                                                                    function showOnlyRadioDiv(rdo) {
                                                                        //$("#ContentPlaceHolder1_schMeeting1_dvehour").hide();
                                                                        $("#ContentPlaceHolder1_schMeeting1_dveday").hide();
                                                                        $("#ContentPlaceHolder1_schMeeting1_dveweek").hide();
                                                                        $("#ContentPlaceHolder1_schMeeting1_dvemonth").hide();
                                                                        $("#ContentPlaceHolder1_schMeeting1_dveyear").hide();
                                                                        $(rdo).show('fast');
                                                                    }
                                                                    $(document).ready(function () {
                                                                        $('#everyhour').spin();
                                                                        $('#everyday').spin();
                                                                        $('#everyweek').spin();
                                                                        $('#everymonth').spin();
                                                                        $('#everymonth-count').spin();
                                                                        $('#yrecur-day').spin();
                                                                        $('#mrecur-daycount').spin();
                                                                        $('#ContentPlaceHolder1_schMeeting1_txtEndAfter').spin();
                                                                    });
                                                                </script>
                                                                <style>
                                                                    #ContentPlaceHolder1_schMeeting1_txtEndAfter ~ img
                                                                    {
                                                                        position: relative;
                                                                        top: -2px;
                                                                        width: 15px;
                                                                    }
                                                                    .enrbtnColl1
                                                                    {
                                                                        margin-top: 4px;
                                                                    }
                                                                    .enrbtnColl1 label
                                                                    {
                                                                        width: 60px;
                                                                    }
                                                                    .endcalpos
                                                                    {
                                                                        position: relative;
                                                                        left: -14px;
                                                                        top: -2px;
                                                                    }
                                                                    .rbtnColl1
                                                                    {
                                                                        white-space: normal;
                                                                    }
                                                                    .spintxtbox
                                                                    {
                                                                        border: 1px solid #999;
                                                                        height: 18px;
                                                                    }
                                                                    .daysalglf td
                                                                    {
                                                                        float: left;
                                                                    }
                                                                </style>
                                                            </asp:Panel>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <div class="right">
                                                    <font color="#A00000">*</font><i><small>Required field</small></i>
                                                </div>
                                                <script type="text/javascript">
                                                    jQuery(function ($) {
                                                        // Load dialog on click - recurrence
                                                        $("#ContentPlaceHolder1_schMeeting1_chkRecurrence").live('click', function (e) {
                                                            var isChk = 0;
                                                            var pnl = document.getElementById("ContentPlaceHolder1_schMeeting1_pnlRecurr");

                                                            if ($('#ContentPlaceHolder1_schMeeting1_chkRecurrence').is(':checked'))
                                                                pnl.style.display = "block";
                                                            else
                                                                pnl.style.display = "none";
                                                        });
                                                    });

                                                    function validateRecurrence() {
                                                        if ($('#ContentPlaceHolder1_schMeeting1_chkRecurrence').is(':checked')) {
                                                            var radioButtonlist = document.getElementsByName("<%=rbtnDurationType.ClientID%>");
                                                            alert(radioButtonlist[0].value);
                                                            for (var x = 0; x < radioButtonlist.length; x++) {
                                                                if (radioButtonlist[x].checked) {
                                                                    alert("Selected item Value " + radioButtonlist[x].value);
                                                                }
                                                            }
                                                            return false;
                                                        }
                                                    }
                                                </script>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <div class="Steps" style="margin-bottom: 10px;">
                                                <table>
                                                    <tr>
                                                        <td colspan="2" align="left" style="padding-bottom: 5px;">
                                                            <asp:Label ID="PresentersLabel" runat="server" Font-Bold="True" Text="Presenters Audio/Webcam Conference Options"
                                                                CssClass="frmHeading" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="padding-left: 2px">
                                                            <table border="0" cellpadding="0" cellspacing="5">
                                                                <tr>
                                                                    <td>
                                                                        <asp:RadioButton ID="radioBtnId11" runat="server" GroupName="PresentersOptions" Checked="True" />
                                                                    </td>
                                                                    <td>
                                                                        &nbsp;<asp:Label ID="lblradio1" runat="server" Font-Bold="False">Use built-in audio conferencing:</asp:Label>&nbsp;<asp:Label
                                                                            ID="lblradioContry" runat="server" Font-Bold="True">United States </asp:Label><img
                                                                                src="../Images/icons/flags/usa.gif" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:RadioButton ID="radioBtnId22" runat="server" GroupName="PresentersOptions" />
                                                                    </td>
                                                                    <td>
                                                                        &nbsp;<asp:Label ID="lblradio2" runat="server" Font-Bold="False">VoIP - Requires microphone and speakers</asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:RadioButton ID="radioBtnId33" runat="server" GroupName="PresentersOptions" />
                                                                    </td>
                                                                    <td>
                                                                        &nbsp;<asp:Label ID="lblradio3" runat="server" Font-Bold="False">Use your own conference call</asp:Label>
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
                                            <div class="FormCont Steps">
                                                <table>
                                                    <tr>
                                                        <td align="left" style="padding-bottom: 10px;">
                                                            <asp:Label ID="PublishLabel" runat="server" Font-Bold="True" Text="Publish to SnapSite"
                                                                CssClass="frmHeading" />&nbsp;
                                                                <span id="spPre" runat="server" visible="false">My SnapSite <span class="spSS" id="sp3">[p]</span></span>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td height="24" style="padding-left: 5px">
                                                            <asp:CheckBox ID="chkEmailRegAPI" runat="server" />
                                                            &nbsp;<asp:Label ID="lblEmailRegAPI" runat="server" Text="Make Publish - Allow others to view your webinar through your SnapSite"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                                <img src="../Images/blank.gif" height="10" />
                            </asp:View>
                            <asp:View ID="vwTheme" runat="server">
                                <uc1:webTheme ID="webTheme1" runat="server" />
                            </asp:View>
                            <asp:View ID="vwAudience" runat="server">
                                <uc2:webAudience ID="webAudience1" runat="server"></uc2:webAudience>
                            </asp:View>
                            <asp:View ID="vwRegistration" runat="server">
                                <uc3:webRegistration ID="webRegistration1" runat="server"></uc3:webRegistration>
                            </asp:View>
                            <asp:View ID="vwEmail" runat="server">
                                <uc4:webEmail ID="webEmail1" runat="server" />
                            </asp:View>
                        </asp:MultiView>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div class="Row Centeralign">
                            <asp:LinkButton CssClass="SubBtn" ID="btnPrev" runat="server" Text="Prev" Visible="false"
                                OnClick="btnPrev_Click"></asp:LinkButton>
                            <asp:LinkButton CssClass="SubBtn" ID="btnNext" runat="server" Text="Save and Continue"
                                OnClientClick="return validateTab();" OnClick="btnNext_Click"></asp:LinkButton>
                            <asp:LinkButton CssClass="SubBtn" ID="btnSave" runat="server" Text="Save Webinar"
                                Visible="false" OnClick="btnSave_Click"></asp:LinkButton>
                            <asp:HiddenField ID="hActiveTab" runat="server" Value="1" />
                            <br />
                            <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
                            <script type="text/javascript">
                                function validateTab() {
                                    var tab = document.getElementById('<%= hActiveTab.ClientID %>');
                                    if (tab.value == 1) {
                                        // start date check
                                        var stDate = $find('<%=rdtStartDate.ClientID %>');
                                        if (stDate.get_textBox().value == '') {
                                            alert('Webinar date is missing');
                                            return false;
                                        }
                                        // Recurrence check
                                        if (document.getElementById('<%=chkRecurrence.ClientID %>').checked) {
                                            if (document.getElementById('<%= rbtnEndAfter.ClientID %>').checked) {
                                                var txt1 = document.getElementById('<%= txtEndAfter.ClientID %>');
                                                if (txt1.value == '') {
                                                    alert('Number of occurences missing');
                                                    return false;
                                                }
                                                else {
                                                    if (ISNumeric(txt1.value) == false) {
                                                        alert('Number occurence should be valid number');
                                                        return false;
                                                    }
                                                }
                                            }
                                            if (document.getElementById('<%= rbtnEndBy.ClientID %>').checked) {
                                                var enDate = $find('<%=rdtEndBy.ClientID %>');
                                                if (enDate.get_textBox().value == '') {
                                                    alert('End by date is missing');
                                                    return false;
                                                }
                                                //                                            else if (isDatePast(enDate) == false) {
                                                //                                                alert('Entered end by date cannot be in past');
                                                //                                                return false;
                                                //                                            }
                                            }
                                        }
                                    }
                                    return true;
                                }

                                function isDatePast(refDate) {
                                    var date = new Date();
                                    var enDate = $find('<%=rdtEndBy.ClientID %>');
                                    var one_day = 1000 * 60 * 60 * 24;
                                    //var hours_conv = 1000 * 60 * 60;
                                    if (enDate.get_textBox().value != '') {
                                        var _Diff = Math.ceil((enDate.get_textBox().value - date.getDate()) / (one_day));
                                        if (_Diff <= 0)
                                            return true;
                                        else
                                            return false;
                                    }
                                    return false;
                                }

                                function ISNumeric(strString) {
                                    var strValidChars = "0123456789";
                                    var strChar;
                                    var blnResult = true;
                                    if (strString.length == 0)
                                        return false;
                                    for (i = 0; i < strString.length && blnResult == true; i++) {
                                        strChar = strString.charAt(i);

                                    }
                                    return blnResult;
                                }
                            </script>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div class="BottBg">
    </div>
    <img src="../Images/blank.gif" height="10" />
    <div class="Clr">
    </div>
</div>

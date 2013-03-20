<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="webaction.ascx.cs" Inherits="EBird.Web.App.UserControls.webaction" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:ValidationSummary ID="xevs_userAdmin" CssClass="ValidationSummary" HeaderText=""
    runat="server" ShowSummary="False" ShowMessageBox="True" />
<script type="text/javascript">
    function OnClientLoad(editor, args) {
        var style = editor.get_contentArea().style;
        style.color = "black";
    }
</script>
<style>
    .wa_row
    {
        padding: 2px 2px 2px 0px;
    }
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
        <table width="100%" align="center" runat="server" id="tbHeader">
            <tr>
                <td align="right" style="padding-bottom: 10px;">
                    <asp:Label ID="lblRtn" Text="Return to " runat="server"></asp:Label><asp:LinkButton
                        ID="lbtnBack" runat="server" Text="Update Webinar" CssClass="lnkBtn1" OnClick="lbtnBack_Click"
                        CausesValidation="false"></asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td>
                    <div>
                        <table border="0" style="padding: 10px 10px 10px 10px; width: 100%">
                            <tr>
                                <td>
                                    <asp:Label ID="lblWebinarTitle" runat="server" CssClass="frmHeading"></asp:Label>
                                    <asp:HiddenField ID="hWebinarID" runat="server" />
                                </td>
                                <td align="right">
                                    <asp:Literal ID="ltrStatus" runat="server" Visible="false" />
                                    <asp:Label ID="lblTime" runat="server" CssClass="frmHeading"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
        </table>
        <asp:MultiView ID="mvWebAction" runat="server">
            <asp:View ID="vwInvalid" runat="server">
                <table width="100%" align="center">
                    <tr>
                        <td>
                            <div class="FormCont Steps">
                                <table border="0" width="100%" cellpadding="5" style="font-family: Verdana; padding: 5px 2px 5px 5px;">
                                    <tr>
                                        <td align="center">
                                            <asp:Label ID="lblInactivemsg" runat="server" Text="Invalid webinar or insufficient previledge"
                                                ForeColor="Red"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>
                </table>
            </asp:View>
            <asp:View ID="vwURL" runat="server">
                <table width="100%" align="center">
                    <tr>
                        <td>
                            <div class="FormCont Steps regOutline">
                                <table border="0" width="100%" cellpadding="0" style="font-family: Verdana; padding: 0px 2px 5px 2px;">
                                    <tr>
                                        <td align="left" style="padding-bottom: 5px;">
                                            <asp:Label ID="lblWebURLs" runat="server" Text="Webinar URLs" CssClass="frmHeading"
                                                Font-Bold="true" />
                                        </td>
                                    </tr>
                                </table>
                                <table border="0" class="genTable1">
                                    <tr>
                                        <td class="wa_row" style="padding-left: 1px; width: 110px">
                                            <asp:Label ID="lblReg" Text="Registration" runat="server" />
                                        </td>
                                        <td class="wa_row" style="width: 15px">
                                            &nbsp;
                                            <asp:CheckBox ID="chkRegURL" runat="server" Enabled="false" />
                                        </td>
                                        <td class="wa_row" style="padding-left: 5px">
                                            <asp:HyperLink ID="hlnkReg" runat="server" Text="Registration URL undefined" Font-Underline="True"
                                                Target="_blank"></asp:HyperLink>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="wa_row" style="padding-left: 5px">
                                            <asp:Label ID="lblPreview" Text="Preview Interface" runat="server" />
                                        </td>
                                        <td class="wa_row">
                                            &nbsp;
                                            <asp:CheckBox ID="chkPreURL" runat="server" Enabled="false" />
                                        </td>
                                        <td style="padding-left: 5px">
                                            <asp:HyperLink ID="hlnkPre" runat="server" Text="Preview Interface undefined" Font-Underline="True"
                                                Target="_blank"></asp:HyperLink>
                                        </td>
                                    </tr>
                                    <%--<tr>
                                        <td class="wa_row">
                                            <asp:Label ID="lblAudi" Text="Audience Interface" runat="server" />
                                        </td>
                                        <td class="wa_row">
                                            &nbsp;
                                            <asp:CheckBox ID="chkAudi" runat="server" Enabled="false" />&nbsp;
                                        </td>
                                        <td class="wa_row">
                                            <asp:HyperLink ID="hlnkAudi" runat="server" Text="Audience Interface URL undefined"
                                                Target="_blank" Font-Underline="True" CssClass="hyperlink_EB"></asp:HyperLink>
                                        </td>
                                    </tr>--%>
                                    <tr>
                                        <td class="wa_row" style="padding-left: 5px">
                                            <asp:Label ID="lblCC" Text="Command Center" runat="server" />
                                        </td>
                                        <td class="wa_row">
                                            &nbsp;
                                            <asp:CheckBox ID="chkCC" runat="server" Enabled="false" />
                                        </td>
                                        <td class="wa_row" style="padding-left: 5px">
                                            <asp:HyperLink ID="hlnkCC" runat="server" Text="Command Center URL undefined" Font-Underline="True"
                                                Target="_blank"></asp:HyperLink>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="wa_row" style="padding-left: 5px">
                                            <asp:Label ID="lblAnalysis" Text="Analytics" runat="server" />
                                        </td>
                                        <td class="wa_row">
                                            &nbsp;
                                            <asp:CheckBox ID="chkAnalysis" runat="server" Enabled="false" />
                                        </td>
                                        <td class="wa_row" style="padding-left: 5px">
                                            <asp:HyperLink ID="hlnkAnalysis" runat="server" Text="Analytics URL undefined" Font-Underline="True"
                                                Target="_blank"></asp:HyperLink>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <img src="../Images/blank.gif" height="5" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" class="wa_row" style="padding-left: 5px">
                                            <asp:TextBox ID="txtEmails" runat="server" TextMode="MultiLine" Height="50" Width="650px"></asp:TextBox>
                                            <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender19" runat="server" TargetControlID="txtEmails"
                                                WatermarkText="Separate multiple addresses with semi-colon(;)" WatermarkCssClass="watermarked_EBSearch" />
                                            <asp:Label ID="lblInvalidEmails" runat="server" Text="" ForeColor="Red" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <img src="../Images/blank.gif" height="10" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;<asp:Button ID="btnEmailURLs" runat="server" Text="Email URLs" CssClass="SubBtn"
                                CausesValidation="False" OnClick="btnEmailURLs_Click" />
                            &nbsp;<asp:Button ID="btnCancelEmailURLs" OnClick="btnCanCancel_Click" CssClass="SubBtn"
                                runat="server" Text="Cancel" CausesValidation="False" CommandName="Cancel" />
                            <asp:Label ID="lblErrorURL" runat="server" Text="" CssClass="msgError" ForeColor="Red" />
                        </td>
                    </tr>
                </table>
            </asp:View>
            <asp:View ID="vwSchSame" runat="server">
                <table width="100%" align="center">
                    <tr>
                        <td>
                            <asp:PlaceHolder ID="phSchSameWebinar" runat="server">
                                <table width="100%" align="center">
                                    <tr>
                                        <td colspan="2" valign="top">
                                            <div class="FormCont Steps" style="padding-bottom: 20px; margin-bottom: 10px">
                                                <table width="100%">
                                                    <tr>
                                                        <td align="left" colspan="2" style="padding-bottom: 14px;">
                                                            <asp:Label ID="lblConfirmEmail" runat="server" Font-Bold="True" Text="Webinar Details (Save As)"
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
                                                                <telerik:RadEditor runat="server" ID="redtSummary1" ToolbarMode="Default" Height="180px"
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
                                                                ControlToValidate="redtSummary1" ErrorMessage="Please enter required field - Description"
                                                                ForeColor="Red" Display="None" Text="*" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td valign="top" style="padding-left: 5px">
                                                            <asp:Label ID="lblStartDate" runat="server">Start Date<span class='EBmsg'>&nbsp;*</span></asp:Label>
                                                        </td>
                                                        <td class="Row LongText">
                                                            <telerik:RadDatePicker ID="rdtStartDate1" runat="server" Height="23" Width="100"
                                                                Skin="Default" AutoPostBack="False" HideAnimation-Duration="800" ShowAnimation-Duration="800"
                                                                Calendar-ClientEvents-OnLoad="rdtDate_SetMaxDateToCurrentDate">
                                                            </telerik:RadDatePicker>
                                                            <asp:RequiredFieldValidator ID="vx_rdtStartDate" runat="server" CssClass="ValidationSummary"
                                                                ControlToValidate="rdtStartDate1" ErrorMessage="Please enter required field - Start Date"
                                                                ForeColor="Red" Display="None" Text="*" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="padding-left: 5px">
                                                            <asp:Label ID="lblStartTime" runat="server">Start Time<span class='EBmsg'>&nbsp;*</span></asp:Label>
                                                        </td>
                                                        <td class="Row LongText">
                                                            <telerik:RadTimePicker ID="rdtStartTime" runat="server" Height="23" Width="100" Skin="Default"
                                                                MinDate="<%#System.DateTime.Now.AddDays(-1) %>" ShowAnimation-Type="Fade" AutoPostBack="False"
                                                                ShowAnimation-Duration="800" HideAnimation-Duration="800">
                                                                <DateInput ID="stTimeInput" runat="server">
                                                                    <ClientEvents OnLoad="onLoadRadStartTime" />
                                                                </DateInput>
                                                                <TimeView ID="tvStartTime" ShowHeader="False" Interval="00:30:00" Columns="6" runat="server"
                                                                    OnClientTimeSelected="stClientTimeSelected">
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
                                                                MinDate='<%# DateTime.Today %>' FocusedDate='<%# DateTime.Today %>' ShowAnimation-Type="Fade"
                                                                AutoPostBack="False" ShowAnimation-Duration="800" HideAnimation-Duration="800">
                                                                <DateInput ID="enTimeInput" runat="server">
                                                                    <ClientEvents OnLoad="onLoadRadEndTime" />
                                                                </DateInput>
                                                                <TimeView ID="tvEndTime" ShowHeader="False" Interval="00:30:00" Columns="6" runat="server">
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
                                                <img src="../Images/blank.gif" height="10" />
                                                <div class="right">
                                                    <font color="#A00000">*</font><i><small>Required field</small></i>
                                                </div>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            &nbsp;<asp:Button ID="btnSaveWebinar" runat="server" Text="Save Webinar" CssClass="SubBtn"
                                                OnClick="btnSaveWebinar_Click" />
                                            &nbsp;<asp:Button ID="btnCancelSaveWebinar" OnClick="btnCanCancel_Click" CssClass="SubBtn"
                                                runat="server" Text="Cancel" CausesValidation="False" CommandName="Cancel" />
                                            &nbsp;<asp:Label ID="lblWebSaveAsError" runat="server" ForeColor="Red"></asp:Label>
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

                                        var stDate = $find('<%=rdtStartDate1.ClientID %>');
                                        var stTime = $find('<%=rdtStartTime.ClientID %>');
                                        var enTime = $find('<%=rdEndTime.ClientID %>');

                                        var Date1 = new Date(stDate.get_textBox().value + ' ' + stTime.get_textBox().value);
                                        var Date2 = new Date(stDate.get_textBox().value + ' ' + enTime.get_textBox().value);

                                        if (stTime.get_textBox().value != "" && enTime.get_textBox().value != "") {
                                            args.IsValid = true;
                                            if ((Date2 - Date1) < 0) {
                                                alert("The end time should be greater than the start time!");
                                                rdEndTime.clear();
                                                args.IsValid = false;
                                            }
                                        }
                                    }

                                    function onLoadRadStartTime(sender, args) {
                                        rdtStartTime = sender;
                                        var now = new Date();
                                        var currDate = new Date(now.getTime());
                                        //rdtStartTime.get_dateInput().set_value(currDate);

                                        //stTime.get_dateInput().set_value("01:00 PM");
                                        //stTime.get_dateInput().set_value(currDate);
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
                            </asp:PlaceHolder>
                            <asp:PlaceHolder ID="phWebinarSchNotActive" runat="server" Visible="false">
                                <div class="FormCont Steps">
                                    <table width="100%">
                                        <tr>
                                            <td align="center">
                                                <br />
                                                <br />
                                                <asp:Label ID="lblSchWebNotActive" runat="server" Text="Webinar is in draft status and cannot be rescheduled.."
                                                    ForeColor="Red" Font-Bold="true"></asp:Label>
                                                <br />
                                                <br />
                                                &nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </asp:PlaceHolder>
                        </td>
                    </tr>
                </table>
            </asp:View>
            <asp:View ID="vwEmailRegistrant" runat="server">
                <table width="100%" align="center" class="tblWebinarActions">
                    <tr>
                        <td>
                            <div class="FormCont Steps">
                                <table>
                                    <tr>
                                        <td>
                                            &nbsp;<asp:Label ID="lblSubject" runat="server" Text="Subject"></asp:Label>&nbsp;
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtSubject" Columns="50" MaxLength="50" runat="server" Height="19"
                                                Width="500" CssClass="textbox_EB" />
                                        </td>
                                    </tr>
                                </table>
                                <br />
                                <table border="0">
                                    <tr>
                                        <td style="padding-left: 5px">
                                            <telerik:RadEditor runat="server" ID="redEmailRegistrants" ToolbarMode="Default"
                                                Height="180px" ToolsFile="~/editor/BasicTools.xml" BorderStyle="None" CssClass="rteditor1"
                                                ToolsWidth="100%" EnableResize="False" EditModes="Design" Width="673px">
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
                                        </td>
                                    </tr>
                                </table>
                                <br />
                                &nbsp;<asp:Label ID="lblInstruction" runat="server" Text="Depending on what stage the Webinar is in, all options may not be available"></asp:Label>
                                <br />
                                <br />
                                <table border="0">
                                    <tr>
                                        <td height="22" style="padding-left: 5px">
                                            <asp:CheckBox ID="chkDidNotAttend" runat="server" CssClass="chkAlign2" Text="Registered but did not attend (13)" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td height="22" style="padding-left: 5px">
                                            <asp:CheckBox ID="chkAttended" runat="server" CssClass="chkAlign2" Text="Attended Webinar (18)" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td height="22" style="padding-left: 5px">
                                            <asp:CheckBox ID="chkAttendedLive" runat="server" CssClass="chkAlign2" Text="Attended Webinar - Viewed Live (10)" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td height="22" style="padding-left: 5px">
                                            <asp:CheckBox ID="chkOnDemand" runat="server" CssClass="chkAlign2" Text="Attended Webinar - Viewed OnDemand (8)" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td height="22" style="padding-left: 5px">
                                            <asp:CheckBox ID="chkRegistered" runat="server" CssClass="chkAlign2" Text="Registered for Webinar (31)"
                                                Checked="true" />
                                        </td>
                                    </tr>
                                </table>
                                <br />
                                <table border="0" style="padding-left: 5px">
                                    <tr>
                                        <td>
                                            <asp:CheckBox ID="chkSendMe" runat="server" Checked="True" />
                                            <asp:Label ID="chkSendMeLable" runat="server" Font-Bold="False" Text="Email a copy of this email to " />
                                            <asp:TextBox ID="txtSendMeEmailAddress" runat="server" Text="someone@domain.com"
                                                CssClass="textbox_EB" Width="200"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;<asp:Button CssClass="SubBtn" runat="server" ID="btnSendEmail" Text="Send Email"
                                OnClick="btnSendEmail_Click" />
                            &nbsp;<asp:Button ID="btnCancelSendEmail" OnClick="btnCanCancel_Click" CssClass="SubBtn"
                                runat="server" Text="Cancel" CausesValidation="False" CommandName="Cancel" />
                            <asp:Label ID="lblError" runat="server" CssClass="msgError"></asp:Label>
                        </td>
                    </tr>
                </table>
            </asp:View>
            <asp:View ID="vwOutlook" runat="server">
                <table width="100%" align="center">
                    <tr>
                        <td style="padding-left: 5px">
                            <div class="FormCont Steps">
                                <font style="color: #000000; font-weight: normal; font-size: 11px;">
                                    Add this webinar to your Outlook calender.</font>
                                <br />
                                <asp:Label ID="lblOutLookErr" runat="server" ForeColor="Red"></asp:Label>
                                <asp:Label ID="lblOutLookMsg" runat="server"></asp:Label>
                                <br />
                            </div>
                            &nbsp;<asp:Button ID="btnOutAdd" runat="server" Text="Add to Outlook" CssClass="SubBtn"
                                CausesValidation="False" OnClick="btnOutAdd_Click" />
                            &nbsp;<asp:Button ID="btnOutCancel" OnClick="btnCanCancel_Click" CssClass="SubBtn"
                                runat="server" Text="Cancel" CausesValidation="False" CommandName="Cancel" />
                        </td>
                    </tr>
                </table>
            </asp:View>
            <asp:View ID="vwCanWebinar" runat="server">
                <table width="100%" align="center">
                    <tr>
                        <td style="padding-left: 5px">
                            <div class="FormCont Steps">
                                <asp:Label ID="lblCancelInstruction" Text="This Webinar will
                                    be cancelled and a notification will be sent to all registrants." runat="server"></asp:Label>
                                <br />
                                <br />
                                <asp:HyperLink ID="hlnkCanPreviewEmail" runat="server" CssClass="lnkBtn2" Text="Preview Cancel Notification"></asp:HyperLink>
                                <br />
                                <br />
                            </div>
                        </td>
                    </tr>
                </table>
                 <script src="/qtip2/qtipCommonfn.js" type="text/javascript" charset="utf-8"></script>
                <script>
                    $(document).ready(function () {

                        // EMAIL CONFIRMATION - Preview
                        $('#ContentPlaceHolder1_webaction1_hlnkCanPreviewEmail').live('click', function (e) {
                            //var themeUrl = $(this).attr('data-typ');
                            var hWebID = document.getElementById('<%=hWebinarID.ClientID %>');
                            var URL = "/Pages/popup/EmailPreview.aspx?typ=CN&ID=" + hWebID.value;
                            qtipPopup2("#ContentPlaceHolder1_webaction1_hlnkCanPreviewEmail",
                    "<div id='modalContainer'><div class='modalClose'><img src='/images/x.png' /></div><iframe id='emlFrame' src=" + URL + " height='425' width='750' scrolling='no' class='bgfill'></iframe></div>",
                    750, '.modalClose', '', '#emlFrame', 'body #ContentPlaceHolder1_hModalStatusFlg');
                        });
                    });

                </script>
                &nbsp;&nbsp;<asp:Button ID="btnDelWebinar" OnClick="btnCanWebinar_Click" runat="server"
                    Text="Cancel Webinar" CssClass="SubBtn" />
                &nbsp;&nbsp;<asp:Button ID="btnDelCancel" OnClick="btnCanCancel_Click" CssClass="SubBtn"
                    runat="server" Text="Do Not Cancel" CausesValidation="False" CommandName="Cancel" />
            </asp:View>
            <asp:View ID="vwDelWebinar" runat="server">
                <table width="100%" align="center">
                    <tr>
                        <td style="padding-left: 5px">
                            <div class="FormCont Steps">
                                <asp:Label ID="lblDeleteInstruction" Text="This Webinar will
                                    be deleted and placed in the Recycle Bin." runat="server"></asp:Label>
                                <br />
                                <br />
                            </div>
                        </td>
                    </tr>
                </table>
                &nbsp;&nbsp;<asp:Button ID="btnDelWebinar1" OnClick="btnDelWebinar_Click" runat="server"
                    OnClientClick="return jsConfirm(this);" Text="Delete Webinar" CssClass="SubBtn" />
                &nbsp;&nbsp;<asp:Button ID="btnDelCancel1" OnClick="btnCanCancel_Click" CssClass="SubBtn"
                    runat="server" Text="Do Not Delete" CausesValidation="False" CommandName="Cancel" />
                <script>
                    function jsConfirm(btn) {
                        if (btn.value == 'Delete Webinar') {
                            return confirm("Are you sure, you want to delete this webinar?");
                        }
                        return true;
                    }
                </script>
            </asp:View>
        </asp:MultiView>
    </div>
    <div class="BottBg">
    </div>
    <!-- The following hidden variable is used for modal window closing flag -->
    <asp:HiddenField ID="hModalStatusFlg" runat="server" />
    <img src="../Images/blank.gif" height="10" />
    <div class="Clr">
    </div>
</div>

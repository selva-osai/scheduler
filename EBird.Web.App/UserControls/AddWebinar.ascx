<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddWebinar.ascx.cs" Inherits="EBird.Web.App.UserControls.AddWebinar" %>
<%@ Register Assembly="RichTextEditor" Namespace="AjaxControls" TagPrefix="rte" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<style type="text/css">
.RadComboBoxDropDown_Default .rcbHovered {
background:#EBEBEB;
color:#C80000;
}

.RadCalendar_Default .rcMain .rcRow .rcHover,.RadCalendar_Default .rcRow .rcSelected {
background:#EBEBEB 0 -1700px repeat-x url(Calendar/sprite.gif);
border-color:#EBEBEB;
}

.RadCalendar_Default .rcMain .rcRow .rcHover a,.RadCalendar_Default .rcMain .rcRow .rcSelected a {
color:#C80000;
}

.RadCalendarMonthView_Default .rcSelected a,.RadCalendarTimeView_Default td.rcHover a,.RadCalendarTimeView_Default td.rcSelected a {
background:#EBEBEB 0 -1700px repeat-x url(Calendar/sprite.gif);
color:#C80000;
border-color:#EBEBEB;
}
</style>
<div>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:Accordion ID="Accordion1" runat="server" SelectedIndex="0" FadeTransitions="true"   TransitionDuration="250"
    FramesPerSecond="40"
    RequireOpenedPane="false">
        <Panes>
            <asp:AccordionPane ID="p1" runat="server">
                <header>
                    Schedule a Webinar</header>
                <content>
                    <table width="97%" align="center">
                        <tr>
                            <td>
                                <asp:Label ID="Label2" runat="server" Text="Title"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtTitle" runat="server" Height="20px" Width="300px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label3" runat="server" Text="Description"></asp:Label>
                            </td>
                            <td>
                                <rte:RichTextEditor ID="RichTextEditor1" runat="server"></rte:RichTextEditor>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label1" runat="server" Text="Start Date"></asp:Label>
                            </td>
                            <td>
                                <telerik:RadDatePicker ID="RadDatePicker1" runat="server" Width="100px" HideAnimation-Duration="800" ShowAnimation-Duration="800">
                                </telerik:RadDatePicker>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label4" runat="server" Text="Start Time" ></asp:Label>
                            </td>
                            <td>
                                <telerik:RadTimePicker ID="RadTimePicker1" runat="server" Width="100px">
                                </telerik:RadTimePicker>
                            </td>
                            <td>
                                <asp:DropDownList ID="DropDownList1" runat="server">
                                    <listitem>(GMT-12:00)
                                        International dateline, west</listitem>
                                    <listitem>(GMT-11:00)
                                        Midway Islands, Samoan Islands</listitem>
                                    <listitem>(GMT-10:00)
                                        Hawaii</listitem>
                                    <listitem>(GMT-09:00)
                                        Alaska</listitem>
                                    <listitem>(GMT-08:00)
                                        Pacific Time (USA or Canada); Tijuana</listitem>
                                    <listitem>(GMT-07:00)
                                        Mountain Time (USA or Canada)</listitem>
                                    <listitem>(GMT-06:00)
                                        Central time (USA or Canada)</listitem>
                                    <listitem>(GMT-05:00)
                                        Eastern time (USA or Canada)</listitem>
                                    <listitem>
                                        (GMT-04:00) Atlantic Time (Canada)</listitem>
                                    <listitem>(GMT-03:30)
                                        Newfoundland</listitem>
                                    <listitem>(GMT-03:00)
                                        Brasilia</listitem>
                                    <listitem>(GMT-02:00)
                                        Mid-Atlantic</listitem>
                                    <listitem>(GMT-01:00)
                                        Azorerne</listitem>
                                    <listitem>(GMT)
                                        Greenwich Mean Time: Dublin, Edinburgh, Lissabon, London</listitem>
                                    <listitem>(GMT+01:00)
                                        Amsterdam, Berlin, Bern, Rom, Stockholm, Wien</listitem>
                                    <listitem>(GMT+02:00)
                                        Athen, Istanbul, Minsk</listitem>
                                    <listitem>(GMT+03:00)
                                        Moscow, St. Petersburg, Volgograd</listitem>
                                    <listitem>(GMT+03:30)
                                        Teheran</listitem>
                                    <listitem>(GMT+04:00)
                                        Abu Dhabi, Muscat</listitem>
                                    <listitem>(GMT+04:30)
                                        Kabul</listitem>
                                    <listitem>(GMT+05:00)
                                        Islamabad, Karachi, Tasjkent</listitem>
                                    <listitem>(GMT+05:30)
                                        Kolkata, Chennai, Mumbai, New Delhi</listitem>
                                    <listitem>(GMT+05:45)
                                        Katmandu</listitem>
                                    <listitem>(GMT+06:00)
                                        Astana, Dhaka</listitem>
                                    <listitem>(GMT+06:30)
                                        Rangoon</listitem>
                                    <listitem>(GMT+07:00)
                                        Bangkok, Hanoi, Djakarta</listitem>
                                    <listitem>(GMT+08:00)
                                        Beijing, Chongjin, SAR Hongkong, Ürümqi</listitem>
                                    <listitem>(GMT+09:00)
                                        Osaka, Sapporo, Tokyo</listitem>
                                    <listitem>(GMT+09:30)
                                        Adelaide</listitem>
                                    <listitem>(GMT+10:00)
                                        Canberra, Melbourne, Sydney</listitem>
                                    <listitem>(GMT+11:00)
                                        Magadan, Solomon Islands, New Caledonien</listitem>
                                    <listitem>(GMT+12:00)
                                        Fiji, Kamtjatka, Marshall Islands</listitem>
                                    <listitem>(GMT+13:00)
                                        Nuku'alofa</listitem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                        <td>
                            <asp:CheckBox ID="CheckBox1" runat="server" Text="Recurrence" />
                        </td>
                        </tr>
                    </table>
                    <div style="float: right; padding-right: 10px;">
                        <br />
                        <span id="ctl00_MainContent_AccordionPane1_content_Label14" style="color: red;">*</span>
                        <span id="ctl00_MainContent_AccordionPane1_content_Label16">Required field</span>
                    </div>
                </Content>
            </asp:AccordionPane>
            </Panes>
            </asp:Accordion>
       
</div>

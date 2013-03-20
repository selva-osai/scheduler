<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdvWebinarSearch.aspx.cs"
    MasterPageFile="~/MasterPages/popupMaster.Master" Inherits="EBird.Web.App.Pages.AdvWebinarSearch" %>

<%@ Register Assembly="eWorld.UI.Compatibility, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2"
    Namespace="eWorld.UI.Compatibility" TagPrefix="cc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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
    <div style="margin-left: 0px">
        <div class="regOutline" style="margin-bottom: 3px;">
            <telerik:RadAjaxPanel ID="RadAjaxPanel2" runat="server" HorizontalAlign="NotSet">
                <table border="0">
                    <tr>
                        <td colspan="4">
                            <asp:Label ID="lblCaption" runat="server" Text="My Webinars - Advanced Search" Font-Bold="true"></asp:Label><br />
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblFromDate" CssClass="label_EB" runat="server" Text="Webinar Date between" />
                        </td>
                        <td>
                            <telerik:RadDatePicker ID="dtpFromDate" runat="server" PopupDirection="BottomLeft"
                                Height="20" Width="100" DateInput-DateFormat="MM/dd/yyyy">
                            </telerik:RadDatePicker>
                        </td>
                        <td>
                            <asp:Label ID="lblToDate" CssClass="label_EB" runat="server" Text=" and " />
                        </td>
                        <td>
                            <telerik:RadDatePicker ID="dtpToDate" runat="server" PopupDirection="BottomLeft"
                                Height="20" Width="100" DateInput-DateFormat="MM/dd/yyyy">
                            </telerik:RadDatePicker>
                        </td>
                    </tr>
                </table>
                <br />
                <br />
                <table border="0">
                    <tr>
                        <td>
                            <asp:TextBox ID="txtAdvSearch" MaxLength="40" runat="server" Height="20" Width="450" />
                            <asp:TextBoxWatermarkExtender ID="EBS" runat="server" TargetControlID="txtAdvSearch"
                                WatermarkText="Search My Webinars" WatermarkCssClass="watermarked_EBSearch" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <br />
                            <asp:RadioButtonList ID="optSearchOption" runat="server" CssClass="optGen" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Title" Value="Title" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="Webinar Summary" Value="Description"></asp:ListItem>
                                <asp:ListItem Text="All" Value="All"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                </table>
                <%--             <br />
                <table border="0">
                    <tr>
                        <td>
                            <asp:Label ID="lblRegNo" runat="server" Text="Registered for webinar"></asp:Label>
                        </td>
                        <td>
                            &nbsp; >=
                        </td>
                        <td>
                            <cc1:MaskedTextBox ID="txtRegNo" runat="server" Mask="999" Width="25" MaxLength="3" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblViewNo" runat="server" Text="Viewed webinar Live"></asp:Label>
                        </td>
                        <td>
                            &nbsp; >=
                        </td>
                        <td>
                            <cc1:MaskedTextBox ID="txtViewNo" runat="server" Mask="999" Width="25" MaxLength="3" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblDemandNo" runat="server" Text="Viewed webinar OnDemand"></asp:Label>
                        </td>
                        <td>
                            &nbsp; >=
                        </td>
                        <td>
                            <cc1:MaskedTextBox ID="txtDemandNo" runat="server" Mask="999" Width="25" MaxLength="3" />
                        </td>
                    </tr>
                </table>--%>
            </telerik:RadAjaxPanel>
            <br />
            <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
                <script type="text/javascript">

                    function GetRadWindow() {
                        var oWindow = null;
                        if (window.radWindow)
                            oWindow = window.radWindow;
                        else if (window.frameElement.radWindow)
                            oWindow = window.frameElement.radWindow;
                        return oWindow;
                    }

                    function closeMe() {
                        GetRadWindow().close();
                    }

                    function callFn() {
                        var oWindow = null;
                        oWindow = GetRadWindow().BrowserWindow.document;

                        var nxt = document.getElementById('<%=txtAdvSearch.ClientID %>');
                        oWindow.getElementById('ContentPlaceHolder1_webinarlist1_hSearchText').value = nxt.value;

                        nxt = document.getElementById('<%=dtpFromDate.ClientID %>');
                        oWindow.getElementById('ctl00_ContentPlaceHolder1_webinarlist1_dpFrom_dateInput').value = nxt.value;

                        nxt = document.getElementById('<%=dtpToDate.ClientID %>');
                        oWindow.getElementById('ctl00_ContentPlaceHolder1_webinarlist1_dpTo_dateInput').value = nxt.value;

                        oWindow.getElementById('ContentPlaceHolder1_webinarlist1_hSearchType').value = 'A';
                        var oRadio = document.getElementById('<%=optSearchOption.ClientID %>');
                        for (var i = 0; i < oRadio.length; i++) {
                            if (oRadio[i].checked) {
                                oWindow.getElementById('ContentPlaceHolder1_webinarlist1_hSearchField').value = oRadio[i].value;
                            }
                        }
                        GetRadWindow().BrowserWindow.searchSubmit();
                        //GetRadWindow().BrowserWindow.clickSearchButton('test text')
                        closeMe();
                    }

                </script>
            </telerik:RadCodeBlock>
        </div>
        <%--<asp:Button ID="btnSearch" CssClass="SubBtn" runat="server" Text="Search" onclick="btnSearch_Click" OnClientClick="callFn();"  />--%>
        <span style="padding-top: 5px; margin-top:10px;">
            <asp:Button ID="btnSearch" CssClass="SubBtn" runat="server" Text="Search" OnClick="btnSearch_Click" />
            <!-- OnClientClick="callFn();"  -->
            &nbsp;<asp:Button ID="btnCancel" CssClass="SubBtn" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
            <!-- OnClientClick="closeMe();" -->
            &nbsp;<asp:Label ForeColor="Red" runat="server" ID="lblError"></asp:Label>
        </span>
    </div>
    <!-- The following hodden variable is used for modal window closing flag -->
    <asp:HiddenField ID="hModalStatusFlg" runat="server" />
</asp:Content>

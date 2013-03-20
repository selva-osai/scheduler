<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/popupMaster.Master"
    AutoEventWireup="true" CodeBehind="audiFeature_Twitter.aspx.cs" Inherits="EBird.Web.App.Pages.popup.audiFeature_Twitter" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
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
    <asp:HiddenField ID="hWebinarID" Value="0" runat="server" />
    <div class="regOutline">
        <div style="margin-top: 5px; margin-bottom: 10px;">
            <asp:Label ID="lblConfigContent" runat="server" Text="Configure Audience Components - Twitter"
                Font-Bold="True"></asp:Label>
        </div>
        <asp:PlaceHolder ID="phConfig" runat="server" Visible="true">
            <table width="98%" align="center" class="genTable1" border="0">
                <tr>
                    <td colspan="2" align="left" style="padding-bottom: 8px; padding-top: 8px;">
                        <asp:Label ID="lblSubCap1" runat="server" Font-Bold="True" Text="Choose Twitter Account"
                            Font-Size="11px"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td width="60%" align="left" style="padding-left: 5px;">
                        <telerik:RadComboBox ID="rcmbAcct" runat="server" Width="150" AutoPostBack="false"
                            CausesValidation="False"  
                            MarkFirstMatch="True" HighlightTemplatedItems="True" ExpandAnimation-Duration="1000"
                            CollapseAnimation-Duration="1000" CollapseAnimation-Type="OutQuart" NoWrap="True"
                            EnableTheming="True" Font-Italic="False" Skin="Default" EmptyMessage="Twitter Account">
                            <Items>
                                <telerik:RadComboBoxItem runat="server" Text="Default" Value="Default" CssClass="ddStyle1" />
                            </Items>
                        </telerik:RadComboBox>
                    </td>
                    <td align="left" style="padding: 6px 0px 0px 3px">
                        <asp:TextBox ID="txtHeaderTitle" runat="server" CssClass="textbox_EB" Text="" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="left" style="padding-bottom: 8px; padding-top: 8px;">
                        <asp:Label ID="lblSubCap2" runat="server" Font-Bold="True" Font-Size="11px" Text="Twitter Feed Details"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="left" style="padding-left: 5px; font-size: 11px;">
                        Display Tweets With Hashtags:
                    </td>
                    <td align="left" style="padding: 0px 0px 0px 3px">
                        <asp:TextBox ID="txtHashTag" runat="server" CssClass="textbox_EB" Text="" />
                    </td>
                </tr>
                <tr>
                    <td align="left" style="padding-left: 5px; font-size: 11px;">
                        Display Tweets from Twitter Accounts:
                    </td>
                    <td align="left" style="padding: 0px 0px 0px 3px">
                        <asp:TextBox ID="txtDisplayAcct" runat="server" CssClass="textbox_EB" Text="" />
                    </td>
                </tr>
                <tr>
                    <td align="left" style="padding-left: 5px; font-size: 11px;">
                        Filter Display by Keywords:
                    </td>
                    <td align="left" style="padding: 0px 0px 0px 3px">
                        <asp:TextBox ID="txtDisplayKeywords" runat="server" CssClass="textbox_EB" Text="" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="left" style="padding-bottom: 8px; padding-top: 8px;">
                        <asp:Label ID="lblSubCap3" CssClass="frmHeading" runat="server" Font-Bold="True"
                            Text="Configure User Tweets"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="left" style="padding-left: 5px; font-size: 11px;">
                        Allow Users to Tweet:
                    </td>
                    <td align="left">
                        <asp:CheckBox ID="chkUserTweet" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td align="left" style="padding-left: 5px; font-size: 11px;">
                        End User Tweets with Hashtags:
                    </td>
                    <td align="left" style="padding: 0px 0px 0px 3px">
                        <asp:TextBox ID="txtEndUseHashtag" runat="server" CssClass="textbox_EB" Text="" />
                    </td>
                </tr>
                <tr>
                    <td align="left" style="padding-left: 5px; font-size: 11px;">
                        End User Tweets with Text/URLs:
                    </td>
                    <td align="left" style="padding: 0px 0px 0px 3px">
                        <asp:TextBox ID="txtEndUseURL" runat="server" CssClass="textbox_EB" Text="" />
                    </td>
                </tr>
            </table>
        </asp:PlaceHolder>
        <asp:PlaceHolder ID="phDisableFeature" runat="server" Visible="false">
            <asp:Label ID="lblDisableFeature" runat="server" Font-Size="11px"></asp:Label>
        </asp:PlaceHolder>
    </div>
    <div style="margin-top: 5px; margin-bottom: 5px">
        <asp:Button ID="btnSave" runat="server" CssClass="SubBtn" Text="Save" OnClick="btnSave_Click" />&nbsp;
        <asp:Button ID="btnCancel" runat="server" CssClass="SubBtn" Text="Cancel" OnClick="btnCancel_Click" />
    </div>
    <asp:HiddenField ID="hModalStatusFlg" runat="server" />
    <telerik:RadCodeBlock ID="rcRad" runat="server">
        <script type="text/javascript">

            function GetRadWindow() {
                var oWindow = null;
                if (window.radWindow) oWindow = window.radWindow; //For mozilla
                if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow; //IE  
                return oWindow;
            }

            function CloseAndReload() {
                var oWnd = GetRadWindow();
                //oWnd.BrowserWindow.location.reload(); add this line if you want to refresh the parent page      
                oWnd.close();
            }
 
        </script>
    </telerik:RadCodeBlock>
</asp:Content>

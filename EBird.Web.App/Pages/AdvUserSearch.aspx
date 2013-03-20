<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdvUserSearch.aspx.cs"
    Inherits="EBird.Web.App.Pages.AdvUserSearch" %>

<%@ Register Assembly="eWorld.UI.Compatibility, Version=2.0.6.2393, Culture=neutral, PublicKeyToken=24d65337282035f2"
    Namespace="eWorld.UI.Compatibility" TagPrefix="cc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Snapsession - User advanced search</title>
    <link id="Link1" href="/Styles/popUpWinStyle.css" rel="stylesheet" type="text/css" />
</head>
<body style="background-color: #FFFFFF">
    <form id="form1" runat="server" style="font-family: Verdana; color: #000000; font-size: 11px;">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div style="margin-left: 0px">
        <div class="regOutline" style="margin-bottom: 3px;">
            <telerik:RadAjaxPanel ID="RadAjaxPanel2" runat="server" HorizontalAlign="NotSet">
                <table border="0">
                    <tr>
                        <td>
                            <asp:Label ID="lblCaption" runat="server" Text="User Management - Advanced Search"
                                Font-Bold="true"></asp:Label><br />
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtAdvSearch" Columns="40" MaxLength="40" runat="server" Height="20"
                                Width="450" CssClass="textbox_EB" />
                            <asp:TextBoxWatermarkExtender ID="EBS" runat="server" TargetControlID="txtAdvSearch"
                                WatermarkText="User Name" WatermarkCssClass="watermarked_EBSearch" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:RadioButtonList ID="optSearchOption" runat="server" CssClass="optGen" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Users" Value="User" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="Administrators" Value="Admin"></asp:ListItem>
                                <asp:ListItem Text="All" Value="All"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table border="0">
                                <tr>
                                    <td>
                                        <asp:Label ID="lblEmailAddress" runat="server" Text="Email Address contains"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtEmailAddress" Columns="40" MaxLength="40" runat="server" Height="20"
                                            Width="200" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
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
                        oWindow.getElementById('ContentPlaceHolder1_userprofile1_hSearchText').value = nxt.value;

                        oWindow.getElementById('ContentPlaceHolder1_userprofile1_hSearchType').value = 'A';

                        var oRadio = document.getElementById('<%=optSearchOption.ClientID %>');
                        alert(oRadio.length);
                        for (var i = 0; i < oRadio.length; i++) {

                            if (oRadio[i].checked) {
                                oWindow.getElementById('ContentPlaceHolder1_userprofile1_hSearchRole').value = oRadio[i].value;
                            }
                        }

                        nxt = document.getElementById('<%=txtEmailAddress.ClientID %>');
                        oWindow.getElementById('ContentPlaceHolder1_userprofile1_hEmailContain').value = nxt.value;

                        GetRadWindow().BrowserWindow.searchSubmit();

                        closeMe();
                    }

                </script>
            </telerik:RadCodeBlock>
        </div>
        <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="SubBtn"  onclick="btnSearch_Click" />
            &nbsp;<asp:Button ID="btnCancel" CssClass="SubBtn" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
            &nbsp;<asp:Label ForeColor="Red" runat="server" ID="lblError"></asp:Label>
            <asp:HiddenField ID="hModalStatusFlg" runat="server" />
    </div>
    </form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="addPresenter.aspx.cs" Inherits="EBird.Web.App.Pages.popup.addPresenter" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<link id="Link1" href="/Styles/popUpWinStyle.css" rel="stylesheet" type="text/css" />
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <br />
    <table width="98%" align="center">
        <tr>
            <td align="center">
                <div class="regOutline">
                    <table width="97%" align="center" border="0">
                        <tr>
                            <td align="left" width="100%">
                                <table border="0" width="100%">
                                    <tr>
                                        <td align="left" width="65%">
                                            <asp:Label ID="lblPresenter" runat="server" Font-Bold="True" Text="My Presenter Information"
                                                CssClass="frmHeading"></asp:Label>
                                        </td>
                                        <td align="left" width="35%">
                                            <asp:Label ID="lblPresenterPhoto" runat="server" Font-Bold="True" Text="My Presenter Photo"
                                                CssClass="frmHeading"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                                <div class="FormCont Steps">
                                    <table border="0" width="100%">
                                        <tr>
                                            <td valign="top" width="65%">
                                                <table border="0" width="100%">
                                                    <tr>
                                                        <td width="30%">
                                                            <asp:Label ID="lblPreName" runat="server" Text="Presenter Name" CssClass="frmFields"></asp:Label>
                                                        </td>
                                                        <td width="70%">
                                                            <asp:TextBox ID="txtPresenterName" runat="server" MaxLength="50" Height="20" Width="250px"
                                                                CssClass="textbox_EB" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblTitle" runat="server" Text="Title" CssClass="frmFields"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtPresenterTitle" runat="server" MaxLength="50" Height="20" Width="200"
                                                                CssClass="textbox_EB" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblOrgName" runat="server" Text="Organization" CssClass="frmFields"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtPreOrgName" runat="server" MaxLength="50" Height="20" Width="250px"
                                                                CssClass="textbox_EB" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td valign="top" width="35%">
                                                <div id="dvProfileImg">
                                                    <asp:Label ID="lblPath" runat="server" Visible="false"></asp:Label>
                                                    <img runat="server" id="imgprofileImg" alt="" src="#" />
                                                    <asp:HiddenField ID="hProfileImgID" runat="server" Value="0" />
                                                </div>
                                                <div style="clear: both;">
                                                </div>
                                                <script language="javascript" type="text/javascript">
                                                </script>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                <asp:Label ID="lblCap4" runat="server" Font-Bold="True" Text="My Presenter Bio" CssClass="frmHeading"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                <div class="FormCont Steps">
                                    <table border="0" width="100%">
                                        <tr>
                                            <td>
                                                <div id="dvRTBio">
                                                    <telerik:RadEditor runat="server" ID="redtBio" ToolbarMode="Default" Height="180px"
                                                        Width="80%" BorderStyle="None" CssClass="rteditor1" ToolsWidth="100%">
                                                        <CssFiles>
                                                            <telerik:EditorCssFile Value="~/Styles/RTEditor.css" />
                                                        </CssFiles>
                                                        <Tools>
                                                            <telerik:EditorToolGroup>
                                                                <telerik:EditorTool Name="Copy" />
                                                                <telerik:EditorTool Name="Cut" />
                                                                <telerik:EditorTool Name="Paste" />
                                                                <telerik:EditorSeparator />
                                                                <telerik:EditorTool Name="DecreaseSize" />
                                                                <telerik:EditorTool Name="IncreaseSize" />
                                                                <telerik:EditorTool Name="FindAndReplace" />
                                                                <telerik:EditorTool Name="Font" />
                                                                <telerik:EditorSeparator />
                                                                <telerik:EditorTool Name="BackColor" />
                                                                <telerik:EditorTool Name="ForeColor" />
                                                                <telerik:EditorSeparator />
                                                                <telerik:EditorTool Name="Bold" />
                                                                <telerik:EditorTool Name="Italic" />
                                                                <telerik:EditorTool Name="Underline" />
                                                                <telerik:EditorTool Name="StrikeThrough" />
                                                                <telerik:EditorSeparator />
                                                                <telerik:EditorTool Name="JustifyLeft" />
                                                                <telerik:EditorTool Name="JustifyCenter" />
                                                                <telerik:EditorTool Name="JustifyRight" />
                                                                <telerik:EditorTool Name="JustifyFull" />
                                                                <telerik:EditorSeparator />
                                                                <telerik:EditorTool Name="Indent" />
                                                                <telerik:EditorTool Name="Outdent" />
                                                                <telerik:EditorSeparator />
                                                                <telerik:EditorTool Name="InsertHorizontalRule" />
                                                                <telerik:EditorTool Name="InsertUnorderedList" />
                                                                <telerik:EditorTool Name="InsertOrderedList" />
                                                                <telerik:EditorTool Name="InsertLink" />
                                                            </telerik:EditorToolGroup>
                                                        </Tools>
                                                    </telerik:RadEditor>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
        <tr><td><asp:Button ID="btnSave" runat="server" CssClass="SubBtn" Text="Save" 
                onclick="btnSave_Click" /> </td></tr>
    </table>
    </form>
</body>
</html>

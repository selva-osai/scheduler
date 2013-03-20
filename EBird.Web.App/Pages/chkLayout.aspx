<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="chkLayout.aspx.cs" Inherits="EBird.Web.App.Pages.chkLayout" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link id="layoutCSS" type="text/css" rel="Stylesheet" href="~/Styles/layout/layout2.css"
        runat="server" />
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table class="content" align="center">
        <tbody>
            <tr>
                <td class="tleft">
                </td>
                <td class="tmid">
                </td>
                <td class="tright">
                </td>
            </tr>
            <tr>
                <td class="lmid">
                </td>
                <td class="cmc">
                    <!--Header place holder-->
                    <asp:PlaceHolder ID="phContent" runat="server">
                        <table width="98%" align="center" border="0">
                            <tr>
                                <td colspan="2">
                                    <p>
                                        <asp:Label ID="lblWebinarTitle" CssClass="regWebTitle" runat="server"></asp:Label></p>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:PlaceHolder ID="phHeader" runat="server">
                                        <table runat="server" id="tblHeader" width="100%" border="0">
                                            <tr>
                                                <td width="100%">
                                                
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:PlaceHolder>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    
                                </td>
                                <td>
                                    
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" width="55%">
                                    
                                </td>
                                <td valign="top" width="45%">
                                    <dl class="expander">
                                        <dd>
                                            <p style="text-align: justify; padding-bottom: 3px;">
                                    
                                            </p>
                                        </dd>
                                    </dl>
                                    
                                    
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <br />
                                </td>
                            </tr>
                        </table>
                    </asp:PlaceHolder>
                </td>
                <td class="rmid">
                </td>
            </tr>
            <tr>
                <td class="bleft">
                </td>
                <td class="bmid">
                </td>
                <td class="bright">
                </td>
            </tr>
        </tbody>
    </table>
    </form>
</body>
</html>

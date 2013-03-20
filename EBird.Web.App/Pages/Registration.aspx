<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Registration.aspx.cs" Inherits="EBird.Web.App.Pages.Registration" %>

<%@ Register Src="~/Pagelets/plLogos.ascx" TagName="plLogos" TagPrefix="uc1" %>
<%@ Register Src="~/Pagelets/plRegForm.ascx" TagName="plRegForm" TagPrefix="uc2" %>
<%@ Register Src="~/Pagelets/plPresenters.ascx" TagName="plRegPresenter" TagPrefix="uc3" %>
<%@ Register Src="~/Pagelets/plPreRegEmail.ascx" TagName="plPreEmail" TagPrefix="uc4" %>
<%@ Register Src="~/Pagelets/plDateTime.ascx" TagName="plDateTime" TagPrefix="uc5" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title id="pgReg" runat="server"></title>
    <link id="layoutCSS" type="text/css" rel="Stylesheet" runat="server" />
    <script src="/js/jquery-1.7.1.min.js"></script>
    <script src="/js/jquery.expander.js"></script>
    <script>
        $(document).ready(function () {
            var opts = { collapseTimer: 4000 };

            $.each(['beforeExpand', 'afterExpand', 'onCollapse'], function (i, callback) {
                opts[callback] = function (byUser) {
                    var by, msg = '<div class="success">' + callback;

                    if (callback == 'onCollapse') {
                        msg += ' (' + (byUser ? 'user' : 'timer') + ')';
                    }
                    msg += '</div>';

                    $(this).parent().parent().append(msg)
                }
            });

            $('dl.expander dd').eq(0).expander();
            $('dl.expander dd').slice(1).expander(opts);
            $('div.expander').expander();
        });
</script>
</head>
<body runat="server" id="regPage">
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
                                                    <uc1:plLogos ID="plLogos1" runat="server" />
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:PlaceHolder>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <uc4:plPreEmail ID="plPreEmail1" runat="server" />
                                </td>
                                <td>
                                    <uc5:plDateTime ID="plDateTime1" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" width="55%">
                                    <uc2:plRegForm ID="plRegForm1" runat="server" />
                                </td>
                                <td valign="top" width="45%">
                                    <dl class="expander">
                                        <dd>
                                            <p style="text-align: justify; padding-bottom: 3px;">
                                                <asp:Label ID="lblWebinarDesc" CssClass="regWebDesc" runat="server"></asp:Label>
                                            </p>
                                        </dd>
                                    </dl>
                                    <hr style="border-bottom: 0px dotted #c0c0c0;">
                                    <uc3:plRegPresenter ID="plRegPresenter1" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <br />
                                    <asp:PlaceHolder ID="phError" runat="server" Visible="false">
                                        <p style="min-height: 500px; text-align: center">
                                            <asp:Label ID="lblError" runat="server" Text="You have reached wrong page or the content expired or the event key is invalid"
                                                ForeColor="Red"></asp:Label>
                                        </p>
                                    </asp:PlaceHolder>
                                </td>
                            </tr>
                        </table>
                    </asp:PlaceHolder>
                    <asp:PlaceHolder ID="phFooter" runat="server"></asp:PlaceHolder>
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

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MySnapSite.aspx.cs" Inherits="EBird.Web.App.Pages.MySnapSite" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Src="~/Pagelets/plLogos.ascx" TagName="plLogos" TagPrefix="uc1" %>
<%@ Register Src="~/Pagelets/plSMTracker.ascx" TagName="plSMT" TagPrefix="uc2" %>
<%@ Register Src="~/Pagelets/plFooter.ascx" TagName="plFooter" TagPrefix="uc6" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title id="pgSS" runat="server"></title>
    <link id="Link1" type="text/css" rel="Stylesheet" href="~/styles/layout/ThemeGen.css"
        runat="server" />
    <link id="layoutCSS" type="text/css" rel="Stylesheet" href="~/styles/layout/theme1.css"
        runat="server" />
    <!--[if lte IE 8]><link rel="stylesheet" href="Css/lte-ie-8.css"><![endif]-->
    <script src="/Js/jquery-1.7.1.min.js" type="text/javascript"></script>
</head>
<body>
    <div id="fb-root">
    </div>
    <script>
        (function (d, s, id) {
            var js, fjs = d.getElementsByTagName(s)[0];
            if (d.getElementById(id)) return;
            js = d.createElement(s); js.id = id;
            js.src = "//connect.facebook.net/en_US/all.js#xfbml=1";
            fjs.parentNode.insertBefore(js, fjs);
        } (document, 'script', 'facebook-jssdk'));</script>
    <script>
        !function (d, s, id) { var js, fjs = d.getElementsByTagName(s)[0]; if (!d.getElementById(id)) { js = d.createElement(s); js.id = id; js.src = "//platform.twitter.com/widgets.js"; fjs.parentNode.insertBefore(js, fjs); } } (document, "script", "twitter-wjs");
    </script>
    <script src="//platform.linkedin.com/in.js" type="text/javascript"></script>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:PlaceHolder ID="phMain" runat="server" Visible="true">
        <div style="margin: 0px auto; width: 630px; overflow: auto; clear: both;">
            <a href="https://twitter.com/share" class="twitter-share-button">Tweet</a>
            <script type="IN/Share" data-counter="right"></script>
            <div class="fb-like" data-send="true" data-width="450" data-show-faces="true">
            </div>
        </div>
        <div class="Registration-Cont mainBox" style="margin-top: 5px">
            <div class="Webinar-Title" runat="server" id="dvTitle">
                <asp:Label ID="lblSSTitle" CssClass="regWebTitle" runat="server"></asp:Label>
            </div>
            <div class="Webi-Banner" id="dvLogo" runat="server">
                <uc1:plLogos ID="plLogos1" runat="server" />
            </div>
            <asp:PlaceHolder ID="phContent" runat="server" Visible="false">
                <div id="dvSSContentContainer">
                    <div class="SS-Desc" runat="server" id="dvDesc">
                        <asp:Label ID="lblDesc" CssClass="ssDesc" runat="server"></asp:Label>
                    </div>
                    <div class="SS-Content">
                        <asp:HiddenField ID="hWebinarID" Value="0" runat="server" />
                        <table cellspacing="0" style="width: 100%;">
                            <tr>
                                <td style="width: 267px">
                                    <telerik:RadTabStrip runat="server" ID="rtabBio" MultiPageID="rmpgSS" SelectedIndex="0"
                                        CausesValidation="false" ReorderTabsOnSelect="True">
                                        <Tabs>
                                            <telerik:RadTab Text="Upcoming Webinars" Selected="True" />
                                            <telerik:RadTab Text="Past Webinars" />
                                        </Tabs>
                                    </telerik:RadTabStrip>
                                </td>
                                <td style="border-bottom: 1px solid #c0c0c0; width: 382px;">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="border-left: 1px solid #c0c0c0; border-bottom: 1px solid #c0c0c0;
                                    border-right: 1px solid #c0c0c0">
                                    <telerik:RadMultiPage runat="server" ID="rmpgSS" SelectedIndex="0" Height="320px"
                                        Width="600px">
                                        <telerik:RadPageView runat="server" ID="rpgV1">
                                            <div class="regOutline">
                                                <div id='mycustomscroll' class='scroll'>
                                                    <asp:Repeater ID="rpUpcoming" runat="server" OnItemDataBound="rpUpcoming_ItemDataBound"
                                                        OnItemCommand="rpUpcoming_ItemCommand">
                                                        <HeaderTemplate>
                                                            <table style="width: 600px;" cellspacing="0">
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td rowspan="2" style="width: 200px; height: 110px; vertical-align: middle; text-align: center">
                                                                    <asp:ImageButton ImageAlign="AbsMiddle" runat="server" ID="imgArticle" />
                                                                </td>
                                                                <td style="width: 400px; height: 110px; vertical-align: top; text-align: left; padding: 15px 5px 5px 5px;">
                                                                    <asp:Label ID="lblContentSummary" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 400px; height: 20px; vertical-align: middle; text-align: left">
                                                                    <asp:LinkButton ID="lbtnReg" runat="server" CssClass="lbtn1" CommandName="register"
                                                                        CommandArgument='<%# Eval("WebinarID") %>' Text="Register"></asp:LinkButton>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            </table>
                                                        </FooterTemplate>
                                                    </asp:Repeater>
                                                </div>
                                            </div>
                                        </telerik:RadPageView>
                                        <telerik:RadPageView runat="server" ID="rpgV2">
                                            <div class="regOutline">
                                                <div id='Div1' class='scroll'>
                                                    <asp:Repeater ID="rpPast" runat="server" OnItemDataBound="rpPast_ItemDataBound" OnItemCommand="rpPast_ItemCommand">
                                                        <HeaderTemplate>
                                                            <table style="width: 600px;" cellspacing="0">
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td rowspan="2" style="width: 200px; height: 110px; vertical-align: middle; text-align: center">
                                                                    <asp:ImageButton ImageAlign="AbsMiddle" runat="server" ID="imgArticle" />
                                                                </td>
                                                                <td style="width: 400px; height: 110px; vertical-align: top; text-align: left; padding: 15px 5px 5px 5px;">
                                                                    <asp:Label ID="lblContentSummary" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 400px; height: 20px; vertical-align: middle; text-align: left">
                                                                    <asp:LinkButton ID="lbtnReg" runat="server" CssClass="lbtn1" CommandName="register"
                                                                        CommandArgument='<%# Eval("WebinarID") %>' Text="Register"></asp:LinkButton>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            </table>
                                                        </FooterTemplate>
                                                    </asp:Repeater>
                                                    <table style="min-height: 200px; text-align: center; vertical-align: middle; width: 100%;">
                                                        <tr>
                                                            <td>
                                                                There is no published past webinars available for view
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </div>
                                        </telerik:RadPageView>
                                    </telerik:RadMultiPage>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </asp:PlaceHolder>
            <asp:PlaceHolder ID="phUnAvail" runat="server" Visible="false">SnapSite is no longer
                public </asp:PlaceHolder>
            <asp:PlaceHolder ID="phNotConfig" runat="server" Visible="false">This SnapSite is not
                configured yet for public</asp:PlaceHolder>
        </div>
    </asp:PlaceHolder>
    <asp:PlaceHolder ID="phInvalid" runat="server" Visible="false">
        <p style="text-align: center; vertical-align: middle; height: 300px; width: 500px;">
            Invalid SnapSite
        </p>
    </asp:PlaceHolder>
    <div class="MySS-footer">
        <uc6:plFooter ID="plFooter1" runat="server" />
    </div>
    <asp:HiddenField ID="hModalStatusFlg" runat="server" />
    </form>
</body>
</html>

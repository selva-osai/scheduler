<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Registration1.aspx.cs"
    Inherits="EBird.Web.App.Pages.Registration1" %>
<%@ Register Src="~/Pagelets/plLogos.ascx" TagName="plLogos" TagPrefix="uc1" %>
<%@ Register Src="~/Pagelets/plRegForm.ascx" TagName="plRegForm" TagPrefix="uc2" %>
<%@ Register Src="~/Pagelets/plPresenters.ascx" TagName="plRegPresenter" TagPrefix="uc3" %>
<%@ Register Src="~/Pagelets/plPreRegEmail.ascx" TagName="plPreEmail" TagPrefix="uc4" %>
<%@ Register Src="~/Pagelets/plDateTime.ascx" TagName="plDateTime" TagPrefix="uc5" %>
<%@ Register Src="~/Pagelets/plFooter.ascx" TagName="plFooter" TagPrefix="uc6" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title id="pgReg" runat="server"></title>
    <link href="/Css/Style.css" rel="stylesheet" type="text/css" />
    <!--[if lte IE 8]><link rel="stylesheet" href="Css/lte-ie-8.css"><![endif]-->
    <script src="/Js/jquery-1.7.1.min.js" type="text/javascript"></script>
    <script>
        $(document).ready(function () {
            //$('input:text:first').focus();
        });
    </script>
</head>
<body style="background: #f5f5f5; margin-top: 10px">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
    <div class="Registration-Cont" style="background: #fff; border: solid 1px #ccc; border-radius: 6px;">
        <div class="Webinar-Title">
            <h1>
                <asp:Label ID="lblWebinarTitle" CssClass="regWebTitle" runat="server"></asp:Label></h1>
        </div>
        <div class="Webi-Banner">
            <uc1:plLogos ID="plLogos1" runat="server" />
        </div>
        <div class="Regi-Top-Sec">
            <uc4:plPreEmail ID="plPreEmail1" runat="server" />
            <div class="L-Part Clr">
                <uc2:plRegForm ID="plRegForm1" runat="server" />
            </div>
            <div class="Clr">
            </div>
        </div>
        <div class="FRight" style="width: 270px; margin-right: 12px;">
            <uc5:plDateTime ID="plDateTime1" runat="server" />
            <div class="Regi-Bott-Sec">
                <div class="Webi-Summary">
                    <p class="regWebDesc">
                        <asp:Label ID="lblWebinarDesc" CssClass="regWebDesc" runat="server"></asp:Label></p>
                </div>
            </div>
            <div class="greybdr">
                <uc3:plRegPresenter ID="plRegPresenter1" runat="server" />
            </div>
            <div class="Clr">
            </div>
            <br />
            &nbsp;
        </div>
    </div>
    <div class="Registration-footer">
        <uc6:plFooter ID="plFooter1" runat="server" />
    </div>
    </form>
</body>
</html>

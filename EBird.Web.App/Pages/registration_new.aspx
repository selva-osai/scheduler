<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="registration_new.aspx.cs"
    Inherits="EBird.Web.App.Pages.registration_new" %>

<%@ Register Src="~/Pagelets/plLogos.ascx" TagName="plLogos" TagPrefix="uc1" %>
<%@ Register Src="~/Pagelets/plRegForm.ascx" TagName="plRegForm" TagPrefix="uc2" %>
<%@ Register Src="~/Pagelets/plPresenters.ascx" TagName="plRegPresenter" TagPrefix="uc3" %>
<%@ Register Src="~/Pagelets/plPreRegEmail.ascx" TagName="plPreEmail" TagPrefix="uc4" %>
<%@ Register Src="~/Pagelets/plDateTime.ascx" TagName="plDateTime" TagPrefix="uc5" %>
<%@ Register Src="~/Pagelets/plFooter.ascx" TagName="plFooter" TagPrefix="uc6" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title id="pgReg" runat="server"></title>
    <link id="Link1" type="text/css" rel="Stylesheet" href="~/styles/layout/ThemeGen.css"
        runat="server" />
    <link id="layoutCSS" type="text/css" rel="Stylesheet" href="~/styles/layout/theme1.css"
        runat="server" />
    <!--[if lte IE 8]><link rel="stylesheet" href="Css/lte-ie-8.css"><![endif]-->
    <script src="/Js/jquery-1.7.1.min.js" type="text/javascript"></script>
    <script>
        $(document).ready(function () {
            //$('input:text:first').focus();
        });
    </script>
    <style>
        #plRegForm1_dv1 ul, li
        {
            list-style-type: none;
        }
        #dvSummary ul li
        {
            list-style-position: inside;
            margin-left: 10px;
        }
        div.moreblock ul li
        {
            list-style-type: disc;
            margin-left: 10px;
        }
        div.moreblock ol li
        {
            list-style-type: decimal;
            list-style-position: inside;
            margin-left: 10px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="Registration-Cont mainBox">
        <div class="Webinar-Title" runat="server" id="dvTitle">
            <h1>
                <asp:Label ID="lblWebinarTitle" CssClass="regWebTitle" runat="server"></asp:Label></h1>
        </div>
        <div class="Webi-Banner" id="dvLogo" runat="server">
            <uc1:plLogos ID="plLogos1" runat="server" />
        </div>
        <asp:PlaceHolder ID="phAll" runat="server">
            <div class="Regi-Top-Sec">
                <uc4:plPreEmail ID="plPreEmail1" runat="server" />
                <div class="L-Part Clr">
                    <uc2:plRegForm ID="plRegForm1" runat="server" />
                    <p style="text-align: center; width: 100%">
                        <asp:Label ID="lblRegStatus" runat="server" Text="Regisration is disabled..." Visible="false"></asp:Label>
                    </p>
                </div>
                <div class="Clr">
                </div>
            </div>
            <div class="FRight" style="width: 270px; margin-right: 12px;">
                <uc5:plDateTime ID="plDateTime1" runat="server" />
                <div class="Regi-Bott-Sec" id="dvSummary" runat="server">
                    <div class="Webi-Summary">
                        <div class="showmore">
                            <div class="moreblock">
                                <p class="regWebDesc">
                                    <asp:Label ID="lblWebinarDesc" CssClass="regWebDesc" runat="server"></asp:Label>
                                </p>
                            </div>
                        </div>
                    </div>
                    <div>
                    </div>
                </div>
                <div id="dvCForm" style="margin-bottom: 10px">
                    <asp:Button ID="btnRefCol1" runat="server" CssClass="Submit-btn dvbutton" Text="Refer a Colleague"
                        OnClick="btnRefCol1_Click" />
                    <asp:Button ID="Predummy3" runat="server" CssClass="Submit-btn dvbutton" Visible="false"
                        Text="Refer a Colleague" />
                </div>
                <div class="greybdr" runat="server" id="dvSpeaker">
                    <uc3:plRegPresenter ID="plRegPresenter1" runat="server" />
                </div>
                <div class="Clr">
                </div>
                <br />
                &nbsp;
            </div>
        </asp:PlaceHolder>
        <asp:PlaceHolder ID="phOpt1" runat="server" Visible="false">
            <uc4:plPreEmail ID="plPreEmail2" runat="server" />
            <div class="FLeft" style="width: 270px; height: 105px">
                <uc5:plDateTime ID="plDateTime2" runat="server" />
                <div class="Clr">
                </div>
                <br>
                &nbsp;
            </div>
            <div class="Regi-Top-Sec" id="dvPre" runat="server">
                <div class="L-Part Clr">
                    <uc2:plRegForm ID="plRegForm2" runat="server" />
                </div>
                <div class="Clr">
                </div>
            </div>
        </asp:PlaceHolder>
        <asp:PlaceHolder ID="phRegFormColleague" runat="server" Visible="false">
            <div id="dvRAFForm" runat="server" class="Reg-Field">
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="" ValidationGroup="grp3"
                    ShowSummary="False" DisplayMode="BulletList" ShowMessageBox="true" ForeColor="#FF0000" />
                <ul class="fld1" runat="server" id="ulcol2">
                    <li visible="true" runat="server" id="LiL1" style="line-height: 20px !important">
                        <asp:Label ID="lbldesc" runat="server" Text="Invite a Colleague's name and e-mail address and we will send a invitation on your behalf. We will not communicate with your colleagues without their consent.">
                        </asp:Label></li>
                    <li visible="true" runat="server" id="LiL2">
                        <asp:Label ID="lblCFName" runat="server" Text="Your Colleague's First Name"></asp:Label>
                        &nbsp;<asp:Label ID="lblM1" Text="*" runat="server" Visible="false"></asp:Label></li>
                    <li visible="true" runat="server" id="LiT1">
                        <asp:TextBox ID="txtCFName" runat="server" CssClass="textbox_EB"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RF1" ErrorMessage="Colleague First Name cannot be left blank"
                            Display="None" ForeColor="Red" runat="server" ValidationGroup="grp3" ControlToValidate="txtCFName" />
                    </li>
                    <li visible="true" runat="server" id="LiL3">
                        <asp:Label ID="lblCLName" runat="server" Text="Your Colleague's Last Name"></asp:Label>
                        &nbsp;<asp:Label ID="lblM2" Text="*" runat="server" Visible="false"></asp:Label></li>
                    <li visible="true" runat="server" id="LiT2">
                        <asp:TextBox ID="txtCLName" runat="server" CssClass="textbox_EB"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RF2" ErrorMessage="Colleague Last Name cannot be left blank"
                            Display="None" ForeColor="Red" runat="server" ValidationGroup="grp3" ControlToValidate="txtCLName" />
                    </li>
                    <li visible="true" runat="server" id="LiL4">
                        <asp:Label ID="lblCEmail" runat="server" Text="Your Colleague's Email Address"></asp:Label>
                        &nbsp;<asp:Label ID="lblM3" Text="*" runat="server" Visible="false"></asp:Label></li>
                    <li visible="true" runat="server" id="LiT3">
                        <asp:TextBox ID="txtCEmail" runat="server" CssClass="textbox_EB"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RF3" ErrorMessage="Colleague Email Address cannot be left blank"
                            Display="None" ForeColor="Red" runat="server" ValidationGroup="grp3" ControlToValidate="txtCEmail" />
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtCEmail"
                            ValidationGroup="grp3" Display="None" ForeColor="Red" ErrorMessage="Colleague Email must be in the form someone@domain.com. Check leading and trailing space(s)."
                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                    </li>
                    <li visible="true" runat="server" id="LiL5">
                        <asp:Label ID="lblFName" runat="server" Text="Your First Name"></asp:Label>
                        &nbsp;<asp:Label ID="lblM4" Text="*" runat="server" Visible="false"></asp:Label></li>
                    <li visible="true" runat="server" id="LiT4">
                        <asp:TextBox ID="txtFName" runat="server" CssClass="textbox_EB"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RF4" ErrorMessage="Your First Name cannot be left blank"
                            Display="None" ForeColor="Red" runat="server" ValidationGroup="grp3" ControlToValidate="txtFName" />
                    </li>
                    <li visible="true" runat="server" id="LiL6">
                        <asp:Label ID="lblLName" runat="server" Text="Your Last Name"></asp:Label>
                        &nbsp;<asp:Label ID="lblM5" Text="*" runat="server" Visible="false"></asp:Label></li>
                    <li visible="true" runat="server" id="LiT5">
                        <asp:TextBox ID="txtLName" runat="server" CssClass="textbox_EB"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RF5" ErrorMessage="Your Last Name cannot be left blank"
                            Display="None" ForeColor="Red" runat="server" ValidationGroup="grp3" ControlToValidate="txtLName" />
                    </li>
                    <li visible="true" runat="server" id="LiL7">
                        <asp:Label ID="lblEmail" runat="server" Text="Your Email Address"></asp:Label>
                        &nbsp;<asp:Label ID="lblM6" Text="*" runat="server" Visible="false"></asp:Label></li>
                    <li visible="true" runat="server" id="LiT6">
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="textbox_EB"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RF6" ErrorMessage="Your Email Address cannot be left blank"
                            Display="None" ForeColor="Red" runat="server" ValidationGroup="grp3" ControlToValidate="txtEmail" />
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtEmail"
                            ValidationGroup="grp3" Display="None" ForeColor="Red" ErrorMessage="Your Email must be in the form someone@domain.com. Check leading and trailing space(s)."
                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                    </li>
                </ul>
                <img src="/Images/blank.gif" height="10" />
                <div id="Div2" runat="server">
                    <asp:Button ID="btnReferCollegue" runat="server" CssClass="Submit-btn dvbutton" Text="Send"
                        ValidationGroup="grp3" OnClick="btnReferCollegue_Click" />
                </div>
            </div>
            <div id="dvRefConf" runat="server" visible="false">
                <asp:Literal ID="ltrRefConf" runat="server"></asp:Literal>
            </div>
            <div id="dvRefExist" runat="server" visible="false">
                <asp:Literal ID="ltrRefExist" runat="server"></asp:Literal>
            </div>
        </asp:PlaceHolder>
    </div>
    <div class="Registration-footer">
        <uc6:plFooter ID="plFooter1" runat="server" />
    </div>
    <asp:HiddenField ID="hWebinarID" runat="server" />
    <asp:HiddenField ID="hphReg" runat="server" />
    <asp:HiddenField ID="hModalStatusFlg" runat="server" />
    <telerik:RadCodeBlock ID="ShowMoreShowLess" runat="server">
        <script type="text/javascript">
            function showMore() {
                // The height of the content block when it's not expanded
                var adjustheight = 153;
                var adjustheightpresenter = 92;
                var moreText = "show more";
                var lessText = "show less";
                $(".showmore > .moreblock").each(function () {
                    if ($(this).height() > adjustheight) {
                        $(this).css('height', adjustheight).css('overflow', 'hidden');
                        $(this).parent(".showmore").append('<br><a href="#" class="adjust"></a>');
                        $(this).parent(".showmore").find("a.adjust").text(moreText);
                        $(this).parent(".showmore").find(".adjust").toggle(function () {
                            $(this).parents("div:first").find(".moreblock").css('height', 'auto').css('overflow', 'visible');
                            $(this).parents("div:first").find("p.continued").css('display', 'none');
                            $(this).text(lessText);
                        }, function () {
                            $(this).parents("div:first").find(".moreblock").css('height', adjustheight).css('overflow', 'hidden');
                            $(this).parents("div:first").find("p.continued").css('display', 'block');
                            $(this).text(moreText);
                        });
                    }
                });
                $(".showmore-presenter > .moreblock-presenter").each(function () {
                    if ($(this).height() > adjustheightpresenter) {
                        $(this).css('height', adjustheightpresenter).css('overflow', 'hidden');
                        $(this).parent(".showmore-presenter").append('<br><a href="#" class="adjust"></a>');
                        $(this).parent(".showmore-presenter").find("a.adjust").text(moreText);
                        $(this).parent(".showmore-presenter").find(".adjust").toggle(function () {
                            $(this).parents("div:first").find(".moreblock-presenter").css('height', 'auto').css('overflow', 'visible');
                            $(this).parents("div:first").find("p.continued").css('display', 'none');
                            $(this).text(lessText);
                        }, function () {
                            $(this).parents("div:first").find(".moreblock-presenter").css('height', adjustheightpresenter).css('overflow', 'hidden');
                            $(this).parents("div:first").find("p.continued").css('display', 'block');
                            $(this).text(moreText);
                        });
                    }
                });
            }
            showMore(); 
        </script>
    </telerik:RadCodeBlock>
    </form>
</body>
</html>

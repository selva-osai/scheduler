<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WaitingRoom.aspx.cs" Inherits="EBird.Web.App.Pages.WaitingRoom" %>

<%@ Register Src="~/Pagelets/plLogos.ascx" TagName="plLogos" TagPrefix="uc1" %>
<%@ Register Src="~/Pagelets/plPresenters.ascx" TagName="plRegPresenter" TagPrefix="uc3" %>
<%@ Register Src="~/Pagelets/plFooter.ascx" TagName="plFooter" TagPrefix="uc6" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
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
        div.WebDesc-Top-Sec ul li 
        {
            list-style-position: inside;
            list-style-type:disc;
        }
        div.WebDesc-Top-Sec ol li
        {
            list-style-type:decimal;
            list-style-position: inside;
        }
    </style>
</head>
<body>
    <script type="text/javascript" src="/js/myscroll.js"></script>
    <link id="link2" href="/css/scrollbar.css" rel="stylesheet" type="text/css" />
    <form id="form1" runat="server">
    <div id='regpgscroll' class='scroll'>
    <div class="Registration-Cont mainBox">
        <div class="Webinar-Title" runat="server" id="dvTitle">
            <h1>
                <asp:Label ID="lblWebinarTitle" CssClass="regWebTitle" runat="server"></asp:Label></h1>
        </div>
        <div class="Webi-Banner" id="dvLogo" runat="server">
            <uc1:plLogos ID="plLogos1" runat="server" />
        </div>
        <div class="Webi-Banner">
            <asp:Literal ID="ltrInstruction" runat="server"></asp:Literal>
        </div>
        <div class="WebDesc-Top-Sec">
            <div class="Reg-Field  greybdr">
             <div class="showmore">
               <div class="moreblock">
                <p class="regWebDesc"><asp:Label ID="lblWebinarDesc" CssClass="regWebDesc" runat="server"></asp:Label></p>
               </div>
             </div>
            </div>
            <img src="/Images/blank.gif" height="10" />
            <div id="dvbutton" runat="server" style="text-align: center;">
                <asp:Button ID="btnLaunch" runat="server" CssClass="Submit-btn dvbutton" Text="LAUNCH PRESENTATION" />
            </div>
        </div>
        <div class="FRight" style="width: 270px; margin-right: 12px;">
            <div class="greybdr">
                <uc3:plRegPresenter ID="plRegPresenter1" runat="server" />
            </div>
            <br />
            &nbsp;
        </div>
    </div>
    </div> 
    <div class="Registration-footer">
        <uc6:plFooter ID="plFooter1" runat="server" />
    </div>
    
     <telerik:RadCodeBlock ID="ShowMoreShowLess" runat="server">
        <script type="text/javascript">
            function showMore() {
                // The height of the content block when it's not expanded
                var adjustheight = 186;
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

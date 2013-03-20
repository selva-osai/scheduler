<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ssadmin.ascx.cs" Inherits="EBird.Web.App.UserControls.mysnapsite" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<link href="../Styles/ThemeStyle.css" rel="stylesheet" type="text/css" />
<link href="/css/carousel1.css" rel="stylesheet" type="text/css" />
<div class="RPart">
    <div class="TopBg">
    </div>
    <div class="widgets">
        <div id="snapContainer">
            <table width="100%">
                <tr>
                    <td width="450px">
                        &nbsp;<asp:HiddenField ID="hWebinarID" runat="server" />
                    </td>
                    <td align="right" width="170px">
                        <asp:CheckBox ID="chkDisableSS" runat="server" CssClass="chkSSDis" Text="Disable My SnapSite" />
                    </td>
                    <td width="80px">
                        <span style="float: right"><asp:HyperLink ID="hlnkSS" runat="server" NavigateUrl="" Target="_blank" Text="View My SnapSite"></asp:HyperLink></span>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <div class="FormCont Steps" style="width: 700px; margin-bottom: 10px;">
                            <table width="100%">
                                <tr>
                                    <td align="left" style="padding-bottom: 10px;">
                                        <asp:Label ID="lblpgCap2" runat="server" Font-Bold="True" Text="SnapSite Details"
                                            CssClass="frmHeading"></asp:Label>
                                    </td>
                                    <td align="right">
                                        <img src="../Images/icons/pop.png" id="imgPop" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <table width="100%" align="center">
                                            <tr>
                                                <td valign="top" width="175px" style="padding-left: 5px">
                                                    <div id="dvlogoUp">
                                                        <script type="text/javascript">
                                                            function checkHeaderTypeChange(sender, eventArgs) {
                                                                var lcount = document.getElementById("<%= hLogoCount.ClientID %>").value;
                                                                if (parseInt(lcount) != 0) {
                                                                    var item = eventArgs.get_item();
                                                                    if (item.get_value() == "BANNER") {
                                                                        alert("Without deleting the logos, you cannot change to different header type");
                                                                        eventArgs.set_cancel(true);
                                                                        return false;
                                                                    }
                                                                    else if (item.get_value() == "Logo") {
                                                                        alert("Without deleting the banner, you cannot change to different header type");
                                                                        eventArgs.set_cancel(true);
                                                                        return false;
                                                                    }
                                                                }
                                                            }
                                                        </script>
                                                        <telerik:RadComboBox ID="rcmbHeader" runat="server" Width="180" CausesValidation="False"
                                                            CssClass="cmbGen1" ViewStateMode="Enabled" AutoPostBack="True" OnClientSelectedIndexChanging="checkHeaderTypeChange"
                                                            OnSelectedIndexChanged="rcmbHeader_SelectedIndexChanged" MarkFirstMatch="True"
                                                            HighlightTemplatedItems="True" ExpandAnimation-Duration="1000" CollapseAnimation-Duration="1000"
                                                            CollapseAnimation-Type="OutQuart" NoWrap="True" EnableTheming="True" Font-Italic="False"
                                                            Skin="Default">
                                                            <Items>
                                                                <telerik:RadComboBoxItem runat="server" Text="Upload Logos" Value="Logo" Selected="True"
                                                                    CssClass="ddStyle1" />
                                                                <telerik:RadComboBoxItem runat="server" Text="Upload Banner" Value="Banner" CssClass="ddStyle1" />
                                                            </Items>
                                                        </telerik:RadComboBox>
                                                        <asp:HiddenField ID="hLogoCount" Value="0" runat="server" />
                                                    </div>
                                                </td>
                                                <td>
                                                    <telerik:RadAsyncUpload runat="server" ID="aupLogo" AllowedFileExtensions="jpeg,jpg,gif,png"
                                                        Localization-Select="Upload" MaxFileInputsCount="1" OnFileUploaded="aupLogo_FileUploaded"
                                                        HttpHandlerUrl="~/handler/VideoUploadT1.ashx" OnClientFileUploaded="fileUploaded"
                                                        OnClientValidationFailed="validationFailed">
                                                    </telerik:RadAsyncUpload>
                                                    <asp:HiddenField ID="hCurrentImg" runat="server" Value="1" />
                                                    <asp:LinkButton ID="lnkUpdateLogo" runat="server" Text="" OnClick="lnkUpdateLogo_Click"
                                                        CausesValidation="false"></asp:LinkButton>
                                                </td>
                                            </tr>
                                        </table>
                                        <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
                                            <script type="text/javascript">
                                                function fileUploaded(sender, args) {

                                                    $telerik.$(".invalid").html("");
                                                    setTimeout(function () {
                                                        sender.deleteFileInputAt(0);
                                                    }, 10);
                                                    __doPostBack('ctl00$ContentPlaceHolder1$schMeeting1$webRegistration1$lnkUpdateLogo', '');
                                                }
                                                function validationFailed(sender, args) {
                                                    $telerik.$(".invalid")
                                            .html("Invalid extension, please choose an image file")
                                            .get(0).style.display = "block";
                                                    sender.deleteFileInputAt(0);
                                                }
                                            </script>
                                        </telerik:RadCodeBlock>
                                        <div style="clear: both">
                                        </div>
                                        <div class="logo-canvas" runat="server" id="logocanvas" visible="false" style="padding-left: 5px">
                                            <asp:PlaceHolder ID="phLogo" runat="server" Visible="false">
                                                <ul id="sortable">
                                                    <asp:Repeater ID="rpLogo" runat="server" OnItemDataBound="rpLogo_ItemDataBound" OnItemCommand="rpLogo_ItemCommand">
                                                        <ItemTemplate>
                                                            <div id="dvLogoContainer" runat="server">
                                                                <li class="ui-state-default" id='<%# DataBinder.Eval(Container.DataItem, "DocID") %>'>
                                                                    <div class="binary-image" id="bimg1" runat="server">
                                                                        <asp:Image ID="imgLogo" runat="server" />
                                                                    </div>
                                                                    <div class="image-remove" id="bimgr1" runat="server">
                                                                        <asp:ImageButton ID="imgbtnDel" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "DocID") %>'
                                                                            onmouseover="this.src='/images/icons/icon-delete.png'" onmouseout="this.src='/images/icons/icon-delete1.png'"
                                                                            runat="server" ImageUrl="/images/icons/icon-delete1.png"></asp:ImageButton>
                                                                    </div>
                                                                </li>
                                                            </div>
                                                            <asp:HiddenField ID="hDocID" Value='<%# DataBinder.Eval(Container.DataItem, "DocID") %>'
                                                                runat="server" />
                                                            <asp:HiddenField ID="hResourceType" Value='<%# DataBinder.Eval(Container.DataItem, "ResourceType") %>'
                                                                runat="server" />
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </ul>
                                                <script>
                                                    $(document).ready(function () {
                                                        $("li.ui-state-default").hover(
										                  function () {
										                      $(this).find('.image-remove').show();
										                  },
										                      function () {
										                          $(this).find('.image-remove').hide();
										                      }
										                    );
                                                        $("li.bannerList").hover(
										                  function () {
										                      $(this).find('.bnr-image-remove').show();
										                  },
										                  function () {
										                      $(this).find('.bnr-image-remove').hide();
										                  }
										                );
                                                    });
                                                </script>
                                                <asp:HiddenField ID="hdynImgIDs" Value="" runat="server" />
                                                <style>
                                                    #sortable
                                                    {
                                                        list-style-type: none;
                                                        margin: 0;
                                                        padding: 0;
                                                    }
                                                    #sortable li
                                                    {
                                                        margin: 3px 3px 3px 0;
                                                        padding: 1px;
                                                        float: left;
                                                        width: 180px;
                                                        height: 90px;
                                                        font-size: 4em;
                                                        text-align: center;
                                                    }
                                                    .bannerWrapper
                                                    {
                                                        border: 1px solid #CCC;
                                                        background: #F6F6F6;
                                                        font-weight: bold;
                                                        color: #1C94C4;
                                                        height: 99px;
                                                    }
                                                </style>
                                                <script src="/js/jquery.sortableExtra.js"></script>
                                                <script>
                                                    $(function () {
                                                        $("#sortable").sortable();
                                                        $("#sortable").disableSelection();
                                                    });
                                                    $('#sortable').sortableExtra().bind('sortupdate', function (e, ui) {
                                                        var idsInOrder = $('#sortable').sortable("toArray");
                                                        var IDs = document.getElementById("<%= hdynImgIDs.ClientID %>");
                                                        var ID1 = "";
                                                        for (var i = 0; i < idsInOrder.length; i++) {
                                                            ID1 = ID1 + idsInOrder[i] + ","
                                                        }
                                                        document.getElementById("<%= hdynImgIDs.ClientID %>").value = ID1;
                                                    });
                                                </script>
                                            </asp:PlaceHolder>
                                            <asp:PlaceHolder ID="phBanner" runat="server" Visible="false">
                                                <ul>
                                                    <li class="bannerList" style="width: 600px;" id='liBanner'>
                                                        <div id="dvBannerContainer" class="bannerWrapper" style="width: 604px !important;"
                                                            runat="server">
                                                            <asp:Image ID="imgBanner" runat="server" />
                                                            <div class="bnr-image-remove" id="bimgr2" runat="server">
                                                                <asp:ImageButton ID="imgbtnDel2" onmouseover="this.src='/images/icons/icon-delete.png'"
                                                                    onmouseout="this.src='/images/icons/icon-delete1.png'" runat="server" ImageUrl="/images/icons/icon-delete1.png">
                                                                </asp:ImageButton>
                                                                <asp:HiddenField ID="hBannerDocID" runat="server" />
                                                            </div>
                                                        </div>
                                                    </li>
                                                </ul>
                                            </asp:PlaceHolder>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                            <table width="100%">
                                <tr>
                                    <td valign="top" width="15%" style="padding-left: 5px">
                                        <asp:Label ID="lblTitle" runat="server" Text="Title"></asp:Label><span class='EBmsg'>&nbsp;*</span>
                                    </td>
                                    <td class="Row LongText" width="85%">
                                        <asp:TextBox ID="txtSSTitle" runat="server" MaxLength="120" Width="572" CssClass="textbox_EB"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="vx_txtSSTitle" runat="server" CssClass="ValidationSummary"
                                            ValidationGroup="grp1" ControlToValidate="txtSSTitle" ErrorMessage="Please enter required field - SnapSite Title"
                                            ForeColor="Red" Display="None" Text="*" />
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top" style="padding-left: 5px">
                                        <asp:Label ID="lblDescription" runat="server" Text="Description"></asp:Label><span
                                            class='EBmsg'>&nbsp;*</span>
                                    </td>
                                    <td class="Row LongText">
                                        <div id="dvDesc">
                                            <telerik:RadEditor runat="server" ID="redtDesc" ToolbarMode="Default" Height="180px"
                                                ToolsFile="~/editor/BasicTools2.xml" BorderStyle="None" CssClass="rteditor1"
                                                ToolsWidth="100%" EnableResize="False" EditModes="Design" Width="581px">
                                                <CssFiles>
                                                    <telerik:EditorCssFile Value="~/Styles/RTEditor.css" />
                                                </CssFiles>
                                                <ContextMenus>
                                                    <telerik:EditorContextMenu Enabled="false" />
                                                    <telerik:EditorContextMenu TagName="IMG" Enabled="false" />
                                                    <telerik:EditorContextMenu TagName="A" Enabled="false" />
                                                    <telerik:EditorContextMenu TagName="TABLE" Enabled="false" />
                                                </ContextMenus>
                                            </telerik:RadEditor>
                                        </div>
                                        <asp:RequiredFieldValidator ID="vx_txtDescription" runat="server" CssClass="ValidationSummary"
                                            ValidationGroup="grp1" ControlToValidate="redtDesc" ErrorMessage="Please enter required field - Description"
                                            ForeColor="Red" Display="None" Text="*" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
                <!-- Select SS theme-->
                <tr>
                    <td colspan="3">
                        <div class="FormCont Steps" style="width: 700px; margin-bottom: 10px">
                            <table height="100">
                                <tr>
                                    <td align="left" style="padding-bottom: 10px;">
                                        <asp:Label ID="lblpgCap3" runat="server" Font-Bold="True" Text="Select a SnapSite Skin"
                                            CssClass="frmHeading"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <asp:Repeater ID="rpThemeCarousel" OnItemDataBound="rpThemeCarousel_ItemDataBound"
                                            runat="server">
                                            <HeaderTemplate>
                                                <div class="image_carousel">
                                                    <div id="foo2">
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <img src='~/Images/Theme/<%# DataBinder.Eval(Container.DataItem, "Name") %>' alt=""
                                                    runat="server" id="imgTheme" class="previewBtn" />
                                                <asp:HiddenField ID="hThemeLayoutID" Value='<%# DataBinder.Eval(Container.DataItem, "Name") %>'
                                                    runat="server" />
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                </div>
                                                <div class="clearfix">
                                                </div>
                                                <a class="prev prevSS" id="foo2_prev" href="#"><span>prev</span></a> <a class="next prevSS"
                                                    id="foo2_next" href="#"><span>next</span></a> </div>
                                            </FooterTemplate>
                                        </asp:Repeater>
                                        <asp:HiddenField ID="hSelThemeID" runat="server" Value="2" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
                <!-- Social Media Configute -->
                <tr>
                    <td colspan="3">
                        <div class="FormCont Steps" style="margin-bottom: 10px">
                            <table border="0" width="30%">
                                <tr>
                                    <td align="left" colspan="6" style="padding-bottom: 10px;">
                                        <asp:Label ID="Label1" runat="server" Font-Bold="True" Text="Configure Social Media Components"
                                            CssClass="frmHeading"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <img runat="server" id="cmpFB" src="~/images/features/facebook.png" alt="Facebook" />
                                    </td>
                                    <td>
                                        <table border="0" style="width: 180px">
                                            <tr>
                                                <td class="style1">
                                                    <asp:CheckBox ID="chkFB" runat="server" CssClass="chkGen chkAlign1" Text="Facebook"
                                                        value="" />
                                                    <span style="margin-top: 5px; float: left">
                                                        <asp:HyperLink ID="lbtnFB" runat="server" Text="[Edit]" Visible="false" CssClass="lnkBtn1b"
                                                            CausesValidation="false" />
                                                    </span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="padding-left: 23px;">
                                                    Allow access to designated Facebook page
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>
                                        <img runat="server" id="cmpTweeter" src="~/images/features/twitter.png" alt="Twitter" />
                                    </td>
                                    <td>
                                        <table border="0" style="width: 160px">
                                            <tr>
                                                <td class="style1">
                                                    <asp:CheckBox ID="chkTwit" runat="server" CssClass="chkGen chkAlign1" Text="Twitter"
                                                        value="" />
                                                    <span style="margin-top: 5px; float: left">
                                                        <asp:HyperLink ID="lbtnTwit" runat="server" Text="[Edit]" Visible="false" CssClass="lnkBtn1b"
                                                            CausesValidation="false" />
                                                    </span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="padding-left: 23px;">
                                                    Allow access to designated Twitter page
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>
                                        <img runat="server" id="cmpLI" src="~/images/features/linkedIn.png" alt="" />
                                    </td>
                                    <td>
                                        <table border="0" style="width: 160px">
                                            <tr>
                                                <td class="style1">
                                                    <asp:CheckBox ID="chkLIn" runat="server" CssClass="chkGen chkAlign1" Text="LinkedIn"
                                                        value="" />
                                                    <span style="margin-top: 5px; float: left">
                                                        <asp:HyperLink ID="lbtnLIn" runat="server" Text="[Edit]" Visible="false" CssClass="lnkBtn1b"
                                                            CausesValidation="false" />
                                                    </span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="padding-left: 23px;">
                                                    Allow access to designated LinkedIn page
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
                <!-- Public webinar -->
                <tr>
                    <td colspan="3">
                        <div class="FormCont Steps" style="margin-bottom: 10px">
                            <table border="0" width="100%">
                                <tr>
                                    <td align="left" style="padding-bottom: 10px;">
                                        <asp:Label ID="lblPubWebinar" runat="server" Font-Bold="True" Text="Select Public Webinars"
                                            CssClass="frmHeading"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table width="100%" align="center">
                                            <tr>
                                                <td valign="top">
                                                    <telerik:RadGrid ID="tgrdWebinarList" runat="server" GridLines="None" AllowPaging="True"
                                                        PageSize="15" MasterTableView-EditFormSettings-EditFormType="Template" AllowSorting="True"
                                                        AutoGenerateColumns="False" ValidationSettings-EnableValidation="false" OnItemCommand="tgrdWebinarList_ItemCommand"
                                                        OnItemDataBound="tgrdWebinarList_ItemDataBound" MasterTableView-NoDetailRecordsText="No public webinar available"
                                                        BorderColor="#D7D7D7">
                                                        <PagerStyle Mode="NumericPages" PagerTextFormat="Navigate pages {4} Page {0} of {1}, Webinars {2} to {3} of {5}" />
                                                        <ExportSettings HideStructureColumns="true" />
                                                        <MasterTableView CommandItemDisplay="Top" AutoGenerateColumns="false" AllowFilteringByColumn="False"
                                                            ShowFooter="false" TableLayout="Auto" Width="100%">
                                                            <CommandItemSettings ShowAddNewRecordButton="false" ShowExportToWordButton="true"
                                                                ShowRefreshButton="true" ShowExportToExcelButton="true" ShowExportToCsvButton="true"
                                                                ShowExportToPdfButton="true" />
                                                            <NoRecordsTemplate>
                                                                <center>
                                                                <br />
                                                                No public webinar available<br />
                                                                &nbsp;</center>
                                                            </NoRecordsTemplate>
                                                            <RowIndicatorColumn>
                                                                <HeaderStyle Width="20px" />
                                                            </RowIndicatorColumn>
                                                            <ExpandCollapseColumn>
                                                                <HeaderStyle Width="20px" />
                                                            </ExpandCollapseColumn>
                                                            <Columns>
                                                                <telerik:GridTemplateColumn HeaderText="Restore" UniqueName="Publish" HeaderStyle-Width="60px">
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="chkPublish" runat="server" />
                                                                        <asp:HiddenField ID="hWebinarID" runat="server" Value='<%# Eval("WebinarID") %>' />
                                                                    </ItemTemplate>
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridBoundColumn DataField="Title" HeaderText="Title" />
                                                                <telerik:GridTemplateColumn HeaderText="Date and Time" UniqueName="webinarDate" HeaderStyle-Width="160px">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblDateTime" runat="server"></asp:Label>
                                                                    </ItemTemplate>
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridBoundColumn DataField="Registered" HeaderText="Registrants" ItemStyle-HorizontalAlign="Center"
                                                                    HeaderStyle-Width="60px" />
                                                                <telerik:GridBoundColumn DataField="WebinarID" Visible="false" />
                                                                <telerik:GridBoundColumn DataField="startDate" Visible="false" />
                                                                <telerik:GridBoundColumn DataField="StartTime" Visible="false" />
                                                                <telerik:GridBoundColumn DataField="modifiedOn" Visible="false" />
                                                                <telerik:GridBoundColumn DataField="isSSPublished" Visible="false" />
                                                            </Columns>
                                                        </MasterTableView>
                                                        <ClientSettings>
                                                            <Scrolling AllowScroll="false" />
                                                            <Selecting AllowRowSelect="true" />
                                                            <ClientEvents OnRowMouseOver="RowMouseOver" OnRowMouseOut="RowMouseOut" />
                                                        </ClientSettings>
                                                    </telerik:RadGrid>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td height="32">
                                                    <a href="#" class="dummyPreview">Preview SnapSite</a>&nbsp;|&nbsp; <a href="#" class="dummyEmbed">
                                                        Embed My SnapSite</a>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        &nbsp;<asp:Button ID="btnPublish" OnClick="btnPublish_Click" runat="server" Text="Publish"
                            ValidationGroup="grp1" CssClass="SubBtn" />
                        &nbsp;<asp:Label ID="lblError" ForeColor="Green" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
            <telerik:RadCodeBlock ID="rcCarosel" runat="server">
                <script src="/js/jquery.carouFredSel-6.0.5-packed.js" type="text/javascript" charset="utf-8"></script>
                <script type="text/javascript">
                    function getHValue() {

                        var hVal = document.getElementById('<%=hSelThemeID.ClientID %>');
                        if (hVal != null)
                            return hVal.value;
                        else
                            return 3;
                    }

                    function isNewWebinar() {
                        var hVal = document.getElementById('<%=hSelThemeID.ClientID %>');

                        if (hVal != null)
                            if (hVal.value == 0)
                                return true;
                            else
                                return false;
                        else
                            return true;
                    }

                    jQuery(function ($) {
                        // Load dialog on click
                        $('.previewBtn').live('click', function (e) {
                            //var themeUrl = $(this).attr('data-typ');
                            var themeUrl = $(this).attr('data-themeurl');
                            //alert(themeUrl);
                            var selThemeID = document.getElementById('<%=hSelThemeID.ClientID %>');
                            selThemeID.value = themeUrl;
                            $('.image_carousel img').removeClass('selected');
                            $(this).addClass('selected');
                            return false;
                        });

                        $('.selectThemeBtn').live('click', function (e) {
                            //use the following themeId to handle things dynamically
                            $('#themeId').val($(this).attr('data-themeid'));
                        });
                    });
		
                </script>
                <script>
                    $("#foo2").carouFredSel({
                        items: 9,
                        start: getHValue(),
                        scroll: {
                            items: 9,
                            duration: 800,
                            timeoutDuration: 2000
                        },
                        auto: false,
                        prev: "#foo2_prev",
                        next: "#foo2_next"
                    }).parent().css("margin", "auto");
                </script>
            </telerik:RadCodeBlock>
        </div>
    </div>
    <div class="BottBg">
    </div>
    <img src="../Images/blank.gif" height="10" />
    <div class="Clr">
    </div>
    <script src="/qtip2/qtipCommonfn.js" type="text/javascript" charset="utf-8"></script>
    <script type="text/javascript">

        $(document).ready(function () {
            // Logo & banner pop-out
            $("#imgPop").click(function () {
                var hWebID = document.getElementById('<%=hWebinarID.ClientID %>');
                var URL = "/Pages/popup/imageCroping1.aspx?ID=" + hWebID.value;
                qtipPopup2("#imgPop",
                    "<div id='modalContainer'><div class='modalClose'><img src='/images/x.png' /></div><iframe id='popImgFrame' src=" + URL + " height='400' width='720' scrolling='no' class='bgfill'></iframe></div>",
                    720, ".modalClose", "/Pages/Schedule", "#popImgFrame", "body #ContentPlaceHolder1_hModalStatusFlg");
            });
        });
    </script>
</div>

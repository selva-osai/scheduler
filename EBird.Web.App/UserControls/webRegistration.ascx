<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="webRegistration.ascx.cs"
    Inherits="EBird.Web.App.UserControls.webRegistration" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<link href="../Styles/ThemeStyle.css" rel="stylesheet" type="text/css" />
<link href="/css/carousel1.css" rel="stylesheet" type="text/css" />
<style>
    div.image-remove
    {
        float: right;
        cursor: pointer;
        width: 16px;
        height: 16px;
        padding: 0;
        position: relative;
        margin-left: -30px;
        display: none;
    }
    div.image-remove:hover
    {
        float: right;
        cursor: pointer;
        width: 16px;
        height: 16px;
        padding: 0;
        position: relative;
    }
    div.bnr-image-remove
    {
        float: right;
        cursor: pointer;
        width: 16px;
        height: 16px;
        padding: 0;
        margin-left: -30px;
    }
    div.bnr-image-remove:hover
    {
        float: right;
        cursor: pointer;
        width: 16px;
        height: 16px;
        padding: 0;
    }
    .accordionHeader
    {
        background-image: url('/Images/arrow_expand.png');
        background-repeat: no-repeat;
        background-position: left center;
        padding: 5px 5px 5px 25px;
        border: 1px solid #f4f4f4;
        color: black;
        background-color: #f4f4f4;
        font-family: Verdana;
        font-size: 11px;
        font-weight: bold;
        margin-top: 3px;
        cursor: pointer;
        border-radius: 8px;
    }
    
    .accordionHeader:hover
    {
        padding: 5px 5px 5px 25px;
        border: 1px solid #f4f4f4;
        color: black;
        background-color: #f4f4f4;
        font-family: Verdana;
        font-size: 11px;
        font-weight: bold;
        margin-top: 3px;
        cursor: pointer;
        border-radius: 8px;
    }
    
    .accordionHeaderSelected
    {
        background-image: url('/Images/arrow_collapse.png');
        background-repeat: no-repeat;
        background-position: left center;
        padding: 5px 5px 5px 25px;
        border: 1px solid #f4f4f4;
        color: black;
        background-color: #f4f4f4;
        font-family: Verdana;
        font-size: 11px;
        font-weight: bold;
        margin-top: 3px;
        cursor: pointer;
        border-top-left-radius: 8px;
        border-top-right-radius: 8px;
    }
    
    .accordionContent
    {
        background-color: #f4f4f4;
        border: 0px dashed #f4f4f4;
        color: black;
        font-family: Verdana;
        font-size: 11px;
        font-weight: normal;
        border-top: none;
        padding-top: 10px;
        padding-left: 5px;
        border-bottom-left-radius: 8px;
        border-bottom-right-radius: 8px;
    }
    
    a.prev, a.next
    {
        height: 50px;
        position: absolute;
        top: 30px;
    }
    
    .ruUploadProgress, li .ruCancel, li .ruRemove
    {
        visibility: hidden;
    }
    li.ruUploading
    {
        display: none;
    }
    #imgPop
    {
        cursor: pointer;
    }
</style>
<div id="dvRegistration">
    <table width="100%">
        <tr>
            <td>
                <div class="FormCont Steps" style="width: 700px; margin-bottom: 10px;">
                    <table width="100%">
                        <tr>
                            <td align="left" style="padding-bottom: 10px;">
                                <asp:Label ID="lblpgCap2" runat="server" Font-Bold="True" Text="Webinar Header" CssClass="frmHeading"></asp:Label>
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
                                                        <telerik:RadComboBoxItem runat="server" Text="Upload Banner" Value="BANNER" CssClass="ddStyle1" />
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
                                <div class="logo-canvas" runat="server" id="logocanvas" visible="false" style="padding-left: 5px;
                                    height: 135px">
                                    <asp:PlaceHolder ID="phLogo" runat="server" Visible="false">
                                        <ul id="sortable">
                                            <asp:Repeater ID="rpLogo" runat="server" OnItemDataBound="rpLogo_ItemDataBound" OnItemCommand="rpLogo_ItemCommand">
                                                <ItemTemplate>
                                                    <div id="dvLogoContainer" runat="server">
                                                        <li class="ui-state-default" id='<%# DataBinder.Eval(Container.DataItem, "DocID") %>'>
                                                            <div class="binary-image" id="bimg1" runat="server">
                                                                <a href='<%# DataBinder.Eval(Container.DataItem, "LogoUrl") %>' target="_blank">
                                                                    <asp:Image ID="imgLogo" runat="server" BorderStyle="None" Width="180px" Height="80px"
                                                                        ToolTip='<%# DataBinder.Eval(Container.DataItem, "LogoUrlName") %>' />
                                                                </a>
                                                            </div>
                                                            <div class="image-remove" id="bimgr1" runat="server">
                                                                <asp:ImageButton ID="imgbtnDel" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "DocID") %>'
                                                                    onmouseover="this.src='/images/icons/icon-delete.png'" onmouseout="this.src='/images/icons/icon-delete1.png'"
                                                                    runat="server" ImageUrl="/images/icons/icon-delete1.png"></asp:ImageButton>
                                                            </div>
                                                            <div>
                                                                <span style="float: left; margin-left: 45px; margin-top: 15px">
                                                                    <asp:HyperLink ID="hlnkAttribute" runat="server" Text="[Add Attribute]" data-id='<%# DataBinder.Eval(Container.DataItem, "DocID") %>'
                                                                        CssClass="lnkBtnLogoAttr" />
                                                                </span>
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
                                        <script src="/js/jquery.sortableExtra.js"></script>
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

                                            //                                            $(function () {
                                            //                                                $("#sortable").sortable();
                                            //                                                $("#sortable").disableSelection();
                                            //                                            });
                                            $('#sortable').sortableExtra().bind('sortupdate', function (e, ui) {
                                                var idsInOrder = $('#sortable').sortable("toArray");
                                                var IDs = document.getElementById("<%= hdynImgIDs.ClientID %>");
                                                alert(idsInOrder.length);
                                                var ID1 = "";
                                                for (var i = 0; i < idsInOrder.length; i++) {
                                                    // alert(idsInOrder[i]);
                                                    ID1 = ID1 + idsInOrder[i] + ","
                                                }

                                                document.getElementById("<%= hdynImgIDs.ClientID %>").value = ID1;
                                            });
                                        </script>
                                    </asp:PlaceHolder>
                                    <asp:PlaceHolder ID="phBanner" runat="server" Visible="false">
                                        <ul>
                                            <li class="bannerList" style="width: 600px;" id='liBanner'>
                                                <div id="dvBannerContainer" class="bannerWrapper" style="width: 604px; height: 105px !important;"
                                                    runat="server">
                                                    <div class="binary-image" id="bimg1" runat="server">
                                                        <a id="bannerHref" runat="server" target="_blank">
                                                            <asp:Image ID="imgBanner" BorderStyle="None" runat="server" Width="600" Height="100" />
                                                        </a>
                                                    </div>
                                                    <div class="bnr-image-remove" id="bimgr2" runat="server">
                                                        <asp:ImageButton ID="imgbtnDel2" onmouseover="this.src='/images/icons/icon-delete.png'"
                                                            OnClick="imgbtnDel2_Click" onmouseout="this.src='/images/icons/icon-delete1.png'"
                                                            runat="server" ImageUrl="/images/icons/icon-delete1.png"></asp:ImageButton>
                                                        <asp:HiddenField ID="hBannerDocID" runat="server" />
                                                    </div>
                                                    
                                                </div>
                                                <div style="clear: both; text-align: center;margin-top: 5px;">
                                                            <asp:HyperLink ID="hlnkAttribute" runat="server" Text="[Add Attribute]" CssClass="lnkBtnLogoAttr" />
                                                    </div>
                                            </li>
                                        </ul>
                                    </asp:PlaceHolder>
                                    <style>
                                        .bannerWrapper
                                        {
                                            text-align: center;
                                            padding: 10px;
                                            background: #fff;
                                        }
                                    </style>
                                    <script>
                                        $(document).ready(function () {

                                            $("li.bannerList").hover(function () {
                                                $(this).find('.bnr-image-remove').show();
                                            },
										    function () {
										        $(this).find('.bnr-image-remove').hide();
										    });
                                        });
                                    </script>
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
        <!-- Select webinar theme-->
        <tr>
            <td>
                <div class="FormCont Steps" style="width: 700px; margin-bottom: 10px">
                    <table height="100">
                        <tr>
                            <td align="left" style="padding-bottom: 10px;">
                                <asp:Label ID="lblpgCap3" runat="server" Font-Bold="True" Text="Select a Webinar Skin"
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
                                        <a class="prev" id="foo2_prev" href="#"><span>prev</span></a> <a class="next" id="foo2_next"
                                            href="#"><span>next</span></a> </div>
                                    </FooterTemplate>
                                </asp:Repeater>
                                <asp:HiddenField ID="hSelThemeID" runat="server" Value="2" />
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
        <!-- Registration form & fields -->
        <tr>
            <td>
                <div class="FormCont Steps" style="width: 700px; margin-bottom: 10px;">
                    <table width="100%">
                        <tr>
                            <td colspan="3" align="left" style="padding-bottom: 10px;">
                                <asp:Label ID="lblFormFields" runat="server" Font-Bold="True" Text="Registration Fields"
                                    CssClass="frmHeading"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td width="49%" valign="top">
                                <table width="100%">
                                    <tr>
                                        <td align="center" height="22" width="20%">
                                            <strong>Include</strong>
                                        </td>
                                        <td>
                                            <b>Field Name</b>
                                        </td>
                                        <td width="12%">
                                            <b>Required?</b>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            <asp:CheckBox ID="chkInc1" runat="server" />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtFld1" runat="server" CssClass="textbox_EB textbox_EB4" Text="First Name"></asp:TextBox>
                                        </td>
                                        <td class="regFormRFieldCol">
                                            <asp:CheckBox ID="chkReq1" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            <asp:CheckBox ID="chkInc2" runat="server" />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtFld2" runat="server" CssClass="textbox_EB textbox_EB4" Text="Last Name"></asp:TextBox>
                                        </td>
                                        <td class="regFormRFieldCol">
                                            <asp:CheckBox ID="chkReq2" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            <asp:CheckBox ID="chkInc3" runat="server" />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtFld3" runat="server" CssClass="textbox_EB textbox_EB4" Text="Email Address"></asp:TextBox>
                                        </td>
                                        <td class="regFormRFieldCol">
                                            <asp:CheckBox ID="chkReq3" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            <asp:CheckBox ID="chkInc4" runat="server" />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtFld4" runat="server" CssClass="textbox_EB textbox_EB4" Text="Address"></asp:TextBox>
                                        </td>
                                        <td class="regFormRFieldCol">
                                            <asp:CheckBox ID="chkReq4" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            <asp:CheckBox ID="chkInc5" runat="server" />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtFld5" runat="server" CssClass="textbox_EB textbox_EB4" Text="City"></asp:TextBox>
                                        </td>
                                        <td class="regFormRFieldCol">
                                            <asp:CheckBox ID="chkReq5" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            <asp:CheckBox ID="chkInc6" runat="server" />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtFld6" runat="server" CssClass="textbox_EB textbox_EB4" Text="State/Province"></asp:TextBox>
                                        </td>
                                        <td class="regFormRFieldCol">
                                            <asp:CheckBox ID="chkReq6" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            <asp:CheckBox ID="chkInc7" runat="server" />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtFld7" runat="server" CssClass="textbox_EB textbox_EB4" Text="Zip code"></asp:TextBox>
                                        </td>
                                        <td class="regFormRFieldCol">
                                            <asp:CheckBox ID="chkReq7" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            <asp:CheckBox ID="chkInc8" runat="server" />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtFld8" runat="server" CssClass="textbox_EB textbox_EB4" Text="Country"></asp:TextBox>
                                        </td>
                                        <td class="regFormRFieldCol">
                                            <asp:CheckBox ID="chkReq8" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            <asp:CheckBox ID="chkInc9" runat="server" />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtFld9" runat="server" CssClass="textbox_EB textbox_EB4" Text="Phone"></asp:TextBox>
                                        </td>
                                        <td class="regFormRFieldCol">
                                            <asp:CheckBox ID="chkReq9" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td width="49%" valign="top">
                                <table width="100%">
                                    <tr>
                                        <td align="center" height="22" width="20%">
                                            <strong>Include</strong>
                                        </td>
                                        <td>
                                            <b>Field Name</b>
                                        </td>
                                        <td width="12%">
                                            <b>Required?</b>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            <asp:CheckBox ID="chkInc10" runat="server" />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtFld10" runat="server" CssClass="textbox_EB textbox_EB4" Text="Industry"></asp:TextBox>
                                        </td>
                                        <td class="regFormRFieldCol">
                                            <asp:CheckBox ID="chkReq10" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            <asp:CheckBox ID="chkInc11" runat="server" />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtFld11" runat="server" CssClass="textbox_EB textbox_EB4" Text="Organization"></asp:TextBox>
                                        </td>
                                        <td class="regFormRFieldCol">
                                            <asp:CheckBox ID="chkReq11" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            <asp:CheckBox ID="chkInc12" runat="server" />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtFld12" runat="server" CssClass="textbox_EB textbox_EB4" Text="Job Title"></asp:TextBox>
                                        </td>
                                        <td class="regFormRFieldCol">
                                            <asp:CheckBox ID="chkReq12" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            <asp:CheckBox ID="chkInc13" runat="server" />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtFld13" runat="server" CssClass="textbox_EB textbox_EB4" Text="No. of Employees"></asp:TextBox>
                                        </td>
                                        <td class="regFormRFieldCol">
                                            <asp:CheckBox ID="chkReq13" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            <asp:CheckBox ID="chkInc14" runat="server" />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtFld14" runat="server" CssClass="textbox_EB textbox_EB4" Text="Purchasing Time frame"></asp:TextBox>
                                        </td>
                                        <td class="regFormRFieldCol">
                                            <asp:CheckBox ID="chkReq14" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            <asp:CheckBox ID="chkInc15" runat="server" />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtFld15" runat="server" CssClass="textbox_EB textbox_EB4" Text="Role in Purchase Process"></asp:TextBox>
                                        </td>
                                        <td class="regFormRFieldCol">
                                            <asp:CheckBox ID="chkReq15" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            <asp:CheckBox ID="chkInc16" runat="server" />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtFld16" runat="server" CssClass="textbox_EB textbox_EB4" Text=""></asp:TextBox>
                                        </td>
                                        <td class="regFormRFieldCol">
                                            <asp:CheckBox ID="chkReq16" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            <asp:CheckBox ID="chkInc17" runat="server" />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtFld17" runat="server" CssClass="textbox_EB textbox_EB4" Text=""></asp:TextBox>
                                        </td>
                                        <td class="regFormRFieldCol">
                                            <asp:CheckBox ID="chkReq17" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            <asp:CheckBox ID="chkInc18" runat="server" />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtFld18" runat="server" CssClass="textbox_EB textbox_EB4" Text=""></asp:TextBox>
                                        </td>
                                        <td class="regFormRFieldCol">
                                            <asp:CheckBox ID="chkReq18" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <table width="95%">
                        <tr>
                            <td style="height: 25px; padding-left: 15px;">
                                <span>
                                    <asp:Label ID="lblCustFieldCap" runat="server" Font-Bold="true" Text="Additional Questions"></asp:Label></span>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div id="dvAddQBtn" runat="server" style="padding-left: 15px">
                                    <asp:Button ID="btnNewQuestion" runat="server" Text="Add Question" CssClass="SubBtn"
                                        OnClick="btnNewQuestion_Click" />
                                </div>
                                <div id="dvAddQA" class="frmQA" runat="server">
                                    <table width="100%">
                                        <tr>
                                            <td style="padding: 1px 3px 5px 10px">
                                                <telerik:RadComboBox ID="rcmbType" runat="server" Width="215" OnClientSelectedIndexChanged="OnTypeChange"
                                                    Text="Additional Question Type" CausesValidation="False" MarkFirstMatch="True"
                                                    AllowCustomText="false" HighlightTemplatedItems="True" ExpandAnimation-Duration="1000"
                                                    CollapseAnimation-Duration="1000" CollapseAnimation-Type="OutQuart" NoWrap="True"
                                                    EnableTheming="True" Font-Italic="False" Skin="Default">
                                                    <Items>
                                                        <telerik:RadComboBoxItem runat="server" Text="Comment" Value="COMM" Selected="True"
                                                            CssClass="ddStyle1" />
                                                        <telerik:RadComboBoxItem runat="server" Text="Multiple Choice - Single Answer" Value="MCSA"
                                                            CssClass="ddStyle1" />
                                                        <telerik:RadComboBoxItem runat="server" Text="Multiple Choice - Multiple Answer"
                                                            Value="MCMA" CssClass="ddStyle1" />
                                                    </Items>
                                                </telerik:RadComboBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <div style="float: left; padding: 5px 15px 5px 12px; vertical-align: middle">
                                                    Question</div>
                                                <div style="float: left">
                                                    <asp:TextBox ID="txtQuestion" runat="server" CssClass="textbox_EB textbox_EB_RegQA"></asp:TextBox>
                                                    &nbsp;
                                                    <asp:HiddenField ID="hqaID" runat="server" Value="0" />
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <div id="dvQADetails" runat="server">
                                                    <table width="100%">
                                                        <tr>
                                                            <td>
                                                                <div style="padding-left: 5px">
                                                                    <!-- q input -->
                                                                    <div class="Clr">
                                                                    </div>
                                                                    <div id="dvDD" style="padding-left: 54px">
                                                                        <table id="tbResp" runat="server">
                                                                            <tr>
                                                                                <td>
                                                                                    <span id="spQL1" runat="server" class="QAResLabel">Response<!--&nbsp;&nbsp;<img src="/images/icons/icon-chk.png" style="padding-top: 2px" />--></span>
                                                                                    <span id="spQC1" runat="server" class="QAResRadio"></span><span id="spQR1" runat="server"
                                                                                        class="QAResCheck"></span>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtRespone1" runat="server" CssClass="textbox_EB textbox_EB_RegResp"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <span id="spQL2" runat="server" class="QAResLabel">Response<!--&nbsp;&nbsp;<img src="/images/icons/icon-chk.png" />--></span>
                                                                                    <span id="spQC2" runat="server" class="QAResRadio"></span><span id="spQR2" runat="server"
                                                                                        class="QAResCheck"></span>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtRespone2" runat="server" CssClass="textbox_EB textbox_EB_RegResp"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <span id="spQL3" runat="server" class="QAResLabel">Response<!--&nbsp;&nbsp;<img src="/images/icons/icon-chk.png" />--></span>
                                                                                    <span id="spQC3" runat="server" class="QAResRadio"></span><span id="spQR3" runat="server"
                                                                                        class="QAResCheck"></span>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtRespone3" runat="server" CssClass="textbox_EB textbox_EB_RegResp"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <span id="spQL4" runat="server" class="QAResLabel">Response<!--&nbsp;&nbsp;<img src="/images/icons/icon-chk.png" />--></span>
                                                                                    <span id="spQC4" runat="server" class="QAResRadio"></span><span id="spQR4" runat="server"
                                                                                        class="QAResCheck"></span>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtRespone4" runat="server" CssClass="textbox_EB textbox_EB_RegResp"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <span id="spQL5" runat="server" class="QAResLabel">Response<!--&nbsp;&nbsp;<img src="/images/icons/icon-chk.png" />--></span>
                                                                                    <span id="spQC5" runat="server" class="QAResRadio"></span><span id="spQR5" runat="server"
                                                                                        class="QAResCheck"></span>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtRespone5" runat="server" CssClass="textbox_EB  textbox_EB_RegResp"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </div>
                                                                    <div class="Clr">
                                                                    </div>
                                                                </div>
                                                                <asp:HiddenField ID="hQOrder" runat="server" Value="1" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <div style="padding-left: 12px; padding-bottom: 10px;">
                                                    <asp:Button ID="btnAddQuestion" CssClass="SubBtn" runat="server" Text="Add to form"
                                                        OnClientClick="javascript:validateAdditionalQst();" OnClick="btnAddQuestion_click" />
                                                    &nbsp;<asp:Button ID="btnEditCancel" CssClass="SubBtn" runat="server" Text="Cancel"
                                                        OnClick="btnEditCancel_click" />
                                                    &nbsp; &nbsp;
                                                    <asp:Label ID="errLbl" runat="server" ForeColor="Red" Font-Size="12px"></asp:Label>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div id="dvQAList" style="padding-left: 15px; padding-bottom: 5px; padding-top: 10px;"
                                    runat="server">
                                    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server">
                                    </telerik:RadAjaxLoadingPanel>
                                    <telerik:RadAjaxManager runat="server" ID="radAjax" DefaultLoadingPanelID="RadAjaxLoadingPanel1">
                                        <AjaxSettings>
                                        </AjaxSettings>
                                    </telerik:RadAjaxManager>
                                    <telerik:RadGrid ID="tgrdQAList" runat="server" GridLines="None" MasterTableView-EditFormSettings-EditFormType="Template"
                                        AutoGenerateColumns="False" ValidationSettings-EnableValidation="false" OnItemCommand="tgrdQAList_ItemCommand"
                                        OnItemDataBound="tgrdQAList_ItemDataBound" OnRowDrop="tgrdQAList_RowDrop" Visible="false"
                                        BorderColor="#D7D7D7">
                                        <ExportSettings HideStructureColumns="true" />
                                        <MasterTableView AutoGenerateColumns="false" AllowFilteringByColumn="False" ShowHeader="true"
                                            CommandItemDisplay="None" ShowFooter="false" TableLayout="Auto" Width="100%"
                                            DataKeyNames="qaID">
                                            <CommandItemSettings ShowAddNewRecordButton="false" ShowExportToWordButton="false"
                                                ShowRefreshButton="false" ShowExportToExcelButton="false" ShowExportToCsvButton="false"
                                                ShowExportToPdfButton="false" />
                                            <NoRecordsTemplate>
                                                <center>
                                        <br />
                                        No questions to display<br />
                                        &nbsp;</center>
                                            </NoRecordsTemplate>
                                            <RowIndicatorColumn>
                                                <HeaderStyle Width="20px" />
                                            </RowIndicatorColumn>
                                            <ExpandCollapseColumn>
                                                <HeaderStyle Width="20px" />
                                            </ExpandCollapseColumn>
                                            <Columns>
                                                <telerik:GridBoundColumn DataField="QuestionOrder" HeaderText="Order">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridTemplateColumn DataField="RegFormQuestion" HeaderText="Question">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRegFormQuestion" Text='<%# Eval("RegFormQuestion") %>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn DataField="QResponseType" HeaderText="Response Type">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblQResponseType" Text='<%# Eval("QResponseType") %>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn UniqueName="DeleteColumn">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="btnEdit" runat="server" CommandArgument='<%# Eval("qaID") %>'
                                                            CommandName="editQA" ImageUrl="~/Images/icons/Edit_16.gif" ToolTip="Update" />
                                                        <asp:ImageButton ID="btnDelete" runat="server" CommandArgument='<%# Eval("qaID") %>'
                                                            OnClientClick="return confirm('Are you sure, you want to delete this question?');"
                                                            CommandName="delQA" ImageUrl="~/Images/icons/trash.png" ToolTip="Delete" />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </telerik:GridTemplateColumn>
                                            </Columns>
                                        </MasterTableView>
                                        <ClientSettings AllowRowsDragDrop="True" AllowColumnsReorder="true" ReorderColumnsOnClient="true">
                                            <Selecting AllowRowSelect="True" EnableDragToSelectRows="false"></Selecting>
                                            <ClientEvents OnRowMouseOver="RowMouseOver" OnRowMouseOut="RowMouseOut" />
                                        </ClientSettings>
                                    </telerik:RadGrid>
                                </div>
                            </td>
                        </tr>
                    </table>
                    <telerik:RadCodeBlock ID="rcbQst" runat="server">
                        <script type="text/javascript">
                        //<![CDATA[

                            function setTypeDisplay(dd) {
                                var elLabel;
                                SetResponseLabelToggle("none");
                                SetResponseCheckToggle("none");
                                SetResponseRadioToggle("none");

                                switch (dd) {
                                    case "L":
                                        SetResponseLabelToggle("block");
                                        break;
                                    case "C":
                                        SetResponseLabelToggle("block");
                                        SetResponseCheckToggle("block");
                                        break;
                                    case "R":
                                        SetResponseRadioToggle("block");
                                        break;
                                }
                            }

                            function SetResponseLabelToggle(dispVal) {
                                var elLabel;
                                elLabel = document.getElementById('<%=spQL1.ClientID %>');
                                if (elLabel != null)
                                    elLabel.style.display = "block";
                                elLabel = document.getElementById('<%=spQL2.ClientID %>');
                                if (elLabel != null)
                                    elLabel.style.display = "block";
                                elLabel = document.getElementById('<%=spQL3.ClientID %>');
                                if (elLabel != null)
                                    elLabel.style.display = "block";
                                elLabel = document.getElementById('<%=spQL4.ClientID %>');
                                if (elLabel != null)
                                    elLabel.style.display = "block";
                                elLabel = document.getElementById('<%=spQL5.ClientID %>');
                                if (elLabel != null)
                                    elLabel.style.display = "block";
                            }

                            function SetResponseCheckToggle(dispVal) {
                                var elLabel;
                                elLabel = document.getElementById('<%=spQC1.ClientID %>');
                                if (elLabel != null)
                                    elLabel.style.display = dispVal;
                                elLabel = document.getElementById('<%=spQC2.ClientID %>');
                                if (elLabel != null)
                                    elLabel.style.display = dispVal;
                                elLabel = document.getElementById('<%=spQC3.ClientID %>');
                                if (elLabel != null)
                                    elLabel.style.display = dispVal;
                                elLabel = document.getElementById('<%=spQC4.ClientID %>');
                                if (elLabel != null)
                                    elLabel.style.display = dispVal;
                                elLabel = document.getElementById('<%=spQC5.ClientID %>');
                                if (elLabel != null)
                                    elLabel.style.display = dispVal;
                            }

                            function SetResponseRadioToggle(dispVal) {
                                var elLabel;
                                elLabel = document.getElementById('<%=spQR1.ClientID %>');
                                if (elLabel != null)
                                    elLabel.style.display = dispVal;
                                elLabel = document.getElementById('<%=spQR2.ClientID %>');
                                if (elLabel != null)
                                    elLabel.style.display = dispVal;
                                elLabel = document.getElementById('<%=spQR3.ClientID %>');
                                if (elLabel != null)
                                    elLabel.style.display = dispVal;
                                elLabel = document.getElementById('<%=spQR4.ClientID %>');
                                if (elLabel != null)
                                    elLabel.style.display = dispVal;
                                elLabel = document.getElementById('<%=spQR5.ClientID %>');
                                if (elLabel != null)
                                    elLabel.style.display = dispVal;
                            }

                            function OnTypeChange(sender, eventArgs) {

                                var ele = document.getElementById("<%=dvQADetails.ClientID %>");
                                ele.style.display = "block";

                                var eleResponse = document.getElementById("dvDD");
                                var item = eventArgs.get_item();
                                //sender.set_text("You selected " + item.get_text());
                                switch (item.get_value()) {
                                    case "COMM":
                                        eleResponse.style.display = "none";
                                        break;
                                    case "DD":
                                        setTypeDisplay('L');
                                        eleResponse.style.display = "block";
                                        break;
                                    case "MCSA":
                                        //setTypeDisplay('R');
                                        setTypeDisplay('C');
                                        eleResponse.style.display = "block";
                                        break;
                                    case "MCMA":
                                        setTypeDisplay('C');
                                        eleResponse.style.display = "block";
                                        break;
                                }
                            }

                            function validateAdditionalQst() {
                                var txtQst = document.getElementById('<%=txtQuestion.ClientID %>');
                                return false;
                            }

                            //]]>
                        </script>
                    </telerik:RadCodeBlock>
                    <br />
                    <!-- Registration pagelet include & exclude -->
                    <table width="100%">
                        <tr>
                            <td style="padding: 5px 5px 5px 15px;" colspan="2">
                                <asp:CheckBox ID="chkIncSummary" runat="server" CssClass="chkGen chkAlign2" Text="Include Webinar Summary" />
                            </td>
                        </tr>
                        <tr>
                            <td style="padding: 5px 5px 5px 15px;" colspan="2">
                                <asp:CheckBox ID="chkIncSpeakerBio" runat="server" CssClass="chkGen chkAlign2" Text="Include Webinar Presenter-Bios" />
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td style="padding: 5px 5px 5px 15px;">
                                <asp:HyperLink ID="lbtnPreviewRegForm1" data-typ="REG" CssClass="lnkBtn ht1" runat="server"
                                    Text="Preview Registration"></asp:HyperLink>&nbsp;&nbsp;|&nbsp;
                                <asp:HyperLink ID="lbtnPreviewWR1" data-typ="WR" CssClass="lnkBtn ht1" runat="server"
                                    Text="Preview Waiting Room"></asp:HyperLink>
                            </td>
                            <td style="text-align: right;">
                                <!-- begin:webinar disable -->
                                <span class="spDisable">
                                    <asp:CheckBox ID="chkEnableReg" runat="server" CssClass="chkGen3 chkAlign3" Text="" />
                                    &nbsp;<asp:Label ID="lblEnableReg" runat="server" Text="Disable Webinar Registration"></asp:Label>
                                    <asp:HiddenField ID="hWebinarID" runat="server" Value="0" />
                                    <asp:HiddenField ID="hInitStatus" runat="server" />
                                </span>
                                <!-- end:webinar disable -->
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
        <!-- Webinar Invitation -->
        <tr>
            <td>
                <table width="100%" align="center">
                    <tr>
                        <td>
                            <div class="Steps" style="width: 700px; margin-bottom: 10px;">
                                <table>
                                    <tr>
                                        <td align="left" style="padding-bottom: 10px">
                                            <asp:Label ID="lblWebInvite" runat="server" Font-Bold="True" Text="Webinar Invitation" />
                                            <span id="spPre4" runat="server" visible="false"><span id="spP4" class="spWI">[p]</span></span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td height="24" style="padding-left: 5px">
                                            <asp:Label ID="lbl1" runat="server" Text="We create and email it to you. You forward it to people you want to invite."></asp:Label>
                                            <br />
                                        </td>
                                    </tr>
                                    <asp:PlaceHolder ID="phPreEnable" Visible="true" runat="server">
                                        <tr>
                                            <td height="24" style="padding-left: 5px">
                                                <asp:HyperLink ID="lbtnPreviewInvite1" CssClass="lnkBtn2" data-typ="INP" runat="server"
                                                    Text="Preview"></asp:HyperLink>
                                                <asp:Literal ID="ltrSep1" runat="server" Text="&nbsp;|&nbsp;"></asp:Literal>
                                                <asp:HyperLink ID="lbtnEditInvite1" CssClass="lnkBtn1" data-typ="INE" runat="server"
                                                    Text="Edit"></asp:HyperLink>
                                                <asp:Literal ID="ltrSep2" runat="server" Text="&nbsp;|&nbsp;"></asp:Literal>
                                                <asp:LinkButton ID="lbtnEmailInvite1" CssClass="lnkBtn1a" data-typ="INI" runat="server"
                                                    Text="Email me the invitation" OnClick="lbtnEmailInvite1_Click" CausesValidation="false" />
                                                <asp:Literal ID="ltrSep3" runat="server" Text="&nbsp;|&nbsp;"></asp:Literal>
                                                <asp:HyperLink ID="lbtnSendReviewInvite1" CssClass="lnkBtn3" data-typ="INR" runat="server"
                                                    Text="Send for Review"></asp:HyperLink>
                                                <asp:HiddenField ID="hInviteContentID" runat="server" />
                                                &nbsp;&nbsp;&nbsp;&nbsp;
                                                <asp:Label ID="lblMsg" ForeColor="Green" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                    </asp:PlaceHolder>
                                    <asp:PlaceHolder ID="phPreDisable" Visible="false" runat="server">
                                        <tr>
                                            <td height="24" style="padding-left: 5px">
                                                <asp:Label ID="lblPreview" ForeColor="#666666" runat="server" Text="Preview" />
                                                <asp:Literal ID="Literal1" runat="server" Text="&nbsp;|&nbsp;"></asp:Literal>
                                                <asp:Label ID="lblEdit" ForeColor="#666666" runat="server" Text="Edit" />
                                                <asp:Literal ID="Literal2" runat="server" Text="&nbsp;|&nbsp;"></asp:Literal>
                                                <asp:Label ID="lblInvite" ForeColor="#666666" runat="server" Text="Email me the invitation" />
                                                <asp:Literal ID="Literal3" runat="server" Text="&nbsp;|&nbsp;"></asp:Literal>
                                                <asp:Label ID="lblReview" ForeColor="#666666" runat="server" Text="Send for Review" />
                                            </td>
                                        </tr>
                                    </asp:PlaceHolder>
                                </table>
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="margin-left: 5px">
                <asp:Accordion ID="Accordion1" CssClass="accordion" HeaderCssClass="accordionHeader"
                    HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent"
                    runat="server" RequireOpenedPane="False" TransitionDuration="500" SelectedIndex="-1"
                    FadeTransitions="False">
                    <Panes>
                        <asp:AccordionPane ID="AccordionPane1" runat="server">
                            <Header>
                                Advanced Registration Features</Header>
                            <Content>
                                <div style="overflow: hidden">
                                    <div class="FormCont Steps" style="width: 700px;">
                                        <table width="100%">
                                            <tr>
                                                <td style="padding: 5px 5px 5px 15px; vertical-align: middle">
                                                    <asp:Label ID="lblCapAPI" Text="Connected Your Registration" runat="server" Font-Bold="true"></asp:Label>
                                                    <span id="spPre1" runat="server" visible="false"><span id="spP1" class="spAR">[p]</span></span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="padding: 5px 5px 5px 20px; vertical-align: middle">
                                                    <asp:CheckBox ID="chkAPI" runat="server" CssClass="chkGen1 chkAlign2" Font-Bold="false"
                                                        Text="Email Connected Your Registration API instructions to" />
                                                    <asp:TextBox ID="txtAPIReg" runat="server" CssClass="textbox_EB" Width="200"></asp:TextBox>
                                                    <asp:HiddenField ID="hAPIReg" runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="padding: 5px 5px 5px 15px; vertical-align: middle">
                                                    <asp:Label ID="lblCapCamp" Text="Webinar Campaign Tracking" runat="server" CssClass="frmHeading"
                                                        Font-Bold="true"></asp:Label>
                                                    <span id="spPre2" runat="server" visible="false"><span id="spP2" class="spAR">[p]</span></span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="padding: 5px 5px 5px 20px; vertical-align: middle">
                                                    <asp:CheckBox ID="chkCamp" runat="server" CssClass="chkGen1 chkAlign2" Font-Bold="false"
                                                        Text="Email Webinar Campaign Tracking instructions to" />
                                                    <asp:TextBox ID="txtEmailCampaign" runat="server" CssClass="textbox_EB" Width="200"></asp:TextBox>
                                                    <asp:HiddenField ID="hEmailCampaign" runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="padding: 5px 5px 5px 15px; vertical-align: middle">
                                                    <asp:Label ID="lblCapDomain" Text="Set up Domain Blocking" CssClass="frmHeading"
                                                        runat="server" Font-Bold="true"></asp:Label>
                                                    <span id="spPre3" runat="server" visible="false"><span id="spP3" class="spAR">[p]</span></span>
                                                    <!--<input id="domainBlockBtn1" class="domBlkCollapse" type="button" />-->
                                                    <span id="spAdd" runat="server"><a href="#" id="domainBlockBtn" class="lnkBtn0">[Add]</a></span>
                                                    <asp:HiddenField ID="hDomainNonStdShow" runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="padding: 5px 5px 5px 10px; vertical-align: middle">
                                                    <div style="clear: both; height: 30px;">
                                                        <div class="FLeft rowgapTxt">
                                                            <asp:CheckBox ID="chkHotmail" runat="server" Checked="True" Height="20" />&nbsp;
                                                            <asp:Label ID="lblHotmail" runat="server" Text="hotmail.com"></asp:Label>
                                                        </div>
                                                        <div class="FLeft rowgapTxt">
                                                            <asp:CheckBox ID="chkYahoo" runat="server" Checked="True" Height="20" />&nbsp;
                                                            <asp:Label ID="lblYahoo" runat="server" Text="yahoo.com"></asp:Label>
                                                        </div>
                                                        <div class="FLeft rowgapTxt">
                                                            <asp:CheckBox ID="chkGmail" runat="server" Checked="True" Height="20" />&nbsp;
                                                            <asp:Label ID="lblGmail" runat="server" Text="gmail.com"></asp:Label>
                                                        </div>
                                                        <div class="FLeft rowgapTxt">
                                                            <asp:CheckBox ID="chkAol" runat="server" Checked="True" Height="20" />&nbsp;
                                                            <asp:Label ID="lblAol" runat="server" Text="aol.com"></asp:Label>
                                                        </div>
                                                        <div class="FLeft rowgapTxt">
                                                            <asp:CheckBox ID="chksbc" runat="server" Checked="True" Height="20" />&nbsp;
                                                            <asp:Label ID="lblsbc" runat="server" Text="sbcglobal.com"></asp:Label>
                                                        </div>
                                                    </div>
                                                    <div id="domainBlockBox">
                                                        <div id="row1" style="clear: both; height: 30px;" runat="server">
                                                            <div class="FLeft rowgap">
                                                                <asp:CheckBox ID="chkr1c1" runat="server" Checked="False" />&nbsp;
                                                                <asp:TextBox ID="txtr1c1" runat="server" Height="20" Width="80" MaxLength="120" CssClass="textbox_EB"></asp:TextBox>
                                                            </div>
                                                            <div class="FLeft rowgap">
                                                                <asp:CheckBox ID="chkr1c2" runat="server" Checked="False" />&nbsp;
                                                                <asp:TextBox ID="txtr1c2" runat="server" Height="20" Width="80" MaxLength="120" CssClass="textbox_EB"></asp:TextBox>
                                                            </div>
                                                            <div class="FLeft rowgap">
                                                                <asp:CheckBox ID="chkr1c3" runat="server" Checked="False" />&nbsp;
                                                                <asp:TextBox ID="txtr1c3" runat="server" Height="20" Width="80" MaxLength="120" CssClass="textbox_EB"></asp:TextBox>
                                                            </div>
                                                            <div class="FLeft rowgap">
                                                                <asp:CheckBox ID="chkr1c4" runat="server" Checked="False" />&nbsp;
                                                                <asp:TextBox ID="txtr1c4" runat="server" Height="20" Width="80" MaxLength="120" CssClass="textbox_EB"></asp:TextBox>
                                                            </div>
                                                            <div class="FLeft rowgap">
                                                                <asp:CheckBox ID="chkr1c5" runat="server" Checked="False" />&nbsp;
                                                                <asp:TextBox ID="txtr1c5" runat="server" Height="20" Width="80" MaxLength="120" CssClass="textbox_EB"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div id="row2" style="clear: both; height: 30px;" runat="server">
                                                            <div class="FLeft rowgap">
                                                                <asp:CheckBox ID="chkr2c1" runat="server" Checked="False" />&nbsp;
                                                                <asp:TextBox ID="txtr2c1" runat="server" Height="20" Width="80" MaxLength="120" CssClass="textbox_EB"></asp:TextBox>
                                                            </div>
                                                            <div class="FLeft rowgap">
                                                                <asp:CheckBox ID="chkr2c2" runat="server" Checked="False" />&nbsp;
                                                                <asp:TextBox ID="txtr2c2" runat="server" Height="20" Width="80" MaxLength="120" CssClass="textbox_EB"></asp:TextBox>
                                                            </div>
                                                            <div class="FLeft rowgap">
                                                                <asp:CheckBox ID="chkr2c3" runat="server" Checked="False" />&nbsp;
                                                                <asp:TextBox ID="txtr2c3" runat="server" Height="20" Width="80" MaxLength="120" CssClass="textbox_EB"></asp:TextBox>
                                                            </div>
                                                            <div class="FLeft rowgap">
                                                                <asp:CheckBox ID="chkr2c4" runat="server" Checked="False" />&nbsp;
                                                                <asp:TextBox ID="txtr2c4" runat="server" Height="20" Width="80" MaxLength="120" CssClass="textbox_EB"></asp:TextBox>
                                                            </div>
                                                            <div class="FLeft rowgap">
                                                                <asp:CheckBox ID="chkr2c5" runat="server" Checked="False" />&nbsp;
                                                                <asp:TextBox ID="txtr2c5" runat="server" Height="20" Width="80" MaxLength="120" CssClass="textbox_EB"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div id="row3" style="clear: both; height: 30px;" runat="server">
                                                            <div class="FLeft rowgap">
                                                                <asp:CheckBox ID="chkr3c1" runat="server" Checked="False" />&nbsp;
                                                                <asp:TextBox ID="txtr3c1" runat="server" Height="20" Width="80" MaxLength="120" CssClass="textbox_EB"></asp:TextBox>
                                                            </div>
                                                            <div class="FLeft rowgap">
                                                                <asp:CheckBox ID="chkr3c2" runat="server" Checked="False" />&nbsp;
                                                                <asp:TextBox ID="txtr3c2" runat="server" Height="20" Width="80" MaxLength="120" CssClass="textbox_EB"></asp:TextBox>
                                                            </div>
                                                            <div class="FLeft rowgap">
                                                                <asp:CheckBox ID="chkr3c3" runat="server" Checked="False" />&nbsp;
                                                                <asp:TextBox ID="txtr3c3" runat="server" Height="20" Width="80" MaxLength="120" CssClass="textbox_EB"></asp:TextBox>
                                                            </div>
                                                            <div class="FLeft rowgap">
                                                                <asp:CheckBox ID="chkr3c4" runat="server" Checked="False" />&nbsp;
                                                                <asp:TextBox ID="txtr3c4" runat="server" Height="20" Width="80" MaxLength="120" CssClass="textbox_EB"></asp:TextBox>
                                                            </div>
                                                            <div class="FLeft rowgap">
                                                                <asp:CheckBox ID="chkr3c5" runat="server" Checked="False" />&nbsp;
                                                                <asp:TextBox ID="txtr3c5" runat="server" Height="20" Width="80" MaxLength="120" CssClass="textbox_EB"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <asp:HiddenField ID="rowCount" runat="server" Value="0" />
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </Content>
                        </asp:AccordionPane>
                    </Panes>
                </asp:Accordion>
            </td>
        </tr>
    </table>
    <br />
    <br />
    <img src="../Images/blank.gif" height="10px" alt="" />
    <asp:HiddenField ID="hRegGUID" runat="server" />
    <asp:HiddenField ID="hAudGUID" runat="server" />
    <asp:HiddenField ID="hCropDocID" Value="126" runat="server" />
    <script>
        var i = eval(document.getElementById('<%=rowCount.ClientID %>').value);

        if (i == 0) {
            $('#ContentPlaceHolder1_schMeeting1_webRegistration1_row1').hide();
            $('#ContentPlaceHolder1_schMeeting1_webRegistration1_row2').hide();
            $('#ContentPlaceHolder1_schMeeting1_webRegistration1_row3').hide();
        }
        if (i == 1) {
            $('#ContentPlaceHolder1_schMeeting1_webRegistration1_row1').show();
            $('#ContentPlaceHolder1_schMeeting1_webRegistration1_row2').hide();
            $('#ContentPlaceHolder1_schMeeting1_webRegistration1_row3').hide();
        }
        if (i == 2) {
            $('#ContentPlaceHolder1_schMeeting1_webRegistration1_row1').show();
            $('#ContentPlaceHolder1_schMeeting1_webRegistration1_row2').show();
            $('#ContentPlaceHolder1_schMeeting1_webRegistration1_row3').hide();
        }
        if (i == 3) {
            $('#ContentPlaceHolder1_schMeeting1_webRegistration1_row1').show();
            $('#ContentPlaceHolder1_schMeeting1_webRegistration1_row2').show();
            $('#ContentPlaceHolder1_schMeeting1_webRegistration1_row3').show();
        }
        $("#domainBlockBtn").click(function () {
            i = i + 1;

            if (i == 1) {
                $('#ContentPlaceHolder1_schMeeting1_webRegistration1_row1').show();
            }
            if (i == 2) {
                $('#ContentPlaceHolder1_schMeeting1_webRegistration1_row2').show();
            }
            if (i == 3) {
                $('#ContentPlaceHolder1_schMeeting1_webRegistration1_row3').show();
                $('#domainBlockBtn').hide();
            }
        });
        
    </script>
    <script src="/qtip2/qtipCommonfn.js" type="text/javascript" charset="utf-8"></script>
    <script type="text/javascript">

        $(document).ready(function () {
            // logo attribute pop-out
            $(".lnkBtnLogoAttr").click(function () {
                var hWebID = document.getElementById('<%=hWebinarID.ClientID %>');
                var docID = $(this).attr('data-id');
                var URL = "/Pages/popup/addAttribute.aspx?docID=" + docID + "&ID=" + hWebID.value;
                qtipPopup2('.lnkBtnLogoAttr',
                   "<div id='modalContainer'><div class='modalClose'><img src='/images/x.png' /></div><iframe id='audFrame' src=" + URL + " height='130' width='680' scrolling='no' class='bgfill'></iframe></div>",
                               680, '.modalClose', '/Pages/Schedule', '#audFrame', 'body #ContentPlaceHolder1_hModalStatusFlg');
            });
            // Logo & banner pop-out
            $("#imgPop").click(function () {
                var hWebID = document.getElementById('<%=hWebinarID.ClientID %>');
                var URL = "/Pages/popup/imageCroping.aspx?ID=" + hWebID.value;
                qtipPopup2("#imgPop",
                    "<div id='modalContainer'><div class='modalClose'><img src='/images/x.png' /></div><iframe id='popImgFrame' src=" + URL + " height='400' width='720' scrolling='no' class='bgfill'></iframe></div>",
                    720, ".modalClose", "/Pages/Schedule", "#popImgFrame", "body #ContentPlaceHolder1_hModalStatusFlg");
            });

            // Enable or Disable registration
            $("#ContentPlaceHolder1_schMeeting1_webRegistration1_chkEnableReg").live('click', function (e) {
                var isChk = document.getElementById('<%=hInitStatus.ClientID %>').value;
                var hWebID = document.getElementById('<%=hWebinarID.ClientID %>');
                var URL = "/Pages/popup/ConfirmWebinarStatus.aspx?ID=" + hWebID.value + "&s=" + isChk;

                qtipPopup2("#ContentPlaceHolder1_schMeeting1_webRegistration1_chkEnableReg",
                    "<div id='modalContainer'><div class='modalClose'><img src='/images/x.png' /></div><iframe id='popFrame4' src=" + URL + " height='100' width='350' scrolling='no' class='bgfill'></iframe></div>",
                    350, ".modalClose", "/Pages/Schedule", "#popFrame4", "body #ContentPlaceHolder1_hModalStatusFlg");
            });

            // Advance registration feature
            $("#ContentPlaceHolder1_schMeeting1_webRegistration1_chkAPI").live('click', function (e) {
                if ($('#ContentPlaceHolder1_schMeeting1_webRegistration1_chkAPI').is(':checked')) {
                    var hAPIReg = document.getElementById('<%=hAPIReg.ClientID %>');
                    document.getElementById('<%=txtAPIReg.ClientID %>').value = hAPIReg.value;
                }
                else {
                    document.getElementById('<%=txtAPIReg.ClientID %>').value = "";
                }
            });

            $("#ContentPlaceHolder1_schMeeting1_webRegistration1_chkCamp").live('click', function (e) {
                if ($('#ContentPlaceHolder1_schMeeting1_webRegistration1_chkCamp').is(':checked')) {
                    var hEmailCamp = document.getElementById('<%=hEmailCampaign.ClientID %>');
                    document.getElementById('<%=txtEmailCampaign.ClientID %>').value = hEmailCamp.value;
                }
                else {
                    document.getElementById('<%=txtEmailCampaign.ClientID %>').value = "";
                }
            });
            //addAttribute
            var hWebID = document.getElementById('<%=hWebinarID.ClientID %>');
            $(".lnkBtn1b").live('click', function (e) {
                var URL = "/Pages/popup/addAttribute.aspx?flg=1&ID=" + hWebID.value;
                qtipPopup2(".lnkBtn1b",
                    "<div id='modalContainer'><div class='modalClose'><img src='/images/x.png' /></div><iframe id='audFrame' src=" + URL + " height='365' width='680' scrolling='no' class='bgfill'></iframe></div>",
                    680, '.modalClose', '/Pages/Schedule', '#audFrame', 'body #ContentPlaceHolder1_hModalStatusFlg');
            });
        });
    </script>
    <telerik:RadCodeBlock ID="jqWebinarStatusConfirm" runat="server">
        <%-- saveRegistrationInfo & waiting room preview --%>
        <script type="text/javascript">
            $(document).ready(function () {
                //$("#ContentPlaceHolder1_schMeeting1_webRegistration1_lbtnPreviewRegForm1").each(function (e) {
                $('#ContentPlaceHolder1_schMeeting1_webRegistration1_lbtnPreviewRegForm1').live('click', function (e) {
                    var hRegGUID = document.getElementById('<%=hRegGUID.ClientID %>').value;
                    var mySplitResult = hRegGUID.split("_");
                    var chkSum = document.getElementById('<%=chkIncSummary.ClientID %>');
                    var chkBio = document.getElementById('<%=chkIncSpeakerBio.ClientID %>');
                    var themeID = document.getElementById('<%=hSelThemeID.ClientID %>');
                    var flg = "";
                    if (chkSum.checked)
                        flg = '1';
                    else
                        flg = '0';
                    if (chkBio.checked)
                        flg = flg + '1';
                    else
                        flg = flg + '0';
                    flg = flg + "$" + themeID.value;
                    var URL = mySplitResult[0] + flg + "_" + mySplitResult[1];

                    qtipPopup2("#ContentPlaceHolder1_schMeeting1_webRegistration1_lbtnPreviewRegForm1",
                    "<div id='modalContainer'><div class='modalClose'><img src='/images/x.png' /></div><iframe id='regFormFrame' src=" + URL + " height='650' width='750' scrolling='no' class='bgfill'></iframe></div>",
                    750, ".modalClose", "", "#regFormFrame", "body #hModalStatusFlg");
                });

                $('#ContentPlaceHolder1_schMeeting1_webRegistration1_lbtnPreviewWR1').live('click', function (e) {
                    var hAudGUID = document.getElementById('<%=hAudGUID.ClientID %>').value;
                    var themeID = document.getElementById('<%=hSelThemeID.ClientID %>');
                    mySplitResult = hAudGUID.split("_");
                    flg = "";
                    flg = "$" + themeID.value;
                    var URL = mySplitResult[0] + flg + "_" + mySplitResult[1];

                    qtipPopup2("#ContentPlaceHolder1_schMeeting1_webRegistration1_lbtnPreviewWR1",
                    "<div id='modalContainer'><div class='modalClose'><img src='/images/x.png' /></div><iframe id='regWRFrame' src=" + URL + " height='650' width='750' scrolling='no' class='bgfill'></iframe></div>",
                    750, ".modalClose", "", "#regWRFrame", "body #hModalStatusFlg");
                });

                $('.lnkBtn1').live('click', function (e) {
                    var themeUrl = $(this).attr('data-typ');
                    var hWebID = document.getElementById('<%=hWebinarID.ClientID %>');
                    var URL = "/Pages/popup/Notification_EmailTpl.aspx?typ=" + themeUrl + "&ID=" + hWebID.value;

                    qtipPopup2('.lnkBtn1',
                    "<div id='modalContainer'><div class='modalClose'><img src='/images/x.png' /></div><iframe id='NotifyFrame' src=" + URL + " height='470' width='670' scrolling='no' class='bgfill'></iframe></div>",
                    670, ".modalClose", "", "#NotifyFrame", "body #ContentPlaceHolder1_hModalStatusFlg");
                });

                $('.lnkBtn2').live('click', function (e) {
                    var themeUrl = $(this).attr('data-typ');
                    var hWebID = document.getElementById('<%=hWebinarID.ClientID %>');
                    var URL = "/Pages/popup/EmailPreview.aspx?typ=" + themeUrl + "&ID=" + hWebID.value;

                    qtipPopup2('.lnkBtn2',
                    "<div id='modalContainer'><div class='modalClose'><img src='/images/x.png' /></div><iframe id='NotifyFrame1' src=" + URL + " height='600' width='670' scrolling='no' class='bgfill'></iframe></div>",
                    670, ".modalClose", "", "#NotifyFrame1", "body #ContentPlaceHolder1_hModalStatusFlg");
                });

                $('.lnkBtn3').live('click', function (e) {
                    var themeUrl = $(this).attr('data-typ');
                    var hWebID = document.getElementById('<%=hWebinarID.ClientID %>');
                    var URL = "/Pages/popup/Notification_EmailTpl.aspx?typ=" + themeUrl + "&ID=" + hWebID.value;

                    qtipPopup2('.lnkBtn1',
                    "<div id='modalContainer'><div class='modalClose'><img src='/images/x.png' /></div><iframe id='NotifyFrame' src=" + URL + " height='600' width='670' scrolling='no' class='bgfill'></iframe></div>",
                    670, ".modalClose", "", "#NotifyFrame", "body #ContentPlaceHolder1_hModalStatusFlg");
                });

            });

            function confirmEmailSend(sMsg) {
                alert(sMsg);
            }
        </script>
    </telerik:RadCodeBlock>
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
    <telerik:RadCodeBlock ID="rcAdv" runat="server">
        <script>
            $("#spAdvSet").click(function () {
                $("#dvAdv").toggle();
                var toggleState = $('#dvAdv').is(':visible');
                if (toggleState) {
                    $("#spAdvSet").removeClass("collapseBtn");
                    $("#spAdvSet").addClass("expandBtn");
                } else {
                    $("#spAdvSet").removeClass("expandBtn");
                    $("#spAdvSet").addClass("collapseBtn");
                }
            });
        </script>
        <style>
            .collapseBtn
            {
                background: transparent url(../Images/icons/collapse.png) no-repeat;
                width: 16px;
                height: 16px;
                border: none;
            }
            .expandBtn
            {
                background: transparent url(../Images/icons/expand.png) no-repeat;
                width: 16px;
                height: 16px;
                border: none;
            }
            .rowgap
            {
                margin: 6px 10px;
            }
            .rowgapTxt
            {
                margin: 6px 10px;
                width: 105px;
            }
            .domBlkCollapse
            {
                background: transparent url(../Images/icons/plusExpandBtn.png) no-repeat;
                width: 16px;
                height: 16px;
                border: none;
            }
        </style>
    </telerik:RadCodeBlock>
</div>

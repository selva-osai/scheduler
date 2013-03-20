<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/popupMaster.Master"
    AutoEventWireup="true" CodeBehind="ImageCroping1.aspx.cs" Inherits="EBird.Web.App.Pages.popup.ImageCroping1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script src="../../js/jquery-1.7.1.min.js" type="text/javascript"></script>
    <script src="../../js/jquery.Jcrop.js" type="text/javascript"></script>
    <script type="text/javascript">
        jQuery(function ($) {
            var hImgType = document.getElementById('<%=hImgType.ClientID %>');
            var jcrop_api;
            initJcrop();

            function initJcrop() {
                $('#dvCropPanel').Jcrop({
                    onSelect: storeCoords,
                    onRelease: releaseCheck
                }, function () {
                    jcrop_api = this;
                    if (hImgType.value == 'BANNER') {
                        //                        jcrop_api.setOptions({ allowSelect: false, allowMove: true, minSize: [0, 0], maxSize: [600, 120]
                        jcrop_api.setOptions({ allowSelect: false, allowMove: true
                        });
                    }
                    else {
                        //                        jcrop_api.setOptions({ allowSelect: false, allowMove: true, minSize: [0, 0], maxSize: [180, 120]
                        jcrop_api.setOptions({ allowSelect: false, allowMove: true
                        });
                    }
                    jcrop_api.focus();
                });
            };

            function releaseCheck() {
                jcrop_api.setOptions({ allowSelect: true });
            };

            function getLogoCoord() {
                //    var dim = jcrop_api.getBounds();
                return [10, 10, 190, 130];
            };

            function getBannerCoord() {
                //   var dim = jcrop_api.getBounds();
                return [10, 10, 610, 130];
            };

            $('#setSelectLogo').click(function (e) {
                jcrop_api.setSelect(getLogoCoord());
                jcrop_api.setOptions({ minSize: [180, 120], maxSize: [180, 120] });
                jcrop_api.focus();
                return false;
            });

            $('#setSelectBanner').click(function (e) {
                jcrop_api.setSelect(getBannerCoord());
                jcrop_api.setOptions({ minSize: [600, 120], maxSize: [600, 120] });
                jcrop_api.focus();
                return false;
            });

            $('#release').click(function (e) {
                // Release method clears the selection
                jcrop_api.release();
                return false;
            });

            //jcrop_api.setSelect([crop.left, crop.top, crop.left + crop.width, crop.top + crop.height

            function storeCoords(c) {
                jQuery('#ContentPlaceHolder1_X').val(c.x);
                jQuery('#ContentPlaceHolder1_Y').val(c.y);
                jQuery('#ContentPlaceHolder1_W').val(c.w);
                jQuery('#ContentPlaceHolder1_H').val(c.h);
            };

        });
    </script>
    <style>
        .container
        {
            margin: 0 auto;
            cursor: move;
        }
        
        #dvCropPanel
        {
            width: 700px;
            height: 200px;
            text-align: center;
            vertical-align: middle;
            background-color: #e6e6e6;
            overflow: hidden;
            clear: both;
            border: 1px solid black;
            margin: 0 auto;
        }
        .imgPosition
        {
            margin-top: 20px;
        }
        .spImg
        {
            width: 80px;
            height: 50px;
            border: 3px solid #262626;
            margin: 3x 2px 3px 2px;
        }
    </style>
    <link rel="stylesheet" href="../../css/jquery.Jcrop.css" type="text/css" />
    <div class="container">
        <asp:HiddenField ID="hLogoCount" Value="0" runat="server" />
        <asp:HiddenField ID="hWebinarID" runat="server" />
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
                                        <asp:HiddenField ID="HiddenField1" Value="0" runat="server" />
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
                            height: 108px">
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
                            </asp:PlaceHolder>
                            <asp:PlaceHolder ID="phBanner" runat="server" Visible="false">
                                <ul>
                                    <li class="bannerList" style="width: 600px;" id='liBanner'>
                                        <div id="dvBannerContainer" class="bannerWrapper" style="width: 604px; height: 105px !important;"
                                            runat="server">
                                            <div class="binary-image" id="bimg1" runat="server">
                                                <asp:Image ID="imgBanner" runat="server" Width="600" Height="100" />
                                            </div>
                                            <div class="bnr-image-remove" id="bimgr2" runat="server">
                                                <asp:ImageButton ID="imgbtnDel2" onmouseover="this.src='/images/icons/icon-delete.png'"
                                                    OnClick="imgbtnDel2_Click" onmouseout="this.src='/images/icons/icon-delete1.png'"
                                                    runat="server" ImageUrl="/images/icons/icon-delete1.png"></asp:ImageButton>
                                                <asp:HiddenField ID="hBannerDocID" runat="server" />
                                            </div>
                                            <div style="clear: both; text-align: center">
                                                <a id="bannerHref" runat="server"></a>
                                                <asp:HyperLink ID="hlnkAttribute" runat="server" Text="[Add Attribute]" CssClass="lnkBtnLogoAttr" />
                                            </div>
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
        <table width="100%" align="center">
            <tr>
                <td>
                    <asp:Repeater ID="rpLogoThumb" runat="server" OnItemDataBound="rpLogoThumb_ItemDataBound"
                        OnItemCommand="rpLogoThumb_ItemCommand">
                        <ItemTemplate>
                            <span>
                                <asp:ImageButton CommandName="select" ID="imgThumb" CommandArgument='<%# Eval("DocID") %>'
                                    runat="server" CssClass="spImg" />
                            </span>
                        </ItemTemplate>
                    </asp:Repeater>
                </td>
            </tr>
        </table>
        <div style="clear: both">
        </div>
        <div style="background-image: url('/images/toolbarbg.png'); background-repeat: no-repeat;
            height: 30px;">
            <table width="100%">
                <tr>
                    <td style="margin-top: 5px">
                        &nbsp;&nbsp;<a href="#" id="setSelectBanner">Banner Select</a>&nbsp;|&nbsp;<a href="#"
                            id="setSelectLogo">Logo Select</a>&nbsp;|&nbsp;<a href="#" id="release">Release Select</a>
                    </td>
                    <td align="right" style="margin-top: 8px">
                        <asp:Button ID="btnCrop" runat="server" Text="Crop" class="SubBtn" OnClick="btnCrop_Click" />
                    </td>
                </tr>
            </table>
        </div>
        <div id="dvCropPanel">
            <asp:Image ID="imgCrop" runat="server" ImageAlign="AbsMiddle" ImageUrl="~/images/NoLogo.png"
                CssClass="imgPosition" />
        </div>
        <asp:Label ID="hDocID" runat="server"></asp:Label>
    </div>
    <asp:HiddenField ID="X" runat="server" />
    <asp:HiddenField ID="Y" runat="server" />
    <asp:HiddenField ID="W" runat="server" />
    <asp:HiddenField ID="H" runat="server" />
    <asp:HiddenField ID="hImgType" runat="server" />
    <asp:HiddenField ID="hUploadedFName" runat="server" />
    <asp:Panel ID="pnlCropped" runat="server" Visible="false">
        <asp:Image ID="imgCropped" runat="server" />
    </asp:Panel>
</asp:Content>

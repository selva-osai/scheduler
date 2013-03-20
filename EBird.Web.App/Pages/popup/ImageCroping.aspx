<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/popupMaster.Master"
    AutoEventWireup="true" CodeBehind="ImageCroping.aspx.cs" Inherits="EBird.Web.App.Pages.popup.ImageCroping" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script src="../../js/jquery-1.7.1.min.js" type="text/javascript"></script>
    <script src="../../js/jquery.Jcrop.js" type="text/javascript"></script>
    <%--    <script type="text/javascript">
        jQuery(function ($) {
            $('#dvCropPanel').Jcrop();
        });
    </script>
    --%>
    <script type="text/javascript">
        // jQuery(document).ready(function()
        jQuery(function ($) {
            var hImgType = document.getElementById('<%=hImgType.ClientID %>');
            var jcrop_api;
            initJcrop();

            function initJcrop() {
                //                if (hImgType.value == 'Banner')
                //                    var ele = "ContentPlaceHolder1_dvCropBannerPanel";
                //                else
                //                    var ele = "ContentPlaceHolder1_dvCropLogoPanel";
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
                    <td style="margin-top:5px">
                        &nbsp;&nbsp;<a href="#" id="setSelectBanner">Banner Select</a>&nbsp;|&nbsp;<a href="#" id="setSelectLogo">Logo
                            Select</a>&nbsp;|&nbsp;<a href="#" id="release">Release Select</a>
                    </td>
                    <td align="right" style="margin-top:8px">
                        <asp:Button ID="btnCrop" runat="server" Text="Crop" class="SubBtn" OnClick="btnCrop_Click" />
                    </td>
                </tr>
            </table>
        </div>
        <div id="dvCropPanel">
            <asp:Image ID="imgCrop" runat="server" ImageAlign="AbsMiddle" ImageUrl="~/images/NoLogo.png"
                CssClass="imgPosition" />
        </div><asp:Label ID="hDocID" runat="server"></asp:Label>
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

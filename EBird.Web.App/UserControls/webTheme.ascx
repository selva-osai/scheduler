<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="webTheme.ascx.cs" Inherits="EBird.Web.App.UserControls.webTheme" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<link href="../Styles/ThemeStyle.css" rel="stylesheet" type="text/css" />
<link rel="stylesheet" href="/css/carousel.css" charset="utf-8" />
<style type="text/css">
.RadComboBoxDropDown_Default .rcbHovered
{
	background: #EBEBEB;
	color: #C80000;
}
.ruUploadProgress, li .ruCancel, li .ruRemove
{
    visibility:hidden;
}
li.ruUploading
{
   display:none;
}
</style>
<div id="dvTheme">
    <table width="100%">
        <tr>
            <td>
                <asp:Label ID="lblpgCap2" runat="server" Font-Bold="True" Text="Webinar Header"></asp:Label>
                <asp:HiddenField ID="hWebinarID" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                <div class="FormCont Steps">
                    <table width="100%">
                        <tr>
                            <td>
                                <table>
                                    <tr>
                                        <td valign="top">
                                            <telerik:RadComboBox ID="rcmbHeader" runat="server" Width="180" CausesValidation="False"
                                                ViewStateMode="Enabled" AutoPostBack="True" OnSelectedIndexChanged="rcmbHeader_SelectedIndexChanged">
                                                <Items>
                                                    <telerik:RadComboBoxItem runat="server" Text="Upload Logo" Value="Logo" CssClass="ddStyle1" />
                                                    <telerik:RadComboBoxItem runat="server" Text="Upload Multiple Logo's" Value="Multi-logo"
                                                        CssClass="ddStyle1" />
                                                    <telerik:RadComboBoxItem runat="server" Text="Upload Banner" Value="Banner" CssClass="ddStyle1" />
                                                </Items>
                                            </telerik:RadComboBox>
                                        </td>
                                        <td>
                                            <telerik:RadAsyncUpload runat="server" ID="aupLogo" AllowedFileExtensions="jpeg,jpg,gif,png"
                                                MaxFileInputsCount="4" OnFileUploaded="aupLogo_FileUploaded" HttpHandlerUrl="~/handler/VideoUploadT1.ashx">
                                            </telerik:RadAsyncUpload>
                                            <asp:HiddenField ID="hCurrentImg" runat="server" Value="1" />
                                        </td>
                                    </tr>
                                </table>
                                <div style="clear: both">
                                </div>
                                <div class="logo-canvas" runat="server" id="logocanvas" visible="false">
                                    <asp:Repeater ID="rpLogo" runat="server" OnItemDataBound="rpLogo_ItemDataBound" OnItemCommand="rpLogo_ItemCommand">
                                        <ItemTemplate>
                                            <div class="binary-image" id="bimg1" runat="server">
                                                <telerik:RadBinaryImage runat="server" ID="Thumbnail1" Width="120" Height="90" ResizeMode="Fit"
                                                    ImageUrl="~/Images/icons/nologo.png" AlternateText="Thumbnail" CssClass="binary-image"
                                                    Visible="false" />
                                                <asp:Image ID="imgLogo" runat="server" />
                                            </div>
                                            <div class="image-remove" id="bimgr1" runat="server">
                                                <asp:ImageButton ID="imgbtnDel" onmouseover="this.src='/images/icons/icon-delete.png'" onmouseout="this.src='/images/icons/icon-delete1.png'"
                                                    runat="server" ImageUrl="/images/icons/icon-delete1.png"></asp:ImageButton>
                                            </div>
                                            <asp:HiddenField ID="hDocID" Value='<%# DataBinder.Eval(Container.DataItem, "DocID") %>'
                                                runat="server" />
                                            <asp:HiddenField ID="hResourceType" Value='<%# DataBinder.Eval(Container.DataItem, "ResourceType") %>'
                                                runat="server" />
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
        <%-- <tr>
            <td>
                <div class="FormCont Steps">
                    <table width="100%" border="1">
                        <tr>
                            <td colspan="2" valign="top">
                                <span>
                                    <telerik:RadProgressManager runat="server" ID="RadProgressManager1" />
                                    <telerik:RadAsyncUpload runat="server" ID="raupVideoFile" HttpHandlerUrl="~/handler/VideoUploadT1.ashx"
                                        MultipleFileSelection="Disabled">
                                    </telerik:RadAsyncUpload>
                                    <telerik:RadProgressArea runat="server" ID="RadProgressArea1">
                                    </telerik:RadProgressArea>
                                </span>

                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>--%>
        <tr>
            <td>
                <asp:Label ID="lblpgCap3" runat="server" Font-Bold="True" Text="Select a Webinar Theme"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <div class="FormCont Steps">
                    <table height="160">
                        <tr>
                            <td>
                                <div>
                                    <asp:Repeater ID="rpThemeCarousel" runat="server">
                                        <HeaderTemplate>
                                            <div class="carousel-container">
                                                <div id="carousel">
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <div class="carousel-feature">
                                                <img class="carousel-image" alt="Image Caption" src="../images/Theme/<%# DataBinder.Eval(Container.DataItem, "Name") %>">
                                                <div class="carousel-caption">
                                                    <p>
                                                        <input type="button" value="Preview" class="previewBtn carouselBtns" data-themeurl='/pages/layoutpreview/<%# getThemeID( Eval("Name")) %>'>
                                                        <input type="button" value="Pick this theme" class="selectThemeBtn carouselBtns"
                                                            data-themeid='<%# getThemeID( Eval("Name")) %>'>
                                                    </p>
                                                </div>
                                            </div>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            </div>
                                            <div id="carousel-left">
                                                <img src="../images/arrow-left.png" /></div>
                                            <div id="carousel-right">
                                                <img src="../images/arrow-right.png" /></div>
                                            </div>
                                        </FooterTemplate>
                                    </asp:Repeater>
                                    <input type="hidden" id="themeId">
                                    <asp:HiddenField ID="hSelThemeID" runat="server" Value="2" />
                                </div>
                                <!-- modal content -->
                                <div id="basic-modal-content">
                                    <h3>
                                        Theme Preview</h3>
                                    <div id="imgPreview">
                                    </div>
                                </div>
                                <!-- preload the images -->
                                <div style='display: none'>
                                    <img src='../images/x.png' alt='' />
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>
    <script src="/js/jquery.featureCarousel.min.js"
        type="text/javascript" charset="utf-8"></script>
    <script src="/js/jquery.simplemodal.js" type="text/javascript" charset="utf-8"></script>
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

        $(document).ready(function () {
            var carousel = $("#carousel").featureCarousel({
                startingFeature: getHValue(),
                autoPlay: isNewWebinar(),
                trackerIndividual: false,
                pauseOnHover: true,
                trackerSummation: false
            })
        });
        jQuery(function ($) {
            // Load dialog on click
            //            $('.previewBtn').live('click', function (e) {
            //                var imageUrl = $(this).attr('data-img');
            //                //var imageUrl = $('.previewBtn').find('.carousel-image').attr('data-img');
            //                $('#imgPreview').html('<img src=\"' + imageUrl + '\" width="580" height="340"/>');
            //                $('#basic-modal-content').modal({
            //                    overlayClose: true
            //                });
            //                return false;
            //            });

            // Load dialog on click
            $('.previewBtn').live('click', function (e) {
                var themeUrl = $(this).attr('data-themeurl');
                $('#imgPreview').html('<iframe src="' + themeUrl + '" width="675" height="500"></iframe>');
                //$('#imgPreview').html('<img src=\"' + imageUrl + '\" width="580" height="340"/>');
                $('#basic-modal-content').modal({
                    overlayClose: true
                });
                return false;
            });

            $('.selectThemeBtn').live('click', function (e) {
                //use the following themeId to handle things dynamically
                $('#themeId').val($(this).attr('data-themeid'));
            });
        });
		
    </script>
</div>

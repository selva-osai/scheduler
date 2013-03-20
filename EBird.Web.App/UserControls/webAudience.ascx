<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="webAudience.ascx.cs"
    Inherits="EBird.Web.App.UserControls.webAudience" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<link href="../Styles/ThemeStyle.css" rel="stylesheet" type="text/css" />
<link href="/css/carousel1.css" rel="stylesheet" type="text/css" />
<style type="text/css">
    .RadComboBoxDropDown_Default .rcbHovered
    {
        background: #EBEBEB;
        color: #C80000;
    }
    .ruUploadProgress, li .ruCancel, li .ruRemove
    {
        visibility: hidden;
    }
    li.ruUploading
    {
        display: none;
    }
</style>
<div id="dvAudience">
    <table width="100%">
        <tr>
            <td>
                <div class="FormCont Steps" style="margin-bottom: 10px">
                    <table width="100%">
                        <tr>
                            <td colspan="2" align="left" style="padding-bottom: 10px;">
                                <asp:Label ID="lblpgCap1" runat="server" Font-Bold="True" Text=" Upload Presentation Slides"
                                    CssClass="frmHeading"></asp:Label>
                                <asp:HiddenField ID="hWebinarID" runat="server" Value="0" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="text-align: left; padding-left: 5px;">
                                <div style="float: left; margin-right: 1px;">
                                    <telerik:RadAsyncUpload runat="server" ID="aupPresentation" AllowedFileExtensions="ppt,pptx,doc,docx,pdf,xls,xlsx,xps,txt,csv"
                                        Localization-Select="Upload" InputSize="30" CssClass="rAU4" OnFileUploaded="aupPresentation_FileUploaded"
                                        OnClientFileUploaded="fileUploaded" OnClientValidationFailed="validationFailed"
                                        HttpHandlerUrl="~/handler/VideoUploadT1.ashx?ftyp=p&u=1">
                                    </telerik:RadAsyncUpload>
                                    <asp:LinkButton ID="lnkUploadPPT" runat="server" Text="" OnClick="lnkUploadPPT_Click"
                                        CausesValidation="false"></asp:LinkButton>
                                </div>
                                <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
                                    <script type="text/javascript">
                                        function fileUploaded(sender, args) {
                                            $telerik.$(".invalid").html("");
                                            setTimeout(function () { sender.deleteFileInputAt(0); }, 10);
                                            __doPostBack('ctl00$ContentPlaceHolder1$schMeeting1$webAudience1$lnkUploadPPT', '');
                                        }
                                        function validationFailed(sender, args) {
                                            $telerik.$(".invalid").html("Invalid extension, please choose an image file").get(0).style.display = "block";
                                            sender.deleteFileInputAt(0);
                                        } 
                                    </script>
                                </telerik:RadCodeBlock>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" colspan="2" style="padding-left: 5px;">
                                <telerik:RadGrid ID="tgrdPPTList" runat="server" GridLines="None" AllowPaging="False"
                                    PageSize="15" MasterTableView-EditFormSettings-EditFormType="Template" AllowSorting="True"
                                    AutoGenerateColumns="False" ValidationSettings-EnableValidation="false" OnItemCommand="tgrdPPTList_ItemCommand"
                                    OnRowDrop="tgrdPPTList_RowDrop" OnItemDataBound="tgrdPPTList_ItemDataBound" MasterTableView-NoDetailRecordsText="No Webinar Presentation Uploaded"
                                    BorderColor="#D7D7D7">
                                    <ExportSettings HideStructureColumns="true" />
                                    <MasterTableView CommandItemDisplay="None" AutoGenerateColumns="false" AllowFilteringByColumn="False"
                                        DataKeyNames="ResourceID" ShowFooter="false" TableLayout="Auto" Width="100%">
                                        <NoRecordsTemplate>
                                            <center> <br />No Webinar Presentation Uploaded<br />&nbsp; </center>
                                        </NoRecordsTemplate>
                                        <RowIndicatorColumn>
                                            <HeaderStyle Width="20px" />
                                        </RowIndicatorColumn>
                                        <ExpandCollapseColumn>
                                            <HeaderStyle Width="20px" />
                                        </ExpandCollapseColumn>
                                        <Columns>
                                            <telerik:GridTemplateColumn HeaderText="Order" UniqueName="DocOrder" HeaderStyle-Width="20px">
                                                <ItemStyle HorizontalAlign="Center" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblOrder" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn HeaderText="File Name" UniqueName="fileName" HeaderStyle-Width="210px"
                                                AllowFiltering="false" SortExpression="fielName">
                                                <ItemTemplate>
                                                    <span>
                                                        <asp:Image ID="imgFileTyp" runat="server" /></span> <span>
                                                            <asp:Label ID="lblFileName" runat="server"></asp:Label></span>
                                                </ItemTemplate>
                                                <HeaderStyle Width="420px" />
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn HeaderText="File Size" UniqueName="filesize" HeaderStyle-Width="180px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblFileSize" runat="server"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Width="110px" />
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn HeaderText="Slides" UniqueName="NoOfSlides" HeaderStyle-Width="50px" Visible="false">
                                                <ItemStyle HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSlideCount" runat="server"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Width="50px" />
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn HeaderText="Add to Briefcase" UniqueName="IsBriefCase">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="btnChecked" runat="server" CommandArgument='<%# Eval("DocID") %>'
                                                        CommandName="CHK" ImageUrl="~/Images/icons/icon-chked.png" ToolTip="In Briefcase"
                                                        Visible="false" />
                                                    <asp:ImageButton ID="btnUnChecked" runat="server" CommandArgument='<%# Eval("DocID") %>'
                                                        CommandName="UNCHK" ImageUrl="~/Images/icons/icon-chk.png" ToolTip="Not in Briefcase"
                                                        Visible="false" />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" Width="120px" />
                                                <ItemStyle HorizontalAlign="Center" Width="120px" />
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn HeaderText="">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="btnDelete" runat="server" CommandArgument='<%# Eval("DocID") %>' OnClientClick="return confirm('Are you sure, you want to this presentation?');"
                                                        CommandName="DEL" ImageUrl="~/Images/icons/trash.png" ToolTip="Delete" />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" Width="80px" />
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridBoundColumn DataField="ResourceID" Visible="false" />
                                            <telerik:GridBoundColumn DataField="DocID" Visible="false" />
                                            <telerik:GridBoundColumn DataField="ResourceTitle" Visible="false" />
                                            <telerik:GridBoundColumn DataField="IsBriefcase" UniqueName="IsBriefcase" Visible="false" />
                                        </Columns>
                                    </MasterTableView>
                                    <ClientSettings AllowRowsDragDrop="True" AllowColumnsReorder="true" ReorderColumnsOnClient="true">
                                        <Selecting AllowRowSelect="True" EnableDragToSelectRows="false"></Selecting>
                                        <ClientEvents OnRowMouseOver="RowMouseOver" OnRowMouseOut="RowMouseOut" />
                                    </ClientSettings>
                                </telerik:RadGrid>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <div class="FormCont Steps" style="margin-bottom: 10px">
                    <table height="100">
                        <tr>
                            <td>
                                <asp:Label ID="lblpgCap2" runat="server" Font-Bold="True" Text="Select Audience Background"
                                    CssClass="frmHeading"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:Repeater ID="rpAudBGCarousel" runat="server" OnItemDataBound="rpAudBGCarousel_ItemDataBound">
                                    <HeaderTemplate>
                                        <div class="image_carousel1">
                                            <div id="foo2">
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <img src='~/Images/backgrounds/<%# DataBinder.Eval(Container.DataItem, "Name") %>'
                                            runat="server" id="imgBG" alt="" width="140" height="100" class="previewBtn" />
                                        <asp:HiddenField ID="hBackGroundID" Value='<%# DataBinder.Eval(Container.DataItem, "Name") %>'
                                            runat="server" />
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        </div>
                                        <div class="clearfix">
                                        </div>
                                        <a class="prev1" id="foo2_prev" href="#"><span>prev</span></a> <a class="next1" id="foo2_next"
                                            href="#"><span>next</span></a> </div>
                                    </FooterTemplate>
                                </asp:Repeater>
                                <asp:HiddenField ID="hSelBgID" runat="server" Value="2" />
                                <telerik:RadCodeBlock ID="rcCarosel" runat="server">
                                    <script src="/js/jquery.carouFredSel-6.0.5-packed.js" type="text/javascript" charset="utf-8"></script>
                                    <script>
                                        $("#foo2").carouFredSel({
                                            items: 4,
                                            start: 4,
                                            scroll: {
                                                items: 4,
                                                duration: 800,
                                                timeoutDuration: 2000
                                            },
                                            auto: false,
                                            prev: "#foo2_prev",
                                            next: "#foo2_next"
                                        }).parent().css("margin", "auto");
                                    </script>
                                    <script src="/js/jquery.simplemodal.js" type="text/javascript" charset="utf-8"></script>
                                    <script type="text/javascript">
                                        function getHValue() {
                                            var hVal = document.getElementById('<%=hSelBgID.ClientID %>');
                                            if (hVal != null)
                                                return hVal.value;
                                            else
                                                return 3;
                                        }

                                        function isNewWebinar() {
                                            var hVal = document.getElementById('<%=hSelBgID.ClientID %>');

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
                                                var themeUrl = $(this).attr('data-themeurl');
                                                var docID = $(this).attr('data-docID');
                                                var selDoc = document.getElementById('<%=hSelBgID.ClientID %>');
                                                if (selDoc != null)
                                                    selDoc.value = docID;

                                                $.modal('<iframe src="' + themeUrl + '" height="475" width="740" style="border:0">', {
                                                    containerCss: {
                                                        backgroundColor: "#fff",
                                                        borderColor: "#e0e0e0",
                                                        height: 475,
                                                        padding: 5,
                                                        width: 740
                                                    },
                                                    overlayClose: true
                                                });

                                                $('.image_carousel1 img').removeClass('selected');
                                                $(this).addClass('selected');
                                                return false;
                                            });

                                            $('.selectThemeBtn').live('click', function (e) {
                                                //use the following themeId to handle things dynamically
                                                $('#themeId').val($(this).attr('data-themeid'));
                                            });
                                        });
		
                                    </script>
                                </telerik:RadCodeBlock>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <div class="FormCont Steps" style="margin-bottom: 10px">
                    <table border="0" width="30%">
                        <tr>
                            <td align="left" colspan="4" style="padding-bottom: 10px;">
                                <asp:Label ID="Label1" runat="server" Font-Bold="True" Text="Select Audience Component"
                                    CssClass="frmHeading"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <img runat="server" id="cmpBio" src="~/images/features/Bio_1.png" alt="" />
                            </td>
                            <td>
                                <table border="0" style="width: 270px">
                                    <tr>
                                        <td class="style1">
                                            <asp:CheckBox ID="chkConfig13" runat="server" CssClass="chkGen chkAlign1" Enabled="false"
                                                Text="Presenter Bio" ForeColor="#cccccc" value="" />
                                            <span style="margin-top: 5px; float: left">
                                                <asp:HyperLink ID="lbtnConfig13" runat="server" Text="[Edit]" Visible="false" CssClass="lnkBtn1b"
                                                    CausesValidation="false" />
                                            </span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding-left: 23px;">
                                            <asp:Label ID="lblConfig13" runat="server" Text="Allow audience to read presenter bios" ForeColor="#cccccc"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <%--<td>
                                <img runat="server" id="cmpSlide" src="~/images/features/download.png" alt="Download Slide" />
                            </td>
                            <td>
                                <table border="0" style="width: 270px">
                                    <tr>
                                        <td class="style1">
                                            <asp:CheckBox ID="chkConfig10" runat="server" CssClass="chkGen chkAlign1" Enabled="false"
                                                Text="Download Slides" ForeColor="#cccccc" value="" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding-left: 23px;">
                                            Allow audience to download slides
                                        </td>
                                    </tr>
                                </table>
                            </td>--%>
                            <td>
                                <img runat="server" id="cmpChat" src="~/images/features/chat.png" alt="Audience Chat" />
                            </td>
                            <td>
                                <table border="0" width="270px">
                                    <tr>
                                        <td>
                                            <asp:CheckBox ID="chkConfig11" runat="server" CssClass="chkGen chkAlign1" Enabled="false"
                                                Text="Audience Chat" ForeColor="#cccccc" value="" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding-left: 23px;">
                                            <asp:Label ID="lblConfig11" runat="server" Text="Allow audience to chat with others" ForeColor="#cccccc"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <img runat="server" id="cmpQA" src="~/images/features/QA.png" alt="Submit Question" />
                            </td>
                            <td>
                                <table border="0" style="width: 270px">
                                    <tr>
                                        <td class="style1">
                                            <asp:CheckBox ID="chkConfig12" runat="server" CssClass="chkGen chkAlign1" Enabled="false"
                                                Text="Submit Question" ForeColor="#cccccc" value="" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding-left: 23px;">
                                            <asp:Label ID="lblConfig12" runat="server" Text="Allow audience to type a question" ForeColor="#cccccc"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td>
                                <img runat="server" id="cmpWiki" src="~/images/features/Wiki.png" alt="Wikipedia" />
                            </td>
                            <td>
                                <table border="0" width="270px">
                                    <tr>
                                        <td>
                                            <asp:CheckBox ID="chkConfig14" runat="server" CssClass="chkGen chkAlign1" Enabled="false"
                                                Text="Wikipedia" ForeColor="#cccccc" value="" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding-left: 23px;">
                                            <asp:Label ID="lblConfig14" runat="server" Text="Allow audience to use Wikipedia" ForeColor="#cccccc"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <img runat="server" id="cmpContent" src="~/images/features/briefcase.png" alt="Briefcase" />
                            </td>
                            <td>
                                <table border="0" style="width: 270px">
                                    <tr>
                                        <td>
                                            <asp:CheckBox ID="chkConfig8" runat="server" CssClass="chkGen chkAlign1" Enabled="false"
                                                Text="Briefcase" ForeColor="#cccccc" value="" />
                                            <span style="margin-top: 5px; float: left">
                                                <asp:HyperLink ID="lbtnConfig8" runat="server" Text="[Edit]" Visible="false" CssClass="lnkBtn1b lnkBtn1bM"
                                                    CausesValidation="false" />
                                            </span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding-left: 23px;">
                                            <asp:Label ID="lblConfig8" runat="server" Text="Allow audience to download content" ForeColor="#cccccc"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td>
                                <img runat="server" id="cmpFB" src="~/images/features/facebook.png" alt="Facebook" />
                            </td>
                            <td>
                                <table border="0" style="width: 270px">
                                    <tr>
                                        <td class="style1">
                                            <asp:CheckBox ID="chkConfig15" runat="server" CssClass="chkGen chkAlign1" Enabled="false"
                                                Text="Facebook" ForeColor="#cccccc" value="" />
                                            <span style="margin-top: 5px; float: left">
                                                <asp:HyperLink ID="lbtnConfig15" runat="server" Text="[Edit]" Visible="false" CssClass="lnkBtn1b"
                                                    CausesValidation="false" />
                                            </span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding-left: 23px;">
                                            <asp:Label ID="lblConfig15" runat="server" Text="Allow access to designated Facebook page" ForeColor="#cccccc"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <img runat="server" id="cmpGoogle" src="~/images/features/Search.png" alt="Search" />
                            </td>
                            <td>
                                <table border="0" style="width: 270px">
                                    <tr>
                                        <td class="style1">
                                            <asp:CheckBox ID="chkConfig19" runat="server" CssClass="chkGen chkAlign1" Enabled="false"
                                                Text="Search" ForeColor="#cccccc" value="" />
                                            <span style="margin-top: 5px; float: left">
                                                <asp:HyperLink ID="lbtnConfig19" runat="server" Text="[Edit]" Visible="false" CssClass="lnkBtn1b"
                                                    CausesValidation="false" />
                                            </span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding-left: 23px;">
                                            <asp:Label ID="lblConfig19" runat="server" Text="Allow audience to search the web" ForeColor="#cccccc"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td>
                                <img runat="server" id="cmpTweeter" src="~/images/features/twitter.png" alt="Twitter" />
                            </td>
                            <td>
                                <table border="0" style="width: 270px">
                                    <tr>
                                        <td class="style1">
                                            <asp:CheckBox ID="chkConfig16" runat="server" CssClass="chkGen chkAlign1" Enabled="false"
                                                Text="Twitter" ForeColor="#cccccc" value="" />
                                            <span style="margin-top: 5px; float: left">
                                                <asp:HyperLink ID="lbtnConfig16" runat="server" Text="[Edit]" Visible="false" CssClass="lnkBtn1b"
                                                    CausesValidation="false" />
                                            </span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding-left: 23px;">
                                            
                                            <asp:Label ID="lblConfig16" runat="server" Text="Allow access to designated Twitter page" ForeColor="#cccccc"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <img runat="server" id="cmpFriend" src="~/images/features/Friend.png" alt="Share" />
                            </td>
                            <td>
                                <table border="0" style="width: 270px">
                                    <tr>
                                        <td class="style1">
                                            <asp:CheckBox ID="chkConfig9" runat="server" CssClass="chkGen chkAlign1" Enabled="false"
                                                Text="Share" ForeColor="#cccccc" value="" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding-left: 23px;">
                                            <asp:Label ID="lblConfig9" runat="server" Text="Allow audience to email webinar" ForeColor="#cccccc"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td>
                                <img runat="server" id="cmpLI" src="~/images/features/linkedIn.png" alt="" />
                            </td>
                            <td>
                                <table border="0" style="width: 270px">
                                    <tr>
                                        <td class="style1">
                                            <asp:CheckBox ID="chkConfig17" runat="server" CssClass="chkGen chkAlign1" Enabled="false"
                                                Text="LinkedIn" ForeColor="#cccccc" value="" />
                                            <span style="margin-top: 5px; float: left">
                                                <asp:HyperLink ID="lbtnConfig17" runat="server" Text="[Edit]" Visible="false" CssClass="lnkBtn1b"
                                                    CausesValidation="false" />
                                            </span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding-left: 23px;">
                                            <asp:Label ID="lblConfig17" runat="server" Text="Allow access to designated LinkedIn page" ForeColor="#cccccc"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
        <tr>
            <td style="padding: 5px 5px 5px 15px;">
                <%--<div class="FormCont Steps">--%>
                <asp:LinkButton ID="lbtnPreviewAI" CssClass="lnkBtn1 ht1" runat="server" Text="Preview Audience Interface"></asp:LinkButton>
                <%--</div>--%>
            </td>
        </tr>
    </table>
    <script src="/qtip2/qtipCommonfn.js" type="text/javascript" charset="utf-8"></script>
    <script type="text/javascript">
        var hWebID = document.getElementById('<%=hWebinarID.ClientID %>');
    </script>
    <script src="/qtip2/qtip_audi_click.js" type="text/javascript" charset="utf-8"></script>
</div>

<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/popupMaster.Master"
    AutoEventWireup="true" CodeBehind="Notification_EmailTpl.aspx.cs" Inherits="EBird.Web.App.Pages.popup.Notification_EmailTpl" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Label ID="lblpgCap2" runat="server" CssClass="pgCap2" Text=""></asp:Label>
    <asp:PlaceHolder ID="phEdit" runat="server" Visible="false">
        <table width="600">
            <tr>
                <td>
                    <asp:HiddenField ID="hReqType" runat="server" />
                    <div class="regOutline2">
                        <table width="100%">
                            <tr>
                                <td style="padding-left: 8px;">
                                    <asp:Label ID="lblTo" runat="server" Text="To"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtTo" runat="server" CssClass="textbox_Email1"></asp:TextBox>&nbsp;
                                    <asp:Button runat="server" ID="btnSendEmail" Text="Send Test" CssClass="SubBtn" Visible="true"
                                        OnClick="btnSendEmail_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td style="padding-left: 8px;">
                                    <asp:Label ID="lblSubject" runat="server" Text="Subject"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtSubject" runat="server" CssClass="textbox_Email1"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div class="regOutline2">
                        <table border="0" width="100%" align="center">
                            <tr>
                                <td>
                                    <img src="/images/blank.gif" height="5" />
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <div>
                                        <telerik:RadEditor runat="server" ID="redtRemEmail" ToolbarMode="Default" Height="220px"
                                            ToolsFile="~/editor/BasicTools1.xml" BorderStyle="None" CssClass="rteditor1 rteditor1W"
                                            ToolsWidth="100%" EnableResize="False" EditModes="Design">
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
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <asp:CheckBox ID="chkSysReq" CssClass="chkGen" runat="server" Text="Include System Requirement" /><br />
                                    <asp:CheckBox ID="chkOutlook" CssClass="chkGen" runat="server" Text="Include link to add to Outlook Calender" /><br />
                                    <asp:CheckBox ID="chkRelWebinar" CssClass="chkGen" runat="server" Text="Include related additional webinar"
                                        Visible="false" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
            <tr>
                <td style="padding-top: 8px;">
                    <asp:Button runat="server" ID="btnSave" Text="Save" CssClass="SubBtn" Visible="true"
                        OnClick="btnSave_Click" />&nbsp;<asp:Button runat="server" ID="btnCancel" Text="Cancel"
                            OnClick="btnCancel_Click" CssClass="SubBtn" Visible="true" />&nbsp;&nbsp;<asp:Label
                                ID="lblError" runat="server" ForeColor="Red"></asp:Label>
                </td>
            </tr>
        </table>
    </asp:PlaceHolder>
    <asp:PlaceHolder ID="phView" runat="server" Visible="false">
    <link href="/css/flexcrollstyles.css" rel="stylesheet" type="text/css" />
	<script type='text/javascript' src="/js/flexcroll.js"></script>
        <table>
            <tr>
                <td>
                    <%--<div class="regOutline" style="min-height:300px !important">--%>
                    <div class='flexcroll'>
                        <asp:Literal ID="ltrEmailContent" runat="server"></asp:Literal>
                    </div>
                    <%--</div>--%>
                </td>
            </tr>
            <tr>
                <td style="padding: 15px 2px 15px 16px">
                    <asp:TextBox ID="txtReviewerEmail" runat="server" CssClass="textbox_EB3Review" Visible="false"></asp:TextBox>
                    <asp:TextBoxWatermarkExtender ID="txtWatermark" runat="server" TargetControlID="txtReviewerEmail"
                        WatermarkText="Separate multiple addresses with semi-colon(;)" WatermarkCssClass="watermarked_EBReview watermarked_EB3Review" />
                    <br />
                    <asp:Button runat="server" ID="btnReview" Text="Send for Review" CssClass="SubBtn"
                        Visible="false" OnClick="btnReview_Click" />&nbsp;<asp:Label ID="lblReviewError"
                            runat="server" ForeColor="Red"></asp:Label>
                </td>
            </tr>
        </table>
    </asp:PlaceHolder>
    <asp:HiddenField ID="hWebinarID" runat="server" Value="0" />
    <asp:HiddenField ID="hModalStatusFlg" runat="server" />
</asp:Content>

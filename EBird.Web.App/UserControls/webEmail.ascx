<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="webEmail.ascx.cs" Inherits="EBird.Web.App.UserControls.webEmail" %>
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
</style>
<div id="dvEmail">
    <asp:HiddenField ID="hWebinarID" runat="server" />
    <table width="100%" align="center">
        <tr>
            <td>
                <div class="Steps" style="margin-bottom: 10px">
                    <table>
                        <tr>
                            <td style="padding-bottom: 5px;">
                                <asp:Label ID="lblConfirmEmail" runat="server" Font-Bold="True" Text="Confirmation Email" />
                            </td>
                        </tr>
                        <tr>
                            <td height="24" style="padding-left: 5px">
                                <asp:CheckBox ID="chkSendAll" runat="server" CssClass="FLeft" />&nbsp; Send to all
                                registrants
                            </td>
                        </tr>
                        <tr>
                            <td height="24" style="padding-left: 5px">
                                <asp:HyperLink ID="lbtnEmailPreviewConf" CssClass="lnkBtn2" data-typ="COP" runat="server"
                                    Text="Preview"></asp:HyperLink>
                                <asp:Literal ID="ltrSepA1" runat="server" Text="&nbsp;|&nbsp;"></asp:Literal>
                                <asp:HyperLink ID="lbtnEmailEditConf" CssClass="lnkBtn1" data-typ="COE" runat="server"
                                    Text="Edit"></asp:HyperLink>
                                <asp:Literal ID="ltrSepA2" runat="server" Text="&nbsp;|&nbsp;"></asp:Literal>
                                <asp:HyperLink ID="lblEmailReviewConf" CssClass="lnkBtn1" data-typ="COR" runat="server"
                                    Text="Send for Review"></asp:HyperLink>
                                <asp:HiddenField ID="hRegConfirmEmailContentID" runat="server" />
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>
    <table width="100%" align="center">
        <tr>
            <td>
                <div class="Steps" style="margin-bottom: 10px">
                    <table>
                        <tr>
                            <td style="padding-bottom: 5px;">
                                <asp:Label ID="lblEmailReminder" runat="server" Font-Bold="True" Text="Reminder Emails to Registrants" />
                            </td>
                        </tr>
                        <tr>
                            <td style="padding-top: 6px !important; vertical-align: middle; padding-left: 5px">
                                &nbsp;
                                <asp:CheckBox ID="chkRem1" runat="server" CssClass="FLeft chk-place" />
                                <telerik:RadComboBox ID="ddRem1Value" runat="server" CssClass="cmbGen" Width="40"
                                    MarkFirstMatch="True" HighlightTemplatedItems="True" ExpandAnimation-Duration="1000"
                                    CollapseAnimation-Duration="1000" CollapseAnimation-Type="OutQuart" NoWrap="True"
                                    EnableTheming="True" Font-Italic="False" Skin="Default">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="15" Value="15"></telerik:RadComboBoxItem>
                                        <telerik:RadComboBoxItem Text="20" Value="20"></telerik:RadComboBoxItem>
                                        <telerik:RadComboBoxItem Text="25" Value="25"></telerik:RadComboBoxItem>
                                        <telerik:RadComboBoxItem Text="30" Value="30"></telerik:RadComboBoxItem>
                                        <telerik:RadComboBoxItem Text="45" Value="45"></telerik:RadComboBoxItem>
                                    </Items>
                                </telerik:RadComboBox>
                                <telerik:RadComboBox ID="ddRem1Type" runat="server" CssClass="cmbGen" Width="90"
                                    MarkFirstMatch="True" HighlightTemplatedItems="True" ExpandAnimation-Duration="1000"
                                    CollapseAnimation-Duration="1000" CollapseAnimation-Type="OutQuart" NoWrap="True"
                                    EnableTheming="True" Font-Italic="False" Skin="Default">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Minutes" Value="M" Selected="True"></telerik:RadComboBoxItem>
                                    </Items>
                                </telerik:RadComboBox>
                                <%--<span style="padding-top:3px !important;"><asp:TextBox ID="txtRem1Min" runat="server" CssClass="textbox_EB textbox_EB5" BackColor="#ffffff" Enabled="false" Text="Minutes"></asp:TextBox></span>--%>
                                Before<asp:HiddenField ID="hRem1" runat="server" Value="0" />
                            </td>
                        </tr>
                        <tr>
                            <td style="padding-left: 5px">
                                &nbsp;
                                <asp:CheckBox ID="chkRem2" runat="server" CssClass="FLeft chk-place" />
                                <telerik:RadComboBox ID="ddRem2Value" runat="server" CssClass="cmbGen" Width="40"
                                    MarkFirstMatch="True" HighlightTemplatedItems="True" ExpandAnimation-Duration="1000"
                                    CollapseAnimation-Duration="1000" CollapseAnimation-Type="OutQuart" NoWrap="True"
                                    EnableTheming="True" Font-Italic="False" Skin="Default">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="1" Value="1" Selected="True"></telerik:RadComboBoxItem>
                                        <telerik:RadComboBoxItem Text="2" Value="2"></telerik:RadComboBoxItem>
                                        <telerik:RadComboBoxItem Text="3" Value="3"></telerik:RadComboBoxItem>
                                        <telerik:RadComboBoxItem Text="4" Value="4"></telerik:RadComboBoxItem>
                                    </Items>
                                </telerik:RadComboBox>
                                <telerik:RadComboBox ID="ddRem2Type" runat="server" CssClass="cmbGen" Width="90"
                                    MarkFirstMatch="True" HighlightTemplatedItems="True" ExpandAnimation-Duration="1000"
                                    CollapseAnimation-Duration="1000" CollapseAnimation-Type="OutQuart" NoWrap="True"
                                    EnableTheming="True" Font-Italic="False" Skin="Default">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Hours" Value="H" Selected="True"></telerik:RadComboBoxItem>
                                        <telerik:RadComboBoxItem Text="Days" Value="D"></telerik:RadComboBoxItem>
                                        <telerik:RadComboBoxItem Text="Weeks" Value="W"></telerik:RadComboBoxItem>
                                    </Items>
                                </telerik:RadComboBox>
                                Before<asp:HiddenField ID="hRem2" runat="server" Value="0" />
                            </td>
                        </tr>
                        <tr>
                            <td style="padding-left: 5px">
                                &nbsp;
                                <asp:CheckBox ID="chkRem3" runat="server" CssClass="FLeft chk-place" />
                                <telerik:RadComboBox ID="ddRem3Value" runat="server" CssClass="cmbGen" Width="40"
                                    MarkFirstMatch="True" HighlightTemplatedItems="True" ExpandAnimation-Duration="1000"
                                    CollapseAnimation-Duration="1000" CollapseAnimation-Type="OutQuart" NoWrap="True"
                                    EnableTheming="True" Font-Italic="False" Skin="Default">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="1" Value="1" Selected="True"></telerik:RadComboBoxItem>
                                        <telerik:RadComboBoxItem Text="2" Value="2"></telerik:RadComboBoxItem>
                                        <telerik:RadComboBoxItem Text="3" Value="3"></telerik:RadComboBoxItem>
                                        <telerik:RadComboBoxItem Text="4" Value="4"></telerik:RadComboBoxItem>
                                    </Items>
                                </telerik:RadComboBox>
                                <telerik:RadComboBox ID="ddRem3Type" runat="server" CssClass="cmbGen" Width="90"
                                    MarkFirstMatch="True" HighlightTemplatedItems="True" ExpandAnimation-Duration="1000"
                                    CollapseAnimation-Duration="1000" CollapseAnimation-Type="OutQuart" NoWrap="True"
                                    EnableTheming="True" Font-Italic="False" Skin="Default">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Hours" Value="H" Selected="True"></telerik:RadComboBoxItem>
                                        <telerik:RadComboBoxItem Text="Days" Value="D"></telerik:RadComboBoxItem>
                                        <telerik:RadComboBoxItem Text="Weeks" Value="W"></telerik:RadComboBoxItem>
                                    </Items>
                                </telerik:RadComboBox>
                                Before<asp:HiddenField ID="hRem3" runat="server" Value="0" />
                            </td>
                        </tr>
                        <tr>
                            <td style="padding-left: 5px">
                                &nbsp;
                                <asp:CheckBox ID="chkRem4" runat="server" CssClass="FLeft chk-place" />
                                <telerik:RadComboBox ID="ddRem4Value" runat="server" CssClass="cmbGen" Width="40"
                                    MarkFirstMatch="True" HighlightTemplatedItems="True" ExpandAnimation-Duration="1000"
                                    CollapseAnimation-Duration="1000" CollapseAnimation-Type="OutQuart" NoWrap="True"
                                    EnableTheming="True" Font-Italic="False" Skin="Default">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="1" Value="1" Selected="True"></telerik:RadComboBoxItem>
                                        <telerik:RadComboBoxItem Text="2" Value="2"></telerik:RadComboBoxItem>
                                        <telerik:RadComboBoxItem Text="3" Value="3"></telerik:RadComboBoxItem>
                                        <telerik:RadComboBoxItem Text="4" Value="4"></telerik:RadComboBoxItem>
                                    </Items>
                                </telerik:RadComboBox>
                                <telerik:RadComboBox ID="ddRem4Type" runat="server" CssClass="cmbGen" Width="90"
                                    MarkFirstMatch="True" HighlightTemplatedItems="True" ExpandAnimation-Duration="1000"
                                    CollapseAnimation-Duration="1000" CollapseAnimation-Type="OutQuart" NoWrap="True"
                                    EnableTheming="True" Font-Italic="False" Skin="Default">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Hours" Value="H" Selected="True"></telerik:RadComboBoxItem>
                                        <telerik:RadComboBoxItem Text="Days" Value="D"></telerik:RadComboBoxItem>
                                        <telerik:RadComboBoxItem Text="Weeks" Value="W"></telerik:RadComboBoxItem>
                                    </Items>
                                </telerik:RadComboBox>
                                Before<asp:HiddenField ID="hRem4" runat="server" Value="0" />
                            </td>
                        </tr>
                        <tr>
                            <td style="padding-left: 5px">
                                &nbsp;
                                <asp:CheckBox ID="chkRem5" runat="server" CssClass="FLeft chk-place" />
                                <telerik:RadComboBox ID="ddRem5Value" runat="server" CssClass="cmbGen" Width="40"
                                    MarkFirstMatch="True" HighlightTemplatedItems="True" ExpandAnimation-Duration="1000"
                                    CollapseAnimation-Duration="1000" CollapseAnimation-Type="OutQuart" NoWrap="True"
                                    EnableTheming="True" Font-Italic="False" Skin="Default">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="1" Value="1" Selected="True"></telerik:RadComboBoxItem>
                                        <telerik:RadComboBoxItem Text="2" Value="2"></telerik:RadComboBoxItem>
                                        <telerik:RadComboBoxItem Text="3" Value="3"></telerik:RadComboBoxItem>
                                        <telerik:RadComboBoxItem Text="4" Value="4"></telerik:RadComboBoxItem>
                                    </Items>
                                </telerik:RadComboBox>
                                <telerik:RadComboBox ID="ddRem5Type" runat="server" CssClass="cmbGen" Width="90"
                                    MarkFirstMatch="True" HighlightTemplatedItems="True" ExpandAnimation-Duration="1000"
                                    CollapseAnimation-Duration="1000" CollapseAnimation-Type="OutQuart" NoWrap="True"
                                    EnableTheming="True" Font-Italic="False" Skin="Default">
                                    <Items>
                                        <telerik:RadComboBoxItem Selected="true" Text="Hours" Value="H"></telerik:RadComboBoxItem>
                                        <telerik:RadComboBoxItem Text="Days" Value="D"></telerik:RadComboBoxItem>
                                        <telerik:RadComboBoxItem Text="Weeks" Value="W"></telerik:RadComboBoxItem>
                                    </Items>
                                </telerik:RadComboBox>
                                Before<asp:HiddenField ID="hRem5" runat="server" Value="0" />
                            </td>
                        </tr>
                        <tr>
                            <td height="30" style="padding-left: 5px">
                                <asp:HyperLink ID="lbtnRemPreview" CssClass="lnkBtn2" data-typ="REP" runat="server"
                                    Text="Preview"></asp:HyperLink>
                                <asp:Literal ID="ltrSepR1" runat="server" Text="&nbsp;|&nbsp;"></asp:Literal>
                                <asp:HyperLink ID="lbtnRemEdit" CssClass="lnkBtn1" data-typ="REE" runat="server"
                                    Text="Edit"></asp:HyperLink>
                                <asp:Literal ID="ltrSepR2" runat="server" Text="&nbsp;|&nbsp;"></asp:Literal>
                                <asp:HyperLink ID="lbtnRemReviewSend" CssClass="lnkBtn1" data-typ="RER" runat="server"
                                    Text="Send for Review"></asp:HyperLink>
                                <asp:HiddenField ID="hReminderEmailContentID" runat="server" />
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>
    <table width="100%" align="center">
        <tr>
            <td>
                <div class="Steps" style="margin-bottom: 10px">
                    <table>
                        <tr>
                            <td style="padding-bottom: 5px;">
                                <asp:Label ID="lblAttendFollowup" runat="server" Font-Bold="True" Text="Follow-Up Email to Attendees" />
                            </td>
                        </tr>
                        <tr>
                            <td style="padding-top: 6px; padding-left: 5px;" height="24">
                                <asp:CheckBox ID="chkFollowAttendee" runat="server" CssClass="FLeft chk-place" />&nbsp;
                                <telerik:RadComboBox ID="ddFollowAttendValue" runat="server" CssClass="cmbGen" Width="40"
                                    MarkFirstMatch="True" HighlightTemplatedItems="True" ExpandAnimation-Duration="1000"
                                    CollapseAnimation-Duration="1000" CollapseAnimation-Type="OutQuart" NoWrap="True"
                                    EnableTheming="True" Font-Italic="False" Skin="Default">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="1" Value="1" Selected="True"></telerik:RadComboBoxItem>
                                        <telerik:RadComboBoxItem Text="2" Value="2"></telerik:RadComboBoxItem>
                                        <telerik:RadComboBoxItem Text="3" Value="3"></telerik:RadComboBoxItem>
                                        <telerik:RadComboBoxItem Text="4" Value="4"></telerik:RadComboBoxItem>
                                    </Items>
                                </telerik:RadComboBox>
                                <telerik:RadComboBox ID="ddFollowAttendType" runat="server" CssClass="cmbGen" Width="90"
                                    MarkFirstMatch="True" HighlightTemplatedItems="True" ExpandAnimation-Duration="1000"
                                    CollapseAnimation-Duration="1000" CollapseAnimation-Type="OutQuart" NoWrap="True"
                                    EnableTheming="True" Font-Italic="False" Skin="Default">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Hours" Value="H" Selected="True"></telerik:RadComboBoxItem>
                                        <telerik:RadComboBoxItem Text="Days" Value="D"></telerik:RadComboBoxItem>
                                        <telerik:RadComboBoxItem Text="Weeks" Value="W"></telerik:RadComboBoxItem>
                                    </Items>
                                </telerik:RadComboBox>
                                After<asp:HiddenField ID="hFollowAttendee" runat="server" Value="0" />
                            </td>
                        </tr>
                        <tr>
                            <td height="30" style="padding-left: 5px">
                                <asp:HyperLink ID="lbtnFollowAttendPreview" CssClass="lnkBtn2" data-typ="FAP" runat="server"
                                    Text="Preview"></asp:HyperLink>
                                <asp:Literal ID="ltrSepF1" runat="server" Text="&nbsp;|&nbsp;"></asp:Literal>
                                <asp:HyperLink ID="lbtnFollowAttendEdit" CssClass="lnkBtn1" data-typ="FAE" runat="server"
                                    Text="Edit"></asp:HyperLink>
                                <asp:Literal ID="ltrSepF2" runat="server" Text="&nbsp;|&nbsp;"></asp:Literal>
                                <asp:HyperLink ID="lbtnFollowAttendReview" CssClass="lnkBtn1" data-typ="FAR" runat="server"
                                    Text="Send for Review"></asp:HyperLink>
                                <asp:HiddenField ID="hFollowupAEmailContentID" runat="server" />
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>
    <table width="100%" align="center">
        <tr>
            <td>
                <div class="Steps" style="margin-bottom: 10px">
                    <table>
                        <tr>
                            <td style="padding-bottom: 5px;">
                                <asp:Label ID="lblUnAttendFollowup" runat="server" Font-Bold="True" Text="Follow up Email to Registrants who Missed the Live Webinar" />
                            </td>
                        </tr>
                        <tr>
                            <td style="padding-top: 6px; padding-left: 5px;" height="24">
                                <asp:CheckBox ID="chkFollowNonAttendee" runat="server" CssClass="FLeft chk-place" />&nbsp;
                                <telerik:RadComboBox ID="ddFollowNonAttendValue" runat="server" CssClass="cmbGen"
                                    Width="40" MarkFirstMatch="True" HighlightTemplatedItems="True" ExpandAnimation-Duration="1000"
                                    CollapseAnimation-Duration="1000" CollapseAnimation-Type="OutQuart" NoWrap="True"
                                    EnableTheming="True" Font-Italic="False" Skin="Default">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="1" Value="1" Selected="True"></telerik:RadComboBoxItem>
                                        <telerik:RadComboBoxItem Text="2" Value="2"></telerik:RadComboBoxItem>
                                        <telerik:RadComboBoxItem Text="3" Value="3"></telerik:RadComboBoxItem>
                                        <telerik:RadComboBoxItem Text="4" Value="4"></telerik:RadComboBoxItem>
                                    </Items>
                                </telerik:RadComboBox>
                                <telerik:RadComboBox ID="ddFollowNonAttendType" runat="server" CssClass="cmbGen"
                                    Width="90" MarkFirstMatch="True" HighlightTemplatedItems="True" ExpandAnimation-Duration="1000"
                                    CollapseAnimation-Duration="1000" CollapseAnimation-Type="OutQuart" NoWrap="True"
                                    EnableTheming="True" Font-Italic="False" Skin="Default">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Hours" Value="H" Selected="True"></telerik:RadComboBoxItem>
                                        <telerik:RadComboBoxItem Text="Days" Value="D"></telerik:RadComboBoxItem>
                                        <telerik:RadComboBoxItem Text="Weeks" Value="W"></telerik:RadComboBoxItem>
                                    </Items>
                                </telerik:RadComboBox>
                                After<asp:HiddenField ID="hFollowNonAttendee" runat="server" Value="0" />
                            </td>
                        </tr>
                        <tr>
                            <td height="30" style="padding-left: 5px">
                                <asp:HyperLink ID="lbtnUnFollowAttendPreview" CssClass="lnkBtn2" data-typ="FUP" runat="server"
                                    Text="Preview"></asp:HyperLink>
                                <asp:Literal ID="ltrSepU1" runat="server" Text="&nbsp;|&nbsp;"></asp:Literal>
                                <asp:HyperLink ID="lbtnUnFollowAttendEdit" CssClass="lnkBtn1" data-typ="FUE" runat="server"
                                    Text="Edit"></asp:HyperLink>
                                <asp:Literal ID="ltrSepU2" runat="server" Text="&nbsp;|&nbsp;"></asp:Literal>
                                <asp:HyperLink ID="lbtnUnFollowAttendReview" CssClass="lnkBtn1" data-typ="FUR" runat="server"
                                    Text="Send for Review"></asp:HyperLink>
                                <asp:HiddenField ID="hFollowupNAEmailContentID" runat="server" />
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>
    <table width="100%" align="center">
        <tr>
            <td>
                <div class="Steps" style="margin-bottom: 10px">
                    <table>
                        <tr>
                            <td style="padding-bottom: 5px;">
                                <asp:Label ID="lblRegistrantUpdate" runat="server" Font-Bold="True" Text="Email Registrant Updates" />
                                <span id="spPre1" runat="server" visible="false"><span id="spP1" class="spRU">[p]</span></span>
                            </td>
                        </tr>
                        <tr>
                            <td style="padding-top: 6px; padding-left: 5px;" height="24">
                                <asp:CheckBox ID="chkEmailRegularUpdate" runat="server" CssClass="FLeft" />&nbsp;
                                Email Registrant Updates Every
                            </td>
                            <td width="110px">
                                &nbsp;
                                <telerik:RadComboBox ID="ddEmailRegularDay" runat="server" CssClass="cmbGen" Width="90"
                                    MarkFirstMatch="True" HighlightTemplatedItems="True" ExpandAnimation-Duration="1000"
                                    CollapseAnimation-Duration="1000" CollapseAnimation-Type="OutQuart" NoWrap="True"
                                    EnableTheming="True" Font-Italic="False" Skin="Default">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="Day" Value="0"></telerik:RadComboBoxItem>
                                        <telerik:RadComboBoxItem Text="Monday" Value="2" Selected="True"></telerik:RadComboBoxItem>
                                        <telerik:RadComboBoxItem Text="Tuesday" Value="3"></telerik:RadComboBoxItem>
                                        <telerik:RadComboBoxItem Text="Wednesday" Value="4"></telerik:RadComboBoxItem>
                                        <telerik:RadComboBoxItem Text="Thursday" Value="5"></telerik:RadComboBoxItem>
                                        <telerik:RadComboBoxItem Text="Friday" Value="6"></telerik:RadComboBoxItem>
                                        <telerik:RadComboBoxItem Text="Saturday" Value="7"></telerik:RadComboBoxItem>
                                        <telerik:RadComboBoxItem Text="Sunday" Value="1"></telerik:RadComboBoxItem>
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                            <td>
                                at&nbsp;
                                <telerik:RadTimePicker ID="rtEmailRegularTime" runat="server" Width="90px" MarkFirstMatch="True"
                                    HighlightTemplatedItems="True" ExpandAnimation-Duration="1000" CollapseAnimation-Duration="1000"
                                    CollapseAnimation-Type="OutQuart" NoWrap="True" EnableTheming="True" Font-Italic="False"
                                    Skin="Default">
                                </telerik:RadTimePicker>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" height="35" style="padding-left: 5px">
                                <asp:CheckBox ID="chkEmailWhenRegistered" runat="server" CssClass="FLeft" />&nbsp;
                                Email when someone registers for this webinar
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" height="35" style="padding-left: 5px">
                                <asp:TextBox ID="txtEmailRegularToRedirect" runat="server" CssClass="textbox_EB ipdd-place1"
                                    Width="410"></asp:TextBox>
                                <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender19" runat="server" TargetControlID="txtEmailRegularToRedirect"
                                    WatermarkText="Separate multiple addresses with semi-colon(;)" WatermarkCssClass="watermarked_EBSearch" />
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>
    <script src="/qtip2/qtipCommonfn.js" type="text/javascript" charset="utf-8"></script>
    <script type="text/javascript">
        //VERY IMPORTANT NOTES: When using css defintiton of an UI element for jquery live click function. The routine is called for every
        //element in the page thought the actual click happen in one of the element, so the css based click is changed to ID base click
        var hWebID = document.getElementById('<%=hWebinarID.ClientID %>');
    </script>
    <script src="/qtip2/qtip_email_click.js" type="text/javascript" charset="utf-8"></script>
</div>

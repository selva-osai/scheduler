<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/masterWithNav.Master"
    AutoEventWireup="true" CodeBehind="emailContent.aspx.cs" Inherits="EBird.Web.App.Admin.emailContent" %>

<%@ Register Src="~/UserControls/schLPart.ascx" TagName="schLPart" TagPrefix="uc1" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="MainCont">
        <div class="Temp1">
            <uc1:schLPart ID="schLPart1" runat="server" />
            <div class="RPart">
                <div class="TopBg">
                </div>
                <div class="widgets">
                    <table width="660">
                        <tr>
                            <td>
                                <asp:HiddenField ID="hReqType" runat="server" />
                                <div class="regOutline">
                                    <table width="100%">
                                        <tr>
                                            <td colspan="2">
                                                <telerik:RadComboBox ID="rmbEmailType" runat="server" Width="180" AutoPostBack="True"
                                                    CausesValidation="False" Visible="true" OnSelectedIndexChanged="rmbEmailType_SelectedIndexChanged"
                                                    MarkFirstMatch="True" HighlightTemplatedItems="True" ExpandAnimation-Duration="1000"
                                                    CollapseAnimation-Duration="1000" CollapseAnimation-Type="OutQuart" NoWrap="True"
                                                    EnableTheming="True" Font-Italic="False" Skin="Default" EmptyMessage="Select email type">
                                                    <Items>
                                                        <telerik:RadComboBoxItem runat="server" Text="Webinar Invitation" Value="Webinar Invitation"
                                                            CssClass="ddStyle1" />
                                                        <telerik:RadComboBoxItem runat="server" Text="Registrant Reminder Email" Value="Registrant Reminder Email"
                                                            CssClass="ddStyle1" />
                                                        <telerik:RadComboBoxItem runat="server" Text="Confirmation Email" Value="Confirmation Email"
                                                            CssClass="ddStyle1" />
                                                        <telerik:RadComboBoxItem runat="server" Text="Attendee Followup" Value="Attendee Followup"
                                                            CssClass="ddStyle1" />
                                                        <telerik:RadComboBoxItem runat="server" Text="Non-Attendee Followup" Value="Non-Attendee Followup"
                                                            CssClass="ddStyle1" />
                                                        <telerik:RadComboBoxItem runat="server" Text="Password Changed" Value="Password Changed"
                                                            CssClass="ddStyle1" />
                                                        <telerik:RadComboBoxItem runat="server" Text="Webinar Cancellation" Value="Webinar Cancellation"
                                                            CssClass="ddStyle1" />
                                                        <telerik:RadComboBoxItem runat="server" Text="Webinar URLs" Value="Webinar URLs"
                                                            CssClass="ddStyle1" />
                                                        <telerik:RadComboBoxItem runat="server" Text="Refer a Collegue" Value="Refer a Collegue"
                                                            CssClass="ddStyle1" />
                                                        <telerik:RadComboBoxItem runat="server" Text="New User Account" Value="New User Account"
                                                            CssClass="ddStyle1" />
                                                        <telerik:RadComboBoxItem runat="server" Text="Campaign Tracking" Value="Campaign Tracking"
                                                            CssClass="ddStyle1" />
                                                        <telerik:RadComboBoxItem runat="server" Text="Connect Your Registration" Value="Connect Your Registration"
                                                            CssClass="ddStyle1" />
                                                    </Items>
                                                </telerik:RadComboBox>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div class="regOutline">
                                    <table border="0" width="100%" align="center">
                                        <tr>
                                            <td>
                                                <img src="/images/blank.gif" height="5" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                               <asp:TextBox runat="server" ID="txtsubject" CssClass="textbox_EB textbox_EB6"></asp:TextBox>
                                                <asp:TextBoxWatermarkExtender ID="txtWatermark" runat="server" TargetControlID="txtsubject"
                                                    WatermarkText="Subject" WatermarkCssClass="watermarked_EB6" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Email Content
                                            </td>
                                            <td align="right">
                                                Valid Meta tags&nbsp;
                                                <telerik:RadComboBox ID="rcmbMetaTag" runat="server" Width="180" CausesValidation="False"
                                                    Visible="true" OnSelectedIndexChanged="rmbEmailType_SelectedIndexChanged" MarkFirstMatch="True"
                                                    HighlightTemplatedItems="True" ExpandAnimation-Duration="1000" CollapseAnimation-Duration="1000"
                                                    CollapseAnimation-Type="OutQuart" NoWrap="True" EnableTheming="True" Font-Italic="False"
                                                    Skin="Default" EmptyMessage="Select email type">
                                                </telerik:RadComboBox>
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" colspan="2">
                                                <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
                                                <div id="dvRTBio">
                                                    <telerik:RadEditor runat="server" ID="redtRemEmail" ToolbarMode="Default" Height="370px"
                                                        BorderStyle="None" CssClass="~/Styles/RTEditor.css" Width="620" ToolsWidth="650"
                                                        EnableResize="False" EditModes="Design">
                                                        <CssFiles>
                                                            <telerik:EditorCssFile Value="~/Styles/RTEditor.css" />
                                                        </CssFiles>
                                                        <Tools>
                                                            <telerik:EditorToolGroup>
                                                                <telerik:EditorTool Name="Copy" />
                                                                <telerik:EditorTool Name="Cut" />
                                                                <telerik:EditorTool Name="Paste" />
                                                                <telerik:EditorSeparator />
                                                                <telerik:EditorTool Name="DecreaseSize" />
                                                                <telerik:EditorTool Name="IncreaseSize" />
                                                                <telerik:EditorTool Name="FindAndReplace" />
                                                                <telerik:EditorTool Name="Font" />
                                                                <telerik:EditorSeparator />
                                                                <telerik:EditorTool Name="BackColor" />
                                                                <telerik:EditorTool Name="ForeColor" />
                                                                <telerik:EditorSeparator />
                                                                <telerik:EditorTool Name="Bold" />
                                                                <telerik:EditorTool Name="Italic" />
                                                                <telerik:EditorTool Name="Underline" />
                                                                <telerik:EditorTool Name="StrikeThrough" />
                                                                <telerik:EditorSeparator />
                                                                <telerik:EditorTool Name="JustifyLeft" />
                                                                <telerik:EditorTool Name="JustifyCenter" />
                                                                <telerik:EditorTool Name="JustifyRight" />
                                                                <telerik:EditorTool Name="JustifyFull" />
                                                                <telerik:EditorSeparator />
                                                                <telerik:EditorTool Name="Indent" />
                                                                <telerik:EditorTool Name="Outdent" />
                                                                <telerik:EditorSeparator />
                                                                <telerik:EditorTool Name="InsertHorizontalRule" />
                                                                <telerik:EditorTool Name="InsertUnorderedList" />
                                                                <telerik:EditorTool Name="InsertOrderedList" />
                                                                <telerik:EditorTool Name="InsertLink" />
                                                            </telerik:EditorToolGroup>
                                                        </Tools>
                                                    </telerik:RadEditor>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td style="padding-top: 8px;">
                                <asp:Button runat="server" ID="btnSave" Text="Save" CssClass="SubBtn" Visible="true"
                                    OnClick="btnSave_Click" />&nbsp;<asp:Label ID="lblResult" runat="server" ForeColor="Green"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="BottBg">
                </div>
                <img src="../Images/blank.gif" height="10" />
                <div class="Clr">
                </div>
            </div>
            <div class="Clr">
            </div>
        </div>
        <div class="Clr">
        </div>
    </div>
</asp:Content>

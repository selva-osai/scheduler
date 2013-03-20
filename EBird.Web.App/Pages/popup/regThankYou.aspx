<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/popupMaster.Master"
    AutoEventWireup="true" CodeBehind="regThankYou.aspx.cs" Inherits="EBird.Web.App.Pages.popup.regThankYou" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Label ID="lblpgCap2" runat="server" Font-Bold="True" Text=""></asp:Label>
    <asp:PlaceHolder ID="phEdit" runat="server" Visible="false">
        <table width="650">
            <tr>
                <td>
                    <img src="/images/blank.gif" height="5" />
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
                                    <td align="left">
                                        <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
                                        <div id="dvRTBio">
                                            <telerik:RadEditor runat="server" ID="redtThankContent" ToolbarMode="Default" Height="350px"
                                                BorderStyle="None" CssClass="~/Styles/RTEditor.css" Width="620" ToolsWidth="640"
                                                EnableResize="False" EditModes="Design">
                                                <CssFiles>
                                                    <telerik:EditorCssFile Value="~/Styles/RTEditor.css" />
                                                </CssFiles>
                                                <Content>&nbsp;</Content>
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
                                <tr>
                                    <td>
                                        <img src="/images/blank.gif" height="5" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
            <tr>
                <td>
                    <asp:Button runat="server" ID="btnSave" Text="Save" CssClass="SubBtn" Visible="true"
                        OnClick="btnSave_Click" />&nbsp;<asp:Button runat="server" ID="btnCancel" Text="Cancel"
                            CssClass="SubBtn" Visible="false" />
                </td>
            </tr>
        </table>
    </asp:PlaceHolder>
    <asp:PlaceHolder ID="phView" runat="server" Visible="false">
        <table width="650">
            <tr>
                <td>
                    <div class="regOutline">
                        <img src="/images/blank.gif" height="10" />
                        <div class="Clr">
                        </div>
                        <asp:Label ID="lblError1" runat="server" ForeColor="Red"></asp:Label>
                        <asp:Literal ID="ltrThankContent" runat="server"></asp:Literal>
                        <div class="Clr">
                        </div>
                    </div>
                </td>
            </tr>
            <tr>
                <td style="padding: 5px 2px 5px 3px">
                    <asp:TextBox ID="txtReviewerEmail" runat="server" CssClass="textbox_EB textbox_EB2"
                        Visible="false"></asp:TextBox>
                    <asp:TextBoxWatermarkExtender ID="txtWatermark" runat="server" TargetControlID="txtReviewerEmail"
                        WatermarkText="Separate multiple addresses with semi-colon(;)" WatermarkCssClass="watermarked_EBReview textbox_EB2" />
                    <br />
                    <asp:Button runat="server" ID="btnReview" Text="Send for Review" CssClass="SubBtn"
                        Visible="false" OnClick="btnReview_Click" />
                </td>
            </tr>
        </table>
    </asp:PlaceHolder>
    <asp:HiddenField ID="hWebinarID" runat="server" Value="0" />
    <asp:HiddenField ID="hReqType" runat="server" />
    <script type="text/javascript">
        function closeMe() {
            $.modal.close();
        }
    </script>
</asp:Content>

<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UC_DeleteWebinar.ascx.cs" Inherits="EBird.Web.App.Pages.popup.UC_DeleteWebinar" %>
  <table width="100%" align="center">
        <tr>
            <td>
                <div class="regOutline">
                    <asp:HiddenField ID="hWebinarID" runat="server" />
                    <asp:Literal ID="ltrStatus" runat="server" Visible="false"></asp:Literal>
                    <asp:Label ID="lblDelInstruction" Text="This Webinar will
                                    be cancelled and a notification will be sent to all participants."
                        runat="server"></asp:Label>
                    <br />
                    <br />
                    <asp:HyperLink ID="hlnkDelPreviewEmail" runat="server" Text="Preview notification message"
                        Font-Underline="True" CssClass="hyperlink_EB" Visible="false"></asp:HyperLink>
                    <br />
                    <br />
                    <asp:Button ID="btnDelWebinar" OnClick="btnDelWebinar_Click" runat="server" Text="Cancel Webinar"
                        CssClass="SubBtn" />
                    <asp:Button ID="btnDelCancel" CssClass="SubBtn" OnClientClick="javascript:parent.frames[0].document.Refresh();" runat="server"
                        Visible="true" Text="Do Not Delete" CausesValidation="False" CommandName="Cancel" />
                </div>
            </td>
        </tr>
    </table>
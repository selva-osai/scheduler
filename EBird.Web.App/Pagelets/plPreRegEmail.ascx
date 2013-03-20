<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="plPreRegEmail.ascx.cs"
    Inherits="EBird.Web.App.Pagelets.plPreRegEmail" %>
<div class="Pre-Reg-Email" id="dvPreEmail" runat="server">
    <asp:ValidationSummary ValidationGroup="grp1" ID="xevs_Employees" runat="server"
        HeaderText="" ShowSummary="False" DisplayMode="BulletList" ShowMessageBox="true"
        ForeColor="#FF0000" />
    <div class="PRE-Titile">
        Pre-registered for this webinar? Login here
    </div>
    <asp:Label ID="lblPreEmail" runat="server" Text="Email"></asp:Label>
    <asp:TextBox ID="txtPreEmail" runat="server" CssClass="textbox_EB"></asp:TextBox>&nbsp;<asp:Button
        ID="btnPreLogin" runat="server" CssClass="Submit-btn1" Text="Login" OnClick="btnPreLogin_Click"
        ValidationGroup="grp1" />
    <asp:Button ID="Predummy1" runat="server" CssClass="Submit-btn1" Visible="false" Text="Login"  />
    <asp:RequiredFieldValidator ID="fv3" ErrorMessage="* Please Enter Email ID" Display="None" ForeColor="Red"
        runat="server" ControlToValidate="txtPreEmail" Enabled="true" ValidationGroup="grp1" />
    <asp:RegularExpressionValidator ID="RegularExpressionValidator" runat="server" ControlToValidate="txtPreEmail"
        Display="None" ForeColor="Red" ErrorMessage="Pre-registered email is not correct format, it must be in the form someone@domain.com. Check leading and trailing space(s)."
        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="grp1"></asp:RegularExpressionValidator>
    <asp:HiddenField ID="hWebinarID" runat="server" />
    <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
    <asp:HiddenField ID="hPreview" runat="server" Value="0" />
</div>

<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/mainMaster.Master"
    AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="EBird.Web.App.Pages.login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%">
        <tr>
            <td width="70%">
                &nbsp;
            </td>
            <td width="30%" valign="top">
                <table width="100%" cellpadding="5">
                    <tr>
                        <td height="50">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <h2 style="margin: 0; padding: 0; font-size: 114%; color: #333;">
                                Sign in to Snap Session</h2>
                            <br />
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 55px">
                            Snap Session ID<br />
                            <asp:TextBox ID="txtUserName" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 70px">
                            Password<br />
                            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 41px">
                            <asp:LinkButton CssClass="SubBtn" ID="btnLogin" runat="server" Text="Sign In" OnClick="btnLogin_Click"></asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 28px">
                            &nbsp;<asp:Label ID="lblMsg" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td height="200">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>

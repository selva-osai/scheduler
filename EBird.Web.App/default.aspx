<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="EBird.Web.App._default1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>SnapSession - Webinars in a Snap!</title>
    <!--[if IE 7]><link href="/Styles/Fiat/style-ie7.css" rel="stylesheet" type="text/css" /><![endif]-->
    <!--[if IE 8]><link href="/Styles/Fiat/style-ie8.css" rel="stylesheet" type="text/css" /><![endif]-->
    <script src="../Scripts/jquery-1.4.4.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.password-strength.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            var myPlugin = $("[id$='txtNewPassword']").password_strength();
        });
    </script>
    <style type="text/css">
        .SubBtn, a.SubBtn
        {
            background: url(/Images/LinkBtnBg.gif) repeat-x left top;
            height: 22px;
            line-height: 18px;
            -moz-border-radius: 3px;
            border-radius: 3px;
            padding: 0 10px;
            border: solid 1px #d9dcdc;
            font-size: 11px;
        }
        a.SubBtn
        {
            width: 80px;
            padding: 5px 25px;
            color: #000;
            text-decoration: none;
        }
        a.SubBtn:hover
        {
            color: #c80108;
        }
        .SubBtn:hover
        {
            color: #c80108;
        }
        
        ul.box
        {
            position: relative;
            z-index: 1; /* prevent shadows falling behind containers with backgrounds */
            overflow: hidden;
            list-style: none;
            margin: 0;
            padding: 0;
        }
        
        
        ul.box li
        {
            position: relative;
            float: left;
            width: 320px;
            height: 70px;
            padding: 0;
            border: 1px solid #efefef;
            margin: 0px 30px 30px 0px;
            padding: 10px 0px 0px 0px;
            vertical-align: middle;
            background: #fff;
            -webkit-box-shadow: 0 1px 4px rgba(0, 0, 0, 0.27), 0 0 40px rgba(0, 0, 0, 0.06) inset;
            -moz-box-shadow: 0 1px 4px rgba(0, 0, 0, 0.27), 0 0 40px rgba(0, 0, 0, 0.06) inset;
            box-shadow: 0 1px 4px rgba(0, 0, 0, 0.27), 0 0 40px rgba(0, 0, 0, 0.06) inset;
        }
        
        
        ul.box li:before, ul.box li:after
        {
            content: '';
            z-index: -1;
            position: absolute;
            left: 10px;
            bottom: 10px;
            width: 70%;
            max-width: 300px; /* avoid rotation causing ugly appearance at large container widths */
            max-height: 100px;
            height: 55%;
            -webkit-box-shadow: 0 8px 16px rgba(0, 0, 0, 0.3);
            -moz-box-shadow: 0 8px 16px rgba(0, 0, 0, 0.3);
            box-shadow: 0 8px 16px rgba(0, 0, 0, 0.3);
            -webkit-transform: skew(-15deg) rotate(-6deg);
            -moz-transform: skew(-15deg) rotate(-6deg);
            -ms-transform: skew(-15deg) rotate(-6deg);
            -o-transform: skew(-15deg) rotate(-6deg);
            transform: skew(-15deg) rotate(-6deg);
        }
        
        
        ul.box li:after
        {
            left: auto;
            right: 10px;
            -webkit-transform: skew(15deg) rotate(6deg);
            -moz-transform: skew(15deg) rotate(6deg);
            -ms-transform: skew(15deg) rotate(6deg);
            -o-transform: skew(15deg) rotate(6deg);
            transform: skew(15deg) rotate(6deg);
        }
        
        .textbox_EB
        {
            border: solid 1px #9e9e9e;
            padding-left: 3px;
            font-size: 11px;
            font-family: verdana;
            border-radius: 3px;
            margin-bottom: 6px;
            height: 20px;
            width:235px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager2" runat="server" />
    <asp:ValidationSummary ID="xevs_Employees" runat="server" HeaderText="" ShowSummary="False"
        ShowMessageBox="True" ForeColor="#FF0000" />
    <div style="text-align: center;">
        <div style="width: 330px; margin-left: auto; margin-right: auto;">
            <br />
            <br />
            <br />
            <br />
            <asp:PlaceHolder ID="InvalidPlaceholder" runat="server" Visible="False">
                <asp:Label ID="lblError1" runat="server" Text="Your login attempt has failed. The Email or Password may be incorrect. Please contact the Snap Session administrator at your company for help."
                    ForeColor="Red" Visible="True" Font-Size="Small"></asp:Label>
                <br />
                <br />
            </asp:PlaceHolder>
            <ul class="box">
                <li>
                    <img src="/Images/SSlogo.png" alt="" /></li>
            </ul>
            <asp:PlaceHolder ID="phLogin" runat="server" Visible="true">
                <!-- Email -->
                <asp:TextBox ID="txtEmailID" runat="server" TabIndex="1"  CssClass="textbox_EB"></asp:TextBox>
                <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="txtEmailID"
                    ErrorMessage="Please enter Email" ToolTip="Please enter Email" ForeColor="Red"
                    Display="None" Text="*"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator" runat="server" ControlToValidate="txtEmailID"
                    Display="None" ForeColor="Red" ErrorMessage="Email must be in the form someone@domain.com. Check leading and trailing space(s)."
                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator><br />
                <asp:TextBoxWatermarkExtender ID="txtWatermark" runat="server" TargetControlID="txtEmailID" 
                    WatermarkText="Email ..." WatermarkCssClass="textbox_EB" />
                <!-- Password -->
                <br />
                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" TabIndex="2" Style="margin-left: 0px"
                     CssClass="textbox_EB"></asp:TextBox>
                <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="txtPassword"
                    ErrorMessage="Please enter Password" ToolTip="Please enter Password" ForeColor="Red"
                    Display="None" Text="*"></asp:RequiredFieldValidator>
                <br />
                <br />
                <table border="0" width="100%">
                    <tr>
                        <td>
                            <asp:Button ID="btnLogin"
                                CssClass="SubBtn" CausesValidation="True" runat="server" Text="Login" TabIndex="3"
                                OnClick="btnLogin_Click" />
                        </td>
                    </tr>
                </table>
            </asp:PlaceHolder>
        </div>
        <div style="width: 330px; margin-left: auto; margin-right: auto; line-height: 24px;">
            <asp:PlaceHolder ID="phChangePassword" runat="server" Visible="false">
                <table width="100%">
                    <tr>
                        <td>
                            <asp:Label ID="lblChangePassIntruct" runat="server" Text="You need to change your password"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Current Password<br />
                            <asp:TextBox ID="txtCurrentPassword" runat="server" TextMode="Password" CssClass="textbox_EB"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            New Password <span style="color: #1589ff!important; text-decoration: none!important; float: none !important; font-size:3; cursor: pointer;" id="spPP">[Password Policy]</span><br />
                            <asp:TextBox ID="txtNewPassword" runat="server" TextMode="Password" CssClass="textbox_EB"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Confirm Password<br />
                            <asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password" CssClass="textbox_EB"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="btnChangePassword" CssClass="SubBtn" CausesValidation="True" runat="server"
                                Text="Change Password" OnClick="btnChangePassword_Click" />
                        </td>
                    </tr>
                </table>
            </asp:PlaceHolder>
        </div>
    </div>
    </form>
    <!-- Start: Password Policy display block -->
    <div id="dvPrePP" style="display: none; height: 220px">
        <div id='modalContainer'><div class='modalClose'><img src='/images/x.png' /></div>
        <table style="width: 98%; text-align: center; margin: 2px 2px 2px 2px; padding: 3px 3px 3px 3px;
            border: 1px solid #c0c0c0;">
            <tr>
                <td colspan="2">
                    <b>Password Policy</b>
                </td>
            </tr>
            <tr bgcolor="#c0c0c0">
                <td align="left">
                    Password duration
                </td>
                <td align="left">
                    180 days
                </td>
            </tr>
            <tr>
                <td align="left">
                    Password minimum length
                </td>
                <td align="left">
                    12
                </td>
            </tr>
            <tr bgcolor="#c0c0c0">
                <td align="left">
                    Password maximum length
                </td>
                <td align="left">
                    25
                </td>
            </tr>
            <tr>
                <td align="left">
                    Required digits
                </td>
                <td align="left">
                    1
                </td>
            </tr>
            <tr bgcolor="#c0c0c0">
                <td align="left">
                    Required upper-case letters
                </td>
                <td align="left">
                    1
                </td>
            </tr>
            <tr>
                <td align="left">
                    Required special characters
                </td>
                <td align="left">
                    1
                </td>
            </tr>
            <tr bgcolor="#c0c0c0">
                <td align="left">
                    Allowable special characters
                </td>
                <td align="left">
                    !@#\\$%*()_+^&}{:;?.
                </td>
            </tr>
        </table>
        </div>
    </div>
    <script type="text/javascript" src="/js/jquery-ui-1.8.18.custom.min.js"></script>
    <script type="text/javascript" src="/Scripts/jQuery_UI1.js"></script>
    <link id="Link3" href="/Styles/qtip.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/js/jq.qtip.js"></script>
    <script src="/qtip2/qtipPassPolicy.js" type="text/javascript" charset="utf-8"></script>
    <script type="text/javascript">
        $('#spPP').click(function () {
            var contentString = $('#dvPrePP');
            qtipPassPolicy('#spPP', contentString, 300, '.modalClose');
        });
    </script>
    <!-- End: Password Policy display block -->
</body>
</html>

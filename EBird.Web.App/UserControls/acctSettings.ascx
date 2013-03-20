<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="acctSettings.ascx.cs"
    Inherits="EBird.Web.App.UserControls.acctSettings" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<link href="/css/carousel1.css" rel="stylesheet" type="text/css" />
<script src="../Scripts/jquery-1.4.4.min.js" type="text/javascript"></script>
<script src="../Scripts/jquery.password-strength.js" type="text/javascript"></script>
<script type="text/javascript">
    $(document).ready(function () {
        var myPlugin = $("[id$='txtNewPassword']").password_strength();
    });
</script>
<style type="text/css">
    .RadComboBoxDropDown_Default .rcbHovered
    {
        background: #EBEBEB;
        color: #C80000;
    }
</style>
<div class="RPart">
    <div class="TopBg">
    </div>
    <div class="widgets">
        <!-- START: page content -->
        <table width="97%" align="center" style="min-height: 430px">
            <tr>
                <td align="right" colspan="2" style="padding-bottom: 10px;">
                    <asp:Literal ID="ltrBack" runat="server" Text="Return to <a href='Webinar' class='lnkBtn1'>My Webinar</a> to manage or start your webinars"></asp:Literal>
                </td>
            </tr>
            <tr style="float:left;">
                <td colspan="2">
                    <div class="FormCont Steps" style="width: 700px; margin-bottom: 10px">
                        <table width="100%">
                            <tr>
                                <td align="left" colspan="2" style="padding-bottom: 10px;">
                                    <asp:Label ID="lblCap1" runat="server" Font-Bold="True" Text="Manage Password" CssClass="frmHeading"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="padding-left: 5px; position:relative;">
                                    <span style="color: #1589ff!important; text-decoration: none!important; float: none !important; font-size:3; cursor: pointer;" id="spPP">Password Policy</span>
                                    <asp:Label ID="lblPassInstruct" runat="server" Text="Password must contain at least 8 characters, one lower case, one upper case, and one digit"
                                        CssClass="frmFields" Visible="false"></asp:Label>
                                    <br />
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td width="20%">
                                    &nbsp;<asp:Label ID="lblCurrPass" runat="server" Text="Current Password" CssClass="frmFields"></asp:Label>
                                </td>
                                <td width="80%">
                                    &nbsp;<asp:TextBox ID="txtCurrPassword" runat="server" Height="20" Width="120" TextMode="Password"
                                        CssClass="textbox_EB" MaxLength="50"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;<asp:Label ID="lblNewPassword" runat="server" Text="New Password" CssClass="frmFields"></asp:Label>
                                </td>
                                <td>
                                    &nbsp;<asp:TextBox ID="txtNewPassword" runat="server" Height="20" Width="120" TextMode="Password"
                                        CssClass="textbox_EB" MaxLength="50"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;<asp:Label ID="lblConfirmPassword" runat="server" Text="Re-Type Password" CssClass="frmFields"></asp:Label>
                                </td>
                                <td>
                                    &nbsp;<asp:TextBox ID="txtConfirmPassword" runat="server" Height="20" Width="120"
                                        TextMode="Password" CssClass="textbox_EB" MaxLength="50"></asp:TextBox>
                                </td>
                                <%--                    <ajaxtoolkit:passwordstrength id="PS" runat="server" targetcontrolid="password1text"
                displayposition="RightSide" strengthindicatortype="Text" preferredpasswordlength="10"
                prefixtext="Strength:" textcssclass="TextIndicator_TextBox1" minimumnumericcharacters="0"
                minimumsymbolcharacters="0" requiresupperandlowercasecharacters="false" textstrengthdescriptions="Very Poor;Weak;Average;Strong;Excellent"
                textstrengthdescriptionstyles="pwPoor;pwWeak;pwAverage;pwStrong;pwExcellent"
                calculationweightings="50;15;15;20" />--%>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
            <tr style="float:left;">
                <td style="padding-bottom:10px;">
                    <asp:Button ID="btnChangePassword" runat="server" Text="Change Password" CssClass="SubBtn" CausesValidation="False"
                        OnClick="btnChangePassword_Click" />&nbsp;<asp:Label ID="lblError1" runat="server" ForeColor="Red"></asp:Label>
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <div id="dvEmail" runat="server" class="FormCont Steps" style="width:700px; margin-bottom: 10px">
                        <table width="100%">
                            <tr>
                                <td align="left" colspan="2" style="padding-bottom: 10px;">
                                    <asp:Label ID="lblCap2" runat="server" Font-Bold="True" Text="Manage Email Settings"
                                        CssClass="frmHeading"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <div style="margin-left: 5px;">
                                        <asp:CheckBox ID="chkEmailUpdate" runat="server" Checked="true" CssClass="configCheckBox" />&nbsp;
                                        <asp:Label ID="lblEmailInstruct" runat="server" Text="Send weekly status updates for my webinars"
                                            CssClass="frmFields"></asp:Label>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <div style="margin-left: 5px">
                                        <asp:HyperLink ID="hlnkPreviewEmailStatus" runat="server" Text="Preview Status Update"
                                            CssClass="lnkBtn1"></asp:HyperLink>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <div id="dvTime" runat="server" class="FormCont Steps" style="width:700px; margin-bottom: 10px">
                        <table width="100%">
                            <tr>
                                <td align="left" colspan="2" style="padding-bottom: 10px;">
                                    <asp:Label ID="lblCap3" runat="server" Font-Bold="True" Text="Manage Time Zone Settings"
                                        CssClass="frmHeading"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td valign="middle" style="padding-left: 5px">
                                    <asp:Label ID="lblTimeZoneLabel" runat="server" Text="Time Zone" CssClass="frmFields"></asp:Label>
                                </td>
                                <td valign="middle">
                                    <telerik:RadComboBox ID="rcmbTimeZone" runat="server" CssClass="cmbGen" Width="435"
                                        MarkFirstMatch="True" HighlightTemplatedItems="True" ExpandAnimation-Duration="1000"
                                        CollapseAnimation-Duration="1000" CollapseAnimation-Type="OutQuart" NoWrap="True"
                                        EnableTheming="True" Font-Italic="False" Skin="Default">
                                        <Items>
                                            <telerik:RadComboBoxItem runat="server" Value="1" Text="(GMT-12:00) International dateline, west" />
                                            <telerik:RadComboBoxItem runat="server" Value="2" Text="(GMT-11:00) Midway Islands, Samoan Islands" />
                                            <telerik:RadComboBoxItem runat="server" Value="3" Text="(GMT-10:00) Hawaii" />
                                            <telerik:RadComboBoxItem runat="server" Value="4" Text="(GMT-09:00) Alaska" />
                                            <telerik:RadComboBoxItem runat="server" Value="5" Selected="True" Text="(GMT-08:00) Pacific Time (USA or Canada); Tijuana" />
                                            <telerik:RadComboBoxItem runat="server" Value="6" Text="(GMT-07:00) Mountain Time (USA or Canada)" />
                                            <telerik:RadComboBoxItem runat="server" Value="7" Text="(GMT-06:00) Central time (USA or Canada)" />
                                            <telerik:RadComboBoxItem runat="server" Value="8" Text="(GMT-05:00) Eastern time (USA or Canada)" />
                                            <telerik:RadComboBoxItem runat="server" Value="9" Text="(GMT-04:00) Atlantic Time (Canada)" />
                                            <telerik:RadComboBoxItem runat="server" Value="10" Text="(GMT-03:30) Newfoundland" />
                                            <telerik:RadComboBoxItem runat="server" Value="11" Text="(GMT-03:00) Brasilia" />
                                            <telerik:RadComboBoxItem runat="server" Value="12" Text="(GMT-02:00) Mid-Atlantic" />
                                            <telerik:RadComboBoxItem runat="server" Value="13" Text="(GMT-01:00) Azorerne" />
                                            <telerik:RadComboBoxItem runat="server" Value="14" Text="(GMT) Greenwich Mean Time: Dublin, Edinburgh, Lissabon, London" />
                                            <telerik:RadComboBoxItem runat="server" Value="15" Text="(GMT+01:00) Amsterdam, Berlin, Bern, Rom, Stockholm, Wien" />
                                            <telerik:RadComboBoxItem runat="server" Value="16" Text="(GMT+02:00) Athen, Istanbul, Minsk" />
                                            <telerik:RadComboBoxItem runat="server" Value="17" Text="(GMT+03:00) Moscow, St. Petersburg, Volgograd" />
                                            <telerik:RadComboBoxItem runat="server" Value="18" Text="(GMT+03:30) Teheran" />
                                            <telerik:RadComboBoxItem runat="server" Value="19" Text="(GMT+04:00) Abu Dhabi, Muscat" />
                                            <telerik:RadComboBoxItem runat="server" Value="20" Text="(GMT+04:30) Kabul" />
                                            <telerik:RadComboBoxItem runat="server" Value="21" Text="(GMT+05:00) Islamabad, Karachi, Tasjkent" />
                                            <telerik:RadComboBoxItem runat="server" Value="22" Text="(GMT+05:30) Kolkata, Chennai, Mumbai, New Delhi" />
                                            <telerik:RadComboBoxItem runat="server" Value="23" Text="(GMT+05:45) Katmandu" />
                                            <telerik:RadComboBoxItem runat="server" Value="24" Text="(GMT+06:00) Astana, Dhaka" />
                                            <telerik:RadComboBoxItem runat="server" Value="25" Text="(GMT+06:30) Rangoon" />
                                            <telerik:RadComboBoxItem runat="server" Value="26" Text="(GMT+07:00) Bangkok, Hanoi, Djakarta" />
                                            <telerik:RadComboBoxItem runat="server" Value="27" Text="(GMT+08:00) Beijing, Chongjin, SAR Hongkong, Ürümqi" />
                                            <telerik:RadComboBoxItem runat="server" Value="28" Text="(GMT+09:00) Osaka, Sapporo, Tokyo" />
                                            <telerik:RadComboBoxItem runat="server" Value="29" Text="(GMT+09:30) Adelaide" />
                                            <telerik:RadComboBoxItem runat="server" Value="30" Text="(GMT+10:00) Canberra, Melbourne, Sydney" />
                                            <telerik:RadComboBoxItem runat="server" Value="31" Text="(GMT+11:00) Magadan, Solomon Islands, New Caledonien" />
                                            <telerik:RadComboBoxItem runat="server" Value="32" Text="(GMT+12:00) Fiji, Kamtjatka, Marshall Islands" />
                                            <telerik:RadComboBoxItem runat="server" Value="33" Text="(GMT+13:00) Nuku'alofa" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <br />
                                    <asp:CheckBox ID="chkDaylight" runat="server" CssClass="configCheckBox" />&nbsp;
                                    <asp:Label ID="lblTimeInstruct" runat="server" Text="Automatically observe Daylight Saving Time. (Does not apply to all time zones)"
                                        CssClass="frmFields"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Button ID="btnSave" runat="server" Text="Save Settings" CssClass="SubBtn" CausesValidation="False"
                        OnClick="btnSave_Click" />
                    &nbsp;<asp:Label ID="lblMsg" ForeColor="Red" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
        <!-- END: page content -->
        <script src="/qtip2/qtipCommonfn.js" type="text/javascript" charset="utf-8"></script>
        <script type="text/javascript">
            jQuery(function ($) {
                $("#ContentPlaceHolder1_acctSetting1_hlnkPreviewEmailStatus").live('click', function (e) {
                    var URL = "/Pages/Reports/popRpt.aspx";

                    qtipPopup("#ContentPlaceHolder1_acctSetting1_hlnkPreviewEmailStatus",
                    "<div id='modalContainer'><div class='modalClose'><img src='/images/x.png' /></div><iframe id='popRpt' src=" + URL + " height='520' width='720' scrolling='no' class='bgfill'></iframe></div>",
                    '#popRpt', 'body #ContentPlaceHolder1_hModalStatusFlg', 720, 520, '.modalClose', '');
                });
            });
        </script>


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
        jQuery(function ($) {
            $("#spPP").live('click', function (e) {
                var contentString = $('#dvPrePP');
                qtipPassPolicy('#spPP', contentString, 350, '.modalClose');
            });
        });
    </script>
    <!-- End: Password Policy display block -->

    </div>
    <div class="BottBg">
    </div>
    <img src="../Images/blank.gif" height="10" />
    <div class="Clr">
    </div>
</div>

<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="plRegForm.ascx.cs" Inherits="EBird.Web.App.Pagelets.plRegForm" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:PlaceHolder ID="phRegFormUser" runat="server">
    <style>
        * 	{ margin:0; padding:0;}
        #plRegForm1_dv1 ul, li
        {
            list-style-type: none;
        }
    </style>
    <table width="100%" align="center">
        <tr>
            <td>
                <asp:HiddenField ID="hWebinarID" runat="server" />
                <asp:HiddenField ID="hRegForm" runat="server" />
                <asp:HiddenField ID="hRegFormReq" runat="server" />
                <asp:HiddenField ID="hPreview" Value="0" runat="server" />
            </td>
        </tr>
        <tr>
            <td style="vertical-align: top !important;">
                <div id="dvRegForm" runat="server" class="Reg-Field">
                    <%--             <h2>
                        Register Now</h2>
                    --%>
                    <asp:ValidationSummary ID="xevs_Employees" runat="server" HeaderText="" ValidationGroup="grp2"
                        ShowSummary="False" DisplayMode="BulletList" ShowMessageBox="true" ForeColor="#FF0000" />
                    <div id="dvError" runat="server" visible="false">
                        <asp:Literal ID="ltrError" runat="server"></asp:Literal>
                    </div>
                    <div id="dv1" runat="server">
                        <ul class="fld1" runat="server" id="ulcol1">
                            <li visible="false" runat="server" id="fl1">
                                <asp:Label ID="lblF1" runat="server" Text="First Name" CssClass="Qstlabel1"></asp:Label>
                                &nbsp;<asp:Label ID="lblR1" Text="*" runat="server" Visible="false"></asp:Label></li>
                            <li visible="false" runat="server" id="fi1">
                                <asp:TextBox ID="InV1" runat="server" CssClass="textbox_EB"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="fv1" ErrorMessage="*" Display="None" ForeColor="Red"
                                    runat="server" ValidationGroup="grp2" ControlToValidate="InV1" Enabled="false" />
                            </li>
                            <li visible="false" runat="server" id="fl2">
                                <asp:Label ID="lblF2" runat="server" Text="Last Name" CssClass="Qstlabel1"></asp:Label>
                                &nbsp;<asp:Label ID="lblR2" Text="*" runat="server" Visible="false"></asp:Label></li>
                            <li visible="false" runat="server" id="fi2">
                                <asp:TextBox ID="InV2" runat="server" CssClass="textbox_EB"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="fv2" ErrorMessage="*" Display="None" ForeColor="Red"
                                    runat="server" ValidationGroup="grp2" ControlToValidate="InV2" Enabled="false" />
                            </li>
                            <li visible="false" runat="server" id="fl3">
                                <asp:Label ID="lblF3" runat="server" Text="Email" CssClass="Qstlabel1"></asp:Label>
                                &nbsp;<asp:Label ID="lblR3" Text="*" runat="server" Visible="false"></asp:Label></li>
                            <li visible="false" runat="server" id="fi3">
                                <asp:TextBox ID="InV3" runat="server" CssClass="textbox_EB"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="fv3" ErrorMessage="*" Display="None" ForeColor="Red"
                                    runat="server" ValidationGroup="grp2" ControlToValidate="InV3" Enabled="false" />
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator" runat="server" ControlToValidate="InV3"
                                    ValidationGroup="grp2" Display="None" ForeColor="Red" ErrorMessage="Email must be in the form someone@domain.com. Check leading and trailing space(s)."
                                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                            </li>
                            <li visible="false" runat="server" id="fl4">
                                <asp:Label ID="lblF4" runat="server" Text="Address" CssClass="Qstlabel1"></asp:Label>
                                &nbsp;<asp:Label ID="lblR4" Text="*" runat="server" Visible="false"></asp:Label></li>
                            <li visible="false" runat="server" id="fi4">
                                <asp:TextBox ID="InV4" runat="server" CssClass="textbox_EB"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="fv4" ErrorMessage="*" Display="None" ForeColor="Red"
                                    runat="server" ValidationGroup="grp2" ControlToValidate="InV4" Enabled="false" />
                            </li>
                            <li visible="false" runat="server" id="fl5">
                                <asp:Label ID="lblF5" runat="server" Text="City" CssClass="Qstlabel1"></asp:Label>
                                &nbsp;<asp:Label ID="lblR5" Text="*" runat="server" Visible="false"></asp:Label></li>
                            <li visible="false" runat="server" id="fi5">
                                <asp:TextBox ID="InV5" runat="server" CssClass="textbox_EB"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="fv5" ErrorMessage="*" Display="None" ForeColor="Red"
                                    runat="server" ValidationGroup="grp2" ControlToValidate="InV5" Enabled="false" />
                            </li>
                            <li visible="false" runat="server" id="fl6">
                                <asp:Label ID="lblF6" runat="server" Text="State/Province" CssClass="Qstlabel1"></asp:Label>
                                &nbsp;<asp:Label ID="lblR6" Text="*" runat="server" Visible="false"></asp:Label></li>
                            <li visible="false" runat="server" id="fi6">
                                <asp:TextBox ID="InV6" runat="server" CssClass="textbox_EB"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="fv6" ForeColor="Red" Display="None" ErrorMessage="*"
                                    runat="server" ValidationGroup="grp2" ControlToValidate="InV6" Enabled="false" />
                            </li>
                            <li visible="false" runat="server" id="fl7">
                                <asp:Label ID="lblF7" runat="server" Text="Zip/Postal Code" CssClass="Qstlabel1"></asp:Label>
                                &nbsp;<asp:Label ID="lblR7" Text="*" runat="server" Visible="false"></asp:Label></li>
                            <li visible="false" runat="server" id="fi7">
                                <asp:TextBox ID="InV7" runat="server" CssClass="textbox_EB"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="fv7" ForeColor="Red" Display="None" ErrorMessage="*"
                                    runat="server" ValidationGroup="grp2" ControlToValidate="InV7" Enabled="false" />
                            </li>
                            <li visible="false" runat="server" id="fl8">
                                <asp:Label ID="lblF8" runat="server" Text="Country" CssClass="Qstlabel1"></asp:Label>
                                &nbsp;<asp:Label ID="lblR8" Text="*" runat="server" Visible="false"></asp:Label></li>
                            <li visible="false" runat="server" id="fi8">
                                <asp:TextBox ID="InV8" runat="server" CssClass="textbox_EB"></asp:TextBox>
                                <telerik:RadComboBox ID="cmbInV8" runat="server" ShowDropDownOnTextboxClick="True"
                                    MarkFirstMatch="True" AllowCustomText="False" HighlightTemplatedItems="True"
                                    ExpandAnimation-Duration="300" CollapseAnimation-Duration="1000" CollapseAnimation-Type="OutQuart"
                                    NoWrap="True" Font-Names="Verdana" Font-Size="11px" EnableTheming="True" ForeColor="Black"
                                    Font-Italic="False" Skin="Default" Width="220" CssClass="rad-combo" Visible="false">
                                    <Items>
                                        <telerik:RadComboBoxItem runat="server" Value="1" Text="Afghanistan" />
                                        <telerik:RadComboBoxItem runat="server" Value="2" Text="Albania" />
                                        <telerik:RadComboBoxItem runat="server" Value="3" Text="Algeria" />
                                        <telerik:RadComboBoxItem runat="server" Value="4" Text="American Samoa" />
                                        <telerik:RadComboBoxItem runat="server" Value="5" Text="Andorra" />
                                        <telerik:RadComboBoxItem runat="server" Value="6" Text="Angola" />
                                        <telerik:RadComboBoxItem runat="server" Value="7" Text="Anguilla" />
                                        <telerik:RadComboBoxItem runat="server" Value="8" Text="Antarctica" />
                                        <telerik:RadComboBoxItem runat="server" Value="9" Text="Antigua And Barbuda" />
                                        <telerik:RadComboBoxItem runat="server" Value="10" Text="Argentina" />
                                        <telerik:RadComboBoxItem runat="server" Value="11" Text="Armenia" />
                                        <telerik:RadComboBoxItem runat="server" Value="12" Text="Aruba" />
                                        <telerik:RadComboBoxItem runat="server" Value="13" Text="Australia" />
                                        <telerik:RadComboBoxItem runat="server" Value="14" Text="Austria" />
                                        <telerik:RadComboBoxItem runat="server" Value="15" Text="Azerbaijan" />
                                        <telerik:RadComboBoxItem runat="server" Value="16" Text="Bahamas" />
                                        <telerik:RadComboBoxItem runat="server" Value="17" Text="Bahrain" />
                                        <telerik:RadComboBoxItem runat="server" Value="18" Text="Bangladesh" />
                                        <telerik:RadComboBoxItem runat="server" Value="19" Text="Barbados" />
                                        <telerik:RadComboBoxItem runat="server" Value="20" Text="Belarus" />
                                        <telerik:RadComboBoxItem runat="server" Value="21" Text="Belgium" />
                                        <telerik:RadComboBoxItem runat="server" Value="22" Text="Belize" />
                                        <telerik:RadComboBoxItem runat="server" Value="23" Text="Benin" />
                                        <telerik:RadComboBoxItem runat="server" Value="24" Text="Bermuda" />
                                        <telerik:RadComboBoxItem runat="server" Value="25" Text="Bhutan" />
                                        <telerik:RadComboBoxItem runat="server" Value="26" Text="Bolivia" />
                                        <telerik:RadComboBoxItem runat="server" Value="27" Text="Bosnia And Herzegowina" />
                                        <telerik:RadComboBoxItem runat="server" Value="28" Text="Botswana" />
                                        <telerik:RadComboBoxItem runat="server" Value="29" Text="Bouvet Island" />
                                        <telerik:RadComboBoxItem runat="server" Value="30" Text="Brazil" />
                                        <telerik:RadComboBoxItem runat="server" Value="31" Text="British Indian Ocean Territory" />
                                        <telerik:RadComboBoxItem runat="server" Value="32" Text="Brunei Darussalam" />
                                        <telerik:RadComboBoxItem runat="server" Value="33" Text="Bulgaria" />
                                        <telerik:RadComboBoxItem runat="server" Value="34" Text="Burkina Faso" />
                                        <telerik:RadComboBoxItem runat="server" Value="35" Text="Burundi" />
                                        <telerik:RadComboBoxItem runat="server" Value="36" Text="Cambodia" />
                                        <telerik:RadComboBoxItem runat="server" Value="37" Text="Cameroon" />
                                        <telerik:RadComboBoxItem runat="server" Value="38" Text="Canada" />
                                        <telerik:RadComboBoxItem runat="server" Value="39" Text="Cape Verde" />
                                        <telerik:RadComboBoxItem runat="server" Value="40" Text="Cayman Islands" />
                                        <telerik:RadComboBoxItem runat="server" Value="41" Text="Central African Republic" />
                                        <telerik:RadComboBoxItem runat="server" Value="42" Text="Chad" />
                                        <telerik:RadComboBoxItem runat="server" Value="43" Text="Chile" />
                                        <telerik:RadComboBoxItem runat="server" Value="44" Text="China" />
                                        <telerik:RadComboBoxItem runat="server" Value="45" Text="Christmas Island" />
                                        <telerik:RadComboBoxItem runat="server" Value="46" Text="Cocos (Keeling) Islands" />
                                        <telerik:RadComboBoxItem runat="server" Value="47" Text="Colombia" />
                                        <telerik:RadComboBoxItem runat="server" Value="48" Text="Comoros" />
                                        <telerik:RadComboBoxItem runat="server" Value="49" Text="Congo" />
                                        <telerik:RadComboBoxItem runat="server" Value="50" Text="Cook Islands" />
                                        <telerik:RadComboBoxItem runat="server" Value="51" Text="Costa Rica" />
                                        <telerik:RadComboBoxItem runat="server" Value="52" Text="Cote D'Ivoire" />
                                        <telerik:RadComboBoxItem runat="server" Value="53" Text="Croatia (Local Name: Hrvatska)" />
                                        <telerik:RadComboBoxItem runat="server" Value="54" Text="Cuba" />
                                        <telerik:RadComboBoxItem runat="server" Value="55" Text="Cyprus" />
                                        <telerik:RadComboBoxItem runat="server" Value="56" Text="Czech Republic" />
                                        <telerik:RadComboBoxItem runat="server" Value="57" Text="Denmark" />
                                        <telerik:RadComboBoxItem runat="server" Value="58" Text="Djibouti" />
                                        <telerik:RadComboBoxItem runat="server" Value="59" Text="Dominica" />
                                        <telerik:RadComboBoxItem runat="server" Value="60" Text="Dominican Republic" />
                                        <telerik:RadComboBoxItem runat="server" Value="61" Text="East Timor" />
                                        <telerik:RadComboBoxItem runat="server" Value="62" Text="Ecuador" />
                                        <telerik:RadComboBoxItem runat="server" Value="63" Text="Egypt" />
                                        <telerik:RadComboBoxItem runat="server" Value="64" Text="El Salvador" />
                                        <telerik:RadComboBoxItem runat="server" Value="65" Text="Equatorial Guinea" />
                                        <telerik:RadComboBoxItem runat="server" Value="66" Text="Eritrea" />
                                        <telerik:RadComboBoxItem runat="server" Value="67" Text="Estonia" />
                                        <telerik:RadComboBoxItem runat="server" Value="68" Text="Ethiopia" />
                                        <telerik:RadComboBoxItem runat="server" Value="69" Text="Falkland Islands (Malvinas)" />
                                        <telerik:RadComboBoxItem runat="server" Value="70" Text="Faroe Islands" />
                                        <telerik:RadComboBoxItem runat="server" Value="71" Text="Fiji" />
                                        <telerik:RadComboBoxItem runat="server" Value="72" Text="Finland" />
                                        <telerik:RadComboBoxItem runat="server" Value="73" Text="France" />
                                        <telerik:RadComboBoxItem runat="server" Value="74" Text="French Guiana" />
                                        <telerik:RadComboBoxItem runat="server" Value="75" Text="French Polynesia" />
                                        <telerik:RadComboBoxItem runat="server" Value="76" Text="French Southern Territories" />
                                        <telerik:RadComboBoxItem runat="server" Value="77" Text="Gabon" />
                                        <telerik:RadComboBoxItem runat="server" Value="78" Text="Gambia" />
                                        <telerik:RadComboBoxItem runat="server" Value="79" Text="Georgia" />
                                        <telerik:RadComboBoxItem runat="server" Value="80" Text="Germany" />
                                        <telerik:RadComboBoxItem runat="server" Value="81" Text="Ghana" />
                                        <telerik:RadComboBoxItem runat="server" Value="82" Text="Gibraltar" />
                                        <telerik:RadComboBoxItem runat="server" Value="83" Text="Greece" />
                                        <telerik:RadComboBoxItem runat="server" Value="84" Text="Greenland" />
                                        <telerik:RadComboBoxItem runat="server" Value="85" Text="Grenada" />
                                        <telerik:RadComboBoxItem runat="server" Value="86" Text="Guadeloupe" />
                                        <telerik:RadComboBoxItem runat="server" Value="87" Text="Guam" />
                                        <telerik:RadComboBoxItem runat="server" Value="88" Text="Guatemala" />
                                        <telerik:RadComboBoxItem runat="server" Value="89" Text="Guinea" />
                                        <telerik:RadComboBoxItem runat="server" Value="90" Text="Guinea-Bissau" />
                                        <telerik:RadComboBoxItem runat="server" Value="91" Text="Guyana" />
                                        <telerik:RadComboBoxItem runat="server" Value="92" Text="Haiti" />
                                        <telerik:RadComboBoxItem runat="server" Value="93" Text="Heard And Mc Donald Islands" />
                                        <telerik:RadComboBoxItem runat="server" Value="94" Text="Holy See (Vatican City State)" />
                                        <telerik:RadComboBoxItem runat="server" Value="95" Text="Honduras" />
                                        <telerik:RadComboBoxItem runat="server" Value="96" Text="Hong Kong" />
                                        <telerik:RadComboBoxItem runat="server" Value="97" Text="Hungary" />
                                        <telerik:RadComboBoxItem runat="server" Value="98" Text="Icel And" />
                                        <telerik:RadComboBoxItem runat="server" Value="99" Text="India" />
                                        <telerik:RadComboBoxItem runat="server" Value="100" Text="Indonesia" />
                                        <telerik:RadComboBoxItem runat="server" Value="101" Text="Iran (Islamic Republic Of)" />
                                        <telerik:RadComboBoxItem runat="server" Value="102" Text="Iraq" />
                                        <telerik:RadComboBoxItem runat="server" Value="103" Text="Ireland" />
                                        <telerik:RadComboBoxItem runat="server" Value="104" Text="Israel" />
                                        <telerik:RadComboBoxItem runat="server" Value="105" Text="Italy" />
                                        <telerik:RadComboBoxItem runat="server" Value="106" Text="Jamaica" />
                                        <telerik:RadComboBoxItem runat="server" Value="107" Text="Japan" />
                                        <telerik:RadComboBoxItem runat="server" Value="108" Text="Jordan" />
                                        <telerik:RadComboBoxItem runat="server" Value="109" Text="Kazakhstan" />
                                        <telerik:RadComboBoxItem runat="server" Value="110" Text="Kenya" />
                                        <telerik:RadComboBoxItem runat="server" Value="111" Text="Kiribati" />
                                        <telerik:RadComboBoxItem runat="server" Value="112" Text="Dem People'S Republic Korea" />
                                        <telerik:RadComboBoxItem runat="server" Value="113" Text="Republic Of Korea" />
                                        <telerik:RadComboBoxItem runat="server" Value="114" Text="Kuwait" />
                                        <telerik:RadComboBoxItem runat="server" Value="115" Text="Kyrgyzstan" />
                                        <telerik:RadComboBoxItem runat="server" Value="116" Text="Lao People'S Dem Republic" />
                                        <telerik:RadComboBoxItem runat="server" Value="117" Text="Latvia" />
                                        <telerik:RadComboBoxItem runat="server" Value="118" Text="Lebanon" />
                                        <telerik:RadComboBoxItem runat="server" Value="119" Text="Lesotho" />
                                        <telerik:RadComboBoxItem runat="server" Value="120" Text="Liberia" />
                                        <telerik:RadComboBoxItem runat="server" Value="121" Text="Libyan Arab Jamahiriya" />
                                        <telerik:RadComboBoxItem runat="server" Value="122" Text="Liechtenstein" />
                                        <telerik:RadComboBoxItem runat="server" Value="123" Text="Lithuania" />
                                        <telerik:RadComboBoxItem runat="server" Value="124" Text="Luxembourg" />
                                        <telerik:RadComboBoxItem runat="server" Value="125" Text="Macau" />
                                        <telerik:RadComboBoxItem runat="server" Value="126" Text="Macedonia" />
                                        <telerik:RadComboBoxItem runat="server" Value="127" Text="Madagascar" />
                                        <telerik:RadComboBoxItem runat="server" Value="128" Text="Malawi" />
                                        <telerik:RadComboBoxItem runat="server" Value="129" Text="Malaysia" />
                                        <telerik:RadComboBoxItem runat="server" Value="130" Text="Maldives" />
                                        <telerik:RadComboBoxItem runat="server" Value="131" Text="Mali" />
                                        <telerik:RadComboBoxItem runat="server" Value="132" Text="Malta" />
                                        <telerik:RadComboBoxItem runat="server" Value="133" Text="Marshall Islands" />
                                        <telerik:RadComboBoxItem runat="server" Value="134" Text="Martinique" />
                                        <telerik:RadComboBoxItem runat="server" Value="135" Text="Mauritania" />
                                        <telerik:RadComboBoxItem runat="server" Value="136" Text="Mauritius" />
                                        <telerik:RadComboBoxItem runat="server" Value="137" Text="Mayotte" />
                                        <telerik:RadComboBoxItem runat="server" Value="138" Text="Mexico" />
                                        <telerik:RadComboBoxItem runat="server" Value="139" Text="Micronesia, Federated States" />
                                        <telerik:RadComboBoxItem runat="server" Value="140" Text="Moldova, Republic Of" />
                                        <telerik:RadComboBoxItem runat="server" Value="141" Text="Monaco" />
                                        <telerik:RadComboBoxItem runat="server" Value="142" Text="Mongolia" />
                                        <telerik:RadComboBoxItem runat="server" Value="143" Text="Montserrat" />
                                        <telerik:RadComboBoxItem runat="server" Value="144" Text="Morocco" />
                                        <telerik:RadComboBoxItem runat="server" Value="145" Text="Mozambique" />
                                        <telerik:RadComboBoxItem runat="server" Value="146" Text="Myanmar" />
                                        <telerik:RadComboBoxItem runat="server" Value="147" Text="Namibia" />
                                        <telerik:RadComboBoxItem runat="server" Value="148" Text="Nauru" />
                                        <telerik:RadComboBoxItem runat="server" Value="149" Text="Nepal" />
                                        <telerik:RadComboBoxItem runat="server" Value="150" Text="Netherlands" />
                                        <telerik:RadComboBoxItem runat="server" Value="151" Text="Netherlands Ant Illes" />
                                        <telerik:RadComboBoxItem runat="server" Value="152" Text="New Caledonia" />
                                        <telerik:RadComboBoxItem runat="server" Value="153" Text="New Zealand" />
                                        <telerik:RadComboBoxItem runat="server" Value="154" Text="Nicaragua" />
                                        <telerik:RadComboBoxItem runat="server" Value="155" Text="Niger" />
                                        <telerik:RadComboBoxItem runat="server" Value="156" Text="Nigeria" />
                                        <telerik:RadComboBoxItem runat="server" Value="157" Text="Niue" />
                                        <telerik:RadComboBoxItem runat="server" Value="158" Text="Norfolk Island" />
                                        <telerik:RadComboBoxItem runat="server" Value="159" Text="Northern Mariana Islands" />
                                        <telerik:RadComboBoxItem runat="server" Value="160" Text="Norway" />
                                        <telerik:RadComboBoxItem runat="server" Value="161" Text="Oman" />
                                        <telerik:RadComboBoxItem runat="server" Value="162" Text="Pakistan" />
                                        <telerik:RadComboBoxItem runat="server" Value="163" Text="Palau" />
                                        <telerik:RadComboBoxItem runat="server" Value="164" Text="Panama" />
                                        <telerik:RadComboBoxItem runat="server" Value="165" Text="Papua New Guinea" />
                                        <telerik:RadComboBoxItem runat="server" Value="166" Text="Paraguay" />
                                        <telerik:RadComboBoxItem runat="server" Value="167" Text="Peru" />
                                        <telerik:RadComboBoxItem runat="server" Value="168" Text="Philippines" />
                                        <telerik:RadComboBoxItem runat="server" Value="169" Text="Pitcairn" />
                                        <telerik:RadComboBoxItem runat="server" Value="170" Text="Poland" />
                                        <telerik:RadComboBoxItem runat="server" Value="171" Text="Portugal" />
                                        <telerik:RadComboBoxItem runat="server" Value="172" Text="Puerto Rico" />
                                        <telerik:RadComboBoxItem runat="server" Value="173" Text="Qatar" />
                                        <telerik:RadComboBoxItem runat="server" Value="174" Text="Reunion" />
                                        <telerik:RadComboBoxItem runat="server" Value="175" Text="Romania" />
                                        <telerik:RadComboBoxItem runat="server" Value="176" Text="Russian Federation" />
                                        <telerik:RadComboBoxItem runat="server" Value="177" Text="Rwanda" />
                                        <telerik:RadComboBoxItem runat="server" Value="178" Text="Saint K Itts And Nevis" />
                                        <telerik:RadComboBoxItem runat="server" Value="179" Text="Saint Lucia" />
                                        <telerik:RadComboBoxItem runat="server" Value="180" Text="Saint Vincent The Grenadines" />
                                        <telerik:RadComboBoxItem runat="server" Value="181" Text="Samoa" />
                                        <telerik:RadComboBoxItem runat="server" Value="182" Text="San Marino" />
                                        <telerik:RadComboBoxItem runat="server" Value="183" Text="Sao Tome And Principe" />
                                        <telerik:RadComboBoxItem runat="server" Value="184" Text="Saudi Arabia" />
                                        <telerik:RadComboBoxItem runat="server" Value="185" Text="Senegal" />
                                        <telerik:RadComboBoxItem runat="server" Value="186" Text="Seychelles" />
                                        <telerik:RadComboBoxItem runat="server" Value="187" Text="Sierra Leone" />
                                        <telerik:RadComboBoxItem runat="server" Value="188" Text="Singapore" />
                                        <telerik:RadComboBoxItem runat="server" Value="189" Text="Slovakia (Slovak Republic)" />
                                        <telerik:RadComboBoxItem runat="server" Value="190" Text="Slovenia" />
                                        <telerik:RadComboBoxItem runat="server" Value="191" Text="Solomon Islands" />
                                        <telerik:RadComboBoxItem runat="server" Value="192" Text="Somalia" />
                                        <telerik:RadComboBoxItem runat="server" Value="193" Text="South Africa" />
                                        <telerik:RadComboBoxItem runat="server" Value="194" Text="South Georgia S Sandwich Is." />
                                        <telerik:RadComboBoxItem runat="server" Value="195" Text="Spain" />
                                        <telerik:RadComboBoxItem runat="server" Value="196" Text="Sri Lanka" />
                                        <telerik:RadComboBoxItem runat="server" Value="197" Text="St. Helena" />
                                        <telerik:RadComboBoxItem runat="server" Value="198" Text="St. Pierre And Miquelon" />
                                        <telerik:RadComboBoxItem runat="server" Value="199" Text="Sudan" />
                                        <telerik:RadComboBoxItem runat="server" Value="200" Text="Suriname" />
                                        <telerik:RadComboBoxItem runat="server" Value="201" Text="Svalbard Jan Mayen Islands" />
                                        <telerik:RadComboBoxItem runat="server" Value="202" Text="Sw Aziland" />
                                        <telerik:RadComboBoxItem runat="server" Value="203" Text="Sweden" />
                                        <telerik:RadComboBoxItem runat="server" Value="204" Text="Switzerland" />
                                        <telerik:RadComboBoxItem runat="server" Value="205" Text="Syrian Arab Republic" />
                                        <telerik:RadComboBoxItem runat="server" Value="206" Text="Taiwan" />
                                        <telerik:RadComboBoxItem runat="server" Value="207" Text="Tajikistan" />
                                        <telerik:RadComboBoxItem runat="server" Value="208" Text="United Republic Of Tanzania" />
                                        <telerik:RadComboBoxItem runat="server" Value="209" Text="Thailand" />
                                        <telerik:RadComboBoxItem runat="server" Value="210" Text="Togo" />
                                        <telerik:RadComboBoxItem runat="server" Value="211" Text="Tokelau" />
                                        <telerik:RadComboBoxItem runat="server" Value="212" Text="Tonga" />
                                        <telerik:RadComboBoxItem runat="server" Value="213" Text="Trinidad And Tobago" />
                                        <telerik:RadComboBoxItem runat="server" Value="214" Text="Tunisia" />
                                        <telerik:RadComboBoxItem runat="server" Value="215" Text="Turkey" />
                                        <telerik:RadComboBoxItem runat="server" Value="216" Text="Turkmenistan" />
                                        <telerik:RadComboBoxItem runat="server" Value="217" Text="Turks And Caicos Islands" />
                                        <telerik:RadComboBoxItem runat="server" Value="218" Text="Tuvalu" />
                                        <telerik:RadComboBoxItem runat="server" Value="219" Text="Uganda" />
                                        <telerik:RadComboBoxItem runat="server" Value="220" Text="Ukraine" />
                                        <telerik:RadComboBoxItem runat="server" Value="221" Text="United Arab Emirates" />
                                        <telerik:RadComboBoxItem runat="server" Value="222" Text="United Kingdom" />
                                        <telerik:RadComboBoxItem runat="server" Value="223" Selected="true" Text="United States" />
                                        <telerik:RadComboBoxItem runat="server" Value="224" Text="United States Minor Is." />
                                        <telerik:RadComboBoxItem runat="server" Value="225" Text="Uruguay" />
                                        <telerik:RadComboBoxItem runat="server" Value="226" Text="Uzbekistan" />
                                        <telerik:RadComboBoxItem runat="server" Value="227" Text="Vanuatu" />
                                        <telerik:RadComboBoxItem runat="server" Value="228" Text="Venezuela" />
                                        <telerik:RadComboBoxItem runat="server" Value="229" Text="Viet Nam" />
                                        <telerik:RadComboBoxItem runat="server" Value="230" Text="Virgin Islands (British)" />
                                        <telerik:RadComboBoxItem runat="server" Value="231" Text="Virgin Islands (U.S.)" />
                                        <telerik:RadComboBoxItem runat="server" Value="232" Text="Wallis And Futuna Islands" />
                                        <telerik:RadComboBoxItem runat="server" Value="233" Text="Western Sahara" />
                                        <telerik:RadComboBoxItem runat="server" Value="234" Text="Yemen" />
                                        <telerik:RadComboBoxItem runat="server" Value="235" Text="Yugoslavia" />
                                        <telerik:RadComboBoxItem runat="server" Value="236" Text="Zaire" />
                                        <telerik:RadComboBoxItem runat="server" Value="237" Text="Zambia" />
                                        <telerik:RadComboBoxItem runat="server" Value="238" Text="Zimbabwe" />
                                    </Items>
                                </telerik:RadComboBox>
                                <asp:RequiredFieldValidator ID="fv8" ForeColor="Red" Display="None" ErrorMessage="*"
                                    runat="server" ValidationGroup="grp2" ControlToValidate="InV8" Enabled="false" />
                            </li>
                            <li visible="false" runat="server" id="fl9">
                                <asp:Label ID="lblF9" runat="server" Text="Phone" CssClass="Qstlabel1"></asp:Label>
                                &nbsp;<asp:Label ID="lblR9" Text="*" runat="server" Visible="false"></asp:Label></li>
                            <li visible="false" runat="server" id="fi9">
                                <asp:TextBox ID="InV9" runat="server" CssClass="textbox_EB"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="fv9" ForeColor="Red" Display="None" ErrorMessage="*"
                                    runat="server" ValidationGroup="grp2" ControlToValidate="InV9" Enabled="false" />
                            </li>
                            <li visible="false" runat="server" id="fl10">
                                <asp:Label ID="lblF10" runat="server" Text="Industry" CssClass="Qstlabel1"></asp:Label>
                                &nbsp;<asp:Label ID="lblR10" Text="*" runat="server" Visible="false"></asp:Label></li>
                            <li visible="false" runat="server" id="fi10">
                                <asp:TextBox ID="InV10" runat="server" CssClass="textbox_EB"></asp:TextBox>
                                <telerik:RadComboBox ID="cmbInV10" runat="server" ShowDropDownOnTextboxClick="True"
                                    MarkFirstMatch="True" AllowCustomText="False" HighlightTemplatedItems="True"
                                    ExpandAnimation-Duration="300" CollapseAnimation-Duration="1000" CollapseAnimation-Type="OutQuart"
                                    NoWrap="True" Font-Names="Verdana" Font-Size="11px" EnableTheming="True" ForeColor="Black"
                                    Font-Italic="False" Skin="Default" Width="220" CssClass="rad-combo" Visible="false">
                                    <Items>
                                        <telerik:RadComboBoxItem runat="server" Value="1" Text="Automotive" />
                                        <telerik:RadComboBoxItem runat="server" Value="2" Text="Beauty/Personal Care" />
                                        <telerik:RadComboBoxItem runat="server" Value="3" Text="Careers/Recruiting" />
                                        <telerik:RadComboBoxItem runat="server" Value="4" Text="Consumer Electronics" />
                                        <telerik:RadComboBoxItem runat="server" Value="5" Text="Consumer Packages Goods (CPG)" />
                                        <telerik:RadComboBoxItem runat="server" Value="6" Text="Dating" />
                                        <telerik:RadComboBoxItem runat="server" Value="7" Text="Education/Training" />
                                        <telerik:RadComboBoxItem runat="server" Value="8" Text="Entertainment/Events/Tickets" />
                                        <telerik:RadComboBoxItem runat="server" Value="9" Text="Financial Services" />
                                        <telerik:RadComboBoxItem runat="server" Value="10" Text="Food/Beverage" />
                                        <telerik:RadComboBoxItem runat="server" Value="11" Text="Games/Gaming" />
                                        <telerik:RadComboBoxItem runat="server" Value="12" Text="Health/Fitness/Diet" />
                                        <telerik:RadComboBoxItem runat="server" Value="13" Text="Healthcare" />
                                        <telerik:RadComboBoxItem runat="server" Value="14" Text="Home/Garden" />
                                        <telerik:RadComboBoxItem runat="server" Value="15" Text="Music/Movies/Media/TV" />
                                        <telerik:RadComboBoxItem runat="server" Value="16" Text="Other Services" />
                                        <telerik:RadComboBoxItem runat="server" Value="17" Text="Public Services" />
                                        <telerik:RadComboBoxItem runat="server" Value="18" Text="Publishing" />
                                        <telerik:RadComboBoxItem runat="server" Value="19" Text="Real Estate" />
                                        <telerik:RadComboBoxItem runat="server" Value="20" Text="Recruiting/HR" />
                                        <telerik:RadComboBoxItem runat="server" Value="21" Text="Restaurant" />
                                        <telerik:RadComboBoxItem runat="server" Value="22" Text="Retail" />
                                        <telerik:RadComboBoxItem runat="server" Value="23" Text="Technology" />
                                        <telerik:RadComboBoxItem runat="server" Value="24" Text="Telecommunications/ISP" />
                                        <telerik:RadComboBoxItem runat="server" Value="25" Text="Travel" />
                                        <telerik:RadComboBoxItem runat="server" Value="26" Text="Other" />
                                    </Items>
                                </telerik:RadComboBox>
                                <asp:RequiredFieldValidator ID="fv10" ForeColor="Red" Display="None" ErrorMessage="*"
                                    runat="server" ValidationGroup="grp2" ControlToValidate="InV10" Enabled="false" />
                            </li>
                            <li visible="false" runat="server" id="fl11">
                                <asp:Label ID="lblF11" runat="server" Text="Organization" CssClass="Qstlabel1"></asp:Label>
                                &nbsp;<asp:Label ID="lblR11" Text="*" runat="server" Visible="false"></asp:Label></li>
                            <li visible="false" runat="server" id="fi11">
                                <asp:TextBox ID="InV11" runat="server" CssClass="textbox_EB"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="fv11" ForeColor="Red" Display="None" ErrorMessage="*"
                                    runat="server" ValidationGroup="grp2" ControlToValidate="InV11" Enabled="false" />
                            </li>
                            <li visible="false" runat="server" id="fl12">
                                <asp:Label ID="lblF12" runat="server" Text="Job Title" CssClass="Qstlabel1"></asp:Label>
                                &nbsp;<asp:Label ID="lblR12" Text="*" runat="server" Visible="false"></asp:Label></li>
                            <li visible="false" runat="server" id="fi12">
                                <asp:TextBox ID="InV12" runat="server" CssClass="textbox_EB"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="fv12" ForeColor="Red" Display="None" ErrorMessage="*"
                                    runat="server" ValidationGroup="grp2" ControlToValidate="InV12" Enabled="false" />
                            </li>
                            <li visible="false" runat="server" id="fl14">
                                <asp:Label ID="lblF14" runat="server" Text="Purchasing Time Frame" CssClass="Qstlabel1"></asp:Label>
                                &nbsp;<asp:Label ID="lblR14" Text="*" runat="server" Visible="false"></asp:Label></li>
                            <li visible="false" runat="server" id="fi14">
                                <asp:TextBox ID="InV14" runat="server" CssClass="textbox_EB"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="fv14" ForeColor="Red" Display="None" ErrorMessage="*"
                                    runat="server" ValidationGroup="grp2" ControlToValidate="InV14" Enabled="false" />
                            </li>
                            <li visible="false" runat="server" id="fl15">
                                <asp:Label ID="lblF15" runat="server" Text="Role in Purchasing Process" CssClass="Qstlabel1"></asp:Label>
                                &nbsp;<asp:Label ID="lblR15" Text="*" runat="server" Visible="false"></asp:Label></li>
                            <li visible="false" runat="server" id="fi15">
                                <asp:TextBox ID="InV15" runat="server" CssClass="textbox_EB"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="fv15" ForeColor="Red" Display="None" ErrorMessage="*"
                                    runat="server" ValidationGroup="grp2" ControlToValidate="InV15" Enabled="false" />
                            </li>
                            <li visible="false" runat="server" id="fl13">
                                <asp:Label ID="lblF13" runat="server" Text="No. of Employees" CssClass="Qstlabel1"></asp:Label>
                                &nbsp;<asp:Label ID="lblR13" Text="*" runat="server" Visible="false"></asp:Label></li>
                            <li visible="false" runat="server" id="fi13">
                                <asp:TextBox ID="InV13" runat="server" CssClass="textbox_EB"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="fv13" ForeColor="Red" Display="None" ErrorMessage="*"
                                    runat="server" ValidationGroup="grp2" ControlToValidate="InV13" Enabled="false" />
                            </li>
                            <li visible="false" runat="server" id="fl16">
                                <asp:Label ID="lblF16" runat="server" Text="Annual Revenue" CssClass="Qstlabel1"></asp:Label>
                                &nbsp;<asp:Label ID="lblR16" Text="*" runat="server" Visible="false"></asp:Label></li>
                            <li visible="false" runat="server" id="fi16">
                                <asp:TextBox ID="InV16" runat="server" CssClass="textbox_EB"></asp:TextBox>
                                <telerik:RadComboBox ID="cmbInV16" runat="server" ShowDropDownOnTextboxClick="True"
                                    MarkFirstMatch="True" AllowCustomText="False" HighlightTemplatedItems="True"
                                    ExpandAnimation-Duration="300" CollapseAnimation-Duration="1000" CollapseAnimation-Type="OutQuart"
                                    NoWrap="True" Font-Names="Verdana" Font-Size="11px" EnableTheming="True" ForeColor="Black"
                                    Font-Italic="False" Skin="Default" Width="120" CssClass="rad-combo" Visible="false">
                                    <Items>
                                        <telerik:RadComboBoxItem runat="server" Value="1" Text="<$1M" />
                                        <telerik:RadComboBoxItem runat="server" Value="2" Text="$1M - $5M" />
                                        <telerik:RadComboBoxItem runat="server" Value="3" Text="$5M - $50M" />
                                        <telerik:RadComboBoxItem runat="server" Value="4" Text="$50M - $100M" />
                                        <telerik:RadComboBoxItem runat="server" Value="5" Text="$100M - $250M" />
                                        <telerik:RadComboBoxItem runat="server" Value="6" Text="$250M - $500M" />
                                        <telerik:RadComboBoxItem runat="server" Value="7" Text="$500M - $1B" />
                                        <telerik:RadComboBoxItem runat="server" Value="8" Text="$1B - $2B" />
                                        <telerik:RadComboBoxItem runat="server" Value="9" Text=">$2B" />
                                    </Items>
                                </telerik:RadComboBox>
                                <asp:RequiredFieldValidator ID="fv16" ForeColor="Red" Display="None" ErrorMessage="*"
                                    runat="server" ValidationGroup="grp2" ControlToValidate="InV16" Enabled="false" />
                            </li>
                            <li visible="false" runat="server" id="fl17">
                                <asp:Label ID="lblF17" runat="server" Text="Addtnl 1" CssClass="Qstlabel1"></asp:Label>
                                &nbsp;<asp:Label ID="lblR17" Text="*" runat="server" Visible="false"></asp:Label></li>
                            <li visible="false" runat="server" id="fi17">
                                <asp:TextBox ID="InV17" runat="server" CssClass="textbox_EB"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="fv17" ForeColor="Red" Display="None" ErrorMessage="*"
                                    runat="server" ValidationGroup="grp2" ControlToValidate="InV17" Enabled="false" />
                            </li>
                            <li visible="false" runat="server" id="fl18">
                                <asp:Label ID="lblF18" runat="server" Text="Addtnl 2" CssClass="Qstlabel1"></asp:Label>
                                &nbsp;<asp:Label ID="lblR18" Text="*" runat="server" Visible="false"></asp:Label></li>
                            <li visible="false" runat="server" id="fi18">
                                <asp:TextBox ID="InV18" runat="server" CssClass="textbox_EB"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="fv18" ForeColor="Red" Display="None" ErrorMessage="*"
                                    runat="server" ValidationGroup="grp2" ControlToValidate="InV18" Enabled="false" />
                            </li>
                        </ul>
                        <asp:Repeater ID="rpAdditionalForm" runat="server" OnItemDataBound="rpAdditionalForm_ItemDataBound">
                            <HeaderTemplate>
                                <ul>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <li>
                                    <asp:HiddenField ID="hQResponseType" Value='<%# DataBinder.Eval(Container.DataItem, "QResponseType") %>'
                                        runat="server" />
                                    <asp:HiddenField ID="hQResponseOptions" Value='<%# DataBinder.Eval(Container.DataItem, "QResponseOptions") %>'
                                        runat="server" />
                                    <asp:HiddenField ID="hQID" Value='<%# DataBinder.Eval(Container.DataItem, "qaID") %>'
                                        runat="server" />
                                    <asp:Label ID="lblQst" Text='<%# DataBinder.Eval(Container.DataItem, "RegFormQuestion") %>'
                                        runat="server" CssClass="Qstlabel1"></asp:Label>
                                    <br />
                                    <asp:TextBox ID="txtComments" runat="server" Visible="false" CssClass="textbox_EBComments"
                                        TextMode="MultiLine" Rows="3" MaxLength="1000">
                                    </asp:TextBox>
                                    <telerik:RadComboBox ID="rcmbType" runat="server" Visible="false">
                                    </telerik:RadComboBox>
                                    <asp:CheckBoxList ID="chkList" runat="server" Visible="false" CssClass="Reg-Form-ChkField"
                                        RepeatDirection="Vertical">
                                    </asp:CheckBoxList>
                                    <asp:RadioButtonList ID="rbtnList" runat="server" Visible="false" CssClass="optList"
                                        RepeatDirection="Vertical">
                                    </asp:RadioButtonList>
                                </li>
                            </ItemTemplate>
                            <FooterTemplate>
                                </ul>
                            </FooterTemplate>
                        </asp:Repeater>
                    </div>
                    <div id="dvAdditonalWeb" runat="server" visible="false">
                        <table width="100%">
                            <tr>
                                <td>
                                    Select related webinar you like to attend
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Repeater ID="rpRelatedWebinar" runat="server" OnItemDataBound="rpRelatedWebinar_ItemDataBound">
                                        <HeaderTemplate>
                                            <table width="100%">
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td width="15px" valign="top">
                                                    <asp:CheckBox ID="chkID" runat="server" /><asp:HiddenField ID="hID" runat="server" />
                                                </td>
                                                <td>
                                                    <asp:LinkButton ID="lbtnTitle" runat="server"></asp:LinkButton>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            </table>
                                        </FooterTemplate>
                                    </asp:Repeater>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <img src="/Images/blank.gif" height="10" />
                    <div id="dvbutton" runat="server">
                        <asp:Button ID="btnRegister" runat="server" CssClass="Submit-btn dvbutton" Text="Register"
                            ValidationGroup="grp2" OnClick="lbtnRegister_Click" />&nbsp;<asp:Label ID="lblEror"
                                ForeColor="Red" runat="server"></asp:Label>
                        <asp:Button ID="Predummy2" runat="server" CssClass="Submit-btn dvbutton" Text="Register" Visible="false" />
                    </div>
                </div>
                <div id="dvRegConf" runat="server" visible="false">
                    <asp:Literal ID="ltrConf" runat="server"></asp:Literal>
                </div>
                <div id="dvRegExist" runat="server" visible="false">
                    <asp:Literal ID="ltrExist" runat="server"></asp:Literal>
                </div>
            </td>
        </tr>
    </table>
</asp:PlaceHolder>
<asp:PlaceHolder ID="phRegFormColleague" runat="server" Visible="false">
    <div id="dvCForm" runat="server" class="Reg-Field">
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="" ValidationGroup="grp3"
            ShowSummary="False" DisplayMode="BulletList" ShowMessageBox="true" ForeColor="#FF0000" />
        <ul class="fld1" runat="server" id="ulcol2">
            <li visible="true" runat="server" id="LiL1" style="line-height: 20px !important">
                <asp:Label ID="lbldesc" runat="server" Text="Invite a Colleague's name and e-mail address and we will send a invitation on your behalf. We will not communicate with your colleagues without their consent.">
                </asp:Label></li>
            <li visible="true" runat="server" id="LiL2">
                <asp:Label ID="lblCFName" runat="server" Text="Your Colleague's First Name"></asp:Label>
                &nbsp;<asp:Label ID="lblM1" Text="*" runat="server" Visible="false"></asp:Label></li>
            <li visible="true" runat="server" id="LiT1">
                <asp:TextBox ID="txtCFName" runat="server" CssClass="textbox_EB"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RF1" ErrorMessage="Colleague First Name cannot be left blank"
                    Display="None" ForeColor="Red" runat="server" ValidationGroup="grp3" ControlToValidate="txtCFName" />
            </li>
            <li visible="true" runat="server" id="LiL3">
                <asp:Label ID="lblCLName" runat="server" Text="Your Colleague's Last Name"></asp:Label>
                &nbsp;<asp:Label ID="lblM2" Text="*" runat="server" Visible="false"></asp:Label></li>
            <li visible="true" runat="server" id="LiT2">
                <asp:TextBox ID="txtCLName" runat="server" CssClass="textbox_EB"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RF2" ErrorMessage="Colleague Last Name cannot be left blank"
                    Display="None" ForeColor="Red" runat="server" ValidationGroup="grp3" ControlToValidate="txtCLName" />
            </li>
            <li visible="true" runat="server" id="LiL4">
                <asp:Label ID="lblCEmail" runat="server" Text="Your Colleague's Email Address"></asp:Label>
                &nbsp;<asp:Label ID="lblM3" Text="*" runat="server" Visible="false"></asp:Label></li>
            <li visible="true" runat="server" id="LiT3">
                <asp:TextBox ID="txtCEmail" runat="server" CssClass="textbox_EB"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RF3" ErrorMessage="Colleague Email Address cannot be left blank"
                    Display="None" ForeColor="Red" runat="server" ValidationGroup="grp3" ControlToValidate="txtCEmail" />
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtCEmail"
                    ValidationGroup="grp3" Display="None" ForeColor="Red" ErrorMessage="Colleague Email must be in the form someone@domain.com. Check leading and trailing space(s)."
                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
            </li>
            <li visible="true" runat="server" id="LiL5">
                <asp:Label ID="lblFName" runat="server" Text="Your First Name"></asp:Label>
                &nbsp;<asp:Label ID="lblM4" Text="*" runat="server" Visible="false"></asp:Label></li>
            <li visible="true" runat="server" id="LiT4">
                <asp:TextBox ID="txtFName" runat="server" CssClass="textbox_EB"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RF4" ErrorMessage="Your First Name cannot be left blank"
                    Display="None" ForeColor="Red" runat="server" ValidationGroup="grp3" ControlToValidate="txtFName" />
            </li>
            <li visible="true" runat="server" id="LiL6">
                <asp:Label ID="lblLName" runat="server" Text="Your Last Name"></asp:Label>
                &nbsp;<asp:Label ID="lblM5" Text="*" runat="server" Visible="false"></asp:Label></li>
            <li visible="true" runat="server" id="LiT5">
                <asp:TextBox ID="txtLName" runat="server" CssClass="textbox_EB"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RF5" ErrorMessage="Your Last Name cannot be left blank"
                    Display="None" ForeColor="Red" runat="server" ValidationGroup="grp3" ControlToValidate="txtLName" />
            </li>
            <li visible="true" runat="server" id="LiL7">
                <asp:Label ID="lblEmail" runat="server" Text="Your Email Address"></asp:Label>
                &nbsp;<asp:Label ID="lblM6" Text="*" runat="server" Visible="false"></asp:Label></li>
            <li visible="true" runat="server" id="LiT6">
                <asp:TextBox ID="txtEmail" runat="server" CssClass="textbox_EB"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RF6" ErrorMessage="Your Email Address cannot be left blank"
                    Display="None" ForeColor="Red" runat="server" ValidationGroup="grp3" ControlToValidate="txtEmail" />
                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtEmail"
                    ValidationGroup="grp3" Display="None" ForeColor="Red" ErrorMessage="Your Email must be in the form someone@domain.com. Check leading and trailing space(s)."
                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
            </li>
        </ul>
        <img src="/Images/blank.gif" height="10" />
        <div id="Div1" runat="server">
            <asp:Button ID="btnReferCollegue" runat="server" CssClass="Submit-btn dvbutton" Text="Send"
                ValidationGroup="grp3" OnClick="btnReferCollegue_Click" />
        </div>
    </div>
    <div id="dvRefConf" runat="server" visible="false">
        <asp:Literal ID="ltrRefConf" runat="server"></asp:Literal>
    </div>
    <div id="dvRefExist" runat="server" visible="false">
        <asp:Literal ID="ltrRefExist" runat="server"></asp:Literal>
    </div>
</asp:PlaceHolder>

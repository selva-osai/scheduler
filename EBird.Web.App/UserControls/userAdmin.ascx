<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="userAdmin.ascx.cs" Inherits="EBird.Web.App.UserControls.userAdmin" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<link href="/css/carousel1.css" rel="stylesheet" type="text/css" />
<style type="text/css">
    .RadComboBoxDropDown_Default .rcbHovered
    {
        background: #EBEBEB;
        color: #C80000;
    }
</style>
<asp:ValidationSummary ID="xevs_userAdmin" CssClass="ValidationSummary" HeaderText=""
    runat="server" ShowSummary="False" ShowMessageBox="True" />
<div class="RPart">
    <div class="TopBg">
    </div>
    <div class="widgets">
        <!-- START: page content -->
        <table width="97%" align="center">
            <tr>
                <td align="right" colspan="3">
                    <asp:Literal ID="ltrBack" runat="server" Text="Return to <a href='Webinar' class='lnkBtn1'>My Webinar</a> to manage or start your webinars"></asp:Literal>
                    <asp:Label ID="lbtnBack_label" runat="server" ForeColor="Black" Text="Return To " Visible="False"></asp:Label>
                    <asp:LinkButton ID="lbtnBack" runat="server" Text="User Management" CssClass="lnkBtn1" CausesValidation="False" Visible="false" OnClick="lbtnBack_Click" />
                </td>
            </tr>
        </table>
        <table width="97%" align="center" style="min-height: 475px;">
            <asp:PlaceHolder ID="phRecList" runat="server" Visible="true">
                <tr>
                    <td valign="top" height="30">
                        <div class="FormCont Steps">
                            <table border="0" width="100%" class="userSearchtbl">
                                <tr>
                                    <td class="usrSearchtxtCell">
                                        <asp:TextBox ID="txtSearch" Columns="40" MaxLength="40" runat="server" Height="20"
                                            Width="270" />
                                        <asp:TextBoxWatermarkExtender ID="txtWatermark" runat="server" TargetControlID="txtSearch"
                                            WatermarkText="Name" WatermarkCssClass="watermarked_EBSearch" />
                                    </td>
                                    <td class="usrSearchOptCell">
                                        <telerik:radcombobox id="rcmbStatus" runat="server" cssclass="cmbPkg" markfirstmatch="True"
                                            highlighttemplateditems="True" expandanimation-duration="1000" collapseanimation-duration="1000"
                                            collapseanimation-type="OutQuart" nowrap="True" enabletheming="True" font-italic="False"
                                            skin="Default">
                                            <Items>
                                                <telerik:RadComboBoxItem Text="ALL" Value="" Selected="True" />
                                                <telerik:RadComboBoxItem Text="Active Users" Value="Active" />
                                                <telerik:RadComboBoxItem Text="Locked Users" Value="Locked" />
                                            </Items>
                                        </telerik:radcombobox>
                                    </td>
                                    <td>
                                        <asp:Button ID="btnFLSearch" runat="server" Text="Search" CssClass="SubBtn" OnClick="btnFLSearch_Click" />
                                        <asp:Button ID="btnAdvSearch" Style="display: none;" runat="server" Text="Search"
                                            OnClick="btnAdvSearch_Click" />
                                    </td>
                                    <td style="padding-left: 110px;">
                                        <asp:HyperLink runat="server" ID="AdvancedSearch" Text="Advanced Search" CssClass="lnkBtn1"></asp:HyperLink>
                                        <asp:HiddenField ID="hSearchType" runat="server" Value="S" />
                                        <asp:HiddenField ID="hSearchText" runat="server" Value="" />
                                        <asp:HiddenField ID="hSearchRole" runat="server" Value="All" />
                                        <asp:HiddenField ID="hEmailContain" runat="server" Value="" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5">
                                        <asp:Label ID="lblMsg1" runat="server" ForeColor="Red"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                            <script src="/qtip2/qtipCommonfn.js" type="text/javascript" charset="utf-8"></script>
                            <script type="text/javascript">
                                jQuery(function ($) {
                                    $("#ContentPlaceHolder1_userprofile1_AdvancedSearch").live('click', function (e) {
                                        var URL = "/Pages/AdvSearchUser";

                                        qtipPopup2("#ContentPlaceHolder1_userprofile1_AdvancedSearch",
                                            "<div id='modalContainer'><div class='modalClose'><img src='/images/x.png' /></div><iframe id='popAdv' src=" + URL + " height='215' width='550' scrolling='no' class='bgfill'></iframe></div>",
                                          550, '.modalClose', '/Pages/UserMgmt', '#popAdv', 'body #hModalStatusFlg');

                                    });
                                });
                            </script>
                        </div>
                    </td>
                </tr>
                <td valign="middle" align="left" width="10%" height="35px">
                    <asp:Button ID="btnAddUser" runat="server" Text="Add" CssClass="SubBtn" OnClick="btnAddUser_Click" />
                    <asp:HiddenField ID="hIsRecycle" runat="server" />
                    <asp:HiddenField ID="hSearchClicked" runat="server" Value="0" />
                </td>
                <tr>
                    <td valign="top">
                        <telerik:radgrid id="tgrdUserList" runat="server" gridlines="None" allowpaging="True"
                            mastertableview-editformsettings-editformtype="Template" allowsorting="True"
                            onitemcommand="tgrdUserList_ItemCommand" onitemdatabound="tgrdUserList_ItemDataBound"
                            onitemcreated="tgrdUserList_ItemCreated" autogeneratecolumns="False" mastertableview-nodetailrecordstext="No active user account available"
                            bordercolor="#D7D7D7" pagesize="15">
                            <PagerStyle Mode="NumericPages" PagerTextFormat="Navigate pages {4} Page {0} of {1}, Users {2} to {3} of {5}" />
                            <ExportSettings HideStructureColumns="true" />
                            <SortingSettings EnableSkinSortStyles="false" />
                            <MasterTableView CommandItemDisplay="Top" AutoGenerateColumns="false" AllowFilteringByColumn="False"
                                ShowFooter="false" TableLayout="Auto" Width="100%">
                                <SortExpressions>
                                    <telerik:GridSortExpression FieldName="firstname,lastname" SortOrder="Ascending" />
                                </SortExpressions>
                                <CommandItemSettings ShowAddNewRecordButton="false" ShowExportToWordButton="true"
                                    ShowRefreshButton="true" ShowExportToExcelButton="true" ShowExportToCsvButton="true"
                                    ShowExportToPdfButton="true" />
                                <NoRecordsTemplate>
                                    <center>
                                        <br />
                                        No active user account available<br />
                                        &nbsp;</center>
                                </NoRecordsTemplate>
                                <Columns>
                                    <telerik:GridTemplateColumn HeaderText="Name" UniqueName="UserName" HeaderStyle-Width="220"
                                        SortExpression="firstname,lastname">
                                        <ItemTemplate>
                                            <asp:Label ID="ltrUserName" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn DataField="EmailID" HeaderText="Email" HeaderStyle-Width="220">
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="webinarCount" HeaderText="Webinar Count">
                                        <ItemStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn UniqueName="LockUser" HeaderText="Lock User">
                                        <ItemStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkLock" runat="server" AutoPostBack="True" OnCheckedChanged="UpdateLockedStatus">
                                            </asp:CheckBox>
                                            <asp:Label ID="lblNoLock" runat="server"></asp:Label>
                                            <asp:HiddenField ID="hUserID" runat="server" Value='<%# Eval("userID") %>' />
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn UniqueName="DeleteColumn" ItemStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnEdit" runat="server" CommandArgument='<%# Eval("userID") %>' 
                                                CommandName="editUser" ImageUrl="~/Images/icons/Edit_16.gif" ToolTip="Update" />
                                            <asp:ImageButton ID="btnDelete" runat="server" CommandArgument='<%# Eval("userID") %>'
                                                CommandName="delUser" ImageUrl="~/Images/icons/trash.png" ToolTip="Delete" />
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                </Columns>
                            </MasterTableView>
                            <ClientSettings>
                                <Scrolling AllowScroll="false" />
                                <Selecting AllowRowSelect="true" />
                                <ClientEvents OnRowMouseOver="RowMouseOver" OnRowMouseOut="RowMouseOut" />
                            </ClientSettings>
                        </telerik:radgrid>
                        <telerik:radcodeblock id="RadCodeBlock1" runat="server">
                            <script type="text/javascript">

                                function clickSearchButton(itemText) {
                                    var btnS = $find("<%= btnFLSearch.ClientID %>");
                                    //var item = menu.findItemByText(itemText);
                                    //btnS.click();
                                    btnS.Value = itemText;
                                }

                                function OnClientItemClicked(sender, args) {
                                    var itemText = args.get_item().get_text()
                                    alert(itemText + " was clicked");
                                }

                                function searchSubmit() {
                                    document.getElementById('<%= btnAdvSearch.ClientID%>').click();
                                }
                            </script>
                        </telerik:radcodeblock>
                    </td>
                </tr>
            </asp:PlaceHolder>
            <tr>
                <td valign="top">
                    <asp:PlaceHolder ID="phRecAddEdit" runat="server" Visible="false">
                        <div class="FormCont Steps" style="padding-bottom: 26px;">
                            <table border="0" width="100%">
                                <tr>
                                    <td colspan="2" style="padding-bottom: 10px;">
                                        <asp:Label ID="lblCaption" runat="server" Text="User Management - Add" Font-Bold="true"
                                            CssClass="frmHeading"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <div class="FormCont Steps" runat="server" id="dvMsg" visible="false">
                                            <asp:Label ID="lblmsg" runat="server" ForeColor="Red"></asp:Label>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="20%" style="padding-left: 5px">
                                        First Name
                                        <asp:PlaceHolder ID="txtUserFName_PlaceHolder" runat="server" Visible="true"><span
                                            class='EBmsg'>&nbsp;*</span> </asp:PlaceHolder>
                                    </td>
                                    <td width="80%">
                                        <asp:TextBox ID="txtUserFName" runat="server" Height="20" Width="250px" CssClass="textbox_EB"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="vx_txtUserFName" runat="server" CssClass="ValidationSummary"
                                            ControlToValidate="txtUserFName" ErrorMessage="Please enter required field - First Name"
                                            ForeColor="Red" Display="None" Text="*" />
                                    </td>
                                </tr>
                                <tr>
                                    <td width="20%" style="padding-left: 5px">
                                        Last Name
                                        <asp:PlaceHolder ID="txtUserLName_PlaceHolder" runat="server" Visible="true"><span
                                            class='EBmsg'>&nbsp;*</span> </asp:PlaceHolder>
                                    </td>
                                    <td width="80%">
                                        <asp:TextBox ID="txtUserLName" runat="server" Height="20" Width="250px" CssClass="textbox_EB"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="vx_txtUserLName" runat="server" CssClass="ValidationSummary"
                                            ControlToValidate="txtUserLName" ErrorMessage="Please enter required field - Last Name"
                                            ForeColor="Red" Display="None" Text="*" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding-left: 5px">
                                        Telephone
                                        <asp:PlaceHolder ID="txtUserPhone_PlaceHolder" runat="server" Visible="true"><span
                                            class='EBmsg'>&nbsp;*</span> </asp:PlaceHolder>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtUserPhone" runat="server" Height="20" Width="200" CssClass="textbox_EB"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="vx_txtUserPhone" runat="server" CssClass="ValidationSummary"
                                            ControlToValidate="txtUserPhone" ErrorMessage="Please enter required field - Telephone"
                                            ForeColor="Red" Display="None" Text="*" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding-left: 5px">
                                        Email Address
                                        <asp:PlaceHolder ID="txtUserEmail_PlaceHolder" runat="server" Visible="true"><span
                                            class='EBmsg'>&nbsp;*</span> </asp:PlaceHolder>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtUserEmail" runat="server" Height="20" Width="250px" CssClass="textbox_EB"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="vx_txtUserEmail" runat="server" CssClass="ValidationSummary"
                                            ControlToValidate="txtUserEmail" ErrorMessage="Please enter required field - Email Address"
                                            ForeColor="Red" Display="None" Text="*" />
                                    </td>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator" runat="server" CssClass="ValidationSummary"
                                        ControlToValidate="txtUserEmail" Display="None" ForeColor="Red" ErrorMessage="Email must be in the form someone@domain.com. Check leading and trailing space(s)."
                                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                </tr>
                                <tr>
                                    <td style="padding-left: 5px">
                                        Department
                                        <asp:PlaceHolder ID="txtUserDept_PlaceHolder" runat="server" Visible="true"><span
                                            class='EBmsg'>&nbsp;*</span> </asp:PlaceHolder>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtUserDept" runat="server" Height="20" Width="250px" CssClass="textbox_EB"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="vx_txtUserDept" runat="server" CssClass="ValidationSummary"
                                            ControlToValidate="txtUserDept" ErrorMessage="Please enter required field - Department"
                                            ForeColor="Red" Display="None" Text="*" />
                                        <asp:HiddenField ID="hUserStatus" runat="server" />
                                    </td>
                                </tr>
                            </table>
                            <asp:PlaceHolder ID="phAdmin" runat="server">
                            <table border="0">
                                <tr>
                                    <td style="padding-left: 5px">
                                        <asp:Label ID="lblCheckAdmin" runat="server" Visible="true"></asp:Label>
                                    </td>
                                    <td class="chkAlign" style="padding-left: 5px">
                                        <asp:CheckBox ID="chkAdmin" runat="server" Visible="true" CssClass="configCheckBox" AutoPostBack="true"
                                            OnCheckedChanged="chkAdmin_CheckedChanged" />&nbsp;
                                        <asp:Label ID="lblPrimary" runat="server" Visible="true"></asp:Label>
                                    </td>
                                </tr>
                                <asp:PlaceHolder ID="phAlternate" runat="server" Visible="false">
                                    <tr>
                                        <td style="padding-left: 5px">
                                            <asp:Label ID="Label1" runat="server" Text="Alternate Primary Administrator" Visible="true"></asp:Label>
                                        </td>
                                        <td width="450">
                                            &nbsp;<telerik:radcombobox id="rcmbAdmin" visible="false" runat="server" showdropdownontextboxclick="True"
                                                markfirstmatch="True" highlighttemplateditems="True" expandanimation-duration="1000"
                                                collapseanimation-duration="1000" collapseanimation-type="OutQuart" nowrap="True"
                                                enabletheming="True" skin="Default" width="250">
                                             </telerik:radcombobox>
                                            <asp:Label ID="lblAlternateAdmin" runat="server" Font-Bold="False"></asp:Label>
                                        </td>
                                    </tr>
                                </asp:PlaceHolder>
                            </table>
                            </asp:PlaceHolder>
                            <div style="margin-left: 8px; margin-top: -12px; margin-bottom: 5px; padding-bottom: 8px;">
                                <asp:PlaceHolder ID="phInstruct" runat="server" Visible="false">
                                    <asp:Label ID="lblInstruct" runat="server"></asp:Label><br />
                                    <asp:CheckBox Text="&nbsp;Increase the number of seats&nbsp;&nbsp;" ID="chkSeats"
                                        CssClass="chkGen chkAlign4" runat="server" Visible="false" AutoPostBack="true"
                                        OnCheckedChanged="chkSeats_CheckedChanged" />
                                    <telerik:Radcombobox id="rcmbSeats" visible="false" runat="server" showdropdownontextboxclick="True"
                                        markfirstmatch="True" highlighttemplateditems="True" expandanimation-duration="1000"
                                        collapseanimation-duration="1000" collapseanimation-type="OutQuart" nowrap="True"
                                        enabletheming="True" skin="Default" width="40">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="5" Value="5" Selected="True" />
                                            <telerik:RadComboBoxItem Text="10" Value="10" />
                                            <telerik:RadComboBoxItem Text="15" Value="15" />
                                            <telerik:RadComboBoxItem Text="20" Value="20" />
                                            <telerik:RadComboBoxItem Text="25" Value="25" />
                                            <telerik:RadComboBoxItem Text="30" Value="30" />
                                            <telerik:RadComboBoxItem Text="35" Value="35" />
                                            <telerik:RadComboBoxItem Text="40" Value="40" />
                                            <telerik:RadComboBoxItem Text="45" Value="45" />
                                            <telerik:RadComboBoxItem Text="50" Value="50" />
                                        </Items>
                                    </telerik:Radcombobox>
                                </asp:PlaceHolder>
                            </div>
                            <asp:PlaceHolder ID="Requiredfield_PlaceHolder" runat="server" Visible="true">
                                <div class="right">
                                    <font color="#A00000">*</font><i><small>Required field</small></i>
                                </div>
                            </asp:PlaceHolder>
                        </div>
                        <asp:Button ID="btnSave" runat="server" OnClientClick="return jsConfirm(this);" Text="Save" CssClass="SubBtn" OnClick="btnSave_Click" />&nbsp;&nbsp;
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="SubBtn" CausesValidation="False"
                            OnClick="btnCancel_Click" />
                        <asp:HiddenField ID="hCurrEmail" runat="server" Value="" />
                        <asp:HiddenField ID="hUserID" runat="server" Value="0" />
                        <asp:HiddenField ID="hAction" Value="A" runat="server" />
                        <script>
                            function jsConfirm(btn) {
                                if (btn.value == 'Delete') {
                                    return confirm("Are you sure, you want to delete this account?");
                                }
                                return true;
                            }
                        </script>
                    </asp:PlaceHolder>
                </td>
            </tr>
        </table>
        <!-- END: page content -->
    </div>
    <div class="BottBg">
    </div>
    <img src="../Images/blank.gif" height="10" />
    <div class="Clr">
    </div>
</div>

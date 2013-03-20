<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/masterWithNav.Master"
    AutoEventWireup="true" CodeBehind="recycle.aspx.cs" Inherits="EBird.Web.App.Pages.recycle" %>

<%@ Register Src="~/UserControls/schLPart.ascx" TagName="schLPart" TagPrefix="uc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="MainCont">
        <div class="Temp1">
            <uc1:schLPart ID="schLPart1" runat="server" />
            <div class="RPart">
                <div class="TopBg">
                </div>
                <div class="widgets">
                    <asp:PlaceHolder ID="phWebinar" runat="server" Visible="false">
                        <table width="100%" align="center" runat="server" id="tbHeader">
                            <tr>
                                <td align="right" style="padding-bottom: 10px;">
                                    <asp:Label ID="lblRtn1" Text="Return to " runat="server"></asp:Label><asp:LinkButton
                                        ID="lbtnBack" runat="server" Text="My Webinar" CssClass="lnkBtn1" OnClick="lbtnBack_Click"
                                        CausesValidation="false"></asp:LinkButton>
                                    <asp:Label ID="lblRtn2" Text=" to manage or start your webinars" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="padding-top: 5px; padding-left: 3px; padding-bottom: 5px">
                                    <asp:Label CssClass="sectionHeader" ID="lblWebinarTitle" Text="My Webinar Recycle Bin"
                                        runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table width="100%" align="center">
                                        <tr>
                                            <td valign="top">
                                                <telerik:RadGrid ID="tgrdWebinarList" runat="server" GridLines="None" AllowPaging="True"
                                                    PageSize="15" MasterTableView-EditFormSettings-EditFormType="Template" AllowSorting="True"
                                                    AutoGenerateColumns="False" ValidationSettings-EnableValidation="false" OnItemCommand="tgrdWebinarList_ItemCommand"
                                                    OnItemDataBound="tgrdWebinarList_ItemDataBound" MasterTableView-NoDetailRecordsText="Recycle Bin is Empty"
                                                    BorderColor="#D7D7D7">
                                                    <PagerStyle Mode="NumericPages" PagerTextFormat="Navigate pages {4} Page {0} of {1}, Webinars {2} to {3} of {5}" />
                                                    <ExportSettings HideStructureColumns="true" />
                                                    <MasterTableView CommandItemDisplay="Top" AutoGenerateColumns="false" AllowFilteringByColumn="False"
                                                        ShowFooter="false" TableLayout="Auto" Width="100%">
                                                        <CommandItemSettings ShowAddNewRecordButton="false" ShowExportToWordButton="false"
                                                            ShowRefreshButton="true" ShowExportToExcelButton="false" ShowExportToCsvButton="false"
                                                            ShowExportToPdfButton="false" />
                                                        <NoRecordsTemplate>
                                                            <center>
                                                                <br />
                                                                Recycle Bin is Empty<br />
                                                                &nbsp;</center>
                                                        </NoRecordsTemplate>
                                                        <RowIndicatorColumn>
                                                            <HeaderStyle Width="20px" />
                                                        </RowIndicatorColumn>
                                                        <ExpandCollapseColumn>
                                                            <HeaderStyle Width="20px" />
                                                        </ExpandCollapseColumn>
                                                        <Columns>
                                                            <telerik:GridTemplateColumn HeaderText="Restore" UniqueName="Restore" HeaderStyle-Width="60px">
                                                                <ItemStyle HorizontalAlign="Center" />
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chkRestore" CssClass="dummycss" runat="server" />
                                                                    <asp:HiddenField ID="hWebinarID" runat="server" Value='<%# Eval("WebinarID") %>' />
                                                                </ItemTemplate>
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridBoundColumn DataField="Title" HeaderText="Title" />
                                                            <telerik:GridTemplateColumn HeaderText="Date and Time" UniqueName="webinarDate" HeaderStyle-Width="160px">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblDateTime" runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridTemplateColumn HeaderText="Deleted date" UniqueName="delDate" HeaderStyle-Width="110px">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblDelDateTime" runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridBoundColumn DataField="Registered" HeaderText="Registrants" ItemStyle-HorizontalAlign="Center"
                                                                HeaderStyle-Width="60px" />
                                                            <telerik:GridTemplateColumn HeaderText="" HeaderStyle-Width="10px">
                                                                <ItemTemplate>
                                                                    <asp:HyperLink runat="server" ID="hlnkPhone" data-webid='<%# Eval("WebinarID") %>'
                                                                        data-typ='P' ToolTip="Webinar Presenter Details" BorderWidth="0" ImageUrl="~/Images/icons/trash.png"
                                                                        BorderStyle="None" CssClass="gridIco"></asp:HyperLink>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="right" />
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridBoundColumn DataField="WebinarID" Visible="false" />
                                                            <telerik:GridBoundColumn DataField="startDate" Visible="false" />
                                                            <telerik:GridBoundColumn DataField="StartTime" Visible="false" />
                                                            <telerik:GridBoundColumn DataField="modifiedOn" Visible="false" />
                                                        </Columns>
                                                    </MasterTableView>
                                                    <ClientSettings>
                                                        <Scrolling AllowScroll="false" />
                                                        <Selecting AllowRowSelect="true" />
                                                        <ClientEvents OnRowMouseOver="RowMouseOver" OnRowMouseOut="RowMouseOut" />
                                                    </ClientSettings>
                                                </telerik:RadGrid>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="padding-top: 5px">
                                                <asp:Button ID="btnRestore" OnClick="btnRestore_Click" runat="server" Text="Restore"
                                                    CssClass="SubBtn" />
                                                <asp:Button ID="btnCancel" OnClick="btnCanCancel_Click" CssClass="SubBtn" runat="server"
                                                    Text="Cancel" CausesValidation="False" CommandName="Cancel" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                        <script src="/qtip2/qtipCommonfn.js" type="text/javascript" charset="utf-8"></script>
                        <script type="text/javascript">
                            $(document).ready(function () {
                                $(".gridIco").live('click', function (e) {
                                    var webinarID = $(this).attr('data-webid');
                                    var URL = "/Pages/popup/DeleteWebinar.aspx?ID=" + webinarID + "&a=d";

                                    qtipPopup2(".gridIco",
                                    "<div id='modalContainer'><div class='modalClose'><img src='/images/x.png' /></div><iframe id='popDelWeb' src=" + URL + " height='125' width='350' scrolling='no' class='bgfill'></iframe></div>",
                                    350, ".modalClose", "/Pages/Recycle/webinar", "#popDelWeb", "body #ContentPlaceHolder1_hModalStatusFlg");
                                });
                            });
                        </script>
                    </asp:PlaceHolder>
                    <asp:PlaceHolder ID="phUserMgmt" runat="server" Visible="false">
                        <table width="100%" align="center" runat="server" id="tbHeader1">
                            <tr>
                                <td align="right" style="padding-bottom: 10px;">
                                    <asp:Label ID="lblRtnw1" Text="Return to " runat="server"></asp:Label><asp:LinkButton
                                        ID="lbtnBack1" runat="server" Text="User management" CssClass="lnkBtn1" OnClick="lbtnBack1_Click"
                                        CausesValidation="false"></asp:LinkButton>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="padding-top: 5px; padding-left: 3px; padding-bottom: 5px">
                                    <asp:Label CssClass="sectionHeader" ID="lblrecycle" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table width="100%" align="center">
                                        <tr>
                                            <td valign="top">
                                                <telerik:RadGrid ID="tgrdUserList" runat="server" GridLines="None" AllowPaging="True"
                                                    PageSize="15" MasterTableView-EditFormSettings-EditFormType="Template" AllowSorting="True"
                                                    AutoGenerateColumns="False" ValidationSettings-EnableValidation="false" OnItemCommand="tgrdUserList_ItemCommand"
                                                    OnItemDataBound="tgrdUserList_ItemDataBound" MasterTableView-NoDetailRecordsText="Recycle Bin is Empty"
                                                    BorderColor="#D7D7D7">
                                                    <PagerStyle Mode="NumericPages" PagerTextFormat="Navigate pages {4} Page {0} of {1}, Users {2} to {3} of {5}" />
                                                    <ExportSettings HideStructureColumns="true" />
                                                    <MasterTableView CommandItemDisplay="Top" AutoGenerateColumns="false" AllowFilteringByColumn="False"
                                                        ShowFooter="false" TableLayout="Auto" Width="100%">
                                                        <CommandItemSettings ShowAddNewRecordButton="false" ShowExportToWordButton="false"
                                                            ShowRefreshButton="true" ShowExportToExcelButton="false" ShowExportToCsvButton="false"
                                                            ShowExportToPdfButton="false" />
                                                        <NoRecordsTemplate>
                                                            <center>
                                                                <br />
                                                                Recycle Bin is Empty<br />
                                                                &nbsp;</center>
                                                        </NoRecordsTemplate>
                                                        <RowIndicatorColumn>
                                                            <HeaderStyle Width="20px" />
                                                        </RowIndicatorColumn>
                                                        <ExpandCollapseColumn>
                                                            <HeaderStyle Width="20px" />
                                                        </ExpandCollapseColumn>
                                                        <Columns>
                                                            <telerik:GridTemplateColumn HeaderText="Restore" UniqueName="Restore" HeaderStyle-Width="60px">
                                                                <ItemStyle HorizontalAlign="Center" />
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chkRestore" CssClass="dummycss" runat="server" />
                                                                    <asp:HiddenField ID="hUserID" runat="server" Value='<%# Eval("UserID") %>' />
                                                                    <asp:HiddenField ID="hUserStatus" runat="server" Value='<%# Eval("userStatus") %>' />
                                                                </ItemTemplate>
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridTemplateColumn HeaderText="Name" UniqueName="UserName" HeaderStyle-Width="220"
                                                                SortExpression="firstname,lastname">
                                                                <ItemTemplate>
                                                                    <asp:Literal ID="ltrUserName" runat="server"></asp:Literal>
                                                                </ItemTemplate>
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridBoundColumn DataField="EmailID" HeaderText="Email" HeaderStyle-Width="220">
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="webinarCount" HeaderText="Webinar Count">
                                                                <ItemStyle HorizontalAlign="Center" />
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn Visible="false" DataField="FirstName" />
                                                            <telerik:GridBoundColumn Visible="false" DataField="LastName" />
                                                            <telerik:GridBoundColumn Visible="false" DataField="Role" />
                                                            <telerik:GridBoundColumn Visible="false" DataField="UserStatus" />
                                                        </Columns>
                                                    </MasterTableView>
                                                    <ClientSettings>
                                                        <Scrolling AllowScroll="false" />
                                                        <Selecting AllowRowSelect="true" />
                                                        <ClientEvents OnRowMouseOver="RowMouseOver" OnRowMouseOut="RowMouseOut" />
                                                    </ClientSettings>
                                                </telerik:RadGrid>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <div class="clear" style="margin-left: 3px">
                                                    <asp:PlaceHolder ID="phInstruct" runat="server" Visible="false">
                                                        <div style="padding:15px 0px 10px 0px;">
                                                            <asp:Label ID="lblInstruct" runat="server" Text="Number of users selected requires more user seats than what is available."></asp:Label></div>
                                                        <div style="padding:5px 0px 10px 0px;">
                                                            <asp:CheckBox Text="&nbsp;Increase the number of seats&nbsp;&nbsp;" ID="chkSeats"
                                                                CssClass="chkGen chkAlign3" runat="server" Visible="true" AutoPostBack="true"
                                                                OnCheckedChanged="chkSeats_CheckedChanged" />
                                                            &nbsp;<telerik:RadComboBox ID="rcmbSeats" runat="server" ShowDropDownOnTextboxClick="True"
                                                                MarkFirstMatch="True" HighlightTemplatedItems="True" ExpandAnimation-Duration="1000"
                                                                CollapseAnimation-Duration="1000" CollapseAnimation-Type="OutQuart" NoWrap="True"
                                                                EnableTheming="True" Skin="Default" Width="50">
                                                                <Items>
                                                                    <telerik:RadComboBoxItem Text="5" Value="5" />
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
                                                            </telerik:RadComboBox>
                                                        </div>
                                                    </asp:PlaceHolder>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="padding-top: 5px">
                                                <asp:Button ID="btnUserRestore" OnClick="btnUserRestore_Click" runat="server" Text="Restore"
                                                    CssClass="SubBtn" />
                                                <asp:Button ID="btnUserCancel" OnClick="btnUserCancel_Click" CssClass="SubBtn" runat="server"
                                                    Text="Cancel" CausesValidation="False" CommandName="Cancel" />&nbsp;
                                                <asp:Label ID="lblUserError" runat="server" ForeColor="Red"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </asp:PlaceHolder>
                </div>
                <div class="BottBg">
                </div>
                <img src="/Images/blank.gif" height="10" />
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

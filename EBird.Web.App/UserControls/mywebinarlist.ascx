<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="mywebinarlist.ascx.cs"
    Inherits="EBird.Web.App.UserControls.mywebinarlist" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<link rel="Stylesheet" type="text/css" href="~/Styles/Style.css" id="style" runat="server" />
<link href="/css/carousel1.css" rel="stylesheet" type="text/css" />
<style type="text/css">
    .RadComboBoxDropDown_Default .rcbHovered
    {
        background: #EBEBEB;
        color: #C80000;
    }
    
    .RadCalendar_Default .rcMain .rcRow .rcHover, .RadCalendar_Default .rcRow .rcSelected
    {
        background: #EBEBEB 0 -1700px repeat-x url(Calendar/sprite.gif);
        border-color: #EBEBEB;
    }
    
    .RadCalendar_Default .rcMain .rcRow .rcHover a, .RadCalendar_Default .rcMain .rcRow .rcSelected a
    {
        color: #C80000;
    }
    
    .RadCalendarMonthView_Default .rcSelected a, .RadCalendarTimeView_Default td.rcHover a, .RadCalendarTimeView_Default td.rcSelected a
    {
        background: #EBEBEB 0 -1700px repeat-x url(Calendar/sprite.gif);
        color: #C80000;
        border-color: #EBEBEB;
    }
</style>
<div class="RPart">
    <div class="TopBg">
    </div>
    <div class="widgets">
        <div class="FormCont Steps1">
            <table border="0" width="100%" cellpadding="5" style="font-family: Verdana; padding: 5px 2px 2px 5px;">
                <tr>
                    <td>
                        <asp:TextBox ID="txtSearch" Columns="40" MaxLength="40" runat="server" Height="20"
                            Width="270" />
                        <asp:TextBoxWatermarkExtender ID="txtWatermark" runat="server" TargetControlID="txtSearch"
                            WatermarkText="Webinar Title" WatermarkCssClass="watermarked_EBSearch" />
                    </td>
                    <td>
                        <telerik:RadComboBox ID="rcmbDateDur" runat="server" CssClass="cmbPkg" AutoPostBack="True"
                            OnSelectedIndexChanged="rcmbDateDur_SelectedIndexChanged" MarkFirstMatch="True"
                            HighlightTemplatedItems="True" ExpandAnimation-Duration="1000" CollapseAnimation-Duration="1000"
                            CollapseAnimation-Type="OutQuart" NoWrap="True" EnableTheming="True" Font-Italic="False"
                            Skin="Default">
                            <Items>
                                <telerik:RadComboBoxItem Text="ALL" Selected="True" Value=""></telerik:RadComboBoxItem>
                                <telerik:RadComboBoxItem Text="Past 30 days" Value="-30"></telerik:RadComboBoxItem>
                                <telerik:RadComboBoxItem Text="Past 60 days" Value="-60"></telerik:RadComboBoxItem>
                                <telerik:RadComboBoxItem Text="Past 90 days" Value="-90"></telerik:RadComboBoxItem>
                            </Items>
                        </telerik:RadComboBox>
                    </td>
                    <td>
                        <asp:Button ID="btnSearch" CssClass="SubBtn" runat="server" Text="Search" OnClick="btnSearch_Click" />
                        <asp:Button ID="btnAdvSearch" Style="display: none;" runat="server" Text="Search"
                            OnClick="btnAdvSearch_Click" />
                    </td>
                    <td align="right" width="50%" style="padding-left: 6px;">
                        <asp:HyperLink runat="server" ID="AdvancedSearch" Text="Advanced Search" CssClass="lnkBtn1"></asp:HyperLink>
                        <asp:HiddenField ID="hSearchText" runat="server" Value="" />
                        <asp:HiddenField ID="hSearchType" runat="server" Value="S" />
                        <asp:HiddenField ID="hSearchField" runat="server" Value="All" />
                        <asp:HiddenField ID="hSearchClicked" runat="server" Value="0" />
                        <asp:HiddenField ID="hRegNo" runat="server" Value="0" />
                        <asp:HiddenField ID="hViewNo" runat="server" Value="0" />
                        <asp:HiddenField ID="hDemandNo" runat="server" Value="0" />
                    </td>
                </tr>
            </table>
        </div>
        <div class="Steps1">
            <table border="0" width="100%" style="font-family: Verdana; padding: 5px 2px 2px 5px;">
                <tr>
                    <td colspan="2" valign="middle" height="27">
                        <span style="font-weight: 400; padding: 0px 3px 0px 0px;">Filter From</span>&nbsp;
                        <telerik:RadDatePicker ID="dpFrom" Width="120px" runat="server" AutoPostBack="true"
                            OnSelectedDateChanged="dpFrom_SelectedDateChanged" HideAnimation-Duration="800"
                            ShowAnimation-Duration="800">
                            <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x">
                            </Calendar>
                            <DateInput DisplayDateFormat="MM-dd-yyyy" DateFormat="MM-dd-yyyy" DisplayText=""
                                LabelWidth="40%" type="text" value="" AutoPostBack="True">
                            </DateInput>
                            <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                        </telerik:RadDatePicker>
                        <span style="font-weight: 400; padding: 0px 3px 0px 5px;">To</span>&nbsp;
                        <telerik:RadDatePicker ID="dpTo" Width="120px" runat="server" AutoPostBack="true"
                            OnSelectedDateChanged="dpTo_SelectedDateChanged" HideAnimation-Duration="800"
                            ShowAnimation-Duration="800">
                            <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x">
                            </Calendar>
                            <DateInput DisplayDateFormat="MM-dd-yyyy" DateFormat="MM-dd-yyyy" DisplayText=""
                                LabelWidth="40%" type="text" value="" AutoPostBack="True">
                            </DateInput>
                            <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                        </telerik:RadDatePicker>
                    </td>
                </tr>
            </table>
        </div>
        <table width="100%" align="center" style="min-height: 350px;">
            <tr>
                <td valign="top">
                    <telerik:RadGrid ID="tgrdWebinarList" runat="server" GridLines="None" AllowPaging="True"
                        PageSize="15" MasterTableView-EditFormSettings-EditFormType="Template" AllowSorting="True"
                        AutoGenerateColumns="False" ValidationSettings-EnableValidation="false" OnItemCommand="tgrdWebinarList_ItemCommand"
                        OnItemDataBound="tgrdWebinarList_ItemDataBound" OnItemCreated="tgrdWebinarList_ItemCreated"
                        MasterTableView-NoDetailRecordsText="No active webinars available" BorderColor="#D7D7D7">
                        <PagerStyle Mode="NumericPages" PagerTextFormat="Navigate pages {4} Page {0} of {1}, Webinars {2} to {3} of {5}" />
                        <ExportSettings HideStructureColumns="true" />
                        <SortingSettings EnableSkinSortStyles="false" />
                        <MasterTableView CommandItemDisplay="Top" AutoGenerateColumns="false" AllowFilteringByColumn="False"
                            ShowFooter="false" TableLayout="Auto" Width="100%">
                            <SortExpressions>
                                <telerik:GridSortExpression FieldName="StartDate" SortOrder="Descending" />
                            </SortExpressions>

                            <CommandItemSettings ShowAddNewRecordButton="false" ShowExportToWordButton="true"
                                ShowRefreshButton="true" ShowExportToExcelButton="true" ShowExportToCsvButton="true"
                                ShowExportToPdfButton="true" />
                            <NoRecordsTemplate>
                                <center>
                                    <br />
                                    No active webinars available<br />
                                    &nbsp;</center>
                            </NoRecordsTemplate>
                            <RowIndicatorColumn>
                                <HeaderStyle Width="20px" />
                            </RowIndicatorColumn>
                            <ExpandCollapseColumn>
                                <HeaderStyle Width="20px" />
                            </ExpandCollapseColumn>
                            <Columns>
                                <telerik:GridTemplateColumn HeaderText="Inactive" UniqueName="Inactive" HeaderStyle-Width="20px">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkInactive" CssClass="dummycss" runat="server" />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderText="Title" UniqueName="Title" DataField="Title"
                                    HeaderStyle-Width="280px" AllowFiltering="false" SortExpression="Title">
                                    <ItemTemplate>
                                        <asp:LinkButton CssClass="lnkBtn1" ID="lnkWeb" CommandName="View" Text='<%# Eval("Title") %>'
                                            runat="server" CommandArgument='<%# Eval("WebinarID") %>' CausesValidation="false"></asp:LinkButton>
                                        <asp:Label Text='<%# Eval("Title") %>' runat="server" ID="lblTitle" Visible="false" />
                                        <asp:Image ID="imgDraft" runat="server" ImageUrl="~/Images/icons/draft.png" Visible="false" />
                                    </ItemTemplate>
                                    <HeaderStyle Width="390px" />
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderText="Date and Time" UniqueName="webinarDate" HeaderStyle-Width="200px"
                                    SortExpression="StartDate">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDateTime" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderText="Registered" UniqueName="RegisteredLink" SortExpression="Registered">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkReg" CommandName="Reg" Text='<%# Eval("Registered") %>' runat="server" CssClass="lnkBtn1" 
                                            CommandArgument='<%# Eval("WebinarID") %>' CausesValidation="false"></asp:LinkButton>
                                        <asp:Label ID="lnkRegLabel" runat="server" Text="0" ForeColor="Black" Visible="False"></asp:Label>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderText="Live" UniqueName="LiveLink" SortExpression="Live">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkLive" CommandName="live" Text='<%# Eval("Live") %>' runat="server" CssClass="lnkBtn1" 
                                            CommandArgument='<%# Eval("WebinarID") %>' CausesValidation="false"></asp:LinkButton>
                                        <asp:Label ID="lnkLiveLabel" runat="server" Text="0" ForeColor="Black" Visible="False"></asp:Label>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderText="OnDemand" UniqueName="OnDemandLink" SortExpression="OnDemand">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkOnDemand" CommandName="onDemand" Text='<%# Eval("OnDemand") %>' CssClass="lnkBtn1" 
                                            runat="server" CommandArgument='<%# Eval("WebinarID") %>' CausesValidation="false"></asp:LinkButton>
                                        <asp:Label ID="lnkOnDemandLabel" runat="server" Text="0" ForeColor="Black" Visible="False"></asp:Label>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderText="" HeaderStyle-Width="120px">
                                    <ItemTemplate>
                                        <asp:HyperLink runat="server" ID="hlnkPhone" data-webid='<%# Eval("WebinarID") %>'
                                            data-typ='P' ToolTip="Webinar Presenter Details" BorderWidth="0" ImageUrl="~/Images/icons/phone.png"
                                            BorderStyle="None" CssClass="gridIco"></asp:HyperLink>
                                        <asp:HyperLink runat="server" ID="WebinarURLs" data-webid='<%# Eval("WebinarID") %>'
                                            data-typ='U' ToolTip="View Webinar URLs" BorderWidth="0" ImageUrl="~/Images/icons/Web_Server_16.gif"
                                            BorderStyle="None" CssClass="gridIco1"></asp:HyperLink>
                                        <img src="~/Images/icons/icoAnalytics.gif" alt="Analytics" data-webid='<%# Eval("WebinarID") %>'
                                            runat="server" id="imgAnal" cssclass="gridIco" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="right" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn DataField="WebinarID" Visible="false" />
                                <telerik:GridBoundColumn DataField="startDate" Visible="false" />
                                <telerik:GridBoundColumn DataField="StartTime" Visible="false" />
                                <telerik:GridBoundColumn DataField="EndTime" Visible="false" />
                                <telerik:GridBoundColumn DataField="registered" Visible="false" />
                                <telerik:GridBoundColumn DataField="live" Visible="false" />
                                <telerik:GridBoundColumn DataField="OnDemand" Visible="false" />
                                <telerik:GridBoundColumn DataField="webinarStatus" Visible="false" />
                            </Columns>
                        </MasterTableView>
                        <ClientSettings>
                            <Scrolling AllowScroll="false" />
                            <Selecting AllowRowSelect="true" />
                            <ClientEvents OnRowMouseOver="RowMouseOver" OnRowMouseOut="RowMouseOut" />
                        </ClientSettings>
                    </telerik:RadGrid>
                    <asp:HiddenField ID="hWMinDate" runat="server" />
                    <asp:HiddenField ID="hWMaxDate" runat="server" />
                    <asp:HiddenField ID="hIsRecycle" runat="server" />
                </td>
            </tr>
        </table>
    </div>
    <div class="BottBg">
    </div>
    <img src="../Images/blank.gif" data-webid="5" height="10" />
    <div class="Clr">
    </div>
    <script src="/qtip2/qtipCommonfn.js" type="text/javascript" charset="utf-8"></script>
    <script src="/qtip2/qtip_webinarlist_click.js" type="text/javascript" charset="utf-8"></script>
    <script type="text/javascript">
        function clickSearchButton(itemText) {
            var btnS = $find("<%= btnSearch.ClientID %>");
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
</div>

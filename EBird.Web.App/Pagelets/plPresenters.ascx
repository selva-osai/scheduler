<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="plPresenters.ascx.cs"
    Inherits="EBird.Web.App.Pagelets.plPresenters" %>
<asp:HiddenField ID="hWebinarID" runat="server" />
<div class="Webi-Speaker">
    <asp:Repeater ID="rpPresenter" runat="server" OnItemDataBound="rpPresenter_ItemDataBound">
        <HeaderTemplate>
        </HeaderTemplate>
        <ItemTemplate>
            <div class="Profile">
                <div class="Pro-title">
                    <h2>
                        Presenter</h2>
                </div>
                <div class="Pro-img">
                    <asp:Image ID="imgPresenterImg" runat="server" ImageUrl="/Images/brian-bio-125px.png"
                        alt="" /></div>
                <div class="Pro-desc">
                    <div class="Pro-name">
                        <asp:Literal ID="ltrPreInfo" runat="server" Text=""></asp:Literal></div>
                    <div class="Pro-Desi">
                        <asp:Literal ID="ltrDesi" runat="server" Text=""></asp:Literal>
                    </div>
                    <div class="showmore-presenter">
                      <div class="moreblock-presenter">
                       <p><asp:Literal ID="ltrPreBio" runat="server" Text=""></asp:Literal></p>
                      </div>
                    </div>
                 </div>
                <div class="Clr">
                </div>
            </div>
        </ItemTemplate>
        <FooterTemplate>
        </FooterTemplate>
    </asp:Repeater>
    <div class="Clr">
    </div>
</div>
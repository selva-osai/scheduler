<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UC_Spinner.ascx.cs"
    Inherits="EBird.Web.App.CommonControls.UC_Spinner" %>
<script type="text/javascript" language="javascript">
    function getval(typ) {

        var otxt = document.getElementById('<%=txtNum.ClientID%>');
        var omin = document.getElementById('<%=hMin.ClientID%>');
        var omax = document.getElementById('<%=hMax.ClientID%>');

        j = otxt.value;
        if (j == '')
            otxt.Text = omin;
        if (typ == 'i') {
            ++j;
        }
        else {
            --j;
        }
        if (j < omin.value)
            j = omin.value;
        if (j > omax.value)
            j = omax.value;
        otxt.value = j;
    }
</script>
<div>
    Every&nbsp;<asp:TextBox ID="txtNum" runat="server" Width="50"></asp:TextBox>
    <span class="incdec">
        <img alt="" runat="server" id="imgIncr" src="~/Images/icons/incre.png" onclick="getval('i');" />
        <img alt="" runat="server" id="imgDecr" src="~/Images/icons/incre.png" onclick="getval('d');" />
    </span>
</div>
<asp:HiddenField runat="server" ID="hMin" Value="1" />
<asp:HiddenField runat="server" ID="hMax" Value="10" />

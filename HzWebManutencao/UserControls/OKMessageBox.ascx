<%@ Control Language="C#" AutoEventWireup="true" Inherits="UserControls_OKMessageBox" Codebehind="OKMessageBox.ascx.cs" %>
<asp:ModalPopupExtender ID="mpext" runat="server" BackgroundCssClass="modalBackground" TargetControlID="pnlPopup" PopupControlID="pnlPopup"></asp:ModalPopupExtender>
<asp:Panel ID="pnlPopup" runat="server" CssClass="modalPopup" Style="display: none;" DefaultButton="btnOk">
    <table width="100%">
        <tr class="topHandle">
            <td colspan="2" align="left" runat="server" id="tdCaption">
                &nbsp; <asp:Label ID="lblCaption" runat="server" Font-Size="Medium"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 60px" valign="middle" align="center">
                <asp:Image ID="imgInfo" runat="server" ImageUrl="~/Imagens/Info-48x48.png" />
            </td>
            <td valign="middle" align="left" style="width: 500px">
                <asp:Label ID="lblMessage" runat="server" Font-Size="Medium"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="right">
                <asp:Button ID="btnOk" runat="server" Text="Ok" OnClick="btnOk_Click" />
            </td>
        </tr>
    </table>
</asp:Panel>

<script type="text/javascript">
        function fnClickOK(sender, e)
        {
            __doPostBack(sender,e);
        }
</script>


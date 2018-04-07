<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="webHorizon.aspx.cs" Inherits="HzWebManutencao.Horizon.webHorizon" %>
<%@ Reference Control="WebUserControl1.ascx" %>  


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server" >

        <script src="xtreeview.js"  type="text/javascript"></script>
    <div style="position: relative; top: 12px; left: 9px; height: 500px; width: 289px;" >
        <asp:Label ID="Label1" runat="server" Text="Centro de Custo"></asp:Label>
        <br />
        <asp:DropDownList ID="cmbCentroCusto" runat="server" Height="18px" 
            Width="149px" onselectedindexchanged="cmbCentroCusto_SelectedIndexChanged" 
            AutoPostBack="True">
        </asp:DropDownList>
        &nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnNovo" runat="server" Text="Novo" onclick="btnNovo_Click" 
            Width="42px" />
        <br />
        <br />
        <asp:Panel ID="Panel1" runat="server" Height="500px" ScrollBars="Vertical">
            <asp:TreeView ID="TreeView1" runat="server" Height="343px" Width="195px" 
            onselectednodechanged="TreeView1_SelectedNodeChanged" 
    ShowLines="True">
            </asp:TreeView>
        </asp:Panel>

    </div>
        <div id="difEquipamento" visible=false style="position: relative; top: -472px; left: 308px; height: 25px; width: 655px; bottom: 472px;" 
         runat=server>
    &nbsp;&nbsp;
            <asp:LinkButton ID="lkbHistoricoManutencao" runat="server">Histórico Manutenção</asp:LinkButton>
&nbsp;&nbsp;
            <asp:LinkButton ID="lkbFotos" runat="server">Fotos</asp:LinkButton>
&nbsp;&nbsp;
            <asp:LinkButton ID="lkbQRCode" runat="server">QR CODE</asp:LinkButton>
    </div>
    <div style="position: relative; top: -455px; left: 308px; height: 460px; width: 655px; bottom: 455px;" 
        id="divComponentes" runat=server>
    </div>
     
                     
</asp:Content>


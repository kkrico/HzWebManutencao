<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Preventivas_App.aspx.cs" Inherits="HzWebManutencao.App_Horizon.Preventivas_App" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
        Width="1100px">
        <Columns>
            <asp:BoundField DataField="cmpIdEquipoamento" HeaderText="Cod. Equipamento">
            <ItemStyle Width="10%" />
            </asp:BoundField>
            <asp:BoundField DataField="cmpDataPreventiva" HeaderText="Data Preventiva">
            <HeaderStyle Width="10%" />
            </asp:BoundField>
            <asp:BoundField DataField="cmpDescEquipamento" HeaderText="Equipamento">
            <HeaderStyle Width="30%" />
            </asp:BoundField>
            <asp:BoundField DataField="cmpDescricaoServ" HeaderText="Serviço">
            <HeaderStyle Width="30%" />
            </asp:BoundField>
            <asp:BoundField DataField="cmpNomeTecnico" HeaderText="Tecnico">
            <HeaderStyle Width="10%" />
            </asp:BoundField>
        </Columns>
    </asp:GridView>
    <asp:Button ID="btnRefresh" runat="server" Text="Refresh" Width="88px" />
</asp:Content>

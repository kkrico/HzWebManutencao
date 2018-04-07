<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="webFin_Relatorio.aspx.cs" Inherits="HzWebManutencao.Financeiro.webFin_Relatorio" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div runat=server id="divstyle" style="position:relative; width: 1000px; top: 12px; left: -54px; margin-left: 40px; min-height: 850px">
        <asp:Label ID="Label1" runat="server" Text="Grupo:" 
                style="position:absolute; top: 23px; left: 11px; height: 18px; width: 44px;">
        </asp:Label>
        <asp:ListBox ID="lstObraGrupo" runat="server" 
            style="position:absolute; top: 23px; left: 128px; width: 308px; height: 110px; bottom: -74px; right: 556px;" 
            SelectionMode="Multiple" AutoPostBack="True" 
            onselectedindexchanged="lstObraGrupo_SelectedIndexChanged">
        </asp:ListBox>

        <asp:Label ID="lblMesInicial" runat="server" Text="Mês Inicial:" 
            style="position:absolute; top: 153px; left: 11px; height: 18px; width: 90px;"></asp:Label>
        <asp:DropDownList ID="cmbMesInicial" runat="server"
            style="position:absolute; top: 153px; left: 128px; height: 18px; width: 198px;">
        </asp:DropDownList>
        <asp:TextBox ID="txtAnoInicial" runat="server"            
            style="position:absolute; top: 152px; left: 338px; height: 18px; width: 60px;">
        </asp:TextBox>

        <asp:Label ID="Label3" runat="server" Text="Mês Final:" 
            style="position:absolute; top: 183px; left: 11px; height: 18px; width: 90px;">
        </asp:Label>
        <asp:DropDownList ID="cmbMesFinal" runat="server"
            style="position:absolute; top: 183px; left: 128px; height: 18px; width: 198px;" 
            AutoPostBack="True" onselectedindexchanged="cmbMesFinal_SelectedIndexChanged">
        </asp:DropDownList>
        <asp:TextBox ID="txtAnoFinal" runat="server"            
            style="position:absolute; top: 181px; left: 338px; height: 18px; width: 60px; right: 594px;">
        </asp:TextBox>
        <asp:Button ID="btnSearch" runat="server" Text="Pesquisar" 
            style="position:absolute; top: 177px; left: 415px; width: 82px; height: 26px;" 
            onclick="btnSearch_Click">
        </asp:Button>

        <asp:CheckBoxList ID="chkRelatorio" runat="server"
            style="position:absolute; top: 153px; left: 600px; height: 18px; width: 553px; right: -153px;">
            <asp:ListItem Value="np">Notas Pendentes</asp:ListItem>
        </asp:CheckBoxList>

        </div>
</asp:Content>

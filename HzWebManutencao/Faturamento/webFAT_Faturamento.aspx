<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="webFAT_Faturamento.aspx.cs" Inherits="HzWebManutencao.Faturamento.webFAT_Faturamento" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .style1
        {
            font-size: large;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Panel ID="Panel1" runat="server" Height="420px" Width="1110px">
        <div style="height: 395px; width: 295px; position: absolute; top: 170px; left: 548px;">
            <br />
            <span class="style1"><strong>Obra</strong></span><br />
            <asp:DropDownList ID="cmbListaObras" runat="server" Height="36px" Width="282px">
            </asp:DropDownList>
            <br />
            <br />
            <br />
            <strong><span class="style1">Período</span></strong><br />
            <br />
            ANO :<br />&nbsp;
            <asp:DropDownList ID="cmbAno" runat="server" Height="16px" Width="71px">
                <asp:ListItem>2016</asp:ListItem>
                <asp:ListItem>2015</asp:ListItem>
                <asp:ListItem>2014</asp:ListItem>
            </asp:DropDownList>
            <br />
            <br />
            Mes Inicial<br />
            <asp:DropDownList ID="cmbMesInicial" runat="server" Height="17px" Width="95px">
                <asp:ListItem Value="1">Janeiro</asp:ListItem>
                <asp:ListItem Value="2">Feveriro</asp:ListItem>
                <asp:ListItem Value="3">Março</asp:ListItem>
                <asp:ListItem Value="4">Abril</asp:ListItem>
                <asp:ListItem Value="5">Maio</asp:ListItem>
                <asp:ListItem Value="6">Junho</asp:ListItem>
                <asp:ListItem Value="7">Julho</asp:ListItem>
                <asp:ListItem Value="8">Agosto</asp:ListItem>
                <asp:ListItem Value="9">Setembro</asp:ListItem>
                <asp:ListItem Value="10">Outubro</asp:ListItem>
                <asp:ListItem Value="11">Novembro</asp:ListItem>
                <asp:ListItem Value="12">Dezembro</asp:ListItem>
            </asp:DropDownList>
            <br />
            <br />
            Mes Final<br />
            <asp:DropDownList ID="cmbMesFinal" runat="server" Height="18px" Width="96px">
                            <asp:ListItem Value="1">Janeiro</asp:ListItem>
                <asp:ListItem Value="2">Feveriro</asp:ListItem>
                <asp:ListItem Value="3">Março</asp:ListItem>
                <asp:ListItem Value="4">Abril</asp:ListItem>
                <asp:ListItem Value="5">Maio</asp:ListItem>
                <asp:ListItem Value="6">Junho</asp:ListItem>
                <asp:ListItem Value="7">Julho</asp:ListItem>
                <asp:ListItem Value="8">Agosto</asp:ListItem>
                <asp:ListItem Value="9">Setembro</asp:ListItem>
                <asp:ListItem Value="10">Outubro</asp:ListItem>
                <asp:ListItem Value="11">Novembro</asp:ListItem>
                <asp:ListItem Value="12">Dezembro</asp:ListItem>
            </asp:DropDownList>
            <br />
            <br />
        </div>
        <br />
        <div style="position: absolute; top: 172px; left: 864px; width: 312px; height: 314px;">
            <div style="position: absolute">
            </div>
            <asp:Button ID="Button1" runat="server" Height="30px" Text="Contas a Receber" 
                Width="187px" onclick="Button1_Click" />
            <br />
            <br />
            <br />
            <asp:Button ID="btnContasRecebidas" runat="server" Height="30px" Text="Contas Recebidas" 
                Width="187px" onclick="btnContasRecebidas_Click" />
        </div>
    </asp:Panel>
</asp:Content>

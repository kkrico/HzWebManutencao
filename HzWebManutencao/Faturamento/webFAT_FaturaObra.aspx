<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="webFAT_FaturaObra.aspx.cs" Inherits="HzWebManutencao.Faturamento.webFAT_FaturaObra" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .style1
        {
            font-size: large;
            color: #FF3300;
        }
        .style2
        {
            font-size: medium;
            color: #000000;
        }
        .style3
        {
            color: #000000;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <p class="style1">
        Dados da Obra Fatura</p>
    <p class="style1">
        <span class="style3">Obra:</span> <asp:DropDownList ID="cmbObra" runat="server" 
            Height="16px" Width="633px" 
            onselectedindexchanged="cmbObra_SelectedIndexChanged" AutoPostBack="True">
        </asp:DropDownList>
    </p>
    <p class="style1">
        <span class="style2">Nome Obra:</span>
        <asp:TextBox ID="txtNomeObra" runat="server" Width="500px"></asp:TextBox>
    </p>
    <p class="style2">
        Nome Orgão:<asp:TextBox ID="txtNomeOrgao" runat="server" Width="495px"></asp:TextBox>
    </p>
    <p class="style2">
        Primeiro Destinatário:
        <asp:TextBox ID="txtPrimDest" runat="server" Width="435px"></asp:TextBox>
    </p>
    <p class="style2">
        Segundo Destinatário:&nbsp;
        <asp:TextBox ID="txtSegDest" runat="server" Width="433px"></asp:TextBox>
    </p>
    <p class="style2">
        <asp:Button ID="btnSalvar" runat="server" Font-Size="Medium" Text="Salvar" 
            onclick="btnSalvar_Click"  />
    </p>
</asp:Content>

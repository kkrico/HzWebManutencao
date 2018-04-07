<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="webAddDocumento.aspx.cs" Inherits="HzWebManutencao.Documentos.webAddDocumento" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div style="position:relative; height: 500px; width: 850px; top: -7px; left: -54px; margin-left: 40px;">
        <asp:Label ID="Label1" runat="server" Text="Tipo:" style="position:absolute"></asp:Label>
        <asp:DropDownList ID="cmbTipo" runat="server" 
            style="position:absolute; top: -2px; left: 74px; width: 211px;" 
            AutoPostBack="True" onselectedindexchanged="cmbTipo_SelectedIndexChanged">
        </asp:DropDownList>
        <asp:Label ID="Label2" runat="server" Text="Pessoal:" 
            style="position:absolute; top: 28px; left: 3px;"></asp:Label>
        <asp:DropDownList ID="cmbPessoal" runat="server" 
            style="position:absolute; top: 25px; left: 74px; width:211px" 
            onselectedindexchanged="cmbPessoal_SelectedIndexChanged">
        </asp:DropDownList>
        <asp:Label ID="Label3" runat="server" Text="Obra:" 
            style="position:absolute; top: 52px; left: 3px;"></asp:Label>
        <asp:DropDownList ID="cmbObra" runat="server" 
            style="position:absolute; top: 54px; left: 74px; width:211px" 
            onselectedindexchanged="cmbObra_SelectedIndexChanged">
        </asp:DropDownList>
        <asp:Label ID="Label4" runat="server" Text="Descrição:" 
            style="position:absolute; top: 81px; left: 1px;"></asp:Label>
        <asp:TextBox ID="txtDescription" runat="server" 
            style="position:absolute; top: 81px; left: 74px; width: 461px; height: 74px;" 
            Columns="5" TextMode="MultiLine"></asp:TextBox>
        <asp:Button ID="btnGenerate" runat="server" Text="Gerar" 
            style="position:absolute; top: -4px; left: 293px;" 
            onclick="btnGenerate_Click" />
        <asp:Label ID="lblFornecedor" runat="server" Text="Fornecedor:" 
            style="position:absolute; top: 172px; left: 2px;"></asp:Label>
        <asp:TextBox ID="txtFornecedor" runat="server" 
            
            style="position:absolute; top: 202px; left: 74px; width: 461px; height: 21px;" ></asp:TextBox>
        <asp:DropDownList ID="cmbFornecedor" runat="server" 
            style="position:absolute; top: 166px; left: 74px; width:211px">
            </asp:DropDownList>
        <asp:Label ID="lblNovoNumero" runat="server" 
            style="position:absolute; top: 1px; left: 379px;" Font-Bold="True" 
            Font-Size="12pt" ForeColor="#CC3300"></asp:Label>
    </div>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="webATE_OSPesquisaNOS.aspx.cs" Inherits="HzWebManutencao.ATE.webATE_OSPesquisaNOS" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div  style="position:relative; height: 121px; width: 972px; top: 5px; left: -52px; margin-left: 40px;">
        <asp:Label ID="Label1" runat="server" Text="Obra:" 
            style="position:absolute; top: 2px; left: 10px; height: 17px; width: 44px;"></asp:Label>
        <asp:DropDownList ID="cmbObra" runat="server" 
            style="position:absolute; top: 2px; left: 53px; height: 22px; width: 758px;" 
            onselectedindexchanged="cmbObra_SelectedIndexChanged"></asp:DropDownList>

        <asp:Label ID="Label3" runat="server" Text="Número da O.S (nº/ano):" ToolTip="Número da O.S. no formato Nº/ano" 
            style="position:absolute; top: 29px; left: 9px; width: 147px;"></asp:Label>
        <asp:TextBox ID="txtNuOS" runat="server" 
            
            style="position:absolute; top: 30px; left: 165px; height: 20px; width: 116px;"></asp:TextBox> 
         <asp:RequiredFieldValidator ID="rfdOrdemServico" runat="server" 
            ControlToValidate="txtNuOS" Display="None" ErrorMessage="Número da ordem de serviço em branco!"
            
            style="position:absolute; top: 67px; left: 310px; width: 187px; height: 16px;" ></asp:RequiredFieldValidator>
        <asp:Button ID="btnPesquisar" runat="server" Text="Pesquisar" 
            style="position:absolute; top: 24px; left: 305px; width: 80px; height: 28px;" 
            onclick="btnPesquisar_Click" />
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
            style="position:absolute; top: 48px; left: 544px; width: 200px; height: 26px;" 
            ShowMessageBox="True" ShowSummary="False" />
</div>

</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="webTAB_TipoAtividade.aspx.cs" Inherits="HzWebManutencao.Tabelas_Apoio.Preventiva.webTAB_TipoAtividade" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent" >
    <div style="position:relative; height: 399px; width: 975px; top: 6px; left: -54px; margin-left: 40px;">
        <asp:Label ID="Label1" runat="server" Text="Tipo de Atividade: " 
            style="position:absolute; left: 29px; top: 50px;"></asp:Label>
        <asp:ListBox ID="lstTipoAtividade" runat="server" style="position:absolute; top: 72px; left: 29px; width: 556px; height: 132px;" 
            AutoPostBack="True" 
            onselectedindexchanged="lstTipoAtividade_SelectedIndexChanged" 
            Font-Names="Calibri" Font-Size="Small"></asp:ListBox>
        <asp:Label ID="Label2" runat="server" Text="Descrição: " 
            style="position:absolute; left: 29px; top: 216px;"></asp:Label>
        <asp:TextBox ID="txtTipoAtividade" runat="server" 
            
            style="position:absolute; top: 237px; left: 29px; width: 553px; height: 42px;" 
            TextMode="MultiLine" Font-Names="Calibri" Font-Size="Small"></asp:TextBox>
        <asp:Button ID="btnSave" runat="server" Text="Gravar" 
            style="position:absolute; top: 311px; left: 107px; width: 77px; height: 28px;" 
            onclick="btnSave_Click" />
        <asp:Button ID="btnDelete" runat="server" Text="Excluir" 
            style="position:absolute; top: 311px; left: 191px; width: 77px; height: 28px;" onclick="btnDelete_Click" 
            />
        <asp:Button ID="btnNew" runat="server" Text="Novo" 
            style="position:absolute; top: 311px; left: 24px; width: 77px; height: 28px;" 
            CausesValidation="False" onclick="btnNew_Click" />
        <asp:Label ID="lblAcao" runat="server" Text="Cadastro de Tipo de Atividade"
            style="position:absolute; top: 4px; left: 6px; height: 27px; width: 921px; text-align: center;" 
            BackColor="White" Font-Bold="True" Font-Size="Large" ForeColor="Maroon" 
            Font-Names="Calibri"></asp:Label>

    </div>
</asp:Content>

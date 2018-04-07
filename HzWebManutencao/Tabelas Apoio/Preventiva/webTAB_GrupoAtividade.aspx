<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="webTAB_GrupoAtividade.aspx.cs" Inherits="HzWebManutencao.Tabelas_Apoio.Preventiva.webTAB_GrupoAtividade" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="child" style="position:relative; height: 499px; width: 932px; top: 6px; left: -54px; margin-left: 40px;">
        <asp:Label ID="lblAcao" runat="server" Text="Cadastro de Grupo Atividade"
            style="position:absolute; top: 4px; left: 6px; height: 27px; width: 921px; text-align: center;" 
            BackColor="White" Font-Bold="True" Font-Size="Large" ForeColor="Maroon" 
            Font-Names="Calibri"></asp:Label>

        <asp:Label ID="Label1" runat="server" Text="Tipo de Atividade " 
            style="position:absolute; left: 28px; top: 37px;"></asp:Label>
        <asp:DropDownList ID="cmbTipoAtividade" runat="server" 
            style="position:absolute; top: 60px; left: 28px; width: 302px; height: 25px;" 
            AutoPostBack="True" 
            onselectedindexchanged="cmbTipoAtividade_SelectedIndexChanged" 
            Font-Names="Calibri" Font-Size="Small">
        </asp:DropDownList>
        <asp:ListBox ID="lstGrupoAtividade" runat="server" 
            style="position:absolute; top: 106px; left: 27px; width: 509px; height: 103px;" 
            AutoPostBack="True" 
            onselectedindexchanged="lstGrupoAtividade_SelectedIndexChanged" 
            Font-Names="Calibri" Font-Size="Small" 
            ></asp:ListBox>
        <asp:TextBox ID="txtGrupoAtividade" runat="server" 
            
            style="position:absolute; top: 233px; left: 29px; width: 499px; height: 74px;" 
            TextMode="MultiLine" Font-Names="Calibri" Font-Size="Small"></asp:TextBox>
        <asp:Button ID="btnNew" runat="server" Text="Novo" 
            style="position:absolute; top: 376px; left: 29px; width: 71px; height: 28px;" 
            CausesValidation="False" onclick="btnNew_Click" />
        <asp:Button ID="btnSave" runat="server" Text="Gravar" 
            style="position:absolute; top: 376px; left: 107px; width: 71px; height: 28px;" 
            onclick="btnSave_Click" />
        <asp:Button ID="btnDelete" runat="server" Text="Excluir" 
            
            style="position:absolute; top: 376px; left: 185px; width: 71px; height: 28px;" onclick="btnDelete_Click" 
            />
        <asp:Label ID="Label3" runat="server" Text="Grupo de Atividade" 
            style="position:absolute; left: 28px; top: 86px;"></asp:Label>
        <asp:Label ID="Label2" runat="server" Text="Tipo" 
            style="position:absolute; left: 32px; top: 316px; height: 18px;"></asp:Label>
        <asp:RadioButtonList ID="rdbTpGrupoAtividade" runat="server" 
            style="position:absolute; top: 332px; left: 36px; width: 288px; height: 29px;" 
            RepeatDirection="Horizontal" Font-Names="Calibri" Font-Size="Medium">
            <asp:ListItem Value="P">Predial</asp:ListItem>
            <asp:ListItem Value="E">Equipamentos</asp:ListItem>
            <asp:ListItem Selected="True" Value="T">Todos</asp:ListItem>
        </asp:RadioButtonList> 
        <asp:Label ID="Label4" runat="server" Text="Descrição" 
            style="position:absolute; left: 30px; top: 212px; height: 18px;"></asp:Label>
    </div>
</asp:Content>


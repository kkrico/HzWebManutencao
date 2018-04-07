<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="webTAB_ItemAtividade.aspx.cs" Inherits="HzWebManutencao.Tabelas_Apoio.Preventiva.webTAB_ItemAtividade" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div style="position:relative; height: 499px; width: 1102px; top: 6px; left: -54px; margin-left: 40px;">
        <asp:Label ID="Label3" runat="server" Text="Itens de Atividade: " 
            
            style="position:absolute; left: 28px; top: 106px; bottom: 376px; width: 119px;"></asp:Label>
        <asp:Label ID="Label2" runat="server" Text="Descrição: "  
            style="position:absolute; left: 28px; top: 278px; height: 17px;"></asp:Label>
        <asp:Label ID="Label4" runat="server" Text="Grupo de Atividade: " 
            style="position:absolute; left: 400px; top: 53px;"></asp:Label>
        <asp:Button ID="btnNew" runat="server" Text="Novo" 
            style="position:absolute; top: 413px; left: 22px; width: 77px; height: 28px;" 
            CausesValidation="False" onclick="btnNew_Click" />
        <asp:Button ID="btnSave" runat="server" Text="Gravar " 
            style="position:absolute; top: 414px; left: 106px; width: 77px; height: 28px;" 
            onclick="btnSave_Click" />
        <asp:Button ID="btnDelete" runat="server" Text="Excluir" 
            
            style="position:absolute; top: 413px; left: 190px; width: 77px; right: 835px; height: 28px;" 
            onclick="btnDelete_Click"/>
        <asp:DropDownList ID="cmbTipoAtividade" runat="server" 
            style="position:absolute; top: 75px; left: 27px; width: 352px; height: 25px;" 
            AutoPostBack="True" 
            onselectedindexchanged="cmbTipoAtividade_SelectedIndexChanged" 
            Font-Names="Calibri" Font-Size="Small">
        </asp:DropDownList>
        <asp:DropDownList ID="cmbGrupoAtividade" runat="server" 
            style="position:absolute; top: 75px; left: 400px; width: 433px; height: 25px;" 
            AutoPostBack="True" 
            OnSelectedIndexChanged="cmbGrupoAtividade_SelectedIndexChanged" 
            Font-Names="Calibri" Font-Size="Small">
        </asp:DropDownList>
        <asp:ListBox ID="lstItemAtividade" runat="server" 
            style="position:absolute; top: 127px; left: 25px; width: 806px; height: 140px;" 
            AutoPostBack="True" 
            onselectedindexchanged="lstItemAtividade_SelectedIndexChanged" 
            Font-Names="Calibri" Font-Size="Small"></asp:ListBox>
        <asp:TextBox ID="txtItemAtividade" runat="server" 
            
            style="position:absolute; top: 297px; left: 28px; width: 798px; height: 88px;" 
            Font-Names="Calibri" Font-Size="Small" TextMode="MultiLine"></asp:TextBox>
        <asp:Label ID="Label1" runat="server" Text="Tipo de Atividade: " 
            style="position:absolute; left: 29px; top: 53px;"></asp:Label>
        <asp:Label ID="lblAcao" runat="server" Text="Cadastro de Itens de Atividade"
            style="position:absolute; top: 4px; left: 6px; height: 27px; width: 921px; text-align: center;" 
            BackColor="White" Font-Bold="True" Font-Size="Large" ForeColor="Maroon" 
            Font-Names="Calibri"></asp:Label>

        <asp:Panel ID="pnlValores" runat="server"  
            style="position:absolute; top: 297px; left: 840px; height: 90px; width: 239px;" 
            BorderStyle="Solid">

              <asp:Label ID="Label8" runat="server" Text="Unidade"  
                style="position:absolute; top: 0px; left: 12px;"></asp:Label>
              <asp:DropDownList ID="cmbUnidade" runat="server"  
                  style="position:absolute; top: 19px; left: 8px;" 
                  onselectedindexchanged="cmbUnidade_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:Label ID="Label5" runat="server" Text="Valor Mínimo"  
                style="position:absolute; top: 42px; left: 12px;"></asp:Label>
            <asp:Label ID="Label6" runat="server" Text="Valor Máximo"  
                style="position:absolute; top: 41px; left: 163px;"></asp:Label>
            <asp:TextBox ID="txtValorMaximo" runat="server"  
                style="position:absolute; top: 59px; left: 161px; width: 44px;" ></asp:TextBox>
            <asp:Label ID="Label7" runat="server" Text="Valor Médio"  
                style="position:absolute; top: 42px; left: 83px;" ></asp:Label>
            <asp:TextBox ID="txtValorMedio" runat="server"  
                style="position:absolute; top: 62px; left: 80px; width: 52px;"></asp:TextBox>
            <asp:TextBox ID="txtValorMinimo" runat="server" 
                style="position:absolute; top: 61px; left: 11px; width: 45px;"></asp:TextBox>
        </asp:Panel>

        <asp:RadioButton ID="rblCheck" runat="server" 
            style="position:absolute; top: 275px; left: 843px;" Text="Check" 
            AutoPostBack="True" oncheckedchanged="rblCheck_CheckedChanged"/>
        <asp:RadioButton ID="rblValor" runat="server" 
            style="position:absolute; top: 275px; left: 911px;" Text="Valor" 
            AutoPostBack="True" oncheckedchanged="rblValor_CheckedChanged"/>
    </div>
</asp:Content>


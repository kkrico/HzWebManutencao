<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="webMTO_Graficos.aspx.cs" Inherits="HzWebManutencao.Manto.webMTO_Graficos" %>
<%@ Register src="~/Controles/Mensagem.ascx" tagname="Mensagem" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<asp:Mensagem ID="CaixaMensagem" runat="server" />

<div style="position:relative; height: 444px; width: 988px; top: 12px; left: -54px; margin-left: 40px;">
     <asp:Label ID="Label1" runat="server" Text="Gráficos Hidráulica e Elétrico" 
                style="position:absolute; top: 0px; left: 11px; height: 22px; width: 964px;" 
                BackColor="White" Font-Bold="True" Font-Size="Medium" ForeColor="Maroon"></asp:Label>
    
    <asp:Label ID="lblAcao" runat="server" Text="Gráficos Hidráulica"
                style="position:absolute; top: 109px; left: 91px; height: 22px; width: 302px;" 
                BackColor="White" Font-Bold="True" Font-Size="Medium" 
         ForeColor="Maroon"></asp:Label>

    <asp:Label ID="Label5" runat="server" Text="Obra:" 
        style="position:absolute; top: 53px; left: 91px; height: 18px; width: 44px;"></asp:Label>
    <asp:DropDownList ID="cmbObra" runat="server" AutoPostBack="True" 
        style="position:absolute; top: 52px; left: 136px; width: 419px; height: 24px; bottom: 402px; right: 425px;"
        onselectedindexchanged="cmbObra_SelectedIndexChanged"></asp:DropDownList>

    <asp:RadioButtonList ID="rdbHidra" runat="server" 
         style="position:absolute; top: 138px; left: 86px; width: 450px; height: 35px; right: 452px;" AutoPostBack="True" 
        onselectedindexchanged="rdbHidra_SelectedIndexChanged" RepeatColumns="2" 
        RepeatDirection="Horizontal">
        <asp:ListItem  Selected ="True" Value="CAD">Consumo diário de água</asp:ListItem>
        <asp:ListItem Value="CAM">Consumo médio mensal de água</asp:ListItem>
    </asp:RadioButtonList>

    <asp:Label ID="Label2" runat="server" Text="Gráficos Elétrico"
                style="position:absolute; top: 183px; left: 91px; height: 22px; width: 302px;" 
                BackColor="White" Font-Bold="True" Font-Size="Medium" 
         ForeColor="Maroon"></asp:Label>

    <asp:RadioButtonList ID="rdbEletrico" runat="server" 
         style="position:absolute; top: 209px; left: 86px; width: 687px; height: 113px; right: 214px;" AutoPostBack="True" 
        onselectedindexchanged="rdbEletrico_SelectedIndexChanged" RepeatColumns="2" 
        RepeatDirection="Horizontal">
        <asp:ListItem Value="CAQG">Consumo absoluto ativo do quadro geral</asp:ListItem>
        <asp:ListItem Value="CDAQG">Consumo de energia ativo diário do QG</asp:ListItem>
        <asp:ListItem Value="CMAQG">Consumo médio diário de energia ativa mensal</asp:ListItem>
        <asp:ListItem Value="CARQG">Consumo absoluto reativo mensal</asp:ListItem>
        <asp:ListItem Value="CDRQG">Consumo de energia reativa diária do quadro geral</asp:ListItem>
        <asp:ListItem Value="CMRQG">Consumo médio de energia reativa mensal</asp:ListItem>
    </asp:RadioButtonList>

    <asp:Label ID="Label4" runat="server" Text="Data Inicial:" 
         style="position:absolute; top: 350px; left: 86px; width: 72px; height: 19px;">
    </asp:Label>

    <asp:DropDownList ID="cmbDtInicial" runat="server" AutoPostBack="True"
         
         style="position:absolute; top: 350px; left: 186px; left: 161px; width: 139px; height: 24px;" 
         onselectedindexchanged="cmbDtInicial_SelectedIndexChanged" ></asp:DropDownList>

    <asp:Label ID="Label3" runat="server" Text="/" 
         
         style="position:absolute; top: 350px; left: 305px; width: 10px; height: 19px;" 
         Font-Bold="True"></asp:Label>

    <asp:TextBox ID="txtAnoInicial" runat="server" AutoPostBack="True"
         
         style="position:absolute; top: 350px; left: 319px; width: 80px; height: 22px;" 
         ontextchanged="txtAnoInicial_TextChanged"></asp:TextBox> 

    <asp:Label ID="lblDtFinal" runat="server" Text="Data Final:" visible="false"
         style="position:absolute; top: 380px; left: 86px; width: 72px; height: 19px;" >
    </asp:Label>

    <asp:DropDownList ID="cmbDtFinal" runat="server" visible="false" AutoPostBack="True"
         
         style="position:absolute; top: 380px; left: 186px; left: 160px; width: 139px; height: 24px;" 
         onselectedindexchanged="cmbDtFinal_SelectedIndexChanged"></asp:DropDownList>

    <asp:Label ID="Label6" runat="server" Text="/" visible="false"
         style="position:absolute; top: 380px; left: 305px; width: 10px; height: 19px;" Font-Bold="True"></asp:Label>

    <asp:TextBox ID="TxtAnoFinal" runat="server" visible="false" AutoPostBack="True"
         
         style="position:absolute; top: 380px; left: 319px; width: 80px; height: 22px;" 
         ontextchanged="TxtAnoFinal_TextChanged"></asp:TextBox> 

    <asp:Button ID="btnGerar" runat="server" Text="Gerar" 
        style="position:absolute; top: 350px; left: 412px; width: 82px; height: 26px;" 
        onclick="btnGerar_Click"/>

    <asp:Button ID="btnGerar0" runat="server" Text="Gerar" 
        style="position:absolute; top: 381px; left: 412px; width: 82px; height: 26px;" 
        Visible="False"/>

</div>
</asp:Content>

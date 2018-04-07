<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="webCON_UsuCadastro.aspx.cs" Inherits="HzWebManutencao.Configuracao.webCON_UsuCadastro" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="AjaxControl"%>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
 <div  style="position:relative; height: 433px; width: 1153px; top: 12px; left: -54px; margin-left: 40px;">

    <asp:Label ID="lblUsuario" runat="server" Text="Nome do Usuário:" 
         style="position:absolute; top: 10px; left: 11px; height: 18px; width: 109px;">
    </asp:Label>
    <asp:TextBox ID="txtUsuario" runat="server" 
        style="position:absolute; top: 10px; left: 128px; width: 340px;">
    </asp:TextBox>

    <asp:Label ID="lblEmail" runat="server" Text="Email:" 
        style="position:absolute; top: 11px; left: 486px; height: 19px;">
    </asp:Label>
    <asp:TextBox ID="txtEmail" runat="server" 
        style="position:absolute; top: 11px; left: 549px; width: 340px;">
    </asp:TextBox>

    <asp:Label ID="lblPerfil" runat="server" Text="Perfil:" 
        style="position:absolute; top: 46px; left: 15px; height: 19px;">
    </asp:Label>
    <asp:DropDownList ID="cmbPerfil" runat="server" 
         style="position:absolute; top: 46px; left: 128px; width: 340px; height: 23px; bottom: 633px; right: 444px;">
    </asp:DropDownList>

    <asp:Label ID="Label1" runat="server" Text="Senha:" 
        style="position:absolute; top: 45px; left: 486px; height: 19px;">
    </asp:Label>
    <asp:TextBox ID="txtPwd" runat="server"
        
         
         style="position:absolute; top: 47px; left: 549px; height: 19px; width: 287px;" 
         TextMode="Password"></asp:TextBox>
    
    <asp:Label ID="lblAtivo" runat="server" Text="Status:" 
    style="position:absolute; top: 46px; left: 860px;"></asp:Label>
     <asp:CheckBox ID="chkAtivo" runat="server"
        style="position:absolute; top: 46px; left: 906px; width: 62px;" 
         Text="Ativo" />

    <asp:Label ID="lblVincula" runat="server" Text="Vincular Usuário a Obra"
        style="position:absolute; top: 83px; left: 16px; height: 21px; width: 1124px; text-align: center;" 
        BackColor="White" Font-Bold="True" Font-Size="Medium" ForeColor="Maroon"></asp:Label>

    <asp:Label ID="LblObras" runat="server" Text="Obras Vinculada"
        style="position:absolute; top: 105px; left: 613px; height: 21px; width: 133px; right: 384px;" 
        BackColor="White" Font-Bold="True" Font-Size="Small" ForeColor="Maroon"></asp:Label>
     <asp:ListBox ID="lstObra" runat="server"
         style="position:absolute; top: 128px; left: 14px; width: 532px; height: 231px;" 
         SelectionMode="Multiple">
     </asp:ListBox>

    <asp:Button ID="btnVincular" runat="server" Text=">>" 
         style="position:absolute; top: 188px; left: 556px; width: 50px; height: 34px;" 
         onclick="btnVincular_Click" CausesValidation="False"/>

    <asp:Button ID="btnDesvincular" runat="server" Text="&lt;&lt;" 
         style="position:absolute; top: 252px; left: 557px; width: 49px; height: 34px;" 
         onclick="btnDesvincular_Click" CausesValidation="False"/>

    <asp:Label ID="LblObras0" runat="server" Text="Obras"
        style="position:absolute; top: 105px; left: 13px; height: 21px; width: 64px; right: 1076px;" 
        BackColor="White" Font-Bold="True" Font-Size="Small" ForeColor="Maroon"></asp:Label>

    <asp:Button ID="btnSave" runat="server" Text="Gravar" 
         style="position:absolute; top: 379px; left: 18px; width: 60px; height: 26px;" 
         onclick="btnSave_Click"/>

    <asp:Button ID="btnNovo" runat="server" Text="Novo Usuário" 
         style="position:absolute; top: 378px; left: 88px; width: 98px; height: 26px;" 
         onclick="btnNovo_Click" CausesValidation="False"/>

     <asp:ValidationSummary ID="ValidationSummary1" runat="server"
     style="position:absolute; top: 365px; left: 582px; width: 262px;" 
         ShowMessageBox="True" ShowSummary="False"/>

     <asp:RequiredFieldValidator ID="rfvEmail" runat="server" 
         ErrorMessage="Email em branco!"  
         style="position:absolute; top: 98px; left: 882px; width: 262px;" 
         ControlToValidate="txtEmail" Display="None"></asp:RequiredFieldValidator>

     <asp:RequiredFieldValidator ID="rfvUsuario" runat="server" 
         ErrorMessage="Nome do usuário em branco!"  
         style="position:absolute; top: 79px; left: 873px; width: 262px;" 
         ControlToValidate="txtUsuario" Display="None"></asp:RequiredFieldValidator>

     <asp:RequiredFieldValidator ID="rfvPerfil" runat="server" 
         ErrorMessage="Perfil em branco!"  
         style="position:absolute; top: 75px; left: 752px; width: 262px;" 
         ControlToValidate="cmbPerfil" Display="None"></asp:RequiredFieldValidator>

     <asp:RequiredFieldValidator ID="rfvSenha" runat="server" 
         ErrorMessage="Senha em branco!"  
         style="position:absolute; top: 395px; left: 758px; width: 262px;" 
         ControlToValidate="txtPwd" Display="None"></asp:RequiredFieldValidator>

    <asp:Button ID="btnVoltar" runat="server" Text="Voltar" 
         style="position:absolute; top: 379px; left: 195px; width: 60px; height: 26px;" 
         onclick="btnVoltar_Click"/>

     <asp:ListBox ID="lstObrVinculada" runat="server"
         style="position:absolute; top: 128px; left: 613px; width: 532px; height: 231px;" 
         SelectionMode="Multiple">
     </asp:ListBox>

 </div>
</asp:Content>

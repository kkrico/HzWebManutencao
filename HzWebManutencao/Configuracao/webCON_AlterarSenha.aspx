<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="webCON_AlterarSenha.aspx.cs" Inherits="HzWebManutencao.Configuracao.webCON_AlterarSenha" %>
<%@ Register src="~/UserControls/OKMessageBox.ascx" tagname="MsgBox" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:MsgBox ID="MsgBox" runat="server" />
    <div  class="accountInfo" style="position:relative; height: 393px; width: 1128px; top: 12px; left: -65px; margin-left: 80px;">
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="failureNotification"
            style="position:absolute; top: 20px; left: 393px; width: 363px; height: 33px;" 
            ShowMessageBox="True" ShowSummary="False" />
        <h2>
            ALTERAR SENHA
        </h2>

        <fieldset class="changePassword"> <legend>Informações da Conta</legend>
            <p>
                <asp:Label ID="CurrentPasswordLabel" runat="server" 
                    AssociatedControlID="CurrentPassword" Width="319px">Senha Atual: </asp:Label>
                <asp:TextBox ID="CurrentPassword" runat="server" CssClass="passwordEntry" TextMode="Password" TabIndex="0"></asp:TextBox>
                <asp:RequiredFieldValidator ID="CurrentPasswordRequired" runat="server" 
                    ControlToValidate="CurrentPassword" 
                    CssClass="failureNotification" ErrorMessage="Senha atual em branco!" ToolTip="Informe a senha atual.">*</asp:RequiredFieldValidator>
            </p>
            <p>
                <asp:Label ID="NewPasswordLabel" runat="server" 
                    AssociatedControlID="NewPassword" Width="321px">Nova Senha: </asp:Label>
                <asp:TextBox ID="NewPassword" runat="server" CssClass="passwordEntry" TextMode="Password" TabIndex="1"></asp:TextBox>
                <asp:RequiredFieldValidator ID="NewPasswordRequired" runat="server" 
                    ControlToValidate="NewPassword" 
                    CssClass="failureNotification" ErrorMessage="Nova senha em branco!" ToolTip="Informe a nova senha.">*</asp:RequiredFieldValidator>
            </p>
            <p>
                <asp:Label ID="ConfirmNewPasswordLabel" runat="server" 
                    AssociatedControlID="ConfirmNewPassword" Width="323px">Confirma Nova Senha: </asp:Label>
                <asp:TextBox ID="ConfirmNewPassword" runat="server" CssClass="passwordEntry" TextMode="Password" TabIndex="2"></asp:TextBox>
                <asp:RequiredFieldValidator ID="ConfirmNewPasswordRequired" runat="server" 
                        ControlToValidate="ConfirmNewPassword" 
                        CssClass="failureNotification" Display="None" ErrorMessage="Confirma nova senha em branco!"
                        ToolTip="Confirme a nova senha.">*</asp:RequiredFieldValidator>
                <asp:CompareValidator ID="NewPasswordCompare" runat="server" 
                    ControlToCompare="NewPassword" ControlToValidate="ConfirmNewPassword" 
                    CssClass="failureNotification" Display="None" ErrorMessage="A senha confirmada está diferente da nova senha.">*</asp:CompareValidator>
            </p>

        </fieldset>

        <asp:Button ID="btnSave" runat="server" Text="Alterar Senha" Font-Size="X-Small" 
        Font-Bold="True" style="position:absolute; top: 230px; left: 17px; width: 101px; height: 22px;" onclick="btnSave_Click" TabIndex="13"   />
 
   </div>
</asp:Content>

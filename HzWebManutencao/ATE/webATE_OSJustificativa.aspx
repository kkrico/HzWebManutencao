<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="webATE_OSJustificativa.aspx.cs" Inherits="HzWebManutencao.ATE.webATE_OSAprovacao" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div  style="position:relative; height: 278px; width: 850px; top: 12px; left: -54px; margin-left: 40px;">
    <asp:TextBox ID="txtObra" runat="server"
        
        
        style="position:absolute; top: 34px; left: 84px; width: 730px;" ></asp:TextBox>

            <asp:Label ID="lblAcao" runat="server" Text="lblAcao"
                style="position:absolute; top: 3px; left: 54px; height: 20px; width: 672px;" 
                BackColor="White" Font-Bold="True" Font-Size="Medium" 
        ForeColor="Maroon"></asp:Label>

    <asp:Label ID="Label1" runat="server" Text="Justificativa:" 
        style="position:absolute; top: 73px; left: 7px;">
    </asp:Label>
    <asp:TextBox ID="txtJustificativa" runat="server" 
        style="position:absolute; top: 73px; left: 82px; width: 725px; height: 75px;" 
        TextMode="MultiLine"></asp:TextBox>

    <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
        style="position:absolute; top: 176px; left: 470px; width: 263px; height: 40px;" 
            ShowMessageBox="True" ShowSummary="False" />

    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
        style="position:absolute; top: 151px; left: 492px; width: 263px; height: 26px;" 
        ControlToValidate="txtJustificativa" Display="None" 
        ErrorMessage="É obrigatório o preenchimento do campo Justificativa!"></asp:RequiredFieldValidator>

    <asp:Button ID="btnExecutar" runat="server" Text="Gravar" 
        style="position:absolute; top: 178px; left: 12px; width: 60px; height: 26px;" 
        onclick="btnExecutar_Click" />
    <asp:Button ID="BtnVoltar" runat="server" Text="Voltar" 
        style="position:absolute; top: 178px; left:79px; height: 26px; width: 71px;" 
        CausesValidation="False" onclick="BtnVoltar_Click"/>


            <asp:Label ID="Label2" runat="server" Text="Obra:" 
                
                
                
        style="position:absolute; top: 34px; left: 11px; height: 18px; width: 44px;">
            </asp:Label>
        
   </div>

</asp:Content>

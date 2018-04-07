<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="webATE_OSAprovacao.aspx.cs" Inherits="HzWebManutencao.ATE.webATE_OSAprovacao" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div  style="position:relative; height: 200px; width: 850px; top: 12px; left: -54px; margin-left: 40px;">
    <asp:Label ID="Label1" runat="server" Text="Justificativa:" 
        style="position:absolute; top: 45px; left: 7px;">
    </asp:Label>
    <asp:TextBox ID="txtJustificativa" runat="server" 
        style="position:absolute; top: 46px; left: 82px; width: 263px; height: 75px;" 
        TextMode="MultiLine"></asp:TextBox>

    <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
        style="position:absolute; top: 73px; left: 398px; width: 263px; height: 40px;" 
            ShowMessageBox="True" ShowSummary="False" />

    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
        style="position:absolute; top: 48px; left: 420px; width: 263px; height: 26px;" 
        ControlToValidate="txtJustificativa" Display="None" 
        ErrorMessage="RequiredFieldValidator">
    </asp:RequiredFieldValidator>

    <asp:Button ID="btnExecutar" runat="server" Text="Gravar" 
        style="position:absolute; top: 138px; left: 12px;" 
        onclick="btnExecutar_Click" />

   </div>

</asp:Content>

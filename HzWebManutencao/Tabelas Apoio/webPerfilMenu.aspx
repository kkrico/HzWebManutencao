<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="webPerfilMenu.aspx.cs" Inherits="HzWebManutencao.Tabelas_Apoio.webPerfilMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Panel ID="Panel1" runat="server" Height="295px" style="margin-left: 0px" 
        Width="1153px">
        <asp:TreeView ID="TreeView1" runat="server" Height="157px" Width="224px" 
            ImageSet="Events" MaxDataBindDepth="10">
            <HoverNodeStyle Font-Underline="False" ForeColor="Red" />
            <NodeStyle Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" 
                HorizontalPadding="5px" NodeSpacing="0px" VerticalPadding="0px" />
            <ParentNodeStyle Font-Bold="False" />
            <SelectedNodeStyle Font-Underline="True" HorizontalPadding="0px" 
                VerticalPadding="0px" />
        </asp:TreeView>
    </asp:Panel>
</asp:Content>

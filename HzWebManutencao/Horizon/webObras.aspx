<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="webObras.aspx.cs" Inherits="HzWebManutencao.Horizon.webObras" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div style="position:relative; height: 400px; width: 500px; top: 0px; left: 0px; margin-left: 0px;" 
        id="divTreeview">
    <asp:DropDownList ID="cmbObras" runat="server" 
        style="position:relative; height: 16px; width: 316px; top: 0px; left: 0px; margin-left: 0px;" >
    </asp:DropDownList>
                    <asp:TreeView ID="TreeView1" runat="server" Height="369px" Width="426px" 
            onselectednodechanged="TreeView1_SelectedNodeChanged" 
    ShowLines="True">
            </asp:TreeView>
</div>
<div style="position:relative; height:400px; width: 500px; top: -396px; left: 510px; margin-left: 0px;" 
        id="divDados">

</div>
</asp:Content>

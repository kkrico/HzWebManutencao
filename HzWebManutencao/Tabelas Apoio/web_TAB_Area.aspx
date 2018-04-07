<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="web_TAB_Area.aspx.cs" Inherits="HzWebManutencao.Tabelas_Apoio.web_TAB_Area" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div>

        <asp:Label ID="Label2" runat="server" Text="Obra"></asp:Label>
        <br />
        <asp:DropDownList ID="DropDownList1" runat="server" Height="17px" Width="263px">
        </asp:DropDownList>
        <br />
        <br />
         <asp:Label ID="Label1" runat="server" Text="Pavimento"></asp:Label>
        <br />
        <asp:DropDownList ID="DropDownList2" runat="server" Height="17px" Width="263px">
        </asp:DropDownList>
        <br />
        <br />
        <asp:Label ID="Label3" runat="server" Text="Descrição"></asp:Label>
        
        <asp:TextBox ID="TextBox1" runat="server" Width="210px"></asp:TextBox>
    </div>
</asp:Content>

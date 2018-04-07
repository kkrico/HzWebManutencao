<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="webTAB_ObraPavimentos.aspx.cs" Inherits="HzWebManutencao.Tabelas_Apoio.webTAB_ObraPavimentos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div style="width: 1124px; height: 202px">
        <asp:Label ID="Label1" runat="server" Text="Obra"></asp:Label>
        <br />
        <asp:DropDownList ID="DropDownList1" runat="server" Height="16px" Width="225px">
        </asp:DropDownList>
         <br />
        <asp:Button ID="Button1" runat="server" Text="Novo Pavimento" />
        <br/>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
            Height="129px" Width="330px">
            <Columns>
                <asp:TemplateField HeaderText="Operação" ShowHeader="False">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server">Editar</asp:LinkButton>
                        &nbsp;<asp:LinkButton ID="LinkButton2" runat="server">Excluir</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="Pavimento" />
            </Columns>
        </asp:GridView>
        <div style="position: absolute; top: 191px; left: 414px; width: 753px; height: 164px;">
            <asp:Button ID="Button2" runat="server" Text="Nova Area / Ativo" 
                Width="132px" />
            <br />
            <br />
            <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" 
                Height="107px" style="margin-top: 0px" Width="382px">
                <Columns>
                    <asp:BoundField HeaderText="Área / Ativo" />
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>

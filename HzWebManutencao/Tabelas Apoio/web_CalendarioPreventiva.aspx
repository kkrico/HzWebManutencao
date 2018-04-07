<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="web_CalendarioPreventiva.aspx.cs" Inherits="HzWebManutencao.Tabelas_Apoio.web_CalendarioPreventiva" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div style="height: 499px; width: 1074px;">
<div style="height: 61px; width: 1072px;">
    <asp:Image ID="Image1" runat="server" 
        
        
        
        
        style="position:absolute; top: 173px; left: 100px; height: 45px; width: 143px;" 
        ImageUrl="~/App_Themes/logoftransp.png" />

        <asp:Label ID="lbljaneiro" runat="server" Text="Janeiro" 
        
        style="position:absolute; top: 183px; left: 599px; height: 34px; width: 65px;"></asp:Label>

    <asp:Button ID="btnretornar" runat="server" Height="40px" Text="&lt;&lt;" 
        style="position:absolute; top: 193px; left: 786px; height: 30px; width: 37px; right: 394px;" 
                CausesValidation="false" onclick="btn_Proximo_Click"/>

                <asp:Button ID="btnproximo" runat="server" Text="&gt;&gt;" 
                style="position:absolute; top: 193px; left: 837px; height: 30px; width: 37px;" 
                CausesValidation="false" onclick="btnPesquisa_Click" />
</div>
<div style="height: 315px; width: 1072px;">

    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
        CellPadding="4" ForeColor="#333333" GridLines="None" 
        onselectedindexchanged="GridView1_SelectedIndexChanged" Width="720px">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:BoundField DataField="Domingo" HeaderText="Domingo">
            <HeaderStyle Width="100px" />
            </asp:BoundField>
            <asp:BoundField DataField="Segunda" HeaderText="Segunda" />
            <asp:BoundField DataField="Terça" HeaderText="Terça" />
            <asp:BoundField DataField="Quarta" HeaderText="Quarta" />
            <asp:BoundField DataField="Quinta" HeaderText="Quinta" />
            <asp:BoundField DataField="Sexta" HeaderText="Sexta">
            <HeaderStyle Width="100px" />
            </asp:BoundField>
            <asp:BoundField DataField="Sabado" HeaderText="Sabado" />
        </Columns>
        <EditRowStyle BackColor="#2461BF" />
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#EFF3FB" Height="100px" />
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        <SortedAscendingCellStyle BackColor="#F5F7FB" />
        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
        <SortedDescendingCellStyle BackColor="#E9EBEF" />
        <SortedDescendingHeaderStyle BackColor="#4870BE" />
    </asp:GridView>

</div>
</div>
</asp:Content>

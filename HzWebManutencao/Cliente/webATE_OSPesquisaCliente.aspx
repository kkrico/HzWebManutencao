<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="webATE_OSPesquisaCliente.aspx.cs" Inherits="HzWebManutencao.ATE.webATE_OSPesquisa" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div  style="position:relative; height: 469px; width: 850px; top: 12px; left: -54px; margin-left: 40px;">

    <asp:Button ID="btnExecutar" runat="server" Text="Executar" 
            style="position:absolute; top: 349px; left: 24px; width: 70px; height: 26px;" 
            onclick="btnPesquisar_Click" />

    <asp:GridView ID="grdOS" runat="server" 
        style="position:absolute; top: 62px; left: 13px;" AllowPaging="True" 
        AllowSorting="True" AutoGenerateColumns="False" 
        onpageindexchanging="grdOS_PageIndexChanging" 
        onrowcommand="grdOS_RowCommand">
        <Columns>
            <asp:CheckBoxField HeaderText="Aprovar" />
            <asp:CheckBoxField HeaderText="Rejeitar" />
            <asp:TemplateField HeaderText="Ordem Serviço">
            <ItemStyle Width="80px" HorizontalAlign="Left"/>
                <ItemTemplate>
                    <asp:LinkButton ID="lnk" runat="server" 
                    CommandArgument='<%# Eval("cmpIdOS") %>' CommandName="lnk"><%#Eval("cmpNuOS") %></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:BoundField DataField="cmpDtAbertura" HeaderText="Data Abertura" 
                DataFormatString="{0:dd/MM/yyyy hh:mm}">
                <ItemStyle Width="120px" HorizontalAlign="Center" />
            </asp:BoundField>

            <asp:BoundField DataField="cmpDcLocal" HeaderText="Local">
                <ItemStyle Width="150px" HorizontalAlign="Left" />
            </asp:BoundField>

            <asp:BoundField DataField="cmpNoSolicitante" HeaderText="Solicitante">
                <ItemStyle Width="150px" HorizontalAlign="Left" />
            </asp:BoundField>

        </Columns>
    </asp:GridView>

    <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
        style="position:absolute; top: 353px; left: 416px; width: 200px; height: 26px;"
        ShowMessageBox="True" ShowSummary="False" />
        
        <asp:Label ID="lblStatus" runat="server" Text="Ordens de Serviços para Aprovação"
            style="position:absolute; top: 14px; left: 170px; height: 23px; width: 340px; right: 340px;" 
            BackColor="White" Font-Bold="True" Font-Size="Medium" 
        ForeColor="Maroon"></asp:Label>

</div>
</asp:Content>

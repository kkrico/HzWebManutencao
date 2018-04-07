<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="webPRE_FormularioPreventiva.aspx.cs" Inherits="HzWebManutencao.Preventiva.webPRE_FormularioPreventiva" %>
<%@ Register Src="~/Controles/Mensagem.ascx" TagName="Mensagem" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Mensagem ID="CaixaMensagem" runat="server" />
    <div style="position:relative; height: 499px; width: 980px; top: 6px; left: -54px; margin-left: 40px;">
        <asp:Label ID="lblAcao" runat="server" Text="Formulário da Manutenção Preventiva"
            style="position:absolute; top: 4px; left: 6px; height: 27px; width: 968px; text-align: center;" 
            BackColor="White" Font-Bold="True" Font-Size="Large" ForeColor="Maroon" 
            Font-Names="Calibri"></asp:Label>

        <asp:Label ID="Label5" runat="server" Text="Obra:" 
            style="position:absolute; top: 44px; left: 12px; height: 17px; width: 44px;"> </asp:Label>
        <asp:DropDownList ID="cmbObra" runat="server" 
            style="position:absolute; top: 63px; left: 13px; width: 320px; height: 22px;"
            AutoPostBack="True" 
        onselectedindexchanged="cmbObra_SelectedIndexChanged" Font-Names="Calibri" 
            Font-Size="Small" >
        </asp:DropDownList>

        <asp:Label ID="Label4" runat="server" Text="Tipo de Atividade: " 
            style="position:absolute; left: 345px; top: 44px"></asp:Label>
        <asp:DropDownList ID="cmbTipoAtividade" runat="server" 
            style="position:absolute; top: 63px; left: 344px; width: 194px; right: 434px; height: 22px;" 
            AutoPostBack="True" 
            TabIndex="3" Font-Names="Calibri" Font-Size="Small" 
            onselectedindexchanged="cmbTipoAtividade_SelectedIndexChanged" >
        </asp:DropDownList>

        <asp:Label ID="Label1" runat="server" Text="Periodicidade: " 
            style="position:absolute; left: 549px; top: 44px"></asp:Label>
        <asp:DropDownList ID="cmbPeriodicidade" runat="server" 
            style="position:absolute; top: 63px; left: 547px; width: 148px; right: 285px; height: 22px;" 
            AutoPostBack="True" 
            TabIndex="3" Font-Names="Calibri" Font-Size="Small" 
            onselectedindexchanged="cmbPeriodicidade_SelectedIndexChanged" >
        </asp:DropDownList>

        <asp:Label ID="Label14" runat="server" Text="Tipo: " 
            style="position:absolute; left: 715px; top: 44px; height: 15px; width: 50px;"></asp:Label>
        <asp:RadioButtonList ID="rdbType" runat="server" 
                style="position:absolute; top: 61px; left: 709px; width: 253px; right: 18px; height: 20px;" 
                onselectedindexchanged="rdbType_SelectedIndexChanged" 
                RepeatDirection="Horizontal" RepeatLayout="Flow" AutoPostBack="True" 
            CellPadding="0" CellSpacing="0" TabIndex="2">
            <asp:ListItem Value="P">Pavimento</asp:ListItem>
            <asp:ListItem Value="E">Equipamento</asp:ListItem>
            <asp:ListItem Value="T" Selected="True">Todos</asp:ListItem>
        </asp:RadioButtonList>

        <asp:Button ID="btnPesquisar" runat="server" Text="Pesquisar" 
                style="position:absolute; top: 91px; left: 14px; width: 82px; height: 26px; right: 884px;" 
                onclick="btnPesquisar_Click" />

        <asp:GridView ID="grdFormPreventiva" runat="server" 
            
            style="position:absolute; top: 125px; left: 16px; width: 950px;" AllowPaging="True" 
            AllowSorting="True" AutoGenerateColumns="False" 
            onpageindexchanging="grdFormPreventiva_PageIndexChanging" 
            ViewStateMode="Enabled" 
            BorderColor="#003366" BorderStyle="Inset" BorderWidth="1px"
            CellPadding="3" BackColor="White" Font-Size="Small" 
            onrowcommand="grdFormPreventiva_RowCommand" 
            onrowdatabound="grdFormPreventiva_RowDataBound" 
            onselectedindexchanged="grdFormPreventiva_SelectedIndexChanged">
            <Columns>
                <asp:BoundField DataField="cmpDcTipoAtividade" HeaderText="Tipo Atividade">
                    <ItemStyle Width="100px" HorizontalAlign="Left" Font-Size="Small" />
                </asp:BoundField>
                <asp:BoundField DataField="cmpDcPeriodicidade" HeaderText="Periodicidade">
                    <ItemStyle Width="100px" HorizontalAlign="Left" Font-Size="Small" />
                </asp:BoundField>
                <asp:BoundField DataField="TipoManutencao" HeaderText="Tipo Manutenção">
                    <ItemStyle Width="100px" HorizontalAlign="Left" Font-Size="Small" />
                </asp:BoundField>
                <asp:BoundField DataField="mes" HeaderText="Mês">
                    <ItemStyle Width="100px" HorizontalAlign="Center" Font-Size="Small" />
                </asp:BoundField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:ImageButton ID="ImgPavimento" runat="server" Height="16px" 
                        CommandArgument= '<%#DataBinder.Eval(Container.DataItem, "cmpCoPreventiva")%>' 
                        CommandName="btn1"
                        ImageUrl="~/App_Themes/General/page_white_acrobat.gif" Width="16px" />
                        <asp:ImageButton ID="ImgEquipamento" runat="server" Height="16px" 
                        CommandArgument= '<%#DataBinder.Eval(Container.DataItem, "cmpCoPreventiva")%>' 
                        CommandName="btn2"
                        ImageUrl="~/App_Themes/General/page_white_acrobat.gif" Width="16px" />
                    </ItemTemplate>
                    <ItemStyle Width="10px" HorizontalAlign="Left" VerticalAlign="Middle" Font-Size="Small" />
                </asp:TemplateField>
            </Columns>
            <FooterStyle BackColor="White" ForeColor="#000066" />
            <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" 
                Font-Names="Calibri" Font-Size="Medium" />
            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
            <RowStyle ForeColor="#000066" Font-Names="Calibri" Font-Size="Small" />
            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F1F1F1" />
            <SortedAscendingHeaderStyle BackColor="#007DBB" />
            <SortedDescendingCellStyle BackColor="#CAC9C9" />
            <SortedDescendingHeaderStyle BackColor="#00547E" />
        </asp:GridView>


    </div>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="webTAB_ExibirManutencaoEquipamento.aspx.cs" Inherits="HzWebManutencao.Tabelas_Apoio.webTAB_ExibirManutencaoEquipamento" %>
<%@ Register src="~/Controles/Mensagem.ascx" tagname="Mensagem" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Mensagem ID="CaixaMensagem" runat="server"/>
    <div  style="position:relative; height: 499px; width: 932px; top: 6px; left: -54px; margin-left: 40px;">
        <asp:Panel ID="pnlDadosEquipamento" runat="server" 
            style="position:absolute; left:10px; width:914px; height:158px; top: 7px;">
            <asp:Label ID="lblAcao" runat="server" Text="Exibir Manutenção Equipamento da Obra" 
                style="position:absolute; top: 1px; left: 11px; height: 20px; width: 895px; text-align: center;" 
                BackColor="White" Font-Bold="True" Font-Size="Medium" ForeColor="Maroon"></asp:Label>
           <asp:Label ID="lblObra" runat="server" Text="Obra" 
                style="position:absolute; top: 23px; left: 11px;  width: 44px;"></asp:Label>
            <asp:TextBox ID="txtNomeObra" runat="server"  
                style="position:absolute; top: 40px; left: 10px; width: 343px;"></asp:TextBox>
            <asp:Label ID="lblDcEquipamento" runat="server" Text="Descrição do Equipamento" 
                style="position:absolute; top: 23px; left: 370px;  width: 167px;"></asp:Label>
            <asp:TextBox ID="txtDcEquipamento" runat="server"
                style="position:absolute; top: 40px; left: 368px;  width: 533px;"></asp:TextBox>
            <asp:Label ID="lblCodEquipamento" runat="server" Text="Código do Equipamento"
                style="position:absolute; top: 67px; left: 10px;  width: 153px;"></asp:Label>
            <asp:TextBox ID="txtCodEquipamento" runat="server" 
                style="position:absolute; top: 86px; left: 10px; width: 140px;"></asp:TextBox>
            <asp:Label ID="lblPavimento" runat="server" Text="Pavimento"
                style="position:absolute; top: 67px; left: 171px;  width: 76px;"></asp:Label>
            <asp:TextBox ID="txtPavimento" runat="server" 
                style="position:absolute; top: 86px; left: 168px; width: 271px;"></asp:TextBox>
            <asp:Label ID="lblLocalizacao" runat="server"  Text="Localização"
                style="position:absolute; top: 67px; left: 450px; width: 76px;"></asp:Label>
            <asp:TextBox ID="txtLocalizacao" runat="server"
                style="position:absolute; top: 86px; left: 448px; width: 450px;"></asp:TextBox>

            <asp:Label ID="lblDtInicial" runat="server" 
                style="position:absolute; top: 110px; left: 10px; height: 19px; width: 76px;" 
                Text="Data Inicial"></asp:Label>
           <asp:TextBox ID="txtDtManutencaoInicial" runat="server"
                style="position:absolute; top: 127px; left: 10px; width: 144px; "></asp:TextBox>
                        <asp:MaskedEditExtender ID="txtDtManutencaoInicial_MaskedEditExtender" runat="server" 
                            Enabled="True" 
                            TargetControlID="txtDtManutencaoInicial" CultureName="pt-BR" 
                            Mask="99/99/9999" MaskType="Date" 
                            AcceptAMPM="true" MessageValidatorTip="false"
                            UserDateFormat="None"></asp:MaskedEditExtender>
                        <asp:CalendarExtender ID="txtDtManutencaoInicial_CalendarExtender" runat="server" 
                            Enabled="True" 
                            TargetControlID="txtDtManutencaoInicial" 
                            Format="dd/MM/yyyy"></asp:CalendarExtender>
            <asp:Label ID="lblDtFinal" runat="server" 
                style="position:absolute; top: 110px; left: 169px; width: 76px;" 
                Text="Data Final"></asp:Label>
           <asp:TextBox ID="txtDtManutencaoFinal" runat="server"
                style="position:absolute; top: 127px; left: 169px; width: 144px;"></asp:TextBox>
                        <asp:MaskedEditExtender ID="txtDtManutencaoFinal_MaskedEditExtender" runat="server" 
                            Enabled="True" 
                            TargetControlID="txtDtManutencaoFinal" CultureName="pt-BR" 
                            Mask="99/99/9999" MaskType="Date" 
                            AcceptAMPM="true" MessageValidatorTip="false"
                            UserDateFormat="None"></asp:MaskedEditExtender>
                        <asp:CalendarExtender ID="txtDtManutencaoFinal_CalendarExtender" runat="server" 
                            Enabled="True" 
                            TargetControlID="txtDtManutencaoFinal" 
                            Format="dd/MM/yyyy"></asp:CalendarExtender>

            <asp:Button ID="btnPesquisa" runat="server" Text="Pesquisar" 
                style="position:absolute; top: 125px; left: 337px; height: 25px; width: 88px;" 
                CausesValidation="false" onclick="btnPesquisa_Click"/>

            <asp:Button ID="btnVoltar" runat="server" Text="Voltar" 
                style="position:absolute; top: 125px; left: 432px; height: 25px; width: 88px;" 
                CausesValidation="false" onclick="btnVoltar_Click"/>

         </asp:Panel>

       <asp:GridView ID="grdPesquisa" runat="server" 
            style="position:absolute; top: 181px; left: 10px; width: 913px; " AllowPaging="True" 
            AllowSorting="True" AutoGenerateColumns="False" 
            onpageindexchanging="grdPesquisa_PageIndexChanging" 
            ViewStateMode="Enabled" 
            BorderColor="#003366" BorderStyle="Inset" BorderWidth="1px"
            CellPadding="3" BackColor="White" Font-Size="Small" PageSize="10" 
            onrowcommand="grdPesquisa_RowCommand">
            <Columns>
                <asp:BoundField DataField="cmpDtManutencaoEquipamento" HeaderText="Data"  DataFormatString="{0:dd/MM/yyyy}">
                    <ItemStyle Width="50px" HorizontalAlign="Center" Font-Size="Small" />
                </asp:BoundField>
                <asp:BoundField DataField="cmpDcDetalhamentoManutencao" HeaderText="Detalhamento Manutenção">
                    <ItemStyle Width="400px" HorizontalAlign="Left" Font-Size="Small" />
                </asp:BoundField>
                <asp:BoundField DataField="cmpNuPreventivaAgenda" HeaderText="Nº Preventiva">
                    <ItemStyle Width="50px" HorizontalAlign="Right" Font-Size="Small" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="Ordem Serviço">
                    <ItemStyle Width="80px" HorizontalAlign="Right" Font-Size="Small"/>
                    <ItemTemplate>
                        <asp:LinkButton ID="lnk" runat="server" 
                        CommandArgument='<%# Eval("cmpIdOS") %>' CommandName="lnk"><%#Eval("cmpNuOS") %></asp:LinkButton>
                    </ItemTemplate>
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

<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="webFAT_FaturamentoEntregaCarta.aspx.cs" Inherits="HzWebManutencao.Faturamento.webFAT_FaturamentoEntregaCarta" EnableEventValidation="false" %>
<%@ Register src="~/UserControls/OKMessageBox.ascx" tagname="MsgBox" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:MsgBox ID="MsgBox" runat="server" />

    <div  style="position:relative; height: 600px; width: 1110px; top: 5px; left: -77px; margin-left: 80px;">
        <asp:Label ID="Label9" runat="server" 
        style="position:absolute; top: 5px; left: 5px; width: 1119px; text-align: center;" 
        Text="REGISTRAR ENTREGA DA CARTA DE FATURAMENTO NO CLIENTE" Font-Bold="True" 
        Font-Size="Medium" ></asp:Label>

       <asp:Label ID="lblObra" runat="server" 
            style="position:absolute; top: 40px; left: 13px; width: 128px;" 
            Text="Nome da Obra:" Font-Bold="True" 
            Font-Size= "Medium" ></asp:Label>

        <asp:Label ID="Label7" runat="server" Text="Tipo de Serviço :" 
            style="position:absolute; top: 40px; left: 583px; width: 148px;" 
            Font-Bold="True" Font-Size= "Medium"></asp:Label>
 
       <asp:Label ID="lblNomeObra" runat="server" 
            style="position:absolute; top: 40px; left: 137px; width: 437px;" 
            Text="Obra" Font-Bold="True" 
            Font-Size="Medium" ></asp:Label>

       <asp:Label ID="lblTipoServico" runat="server" 
            style="position:absolute; top: 40px; left: 725px; width: 373px;" 
            Text="Tipo Servico" Font-Bold="True" 
            Font-Size="Medium" ></asp:Label>      

        <div id="divCarta" style="position:absolute; top: 85px; left: 5px; width: 1099px; height: 490px; border-width:medium;">
            <asp:Label ID="lblCarta" runat="server" 
                style="position:absolute; top: 4px; left: 1px; width: 1098px; text-align: center;" 
                Text="Carta Faturamento" Font-Bold="True" Font-Size="Medium" ></asp:Label>

           <asp:Panel ID="Panel2" 
                style="position:absolute; top: 36px; left: 546px; width: 543px; height:196px" 
                runat="server">
                <asp:Label ID="lblDestinatario1" runat="server" Font-Bold="True" 
                    style="position:absolute; top: 34px; left: 9px; width: 130px; height: 15px;" 
                    Text="Primeiro Destinatário:"></asp:Label>
                <asp:Label ID="lblNuCarta" runat="server" 
                    style="position:absolute; top: 5px; left: 9px; width: 516px; text-align: left;" 
                    Text="Número da Carta" Font-Bold="True" Font-Size="Medium" ></asp:Label>

                <asp:TextBox ID="txtNomeDestinatario1" runat="server"  
                    style="position:absolute; top: 33px; left: 146px; width: 372px;" 
                    TabIndex="0"></asp:TextBox>

                <asp:Label ID="lblDestinatario2" runat="server" Font-Bold="True"
                    style="position:absolute; top: 58px; left: 9px; width: 133px; height: 15px;" 
                    Text="Segundo Destinatário:"></asp:Label>
                <asp:TextBox ID="txtNomeDestinatario2" runat="server"  
                    style="position:absolute; top: 56px; left: 146px; width: 372px;" TabIndex="1"></asp:TextBox>

                <asp:Label ID="lblOrgao" runat="server" Font-Bold="True"
                    style="position:absolute; top: 86px; left: 9px; width: 90px; height: 14px;" 
                    Text="Nome do Órgão:"></asp:Label>
                <asp:TextBox ID="txtNomeOrgao" runat="server" 
                    style="position:absolute; top: 84px; left: 146px; width: 372px;" TabIndex="2"></asp:TextBox>

                <asp:Label    ID="lblSetor" runat="server" Font-Bold="True"  
                    style="position:absolute; top: 114px; left: 9px; width: 90px; " 
                    Text="Setor do Órgão:"></asp:Label>
                <asp:TextBox  ID="txtSetor" runat="server" 
                    style="position:absolute; top: 110px; left: 146px; width: 372px; " 
                    TabIndex="3"></asp:TextBox>


    </asp:Panel>
    <asp:Panel ID="Panel1" 
            style="position:absolute; top: 36px; left: 7px; width: 533px; height:196px" 
            ScrollBars="Vertical" runat="server">
        <asp:GridView ID="gvDadosCarta" runat="server" 
            style="position:absolute; top: 5px; left: 9px; width: 521px; right: 3px;" ShowFooter="True" 
        AllowSorting="True" AutoGenerateColumns="False" CellPadding="3" 
        BackColor="White" BorderColor="#003366" BorderStyle="Inset" BorderWidth="1px"
        ViewStateMode="Enabled" 
        onrowdatabound="gvDadosCarta_RowDataBound" 
        onselectedindexchanged="gvDadosCarta_SelectedIndexChanged" 
            onrowcommand="gvDadosCarta_RowCommand">
        <FooterStyle BackColor="Tan" Font-Bold="True" HorizontalAlign="Right" />
        <Columns>
            <asp:TemplateField HeaderText="Nº Carta">
                <ItemTemplate>
                    <asp:Label ID="lblNumeroCarta"  runat="server"  Text='<%#Eval("cmpNuCartaOrion")%>'/>
                </ItemTemplate>
                <ItemStyle Width="40px" HorizontalAlign="Left" VerticalAlign="Middle"/>
            </asp:TemplateField>

            <asp:BoundField DataField="cmpDtEmissaoCarta" HeaderText="Emissão">
                <ItemStyle HorizontalAlign="Center" Width="80px" VerticalAlign="Middle" Wrap="True" />
            </asp:BoundField>

            <asp:TemplateField HeaderText="Valor Carta (R$)">
            <ItemStyle Width="30px" HorizontalAlign="Right"/>
            <ItemTemplate>
                <asp:Label ID="lblValor" runat="server" Text='<%#Eval("cmpVlCarta","{0:N2}")%>'/>
                </ItemTemplate>
                <FooterTemplate>
                <asp:Label ID="lblValorTotal" runat="server" />
                </FooterTemplate>                   
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Visualizar Carta">
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="15px"/>
                <ItemTemplate>
                    <asp:ImageButton ID="Image1" runat="server" Height="14px" 
                    CommandArgument= '<%#DataBinder.Eval(Container.DataItem, "cmpEdCartaoOrion") %>' CommandName="btnCarta" ImageUrl="~/App_Themes/General/word.png" Width="16px" />
                </ItemTemplate>
            </asp:TemplateField>

            <asp:BoundField DataField="cmpIdFaturaCarta" HeaderText="IdFaturaCarta">
                <ItemStyle HorizontalAlign="Center" Width="50px" VerticalAlign="Middle" Wrap="True" />
            </asp:BoundField>

            </Columns>
        <FooterStyle BackColor="White" ForeColor="#000066"/>
        <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="White" ForeColor="#000066"/>
        <RowStyle ForeColor =" #000066" Font-Names="Calibri" Font-Size="Small" />
<%--            <selectedrowstyle backcolor="LightCyan" forecolor="DarkBlue" font-bold="true"/>--%>  
        <SelectedRowStyle BackColor="White" Font-Bold="True" ForeColor="DarkBlue" />            
        <SortedAscendingCellStyle BackColor="#F1F1F1" />
        <SortedAscendingHeaderStyle BackColor="#007DBB" />
        <SortedDescendingCellStyle BackColor="#CAC9C9" />
        <SortedDescendingHeaderStyle BackColor="#00547E" />
    </asp:GridView>
    </asp:Panel>
        <asp:Panel ID="pnlFileUpload" runat="server" Style="position:absolute;top:250px; left: 5px; width: 523px; height:25px" CssClass="modalPopup">Anexar o documento:
            <asp:FileUpload ID="fileUpload" runat="server" style="position:relative; left:8px; top:2px; width:300px" />
        </asp:Panel>

        <asp:Button ID="btnGravarCarta" runat="server"  TabIndex="11" Font-Bold="True"
            style="position: absolute; top: 250px; left: 546px; height: 25px; width: 120px;" 
            Text="Registrar Entrega" onclick="btnGravarCarta_Click" />

        <asp:Button ID="btnVoltar" runat="server" Font-Bold="True"
            style="position: absolute; top: 250px; left: 673px; height: 25px; width: 88px;" 
            Text="Voltar" onclick="btnVoltar_Click" />
    </div>
    </div>
</asp:Content>
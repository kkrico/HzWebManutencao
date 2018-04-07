<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="webFAT_FaturamentoObraCarta.aspx.cs" Inherits="HzWebManutencao.Faturamento.webFAT_FaturamentoObraCarta" EnableEventValidation="false"%>
<%@ Register src="~/UserControls/OKMessageBox.ascx" tagname="MsgBox" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:MsgBox ID="MsgBox" runat="server" />

    <div  style="position:relative; height: 741px; width: 1128px; top: 5px; left: -77px; margin-left: 80px;">
        <asp:Label ID="Label9" runat="server" 
        style="position:absolute; top: 5px; left: 5px; width: 1119px; text-align: center;" 
        Text="PREPARAR E EMITIR CARTA PARA FATURAMENTO" Font-Bold="True" 
        Font-Size="Medium" ></asp:Label>

       <asp:Label ID="lblObra" runat="server" 
            style="position:absolute; top: 25px; left: 13px; width: 128px;" 
            Text="Nome da Obra:" Font-Bold="True" 
            Font-Size= "Medium" ></asp:Label>

        <asp:Label ID="Label7" runat="server" Text="Tipo de Serviço :" 
            style="position:absolute; top: 25px; left: 650px; width: 148px;" 
            Font-Bold="True" Font-Size= "Medium"></asp:Label>
 
       <asp:Label ID="lblNomeObra" runat="server" 
            style="position:absolute; top: 25px; left: 143px; width: 497px;" 
            Text="Obra" Font-Bold="True" 
            Font-Size="Medium" ></asp:Label>

       <asp:Label ID="lblTipoServico" runat="server" 
            style="position:absolute; top: 25px; left: 797px; width: 326px;" 
            Text="Tipo Servico" Font-Bold="True" 
            Font-Size="Medium" ></asp:Label>      

        <hr id="Hr1" 
            style="position:absolute; top: 67px; left: 4px; width: 1119px; height:-15px;" 
            runat="server"/>

        <asp:Button ID="btnVoltar" runat="server" Font-Bold="True"
            style="position: absolute; top: 80px; left: 13px; height: 25px; width: 88px;" 
            Text="Voltar" onclick="btnVoltar_Click" />

        <div id="divCarta" 
            style="position:absolute; top: 100px; left: 5px; width: 636px; height: 643px; border-width:medium;">
            
            <asp:Label ID="lblCarta" runat="server" 
                style="position:absolute; top: 4px; left: 1px; width: 629px; text-align: center;" 
                Text="Carta Faturamento" Font-Bold="True" Font-Size="Medium" ></asp:Label>

            <asp:Label ID="lblNuCarta" runat="server" 
                style="position:absolute; top: 30px; left: 13px; width: 608px; text-align: left;" 
                Text="Número da Carta" Font-Bold="True" Font-Size="Medium" ></asp:Label>

            <asp:Label ID="lblDestinatario1" runat="server" Font-Bold="True"
                style="position:absolute; top: 62px; left: 13px; width: 155px; height: 15px;" 
                Text="Primeiro Destinatário:"></asp:Label>
            <asp:TextBox ID="txtNomeDestinatario1" runat="server"  
                style="position:absolute; top: 60px; left: 168px; width: 455px;" TabIndex="0"></asp:TextBox>

            <asp:Label ID="lblDestinatario2" runat="server" Font-Bold="True"
                style="position:absolute; top: 92px; left: 13px; width: 155px; height: 15px;" 
                Text="Segundo Destinatário:"></asp:Label>
            <asp:TextBox ID="txtNomeDestinatario2" runat="server"  style="position:absolute; top: 90px; left: 168px; width: 455px;" TabIndex="1"></asp:TextBox>

           <asp:Label ID="lblOrgao" runat="server" Font-Bold="True"
                style="position:absolute; top: 122px; left: 13px; width: 90px; " 
                Text="Nome do Órgão:"></asp:Label>
           <asp:TextBox ID="txtNomeOrgao" runat="server" style="position:absolute; top: 120px; left: 168px; width: 455px;" TabIndex="2"></asp:TextBox>

           <asp:Label    ID="lblSetor" runat="server" Font-Bold="True"  
                style="position:absolute; top: 152px; left: 13px; width: 90px; " 
                Text="Setor do Órgão:"></asp:Label>
           <asp:TextBox  ID="txtSetor" runat="server" 
                style="position:absolute; top: 150px; left: 168px; width: 455px; " TabIndex="3"></asp:TextBox>

           <asp:Label ID="Label10" runat="server" Text="Email Engenheiro Obra:" Font-Bold="True"
                style="position:absolute; top: 182px; left: 13px; width: 147px; height: 13px;"></asp:Label>
           <asp:TextBox ID="txtEmailEng" runat="server" style="position:absolute; top: 180px; left: 168px; width: 455px;" TabIndex="4"></asp:TextBox>

           <asp:Label ID="Label11" runat="server" Text="Email do Administrativo:" Font-Bold="True"
                style="position:absolute; top: 212px; left: 13px; width: 155px;"></asp:Label>
           <asp:TextBox ID="txtEmailAux" runat="server" 
                style="position:absolute; top: 210px; left: 168px; width: 455px;" TabIndex="5"></asp:TextBox>

            <asp:Label ID="lblTipoDoc" runat="server" Text="Tipos de Documentos" TabIndex="6"
                style="position:absolute; top: 241px; left: 6px; height: 17px; width: 151px; " 
                Font-Size="Small" Font-Bold="True"></asp:Label>
            <asp:ListBox ID="lstDocumentos" runat="server" 
                style="position:absolute; top: 260px; left: 6px; width: 300px; height: 133px;" 
                SelectionMode="Multiple" Font-Size="Small" TabIndex="9"></asp:ListBox>

           <asp:Button ID="btnVincular" runat="server" Text=">>" style="position:absolute; top: 296px; left: 307px; width: 29px; height: 25px; bottom: 338px;" 
                 onclick="btnVincular_Click" CausesValidation="False" Font-Bold="True"  TabIndex="10"
                Font-Names="Calibri" Font-Size="Medium"/>
            <asp:Button ID="btnDesvincular" runat="server" Text="&lt;&lt;" style="position:absolute; top: 335px; left: 307px; width: 29px; height: 25px; bottom: 299px;" 
                  CausesValidation="False" Font-Bold="True" Font-Names="Calibri" 
                Font-Size="Medium" Font-Italic="False" onclick="btnDesvincular_Click"/>
 
            <asp:Label ID="lblDocAnexados" runat="server" Text="Documentos Anexados" 
                style="position:absolute; top: 241px; left: 337px; height: 17px; width: 151px; right: 620px;" 
                Font-Size="Small" Font-Bold="True"></asp:Label>

            <asp:ListBox ID="lstDocAnexo" runat="server"  TabIndex="10"
                style="position:absolute; top: 260px; left: 337px; width: 293px; height: 133px;" 
                SelectionMode="Multiple" Font-Size="Small"></asp:ListBox>

           <asp:Button ID="btnNovo" runat="server" Text="Nova Carta" 
                CausesValidation="False" Font-Bold="True"
                style="position:absolute; top: 406px; left: 9px; width: 88px; height: 25px; right: 539px;" 
                onclick="btnNovo_Click"/>

           <asp:Button ID="btnGravarCarta" runat="server"  TabIndex="11" Font-Bold="True"
                style="position: absolute; top: 406px; left: 103px; height: 25px; width: 88px;" 
                Text="Gravar Carta" onclick="btnGravarCarta_Click" />

           <asp:Button ID="btnExcluirCarta" runat="server"  TabIndex="11" Font-Bold="True"
                style="position: absolute; top: 406px; left: 199px; height: 25px; width: 88px;" 
                Text="Excluir" onclick="btnExcluirCarta_Click"/>

           <asp:Button ID="btnCartaOrion" runat="server"  TabIndex="11" Font-Bold="True" Enabled="false" Visible="false"
                style="position: absolute; top: 406px; left: 392px; height: 25px; width: 88px;" 
                Text="Gerar Carta"/>

           <asp:Button ID="btnCancelarCarta" runat="server"  TabIndex="11" Font-Bold="True" Enabled="false"
                style="position: absolute; top: 406px; left: 296px; height: 25px; width: 88px;" 
                Text="Cancelar" onclick="btnCancelarCarta_Click"/>

        <asp:Panel ID="Panel1" 
                style="position:absolute; top: 444px; left: 5px; width: 624px; height:196px" 
                ScrollBars="Vertical" runat="server">

         <asp:GridView ID="gvDadosCarta" runat="server" 
            style="position:absolute; top: 5px; left: 9px; width: 598px;" ShowFooter="True" 
            AllowSorting="True" AutoGenerateColumns="False" CellPadding="3" 
            BackColor="White" BorderColor="#003366" BorderStyle="Inset" BorderWidth="1px"
            ViewStateMode="Enabled" 
            onrowdatabound="gvDadosCarta_RowDataBound" 
            onselectedindexchanged="gvDadosCarta_SelectedIndexChanged" 
                onrowcommand="gvDadosCarta_RowCommand">
             <%--            <selectedrowstyle backcolor="LightCyan" forecolor="DarkBlue" font-bold="true"/>--%>
             <FooterStyle BackColor="Tan" Font-Bold="True" HorizontalAlign="Right" />
             <Columns>
                 <asp:TemplateField HeaderText="Nº Carta">
                     <ItemTemplate>
                         <asp:Label ID="lblRascunho" runat="server" Text="Rascunho" />
                         <asp:Label ID="lblNumeroCarta" runat="server" 
                             Text='<%#Eval("cmpNuCartaOrion")%>' visible="false" />
                     </ItemTemplate>
                     <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="40px" />
                 </asp:TemplateField>
                 <asp:BoundField DataField="cmpDtEmissaoCarta" HeaderText="Emissão">
                 <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80px" 
                     Wrap="True" />
                 </asp:BoundField>
                 <asp:TemplateField HeaderText="Valor Carta (R$)">
                     <ItemStyle HorizontalAlign="Right" Width="30px" />
                     <ItemTemplate>
                         <asp:Label ID="lblValor" runat="server" 
                             Text='<%#Eval("cmpVlCarta","{0:N2}")%>' />
                     </ItemTemplate>
                     <FooterTemplate>
                         <asp:Label ID="lblValorTotal" runat="server" />
                     </FooterTemplate>
                 </asp:TemplateField>
                 <asp:BoundField DataField="cmpDtEnvioEmailObra" HeaderText="Email Enviado">
                 <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px" 
                     Wrap="True" />
                 </asp:BoundField>
                 <asp:TemplateField HeaderText="Gerar Carta">
                     <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="15px" />
                     <ItemTemplate>
                         <asp:ImageButton ID="Image1" runat="server" 
                             CommandArgument='<%#DataBinder.Eval(Container.DataItem, "cmpIdFaturaCarta") %>' 
                             CommandName="btnCarta" Height="14px" ImageUrl="~/App_Themes/General/word.png" 
                             Width="16px" />
                         <asp:ImageButton ID="imgVerdoc" runat="server" 
                             CommandArgument='<%#DataBinder.Eval(Container.DataItem, "cmpIdFaturaCarta") %>' 
                             CommandName="btnCarta" Height="14px" 
                             ImageUrl="~/App_Themes/General/Vizualizar.jpg" Width="16px" />
                     </ItemTemplate>
                 </asp:TemplateField>
                 <asp:TemplateField HeaderText="Enviar Email">
                     <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="15px" />
                     <ItemTemplate>
                         <asp:ImageButton ID="Image2" runat="server" 
                             CommandArgument='<%#DataBinder.Eval(Container.DataItem, "cmpIdFaturaCarta") %>' 
                             CommandName="btnEmail" Height="14px" 
                             ImageUrl="~/App_Themes/General/outlook_preto.png" Width="25px" />
                     </ItemTemplate>
                 </asp:TemplateField>
                 <asp:TemplateField HeaderText="Carta">
                     <ItemTemplate>
                         <asp:Label ID="lblNuCarta" runat="server" />
                     </ItemTemplate>
                 </asp:TemplateField>
                 <asp:BoundField DataField="cmpIdFaturaCarta" HeaderText="IdFaturaCarta">
                 <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" 
                     Wrap="True" />
                 </asp:BoundField>
                 <asp:TemplateField HeaderText="Caminho" Visible="False">
                     <ItemTemplate>
                         <asp:Label ID="lblCaminho" runat="server" 
                             Text='<%# Bind("cmpEdCartaoOrion") %>'></asp:Label>
                     </ItemTemplate>
                     <EditItemTemplate>
                         <asp:TextBox ID="TextBox1" runat="server" 
                             Text='<%# Bind("cmpEdCartaoOrion") %>'></asp:TextBox>
                     </EditItemTemplate>
                 </asp:TemplateField>
                 <asp:TemplateField HeaderText="Regerar Carta" ShowHeader="False">
                     <ItemTemplate>
                         <asp:ImageButton runat="server" 
                             CommandArgument='<%# DataBinder.Eval(Container.DataItem, "cmpIdFaturaCarta") %>' 
                             CommandName="btnRegerar" Height="25px" 
                             ImageUrl="~/App_Themes/General/regerar.png" Width="24px" />
                     </ItemTemplate>
                     <ItemStyle Width="15px" />
                 </asp:TemplateField>
             </Columns>
             <FooterStyle BackColor="White" ForeColor="#000066" />
            <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="White" ForeColor="#000066"/>
            <RowStyle ForeColor =" #000066" Font-Names="Calibri" Font-Size="Small" />
            <SelectedRowStyle BackColor="White" Font-Bold="True" ForeColor="DarkBlue" />            
            <SortedAscendingCellStyle BackColor="#F1F1F1" />
            <SortedAscendingHeaderStyle BackColor="#007DBB" />
            <SortedDescendingCellStyle BackColor="#CAC9C9" />
            <SortedDescendingHeaderStyle BackColor="#00547E" />
        </asp:GridView>

        </asp:Panel>
    </div>

    <div id="divNota" runat="server"
            
            
            style="position:relative; top: 100px; left: 651px; width: 465px; height: 604px; border-width:medium;">
       <asp:Label ID="lblNota" runat="server" style="position:absolute; top: 5px; left: 7px; width: 440px; text-align: center;" Text="Nota Fiscal" Font-Bold="True" Font-Size="Medium" ></asp:Label>
       <asp:Label ID="Label3" runat="server" style="position:absolute; top: 32px; left: 8px; width: 60px;" Text="Número:" Font-Bold="True"></asp:Label>
       <asp:TextBox ID="txtNuNotaFiscal" runat="server" 
            style="position:absolute; top: 30px; left: 78px; width: 150px;" TabIndex="20"></asp:TextBox>

       <asp:Label ID="Label4" runat="server" style="position:absolute; top: 32px; left: 240px; width: 60px;" Text="Emissão:" Font-Bold="True"></asp:Label>
       <asp:TextBox ID="txtEmissaoNota" runat="server"  style="position:absolute; top: 30px; left: 304px; width: 150px; right: 11px;" TabIndex="21"></asp:TextBox> 
            <asp:MaskedEditExtender ID="MaskedEditExtender2" runat="server" Enabled="True" TargetControlID="txtEmissaoNota" CultureName="pt-BR" 
                Mask="99/99/9999" MaskType="Date" UserDateFormat="DayMonthYear" UserTimeFormat="TwentyFourHour"></asp:MaskedEditExtender>
                <asp:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True" TargetControlID="txtEmissaoNota" Format="dd/MM/yyyy"></asp:CalendarExtender>

      <asp:Label ID="Label6" runat="server" style="position:absolute; top: 62px; left: 8px; width: 50px;" Text="Valor:" Font-Bold="True"></asp:Label>
      <asp:TextBox ID="txtValorNota" runat="server" style="position:absolute; top: 60px; left: 80px; width: 150px;" TabIndex="22"></asp:TextBox>
             <asp:MaskedEditExtender ID="MaskedEditExtender4"  InputDirection = "RightToLeft" runat="server" TargetControlID="txtValorNota" 
                    Mask="999999999,99" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError"
                    MaskType =  "Number" AcceptNegative= "None" ClearMaskOnLostFocus="true" ErrorTooltipEnabled="True"/>

      <asp:Label ID="Label5" runat="server" Text="Período" style="position:absolute; top: 90px; left: 7px; width: 451px; text-align: center;" Font-Bold="True"></asp:Label>
      <asp:Label ID="Label8" runat="server" Text="Data Inicial :" 
            style="position:absolute; top: 122px; left: 8px; width: 71px;" Font-Bold="True"></asp:Label>  
        <asp:TextBox ID="txtDataInicial" runat="server" style="position:absolute; top: 120px; left: 80px; width: 150px; right: 230px;" TabIndex="23"></asp:TextBox> 
            <asp:MaskedEditExtender ID="txtDataInicial_MaskedEditExtender" runat="server" Enabled="True"
                TargetControlID="txtDataInicial" CultureName="pt-BR" 
                Mask="99/99/9999" MaskType="Date" UserDateFormat="DayMonthYear" 
                UserTimeFormat="TwentyFourHour"></asp:MaskedEditExtender>
                <asp:CalendarExtender ID="txtDataInicial_CalendarExtender" runat="server" Enabled="True" TargetControlID="txtDataInicial" Format="dd/MM/yyyy"></asp:CalendarExtender>

      <asp:Label ID="Label12" runat="server" Text="Data Final :" 
            style="position:absolute; top: 122px; left: 237px; width: 65px;" 
            Font-Bold="True"></asp:Label>  
      <asp:TextBox ID="txtDataFinal" runat="server" style="position:absolute; top: 120px; left: 304px; width: 150px; right: 3px;" TabIndex="24"></asp:TextBox> 
            <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Enabled="True" 
                TargetControlID="txtDataFinal" CultureName="pt-BR" 
                Mask="99/99/9999" MaskType="Date" UserDateFormat="DayMonthYear" 
                UserTimeFormat="TwentyFourHour"></asp:MaskedEditExtender>
                <asp:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" TargetControlID="txtDataFinal" Format="dd/MM/yyyy"></asp:CalendarExtender>

      <asp:Label ID="Label13" runat="server" Text="Mês Inicial :" 
            style="position:absolute; top: 152px; left: 8px; width: 70px;" Font-Bold="True"></asp:Label>  
      <asp:TextBox ID="txtMesInicial" runat="server" style="position:absolute; top: 150px; left: 80px; width: 150px; right: 227px;" TabIndex="25"></asp:TextBox> 

      <asp:Label ID="Label14" runat="server" Text="Mês Final :" 
            style="position:absolute; top: 152px; left: 237px; width: 62px;" 
            Font-Bold="True"></asp:Label>  
      <asp:TextBox ID="txtMesFinal" runat="server" TabIndex="26"
            style="position:absolute; top: 150px; left: 304px; width: 150px; right: 11px;"></asp:TextBox> 

        <asp:Panel ID="Panel2" style="position:absolute; top: 216px; left: 5px; width: 447px; height:184px" ScrollBars="Vertical" runat="server">
             <asp:GridView ID="gvDadosNota" runat="server" 
                 
                 style="position:absolute; top: 5px; left: 8px; width: 419px;" ShowFooter="True" 
                AllowSorting="True" AutoGenerateColumns="False" CellPadding="3" 
                BackColor="White" BorderColor="#003366" BorderStyle="Inset" BorderWidth="1px" 
                OnRowDataBound="gvDadosNota_RowDataBound" 
                onselectedindexchanged="gvDadosNota_SelectedIndexChanged"
                ViewStateMode="Enabled" onrowcommand="gvDadosNota_RowCommand">
                <Columns>
                         <asp:TemplateField HeaderText="Nº Nota Fiscal">
                                <ItemTemplate>
                                    <asp:Label ID="lblNota"  runat="server" Text='<%#Eval("cmpNuNotaFiscal")%>'  CommandArgument='<%# Eval("cmpIdFaturaCartaNota") %>' CommandName="lblNota"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="30px" HorizontalAlign="Left" VerticalAlign="Middle"/>
                            </asp:TemplateField>

                        <asp:BoundField DataField="cmpDtEmissaoNotaFiscal" HeaderText="Emissão" DataFormatString="{0:dd/MM/yyyy}">
                            <ItemStyle HorizontalAlign="Center" Width="30px" VerticalAlign="Middle" Wrap="True" />
                        </asp:BoundField>

                        <asp:TemplateField HeaderText="Valor Nota (R$)">
                        <ItemStyle Width="30px" HorizontalAlign="Right"/>
                        <ItemTemplate>
                            <asp:Label ID="lblValorNota" runat="server" Text='<%#Eval("cmpVlNota","{0:N2}")%>'/>
                            </ItemTemplate>
                            <FooterTemplate>
                            <asp:Label ID="lblValorTotalNota" runat="server" />
                            </FooterTemplate>                   
                        </asp:TemplateField>

                        <asp:BoundField DataField="cmpIdFaturaCartaNota" HeaderText="IdFaturaCartaNota">
                            <ItemStyle HorizontalAlign="Center" Width="10px" VerticalAlign="Middle" Wrap="True" />
                        </asp:BoundField>

                        <asp:BoundField DataField="cmpDtPeriodoInicialServico" HeaderText="cmpDtPeriodoInicialServico"  DataFormatString="{0:dd/MM/yyyy}">
                            <ItemStyle HorizontalAlign="Center" Width="10px" VerticalAlign="Middle" Wrap="True" />
                        </asp:BoundField>

                        <asp:BoundField DataField="cmpDtPeriodoFinalServico" HeaderText="cmpDtPeriodoFinalServico"  DataFormatString="{0:dd/MM/yyyy}">
                            <ItemStyle HorizontalAlign="Center" Width="10px" VerticalAlign="Middle" Wrap="True" />
                        </asp:BoundField>

                        <asp:BoundField DataField="cmpNoMesInicialServico" HeaderText="cmpNoMesInicialServico">
                            <ItemStyle HorizontalAlign="Center" Width="10px" VerticalAlign="Middle" Wrap="True" />
                        </asp:BoundField>

                        <asp:BoundField DataField="cmpNoMesFinalServico" HeaderText="cmpNoMesFinalServico">
                            <ItemStyle HorizontalAlign="Center" Width="10px" VerticalAlign="Middle" Wrap="True" />
                        </asp:BoundField>


                </Columns>
                <FooterStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Right" />
                <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="White" ForeColor="#000066"/>
                <RowStyle ForeColor="#000066" Font-Names="Calibri" Font-Size="Small" />
                <SelectedRowStyle BackColor="White" Font-Bold="True" ForeColor="#000066" />
                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                <SortedAscendingHeaderStyle BackColor="#007DBB" />
                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                <SortedDescendingHeaderStyle BackColor="#00547E" />
            </asp:GridView>
        </asp:Panel>
        <asp:Button ID="btnNovaNota"     Font-Bold="True" runat="server"   Text="Nova Nota"     style="position: absolute; top: 181px; left: 8px;   height: 25px; width: 88px;"  onclick="btnNovaNota_Click"/>
        <asp:Button ID="btnGravarNota"   Font-Bold="True" runat="server"   Text="Gravar Nota"   style="position: absolute; top: 181px; left: 106px; height: 25px; width: 88px;"  onclick="btnGravarNota_Click" />
        <asp:Button ID="btnExcluirNota"  Font-Bold="True" runat="server"   Text="Excluir"       style="position: absolute; top: 181px; left: 205px; height: 25px; width: 88px;"  onclick="btnExcluirNota_Click"/>
        <asp:Button ID="btnCancelarNota" Font-Bold="True" runat="server"   Text="Cancelar"      style="position: absolute; top: 181px; left: 303px; height: 25px; width: 88px;"  onclick="btnCancelarNota_Click" Enabled="false"/>
    </div>
 
    </div>
</asp:Content>
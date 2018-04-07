<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="webFAT_RecebeFatura.aspx.cs" Inherits="HzWebManutencao.Faturamento.webFAT_RecebeFatura" EnableEventValidation="false"%>
<%@ Register src="~/UserControls/OKMessageBox.ascx" tagname="MsgBox" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:MsgBox ID="MsgBox" runat="server" />
    <div  style="position:relative; height: 408px; width: 1130px; top: 12px; left: -80px; margin-left: 80px;">
        <asp:Label      ID="Label12"                runat="server" style="position:absolute; top: 4px; left: 5px;    width : 1117px; text-align: center;"   Text="Recebimento da Fatura GrupoOrion"   Font-Bold="True" Font-Size="Medium" ></asp:Label>
       <asp:Label ID="lblObra" runat="server" 
            style="position:absolute; top: 31px; left: 13px; width: 122px;" 
            Text="Nome da Obra:" Font-Bold="True" 
            Font-Size="Medium"   ></asp:Label>

        <asp:Label ID="Label7" runat="server" Text="Área de Negócio:" 
            style="position:absolute; top: 31px; left: 524px; width: 143px;" 
            Font-Bold="True" Font-Size= "Medium"></asp:Label>
 
       <asp:Label ID="lblNomeObra" runat="server" 
            style="position:absolute; top: 31px; left: 142px; width: 382px; height: 29px;" 
            Text="Obra" Font-Bold="True" 
            Font-Size="Medium" ></asp:Label>

       <asp:Label ID="lblTipoServico" runat="server" 
            style="position:absolute; top: 31px; left: 670px; width: 446px; height: 38px;" 
            Text="Tipo Servico" Font-Bold="True" 
            Font-Size="Medium" ></asp:Label> 
                 
        <hr id="Hr1" style="position:absolute; top: 70px; left: 4px; width: 1119px; height:-15px;" runat="server"/>

        <asp:Panel ID="Panel2" 
            style="position:absolute; top: 93px; left: 13px; width: 600px; height:250px;" 
            ScrollBars="Vertical" runat="server">
           <asp:Label      ID="Label1"     runat="server" 
                style="position:absolute; top: 1px; left: 5px; height: 17px; width: 162px;" 
                Font-Size="Small" Font-Bold="True" Text="Dados Nota Fiscal"></asp:Label>

             <asp:GridView ID="gvDadosNota" runat="server" 
                 style="position:absolute; top: 25px; left: 4px; width: 575px;" ShowFooter="True" 
                AllowSorting="True" AutoGenerateColumns="False" CellPadding="3" 
                BackColor="White" BorderColor="#003366" BorderStyle="Inset" BorderWidth="1px" 
                OnRowDataBound="gvDadosNota_RowDataBound" 
                onselectedindexchanged="gvDadosNota_SelectedIndexChanged"
                ViewStateMode="Enabled">
                <Columns>
                        <asp:BoundField DataField="cmpIdFaturaCartaNota" HeaderText="IdFaturaCartaNota">
                            <ItemStyle HorizontalAlign="Center" Width="10px" VerticalAlign="Middle"  />
                        </asp:BoundField>

                        <asp:TemplateField HeaderText="Nº Nota Fiscal">
                                <ItemTemplate>
                                    <asp:Label ID="lblNota"  runat="server" Text='<%#Eval("cmpNuNotaFiscal")%>'  CommandArgument='<%# Eval("cmpIdFaturaCartaNota") %>' CommandName="lblNota"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="40px" HorizontalAlign="Right" VerticalAlign="Middle"/>
                        </asp:TemplateField>

                        <asp:BoundField DataField="cmpDtEmissaoNotaFiscal" HeaderText="Emissão" DataFormatString="{0:dd/MM/yyyy}">
                            <ItemStyle HorizontalAlign="Center" Width="20px" VerticalAlign="Middle" Wrap="True" />
                        </asp:BoundField>

                        <asp:TemplateField HeaderText="Valor Nota (R$)">
                        <ItemStyle Width="60px" HorizontalAlign="Right"/>
                        <ItemTemplate>
                            <asp:Label ID="lblValorNota" runat="server" Text='<%#Eval("cmpVlNota","{0:N2}")%>'/>
                            </ItemTemplate>
                            <FooterTemplate>
                            <asp:Label ID="lblValorTotalNota" runat="server" />
                            </FooterTemplate>                   
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Valor Recebido (R$)">
                        <ItemStyle Width="60px" HorizontalAlign="Right"/>
                        <ItemTemplate>
                            <asp:Label ID="lblValorNotaRecebido" runat="server" Text='<%#Eval("cmpVlRecebidoNota","{0:N2}")%>'/>
                            </ItemTemplate>
                            <FooterTemplate>
                            <asp:Label ID="lblValorTotalNotaRecebido" runat="server" />
                            </FooterTemplate>                   
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Valor Glosado (R$)">
                        <ItemStyle Width="60px" HorizontalAlign="Right"/>
                        <ItemTemplate>
                            <asp:Label ID="lblValorNotaGlosado" runat="server" Text='<%#Eval("cmpVlGlosadoNota","{0:N2}")%>'/>
                            </ItemTemplate>
                            <FooterTemplate>
                            <asp:Label ID="lblValorTotalNotaGlosado" runat="server" />
                            </FooterTemplate>                   
                        </asp:TemplateField>

                       <asp:BoundField DataField="cmpDtRecebimentoNota" HeaderText="Recebido" DataFormatString="{0:dd/MM/yyyy}">
                            <ItemStyle HorizontalAlign="Center" Width="30px" VerticalAlign="Middle" Wrap="True" />
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

        <asp:Panel ID="Panel1" style="position:absolute; top: 93px; left: 617px; width: 500px; height:250px;" 
            runat="server">
            <asp:Label      ID="lblTipoDoc"     runat="server" 
                style="position:absolute; top: 1px; left: 5px; height: 17px; width: 162px;" 
                Font-Size="Small" Font-Bold="True" Text="Dados Recebimento"></asp:Label>
            <asp:Label      ID="lblNotaFiscal"     runat="server" 
                style="position:absolute; top: 28px; left: 5px; height: 17px; width: 261px;" 
                Font-Size="Small" Font-Bold="True" Text="Número da Nota Fiscal:"></asp:Label>

            <asp:Label      ID="Label9"         runat="server" 
                style="position:absolute; top: 137px; left: 14px; width: 78px; height: 13px;" 
                Text="Valor Recebido "></asp:Label>
            <asp:TextBox    ID="txtVlRecebido"  runat="server"  TabIndex="0"
                style="position:absolute; top: 154px; left: 14px; width: 146px;"></asp:TextBox>
                <asp:MaskedEditExtender ID="MaskedEditExtender5"  runat="server" InputDirection = "RightToLeft" TargetControlID="txtVlRecebido" Mask="999999999,99" MessageValidatorTip="true" 
                    OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType =  "Number" AcceptNegative= "None" ErrorTooltipEnabled="True"/>
            <asp:Label      ID="Label10"        runat="server" 
                style="position:absolute; top: 137px; left: 167px; width: 87px; height: 16px;" 
                Text="Valor Glosado "></asp:Label>
            <asp:TextBox    ID="txtVlGlosado"   runat="server" TabIndex="1"
                style="position:absolute; top: 154px; left: 167px; width: 146px; "></asp:TextBox>
                <asp:MaskedEditExtender ID="MaskedEditExtender6" runat="server" InputDirection = "RightToLeft" TargetControlID="txtVlGlosado" Mask="999999999,99" MessageValidatorTip="true" 
                    OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType=  "Number" AcceptNegative= "None" ClearMaskOnLostFocus="true" ErrorTooltipEnabled="True"/>
            <hr id="Hr2" style="position:absolute; top: 60px; left: 5px; width: 486px; height:-15px;" runat="server"/>
                <asp:RadioButtonList ID="rdbRecebe" runat="server" Font-Size="Small" style="position:absolute; top: 70px; left: 10px; width: 470px; height: 35px;"
                    AutoPostBack="True" RepeatDirection="Horizontal"  RepeatColumns="3" 
                    onselectedindexchanged="rdbRecebe_SelectedIndexChanged">
                    <asp:ListItem Value="RT" Selected="True">Recebimento Total </asp:ListItem>
                    <asp:ListItem Value="RP">Recebimento Parcial</asp:ListItem>
                    <asp:ListItem Value="RG">Recebimento Glosado</asp:ListItem>
                    <asp:ListItem Value="RA">Recebimento Em Aberto </asp:ListItem>
                    <asp:ListItem Value="NR">Não Recebido</asp:ListItem>
                    <asp:ListItem Value="NI">Não Informado</asp:ListItem>
                </asp:RadioButtonList>
            <hr id="Hr3" style="position:absolute; top: 120px; left: 5px; width: 486px; height:-15px;" runat="server"/>

            <asp:Label      ID="Label11"        runat="server" 
                style="position:absolute; top: 137px; left: 328px; width: 103px; height: 15px;" 
                Text="Data Recebimento"></asp:Label>
            <asp:TextBox    ID="txtDtRecebido"  runat="server" TabIndex="2"
                style="position:absolute; top: 154px; left: 328px; width: 96px; right: 76px;"></asp:TextBox> 
                <asp:MaskedEditExtender ID="MaskedEditExtender7" runat="server" Enabled="True" TargetControlID="txtDtRecebido" CultureName="pt-BR" Mask="99/99/9999" MaskType="Date" UserDateFormat="DayMonthYear" UserTimeFormat="TwentyFourHour"></asp:MaskedEditExtender>
                <asp:CalendarExtender ID="CalendarExtender4" runat="server" Enabled="True" TargetControlID="txtDtRecebido" Format="dd/MM/yyyy"></asp:CalendarExtender>

            <asp:Button ID="btnGravar" runat="server" Font-Bold="True" TabIndex="3"
                    style="position: absolute; top: 190px; left: 12px; height: 25px; width: 88px;"    
                    Text="Gravar" onclick="btnGravar_Click" />
            <asp:Button ID="btnVoltar" runat="server" Font-Bold="True"
                    style="position: absolute; top: 190px; left: 310px; height: 25px; width: 88px;"   
                    Text="Voltar" onclick="btnVoltar_Click" CausesValidation="false"/>

            <asp:Button ID="btnJustificativa" runat="server" Font-Bold="True"
                    style="position: absolute; top: 190px; left: 108px; height: 25px; width: 193px;"  
                    Text="Justificar Não Recebimento" 
                    CausesValidation="false" onclick="btnJustificativa_Click"/>
        </asp:Panel>

        <asp:Button ID="btnShowPopup" runat="server" style="display:none" />
            <asp:ModalPopupExtender ID="ModalPopupExtender" runat="server" TargetControlID="btnShowPopup" PopupControlID="pnlpopup" BackgroundCssClass="modalBackground"></asp:ModalPopupExtender>
        <asp:Panel ID="pnlpopup" runat="server" BackColor="White" Height="375px" style="display:none"
            Width="639px" BorderStyle="Solid" BorderWidth="1px">
            <asp:Label      ID="Label13"    runat="server" style="position:absolute; top: 5px; left: 7px; height: 20px; width: 589px; font-size:medium; text-align: center;" Text="Justificativa Não Recebimento Fatura"  
                                BackColor="White" Font-Bold="True" Font-Size="Large" ForeColor="#3333FF" Font-Names="Calibri"></asp:Label>

            <asp:Label      ID="Label2"        runat="server" 
                style="position:absolute; top: 217px; left: 16px; width: 199px;" 
                Text="Justificativa de Não Recebimento"></asp:Label>
            <asp:TextBox    ID="txtJustificativa"   runat="server" 
                style="position:absolute; top: 233px; left: 16px; width: 585px; height: 83px;"  
                TextMode="MultiLine"></asp:TextBox>

      <asp:Button ID="btnNovaJustifica" runat="server" 
                style="position: absolute; top: 34px; left: 18px; height: 25px; width: 127px;" 
                Text="Nova Justificativa"   onclick="btnNovaJustifica_Click"  
                CausesValidation="false"/>
      <asp:Button ID="btnGravarJustifica"        runat="server" 
                style="position:absolute;  top: 326px; left: 15px; width: 70px; height: 30px;"   
                Text="Gravar"               onclick="btnGravarJustifica_Click"         
                CausesValidation="false" CommandName="Update"/>
      <asp:Button ID="btnFechar"       runat="server" 
                style="position:absolute; top: 326px; left: 90px; width: 70px; height: 30px;"    
                Text="Fechar"               onclick="btnFechar_Click"        
                CausesValidation="false"/>
  
        <asp:GridView ID="grdJustifica" runat="server" 
                style="position:absolute; top: 65px; left: 16px; width: 597px;" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" 
                onpageindexchanging="grdJustifica_PageIndexChanging" OnRowDataBound="grdJustifica_RowDataBound" OnRowCommand="grdJustifica_RowCommand"
                ViewStateMode="Enabled" BorderColor="#003366" BorderStyle="Inset" 
                BorderWidth="1px" CellPadding="3" BackColor="White" Font-Size="X-Small" 
                PageSize="4">
                <Columns>
                     <asp:BoundField DataField="cmpIdFaturaObraEtapaJustifica" HeaderText="cmpIdFaturaObraEtapaJustifica">
                        <ItemStyle HorizontalAlign="Center" Width="10px" VerticalAlign="Middle"  />
                     </asp:BoundField>

                     <asp:TemplateField HeaderText="Ação">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkEditar" runat="server" Text="Editar" Font-Size="X-Small"
                                    CommandArgument='<%# Eval("cmpIdFaturaObraEtapaJustifica") %>' CommandName="Editar" CausesValidation="False">
                                </asp:LinkButton>
                                <asp:LinkButton ID="lnkExcluir" runat="server" Text="Excluir" Font-Size="X-Small"
                                    CommandArgument='<%# Eval("cmpIdFaturaObraEtapaJustifica") %>' CommandName="Excluir"                             
                                    CausesValidation="False">
                                </asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle Width="60px" Font-Size="X-Small" />
                            <HeaderStyle Font-Size="Small" />
                        </asp:TemplateField>
                    <asp:BoundField DataField="cmpDtEtapaJustifica" HeaderText="Data" 
                         DataFormatString="{0:dd/MM/yyyy hh:mm}">
                        <ItemStyle Width="80px" HorizontalAlign="Left" Font-Size="X-Small" />
                        <HeaderStyle Font-Size="Small" />
                    </asp:BoundField>
                    <asp:BoundField DataField="cmpDcFaturaObraEtapaJustifica" 
                         HeaderText="Justificativa">
                        <ItemStyle Width="400px" HorizontalAlign="Left" Font-Size="X-Small" />
                        <HeaderStyle Font-Size="Small" />
                    </asp:BoundField>
                </Columns>
            <FooterStyle BackColor="White" ForeColor="#000066" />
            <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" 
                    Font-Size="Medium" />
            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
            <RowStyle ForeColor="#000066"   Font-Size="Small" />
            <%--<SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />--%>
            <SortedAscendingCellStyle BackColor="#F1F1F1" />
            <SortedAscendingHeaderStyle BackColor="#007DBB" />
            <SortedDescendingCellStyle BackColor="#CAC9C9" />
            <SortedDescendingHeaderStyle BackColor="#00547E" />
      </asp:GridView>
  
    </asp:Panel>
</div>

</asp:Content>

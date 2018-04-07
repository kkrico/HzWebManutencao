<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="webPRE_Equipamento.aspx.cs" Inherits="HzWebManutencao.Preventiva.webPRE_Equipamento" %>
<%@ Register src="~/Controles/Mensagem.ascx" tagname="Mensagem" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Mensagem ID="CaixaMensagem" runat="server"/>
    <div  style="position:relative; height: 517px; width: 968px; top: 6px; left: -54px; margin-left: 40px;">
        <asp:Panel ID="pnlPreEquipamento" runat="server" 
            style="position:absolute; left:0px; width:962px; height:84px; top: 2px;">
            <asp:Label ID="lblAcao" runat="server" Text="Equipamentos da Obra" 
                style="position:absolute; top: 1px; left: 11px; height: 20px; width: 947px; text-align: center;" 
                BackColor="White" Font-Bold="True" Font-Size="Medium" ForeColor="Maroon"></asp:Label>

            <asp:Label ID="lblNuPreventiva" runat="server" Text="Número da Preventiva" 
                style="position:absolute; top: 27px; left: 9px; height: 20px; width: 186px; text-align: left;" 
                BackColor="White" Font-Bold="True" Font-Size="Medium" ForeColor="Maroon"></asp:Label>

            <asp:Label ID="lblNuPre" runat="server" 
                style="position:absolute; top: 47px; left: 9px; height: 20px; width: 173px; text-align: left;" 
                BackColor="White" Font-Bold="True" Font-Size="Medium" ForeColor="#CC0000"></asp:Label>

            <asp:Label ID="lblTipo" runat="server" Text="Tipo do Equipamento" 
                style="position:absolute; top: 27px; left: 214px; height: 20px; width: 235px; text-align: left;" 
                BackColor="White" Font-Bold="True" Font-Size="Medium" ForeColor="Maroon"></asp:Label>

            <asp:Label ID="lblTipoEqui" runat="server" 
                style="position:absolute; top: 47px; left: 214px; height: 20px; width: 235px; text-align: left;" 
                BackColor="White" Font-Bold="True" Font-Size="Medium" ForeColor="#CC0000"></asp:Label>

            <asp:Label ID="lblOS" runat="server" Text="Número Ordem Serviço" 
                style="position:absolute; top: 27px; left: 458px; height: 20px; width: 187px; text-align: left;" 
                BackColor="White" Font-Bold="True" Font-Size="Medium" ForeColor="Maroon"></asp:Label>

            <asp:Label ID="lblNuOS" runat="server" 
                style="position:absolute; top: 47px; left: 458px; height: 20px; width: 187px; text-align: left;" 
                BackColor="White" Font-Bold="True" Font-Size="Medium" ForeColor="#CC0000"></asp:Label>

            <asp:Label ID="lblSitua" runat="server" Text="Situação" 
                style="position:absolute; top: 27px; left: 679px; height: 20px; width: 267px; text-align: left;" 
                BackColor="White" Font-Bold="True" Font-Size="Medium" ForeColor="Maroon"></asp:Label>

            <asp:Label ID="lblDcSitua" runat="server" 
                style="position:absolute; top: 47px; left: 680px; height: 20px; width: 262px; text-align: left;" 
                BackColor="White" Font-Bold="True" Font-Size="Medium" ForeColor="#CC0000"></asp:Label>

         </asp:Panel>

        <asp:GridView ID="grdPesquisa" runat="server" 
            style="position:absolute; top: 91px; left: 12px; width: 952px;" AllowPaging="True" 
            AllowSorting="True" AutoGenerateColumns="False" 
            onpageindexchanging="grdPesquisa_PageIndexChanging" 
            ViewStateMode="Enabled" 
            BorderColor="#003366" BorderStyle="Inset" BorderWidth="1px"
            CellPadding="3" BackColor="White" Font-Size="Small" PageSize="12" 
            onrowdatabound="grdPesquisa_RowDataBound">
            <Columns>
                 <asp:TemplateField HeaderText="Operação">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkSelecionar" runat="server" Text="Selecionar" 
                                CommandArgument='<%# Eval("cmpIdEquipamentoObra") %>' CommandName="Selecionar"                             
                                OnClick="lnkSelecionar_Click" CausesValidation="False">
                            </asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle Width="50px" HorizontalAlign="Left" VerticalAlign="Middle" />
                    </asp:TemplateField>
                <asp:BoundField DataField="CmpDcEquipamentoObra" HeaderText="Descrição Equipamento">
                    <ItemStyle Width="100px" HorizontalAlign="Left" Font-Size="Small" />
                </asp:BoundField>
                <asp:BoundField DataField="CodEquipamento" HeaderText="Código">
                    <ItemStyle Width="40px" HorizontalAlign="Right" Font-Size="Small" />
                </asp:BoundField>
                <asp:BoundField DataField="cmpDcTipoAtividade" HeaderText="Tipo">
                    <ItemStyle Width="100px" HorizontalAlign="Left" Font-Size="Small" />
                </asp:BoundField>
                <asp:BoundField DataField="cmpDcMarcaModeloEquipamento" HeaderText="Marca/Modelo">
                    <ItemStyle Width="100px" HorizontalAlign="Right" Font-Size="Small" />
                </asp:BoundField>
                <asp:BoundField DataField="cmpNuSerieEquipamento" HeaderText="Nº Série">
                    <ItemStyle Width="10px" HorizontalAlign="Left" Font-Size="Small" />
                </asp:BoundField>
                <asp:BoundField DataField="cmpDcPavimento" HeaderText="Pavimento">
                    <ItemStyle Width="50px" HorizontalAlign="Right" Font-Size="Small" />
                </asp:BoundField>
                <asp:BoundField DataField="cmpDcLocalEquipamento" HeaderText="Localização">
                    <ItemStyle Width="150px" HorizontalAlign="Left" Font-Size="Small" />
                </asp:BoundField>
                <asp:BoundField DataField="cmpIdManutencaoEquipamento" HeaderText="">
                    <ItemStyle Width="10px" HorizontalAlign="Left" Font-Size="Small" />
                </asp:BoundField>
                <asp:BoundField DataField="cmpDtManutencaoEquipamento" HeaderText="">
                    <ItemStyle Width="10px" HorizontalAlign="Left" Font-Size="Small" />
                </asp:BoundField>
                <asp:BoundField DataField="cmpDcDetalhamentoManutencao" HeaderText="">
                    <ItemStyle Width="10px" HorizontalAlign="Left" Font-Size="Small" />
                </asp:BoundField>
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

        <asp:Button ID="btnShowPopup1" runat="server" style="display:none" />
        <asp:ModalPopupExtender 
            ID="ModalPopupExtender1" 
            runat="server" 
            TargetControlID="btnShowPopup1" 
            PopupControlID="pnlpopup1"
            BackgroundCssClass="modalBackground"></asp:ModalPopupExtender>
            
       <asp:Panel ID="pnlpopup1" runat="server" BackColor="White" Height="379px" Width="750px" BorderStyle="Solid" 
                    BorderWidth="1px" style="display:none" >
            <asp:Label ID="Label4" runat="server" Text="Manutenção Equipamento"
                        style="position:absolute; top: 5px; left: 6px; height: 20px; width: 700px; font-size:medium; text-align: center;" 
                        BackColor="White" Font-Bold="True" Font-Size="Large" 
                        ForeColor="#3333FF" Font-Names="Calibri"></asp:Label>
            <asp:Label ID="lblDescricao" runat="server" Text="Descrição do Equipamento"
                        style="position:absolute; top: 34px; left: 15px; height: 16px; width: 150px;" 
                        BackColor="White" Font-Bold="True" Font-Size= "Small" 
                        ForeColor="#3333FF" Font-Names="Calibri"></asp:Label>
            <asp:TextBox ID="TxtDescricao" runat="server" 
                        style="position:absolute; top: 49px; left: 15px; width: 335px; height: 54px; " 
                        Font-Names="Calibri" Font-Size="Small" TextMode="MultiLine"></asp:TextBox>
            <asp:Label ID="lblCodigo" runat="server" Text="Código do Equipamento"
                        style="position:absolute; top: 34px; left: 360px; height: 17px; width: 135px;" 
                        BackColor="White" Font-Bold="True" Font-Size= "Small" 
                        ForeColor="#3333FF" Font-Names="Calibri"></asp:Label>
           <asp:TextBox ID="txtCodigoEquip" runat="server" Font-Names="Calibri" Font-Size="Small" 
                        style="position:absolute; top: 49px; left: 359px; width: 189px; height: 17px;"></asp:TextBox>
            <asp:Label ID="lblTipoEquip" runat="server" Text="Tipo Equipamento"
                        style="position:absolute; top: 34px; left: 559px; height: 15px; width: 113px;" 
                        BackColor="White" Font-Bold="True" Font-Size="Small" 
                        ForeColor="#3333FF" Font-Names="Calibri"></asp:Label>
             <asp:TextBox ID="txtTpEquipamento" runat="server" 
                        style="position:absolute; top: 49px; left: 561px; width: 170px; height: 17px;" 
                        Font-Names="Calibri" Font-Size="Small"></asp:TextBox>
            <asp:Label ID="lblMarca" runat="server" Text="Marca / Modelo"
                        style="position:absolute; top: 73px; left: 362px; height: 17px; width: 98px; right: 508px;" 
                        BackColor="White" Font-Bold="True" Font-Size="Small" 
                        ForeColor="#3333FF" Font-Names="Calibri"></asp:Label>
            <asp:TextBox ID="txtMarcaModelo" runat="server"
                        style="position:absolute; top: 92px; left: 359px; width: 183px; height: 17px;" 
                        Font-Names="Calibri" Font-Size="Small"></asp:TextBox>
            <asp:Label ID="lblNumeroSerie" runat="server" Text="Número de Série"
                        style="position:absolute; top: 73px; left: 565px; height: 17px; width: 98px; right: 305px;" 
                        BackColor="White" Font-Bold="True" Font-Size="Small" 
                        ForeColor="#3333FF" Font-Names="Calibri"></asp:Label>
            <asp:TextBox ID="txtNumeroSerie" runat="server"
                        style="position:absolute; top: 92px; left: 563px; width: 174px; height: 17px;" 
                        Font-Names="Calibri" Font-Size="Small"></asp:TextBox>
            <asp:Label ID="lblPavimento" runat="server" Text="Pavimento"
                        style="position:absolute; top: 118px; left: 19px; height: 17px; width: 98px;" 
                        BackColor="White" Font-Bold="True" Font-Size= "Small" 
                        ForeColor="#3333FF" Font-Names="Calibri"></asp:Label>
            <asp:TextBox ID="txtPavimento" runat="server" Font-Names="Calibri" Font-Size="Small" 
                        style="position:absolute; top: 134px; left: 19px; width: 325px; height: 18px;"></asp:TextBox>
            <asp:Label ID="lblLocalizacao" runat="server" Text="Localização"
                        style="position:absolute; top: 120px; left: 360px; height: 16px; width: 111px;" 
                        BackColor="White" Font-Bold="True" Font-Size= "Small" 
                        ForeColor="#3333FF" Font-Names="Calibri"></asp:Label>
            <asp:TextBox ID="txtLocalizacao" runat="server"
                        style="position:absolute; top: 135px; left: 362px; width: 365px; height: 18px;" 
                        Font-Names="Calibri" Font-Size="Small"></asp:TextBox>
            <asp:Label ID="lblDtManutencao" runat="server" Text="Data Manutencao"
                        style="position:absolute; top: 160px; left: 22px; width: 111px;" 
                        BackColor="White" Font-Bold="True" Font-Size= "Small" 
                        ForeColor="#3333FF" Font-Names="Calibri"></asp:Label>
            <asp:TextBox ID="txtDtManutencao" runat="server"
                style="position:absolute; top: 180px; left: 22px; width: 100px;"></asp:TextBox>
                        <asp:MaskedEditExtender ID="txtDtManutencao_MaskedEditExtender" runat="server" 
                            Enabled="True" 
                            TargetControlID="txtDtManutencao" CultureName="pt-BR" 
                            Mask="99/99/9999" MaskType="Date" 
                            AcceptAMPM="true" MessageValidatorTip="false"
                            UserDateFormat="None"></asp:MaskedEditExtender>
                        <asp:CalendarExtender ID="txtDtManutencao_CalendarExtender" runat="server" 
                            Enabled="True" 
                            TargetControlID="txtDtManutencao" 
                            Format="dd/MM/yyyy"></asp:CalendarExtender>
                       <asp:RequiredFieldValidator ID="RvftxtDtManutencao" runat="server"  
                            ControlToValidate="txtDtManutencao"
                            style="position:absolute; top: 333px; left: 345px; height: 22px; width: 157px; "
                            ErrorMessage="Data da manutenção em branco!" Display="None"></asp:RequiredFieldValidator>
           <asp:Label ID="lblDetalhamento" runat="server" Text="Detalhamento Manutenção"
                        style="position:absolute; top: 206px; left: 24px; height: 16px; width: 179px;" 
                        BackColor="White" Font-Bold="True" Font-Size= "Small" 
                        ForeColor="#3333FF" Font-Names="Calibri"></asp:Label>
            <asp:TextBox ID="txtDetalhamento" runat="server" 
                        style="position:absolute; top: 226px; left: 21px; width: 711px; height: 102px;" 
                        Font-Names="Calibri" Font-Size="Small" TextMode="MultiLine" 
                TabIndex="1"></asp:TextBox>
                       <asp:RequiredFieldValidator ID="RvftxtDetalhamento" runat="server"  
                            ControlToValidate="txtDetalhamento"
                            style="position:absolute; top: 333px; left: 345px; height: 22px; width: 157px; "
                            ErrorMessage="Detalhamento da manutenção em branco!" Display="None"></asp:RequiredFieldValidator>
    
            <asp:ValidationSummary ID="ValidationSummary" runat="server" 
                        style="position:absolute; top: 50px; left: 774px; width: 200px; height: 26px;"
                        ShowMessageBox="True" ShowSummary="False" />

            <asp:Button ID="btnUpdate" CommandName="Update" runat="server" Text="Gravar" 
                        style="position:absolute; top: 342px; left: 19px; width: 70px; height: 30px;" 
                        onclick="btnUpdate_Click"/>
            <asp:Button ID="btnExcluir" runat="server" Text="Excluir" CausesValidation="false"
                        style="position:absolute; top: 342px; left: 94px; height: 30px; width: 70px;" 
                        onclick="btnExcluir_Click" />
            <asp:Button ID="btnCancel" runat="server" Text="Cancelar" CausesValidation="false"
                        style="position:absolute; top: 342px; left: 170px; height: 30px; width: 70px;" 
                        onclick="btnCancel_Click" />
        </asp:Panel>

        <asp:Panel ID="pnlVincularOS" runat="server" 
            style="position:absolute; left:8px; width:953px; height:54px; top: 439px;">
            <asp:Button ID="btnVincularOS" runat="server" Text="Ordem de Serviço"
                        CausesValidation="False"
                        style="position:absolute; top: 13px; left: 10px; width: 121px; height: 31px;" 
                        onclick="btnVincularOS_Click" />
        </asp:Panel>
   </div>
</asp:Content>

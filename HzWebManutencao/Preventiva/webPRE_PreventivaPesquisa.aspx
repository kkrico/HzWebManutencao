<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="webPRE_PreventivaPesquisa.aspx.cs" Inherits="HzWebManutencao.Preventiva.webPRE_PreventivaPesquisa" %>
<%@ Register src="~/Controles/Mensagem.ascx" tagname="Mensagem" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Mensagem ID="CaixaMensagem" runat="server" />
    <div style="position:relative; height: 499px; width: 980px; top: 6px; left: -54px; margin-left: 40px;">
        <asp:Label ID="lblAcao" runat="server" Text="Confirma Execução da Manutenção Preventiva"
            style="position:absolute; top: 4px; left: 6px; height: 21px; width: 969px; text-align: center;" 
            BackColor="White" Font-Bold="True" Font-Size="Large" ForeColor="Maroon" 
            Font-Names="Calibri"></asp:Label>

        <asp:Label ID="Label5" runat="server" Text="Obra" Font-Size="X-Small"
            style="position:absolute; top: 30px; left: 12px; height: 15px; width: 33px;"> </asp:Label>
        <asp:DropDownList ID="cmbObra" runat="server" Font-Size="X-Small"
            style="position:absolute; top: 47px; left: 12px; width: 593px; "
            AutoPostBack="True" 
            onselectedindexchanged="cmbObra_SelectedIndexChanged" >
        </asp:DropDownList>

        <asp:Label ID="Label3" runat="server" Text="Data Inicial" Font-Size="X-Small"
            
            
            style="position:absolute; top: 30px; left: 617px; width: 70px; "></asp:Label>
        <asp:TextBox ID="txtDataInicial" runat="server" Font-Size="X-Small"
            style="position:absolute; top: 46px; left: 616px; width: 90px; " 
            AutoPostBack="True" ontextchanged="txtData_TextChanged"></asp:TextBox> 
                <asp:MaskedEditExtender ID="MaskedEditExtender4" runat="server" 
                    Enabled="True" 
                    TargetControlID="txtDataInicial" CultureName="pt-BR" 
                    Mask="99/99/9999" MaskType="Date" UserDateFormat="DayMonthYear" 
                    UserTimeFormat="TwentyFourHour"></asp:MaskedEditExtender>
                <asp:CalendarExtender ID="txtDataInicial_CalendarExtender" runat="server" 
                    Enabled="True" TargetControlID="txtDataInicial" Format="dd/MM/yyyy"></asp:CalendarExtender>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                style="position:absolute; top: 99px; left: 424px; width: 200px; height: 26px;" 
                ErrorMessage="Data inicial em branco!" 
                ControlToValidate="txtDataInicial"></asp:RequiredFieldValidator>

        <asp:Label ID="Label16" runat="server" Text="Data Final" Font-Size="X-Small"
            
            
            style="position:absolute; top: 30px; left: 723px; width: 63px;"></asp:Label>
        <asp:TextBox ID="txtDataFinal" runat="server" Font-Size="X-Small"
            style="position:absolute; top: 45px; left: 722px; width: 90px;" 
            AutoPostBack="True" ontextchanged="txtDataFinal_TextChanged"></asp:TextBox> 
                <asp:MaskedEditExtender ID="txtDataFinal_MaskedEditExtender" runat="server" 
                    Enabled="True" 
                    TargetControlID="txtDataFinal" CultureName="pt-BR" 
                    Mask="99/99/9999" MaskType="Date" UserDateFormat="DayMonthYear" 
                    UserTimeFormat="TwentyFourHour"></asp:MaskedEditExtender>
                <asp:CalendarExtender ID="txtDataFinal_CalendarExtender" runat="server" 
                    Enabled="True" TargetControlID="txtDataFinal" Format="dd/MM/yyyy"></asp:CalendarExtender>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                    style="position:absolute; top: 98px; left: 658px; width: 200px; height: 26px;" 
                    ErrorMessage="Data final em branco!" 
                    ControlToValidate="txtDataFinal"></asp:RequiredFieldValidator>
        <asp:Label ID="Label14" runat="server" Text="Tipo Preventiva " Font-Size="X-Small"
            
            
            
            style="position:absolute; left: 12px; top: 72px; height: 15px; width: 101px;"></asp:Label>
        <asp:RadioButtonList ID="rdbType" runat="server" Font-Size="X-Small"
                style="position:absolute; top: 87px; left: 8px; width: 202px; right: 770px; height: 20px;" 
                onselectedindexchanged="rdbType_SelectedIndexChanged" 
                RepeatDirection="Horizontal" RepeatLayout="Flow" AutoPostBack="True" 
            CellPadding="0" CellSpacing="0" TabIndex="2">
            <asp:ListItem Value="P">Pavimento</asp:ListItem>
            <asp:ListItem Value="E">Equipamento</asp:ListItem>
            <asp:ListItem Value="T" Selected="True">Todos</asp:ListItem>
        </asp:RadioButtonList>

        <asp:Label ID="Label19" runat="server" Text="Periodicidade" Font-Size="X-Small"
            style="position:absolute; left: 220px; top: 72px;"></asp:Label>
        <asp:DropDownList ID="cmbPeriodicidade" runat="server" Font-Size="X-Small"
            style="position:absolute; left: 219px; top: 88px; width: 133px;" 
            AutoPostBack="True" 
            onselectedindexchanged="cmbPeriodicidade_SelectedIndexChanged">
        </asp:DropDownList>

        <asp:Label ID="Label23" runat="server" Text="Tipo de Atividade " Font-Size="X-Small"
            style="position:absolute; left: 362px; top: 72px; right: 499px;"></asp:Label>

        <asp:DropDownList ID="cmbTipoAtividade" runat="server" Font-Size="X-Small"
            style="position:absolute; top: 88px; left: 361px; width: 244px; " 
            AutoPostBack="True" 
            onselectedindexchanged="cmbTipoAtividade_SelectedIndexChanged"  >
        </asp:DropDownList>

        <asp:Label ID="Label25" runat="server" Text="Situação " Font-Size="X-Small"
            style="position:absolute; left: 618px; top: 72px"></asp:Label>

        <asp:DropDownList ID="cmbSituacao" runat="server" Font-Size="X-Small"
            style="position:absolute; top: 88px; left: 616px; width: 121px; " 
            AutoPostBack="True" 
            onselectedindexchanged="cmbSituacao_SelectedIndexChanged" >
            <asp:ListItem Value="0">-- Selecione --</asp:ListItem>
            <asp:ListItem Value="A">Em Aberto</asp:ListItem>
            <asp:ListItem Value="S">Confirmada</asp:ListItem>
            <asp:ListItem Value="N">Expirada</asp:ListItem>
        </asp:DropDownList>



        <asp:Button ID="btnPesquisar" runat="server" Text="Pesquisar" CausesValidation="False"
                style="position:absolute; top: 25px; left: 843px; width: 109px; height: 26px; right: 28px;" 
                onclick="btnPesquisar_Click" />

        <asp:Button ID="btnImprimir" runat="server" Text="Imprimir Agenda" CausesValidation="false"
            
            style="position:absolute; top: 56px; left: 843px; width: 110px; height: 26px; right: 27px;" 
            onclick="btnImprimir_Click"/>

        <asp:Button ID="btnResumoPreventiva" runat="server" Text="Resumo Prevent." CausesValidation="false"
            
            style="position:absolute; top: 85px; left: 844px; width: 110px; height: 26px; right: 26px;" 
            onclick="btnResumoPreventiva_Click"/>

       <asp:GridView ID="grdPreventivaPesq" runat="server" 
            style="position:absolute; top: 113px; left: 3px; width: 950px;" AllowPaging="True" 
            AllowSorting="True" AutoGenerateColumns="False" 
            onpageindexchanging="grdPreventivaPesq_PageIndexChanging" 
            ViewStateMode="Enabled" 
            BorderColor="#003366" BorderStyle="Inset" BorderWidth="1px"
            CellPadding="3" BackColor="White" Font-Size="Small" PageSize="14" 
            onrowdatabound="grdPreventivaPesq_RowDataBound" 
            onrowcommand="grdPreventivaPesq_RowCommand" 
            DataKeyNames="cmpIdPreventivaAgenda,cmpTpPreventiva,cmpCoTipoAtividade">
            <Columns>
                 <asp:TemplateField HeaderText="Selecionar">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkConfirmar" runat="server" Text="Confirmar" 
                                CommandArgument='<%# Eval("cmpIdPreventivaAgenda") %>' CommandName="confirmar"                             
                                OnClick="lnkConfirmar_Click" CausesValidation="False">
                            </asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle Width="20px" HorizontalAlign="Left" VerticalAlign="Middle" />
                    </asp:TemplateField>
                <asp:BoundField DataField="cmpNuPreventivaAgenda" HeaderText="Preventiva">
                    <ItemStyle Width="60px" HorizontalAlign="Right" Font-Size="X-Small" />
                </asp:BoundField>
                <asp:BoundField DataField="cmpDtReprogramacaoPreventivaAgenda" HeaderText="Data"  DataFormatString="{0:dd/MM/yyyy}">
                    <ItemStyle Width="60px" HorizontalAlign="Right" Font-Size="X-Small" />
                </asp:BoundField>
                <asp:BoundField DataField="cmpCoPeriodicidade" HeaderText="CoPeriodicidade">
                    <ItemStyle Width="100px" HorizontalAlign="Left" Font-Size="X-Small" />
                </asp:BoundField>
                <asp:BoundField DataField="cmpDcPeriodicidade" HeaderText="Periodicidade">
                    <ItemStyle Width="100px" HorizontalAlign="Left" Font-Size="X-Small" />
                </asp:BoundField>
                <asp:BoundField DataField="cmpDcTipoAtividade" HeaderText="Tipo Atividade">
                    <ItemStyle Width="100px" HorizontalAlign="Left" Font-Size="X-Small" />
                </asp:BoundField>
                <asp:BoundField DataField="TpPreventiva" HeaderText="Tipo Preventiva">
                    <ItemStyle Width="100px" HorizontalAlign="Left" Font-Size="X-Small" />
                </asp:BoundField>
                <asp:BoundField DataField="EstadoManutencaoPreventiva" HeaderText="Situação">
                    <ItemStyle Width="50px" HorizontalAlign="Center" Font-Size="X-Small" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="Inconforme">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" visible="false"
                            CommandArgument='<%# Eval("cmpIdOS") %>' 
                            CommandName="LinkButton1"><%#Eval("cmpNuOS") %></asp:LinkButton>
                        <asp:LinkButton ID="LinkButton2" runat="server" Text="Vincular O.S" visible="false"
                            CommandArgument= '<%# Eval("cmpIdPreventivaConfirmacao") + "$" + Eval("cmpNuPreventivaAgenda") %>' 
                            CommandName="LinkButton2"></asp:LinkButton>
                        <asp:LinkButton ID="LinkButton3" runat="server" Text="Vincular Equipamento" visible="false"
                            CommandArgument= '<%# Eval("cmpCoTipoAtividade") + "$" + Eval("cmpNuPreventivaAgenda") + "$" + Eval("cmpIdPreventivaConfirmacao") %>' 
                            CommandName="LinkButton3"></asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle Width="60px" HorizontalAlign="Right" VerticalAlign="Middle" Font-Size="X-Small" />
                </asp:TemplateField>
                <asp:BoundField DataField="cmpIdOS" HeaderText="cmpIdOS">
                    <ItemStyle Width="1px" HorizontalAlign="Center" Font-Size="Small" /></asp:BoundField>
                <asp:BoundField DataField="cmpIdPreventivaConfirmacao" HeaderText="cmpIdPreventivaConfirmacao">
                    <ItemStyle Width="1px" HorizontalAlign="Center" Font-Size="Small" />
                </asp:BoundField>
               <asp:BoundField DataField="cmpInOcorrenciaPreventiva" HeaderText="cmpInOcorrenciaPreventiva">
                    <ItemStyle Width="1px" HorizontalAlign="Center" Font-Size="Small" />
                </asp:BoundField>

                 <asp:TemplateField HeaderText="Selecionar">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkReprogramar" runat="server" Text="Reprogramar" 
                                CommandArgument='<%# Eval("cmpIdPreventivaAgenda") %>' CommandName="reprogramar"                             
                                OnClick="lnkReprogramar_Click" CausesValidation="False">
                            </asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle Width="80px" HorizontalAlign="Left" VerticalAlign="Middle" Font-Size="X-Small"/>
                    </asp:TemplateField>
                    <asp:BoundField DataField="cmpCoTipoAtividade" HeaderText="cmpCoTipoAtividade">
                        <ItemStyle Width="1px" HorizontalAlign="Center" Font-Size="Small" />
                    </asp:BoundField>
                    <asp:BoundField DataField="cmpIdManutencaoEquipamento" HeaderText="cmpIdManutencaoEquipamento">
                        <ItemStyle Width="10px" HorizontalAlign="Left" Font-Size="Small" />
                    </asp:BoundField>
                    <asp:BoundField DataField="cmpDtReprogramacaoPreventivaAgenda" HeaderText="cmpDtReprogramacaoPreventivaAgenda">
                        <ItemStyle Width="10px" HorizontalAlign="Left" Font-Size="Small" />
                    </asp:BoundField>

                 <asp:TemplateField HeaderText="Vizualizar Preventiva">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkVizualizar" runat="server" Text="Vizualizar" 
                                CommandArgument='<%# Container.DataItemIndex %>' CommandName="vizualizar"                             
                                OnClick="lnkEditarPreventiva_Click" CausesValidation="False">
                            </asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle Width="20px" HorizontalAlign="Left" VerticalAlign="Middle" Font-Size="X-Small"/>
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
           
        <asp:Button ID="btnShowPopup" runat="server" style="display:none" />
            <asp:ModalPopupExtender 
                ID="ModalPopupExtender1" 
                runat="server" 
                TargetControlID="btnShowPopup" 
                PopupControlID="pnlpopup"
                BackgroundCssClass="modalBackground"></asp:ModalPopupExtender>

        <asp:Panel ID="pnlpopup" runat="server" BackColor="White" Height="370px" Width="600px" style="display:none" 
                    BorderStyle="Solid" BorderWidth="1px">
            <asp:Label ID="Label1" runat="server" Text="Confirma Execução da Manutenção Preventiva "
                        style="position:absolute; top: 5px; left: 7px; height: 20px; width: 589px; font-size:medium; text-align: center;" 
                        BackColor="White" Font-Bold="True" Font-Size="Large" 
                        ForeColor="#3333FF" Font-Names="Calibri"></asp:Label>
            <asp:Label ID="Label6" runat="server" Text="Tipo Atividade : "
                        style="position:absolute; top: 38px; left: 10px; height: 20px; width: 100px;" 
                        BackColor="White" Font-Bold="True" Font-Size="Small" 
                        ForeColor="#3333FF" Font-Names="Calibri"></asp:Label>
            <asp:Label ID="lblTipoAtividade" runat="server"
                        style="position:absolute; top: 38px; left: 100px; height: 20px; width: 200px;" 
                        BackColor="White" Font-Bold="True" Font-Size= "Small" 
                        ForeColor="#3333FF" Font-Names="Calibri"></asp:Label>
            <asp:Label ID="lblTipoPreventiva" runat="server" 
                        style="position:absolute; top: 38px; left: 322px; height: 20px; width: 200px;" 
                        BackColor="White" Font-Bold="True" Font-Size= "Small" 
                        ForeColor="#3333FF" Font-Names="Calibri"></asp:Label>
            <asp:Label ID="Label15" runat="server" Text="Data Agendada"
                        style="position:absolute; top: 73px; left: 13px; height: 16px; width: 106px;" 
                        BackColor="White" Font-Bold="True" Font-Size= "Small" 
                        ForeColor="#3333FF" Font-Names="Calibri"></asp:Label>
            <asp:TextBox ID="txtDtAgenda" runat="server" 
                        style="position:absolute; top: 94px; left: 13px; width: 106px;" 
                        BackColor="White" Font-Bold="True" Font-Size= "Small" 
                        ForeColor="#3333FF" Font-Names="Calibri"></asp:TextBox>
                        <asp:MaskedEditExtender ID="MaskedEditExtender3" runat="server" 
                            Enabled="True" 
                            TargetControlID="txtDtAgenda" CultureName="pt-BR" 
                            Mask="99/99/9999" MaskType="Date"  UserDateFormat="DayMonthYear"
                            MessageValidatorTip="false"></asp:MaskedEditExtender>
           <asp:Label ID="Label11" runat="server" Text="Prazo Execução "
                        style="position:absolute; top:73px; left: 158px; height: 17px; width: 90px;" 
                        BackColor="White" Font-Bold="True" Font-Size="Small" 
                        ForeColor="#3333FF" Font-Names="Calibri"></asp:Label>
            <asp:Label ID="lblDiasExecucao" runat="server" Text="5 dias"
                        style="position:absolute; top: 92px; left: 157px; height: 18px; width: 81px; text-align: center;" 
                        BackColor="White" Font-Bold="True" Font-Size= "Small" 
                        ForeColor="#3333FF" Font-Names="Calibri"></asp:Label>
            <asp:Label ID="Label18" runat="server" Text="Limite Confirmação"
                        style="position:absolute; top: 73px; left: 280px; height: 16px; width: 106px;" 
                        BackColor="White" Font-Bold="True" Font-Size= "Small" 
                        ForeColor="#3333FF" Font-Names="Calibri"></asp:Label>
            <asp:TextBox ID="txtLimiteConfirma" runat="server" 
                        style="position:absolute; top: 92px; left: 279px; width: 100px;" 
                        Font-Names="Calibri" Font-Size="Small"></asp:TextBox>
                        <asp:MaskedEditExtender ID="MaskedEditExtender2" runat="server" 
                            Enabled="True" 
                            TargetControlID="txtLimiteConfirma" CultureName="pt-BR" 
                            Mask="99/99/9999" MaskType="Date"  UserDateFormat="DayMonthYear"
                            MessageValidatorTip="false"></asp:MaskedEditExtender>
            <asp:Label ID="lblDtConfirmacao" runat="server" Text="Data Confirmação: "
                        style="position:absolute; top: 73px; left: 412px; height: 16px; width: 106px;" 
                        BackColor="White" Font-Bold="True" Font-Size= "Small" 
                        ForeColor="#3333FF" Font-Names="Calibri"></asp:Label>
            <asp:TextBox ID="TxtDtConfirmacao" runat="server" 
                        style="position:absolute; top: 92px; left: 411px; width: 100px;" 
                        Font-Names="Calibri" Font-Size="Small"></asp:TextBox>
                        <asp:MaskedEditExtender ID="TxtDtConfirmacao_MaskedEditExtender" runat="server" 
                            Enabled="True" 
                            TargetControlID="TxtDtConfirmacao" CultureName="pt-BR" 
                            Mask="99/99/9999" MaskType="Date"  UserDateFormat="DayMonthYear"
                            MessageValidatorTip="false"></asp:MaskedEditExtender>
                        <asp:CalendarExtender ID="TxtDtConfirmacao_CalendarExtender" runat="server" 
                            Enabled="True" TargetControlID="TxtDtConfirmacao" Format="dd/MM/yyyy"></asp:CalendarExtender>

<%--                        <asp:RequiredFieldValidator ID="RvfDtConfirmacao" runat="server"  
                            ControlToValidate="TxtDtConfirmacao"
                            style="position:absolute; top: 298px; left: 343px; height: 22px; width: 157px; "
                            ErrorMessage="Data de confirmação da preventiva em branco!" 
                            Display="None"></asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="RvfDtConfirmacao1" runat="server" 
                            style="position:absolute; top: 176px; left: 223px; width: 147px;"
                            ErrorMessage="Data confirmação menor que data agendada!" 
                            ControlToCompare="txtDtAgenda" ControlToValidate="TxtDtConfirmacao"  
                            Display="None" Operator= "GreaterThanEqual"
                            Type="Date">
                        </asp:CompareValidator>
                        <asp:CompareValidator ID="RvfDtConfirmacao2" runat="server" 
                            style="position:absolute; top: 176px; left: 223px; width: 147px;"
                            ErrorMessage="Data confirmação maior que data limite para confirmação!" 
                            ControlToCompare="txtLimiteConfirma" ControlToValidate="TxtDtConfirmacao" 
                            Display="None" Operator= "LessThanEqual"
                            Type="Date">
                        </asp:CompareValidator>
--%>
            <asp:Label ID="Label2" runat="server" Text="Gestor "
                        style="position:absolute; top: 130px; left: 10px; height: 20px; width: 87px;" 
                        BackColor="White" Font-Bold="True" Font-Size="Small" 
                        ForeColor="#3333FF" Font-Names="Calibri"></asp:Label>
            <asp:TextBox ID="TxtGestor" runat="server" 
                        style="position:absolute; top: 148px; left: 10px; width: 566px;" 
                        Font-Names="Calibri" Font-Size="Small"></asp:TextBox>

<%--                        <asp:RequiredFieldValidator ID="RvfTxtGestor" runat="server"  
                            ControlToValidate="TxtGestor"
                            style="position:absolute; top: 313px; left: 343px; height: 22px; width: 157px; "
                            ErrorMessage="Nome do gestor em branco!" Display="None"></asp:RequiredFieldValidator>
--%>
            <asp:Label ID="Label4" runat="server" Text="Funcionário "
                        style="position:absolute; top: 177px; left: 10px; height: 23px; width: 98px; right: 872px;" 
                        BackColor="White" Font-Bold="True" Font-Size="Small" 
                        ForeColor="#3333FF" Font-Names="Calibri"></asp:Label>
            <asp:TextBox ID="txtFuncionario" runat="server"
                        style="position:absolute; top: 194px; left: 10px; width: 566px;" 
                        Font-Names="Calibri" Font-Size="Small"></asp:TextBox>

<%--                        <asp:RequiredFieldValidator ID="RvftxtFuncionario" runat="server"  
                            ControlToValidate="txtFuncionario"
                            style="position:absolute; top: 333px; left: 345px; height: 22px; width: 157px; "
                            ErrorMessage="Nome do funcionário em branco!" Display="None"></asp:RequiredFieldValidator>
--%>
            <asp:Label ID="Label7" runat="server" Text="Observação "
                        style="position:absolute; top: 218px; left: 11px; height: 20px; width: 80px;" 
                        BackColor="White" Font-Bold="True" Font-Size="Small" 
                        ForeColor="#3333FF" Font-Names="Calibri"></asp:Label>
            <asp:TextBox ID="txtObservacao" runat="server"
                        style="position:absolute; top: 235px; left: 10px; width: 563px; height: 51px;" 
                        TextMode="MultiLine" Font-Names="Calibri" Font-Size="Small"></asp:TextBox>
            <asp:Label ID="Label8" runat="server" Text="Ocorrência :"
                        style="position:absolute; top: 297px; left: 10px; height: 20px; width: 80px;" 
                        BackColor="White" Font-Bold="True" Font-Size="Small" 
                        ForeColor="#3333FF" Font-Names="Calibri"></asp:Label>
           <asp:RadioButtonList ID="rdbState" runat="server"  
                        style="position:absolute; top: 293px; left: 85px; width: 130px;" RepeatColumns="2" 
                        RepeatDirection="Horizontal" Font-Names="Calibri" 
                Font-Size="Small">
                        <asp:ListItem Selected="True" Value="0">Não OK</asp:ListItem>
                        <asp:ListItem Value="1">OK</asp:ListItem></asp:RadioButtonList>

<%--                         <asp:RequiredFieldValidator ID="rvfrdbState" runat="server"  
                            ControlToValidate="rdbState"
                            style="position:absolute; top: 301px; left: 461px; height: 22px; width: 127px; "
                            ErrorMessage="Ocorrência em branco!" Display="None"></asp:RequiredFieldValidator>
--%>
            <asp:Button ID="btnUpdate" CommandName="Update" runat="server" Text="Gravar"
                        style="position:absolute; top: 330px; left: 10px; width: 70px; height: 30px;" 
                        CausesValidation="false"
                        onclick="btnUpdate_Click"/>
            <asp:Button ID="btnCancel1" runat="server" Text="Cancelar" CausesValidation="false"
                        style="position:absolute; top: 330px; left: 90px; width: 70px; height: 30px;" 
                        onclick="btnCancel1_Click" />
        </asp:Panel>

        <asp:Button ID="btnpopUpReprogramar" runat="server" style="display:none" />
            <asp:ModalPopupExtender 
                ID="ModalPopupExtender2" 
                runat="server" 
                TargetControlID="btnpopUpReprogramar" 
                PopupControlID="pnlpopupReprogramar"
                BackgroundCssClass="modalBackground"></asp:ModalPopupExtender>

        <asp:Panel ID="pnlpopupReprogramar" runat="server" BackColor="White" style="display:none"
                    Height="205px" Width="541px" BorderStyle="Solid" BorderWidth="1px">
            <asp:Label ID="Label9" runat="server" Text="Reprogramação da Manutenção Preventiva "
                        style="position:absolute; top: 5px; left: 7px; height: 20px; width: 531px; font-size:medium; text-align: center;" 
                        BackColor="White" Font-Bold="True" Font-Size="Large" 
                        ForeColor="#3333FF" Font-Names="Calibri"></asp:Label>
            <asp:Label ID="Label17" runat="server" BackColor="White" Font-Bold="True" 
                        Font-Names="Calibri" Font-Size="Small" ForeColor="#3333FF" 
                        style="position:absolute; top: 48px; left: 16px; height: 15px; width: 39px;" 
                        Text="Obra : "></asp:Label>
            <asp:Label ID="lblObra" runat="server"
                        style="position:absolute; top: 48px; left: 57px; height: 15px; width: 437px;" 
                        BackColor="White" Font-Bold="True" Font-Size= "Small" 
                        ForeColor="#3333FF" Font-Names="Calibri"></asp:Label>
            <asp:Label ID="Label10" runat="server" Text="Tipo Atividade : "
                        style="position:absolute; top: 68px; left: 15px; height: 20px; width: 88px;" 
                        BackColor="White" Font-Bold="True" Font-Size="Small" 
                        ForeColor="#3333FF" Font-Names="Calibri"></asp:Label>
            <asp:Label ID="lblAtividade" runat="server"
                        style="position:absolute; top: 68px; left: 104px; height: 15px; width: 246px;" 
                        BackColor="White" Font-Bold="True" Font-Size= "Small" 
                        ForeColor="#3333FF" Font-Names="Calibri"></asp:Label>
            <asp:Label ID="Label20" runat="server" Text="Prazo Execução "
                        style="position:absolute; top: 100px; left: 95px; height: 17px; width: 92px;" 
                        BackColor="White" Font-Bold="True" Font-Size="Small" 
                        ForeColor="#3333FF" Font-Names="Calibri"></asp:Label>
            <asp:Label ID="Label26" runat="server" Text="5 dias"
                        style="position:absolute; top: 120px; left: 116px; height: 18px; width: 74px;" 
                        BackColor="White" Font-Bold="True" Font-Size= "Small" 
                        ForeColor="#3333FF" Font-Names="Calibri"></asp:Label>
            <asp:Label ID="Label21" runat="server"
                        style="position:absolute; top: 69px; left: 450px; height: 15px; width: 80px;" 
                        BackColor="White" Font-Bold="True" Font-Size= "Small" 
                        ForeColor="#3333FF" Font-Names="Calibri"></asp:Label>
           <asp:Label ID="Label22" runat="server" Text="Periodicidade : "
                        style="position:absolute; top: 69px; left: 363px; height: 17px; width: 88px;" 
                        BackColor="White" Font-Bold="True" Font-Size="Small" 
                        ForeColor="#3333FF" Font-Names="Calibri"></asp:Label>
            <asp:Label ID="lblPeriodicidade" runat="server"
                        style="position:absolute; top: 69px; left: 450px; height: 15px; width: 82px;" 
                        BackColor="White" Font-Bold="True" Font-Size= "Small" 
                        ForeColor="#3333FF" Font-Names="Calibri"></asp:Label>
            <asp:Label ID="Label24" runat="server" Text="Data Início"
                        style="position:absolute; top: 100px; left: 15px; height: 15px; width: 60px;" 
                        BackColor="White" Font-Bold="True" Font-Size= "Small" 
                        ForeColor="#3333FF" Font-Names="Calibri"></asp:Label>
            <asp:TextBox ID="txtDtExecucao" runat="server" 
                        style="position:absolute; top: 118px; left: 15px; width: 66px;" 
                        Font-Names="Calibri" Font-Size="Small"></asp:TextBox>
                        <asp:MaskedEditExtender ID="txtDataInicial_MaskedEditExtender" runat="server" 
                            Enabled="True" 
                            TargetControlID="txtDtExecucao" CultureName="pt-BR" 
                            Mask="99/99/9999" MaskType="Date" UserDateFormat="DayMonthYear" 
                            UserTimeFormat="TwentyFourHour"></asp:MaskedEditExtender>
            <asp:Label ID="Label13" runat="server" Text="Limite para Reprogramação "
                        style="position:absolute; top: 100px; left: 191px; height: 17px; width: 157px;" 
                        BackColor="White" Font-Bold="True" Font-Size="Small" 
                        ForeColor="#3333FF" Font-Names="Calibri"></asp:Label>
            <asp:TextBox ID="txtLimiteRepro" runat="server" 
                        style="position:absolute; top: 118px; left: 191px; width: 152px;" 
                        Font-Names="Calibri" Font-Size="Small"></asp:TextBox>
                        <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server" 
                            Enabled="True" 
                            TargetControlID="txtLimiteRepro" CultureName="pt-BR" 
                            Mask="99/99/9999" MaskType="Date" UserDateFormat="DayMonthYear" 
                            UserTimeFormat="TwentyFourHour"></asp:MaskedEditExtender>
            <asp:Label ID="Label12" runat="server" Text="Reprogramar Para"
                        style="position:absolute; top: 100px; left: 360px; height: 15px; width: 120px;" 
                        BackColor="White" Font-Bold="True" Font-Size= "Small" 
                        ForeColor="#3333FF" Font-Names="Calibri"></asp:Label>
            <asp:TextBox ID="TxtDtReprogramar" runat="server" CausesValidation = "false"
                    style="position:absolute; top: 118px; left: 360px; width: 100px;" 
                    Font-Names="Calibri" Font-Size="Small"></asp:TextBox>
                    <asp:MaskedEditExtender ID="MaskedEditExtender5" runat="server" 
                        Enabled="True" 
                        TargetControlID="TxtDtReprogramar" CultureName="pt-BR" 
                        Mask="99/99/9999" MaskType="Date"  UserDateFormat="DayMonthYear"
                        MessageValidatorTip="false"></asp:MaskedEditExtender>
                    <asp:CalendarExtender ID="TxtDtReprogramar_CalendarExtender" runat="server" 
                        Enabled="True" TargetControlID="TxtDtReprogramar" Format="dd/MM/yyyy"></asp:CalendarExtender>

<%--                    <asp:RequiredFieldValidator ID="rvfReprogramar" runat="server" 
                        ControlToValidate="TxtDtReprogramar"
                        style="position:absolute; top: 156px; left: 223px; height: 22px; width: 157px; "
                        ErrorMessage="Data de reprogramação da preventiva em branco!" 
                        Display="None"></asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" 
                        style="position:absolute; top: 176px; left: 223px; width: 147px;"
                        ErrorMessage="Data reprogramação menor que data início da execução!" 
                        ControlToCompare="txtDtExecucao" ControlToValidate="TxtDtReprogramar"  
                        Display="None" Operator= "GreaterThanEqual"
                        Type="Date">
                    </asp:CompareValidator>
                    <asp:CompareValidator ID="CompareValidator2" runat="server" 
                        style="position:absolute; top: 176px; left: 223px; width: 147px;"
                        ErrorMessage="Data reprogramação maior que data limite para reprogramação!" 
                        ControlToCompare="txtLimiteRepro" ControlToValidate="TxtDtReprogramar" 
                        Display="None" Operator= "LessThanEqual"
                        Type="Date">
                    </asp:CompareValidator>
--%>
            <asp:Button ID="btnSaveRepro" CommandName="UpdateRepro" runat="server" Text="Gravar" 
                        style="position:absolute; top: 162px; left: 17px; width: 70px; height: 30px;" 
                        CausesValidation="false"
                        onclick="btnSaveRepro_Click"/>
            <asp:Button ID="btnCancelRepro" runat="server" Text="Cancelar" CausesValidation="false"
                        style="position:absolute; top: 162px; left: 95px; width: 70px; height: 30px;" 
                        onclick="btnCancelRepro_Click" />
            </asp:Panel>


<%--            <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
                        style="position:absolute; top: 50px; left: 774px; width: 200px; height: 26px;"
                        ShowMessageBox="True" ShowSummary="False" />
--%>
    </div>

</asp:Content>

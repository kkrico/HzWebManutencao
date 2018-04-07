<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="True" CodeBehind="webATE_OS.aspx.cs" Inherits="HzWebManutencao.ATE.webATE_OS" %>
<%@ Register src="~/Controles/Mensagem.ascx" tagname="Mensagem" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Mensagem ID="CaixaMensagem" runat="server" />
    <div style="position:relative; height: 469px; width: 1154px; top: 12px; left: -54px; margin-left: 40px;">
            <asp:Label ID="lblNumeroOS" runat="server" Text="lblNumeroOS"
                style="position:absolute; top: 3px; left: 9px; height: 21px; width: 362px;" 
                BackColor="White" Font-Bold="True" Font-Size="Medium" ForeColor="Maroon"></asp:Label>
            <asp:Label ID="lblStatus" runat="server" Text="lblStatus"
                style="position:absolute; top: 4px; left: 462px; height: 21px; width: 290px;" 
                BackColor="White" Font-Bold="True" Font-Size="Medium" ForeColor="Maroon"></asp:Label>
            <asp:Label ID="lblNuPreventiva" runat="server" Text="lblNuPreventiva"  Visible ="false"
                style="position:absolute; top: 23px; left: 9px; height: 21px; width: 302px;" 
                BackColor="White" Font-Bold="True" Font-Size="Medium" ForeColor="Maroon"></asp:Label>

            <asp:Label ID="Label1" runat="server" Text="Obra:" 
                style="position:absolute; top: 53px; left: 11px; height: 18px; width: 44px;"></asp:Label>
            <asp:DropDownList ID="cmbObra" runat="server" AutoPostBack="True" 
                style="position:absolute; top: 52px; left: 50px; width: 631px; bottom: 395px; right: 465px;" 
                onselectedindexchanged="cmbObra_SelectedIndexChanged" ></asp:DropDownList>
            <asp:Label ID="Label14" runat="server" Text="*"
                style="position:absolute; top: 53px; left: 690px; height: 18px; width: 14px;" 
                ForeColor="Red"></asp:Label>

            <asp:Label ID="Label2" runat="server" Text="Tipo O.S:" 
                style="position:absolute; top: 115px; left: 711px; height: 19px;"></asp:Label>
            <asp:DropDownList ID="cmbOrigemOS" runat="server" 
                style="position:absolute; top: 114px; left: 840px; width: 269px; right: 47px;" 
                TabIndex="8"></asp:DropDownList>
            <asp:Label ID="Label3" runat="server" Text="*"
                style="position:absolute; top: 115px; left: 1119px; height: 19px; width: 5px; right: 30px;" 
                ForeColor="Red"></asp:Label>

            <asp:Label ID="Label13" runat="server" Text="Local:" 
                style="position:absolute; top: 82px; left: 10px;"></asp:Label>
            <asp:TextBox ID="txtLocal" runat="server" 
                style="position:absolute; top: 83px; left: 143px; width: 535px;" 
                TabIndex="1"></asp:TextBox>
            <asp:Label ID="Label6" runat="server" Text="*"
                style="position:absolute; top: 87px; left: 690px; height: 18px; width: 14px;" 
                ForeColor="Red"></asp:Label>

            <asp:Label ID="LblSetor" runat="server" Text="Setor:" 
                style="position:absolute; top: 115px; left: 10px;"></asp:Label>
            <asp:TextBox ID="txtSetor" runat="server" 
                style="position:absolute; top: 114px; left: 143px; width: 535px;" 
                TabIndex="2"></asp:TextBox>

            <asp:Label ID="lblFormaSolicitacao" runat="server" Text="Forma Solicitação:" 
                style="position:absolute; top: 149px; left: 10px;"></asp:Label>
            <asp:DropDownList ID="cmbFormaSolicitacao" runat="server" 
                style="position:absolute; top: 147px; left: 143px; width: 538px; bottom: 300px; right: 468px;" 
                TabIndex="3"></asp:DropDownList>
            <asp:Label ID="Label16" runat="server" Text="*"
                style="position:absolute; top: 150px; left: 690px; height: 18px; width: 14px;" 
                ForeColor="Red"></asp:Label>

            <asp:Label ID="Label4" runat="server" Text="Pavimento:" 
                style="position:absolute; top: 85px; left: 711px;"></asp:Label>
            <asp:DropDownList ID="cmbObraPavimento" runat="server" 
                style="position:absolute; top: 83px; left: 840px; width: 269px; bottom: 364px; right: 45px;" 
                TabIndex="7" AutoPostBack="True" 
                onselectedindexchanged="cmbObraPavimento_SelectedIndexChanged"></asp:DropDownList>
            <asp:Label ID="Label9" runat="server" Text="*"
                style="position:absolute; top: 85px; left: 1119px; height: 19px;" 
                ForeColor="Red"></asp:Label>

            <asp:Label ID="Label5" runat="server" Text="Solicitante:" 
                style="position:absolute; top: 187px; left: 10px;"></asp:Label>
            <asp:TextBox ID="txtSolicitante" runat="server" 
                style="position:absolute; top: 182px; left: 143px; width: 535px;" 
                TabIndex="4"></asp:TextBox>
            <asp:Label ID="Label17" runat="server" Text="*"
                style="position:absolute; top: 186px; left: 690px; height: 18px; width: 14px;" 
                ForeColor="Red"></asp:Label>

            <asp:TextBox ID="txtNuDemanda" runat="server" 
                style="position:absolute; top: 53px; left: 840px; width: 265px; " 
                TabIndex="6"></asp:TextBox>

            <asp:Label ID="Label12" runat="server" Text="Telefone/Ramal:" 
                style="position:absolute; top: 148px; left: 711px; right: 339px;"></asp:Label>
            <asp:TextBox ID="txtTelefone" runat="server" 
                style="position:absolute; top: 146px; left: 840px; width: 265px;" 
                TabIndex="9"></asp:TextBox>

            <asp:Label ID="lblDtAbertura" runat="server" Text="Data Abertura:" 
                style="position:absolute; top: 4px; left: 777px; width: 114px;"
                BackColor="White" Font-Bold="True" Font-Size="Medium" ForeColor="Maroon"></asp:Label>
            <asp:TextBox ID="txtDtAbertura" runat="server" 
                style="position:absolute; top: 4px; left: 893px; width: 213px;"
                BackColor="White" Font-Bold="True" Font-Size="Medium" ForeColor="Maroon"></asp:TextBox>
            <asp:Label ID="Label10" runat="server" Text="E-mail Para Contato:" 
                style="position:absolute; top: 215px; left: 11px;"></asp:Label>
            <asp:TextBox ID="txtEmail" runat="server" 
                style="position:absolute; top: 212px; left: 143px; width: 535px;" 
                TabIndex="5"></asp:TextBox>

            <asp:Label ID="Label22" runat="server" Text="Equipamento:" 
                style="position:absolute; top: 250px; left: 11px;"></asp:Label>
            <asp:TextBox ID="txtEquipamentoObra" runat="server" 
                style="position:absolute; top: 250px; left: 143px; width: 500px;" 
                TabIndex="5" Enabled="False"></asp:TextBox>

        <asp:Button ID="btnEquipamento" runat="server" Text="..." 
                style="position:absolute; top: 250px; left: 658px; width: 24px;" 
                onclick="btnEquipamento_Click" />

                        <asp:Button ID="Button1" runat="server" Text="..." 
                style="position:absolute; top: 250px; left: 658px; width: 24px;" 
                onclick="btnEquipamento_Click" visible=false Enabled="False"/>

            <asp:Label ID="Label7" runat="server" Text="Tipo Atividade:" 
                style="position:absolute; top: 185px; left: 716px; height: 15px;"></asp:Label>
            <asp:DropDownList ID="cmbTipoAtividade" runat="server" 
                style="position:absolute; top: 182px; left: 840px; width: 269px; " 
                AutoPostBack="True" 
                onselectedindexchanged="cmbTipoAtiviade_SelectedIndexChanged" 
                TabIndex="10"></asp:DropDownList>
            <asp:Label ID="Label18" runat="server" Text="*"
                style="position:absolute; top: 183px; left: 1119px; height: 18px; width: 9px;" 
                ForeColor="Red"></asp:Label>

            <asp:Label ID="Label8" runat="server" Text="Solicitação:" 
                style="position:absolute; top: 212px; left: 716px;"></asp:Label>
            <asp:DropDownList ID="cmbSolicitacao" runat="server" 
                style="position:absolute; top: 212px; left: 840px; width: 269px;" 
                TabIndex="11"></asp:DropDownList>

                            <asp:Label ID="Label30" runat="server" Text="Técnico:" 
                style="position:absolute; top: 248px; left: 716px;"></asp:Label>
            <asp:DropDownList ID="cmbTecnico" runat="server" 
                style="position:absolute; top: 242px; left: 840px; width: 269px;" 
                TabIndex="11" AutoPostBack="True" 
                onselectedindexchanged="cmbTecnico_SelectedIndexChanged" Enabled="False"></asp:DropDownList>

            <asp:Label ID="Label19" runat="server" Text="*"
                style="position:absolute; top: 213px; left: 1119px; height: 18px; width: 14px;" 
                ForeColor="Red"></asp:Label>

            <asp:Label ID="Label11" runat="server" Text="Detalhamento da Solicitação:" 
                style="position:absolute; top: 293px; left: 11px; width: 179px;"></asp:Label>
            <asp:TextBox ID="txtObservacoes" runat="server" 
                style="position:absolute; top: 315px; left: 11px; width: 1096px; height: 55px;" 
                TextMode="MultiLine" TabIndex="12"></asp:TextBox>

            <asp:Label ID="Label20" runat="server" Text="* Campos Obrigatórios" 
                style="position:absolute; top: 422px; left: 13px;" ForeColor="Red"></asp:Label>

            <asp:CompareValidator ID="cvObra" runat="server" 
                ControlToValidate="cmbObra" Display="None" ErrorMessage="É obrigatório o preenchimento do campo Obra!" 
                style="position:absolute; top: 420px; left: 159px; width: 80px; height: 16px;" 
                Operator="NotEqual" ValueToCompare="0" ></asp:CompareValidator>
            <asp:CompareValidator ID="cvTipo" runat="server" 
                ControlToValidate="cmbOrigemOS" Display="None" ErrorMessage="É obrigatório o preenchimento do campo Tipo!" 
                style="position:absolute; top: 441px; left: 160px; width: 75px; height: 16px;" 
                Operator="NotEqual" ValueToCompare="0" ></asp:CompareValidator>
            <asp:RequiredFieldValidator ID="rfdLocal" runat="server" 
                ControlToValidate="txtLocal" Display="None" ErrorMessage="É obrigatório o preenchimento do campo Local!"
                style="position:absolute; top: 423px; left: 376px; width: 94px; height: 16px;" ></asp:RequiredFieldValidator>
            <asp:CompareValidator ID="cvPavimento" runat="server" 
                ControlToValidate="cmbObraPavimento" Display="None" ErrorMessage="É obrigatório o preenchimento do campo Pavimento!" 
                style="position:absolute; top: 439px; left: 376px; width: 80px; height: 16px;" 
                Operator="NotEqual" ValueToCompare="0" ></asp:CompareValidator>
 
            <asp:CompareValidator ID="cvFormaSolicitacao" runat="server" 
                ControlToValidate="cmbFormaSolicitacao" Display="None" ErrorMessage="É obrigatório o preenchimento do campo Forma Solicitação!" 
                style="position:absolute; top: 425px; left: 241px; width: 123px; height: 16px;" 
                Operator="NotEqual" ValueToCompare="0" ></asp:CompareValidator>
             <asp:RequiredFieldValidator ID="rfdSolicitante" runat="server" 
                ControlToValidate="txtSolicitante" Display="None" ErrorMessage="É obrigatório o preenchimento do campo Solicitante!"
                style="position:absolute; top: 441px; left: 479px; width: 94px; height: 16px;"></asp:RequiredFieldValidator>
            <asp:CompareValidator ID="cvSolicitacao" runat="server" 
                ControlToValidate="cmbSolicitacao" Display="None" ErrorMessage="É obrigatório o preenchimento do campo Solicitação!" 
                style="position:absolute; top: 422px; left: 477px; width: 87px; height: 16px;" 
                Operator="NotEqual" ValueToCompare="0" ></asp:CompareValidator>
            <asp:CompareValidator ID="cvAtividade" runat="server" 
                ControlToValidate="cmbTipoAtividade" Display="None" ErrorMessage="É obrigatório o preenchimento do campo Tipo de atividade!" 
                style="position:absolute; top: 421px; left: 566px; width: 94px; height: 16px;" 
                Operator="NotEqual" ValueToCompare="0" ></asp:CompareValidator>

            <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
                style="position:absolute; top: 424px; left: 665px; width: 200px; height: 33px;" 
                ShowMessageBox="True" ShowSummary="False" />

            <asp:Button ID="btnSave" runat="server" Text="Gravar" Font-Size="X-Small" Font-Bold="True"
                style="position:absolute; top: 390px; left: 8px; width: 62px; height: 22px;" 
                onclick="btnSave_Click" TabIndex="13"   />
            <asp:Button ID="btnReset" runat="server" Text="Novo"  Font-Size="X-Small" Font-Bold="True"
                style="position:absolute; top: 390px; left: 75px; width: 58px; height: 22px;" 
                onclick="btnReset_Click" CausesValidation="False" TabIndex="200"/>
           <asp:Button ID="btnPesquisar" runat="server" Text="Pesquisar" 
                Font-Size="X-Small" Font-Bold="True"
                style="position:absolute; top: 390px; left: 136px; width: 83px; height: 22px;" 
                onclick="btnPesquiar_Click" CausesValidation="False" TabIndex="300"/>
           <asp:Button ID="btnConcluir" runat="server" Text="Concluir" Font-Size="X-Small" Font-Bold="True"
                style="position:absolute; top: 390px; left: 222px; width: 77px; height: 22px;" 
                onclick="btnConcluir_Click" TabIndex="400"/>
           <asp:Button ID="btnEnviarAprovacao" runat="server" Text="Enviar p/ Aprovação" 
                Font-Size="X-Small" Font-Bold="True"
                style="position:absolute; top: 390px; left: 303px; width: 130px; height: 22px;" 
                onclick="btnEnviarAprovacao_Click" TabIndex="500"/>
           <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" Font-Size="X-Small" Font-Bold="True"
                style="position:absolute; top: 390px; left: 437px; width: 74px; height: 22px;" 
                onclick="btnCancelar_Click" TabIndex="600"/>
           <asp:Button ID="btnExcluir" runat="server" Text="Excluir" Font-Size="X-Small" Font-Bold="True"
                style="position:absolute; top: 390px; left: 515px; width: 64px; height: 22px;" 
                onclick="btnExcluir_Click" TabIndex="700"/>
           <asp:Button ID="btnExecucao" runat="server" Text="Execução" Font-Size="X-Small" Font-Bold="True"
                style="position:absolute; top: 390px; left: 665px; width: 67px; height: 22px;" 
                onclick="btnExecucao_Click" TabIndex="800"/>
           <asp:Button ID="btnReabrirOS" runat="server" Text="Reabrir OS" 
                Font-Size="X-Small" Font-Bold="True"
                style="position:absolute; top: 390px; left: 583px; width: 78px; height: 22px;" 
                onclick="btnReabrirOS_Click" TabIndex="850"/>
           <asp:Button ID="btnImprimirOS" runat="server" Text="Imprimir O.S" 
                Font-Size="X-Small" Font-Bold="True"
                style="position:absolute; top: 390px; left: 738px; width: 84px; height: 22px;" 
                onclick="btnImprimirOS_Click" TabIndex="900"/>
           <asp:Button ID="btnMaterial" runat="server" Text="Material" Font-Size="X-Small" Font-Bold="True"
                style="position:absolute; top: 390px; left: 828px; width: 140px; height: 22px;" 
                onclick="btnMaterial_Click" TabIndex="950"/>

        <asp:Label ID="lbOSAndamento" runat="server" Text="Andamentos da Ordem de Serviço"
            style="position:absolute; top: 282px; left: 375px; font-weight: 700;"></asp:Label>
        <asp:ImageButton ID="imgAndamento" runat="server" CausesValidation="False"
            style="position:absolute; top: 277px; left: 335px; width: 34px; height: 28px;" 
            ImageUrl="~/Imagens/note_accept.jpg"/>
            <asp:ModalPopupExtender 
                ID="ModalPopupExtender3" 
                runat="server" 
                TargetControlID="imgAndamento" 
                PopupControlID="pnlAndamento"
                DropShadow="true"   
                CancelControlID="btnFecharAndamento"
                BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>

       <asp:Panel ID="pnlAndamento" runat="server" BackColor="#FFFFCC" Height="543px" Width="900px"
                        BorderStyle="Solid" BorderWidth="1px" style="display:none" >
            <asp:Label ID="Label29" runat="server" Text="Andamentos da Ordem de Serviço"
                        style="position:absolute; top: 12px; left: 4px; height: 20px; width: 889px; font-size:medium; text-align: center;" 
                        BackColor="#FFFFCC" Font-Bold="True" Font-Size="Large" 
                        ForeColor="#3333FF" Font-Names="Calibri"></asp:Label>
            <asp:GridView ID="grdOSAndamento" runat="server" 
                style="position:absolute; top: 35px; left: 15px; width: 880px;" AllowPaging="True" 
                AllowSorting="True" 
                AutoGenerateColumns="False" 
                CellPadding="3" 
                BackColor="White" BorderColor="#003366" 
                BorderStyle="Inset" BorderWidth="1px" 
                ViewStateMode="Enabled" 
                PageSize="15">
                <Columns>
                    <asp:BoundField DataField="cmpDtAndamento" HeaderText="Data">
                        <ItemStyle Width="95px" HorizontalAlign="Left" Font-Size="X-Small" VerticalAlign="Middle"/>
                    </asp:BoundField>
                    <asp:BoundField DataField="cmpDcOSAndamento" HeaderText="Situação">
                        <ItemStyle Width="100px" HorizontalAlign="Left" Font-Size="X-Small" VerticalAlign="Middle" Wrap="True" />
                    </asp:BoundField>
                    <asp:BoundField DataField="cmpDcObsAndamento" HeaderText="Observação">
                      <ItemStyle Width="400px"  HorizontalAlign="Left" Font-Size="X-Small" VerticalAlign="Middle" Wrap="True"/>
                    </asp:BoundField>
                    <asp:BoundField DataField="cmpNoUsuario" HeaderText="Responsável pelo Cadastro">
                      <ItemStyle Width="180px"  HorizontalAlign="Left" Font-Size="X-Small" VerticalAlign="Middle" Wrap="True"/>
                    </asp:BoundField>
                </Columns>
                <FooterStyle BackColor="White" ForeColor="#000066"/>
                <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="White" ForeColor="#000066"/>
                <RowStyle ForeColor="#000066" Font-Names="Calibri" Font-Size="Small" />
                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                <SortedAscendingHeaderStyle BackColor="#007DBB" />
                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                <SortedDescendingHeaderStyle BackColor="#00547E" />
            </asp:GridView>

            <asp:Button ID="btnFecharAndamento" runat="server" CausesValidation="False" Text="Fechar" 
                style="position:absolute; top: 500px; left: 14px; height: 30px; width: 80px;"/>
        </asp:Panel>

        <asp:Label ID="lblJustifica" runat="server" Text="Justificativa"
            style="position:absolute; top: 291px; left: 675px; font-weight: 700;"></asp:Label>
        <asp:ImageButton ID="ImgJustifica" runat="server" CausesValidation="False"
            style="position:absolute; top: 277px; left: 639px; width: 34px; height: 28px;" 
            ImageUrl="~/Imagens/7155_32x32.png"/>
            <asp:ModalPopupExtender 
                ID="ModalPopupExtender2" 
                runat="server" 
                TargetControlID="ImgJustifica" 
                PopupControlID="pnlJustificativa"
                DropShadow="true"   
                CancelControlID="CancelBtn"
                BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>

       <asp:Panel ID="pnlJustificativa" runat="server" BackColor="#FFFFCC"  Height="243px" Width="592px"
                        BorderStyle="Solid" BorderWidth="1px" style="display:none" >
            <asp:Label ID="lblJustificativa" runat="server" Text="Justificativa"
                        style="position:absolute; top: 6px; left: 9px; height: 20px; width: 580px; font-size:medium; text-align: center;" 
                        BackColor="#FFFFCC" Font-Bold="True" Font-Size="Large" 
                        ForeColor="#3333FF" Font-Names="Calibri"></asp:Label>
            <asp:TextBox ID="TxtJustificativa" runat="server" ForeColor="#3333FF" 
                style="position:absolute; top: 35px; left: 13px; width: 569px; height: 160px;" 
                TextMode="MultiLine"></asp:TextBox>
            <asp:Button ID="btnFecharJutifica" runat="server" CausesValidation="False" Text="Fechar" 
                style="position:absolute; top: 212px; left: 14px; height: 30px; width: 80px;"/>
        </asp:Panel>

        <asp:Label ID="lblConclusao" runat="server" Text="Visualizar Conclusão Ordem de Serviço"
            style="position:absolute; top: 290px; left: 674px; font-weight: 700;"></asp:Label>
        <asp:ImageButton ID="ImgConclusao" runat="server" CausesValidation="False"
            style="position:absolute; top: 278px; left: 639px; width: 34px; height: 28px;" 
            ImageUrl="~/Imagens/7155_32x32.png"/>
            <asp:ModalPopupExtender 
                ID="ModalPopupExtender1" 
                runat="server" 
                TargetControlID="ImgConclusao" 
                PopupControlID="pnlpopup"
                DropShadow="true"   
                CancelControlID="btnFecharJutifica"
                BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>

        <asp:Panel ID="pnlpopup" runat="server" BackColor="#FFFFCC"  Height="304px" Width="825px"
                    BorderStyle="Solid" BorderWidth="1px" style="display:none">
            <asp:Label ID="Label21" runat="server" Text="Conclusão da Ordem de Serviço"
                        style="position:absolute; top: 6px; left: 9px; height: 20px; width: 807px; font-size:medium; text-align: center;" 
                        BackColor="#FFFFCC" Font-Bold="True" Font-Size="Large" 
                        ForeColor="#3333FF" Font-Names="Calibri"></asp:Label>
            <asp:Label ID="Label23" runat="server" Text="Cliente Satisfeito Com Serviço"
                style="position:absolute; top: 39px; left: 286px; height: 17px; width: 180px;" 
                ForeColor="#3333FF"></asp:Label>
            <asp:Label ID="lblSatisfExecucao" runat="server" Text="Cliente Satisfeito Com o Prazo de Execução do Serviço"
                style="position:absolute; top: 39px; left: 471px; height: 17px; width: 326px;" 
                ForeColor="#3333FF"></asp:Label>
            <asp:Label ID="lblSatisfacaoExecucao" runat="server" ForeColor="#3333FF" 
                style="position:absolute; top: 55px; left: 473px; height: 17px; width: 148px;"></asp:Label>
            <asp:TextBox ID="txtDtInicio" runat="server" 
                style="position:absolute; top: 56px; left: 21px; width: 109px; right: 858px;" 
                BackColor="#FFFFCC"></asp:TextBox>
            <asp:Label ID="Label24" runat="server" Text="Término"
                style="position:absolute; top: 38px; left: 155px; width: 56px;" 
                ForeColor="#3333FF"></asp:Label>
            <asp:TextBox ID="txtConclusao" runat="server" 
                BackColor="#FFFFCC"
                style="position:absolute; top: 56px; left: 153px; width: 109px;"></asp:TextBox>
            <asp:Label ID="Label25" runat="server" Text="Gestor Obra"
                style="position:absolute; top: 83px; left: 20px; height: 17px; width: 89px;" 
                ForeColor="#3333FF"></asp:Label>
            <asp:TextBox ID="txtAtestado" runat="server" BackColor="#FFFFCC"
                style="position:absolute; top: 102px; left: 19px; width: 350px;"></asp:TextBox>
            <asp:Label ID="Label26" runat="server" Text="Executante(s)"
                style="position:absolute; top: 82px; left: 384px; width: 80px;" 
                ForeColor="#3333FF"></asp:Label>
            <asp:TextBox ID="txtConcluido" runat="server" BackColor="#FFFFCC"
                style="position:absolute; top: 101px; left: 384px; width: 427px;"></asp:TextBox>
            <asp:Label ID="Label27" runat="server" Text="Detalhamento da Execução Serviço" 
                style="position:absolute; top: 129px; left: 20px; height: 16px; width: 218px;" 
                ForeColor="#3333FF"></asp:Label>
            <asp:TextBox ID="txtObs" runat="server" BackColor="#FFFFCC"
                style="position:absolute; top: 148px; left: 19px; width: 795px; height: 110px; right: 174px;" 
                TextMode="MultiLine"></asp:TextBox>
            <asp:Label ID="Label28" runat="server" ForeColor="#3333FF" 
                style="position:absolute; top: 39px; left: 22px; height: 16px; width: 53px;" 
                Text="Início"></asp:Label>
            <asp:Label ID="lblSatisfacaoServico" runat="server" ForeColor="#3333FF" 
                style="position:absolute; top: 54px; left: 288px; height: 17px; width: 148px;"></asp:Label>
            <asp:Button ID="CancelBtn" runat="server" CausesValidation="False" Text="Fechar" 
                style="position:absolute; top: 265px; left: 21px; height: 30px; width: 80px;"/>

        </asp:Panel>
           
    <asp:Label ID="Label15" runat="server" Text="Nº Demanda Cliente:" 
        style="position:absolute; top: 55px; left: 711px;"></asp:Label>

    <asp:Panel ID="Panel2" runat="server" 
            style="position:absolute; top: 400px; left: 908px;">
        <asp:Label ID="lblmsg" runat="server"></asp:Label>
    </asp:Panel>

    <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False"
        style="position:absolute; top: 111px; left: 805px; width: 25px; height: 28px;" 
        ImageUrl="~/Imagens/8115_32x32.png"/>
    <asp:BalloonPopupExtender ID="PopupControlExtender2" runat="server"
            TargetControlID="ImageButton1"
            BalloonPopupControlID="Panel2"
            Position="BottomLeft" 
            BalloonStyle="Rectangle"
            BalloonSize="Large"
            CustomCssUrl="CustomStyle/BalloonPopupOvalStyle.css"
            CustomClassName="oval"
            UseShadow="true" 
            ScrollBars="Auto"
            DisplayOnMouseOver="true"
            DisplayOnFocus="false"
            DisplayOnClick="true"/>


           <asp:ModalPopupExtender 
                ID="ModalPopupExtender5" 
                runat="server" 
                TargetControlID="btnEquipamento" 
                PopupControlID="Panel1"
                BackgroundCssClass="modalBackground" CancelControlID="btnCancela" ></asp:ModalPopupExtender>

                <asp:Panel ID="Panel1" runat="server" BackColor="White" 
                    Height="382px" Width="650px"  BorderStyle="Solid" 
                    BorderWidth="1px"
                    style="display:yes">

                    <asp:Label ID="lblAcao" runat="server" Text="Cadastro de Equipamento da Obra" 
                style="position:absolute; top: 25px; left: 138px; height: 20px; width: 383px; text-align: center;" 
                BackColor="White" Font-Bold="True" Font-Size="Medium" 
            ForeColor="Maroon"></asp:Label>

                    <asp:Button ID="btnCancela" runat="server" Text="Cancelar" 
                        
                        style="position:absolute; top: 334px; left: 264px; height: 30px; width: 68px; text-align: center; right: 822px;" />
            <asp:Label ID="Label31" runat="server" Text="Pavimento"
 
            style="position:absolute; top: 45px; left: 26px; height: 14px; width: 76px;"></asp:Label>

<asp:GridView ID="grdPesquisa" runat="server" 
            
            style="position:absolute; top: 50px; left: 20px; width: 625px; height: 100px;" AllowPaging="True" 
            AllowSorting="True" AutoGenerateColumns="False" 
            onpageindexchanging="grdPesquisa_PageIndexChanging" 
            ViewStateMode="Enabled" 
            BorderColor="#003366" BorderStyle="Inset" BorderWidth="1px"
            CellPadding="3" BackColor="White" Font-Size="Small" 
            >
            <Columns>
                             <asp:TemplateField HeaderText="Operação">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkEditar" runat="server" Text="Selecionar" 
                                CommandArgument='<%# Eval("cmpIdEquipamentoObra") %>' CommandName="editar"                             
                                OnClick="lnkEditar_Click" CausesValidation="False"></asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle Width="80px" HorizontalAlign="Left" VerticalAlign="Middle" />
                </asp:TemplateField>
                <asp:BoundField DataField="CmpDcEquipamentoObra" HeaderText="Descrição Equipamento">
                    <ItemStyle Width="200px" HorizontalAlign="Left" Font-Size="Small" />
                </asp:BoundField>
                 <asp:BoundField DataField="CodEquipamento" HeaderText="Código">
                    <ItemStyle Width="50px" HorizontalAlign="Left" Font-Size="Small" />
                </asp:BoundField>
                 <asp:BoundField DataField="cmpDcTipoAtividade" HeaderText="Tipo">
                    <ItemStyle Width="100px" HorizontalAlign="Left" Font-Size="Small" />
                </asp:BoundField>
                 <asp:BoundField DataField="cmpIdEquipamentoObra" HeaderText="cmpIdEquipamentoObra" Visible="False">
                    <ItemStyle Width="130px" HorizontalAlign="Left" Font-Size="Small" />
                </asp:BoundField>
            </Columns>
            <FooterStyle BackColor="White" ForeColor="#000066" />
            <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" Font-Names="Calibri" Font-Size="Medium" />
            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
            <RowStyle ForeColor="#000066" Font-Names="Calibri" Font-Size="Small" />
            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F1F1F1" />
            <SortedAscendingHeaderStyle BackColor="#007DBB" />
            <SortedDescendingCellStyle BackColor="#CAC9C9" />
            <SortedDescendingHeaderStyle BackColor="#00547E" />
        </asp:GridView>
        </asp:Panel>

    </div>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="True" CodeBehind="webATE_ServicosTI.aspx.cs" Inherits="HzWebManutencao.ATE.webATE_ServicosTI" %>
<%@ Register src="~/Controles/Mensagem.ascx" tagname="Mensagem" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Mensagem ID="CaixaMensagem" runat="server" />
    <div style="position:relative; height: 469px; width: 988px; top: 3px; left: -54px; margin-left: 40px;">
            <asp:Label ID="lblNumeroOS" runat="server" Text="lblNumeroOS"
                style="position:absolute; top: 3px; left: 9px; height: 21px; width: 362px;" 
                BackColor="White" Font-Bold="True" Font-Size="Medium" ForeColor="Maroon"></asp:Label>
            <asp:Label ID="lblStatus" runat="server" Text="lblStatus"
                style="position:absolute; top: 4px; left: 412px; height: 21px; width: 206px;" 
                BackColor="White" Font-Bold="True" Font-Size="Medium" ForeColor="Maroon"></asp:Label>

            <asp:DropDownList ID="cmbLocal" runat="server" AutoPostBack="True" 
                style="position:absolute; top: 34px; left: 128px; width: 836px;" 
                onselectedindexchanged="cmbLocal_SelectedIndexChanged" ></asp:DropDownList>
            <asp:Label ID="Label14" runat="server" Text="*" 
                style="position:absolute; top: 35px; left: 967px; height: 12px; width: 8px;" 
                ForeColor="Red"></asp:Label>

            <asp:Label ID="Label2" runat="server" Text="Tipo O.S:" 
                
                
                
                
                style="position:absolute; top: 66px; left: 741px; height: 19px; width: 59px;"></asp:Label>
            <asp:Label ID="Label3" runat="server" Text="*"
                style="position:absolute; top: 67px; left: 731px; height: 12px; width: 5px; right: 252px;" 
                ForeColor="Red"></asp:Label>

            <asp:Label ID="Label13" runat="server" Text="Obra:" 
                style="position:absolute; top: 37px; left: 10px; width: 50px;"></asp:Label>
            <asp:Label ID="Label16" runat="server" Text="*"
                style="position:absolute; top: 99px; left: 479px; height: 12px; width: 8px;" 
                ForeColor="Red"></asp:Label>

            <asp:Label ID="Label6" runat="server" Text="*"
                style="position:absolute; top: 71px; left: 480px; height: 12px; width: 8px;" 
                ForeColor="Red"></asp:Label>

            <asp:Label ID="lblFormaSolicitacao" runat="server" Text="Forma Solicitação:" 
                style="position:absolute; top: 68px; left: 10px;"></asp:Label>
            <asp:DropDownList ID="cmbFormaSolicitacao" runat="server" 
                
                style="position:absolute; top: 66px; left: 128px; width: 347px; bottom: 381px; right: 513px;" 
                TabIndex="3"></asp:DropDownList>

            <asp:Label ID="Label9" runat="server" Text="*"
                style="position:absolute; top: 66px; left: 967px; height: 12px; width: 9px;" 
                ForeColor="Red"></asp:Label>

            <asp:Label ID="Label5" runat="server" Text="Solicitante:" 
                style="position:absolute; top: 98px; left: 10px;"></asp:Label>
            <asp:TextBox ID="txtSolicitante" runat="server" 
                style="position:absolute; top: 95px; left: 128px; width: 343px;" 
                TabIndex="4"></asp:TextBox>
            <asp:Label ID="Label17" runat="server" Text="*"
                style="position:absolute; top: 156px; left: 480px; height: 12px; width: 11px;" 
                ForeColor="Red"></asp:Label>

            <asp:Label ID="Label12" runat="server" Text="Tel./Ramal:" 
                style="position:absolute; top: 67px; left: 504px; right: 415px;"></asp:Label>
            <asp:TextBox ID="txtTelefone" runat="server" 
                style="position:absolute; top: 65px; left: 576px; width: 153px;" 
                TabIndex="12"></asp:TextBox>

            <asp:Label ID="lblDtAbertura" runat="server" Text="Data Abertura:" 
                style="position:absolute; top: 4px; left: 627px; width: 135px;"
                BackColor="White" Font-Bold="True" Font-Size="Medium" ForeColor="Maroon"></asp:Label>
            <asp:TextBox ID="txtDtAbertura" runat="server" 
                style="position:absolute; top: 4px; left: 756px; width: 194px;"
                BackColor="White" Font-Bold="True" Font-Size="Medium" ForeColor="Maroon"></asp:TextBox>
            <asp:Label ID="Label10" runat="server" Text="E-mail:" 
                style="position:absolute; top: 126px; left: 10px;"></asp:Label>
            <asp:TextBox ID="txtEmail" runat="server" 
                style="position:absolute; top: 123px; left: 128px; width: 342px;" 
                TabIndex="5"></asp:TextBox>

            <asp:Label ID="Label7" runat="server" Text="Tipo Atividade:" 
                style="position:absolute; top: 158px; left: 10px;"></asp:Label>
            <asp:DropDownList ID="cmbTipoAtividade" runat="server" 
                style="position:absolute; top: 154px; left: 128px; width: 347px;" 
                AutoPostBack="True" 
                onselectedindexchanged="cmbTipoAtiviade_SelectedIndexChanged" TabIndex="6"></asp:DropDownList>

            <asp:Label ID="Label8" runat="server" Text="Solicitação:" 
                style="position:absolute; top: 189px; left: 10px;"></asp:Label>
            <asp:DropDownList ID="cmbSolicitacao" runat="server" 
                style="position:absolute; top: 186px; left: 128px; width: 347px;" 
                TabIndex="7"></asp:DropDownList>

            <asp:Label ID="Label11" runat="server" Text="Detalhamento da Solicitação:" 
                style="position:absolute; top: 216px; left: 10px; width: 179px;"></asp:Label>
            <asp:TextBox ID="txtObservacoes" runat="server" 
                style="position:absolute; top: 233px; left: 9px; width: 480px; height: 136px;" 
                TextMode="MultiLine" TabIndex="8"></asp:TextBox>

            <asp:Label ID="Label20" runat="server" Text="* Campos Obrigatórios" 
                style="position:absolute; top: 430px; left: 19px;" ForeColor="Red"></asp:Label>

            <asp:CompareValidator ID="cvObra" runat="server" 
                ControlToValidate="cmbLocal" Display="None" ErrorMessage="É obrigatório o preenchimento do campo Obra!" 
                style="position:absolute; top: 420px; left: 159px; width: 80px; height: 16px;" 
                Operator="NotEqual" ValueToCompare="0" ></asp:CompareValidator>
            <asp:CompareValidator ID="cvTipo" runat="server" 
                ControlToValidate="cmbOrigemOS" Display="None" ErrorMessage="É obrigatório o preenchimento do campo Tipo!" 
                style="position:absolute; top: 441px; left: 160px; width: 75px; height: 16px;" 
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
                style="position:absolute; top: 420px; left: 665px; width: 200px; height: 33px;" 
                ShowMessageBox="True" ShowSummary="False" />

            <asp:Button ID="btnSave" runat="server" Text="Gravar" 
                style="position:absolute; top: 390px; left: 8px; width: 60px; height: 26px;" 
                onclick="btnSave_Click" TabIndex="13" />
            <asp:Button ID="btnReset" runat="server" Text="Novo" 
                style="position:absolute; top: 390px; left: 71px; width: 60px; height: 26px;" 
                onclick="btnReset_Click" CausesValidation="False" TabIndex="200"/>
           <asp:Button ID="btnPesquisar" runat="server" Text="Pesquisar" 
                style="position:absolute; top: 390px; left: 134px; width: 83px; height: 26px;" 
                onclick="btnPesquiar_Click" CausesValidation="False" TabIndex="300"/>
           <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" 
                style="position:absolute; top: 390px; left: 221px; width: 73px; height: 26px;" 
                onclick="btnCancelar_Click" TabIndex="600"/>
           <asp:Button ID="btnExcluir" runat="server" Text="Excluir" 
                style="position:absolute; top: 390px; left: 298px; width: 65px; height: 26px;" 
                onclick="btnExcluir_Click" TabIndex="700"/>
           <asp:Button ID="btnImprimirOS" runat="server" Text="Imprimir O.S" 
                style="position:absolute; top: 390px; left: 368px; width: 90px; height: 26px;" 
                onclick="btnImprimirOS_Click" TabIndex="900"/>
           <asp:Button ID="btnConcluir" runat="server" Text="Concluir"  Visible="false" 
                style="position:absolute; top: 389px; left: 505px; width: 67px; height: 26px;" 
                onclick="btnConcluir_Click" TabIndex="400"/>
           <asp:Button ID="btnEnviarAprovacao" runat="server" Text="Enviar p/ Aprovação" Visible="false" 
                style="position:absolute; top: 389px; left: 576px; width: 145px; height: 26px;" 
                onclick="btnEnviarAprovacao_Click" TabIndex="500"/>
           <asp:Button ID="btnExecucao" runat="server" Text="Execução" Visible="false" 
                style="position:absolute; top: 389px; left: 725px; width: 75px; height: 26px;" 
                onclick="btnExecucao_Click" TabIndex="800"/>

        <asp:Panel ID="pnlJustificativa" runat="server" Enabled="false"
                style="position:absolute; top:130px; left:509px; width:467px; height:212px"
                BorderStyle="Solid" BorderWidth="1px">
            <asp:Label ID="lblJustificativa" runat="server" Text="Justificativa"
                style="position:absolute; top: 00px; left: 3px; height: 20px; width: 348px;" 
                BackColor="White" Font-Bold="True" Font-Size="Small" ForeColor="#3333FF"></asp:Label>
            <asp:TextBox ID="TxtJustificativa" runat="server" 
                style="position:absolute; top: 25px; left: 12px; width: 416px; height: 80px;" 
                TextMode="MultiLine"></asp:TextBox>
        </asp:Panel>

        <asp:Panel ID="pnlConclusao" runat="server" Enabled="false"
                style="position:absolute; top:100px; left:501px; width:475px; height:269px"
                BorderStyle="Solid" BorderWidth="1px">
            <asp:Label ID="lblTituloConclusao" runat="server" Text="Conclusão Atendimento"
                style="position:absolute; top: 00px; left: 3px; height: 20px; width: 348px;" 
                BackColor="White" Font-Bold="True" Font-Size="Small" ForeColor="#3333FF"></asp:Label>
            <asp:Label ID="lblDataInicio0" runat="server" Text="Cliente Satisfeito:"
                style="position:absolute; top: 45px; left: 3px; height: 17px; width: 103px;" 
                ForeColor="#3333FF"></asp:Label>
            <asp:TextBox ID="txtDtInicio" runat="server" 
                
                style="position:absolute; top: 20px; left: 109px; width: 126px; right: 215px;"></asp:TextBox>
            <asp:Label ID="lblDataFim" runat="server" Text="Conclusão:"
                style="position:absolute; top: 20px; left: 250px; height: 17px; width: 66px;" 
                ForeColor="#3333FF"></asp:Label>
            <asp:TextBox ID="txtConclusao" runat="server" 
                style="position:absolute; top: 20px; left: 315px; width: 126px;"></asp:TextBox>
            <asp:Label ID="lblAtestado" runat="server" Text="Atestado por: "
                style="position:absolute; top: 70px; left: 3px; height: 17px; width: 89px;" 
                ForeColor="#3333FF"></asp:Label>
            <asp:TextBox ID="txtAtestado" runat="server" 
                style="position:absolute; top: 70px; left: 107px; width: 330px;"></asp:TextBox>
            <asp:Label ID="lblConcluido" runat="server" Text="Concluída por: "
                style="position:absolute; top: 100px; left: 3px; height: 17px; width: 89px;" 
                ForeColor="#3333FF"></asp:Label>
            <asp:TextBox ID="txtConcluido" runat="server" 
                style="position:absolute; top: 100px; left: 107px; width: 331px;"></asp:TextBox>
            <asp:Label ID="lblObs" runat="server" Text="Obs.:" 
                style="position:absolute; top: 128px; left: 3px; height: 18px; width: 33px;" 
                ForeColor="#3333FF"></asp:Label>
            <asp:TextBox ID="txtObs" runat="server" 
                style="position:absolute; top: 130px; left: 40px; width: 426px; height: 128px;" 
                TextMode="MultiLine"></asp:TextBox>
            <asp:Label ID="lblDataInicio1" runat="server" ForeColor="#3333FF" 
                style="position:absolute; top: 20px; left: 3px; height: 17px; width: 40px;" 
                Text="Início:"></asp:Label>
            <asp:Label ID="lblSatisfacao" runat="server" ForeColor="#3333FF" 
                style="position:absolute; top: 46px; left: 109px; height: 17px; width: 108px;"></asp:Label>
        </asp:Panel>
            
            <asp:Label ID="Label21" runat="server" Text="*"
                style="position:absolute; top: 188px; left: 480px; height: 12px; width: 11px;" 
                ForeColor="Red"></asp:Label>

            <asp:DropDownList ID="cmbOrigemOS" runat="server" 
                style="position:absolute; top: 64px; left: 800px; width: 156px; right: 24px;" 
                TabIndex="11"></asp:DropDownList>

    </div>

</asp:Content>

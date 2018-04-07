<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="webATE_OSConclusao.aspx.cs" Inherits="HzWebManutencao.ATE.webATE_OSConclusao" %>
<%@ Register src="~/Controles/Mensagem.ascx" tagname="Mensagem" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Mensagem ID="CaixaMensagem" runat="server" />
    <div  style="position:relative; height: 430px; width: 850px; top: 12px; left: -54px; margin-left: 40px;">
       <asp:Label ID="lblAcao" runat="server" Text="lblAcao"
            style="position:absolute; top: 3px; left: 7px; height: 20px; width: 835px;" 
            BackColor="White" Font-Bold="True" Font-Size="Medium" 
            ForeColor="Maroon"></asp:Label>

        <asp:Label ID="Label1" runat="server" Text="Obra: "
            style="position:absolute; top: 43px; left: 11px; height: 17px; width: 40px;"></asp:Label>
        <asp:TextBox ID="TxtObra" runat="server"
            
            
            style="position:absolute; top: 43px; left: 188px; height: 22px; width: 623px;"></asp:TextBox>

        <asp:Label ID="Label3" runat="server" Text="Data Início Atendimento: "
            style="position:absolute; top: 73px; left: 11px; height: 17px; width: 157px;"></asp:Label>
        <asp:TextBox ID="TxtInicioAtendimento" runat="server"
            style="position:absolute; top: 73px; left: 188px; height: 22px; width: 100px;" 
            TabIndex="1"></asp:TextBox>
            <asp:MaskedEditExtender ID="TxtInicioAtendimento_MaskedEditExtender" runat="server" 
                Enabled="True" 
                TargetControlID="TxtInicioAtendimento" CultureName="pt-BR" 
                Mask="99/99/9999" MaskType="Date" 
                AcceptAMPM="true" MessageValidatorTip="false"
                UserDateFormat="None"></asp:MaskedEditExtender>
             <asp:RequiredFieldValidator ID="RvfInicioAtendimento" runat="server"  
                ControlToValidate="TxtInicioAtendimento"
                style="position:absolute; top: 75px; left: 579px; height: 22px; width: 157px; "
                ErrorMessage="Data de início de atendimento em branco!" Display="None"></asp:RequiredFieldValidator>

        <asp:TextBox ID="txtHoraIni" runat="server"
            style="position:absolute; top: 73px; left: 303px; height: 22px; width: 40px;" 
            TabIndex="2"></asp:TextBox>
            <asp:MaskedEditExtender ID="txHora_MaskedEditExtender" runat="server" 
                TargetControlID="txtHoraIni" CultureName="pt-BR" 
                Mask="99:99" MaskType="Time"></asp:MaskedEditExtender>
          <asp:RequiredFieldValidator ID="rvfHoraIni" runat="server"  
                ControlToValidate="txtHoraIni"
                style="position:absolute; top: 77px; left: 751px; height: 15px; width: 108px;"
                ErrorMessage="Hora de início de atendimento em branco!" Display="None"></asp:RequiredFieldValidator>

        <asp:Label ID="Label4" runat="server" Text="Data Término Atendimento: "
            style="position:absolute; top: 103px; left: 11px; height: 17px; width: 190px;"> </asp:Label>
        <asp:TextBox ID="TxtConclusaoAtendimento" runat="server"
            style="position:absolute; top: 103px; left: 188px; height: 22px; width: 100px;" 
            TabIndex="3"></asp:TextBox>
            <asp:MaskedEditExtender ID="TxtConclusaoAtendimento_MaskedEditExtender" runat="server" 
                Enabled="True" 
                TargetControlID="TxtConclusaoAtendimento" CultureName="pt-BR" 
                Mask="99/99/9999" MaskType="Date" 
                UserDateFormat="None"></asp:MaskedEditExtender>
            <asp:RequiredFieldValidator ID="RvfConclusaoAtendimento" runat="server"  
                ControlToValidate="TxtConclusaoAtendimento"
                style="position:absolute; top: 99px; left: 578px; height: 22px; width: 114px; right: 158px;"
                ErrorMessage="Data de conclusão de atendimento em branco!" 
            Display="None"></asp:RequiredFieldValidator>

        <asp:TextBox ID="txtHoraFim" runat="server"
            style="position:absolute; top: 103px; left: 303px; height: 22px; width: 40px;" 
            TabIndex="4"></asp:TextBox>
            <asp:MaskedEditExtender ID="txtHoraFim_MaskedEditExtender1" runat="server" 
                    TargetControlID="txtHoraFim" CultureName="pt-BR" 
                    Mask="99:99" MaskType="Time"></asp:MaskedEditExtender>
            <asp:RequiredFieldValidator ID="rvfHoraFim" runat="server"  
                ControlToValidate="txtHoraFim"
                style="position:absolute; top: 100px; left: 749px; height: 15px; width: 108px;"
                ErrorMessage="Hora de conclusão de atendimento em branco!" 
            Display="None"></asp:RequiredFieldValidator>

        <asp:CompareValidator ID="CompareValidator2" runat="server" 
                style="position:absolute; top: 10px; left: 588px; width: 147px; height: 26px;"
                ErrorMessage="Data final menor que data inicial!" 
                ControlToCompare="TxtInicioAtendimento" ControlToValidate="TxtConclusaoAtendimento" 
                Display="None" Operator="GreaterThanEqual" 
                Type="Date"></asp:CompareValidator>

       <asp:Label ID="Label6" runat="server" Text="Gestor do Serviço: "
            style="position:absolute; top: 133px; left: 11px; height: 17px; width: 190px;"> </asp:Label>
        <asp:TextBox ID="TxtAtestador" runat="server"
            style="position:absolute; top: 133px; left: 188px; height: 22px; width: 300px;" 
            TabIndex="5"> </asp:TextBox>
        <asp:RequiredFieldValidator ID="RvfAtestador" runat="server"  ControlToValidate="TxtAtestador"
            style="position:absolute; top: 383px; left: 215px; height: 22px; width: 50px;"
            ErrorMessage="Nome do atestador em branco!" Display="None"></asp:RequiredFieldValidator>

       <asp:Label ID="Label5" runat="server" Text="Matrícula Gestor do Serviço: "
            
            
            style="position:absolute; top: 133px; left: 500px; height: 17px; width: 169px;"> </asp:Label>
        <asp:TextBox ID="txtMatriculaGestor" runat="server"
            style="position:absolute; top: 132px; left: 675px; height: 22px; width: 136px;" 
            TabIndex="6"> </asp:TextBox>


        <asp:Label ID="Label7" runat="server" Text="Executante(s): "
            style="position:absolute; top: 163px; left: 11px; height: 17px; width: 190px;"> </asp:Label>
        <asp:TextBox ID="TxtExecutor" runat="server"
            style="position:absolute; top: 163px; left: 188px; height: 22px; width: 493px;" 
            TabIndex="7"> </asp:TextBox>

<%--        <asp:AutoCompleteExtender     
            ID="AutoCompleteExtender1"   
            TargetControlID="TxtExecutor"   
            runat="server" 
            servicepath = "../../DynamicPopulate.asmx"
            servicemethod = "RetornaFuncionarioObra"
            DelimiterCharacters=","
            MinimumPrefixLength = "1"
            CompletionSetCount = "12"
            EnableCaching ="True"
            UseContextKey="True" /> --%>

         <asp:RequiredFieldValidator ID="RvfExecutor" runat="server"  ControlToValidate="TxtExecutor"
            style="position:absolute; top: 382px; left: 309px; height: 22px; width: 50px;"
            ErrorMessage="Nome do executor em branco!" Display="None"></asp:RequiredFieldValidator>

        <asp:Label ID="Label12" runat="server" Text="Cliente Satisfeito com o Serviço: "
            style="position:absolute; top: 198px; left: 11px; height: 17px; width: 198px;"></asp:Label>
        <asp:RadioButtonList ID="rbSatisfacaoCliente" runat="server" TabIndex="8"
            style="position:absolute; top: 194px; left: 201px; width: 120px; height: 29px;" 
            RepeatDirection="Horizontal">
            <asp:ListItem Value="1">Sim</asp:ListItem>
            <asp:ListItem Value="0">Não</asp:ListItem>
        </asp:RadioButtonList>
        <asp:Label ID="Label2" runat="server" Text="Cliente Satisfeito com o Prazo de Atendimento:" Visible="false"
            style="position:absolute; top: 198px; left: 336px; height: 17px; width: 280px;"></asp:Label>
        <asp:RadioButtonList ID="rbSatisfacaoPrazo" runat="server" Visible="false" TabIndex="9"
            style="position:absolute; top: 194px; left: 615px; width: 109px; height: 29px;" 
            RepeatDirection="Horizontal">
            <asp:ListItem Value="1">Sim</asp:ListItem>
            <asp:ListItem Value="0">Não</asp:ListItem>
        </asp:RadioButtonList>


        <asp:RequiredFieldValidator ID="rvfSatisfacaoServico" runat="server"  ControlToValidate="rbSatisfacaoCliente"
            style="position:absolute; top: 216px; left: 714px; height: 22px; width: 120px;"
            ErrorMessage="Selecione a satisfação do cliente quanto ao Serviço!" 
            Display="None"></asp:RequiredFieldValidator>

<%--        <asp:RequiredFieldValidator ID="rvfSatisfacaoPrazo" runat="server"  ControlToValidate="rbSatisfacaoPrazo"
            style="position:absolute; top: 195px; left: 728px; height: 22px; width: 112px;"
            ErrorMessage="Selecione a satisfação do cliente Quanto ao Prazo!" Display="None"></asp:RequiredFieldValidator>
--%>
        <asp:Label ID="Label11" runat="server" Text="Detalhamento do Serviço Executado:" 
            style="position:absolute; top: 226px; left: 13px; width: 222px;"> </asp:Label>
        <asp:TextBox ID="txtObservacaoConclusao" runat="server" 
            style="position:absolute; top: 246px; left: 13px; width: 802px; height: 111px;" 
            TextMode="MultiLine" TabIndex="10"> </asp:TextBox>

        <asp:Button ID="btnVoltar" runat="server" Text="Voltar" 
            style="position:absolute; top: 371px; left: 80px; width: 67px; height: 29px;" 
            CausesValidation="False" onclick="btnVoltar_Click" TabIndex="9" />
        
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
            style="position:absolute; top: 371px; left: 438px; width: 169px; height: 40px;"
            ShowMessageBox="True" ShowSummary="False" />
        
        <asp:Button ID="btnSave" runat="server" Text="Gravar" 
            style="position:absolute; top: 372px; left: 8px; width: 67px; height: 29px;" 
            onclick="btnSave_Click" TabIndex="8" />

    </div>
</asp:Content>

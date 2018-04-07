<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="webFAT_AberturaFaturamentoObra.aspx.cs" Inherits="HzWebManutencao.Faturamento.webFAT_AberturaFaturamentoObra" %>
<%@ Register src="~/UserControls/OKMessageBox.ascx" tagname="MsgBox" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:MsgBox ID="MsgBox" runat="server" />

    <div  style="position:relative; height: 408px; width: 1100px; top: 12px; left: -50px; margin-left: 80px;">

        <asp:Label ID="Label9" runat="server" 
        style="position:absolute; top: 5px; left: 13px; width: 1078px; text-align: center;" 
        Text="Emissão da Nota Fiscal" Font-Bold="True" 
        Font-Size="Medium" ></asp:Label>

       <asp:Label ID="lblObra" runat="server" 
            style="position:absolute; top: 28px; left: 13px; width: 128px;" 
            Text="Nome da Obra:" Font-Bold="True" 
            Font-Size="Medium" ></asp:Label>

       <asp:Label ID="lblNomeObra" runat="server" 
            style="position:absolute; top: 28px; left: 145px; width: 679px;" 
            Text="Obra" Font-Bold="True" 
            Font-Size="Medium" ></asp:Label>

       <asp:Label ID="lblItMaterial3" runat="server" 
            style="position:absolute; top: 57px; left: 13px; width: 114px; height: 15px;" 
            Text="Nome do Destinatário:"></asp:Label>
       <asp:TextBox ID="txtNomeDestinatario" runat="server"  
            style="position:absolute; top: 53px; left: 128px; width: 397px; "></asp:TextBox>

       <asp:Label ID="Label1" runat="server" 
            style="position:absolute; top: 57px; left: 549px; width: 90px; " 
            Text="Nome do Órgão:"></asp:Label>
       <asp:TextBox ID="txtNomeOrgao" runat="server"
            style="position:absolute; top: 53px; left: 633px; width: 455px; " 
            TabIndex="1"></asp:TextBox>

        <asp:Label ID="Label2" runat="server" Text="Período :" 
            style="position:absolute; top: 91px; left: 13px; width: 62px;"></asp:Label>
        <asp:TextBox ID="txtDataInicial" runat="server" 
            
            style="position:absolute; top: 86px; left: 128px; width: 85px; right: 915px;" 
            TabIndex="2"></asp:TextBox> 
            <asp:MaskedEditExtender ID="txtDataInicial_MaskedEditExtender" runat="server" Enabled="True"
                TargetControlID="txtDataInicial" CultureName="pt-BR" 
                Mask="99/99/9999" MaskType="Date" UserDateFormat="DayMonthYear" 
                UserTimeFormat="TwentyFourHour"></asp:MaskedEditExtender>
                <asp:CalendarExtender ID="txtDataInicial_CalendarExtender" runat="server" 
                    Enabled="True" TargetControlID="txtDataInicial" Format="dd/MM/yyyy"></asp:CalendarExtender>
       
        <asp:TextBox ID="txtDataFinal" runat="server"
            
            style="position:absolute; top: 86px; left: 229px; width: 85px; right: 786px; " 
            TabIndex="3"></asp:TextBox> 
            <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Enabled="True" 
                TargetControlID="txtDataFinal" CultureName="pt-BR" 
                Mask="99/99/9999" MaskType="Date" UserDateFormat="DayMonthYear" 
                UserTimeFormat="TwentyFourHour"></asp:MaskedEditExtender>
                <asp:CalendarExtender ID="CalendarExtender1" runat="server" 
                    Enabled="True" TargetControlID="txtDataFinal" Format="dd/MM/yyyy"></asp:CalendarExtender>

       <asp:Label ID="Label3" runat="server" 
            style="position:absolute; top: 91px; left: 333px; width: 99px; " 
            Text="Número Nota Fiscal:"></asp:Label>
       <asp:TextBox ID="txtNuNotaFiscal" runat="server" 
            style="position:absolute; top: 86px; left: 435px; width: 146px; " 
            TabIndex="4"></asp:TextBox>

       <asp:Label ID="Label4" runat="server" 
            style="position:absolute; top: 91px; left: 598px; width: 99px; " 
            Text="Emissão Nota Fiscal:"></asp:Label>
       <asp:TextBox ID="txtEmissaoNota" runat="server"
            
            
            style="position:absolute; top: 86px; left: 702px; width: 102px; right: 296px;" 
            TabIndex="5"></asp:TextBox> 
            <asp:MaskedEditExtender ID="MaskedEditExtender2" runat="server" Enabled="True" 
                TargetControlID="txtEmissaoNota" CultureName="pt-BR" 
                Mask="99/99/9999" MaskType="Date" UserDateFormat="DayMonthYear" 
                UserTimeFormat="TwentyFourHour"></asp:MaskedEditExtender>
                <asp:CalendarExtender ID="CalendarExtender2" runat="server" 
                    Enabled="True" TargetControlID="txtEmissaoNota" Format="dd/MM/yyyy"></asp:CalendarExtender>

      <asp:Label ID="Label6" runat="server" 
            style="position:absolute; top: 91px; left: 819px; width: 83px; " 
            Text="Valor Nota Fiscal:"></asp:Label>
       <asp:TextBox ID="txtValorNota" runat="server"
            style="position:absolute; top: 86px; left: 908px; width: 146px;   " 
            TabIndex="6"></asp:TextBox>
                <asp:MaskedEditExtender ID="MaskedEditExtender4"    
                    InputDirection = "RightToLeft"
                    runat="server"
                    TargetControlID="txtValorNota" 
                    Mask="999999999,99"
                    MessageValidatorTip="true" 
                    OnFocusCssClass="MaskedEditFocus" 
                    OnInvalidCssClass="MaskedEditError"
                    MaskType =  "Number" 
                    AcceptNegative= "None" 
                    ClearMaskOnLostFocus="true"
                    ErrorTooltipEnabled="True"/>

        <asp:Label ID="Label5" runat="server" Text="Data:" 
            style="position:absolute; top: 189px; left: 699px; width: 34px;"></asp:Label>

       <asp:Label ID="Label8" runat="server" Text="Número:" 
            style="position:absolute; top: 188px; left: 870px; width: 48px; height: 15px;"></asp:Label>

       <asp:TextBox ID="txtNumeroDoc" runat="server"
            style="position:absolute; top: 184px; left: 917px; width: 102px;" 
            TabIndex="100"></asp:TextBox>

        <asp:Label ID="DadosCarta" runat="server" Text="Dados Carta Orion" 
                style="position:absolute; top: 157px; left: 675px; height: 17px; width: 119px; right: 306px;" 
                Font-Size="Small" Font-Bold="True"></asp:Label>

        <asp:Label ID="Label7" runat="server" Text="Tipo de Serviço :" 
            style="position:absolute; top: 127px; left: 13px; width: 93px;"></asp:Label>
         <asp:Label ID="lblTipoDoc" runat="server" Text="Tipos de Documentos" 
                style="position:absolute; top: 157px; left: 13px; height: 17px; width: 151px; " 
                Font-Size="Small" Font-Bold="True"></asp:Label>
         <asp:ListBox ID="lstDocumentos" runat="server" 
             style="position:absolute; top: 176px; left: 13px; width: 294px; height: 133px;" 
             SelectionMode="Multiple" Font-Size="Small">
         </asp:ListBox>

        <asp:Button ID="btnDesvincular" runat="server" Text="&lt;&lt;" 
             style="position:absolute; top: 256px; left: 313px; width: 49px; height: 25px; bottom: 127px;" 
              CausesValidation="False" Font-Bold="True" 
                Font-Names="Calibri" Font-Size="Medium" Font-Italic="False" 
                onclick="btnDesvincular_Click"/>
        <asp:Button ID="btnVincular" runat="server" Text=">>" 
             style="position:absolute; top: 217px; left: 314px; width: 49px; height: 25px; bottom: 166px;" 
             onclick="btnVincular_Click" CausesValidation="False" Font-Bold="True" 
                Font-Names="Calibri" Font-Size="Medium"/>

        <asp:Label ID="lblDocAnexados" runat="server" Text="Documentos Anexados" 
                style="position:absolute; top: 157px; left: 376px; height: 17px; width: 151px; right: 573px;" 
                Font-Size="Small" Font-Bold="True"></asp:Label>

        <asp:ListBox ID="lstDocAnexo" runat="server"
                 style="position:absolute; top: 176px; left: 373px; width: 294px; height: 133px;" 
                 SelectionMode="Multiple" Font-Size="Small"></asp:ListBox>

         <asp:DropDownList ID="cmbTipoServico" runat="server" Font-Size="Small"
            
            style="position: absolute; top: 119px; left: 128px; width: 451px;" 
            TabIndex="7">
        </asp:DropDownList>

        <asp:Label ID="Label10" runat="server" Text="Email Engenheiro" 
            style="position:absolute; top: 219px; left: 674px; width: 89px;"></asp:Label>
       <asp:TextBox ID="txtEmailEng" runat="server"
            style="position:absolute; top: 235px; left: 672px; width: 417px; " 
            TabIndex="8"></asp:TextBox>

        <asp:Label ID="Label11" runat="server" Text="Email Auxiliar Administrativo" 
            style="position:absolute; top: 271px; left: 674px; width: 162px;"></asp:Label>
       <asp:TextBox ID="txtEmailAux" runat="server"
            style="position:absolute; top: 285px; left: 672px; width: 417px; " 
            TabIndex="9"></asp:TextBox>

        <asp:TextBox ID="txtDtCartaOrion" runat="server"
            
            
            style="position:absolute; top: 184px; left: 731px; width: 96px; right: 273px;" 
            TabIndex="99"></asp:TextBox> 
            <asp:MaskedEditExtender ID="MaskedEditExtender3" runat="server" Enabled="True" 
                TargetControlID="txtDtCartaOrion" CultureName="pt-BR" 
                Mask="99/99/9999" MaskType="Date" UserDateFormat="DayMonthYear" 
                UserTimeFormat="TwentyFourHour"></asp:MaskedEditExtender>
                <asp:CalendarExtender ID="CalendarExtender3" runat="server" 
                    Enabled="True" TargetControlID="txtDtCartaOrion" Format="dd/MM/yyyy"></asp:CalendarExtender>

       <asp:Button ID="btnGravar" runat="server"  
            style="position: absolute; top: 325px; left: 19px; height: 25px; width: 88px;" 
            Text="Gravar" onclick="btnGravar_Click" />

        <asp:Button ID="btnCartaOrion" runat="server" 
        style="position: absolute; top: 326px; left: 114px; height: 25px; width: 123px;" 
        Text="Gerar Carta Orion" onclick="btnCartaOrion_Click" />

        <asp:Button ID="btnEnviarEmail" runat="server" 
        style="position: absolute; top: 327px; left: 244px; height: 25px; width: 123px;" 
        Text="Enviar Email" onclick="btnEnviarEmail_Click" />

        <asp:Button ID="btnVoltar" runat="server" 
            style="position: absolute; top: 327px; left: 375px; height: 25px; width: 88px;" 
            Text="Voltar" onclick="btnVoltar_Click" />
    </div>
</asp:Content>

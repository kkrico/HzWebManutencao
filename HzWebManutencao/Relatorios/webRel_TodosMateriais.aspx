<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="webRel_TodosMateriais.aspx.cs" Inherits="HzWebManutencao.Relatorios.webRel_TodosMateriais" %>
<%@ Register src="~/Controles/Mensagem.ascx" tagname="Mensagem" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
 <asp:Mensagem ID="CaixaMensagem" runat="server" />
    <div  style="position:relative; height: 492px; width: 1001px; top: 12px; left: -61px; margin-left: 40px; margin-right: 0px;">
       <asp:Label ID="lblAcao" runat="server" Text="Relatório de Materiais" 
            style="position:absolute; top: 3px; left: 7px; height: 20px; width: 899px;" 
            BackColor="White" Font-Bold="True" Font-Size="Medium" ForeColor="Maroon"></asp:Label>

        <asp:Label ID="lblObra" runat="server" Text="Obra:" 
            style="position:absolute; top: 43px; left: 10px; height: 19px; width: 38px;" 
            Font-Names="Calibri" Font-Size="Small"></asp:Label>
            
         <asp:DropDownList ID="cmbObra" runat="server" 
            style="position:absolute; top: 40px; left: 49px; width: 804px; "
            AutoPostBack="True" onselectedindexchanged="cmbObra_SelectedIndexChanged" 
            Font-Names="Calibri" Font-Size="Small" >
         </asp:DropDownList>

        <asp:Label ID="lblTipo" runat="server" Text="Tipo" 
            style="position:absolute; top: 81px; left: 52px; width: 51px; height: 16px; bottom: 402px;"
            Font-Bold="True" Font-Names="Calibri"></asp:Label>

        <asp:RadioButtonList ID="rbdTipo" runat="server" 
            style="position:absolute; top: 95px; left: 46px; width: 202px; height: 18px;" 
            RepeatColumns="3" 
            RepeatDirection="Horizontal" Font-Bold="True" Font-Names="Calibri">
            <asp:ListItem Value="0">Material</asp:ListItem>
            <asp:ListItem Value="1">Serviço</asp:ListItem>
            <asp:ListItem Value="2" Selected="True">Todos</asp:ListItem>
        </asp:RadioButtonList>

        <asp:Label ID="lblRelatorios" runat="server" Text="Selecione o Relatório" 
            style="position:absolute; top: 143px; left: 52px; width: 171px; height: 16px; bottom: 340px;"
            Font-Bold="True" Font-Names="Calibri"></asp:Label>
        <asp:RadioButtonList ID="rdbMateriais" runat="server" AutoPostBack="True"
            style="position:absolute; top: 166px; left: 46px; width: 429px; height: 88px; right: 516px;" 
            RepeatColumns="1" 
            RepeatDirection="Vertical" 
            onselectedindexchanged="rdbMateriais_SelectedIndexChanged">
            <asp:ListItem Value="LGMT">Lista Geral de Materiais</asp:ListItem>
            <asp:ListItem Value="LMUO">Lista de Materiais Utilizados por Obra</asp:ListItem>
            <asp:ListItem Value="LMUA">Lista de Materiais Utilizados Agrupados por Obra</asp:ListItem>
        </asp:RadioButtonList>


        <asp:Label ID="lblDtOs" runat="server" Text="Data da Ordem de Serviço" 
            style="position:absolute; top: 270px; left: 52px; width: 154px; height: 16px; bottom: 213px;"
            Font-Bold="True" Font-Names="Calibri"></asp:Label>

        <asp:RadioButtonList ID="rdbData" runat="server"
            AutoPostBack="True"
            style="position:absolute; top: 289px; left: 46px; width: 240px; height: 20px; right: 703px;" 
            RepeatColumns="2" BorderStyle="None" Font-Bold="True" Font-Names="Calibri" 
            RepeatDirection="Horizontal">
            <asp:ListItem Selected="True" Value="CL">Data Conclusão</asp:ListItem>
            <asp:ListItem Value="AB">Data Abertura</asp:ListItem>
        </asp:RadioButtonList>

        <asp:Label ID="lblPeriodo" runat="server" Text="Período:" 
            style="position:absolute; top: 293px; left: 336px; width: 51px; height: 16px; bottom: 190px;"
            Font-Bold="True" Font-Names="Calibri"></asp:Label>

        <asp:TextBox ID="txtDataInicial" runat="server" Font-Names="Calibri" Font-Size="Small" 
            style="position:absolute; top: 290px; left: 388px; width: 95px;"></asp:TextBox> 
            <asp:MaskedEditExtender ID="txtDataInicial_MaskedEditExtender" runat="server" 
                Enabled="True" 
                TargetControlID="txtDataInicial" CultureName="pt-BR" 
                Mask="99/99/9999" MaskType="Date" UserDateFormat="DayMonthYear" 
                UserTimeFormat="TwentyFourHour"></asp:MaskedEditExtender>
            <asp:CalendarExtender ID="txtDataInicial_CalendarExtender" runat="server" 
                Enabled="True" TargetControlID="txtDataInicial" Format="dd/MM/yyyy"></asp:CalendarExtender>

        <asp:Label ID="Label1" runat="server" Text="a" 
            style="position:absolute; top: 293px; left: 495px; height: 17px; width: 7px;" 
            Font-Names="Calibri" Font-Size="Small"></asp:Label>

        <asp:TextBox ID="txtDataFinal" runat="server" Font-Names="Calibri" Font-Size="Small" 
            style="position:absolute; top: 289px; left: 516px; width: 95px; right: 377px;"></asp:TextBox> 
            <asp:MaskedEditExtender ID="txtDataFinal_MaskedEditExtender" runat="server" 
                Enabled="True" 
                TargetControlID="txtDataFinal" CultureName="pt-BR" 
                Mask="99/99/9999" MaskType="Date" UserDateFormat="DayMonthYear" 
                UserTimeFormat="TwentyFourHour"></asp:MaskedEditExtender>
                <asp:CalendarExtender ID="txtDataFinal_CalendarExtender" runat="server" 
                    Enabled="True" TargetControlID="txtDataFinal" Format="dd/MM/yyyy"></asp:CalendarExtender>

        <asp:CompareValidator ID="CompareValidator1" runat="server" 
            style="position:absolute; top: 394px; left: 539px; width: 131px; height: 26px;"
            ErrorMessage="Data final menor que data inicial!" 
            ControlToCompare="txtDataInicial" ControlToValidate="txtDataFinal" 
            Display="None" Operator="GreaterThanEqual" 
            Type="Date">
        </asp:CompareValidator>
        
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
            style="position:absolute; top: 387px; left: 677px; width: 155px; height: 34px;"
            ShowMessageBox="True" ShowSummary="False" />




        <asp:UpdatePanel runat="server" ID="upProcessamento" UpdateMode="Conditional">
            <ContentTemplate>
                <%-- A área a ser processada, deve ser inserida dentro do Content Template --%>
<%--                <p>Clique no botão abaixo para processar alguma coisa:</p>
--%>                <%--Um botão com o evento Click já programado --%>

            <asp:Button ID="btnGerarRel" runat="server" Text="Gerar Relatório" CausesValidation = "true"
                style="position:absolute; top: 87px; left: 745px; height: 24px; width: 104px; right: 152px;" 
                Font-Bold="True" Font-Names="Calibri" Font-Size="Small" onclick="btnGerarRel_Click"
                />

           <asp:Button ID="btnImprimir" runat="server" Text="Imprimir" CausesValidation = "false" 
                style="position:absolute; top: 122px; left: 747px; height: 24px; width: 69px; right: 185px;" 
                Font-Bold="True" Font-Names="Calibri" Font-Size="Small" 
                Visible="False" onclick="btnImprimir_Click" />

            </ContentTemplate>
            <Triggers>
                <%--Dentro dessa tag ela reconhece como um postback assíncrono, passo o mesmo 
                Id do botão dentro de ControlID e o EventName como Click -->
                Para outros componentes é só trocar o ControlID e o EventName pelos
                respectivos IDs e Eventos de outros controles--%>
                <asp:AsyncPostBackTrigger ControlID="btnGerarRel" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
        <%--Este componente que é o responsável por dar ideia de processamento ao usuário
        mediante uma requisição do mesmo --%>
       <asp:UpdateProgress ID="UpdateProgress1" runat="server"  
            style="position:absolute; top: 90px; left: 400px; width: 100px;">
            <ProgressTemplate>
                <%--Este é o componente responsável pelo gif de processamento ou qualquer
                    outro conteúdo que eu queira exibir --%>
                <%--<img src="../App_Themes/General/ajax-loader.gif" style="position:absolute; top: 71px; left: 227px; z-index:1000" alt="Processando ..." />--%>                
                <img src="../App_Themes/General/loading.gif" style="position:absolute; top: 19px; left: 63px; z-index:1000" alt="Processando ..." />
            </ProgressTemplate>
        </asp:UpdateProgress>

    </div>
</asp:Content>

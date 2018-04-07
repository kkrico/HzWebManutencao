<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="webATE_OSArquivoProtocoladoCEF.aspx.cs" Inherits="HzWebManutencao.ATE.webATE_OSArquivoProtocoladoCEF" %>
<%@ Register src="~/Controles/Mensagem.ascx" tagname="Mensagem" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
        function pageLoad(sender, args) {
        }

        function startUpload(sender, args) {
            $('#uploadMessage p').html();
            $('#uploadMessage').hide();
        }

        function uploadComplete(sender, args) {
            showUploadMessage(args.get_fileName() + " Arquivo gravado com sucesso - " + +args.get_length() + " bytes", '');
        }

        function uploadError(sender, args) {
            showUploadMessage("Erro durante gravação do arquivo. " + args.get_errorMessage(), '#ff0000');
        }

        function showUploadMessage(text, color) {
            $('#uploadMessage p').html(text).css('color', color);
            $('#uploadMessage').show();
        }
 
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
    <asp:AsyncFileUpload ID="pageBannerUpload" 
        OnClientUploadError="uploadError"
        OnClientUploadStarted="startUpload"
        OnClientUploadComplete="uploadComplete"
        onuploadedcomplete="upload_UploadedComplete"
        ThrobberID="myThrobber"
        IsInFileUploadPostBack="true"
        runat="server" />

 <%--   <asp:Label runat="server" ID="myThrobber" Style="display: none;"><img align="middle" alt="" src="../imagens/loading.gif"/></asp:Label>
--%>
<%--    <asp:AsyncFileUpload ID="pageBannerUpload" ThrobberID="myThrobber" runat="server" 
    onuploadedcomplete="upload_UploadedComplete"/>--%>
    <div id="uploadMessage"><p></p></div>

    <asp:Mensagem ID="CaixaMensagem" runat="server" />
    <div style="position:relative; height: 469px; width: 1163px; top: 12px; left: -54px; margin-left: 40px;">

         <asp:Label ID="Label21" runat="server" Text="Arquivos Recebidos CEF"  Font-Bold="true"
                style="position:absolute; top: 7px; left: 6px; height: 20px; width: 288px; text-align:center"></asp:Label>

        <asp:Button ID="btnVerArquivos" runat="server" Text="Carregar Arquivos" CausesValidation = "false"
            style="position:absolute; top: 33px; left: 9px; width: 120px;" 
            Font-Bold="True" Font-Names="Calibri" Font-Size="Small" 
            onclick="btnVerArquivos_Click"/>

        <asp:Button ID="btnRemoverArquivos" runat="server" Text="Remover Arquivos" CausesValidation = "false"
            style="position:absolute; top: 33px; left: 156px; width: 120px;" 
            Font-Bold="True" Font-Names="Calibri" Font-Size="Small" 
            onclick="btnRemoverArquivos_Click"/>

    <asp:ListBox ID="lstArquivo" runat="server"
             style="position:absolute; top: 64px; left: 7px; width: 280px; height: 377px;" 
            SelectionMode="Multiple" Font-Names="Calibri"  
             AutoPostBack="true"
            onselectedindexchanged="lstArquivo_SelectedIndexChanged">
    </asp:ListBox>

    <asp:Panel ID="pnlArquivo" runat="server" 
        style="position:absolute; top:5px; left:299px; width:855px; height:438px"
        BorderStyle="Solid" BorderWidth="1px">

      <asp:Label ID="Label12" runat="server" Text="Registro Tipo 1"  Font-Bold="true"
             
            style="position:absolute; top: 8px; left: 6px; height: 20px; width: 828px; text-align:center"></asp:Label>


        <asp:Label ID="Label3" runat="server" Text="Data Geração Arquivo:" 
             style="position:absolute; top: 35px; left: 10px; width: 137px;"></asp:Label>

         <asp:TextBox ID="txtDataGeracao" runat="server" 
                style="position:absolute; top: 35px; left: 158px; width: 97px;" 
                TabIndex="1"></asp:TextBox>

       <asp:Label ID="Label4" runat="server" Text="Hora Geração Arquivo:" 
             
            
            
            
            style="position:absolute; top: 35px; left: 272px; height: 20px; width: 142px;"></asp:Label>

        <asp:TextBox ID="txtHoraGeracao" runat="server" 
            style="position:absolute; top: 35px; left: 410px; width: 65px;" 
            TabIndex="1"></asp:TextBox>

       <asp:Label ID="Label11" runat="server" Text="Atividade do Chamado:" 
            style="position:absolute; top: 165px; left: 11px; width: 142px; right: 702px;"></asp:Label>

        <asp:TextBox ID="txtAtividade" runat="server" 
            style="position:absolute; top: 162px; left: 154px; width: 57px;" 
            TabIndex="1"></asp:TextBox>

         <asp:Label ID="Label1" runat="server" Text="Nome da Unidade:" 
             
             
            style="position:absolute; top: 61px; left: 11px; height: 18px; width: 112px;"></asp:Label>

         <asp:TextBox ID="txtObra" runat="server" 
                style="position:absolute; top: 62px; left: 157px; width: 463px;" 
                TabIndex="1"></asp:TextBox>

        <asp:Label ID="Label2" runat="server" Text="Código do Fornecedor:" 
             
            
            style="position:absolute; top: 87px; left: 10px; height: 20px; width: 140px;"></asp:Label>

         <asp:TextBox ID="txtCodFornecedor" runat="server" 
                style="position:absolute; top: 87px; left: 156px; width: 62px;" 
                TabIndex="1"></asp:TextBox>


            
           <asp:Label ID="Label5" runat="server" Text="Número do Chamado na CEF:" 
                 
            
            
            
            style="position:absolute; top: 35px; left: 492px; height: 20px; width: 181px;"></asp:Label>

         <asp:TextBox ID="txtNuChamandoCEF" runat="server" 
                style="position:absolute; top: 35px; left: 670px; width: 160px;" 
                TabIndex="1"></asp:TextBox>


      <asp:Label ID="Label7" runat="server" Text="Código do Banco:" 
             
            
            
            style="position:absolute; top: 87px; left: 270px; height: 20px; width: 111px;"></asp:Label>

         <asp:TextBox ID="txtCodBanco" runat="server" 
                style="position:absolute; top: 87px; left: 383px; width: 61px;" 
                TabIndex="1"></asp:TextBox>            

          <asp:Label ID="Label6" runat="server" Text="Código da Unidade:" 
             
             
             
            
            style="position:absolute; top: 87px; left: 480px; height: 20px; width: 120px;"></asp:Label>

        <asp:TextBox ID="txtCodUnidade" runat="server" 
            style="position:absolute; top: 87px; left: 605px; width: 115px;" 
            TabIndex="1"></asp:TextBox>            

      <asp:Label ID="Label8" runat="server" Text="Endereço da Unidade:" 
             
            
            style="position:absolute; top: 114px; left: 13px; height: 20px; width: 131px;"></asp:Label>

         <asp:TextBox ID="txtEndereco" runat="server" 
                style="position:absolute; top: 112px; left: 155px; width: 624px;" 
                TabIndex="1"></asp:TextBox>            

      <asp:Label ID="Label9" runat="server" Text="Cidade da Unidade:" 
            
            style="position:absolute; top: 138px; left: 10px; height: 20px; width: 131px;"></asp:Label>

         <asp:TextBox ID="txtCidade" runat="server" 
                style="position:absolute; top: 136px; left: 155px; width: 367px;" 
                TabIndex="1"></asp:TextBox>            

      <asp:Label ID="Label10" runat="server" Text="UF:" 
            style="position:absolute; top: 138px; left: 533px; height: 20px; width: 21px;"></asp:Label>

         <asp:TextBox ID="txtUF" runat="server" 
                style="position:absolute; top: 136px; left: 557px; width: 47px;" 
                TabIndex="1"></asp:TextBox>  

      <asp:Label ID="Label14" runat="server" Text="Contato no Local Atendimento:" 
            style="position:absolute; top: 210px; left: 10px; height: 20px; width: 190px;"></asp:Label>

        <asp:TextBox ID="txtContato" runat="server" 
            style="position:absolute; top: 209px; left: 198px; width: 272px;"></asp:TextBox>  

      <asp:Label ID="Label15" runat="server" Text="Telefone no Local Atendimento:" 
            
            style="position:absolute; top: 210px; left: 484px; height: 20px; width: 190px;"></asp:Label>

        <asp:TextBox ID="txtFone" runat="server" 
            style="position:absolute; top: 209px; left: 675px; width: 155px;"></asp:TextBox>  

     <asp:Label ID="Label16" runat="server" Text="Tipo de Serviço na Caixa:" 
             style="position:absolute; top: 237px; left: 10px; width: 152px;"></asp:Label>

        <asp:TextBox ID="txtServico" runat="server" 
            style="position:absolute; top: 237px; left: 171px; width: 654px;"></asp:TextBox>  

     <asp:Label ID="Label17" runat="server" Text="Prioridade do chamado:" 
             style="position:absolute; top: 266px; left: 10px; width: 156px;"></asp:Label>

        <asp:TextBox ID="txtPrioridade" runat="server" 
            style="position:absolute; top: 266px; left: 171px; width: 35px;"></asp:TextBox>  

     <asp:Label ID="Label18" runat="server" Text="Data Limite para Atendimento:" 
             style="position:absolute; top: 268px; left: 220px; width: 182px;"></asp:Label>

        <asp:TextBox ID="txtDataLimite" runat="server" 
            style="position:absolute; top: 266px; left: 404px; width: 186px;"></asp:TextBox>  

      <asp:Label ID="Label19" runat="server" Text="Registro Tipo 3"  Font-Bold="true"
            style="position:absolute; top: 293px; left: 8px; height: 20px; width: 837px; text-align:center"></asp:Label>

      <asp:Label ID="Label20" runat="server" Text="Descrição do Problema / Solicitação:" 
             
             
             
            
            
            
            style="position:absolute; top: 309px; left: 10px; height: 20px; width: 226px; bottom: 109px;"></asp:Label>

        <asp:TextBox ID="txtDescricao" runat="server"  Font-Names="Calibri"
            
            style="position:absolute; top: 324px; left: 11px; width: 829px; height: 66px; margin-top: 5px; margin-bottom: 4px;" 
            TextMode="MultiLine"></asp:TextBox>  

        <asp:Label ID="Label13" runat="server" Font-Bold="true" 
            style="position:absolute; top: 187px; left: 12px; height: 20px; width: 833px; text-align:center" 
            Text="Registro Tipo 2"></asp:Label>

        <asp:Button ID="btnGerarOS" runat="server" Text="Gerar Ordem de Serviço" CausesValidation = "false"
            style="position:absolute; top: 405px; left: 14px; height: 24px; width: 157px; right: 684px;" 
            Font-Bold="True" Font-Names="Calibri" Font-Size="Small" 
            onclick="btnGerarOS_Click"/>

   </asp:Panel>
 </div>
    
</asp:Content>

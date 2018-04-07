<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Mensagem.ascx.cs" Inherits="Apresentacao.Controles.Mensagem" %>
<link href="../Styles/ui-lightness/jquery-ui-1.8.11.custom.css" rel="stylesheet" type="text/css" />
<script src="../Scripts/jquery-1.5.1.min.js" type="text/javascript"></script>
<script src="../Scripts/jquery-ui-1.8.11.custom.min.js" type="text/javascript"></script>

<div id="divMensagem" title="">
    <div id="divFundoMensagem">
        <span ID="lblMensagem"></span>
    </div>
    <div id="divBotoes">
                    <asp:Button class="ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only "
            role="button" aria-disabled="false" ID="btnCustomiza" runat="server" Text="Salvar"
            OnClick="btnCustomiza_Click" />
    
    </div>
</div>
    
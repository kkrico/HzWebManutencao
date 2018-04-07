<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="webTABCentrodeCusto.aspx.cs" Inherits="HzWebManutencao.Horizon.webTABCentrodeCusto" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="height: 141px;">
    
        <br />
        <br />
        <div style="margin-top: 0px; position: absolute; top: 20px; left: 14px; height: 133px; width: 781px;">
            <asp:Label ID="Label1" runat="server" Text="Centro de Custo"></asp:Label>
            <br />
            <asp:TextBox ID="txtDescricaoCentroCusto" runat="server" Height="42px" 
                Width="253px"></asp:TextBox>
            <br />
            <br />
            <asp:Button ID="btnSalvar" runat="server" Text="Salvar" Width="72px" 
                onclick="btnSalvar_Click" />
            <asp:Button ID="btnExcluir" runat="server" Text="Excluir" 
                onclick="btnExcluir_Click" />
        </div>
    
    </div>
    </form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm2.aspx.cs" Inherits="HzWebManutencao.WebForm2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
       <asp:Panel ID="pnlpopup2" runat="server" BackColor="White"  
                    Height="376px" Width="749px" BorderStyle="Solid" 
                    BorderWidth="1px" style="display:yes">
            <asp:Label ID="Label4" runat="server" Text="Equipamento da Obra"
                        style="position:absolute; top: 5px; left: 6px; height: 20px; width: 700px; font-size:medium; text-align: center;" 
                        BackColor="White" Font-Bold="True" Font-Size="Large" 
                        ForeColor="#3333FF" Font-Names="Calibri"></asp:Label>
            <asp:Label ID="lblDescricao" runat="server" Text="Descrição do Equipamento"
                        style="position:absolute; top: 34px; left: 15px; height: 16px; width: 150px;" 
                        BackColor="White" Font-Bold="True" Font-Size= "Small" 
                        ForeColor="#3333FF" Font-Names="Calibri"></asp:Label>
            <asp:TextBox ID="TxtDescricao" runat="server" 
                        style="position:absolute; top: 50px; left: 13px; width: 335px; height: 62px; bottom: 381px;" 
                        Font-Names="Calibri" Font-Size="Small" TextMode="MultiLine"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RvfTxtDescricao" runat="server"  
                            ControlToValidate="TxtDescricao"
                            style="position:absolute; top: 399px; left: 359px; height: 22px; width: 157px; "
                            ErrorMessage="Descrição do equipamento em branco!" 
                            Display="None"></asp:RequiredFieldValidator>
            <asp:Label ID="lblCodigo" runat="server" Text="Código do Equipamento"
                        style="position:absolute; top: 34px; left: 360px; height: 17px; width: 135px;" 
                        BackColor="White" Font-Bold="True" Font-Size= "Small" 
                        ForeColor="#3333FF" Font-Names="Calibri"></asp:Label>
           <asp:TextBox ID="txtCodigoEquip" runat="server" Font-Names="Calibri" Font-Size="Small" 
                        style="position:absolute; top: 50px; left: 360px; width: 189px; height: 15px;"></asp:TextBox>
            <asp:Label ID="lblTipoEquip0" runat="server" Text="Tipo Equipamento"
                        style="position:absolute; top: 34px; left: 559px; height: 15px; width: 113px;" 
                        BackColor="White" Font-Bold="True" Font-Size="Small" 
                        ForeColor="#3333FF" Font-Names="Calibri"></asp:Label>
             <asp:TextBox ID="txtTpEquipamento" runat="server" 
                        style="position:absolute; top: 50px; left: 561px; width: 170px; height: 15px;" 
                        Font-Names="Calibri" Font-Size="Small"></asp:TextBox>
            <asp:Label ID="lblCapacidade" runat="server" Text="Capacidade"
                        style="position:absolute; top: 80px; left: 360px; height: 16px; width: 68px;" 
                        BackColor="White" Font-Bold="True" Font-Size= "Small" 
                        ForeColor="#3333FF" Font-Names="Calibri"></asp:Label>
            <asp:TextBox ID="txtCapacidade" runat="server"
                        style="position:absolute; top: 96px; left: 360px; width: 78px; height: 15px;" 
                        Font-Names="Calibri" Font-Size="Small"></asp:TextBox>
            <asp:Label ID="lblTipoCapacidade" runat="server" Text="Tipo Capacidade"
                        style="position:absolute; top: 80px; left: 450px; height: 17px; width: 106px;" 
                        BackColor="White" Font-Bold="True" Font-Size= "Small" 
                        ForeColor="#3333FF" Font-Names="Calibri"></asp:Label>
            <asp:DropDownList ID="cmbTipoCapacidade" runat="server" Font-Names="Calibri" Font-Size="Small"
                        style="position:absolute; top: 96px; left: 450px; width: 102px; height: 20px;"></asp:DropDownList>
            <asp:Label ID="lblMarca" runat="server" Text="Marca / Modelo"
                        style="position:absolute; top: 80px; left: 562px; height: 17px; width: 98px; right: 272px;" 
                        BackColor="White" Font-Bold="True" Font-Size="Small" 
                        ForeColor="#3333FF" Font-Names="Calibri"></asp:Label>
            <asp:TextBox ID="txtMarcaModelo" runat="server"
                        style="position:absolute; top: 96px; left: 559px; width: 173px; height: 15px;" 
                        Font-Names="Calibri" Font-Size="Small"></asp:TextBox>
            <asp:Label ID="lblNumeroSerie" runat="server" Text="Número de Série"
                        style="position:absolute; top: 129px; left: 360px; height: 17px; width: 98px; right: 470px;" 
                        BackColor="White" Font-Bold="True" Font-Size="Small" 
                        ForeColor="#3333FF" Font-Names="Calibri"></asp:Label>
            <asp:TextBox ID="txtNumeroSerie" runat="server"
                        style="position:absolute; top: 145px; left: 360px; width: 189px; height: 15px;" 
                        Font-Names="Calibri" Font-Size="Small"></asp:TextBox>
            <asp:Label ID="lblNumeroPatrimonio" runat="server" Text="Número do Patrimônio"
                        style="position:absolute; top: 129px; left: 562px; height: 15px; width: 130px; right: 240px;" 
                        BackColor="White" Font-Bold="True" Font-Size="Small" 
                        ForeColor="#3333FF" Font-Names="Calibri"></asp:Label>
            <asp:TextBox ID="txtNumeroPatrimonio" runat="server"
                        style="position:absolute; top: 145px; left: 561px; width: 169px; height: 15px;" 
                        Font-Names="Calibri" Font-Size="Small"></asp:TextBox>
           <asp:Label ID="lblObservacao" runat="server" Text="QR CODE"
                        style="position:absolute; top: 222px; left: 19px; height: 14px; width: 150px;" 
                        BackColor="White" Font-Bold="True" Font-Size= "Small" 
                        ForeColor="#3333FF" Font-Names="Calibri"></asp:Label>
            <asp:TextBox ID="txtObservacao" runat="server" 
                        style="position:absolute; top: 130px; left: 13px; width: 335px; height: 81px;" 
                        Font-Names="Calibri" Font-Size="Small" TextMode="MultiLine"></asp:TextBox>
            <asp:Label ID="lblPavimento" runat="server" Text="Pavimento"
                        style="position:absolute; top: 172px; left: 360px; height: 17px; width: 98px;" 
                        BackColor="White" Font-Bold="True" Font-Size= "Small" 
                        ForeColor="#3333FF" Font-Names="Calibri"></asp:Label>
            <asp:DropDownList ID="cmbPavimento" runat="server" Font-Names="Calibri" Font-Size="Small"
                        style="position:absolute; top: 188px; left:360px; width: 229px; height: 20px;"></asp:DropDownList>
            <asp:Label ID="lblLocalizacao" runat="server" Text="Localização"
                        style="position:absolute; top: 220px; left: 360px; height: 16px; width: 111px;" 
                        BackColor="White" Font-Bold="True" Font-Size= "Small" 
                        ForeColor="#3333FF" Font-Names="Calibri"></asp:Label>
            <asp:TextBox ID="txtLocalizacao" runat="server"
                        style="position:absolute; top: 238px; left: 360px; width: 365px; height: 18px;" 
                        Font-Names="Calibri" Font-Size="Small"></asp:TextBox>

            <asp:Label ID="Label5" runat="server" Text="TAG"
                        style="position:absolute; top: 264px; left: 360px; height: 16px; width: 111px;" 
                        BackColor="White" Font-Bold="True" Font-Size= "Small" 
                        ForeColor="#3333FF" Font-Names="Calibri"></asp:Label>
            <asp:TextBox ID="txtTag" runat="server"
                        style="position:absolute; top: 285px; left: 360px; width: 365px; height: 18px;" 
                        Font-Names="Calibri" Font-Size="Small"></asp:TextBox>

            <asp:Button ID="btnGerar" runat="server" CommandName="Gerar" 
                onclick="btnUpdate_Click" 
                style="position:absolute; top: 301px; left: 171px; width: 70px; height: 30px;" 
                Text="Gerar" />
            <asp:Image ID="Image1" runat="server" 
            
                
                style="position:absolute; top: 245px; left: 20px; height: 130px; width: 135px;" 
                BorderStyle="Double" ImageUrl="~/QRCODE/Orion.Jpeg"/>
            <asp:Label ID="lblObservacao0" runat="server" BackColor="White" 
                Font-Bold="True" Font-Names="Calibri" Font-Size="Small" ForeColor="#3333FF" 
                style="position:absolute; top: 115px; left: 14px; height: 14px; width: 150px;" 
                Text="Observação"></asp:Label>

            <asp:Button ID="btnGerarQRCode" runat="server" onclick="btnGerarQRCode_Click" 
             style="position:absolute; top: 246px; left: 171px; width: 137px; height: 30px;" 
                Text="Gerar QR Code" />
        </asp:Panel>
    </form>
</body>
</html>

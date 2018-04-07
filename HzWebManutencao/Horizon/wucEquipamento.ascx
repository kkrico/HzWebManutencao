<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="wucEquipamento.ascx.cs" Inherits="HzWebManutencao.Horizon.wucEquipamento" %>

<asp:Panel ID="Panel1" runat="server" Height="16px" Width="790px">
</asp:Panel>
      <asp:Panel ID="pnlpopup2" runat="server" BackColor="White"  
                    Height="373px" Width="790px" BorderStyle="Solid" 
                    BorderWidth="1px" style="display:true">
            <asp:Label ID="Label4" runat="server" Text="Equipamento da Obra"
                        style="position:absolute; top: 42px; left: 5px; height: 20px; width: 700px; font-size:medium; text-align: center;" 
                        BackColor="White" Font-Bold="True" Font-Size="Large" 
                        ForeColor="#3333FF" Font-Names="Calibri"></asp:Label>
            <asp:Label ID="lblDescricao" runat="server" Text="Descrição do Equipamento"
                        style="position:absolute; top: 90px; left: 28px; height: 16px; width: 150px;" 
                        BackColor="White" Font-Bold="True" Font-Size= "Small" 
                        ForeColor="#3333FF" Font-Names="Calibri"></asp:Label>
            <asp:TextBox ID="TxtDescricao" runat="server" 
                        style="position:absolute; top: 115px; left: 20px; width: 335px; height: 62px; bottom: 369px;" 
                        Font-Names="Calibri" Font-Size="Small" TextMode="MultiLine"></asp:TextBox>
            <asp:Label ID="lblCodigo" runat="server" Text="Código do Equipamento"
                        style="position:absolute; top: 73px; left: 377px; height: 17px; width: 135px;" 
                        BackColor="White" Font-Bold="True" Font-Size= "Small" 
                        ForeColor="#3333FF" Font-Names="Calibri"></asp:Label>
           <asp:TextBox ID="txtCodigoEquip" runat="server" Font-Names="Calibri" Font-Size="Small" 
                        
                style="position:absolute; top: 100px; left: 377px; width: 189px; height: 15px;"></asp:TextBox>
            <asp:Label ID="lblTipoEquip0" runat="server" Text="Tipo Equipamento"
                        style="position:absolute; top: 74px; left: 598px; height: 15px; width: 113px;" 
                        BackColor="White" Font-Bold="True" Font-Size="Small" 
                        ForeColor="#3333FF" Font-Names="Calibri"></asp:Label>
             <asp:TextBox ID="txtTpEquipamento" runat="server" 
                        style="position:absolute; top: 99px; left: 594px; width: 170px; height: 15px;" 
                        Font-Names="Calibri" Font-Size="Small"></asp:TextBox>
            <asp:Label ID="lblCapacidade" runat="server" Text="Capacidade"
                        style="position:absolute; top: 126px; left: 379px; height: 16px; width: 68px;" 
                        BackColor="White" Font-Bold="True" Font-Size= "Small" 
                        ForeColor="#3333FF" Font-Names="Calibri"></asp:Label>
            <asp:TextBox ID="txtCapacidade" runat="server"
                        style="position:absolute; top: 152px; left: 373px; width: 78px; height: 15px;" 
                        Font-Names="Calibri" Font-Size="Small"></asp:TextBox>
            <asp:Label ID="lblTipoCapacidade" runat="server" Text="Tipo Capacidade"
                        style="position:absolute; top: 127px; left: 476px; height: 17px; width: 106px;" 
                        BackColor="White" Font-Bold="True" Font-Size= "Small" 
                        ForeColor="#3333FF" Font-Names="Calibri"></asp:Label>
            <asp:DropDownList ID="cmbTipoCapacidade" runat="server" Font-Names="Calibri" Font-Size="Small"
                        
                style="position:absolute; top: 151px; left: 471px; width: 102px; height: 20px;"></asp:DropDownList>
            <asp:Label ID="lblMarca" runat="server" Text="Marca / Modelo"
                        style="position:absolute; top: 125px; left: 581px; height: 17px; width: 98px; right: 620px;" 
                        BackColor="White" Font-Bold="True" Font-Size="Small" 
                        ForeColor="#3333FF" Font-Names="Calibri"></asp:Label>
            <asp:TextBox ID="txtMarcaModelo" runat="server"
                        style="position:absolute; top: 156px; left: 574px; width: 173px; height: 15px;" 
                        Font-Names="Calibri" Font-Size="Small"></asp:TextBox>
            <asp:Label ID="lblNumeroSerie" runat="server" Text="Número de Série"
                        style="position:absolute; top: 175px; left: 367px; height: 17px; width: 98px; right: 834px;" 
                        BackColor="White" Font-Bold="True" Font-Size="Small" 
                        ForeColor="#3333FF" Font-Names="Calibri"></asp:Label>
            <asp:TextBox ID="txtNumeroSerie" runat="server"
                        style="position:absolute; top: 199px; left: 362px; width: 189px; height: 15px;" 
                        Font-Names="Calibri" Font-Size="Small"></asp:TextBox>
            <asp:Label ID="lblNumeroPatrimonio" runat="server" Text="Número do Patrimônio"
                        style="position:absolute; top: 176px; left: 574px; height: 15px; width: 130px; right: 595px;" 
                        BackColor="White" Font-Bold="True" Font-Size="Small" 
                        ForeColor="#3333FF" Font-Names="Calibri"></asp:Label>
            <asp:TextBox ID="txtNumeroPatrimonio" runat="server"
                        style="position:absolute; top: 197px; left: 570px; width: 169px; height: 15px;" 
                        Font-Names="Calibri" Font-Size="Small"></asp:TextBox>
           <asp:Label ID="lblObservacao" runat="server" Text="Observação"
                        style="position:absolute; top: 194px; left: 19px; height: 14px; width: 150px;" 
                        BackColor="White" Font-Bold="True" Font-Size= "Small" 
                        ForeColor="#3333FF" Font-Names="Calibri"></asp:Label>
            <asp:Label ID="lblPavimento" runat="server" Text="Pavimento"
                        style="position:absolute; top: 223px; left: 368px; height: 17px; width: 98px;" 
                        BackColor="White" Font-Bold="True" Font-Size= "Small" 
                        ForeColor="#3333FF" Font-Names="Calibri"></asp:Label>
            <asp:DropDownList ID="cmbPavimento" runat="server" Font-Names="Calibri" Font-Size="Small"
                        
                style="position:absolute; top: 253px; left:363px; width: 229px; height: 20px;"></asp:DropDownList>
            <asp:Label ID="lblLocalizacao" runat="server" Text="Localização"
                        style="position:absolute; top: 297px; left: 367px; height: 16px; width: 111px;" 
                        BackColor="White" Font-Bold="True" Font-Size= "Small" 
                        ForeColor="#3333FF" Font-Names="Calibri"></asp:Label>
            <asp:TextBox ID="txtLocalizacao" runat="server"
                        style="position:absolute; top: 324px; left: 365px; width: 365px; height: 18px;" 
                        Font-Names="Calibri" Font-Size="Small"></asp:TextBox>

            <asp:Button ID="btnUpdate" CommandName="Update" runat="server" Text="Gravar" 
                        style="position:absolute; top: 364px; left: 21px; width: 70px; height: 30px;" 
                        onclick="btnUpdate_Click"/>
            <asp:Button ID="btnCancel" runat="server" Text="Cancelar" CausesValidation="false"
                        style="position:absolute; top: 366px; left: 104px; height: 30px; right: 1118px;" 
                        onclick="btnCancel_Click" />
            <asp:TextBox ID="txtObservacao" runat="server" Font-Names="Calibri" 
                Font-Size="Small" 
                style="position:absolute; top: 216px; left: 17px; width: 335px; height: 127px;" 
                TextMode="MultiLine"></asp:TextBox>
        </asp:Panel>
<p>
    &nbsp;</p>
<p>
                        <asp:RequiredFieldValidator ID="RvfTxtDescricao" runat="server"  
                            ControlToValidate="TxtDescricao"
                            style="position:absolute; top: 399px; left: 359px; height: 22px; width: 157px; "
                            ErrorMessage="Descrição do equipamento em branco!" 
                            Display="None"></asp:RequiredFieldValidator>
            </p>

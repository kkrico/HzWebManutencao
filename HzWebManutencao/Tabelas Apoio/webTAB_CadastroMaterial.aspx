<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="webTAB_CadastroMaterial.aspx.cs" Inherits="HzWebManutencao.Configuracao.webCON_CadastroMaterial" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div  style="position:relative; height: 499px; width: 932px; top: 6px; left: -54px; margin-left: 40px;">
        <asp:Panel ID="pnlCadastraMaterial" runat="server" 
            style="position:absolute; left:10px; width:914px; height:250px; top: 7px;">
            <asp:Label ID="lblAcao" runat="server" Text="Cadastro de Material"
                style="position:absolute; top: 0px; left: 11px; height: 20px; width: 672px;" 
                BackColor="White" Font-Bold="True" Font-Size="Medium" ForeColor="Maroon">
            </asp:Label>
            <asp:Label ID="lblMaterial" runat="server" Text="Material: "
                style="position:absolute; top: 23px; left: 23px; height: 25px; width: 40px;">
            </asp:Label>
            <asp:TextBox ID="txtItemMaterial" runat="server"
                
                style="position:absolute; top: 41px; left: 20px; height: 25px; width: 731px;">
            </asp:TextBox>
            <asp:Button ID="btnPesqMaterial" runat="server" Text="Pesquisar Material" 
                style="position:absolute; top: 37px; left: 756px; height: 30px; width: 127px;" 
                CausesValidation="false" onclick="btnPesqMaterial_Click"/>
            <asp:ListBox ID="LstMaterial" runat="server"
                style="position:absolute; top: 68px; left: 18px; width: 861px; height: 170px;" 
                Visible="true" AutoPostBack="True" 
                onselectedindexchanged="LstMaterial_SelectedIndexChanged" >
            </asp:ListBox>
         </asp:Panel>

        <asp:Panel ID="pnlDadosMaterial" runat="server" BackColor="White" 
            BorderStyle="Solid" BorderWidth="1px"
            style="position:absolute; left:20px; width:871px; height:210px; top: 271px;">
            <asp:Label ID="Label3" runat="server" Text="Dados do Material"
                style="position:absolute; top: 3px; left: 10px; height: 20px; width: 250px;"
                BackColor="White" Font-Bold="True" Font-Size="Medium" ForeColor="Maroon"></asp:Label>
            <asp:Label ID="lblItMaterial" runat="server" 
                style="position:absolute; top: 28px; left: 16px; height: 20px; width: 121px;" 
                Text="Item do Material"></asp:Label>
            <asp:TextBox ID="txtItMaterial" runat="server" enabled = "false"
                style="position:absolute; top: 48px; left: 16px; width: 117px; height: 21px;"></asp:TextBox>
            <asp:DropDownList ID="cmbUnidade" runat="server" 
                
                style="position:absolute; top: 47px; left: 575px; width: 120px; height: 27px;">
            </asp:DropDownList>
            <asp:Label ID="lblValor" runat="server" Text="Preço Unitário: "
                style="position:absolute; top: 27px; left: 701px; height: 17px; width: 143px;">
            </asp:Label>
            <asp:TextBox ID="txtPrecoMaterial" runat="server" 
                style="position:absolute; top: 47px; left: 703px; height: 21px; width: 150px; text-align:right"></asp:TextBox>
            <asp:Label ID="Label4" runat="server" 
                style="position:absolute; top: 75px; left: 18px; height: 20px; width: 143px;" 
                Text="Descrição do Material"></asp:Label>
            <asp:TextBox ID="txtDcMaterial" runat="server" 
                style="position:absolute; top: 96px; left: 18px; height: 58px; width: 832px;" 
                TextMode="MultiLine"></asp:TextBox>
            <asp:Label ID="lblTipo" runat="server" 
                style="position:absolute; top: 27px; left: 144px; height: 20px; width: 143px;" 
                Text="Tipo"></asp:Label>
            <asp:DropDownList ID="cmbTipo" runat="server" 
                style="position:absolute; top: 48px; left: 141px; width: 430px; height: 27px;">
            </asp:DropDownList>
           <asp:Button ID="btnNovo" CommandName="Update" runat="server" Text="Novo"
                style="position:absolute; top: 177px; left: 18px; width: 70px; height: 30px;" 
                CausesValidation="false" onclick="btnNovo_Click"/>
            <asp:Button ID="btnGravar" runat="server" CommandName="Update" 
                style="position:absolute; top: 177px; left: 94px; width: 70px; height: 30px; right: 630px;" 
                Text="Gravar" onclick="btnGravar_Click" />
            <asp:Button ID="btnExcluir" runat="server" 
                style="position:absolute; top: 177px; left: 171px; width: 70px; height: 30px; right: 553px;" 
                Text="Excluir" onclick="btnExcluir_Click" />
            <asp:Label ID="lblUnidade" runat="server" 
                style="position:absolute; top: 27px; left: 575px; height: 20px; width: 100px;" 
                Text="Unidade"></asp:Label>
        </asp:Panel>

        <asp:RequiredFieldValidator ID="RvfItem" runat="server" 
            style="position:absolute; top: 428px; left: 492px; width: 200px; height: 26px;" 
            ErrorMessage="Item do material em branco!"
            ControlToValidate="txtItMaterial" Display="None"></asp:RequiredFieldValidator>
        <asp:CompareValidator ID="cbUnidade" runat="server" 
            ControlToValidate="cmbUnidade" Display="None" ErrorMessage="É obrigatório o preenchimento do campo Unidade do Material!" 
            style="position:absolute; top: 411px; left: 715px; width: 123px; height: 16px;" 
            Operator="NotEqual" ValueToCompare="0" ></asp:CompareValidator>
         <asp:CompareValidator ID="CvfTipo" runat="server" 
            ControlToValidate="cmbTipo" Display="None" ErrorMessage="É obrigatório o preenchimento do campo Tipo!" 
            style="position:absolute; top: 394px; left: 718px; width: 123px; height: 16px;" 
            Operator="NotEqual" ValueToCompare="0" ></asp:CompareValidator>
        <asp:RequiredFieldValidator ID="RvfDescricao" runat="server" 
            style="position:absolute; top: 443px; width: 200px; height: 26px;left: 492px;" 
            ErrorMessage="Descrição do material em branco!"
            ControlToValidate="txtDcMaterial" Display="None"></asp:RequiredFieldValidator>
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
        style="position:absolute; top: 433px; left: 688px; width: 200px; height: 36px;"
        ShowMessageBox="True" ShowSummary="False" />

    </div>

</asp:Content>

<%@ Page Title="" MaintainScrollPositionOnPostback="true" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="True" CodeBehind="webTAB_VincularMaterialObra.aspx.cs" Inherits="HzWebManutencao.Tabelas_Apoio.webTAB_VincularMaterialObra" %>
<%@ Register src="~/Controles/Mensagem.ascx" tagname="Mensagem" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Mensagem ID="CaixaMensagem" runat="server" />
    <div  style="position:relative; height: 435px; width: 1147px; top: 12px; left: -54px; margin-left: 80px;">
        <asp:Label ID="lblObra" runat="server" Text="Obra:" 
            style="position:absolute; top: 4px; left: 10px; height: 17px; width: 44px;" 
            Font-Names="Calibri" Font-Size="Small"></asp:Label>
            
         <asp:DropDownList ID="cmbObra" runat="server" 
            style="position:absolute; top: 2px; left: 48px; width: 904px;"
            AutoPostBack="True" onselectedindexchanged="cmbObra_SelectedIndexChanged" 
            Font-Names="Calibri" Font-Size="Small" >
        </asp:DropDownList>

        <asp:RadioButtonList ID="rbdTipo" runat="server" 
            
            
            
            
            style="position:absolute; top: 21px; left: 4px; width: 245px; height: 13px;" AutoPostBack="True" 
            RepeatColumns="3" 
            RepeatDirection="Horizontal" Font-Bold="True" Font-Names="Calibri">
            <asp:ListItem Value="0">Material</asp:ListItem>
            <asp:ListItem Value="1">Serviço</asp:ListItem>
            <asp:ListItem Value="2" Selected="True">Todos</asp:ListItem>
        </asp:RadioButtonList>

        <asp:Label ID="lblMaterial" runat="server" Text="Material da Obra" 
            style="position:absolute; top: 50px; left: 605px; height: 19px; width: 111px;" 
            Font-Names="Calibri" Font-Size="Small" Font-Bold="True"></asp:Label>
         <asp:ListBox ID="lstMaterial1" runat="server"
             style="position:absolute; top: 68px; left: 603px; width: 517px; height: 107px;" 
             SelectionMode="Multiple" Font-Names="Calibri" Font-Size="X-Small" 
                AutoPostBack    ="True" 
                onselectedindexchanged="lstMaterial1_SelectedIndexChanged1">
         </asp:ListBox>

        <asp:Panel ID="pnlMatObra" runat="server" BackColor="#FFFFCC" 
            BorderStyle="Solid" BorderWidth="1px" 
            
            style="position:absolute; Height:231px; Width:509px; Top:182px; left:607px;">

        <asp:Label ID="lblItMaterial1" runat="server" 
            style="position:absolute; top: 5px; left: 10px; height: 16px; width: 32px;" 
            Text="Item:" Font-Names="Calibri" Font-Size="Small">
        </asp:Label>
        <asp:TextBox ID="txtItemMaterial2" runat="server" 
            style="position:absolute; top: 3px; left: 45px; width: 69px; height: 12px;" 
            Font-Size="X-Small" ontextchanged="txtItemMaterial2_TextChanged">
        </asp:TextBox>

        <asp:Label ID="lblItMaterial5" runat="server" 
            style="position:absolute; top: 5px; left: 130px; height: 13px; width: 35px;" 
            Text="Tipo:" Font-Names="Calibri" Font-Size="Small"></asp:Label>
        <asp:TextBox ID="txtTipo2" runat="server"
            style="position:absolute; top: 3px; left: 164px; width: 337px; height: 12px;" 
            Font-Size="X-Small" Enabled="False" ForeColor="RoyalBlue" 
                ontextchanged="txtTipo2_TextChanged"            >
        </asp:TextBox>

        <asp:Label ID="lblItMaterial6" runat="server" 
            style="position:absolute; top: 27px; left: 10px; height: 15px; width: 56px;" 
            Text="Unidade:" Font-Names="Calibri" Font-Size="Small">
        </asp:Label>
        <asp:TextBox ID="txtUnidade2" runat="server"
            style="position:absolute; top: 25px; left: 73px; width: 38px; height: 12px;" 
            Font-Size="X-Small" Enabled="False">
        </asp:TextBox>

        <asp:Label ID="lblValor0" runat="server" Text="Preço Unitário: "
            style="position:absolute; top: 27px; left: 130px; height: 17px; width: 85px;" 
            Font-Names="Calibri" Font-Size="Small">
        </asp:Label>
        <asp:TextBox ID="txtPrecoMaterial2" runat="server" 
            style="position:absolute; top: 25px; left: 220px; height: 12px; width: 123px; text-align:right" 
            Font-Size="X-Small">
        </asp:TextBox>

        <asp:Label ID="lblSerMat2" runat="server" Text="Material"
            style="position:absolute; top: 27px; left: 362px; height: 17px; width: 74px;" 
            Font-Names="Calibri" Font-Size="Small" Font-Bold="True">
        </asp:Label>

       <asp:Label ID="lblValor1" runat="server" Text="Qtd. Contrato"
            style="position:absolute; top: 48px; left: 10px; height: 17px; width: 77px;" 
            Font-Names="Calibri" Font-Size="Small">
       </asp:Label>
       <asp:TextBox ID="txtQtContrato" runat="server" 
            style="position:absolute; top: 63px; left: 12px; height: 11px; width: 75px; text-align:right; right: 421px;" 
            Font-Size="X-Small">
       </asp:TextBox>

        <asp:Label ID="Label1" runat="server" Text="Qtd. Utilizada Agrupada"
            style="position:absolute; top: 48px; left: 98px; height: 17px; width: 138px;" 
            Font-Names="Calibri" Font-Size="Small">
        </asp:Label>
        <asp:TextBox ID="txtQtUtilizadaAgrupada" runat="server" 
            style="position:absolute; top: 63px; left: 98px; height: 12px; width: 134px; text-align:right" 
            Font-Size="X-Small">
        </asp:TextBox>

        <asp:Label ID="Label2" runat="server" Text="Qtd. Estoque"
            style="position:absolute; top: 48px; left: 244px; height: 17px; width: 77px;" 
            Font-Names="Calibri" Font-Size="Small">
        </asp:Label>
        <asp:TextBox ID="txtQtEstoque" runat="server" 
            style="position:absolute; top: 63px; left: 244px; height: 12px; width: 77px; text-align:right" 
            Font-Size="X-Small">
        </asp:TextBox>

        <asp:Label ID="lblValor2" runat="server" Text="Qtd. Utilizada Obra"
            style="position:absolute; top: 48px; left: 332px; height: 17px; width: 113px;" 
            Font-Names="Calibri" Font-Size="Small">
        </asp:Label>
        <asp:TextBox ID="txtQtUtilizadaObra" runat="server" 
            style="position:absolute; top: 63px; left: 332px; height: 12px; width: 111px; text-align:right; bottom: 135px;" 
            Font-Size="X-Small">
        </asp:TextBox>

        <asp:Label ID="lblItMaterial" runat="server" 
            style="position:absolute; top: 85px; left: 10px; height: 13px; width: 154px;" 
            Text="Descrição do Material" Font-Names="Calibri" Font-Size="Small">
        </asp:Label>
        <asp:TextBox ID="txtDescricao2" runat="server"
            style="position:absolute; top: 101px; left: 10px; width: 468px; height: 92px;" 
            Font-Size="X-Small" TextMode="MultiLine">
        </asp:TextBox>

        <asp:Button ID="btnSalvar" runat="server" Text="Gravar" 
            style="position:absolute; top: 201px; left: 10px; height: 24px; width: 63px; right: 436px;" 
            Font-Bold="True" Font-Names="Calibri" Font-Size="Small" 
            onclick="btnSalvar_Click" />
     </asp:Panel>

     <asp:ListBox ID="lstMaterial0" runat="server" 
         style="position:absolute; top: 68px; left: 6px; width: 517px; height: 107px;" 
         SelectionMode="Multiple" Font-Names="Calibri" Font-Size="X-Small" 
            onselectedindexchanged="lstMaterial0_SelectedIndexChanged" 
            AutoPostBack="True">
     </asp:ListBox>

    <asp:Button ID="btnVincular" runat="server" Text=">>" 
         style="position:absolute; top: 86px; left: 539px; width: 49px; height: 25px; bottom: 324px;" 
         onclick="btnVincular_Click" CausesValidation="False" Font-Bold="True" 
            Font-Names="Calibri" Font-Size="Medium"/>

    <asp:Button ID="btnDesvincular" runat="server" Text="&lt;&lt;" 
         style="position:absolute; top: 126px; left: 539px; width: 49px; height: 25px; bottom: 284px;" 
          CausesValidation="False" Font-Bold="True" 
            Font-Names="Calibri" Font-Size="Medium" Font-Italic="False" 
            onclick="btnDesvincular_Click"/>
 
            <asp:Label ID="lblValor" runat="server" Text="Preço Unitário"
            style="position:absolute; top: 223px; left: 99px; height: 17px; width: 92px;" 
            Font-Names="Calibri" Font-Size="Small"></asp:Label>

            <asp:Label ID="lblItMaterial0" runat="server" 
                style="position:absolute; top: 185px; left: 99px; height: 13px; width: 35px;" 
                Text="Tipo" Font-Names="Calibri" Font-Size="Small"></asp:Label>
                 
            <asp:Label ID="lblItMaterial2" runat="server" 
                style="position:absolute; top: 223px; left: 12px; height: 15px; width: 56px;" 
                Text="Unidade" Font-Names="Calibri" Font-Size="Small"></asp:Label>


           <asp:Label ID="lblItMaterial3" runat="server" 
                style="position:absolute; top: 185px; left: 13px; height: 16px; width: 72px;" 
                Text="Item" Font-Names="Calibri" Font-Size="Small"></asp:Label>
            <asp:TextBox ID="txtItemMaterial1" runat="server" enabled = "false"
            style="position:absolute; top: 202px; left: 12px; width: 77px; height: 12px;" 
            Font-Size="X-Small"></asp:TextBox>




            <asp:TextBox ID="txtPrecoMaterial1" runat="server" enabled = "false"
            style="position:absolute; top: 241px; left: 98px; height: 12px; width: 128px; text-align:right" 
            Font-Size="X-Small"></asp:TextBox>

            <asp:Label ID="lblItMaterial4" runat="server" 
                style="position:absolute; top: 277px; left: 13px; height: 15px; width: 155px;" 
                Text="Descrição do Material" Font-Names="Calibri" Font-Size="Small"></asp:Label>
            <asp:TextBox ID="txtDescricao1" runat="server" enabled = "false"
            style="position:absolute; top: 292px; left: 11px; width: 509px; height: 105px;" 
            Font-Size="X-Small" TextMode="MultiLine"></asp:TextBox>

        <asp:Label ID="lblMaterial0" runat="server" Text="Material:" 
            style="position:absolute; top: 28px; left: 261px; height: 17px; width: 59px;" 
            Font-Names="Calibri" Font-Size="Small"></asp:Label>
        <asp:Label ID="lblMaterial1" runat="server" Text="Material Referência" 
            style="position:absolute; top: 50px; left: 9px; height: 17px; width: 151px; right: 822px;" 
            Font-Names="Calibri" Font-Size="Small" Font-Bold="True"></asp:Label>

 





 



            <asp:Label ID="lblSerMat1" runat="server" Text="Material"
            style="position:absolute; top: 241px; left: 241px; height: 17px; width: 107px; text-align: left;" 
            Font-Names="Calibri" Font-Size="Small" Font-Bold="True"></asp:Label>





 
            <asp:TextBox ID="txtTipo1" runat="server" enabled = "false"
            style="position:absolute; top: 202px; left: 98px; width: 336px; height: 12px;" 
            Font-Size="X-Small"></asp:TextBox>

 
            <asp:TextBox ID="txtUnidade1" runat="server" enabled = "false"
            style="position:absolute; top: 241px; left: 12px; width: 76px; height: 12px;" 
            Font-Size="X-Small"></asp:TextBox>

        <asp:RequiredFieldValidator ID="rfvItemMaterial" runat="server" 
                ControlToValidate="txtItemMaterial2" Display="None" ErrorMessage="É obrigatório o preenchimento do campo item!"
            
            
            
            style="position:absolute; top: 400px; left: 886px; width: 94px; height: 16px;"></asp:RequiredFieldValidator>

        <asp:RequiredFieldValidator ID="rfvDescricaoMaterial" runat="server" 
                ControlToValidate="txtDescricao2" Display="None" ErrorMessage="É obrigatório o preenchimento do campo descrição do Material!"
                
            
            
            style="position:absolute; top: 400px; left: 756px; width: 94px; height: 16px;"></asp:RequiredFieldValidator>
        <asp:ValidationSummary ID="ValidationSummary1" runat="server"
            style="position:absolute; top: 385px; left: 964px; width: 162px; height: 33px;" 
            ShowMessageBox="True" ShowSummary="False" />


        <asp:Button ID="btnPesquisar" runat="server" Text="Pesquisar" CausesValidation = "false"
            style="position:absolute; top: 26px; left: 881px; height: 24px; width: 69px; right: 197px;" 
            Font-Bold="True" Font-Names="Calibri" Font-Size="Small" 
            onclick="btnPesquisar_Click" />



        <asp:TextBox ID="txtMaterial" runat="server"
            style="position:absolute; top: 25px; left: 317px; height: 15px; width: 534px; right: 296px;" 
            Font-Names="Calibri" Font-Size="Small"></asp:TextBox>

</div>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="webRelManutencaoPreventiva.aspx.cs" Inherits="HzWebManutencao.Relatorios.webRelManutencaoPreventiva" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style2
        {
            width: 232px;
        }
        .style3
        {
            height: 24px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">


    <table class="style1">
        <tr>
            <td class="style2">
                FILTROS</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style2">
                <table class="style1">
                    <tr>
                        <td>
                            <asp:RadioButton ID="rblNumPreventPeriodo" runat="server" AutoPostBack="True" 
                                Checked="True" oncheckedchanged="rblNumPreventPeriodo_CheckedChanged" 
                                Text="Número Preventiva e Período" />
                        </td>
                    </tr>
                    <tr>
                        <td class="style3">
                            <asp:RadioButton ID="rblObraPeriodo" runat="server" AutoPostBack="True" 
                                oncheckedchanged="rblObraPeriodo_CheckedChanged" Text="Obra e Período" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                    <asp:Button ID="btnImprimir" runat="server" Height="26px" 
        onclick="btnImprimir_Click" Text="Imprimir" Width="83px" />
    
                        </td>
                    </tr>
                </table>
    
            </td>
            <td>

                <asp:MultiView ID="MultiView1" runat="server">

                    <asp:View ID="View2" runat="server">
                                    Obra: <asp:DropDownList ID="cmbObra" runat="server" Height="16px" Width="329px" 
                                        onselectedindexchanged="cmbObra_SelectedIndexChanged">
                                       
                </asp:DropDownList>
                                    <br />
                <br />
                        Data Inicial: <asp:TextBox ID="TxtdtInicialObra" runat="server"></asp:TextBox>
    
                                    <br />
    
    <br />
    Data Final:<asp:TextBox ID="txtDtFinalObra" runat="server"></asp:TextBox>
                                    <br />
                    </asp:View>
                    <asp:View ID="View1" runat="server">
                        <asp:Label ID="Label1" runat="server" Text="Numero da Preventiva:"></asp:Label>

    <asp:TextBox ID="txtNumPreventiva" runat="server"></asp:TextBox>
    <br />
    <br />
    Data Inicial :<asp:TextBox ID="txtDataInicial" runat="server"></asp:TextBox>
    <br />
    <br />
    Data Final:<asp:TextBox ID="txtDataFinal" runat="server"></asp:TextBox>
    <br />
                    </asp:View>
                </asp:MultiView>
            </td>
        </tr>
    </table>
    <br />
    <br />
    <br />
    <br />
    <br />

</asp:Content>

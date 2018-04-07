<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="wucPavimento.ascx.cs" Inherits="HzWebManutencao.Horizon.wucPavimento" %>
<asp:Label ID="Label1" runat="server" Text="Pavimento"></asp:Label>
<br />
<asp:TextBox ID="txtPavimento" runat="server" Width="257px" Enabled="False"></asp:TextBox>
<br />
<br />
<asp:Label ID="Label2" runat="server" Text="Equipamentos"></asp:Label> &nbsp; &nbsp; &nbsp
<asp:Button ID="Button1" runat="server" Text="Novo" Width="68px" />
<p>
    
    <asp:GridView ID="grvEquipamentos" runat="server" AutoGenerateColumns="False" 
        Width="518px">
        <Columns>
            <asp:TemplateField HeaderText="Operação" ShowHeader="False">
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="false" 
                        CommandName="" Text="Editar"></asp:LinkButton>
                    &nbsp;&nbsp;
                    <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="false" 
                        CommandName="" Text="Excluir"></asp:LinkButton>
                </ItemTemplate>
                <ItemStyle Width="100px" />
            </asp:TemplateField>
            <asp:BoundField HeaderText="Descrição" DataField="cmpDcEquipamentoObra" >
            <ItemStyle Width="200px" />
            </asp:BoundField>
            <asp:BoundField HeaderText="Tipo" DataField="cmpDcTipoAtividade" >
            <ItemStyle Width="100px" />
            </asp:BoundField>
            <asp:BoundField HeaderText="Marca" DataField="cmpDcMarcaModeloEquipamento" >
            <ItemStyle Width="100px" />
            </asp:BoundField>
        </Columns>
    </asp:GridView>
</p>


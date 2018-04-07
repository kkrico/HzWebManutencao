<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="wucEquipamentosObra.ascx.cs" Inherits="HzWebManutencao.Horizon.wucEquipamentosObra" %>
            <asp:Panel ID="pnlpopup" runat="server" BackColor="White" 
    Height="405px" Width="752px" style="display:yes"
                    BorderStyle="Solid" BorderWidth="1px">
        <asp:Label ID="Label2" runat="server" Text="Equipamentos"
            BackColor="White" Font-Bold="True" Font-Size="Large" ForeColor="Maroon" 
            Font-Names="Calibri"></asp:Label>

                       <asp:Label ID="lblDescricaoEquip" runat="server" BackColor="White" Font-Bold="True" 
                        Font-Names="Calibri" Font-Size="Small" ForeColor="#3333FF" 
                        style="position:absolute; top: 34px; left: 24px; height: 20px; width: 154px;" 
                        Text="Descrição do Equipamento:"></asp:Label>
            <asp:TextBox ID="txtDescEquip" runat="server" Font-Names="Calibri" 
                        Font-Size="Small" 
                        
                    
                    style="position:absolute; top: 33px; left: 178px; height: 15px; width: 128px;"></asp:TextBox>
            <asp:Label ID="lblTipoEquip1" runat="server" BackColor="White" Font-Bold="True" 
                        Font-Names="Calibri" Font-Size="Small" ForeColor="#3333FF" 
                        style="position:absolute; top: 33px; left: 324px; height: 20px; width: 38px;" 
                        Text="Tipo :"></asp:Label>
            <asp:DropDownList ID="cmbTipoEquip" runat="server" Font-Names="Calibri" Font-Size="Small"
                        
                    style="position:absolute; top: 30px; left: 372px; width: 139px; height: 15px;" 
                    AutoPostBack="True" 
                    onselectedindexchanged="cmbTipoEquip_SelectedIndexChanged"></asp:DropDownList>

                <asp:Button ID="btnPesquisarEquipamento" runat="server" Text="Pesquisar" 
                 
                    style="position:absolute; top: 24px; left: 549px; width: 81px; height: 30px;" 
                    onclick="btnPesquisarEquipamento_Click"/>

        <asp:GridView ID="dgvEquipamento" runat="server" Height="137px" 
            AutoGenerateColumns="False" AllowPaging="True" 
            DataKeyNames="cmpIdEquipamentoObra" 
            onpageindexchanging="dgvEquipamento_PageIndexChanging" CellPadding="4" ForeColor="#333333" 
                    GridLines="None" PageSize="9"
                     style="position:absolute; left: 11px; top: 67px; width: 736px;">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:CommandField EditText="Editar" SelectText="Editar" 
                    ShowSelectButton="True" />
                <asp:BoundField HeaderText="Equipamento" DataField="cmpDcEquipamentoObra" />
                <asp:BoundField DataField="cmpDcTipoEquipamento" 
                    HeaderText="Tipo Equipamento" />
                <asp:BoundField HeaderText="Local" DataField="cmpDcLocalEquipamento" />
                <asp:BoundField DataField="cmpDcTipoAtividade" HeaderText="Atividade" />
                <asp:BoundField DataField="cmpDcTipoAtividade" HeaderText="Tipo Atividade" />
                <asp:BoundField DataField="cmpTagEquipamento" HeaderText="TAG" />
            </Columns>
            <EditRowStyle BackColor="#2461BF" />
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <PagerSettings PageButtonCount="5" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#EFF3FB" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#F5F7FB" />
            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
            <SortedDescendingCellStyle BackColor="#E9EBEF" />
            <SortedDescendingHeaderStyle BackColor="#4870BE" />
        </asp:GridView>
        <br />
        <asp:Button ID="btnCancelarEquipamento" runat="server" Text="Cancelar" CausesValidation="false"
            Height="30px" Width="75px" onclick="btnCancelarEquipamento_Click"  
                    style="position:absolute; left: 669px; top: 384px; right: 769px;"/>
             <asp:Button ID="btnNovo" runat="server" Text="Novo" CausesValidation="false"
            Height="30px" Width="75px" onclick="btnCancelarEquipamento_Click"  
                    style="position:absolute; left: 663px; top: 24px; right: 775px;"/>
                    </asp:Panel>

<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="webPREVizualizar.aspx.cs" Inherits="HzWebManutencao.Preventiva.webPREVizualizar" %>
<%@ Register Src="~/Controles/Mensagem.ascx" TagName="Mensagem" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Mensagem ID="CaixaMensagem" runat="server" />
    <div style="position:relative; height: 499px; width: 990px; top: 6px; left: -54px; margin-left: 40px;">
        <asp:Label ID="lblAcao" runat="server" Text="Vizualizar Preventivas"
            style="position:absolute; top: 4px; left: 6px; height: 27px; width: 968px; text-align: center;" 
            BackColor="White" Font-Bold="True" Font-Size="Large" ForeColor="Maroon" 
            Font-Names="Calibri"></asp:Label>

        <asp:Label ID="Label5" runat="server" Text="Obra:" 
            style="position:absolute; top: 44px; left: 12px; height: 17px; width: 44px;"> </asp:Label>
        <asp:DropDownList ID="cmbObra" runat="server" 
            style="position:absolute; top: 63px; left: 13px; width: 320px; height: 22px;"
            AutoPostBack="True" 
        onselectedindexchanged="cmbObra_SelectedIndexChanged" Font-Names="Calibri" 
            Font-Size="Small" >
        </asp:DropDownList>

        <asp:Label ID="Label4" runat="server" Text="Tipo de Atividade: " 
            style="position:absolute; left: 345px; top: 44px"></asp:Label>
        <asp:DropDownList ID="cmbTipoAtividade" runat="server" 
            style="position:absolute; top: 63px; left: 344px; width: 194px; right: 434px; height: 22px;" 
            AutoPostBack="True" 
            TabIndex="3" Font-Names="Calibri" Font-Size="Small" 
            onselectedindexchanged="cmbTipoAtividade_SelectedIndexChanged" >
        </asp:DropDownList>

        <asp:Label ID="Label1" runat="server" Text="Periodicidade: " 
            style="position:absolute; left: 549px; top: 44px"></asp:Label>
        <asp:DropDownList ID="cmbPeriodicidade" runat="server" 
            style="position:absolute; top: 63px; left: 547px; width: 148px; right: 285px; height: 22px;" 
            AutoPostBack="True" 
            TabIndex="3" Font-Names="Calibri" Font-Size="Small" 
            onselectedindexchanged="cmbPeriodicidade_SelectedIndexChanged" >
        </asp:DropDownList>

        <asp:Label ID="Label14" runat="server" Text="Tipo: " 
            style="position:absolute; left: 715px; top: 44px; height: 15px; width: 50px;"></asp:Label>
        <asp:RadioButtonList ID="rdbType" runat="server" 
                style="position:absolute; top: 61px; left: 701px; width: 253px; right: 26px; height: 20px;" 
                onselectedindexchanged="rdbType_SelectedIndexChanged" 
                RepeatDirection="Horizontal" RepeatLayout="Flow" AutoPostBack="True" 
            CellPadding="0" CellSpacing="0" TabIndex="2">
            <asp:ListItem Value="P">Pavimento</asp:ListItem>
            <asp:ListItem Value="E">Equipamento</asp:ListItem>
            <asp:ListItem Value="T" Selected="True">Todos</asp:ListItem>
        </asp:RadioButtonList>
        <asp:CheckBox ID="ckEspelhadas" runat="server"
                style="position:absolute; top: 64px; left: 914px; width: 85px; right: -19px; height: 20px;" 
                    oncheckedchanged="ckEspelhadas_CheckedChanged" Text="Espelhadas" 
            AutoPostBack="True" />
        <asp:Button ID="btnPesquisar" runat="server" Text="Pesquisar" 
                style="position:absolute; top: 64px; left: 1007px; width: 82px; height: 26px; right: -99px;" 
                onclick="btnPesquisar_Click" />

        <asp:GridView ID="grdFormPreventiva" runat="server" 
            
            style="position:absolute; top: 100px; left: 14px; width: 1150px;" AllowPaging="True" 
            AllowSorting="True" AutoGenerateColumns="False" 
            onpageindexchanging="grdFormPreventiva_PageIndexChanging" 
            ViewStateMode="Enabled" 
            BorderColor="#003366" BorderStyle="Inset" BorderWidth="1px"
            CellPadding="3" BackColor="White" Font-Size="Small" PageSize="7" 
            DataKeyNames="cmpCoPreventiva,cmpCoObraGrupoLista" 
            onrowcommand="grdFormPreventiva_RowCommand" onselectedindexchanged="grdFormPreventiva_SelectedIndexChanged" 
            >
            <Columns>
                <asp:TemplateField HeaderText="Cod. Prev." ShowHeader="False">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" 
                            CommandName="Visualizar" Text='<%# Bind("CodPrevGrupo") %>'></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="cmpDcTipoAtividade" HeaderText="Tipo Atividade">
                    <HeaderStyle Width="200px" />
                    <ItemStyle Width="100px" HorizontalAlign="Left" Font-Size="Small" />
                </asp:BoundField>
                <asp:BoundField DataField="cmpDcGrupoAtividade" HeaderText="Grupo Atividade">
                <ItemStyle Width="100px" />
                </asp:BoundField>
                <asp:BoundField DataField="cmpDcPeriodicidade" HeaderText="Periodicidade">
                    <ItemStyle Width="100px" HorizontalAlign="Left" Font-Size="Small" />
                </asp:BoundField>
                <asp:BoundField DataField="TipoManutencao" HeaderText="Tipo Manutenção">
                    <ItemStyle Width="100px" HorizontalAlign="Left" Font-Size="Small" />
                </asp:BoundField>
                <asp:BoundField DataField="cmpDcEquipamentoObra" HeaderText="Equipamento" />
                <asp:BoundField DataField="cmpDcPavimento" HeaderText="Pavimento" />
                <asp:BoundField DataField="cmpDcLocalEquipamento" HeaderText="Local" >
                <ItemStyle Width="100px" />
                </asp:BoundField>
                <asp:CommandField HeaderText="Espelhar Preventiva" SelectText="Espelhar" 
                    ShowSelectButton="True">
                <HeaderStyle Width="100px" />
                <ItemStyle HorizontalAlign="Center" Width="25px" />
                </asp:CommandField>
                <asp:CommandField DeleteText="Excluir" HeaderText="Excluir Preventiva" 
                    ShowDeleteButton="True">
                <HeaderStyle Width="100px" />
                <ItemStyle HorizontalAlign="Center" Width="25px" />
                </asp:CommandField>
            </Columns>
            <FooterStyle BackColor="White" ForeColor="#000066" />
            <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" 
                Font-Names="Calibri" Font-Size="Medium" />
            <PagerSettings PageButtonCount="9" />
            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
            <RowStyle ForeColor="#000066" Font-Names="Calibri" Font-Size="Small" />
            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F1F1F1" />
            <SortedAscendingHeaderStyle BackColor="#007DBB" />
            <SortedDescendingCellStyle BackColor="#CAC9C9" />
            <SortedDescendingHeaderStyle BackColor="#00547E" />
        </asp:GridView>
        <asp:Button ID="btnShowPopup" runat="server" style="display:none" />
                <asp:ModalPopupExtender 
                ID="ModalPopupExtender1" 
                runat="server" 
                TargetControlID="btnShowPopup" 
                PopupControlID="pnlpopup"
                BackgroundCssClass="modalBackground"></asp:ModalPopupExtender>
            <asp:Panel ID="pnlpopup" runat="server" BackColor="White" Height="390px" Width="910px" style="display:none"
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
                        style="position:absolute; top: 33px; left: 178px; height: 15px; width: 298px;"></asp:TextBox>
            <asp:Label ID="lblTipoEquip1" runat="server" BackColor="White" Font-Bold="True" 
                        Font-Names="Calibri" Font-Size="Small" ForeColor="#3333FF" 
                        style="position:absolute; top: 32px; left: 487px; height: 20px; width: 38px;" 
                        Text="Tipo :"></asp:Label>
            <asp:DropDownList ID="cmbTipoEquip" runat="server" Font-Names="Calibri" Font-Size="Small"
                        
                    style="position:absolute; top: 30px; left: 530px; width: 215px; height: 22px;" 
                    AutoPostBack="True" onselectedindexchanged="cmbTipoEquip_SelectedIndexChanged"></asp:DropDownList>

                <asp:Button ID="btnPesquisarEquipamento" runat="server" Text="Pesquisar" 
                 
                    style="position:absolute; top: 28px; left: 785px; width: 101px; height: 27px;" 
                    onclick="btnPesquisarEquipamento_Click"/>

        <asp:GridView ID="dgvEquipamento" runat="server" Height="137px" Width="880px" 
            AutoGenerateColumns="False" AllowPaging="True" 
            DataKeyNames="cmpIdEquipamentoObra" 
            onpageindexchanging="dgvEquipamento_PageIndexChanging" 
            onrowcommand="dgvEquipamento_RowCommand" CellPadding="4" ForeColor="#333333" 
                    GridLines="None" PageSize="9"
                     style="position:absolute; left: 11px; top: 67px" 
                    onselectedindexchanged="dgvEquipamento_SelectedIndexChanged">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:CommandField SelectText="Selecionar" ShowSelectButton="True" >
                <HeaderStyle Width="50px" />
                </asp:CommandField>
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
                    style="position:absolute; left: 400px; top: 340px"/>
                    
                    </asp:Panel>

        <asp:Button ID="btnShowPopup2" runat="server" style="display:none" />
                    <asp:ModalPopupExtender 
                ID="ModalPopupExtender2" 
                runat="server" 
                TargetControlID="btnShowPopup2" 
                PopupControlID="pnlpopup2"
                BackgroundCssClass="modalBackground"></asp:ModalPopupExtender>
                         <asp:Panel ID="pnlpopup2" runat="server" BackColor="White" 
            Height="100px" Width="284px" style="position:absolute; top: 221px; left: 13px; width: 250px; "  BorderStyle="Solid" BorderWidth="1px">
                     <asp:Label ID="Label3" runat="server" Text="Deseja adcionar este equipamento a um Plano de Manutenção Preventiva?"
                      style="position:absolute; left: 10px; top: 10px"></asp:Label>
                             <asp:Button ID="btnSalvarEspelho" runat="server" Text="SIM"  Height="30px" Width="75px"
                              style="position:absolute; left: 41px; top: 50px" 
                                 onclick="btnSalvarEspelho_Click"/>
                              <asp:Button ID="btnCancelarEspelho" runat="server" Text="NÃO"  Height="30px" Width="75px"
                              style="position:absolute; left: 150px; top: 50px" 
                                 onclick="btnCancelarEspelho_Click"/>
                    </asp:Panel>

                            <asp:Button ID="Button1" runat="server" style="display:none" />
                    <asp:ModalPopupExtender 
                ID="ModalPopupExtender3" 
                runat="server" 
                TargetControlID="btnShowPopup2" 
                PopupControlID="pnlpopup3"
                BackgroundCssClass="modalBackground"></asp:ModalPopupExtender>
                         <asp:Panel ID="pnlpopup3" runat="server" BackColor="White" 
            style="position:absolute; top: 34px; left: 13px; width: 515px; height: 436px;"  
            BorderStyle="Solid" BorderWidth="1px">
                   <asp:Label ID="lblPreventiva" runat="server" BackColor="White" Font-Bold="True" 
                        Font-Names="Calibri" Font-Size="Small" ForeColor="#3333FF" 
                        style="position:absolute; top: 14px; left: 15px; height: 20px; width: 486px;" 
                        Text="Preventiva :"></asp:Label>
                <asp:GridView ID="grdPreventiva" runat="server" AutoGenerateColumns="False" 
                 style="position:absolute; top: 44px; left: 12px; width: 491px; height: 76px;"
        onrowcreated="grdPreventiva_RowCreated">
        <Columns>
            <asp:BoundField DataField="cmpDcItemAtividadePreventiva">
            <HeaderStyle Width="500px" />
            </asp:BoundField>
        </Columns>
    </asp:GridView>
     <asp:Button ID="btnCancelarVisualizar" runat="server" Text="Cancelar" CausesValidation="false"
            Height="30px" Width="75px" onclick="btnCancelarEquipamento_Click"  
                    style="position:absolute; left: 228px; top: 395px"/>
            </asp:Panel>
    </div>
</asp:Content>
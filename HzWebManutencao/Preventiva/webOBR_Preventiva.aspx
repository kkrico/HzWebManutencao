<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="webOBR_Preventiva.aspx.cs" Inherits="HzWebManutencao.Preventiva.webOBR_Preventiva" ValidateRequest="false" %>
<%@ Register src="~/Controles/Mensagem.ascx" tagname="Mensagem" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Mensagem ID="CaixaMensagem" runat="server" />
    <div style="position:relative; height: 535px; width: 1154px; top: 6px; left: -54px; margin-left: 40px;">
        <asp:Label ID="lblAcao" runat="server" Text="Plano de Manutenção Preventiva"
            style="position:absolute; top: 4px; left: 6px; height: 27px; width: 968px; text-align: center;" 
            BackColor="White" Font-Bold="True" Font-Size="Large" ForeColor="Maroon" 
            Font-Names="Calibri"></asp:Label>

        <asp:Label ID="lblObra" runat="server" Text="Obra: " 
            style="position:absolute; left: 14px; top: 39px;"></asp:Label>
        <asp:DropDownList ID="cmbObra" runat="server" 
            style="position:absolute; top: 39px; left: 56px; width: 860px;" 
            AutoPostBack="True" Font-Names="Calibri" Font-Size="Small" 
            onselectedindexchanged="cmbObra_SelectedIndexChanged" >
        </asp:DropDownList>

        <asp:Button ID="btnImprimir" runat="server" Text="Imprimir Plano Manutenção" CausesValidation="false"
                
            style="position:absolute; top: 33px; left: 938px; width: 185px; height: 26px; right: 31px;" 
            onclick="btnImprimir_Click"/>

        <asp:Label ID="Label1" runat="server" Text="Periodicidade:" 
            style="position:absolute; left: 14px; top: 71px;"></asp:Label>
        <asp:DropDownList ID="cmbPeriodicidade" runat="server" 
            style="position:absolute; left: 14px; top: 89px; width: 164px;" 
            onselectedindexchanged="cmbPeriodicidade_SelectedIndexChanged" 
            AutoPostBack="True" TabIndex="1" Font-Names="Calibri" Font-Size="Small">
        </asp:DropDownList>

        <asp:Label ID="lblEquipamentos" runat="server" Text="Equipamento:" 
            
            
            style="position:absolute; left: 17px; top: 134px; height: 15px; width: 50px;"></asp:Label>
        <asp:RadioButtonList ID="rdbType" runat="server" 
                style="position:absolute; top: 89px; left: 200px; width: 253px; right: 701px; height: 20px;" 
                onselectedindexchanged="rdbType_SelectedIndexChanged" 
                RepeatDirection="Horizontal" RepeatLayout="Flow" AutoPostBack="True" 
            CellPadding="0" CellSpacing="0" TabIndex="2">
            <asp:ListItem Value="P">Pavimento</asp:ListItem>
            <asp:ListItem Value="E">Equipamento</asp:ListItem>
            <asp:ListItem Value="T">Todos</asp:ListItem>
        </asp:RadioButtonList>

        <asp:DropDownList ID="cmbTipoAtividade" runat="server" 
            style="position:absolute; top: 90px; left: 477px; width: 309px; right: 368px; bottom: 429px;" 
            AutoPostBack="True" 
            onselectedindexchanged="cmbTipoAtividade_SelectedIndexChanged" 
            TabIndex="3" Font-Names="Calibri" Font-Size="Small" >
        </asp:DropDownList>

        <asp:Label ID="Label5" runat="server" Text="Grupo de Atividade: " 
            style="position:absolute; left: 808px; top: 71px; right: 179px;"></asp:Label>

        <asp:Label ID="Label6" runat="server" Text="Itens de Atividade de Referência: " 
            style="position:absolute; left: 14px; top: 162px; right: 707px;"></asp:Label>
        <asp:ListBox ID="lstItemAtividade" runat="server" 
            style="position:absolute; top: 178px; left: 14px; width: 525px; height: 167px;" 
            AutoPostBack="True" TabIndex="7" Font-Names="Calibri" Font-Size="X-Small" 
            onselectedindexchanged="lstItemAtividade_SelectedIndexChanged"></asp:ListBox>

        <asp:Button ID="btnAddAtividade" runat="server" Text=">>"  CausesValidation="false"
            style="position:absolute; top: 236px; left: 552px; width: 36px;" 
            onclick="btnAddAtividade_Click" Font-Bold="True" />
        <asp:Button ID="btnRemoveAtividade" runat="server" Text="<<"  CausesValidation="false"
            style="position:absolute; top: 275px; left: 553px; width: 36px;" 
            onclick="btnRemoveAtividade_Click" Font-Bold="True" />

        <asp:ListBox ID="lstAtividadeObra" runat="server" 
            style="position:absolute; top: 182px; left: 604px; width: 525px; height: 156px;" 
            AutoPostBack="True" TabIndex="8" Font-Names="Calibri" Font-Size="X-Small"  OnSelectedIndexChanged="lstAtividadeObra_SelectedIndexChanged"
            ></asp:ListBox>

        <asp:DropDownList ID="cmbGrupoAtividade" runat="server" 
            style="position:absolute; top: 89px; left: 805px; width: 319px;" 
            AutoPostBack="True" 
            onselectedindexchanged="cmbGrupoAtividade_SelectedIndexChanged" 
            TabIndex="4" Font-Names="Calibri" Font-Size="Small" >
        </asp:DropDownList>

        <asp:Label ID="Label8" runat="server" Text="Itens de Atividade da Obra: " 
            style="position:absolute; left: 606px; top: 164px"></asp:Label>

        <asp:Label ID="Label4" runat="server" Text="Tipo de Atividade: " 
            style="position:absolute; left: 477px; top: 71px"></asp:Label>
        <asp:TextBox ID="txtDescriptionItemObra" runat="server" 
            style="position:absolute; top: 383px; left: 604px; width: 525px; height: 84px" 
            TabIndex="1" Font-Names="Calibri" Font-Size="Small" TextMode="MultiLine" ></asp:TextBox>

        <asp:Label ID="Label9" runat="server" Text="Descrição Item de Atividade de Referência" 
            style="position:absolute; left: 14px; top: 361px; right: 645px;"></asp:Label>

        <asp:Button ID="btnGravar" runat="server" Text="Gravar" CausesValidation="true"
            style="position:absolute; top: 490px; left: 604px; width: 71px;" 
            TabIndex="9" onclick="btnGravar_Click" />

        <asp:TextBox ID="txtDescriptionItemRef" runat="server" 
            style="position:absolute; top: 382px; left: 14px; width: 525px; height: 84px" 
            TabIndex="1" Font-Names="Calibri" Font-Size="Small" TextMode="MultiLine" 
            Enabled="False" ></asp:TextBox>

        <asp:Label ID="Label10" runat="server" Text="Descrição Item de Atividade da Obra" 
            style="position:absolute; left: 606px; top: 360px; right: 141px;"></asp:Label>

       <asp:RequiredFieldValidator ID="rfvDescricaoItemObra" runat="server" 
                ControlToValidate="txtDescriptionItemObra" Display="None" ErrorMessage="É obrigatório o preenchimento do campo descrição do item de atividade da obra!"
                
            
            
            style="position:absolute; top: 495px; left: 963px; width: 94px; height: 16px;"></asp:RequiredFieldValidator>
        <asp:ValidationSummary ID="ValidationSummary1" runat="server"
            style="position:absolute; top: 481px; left: 769px; width: 162px; height: 33px;" 
            ShowMessageBox="True" ShowSummary="False" />

        <asp:Label ID="Label11" runat="server" Text="Tipo: " 
            
            style="position:absolute; left: 200px; top: 71px; height: 15px; width: 50px;"></asp:Label>
        <asp:TextBox ID="txtEquipamento" runat="server"  
            
            
            style="position:absolute; left: 104px; top: 133px;width: 901px; right: 141px;" 
            Enabled="False"></asp:TextBox>
        <asp:Button ID="btnAddEquipamento" runat="server" Text="Equipamentos" CausesValidation="false"
            style="position:absolute; left: 1024px; top: 134px;  width: 101px;" 
            onclick="btnAddEquipamento_Click" Enabled="False" />
       

    </div>
    <asp:Button ID="btnShowPopup" runat="server" style="display:none" />
        <asp:ModalPopupExtender 
                ID="ModalPopupExtender1" 
                runat="server" 
                TargetControlID="btnShowPopup" 
                PopupControlID="pnlpopup"
                BackgroundCssClass="modalBackground"></asp:ModalPopupExtender>
    <asp:Panel ID="pnlpopup" runat="server" BackColor="White" Height="300px" Width="750px" style="display:true"
                    BorderStyle="Solid" BorderWidth="1px">
        <asp:Label ID="Label2" runat="server" Text="Equipamentos"
            BackColor="White" Font-Bold="True" Font-Size="Large" ForeColor="Maroon" 
            Font-Names="Calibri"></asp:Label>
        <asp:GridView ID="dgvEquipamento" runat="server" Height="137px" Width="730px" 
            AutoGenerateColumns="False" AllowPaging="True" 
            onpageindexchanged="dgvEquipamento_PageIndexChanged" 
            onselectedindexchanged="dgvEquipamento_SelectedIndexChanged" 
            DataKeyNames="cmpIdEquipamentoObra" 
            onpageindexchanging="dgvEquipamento_PageIndexChanging" 
            onrowcommand="dgvEquipamento_RowCommand">
            <Columns>
                <asp:CommandField SelectText="Selecionar" ShowSelectButton="True" >
                <HeaderStyle Width="50px" />
                </asp:CommandField>
                <asp:BoundField HeaderText="Equipamento" DataField="cmpDcEquipamentoObra" />
                <asp:BoundField HeaderText="Local" DataField="cmpDcLocalEquipamento" />
            </Columns>
            <PagerSettings PageButtonCount="5" />
        </asp:GridView>
        <br />
        <asp:Button ID="btnCancelarEquipamento" runat="server" Text="Cancelar" CausesValidation="false"
            Height="24px" Width="75px" onclick="btnCancelarEquipamento_Click" />
                    </asp:Panel>
</asp:Content>

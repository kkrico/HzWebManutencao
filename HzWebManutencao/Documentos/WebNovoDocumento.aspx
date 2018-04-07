<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="WebNovoDocumento.aspx.cs" Inherits="HzWebManutencao.Documentos.WebNovoDocumento" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div style="position: relative; height: 500px; width: 1012px; top: -4px; left: -13px;">
        <asp:Panel ID="pnlFileUpload" runat="server" Style="position: absolute; top: 157px; left: 5px; width: 523px;"
            CssClass="modalPopup">
            Anexar o documento:
        <asp:FileUpload ID="fileUpload" runat="server"
            Style="position: relative; left: 8px; top: 2px; width: 300px" />
        </asp:Panel>
        <asp:Label ID="Label1" runat="server" Text="Tipo:" Style="position: absolute; left: 36px; top: 1px;"></asp:Label>
        <asp:DropDownList ID="cmbTipo" runat="server"
            Style="position: absolute; top: -2px; left: 74px; width: 211px; z-index: 200;">
        </asp:DropDownList>
        <asp:GridView ID="grdReport" runat="server"
            Style="position: absolute; top: 220px; left: 0px; width: 906px"
            AllowPaging="True" AutoGenerateColumns="False">
            <Columns>
                <asp:TemplateField HeaderText="N.">
                    <ItemStyle Width="80px" HorizontalAlign="Right" />
                    <ItemTemplate>
                        <asp:LinkButton ID="lnk" runat="server"
                            CommandArgument='<%# Eval("cmpCoNumeracaoDocumento") %>' CommandName="lnk"><%#Eval("cmpNuDocumento") %></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="cmpNoObra" HeaderText="Obra">
                    <ItemStyle Width="150px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="cmpNoFuncionario" HeaderText="Funcionário">
                    <ItemStyle Width="300px" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="Tipo">
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%#Eval("cmpDcTipoNumeracao") %>' ToolTip='<%#Eval("cmpTxObservacoes") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Documento" Visible="False">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" ToolTip='<%#Eval("cmpTxObservacoes") %>'
                            CommandArgument='<%# Eval("cmpDcDocumento") %>' CommandName="cmd"><%# Eval("cmpDcDocumento").ToString().Substring(Eval("cmpDcDocumento").ToString().LastIndexOf("\\") + 1, Eval("cmpDcDocumento").ToString().Length - (Eval("cmpDcDocumento").ToString().LastIndexOf("\\") + 1)) %></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:CommandField SelectText="Documento" ShowSelectButton="True" />
            </Columns>
        </asp:GridView>
        <asp:TextBox ID="txtNumber" runat="server"
            Style="position: absolute; top: 86px; left: 75px; width: 205px;"></asp:TextBox>
        <asp:Button ID="btnSearch" runat="server" Text="Pesquisar"
            Style="position: absolute; top: 118px; left: 396px;"
            CausesValidation="False" />
        <asp:Button ID="btnSave" runat="server" Text="Gravar"
            Style="position: absolute; top: 118px; left: 493px; height: 26px;" />
        <asp:Label ID="Label3" runat="server" Text="Funcionário:"
            Style="position: absolute; top: 26px; left: 0px;"></asp:Label>
        <asp:DropDownList ID="cmbFuncionario" runat="server"
            Style="position: absolute; top: 27px; left: 74px; width: 211px;">
        </asp:DropDownList>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
            Enabled="false" Style="position: absolute; top: 6px; left: 316px;"
            ErrorMessage="Selecione um arquivo." ControlToValidate="fileUpload"></asp:RequiredFieldValidator>
        <asp:Label ID="Label4" runat="server" Text=""
            Style="position: absolute; top: 29px; left: 306px;"></asp:Label>
        <asp:Label ID="Label5" runat="server" Text="Ano:"
            Style="position: absolute; top: 59px; left: 4px;"></asp:Label>
        <asp:Label ID="Label2" runat="server" Text="Descrição:"
            Style="position: absolute; top: 120px; left: 6px;"></asp:Label>
        <asp:TextBox ID="txtDescription" runat="server"
            Style="position: absolute; top: 120px; left: 74px; width: 306px;"></asp:TextBox>
        <asp:Label ID="Label6" runat="server" Text="Número:"
            Style="position: absolute; top: 88px; left: 2px;"></asp:Label>
        <asp:TextBox ID="txtYear" runat="server"
            Style="position: absolute; top: 55px; left: 74px; width: 49px;"></asp:TextBox>
        <asp:Label ID="lblTip" runat="server" Text="Para visualizar observações, coloque o mouse sobre o tipo"
            Style="position: absolute; top: 200px; left: 5px; width: 461px;"></asp:Label>
    </div>

    <asp:Button ID="btnShow" runat="server" Text="Show Modal Popup" />

    <!-- ModalPopupExtender -->
    <asp:ModalPopupExtender ID="mp1" runat="server" PopupControlID="Panel1" TargetControlID="btnShow"
        CancelControlID="CancelButton"
        BackgroundCssClass="modalBackground">
    </asp:ModalPopupExtender>
    <asp:Panel ID="Panel1" runat="server" CssClass="modalPopup" align="center" Style="display: none">
        <div style="position: relative; height: 500px; width: 850px; top: -7px; left: -54px; margin-left: 40px;">
            <asp:Label ID="Label7" runat="server" Text="Tipo:" Style="position: absolute"></asp:Label>
            <asp:DropDownList ID="DropDownList1" runat="server"
                Style="position: absolute; top: -2px; left: 74px; width: 211px;"
                AutoPostBack="True">
            </asp:DropDownList>
            <asp:Label ID="Label8" runat="server" Text="Pessoal:"
                Style="position: absolute; top: 28px; left: 3px;"></asp:Label>
            <asp:DropDownList ID="cmbPessoal" runat="server"
                Style="position: absolute; top: 25px; left: 74px; width: 211px">
            </asp:DropDownList>
            <asp:Label ID="Label9" runat="server" Text="Obra:"
                Style="position: absolute; top: 52px; left: 3px;"></asp:Label>
            <asp:DropDownList ID="cmbObra" runat="server"
                Style="position: absolute; top: 54px; left: 74px; width: 211px">
            </asp:DropDownList>
            <asp:Label ID="Label10" runat="server" Text="Descrição:"
                Style="position: absolute; top: 81px; left: 1px;"></asp:Label>
            <asp:TextBox ID="TextBox1" runat="server"
                Style="position: absolute; top: 81px; left: 74px; width: 461px; height: 74px;"
                Columns="5" TextMode="MultiLine"></asp:TextBox>
            <asp:Button ID="btnGenerate" runat="server" Text="Gerar"
                Style="position: absolute; top: -4px; left: 293px;" />
            <asp:Label ID="lblFornecedor" runat="server" Text="Fornecedor:"
                Style="position: absolute; top: 172px; left: 2px;"></asp:Label>
            <asp:TextBox ID="txtFornecedor" runat="server"
                Style="position: absolute; top: 202px; left: 74px; width: 461px; height: 21px;"></asp:TextBox>
            <asp:DropDownList ID="cmbFornecedor" runat="server"
                Style="position: absolute; top: 166px; left: 74px; width: 211px">
            </asp:DropDownList>
            <asp:Label ID="lblNovoNumero" runat="server"
                Style="position: absolute; top: 1px; left: 379px;" Font-Bold="True"
                Font-Size="12pt" ForeColor="#CC3300"></asp:Label>
            <asp:Button runat="server" ID="CancelButton" Text="Cancelar" />
        </div>
    </asp:Panel>
</asp:Content>

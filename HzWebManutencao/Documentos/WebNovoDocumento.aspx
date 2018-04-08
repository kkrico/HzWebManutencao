<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="WebNovoDocumento.aspx.cs" Inherits="HzWebManutencao.Documentos.WebNovoDocumento" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <%--<link href="~/Styles/simple-grid.min.css" rel="stylesheet" type="text/css" />--%>

    <link rel="stylesheet" href="<%= Page.ResolveClientUrl("~/Styles/simple-grid.min.css") %>"/>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-2 hidden-sm"></div>
        <div class="col-8">
            <h3 class="center m-bottom">Simple Typography</h3>
            <p>
                Simple Grid uses Lato from Google Fonts as a base font-family. Font-size is based on root rem units.
            </p>
            <h1>Header 1</h1>
            <h2>Header 2</h2>
            <h3>Header 3</h3>
            <h4>Header 4</h4>
            <h5>Header 5</h5>
            <h6>Header 6</h6>
            <p>
                Additionally, should you choose to style any of the headers or paragraph font-weights, simply add the class .font-light, <span class="font-regular">.font-regular</span>, or <span class="font-heavy">.font-heavy</span> to your markup. Paragraph text is set by default to a font-weight of 200. Note: the .font-heavy class should not be used as a replacement for semantic bold body copy.
            </p>
        </div>
        <div class="col-2 hidden-sm"></div>
    </div>
    <div>
        <asp:Panel ID="pnlFileUpload" runat="server" CssClass="modalPopup">
            Anexar o documento:
        <asp:FileUpload ID="fileUpload" runat="server" />
        </asp:Panel>
        <asp:Label ID="Label1" runat="server" Text="Tipo:"></asp:Label>
        <asp:DropDownList ID="cmbTipo" runat="server">
        </asp:DropDownList>
        <asp:GridView ID="grdReport" runat="server"
            AllowPaging="True" AutoGenerateColumns="False">
            <Columns>
                <asp:TemplateField HeaderText="N.">
                    <ItemStyle HorizontalAlign="Right" />
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
        <asp:TextBox ID="txtNumber" runat="server"></asp:TextBox>
        <asp:Button ID="btnSearch" runat="server" Text="Pesquisar" CausesValidation="False" />
        <asp:Button ID="btnSave" runat="server" Text="Gravar" />
        <asp:Label ID="Label3" runat="server" Text="Funcionário:"></asp:Label>
        <asp:DropDownList ID="cmbFuncionario" runat="server">
        </asp:DropDownList>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
            Enabled="false" ErrorMessage="Selecione um arquivo." ControlToValidate="fileUpload"></asp:RequiredFieldValidator>
        <asp:Label ID="Label4" runat="server" Text=""></asp:Label>
        <asp:Label ID="Label5" runat="server" Text="Ano:"></asp:Label>
        <asp:Label ID="Label2" runat="server" Text="Descrição:"></asp:Label>
        <asp:TextBox ID="txtDescription" runat="server"></asp:TextBox>
        <asp:Label ID="Label6" runat="server" Text="Número:"></asp:Label>
        <asp:TextBox ID="txtYear" runat="server"></asp:TextBox>
        <asp:Label ID="lblTip" runat="server" Text="Para visualizar observações, coloque o mouse sobre o tipo"></asp:Label>
    </div>

    <asp:Button ID="btnShow" runat="server" Text="Show Modal Popup" />

    <!-- ModalPopupExtender -->
    <asp:ModalPopupExtender ID="mp1" runat="server" PopupControlID="Panel1" TargetControlID="btnShow"
        CancelControlID="CancelButton"
        BackgroundCssClass="modalBackground">
    </asp:ModalPopupExtender>
    <asp:Panel ID="Panel1" runat="server" CssClass="modalPopup" align="center" Style="display: none">
        <div>
            <asp:Label ID="Label7" runat="server" Text="Tipo:"></asp:Label>
            <asp:DropDownList ID="DropDownList1" runat="server"
                AutoPostBack="True">
            </asp:DropDownList>
            <asp:Label ID="Label8" runat="server" Text="Pessoal:"></asp:Label>
            <asp:DropDownList ID="cmbPessoal" runat="server">
            </asp:DropDownList>
            <asp:Label ID="Label9" runat="server" Text="Obra:"></asp:Label>
            <asp:DropDownList ID="cmbObra" runat="server">
            </asp:DropDownList>
            <asp:Label ID="Label10" runat="server" Text="Descrição:"></asp:Label>
            <asp:TextBox ID="TextBox1" runat="server"
                Columns="5" TextMode="MultiLine"></asp:TextBox>
            <asp:Button ID="btnGenerate" runat="server" Text="Gerar" />
            <asp:Label ID="lblFornecedor" runat="server" Text="Fornecedor:"></asp:Label>
            <asp:TextBox ID="txtFornecedor" runat="server"></asp:TextBox>
            <asp:DropDownList ID="cmbFornecedor" runat="server">
            </asp:DropDownList>
            <asp:Label ID="lblNovoNumero" runat="server"
                ForeColor="#CC3300"></asp:Label>
            <asp:Button runat="server" ID="CancelButton" Text="Cancelar" />
        </div>
    </asp:Panel>
</asp:Content>

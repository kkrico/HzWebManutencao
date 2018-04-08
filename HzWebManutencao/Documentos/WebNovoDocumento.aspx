<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="WebNovoDocumento.aspx.cs" Inherits="HzWebManutencao.Documentos.WebNovoDocumento" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="stylesheet" href="<%= Page.ResolveClientUrl("~/Styles/simple-grid.min.css") %>" />
    <style>
        table {
            width: 100%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <asp:ModalPopupExtender ID="mp1" runat="server" PopupControlID="Panel1" TargetControlID="btnShow"
        CancelControlID="CancelButton"
        BackgroundCssClass="modalBackground">
    </asp:ModalPopupExtender>
    <asp:Panel ID="Panel1" runat="server" CssClass="modalPopup" align="center" Style="display: none">
        <div class="row esquerda">
            <div class="col-12">
                <asp:Label ID="Label2" runat="server" Text="Tipo:"></asp:Label>
                <asp:DropDownList ID="cmbTipoGerar" runat="server">
                </asp:DropDownList>
            </div>
        </div>
        <div class="row esquerda">
            <div class="col-12">
                <asp:Label ID="Label7" runat="server" Text="Pessoal:"></asp:Label>
                <asp:DropDownList ID="cmbPessoal" runat="server">
                </asp:DropDownList>
            </div>
        </div>
        <div class="row esquerda">
            <div class="col-12">
                <asp:Label ID="Label8" runat="server" Text="Obra:"></asp:Label>
                <asp:DropDownList ID="cmbObra" runat="server">
                </asp:DropDownList>
                <asp:TextBox ID="txtFornecedor" runat="server" Visible="False"></asp:TextBox>
                <asp:Label ID="lblNovoNumero" runat="server"></asp:Label>
                <asp:Label  ID="lblFornecedor" runat="server"></asp:Label>
                <asp:DropDownList ID="cmbFornecedor" runat="server" Visible="False">
                </asp:DropDownList>
            </div>
        </div>
        <div class="row esquerda">
            <div class="col-12">
                <asp:Label ID="Label9" runat="server" Text="Descrição:"></asp:Label>
                <asp:TextBox ID="TextBox1" runat="server" Columns="5" TextMode="MultiLine"></asp:TextBox>
            </div>
        </div>
        <div class="row esquerda">
            <div class="col-12">
                <asp:Button ID="btnGenerate" runat="server" Text="Gerar" OnClick="btnGenerate_Click" />
                <asp:Button runat="server" ID="CancelButton" Text="Cancelar" />
            </div>
        </div>
    </asp:Panel>

    <div class="row">
        <div class="col-12">
            <div class="row">
                <div class="col-12">
                    <asp:Button runat="server" ID="btnSearch" Text="Pesquisar" OnClick="btnSearch_Click" />
                    <asp:Button runat="server" ID="btnSave" Text="Gravar" OnClick="btnSave_Click" />
                    <asp:Button ID="btnShow" runat="server" Text="Cadastrar"/>
                </div>
            </div>
            <div class="row">
                <div class="col-1">
                    <asp:Label ID="Label1" runat="server" Text="Tipo:"></asp:Label>
                </div>
                <div class="col-11">
                    <asp:DropDownList ID="cmbTipo" runat="server">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="row">
                <div class="col-1">
                    <asp:Label ID="Label3" runat="server" Text="Funcionário:"></asp:Label>
                </div>
                <div class="col-11">
                    <asp:DropDownList ID="cmbFuncionario" runat="server">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="row">
                <div class="col-1">
                    <asp:Label ID="Label11" runat="server" Text="Ano:"></asp:Label>
                </div>
                <div class="col-11">
                    <asp:TextBox ID="txtYear" runat="server"></asp:TextBox>
                </div>
            </div>
            <div class="row">
                <div class="col-1">
                    <asp:Label ID="Label6" runat="server" Text="Número:"></asp:Label>
                </div>
                <div class="col-11">
                    <asp:TextBox ID="txtNumber" runat="server"></asp:TextBox>
                </div>
            </div>
            <div class="row">
                <div class="col-1">
                    <asp:Label ID="Label5" runat="server" Text="Descrição:"></asp:Label>
                </div>
                <div class="col-11">
                    <asp:TextBox ID="txtDescription" runat="server" Style="width: 97%;"></asp:TextBox>
                </div>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-12">
            <asp:Label ID="lblTip" runat="server" Text="Para visualizar observações, coloque o mouse sobre o tipo"></asp:Label>
            <asp:Label ID="Label4" runat="server" Text="Label"></asp:Label>
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
                    <asp:TemplateField HeaderText="Enviar">
                        <ItemTemplate>
                            <asp:Panel runat="server" Style="margin: 0.5em; text-align: center">
                                <asp:FileUpload ID="fileUpload" runat="server" />
                                <asp:Button runat="server" Text="Enviar" />
                            </asp:Panel>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:CommandField SelectText="Documento" ShowSelectButton="True" HeaderText="Documento" />
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>

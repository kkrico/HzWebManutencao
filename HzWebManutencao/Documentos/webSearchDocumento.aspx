<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="webSearchDocumento.aspx.cs" Inherits="HzWebManutencao.Documentos.webSearchDocumento" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div style="position:relative; height: 500px; width: 1012px; top: -4px; left: -13px;">
        <asp:Panel ID="pnlFileUpload" runat="server" Style="position:absolute;top:157px; left: 5px; width: 523px;" 
        CssClass="modalPopup">Anexar o documento:
        <asp:FileUpload ID="fileUpload" runat="server" 
                style="position:relative; left:8px; top:2px; width:300px" />
        </asp:Panel>
                <asp:Label ID="Label1" runat="server" Text="Tipo:" style="position:absolute;left:36px; top: 1px;"></asp:Label>
                <asp:DropDownList ID="cmbTipo" runat="server" 
                    style="position:absolute; top: -2px; left: 74px; width: 211px;z-index:200;" 
                    >
                </asp:DropDownList>
                <asp:GridView ID="grdReport" runat="server" 
                    style="position:absolute; top: 220px; left: 0px; width:906px" 
                    AllowPaging="True" AutoGenerateColumns="False" 
                    onpageindexchanging="grdReport_PageIndexChanging" 
                    onrowcommand="grdReport_RowCommand" >
                    <Columns>
                        <asp:TemplateField HeaderText="N.">
                        <ItemStyle Width="80px" HorizontalAlign="Right"/>
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
                                <asp:Label runat="server" text='<%#Eval("cmpDcTipoNumeracao") %>' tooltip='<%#Eval("cmpTxObservacoes") %>'></asp:Label>
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
                    style="position:absolute; top: 86px; left: 75px; width: 205px;"></asp:TextBox>
                <asp:Button ID="btnSearch" runat="server" Text="Pesquisar" 
                    style="position:absolute; top: 118px; left: 396px;" 
                    onclick="btnSearch_Click" CausesValidation="False"/>
                <asp:Button ID="btnSave" runat="server" Text="Gravar" 
                    style="position:absolute; top: 118px; left: 493px; height: 26px;" 
                    onclick="btnSave_Click" />
                <asp:Label ID="Label3" runat="server" Text="Funcionário:" 
                    style="position:absolute; top: 26px; left: 0px;"></asp:Label>
                <asp:DropDownList ID="cmbFuncionario" runat="server" 
                    style="position:absolute; top: 27px; left: 74px; width: 211px;">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                    Enabled="false" style="position:absolute; top: 6px; left: 316px;"
                    ErrorMessage="Selecione um arquivo." ControlToValidate="fileUpload"></asp:RequiredFieldValidator>
                <asp:Label ID="Label4" runat="server" Text="Label" 
                    style="position:absolute; top: 29px; left: 306px;"></asp:Label>
                <asp:Label ID="Label5" runat="server" Text="Ano:" 
                    style="position:absolute; top: 59px; left: 4px;"></asp:Label>
                <asp:Label ID="Label2" runat="server" Text="Descrição:" 
                    style="position:absolute; top: 120px; left: 6px;"></asp:Label>
                <asp:TextBox ID="txtDescription" runat="server" 
                    style="position:absolute; top: 120px; left: 74px; width: 306px;"></asp:TextBox>
                <asp:Label ID="Label6" runat="server" Text="Número:" 
                    style="position:absolute; top: 88px; left: 2px;"></asp:Label>
                <asp:TextBox ID="txtYear" runat="server" 
                    style="position:absolute; top: 55px; left: 74px; width: 49px;"></asp:TextBox>
                <asp:Label ID="lblTip" runat="server" Text="Para visualizar observações, coloque o mouse sobre o tipo" 
                    style="position:absolute; top: 200px; left: 5px; width: 461px;"></asp:Label>
    </div>
</asp:Content>

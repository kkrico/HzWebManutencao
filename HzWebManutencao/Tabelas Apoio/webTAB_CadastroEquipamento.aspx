<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="webTAB_CadastroEquipamento.aspx.cs" Inherits="HzWebManutencao.Tabelas_Apoio.webTAB_CadastroEquipamento" %>
<%@ Register Src="~/Controles/Mensagem.ascx" TagName="Mensagem" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Mensagem ID="CaixaMensagem" runat="server" />
    <div  style="position:relative; height: 499px; width: 932px; top: 6px; left: -54px; margin-left: 40px;">
        <asp:Panel ID="pnlCadastraMaterial" runat="server" 
            style="position:absolute; left:10px; width:914px; height:125px; top: 7px;">
            <asp:Label ID="lblAcao" runat="server" Text="Cadastro de Equipamentos" 
                style="position:absolute; top: 1px; left: 11px; height: 20px; width: 895px; text-align: center;" 
                BackColor="White" Font-Bold="True" Font-Size="Medium" ForeColor="Maroon">
            </asp:Label>
            <asp:Label ID="lblEquipamento" runat="server" Text="Descrição do Equipamento"
                style="position:absolute; top: 32px; left: 21px; height: 25px; width: 167px;">
            </asp:Label>
            <asp:TextBox ID="txtDcEquipamentoPesq" runat="server"
                style="position:absolute; top: 51px; left: 20px; height: 18px; width: 406px;" 
                AutoPostBack="True" ontextchanged="txtDcEquipamento_TextChanged"></asp:TextBox>
            <asp:Label ID="lblTipoEquipamento" runat="server" Text="Tipo do Equipamento"
                style="position:absolute; top: 32px; left: 434px; height: 25px; width: 137px;">
            </asp:Label>
            <asp:DropDownList ID="cmbTipoEquipamento" runat="server" AutoPostBack="True" 
                Font-Names="Calibri" Font-Size="Small" 
                style="position:absolute; top: 51px; left: 435px; width: 326px; height: 24px;" 
                onselectedindexchanged="cmbTipoEquipamento_SelectedIndexChanged">
            </asp:DropDownList>

            <asp:Button ID="btnPesquisa" runat="server" Text="Pesquisar" 
                style="position:absolute; top: 84px; left: 23px; height: 30px; width: 103px;" 
                CausesValidation="false" onclick="btnPesquisa_Click"/>

            <asp:Button ID="btnNovo" runat="server" Text="Novo" 
                style="position:absolute; top: 84px; left: 133px; height: 30px; width: 103px;" 
                CausesValidation="false" onclick="btnNovo_Click"/>

         </asp:Panel>

       <asp:GridView ID="grdPesquisa" runat="server" 
            style="position:absolute; top: 142px; left: 7px; width: 913px; " AllowPaging="True" 
            AllowSorting="True" AutoGenerateColumns="False" 
            onpageindexchanging="grdPesquisa_PageIndexChanging" 
            ViewStateMode="Enabled" 
            BorderColor="#003366" BorderStyle="Inset" BorderWidth="1px"
            CellPadding="3" BackColor="White" Font-Size="Small" 
            onrowdatabound="grdPesquisa_RowDataBound">
            <Columns>
                 <asp:TemplateField HeaderText="Operação">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkIncluir" runat="server" Text="Incluir" 
                                CommandArgument='<%# Eval("cmpIdEquipamento") %>' CommandName="incluir"                             
                                OnClick="lnkIncluir_Click" CausesValidation="False">
                            </asp:LinkButton>
                            <asp:LinkButton ID="lnkEditar" runat="server" Text="Editar" 
                                CommandArgument='<%# Eval("cmpIdEquipamento") %>' CommandName="editar"                             
                                OnClick="lnkEditar_Click" CausesValidation="False">
                            </asp:LinkButton>
                            <asp:LinkButton ID="lnkExcluir" runat="server" Text="Excluir" 
                                CommandArgument='<%# Eval("cmpIdEquipamento") %>' CommandName="excluir"                             
                                OnClick="lnkExcluir_Click" CausesValidation="False">
                            </asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle Width="120px" HorizontalAlign="Left" VerticalAlign="Middle" />
                    </asp:TemplateField>
                <asp:BoundField DataField="CmpDcEquipamento" HeaderText="Descrição Equipamento">
                    <ItemStyle Width="200px" HorizontalAlign="Left" Font-Size="Small" />
                </asp:BoundField>
                <asp:BoundField DataField="cmpDcTipoAtividade" HeaderText="Tipo Equipamento">
                    <ItemStyle Width="100px" HorizontalAlign="Left" Font-Size="Small" />
                </asp:BoundField>
                <asp:BoundField DataField="cmpCoGrupoAtividade" HeaderText="">
                    <ItemStyle Width="10px" HorizontalAlign="Left" Font-Size="Small" />
                </asp:BoundField>
            </Columns>
            <FooterStyle BackColor="White" ForeColor="#000066" />
            <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" 
                Font-Names="Calibri" Font-Size="Medium" />
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

        <asp:Panel ID="pnlpopup" runat="server" BackColor="White" Height="213px" Width="600px" style="display:none"
                    BorderStyle="Solid" BorderWidth="1px">
            <asp:Label ID="Label1" runat="server" Text="Cadastro de Equipamento"
                        style="position:absolute; top: 5px; left: 7px; height: 20px; width: 589px; font-size:medium; text-align: center;" 
                        BackColor="White" Font-Bold="True" Font-Size="Large" 
                        ForeColor="#3333FF" Font-Names="Calibri"></asp:Label>
            <asp:Label ID="lblDescricao" runat="server" Text="Descrição do Equipamento"
                        style="position:absolute; top: 35px; left: 17px; height: 16px; width: 150px;" 
                        BackColor="White" Font-Bold="True" Font-Size= "Small" 
                        ForeColor="#3333FF" Font-Names="Calibri"></asp:Label>
            <asp:TextBox ID="TxtDescricao" runat="server" 
                        style="position:absolute; top: 55px; left: 12px; width: 566px; height: 45px;" 
                        Font-Names="Calibri" Font-Size="Small" TextMode="MultiLine"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RvfTxtDescricao" runat="server"  
                            ControlToValidate="TxtDescricao"
                            style="position:absolute; top: 173px; left: 334px; height: 22px; width: 157px; "
                            ErrorMessage="Descrição do equipamento em branco!" 
                Display="None"></asp:RequiredFieldValidator>
            <asp:Label ID="lblTipoEquip" runat="server" Text="Tipo Equipamento"
                        style="position:absolute; top: 109px; left: 14px; height: 20px; width: 113px;" 
                        BackColor="White" Font-Bold="True" Font-Size="Small" 
                        ForeColor="#3333FF" Font-Names="Calibri"></asp:Label>
            <asp:DropDownList ID="cmbTipoEquip" runat="server" Font-Names="Calibri" Font-Size="Small"
                        style="position:absolute; top: 127px; left: 14px; width: 285px; height: 24px;"></asp:DropDownList>
            <asp:Button ID="btnUpdate" CommandName="Update" runat="server" Text="Gravar" 
                        style="position:absolute; top: 169px; left: 16px; width: 70px; height: 30px;" 
                        onclick="btnUpdate_Click"/>
            <asp:Button ID="btnCancel" runat="server" Text="Cancelar" CausesValidation="false"
                        style="position:absolute; top: 170px; left: 95px; width: 70px; height: 30px;" 
                        onclick="btnCancel_Click" />
        </asp:Panel>


    </div>

</asp:Content>

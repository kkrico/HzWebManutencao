<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="webTAB_CadastroEquipamentoObra.aspx.cs" Inherits="HzWebManutencao.Tabelas_Apoio.webTAB_CadastroEquipamentoObra" %>
<%@ Register src="~/Controles/Mensagem.ascx" tagname="Mensagem" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Mensagem ID="CaixaMensagem" runat="server"/>
    <div  style="position:relative; height: 499px; width: 1157px; top: 6px; left: -54px; margin-left: 40px;">
        <asp:Panel ID="pnlCadastraMaterial" runat="server" 
            
            style="position:absolute; left:10px; width:1144px; height:111px; top: 7px;">
            <asp:Label ID="lblAcao" runat="server" Text="Cadastro de Equipamento da Obra" 
                style="position:absolute; top: 1px; left: 7px; height: 20px; width: 1131px; text-align: center;" 
                BackColor="White" Font-Bold="True" Font-Size="Medium" ForeColor="Maroon"></asp:Label>
           <asp:Label ID="Label3" runat="server" Text="Obra" 
                style="position:absolute; top: 22px; left: 11px; height: 18px; width: 44px;"></asp:Label>
            <asp:DropDownList ID="cmbObra" runat="server" AutoPostBack="True" 
                Font-Names="Calibri" Font-Size="Small"  
                style="position:absolute; top: 40px; left: 10px; width: 625px; height: 21px; bottom: 64px; right: 279px;" 
                onselectedindexchanged="cmbObra_SelectedIndexChanged" ></asp:DropDownList>
            <asp:Label ID="lblEquipamento" runat="server" Text="Descrição do Equipamento"
                style="position:absolute; top: 22px; left: 641px; height: 15px; width: 167px;"></asp:Label>
            <asp:TextBox ID="txtDcEquipamentoPesq" runat="server"
                style="position:absolute; top: 39px; left: 641px; height: 16px; width: 264px;" 
                AutoPostBack="True" ontextchanged="txtDcEquipamento_TextChanged"></asp:TextBox>
            <asp:Label ID="lblTipoEquipamento" runat="server" Text="Tipo do Equipamento"
                style="position:absolute; top: 63px; left: 11px; height: 15px; width: 137px;"></asp:Label>
            <asp:DropDownList ID="cmbTipoEquipamento" runat="server" AutoPostBack="True" 
                Font-Names="Calibri" Font-Size="Small" 
                onselectedindexchanged="cmbTipoEquipamento_SelectedIndexChanged" 
                style="position:absolute; top: 81px; left: 10px; width: 378px; height: 21px;"></asp:DropDownList>
            <asp:Label ID="Label2" runat="server" Text="Pavimento"
                style="position:absolute; top: 62px; left: 397px; height: 14px; width: 76px;"></asp:Label>
            <asp:DropDownList ID="cmbObraPavimento" runat="server" AutoPostBack="True" 
                Font-Names="Calibri" Font-Size="Small" 
                style="position:absolute; top: 80px; left: 397px; width: 238px; bottom: 10px; right: 279px;" 
                onselectedindexchanged="cmbObraPavimento_SelectedIndexChanged"></asp:DropDownList>
         </asp:Panel>

        <asp:Button ID="btnPesquisa" runat="server" Text="Pesquisar" 
            style="position:absolute; top: 80px; left: 733px; height: 30px; width: 88px;" 
            CausesValidation="false" onclick="btnPesquisa_Click"/>
        <asp:Button ID="btnNovo" runat="server" Text="Novo" 
            style="position:absolute; top: 79px; left: 831px; height: 30px; width: 88px;" 
            CausesValidation="false" onclick="btnNovo_Click"/>

            <asp:Button ID="btnGerarQrcodes" runat="server" Text="Gerar QRCODE" 
            style="position:absolute; top: 79px; left: 931px; height: 30px; width: 88px;" 
            CausesValidation="false" onclick="btnGerarQrcodes_Click"/>

       <asp:GridView ID="grdPesquisa" runat="server" 
            style="position:absolute; top: 121px; left: 13px; width: 1150px; " AllowPaging="True" 
            AllowSorting="True" AutoGenerateColumns="False" 
            onpageindexchanging="grdPesquisa_PageIndexChanging" 
            ViewStateMode="Enabled" 
            BorderColor="#003366" BorderStyle="Inset" BorderWidth="1px"
            CellPadding="3" BackColor="White" Font-Size="Small" 
            onrowdatabound="grdPesquisa_RowDataBound" 
            onrowcommand="grdPesquisa_RowCommand" 
            onselectedindexchanged="grdPesquisa_SelectedIndexChanged1" PageSize="8">
            <Columns>
                 <asp:TemplateField HeaderText="Operação">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkIncluir" runat="server" Text="Incluir" 
                                CommandArgument='<%# Eval("cmpIdEquipamentoObra") %>' CommandName="incluir"                             
                                OnClick="lnkIncluir_Click" CausesValidation="False">
                            </asp:LinkButton>
                            <asp:LinkButton ID="lnkEditar" runat="server" Text="Editar" 
                                CommandArgument='<%# Eval("cmpIdEquipamentoObra") %>' CommandName="editar"                             
                                OnClick="lnkEditar_Click" CausesValidation="False">
                            </asp:LinkButton>
                            <asp:LinkButton ID="lnkExcluir" runat="server" Text="Excluir" 
                                CommandArgument='<%# Eval("cmpIdEquipamentoObra") %>' CommandName="excluir"                             
                                OnClick="lnkExcluir_Click" CausesValidation="False">
                            </asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle Width="130px" HorizontalAlign="Left" VerticalAlign="Middle" />
                </asp:TemplateField>
                <asp:BoundField DataField="CmpDcEquipamentoObra" HeaderText="Descrição Equipamento">
                    <ItemStyle Width="300px" HorizontalAlign="Left" Font-Size="Small" />
                </asp:BoundField>
                 <asp:BoundField DataField="CodEquipamento" HeaderText="Código">
                    <ItemStyle Width="50px" HorizontalAlign="Left" Font-Size="Small" />
                </asp:BoundField>
                 <asp:BoundField DataField="cmpDcPavimento" HeaderText="Pavimento">
                    <ItemStyle Width="100px" HorizontalAlign="Left" Font-Size="Small" />
                </asp:BoundField>
                 <asp:BoundField DataField="cmpDcTipoAtividade" HeaderText="Tipo">
                    <ItemStyle Width="150px" HorizontalAlign="Left" Font-Size="Small" />
                </asp:BoundField>
                 <asp:BoundField DataField="cmpDcMarcaModeloEquipamento" HeaderText="Marca/Marca">
                    <ItemStyle Width="130px" HorizontalAlign="Left" Font-Size="Small" />
                </asp:BoundField>
                 <asp:BoundField DataField="DcCapacidade" HeaderText="Capacidade">
                    <ItemStyle Width="60px" HorizontalAlign="Right" Font-Size="Small" />
                </asp:BoundField>
                 <asp:BoundField DataField="cmpNuSerieEquipamento" HeaderText="Série">
                    <ItemStyle Width="100px" HorizontalAlign="Right" Font-Size="Small" />
                </asp:BoundField>

                <asp:TemplateField HeaderText="OS">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnk" runat="server" 
                            CommandArgument='<%# Eval("cmpIdEquipamentoObra") %>' 
                            CommandName="lnk" 
                            CausesValidation="False">
                        </asp:LinkButton>
                    </ItemTemplate>
                    <HeaderStyle Width="10px" />
                    <ItemStyle Width="80px" HorizontalAlign="Left" Font-Size="Small"/>
                </asp:TemplateField>
                <asp:BoundField DataField="cmpCoGrupoAtividade" HeaderText="cmpCoGrupoAtividade">
                    <ItemStyle Width="10px" HorizontalAlign="Left" Font-Size="Small" />
                </asp:BoundField>
                <asp:BoundField DataField="cmpIdTipoCapacidadeEquipamento" HeaderText="cmpIdTipoCapacidadeEquipamento">
                    <ItemStyle Width="10px" HorizontalAlign="Left" Font-Size="Small" />
                </asp:BoundField>
                <asp:BoundField DataField="cmpIdEquipamento" HeaderText="cmpIdEquipamento">
                    <ItemStyle Width="10px" HorizontalAlign="Left" Font-Size="Small" />
                </asp:BoundField>
                 <asp:BoundField DataField="cmpIdEquipamentoObra" 
                     HeaderText="IdEquipamentoObra" />
                 <asp:ButtonField ButtonType="Image" CommandName="Historico" 
                     HeaderText="Histórico" ImageUrl="~/App_Themes/General/page_white_acrobat.png" 
                     Text="Button" />
            </Columns>
            <FooterStyle BackColor="White" ForeColor="#000066" />
            <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" Font-Names="Calibri" Font-Size="Medium" />
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

        <asp:Panel ID="pnlpopup" runat="server" BackColor="White" 
                    Height="452px" Width="850px" BorderStyle="Solid" 
                    BorderWidth="1px"
                    style="display:none">
            <asp:Label ID="Label1" runat="server" Text="Equipamentos Orion"
                        style="position:absolute; top: 5px; left: 0px; height: 20px; width: 800px; font-size:medium; text-align: center;" 
                        BackColor="White" Font-Bold="True" Font-Size="Large" 
                        ForeColor="#3333FF" Font-Names="Calibri"></asp:Label>
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
                        style="position:absolute; top: 30px; left: 530px; width: 215px; height: 22px;"></asp:DropDownList>
            <asp:Button ID="btnPesqEquipOrion" runat="server" Text="Pesquisar" CausesValidation="false"
                        style="position:absolute; top: 60px; left: 24px; width: 80px; height: 30px;" 
                        onclick="btnPesqEquipOrion_Click" />
            <asp:Button ID="btnVoltar" runat="server" Text="Voltar" CausesValidation="false"
                        style="position:absolute; top: 60px; left: 110px; height: 30px; width: 80px;" 
                        onclick="btnVoltar_Click" />

            <asp:GridView ID="grdEquipOrion" runat="server" AllowPaging="True" 
                AllowSorting="True" AutoGenerateColumns="False" BackColor="White" 
                BorderColor="#003366" BorderStyle="Inset" BorderWidth="1px" CellPadding="3" 
                Font-Names="Calibri" Font-Size="Smaller" 
                onpageindexchanging="grdEquipOrion_PageIndexChanging" 
                onrowdatabound="grdEquipOrion_RowDataBound" PageSize="10" 
                SelectedRowStyle-BackColor="#33CCCC" 
                style="position:absolute; top: 100px; left: 24px; width: 800px;" 
                ViewStateMode="Enabled">
                <HeaderStyle CssClass="header" />
                <RowStyle CssClass="normal" />
                <Columns>
                    <asp:TemplateField HeaderText="Seleção">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkSelecao" runat="server" CausesValidation="False" 
                                CommandArgument='<%# Eval("cmpIdEquipamento") %>' CommandName="selecao" 
                                OnClick="lnkSelecao_Click" Text="Vincular">
                                </asp:LinkButton>
                        </ItemTemplate>
                        <FooterStyle Font-Names="Calibri" Font-Size="Small" />
                        <HeaderStyle Font-Names="Calibri" Font-Size="Small" />
                        <ItemStyle Font-Names="Calibri" Font-Size="Small" HorizontalAlign="Left" 
                            VerticalAlign="Middle" Width="20px" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="CmpDcEquipamento" HeaderText="Descrição Equipamento">
                    <HeaderStyle Font-Names="Calibri" Font-Size="Small" />
                    <ItemStyle Font-Names="Calibri" Font-Size="Small" HorizontalAlign="Left" 
                        Width="300px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="cmpDcTipoAtividade" HeaderText="Tipo Equipamento">
                    <HeaderStyle Font-Names="Calibri" Font-Size="Small" />
                    <ItemStyle Font-Names="Calibri" Font-Size="Small" HorizontalAlign="Left" 
                        Width="150px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="cmpCoGrupoAtividade" HeaderText="">
                    <ItemStyle Font-Size="Small" HorizontalAlign="Left" Width="10px" />
                    </asp:BoundField>
                </Columns>
                <FooterStyle BackColor="White" Font-Names="Calibri" Font-Size="Small" 
                    ForeColor="#000066" />
                <HeaderStyle BackColor="#006699" Font-Bold="True" Font-Names="Calibri" 
                    Font-Size="Small" ForeColor="White" />
                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                <RowStyle Font-Names="Calibri" Font-Size="Small" ForeColor="#000066" 
                    Height="8" />
                <SelectedRowStyle BackColor="#33CCCC" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                <SortedAscendingHeaderStyle BackColor="#007DBB" />
                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                <SortedDescendingHeaderStyle BackColor="#00547E" />
            </asp:GridView>
        </asp:Panel>

       <asp:Button ID="btnShowPopup2" runat="server" style="display:none" />
        <asp:ModalPopupExtender 
            ID="ModalPopupExtender2" 
            runat="server" 
            TargetControlID="btnShowPopup2" 
            PopupControlID="pnlpopup2"
            BackgroundCssClass="modalBackground"></asp:ModalPopupExtender>
            
              <asp:Panel ID="pnlpopup2" runat="server" BackColor="White"  
                    Height="376px" Width="749px" BorderStyle="Solid" 
                    BorderWidth="1px" style="display:none">
            <asp:Label ID="Label4" runat="server" Text="Equipamento da Obra"
                        style="position:absolute; top: 5px; left: 6px; height: 20px; width: 700px; font-size:medium; text-align: center;" 
                        BackColor="White" Font-Bold="True" Font-Size="Large" 
                        ForeColor="#3333FF" Font-Names="Calibri"></asp:Label>
            <asp:Label ID="lblDescricao" runat="server" Text="Descrição do Equipamento"
                        style="position:absolute; top: 34px; left: 15px; height: 16px; width: 150px;" 
                        BackColor="White" Font-Bold="True" Font-Size= "Small" 
                        ForeColor="#3333FF" Font-Names="Calibri"></asp:Label>
            <asp:TextBox ID="TxtDescricao" runat="server" 
                        style="position:absolute; top: 50px; left: 13px; width: 335px; height: 62px; bottom: 381px;" 
                        Font-Names="Calibri" Font-Size="Small" TextMode="MultiLine"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RvfTxtDescricao" runat="server"  
                            ControlToValidate="TxtDescricao"
                            style="position:absolute; top: 399px; left: 359px; height: 22px; width: 157px; "
                            ErrorMessage="Descrição do equipamento em branco!" 
                            Display="None"></asp:RequiredFieldValidator>
            <asp:Label ID="lblCodigo" runat="server" Text="Código do Equipamento"
                        style="position:absolute; top: 34px; left: 360px; height: 17px; width: 135px;" 
                        BackColor="White" Font-Bold="True" Font-Size= "Small" 
                        ForeColor="#3333FF" Font-Names="Calibri"></asp:Label>
           <asp:TextBox ID="txtCodigoEquip" runat="server" Font-Names="Calibri" Font-Size="Small" 
                        style="position:absolute; top: 50px; left: 360px; width: 189px; height: 15px;"></asp:TextBox>
            <asp:Label ID="lblTipoEquip0" runat="server" Text="Tipo Equipamento"
                        style="position:absolute; top: 34px; left: 559px; height: 15px; width: 113px;" 
                        BackColor="White" Font-Bold="True" Font-Size="Small" 
                        ForeColor="#3333FF" Font-Names="Calibri"></asp:Label>
             <asp:TextBox ID="txtTpEquipamento" runat="server" 
                        style="position:absolute; top: 50px; left: 561px; width: 170px; height: 15px;" 
                        Font-Names="Calibri" Font-Size="Small"></asp:TextBox>
            <asp:Label ID="lblCapacidade" runat="server" Text="Capacidade"
                        style="position:absolute; top: 80px; left: 360px; height: 16px; width: 68px;" 
                        BackColor="White" Font-Bold="True" Font-Size= "Small" 
                        ForeColor="#3333FF" Font-Names="Calibri"></asp:Label>
            <asp:TextBox ID="txtCapacidade" runat="server"
                        style="position:absolute; top: 96px; left: 360px; width: 78px; height: 15px;" 
                        Font-Names="Calibri" Font-Size="Small"></asp:TextBox>
            <asp:Label ID="lblTipoCapacidade" runat="server" Text="Tipo Capacidade"
                        style="position:absolute; top: 80px; left: 450px; height: 17px; width: 106px;" 
                        BackColor="White" Font-Bold="True" Font-Size= "Small" 
                        ForeColor="#3333FF" Font-Names="Calibri"></asp:Label>
            <asp:DropDownList ID="cmbTipoCapacidade" runat="server" Font-Names="Calibri" Font-Size="Small"
                        style="position:absolute; top: 96px; left: 450px; width: 102px; height: 20px;"></asp:DropDownList>
            <asp:Label ID="lblMarca" runat="server" Text="Marca / Modelo"
                        style="position:absolute; top: 80px; left: 562px; height: 17px; width: 98px; right: 272px;" 
                        BackColor="White" Font-Bold="True" Font-Size="Small" 
                        ForeColor="#3333FF" Font-Names="Calibri"></asp:Label>
            <asp:TextBox ID="txtMarcaModelo" runat="server"
                        style="position:absolute; top: 96px; left: 559px; width: 173px; height: 15px;" 
                        Font-Names="Calibri" Font-Size="Small"></asp:TextBox>
            <asp:Label ID="lblNumeroSerie" runat="server" Text="Número de Série"
                        style="position:absolute; top: 129px; left: 360px; height: 17px; width: 98px; right: 470px;" 
                        BackColor="White" Font-Bold="True" Font-Size="Small" 
                        ForeColor="#3333FF" Font-Names="Calibri"></asp:Label>
            <asp:TextBox ID="txtNumeroSerie" runat="server"
                        style="position:absolute; top: 145px; left: 360px; width: 189px; height: 15px;" 
                        Font-Names="Calibri" Font-Size="Small"></asp:TextBox>
            <asp:Label ID="lblNumeroPatrimonio" runat="server" Text="Número do Patrimônio"
                        style="position:absolute; top: 129px; left: 562px; height: 15px; width: 130px; right: 240px;" 
                        BackColor="White" Font-Bold="True" Font-Size="Small" 
                        ForeColor="#3333FF" Font-Names="Calibri"></asp:Label>
            <asp:TextBox ID="txtNumeroPatrimonio" runat="server"
                        style="position:absolute; top: 145px; left: 561px; width: 169px; height: 15px;" 
                        Font-Names="Calibri" Font-Size="Small"></asp:TextBox>
           <asp:Label ID="lblObservacao" runat="server" Text="Observação"
                        style="position:absolute; top: 115px; left: 14px; height: 14px; width: 150px;" 
                        BackColor="White" Font-Bold="True" Font-Size= "Small" 
                        ForeColor="#3333FF" Font-Names="Calibri"></asp:Label>
            <asp:TextBox ID="txtObservacao" runat="server" 
                        style="position:absolute; top: 130px; left: 13px; width: 335px; height: 69px;" 
                        Font-Names="Calibri" Font-Size="Small" TextMode="MultiLine"></asp:TextBox>
            <asp:Label ID="lblPavimento" runat="server" Text="Pavimento"
                        style="position:absolute; top: 172px; left: 360px; height: 17px; width: 98px;" 
                        BackColor="White" Font-Bold="True" Font-Size= "Small" 
                        ForeColor="#3333FF" Font-Names="Calibri"></asp:Label>
            <asp:DropDownList ID="cmbPavimento" runat="server" Font-Names="Calibri" Font-Size="Small"
                        style="position:absolute; top: 188px; left:360px; width: 229px; height: 20px;"></asp:DropDownList>
            <asp:Label ID="lblLocalizacao" runat="server" Text="Localização"
                        style="position:absolute; top: 220px; left: 360px; height: 16px; width: 111px;" 
                        BackColor="White" Font-Bold="True" Font-Size= "Small" 
                        ForeColor="#3333FF" Font-Names="Calibri"></asp:Label>
            <asp:TextBox ID="txtLocalizacao" runat="server"
                        style="position:absolute; top: 238px; left: 360px; width: 365px; height: 18px;" 
                        Font-Names="Calibri" Font-Size="Small"></asp:TextBox>

            <asp:Label ID="Label5" runat="server" Text="TAG"
                        style="position:absolute; top: 264px; left: 360px; height: 16px; width: 111px;" 
                        BackColor="White" Font-Bold="True" Font-Size= "Small" 
                        ForeColor="#3333FF" Font-Names="Calibri"></asp:Label>
            <asp:TextBox ID="txtTag" runat="server"
                        style="position:absolute; top: 285px; left: 360px; width: 365px; height: 18px;" 
                        Font-Names="Calibri" Font-Size="Small"></asp:TextBox>

            <asp:Button ID="btnUpdate" CommandName="Update" runat="server" Text="Gravar" 
                        style="position:absolute; top: 339px; left: 567px; width: 70px; height: 30px;" 
                        onclick="btnUpdate_Click"/>
            <asp:Button ID="btnCancel" runat="server" Text="Cancelar" CausesValidation="false"
                        style="position:absolute; top: 338px; left: 651px; height: 30px;" 
                        onclick="btnCancel_Click" />
            <asp:Image ID="imgQrCode" runat="server" BorderStyle="Double" 
                ImageUrl="~/QRCODE/Orion.Jpeg" 
                
                      style="position:absolute; top: 217px; left: 21px; height: 130px; width: 135px;" />
            <asp:Button ID="btnGerarQRCode" CommandName="GerarQR" runat="server" onclick="btnGerarQRCode_Click" 
                style="position:absolute; top: 225px; left: 174px; width: 137px; height: 26px;" 
                Text="Gerar QR Code" />

                  <asp:Button ID="btnImprimirQR" CommandName="ImprimirQR" runat="server" 
                style="position:absolute; top: 281px; left: 174px; width: 137px; height: 30px;" 
                Text="Imprimir QR Code" onclick="btnImprimirQR_Click" />
        </asp:Panel>

        <asp:ModalPopupExtender 
            ID="ModalPopupExtender3" 
            runat="server" 
            TargetControlID="Button5" 
            PopupControlID="pnlOS"
            CancelControlID="Button5" 
            BackgroundCssClass="modalBackground"></asp:ModalPopupExtender>

                 <asp:Panel ID="pnlOS" runat="server" BackColor="White"  
                    Height="220px" Width="350px" BorderStyle="Solid" 
                    BorderWidth="1px" style="display:yes">
                     <asp:Button ID="Button5" runat="server" Text="Fechar"  
                         Width="63px" />
                     <asp:GridView ID="grvOS" runat="server" AutoGenerateColumns="False" 
                         Width="322px" CellPadding="4" ForeColor="#333333" GridLines="None" 
                         DataKeyNames="cmpIdOS" onrowcommand="grvOS_RowCommand">
                         <AlternatingRowStyle BackColor="White" />
                         <Columns>
                             <asp:ButtonField HeaderText="OS" Text="Abrir" CommandName="Select" >
                             <HeaderStyle Width="20px" />
                             <ItemStyle Width="20px" />
                             </asp:ButtonField>
                             <asp:HyperLinkField DataTextField="cmpNuOS" HeaderText="Num. OS">
                             <HeaderStyle Width="20px" />
                             </asp:HyperLinkField>
                             <asp:BoundField DataField="cmpDtAbertura" HeaderText="Data Abertura" >
                             <HeaderStyle Width="15px" />
                             </asp:BoundField>
                         </Columns>
                         <EditRowStyle BackColor="#2461BF" />
                         <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                         <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                         <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                         <RowStyle BackColor="#EFF3FB" />
                         <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                         <SortedAscendingCellStyle BackColor="#F5F7FB" />
                         <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                         <SortedDescendingCellStyle BackColor="#E9EBEF" />
                         <SortedDescendingHeaderStyle BackColor="#4870BE" />
                     </asp:GridView>
                   
                    </asp:Panel>
    </div>

</asp:Content>

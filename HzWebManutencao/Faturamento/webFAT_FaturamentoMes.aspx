<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="webFAT_FaturamentoMes.aspx.cs" Inherits="HzWebManutencao.Faturamento.webFAT_FaturamentoMes" %>
<%@ Register src="~/Controles/Mensagem.ascx" tagname="Mensagem" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script language="javascript" type="text/javascript">
        function divexpandcollapse(divname) {
            var div = document.getElementById(divname);
            var img = document.getElementById('img' + divname);

            if (div.style.display == "none") {
                div.style.display = "inline";
                img.src = "../App_Themes/General/minus.gif";
            } else {
                div.style.display = "none";
                img.src = "../App_Themes/General/plus.gif";
            }
        }
 </script>        
<asp:Mensagem ID="CaixaMensagem" runat="server" />
<div runat="server" id="divstyle" style="position:relative; height: 464px; width: 1162px; top: 6px; left: -54px; margin-left: 40px;">
        <asp:Label ID="lblAcao" runat="server" Text="Faturamento GrupoOrion"
            style="position:absolute; top: 4px; left: 6px; height: 21px; width: 1150px; text-align: center;" 
            BackColor="White" Font-Bold="True" Font-Size="Large" ForeColor="Maroon" 
            Font-Names="Calibri"></asp:Label>

        <asp:Label ID="Label5" runat="server" Text="Área de Negócio" Font-Size= "Small"
            style="position:absolute; top: 27px; left: 8px; height: 15px; width: 163px;"></asp:Label>
        <asp:DropDownList ID="cmbTipoServico" runat="server" Font-Size="Small"
            style="position:absolute; top: 45px; left: 8px; width: 461px;"
            onselectedindexchanged="cmbTipoServico_SelectedIndexChanged" 
            AutoPostBack="True" Enabled="False">
        </asp:DropDownList>

        <asp:Label ID="Label1" runat="server" Text="Obra" style="position:absolute; top: 27px; left: 477px; width: 29px;">
        </asp:Label>

        <asp:DropDownList ID="cmbObra" runat="server" 
            style="position:absolute; top: 45px; left: 477px; width: 442px;"
            AutoPostBack="True" onselectedindexchanged="cmbObra_SelectedIndexChanged" 
            Enabled="False" >
        </asp:DropDownList>

        <asp:Label ID="lblMes" runat="server" Text="Mês" Font-Size="Small"
            
            style="position:absolute; top: 26px; left: 926px; height: 15px; width: 27px;"> </asp:Label>
        <asp:DropDownList ID="cmbMes" runat="server" Font-Size="Small"
            
            
            style="position:absolute; top: 44px; left: 925px; width: 120px;" onselectedindexchanged="btnPesquisar_Click"
            AutoPostBack="True" Enabled="False">
        </asp:DropDownList>

        <asp:Label ID="lblEmiFatura" runat="server" Text="Emissão Fatura" Font-Size="Small"
            
            style="position:absolute; top: 81px; left: 10px; height: 15px; width: 100px;"> </asp:Label>

        <asp:CheckBoxList ID="chkEmissaoFatura" runat="server" style="position:absolute; top: 98px; left: 8px; width: 228px; height: 28px;" 
            onselectedindexchanged="btnPesquisar_Click"  AutoPostBack="True" 
             RepeatColumns="3"  RepeatDirection="Horizontal" BorderStyle="Solid" 
            Enabled="False">
            <asp:ListItem Value="A">Em Aberto</asp:ListItem>
            <asp:ListItem Value="E">Emitido</asp:ListItem>
            <asp:ListItem Value="P">Pendente</asp:ListItem>
        </asp:CheckBoxList>

<%--
        <asp:DropDownList ID="cmbEmiFatura" runat="server" Font-Size="Small"
            style="position:absolute; top: 45px; left: 627px; width: 125px;" onselectedindexchanged="btnPesquisar_Click"
            AutoPostBack="True">
        </asp:DropDownList>--%>

        <asp:Label ID="lblEntFatura" runat="server" Text="Entrega Fatura" Font-Size="Small"
            style="position:absolute; top: 81px; left: 244px; height: 15px; width: 117px;"> </asp:Label>
        <asp:CheckBoxList ID="chkEntregaFatura" runat="server" style="position:absolute; top: 98px; left: 242px; width: 228px; height: 28px;" 
            onselectedindexchanged="btnPesquisar_Click"  AutoPostBack="True" 
             RepeatColumns="3"  RepeatDirection="Horizontal" BorderStyle="Solid">
            <asp:ListItem Value="A">Em Aberto</asp:ListItem>
            <asp:ListItem Value="E">Emitido</asp:ListItem>
            <asp:ListItem Value="P">Pendente</asp:ListItem>
        </asp:CheckBoxList>


<%--        <asp:DropDownList ID="cmbEntFatura" runat="server" Font-Size="Small"
            style="position:absolute; top: 45px; left: 760px; width: 125px;" onselectedindexchanged="btnPesquisar_Click"
            AutoPostBack="True">
        </asp:DropDownList>
--%>
        <asp:Label ID="lblRecFatura" runat="server" Text="Recebimento Fatura" Font-Size="Small"
            
            style="position:absolute; top: 81px; left: 477px; height: 15px; width: 183px;"> </asp:Label>

        <asp:CheckBoxList ID="chkRecebeFatura" runat="server" style="position:absolute; top: 98px; left: 477px; width: 288px; height: 28px;" 
            onselectedindexchanged="btnPesquisar_Click"  AutoPostBack="True" 
             RepeatColumns="4"  RepeatDirection="Horizontal" BorderStyle="Solid" 
            Enabled="False">
            <asp:ListItem Value="RA">Em Aberto</asp:ListItem>
            <asp:ListItem Value="RT">Total</asp:ListItem>
            <asp:ListItem Value="RP">Parcial</asp:ListItem>
            <asp:ListItem Value="PE">Pendente</asp:ListItem>
        </asp:CheckBoxList>

<%--        <asp:DropDownList ID="cmbRecFatura" runat="server" Font-Size="Small"
            style="position:absolute; top: 45px; left: 892px; width: 149px;" onselectedindexchanged="btnPesquisar_Click"
            AutoPostBack="True">
        </asp:DropDownList>
--%>
        <asp:Label ID="lblAno" runat="server" Text="Ano" Font-Size="Small"
            
            style="position:absolute; top: 26px; left: 1055px; height: 15px; width: 24px;"> </asp:Label>

        <asp:DropDownList ID="cmbAno" runat="server" 
            style="position:absolute; top: 45px; left: 1054px; width: 90px;" onselectedindexchanged="btnPesquisar_Click"
            AutoPostBack="True" Enabled="False">
        </asp:DropDownList>

        <asp:Button ID="btnPesquisar" runat="server" Text="Pesquisar" 
            CausesValidation="False" Font-Bold="True"
                style="position:absolute; top: 95px; left: 1038px; width: 107px; height: 30px; right: 17px;" 
                onclick="btnPesquisar_Click" Enabled="False"/>

          <asp:GridView ID="gvDados" runat="server" 
            style="position:absolute; top: 143px; left: 3px; width: 1152px;" 
                    BackColor="LightGoldenrodYellow" BorderColor="Tan" BorderWidth="5px" 
                    ShowFooter="True" CellPadding="3" ForeColor="Black" 
                    GridLines="None" BorderStyle="None" CellSpacing="2" Font-Size="X-Small" 
                    AutoGenerateColumns="False"
                    OnRowCreated="gvDados_RowCreated" 
            onrowcommand="gvDados_RowCommand" onrowdatabound="gvDados_RowDataBound">
            <FooterStyle BackColor="Tan" Font-Bold="True" HorizontalAlign="Right" />
            <SelectedRowStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
            <PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" HorizontalAlign="Left" />
            <HeaderStyle BackColor="Tan" Font-Bold="True" />
            <AlternatingRowStyle BackColor="PaleGoldenrod" />
            <Columns>
                <asp:TemplateField HeaderText="Fatura">
                <ItemStyle Width="20px" HorizontalAlign="Left" Font-Size="X-Small" />
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkFatura" runat="server" Text="Editar" CommandArgument='<%# Eval("cmpIdFaturaObra") %>' CommandName="lnkFatura"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>                
                
                <asp:TemplateField HeaderText="Entrega">
                <ItemStyle Width="20px" HorizontalAlign="Left" Font-Size="X-Small" />
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkEntregaCarta" runat="server" Text="Editar"
                        CommandArgument='<%# Eval("cmpIdFaturaObra") %>' CommandName="lnkEntregaCarta"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField> 

                <asp:TemplateField HeaderText="Recebe">
                    <ItemStyle Width="15px" HorizontalAlign="Left" Font-Size="X-Small" />
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkRecebe" runat="server" Text="Editar" CommandArgument='<%# Eval("cmpIdFaturaObra") %>' CommandName="lnkRecebe"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>   

                <asp:TemplateField ItemStyle-Width="10px">
                    <ItemTemplate>
                        <a href="JavaScript:divexpandcollapse('div<%# Eval("cmpIdFaturaObra") %>');">
                            <img id="imgdiv<%# Eval("cmpIdFaturaObra") %>" width="9px" border="0" src="../App_Themes/General/plus.gif" />
                        </a>
                    </ItemTemplate>
                </asp:TemplateField>
 
                 <asp:BoundField DataField="cmpNoObraFatura" HeaderText="Nome da Obra">
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle Width="200px" HorizontalAlign="Left" Font-Size="X-Small" />
                </asp:BoundField>

                <asp:BoundField DataField="cmpDcTipoServico"       HeaderText="Área de Negócio">
                    <ItemStyle Width="200px" HorizontalAlign="Left" Font-Size="X-Small" />
                </asp:BoundField>

                <asp:TemplateField HeaderText="Valor">
                  <ItemStyle Width="30px" HorizontalAlign="Right" Font-Size="X-Small" />
                    <ItemTemplate>
                        <asp:Label ID="lblValor" runat="server" Text='<%#Eval("cmpVlFaturado","{0:N2}")%>'/>
                     </ItemTemplate>
                     <FooterTemplate>
                        <asp:Label ID="lblValorTotal" runat="server" />
                     </FooterTemplate>                   
                  </asp:TemplateField>

                <asp:BoundField DataField="cmpDtPrevEmissaoNota" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Data">
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle Width="20px" HorizontalAlign="Center" Font-Size="X-Small" />
                </asp:BoundField>
                <asp:BoundField DataField="cmpSitNota" HeaderText="Situação"  >
                <HeaderStyle  HorizontalAlign="Center" />
                <ItemStyle Width="30px" HorizontalAlign="Left" Font-Size="X-Small"/>
                </asp:BoundField>

                <asp:BoundField DataField="cmpDtPrevEntregaDocObra" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Data" >
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle Width="20px" HorizontalAlign="Center" Font-Size="X-Small"/>
                </asp:BoundField>
                <asp:BoundField DataField="cmpSitDocObra" HeaderText="Situação">
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle Width="30px" HorizontalAlign="Left" Font-Size="X-Small"/>
                </asp:BoundField>

<%--                <asp:BoundField DataField="cmpVlRecebido" HeaderText="Valor">
                <HeaderStyle CssClass="Cell" HorizontalAlign="Center" />
                <ItemStyle Width="30px" HorizontalAlign="Left" Font-Size="X-Small"/>
                </asp:BoundField>--%>

               <asp:TemplateField HeaderText="Valor">
                  <ItemStyle Width="30px" HorizontalAlign="Right" Font-Size="X-Small" />
                    <ItemTemplate>
                        <asp:Label ID="lblValorRecebido" runat="server" Text='<%#Eval("cmpVlRecebido","{0:N2}")%>'/>
                     </ItemTemplate>
                     <FooterTemplate>
                        <asp:Label ID="lblValorTotalRecebido" runat="server" />
                     </FooterTemplate>                   
                  </asp:TemplateField>

                <asp:BoundField DataField="cmpDtPrevRecebeNota" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Data" >
                <HeaderStyle  HorizontalAlign="Center" />
                <ItemStyle Width="20px" HorizontalAlign="Center" Font-Size="X-Small"/>
                </asp:BoundField>

                <asp:BoundField DataField="cmpSitRecNota" HeaderText="Situação">
                <HeaderStyle CssClass="Cell" HorizontalAlign="Center" />
                <ItemStyle Width="30px" HorizontalAlign="Left" Font-Size="X-Small"/>
                </asp:BoundField>

                <asp:TemplateField>
                    <ItemStyle Width="1px"/>
                    <ItemTemplate>
                        <tr>
                            <td colspan="100%">
                                <div id="div<%# Eval("cmpIdFaturaObra") %>" style="display: none; position: relative; left: 150px; overflow: auto">
                                    <asp:GridView ID="gvChildGrid" runat="server" AutoGenerateColumns="false" BorderStyle="Double" BorderColor="White" 
                                        CellPadding="3" CellSpacing="2" GridLines="Both" Width="450px">
                                        <HeaderStyle BackColor="#df5015" Font-Bold="true" ForeColor="White"  HorizontalAlign="Center"/>
                                        <RowStyle BackColor="White"/>
                                        <AlternatingRowStyle BackColor="Gainsboro" />
                                        <Columns>
                                            <asp:BoundField DataField="cmpnunotafiscal"         HeaderText="Nota Fiscal"     >
                                                <ItemStyle Width="50px" HorizontalAlign="Right" />
                                            </asp:BoundField>

                                            <asp:BoundField DataField="cmpdtemissaonotafiscal"  HeaderText="Emissão NF"       DataFormatString="{0:dd/MM/yyyy}">
                                                <ItemStyle Width="30px" HorizontalAlign="Center" />
                                            </asp:BoundField>

                                            <asp:BoundField DataField="cmpvlnota"               HeaderText="Valor Faturado"  DataFormatString="{0:N2}" >
                                                <ItemStyle Width="60px" HorizontalAlign="Right" />
                                            </asp:BoundField>

                                            <asp:BoundField DataField="cmpVlGlosadoNota"       HeaderText="Valor Glosado"  DataFormatString="{0:N2}">
                                                <ItemStyle Width="60px" HorizontalAlign="Right" />
                                            </asp:BoundField>

                                            <asp:BoundField DataField="cmpDtRecebimento"  HeaderText="Data Recebimento"       DataFormatString="{0:dd/MM/yyyy}">
                                                <ItemStyle Width="30px" HorizontalAlign="Center" />
                                            </asp:BoundField>

                                            <asp:BoundField DataField="cmpvlrecebidonota"       HeaderText="Valor Recebido"  DataFormatString="{0:N2}">
                                                <ItemStyle Width="60px" HorizontalAlign="Right" />
                                            </asp:BoundField>

                                            <asp:BoundField DataField="SituacaoRecebimento"       HeaderText="Situação Recebimento"  DataFormatString="{0:N2}">
                                                <ItemStyle Width="100px" HorizontalAlign="Left" />
                                            </asp:BoundField>

                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:TemplateField>

            </Columns>
        </asp:GridView>

    </div>
</asp:Content>
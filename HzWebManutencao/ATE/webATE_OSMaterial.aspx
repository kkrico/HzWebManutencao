<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="webATE_OSMaterial.aspx.cs" Inherits="HzWebManutencao.ATE.webATE_OSMaterial" %>
<%@ Register Src="~/Controles/Mensagem.ascx" TagName="Mensagem" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
<%--    <script src="../Scripts/jquery-ui-1.8.11.custom.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.MetaData.js" type="text/javascript"></script>
    <script src="../Scripts/autoNumeric-1.7.5.js" type="text/javascript"></script>
    <script type="text/javascript">
     $(document).ready
     (function () 
        {
            $("#<%= txtValorMaterial.ClientID %>").autoNumeric(
            { aSep: ''.'', aDec: '','', aSign: '''', mDec: ''2'',
                vMax: ''99999.99'' }
            );
        }
      );
    </script>--%>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Mensagem ID="CaixaMensagem" runat="server" />
    <div  style="position:relative; height: 489px; width: 992px; top: 6px; left: -54px; margin-left: 40px;">
        <asp:Panel ID="pnlMaterial" runat="server" 
            style="position:absolute; left:9px; width:979px; height:133px; top: 2px;">
            <asp:Label ID="lblAcao" runat="server" Text="lblAcao"
                style="position:absolute; top: 0px; left: 11px; height: 20px; width: 964px;" 
                BackColor="White" Font-Bold="True" Font-Size="Medium" ForeColor="Maroon">
            </asp:Label>

            <asp:Label ID="lblObra" runat="server" Text="Obra "
                style="position:absolute; top: 24px; left: 12px; width: 40px;" 
                Font-Names="Calibri" Font-Size="Small"></asp:Label>
            <asp:TextBox ID="TxtObra" runat="server"
                style="position:absolute; top: 42px; left: 12px; height: 15px; width: 380px;" 
                Font-Size="Small" Font-Names="Calibri"></asp:TextBox>

            <asp:Label ID="lblMaterial" runat="server" Text="Material "
                style="position:absolute; top: 24px; left: 409px; width: 51px;">
            </asp:Label>
            <asp:TextBox ID="txtItemMaterial" runat="server"
                style="position:absolute; top: 42px; left: 407px; height: 15px; width: 303px;" 
                Font-Size="Small" Font-Names="Calibri"></asp:TextBox>
 
            <asp:Button ID="btnPesqMaterial" runat="server" Text="Pesquisar Material" 
                style="position:absolute; top: 38px; left: 725px; height: 25px; width: 145px;" 
                onclick="btnPesqMaterial_Click" CausesValidation="false"/>

            <asp:Label ID="lblMatSelecionado" runat="server" Text="Material Selecionado"
                style="position:absolute; top: 65px; left: 12px; height: 17px; width: 156px;">
            </asp:Label>
            <asp:TextBox ID="txtMaterialSelecionado" runat="server" Enabled="false"
                style="position:absolute; top: 85px; left: 12px; height: 46px; width: 380px;" 
                Font-Size="Small" Font-Names="Calibri" TextMode="MultiLine"></asp:TextBox>

            <asp:Label ID="lblQtMaterial" runat="server" Text="Quantidade "
                style="position:absolute; top: 65px; left: 590px; height: 17px; width: 80px;">
            </asp:Label>
            <asp:TextBox ID="txtQtMaterial" runat="server"  
                style="position:absolute; top: 85px; left: 590px; height: 17px; width: 119px; text-align:right"></asp:TextBox>
                <asp:MaskedEditExtender ID="MaskedEditExtender4"    
                    InputDirection ="RightToLeft"
                    DisplayMoney="None"
                    runat="server"
                    TargetControlID="txtQtMaterial" 
                    Mask="999999,99"
                    MessageValidatorTip="true" 
                    OnFocusCssClass="MaskedEditFocus" 
                    OnInvalidCssClass="MaskedEditError"
                    MaskType =  "Number" 
                    AcceptNegative= "None" 
                    ErrorTooltipEnabled="True"/>
 
            <asp:Label ID="lblValorMaterial" runat="server" Text="Valor Material "
                style="position:absolute; top: 65px; left: 410px; height: 17px; width: 100px;">
            </asp:Label>
            <asp:TextBox ID="txtValorMaterial" runat="server"  
                style="position:absolute; top: 85px; left: 409px; height: 17px; width: 150px; text-align:right"></asp:TextBox>
                <asp:MaskedEditExtender ID="MaskedEditExtender3"    
                    InputDirection ="RightToLeft"
                    DisplayMoney="None"
                    runat="server"
                    TargetControlID="txtValorMaterial" 
                    Mask="999999,99"
                    MessageValidatorTip="true" 
                    OnFocusCssClass="MaskedEditFocus" 
                    OnInvalidCssClass="MaskedEditError"
                    MaskType =  "Number" 
                    AcceptNegative= "None" 
                    ErrorTooltipEnabled="True"/>

             <asp:CheckBox ID="chkControle" runat="server" Text="Controle" 
                style="position:absolute; top: 115px; left: 406px; height: 23px; width: 75px;"/>

            <asp:Button ID="btnGravar" runat="server" Text="Gravar" 
                style="position:absolute; top: 95px; left: 725px; height: 25px; width: 68px;" 
                onclick="btnGravar_Click"/>
        </asp:Panel>

        <asp:Button ID="BtnVoltar" runat="server" Text="Voltar" 
            style="position:absolute; top: 97px; left:810px; height: 25px; width: 71px;" 
            CausesValidation="False" onclick="BtnVoltar_Click"/>

        <asp:Label ID="lblTotalMaterial" runat="server" Text="Total Material"
                    style="position:absolute; top: 151px; left: 650px; height: 20px; width: 271px; font-size:medium" 
                    BackColor="White" Font-Bold="True" Font-Size="Small" Visible="false" 
            ForeColor="#3333FF"></asp:Label>

        <asp:GridView ID="grdOSMaterial" runat="server" 
            style="position:absolute; top: 180px; left: 15px; width: 950px;" AllowPaging="True" 
                AllowSorting="True" 
                AutoGenerateColumns="False" 
                onpageindexchanging="grdOSMaterial_PageIndexChanging" 
                CellPadding="3" 
                Visible="False" BackColor="White" BorderColor="#003366" 
                BorderStyle="Inset" BorderWidth="1px" 
                ViewStateMode="Enabled" 
                onrowcommand="grdOSMaterial_RowCommand" 
                onrowdatabound="grdOSMaterial_RowDataBound" 
                ondatabound="grdOSMaterial_DataBound"
                PageSize="6">
                <Columns>
 <%--                   <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkEditar" runat="server" Text="Editar" 
                                CommandArgument='<%# Eval("cmpIdOsMaterial") %>' CommandName="editar"                             
                                OnClick="lnkEditar_Click" CausesValidation="False">
                            </asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                    </asp:TemplateField>
--%>
                    <asp:TemplateField ShowHeader="False">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkDelete" runat="server" Text="Excluir" 
                                CommandArgument='<%# Eval("cmpIdOsMaterial") %>' CommandName="excluir" 
                                CausesValidation="False"></asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                    </asp:TemplateField>
                    
                    <asp:BoundField DataField="cmpDcItem" HeaderText="Item">
                      <ItemStyle HorizontalAlign="Left" Width="30px" VerticalAlign="Middle" />
                    </asp:BoundField>

                    <asp:BoundField DataField="cmpDcMaterialObraGrupo" HeaderText="Material">
                        <ItemStyle HorizontalAlign="Left" Width="720px" VerticalAlign="Middle" Wrap="True" />
                    </asp:BoundField>
                       
                    <asp:BoundField DataField="cmpDcUnidade" HeaderText="Unid.">
                      <ItemStyle Width="8px" HorizontalAlign="Center" VerticalAlign="Middle"/>
                    </asp:BoundField>

                    <asp:BoundField DataField="cmpNuQtdContrato" HeaderText="Qt. Contrato" 
                        DataFormatString="{0:f0}" HtmlEncode="False">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle Width="20px" HorizontalAlign="Right" VerticalAlign="Middle" />
                    </asp:BoundField>

                    <asp:BoundField DataField="QTDESTOQUE" HeaderText="Estoque" DataFormatString="{0:n2}" HtmlEncode="False">
                        <ItemStyle Width="20px" HorizontalAlign="Right" VerticalAlign="Middle" />
                    </asp:BoundField>

                    <asp:BoundField DataField="cmpVlMaterial" HeaderText="Valor Material" DataFormatString="{0:n2}" HtmlEncode="False">
                        <ItemStyle Width="30px" HorizontalAlign="Right" />
                    </asp:BoundField>

                    <asp:BoundField DataField="cmpQtMaterial" HeaderText="Qt. Material" DataFormatString="{0:n2}" HtmlEncode="False">
                        <ItemStyle HorizontalAlign="Right" Width="30px" VerticalAlign="Middle" />
                    </asp:BoundField>
                        
                    <asp:BoundField DataField="cmpVlSubTotal" HeaderText="Valor Total" DataFormatString="{0:n2}" HtmlEncode="False">
                        <ItemStyle HorizontalAlign="right" Width="100px" VerticalAlign="Middle" />
                    </asp:BoundField>

                     <asp:BoundField DataField="cmpInControle" HeaderText="Email">
                        <ItemStyle Width="8px" HorizontalAlign="Center" VerticalAlign="Middle"/>
                    </asp:BoundField>

                </Columns>
                <FooterStyle BackColor="White" ForeColor="#000066"/>
                <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="White" ForeColor="#000066"/>
                <RowStyle ForeColor="#000066" Font-Names="Calibri" Font-Size="Small" />
                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                <SortedAscendingHeaderStyle BackColor="#007DBB" />
                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                <SortedDescendingHeaderStyle BackColor="#00547E" />
            </asp:GridView>

            <asp:Button ID="btnShowPopup1" runat="server" style="display:none" />
                <asp:ModalPopupExtender 
                    ID="ModalPopupExtender1" 
                    runat="server" 
                    TargetControlID="btnShowPopup1" 
                    PopupControlID="pnlpopup1"
                    BackgroundCssClass="modalBackground"></asp:ModalPopupExtender>

            <asp:Panel ID="pnlpopup1" runat="server" BackColor="White" Height="552px" Width="850px" BorderStyle="Solid" 
                        BorderWidth="1px"
                        style="display:none">
                <asp:Label ID="Label2" runat="server" Text="Material de Referência Orion"
                            style="position:absolute; top: 5px; left: 40px; height: 20px; width: 800px; font-size:medium; text-align: center;" 
                            BackColor="White" Font-Bold="True" Font-Size="Large" 
                            ForeColor="#3333FF" Font-Names="Calibri"></asp:Label>
                <asp:Button ID="btnRetornar" runat="server" Text="Voltar" 
                    style="position:absolute; top:20px; left:40px; height: 25px; width: 69px;" 
                    CausesValidation="False" onclick="btnRetornar_Click"/>

                <asp:GridView ID="grdMaterialOrion" runat="server"  
                    style="position:absolute; top: 50px; left: 40px; width: 791px;"
                    AutoGenerateColumns="False"
                    ViewStateMode="Enabled" 
                    BorderColor="#003366" BorderStyle="Inset" BorderWidth="1px"
                    AllowPaging="True" 
                    AllowSorting="True" 
                    CellPadding="3"
                    BackColor="White" Font-Size="Small" PageSize="9" 
                    onrowdatabound="grdMaterialOrion_RowDataBound" 
                    Font-Names="Calibri" 
                    onpageindexchanging="grdMaterialOrion_PageIndexChanging">
                    <Columns>
                        <asp:TemplateField HeaderText="Seleção" >
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkSelecao" runat="server" Text="Selecionar" 
                                    CommandArgument='<%# Eval("cmpCoObraGrupoMaterial") %>' 
                                    CommandName="selecao"
                                    OnClick="lnkSelecao_Click"
                                    CausesValidation="False">
                                </asp:LinkButton>
                            </ItemTemplate>
                            <HeaderStyle Font-Names="Calibri" Font-Size="Small" />
                            <ItemStyle Width="20px" HorizontalAlign="Left" VerticalAlign="Middle" 
                                Font-Names="Calibri" Font-Size="Small" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="cmpDcMaterial" HeaderText="Descrição Material">
                            <HeaderStyle Font-Names="Calibri" Font-Size="Small"/>
                            <ItemStyle Width="400px" HorizontalAlign="Left" Font-Size="Small" Font-Names="Calibri"/>
                        </asp:BoundField>
                         <asp:BoundField DataField="cmpDcUnidade" HeaderText="Unid.">
                            <HeaderStyle Font-Names="Calibri" Font-Size="Small"/>
                            <ItemStyle Width="50px" HorizontalAlign="Right" Font-Size="Small" Font-Names="Calibri"/>
                        </asp:BoundField>
                        <asp:BoundField DataField="QtContrato" HeaderText="Qt. Contrato">
                            <HeaderStyle Font-Names="Calibri" Font-Size="Small"/>
                            <ItemStyle Width="50px" HorizontalAlign="Right" Font-Size="Small" Font-Names="Calibri"/>
                        </asp:BoundField>
                        <asp:BoundField DataField="EstoqueAtual" HeaderText="Est. Atual">
                            <HeaderStyle Font-Names="Calibri" Font-Size="Small"/>
                            <ItemStyle Width="50px" HorizontalAlign="Right" Font-Size="Small" Font-Names="Calibri"/>
                        </asp:BoundField>
                        <asp:BoundField DataField="cmpVlPrecoUnitario" HeaderText="Vl. Unitário">
                            <HeaderStyle Font-Names="Calibri" Font-Size="Small"/>
                            <ItemStyle Width="100px" HorizontalAlign="Right" Font-Size="Small" Font-Names="Calibri"/>
                        </asp:BoundField>
<%--                       <asp:BoundField DataField="DcMaterial" HeaderText="Descrição Material">
                        </asp:BoundField>
--%>                    </Columns>
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

            </asp:Panel>

            <asp:Button ID="btnShowPopup2" runat="server" style="display:none" />
                <asp:ModalPopupExtender 
                    ID="ModalPopupExtender2" 
                    runat="server" 
                    TargetControlID="btnShowPopup2" 
                    PopupControlID="pnlpopup2"
                    BackgroundCssClass="modalBackground"></asp:ModalPopupExtender>

            <asp:Panel ID="pnlpopup2" runat="server" BackColor="White" Height="250px" Width="561px" style="display:none" 
                        BorderStyle="Solid" BorderWidth="1px" >
                <asp:Label ID="Label3" runat="server" Text="Alterar Material " 
                            style="position:absolute; top: 11px; left: 198px; height: 20px; width: 136px; font-size:medium" 
                            BackColor="White" Font-Bold="True" Font-Size="Small" 
                    ForeColor="#3333FF"></asp:Label>
                <asp:Label ID="Label4" runat="server" Text="Material:"
                            style="position:absolute; top: 34px; left: 17px; height: 20px; width: 66px;" 
                            BackColor="White" Font-Bold="True" Font-Size="Small" 
                            ForeColor="#3333FF"></asp:Label>
                <asp:Label ID="lblDadosMaterial" runat="server"
                            style="position:absolute; top: 34px; left: 85px; height: 70px; width: 470px;" 
                            BackColor="White" Font-Bold="True" Font-Size= "Small" 
                            ForeColor="#3333FF"></asp:Label>
                <asp:Label ID="Label5" runat="server" Text="Quantidade "
                            style="position:absolute; top: 118px; left: 23px; height: 20px; width: 80px;" 
                            BackColor="White" Font-Bold="True" Font-Size="Small" 
                            ForeColor="#3333FF"></asp:Label>
                <asp:TextBox ID="txtQuantiMaterial" runat="server"
                            style="position:absolute; top: 141px; left: 20px; width: 133px; height: 20px;"></asp:TextBox>
                            <asp:MaskedEditExtender ID="MaskedEditExtender1"    
                                InputDirection ="RightToLeft"
                                DisplayMoney="None"
                                runat="server"
                                TargetControlID="txtQuantiMaterial" 
                                Mask="999999,99"
                                MessageValidatorTip="true" 
                                OnFocusCssClass="MaskedEditFocus" 
                                OnInvalidCssClass="MaskedEditError"
                                MaskType =  "Number" 
                                AcceptNegative= "None" 
                                ErrorTooltipEnabled="True"/>

                <asp:Label ID="lblVlMat" runat="server" Text="Valor Material "
                            style="position:absolute; top: 120px; left: 177px; height: 16px; width: 100px;" 
                            BackColor="White" Font-Bold="True" Font-Size="Small" 
                            ForeColor="#3333FF"></asp:Label>
                <asp:TextBox ID="txtVlMat" runat="server"
                            style="position:absolute; top: 140px; left: 173px; width: 131px; height: 20px;"></asp:TextBox>
                            <asp:MaskedEditExtender ID="MaskedEditExtender5"    
                                InputDirection ="RightToLeft"
                                DisplayMoney="None"
                                runat="server"
                                TargetControlID="txtVlMat" 
                                Mask="999999,99"
                                MessageValidatorTip="true" 
                                OnFocusCssClass="MaskedEditFocus" 
                                OnInvalidCssClass="MaskedEditError"
                                MaskType =  "Number" 
                                AcceptNegative= "None" 
                                ErrorTooltipEnabled="True"/>
                <asp:CheckBox ID="chkControle2" runat="server" Text="Controle" 
                            style="position:absolute; top: 140px; left: 323px; height: 23px; width: 75px;"
                            BackColor="White" Font-Bold="True" Font-Size="Small" 
                            ForeColor="#3333FF"/>

                <asp:Button ID="btnUpdate" CommandName="Update" runat="server" Text="Gravar"
                            style="position:absolute; top: 200px; left: 10px; width: 70px; height: 30px;" 
                             onclick="btnUpdate_Click"/>
                <asp:Button ID="btnCancel1" runat="server" Text="Cancelar" CausesValidation="false"
                    style="position:absolute; top: 200px; left: 90px; width: 70px; height: 30px;" 
                    onclick="btnCancel1_Click" />
            </asp:Panel>

    </div>
</asp:Content>

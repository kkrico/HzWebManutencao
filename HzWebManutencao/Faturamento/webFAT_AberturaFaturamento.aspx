<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="webFAT_AberturaFaturamento.aspx.cs" Inherits="HzWebManutencao.Faturamento.webFAT_AberturaFaturamento" %>
<%@ Register src="~/UserControls/OKMessageBox.ascx" tagname="MsgBox" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<asp:MsgBox ID="MsgBox" runat="server" />
<div runat="server" id="divstyle" style="position:relative; height: 464px; width: 1162px; top: 6px; left: -54px; margin-left: 40px;">
        <asp:Label ID="lblAcao" runat="server" Text="Abertura de Faturamento GrupoOrion"
            style="position:absolute; top: 4px; left: 6px; height: 21px; width: 1152px; text-align: center;" 
            BackColor="White" Font-Bold="True" Font-Size="Large" ForeColor="Maroon" 
            Font-Names="Calibri"></asp:Label>

        <asp:Label ID="Label5" runat="server" Text="Área de Negócio" Font-Size= "Small"
            style="position:absolute; top: 27px; left: 8px; height: 15px; width: 163px;"></asp:Label>
        <asp:DropDownList ID="cmbTipoServico" runat="server" Font-Size="Small"
            style="position:absolute; top: 45px; left: 8px; width: 390px;"
            onselectedindexchanged="btnPesquisar_Click" AutoPostBack="True">
        </asp:DropDownList>

        <asp:Label ID="Label1" runat="server" Text="Mês" Font-Size="Small"
            style="position:absolute; top: 27px; left: 405px; height: 15px; width: 27px;"> </asp:Label>
        <asp:DropDownList ID="cmbMes" runat="server" Font-Size="Small"
            style="position:absolute; top: 45px; left: 405px; width: 120px;" onselectedindexchanged="btnPesquisar_Click"
            AutoPostBack="True">
        </asp:DropDownList>

        <asp:Label ID="Label2" runat="server" Text="Ano" Font-Size="Small"
            style="position:absolute; top: 27px; left: 532px; height: 15px; width: 24px;"> </asp:Label>

        <asp:DropDownList ID="cmbAno" runat="server" Font-Size="Small"
            style="position:absolute; top: 45px; left: 532px; width: 90px;" onselectedindexchanged="btnPesquisar_Click"
            AutoPostBack="True">
        </asp:DropDownList>

        <asp:CheckBox ID="chkEmiteNotaFiscal" runat="server" Text="Obras que não emitem faturas" Font-Bold="true"
            AutoPostBack="True"
            style="position:absolute; top: 45px; left: 624px; width: 203px; bottom: 393px; right: 335px; margin-bottom: 25px;" 
            oncheckedchanged="chkEmiteNotaFiscal_CheckedChanged"/>

        <asp:Button ID="btnPesquisar" runat="server" Text="Pesquisar" 
            CausesValidation="False" Font-Bold="True"
                style="position:absolute; top: 41px; left: 832px; width: 107px; height: 24px; right: 223px;" 
                onclick="btnPesquisar_Click" />

        <asp:Button ID="btnAbertura" runat="server" Text="Abrir Faturamento" 
            CausesValidation="False" Font-Bold="True"
            style="position:absolute; top: 40px; left: 948px; width: 121px; height: 24px; right: 93px;" 
            onclick="btnAbertura_Click"/>

<%--PageSize="15"
 onpageindexchanging="gvDados_PageIndexChanging" 
  AllowPaging="True" 
 --%>
         <asp:GridView ID="gvDados" runat="server"
                AutoGenerateColumns="False" BackColor="LightGoldenrodYellow" BorderColor="Tan" 
                BorderStyle="None" BorderWidth="5px" CellPadding="3" CellSpacing="2" 
                Font-Size="X-Small" ForeColor="Black" GridLines="None" 
                style="position:absolute; top: 83px; left: 3px; width: 1123px;" 
                OnRowCreated="gvDados_RowCreated" 
                onrowdatabound="gvDados_RowDataBound">
                <FooterStyle BackColor="Tan" />
                <SelectedRowStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
                <PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" 
                    HorizontalAlign="Left" />
                <HeaderStyle BackColor="Tan" Font-Bold="True" />
                <AlternatingRowStyle BackColor="PaleGoldenrod" />
                <Columns>
                    <asp:BoundField DataField="cmpDcTipoServico" HeaderText="Tipo Fatura">
                    <ItemStyle Font-Size="X-Small" HorizontalAlign="Left" Width="20px" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="Fatura">
                        <ItemStyle Font-Size="X-Small" HorizontalAlign="Left" Width="20px" />
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkFatura" runat="server" CausesValidation="False" 
                                CommandArgument='<%# Eval("cmpIdFaturaObra") %>' CommandName="lnkFatura" 
                                OnClick="lnkFatura_Click" Text="Editar"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:BoundField DataField="cmpInEmiteNF" HeaderText="Emite Fatura">
                    <ItemStyle Font-Size="X-Small" HorizontalAlign="Left" Width="10px" />
                    </asp:BoundField>

                    <asp:BoundField DataField="cmpNoObraFatura" HeaderText="Nome da Obra">
                    <ItemStyle Font-Size="X-Small" HorizontalAlign="Left" Width="150px" />
                    </asp:BoundField>

                    <asp:BoundField DataField="cmpDcTipoServico" HeaderText="Faturamento">
                    <ItemStyle Font-Size="X-Small" HorizontalAlign="Left" Width="150px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="cmpDtPrevEmissaoNota" 
                        DataFormatString="{0:dd/MM/yyyy}" HeaderText="Data">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle Font-Size="X-Small" HorizontalAlign="Center" Width="20px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="cmpDtPrevEntregaDocObra" 
                        DataFormatString="{0:dd/MM/yyyy}" HeaderText="Data">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle Font-Size="X-Small" HorizontalAlign="Center" Width="20px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="cmpDtPrevRecebeNota" 
                        DataFormatString="{0:dd/MM/yyyy}" HeaderText="Data">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle Font-Size="X-Small" HorizontalAlign="Center" Width="20px" />
                    </asp:BoundField>
                </Columns>
            </asp:GridView>

        <asp:Button ID="btnPopUpEditar" runat="server" style="display:none" />
            <asp:ModalPopupExtender 
                ID="ModalPopupExtender2" 
                runat="server" 
                TargetControlID="btnPopUpEditar" 
                PopupControlID="pnlPopUpEditar"
                BackgroundCssClass="modalBackground"></asp:ModalPopupExtender>

        <asp:Panel ID="pnlPopUpEditar" runat="server" BackColor="LightGoldenrodYellow" style="display:none" 
                    Height="249px" Width="537px" BorderStyle="Solid" BorderWidth="1px">
            <asp:Label ID="Label9" runat="server" Text="Edição Abertura do Faturamento"
                        style="position:absolute; top: 5px; left: 7px; height: 20px; width: 524px; font-size:medium; text-align: center;" 
                        BackColor="LightGoldenrodYellow" Font-Bold="True" Font-Size="Large" 
                        ForeColor="#3333FF" Font-Names="Calibri"></asp:Label>
            <asp:Label ID="lblNomeObra" runat="server" BackColor="LightGoldenrodYellow" Font-Bold="True" 
                        Font-Size="Small" ForeColor="#3333FF" 
                        style="position:absolute; top: 35px; left: 16px; height: 15px; width: 50px;" 
                        Text="OBRA : "></asp:Label>
            <asp:Label ID="lblObra" runat="server"
                        style="position:absolute; top: 35px; left: 66px; height: 15px; width: 469px;" 
                        BackColor="LightGoldenrodYellow" Font-Bold="True" Font-Size="Small"
                        ForeColor="#3333FF"></asp:Label>

            <asp:Label ID="lblAnoAbertura" runat="server" Text="Ano Abertura"
                        style="position:absolute; top: 55px; left: 22px; height: 15px; width: 102px; right: 1042px;" 
                        BackColor="LightGoldenrodYellow" Font-Bold="True" Font-Size= "Small" 
                        ForeColor="#3333FF" Font-Names="Calibri"></asp:Label>

            <asp:DropDownList ID="cmbAnoAbertura" runat="server" Font-Size="Small"
                style="position:absolute; top: 74px; left: 22px; width: 150px; height: 17px;" >
            </asp:DropDownList>

            <asp:DropDownList ID="cmbMesAbertura" runat="server" Font-Size="Small"
                style="position:absolute; top: 74px; left: 186px; width: 150px;" >
            </asp:DropDownList>

            <asp:Label ID="lblMesAbertura" runat="server" Text="Mes Abertura"
                        style="position:absolute; top: 55px; left: 186px; height: 15px; width: 101px; right: 884px;" 
                        BackColor="LightGoldenrodYellow" Font-Bold="True" Font-Size= "Small" 
                        ForeColor="#3333FF" Font-Names="Calibri"></asp:Label>

           <asp:CheckBox ID="ChkEmissaoNota" runat="server"  
                Text="Confirma Emissão Nota Fiscal" Font-Bold="true"
                style="position:absolute; top: 165px; left: 22px; height: 23px; width: 189px; right: 951px;" />

            <asp:Label ID="Label24" runat="server" Text="Emissão da Nota Fiscal"
                        style="position:absolute; top: 112px; left: 22px; height: 15px; width: 149px; right: 992px;" 
                        BackColor="LightGoldenrodYellow" Font-Bold="True" Font-Size= "Small" 
                        ForeColor="#3333FF" Font-Names="Calibri"></asp:Label>
            <asp:TextBox ID="txtDtEmissaoNota" runat="server" 
                        style="position:absolute; top: 132px; left: 22px; width: 150px;" 
                        Font-Names="Calibri" Font-Size="Small"></asp:TextBox>
                        <asp:MaskedEditExtender ID="txtDtEmissaoNota_MaskedEditExtender" runat="server" 
                            Enabled="True" 
                            TargetControlID="txtDtEmissaoNota" CultureName="pt-BR" 
                            Mask="99/99/9999" MaskType="Date" UserDateFormat="DayMonthYear" 
                            UserTimeFormat="TwentyFourHour"></asp:MaskedEditExtender>
                        <asp:CalendarExtender ID="txtDtEmissaoNota_CalendarExtender" runat="server" 
                            Enabled="True" TargetControlID="txtDtEmissaoNota" Format="dd/MM/yyyy"></asp:CalendarExtender>

            <asp:Label ID="Label13" runat="server" Text="Entrega Documentação"
                        style="position:absolute; top: 112px; left: 186px; height: 17px; width: 147px;" 
                        BackColor="LightGoldenrodYellow" Font-Bold="True" Font-Size="Small" 
                        ForeColor="#3333FF" Font-Names="Calibri"></asp:Label>
            <asp:TextBox ID="txtDtEntregaDoc" runat="server" 
                        style="position:absolute; top: 132px; left: 186px; width: 150px;"
                        Font-Names="Calibri" Font-Size="Small"></asp:TextBox>
                        <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server" 
                            Enabled="True" 
                            TargetControlID="txtDtEntregaDoc" CultureName="pt-BR" 
                            Mask="99/99/9999" MaskType="Date" UserDateFormat="DayMonthYear" 
                            UserTimeFormat="TwentyFourHour"></asp:MaskedEditExtender>
                        <asp:CalendarExtender ID="txtDtEntregaDoc_CalendarExtender" runat="server" 
                            Enabled="True" TargetControlID="txtDtEntregaDoc" Format="dd/MM/yyyy"></asp:CalendarExtender>

            <asp:Label ID="Label12" runat="server" Text="Recebimento Fatura:"
                        style="position:absolute; top: 112px; left: 345px; height: 15px; width: 148px;" 
                        BackColor="LightGoldenrodYellow" Font-Bold="True" Font-Size= "Small" 
                        ForeColor="#3333FF" Font-Names="Calibri"></asp:Label>
            <asp:TextBox ID="TxtDtRecFatura" runat="server" CausesValidation = "false"
                    style="position:absolute; top: 132px; left: 343px; width: 150px;" 
                    Font-Names="Calibri" Font-Size="Small"></asp:TextBox>
                    <asp:MaskedEditExtender ID="MaskedEditExtender5" runat="server" 
                        Enabled="True" 
                        TargetControlID="TxtDtRecFatura" CultureName="pt-BR" 
                        Mask="99/99/9999" MaskType="Date"  UserDateFormat="DayMonthYear"
                        MessageValidatorTip="false"></asp:MaskedEditExtender>
                    <asp:CalendarExtender ID="TxtDtRecFatura_CalendarExtender" runat="server" 
                        Enabled="True" TargetControlID="TxtDtRecFatura" Format="dd/MM/yyyy"></asp:CalendarExtender>

            <asp:Button ID="btnSaveFatura" CommandName="UpdateSaveFatura" runat="server" Text="Gravar" 
                        style="position:absolute; top: 208px; left: 20px; width: 70px; height: 30px;" 
                        CausesValidation="false"
                        onclick="btnSaveFatura_Click"/>
            <asp:Button ID="btnCancelRepro" runat="server" Text="Cancelar" CausesValidation="false"
                        style="position:absolute; top: 208px; left: 101px; width: 70px; height: 30px;" 
                        onclick="btnCancelRepro_Click" />
            
 
            
            </asp:Panel>

    </div>
</asp:Content>
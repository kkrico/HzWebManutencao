<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="webPRE_PreventivaSemConformidade.aspx.cs" Inherits="HzWebManutencao.Preventiva.webPRE_PreventivaSemConformidade" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div style="position:relative; height: 499px; width: 980px; top: 6px; left: -54px; margin-left: 40px;">
        <asp:Label ID="lblAcao" runat="server" Text="Preventiva Confirmada sem Conformidade"
            style="position:absolute; top: 4px; left: 6px; height: 27px; width: 969px; text-align: center;" 
            BackColor="White" Font-Bold="True" Font-Size="Large" ForeColor="Maroon" 
            Font-Names="Calibri"></asp:Label>

        <asp:Label ID="Label5" runat="server" Text="Obra:" 
            style="position:absolute; top: 43px; left: 12px; height: 17px; width: 34px;"></asp:Label>
        <asp:DropDownList ID="cmbObra" runat="server" 
            style="position:absolute; top: 43px; left: 51px; width: 349px; height: 24px; right: 572px;"
            AutoPostBack="True" 
            onselectedindexchanged="cmbObra_SelectedIndexChanged" >
        </asp:DropDownList>

        <asp:RadioButtonList ID="rdbOcorrencia" runat="server" 
            
            style="position:absolute; top: 71px; left: 6px; width: 502px; height: 22px;" AutoPostBack="True" 
            onselectedindexchanged="rdbOcorrencia_SelectedIndexChanged" RepeatColumns="3" 
            RepeatDirection="Horizontal" Visible="False">
            <asp:ListItem Value="1">Vinculada a Ordem de Serviço</asp:ListItem>
            <asp:ListItem Value="0">Não Vinculada a Ordem de Serviço</asp:ListItem>
            <asp:ListItem Selected="True" Value="T">Todas</asp:ListItem>
        </asp:RadioButtonList>

        <asp:Label ID="Label7" runat="server" Text="Data Inicial:" 
            style="position:absolute; top: 43px; left: 411px; width: 70px; height: 21px;"></asp:Label>
        <asp:TextBox ID="txtDataInicial" runat="server" 
            style="position:absolute; top: 42px; left: 484px; height: 22px; width: 111px;" 
            AutoPostBack="True" ontextchanged="txtData_TextChanged"></asp:TextBox> 
                <asp:MaskedEditExtender ID="txtDataInicial_MaskedEditExtender" runat="server" 
                    Enabled="True" 
                    TargetControlID="txtDataInicial" CultureName="pt-BR" 
                    Mask="99/99/9999" MaskType="Date" UserDateFormat="DayMonthYear" 
                    UserTimeFormat="TwentyFourHour"></asp:MaskedEditExtender>
                <asp:CalendarExtender ID="txtDataInicial_CalendarExtender" runat="server" 
                    Enabled="True" TargetControlID="txtDataInicial" Format="dd/MM/yyyy"></asp:CalendarExtender>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                style="position:absolute; top: 79px; left: 660px; width: 200px; height: 26px;" 
                ErrorMessage="Data inicial em branco!" 
                ControlToValidate="txtDataInicial"></asp:RequiredFieldValidator>

        <asp:Label ID="Label6" runat="server" Text="Data Final:" 
            style="position:absolute; top: 41px; left: 611px; width: 70px; height: 21px;"></asp:Label>
        <asp:TextBox ID="txtDataFinal" runat="server" 
            style="position:absolute; top: 39px; left: 677px; height: 24px; width: 111px;" 
            AutoPostBack="True" ontextchanged="txtDataFinal_TextChanged"></asp:TextBox> 
                <asp:MaskedEditExtender ID="txtDataFinal_MaskedEditExtender" runat="server" 
                    Enabled="True" 
                    TargetControlID="txtDataFinal" CultureName="pt-BR" 
                    Mask="99/99/9999" MaskType="Date" UserDateFormat="DayMonthYear" 
                    UserTimeFormat="TwentyFourHour"></asp:MaskedEditExtender>
                <asp:CalendarExtender ID="txtDataFinal_CalendarExtender" runat="server" 
                    Enabled="True" TargetControlID="txtDataFinal" Format="dd/MM/yyyy"></asp:CalendarExtender>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                    style="position:absolute; top: 6px; left: 789px; width: 200px; height: 26px;" 
                    ErrorMessage="Data final em branco!" 
                    ControlToValidate="txtDataFinal"></asp:RequiredFieldValidator>

        <asp:Button ID="btnPesquisar" runat="server" Text="Pesquisar" CausesValidation="False"
                style="position:absolute; top: 37px; left: 802px; width: 82px; height: 26px; right: 96px;" 
                onclick="btnPesquisar_Click" />

       <asp:GridView ID="grdPreventivaPesq" runat="server" 
            style="position:absolute; top: 105px; left: 16px; width: 950;" AllowPaging="True" 
            AllowSorting="True" AutoGenerateColumns="False" 
            onpageindexchanging="grdPreventivaPesq_PageIndexChanging" 
            ViewStateMode="Enabled" 
            BorderColor="#003366" BorderStyle="Inset" BorderWidth="1px"
            CellPadding="3" BackColor="White" Font-Size="Small" PageSize="12" 
            onrowdatabound="grdPreventivaPesq_RowDataBound" 
            onrowcommand="grdPreventivaPesq_RowCommand">
            <Columns>
                <asp:BoundField DataField="cmpDtReprogramacaoPreventivaAgenda" HeaderText="Agendado" 
                                DataFormatString="{0:dd/MM/yyyy}">
                    <ItemStyle Width="20px" HorizontalAlign="Center" Font-Size="Small" /></asp:BoundField>
                <asp:BoundField DataField="cmpDtConfirmacaoPreventiva" HeaderText="Executado" 
                                DataFormatString="{0:dd/MM/yyyy}">
                    <ItemStyle Width="20px" HorizontalAlign="Center" Font-Size="Small" /></asp:BoundField>
                <asp:BoundField DataField="cmpNuPreventivaAgenda" HeaderText="Preventiva Nº">
                    <ItemStyle Width="100px" HorizontalAlign="Left" Font-Size="Small" /></asp:BoundField>
                <asp:BoundField DataField="cmpCoPeriodicidade" HeaderText="CoPeriodicidade">
                    <ItemStyle Width="1" HorizontalAlign="Left" Font-Size="Small" /></asp:BoundField>
                <asp:BoundField DataField="cmpDcPeriodicidade" HeaderText="Periodicidade">
                    <ItemStyle Width="200" HorizontalAlign="Left" Font-Size="Small" /></asp:BoundField>
                <asp:BoundField DataField="cmpDcTipoAtividade" HeaderText="Tipo Atividade">
                    <ItemStyle Width="300px" HorizontalAlign="Left" Font-Size="Small" /></asp:BoundField>
                <asp:BoundField DataField="cmpIdPreventivaConfirmacao" HeaderText="cmpIdPreventivaConfirmacao">
                    <ItemStyle Width="1px" HorizontalAlign="Center" Font-Size="Small" /></asp:BoundField>
                <asp:BoundField DataField="cmpIdOS" HeaderText="cmpIdOS">
                    <ItemStyle Width="1px" HorizontalAlign="Center" Font-Size="Small" /></asp:BoundField>
                <asp:TemplateField HeaderText="Ordem Serviço">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" visible="false"
                            CommandArgument='<%# Eval("cmpIdOS") %>' 
                            CommandName="LinkButton1"><%#Eval("cmpNuOS") %></asp:LinkButton>
                        <asp:LinkButton ID="LinkButton2" runat="server" Text="Vincular" visible="false"
                            CommandArgument= '<%# Eval("cmpIdPreventivaAgenda") + "$" + Eval("cmpNuPreventivaAgenda") %>' 
                            CommandName="LinkButton2"></asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle Width="120px" HorizontalAlign="Right" VerticalAlign="Middle" />
                </asp:TemplateField>
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

       <asp:CompareValidator ID="CompareValidator1" runat="server" 
            style="position:absolute; top: 78px; left: 804px; width: 147px; height: 26px;"
            ErrorMessage="Data final menor que data inicial!" 
            ControlToCompare="txtDataInicial" ControlToValidate="txtDataFinal" 
            Display="None" Operator="GreaterThanEqual" 
            Type="Date"></asp:CompareValidator>
        
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
            style="position:absolute; top: 71px; left: 521px; width: 200px; height: 26px;"
            ShowMessageBox="True" ShowSummary="False" />
        
</div>
</asp:Content>

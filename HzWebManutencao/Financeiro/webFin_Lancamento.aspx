<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="webFin_Lancamento.aspx.cs" Inherits="HzWebManutencao.Financeiro.webFIN_Lancamento" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div runat=server id="divstyle" style="position:relative; width: 1000px; top: 12px; left: -54px; margin-left: 40px; min-height: 850px">
        <asp:Label ID="Label1" runat="server" Text="Grupo:" 
                style="position:absolute; top: 23px; left: 11px; height: 18px; width: 44px;">
        </asp:Label>
        <asp:ListBox ID="lstObraGrupo" runat="server" 
            style="position:absolute; top: 23px; left: 128px; width: 308px; height: 110px; bottom: -74px; right: 556px;" 
            SelectionMode="Multiple" AutoPostBack="True" 
            onselectedindexchanged="lstObraGrupo_SelectedIndexChanged">
        </asp:ListBox>

        <asp:Label ID="lblMesInicial" runat="server" Text="Mês Inicial:" 
            
            style="position:absolute; top: 153px; left: 11px; height: 18px; width: 90px;"></asp:Label>
        <asp:DropDownList ID="cmbMesInicial" runat="server"
            style="position:absolute; top: 153px; left: 128px; height: 18px; width: 198px;">
        </asp:DropDownList>
        <asp:TextBox ID="txtAnoInicial" runat="server"            
            
            style="position:absolute; top: 152px; left: 338px; height: 18px; width: 60px;">
        </asp:TextBox>

        <asp:Label ID="Label3" runat="server" Text="Mês Final:" 
            style="position:absolute; top: 183px; left: 11px; height: 18px; width: 90px;">
        </asp:Label>
        <asp:DropDownList ID="cmbMesFinal" runat="server"
            
            style="position:absolute; top: 183px; left: 128px; height: 18px; width: 198px;" 
            AutoPostBack="True" onselectedindexchanged="cmbMesFinal_SelectedIndexChanged">
        </asp:DropDownList>
        <asp:TextBox ID="txtAnoFinal" runat="server"            
            
            style="position:absolute; top: 181px; left: 338px; height: 18px; width: 60px; right: 594px;">
        </asp:TextBox>

        <asp:CheckBox ID="chkExcel" runat="server"
            style="position:absolute; top: 152px; left: 424px; height: 18px; width: 127px;" 
            Text="Gerar Excel">
        </asp:CheckBox>

        <asp:CheckBox ID="chkGeneral" runat="server"
            style="position:absolute; top: 152px; left: 524px; height: 18px; width: 127px;" 
            Text="Grupo">
        </asp:CheckBox>

        <asp:Button ID="btnSave" runat="server" Text="Gravar" 
            style="position:absolute; top: 176px; left: 632px; width: 60px; height: 26px;" 
            onclick="btnSave_Click" Visible="False">
        </asp:Button>
        <asp:Button ID="btnReset" runat="server" Text="Incluir/Alterar" 
            
            style="position:absolute; top: 176px; left: 511px; width: 102px; height: 26px;" 
            onclick="btnReset_Click">
        </asp:Button>
        <asp:Button ID="btnSearch" runat="server" Text="Pesquisar" 
            
            style="position:absolute; top: 177px; left: 415px; width: 82px; height: 26px;" 
            onclick="btnSearch_Click">
        </asp:Button>
        <asp:Label ID="lblTotal" runat="server" 
            style="position:absolute; top:212px; left:450px"></asp:Label>


        <asp:Label ID="lblContrato" runat="server" Text="Valor Contrato (R$):" 
            style="position:absolute; top: 230px; left: 11px; height: 18px; width: 132px;" 
            Visible="False"></asp:Label>
        <asp:TextBox ID="txtVlContrato" runat="server"            
            style="position:absolute; top: 230px; left: 140px; height: 18px; width: 175px; right: 697px;" 
            Visible="False"></asp:TextBox>

        <asp:Label ID="lblMetaMensal" runat="server" Text="Meta Mensal (%):" 
            style="position:absolute; top: 260px; left: 11px; height: 18px; width: 132px;" 
            Visible="False"></asp:Label>
        <asp:TextBox ID="txtMetaMensal" runat="server"            
            style="position:absolute; top: 260px; left: 140px; height: 18px; width: 175px; right: 697px;" 
            Visible="False"></asp:TextBox>

        <asp:Label ID="lblAdm" runat="server" Text="Adm.: (%):" 
            style="position:absolute; top: 290px; left: 11px; height: 18px; width: 132px;" 
            Visible="False"></asp:Label>
        <asp:TextBox ID="txtVlAdm" runat="server"            
            style="position:absolute; top: 290px; left: 140px; height: 18px; width: 175px; right: 697px;" 
            Visible="False"></asp:TextBox>


        <asp:Label ID="lblPendencia" runat="server" Text="Pendências Anteriores"
            style="position:absolute; top: 370px; left: 11px; height: 18px; width: 315px;" 
            Font-Bold="True" CssClass="title" Visible="False"></asp:Label>
        <asp:GridView ID="grdPendencia" runat="server"
        style="position:absolute; top: 390px; left: 9px; width:320px; height: 26px;" 
            AutoGenerateColumns="False">
            <Columns>
                <asp:BoundField DataField="cmpDcMes" HeaderText="Mês" >
                    <ItemStyle Width="70px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="cmpNuAno" HeaderText="Ano" >
                    <ItemStyle Width="170px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="cmpVlLancado" HeaderText="Contrato (R$)" 
                    DataFormatString="{0:N}" >
                    <ItemStyle Width="170px" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="Pagamento Efetuado?">
                    <ItemTemplate>
                        <asp:checkbox ID="chkPagamento" runat="server"></asp:Checkbox>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                 </asp:TemplateField>
            </Columns>
        </asp:GridView>

       <asp:Label ID="lblFinanceiro" runat="server" Text="Financeiro Atual de "
            style="position:absolute; top: 370px; left: 420px; height: 18px; width: 315px;" 
            Font-Bold="True" CssClass="title" Visible="False"></asp:Label>

      <asp:Label ID="lblRealizado" runat="server" Text="Pagamento Realizado?"
            style="position:absolute; top: 370px; left: 620px; height: 18px; width: 315px;" 
            Font-Bold="True" CssClass="title" Visible="False"></asp:Label>
      <asp:CheckBox ID="chkRealizado" runat="server" Visible="false" 
            
            style="position:absolute; top: 370px; left: 760px; height: 18px; width: 21px;"></asp:checkbox>

        <asp:GridView ID="grdTipoConta" runat="server"
        style="position:absolute; top: 390px; left: 420px; width:320px; height: 26px;" 
            AutoGenerateColumns="False">
            <Columns>
                <asp:BoundField DataField="cmpDcCodigoConta" HeaderText="Conta" >
                    <ItemStyle Width="70px" />
                </asp:BoundField>
                <asp:BoundField DataField="cmpDcTipoConta" HeaderText="Conta" >
                    <ItemStyle Width="170px" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="Valor Atual (R$)" >
                    <ItemTemplate>
                        <asp:Label ID="lblValue" runat="server"></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                 </asp:TemplateField>
                <asp:TemplateField HeaderText="Valor (R$)" >
                    <ItemTemplate>
                        <asp:TextBox ID="txtValue" runat="server"></asp:TextBox>
                    </ItemTemplate>
                 </asp:TemplateField>
                 <asp:TemplateField HeaderText="Somar">
                    <ItemTemplate>
                        <asp:CheckBox ID="chkSum" runat="server"></asp:CheckBox>
                    </ItemTemplate>
                 </asp:TemplateField>

                <asp:TemplateField HeaderText="codigo" Visible="False">
                    <ItemTemplate>
                        <asp:TextBox ID="txtCode" runat="server" Visible="false"></asp:TextBox>
                    </ItemTemplate>
                 </asp:TemplateField>
                <asp:TemplateField HeaderText="codigo" Visible="False">
                    <ItemTemplate>
                        <asp:TextBox ID="txtObraSiengeConta" runat="server" Visible="false"></asp:TextBox>
                    </ItemTemplate>
                 </asp:TemplateField>
            </Columns>
        </asp:GridView>

        <asp:Chart ID="Chart1" runat="server" BorderlineColor="" 
            BorderlineDashStyle="Solid" 
            
            style="position:absolute; top: 139px; left: 902px; height: 181px; width: 242px;" 
            Visible="False">
            <Series>
                <asp:Series Name="Series1" ChartType="Line" Legend="Legend1" IsValueShownAsLabel="True">
                </asp:Series>
            </Series>
            <ChartAreas>
                <asp:ChartArea Name="ChartArea1"  BorderColor="Black" BorderWidth="3">
                </asp:ChartArea>
            </ChartAreas>
            <Legends>
                <asp:Legend Name="Legend1" LegendStyle="Column" HeaderSeparator="None">
                </asp:Legend>
            </Legends>
            <Titles>
                <asp:Title Name="title1" Text="Quantidade de Ordens de Serviço no Período">
                </asp:Title>
            </Titles>
        </asp:Chart>

        <asp:RequiredFieldValidator ID="rfvMonth" runat="server" 
            ControlToValidate="cmbMesFinal" Display="None" ErrorMessage="Mês em branco!"></asp:RequiredFieldValidator>
        <asp:RequiredFieldValidator ID="rfvYear" runat="server" 
            ControlToValidate="txtAnoFinal" Display="None" ErrorMessage="Ano em branco!"></asp:RequiredFieldValidator>
        <asp:RequiredFieldValidator ID="rfvObra" runat="server" 
            ControlToValidate="lstObraGrupo" Display="None" ErrorMessage="Selecione uma obra!"></asp:RequiredFieldValidator>
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
            ShowMessageBox="True" ShowSummary="False" />        
    </div>
</asp:Content>

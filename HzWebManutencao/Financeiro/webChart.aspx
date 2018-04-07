<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="webChart.aspx.cs" Inherits="HzWebManutencao.Financeiro.webChart" %>
<%@ Register assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI.DataVisualization.Charting" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div style="position:relative; height: 469px; width: 1000px; top: 12px; left: -54px; margin-left: 40px;">
        <asp:Chart ID="Chart1" runat="server" Width="500px" BorderlineColor="" 
            BorderlineDashStyle="Solid" 
            style="position:absolute; top: 215px; left: 85px;" Visible="False">
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
        <asp:PlaceHolder ID="plcHolder" runat="server"></asp:PlaceHolder>
        <asp:CheckBoxList ID="chkGraphic" runat="server" 
            style="position:absolute; top: 41px; left: 86px;" RepeatDirection="Horizontal" 
            RepeatLayout="Flow" >
            <asp:ListItem Value="nabsolutomes">N. Absoluto Mês</asp:ListItem>
            <asp:ListItem Value="natendimentodiario">N. Atendimento Diário</asp:ListItem>
            <asp:ListItem Value="natendimentotipo">N. Atendimento Tipo</asp:ListItem>
            <asp:ListItem Value="mediatempoatendimento">Média Tempo Atendimento</asp:ListItem>
        </asp:CheckBoxList>

        <asp:DropDownList ID="cmbObra" runat="server" 
            
            
            style="position:absolute; top: 82px; left: 89px; width: 386px; height: 21px;">
        </asp:DropDownList>
        <asp:CheckBox ID="chkDataGrafico" runat="server" 
            Text="Gerar por Data de Abertura" Checked="true"
            style="position:absolute; top: 81px; left: 479px; width: 189px; height: 21px;"/>

        <asp:Label ID="Label2" runat="server" Text="Obra:" 
            style="position:absolute; top: 82px; left: 14px;"></asp:Label>

        <asp:Label ID="Label3" runat="server" Text="Mês Inicial:" 
            style="position:absolute; top: 131px; left: 14px;"></asp:Label>
        <asp:DropDownList ID="cmbMesInicial" runat="server" 
            style="position:absolute; top: 131px; left: 89px; width: 189px">
        </asp:DropDownList>
        <asp:TextBox ID="txtYaerInitial" runat="server" 
            style="position:absolute; left: 288px; top: 131px; width: 54px;" 
            MaxLength="4"></asp:TextBox>

        <asp:Label ID="Label1" runat="server" Text="Mês Final:" 
            style="position:absolute; top: 164px; left: 14px;"></asp:Label>
        <asp:DropDownList ID="cmbMesFinal" runat="server" 
            style="position:absolute; top: 164px; left: 89px; width: 189px">
        </asp:DropDownList>
        <asp:TextBox ID="txtYearFinal" runat="server" 
            style="position:absolute; left: 288px; top: 164px; width: 54px;" 
            MaxLength="4"></asp:TextBox>
        <asp:Button ID="btnGraphic" runat="server" Text="Gerar" 
            style="position:absolute; top: 164px; left: 403px; width: 69px;" 
            onclick="btnGraphic_Click" />
    </div>
</asp:Content>

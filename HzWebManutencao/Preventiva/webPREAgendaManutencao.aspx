<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="webPREAgendaManutencao.aspx.cs" Inherits="HzWebManutencao.Preventiva.webPREAgendaManutencao" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Panel ID="Panel1" runat="server" Height="431px" Width="1082px" 
        style="margin-left: 0px">
        <asp:Panel ID="Panel2" runat="server" Height="412px" Width="1071px">
                <asp:Label ID="Label1" runat="server" Text="Obra: "></asp:Label>
                            <asp:DropDownList ID="cmbObra" runat="server" 
                onselectedindexchanged="cmbObra_SelectedIndexChanged" AutoPostBack="True" 
                Height="35px" Width="319px">
            </asp:DropDownList>
                <br />
                <br />
                <asp:Label ID="Label2" runat="server" ForeColor="#009933" Text="(Preventiva)"></asp:Label>
                &nbsp;
                <asp:Label ID="Label3" runat="server" ForeColor="#0066FF" Text="(OS)"></asp:Label>
                <br />
                <br />
            <asp:Calendar ID="Calendar1" runat="server" Height="335px" 
                ondayrender="Calendar1_DayRender" Width="322px"  
                
                onselectionchanged="Calendar1_SelectionChanged"></asp:Calendar>
            
                <div style="position: absolute; top: 230px; left: 581px; width: 680px; height: 333px;">
                    <asp:GridView ID="grvAtividades" runat="server" AutoGenerateColumns="False" 
                        Width="672px">
                        <Columns>
                            <asp:BoundField DataField="TipoPedido" HeaderText="Tipo" />
                            <asp:BoundField DataField="cmpTpPreventiva" HeaderText="Tipo Preventiva" />
                            <asp:BoundField DataField="cmpEstadoManutencaoPreventiva" 
                                HeaderText="Estado Preventiva" />
                            <asp:BoundField DataField="cmpDcTipoAtividade" HeaderText="Tipo Atividade" />
                        </Columns>
                    </asp:GridView>
                </div>
            
        </asp:Panel>
    </asp:Panel>
</asp:Content>

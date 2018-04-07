<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="webHorizonObras.aspx.cs" Inherits="HzWebManutencao.Horizon.webHorizonObras" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div style="position: relative; top: 12px; left: 9px; height: 451px; width: 1110px;" >
   <div style="position: relative; top: 12px; left: 9px; height: 451px; width: 350px;" >
        <asp:Label ID="Label1" runat="server" Text="CENTRO DE CUSTO"
         style="position: absolute; top: 1px; left: 3px; height: 16px; width: 138px;"></asp:Label>
        <br />
        <asp:DropDownList ID="cmbCentroCusto" runat="server" onselectedindexchanged="cmbCentroCusto_SelectedIndexChanged" 
            AutoPostBack="True"
            style="position: absolute; top: 19px; left: 12px; height: 18px; width: 229px; bottom: 378px;">
        </asp:DropDownList>
        <asp:Button ID="btnNovo" runat="server" Text="Novo" onclick="btnNovo_Click" 
            style="position: absolute; top: 16px; left: 255px; height: 24px; width: 56px;"/>
 
        <asp:Label ID="Label2" runat="server" Text="OBRAS"
         style="position: absolute; top: 49px; left: 6px; height: 14px; width: 45px;"></asp:Label>
 
         <asp:Panel ID="Panel1" runat="server" ScrollBars="Vertical"
         
            style="position: absolute; top: 70px; left: 0px; height: 354px; width: 340px;">
            <asp:TreeView ID="trvObras" runat="server"
                
                 style="position: absolute; top: 0px; left: 0px; height: 150px; width: 320px;" 
                 onselectednodechanged="trvObras_SelectedNodeChanged">
            </asp:TreeView>
             </asp:Panel>
    </div>
    <div  id="divComponentes"  style="position: relative; top: -441px; left: 370px; height: 451px; width: 726px;" runat=server >

    </div>
    </div>
</asp:Content>

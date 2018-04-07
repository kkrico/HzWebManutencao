<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="webSistemasPrediais.aspx.cs" Inherits="HzWebManutencao.CentroCusto.webSistemasPrediais" %>
<%@ Register src="Navegacao.ascx" tagname="Navegacao" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
         <div  style=" position:relative; left:59; height: 85px; width: 1984px;">
           
            <asp:Label ID="Label2" runat="server" Text="Sistemas Prediais"
            BackColor="White" Font-Bold="True" Font-Size="Large" ForeColor="Maroon" 
            Font-Names="Calibri" 
               style="position:absolute; top: 0px; left: 271px; height: 21px;"></asp:Label>

 
          <uc1:Navegacao ID="Navegacao1" runat="server" style="position:relative; top: 10px; left: 59px; height: 12px; width: 139px;" />


        <asp:Button ID="btnIncluir" runat="server" Text="Incluir" CausesValidation="false"
            Height="30px" Width="75px"     
                
                 
                 style="position:absolute; left: 80px; top: 69px; right: 910px; width: 70px; height: 25px; bottom: 8px;" onclick="btnIncluir_Click" 
            />

            <asp:Button ID="btnPavimentos" runat="server" Height="28px" Text="Pavimentos>>" 
                   style="position:absolute; left: 171px; top: 67px; width: 81px; height: 25px; bottom: 10px; right: 808px;" 
                   onclick="btnPavimentos_Click" />
             
    </div>
        <asp:GridView ID="grdSistPredial" runat="server" AutoGenerateColumns="False" 
            CellPadding="4" ForeColor="#333333" GridLines="None"  
           
                
                style="position:relative; left: 59px; top: 8px; width: 821px;" 
                AllowPaging="True" onrowcommand="grdSistPredial_RowCommand" >
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:CommandField ShowSelectButton="True" />
                <asp:BoundField DataField="cmpDcTipoAtividade" HeaderText="Sistema Predial" />
                <asp:CommandField ShowDeleteButton="True" />
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



               

    <asp:Button ID="Button2" runat="server" Text="Button" style="display: none;" />

             <asp:ModalPopupExtender 
            ID="modSistPredial" 
            runat="server" 
            TargetControlID="Button2" 
            PopupControlID="pnlSistPredial"
            CancelControlID="btnFecharSelecPavimentos" 
            BackgroundCssClass="modalBackground"></asp:ModalPopupExtender>

             <asp:Panel ID="pnlSistPredial" runat="server" 
            style="position:absolute; top: 190px; left: 767px; height: 330px; width: 505px;" 
            BackColor="White" BorderStyle="Solid" 
            BorderWidth="1px"  >

            <asp:Button ID="btnFecharSelecPavimentos" runat="server" Text="Fechar" 
                
                     style="position:absolute; left: 26px; top: 10px; right: 419px; width: 60px; height: 21px;"    />

                <asp:Panel ID="Panel1" runat="server" 
            style="position:absolute; top: 0px; left: 0px; height: 230px; width: 405px;" ScrollBars="Vertical"  >

                <asp:GridView ID="dgvSistPredial" runat="server" 
                 style="position:absolute; top: 45px; left: 19px;  width: 306px;" 
                 AutoGenerateColumns="False" onrowcommand="dgvSistPredial_RowCommand" 
                     CellPadding="4" ForeColor="#333333" GridLines="None" >
                    <AlternatingRowStyle BackColor="White" />
                 <Columns>
                     <asp:TemplateField ShowHeader="False">
                         <ItemTemplate>
                             <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" 
                                 CommandArgument='<%# Eval("cmpCoTipoAtividade") %>' CommandName="Select" 
                                 Text="Select"></asp:LinkButton>
                         </ItemTemplate>
                     </asp:TemplateField>
                     <asp:BoundField DataField="cmpDcTipoAtividade" HeaderText="Sistema Predial" />
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
            </asp:Panel>

</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="webPavimentos.aspx.cs" Inherits="HzWebManutencao.CentroCusto.webPavimentos" %>
<%@ Register src="Navegacao.ascx" tagname="Navegacao" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div style="height: 461px; width: 1136px;">

     <div  style="position:absolute; height: 102px; width: 1060px; margin-top: 0px; margin-left: 0px; top: 119px; left: 41px; margin-bottom: 22px;">

             <asp:Label ID="Label2" runat="server" Text="Pavimentos"
            BackColor="White" Font-Bold="True" Font-Size="Large" ForeColor="Maroon" 
            Font-Names="Calibri" 
               style="position:absolute; top: 0px; left: 271px; height: 21px;"></asp:Label>


    <asp:Label ID="lblCentroCusto" runat="server" 
        style="position: absolute; top: 6px; left: 38px; right: 1088px;"></asp:Label>

          <div  style=" position:absolute; height: 30px; width: 939px; margin-top: 0px; margin-left: 0px; top: 27px; left: 95px;">
          <uc1:Navegacao ID="Navegacao1" runat="server" style="position:absolute; top: 0px; left: 509px; height: 12px; width: 70px;" />
        </div>

        <asp:Button ID="btnIncluir" runat="server" Text="Incluir"
            Height="30px" Width="75px"  
                 style="position:absolute; left: 133px; top: 84px; right: 857px; width: 70px; height: 25px;" 
                 onclick="btnIncluir_Click"/>
                 <asp:Button ID="btnNovo" runat="server" Text="Novo"
            Height="30px" Width="75px"  
                 
                 
                 
                 style="position:absolute; left: 219px; top: 82px; right: 771px; width: 70px; height: 25px;" 
                 onclick="btnNovo_Click"/>
    </div>
        <asp:GridView ID="grdPavimentos" runat="server" AutoGenerateColumns="False" 
            CellPadding="4" ForeColor="#333333" GridLines="None"  
            style="position:absolute; left: 217px; top: 236px; width: 430px; height: 252px;" 
            
            ViewStateMode="Enabled" onrowcommand="grdPavimentos_RowCommand" 
                 AllowPaging="True"  >
            <AlternatingRowStyle BackColor="White" />
            <Columns>

                <asp:TemplateField ShowHeader="False">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" 
                            CommandArgument='<%# Eval("cmpCoObraPavimento") %>' CommandName="Select" Text="Select"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="cmpDcPavimento" HeaderText="Pavimento" />
                <asp:BoundField DataField="cmpSgPavimento" HeaderText="Sigla" />
                <asp:BoundField DataField="cmpNuOrdenacao" HeaderText="Ordenação" />
                <asp:TemplateField ShowHeader="False">
               
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" 
                            CommandArgument='<%# Eval("cmpCoObraPavimento") %>' CommandName="Edit" 
                            Text="Edit"></asp:LinkButton>
                    </ItemTemplate>
               
                </asp:TemplateField>
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
            ID="modSelecPavimentos" 
            runat="server" 
            TargetControlID="Button2" 
            PopupControlID="pnlSelecPavimentos"
            CancelControlID="btnFecharSelecPavimentos" 
            BackgroundCssClass="modalBackground"></asp:ModalPopupExtender>

              <asp:Panel ID="pnlSelecPavimentos" runat="server" 
            style="position:absolute; top: 203px; left: 176px; height: 330px; width: 505px;" 
            BackColor="White" BorderStyle="Solid" 
            BorderWidth="1px">
                  
                  <asp:Button ID="btnFecharSelecPavimentos" runat="server" Text="Fechar" 
                style="position:absolute; left: 36px; top: 10px; right: 409px; width: 60px; height: 21px;" 
                />
                 <asp:Panel ID="Panel1" runat="server" 
            style="position:absolute; top: 51px; left: 11px; height: 230px; width: 405px;" 
            BackColor="White" BorderStyle="Solid" ScrollBars="Vertical">

                  <asp:GridView ID="grvPavimentosSelec" runat="server" AutoGenerateColumns="False" 
                      CellPadding="4" ForeColor="#333333" GridLines="None" 
                       
                      
                      style="position:absolute; top: 9px; left: 14px;  width: 376px;" 
                      ViewStateMode="Enabled" 
                      onrowcommand="grvPavimentosSelec_RowCommand">
                      <AlternatingRowStyle BackColor="White" />
                      <Columns>
                          <asp:TemplateField ShowHeader="False">
                              <ItemTemplate>
                                  <asp:LinkButton ID="LinkButton3" runat="server" CausesValidation="False" 
                                      CommandArgument='<%# Eval("cmpCoPavimento") %>' CommandName="Select" 
                                      Text="Select" onclick="LinkButton3_Click"></asp:LinkButton>
                              </ItemTemplate>
                          </asp:TemplateField>
                          <asp:BoundField DataField="cmpDcPavimento" HeaderText="Pavimento" />
                          <asp:BoundField DataField="cmpSgPavimento" HeaderText="Sigla" />
                          <asp:BoundField DataField="cmpNuOrdenacao" HeaderText="Ordenação" />
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
            
            
            <asp:Button ID="Button1" runat="server" Text="Button" style="display: none;" />
            <asp:ModalPopupExtender 
            ID="mpNovo" 
            runat="server" 
            TargetControlID="Button1" 
            PopupControlID="pnlNovo"
            CancelControlID="btnFecharNovo" 
            BackgroundCssClass="modalBackground"></asp:ModalPopupExtender>

              <asp:Panel ID="pnlNovo" runat="server" 
            style="position:absolute; top: 203px; left: 702px; height: 205px; width: 325px;" 
            BackColor="White" BorderStyle="Solid" 
            BorderWidth="1px" >
                  <asp:Label ID="Label1" runat="server" Text="Descrição Pavimento" 
                      
                      style="position:absolute; top: 8px; left: 10px; width: 182px;" ></asp:Label>
                  <asp:TextBox ID="txtDcPavimento" runat="server" 
                      style="position:absolute; top: 28px; left: 10px; width: 289px;" ></asp:TextBox>
                  <asp:Label ID="Label3" runat="server" Text="Sigla" 
                      style="position:absolute; top: 56px; left: 10px; width: 50px;" ></asp:Label>
                  <asp:TextBox ID="txtSgPavimento" runat="server" 
                      style="position:absolute; top: 77px; left: 10px; width: 50px;" ></asp:TextBox>
                    <asp:Label ID="Label4" runat="server" Text="Ordenação" 
                      style="position:absolute; top: 58px; left: 92px; width: 50px;" ></asp:Label>
                  <asp:TextBox ID="txtNuOrdenacao" runat="server" 
                      style="position:absolute; top: 77px; left: 92px; width: 32px;" ></asp:TextBox>
              <asp:Button ID="btnFecharNovo" runat="server" Text="Fechar" 
                style="position:absolute; left: 139px; top: 147px; right: 126px; width: 60px; height: 21px;" 
                />
                <asp:Button ID="btnSalvarNovo" runat="server" Text="Salvar" 
                
                      style="position:absolute; left: 59px; top: 147px; right: 206px; width: 60px; height: 21px;" onclick="btnSalvarNovo_Click" 
                />
                  
                  </asp:Panel>
    

            
&nbsp;</div>
</asp:Content>

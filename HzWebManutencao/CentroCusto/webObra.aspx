<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="webObra.aspx.cs" Inherits="HzWebManutencao.CentroCusto.webObra" %>
<%@ Register src="Navegacao.ascx" tagname="Navegacao" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
       <div  style=" position:relative; height: 92px; width: 1060px; top: 0px; left: 59px; margin-bottom: 22px;">
          <asp:Label ID="Label2" runat="server" Text="Obras"
            BackColor="White" Font-Bold="True" Font-Size="Large" ForeColor="Maroon" 
            Font-Names="Calibri" 
            style="position:absolute; top: -5px; left: 440px; height: 21px; width: 76px;"></asp:Label>
          <uc1:Navegacao ID="Navegacao1" runat="server" style="position:absolute; top: 0px; left: 509px; height: 12px; width: 139px;" />

          <asp:Button ID="btnIncluir" runat="server" Text="Incluir"
            Height="30px" Width="75px"  
            style="position:absolute; left: 24px; top: 59px; right: 971px; width: 65px; height: 25px;" 
            onclick="btnIncluir_Click"/>

            <asp:Button ID="btnNovo" runat="server" Text="Novo"
            Height="30px" Width="75px"  
            style="position:absolute; left: 109px; top: 59px; right: 886px; width: 65px; height: 25px; margin-top: 1px;" 
            onclick="btnNovo_Click1"/>
        </div>

        <asp:GridView ID="gvdWebObra" runat="server" AutoGenerateColumns="False" 
            CellPadding="4" ForeColor="#333333" GridLines="None"  
            
            
            style="position:relative; left: 59px; top: 8px; width: 613px;" 
            ViewStateMode="Enabled" AllowPaging="True" PageSize="9" 
                   onrowcommand="gvdWebObra_RowCommand" >
            <AlternatingRowStyle BackColor="White" />
            <Columns>

                <asp:TemplateField ShowHeader="False">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" 
                            CommandArgument='<%# Eval("cmpCoObra") %>' CommandName="Select" Text="Select"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="cmpNuObra" HeaderText="Numero" />
                <asp:BoundField DataField="cmpNoObra" HeaderText="Nome" />
                <asp:TemplateField ShowHeader="False">
               
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" 
                            CommandArgument='<%# Eval("cmpCoObra") %>' CommandName="Editar" 
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



        


             <asp:ModalPopupExtender 
            ID="ModObra" 
            runat="server" 
            TargetControlID="Button1" 
            PopupControlID="divObra"
            CancelControlID="btnFechar" 
            BackgroundCssClass="modalBackground"></asp:ModalPopupExtender>
         <asp:Panel ID="divObra" runat="server" BackColor="White" BorderStyle="Solid" 
                    BorderWidth="1px" 

            
           style="position:absolute; top: 205px; left: 1111px; height: 261px; width: 348px;" >

                            <asp:Label ID="Label1" runat="server" Text="Área de Negócio" 
                BackColor="White" Font-Bold="True" Font-Size="Large" ForeColor="Black" 
                Font-Names="Calibri" 
               style="position:absolute; top: 59px; height: 21px; width: 134px; left: 17px;"></asp:Label>  

                <asp:TextBox ID="txtNumeroObra" runat="server" 
                    style="position:absolute; top: 30px; left: 17px; height: 19px; width: 103px;" 
                    ></asp:TextBox>

                <asp:Label ID="Label21" runat="server" Text="Obra" 
                BackColor="White" Font-Bold="True" Font-Size="Large" ForeColor="black" 
                Font-Names="Calibri" 
               style="position:absolute; top: 103px; height: 21px; width: 76px; left: 23px;"></asp:Label>  

                    <br />
                    <br />
               <asp:Button ID="btnSalvar" runat="server" Text="Salvar"
                    
                                style="position:absolute; top: 102px;  left: 91px; top: 222px; right: 187px; width: 70px; height: 30px;" onclick="btnSalvar_Click1"
                />
                <asp:Button ID="btnFechar" runat="server" Text="Fechar" Width="63px" 
                   
                    
                    style="position:absolute;  left: 185px; top: 220px; right: 93px; width: 70px; height: 30px;" 
                    onclick="btnFechar_Click" />

                <asp:TextBox ID="txtDescricaoObra" runat="server" 
                    style="position:absolute; top: 130px; left: 24px; height: 75px; width: 270px;" 
                    TextMode="MultiLine"></asp:TextBox>
                            <asp:Label ID="Label22" runat="server" BackColor="White" Font-Bold="True" 
                                Font-Names="Calibri" Font-Size="Large" ForeColor="black" 
                                style="position:absolute; top: 3px; height: 21px; width: 134px; left: 17px;" 
                                Text="Número da Obra"></asp:Label>
                            <asp:DropDownList ID="cmbAreaNegocio" runat="server"  
                                style="position:absolute; top: 79px; left: 17px; width: 205px;" 
                                DataTextField="cmpDcAreaNegocio" DataValueField="cmpCoAreaNegocio" >
                            </asp:DropDownList>
            </asp:Panel>

       <asp:ModalPopupExtender 
            ID="modSelecObras" 
            runat="server" 
            TargetControlID="Button2" 
            PopupControlID="pnlSelecObras"
            CancelControlID="btnFecharSelecObras" 
            BackgroundCssClass="modalBackground"></asp:ModalPopupExtender>
        <asp:Panel ID="pnlSelecObras" runat="server" 
            

            style="position:absolute; top: 370px; left: 120px; height: 306px; width: 485px;" 
            BackColor="White" BorderStyle="Solid" 
            BorderWidth="1px" >
            <asp:Button ID="btnFecharSelecObras" runat="server" Text="Fechar" 
             
                style="position:absolute; left: 209px; top: 273px; right: 216px; width: 60px; height: 21px;" 
                onclick="btnFecharSelecObras_Click"/>
             
             
        <asp:GridView ID="gvdWebObraSelect" runat="server" AutoGenerateColumns="False" 
            CellPadding="4" ForeColor="#333333" GridLines="None"  
            
            style="position:absolute; left: 13px; top: 7px; width: 443px; height: 247px" 
                ViewStateMode="Enabled" AllowPaging="True" 
                onselectedindexchanged="gvdWebObraSelect_SelectedIndexChanged" 
                onrowcommand="gvdWebObraSelect_RowCommand" >
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:TemplateField ShowHeader="False">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" 
                            CommandArgument='<%# Eval("cmpCoObra") %>' CommandName="Select" Text="Select"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="cmpNoObra" HeaderText="Nome da Obra" />
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
        <asp:Button ID="Button1" runat="server" Text="Button" style="display: none;" />
        <asp:Button ID="Button2" runat="server" Text="Button" style="display: none;" />



               <br />



</asp:Content>

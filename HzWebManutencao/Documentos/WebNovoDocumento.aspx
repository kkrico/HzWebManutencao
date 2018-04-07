<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="WebNovoDocumento.aspx.cs" Inherits="HzWebManutencao.Documentos.WebNovoDocumento" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <!--Divisão -->
    <div style= "position:relative; top: -6px; left: -211px; height: 569px; width: 1157px;" "height: 446px; width: 1020px;">

    <!--Título -->
            <asp:Label ID="lblnovodoc" runat="server" Text="Pesquisa Documento" 
            style="position:absolute; top: 13px; left: 116px; height: 20px; width: 895px; text-align: center;" 
            BackColor="White" Font-Bold="True" Font-Size="Medium" ForeColor="Maroon"></asp:Label>

            <!-- Caixa de seleção -->

            <!--Pesquisa -->

            <!--Descrição -->

            <!--Data /inicio e fim  -->
           <%-- <asp:Label ID="lbldatainicio" runat="server" Text="Data Inicio" 
            style="position:absolute; top: 59px; left: 776px; width: 54px; height: 18px;"></asp:Label>
            <asp:TextBox ID="txtdatainicio" runat="server" 
                style="position:absolute; top: 76px; left: 772px;" 
            ontextchanged="txtdatainicio_TextChanged"></asp:TextBox>--%>


            <!--Ano -->

            <!--Numero -->

            <!--Lista -->


             <!--botões -->
                <!--botão pesquisar -->
             <asp:Button ID="btnpesquisar" runat="server" Text="Pesquisar" 
             
            style="position:absolute; top: 73px; left: 55px; height: 30px; width: 103px;" 
            onclick="btnpesquisar_Click"/>


             <!--botão novo -->
            <asp:Button ID="btnnovo" runat="server" Text="Novo" 
                
            
            style="position:absolute; top: 73px; left: 289px; height: 30px; width: 103px;" />


             <!--botão salvar -->
            <asp:Button ID="btnsalvar" runat="server" Text="Salvar" 
            
            style="position:absolute; top: 73px; left: 172px; height: 30px; width: 103px;"/>



             <!--Combos -->
             <asp:Label ID="lbltipo" runat="server" Text="Tipo:" 
            style="position:absolute; top: 145px; left: 43px;"></asp:Label>
            <asp:DropDownList ID="ddltipo" runat="server" 
            
            
            style="position:absolute; top: 145px; left: 120px; height: 19px; width: 277px; right: 752px;">
             </asp:DropDownList>

              <asp:Label ID="lblfuncionario" runat="server" Text="Funcionario:" 
            style="position:absolute; top: 195px; left: 43px;"></asp:Label>
            <asp:DropDownList ID="ddlfuncionario" runat="server" 
            
            
            style="position:absolute; top: 194px; left: 121px; height: 18px; width: 273px; right: 755px;">
             </asp:DropDownList>


             <!--Campos de textos -->
               <asp:Label ID="lblano" runat="server" Text="Ano:" 
            style="position:absolute; top: 240px; left: 50px;"></asp:Label>
            <asp:TextBox ID="txtano" runat="server" 
            style="position:absolute; top: 286px; left: 122px; width: 264px;"></asp:TextBox>

            <asp:Label ID="lblnumero" runat="server" Text="Numero:" 
            style="position:absolute; top: 290px; left: 50px;"></asp:Label>
            <asp:TextBox ID="txtnumero" runat="server" 
            style="position:absolute; top: 337px; left: 122px; width: 262px;"></asp:TextBox>


            <asp:Label ID="lbldescricao" runat="server" Text="Descrição:" 
            style="position:absolute; top: 340px; left: 50px;"></asp:Label>
            <asp:TextBox ID="txtdescricao" runat="server" 
            style="position:absolute; top: 237px; left: 122px;"></asp:TextBox>








              <!--GridView -->
            <asp:GridView ID="grvlista" runat="server" 
                style="position:absolute; top: 417px; left: 36px; width:930px; height: 99px;" 
                AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" 
                BorderStyle="None" BorderWidth="1px" CellPadding="3" onpageindexchanging="grvlista_PageIndexChanging" 
            >
                <Columns>
                    <asp:TemplateField HeaderText="Selecionar" ShowHeader="False">
                        <ItemTemplate>
                            <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" 
                                CommandName="Select" Text="Select" 
                                CommandArgument='<%# Eval("cmpCoNumeracaoDocumento") %>'></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="N°" />
                    <asp:BoundField HeaderText="Obra" />
                    <asp:BoundField HeaderText="Autor" />
                    <asp:BoundField HeaderText="Tipo" />
                    <asp:BoundField HeaderText="Documento" />
                    <asp:BoundField HeaderText="Descrição" />
                </Columns>
                <FooterStyle BackColor="White" ForeColor="#000066" />
                <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                <RowStyle ForeColor="#000066" />
                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                <SortedAscendingHeaderStyle BackColor="#007DBB" />
                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                <SortedDescendingHeaderStyle BackColor="#00547E" />
            </asp:GridView>



            

            

       

            

      

      <asp:Repeater DataSource="" runat=server>
      <HeaderTemplate>
      
      </HeaderTemplate>
      </asp:Repeater>

            

            

       

            

    </div>
    

</asp:Content>

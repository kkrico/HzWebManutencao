<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="webCON_UsuPesquisa.aspx.cs" Inherits="HzWebManutencao.Configuracao.webCON_UsuPesquisa" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
       <div  style="position:relative; height: 456px; width: 911px; top: 12px; left: -54px; margin-left: 40px;">
            <asp:DropDownList ID="cmbPerfil" runat="server" 
                style="position:absolute; top: 8px; left: 508px; width: 204px; height: 24px;"
                AutoPostBack="True" onselectedindexchanged="cmbObra_SelectedIndexChanged"></asp:DropDownList>
            <asp:Label ID="Label6" runat="server" Text="Perfil:" 
                style="position:absolute; top: 10px; left: 469px; height: 17px; width: 44px;"></asp:Label>
            <asp:Label ID="Label7" runat="server" Text="Nome do Usuário:" 
                    
                style="position:absolute; top: 9px; left: 10px; height: 17px; width: 112px; right: 789px;"></asp:Label>
            <asp:TextBox ID="txtNome" runat="server"
                style="position:absolute; top: 9px; left: 129px; width: 329px;" 
                AutoPostBack="True" ontextchanged="txtNome_TextChanged"></asp:TextBox>
            <asp:Button ID="btnPesquisar" runat="server" Text="Pesquisar" 
                    style="position:absolute; top: 5px; left: 728px; width: 82px; height: 26px; right: 101px;" 
                    onclick="btnPesquisar_Click" />

            <asp:GridView ID="grdUsu" runat="server" 
                style="position:absolute; top: 49px; left: 12px; width: 883px;" AllowPaging="True" 
                AllowSorting="True" AutoGenerateColumns="False" 
                onpageindexchanging="grdUsu_PageIndexChanging" 
                onrowcommand="grdUsu_RowCommand" 
                ViewStateMode="Enabled" 
                CellPadding="3" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" 
                BorderWidth="1px" onrowdatabound="grdUsu_RowDataBound">
                <Columns>
                    <asp:TemplateField HeaderText="Nome Usuário">
                    <ItemStyle Width="300px" HorizontalAlign="Left"/>
                        <ItemTemplate>
                            <asp:LinkButton ID="lnk" runat="server" 
                            CommandArgument='<%# Eval("cmpCoUsuario") %>' CommandName="lnk"><%#Eval("cmpNoUsuario") %></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="cmpDcPerfil" HeaderText="Perfil">
                        <ItemStyle Width="50px" HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="cmpDcEmail" HeaderText="Email">
                    <ItemStyle Width="200px"></ItemStyle>
                    </asp:BoundField>

                   <asp:TemplateField HeaderText="Status" >
                        <ItemTemplate>
                            <asp:Label ID="lblStatus" runat="server" Text='Ativo'>
                            </asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle Width="80px" HorizontalAlign="Center"/>
                    </asp:TemplateField>

                   <asp:TemplateField Visible="False" >
                        <ItemTemplate>
                            <asp:Label ID="lblAtivo" runat="server" Text='<%# Bind("cmpInAtivo") %>'>
                            </asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle Width="80px" HorizontalAlign="Center"/>
                    </asp:TemplateField>
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


</div>

</asp:Content>

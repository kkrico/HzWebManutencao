<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="WebPesquisaDocumento.aspx.cs" Inherits="HzWebManutencao.Documentos.WebPesquisaDocumento" %>
    <asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

    </asp:Content>
    <asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

        <!--Divisão -->
        <div style="height: 446px; width: 1131px;">

            <!--Título -->
            <asp:Label ID="lblpesquisadoc" runat="server" Text="Documento" 
                style="position:absolute; top: 172px; left: 295px; height: 20px; width: 895px; text-align: center; margin-top: 0px;" 
                BackColor="White" Font-Bold="True" Font-Size="Medium" ForeColor="Maroon"></asp:Label>

            <!-- Caixa de seleção -->
            <asp:Label ID="lblautor" runat="server" Text="Autor" 
                style="position:absolute; top: 210px; left: 290px;"></asp:Label>
            <asp:DropDownList ID="ddlautor" runat="server" 
                
                style="position:absolute; top: 230px; left: 283px; height: 25px; width: 421px; right: 961px;" 
                onselectedindexchanged="ddlautor_SelectedIndexChanged">
            </asp:DropDownList>

            <!--Pesquisa -->
            <asp:Label ID="lblpesquisa" runat="server" Text="Pesquisa" 
                style="position:absolute; top: 269px; left: 285px; height: 18px; width: 48px;"></asp:Label>
            <asp:TextBox ID="txtpesquisa" runat="server" 
                style="position:absolute; top: 290px; left: 280px; width: 421px; right: 964px;"></asp:TextBox>

            <!--Descrição -->
            <asp:Label ID="lbldescricao" runat="server" Text="Descrição" 
                style="position:absolute; top: 320px; left: 281px;"></asp:Label>
            <asp:TextBox ID="txtdescricao" runat="server" 
                style="position:absolute; top: 340px; left: 280px; width: 421px; height: 75px;"></asp:TextBox>

            <!--Data /inicio e fim  -->
            <asp:Label ID="lblano" runat="server" Text="Ano" style="position:absolute; top: 210px; left: 1043px; width: 54px; height: 18px;"></asp:Label>
            <asp:TextBox ID="txtano" runat="server" style="position:absolute; top: 230px; left: 1031px;"></asp:TextBox>

            <%--<asp:Label ID="lbldatafim" runat="server" Text="Data Fim " style="position:absolute; top: 210px; left: 1190px; right: 605px; width: 54px; height: 18px;"></asp:Label>
            <asp:TextBox ID="txtdatafim" runat="server" style="position:absolute; top: 230px; left: 1182px;"></asp:TextBox>--%>


            <!--Numero -->
            <asp:Label ID="lblnumero" runat="server" Text="Número" style="position:absolute; top: 210px; left: 904px;"></asp:Label>
            <asp:TextBox ID="txtnumero" runat="server" style="position:absolute; top: 230px; left: 885px;"></asp:TextBox>

            <!--Lista -->
            <asp:Label ID="lbllista" runat="server" Text="lista" 
                style="position:absolute; top: 270px; left: 884px; height: 18px; width: 48px;"></asp:Label>
            <asp:DropDownList ID="ddllista" runat="server" style="position:absolute; top: 290px; left: 883px; height: 25px; width: 442px;">
            </asp:DropDownList>

            <!--botão pesquisar -->
            <asp:Button ID="btnpesquisar" runat="server" Text="Pesquisar" 
                
                style="position:absolute; top: 380px; left: 825px; height: 30px; width: 103px;" 
                onclick="btnpesquisar_Click" />

            <!--botão novo -->
            <asp:Button ID="btnnovo" runat="server" Text="Novo" 
                style="position:absolute; top: 380px; left: 938px; height: 30px; width: 103px;" />

            <!--botão salvar -->
            <asp:Button ID="btnsalvar" runat="server" Text="Salvar" 
                
                style="position:absolute; top: 380px; left: 1054px; height: 30px; width: 103px;" 
                onclick="btnsalvar_Click" />

            <!--GridView -->
            <asp:GridView ID="grvlista" runat="server" 
                style="position:absolute; top: 439px; left: 290px; width:930px; height: 99px;" 
                AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" 
                BorderStyle="None" BorderWidth="1px" CellPadding="3" 
                onselectedindexchanged="grvlista_SelectedIndexChanged">
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


            <!---PainelPopap-->
            <asp:Panel ID="divPop" runat="server" BackColor="White" BorderStyle="Solid" BorderWidth="1px" style="position:absolute; top: 336px; left: 1192px; height: 352px; width: 316px;">

                <!-----Cadastro----->
                <asp:Label ID="lblcadastro" runat="server" Text="Cadastro" BackColor="White" Font-Bold="True" Font-Size="Large" ForeColor="black" Font-Names="Calibri" style="position:absolute; top: 24px; left: 123px; height: 19px; width: 94px; "></asp:Label>

                <!----Tipo de documento---->
                <asp:Label ID="lbltipo" runat="server" Text="Tipo" 
                    style="position:absolute; top: 58px; left: 30px; height: 14px; width: 70px;"></asp:Label>
                <asp:DropDownList ID="ddltipo" runat="server" 
                    style="position:absolute; top: 81px; left: 24px; height: 13px; width: 261px;">
                </asp:DropDownList>

                <!----Seleção do autor----->
                <asp:Label ID="lblautor1" runat="server" Text="Autor" 
                    style="position:absolute; top: 108px; left: 31px; height: 13px; width: 58px;"></asp:Label>
                <asp:DropDownList ID="ddlautor1" runat="server" 
                    style="position:absolute; top: 122px; left: 26px; height: 11px; width: 253px;">
                </asp:DropDownList>

                <!--------Seleção da Obra --------->
                <asp:Label ID="lblobra" runat="server" Text="Obra" 
                    style="position:absolute; top: 148px; left: 33px; height: 15px; width: 56px;"></asp:Label>
                <asp:DropDownList ID="ddlobra" runat="server" 
                    style="position:absolute; top: 168px; left: 29px; height: 8px; width: 250px;">
                </asp:DropDownList>

                <!----Descrição---->
                <asp:Label ID="lbldescricao0" runat="server" Text="Descrição"  
                    style="position:absolute; top: 197px; left: 30px;"></asp:Label>
                <asp:TextBox ID="txtDescricaoCentroCusto" runat="server" style="position:absolute; top: 219px; left: 23px; height: 82px; width: 270px;" TextMode="MultiLine"></asp:TextBox>

                <!---Botões---->
                <asp:Button ID="Button1" runat="server" Text="Salvar" style="position:absolute; top: 102px; left: 180px; top: 314px; right: 50px; width: 108px; height: 30px;" onclick="btnSalvar_Click" />
                <asp:Button ID="btnFechar" runat="server" Text="Fechar" style="position:absolute; left: 31px; top: 313px; right: 196px; width: 111px; height: 30px;" />

            </asp:Panel>
        </div>

    </asp:Content>
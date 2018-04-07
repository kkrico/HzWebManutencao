<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="WebCadastroArea.aspx.cs" Inherits="HzWebManutencao.Tabelas_Apoio.WebCadastroArea" %>
    <asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    </asp:Content>
    <asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
        <div style="height: 648px; width: 1125px;">

            <asp:Panel ID="pnlCadastroArea" runat="server" 
                style="position:absolute; left:406px; width:1088px; height:165px; top: 131px;" 
                BorderColor="#CCCCCC" BorderStyle="Outset">

                <!------ Título------->
                <asp:Label ID="lblAcao" runat="server" Text="Cadastro de Area" 
                    style="position:absolute; top: 1px; left: 103px; height: 20px; width: 849px; text-align: center;" 
                    BackColor="White" Font-Bold="True" Font-Size="X-Large" ForeColor="Maroon"></asp:Label>

                <!------ Descrição area------->
                <asp:Label ID="lbldescriçãodaarea" runat="server" Text="Descrição da Area" 
                    style="position:absolute; top: 41px; left: 568px; height: 14px;"></asp:Label>
                <asp:TextBox ID="txtdescricaodaarea" runat="server" 
                    style="position:absolute; top: 59px; left: 565px; height: 23px; width: 502px; right: 13px;" 
                    ontextchanged="txtdescricaodaarea_TextChanged"></asp:TextBox>

                <%--<asp:DropDownList ID="ddldescricaodaarea" runat="server" style="position:absolute; top: 257px; left: 363px; height: 23px; width: 477px;">
                    </asp:DropDownList>--%>


                    <!------ Obra------->
                    <asp:Label ID="lblobra" runat="server" Text="Obra" 
                    
                    
                    style="position:absolute; top: 41px; left: 19px; height: 15px; width: 42px;"></asp:Label>
                    <asp:DropDownList ID="cmbObra" runat="server" 
                    style="position:absolute; top: 60px; left: 17px; height: 26px; width: 529px; right: 534px;" 
                    onselectedindexchanged="cmbObra_SelectedIndexChanged">
                    </asp:DropDownList>

                    <!------ button Pesquisar------->
                    <asp:Button ID="btnpesquisar" runat="server" Text="Pesquisar" 
                    style="position:absolute; top: 103px; left: 566px; height: 30px; width: 103px; right: 419px;" 
                    onclick="btnpesquisar_Click" />

                    <!------ button Novo------->
                    <asp:Button ID="btnnovo" runat="server" Text="Novo" 
                    style="position:absolute; top: 103px; left: 680px; height: 30px; width: 103px;" 
                    onclick="btnnovo_Click" />

            </asp:Panel>

            <!------ Gridview------->

            <asp:GridView ID="grvarea" runat="server" 
                style="position:absolute; top: 343px; height: 143px; width: 964px; left: 432px;" 
                AutoGenerateColumns="False" CellPadding="3" BackColor="White" 
                BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px"  
                ViewStateMode="Enabled" 
                >
                <Columns>
                    <asp:TemplateField HeaderText="Operação">
                        <EditItemTemplate>
                            <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="True" CommandName="Update" Text="Update"></asp:LinkButton>
                            &nbsp;
                            <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel"></asp:LinkButton>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtnincluir" runat="server" CausesValidation="False" 
                                CommandName="Edit" Text="Incluir" CommandArgument='<%# Eval("cmpIdArea") %>' 
                                onclick="lbtnincluir_Click"></asp:LinkButton>
                            &nbsp;
                            <asp:LinkButton ID="lbtneditar" runat="server" CausesValidation="False" 
                                CommandName="Edit" Text="Editar" CommandArgument='<%# Eval("cmpIdArea") %>' 
                                onclick="lbtneditar_Click"></asp:LinkButton>
                            &nbsp;
                            <asp:LinkButton ID="lbtnexcluir" runat="server" CausesValidation="False" 
                                CommandName="Edit" Text="Excluir" CommandArgument='<%# Eval("cmpIdArea") %>' 
                                onclick="lbtnexcluir_Click"></asp:LinkButton>
                        </ItemTemplate>
                        <FooterStyle HorizontalAlign="Right" />
                        <ItemStyle Width="120px" />
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="Nome" DataField="cmpNome" />
                    <asp:BoundField HeaderText="Tag" DataField="cmpTag" />
                    <asp:BoundField HeaderText="Pavimento" DataField="cmpDcPavimento" />
                    <asp:BoundField HeaderText="Metros" DataField="cmpMetros" />
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



            <!------ ModalPopupExtender------->
             <asp:ModalPopupExtender 
            ID="ModArea" 
            runat="server" 
            TargetControlID="btnsalvar"
            PopupControlID="btnfechar"
            CancelControlID="btnfechar" 
            BackgroundCssClass="modalBackground"></asp:ModalPopupExtender>
            <!------ PainelPopap------->
            <asp:Panel ID="divPop" runat="server" BackColor="White" BorderStyle="Solid" 
                BorderWidth="1px" 
                style="position:absolute; top: 490px; left: 475px; height: 310px; width: 290px;" 
                Visible="False">
           

            <!------ TituloPopap------->
            <asp:Label ID="lblcadastro" runat="server" Text="Cadastro Area" style="position:absolute; top: 518px; left: 554px; height: 20px; width: 130px; text-align: center;" BackColor="White" Font-Bold="True" Font-Size="Medium" ForeColor="Maroon"></asp:Label>

            <!------ NomePopap------->
            <asp:Label ID="lblnome" runat="server" Text="Nome" style="position:absolute; top: 561px; left: 499px;"></asp:Label>
            <asp:TextBox ID="txtnome" runat="server" style="position:absolute; top: 575px; left: 495px; width: 233px;"></asp:TextBox>

            <!------ TagPopap------->
            <asp:TextBox ID="txttag" runat="server" style="position:absolute; top: 660px; left: 496px; width: 228px;"></asp:TextBox>
            <asp:Label ID="lbltag" runat="server" Text="TAG" style="position:absolute; top: 644px; left: 498px; height: 10px; width: 39px;"></asp:Label>


            <!------ PavimentoPopap------->
            <asp:Label ID="lblpavimento" runat="server" Text="Pavimento" style="position:absolute; top: 602px; left: 499px; height: 10px; width: 39px;"></asp:Label>
            <asp:TextBox ID="txtpavimento" runat="server" style="position:absolute; top: 616px; left: 496px; width: 228px;"></asp:TextBox>

            <!------ MetrosPopap------->
            <asp:Label ID="lblmetros" runat="server" Text="Metros" style="position:absolute; top: 689px; left: 499px; height: 10px; width: 39px;"></asp:Label>
            <asp:TextBox ID="txtmetros" runat="server" style="position:absolute; top: 704px; left: 496px; width: 96px;"></asp:TextBox>

            <!------ buttoSalvar------->
            <asp:Button ID="btnsalvar" runat="server" Text="Salvar" 
                style="position:absolute; top: 738px; left: 498px; height: 30px; width: 103px;" 
                onclick="btnsalvar_Click" />

            <!------ buttofechar------->
            <asp:Button ID="btnfechar" runat="server" Text="Fechar" Width="63px" 
                
                style="position:absolute;  left: 625px; top: 738px; right: 1121px; height: 30px; width: 103px;" />
 </asp:Panel>

             <!-----Como Modelo------->
            <!------ ModalPopupExtender------->
            <%-- <asp:ModalPopupExtender 
            ID="ModObra" 
            runat="server" 
            TargetControlID="Button1"//btnSalvar 
            PopupControlID="divObra"
            CancelControlID="btnFechar" 
            BackgroundCssClass="modalBackground"></asp:ModalPopupExtender>--%>
             <!------ Panel------->
         <%--<asp:Panel ID="divObra" runat="server" BackColor="White" BorderStyle="Solid" 
                    BorderWidth="1px" 

            
                style="position:absolute; top: 496px; left: 849px; height: 261px; width: 348px;" >

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
               <asp:Button ID="Button1" runat="server" Text="Salvar"
                    
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
            </asp:Panel>--%>



        </div>
    </asp:Content>
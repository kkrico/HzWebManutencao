<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="webFAT_CadastroEmail.aspx.cs" Inherits="HzWebManutencao.Faturamento.webFAT_CadastroEmail" %>
<%@ Register src="~/UserControls/OKMessageBox.ascx" tagname="MsgBox" tagprefix="asp"%>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<asp:MsgBox ID="MsgBox" runat="server" />
<div runat="server" id="divstyle" style="position:relative; height: 464px; width: 1162px; top: 6px; left: -54px; margin-left: 40px;">
        <asp:Label ID="lblAcao" runat="server" Text="Cadastro Email Faturamento"
            style="position:absolute; top: 4px; left: 6px; height: 21px; width: 1150px; text-align: center;" 
            BackColor="White" Font-Bold="True" Font-Size="Large" ForeColor="Maroon" 
            Font-Names="Calibri"></asp:Label>

        <asp:Label ID="lblLotacao" runat="server" Text="Lotação" Font-Size="X-Small" 
            style="position:absolute; top: 25px; left: 20px; height: 15px; width: 136px;"> </asp:Label>
        <asp:RadioButtonList ID="rdbData" runat="server" Font-Size="X-Small" 
            style="position:absolute; top: 37px; left: 11px; width: 184px; height: 30px;" AutoPostBack="True" 
            RepeatColumns="3"  RepeatDirection="Horizontal" 
            onselectedindexchanged="btnPesquisar_Click"  >
            <asp:ListItem Value="1">Escritório</asp:ListItem>
            <asp:ListItem Value="0">Obras</asp:ListItem>
            <asp:ListItem Selected="True" Value="T">Todas</asp:ListItem>
        </asp:RadioButtonList>

       <asp:Label ID="lblPerfil" runat="server" Text="Perfil" Font-Size="X-Small" 
            style="position:absolute; top: 25px; left: 225px; height: 15px; width: 136px;"> </asp:Label>
        <asp:RadioButtonList ID="rdbPerfilEmail" runat="server" Font-Size="X-Small" 
            style="position:absolute; top: 37px; left: 216px; width: 658px; height: 30px;" AutoPostBack="True" 
            RepeatColumns="7" RepeatDirection="Horizontal" onselectedindexchanged="btnPesquisar_Click">
            <asp:ListItem Value="1">Diretor</asp:ListItem>
            <asp:ListItem Value="2">Gerente</asp:ListItem>
            <asp:ListItem Value="3">Coordenador</asp:ListItem>
            <asp:ListItem Value="4">Engenheiro Responável Obra</asp:ListItem>
            <asp:ListItem Value="5">Setor Faturamento</asp:ListItem>
            <asp:ListItem Value="6">Assistente Administrativo</asp:ListItem>
            <asp:ListItem Selected="True" Value="T">Todos</asp:ListItem>
        </asp:RadioButtonList>

        <asp:Button ID="btnPesquisar" runat="server" Text="Pesquisar" 
            CausesValidation="False" style="position:absolute; top: 40px; left: 1014px; width: 107px; height: 23px; right: 41px;" 
                onclick="btnPesquisar_Click" />

        <asp:Button ID="btnNovo" runat="server" Text="Novo Email"  CausesValidation="False" onclick="btnNovo_Click"
            style="position:absolute; top: 40px; left: 896px; width: 107px; height: 23px; right: 159px;" />

          <asp:GridView ID="gvDados" runat="server" style="position:absolute; top: 81px; left: 3px; width: 1123px;" 
            BackColor="LightGoldenrodYellow" BorderColor="Tan" BorderWidth="5px" 
            CellPadding="3" ForeColor="Black" GridLines="None" BorderStyle="None" 
            CellSpacing="2" Font-Size="X-Small" AutoGenerateColumns="False" 
            onrowdatabound="gvDados_RowDataBound">
            <FooterStyle BackColor="Tan" />
            <SelectedRowStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
            <PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" HorizontalAlign="Left" />
            <HeaderStyle BackColor="Tan" Font-Bold="True" />
            <AlternatingRowStyle BackColor="PaleGoldenrod" />
            <Columns>
                <asp:TemplateField HeaderText="Editar">
                <ItemStyle Width="20px" HorizontalAlign="Left" Font-Size="X-Small" />
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkEditar" runat="server" Text="Editar" CommandArgument='<%# Eval("cmpIdFaturaEmail") %>' CommandName="lnkEditar" OnClick="lnkEditar_Click" CausesValidation="False"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>                
                
                <asp:TemplateField HeaderText="Excluir">
                <ItemStyle Width="15px" HorizontalAlign="Left" Font-Size="X-Small" />
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkExcluir" runat="server" Text="Excluir" CommandArgument='<%# Eval("cmpIdFaturaEmail") %>' CommandName="lnkExcluir" OnClick="lnkExcluir_Click" CausesValidation="False"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>   
               
                <asp:BoundField DataField="cmpNoFuncionario"       HeaderText="Nome Funcionário">
                    <ItemStyle Width="200px" HorizontalAlign="Left" Font-Size="X-Small" />
                </asp:BoundField>

                <asp:BoundField DataField="cmpDcEmail"       HeaderText="Email Funcionário">
                    <ItemStyle Width="200px" HorizontalAlign="Left" Font-Size="X-Small" />
                </asp:BoundField>
               
                <asp:BoundField DataField="cmpDcPerfil"       HeaderText="Perfil Email">
                    <ItemStyle Width="150px" HorizontalAlign="Left" Font-Size="X-Small" />
                </asp:BoundField>
                                
 <%--               <asp:BoundField DataField="cmpNoObraFatura"       HeaderText="Nome da Obra">
                    <ItemStyle Width="300px" HorizontalAlign="Left" Font-Size="X-Small" />
                </asp:BoundField>--%>

            </Columns>
        </asp:GridView>

        <asp:Button ID="btnPopUpEditar" runat="server" style="display:none" />
            <asp:ModalPopupExtender 
                ID="ModalPopupExtender2" 
                runat="server" 
                TargetControlID="btnPopUpEditar" 
                PopupControlID="pnlPopUpEditar"
                BackgroundCssClass="modalBackground"></asp:ModalPopupExtender>

        <asp:Panel ID="pnlPopUpEditar" runat="server" BackColor="LightGoldenrodYellow" 
                    Height="479px" Width="738px" BorderStyle="Solid" BorderWidth="1px">
            <asp:Label ID="Label9" runat="server" Text="Editar Dados Email Funcionário"
                        style="position:absolute; top: 5px; left: 7px; height: 20px; width: 676px; font-size:medium; text-align: center;" 
                        BackColor="LightGoldenrodYellow" Font-Bold="True" Font-Size="Large" 
                        ForeColor="#3333FF" Font-Names="Calibri"></asp:Label>
        
            <asp:Label ID="lblFuncionario" runat="server" Text="Nome Funcionário:"
                        style="position:absolute; top: 39px; left: 18px; width: 109px; right: 1035px;" 
                        Font-Bold="True"></asp:Label>
            <asp:TextBox ID="txtNomeFuncionario" runat="server" 
                        style="position:absolute; top: 35px; left: 133px; width: 541px;" 
                Font-Names="Calibri" Font-Size="Small"></asp:TextBox>
            <asp:Label ID="Label13" runat="server" Text="Descrição do Email:"
                style="position:absolute; top: 69px; left: 18px; height: 17px; width: 109px;" 
                Font-Bold="True"></asp:Label>
            <asp:TextBox ID="txtDcEmail" runat="server" 
                        style="position:absolute; top: 65px; left: 133px; width: 541px;" 
                Font-Names="Calibri" Font-Size="Small"></asp:TextBox>

            <asp:Label ID="Label12" runat="server" Text="Perfil Email:"
                        style="position:absolute; top: 101px; left: 18px; height: 23px; width: 100px;" 
                        Font-Bold="True" ></asp:Label>

           <asp:RadioButtonList ID="rdbPerfil" runat="server"  
                Font-Bold="True"
                style="position:absolute; top: 94px; left: 133px; width: 540px; height: 30px;" 
                RepeatColumns="3" RepeatDirection="Horizontal">
                <asp:ListItem Value="1">Diretor</asp:ListItem>
                <asp:ListItem Value="2">Gerente</asp:ListItem>
                <asp:ListItem Value="3">Coordenador</asp:ListItem>
                <asp:ListItem Value="4">Engenheiro Responável Obra</asp:ListItem>
                <asp:ListItem Value="5">Setor Faturamento</asp:ListItem>
                <asp:ListItem Value="6">Assistente Administrativo</asp:ListItem>
            </asp:RadioButtonList>

            <asp:Button ID="btnSave" CommandName="Save" runat="server" Text="Gravar" ForeColor="#3333FF" Font-Bold="True"
                        style="position:absolute; top: 151px; left: 18px; width: 70px; height: 30px;" 
                        CausesValidation="false" onclick="btnSave_Click"/>
            <asp:Button ID="btnVoltar" runat="server" Text="Voltar"  ForeColor="#3333FF" Font-Bold="True" 
                        CausesValidation="false" onclick="btnVoltar_Click" 
                style="position:absolute; top: 152px; left: 100px; width: 70px; height: 30px;"/>

            <asp:Label ID="Label5" runat="server" Text="Área de Negócio:" 
                Font-Bold="True"
                
                style="position:absolute; top: 200px; left: 18px; height: 15px; width: 103px;"></asp:Label>
            <asp:DropDownList ID="cmbTipoServico" runat="server" Font-Size="Small"
                style="position:absolute; top: 196px; left: 133px; width: 420px; height: 18px;"
                onselectedindexchanged="btnPesqAreaNegocio_Click" AutoPostBack="True">
            </asp:DropDownList>
            <asp:Button ID="btnPesqAreaNegocio" CommandName="Save" runat="server" 
                Text="Filtrar Área Negócio" ForeColor="#3333FF" Font-Bold="True"
                        style="position:absolute; top: 193px; left: 563px; width: 129px; height: 24px;" 
                        CausesValidation="false" onclick="btnPesqAreaNegocio_Click"/>

            <asp:ListBox ID="lstObras" runat="server" Font-Size="Small" 
                SelectionMode="Multiple" 
                
                style="position:absolute; top: 241px; left: 17px; width: 315px; height: 213px;">
            </asp:ListBox>
            <asp:ListBox ID="lstObraFunc" runat="server" Font-Size="Small" 
                SelectionMode="Multiple" 
                
                style="position:absolute; top: 241px; left: 402px; width: 315px; height: 209px;">
            </asp:ListBox>
            <asp:Button ID="btnDesvincular" runat="server" CausesValidation="False" 
                Font-Bold="True" Font-Italic="False" Font-Names="Calibri" Font-Size="Medium" 
                onclick="btnDesvincular_Click" 
                style="position:absolute; top: 362px; left: 343px; width: 49px; height: 25px; bottom: 77px;" 
                Text="&lt;&lt;" />
            <asp:Button ID="btnVincular" runat="server" CausesValidation="False" 
                Font-Bold="True" Font-Names="Calibri" Font-Size="Medium" 
                onclick="btnVincular_Click" 
                style="position:absolute; top: 313px; left: 341px; width: 49px; height: 25px; bottom: 126px;" 
                Text="&gt;&gt;" />
            <asp:Label ID="lblTipoDoc" runat="server" Font-Bold="True" Font-Size="Small" 
                style="position:absolute; top: 225px; left: 20px; height: 17px; width: 151px; " 
                Text="Obras"></asp:Label>
            <asp:Label ID="lblDocAnexados" runat="server" Font-Bold="True" 
                Font-Size="Small" 
                style="position:absolute; top: 225px; left: 405px; height: 17px; width: 151px; right: 606px;" 
                Text="Obras do Funcionário"></asp:Label>
            </asp:Panel>


    </div>
</asp:Content>

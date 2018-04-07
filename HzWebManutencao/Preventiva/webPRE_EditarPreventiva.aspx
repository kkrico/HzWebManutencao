<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="webPRE_EditarPreventiva.aspx.cs" Inherits="HzWebManutencao.Preventiva.webPRE_EditarPreventiva" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server" >

  
    

    <br/> 
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br/> 
    <asp:GridView ID="grdPreventiva" runat="server" 
        
        
        
        
        style="position:absolute; top: 143px; height: 286px; width: 906px; left: 619px;"  AutoGenerateColumns="False" 
        onrowcreated="grdPreventiva_RowCreated">
        <Columns>
            <asp:BoundField DataField="cmpDcItemAtividadePreventiva">
            <HeaderStyle Width="500px" />
            </asp:BoundField>
            <asp:CheckBoxField DataField="cmpCkValor" HeaderText="Realizado" />
            <asp:CheckBoxField DataField="cmpValores" HeaderText="Campo de Valor" />
            <asp:BoundField DataField="cmpValor" HeaderText="VALOR" />
            <asp:BoundField DataField="cmpValorMin" HeaderText="MIN" />
            <asp:BoundField DataField="cmpValorMedio" HeaderText="MED" />
            <asp:BoundField DataField="cmpValorMax" HeaderText="MAX" />
        </Columns>
    </asp:GridView>


    <br/> <br/> <br/> <br/> <br/> <br/> 
    <br />
    <br/>
    <asp:GridView ID="grdPreventivaEquipamento" runat="server" 
        style="position:absolute; top: 141px; left: 375px; width: 238px;" 
        AutoGenerateColumns="False" 
        onselectedindexchanged="grdPreventivaEquipamento_SelectedIndexChanged" 
        onrowcommand="grdPreventivaEquipamento_RowCommand" 
        onrowcreated="grdPreventivaEquipamento_RowCreated" AllowPaging="True" 
        onpageindexchanging="grdPreventivaEquipamento_PageIndexChanging" 
        ViewStateMode="Enabled">
        <Columns>
            <asp:TemplateField ShowHeader="False">
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" 
                        CommandName="Select" CommandArgument='<%# Eval("cmpCoGrupoAtividade") %>' 
                        Text='Selecionar'></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="cmpDcEquipamentoObra" HeaderText="Equipamento" />
        </Columns>
    </asp:GridView>
    

</asp:Content>

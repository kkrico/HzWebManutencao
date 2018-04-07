<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="web_CalendarioPreventiva.aspx.cs" Inherits="HzWebManutencao.Tabelas_Apoio.web_CalendarioPreventiva" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div style="height: 536px; width: 1136px; margin-right: 13px;">
    <div style="position:relative; top: -19px; left: -6px; width: 1155px; right: 6px; margin-top: 0px; height: 39px;" >


                <asp:Button ID="btnImprimir" runat="server" Text="Imprimir" 
        style="position:relative; top: 17px; left: 1057px; height: 24px; width: 97px; right: -1057px; margin-top: 0px;" 
                CausesValidation="False" onclick="btnImprimir_Click" />


    <asp:Label ID="lblobra" runat="server" Text="Obra:"
    style="position:absolute; top: 16px; left: 46px; height: 14px; right: 1081px;" ForeColor="Black"
    ></asp:Label>
    <asp:DropDownList ID="cmbObra" runat="server" 
    
        style="position:relative; top: 11px; left: -9px; height: 20px; width: 269px; right: -260px; margin-top: 0px;" 
        AutoPostBack="True" onselectedindexchanged="cmbObra_SelectedIndexChanged"
        >                    
                </asp:DropDownList>


                <asp:Button ID="btnproximo" runat="server" Text="&gt;&gt;" 
                style="position:relative; top: 19px; left: 305px; height: 24px; width: 42px; right: -305px;" 
                CausesValidation="false" onclick="btnproximo_Click" />

                <asp:Button ID="btnretornar" runat="server" Text="&lt;&lt;" 
                style="position:relative; top: 20px; left: 81px; height: 24px; width: 42px; right: -81px;" 
                CausesValidation="false" onclick="btnretornar_Click" />

                                <asp:Button ID="btnAno" runat="server" Text="Ano" 
                style="position:relative; top: 17px; left: 320px; height: 24px; width: 97px; right: -320px;" 
                CausesValidation="false" onclick="btnAno_Click" />

                <asp:Label ID="lblmes_ano" runat="server" Text="Mes_Ano" 
                 
                    style="position:relative; top: 22px; left: 12px; height: 20px; width: 84px; right: -12px; margin-top: 0px;" 
                    BorderStyle="None" Font-Bold="True" ForeColor="Black" 
                    Font-Underline="True"></asp:Label>

               <asp:Button ID="btnfiltro" runat="server" Text="Filtro" style="position:relative; top: 17px; left: 252px; width: 97px; right: -252px;"  
                CausesValidation="false" onclick="btnfiltro_Click"  />


</div>
    
    <asp:GridView ID="grvCalendario" runat="server" AutoGenerateColumns="False" 
        onselectedindexchanged="GridView1_SelectedIndexChanged" Width="1143px" 
        onrowdatabound="grvCalendario_RowDataBound" 
            style="position:relative;margin-bottom: 0px; margin-top: 27px; margin-right: 0px; margin-left: 0px; top: -33px; left: 0px; height: 468px;" 
            EnableModelValidation="False" BorderStyle="None">
        <Columns>
            <asp:TemplateField HeaderText="Domingo">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Domingo") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>

                    <asp:HyperLink ID="hpDomingo" runat="server">1</asp:HyperLink>
                    <asp:Label ID="lblDomingo" runat="server" Font-Size="XX-Small">1</asp:Label>

                </ItemTemplate>
                <HeaderStyle Width="100px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Segunda">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("Segunda") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:HyperLink ID="hpSegunda" runat="server">HyperLink</asp:HyperLink>
                    <br />
                    <asp:Label ID="lblSegunda" runat="server" Font-Size="XX-Small"></asp:Label>
                </ItemTemplate>
                <HeaderStyle Width="100px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Terça">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("Terca") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:HyperLink ID="hpTerca" runat="server">HyperLink</asp:HyperLink>
                    <br />
                    <asp:Label ID="lblTerca" runat="server" Font-Size="XX-Small"></asp:Label>
                </ItemTemplate>
                <HeaderStyle Width="100px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Quarta">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("Quarta") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:HyperLink ID="hpQuarta" runat="server">HyperLink</asp:HyperLink>
                    <br />
                    <asp:Label ID="lblQuarta" runat="server" Font-Size="XX-Small"></asp:Label>
                </ItemTemplate>
                <HeaderStyle Width="100px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Quinta">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox5" runat="server" Text='<%# Bind("Quinta") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:HyperLink ID="hpQuinta" runat="server">[hpQuinta]</asp:HyperLink>
                    <br />
                    <asp:Label ID="lblQuinta" runat="server" Font-Size="XX-Small"></asp:Label>
                </ItemTemplate>
                <HeaderStyle Width="100px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Sexta">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox6" runat="server" Text='<%# Bind("Sexta") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:HyperLink ID="hpSexta" runat="server">[hpSexta]</asp:HyperLink>
                    <br />
                    <asp:Label ID="lblSexta" runat="server" Font-Size="XX-Small"></asp:Label>
                </ItemTemplate>
                <HeaderStyle Width="100px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Sabado">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox7" runat="server" Text='<%# Bind("Sabado") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:HyperLink ID="hpSabado" runat="server">[hpSabado]</asp:HyperLink>
                    <br />
                    <asp:Label ID="lblSabado" runat="server" Font-Size="XX-Small"></asp:Label>
                </ItemTemplate>
                <HeaderStyle Width="100px" />
            </asp:TemplateField>
        </Columns>
        <EditRowStyle Wrap="False" />
        <HeaderStyle Height="40px" />
        <RowStyle VerticalAlign="Top" />
        <SelectedRowStyle HorizontalAlign="Left" VerticalAlign="Top" />
    </asp:GridView>

</div>
        <asp:Button ID="btnShowPopup2" runat="server" style="display:none" />
                    <asp:ModalPopupExtender 
                ID="ModalPopupExtender2" 
                runat="server" 
                TargetControlID="btnShowPopup2" 
                PopupControlID="pnlpopup2"
                BackgroundCssClass="modalBackground"></asp:ModalPopupExtender>

                <asp:Panel ID="pnlpopup2" runat="server" BackColor="White" 
        style="position:absolute; top: 218px; left: 225px; width: 461px; height: 473px;"  
        BorderStyle="Solid" BorderWidth="1px">
        <div style="position:relative; top: -36px; left: 2px; width: 242px; right: 4px; margin-top: 0px; height: 264px;">

    &nbsp;<asp:Label ID="lblAtivdiades" runat="server" Text="Atividades Da Obra" 
                
                style="position:absolute; top: 58px; left: 152px; height: 19px; right: -92px; bottom: 187px;" 
                Font-Bold="True" ForeColor="Black" Font-Size="Medium"
    ></asp:Label>

    <asp:GridView ID="grvAtividades" runat="server" AutoGenerateColumns="False"  
                  CellPadding="4" GridLines="None"
                   
                  
                
                
                
                style="position:relative; margin-right: 0px; top: 112px; left: 23px; width: 189px; height: 263px; margin-top: 0px;" 
                BorderStyle="Solid" AllowPaging="True" 
                onpageindexchanged="grvAtividades_PageIndexChanged" 
                onpageindexchanging="grvAtividades_PageIndexChanging" PageSize="6" 
                >

        <Columns>
            <asp:TemplateField HeaderText="Atividade">
                <EditItemTemplate>
                    <asp:CheckBox ID="CheckBox1" runat="server" 
                        Checked='<%# Bind("cmpDcTipoAtividade") %>' />
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:CheckBox ID="ckAtividade" runat="server" 
                        Text='<%# Bind("cmpDcTipoAtividade") %>' 
                        ToolTip='<%# Bind("cmpCoTipoAtividade") %>' Checked="True" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>

    </asp:GridView>


    <asp:GridView ID="grvPeriodicidade" runat="server" AutoGenerateColumns="False"  
                  CellPadding="4" GridLines="None"     
                style="position:relative; margin-right: 0px; top: -150px; left: 248px; width: 186px; height: 263px; margin-top: 0px;" 
                BorderStyle="Solid"            >

        <Columns>
            <asp:TemplateField HeaderText="Periodicidade">
            
                <EditItemTemplate>
                    <asp:CheckBox ID="CheckBox1" runat="server" 
                        Checked='<%# Bind("cmpCoPeriodicidade") %>' />
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:CheckBox ID="ckPeriodicidade" runat="server" 
                        Text='<%# Bind("cmpDcPeriodicidade") %>' 
                        ToolTip='<%# Bind("cmpCoPeriodicidade") %>' Checked="True" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>

    </asp:GridView>



    <asp:Button ID="BtnOk" runat="server" Text="OK" 
                style="position:relative; top: -101px; left: 203px; height: 30px; width: 60px;" 
                onclick="BtnOk_Click"  />

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;


</div>
</asp:Panel>

</asp:Content>

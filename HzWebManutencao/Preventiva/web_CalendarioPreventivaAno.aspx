<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="web_CalendarioPreventivaAno.aspx.cs" Inherits="HzWebManutencao.Tabelas_Apoio.web_CalendarioPreventivaAno" %>

        <asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
        </asp:Content>

        <asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

        <div style="height: 507px; width: 1142px; margin-right: 10px;"/>

        <div style="position:relative; top:-2px; left: -965px; width: 1155px; right: 965px; margin-top: 1px; height: 45px; margin-left: 960px;" >


        <asp:Label ID="lblObra" runat="server" Text="Obra:" 
        style="position:absolute; top: 9px; left: 46px; height: 14px; right: 1081px;" 
                ForeColor="Black"></asp:Label>


        <asp:DropDownList ID="cmbObra" runat="server" 
        style="position:relative; top: 5px; left: 83px; height: 18px; width: 261px; right: -83px; margin-top: 0px;" 
        AutoPostBack="True" onselectedindexchanged="cmbObra_SelectedIndexChanged"
        ></asp:DropDownList>


              <asp:button ID="btnFitro" runat="server" Text="Filtro" 
              style="position:relative; top: 8px; left: 651px; height: 23px; width: 92px; right: -651px; margin-top: 0px;"
              CausesValidation="false" onclick="btnfiltro_Click"/>


             
              <asp:button ID="btnImprimir" runat="server" Text="Imprimir" 
              style="position:relative; top: 8px; left: 703px; height: 23px; width: 87px; right: -703px; margin-top: 0px;"
              CausesValidation="False" onclick="btnImprimir_Click"/>

               
                <asp:Button ID="btnproximo" runat="server" Text="&gt;&gt;" 
                style="position:relative; top: 8px; left: 237px; height: 24px; width: 42px; right: -237px;" 
                CausesValidation="false" onclick="btnproximo_Click" />


                <asp:Button ID="btnretornar" runat="server" Text="&lt;&lt;" 
                style="position:relative; top: 8px; left: -5px; height: 24px; width: 42px; right: 5px;" 
                CausesValidation="false" onclick="btnretornar_Click" />



                <asp:Button ID="btnmes" runat="server" Text="Mês" 
                style="position:relative; top: 8px; left: 269px; height: 24px; width: 97px; right: -269px;" 
                CausesValidation="false" onclick="btnmes_Click" />



             <asp:Label ID="lblAno" runat="server" Text="Ano" CssClass="bold" 
                style="position:relative; top: 8px; left: -65px; height: 18px; width: 84px; right: 65px; margin-top: 0px;" 
                    BorderStyle="None" Font-Bold="True" ForeColor="Black" 
                Font-Strikeout="False" Font-Underline="True" 
                ></asp:Label>

               

             <%--<asp:Label ID="lblAno" runat="server" Text="Anual" 
              style="position:relative; top: -3px; left: -643px; height: 24px; width: 85px; right: 643px;" 
              BorderStyle="None" Font-Bold="True" Font-Size="Small" 
              ForeColor="#666666"></asp:Label>--%>


            


   </div>


   <asp:GridView ID="grvCalendarioAno" runat="server" AutoGenerateColumns="False" 
           style="position:relative;margin-bottom: 0px; margin-top: 46px; margin-right: 0px; margin-left: 0px; top: -47px; left: -3px; height: 453px; width: 1154px;"  
            EnableModelValidation="False" CaptionAlign="Top" HorizontalAlign="Center" 
                onrowdatabound="grvCalendarioAno_RowDataBound" ShowHeader="False">
        <Columns>
            <asp:TemplateField>
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Col1") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:HyperLink ID="hpCol1" runat="server">HyperLink</asp:HyperLink>
                    <br />
                    <asp:Label ID="lblCol1" runat="server" Font-Size="X-Small"></asp:Label>

                </ItemTemplate>

                <HeaderStyle Width="100px" />

            </asp:TemplateField>
            <asp:TemplateField>
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("Col2") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:HyperLink ID="hpCol2" runat="server">HyperLink</asp:HyperLink>
                    <br />
                    <asp:Label ID="lblCol2" runat="server" Font-Size="X-Small"></asp:Label>
                </ItemTemplate>
                <HeaderStyle Width="100px" />

            </asp:TemplateField>

            <asp:TemplateField>
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("Col3") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:HyperLink ID="hpCol3" runat="server">HyperLink</asp:HyperLink>
                    <br />
                    <asp:Label ID="lblCol3" runat="server" Font-Size="X-Small"></asp:Label>
                </ItemTemplate>

                <HeaderStyle Width="100px" />

            </asp:TemplateField>

            
            
        </Columns>
        <PagerSettings Position="Top" />
        <RowStyle HorizontalAlign="Left" VerticalAlign="Top" />
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
        style="position:absolute; top: 239px; left: 378px; width: 460px; height: 473px;"  
        BorderStyle="Solid" BorderWidth="1px">
        <div style="position:relative; top: -36px; left: 2px; width: 242px; right: 4px; margin-top: 0px; height: 264px;">

    &nbsp;<asp:Label ID="lblAtivdiades" runat="server" Text="Atividades Da Obra" 
                
                style="position:absolute; top: 64px; left: 134px; height: 19px; right: -62px; bottom: 181px;" 
                Font-Bold="True" ForeColor="Black" Font-Size="Medium"
    ></asp:Label>

    <asp:GridView ID="grvAtividades" runat="server" AutoGenerateColumns="False"  
                  CellPadding="4" GridLines="None"
                   
                  
                
                
                
                style="position:relative; margin-right: 0px; top: 111px; left: 32px; width: 189px; height: 263px;" 
                BorderStyle="Solid" AllowPaging="True" 
                onpageindexchanged="GridView1_SelectedIndexChanged" 
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
                style="position:relative; margin-right: 0px; top: -147px; left: 245px; width: 185px; height: 255px;" 
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
                style="position:relative; top: -121px; left: 204px; height: 30px; width: 60px;" 
                onclick="BtnOk_Click"  />

</div>
</asp:Panel>

</asp:Content>


 



                
                




             






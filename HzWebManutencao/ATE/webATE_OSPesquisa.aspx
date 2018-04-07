<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="webATE_OSPesquisa.aspx.cs" Inherits="HzWebManutencao.ATE.webATE_OSPesquisa" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div  style="position:relative; height: 700px; width: 1152px; top: 12px; left: -53px; margin-left: 40px; margin-right: 43px;">

        <asp:Label ID="Label5" runat="server" Text="Obra:" Font-Size="X-Small"
            
            style="position:absolute; top: 11px; left: 12px; width: 29px;">
        </asp:Label>
        <asp:DropDownList ID="cmbObra" runat="server" Font-Size="X-Small"
            style="position:absolute; top: 9px; left: 43px; width: 647px;"
            AutoPostBack="True" onselectedindexchanged="cmbObra_SelectedIndexChanged" >
        </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                style="position:absolute; top: 448px; left: 752px; width: 136px; height: 26px;" 
                ErrorMessage="Data inicial em branco!" 
            ControlToValidate="txtDataInicial"></asp:RequiredFieldValidator>
  
          <asp:CheckBox ID="chkGrupoObra" runat="server" Font-Size="X-Small" AutoPostBack="True"
            style="position:absolute; top: 7px; left: 694px; width: 93px; height: 18px;"  
            Text="Agrupar Obras" oncheckedchanged="chkGrupoObra_CheckedChanged" />

        <asp:Label ID="Label2" runat="server" Text="Data Final" Font-Size="X-Small"
            style="position:absolute; top: 33px; left: 637px; width: 62px; height: 15px; bottom: 652px;"></asp:Label>
        <asp:TextBox ID="txtDataFinal" runat="server" Font-Size="X-Small"
            style="position:absolute; top: 48px; left: 635px; width: 74px; right: 263px;"></asp:TextBox> 
            <asp:MaskedEditExtender ID="txtDataFinal_MaskedEditExtender" runat="server" 
                Enabled="True" 
                TargetControlID="txtDataFinal" CultureName="pt-BR" 
                Mask="99/99/9999" MaskType="Date" UserDateFormat="DayMonthYear" 
                UserTimeFormat="TwentyFourHour"></asp:MaskedEditExtender>
                <asp:CalendarExtender ID="txtDataFinal_CalendarExtender" runat="server" 
                    Enabled="True" TargetControlID="txtDataFinal" Format="dd/MM/yyyy"></asp:CalendarExtender>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                    style="position:absolute; top: 470px; left: 756px; width: 134px; height: 26px;" 
                    ErrorMessage="Data final em branco!" 
                    ControlToValidate="txtDataFinal"></asp:RequiredFieldValidator>
        <asp:GridView ID="grdOS" runat="server" 
            style="position:absolute; top: 112px; left: 7px; width: 1126px;" AllowPaging="True" 
            AllowSorting="True" AutoGenerateColumns="False" 
            onpageindexchanging="grdOS_PageIndexChanging" 
            onrowcommand="grdOS_RowCommand" 
            onrowdatabound="grdOS_RowDataBound" ViewStateMode="Enabled" 
            BorderColor="#003366" BorderStyle="Inset" BorderWidth="1px"
            CellPadding="3" BackColor="White" 
            PageSize="14" Font-Size="X-Small">
            <Columns>
                <asp:TemplateField HeaderText="Aprovar/Reprovar">
                    <HeaderTemplate>
                        <asp:DropDownList ID="cmbHeaderAprovar" runat="server" AutoPostBack="true" onselectedindexchanged="cmbHeaderAprovar_SelectedIndexChanged" Font-Size="X-Small" >
                            <asp:ListItem Value="T">--- Selecione ---</asp:ListItem>
                            <asp:ListItem Value="A">Aprovar</asp:ListItem>
                            <asp:ListItem Value="R">Reprovar</asp:ListItem>
                        </asp:DropDownList>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:DropDownList ID="cmbAprovar" runat="server" Font-Size="X-Small">
                            <asp:ListItem Value="T">--- Selecione ---</asp:ListItem>
                            <asp:ListItem Value="A">Aprovar</asp:ListItem>
                            <asp:ListItem Value="R">Reprovar</asp:ListItem>
                        </asp:DropDownList>
                    </ItemTemplate>
                    <ItemStyle Font-Size="Small" />
                </asp:TemplateField>
                <asp:BoundField DataField="cmpStOSDescricao" HeaderText="Estado">
                    <ItemStyle Width="100px" HorizontalAlign="Center" Font-Size="X-Small" />
                </asp:BoundField>

                <asp:TemplateField HeaderText="Ordem Serviço">
                <ItemStyle Width="80px" HorizontalAlign="Left" Font-Size="X-Small" />
                    <ItemTemplate>
                        <asp:LinkButton ID="lnk" runat="server" 
                        CommandArgument='<%# Eval("cmpIdOS") %>' CommandName="lnk"><%#Eval("cmpNuOS") %></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:BoundField DataField="cmpNoObra" HeaderText="Nome da Obra">
                    <ItemStyle Width="250px" HorizontalAlign="Left" Font-Size="X-Small" />
                </asp:BoundField>

                <asp:BoundField DataField="cmpDtAbertura" HeaderText="Data Abertura" 
                    DataFormatString="{0:dd/MM/yyyy hh:mm}">
                    <ItemStyle Width="100px" HorizontalAlign="Center" Font-Size="X-Small"/>
                </asp:BoundField>

                <asp:BoundField DataField="cmpNoSolicitante" HeaderText="Solicitante">
                    <ItemStyle Width="80px" HorizontalAlign="Left" Font-Size="X-Small"/>
                </asp:BoundField>

                <asp:BoundField DataField="cmpDcLocal" HeaderText="Local">
                    <ItemStyle Width="200px" HorizontalAlign="Left" Font-Size="X-Small"/>
                </asp:BoundField>

                <asp:BoundField DataField="cmpDcPavimento" HeaderText="Pavimento">
                    <ItemStyle Width="100px" HorizontalAlign="Left" Font-Size="X-Small"/>
                </asp:BoundField>

<%--                <asp:BoundField DataField="cmpDcLocal" HeaderText="Local">
                    <ItemStyle Width="150px" HorizontalAlign="Left" Font-Size="Small" />
                </asp:BoundField>

                <asp:BoundField DataField="cmpNoSetor" HeaderText="Setor">
                <ItemStyle Width="150px" Font-Size="Small"></ItemStyle>
                </asp:BoundField>
--%>
                <asp:BoundField DataField="cmpDcTipoAtividade" HeaderText="Atividade" >
                <ItemStyle Width="100px" Font-Size="X-Small"/>
                </asp:BoundField>

                <asp:BoundField DataField="cmpDcOrigemOS" HeaderText="Tipo O.S" >
                <ItemStyle Width="100px" Font-Size="X-Small"/>
                </asp:BoundField>

                
                <asp:TemplateField>
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:ImageButton ID="Image1" runat="server" Height="16px" 
                        CommandArgument= '<%#DataBinder.Eval(Container.DataItem, "cmpCoObraGrupoLista") + "$" +
                                             DataBinder.Eval(Container.DataItem, "cmpIdOs") %>' 
                        CommandName="btn"
                        ImageUrl="~/App_Themes/General/page_white_acrobat.gif" Width="16px" />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Size="X-Small"/>
                </asp:TemplateField>

                <asp:TemplateField ShowHeader="False">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" 
                            CommandArgument='<%#DataBinder.Eval(Container.DataItem, "cmpIdOs") %>'  
                            CommandName="Log" Text="LOG"></asp:LinkButton>
                    </ItemTemplate>
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
            <SortedAscendingCellStyle BackColor="#F1F1F1" />
            <SortedAscendingHeaderStyle BackColor="#007DBB" />
            <SortedDescendingCellStyle BackColor="#CAC9C9" />
            <SortedDescendingHeaderStyle BackColor="#00547E" />
        </asp:GridView>

        <asp:CompareValidator ID="CompareValidator1" runat="server" 
            style="position:absolute; top: 469px; left: 437px; width: 131px; height: 26px;"
            ErrorMessage="Data final menor que data inicial!" 
            ControlToCompare="txtDataInicial" ControlToValidate="txtDataFinal" 
            Display="None" Operator="GreaterThanEqual" 
            Type="Date">
        </asp:CompareValidator>
        
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
            style="position:absolute; top: 122px; left: 761px; width: 155px; height: 20px;"
            ShowMessageBox="True" ShowSummary="False" />
    
        <asp:Button ID="btnSave" runat="server" Text="Pesquisar" Font-Size="Small"
                style="position:absolute; top: 42px; left: 877px; width: 82px; height: 26px; right: 13px;" 
                onclick="btnPesquisar_Click" />

       <asp:Button ID="btnAprove" runat="server" Text="Executar" Font-Size="Small"
            style="position:absolute; top: 74px; left: 876px; width: 82px; height: 26px;" 
            onclick="btnAprove_Click" Visible="False" />

        <asp:RadioButtonList ID="rdbData" runat="server" Font-Size="X-Small"
            style="position:absolute; top: 34px; left: 423px; width: 108px; height: 67px;" AutoPostBack="True" 
            RepeatColumns="1" 
            onselectedindexchanged="rdbData_SelectedIndexChanged" 
            RepeatDirection="Horizontal" BorderStyle="Solid">
            <asp:ListItem Selected="True" Value="CL">Data Conclusão</asp:ListItem>
            <asp:ListItem Value="AB">Data Abertura</asp:ListItem>
        </asp:RadioButtonList>

        <asp:Label ID="lblRelatorios" runat="server" Text="Relatórios:" Font-Size="X-Small"
            style="position:absolute; top: 79px; left: 635px; width: 45px; right: 292px;"></asp:Label>

        <asp:TextBox ID="txtDataInicial" runat="server" Font-Size="X-Small"
            style="position:absolute; top: 48px; left: 548px; width: 74px; "></asp:TextBox> 
            <asp:MaskedEditExtender ID="txtDataInicial_MaskedEditExtender" runat="server" 
                Enabled="True" 
                TargetControlID="txtDataInicial" CultureName="pt-BR" 
                Mask="99/99/9999" MaskType="Date" UserDateFormat="DayMonthYear" 
                UserTimeFormat="TwentyFourHour"></asp:MaskedEditExtender>
            <asp:CalendarExtender ID="txtDataInicial_CalendarExtender" runat="server" 
                Enabled="True" TargetControlID="txtDataInicial" Format="dd/MM/yyyy"></asp:CalendarExtender>
        <asp:DropDownList ID="cmbOrigemOS" runat="server" Font-Size="X-Small"
            style="position:absolute; top: 9px; left: 155px; left: 819px; width: 139px; "
            AutoPostBack="True" 
            onselectedindexchanged="cmbOrigemOS_SelectedIndexChanged" >
        </asp:DropDownList>

        <asp:DropDownList ID="cmbRelatorios" runat="server" Font-Size="X-Small"
            style="position:absolute; top: 75px; left: 155px; left: 687px; width: 172px;"
            AutoPostBack="True" 
            onselectedindexchanged="cmbRelatorios_SelectedIndexChanged">
            <asp:ListItem Value="0">--- Selecione o Relatório ---</asp:ListItem>
            <asp:ListItem Value="1">Ordens de Serviços Selecionadas</asp:ListItem>
            <asp:ListItem Value="2">Atendimentos Realizados</asp:ListItem>
            <asp:ListItem Value="3">Fechamento do Mês Com Material</asp:ListItem>
            <asp:ListItem Value="4">Fechamento do Mês Sem Material</asp:ListItem>
            <asp:ListItem Value="5">Fechamento do Mes Sem Material (Completo)</asp:ListItem>
        </asp:DropDownList>

        <asp:RadioButtonList ID="rdbState" runat="server" Font-Size="X-Small"
            style="position:absolute; top: 34px; left: 10px; width: 406px; height: 67px; right: 556px;" AutoPostBack="True" 
            onselectedindexchanged="rdbState_SelectedIndexChanged" RepeatColumns="5" 
            RepeatDirection="Horizontal" BorderStyle="Solid">
            <asp:ListItem Value="N">Em Aberto</asp:ListItem>
            <asp:ListItem Value="B">Reaberto</asp:ListItem>
            <asp:ListItem Value="S">Concluídas</asp:ListItem>
            <asp:ListItem Value="G">Em Aprovação</asp:ListItem>
            <asp:ListItem Value="P">Aprovada</asp:ListItem>
            <asp:ListItem Value="R">Reprovada</asp:ListItem>
            <asp:ListItem Value="E">Em Execução</asp:ListItem>
            <asp:ListItem Value="C">Cancelada</asp:ListItem>

            <asp:ListItem Selected="True" Value="T">Todas</asp:ListItem>
        </asp:RadioButtonList>

        <asp:Label ID="Label4" runat="server" Text="Tipo:" Font-Size="X-Small"
            
            
            style="position:absolute; top: 12px; left: 795px; width: 23px; "></asp:Label>

        <asp:Label ID="Label3" runat="server" Text="Data Inicial" Font-Size="X-Small"
            style="position:absolute; top: 33px; left: 549px; width: 72px;"></asp:Label>

        <asp:Label ID="Label6" runat="server" Text="Nº Demanda Cliente" Font-Size="X-Small"
            
            
            style="position:absolute; top: 33px; left: 722px; width: 98px;  bottom: 649px;"></asp:Label>

        <asp:TextBox ID="txtNumeroDemanda" runat="server" Font-Size="X-Small"
            
            
            
            
            style="position:absolute; top: 48px; left: 720px; width: 140px; bottom: 633px;"></asp:TextBox>        

        <asp:CheckBox ID="chkServicosTI" runat="server" Font-Size="X-Small" Text="Serviços de TI"
            AutoPostBack="True"
            style="position:absolute; top: 76px; left: 542px; width: 87px; bottom: 606px; right: 343px;" 
            oncheckedchanged="chkServicosTI_CheckedChanged"/>

        <asp:Button ID="btnShowPopup" runat="server" style="display:none" />
            <asp:ModalPopupExtender 
                ID="ModalPopupExtender1" 
                runat="server" 
                TargetControlID="btnShowPopup" 
                PopupControlID="pnlJustificativa"
                BackgroundCssClass="modalBackground"></asp:ModalPopupExtender>

       <asp:Panel ID="pnlJustificativa" runat="server" BackColor="#FFFFCC"  Height="243px" Width="592px"
                        BorderStyle="Solid" BorderWidth="1px" style="display:none">
            <asp:Label ID="lblJustificativa" runat="server" Text="Justificativa"
                        style="position:absolute; top: 6px; left: 9px; height: 20px; width: 580px; font-size:medium; text-align: center;" 
                        BackColor="#FFFFCC" Font-Bold="True" Font-Size="Large" 
                        ForeColor="#3333FF" Font-Names="Calibri"></asp:Label>
            <asp:TextBox ID="TxtJustificativa" runat="server" ForeColor="#3333FF" 
                style="position:absolute; top: 35px; left: 13px; width: 569px; height: 160px;" 
                TextMode="MultiLine"></asp:TextBox>
            <asp:Button ID="btnFecharJutifica" runat="server" CausesValidation="False" Text="Fechar" 
                style="position:absolute; top: 212px; left: 14px; height: 30px; width: 80px;"/>
        </asp:Panel>

                    <asp:ModalPopupExtender 
                ID="ModalPopupExtender2" 
                runat="server" 
                TargetControlID="btnShowPopup" 
                PopupControlID="pnlLog"
                BackgroundCssClass="modalBackground"></asp:ModalPopupExtender>
<asp:Panel ID="pnlLog" runat="server" BackColor="#FFFFCC"  Height="190px" Width="328px"
                        BorderStyle="Solid" BorderWidth="1px" style="display:none">
            
            <asp:GridView ID="grdLogs" runat="server" AutoGenerateColumns="False" 
                Height="44px" Width="328px" CellPadding="4" ForeColor="#333333" 
                GridLines="None">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:BoundField HeaderText="Situação" DataField="cmpStOSDesc" >
                    <ItemStyle Width="300px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Data / Hora" DataField="cmpDtDataLog" >
                    <ItemStyle Width="200px" />
                    </asp:BoundField>
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
            <asp:Button ID="btnFecharLog" runat="server" CausesValidation="False" Text="Fechar" 
                
                style="position:absolute; top: 156px; left: 129px; height: 30px; width: 80px;" 
                onclick="btnFecharLog_Click"/> 
        </asp:Panel>

</div>
</asp:Content>

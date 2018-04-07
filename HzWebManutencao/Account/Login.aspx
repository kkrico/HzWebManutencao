<%@ Page Title="Log In" Language="C#" MasterPageFile="Login.master" AutoEventWireup="true"
    CodeBehind="Login.aspx.cs" Inherits="HzWebNumerador.Account.Login" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="child" style="position: relative; left: 20%">
        <asp:Panel ID="pnlLogin" runat="server" 
            style="position:absolute; top: 2px; left: 130px; width: 489px; height: 200px;" BorderStyle="Solid">
            <!-- Data da validação -->
            <asp:RequiredFieldValidator ID="rfdSenha" runat="server" 
                ErrorMessage="Senha em branco!" ControlToValidate="txtPassword" 
                Display="None"
                style="position:absolute; top: -5px; left: 534px; width: 80px;" 
                ViewStateMode="Enabled"></asp:RequiredFieldValidator>
            <asp:RequiredFieldValidator ID="rfdEmail" runat="server" 
                ErrorMessage="E-mail em branco!" ControlToValidate="txtEmail" 
                Display="None"
                style="position:absolute; top: -5px; left: 603px; width: 80px;"></asp:RequiredFieldValidator>
            <asp:RequiredFieldValidator ID="rfdEmpresa" runat="server" 
                ErrorMessage="Empresa em branco!" ControlToValidate="cmbEmpresa" 
                Display="None" Enabled="false"
                style="position:absolute; top: -6px; left: 671px; width: 80px;"></asp:RequiredFieldValidator>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
                style="position:absolute; top: 25px; left: 536px; width: 313px;" 
                ShowMessageBox="True" ShowSummary="False"/>

            <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="updItemAtividade">
                <ProgressTemplate>
                    <img src="../App_Themes/General/ajax-loader.gif" style="position:absolute; top: 71px; left: 227px; z-index:1000" alt="" />
                </ProgressTemplate>
            </asp:UpdateProgress>
            <asp:UpdatePanel ID="updItemAtividade" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <!-- Dados do Login -->
                    <asp:Label ID="Label1" runat="server" Text="Senha:" 
                        style="position:absolute; top: 44px; left: 12px;"></asp:Label>

                    <asp:TextBox ID="txtEmail" runat="server"
                        style="position:absolute; top: 11px; left: 100px; width: 231px;"></asp:TextBox>
                    <asp:Label ID="Label2" runat="server" 
                        style="position:absolute; top: 11px; left: 12px;" Text="E-mail:"></asp:Label>
                    <asp:TextBox ID="txtPassword" runat="server" 
                        style="position:absolute; top: 44px; left: 100px; width: 231px;" 
                        TextMode="Password"></asp:TextBox>
                    <asp:Button ID="btnSearch" runat="server" Text="Pesquisar"
                        style="position:absolute; top: 9px; left: 370px; width: 80px;" 
                        onclick="btnSearch_Click"></asp:Button>
                    <asp:Button ID="btnConect" runat="server" Text="Conectar"
                        style="position:absolute; top: 91px; left: 370px;" 
                        CausesValidation="False" onclick="btnConect_Click"/>

                    <!-- Dados do Contratante/Empresa -->
                    <asp:Label ID="lblContratante" runat="server" 
                        style="position:absolute; top: 92px; left: 12px;" Text="Contratante:"></asp:Label>
                    <asp:Label ID="lblEmpresa" runat="server" 
                        style="position:absolute; top: 125px; left: 12px;" Text="Empresa:"></asp:Label>
                    <asp:Label ID="lblLocal" runat="server" 
                        style="position:absolute; top: 158px; left: 12px;" Text="Local:"></asp:Label>
                    <asp:DropDownList ID="cmbContratante" runat="server" AutoPostBack="True" 
                        style="position:absolute; top: 92px; left: 100px; width: 231px" 
                        onselectedindexchanged="cmbContratante_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:DropDownList ID="cmbEmpresa" runat="server"
                    style="position:absolute; top: 125px; left: 100px; width: 231px" 
                        AutoPostBack="True" onselectedindexchanged="cmbEmpresa_SelectedIndexChanged"></asp:DropDownList>
                    <asp:DropDownList ID="cmbLocal" runat="server"
                    style="position:absolute; top: 158px; left: 100px; width: 231px"
                        AutoPostBack="True" OnSelectedIndexChanged="cmbLocal_SelectedIndexChanged"></asp:DropDownList>

                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="btnConect" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="cmbContratante" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="cmbEmpresa" EventName="SelectedIndexChanged" />
                </Triggers>
            </asp:UpdatePanel>
        </asp:Panel>

        <asp:Panel ID="pnlManutencao" runat="server"
            style="position:absolute; top: 93px; left: 10px; width: 719px; height: 138px; display:none" 
            BorderStyle="Solid">
            <asp:Label ID="Label3" runat="server" Text="Sistema em Manutenção. Previsão de retorno as 14:00 hs." 
                style="position:absolute; top: 49px; left: 9px; height: 60px; width: 650px;" 
                Font-Size="X-Large"></asp:Label>            
        </asp:Panel>

    </div>
</asp:Content>

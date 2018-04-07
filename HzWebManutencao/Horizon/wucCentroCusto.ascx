<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="wucCentroCusto.ascx.cs" Inherits="HzWebManutencao.Horizon.wucCentroCusto" %>
    <div style="height: 562px;">
    
        <br />
        <br />
        <div style="margin-top: 0px; position: absolute; top: 20px; left: 14px; height: 547px; width: 1070px;">
            <asp:Label ID="Label1" runat="server" Text="Centro de Custo"></asp:Label>
            <br />
            <asp:TextBox ID="txtDescricaoCentroCusto" runat="server" Height="42px" 
                Width="253px" TextMode="MultiLine"></asp:TextBox>
            <br />
            <br />
            <asp:Label ID="lblDescricaoEquip" runat="server" BackColor="White" Font-Bold="True" 
                        Font-Names="Calibri" Font-Size="Small" 
                        style="position:absolute; top: 1px; left: 286px; height: 20px; width: 154px;" 
                        Text="Número:"></asp:Label>
            <asp:TextBox ID="txtDescEquip" runat="server" Font-Names="Calibri" 
                        Font-Size="Small" 
                        
                    
                
                style="position:absolute; top: 19px; left: 286px; height: 21px; width: 108px; right: 668px;"></asp:TextBox>
            <div style="position: absolute; top: 78px; left: 2px; width: 296px; height: 244px;">
                Obras<br />
                <asp:ListBox ID="lstObras" runat="server" Width="288px" Height="218px"></asp:ListBox>
                <br />
            </div>
            <div style="position: absolute; top: 78px; left: 356px; width: 296px; height: 244px;">
                Obras<br />
                <asp:ListBox ID="lstObrasCentroCusto" runat="server" Width="288px" 
                    Height="218px"></asp:ListBox>
                <br />
            </div>
            <div style="position: absolute; top: 78px; left: 309px; width: 35px; height: 244px;">
                <br />
                <asp:Button ID="btnAdciona" runat="server" Text="&gt;&gt;" 
                    onclick="btnAdciona_Click" />
                <br />
                <br />
                <asp:Button ID="btnExclui" runat="server" Text="&lt;&lt;" 
                    onclick="btnExclui_Click" />
            </div>
            <div style="position: absolute; top: 343px; left: 296px; width: 78px;">
            <asp:Button ID="btnSalvar" runat="server" Text="Salvar" Width="72px" 
                onclick="btnSalvar_Click" />
            </div>
        </div>
    
    </div>
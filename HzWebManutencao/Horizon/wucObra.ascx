<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="wucObra.ascx.cs" Inherits="HzWebManutencao.Horizon.wucObra" %>


        <div style="margin-top: 0px; position: absolute; top: 20px; left: 14px; height: 547px; width: 1070px;">
            <asp:Label ID="Label1" runat="server" Text="OBRA"></asp:Label>
            <br />
            <asp:TextBox ID="txtDescricaoObra" runat="server" Height="42px" 
                Width="294px" TextMode="MultiLine"></asp:TextBox>
            <br />
            <br />
            <div style="position: absolute; top: 78px; left: 2px; width: 296px; height: 244px;">
                Pavimentos<br />
                <asp:ListBox ID="lstPavimentos" runat="server" Width="288px" Height="218px"></asp:ListBox>
                <br />
            </div>
            <div style="position: absolute; top: 78px; left: 356px; width: 296px; height: 244px;">
                Pavimentos Obra<br />
                <asp:ListBox ID="lstPavimentosObra" runat="server" Width="288px" 
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

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm4.aspx.cs" Inherits="HzWebManutencao.WebForm4" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
              <asp:ModalPopupExtender 
            ID="modSelecObra" 
            runat="server" 
            TargetControlID="btnfecharSelectObra" 
            PopupControlID="pnpSelectObra"
            CancelControlID="btnfecharSelectObra"
            BackgroundCssClass="modalBackground"
             DropShadow="true">
            </asp:ModalPopupExtender>

             <asp:Panel ID="pnpSelectObra" runat="server" BackColor="White" BorderStyle="Solid" 
                    BorderWidth="1px" 
               
            style="position:absolute; top: 204px; left: 856px; height: 184px; width: 303px;display:yes" >

            <asp:Button ID="btnfecharSelectObra" runat="server" Text="Fechar" Width="63px" 
                     
                     style="position:absolute; left: 79px; top: -1px; right: 149px; width: 75px; height: 21px;" 
                     onclick="Button6_Click"/>
                     
                 
              </asp:Panel>

                      <asp:ModalPopupExtender 
            ID="ModalPopupExtender1" 
            runat="server" 
            TargetControlID="Button2" 
            PopupControlID="Panel1"
            CancelControlID="Button2"
            BackgroundCssClass="modalBackground"
             DropShadow="true">
            </asp:ModalPopupExtender>

             <asp:Panel ID="Panel1" runat="server" BackColor="White" BorderStyle="Solid" 
                    BorderWidth="1px" 
               
            
                  style="position:absolute; top: 248px; left: 436px; height: 184px; width: 303px;display:yes" >

            <asp:Button ID="Button2" runat="server" Text="FecharPainel2" Width="63px" 
                     
                     style="position:absolute; left: 55px; top: -1px; right: 173px; width: 75px; height: 21px;" 
                     onclick="Button6_Click"/>
                     
                 
              </asp:Panel>
    </div>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="Button" />
    <asp:Button ID="Button3" runat="server" onclick="Button3_Click" Text="Button" />
    </form>
</body>
</html>

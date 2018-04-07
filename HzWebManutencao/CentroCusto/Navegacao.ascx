<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Navegacao.ascx.cs" Inherits="HzWebManutencao.CentroCusto.Navegacao" %>
         <div style="width: 1066px; position:absolute; top: 15px; left: 0px; height: 33px;" >
          <asp:HyperLink ID="hypCentroCusto" runat="server" style="position:absolute; top: 5px; left: 12px;"
               NavigateUrl="~/CentroCusto/webCentroCusto.aspx">Centro de Custo >></asp:HyperLink>

           <asp:HyperLink ID="hypObras" runat="server" style="position:absolute; top: 5px; left: 137px; height: 15px; right: 860px;"
               NavigateUrl="~/CentroCusto/webObra.aspx">Obra >></asp:HyperLink>

           <asp:HyperLink ID="hypSistemasPrediais" runat="server" 
           style="position:absolute; top: 5px; left: 196px; " 
                NavigateUrl="~/CentroCusto/webSistemasPrediais.aspx" >Sistemas Prediais >></asp:HyperLink>

            <asp:HyperLink ID="hypPavimentos" runat="server" style="position:absolute; top: 6px; left: 330px; height: 18px; right: 640px;" 
            NavigateUrl="~/CentroCusto/webPavimentos.aspx">Pavimentos >></asp:HyperLink>

           <asp:HyperLink ID="hypAtivos" runat="server" 
                   style="position:absolute; top: 5px; left: 424px; right: 581px;" 
                NavigateUrl="~/CentroCusto/webAtivoObra.aspx">Ativos >></asp:HyperLink>

             <asp:Label ID="lblDescricao" runat="server" 
                 style="position:absolute; top: 26px; left: 18px;"></asp:Label>
                </div>
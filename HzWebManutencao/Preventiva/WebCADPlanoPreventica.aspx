<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="WebCADPlanoPreventica.aspx.cs" Inherits="HzWebManutencao.Preventiva.WebCADPlanoPreventica" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server" >
   <div style="height: 947px; width: 1150px; margin-left: -200px;">

      <!------ Título------->
      <asp:Label ID="lblAcao" runat="server" Text="Plano de Manutenção Preventiva"
         style="position:absolute; top: 183px; left: 700px; height: 21px; width: 460px; text-align: center;" 
         BackColor="White" Font-Bold="True" Font-Size="Large" ForeColor="Maroon" 
         Font-Names="Calibri"></asp:Label>

      <!------ Tipo Atividade------->
      <asp:Label ID="lblTipoAtividade" runat="server" Text="Tipo Atividade" 
         style="position:absolute; top: 290px; left: 367px;"></asp:Label>
      <asp:DropDownList ID="ddlTipoAtividade" runat="server" 
         style="position:absolute; top: 312px; left: 368px; width: 467px; right: 1006px; bottom: 469px;">
      </asp:DropDownList>

      <!------ Grupo Atividade------->
      <asp:Label ID="lblGrupoAtividade" runat="server" Text="GrupoAtividade" 
         style="position:absolute; top: 293px; left: 852px; height: 12px;"></asp:Label>
      <asp:DropDownList ID="ddlGrupoAtividade" runat="server" 
         style="position:absolute; top: 311px; left: 850px; width: 433px; right: 558px; bottom: 464px;">
      </asp:DropDownList>
      <asp:Button ID="btnAddAtividade" runat="server" Text=">>" 
         style="position:absolute; top: 421px; left: 903px; width: 54px; height: 26px;"  />
      <asp:Button ID="btnRemoverAtividade" runat="server" Text="<<" 
         style="position:absolute; top: 472px; left: 904px; width: 54px; height: 26px;"  />

      <!------ Periodicidade------->
      <asp:Label ID="lblPeriodicidade" runat="server" Text="Periodicidade" 
         style="position:absolute; top: 291px; left: 1301px; height: 13px;"></asp:Label>
      <asp:DropDownList ID="ddlPeriodicidade" runat="server" 
         style="position:absolute; top: 311px; left: 1299px; width: 187px; right: 363px; bottom: 471px; margin-top: 0px;">
      </asp:DropDownList>

      <!------ Obra------->
      <asp:Label ID="lblObra" runat="server" Text="Obra" 
         style="position:absolute; top: 236px; left: 366px; height: 13px;"></asp:Label>
      <asp:DropDownList ID="ddlObra" runat="server" 
         style="position:absolute; top: 258px; left: 366px; height: 16px; width: 1116px;">
      </asp:DropDownList>

      <!------ Buttons------->
      <asp:Button ID="btnNovo" runat="server" Text="Novo" 
         style="position:absolute; top: 183px; left: 360px; height: 29px; width: 66px;" />
      <asp:Button ID="btnRelatorios" runat="server" Text="Relatório" 
         style="position:absolute; top: 183px; left: 433px; height: 29px; width: 67px;" />
      <asp:Button ID="btnAtivos" runat="server" Text="Ativos"  
         style="position:absolute; top: 183px; left: 507px; height: 29px; width: 66px;"/>
      <asp:Button ID="btnGravar" runat="server" Text="Gravar"  
         style="position:absolute; top: 553px; left: 969px; height: 29px; width: 66px;"/>
       <asp:Button ID="btnPesquisar" runat="server" Text="Pesquisar" 
           
           style="position:absolute; top: 184px; left: 581px; width: 66px; height: 29px;" />

      <!------ GridView------->
      <asp:GridView ID="dgvDescricaoAtividade" runat="server"  
         style="position:absolute; top: 378px; left: 366px; width: 525px; height: 167px;" 
         AutoGenerateColumns="False">
         <Columns>
            <asp:CommandField ShowEditButton="True" />
            <asp:BoundField DataField="cmpDcEquipamentoObra" 
               HeaderText="Itens de Atividade de Referência" />
         </Columns>
      </asp:GridView>
      <asp:GridView ID="dgvDescricaoItemObra" runat="server" 
         style="position:absolute; top: 379px; left: 968px; width: 525px; height: 167px;" 
         AutoGenerateColumns="False">
         <Columns>
            <asp:CommandField ShowSelectButton="True" />
            <asp:BoundField DataField="cmpDcEquipamentoObra" 
               HeaderText="Itens de Atividade da Obra" />
         </Columns>
      </asp:GridView>

      <!------ModalPopupExtender------->
      <asp:ModalPopupExtender 
         ID="ModalPopupExtender1" 
         runat="server" 
         TargetControlID="btnShowPopup" 
         PopupControlID="pnlPopUp"
         BackgroundCssClass="modalBackground"></asp:ModalPopupExtender>

         <!------Panel------->
         <asp:Panel ID="pnlPopUp" runat="server" 
           style="position:absolute; top: 599px; left: 278px; height: 321px; width: 345px;" 
           BackColor="White" BorderStyle="Solid" BorderWidth="1px" 
           Width="250px">
             <asp:Label ID="lblRelatorios" runat="server" Text="Relatório" 
                  style="position:absolute; top: 63px; left: 59px; height: 21px; width: 214px; text-align: center;" 
         BackColor="White" Font-Bold="True" Font-Size="Large" ForeColor="Maroon" 
         Font-Names="Calibri"></asp:Label>

           

             <asp:GridView ID="dgvRelatorios" runat="server"  
                 style="position:absolute; top: 100px; left: 22px; height: 156px; width: 304px;" 
                 AutoGenerateColumns="False">
                 <Columns>
                     <asp:CommandField ShowSelectButton="True" />
                     <asp:BoundField DataField="cmpDcEquipamentoObra" />
                 </Columns>
             </asp:GridView>
               </asp:Panel>


              <!------ModalPopupExtender2------->

                  <asp:ModalPopupExtender 
                 ID="ModalPopupExtender2" 
                 runat="server" 
                 TargetControlID="btnShowPopup" 
                 PopupControlID="pnlPopUp2"
                 BackgroundCssClass="modalBackground"></asp:ModalPopupExtender>

     
           <!------Panel2------->
              
             <asp:Panel ID="pnlPopUp2" runat="server" 
                 style="position:absolute; top: 602px; left: 645px; height: 320px; width: 342px;" 
                 BackColor="White" BorderStyle="Solid" BorderWidth="1px">

           <asp:Label ID="lblAtivos" runat="server" Text="Ativos" 
                  style="position:absolute; top: 63px; left: 59px; height: 21px; width: 214px; text-align: center;" 
         BackColor="White" Font-Bold="True" Font-Size="Large" ForeColor="Maroon" 
         Font-Names="Calibri"></asp:Label>

                 <asp:GridView ID="dgvAtivos" runat="server" 
                     
                     style="position:absolute; top: 98px; left: 20px; height: 156px; width: 304px;" 
                     AutoGenerateColumns="False">
                     <Columns>
                         <asp:CommandField ShowSelectButton="True" />
                         <asp:BoundField DataField="cmpDcEquipamentoObra" />
                     </Columns>
                 </asp:GridView>

             </asp:Panel>

              <!------ModalPopupExtender3------->

                <asp:ModalPopupExtender 
                 ID="ModalPopupExtender3" 
                 runat="server" 
                 TargetControlID="btnShowPopup" 
                 PopupControlID="pnlPopUp3"
                 BackgroundCssClass="modalBackground"></asp:ModalPopupExtender>

                 <!------Panel3------->

               <asp:Panel ID="pnlPopUp3" runat="server" 
           style="position:absolute; top: 607px; left: 1035px; height: 320px; width: 342px;" 
                 BackColor="White" BorderStyle="Solid" BorderWidth="1px">
               </asp:Panel>

                  <asp:Label ID="Label1" runat="server" Text="Pesquisar" 
                  style="position:absolute; top: 669px; left: 1113px; height: 21px; width: 214px; text-align: center;" 
         BackColor="White" Font-Bold="True" Font-Size="Large" ForeColor="Maroon" 
         Font-Names="Calibri"></asp:Label>


       <asp:GridView ID="dgvPesquisa" runat="server" 
           style="position:absolute; top: 707px; left: 1057px; height: 156px; width: 304px;" 
           AutoGenerateColumns="False">
           <Columns>
               <asp:CommandField ShowSelectButton="True" />
               <asp:BoundField DataField="cmpDcEquipamentoObra" />
           </Columns>
       </asp:GridView>




   </div>
</asp:Content>


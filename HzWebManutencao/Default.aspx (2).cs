using System;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class _Default : System.Web.UI.Page 
{
    // Utilizarei datatable como fonte de dados para popular os grid
    DataTable dtDados       = new DataTable("Despesas");

    protected void Page_Load(object sender, EventArgs e)
    {
        MontaTabela();
        CarregaGridView(ref gvPrincipal);
    }
    

    #region Criar objetos dinamicos dentro de celulas especificas de um gridview

        private void MontaTabela()
        {
            // Utilizo esta rotina para criar estrutura da fonte de dados, bem como alimenta-la.
            // Vale destacar que utilizo esta fonte de dados, apenas por comodidade poderiamos ter como fonte
            // um banco sql server, oracle, xml, arq. txt, enfim qualquer fonte que o ADO.NET consiga ler.

            try
            {
                dtDados.Columns.Add("IDReg"     , Type.GetType("System.Int32"));
                dtDados.Columns.Add("Descricao" , Type.GetType("System.String"));
                dtDados.Columns.Add("ValorFull" , Type.GetType("System.Double"));

                // Aproveitando que vou utilizar como fonte um datatable, exemplifico como criar uma coluna
                // de chave primaria e do tipo auto-numeração

                // Configurando coluna IDReg como chave primaria
                DataColumn[] pk = new DataColumn[1];
                pk[0] = dtDados.Columns["IDReg"];
                dtDados.PrimaryKey = pk;
                dtDados.Columns["IDReg"].AutoIncrement = true;
                dtDados.Columns["IDReg"].AutoIncrementSeed = 1;
                dtDados.Columns["IDReg"].ReadOnly = true;
            }
            catch (Exception e)
            {
                Response.Write("Error: " + e.ToString());
            }
            finally
            {
                PreencheTabela();
            }
        }

        private void PreencheTabela()
        {
            // Utilizo esta rotina para popular a estrura de dados.
            // a cada registro é criado uma nova linha dtRow

            try
            {
                DataRow dtRow = dtDados.NewRow();
                dtRow["Descricao"] = "Conta de Luz";
                dtRow["ValorFull"] = 1500.92;
                dtDados.Rows.Add(dtRow);
                
                dtRow = dtDados.NewRow();
                dtRow["Descricao"] = "Conta de agua";
                dtRow["ValorFull"] = 1500.92;
                dtDados.Rows.Add(dtRow);

                dtRow = dtDados.NewRow();
                dtRow["Descricao"] = "Conta de Telefone";
                dtRow["ValorFull"] = 1500.92;
                dtDados.Rows.Add(dtRow);

                dtRow = dtDados.NewRow();
                dtRow["Descricao"] = "Combustivel";
                dtRow["ValorFull"] = 1500.92;
                dtDados.Rows.Add(dtRow);
            }
            catch (Exception e)
            {
                Response.Write("Error: " + e.ToString());
            }
        }

        private void CarregaGridView(ref GridView objGrid)
        {
            // Rotina que recebe como refencia de um gridview, seja ele criado dinamico ou nao.
            // o importante é o gridview passado como parametro existir

            try
            {
                objGrid.DataSource = dtDados;
                objGrid.DataBind();
            }
            catch (Exception e)
            {
                Response.Write("Error: " + e.ToString());
            }
        }

        protected void gvPrincipal_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            // Ao ser disparado o metodo DataBind na rotina CarregaGridView(), automaticamente este metodo
            // é executado. É esteé um metodo do proprio gridview e ocorre todas as vezes que o gridview for 
            // atualizado, ou seja toda vez que a fonte de dados for lida e as informações transportadas ao gridview

            try
            {
                
                if (e.Row.RowType.ToString().Equals("Header"))
                {
                    // Quando a linha de cabeçalho do grid estiver sendo criada, posso aproveitar para
                    // modificar os titulos das colunas, lembrando que a numeração de coluna inicia-se em ZERO.

                    e.Row.Cells[0].Text = "No";
                    e.Row.Cells[1].Text = "Descrição da despesa";
                    e.Row.Cells[2].Text = "Valor R$";

                    // centralizo o titulo da coluna 2
                    e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Center;

                }
                else
                {
                    // Se a linha que esta sendo populada nao é de titulo de coluna, então ajusto a largura 
                    // das colunas e alinho as informações conforne necessário.

                    e.Row.Cells[0].Width = 50;
                    e.Row.Cells[2].Width = 150;
                    e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Right;

                    // Apartir daqui começa propriamente o exemplo, vou tentar comentar todas as linhas ok ?

                    // Obtenho uma referencia do gridview que esta sendo executado.
                    GridView    gvObj           = (GridView)sender;

                    // Crio em tempo de execução na memoria um objeto ASPX do tipo Label
                    // Seto seu ID e sua propriedade Text;
                    // Aproveito o momento para limpar o conteudo da celula UM do grid, esta é a celula
                    // Descrição da despesa isto se faz necessário apenas porque eu quero colocar um icone a esquerda
                    // do texto da celula.
                    
                    Label       lblObj          = new Label();
                                lblObj.ID       = "lblObjt_" + e.Row.RowIndex.ToString();
                                lblObj.Text     = " " + e.Row.Cells[1].Text;
                                                        e.Row.Cells[1].Text = "";
                    // Da mesma forma que a anterior eu crio agora um objeto do tipo imagemButton, e seto suas 
                    // propriedade, definindo incluive o path de localização da imagem.
                    // Neste objeto alem de setar o path da imagem, estou definindo o nome de um metodo (rotina), que
                    // será disparado toda vez que o botão receber um click.


                    ImageButton ibtObj          = new ImageButton();
                                ibtObj.ID       = "ibtObj_" + e.Row.RowIndex.ToString();
                                ibtObj.ImageUrl = "GridLupa.png";
                                ibtObj.Click +=new ImageClickEventHandler(ibtObj_Click);
                    
                    // Agora que ja temos os objetos criado dinamicamente e devidamente configurados/formatados,
                    // vamos adiciona-los a celula do gridview. A ordem destas duas linhas tem muita importancia
                    // aqui estou inserindo um ImagemButton e depois um Label, se for para inverter a ordem, nao 
                    // se faz necessário criar o objeto label e nem limpara o conteudo da celula como descrivi a cima

                                e.Row.Cells[1].Controls.Add(ibtObj);
                                e.Row.Cells[1].Controls.Add(lblObj);
                }
            }
            catch (Exception f)
            {
                Response.Write("Error: " + f.ToString());
            }
        }

        protected void ibtObj_Click(object sender, ImageClickEventArgs e)
        {
            // Esta é rotina que será disparada toda vez que o ImagemButton, receber um click.
            
            // Aqui crio em tempo de execução uma referencia do botão que RECEBEU CLICK.
            ImageButton btImagem    = (ImageButton)sender;

            // Apenas para exemplificar crio uma referencia ao GridView que contem o ImagemButton
            //GridView    gvView      = (GridView)btImagem.Parent.Parent.Parent.Parent;

            // Aqui estou fazendo uma referencia a LINHA DO GRIDVIEW que contem o botão que RECEBEU O CLICK.
            GridViewRow gvRow       = (GridViewRow)btImagem.Parent.Parent;

            // Tambem só para exemplicar apresento como obter o numero da linha que contem o botão que recebeu click.
            //Int32       gvRowIndex  = gvRow.RowIndex;

            // Aqui estamos CRIANDO em tempo de execução um gridview e seto seu tamanho para 100%, para que ele
            // ocupe TODA a area que a ele for destinada.
            GridView    objGridView = new GridView();
                        objGridView.Width = System.Web.UI.WebControls.Unit.Percentage(100);
                        
            // Aqui eu poderia utilizar uma outra fonte de dados, mas para simplificar utilizo a mesma. Então carrego
            // o GridView criado em tempo de execução com dados, lembrando que este dados pode vir de qualquer fonte.
                        CarregaGridView(ref objGridView);

            // Adiciono o gridview criado em tempo de execução a celula que contem o botão que recebeu o click.
                        gvRow.Cells[1].Controls.Add(objGridView);
            
            // Vale destaca que da mesma forma que criei uma rotina para ser disparada por um botão que foi criado
            // em tempo de execução eu posso criar outras rotinas a serem disparadas por este novo gridview criado
            // em tempo de execução. Partindo deste principio posso criar tudo o quem bem quiser sem dificuldade.

            // Espero sinceramente que o exemplo este objetivo e bem claro de se entender. Até a proxima.
        }
    #endregion
  
}

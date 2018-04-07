using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Apresentacao.Controles
{
    public partial class Mensagem : System.Web.UI.UserControl
    {
        #region Variáveis

        public delegate void EventHandler(Object obj, EventArgs e);//Delegate eventos
        public event EventHandler btnCustomizaClick;
        public string _titulo           { get; set; }//Título da caixa de mensagem
        public string _mensagemCaixa    { get; set; }//Mensagem foi definida a aparecer na caixa de mensagem
        public string _javascript = string.Empty;//Variável que recebe o javascript e por padrão é branco        
        public Tipo _tipoMensagem;

        //enum que significa enumeração e apresentam quatro elementos
        public enum Tipo {
            Info,
            Sucesso,
            Aviso,
            Erro,
        }

        #endregion

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            //Inicializa como false para não mostrar o botão na tela
            btnCustomiza.Visible = false;
        }

        protected void btnCustomiza_Click(object sender, EventArgs e)
        {
            OnbtnCustomiza_Click();
        }

        #endregion

        #region Métodos

        protected void OnbtnCustomiza_Click()
        {
            if (btnCustomizaClick != null)
            {
                btnCustomizaClick(this, new EventArgs());
            }
        }

        /// <summary>
        /// Mostra mensagem sem o botão customizar
        /// </summary>
        /// <param name="tipo"></param>
        /// <param name="mensagem"></param>
        public void Mostar(Tipo tipo, string mensagem)
        {
            _tipoMensagem = tipo;//Tipo da mensagem
            _mensagemCaixa = mensagem;//Mensagem que vai ser mostrada em tela
            AdicionaJavaScriptPagina();//Adiciona JavaScript na página 
        }

        /// <summary>
        /// Mostra mensagem e botão onde o evento pode ser customizado e
        /// acessado pela página que o chamou através do delegate
        /// </summary>
        /// <param name="tipo"></param>
        /// <param name="mensagem"></param>
        /// <param name="verbtnCustomiza"></param>
        /// <param name="nomebtnCustomiza"></param>
        public void Mostar(Tipo tipo, string mensagem, string nomebtnCustomiza)
        {   
            btnCustomiza.Visible = true;
            btnCustomiza.Text = nomebtnCustomiza;//Nome do botão que vai aparecer na tela
            _tipoMensagem = tipo;//Tipo da mensagem
            _mensagemCaixa = mensagem;//Mensagem que vai ser mostrada em tela
            AdicionaJavaScriptPagina();//Adiciona JavaScript na página 
        }

        /// <summary>
        /// Adiciona o JavaScript na página
        /// </summary>
        private void AdicionaJavaScriptPagina()
        {
            #region Template

            _javascript = "  ";
            _javascript += "  	$(function() { ";
            _javascript += "  		$( \"#dialog:ui-dialog\" ).dialog( \"destroy\" ); ";
            //A mensagem que vai ser mostrada em tela
            _javascript += "  		$( \"#lblMensagem\" ).append( \"MensagemCaixa\" ); ";
            //Adiciona o css que mostra a imagem e coloca a cor na fonte
            _javascript += "  		$(\"#divFundoMensagem\").addClass(\"NomeClasse\"); ";
            //Mostra a mensagem na tela
            _javascript += "                 $(\"#divMensagem\").dialog({ ";
            //Mostra o fundo congelado = true ou remove o fundo congelado = false
            _javascript += "                     modal: true, ";
            _javascript += "                     autoOpen: true, ";
            _javascript += "                     draggable: true, ";
            //Título da Caixa de Mensagem
            _javascript += "                     title: \"NomeTitulo\", ";
            //Adicionando um evento para anexar a div ..
            _javascript += "                     open: function(type, data) { ";
            //..no form, dessa forma o postback do botão irá funcionar
            _javascript += "                         $(this).parent().appendTo(\"form\"); "; 
            _javascript += "                     } ";
            _javascript += "                 }); ";
            _javascript += "         }); ";

            #endregion

            #region Atribui o estilo/css, título e a mensagem e depois registra o Script na página

            switch (_tipoMensagem)
            {
                case Tipo.Info:
                    _javascript = _javascript.Replace("NomeClasse", "info").Replace("NomeTitulo", "Informação").Replace("MensagemCaixa", _mensagemCaixa);
                    break;

                case Tipo.Sucesso:
                    _javascript = _javascript.Replace("NomeClasse", "successo").Replace("NomeTitulo", "Sucesso").Replace("MensagemCaixa", _mensagemCaixa); ;
                    break;

                case Tipo.Aviso:
                    _javascript = _javascript.Replace("NomeClasse", "aviso").Replace("NomeTitulo", "Aviso").Replace("MensagemCaixa", _mensagemCaixa); ;
                    break;

                case Tipo.Erro:
                    _javascript = _javascript.Replace("NomeClasse", "erro").Replace("NomeTitulo", "Erro").Replace("MensagemCaixa", _mensagemCaixa); ;
                    break;
            }

            //Registra o JavaScript na página
            Page.ClientScript.RegisterStartupScript(GetType(), "Javascript", _javascript, true);

            #endregion
        }

        #endregion
    }
}
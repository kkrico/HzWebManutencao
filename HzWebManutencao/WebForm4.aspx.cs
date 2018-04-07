using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HzWebManutencao
{
    public partial class WebForm4 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button6_Click(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            ModalPopupExtender1.Hide();
            modSelecObra.Show();
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            modSelecObra.Hide();
            ModalPopupExtender1.Show();
        }
    }
}
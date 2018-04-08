using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HzLibConnection.Sql;
using HzLibCorporativo.Funcional;
using HzLibGeneral.Util;
using HzClasses.Tabelas.Apoio;
using System.Data;
using HzLibConnection.Data;
using HzClasses.Numeracao;
using System.IO;
using System.Web.Services;

namespace HzWebManutencao.Documentos
{
    public partial class WebNovoDocumento : System.Web.UI.Page
    {
        [WebMethod]
        public object DoAlgo()
        {
            return new { Data = "Foo" };
        }
    }


}

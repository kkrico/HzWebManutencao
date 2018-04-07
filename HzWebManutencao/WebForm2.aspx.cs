using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

namespace HzWebManutencao
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            //MultiView1.ActiveViewIndex = 1;
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
  
        }
        public Bitmap GerarQRCode(int width, int height, string text)
        {
            try
            {
                var bw = new ZXing.BarcodeWriter();
                var encOptions = new ZXing.Common.EncodingOptions() { Width = width, Height = height, Margin = 0 };
                bw.Options = encOptions;
                bw.Format = ZXing.BarcodeFormat.QR_CODE;
                var resultado = new Bitmap(bw.Write(text));
                return resultado;
            }
            catch
            {
                throw;
            }
        }

        protected void btnGerarQRCode_Click(object sender, EventArgs e)
        {
            
                      Bitmap QR = GerarQRCode(200, 200, "Orion");

                      string filepath = Server.MapPath("~/QRCODE");
                      filepath += "\\Origon.Jpeg";
            QR.Save(filepath, System.Drawing.Imaging.ImageFormat.Jpeg);
        }
    }
}
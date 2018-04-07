using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;

namespace HzWebManutencao.QRCODE
{
    public class clQRCODE
    {
        public string Arquivo;
        public string TAG;
        public string BuscarQRCODE(string diretorio, string codigo)
        {
            diretorio = diretorio + "\\QRCODE\\" + codigo + ".jpeg";
            if (File.Exists(diretorio) == true) { return diretorio; }
            else { return null; }
        }
        public string GerarQRCODE(string diretorio, string codigo, string tag, int numAleatorio)
        {
            Arquivo = codigo + ".jpeg";
            tag += alfanumericoAleatorio(numAleatorio);
            Bitmap QR = GerarQRCode(200, 200, tag);
            diretorio = diretorio + "\\" + Arquivo;
            bool excluir = RemoveFileFromServer(diretorio);
            QR.Save(diretorio, System.Drawing.Imaging.ImageFormat.Jpeg);
            return tag;
        }
        public string GerarQRCODE(string diretorio,  string tag)
        {
            Arquivo = ".jpeg";
            Bitmap QR = GerarQRCode(200, 200, tag);
            diretorio = diretorio + ".jpeg";


            //if (File.Exists(diretorio) == true) { File.Delete(diretorio); }
            bool excluir = RemoveFileFromServer(diretorio);
            QR.Save(diretorio, System.Drawing.Imaging.ImageFormat.Jpeg);
            return diretorio;
        }
        public string GerarQRCODEStringImg(int numAleatorio, string tag)
        {
            tag += ";" + alfanumericoAleatorio(numAleatorio);
            string QR = GerarQRCodeImagemSTR(200, 200, tag);
            TAG = tag;
            return QR;
        }
        public string GerarQRCODEStringImg(string tag)
        {
            string QR = GerarQRCodeImagemSTR(200, 200, tag);

            return QR;
        }
        public byte[] GerarQRCODEStringBYTE(string tag)
        {
            return   GerarQRCodeImagemBYTE(200, 200, tag);
        }
        private bool RemoveFileFromServer(string path)
        {
            if (!System.IO.File.Exists(path)) return false;

            try //Maybe error could happen like Access denied or Presses Already User used
            {
                FileInfo fInfo;

                fInfo = new FileInfo(path);

                fInfo.Delete();

                System.IO.File.Delete(path);
                return true;
            }
            catch (Exception e)
            {
                //Debug.WriteLine(e.Message);
            }
            return false;
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
        private string GerarQRCodeImagemSTR(int width, int height, string text)
        {
            try
            {
                var bw = new ZXing.BarcodeWriter();
                var encOptions = new ZXing.Common.EncodingOptions() { Width = width, Height = height, Margin = 0 };
                bw.Options = encOptions;
                bw.Format = ZXing.BarcodeFormat.QR_CODE;
                var resultado = new Bitmap(bw.Write(text));

                MemoryStream ms = new MemoryStream();
                resultado.Save(ms, ImageFormat.Jpeg);
                var base64Data = Convert.ToBase64String(ms.ToArray());
                string imagemString = "data:image/gif;base64," + base64Data;



                return imagemString;
            }
            catch
            {
                throw;
            }
        }
        private byte[] GerarQRCodeImagemBYTE(int width, int height, string text)
        {
            try
            {
                var bw = new ZXing.BarcodeWriter();
                var encOptions = new ZXing.Common.EncodingOptions() { Width = width, Height = height, Margin = 0 };
                bw.Options = encOptions;
                bw.Format = ZXing.BarcodeFormat.QR_CODE;
                var resultado = new Bitmap(bw.Write(text));

                MemoryStream ms = new MemoryStream();
                resultado.Save(ms, ImageFormat.Jpeg);
                BinaryReader reader = new BinaryReader(ms);
                byte[] photo = ConvertImageToByteArray(resultado, System.Drawing.Imaging.ImageFormat.Jpeg);
                
                return photo;
            }
            catch
            {
                throw;
            }
        }
        private byte[] ConvertImageToByteArray(System.Drawing.Image imageToConvert,
                                       System.Drawing.Imaging.ImageFormat formatOfImage)
        {
            byte[] Ret;
            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    imageToConvert.Save(ms, formatOfImage);
                    Ret = ms.ToArray();
                }
            }
            catch (Exception) { throw; }
            return Ret;
        }
        public string NumeroAleatorio()
        {
            Random rd = new Random();
            int num= rd.Next(1000000, 9000000);
            return num.ToString();
        }
        private  string alfanumericoAleatorio(int tamanho)
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            var result = new string(
                Enumerable.Repeat(chars, tamanho)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray());
            return result;
        }
    }
}
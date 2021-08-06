using Microsoft.AspNetCore.Razor.Runtime.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ZXing.QrCode;

namespace Clase14.TagHelpers
{
    // You may need to install the Microsoft.AspNetCore.Razor.Runtime package into your project
    [HtmlTargetElement("qr")] //<qr></qr>
    public class Ejemplo3 : TagHelper
    {
        [HtmlAttributeName("valor")]
        public string contenido { get; set; }
        [HtmlAttributeName("ancho")]
        public int ancho { get; set; }
        [HtmlAttributeName("alto")]
        public int alto { get; set; }//<qr valor="" ancho="" alto=""></qr>
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            //Generación de la imagen QR
            var datosDeBarcodeWriter = new ZXing.BarcodeWriterPixelData {
                Format = ZXing.BarcodeFormat.QR_CODE,//CODE_128  --> Código de barras
                Options = new QrCodeEncodingOptions { 
                    Height = alto,
                    Width = ancho,
                    Margin = 0
                }
            };
            var datosPixel = datosDeBarcodeWriter.Write(contenido);
            using (Bitmap bitmap = new Bitmap(datosPixel.Width, datosPixel.Height, System.Drawing.Imaging.PixelFormat.Format32bppRgb)) {
                using (MemoryStream flujoDeMemoria = new MemoryStream()) {
                    var datosDelBitmap = bitmap.LockBits(new Rectangle(0,0,datosPixel.Width,datosPixel.Height), System.Drawing.Imaging.ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format32bppRgb);
                    try
                    {
                        System.Runtime.InteropServices.Marshal.Copy(datosPixel.Pixels, 0, datosDelBitmap.Scan0, datosPixel.Pixels.Length);
                    }
                    finally {
                        bitmap.UnlockBits(datosDelBitmap);
                    }
                    bitmap.Save(flujoDeMemoria, System.Drawing.Imaging.ImageFormat.Png);
                    //Pasarlo al output
                    output.TagName = "img";
                    output.Attributes.Clear();
                    output.Attributes.Add("src", String.Format("data:image/png;base64,{0}", Convert.ToBase64String(flujoDeMemoria.ToArray())));
                    output.Attributes.Add("width", ancho);
                    output.Attributes.Add("height", alto);
                }
            }
            
        }
    }
}

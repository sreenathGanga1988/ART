using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZXing;
using ZXing.QrCode;

namespace ArtWebApp.Reports.Inventoryreport
{
    public partial class RollDataPrinter : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            

           
          
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //QrCodeEncodingOptions options = new QrCodeEncodingOptions
            //{
            //    DisableECI = true,
            //    CharacterSet = "UTF-8",
            //    Width = 230,
            //    Height = 230,
            //};
            //var qr = new ZXing.BarcodeWriter();
            //qr.Options = options;
            //qr.Format = ZXing.BarcodeFormat.QR_CODE;
            //Bitmap result = new Bitmap(qr.Write(TextBox1.Text.Trim()));


            //using (MemoryStream ms = new MemoryStream())
            //{
            //    result.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            //    byte[] byteImage = ms.ToArray();

            //    Convert.ToBase64String(byteImage);
            //    Image1.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(byteImage);
            //}

            
        }



         public void addbarcode(String rollnum, System.Web.UI.WebControls.Image Image1)
        {
            QrCodeEncodingOptions options = new QrCodeEncodingOptions
            {
                DisableECI = true,
                CharacterSet = "UTF-8",
                Width = 200,
                Height = 200,
            };
            var qr = new ZXing.BarcodeWriter();
            qr.Options = options;
            qr.Format = ZXing.BarcodeFormat.QR_CODE;
            Bitmap result = new Bitmap(qr.Write(rollnum));


            using (MemoryStream ms = new MemoryStream())
            {
                result.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                byte[] byteImage = ms.ToArray();

                Convert.ToBase64String(byteImage);
                Image1.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(byteImage);
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                String lbl_rollPk = (e.Row.FindControl("lbl_rollPk") as Label).Text.Trim();
             System.Web.UI.WebControls.Image imgage = (e.Row.FindControl("Image1") as System.Web.UI.WebControls.Image);


                addbarcode(lbl_rollPk, imgage);


            }
            catch (Exception)
            {

            }
         
        }
    }
}
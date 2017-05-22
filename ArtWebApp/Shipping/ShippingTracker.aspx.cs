using ArtWebApp.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
namespace ArtWebApp.Shipping
{
    public partial class ShippingTracker : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void BTN_Click(object sender, EventArgs e)
        {
          

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @" SELECT 
                         ShippingDocumentMaster.ShipDocNum, ShippingDocumentMaster.ShipperName, ShippingDocumentMaster.ExporterName, ShippingDocumentMaster.ShipperInv, ShippingDocumentMaster.Vessel, 
                         ShippingDocumentMaster.Conatianer, ShippingDocumentMaster.ETA
FROM            ShippingDocumentDetails INNER JOIN
                         ShippingDocumentMaster ON ShippingDocumentDetails.ShipingDoc_PK = ShippingDocumentMaster.ShipingDoc_PK INNER JOIN
                         DocMaster ON ShippingDocumentDetails.Doc_Pk = DocMaster.Doc_Pk
WHERE(DocMaster.DocNum  like  @Param1)";

                cmd.Parameters.AddWithValue("@Param1", txt_adn.Text);
               GridView1.DataSource = QueryFunctions.ReturnQueryResultDatatable(cmd);
                GridView1.DataBind();



          
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @" SELECT 
                         ShippingDocumentMaster.ShipDocNum, ShippingDocumentMaster.ShipperName, ShippingDocumentMaster.ExporterName, ShippingDocumentMaster.AddedBY, ShippingDocumentMaster.BL, 
                         ShippingDocumentMaster.ETA, ShippingDocumentMaster.Conatianer, ShippingDocumentMaster.Vessel, DeliveryOrderMaster.DONum
FROM            ShippingDocumentMaster INNER JOIN
                         ShippingDocumentDODetails ON ShippingDocumentMaster.ShipingDoc_PK = ShippingDocumentDODetails.ShipingDoc_PK INNER JOIN
                         DeliveryOrderMaster ON ShippingDocumentDODetails.DO_PK = DeliveryOrderMaster.DO_PK
WHERE        (DeliveryOrderMaster.DONum = @Param1)";

            cmd.Parameters.AddWithValue("@Param1", txt_do.Text);
            GridView1.DataSource = QueryFunctions.ReturnQueryResultDatatable(cmd);
            GridView1.DataBind();
        }

        protected void TextBox3_TextChanged(object sender, EventArgs e)
        {
          

        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"SELECT        ShipDocNum, ShipperName, ExporterName, AddedBY, BL, ETA, Conatianer, Vessel, ShipperInv
FROM            ShippingDocumentMaster
WHERE        (Vessel = @Param1) or (Conatianer = @Param1) or (BL = @Param1) or (ShipDocNum = @Param1) or (ShipperInv = @Param1)";

            cmd.Parameters.AddWithValue("@Param1", txt_ref.Text);
            GridView1.DataSource = QueryFunctions.ReturnQueryResultDatatable(cmd);
            GridView1.DataBind();
        }
    }
}
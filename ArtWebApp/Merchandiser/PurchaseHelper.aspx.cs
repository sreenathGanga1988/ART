using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
namespace ArtWebApp.Merchandiser
{
    public partial class PurchaseHelper : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            filltemplate();
            filltemplateCompostion();
            filltemplateconstruction();
        }




        public void filltemplate()
        {
            SqlCommand cmd = new SqlCommand(@"SELECT        Template_PK, TemplateCode, Description
FROM            Template_Master
WHERE        (Description like '%"+txt_itemdescription.Text.Trim()+"%')");

            tbl_template.DataSource = QueryFunctions.ReturnQueryResultDatatable(cmd);
            tbl_template.DataBind();
           
            upd_templategrid.Update();
        }

        public void filltemplateCompostion()
        {
            SqlCommand cmd = new SqlCommand(@"SELECT        TemplateComposition.TemplateCom_Pk, Template_Master.Description, TemplateComposition.Composition,Template_Master.Template_PK
FROM            Template_Master INNER JOIN
                         TemplateComposition ON Template_Master.Template_PK = TemplateComposition.Template_Pk
WHERE        (TemplateComposition.Composition like '%" + txt_itemdescription.Text.Trim() + "%')");

            tbl_comp.DataSource = QueryFunctions.ReturnQueryResultDatatable(cmd);
            tbl_comp.DataBind();

            upd_compositiongrid.Update();
        }


        public void filltemplateconstruction()
        {
            SqlCommand cmd = new SqlCommand(@"SELECT        Template_Master.Description, TemplateConstruction.Construct, TemplateConstruction.TemplateCon_Pk
FROM            Template_Master INNER JOIN
                         TemplateConstruction ON Template_Master.Template_PK = TemplateConstruction.Template_Pk
WHERE        (TemplateConstruction.Construct like '%" + txt_itemdescription.Text.Trim() + "%')");

            tbl_con.DataSource = QueryFunctions.ReturnQueryResultDatatable(cmd);
            tbl_con.DataBind();

            upd_constructiongrid.Update();
        }

        protected void tbl_template_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int rownum = int.Parse(e.CommandArgument.ToString());
            int lbl_templatepk = int.Parse((tbl_template.Rows[rownum].FindControl("lbl_templatepk") as Label).Text);

            SqlCommand cmd = new SqlCommand(@"SELECT        StockPOMaster.SPO_Pk, StockPOMaster.SPONum, StockPOMaster.AddedDate, StockPOMaster.ApprovedBy, StockPOMaster.DeliveryDate, StockPOMaster.IsApproved, UOMMaster.UomCode, 
                         StockPODetails.POQty, StockPODetails.CUrate, StockPODetails.TemplateColor, StockPODetails.TemplateSize, StockPODetails.TemplateWidth, StockPODetails.TemplateWeight, StockPODetails.Composition, 
                         StockPODetails.Construct, StockPODetails.SPODetails_PK, SupplierMaster.SupplierName, StockPODetails.Template_PK, PaymentTermMaster.PaymentCodeDescription, DeliveryTermMaster.DeliveryTerm
FROM            StockPODetails INNER JOIN
                         StockPOMaster ON StockPODetails.SPO_PK = StockPOMaster.SPO_Pk INNER JOIN
                         UOMMaster ON StockPODetails.Uom_PK = UOMMaster.Uom_PK INNER JOIN
                         SupplierMaster ON StockPOMaster.Supplier_Pk = SupplierMaster.Supplier_PK INNER JOIN
                         PaymentTermMaster ON StockPOMaster.PaymentTermID = PaymentTermMaster.PaymentTermID INNER JOIN
                         DeliveryTermMaster ON StockPOMaster.DeliveryTerms_Pk = DeliveryTermMaster.DeliveryTerms_Pk WHERE  (StockPODetails.Template_PK = @Param1) ");
            cmd.Parameters.AddWithValue("@Param1", lbl_templatepk);

            DataTable dt= QueryFunctions.ReturnQueryResultDatatable(cmd);

            filldetails(dt);
        }




        public void filldetails( DataTable dt)
        {
            if(dt.Rows.Count>0)
            {
                DataTable maxRow = dt.Select("CUrate = MAX(CUrate)").CopyToDataTable();
                lbl_maxsuppier.Text = maxRow.Rows[0]["SupplierName"].ToString();
                lbl_maxunitprice.Text = maxRow.Rows[0]["CUrate"].ToString();
                lbl_maxpaymentterm.Text = maxRow.Rows[0]["PaymentCodeDescription"].ToString();
                lbl_maxqty.Text = maxRow.Rows[0]["POQty"].ToString();
                lbl_maxuom.Text = maxRow.Rows[0]["UomCode"].ToString();
                lbl_maxaDDEDDATE.Text = maxRow.Rows[0]["AddedDate"].ToString();
                lbl_maxdeliveryterm.Text = maxRow.Rows[0]["DeliveryTerm"].ToString();

                DataTable minRow = dt.Select("CUrate = MIN(CUrate)").CopyToDataTable();
                lbl_minsupplier.Text = minRow.Rows[0]["SupplierName"].ToString();
                lbl_minunitprice.Text = minRow.Rows[0]["CUrate"].ToString();
                lbl_minpaymentterm.Text = minRow.Rows[0]["PaymentCodeDescription"].ToString();
                lbl_minqty.Text = minRow.Rows[0]["POQty"].ToString();
                lbl_minuom.Text = minRow.Rows[0]["UomCode"].ToString();
                lbl_minaddedate.Text = minRow.Rows[0]["AddedDate"].ToString();
                lbl_minaddedate.Text = minRow.Rows[0]["DeliveryTerm"].ToString();

                tbl_podetails.DataSource = dt;
                tbl_podetails.DataBind();

                upd_pricedetail.Update();
            }
            else
            {
                lbl_maxsuppier.Text = "";
                lbl_maxunitprice.Text = "";
                lbl_maxpaymentterm.Text = "";
                lbl_maxqty.Text ="";
                lbl_maxuom.Text = "";
                lbl_maxaDDEDDATE.Text = "";
                lbl_maxdeliveryterm.Text = "";
                lbl_minsupplier.Text = "";
                lbl_minunitprice.Text = "";
                lbl_minpaymentterm.Text = "";
                lbl_minqty.Text = "";
                lbl_minuom.Text = "";
                lbl_minaddedate.Text = "";
                lbl_minaddedate.Text = "";


                tbl_podetails.DataSource = null;
                tbl_podetails.DataBind();
                upd_pricedetail.Update();
            }
           

        }

        protected void tbl_comp_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if(e.CommandName== "ShowPoHistory")
            {
                int rownum = int.Parse(e.CommandArgument.ToString());
                int lbl_templatepk = int.Parse((tbl_comp.Rows[rownum].FindControl("lbl_templatepk") as Label).Text);
                int lbl_TemplateCom_Pk = int.Parse((tbl_comp.Rows[rownum].FindControl("lbl_TemplateCom_Pk") as Label).Text);
                string lbl_composition = (tbl_comp.Rows[rownum].FindControl("lbl_composition") as Label).Text;
                SqlCommand cmd = new SqlCommand(@"   SELECT        StockPOMaster.SPO_Pk, StockPOMaster.SPONum, StockPOMaster.AddedDate, StockPOMaster.ApprovedBy, StockPOMaster.DeliveryDate, StockPOMaster.IsApproved, UOMMaster.UomCode, 
                         StockPODetails.POQty, StockPODetails.CUrate, StockPODetails.TemplateColor, StockPODetails.TemplateSize, StockPODetails.TemplateWidth, StockPODetails.TemplateWeight, StockPODetails.Composition, 
                         StockPODetails.Construct, StockPODetails.SPODetails_PK, SupplierMaster.SupplierName, StockPODetails.Template_PK, PaymentTermMaster.PaymentCodeDescription, DeliveryTermMaster.DeliveryTerm
FROM            StockPODetails INNER JOIN
                         StockPOMaster ON StockPODetails.SPO_PK = StockPOMaster.SPO_Pk INNER JOIN
                         UOMMaster ON StockPODetails.Uom_PK = UOMMaster.Uom_PK INNER JOIN
                         SupplierMaster ON StockPOMaster.Supplier_Pk = SupplierMaster.Supplier_PK INNER JOIN
                         PaymentTermMaster ON StockPOMaster.PaymentTermID = PaymentTermMaster.PaymentTermID INNER JOIN
                         DeliveryTermMaster ON StockPOMaster.DeliveryTerms_Pk = DeliveryTermMaster.DeliveryTerms_Pk
WHERE        (StockPODetails.Template_PK = @Param1) AND (StockPODetails.Composition like '%" + lbl_composition.Trim() + "%')");
                cmd.Parameters.AddWithValue("@Param1", lbl_templatepk);


                DataTable dt = QueryFunctions.ReturnQueryResultDatatable(cmd);

                filldetails(dt);
            }
           

         

        }
    }
}
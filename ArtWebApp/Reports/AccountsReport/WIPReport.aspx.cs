using Microsoft.Reporting.WebForms;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArtWebApp.Reports.AccountsReport
{
    public partial class WIPReport : System.Web.UI.Page
    {String connStr = System.Configuration.ConfigurationManager.ConnectionStrings["ArtConnectionString"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        { ArrayList atclist = new ArrayList();
            List<Infragistics.Web.UI.ListControls.DropDownItem> items = cmb_atc.SelectedItems;
            foreach (Infragistics.Web.UI.ListControls.DropDownItem item in items)
            {

                int atcid = int.Parse(item.Value.ToString());
                atclist.Add(atcid);
            }


            if (atclist.Count > 0 && atclist != null)
            {
                DataTable masterdata = createdatataable(atclist);

                DateTime uptodate = DateTime.Parse(dtp_to.Value.ToString());

                foreach (System.Data.DataColumn col in masterdata.Columns) col.ReadOnly = false;
                for (int i = 0; i < masterdata.Rows.Count; i++)
                {
                    int atcid = int.Parse(masterdata.Rows[i]["atcid"].ToString());


                    masterdata = FillWFData(masterdata, atcid, uptodate, i);

                    masterdata = FillPackedData(masterdata, atcid, uptodate, i);


                 //   masterdata = FillINVData(masterdata, atcid, uptodate, i);

                    masterdata = calculateWIPvalues(masterdata, i);

                }

                showreport(masterdata);
                //GridView1.DataSource = masterdata;
                //GridView1.DataBind();
            }
        }
  
    
    
    
    
        public DataTable FillWFData(DataTable masterdata, int atcid, DateTime uptodate,int i)
        {
            float sumoftrim = 0;
            float sumoffabric = 0;

            DataTable invtrydata = GetInventoryData(atcid, uptodate);

            if (invtrydata.Rows.Count > 0)
            {
                try
                {
                    object Sumfabric = invtrydata.Compute("Sum(Wfvalue)", "ItemGroup_PK= " + 1 + "");

                    sumoffabric = float.Parse(Sumfabric.ToString());
                }
                catch (Exception)
                {


                }


                try
                {
                    object Sumtrim = invtrydata.Compute("Sum(Wfvalue)", "ItemGroup_PK= " + 2 + "");
                    sumoftrim = float.Parse(Sumtrim.ToString());
                }
                catch (Exception)
                {


                }

                masterdata.Rows[i]["WFValue"] = (sumoffabric + sumoftrim).ToString();
                masterdata.Rows[i]["WFFabricvalue"] = sumoffabric.ToString();
                masterdata.Rows[i]["WFTrimsValue"] = sumoftrim.ToString();
            }

            return masterdata;
        }

      


        public DataTable FillPackedData(DataTable masterdata, int atcid, DateTime uptodate, int i)
        {
            float sumoftrim = 0;
            float sumoffabric = 0;
            float sumofproces = 0;
            DataTable packeddata = GetPackedData(atcid);
            if (packeddata.Rows.Count > 0)
            {
                if (packeddata.Rows.Count > 0)
                {
                    masterdata.Rows[i]["PackedQty"] = float.Parse(packeddata.Rows[0]["PackedQty"].ToString());
                    masterdata.Rows[i]["PackedQtyValue"] = float.Parse(packeddata.Rows[0]["PackedQtyvalue"].ToString());
                }



            }



            //DataTable packedDetaildata = GetPackeddetails(atcid,uptodate);


            DataTable packedDetaildata = getsomecorrectcosting(SUBGetPackeddetails(atcid, uptodate));

            if (packedDetaildata.Rows.Count > 0)
            {







                try
                {
                    object Sumfabric = packedDetaildata.Compute("Sum(fabvalue)", "");
                    sumoffabric = float.Parse(Sumfabric.ToString());
                }
                catch (Exception)
                {
                }

                try
                {
                    object Sumtrim = packedDetaildata.Compute("Sum(trimvalue)", "");
                    sumoftrim = float.Parse(Sumtrim.ToString());
                }
                catch (Exception)
                {
                }

                try
                {
                    object pacprocess = packedDetaildata.Compute("Sum(Prackedprocessvalue)", "");
                    sumofproces = float.Parse(pacprocess.ToString());
                }
                catch (Exception)
                {
                }

                masterdata.Rows[i]["PackedQtyFabricvalue"] = float.Parse(sumoffabric.ToString());
                masterdata.Rows[i]["PackedQtyTrimsValue"] = float.Parse(sumoftrim.ToString());
                masterdata.Rows[i]["PackedQtyProcessValue"] = float.Parse(sumofproces.ToString());







                float invtrimvalue = 0, invfabvalue = 0, invprocessvalue = 0, invqty = 0;

                try
                {
                    object Sumfabric = packedDetaildata.Compute("Sum(invoicefabvalue)", "");

                    invfabvalue = float.Parse(Sumfabric.ToString());
                }
                catch (Exception)
                {
                }

                try
                {
                    object Sumtrim = packedDetaildata.Compute("Sum(invoicetrimvalue)", "");
                    invtrimvalue = float.Parse(Sumtrim.ToString());
                }
                catch (Exception)
                {

                }


                try
                {
                    object SumInvoiceQty = packedDetaildata.Compute("Sum(InvoicedQty)", "");
                    invqty = float.Parse(SumInvoiceQty.ToString());
                }
                catch (Exception)
                {


                }


                try
                {
                    object suminvprocessvalue = packedDetaildata.Compute("Sum(invprocessvalue)", "");
                    invprocessvalue = float.Parse(suminvprocessvalue.ToString());
                }
                catch (Exception)
                {


                }




                masterdata.Rows[i]["InvoicedQtyFabricvalue"] = invfabvalue.ToString();
                masterdata.Rows[i]["InvoicedQtyTrimsValue"] = invtrimvalue.ToString();
                masterdata.Rows[i]["InvoicedQty"] = invqty.ToString();
                masterdata.Rows[i]["InvoicedQtyProcessValue"] = invprocessvalue.ToString();








            }
            return masterdata;
        }


        //public DataTable FillPackedData(DataTable masterdata, int atcid, DateTime uptodate, int i)
        //{
        //    float sumoftrim = 0;
        //    float sumoffabric = 0;
        //    float sumofproces = 0;
        //    DataTable packeddata = GetPackedData(atcid);
        //    if (packeddata.Rows.Count > 0)
        //    {
        //        if (packeddata.Rows.Count > 0)
        //        {
        //            masterdata.Rows[i]["PackedQty"] = float.Parse(packeddata.Rows[0]["PackedQty"].ToString());
        //            masterdata.Rows[i]["PackedQtyValue"] = float.Parse(packeddata.Rows[0]["PackedQtyvalue"].ToString());
        //        }



        //    }
        //    DataTable packedDetaildata = GetPackeddetails(atcid,uptodate);

        //    if (packedDetaildata.Rows.Count > 0)
        //    {

        //        try
        //        {
        //            object Sumfabric = packedDetaildata.Compute("Sum(fabvalue)", "");

        //            sumoffabric = float.Parse(Sumfabric.ToString());
        //        }
        //        catch (Exception)
        //        {


        //        }


        //        try
        //        {
        //            object Sumtrim = packedDetaildata.Compute("Sum(trimvalue)", "");
        //            sumoftrim = float.Parse(Sumtrim.ToString());
        //        }
        //        catch (Exception)
        //        {


        //        }


        //        try
        //        {
        //            object pacprocess = packedDetaildata.Compute("Sum(Prackedprocessvalue)", "");
        //            sumofproces = float.Parse(pacprocess.ToString());
        //        }
        //        catch (Exception)
        //        {


        //        }

        //           masterdata.Rows[i]["PackedQtyFabricvalue"] = sumoffabric.ToString();
        //        masterdata.Rows[i]["PackedQtyTrimsValue"] = sumoftrim.ToString();
        //        masterdata.Rows[i]["PackedQtyProcessValue"] = sumofproces.ToString();

        //    }
        //    return masterdata;
        //}


        public DataTable SUBGetPackeddetails(int atcid, DateTime Uptodate)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                string Query1 = @"SELECT        OurStyleID, OurStyle, SUM(PackedQty) AS PackedQty, SUM(PackedQty * PackProcessvalue) AS Prackedprocessvalue, 0000.00 as fabvalue, 0000.00 as trimvalue, isnull((SELECT        SUM(InvoiceQty) AS Expr1
FROM            InvoiceDetail
WHERE(OurStyleID = tt.OurStyleID)), 0)as InvoicedQty,00.00 as invoicetrimvalue,000.000 as invoicefabvalue,PackProcessvalue, 00.00 as invprocessvalue
FROM(SELECT        AtcDetails.OurStyleID, AtcDetails.OurStyle, ProductionReportDetails.PackedQty,
                                                        (SELECT        SUM(StyleCostingComponentDetails.CompValue) AS Expr1
                                                          FROM            StyleCostingMaster INNER JOIN
                                                                                    StyleCostingComponentDetails ON StyleCostingMaster.Costing_PK = StyleCostingComponentDetails.Costing_PK
                                                          WHERE(StyleCostingMaster.IsApproved = N'A') AND(StyleCostingMaster.OurStyleID = AtcDetails.OurStyleID) AND(StyleCostingComponentDetails.CostComp_PK <> 8) AND
                                                                                    (StyleCostingComponentDetails.CostComp_PK <> 9) AND(StyleCostingComponentDetails.CostComp_PK <> 1) AND(StyleCostingComponentDetails.CostComp_PK <> 2))
                                                    AS PackProcessvalue
                          FROM ProductionReportDetails INNER JOIN
                                                    JobContractDetail ON ProductionReportDetails.JobContractDetail_pk = JobContractDetail.JobContractDetail_pk INNER JOIN
                                                    AtcDetails ON JobContractDetail.OurStyleID = AtcDetails.OurStyleID
                    where    (AtcDetails.AtcId=@atcid) ) AS tt
GROUP BY OurStyleID, OurStyle, PackProcessvalue ";


                cmd.CommandText = Query1;
                cmd.Parameters.AddWithValue("@atcid", atcid);
                //cmd.Parameters.AddWithValue("@uptodate", Uptodate);
                SqlDataReader rdr = cmd.ExecuteReader();

                dt.Load(rdr);



            }
            return dt;
        }




        public DataTable getsomecorrectcosting(DataTable packedDetaildata)
        {
            foreach (System.Data.DataColumn col in packedDetaildata.Columns) col.ReadOnly = false;
            for (int i = 0; i < packedDetaildata.Rows.Count; i++)
            {

                float sumoffabric = 0, sumoftrim = 0;

                int ourstyleid = int.Parse(packedDetaildata.Rows[i]["ourstyleid"].ToString());
                int packedQty = int.Parse(packedDetaildata.Rows[i]["PackedQty"].ToString());
                int InvoicedQty = int.Parse(packedDetaildata.Rows[i]["InvoicedQty"].ToString());
                float Processvalue = float.Parse(packedDetaildata.Rows[i]["PackProcessvalue"].ToString());
                DataTable sytlecostingdetails = filldetailsofourstyle(ourstyleid);
                foreach (System.Data.DataColumn col in sytlecostingdetails.Columns) col.ReadOnly = false;
                for (int j = 0; j < sytlecostingdetails.Rows.Count; j++)
                {
                    int Sku_PK = int.Parse(sytlecostingdetails.Rows[j]["Sku_PK"].ToString());
                    int baseUom_PK = int.Parse(sytlecostingdetails.Rows[j]["Uom_PK"].ToString());

                    float consumption = float.Parse(sytlecostingdetails.Rows[j]["Consumption"].ToString());

                    float curate = calculateAvgrate(Sku_PK, baseUom_PK);
                    sytlecostingdetails.Rows[j]["skuvalue"] = packedQty * consumption * curate;

                    sytlecostingdetails.Rows[j]["invskuvalue"] = InvoicedQty * consumption * curate;

                }


                try
                {
                    object Sumfabric = sytlecostingdetails.Compute("Sum(skuvalue)", "ItemGroup_PK= " + 1 + "");
                    sumoffabric = float.Parse(Sumfabric.ToString());
                }
                catch (Exception)
                {
                }

                try
                {
                    object Sumtrim = sytlecostingdetails.Compute("Sum(skuvalue)", "ItemGroup_PK= " + 2 + "");
                    sumoftrim = float.Parse(Sumtrim.ToString());
                }
                catch (Exception)
                {
                }


                packedDetaildata.Rows[i]["fabvalue"] = float.Parse(sumoffabric.ToString());
                packedDetaildata.Rows[i]["trimvalue"] = float.Parse(sumoftrim.ToString());




                float invtrimvalue = 0, invfabvalue = 0;

                try
                {
                    object Sumfabric = sytlecostingdetails.Compute("Sum(invskuvalue)", "ItemGroup_PK= " + 1 + "");
                    invfabvalue = float.Parse(Sumfabric.ToString());
                }
                catch (Exception)
                {
                }

                try
                {
                    object Sumtrim = sytlecostingdetails.Compute("Sum(invskuvalue)", "ItemGroup_PK= " + 2 + "");
                    invtrimvalue = float.Parse(Sumtrim.ToString());
                }
                catch (Exception)
                {
                }

                packedDetaildata.Rows[i]["invoicetrimvalue"] = float.Parse(invtrimvalue.ToString());
                packedDetaildata.Rows[i]["invoicefabvalue"] = float.Parse(invfabvalue.ToString());
                packedDetaildata.Rows[i]["invprocessvalue"] = float.Parse(packedDetaildata.Rows[i]["PackProcessvalue"].ToString()) * float.Parse(packedDetaildata.Rows[i]["InvoicedQty"].ToString());








            }
            return packedDetaildata;
        }


        public DataTable filldetailsofourstyle(int ourstyleid)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"SELECT        StyleCostingDetails.Sku_PK, StyleCostingDetails.Consumption, Template_Master.ItemGroup_PK,00.00 AS skuvalue,SkuRawMaterialMaster.Uom_PK,00.00 AS invskuvalue
FROM            StyleCostingDetails INNER JOIN
                         StyleCostingMaster ON StyleCostingDetails.Costing_PK = StyleCostingMaster.Costing_PK INNER JOIN
                         SkuRawMaterialMaster ON StyleCostingDetails.Sku_PK = SkuRawMaterialMaster.Sku_Pk INNER JOIN
                         Template_Master ON SkuRawMaterialMaster.Template_pk = Template_Master.Template_PK
WHERE        (StyleCostingMaster.IsApproved = N'A') AND (StyleCostingMaster.OurStyleID = @ourstyle)";
            cmd.Parameters.AddWithValue("@ourstyle", ourstyleid);
            return QueryFunctions.ReturnQueryResultDatatable(cmd);
        }





        public float calculateAvgrate(int sku_PK, int baseuompk)
        {
            float curate = 0;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"SELECT        AVG(InventoryMaster.CURate) AS curate, ISNULL(InventoryMaster.Uom_Pk, 0) AS Uom_Pk, SkuRawmaterialDetail.SkuDet_PK
FROM            InventoryMaster INNER JOIN
                         SkuRawmaterialDetail ON InventoryMaster.SkuDet_Pk = SkuRawmaterialDetail.SkuDet_PK
GROUP BY InventoryMaster.Uom_Pk, SkuRawmaterialDetail.SkuDet_PK, SkuRawmaterialDetail.Sku_PK
HAVING        (SkuRawmaterialDetail.Sku_PK = @skupk)";
            cmd.Parameters.AddWithValue("@skupk", sku_PK);
            DataTable dt = QueryFunctions.ReturnQueryResultDatatable(cmd);


            if (dt.Rows.Count > 0)
            {

                foreach (System.Data.DataColumn col in dt.Columns) col.ReadOnly = false;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    curate = float.Parse(dt.Rows[i]["curate"].ToString());
                    int uomPK = int.Parse(dt.Rows[i]["Uom_Pk"].ToString());

                    if (uomPK != baseuompk)
                    {
                        curate = UOMConvertortoAlt(baseuompk, uomPK, curate);
                    }

                    dt.Rows[i]["curate"] = curate;
                }


            }

            try
            {
                object avgFiveSbefore = dt.Compute("Avg(curate)", "");
                curate = float.Parse(avgFiveSbefore.ToString());
            }
            catch
            {

            }



            return curate;

        }




        /// <summary>
        /// convert the qnty in alt UOm rate to the base uom rate
        /// </summary>
        /// <param name="uomPK"></param>
        /// <param name="auomPk"></param>
        /// <param name="balqtyinBaseuom"></param>
        /// <returns></returns>
        public float UOMConvertortoAlt(int uomPK, int auomPk, float curate)
        {

            float newcurate = 0;
            float operend = 1;
            String operatorused = "*";

            DataTable dt = getAltuomdata(uomPK, auomPk);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {

                    operend = float.Parse(dt.Rows[0]["Conv_fact"].ToString());
                    operatorused = dt.Rows[0]["Operator"].ToString().Trim();
                    if (operatorused == "*")
                    {

                        newcurate = curate * operend;
                    }
                    else if (operatorused == "/")
                    {
                        newcurate = curate / operend;
                    }
                }





            }
            return newcurate;


        }


        public DataTable getAltuomdata(int baseuom_pk, int altuom_pk)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();




                SqlCommand cmd = new SqlCommand(@"SELECT        Conv_fact, Operator
FROM            AltUOMMaster
WHERE        (Uom_PK = @baseuom) AND (AltUom_PK = @altuom)", con);
                cmd.Parameters.AddWithValue("@baseuom", baseuom_pk);
                cmd.Parameters.AddWithValue("@altuom", altuom_pk);
                SqlDataReader rdr = cmd.ExecuteReader();

                dt.Load(rdr);



            }
            return dt;
        }


















        //public DataTable FillINVData(DataTable masterdata, int atcid, DateTime uptodate, int i)
        //{
        //    float sumoftrim = 0;
        //    float sumoffabric = 0;
        //    float invoiceqty = 0;
        //    float invoicevalue = 0;
        //    float invporocessvalue = 0;
        //    DataTable invdata = GetInvoiceData(atcid, uptodate);
           
               

        //            if (invdata.Rows.Count > 0)
        //    {

        //        try
        //        {
        //            object Sumfabric = invdata.Compute("Sum(fabvalue)", "");

        //            sumoffabric = float.Parse(Sumfabric.ToString());
        //        }
        //        catch (Exception)
        //        {


        //        }


        //        try
        //        {
        //            object Sumtrim = invdata.Compute("Sum(trimvalue)", "");
        //            sumoftrim = float.Parse(Sumtrim.ToString());
        //        }
        //        catch (Exception)
        //        {


        //        }
        //        try
        //        {
        //            object Suminvoicevalue = invdata.Compute("Sum(InvoiceQtyValue)", "");

        //            invoicevalue = float.Parse(Suminvoicevalue.ToString());
        //        }
        //        catch (Exception)
        //        {


        //        }


        //        try
        //        {
        //            object SumInvoiceQty = invdata.Compute("Sum(InvoiceQty)", "");
        //            invoiceqty = float.Parse(SumInvoiceQty.ToString());
        //        }
        //        catch (Exception)
        //        {


        //        }


        //        try
        //        {
        //            object SumInvoiceQty = invdata.Compute("Sum(Invprocessvalue)", "");
        //            invporocessvalue = float.Parse(SumInvoiceQty.ToString());
        //        }
        //        catch (Exception)
        //        {


        //        }


                     
        //        masterdata.Rows[i]["InvoicedQtyFabricvalue"] = sumoffabric.ToString();
        //        masterdata.Rows[i]["InvoicedQtyTrimsValue"] = sumoftrim.ToString();
        //        masterdata.Rows[i]["InvoicedQty"] = invoiceqty.ToString ();
        //        masterdata.Rows[i]["InvoicedQtyValue"] = invoicevalue.ToString();
        //        masterdata.Rows[i]["InvoicedQtyProcessValue"] = invporocessvalue.ToString();

        //    }
        //    return masterdata;
        //}





        public DataTable createdatataable(ArrayList Atclist)
        {
            DataTable dt = new DataTable();
            string condition = "AND";
            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;


                for (int i = 0; i < Atclist.Count; i++)
                {
                    if (i == 0)
                    {
                        condition = "where AtcId =" + Atclist[i].ToString().Trim();
                    }
                    else
                    {
                        condition = condition + "  or AtcId=" + Atclist[i].ToString().Trim();
                    }



                }


                string Query1 = @"SELECT        AtcId, AtcNum, 0.0 as WFValue,0.0 as WFFabricvalue,
0.0 as WFTrimsValue,0.0 as PackedQty,0.0 as PackedQtyValue,0.0 as PackedQtyFabricvalue,
0.0 as PackedQtyTrimsValue,0.0 as PackedQtyProcessValue,0.0 as InvoicedQty,0.0 as InvoicedQtyValue,0.0 as InvoicedQtyFabricvalue,
0.0 as InvoicedQtyTrimsValue,0.0 as InvoicedQtyProcessValue ,0.0 as WIPWFValue,0.0 as WIPFGValue,0.0 as WIPInvoicedValue
FROM            AtcMaster "+condition ;


                cmd.CommandText = Query1;
                //cmd.Parameters.AddWithValue("@PONUm", PO);

                SqlDataReader rdr = cmd.ExecuteReader();

                dt.Load(rdr);



            }
            return dt;
        }


        public DataTable GetInventoryData(int atcid,DateTime Uptodate)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                string Query1 = @"SELECT        SUM(InventoryMaster.OnhandQty * InventoryMaster.CURate) AS Wfvalue, Template_Master.ItemGroup_PK, InventoryMaster.AddedDate
FROM            InventoryMaster INNER JOIN
                         SkuRawmaterialDetail ON InventoryMaster.SkuDet_Pk = SkuRawmaterialDetail.SkuDet_PK INNER JOIN
                         SkuRawMaterialMaster ON SkuRawmaterialDetail.Sku_PK = SkuRawMaterialMaster.Sku_Pk INNER JOIN
                         Template_Master ON SkuRawMaterialMaster.Template_pk = Template_Master.Template_PK
GROUP BY Template_Master.ItemGroup_PK, SkuRawMaterialMaster.Atc_id, InventoryMaster.ReceivedVia, InventoryMaster.AddedDate
HAVING        (SkuRawMaterialMaster.Atc_id = @atcid) AND (InventoryMaster.ReceivedVia = N'FR') AND (InventoryMaster.AddedDate <= @uptodate)";


                cmd.CommandText = Query1;
                cmd.Parameters.AddWithValue("@atcid", atcid);
                cmd.Parameters.AddWithValue("@uptodate", Uptodate);
                SqlDataReader rdr = cmd.ExecuteReader();

                dt.Load(rdr);



            }
            return dt;
        }

        public DataTable GetPackedData(int atcid)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                string Query1 = @"SELECT    isnull(    SUM(ProductionReportDetails.PackedQty) ,0) AS PackedQty,   isnull(    SUM(ProductionReportDetails.PackedQty * JobContractDetail.CMvalue),0) AS PackedQtyvalue, AtcDetails.AtcId
FROM            ProductionReportDetails INNER JOIN
                         JobContractDetail ON ProductionReportDetails.JobContractDetail_pk = JobContractDetail.JobContractDetail_pk INNER JOIN
                         AtcDetails ON JobContractDetail.OurStyleID = AtcDetails.OurStyleID
GROUP BY AtcDetails.AtcId
HAVING        (AtcDetails.AtcId = @atcid)";


                cmd.CommandText = Query1;
                cmd.Parameters.AddWithValue("@atcid", atcid);

                SqlDataReader rdr = cmd.ExecuteReader();

                dt.Load(rdr);



            }
            return dt;
        }

//        //public DataTable GetInvoiceData(int atcid, DateTime Uptodate)
//        {
//            DataTable dt = new DataTable();

//            using (SqlConnection con = new SqlConnection(connStr))
//            {
//                con.Open();
//                SqlCommand cmd = new SqlCommand();
//                cmd.Connection = con;
//                string Query1 = @"select tt.InvoiceQty ,tt.InvoiceQtyValue ,tt.OurStyle, (tt.InvoiceQty *tt.fabricvalue) as fabvalue ,(tt.InvoiceQty *tt.trimvalue) as trimvalue ,(tt.InvoiceQty *tt.invProcessvalue) as Invprocessvalue   from 
//(SELECT        ISNULL(InvoiceDetail.InvoiceQty, 0) AS InvoiceQty, ISNULL(InvoiceDetail.FOB * InvoiceDetail.InvoiceQty, 0) AS InvoiceQtyValue, AtcDetails.OurStyle

//,ISNULL((SELECT       StyleCostingComponentDetails.CompValue
//FROM            StyleCostingMaster INNER JOIN
//                         StyleCostingComponentDetails ON StyleCostingMaster.Costing_PK = StyleCostingComponentDetails.Costing_PK
//WHERE        (StyleCostingMaster.IsApproved = N'A') AND (StyleCostingMaster.OurStyleID = .AtcDetails.OurStyleID)  AND
//                      (StyleCostingComponentDetails.CostComp_PK = 1) ),0) AS fabricvalue , ISNULL((SELECT       StyleCostingComponentDetails.CompValue
//FROM            StyleCostingMaster INNER JOIN
//                         StyleCostingComponentDetails ON StyleCostingMaster.Costing_PK = StyleCostingComponentDetails.Costing_PK
//WHERE        (StyleCostingMaster.IsApproved = N'A') AND (StyleCostingMaster.OurStyleID = .AtcDetails.OurStyleID)  AND
//                      (StyleCostingComponentDetails.CostComp_PK = 2) ),0) AS trimvalue ,(SELECT     sum(  StyleCostingComponentDetails.CompValue)
//FROM            StyleCostingMaster INNER JOIN
//                         StyleCostingComponentDetails ON StyleCostingMaster.Costing_PK = StyleCostingComponentDetails.Costing_PK
//WHERE        (StyleCostingMaster.IsApproved = N'A') AND (StyleCostingMaster.OurStyleID = AtcDetails.OurStyleID)  AND
//                      ((StyleCostingComponentDetails.CostComp_PK != 8) and (StyleCostingComponentDetails.CostComp_PK != 9)  and (StyleCostingComponentDetails.CostComp_PK != 1) and (StyleCostingComponentDetails.CostComp_PK != 2) )) as invProcessvalue
//FROM            InvoiceDetail INNER JOIN
//                         AtcDetails ON InvoiceDetail.OurStyleID = AtcDetails.OurStyleID INNER JOIN
//                         InvoiceMaster ON InvoiceDetail.Invoice_PK = InvoiceMaster.Invoice_PK
//WHERE        (AtcDetails.AtcId = @atcid))tt";


//                cmd.CommandText = Query1;
//                cmd.Parameters.AddWithValue("@atcid", atcid);

//                SqlDataReader rdr = cmd.ExecuteReader();

//                dt.Load(rdr);



//            }
//            return dt;
//        }


//        public DataTable GetPackeddetails(int atcid, DateTime Uptodate)
//        {
//            DataTable dt = new DataTable();

//            using (SqlConnection con = new SqlConnection(connStr))
//            {
//                con.Open();
//                SqlCommand cmd = new SqlCommand();
//                cmd.Connection = con;
//                string Query1 = @"Select  tt.OurStyle, (tt.PackedQty *tt.fabricvalue) as fabvalue ,(tt.PackedQty *tt.trimvalue)  as trimvalue,(tt.PackedQty *tt.PackProcessvalue) as Prackedprocessvalue from(SELECT        AtcDetails.OurStyle, ProductionReportDetails.PackedQty,ISNULL((SELECT       StyleCostingComponentDetails.CompValue
//FROM            StyleCostingMaster INNER JOIN
//                         StyleCostingComponentDetails ON StyleCostingMaster.Costing_PK = StyleCostingComponentDetails.Costing_PK
//WHERE        (StyleCostingMaster.IsApproved = N'A') AND (StyleCostingMaster.OurStyleID = .AtcDetails.OurStyleID)  AND
//                      (StyleCostingComponentDetails.CostComp_PK = 1) ),0) AS fabricvalue , ISNULL((SELECT       StyleCostingComponentDetails.CompValue
//FROM            StyleCostingMaster INNER JOIN
//                         StyleCostingComponentDetails ON StyleCostingMaster.Costing_PK = StyleCostingComponentDetails.Costing_PK
//WHERE        (StyleCostingMaster.IsApproved = N'A') AND (StyleCostingMaster.OurStyleID = .AtcDetails.OurStyleID)  AND
//                      (StyleCostingComponentDetails.CostComp_PK = 2) ),0) AS trimvalue ,(SELECT     sum(  StyleCostingComponentDetails.CompValue)
//FROM            StyleCostingMaster INNER JOIN
//                         StyleCostingComponentDetails ON StyleCostingMaster.Costing_PK = StyleCostingComponentDetails.Costing_PK
//WHERE        (StyleCostingMaster.IsApproved = N'A') AND (StyleCostingMaster.OurStyleID = AtcDetails.OurStyleID)  AND
//                      ((StyleCostingComponentDetails.CostComp_PK != 8) and (StyleCostingComponentDetails.CostComp_PK != 9)  and (StyleCostingComponentDetails.CostComp_PK != 1) and (StyleCostingComponentDetails.CostComp_PK != 2) )) as PackProcessvalue
//FROM            ProductionReportDetails INNER JOIN
//                         JobContractDetail ON ProductionReportDetails.JobContractDetail_pk = JobContractDetail.JobContractDetail_pk INNER JOIN
//                         AtcDetails ON JobContractDetail.OurStyleID = AtcDetails.OurStyleID where (AtcDetails.AtcId=@atcid))tt ";


//                cmd.CommandText = Query1;
//                cmd.Parameters.AddWithValue("@atcid", atcid);
//                //cmd.Parameters.AddWithValue("@uptodate", Uptodate);
//                SqlDataReader rdr = cmd.ExecuteReader();

//                dt.Load(rdr);



//            }
//            return dt;
//        }
        
        
        
        public DataTable  calculateWIPvalues(DataTable masterdata, int i )
        {
            float PACKEDVALUE=float.Parse(masterdata.Rows[i]["PackedQtyFabricvalue"].ToString ())+float.Parse(masterdata.Rows[i]["PackedQtyTrimsValue"].ToString ())+float.Parse(masterdata.Rows[i]["PackedQtyProcessValue"].ToString ());
            float iNVVALUE = float.Parse(masterdata.Rows[i]["InvoicedQtyFabricvalue"].ToString()) + float.Parse(masterdata.Rows[i]["InvoicedQtyTrimsValue"].ToString()) + float.Parse(masterdata.Rows[i]["InvoicedQtyProcessValue"].ToString());
            masterdata.Rows[i]["WIPWFValue"] =float.Parse(masterdata.Rows[i]["WFValue"].ToString ())-( float.Parse(masterdata.Rows[i]["PackedQtyFabricvalue"].ToString ()) + float.Parse(masterdata.Rows[i]["PackedQtyTrimsValue"].ToString()));
            masterdata.Rows[i]["WIPFGValue"] = PACKEDVALUE - iNVVALUE;
            masterdata.Rows[i]["WIPInvoicedValue"] = float.Parse(masterdata.Rows[i]["InvoicedQtyValue"].ToString());

            return masterdata;

        }
        
        
        
        
        


        public void showreport(DataTable dt)
        {

           

            ReportDataSource datasource = new ReportDataSource("DataSet1", dt);
            this.ReportViewer1.LocalReport.DataSources.Clear();
            this.ReportViewer1.LocalReport.DataSources.Add(datasource);
            this.ReportViewer1.LocalReport.ReportPath = @"Reports\RDLC\WIPReport.rdlc";
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            DBTransaction.ReportTransactions.AccountReportrans acctrn = new DBTransaction.ReportTransactions.AccountReportrans();

            DataTable dt = acctrn.GetCostingData(0);

            ReportDataSource datasource = new ReportDataSource("DataSet1", dt);
            this.ReportViewer1.LocalReport.DataSources.Clear();
            this.ReportViewer1.LocalReport.DataSources.Add(datasource);
            this.ReportViewer1.LocalReport.ReportPath = @"Reports\RDLC\costingreport.rdlc";
        }
    }
    
    
    
    
    
    
    
    
    
}
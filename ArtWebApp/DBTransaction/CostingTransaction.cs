using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using ArtWebApp.DataModels;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
namespace ArtWebApp.DBTransaction
{
    public class CostingTransaction
    {
        String connStr = System.Configuration.ConfigurationManager.ConnectionStrings["ArtConnectionString"].ConnectionString;

        public DataTable GetCostingDetails(int atcid,int ourstyleid)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();


                SqlCommand cmd = new SqlCommand("GetDateforCosting_SP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@atcid", atcid);
                cmd.Parameters.AddWithValue("@ourstyleid", ourstyleid);
                SqlDataReader rdr = cmd.ExecuteReader();

                dt.Load(rdr);



            }
            return dt;
        }

        /// <summary>
        /// returns the List of amandatory Components
        /// </summary>
        /// <param name="atcid"></param>
        /// <param name="ourstyleid"></param>
        /// <returns></returns>
        public DataTable GetManadatoryCostingComponents()
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();


                SqlCommand cmd = new SqlCommand(@"SELECT        CostComp_PK, ComponentName, IsOptional, 0 AS CompValue, CalculationType AS CalculationMode
FROM            CostingComponentMaster
WHERE        (IsOptional = N'N') ", con);
              
                SqlDataReader rdr = cmd.ExecuteReader();

                dt.Load(rdr);



            }
            return dt;
        }



        public DataTable GetManadatoryCostingComponents(int costingid)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();


                SqlCommand cmd = new SqlCommand(@"SELECT        CostingComponentMaster.CostComp_PK, CostingComponentMaster.ComponentName, CostingComponentMaster.IsOptional, CostingComponentMaster.CalculationType AS CalculationMode, 
                         ISNULL(StyleCostingComponentDetails.CompValue, 0) AS CompValue
FROM            CostingComponentMaster LEFT OUTER JOIN
                         StyleCostingComponentDetails ON CostingComponentMaster.CostComp_PK = StyleCostingComponentDetails.CostComp_PK
WHERE    (CostingComponentMaster.IsOptional = N'N') AND    (StyleCostingComponentDetails.Costing_PK = @costingid)
", con);


                cmd.Parameters.AddWithValue("@costingid", costingid);
                SqlDataReader rdr = cmd.ExecuteReader();

                dt.Load(rdr);



            }
            return dt;
        }


        public DataTable GetOptionalCostingComponents(int costingid)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();


                SqlCommand cmd = new SqlCommand(@"SELECT        CostingComponentMaster.CostComp_PK, CostingComponentMaster.ComponentName,  ISNULL(StyleCostingComponentDetails.CompValue,0) AS CompValue, StyleCostingComponentDetails.CalculationMode, 
                         CostingComponentMaster.IsOptional
FROM            CostingComponentMaster INNER JOIN
                         StyleCostingComponentDetails ON CostingComponentMaster.CostComp_PK = StyleCostingComponentDetails.CostComp_PK
WHERE        (CostingComponentMaster.IsOptional = N'Y') AND (StyleCostingComponentDetails.Costing_PK = @costingid) ", con);
                cmd.Parameters.AddWithValue("@costingid", costingid);
                SqlDataReader rdr = cmd.ExecuteReader();

                dt.Load(rdr);



            }
            return dt;
        }








        /// <summary>
        /// returns the List of amandatory Components
        /// </summary>
        /// <param name="atcid"></param>
        /// <param name="ourstyleid"></param>
        /// <returns></returns>
        public DataTable GetOptionalCostingComponents()
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();


                SqlCommand cmd = new SqlCommand(@"SELECT        CostComp_PK, ComponentName, IsOptional, 0 AS CompValue, CalculationType AS CalculationMode
FROM            CostingComponentMaster
WHERE        (IsOptional = N'Y')  and  (IsActive = N'Y') ", con);

                SqlDataReader rdr = cmd.ExecuteReader();

                dt.Load(rdr);



            }
            return dt;
        }














# region InsertCosting
        /// <summary>
        /// insert the stylecosting master
        /// </summary>
        /// <param name="ourstyleid"></param>
        /// <param name="atcid"></param>
        /// <returns></returns>
        public int insertcostingmaster(int ourstyleid)
        {
            int stylecostingpk = 0;
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {
                var count = (from o in enty.StyleCostingMasters
                             where o.OurStyleID == ourstyleid

                             select o).Count();

                StyleCostingMaster scmstr = new StyleCostingMaster();
                scmstr.OurStyleID = ourstyleid;
                scmstr.CreatedBy = HttpContext.Current.Session["Username"].ToString();
                scmstr.CostingCount = int.Parse(count.ToString()) + 1;
                scmstr.IsApproved = "N";
                scmstr.IsAccountable = "N";
                scmstr.IsSubmitted = "N";
                scmstr.IsLast = "Y";

                scmstr.CreatedDate = DateTime.Now;
                scmstr.FOB = 0;
                scmstr.TotalCost = 0;
                scmstr.Margin = 0;
                scmstr.MarginValue = 0;
                enty.StyleCostingMasters.Add(scmstr);
                enty.SaveChanges();

                stylecostingpk = int.Parse(scmstr.Costing_PK.ToString());
                MarkOtherCostingOld(ourstyleid, stylecostingpk);
            }

            return stylecostingpk;



        }

        /// <summary>
        /// Update FOB,margin,Marginvalue,etc
        /// </summary>
        /// <param name="costingID"></param>
        public void UpdateCostingmasterData(int costingID)
        {

            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand("CalculateCostingMasterData_sp", con);
                cmd.Parameters.AddWithValue("@CostingPK", costingID);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.ExecuteNonQuery();


            }
        }





        /// <summary>
        /// Inserts the Style Components Both Optional and Mandatory
        /// </summary>
        /// <param name="costingID"></param>
        /// <param name="cmstr"></param>
        public void InsertCostingComponents(BLL.CostingBLL. StyleCostingMaster cmstr)
        {

            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {

                for (int i = 0; i < cmstr.stylecombdata.Rows.Count; i++)
                {

                    int costcompk = int.Parse(cmstr.stylecombdata.Rows[i]["CostComp_Pk"].ToString());


                    if (!enty.StyleCostingComponentDetails.Any(f => f.CostComp_PK == costcompk && f.Costing_PK == cmstr.Costing_PK))
                    {

                        StyleCostingComponentDetail det = new StyleCostingComponentDetail();
                        det.Costing_PK = cmstr.Costing_PK;
                        det.CostComp_PK = costcompk;
                        det.CompValue = Decimal.Parse(cmstr.stylecombdata.Rows[i]["CompValue"].ToString());
                        det.CalculationMode = cmstr.stylecombdata.Rows[i]["CalculationMode"].ToString().Trim();

                        enty.StyleCostingComponentDetails.Add(det);
                    }
                    else
                    {
                        var q = from stycm in enty.StyleCostingComponentDetails
                                where stycm.CostComp_PK == costcompk && stycm.Costing_PK == cmstr.Costing_PK
                                select stycm;
                        foreach (var element in q)
                        {
                            if (element.CostComp_PK!=1 && element.CostComp_PK != 2) {


                                if(element.CostComp_PK== 3)
                                {
                                    Decimal jobcontractcm = getJobContractCM(cmstr.OurStyleID);
                                    
                                        if (jobcontractcm> Decimal.Parse(cmstr.stylecombdata.Rows[i]["CompValue"].ToString()))
                                    {

                                    }
                                    else
                                    {
                                        element.CompValue = Decimal.Parse(cmstr.stylecombdata.Rows[i]["CompValue"].ToString());
                                    
                                    }
                                }
                                else
                                {
                                    element.CompValue = Decimal.Parse(cmstr.stylecombdata.Rows[i]["CompValue"].ToString());
                                }

                                
                            }
                            

                        }


                    }


                    enty.SaveChanges();
                }
            }
        }







        /// <summary>
        /// Mark All the Previous costing to Obsolute by marking is last 
        /// </summary>
        /// <param name="ourstyleid"></param>
        /// <param name="costing_pk"></param>
        public void MarkOtherCostingOld(int ourstyleid, int costing_pk)
        {
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {
                var datatounapprove = from cstingmstr in enty.StyleCostingMasters
                                      where cstingmstr.Costing_PK != costing_pk && cstingmstr.OurStyleID == ourstyleid
                                      select cstingmstr;

                foreach (var element in datatounapprove)
                {
                    element.IsLast = "N";
                }

                enty.SaveChanges();
            }
        }


        /// <summary>
        /// Insert the costing Rawmaterial Details
        /// </summary>
        /// <param name="tbl_costingdetails"></param>
        /// <param name="costingid"></param>
        public void insertstylecostingdetails(GridView tbl_costingdetails, int costingid)
        {
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {
                for (int i = 0; i < tbl_costingdetails.Rows.Count; i++)
                {
                    String chk_isreq = ((tbl_costingdetails.Rows[i].FindControl("chk_isrequired") as CheckBox).Checked == true ? "Y" : "N");
                    Label lbl_skuPK = (tbl_costingdetails.Rows[i].FindControl("lbl_sku") as Label);
                    Label lblprcperdozen = (tbl_costingdetails.Rows[i].FindControl("lbl_pcDzn") as Label);
                    Label lblprcperpc = (tbl_costingdetails.Rows[i].FindControl("lbl_pcpr") as Label);
                    TextBox consumption = (tbl_costingdetails.Rows[i].FindControl("txt_consumption") as TextBox);
                    TextBox txt_rate = (tbl_costingdetails.Rows[i].FindControl("txt_rate") as TextBox);
                    Label lbl_isgsd = (tbl_costingdetails.Rows[i].FindControl("lbl_isgsd") as Label);
                    
                    decimal pricrperdozen = Decimal.Parse(lblprcperdozen.Text, System.Globalization.NumberStyles.Float);
                    decimal priceperpc= Decimal.Parse(lblprcperpc.Text, System.Globalization.NumberStyles.Float);
                    
                    if (chk_isreq.Trim() == "Y")
                    {
                        StyleCostingDetail detail = new StyleCostingDetail();
                        detail.Costing_PK = costingid;
                        detail.Sku_PK = int.Parse(lbl_skuPK.Text);
                        detail.Consumption = decimal.Parse(consumption.Text);
                        detail.IsRequired = chk_isreq;
                        detail.PriceperDozen = pricrperdozen;
                        detail.Priceperpc = priceperpc;
                        detail.Rate = Decimal.Parse(txt_rate.Text);
                        detail.IsGD = lbl_isgsd.Text.Trim ();
                        enty.StyleCostingDetails.Add(detail);

                    }


                }
                enty.SaveChanges();
            }
            UpdateCostingmasterData(costingid);
        }


        public int getLastCM(int fromstyle)
        {
            int costingpk = 0;
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {


                var costingpkc = (from o in enty.StyleCostingMasters
                                  where o.IsLast == "Y" && o.OurStyleID == fromstyle
                                  select o.Costing_PK).Max();


                costingpk = int.Parse(costingpkc.ToString());
            }

            return costingpk;
        }

        #endregion




        public Decimal getJobContractCM(int fromstyle)
        {
            Decimal compvalue = 0;
            try
            {
                using (ArtEntitiesnew enty = new ArtEntitiesnew())
                {


                    var compvaluevar = (from o in enty.JobContractMasters
                                        where o.OurStyleID == fromstyle
                                        select o.CM).Max();


                    compvalue = Decimal.Parse(compvaluevar.ToString());
                }

            }
            catch (Exception)
            {

                compvalue = 0;
            }
            return compvalue;
        }







        /// <summary>
        /// Get the last approved costing pk of a style
        /// </summary>
        /// <param name="fromstyle"></param>
        /// <returns></returns>
        public int getLastApprovedCostingofAstyle(int fromstyle)
        {
            int costingpk = 0;
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {


                var costingpkc = (from o in enty.StyleCostingMasters
                                  where o.IsLast == "Y" && o.OurStyleID == fromstyle
                                  select o.Costing_PK).Max();


                costingpk = int.Parse(costingpkc.ToString());
            }

            return costingpk;
        }


        /// <summary>
        /// get the last costing
        /// </summary>
        /// <param name="ourstyle"></param>
        /// <returns></returns>
        public int GetLastCosting(int ourstyle)
        {
            int costingpk = 0;
            


                using (SqlConnection con = new SqlConnection(connStr))
                {
                    con.Open();

                    SqlCommand cmd= new SqlCommand (@"SELECT        MAX(Costing_PK) AS Expr1
FROM            StyleCostingMaster WHERE        (IsLast = N'Y') AND (OurStyleID = @ourstyle1)",con);

                    cmd.Parameters.AddWithValue("@ourstyle1", ourstyle);
                    try
                    {
                        costingpk = int.Parse(cmd.ExecuteScalar().ToString());
                    }
                    catch (Exception)
                    {

                        costingpk = 0;
                    }
                }

               
            

            return costingpk;
        }






        /// <summary>
        /// get the last costing
        /// </summary>
        /// <param name="ourstyle"></param>
        /// <returns></returns>
        public Decimal GetAllowedFreightCharges(int atcid)
        {
            Decimal costingpk = 0;
            Decimal alreadyUsed = 0;
            

            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand(@"SELECT      sum(  Allowedvalue) As AllowedValue
FROM            (SELECT        OurStyleID, Qty * Compvalue AS Allowedvalue, AtcId
                          FROM            (SELECT        OurStyleID, ISNULL
                             ((SELECT        SUM(PoQty) AS Expr1
                                 FROM            POPackDetails
                                 WHERE        (OurStyleID = AtcDetails.OurStyleID)), 0) AS Qty, ISNULL
                             ((SELECT        MAX(StyleCostingComponentDetails.CompValue) AS Expr1
                                 FROM            StyleCostingComponentDetails INNER JOIN
                                                          StyleCostingMaster ON StyleCostingComponentDetails.Costing_PK = StyleCostingMaster.Costing_PK
                                 WHERE        (StyleCostingMaster.IsApproved = N'A') AND (StyleCostingComponentDetails.CostComp_PK = 8) AND (StyleCostingMaster.OurStyleID = AtcDetails.OurStyleID)), 0) AS Compvalue, AtcId
FROM            AtcDetails
WHERE        (AtcId = @atcid)) AS tt) AS ttt
GROUP BY  AtcId", con);

                cmd.Parameters.AddWithValue("@atcid", atcid);
                try
                {
                    costingpk = Decimal.Parse(cmd.ExecuteScalar().ToString());
                }
                catch (Exception)
                {

                    costingpk = 0;
                }

                using (ArtEntitiesnew enty = new ArtEntitiesnew())
                {

                    var q = (from freightChargeDetail in enty.FreightChargeDetails
                            where freightChargeDetail.AtcID == atcid
                            select new { freightChargeDetail.FreightCharge }).ToList();
                    
                    foreach(var element in q)
                    {

                        try
                        {
                            if (decimal.Parse(element.FreightCharge.ToString()) > 0)
                            {
                                alreadyUsed = alreadyUsed + decimal.Parse(element.FreightCharge.ToString());
                            }

                        }
                        catch (Exception)
                        {

                           
                        }
                    }


                    

                }





                }

            costingpk = costingpk - alreadyUsed;


            return costingpk;
        }







        public DataTable  GetCostingComponentofPK(int costingpk)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"GetCostingDetailsofstyle_SP";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@costingid", costingpk);


            return QueryFunctions.ReturnQueryResultDatatableforSP(cmd);
        }


        public DataTable GetApprovedcostingComponentofStyle(int ourstyleid)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"GetApprovedCostingDetailsofOurstyle_SP";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ourstyleid", ourstyleid);



            return QueryFunctions.ReturnQueryResultDatatableforSP(cmd);
        }






























        /// <summary>
        /// Copy Rawmaterial Details from one costing to Another
        /// </summary>
        /// <param name="oldcostingpk"></param>
        /// <param name="newcostingpk"></param>
        public void CopyCostingFromOneStyletoAnother(int oldcostingpk, int newcostingpk)
        {


            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {



                var q = from oldcostingdata in enty.StyleCostingDetails
                        where oldcostingdata.Costing_PK == oldcostingpk
                        select oldcostingdata;

                foreach (var existingcostingdata in q)
                {

                    StyleCostingDetail detail = new StyleCostingDetail();
                    detail.Costing_PK = newcostingpk;
                    detail.Sku_PK = existingcostingdata.Sku_PK;
                    detail.Consumption = existingcostingdata.Consumption;
                    detail.IsRequired = existingcostingdata.IsRequired;
                    detail.PriceperDozen = existingcostingdata.PriceperDozen;
                    detail.Priceperpc = existingcostingdata.Priceperpc;
                    detail.Rate = existingcostingdata.Rate;
                    enty.StyleCostingDetails.Add(detail);


                }




                enty.SaveChanges();
            }





        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="oldcostingpk"></param>
        /// <param name="newcostingpk"></param>
        public void CopyCoMponentsOFOneCostingtoAnother(int oldcostingpk, int newcostingpk)
        {
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {


                var q = from oldcostingdata in enty.StyleCostingComponentDetails
                        where oldcostingdata.Costing_PK == oldcostingpk
                        select oldcostingdata;

                foreach (var existingcostingdata in q)
                {


                    StyleCostingComponentDetail det = new StyleCostingComponentDetail();
                    det.Costing_PK = newcostingpk;
                    det.CostComp_PK = existingcostingdata.CostComp_PK;
                    det.CompValue = existingcostingdata.CompValue;
                    det.CalculationMode = existingcostingdata.CalculationMode;

                    enty.StyleCostingComponentDetails.Add(det);
                }


                enty.SaveChanges();
            }
        }









     
        public float GetFabricValue(int costingid)
        {
            float nongdsum = 0;
            float gdsum = 0;
            try
            {
                SqlCommand cmdfornonGD = new SqlCommand();
                cmdfornonGD.CommandText = @"SELECT        ISNULL(SUM(StyleCostingDetails.PriceperDozen), 0) AS fABSUM
FROM            StyleCostingDetails INNER JOIN
                         SkuRawMaterialMaster ON StyleCostingDetails.Sku_PK = SkuRawMaterialMaster.Sku_Pk INNER JOIN
                         Template_Master ON SkuRawMaterialMaster.Template_pk = Template_Master.Template_PK
WHERE        (Template_Master.ItemGroup_PK = 1)
GROUP BY StyleCostingDetails.Costing_PK, StyleCostingDetails.IsGD
HAVING        (StyleCostingDetails.IsGD = N'N')
  and (StyleCostingDetails.Costing_PK = @costingid)";
                cmdfornonGD.Parameters.AddWithValue("@costingid", costingid);
                nongdsum = float.Parse(QueryFunctions.ReturnQueryValue(cmdfornonGD).ToString());
            }
            catch (Exception)
            {

               
            }
            try
            {
                SqlCommand cmdforGD = new SqlCommand();
                cmdforGD.CommandText = @"SELECT       sum( PriceperDozen) as PriceperDozen
FROM            (SELECT        SkuRawMaterialMaster.AtcRaw_PK, MAX(StyleCostingDetails.PriceperDozen) AS PriceperDozen
FROM            StyleCostingDetails INNER JOIN
                         SkuRawMaterialMaster ON StyleCostingDetails.Sku_PK = SkuRawMaterialMaster.Sku_Pk INNER JOIN
                         Template_Master ON SkuRawMaterialMaster.Template_pk = Template_Master.Template_PK
WHERE        (StyleCostingDetails.IsGD = N'Y') AND (StyleCostingDetails.Costing_PK = @costingid)
GROUP BY  SkuRawMaterialMaster.AtcRaw_PK, Template_Master.ItemGroup_PK
HAVING        (Template_Master.ItemGroup_PK = 1)) AS tt";
                cmdforGD.Parameters.AddWithValue("@costingid", costingid);
                gdsum = float.Parse(QueryFunctions.ReturnQueryValue(cmdforGD).ToString());
            }
            catch (Exception)
            {


            }

            return (nongdsum + gdsum);

        }


        public float GetTrimValue(int costingid)
        {
            float nongdsum = 0;
            float gdsum = 0;
            try
            {
                SqlCommand cmdfornonGD = new SqlCommand();
                cmdfornonGD.CommandText = @"SELECT        ISNULL(SUM(StyleCostingDetails.PriceperDozen), 0) AS fABSUM
FROM            StyleCostingDetails INNER JOIN
                         SkuRawMaterialMaster ON StyleCostingDetails.Sku_PK = SkuRawMaterialMaster.Sku_Pk INNER JOIN
                         Template_Master ON SkuRawMaterialMaster.Template_pk = Template_Master.Template_PK
WHERE        (Template_Master.ItemGroup_PK = 2)
GROUP BY StyleCostingDetails.Costing_PK, StyleCostingDetails.IsGD
HAVING        (StyleCostingDetails.IsGD = N'N')
  and (StyleCostingDetails.Costing_PK = @costingid)";
                cmdfornonGD.Parameters.AddWithValue("@costingid", costingid);
                nongdsum = float.Parse(QueryFunctions.ReturnQueryValue(cmdfornonGD).ToString());
            }
            catch (Exception)
            {


            }
            try
            {
                SqlCommand cmdforGD = new SqlCommand();
                cmdforGD.CommandText = @"SELECT       sum( PriceperDozen) as PriceperDozen
FROM            (SELECT         SkuRawMaterialMaster.AtcRaw_PK, MAX(StyleCostingDetails.PriceperDozen) AS PriceperDozen
FROM            StyleCostingDetails INNER JOIN
                         SkuRawMaterialMaster ON StyleCostingDetails.Sku_PK = SkuRawMaterialMaster.Sku_Pk INNER JOIN
                         Template_Master ON SkuRawMaterialMaster.Template_pk = Template_Master.Template_PK
WHERE        (StyleCostingDetails.IsGD = N'Y') AND (StyleCostingDetails.Costing_PK = @costingid)
GROUP BY  SkuRawMaterialMaster.AtcRaw_PK, Template_Master.ItemGroup_PK
HAVING        (Template_Master.ItemGroup_PK = 2)) AS tt";
                cmdforGD.Parameters.AddWithValue("@costingid", costingid);
                gdsum = float.Parse(QueryFunctions.ReturnQueryValue(cmdforGD).ToString());
            }
            catch (Exception)
            {


            }

            return (nongdsum + gdsum);

        }

        public ArrayList GetFabTrimCost(int costingid)
        {
            float fabsumnormal = 0;
            float trimsumnormal = 0;
            float fabsumGD = 0;
            float trimsumgd = 0;


            float fabsum = GetFabricValue(costingid);
            float trimsum = GetTrimValue(costingid);

            ArrayList asd = new ArrayList();
           

            asd.Add(fabsum);
            asd.Add(trimsum);
            return asd;
        }

        //        public ArrayList GetFabTrimCost(int costingid)
        //        {
        //            float fabsumnormal = 0;
        //            float trimsumnormal = 0;
        //            float fabsumGD = 0;
        //            float trimsumgd = 0;


        //            float fabsum=0;
        //            float trimsum=0;

        //            ArrayList asd= new ArrayList ();
        //            DataTable dt = new DataTable();
        //             DataTable dt2 = new DataTable();
        //            using (SqlConnection con = new SqlConnection(connStr))
        //            {
        //                con.Open();


        //                SqlCommand cmdfabbric = new SqlCommand(@"SELECT       isnull(   SUM(StyleCostingDetails.PriceperDozen),0) AS fABSUM
        //FROM            StyleCostingDetails INNER JOIN
        //                         SkuRawMaterialMaster ON StyleCostingDetails.Sku_PK = SkuRawMaterialMaster.Sku_Pk INNER JOIN
        //                         Template_Master ON SkuRawMaterialMaster.Template_pk = Template_Master.Template_PK
        //WHERE        (Template_Master.ItemGroup_PK = 1) GROUP BY StyleCostingDetails.Costing_PK
        //HAVING        (StyleCostingDetails.Costing_PK = @costingid)", con);

        //                cmdfabbric.Parameters.AddWithValue("@costingid", costingid);

        //                SqlDataReader rdr = cmdfabbric.ExecuteReader();

        //                dt.Load(rdr);


        //                if(dt!=null)
        //                {
        //                  if( dt.Rows.Count >0)
        //                  {
        //                      fabsum= float.Parse (dt .Rows [0]["fABSUM"].ToString ());
        //                  }
        //                }














        //                SqlCommand cmdtrim = new SqlCommand(@"SELECT       isnull(   SUM(StyleCostingDetails.PriceperDozen),0)AS fABSUM
        //FROM            StyleCostingDetails INNER JOIN
        //                         SkuRawMaterialMaster ON StyleCostingDetails.Sku_PK = SkuRawMaterialMaster.Sku_Pk INNER JOIN
        //                         Template_Master ON SkuRawMaterialMaster.Template_pk = Template_Master.Template_PK
        //WHERE        (Template_Master.ItemGroup_PK = 2)GROUP BY StyleCostingDetails.Costing_PK
        //HAVING        (StyleCostingDetails.Costing_PK = @costingid)", con);

        //                cmdtrim.Parameters.AddWithValue("@costingid", costingid);

        //                SqlDataReader rdr1 = cmdtrim.ExecuteReader();

        //                dt2.Load(rdr1);


        //                if(dt!=null)
        //                {
        //                  if( dt2.Rows.Count >0)
        //                  {
        //                      trimsum= float.Parse (dt2 .Rows [0]["fABSUM"].ToString ());
        //                  }
        //                }






        //            }

        //            asd.Add(fabsum);
        //            asd.Add(trimsum);
        //            return asd;
        //        }











        public DataTable GetAllAprovedCostingOfaStyle(int ourstyleid)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();




                SqlCommand cmd = new SqlCommand(@"select  Costing_PK, isnull(TotalCost , 0) as TotalCost , isnull(MarginValue, 0) as MarginValue ,isnull( FOB, 0) as FOB , isnull( Margin,0) as Margin ,CreatedDate  FROM StyleCostingMaster
WHERE Costing_PK IN
(
select TOP 1 MIN(Costing_PK) as id FROM            StyleCostingMaster
WHERE        (OurStyleID = @ourstyleid) AND (IsAccountable = N'A')
UNION ALL
select TOP 2 Costing_PK as id FROM             StyleCostingMaster
WHERE        (OurStyleID = @ourstyleid) AND (IsAccountable = N'A')
ORDER BY Costing_PK desc
)ORDER BY Costing_PK ASC ", con);



                cmd.Parameters.AddWithValue("@ourstyleid", ourstyleid);
                SqlDataReader rdr = cmd.ExecuteReader();

                dt.Load(rdr);



            }
            return dt;
        }



        public DataTable GetCostingmaster(int costingid)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();




                SqlCommand cmd = new SqlCommand(@"select  Costing_PK, isnull(TotalCost , 0) as TotalCost , isnull(MarginValue, 0) as MarginValue ,isnull( FOB, 0) as FOB , isnull( Margin,0) as Margin ,CreatedDate ,ApprovedBy, ApprovedDate  FROM StyleCostingMaster
WHERE Costing_PK =@costingid", con);



                cmd.Parameters.AddWithValue("@costingid", costingid);
                SqlDataReader rdr = cmd.ExecuteReader();

                dt.Load(rdr);



            }
            return dt;
        }


         public DataTable GetATCDetailsofCosting(int costingPK)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();




                SqlCommand cmd = new SqlCommand(@" SELECT        AtcMaster.AtcNum, AtcDetails.OurStyle, AtcDetails.BuyerStyle, AtcDetails.FOB,(SELECT      sum(  PoQty)
FROM            POPackDetails
WHERE        (OurStyleID =AtcDetails.OurStyleID)) as Qty
FROM            AtcMaster INNER JOIN
                         AtcDetails ON AtcMaster.AtcId = AtcDetails.AtcId INNER JOIN
                         StyleCostingMaster ON AtcDetails.OurStyleID = StyleCostingMaster.OurStyleID
WHERE        (StyleCostingMaster.Costing_PK = @Costing_PK) ", con);



                cmd.Parameters.AddWithValue("@Costing_PK", costingPK);
                SqlDataReader rdr = cmd.ExecuteReader();

                dt.Load(rdr);



            }
            return dt;
        }


         public DataTable GetFabricTrimsDetailsofATC(int costingPK)
         {
             DataTable dt = new DataTable();

             using (SqlConnection con = new SqlConnection(connStr))
             {
                 con.Open();




                 SqlCommand cmd = new SqlCommand(@" SELECT        SkuRawMaterialMaster.RMNum, SkuRawMaterialMaster.Composition + '-' + SkuRawMaterialMaster.Construction + '-' + isnull( SkuRawMaterialMaster.Width,'') + '-' +  isnull( SkuRawMaterialMaster.Weight,'') AS ItemDescription, UOMMaster.UomCode as UOM, StyleCostingDetails.Rate, StyleCostingDetails.Consumption, 
                         StyleCostingDetails.Priceperpc, StyleCostingDetails.PriceperDozen, ItemGroupMaster.ItemGroupName
FROM            ItemGroupMaster INNER JOIN
                         Template_Master ON ItemGroupMaster.ItemGroupID = Template_Master.ItemGroup_PK INNER JOIN
                         StyleCostingDetails INNER JOIN
                         SkuRawMaterialMaster ON StyleCostingDetails.Sku_PK = SkuRawMaterialMaster.Sku_Pk INNER JOIN
                         UOMMaster ON SkuRawMaterialMaster.Uom_PK = UOMMaster.Uom_PK ON Template_Master.Template_PK = SkuRawMaterialMaster.Template_pk
WHERE        (StyleCostingDetails.Costing_PK =  @Costing_PK) ", con);



                 cmd.Parameters.AddWithValue("@Costing_PK", costingPK);
                 SqlDataReader rdr = cmd.ExecuteReader();

                 dt.Load(rdr);



             }
             return dt;
         }


         public float getCM(int ourstyleid)
         {
             float cmvalue = 0;

             DataTable dt = new DataTable();

             using (SqlConnection con = new SqlConnection(connStr))
             {
                 con.Open();




                 SqlCommand cmd = new SqlCommand(@"SELECT            TOP (1) isnull( StyleCostingComponentDetails.CompValue,0)
FROM            StyleCostingComponentDetails INNER JOIN
                         CostingComponentMaster ON StyleCostingComponentDetails.CostComp_PK = CostingComponentMaster.CostComp_PK INNER JOIN
                         StyleCostingMaster ON StyleCostingComponentDetails.Costing_PK = StyleCostingMaster.Costing_PK
WHERE        (StyleCostingMaster.OurStyleID = @ourstyleid) AND (CostingComponentMaster.ComponentName = N'CM')
ORDER BY StyleCostingComponentDetails.Costing_PK DESC", con);



                 cmd.Parameters.AddWithValue("@ourstyleid", ourstyleid);
                 SqlDataReader rdr = cmd.ExecuteReader();

                 dt.Load(rdr);

                 if (dt != null)
                 {
                     if (dt.Rows.Count > 0)
                     {
                         cmvalue = float.Parse(dt.Rows[0][0].ToString());
                     }
                 }

             }
              return cmvalue;
         }


        # region Approval


        public void ApproveCosting(int costingPK)
        {

            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {

                // gOTTHE OURSTYLE ID
                 var Ourstyledata = (from o in enty.StyleCostingMasters where o.Costing_PK==costingPK

                             select o.OurStyleID).FirstOrDefault();
                int ourstyleid=int.Parse (Ourstyledata.ToString ());

                var datatounapprove = from cstingmstr in enty.StyleCostingMasters
                        where cstingmstr.Costing_PK != costingPK && cstingmstr.OurStyleID == ourstyleid
                        select cstingmstr;
                //mARKED ALL UNAPPROVED
                foreach(var element in datatounapprove)
                {
                    element.IsApproved = "N";
                    element.IsApplicable = "N";
                }
                      
                 var datatoapprove = from cstingmstr in enty.StyleCostingMasters
                        where cstingmstr.Costing_PK == costingPK && cstingmstr.OurStyleID == ourstyleid
                        select cstingmstr;

                foreach(var element123 in datatoapprove)
                {
                    element123.IsApproved = "A";
                    element123.IsApplicable="A";
                    element123.IsAccountable = "A";
                    element123.ApprovedBy = HttpContext.Current.Session["Username"].ToString();
                    element123.ApprovedDate = DateTime.Now;
                }

                enty.SaveChanges();
            }


        }




        public void UnapproveApproveCosting(int ourstylid)
        {
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {

                var datatounapprove = from cstingmstr in enty.StyleCostingMasters
                                      where cstingmstr.OurStyleID== ourstylid
                                      select cstingmstr;
                //mARKED ALL UNAPPROVED
                foreach (var element in datatounapprove)
                {
                    element.IsApproved = "N";
                    element.IsApplicable = "N";
                }
            }
        }

        public void ForwardCosting(int costingPK)
        {

            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {

                // gOTTHE OURSTYLE ID
       

                var datatoapprove = from cstingmstr in enty.StyleCostingMasters
                                    where cstingmstr.Costing_PK == costingPK
                                    select cstingmstr;

                foreach (var element123 in datatoapprove)
                {
                    element123.IsFowarded = "Y";
                    element123.ForwardedBy = HttpContext.Current.Session["Username"].ToString();
                }

                enty.SaveChanges();
            }


        }



     
                   

        #endregion

    }
}
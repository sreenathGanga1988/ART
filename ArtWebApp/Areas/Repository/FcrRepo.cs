using ArtWebApp.Areas.CuttingMVC.ViewModel;
using ArtWebApp.DataModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ArtWebApp.Areas.Repository
{
    public class FcrRepo
    {

        #region fcr
        public DataTable GetAtcTemplateData(int skudet_pk)
        {
            DataTable dt = new DataTable();




            SqlCommand cmd = new SqlCommand(@"GetLaysheetFCR_SP");


            cmd.Parameters.AddWithValue("@skudet_pk", skudet_pk);

            cmd.CommandType = CommandType.StoredProcedure;


            return QueryFunctions.ReturnQueryResultDatatableforSP(cmd); ;
        }

        public DataTable GetLayShortageData(int skudet_pk)
        {
            DataTable dt = new DataTable();




            SqlCommand cmd = new SqlCommand(@"  SELECT LayAdjustDetails.LayCutAdjustID, LayAdjustDetails.Qty, CutOrderMaster.Cut_NO, LayShortageReqMaster.LayShortageReqCode, CutOrderMaster.OurStyleID, CutOrderMaster.ToLoc
FROM            LayAdjustDetails INNER JOIN
                         CutOrderMaster ON LayAdjustDetails.CutID = CutOrderMaster.CutID INNER JOIN
                         LayShortageReqMaster ON LayAdjustDetails.LayShortageMasterID = LayShortageReqMaster.LayShortageMasterID
WHERE        (CutOrderMaster.SkuDet_pk = @skudet_pk) ");


            cmd.Parameters.AddWithValue("@skudet_pk", skudet_pk);






            return QueryFunctions.ReturnQueryResultDatatable(cmd); ;
        }

        public DataTable GetRejectionData(int skudet_pk)
        {
            DataTable dt = new DataTable();




            SqlCommand cmd = new SqlCommand(@"  SELECT RejectionAdjustDetails.Qty, RejectReqMaster.Reqnum, RejectReqMaster.RejectionType, CutOrderMaster.Cut_NO, CutOrderMaster.SkuDet_pk,CutOrderMaster.OurStyleID, CutOrderMaster.ToLoc
FROM            RejectionAdjustDetails INNER JOIN
                         RejectReqMaster ON RejectionAdjustDetails.RejReqMasterIDID = RejectReqMaster.RejReqMasterID INNER JOIN
                         CutOrderMaster ON RejectionAdjustDetails.CutID = CutOrderMaster.CutID
WHERE         (CutOrderMaster.SkuDet_pk = @skudet_pk) ");


            cmd.Parameters.AddWithValue("@skudet_pk", skudet_pk);




            return QueryFunctions.ReturnQueryResultDatatable(cmd); ;
        }


        public DataTable GetDeliveryData(int skudet_pk)
        {
            DataTable dt = new DataTable();




            SqlCommand cmd = new SqlCommand(@"DosOfSku_SP");


            cmd.Parameters.AddWithValue("@skudet_pk", skudet_pk);

            cmd.CommandType = CommandType.StoredProcedure;


            return QueryFunctions.ReturnQueryResultDatatableforSP(cmd); ;
        }

        public DataTable GetFabriclocationGroup(int Atcid)
        {
            DataTable dt = new DataTable();
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = @"CreateLocationfabricDetails_SP";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@AtcId", Atcid);


                dt = QueryFunctions.ReturnQueryResultDatatableforSP(cmd);

                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {




                        DataView view = new DataView(dt);
                        DataTable FablocCombo = view.ToTable(true, "AtcId", "SkuDet_PK", "Location_pk", "ColorName", "ItemColor", "ColorCode", "LocationName", "ITemdEscription", "Description");







                        using (ArtWebApp.DataModels.ArtEntitiesnew enty = new DataModels.ArtEntitiesnew())
                        {



                            foreach (DataRow row in FablocCombo.Rows)
                            {
                                int skudetpk = int.Parse(row["SkuDet_PK"].ToString());
                                int locationpk = int.Parse(row["Location_pk"].ToString());

                                if (!enty.FabricInLocationAtcMaster_tbl.Any(f => f.SkuDet_PK == skudetpk && f.Location_pk == locationpk))
                                {
                                    FabricInLocationAtcMaster_tbl fabricInLocation_Tbl = new FabricInLocationAtcMaster_tbl();

                                    fabricInLocation_Tbl.AtcId = int.Parse(row["AtcId"].ToString());

                                    fabricInLocation_Tbl.Location_pk = int.Parse(row["Location_pk"].ToString());
                                    fabricInLocation_Tbl.SkuDet_PK = int.Parse(row["SkuDet_PK"].ToString());
                                    fabricInLocation_Tbl.ColorName = row["ColorName"].ToString();
                                    fabricInLocation_Tbl.ColorCode = row["ColorCode"].ToString();
                                    fabricInLocation_Tbl.ItemColor = row["ItemColor"].ToString();
                                    fabricInLocation_Tbl.LocationName = row["LocationName"].ToString();
                                    fabricInLocation_Tbl.ITemdEscription = row["ITemdEscription"].ToString();
                                    fabricInLocation_Tbl.Description = row["Description"].ToString();
                                    fabricInLocation_Tbl.IsClosed = "N";
                                    fabricInLocation_Tbl.Status = "Open";
                                    enty.FabricInLocationAtcMaster_tbl.Add(fabricInLocation_Tbl);
                                }

                            }





                            foreach (DataRow row in dt.Rows)
                            {

                                int skudetpk = int.Parse(row["SkuDet_PK"].ToString());
                                int locationpk = int.Parse(row["Location_pk"].ToString());
                                int ourstyleid = int.Parse(row["OurStyleID"].ToString());

                                if (!enty.FabricInLocation_tbl.Any(f => f.SkuDet_PK == skudetpk && f.OurStyleId == ourstyleid && f.Location_pk == locationpk))
                                {


                                    FabricInLocation_tbl fabricInLocation_Tbl = new FabricInLocation_tbl();

                                    fabricInLocation_Tbl.AtcId = int.Parse(row["AtcId"].ToString());
                                    fabricInLocation_Tbl.OurStyleId = int.Parse(row["OurStyleID"].ToString());
                                    fabricInLocation_Tbl.Location_pk = int.Parse(row["Location_pk"].ToString());
                                    fabricInLocation_Tbl.SkuDet_PK = int.Parse(row["SkuDet_PK"].ToString());
                                    fabricInLocation_Tbl.ColorName = row["ColorName"].ToString();
                                    fabricInLocation_Tbl.ColorCode = row["ColorCode"].ToString();
                                    fabricInLocation_Tbl.ItemColor = row["ItemColor"].ToString();
                                    fabricInLocation_Tbl.LocationName = row["LocationName"].ToString();
                                    fabricInLocation_Tbl.ITemdEscription = row["ITemdEscription"].ToString();
                                    fabricInLocation_Tbl.Description = row["Description"].ToString();
                                    fabricInLocation_Tbl.IsClosed = "N";
                                    fabricInLocation_Tbl.Status = "Open";

                                    enty.FabricInLocation_tbl.Add(fabricInLocation_Tbl);


                                }

                                enty.SaveChanges();

                            }
















                        }
                    }




                }

            }

            using (SqlCommand cmd = new SqlCommand())
            {

                cmd.CommandText = @"FabricoflocationOfAtc_SP";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@AtcId", Atcid);
                dt = QueryFunctions.ReturnQueryResultDatatableforSP(cmd);
            }
            return dt;










        }

        internal DataTable Getdatewisefabricconsume(object fromdate, object todate, object locid, object atcid)
        {
            throw new NotImplementedException();
        }

        public DataTable GetSampleAndExtraCutorder(int skudet_pk, int ourStyleid, int locationpk)
        {
            DataTable dt = new DataTable();




            SqlCommand cmd = new SqlCommand(@"   SELECT        CutOrderMaster.CutID, CutOrderMaster.Cut_NO, SUM(CutOrderDO.DeliveryQty) AS FabQty, CutOrderMaster.CutOrderType, CutOrderMaster.SkuDet_pk
FROM            CutOrderMaster INNER JOIN
                         CutOrderDO ON CutOrderMaster.CutID = CutOrderDO.CutID
WHERE        (CutOrderMaster.CutOrderType <> N'Normal') AND (CutOrderMaster.SkuDet_pk = @skudet_pk) 
and  (CutOrderMaster.OurStyleID = @OurStyleID) AND (CutOrderMaster.ToLoc = @ToLoc)
GROUP BY CutOrderMaster.CutID, CutOrderMaster.Cut_NO, CutOrderMaster.CutOrderType, CutOrderMaster.OurStyleID, CutOrderMaster.ToLoc, CutOrderMaster.SkuDet_pk ");


            cmd.Parameters.AddWithValue("@skudet_pk", skudet_pk);

            cmd.Parameters.AddWithValue("@OurStyleID", ourStyleid);
            cmd.Parameters.AddWithValue("@ToLoc", locationpk);





            return QueryFunctions.ReturnQueryResultDatatable(cmd); ;
        }


        public DataTable GetFabricoflocationByAtc(int AtcId)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand(@"  SELECT        FabricInLocationAtcMasterId, LocationName, Description, ITemdEscription, SkuDet_PK, ColorName, ColorCode, ItemColor, Status, IsClosed, ClosedBy, ClosedDate
FROM            FabricInLocationAtcMaster_tbl
WHERE        (AtcId = @AtcId)");


            cmd.Parameters.AddWithValue("@AtcId", AtcId); return QueryFunctions.ReturnQueryResultDatatable(cmd); ;
        }


        #endregion

        #region FCR Summary



        public FCRSummary GetFCRSummary(int atcid, int locationID)
        {
            FCRSummary fCRSummary = new FCRSummary();


            fCRSummary = CreateFCRSummarytable(atcid, locationID, fCRSummary);
            return fCRSummary;
        }


        public FCRSummary CreateFCRSummarytable(int atcid, int locationID, FCRSummary fCRSummary)
        {
            String Atcnum = "";
            String factory = "";
            int Atcid = 0;
            Decimal TotalActualConsumption = 0;
            decimal TotalMarkerConsumption = 0;
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("locationID");
            dataTable.Columns.Add("AtcID");
            dataTable.Columns.Add("AtcNum");
            dataTable.Columns.Add("Details");
            dataTable.Columns.Add("SkudDetPk");

            List<FabricInLocation_tbl> fabriclist = new List<FabricInLocation_tbl>();
            List<CutPlanMaster> cutplanlist = new List<CutPlanMaster>();

            List<AtcDetail> ourstylelist = new List<AtcDetail>();

            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {
                //var atcnum =enty.AtcMasters.Where(u => u.AtcId == atcid).Select(u=>u.AtcNum).FirstOrDefault();

                ourstylelist = enty.AtcDetails.Where(u => u.AtcId == atcid).ToList();
                foreach (var element in ourstylelist)
                {
                    dataTable.Columns.Add(element.OurStyle);
                    Atcnum = element.AtcMaster.AtcNum;
                    Atcid = int.Parse(element.AtcId.ToString());
                }
            }

            dataTable.Columns.Add("Summary");
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {
                //var fabmar = enty.CutPlanMasters.Where(u => u.SkuDet_PK == 1 && u.Location_PK == 2).Select(u => u.CutPlanFabReq).Sum();
                cutplanlist = enty.CutPlanMasters.Where(u => u.Location_PK == locationID).ToList();


                var q = enty.FabricInLocationAtcMaster_tbl.Where(u => u.AtcId == atcid && u.Location_pk == locationID).ToList();
                fabriclist = enty.FabricInLocation_tbl.Where(u => u.AtcId == atcid).ToList();
                foreach (var element in q)
                {
                    factory = element.LocationName;

                    DataRow dataRow = dataTable.NewRow();
                    dataRow["AtcID"] = Atcid;
                    dataRow["AtcNum"] = Atcnum;
                    dataRow["locationID"] = locationID;
                    dataRow["Details"] = "Color";
                    dataRow["SkudDetPk"] = element.SkuDet_PK;
                    dataTable.Rows.Add(dataRow);

                    DataRow dataRow1 = dataTable.NewRow();
                    dataRow1["AtcID"] = Atcid;
                    dataRow1["AtcNum"] = Atcnum;
                    dataRow1["Details"] = "Order Qty(Asq)";
                    dataRow1["locationID"] = locationID;
                    dataRow1["SkudDetPk"] = element.SkuDet_PK;
                    dataTable.Rows.Add(dataRow1);

                    DataRow dataRow2 = dataTable.NewRow();
                    dataRow2["AtcID"] = Atcid;
                    dataRow2["AtcNum"] = Atcnum;
                    dataRow2["Details"] = "CutQty";
                    dataRow2["locationID"] = locationID;
                    dataRow2["SkudDetPk"] = element.SkuDet_PK;
                    dataTable.Rows.Add(dataRow2);

                    DataRow dataRow8 = dataTable.NewRow();
                    dataRow8["AtcID"] = Atcid;
                    dataRow8["AtcNum"] = Atcnum;
                    dataRow8["Details"] = "Total Marker Fabric Req";
                    dataRow8["locationID"] = locationID;
                    dataRow8["SkudDetPk"] = element.SkuDet_PK;
                    dataTable.Rows.Add(dataRow8);

                    DataRow dataRow3 = dataTable.NewRow();
                    dataRow3["AtcID"] = Atcid;
                    dataRow3["AtcNum"] = Atcnum;
                    dataRow3["Details"] = "Total Fabric Consumed";
                    dataRow3["locationID"] = locationID;
                    dataRow3["SkudDetPk"] = element.SkuDet_PK;
                    dataTable.Rows.Add(dataRow3);


                    DataRow dataRow4 = dataTable.NewRow();
                    dataRow4["AtcID"] = Atcid;
                    dataRow4["AtcNum"] = Atcnum;
                    dataRow4["Details"] = "Bom Consumption";
                    dataRow4["locationID"] = locationID;
                    dataRow4["SkudDetPk"] = element.SkuDet_PK;
                    dataTable.Rows.Add(dataRow4);

                    DataRow dataRow5 = dataTable.NewRow();
                    dataRow5["AtcID"] = Atcid;
                    dataRow5["AtcNum"] = Atcnum;
                    dataRow5["Details"] = "Marker Consumption";
                    dataRow5["locationID"] = locationID;
                    dataRow5["SkudDetPk"] = element.SkuDet_PK;
                    dataTable.Rows.Add(dataRow5);

                    DataRow dataRow6 = dataTable.NewRow();
                    dataRow6["AtcID"] = Atcid;
                    dataRow6["AtcNum"] = Atcnum;
                    dataRow6["Details"] = "Actual FCr Consumption";
                    dataRow6["locationID"] = locationID;
                    dataRow6["SkudDetPk"] = element.SkuDet_PK;
                    dataTable.Rows.Add(dataRow6);


                    DataRow dataRow7 = dataTable.NewRow();
                    dataRow7["AtcID"] = Atcid;
                    dataRow7["AtcNum"] = Atcnum;
                    dataRow7["Details"] = "Over Consumed (%)";
                    dataRow7["locationID"] = locationID;
                    dataRow7["SkudDetPk"] = element.SkuDet_PK;
                    dataTable.Rows.Add(dataRow7);




                }


            }






            foreach (DataRow row in dataTable.Rows)
            {

                int skutempid = int.Parse(row["SkudDetPk"].ToString());

                int locationtempID = int.Parse(row["locationID"].ToString());

                String Detail = row["Details"].ToString().Trim();
                foreach (DataColumn col in dataTable.Columns)
                {
                    if (col.ColumnName != "AtcID" && col.ColumnName != "locationID" && col.ColumnName != "SkudDetPk" &&
                        col.ColumnName != "AtcNum" && col.ColumnName != "Details" && col.ColumnName != "Summary")
                    {

                        string ourstyle = col.ColumnName;
                        int ourstyleid = int.Parse(ourstylelist.Where(u => u.OurStyle == ourstyle).Select(U => U.OurStyleID).FirstOrDefault().ToString());
                        try
                        {


                            var listelement = fabriclist.Where(u => u.SkuDet_PK == skutempid && u.Location_pk == locationtempID && u.OurStyleId == ourstyleid).FirstOrDefault();
                            var cutplanfab = cutplanlist.Where(u => u.OurStyleID == ourstyleid && u.SkuDet_PK == skutempid && u.Location_PK == locationtempID).Select(u => u.CutPlanFabReq).Sum();
                            if (Detail == "Color")
                            {

                                row[ourstyle] = listelement.ColorName;
                            }
                            else if (Detail == "Order Qty(Asq)") { row[ourstyle] = listelement.OrderQty; }
                            else if (Detail == "CutQty") { row[ourstyle] = listelement.CutQty; }
                            else if (Detail == "Total Fabric Consumed") { row[ourstyle] = listelement.TotalFabConsumed; }
                            else if (Detail == "Bom Consumption") { row[ourstyle] = listelement.BomConsumption; }
                            else if (Detail == "Marker Consumption") { row[ourstyle] = listelement.MarkerConsumption; }
                            else if (Detail == "Actual FCr Consumption") { row[ourstyle] = listelement.ActualConsumption; }
                            else if (Detail == "Over Consumed (%)") { row[ourstyle] = listelement.OverConsumedPer; }
                            else if (Detail == "Total Marker Fabric Req") { row[ourstyle] = cutplanfab; }




                        }
                        catch (Exception)
                        {


                        }





                    }


                }

                decimal summary = 0;
                foreach (DataColumn col in dataTable.Columns)
                {
                    if (col.ColumnName != "AtcID" && col.ColumnName != "locationID" && col.ColumnName != "SkudDetPk" &&
                        col.ColumnName != "AtcNum" && col.ColumnName != "Details" && col.ColumnName != "Summary")
                    {



                        try
                        {
                            summary += Decimal.Parse(row[col.ColumnName].ToString());
                        }
                        catch (Exception)
                        {


                        }



                    }


                }

                row["Summary"] = summary;


            }


            int actualrowcount = dataTable.Rows.Count;

            DataRow Summary = dataTable.NewRow();
            Summary["AtcID"] = Atcid;
            Summary["AtcNum"] = Atcnum;
            Summary["Details"] = "Summary ";
            Summary["locationID"] = locationID;
            Summary["SkudDetPk"] = 0;
            dataTable.Rows.Add(Summary);

            DataRow summary0 = dataTable.NewRow();
            summary0["AtcID"] = Atcid;
            summary0["AtcNum"] = Atcnum;
            summary0["Details"] = "OverAll Order Qty";
            summary0["locationID"] = locationID;
            summary0["SkudDetPk"] = 0;
            dataTable.Rows.Add(summary0);

            DataRow summary1 = dataTable.NewRow();
            summary1["AtcID"] = Atcid;
            summary1["AtcNum"] = Atcnum;
            summary1["Details"] = "OverAll Cut Qty ";
            summary1["locationID"] = locationID;
            summary1["SkudDetPk"] = 0;
            dataTable.Rows.Add(summary1);

            DataRow summary8 = dataTable.NewRow();
            summary8["AtcID"] = Atcid;
            summary8["AtcNum"] = Atcnum;
            summary8["Details"] = "OverAll Total Marker Fabric";
            summary8["locationID"] = locationID;
            summary8["SkudDetPk"] = 0;
            dataTable.Rows.Add(summary8);

            DataRow summary2 = dataTable.NewRow();
            summary2["AtcID"] = Atcid;
            summary2["AtcNum"] = Atcnum;
            summary2["Details"] = "OverAll Fabric Consumed";
            summary2["locationID"] = locationID;
            summary2["SkudDetPk"] = 0;
            dataTable.Rows.Add(summary2);

            DataRow summary3 = dataTable.NewRow();
            summary3["AtcID"] = Atcid;
            summary3["AtcNum"] = Atcnum;
            summary3["Details"] = "OverAll BOM Consumption";
            summary3["locationID"] = locationID;
            summary3["SkudDetPk"] = 0;
            dataTable.Rows.Add(summary3);

            DataRow summary4 = dataTable.NewRow();
            summary4["AtcID"] = Atcid;
            summary4["AtcNum"] = Atcnum;
            summary4["Details"] = "OverAll Marker Consumption";
            summary4["locationID"] = locationID;
            summary4["SkudDetPk"] = 0;
            dataTable.Rows.Add(summary4);



            DataRow summary6 = dataTable.NewRow();
            summary6["AtcID"] = Atcid;
            summary6["AtcNum"] = Atcnum;
            summary6["Details"] = "OverAll FCR Consumption";
            summary6["locationID"] = locationID;
            summary6["SkudDetPk"] = 0;
            dataTable.Rows.Add(summary6);



            DataRow summary7 = dataTable.NewRow();
            summary7["AtcID"] = Atcid;
            summary7["AtcNum"] = Atcnum;
            summary7["Details"] = "OverAll over Consumed";
            summary7["locationID"] = locationID;
            summary7["SkudDetPk"] = 0;
            dataTable.Rows.Add(summary7);



            for (int i = actualrowcount + 1; i < dataTable.Rows.Count; i++)
            {
                foreach (DataColumn col in dataTable.Columns)
                {
                    if (col.ColumnName != "AtcID" && col.ColumnName != "locationID" && col.ColumnName != "SkudDetPk" &&
                        col.ColumnName != "AtcNum" && col.ColumnName != "Details" && col.ColumnName != "Summary")
                    {


                        string ourstyle = col.ColumnName;
                        int ourstyleid = int.Parse(ourstylelist.Where(u => u.OurStyle == ourstyle).Select(U => U.OurStyleID).FirstOrDefault().ToString());

                        var listelement = fabriclist.Where(u => u.Location_pk == locationID && u.OurStyleId == ourstyleid).ToList();



                        if (dataTable.Rows[i]["Details"].ToString().Trim() == "OverAll Cut Qty")
                        {

                            try
                            {
                                var cutqty = listelement.Select(u => u.CutQty).Sum();

                                dataTable.Rows[i][col] = cutqty.ToString();
                            }
                            catch (Exception)
                            {

                                dataTable.Rows[i][col] = 0;
                            }

                        }
                        else if (dataTable.Rows[i]["Details"].ToString().Trim() == "OverAll Order Qty")
                        {
                            try
                            {
                                var TotalFabConsumed = listelement.Select(u => u.OrderQty).Sum();

                                dataTable.Rows[i][col] = TotalFabConsumed.ToString();
                            }
                            catch (Exception)
                            {

                                dataTable.Rows[i][col] = 0;
                            }
                        }
                        else if (dataTable.Rows[i]["Details"].ToString().Trim() == "OverAll Fabric Consumed")
                        {
                            try
                            {
                                var TotalFabConsumed = listelement.Select(u => u.TotalFabConsumed).Sum();

                                dataTable.Rows[i][col] = TotalFabConsumed.ToString();
                            }
                            catch (Exception)
                            {

                                dataTable.Rows[i][col] = 0;
                            }
                        }
                        else if (dataTable.Rows[i]["Details"].ToString().Trim() == "OverAll BOM Consumption")
                        {
                            try
                            {
                                var BomConsumption = listelement.Select(u => u.BomConsumption).Average();

                                dataTable.Rows[i][col] = BomConsumption.ToString();
                            }
                            catch (Exception)
                            {

                                dataTable.Rows[i][col] = 0;
                            }
                        }
                        else if (dataTable.Rows[i]["Details"].ToString().Trim() == "OverAll Marker Consumption")
                        {
                            try
                            {
                                var MarkerConsumption = listelement.Select(u => u.MarkerConsumption).Average();

                                dataTable.Rows[i][col] = MarkerConsumption.ToString();
                            }
                            catch (Exception)
                            {

                                dataTable.Rows[i][col] = 0;
                            }

                        }
                        else if (dataTable.Rows[i]["Details"].ToString().Trim() == "OverAll FCR Consumption")
                        {
                            try
                            {
                                var ActualConsumption = listelement.Select(u => u.ActualConsumption).Average();

                                dataTable.Rows[i][col] = ActualConsumption.ToString();
                            }
                            catch (Exception)
                            {

                                dataTable.Rows[i][col] = 0;
                            }
                        }
                        else if (dataTable.Rows[i]["Details"].ToString().Trim() == "OverAll over Consumed")
                        {
                            try
                            {
                                var OverConsumed = listelement.Select(u => u.OverConsumed).Average();

                                dataTable.Rows[i][col] = OverConsumed.ToString();
                            }
                            catch (Exception)
                            {

                                dataTable.Rows[i][col] = 0;
                            }

                        }
                        else if (dataTable.Rows[i]["Details"].ToString().Trim() == "OverAll Total Marker Fabric")
                        {
                            try
                            {
                                var cutplanfab = cutplanlist.Where(u => u.OurStyleID == ourstyleid && u.Location_PK == locationID).Select(u => u.CutPlanFabReq).Sum();

                                dataTable.Rows[i][col] = cutplanfab.ToString();
                            }
                            catch (Exception)
                            {

                                dataTable.Rows[i][col] = 0;
                            }

                        }
                    }

                    if (col.ColumnName == "Summary")
                    {
                        var listelement = fabriclist.Where(u => u.Location_pk == locationID).ToList();


                        if (dataTable.Rows[i]["Details"].ToString().Trim() == "OverAll Cut Qty")
                        {

                            try
                            {
                                var cutqty = listelement.Select(u => u.CutQty).Sum();

                                dataTable.Rows[i][col] = cutqty.ToString();
                            }
                            catch (Exception)
                            {

                                dataTable.Rows[i][col] = 0;
                            }

                        }
                        else if (dataTable.Rows[i]["Details"].ToString().Trim() == "OverAll Order Qty")
                        {
                            try
                            {
                                var TotalFabConsumed = listelement.Select(u => u.OrderQty).Sum();

                                dataTable.Rows[i][col] = TotalFabConsumed.ToString();
                            }
                            catch (Exception)
                            {

                                dataTable.Rows[i][col] = 0;
                            }
                        }
                        else if (dataTable.Rows[i]["Details"].ToString().Trim() == "OverAll Fabric Consumed")
                        {
                            try
                            {
                                var TotalFabConsumed = listelement.Select(u => u.TotalFabConsumed).Sum();

                                dataTable.Rows[i][col] = TotalFabConsumed.ToString();
                            }
                            catch (Exception)
                            {

                                dataTable.Rows[i][col] = 0;
                            }
                        }
                        else if (dataTable.Rows[i]["Details"].ToString().Trim() == "OverAll BOM Consumption")
                        {
                            try
                            {
                                var BomConsumption = listelement.Select(u => u.BomConsumption).Average();

                                dataTable.Rows[i][col] = BomConsumption.ToString();
                            }
                            catch (Exception)
                            {

                                dataTable.Rows[i][col] = 0;
                            }
                        }
                        else if (dataTable.Rows[i]["Details"].ToString().Trim() == "OverAll Marker Consumption")
                        {
                            try
                            {
                                TotalMarkerConsumption = Decimal.Parse(listelement.Select(u => u.MarkerConsumption).Average().ToString());

                                dataTable.Rows[i][col] = TotalMarkerConsumption;
                            }
                            catch (Exception)
                            {

                                dataTable.Rows[i][col] = 0;
                            }

                        }
                        else if (dataTable.Rows[i]["Details"].ToString().Trim() == "OverAll FCR Consumption")
                        {
                            try
                            {
                                TotalActualConsumption = Decimal.Parse(listelement.Select(u => u.ActualConsumption).Average().ToString());

                                dataTable.Rows[i][col] = TotalActualConsumption;
                            }
                            catch (Exception)
                            {

                                dataTable.Rows[i][col] = 0;
                            }
                        }
                        else if (dataTable.Rows[i]["Details"].ToString().Trim() == "OverAll over Consumed")
                        {
                            try
                            {
                                //var OverConsumed = listelement.Select(u => u.OverConsumed).Average();
                                var OverConsumed = TotalActualConsumption - TotalMarkerConsumption;

                                dataTable.Rows[i][col] = OverConsumed.ToString();
                            }
                            catch (Exception)
                            {

                                dataTable.Rows[i][col] = 0;
                            }

                        }
                        else if (dataTable.Rows[i]["Details"].ToString().Trim() == "OverAll Total Marker Fabric")
                        {
                            try
                            {

                                using (ArtEntitiesnew enty = new ArtEntitiesnew())
                                {
                                    var totalcutplanfab = enty.CutPlanMasters.Where(u => u.AtcDetail.AtcId == Atcid && u.Location_PK == locationID).Select(u => u.CutPlanFabReq).Sum();
                                    dataTable.Rows[i][col] = totalcutplanfab;
                                }


                            }
                            catch (Exception ex)
                            {

                                dataTable.Rows[i][col] = 0;
                            }

                        }

                    }

                }
            }





            fCRSummary.AtcNum = Atcnum;
            fCRSummary.Factory = factory;

            fCRSummary.FCRDatatable = dataTable;

            return fCRSummary;

        }





        public DataTable AddSummaryRows(DataTable dt)
        {



            return dt;
        }

        #endregion


        public DataTable Getdatewisefabricconsume(DateTime fromdate, DateTime todate, int locid, int atcid)
        {

            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();
            SqlCommand cmd = new SqlCommand(@"SELECT        OurStyle, LocationName, color, SUM(Ratiosum * NoOfPlies) AS CutQty, SUM(Fabutilized) AS FabricConsumed, LaysheetDate, OustyleID, Location_PK,skudet_pk
FROM            (SELECT        LaySheetMaster.LaySheet_PK, LaySheetMaster.CutOrderDet_PK, LaySheetMaster.LayCutNum, LaySheetMaster.IsEdited, LaySheetMaster.IsDetailUploaded, LaySheetMaster.NoOfPlies, 
                                                    LaySheetMaster.LaySheetNum, LaySheetMaster.OustyleID, LaySheetMaster.AtcID, LocationMaster.Location_PK,
                                                        (SELECT        CutOrderMaster.Color
                                                          FROM            CutOrderMaster INNER JOIN
                                                                                    CutOrderDetails ON CutOrderMaster.CutID = CutOrderDetails.CutID
                                                          WHERE        (CutOrderDetails.CutOrderDet_PK = LaySheetMaster.CutOrderDet_PK)) AS color, 
(SELECT        CutOrderMaster.SkuDet_pk
                                                          FROM            CutOrderMaster INNER JOIN
                                                                                    CutOrderDetails ON CutOrderMaster.CutID = CutOrderDetails.CutID
                                                          WHERE        (CutOrderDetails.CutOrderDet_PK = LaySheetMaster.CutOrderDet_PK)) AS skudet_pk,
CONVERT(DATE, LaySheetMaster.AddedDate, 101) AS LaysheetDate, LaySheetMaster.IsDeleted, ISNULL
                                                        ((SELECT        SUM(CutPlanMarkerSizeDetails.Ratio) AS Expr1
                                                            FROM            CutOrderDetails AS CutOrderDetails_1 INNER JOIN
                                                                                     CutPlanMarkerDetails ON CutOrderDetails_1.CutPlanMarkerDetails_PK = CutPlanMarkerDetails.CutPlanMarkerDetails_PK INNER JOIN
                                                                                     CutPlanMarkerSizeDetails ON CutPlanMarkerDetails.CutPlanMarkerDetails_PK = CutPlanMarkerSizeDetails.CutPlanMarkerDetails_PK
                                                            WHERE        (CutOrderDetails_1.CutOrderDet_PK = LaySheetMaster.CutOrderDet_PK) AND (CutPlanMarkerSizeDetails.IsDeleted = N'N') AND (CutPlanMarkerDetails.IsDeleted = N'N')), 0) AS Ratiosum, 
                                                    ISNULL
                                                        ((SELECT        SUM(EndBit) AS Expr1
                                                            FROM            LaySheetDetails
                                                            WHERE        (IsDeleted = 'N') AND (LaySheet_PK = LaySheetMaster.LaySheet_PK)), 0) AS NonReusableEndbit, ISNULL
                                                        ((SELECT        SUM(FabUtilized) AS Expr1
                                                            FROM            LaySheetDetails AS LaySheetDetails_1
                                                            WHERE        (IsDeleted = 'N') AND (LaySheet_PK = LaySheetMaster.LaySheet_PK)), 0) AS Fabutilized, AtcDetails.OurStyle, LocationMaster.LocationName
                          FROM            LaySheetMaster INNER JOIN
                                                    AtcDetails ON LaySheetMaster.OustyleID = AtcDetails.OurStyleID INNER JOIN
                                                    LocationMaster ON LaySheetMaster.Location_PK = LocationMaster.Location_PK
                          WHERE        (CONVERT(DATE, LaySheetMaster.AddedDate, 101) BETWEEN @fromdate AND @todate) AND (LaySheetMaster.Location_PK = @locid) AND (LaySheetMaster.IsDeleted = N'N')) AS tt
GROUP BY OurStyle, LocationName, color, LaysheetDate, OustyleID, Location_PK,skudet_pk
HAVING        (SUM(Ratiosum * NoOfPlies) > 0)
ORDER BY LaysheetDate, OustyleID");
            

                cmd.Parameters.AddWithValue("@fromdate", fromdate);
                cmd.Parameters.AddWithValue("@todate", todate);
                cmd.Parameters.AddWithValue("@locid", locid);
                dt = QueryFunctions.ReturnQueryResultDatatable(cmd);

            try
            {
                dt.Columns.Add("OrderQty(ASQ)");
                dt.Columns.Add("CummulativeQty");
                dt.Columns.Add("BalCutQty");
                dt.Columns.Add("TotalFabricConsumed");
                dt.Columns.Add("ConsumptionPerPC");
                dt.Columns.Add("ApprovedConsPPC");
                dt.Columns.Add("BOMConsumptionPerPc");
                dt.Columns.Add("BOMConsumed");
                dt.Columns.Add("AgainstBOMSaving");
            }
            catch (Exception exp)
            {

                throw;
            }
            foreach (DataRow rowdata in dt.Rows)
            {
                try
                {
                    int ourstyleid = int.Parse(rowdata["OustyleID"].ToString());
                    int skudet_pk = int.Parse(rowdata["skudet_pk"].ToString());
                    Decimal CutQty= Decimal.Parse(rowdata["CutQty"].ToString());
                    Decimal Fabconsumed= Decimal.Parse(rowdata["FabricConsumed"].ToString());
                    var date_from = DateTime.Parse(rowdata["LaysheetDate"].ToString());
                    var colorcode = rowdata["color"];
                    SqlCommand cmd1 = new SqlCommand(@"SELECT        SUM(CutQty) AS cumlative_qty, SUM(FabricConsumed) AS cosumed
FROM            (SELECT        OurStyle, LocationName, color, Ratiosum * NoOfPlies AS CutQty,  Fabutilized AS FabricConsumed,OustyleID
                          FROM            (SELECT        LaySheetMaster.LaySheet_PK, LaySheetMaster.CutOrderDet_PK, LaySheetMaster.LayCutNum, LaySheetMaster.IsEdited, LaySheetMaster.IsDetailUploaded, LaySheetMaster.NoOfPlies, 
                                                                              LaySheetMaster.LaySheetNum, LaySheetMaster.OustyleID, LaySheetMaster.AtcID,
                                                                                  (SELECT        CutOrderMaster.Color
                                                                                    FROM            CutOrderMaster INNER JOIN
                                                                                                              CutOrderDetails ON CutOrderMaster.CutID = CutOrderDetails.CutID
                                                                                    WHERE        (CutOrderDetails.CutOrderDet_PK = LaySheetMaster.CutOrderDet_PK)) AS color, CONVERT(DATE, LaySheetMaster.AddedDate, 101) AS LaysheetDate, LaySheetMaster.IsDeleted, 
                                                                              ISNULL
                                                                                  ((SELECT        SUM(CutPlanMarkerSizeDetails.Ratio) AS Expr1
                                                                                      FROM            CutOrderDetails AS CutOrderDetails_1 INNER JOIN
                                                                                                               CutPlanMarkerDetails ON CutOrderDetails_1.CutPlanMarkerDetails_PK = CutPlanMarkerDetails.CutPlanMarkerDetails_PK INNER JOIN
                                                                                                               CutPlanMarkerSizeDetails ON CutPlanMarkerDetails.CutPlanMarkerDetails_PK = CutPlanMarkerSizeDetails.CutPlanMarkerDetails_PK
                                                                                      WHERE        (CutOrderDetails_1.CutOrderDet_PK = LaySheetMaster.CutOrderDet_PK) AND (CutPlanMarkerSizeDetails.IsDeleted = N'N') AND (CutPlanMarkerDetails.IsDeleted = N'N')), 0) AS Ratiosum, 
                                                                              ISNULL
                                                                                  ((SELECT        SUM(EndBit) AS Expr1
                                                                                      FROM            LaySheetDetails
                                                                                      WHERE        (IsDeleted = 'N') AND (LaySheet_PK = LaySheetMaster.LaySheet_PK)), 0) AS NonReusableEndbit, ISNULL
                                                                                  ((SELECT        SUM(FabUtilized) AS Expr1
                                                                                      FROM            LaySheetDetails AS LaySheetDetails_1
                                                                                      WHERE        (IsDeleted = 'N') AND (LaySheet_PK = LaySheetMaster.LaySheet_PK)), 0) AS Fabutilized, AtcDetails.OurStyle, LocationMaster.LocationName
                                                    FROM            LaySheetMaster INNER JOIN
                                                                              AtcDetails ON LaySheetMaster.OustyleID = AtcDetails.OurStyleID INNER JOIN
                                                                              LocationMaster ON LaySheetMaster.Location_PK = LocationMaster.Location_PK
                                                    WHERE        (CONVERT(DATE, LaySheetMaster.AddedDate, 101) <=@from_date) AND (LaySheetMaster.Location_PK = @loc_id) and  (LaySheetMaster.IsDeleted = N'N')) AS tt) AS consolidate
where OustyleID=@ourstyleID and color=@Color GROUP BY OustyleID 
");
                    cmd1.Parameters.AddWithValue("@from_date", date_from);
                    cmd1.Parameters.AddWithValue("@loc_id", locid);
                    cmd1.Parameters.AddWithValue("@ourstyleID", ourstyleid);
                    cmd1.Parameters.AddWithValue("@Color", colorcode);
                    dt1 = QueryFunctions.ReturnQueryResultDatatable(cmd1);
                    foreach (DataRow cum in dt1.Rows)
                    {
                        Decimal cumulative = Decimal.Parse(cum["cumlative_qty"].ToString());
                        Decimal FabricConsumed = Decimal.Parse(cum["cosumed"].ToString());
                        rowdata["CummulativeQty"] = cumulative;
                        rowdata["TotalFabricConsumed"] = FabricConsumed;
                        try
                        {
                            rowdata["ConsumptionPerPC"] = FabricConsumed / cumulative;
                        }
                        catch (Exception)
                        {

                            rowdata["ConsumptionPerPC"] = 0;
                        }
                        
                        using (ArtEntitiesnew enty = new ArtEntitiesnew()) {
                            decimal totalqty = 0;
                            decimal totalweightedqty = 0;
                            var skupk = enty.SkuRawmaterialDetails.Where(u => u.SkuDet_PK == skudet_pk).Select(u => u.Sku_PK).FirstOrDefault();
                            int sku_pk = int.Parse(skupk.ToString());
                            var q4 = (from stylmstr in enty.StyleCostingMasters
                                      join styldet in enty.StyleCostingDetails
                                      on stylmstr.Costing_PK equals styldet.Costing_PK
                                      where styldet.Sku_PK == sku_pk && stylmstr.IsApproved == "A" && stylmstr.AtcDetail.OurStyleID == ourstyleid
                                      select styldet.Consumption).Max();

                            rowdata["BOMConsumptionPerPc"] = q4;
                            rowdata["BOMConsumed"] = q4 * cumulative;
                            rowdata["AgainstBOMSaving"] = (FabricConsumed - (q4 * cumulative));
                            var asqcolorcode = from s in enty.SkuRawmaterialDetails
                                    where s.SkuDet_PK == skudet_pk
                                            select s;
                            foreach(var cdode in asqcolorcode)
                            {
                                colorcode = cdode.ColorCode;
                            }
                            var orderqty = enty.POPackDetails.Where(u => u.OurStyleID == ourstyleid && u.PoPackMaster.ExpectedLocation_PK == locid && u.ColorCode == colorcode).Select(u => u.PoQty).Sum();
                            rowdata["OrderQty(ASQ)"] = orderqty;
                            rowdata["BalCutQty"] = orderqty- cumulative;


                            var q5 = (from cutorder in  enty.CutPlanMasters
                                  where cutorder.SkuDet_PK == skudet_pk && cutorder.OurStyleID == ourstyleid && cutorder.IsDeleted == "N"
                                  select new { cutorder.FabDescription, cutorder.CutPlan_PK, cutorder.CutplanConsumption }).ToList();

                            foreach (var element in q5)
                            {

                                try
                                {   
                                    if (element.CutplanConsumption != null)
                                    {
                                        int cutpk = int.Parse(element.CutPlan_PK.ToString());
                                        var qtynew = enty.CutPlanASQDetails.Where(u => u.CutPlan_PK == cutpk && u.IsDeleted == "N").Select(u => u.CutQty).Sum();
                                        totalqty += decimal.Parse(qtynew.ToString());

                                        decimal tryqty = decimal.Parse(qtynew.ToString()) * decimal.Parse(element.CutplanConsumption.ToString());
                                        totalweightedqty += tryqty;
                                    }
                                }
                                catch (Exception)
                                {


                                }

                            }
                            try
                            {
                                rowdata["ApprovedConsPPC"] = (totalweightedqty / totalqty);
                            }
                            catch (Exception)
                            {

                                rowdata["ApprovedConsPPC"] = 0;
                            }
                        }

                       
                    }
                }


                catch (Exception exp)
                {

                    throw;
                }
            }
           






            return dt;


        }


        public DataTable GetATCwisefabricconsume(int locid, int atcid)
        {

            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();
            SqlCommand cmd1 = new SqlCommand(@"SELECT        OurStyle, LocationName, color, SUM(Ratiosum * NoOfPlies) AS CutQty, SUM(Fabutilized) AS FabricConsumed,  OustyleID, Location_PK,skudet_pk,laysheetdate
                        FROM            (SELECT        LaySheetMaster.LaySheet_PK, LaySheetMaster.CutOrderDet_PK, LaySheetMaster.LayCutNum, LaySheetMaster.IsEdited, LaySheetMaster.IsDetailUploaded, LaySheetMaster.NoOfPlies, 
                        LaySheetMaster.LaySheetNum, LaySheetMaster.OustyleID, LaySheetMaster.AtcID, LocationMaster.Location_PK,                                                       
                        (SELECT        CutOrderMaster.Color FROM            CutOrderMaster INNER JOIN
                        CutOrderDetails ON CutOrderMaster.CutID = CutOrderDetails.CutID
                        WHERE        (CutOrderDetails.CutOrderDet_PK = LaySheetMaster.CutOrderDet_PK)) AS color, (SELECT        CutOrderMaster.SkuDet_pk
                        FROM            CutOrderMaster INNER JOIN
                        CutOrderDetails ON CutOrderMaster.CutID = CutOrderDetails.CutID
                        WHERE        (CutOrderDetails.CutOrderDet_PK = LaySheetMaster.CutOrderDet_PK)) AS skudet_pk,
                        (SELECT max(LaySheetMaster.AddedDate)
                        FROM            LaySheetMaster INNER JOIN CutOrderDetails ON LaySheetMaster.CutOrderDet_PK = CutOrderDetails.CutOrderDet_PK INNER JOIN
                                                 CutOrderMaster ON CutOrderDetails.CutID = CutOrderMaster.CutID
                        WHERE        (LaySheetMaster.OustyleID = atcdetails.OurStyleID) AND (CutOrderMaster.SkuDet_pk = skudet_pk)) as laysheetdate,
                         LaySheetMaster.IsDeleted,ISNULL((SELECT        SUM(CutPlanMarkerSizeDetails.Ratio) AS Expr1
                        FROM            CutOrderDetails AS CutOrderDetails_1 INNER JOIN
                        CutPlanMarkerDetails ON CutOrderDetails_1.CutPlanMarkerDetails_PK = CutPlanMarkerDetails.CutPlanMarkerDetails_PK INNER JOIN
                        CutPlanMarkerSizeDetails ON CutPlanMarkerDetails.CutPlanMarkerDetails_PK = CutPlanMarkerSizeDetails.CutPlanMarkerDetails_PK
                        WHERE        (CutOrderDetails_1.CutOrderDet_PK = LaySheetMaster.CutOrderDet_PK) AND (CutPlanMarkerSizeDetails.IsDeleted = N'N') AND (CutPlanMarkerDetails.IsDeleted = N'N')), 0) AS Ratiosum, 
                        ISNULL((SELECT        SUM(EndBit) AS Expr1 FROM            LaySheetDetails
                        WHERE        (IsDeleted = 'N') AND (LaySheet_PK = LaySheetMaster.LaySheet_PK)), 0) AS NonReusableEndbit, ISNULL
                        ((SELECT        SUM(FabUtilized) AS Expr1 
                        FROM            LaySheetDetails AS LaySheetDetails_1
                        WHERE        (IsDeleted = 'N') AND (LaySheet_PK = LaySheetMaster.LaySheet_PK)), 0) AS Fabutilized, AtcDetails.OurStyle, LocationMaster.LocationName
                        FROM            LaySheetMaster INNER JOIN
                        AtcDetails ON LaySheetMaster.OustyleID = AtcDetails.OurStyleID INNER JOIN
                        LocationMaster ON LaySheetMaster.Location_PK = LocationMaster.Location_PK
                        WHERE  (atcdetails.AtcId=@atcid) and (LaySheetMaster.Location_PK = @locid) AND (LaySheetMaster.IsDeleted = N'N')) AS tt
                        GROUP BY OurStyle, LocationName, color,  OustyleID, Location_PK,skudet_pk,laysheetdate
                        ORDER BY  OustyleID");
                    
                    cmd1.Parameters.AddWithValue("@locid", locid);
                    cmd1.Parameters.AddWithValue("@atcid", atcid);
                    dt1 = QueryFunctions.ReturnQueryResultDatatable(cmd1);
            dt1.Columns.Add("OrderQty(ASQ)");
            dt1.Columns.Add("BalCutQty");
            dt1.Columns.Add("ConsumptionPerPC");
            dt1.Columns.Add("ApprovedConsPPC");
            dt1.Columns.Add("BOMConsumptionPerPc");
            dt1.Columns.Add("BOMConsumed");
            dt1.Columns.Add("AgainstBOMSaving");
            foreach (DataRow cum in dt1.Rows)
                    {
                        Decimal cumulative = Decimal.Parse(cum["CutQty"].ToString());
                        Decimal FabricConsumed = Decimal.Parse(cum["FabricConsumed"].ToString());
                int skudet_pk = int.Parse(cum["skudet_pk"].ToString());
                int OustyleID = int.Parse(cum["OustyleID"].ToString());
                var colorcode = cum["color"];
                try
                {
                    cum["ConsumptionPerPC"] = FabricConsumed / cumulative;
                }
                catch (Exception exp)
                {

                    throw;
                }


                using (ArtEntitiesnew enty = new ArtEntitiesnew())
                        {
                            decimal totalqty = 0;
                            decimal totalweightedqty = 0;
                            var skupk = enty.SkuRawmaterialDetails.Where(u => u.SkuDet_PK == skudet_pk).Select(u => u.Sku_PK).FirstOrDefault();
                            int sku_pk = int.Parse(skupk.ToString());
                            var q4 = (from stylmstr in enty.StyleCostingMasters
                                      join styldet in enty.StyleCostingDetails
                                      on stylmstr.Costing_PK equals styldet.Costing_PK
                                      where styldet.Sku_PK == sku_pk && stylmstr.IsApproved == "A" && stylmstr.AtcDetail.OurStyleID == OustyleID
                                      select styldet.Consumption).Max();

                    cum["BOMConsumptionPerPc"] = q4;
                    cum["BOMConsumed"] = q4 * cumulative;
                    cum["AgainstBOMSaving"] = ( FabricConsumed- (q4 * cumulative));
                    var asqcolorcode = from s in enty.SkuRawmaterialDetails
                                       where s.SkuDet_PK == skudet_pk
                                       select s;
                    foreach (var cdode in asqcolorcode)
                    {
                        colorcode = cdode.ColorCode;
                    }
                    var orderqty = enty.POPackDetails.Where(u => u.OurStyleID == OustyleID && u.PoPackMaster.ExpectedLocation_PK == locid && u.ColorCode == colorcode).Select(u => u.PoQty).Sum();
                    cum["OrderQty(ASQ)"] = orderqty;
                    cum["BalCutQty"] = orderqty - cumulative;

                    var q5 = (from cutorder in enty.CutPlanMasters
                                      where cutorder.SkuDet_PK == skudet_pk && cutorder.OurStyleID == OustyleID && cutorder.IsDeleted == "N"
                                      select new { cutorder.FabDescription, cutorder.CutPlan_PK, cutorder.CutplanConsumption }).ToList();

                            foreach (var element in q5)
                            {

                                try
                                {
                                    if (element.CutplanConsumption != null)
                                    {
                                        int cutpk = int.Parse(element.CutPlan_PK.ToString());
                                        var qtynew = enty.CutPlanASQDetails.Where(u => u.CutPlan_PK == cutpk && u.IsDeleted == "N").Select(u => u.CutQty).Sum();
                                        totalqty += decimal.Parse(qtynew.ToString());

                                        decimal tryqty = decimal.Parse(qtynew.ToString()) * decimal.Parse(element.CutplanConsumption.ToString());
                                        totalweightedqty += tryqty;
                                    }
                                }
                                catch (Exception)
                                {


                                }

                            }
                            try
                            {
                                cum["ApprovedConsPPC"] = (totalweightedqty / totalqty);
                            }
                            catch (Exception)
                            {

                                cum["ApprovedConsPPC"] = 0;
                            }
                        }


                    }
            return dt1;


        }









    }
}
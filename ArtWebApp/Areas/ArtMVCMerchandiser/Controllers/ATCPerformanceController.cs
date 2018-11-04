using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Data;
using ArtWebApp.Areas.ArtMVCMerchandiser.ViewModel;
using System.Web.Mvc;
using ArtWebApp.DataModels;
using ArtWebApp.Areas.Repository;

namespace ArtWebApp.Areas.ArtMVCMerchandiser.Controllers
{
    public class ATCPerformanceController : Controller
    {
        ArtWebApp.DataModels.ArtEntitiesnew enty = new DataModels.ArtEntitiesnew();
        // GET: ArtMVCMerchandiser/ATCPerformance
        public ActionResult Index()
        {
            
            //ViewBag.AtcID = new SelectList(enty.AtcMasters.Where(u => u.IsShipmentCompleted == "Y" && u.IsMCRDone == "Y").ToList(), "AtcId", "AtcNum");
            ViewBag.AtcID = new SelectList(enty.AtcMasters, "AtcId", "AtcNum");
            return View();
        }


        [HttpGet]
        public PartialViewResult ShowATCPerformance(int atcid, decimal RejectedPcsRate, decimal GoodPcsRate)
        {
            DataTable dt = new DataTable();
            DataTable fdt = new DataTable();
            DataTable tdt = new DataTable();
            DataTable imt = new DataTable();
            DataTable chgt = new DataTable();
            DataTable cdt = new DataTable();
            DataTable tot = new DataTable();
            
            AtcPerformance atcPerformance = new AtcPerformance();
            AtcperformanceRepo repo = new AtcperformanceRepo();
            dt.Columns.Add("Description");
            dt.Columns.Add("Qty");
            dt.Columns.Add("Value");

            fdt.Columns.Add("Description");
            fdt.Columns.Add("Qty");
            fdt.Columns.Add("Value");

            tdt.Columns.Add("Description");
            tdt.Columns.Add("Qty");
            tdt.Columns.Add("Value");

            imt.Columns.Add("Description");
            imt.Columns.Add("Qty");
            imt.Columns.Add("Value");

            chgt.Columns.Add("Description");
            chgt.Columns.Add("Qty");
            chgt.Columns.Add("Value");

            cdt.Columns.Add("Description");
            cdt.Columns.Add("Qty");
            cdt.Columns.Add("Value");

            tot.Columns.Add("Description");
            tot.Columns.Add("Qty");
            tot.Columns.Add("Value");

            //DataRow dataRow = dt.NewRow();
            //dataRow["Description"]="Garments";
            //dataRow["Qty"]=0;
            //dataRow["Value"] = 0;
            //dt.Rows.Add(dataRow);
            DataRow dataRow1 = dt.NewRow();
            dataRow1["Description"]="Shipped to Buyer";
            dataRow1["Qty"]=0;
            dataRow1["Value"] = 0;
            dt.Rows.Add(dataRow1);
            DataRow dataRow2 = dt.NewRow();
            dataRow2["Description"]="Leftover Good Pcs";
            dataRow2["Qty"]=0;
            dataRow2["Value"] = 0;
            dt.Rows.Add(dataRow2);
            DataRow dataRow3 = dt.NewRow();
            dataRow3["Description"]="Leftover Rejected Pcs";
            dataRow3["Qty"]=0;
            dataRow3["Value"] = 0;
            dt.Rows.Add(dataRow3);
            DataRow dataRow4 = dt.NewRow();
            dataRow4["Description"]="Total Garments";
            dataRow4["Qty"]=0;
            dataRow4["Value"] = 0;
            dt.Rows.Add(dataRow4);

            DataRow dataRow5 = fdt.NewRow();
            dataRow5["Description"]="Fabric Purchased";
            dataRow5["Qty"]=0;
            dataRow5["Value"] = 0;
            fdt.Rows.Add(dataRow5);
            DataRow dataRow6 = fdt.NewRow();
            dataRow6["Description"]="Fabric Leftover";
            dataRow6["Qty"]=0;
            dataRow6["Value"] = 0;
            fdt.Rows.Add(dataRow6);
            DataRow dataRow7 = fdt.NewRow();
            dataRow7["Description"]="Fabric Cost";
            dataRow7["Qty"]=0;
            dataRow7["Value"] = 0;
            fdt.Rows.Add(dataRow7);

            DataRow dataRow8 = tdt.NewRow();
            dataRow8["Description"]="Accessories Purchased";
            dataRow8["Qty"]=0;
            dataRow8["Value"] = 0;
            tdt.Rows.Add(dataRow8);
            DataRow dataRow9 = tdt.NewRow();
            dataRow9["Description"]="Accessories LeftOver";
            dataRow9["Qty"]=0;
            dataRow9["Value"] = 0;
            tdt.Rows.Add(dataRow9);
            DataRow dataRow10 = tdt.NewRow();
            dataRow10["Description"]="Accessories Cost";
            dataRow10["Qty"]=0;
            dataRow10["Value"] = 0;
            tdt.Rows.Add(dataRow10);

            DataRow dataRow11 = chgt.NewRow();
            dataRow11["Description"] = "Freight Charges";
            dataRow11["Qty"] = 0;
            dataRow11["Value"] = 0;
            chgt.Rows.Add(dataRow11);
            DataRow dataRow12 = chgt.NewRow();
            dataRow12["Description"] = "Embroidery Charges";
            dataRow12["Qty"] = 0;
            dataRow12["Value"] = 0;
            chgt.Rows.Add(dataRow12);
            DataRow dataRow13 = chgt.NewRow();
            dataRow13["Description"] = "Washing Charges";
            dataRow13["Qty"] = 0;
            dataRow13["Value"] = 0;
            chgt.Rows.Add(dataRow13);
            DataRow dataRow14 = chgt.NewRow();
            dataRow14["Description"] = "Printing Charges";
            dataRow14["Qty"] = 0;
            dataRow14["Value"] = 0;
            chgt.Rows.Add(dataRow14);
            DataRow dataRow15 = chgt.NewRow();
            dataRow15["Description"] = "CM Charges";
            dataRow15["Qty"] = 0;
            dataRow15["Value"] = 0;
            chgt.Rows.Add(dataRow15);

            DataRow dataRow16 = imt.NewRow();
            dataRow16["Description"] = "Inventory Misplaced";
            dataRow16["Qty"] = 0;
            dataRow16["Value"] = 0;
            imt.Rows.Add(dataRow16);
            DataRow dataRow17 = imt.NewRow();
            dataRow17["Description"] = "Short Shipment";
            dataRow17["Qty"] = 0;
            dataRow17["Value"] = 0;
            imt.Rows.Add(dataRow17);
            DataRow dataRow18 = imt.NewRow();
            dataRow18["Description"] = "Buyers Debit Note";
            dataRow18["Qty"] = 0;
            dataRow18["Value"] = 0;
            imt.Rows.Add(dataRow18);
            DataRow dataRow19 = imt.NewRow();
            dataRow19["Description"] = "Excess Freight Charge";
            dataRow19["Qty"] = 0;
            dataRow19["Value"] = 0;
            imt.Rows.Add(dataRow19);
            DataRow dataRow20 = imt.NewRow();
            dataRow20["Description"] = "Discounts";
            dataRow20["Qty"] = 0;
            dataRow20["Value"] = 0;
            imt.Rows.Add(dataRow20);
            DataRow dataRow21 = imt.NewRow();
            dataRow21["Description"] = "Shortage";
            dataRow21["Qty"] = 0;
            dataRow21["Value"] = 0;
            imt.Rows.Add(dataRow21);
            DataRow dataRow22 = imt.NewRow();
            dataRow22["Description"] = "Quality Claims";
            dataRow22["Qty"] = 0;
            dataRow22["Value"] = 0;
            imt.Rows.Add(dataRow22);
            DataRow dataRow23 = imt.NewRow();
            dataRow23["Description"] = "Other Claims";
            dataRow23["Qty"] = 0;
            dataRow23["Value"] = 0;
            imt.Rows.Add(dataRow23);
            DataRow dataRow24 = imt.NewRow();
            dataRow24["Description"] = "Liability Claim";
            dataRow24["Qty"] = 0;
            dataRow24["Value"] = 0;
            imt.Rows.Add(dataRow24);

            DataRow dataRow25 = cdt.NewRow();
            dataRow25["Description"] = "Extra CM Charges";
            dataRow25["Qty"] = 0;
            dataRow25["Value"] = 0;
            cdt.Rows.Add(dataRow25);
            DataRow dataRow26 = cdt.NewRow();
            dataRow26["Description"] = "Miscellaneous";
            dataRow26["Qty"] = 0;
            dataRow26["Value"] = 0;
            cdt.Rows.Add(dataRow26);
            DataRow dataRow27 = cdt.NewRow();
            dataRow27["Description"] = "Claims";
            dataRow27["Qty"] = 0;
            dataRow27["Value"] = 0;
            cdt.Rows.Add(dataRow27);
            DataRow dataRow28 = cdt.NewRow();
            dataRow28["Description"] = "Discounts";
            dataRow28["Qty"] = 0;
            dataRow28["Value"] = 0;
            cdt.Rows.Add(dataRow28);
            DataRow dataRow29 = tot.NewRow();
            dataRow29["Description"] = "ATC(P/L)";
            dataRow29["Qty"] = 0;
            dataRow29["Value"] = 0;
            tot.Rows.Add(dataRow29);



            decimal ShippedQty = 0;
            decimal ShippedValue = 0;
            decimal Goodpcs = 0;
            decimal GoodpcsValue = 0;
            decimal Rejectpcs = 0;
            decimal RejectpcsValue = 0;
            decimal FabricValue = 0;
            decimal FabricQty= 0;
            decimal TrimsValue = 0;            
            decimal TrimsQty = 0;            
            decimal Fableftovervalue = 0;
            decimal FableftoverQty= 0;
            decimal TrimsleftoverValue = 0;
            decimal TrimsleftoverQty = 0;
            decimal misplaceqty = 0;
            decimal misplacevalue = 0;
            decimal shortshipqty = 0;
            decimal shortshipvalue = 0;
            decimal embqty = 0;
            decimal embvalue = 0;
            decimal washqty = 0;
            decimal washvalue = 0;
            decimal printqty = 0;
            decimal printvalue = 0;
            decimal otherqty = 0;
            decimal othervalue = 0;
            decimal freightcharge = 0;
            decimal cmqty = 0;
            decimal cmcharges = 0;
            foreach (DataRow row in dt.Rows)
            {
                String Detail = row["Description"].ToString().Trim();
                foreach (DataColumn col in dt.Columns)
                {
                    if (col.ColumnName != "Description" && col.ColumnName != "Qty")
                    {
                        if (Detail == "Shipped to Buyer")
                        {
                            DataTable fab = repo.getShippedQty(atcid);
                            if (fab != null)
                            {
                                ShippedQty = fab.AsEnumerable().Sum(Frow => Frow.Field<decimal>("InvoiceQty"));
                                ShippedValue = fab.AsEnumerable().Sum(Frow => Frow.Field<decimal>("price"));
                                row["Qty"] = ShippedQty;
                                row["Value"] = ShippedValue;
                            }
                        }
                        if (Detail == "Leftover Good Pcs")
                        {
                            DataTable fab = repo.GetGarmentdetails(atcid);
                            if (fab != null)
                            {
                                Goodpcs = fab.AsEnumerable().Sum(Frow => Frow.Field<decimal>("GoodPcs"));
                                GoodpcsValue= fab.AsEnumerable().Sum(Frow => Frow.Field<decimal>("GoodPcs"));
                                row["Qty"] = Goodpcs;
                                row["Value"] = Goodpcs*GoodPcsRate;
                            }
                        }
                        if (Detail == "Leftover Rejected Pcs")
                        {
                            DataTable fab = repo.GetGarmentdetails(atcid);
                            if (fab != null)
                            {
                                Rejectpcs = fab.AsEnumerable().Sum(Frow => Frow.Field<decimal>("RejectPcs"));
                                RejectpcsValue= fab.AsEnumerable().Sum(Frow => Frow.Field<decimal>("RejectPcs"));
                                row["Qty"] = Rejectpcs;
                                row["Value"] = RejectedPcsRate*Rejectpcs;
                            }
                        }
                        if (Detail == "Total Garments")
                        {
                            DataTable fab = repo.GetGarmentdetails(atcid);
                            if (fab != null)
                            {
                                
                                row["Qty"] = ShippedQty+ Goodpcs+ Rejectpcs;
                                row["Value"] = ShippedValue + GoodpcsValue + RejectpcsValue;
                            }
                        }
                    }
                }

            }
            foreach (DataRow row in fdt.Rows)
            {
                String Detail = row["Description"].ToString().Trim();
                foreach (DataColumn col in fdt.Columns)
                {
                    if (col.ColumnName != "Description" && col.ColumnName != "Qty")
                    {
                        
                        if (Detail == "Fabric Purchased")
                        {
                            DataTable fab = repo.getfabricdetails(atcid);
                            if (fab != null)
                            {
                                FabricValue = fab.AsEnumerable().Sum(Frow => Frow.Field<decimal>("PurchaseValue"));
                                FabricQty = fab.AsEnumerable().Sum(Frow => Frow.Field<decimal>("TotalQTY"));
                                row["Qty"] = FabricQty;
                                row["Value"] = FabricValue;
                            }
                        }
                       
                        if (Detail == "Fabric Leftover")
                        {
                            DataTable fab1 = repo.GetFabricLeftover(atcid);
                            if (fab1 != null)
                            {
                                Fableftovervalue = fab1.AsEnumerable().Sum(Frow => Frow.Field<decimal>("Value"));
                                FableftoverQty = fab1.AsEnumerable().Sum(Frow => Frow.Field<decimal>("PhysicalQty"));
                                row["Qty"] = FableftoverQty;
                                row["Value"] = Fableftovervalue;
                            }
                        }
                        if (Detail == "Fabric Cost")
                        {
                            row["Qty"] = FabricQty - FableftoverQty;
                            row["Value"] = FabricValue - Fableftovervalue;

                        }

                    }
                }

            }
            foreach (DataRow row in tdt.Rows)
            {
                String Detail = row["Description"].ToString().Trim();
                foreach (DataColumn col in tdt.Columns)
                {
                    if (col.ColumnName != "Description" && col.ColumnName != "Qty")
                    {
                        if (Detail == "Accessories Purchased")
                        {
                            DataTable trim = repo.getTrimsdetails(atcid);
                            if (trim != null)
                            {
                                TrimsValue = trim.AsEnumerable().Sum(Frow => Frow.Field<decimal>("PurchaseValue"));
                                TrimsQty = trim.AsEnumerable().Sum(Frow => Frow.Field<decimal>("TotalQTY"));
                                row["Qty"] = TrimsQty;
                                row["Value"] = TrimsValue;
                            }
                        }
                        if (Detail == "Accessories LeftOver")
                        {
                            DataTable trim1 = repo.GetTrimsLeftover(atcid);
                            if (trim1 != null)
                            {
                                TrimsleftoverValue = trim1.AsEnumerable().Sum(Frow => Frow.Field<decimal>("Value"));
                                TrimsleftoverQty = trim1.AsEnumerable().Sum(Frow => Frow.Field<decimal>("PhysicalQty"));
                                row["Qty"] = TrimsleftoverQty;
                                row["Value"] = TrimsleftoverValue;
                            }
                        }
                        
                        if (Detail == "Accessories Cost")
                        {
                            row["Qty"] = TrimsQty - TrimsleftoverQty;
                            row["Value"] = TrimsValue - TrimsleftoverValue;

                        }
                    }
                }

            }
            foreach (DataRow row in chgt.Rows)
            {
                String Detail = row["Description"].ToString().Trim();
                foreach (DataColumn col in imt.Columns)
                {
                    if (col.ColumnName != "Description" && col.ColumnName != "Qty")
                    {
                        if (Detail == "Freight Charges")
                        {
                            DataTable trim = repo.getFactoryLogisticCharge(atcid);
                            if (trim != null)
                            {
                                row["Qty"] = 1;
                                row["Value"] = trim.AsEnumerable().Sum(Frow => Frow.Field<decimal>("FreightCharge"));
                                freightcharge = trim.AsEnumerable().Sum(Frow => Frow.Field<decimal>("FreightCharge"));
                            }
                            
                        }

                        if (Detail == "Embroidery Charges")
                        {
                            DataTable trim = repo.getEmbroideryCharge(atcid);
                            if (trim != null)
                            {
                                embqty = trim.AsEnumerable().Sum(Frow => Frow.Field<decimal>("shippedqty"));
                                embvalue = trim.AsEnumerable().Sum(Frow => Frow.Field<decimal>("charge"));
                            }
                            row["Qty"] = embqty;
                            row["Value"] = embvalue;
                        }
                        if (Detail == "Washing Charges")
                        {
                            DataTable trim = repo.getWashingCharge(atcid);
                            if (trim != null)
                            {
                                washqty= trim.AsEnumerable().Sum(Frow => Frow.Field<decimal>("shippedqty"));
                                washvalue = trim.AsEnumerable().Sum(Frow => Frow.Field<decimal>("charge"));
                            }
                            row["Qty"] = washqty;
                            row["Value"] = washvalue;
                        }
                        if (Detail == "Printing Charges")
                        {
                            DataTable trim = repo.getPrintingCharge(atcid);
                            if (trim != null)
                            {
                                printqty = trim.AsEnumerable().Sum(Frow => Frow.Field<decimal>("shippedqty"));
                                printvalue = trim.AsEnumerable().Sum(Frow => Frow.Field<decimal>("charge"));
                            }
                            row["Qty"] = printqty;
                            row["Value"] = printvalue;
                        }
                        if (Detail == "CM Charges")
                        {
                            DataTable trim = repo.getCMCharge(atcid);
                            if (trim != null)
                            {
                                cmqty = trim.AsEnumerable().Sum(Frow => Frow.Field<decimal>("shippedqty"));
                                cmcharges= trim.AsEnumerable().Sum(Frow => Frow.Field<decimal>("charge"));
                            }
                            row["Qty"] = cmqty;
                            row["Value"] = cmcharges;
                        }
                    }
                }

            }


            foreach (DataRow row in imt.Rows)
            {
                String Detail = row["Description"].ToString().Trim();
                foreach (DataColumn col in imt.Columns)
                {
                    if (col.ColumnName != "Description" && col.ColumnName != "Qty")
                    {
                        if (Detail == "Inventory Misplaced")
                        {
                            DataTable trim = repo.getInventoryMisplaced(atcid);
                            if (trim != null)
                            {
                                foreach (DataRow drow in trim.Rows)
                                {
                                    decimal misqty= decimal .Parse(drow["Qty"].ToString());
                                    decimal misvalue = decimal .Parse(drow["CURate"].ToString());
                                    decimal totmisvalue = misqty * misvalue;
                                    misplaceqty = misplaceqty + misqty;
                                    misplacevalue = misplacevalue + totmisvalue;
                                }
                                
                            }
                            row["Qty"] = misplaceqty;
                            row["Value"] = misplacevalue;
                        }
                        if (Detail == "Buyers Debit Note")
                        {
                            //DataTable trim = repo.getInventoryMisplaced(atcid);
                            //if (trim != null)
                            //{
                            //    foreach (DataRow drow in trim.Rows)
                            //    {
                            //        decimal misqty = decimal.Parse(drow["Qty"].ToString());
                            //        decimal misvalue = decimal.Parse(drow["CURate"].ToString());
                            //        decimal totmisvalue = misqty * misvalue;
                            //        misplaceqty = misplaceqty + misqty;
                            //        misplacevalue = misplacevalue + totmisvalue;
                            //    }

                            //}
                            row["Qty"] = 0;
                            row["Value"] =0;
                        }

                        if (Detail == "Short Shipment")
                        {
                            DataTable trim = repo.getShortShipment(atcid);
                            if (trim != null)
                            {
                                decimal POQty = trim.AsEnumerable().Sum(Frow => Frow.Field<decimal>("POQty"));
                                decimal POValue = trim.AsEnumerable().Sum(Frow => Frow.Field<decimal>("POValue"));
                                decimal shipqty = trim.AsEnumerable().Sum(Frow => Frow.Field<decimal>("shipqty"));
                                decimal shipvalue = trim.AsEnumerable().Sum(Frow => Frow.Field<decimal>("shipvalue"));

                                shortshipqty = POQty -shipqty ;
                                shortshipvalue  = POValue - shipvalue ;
                            }
                            row["Qty"] = shortshipqty ;
                            row["Value"] = shortshipvalue ;
                        }
                        if (Detail == "Excess Freight Charge")
                        {
                            //DataTable trim = repo.getInventoryMisplaced(atcid);
                            //if (trim != null)
                            //{
                            //    foreach (DataRow drow in trim.Rows)
                            //    {
                            //        decimal misqty = decimal.Parse(drow["Qty"].ToString());
                            //        decimal misvalue = decimal.Parse(drow["CURate"].ToString());
                            //        decimal totmisvalue = misqty * misvalue;
                            //        misplaceqty = misplaceqty + misqty;
                            //        misplacevalue = misplacevalue + totmisvalue;
                            //    }

                            //}
                            row["Qty"] = 0;
                            row["Value"] = 0;
                        }
                        if (Detail == "Discounts")
                        {
                            //DataTable trim = repo.getInventoryMisplaced(atcid);
                            //if (trim != null)
                            //{
                            //    foreach (DataRow drow in trim.Rows)
                            //    {
                            //        decimal misqty = decimal.Parse(drow["Qty"].ToString());
                            //        decimal misvalue = decimal.Parse(drow["CURate"].ToString());
                            //        decimal totmisvalue = misqty * misvalue;
                            //        misplaceqty = misplaceqty + misqty;
                            //        misplacevalue = misplacevalue + totmisvalue;
                            //    }

                            //}
                            row["Qty"] = 0;
                            row["Value"] = 0;
                        }
                        if (Detail == "Shortage")
                        {
                            //DataTable trim = repo.getInventoryMisplaced(atcid);
                            //if (trim != null)
                            //{
                            //    foreach (DataRow drow in trim.Rows)
                            //    {
                            //        decimal misqty = decimal.Parse(drow["Qty"].ToString());
                            //        decimal misvalue = decimal.Parse(drow["CURate"].ToString());
                            //        decimal totmisvalue = misqty * misvalue;
                            //        misplaceqty = misplaceqty + misqty;
                            //        misplacevalue = misplacevalue + totmisvalue;
                            //    }

                            //}
                            row["Qty"] = 0;
                            row["Value"] = 0;
                        }
                        if (Detail == "Quality Claims")
                        {
                            //DataTable trim = repo.getInventoryMisplaced(atcid);
                            //if (trim != null)
                            //{
                            //    foreach (DataRow drow in trim.Rows)
                            //    {
                            //        decimal misqty = decimal.Parse(drow["Qty"].ToString());
                            //        decimal misvalue = decimal.Parse(drow["CURate"].ToString());
                            //        decimal totmisvalue = misqty * misvalue;
                            //        misplaceqty = misplaceqty + misqty;
                            //        misplacevalue = misplacevalue + totmisvalue;
                            //    }

                            //}
                            row["Qty"] = 0;
                            row["Value"] = 0;
                        }
                        if (Detail == "Other Claims")
                        {
                            //DataTable trim = repo.getInventoryMisplaced(atcid);
                            //if (trim != null)
                            //{
                            //    foreach (DataRow drow in trim.Rows)
                            //    {
                            //        decimal misqty = decimal.Parse(drow["Qty"].ToString());
                            //        decimal misvalue = decimal.Parse(drow["CURate"].ToString());
                            //        decimal totmisvalue = misqty * misvalue;
                            //        misplaceqty = misplaceqty + misqty;
                            //        misplacevalue = misplacevalue + totmisvalue;
                            //    }

                            //}
                            row["Qty"] = 0;
                            row["Value"] = 0;
                        }
                        if (Detail == "Liability Claims")
                        {
                            //DataTable trim = repo.getInventoryMisplaced(atcid);
                            //if (trim != null)
                            //{
                            //    foreach (DataRow drow in trim.Rows)
                            //    {
                            //        decimal misqty = decimal.Parse(drow["Qty"].ToString());
                            //        decimal misvalue = decimal.Parse(drow["CURate"].ToString());
                            //        decimal totmisvalue = misqty * misvalue;
                            //        misplaceqty = misplaceqty + misqty;
                            //        misplacevalue = misplacevalue + totmisvalue;
                            //    }

                            //}
                            row["Qty"] = 0;
                            row["Value"] = 0;
                        }
                    }
                }

            }
            foreach (DataRow row in cdt.Rows)
            {
                String Detail = row["Description"].ToString().Trim();
                foreach (DataColumn col in cdt.Columns)
                {
                    if (col.ColumnName != "Description" && col.ColumnName != "Qty")
                    {
                        if (Detail == "Extra CM Charges")
                        {
                            //DataTable trim = repo.getInventoryMisplaced(atcid);
                            //if (trim != null)
                            //{
                            //    foreach (DataRow drow in trim.Rows)
                            //    {
                            //        decimal misqty = decimal.Parse(drow["Qty"].ToString());
                            //        decimal misvalue = decimal.Parse(drow["CURate"].ToString());
                            //        decimal totmisvalue = misqty * misvalue;
                            //        misplaceqty = misplaceqty + misqty;
                            //        misplacevalue = misplacevalue + totmisvalue;
                            //    }

                            //}
                            row["Qty"] = 0;
                            row["Value"] = 0;
                        }
                        if (Detail == "Miscellaneous")
                        {
                            //DataTable trim = repo.getInventoryMisplaced(atcid);
                            //if (trim != null)
                            //{
                            //    foreach (DataRow drow in trim.Rows)
                            //    {
                            //        decimal misqty = decimal.Parse(drow["Qty"].ToString());
                            //        decimal misvalue = decimal.Parse(drow["CURate"].ToString());
                            //        decimal totmisvalue = misqty * misvalue;
                            //        misplaceqty = misplaceqty + misqty;
                            //        misplacevalue = misplacevalue + totmisvalue;
                            //    }

                            //}
                            row["Qty"] = 0;
                            row["Value"] = 0;
                        }
                        if (Detail == "Claims")
                        {
                            //DataTable trim = repo.getInventoryMisplaced(atcid);
                            //if (trim != null)
                            //{
                            //    foreach (DataRow drow in trim.Rows)
                            //    {
                            //        decimal misqty = decimal.Parse(drow["Qty"].ToString());
                            //        decimal misvalue = decimal.Parse(drow["CURate"].ToString());
                            //        decimal totmisvalue = misqty * misvalue;
                            //        misplaceqty = misplaceqty + misqty;
                            //        misplacevalue = misplacevalue + totmisvalue;
                            //    }

                            //}
                            row["Qty"] = 0;
                            row["Value"] = 0;
                        }
                        if (Detail == "Discounts")
                        {
                            //DataTable trim = repo.getInventoryMisplaced(atcid);
                            //if (trim != null)
                            //{
                            //    foreach (DataRow drow in trim.Rows)
                            //    {
                            //        decimal misqty = decimal.Parse(drow["Qty"].ToString());
                            //        decimal misvalue = decimal.Parse(drow["CURate"].ToString());
                            //        decimal totmisvalue = misqty * misvalue;
                            //        misplaceqty = misplaceqty + misqty;
                            //        misplacevalue = misplacevalue + totmisvalue;
                            //    }

                            //}
                            row["Qty"] = 0;
                            row["Value"] = 0;
                        }
                    }
                }
            }
            foreach (DataRow row in tot.Rows)
            {
                String Detail = row["Description"].ToString().Trim();
                foreach (DataColumn col in cdt.Columns)
                {
                    if (col.ColumnName != "Description" && col.ColumnName != "Qty")
                    {
                        if (Detail == "ATC(P/L)")
                        {                            
                            row["Qty"] = 0;
                            decimal garmentvalue = ShippedValue + GoodpcsValue + RejectpcsValue;
                            decimal totfabvalue = FabricValue - Fableftovervalue;
                            decimal tottrimsvalue = TrimsValue - TrimsleftoverValue;
                            decimal charges = freightcharge+ embvalue+ washvalue+printvalue+ cmcharges;
                            decimal debitnotes = misplacevalue+ shortshipvalue;

                            row["Value"] = (garmentvalue+ debitnotes) - (totfabvalue + tottrimsvalue + charges );
                        }
                    }
                }
            }


            var q = from atc in enty.AtcMasters where atc.AtcId == atcid select atc;
            foreach(var element in q)
            {
                atcPerformance.AtcNum = element.AtcNum;
            }
            atcPerformance.ATCDatatable = dt;
            atcPerformance.ATCFabricDatatable = fdt;
            atcPerformance.ATCTrimDatatable  = tdt;
            atcPerformance.InventoryMisplace = imt;
            atcPerformance.charges = chgt;
            atcPerformance.Creditnotes = cdt;
            atcPerformance.atcpl = tot;
            return PartialView("ATCPerformanceView_P", atcPerformance );

        }
    }
}
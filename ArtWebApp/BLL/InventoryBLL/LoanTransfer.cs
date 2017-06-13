using ArtWebApp.DataModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ArtWebApp.BLL.InventoryBLL
{
    public class LoanTransfer
    {
        public string LoanActionType = "";
        public LoanTransferData lftdata { get; set; }
        public String insertinvenloanmst(LoanTransferData inlmst)
        {
            String LOANNUM = "";
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {

                InventoryLoanMaster invloanmstr = new InventoryLoanMaster();
                invloanmstr.FromSkudet_PK = inlmst.FromSkudet_PK;
                invloanmstr.FromIIT_Pk = inlmst.FromIIT_Pk;
                invloanmstr.ToSkuDet_PK = inlmst.ToSkuDet_PK;
                invloanmstr.LoanQty = inlmst.LoanQty;
                invloanmstr.UnitPrice = inlmst.UnitPrice;
                invloanmstr.PaidBackQty = 0;
                invloanmstr.AddedBY = HttpContext.Current.Session["Username"].ToString().Trim();
                invloanmstr.AddedDate = DateTime.Now;
                invloanmstr.IsApproved = "N";
                invloanmstr.IsDeleted = "N";
                invloanmstr.LoanType = this.LoanActionType;
                invloanmstr.Location_PK = int.Parse(HttpContext.Current.Session["UserLoc_pk"].ToString().Trim());

                enty.InventoryLoanMasters.Add(invloanmstr);

                enty.SaveChanges();


                LOANNUM = invloanmstr.LoanNum = CodeGenerator.GetUniqueCode("LN", HttpContext.Current.Session["lOC_Code"].ToString().Trim(), int.Parse(invloanmstr.Loan_PK.ToString()));
                enty.SaveChanges();

            }

            return LOANNUM;

        }



        public void GetLoanApproved(int loan_pk)
        {
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {

                var q = from lnmstr in enty.InventoryLoanMasters
                        where lnmstr.Loan_PK == loan_pk
                        select lnmstr;

                foreach (var element in q)
                {
                    element.IsApproved = "Y";
                    element.ApprovedBy = HttpContext.Current.Session["Username"].ToString().Trim();
                    element.ApprovedDate = DateTime.Now;
                }
                transferloanQty(loan_pk);
                enty.SaveChanges();

            }
        }

        public void GetLoanDeleted(int loan_pk)
        {
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {

                var q = from lnmstr in enty.InventoryLoanMasters
                        where lnmstr.Loan_PK == loan_pk
                        select lnmstr;

                foreach (var element in q)
                {
                    element.IsDeleted = "Y";
                    element.IsApproved = "D";
                    element.DeletedBy = HttpContext.Current.Session["Username"].ToString().Trim();
                    element.DeletedDate = DateTime.Now;
                }

                enty.SaveChanges();

            }
        }



        public void transferloanQty(int loan_pk)
        {
            int mrndet_pk = 0;
            int podet_pk = 0;
            int uom_pk = 0;
            decimal curate = 0;
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {


                var q = from lnmstr in enty.InventoryLoanMasters
                        where lnmstr.Loan_PK == loan_pk
                        select lnmstr;

                foreach (var element in q)
                {
                    var existinginventory = from invitem in enty.InventoryMasters
                                            where invitem.InventoryItem_PK == element.FromIIT_Pk
                                            select invitem;

                    foreach (var invitemdetail in existinginventory)
                    {

                        invitemdetail.DeliveredQty = invitemdetail.DeliveredQty + element.LoanQty;
                        invitemdetail.OnhandQty = invitemdetail.OnhandQty - element.LoanQty;

                        uom_pk = int.Parse(invitemdetail.Uom_Pk.ToString());
                        mrndet_pk = int.Parse(invitemdetail.MrnDet_PK.ToString());
                        podet_pk = int.Parse(invitemdetail.PoDet_PK.ToString());
                        curate = decimal.Parse(invitemdetail.CURate.ToString());
                    }

                    InventoryMaster invmstr = new InventoryMaster();
                    invmstr.MrnDet_PK = mrndet_pk;
                    invmstr.PoDet_PK = podet_pk;
                    invmstr.Uom_Pk = uom_pk;
                    invmstr.SkuDet_Pk = element.ToSkuDet_PK;
                    invmstr.ReceivedQty = element.LoanQty;
                    invmstr.OnhandQty = element.LoanQty;
                    invmstr.DeliveredQty = 0;
                    invmstr.ReceivedVia = "LN";
                    invmstr.Location_PK = element.Location_PK;
                    invmstr.CURate = curate;
                    invmstr.AddedDate = DateTime.Now.Date;
                    invmstr.Refnum = element.LoanNum.ToString();
                    enty.InventoryMasters.Add(invmstr);

                }



                enty.SaveChanges();




            }
        }


        public DataTable getLoanofaSKU(int skudet_pk)
        {

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = @"SELECT        Loan_PK, LoanNum
FROM            InventoryLoanMaster
WHERE        (ToSkuDet_PK = @skudet_pk)";
                cmd.Parameters.AddWithValue("@skudet_pk", skudet_pk);

                return QueryFunctions.ReturnQueryResultDatatable(cmd);
            }
        }


        /// <summary>
        /// Get the Item from Which Loan is Taken
        /// </summary>
        /// <param name="atcid"></param>
        /// <param name="Location_pk"></param>
        /// <returns></returns>
        public DataTable GetFromIITDetailsOfALoan(int loan_pk)
        {
            DataTable dt = new DataTable();






            SqlCommand cmd = new SqlCommand(@"SELECT        InventoryMaster.InventoryItem_PK, InventoryMaster.SkuDet_Pk, SkuRawMaterialMaster.RMNum, 
                         SkuRawMaterialMaster.Composition + ' ' + SkuRawMaterialMaster.Construction + ' ' + SkuRawMaterialMaster.Weight + ' ' + SkuRawMaterialMaster.Width AS Description, SkuRawmaterialDetail.ItemColor, 
                         SkuRawmaterialDetail.ItemSize, ProcurementDetails.SupplierSize, ProcurementDetails.SupplierColor, UOMMaster.UomCode, InventoryMaster.OnhandQty, ProcurementDetails_1.POUnitRate, 
                         SkuRawMaterialMaster.Atc_id, SkuRawMaterialMaster.Template_pk, LocationMaster.LocationPrefix, ProcurementDetails.CURate, InventoryMaster.OnhandQty AS TransferQty, InventoryLoanMaster.Loan_PK, 
                         InventoryLoanMaster.LoanNum
FROM            InventoryMaster INNER JOIN
                         ProcurementDetails ON InventoryMaster.PoDet_PK = ProcurementDetails.PODet_PK INNER JOIN
                         SkuRawmaterialDetail ON ProcurementDetails.SkuDet_PK = SkuRawmaterialDetail.SkuDet_PK INNER JOIN
                         SkuRawMaterialMaster ON SkuRawmaterialDetail.Sku_PK = SkuRawMaterialMaster.Sku_Pk INNER JOIN
                         UOMMaster ON ProcurementDetails.Uom_PK = UOMMaster.Uom_PK INNER JOIN
                         ProcurementDetails AS ProcurementDetails_1 ON InventoryMaster.PoDet_PK = ProcurementDetails_1.PODet_PK INNER JOIN
                         LocationMaster ON InventoryMaster.Location_PK = LocationMaster.Location_PK INNER JOIN
                         InventoryLoanMaster ON InventoryMaster.InventoryItem_PK = InventoryLoanMaster.FromIIT_Pk
WHERE        (InventoryLoanMaster.Loan_PK = @Loan_pk)
ORDER BY SkuRawMaterialMaster.RMNum, Description, SkuRawmaterialDetail.ItemColor, SkuRawmaterialDetail.ItemSize, ProcurementDetails.SupplierSize, ProcurementDetails.SupplierColor, UOMMaster.UomCode");
            cmd.Parameters.AddWithValue("@Loan_pk", loan_pk);



            return QueryFunctions.ReturnQueryResultDatatable(cmd);
        }






    }
    public class LoanTransferData
    {
        public int Loan_PK { get; set; }
        public int FromSkudet_PK { get; set; }
        public int FromIIT_Pk { get; set; }
        public int ToSkuDet_PK { get; set; }
        public Decimal LoanQty { get; set; }
        public Decimal UnitPrice { get; set; }
        public int PaidBackQty { get; set; }
        public string AddedBY { get; set; }
        public DateTime AddedDate { get; set; }
        public int Location_PK { get; set; }
        public string IsApproved { get; set; }
        public string IsDeleted { get; set; }
        public string ApprovedBy { get; set; }
        public string DeletedBy { get; set; }
        public DateTime DeletedDate { get; set; }
    }


    public class InventoryTransferData
    {
        public int InventoryItemPK { get; set; }
        public int FromSkudet_PK { get; set; }
        public int Template_PK { get; set; }
        public string Composition { get; set; }
        public string Construct { get; set; }
        public string TemplateColor { get; set; }
        public string TemplateSize { get; set; }
        public string TemplateWidth { get; set; }
        public string TemplateWeight { get; set; }
        public string UOMCode { get; set; }
        public Decimal Unitprice { get; set; }
        public Decimal ReceivedQty { get; set; }
        public Decimal ExtraQty { get; set; }
        private int Uom_PK;

        public int Uom_PK1
        {
            get { return getUOM_Pk(UOMCode); }
            set { Uom_PK = value; }
        }

        public int getUOM_Pk(String UOM)
        {
            int uompk = 0;
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {
                var q = (from um in enty.UOMMasters
                         where um.UomCode.Trim() == UOM

                         select um.Uom_PK).FirstOrDefault();

                uompk = int.Parse(q.ToString());
            }

            return uompk;
        }


    }



    public class AtcToGstockTransferDetails
    {
        public int InventoryItemPK { get; set; }
        public int FromSkudet_PK { get; set; }
        public int Template_PK { get; set; }

        public Decimal OldUnitprice { get; set; }
        public Decimal NewUnitprice { get; set; }
        public Decimal ReceivedQty { get; set; }
    }



    public class AtcToGstockTransfermaster
    {
        public DateTime CreatedDate { get; set; }
        public string IsApproved { get; set; }
        public string IsDeleted { get; set; }
        public string AddedBy { get; set; }
        public string DeletedBy { get; set; }
        public string ApprovedBy { get; set; }
        public DateTime DeletedDate { get; set; }
        public DateTime Approveddate { get; set; }
        public int ToSkuDet_PK { get; set; }

        public int Location_Pk { get; set; }
        public List<AtcToGstockTransferDetails> AtcToGstockTransferDetailsCollection { get; set; }



        public String insertAtcToGstockData(AtcToGstockTransfermaster Porcpt)
        {
            String trnnum = "";
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {
                TransferToGstockMaster trnmstr = new TransferToGstockMaster();

                trnmstr.CreatedDate = DateTime.Now;

                trnmstr.AddedBy = Porcpt.AddedBy;

                trnmstr.Location_Pk = Porcpt.Location_Pk;
                trnmstr.IsApproved = "N";
                trnmstr.IsDeleted = "N";

                enty.TransferToGstockMasters.Add(trnmstr);
                enty.SaveChanges();

                trnnum = trnmstr.TransferNumber = CodeGenerator.GetUniqueCode("GT", HttpContext.Current.Session["lOC_Code"].ToString().Trim(), int.Parse(trnmstr.TransferToGSTock_PK.ToString()));


                foreach (AtcToGstockTransferDetails mrnrdet in Porcpt.AtcToGstockTransferDetailsCollection)
                {
                    TransferToGstockDetail mrndetdb = new TransferToGstockDetail();
                    mrndetdb.InventoryItemPK = mrnrdet.InventoryItemPK;
                    mrndetdb.TransferToGSTock_PK = trnmstr.TransferToGSTock_PK;
                    mrndetdb.FromSkudet_PK = mrnrdet.FromSkudet_PK;
                    mrndetdb.Template_PK = mrnrdet.Template_PK;
                    mrndetdb.OldUnitprice = mrnrdet.OldUnitprice;
                    mrndetdb.NewUnitprice = mrnrdet.NewUnitprice;
                    mrndetdb.ReceivedQty = mrnrdet.ReceivedQty;


                    enty.TransferToGstockDetails.Add(mrndetdb);

                    enty.SaveChanges();


                }

                enty.SaveChanges();


            }

            return trnnum;
        }

        public void GetTransferApproved(int trnsfr_pk)
        {
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {

                var q = from lnmstr in enty.TransferToGstockMasters
                        where lnmstr.TransferToGSTock_PK == trnsfr_pk
                        select lnmstr;

                foreach (var element in q)
                {
                    element.IsApproved = "Y";
                    element.ApprovedBy = HttpContext.Current.Session["Username"].ToString().Trim();
                    element.Approveddate = DateTime.Now;
                }
                transferQty(trnsfr_pk);
                enty.SaveChanges();

            }
        }

        public void transferQty(int trnsfr_pk)
        {
            int mrndet_pk = 0;

            int podet_pk = 0;
            int uom_pk = 0;
            decimal curate = 0;
            int skudetpk = 0;

            String Construction = "";
            String Composition = "";
            String ItemColor = "";
            String ItemSize = "";
            String TransferNumber = "";
            int location_pk = 0;

            int templete_PK = 0;
            Decimal NewUnitprice = 0;
            Decimal receivedQty = 0;

            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {




                var q = from lnmstr in enty.TransferToGstockMasters
                        where lnmstr.TransferToGSTock_PK == trnsfr_pk
                        select lnmstr;

                foreach (var element in q)
                {

                    location_pk = int.Parse(element.Location_Pk.ToString());

                    TransferNumber = element.TransferNumber.ToString();

                }


                StockPOMaster spomstr = new StockPOMaster();
                spomstr.Supplier_Pk = 1113;
                spomstr.DeliveryDate = DateTime.Now;
                spomstr.DeliveryTerms_Pk = 1;
                spomstr.DeliveryMethod_Pk = 1;
                spomstr.PaymentTermID = 3;
                spomstr.PO_value = 0;
                spomstr.Location_PK = location_pk;
                spomstr.CurrencyID = 18;
                spomstr.Remark = "Po against Stock Transfer";
                spomstr.AddedBy = "Admin";
                spomstr.AddedDate = DateTime.Now;
                spomstr.IsApproved = "Y";
                spomstr.IsDeleted = "N";
                spomstr.SPONum = TransferNumber;
                enty.StockPOMasters.Add(spomstr);
                enty.SaveChanges();

                StockMrnMaster smrnmstrdb = new StockMrnMaster();
                smrnmstrdb.DoNumber = TransferNumber;
                smrnmstrdb.AddedDate = DateTime.Now;
                smrnmstrdb.SPo_PK = spomstr.SPO_Pk;
                smrnmstrdb.AddedBY = "Admin";

                smrnmstrdb.Location_Pk = location_pk;
                smrnmstrdb.SReciept_Pk = 30122;
                enty.StockMrnMasters.Add(smrnmstrdb);
                enty.SaveChanges();



                var transferdetails = from trndet in enty.TransferToGstockDetails
                                      where trndet.TransferToGSTock_PK == trnsfr_pk
                                      select trndet;

                foreach (var trdet in transferdetails)
                {
                    templete_PK = int.Parse(trdet.Template_PK.ToString());
                    NewUnitprice = Decimal.Parse(trdet.NewUnitprice.ToString());
                    receivedQty = Decimal.Parse(trdet.ReceivedQty.ToString());

                    var existinginventory = from invitem in enty.InventoryMasters
                                            where invitem.InventoryItem_PK == trdet.InventoryItemPK
                                            select invitem;

                    foreach (var invitemdetail in existinginventory)
                    {

                        invitemdetail.DeliveredQty = invitemdetail.DeliveredQty + trdet.ReceivedQty;
                        invitemdetail.OnhandQty = invitemdetail.OnhandQty - trdet.ReceivedQty;

                        uom_pk = int.Parse(invitemdetail.Uom_Pk.ToString());
                        skudetpk = int.Parse(invitemdetail.SkuDet_Pk.ToString());
                    }


                    var templatedata = from skudet in enty.SkuRawmaterialDetails
                                       join skumstr in enty.SkuRawMaterialMasters on skudet.SkuDet_PK equals skumstr.Sku_Pk
                                       where skudet.SkuDet_PK == skudetpk
                                       select new { skumstr.Construction, skumstr.Composition, skudet.ItemColor, skudet.ItemSize };

                    foreach (var elementtemplatedata in templatedata)
                    {



                        Construction = elementtemplatedata.Construction == null ? "" : elementtemplatedata.Construction.ToString();
                        Composition = elementtemplatedata.Composition == null ? "" : elementtemplatedata.Composition.ToString();
                        ItemColor = elementtemplatedata.ItemColor == null ? "" : elementtemplatedata.ItemColor.ToString();
                        ItemSize = elementtemplatedata.ItemSize == null ? "" : elementtemplatedata.ItemSize.ToString();
                    }



                }
                StockPODetail spodetal = new StockPODetail();
                spodetal.SPO_PK = spomstr.SPO_Pk;
                spodetal.Template_PK = templete_PK;

                spodetal.Unitprice = NewUnitprice;
                spodetal.POQty = receivedQty;
                spodetal.Uom_PK = uom_pk;
                spodetal.CUrate = NewUnitprice;
                spodetal.Composition = Construction;
                spodetal.Construct = Composition;
                spodetal.TemplateColor = ItemColor;
                spodetal.TemplateSize = ItemSize;
                enty.StockPODetails.Add(spodetal);
                enty.SaveChanges();





                StockMRNDetail smrndetdb = new StockMRNDetail();
                smrndetdb.SMRN_Pk = smrnmstrdb.SMrn_PK;
                smrndetdb.SPODetails_PK = spodetal.SPODetails_PK;
                smrndetdb.SPO_PK = spomstr.SPO_Pk;
                smrndetdb.ReceivedQty = receivedQty;
                smrndetdb.Unitprice = NewUnitprice;
                smrndetdb.Uom_PK = uom_pk;
                smrndetdb.ExtraQty = 0;
                enty.StockMRNDetails.Add(smrndetdb);

                enty.SaveChanges();




                StockInventoryMaster sinvmstr = new StockInventoryMaster();

                sinvmstr.SMRNDet_Pk = smrndetdb.SMRNDet_Pk;
                sinvmstr.SPODetails_PK = spodetal.SPODetails_PK;
                sinvmstr.Template_PK = templete_PK;
                sinvmstr.OnHandQty = receivedQty;
                sinvmstr.ReceivedQty = receivedQty;
                sinvmstr.DeliveredQty = 0;
                sinvmstr.Unitprice = smrndetdb.Unitprice;
                sinvmstr.Composition = Composition;
                sinvmstr.Construct = Construction;
                sinvmstr.TemplateColor = ItemColor;
                sinvmstr.TemplateSize = ItemSize;

                sinvmstr.Uom_PK = uom_pk;
                sinvmstr.CuRate = smrndetdb.Unitprice;
                sinvmstr.ReceivedVia = "GTR";
                sinvmstr.Location_Pk = smrnmstrdb.Location_Pk;
                sinvmstr.Refnum = smrnmstrdb.SMrnNum;
                sinvmstr.AddedDate = DateTime.Now.Date;
                enty.StockInventoryMasters.Add(sinvmstr);
                enty.SaveChanges();









            }























        }
    }

    public class SalesMasterData
    {
        public int SalesDO_PK { get; set; }
        public string SalesDONum { get; set; }
        public DateTime SalesDate { get; set; }
        public int FromLocation_PK { get; set; }
        public int ToLocation_PK { get; set; }
        public DateTime SalesDODate { get; set; }
        public string ContainerNumber { get; set; }
        public string BoeNum { get; set; }
        public int Deliverymethod_Pk { get; set; }

        public string AddedBy { get; set; }
        public DateTime AddedDate { get; set; }
        public string DoType { get; set; }
        public List<SalesDetailsData> SalesDetailsDataCollection { get; set; }

        public String InsertSalesDOInternal()
        {
            String mrnum = "";
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {
                InventorySalesMaster trnmstr = new InventorySalesMaster();

                trnmstr.SalesDate = this.SalesDate;

                trnmstr.FromLocation_PK = this.FromLocation_PK;
                trnmstr.ToLocation_PK = this.ToLocation_PK;
                trnmstr.ContainerNumber = this.ContainerNumber;
                trnmstr.Deliverymethod_Pk = this.Deliverymethod_Pk;
                trnmstr.SalesDODate = DateTime.Now;
                trnmstr.ISApproved = "N";
                trnmstr.DoType = "Internal";
                trnmstr.AddedBy = HttpContext.Current.Session["Username"].ToString().Trim();
                trnmstr.AddedDate = DateTime.Now;
                trnmstr.ContainerNumber = this.ContainerNumber;

                enty.InventorySalesMasters.Add(trnmstr);
                enty.SaveChanges();

                mrnum = trnmstr.SalesDONum = ArtWebApp.CodeGenerator.GetUniqueCode("DO", HttpContext.Current.Session["lOC_Code"].ToString().Trim(), int.Parse(trnmstr.SalesDO_PK.ToString()));

                foreach (SalesDetailsData sinvdet in this.SalesDetailsDataCollection)
                {
                    InventorySalesDetail sinvdetdb = new InventorySalesDetail();
                    sinvdetdb.SalesDO_PK = trnmstr.SalesDO_PK;
                    sinvdetdb.SInventoryItem_PK = sinvdet.SInventoryItem_PK;
                    sinvdetdb.DeliveryQty = sinvdet.DeliveryQty;
                    sinvdetdb.CuRate = sinvdet.CUrate;



                    enty.InventorySalesDetails.Add(sinvdetdb);



                    var q = from invitem in enty.StockInventoryMasters
                            where invitem.SInventoryItem_PK == sinvdet.SInventoryItem_PK
                            select invitem;

                    foreach (var invitemdetail in q)
                    {
                        invitemdetail.DeliveredQty = invitemdetail.DeliveredQty + sinvdet.DeliveryQty;
                        invitemdetail.OnHandQty = invitemdetail.OnHandQty - sinvdet.DeliveryQty;
                    }

                    enty.SaveChanges();
                }

                enty.SaveChanges();


            }

            return mrnum;
        }



        public String InsertSalesDOExternal()
        {
            String mrnum = "";
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {
                InventorySalesMaster trnmstr = new InventorySalesMaster();

                trnmstr.SalesDate = this.SalesDate;

                trnmstr.FromLocation_PK = this.FromLocation_PK;
                trnmstr.ToLocation_PK = this.ToLocation_PK;
                trnmstr.ContainerNumber = this.ContainerNumber;
                trnmstr.Deliverymethod_Pk = this.Deliverymethod_Pk;
                trnmstr.SalesDODate = DateTime.Now;
                trnmstr.ISApproved = "N";
                trnmstr.DoType = "External";
                trnmstr.AddedBy = HttpContext.Current.Session["Username"].ToString().Trim();
                trnmstr.AddedDate = DateTime.Now;
                trnmstr.ContainerNumber = this.ContainerNumber;

                enty.InventorySalesMasters.Add(trnmstr);
                enty.SaveChanges();
                mrnum = trnmstr.SalesDONum = ArtWebApp.CodeGenerator.GetUniqueCode("EDO", HttpContext.Current.Session["lOC_Code"].ToString().Trim(), int.Parse(trnmstr.SalesDO_PK.ToString()));

                foreach (SalesDetailsData sinvdet in this.SalesDetailsDataCollection)
                {
                    InventorySalesDetail sinvdetdb = new InventorySalesDetail();
                    sinvdetdb.SalesDO_PK = trnmstr.SalesDO_PK;
                    sinvdetdb.SInventoryItem_PK = sinvdet.SInventoryItem_PK;
                    sinvdetdb.DeliveryQty = sinvdet.DeliveryQty;
                    sinvdetdb.CuRate = sinvdet.CUrate;



                    enty.InventorySalesDetails.Add(sinvdetdb);



                    var q = from invitem in enty.StockInventoryMasters
                            where invitem.SInventoryItem_PK == sinvdet.SInventoryItem_PK
                            select invitem;

                    foreach (var invitemdetail in q)
                    {
                        invitemdetail.DeliveredQty = invitemdetail.DeliveredQty + sinvdet.DeliveryQty;
                        invitemdetail.OnHandQty = invitemdetail.OnHandQty - sinvdet.DeliveryQty;
                    }

                    enty.SaveChanges();
                }

                enty.SaveChanges();


            }

            return mrnum;
        }

    }

     public class SalesDetailsData
        {
            public int SalesDODet_PK { get; set; }
            public int SalesDO_PK { get; set; }
            public int SInventoryItem_PK { get; set; }
            public Decimal DeliveryQty { get; set; }
            public Decimal CUrate { get; set; }
            public string Remark { get; set; }


        }

    }


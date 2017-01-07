using ArtWebApp.DataModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
namespace ArtWebApp.BLL.ShippingBLL
{
    public class ShippingBLL
    {
    }



    public class InvoiceData
    {
        public InvoiceMasterData Invmstrdata { get; set; }
        public List<InvoiceDetailsData> InvoiceDetailsDataCollection { get; set; }

        public String InsertInvoiceData(InvoiceData Invdata)
        {
            String Donum = "";
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {
                InvoiceMaster invmstr = new InvoiceMaster();
                invmstr.Location_PK = Invdata.Invmstrdata.Location_pk;
                invmstr.AddedDate = DateTime.Now;
                invmstr.AddedBy = Invdata.Invmstrdata.AddedBy;
                invmstr.Bank_PK = Invdata.Invmstrdata.Bank_pk;
                invmstr.Refnum = Invdata.Invmstrdata.refnum;
                enty.InvoiceMasters.Add(invmstr);


                enty.SaveChanges();

                Donum = invmstr.InvoiceNum = CodeGenerator.GetUniqueCode("INV", HttpContext.Current.Session["lOC_Code"].ToString().Trim(), int.Parse(invmstr.Invoice_PK.ToString()));




                foreach (InvoiceDetailsData di in Invdata.InvoiceDetailsDataCollection)
                {
                    //Add the delivery details
                    InvoiceDetail invdet = new InvoiceDetail();
                    invdet.Invoice_PK = invmstr.Invoice_PK;
                    invdet.FOB = decimal.Parse(di.FOB.ToString());
                    invdet.OurStyleID = di.OurStyleID;
                    invdet.InvoiceQty = di.InvoiceQty;
                    invdet.CartonNum = di.CartonNum;
                    invdet.PoPackID = di.PoPackID;
                    invdet.ShipmentHandOver_PK = di.ShipmentHandOver_PK;
                    enty.InvoiceDetails.Add(invdet);




                }
                enty.SaveChanges();

            }


            return Donum;
        }
    }

    public class InvoiceMasterData
    {

        public int Invoice_PK { get; set; }
        public int InvoiceNum { get; set; }
        public int Location_pk { get; set; }
        public int Bank_pk { get; set; }
        public string AddedBy { get; set; }
        public string refnum { get; set; }
        public DateTime AddedDate { get; set; }



    }

    public class InvoiceDetailsData
    {

        public int InvoiceDet_PK { get; set; }
        public int ShipmentHandOver_PK { get; set; }
        public float FOB { get; set; }
        public int PoPackID { get; set; }
        public int OurStyleID { get; set; }
        public int InvoiceQty { get; set; }
        public int CartonNum { get; set; }


    }
















    public class LCMasterData
    {
        public int LC_PK { get; set; }
        public string LCNum { get; set; }
        public int Bank_PK { get; set; }
        public DateTime Issuedate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public int Supplier_pk { get; set; }
        public int Location_Pk { get; set; }
        public string Status { get; set; }
        public decimal value { get; set; }
        public DateTime AddedDate { get; set; }
        public string AddedBy { get; set; }

        public void InsertLC()
        {

            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {
                LCMaster lcmstr = new LCMaster();
                lcmstr.LCNum = this.LCNum;
                lcmstr.AddedDate = DateTime.Now;
                lcmstr.AddedBy = this.AddedBy;
                lcmstr.Location_Pk = this.Location_Pk;
                lcmstr.Supplier_pk = this.Supplier_pk;
                lcmstr.Bank_PK = this.Bank_PK;
                lcmstr.Issuedate = this.Issuedate;
                lcmstr.ExpiryDate = this.ExpiryDate;
                enty.LCMasters.Add(lcmstr);


                enty.SaveChanges();
            }

        }



    }


    public class LCdata
    {


        public List<LCDetailData> LCDetailsDetailsDataCollection { get; set; }



        public List<LCDetailData> GetPOData(List<Infragistics.Web.UI.ListControls.DropDownItem> items, String PoType, int lc_pk)
        {

            List<LCDetailData> rk = new List<LCDetailData>();
            DataTable dt = new DataTable();
            string condition = "";
            foreach (Infragistics.Web.UI.ListControls.DropDownItem item in items)
            {

                LCDetailData lcdet = new LCDetailData();
                lcdet.POType = PoType;
                lcdet.LC_PK = lc_pk;
                lcdet.PO_PK = int.Parse(item.Value.ToString());
                lcdet.PONUM = item.Text.ToString().Trim();
                lcdet.LCValue = 0;
                lcdet.InvoiceNUM = "";

                lcdet.AddedBy = HttpContext.Current.Session["Username"].ToString().Trim(); ;
                lcdet.AddedDate = DateTime.Now;
                if (PoType == "PO")
                {
                    lcdet.ATCnum = getAtcNumofPO(lcdet.PO_PK);

                }
                else
                {
                    lcdet.ATCnum = "STOCK PO";
                }


                rk.Add(lcdet);
            }

            return rk;

        }



        public String getAtcNumofPO(int popk)
        {
            String atcnum = "";
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {
                enty.Configuration.AutoDetectChangesEnabled = false;
                var q = from atcmstr in enty.AtcMasters
                        join pomstr in enty.ProcurementMasters
                        on atcmstr.AtcId equals pomstr.AtcId
                        where pomstr.PO_Pk == popk
                        select new { atc = atcmstr.AtcNum };

                foreach (var element in q)
                {
                    atcnum = element.atc;
                }
            }
            return atcnum;
        }

        public String insertProductionbReport()
        {
            String Donum = "";
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {

                foreach (LCDetailData di in this.LCDetailsDetailsDataCollection)
                {
                    //Add the delivery details
                    LCDetail lcdet = new LCDetail();
                    lcdet.LC_PK = di.LC_PK;
                    lcdet.POType = di.POType;
                    lcdet.ATCnum = di.ATCnum;
                    lcdet.PONUM = di.PONUM;
                    lcdet.PO_PK = di.PO_PK;
                    lcdet.LCValue = di.LCValue;
                    lcdet.InvoiceNUM = di.InvoiceNUM;

                    lcdet.AddedBy = di.AddedBy;
                    lcdet.AddedDate = di.AddedDate;

                    enty.LCDetails.Add(lcdet);





                }
                enty.SaveChanges();

            }


            return Donum;
        }



        public DataTable getlcdata(int lcpk)
        {
            DataTable dt = new DataTable();
            DBTransaction.ShippingTransaction.lctransaction lctrn = new DBTransaction.ShippingTransaction.lctransaction();
            dt = lctrn.GetLCData(lcpk);
            return dt;
        }

    }



    public class LCDetailData
    {
        public int LCDet_PK { get; set; }
        public int LC_PK { get; set; }
        public string POType { get; set; }
        public string ATCnum { get; set; }
        public string PONUM { get; set; }
        public int PO_PK { get; set; }
        public decimal LCValue { get; set; }
        public string InvoiceNUM { get; set; }
        public string AddedBy { get; set; }
        public DateTime AddedDate { get; set; }


        public void updatelcdet()
        {
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {

                var q = from lcdet in enty.LCDetails
                        where lcdet.LCDet_PK == this.LCDet_PK
                        select lcdet;

                foreach (var element in q)
                {
                    element.LCValue = this.LCValue;
                    element.InvoiceNUM = this.InvoiceNUM;
                    element.AddedBy = this.AddedBy;
                    element.AddedDate = this.AddedDate;
                }
                enty.SaveChanges();
            }
        }


    }





    public class LCBankAdviceDetailsDataMaster
    {

        public List<LCBankAdviceDetailsData> LCBankAdviceDetailsDataCollection { get; set; }


        public String InsertLCBankAdviceDetails()
        {
            String Donum = "";
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {
                foreach (LCBankAdviceDetailsData di in this.LCBankAdviceDetailsDataCollection)
                {
                    //Add the delivery details
                    LCBankAdviceDetail invdet = new LCBankAdviceDetail();
                    invdet.LCDet_PK = di.LCDet_PK;
                    invdet.TrValue = di.TrValue;
                    invdet.Docnum = di.Docnum;
                    invdet.AddedBy = di.AddedBy;
                    invdet.AddedDate = di.AddedDate;
                    invdet.IsApproved = "N";
                    enty.LCBankAdviceDetails.Add(invdet);




                }
                enty.SaveChanges();

            }


            return Donum;
        }

    }





    public class LCBankAdviceDetailsData
    {
        public int BankAdvice_Pk { get; set; }
        public int LCDet_PK { get; set; }
        public decimal TrValue { get; set; }
        public string Docnum { get; set; }
        public DateTime AddedDate { get; set; }
        public string AddedBy { get; set; }

        public String updateLCBankAdviceDetails(int bankadvice_pk)
        {
            String Donum = "";
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {
                var q = from bnk in enty.LCBankAdviceDetails
                        where bnk.BankAdvice_Pk == bankadvice_pk
                        select bnk;
                foreach (var element in q)
                {
                    element.IsApproved = "A";
                }



                enty.SaveChanges();

            }


            return Donum;
        }
    }






    public class ShippingDocumentMasterData
    {


        public DateTime AddedDate { get; set; }
        public string AddedBY { get; set; }
        public string ShipperName { get; set; }
        public string ExporterName { get; set; }
        public string ShipperInv { get; set; }
        public string Description { get; set; }
        public string NOofctnRoll { get; set; }
        public string Packagetype { get; set; }
        public string Weight { get; set; }
        public string Type { get; set; }
        public string InvoiceValue { get; set; }
        public string Vessel { get; set; }
        public string Conatianer { get; set; }
        public string ContsainerType { get; set; }
        public DateTime ETA { get; set; }
        public string BL { get; set; }
        public string Mode { get; set; }
        public string ShipDocNum { get; set; }

        public string DocType { get; set; }

        public List<ShippingDocumentDetailsData> ShippingDocumentDetailsDataCollection { get; set; }
        public List<ShippingDocumentDODetailsData> ShippingDocumentDODetailsDataCollection { get; set; }
        public String InsertShippingDocumentDataDirect()
        {
            String Donum = "";
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {
                ShippingDocumentMaster shpdocmstr = new ShippingDocumentMaster();
                shpdocmstr.AddedBY = this.AddedBY;
                shpdocmstr.AddedDate = this.AddedDate;
                shpdocmstr.ShipperName = this.ShipperName;
                shpdocmstr.ExporterName = this.ExporterName;
                shpdocmstr.ShipperInv = this.ShipperInv;
                shpdocmstr.Description = this.Description;
                shpdocmstr.NOofctnRoll = this.NOofctnRoll;
                shpdocmstr.Packagetype = this.Packagetype;
                shpdocmstr.Weight = this.Weight;
                shpdocmstr.Type = this.Type;
                shpdocmstr.InvoiceValue = this.InvoiceValue;
                shpdocmstr.Vessel = this.Vessel;
                shpdocmstr.Conatianer = this.Conatianer;
                shpdocmstr.ContsainerType = this.ContsainerType;
                shpdocmstr.BL = this.BL;
                shpdocmstr.Mode = this.Mode;
                shpdocmstr.DocType = this.DocType;
                try
                {
                    shpdocmstr.ETA = this.ETA;
                }
                catch (Exception)
                {


                }


                enty.ShippingDocumentMasters.Add(shpdocmstr);


                enty.SaveChanges();

                Donum = shpdocmstr.ShipDocNum = CodeGenerator.GetUniqueCode("EXP", HttpContext.Current.Session["lOC_Code"].ToString().Trim(), int.Parse(shpdocmstr.ShipingDoc_PK.ToString()));




                foreach (ShippingDocumentDetailsData di in this.ShippingDocumentDetailsDataCollection)
                {

                    ShippingDocumentDetail invdet = new ShippingDocumentDetail();
                    invdet.ShipingDoc_PK = shpdocmstr.ShipingDoc_PK;

                    invdet.Doc_Pk = di.Doc_Pk;


                    enty.ShippingDocumentDetails.Add(invdet);




                }
                enty.SaveChanges();

            }


            return Donum;
        }

        public String InsertShippingDocumentDataVia()
        {
            String Donum = "";
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {
                ShippingDocumentMaster shpdocmstr = new ShippingDocumentMaster();
                shpdocmstr.AddedBY = this.AddedBY;
                shpdocmstr.AddedDate = this.AddedDate;
                shpdocmstr.ShipperName = this.ShipperName;
                shpdocmstr.ExporterName = this.ExporterName;
                shpdocmstr.ShipperInv = this.ShipperInv;
                shpdocmstr.Description = this.Description;
                shpdocmstr.NOofctnRoll = this.NOofctnRoll;
                shpdocmstr.Packagetype = this.Packagetype;
                shpdocmstr.Weight = this.Weight;
                shpdocmstr.Type = this.Type;
                shpdocmstr.InvoiceValue = this.InvoiceValue;
                shpdocmstr.Vessel = this.Vessel;
                shpdocmstr.Conatianer = this.Conatianer;
                shpdocmstr.ContsainerType = this.ContsainerType;
                shpdocmstr.BL = this.BL;
                shpdocmstr.Mode = this.Mode;
                shpdocmstr.DocType = this.DocType;
                try
                {
                    shpdocmstr.ETA = this.ETA;
                }
                catch (Exception)
                {


                }


                enty.ShippingDocumentMasters.Add(shpdocmstr);


                enty.SaveChanges();

                Donum = shpdocmstr.ShipDocNum = CodeGenerator.GetUniqueCode("EXP", HttpContext.Current.Session["lOC_Code"].ToString().Trim(), int.Parse(shpdocmstr.ShipingDoc_PK.ToString()));




                foreach (ShippingDocumentDODetailsData di in this.ShippingDocumentDODetailsDataCollection)
                {

                    ShippingDocumentDODetail invdet = new ShippingDocumentDODetail();
                    invdet.ShipingDoc_PK = shpdocmstr.ShipingDoc_PK;

                    invdet.DO_PK = di.DO_PK;


                    enty.ShippingDocumentDODetails.Add(invdet);




                }
                enty.SaveChanges();

            }


            return Donum;
        }


        public DataTable GetAWList()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"SELECT        DeliveryOrderMaster.DO_PK as PK, DeliveryOrderMaster.DONum as name
FROM DeliveryOrderMaster LEFT OUTER JOIN
              ShippingDocumentDODetails ON DeliveryOrderMaster.DO_PK = ShippingDocumentDODetails.DO_PK
WHERE(DeliveryOrderMaster.DeliveryDate = CONVERT(DATETIME, '2016-12-01 00:00:00', 102)) AND(ShippingDocumentDODetails.ShippingDocumentDO_PK IS NULL) AND(DeliveryOrderMaster.DONum LIKE N'AWATRW%')";

            return QueryFunctions.ReturnQueryResultDatatable(cmd);
        }
        public DataTable GetADNList()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"SELECT        DocMaster.Doc_Pk as PK, DocMaster.DocNum as Name
FROM            DocMaster LEFT OUTER JOIN
                         ShippingDocumentDetails ON DocMaster.Doc_Pk = ShippingDocumentDetails.Doc_Pk
WHERE        (DocMaster.InhouseDate > CONVERT(DATETIME, '2016-12-01 00:00:00', 102)) AND (ShippingDocumentDetails.ShippingDet_PK IS NULL) AND (DocMaster.ADNType = N'IntlSupplier')";

            return QueryFunctions.ReturnQueryResultDatatable(cmd);
        }


        public string getinboundtype(int shippingdoc_pk)
        {
            string inboundtype = "";
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {
                var typeofdet = enty.ShippingDocumentMasters.Where(u => u.ShipingDoc_PK == shippingdoc_pk).Select(u => u.DocType).FirstOrDefault();

                inboundtype = typeofdet.ToString();

            }
            return inboundtype;
        }

    }

    public class ShippingDocumentDetailsData
    {
        public int ShippingDet_PK { get; set; }
        public int ShipingDoc_PK { get; set; }
        public int Doc_Pk { get; set; }

    }


    public class ShippingDocumentDODetailsData
    {
        public int ShippingDet_PK { get; set; }
        public int DO_PK { get; set; }
    }

}










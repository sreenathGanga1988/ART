using ArtWebApp.DataModels;
using Infragistics.Web.UI.ListControls;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
namespace ArtWebApp.BLL.MerchandsingBLL
{
    public class AtcChartBLL
    {

        public void LoadSupplierCombo(int atcid, Infragistics.Web.UI.ListControls.WebDropDown cmb_debit)
        {


            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {             

                    var PoQuery = from spmstr  in enty.SupplierMasters join
                                       pomstr in enty.ProcurementMasters on spmstr.Supplier_PK equals pomstr.Supplier_Pk
                                  where pomstr.AtcId == atcid
                                  select new
                                  {
                                      name = spmstr.SupplierName,
                                      pk = spmstr.Supplier_PK
                                  };
                    cmb_debit.DataSource = PoQuery.ToList();
                    cmb_debit.DataBind();
               
                
            }

            
        }





        public void LoadPOCombo(int atcid,int supp_pk, Infragistics.Web.UI.ListControls.WebDropDown cmb_debit)
        {


            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {
                if(supp_pk==0)
                {
                    var PoQuery = from  pomstr in enty.ProcurementMasters 
                                  where pomstr.AtcId == atcid
                                  select new
                                  {
                                      name = pomstr.PONum,
                                      pk = pomstr.PO_Pk
                                  };

                    cmb_debit.DataSource = PoQuery.ToList();
                    cmb_debit.DataBind();

                }
                else
                {

                    var PoQuery = from pomstr in enty.ProcurementMasters
                                  where pomstr.AtcId == atcid && pomstr.Supplier_Pk == supp_pk
                                  select new
                                  {
                                      name = pomstr.PONum,
                                      pk = pomstr.PO_Pk
                                  };

                    cmb_debit.DataSource = PoQuery.ToList();
                    cmb_debit.DataBind();

                }

               
                


            }


        }







        public DataTable  getMultiSelectedPoData(List<DropDownItem> items)
        {
            DataTable dt = new DataTable();
            String Condition = "";

            foreach (DropDownItem item in items)
            {
                if(Condition.Trim()=="")
                {
                    Condition = " WHERE (ProcurementMaster.PO_Pk = " + item.Value.ToString().Trim() +" )";
                }else
                {
                    Condition = Condition + " OR ( ProcurementMaster.PO_Pk = " + item.Value.ToString().Trim() + ")";
                }
            }



            String Query = @"SELECT        PONum, SkuDet_PK, PODet_PK, RMNum, Description, ItemColor, ItemSize, SupplierColor, SupplierSize, UomCode, POQty, ReceivedQty, ISNULL(addedqty, 0) AS addedQty,ISNULL(ExtraQty, 0) AS ExtraQty , ISNULL(POQty - ISNULL(addedqty, 0), 
                         0) AS BalanceQty,POType
FROM            (SELECT        ProcurementDetails.PODet_PK, ProcurementDetails.SkuDet_PK, SkuRawMaterialMaster.RMNum, 
                         SkuRawMaterialMaster.Composition + ' ' + SkuRawMaterialMaster.Construction + ' ' + SkuRawMaterialMaster.Weight + ' ' + SkuRawMaterialMaster.Width AS Description, SkuRawmaterialDetail.ItemColor, 
                         SkuRawmaterialDetail.ItemSize, ProcurementDetails.SupplierColor, ProcurementDetails.SupplierSize, UOMMaster.UomCode, ProcurementDetails.POQty,
                             (SELECT        SUM(Qty) AS Expr1
                               FROM            DocDetails
                               GROUP BY PODet_Pk
                               HAVING         (PODet_Pk = ProcurementDetails.PODet_PK)) AS addedqty,
                             (SELECT        SUM(ExtraQty) AS Expr1
                               FROM            DocDetails AS DocDetails_1
                               GROUP BY PODet_Pk
                               HAVING         (PODet_Pk = ProcurementDetails.PODet_PK)) AS ExtraQty,
                             (SELECT        ISNULL(SUM(ReceiptQty) + SUM(ExtraQty), 0) AS Expr1
                               FROM            MrnDetails
                               WHERE        (PODet_PK = ProcurementDetails.PODet_PK) AND (SkuDet_PK = ProcurementDetails.SkuDet_PK)) AS ReceivedQty, ProcurementMaster.PONum, ProcurementMaster.PO_Pk, 
                         ProcurementMaster.POType
FROM            ProcurementDetails INNER JOIN
                         SkuRawmaterialDetail ON ProcurementDetails.SkuDet_PK = SkuRawmaterialDetail.SkuDet_PK INNER JOIN
                         SkuRawMaterialMaster ON SkuRawmaterialDetail.Sku_PK = SkuRawMaterialMaster.Sku_Pk INNER JOIN
                         UOMMaster ON ProcurementDetails.Uom_PK = UOMMaster.Uom_PK INNER JOIN
                         ProcurementMaster ON ProcurementDetails.PO_Pk = ProcurementMaster.PO_Pk " + Condition + ")tt";


            DBTransaction.MrnTransaction mrntra = new DBTransaction.MrnTransaction();

          dt=  mrntra.getMultiPodetails(Query);

          return dt;
        }



        public DataTable GetDetailsofADOC (int doc_PK)
        {
            DataTable dt = new DataTable();





            //            String Query = @"SELECT        tt.PONum, tt.SkuDet_PK, tt.PODet_PK, tt.RMNum, tt.Description, tt.ItemColor, tt.ItemSize, tt.SupplierColor, tt.SupplierSize, tt.UomCode, tt.POQty, DocDetails.Qty, DocDetails.Eta, DocDetails.Donumber, 
            //                         DocDetails.Doc_Pk, DocDetails.DocDet_Pk
            //FROM            (SELECT        ProcurementDetails.PODet_PK, ProcurementDetails.SkuDet_PK, SkuRawMaterialMaster.RMNum, 
            //                                                    SkuRawMaterialMaster.Composition + ' ' + SkuRawMaterialMaster.Construction + ' ' + SkuRawMaterialMaster.Weight + ' ' + SkuRawMaterialMaster.Width AS Description, 
            //                                                    SkuRawmaterialDetail.ItemColor, SkuRawmaterialDetail.ItemSize, ProcurementDetails.SupplierColor, ProcurementDetails.SupplierSize, UOMMaster.UomCode, ProcurementDetails.POQty, 
            //                                                    ProcurementMaster.PONum, ProcurementMaster.PO_Pk
            //                          FROM            ProcurementDetails INNER JOIN
            //                                                    SkuRawmaterialDetail ON ProcurementDetails.SkuDet_PK = SkuRawmaterialDetail.SkuDet_PK INNER JOIN
            //                                                    SkuRawMaterialMaster ON SkuRawmaterialDetail.Sku_PK = SkuRawMaterialMaster.Sku_Pk INNER JOIN
            //                                                    UOMMaster ON ProcurementDetails.Uom_PK = UOMMaster.Uom_PK INNER JOIN
            //                                                    ProcurementMaster ON ProcurementDetails.PO_Pk = ProcurementMaster.PO_Pk) AS tt INNER JOIN
            //                         DocDetails ON tt.PODet_PK = DocDetails.PODet_Pk
            //WHERE        (DocDetails.Doc_Pk = @Param1)";


            String Query = @"SELECT        ttt.PONum, ttt.SkuDet_PK, ttt.PODet_PK, ttt.RMNum, ttt.Description, ttt.ItemColor, ttt.ItemSize, ttt.SupplierColor, ttt.SupplierSize, ttt.UomCode, ttt.POQty, ttt.Qty, ttt.Eta, ttt.Donumber, ttt.Doc_Pk, ttt.DocDet_Pk, 
                         ttt.ExtraQty, ttt.ReceivedExtra, ttt.ReceivedQty, SUM(DocDetails_1.Qty) AS TotalDocQty, SUM(DocDetails_1.ExtraQty) AS TotalExtraQty
FROM            (SELECT        tt.PONum, tt.SkuDet_PK, tt.PODet_PK, tt.RMNum, tt.Description, tt.ItemColor, tt.ItemSize, tt.SupplierColor, tt.SupplierSize, tt.UomCode, tt.POQty, DocDetails.Qty, DocDetails.Eta, DocDetails.Donumber, 
                                                    DocDetails.Doc_Pk, DocDetails.DocDet_Pk, DocDetails.ExtraQty, ISNULL(SUM(MrnDetails.ExtraQty), 0) AS ReceivedExtra, ISNULL(SUM(MrnDetails.ReceiptQty), 0) AS ReceivedQty
                          FROM            (SELECT        ProcurementDetails.PODet_PK, ProcurementDetails.SkuDet_PK, SkuRawMaterialMaster.RMNum, 
                                                                              SkuRawMaterialMaster.Composition + ' ' + SkuRawMaterialMaster.Construction + ' ' + SkuRawMaterialMaster.Weight + ' ' + SkuRawMaterialMaster.Width AS Description, 
                                                                              SkuRawmaterialDetail.ItemColor, SkuRawmaterialDetail.ItemSize, ProcurementDetails.SupplierColor, ProcurementDetails.SupplierSize, UOMMaster.UomCode, ProcurementDetails.POQty, 
                                                                              ProcurementMaster.PONum, ProcurementMaster.PO_Pk
                                                    FROM            ProcurementDetails INNER JOIN
                                                                              SkuRawmaterialDetail ON ProcurementDetails.SkuDet_PK = SkuRawmaterialDetail.SkuDet_PK INNER JOIN
                                                                              SkuRawMaterialMaster ON SkuRawmaterialDetail.Sku_PK = SkuRawMaterialMaster.Sku_Pk INNER JOIN
                                                                              UOMMaster ON ProcurementDetails.Uom_PK = UOMMaster.Uom_PK INNER JOIN
                                                                              ProcurementMaster ON ProcurementDetails.PO_Pk = ProcurementMaster.PO_Pk) AS tt INNER JOIN
                                                    DocDetails ON tt.PODet_PK = DocDetails.PODet_Pk LEFT OUTER JOIN
                                                    MrnDetails ON DocDetails.PODet_Pk = MrnDetails.PODet_PK AND DocDetails.Doc_Pk = MrnDetails.Doc_Pk
													WHERE        (DocDetails.Doc_Pk = @Param1)
                          GROUP BY tt.PONum, tt.SkuDet_PK, tt.PODet_PK, tt.RMNum, tt.Description, tt.ItemColor, tt.ItemSize, tt.SupplierColor, tt.SupplierSize, tt.UomCode, tt.POQty, DocDetails.Qty, DocDetails.Eta, DocDetails.Donumber, 
                                                    DocDetails.Doc_Pk, DocDetails.DocDet_Pk, DocDetails.ExtraQty) AS ttt INNER JOIN
                         DocDetails AS DocDetails_1 ON ttt.PODet_PK = DocDetails_1.PODet_PK
GROUP BY ttt.PONum, ttt.SkuDet_PK, ttt.PODet_PK, ttt.RMNum, ttt.Description, ttt.ItemColor, ttt.ItemSize, ttt.SupplierColor, ttt.SupplierSize, ttt.UomCode, ttt.POQty, ttt.Qty, ttt.Eta, ttt.Donumber, ttt.Doc_Pk, ttt.DocDet_Pk, 
                         ttt.ExtraQty, ttt.ReceivedExtra, ttt.ReceivedQty";

            SqlCommand cmd = new SqlCommand();
            
                cmd.CommandText = Query;

            cmd.Parameters.AddWithValue("@Param1", doc_PK);

                return QueryFunctions.ReturnQueryResultDatatable (cmd) ;
        }


        public DataTable getMultiSelectedStockPoData(List<DropDownItem> items)
        {
            DataTable dt = new DataTable();
            String Condition = "";

            foreach (DropDownItem item in items)
            {
                if (Condition.Trim() == "")
                {
                    Condition = " WHERE (StockPODetails.SPO_PK  = " + item.Value.ToString().Trim() + " )";
                }
                else
                {
                    Condition = Condition + " OR ( StockPODetails.SPO_PK  = " + item.Value.ToString().Trim() + ")";
                }
            }



            String Query = @"SELECT        SPODetails_PK, Template_PK, Composition, Construct, TemplateColor, TemplateSize, TemplateWidth, TemplateWeight, POQty, UomCode, Unitprice, ReceivedQty,ISNULL(addedqty, 0) AS addedQty,ISNULL(ExtraQty, 0) AS ExtraQty , ISNULL(POQty - ISNULL(addedqty, 0), 
                         0) AS BalanceQty,
                         SPONum
FROM            (SELECT        StockPODetails.SPODetails_PK, StockPODetails.Template_PK, StockPODetails.Composition, StockPODetails.Construct, StockPODetails.TemplateColor, StockPODetails.TemplateSize, 
                                                    StockPODetails.TemplateWidth, StockPODetails.TemplateWeight, StockPODetails.POQty, UOMMaster.UomCode, StockPODetails.Unitprice, ISNULL
                                                        ((SELECT        SUM(ReceivedQty) AS Expr1
                                                            FROM            StockMRNDetails
                                                            GROUP BY SPODetails_PK
                                                            HAVING        (SPODetails_PK = StockPODetails.SPODetails_PK)), 0) AS ReceivedQty, 0 AS BalanceQty, StockPOMaster.SPONum,  (SELECT        SUM(Qty) AS Expr1
                               FROM            SDocDetails
                               GROUP BY SPODet_Pk
                               HAVING         (SPODet_Pk = StockPODetails.SPODetails_PK)) AS addedqty,
                             (SELECT        SUM(ExtraQty) AS Expr1
                               FROM            SDocDetails AS sDocDetails_1
                               GROUP BY SPODet_Pk
                               HAVING         (SPODet_Pk = StockPODetails.SPODetails_PK)) AS ExtraQty
                          FROM            StockPODetails INNER JOIN
                                                    UOMMaster ON StockPODetails.Uom_PK = UOMMaster.Uom_PK INNER JOIN
                                                    StockPOMaster ON StockPODetails.SPO_PK = StockPOMaster.SPO_Pk " + Condition + ")tt";


            DBTransaction.MrnTransaction mrntra = new DBTransaction.MrnTransaction();

            dt = mrntra.getMultiPodetails(Query);

            return dt;
        }

    }



    public class DocumentReceiptdata
    {
        public int Doc_Pk { get; set; }
        public string DocNum { get; set; }
        public int Location_PK { get; set; }
        public string ContainerNum { get; set; }
        public string BOENum { get; set; }
        public string Remark { get; set; }
        public DateTime InhouseDate { get; set; }
        public DateTime ETADate { get; set; }
        public int Supplier_PK { get; set; }
        public string AddedBy { get; set; }
        public DateTime AddedDate { get; set; }
        public string IsCompleted { get; set; }
        public int currency_Pk { get; set; }
        public Decimal docvalue { get; set; }
        public string DocType{ get; set; }
        public string Adntype { get; set; }
        public DateTime geteta(int Doc_Pk)
        {
            DateTime dt = new DateTime();
            using (ArtEntitiesnew entry = new ArtEntitiesnew())
            {
                var prefix = entry.DocMasters.Where(u => u.Doc_Pk == Doc_Pk).Select(u => u.ETADate).FirstOrDefault();
                dt = DateTime.Parse(prefix.ToString());
            }
            return dt;
        }

        /// <summary>
        /// insert document
        /// </summary>
        /// <param name="docreptmstr"></param>
        /// <returns></returns>
        public String InsertReciptMstr(DocumentReceiptdata docreptmstr)
        {

            String num = "";
            using (ArtEntitiesnew entry = new ArtEntitiesnew())
            {
                DocMaster dcmstr = new DocMaster();

              
                dcmstr.Location_PK = docreptmstr.Location_PK;
                dcmstr.ContainerNum = docreptmstr.ContainerNum;
                dcmstr.BOENum = docreptmstr.BOENum;
                dcmstr.Remark = docreptmstr.Remark;
                dcmstr.InhouseDate = docreptmstr.InhouseDate;
                dcmstr.ETADate = docreptmstr.ETADate;
                dcmstr.Supplier_PK = docreptmstr.Supplier_PK;
                dcmstr.AddedBy = HttpContext.Current.Session["Username"].ToString().Trim();
                dcmstr.AddedDate = DateTime.Now;
                dcmstr.IsCompleted = docreptmstr.IsCompleted;
                dcmstr.Currency_PK = int.Parse(docreptmstr.currency_Pk.ToString());
                dcmstr.DocValue = docreptmstr.docvalue;
                dcmstr.ADNType = docreptmstr.DocType;
                entry.DocMasters.Add(dcmstr);

                entry.SaveChanges();

                dcmstr.DocNum = CodeGenerator.GetUniqueCode("DocumentCreation", HttpContext.Current.Session["lOC_Code"].ToString().Trim(), int.Parse(dcmstr.Doc_Pk.ToString()));

              //  dcmstr.DocNum = "R" + HttpContext.Current.Session["lOC_Code"].ToString().Trim() + dcmstr.Doc_Pk.ToString().PadLeft(6, '0');
                num = dcmstr.DocNum;

                entry.SaveChanges();


            }


            return num;

        }

        /// <summary>
        /// update document
        /// </summary>
        /// <returns></returns>
        public String UpdateReciptMstr()
        {

            String num = "";
            using (ArtEntitiesnew entry = new ArtEntitiesnew())
            {

                var q = from docmstr in entry.DocMasters
                        where docmstr.Doc_Pk == this.Doc_Pk
                        select docmstr;


                foreach (var element in q)
                {
                    element.Location_PK = this.Location_PK;
                    element.ContainerNum = this.ContainerNum;
                    element.BOENum = this.BOENum;
                    element.Remark = this.Remark;
                    element.InhouseDate = DateTime.Now;
                    element.ETADate = this.ETADate;
                    element.Supplier_PK = this.Supplier_PK;
                    element.AddedBy = HttpContext.Current.Session["Username"].ToString().Trim();
                    element.AddedDate = DateTime.Now;
                    element.IsCompleted = this.IsCompleted;
                    element.ADNType = this.Adntype;
                }



                entry.SaveChanges();


                //  dcmstr.DocNum = "R" + HttpContext.Current.Session["lOC_Code"].ToString().Trim() + dcmstr.Doc_Pk.ToString().PadLeft(6, '0');
                


            }


            return num;

        }









        public List<DocPodetaildata> DocumentDetailsDataCollection { get; set; }






        public String insertPoEtaData(DocumentReceiptdata Dorcpt)
        {
            String mrnum = "";
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {

                foreach (DocPodetaildata mrnrdet in Dorcpt.DocumentDetailsDataCollection)
                {
                    DocDetail docdetdb = new DocDetail();
                    docdetdb.Doc_Pk = mrnrdet.Doc_Pk;
                    docdetdb.PODet_Pk = mrnrdet.podet_PK;
                    docdetdb.Qty = mrnrdet.Qty;
                    docdetdb.Eta = mrnrdet.ETADate;
                    docdetdb.Donumber = mrnrdet.InvNum;
                    docdetdb.AddedBy = mrnrdet.AddedBy;
                    docdetdb.AddedDate = mrnrdet.AddedDate;
                    docdetdb.ExtraQty = mrnrdet.eXCESSQty;
                    enty.DocDetails.Add(docdetdb);




                }

                enty.SaveChanges();


            }

            return mrnum;
        }









        public String UpdatePoEtaData()
        {
            String mrnum = "";
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {
               
                foreach (DocPodetaildata mrnrdet in this.DocumentDetailsDataCollection)
                 {

                    var q = from docdet in enty.DocDetails
                            where docdet.DocDet_Pk == mrnrdet.DocDet_Pk
                            select docdet;
                    
                    foreach (var element in q)
                    {

                        element.Qty = mrnrdet.Qty;
                      element.ExtraQty = mrnrdet.eXCESSQty;
                        element.Donumber = mrnrdet.InvNum;
                        element.AddedBy = mrnrdet.AddedBy;
                        element.AddedDate = mrnrdet.AddedDate;
                    }
                                  
                }
               
                enty.SaveChanges();

               
            }

            return mrnum;
        }




        public DocumentReceiptdata getdocedit(int doc_PK)
        {
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {

                var q = from docmstr in enty.DocMasters
                        where docmstr.Doc_Pk == doc_PK
                        select docmstr;

                foreach (var element in q)
                {
                    this.BOENum = element.BOENum;
                    this.ContainerNum = element.ContainerNum;
                    this.Remark = element.Remark;
                    this.InhouseDate = DateTime.Parse(element.InhouseDate.ToString());
                    this.ETADate = DateTime.Parse ( element.ETADate.ToString ());
                    this.Adntype = element.ADNType;
                   // this.currency_Pk =int.Parse (element.Currency_PK.ToString ());
                } 

            }
            return this;

            }


        public DataTable getDOCData(int docpk)
         {
             DataTable dt = new DataTable();
             DBTransaction.ShippingTransaction.DocumentTransaction shptran = new DBTransaction.ShippingTransaction.DocumentTransaction();
             dt = shptran.GetDOCData(docpk);
             return dt;

         }
        public DataTable getDOCData(ArrayList shpdetlist)
        {
            DataTable dt = new DataTable();
            DBTransaction.ShippingTransaction.DocumentTransaction shptran = new DBTransaction.ShippingTransaction.DocumentTransaction();
            dt = shptran.GetDOCData(shpdetlist);
            return dt;

        }
     
        public DataTable getSDOCData(ArrayList shpdetlist)
        {
            DataTable dt = new DataTable();
            DBTransaction.ShippingTransaction.DocumentTransaction shptran = new DBTransaction.ShippingTransaction.DocumentTransaction();
            dt = shptran.GetSDOCData(shpdetlist);
            return dt;

        }


        public DataTable getDOData(ArrayList shpdetlist)
        {
            DataTable dt = new DataTable();
            DBTransaction.ShippingTransaction.DocumentTransaction shptran = new DBTransaction.ShippingTransaction.DocumentTransaction();
            dt = shptran.GetDOData(shpdetlist);
            return dt;

        }

        public DataTable getSDOData(ArrayList shpdetlist)
        {
            DataTable dt = new DataTable();
            DBTransaction.ShippingTransaction.DocumentTransaction shptran = new DBTransaction.ShippingTransaction.DocumentTransaction();
            dt = shptran.GetSDOData(shpdetlist);
            return dt;

        }


        public string isADNMRNmakable(int adn_pk)
        {
            String mrnum = "";
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {

                var adntype = enty.DocMasters.Where(u => u.Doc_Pk == adn_pk).Select(u => u.ADNType).FirstOrDefault();

                mrnum = adntype.ToString();

            }
            return mrnum;

        }

        public string GetShippingDocument(int adn_pk)
        {
            String mrnum = "";
        

                SqlCommand cmd = new SqlCommand(@"SELECT        ShippingDocumentMaster.ShipDocNum
FROM            ShippingDocumentMaster INNER JOIN
                         ShippingDocumentDetails ON ShippingDocumentMaster.ShipingDoc_PK = ShippingDocumentDetails.ShipingDoc_PK
WHERE(ShippingDocumentDetails.Doc_Pk = @Param1)
GROUP BY ShippingDocumentMaster.ShipDocNum");

            cmd.Parameters.AddWithValue("@Param1", adn_pk);
            //  foreach (var elent in shipingdocnum)
            //{
            //    mrnum = elent.ShipDocNum;

            //}
            try
            {
                mrnum = QueryFunctions.ReturnQueryValue(cmd).ToString();
            }
            catch (Exception)
            {
                mrnum = "";


            }
       
            return mrnum;

        }


        public string GetShippingDODocument(int adn_pk)
        {
            String mrnum = "";
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {

                var shipingdocnum = from shipdocdet in enty.ShippingDocumentDODetails
                                    join shpdocmstr in enty.ShippingDocumentMasters on shipdocdet.ShipingDoc_PK equals shpdocmstr.ShipingDoc_PK
                                    select new { shpdocmstr.ShipDocNum };

                foreach (var elent in shipingdocnum)
                {
                    mrnum = elent.ShipDocNum;
                }

            }
            return mrnum;

        }

    }


    public class StockDocumentReceiptdata
    {
        public int Doc_Pk { get; set; }
        public string DocNum { get; set; }
        public int Location_PK { get; set; }
        public string ContainerNum { get; set; }
        public string BOENum { get; set; }
        public string Remark { get; set; }
        public DateTime InhouseDate { get; set; }
        public DateTime ETADate { get; set; }
        public int Supplier_PK { get; set; }
        public string AddedBy { get; set; }
        public DateTime AddedDate { get; set; }
        public string IsCompleted { get; set; }
        public int currency_Pk { get; set; }
        public Decimal docvalue { get; set; }


        public DateTime geteta(int Doc_Pk)
        {
            DateTime dt = new DateTime();
            using (ArtEntitiesnew entry = new ArtEntitiesnew())
            {
                var prefix = entry.SDocMasters.Where(u => u.SDoc_Pk == Doc_Pk).Select(u => u.ETADate).FirstOrDefault();
                dt = DateTime.Parse(prefix.ToString());
            }
            return dt;
        }

        /// <summary>
        /// insert document
        /// </summary>
        /// <param name="docreptmstr"></param>
        /// <returns></returns>
        public String InsertReciptMstr(StockDocumentReceiptdata docreptmstr)
        {

            String num = "";
            using (ArtEntitiesnew entry = new ArtEntitiesnew())
            {
                SDocMaster dcmstr = new SDocMaster();


                dcmstr.Location_PK = docreptmstr.Location_PK;
                dcmstr.ContainerNum = docreptmstr.ContainerNum;
                dcmstr.BOENum = docreptmstr.BOENum;
                dcmstr.Remark = docreptmstr.Remark;
                dcmstr.InhouseDate = DateTime.Now;
                dcmstr.ETADate = docreptmstr.ETADate;
                dcmstr.Supplier_PK = docreptmstr.Supplier_PK;
                dcmstr.AddedBy = HttpContext.Current.Session["Username"].ToString().Trim();
                dcmstr.AddedDate = DateTime.Now;
                dcmstr.IsCompleted = docreptmstr.IsCompleted;
                dcmstr.Currency_PK = int.Parse(docreptmstr.currency_Pk.ToString());
                dcmstr.SDocValue = dcmstr.SDocValue;

                entry.SDocMasters.Add(dcmstr);

                entry.SaveChanges();

                dcmstr.SDocNum = CodeGenerator.GetUniqueCode("SDocumentCreation", HttpContext.Current.Session["lOC_Code"].ToString().Trim(), int.Parse(dcmstr.SDoc_Pk.ToString()));

                //  dcmstr.DocNum = "R" + HttpContext.Current.Session["lOC_Code"].ToString().Trim() + dcmstr.Doc_Pk.ToString().PadLeft(6, '0');
                num = dcmstr.SDocNum;

                entry.SaveChanges();


            }


            return num;

        }

        /// <summary>
        /// update document
        /// </summary>
        /// <returns></returns>
        public String UpdateReciptMstr()
        {

            String num = "";
            using (ArtEntitiesnew entry = new ArtEntitiesnew())
            {

                var q = from docmstr in entry.SDocMasters
                        where docmstr.SDoc_Pk == this.Doc_Pk
                        select docmstr;


                foreach (var element in q)
                {
                    element.Location_PK = this.Location_PK;
                    element.ContainerNum = this.ContainerNum;
                    element.BOENum = this.BOENum;
                    element.Remark = this.Remark;
                    element.InhouseDate = DateTime.Now;
                    element.ETADate = this.ETADate;
                    element.Supplier_PK = this.Supplier_PK;
                    element.AddedBy = HttpContext.Current.Session["Username"].ToString().Trim();
                    element.AddedDate = DateTime.Now;
                    element.IsCompleted = this.IsCompleted;
                }



                entry.SaveChanges();


                //  dcmstr.DocNum = "R" + HttpContext.Current.Session["lOC_Code"].ToString().Trim() + dcmstr.Doc_Pk.ToString().PadLeft(6, '0');



            }


            return num;

        }









        public List<DocPodetaildata> DocumentDetailsDataCollection { get; set; }






        public String insertPoEtaData(DocumentReceiptdata Dorcpt)
        {
            String mrnum = "";
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {

                foreach (DocPodetaildata mrnrdet in Dorcpt.DocumentDetailsDataCollection)
                {
                    SDocDetail docdetdb = new SDocDetail();
                    docdetdb.SDoc_Pk = mrnrdet.Doc_Pk;
                    docdetdb.SPODet_Pk = mrnrdet.podet_PK;
                    docdetdb.Qty = mrnrdet.Qty;
                    docdetdb.Eta = mrnrdet.ETADate;
                    docdetdb.Donumber = mrnrdet.InvNum;
                    docdetdb.AddedBy = mrnrdet.AddedBy;
                    docdetdb.AddedDate = mrnrdet.AddedDate;
                    docdetdb.ExtraQty = mrnrdet.eXCESSQty;
                    enty.SDocDetails.Add(docdetdb);




                }

                enty.SaveChanges();


            }

            return mrnum;
        }









        public String UpdatePoEtaData()
        {
            String mrnum = "";
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {

                foreach (DocPodetaildata mrnrdet in this.DocumentDetailsDataCollection)
                {

                    var q = from docdet in enty.SDocDetails
                            where docdet.SDocDet_Pk == mrnrdet.DocDet_Pk
                            select docdet;

                    foreach (var element in q)
                    {

                        element.Qty = mrnrdet.Qty;
                        //  element.Eta = mrnrdet.ETADate;
                        element.Donumber = mrnrdet.InvNum;
                        element.AddedBy = mrnrdet.AddedBy;
                        element.AddedDate = mrnrdet.AddedDate;
                    }

                }

                enty.SaveChanges();


            }

            return mrnum;
        }




        public StockDocumentReceiptdata getdocedit(int doc_PK)
        {
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {

                var q = from docmstr in enty.DocMasters
                        where docmstr.Doc_Pk == doc_PK
                        select docmstr;

                foreach (var element in q)
                {
                    this.BOENum = element.BOENum;
                    this.ContainerNum = element.ContainerNum;
                    this.Remark = element.Remark;
                    this.InhouseDate = DateTime.Parse(element.InhouseDate.ToString());
                    this.ETADate = DateTime.Parse(element.ETADate.ToString());
                }

            }
            return this;

        }


        public DataTable getDOCData(int docpk)
        {
            DataTable dt = new DataTable();
            DBTransaction.ShippingTransaction.DocumentTransaction shptran = new DBTransaction.ShippingTransaction.DocumentTransaction();
            dt = shptran.GetDOCData(docpk);
            return dt;

        }
        public DataTable getDOCData(ArrayList shpdetlist)
        {
            DataTable dt = new DataTable();
            DBTransaction.ShippingTransaction.DocumentTransaction shptran = new DBTransaction.ShippingTransaction.DocumentTransaction();
            dt = shptran.GetDOCData(shpdetlist);
            return dt;

        }



        public DataTable getDOData(ArrayList shpdetlist)
        {
            DataTable dt = new DataTable();
            DBTransaction.ShippingTransaction.DocumentTransaction shptran = new DBTransaction.ShippingTransaction.DocumentTransaction();
            dt = shptran.GetDOData(shpdetlist);
            return dt;

        }



    }

    public class DocPodetaildata
    {
        public int Doc_Pk { get; set; }

        public int DocDet_Pk { get; set; }
        public DateTime ETADate { get; set; }
        public int podet_PK { get; set; }
        public string AddedBy { get; set; }
        public DateTime AddedDate { get; set; }
      
        public string IsCompleted { get; set; }
        public string InvNum { get; set; }
        public decimal Qty { get; set; }
        public decimal eXCESSQty { get; set; }



    }

    public class ProcurementplanMasterBLL
    {


        public List<ProcurementplanDetailsData> ProcurementplanDetailsDataCollection { get; set; }
        public string insertPlaningMaster()
        {
            string asqshuffle = "";
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {


                foreach (ProcurementplanDetailsData rdet in this.ProcurementplanDetailsDataCollection)
                {




                    ProcurmentPlanDetail pkdet = new DataModels.ProcurmentPlanDetail();


                    pkdet.Skudet_Pk = rdet.Skudet_Pk;
                    pkdet.Qty = rdet.Qty;
                    pkdet.ETADate = rdet.ETADate;
                    pkdet.IsDeleted = "N";
                    pkdet.AddedBY = HttpContext.Current.Session["Username"].ToString().Trim();
                    pkdet.AddedDate = DateTime.Now;
                    enty.ProcurmentPlanDetails.Add(pkdet);



                }


                enty.SaveChanges();

            }

            return asqshuffle;


        }

        public String DeletePlaning(int prpl_pk)
        {
            string asqshuffle = "Error";
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {
                var q = from ppl in enty.ProcurmentPlanDetails
                        where ppl.ProcPlan_PK == prpl_pk
                        select ppl;

                foreach (var element in q)
                {
                    element.IsDeleted = "Y";
                    element.DeletedBy = HttpContext.Current.Session["Username"].ToString().Trim();
                }
                enty.SaveChanges();
                asqshuffle = "Sucessfully Deleted";
            }

            return asqshuffle;
        }
    }
    public class ProcurementplanDetailsData
    {
        public int Skudet_Pk { get; set; }
        public Decimal Qty { get; set; }
        public DateTime ETADate { get; set; }
        public int AddedBY { get; set; }
        public DateTime AddedDate { get; set; }
        public string insertETA()
        {
            string sucess = "";
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {
                ProcurmentPlanDetail pkdet = new DataModels.ProcurmentPlanDetail();


                pkdet.Skudet_Pk = this.Skudet_Pk;
                pkdet.Qty = this.Qty;
                pkdet.ETADate = this.ETADate;

                pkdet.AddedBY = HttpContext.Current.Session["Username"].ToString().Trim();
                pkdet.AddedDate = DateTime.Now;
                enty.ProcurmentPlanDetails.Add(pkdet);
                enty.SaveChanges();
            }

            return sucess;
        }
    }

    public class ProcurementplanRemarkData
    {
        public int Skudet_Pk { get; set; }
        public String Remark { get; set; }

        public string insertRemark()
        {
            string sucess = "";
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {
                PlaningRemarkMaster prmmstr = new DataModels.PlaningRemarkMaster();
                prmmstr.Remark = this.Remark;
                prmmstr.SkuDet_PK = this.Skudet_Pk;
                prmmstr.AddedBy = HttpContext.Current.Session["Username"].ToString().Trim();
                prmmstr.AddedDate = DateTime.Now;
                prmmstr.IsDeleted = "N";
                enty.PlaningRemarkMasters.Add(prmmstr);
                enty.SaveChanges();
            }

            return sucess;
        }


        public String DeleteRemark(int prpl_pk)
        {
            string asqshuffle = "Error";
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {
                var q = from ppl in enty.PlaningRemarkMasters
                        where ppl.PlanRemark_PK == prpl_pk
                        select ppl;

                foreach (var element in q)
                {
                    element.IsDeleted = "Y";
                    element.DeletedBy = HttpContext.Current.Session["Username"].ToString().Trim();
                }
                enty.SaveChanges();
                asqshuffle = "Sucessfully Deleted";
            }

            return asqshuffle;
        }
    }





}
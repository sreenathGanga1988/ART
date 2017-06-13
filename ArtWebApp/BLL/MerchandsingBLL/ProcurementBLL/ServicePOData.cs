using ArtWebApp.DataModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace ArtWebApp.BLL.ProcurementBLL
{
    public class ServicePOData
    {
        public int ServicePo_PK { get; set; }
        public int ServiceType_PK { get; set; }
        public int CurrencyID { get; set; }
        public String  ServicePonum { get; set; }
        public String DebitFrom { get; set; }
        public String DebitName { get; set; }
        public String ServiceType { get; set; }
        public String Remark { get; set; }
        public Decimal  Amount { get; set; }
        public String Description { get; set; }
        public String AddedBy { get; set; }
        public DateTime AddedDate { get; set; }


        /// <summary>
        /// Load The DebitfromCombo based on user
        /// </summary>
        /// <param name="Debitfrom"></param>
        /// <param name="cmb_debit"></param>
        public void LoadCombo(String Debitfrom, CustomDropDown.DropDownListChosen cmb_debit)
        {


            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {

                if (Debitfrom == "Buyer")
                {

                    var PoQuery = from bmstr in enty.BuyerMasters
                                  where bmstr.BuyerName != ""
                                  select new
                                  {
                                      name = bmstr.BuyerName,
                                      pk = bmstr.BuyerID
                                  };
                    cmb_debit.DataSource = PoQuery.ToList();
                    cmb_debit.DataBind();
                }
                else if (Debitfrom == "ATC")
                {
                    var PoQuery = from atcmstr in enty.AtcMasters
                                  select new
                                  {
                                      name = atcmstr.AtcNum,
                                      pk = atcmstr.AtcId
                                  };
                    cmb_debit.DataSource = PoQuery.ToList();
                    cmb_debit.DataBind();
                }
                else if (Debitfrom == "Individual")
                {

                    var PoQuery = from atcmstr in enty.MerchandiserMasters
                                  select new
                                  {
                                      name = atcmstr.MerchandiserName,
                                      pk = atcmstr.MerChandiser_Pk
                                  };
                    cmb_debit.DataSource = PoQuery.ToList();
                    cmb_debit.DataBind();
                }

                else if (Debitfrom == "Factory")
                {

                    var PoQuery = from order in enty.LocationMasters
                            where order.LocType == "F"
                            select new
                            {
                                name = order.LocationName,
                                pk = order.Location_PK
                            };
                    cmb_debit.DataSource = PoQuery.ToList();
                    cmb_debit.DataBind();
                }



            }

            //showAllPoPackATC();
        }



        public void loadserviceTypeandCurrency(CustomDropDown.DropDownListChosen cmb_servicetype, CustomDropDown.DropDownListChosen cmb_currency)
        {
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {

                var PoQuery = from bmstr in enty.ServiceTypeMasters
                              select new
                              {
                                  name = bmstr.ServiceType,
                                  pk = bmstr.ServiceType_Pk
                              };


                var curquery= from bmstr in enty.CurrencyMasters
                              select new
                              {
                                  name = bmstr.CurrencyCode,
                                  pk = bmstr.CurrencyID
                              };
                cmb_servicetype.DataSource = PoQuery.ToList();
                cmb_servicetype.DataBind();



                cmb_currency.DataSource = curquery.ToList();
                cmb_currency.DataBind();
            }

        }

        /// <summary>
        /// Inser Service PO
        /// </summary>
        public String  insertServicePO(ServicePOData srvdata)
        {
            String Srpnum = "";
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {
               
                

                ServicePOMaster spmstr = new ServicePOMaster();

                spmstr.DebitFrom = srvdata.DebitFrom;
                spmstr.DebitName = srvdata.DebitName;
                spmstr.ServiceType_Pk = srvdata.ServiceType_PK;
                spmstr.Amount = srvdata.Amount;
                spmstr.ServicePOnumber = Srpnum;
                spmstr.CurrencyID = srvdata.CurrencyID;
                spmstr.Remark = srvdata.Remark;
                spmstr.Description = srvdata.Description;
                spmstr.AddedBy = HttpContext.Current.Session["Username"].ToString().Trim();
                spmstr.AddedDate = DateTime.Now;
                spmstr.IsApproved = "N";
                spmstr.IsDeleted = "N";
                enty.ServicePOMasters.Add(spmstr);


               
                
                enty.SaveChanges();
                spmstr.ServicePOnumber = Srpnum = "SR" + spmstr.ServicePO_PK;
                enty.SaveChanges();
                enty.SaveChanges();
            }

            return Srpnum;

        }

    }



    public class StockPOMasterdata
    {
     
        public int SPO_Pk { get; set; }
        public string SPONum { get; set; }
        public int AtcId { get; set; }
        public int Supplier_Pk { get; set; }
        public int DeliveryTerms_Pk { get; set; }
        public int DeliveryMethod_Pk { get; set; }
        public int PaymentTermID { get; set; }
        public int PaymentModeID { get; set; }
        public float PO_value { get; set; }
        public int Location_PK { get; set; }
        public int CurrencyID { get; set; }
        public string AddedBy { get; set; }
        public DateTime AddedDate { get; set; }
        public string Remark { get; set; }
        public string IsApproved { get; set; }
        public string IsDeleted { get; set; }
        public string DeletedBy { get; set; }
        public string ApprovedBy { get; set; }
        public DateTime DeletedDate { get; set; }
        public DateTime Approveddate { get; set; }
        public DateTime DeliveryDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="spodata"></param>
        /// <returns></returns>
        public String InsertSpoMasterData(StockPOMasterdata spodata)
        {
            string sPONUM = "";
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {
                StockPOMaster spomstr = new StockPOMaster();

                spomstr.Supplier_Pk = spodata.Supplier_Pk;
                
                spomstr.DeliveryDate = DateTime.Parse ( spodata.DeliveryDate.ToString ());
                spomstr.DeliveryTerms_Pk = spodata.DeliveryTerms_Pk;
                spomstr.DeliveryMethod_Pk = spodata.DeliveryMethod_Pk;
                spomstr.PaymentTermID = spodata.PaymentTermID;
                spomstr.PO_value = 0;
                spomstr.Location_PK = spodata.Location_PK;
                spomstr.CurrencyID = spodata.CurrencyID;
                spomstr.Remark = spodata.Remark;
                spomstr.AddedBy = HttpContext.Current.Session["Username"].ToString().Trim();
                spomstr.AddedDate = DateTime.Now;
                spomstr.IsApproved = "N";
                spomstr.IsDeleted = "N";

                spodata.SPONum = CreateSPOnum();
                spomstr.SPONum = spodata.SPONum;
                enty.StockPOMasters.Add(spomstr);
                enty.SaveChanges();
                //spodata.SPONum = CodeGenerator.GetUniqueCode("SPO", "", int.Parse(spomstr.SPO_Pk.ToString())); ;

                //enty.SaveChanges();

                HttpContext.Current.Session["spo_pk"] = spomstr.SPO_Pk;
                sPONUM = spodata.SPONum;
            }
            return sPONUM;

        }




        public String UpdateSpoMasterData(StockPOMasterdata spodata)
        {
            string sPONUM = "";
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {



                var q = from ponmbr in enty.StockPOMasters
                        where ponmbr.SPO_Pk == spodata.SPO_Pk
                        select ponmbr;



                foreach(var spomstr  in q)
                {
                    spomstr.Supplier_Pk = spodata.Supplier_Pk;

                    spomstr.DeliveryDate = DateTime.Parse(spodata.DeliveryDate.ToString());
                    spomstr.DeliveryTerms_Pk = spodata.DeliveryTerms_Pk;
                    spomstr.DeliveryMethod_Pk = spodata.DeliveryMethod_Pk;
                    spomstr.PaymentTermID = spodata.PaymentTermID;
                    spomstr.PO_value = 0;
                    spomstr.Location_PK = spodata.Location_PK;
                    spomstr.CurrencyID = spodata.CurrencyID;
                    spomstr.Remark = spodata.Remark;
                    spomstr.AddedBy = HttpContext.Current.Session["Username"].ToString().Trim();
                    spomstr.AddedDate = DateTime.Now;
                    spomstr.IsApproved = "N";
                    spomstr.IsDeleted = "N";

                  
                   // spomstr.SPONum = spodata.SPONum;
                }


               

               
               
                enty.SaveChanges();
                //spodata.SPONum = CodeGenerator.GetUniqueCode("SPO", "", int.Parse(spomstr.SPO_Pk.ToString())); ;

                //enty.SaveChanges();

                HttpContext.Current.Session["spo_pk"] = spodata.SPO_Pk;
                sPONUM = spodata.SPONum;
            }
            return sPONUM;

        }





        /// <summary>
        /// Create POnum
        /// </summary>
        /// <returns></returns>
        public String CreateSPOnum()
        {
            String atcnum = "";
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {
                var count = (from o in enty.StockPOMasters

                             select o).Count();


                atcnum = "SPO" + (int.Parse(count.ToString()) + 50000).ToString().PadLeft(6, '0');



            }

            return atcnum;
        }





        /// <summary>
        /// Approve the StockPO
        /// </summary>
        /// <param name="costingPK"></param>
        public void ApproveSPO(int spo_pk)
        {

            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {


                var datatoapprove = from pomstr in enty.StockPOMasters
                                    where pomstr.SPO_Pk == spo_pk
                                    select pomstr;

                foreach (var element123 in datatoapprove)
                {
                    element123.IsApproved = "Y";
                    element123.ApprovedBy = HttpContext.Current.Session["Username"].ToString();
                    element123.Approveddate = DateTime.Now;
                }

                enty.SaveChanges();
            }


        }



        public void ForwardforSPOApproval(int spo_pk)
        {

            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {


                var datatoapprove = from pomstr in enty.StockPOMasters
                                    where pomstr.SPO_Pk == spo_pk
                                    select pomstr;

                foreach (var element123 in datatoapprove)
                {
                    element123.IsSubmitted = "Y";
                    element123.ForwardedBy = HttpContext.Current.Session["Username"].ToString();
                }

                enty.SaveChanges();
            }


        }










    }


    public class StockPODetailsdata
    {

        public int SPODetails_PK { get; set; }
        public int SPO_PK { get; set; }
        public int Template_PK { get; set; }
        public string Composition { get; set; }
        public string Construct { get; set; }
        public string TemplateColor { get; set; }
        public string TemplateSize { get; set; }
        public string TemplateWidth { get; set; }
        public string TemplateWeight { get; set; }
        public Decimal Unitprice { get; set; }
        public Decimal POQty { get; set; }
        public int Uom_PK { get; set; }
        public int currencypk { get; set; }


        public int oodoPo_PK { get; set; }

        public int oodoPolineid { get; set; }
        public int GetCurrencyPK(int spo_pk)
        {
            int currencypk = 0;
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {
                var q = (from skudet in enty.StockPOMasters
                         where skudet.SPO_Pk == spo_pk
                         select skudet.CurrencyID).FirstOrDefault();

                currencypk = int.Parse(q.ToString());
            }

            return currencypk;
        }

        public DataTable GetSpoData(int spopk)
        {
            DBTransaction.ProcurementTransaction pktrans = new DBTransaction.ProcurementTransaction();
            return pktrans.GetSpoDetails(spopk);
        }


        public String InsertSpoDetails(StockPODetailsdata detdata)
        {
            string sPONUM = "";
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {
                StockPODetail spodetal = new StockPODetail();
                spodetal.SPO_PK = detdata.SPO_PK;
                spodetal.Template_PK = detdata.Template_PK;
                spodetal.Composition = detdata.Composition;
                spodetal.Construct = detdata.Construct;
                spodetal.TemplateColor = detdata.TemplateColor;
                spodetal.TemplateSize = detdata.TemplateSize;
                spodetal.TemplateWidth = detdata.TemplateWidth;
                spodetal.TemplateWeight = detdata.TemplateWeight;
                spodetal.Unitprice = detdata.Unitprice;
                spodetal.POQty = detdata.POQty;
                spodetal.Uom_PK = detdata.Uom_PK;
                int currencypk = GetCurrencyPK(int.Parse(detdata.SPO_PK.ToString()));
                spodetal.CUrate = Controls.currencyConvertor.converttousd(currencypk, detdata.Unitprice);
                enty.StockPODetails.Add(spodetal);              
                enty.SaveChanges();


                var supliername = enty.StockPOMasters.Where(u => u.SPO_Pk == detdata.SPO_PK).Select(u => u.SupplierMaster.SupplierName).FirstOrDefault();

                var OdooLocation = enty.ODOOGPOMasters.Where(u => u.POLineID == detdata.oodoPolineid).Select(u => u.OdooLocation).FirstOrDefault();

                

                StocPOForODOO spoforpo = new StocPOForODOO();
                spoforpo.SPoDet_PK = spodetal.SPODetails_PK;
                spoforpo.Spo_PK = spodetal.SPO_PK;
                spoforpo.CuRate = spodetal.CUrate;
                spoforpo.POId = detdata.oodoPo_PK;
                spoforpo.POLineID = detdata.oodoPolineid;
                spoforpo.CuRate = spodetal.CUrate;
                spoforpo.POQty = spodetal.POQty;
                spoforpo.PoDate = DateTime.Now.Date;
                spoforpo.NewPOId = 0;
                spoforpo.NewPOLineId = 0;
                spoforpo.SupplierName = supliername.ToString();
                spoforpo.OdooLocation = OdooLocation.ToString();
                enty.StocPOForODOOs.Add(spoforpo);

                //try
                //{
                //    if (detdata.oodoPolineid != 0)
                //    {

                //        stock
                //        var q = from oodopo in enty.ODOOGPOMasters
                //                where oodopo.POLineID == detdata.oodoPolineid
                //                select oodopo;
                //        foreach (var element in q)
                //        {
                //            element.SpoDet_PK = spodetal.SPODetails_PK;
                //            element.SPO_PK = spodetal.SPO_PK;
                //            element.CuRate = spodetal.CUrate;
                //            element.Iscompleted = "Y";
                //        }
                //    }
                //}
                //catch (Exception)
                //{


                //}


                enty.SaveChanges();

            }
            return sPONUM;

        }



        public string GetBalanceofIR(int poid,int polineid)
        {
            float balance = 0;

            float irqtytemp = 0;
            float poqtytemp = 0;
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {




                try
                {
                    var Irqty = enty.ODOOGPOMasters.Where(u => u.POId == poid && u.POLineID == polineid).Select(u => u.Qty ?? 0).Sum();
                    irqtytemp = float.Parse(Irqty.ToString());
                }
                catch (Exception)
                {

                    irqtytemp = 0;
                }
                try
                {
                    var POqty = enty.StocPOForODOOs.Where(u => u.POId == poid && u.POLineID == polineid).Select(u => u.POQty ?? 0).Sum();
                    poqtytemp = float.Parse(POqty.ToString());
                }
                catch (Exception)
                {

                    poqtytemp = 0;
                }


                balance = (irqtytemp - poqtytemp);
            }


            return balance.ToString();

        }


        public String DeleteSpoDetailsPK(int spoDetpk)
        {
            String msg = "";
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {

                if(isSMRMade(spoDetpk))
                {
                    msg = "Cannot Delete Details Since SMRN Made";
                }
                else if(isSPODataPulled(spoDetpk))
                {
                    msg = "Cannot Pull Data as Data Already updated in Odoo";
                }
                else
                {
                    Deletespodetail(spoDetpk);
                    msg = "Spodetails Deleted";
                }
               

            }

            return msg;
        }




        /// <summary>
        /// check whether mrn  is made  for a spodetpk
        /// </summary>
        /// <param name="skudetPK"></param>
        /// <returns></returns>
        public Boolean isSMRMade(int spoDetpk)
        {
            Boolean ismrnmade = false;
            using (ArtEntitiesnew entty = new ArtEntitiesnew())
            {
                if (!entty.StockMRNDetails.Any(f => f.SPODetails_PK == spoDetpk))
                {
                    ismrnmade = false;
                }
                else
                {
                    ismrnmade = true;
                }

            }
            return ismrnmade;
        }


        public Boolean isSPODataPulled(int spoDetpk)
        {
            Boolean ispulled = true;
            using (ArtEntitiesnew entty = new ArtEntitiesnew())
            {
                if (!entty.StocPOForODOOs.Any(f => f.SPoDet_PK == spoDetpk  && f.NewPOLineId !=0))
                {
                    ispulled = false;
                }
             
            }
            return ispulled;
        }



        public void Deletespodetail(int spoDetpk)
        {
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {

                var q = from ponmbr in enty.StockPODetails
                        where ponmbr.SPODetails_PK == spoDetpk
                        select ponmbr;


                foreach (var element in q)
                {
                    enty.StockPODetails.Remove(element);
                   
                }

                var q1 = from ponmbrs in enty.StocPOForODOOs
                        where ponmbrs.SPoDet_PK == spoDetpk
                        select ponmbrs;


                foreach (var element in q1)
                {
                    enty.StocPOForODOOs.Remove(element);

                }


                enty.SaveChanges();
            }
        }






    }




}
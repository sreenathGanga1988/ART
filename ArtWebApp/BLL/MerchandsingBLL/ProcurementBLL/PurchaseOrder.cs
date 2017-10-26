using ArtWebApp.DataModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArtWebApp.BLL.MerchandsingBLL.ProcurementBLL
{
    public class PurchaseOrder
    {
    }

    public class ProcurementDetails
    {
        public int PODet_PK { get; set; }
        public int PO_Pk { get; set; }
        public int SkuDet_PK { get; set; }
        public decimal POQty { get; set; }
        public float POUnitRate { get; set; }
        public string SupplierSize { get; set; }
        public string SupplierColor { get; set; }
        public int Uom_PK { get; set; }
        public float CURate { get; set; }

    }


    public class ProcurementMasterData
    {
        DBTransaction.ProcurementTransaction potrans = new DBTransaction.ProcurementTransaction();
        public ProcurementMasterData()
        {
            potrans = new DBTransaction.ProcurementTransaction();
        }
        public int PO_Pk { get; set; }
        public string PONum { get; set; }
        public int AtcId { get; set; }
        public int Supplier_Pk { get; set; }
        public int DeliveryTerms_Pk { get; set; }
        public int DeliveryMethod_Pk { get; set; }
        public int PaymentTermID { get; set; }
        public int PaymentModeID { get; set; }
        public float PO_value { get; set; }
        public int Location_PK { get; set; }
        public int CurrencyID { get; set; }


        public string PoType { get; set; }
        public string AddedBy { get; set; }
        public DateTime AddedDate { get; set; }
        public string Remark { get; set; }
        public string IsApproved { get; set; }
        public string IsNormal { get; set; }
        public string IsDeleted { get; set; }
        public string DeletedBy { get; set; }
        public string ApprovedBy { get; set; }
        public DateTime DeletedDate { get; set; }
        public DateTime Approveddate { get; set; }
        public DateTime Deliverydate { get; set; }
        public string FreightType { get; set; }
        public decimal freightcharge { get; set; }
        public List<ProcurementDetails> ProcurementDetailsCollection { get; set; }

        public DataTable POdetails { get; set; }

        /// <summary>
        /// Insert Po
        /// </summary>
        /// <param name="Pmmstr"></param>
        /// <returns></returns>
        public String insertPoData(ProcurementMasterData Pmmstr)
        {
            String ponum = "";

            using (ArtEntitiesnew entty = new ArtEntitiesnew())
            {
                using (var dbContextTransaction = entty.Database.BeginTransaction())
                {
                    try
                    {


                        try
                        {
                            ProcurementMaster POmstr = new ProcurementMaster();
                            POmstr.Supplier_Pk = Pmmstr.Supplier_Pk;
                            POmstr.DeliveryTerms_Pk = Pmmstr.DeliveryTerms_Pk;
                            POmstr.PaymentTermID = Pmmstr.PaymentTermID;
                            POmstr.DeliveryMethod_Pk = Pmmstr.DeliveryMethod_Pk;
                            POmstr.CurrencyID = Pmmstr.CurrencyID;
                            POmstr.Location_PK = Pmmstr.Location_PK;
                            POmstr.AtcId = Pmmstr.AtcId;
                            POmstr.AddedBy = Pmmstr.AddedBy;
                            POmstr.AddedDate = DateTime.Now;
                            POmstr.IsApproved = "N";
                            POmstr.IsDeleted = "N";
                            POmstr.IsNormal = Pmmstr.IsNormal;
                            POmstr.POType = Pmmstr.PoType;
                            POmstr.DeliveryDate = Pmmstr.Deliverydate;
                            POmstr.Remark = Pmmstr.Remark;
                            POmstr.IsNormal = "Y";
                            POmstr.FreightCharge = Pmmstr.freightcharge;
                            POmstr.FreightType = Pmmstr.FreightType;
                            POmstr.PONum = CreatePOnum();
                            entty.ProcurementMasters.Add(POmstr);



                            entty.SaveChanges();


                            //   ponum = CodeGenerator.GetUniqueCode("APO", HttpContext.Current.Session["lOC_Code"].ToString().Trim(), int.Parse(POmstr.PO_Pk.ToString())); ;
                            // POmstr.PONum = ponum;

                            foreach (ProcurementDetails rdet in Pmmstr.ProcurementDetailsCollection)
                            {

                                ProcurementDetail pddetails = new ProcurementDetail();
                                pddetails.SkuDet_PK = rdet.SkuDet_PK;
                                pddetails.POQty = rdet.POQty;
                                pddetails.POUnitRate = Convert.ToDecimal(rdet.POUnitRate);
                                pddetails.SupplierColor = rdet.SupplierColor;
                                pddetails.SupplierSize = rdet.SupplierSize;
                                pddetails.PO_Pk = POmstr.PO_Pk;
                                pddetails.Uom_PK = rdet.Uom_PK;
                                pddetails.CURate = Convert.ToDecimal(rdet.CURate);
                                entty.ProcurementDetails.Add(pddetails);
                            }


                            ponum = POmstr.PONum;

                        }
                        catch (Exception EXP)
                        {

                          
                        }



                        entty.SaveChanges();






                        dbContextTransaction.Commit();

                    }
                    catch (Exception)
                    {
                        dbContextTransaction.Rollback();
                    }
                }
            }
            return ponum;

        }






       
        public String AddnewContenttoPO(ProcurementMasterData Pmmstr)
        {
            String ponum = "";

            using (ArtEntitiesnew entty = new ArtEntitiesnew())
            {
                using (var dbContextTransaction = entty.Database.BeginTransaction())
                {
                    try
                    {


                        try
                        {


                            //   ponum = CodeGenerator.GetUniqueCode("APO", HttpContext.Current.Session["lOC_Code"].ToString().Trim(), int.Parse(POmstr.PO_Pk.ToString())); ;
                            // POmstr.PONum = ponum;

                            foreach (ProcurementDetails rdet in Pmmstr.ProcurementDetailsCollection)
                            {

                                ProcurementDetail pddetails = new ProcurementDetail();
                                pddetails.SkuDet_PK = rdet.SkuDet_PK;
                                pddetails.POQty = rdet.POQty;
                                pddetails.POUnitRate = Convert.ToDecimal(rdet.POUnitRate);
                                pddetails.SupplierColor = rdet.SupplierColor;
                                pddetails.SupplierSize = rdet.SupplierSize;
                                pddetails.PO_Pk = Pmmstr.PO_Pk;
                                pddetails.Uom_PK = rdet.Uom_PK;
                                pddetails.CURate = Convert.ToDecimal(rdet.CURate);
                                entty.ProcurementDetails.Add(pddetails);
                            }


                            

                        }
                        catch (Exception)
                        {


                        }



                        entty.SaveChanges();






                        dbContextTransaction.Commit();

                    }
                    catch (Exception)
                    {
                        dbContextTransaction.Rollback();
                    }
                }
            }
            return ponum;

        }







        /// <summary>
        /// Create POnum
        /// </summary>
        /// <returns></returns>
        public String CreatePOnum()
        {
            String atcnum = "";
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {
                var count = (from o in enty.ProcurementMasters

                             select o).Count();


                atcnum = "PO" + (int.Parse(count.ToString()) + 50000).ToString().PadLeft(6, '0');



            }

            return atcnum;
        }

        public float UnitRatecalculator(float cuunitrate, String CurrencyCode, int CurrencyPk)
        {
            float unitrateinpocurrecncy = 0;


            float convfact = 0;
            potrans = new DBTransaction.ProcurementTransaction();
            if (CurrencyCode.Trim() == "USD")
            {
                convfact = 1;
            }
            else
            {
                convfact = potrans.Getconversionfact(CurrencyPk);
            }



            unitrateinpocurrecncy = cuunitrate * convfact;



            return unitrateinpocurrecncy;

        }
        /// <summary>
        /// convert the qnty in base UOm to the Qty in Alt UOm
        /// </summary>
        /// <param name="uomPK"></param>
        /// <param name="auomPk"></param>
        /// <param name="balqtyinBaseuom"></param>
        /// <returns></returns>
        public float UOMConvertortoAlt(int uomPK, int auomPk, float balqtyinBaseuom)
        {

            float converttobaseqty = 0;
            float operend = 1;
            String operatorused = "*";

            DataTable dt = potrans.getAltuomdata(uomPK, auomPk);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {

                    operend = float.Parse(dt.Rows[0]["Conv_fact"].ToString());
                    operatorused = dt.Rows[0]["Operator"].ToString().Trim();
                    if (operatorused == "*")
                    {

                        converttobaseqty = balqtyinBaseuom / operend;
                    }
                    else if (operatorused == "/")
                    {
                        converttobaseqty = balqtyinBaseuom * operend;
                    }
                }





            }
            return converttobaseqty;


        }


        /// <summary>
        /// Convert the unitprice from BaseUOM to AltUOM
        /// </summary>
        /// <param name="uomPK"></param>
        /// <param name="auomPk"></param>
        /// <param name="unitpriceinUSD"></param>
        /// <returns></returns>
        public float ConvertUnitPriceFromBAseToAlt(int uomPK, int auomPk, float unitpriceinUSD)
        {

            float Newcuuintprice = 0;
            float operend = 1;
            String operatorused = "*";

            DataTable dt = potrans.getAltuomdata(uomPK, auomPk);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {

                    operend = float.Parse(dt.Rows[0]["Conv_fact"].ToString());
                    operatorused = dt.Rows[0]["Operator"].ToString().Trim();
                    if (operatorused == "*")
                    {

                        Newcuuintprice = unitpriceinUSD * operend;
                    }
                    else if (operatorused == "/")
                    {
                        Newcuuintprice = unitpriceinUSD / operend;
                    }
                }





            }
            return Newcuuintprice;


        }


        public float Getconversionfact(int currencyid)
        {
            float conv = 1;
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {
                enty.Configuration.AutoDetectChangesEnabled = false;
                var CONVFACT = enty.POCurrExRates.Where(u => u.CurrencyID == currencyid).Select(u => u.Convrate).FirstOrDefault();
                if (CONVFACT != null)
                {
                    conv = float.Parse(CONVFACT.ToString());
                }
                else
                {
                    conv = 0;
                }
            }
            return conv;
        }



        public ArrayList ConvertCurrencyAndUOM(int uomPK, int auomPk, float balqtyinBaseuom, float cuunitrate, String CurrencyCode, int CurrencyPk)
        {

            ArrayList QTYandPricelist = new ArrayList();




            float unitrateinpocurrecncy = 0;


            float convfact = 0;
           
            if (CurrencyCode.Trim() == "USD")
            {
                convfact = 1;
            }
            else
            {
                convfact =Getconversionfact(CurrencyPk);
            }



            unitrateinpocurrecncy = cuunitrate * convfact; //calculate the baseUOmsUnitprice






            float converttobaseqty = 0;
            float operendforUOM = 1;
            String operatorusedforUOM = "*";

            if (uomPK == auomPk)
            {
                operendforUOM = 1;
                operatorusedforUOM = "*";

                converttobaseqty = balqtyinBaseuom;

            }
            else
            {
                DataTable dt = potrans.getAltuomdata(uomPK, auomPk);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {

                        operendforUOM = float.Parse(dt.Rows[0]["Conv_fact"].ToString());
                        operatorusedforUOM = dt.Rows[0]["Operator"].ToString().Trim();
                        if (operatorusedforUOM == "*")
                        {

                            converttobaseqty = balqtyinBaseuom * operendforUOM;

                            unitrateinpocurrecncy = unitrateinpocurrecncy / operendforUOM;
                        }
                        else if (operatorusedforUOM == "/")
                        {
                            converttobaseqty = balqtyinBaseuom / operendforUOM;

                            unitrateinpocurrecncy = unitrateinpocurrecncy * operendforUOM;
                        }
                    }
                }


            }





            QTYandPricelist.Add(Math.Ceiling(converttobaseqty));

            QTYandPricelist.Add(unitrateinpocurrecncy);







            return QTYandPricelist;


        }

        public ProcurementMasterData GetPodata(int popk)
        {



            ProcurementMasterData POmstr = new ProcurementMasterData();
            using (ArtEntitiesnew entty = new ArtEntitiesnew())
            {


                var q = from prodmstr in entty.ProcurementMasters
                        where prodmstr.PO_Pk == popk
                        select prodmstr;

                foreach (var element in q)
                {

                    POmstr.Supplier_Pk = int.Parse(element.Supplier_Pk.ToString());
                    POmstr.DeliveryTerms_Pk = int.Parse(element.DeliveryTerms_Pk.ToString());
                    POmstr.PaymentTermID = int.Parse(element.PaymentTermID.ToString());
                    POmstr.DeliveryMethod_Pk = int.Parse(element.DeliveryMethod_Pk.ToString());
                    POmstr.CurrencyID = int.Parse(element.CurrencyID.ToString());
                    POmstr.Location_PK = int.Parse(element.Location_PK.ToString());
                    POmstr.AtcId = int.Parse(element.AtcId.ToString());
                    POmstr.IsApproved = element.IsApproved;
                    POmstr.IsDeleted = element.IsDeleted;
                    POmstr.PoType = element.POType;
                    POmstr.Deliverydate = DateTime.Parse(element.DeliveryDate.ToString());
                    POmstr.PONum = element.PONum;
                    POmstr.Remark = element.Remark;
                    POmstr.freightcharge =Decimal.Parse( element.FreightCharge.ToString());
                    POmstr.FreightType = element.FreightType;






                }
                potrans = new DBTransaction.ProcurementTransaction();
                POmstr.POdetails = potrans.GetPODetailsData(popk);

            }

            return POmstr;

        }


        public void updateProcurementData(ProcurementMasterData PMdata)
        {
            using (ArtEntitiesnew entty = new ArtEntitiesnew())
            {


                var q = from prodmstr in entty.ProcurementMasters
                        where prodmstr.PO_Pk == PMdata.PO_Pk
                        select prodmstr;

                foreach (var POmstr in q)
                {

                    POmstr.Supplier_Pk = PMdata.Supplier_Pk;
                    POmstr.DeliveryTerms_Pk = PMdata.DeliveryTerms_Pk;
                    POmstr.PaymentTermID = PMdata.PaymentTermID;
                    POmstr.DeliveryMethod_Pk = PMdata.DeliveryMethod_Pk;
                    POmstr.CurrencyID = PMdata.CurrencyID;
                    POmstr.Location_PK = PMdata.Location_PK;
                    POmstr.AddedBy = PMdata.AddedBy;
                    POmstr.AddedDate = DateTime.Now;
                    POmstr.IsApproved = "N";
                    POmstr.IsDeleted = "N";
                    POmstr.POType = PMdata.PoType;
                    POmstr.DeliveryDate = PMdata.Deliverydate;
                    POmstr.Remark = PMdata.Remark;
                    POmstr.FreightCharge = PMdata.freightcharge;
                    POmstr.FreightType = PMdata.FreightType;




                }

                foreach (ProcurementDetails rdet in PMdata.ProcurementDetailsCollection)
                {
                    var q1 = from proddet in entty.ProcurementDetails
                             where proddet.PODet_PK == rdet.PODet_PK
                             select proddet;
                    foreach (var pddetails in q1)
                    {
                        pddetails.POQty = rdet.POQty;
                        pddetails.SupplierColor = rdet.SupplierColor;
                        pddetails.SupplierSize = rdet.SupplierSize;
                        

                        if(pddetails.POUnitRate!=Convert.ToDecimal( rdet.POUnitRate))
                        {
                            pddetails.POUnitRate = Convert.ToDecimal(rdet.POUnitRate);
                            pddetails.CURate =Convert.ToDecimal( rdet.CURate);
                        }

                    }
                   
                }


                entty.SaveChanges();

            }
        }



        public String DeletePODetailsPK(int poDetpk)
        {
            String msg = "";
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {

                if (isMRMade(poDetpk))
                {
                    msg = "Cannot Delete Details Since SMRN Made";
                }
                else
                {
                    DeletePodetail(poDetpk);
                    msg = "Spodetails Deleted";
                }


            }

            return msg;
        }




        /// <summary>
        /// check whether mrn  is made  for a podetpk
        /// </summary>
        /// <param name="skudetPK"></param>
        /// <returns></returns>
        public Boolean isMRMade(int poDetpk)
        {
            Boolean ismrnmade = false;
            using (ArtEntitiesnew entty = new ArtEntitiesnew())
            {
                if (!entty.MrnDetails.Any(f => f.PODet_PK == poDetpk))
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


        public void DeletePodetail(int poDetpk)
        {
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {

                var q = from ponmbr in enty.ProcurementDetails
                        where ponmbr.PODet_PK == poDetpk
                        select ponmbr;


                foreach (var element in q)
                {
                    enty.ProcurementDetails.Remove(element);
                  
                }
                enty.SaveChanges();
            }
        }





        #region trial



        public void DoUOMAndCurrencyConversionConversion(System.Web.UI.WebControls.GridViewRow currentRow ,int CurrencyPk, String CurrencyCode)
        {
           
           
            float cuunitrate = float.Parse((currentRow.FindControl("lbl_costunitrate") as Label).Text.ToString());
            int baseuompk = int.Parse((currentRow.FindControl("lbl_uomPK") as Label).Text.ToString());
            float basebalanceqty = float.Parse((currentRow.FindControl("lbl_balqty") as Label).Text.ToString());


            //int CurrencyPk = 18;
            //String CurrencyCode = "USD";
            //try
            //{
            //    CurrencyPk = int.Parse(drp_currency.SelectedValue.ToString());
            //    CurrencyCode = drp_currency.SelectedItem.Text.ToString();
            //}
            //catch (Exception)
            //{

            //    updateStatus("Select Currency");
            //}


            int altuompk = 0;
            try
            {
                altuompk = int.Parse((currentRow.FindControl("ddl_AltUOM") as DropDownList).SelectedValue.ToString());
            }
            catch (Exception)
            {

                altuompk = baseuompk;
            }


            ArrayList QtyandPrice = ConvertCurrencyAndUOM(baseuompk, altuompk, basebalanceqty, cuunitrate, CurrencyCode, CurrencyPk);


            (currentRow.FindControl("lbl_BalQtyinALTUOM") as Label).Text = QtyandPrice[0].ToString();


            (currentRow.FindControl("lbl_supunitrate") as Label).Text = QtyandPrice[1].ToString();


            //(currentRow.FindControl("txt_unitrate") as TextBox).Text = QtyandPrice[1].ToString();
            //(currentRow.FindControl("txt_poQty") as TextBox).Text = QtyandPrice[0].ToString();








            

         //   updateRowUpdatepanels(currentRow);
            //  upd_detail.Update();

        }

        public void updateRowUpdatepanels(GridViewRow currentRow)
        {
            UpdatePanel upd_lbl_supunitrate = (currentRow.FindControl("upd_lbl_supunitrate") as UpdatePanel);
            UpdatePanel Upd_txt_unitrate = (currentRow.FindControl("Upd_txt_unitrate") as UpdatePanel);
            UpdatePanel Upd_lbl_BalQtyinALTUOM = (currentRow.FindControl("Upd_lbl_BalQtyinALTUOM") as UpdatePanel);

            UpdatePanel Upd_txt_poQty = (currentRow.FindControl("Upd_txt_poQty") as UpdatePanel);



            upd_lbl_supunitrate.Update();
            Upd_txt_unitrate.Update();
            Upd_lbl_BalQtyinALTUOM.Update();
            Upd_txt_poQty.Update();


        }

        #endregion







        public int GetSupplierPaymentFixed(int supplierpk)



        {
            int paymenttermid = 0;

            try
            {

                using (ArtEntitiesnew enty = new ArtEntitiesnew())
                {

                    var q = from ponmbr in enty.SupplierMasters
                            where ponmbr.Supplier_PK == supplierpk
                            select ponmbr;


                    foreach (var element in q)
                    {
                        if (element.IsFixedPaymentTermID == true)
                        {
                            paymenttermid = int.Parse(element.PaymentTermID.ToString());
                        }

                    }
                }


            }
            catch (Exception)
            {

                paymenttermid = 0;
            }
            return paymenttermid;
        }





    }
}
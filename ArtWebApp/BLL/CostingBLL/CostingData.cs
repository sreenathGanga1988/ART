using ArtWebApp.DataModels;
using ArtWebApp.DBTransaction;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ArtWebApp.BLL.CostingBLL
{
    public class CostingData
    {
    }

    public class StyleCostingMaster
    {
        public int Costing_PK { get; set; }
        public int OurStyleID { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string IsApproved { get; set; }
        public string ApprovedBy { get; set; }
        public DateTime ApprovedDate { get; set; }
        public int CostingCount { get; set; }
        public string IsApplicable { get; set; }


        public ArrayList styleCompdet;


        public DataTable stylecombdata;

        public DataTable Stylecombdata
        {
            get { return stylecombdata; }
            set { stylecombdata = value; }
        }


        public int InsertCosting(int ourstyleid, int atcid)
        {
            int costid = 0;
            CostingTransaction csttrans = new CostingTransaction();
            costid = csttrans.insertcostingmaster(ourstyleid);
            return costid;

        }







        public int CopyCostingFromOneStyletoAnother(int fromstyle, int tostyle)
        {

            int newcostingid = 0;
            int oldstylecostingid=0;
            CostingTransaction csttrans = new CostingTransaction();

            // get the new costing id
            newcostingid = csttrans.insertcostingmaster(tostyle);

            // get the last approved Costing Data
            oldstylecostingid = csttrans.getLastApprovedCostingofAstyle(fromstyle);


            //Copy All the StylerawMaterial Details
            csttrans.CopyCostingFromOneStyletoAnother(oldstylecostingid, newcostingid);

            //Copy All the StyleCotingComponents
            csttrans.CopyCoMponentsOFOneCostingtoAnother(oldstylecostingid, newcostingid);

            //Updated the FOB and margin etc
            csttrans.UpdateCostingmasterData(newcostingid);




            return newcostingid;


        }

        /// <summary>
        /// Get the costing with islast="Y"
        /// </summary>
        /// <param name="fromstyle"></param>
        /// <returns></returns>
        public int getlastcostingid(int fromstyle)
        {
            int costingid = 0;
            CostingTransaction csttrans = new CostingTransaction();

            costingid =csttrans. GetLastCosting(fromstyle);

            return costingid;
        }








        /// <summary>
        /// Inserting Costing Components
        /// </summary>
        /// <param name="cmstr"></param>
        public void InsertCostingComponent(StyleCostingMaster cmstr)
        {

            CostingTransaction csttrans = new CostingTransaction();

            csttrans.InsertCostingComponents(cmstr);

            UpdateCostingmasterData(cmstr.Costing_PK);        


          
        }





        public void UpdateCostingmasterData(int costingID)
        {
            DBTransaction.CostingTransaction cstrans = new CostingTransaction();
            cstrans.UpdateCostingmasterData(costingID);
        }

        /// <summary>
        /// submit the costing for approval
        /// </summary>
        /// <param name="costingID"></param>
        public void SubmitCostingForApproval(int costingID)
        {
            int ourstylid = 0;
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {
                var q = from cst in enty.StyleCostingMasters
                        where cst.Costing_PK == costingID
                        select cst;
                foreach (var element in q )
                {
                    element.IsSubmitted= "Y";
                    ourstylid = int.Parse(element.OurStyleID.ToString());
                    try
                    {
                        if (isManagementApprovalrequired(ourstylid, costingID))
                        {
                            element.IsLocalApproval = "N";
                        }
                        else
                        {
                            element.IsLocalApproval = "Y";
                        }
                    }
                    catch (Exception)
                    {

                       
                    }

                }
                enty.SaveChanges();
                
               

            }
        }


         public Boolean isManagementApprovalrequired( int ourstyleid ,int costingpk )
        {
            CostingTransaction csttrans = new CostingTransaction();

            Boolean isapprovalrequired = false;



            DataTable newCostingdata = csttrans.GetCostingComponentofPK(costingpk);

            DataTable ApprovedCostingdata = csttrans.GetApprovedcostingComponentofStyle(ourstyleid);




            if (ApprovedCostingdata != null)
            {
                if (ApprovedCostingdata.Rows.Count > 0)
                {

                    if (newCostingdata != null)
                    {
                        if (newCostingdata.Rows.Count > 0)
                        {


                            if (ApprovedCostingdata.Rows [0]["CM"].ToString ().Trim  ()!= newCostingdata.Rows[0]["CM"].ToString().Trim())
                            {
                                isapprovalrequired = true;
                            }
                            else if (ApprovedCostingdata.Rows[0]["WASH"].ToString().Trim() != newCostingdata.Rows[0]["WASH"].ToString().Trim())
                            {
                                isapprovalrequired = true;
                            }
                            else if (ApprovedCostingdata.Rows[0]["DRYPROCESS"].ToString().Trim() != newCostingdata.Rows[0]["DRYPROCESS"].ToString().Trim())
                            {
                                isapprovalrequired = true;
                            }
                            else if (ApprovedCostingdata.Rows[0]["EMBROIDERY"].ToString().Trim() != newCostingdata.Rows[0]["EMBROIDERY"].ToString().Trim())
                            {
                                isapprovalrequired = true;
                            }
                            else if (ApprovedCostingdata.Rows[0]["FABCOMMISION"].ToString().Trim() != newCostingdata.Rows[0]["FABCOMMISION"].ToString().Trim())
                            {

                                isapprovalrequired = true;
                            }
                            else if (ApprovedCostingdata.Rows[0]["GARMENTCOMMMISION"].ToString().Trim() != newCostingdata.Rows[0]["GARMENTCOMMMISION"].ToString().Trim())
                            {
                                isapprovalrequired = true;
                            }
                            else if (ApprovedCostingdata.Rows[0]["COMPANYLOGISTICS"].ToString().Trim() != newCostingdata.Rows[0]["COMPANYLOGISTICS"].ToString().Trim())
                            {
                                isapprovalrequired = true;
                            }
                            else if (ApprovedCostingdata.Rows[0]["FACTORYLOGISTICS"].ToString().Trim() != newCostingdata.Rows[0]["FACTORYLOGISTICS"].ToString().Trim())
                            {
                                isapprovalrequired = true;
                            }
                            else if (ApprovedCostingdata.Rows[0]["OTHERS"].ToString().Trim() != newCostingdata.Rows[0]["OTHERS"].ToString().Trim())
                            {
                                isapprovalrequired = true;
                            }
                            else if (ApprovedCostingdata.Rows[0]["FAB"].ToString().Trim() != newCostingdata.Rows[0]["FAB"].ToString().Trim())
                            {
                                isapprovalrequired = true;
                            }
                            else if (ApprovedCostingdata.Rows[0]["TRIMS"].ToString().Trim() != newCostingdata.Rows[0]["TRIMS"].ToString().Trim())
                            {
                                isapprovalrequired = true;
                            }
                            else
                            {
                                isapprovalrequired = false;
                            }


                        }



                    }

                }
            }














            return isapprovalrequired;
        }


    }
    public class StyleCostingDetails
        {
            public int CostingDetail_PK { get; set; }
            public int Costing_PK { get; set; }
            public int Sku_PK { get; set; }
            public int Consumption { get; set; }
            public int Rate { get; set; }
            public string IsRequired { get; set; }
            public int PriceperDozen { get; set; }



        }
    public class StyleCostingComponentDetails
        {
            public int StyleCostingCom_Pk { get; set; }
            public int CostComp_PK { get; set; }
            public decimal CompValue { get; set; }
            public DateTime AddedDate { get; set; }
            public string AddedBy { get; set; }
            public int Costing_PK { get; set; }

            public string CalculationMode { get; set; }




            public void InsertMandatoryComponentvalue(int costingId,int ourstyleid)
            {
                CostingTransaction csttrans = new CostingTransaction();
                DataTable dtcomp = csttrans.GetManadatoryCostingComponents();
                ArrayList fabcost = csttrans.GetFabTrimCost(costingId);//First is fabric cost and Second is 


                using (ArtEntitiesnew enty = new ArtEntitiesnew())
                {


                    var q = from stylecom in enty.CostingComponentMasters
                            where stylecom.IsOptional == "N"
                            select stylecom;
                    foreach ( var element in q )
                    {
                        float compvalue = 0;

                        if(element.ComponentName.ToString().Trim ()=="FAB")
                        {
                            compvalue = (float.Parse(fabcost[0].ToString()) / 12);
                        }
                        else if (element.ComponentName.ToString().Trim() == "TRIMS")
                        {
                            compvalue = (float.Parse(fabcost[1].ToString()) / 12);
                        }
                        else
                        {

                            compvalue = csttrans.getCM(ourstyleid);
                        }



                        StyleCostingComponentDetail det = new StyleCostingComponentDetail();
                        det.Costing_PK = costingId;
                        det.CostComp_PK = element.CostComp_PK;
                        det.CompValue = decimal.Parse( compvalue.ToString ());
                        det.CalculationMode = element.CalculationType.ToString().Trim();

                        enty.StyleCostingComponentDetails.Add(det);

                    }

                    enty.SaveChanges();
                }

                

            }


        }

    

}
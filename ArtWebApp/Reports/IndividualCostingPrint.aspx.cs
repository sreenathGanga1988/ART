using ArtWebApp.DataModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArtWebApp.Reports
{
    public partial class IndividualCostingPrint : System.Web.UI.Page
    {
        DBTransaction.CostingTransaction csttran = null;
        protected void Page_Load(object sender, EventArgs e)
        {


            if (!IsPostBack)
            {
                String costpk = Session["printcostpk"].ToString ();
                int costingid = int.Parse(costpk);

                
                



                try
                {
                    fillatcdetails(costingid);
                }
                catch (Exception)
                {
                }


                 try
                {
                    getFirstCostingDetails(costingid);
                }
                catch (Exception)
                {
                }




                try
                {
                    getcostingdetails(costingid);
                }
                catch (Exception)
                {


                }
                try
                {
                    getoucomponents(costingid);
                }
                catch (Exception)
                {



                }


                try
                {
                    calculateTheDZNvalues();
                }
                catch (Exception)
                {



                }







            }
        }




        /// <summary>
        /// get the details of Atc and Styles
        /// </summary>
        /// <param name="costing_pk"></param>
        public void fillatcdetails(int costing_pk)
        {
            csttran = new DBTransaction.CostingTransaction();
            DataTable dt = csttran.GetATCDetailsofCosting(costing_pk);
            if (dt != null)
            {

                if (dt.Rows.Count >= 1)
                {


                    lbl_atc.Text = dt.Rows[0]["Atcnum"].ToString().Trim();
                    lbl_oursrtyle.Text = dt.Rows[0]["OurStyle"].ToString().Trim();
                    lbl_buyerstyle.Text = dt.Rows[0]["BuyerStyle"].ToString().Trim();
                    lbl_qty.Text = dt.Rows[0]["Qty"].ToString().Trim();

                }
            }
        }






        /// <summary>
        /// Components
        /// </summary>
        /// <param name="costingid"></param>

        public void getoucomponents(int costingid)
        {
            float commision = 0;
            float others = 0;
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {
                var ourstyleidvar = from stcompdet in enty.StyleCostingComponentDetails
                                    join compmstr in enty.CostingComponentMasters on stcompdet.CostComp_PK equals compmstr.CostComp_PK
                                    where stcompdet.Costing_PK == costingid
                                    select new { compmstr.ComponentName, stcompdet.CompValue, stcompdet.CalculationMode };

                foreach (var element in ourstyleidvar)
                {

                    if (element.ComponentName.Trim() == "FAB")
                    {
                        lbl_fabric.Text = element.CompValue.ToString();
                        lbl_fabricdet.Text = element.CompValue.ToString();
                        lblsummary_fabriccosts.Text = element.CompValue.ToString();
                       
                    }
                    else if (element.ComponentName.Trim() == "TRIMS")
                    {
                        lbl_trim.Text = element.CompValue.ToString();
                        lbl_trimdet.Text = element.CompValue.ToString();
                        lblsummary_trimcost.Text = element.CompValue.ToString();
                       
                    }
                    else if (element.ComponentName.Trim() == "CM")
                    {
                        lbl_cm.Text = element.CompValue.ToString();

                        lblsummary_cmcost.Text = element.CompValue.ToString();
                        
                    }
                    else if (element.ComponentName.Trim() == "WASH")
                    {
                        lbl_wash.Text = element.CompValue.ToString();
                        lblsummary_washcost.Text = element.CompValue.ToString();
                        
                       
                    }
                    else if (element.ComponentName.Trim() == "DRY PROCESS")
                    {
                        lbl_dryprocess.Text = element.CompValue.ToString();
                        others = others + float.Parse(element.CompValue.ToString());
                    }
                    else if (element.ComponentName.Trim() == "FAB COMMISION")
                    {
                        lbl_fabcommision.Text = element.CompValue.ToString();
                        commision = commision + float.Parse(element.CompValue.ToString());
                    }
                    else if (element.ComponentName.Trim() == "GARMENT COMMMISION")
                    {
                        lbl_garmentcom.Text = element.CompValue.ToString();
                        commision = commision + float.Parse(element.CompValue.ToString());
                    }
                    else if (element.ComponentName.Trim() == "COMPANY LOGISTICS")
                    {
                        lbl_companylogistic.Text = element.CompValue.ToString();
                        others = others + float.Parse(element.CompValue.ToString());
                    }
                    else if (element.ComponentName.Trim() == "FACTORY LOGISTICS")
                    {
                        lbl_factorylogistic.Text = element.CompValue.ToString();
                        others = others + float.Parse(element.CompValue.ToString());
                    }
                    else if (element.ComponentName.Trim() == "OTHERS")
                    {
                        lbl_others.Text = element.CompValue.ToString();
                        others = others + float.Parse(element.CompValue.ToString());
                    }

                   

                }

                lblsummary_otherscost.Text = others.ToString();
                lblsummary_comision.Text = commision.ToString();
            }


        }








        /// <summary>
        /// get the details of costing passed
        /// </summary>
        /// <param name="costingid"></param>
        public void getcostingdetails(int costingid)
        {
            csttran = new DBTransaction.CostingTransaction();
            DataTable dt = csttran.GetFabricTrimsDetailsofATC(costingid);


            DataTable dtfab = dt.Select("ItemGroupName ='Fabric'").CopyToDataTable();

            DataTable dtTrim = dt.Select("ItemGroupName ='Trims'").CopyToDataTable();

            DataView dv = dtTrim.DefaultView;
            dv.Sort = "RMNum";
            DataTable sortedDT = dv.ToTable();

            tbl_fabdet.DataSource = dtfab;
            tbl_fabdet.DataBind();

            tbl_trmdet.DataSource = sortedDT;
            tbl_trmdet.DataBind();

        }


        /// <summary>
        /// get the costing details
        /// </summary>
        /// <param name="ourstyleid"></param>
        public void getFirstCostingDetails(int costingid)
        {

            csttran = new DBTransaction.CostingTransaction();
            DataTable dt = csttran.GetCostingmaster(costingid);

            if (dt != null)
            {

                if (dt.Rows.Count >= 1)
                {                  
                  
                        try
                        {
                          lbl_costdate.Text = DateTime.Parse(dt.Rows[0]["CreatedDate"].ToString()).ToString("dd/MM/yyyy").Trim();
                          lbl_costid.Text = dt.Rows[0]["Costing_PK"].ToString().Trim();
                            
                            lblsummary_fobvalue.Text = dt.Rows[0]["FOB"].ToString().Trim();
                            lblsummary_marginpercent.Text = dt.Rows[0]["Margin"].ToString().Trim();
                            lblsummary_totalcost.Text = dt.Rows[0]["TotalCost"].ToString().Trim();
                            lblsummary_Marginvalue.Text = dt.Rows[0]["MarginValue"].ToString().Trim();
                        lbl_approvedby.Text = dt.Rows[0]["ApprovedBy"].ToString().Trim();
                        lbl_approveddate.Text = dt.Rows[0]["ApprovedDate"].ToString().Trim();
                         



                        }
                        catch (Exception)
                        {


                        }
                    }








                }



            }

        




        public void calculateTheDZNvalues()
        {
            float totalcostindzn = float.Parse(lblsummary_totalcost.Text) * 12;
            float fobindzn = float.Parse(lblsummary_fobvalue.Text) * 12;
            float marginvalueindzn = float.Parse(lblsummary_Marginvalue.Text) * 12;
            float fabricperdzn = float.Parse(lblsummary_fabriccosts.Text) * 12;           
            float trimperdzn = float.Parse(lblsummary_trimcost.Text) * 12;
            float cmdzn = float.Parse(lblsummary_cmcost.Text) * 12;
            float washperdzn = float.Parse(lblsummary_washcost.Text ) * 12;
            float otherperdozen = float.Parse(lblsummary_otherscost.Text) * 12;

            float commisionperdozen = float.Parse( lblsummary_comision.Text) * 12;



                    
            lblsummary_totalcostDZN.Text = totalcostindzn.ToString();
            lblsummary_fobvaluedzn.Text = fobindzn.ToString();
            lblsummary_Marginvaluedzn.Text = marginvalueindzn.ToString();
            lblsummary_fabriccostperdzn.Text = fabricperdzn.ToString();
            lblsummary_trimcostdzn.Text = trimperdzn.ToString();
            lblsummary_cmdzn.Text = cmdzn.ToString();
            lblsummary_washdzn.Text = washperdzn.ToString();
            lblsummary_othersdzn.Text = otherperdozen.ToString();
            lblsummary_comisiondzn.Text=commisionperdozen.ToString ();





            float fabricpercent = (fabricperdzn / fobindzn) * 100;
            float trimpercent = (trimperdzn / fobindzn) * 100;
            float cmperemt = (cmdzn / fobindzn) * 100;
            float washpercent = (washperdzn / fobindzn) * 100;
            float commisionpercent = (commisionperdozen / fobindzn) * 100;
            float otherpercent = (otherperdozen / fobindzn) * 100;
            float totalpercent = (totalcostindzn / fobindzn) * 100;




            lblsummary_fabricpercent.Text = fabricpercent.ToString();
            lblsummary_comisionpercent.Text = commisionpercent.ToString();
            lblsummary_cmpercent.Text = cmperemt.ToString();
            lblsummary_trimpercent.Text = trimpercent.ToString();
            lblsummary_washpercent.Text = washpercent.ToString();
            lblsummary_otherspercent.Text = otherpercent.ToString();
            lblsummary_totalcostpercent.Text = totalpercent.ToString();

           



        }

      


    }
}
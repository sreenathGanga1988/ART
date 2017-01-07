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
    public partial class StyleCostingHtmlReport : System.Web.UI.Page
    {
        DBTransaction.CostingTransaction csttran = null;
        protected void Page_Load(object sender, EventArgs e)
        {


            if (!IsPostBack)
            {
                String costpk = Request.QueryString["costingid"];
                int costingid = int.Parse(costpk);

                int ourstyleid = getourstyleid(costingid);





                try
                {
                    getCostingDetails(costingid);
                }
                catch (Exception)
                {


                }
                try
                {
                    getFirstCostingDetails(ourstyleid);
                }
                catch (Exception)
                {


                }
                try
                {
                    fillatcdetails(costingid);
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








        public void getoucomponents(int costingid)
        {

            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {
                var ourstyleidvar = from stcompdet in enty.StyleCostingComponentDetails
                                    join compmstr in enty.CostingComponentMasters on stcompdet.CostComp_PK equals compmstr.CostComp_PK
                                    where stcompdet.Costing_PK==costingid
                                    select new { compmstr.ComponentName, stcompdet.CompValue, stcompdet.CalculationMode };

                foreach (var element in ourstyleidvar)
                {

                    if (element.ComponentName.Trim() == "FAB")
                    {
                        lbl_fabric.Text = element.CompValue.ToString();
                    }
                    else if (element.ComponentName.Trim() == "TRIMS")
                    {
                        lbl_trim.Text = element.CompValue.ToString();
                    }
                    else if (element.ComponentName.Trim() == "CM")
                    {
                        lbl_cm.Text = element.CompValue.ToString();
                    }
                    else if (element.ComponentName.Trim() == "WASH")
                    {
                        lbl_wash.Text = element.CompValue.ToString();
                    }
                    else if (element.ComponentName.Trim() == "DRY PROCESS")
                    {
                        lbl_dryprocess.Text = element.CompValue.ToString();
                    }
                    else if (element.ComponentName.Trim() == "FAB COMMISION")
                    {
                        lbl_fabcommision.Text = element.CompValue.ToString();
                    }
                    else if (element.ComponentName.Trim() == "GARMENT COMMMISION")
                    {
                        lbl_garmentcom.Text = element.CompValue.ToString();
                    }
                    else if (element.ComponentName.Trim() == "COMPANY LOGISTICS")
                    {
                        lbl_companylogistic.Text = element.CompValue.ToString();
                    }
                    else if (element.ComponentName.Trim() == "FACTORY LOGISTICS")
                    {
                        lbl_factorylogistic.Text = element.CompValue.ToString();
                    }
                    else if (element.ComponentName.Trim() == "OTHERS")
                    {
                        lbl_others.Text = element.CompValue.ToString();
                    }


                }
            }


        }







        /// <summary>
        /// get ourstyleiud
        /// </summary>
        /// <param name="costingid"></param>
        /// <returns></returns>
        public int getourstyleid(int costingid)
        {
            int oursyleid = 0;
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {
                var ourstyleidvar = (from o in enty.StyleCostingMasters
                                     where o.Costing_PK == costingid

                                     select o.OurStyleID).FirstOrDefault();

                oursyleid = int.Parse(ourstyleidvar.ToString());
            }

            return oursyleid;
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

            tbl_fabdet.DataSource = dtfab;
            tbl_fabdet.DataBind();

            tbl_trmdet.DataSource = dtTrim;
            tbl_trmdet.DataBind();

        }

        /// <summary>
        /// get the first and last costing details
        /// </summary>
        /// <param name="ourstyleid"></param>
        public void getFirstCostingDetails(int ourstyleid)
        {

            csttran = new DBTransaction.CostingTransaction();
            DataTable dt = csttran.GetAllAprovedCostingOfaStyle(ourstyleid);

            if (dt != null)
            {

                if (dt.Rows.Count >= 1)
                {

                    if (dt.Rows.Count >= 1)
                    {
                        try
                        {
                            lbl_costingdate1.Text = DateTime.Parse(dt.Rows[0]["CreatedDate"].ToString()).ToString("dd/MM/yyyy").Trim();
                            lbl_costingID1.Text = dt.Rows[0]["Costing_PK"].ToString().Trim();
                            lbl_fob1.Text = dt.Rows[0]["FOB"].ToString().Trim();
                            lbl_margin1.Text = dt.Rows[0]["Margin"].ToString().Trim();
                            lbl_totalcost1.Text = dt.Rows[0]["TotalCost"].ToString().Trim();
                            lbl_marginvalue1.Text = dt.Rows[0]["MarginValue"].ToString().Trim();
                        }
                        catch (Exception)
                        {


                        }
                    }


                    if (dt.Rows.Count >= 2)
                    {
                        try
                        {
                            lbl_costingdate2.Text = DateTime.Parse(dt.Rows[1]["CreatedDate"].ToString()).ToString("dd/MM/yyyy").Trim();
                            lbl_costingID2.Text = dt.Rows[1]["Costing_PK"].ToString().Trim();
                            lbl_fob2.Text = dt.Rows[1]["FOB"].ToString().Trim();
                            lbl_margin2.Text = dt.Rows[1]["Margin"].ToString().Trim();
                            lbl_totalcost2.Text = dt.Rows[1]["TotalCost"].ToString().Trim();
                            lbl_marginvalue2.Text = dt.Rows[1]["MarginValue"].ToString().Trim();
                        }
                        catch (Exception)
                        {


                        }
                    }

                    if (dt.Rows.Count >= 3)
                    {
                        try
                        {
                            lbl_costingdate3.Text = DateTime.Parse(dt.Rows[2]["CreatedDate"].ToString()).ToString("dd/MM/yyyy").Trim();
                            lbl_costingID3.Text = dt.Rows[2]["Costing_PK"].ToString().Trim();
                            lbl_fob3.Text = dt.Rows[2]["FOB"].ToString().Trim();
                            lbl_margin3.Text = dt.Rows[2]["Margin"].ToString().Trim();
                            lbl_totalcost3.Text = dt.Rows[2]["TotalCost"].ToString().Trim();
                            lbl_marginvalue3.Text = dt.Rows[2]["MarginValue"].ToString().Trim();
                        }
                        catch (Exception)
                        {


                        }
                    }








                }



            }

        }

        /// <summary>
        /// fill the coisting trim and fabric details
        /// </summary>
        /// <param name="costingid"></param>
        public void getCostingDetails(int costingid)
        {
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {

                var q = from stylcstmstr in enty.StyleCostingMasters
                        where stylcstmstr.Costing_PK == costingid

                        select stylcstmstr;

                foreach (var element in q)
                {

                    lbl_costingdate4.Text = DateTime.Parse(element.CreatedDate.ToString()).ToString("dd/MM/yyyy").Trim();
                    lbl_costingID4.Text = element.Costing_PK.ToString().Trim();
                    lbl_fob4.Text = element.FOB.ToString().Trim();
                    lbl_margin4.Text = element.Margin.ToString().Trim();
                    lbl_totalcost4.Text = element.TotalCost.ToString().Trim();
                    lbl_marginvalue4.Text = element.MarginValue.ToString().Trim();

                }



            }

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            String costpk = Request.QueryString["costingid"];
            int costingid = int.Parse(costpk);

            BLL.CostingBLL.StyleCostingMaster cstml = new BLL.CostingBLL.StyleCostingMaster();
            cstml.SubmitCostingForApproval(costingid);


        }




    }
}
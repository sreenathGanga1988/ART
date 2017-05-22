using ArtWebApp.DataModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArtWebApp.Reports.MerchandiserReport
{
    public partial class ASQStatusReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            


            if(!IsPostBack)
            {
                ViewState["SQl"] = null;
              //  ASQData.SelectCommand = ViewState["SQl"].ToString();
            }
            else
            {
                try
                {
                    ASQData.SelectCommand = ViewState["SQl"].ToString();
                    ASQData.DataBind();
                }
                catch (Exception)
                {

                   
                }

            }
        }







        public void fillFactory()
        {


            ArrayList popaklist = new ArrayList();
            List<Infragistics.Web.UI.ListControls.DropDownItem> items = drp_country.SelectedItems;
            foreach (Infragistics.Web.UI.ListControls.DropDownItem item in items)
            {

                int popackid = int.Parse(item.Value.ToString());
                popaklist.Add(popackid);
            }


            if (popaklist.Count > 0 && popaklist != null)
            {

                string condition = "and( ";

                for (int i = 0; i < popaklist.Count; i++)
                {
                    if (i == 0)
                    {
                        condition = condition + " LocationMaster.CountryID=" + popaklist[i].ToString().Trim();
                    }
                    else
                    {
                        condition = condition + "  or LocationMaster.CountryID=" + popaklist[i].ToString().Trim();
                    }



                }

                if (condition != "and( ")
                {
                    condition = condition + " )";
                    String query = @"SELECT       LocationName as name, Location_PK as PK
FROM            LocationMaster  
 WHERE        (LocType = 'F')"+ condition;
                   
                    drp_factory.DataSource = null;
                    drp_factory.DataBind();
                    System.Data.DataTable dt = QueryFunctions.ReturnQueryResultDatatable(query);
                    drp_factory.DataSource = dt;
                    drp_factory.DataBind();
                    upd_factory.Update();
                    //   Upd_maingrid.Update();
                }

            }

        }







        public void fillAtcDatainGrid()
        {


            ArrayList popaklist = new ArrayList();
            List<Infragistics.Web.UI.ListControls.DropDownItem> items = drp_buyer.SelectedItems;
            foreach (Infragistics.Web.UI.ListControls.DropDownItem item in items)
            {

                int popackid = int.Parse(item.Value.ToString());
                popaklist.Add(popackid);
            }


            if (popaklist.Count > 0 && popaklist != null)
            {

                string condition = "where ";

                for (int i = 0; i < popaklist.Count; i++)
                {
                    if (i == 0)
                    {
                        condition = condition + " AtcMaster.Buyer_ID=" + popaklist[i].ToString().Trim();
                    }
                    else
                    {
                        condition = condition + "  or AtcMaster.Buyer_ID=" + popaklist[i].ToString().Trim();
                    }



                }

                if (condition != "where")
                {
                    String query = @"SELECT        AtcId AS PK, AtcNum AS name
FROM            AtcMaster " + condition;

                    atcdatasource.SelectCommand = query;
                    atcdatasource.DataBind();
                    drp_atc.DataBind();
                    //   Upd_maingrid.Update();
                }


            }









            }











        public void fillAtc()
        {
            ArrayList popaklist = new ArrayList();
            List<Infragistics.Web.UI.ListControls.DropDownItem> items = drp_buyer.SelectedItems;
            foreach (Infragistics.Web.UI.ListControls.DropDownItem item in items)
            {

                int popackid = int.Parse(item.Value.ToString());
                popaklist.Add(popackid);
            }


            if (popaklist.Count > 0 && popaklist != null)
            {

                string condition = "where ";

                for (int i = 0; i < popaklist.Count; i++)
                {
                    if (i == 0)
                    {
                        condition = condition + " AtcMaster.Buyer_ID=" + popaklist[i].ToString().Trim();
                    }
                    else
                    {
                        condition = condition + "  or AtcMaster.Buyer_ID=" + popaklist[i].ToString().Trim();
                    }



                }

                if (condition != "where")
                {
                    String query = @"SELECT        AtcId AS PK, AtcNum AS name
FROM            AtcMaster " + condition;

                    //atcdatasource.SelectCommand = query;
                    //atcdatasource.DataBind();
                    //drp_atc.DataBind();
                    ////   Upd_maingrid.Update();


                    drp_atc.DataSource = null;
                    drp_atc.DataBind();
                    System.Data.DataTable dt = QueryFunctions.ReturnQueryResultDatatable(query);
                    drp_atc.DataSource = dt;
                    drp_atc.DataBind();
                    upd_atc.Update();



                }

            }
        }




        public void fillReportofAtc()
        {
            ArrayList popaklist = new ArrayList();
            List<Infragistics.Web.UI.ListControls.DropDownItem> items = drp_atc.SelectedItems;
            foreach (Infragistics.Web.UI.ListControls.DropDownItem item in items)
            {

                int popackid = int.Parse(item.Value.ToString());
                popaklist.Add(popackid);
            }
            if (popaklist.Count > 0 && popaklist != null)
            {

                string conditionatc = "where( ";

                for (int i = 0; i < popaklist.Count; i++)
                {
                    if (i == 0)
                    {
                        conditionatc = conditionatc + " PoPackMaster.AtcId=" + popaklist[i].ToString().Trim();
                    }
                    else
                    {
                        conditionatc = conditionatc + "  or PoPackMaster.AtcId=" + popaklist[i].ToString().Trim();
                    }
                  


                }
                conditionatc = conditionatc + ")";
                if (conditionatc== "where()")
                {
                    conditionatc = "";
                }

                String query = @"SELECT        tt.PoPackId, tt.OurStyleID, tt.SeasonName, BuyerMaster.BuyerName, AtcMaster.AtcNum, tt.PoPacknum, tt.BuyerPO, tt.OurStyle, tt.BuyerStyle, tt.POQty, tt.ShipedQty, tt.POQty - tt.ShipedQty AS Balance, 
                         tt.FirstDeliveryDate, tt.HandoverDate, tt.DeliveryDate, ISNULL(tt.IsCutable, 'N') AS Iscuttable,isnull(  tt.LocationName,( (SELECT DISTINCT LocationMaster.LocationName
                               FROM            ASQAllocationMaster INNER JOIN
                                                         LocationMaster ON ASQAllocationMaster.Locaion_PK = LocationMaster.Location_PK
                               WHERE        (ASQAllocationMaster.PoPackId = tt.PoPackId) AND (ASQAllocationMaster.OurStyleId = tt.OurStyleID)))) as LocationName
FROM            (SELECT        PoPackMaster.PoPackId, PoPackMaster.PoPacknum, PoPackMaster.BuyerPO, AtcDetails.OurStyle, AtcDetails.BuyerStyle, SUM(POPackDetails_1.PoQty) AS POQty, ISNULL
                                                        ((SELECT        SUM(ShipmentHandOverDetails.ShippedQty) AS Expr1
                                                            FROM            ShipmentHandOverDetails INNER JOIN
                                                                                     JobContractDetail ON ShipmentHandOverDetails.JobContractDetail_pk = JobContractDetail.JobContractDetail_pk
                                                            GROUP BY JobContractDetail.PoPackID, JobContractDetail.OurStyleID
                                                            HAVING        (JobContractDetail.PoPackID = PoPackMaster.PoPackId) AND (JobContractDetail.OurStyleID = POPackDetails_1.OurStyleID)), 0) AS ShipedQty, AtcDetails.OurStyleID, 
                                                    PoPackMaster.FirstDeliveryDate, PoPackMaster.DeliveryDate, PoPackMaster.AtcId, PoPackMaster.HandoverDate, MAX(POPackDetails_1.IsCutable) AS IsCutable, PoPackMaster.SeasonName, 
                                                    LocationMaster.LocationName
                          FROM            PoPackMaster INNER JOIN
                                                    POPackDetails AS POPackDetails_1 ON PoPackMaster.PoPackId = POPackDetails_1.POPackId INNER JOIN
                                                    AtcDetails ON POPackDetails_1.OurStyleID = AtcDetails.OurStyleID LEFT OUTER JOIN
                                                    LocationMaster ON PoPackMaster.ExpectedLocation_PK = LocationMaster.Location_PK " + conditionatc +
                         @"  GROUP BY PoPackMaster.PoPackId, PoPackMaster.PoPacknum, PoPackMaster.BuyerPO, AtcDetails.OurStyle, AtcDetails.BuyerStyle, POPackDetails_1.OurStyleID, AtcDetails.OurStyleID, 
                                                    PoPackMaster.FirstDeliveryDate, PoPackMaster.DeliveryDate, PoPackMaster.AtcId, PoPackMaster.HandoverDate, PoPackMaster.SeasonName, LocationMaster.LocationName) AS tt INNER JOIN
                         AtcMaster ON tt.AtcId = AtcMaster.AtcId INNER JOIN
                         BuyerMaster ON AtcMaster.Buyer_ID = BuyerMaster.BuyerID
WHERE        (tt.POQty - tt.ShipedQty > 0)";


                ViewState["SQl"] = query;

                ASQData.SelectCommand = query;
                ASQData.DataBind();
                WebDataGrid1.DataBind();

            }
        }




        protected void BTN_CTRY_Click(object sender, EventArgs e)
        {



            fillFactory();





         
        }

        protected void btn_fact_Click(object sender, EventArgs e)
        {

        }

        protected void btn_buyer_Click(object sender, EventArgs e)
        {

        }

        protected void btn_atc_Click(object sender, EventArgs e)
        {

        }

        protected void btn_buyer_Click1(object sender, EventArgs e)
        {
            fillAtc();
        }

        protected void drp_country_SelectionChanged(object sender, Infragistics.Web.UI.ListControls.DropDownSelectionChangedEventArgs e)
        {

        }

        protected void btn_hD_Click(object sender, EventArgs e)
        {
            WebDataGrid1.DataSource = ASQData;
            WebDataGrid1.DataBind();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            fillReportofAtc();
        }

        protected void drp_atc_SelectionChanged(object sender, Infragistics.Web.UI.ListControls.DropDownSelectionChangedEventArgs e)
        {

        }

        protected void drp_factory_SelectionChanged(object sender, Infragistics.Web.UI.ListControls.DropDownSelectionChangedEventArgs e)
        {

        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            this.WebExcelExporter1.Export(this.WebDataGrid1);
        }
    }
}
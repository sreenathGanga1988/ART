using ArtWebApp.DataModels;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OfficeOpenXml;

using System.IO;
using Infragistics.Web.UI.ListControls;
using System.Data.SqlClient;
using System.Configuration;

namespace ArtWebApp.Production.Schedular
{
    public partial class ProductionReporting : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                FillAtcCombo();

            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (FileUpload1.HasFile)
            {
                if (Path.GetExtension(FileUpload1.FileName) == ".xlsx")
                {
                    ExcelPackage package = new ExcelPackage(FileUpload1.FileContent);
                    DataTable dt = package.ToDataTable();

                    if (dt != null)
                    {

                        DataView view = new DataView(dt);
                        DataTable distinctValues = view.ToTable(true, "Atc");
                        distinctValues.Columns.Add("Atcid");
                        for (int i = 0; i < distinctValues.Rows.Count; i++)
                        {
                            using (ArtEntitiesnew entty = new ArtEntitiesnew())
                            {

                                String atcnum = dt.Rows[i][0].ToString();
                                var atciddata = (from o in entty.AtcMasters
                                                 where o.AtcNum == atcnum

                                                 select o.AtcId).FirstOrDefault();
                                distinctValues.Rows[i]["Atcid"] = atciddata.ToString();
                            }
                            DropDownItem item = drp_Atc.Items.FindItemByValue(distinctValues.Rows[i]["Atcid"].ToString());

                            item.Selected = true;
                        }



                    }

                }
            }
        }


        public void FillAtcCombo()
        {
            using (ArtEntitiesnew entty = new ArtEntitiesnew())
            {
                entty.Configuration.AutoDetectChangesEnabled = false;
                var q = from atcorder in entty.AtcMasters
                        select new
                        {
                            name = atcorder.AtcNum,
                            pk = atcorder.AtcId
                        };

                // Create a table from the query.
                drp_Atc.DataSource = q.ToList();
                drp_Atc.DataBind();




            }
        }
        public DataTable GetProductionAtcWorldData(String Condition)
        {


            DateTime today = DateTime.Now.Date;
            DataTable datafromart = new DataTable();

            String Datepend = "";

            String q3 = @"SELECT        ArtLocation_PK, CutQty, SewingQty, SortQty, WashQty, FinishQty, AirOutQty, ShipQty, DOReceiveQty, ProductionDate, PoPack_Detail_PK,Atc_Id
FROM            ProductionStatusDatewiseATCWorld_VW
WHERE        (Atc_Id = 121)";



            //  cmd.CommandText = Query1;






            return QueryFunctions.ReturnQueryResultDatatablefromAtcWorldkENYA(q3);
        }




      


        protected void btn_sales_Click(object sender, EventArgs e)
        {

            DataTable dt = GetProductionAtcWorldData(Getcondition());

            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {


                foreach (DataRow dr in dt.Rows)
            {

               
                    TempProductionData tmpprod = new TempProductionData();

                    tmpprod.ArtLocation_PK = int.Parse(dr["ArtLocation_PK"].ToString());
                    tmpprod.CutQty = int.Parse(dr["CutQty"].ToString());
                    tmpprod.SewingQty = int.Parse(dr["SewingQty"].ToString());
                    tmpprod.SortQty = int.Parse(dr["SortQty"].ToString());
                    tmpprod.WashQty = int.Parse(dr["WashQty"].ToString());
                    tmpprod.FinishQty = int.Parse(dr["FinishQty"].ToString());
                    tmpprod.AirOutQty = int.Parse(dr["AirOutQty"].ToString());
                    tmpprod.ShipQty = int.Parse(dr["ShipQty"].ToString());
                    tmpprod.DOReceiveQty = int.Parse(dr["DOReceiveQty"].ToString());
                    tmpprod.ProductionDate = DateTime.Parse(dr["ProductionDate"].ToString());
                    tmpprod.PoPack_Detail_PK = int.Parse(dr["PoPack_Detail_PK"].ToString());
                    tmpprod.AtcId = int.Parse(dr["Atc_Id"].ToString());
                    tmpprod.AddedBY = HttpContext.Current.Session["Username"].ToString();
                    tmpprod.AddedDate = DateTime.Now;

                    enty.TempProductionDatas.Add(tmpprod);

                }
                enty.SaveChanges();



                  


             
            }   
        }


        public string Getcondition()
        {

            ArrayList atcdetlist = getAtclist();

            String con = "";
            if (atcdetlist.Count > 0)
            {
                string condition = "Where (";

                for (int i = 0; i < atcdetlist.Count; i++)
                {

                    if (i == 0)
                    {
                        condition = condition + " ProductionStatusDatewiseATCWorld_VW.AtcId  =" + atcdetlist[i].ToString().Trim();
                    }
                    else
                    {
                        condition = condition + "  or ProductionStatusDatewiseATCWorld_VW.AtcId  =" + atcdetlist[i].ToString().Trim();
                    }






                }
                condition = condition + ")";

                con = condition;
            }

            return con;
        }

        public ArrayList getAtclist()
        {
            ArrayList atcArraylist = new ArrayList();
            List<Infragistics.Web.UI.ListControls.DropDownItem> items = drp_Atc.SelectedItems;
            foreach (Infragistics.Web.UI.ListControls.DropDownItem item in items)
            {

                int atcid = int.Parse(item.Value.ToString());
                atcArraylist.Add(atcid);
            }
            return atcArraylist;
        }
    }
}
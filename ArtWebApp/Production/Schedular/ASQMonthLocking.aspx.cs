using ArtWebApp.DataModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArtWebApp.Production.Schedular
{
    public partial class ASQMonthLocking : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button3_Click(object sender, EventArgs e)
        {

        }

        protected void Button3_Click1(object sender, EventArgs e)
        {

            int year = int.Parse(cmb_year.SelectedItem.Text);

            int month = int.Parse(cmb_Month.SelectedValue.ToString());

            var startDate = new DateTime(year, month, 1);
            var endDate = startDate.AddMonths(1).AddDays(-1);

            lbl_fromdate.Text = startDate.ToString();
            lbl_todate.Text = endDate.ToString();
            getPOData();
            showstatus(year.ToString(), month);

        }


        public void showstatus(String year,int month)
        {
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {

                var q = from yrmstr in enty.YearMonthMasters
                        where yrmstr.YearName == year && yrmstr.MonthNum == month
                        select yrmstr;
                foreach(var element in q)
                {
                    lbl_Targetlocked.Text = element.IsTargetLocked.ToString();
                    lbl_shipmentclosed.Text = element.IsShipmentClose.ToString();

                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

          

                if (lbl_Targetlocked.Text=="N")
            {

                int year = int.Parse(cmb_year.SelectedItem.ToString());
                int monthnum = int.Parse(cmb_Month.SelectedValue.ToString());
                foreach (GridViewRow di in tbl_podata.Rows)
                {



                    int locationpk = int.Parse(((di.FindControl("lbl_locationpk") as Label).Text.ToString()));
                    int lbl_ourstyleid = int.Parse(((di.FindControl("lbl_ourstyleid") as Label).Text.ToString()));
                    int lbl_qty = int.Parse(((di.FindControl("lbl_qty") as Label).Text.ToString()));
                    int lbl_popackid = int.Parse(((di.FindControl("lbl_popackid") as Label).Text.ToString()));

                   

                    string monthnam = cmb_Month.SelectedItem.ToString();

                    BLL.PoPackMasterData pomstrdata = new BLL.PoPackMasterData();

                    pomstrdata.PoPackId = lbl_popackid;
                    pomstrdata.styleid = lbl_ourstyleid;
                    pomstrdata.location_PK = locationpk;
                    pomstrdata.Year = year;
                    pomstrdata.Month = monthnum;
                    pomstrdata.MonthName = monthnam;
                    pomstrdata.AsqQty = Decimal.Parse(lbl_qty.ToString());



                    pomstrdata.Inserttarget();


                }
                BLL.PoPackMasterData pomstrdata1 = new BLL.PoPackMasterData();

                pomstrdata1.LockMontofProjection(year, monthnum);

                String msg = "Monthly Projection  Locked Successfully";

                ArtWebApp.Controls.Messagebox.MessgeboxUpdate(Messaediv, "sucess", msg);
                tbl_podata.DataSource = null;
                tbl_podata.DataBind();
            }


          
        }





        public void getPOData()
        {

            using (SqlCommand cmd= new SqlCommand ())
            {
                cmd.CommandText = @"SELECT        PoPackMaster.PoPackId, AtcMaster.AtcNum, PoPackMaster.BuyerPO, PoPackMaster.PoPacknum, PoPackMaster.DeliveryDate, SUM(POPackDetails.PoQty) AS PoQty, POPackDetails.OurStyleID, 
                        isnull( PoPackMaster.ExpectedLocation_PK,0) as LocationPK, MAX(POPackDetails.IsShortClosed) AS Expr1
FROM            AtcMaster INNER JOIN
                         PoPackMaster ON AtcMaster.AtcId = PoPackMaster.AtcId INNER JOIN
                         POPackDetails ON PoPackMaster.PoPackId = POPackDetails.POPackId
GROUP BY AtcMaster.AtcNum, PoPackMaster.PoPackId, PoPackMaster.PoPacknum, PoPackMaster.BuyerPO, PoPackMaster.DeliveryDate, POPackDetails.OurStyleID, PoPackMaster.ExpectedLocation_PK
HAVING        (PoPackMaster.DeliveryDate BETWEEN @param1 AND @param2) AND (MAX(POPackDetails.IsShortClosed) = N'N')";

                cmd.Parameters.AddWithValue("@param1", DateTime.Parse(lbl_fromdate.Text.ToString()));
                cmd.Parameters.AddWithValue("@param2", DateTime.Parse(lbl_todate.Text.ToString()));

                DataTable dt = QueryFunctions.ReturnQueryResultDatatable(cmd);
                tbl_podata.DataSource = dt;
                tbl_podata.DataBind();
            }

        }






    }
}
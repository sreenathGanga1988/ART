﻿using ArtWebApp.DataModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
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
            getPendingASQData();
            showstatus(year.ToString(), month);


        }


        public void showstatus(String year, int month)
        {
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {

                var q = from yrmstr in enty.YearMonthMasters
                        where yrmstr.YearName == year && yrmstr.MonthNum == month
                        select yrmstr;
                foreach (var element in q)
                {
                    lbl_Targetlocked.Text = element.IsTargetLocked.ToString();
                    lbl_shipmentclosed.Text = element.IsShipmentClose.ToString();

                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {



            if (lbl_Targetlocked.Text.Trim() == "N")
            {

                int year = int.Parse(cmb_year.SelectedItem.ToString());
                int monthnum = int.Parse(cmb_Month.SelectedValue.ToString());
                foreach (GridViewRow di in tbl_podata.Rows)
                {




                    int lbl_ourstyleid = int.Parse(((di.FindControl("lbl_ourstyleid") as Label).Text.ToString()));
                    int lbl_qty = int.Parse(((di.FindControl("lbl_Balance") as Label).Text.ToString()));
                    int lbl_popackid = int.Parse(((di.FindControl("lbl_popackid") as Label).Text.ToString()));

                    int locationpk = int.Parse(((di.FindControl("lbl_locationpk") as Label).Text.ToString()));

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

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = @"SELECT        PoPackId, AtcNum, BuyerPO, PoPacknum, DeliveryDate, PoQty, OurStyleID, LocationPK, Expr1, ShipedQty, PoQty - ShipedQty AS BalanceQty, SalesHandoverDate
FROM            (SELECT        PoPackMaster.PoPackId, AtcMaster.AtcNum, PoPackMaster.BuyerPO, PoPackMaster.PoPacknum, PoPackMaster.DeliveryDate, SUM(POPackDetails.PoQty) AS PoQty, POPackDetails.OurStyleID, 
                                                    ISNULL(PoPackMaster.ExpectedLocation_PK, 0) AS LocationPK, MAX(POPackDetails.IsShortClosed) AS Expr1, ISNULL
                                                        ((SELECT        SUM(ShippedQty) AS Expr1
                                                            FROM            ShipmentHandOverDetails
                                                            GROUP BY POPackId, OurStyleID
                                                            HAVING        (POPackId = PoPackMaster.PoPackId) AND (OurStyleID = POPackDetails.OurStyleID)), 0) AS ShipedQty, PoPackMaster.SalesHandoverDate
                          FROM            AtcMaster INNER JOIN
                                                    PoPackMaster ON AtcMaster.AtcId = PoPackMaster.AtcId INNER JOIN
                                                    POPackDetails ON PoPackMaster.PoPackId = POPackDetails.POPackId
                          GROUP BY AtcMaster.AtcNum, PoPackMaster.PoPackId, PoPackMaster.PoPacknum, PoPackMaster.BuyerPO, PoPackMaster.DeliveryDate, POPackDetails.OurStyleID, PoPackMaster.ExpectedLocation_PK, 
                                                    PoPackMaster.SalesHandoverDate
                          HAVING         (PoPackMaster.DeliveryDate BETWEEN @param1 AND @param2) AND (MAX(POPackDetails.IsShortClosed) = N'N')) AS tt";

                cmd.Parameters.AddWithValue("@param1", DateTime.Parse(lbl_fromdate.Text.ToString()));
                cmd.Parameters.AddWithValue("@param2", DateTime.Parse(lbl_todate.Text.ToString()));

                DataTable dt = QueryFunctions.ReturnQueryResultDatatable(cmd);
                tbl_podata.DataSource = dt;
                tbl_podata.DataBind();
            }

        }


        public void getPendingASQData()
        {

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = @"Select COUNT(PoPackId) from (SELECT        PoPackId, PoPacknum, BuyerPO, OurStyle, BuyerStyle, POQty, ShipedQty, OurStyleID, FirstDeliveryDate, DeliveryDate, HandoverDate
FROM(SELECT        PoPackMaster.PoPackId, PoPackMaster.PoPacknum, PoPackMaster.BuyerPO, AtcDetails.OurStyle, AtcDetails.BuyerStyle, SUM(POPackDetails.PoQty) AS POQty, isnull((SELECT        SUM(ShippedQty) AS Expr1
FROM            ShipmentHandOverDetails
GROUP BY POPackId, OurStyleID
HAVING        (POPackId = PoPackMaster.PoPackId) AND (OurStyleID = POPackDetails.OurStyleID)), 0) AS ShipedQty, AtcDetails.OurStyleID, PoPackMaster.FirstDeliveryDate, 
                         PoPackMaster.DeliveryDate, PoPackMaster.AtcId, PoPackMaster.HandoverDate, MAX(POPackDetails.IsShortClosed) AS Expr1
FROM PoPackMaster INNER JOIN
                         POPackDetails ON PoPackMaster.PoPackId = POPackDetails.POPackId INNER JOIN
                         AtcDetails ON POPackDetails.OurStyleID = AtcDetails.OurStyleID
GROUP BY PoPackMaster.PoPackId, PoPackMaster.PoPacknum, PoPackMaster.BuyerPO, AtcDetails.OurStyle, AtcDetails.BuyerStyle, POPackDetails.OurStyleID, AtcDetails.OurStyleID, PoPackMaster.FirstDeliveryDate, 
                         PoPackMaster.DeliveryDate, PoPackMaster.AtcId, PoPackMaster.HandoverDate
HAVING(PoPackMaster.HandoverDate < @param1) AND(MIN(POPackDetails.IsShortClosed) = N'N') ) AS tt
WHERE(POQty - ShipedQty > 0)
)tt";

                cmd.Parameters.AddWithValue("@param1", DateTime.Parse(lbl_fromdate.Text.ToString()));


                var count = QueryFunctions.ReturnQueryValue(cmd);

                int pending = 0;


                try
                {
                    pending = int.Parse(count.ToString());
                }
                catch (Exception)
                {


                }

                lbl_pendingASQ.Text = pending.ToString();
            }

        }
        int totalvalue = 0;
        int totalvalueship = 0;


        protected void tbl_podata_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //Check if the current row is datarow or not
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //Add the value of column
                totalvalue += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "PoQty"));
                totalvalueship += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "ShipedQty"));

                AjaxControlToolkit.CalendarExtender txtcalender = (e.Row.FindControl("dtp_deliverydate_CalendarExtender") as AjaxControlToolkit.CalendarExtender);
                Label lbl_handoverdate = (e.Row.FindControl("lbl_handoverdate") as Label);

                txtcalender.SelectedDate = DateTime.Parse(lbl_handoverdate.Text);

            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                //Find the control label in footer 
                Label lblamount = (Label)e.Row.FindControl("lblTotalValue");
                //Assign the total value to footer label control
                lblamount.Text = "Total Qty is : " + totalvalue.ToString();
                Label lblTotalValueship = (Label)e.Row.FindControl("lblTotalValueship");
                //Assign the total value to footer label control
                lblTotalValueship.Text = "Total ShippedQty is : " + totalvalueship.ToString();
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow di in tbl_podata.Rows)
            {

                CheckBox chkBx = (CheckBox)di.FindControl("chk_select");
                TextBox dtp_deliverydate = (TextBox)di.FindControl("dtp_deliverydate");
                if (chkBx != null && chkBx.Checked)
                {

                    string s = DateTime.Parse(Request.Form[dtp_deliverydate.UniqueID].ToString()).ToString("dd/MMM/yyyy", CultureInfo.InvariantCulture);

                    DateTime deliverydate = DateTime.Parse(((Label)di.FindControl("lbl_handoverdate")).Text);
                    int popackid = int.Parse(((di.FindControl("lbl_popackid") as Label).Text.ToString()));
                    BLL.PoPackMasterData pomstrdata = new BLL.PoPackMasterData();

                    pomstrdata.PoPackId = popackid;

                    pomstrdata.HandoverDate = DateTime.Parse(s);

                    pomstrdata.updatePOpAckSHD(pomstrdata);
                }
            }
        }
    }
}
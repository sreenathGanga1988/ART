using ArtWebApp.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArtWebApp.Production.Schedular
{
    public partial class ASQMonthShipmentClosing : System.Web.UI.Page
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

            showstatus(year.ToString(), month);

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if(lbl_Targetlocked.Text.Trim()=="Y"&& lbl_shipmentclosed.Text.Trim() == "N")
            {
                MonthClosing();
            }
         
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

        public void MonthClosing()
        {
            int year = int.Parse(cmb_year.SelectedItem.ToString());
            int month = int.Parse(cmb_Month.SelectedValue.ToString());
            DateTime fromdate = DateTime.Parse(lbl_fromdate.Text);
            DateTime todate = DateTime.Parse(lbl_todate.Text);
            BLL.PoPackMasterData pomstrdata = new BLL.PoPackMasterData();

          
            pomstrdata.Year = year;
            pomstrdata.Month = month;
            pomstrdata.MonthName = cmb_Month.SelectedItem.Text.ToString().Trim();
            //pomstrdata.AsqQty = Decimal.Parse(lbl_qty.ToString());



            pomstrdata.LockShipment();

            String msg = "Shipment Closed for the Month of " + pomstrdata.MonthName + "/" + pomstrdata.Year.ToString() + "Successfully";

            ArtWebApp.Controls.Messagebox.MessgeboxUpdate(Messaediv, "sucess", msg);
            
            GridView1.DataBind();
        }
    }
}
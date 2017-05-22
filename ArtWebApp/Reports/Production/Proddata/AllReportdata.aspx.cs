using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArtWebApp.Reports.Production.Proddata
{
    public partial class AllReportdata : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public void filldata(DataTable dt)
        {

            //Populating a DataTable from database.
        

            //Building an HTML string.
            StringBuilder html = new StringBuilder();

            //Table start.
            html.Append("<table  id='example' border = '2'>");

            //Building the Header row.
            html.Append(" <thead> <tr>");
            foreach (DataColumn column in dt.Columns)
            {
                html.Append("<th>");
                html.Append(column.ColumnName);
                html.Append("</th>");
            }
            html.Append("</tr></thead>");

            html.Append(" <thead class='filters'> <tr>");
            foreach (DataColumn column in dt.Columns)
            {
                html.Append("<td>");
                html.Append(column.ColumnName);
                html.Append("</td>");
            }
            html.Append("</tr></thead>");


            html.Append(" <tfoot> <tr>");
            foreach (DataColumn column in dt.Columns)
            {
                html.Append("<th>");
                html.Append(column.ColumnName);
                html.Append("</th>");
            }
            html.Append("</tr></tfoot>");






            //Building the Data rows.
            foreach (DataRow row in dt.Rows)
            {
                html.Append("<tr>");
                foreach (DataColumn column in dt.Columns)
                {
                    html.Append("<td>");
                    html.Append(row[column.ColumnName]);
                    html.Append("</td>");
                }
                html.Append("</tr>");
            }

          

            //Table end.
            html.Append("</table>");

            //Append the HTML string to Placeholder.
            MasterDiv.Controls.Add(new Literal { Text = html.ToString() });

        }
        protected void btn_showCountrydata_Click(object sender, EventArgs e)
        {
            DateTime fromdate = DateTime.Parse(dtp_shipStartdate.Value.ToString());
            DateTime todate = DateTime.Parse(dtp_shipenddate.Value.ToString());

            DataTable dt = BLL.ProductionBLL.AllInOneProducttionReportBLL.getProductionDataofCountryWithinPeriod(drp_country.SelectedItem.Text, fromdate, todate);

            filldata(dt);
        }

        protected void btn_showBuyerdata_Click(object sender, EventArgs e)
        {
            DateTime fromdate = DateTime.Parse(dtp_shipStartdate.Value.ToString());
            DateTime todate = DateTime.Parse(dtp_shipenddate.Value.ToString());

            DataTable dt = BLL.ProductionBLL.AllInOneProducttionReportBLL.getProductionDataofBuyerWithinPeriod(drp_buyer.SelectedItem.Text, fromdate, todate);
            
            filldata(dt);
        }

        protected void btn_showAtcdata_Click(object sender, EventArgs e)
        {
            fillReportofAtc();
        }



        public void fillReportofAtc()
        {
            ArrayList popaklist = new ArrayList();
            List<Infragistics.Web.UI.ListControls.DropDownItem> items = drp_Atc.SelectedItems;
            foreach (Infragistics.Web.UI.ListControls.DropDownItem item in items)
            {

                String popackid = item.Text.ToString();
                popaklist.Add(popackid);
            }
            if (popaklist.Count > 0 && popaklist != null)
            {

                string conditionatc = " and ( ";

                for (int i = 0; i < popaklist.Count; i++)
                {
                    if (i == 0)
                    {
                        conditionatc = conditionatc + " ttt.AtcNum='" + popaklist[i].ToString().Trim()+"'";
                    }
                    else
                    {
                        conditionatc = conditionatc + "  or ttt.AtcNum='" + popaklist[i].ToString().Trim() + "'";
                    }



                }
                conditionatc = conditionatc + ")";
                if (conditionatc == "and()")
                {
                    conditionatc = "";
                }

                DateTime fromdate = DateTime.Parse(dtp_shipStartdate.Value.ToString());
                DateTime todate = DateTime.Parse(dtp_shipenddate.Value.ToString());

                DataTable dt = BLL.ProductionBLL.AllInOneProducttionReportBLL.getProductionDataofSelectedAtcWithinPeriod(conditionatc, fromdate, todate);

                filldata(dt);
            }
        }



        protected void btn_showatc_Click(object sender, EventArgs e)
        {
            fillAtc();
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


                    drp_Atc.DataSource = null;
                    drp_Atc.DataBind();
                    System.Data.DataTable dt = QueryFunctions.ReturnQueryResultDatatable(query);
                    drp_Atc.DataSource = dt;
                    drp_Atc.DataBind();
                



                }

            }
        }
    }
}
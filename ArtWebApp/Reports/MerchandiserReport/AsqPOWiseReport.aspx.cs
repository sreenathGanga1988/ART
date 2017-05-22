using ArtWebApp.DataModels;
using Infragistics.Web.UI.GridControls;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArtWebApp.Reports.MerchandiserReport
{
    public partial class AsqPOWiseReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
           
                  
           
           
        }









        public void filldata( DataTable dt)
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













        protected void ShowBom_Click(object sender, EventArgs e)
        {
             
           
            fillcontrol();
            filldata(GetData());
        }


        private DataTable GetData()
        {
            int atcid = int.Parse(cmb_atc.SelectedValue.ToString());

            using (SqlCommand cmd = new SqlCommand())
            {

                cmd.CommandText = @"SELECT POPackDetails.PoPack_Detail_PK, AtcMaster.AtcNum, AtcDetails.OurStyle, AtcDetails.BuyerStyle, PoPackMaster.SeasonName, 
PoPackMaster.DeliveryDate, PoPackMaster.FirstDeliveryDate, PoPackMaster.HandoverDate, BuyerDestinationMaster.BuyerDestination, ChannelMaster.ChannelName, 
PoPackMaster.PoPacknum, PoPackMaster.BuyerPO, POPackDetails.ColorName, POPackDetails.SizeName, POPackDetails.PoQty, ISNULL(POPackDetails.IsCutable, 'N') AS IsCutable,
LocationMaster.LocationName FROM PoPackMaster INNER JOIN POPackDetails ON PoPackMaster.PoPackId = POPackDetails.POPackId INNER JOIN AtcDetails ON 
POPackDetails.OurStyleID = AtcDetails.OurStyleID INNER JOIN AtcMaster ON AtcDetails.AtcId = AtcMaster.AtcId INNER JOIN BuyerDestinationMaster ON 
PoPackMaster.BuyerDestination_PK = BuyerDestinationMaster.BuyerDestination_PK INNER JOIN ChannelMaster ON PoPackMaster.ChannelID = ChannelMaster.ChannelID LEFT 
OUTER JOIN LocationMaster ON PoPackMaster.ExpectedLocation_PK = LocationMaster.Location_PK WHERE (PoPackMaster.AtcId = @param1)";

                cmd.Parameters.AddWithValue("@param1", atcid);


             return   QueryFunctions.ReturnQueryResultDatatable(cmd);

            }
            
        }


        public void fillcontrol()
        {
            int atcid = int.Parse(cmb_atc.SelectedValue.ToString());

            ArtEntitiesnew enty = new ArtEntitiesnew();
            var PoQuery = from pckmst in enty.PoPackMasters
                          where pckmst.AtcId == atcid
                          select new
                          {
                              name = pckmst.PoPacknum + " " + pckmst.BuyerPO,
                              pk = pckmst.PoPackId
                          };



            drp_popack.DataSource = PoQuery.ToList();
            drp_popack.DataBind();







            //showAllPoPackATC();
        }

        protected void ShowBom0_Click(object sender, EventArgs e)
        {
            filldata(getfilterdata());
        }



       public DataTable getfilterdata()
        {
            DataTable dt = new DataTable();

            ArrayList popaklist = new ArrayList();
            List<Infragistics.Web.UI.ListControls.DropDownItem> items = drp_popack.SelectedItems;
            foreach (Infragistics.Web.UI.ListControls.DropDownItem item in items)
            {

                int popackid = int.Parse(item.Value.ToString());
                popaklist.Add(popackid);
            }


            if (popaklist.Count > 0 && popaklist != null)
            {

                string condition = "where";

                for (int i = 0; i < popaklist.Count; i++)
                {
                    if (i == 0)
                    {
                        condition = condition + " POPackDetails.PoPackId=" + popaklist[i].ToString().Trim();
                    }
                    else
                    {
                        condition = condition + "  or POPackDetails.PoPackId=" + popaklist[i].ToString().Trim();
                    }



                }

                if (condition != "where")
                {
                    String query = @"SELECT        POPackDetails.PoPack_Detail_PK, AtcMaster.AtcNum, AtcDetails.OurStyle, AtcDetails.BuyerStyle, PoPackMaster.SeasonName, PoPackMaster.DeliveryDate, PoPackMaster.FirstDeliveryDate, 
                         PoPackMaster.HandoverDate, BuyerDestinationMaster.BuyerDestination, ChannelMaster.ChannelName, PoPackMaster.PoPacknum, PoPackMaster.BuyerPO, POPackDetails.ColorName, 
                         POPackDetails.SizeName, POPackDetails.PoQty, ISNULL(POPackDetails.IsCutable, 'N') AS IsCutable, PoPackMaster.PoPackId
FROM            PoPackMaster INNER JOIN
                         POPackDetails ON PoPackMaster.PoPackId = POPackDetails.POPackId INNER JOIN
                         AtcDetails ON POPackDetails.OurStyleID = AtcDetails.OurStyleID INNER JOIN
                         AtcMaster ON AtcDetails.AtcId = AtcMaster.AtcId INNER JOIN
                         BuyerDestinationMaster ON PoPackMaster.BuyerDestination_PK = BuyerDestinationMaster.BuyerDestination_PK INNER JOIN
                         ChannelMaster ON PoPackMaster.ChannelID = ChannelMaster.ChannelID " + condition;
                    using (SqlCommand cmd = new SqlCommand())
                    {

                        cmd.CommandText = query;




                        dt= QueryFunctions.ReturnQueryResultDatatable(cmd);

                    }
                }


            }

            return dt;
        }

    }
}
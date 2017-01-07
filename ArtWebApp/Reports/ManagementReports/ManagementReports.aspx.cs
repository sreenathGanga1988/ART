using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArtWebApp.Reports.ManagementReports
{
    public partial class ManagementReports : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (HttpContext.Current.User.Identity.Name == "Mannan" || HttpContext.Current.User.Identity.Name == "sree" || HttpContext.Current.User.Identity.Name == "siraj" )
                {
                  










                }
                else
                {
                    Response.Redirect("../Authorisation.aspx");
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            
            DBTransaction.ManagementTransaction.ManagementReportTransaction  mngttran = new DBTransaction.ManagementTransaction.ManagementReportTransaction();

            System.Data.DataTable dt = mngttran.GetProfitabilty();


            showInventoryReport(calculatePOQty(dt));
        }



        public void showInventoryReport(DataTable dt)
        {
            ReportDataSource datasource = new ReportDataSource("DataSet1", dt);
            this.ReportViewer1.LocalReport.DataSources.Clear();
            this.ReportViewer1.LocalReport.DataSources.Add(datasource);
           

            this.ReportViewer1.LocalReport.ReportPath = @"Reports\RDLC\Profitability.rdlc";
          
        }



        public DataTable calculatePOQty(DataTable dt)
        {
            
            foreach (DataColumn clmn in dt.Columns)
            {
                clmn.ReadOnly = false;
            }
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                float atcid = float.Parse(dt.Rows[i]["AtcId"].ToString());
                float OurStyleID = float.Parse(dt.Rows[i]["OurStyleID"].ToString());
                float POValue = float.Parse(dt.Rows[i]["POValue"].ToString());
                float ActualCM = float.Parse(dt.Rows[i]["ActualCM"].ToString());
                float Overhead = float.Parse(dt.Rows[i]["Overhead"].ToString());
                float ShippedQty = float.Parse(dt.Rows[i]["ShippedQty"].ToString());
                float FOB = float.Parse(dt.Rows[i]["FOB"].ToString());
                object asqqtyofatc;
                asqqtyofatc = dt.Compute("Sum(ASQQTY)", "AtcId=" + atcid + "");

                object asqqtyofourstyle;
                asqqtyofourstyle = dt.Compute("Sum(ASQQTY)", "OurStyleID=" + OurStyleID + "");


                float newpovalue = (POValue / float.Parse(asqqtyofatc.ToString())) * float.Parse(asqqtyofourstyle.ToString());
                dt.Rows[i]["ActualPOValue"] = newpovalue.ToString();


                float totalspent = (newpovalue + ActualCM + Overhead);
                dt.Rows[i]["TotalSpent"] = totalspent.ToString();


                float actualprofit = (ShippedQty * FOB) - (totalspent);
                dt.Rows[i]["Actualprofit"] = actualprofit.ToString();

            }
            return dt;
        }
    


}
}
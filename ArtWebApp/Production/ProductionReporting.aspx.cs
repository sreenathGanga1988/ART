using ArtWebApp.DataModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArtWebApp.Production
{
    public partial class ProductionReporting : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Btn_showJC_Click(object sender, EventArgs e)
        {
            try
            {
                loadJobcontract(int.Parse(drp_factory.SelectedValue.ToString()));
            }
            catch (Exception)
            {


            }
        }


        public void loadJobcontract(int factid)
        {
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {
                var q = from jcdata in enty.JobContractMasters
                        where jcdata.Location_Pk == factid
                        select new
                        {
                            name = jcdata.JOBContractNUM,
                            pk = jcdata.JobContract_pk.ToString()
                        };
                drp_jobcontract.DataSource = q.ToList();
                drp_jobcontract.DataBind();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            ArrayList jobcontrlist = new ArrayList();
            List<Infragistics.Web.UI.ListControls.DropDownItem> items = drp_jobcontract.SelectedItems;
            foreach (Infragistics.Web.UI.ListControls.DropDownItem item in items)
            {

                int popackid = int.Parse(item.Value.ToString());
                jobcontrlist.Add(popackid);
            }


            if (jobcontrlist.Count > 0 && jobcontrlist != null)
            {
                BLL.ProductionBLL.ProductionReportData jkmstrdata = new BLL.ProductionBLL.ProductionReportData();

                tbl_podetails.DataSource = jkmstrdata.GetDataForproductionreport(jobcontrlist);
                tbl_podetails.DataBind();
            }
        }

        protected void btn_submitShipment_Click(object sender, EventArgs e)
        {
            BLL.ProductionBLL.ProductionReportData prddata = new BLL.ProductionBLL.ProductionReportData();

            prddata.ProductionReportDetailsDataCollection = GetProductionReportDetailsData();

            prddata.insertProductionbReport(prddata);

            tbl_podetails.DataSource = null;
            tbl_podetails.DataBind();
            MessgeboxUpdate("sucess", "Production Data Entered");
           
        }



        public List<BLL.ProductionBLL.ProductionReportDetailsData> GetProductionReportDetailsData()
        {

            List<BLL.ProductionBLL.ProductionReportDetailsData> rk = new List<BLL.ProductionBLL.ProductionReportDetailsData>();


            foreach (GridViewRow di in tbl_podetails.Rows)
            {
                CheckBox chkBx = (CheckBox)di.FindControl("chk_select");

                if (chkBx != null && chkBx.Checked)
                {
                    int JobContractDetail_pk = int.Parse(((di.FindControl("lbl_JobContractDetail_pk") as Label).Text.ToString()));

                    int cutqty = int.Parse(((di.FindControl("txt_cutQty") as TextBox).Text.ToString()));
                    int sewnqty = int.Parse(((di.FindControl("txt_Sewnqty") as TextBox).Text.ToString()));
                    int washqty = int.Parse(((di.FindControl("txt_washedqty") as TextBox).Text.ToString()));
                    int packqty = int.Parse(((di.FindControl("txt_packedqty") as TextBox).Text.ToString()));
                

                    BLL.ProductionBLL.ProductionReportDetailsData shpdet = new BLL.ProductionBLL.ProductionReportDetailsData();
                    shpdet.JobContractDetail_pk = JobContractDetail_pk;
                    shpdet.ProductionDate = DateTime.Parse(productionDate.Value.ToString());

                    shpdet.CutQty = int.Parse(cutqty.ToString());
                    shpdet.SewnQty= int.Parse(sewnqty.ToString());
                    shpdet.WashedQty = int.Parse(washqty.ToString());
                    shpdet.PackedQty = int.Parse(packqty.ToString());
                   
                    shpdet.AddedBy = Session["Username"].ToString().Trim();
                    shpdet.AddedDate = DateTime.Now;

                    rk.Add(shpdet);
                }
            }
            return rk;


        }







        public void MessgeboxUpdate(String Messagetype, String Messg)
        {
            if (Messagetype == "sucess")
            {
                Messaediv.Attributes["class"] = "success";
                Messaediv.InnerText = Messg;
            }
            else
            {

            }
        }
    
    }
}
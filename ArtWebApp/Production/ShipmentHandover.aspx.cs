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
    public partial class ShipmentHandover : System.Web.UI.Page
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
                BLL.ProductionBLL.JobContractDetailData jkmstrdata = new BLL.ProductionBLL.JobContractDetailData();

                tbl_podetails.DataSource = jkmstrdata.GetJobContractdetailofList(jobcontrlist);
                tbl_podetails.DataBind();
            }
        }

        protected void btn_submitShipment_Click(object sender, EventArgs e)
        {
            string msg = "";
            String num = "";
            BLL.ProductionBLL.ShipmentHandOverMasterData SHPMSTR = new BLL.ProductionBLL.ShipmentHandOverMasterData();
            SHPMSTR.LocationPK_pk=int.Parse(drp_factory.SelectedValue.ToString());
            SHPMSTR.ShipmentHandOverMasterDataCollection = GetShipmentHandOverMasterData();

          num= SHPMSTR.insertShipmentHandOver(SHPMSTR);

          msg = "Shipment HandOver # : " + num + " is generated Sucessfully";
            tbl_podetails.DataSource = null;
            tbl_podetails.DataBind();
               MessgeboxUpdate("sucess", msg);
        }

        public List<BLL.ProductionBLL.ShipmentHandOverData> GetShipmentHandOverMasterData()
        {

            List<BLL.ProductionBLL.ShipmentHandOverData> rk = new List<BLL.ProductionBLL.ShipmentHandOverData>();


            foreach (GridViewRow di in tbl_podetails.Rows)
            {
                CheckBox chkBx = (CheckBox)di.FindControl("chk_select");

                if (chkBx != null && chkBx.Checked)
                {
                    int JobContractDetail_pk = int.Parse(((di.FindControl("lbl_JobContractDetail_pk") as Label).Text.ToString()));

                    int JCnewcm = int.Parse(((di.FindControl("txt_qty") as TextBox).Text.ToString()));
                    BLL.ProductionBLL.ShipmentHandOverData shpdet = new BLL.ProductionBLL.ShipmentHandOverData();
                    shpdet.JobContractDetail_pk = JobContractDetail_pk;
                    shpdet.ShipmenthandOverdate =DateTime.Parse ( shipdate.Value.ToString());
                    shpdet.ShippedQty = int.Parse(JCnewcm.ToString());
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
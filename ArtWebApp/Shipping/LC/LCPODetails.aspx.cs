using ArtWebApp.DataModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArtWebApp.Shipping.LC
{
    public partial class LCPODetails : System.Web.UI.Page
    {
        private ArtEntitiesnew enty = new ArtEntitiesnew();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_showlc_Click(object sender, EventArgs e)
        {
            try
            {
                loadLC(int.Parse(drp_supplier.SelectedItem.Value.ToString()));
            }
            catch (Exception)
            {


            }
        }

        public void loadLC(int supid)
        {
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {
                var q = from jcdata in enty.LCMasters
                        where jcdata.Supplier_pk == supid
                        select new
                        {
                            name = jcdata.LCNum,
                            pk = jcdata.LC_PK.ToString()
                        };
                drp_lc.DataSource = q.ToList();
                drp_lc.DataBind();
            }
        }

        public void loadPO(int supid)
        {
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {
                var q = from jcdata in enty.ProcurementMasters
                        where jcdata.Supplier_Pk == supid
                        select new
                        {
                            name = jcdata.PONum,
                            pk = jcdata.PO_Pk.ToString()
                        };
                drp_po.DataSource = q.ToList();
                drp_po.DataBind();
            }
        }


        public void loadSPO(int supid)
        {
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {
                var q = from jcdata in enty.StockPOMasters
                        where jcdata.Supplier_Pk == supid
                        select new
                        {
                            name = jcdata.SPONum,
                            pk = jcdata.SPO_Pk.ToString()
                        };
                drp_po.DataSource = q.ToList();
                drp_po.DataBind();
            }
        }


        protected void btn_showPO_Click(object sender, EventArgs e)
        {
            if(drp_Potype.SelectedItem.Text=="PO")
            {
                try
                {
                    loadPO(int.Parse(drp_supplier.SelectedItem.Value.ToString()));
                }
                catch (Exception)
                {


                }
            }
            else if (drp_Potype.SelectedItem.Text == "SPO")
            {
                try
                {
                    loadSPO(int.Parse(drp_supplier.SelectedItem.Value.ToString()));
                }
                catch (Exception)
                {


                }
            }
        }

        protected void btn_addpo_Click(object sender, EventArgs e)
        {
            ArrayList povaluelist = new ArrayList();
            ArrayList ponumlist = new ArrayList();
            List<Infragistics.Web.UI.ListControls.DropDownItem> items = drp_po.SelectedItems;
            BLL.ShippingBLL.LCdata lcdata = new BLL.ShippingBLL.LCdata();
            if (items.Count>0)
            {
                
                lcdata.LCDetailsDetailsDataCollection = lcdata.GetPOData(items, drp_Potype.SelectedItem.Text.Trim(), int.Parse(Session["lc_pk"].ToString ()));
                lcdata.insertProductionbReport();
            }
            Session["lc_pk"] = int.Parse(drp_lc.SelectedItem.Value.ToString());            
            tbl_podetails.DataSource = lcdata.getlcdata(int.Parse(drp_lc.SelectedItem.Value.ToString()));
            tbl_podetails.DataBind();
        }

        protected void button_showLCData_Click(object sender, EventArgs e)
        {
            Session["lc_pk"] = int.Parse(drp_lc.SelectedItem.Value.ToString());
            BLL.ShippingBLL.LCdata lcdata = new BLL.ShippingBLL.LCdata();
            int lcpk = int.Parse(drp_lc.SelectedItem.Value.ToString());
            Decimal lcvalue = 0; 
            var q = from lc in enty.LCMasters where lc.LC_PK == lcpk select lc;
            foreach(var element in q)
            {
                lcvalue = decimal.Parse(element.Value.ToString());
            }
            txt_lcvalue.Text = lcvalue.ToString();
            tbl_podetails.DataSource = lcdata.getlcdata(int.Parse(drp_lc.SelectedItem.Value.ToString()));
            tbl_podetails.DataBind();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Decimal totvalue = 0;
            foreach (GridViewRow di in tbl_podetails.Rows)
            {
                int lcdetpk_pk = int.Parse(((di.FindControl("lbl_lcdetpk") as Label).Text.ToString()));
                Decimal lcvalue = Decimal.Parse(((di.FindControl("txt_lcvalue") as TextBox).Text.ToString()));
                totvalue += Decimal.Parse(((di.FindControl("txt_lcvalue") as TextBox).Text.ToString()));
                String invoicenum = ((di.FindControl("txt_invoicenum") as TextBox).Text.ToString());
                BLL.ShippingBLL.LCDetailData shpdet = new BLL.ShippingBLL.LCDetailData();
                shpdet.LCDet_PK = lcdetpk_pk;
                shpdet.LCValue = lcvalue;
                shpdet.InvoiceNUM = invoicenum.Trim ();
                shpdet.AddedBy = Session["Username"].ToString().Trim();
                shpdet.AddedDate = DateTime.Now;
                shpdet.updatelcdet();
               
            }

        }
  
    
    
    
    
    }
}
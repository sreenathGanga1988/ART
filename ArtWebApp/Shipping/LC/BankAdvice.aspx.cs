using ArtWebApp.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArtWebApp.Shipping.LC
{
    public partial class BankAdvice : System.Web.UI.Page
    {
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

        protected void button_showLCData_Click(object sender, EventArgs e)
        {
            Session["lc_pk"] = int.Parse(drp_lc.SelectedItem.Value.ToString());
            BLL.ShippingBLL.LCdata lcdata = new BLL.ShippingBLL.LCdata();
            tbl_podetails.DataSource = lcdata.getlcdata(int.Parse(drp_lc.SelectedItem.Value.ToString()));
            tbl_podetails.DataBind();
        }













        public List<BLL.ShippingBLL.LCBankAdviceDetailsData> GetBankAdviceDetailData()
        {

            List<BLL.ShippingBLL.LCBankAdviceDetailsData> rk = new List<BLL.ShippingBLL.LCBankAdviceDetailsData>();

            
            foreach (GridViewRow di in tbl_podetails.Rows)
            {
                CheckBox chkBx = (CheckBox)di.FindControl("chk_select");

                if (chkBx != null && chkBx.Checked)
                {
                    int lbl_lcdetpk = int.Parse(((di.FindControl("lbl_lcdetpk") as Label).Text.ToString()));
                    decimal txt_lcvalue = decimal.Parse(((di.FindControl("txt_lcvalue") as TextBox).Text.ToString()));
                    string txt_invoicenum = ((di.FindControl("txt_invoicenum") as TextBox).Text.ToString());
                    BLL.ShippingBLL.LCBankAdviceDetailsData deldet = new BLL.ShippingBLL.LCBankAdviceDetailsData();
                    deldet.LCDet_PK = lbl_lcdetpk;
                    deldet.TrValue = txt_lcvalue;
                    deldet.Docnum = txt_invoicenum;
                    deldet.AddedBy = Session["Username"].ToString().Trim();
                    deldet.AddedDate = DateTime.Now;
                    rk.Add(deldet);
                }
            }
            return rk;


        }

        protected void btn_submitbankadvice_Click(object sender, EventArgs e)
        {
           
            BLL.ShippingBLL.LCBankAdviceDetailsDataMaster lcmstr = new BLL.ShippingBLL.LCBankAdviceDetailsDataMaster();
            lcmstr.LCBankAdviceDetailsDataCollection = GetBankAdviceDetailData();
            lcmstr.InsertLCBankAdviceDetails();
            MessgeboxUpdate("sucess",  " Bank Advice  Added Sucessfully");
            tbl_podetails.DataSource = null;
            tbl_podetails.DataBind();
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
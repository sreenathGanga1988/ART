using ArtWebApp.DataModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArtWebApp.Shipping
{
    public partial class Invoicing : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_showPO_Click(object sender, EventArgs e)
        {
            try
            {
                loadShipmentCode(int.Parse(drp_factory.SelectedValue.ToString()));
            }
            catch (Exception)
            {


            }
        }


        public void loadShipmentCode(int factid)
        {
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {
                var q = from shphndmstr in enty.ShipmentHandOverMasters
                        where shphndmstr.Location_Pk == factid
                        && shphndmstr.IsCompleted=="N"
                        select new
                        {
                            name = shphndmstr.ShipmentHandOverCode,
                            pk = shphndmstr.ShipmentHandMaster_PK.ToString()
                        };
                drp_shpcode.DataSource = q.ToList();
                drp_shpcode.DataBind();
            }
        }

        protected void btn_JCSubmit_Click(object sender, EventArgs e)
        {
           

            string msg = "";
            String num = "";
            if (ArtWebApp.Controls.Gridviewvalidation.countofRowselected(tbl_podetails, "chk_select") > 0)
            {

                BLL.ShippingBLL.InvoiceData invMSTR = new BLL.ShippingBLL.InvoiceData();
            invMSTR.Invmstrdata = GetINVMasterData();

            invMSTR.InvoiceDetailsDataCollection = InvoiceDetailsData();

            num = invMSTR.InsertInvoiceData(invMSTR);

            msg = "Invoice # : " + num + " is generated Sucessfully";
            tbl_podetails.DataSource = null;
            tbl_podetails.DataBind();
            MessgeboxUpdate("sucess", msg);
            }
            else
            {
                string Msg = "alert('Please select the Items to be added in this Invoice ')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", Msg, true);
            }
        }







        public BLL.ShippingBLL.InvoiceMasterData GetINVMasterData()
        {
            BLL.ShippingBLL.InvoiceMasterData invmstrdata = new BLL.ShippingBLL.InvoiceMasterData();
            invmstrdata.Bank_pk = int.Parse(drp_bank.SelectedValue.ToString());
            invmstrdata.Location_pk= int.Parse(drp_factory.SelectedValue.ToString());
            invmstrdata.AddedBy = Session["Username"].ToString().Trim();
            invmstrdata.refnum = txt_ref.Text.Trim();
            
            invmstrdata.AddedDate = DateTime.Now;

            return invmstrdata;
        }

        public List<BLL.ShippingBLL.InvoiceDetailsData> InvoiceDetailsData()
        {

            List<BLL.ShippingBLL.InvoiceDetailsData> rk = new List<BLL.ShippingBLL.InvoiceDetailsData>();


            foreach (GridViewRow di in tbl_podetails.Rows)
            {
                CheckBox chkBx = (CheckBox)di.FindControl("chk_select");

                if (chkBx != null && chkBx.Checked)
                {
                    
                    


                  int lbl_popackid = int.Parse(((di.FindControl("lbl_popackid") as Label).Text.ToString()));
                  int lbl_ourstyleid = int.Parse(((di.FindControl("lbl_ourstyleid") as Label).Text.ToString()));
                     int lbl_ShipmentHandOverPK = int.Parse(((di.FindControl("lbl_ShipmentHandOverPK") as Label).Text.ToString()));
                    int txt_qty = int.Parse(((di.FindControl("txt_qty") as TextBox).Text.ToString()));
                    int txt_Ctn = int.Parse(((di.FindControl("txt_Ctn") as TextBox).Text.ToString()));
                     float lbl_fob = float.Parse(((di.FindControl("lbl_fob") as Label).Text.ToString()));

                    float txt_fob = float.Parse(((di.FindControl("txt_newfob") as TextBox).Text.ToString()));

                    BLL.ShippingBLL.InvoiceDetailsData invdata = new BLL.ShippingBLL.InvoiceDetailsData();


                     invdata.FOB = txt_fob;
                     invdata.OurStyleID = lbl_ourstyleid;
                     invdata.InvoiceQty = txt_qty;
                     invdata.CartonNum = txt_Ctn;
                     invdata.PoPackID = lbl_popackid;
                     invdata.ShipmentHandOver_PK = lbl_ShipmentHandOverPK;

                     rk.Add(invdata);
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


        protected void Button1_Click(object sender, EventArgs e)
        {
            ArrayList SHPlist = new ArrayList();
            List<Infragistics.Web.UI.ListControls.DropDownItem> items = drp_shpcode.SelectedItems;
            foreach (Infragistics.Web.UI.ListControls.DropDownItem item in items)
            {

                int shpdata = int.Parse(item.Value.ToString());
                SHPlist.Add(shpdata);
            }


            if (SHPlist.Count > 0 && SHPlist != null)
            {
                BLL.ProductionBLL.ShipmentHandOverData jkmstrdata = new BLL.ProductionBLL.ShipmentHandOverData();

                tbl_podetails.DataSource = jkmstrdata.GetDataForShipmentData(SHPlist);
                tbl_podetails.DataBind();
                upd_grid.Update();
            }
        }
      

    }
}
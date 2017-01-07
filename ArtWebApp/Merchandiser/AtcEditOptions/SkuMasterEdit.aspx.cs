using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArtWebApp.Merchandiser.AtcEditOptions
{
    public partial class SkuMasterEdit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            String navtype = Request.QueryString["navtype"];

            string v = Request.QueryString["navtype"];
            if (navtype == "Master")
            {
                MultiView1.ActiveViewIndex = 0;
            }
            else if (navtype == "Detail")
            {
                MultiView1.ActiveViewIndex = 1;
            }
        }

        protected void buttonAtc_Click(object sender, EventArgs e)
        {
            HiddenField1.Value = cmb_atc.SelectedValue.ToString();
          
            tbl_skumaster.DataBind();
          
            upd_skugrid.Update();
        }

        protected void tbl_skumaster_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            Int32 skuid = Convert.ToInt32(tbl_skumaster.DataKeys[e.RowIndex].Value);
            BLL.MerchandsingBLL.SKUManager skumgr = new BLL.MerchandsingBLL.SKUManager();
           if( skumgr.DeleteSKUID(skuid)==true)
           {
               lbl_msg.Text = "Deleted";
               Upd_label.Update();
               tbl_skumaster.DataBind();

               upd_skugrid.Update();

           }
           else
           {
               lbl_msg.Text = "Cannot Delete SKU since PO Given";
               Upd_label.Update();
              
           }
        }

        protected void Btn_updatesku_Click(object sender, EventArgs e)
        {
         
        }

        protected void tbl_skumaster_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if(e.CommandName=="DeleteSkuDetails")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = tbl_skumaster.Rows[index];



                int skupk = int.Parse((row.FindControl("lbl_skuPK") as Label).Text);
                DBTransaction.SKUTransaction skucrtr = new DBTransaction.SKUTransaction();
                //Deletes the SKudetails first
                skucrtr.DeleteSKUDetailOfSkUID(skupk);

            }
        }

        protected void cmb_atc_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            tbl_skuDetails.DataBind();
            UpdatePanel2.Update();

        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            for(int i=0;i<tbl_skuDetails.Rows.Count;i++)
            {
                GridViewRow row = tbl_skuDetails.Rows[i];
                String chk_isreq = ((row.FindControl("chk_select") as CheckBox).Checked == true ? "Y" : "N");
                if (chk_isreq == "Y")
                {
                    int skudetpk = int.Parse((row.FindControl("lbl_skudetpk") as Label).Text);
                    DBTransaction.SKUTransaction skucrtr = new DBTransaction.SKUTransaction();
                    //Deletes the SKudetails first
                    skucrtr.DeleteaSKUDetail(skudetpk);
                }
            }
            tbl_skuDetails.DataBind();
            UpdatePanel2.Update();
        }
    }
}
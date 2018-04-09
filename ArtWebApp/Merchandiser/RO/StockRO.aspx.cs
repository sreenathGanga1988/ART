using ArtWebApp.BLL.ProcurementBLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArtWebApp.Merchandiser
{
    public partial class StockRO : System.Web.UI.Page
    {
        DBTransaction.ProcurementTransaction potrans = new DBTransaction.ProcurementTransaction();
        DataTable dt = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                dt = (DataTable)Session["ItemforPO"];
                tbl_Podetails.DataSource = dt;
                tbl_Podetails.DataBind();

            }
        }

        protected void tbl_Podetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void ShowBom_Click(object sender, EventArgs e)
        {
            // DataTable dt = potrans.GetDetailforRO(int.Parse(cmb_atc.SelectedValue.ToString()), int.Parse(Session["TemplatePk"].ToString ()));
            DBTransaction.InventoryTransaction.InventoryTransaction invtra = new DBTransaction.InventoryTransaction.InventoryTransaction();

            DataTable dt = invtra.GetGStockInventoryofanItem( int.Parse(Session["TemplatePk"].ToString()), int.Parse(cmb_warehouse.SelectedValue.ToString()));
            tbl_InverntoryDetails.DataSource = dt;
            tbl_InverntoryDetails.DataBind();
            UpdatePanel2.Update();
        }

        protected void chk_select_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chkbox = (CheckBox)sender;
            GridViewRow currentRow = (GridViewRow)chkbox.Parent.Parent;
            int rowindex = 0;
            rowindex = currentRow.RowIndex;

            Session["TemplatePk"] = int.Parse((tbl_Podetails.Rows[rowindex].FindControl("lbl_templatepk") as Label).Text);

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            int selectedrowsoftoitem = IsMultipleRowselected(tbl_Podetails, "chk_select");
            int seletedfromitems = IsMultipleRowselected(tbl_InverntoryDetails, "chk_selectitem");
            lbl_message.Text = "";
            if (selectedrowsoftoitem == 0)
            {
                lbl_message.Text = "Select the item to which transfer should happen";
            }
            else if (seletedfromitems == 0)
            {
                lbl_message.Text = "Select the item from  which transfer should happen";
            }
            else
            {
                getRowDetails();

            }
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
                Messaediv.Attributes["class"] = "error-message";
                Messaediv.InnerText = Messg;
            }
        }



        public void getRowDetails()
        {
            RequestOrderMasterData rmstr = new RequestOrderMasterData();
            RoDetailsData rddet = new RoDetailsData();



           int toskudetpk = 0;
            foreach (GridViewRow di in tbl_Podetails.Rows)
            {
                CheckBox chkBx = (CheckBox)di.FindControl("chk_select");

                if (chkBx != null && chkBx.Checked)
                {
                    toskudetpk = int.Parse((di.FindControl("lbl_skudet_pk") as Label).Text);

                }
            }
            Boolean isOnhandlesser = false;
            List<RoDetailsData> rk = new List<RoDetailsData>();
            foreach (GridViewRow di in tbl_InverntoryDetails.Rows)
            {
                CheckBox chkBx = (CheckBox)di.FindControl("chk_selectitem");


                if (chkBx != null && chkBx.Checked)
                {
                    //lblInventoryItem_PK

                    rddet = new RoDetailsData();

                    rddet.ToSkuDet_PK = toskudetpk;
                    rddet.Qty = Decimal.Parse((di.FindControl("txt_deliveryQty") as TextBox).Text);
                    rddet.InventoryItem_PK = int.Parse((di.FindControl("lblInventoryItem_PK") as Label).Text);
                    rddet.UnitPrice = Decimal.Parse((di.FindControl("lbl_fromrate") as Label).Text); 
                    rddet.OnhandQty = Decimal.Parse((di.FindControl("lbl_onhandQty") as Label).Text);                   
                    rk.Add(rddet);

                    if (rddet.OnhandQty < rddet.Qty) {
                        isOnhandlesser = true;
                    }
                }

            }



            if (isOnhandlesser == false)
            {
                rmstr.RoDetailsDataCollection = rk;
                rmstr.ToSkuDet_PK = toskudetpk;
                rmstr.Location_Pk = int.Parse(cmb_warehouse.SelectedValue.ToString());
                String ro = rmstr.insertStockRowmaterial(rmstr);
                string msg = "Ro# '" + ro + "' Generated Successfully";
                MessgeboxUpdate("sucess", msg);
            }
            else
            {
                string msg = "Cannot Create Ro greater than Onhand";
                MessgeboxUpdate("error", msg);
            }
          
        }






        public int IsMultipleRowselected(GridView tbldata, String checkboxname)
        {

            int selectedrowscount = 0;
            foreach (GridViewRow di in tbldata.Rows)
            {
                CheckBox chkBx = (CheckBox)di.FindControl(checkboxname);

                if (chkBx != null && chkBx.Checked)
                {

                    //get the uniqueID of that row
                    selectedrowscount = selectedrowscount + 1;
                }
            }



            return selectedrowscount;
        }
    }
}
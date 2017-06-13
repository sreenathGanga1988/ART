using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArtWebApp.Inventory
{
    public partial class TransferToGtock : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["TemplatePk"] = 0;
                DataTable dt = new DataTable();
                dt = (DataTable)Session["ItemforTransfer"];
                tbl_InverntoryDetails.DataSource = dt;
                tbl_InverntoryDetails.DataBind();
            }
        }

        protected void btn_confirmtransfer_Click(object sender, EventArgs e)
        {
          String reqnum=  getdata();
            tbl_InverntoryDetails.DataSource = null;
            tbl_InverntoryDetails.DataBind();
            String msg = "Transfer Request is submitted Sucessfully";
            MessageBoxShow(msg);
        }
        public void MessageBoxShow(String msg)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "Art", "alert('" + msg + "');", true);

            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('" + msg + "');", true);
        }



        public String getdata()
        {
            String reqnum = "";

               BLL.InventoryBLL.AtcToGstockTransfermaster atcgstockmstr = new BLL.InventoryBLL.AtcToGstockTransfermaster();

            atcgstockmstr.CreatedDate = DateTime.Now;
            atcgstockmstr.AddedBy = Session["Username"].ToString().Trim();
            atcgstockmstr.Location_Pk = int.Parse(Session["UserLoc_pk"].ToString());

            atcgstockmstr.IsApproved = "N";
            atcgstockmstr.IsDeleted = "N";
            atcgstockmstr.CreatedDate = DateTime.Now;

            atcgstockmstr.AtcToGstockTransferDetailsCollection = GetTransferdetDetailsData();

            reqnum = atcgstockmstr.insertAtcToGstockData(atcgstockmstr);


            return reqnum;
        }


        public List<BLL.InventoryBLL.AtcToGstockTransferDetails> GetTransferdetDetailsData()
        {

            BLL.InventoryBLL.AtcToGstockTransferDetails invtrndata = null;
            List<BLL.InventoryBLL.AtcToGstockTransferDetails> rk = new List<BLL.InventoryBLL.AtcToGstockTransferDetails>();
            foreach (GridViewRow di in tbl_InverntoryDetails.Rows)
            {

                invtrndata = new BLL.InventoryBLL.AtcToGstockTransferDetails();
                invtrndata.InventoryItemPK = int.Parse((di.FindControl("lblInventoryItem_PK") as Label).Text);
                invtrndata.FromSkudet_PK = int.Parse((di.FindControl("lbl_fromSkuDet_Pk") as Label).Text);
                invtrndata.Template_PK = int.Parse((di.FindControl("lbl_templatepk") as Label).Text);
                invtrndata.OldUnitprice = Decimal.Parse((di.FindControl("lbl_fromcurate") as Label).Text);
                invtrndata.ReceivedQty = Decimal.Parse((di.FindControl("txt_deliveryQty") as TextBox).Text);
                invtrndata.NewUnitprice = Decimal.Parse((di.FindControl("txt_newrate") as TextBox).Text);


                rk.Add(invtrndata);

            }


            return rk;


        }
    }
}
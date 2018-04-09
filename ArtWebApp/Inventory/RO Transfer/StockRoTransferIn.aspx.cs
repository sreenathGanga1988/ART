using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArtWebApp.Inventory.RO_Transfer
{
    public partial class StockRoTransferIn : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            showrodata();
        }


        public void showrodata()
        {
            BLL.ProcurementBLL.RoDetailsData rodetdata = new BLL.ProcurementBLL.RoDetailsData();
            rodetdata.RO_Pk = int.Parse(drp_ro.SelectedItem.Value.ToString());
            tbl_Podetails.DataSource = rodetdata.StocRodetails1;
            tbl_Podetails.DataBind();
        }









        public void DoRoTransfer()
        {
            String mrnum = "";
            BLL.InventoryBLL.ROIN roin = new BLL.InventoryBLL.ROIN();
            roin.RoinmastrData = getRoInMasterData();
            roin.rodetaildata = GetRODetailsData();

            mrnum = roin.insertStockRomaterial(roin);
            tbl_Podetails.DataSource = null;
            tbl_Podetails.DataBind();

            String msg = "Transfer # : " + mrnum + " is generated Sucessfully";

           
            MessageBoxShow(msg);

        }

        public void MessageBoxShow(String msg)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "Art", "alert('" + msg + "');", true);
        }
        public BLL.InventoryBLL.ROINMasterData getRoInMasterData()
        {

            BLL.InventoryBLL.ROINMasterData romstr = new BLL.InventoryBLL.ROINMasterData();


            romstr.AddedDate = DateTime.Now;
            romstr.AddedBy = Session["Username"].ToString().Trim();
            romstr.RO_Pk = int.Parse(drp_ro.SelectedItem.Value.ToString());

            romstr.Location_pk = int.Parse(Session["UserLoc_pk"].ToString());

            return romstr;
        }

        public List<BLL.ProcurementBLL.RoDetailsData> GetRODetailsData()
        {

            List<BLL.ProcurementBLL.RoDetailsData> rk = new List<BLL.ProcurementBLL.RoDetailsData>();
            foreach (GridViewRow di in tbl_Podetails.Rows)
            {
                CheckBox chkBx = (CheckBox)di.FindControl("chk_select");

                if (chkBx != null && chkBx.Checked)
                {

                    int rodet_pk = int.Parse(((di.FindControl("lbl_rodetPk") as Label).Text.ToString()));
                    Decimal cunitrate = Decimal.Parse(((di.FindControl("lbl_cunitrate") as Label).Text.ToString()));
                    /*      int frmskudet_pk = int.Parse(((di.FindControl("lbl_FromSkuDet_PK") as Label).Text.ToString()));*/
                    int toskudet_pk = int.Parse(((di.FindControl("lbl_ToSkuDet_PK") as Label).Text.ToString()));
                    int inventoryitempk = int.Parse(((di.FindControl("lbl_InventoryItem_PK") as Label).Text.ToString()));
                    Decimal qty = Decimal.Parse(((di.FindControl("lbl_txtQty") as Label).Text.ToString()));

                    BLL.ProcurementBLL.RoDetailsData rodetdata = new BLL.ProcurementBLL.RoDetailsData();

                    rodetdata.RODet_Pk = rodet_pk;
                    rodetdata.ToSkuDet_PK = toskudet_pk;
                    //rodetdata.FromSkuDet_PK = frmskudet_pk;
                    rodetdata.InventoryItem_PK = inventoryitempk;
                    rodetdata.Qty = qty;
                    rodetdata.UnitPrice = cunitrate;


                    rk.Add(rodetdata);
                }
            }



            return rk;


        }

        protected void Btn_TransferRO_Click(object sender, EventArgs e)
        {
            if (!IsNORowselected())
            {
                DoRoTransfer();
                lbl_errordisplayer.Text = "*";
            }
            else
            {
                lbl_errordisplayer.Text = "Select Item To Transfer";
            }

        }


        public Boolean IsNORowselected()
        {
            Boolean isnorowselected = false;
            int selectedrowscount = 0;
            foreach (GridViewRow di in tbl_Podetails.Rows)
            {
                CheckBox chkBx = (CheckBox)di.FindControl("chk_select");

                if (chkBx != null && chkBx.Checked)
                {

                    //get the uniqueID of that row
                    selectedrowscount = selectedrowscount + 1;
                }
            }

            if (selectedrowscount == 0)
            {
                isnorowselected = true;
            }

            return isnorowselected;
        }










    }
}
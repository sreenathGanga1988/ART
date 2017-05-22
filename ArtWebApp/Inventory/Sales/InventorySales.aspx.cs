using ArtWebApp.DataModels;
using ArtWebApp.DBTransaction;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArtWebApp.Inventory
{
    public partial class InventorySales : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                filltowarehouses();
            }
        }

        protected void btn_confirmAtc_Click(object sender, EventArgs e)
        {
            DeliveryOrdertransaction dotran = new DeliveryOrdertransaction();
            DataTable dt = dotran.GetGeneralStockDetails(int.Parse(Session["UserLoc_pk"].ToString()));
            tbl_InverntoryDetails.DataSource = dt;
            tbl_InverntoryDetails.DataBind();
        }

        protected void btn_saveDO_Click(object sender, EventArgs e)
        {
            if (checkdatagridValue(tbl_InverntoryDetails, "lbl_onhandQty", "txt_deliveryQty"))
            {
                InsertDOdata();
            }
        }

        public void InsertDOdata()
        {

           

            BLL.InventoryBLL.SalesMasterData slmstrdata = GetDoMasterData();
            slmstrdata.SalesDetailsDataCollection = GetDeliveryOrderDetailsData();
            String donum = slmstrdata.InsertSalesDOInternal();


            String msg = "DO # : " + donum + " is generated Sucessfully";


            MessgeboxUpdate("sucess", msg);


        }


        public BLL.InventoryBLL.SalesMasterData GetDoMasterData()
        {
            BLL.InventoryBLL.SalesMasterData slmstrdata = new BLL.InventoryBLL.SalesMasterData();
            
            slmstrdata.AddedDate = DateTime.Now;
            slmstrdata.ContainerNumber = txt_containernum.Text;
            slmstrdata.SalesDate = DateTime.Parse(dtp_dodate.Value.ToString());
            slmstrdata.BoeNum = txt_BOE_no.Text;
            slmstrdata.AddedBy = Session["Username"].ToString().Trim();
            slmstrdata.ToLocation_PK = int.Parse(drp_ToWarehouse.SelectedValue.ToString()); ;
            slmstrdata.FromLocation_PK = int.Parse(Session["UserLoc_pk"].ToString());
            slmstrdata.DoType = "SL";

            return slmstrdata;
        }

        public List<BLL.InventoryBLL.SalesDetailsData> GetDeliveryOrderDetailsData()
        {

            List<BLL.InventoryBLL.SalesDetailsData> rk = new List<BLL.InventoryBLL.SalesDetailsData>();


            foreach (GridViewRow di in tbl_InverntoryDetails.Rows)
            {
                CheckBox chkBx = (CheckBox)di.FindControl("chk_select");

                if (chkBx != null && chkBx.Checked)
                {
                    int item_pk = int.Parse(((di.FindControl("lblInventoryItem_PK") as Label).Text.ToString()));
                    decimal deliveryqty = decimal.Parse(((di.FindControl("txt_deliveryQty") as TextBox).Text.ToString()));
                    decimal curate = decimal.Parse(((di.FindControl("lbl_curate") as Label).Text.ToString()));
                    BLL.InventoryBLL.SalesDetailsData deldet = new BLL.InventoryBLL.SalesDetailsData();
                    deldet.SInventoryItem_PK = item_pk;
                    deldet.CUrate = curate;
                    deldet.DeliveryQty = deliveryqty;
                    
                   rk.Add(deldet);
                }
            }
            return rk;


        }


        public Boolean checkdatagridValue(GridView tblgrid, String lbl_Qty1, String txt_Qty2)
        {

            Boolean isQtyok = true;
            for (int i = 0; i < tblgrid.Rows.Count; i++)
            {
                GridViewRow currentRow = tblgrid.Rows[i];
                CheckBox chkBx = (CheckBox)currentRow.FindControl("chk_select");

                if (chkBx != null && chkBx.Checked)
                {
                    try
                    {

                        float AllowedQty = float.Parse(((tblgrid.Rows[i].FindControl(lbl_Qty1) as Label).Text.ToString()));
                        float Enterqty = float.Parse(((tblgrid.Rows[i].FindControl(txt_Qty2) as TextBox).Text.ToString()));
                        if (!QuantityValidator.ISFloatQuantityLesser(AllowedQty, Enterqty))
                        {
                            isQtyok = false;
                            (tblgrid.Rows[i].FindControl(txt_Qty2) as TextBox).BackColor = System.Drawing.Color.Red;


                        }
                        else
                        {
                            (tblgrid.Rows[i].FindControl(txt_Qty2) as TextBox).BackColor = System.Drawing.Color.White;
                        }

                    }
                    catch (Exception)
                    {
                        isQtyok = false;
                        (tblgrid.Rows[i].FindControl(txt_Qty2) as TextBox).BackColor = System.Drawing.Color.Red;

                    }
                }







            }
            return isQtyok;
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
        public void filltowarehouses()
        {
            using (ArtEntitiesnew entty = new ArtEntitiesnew())
            {
                var q = from order in entty.LocationMasters
                        where order.LocType == "W"
                        select new
                        {
                            name = order.LocationName,
                            pk = order.Location_PK
                        };
                var q1 = from order in entty.DeliveryMethodMasters
                         select new
                         {
                             name = order.DeliveryMethod,
                             pk = order.Deliverymethod_Pk
                         };
                // Create a table from the query.
                drp_ToWarehouse.DataSource = q.ToList();
                drp_ToWarehouse.DataBind();

                drp_deliverymode.DataSource = q1.ToList();
                drp_deliverymode.DataBind();

                // Bind the table to a System.Windows.Forms.BindingSource object, 
                // which acts as a proxy for a System.Windows.Forms.DataGridView object.

            }
        }

        protected void tbl_InverntoryDetails_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
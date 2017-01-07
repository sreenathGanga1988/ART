using ArtWebApp.DataModels;
using ArtWebApp.DBTransaction;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArtWebApp.Inventory.DeliveryOrder
{
    public partial class DeliveryOrderStockCountry : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack )
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
            btn_saveDO.Enabled = true;
        }

        protected void btn_saveDO_Click(object sender, EventArgs e)
        {
            if (checkdatagridValue(tbl_InverntoryDetails, "lbl_onhandQty", "txt_deliveryQty"))
            {
               // btn_saveDO.Enabled = false;
                InsertDOdata();
            }
        }

        public void InsertDOdata()
        {

            BLL.InventoryBLL.StockDeliveryOrder dodata = new BLL.InventoryBLL.StockDeliveryOrder();

            dodata.Domstrdata = GetDoMasterData();
            dodata.DeliveryOrderDetailsDataCollection = GetDeliveryOrderDetailsData();
            String donum = dodata.insertStockWarehouseDO(dodata);


            String msg = "DO # : " + donum + " is generated Sucessfully";


            MessgeboxUpdate("sucess", msg);


        }


        public BLL.InventoryBLL.StockDeliveryOrderMasterData GetDoMasterData()
        {
            BLL.InventoryBLL.StockDeliveryOrderMasterData domstr = new BLL.InventoryBLL.StockDeliveryOrderMasterData();
            domstr.AtcID = 0 ;
            domstr.AddedDate = DateTime.Now;
            domstr.ContainerNumber = txt_containernum.Text;
            domstr.DeliveryDate = DateTime.Parse(dtp_dodate.Value.ToString());
            domstr.BoeNum = txt_containernum.Text;
            domstr.AddedBy = Session["Username"].ToString().Trim();
            domstr.ToLocation_PK = int.Parse(drp_ToWarehouse.SelectedValue.ToString()); ;
            domstr.FromLocation_PK = int.Parse(Session["UserLoc_pk"].ToString());
            domstr.DoType = "WW";

            return domstr;
        }

        public List<BLL.InventoryBLL.StockDeliveryOrderDetailsData> GetDeliveryOrderDetailsData()
        {

            List<BLL.InventoryBLL.StockDeliveryOrderDetailsData> rk = new List<BLL.InventoryBLL.StockDeliveryOrderDetailsData>();


            foreach (GridViewRow di in tbl_InverntoryDetails.Rows)
            {
                CheckBox chkBx = (CheckBox)di.FindControl("chk_select");

                if (chkBx != null && chkBx.Checked)
                {
                    int item_pk = int.Parse(((di.FindControl("lblInventoryItem_PK") as Label).Text.ToString()));
                    decimal deliveryqty = decimal.Parse(((di.FindControl("txt_deliveryQty") as TextBox).Text.ToString()));
                    BLL.InventoryBLL.StockDeliveryOrderDetailsData deldet = new BLL.InventoryBLL.StockDeliveryOrderDetailsData();
                    deldet.InventoryItem_PK = item_pk;
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
    }
}
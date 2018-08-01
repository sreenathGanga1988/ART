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
    public partial class DeliveryOrderFactory : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            btn_saveDO.Attributes.Add("onclick", " this.disabled = true; " + ClientScript.GetPostBackEventReference(btn_saveDO, null) + ";");
            if (!IsPostBack)
            {
                
                filltowarehouses(int.Parse(Session["UserLoc_pk"].ToString()));
            }
        }

        protected void btn_saveDO_Click(object sender, EventArgs e)
        {
            if (checkdatagridValue(tbl_InverntoryDetails, "lbl_onhandQty", "txt_deliveryQty"))
            {
               // btn_saveDO.Enabled = false;
                InsertDOdata();
               // btn_saveDO.Enabled = true;
            }
        }

        protected void btn_confirmAtc_Click(object sender, EventArgs e)
        {
            DeliveryOrdertransaction dotran = new DeliveryOrdertransaction();
            DataTable dt = new DataTable();
            if (int.Parse(Session["UserLoc_pk"].ToString())==6) {
                dt = dotran.GetStockDetails(int.Parse(cmb_atc.SelectedValue.ToString()), int.Parse(Session["UserLoc_pk"].ToString()));
            }
            else
            {
                dt = dotran.GetStockItemDetails(int.Parse(cmb_atc.SelectedValue.ToString()), int.Parse(Session["UserLoc_pk"].ToString()), "Trims");
            }
         
            tbl_InverntoryDetails.DataSource = dt;
            tbl_InverntoryDetails.DataBind();
            upd_grid.Update();
            btn_saveDO.Enabled = true;
        }


        public void filltowarehouses(int location_pk)
        {
            using (ArtEntitiesnew entty = new ArtEntitiesnew())
            {
                var q = from order in entty.LocationMasters
                        join fwm in entty.FactWareLinkMasters on order.Location_PK equals fwm.ToLoc_PK
                        where fwm.FromLoc_pk == location_pk
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
                   drp_ToWarehouse.DataValueField = "pk";
                   drp_ToWarehouse.DataTextField = "name";
                drp_ToWarehouse.DataBind();

                drp_deliverymode.DataSource = q1.ToList();
                drp_deliverymode.DataValueField = "pk";
                drp_deliverymode.DataTextField = "name";
                drp_deliverymode.DataBind();

                // Bind the table to a System.Windows.Forms.BindingSource object, 
                // which acts as a proxy for a System.Windows.Forms.DataGridView object.

            }
        }





        public void InsertDOdata()
        {

            BLL.InventoryBLL.DeliveryOrder dodata = new BLL.InventoryBLL.DeliveryOrder();

            dodata.Domstrdata = GetDoMasterData();
            dodata.DeliveryOrderDetailsDataCollection = GetDeliveryOrderDetailsData();

            if (dodata.CheckWhetherQtyAvaialblefortransfer(dodata))
            {
                String donum = dodata.insertFactoryDO(dodata);
                tbl_InverntoryDetails.DataSource = null;
                tbl_InverntoryDetails.DataBind();
                upd_grid.Update();
                String msg = "DO # : " + donum + " is generated Sucessfully";

                MessgeboxUpdate("sucess", msg);
            }

            else
            {
                String msg = "Stock Not Available in Location";
                tbl_InverntoryDetails.DataSource = null;
                tbl_InverntoryDetails.DataBind();
                upd_grid.Update();

                MessgeboxUpdate("fail", msg);
            }


        }




        public BLL.InventoryBLL.DeliveryOrderMasterData GetDoMasterData()
        {
            BLL.InventoryBLL.DeliveryOrderMasterData domstr = new BLL.InventoryBLL.DeliveryOrderMasterData();
            domstr.AtcID = int.Parse(cmb_atc.SelectedValue.ToString()); ;
            domstr.AddedDate = DateTime.Now;
            domstr.ContainerNumber = txt_containernum.Text;
            domstr.DeliveryDate = DateTime.Parse(dtp_dodate.Value.ToString());
            domstr.BoeNum = txt_containernum.Text;
            domstr.AddedBy = Session["Username"].ToString().Trim();
            domstr.ToLocation_PK = int.Parse(drp_ToWarehouse.SelectedValue.ToString()); ;
            domstr.FromLocation_PK = int.Parse(Session["UserLoc_pk"].ToString());
            domstr.DoType = "WF";

            return domstr;
        }

        public List<BLL.InventoryBLL.DeliveryOrderDetailsData> GetDeliveryOrderDetailsData()
        {

            List<BLL.InventoryBLL.DeliveryOrderDetailsData> rk = new List<BLL.InventoryBLL.DeliveryOrderDetailsData>();


            foreach (GridViewRow di in tbl_InverntoryDetails.Rows)
            {
                CheckBox chkBx = (CheckBox)di.FindControl("chk_select");

                if (chkBx != null && chkBx.Checked)
                {
                    int item_pk = int.Parse(((di.FindControl("lblInventoryItem_PK") as Label).Text.ToString()));
                    decimal deliveryqty = decimal.Parse(((di.FindControl("txt_deliveryQty") as TextBox).Text.ToString()));
                    BLL.InventoryBLL.DeliveryOrderDetailsData deldet = new BLL.InventoryBLL.DeliveryOrderDetailsData();
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

        protected void lnkbtn_mrn_Click(object sender, EventArgs e)
        {
            LinkButton txtcons = (LinkButton)sender;
            GridViewRow currentRow = txtcons.ClosestContainer<GridViewRow>();
            DBTransaction.InventoryTransaction.InventoryTransaction invtran = new DBTransaction.InventoryTransaction.InventoryTransaction();
            int podetpk = int.Parse(((currentRow.FindControl("lblInventoryItem_PK") as Label).Text.ToString()));
            DataTable dt = invtran.GetNonCompletedTransactionofaIIT_PK(podetpk);

            GridView1.DataSource = dt;
            GridView1.DataBind();
            upd_subgrid.Update();
            ModalPopupExtender1.Show();
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

        protected void SqlDataSource1_Selecting()
        {

        }
    }
}
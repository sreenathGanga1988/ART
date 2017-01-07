using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ArtWebApp.DataModels;
using ArtWebApp.DBTransaction;
using System.Data;
namespace ArtWebApp.Inventory.Inventory_Requests
{
    public partial class InventoryMisplaced : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
          if( !IsPostBack)
            {
                filltowarehouses();
            }
        }

        protected void btn_confirmAtc_Click(object sender, EventArgs e)
        {
            DeliveryOrdertransaction dotran = new DeliveryOrdertransaction();
            DataTable dt = dotran.GetStockDetails(int.Parse(cmb_atc.SelectedValue.ToString()), int.Parse(drp_fromWarehouse.SelectedValue.ToString ()));
            tbl_InverntoryDetails.DataSource = dt;
            tbl_InverntoryDetails.DataBind();
            filltowarehouses();
        }




        public void filltowarehouses()
        {
            using (ArtEntitiesnew entty = new ArtEntitiesnew())
            {
                var q = from order in entty.LocationMasters
                        where order.LocType == "W" || order.LocType == "F"
                        select new
                        {
                            name = order.LocationName,
                            pk = order.Location_PK
                        };
              
                // Create a table from the query.
                drp_fromWarehouse.DataSource = q.ToList();
                drp_fromWarehouse.DataValueField = "pk";
                drp_fromWarehouse.DataTextField = "name";
                drp_fromWarehouse.DataBind();

              

                // Bind the table to a System.Windows.Forms.BindingSource object, 
                // which acts as a proxy for a System.Windows.Forms.DataGridView object.

            }
        }


        public void InsertDOdata()
        {

            BLL.InventoryBLL.InventoryMissingRequestData dodata = new BLL.InventoryBLL.InventoryMissingRequestData();
            dodata.Explanation = txt_explanation.Text.Trim();
            dodata.Domstrdata = GetDoMasterData();
            dodata.DeliveryOrderDetailsDataCollection = GetDeliveryOrderDetailsData();
            String donum = dodata.insertMissingInventoryRequest(dodata);

            tbl_InverntoryDetails.DataSource = null;
            tbl_InverntoryDetails.DataBind();
            String msg = donum + " Submitted Sucessfully";


      ArtWebApp.Controls.Messagebox.MessgeboxUpdate(Messaediv, "sucess", msg);


        }




        public BLL.InventoryBLL.DeliveryOrderMasterData GetDoMasterData()
        {
            BLL.InventoryBLL.DeliveryOrderMasterData domstr = new BLL.InventoryBLL.DeliveryOrderMasterData();
            domstr.AtcID = int.Parse(cmb_atc.SelectedValue.ToString()); ;
            domstr.AddedDate = DateTime.Now;
            domstr.ContainerNumber = txt_explanation.Text;
            domstr.DeliveryDate = DateTime.Parse(dtp_dodate.Value.ToString());
            domstr.BoeNum = txt_explanation.Text;
            domstr.AddedBy = Session["Username"].ToString().Trim();
           
            domstr.FromLocation_PK = int.Parse(Session["UserLoc_pk"].ToString());
            domstr.DoType = "MM";

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


        protected void btn_saveDO_Click(object sender, EventArgs e)
        {
            if (checkdatagridValue(tbl_InverntoryDetails, "lbl_onhandQty", "txt_deliveryQty"))
            {
                InsertDOdata();
            }
        }

        //public void MessgeboxUpdate(String Messagetype, String Messg)
        //{
        //    if (Messagetype == "sucess")
        //    {
        //        Messaediv.Attributes["class"] = "success";
        //        Messaediv.InnerText = Messg;
        //    }
        //    else
        //    {
        //        Messaediv.Attributes["class"] = "error-message";
        //        Messaediv.InnerText = Messg;
        //    }
        //}
    }
}
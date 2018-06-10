using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ArtWebApp.DBTransaction;
using System.Data;
using ArtWebApp.DataModels;
namespace ArtWebApp.Inventory
{
    public partial class DeliveryOrderCountry : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            btn_saveDO.Attributes.Add("onclick", " this.disabled = true; " + ClientScript.GetPostBackEventReference(btn_saveDO, null) + ";");
        }

        protected void btn_confirmAtc_Click(object sender, EventArgs e)
        {
            DeliveryOrdertransaction dotran = new DeliveryOrdertransaction();
            DataTable dt = dotran.GetStockDetails(int.Parse(cmb_atc.SelectedValue.ToString()), int.Parse(Session["UserLoc_pk"].ToString()));
            tbl_InverntoryDetails.DataSource = dt;
            tbl_InverntoryDetails.DataBind();
            filltowarehouses();
            btn_saveDO.Enabled = true; 
        }




        public void filltowarehouses()
        {
            using (ArtEntitiesnew entty = new ArtEntitiesnew())
            {
                var q = from order in entty.LocationMasters
                        where order.LocType=="W"
                        select new
                        {
                            name = order.LocationName,
                            pk=order.Location_PK
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
                String donum = dodata.insertWarehouseDO(dodata);

                tbl_InverntoryDetails.DataSource = null;
                tbl_InverntoryDetails.DataBind();
                String msg = "DO # : " + donum + " is generated Successfully";


                MessgeboxUpdate("sucess", msg);
            }

            else
            {
                String msg = "Stock Not Available in Location";
                tbl_InverntoryDetails.DataSource = null;
                tbl_InverntoryDetails.DataBind();
                

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
            domstr.BoeNum = txt_BOE_no.Text;
            domstr.AddedBy = Session["Username"].ToString().Trim();
            domstr.ToLocation_PK = int.Parse(drp_ToWarehouse.SelectedValue.ToString()); ;
            domstr.FromLocation_PK = int.Parse(Session["UserLoc_pk"].ToString());
            domstr.ExportContainer = drp_expref.Text;
            
            domstr.DoType = "WW";

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
                    BLL.InventoryBLL.DeliveryOrderDetailsData  deldet= new BLL.InventoryBLL.DeliveryOrderDetailsData();
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
                        if (Enterqty==0)
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
                if (ArtWebApp.Controls.Gridviewvalidation.countofRowselected(tbl_InverntoryDetails, "chk_select") > 0)
                {
                    InsertDOdata();









                }
                else
                {
                    string Msg = "alert('Please select the Items to be added in this WW ')";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", Msg, true);
                }
               

              //  btn_saveDO.Enabled = false;
            }
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

        protected void cmb_atc_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
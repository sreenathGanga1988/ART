using ArtWebApp.DataModels;
using ArtWebApp.DBTransaction;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ArtWebApp.BLL.InventoryBLL;
using System.Collections;

namespace ArtWebApp.Inventory.DeliveryOrder
{
    
    public partial class TransferToEndbit : System.Web.UI.Page
    {
        private ArtEntitiesnew enty = new ArtEntitiesnew();
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        protected void btn_confirmAtc_Click(object sender, EventArgs e)
        {
            DeliveryOrdertransaction dotran = new DeliveryOrdertransaction();
            DataTable dt = dotran.GetFabricToEndbit(int.Parse(Session["UserLoc_pk"].ToString()));
            tbl_InverntoryDetails.DataSource = dt;
            tbl_InverntoryDetails.DataBind();
            btn_saveDO.Enabled = true;
            //UpdatePanel1.Update();
        }


        protected void btn_confirminvdet_Click(object sender, EventArgs e)
        {
            lbl_ebdbit.Text = "0";


            lbl_ebdbit.Text = calculategridQty().ToString();
            upd_endbit.Update();
        }
        public float calculategridQty()
        {
            float Invqty = 0;
            for (int i = 0; i < tbl_InverntoryDetails.Rows.Count; i++)
            {
                GridViewRow currentRow = tbl_InverntoryDetails.Rows[i];
                CheckBox chkBx = (CheckBox)currentRow.FindControl("chk_select");

                if (chkBx != null && chkBx.Checked)
                {
                    try
                    {
                        Invqty = Invqty + float.Parse(((tbl_InverntoryDetails.Rows[i].FindControl("txt_deliveryQty") as TextBox).Text.ToString()));
                    }
                    catch (Exception)
                    {

                    }
                }
            }
            return Invqty;
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
        protected void btn_saveDO_Click(object sender, EventArgs e)
        {
            if (checkdatagridValue(tbl_InverntoryDetails, "lblOnhandQty", "txt_deliveryQty"))
            {
                btn_saveDO.Enabled = false;
                string msg = InsertDOdata();
                tbl_InverntoryDetails.DataSource = null;
                tbl_InverntoryDetails.DataBind();
                MessgeboxUpdate("sucess", "EndBit Do is Generated   " + msg);
            }
            else
            {
                MessgeboxUpdate("Error", "Check the Entry");
            }


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
                        else if (Enterqty < 0)
                        {
                            isQtyok = false;

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
        public string InsertDOdata()
        {
            String Donum = "";
            int userloc = int.Parse(Session["UserLoc_pk"].ToString());
            int toloc = 0;

            var q = from loc in enty.FactWareLinkMasters where loc.ToLoc_PK == userloc select loc;
            foreach (var element in q)
            {
                toloc = int.Parse(element.FromLoc_pk.ToString());
            }

            foreach (GridViewRow di in tbl_InverntoryDetails.Rows)
            {
                CheckBox chkBx = (CheckBox)di.FindControl("chk_select");

                if (chkBx != null && chkBx.Checked)
                {
                    int inv_pk = int.Parse(((di.FindControl("lblInventoryItem_PK") as Label).Text.ToString()));
                    int skudet_pk = int.Parse(((di.FindControl("lblSkuDet_PK") as Label).Text.ToString()));
                    Decimal endbit = decimal.Parse(((di.FindControl("txt_deliveryQty") as TextBox).Text.ToString()));                    
                    EndbitInventory endbitInventory = new EndbitInventory();             
                    endbitInventory.Skudet_pk = skudet_pk;
                    endbitInventory.Location_pk = userloc;
                    endbitInventory.OnHandQty = endbit;
                    endbitInventory.ReceivedQty = endbit;
                    endbitInventory.DeliveredQty = 0;
                    endbitInventory.Inventoryitem_pk = inv_pk;
                    endbitInventory.Roll_Pk = 0;
                    endbitInventory.RefNum = "Old Endbit";
                    enty.EndbitInventories.Add(endbitInventory);
                    enty.SaveChanges();
                    var q1 = from inv in enty.InventoryMasters where inv.InventoryItem_PK == inv_pk select inv;
                    foreach(var element in q1)
                    {
                        element.OnhandQty = element.OnhandQty - endbit;
                        element.DeliveredQty= element.DeliveredQty+ endbit;
                    }

                }
            }
            enty.SaveChanges();

            return Donum;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArtWebApp.Inventory.DeliveryOrder
{
    public partial class DeliveryOrderStockReciept : System.Web.UI.Page
    {
        DBTransaction.DeliveryOrdertransaction dtrans = null;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {

        }

        protected void btn_DO_Click(object sender, EventArgs e)
        {
            dtrans = new DBTransaction.DeliveryOrdertransaction();
            DataTable dt = dtrans.GetDetailsOfsTOCKDO(int.Parse(cmb_do.SelectedValue.ToString()));
            tbl_InverntoryDetails.DataSource = dt;
            tbl_InverntoryDetails.DataBind();
            btn_saveDOR.Enabled = true;
        }

        protected void btn_saveDO_Click(object sender, EventArgs e)
        {
            if (checkdatagridValue(tbl_InverntoryDetails, "lbl_balanceqty", "txt_receiptQty"))
            {

                if (!IsNORowselected())
                {
                    btn_saveDOR.Enabled = false;
                    string msG=  insertDOR();
                    tbl_InverntoryDetails.DataSource = null;
                    tbl_InverntoryDetails.DataBind();
                    lbl_errordisplayer.Text = "*";
                    MessgeboxUpdate("sucess", msG);
                }
                else
                {
                    lbl_errordisplayer.Text = "Select Item To Recieve";
                }
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
        public Boolean IsNORowselected()
        {
            Boolean isnorowselected = false;
            int selectedrowscount = 0;
            foreach (GridViewRow di in tbl_InverntoryDetails.Rows)
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

        public void MessageBoxShow(String msg)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "Art", "alert('" + msg + "');", true);
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











        public String insertDOR()
        {
            
            BLL.InventoryBLL.StockDeliveryReciept sdorcpt = new BLL.InventoryBLL.StockDeliveryReciept();
            sdorcpt.Domstrdata = new BLL.InventoryBLL.StockDeliveryRecieptData();

            sdorcpt.Domstrdata.SDO_PK = int.Parse(cmb_do.SelectedValue.ToString());
            sdorcpt.Domstrdata.AddedDate = DateTime.Now;
            sdorcpt.Domstrdata.AddedBy = Session["Username"].ToString().Trim();
            sdorcpt.Domstrdata.Location_PK = int.Parse(Session["UserLoc_pk"].ToString());
            sdorcpt.Domstrdata.DoRecieptType = "SDWW";
            List<BLL.InventoryBLL.StockDeliveryRecieptDetailsData> rk = new List<BLL.InventoryBLL.StockDeliveryRecieptDetailsData>();
            foreach (GridViewRow di in tbl_InverntoryDetails.Rows)
            {
                CheckBox chkBx = (CheckBox)di.FindControl("chk_select");

                if (chkBx != null && chkBx.Checked)
                {
                    BLL.InventoryBLL.StockDeliveryRecieptDetailsData sdddata = new BLL.InventoryBLL.StockDeliveryRecieptDetailsData();
                    
                    int item_pk = int.Parse(((di.FindControl("lblInventoryItem_PK") as Label).Text.ToString()));
                    decimal recptqty = decimal.Parse(((di.FindControl("txt_receiptQty") as TextBox).Text.ToString()));
                    int dodet_pk = int.Parse(((di.FindControl("lbl_dodetpk") as Label).Text.ToString()));
                  

                    sdddata.SDODet_PK = dodet_pk;
                    sdddata.ReceivedQty = recptqty;
                    sdddata.SInventoryItem_PK = item_pk;
                    rk.Add(sdddata);
                    //sdorcpt.DeliveryOrderDetailsDataCollection.Add(sdddata);

                }
            }
            sdorcpt.DeliveryOrderDetailsDataCollection = rk;
            
        string MSG=    sdorcpt.insertStockDOR();


        return MSG;


           

            //enty.SaveChanges();

        }
    }
}
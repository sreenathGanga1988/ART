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
    public partial class DeliveryOrderReceipt : System.Web.UI.Page
    {
        DBTransaction.DeliveryOrdertransaction dtrans = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            btn_saveDOR.Attributes.Add("onclick", " this.disabled = true; " + ClientScript.GetPostBackEventReference(btn_saveDOR, null) + ";");
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            
        }

        protected void btn_DO_Click(object sender, EventArgs e)
        {
            var Value1 = cmb_do.SelectedItem.Text.Trim();

            Boolean isvia = false;

            if (Value1.Contains("AWATRW"))
            {
                isvia = true;
            }
            else
            {
                isvia = false;
            }


            if (isvia == true)
            {
                BLL.MerchandsingBLL.DocumentReceiptdata abll = new BLL.MerchandsingBLL.DocumentReceiptdata();
                String shipmentdocnum = "";
                try
                {
                    shipmentdocnum = abll.GetShippingDODocument(int.Parse(cmb_do.SelectedValue.ToString()));
                }
                catch (Exception)
                {

                    shipmentdocnum = "";
                }
                lbl_expnum.Text = shipmentdocnum;
                if (shipmentdocnum != "")
                {
                    dtrans = new DeliveryOrdertransaction();
                    DataTable dt = dtrans.GetDetailsOfDO(int.Parse(cmb_do.SelectedValue.ToString()));
                    tbl_InverntoryDetails.DataSource = dt;
                    tbl_InverntoryDetails.DataBind();
                    btn_saveDOR.Enabled = true;
                }
                else
                {

                    String msg = "Shipping document is not Updated Against this AW .Please contact Shipping Dept";


                    tbl_InverntoryDetails.DataSource = null;
                    tbl_InverntoryDetails.DataBind();


                    MessageBoxShow(msg);
                }
            }
            else
            {
                dtrans = new DeliveryOrdertransaction();
                DataTable dt = dtrans.GetDetailsOfDO(int.Parse(cmb_do.SelectedValue.ToString()));
                tbl_InverntoryDetails.DataSource = dt;
                tbl_InverntoryDetails.DataBind();
                btn_saveDOR.Enabled = true;
            }

            
        
        }

        protected void btn_saveDO_Click(object sender, EventArgs e)
        {
            if (checkdatagridValue(tbl_InverntoryDetails, "lbl_balanceqty", "txt_receiptQty"))
            {

                if (!IsNORowselected())
                {
                    btn_saveDOR.Enabled = false;
                    insertDOR();
                    tbl_InverntoryDetails.DataSource = null;
                    tbl_InverntoryDetails.DataBind();
                    lbl_errordisplayer.Text = "*";
                    
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
        
        
        
        
        
        
        
        
        
        
        
        public void insertDOR()
        {
            
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {
                DeliveryReceiptMaster dorcpt = new DeliveryReceiptMaster();
                dorcpt.DO_PK = int.Parse(cmb_do.SelectedValue.ToString()); 
                dorcpt.AddedDate = DateTime.Now;
                dorcpt.AddedBy = Session["Username"].ToString().Trim();
                dorcpt.Location_PK = int.Parse(Session["UserLoc_pk"].ToString());
                
                dorcpt.DOReceiptType = "DWW";
                enty.DeliveryReceiptMasters.Add(dorcpt);


                enty.SaveChanges();


                dorcpt.DORNum = "WR" + Session["lOC_Code"].ToString().Trim() + dorcpt.DOR_PK.ToString().PadLeft(6, '0');

              
                foreach (GridViewRow di in tbl_InverntoryDetails.Rows)
                {
                    CheckBox chkBx = (CheckBox)di.FindControl("chk_select");

                    if (chkBx != null && chkBx.Checked)
                    {


                        

                        int item_pk = int.Parse(((di.FindControl("lblInventoryItem_PK") as Label).Text.ToString()));
                        decimal recptqty = decimal.Parse(((di.FindControl("txt_receiptQty") as TextBox).Text.ToString()));
                        int dodet_pk = int.Parse(((di.FindControl("lbl_dodetpk") as Label).Text.ToString()));
                        int mrndet_pk=0;
                        int podet_pk=0;
                        int skudetPK=0;
                        decimal curate = 0;
                        int uom_pk = 0;
                        DeliveryReceiptDetail drd = new DeliveryReceiptDetail();
                        drd.DODet_PK = dodet_pk;
                        drd.DOR_PK = dorcpt.DOR_PK;
                        drd.ReceivedQty = recptqty;
                        drd.InventoryItem_PK = item_pk;

                        enty.DeliveryReceiptDetails.Add(drd);




                        var q= from invitem in enty.InventoryMasters
                               where invitem.InventoryItem_PK==item_pk
                               select invitem;

                        foreach(var invitemdetail in q)
                        {
                            skudetPK=int.Parse (invitemdetail.SkuDet_Pk.ToString ());
                            mrndet_pk=int.Parse (invitemdetail.MrnDet_PK.ToString ());
                            podet_pk=int.Parse (invitemdetail.PoDet_PK.ToString ());
                            curate = decimal.Parse(invitemdetail.CURate.ToString());
                            uom_pk = int.Parse(invitemdetail.Uom_Pk.ToString());
                        }

                        var q1 = from godsintran in enty.GoodsInTransits
                                 where godsintran.DO_PK.ToString().Trim() == cmb_do.SelectedValue.ToString().Trim() && godsintran.InventoryItem_PK.ToString().Trim() == item_pk.ToString().Trim ()
                                 select godsintran;

                        foreach (var trans in q1)
                        {
                            trans.TransitQty = trans.TransitQty - recptqty;

                        }
                       

                        InventoryMaster invmstr = new InventoryMaster();
                        invmstr.MrnDet_PK = mrndet_pk;
                        invmstr.PoDet_PK = podet_pk;
                        invmstr.SkuDet_Pk = skudetPK;
                        invmstr.ReceivedQty = recptqty;
                        invmstr.OnhandQty = recptqty;
                        invmstr.DeliveredQty = 0;
                        invmstr.ReceivedVia = "WR";
                        invmstr.Location_PK = int.Parse(Session["UserLoc_pk"].ToString());
                        invmstr.CURate = curate;
                        invmstr.AddedDate = DateTime.Now.Date;
                        invmstr.Uom_Pk = uom_pk;
                        invmstr.Refnum = dorcpt.DORNum;
                        enty.InventoryMasters.Add(invmstr);



                        enty.SaveChanges();


                    }
                }
                String msg = "DO # : " + dorcpt.DORNum + " is generated Sucessfully";


                MessgeboxUpdate("sucess", msg);

                //enty.SaveChanges();
            }
        }
    }
}
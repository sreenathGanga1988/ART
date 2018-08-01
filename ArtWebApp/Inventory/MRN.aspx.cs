using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ArtWebApp.DataModels;
using ArtWebApp.BLL;
using System.Data;
namespace ArtWebApp.Inventory
{
    public partial class MRN : System.Web.UI.Page
    {
        BLL.InventoryBLL.MrnDetailsData mrndetdata = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            btn_saveMrn.Attributes.Add("onclick", " this.disabled = true; " + ClientScript.GetPostBackEventReference(btn_saveMrn, null) + ";");
            btn_excess.Attributes.Add("onclick", " this.disabled = true; " + ClientScript.GetPostBackEventReference(btn_excess, null) + ";");
        }


        /// <summary>
        /// insert the reciept
        /// </summary>

        public void InsertRecieptmasterData()
        {
            String rcptnum = "";
            BLL.InventoryBLL.RecieptMasterData rcptmstrdata = new BLL.InventoryBLL.RecieptMasterData();


               rcptmstrdata.ContainerNum = txt_container.Text.Trim();
                rcptmstrdata.Supplier_PK = int.Parse(drp_supplier.SelectedValue.ToString());
                rcptmstrdata.BOENum = txt_container.Text.Trim();
                rcptmstrdata.Deliverydate = DateTime.Parse(dtp_inhousedate.Value.ToString());
                rcptmstrdata.Remark = txta_remark.Value.ToString();
                rcptmstrdata.InhouseDate = DateTime.Parse(dtp_inhousedate.Value.ToString());
                rcptmstrdata.AddedBy = Session["Username"].ToString().Trim();
                rcptmstrdata.IsCompleted = "N";
                   rcptmstrdata.RecptLocation_PK= int.Parse(Session["UserLoc_pk"].ToString());

            rcptnum= rcptmstrdata.InsertReciptMstr(rcptmstrdata);


               String msg = "Reciept # : " + rcptnum + " is generated Sucessfully";
               MessageBoxShow(msg);
        }



       
      

        protected void btn_savercpt_Click(object sender, EventArgs e)
        {
            InsertRecieptmasterData();
            updaterecieptdata();
        }




        public void updaterecieptdata()
        {

            drp_rcpt.DataBind();
            Upd_rxptdrop.Update(); ;
        }
        protected void btn_confirmRcpt_Click(object sender, EventArgs e)
        {
            hdn_rcptnum.Value =drp_rcpt.SelectedValue.ToString();
            drp_po.DataBind();
        

        }

        protected void btn_confirmPO_Click(object sender, EventArgs e)
        {
            //DBTransaction.MrnTransaction mrntran = new DBTransaction.MrnTransaction();
            //DataTable dt=  mrntran.GetPODetails(int.Parse(drp_po.SelectedValue.ToString()));
            //tbl_Podetails.DataSource = dt;
            //tbl_Podetails.DataBind();
            fillADN(int.Parse(drp_po.SelectedValue.ToString()));
            //btn_saveMrn.Enabled = true;
        }













  


        public void fillADN( int popk)
        {
            using (ArtEntitiesnew entty = new ArtEntitiesnew())
            {
                entty.Configuration.AutoDetectChangesEnabled = false;
                var q = from docmstr in entty.DocMasters join  doccdet  in entty.DocDetails on docmstr.Doc_Pk equals doccdet.Doc_Pk
                        join procdet in entty.ProcurementDetails on doccdet.PODet_Pk equals procdet.PODet_PK
                        where procdet.PO_Pk==popk
                        select new
                        {
                            name = docmstr.DocNum,
                            pk = docmstr.Doc_Pk
                        } into X 
                        group X by new { X.name, X.pk } into g
                        select new
                        {
                            name = g.Key.name,
                            pk = g.Key.pk

                        };

                // Create a table from the query.
                drp_doc.DataSource = q.ToList();
                drp_doc.DataBind();




            }

        }

        protected void btn_saveMrn_Click(object sender, EventArgs e)
        {
            if (checkdatagridValue(tbl_Podetails, "lbl_balnceqty", "txt_reciept"))
            {
                if (ArtWebApp.Controls.Gridviewvalidation.countofRowselected(tbl_Podetails, "chk_select") > 0)
                {
                    //      btn_saveMrn.Enabled = false;
                    insertmrn();
                }
                else
                {
                    string Msg = "alert('Please select the Items to be added in this MRN ')";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", Msg, true);
                }

            }

            
        }



        //public void ValidationGrid()
        //{
        //    Boolean isok=false;

        //    foreach (GridViewRow di in tbl_Podetails.Rows)
        //    {
        //        CheckBox chkBx = (CheckBox)di.FindControl("chk_select");

        //        if (chkBx != null && chkBx.Checked)
        //        {
        //            int BalQty = int.Parse(((di.FindControl("lbl_balnceqty") as Label).Text.ToString()));
        //            int receivedqty = int.Parse(((di.FindControl("txt_reciept") as TextBox).Text.ToString()));

        //            if(QuantityValidator.ISQuantityLesser (receivedqty,BalQty))
        //            {
        //                 isok=true;
        //            }else{
        //                 isok=false;
        //            }
        //        }
        //    }
        //}




        public BLL.InventoryBLL.MrnMasterData getMRNMasterData()
        {

            BLL.InventoryBLL.MrnMasterData mrnmstr = new BLL.InventoryBLL.MrnMasterData();

            mrnmstr.DoNumber = txt_deliverynote.Text;
            mrnmstr.AddedDate = DateTime.Now;
            mrnmstr.AddedBY = Session["Username"].ToString().Trim();
            mrnmstr.Po_PK = int.Parse(drp_po.SelectedValue.ToString());
            mrnmstr.Location_Pk = int.Parse(Session["UserLoc_pk"].ToString());
            mrnmstr.Reciept_Pk = int.Parse(drp_rcpt.SelectedValue.ToString());
          
            return mrnmstr;
        }



        public void insertmrn()
        {
            String mrnum = "";
            BLL.InventoryBLL.PoReceipt prrrcpt = new BLL.InventoryBLL.PoReceipt();
            prrrcpt.mrnmstrdata = getMRNMasterData();
            prrrcpt.MrnDetailsDataCollection = GetMRnDetailsData();

         mrnum= prrrcpt.insertMRNData(prrrcpt);


            String msg = "MRN # : " + mrnum + " is generated Sucessfully";


            tbl_Podetails.DataSource = null;
            tbl_Podetails.DataBind();
          

            MessageBoxShow(msg);
              
        }

        public List<BLL.InventoryBLL.MrnDetailsData> GetMRnDetailsData()
        {
           
            List<BLL.InventoryBLL.MrnDetailsData> rk = new List<BLL.InventoryBLL.MrnDetailsData>();
            foreach (GridViewRow di in tbl_Podetails.Rows)
            {
                CheckBox chkBx = (CheckBox)di.FindControl("chk_select");

                if (chkBx != null && chkBx.Checked)
                {
                    int skudet_pk = int.Parse(((di.FindControl("lbl_skudet_pk") as Label).Text.ToString()));
                    int podet_pk = int.Parse(((di.FindControl("lbl_podet_pk") as Label).Text.ToString()));
                    decimal recieptqty = decimal.Parse((di.FindControl("txt_reciept") as TextBox).Text.ToString());
                    decimal extraqty = decimal.Parse((di.FindControl("txt_extra") as TextBox).Text.ToString());
                    decimal curate = decimal.Parse((di.FindControl("lbl_curate") as Label).Text.ToString());
                    int uom_pk = int.Parse(((di.FindControl("lbl_uom_pk") as Label).Text.ToString()));
                    int Doc_Pk = int.Parse(((di.FindControl("lbl_Doc_Pk") as Label).Text.ToString()));
                    
                    mrndetdata = new BLL.InventoryBLL.MrnDetailsData();
                    
                         mrndetdata.Doc_pk = Doc_Pk;
                    mrndetdata.PODet_PK = podet_pk;
                    mrndetdata.SkuDet_PK = skudet_pk;
                    mrndetdata.ReceiptQty = recieptqty;
                    mrndetdata.ExtraQty = extraqty;
                    mrndetdata.Remark = txta_remark.Value.ToString().Trim();
                    mrndetdata.Curate = curate;
                    mrndetdata.Uom_PK1 = uom_pk;
                    rk.Add(mrndetdata);
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
                Messaediv.Attributes["class"] = "error-message ";
                Messaediv.InnerText = Messg;
            }
            updstatus.Update();
        }
        
        public void MessageBoxShow(String msg)
        {
            MessgeboxUpdate("sucess", msg);
            ClientScript.RegisterStartupScript(this.GetType(), "Art", "alert('" + msg + "');", true);
        }

        public void RCPTMessageBoxShow(String msg)
        {
            MessgeboxUpdate("sucess", msg);
            ClientScript.RegisterStartupScript(this.GetType(), "Art", "alert('" + msg + "');", true);
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            recieptdata.SelectCommand = "SELECT Reciept_Pk, RecieptNum FROM RecieptMaster WHERE (IsCompleted = 'N') AND (RecptLocation_PK = " + int.Parse(Session["UserLoc_pk"].ToString()) + ") AND ( Supplier_PK=" + int.Parse(drp_supplier.SelectedValue.ToString()) + ")";
            drp_rcpt.DataBind();
            Upd_rxptdrop.Update();
        }

        protected void btn_excess_Click(object sender, EventArgs e)
        {
            insertmrn(); 
        }

        protected void btn_confirmADN_Click(object sender, EventArgs e)
        {
            int adnnum = int.Parse(drp_doc.SelectedValue.ToString());

            BLL.MerchandsingBLL.DocumentReceiptdata abll = new BLL.MerchandsingBLL.DocumentReceiptdata();
            DBTransaction.MrnTransaction mrntran = new DBTransaction.MrnTransaction();
            DataTable dt = new DataTable();
            String type= abll.isADNMRNmakable(adnnum);
            lbl_adntype.Text = type.ToString();

            if (type== "LocalUAE")
            {
                if(Session["UserLoc_pk"].ToString ().Trim()=="6")
                {
                    dt = mrntran.GetPODetailsforMRN(int.Parse(drp_po.SelectedValue.ToString()), int.Parse(drp_doc.SelectedValue.ToString()));
                    tbl_Podetails.DataSource = dt;
                    tbl_Podetails.DataBind();

                    btn_saveMrn.Enabled = true;
                }
                else
                {
                    String msg = "This is a local UAE ADN and you cannot MRN it";


                    tbl_Podetails.DataSource = null;
                    tbl_Podetails.DataBind();


                    MessageBoxShow(msg);
                }

            }
            else if (type == "LocalKenya")
            {
                if (Session["UserLoc_pk"].ToString().Trim() != "6")
                {
                    dt = mrntran.GetPODetailsforMRN(int.Parse(drp_po.SelectedValue.ToString()), int.Parse(drp_doc.SelectedValue.ToString()));
                    tbl_Podetails.DataSource = dt;
                    tbl_Podetails.DataBind();

                    btn_saveMrn.Enabled = true;
                }
                else
                {
                    String msg = "This is a Non UAE ADN and you cannot MRN it";


                    tbl_Podetails.DataSource = null;
                    tbl_Podetails.DataBind();


                    MessageBoxShow(msg);
                }

            }
            else if (type == "IntlSupplier")
            {
                String shipmentdocnum = abll.GetShippingDocument(adnnum);
                lbl_expnum.Text = shipmentdocnum;
                if(shipmentdocnum!="")
                {
                    dt = mrntran.GetPODetailsforMRN(int.Parse(drp_po.SelectedValue.ToString()), int.Parse(drp_doc.SelectedValue.ToString()));
                    tbl_Podetails.DataSource = dt;
                    tbl_Podetails.DataBind();

                    btn_saveMrn.Enabled = true;
                }
                else
                {
                    String msg = "Shipping document is not Updated Against this ADN .Please contact Shipping Dept";


                    tbl_Podetails.DataSource = null;
                    tbl_Podetails.DataBind();


                    MessageBoxShow(msg);
                }
            }
           
          
        }

        protected void tbl_Podetails_SelectedIndexChanged()
        {

        }
    }
}
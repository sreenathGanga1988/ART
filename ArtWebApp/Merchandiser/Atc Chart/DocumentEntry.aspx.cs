using Infragistics.Web.UI.ListControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArtWebApp.Merchandiser.Atc_Chart
{
    public partial class DocumentEntry : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_confirmPO_Click(object sender, EventArgs e)
        {
            Adddata();
        }


        public void Adddata()
        {
            List<DropDownItem> items = drp_po.SelectedItems;
            ArtWebApp.BLL.MerchandsingBLL.AtcChartBLL atcbll = new BLL.MerchandsingBLL.AtcChartBLL();
            tbl_Podetails.DataSource = atcbll.getMultiSelectedPoData(items);
            tbl_Podetails.DataBind();
            upd_grid.Update();
        }

        protected void btn_savercpt_Click(object sender, EventArgs e)
        {
           String msg= InsertDocRecieptmasterData();
        

            MessgeboxUpdate1("sucess", msg);
            drp_rcpt.DataBind();
            udp_drprcpt.Update();
        }

        /// <summary>
        /// insert the Docreciept
        /// </summary>

        public String InsertDocRecieptmasterData()
        {
            String rcptnum = "";
            String msg = "";
            if (ddl_adnType.SelectedValue!="Select")
            {
                BLL.MerchandsingBLL.DocumentReceiptdata rcptmstrdata = new BLL.MerchandsingBLL.DocumentReceiptdata();




                rcptmstrdata.ContainerNum = txt_container.Text.Trim();
                rcptmstrdata.Supplier_PK = int.Parse(drp_supplier.SelectedValue.ToString());
                rcptmstrdata.BOENum = txt_boe.Text.Trim();
                rcptmstrdata.ETADate = DateTime.Parse(dtp_deliverydate.Value.ToString());
                rcptmstrdata.Remark = txta_remark.Value.ToString();
                rcptmstrdata.InhouseDate = DateTime.Parse(dtp_deliverydate.Value.ToString());
                rcptmstrdata.AddedBy = Session["Username"].ToString().Trim();
                rcptmstrdata.IsCompleted = "N";
                rcptmstrdata.Location_PK = int.Parse(Session["UserLoc_pk"].ToString());
                rcptmstrdata.currency_Pk = int.Parse(drp_supplier.SelectedValue.ToString());
                rcptmstrdata.docvalue = decimal.Parse(txt_docvalue.Text);
                rcptmstrdata.DocType = ddl_adnType.SelectedValue.Trim();
                rcptnum = rcptmstrdata.InsertReciptMstr(rcptmstrdata);
                msg = "Doc # : " + rcptnum + " is generated Sucessfully";

                string Msg = "alert('"+ msg + " ')";
                ScriptManager.RegisterClientScriptBlock((btn_savercpt as Control), this.GetType(), "alert", Msg, true);
              
            }
            else
            {

                string Msg = "alert('Please select the ADN TYPE ')";
                ScriptManager.RegisterClientScriptBlock((btn_savercpt as Control), this.GetType(), "alert", Msg, true);
                //string message = "alert('Hello!')";
                //ScriptManager.RegisterClientScriptBlock((this as Control), this.GetType(), "alert", message, true);

            }
          
           


         
     //       MessageBoxShow(msg);

            return msg;
        }



       

        protected void btn_confirmRcpt_Click(object sender, EventArgs e)
        {
            hdn_rcptnum.Value = drp_rcpt.SelectedValue.ToString();
            BLL.MerchandsingBLL.DocumentReceiptdata rcptmstrdata = new BLL.MerchandsingBLL.DocumentReceiptdata();
          
            //drp_po.DataBind();
            //udp_drppo.Update();
         dtp_eta.Value=    rcptmstrdata.geteta (int.Parse( drp_rcpt.SelectedValue.ToString() ));
         upd_eta.Update();
        }

        protected void btn_saveMrn_Click(object sender, EventArgs e)
        {
            if (ArtWebApp.Controls.Gridviewvalidation.countofRowselected(tbl_Podetails, "chk_select") > 0)
            {
                if (checkdatagridValue(tbl_Podetails, "lbl_bal", "txt_qty"))
                {
                    InsertPodetails();
                    tbl_Podetails.DataSource = null;
                    tbl_Podetails.DataBind();
                    upd_grid.Update();
                    //lbl_errordisplayer.Text = "Details Added";
                }
                else
                {
                    string msg = "Entered CM is greater than Approved CM Please Revise the Costing";
               //     lbl_errordisplayer.Text = msg;
                }
            }
            else
            {
                string Msg = "alert('Please select the Items to be added in this ADN ')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", Msg, true);
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


        public void InsertPodetails()
        {
            BLL.MerchandsingBLL.DocumentReceiptdata rcptmstrdata = new BLL.MerchandsingBLL.DocumentReceiptdata();
            rcptmstrdata.DocumentDetailsDataCollection = GetPODetailsData();
            rcptmstrdata.insertPoEtaData(rcptmstrdata);
        }

        public void MessageBoxShow(String msg)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "Art", "alert('" + msg + "');", true);
        }

        protected void dtp_eta_ValueChanged(object sender, Infragistics.Web.UI.EditorControls.TextEditorValueChangedEventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow di in tbl_Podetails.Rows)
            {
                CheckBox chkBx = (CheckBox)di.FindControl("chk_select");

                if (chkBx != null && chkBx.Checked)
                {
                    Infragistics.Web.UI.EditorControls.WebDatePicker wbdt = di.FindControl("wdp_etadate") as Infragistics.Web.UI.EditorControls.WebDatePicker;


                    wbdt.Date = DateTime.Parse(dtp_eta.Date.ToString());
                }
            }
            upd_grid.Update();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow di in tbl_Podetails.Rows)
            {
                CheckBox chkBx = (CheckBox)di.FindControl("chk_select");

                if (chkBx != null && chkBx.Checked)
                {
                    TextBox wbdt = di.FindControl("txt_do") as TextBox;


                    wbdt.Text = txt_deliverynote.Text.Trim();
                }
            }
            upd_grid.Update();
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
        public void MessgeboxUpdate1(String Messagetype, String Messg)
        {
            if (Messagetype == "sucess")
            {
                Messaediv1.Attributes["class"] = "success";
                Messaediv1.InnerText = Messg;
            }
            else
            {
                Messaediv1.Attributes["class"] = "error-message";
                Messaediv1.InnerText = Messg;
            }
        }


        public List<BLL.MerchandsingBLL.DocPodetaildata> GetPODetailsData()
        {

            List<BLL.MerchandsingBLL.DocPodetaildata> rk = new List<BLL.MerchandsingBLL.DocPodetaildata>();
            foreach (GridViewRow di in tbl_Podetails.Rows)
            {
                CheckBox chkBx = (CheckBox)di.FindControl("chk_select");

                if (chkBx != null && chkBx.Checked)
                {
                      
                   
                    int podet_pk = int.Parse(((di.FindControl("lbl_podet_pk") as Label).Text.ToString()));
                    decimal recieptqty = decimal.Parse((di.FindControl("txt_qty") as TextBox).Text.ToString());
                    decimal txt_newExcessqty = decimal.Parse((di.FindControl("txt_newExcessqty") as TextBox).Text.ToString());
                    String  invnum = (di.FindControl("txt_do") as TextBox).Text;
                     Infragistics.Web.UI.EditorControls.WebDatePicker wbdt = di.FindControl("wdp_etadate") as Infragistics.Web.UI.EditorControls.WebDatePicker;
                    BLL.MerchandsingBLL.DocPodetaildata podetdata = new BLL.MerchandsingBLL.DocPodetaildata();

                    podetdata.Doc_Pk = int.Parse (drp_rcpt.SelectedItem.Value.ToString ());
                    podetdata.podet_PK = podet_pk;
                    podetdata.Qty = recieptqty;
                    podetdata.InvNum = invnum;
                    podetdata.ETADate = wbdt.Date;
                    podetdata.eXCESSQty = txt_newExcessqty;

                    podetdata.AddedDate = DateTime.Now;
                    podetdata.AddedBy = Session["Username"].ToString().Trim();
                    rk.Add(podetdata);
                }
            }



            return rk;


        }

        protected void btn_confirmatc_Click(object sender, EventArgs e)
        {
            hdn_atc.Value = ddl_atc.SelectedValue.ToString();
          
            drp_po.DataBind();
            udp_drppo.Update();
        }
    }
}
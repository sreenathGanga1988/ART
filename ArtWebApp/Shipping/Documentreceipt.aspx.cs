using ArtWebApp.DataModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArtWebApp.Shipping
{
    public partial class Documentreceipt : System.Web.UI.Page
    {
        BLL.MerchandsingBLL.DocumentReceiptdata docrcptdat = new BLL.MerchandsingBLL.DocumentReceiptdata();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                filltowarehouses();
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
                drp_deliverymode.DataValueField = "pk";
                drp_deliverymode.DataTextField = "name";
                drp_deliverymode.DataBind();


                // Bind the table to a System.Windows.Forms.BindingSource object, 
                // which acts as a proxy for a System.Windows.Forms.DataGridView object.

            }
        }



        public void fillDocumentNum(int locationpk)
        {
            using (ArtEntitiesnew entty = new ArtEntitiesnew())
            {
                var q = from order in entty.DocMasters
                        where order.Location_PK == locationpk
                        select new
                        {
                            name = order.DocNum,
                            pk = order.Doc_Pk,
                        };

                // Create a table from the query.
                drp_rcpt.DataSource = q.ToList();
                drp_rcpt.DataBind();



                // Bind the table to a System.Windows.Forms.BindingSource object, 
                // which acts as a proxy for a System.Windows.Forms.DataGridView object.

            }
        }


        public void fillDoNum(int locationpk)
        {
            using (ArtEntitiesnew entty = new ArtEntitiesnew())
            {
                var q = from order in entty.DeliveryOrderMasters  

                        where order.FromLocation_PK == locationpk
                        select new
                        {
                            name = order.DONum,
                            pk = order.DO_PK,
                        };

                // Create a table from the query.
                drp_rcpt.DataSource = q.ToList();
                drp_rcpt.DataBind();



                // Bind the table to a System.Windows.Forms.BindingSource object, 
                // which acts as a proxy for a System.Windows.Forms.DataGridView object.

            }
        }


        protected void btn_show_Click(object sender, EventArgs e)
        {
            ArrayList doclist = new ArrayList();
            List<Infragistics.Web.UI.ListControls.DropDownItem> items = drp_rcpt.SelectedItems;
            foreach (Infragistics.Web.UI.ListControls.DropDownItem item in items)
            {

                int shpdata = int.Parse(item.Value.ToString());
                doclist.Add(shpdata);
            }


            if (doclist.Count > 0 && doclist != null)
            {

                if (RadioButton1.Checked == true)
                {
                    tbl_Podetails.DataSource = docrcptdat.getDOCData(doclist);
                    tbl_Podetails.DataBind();
                    tbl_dodetails.DataSource = null;
                    tbl_dodetails.DataBind();
                    tbl_StockAdn.DataSource = null;
                    tbl_StockAdn.DataBind();
                    tbl_stockDO.DataSource = null;
                    tbl_stockDO.DataBind();
                }
                else if (RadioButton2.Checked == true)
                {
                    tbl_dodetails.DataSource = docrcptdat.getDOData(doclist);
                    tbl_dodetails.DataBind();
                    tbl_Podetails.DataSource = null;
                    tbl_Podetails.DataBind();
                    tbl_StockAdn.DataSource = null;
                    tbl_StockAdn.DataBind();
                }
                else if (rbt_viageneralitem.Checked == true)
                {
                 
                    tbl_stockDO.DataSource = docrcptdat.getSDOData(doclist);
                    tbl_stockDO.DataBind();
                    tbl_Podetails.DataSource = null;
                    tbl_Podetails.DataBind();
                    tbl_StockAdn.DataSource = null;
                    tbl_StockAdn.DataBind();
                    tbl_dodetails.DataSource = null;
                    tbl_dodetails.DataBind();
                }
                else if (rbt_directgenitem.Checked == true)
                {
                    tbl_StockAdn.DataSource = docrcptdat.getSDOCData(doclist);
                    tbl_StockAdn.DataBind();

                    tbl_stockDO.DataSource = null;
                    tbl_stockDO.DataBind();
                    tbl_Podetails.DataSource = null;
                    tbl_Podetails.DataBind();
                  
                    tbl_dodetails.DataSource = null;
                    tbl_dodetails.DataBind();

                }

                upd_grid.Update();
            }


        }

        protected void btn_showfromLoc_Click(object sender, EventArgs e)
        {

            try
            {
                BLL.ShippingBLL.ShippingDocumentMasterData shmpmstr = new BLL.ShippingBLL.ShippingDocumentMasterData(); 
               
                if (RadioButton1.Checked == true)
                {
                    //fillDocumentNum(int.Parse(drp_ToWarehouse.SelectedValue.ToString()));

                    drp_rcpt.DataSource = shmpmstr.GetADNList();
                }
                else if (RadioButton2.Checked == true)
                {
                   // fillDoNum(int.Parse(drp_ToWarehouse.SelectedValue.ToString()));
                    drp_rcpt.DataSource = shmpmstr.GetAWList();
                }
                else if(rbt_viageneralitem.Checked==true)
                {
                    drp_rcpt.DataSource = shmpmstr.GetStockAWList();
                }
                else if (rbt_directgenitem.Checked == true)
                {
                    drp_rcpt.DataSource = shmpmstr.GetStockADNList();
                  
                }

                drp_rcpt.DataBind();

            }
            catch (Exception)
            {


            }
        }




       


        #region atc Item




        public BLL.ShippingBLL.ShippingDocumentMasterData GetINVMasterDataForDirect()
        {
            BLL.ShippingBLL.ShippingDocumentMasterData shpdocmstr = new BLL.ShippingBLL.ShippingDocumentMasterData();
            shpdocmstr.AddedBY = Session["Username"].ToString().Trim();
            shpdocmstr.AddedDate = DateTime.Now; ;
            shpdocmstr.ShipperName = txt_shipper.Text;
            shpdocmstr.ExporterName = txt_exporter.Text;
            shpdocmstr.ShipperInv = txt_shiperinv.Text;
            shpdocmstr.Description = txt_description.Text; ;
            shpdocmstr.NOofctnRoll = txt_noctn.Text; ;
            shpdocmstr.Packagetype = txt_ctnroll.Text; ;
            shpdocmstr.Weight = txt_weight.Text;
            shpdocmstr.Type = txt_type.Text;
            shpdocmstr.InvoiceValue = txt_invvalue.Text;
            shpdocmstr.Vessel = txt_vessel.Text;
            shpdocmstr.Conatianer = txt_container.Text; ;
            shpdocmstr.ContsainerType = txt_containertype.Text; ;
            shpdocmstr.BL = txtBL.Text;
            shpdocmstr.DocType = "Direct";
            shpdocmstr.Mode = drp_deliverymode.SelectedItem.Text;

            try
            {
                string et = Request.Form[dtp_deliverydate.UniqueID].ToString();

                shpdocmstr.ETA = DateTime.Parse(et);
            }
            catch (Exception)
            {
                shpdocmstr.ETA = DateTime.Now; ;

            }
            shpdocmstr.ShippingDocumentDetailsDataCollection = ShippingDocumentDetailsData();
            return shpdocmstr;
        }


        public BLL.ShippingBLL.ShippingDocumentMasterData GetINVMasterDataForVia()
        {
            BLL.ShippingBLL.ShippingDocumentMasterData shpdocmstr = new BLL.ShippingBLL.ShippingDocumentMasterData();
            shpdocmstr.AddedBY = Session["Username"].ToString().Trim();
            shpdocmstr.AddedDate = DateTime.Now; ;
            shpdocmstr.ShipperName = txt_shipper.Text;
            shpdocmstr.ExporterName = txt_exporter.Text;
            shpdocmstr.ShipperInv = txt_shiperinv.Text;
            shpdocmstr.Description = txt_description.Text; ;
            shpdocmstr.NOofctnRoll = txt_noctn.Text; ;
            shpdocmstr.Packagetype = txt_ctnroll.Text; ;
            shpdocmstr.Weight = txt_weight.Text;
            shpdocmstr.Type = txt_type.Text;
            shpdocmstr.InvoiceValue = txt_invvalue.Text;
            shpdocmstr.Vessel = txt_vessel.Text;
            shpdocmstr.Conatianer = txt_container.Text; ;
            shpdocmstr.ContsainerType = txt_containertype.Text; ;
            shpdocmstr.BL = txtBL.Text;
            shpdocmstr.DocType = "Via";
            shpdocmstr.Mode = drp_deliverymode.SelectedItem.Text;

            try
            {
                string et = Request.Form[dtp_deliverydate.UniqueID].ToString();

                shpdocmstr.ETA = DateTime.Parse(et);
            }
            catch (Exception)
            {
                shpdocmstr.ETA = DateTime.Now; ;

            }
            shpdocmstr.ShippingDocumentDODetailsDataCollection = ShippingDocumentDetailDOData();
            return shpdocmstr;
        }

        public List<BLL.ShippingBLL.ShippingDocumentDetailsData> ShippingDocumentDetailsData()
        {

            List<BLL.ShippingBLL.ShippingDocumentDetailsData> rk = new List<BLL.ShippingBLL.ShippingDocumentDetailsData>();


            foreach (GridViewRow di in tbl_Podetails.Rows)
            {
                CheckBox chkBx = (CheckBox)di.FindControl("chk_select");

                if (chkBx != null && chkBx.Checked)
                {




                    int doc_pk = int.Parse(((di.FindControl("lbl_doc_pk") as Label).Text.ToString()));



                    BLL.ShippingBLL.ShippingDocumentDetailsData invdata = new BLL.ShippingBLL.ShippingDocumentDetailsData();


                    invdata.Doc_Pk = doc_pk;



                    rk.Add(invdata);
                }
            }
            return rk;


        }
        public List<BLL.ShippingBLL.ShippingDocumentDODetailsData> ShippingDocumentDetailDOData()
        {

            List<BLL.ShippingBLL.ShippingDocumentDODetailsData> rk = new List<BLL.ShippingBLL.ShippingDocumentDODetailsData>();


            foreach (GridViewRow di in tbl_dodetails.Rows)
            {
                CheckBox chkBx = (CheckBox)di.FindControl("chk_select");

                if (chkBx != null && chkBx.Checked)
                {




                    int doc_pk = int.Parse(((di.FindControl("lbl_DO_PK") as Label).Text.ToString()));



                    BLL.ShippingBLL.ShippingDocumentDODetailsData invdata = new BLL.ShippingBLL.ShippingDocumentDODetailsData();


                    invdata.DO_PK = doc_pk;



                    rk.Add(invdata);
                }
            }
            return rk;


        }
        #endregion



        #region General

        public BLL.ShippingBLL.ShippingDocumentMasterData GetINVMasterDataForViaGeneral()
        {
            BLL.ShippingBLL.ShippingDocumentMasterData shpdocmstr = new BLL.ShippingBLL.ShippingDocumentMasterData();
            shpdocmstr.AddedBY = Session["Username"].ToString().Trim();
            shpdocmstr.AddedDate = DateTime.Now; ;
            shpdocmstr.ShipperName = txt_shipper.Text;
            shpdocmstr.ExporterName = txt_exporter.Text;
            shpdocmstr.ShipperInv = txt_shiperinv.Text;
            shpdocmstr.Description = txt_description.Text; ;
            shpdocmstr.NOofctnRoll = txt_noctn.Text; ;
            shpdocmstr.Packagetype = txt_ctnroll.Text; ;
            shpdocmstr.Weight = txt_weight.Text;
            shpdocmstr.Type = txt_type.Text;
            shpdocmstr.InvoiceValue = txt_invvalue.Text;
            shpdocmstr.Vessel = txt_vessel.Text;
            shpdocmstr.Conatianer = txt_container.Text; ;
            shpdocmstr.ContsainerType = txt_containertype.Text; ;
            shpdocmstr.BL = txtBL.Text;
            shpdocmstr.DocType = "GenVia";
            shpdocmstr.Mode = drp_deliverymode.SelectedItem.Text;

            try
            {
                string et = Request.Form[dtp_deliverydate.UniqueID].ToString();

                shpdocmstr.ETA = DateTime.Parse(et);
            }
            catch (Exception)
            {
                shpdocmstr.ETA = DateTime.Now; ;

            }
            shpdocmstr.ShippingDocumentDODetailsDataCollection = ShippingDocumentDetailDODataGeneral();
            return shpdocmstr;
        }
        public List<BLL.ShippingBLL.ShippingDocumentDODetailsData> ShippingDocumentDetailDODataGeneral()
        {

            List<BLL.ShippingBLL.ShippingDocumentDODetailsData> rk = new List<BLL.ShippingBLL.ShippingDocumentDODetailsData>();


            foreach (GridViewRow di in tbl_stockDO.Rows)
            {
                CheckBox chkBx = (CheckBox)di.FindControl("chk_select");

                if (chkBx != null && chkBx.Checked)
                {




                    int doc_pk = int.Parse(((di.FindControl("lbl_DO_PK") as Label).Text.ToString()));



                    BLL.ShippingBLL.ShippingDocumentDODetailsData invdata = new BLL.ShippingBLL.ShippingDocumentDODetailsData();


                    invdata.DO_PK = doc_pk;



                    rk.Add(invdata);
                }
            }
            return rk;


        }
        /// <summary>
        /// gernal direct gone ggods
        /// looks into the stock adn
        /// 
        /// </summary>
        /// <returns></returns>

        public BLL.ShippingBLL.ShippingDocumentMasterData GetINVMasterDataForGeneralDirect()
        {
            BLL.ShippingBLL.ShippingDocumentMasterData shpdocmstr = new BLL.ShippingBLL.ShippingDocumentMasterData();
            shpdocmstr.AddedBY = Session["Username"].ToString().Trim();
            shpdocmstr.AddedDate = DateTime.Now; ;
            shpdocmstr.ShipperName = txt_shipper.Text;
            shpdocmstr.ExporterName = txt_exporter.Text;
            shpdocmstr.ShipperInv = txt_shiperinv.Text;
            shpdocmstr.Description = txt_description.Text; ;
            shpdocmstr.NOofctnRoll = txt_noctn.Text; ;
            shpdocmstr.Packagetype = txt_ctnroll.Text; ;
            shpdocmstr.Weight = txt_weight.Text;
            shpdocmstr.Type = txt_type.Text;
            shpdocmstr.InvoiceValue = txt_invvalue.Text;
            shpdocmstr.Vessel = txt_vessel.Text;
            shpdocmstr.Conatianer = txt_container.Text; ;
            shpdocmstr.ContsainerType = txt_containertype.Text; ;
            shpdocmstr.BL = txtBL.Text;
            shpdocmstr.DocType = "GenDirect";
            shpdocmstr.Mode = drp_deliverymode.SelectedItem.Text;

            try
            {
                string et = Request.Form[dtp_deliverydate.UniqueID].ToString();

                shpdocmstr.ETA = DateTime.Parse(et);
            }
            catch (Exception)
            {
                shpdocmstr.ETA = DateTime.Now; ;

            }
            shpdocmstr.ShippingDocumentDetailsDataCollection = ShippingDocumentDetailsDataofGeneral();
            return shpdocmstr;
        }

        public List<BLL.ShippingBLL.ShippingDocumentDetailsData> ShippingDocumentDetailsDataofGeneral()
        {

            List<BLL.ShippingBLL.ShippingDocumentDetailsData> rk = new List<BLL.ShippingBLL.ShippingDocumentDetailsData>();


            foreach (GridViewRow di in tbl_StockAdn.Rows)
            {
                CheckBox chkBx = (CheckBox)di.FindControl("chk_select");

                if (chkBx != null && chkBx.Checked)
                {




                    int doc_pk = int.Parse(((di.FindControl("lbl_doc_pk") as Label).Text.ToString()));



                    BLL.ShippingBLL.ShippingDocumentDetailsData invdata = new BLL.ShippingBLL.ShippingDocumentDetailsData();


                    invdata.Doc_Pk = doc_pk;



                    rk.Add(invdata);
                }
            }
            return rk;


        }

        #endregion















        protected void btn_sumbit_Click(object sender, EventArgs e)
        {
            String docnum = "";
            if (RadioButton1.Checked == true && tbl_Podetails.Rows.Count > 0)
            {


                if(ArtWebApp.Controls.Gridviewvalidation.countofRowselected(tbl_Podetails, "chk_select") >0)
                {
                    BLL.ShippingBLL.ShippingDocumentMasterData Docmstrdata = GetINVMasterDataForDirect();
                    docnum = Docmstrdata.InsertShippingDocumentDataDirect();
                    tbl_Podetails.DataSource = null;
                    tbl_Podetails.DataBind();
                    docnum = docnum + " is  created Sucessfully";

                }
                else
                {
                    string Msg = "alert('Please select the Documents to be added in this Inbound ')";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", Msg, true);
                }
               
            }
            else if (RadioButton2.Checked == true && tbl_dodetails.Rows.Count > 0)
            {
                if (ArtWebApp.Controls.Gridviewvalidation.countofRowselected(tbl_dodetails, "chk_select") > 0)
                {
                    BLL.ShippingBLL.ShippingDocumentMasterData Docmstrdata = GetINVMasterDataForVia();
                docnum = Docmstrdata.InsertShippingDocumentDataVia();
                tbl_dodetails.DataSource = null;
                tbl_dodetails.DataBind();
                docnum = docnum + " is  created Sucessfully";
                }
                else
                {
                    string Msg = "alert('Please select the Documents to be added in this Inbound ')";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", Msg, true);
                }
            }

            else if (rbt_directgenitem.Checked == true && tbl_dodetails.Rows.Count > 0)
            {
                if (ArtWebApp.Controls.Gridviewvalidation.countofRowselected(tbl_StockAdn, "chk_select") > 0)
                {
                    BLL.ShippingBLL.ShippingDocumentMasterData Docmstrdata = GetINVMasterDataForGeneralDirect();
                    docnum = Docmstrdata.InsertShippingDocumentDataDirectGeneral();
                    tbl_Podetails.DataSource = null;
                    tbl_Podetails.DataBind();
                    docnum = docnum + " is  created Sucessfully";

                }
                else
                {
                    string Msg = "alert('Please select the Documents to be added in this Inbound ')";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", Msg, true);
                }

            }

            else if (rbt_viageneralitem.Checked == true && tbl_stockDO.Rows.Count > 0)
            {
                if (ArtWebApp.Controls.Gridviewvalidation.countofRowselected(tbl_stockDO, "chk_select") > 0)
                {
                    BLL.ShippingBLL.ShippingDocumentMasterData Docmstrdata = GetINVMasterDataForVia();
                    docnum = Docmstrdata.InsertShippingDocumentDataViaGeneral();
                    tbl_dodetails.DataSource = null;
                    tbl_dodetails.DataBind();
                    docnum = docnum + " is  created Sucessfully";

                }
                else
                {
                    string Msg = "alert('Please select the Documents to be added in this Inbound ')";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", Msg, true);
                }

            }

            if (docnum != "")
            {


                ArtWebApp.Controls.Messagebox.MessgeboxUpdate(Messaediv, "sucess", docnum);
                tbl_Podetails.DataSource = null;
                tbl_Podetails.DataBind();
                upd_grid.Update();
            }


        }

        protected void RadioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void RadioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
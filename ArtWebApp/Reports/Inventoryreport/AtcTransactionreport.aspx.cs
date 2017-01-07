using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ArtWebApp.DBTransaction;
using System.Data;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Drawing;
using System.Web.UI.HtmlControls;
using ArtWebApp.DataModels;

namespace ArtWebApp.Reports.Inventoryreport
{
    public partial class AtcTransactionreport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {


            }

        }

        public override void
   VerifyRenderingInServerForm(Control control)
        {
            return;
        }



        public void fillrmnumber()
        {
            DataTable dt = (DataTable)(ViewState["Bomdata"]);

            DataView sview = new DataView(dt);
            DataTable rawmaterialdata = sview.ToTable(true, "Sku_Pk", "RMNum");
            drp_rmnum.DataSource = rawmaterialdata;
            drp_rmnum.DataBind();

        }





        protected void ShowBom_Click(object sender, EventArgs e)
        {

            var stopwatch = new Stopwatch();
            stopwatch.Start();
            ShowBOM();
         
            long elapsed_time = stopwatch.ElapsedMilliseconds;
        }


        public void ShowBOM()
        {

            string onhandtype = "A";
            DataTable BomData = BLL.InventoryBLL.FactoryInventory .GetBOM(int.Parse(cmb_atc.SelectedValue.ToString()));
          
            DataTable Inbounddata = BLL.FactoryAtcChart.GetInboundData(int.Parse(cmb_atc.SelectedValue.ToString()));

            DataTable Podataofatc = BLL.FactoryAtcChart.GetPODataofAtc(int.Parse(cmb_atc.SelectedValue.ToString()));


            DataTable MRNofatc = BLL.InventoryBLL.FactoryInventory.GetMRNetails(int.Parse(cmb_atc.SelectedValue.ToString()));

            DataTable DORfatc = BLL.InventoryBLL.FactoryInventory.GetDOR(int.Parse(cmb_atc.SelectedValue.ToString()));

            DataTable Loanfatc = BLL.InventoryBLL.FactoryInventory.GetLoanDetails(int.Parse(cmb_atc.SelectedValue.ToString()));

            DataTable DOfatc = BLL.InventoryBLL.FactoryInventory.GetDODetails(int.Parse(cmb_atc.SelectedValue.ToString()));

            DataTable LoanOutfatc = BLL.InventoryBLL.FactoryInventory.GetLoanOutDetails(int.Parse(cmb_atc.SelectedValue.ToString()));
            DataTable ROOutfatc = BLL.InventoryBLL.FactoryInventory.GetROOutDetails(int.Parse(cmb_atc.SelectedValue.ToString()));

            if (chk_ct.Checked == true)
            {
                DataTable cutorderofatc = BLL.FactoryAtcChart.GetCutOrderDetails(int.Parse(cmb_atc.SelectedValue.ToString()));
                ViewState["cutorderofatc"] = cutorderofatc;
            }

            if (chk_doc.Checked == true)
            {
                DataTable AdnofATC = BLL.FactoryAtcChart.GetADNDetails(int.Parse(cmb_atc.SelectedValue.ToString()));
                ViewState["AdnofATC"] = AdnofATC;
            }

            if (chk_remark.Checked == true)
            {
                DataTable RemarkofATC = BLL.FactoryAtcChart.GetPlanningRemark(int.Parse(cmb_atc.SelectedValue.ToString()));
                ViewState["RemarkofATC"] = RemarkofATC;
            }
            if ((chk_f.Checked == true) && (chk_W.Checked == false))
            {
                onhandtype = "F";
            }
            else if ((chk_f.Checked == false) && (chk_W.Checked == true))
            {
                onhandtype = "W";
            }
            else
            {
                onhandtype = "A";
            }
            DataTable onhandqty = BLL.FactoryAtcChart.GetOnhandQty(int.Parse(cmb_atc.SelectedValue.ToString()), onhandtype);

            if (BomData.Rows.Count <= 0)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Art", "alert('No BOM Available');", true);
            }
            else
            {

                ViewState["Bomdata"] = BomData;
                
                ViewState["Inbounddata"] = Inbounddata;
                ViewState["Podata"] = Podataofatc;
                ViewState["onhandqty"] = onhandqty;
                ViewState["MRNofatc"] = MRNofatc;
                ViewState["DORfatc"] = DORfatc;
                ViewState["Loanfatc"] = Loanfatc;
                ViewState["DOfatc"] = DOfatc;
                ViewState["LoanOutfatc"] = LoanOutfatc;
                ViewState["ROOutfatc"] = ROOutfatc;
                
                fillrmnumber();
                tbl_bom.DataSource = BomData;
                tbl_bom.DataBind();
                Upd_maingrid.Update();

            }
        }





























        protected void tbl_bom_DataBound1(object sender, EventArgs e)
        {
            for (int i = tbl_bom.Rows.Count - 1; i > 0; i--)
            {
                GridViewRow row = tbl_bom.Rows[i];
                GridViewRow previousRow = tbl_bom.Rows[i - 1];
                for (int j = 0; j < 2; j++)
                {
                    if (row.Cells[j].Text == previousRow.Cells[j].Text)
                    {
                        if (previousRow.Cells[j].RowSpan == 0)
                        {
                            if (row.Cells[j].RowSpan == 0)
                            {
                                previousRow.Cells[j].RowSpan += 2;
                            }
                            else
                            {
                                previousRow.Cells[j].RowSpan = row.Cells[j].RowSpan + 1;
                            }
                            row.Cells[j].Visible = false;
                        }
                    }
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=GridViewExport.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                //To Export all pages
                tbl_bom.AllowPaging = false;


                tbl_bom.HeaderRow.BackColor = Color.White;
                foreach (TableCell cell in tbl_bom.HeaderRow.Cells)
                {
                    cell.BackColor = tbl_bom.HeaderStyle.BackColor;
                }
                foreach (GridViewRow row in tbl_bom.Rows)
                {
                    row.BackColor = Color.White;
                    foreach (TableCell cell in row.Cells)
                    {
                        if (row.RowIndex % 2 == 0)
                        {
                            cell.BackColor = tbl_bom.AlternatingRowStyle.BackColor;
                        }
                        else
                        {
                            cell.BackColor = tbl_bom.RowStyle.BackColor;
                        }
                        cell.CssClass = "textmode";
                    }
                }

                tbl_bom.RenderControl(hw);

                //style to format numbers to string
                string style = @"<style> .textmode { } </style>";
                Response.Write(style);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
        }

        protected void tbl_bom_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int skudetpk = int.Parse((e.Row.FindControl("lbl_skudetpk") as Label).Text);
             


                float planndqty = 0;
                DataTable newresult = new DataTable();
               
                  


                try
                {
                    DataTable dt1 = (DataTable)(ViewState["Inbounddata"]);
                    DataTable iinboundtemp = dt1.Select("Skudet_Pk=" + skudetpk).CopyToDataTable();

                    GridView tbl_shipping = (e.Row.FindControl("tbl_shipping") as GridView);
                    tbl_shipping.DataSource = iinboundtemp;
                    tbl_shipping.DataBind();
                }
                catch (Exception)
                {


                }
                try
                {
                    DataTable dt2 = (DataTable)(ViewState["Podata"]);
                    DataTable Podatatemp = dt2.Select("Skudet_Pk=" + skudetpk).CopyToDataTable();

                    GridView tbl_PO = (e.Row.FindControl("tbl_PO") as GridView);
                    tbl_PO.DataSource = Podatatemp;
                    tbl_PO.DataBind();
                }
                catch (Exception)
                {


                }

                try
                {
                    DataTable dt3 = (DataTable)(ViewState["onhandqty"]);
                    DataTable onhandqtytemp = dt3.Select("Skudet_Pk=" + skudetpk).CopyToDataTable();

                    GridView tbl_onhand = (e.Row.FindControl("tbl_onhand") as GridView);
                    tbl_onhand.DataSource = onhandqtytemp;
                    tbl_onhand.DataBind();
                }
                catch (Exception)
                {



                }
                try
                {
                    if (chk_ct.Checked == true)
                    {
                        DataTable dt4 = (DataTable)(ViewState["cutorderofatc"]);
                        DataTable cutordertemp = dt4.Select("Skudet_Pk=" + skudetpk).CopyToDataTable();

                        GridView tbl_cutorder = (e.Row.FindControl("tbl_cutorder") as GridView);
                        tbl_cutorder.DataSource = cutordertemp;
                        tbl_cutorder.DataBind();
                    }
                }
                catch (Exception)
                {


                }
                try
                {
                    if (chk_doc.Checked == true)
                    {
                        DataTable dt5 = (DataTable)(ViewState["AdnofATC"]);
                        DataTable cutordertemp = dt5.Select("Skudet_Pk=" + skudetpk).CopyToDataTable();

                        GridView tbl_ADN = (e.Row.FindControl("tbl_ADN") as GridView);
                        tbl_ADN.DataSource = cutordertemp;
                        tbl_ADN.DataBind();
                    }
                }
                catch (Exception)
                {

                    
                }
               

                
                try
                {
                   
                        DataTable dt6 = (DataTable)(ViewState["MRNofatc"]);
                        DataTable cutordertemp = dt6.Select("Skudet_Pk=" + skudetpk).CopyToDataTable();

                        GridView tbl_MRN = (e.Row.FindControl("tbl_MRN") as GridView);
                          tbl_MRN.DataSource = cutordertemp;
                          tbl_MRN.DataBind();
                   
                }
                catch (Exception)
                {


                }
                try
                {
                    

                       DataTable dt7 = (DataTable)(ViewState["DORfatc"]);
                    DataTable cutordertemp = dt7.Select("Skudet_Pk=" + skudetpk).CopyToDataTable();

                    GridView tbl_DOR = (e.Row.FindControl("tbl_DOR") as GridView);
                    tbl_DOR.DataSource = cutordertemp;
                    tbl_DOR.DataBind();

                }
                catch (Exception)
                {


                }
                try
                {
                   

                       DataTable dt8 = (DataTable)(ViewState["Loanfatc"]);
                    DataTable cutordertemp = dt8.Select("Skudet_Pk=" + skudetpk).CopyToDataTable();

                    GridView tbl_loan = (e.Row.FindControl("tbl_loan") as GridView);
                    tbl_loan.DataSource = cutordertemp;
                    tbl_loan.DataBind();

                }
                catch (Exception)
                {


                }

                try
                {


                    DataTable dt9 = (DataTable)(ViewState["DOfatc"]);
                    DataTable cutordertemp = dt9.Select("Skudet_Pk=" + skudetpk).CopyToDataTable();

                    GridView tbl_DO = (e.Row.FindControl("tbl_DO") as GridView);
                    tbl_DO.DataSource = cutordertemp;
                    tbl_DO.DataBind();

                }
                catch (Exception)
                {

                    
                }
                try
                {


                    DataTable dt10 = (DataTable)(ViewState["LoanOutfatc"]);
                    DataTable cutordertemp = dt10.Select("Skudet_Pk=" + skudetpk).CopyToDataTable();

                    GridView tbl_LoanOut = (e.Row.FindControl("tbl_LoanOut") as GridView);
                    tbl_LoanOut.DataSource = cutordertemp;
                    tbl_LoanOut.DataBind();

                }
                catch (Exception)
                {
                    


                }
                try
                {


                    DataTable dt11 = (DataTable)(ViewState["ROOutfatc"]);
                    DataTable cutordertemp = dt11.Select("Skudet_Pk=" + skudetpk).CopyToDataTable();

                    GridView tbl_ROOut = (e.Row.FindControl("tbl_ROOut") as GridView);
                    tbl_ROOut.DataSource = cutordertemp;
                    tbl_ROOut.DataBind();

                }
                catch (Exception)
                {
                    


                }
            }
        }

        protected void ShowRawmaterialBOM_Click(object sender, EventArgs e)
        {
            ArrayList popaklist = new ArrayList();
            List<Infragistics.Web.UI.ListControls.DropDownItem> items = drp_rmnum.SelectedItems;
            foreach (Infragistics.Web.UI.ListControls.DropDownItem item in items)
            {

                int popackid = int.Parse(item.Value.ToString());
                popaklist.Add(popackid);
            }


            if (popaklist.Count > 0 && popaklist != null)
            {
                string condition = "";
                for (int i = 0; i < popaklist.Count; i++)
                {



                    if (i == 0)
                    {
                        condition = condition + " Sku_Pk=" + popaklist[i].ToString().Trim();
                    }
                    else
                    {
                        condition = condition + "  or  Sku_Pk=" + popaklist[i].ToString().Trim();
                    }



                }
                DataTable dt = (DataTable)(ViewState["Bomdata"]);


                DataTable newresult = dt.Select(condition).CopyToDataTable();
                tbl_bom.DataSource = newresult;
                tbl_bom.DataBind();
                Upd_maingrid.Update();
            }
        }

        protected void chk_W_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
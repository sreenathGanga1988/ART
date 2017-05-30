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
using System.Globalization;
using System.Web.Services;
using ArtWebApp.BLL.MerchandsingBLL;

namespace ArtWebApp.Merchandiser.Atc_Chart
{
    public partial class ProcurmentPlaning : System.Web.UI.Page
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









        protected void ShowBom_Click(object sender, EventArgs e)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            ShowBOM();
            //   Session["atcid"] = int.Parse(cmb_atc.SelectedValue.ToString());
            fillrmnumber();
            stopwatch.Stop();
            long elapsed_time = stopwatch.ElapsedMilliseconds;
        }


        public void fillrmnumber()
        {
            DataTable dt = (DataTable)(ViewState["Bomdata"]);

            DataView sview = new DataView(dt);
            DataTable rawmaterialdata = sview.ToTable(true, "Sku_Pk", "RMNum");
            drp_rmnum.DataSource = rawmaterialdata;
            drp_rmnum.DataBind();

        }


        public void ShowBOM()
        {


            DataTable BomData = BLL.FactoryAtcChart.ShowBOM(int.Parse(cmb_atc.SelectedValue.ToString()),"atc",0);
            DataTable procurementplandata = BLL.FactoryAtcChart.GetProcurementPlan(int.Parse(cmb_atc.SelectedValue.ToString()));
            DataTable Remarkdata = BLL.FactoryAtcChart.GetPlanningRemark(int.Parse(cmb_atc.SelectedValue.ToString()));

            if (BomData.Rows.Count <= 0)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Art", "alert('No BOM Available');", true);
            }
            else
            {
               
                ViewState["Bomdata"] = BomData;
                ViewState["ProPlandata"] = procurementplandata;
                ViewState["Remarkdata"] = Remarkdata;
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
                for (int j = 1; j < 3; j++)
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

        protected void ShowRawmaterialBOM_Click(object sender, EventArgs e)
        {
            DataTable procurementplandata = BLL.FactoryAtcChart.GetProcurementPlan(int.Parse(cmb_atc.SelectedValue.ToString()));
            ViewState["ProPlandata"] = procurementplandata;
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

        protected void tbl_bom_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int skudetpk = int.Parse((e.Row.FindControl("lbl_skudetpk") as Label).Text);
                float lbl_poissuedqty = float.Parse((e.Row.FindControl("lbl_poissuedqty") as Label).Text);
                float lbl_reqdqty = float.Parse((e.Row.FindControl("lbl_reqdqty") as Label).Text);
                
                float planndqty = 0;
                DataTable newresult = new DataTable();
                try
                {
                    try
                    {
                        DataTable dt = (DataTable)(ViewState["ProPlandata"]);


                        newresult = dt.Select("Skudet_Pk=" + skudetpk).CopyToDataTable();

                        object plannedqtyob = newresult.Compute("Sum(Qty)", "");
                        if (plannedqtyob.ToString().Trim() == "")
                        {
                            planndqty = 0;
                        }
                        else
                        {
                            planndqty = float.Parse(plannedqtyob.ToString());
                        }
                    }
                    catch (Exception)
                    {

                        planndqty=0;
                    }

                   
                    float baltoplan = lbl_reqdqty - planndqty;
                    if (baltoplan > 0)
                    {

                        HtmlTable tbl = (e.Row.FindControl("fillfull") as HtmlTable);
                        (e.Row.FindControl("txt_qty") as TextBox).Text = baltoplan.ToString();
                        tbl.Attributes.Add("Class", "Appear");

                    }
                    else
                    {

                    }


                    if (planndqty > 0)
                    {
                        GridView tbleta = (e.Row.FindControl("tbl_eta") as GridView);
                        tbleta.DataSource = newresult;
                        tbleta.DataBind();

                    }

                (e.Row.FindControl("lbl_plannedqty") as Label).Text = planndqty.ToString();
                    (e.Row.FindControl("lbl_balplanqty") as Label).Text = baltoplan.ToString();

                }
                catch
                {
                    planndqty = 0;
                }



                


                   
            }
        }

        protected void btn_savePlan_Click(object sender, EventArgs e)
        {
            BLL.MerchandsingBLL.ProcurementplanMasterBLL ppbll = new BLL.MerchandsingBLL.ProcurementplanMasterBLL();
            if (checkdatagridValue(tbl_bom, "lbl_balplanqty", "txt_qty"))
            {
                InsertPodetails();
                DataTable procurementplandata = BLL.FactoryAtcChart.GetProcurementPlan(int.Parse(cmb_atc.SelectedValue.ToString()));
                ViewState["ProPlandata"] = procurementplandata;

                tbl_bom.DataBind();
            }
            else
            {
              
             
            }

        }




        public void InsertPodetails()
        {
            BLL.MerchandsingBLL.ProcurementplanMasterBLL ppmbll = new BLL.MerchandsingBLL.ProcurementplanMasterBLL();
            ppmbll.ProcurementplanDetailsDataCollection = GetProcurementplanDetailsData();
            ppmbll.insertPlaningMaster();
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
        public List<BLL.MerchandsingBLL.ProcurementplanDetailsData> GetProcurementplanDetailsData()
        {

            List<BLL.MerchandsingBLL.ProcurementplanDetailsData> rk = new List<BLL.MerchandsingBLL.ProcurementplanDetailsData>();


            foreach (GridViewRow di in tbl_bom.Rows)
            {
                CheckBox chkBx = (CheckBox)di.FindControl("chk_select");

                if (chkBx != null && chkBx.Checked)
                {
                    int skudetpk = int.Parse(((di.FindControl("lbl_skudetpk") as Label).Text.ToString()));
               
                    decimal txt_qty = decimal.Parse(((di.FindControl("txt_qty") as TextBox).Text.ToString()));

                    TextBox dtp_deliverydate = (di.FindControl("dtp_deliverydate") as TextBox);
                    string s = DateTime.Parse(Request.Form[dtp_deliverydate.UniqueID].ToString()).ToString("dd/MMM/yyyy", CultureInfo.InvariantCulture);

                    BLL.MerchandsingBLL.ProcurementplanDetailsData deldet = new BLL.MerchandsingBLL.ProcurementplanDetailsData();
                    deldet.Skudet_Pk = skudetpk;
                    deldet.Qty = txt_qty;
                    deldet.ETADate = DateTime.Parse(s);
                    rk.Add(deldet);
                }
            }
            return rk;


        }

        protected void tbl_bom_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = tbl_bom.Rows[index];
            if (e.CommandName == "AddRemark")
            {

                     CheckBox chkBx = (CheckBox)row.FindControl("chk_select");
                if (chkBx.Checked == true)
                {
                    int skudetpk = int.Parse((row.FindControl("lbl_skudetpk") as Label).Text);
                    BLL.MerchandsingBLL.ProcurementplanRemarkData deldet = new BLL.MerchandsingBLL.ProcurementplanRemarkData();


                    TextBox remark = (row.FindControl("txt_remark") as TextBox);
                    if(remark.Text!="")
                    {
                        deldet.Skudet_Pk = skudetpk;
                        deldet.Remark = remark.Text;
                        remark.Text = "";
                        deldet.insertRemark();
                   
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Remark Added SucessFully');", true);
                    }

                   


                    

                }
            }
           
            else if (e.CommandName == "AddETA")
            {
                int skudetpk = int.Parse(((row.FindControl("lbl_skudetpk") as Label).Text.ToString()));

                TextBox qty = (row.FindControl("txt_qty") as TextBox);
                decimal txt_qty = decimal.Parse(qty.Text.ToString());

                TextBox dtp_deliverydate = (row.FindControl("dtp_deliverydate") as TextBox);
                string s = DateTime.Parse(Request.Form[dtp_deliverydate.UniqueID].ToString()).ToString("dd/MMM/yyyy", CultureInfo.InvariantCulture);

                BLL.MerchandsingBLL.ProcurementplanDetailsData deldet = new BLL.MerchandsingBLL.ProcurementplanDetailsData();
                if (qty.Text != "")
                {
                    deldet.Skudet_Pk = skudetpk;
                    deldet.Qty = txt_qty;
                    deldet.ETADate = DateTime.Parse(s);
                    qty.Text = "";
                    deldet.insertETA();

                    
                   
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('ETA Added');", true);
                //    ScriptManager.RegisterStartupScript(                UpdatePanelID,
                //UpdatePanelID.GetType(),
                //"Create Time Table",
                //" alert('Time Table Created Successfully.'); window.location.href = 'create.aspx';",
                //true);
                }
            }

        }


        [WebMethod]
        public static string DeletePlanAysnc(int Planid)
        {
            ProcurementplanMasterBLL pbll = new ProcurementplanMasterBLL();

            return pbll.DeletePlaning(Planid); ;
        }





    }
}
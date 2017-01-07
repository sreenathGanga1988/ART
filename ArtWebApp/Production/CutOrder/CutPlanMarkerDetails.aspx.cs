using ArtWebApp.DataModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArtWebApp.Production.CutOrder
{
    public partial class CutPlanMarkerDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillAtcCombo();

            }
            else
            {
                 GenerateTable();
            }
        }


        public void FillAtcCombo()
        {
            using (ArtEntitiesnew entty = new ArtEntitiesnew())
            {
                var q = from atcorder in entty.AtcMasters
                        select new
                        {
                            name = atcorder.AtcNum,
                            pk = atcorder.AtcId
                        };

                // Create a table from the query.


                drp_atc.DataSource = q.ToList();
                drp_atc.DataBind();
                upd_atc.Update();




            }
        }
        public void FillOurStyleCombo(int atcid)
        {
            using (ArtEntitiesnew entty = new ArtEntitiesnew())
            {
                var q = from ponmbr in entty.AtcDetails
                        where ponmbr.AtcId == atcid
                        select new
                        {
                            name = ponmbr.OurStyle,
                            pk = ponmbr.OurStyleID
                        };

                drp_ourstyle.DataSource = q.ToList();
                drp_ourstyle.DataBind();
                upd_ourstyle.Update();



            }
        }


        protected void btn_showfromLoc_Click(object sender, EventArgs e)
        {

        }

        protected void chk_selectAll_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void btn_sumbit_Click(object sender, EventArgs e)
        {

        }

        protected void btn_atc_Click(object sender, EventArgs e)
        {
            FillOurStyleCombo(int.Parse(drp_atc.SelectedValue.ToString()));
        }

        public void FillAllcutorder(int ourstyleid)
        {


            using (ArtEntitiesnew entty = new ArtEntitiesnew())
            {
                var q = from ponmbr in entty.CutPlanMasters
                        where ponmbr.OurStyleID == ourstyleid
                        select new
                        {
                            name = ponmbr.CutPlanNUM,

                            //  name=ponmbr.CostingCount,
                            pk = ponmbr.CutPlan_PK
                        };


                drp_cutorder.DataSource = q.ToList();
                drp_cutorder.DataBind();
                upd_cutorder.Update();



            }

        }




        protected void btn_OURSTYLE_Click(object sender, EventArgs e)
        {


            //  fillsizedata();
            FillAllcutorder(int.Parse(drp_ourstyle.SelectedValue.ToString().ToString()));

        }
        protected void btn_cutorder_Click(object sender, EventArgs e)
        {
            fillsmalltable();
            showGrid();
        }
        public void fillsmalltable()
        {
            ViewState["sizedatamaster"] = null;
            ViewState["sizedataheader"] = null;
            ViewState["Sizeratiodata"] = null;
            //  DataTable dt = createdatatable(int.Parse(drp_ourstyle.SelectedValue.ToString().ToString()));

            DataTable dt = BLL.popackupdater.createdatatable(int.Parse(drp_ourstyle.SelectedValue.ToString()));
            ViewState["sizedatamaster"] = dt;

            DataTable dtheader = dt.Copy();

            ViewState["sizedataheader"] = AddValueToHeader(dtheader);
            ViewState["Sizeratiodata"] = createdatatableforRatio(dt);


            GenerateSmallTable();

        }


        public DataTable AddValueToHeader(DataTable dt)
        {
            DataRow row1 = dt.NewRow();

            for (int i = 0; i < dt.Columns.Count; i++)
            {
                row1[i] = 0;
            }

            dt.Rows.Add(row1);

            DataRow AlreadyCutrow = dt.NewRow();

            for (int i = 0; i < dt.Columns.Count; i++)
            {
                AlreadyCutrow[i] = 0;
            }

            dt.Rows.Add(AlreadyCutrow);




            dt.Rows[0]["Color"] = "Cut Plan Qty";
            dt.Rows[1]["Color"] = "Marker Qty";
            dt.Rows[2]["Color"] = "Not Assigned";


            DataTable cutplandata = BLL.CutOrderBLL.CutPlan.fillCutplanqty(int.Parse(drp_cutorder.SelectedValue.ToString()));




            for (int i = 1; i < dt.Columns.Count; i++)
            {
                String Sizename = dt.Columns[i].ColumnName.ToString();
                try
                {
                    object SumofcutQty = cutplandata.Compute("Sum(CutQty)", "SizeName= '" + Sizename + "' ");
                    if (SumofcutQty.ToString().Trim() == "")
                    {
                        SumofcutQty = "0";
                    }

                    dt.Rows[0][i] = SumofcutQty.ToString();
                }
                catch (Exception)
                {

                    object SumofcutQty = 0;

                    dt.Rows[0][i] = SumofcutQty.ToString();
                }


            }

            return dt;
        }








        public void showGrid()
        {
            DataTable table = new DataTable();
            table.Columns.Add("CutPlanMarkerDetails_PK", typeof(int));
            table.Columns.Add("MarkerNo", typeof(string));

            table.Columns.Add("Qty", typeof(string));



            for (int i = 0; i < 12; i++)
            {
                DataRow row = table.NewRow();
                row[0] = i + 1;
                row[1] = (i + 1).ToString();
                row[2] = "0";
                table.Rows.Add(row);
            }

            tbl_cutorderdata.DataSource = table;
            tbl_cutorderdata.DataBind();

            upd_gridtable.Update();
        }

        public System.Data.DataTable createdatatableforRatio(DataTable dt)
        {

            dt.Columns.Add("Total", typeof(String));

            dt.Rows[0]["Total"] = 0;

            DataRow row1 = dt.NewRow();

            for (int i = 0; i < dt.Columns.Count; i++)
            {
                row1[i] = 0;
            }

            dt.Rows.Add(row1);


            return dt;
        }

        protected void btn_marker_Click(object sender, EventArgs e)
        {

        }


        protected void Button1_Click(object sender, EventArgs e)
        {

        }

        protected void btn_add_Click(object sender, EventArgs e)
        {
            int k = 0;
        }

        protected void tbl_cutorderdata_RowCommand(object sender, GridViewCommandEventArgs e)
        {



            //DataTable dt = new DataTable();

            //dt.Columns.Add("Index", typeof(String));
            //dt.Columns.Add("Size", typeof(String));





            //int index = Convert.ToInt32(e.CommandArgument);
            //GridViewRow row = tbl_cutorderdata.Rows[index];
            //if (e.CommandName == "Add")
            //{



            //    CheckBox chkBx = (CheckBox)row.FindControl("chk_select");
            //    if (chkBx.Checked == true)
            //    {

            //        int CutOrderDet_PK = int.Parse((row.FindControl("lbl_CutOrderDet_PK") as Label).Text);
            //        BLL.CutOrderBLL.CutPlanMarkerDetailsData cddetdata = new BLL.CutOrderBLL.CutPlanMarkerDetailsData();
            //        cddetdata.CutPlanSizeDetailsDataCollection = GetSizedata(CutOrderDet_PK, row);
            //        cddetdata.InsertCutOrderSizeData();
            //    }
            //}
            //else if (e.CommandName == "ShowDropDown")
            //{

            //}

        }






        protected void tbl_cutorderdata_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void tbl_cutorderdata_DataBound(object sender, EventArgs e)
        {
            GenerateTable();
        }




        public List<BLL.CutOrderBLL.CutPlanSizeDetailsData> GetSizedata(int cutdetPK, GridViewRow row)
        {

            List<BLL.CutOrderBLL.CutPlanSizeDetailsData> rk = new List<BLL.CutOrderBLL.CutPlanSizeDetailsData>();




            Panel panel1 = (row.FindControl("panel1") as Panel);
            Table Table1 = (row.FindControl("Table1") as Table);




            for (int tabroindex = 0; tabroindex < Table1.Rows.Count - 1; tabroindex++)
            {
                TableRow tbrow = Table1.Rows[tabroindex];

                for (int tabcellindex = 0; tabcellindex < tbrow.Cells.Count; tabcellindex++)
                {

                    TableCell cell = tbrow.Cells[tabcellindex];

                    for (int tabcntrlindex = 0; tabcntrlindex < cell.Controls.Count; tabcntrlindex++)
                    {
                        Control ctrl = cell.Controls[tabcntrlindex];


                        if (ctrl is TextBox)
                        {
                            TextBox txtqty = (TextBox)ctrl;

                            TextBox txtratio = (TextBox)Table1.Rows[2].Cells[tabcellindex].Controls[tabcntrlindex];

                            if (txtqty.Text == "Qty" || txtqty.Text == "Ratio")
                            {

                            }
                            else
                            {



                                Control ctrlsize = Table1.Rows[0].Cells[tabcellindex].Controls[0];
                                Label lblsize = (Label)ctrlsize;


                                if (lblsize.Text.Trim() != "Total")
                                {

                                    BLL.CutOrderBLL.CutPlanSizeDetailsData cutdet = new BLL.CutOrderBLL.CutPlanSizeDetailsData();
                                    cutdet.Sizename = lblsize.Text.Trim();
                                    cutdet.Qty = Decimal.Parse(txtqty.Text);
                                    cutdet.CutPlan_PK = cutdetPK;
                                    cutdet.Ratio = Decimal.Parse(txtratio.Text);



                                    rk.Add(cutdet);
                                }



                            }


                        }
                    }
                }

            }
            return rk;
        }
        public DataTable fillsizedata()
        {
            DataTable dt = new DataTable();
            try
            {


                if (ViewState["sizedatamaster"] == null)
                {
                    dt = BLL.popackupdater.createdatatable(int.Parse(drp_ourstyle.SelectedValue.ToString()));
                    // dt = createdatatable(int.Parse(drp_ourstyle.SelectedValue.ToString().ToString()));
                    ViewState["sizedatamaster"] = dt;
                }
                else
                {
                    dt = (DataTable)ViewState["sizedatamaster"];
                }


            }
            catch
            {

            }
            return dt;
        }

        protected void tbl_cutorderdata_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void Button1_Click1(object sender, EventArgs e)
        {
            upd_gridtable.Update();
        }











        private void GenerateSmallTable()
        {


            DataTable dt = (DataTable)(ViewState["sizedataheader"]);



            Panel panel1 = masterpanel;
            Table Table1 = Mastertable;


            //   Creat the Table and Add it to the Page
            Table1.Rows.Clear();




            Table1.ID = "Mastertable";
            // Page.Form.Controls.Add(Table1);
            panel1.Controls.Add(Table1);
            Table1.CssClass = "Headernewtable";
            //  The number of Columns to be generated
            if (dt != null)

            {
                TableHeaderRow hrow = new TableHeaderRow();
                for (int k = 0; k < dt.Columns.Count; k++)
                {
                    TableHeaderCell hcell = new TableHeaderCell();
                    hcell.Width = 60;
                    hcell.CssClass = "na";
                    Label tb = new Label();
                    tb.Text = dt.Columns[k].ColumnName.ToString();
                    if (tb.Text == "ColorTotal")
                    {
                        tb.Width = 80;
                        tb.CssClass = "headercolor";
                    }
                    else
                    {
                        tb.Width = 60;
                        tb.CssClass = "HeaderSize";
                    }

                    hcell.Controls.Add(tb);
                    //  Add the TableCell to the TableRow
                    hrow.Cells.Add(hcell);
                    hrow.CssClass = "th";
                    //    hcell.CssClass = "Widthclass";
                }
                Table1.Rows.Add(hrow);



                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    TableRow row = new TableRow();
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {





                        TableCell cell = new TableCell();
                        cell.Width = 70;

                        TextBox tb = new TextBox();

                        if (j == 0)
                        {
                            tb.CssClass = "colorname";
                            tb.Enabled = false;
                            tb.Width = 80;
                        }
                        else if (dt.Columns[j].ColumnName == "ColorTotal")
                        {

                            tb.Enabled = false;
                            tb.Width = 70;
                            dt.Rows[i][j] = dt.Rows[i][j].ToString();
                            if (i == dt.Rows.Count - 1)
                            {
                                tb.CssClass = "GrandTotal";
                            }
                            else
                            {
                                tb.CssClass = "ColorTotal";
                            }
                        }
                        else if (i == dt.Rows.Count - 1)
                        {
                            tb.CssClass = "BalQty";
                            tb.Enabled = true;
                            tb.Width = 70;
                            dt.Rows[i][j] = dt.Rows[i][j].ToString();
                        }

                        else if (i == dt.Rows.Count - 2)
                        {
                            tb.CssClass = "NewQty";
                            tb.Enabled = false;
                            tb.Width = 70;
                            dt.Rows[i][j] = dt.Rows[i][j].ToString();
                            tb.Attributes.Add("onchange", "Calculatebalance()");
                        }
                        else if (i == dt.Rows.Count - 3)
                        {
                            tb.CssClass = "AvailQty";
                            tb.Enabled = false;
                            tb.Width = 70;
                            dt.Rows[i][j] = dt.Rows[i][j].ToString();
                            tb.Attributes.Add("onchange", "Calculatebalance()");
                        }


                        //  Set a unique ID for each TextBox added
                        // tb.ReadOnly = true;

                        tb.ID = "tb" + i + j;
                        tb.Text = dt.Rows[i][j].ToString();

                        //    Add the control to the TableCell
                        cell.Controls.Add(tb);
                        //    Add the TableCell to the TableRow
                        cell.CssClass = "td";
                        row.CssClass = "tr";
                        row.Cells.Add(cell);








                    }

                    //  And finally, add the TableRow to the Table
                    Table1.Rows.Add(row);
                }

            }
            // Now iterate through the table and add your controls

            Table1.EnableViewState = true;
            Table1.Enabled = false;

        }



        //public void generatetableonPostback()
        //{
        //    foreach (GridViewRow di in tbl_cutorderdata.Rows)
        //    {
        //        Panel panel1 = (di.FindControl("panel1") as Panel);
        //        Table Table1 = (di.FindControl("Table1") as Table);


        //        //   Creat the Table and Add it to the Page
        //        Table1.Rows.Clear();
        //    }
        //}


        private void GenerateTable()
        {
            DataTable dt = (DataTable)ViewState["Sizeratiodata"];
            //  BLL.CutOrderBLL.CutDetailsData cddetdataclass = new BLL.CutOrderBLL.CutDetailsData();
            foreach (GridViewRow di in tbl_cutorderdata.Rows)
            {





                Panel panel1 = (di.FindControl("panel1") as Panel);
                Table Table1 = (di.FindControl("Table1") as Table);


                //   Creat the Table and Add it to the Page
                Table1.Rows.Clear();




                Table1.ID = "Table1";
                // Page.Form.Controls.Add(Table1);
                panel1.Controls.Add(Table1);
                Table1.CssClass = "dynamicentrytable";
                //  The number of Columns to be generated
                if (dt != null)

                {
                    TableHeaderRow hrow = new TableHeaderRow();
                    for (int k = 0; k < dt.Columns.Count; k++)
                    {
                        TableHeaderCell hcell = new TableHeaderCell();
                        hcell.Width = 10;
                        hcell.CssClass = "na";
                        Label tb = new Label();
                        tb.Text = dt.Columns[k].ColumnName.ToString();
                        tb.Width = 40;
                        tb.CssClass = "Headerlabel";
                        hcell.Controls.Add(tb);
                        //  Add the TableCell to the TableRow
                        hrow.Cells.Add(hcell);
                        hrow.CssClass = "na";
                        //    hcell.CssClass = "Widthclass";
                    }
                    Table1.Rows.Add(hrow);



                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        TableRow row = new TableRow();
                        for (int j = 0; j < dt.Columns.Count; j++)
                        {
                            // if firstcolumn
                            if (j == 0)
                            {

                                if (i == 0)
                                {
                                    TableCell cell = new TableCell();
                                    cell.Width = 40;

                                    TextBox tb = new TextBox();


                                    //  Set a unique ID for each TextBox added

                                    tb.ID = "tb" + i + j;
                                    tb.Text = "Qty";
                                    tb.CssClass = "txtqty";

                                    tb.Width = 40;
                                    tb.Enabled = false;
                                    //  Add the control to the TableCell
                                    cell.Controls.Add(tb);

                                    //   Add the TableCell to the TableRow
                                    //   cell.CssClass = "Widthclass";
                                    row.Cells.Add(cell);
                                }
                                else if (i == 1)
                                {
                                    TableCell cell = new TableCell();
                                    cell.Width = 40;

                                    TextBox tb = new TextBox();


                                    //  Set a unique ID for each TextBox added

                                    tb.ID = "tb" + i + j;
                                    tb.Text = "Ratio";
                                    tb.CssClass = "txtratio";
                                    tb.Enabled = false;
                                    tb.Width = 40;
                                    //      Add the control to the TableCell
                                    cell.Controls.Add(tb);
                                    //   Add the TableCell to the TableRow
                                    //  cell.CssClass = "Widthclass";
                                    row.Cells.Add(cell);
                                }

                            }




                            else if (j == dt.Columns.Count - 1)
                            {
                                if (i == 0)
                                {
                                    TableCell cell = new TableCell();
                                    cell.Width = 40;

                                    TextBox tb = new TextBox();


                                    //   Set a unique ID for each TextBox added

                                    tb.ID = "sum";
                                    tb.Text = "0";
                                    tb.CssClass = "totalQtyRow";

                                    tb.Enabled = false;
                                    //  Add the control to the TableCell
                                    cell.Controls.Add(tb);
                                    tb.Width = 40;
                                    //  Add the TableCell to the TableRow
                                    // cell.CssClass = "Widthclass";
                                    row.Cells.Add(cell);
                                }
                                else if (i == 1)
                                {
                                    TableCell cell = new TableCell();
                                    cell.Width = 40;

                                    TextBox tb = new TextBox();


                                    //   Set a unique ID for each TextBox added

                                    tb.ID = "tb" + i + j;
                                    tb.Text = "0";
                                    tb.CssClass = "totalRatioRow";
                                    tb.Enabled = false;
                                    tb.Width = 40;

                                    //   Add the control to the TableCell
                                    cell.Controls.Add(tb);
                                    //  Add the TableCell to the TableRow
                                    //  cell.CssClass = "Widthclass";
                                    row.Cells.Add(cell);

                                }


                            }



                            else
                            {

                                if (i == 0)
                                {

                                    TableCell cell = new TableCell();
                                    cell.Width = 40;

                                    TextBox tb = new TextBox();

                                    tb.CssClass = "txtCalQty";
                                    //  Set a unique ID for each TextBox added
                                    // tb.ReadOnly = true;
                                    tb.Attributes.Add("onkeypress", "return isNumberKey(event,this)");
                                    //      tb.Attributes.Add("onchange", "sumofQty(this)");
                                    tb.Attributes.Add("onchange", "totalcalculation()");
                                    tb.ID = "tb" + i + j;
                                    tb.Text = dt.Rows[i][j].ToString();
                                    tb.Width = 40;
                                    tb.Enabled = false;
                                    //    Add the control to the TableCell
                                    cell.Controls.Add(tb);
                                    //    Add the TableCell to the TableRow
                                    //  cell.CssClass = "Widthclass";
                                    row.Cells.Add(cell);

                                }

                                else if (i == 1)
                                {
                                    TableCell cell = new TableCell();
                                    cell.Width = 40;

                                    TextBox tb = new TextBox();

                                    tb.CssClass = "txtCalRatio";
                                    //  Set a unique ID for each TextBox added
                                    tb.Attributes.Add("onkeypress", "return isNumberKey(event,this)");
                                    //  tb.Attributes.Add("onchange", "sumofRatio(this)");

                                    tb.Attributes.Add("onchange", "totalcalculation()");

                                    tb.ID = "tb" + i + j;
                                    tb.Text = dt.Rows[i][j].ToString();
                                    tb.Width = 40;
                                    //    Add the control to the TableCell
                                    cell.Controls.Add(tb);
                                    //    Add the TableCell to the TableRow
                                    //  cell.CssClass = "Widthclass";
                                    row.Cells.Add(cell);
                                }


                            }



                        }

                        //  And finally, add the TableRow to the Table
                        Table1.Rows.Add(row);
                    }

                }
                // Now iterate through the table and add your controls

                Table1.EnableViewState = true;
                //     upd_table.Update();
            }
        }

        protected void Button1_Click2(object sender, EventArgs e)
        {
        }

        protected void Button2_Click(object sender, EventArgs e)
        {

        }

        protected void Button1_Click3(object sender, EventArgs e)
        {
            int k = 0;
            foreach (GridViewRow row in tbl_cutorderdata.Rows)
            {





                CheckBox chkBx = (CheckBox)row.FindControl("chk_select");
                if (chkBx.Checked == true)
                {
                    int cutplanpk = int.Parse(drp_cutorder.SelectedValue.ToString());


                    BLL.CutOrderBLL.CutPlanMarkerDetailsData cddetdata = new BLL.CutOrderBLL.CutPlanMarkerDetailsData();

                    cddetdata.Cutperplies = int.Parse((row.FindControl("txt_totalcut") as TextBox).Text);
                    cddetdata.cutreq = int.Parse((row.FindControl("txt_cutreq") as TextBox).Text);
                    cddetdata.NoOfPlies = int.Parse((row.FindControl("txt_totalplies") as TextBox).Text);
                    cddetdata.MarkerNo = (row.FindControl("lbl_markernum") as Label).Text;
                    cddetdata.CutPlan_PK = cutplanpk;
                    cddetdata.CutPlanSizeDetailsDataCollection = GetSizedata(cutplanpk, row);
                    cddetdata.InsertCutOrderMarkerSizeData();
                    k++;
                }

            }
            if (k > 0)
            {
                String msg = " Marker Details Added Sucessfully ";

                ArtWebApp.Controls.Messagebox.MessgeboxUpdate(Messaediv, "sucess", msg);
                tbl_cutorderdata.DataSource = null;
                tbl_cutorderdata.DataBind();
                upd_gridtable.Update();
            }
        }















    }
}
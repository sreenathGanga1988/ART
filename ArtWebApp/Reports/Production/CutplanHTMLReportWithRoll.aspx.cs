using ArtWebApp.DataModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace ArtWebApp.Reports.Production
{
    public partial class CutplanHTMLReportWithRoll : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                String costpk = Session["cutpkrpt"].ToString();
                int cutpkrpt = int.Parse(costpk);
                fillsmalltable(cutpkrpt);
                tbl_cutplanmarkerdata.DataSource = cutplanmarkerdetails;
                tbl_cutplanmarkerdata.DataBind();
                tbl_markertype.DataSource = cutplanmarkertypedata;
                tbl_markertype.DataBind();

                tbl_rolldata.DataBind();

                upd_roll.Update();

                upd_cutplanmarkergrid.Update();
                upd_markertype.Update();
                fillasqgrid();
                fillcutplandetails(cutpkrpt);

            }
            else
            {
                //GenerateTable(fillsizedata());
            }
        }









        protected void btn_cutorder_Click(object sender, EventArgs e)
        {

        }






        public void fillcutplandetails(int cutplanpk)
        {
            using (ArtEntitiesnew entty = new ArtEntitiesnew())
            {
                var q = from ponmbr in entty.CutPlanMasters
                        join ourstyledet in entty.AtcDetails on ponmbr.OurStyleID equals ourstyledet.OurStyleID
                        join atcmstr in entty.AtcMasters on ourstyledet.AtcId equals atcmstr.AtcId
                        join
lctnmstr in entty.LocationMasters on ponmbr.Location_PK equals lctnmstr.Location_PK
                        where ponmbr.CutPlan_PK == cutplanpk
                        select new { ponmbr.BOMConsumption, ponmbr.CutPlanNUM, ponmbr.FabDescription, ponmbr.ShrinkageGroup, ponmbr.WidthGroup, ponmbr.MarkerType, atcmstr.AtcNum, ourstyledet.OurStyle, ponmbr.AddedBy, ponmbr.AddedDate, ponmbr.ApprovedBy, ponmbr.ApprovedDate, ourstyledet.BuyerStyle, ponmbr.RefPattern, ponmbr.MarkerMade, ponmbr.CutplanConsumption, ponmbr.CutplanEfficency, ponmbr.CutPlanFabReq, ponmbr.Fabrication, lctnmstr.LocationName ,ponmbr.RollYard};

                foreach (var element in q)
                {
                    lbl_atc.Text = element.AtcNum.ToString();
                    lbl_ourstyle.Text = element.OurStyle.ToString();
                    lbl_Markertype.Text = element.MarkerType.ToString();
                    lbl_shrink.Text = element.ShrinkageGroup.ToString();
                    lbl_with.Text = element.WidthGroup.ToString();
                    lbl_bomconsumption.Text = element.BOMConsumption.ToString();
                    lbl_fabric.Text = element.FabDescription.ToString();
                    lbl_loc.Text = element.LocationName;

                    lbl_addedBY.Text = element.AddedBy.ToString();
                    lbl_addeddate.Text = element.AddedDate.ToString();
                    try
                    {
                        lbl_approvedBy.Text = element.ApprovedBy.ToString();
                        lbl_approveddate.Text = element.ApprovedDate.ToString();
                    }
                    catch (Exception)
                    {

                       
                    }
                    lbl_cutnum.Text = element.CutPlanNUM.ToString();
                    lbl_style.Text = element.BuyerStyle.ToString();
                    lbl_cutplancons.Text = element.CutplanConsumption.ToString();
                    lbl_cutplaneffc.Text = element.CutplanEfficency.ToString();
                    lbl_patternmode.Text = element.MarkerMade.ToString();
                    lbl_refpattern.Text = element.RefPattern.ToString();
                    lbl_fabreq.Text = element.CutPlanFabReq.ToString();
                    lbl_fabrication.Text = element.Fabrication;
                    lbl_rollyard.Text = element.RollYard.ToString();
                }


                Upd_cutplandetails.Update();
            }
        }





        public void fillsmalltable(int cutpkrpt)
        {

            ViewState["cutplandata"] = null;
            ViewState["Sizeratiodata"] = null;
            //  DataTable dt = createdatatable(int.Parse(drp_ourstyle.SelectedValue.ToString().ToString()));

            DataTable dt1 = BLL.popackupdater.GetCutPlanmarkerSizedetails(cutpkrpt);
            ViewState["cutplandata"] = dt1;

            DataTable dt = BLL.popackupdater.createdatatableofCutPlan(cutpkrpt);
            ViewState["Sizeratiodata"] = dt;

            DataTable dtheader = dt.Copy();

            ViewState["sizedataheader"] = AddValueToHeader(dtheader);


            GenerateSmallTable();

        }

        public DataTable AddValueToHeader(DataTable dt)
        {

            DataTable cutplandata = BLL.CutOrderBLL.CutPlan.fillCutplanqty(int.Parse(Session["cutpkrpt"].ToString()));




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













        private void GenerateTable()
        {
            DataTable dt = (DataTable)ViewState["Sizeratiodata"];

            DataTable cutplandatatemp = (DataTable)ViewState["cutplandata"];


            //  BLL.CutOrderBLL.CutDetailsData cddetdataclass = new BLL.CutOrderBLL.CutDetailsData();
            foreach (GridViewRow di in tbl_cutplanmarkerdata.Rows)
            {
                DataTable cutplandata = new DataTable();

                int CutOrderDet_PK = int.Parse((di.FindControl("lbl_CutOrderDet_PK") as Label).Text);



                try
                {
                    cutplandata = cutplandatatemp.Select("CutPlanMarkerDetails_PK=" + CutOrderDet_PK).CopyToDataTable();
                }
                catch (Exception)
                {


                }

                if (cutplandata.Rows.Count > 0)
                {

                }

                for (int i = 1; i < dt.Columns.Count; i++)
                {
                    dt.Columns[i].ReadOnly = false;
                    String SIZENAME = dt.Columns[i].ColumnName.ToString();


                    try
                    {
                        object Sumtrim = cutplandata.Compute("Sum(Qty)", "CutPlanMarkerDetails_PK= " + CutOrderDet_PK + " and Size ='" + SIZENAME + "'");

                        if (Sumtrim.ToString().Trim() == "")
                        {
                            Sumtrim = "0";
                        }

                        dt.Rows[0][i] = Sumtrim.ToString();
                    }
                    catch (Exception)
                    {

                        dt.Rows[0][i] = "0";
                    }
                    try
                    {
                        object Sumtrim = cutplandata.Compute("Sum(Ratio)", "CutPlanMarkerDetails_PK= " + CutOrderDet_PK + " and  Size ='" + SIZENAME + "'");

                        dt.Rows[1][i] = Sumtrim.ToString();
                    }
                    catch (Exception)
                    {

                        dt.Rows[1][i] = "0";
                    }

                }
                dt = ArtWebApp.Controls.DataTableFunction.SumOfDataColumns(1, dt.Columns.Count - 2, 0, dt.Rows.Count - 1, dt.Columns.Count - 1, dt.Rows.Count - 1, dt);




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
                                    tb.Text = dt.Rows[i][j].ToString();
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
                                    tb.Text = dt.Rows[i][j].ToString();
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







        private void GenerateSmallTable()
        {


            DataTable dt = (DataTable)(ViewState["sizedataheader"]);
            dt = ArtWebApp.Controls.DataTableFunction.SumOfDataColumns(1, dt.Columns.Count - 2, 0, dt.Rows.Count - 1, dt.Columns.Count - 1, dt.Rows.Count - 1, dt);

            DataRow dr = dt.Rows[1];
            dr.Delete();

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





        protected void tbl_cutorderdata_DataBound(object sender, EventArgs e)
        {
            GenerateTable();
        }







        public void fillasqgrid()
        {
            DataTable podetaildata = BLL.CutOrderBLL.CutPlan.GetCutPlanASQSizeData(int.Parse(Session["cutpkrpt"].ToString()));

            ViewState["podetaildata"] = podetaildata;


            DataView view = new DataView(podetaildata);
            DataTable distinctcolorValues = view.ToTable(true, "ASQ", "BuyerPO", "PoPackId", "OurStyleID", "OurStyle", "BuyerStyle", "CutPlan_PK");
            tbl_ASQdata.DataSource = distinctcolorValues;

            tbl_ASQdata.DataBind();
            updASQgrid.Update();
            //fillsmalltable();

            //tbl_ASQdata.DataSource = BLL.CutOrderBLL.CutPlan.GetPOMasterDataofColor(int.Parse(drp_ourstyle.SelectedValue.ToString()), ddl_color.SelectedValue.ToString());
            //tbl_ASQdata.DataBind();
            //updASQgrid.Update();
        }


        private void GenerateASQTable()
        {
            DataTable cutplandata = (DataTable)ViewState["podetaildata"];


            foreach (GridViewRow di in tbl_ASQdata.Rows)
            {


                int popackid = int.Parse((di.FindControl("lbl_popackid") as Label).Text);


                int ourstyleid = int.Parse((di.FindControl("lbl_ourstyleid") as Label).Text);


                DataTable dt = BLL.popackupdater.createdatatableforCutplanASQ(cutplandata, popackid, ourstyleid);
                //       dt = BLL.CutOrderBLL.CutPlan.AddToTalQty(dt);








                dt.Columns.Add("ColorTotal", typeof(System.Int32));

                dt = ArtWebApp.Controls.DataTableFunction.SumOfDataColumns(1, dt.Columns.Count - 2, 0, dt.Rows.Count - 1, dt.Columns.Count - 1, dt.Rows.Count - 1, dt);

                BLL.CutOrderBLL.CutDetailsData cddetdataclass = new BLL.CutOrderBLL.CutDetailsData();




                Panel panel1 = (di.FindControl("panel2") as Panel);
                Table Table1 = (di.FindControl("Table2") as Table);


                //   Creat the Table and Add it to the Page
                Table1.Rows.Clear();




                Table1.ID = "Table1";
                // Page.Form.Controls.Add(Table1);
                panel1.Controls.Add(Table1);
                Table1.CssClass = "Tableclass";
                //  The number of Columns to be generated

                if (dt != null)

                {
                    TableHeaderRow hrow = new TableHeaderRow();
                    for (int k = 0; k < dt.Columns.Count; k++)
                    {
                        TableHeaderCell hcell = new TableHeaderCell();
                        hcell.Width = 60;
                        hcell.CssClass = "HeaderCell";
                        Label tb = new Label();
                        tb.Text = dt.Columns[k].ColumnName.ToString();
                        if (tb.Text == "ColorTotal")
                        {
                            tb.Width = 80;
                        }
                        else
                        {
                            tb.Width = 60;
                        }
                        tb.CssClass = "Headerlabel";
                        hcell.Controls.Add(tb);
                        //  Add the TableCell to the TableRow
                        hrow.Cells.Add(hcell);
                        hrow.CssClass = "thcss";
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
                                tb.CssClass = "Qty";
                                tb.Enabled = true;
                                tb.Width = 70;
                                dt.Rows[i][j] = dt.Rows[i][j].ToString();
                            }
                            else if (i == dt.Rows.Count - 2)
                            {
                                tb.CssClass = "BalQty";
                                tb.Enabled = false;
                                tb.Width = 70;
                                dt.Rows[i][j] = dt.Rows[i][j].ToString();
                            }
                            else
                            {
                                tb.CssClass = "Qty";
                                tb.Enabled = false;
                                tb.Width = 70;
                                dt.Rows[i][j] = dt.Rows[i][j].ToString();
                            }



                            //  Set a unique ID for each TextBox added
                            // tb.ReadOnly = true;
                            tb.Attributes.Add("onkeypress", "return isNumberKey(event,this)");
                            tb.Attributes.Add("onchange", "QtyKeyUp(this)");
                            tb.ID = "tb" + i + j;
                            tb.Text = dt.Rows[i][j].ToString();

                            //    Add the control to the TableCell
                            cell.Controls.Add(tb);
                            //    Add the TableCell to the TableRow
                            //  cell.CssClass = "Widthclass";
                            row.Cells.Add(cell);








                        }

                        //  And finally, add the TableRow to the Table
                        Table1.Rows.Add(row);
                    }

                }
                Table1.EnableViewState = true;
            }



            // Now iterate through the table and add your controls




        }

        protected void tbl_ASQdata_DataBound(object sender, EventArgs e)
        {
            GenerateASQTable();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            BLL.CutOrderBLL.CutPlanMasterData cmstrdata = new BLL.CutOrderBLL.CutPlanMasterData();
            cmstrdata.ApproveCutPlan(int.Parse(Session["cutpkrpt"].ToString()));
            string msg = " CutPlan Updated And Send to Sampling Department";
            ArtWebApp.Controls.Messagebox.MessgeboxUpdate(Messaediv, "sucess", msg);
        }

       
      
       
    }
}
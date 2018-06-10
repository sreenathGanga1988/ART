using ArtWebApp.DataModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArtWebApp.Reports.MerchandiserReport
{
    public partial class ASQMasterReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewState["asqtable"] = null;
                cmb_atc.DataSource = SqlDataSource1;
                cmb_atc.DataBind();
                upd_atc.Update();
              //  filltable();
            }
            filltable();
        }

        protected void buttonAtc_Click(object sender, EventArgs e)
        {
            ViewState["asqtable"] = null;
            fillcontrol();
            DataTable asqtable = BLL.FactoryAtcChart.GetASQDetailsWithSeasonAndLocationandStyle(int.Parse(cmb_atc.SelectedValue.ToString()));

           
             ViewState["asqtable"] = asqtable;
        }
        public void fillcontrol()
        {
            int atcid = int.Parse(cmb_atc.SelectedValue.ToString());

            ArtEntitiesnew enty = new ArtEntitiesnew();
            var PoQuery = from pckmst in enty.PoPackMasters
                          where pckmst.AtcId == atcid
                          select new
                          {
                              name = pckmst.PoPacknum + " " + pckmst.BuyerPO,
                              pk = pckmst.PoPackId
                          };




            drp_popack.DataSource = PoQuery.ToList();
            drp_popack.DataBind();



            drp_style.DataSource = StyledataSource;
            drp_style.DataBind();


            drp_season.DataSource = SeasonDataSource;
            drp_season.DataBind();


            drp_fact.DataSource = FactoryData;
            drp_fact.DataBind();


            upd_fact.Update();
            upd_popack.Update();
            upd_season.Update();
            upd_style.Update();

            

            //showAllPoPackATC();
        }
        protected void cmb_atc_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        public DataTable addRowtotal(DataTable dt)
        {
            DataRow row1 = dt.NewRow();
            row1[0] = "SizeTotal";
            for (int i = 1; i < dt.Columns.Count; i++)
            {
                row1[i] = 0;
            }

            dt.Rows.Add(row1);



            for (int j = 1; j < dt.Columns.Count; j++)
            {
                float colsum = 0;
                for (int i = 0; i < dt.Rows.Count - 1; i++)
                {
                    colsum = colsum + float.Parse(dt.Rows[i][j].ToString());

                }
                dt.Rows[dt.Rows.Count - 1][j] = colsum.ToString();
            }








            return dt;
        }




        private void GenerateTable(DataTable dt, GridViewRow di)
        {

            //dt.Columns.Add("ColorTotal", typeof(System.Int32));

            //dt = ArtWebApp.Controls.DataTableFunction.SumOfDataColumns(1, dt.Columns.Count - 2, 0, dt.Rows.Count - 1, dt.Columns.Count - 1, dt.Rows.Count - 1, dt);

            //dt = addRowtotal(dt);


            BLL.CutOrderBLL.CutDetailsData cddetdataclass = new BLL.CutOrderBLL.CutDetailsData();




            Panel panel1 = (di.FindControl("panel1") as Panel);
            Table Table1 = (di.FindControl("Table1") as Table);


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
                    hcell.CssClass = "na";
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
                    tb.Font.Size = 7;
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





                        TableCell cell = new TableCell();
                        cell.Width = 60;

                        TextBox tb = new TextBox();

                        if (j == 0)
                        {
                            tb.CssClass = "colorname";
                            tb.Enabled = false;
                            tb.Width = 120;
                            tb.Font.Size = 7;
                        }
                        else if (dt.Columns[j].ColumnName == "ColorTotal")
                        {
                            tb.CssClass = "ColorTotal";
                            tb.Enabled = false;
                            tb.Width = 60;
                            dt.Rows[i][j] = dt.Rows[i][j].ToString();
                            tb.Font.Size = 7;
                        }
                        else
                        {
                            tb.CssClass = "Qty";
                            tb.Width = 60;
                            tb.Font.Size = 8;
                        }



                        //  Set a unique ID for each TextBox added
                        // tb.ReadOnly = true;
                        tb.Attributes.Add("onkeypress", "return isNumberKey(event,this)");
                        tb.Attributes.Add("onchange", "sumofQty(this)");
                        tb.ID = "tb" + i + j + dt.Rows[i][j].ToString() + "Row" + i + "col" + j;                     
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
            // Now iterate through the table and add your controls

            Table1.EnableViewState = true;
            Table1.Enabled = false;

        }

        protected void tbl_podata_RowDataBound(object sender, GridViewRowEventArgs e)

        {
            int popackidtemp = 0;
            int ourstyleidtemp = 0;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    try
                    {
                        int ourstyleid = int.Parse((e.Row.FindControl("lbl_ourstyleid") as Label).Text);
                        int popackid = int.Parse((e.Row.FindControl("lbl_popackid") as Label).Text);
                        popackidtemp = popackid;
                        ourstyleidtemp = ourstyleid;

                        if(popackidtemp==85625&& ourstyleidtemp==1033)
                        {
                            popackidtemp = 0;
                        }
                        DataTable dt = (DataTable)(ViewState["asqtable"]);
                        DataTable sizedata = BLL.FactoryAtcChart.createdatatable(dt, popackid, ourstyleid);

                        GenerateTable(sizedata, e.Row);

                        //    GenerateTable(BLL.popackupdater.createdatatable(ourstyleid, popackid), e.Row);


                        string IsAllocated = BLL.popackupdater.IsAllocated(ourstyleid, popackid);

                        (e.Row.FindControl("lbl_allocated") as Label).Text = IsAllocated;

                    }
                    catch (Exception EX)
                    {
                        int k = popackidtemp;
                        int y= ourstyleidtemp;
                        throw;
                    }

                }











            }

        }




        public void filltable()
        {

            foreach(GridViewRow row in tbl_podata.Rows)
            {
                int ourstyleid = int.Parse((row.FindControl("lbl_ourstyleid") as Label).Text);
                int popackid = int.Parse((row.FindControl("lbl_popackid") as Label).Text);

                DataTable dt = (DataTable)(ViewState["asqtable"]);
                try
                {
                    DataTable sizedata = BLL.FactoryAtcChart.createdatatable(dt, popackid, ourstyleid);

                    GenerateTable(sizedata, row);
                }
                catch (Exception)
                {

                   
                }

            }

        }


        protected void tbl_podata_RowCommand(object sender, GridViewCommandEventArgs e)
        {


        }




       

        protected void tbl_podata_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        public void validatepotype()
        {
            int k = 0;
            foreach (GridViewRow row in tbl_podata.Rows)
            {





                CheckBox chkBx = (CheckBox)row.FindControl("chk_select");
                if (chkBx.Checked == true)
                {


                }
            }
        }

        protected void buttonAtc0_Click(object sender, EventArgs e)
        {
            ArrayList popaklist = new ArrayList();
            List<Infragistics.Web.UI.ListControls.DropDownItem> items = drp_popack.SelectedItems;
            foreach (Infragistics.Web.UI.ListControls.DropDownItem item in items)
            {

                int popackid = int.Parse(item.Value.ToString());
                popaklist.Add(popackid);
            }


            if (popaklist.Count > 0 && popaklist != null)
            {

                BLL.MerchandsingBLL.AllocationBLL pkmstrdata = new BLL.MerchandsingBLL.AllocationBLL();

                tbl_podata.DataSource = pkmstrdata.GetPOPACKMasterofListforreport(popaklist);
                //  tbl_podata.DataSource = asqshuffle.GetAllPOPackDataofStyleandPopack(int.Parse(drp_ourstyle.SelectedValue.ToString()), popaklist);
                tbl_podata.DataBind();
                // updgrid2.Update();
            }
            showAsqDataofselectedPO();
            upd_smalltable.Update();
            upd_main.Update();
        }



        public void showAsqDataofselectedPO()
        {
            ArrayList popaklist = new ArrayList();
            List<Infragistics.Web.UI.ListControls.DropDownItem> items = drp_popack.SelectedItems;
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
                        condition = condition + " PoPackId=" + popaklist[i].ToString().Trim();
                    }
                    else
                    {
                        condition = condition + "  or  PoPackId=" + popaklist[i].ToString().Trim();
                    }



                }
                DataTable dt = (DataTable)(ViewState["asqtable"]);
                DataTable newresult = dt.Select(condition).CopyToDataTable();

                DataTable sizedata = BLL.FactoryAtcChart.createdatatable(newresult);
                GenerateSmallTable(sizedata);
            }

        }





        public void showAsqDataofselectedStyle()
        {
            ArrayList popaklist = new ArrayList();
            List<Infragistics.Web.UI.ListControls.DropDownItem> items = drp_style.SelectedItems;
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
                        condition = condition + " OurStyleID=" + popaklist[i].ToString().Trim();
                    }
                    else
                    {
                        condition = condition + "  or  OurStyleID=" + popaklist[i].ToString().Trim();
                    }



                }
                DataTable dt = (DataTable)(ViewState["asqtable"]);
                DataTable newresult = dt.Select(condition).CopyToDataTable();

                DataTable sizedata = BLL.FactoryAtcChart.createdatatable(newresult);
                GenerateSmallTable(sizedata);
            }

        }



        public void showAsqDataofselectedSeason()
        {
            ArrayList popaklist = new ArrayList();
            List<Infragistics.Web.UI.ListControls.DropDownItem> items = drp_season.SelectedItems;
            foreach (Infragistics.Web.UI.ListControls.DropDownItem item in items)
            {

                String  popackid = item.Value.ToString();
                popaklist.Add(popackid);
            }


            if (popaklist.Count > 0 && popaklist != null)
            {
                string condition = "";
                for (int i = 0; i < popaklist.Count; i++)
                {



                    if (i == 0)
                    {
                        condition = condition + " SeasonName='" + popaklist[i].ToString().Trim()+"'";
                    }
                    else
                    {
                        condition = condition + "  or  SeasonName='" + popaklist[i].ToString().Trim() + "'";
                    }



                }
                DataTable dt = (DataTable)(ViewState["asqtable"]);
                DataTable newresult = dt.Select(condition).CopyToDataTable();

                DataTable sizedata = BLL.FactoryAtcChart.createdatatable(newresult);
                GenerateSmallTable(sizedata);
            }

        }

        public void showAsqDataofselectedLocation()
        {
            ArrayList popaklist = new ArrayList();
            List<Infragistics.Web.UI.ListControls.DropDownItem> items = drp_fact.SelectedItems;
            foreach (Infragistics.Web.UI.ListControls.DropDownItem item in items)
            {

                String popackid = item.Value.ToString();
                popaklist.Add(popackid);
            }


            if (popaklist.Count > 0 && popaklist != null)
            {
                string condition = "";
                for (int i = 0; i < popaklist.Count; i++)
                {



                    if (i == 0)
                    {
                        condition = condition + " ExpectedLocation_PK='" + popaklist[i].ToString().Trim() + "'";
                    }
                    else
                    {
                        condition = condition + "  or  ExpectedLocation_PK='" + popaklist[i].ToString().Trim() + "'";
                    }



                }
                DataTable dt = (DataTable)(ViewState["asqtable"]);
                DataTable newresult = dt.Select(condition).CopyToDataTable();

                DataTable sizedata = BLL.FactoryAtcChart.createdatatable(newresult);
                GenerateSmallTable(sizedata);
            }

        }



        public ArrayList getSeasonList()
        {
            ArrayList seasonlist = new ArrayList();
            List<Infragistics.Web.UI.ListControls.DropDownItem> items = drp_season.SelectedItems;
            foreach (Infragistics.Web.UI.ListControls.DropDownItem item in items)
            {

                String popackid = item.Value.ToString();
                seasonlist.Add(popackid);
            }

            return seasonlist;
        }
        public ArrayList GetFactoryList()
        {
            ArrayList factlist = new ArrayList();
            List<Infragistics.Web.UI.ListControls.DropDownItem> items = drp_fact.SelectedItems;
            foreach (Infragistics.Web.UI.ListControls.DropDownItem item in items)
            {

                String popackid = item.Value.ToString();
                factlist.Add(popackid);
            }


            return factlist;
        }




        public ArrayList GetStyleList()
        {
            ArrayList stylelist = new ArrayList();
            List<Infragistics.Web.UI.ListControls.DropDownItem> items = drp_style.SelectedItems;
            foreach (Infragistics.Web.UI.ListControls.DropDownItem item in items)
            {

                String popackid = item.Value.ToString();
                stylelist.Add(popackid);
            }


            return stylelist;
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
                tbl_podata.AllowPaging = false;


                tbl_podata.HeaderRow.BackColor = Color.White;
                foreach (TableCell cell in tbl_podata.HeaderRow.Cells)
                {
                    cell.BackColor = tbl_podata.HeaderStyle.BackColor;
                }
                foreach (GridViewRow row in tbl_podata.Rows)
                {
                    row.BackColor = Color.White;
                    foreach (TableCell cell in row.Cells)
                    {
                        if (row.RowIndex % 2 == 0)
                        {
                            cell.BackColor = tbl_podata.AlternatingRowStyle.BackColor;
                        }
                        else
                        {
                            cell.BackColor = tbl_podata.RowStyle.BackColor;
                        }
                        cell.CssClass = "textmode";
                    }
                }

                tbl_podata.RenderControl(hw);

                //style to format numbers to string
                string style = @"<style> .textmode { } </style>";
                Response.Write(style);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }

        }
        public override void VerifyRenderingInServerForm(Control control)
        {
        
        }
        protected void btn_showallasq_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            try
            {
                 dt = (DataTable)(ViewState["asqtable"]);
            }
            catch (Exception)
            {
                DataTable asqtable = BLL.FactoryAtcChart.GetASQDetailsWithSeasonAndLocationandStyle(int.Parse(cmb_atc.SelectedValue.ToString()));


                ViewState["asqtable"] = asqtable;
                dt = asqtable;
            }

            DataTable sizedata = BLL.FactoryAtcChart.createdatatable(dt);
            tbl_podata.DataSource = SqlDataSource3;
            //  tbl_podata.DataSource = asqshuffle.GetAllPOPackDataofStyleandPopack(int.Parse(drp_ourstyle.SelectedValue.ToString()), popaklist);

            try
            {
                tbl_podata.DataBind();
            }
            catch (Exception exp)
            {

                throw;
            }
           
            GenerateSmallTable(sizedata);
            upd_main.Update();
            upd_smalltable.Update();
        }

        protected void Button1_Click1(object sender, EventArgs e)
        {
            ArrayList popaklist = new ArrayList();
            List<Infragistics.Web.UI.ListControls.DropDownItem> items = drp_style.SelectedItems;
            foreach (Infragistics.Web.UI.ListControls.DropDownItem item in items)
            {

                int popackid = int.Parse(item.Value.ToString());
                popaklist.Add(popackid);
            }


            if (popaklist.Count > 0 && popaklist != null)
            {

                BLL.MerchandsingBLL.AllocationBLL pkmstrdata = new BLL.MerchandsingBLL.AllocationBLL();

                tbl_podata.DataSource = pkmstrdata.GetPOPACKMasterofListofStyleforreport(popaklist);
                //  tbl_podata.DataSource = asqshuffle.GetAllPOPackDataofStyleandPopack(int.Parse(drp_ourstyle.SelectedValue.ToString()), popaklist);
                tbl_podata.DataBind();
                // updgrid2.Update()
                showAsqDataofselectedStyle();
                upd_main.Update();
                upd_smalltable.Update();

            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            int atcid = int.Parse(cmb_atc.SelectedValue.ToString());
            ArrayList popaklist = new ArrayList();
            List<Infragistics.Web.UI.ListControls.DropDownItem> items = drp_season.SelectedItems;
            foreach (Infragistics.Web.UI.ListControls.DropDownItem item in items)
            {

                String popackid = item.Value.ToString();
                popaklist.Add(popackid);
            }


            if (popaklist.Count > 0 && popaklist != null)
            {

                BLL.MerchandsingBLL.AllocationBLL pkmstrdata = new BLL.MerchandsingBLL.AllocationBLL();

                tbl_podata.DataSource = pkmstrdata.GetPOPACKMasterofListofStyleandSeasonforreport(popaklist,atcid);
                //  tbl_podata.DataSource = asqshuffle.GetAllPOPackDataofStyleandPopack(int.Parse(drp_ourstyle.SelectedValue.ToString()), popaklist);
                tbl_podata.DataBind();
                // updgrid2.Update();
                showAsqDataofselectedSeason();
                upd_main.Update();
                upd_smalltable.Update();
            }
        }






        private void GenerateSmallTable(DataTable dt)
        {






            Panel panel1 = mpanel1;
            Table Table1 = mTable1;


            //   Creat the Table and Add it to the Page
            Table1.Rows.Clear();




            Table1.ID = "Mastertable";
            // Page.Form.Controls.Add(Table1);
            panel1.Controls.Add(Table1);
            Table1.CssClass = "Headernewtable";
            Table1.Attributes.Add("Style", "border:1px solid #ccc");
            //  The number of Columns to be generated
            if (dt != null)

            {
                TableHeaderRow hrow = new TableHeaderRow();
                for (int k = 0; k < dt.Columns.Count; k++)
                {
                    TableHeaderCell hcell = new TableHeaderCell();
                    hcell.Width = 80;
                    hcell.CssClass = "na";
                    Label tb = new Label();
                    tb.Text = dt.Columns[k].ColumnName.ToString();
                    if (tb.Text == "Color")
                    {
                        tb.Width = 120;
                    }
                    else if (tb.Text == "ColorTotal")
                    {
                        tb.Width = 80;
                        tb.CssClass = "headercolor";
                    }
                   
                    else
                    {
                        tb.Width = 60;
                        tb.CssClass = "HeaderSize";
                    }
                    tb.Font.Size = 8;
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
                        cell.Width = 100;

                        TextBox tb = new TextBox();

                        if (j == 0)
                        {
                            tb.CssClass = "colorname";
                            tb.Enabled = false;
                            tb.Width = 120;
                        }
                        else if (dt.Columns[j].ColumnName == "ColorTotal")
                        {

                            tb.Enabled = false;
                            tb.Width = 60;
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
                            tb.Enabled = false;
                            tb.Width = 60;
                            dt.Rows[i][j] = dt.Rows[i][j].ToString();
                        }

                        else
                        {
                            tb.CssClass = "Qty";
                            tb.Enabled = false;
                            tb.Width = 60;
                            dt.Rows[i][j] = dt.Rows[i][j].ToString();
                        }



                        //  Set a unique ID for each TextBox added
                        // tb.ReadOnly = true;

                        tb.ID = "tb" + i + j + dt.Rows[i][j].ToString() + "Row" + i + "col" + j;
                        tb.Text = dt.Rows[i][j].ToString();
                        tb.Font.Size = 8;
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


        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            int atcid = int.Parse(cmb_atc.SelectedValue.ToString());
            ArrayList popaklist = new ArrayList();
            upd_atc.Update();
            List<Infragistics.Web.UI.ListControls.DropDownItem> items = drp_fact.SelectedItems;
            foreach (Infragistics.Web.UI.ListControls.DropDownItem item in items)
            {

                String popackid = item.Text.ToString();
                popaklist.Add(popackid);
            }


            if (popaklist.Count > 0 && popaklist != null)
            {

                BLL.MerchandsingBLL.AllocationBLL pkmstrdata = new BLL.MerchandsingBLL.AllocationBLL();

                tbl_podata.DataSource = pkmstrdata.GetPOPACKMasteroLocationforreport(popaklist, atcid);
                //  tbl_podata.DataSource = asqshuffle.GetAllPOPackDataofStyleandPopack(int.Parse(drp_ourstyle.SelectedValue.ToString()), popaklist);
                tbl_podata.DataBind();
                // updgrid2.Update();
                showAsqDataofselectedLocation();
                upd_main.Update();
                upd_smalltable.Update();
                drp_fact.ClearSelection();
             
            }
        }

        protected void drp_fact_SelectionChanged(object sender, Infragistics.Web.UI.ListControls.DropDownSelectionChangedEventArgs e)
        {

        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            LocationSeasonRequest();
        }


        public void LocationSeasonRequest()
        {
            ArrayList SesonList = new ArrayList();
            int atcid = int.Parse(cmb_atc.SelectedValue.ToString());
            List<Infragistics.Web.UI.ListControls.DropDownItem> seasonitems = drp_season.SelectedItems;
            foreach (Infragistics.Web.UI.ListControls.DropDownItem item in seasonitems)
            {

                String popackid = item.Text.ToString();
                SesonList.Add(popackid);
            }

            ArrayList LocArraylist = new ArrayList();

            List<Infragistics.Web.UI.ListControls.DropDownItem> locitems = drp_fact.SelectedItems;
            foreach (Infragistics.Web.UI.ListControls.DropDownItem item in locitems)
            {

                String popackid = item.Value.ToString();
                LocArraylist.Add(popackid);
            }
            
            if (SesonList.Count > 0 && LocArraylist != null && SesonList.Count > 0 && LocArraylist != null)
            {
                string condition = "Where (";

                for (int i = 0; i < SesonList.Count; i++)
                {

                    if (i == 0)
                       
                    {
                        condition = condition + " PoPackMaster.SeasonName  ='" + SesonList[i].ToString().Trim()+"'";
                    }
                    else
                    {
                        condition = condition + "  or PoPackMaster.SeasonName  ='" + SesonList[i].ToString().Trim() + "'";
                    }






                }
                condition = condition + ")";

                condition = condition + "AND (";



                for (int i = 0; i < LocArraylist.Count; i++)
                {
                    if (i == 0)
                    {
                       
                            condition = condition + " PoPackMaster.ExpectedLocation_PK =" + LocArraylist[i].ToString().Trim();
                       


                    }
                    else
                    {
                        condition = condition + "  or PoPackMaster.ExpectedLocation_PK=" + LocArraylist[i].ToString().Trim();
                    }



                }

                condition = condition + ")";

                condition = condition + "and PoPackMaster.AtcId=" + atcid;

                BLL.MerchandsingBLL.AllocationBLL pkmstrdata = new BLL.MerchandsingBLL.AllocationBLL();

                tbl_podata.DataSource = pkmstrdata.GetPOPACKMasteronCondition(condition);
                //  tbl_podata.DataSource = asqshuffle.GetAllPOPackDataofStyleandPopack(int.Parse(drp_ourstyle.SelectedValue.ToString()), popaklist);
                tbl_podata.DataBind();
               
               showAsqDataofselectedLocationandSeason();
                upd_main.Update();
              upd_smalltable.Update();
                drp_fact.ClearSelection();
            }
        }


        public void LocationStyleRequest()
        {
         
            int atcid = int.Parse(cmb_atc.SelectedValue.ToString());


            ArrayList LocArraylist = GetFactoryList();
            ArrayList Stylelist = GetStyleList();

         

            if (Stylelist.Count > 0 && LocArraylist != null && Stylelist.Count > 0 && LocArraylist != null)
            {
                string condition = "Where (";

                for (int i = 0; i < Stylelist.Count; i++)
                {

                    if (i == 0)

                    {
                        condition = condition + " POPackDetails.OurStyleID  =" + Stylelist[i].ToString().Trim() + "";
                    }
                    else
                    {
                        condition = condition + "  or POPackDetails.OurStyleID  =" + Stylelist[i].ToString().Trim() + "";
                    }






                }
                condition = condition + ")";

                condition = condition + "AND (";



                for (int i = 0; i < LocArraylist.Count; i++)
                {
                    if (i == 0)
                    {

                        condition = condition + " PoPackMaster.ExpectedLocation_PK =" + LocArraylist[i].ToString().Trim();



                    }
                    else
                    {
                        condition = condition + "  or PoPackMaster.ExpectedLocation_PK=" + LocArraylist[i].ToString().Trim();
                    }



                }

                condition = condition + ")";

                condition = condition + "and PoPackMaster.AtcId=" + atcid;

                BLL.MerchandsingBLL.AllocationBLL pkmstrdata = new BLL.MerchandsingBLL.AllocationBLL();

                tbl_podata.DataSource = pkmstrdata.GetPOPACKMasteronCondition(condition);
                //  tbl_podata.DataSource = asqshuffle.GetAllPOPackDataofStyleandPopack(int.Parse(drp_ourstyle.SelectedValue.ToString()), popaklist);
                tbl_podata.DataBind();

                showAsqDataofselectedLocationandStyle();
                upd_main.Update();
                upd_smalltable.Update();
                drp_fact.ClearSelection();
            }
        }


        public void showAsqDataofselectedLocationandSeason()
        {
            DataTable dt = (DataTable)(ViewState["asqtable"]);

            ArrayList factlist = GetFactoryList();

            ArrayList seasonlist = getSeasonList();

            if (factlist.Count > 0 && factlist != null)
            {
                string condition = "";
                for (int i = 0; i < factlist.Count; i++)
                {



                    if (i == 0)
                    {
                        condition = condition + " ExpectedLocation_PK=" + factlist[i].ToString().Trim() + "";
                    }
                    else
                    {
                        condition = condition + "  or  ExpectedLocation_PK=" + factlist[i].ToString().Trim() + "";
                    }



                }

                dt = dt.Select(condition).CopyToDataTable();


            }



            if (seasonlist.Count > 0 && seasonlist != null)
            {
                string seasoncondition = "";
                for (int i = 0; i < seasonlist.Count; i++)
                {



                    if (i == 0)
                    {
                        seasoncondition = seasoncondition + " SeasonName='" + seasonlist[i].ToString().Trim() + "'";
                    }
                    else
                    {
                        seasoncondition = seasoncondition + "  or  SeasonName='" + seasonlist[i].ToString().Trim() + "'";
                    }



                }

                DataTable newresult = dt.Select(seasoncondition).CopyToDataTable();

                DataTable sizedata = BLL.FactoryAtcChart.createdatatable(newresult);
                GenerateSmallTable(sizedata);
            }
        }



        public void showAsqDataofselectedLocationandStyle()
        {
            DataTable dt = (DataTable)(ViewState["asqtable"]);

            ArrayList factlist = GetFactoryList();

            ArrayList stylelist = GetStyleList();

            if (factlist.Count > 0 && factlist != null)
            {
                string condition = "";
                for (int i = 0; i < factlist.Count; i++)
                {



                    if (i == 0)
                    {
                        condition = condition + " ExpectedLocation_PK=" + factlist[i].ToString().Trim() + "";
                    }
                    else
                    {
                        condition = condition + "  or  ExpectedLocation_PK=" + factlist[i].ToString().Trim() + "";
                    }



                }

                dt = dt.Select(condition).CopyToDataTable();


            }



            if (stylelist.Count > 0 && stylelist != null)
            {
                string seasoncondition = "";
                for (int i = 0; i < stylelist.Count; i++)
                {



                    if (i == 0)
                    {
                        seasoncondition = seasoncondition + " OurStyleID=" + stylelist[i].ToString().Trim() + "";
                    }
                    else
                    { 
                        seasoncondition = seasoncondition + "  or  OurStyleID=" + stylelist[i].ToString().Trim() + "";
                    }



                }

                DataTable newresult = dt.Select(seasoncondition).CopyToDataTable();

                DataTable sizedata = BLL.FactoryAtcChart.createdatatable(newresult);
                GenerateSmallTable(sizedata);
            }
        }


        protected void Button6_Click(object sender, EventArgs e)
        {
            LocationStyleRequest();
        }

        protected void exportSummary_btn_Click(object sender, EventArgs e)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=summary.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                //To Export all pages






                //table1.RenderControl(hw);

                //style to format numbers to string
                string style = @"<style> .textmode { } </style>";
                Response.Write(style);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }

        }
    }
}
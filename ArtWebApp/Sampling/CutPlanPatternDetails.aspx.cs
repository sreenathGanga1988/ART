using ArtWebApp.DataModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArtWebApp.Sampling
{
    public partial class CutPlanPatternDetails : System.Web.UI.Page
    {
      
         protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!IsPostBack)
                    txt_overallfabreq.Attributes.Add("readonly", "readonly");
                txt_overefficency.Attributes.Add("readonly", "readonly");
                txt_overallConsumption.Attributes.Add("readonly", "readonly");
               
                FillAtcCombo();

            }
            else
            {
                GenerateTable(fillsizedata());
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
                        where ponmbr.OurStyleID == ourstyleid && ponmbr.IsCutorderGiven=="N" && ponmbr.IsApproved=="Y"
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

            ViewState["sizedata"] = null;
            DataTable dt1 = createdatatable(int.Parse(drp_ourstyle.SelectedValue.ToString().ToString()));
            ViewState["sizedata"] = dt1;


            String cutplnqty = BLL.CutOrderBLL.CutPlan.GetCutplanQty(int.Parse(drp_cutorder.SelectedValue.ToString().ToString())).ToString();
            lbl_cutQty.Text = cutplnqty;
       
            //ViewState["cutsizedata"] = null;


            //DataTable dt = BLL.CutOrderBLL.CutPlan.GetCutPlanSizeData(int.Parse(drp_cutorder.SelectedValue.ToString().ToString()));
            //ViewState["cutsizedata"] = dt;


            fillcutplandetails(int.Parse(drp_cutorder.SelectedValue.ToString()));
            fillsmalltable();
            tbl_markertype.DataSource = cutplanmarkertypedata;
            tbl_markertype.DataBind();
            upd_grid.Update();
        }


        public void fillsmalltable()
        {

            ViewState["cutplandata"] = null;
            ViewState["Sizeratiodata"] = null;
            //  DataTable dt = createdatatable(int.Parse(drp_ourstyle.SelectedValue.ToString().ToString()));

            DataTable dt1 = BLL.popackupdater.GetCutPlanmarkerSizedetails(int.Parse(drp_cutorder.SelectedValue.ToString()));
            ViewState["cutplandata"] = dt1;

            DataTable dt = BLL.popackupdater.createdatatableofCutPlan(int.Parse(drp_cutorder.SelectedValue.ToString()));
            ViewState["Sizeratiodata"] = dt;

            DataTable dtheader = dt.Copy();

            ViewState["sizedataheader"] = AddValueToHeader(dtheader);
            GenerateSmallTable();



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
                            tb.Enabled = false;
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
                        tb.Enabled = false;
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

        public DataTable AddValueToHeader(DataTable dt)
        {

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



        public void fillcutplandetails(int cutplanpk)
        {
            using (ArtEntitiesnew entty = new ArtEntitiesnew())
            {
                var q = from ponmbr in entty.CutPlanMasters
                        join ourstyledet in entty.AtcDetails on ponmbr.OurStyleID equals ourstyledet.OurStyleID
                        join atcmstr in entty.AtcMasters on ourstyledet.AtcId equals atcmstr.AtcId join lctnmstr in entty.LocationMasters
                        on ponmbr.Location_PK equals lctnmstr.Location_PK
                        where ponmbr.CutPlan_PK == cutplanpk
                        select new { ponmbr.BOMConsumption, ponmbr.CutPlanNUM, ponmbr.FabDescription, ponmbr.ShrinkageGroup, ponmbr.WidthGroup, ponmbr.MarkerType, atcmstr.AtcNum, ourstyledet.OurStyle ,ponmbr.RefPattern,ponmbr.MarkerMade,ourstyledet.BuyerStyle,ponmbr.Fabrication,lctnmstr.LocationName,ponmbr.Maxmarkerlength};

                foreach (var element in q)
                {
                    lbl_atc.Text = element.AtcNum.ToString();
                    lbl_ourstyle.Text = element.OurStyle.ToString();
                    lbl_Markertype.Text = element.MarkerType.ToString();
                    lbl_shrink.Text = element.ShrinkageGroup.ToString();
                    lbl_with.Text = element.WidthGroup.ToString();
                    lbl_bomconsumption.Text = element.BOMConsumption.ToString();
                    lbl_fabric.Text = element.FabDescription.ToString();
                    lbl_markermade.Text = element.MarkerMade.ToString();
                    lbl_refnum.Text = element.RefPattern.ToString();
                    lbl_buyerstyle.Text = element.BuyerStyle;
                    lbl_location.Text = element.LocationName.ToString();
                    lbl_maxLenth.Text = element.Maxmarkerlength;
                    lbl_fabrication.Text = element.Fabrication;
                    
                }


                Upd_cutplandetails.Update();
            }
        }


        public System.Data.DataTable createdatatable(int ourstyleid)
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("C", typeof(String));

            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {


                var sizedetails = (from size in enty.StyleSizes
                                   where size.OurStyleID == ourstyleid
                                   group size by size.SizeName into sizeGroup
                                   orderby sizeGroup.Min(size => size.Orderof)
                                   select new
                                   {
                                       SizeName = sizeGroup.Key
                                   });
                foreach (var sizedet in sizedetails)
                {
                    dt.Columns.Add(sizedet.SizeName.Trim(), typeof(String));
                }

                dt.Columns.Add("Total", typeof(String));

                DataRow row = dt.NewRow();

                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    row[i] = 0;
                }

                dt.Rows.Add(row);


                DataRow row1 = dt.NewRow();

                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    row[i] = 0;
                }

                dt.Rows.Add(row1);





            }




            return dt;
        }




        public DataTable fillsizedata()
        {
            DataTable dt = new DataTable();
            try
            {


                if (ViewState["sizedata"] == null)
                {

                    dt = createdatatable(int.Parse(drp_ourstyle.SelectedValue.ToString().ToString()));
                    ViewState["sizedata"] = dt;
                }
                else
                {
                    dt = (DataTable)ViewState["sizedata"];
                }


            }
            catch
            {

            }
            return dt;
        }





        private void GenerateTable(DataTable dt)
        {
            BLL.CutOrderBLL.CutDetailsData cddetdataclass = new BLL.CutOrderBLL.CutDetailsData();
            foreach (GridViewRow di in tbl_cutorderdata.Rows)
            {

                int CutOrderDet_PK = int.Parse((di.FindControl("lbl_CutOrderDet_PK") as Label).Text);




                DataTable cutdetdata = new DataTable();
                //cutdetdata = (DataTable)ViewState["cutsizedata"];

                cutdetdata = ArtWebApp.DBTransaction.Productiontransaction.CutPlanTransaction.GetCutplanmarkerSizeQty(CutOrderDet_PK);

                //    cutdetdata = cddetdataclass.GetCutOrderSizeDataofMarker(CutOrderDet_PK);
                //    foreach (System.Data.DataColumn col in dt.Columns) col.ReadOnly = false;
                for (int i = 1; i < dt.Columns.Count; i++)
                {
                    dt.Columns[i].ReadOnly = false;
                    String SIZENAME = dt.Columns[i].ColumnName.ToString();


                    try
                    {
                        object Sumtrim = cutdetdata.Compute("Sum(Qty)", "CutPlanMarkerDetails_PK= " + CutOrderDet_PK + " and  Size ='" + SIZENAME + "'");

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
                        object Sumtrim = cutdetdata.Compute("Sum(Ratio)", "CutPlanMarkerDetails_PK= " + CutOrderDet_PK + " and  Size ='" + SIZENAME + "'");

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
                        tb.Font.Bold = true;
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
                                    tb.Font.Bold = true;
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
                                    tb.Font.Bold = true;
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
                                    tb.Text = dt.Rows[i][j].ToString();
                                    tb.CssClass = "totalQtyRow";

                                    tb.Enabled = false;
                                    //  Add the control to the TableCell
                                    cell.Controls.Add(tb);
                                    tb.Width = 40;
                                    tb.Font.Bold = true;
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
                                    tb.Text = dt.Rows[i][j].ToString();
                                    tb.CssClass = "totalRatioRow";
                                    tb.Enabled = false;
                                    tb.Width = 40;
                                    tb.Font.Bold = true;
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
                                    tb.Attributes.Add("onchange", "sumofQty(this)");
                                    tb.ID = "tb" + i + j;
                                    tb.Text = dt.Rows[i][j].ToString();
                                    tb.Width = 40;
                                    tb.Font.Bold = true;
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
                                    tb.Attributes.Add("onchange", "sumofRatio(this)");
                                    tb.ID = "tb" + i + j;
                                    tb.Text = dt.Rows[i][j].ToString();
                                    tb.Width = 40;
                                    tb.Font.Bold = true;
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
                Table1.Enabled = false;
                Table1.EnableViewState = true;
                upd_table.Update();
                
            }
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



            DataTable dt = new DataTable();

            dt.Columns.Add("Index", typeof(String));
            dt.Columns.Add("Size", typeof(String));





            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = tbl_cutorderdata.Rows[index];
            if (e.CommandName == "Add")
            {



                CheckBox chkBx = (CheckBox)row.FindControl("chk_select");
                if (chkBx.Checked == true)
                {

                    int CutOrderDet_PK = int.Parse((row.FindControl("lbl_CutOrderDet_PK") as Label).Text);
                    BLL.CutOrderBLL.CutDetailsData cddetdata = new BLL.CutOrderBLL.CutDetailsData();
                    //   cddetdata.CutSizeDetailsDataCollection = GetSizedata(CutOrderDet_PK, row);
                    //   cddetdata.InsertCutOrderSizeData();
                }
            }
            else if (e.CommandName == "ShowDropDown")
            {

            }

        }






        protected void tbl_cutorderdata_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //int templatepk = int.Parse((e.Row.FindControl("lbl_templatepk") as Label).Text);

                int rowindex = e.Row.RowIndex;
                //  GenerateTable(fillsizedata(), rowindex);





            }
        }

        protected void tbl_cutorderdata_DataBound(object sender, EventArgs e)
        {
           GenerateTable(fillsizedata());
        }






        protected void tbl_cutorderdata_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

      

        protected void Button1_Click1(object sender, EventArgs e)
        {
           

            //   showGrid();

        }



        public void MessageBoxShow(String mssg)
        {
            lbl_msg.Text = mssg;

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



      

        protected void Button3_Click(object sender, EventArgs e)
        {
            int k = 0;
            BLL.CutOrderBLL.CutPlanMasterData ctmstr = new BLL.CutOrderBLL.CutPlanMasterData();


            ctmstr.CutPlanMarkerDetailsDataCollection = getdata();

            ctmstr.Efficiecny = Decimal.Parse( txt_overefficency.Text);

                ctmstr.cutplanConsumption= Decimal.Parse(txt_overallConsumption.Text);
            ctmstr.CutPlanFabReq = Decimal.Parse(txt_overallfabreq.Text);
            ctmstr.newRefPattern = txt_patternmaenew.Text;
          ctmstr.UpdateMarkerDetails();
            String msg = " Marker Details Added Sucessfully ";

                ArtWebApp.Controls.Messagebox.MessgeboxUpdate(Messaediv, "sucess", msg);
               tbl_cutorderdata.DataSource = null;
              tbl_cutorderdata.DataBind();

            upd_grid.Update();
        }


        public List< BLL.CutOrderBLL. CutPlanMarkerDetailsData> getdata()
        {
            List<BLL.CutOrderBLL.CutPlanMarkerDetailsData> rk = new List<BLL.CutOrderBLL.CutPlanMarkerDetailsData>();

            foreach (GridViewRow row in tbl_cutorderdata.Rows)
            {


             

                int cutplanpk = int.Parse(drp_cutorder.SelectedValue.ToString());
                int CutOrderDet_PK = int.Parse((row.FindControl("lbl_CutOrderDet_PK") as Label).Text);
                BLL.CutOrderBLL.CutPlanMarkerDetailsData cddetdata = new BLL.CutOrderBLL.CutPlanMarkerDetailsData();

                cddetdata.PaternMarkerName = (row.FindControl("txt_newmarkernum") as TextBox).Text;
                cddetdata.MarkerLength = Decimal.Parse((row.FindControl("txt_newMarkerlength") as TextBox).Text);
                cddetdata.Tolerancelength = Decimal.Parse((row.FindControl("txt_newtolerance") as TextBox).Text);
                cddetdata.TotalfabReq = Decimal.Parse((row.FindControl("txt_fabreq") as TextBox).Text);
                cddetdata.efficiency = Decimal.Parse((row.FindControl("txt_eff") as TextBox).Text);
                cddetdata.CutPlan_PK = cutplanpk;
                cddetdata.CutPlanMarkerDetails_PK = CutOrderDet_PK;
                rk.Add(cddetdata);

            }


            return rk;
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
          
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            Session["cutpkrpt"] = int.Parse(drp_cutorder.SelectedValue.ToString());

            Page.ClientScript.RegisterStartupScript(
 this.GetType(), "OpenWindow", "window.open('~/Reports/Production/CutPlanHtmlReport.aspx','_blank');", true);
            // Response.Redirect("~/Reports/Production/CutPlanHtmlReport.aspx");
            //Context.Response.Write("<script> language='javascript'>window.open('','_blank');</script>");
        }
    }
}
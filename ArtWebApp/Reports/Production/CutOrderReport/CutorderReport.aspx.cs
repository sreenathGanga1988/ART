using ArtWebApp.DataModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArtWebApp.Reports.Production.CutOrderReport
{
    public partial class CutorderReport : System.Web.UI.Page
    {
        int ourstyleid = 0;
        int CutPK =0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                String Cut_PK = Request.QueryString["cutpk"];
                Session["Cut_PKreport"] = Cut_PK;
                 CutPK = int.Parse(Cut_PK);
                filldata(CutPK);
            }

            }

        public void filldata(int CutPK)
        {

            DBTransaction.CutOrderTransaction potrsans = new DBTransaction.CutOrderTransaction();

            DataTable dt = potrsans.GetCutOrderMasterData(CutPK);

            if(dt!=null)
            {
                if(dt.Rows.Count>0)
                {




                  
                         









                    

                    lbl_oursrtyle.Text = dt.Rows[0]["OurStyle"].ToString().Trim();
                    lbl_cutorderDate.Text = dt.Rows[0]["CutOrderDate"].ToString().Trim();
                    lbl_color.Text = dt.Rows[0]["Color"].ToString().Trim();
                    lbl_atc.Text = dt.Rows[0]["AtcNum"].ToString().Trim();
                    lbl_cutorder.Text = dt.Rows[0]["Cut_NO"].ToString().Trim();
                    lbl_fabricQty.Text = dt.Rows[0]["FabQty"].ToString().Trim();
                    lbl_cutqty.Text = dt.Rows[0]["CutQty"].ToString().Trim();
                    lbl_cutordertype.Text = dt.Rows[0]["CutOrderType"].ToString().Trim();
                    lbl_shrinkage.Text = dt.Rows[0]["Shrinkage"].ToString().Trim();
                    lbl_cutwidth.Text = dt.Rows[0]["CutWidth"].ToString().Trim();
                      ourstyleid= int.Parse ( dt.Rows[0]["OurStyleID"].ToString().Trim());
                    fillCutorderData(ourstyleid, CutPK);
                    lbl_addedBy.Text = dt.Rows[0]["AddedBy"].ToString().Trim();
                    lbl_Addeddate.Text = dt.Rows[0]["CutOrderDate"].ToString().Trim();
                    lbl_markertype.Text = dt.Rows[0]["MarkerType"].ToString().Trim();
                    lbl_markername.Text = dt.Rows[0]["PaternName"].ToString().Trim();
                    lbl_reason.Text = dt.Rows[0]["ExtraReason"].ToString().Trim();
                    lbl_location.Text = dt.Rows[0]["LocationName"].ToString().Trim();
                   

                }
            }



        }



        public void fillCutorderData(int ourstyleid ,int cutpk)
        {
            ViewState["sizedata"] = null;
            DataTable dt1 = createdatatable(ourstyleid);
            ViewState["sizedata"] = dt1;


            ViewState["cutsizedata"] = null;

            BLL.CutOrderBLL.CutDetailsData cddetdata = new BLL.CutOrderBLL.CutDetailsData();
            DataTable dt = cddetdata.GetCutOrderSizeData(cutpk);
            ViewState["cutsizedata"] = dt;



            upd_grid.Update();

        }


        public System.Data.DataTable createdatatable(int ourstyleid)
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("C", typeof(String));

            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {


                var sizedetails = (from size in enty.StyleSizes
                                   where size.OurStyleID == ourstyleid
                                   select new
                                   {
                                       size.SizeName
                                   }).Distinct();

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

                    dt = createdatatable(ourstyleid);
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

                cutdetdata = cddetdataclass.GetCutOrderSizeDataofMarker(CutOrderDet_PK);
                //    foreach (System.Data.DataColumn col in dt.Columns) col.ReadOnly = false;
                for (int i = 1; i < dt.Columns.Count; i++)
                {
                    dt.Columns[i].ReadOnly = false;
                    String SIZENAME = dt.Columns[i].ColumnName.ToString();


                    try
                    {
                        object Sumtrim = cutdetdata.Compute("Sum(Qty)", "CutOrderDet_PK= " + CutOrderDet_PK + " and  Size ='" + SIZENAME + "'");

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
                        object Sumtrim = cutdetdata.Compute("Sum(Ratio)", "CutOrderDet_PK= " + CutOrderDet_PK + " and  Size ='" + SIZENAME + "'");

                        dt.Rows[1][i] = Sumtrim.ToString();
                    }
                    catch (Exception ex)
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
                                        tb.Attributes.Add("onchange", "sumofQty(this)");
                                        tb.ID = "tb" + i + j;
                                        tb.Text = dt.Rows[i][j].ToString();
                                        tb.Width = 40;
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
              //  upd_table.Update();
            }
        }

        protected void tbl_cutorderdata_DataBound(object sender, EventArgs e)
        {
            GenerateTable(fillsizedata());
        }
    }
}
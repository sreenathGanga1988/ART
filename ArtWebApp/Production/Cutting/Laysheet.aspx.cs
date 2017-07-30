using ArtWebApp.DataModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArtWebApp.Production.Cutting
{
    public partial class Laysheet : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillAtcCombo();

            }
            else
            {
                fillsizedata();
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

                var q1 = from order in entty.LocationMasters
                         where order.LocType == "F"
                         select new
                         {
                             name = order.LocationName,
                             pk = order.Location_PK
                         };
                drp_fact.DataSource = q1.ToList();
                drp_fact.DataBind();

                UPD_FACT.Update();
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

            string msg = "";
            String num = "";
            BLL.ProductionBLL.LaysheetMasterData lblmstr = new BLL.ProductionBLL.LaysheetMasterData();
            lblmstr.AddedDate = DateTime.Now;
            lblmstr.atcid = int.Parse(drp_atc.SelectedValue.ToString());
            lblmstr.ourstyleid = int.Parse(drp_ourstyle.SelectedValue.ToString());
            lblmstr.CutOrderDet_PK = int.Parse(drp_markernum.SelectedValue.ToString());
            lblmstr.Location_PK= int.Parse(drp_fact.SelectedValue.ToString());
            lblmstr.cutnum = "NA";
            lblmstr.LayRollRef = drp_cutRoll.SelectedItem.Text;
            lblmstr.LaysheetRollmaster_Pk = int.Parse(drp_cutRoll.SelectedValue.ToString());
            lblmstr.LaysheetDetaolsDataCollection = LSDetailsData();
          
            num = lblmstr.InsertLaySheet();

            msg = "Laysheet # : " + num + " is generated Successfully";
            tbl_RollDetails.DataSource = null;
            tbl_RollDetails.DataBind();
            ArtWebApp.Controls.Messagebox.MessgeboxUpdate(Messaediv, "sucess", msg);
        }








        public List<BLL.ProductionBLL.LaysheetDetaolsData> LSDetailsData()
        {

            List<BLL.ProductionBLL.LaysheetDetaolsData> rk = new List<BLL.ProductionBLL.LaysheetDetaolsData>();


            foreach (GridViewRow di in tbl_RollDetails.Rows)
            {
                CheckBox chkBx = (CheckBox)di.FindControl("chk_select");

                if (chkBx != null && chkBx.Checked)
                {


                    int lbl_rollpk = int.Parse(((di.FindControl("lbl_rollpk") as Label).Text.ToString()));

                    int lbl_LaySheetRoll_Pk = int.Parse(((di.FindControl("lbl_LaySheetRoll_Pk") as Label).Text.ToString()));

                    int txt_plies = int.Parse(((di.FindControl("txt_plies") as TextBox).Text.ToString()));
                    Decimal txt_fab = Decimal.Parse(((di.FindControl("txt_fab") as TextBox).Text.ToString()));
                    decimal txt_balance = decimal.Parse(((di.FindControl("txt_txtBalance") as TextBox).Text.ToString()));
                    decimal txt_excessshortage = decimal.Parse(((di.FindControl("txt_excessshort") as TextBox).Text.ToString()));


                    CheckBox chk_cutable = (di.FindControl("chk_cutable") as CheckBox);
                    

                    BLL.ProductionBLL.LaysheetDetaolsData lsdetdata = new BLL.ProductionBLL.LaysheetDetaolsData();


                    lsdetdata.Roll_PK = lbl_rollpk;
                    lsdetdata.NoOfPlies = txt_plies;
                    lsdetdata.fabqty = txt_fab;
                    lsdetdata.Balance = txt_balance;
                    lsdetdata.LaySheetRoll_Pk = lbl_LaySheetRoll_Pk;
                    lsdetdata.ExceSShortage = txt_excessshortage;
                    if (chk_cutable.Checked == true)
                    {
                        lsdetdata.IsRecuttable = "Y";
                    }
                    else
                    {
                        lsdetdata.IsRecuttable = "N";
                    }
                    rk.Add(lsdetdata);
                }
            }
            return rk;


        }

        protected void btn_atc_Click(object sender, EventArgs e)
        {
            FillOurStyleCombo(int.Parse(drp_atc.SelectedValue.ToString()));
        }

        public void FillAllcutorder(int ourstyleid,int skudet_pk)
        {


            using (ArtEntitiesnew entty = new ArtEntitiesnew())
            {
                var q = from ponmbr in entty.CutOrderMasters
                        where ponmbr.OurStyleID == ourstyleid && ponmbr.SkuDet_pk== skudet_pk && ponmbr.IsDeleted=="N"
                        select new
                        {
                            name = ponmbr.Cut_NO,

                            //  name=ponmbr.CostingCount,
                            pk = ponmbr.CutID
                        };


                drp_cutorder.DataSource = q.ToList();
                drp_cutorder.DataBind();
                upd_cutorder.Update();



            }

        }

        public void FillMarkernum(int cutid)
        {


            using (ArtEntitiesnew entty = new ArtEntitiesnew())
            {
                var q = from ponmbr in entty.CutOrderDetails
                        where ponmbr.CutID == cutid
                        select new
                        {
                            name = ponmbr.MarkerNo,

                            //  name=ponmbr.CostingCount,
                            pk = ponmbr.CutOrderDet_PK
                        };


                drp_markernum.DataSource = q.ToList();
                drp_markernum.DataBind();
                upd_markernum.Update();



            }

        }
        public void fillColorcombo(int ourstyleid)
        {


            drp_fabcolor.DataSource = BLL.CutOrderBLL.CutPlan.fillFabColor(int.Parse(drp_ourstyle.SelectedValue.ToString()), "");
            drp_fabcolor.DataBind();
            upd_fabcolor.Update();


        }

        protected void btn_OURSTYLE_Click(object sender, EventArgs e)
        {
            //ViewState["sizedata"] = null;
            //DataTable   dt = createdatatable(int.Parse(drp_ourstyle.SelectedValue.ToString ().ToString()));
            //ViewState["sizedata"] = dt;
            //GenerateTable(dt);
            fillsizedata();
           
            fillColorcombo(int.Parse(drp_ourstyle.SelectedValue.ToString().ToString()));
        }
        protected void btn_cutorder_Click(object sender, EventArgs e)
        {
            FillMarkernum(int.Parse(drp_cutorder.SelectedValue.ToString().ToString()));
            GenerateCutorderTable(fillsizedata());
        }

        public System.Data.DataTable createdatatable(int ourstyleid)
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("", typeof(String));

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

                DataRow AlreadyCutrow = dt.NewRow();

                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    row[i] = 0;
                }

                dt.Rows.Add(AlreadyCutrow);
                DataRow BalanceCutrow = dt.NewRow();

                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    row[i] = 0;
                }

                dt.Rows.Add(BalanceCutrow);



            }




            return dt;
        }




        public void Filllaysheetroll(int cutid)
        {
            int UserLoc_pk = int.Parse(drp_fact.SelectedValue.ToString());

            
            if (Session["UserProfileName"].ToString().Trim ()!="Admin")
            {
                using (ArtEntitiesnew entty = new ArtEntitiesnew())
                {

                    var sizedetails = (from ponmbr in entty.LaySheetRollMasters
                                       where ponmbr.CutID == cutid && ponmbr.Location_Pk == UserLoc_pk
                                       select new
                                   {
                                       ponmbr.LaysheetRollmaster_Pk,
                                       ponmbr.LayRollRef
                                   }).Distinct();

                    drp_cutRoll.DataSource = sizedetails.ToList();
                    drp_cutRoll.DataBind();
                    upd_layroll.Update();



                }
            }
            else
            {
                using (ArtEntitiesnew entty = new ArtEntitiesnew())
                {

                    var sizedetails = (from ponmbr in entty.LaySheetRollMasters
                                       where ponmbr.CutID == cutid
                                       select new
                                   {
                                       ponmbr.LaysheetRollmaster_Pk,
                                       ponmbr.LayRollRef
                                   }).Distinct();

                    drp_cutRoll.DataSource = sizedetails.ToList();
                    drp_cutRoll.DataBind();
                    upd_layroll.Update();



                }

            }
           
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








        private void GenerateTable(DataTable dt, float plies)
        {
            BLL.CutOrderBLL.CutDetailsData cddetdataclass = new BLL.CutOrderBLL.CutDetailsData();
            int cutorderdetpk = int.Parse(drp_markernum.SelectedValue.ToString());
            DataTable cutdetdata = new DataTable();
            cutdetdata = cddetdataclass.GetCutOrderSizeDataofMarker(cutorderdetpk);
            for (int i = 1; i < dt.Columns.Count; i++)
            {
                dt.Columns[i].ReadOnly = false;
                String SIZENAME = dt.Columns[i].ColumnName.ToString();


                try
                {
                    object Sumtrim = cutdetdata.Compute("Sum(Qty)", "CutOrderDet_PK= " + cutorderdetpk + " and  Size ='" + SIZENAME + "'");

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
                    object Sumtrim = cutdetdata.Compute("Sum(Ratio)", "CutOrderDet_PK= " + cutorderdetpk + " and  Size ='" + SIZENAME + "'");



                    if (Sumtrim.ToString().Trim() == "")
                    {
                        Sumtrim = "0";
                    }

                    dt.Rows[1][i] = Sumtrim.ToString();


                    dt.Rows[2][i] = (float.Parse(Sumtrim.ToString()) * plies).ToString();

                    dt.Rows[3][i] = (float.Parse(dt.Rows[0][i].ToString()) - float.Parse(dt.Rows[2][i].ToString())).ToString();
                }
                catch (Exception ex)
                {

                    dt.Rows[1][i] = "0";
                }

            }





            //  dt = BLL.ProductionBLL.LaySheetfunction.CalculateAlreadyCut(dt);



            dt = ArtWebApp.Controls.DataTableFunction.SumOfDataColumns(1, dt.Columns.Count - 2, 0, dt.Rows.Count - 1, dt.Columns.Count - 1, dt.Rows.Count - 1, dt);



            DataRow row123 = dt.NewRow();

            for (int i = 0; i < dt.Columns.Count; i++)
            {
                row123[i] = dt.Rows[dt.Rows.Count-1][i].ToString ();
            }

            dt.Rows.Add(row123);

            //Creat the Table and Add it to the Page
            Table1.Rows.Clear();




            Table1.ID = "Table1";
            //Page.Form.Controls.Add(table);
            panel1.Controls.Add(Table1);
            Table1.CssClass = "dynamicentrytable";
            //The number of Columns to be generated
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
                                tb.CssClass = "txtqtycap";

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
                                tb.CssClass = "txtratiocap";
                                tb.Enabled = false;
                                tb.Width = 40;
                                //      Add the control to the TableCell
                                cell.Controls.Add(tb);
                                //   Add the TableCell to the TableRow
                                //  cell.CssClass = "Widthclass";
                                row.Cells.Add(cell);

                            }
                            else if (i == 2)
                            {
                                TableCell cell = new TableCell();
                                cell.Width = 40;

                                TextBox tb = new TextBox();


                                //  Set a unique ID for each TextBox added

                                tb.ID = "tb" + i + j;
                                tb.Text = "Cut Qty";
                                tb.CssClass = "txt_cutQTYcap";
                                tb.Enabled = false;
                                tb.Width = 40;
                                //      Add the control to the TableCell
                                cell.Controls.Add(tb);
                                //   Add the TableCell to the TableRow
                                //  cell.CssClass = "Widthclass";
                                row.Cells.Add(cell);
                            }
                            else if (i == 3)
                            {
                                TableCell cell = new TableCell();
                                cell.Width = 40;

                                TextBox tb = new TextBox();


                                //  Set a unique ID for each TextBox added

                                tb.ID = "tb" + i + j;
                                tb.Text = "BAL Qty";
                                tb.CssClass = "txt_balQtycap";
                                tb.Enabled = false;
                                tb.Width = 40;
                                //      Add the control to the TableCell
                                cell.Controls.Add(tb);
                                //   Add the TableCell to the TableRow
                                //  cell.CssClass = "Widthclass";
                                row.Cells.Add(cell);
                            }
                            else if (i == 4)
                            {
                                TableCell cell = new TableCell();
                                cell.Width = 40;

                                TextBox tb = new TextBox();


                                //  Set a unique ID for each TextBox added

                                tb.ID = "tb" + i + j;
                                tb.Text = "BAL Qty";
                                tb.CssClass = "txt_newbalQtycab";
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

                            else if (i == 2)
                            {
                                TableCell cell = new TableCell();
                                cell.Width = 40;

                                TextBox tb = new TextBox();


                                //   Set a unique ID for each TextBox added

                                tb.ID = "tb" + i + j;
                                tb.Text = dt.Rows[i][j].ToString();
                                tb.CssClass = "totalcutRow";
                                tb.Enabled = false;
                                tb.Width = 40;

                                //   Add the control to the TableCell
                                cell.Controls.Add(tb);
                                //  Add the TableCell to the TableRow
                                //  cell.CssClass = "Widthclass";
                                row.Cells.Add(cell);
                            }
                            else if (i == 3)
                            {
                                TableCell cell = new TableCell();
                                cell.Width = 40;

                                TextBox tb = new TextBox();


                                //   Set a unique ID for each TextBox added

                                tb.ID = "tb" + i + j;
                                tb.Text = dt.Rows[i][j].ToString();
                                tb.CssClass = "totalbalRow";
                                tb.Enabled = false;
                                tb.Width = 40;

                                //   Add the control to the TableCell
                                cell.Controls.Add(tb);
                                //  Add the TableCell to the TableRow
                                //  cell.CssClass = "Widthclass";
                                row.Cells.Add(cell);
                            }
                            else if (i == 4)
                            {
                                TableCell cell = new TableCell();
                                cell.Width = 40;

                                TextBox tb = new TextBox();


                                //  Set a unique ID for each TextBox added

                                tb.ID = "tb" + i + j;
                                tb.Text = dt.Rows[i][j].ToString();
                                tb.CssClass = "txt_totalnewbalQty";
                                tb.Enabled = false;
                                tb.Width = 40;
                                //      Add the control to the TableCell
                                cell.Controls.Add(tb);
                                //   Add the TableCell to the TableRow
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
                            else if (i == 2)
                            {
                                TableCell cell = new TableCell();
                                cell.Width = 40;

                                TextBox tb = new TextBox();

                                tb.CssClass = "txtCalcut";
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
                            else if (i == 3)
                            {
                                TableCell cell = new TableCell();
                                cell.Width = 40;

                                TextBox tb = new TextBox();

                                tb.CssClass = "txtCalbal";
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

                            else if (i == 4)
                            {
                                TableCell cell = new TableCell();
                                cell.Width = 40;

                                TextBox tb = new TextBox();

                                tb.CssClass = "txtCalNewBal";
                                //  Set a unique ID for each TextBox added
                                //tb.Attributes.Add("onkeypress", "return isNumberKey(event,this)");
                                //tb.Attributes.Add("onchange", "sumofRatio(this)");
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
            upd_table.Update();
        }



        private void GenerateCutorderTable(DataTable dt)
        {
            BLL.CutOrderBLL.CutDetailsData cddetdataclass = new BLL.CutOrderBLL.CutDetailsData();
            int cutorderpk = int.Parse(drp_cutorder.SelectedValue.ToString());
            DataTable cutdetdata = new DataTable();
            cutdetdata = cddetdataclass.GetCutOrderMasterSizeDataofCutorder(cutorderpk);
            for (int i = 1; i < dt.Columns.Count; i++)
            {
                dt.Columns[i].ReadOnly = false;
                String SIZENAME = dt.Columns[i].ColumnName.ToString();


                try
                {
                    object Sumtrim = cutdetdata.Compute("Sum(Qty)", " Size ='" + SIZENAME + "'");

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
                    object Sumtrim = cutdetdata.Compute("Sum(Ratio)", " Size ='" + SIZENAME + "'");



                    if (Sumtrim.ToString().Trim() == "")
                    {
                        Sumtrim = "0";
                    }

                    dt.Rows[1][i] = Sumtrim.ToString();




                }
                catch (Exception ex)
                {

                    dt.Rows[1][i] = "0";
                }
                try
                {
                    object Sumtrim = cutdetdata.Compute("Sum(CutQty)", " Size ='" + SIZENAME + "'");



                    if (Sumtrim.ToString().Trim() == "")
                    {
                        Sumtrim = "0";
                    }

                    dt.Rows[2][i] = Sumtrim.ToString();



                }
                catch (Exception ex)
                {

                    dt.Rows[1][i] = "0";
                }



                dt.Rows[3][i] = (float.Parse(dt.Rows[0][i].ToString()) - float.Parse(dt.Rows[2][i].ToString())).ToString();



            }


            //  dt = BLL.ProductionBLL.LaySheetfunction.CalculateAlreadyCut(dt);



            dt = ArtWebApp.Controls.DataTableFunction.SumOfDataColumns(1, dt.Columns.Count - 2, 0, dt.Rows.Count - 1, dt.Columns.Count - 1, dt.Rows.Count - 1, dt);

            //Creat the Table and Add it to the Page
            Table2.Rows.Clear();




            Table2.ID = "Table2";
            //Page.Form.Controls.Add(table);
            panel2.Controls.Add(Table2);
            Table2.CssClass = "dynamicentrytable";
            //The number of Columns to be generated
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
                Table2.Rows.Add(hrow);



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
                                tb.Text = " Tot Qty";
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
                            else if (i == 2)
                            {
                                TableCell cell = new TableCell();
                                cell.Width = 40;

                                TextBox tb = new TextBox();


                                //  Set a unique ID for each TextBox added

                                tb.ID = "tb" + i + j;
                                tb.Text = "Cut Qty";
                                tb.CssClass = "txt_cutQTY";
                                tb.Enabled = false;
                                tb.Width = 40;
                                //      Add the control to the TableCell
                                cell.Controls.Add(tb);
                                //   Add the TableCell to the TableRow
                                //  cell.CssClass = "Widthclass";
                                row.Cells.Add(cell);
                            }
                            else if (i == 3)
                            {
                                TableCell cell = new TableCell();
                                cell.Width = 40;

                                TextBox tb = new TextBox();


                                //  Set a unique ID for each TextBox added

                                tb.ID = "tb" + i + j;
                                tb.Text = "BAL Qty";
                                tb.CssClass = "txt_balQty";
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

                            else if (i == 2)
                            {
                                TableCell cell = new TableCell();
                                cell.Width = 40;

                                TextBox tb = new TextBox();


                                //   Set a unique ID for each TextBox added

                                tb.ID = "tb" + i + j;
                                tb.Text = dt.Rows[i][j].ToString();
                                tb.CssClass = "totalcutRow";
                                tb.Enabled = false;
                                tb.Width = 40;

                                //   Add the control to the TableCell
                                cell.Controls.Add(tb);
                                //  Add the TableCell to the TableRow
                                //  cell.CssClass = "Widthclass";
                                row.Cells.Add(cell);
                            }
                            else if (i == 3)
                            {
                                TableCell cell = new TableCell();
                                cell.Width = 40;

                                TextBox tb = new TextBox();


                                //   Set a unique ID for each TextBox added

                                tb.ID = "tb" + i + j;
                                tb.Text = dt.Rows[i][j].ToString();
                                tb.CssClass = "balRow";
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
                            else if (i == 2)
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
                            else if (i == 3)
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
                    Table2.Rows.Add(row);
                }

            }
            // Now iterate through the table and add your controls

            Table2.EnableViewState = true;
            upd_secndtable.Update();
        }






        protected void btn_marker_Click(object sender, EventArgs e)
        {

            BLL.ProductionBLL.LaysheetBLL lblldata = new BLL.ProductionBLL.LaysheetBLL();


            float plies = 0;

            try
            {
                plies = lblldata.GetCutPlies(int.Parse(drp_markernum.SelectedValue.ToString()));


                txt_pliescut.Text = plies.ToString();
                upd_pliescut.Update();


                ArrayList ary = lblldata.getcutplanMarkerdata(int.Parse(drp_markernum.SelectedValue.ToString()));
                txt_cutperplies.Text = ary[0].ToString();
                txt_noofplies.Text = ary[1].ToString();
                upd_cutperplies.Update();
                upd_noofplies.Update();

                float balancecut = float.Parse(txt_noofplies.Text) - float.Parse(txt_pliescut.Text);

                txt_baltocutnow.Text = balancecut.ToString();
                upd_baltocutnow.Update();


            }
            catch (Exception exp)
            {
                txt_pliescut.Text = plies.ToString();
                upd_pliescut.Update();

            };

          
            //GenerateTable(fillsizedata());
            showlaylength(int.Parse(drp_markernum.SelectedValue.ToString()));
            Upd_markerLaylength.Update();
            upd_Laylength.Update();
            Filllaysheetroll(int.Parse(drp_cutorder.SelectedValue.ToString()));
        }




        public void showlaylength(int cutorderdet_pk)
        {
            using (ArtEntitiesnew entty = new ArtEntitiesnew())

            {
                var q = from cddet in entty.CutOrderDetails
                        join cdplnmrkdet in entty.CutPlanMarkerDetails on cddet.CutPlanMarkerDetails_PK equals cdplnmrkdet.CutPlanMarkerDetails_PK
                        where cddet.CutOrderDet_PK== cutorderdet_pk
                        select new { cdplnmrkdet.Tolerancelength, cdplnmrkdet.MarkerLength };

                foreach(var element in q)
                {
                    txt_markerLaylength.Text = element.MarkerLength.ToString ();
                    txt_Laylength.Text = (Decimal.Parse(element.Tolerancelength.ToString()) * Decimal.Parse("0.0278") + Decimal.Parse(element.MarkerLength.ToString())).ToString();
                }
            }
            }

        protected void btn_color_Click(object sender, EventArgs e)
        {
            FillAllcutorder(int.Parse(drp_ourstyle.SelectedValue.ToString().ToString()), int.Parse(drp_fabcolor.SelectedValue.ToString().ToString()));
        }

        protected void btn_showroll_Click(object sender, EventArgs e)
        {

            int plies =int.Parse ( txt_pliescut.Text);
            BLL.ProductionBLL.LaysheetBLL lblldata = new BLL.ProductionBLL.LaysheetBLL();
            DataTable dt = lblldata.getRollSelectedAgainstALaysheetroll(drp_cutRoll.SelectedItem.Text);
            tbl_RollDetails.DataSource = dt;
            tbl_RollDetails.DataBind();
            upd_grid.Update();
            GenerateTable(fillsizedata(), plies);
            GenerateCutorderTable(fillsizedata());
            Table1.Enabled = false;
        }

        protected void txt_noofplies0_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
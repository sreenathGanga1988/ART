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
    public partial class CutOrderFinal : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillAtcCombo();

            }
            else
            {
                GenerateTable(fillsizedata());
            }
        }

        public void fillReason()
        {
            using (ArtEntitiesnew entty = new ArtEntitiesnew())
            {
                var q = from ponmbr in entty.ExtraRequestReasonMasters

                        select new
                        {
                            name = ponmbr.ExtraReason,
                            pk = ponmbr.ExtraReason_Pk
                        };

                drp_reason.DataSource = q.ToList();
                drp_reason.DataBind();
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
                        where ponmbr.OurStyleID == ourstyleid && ponmbr.IsPatternAdded=="Y" && ponmbr.IsDeleted=="N" && ponmbr.IsCutorderGiven=="N"
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


            String cutplnqty = BLL.CutOrderBLL.CutPlan.GetCutplanQty(int.Parse(drp_cutorder.SelectedValue.ToString().ToString())).ToString ();
            lbl_cutQty.Text = cutplnqty;
            upd_cutQty.Update();
            String str = drp_cutorder.SelectedItem.Text.ToString();
            str = str.Replace("CPL#", "");
            txt_cutno.Text = str;

            upd_cutno.Update();
            //ViewState["cutsizedata"] = null;


            //DataTable dt = BLL.CutOrderBLL.CutPlan.GetCutPlanSizeData(int.Parse(drp_cutorder.SelectedValue.ToString().ToString()));
            //ViewState["cutsizedata"] = dt;


            fillcutplandetails(int.Parse(drp_cutorder.SelectedValue.ToString()));
            upd_grid.Update();
        }









        public void fillcutplandetails( int cutplanpk)
        {
            using (ArtEntitiesnew entty = new ArtEntitiesnew())
            {
                var q = from ponmbr in entty.CutPlanMasters
                        join ourstyledet in entty.AtcDetails on ponmbr.OurStyleID equals ourstyledet.OurStyleID
                        join atcmstr in entty.AtcMasters on ourstyledet.AtcId equals atcmstr.AtcId
                        where ponmbr.CutPlan_PK == cutplanpk
                        select new {ponmbr.BOMConsumption, ponmbr.CutPlanNUM , ponmbr.FabDescription,ponmbr.ShrinkageGroup,ponmbr.WidthGroup,ponmbr.MarkerType,atcmstr.AtcNum ,ourstyledet.OurStyle , ponmbr.CutplanConsumption,ponmbr.CutPlanFabReq,ponmbr.RollYard,ponmbr.NewPatternName};

               foreach(var element in q)
                {
                    lbl_atc.Text = element.AtcNum.ToString();
                    lbl_ourstyle.Text = element.OurStyle.ToString();
                    lbl_Markertype.Text = element.MarkerType.ToString();
                    lbl_shrink.Text = element.ShrinkageGroup.ToString();
                    lbl_with.Text = element.WidthGroup.ToString();
                    lbl_bomconsumption.Text = element.BOMConsumption.ToString();
                    lbl_fabric.Text = element.FabDescription.ToString();
                    lbl_coconsumption.Text = element.CutplanConsumption.ToString();
                    lbl_fabreq.Text = element.CutPlanFabReq.ToString();

                    int fabreq = decimal.ToInt32(decimal.Parse(element.CutPlanFabReq.ToString()));
                    txt_markername.Text = element.NewPatternName.ToString().Trim();
                    txt_fabAllocation.Text = fabreq.ToString();
                    lbl_rollyard.Text = element.RollYard.ToString();
                }

                upd_fabAllocation.Update();
                Upd_cutplandetails.Update();
                upd_markername.Update();

                lbl_newConsumption.Text = (float.Parse(txt_fabAllocation.Text) / float.Parse(lbl_cutQty.Text)).ToString();

                upd_consumption.Update();
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
        }

        protected void tbl_cutorderdata_DataBound(object sender, EventArgs e)
        {
            GenerateTable(fillsizedata());
        }




      

        protected void tbl_cutorderdata_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void drp_cutorderType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (drp_cutorderType.SelectedItem.Text == "Extra")
                {
                    fillReason();
                }
                else
                {


                    drp_reason.Items.Clear();
                    drp_reason.DataSource = null;

                    drp_reason.DataBind();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        protected void Button1_Click1(object sender, EventArgs e)
        {
            SaveCutOrder();

         //   showGrid();
            
        }



        public void MessageBoxShow(String mssg)
        {
            lbl_msg.Text = mssg;

        }
        public void SaveCutOrder()
        {
            if (validationcontrol())
            {
                BLL.CutOrderBLL.FinalCutOrderEntry cdata = new BLL.CutOrderBLL.FinalCutOrderEntry();

              
                    cdata.Atcid = int.Parse(drp_atc.SelectedValue.ToString());
                    cdata.Ourstyleid = int.Parse(drp_ourstyle.SelectedValue.ToString());
                    //cdata.FabDescription = ddl_color.SelectedItem.Text.Trim();
                    //cdata.Skudet_pk = int.Parse(ddl_color.SelectedValue.ToString());
                    cdata.CutNum = txt_cutno.Text.ToString();
                    //cdata.Tofactid = int.Parse(drp_fact.SelectedValue.ToString());
                    cdata.CutorderType = drp_cutorderType.SelectedItem.ToString().Trim();
                    cdata.CofabAllocation = int.Parse(txt_fabAllocation.Text);
                    
                    //cdata.Cutablewidth = drp_width.Text.Trim();
                    //cdata.Shrinkage = drp_shrink.Text.Trim();
                    cdata.patername = txt_markername.Text.Trim();
                    cdata.cutplanpk = int.Parse(drp_cutorder.SelectedValue.ToString());
                    if (drp_cutorderType.SelectedItem.Text == "Extra")
                    {
                        cdata.ExtraReason_Pk = int.Parse(drp_reason.SelectedValue.ToString());
                    }
                    else
                    {
                        cdata.ExtraReason_Pk = 6;


                    }
                    cdata.CutOrderQty = Decimal.Parse ( lbl_cutQty.Text);
                   cdata.ApprovedConsumption = Decimal.Parse(lbl_newConsumption.Text); ;
                    if (!cdata.IsCutOrdernumPresent(txt_cutno.Text.ToString()))
                    {
                        String cutno = lbl_msg.Text = cdata.InsertNewCutOrder();
                    String msg = "Cutorder # : " + cutno + " is Generated Sucessfully";
                    MessgeboxUpdate("sucess", msg);
                    }
                    else
                    {
                    String cutno = lbl_msg.Text = cdata.UpdateCutOrder();
                    String msg = "Cutorder # : " + cutno + " is Updated  Sucessfully";
                    MessgeboxUpdate("sucess", msg);
                }

               
            }
        }


        /// <summary>
        /// valiadtes the form before saving athe data
        /// </summary>
        /// <returns></returns
        public Boolean validationcontrol()
        {
            
            Boolean sucess = false;
            if (drp_atc.SelectedValue.ToString().Trim() == null || drp_atc.SelectedValue.ToString().Trim() == "")
            {
                MessageBoxShow("Enter the Atc#");
            }

            else if (txt_cutno.Text == null || txt_cutno.Text.Trim() == "")
            {
                MessageBoxShow("Enter the Cut Plan #");
            }
            else if (drp_cutorderType.SelectedItem.Text == null || drp_cutorderType.SelectedItem.Text.Trim() == "")
            {
                MessageBoxShow("Enter the Cut Plan Type ");
            }

            else if (txt_fabAllocation.Text == null || txt_fabAllocation.Text.Trim() == "")
            {
                MessageBoxShow("Enter the Fabric Quantity ");
            }

            else
            {
                sucess = true;
            }
            return sucess;
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



        public List<BLL.CutOrderBLL.CutSizeDetailsData> GetSizedata()
        {



            List<BLL.CutOrderBLL.CutSizeDetailsData> rk = new List<BLL.CutOrderBLL.CutSizeDetailsData>();

            foreach (GridViewRow row in tbl_cutorderdata.Rows)
            {





                CheckBox chkBx = (CheckBox)row.FindControl("chk_select");
                //if (chkBx.Checked == true)
                //{

                int CutOrderDet_PK = int.Parse((row.FindControl("lbl_CutOrderDet_PK") as Label).Text);
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

                                        BLL.CutOrderBLL.CutSizeDetailsData cutdet = new BLL.CutOrderBLL.CutSizeDetailsData();
                                        cutdet.Sizename = lblsize.Text.Trim();
                                        cutdet.Qty = Decimal.Parse(txtqty.Text);
                                        cutdet.CutOrderDet_PK = CutOrderDet_PK;
                                        cutdet.Ratio = Decimal.Parse(txtratio.Text);



                                        rk.Add(cutdet);
                                    }



                                }


                            }
                        }
                    }

                    //}
                }
            }
                    return rk;
        }

        protected void lbl_newConsumption_TextChanged(object sender, EventArgs e)
        {

        }

        protected void txt_fabAllocation_TextChanged(object sender, EventArgs e)
        {
            lbl_newConsumption.Text = (float.Parse(txt_fabAllocation.Text) / float.Parse(lbl_cutQty.Text)).ToString();

            upd_consumption.Update();
        }
    }
}
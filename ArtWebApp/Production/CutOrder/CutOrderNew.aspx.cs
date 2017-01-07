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
    public partial class CutOrderNew : System.Web.UI.Page
    {
        BLL.CutOrderBLL.CutOrderData cdata = null;
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

                var q1 = from order in entty.LocationMasters
                         where order.LocType == "F"
                         select new
                         {
                             name = order.LocationName,
                             pk = order.Location_PK
                         };
                drp_Atc.DataSource = q.ToList();
                drp_Atc.DataBind();

                drp_fact.DataSource = q1.ToList();
                drp_fact.DataBind();
                UpdatePanel2.Update();


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
        public void fillColorcombo()
        {

            cdata = new BLL.CutOrderBLL.CutOrderData();
            ddl_color.DataSource = cdata.GetFabricDescription(int.Parse(drp_Atc.SelectedValue.ToString()));
            ddl_color.DataBind();

        }




        public void clearcontroll()
        {
            drp_Atc.SelectedIndex = -1;
            drp_fact.SelectedIndex = 0;
            drp_cutorderType.SelectedIndex = 0;
            drp_ourstyle.SelectedIndex = 0;
           
            txt_cutno.Text = "";
            
          
        
            txt_fabAllocation.Text = "0";
        }

        /// <summary>
        /// valiadtes the form before saving athe data
        /// </summary>
        /// <returns></returns
        public Boolean validationcontrol()
        {
            lbl_errordisplayer.Text = "";
            Boolean sucess = false;
            if (drp_Atc.SelectedValue.ToString().Trim() == null || drp_Atc.SelectedValue.ToString().Trim() == "")
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



        public void MessageBoxShow(String mssg)
        {
            lbl_errordisplayer.Text = mssg;

        }


        public void SaveCutOrder()
        {
            if (validationcontrol())
            {
                BLL.CutOrderBLL.CutOrderData cdata = new BLL.CutOrderBLL.CutOrderData();

                if (!cdata.IsCutOrdernumPresent(txt_cutno.Text.ToString()))
                {
                    cdata.Atcid = int.Parse(drp_Atc.SelectedValue.ToString());
                    cdata.Ourstyleid = int.Parse(drp_ourstyle.SelectedValue.ToString());
                    cdata.FabDescription = ddl_color.SelectedItem.Text.Trim();
                    cdata.Skudet_pk = int.Parse(ddl_color.SelectedValue.ToString());
                    cdata.CutNum = txt_cutno.Text.ToString();
                    cdata.Tofactid = int.Parse(drp_fact.SelectedValue.ToString());
                    cdata.CutorderType = drp_cutorderType.SelectedItem.ToString().Trim();
                    cdata.CofabAllocation = int.Parse(txt_fabAllocation.Text);
                    cdata.Cutablewidth = drp_width.Text.Trim();
                    cdata.Shrinkage = drp_shrink.Text.Trim();
                    cdata.patername = txt_markername.Text.Trim();
                    
                    if (drp_cutorderType.SelectedItem.Text == "Extra")
                    {
                        cdata.ExtraReason_Pk = int.Parse(drp_reason.SelectedValue.ToString());
                    }
                    else
                    {
                        cdata.ExtraReason_Pk = 6;


                    }
                    cdata.MarkerType = drp_markerType.SelectedItem.Text.Trim();
                    cdata.CutDetailsDataCollection = GetMArkerDetData();
                    String cutno = lbl_errordisplayer.Text = cdata.InsertNewCutOrder();
                    String msg = "Cutorder # : " + cutno + " is Generated Sucessfully";
                    MessgeboxUpdate("sucess", msg);
                    
                }
                else
                {
                    lbl_errordisplayer.Text = "CutOrder NO# Already Present";
                }
            }
        }






        public List<BLL.CutOrderBLL.CutDetailsData> GetMArkerDetData()
        {

            List<BLL.CutOrderBLL.CutDetailsData> rk = new List<BLL.CutOrderBLL.CutDetailsData>();


            foreach (GridViewRow di in tbl_marker.Rows)
            {
                

                
                

                    int txt_qty = int.Parse(((di.FindControl("txt_qty") as TextBox).Text.ToString()));
                int txt_nopc = int.Parse(((di.FindControl("txt_nopc") as TextBox).Text.ToString()));

                    String txt_markernum = ((di.FindControl("txt_markernum") as TextBox).Text);



                    BLL.CutOrderBLL.CutDetailsData cutdet = new BLL.CutOrderBLL.CutDetailsData();
                cutdet.MarkerNo = txt_markernum;
                cutdet.NoOfPc = txt_nopc;
                cutdet.Qty = txt_qty;
              


                    rk.Add(cutdet);
                
            }
            return rk;


        }







        protected void btn_saveCutorder_Click(object sender, EventArgs e)
        {
            SaveCutOrder();
            
            showGrid();
        }








        public void showGrid()
        {

            ViewState["sizedata"] = null;
            DataTable dt1 = createdatatable(int.Parse(drp_ourstyle.SelectedValue.ToString().ToString()));
            ViewState["sizedata"] = dt1;
            GenerateTable(dt1);

            //ViewState["cutsizedata"] = null;

            //BLL.CutOrderBLL.CutDetailsData cddetdata = new BLL.CutOrderBLL.CutDetailsData();
            //DataTable dt = cddetdata.GetCutOrderSizeData(int.Parse(drp_cutorder.SelectedValue.ToString().ToString()));
            //ViewState["cutsizedata"] = dt;



            upd_gridtable.Update();
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

                }
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
                            if (j == 0 && i == 0)
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
                            else if (j == 0 && i == 1)
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

                            else if (j == dt.Columns.Count - 1 && i == 0)
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
                            else if (j == dt.Columns.Count - 1 && i == 1)
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
           //     upd_table.Update();
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
                    row1[i] = 0;
                }

                dt.Rows.Add(row1);





            }




            return dt;
        }


        protected void drp_ourstyle_DataBound(object sender, EventArgs e)
        {

        }

        protected void btn_show_Click(object sender, EventArgs e)
        {
            fillColorcombo();
            FillOurStyleCombo(int.Parse(drp_Atc.SelectedValue.ToString()));
            ViewState["sizedata"] = null;
            Session["cutid"] = 0;
        }

       

        protected void drp_ourstyle_SelectionChanged(object sender, Infragistics.Web.UI.ListControls.DropDownSelectionChangedEventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {

        }

        protected void drp_cutorderType_SelectionChanged(object sender, Infragistics.Web.UI.ListControls.DropDownSelectionChangedEventArgs e)
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

        protected void btn_color_Click(object sender, EventArgs e)
        {
            FillCombo(int.Parse(ddl_color.SelectedValue.ToString()));
            Session["cutid"] = 0;
        }











        public void FillCombo(int skudet_pk)
        {
            cdata = new BLL.CutOrderBLL.CutOrderData();


            drp_shrink.DataSource = cdata.GetFabricShrinkage(int.Parse(ddl_color.SelectedValue.ToString()));
            drp_shrink.DataTextField = "ShrinkageGroup";
            drp_shrink.DataValueField = "ShrinkageGroup";
            drp_shrink.DataBind();


            drp_width.DataSource = cdata.GetFabricWidth(int.Parse(ddl_color.SelectedValue.ToString()));
            drp_width.DataTextField = "WidthGroup";
            drp_width.DataValueField = "WidthGroup";
            drp_width.DataBind();

            drp_markerType.DataSource = cdata.GetFabricMarkertype(int.Parse(ddl_color.SelectedValue.ToString()));
            drp_markerType.DataTextField = "MarkerType";
            drp_markerType.DataValueField = "MarkerType";
            drp_markerType.DataBind();
        }


        protected void btn_markertype_Click(object sender, EventArgs e)
        {
           
            tbl_marker.DataSource = BLL.CutOrderBLL.CutOrderData.CreateRollRows(int.Parse(txt_noofmarker.Text));
            tbl_marker.DataBind();
            upd_grid.Update();
        }

        protected void btnMarkerconfirm_Click(object sender, EventArgs e)
        {if(checkdatagridValue())
            {
              

            }

        }


        public Boolean checkdatagridValue()
        {

            Boolean isQtyok = true;
            for (int i = 0; i < tbl_marker.Rows.Count; i++)
            {
                GridViewRow currentRow = tbl_marker.Rows[i];
           
              
                    try
                    {

                        String pcpermarker =((tbl_marker.Rows[i].FindControl("txt_nopc") as TextBox).Text.ToString());
                       String qty = ((tbl_marker.Rows[i].FindControl("txt_qty") as TextBox).Text.ToString());
                    if (!QuantityValidator.ISFloat(pcpermarker))
                        {
                          


                            (tbl_marker.Rows[i].FindControl("txt_nopc") as TextBox).BackColor = System.Drawing.Color.Red;


                        }
                        else
                        {
                            (tbl_marker.Rows[i].FindControl("txt_qty") as TextBox).BackColor = System.Drawing.Color.White;
                        }
                    if (!QuantityValidator.ISFloat(pcpermarker))
                    {



                        (tbl_marker.Rows[i].FindControl("txt_nopc") as TextBox).BackColor = System.Drawing.Color.Red;


                    }
                    else
                    {
                        (tbl_marker.Rows[i].FindControl("txt_qty") as TextBox).BackColor = System.Drawing.Color.White;
                    }
                }
                    catch (Exception)
                    {
                        isQtyok = false;
                        (tbl_marker.Rows[i].FindControl("txt_qty") as TextBox).BackColor = System.Drawing.Color.Red;

                    }
              







            }
          
            return isQtyok;
        }




        public float calculategridValue()
        {


            float invvalue = 0;
            for (int i = 0; i < tbl_marker.Rows.Count; i++)
            {
                GridViewRow currentRow = tbl_marker.Rows[i];
               
                    try
                    {

                        float Invqty = float.Parse(((tbl_marker.Rows[i].FindControl("txt_qty") as TextBox).Text.ToString()));
                       
                        invvalue = invvalue + Invqty;
                    }
                    catch (Exception)
                    {

                        (tbl_marker.Rows[i].FindControl("txt_invQty") as TextBox).BackColor = System.Drawing.Color.Red;

                    }
                


            }

            return invvalue;
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
                    cddetdata.CutSizeDetailsDataCollection = GetSizedata(CutOrderDet_PK, row);
                    cddetdata.InsertCutOrderSizeData();
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




        public List<BLL.CutOrderBLL.CutSizeDetailsData> GetSizedata(int cutdetPK, GridViewRow row)
        {

            List<BLL.CutOrderBLL.CutSizeDetailsData> rk = new List<BLL.CutOrderBLL.CutSizeDetailsData>();




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
                                    cutdet.CutOrderDet_PK = cutdetPK;
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

        protected void tbl_cutorderdata_SelectedIndexChanged(object sender, EventArgs e)
        {

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




    }
}
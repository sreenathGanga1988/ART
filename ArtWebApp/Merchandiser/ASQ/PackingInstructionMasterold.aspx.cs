using ArtWebApp.BLL;
using ArtWebApp.DataModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ArtWebApp.DataModelAtcWorld;
using System.Drawing;

namespace ArtWebApp.Merchandiser.ASQ
{
    public partial class PackingInstructionMaster : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
            else
            {
                filldata();
            }
        }

        protected void buttonAtc_Click(object sender, EventArgs e)
        {
            fillcontrol();
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







            //showAllPoPackATC();
        }
        protected void cmb_atc_SelectedIndexChanged(object sender, EventArgs e)
        {

        }




        private void GenerateCalTable(DataTable dt, GridViewRow di)
        {

            dt.Columns.Add("ColorTotal", typeof(System.Int32));

            dt = ArtWebApp.Controls.DataTableFunction.SumOfDataColumns(1, dt.Columns.Count - 2, 0, dt.Rows.Count - 1, dt.Columns.Count - 1, dt.Rows.Count - 1, dt);

            BLL.CutOrderBLL.CutDetailsData cddetdataclass = new BLL.CutOrderBLL.CutDetailsData();




            Panel panel1 = (di.FindControl("panel3") as Panel);
            Table Table1 = (di.FindControl("Table3") as Table);


            //   Creat the Table and Add it to the Page
            Table1.Rows.Clear();




            Table1.ID = "Table3";
            // Page.Form.Controls.Add(Table1);
            panel1.Controls.Add(Table1);
            Table1.CssClass = "calctable";
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
                        tb.Width = 60;

                    }
                    else
                    {
                        tb.Width = 60;
                    }
                    tb.Font.Size = 8;
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
                            tb.Width = 60;
                            tb.Text = dt.Rows[i][j].ToString();
                        }
                        else if (dt.Columns[j].ColumnName == "ColorTotal")
                        {
                            tb.CssClass = "ColorTotal";
                            tb.Enabled = false;
                            tb.Width = 60;
                            dt.Rows[i][j] = dt.Rows[i][j].ToString();
                        }
                        else
                        {
                            tb.CssClass = "Qty";
                            tb.Width = 50;
                            tb.Text = "0";
                        }



                        //  Set a unique ID for each TextBox added
                        // tb.ReadOnly = true;
                        tb.Attributes.Add("onkeypress", "return isNumberKey(event,this)");
                        tb.Attributes.Add("onchange", "totalcalculation()");
                        tb.ID = "tb3" + i + j;

                        tb.Font.Size = 8;
                        //tb.Font.Size = 10;
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


        }
        private void GenerateTable(DataTable dt, GridViewRow di)
        {

            dt.Columns.Add("ColorTotal", typeof(System.Int32));

            dt = ArtWebApp.Controls.DataTableFunction.SumOfDataColumns(1, dt.Columns.Count - 2, 0, dt.Rows.Count - 1, dt.Columns.Count - 1, dt.Rows.Count - 1, dt);

            BLL.CutOrderBLL.CutDetailsData cddetdataclass = new BLL.CutOrderBLL.CutDetailsData();




            Panel panel1 = (di.FindControl("panel1") as Panel);
            Table Table1 = (di.FindControl("Table1") as Table);


            //   Creat the Table and Add it to the Page
            Table1.Rows.Clear();




            Table1.ID = "Table1";
            // Page.Form.Controls.Add(Table1);
            panel1.Controls.Add(Table1);
            Table1.CssClass = "PendingTable";
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
                    tb.Font.Size = 8;
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
                            tb.Width = 60;
                        }
                        else if (dt.Columns[j].ColumnName == "ColorTotal")
                        {
                            tb.CssClass = "ColorTotal";
                            tb.Enabled = false;
                            tb.Width = 60;
                            dt.Rows[i][j] = dt.Rows[i][j].ToString();
                        }
                        else
                        {
                            tb.CssClass = "Qty";
                            tb.Width = 50;
                        }



                        //  Set a unique ID for each TextBox added
                        // tb.ReadOnly = true;
                        tb.Attributes.Add("onkeypress", "return isNumberKey(event,this)");
                        tb.Attributes.Add("onchange", "sumofQty(this)");
                        tb.ID = "tb" + i + j;
                        tb.Text = dt.Rows[i][j].ToString();
                        tb.Font.Size = 8;
                        //tb.Font.Size = 10;
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

        private void GeneratesubTable(DataTable dt, GridViewRow di)
        {

            dt.Columns.Add("ColorTotal", typeof(System.Int32));

            dt = ArtWebApp.Controls.DataTableFunction.SumOfDataColumns(1, dt.Columns.Count - 2, 0, dt.Rows.Count - 1, dt.Columns.Count - 1, dt.Rows.Count - 1, dt);

            BLL.CutOrderBLL.CutDetailsData cddetdataclass = new BLL.CutOrderBLL.CutDetailsData();




            Panel panel1 = (di.FindControl("panel2") as Panel);
            Table Table1 = (di.FindControl("Table2") as Table);


            //   Creat the Table and Add it to the Page
            Table1.Rows.Clear();




            Table1.ID = "Table2";
            // Page.Form.Controls.Add(Table1);
            panel1.Controls.Add(Table1);
            Table1.CssClass = "newTable";
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
                        tb.Width = 60;

                    }
                    else
                    {
                        tb.Width = 60;
                    }
                    tb.Font.Size = 8;
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
                            tb.Width = 60;
                            tb.Text = dt.Rows[i][j].ToString();
                        }
                        else if (dt.Columns[j].ColumnName == "ColorTotal")
                        {
                            tb.CssClass = "ColorTotal";
                            tb.Enabled = false;
                            tb.Width = 60;
                            dt.Rows[i][j] = dt.Rows[i][j].ToString();
                        }
                        else
                        {
                            tb.CssClass = "Qty";
                            tb.Width = 50;
                            tb.Text = "0";
                        }



                        //  Set a unique ID for each TextBox added
                        // tb.ReadOnly = true;
                        tb.Attributes.Add("onkeypress", "return isNumberKey(event,this)");
                        tb.Attributes.Add("onchange", "totalcalculation()");
                        tb.ID = "tb2" + i + j;

                        tb.Font.Size = 8;
                        //tb.Font.Size = 10;
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


        }

        protected void tbl_podata_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int ourstyleid = int.Parse((e.Row.FindControl("lbl_ourstyleid") as Label).Text);
                int popackid = int.Parse((e.Row.FindControl("lbl_popackid") as Label).Text);
                int lbl_Location_PK = int.Parse((e.Row.FindControl("lbl_Location_PK") as Label).Text);

                DataTable SizedColorData = new DataTable();


                GenerateTable(BLL.MerchandsingBLL.AllocationBLL.createdatatable(ourstyleid, popackid, lbl_Location_PK, (DataTable)(ViewState["sizecolordata"])), e.Row);
                GenerateCalTable(BLL.MerchandsingBLL.AllocationBLL.createdatatable(ourstyleid, popackid, lbl_Location_PK, (DataTable)(ViewState["sizecolordata"])), e.Row);
                GeneratesubTable(BLL.MerchandsingBLL.AllocationBLL.createdatatable(ourstyleid, popackid, lbl_Location_PK, (DataTable)(ViewState["sizecolordata"])), e.Row);
                string lbl_iscutable = BLL.popackupdater.IsASQCutable(ourstyleid, popackid);
                





            }

        }

        protected void tbl_podata_RowCommand(object sender, GridViewCommandEventArgs e)
        {


        }




        

        protected void tbl_podata_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void buttonAtc0_Click(object sender, EventArgs e)
        {
           
        }



        //public void fillcolrcombo()
        //{
        //    DataView colorview = new DataView((DataTable)(ViewState["mstrdata"]));
        //    DataTable distinctcolorValues = colorview.ToTable(true, "ColorName");
        //    drp_color.DataSource = distinctcolorValues;
        //    drp_color.DataTextField = "ColorName";
        //    drp_color.DataValueField = "ColorName";
        //    drp_color.DataBind();
        //}



        public ArrayList getselectedasqlist()
        {
            ArrayList popaklist = new ArrayList();
            List<Infragistics.Web.UI.ListControls.DropDownItem> items = drp_popack.SelectedItems;
            foreach (Infragistics.Web.UI.ListControls.DropDownItem item in items)
            {

                int popackid = int.Parse(item.Value.ToString());
                popaklist.Add(popackid);
            }
            return popaklist;
        }

        public DataTable getPopackMasterdata(ArrayList popaklist)
        {
            BLL.MerchandsingBLL.AllocationBLL pkmstrdata = new BLL.MerchandsingBLL.AllocationBLL();
            DataTable mstrdata = new DataTable();
            if (drp_type.SelectedValue.ToString ().Trim()== "Ass Color Ass Size")
            {
               mstrdata = pkmstrdata.GetAllASQforPacking(popaklist);
            }
            else if (drp_type.SelectedValue.ToString().Trim() == "Solid Color Assc Size")
            {
                mstrdata = pkmstrdata.GetAllASQforPackingWithColor(popaklist);
            }
             else  if (drp_type.SelectedValue.ToString().Trim() == "Solid Color Solid Size")
            {
                mstrdata = pkmstrdata.GetAllASQforPacking(popaklist);
            }
            else if (drp_type.SelectedValue.ToString().Trim() == "Assc Color Solid Size")
            {
                mstrdata = pkmstrdata.GetAllASQforPacking(popaklist);
            }

            ViewState["mstrdata"] = mstrdata;

            return mstrdata;

        }

        public DataTable GetSizecolordata(ArrayList popaklist)
        {
            BLL.MerchandsingBLL.AllocationBLL pkmstrdata = new BLL.MerchandsingBLL.AllocationBLL();
            DataTable sizecolordata = (DataTable)(ViewState["sizecolordata"]);
            if (drp_type.SelectedValue.ToString().Trim() == "Ass Color Ass Size")
            {
              
            }
            else if (drp_type.SelectedValue.ToString().Trim() == "Solid Color Assc Size")
            {
            
            }
            else if (drp_type.SelectedValue.ToString().Trim() == "Solid Color Solid Size")
            {
              
            }
            else if (drp_type.SelectedValue.ToString().Trim() == "Assc Color Solid Size")
            {
               
            }

            ViewState["sizecolordata"] = sizecolordata;

            return sizecolordata;

        }




        public void fillGridview()
        {

        }

        public void filldata()
        {

            foreach (GridViewRow row in tbl_podata.Rows)
            {

                int ourstyleid = int.Parse((row.FindControl("lbl_ourstyleid") as Label).Text);
                int popackid = int.Parse((row.FindControl("lbl_popackid") as Label).Text);
                int lbl_Location_PK = int.Parse((row.FindControl("lbl_Location_PK") as Label).Text);

              //  GenerateTable(BLL.MerchandsingBLL.AllocationBLL.createdatatable(ourstyleid, popackid, lbl_Location_PK, (DataTable)(ViewState["sizecolordata"])), row);

            //    GeneratesubTable(BLL.MerchandsingBLL.AllocationBLL.createdatatable(ourstyleid, popackid, lbl_Location_PK, (DataTable)(ViewState["sizecolordata"])), row);
                //  string lbl_iscutable = BLL.popackupdater.IsASQCutable(ourstyleid, popackid);

                GenerateTable(BLL.MerchandsingBLL.AllocationBLL.createdatatable(ourstyleid, popackid, lbl_Location_PK, (DataTable)(ViewState["sizecolordata"])), row);
                GenerateCalTable(BLL.MerchandsingBLL.AllocationBLL.createdatatable(ourstyleid, popackid, lbl_Location_PK, (DataTable)(ViewState["sizecolordata"])), row);
                GeneratesubTable(BLL.MerchandsingBLL.AllocationBLL.createdatatable(ourstyleid, popackid, lbl_Location_PK, (DataTable)(ViewState["sizecolordata"])), row);

            }
        }
        protected void btn_savelist_Click(object sender, EventArgs e)
        {
           
          insertPackinglist();
        }
        public void insertPackinglist()
        {
            ArtWebApp.BLL.MerchandsingBLL.PackingListMasterBLL pdata = new ArtWebApp.BLL.MerchandsingBLL.PackingListMasterBLL();

            int atcid = int.Parse(cmb_atc.SelectedValue.ToString());
            int noofctn = int.Parse(txt_totalctn.Text);
            int pcperctn = int.Parse(txt_pcperctn.Text);
       
            pdata.Atc_ID = atcid;
            pdata.NoofCTN = noofctn;
            pdata.PCPerCtn = pcperctn;
            pdata.PcPerPolybag = int.Parse(txt_polybagperctn.Text);
            pdata.PackingInstruction = txt_instruction.InnerText;
            //pdata.CtnDimension = ctndiemension;

            pdata.PackingListdetailDataDataCollection = GetSizeColordata();
            pdata.insertPackinglistMaster();
            //    tbl_podata.DataSource = null;
              //  tbl_podata.DataBind();
                String msg = " ASQ Details Added Sucessfully ";
                ArtWebApp.Controls.Messagebox.MessgeboxUpdate(Messaediv, "sucess", msg);
            
        }


        public void insertSolidColorPackinglist()
        {
            ArtWebApp.BLL.MerchandsingBLL.PackingListMasterBLL pdata = new ArtWebApp.BLL.MerchandsingBLL.PackingListMasterBLL();

            int atcid = int.Parse(cmb_atc.SelectedValue.ToString());
            int noofctn = int.Parse(txt_totalctn.Text);
            int pcperctn = int.Parse(txt_pcperctn.Text);

            pdata.Atc_ID = atcid;
          
            pdata.PcPerPolybag = int.Parse(txt_polybagperctn.Text);
            pdata.PackingInstruction = txt_instruction.InnerText;
            //pdata.CtnDimension = ctndiemension;

            pdata.PackingListdetailDataDataCollection = GetSizedataofSolidcolor();
            pdata.insertPackinglistMaster();
            //    tbl_podata.DataSource = null;
            //  tbl_podata.DataBind();
            String msg = " ASQ Details Added Sucessfully ";
            ArtWebApp.Controls.Messagebox.MessgeboxUpdate(Messaediv, "sucess", msg);

        }

        public List<BLL.MerchandsingBLL.PackingListdetailDataBLL> GetSizeColordata()
        {

            List<BLL.MerchandsingBLL.PackingListdetailDataBLL> rk = new List<BLL.MerchandsingBLL.PackingListdetailDataBLL>();

            foreach (GridViewRow row in tbl_podata.Rows)
            {





                CheckBox chkBx = (CheckBox)row.FindControl("chk_select");
                if (chkBx.Checked == true)
                {




                    int lbl_popackid = int.Parse((row.FindControl("lbl_popackid") as Label).Text);
                    int lbl_ourstyleid = int.Parse((row.FindControl("lbl_ourstyleid") as Label).Text);
                    int atcid = int.Parse((row.FindControl("lbl_atcid") as Label).Text);

                    


                    Panel panel1 = (row.FindControl("panel2") as Panel);
                    Table Table1 = (row.FindControl("Table2") as Table);




                    for (int tabroindex = 1; tabroindex < Table1.Rows.Count; tabroindex++)
                    {
                        TableRow tbrow = Table1.Rows[tabroindex];

                        for (int tabcellindex = 1; tabcellindex < tbrow.Cells.Count; tabcellindex++)
                        {

                            TableCell cell = tbrow.Cells[tabcellindex];

                            for (int tabcntrlindex = 0; tabcntrlindex < cell.Controls.Count; tabcntrlindex++)
                            {
                                Control ctrl = cell.Controls[tabcntrlindex];


                                if (ctrl is TextBox)
                                {
                                    TextBox txtqty = (TextBox)ctrl;

                                    TextBox txtcolor = (TextBox)Table1.Rows[tabroindex].Cells[0].Controls[0];
                                    Label txtsize = (Label)Table1.Rows[0].Cells[tabcellindex].Controls[0];

                                    string color = txtcolor.Text;

                                    string size = txtsize.Text;

                                    string qty = txtqty.Text;

                                    Control ctrlsize = Table1.Rows[0].Cells[tabcellindex].Controls[0];
                                        Label lblsize = (Label)ctrlsize;


                                        if (txtsize.Text.Trim() != "ColorTotal" && int.Parse(txtqty.Text)>0)
                                        {

                                            BLL.MerchandsingBLL.PackingListdetailDataBLL pkdet = new BLL.MerchandsingBLL.PackingListdetailDataBLL();


                                        pkdet.POPackId = lbl_popackid;
                                        pkdet.OurStyleID = lbl_ourstyleid;
                                        pkdet.Atcid = atcid;
                                        pkdet.SizeName = txtsize.Text; 
                                        pkdet.ColorName = txtcolor.Text;
                                        pkdet.TotalQty = int.Parse(txtqty.Text);

                                        rk.Add(pkdet);
                                        }



                                    }


                                }
                            }
                        }

                    }

                }

            

            return rk;
        }




        public List<BLL.MerchandsingBLL.PackingListdetailDataBLL> GetSizedataofSolidcolor()
        {

            List<BLL.MerchandsingBLL.PackingListdetailDataBLL> rk = new List<BLL.MerchandsingBLL.PackingListdetailDataBLL>();

            foreach (GridViewRow row in tbl_podata.Rows)
            {





                CheckBox chkBx = (CheckBox)row.FindControl("chk_select");
                if (chkBx.Checked == true)
                {




                    int lbl_popackid = int.Parse((row.FindControl("lbl_popackid") as Label).Text);
                    int lbl_ourstyleid = int.Parse((row.FindControl("lbl_ourstyleid") as Label).Text);
                    int atcid = int.Parse((row.FindControl("lbl_atcid") as Label).Text);


                    int txt_totalctnnew = int.Parse((row.FindControl("txt_totalctnnew") as TextBox).Text);
                    int txt_pcperctnnew = int.Parse((row.FindControl("txt_pcperctnnew") as TextBox).Text);


                    Panel panel1 = (row.FindControl("panel2") as Panel);
                    Table Table1 = (row.FindControl("Table2") as Table);




                    for (int tabroindex = 1; tabroindex < Table1.Rows.Count; tabroindex++)
                    {
                        TableRow tbrow = Table1.Rows[tabroindex];

                        for (int tabcellindex = 1; tabcellindex < tbrow.Cells.Count; tabcellindex++)
                        {

                            TableCell cell = tbrow.Cells[tabcellindex];

                            for (int tabcntrlindex = 0; tabcntrlindex < cell.Controls.Count; tabcntrlindex++)
                            {
                                Control ctrl = cell.Controls[tabcntrlindex];


                                if (ctrl is TextBox)
                                {
                                    TextBox txtqty = (TextBox)ctrl;

                                    TextBox txtcolor = (TextBox)Table1.Rows[tabroindex].Cells[0].Controls[0];
                                    Label txtsize = (Label)Table1.Rows[0].Cells[tabcellindex].Controls[0];

                                    string color = txtcolor.Text;

                                    string size = txtsize.Text;

                                    string qty = txtqty.Text;

                                    Control ctrlsize = Table1.Rows[0].Cells[tabcellindex].Controls[0];
                                    Label lblsize = (Label)ctrlsize;


                                    if (txtsize.Text.Trim() != "ColorTotal" && int.Parse(txtqty.Text) > 0)
                                    {

                                        BLL.MerchandsingBLL.PackingListdetailDataBLL pkdet = new BLL.MerchandsingBLL.PackingListdetailDataBLL();


                                        pkdet.POPackId = lbl_popackid;
                                        pkdet.OurStyleID = lbl_ourstyleid;
                                        pkdet.Atcid = atcid;
                                        pkdet.SizeName = txtsize.Text;
                                        pkdet.ColorName = txtcolor.Text;
                                        pkdet.TotalQty = int.Parse(txtqty.Text);
                                        pkdet.NoofCTN = txt_totalctnnew;
                                        pkdet.PcperCtn= txt_pcperctnnew;
                                        rk.Add(pkdet);
                                    }



                                }


                            }
                        }
                    }

                }

            }



            return rk;
        }



        protected void Button1_Click(object sender, EventArgs e)
        {
            tbl_podata.DataSource = null;

            tbl_podata.DataBind();
            updgrid.Update();



            fillgridview();
            if (drp_type.SelectedValue.ToString().Trim() == "Ass Color Ass Size")
            {
                tbl_podata.DataSource = (DataTable)ViewState["mstrdata"];

                tbl_podata.DataBind();
                updgrid.Update();
                tbl_podata.Columns[4]. Visible=false;
               
            }
           
              else if (drp_type.SelectedValue.ToString().Trim() == "Solid Color Assc Size")
            {

                Response.Redirect(@"~\Merchandiser\ASQ\Packing\SolidColorpackingInstruction.aspx");

            }
            else 
            {
               Response.Redirect(@"~\Merchandiser\ASQ\Packing\SolidColorpackingInstruction.aspx");
            }
          

        
        }






           public void fillgridview()
        {
            ArrayList popaklist = getselectedasqlist();


            if (popaklist.Count > 0 && popaklist != null)
            {

                BLL.MerchandsingBLL.AllocationBLL pkmstrdata = new BLL.MerchandsingBLL.AllocationBLL();
                DataTable wholedata = pkmstrdata.GetAllASQqDetailsforPacking(popaklist);
                ViewState["sizecolordata"] = wholedata;

                DataTable mstrdata = getPopackMasterdata(popaklist);
                ViewState["mstrdata"] = mstrdata;
           


            }
        }

       
    }
}
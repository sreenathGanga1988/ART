using ArtWebApp.DataModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArtWebApp.Merchandiser.ASQ.Packing
{
    public partial class SolidColorpackingInstruction : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                tbl_podata.DataSource = (DataTable)Session["mstrdata"];

                tbl_podata.DataBind();
                updgrid.Update();
            }
            else
            {
               filldata();
            }
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
                String ColorName = (e.Row.FindControl("lbl_ColorName") as Label).Text;


                DataTable SizedColorData = (DataTable)Session["sizecolordata"];


                GenerateTable(BLL.MerchandsingBLL.AllocationBLL.createdatatableofColor(ourstyleid, popackid, lbl_Location_PK, ColorName, SizedColorData), e.Row);
                GenerateCalTable(BLL.MerchandsingBLL.AllocationBLL.createdatatableofColor(ourstyleid, popackid, lbl_Location_PK, ColorName, SizedColorData), e.Row);
                GeneratesubTable(BLL.MerchandsingBLL.AllocationBLL.createdatatableofColor(ourstyleid, popackid, lbl_Location_PK, ColorName, SizedColorData), e.Row);
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
                String ColorName = (row.FindControl("lbl_ColorName") as Label).Text;


                DataTable SizedColorData = (DataTable)Session["sizecolordata"];


                GenerateTable(BLL.MerchandsingBLL.AllocationBLL.createdatatableofColor(ourstyleid, popackid, lbl_Location_PK, ColorName, SizedColorData), row);
                GenerateCalTable(BLL.MerchandsingBLL.AllocationBLL.createdatatableofColor(ourstyleid, popackid, lbl_Location_PK, ColorName, SizedColorData), row);
                GeneratesubTable(BLL.MerchandsingBLL.AllocationBLL.createdatatableofColor(ourstyleid, popackid, lbl_Location_PK, ColorName, SizedColorData), row);
                string lbl_iscutable = BLL.popackupdater.IsASQCutable(ourstyleid, popackid);
            }
        }
        protected void btn_savelist_Click(object sender, EventArgs e)
        {

            insertSolidColorPackinglist();
            tbl_podata.DataSource = null;

            tbl_podata.DataBind();
            updgrid.Update();
        }


        public void insertSolidColorPackinglist()
        {



            foreach (GridViewRow row in tbl_podata.Rows)
            {





                CheckBox chkBx = (CheckBox)row.FindControl("chk_select");
                if (chkBx.Checked == true)
                {

                    int atcid = int.Parse((row.FindControl("lbl_atcid") as Label).Text);


                    int txt_totalctnnew = int.Parse((row.FindControl("txt_totalctnnew") as TextBox).Text);
                    int txt_pcperctnnew = int.Parse((row.FindControl("txt_pcperctnnew") as TextBox).Text);

                    ArtWebApp.BLL.MerchandsingBLL.PackingListMasterBLL pdata = new ArtWebApp.BLL.MerchandsingBLL.PackingListMasterBLL();

                    decimal txt_length = decimal.Parse((row.FindControl("txt_length") as TextBox).Text);
                    decimal txt_width = decimal.Parse((row.FindControl("txt_width") as TextBox).Text);
                    decimal txt_height = decimal.Parse((row.FindControl("txt_height") as TextBox).Text);
                    decimal txt_NNWeight = decimal.Parse((row.FindControl("txt_NNWeight") as TextBox).Text);
                    decimal txt_Netweight = decimal.Parse((row.FindControl("txt_Netweight") as TextBox).Text);
                    decimal txt_gross = decimal.Parse((row.FindControl("txt_gross") as TextBox).Text);
                    string drp_weightuom = (row.FindControl("drp_weightuom") as DropDownList).SelectedValue.ToString();
                    string drp_NetUOM = (row.FindControl("drp_NetUOM") as DropDownList).SelectedValue.ToString();

                   

                    pdata.Atc_ID = atcid;

                    pdata.CtnDimension = "NA";
                    pdata.NoofCTN = txt_totalctnnew;
                    pdata.PCPerCtn = txt_pcperctnnew;
                    pdata.PcPerPolybag = 0;
                    pdata.PackingInstruction = txt_instruction.InnerText;
                    pdata.Length = txt_length;
                    pdata.Width = txt_width;
                    pdata.Height = txt_height;
                    pdata.NetWeight = txt_Netweight;
                    pdata.NNWeight = txt_NNWeight;
                    pdata.Grossweight = txt_gross;
                    pdata.WeightUOM = drp_weightuom;
                    pdata.CtnUOM = drp_NetUOM;

                    pdata.PackingListdetailDataDataCollection = GetSizedataofSolidcolor(row);
                    pdata.insertPackinglistMaster();
                    //    tbl_podata.DataSource = null;
                    //  tbl_podata.DataBind();
                    String msg = " ASQ Details Added Successfully ";
                    ArtWebApp.Controls.Messagebox.MessgeboxUpdate(Messaediv, "sucess", msg);
                }
            }

        }




        public List<BLL.MerchandsingBLL.PackingListdetailDataBLL> GetSizedataofSolidcolor(GridViewRow row)
        {

            List<BLL.MerchandsingBLL.PackingListdetailDataBLL> rk = new List<BLL.MerchandsingBLL.PackingListdetailDataBLL>();




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
                                        pkdet.PcperCtn = txt_pcperctnnew;
                                        rk.Add(pkdet);
                                    }



                                }


                            }
                        }
                    }

                }

            



            return rk;
        }







    }
}
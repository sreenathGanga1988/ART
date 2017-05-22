using ArtWebApp.BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArtWebApp.Merchandiser.ASQ
{
    public partial class ASQUpdator : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void buttonAtc_Click(object sender, EventArgs e)
        {

        }

        protected void cmb_atc_SelectedIndexChanged(object sender, EventArgs e)
        {

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
                                    cell.Width = 70;

                                    TextBox tb = new TextBox();
                                    
                        if(j==0)
                        {
                            tb.CssClass = "colorname";
                            tb.Enabled = false;
                            tb.Width = 80;
                        }
                        else if(dt.Columns[j].ColumnName== "ColorTotal")
                        {
                            tb.CssClass = "ColorTotal";
                            tb.Enabled = false;
                            tb.Width = 70;
                            dt.Rows[i][j] = dt.Rows[i][j].ToString();
                        }
                        else
                        {
                            tb.CssClass = "Qty";
                            tb.Width = 60;
                        }


                                   
                                    //  Set a unique ID for each TextBox added
                                    // tb.ReadOnly = true;
                                    tb.Attributes.Add("onkeypress", "return isNumberKey(event,this)");
                                    tb.Attributes.Add("onchange", "sumofQty(this)");
                                    tb.ID = "tb" + i + j;
                                    tb.Text = dt.Rows[i][j].ToString();
                        tb.Font.Size = 8;
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

                GenerateTable(BLL.popackupdater.createdatatable(ourstyleid,popackid),e.Row);


                string lbl_iscutable = BLL.popackupdater.IsASQCutable(ourstyleid, popackid);
                CheckBox chK_IsCutable = (e.Row.FindControl("chK_IsCutable") as CheckBox);
                if (lbl_iscutable.Trim() == "Y")
                {
                    chK_IsCutable.Checked = true;
                    chK_IsCutable.Enabled = false;
                }


                string lbl_ispackable = BLL.popackupdater.IsASQPackable(ourstyleid, popackid);
                CheckBox chK_Ispackable = (e.Row.FindControl("chK_Ispackable") as CheckBox);
                if (lbl_ispackable.Trim() == "Y")
                {
                    chK_Ispackable.Checked = true;
                    chK_Ispackable.Enabled = false;
                }


                string lbl_isDeletable = BLL.popackupdater.IsDeleted(ourstyleid, popackid);
                CheckBox chK_IsDeleted = (e.Row.FindControl("chK_IsDeleted") as CheckBox);
                if (lbl_isDeletable.Trim() == "Y")
                {
                    chK_IsDeleted.Checked = true;
                    chK_IsDeleted.Enabled = false;
                }



            }
               
        }

        protected void tbl_podata_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            
                   DataTable dt = new DataTable();

            dt.Columns.Add("Index", typeof(String));
            dt.Columns.Add("Size", typeof(String));





            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = tbl_podata.Rows[index];
            if (e.CommandName == "CutableUpdate")
            {



                CheckBox chkBx = (CheckBox)row.FindControl("chk_select");
                if (chkBx.Checked == true)
                {
                    
                        
                    int lbl_popackid = int.Parse((row.FindControl("lbl_popackid") as Label).Text);
                    int lbl_ourstyleid = int.Parse((row.FindControl("lbl_ourstyleid") as Label).Text);

                    ArtWebApp.BLL.POPackDetailData pdata = new POPackDetailData();
                   
                    pdata.PoPackId = lbl_popackid;
                    pdata.Ourstyleid = lbl_ourstyleid;
                    CheckBox chK_IsCutable = (CheckBox)row.FindControl("chK_IsCutable");
                    if (chK_IsCutable.Checked == true)
                    {
                        pdata.MarkASQCutable(true);
                    }
                    else
                    {
                        pdata.MarkASQCutable(false);
                    }
                        
                   
                }
            }

            else if (e.CommandName == "PackableUpdate")
            {

                CheckBox chkBx = (CheckBox)row.FindControl("chk_select");
                if (chkBx.Checked == true)
                {


                    int lbl_popackid = int.Parse((row.FindControl("lbl_popackid") as Label).Text);
                    int lbl_ourstyleid = int.Parse((row.FindControl("lbl_ourstyleid") as Label).Text);

                    ArtWebApp.BLL.POPackDetailData pdata = new POPackDetailData();

                    pdata.PoPackId = lbl_popackid;
                    pdata.Ourstyleid = lbl_ourstyleid;
                    CheckBox chK_Ispackable = (CheckBox)row.FindControl("chK_Ispackable");
                    if (chK_Ispackable.Checked == true)
                    {
                        pdata.MarkASQPackable(true);
                    }
                    else
                    {
                        pdata.MarkASQPackable(false);
                    }


                }
            }
            else if (e.CommandName == "Deleteupdate")
            {

                CheckBox chkBx = (CheckBox)row.FindControl("chk_select");
                if (chkBx.Checked == true)
                {


                    int lbl_popackid = int.Parse((row.FindControl("lbl_popackid") as Label).Text);
                    int lbl_ourstyleid = int.Parse((row.FindControl("lbl_ourstyleid") as Label).Text);

                    ArtWebApp.BLL.POPackDetailData pdata = new POPackDetailData();

                    pdata.PoPackId = lbl_popackid;
                    pdata.Ourstyleid = lbl_ourstyleid;
                    CheckBox chK_IsDeleted = (CheckBox)row.FindControl("chK_IsDeleted");
                    if (chK_IsDeleted.Checked == true)
                    {
                        pdata.MarkASQDeleted(true);
                    }
                    else
                    {
                        pdata.MarkASQDeleted(false);
                    }


                }
            }

            else if (e.CommandName == "ShowDropDown")
            {

            }
        }




        public List<BLL.CutOrderBLL.CutSizeDetailsData> GetSizeColordata(int cutdetPK, GridViewRow row)
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

        protected void tbl_podata_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
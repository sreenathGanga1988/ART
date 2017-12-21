using ArtWebApp.DataModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArtWebApp.Merchandiser.ASQ
{
    public partial class ASQAllocation : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if(Session["UserLoc_pk"].ToString ()=="6")
            {
                Button1.Enabled = true;
            }
            else
            {

                Button1.Enabled = false;
            }
          
        }

        protected void buttonAtc_Click(object sender, EventArgs e)
        {
            Fillcontrol();
        }
        public void Fillcontrol()
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
                            tb.Width = 60;
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
                      
                        tb.ID = "tb" + i + j + dt.Rows[i][j].ToString()+"Row"+i+"col"+j;
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
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    int ourstyleid = int.Parse((e.Row.FindControl("lbl_ourstyleid") as Label).Text);
                    int popackid = int.Parse((e.Row.FindControl("lbl_popackid") as Label).Text);
                    CheckBox chkBx = (CheckBox)e.Row.FindControl("chk_select");
                    GenerateTable(BLL.popackupdater.createdatatable(ourstyleid, popackid), e.Row);


                    string lbl_iscutable = BLL.popackupdater.IsASQCutable(ourstyleid, popackid);

                    (e.Row.FindControl("lbl_iscutable") as Label).Text = lbl_iscutable;


                  
                    if (lbl_iscutable.Trim() == "N")
                    {
                        e.Row.Enabled = false;
                        chkBx.Enabled = false;
                    }


                    String lbl_location = (e.Row.FindControl("lbl_location") as Label).Text;
                    if (lbl_location.Trim() == "NA")
                    {
                        DropDownList factlist = (e.Row.FindControl("drp_loc") as DropDownList);
                        factlist.Visible = true;

                        try
                        {
                            int lbl_recfactid = int.Parse((e.Row.FindControl("lbl_recfactid") as Label).Text);
                            factlist.SelectedValue = lbl_recfactid.ToString();
                            factlist.Enabled = false;
                        }
                        catch (Exception)
                        {

                         
                        }
                         
                    }
                    else if(lbl_iscutable.Trim() == "N" && lbl_location.Trim() != "NA")
                    {

                        DropDownList factlist = (e.Row.FindControl("drp_loc") as DropDownList);
                        factlist.Visible = true;
                        e.Row.Enabled = true;
                        chkBx.Enabled = false;
                        try
                        {
                            int lbl_recfactid = int.Parse((e.Row.FindControl("lbl_recfactid") as Label).Text);
                            factlist.SelectedValue = lbl_recfactid.ToString();
                            factlist.Enabled = false;
                        }
                        catch (Exception)
                        {


                        }
                    }
                    else if (lbl_iscutable.Trim() == "Y" && lbl_location.Trim() != "NA")
                    {

                        DropDownList factlist = (e.Row.FindControl("drp_loc") as DropDownList);
                        factlist.Visible = true;
                        e.Row.Enabled = true;
                        try
                        {
                            int lbl_recfactid = int.Parse((e.Row.FindControl("lbl_recfactid") as Label).Text);
                            factlist.SelectedValue = lbl_recfactid.ToString();
                            factlist.Enabled = false;
                        }
                        catch (Exception)
                        {


                        }
                    }
                    else
                    {
                        DropDownList factlist = (e.Row.FindControl("drp_loc") as DropDownList);
                        factlist.Visible = true;
                      
                      
                    }

                  





                }











            }

        }

        protected void tbl_podata_RowCommand(object sender, GridViewCommandEventArgs e)
        {


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

                tbl_podata.DataSource = pkmstrdata.GetPOPACKMasterofList(popaklist);
                //  tbl_podata.DataSource = asqshuffle.GetAllPOPackDataofStyleandPopack(int.Parse(drp_ourstyle.SelectedValue.ToString()), popaklist);
                tbl_podata.DataBind();
                // updgrid2.Update();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

            int k = 0;
            int atcid = int.Parse(cmb_atc.SelectedItem.Value.ToString());


            ArtWebApp.BLL.MerchandsingBLL.AllocationBLL apll = new ArtWebApp.BLL.MerchandsingBLL.AllocationBLL();

            String isatcworld = apll.IsAtcWorldAtc(atcid);

            foreach (GridViewRow row in tbl_podata.Rows)
            {





                CheckBox chkBx = (CheckBox)row.FindControl("chk_select");
                if (chkBx.Checked == true)
                {

                    ArtWebApp.BLL.MerchandsingBLL.AllocationBLL  pdata = new ArtWebApp.BLL.MerchandsingBLL.AllocationBLL();

                    int ourstyleid = int.Parse((row.FindControl("lbl_ourstyleid") as Label).Text);
                    int popackid = int.Parse((row.FindControl("lbl_popackid") as Label).Text);
                    DropDownList factlist = (row.FindControl("drp_loc") as DropDownList);
                    int factid = int.Parse(factlist.SelectedValue.ToString());



                    RadioButtonList potyelist = (row.FindControl("rbt_potype") as RadioButtonList);
                    string potype = potyelist.SelectedValue;
                    pdata.Ourstyleid = ourstyleid;
                    pdata.PoPackId = popackid;

                    pdata.ASQ = (row.FindControl("lbl_asq") as Label).Text;
                    pdata.OurStyle = (row.FindControl("lbl_ourstyle") as Label).Text;
                    pdata.BuyerPO = (row.FindControl("lbl_buyerpo") as Label).Text;
                    pdata.BuyerStyle = (row.FindControl("lbl_buyerstyle") as Label).Text;
                    pdata.Atcnum = (row.FindControl("lbl_atcnum") as Label).Text;
                    pdata.Destination = (row.FindControl("lbl_destination") as Label).Text;
                    pdata.Garmentcatagory = (row.FindControl("lbl_CategoryName") as Label).Text;
                    pdata.seasonName = (row.FindControl("lbl_season") as Label).Text;
                    pdata.ChannelName = (row.FindControl("lbl_ChannelName") as Label).Text;
                    pdata.Potype = (row.FindControl("lbl_ChannelName") as Label).Text;

                    pdata.BuyerName = (row.FindControl("lbl_BuyerName") as Label).Text;
                    pdata.BuyerID = int.Parse((row.FindControl("lbl_BuyerID") as Label).Text);

                    pdata.DeliveryDate = DateTime.Parse((row.FindControl("lbl_deliverydate") as Label).Text);

                    pdata.HandoverDate = DateTime.Parse((row.FindControl("lbl_HandoverDate") as Label).Text);
                    pdata.FirstDeliveryDate = DateTime.Parse((row.FindControl("lbl_FirstDeliveryDate") as Label).Text);
                    pdata.IsAcdWorld = isatcworld;



                    pdata.CategoryID = int.Parse((row.FindControl("lbl_CategoryID") as Label).Text);
                    pdata.ChannelID = int.Parse((row.FindControl("lbl_ChannelID") as Label).Text);
                    pdata.BuyerDestination_PK = int.Parse((row.FindControl("lbl_BuyerDestination_PK") as Label).Text);
                    pdata.Season_PK = int.Parse((row.FindControl("lbl_Season_PK") as Label).Text);
                    pdata.atcid = int.Parse((row.FindControl("lbl_atcid") as Label).Text);


                   
                   // pdata.AllocatePO(popackid, ourstyleid, factid);
                 pdata.AllocatePONew(popackid, ourstyleid, factid);
                //    pdata.AllocatePOinDubaionly(popackid, ourstyleid, factid);
                    
                    k++;

                }

            }
            if (k > 0)
            {
                tbl_podata.DataSource = null;
                tbl_podata.DataBind();
                String msg = " ASQ Details Added Sucessfully ";
                ArtWebApp.Controls.Messagebox.MessgeboxUpdate(Messaediv, "sucess", msg);
            }
        }

        protected void btn_showallasq_Click(object sender, EventArgs e)
        {
            BLL.MerchandsingBLL.AllocationBLL pkmstrdata = new BLL.MerchandsingBLL.AllocationBLL();

            tbl_podata.DataSource = pkmstrdata.GetPopackMasterOFAtc(int.Parse (cmb_atc.SelectedValue.ToString()));
            
            tbl_podata.DataBind();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            int k = 0;
            int atcid = int.Parse(cmb_atc.SelectedItem.Value.ToString());


            ArtWebApp.BLL.MerchandsingBLL.AllocationBLL apll = new ArtWebApp.BLL.MerchandsingBLL.AllocationBLL();

            String isatcworld = apll.IsAtcWorldAtc(atcid);
            foreach (GridViewRow row in tbl_podata.Rows)
            {





                CheckBox chkBx = (CheckBox)row.FindControl("chk_select");
                if (chkBx.Checked == true)
                {

                    ArtWebApp.BLL.MerchandsingBLL.AllocationBLL pdata = new ArtWebApp.BLL.MerchandsingBLL.AllocationBLL();

                    int ourstyleid = int.Parse((row.FindControl("lbl_ourstyleid") as Label).Text);
                    int popackid = int.Parse((row.FindControl("lbl_popackid") as Label).Text);
                    DropDownList factlist = (row.FindControl("drp_loc") as DropDownList);
                    int factid = int.Parse(factlist.SelectedValue.ToString());



                    RadioButtonList potyelist = (row.FindControl("rbt_potype") as RadioButtonList);
                    string potype = potyelist.SelectedValue;
                    pdata.Ourstyleid = ourstyleid;
                    pdata.PoPackId = popackid;

                    pdata.ASQ = (row.FindControl("lbl_asq") as Label).Text;
                    pdata.OurStyle = (row.FindControl("lbl_ourstyle") as Label).Text;
                    pdata.BuyerPO = (row.FindControl("lbl_buyerpo") as Label).Text;
                    pdata.BuyerStyle = (row.FindControl("lbl_buyerstyle") as Label).Text;
                    pdata.Atcnum = (row.FindControl("lbl_atcnum") as Label).Text;
                    pdata.Destination = (row.FindControl("lbl_destination") as Label).Text;
                    pdata.Garmentcatagory = (row.FindControl("lbl_CategoryName") as Label).Text;
                    pdata.seasonName = (row.FindControl("lbl_season") as Label).Text;
                    pdata.ChannelName = (row.FindControl("lbl_ChannelName") as Label).Text;
                    pdata.Potype = (row.FindControl("lbl_ChannelName") as Label).Text;

                    pdata.BuyerName = (row.FindControl("lbl_BuyerName") as Label).Text;
                    pdata.BuyerID = int.Parse((row.FindControl("lbl_BuyerID") as Label).Text);

                    pdata.DeliveryDate = DateTime.Parse((row.FindControl("lbl_deliverydate") as Label).Text);
                    pdata.IsAcdWorld = isatcworld;


                    pdata.CategoryID = int.Parse((row.FindControl("lbl_CategoryID") as Label).Text);
                    pdata.ChannelID = int.Parse((row.FindControl("lbl_ChannelID") as Label).Text);
                    pdata.BuyerDestination_PK = int.Parse((row.FindControl("lbl_BuyerDestination_PK") as Label).Text);
                    pdata.Season_PK = int.Parse((row.FindControl("lbl_Season_PK") as Label).Text);
                    pdata.atcid = int.Parse((row.FindControl("lbl_atcid") as Label).Text);

                    // pdata.AllocatePO(popackid, ourstyleid, factid);
                    pdata.AllocatePONewEthiopia(popackid, ourstyleid, factid);
                    //    pdata.AllocatePOinDubaionly(popackid, ourstyleid, factid);

                    k++;

                }

            }
            if (k > 0)
            {
                tbl_podata.DataSource = null;
                tbl_podata.DataBind();
                String msg = " ASQ Details Added Sucessfully ";
                ArtWebApp.Controls.Messagebox.MessgeboxUpdate(Messaediv, "sucess", msg);
            }
        }
    }
}
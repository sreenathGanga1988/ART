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
    public partial class FactoryCutPlan : System.Web.UI.Page
    {
    
       
        protected void Page_Load(object sender, EventArgs e)
        {
           // Button2.Attributes.Add("onclick", "return totalcalculation();return false;");
            if (!IsPostBack)
            {
                FillAtcCombo();

            }
            else
            {
                FillNewTable();

                try
                {
                    fillsmalltable();
                }
                catch (Exception)
                {

                }
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



    
        public void fillColorcombo(int ourstyleid , String COLORCODE)
        {


            using (ArtEntitiesnew entty = new ArtEntitiesnew())
            {

                if(COLORCODE=="CM")
                {
                    var data = entty.POPackDetails.Where(i => i.OurStyleID == ourstyleid).Select(i => new { i.ColorName, i.ColorCode }).Distinct();
                    ddl_color.DataSource = data.ToList();
                    ddl_color.DataBind();

                }
                else
                {
                    var data = entty.POPackDetails.Where(i => i.OurStyleID == ourstyleid && i.ColorCode == COLORCODE).Select(i => new { i.ColorName, i.ColorCode }).Distinct();
                    ddl_color.DataSource = data.ToList();
                    ddl_color.DataBind();
                }
             
               upd_garmentColor.Update();
            }
           
        }




      

       


       


        






    




        protected void btn_saveCutorder_Click(object sender, EventArgs e)
        {
       
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


    
        protected void btn_show_Click(object sender, EventArgs e)
        {
            cleargrid();
            FillOurStyleCombo(int.Parse(drp_Atc.SelectedValue.ToString()));
           
        }



    




       
     

        protected void btn_color_Click(object sender, EventArgs e)
        {



           

          



        }


        public void cleargrid()
        {
            try
            {
                Session["dt"] = null;
                tbl_podata.DataSource = null;
                tbl_podata.DataBind();
                updgrid.Update();
                lbl_msg.Text = "*";
            }
            catch (Exception)
            {

              
            }

        }


        public void fillsmalltable()
        {
            DataTable dt = BLL.popackupdater.createdatatable(int.Parse(drp_ourstyle.SelectedValue.ToString()));
            //  dt = BLL.CutOrderBLL.CutPlan.AddToTalQty(dt);
            GenerateSmallTable(dt);

        }

        public void fillgrid()
        {
            Session["dt"] = BLL.CutOrderBLL.CutPlan.GetPODataofColor(int.Parse(drp_ourstyle.SelectedValue.ToString()), ddl_color.SelectedValue.ToString ().Trim ());

            fillsmalltable();
          
            tbl_podata.DataSource = BLL.CutOrderBLL.CutPlan.GetPOMasterDataofColor(int.Parse(drp_ourstyle.SelectedValue.ToString()), ddl_color.SelectedValue.ToString().Trim());
            tbl_podata.DataBind();
            updgrid.Update();
        }


        public void filltable()
        {
            BLL.CutOrderBLL.CutPlanDetailsData  cddet = BLL.CutOrderBLL.CutPlan.GetRollDetails(int.Parse(drp_ourstyle.SelectedValue.ToString()),int.Parse(drp_fabcolor.SelectedValue.ToString()), drp_shrink.SelectedValue.ToString(), drp_width.SelectedValue.ToString(),drp_markerType.SelectedValue.ToString());

            lbl_rollinspected.Text = cddet.RollCount.ToString();
            lbl_ayard.Text = cddet.rollYard.ToString();
            lbl_consumption.Text = cddet.bomconsumption.ToString();
            lbl_balyard.Text = cddet.balanceyard.ToString();
            lbl_alreadycut.Text = cddet.alreadycut.ToString();

            lbl_apprQty.Text = (float.Parse(cddet.rollYard.ToString()) / float.Parse(cddet.bomconsumption.ToString())).ToString();
            upd_rolldetails.Update();
            Upd_consumption.Update();
            upd_alreadyCut.Update();
            upd_garmentDetail.Update();
        }


        public void FillCombo(int skudet_pk)
        {
            BLL.CutOrderBLL.CutOrderData cdata = new BLL.CutOrderBLL.CutOrderData();


            drp_shrink.DataSource = cdata.GetFabricShrinkage(skudet_pk);
            drp_shrink.DataTextField = "ShrinkageGroup";
            drp_shrink.DataValueField = "ShrinkageGroup";
            drp_shrink.DataBind();
           upd_shrnk.Update();

            drp_width.DataSource = cdata.GetFabricWidth(skudet_pk);
            drp_width.DataTextField = "WidthGroup";
            drp_width.DataValueField = "WidthGroup";
            drp_width.DataBind();
            upd_width.Update();


            drp_markerType.DataSource = cdata.GetFabricMarkertype(skudet_pk);
            drp_markerType.DataTextField = "MarkerType";
            drp_markerType.DataValueField = "MarkerType";
            drp_markerType.DataBind();

            upd_marker.Update();

        }






        protected void btn_markertype_Click(object sender, EventArgs e)
        {

            //tbl_marker.DataSource = BLL.CutOrderBLL.CutOrderData.CreateRollRows(int.Parse(txt_noofmarker.Text));
            //tbl_marker.DataBind();
            //upd_grid.Update();
        }

        protected void btnMarkerconfirm_Click(object sender, EventArgs e)
        {
           

        }




        private void GenerateSmallTable(DataTable dt)
        {






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
                            tb.CssClass = "Qty";
                            tb.Enabled = true;
                            tb.Width = 70;
                            dt.Rows[i][j] = dt.Rows[i][j].ToString();
                        }
                        
                        else
                        {
                            tb.CssClass = "Qty";
                            tb.Enabled = false;
                            tb.Width = 70;
                            dt.Rows[i][j] = dt.Rows[i][j].ToString();
                        }



                        //  Set a unique ID for each TextBox added
                        // tb.ReadOnly = true;
                      
                        tb.ID = "tb" + i + j;
                        tb.Text = dt.Rows[i][j].ToString();

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
                    hcell.CssClass = "HeaderCell";
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
                    tb.CssClass = "Headerlabel";
                    hcell.Controls.Add(tb);
                    //  Add the TableCell to the TableRow
                    hrow.Cells.Add(hcell);
                    hrow.CssClass = "thcss";
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
                            }else
                            {
                                tb.CssClass = "ColorTotal";
                            }
                        }
                        else if (i==dt.Rows.Count-1)
                        {
                            tb.CssClass = "Qty";
                            tb.Enabled = true;
                            tb.Width = 70;
                            dt.Rows[i][j] = dt.Rows[i][j].ToString();
                        }
                        else if (i == dt.Rows.Count - 2)
                        {
                            tb.CssClass = "BalQty";
                            tb.Enabled = false;
                            tb.Width = 70;
                            dt.Rows[i][j] = dt.Rows[i][j].ToString();
                        }
                        else
                        {
                            tb.CssClass = "Qty";
                            tb.Enabled = false;
                            tb.Width = 70;
                            dt.Rows[i][j] = dt.Rows[i][j].ToString();
                        }



                        //  Set a unique ID for each TextBox added
                        // tb.ReadOnly = true;
                        tb.Attributes.Add("onkeypress", "return isNumberKey(event,this)");
                        tb.Attributes.Add("onchange", "QtyKeyUp(this)");
                        tb.ID = "tb" + i + j;
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


        }

        protected void tbl_podata_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int ourstyleid = int.Parse((e.Row.FindControl("lbl_ourstyleid") as Label).Text);
                int popackid = int.Parse((e.Row.FindControl("lbl_popackid") as Label).Text);
                DataTable dt = BLL.popackupdater.createdatatable((DataTable)Session["dt"], popackid);
                if(dt!=null&& dt.Rows.Count>0)
                {
                    dt = BLL.CutOrderBLL.CutPlan.AddToTalQty(dt);
                    GenerateTable(dt, e.Row);
                }

               
            }

        }





        public void FillNewTable()
        {
            for (int i = 0; i < tbl_podata.Rows.Count; i++)
            {
                GridViewRow row = tbl_podata.Rows[i];
                int popackid = int.Parse((row.FindControl("lbl_popackid") as Label).Text);
                DataTable dt = BLL.popackupdater.createdatatable((DataTable)Session["dt"], popackid);
                dt = BLL.CutOrderBLL.CutPlan.AddToTalQty(dt);
                GenerateTable(dt, row);
            }
        }
        protected void tbl_podata_RowCommand(object sender, GridViewCommandEventArgs e)
        {

           




            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = tbl_podata.Rows[index];
            if (e.CommandName == "Update")
            {



                CheckBox chkBx = (CheckBox)row.FindControl("chk_select");
                if (chkBx.Checked == true)
                {

                    //int CutOrderDet_PK = int.Parse((row.FindControl("lbl_CutOrderDet_PK") as Label).Text);
                //    BLL.CutOrderBLL.CutDetailsData cddetdata = new BLL.CutOrderBLL.CutDetailsData();
                   // cddetdata.CutSizeDetailsDataCollection = 
                        GetSizedata(0);
                   // cddetdata.InsertCutOrderSizeData();
                }
            }
            else if (e.CommandName == "ShowDropDown")
            {

            }
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

        protected void btn_shostyle_Click(object sender, EventArgs e)
        {
            drp_fabcolor.DataSource = BLL.CutOrderBLL.CutPlan.fillFabColor(int.Parse(drp_ourstyle.SelectedValue.ToString()), "");
            drp_fabcolor.DataBind();
            upd_fabcolor.Update();

            cleargrid();



        }

        protected void Button1_Click1(object sender, EventArgs e)
        {
            Session["dt"] = BLL.CutOrderBLL.CutPlan.GetPODataofColor(int.Parse(drp_ourstyle.SelectedValue.ToString()), ddl_color.SelectedValue.ToString());
            tbl_podata.DataSource = BLL.CutOrderBLL.CutPlan.GetPOMasterDataofColor(int.Parse(drp_ourstyle.SelectedValue.ToString()), "");
            tbl_podata.DataBind();
            updgrid.Update();


            drp_fabcolor.DataSource = BLL.CutOrderBLL.CutPlan.fillFabColor(int.Parse(drp_ourstyle.SelectedValue.ToString()), "");
            drp_fabcolor.DataBind();
            upd_fabcolor.Update();
        }

        protected void BTN_FABRICSHOW_Click(object sender, EventArgs e)
        {
            String Colorcode = BLL.CutOrderBLL.CutPlan.getGarmentColor(int.Parse(drp_fabcolor.SelectedValue.ToString()));
            lbl_labelcode.Text = Colorcode;

            lbl_labelcode.Text = "CM";

            Session["Colorcode"] = Colorcode;
            int ourstyleid = int.Parse(drp_ourstyle.SelectedValue.ToString());
            if (Colorcode == "CM")
            {
                

            }
            else
            {
                using (ArtEntitiesnew entty = new ArtEntitiesnew())
                {
                    var colorname = entty.ColorMasters.Where(i =>  i.ColorCode == Colorcode.Trim ()).Select(i =>  i.ColorName ).FirstOrDefault();
                    lbl_garmentColor.Text = colorname.ToString();


                    var sum = entty.POPackDetails.Where(i => i.OurStyleID == ourstyleid && i.ColorCode == Colorcode.Trim() && i.IsCutable == "Y").Select(i => i.PoQty).Sum();

                    lbl_garmentQty.Text = sum.ToString();

                    upd_garmentDetail.Update();
                }
            }


            fillColorcombo(int.Parse(drp_ourstyle.SelectedValue.ToString()),Colorcode);
            FillCombo(int.Parse(drp_fabcolor.SelectedValue.ToString()));
            cleargrid();
        }

        protected void btn_factoryshow_Click(object sender, EventArgs e)
        {
            cleargrid();
        }

        protected void btn_showgrid_Click(object sender, EventArgs e)
        {
            filltable();
            
        }

        protected void btn_cutorder_Click(object sender, EventArgs e)
        {
            try
            {
               
                fillgrid();
            }
            catch (Exception ex)
            {
               
                
            }
        }


        public String InsertCutPlanMasterData()
        {
            BLL.CutOrderBLL.CutPlanMasterData cmstrdata = new BLL.CutOrderBLL.CutPlanMasterData();
            cmstrdata.OurStyleID = int.Parse(drp_ourstyle.SelectedValue.ToString());
            cmstrdata.SkuDet_PK = int.Parse(drp_fabcolor.SelectedValue.ToString());
            
            cmstrdata.ColorName = ddl_color.Text.ToString();
            cmstrdata.ColorCode = ddl_color.SelectedValue.ToString();
            cmstrdata.ShrinkageGroup = drp_shrink.Text.ToString();
            cmstrdata.WidthGroup = drp_width.Text.ToString();
            cmstrdata.AddedBy = Session["Username"].ToString().Trim();
            cmstrdata.AddedDate = DateTime.Now;
            cmstrdata.location_PK = int.Parse(drp_fact.SelectedValue.ToString());
            cmstrdata.FabDescription = drp_fabcolor.SelectedItem.Text.Trim();
            cmstrdata.MarkerType = drp_markerType.SelectedItem.Text.Trim();
            cmstrdata.BOMConsumption =Decimal.Parse ( lbl_consumption.Text.Trim());
            
            String msg = cmstrdata.InsertNewCutPlanMaster();
            //msg = msg + "  Created Sucessfully .  Add ASQ Details";
            //ArtWebApp.Controls.Messagebox.MessgeboxUpdate(Messaediv, "sucess", msg);

            return msg;
        }

        protected void Button2_Click(object sender, EventArgs e)
        {

        }

        protected void btn_asqdetails_Click(object sender, EventArgs e)
        {
            String msg = "";
            msg = InsertCutPlanMasterData();
            msg = msg + "  Created Sucessfully ";
            BLL.CutOrderBLL.CutPlanMasterData cmstrdata = new BLL.CutOrderBLL.CutPlanMasterData();
            cmstrdata.CutPlanDetailsDataCollection = GetSizedata(int.Parse(Session["CutPlan_PK"].ToString()));
            cmstrdata.InsertCutASQDetailsPlan();
          msg = msg + " and  ASQ Details Added Sucessfully Add marker Details" ;
            ArtWebApp.Controls.Messagebox.MessgeboxUpdate(Messaediv1, "sucess", msg);
            tbl_podata.DataSource = null;
            tbl_podata.DataBind();
            updgrid.Update();
        }




        public List<BLL.CutOrderBLL.CutPlanDetailsData> GetSizedata(int cutdetPK)
        {

            List<BLL.CutOrderBLL.CutPlanDetailsData> rk = new List<BLL.CutOrderBLL.CutPlanDetailsData>();

            for (int i=0;i<tbl_podata.Rows.Count; i++)
            {
                GridViewRow row = tbl_podata.Rows[i];
                CheckBox chkBx = (CheckBox)row.FindControl("chk_select");
                if (chkBx.Checked == true)
                {
                    Panel panel1 = (row.FindControl("panel1") as Panel);
                    Table Table1 = (row.FindControl("Table1") as Table);
                    int lbl_popackid = int.Parse((row.FindControl("lbl_popackid") as Label).Text);
                    int lbl_ourstyleid = int.Parse((row.FindControl("lbl_ourstyleid") as Label).Text);

                    int tablerowcount = Table1.Rows.Count;
                    //for each row in table
                    for (int tabroindex = 0; tabroindex < Table1.Rows.Count; tabroindex++)
                    {    //get row
                        TableRow tbrow = Table1.Rows[tabroindex];
                        //navigate through all the columns of row

                        if (tabroindex == Table1.Rows.Count - 1)
                        {

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

                                        if (txtqty.Text == "New Cut" || txtqty.CssClass == "GrandTotal")
                                        {

                                        }
                                        else
                                        {



                                            Control ctrlsize = Table1.Rows[0].Cells[tabcellindex].Controls[0];
                                            Label lblsize = (Label)ctrlsize;

                                            Control ctrlcolor = Table1.Rows[1].Cells[0].Controls[0];
                                            TextBox lblctrlcolor = (TextBox)ctrlcolor;

                                            if (lblsize.Text.Trim() != "Total")
                                            {

                                                BLL.CutOrderBLL.CutPlanDetailsData cutdet = new BLL.CutOrderBLL.CutPlanDetailsData();
                                                cutdet.SizeName = lblsize.Text.Trim();
                                                cutdet.ColorName = lblctrlcolor.Text.Trim();
                                                cutdet.CutQty = int.Parse(txtqty.Text);
                                                cutdet.PoPackId = lbl_popackid;
                                                cutdet.CutPlan_PK = cutdetPK;
                                                cutdet.OurStyleId = lbl_ourstyleid;
                                                cutdet.skudet_PK = int.Parse(drp_fabcolor.SelectedValue.ToString());

                                                rk.Add(cutdet);
                                            }



                                        }


                                    }
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
using ArtWebApp.Controls;
using ArtWebApp.DataModels;
using System;
using System.Collections;
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
            List<decimal?> list = Session["ApprovedLocationlist"] as List<decimal?>;
          
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
                         where order.LocType == "F" && list.Contains(order.Location_PK)
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

        public void FillAllCombo()
        {
            using (ArtEntitiesnew entty = new ArtEntitiesnew())
            {
                var bodyparts = (from ponmbr in entty.BodyPartMasters
                                 where ponmbr.IsActive == true
                        select new
                        {
                            name = ponmbr.BodyPartName,
                            pk = ponmbr.BodyPartName
                        }).ToList();
                var cuttype = (from ponmbr in entty.CutTypeMasters
                               where ponmbr.IsActive == true
                               select new
                               {
                                   name = ponmbr.CutType,
                                   pk = ponmbr.CutType
                               }).ToList();
                var markermade = (from ponmbr in entty.MarkerMadeMasters
                                  where ponmbr.IsActive == true
                                  select new
                                  {
                                      name = ponmbr.MarkerMade,
                                      pk = ponmbr.MarkerMade
                                  }).ToList();



                var markerdirection = (from ponmbr in entty.MarkerDirectionMasters
                                  where ponmbr.IsActive == true
                                  select new
                                  {
                                      name = ponmbr.MarkerDirection,
                                      pk = ponmbr.MarkerDirection
                                  }).ToList();



                drp_fabrication.DataSource = bodyparts;
                drp_fabrication.DataBind();
               

              
                drp_cuttype.DataSource = cuttype;
                drp_cuttype.DataBind();
              
             

             

                drp_markermade.DataSource = markermade;
                drp_markermade.DataBind();

             

                drp_markerdirection.DataSource = markerdirection;
                drp_markerdirection.DataBind();

                upd_markerdirection.Update();
                upd_cuttype.Update();
                upd_fabrication.Update();
                upd_markermade.Update();
            }
        }


        public void fillColorcombo(int ourstyleid, String COLORCODE)
        {


            using (ArtEntitiesnew entty = new ArtEntitiesnew())
            {

                if (COLORCODE == "CM")
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


            FillAllCombo();
        }











        protected void btn_color_Click(object sender, EventArgs e)
        {









        }


        public void cleargrid()
        {
            try
            {


                lbl_alreadycutelectedFactory.Text = "0";
                lbl_rollinspected.Text = "0";
                lbl_ayard.Text = "0";
                lbl_consumption.Text = "0";
                lbl_balyard.Text = "0";
                lbl_alreadycut.Text = "0";
                lbl_deliveredrolls.Text = "0";
                lbl_deliveredYard.Text = "0";
                lbl_balroll.Text = "0";

                lbl_baltocutlocation.Text ="0";
                lbl_totalcutplanyardage.Text = "0";
                lbl_locationcutplanyardage.Text = "0";
                lbl_apprQty.Text = "0";
                lbl_balancetodeliveryardage.Text = "0";



                Session["dt"] = null;
                tbl_podata.DataSource = null;
                tbl_podata.DataBind();
                updgrid.Update();
                upd_rolldetails.Update();
                Upd_consumption.Update();
                upd_alreadyCut.Update();
                upd_garmentDetail.Update();
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
            Session["dt"] = BLL.CutOrderBLL.CutPlan.GetPODataofColor(int.Parse(drp_ourstyle.SelectedValue.ToString()), ddl_color.SelectedValue.ToString().Trim());


            Session["Alreadycut"] = BLL.CutOrderBLL.CutPlan.GetAlreadyCutofColor(int.Parse(drp_ourstyle.SelectedValue.ToString()), ddl_color.SelectedValue.ToString().Trim(), int.Parse(drp_fabcolor.SelectedValue.ToString()));
            fillsmalltable();

            //  tbl_podata.DataSource = BLL.CutOrderBLL.CutPlan.GetPOMasterDataofColor(int.Parse(drp_ourstyle.SelectedValue.ToString()), ddl_color.SelectedValue.ToString().Trim());

            tbl_podata.DataSource = BLL.CutOrderBLL.CutPlan.GetPOMasterDataofColorandLocation(int.Parse(drp_ourstyle.SelectedValue.ToString()), ddl_color.SelectedValue.ToString().Trim(), int.Parse(drp_fact.SelectedValue.ToString()));
            tbl_podata.DataBind();
            updgrid.Update();
        }
        public void fillgridCommon()
        {
            Session["dt"] = BLL.CutOrderBLL.CutPlan.GetPODataofColor(int.Parse(drp_ourstyle.SelectedValue.ToString()), lbl_labelcode.Text.Trim());


            Session["Alreadycut"] = BLL.CutOrderBLL.CutPlan.GetAlreadyCutofColor(int.Parse(drp_ourstyle.SelectedValue.ToString()), lbl_labelcode.Text.Trim(), int.Parse(drp_fabcolor.SelectedValue.ToString()));
            fillsmalltable();

            // tbl_podata.DataSource = BLL.CutOrderBLL.CutPlan.GetPOMasterDataofColor(int.Parse(drp_ourstyle.SelectedValue.ToString()), lbl_labelcode.Text.Trim());
            tbl_podata.DataSource = BLL.CutOrderBLL.CutPlan.GetPOMasterDataofColorandLocation(int.Parse(drp_ourstyle.SelectedValue.ToString()), lbl_labelcode.Text.Trim(), int.Parse(drp_fact.SelectedValue.ToString()));
            tbl_podata.DataBind();
            updgrid.Update();
        }

        public void filltable()
        {
            int skudetpk = int.Parse(drp_fabcolor.SelectedValue.ToString());
            int ourtyleid = int.Parse(drp_ourstyle.SelectedValue.ToString());
                String shrinkagegroup = drp_shrink.SelectedValue.ToString();
            String widthgroup = drp_width.SelectedValue.ToString();
            String markergroup = drp_markerType.SelectedValue.ToString();
            int warehouselocation = int.Parse(Session["UserLoc_pk"].ToString());

            int factorylocation = int.Parse(drp_fact.SelectedValue.ToString());


            //   BLL.CutOrderBLL.CutPlanDetailsData  cddet = BLL.CutOrderBLL.CutPlan.GetRollDetails(int.Parse(drp_ourstyle.SelectedValue.ToString()),int.Parse(drp_fabcolor.SelectedValue.ToString()), drp_shrink.SelectedValue.ToString(), drp_width.SelectedValue.ToString(),drp_markerType.SelectedValue.ToString());
            BLL.CutOrderBLL.CutPlanDetailsData cddet = BLL.CutOrderBLL.CutPlan.GetRollDetailsoflocation(ourtyleid, skudetpk, shrinkagegroup, widthgroup, markergroup, warehouselocation, factorylocation);


            lbl_alreadycutelectedFactory.Text = cddet.alreadycutoflocation.ToString();

            lbl_alreadycutofgroup.Text = cddet.alreadycutforselectedfactoryofgroup.ToString();
            // lbl_rollinspected.Text = cddet.RollCount.ToString();

            lbl_rollinspected.Text = (cddet.RollCount+ cddet.DeliverdRollCount).ToString();
            lbl_ayard.Text = (cddet.rollYard+ cddet.DeliverdrollYard).ToString();
            lbl_consumption.Text = cddet.bomconsumption.ToString();
            lbl_consumptionactual.Text = cddet.bomconsumption.ToString();
            lbl_weight.Text = cddet.balWeightSum.ToString();


            lbl_uom.Text = cddet.UOMName;
         //   lbl_balyard.Text = cddet.balanceyard.ToString();
            lbl_alreadycut.Text = cddet.alreadycut.ToString();
            lbl_deliveredrolls.Text = cddet.DeliverdRollCount.ToString();
            lbl_deliveredYard.Text = cddet.DeliverdrollYard.ToString();
            lbl_balroll.Text = cddet.balRollCount.ToString();

            //    lbl_reqyardforstyle.Text= cddet.bomconsumption.ToString()*

            lbl_prevcutplanblocked.Text = cddet.CutplanBlockedRoll.ToString() + "Rolls" + " / " + cddet.CutplanBlockedYardage.ToString() +" Yards";

            if (lbl_uom.Text.Trim() == "KGS")
            {
                cddet.bomconsumption= calculateYadsConsumption();
            }

            //total yardage blocked for total cutplan made for that skudetpk irrespective of group
            float totalcutplanyardage = float.Parse(cddet.alreadycut.ToString()) * float.Parse(cddet.bomconsumption.ToString());

            float locationcutplanyardage = BLL.CutOrderBLL.CutPlan.GetCutplanfabutilisedofAGroup(skudetpk, ourtyleid, factorylocation, shrinkagegroup, widthgroup, markergroup);

        //  float locationcutplanyardage = float.Parse(cddet.alreadycutoflocation.ToString()) * float.Parse(cddet.bomconsumption.ToString());

            float balanccetocutinlocation = float.Parse(lbl_allocatedQty.Text) - float.Parse(cddet.alreadycutoflocation.ToString());


            lbl_balyard.Text = ((float.Parse(cddet.rollYard.ToString()) + float.Parse(cddet.DeliverdrollYard.ToString())) - (locationcutplanyardage + cddet.deliveredayardsumdummy)).ToString();
         
            //creates a consumption based on history
            float historyconsumption  = BLL.CutOrderBLL.CutPlan.GetConsumptionBasedonHistory(ourtyleid, factorylocation, skudetpk, balanccetocutinlocation, cddet.bomconsumption);

            // lbl_balyard.Text = (float.Parse(cddet.rollYard.ToString())  + float.Parse(cddet.DeliverdrollYard.ToString())) - locationcutplanyardage).ToString());



            lbl_historyconsumption.Text = historyconsumption.ToString();
            lbl_baltocutlocation.Text = balanccetocutinlocation.ToString();
            lbl_totalcutplanyardage.Text = totalcutplanyardage.ToString();
            lbl_locationcutplanyardage.Text = locationcutplanyardage.ToString();




            //lbl_apprQty.Text = (float.Parse(cddet.balanceyard.ToString()) / float.Parse(cddet.bomconsumption.ToString())).ToString();

          //if(lbl_uom.Text.Trim()=="KGS")
          //  {
          //      lbl_apprQty.Text = (float.Parse(lbl_weight.Text) / float.Parse(cddet.bomconsumption.ToString())).ToString();

          //  }
          //  else
          //  {
                lbl_apprQty.Text = (float.Parse(lbl_balyard.Text) / float.Parse(cddet.bomconsumption.ToString())).ToString();

            //}


            float balancetodeliveryardage = (float.Parse(lbl_locationcutplanyardage.Text.ToString()) - float.Parse(lbl_deliveredYard.Text.ToString()));

            lbl_balancetodeliveryardage.Text = balancetodeliveryardage.ToString();
            upd_rolldetails.Update();
            Upd_consumption.Update();
            upd_alreadyCut.Update();
            upd_garmentDetail.Update();

        }


        public void FillCombo(int skudet_pk)
        {
            int warehouselocation = int.Parse(Session["UserLoc_pk"].ToString());

            BLL.CutOrderBLL.CutOrderData cdata = new BLL.CutOrderBLL.CutOrderData();


            drp_shrink.DataSource = cdata.GetFabricShrinkageLocation(skudet_pk, warehouselocation);
            drp_shrink.DataTextField = "ShrinkageGroup";
            drp_shrink.DataValueField = "ShrinkageGroup";
            drp_shrink.DataBind();
            upd_shrnk.Update();

            drp_width.DataSource = cdata.GetFabricWidthLocation(skudet_pk, warehouselocation);
            drp_width.DataTextField = "WidthGroup";
            drp_width.DataValueField = "WidthGroup";
            drp_width.DataBind();
            upd_width.Update();


            drp_markerType.DataSource = cdata.GetFabricMarkertypeLocation(skudet_pk, warehouselocation);
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




        public float calculateYadsConsumption()
        {

            float consumptioninyards = (float.Parse(lbl_consumptionactual.Text) *
             float.Parse("1549996.9")) / (((float.Parse(txt_gsm.Text) * 36) * float.Parse(txt_width.Text)));
            lbl_consumption.Text = consumptioninyards.ToString();

            return consumptioninyards;

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
                DataTable datatemp = (DataTable)Session["Alreadycut"];
                DataTable alrdeaycut = new DataTable();
                alrdeaycut = null;


                try
                {
                    alrdeaycut = datatemp.Select("PoPackId=" + popackid + " and OurStyleID=" + ourstyleid + "").CopyToDataTable();
                }
                catch (Exception)
                {


                }

                DataTable dt = BLL.popackupdater.createdatatable((DataTable)Session["dt"], popackid);
                if (dt != null && dt.Rows.Count > 0)
                {
                    dt = BLL.CutOrderBLL.CutPlan.AddToTalQty(dt, alrdeaycut);
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
                int ourstyleid = int.Parse((row.FindControl("lbl_ourstyleid") as Label).Text);
                DataTable datatemp = (DataTable)Session["Alreadycut"];
                DataTable alrdeaycut = new DataTable();
                alrdeaycut = null;


                try
                {
                    alrdeaycut = datatemp.Select("PoPackId=" + popackid + " and OurStyleID=" + ourstyleid + "").CopyToDataTable();
                }
                catch (Exception)
                {


                }
                DataTable dt = BLL.popackupdater.createdatatable((DataTable)Session["dt"], popackid);
                if (dt != null && dt.Rows.Count > 0)
                {
                    dt = BLL.CutOrderBLL.CutPlan.AddToTalQty(dt, alrdeaycut);
                    GenerateTable(dt, row);
                }
                //dt = BLL.CutOrderBLL.CutPlan.AddToTalQty(dt);
                //GenerateTable(dt, row);
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
            lbl_stylename.Text = BLL.CutOrderBLL.CutPlan.getStylename(int.Parse(drp_ourstyle.SelectedValue.ToString()));

            cleargrid();

            upd_garmentDetail.Update();


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

            // lbl_labelcode.Text = "CM";
            int skudetpk = 0;
            int locationpk = 0;

            skudetpk = int.Parse(drp_fabcolor.SelectedValue.ToString());
            
            locationpk = int.Parse(Session["UserLoc_pk"].ToString());
            Session["Colorcode"] = Colorcode;
            int ourstyleid = int.Parse(drp_ourstyle.SelectedValue.ToString());
            int allocatedlocation = int.Parse(drp_fact.SelectedValue.ToString());
            lbl_skudet_pk.Text = skudetpk.ToString()+"/"+ ourstyleid.ToString();
            if (Colorcode == "CM")
            {
                lbl_garmentColor.Text = "Common";

                using (ArtEntitiesnew entty = new ArtEntitiesnew())
                {


                    var sum = entty.POPackDetails.Where(i => i.OurStyleID == ourstyleid &&  i.IsCutable == "Y").Select(i => i.PoQty).DefaultIfEmpty(0).Sum();

                    lbl_garmentQty.Text = sum.ToString();



                    try
                    {
                        var allocatedqty = entty.POPackDetails.Where(i => i.OurStyleID == ourstyleid &&  i.IsCutable == "Y" && i.PoPackMaster.ExpectedLocation_PK == allocatedlocation).Select(i => i.PoQty).DefaultIfEmpty(0).Sum();

                        lbl_allocatedQty.Text = allocatedqty.ToString();
                    }
                    catch (Exception)
                    {

                        lbl_allocatedQty.Text = "0";
                    }











                    try
                    {
                        var onhandsum = entty.InventoryMasters.Where(i => i.SkuDet_Pk == skudetpk && i.Location_PK == locationpk).Select(i => i.OnhandQty).Sum();


                        lbl_onhand.Text = onhandsum.ToString();
                    }
                    catch (Exception)
                    {

                        lbl_onhand.Text = "0";
                    }

                    upd_garmentDetail.Update();
                    upd_alreadyCut.Update();
                    upd_skudetPK.Update();

                }

            }
            else
            {
                using (ArtEntitiesnew entty = new ArtEntitiesnew())
                {
                    var colorname = entty.ColorMasters.Where(i => i.ColorCode == Colorcode.Trim()).Select(i => i.ColorName).FirstOrDefault();
                    lbl_garmentColor.Text = colorname.ToString();


                    var sum = entty.POPackDetails.Where(i => i.OurStyleID == ourstyleid && i.ColorCode == Colorcode.Trim() && i.IsCutable == "Y").Select(i => i.PoQty).DefaultIfEmpty(0).Sum();

                    lbl_garmentQty.Text = sum.ToString();



                    try
                    {
                        var allocatedqty = entty.POPackDetails.Where(i => i.OurStyleID == ourstyleid && i.ColorCode == Colorcode.Trim() && i.IsCutable == "Y" && i.PoPackMaster.ExpectedLocation_PK == allocatedlocation).Select(i => i.PoQty).DefaultIfEmpty(0).Sum();

                        lbl_allocatedQty.Text = allocatedqty.ToString();
                    }
                    catch (Exception)
                    {

                        lbl_allocatedQty.Text = "0";
                    }











                    try
                    {
                        var onhandsum = entty.InventoryMasters.Where(i => i.SkuDet_Pk == skudetpk && i.Location_PK == locationpk).Select(i => i.OnhandQty).Sum();


                        lbl_onhand.Text = onhandsum.ToString();
                    }
                    catch (Exception)
                    {

                        lbl_onhand.Text = "0";
                    }

                    upd_garmentDetail.Update();
                    upd_alreadyCut.Update();
                    upd_skudetPK.Update();
                }
            }


            fillColorcombo(int.Parse(drp_ourstyle.SelectedValue.ToString()), Colorcode);
            FillCombo(int.Parse(drp_fabcolor.SelectedValue.ToString()));
            cleargrid();
        }

        protected void btn_factoryshow_Click(object sender, EventArgs e)
        {
            cleargrid();
        }

        protected void btn_showgrid_Click(object sender, EventArgs e)
        {
            


            try
            {
                txt_width.Text = (float.Parse(drp_width.SelectedItem.Text)).ToString();

            }
            catch (Exception)
            {

               
            }
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
            cmstrdata.Fabrication = drp_fabrication.Text;
            cmstrdata.MarkerType = drp_markerType.SelectedItem.Text.Trim();
            cmstrdata.MakerMade = drp_markermade.Text;
            cmstrdata.BOMConsumption = Decimal.Parse(lbl_consumption.Text.Trim());
            cmstrdata.CutPlanMarkerTypeDataDataCollection = getmarkertype();
            cmstrdata.Maxmarkerlength = txt_maximumMarkerlength.Text;
            String msg = cmstrdata.InsertNewCutPlanMaster();
            //msg = msg + "  Created Sucessfully .  Add ASQ Details";
            //ArtWebApp.Controls.Messagebox.MessgeboxUpdate(Messaediv, "sucess", msg);

            return msg;
        }

        public String InsertCutPlanMasterDataTotal()
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
            cmstrdata.Fabrication = drp_fabrication.Text;
            cmstrdata.MarkerType = drp_markerType.SelectedItem.Text.Trim();
            cmstrdata.MakerMade = drp_markermade.Text;
            cmstrdata.CutType = drp_cuttype.Text;
            cmstrdata.BOMConsumption = Decimal.Parse(lbl_consumption.Text.Trim());
            cmstrdata.CutPlanMarkerTypeDataDataCollection = getmarkertype();
            cmstrdata.Maxmarkerlength = txt_maximumMarkerlength.Text;
            cmstrdata.CutPlanDetailsDataCollection = GetSizedata();

            String msg = "";
            if (cmstrdata.CutPlanDetailsDataCollection.Count == 0)
            {
                WebMsgBox.Show("No ASQ Selected");
            }
            else
            {
                msg = cmstrdata.InsertNewCutPlanMasterWithASQ();
            }

              
            //msg = msg + "  Created Sucessfully .  Add ASQ Details";
            //ArtWebApp.Controls.Messagebox.MessgeboxUpdate(Messaediv, "sucess", msg);

            return msg;
        }
        public List<BLL.CutOrderBLL.CutPlanMarkerTypeData> getmarkertype()
        {
            List<BLL.CutOrderBLL.CutPlanMarkerTypeData> rk = new List<BLL.CutOrderBLL.CutPlanMarkerTypeData>();
            ArrayList popaklist = new ArrayList();
            List<Infragistics.Web.UI.ListControls.DropDownItem> items = drp_markerdirection.SelectedItems;
            foreach (Infragistics.Web.UI.ListControls.DropDownItem item in items)
            {

                String markertype = item.Value.ToString();

                BLL.CutOrderBLL.CutPlanMarkerTypeData cutdet = new BLL.CutOrderBLL.CutPlanMarkerTypeData();
                cutdet.MarkerTypeName = markertype.Trim();



                rk.Add(cutdet);
            }

            return rk;
        }

        protected void Button2_Click(object sender, EventArgs e)
        {

        }

        protected void btn_asqdetails_Click(object sender, EventArgs e)
        {
            if (float.Parse(lbl_consumption.Text) != 0)
            {
                String msg = "";
                //msg = InsertCutPlanMasterData();
                //msg = msg + "  Created Sucessfully ";
                //BLL.CutOrderBLL.CutPlanMasterData cmstrdata = new BLL.CutOrderBLL.CutPlanMasterData();
                //cmstrdata.CutPlanDetailsDataCollection = GetSizedata(int.Parse(Session["CutPlan_PK"].ToString()));
                //cmstrdata.InsertCutASQDetailsPlan();


                msg = InsertCutPlanMasterDataTotal();


                msg = msg + " and  ASQ Details Added Sucessfully Add marker Details";
                ArtWebApp.Controls.Messagebox.MessgeboxUpdate(Messaediv1, "sucess", msg);
                tbl_podata.DataSource = null;
                tbl_podata.DataBind();
                updgrid.Update();
            }
        }



        public List<BLL.CutOrderBLL.CutPlanDetailsData> GetSizedata(int cutdetPK)
        {

            List<BLL.CutOrderBLL.CutPlanDetailsData> rk = new List<BLL.CutOrderBLL.CutPlanDetailsData>();

            for (int i = 0; i < tbl_podata.Rows.Count; i++)
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




        public List<BLL.CutOrderBLL.CutPlanDetailsData> GetSizedata()
        {

            List<BLL.CutOrderBLL.CutPlanDetailsData> rk = new List<BLL.CutOrderBLL.CutPlanDetailsData>();

            for (int i = 0; i < tbl_podata.Rows.Count; i++)
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


        protected void btn_cutordermnum_Click(object sender, EventArgs e)
        {
            try
            {

                fillgridCommon();
            }
            catch (Exception ex)
            {


            }
        }

        protected void lbl_deliveredrolls_Click(object sender, EventArgs e)
        {

            OpenNewWindow("~/Production/RollDisplayer.aspx");
          //  Response.Write("  <script language='javascript'> window.open('.\\Production\\RollDisplayer.aspx','','width=1020,Height=720,fullscreen=1,location=0,scrollbars=1,menubar=1,toolbar=1'); </script>");
        }


        public void OpenNewWindow(string url)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "SubWindow", String.Format("<script>window.open('{0}');</script>", url),true);

         //   ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ApprovalHistory", "window.open(MarkerDetails.aspx', '_blank');", true);


        }
    }
}
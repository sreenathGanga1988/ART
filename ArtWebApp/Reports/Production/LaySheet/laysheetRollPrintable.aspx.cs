using ArtWebApp.DataModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace ArtWebApp.Reports.Production.LaySheet
{
    public partial class laysheetRollPrintable : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                String laysheetpkstr = Request.QueryString["laysheetpk"];
                int laysheetpk = int.Parse(laysheetpkstr);
                FillLaySheetmasterDatausingEntity(laysheetpk);
                fillSizeratiodata(laysheetpk);
            }
        }





        public void FillLaySheetmasterDatausingEntity(int laysheetpk)
        {
            using (ArtEntitiesnew entty = new ArtEntitiesnew())
            {
                var q = from lymstr in entty.LaySheetRollMasters join
                        lyrolldet in entty.LaySheetRollDetails on lymstr.LaysheetRollmaster_Pk equals lyrolldet.LaysheetRollmaster_Pk
                        join cutorderdet in entty.CutOrderDetails on lyrolldet.CutOrderDet_PK equals cutorderdet.CutOrderDet_PK
                        join cutplanmarkerdet in entty.CutPlanMarkerDetails on cutorderdet.CutPlanMarkerDetails_PK equals cutplanmarkerdet.CutPlanMarkerDetails_PK
                        join cutordrmstr in entty.CutOrderMasters on cutorderdet.CutID equals cutordrmstr.CutID
                        join atcdet in entty.AtcDetails on cutordrmstr.OurStyleID equals atcdet.OurStyleID
                        join atcmstr in entty.AtcMasters on atcdet.AtcId equals atcmstr.AtcId
                        join bymstr in entty.BuyerMasters on atcmstr.Buyer_ID equals bymstr.BuyerID
                        join lctnmstr in entty.LocationMasters on cutordrmstr.ToLoc equals lctnmstr.Location_PK
                        join ctplnmst in entty.CutPlanMasters on cutplanmarkerdet.CutPlan_PK equals ctplnmst.CutPlan_PK
                        where lymstr.LaysheetRollmaster_Pk == laysheetpk
                        select new
                        {
                            lymstr.LocationSequencenum,
                            lymstr.LayRollRef,
                            lyrolldet.AddedDate,
                            lyrolldet.AddedBy,
                            lymstr.NoofPlies,

                            atcmstr.AtcNum,
                            atcdet.OurStyle,
                            atcdet.BuyerStyle,

                            lctnmstr.LocationName,
                            bymstr.BuyerName,

                            ctplnmst.CutPlanNUM,
                            cutordrmstr.Cut_NO,

                            cutplanmarkerdet.PaternMarkerName,
                            cutplanmarkerdet.MarkerLength,
                            cutplanmarkerdet.Tolerancelength,

                            cutordrmstr.Color,


                            ctplnmst.ShrinkageGroup,
                            ctplnmst.WidthGroup,
                            ctplnmst.MarkerType,


                            ctplnmst.MarkerMade,

                            ctplnmst.Fabrication

                        };

                foreach (var element in q)
                {
                    lbl_laysheetnum.Text = element.LayRollRef.ToString().Trim();
                    lbl_Rl.Text = element.LocationSequencenum.ToString().Trim();
                    lbl_manualcutnum.Text = element.LayRollRef.ToString().Trim();
                    lbl_atc.Text = element.AtcNum.ToString().Trim();
                    lbl_ourstyle.Text = element.OurStyle.ToString().Trim();
                    lbl_style.Text = element.BuyerStyle.ToString().Trim();
                    lbl_buyer.Text = element.BuyerName.ToString().Trim();
                    lbl_cutordernum.Text = element.Cut_NO.ToString().Trim();
                    lbl_cutplannum.Text = element.CutPlanNUM.ToString().Trim();
                    lbl_patternname.Text = element.PaternMarkerName.ToString().Trim();
                    lbl_markerlength.Text = (Decimal.Parse(element.Tolerancelength.ToString()) * Decimal.Parse("0.0278") + Decimal.Parse(element.MarkerLength.ToString())).ToString();

                    lbl_fabric.Text = element.Color.ToString();
                    lbl_loc.Text = element.LocationName;

                    lbl_noofplies.Text = element.NoofPlies.ToString();

                    lbl_Markertype.Text = element.MarkerType.ToString();
                    lbl_shrink.Text = element.ShrinkageGroup.ToString();
                    lbl_with.Text = element.WidthGroup.ToString();
                    lbl_fabrication.Text = element.Fabrication;



                    lbl_addedBY.Text = element.AddedBy.ToString();
                    lbl_addeddate.Text = element.AddedDate.ToString();




                    lbl_patternmode.Text = element.MarkerMade.ToString();


                }


                Upd_cutplandetails.Update();


            }
        }


        public DataTable fillSizeratiodata(int laysheetpk)
        {
            DataTable matrixdt = new DataTable();

            matrixdt.Columns.Add("Size");
            int noofliesint = 0;

            DataTable dt = DBTransaction.LaysheetTransaction.getSizeRatioofLaysheetRollPk(laysheetpk);
            using (ArtEntitiesnew entty = new ArtEntitiesnew())
            {
                var nopopplies = entty.LaySheetRollMasters.Where(i => i.LaysheetRollmaster_Pk == laysheetpk).Select(i => i.NoofPlies).Sum();
                // noofliesint = int.Parse(nopopplies.ToString());
                noofliesint = (int)Math.Round(float.Parse(nopopplies.ToString()), 0);
            }
            DataTable UniqueSizeunsorted = dt.DefaultView.ToTable(true, "Size", "Orderof");
            DataView dv = UniqueSizeunsorted.DefaultView;
            dv.Sort = "Orderof ASC";
            DataTable UniqueSize = dv.ToTable();
            for (int i = 0; i < UniqueSize.Rows.Count; i++)
            {
                matrixdt.Columns.Add(UniqueSize.Rows[i][0].ToString(), typeof(String));
            }

            DataRow row = matrixdt.NewRow();
            for (int i = 0; i < matrixdt.Columns.Count; i++)
            {
                row[i] = 0;
            }
            matrixdt.Rows.Add(row);

            DataRow row1 = matrixdt.NewRow();

            for (int i = 0; i < matrixdt.Columns.Count; i++)
            {
                row1[i] = 0;
            }

            matrixdt.Rows.Add(row1);

            DataRow row2 = matrixdt.NewRow();

            for (int i = 0; i < matrixdt.Columns.Count; i++)
            {
                row2[i] = 0;
            }

            matrixdt.Rows.Add(row2);
            // Add ratio
            matrixdt.Rows[0][0] = "MarkerQty";
            matrixdt.Rows[1][0] = "Ratio";
            matrixdt.Rows[2][0] = "Qty";

            for (int i = 1; i < matrixdt.Columns.Count; i++)
            {
                String Sizename = matrixdt.Columns[i].ColumnName.ToString();
                try
                {
                    object SumofcutQty = dt.Compute("Sum(Qty)", "Size= '" + Sizename + "' ");
                    if (SumofcutQty.ToString().Trim() == "")
                    {
                        SumofcutQty = "0";
                    }

                    matrixdt.Rows[0][i] = SumofcutQty.ToString();
                }
                catch (Exception)
                {

                    object SumofcutQty = 0;

                    matrixdt.Rows[0][i] = SumofcutQty.ToString();
                }
                try
                {
                    object Sumtrim = dt.Compute("Sum(Ratio)", "Size= '" + Sizename + "' ");

                    matrixdt.Rows[1][i] = Sumtrim.ToString();
                }
                catch (Exception)
                {

                    matrixdt.Rows[1][i] = "0";
                }

                try
                {
                    int layedqty = int.Parse(matrixdt.Rows[1][i].ToString()) * noofliesint;

                    matrixdt.Rows[2][i] = layedqty.ToString();
                }
                catch (Exception)
                {

                    matrixdt.Rows[2][i] = "0";
                }

            }
            matrixdt.Columns.Add("Total", typeof(String));
            matrixdt = ArtWebApp.Controls.DataTableFunction.SumOfDataColumns(1, matrixdt.Columns.Count - 2, 0, matrixdt.Rows.Count - 1, matrixdt.Columns.Count - 1, matrixdt.Rows.Count - 1, matrixdt);


            GenerateSmallTable(matrixdt);


            return matrixdt;

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
                            tb.CssClass = "BalQty";
                            tb.Enabled = true;
                            tb.Width = 70;
                            dt.Rows[i][j] = dt.Rows[i][j].ToString();
                        }

                        else if (i == dt.Rows.Count - 2)
                        {
                            tb.CssClass = "NewQty";
                            tb.Enabled = false;
                            tb.Width = 70;
                            dt.Rows[i][j] = dt.Rows[i][j].ToString();
                            tb.Attributes.Add("onchange", "Calculatebalance()");
                        }
                        else if (i == dt.Rows.Count - 3)
                        {
                            tb.CssClass = "AvailQty";
                            tb.Enabled = false;
                            tb.Width = 70;
                            dt.Rows[i][j] = dt.Rows[i][j].ToString();
                            tb.Attributes.Add("onchange", "Calculatebalance()");
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
            Table1.Enabled = false;

        }

    }
}
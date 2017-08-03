using ArtWebApp.BLL.MerchandsingBLL;
using ArtWebApp.DataModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArtWebApp.Production.CutOrder
{
    public partial class CutPlanRollMapping : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillAtcCombo();

            }
            else
            {
               
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



        protected void btn_atc_Click(object sender, EventArgs e)
        {
            FillOurStyleCombo(int.Parse(drp_atc.SelectedValue.ToString()));
        }

        public void FillAllcutorder(int ourstyleid)
        {


            using (ArtEntitiesnew entty = new ArtEntitiesnew())
            {
                var q = (from ponmbr in entty.CutPlanMasters
                        where ponmbr.OurStyleID == ourstyleid 
                         select new
                        {
                            name = ponmbr.CutPlanNUM,

                           
                            pk = ponmbr.CutPlan_PK
                        }).ToList();


                drp_cutorder.DataSource = q;
                drp_cutorder.DataBind();
                upd_cutorder.Update();



            }

        }


        public void FillAllcutplandetails(int cutplan_pk)
        {


            using (ArtEntitiesnew entty = new ArtEntitiesnew())
            {
                var q = (from ponmbr in entty.CutPlanMasters
                        where ponmbr.CutPlan_PK == cutplan_pk
                        select new
                        {
                            Shrinkagegroup = ponmbr.ShrinkageGroup,

                           
                            Markertype= ponmbr.MarkerType,

                            widthgroup= ponmbr.WidthGroup,

                            Skudetpk=ponmbr.SkuDet_PK
                        }).ToList();


              foreach(var element in q)
              {


                    lbl_shringagegroup.Text = element.Shrinkagegroup;
                    lbl_markerType.Text = element.Markertype; 
                    lbl_widthgroup.Text = element.widthgroup;
                    lbl_skudet_pk.Text = element.Skudetpk.ToString ();
                }


            }

        }



        protected void btn_OURSTYLE_Click(object sender, EventArgs e)
        {


            //  fillsizedata();
            FillAllcutorder(int.Parse(drp_ourstyle.SelectedValue.ToString().ToString()));

        }
     



        public void fillroll()
        {

            int cutplanpk = int.Parse(drp_cutorder.SelectedValue.ToString());
            DataTable dt = BLL.InventoryBLL.RollTransactionBLL.getFabricRollAvailableforCutPLan(cutplanpk, int.Parse(Session["UserLoc_pk"].ToString()));

           ;

            if (dt.Rows.Count > 0)
            {
                DataView view = new DataView(dt);
                DataTable shadetable = view.ToTable(true, "ShadeGroup");
                drp_shade.DataSource = shadetable;

                drp_shade.DataBind();


                ViewState["rolldatafordo"] = null;


                ViewState["rolldatafordo"] = dt;



                if (dt.Rows.Count > 0)
                {
                    String shrinkagegrpe = dt.Rows[0]["ShrinkageGroup"].ToString();
                    String WidthGroup = dt.Rows[0]["WidthGroup"].ToString();
                    String MarkerType = dt.Rows[0]["MarkerType"].ToString();


                    lbl_shringagegroup.Text = shrinkagegrpe;
                    lbl_markerType.Text = MarkerType;
                    lbl_widthgroup.Text = WidthGroup;
                }



                tbl_rolldata.DataSource = dt;
                tbl_rolldata.DataBind();
               
                upd_shade.Update();
            
            }
            else
            {
               


                String msg = " No New  Roll Data Found for Adding ";

                ArtWebApp.Controls.Messagebox.MessgeboxUpdate(Messaediv, "error", msg);
                tbl_rolldata.DataSource = null;
                tbl_rolldata.DataBind();
            }
            Upd_roll.Update();
        }
   





  




        protected void tbl_cutorderdata_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void btn_cutorder_Click(object sender, EventArgs e)
        {
            fillroll();
            txt_alreadyAdded.Text = BLL.CutOrderBLL.CutPlan.getAlreadyAllocatedAyardage(int.Parse(drp_cutorder.SelectedValue.ToString())).ToString();

            String fabreq = BLL.CutOrderBLL.CutPlan.GetCutFabreq(int.Parse(drp_cutorder.SelectedValue.ToString())).ToString ();

            try
            {
                lbl_baltoadd.Text = (float.Parse(fabreq) - float.Parse(txt_alreadyAdded.Text)).ToString();
            }
            catch (Exception)
            {
                lbl_baltoadd.Text = "0";

            }

            txt_fabreq.Text = fabreq;
            upd_fabreq.Update();
            AlreadyAddedRoll.DataBind();
            GridView1.DataSource = AlreadyAddedRoll;
            GridView1.DataBind();
            UPD_ALREADYADDED.Update();
            upd_baltoadd.Update();
            upd_alreadyaddedqty.Update();
            FillAllcutplandetails(int.Parse(drp_cutorder.SelectedValue.ToString()));
        }

        protected void Button1_Click3(object sender, EventArgs e)
        {
            ArrayList rollarray = new ArrayList();
            foreach (GridViewRow di in tbl_rolldata.Rows)
            {
                CheckBox chkBx = (CheckBox)di.FindControl("chk_select");

                if (chkBx != null && chkBx.Checked)
                {


                    int roll_PK = int.Parse(((di.FindControl("lbl_Roll_PK") as Label).Text.ToString()));



                    rollarray.Add(roll_PK);


                }
            }


            if(rollarray.Count>0)
            {
                BLL.CutOrderBLL.CutPlanMarkerDetailsData cdetdata = new BLL.CutOrderBLL.CutPlanMarkerDetailsData();
                cdetdata.AddCutplanRoll(rollarray, int.Parse(drp_cutorder.SelectedValue.ToString()));
                tbl_rolldata.DataSource = null;
                tbl_rolldata.DataBind();
                Upd_roll.Update();
            }
        }

        protected void Button1_Click1(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)ViewState["rolldatafordo"];


            ArrayList shadegroup = new ArrayList();
            List<Infragistics.Web.UI.ListControls.DropDownItem> items = drp_shade.SelectedItems;
            foreach (Infragistics.Web.UI.ListControls.DropDownItem item in items)
            {

                String popackid = item.Value.ToString();
                shadegroup.Add(popackid);
            }


            if (shadegroup.Count > 0 && shadegroup != null)
            {
                string condition = "";
                for (int i = 0; i < shadegroup.Count; i++)
                {



                    if (i == 0)
                    {
                        condition = condition + " ShadeGroup='" + shadegroup[i].ToString().Trim() + "'";
                    }
                    else
                    {
                        condition = condition + "  or  ShadeGroup='" + shadegroup[i].ToString().Trim() + "'";
                    }



                }
                dt = dt.Select(condition).CopyToDataTable();

            }



            tbl_rolldata.DataSource = dt;
            tbl_rolldata.DataBind();
        }
        int totalvalue = 0;
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //Check if the current row is datarow or not
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //Add the value of column
                totalvalue += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "AYard"));
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                //Find the control label in footer 
                Label lblamount = (Label)e.Row.FindControl("lblTotalValue");
                //Assign the total value to footer label control
                lblamount.Text = "Total Qty is : " + totalvalue.ToString();
            }
        }



        [WebMethod]
        public static string DeleteCutplanAysnc(int Planid)
        {

            BLL.CutOrderBLL.CutPlanMarkerDetailsData cdetdata = new BLL.CutOrderBLL.CutPlanMarkerDetailsData();
          
            return cdetdata.DeleteCutplanRoll(Planid); ;
        }







    }
}
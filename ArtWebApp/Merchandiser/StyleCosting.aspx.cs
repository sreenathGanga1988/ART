using ArtWebApp.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ArtWebApp.DBTransaction;
using ArtWebApp.BLL;
using System.Data;
using Infragistics.Web.UI.ListControls;
using System.Collections;
using System.Web.Services;

namespace ArtWebApp.Merchandiser
{
    public partial class StyleCosting : System.Web.UI.Page
    {

        BLL.CostingBLL.StyleCostingMaster csmstr = null;
        BLL.CostingBLL.StyleCostingDetails csdet = null;
        BLL.CostingBLL.StyleCostingComponentDetails cscompdet = null;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void WebDropDown1_SelectionChanged(object sender, Infragistics.Web.UI.ListControls.DropDownSelectionChangedEventArgs e)
        {
          
        }

       




  




     
        protected void tbl_costing_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                String Isreq = (e.Row.FindControl("lbl_isrequired") as Label).Text;
                CheckBox chk_iscd = (e.Row.FindControl("chk_isrequired") as CheckBox);

                if (Isreq.Trim() == "Y")
                {
                    chk_iscd.Checked = true;
                }
                else
                {
                    chk_iscd.Checked = false;
                }
            }
            catch (Exception){ }
            try
            {

                string Consupt = (e.Row.FindControl("lbl_consumption") as Label).Text;
                TextBox textconsupt = (e.Row.FindControl("txt_consumption") as TextBox);


                textconsupt.Text = Consupt.ToString();
            }
            catch (Exception)
            {


            }
        }



        protected void txt_consumption_TextChanged(object sender, EventArgs e)
        {
            
            try
            {
                TextBox txtcons = (TextBox)sender;
               
                var  currentRow = (GridViewRow)txtcons.Parent.Parent;
              
                calculateperdozen(currentRow);

            }
            catch (Exception)
            {


            }


        }

        protected void txt_rate_TextChanged(object sender, EventArgs e)
        {
            try
            {
                TextBox txtcons = (TextBox)sender;
                GridViewRow currentRow = (GridViewRow)txtcons.Parent.Parent;

                calculateperdozen(currentRow);

            }
            catch (Exception)
            {


            }
        }

     

        protected void btn_showRawmaterial_Click(object sender, EventArgs e)
        {
            BLL.CostingBLL.StyleCostingMaster scmstr = new BLL.CostingBLL.StyleCostingMaster();
            int oldcostingid = scmstr.getlastcostingid(int.Parse(ddl_ourstyle.SelectedValue.ToString()));

            if (oldcostingid != 0)
            {
                fillQtyandFOB(int.Parse(ddl_ourstyle.SelectedValue.ToString()), oldcostingid);
            }
            else
            {
                fillQtyandFOB(int.Parse(ddl_ourstyle.SelectedValue.ToString()));
            }
            
            fillmandatorycomponent(oldcostingid);
            fillOptionalComponentsOfCosting(oldcostingid);
            fillOptionalComponents();
            LoadCostingGrid();
        }
 
        protected void btn_InsertCosting_Click(object sender, EventArgs e)
        {

            if(int.Parse (lbl_projqty.Text)>0)
            {
                int atcid = int.Parse(ddl_atc.SelectedValue.ToString());
                int ourstyleid = int.Parse(ddl_ourstyle.SelectedValue.ToString());
                InsertCosting(ourstyleid, atcid);
                lbl_cost.Text = Session["CostingID"].ToString();
                upd_mandatorypandel.Update();
                lbl_message.Text = "Costing #" + Session["CostingID"].ToString() + " Is Created Sucessfully";

                lbl_secondaryComponentmsg.Text = "";
                lbl_primaryComponentmsg.Text = "";
            }
            else
            {
                String Msg = " Cannot Save Costing with Zero projection Qty";
                lbl_message.Text = Msg;
                ClientScript.RegisterStartupScript(this.GetType(), "Art", "alert('" + Msg + "');", true);
            }
           
        }

        public void MessageBoxShow(String Msg)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "Art", "alert('" + Msg + "');", true);


        }

      


        public void GetSelectedOptionalComponent(List<DropDownItem> items)
        {

          


            DataTable table = new DataTable();
            table.Columns.Add("CostComp_Pk", typeof(int));
            table.Columns.Add("ComponentName", typeof(string));
            table.Columns.Add("CompValue", typeof(int));



            //foreach (DropDownItem item in items)
            //{
            //    if (tbl_OptionalComponent.Rows.Count>0)
            //    {
            //        for (int i = 0; i < tbl_OptionalComponent.Rows.Count; i++)
            //        {
            //            int costingcomppk = int.Parse((tbl_OptionalComponent.Rows[i].FindControl("lbl_optcostcomp_pk") as Label).Text);
            //            if (item.Text == costingcomppk.ToString ())
            //            { 

            //            }
            //            else
            //            {
            //                table.Rows.Add(int.Parse(item.Value.ToString()), item.Text.ToString(), 0);
            //            }
            //        }

            //    }
            //    else
            //    {
            //        table.Rows.Add(int.Parse(item.Value.ToString()), item.Text.ToString(), 0);
            //    }
               


                
           
            foreach (DropDownItem item in items)
            {
            table.Rows.Add(int.Parse(item.Value.ToString()), item.Text.ToString(), 0);
        }
            tbl_OptionalComponent.DataSource = table;
            tbl_OptionalComponent.DataBind();
            udp_optionalGrid.Update();

        }

        protected void btn_addOptionalData_Click(object sender, EventArgs e)
        {
            List<DropDownItem> items = drp_optionalcomb.SelectedItems;
            GetSelectedOptionalComponent(items);
        }

        protected void btn_addprimaryComponent_Click(object sender, EventArgs e)
        {
            AddPrimaryComponent();
            lbl_primaryComponentmsg.Text = "Mandatory Components Updated";
        }
        protected void btn_secondarycomponent_Click(object sender, EventArgs e)
        {
            AddOptionalComponent();
            lbl_secondaryComponentmsg.Text = "Optional Components Updated";
        }

        public void AddPrimaryComponent()
        {
            csmstr = new BLL.CostingBLL.StyleCostingMaster();
            csmstr.Costing_PK = int.Parse(Session["CostingID"].ToString());

            DataTable table = new DataTable();
            table.Columns.Add("CostComp_Pk", typeof(int));
            table.Columns.Add("CalculationMode", typeof(string));
            table.Columns.Add("CompValue", typeof(decimal));

            for (int i = 0; i < tbl_manadatorycomponent.Rows.Count; i++)
            {
                int costingcomppk = int.Parse((tbl_manadatorycomponent.Rows[i].FindControl("lbl_costcomp_pk") as Label).Text);
                String Calcmode = (tbl_manadatorycomponent.Rows[i].FindControl("lbl_calcmode") as Label).Text.Trim();
                Decimal compvalue = Decimal.Parse((tbl_manadatorycomponent.Rows[i].FindControl("txt_compvalue") as TextBox).Text);
                table.Rows.Add(costingcomppk, Calcmode, compvalue);
            }

            csmstr.Stylecombdata = table;
            csmstr.InsertCostingComponent(csmstr);

        }


        public void AddOptionalComponent()
        {
            csmstr = new BLL.CostingBLL.StyleCostingMaster();
            csmstr.Costing_PK = int.Parse(Session["CostingID"].ToString());
           



            DataTable table = new DataTable();
            table.Columns.Add("CostComp_Pk", typeof(int));
            table.Columns.Add("CalculationMode", typeof(string));
            table.Columns.Add("CompValue", typeof(decimal));

            for (int i = 0; i < tbl_OptionalComponent.Rows.Count; i++)
            {
                DropDownList calcmodedrp = tbl_OptionalComponent.Rows[i].FindControl("drp_optcalcmode") as DropDownList;

                int costingcomppk = int.Parse((tbl_OptionalComponent.Rows[i].FindControl("lbl_optcostcomp_pk") as Label).Text);
                String Calcmode = calcmodedrp.SelectedItem.Text.Trim();
                Decimal compvalue = Decimal.Parse((tbl_OptionalComponent.Rows[i].FindControl("txt_optcompvalue") as TextBox).Text);
                table.Rows.Add(costingcomppk, Calcmode, compvalue);
            }

            csmstr.Stylecombdata = table;
            csmstr.InsertCostingComponent(csmstr);

        }



        /// <summary>
        /// Load grid for Atc
        /// </summary>
        public void LoadCostingGrid()
        {                   
            
            CostingTransaction csttrans = new CostingTransaction();
            DataTable dt = csttrans.GetCostingDetails(int.Parse(ddl_atc.SelectedValue.ToString()), int.Parse(ddl_ourstyle.SelectedValue.ToString()));
            tbl_costing.DataSource = dt;
            tbl_costing.DataBind();
            upd_gridpanel.Update();      

           

        }




        public void fillmandatorycomponent(int oldcostingid)
        {
            CostingTransaction csttrans = new CostingTransaction();
            DataTable dtcomp = csttrans.GetManadatoryCostingComponents(oldcostingid);

            tbl_manadatorycomponent.DataSource = dtcomp;
            tbl_manadatorycomponent.DataBind();
            upd_mandatorypandel.Update();
        }


        public void fillOptionalComponentsOfCosting(int oldcostingid )
        {
            CostingTransaction csttrans = new CostingTransaction();
            DataTable dtoptcomb = csttrans.GetOptionalCostingComponents(oldcostingid);
            tbl_OptionalComponent.DataSource = dtoptcomb;
            tbl_OptionalComponent.DataBind();
            udp_optionalGrid.Update();
        }

        public void fillOptionalComponents()
        {
            CostingTransaction csttrans = new CostingTransaction();
            DataTable dtoptcomb = csttrans.GetOptionalCostingComponents();
            drp_optionalcomb.DataSource = dtoptcomb;
            udp_optionalcombo.Update();
        }
        /// <summary>
        /// Insert Costing data
        /// </summary>
        public void InsertCosting(int ourstyleid, int atcid)
        {

            csmstr = new BLL.CostingBLL.StyleCostingMaster();
            cscompdet = new BLL.CostingBLL.StyleCostingComponentDetails();


            CostingTransaction csttrans = new CostingTransaction();
            
            //Insert Costingid
            int costingid = csmstr.InsertCosting(ourstyleid, atcid);
            
            //Insert Costing details
            csttrans.insertstylecostingdetails(this.tbl_costing, costingid);
            
          

            // Insert Components
            cscompdet.InsertMandatoryComponentvalue(costingid, ourstyleid);
            
            
            Session["CostingID"] = costingid;



            //Fill the Mandatory Components
            fillmandatorycomponent(costingid);
           
            //ArrayList fabcost = csttrans.GetFabTrimCost(costingid);
            //fillMandatoryComponentvalue(fabcost);
            lbl_cost.Text = "Enter Costing Components for" + costingid.ToString();
            MessageBoxShow(" Costing #  " + costingid.ToString() + " Created Against " + ddl_ourstyle.SelectedItem.Text.Trim());
        }
        //calculateperdozenprice
        public void calculateperdozen(GridViewRow currentRow)
        {
            TextBox rate = (currentRow.FindControl("txt_rate") as TextBox);
            TextBox txtcons = (currentRow.FindControl("txt_consumption") as TextBox);
            Label pcperdozen = (currentRow.FindControl("lbl_pcDzn") as Label);
            Label pcperpc = (currentRow.FindControl("lbl_pcpr") as Label); 
            
            float perpc= float.Parse(txtcons.Text) * float.Parse(rate.Text);
            float cons = perpc * 12;

   
            pcperdozen.Text = cons.ToString();
            pcperpc.Text = perpc.ToString();
        }

      
        public   String RefreshAll()
        {
            String msg = "Notupdated";
            foreach (GridViewRow currentRow in tbl_costing.Rows)
            {
                calculateperdozen(currentRow);
                msg = "Updated";
            }

            return msg;
        }

        /// <summary>
        /// this is function for filling the cm value if the current cm valu is null or zero
        /// </summary>
        /// <param name="oldcmvalue"></param>
        /// <returns></returns>
        public String FILLCMVALUE(string oldcmvalue)
        {
            string textvalue = "0";
            if (tbl_manadatorycomponent.Rows.Count > 0)
            {
                for (int i = 0; i < tbl_manadatorycomponent.Rows.Count; i++)
                {

                    Label calcname = tbl_manadatorycomponent.Rows[i].FindControl("lbl_ComponentName") as Label;
                    TextBox txtcomvalue = tbl_manadatorycomponent.Rows[i].FindControl("txt_compvalue") as TextBox;
                    if (calcname.Text.Trim() == "CM")
                    {
                        if (float.Parse(txtcomvalue.Text) == 0)
                        {
                            txtcomvalue.Text = oldcmvalue;
                        }
                    }
                }
            }
            return textvalue;
        }

        /// <summary>
        /// this is function to get the old cm value
        /// </summary>
        /// <returns></returns>
        public String getoldcmvalucmvalue()
        {
            string textvalue = "0";
            if (tbl_manadatorycomponent.Rows.Count > 0)
            {
                for (int i = 0; i < tbl_manadatorycomponent.Rows.Count; i++)
                {

                    Label calcname = tbl_manadatorycomponent.Rows[i].FindControl("lbl_ComponentName") as Label;
                    TextBox txtcomvalue = tbl_manadatorycomponent.Rows[i].FindControl("txt_compvalue") as TextBox;
                    if (calcname.Text.Trim() == "CM")
                    {
                        textvalue = txtcomvalue.Text;
                    }
                }
            }
            return textvalue;
        }
        public void fillMandatoryComponentvalue(ArrayList asd)
        {
            if (tbl_manadatorycomponent.Rows.Count>0)
            {
                for (int i = 0; i < tbl_manadatorycomponent.Rows.Count; i++)
                {

                    Label calcname = tbl_manadatorycomponent.Rows[i].FindControl("lbl_ComponentName") as Label;
                    TextBox txtcomvalue = tbl_manadatorycomponent.Rows[i].FindControl("txt_compvalue") as TextBox;
                    if (calcname.Text.Trim() == "FAB")
                    {
                        txtcomvalue.Text = (float.Parse(asd[0].ToString()) / 12).ToString();
                    }
                    else if (calcname.Text.Trim() == "TRIMS")
                    {
                        txtcomvalue.Text = (float.Parse(asd[1].ToString()) / 12).ToString();
                    }
                }

            }
           
        }









        /// <summary>
        /// fill style fob and qty
        /// </summary>
        /// <param name="ourstyleid"></param>
        public void fillQtyandFOB(int ourstyleid)
        {
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {


                // get the ourstylids fob
                var fobvalue = enty.AtcDetails.Where(u => u.OurStyleID == ourstyleid).Select(u => u.FOB).FirstOrDefault();
                try
                {
                    var styleqty = enty.POPackDetails.Where(u => u.OurStyleID == ourstyleid).Select(u => u.PoQty ?? 0).Sum();
                    lbl_styleqty.Text = styleqty.ToString();
                }
                catch (Exception)
                {
                    lbl_styleqty.Text = "0";

                }

                try
                {
                    var projqty = enty.AtcDetails.Where(u => u.OurStyleID == ourstyleid).Select(u => u.Quantity).FirstOrDefault();
                    lbl_projqty.Text = projqty.ToString();

                }
                catch (Exception)
                {
                    String Msg = " Cannot Create Costing For Style With Zero Projection Qty";

                    ClientScript.RegisterStartupScript(this.GetType(), "Art", "alert('" + Msg + "');", true);
                   
                }

                lbl_stylefob.Text = fobvalue.ToString();












            }

            upd_basic.Update();
        }




     





        public void fillQtyandFOB(int ourstyleid,int costingid)
        {
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {

                var costingdata = from scm in enty.StyleCostingMasters
                                  where scm.Costing_PK == costingid
                                  select new { scm.FOB, scm.Margin, scm.MarginValue, scm.TotalCost };

                foreach(var element in costingdata)
                {
                    lbl_stylefob.Text = element.FOB== null ? "0.0" : element.FOB.ToString();
                    lbl_stylemargin.Text = element.Margin == null ? "0.0" : element.Margin.ToString();
                    lbl_styletotalcost.Text = element.TotalCost == null ? "0.0" : element.TotalCost.ToString();
                }
                try
                {
                    var styleqty = enty.POPackDetails.Where(u => u.OurStyleID == ourstyleid).Select(u => u.PoQty ?? 0).Sum();
                    lbl_styleqty.Text = styleqty.ToString();
                }
                catch (Exception)
                {
                    lbl_styleqty.Text = "0";

                }
                try
                {
                    var projqty = enty.AtcDetails.Where(u => u.OurStyleID == ourstyleid).Select(u => u.Quantity).FirstOrDefault();
                    lbl_projqty.Text = projqty.ToString();

                }
                catch (Exception)
                {

                    String Msg = " Cannot Create Costing For Style With Zero Projection Qty";

                    ClientScript.RegisterStartupScript(this.GetType(), "Art", "alert('" + Msg + "');", true);
                }
                upd_basic.Update();
            }
        }








        protected void Button2_Click(object sender, EventArgs e)
        {
            try
            {
                hdf_atcid.Value = ddl_atc.SelectedValue.ToString();
                ddl_ourstyle.DataBind();
            }
            catch (Exception)
            {


            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Reports/StyleCostingHtmlReport.aspx?costingid=" + Session["CostingID"].ToString());
        }

        //protected void CheckBox2_CheckedChanged(object sender, EventArgs e)
        //{
        //    selectall();
        //}


        //public void selectall()
        //{
        //    if (chk_selectAll.Checked == true)
        //    {
        //        foreach (GridViewRow di in tbl_costing.Rows)
        //        {
        //            CheckBox chkBx = (CheckBox)di.FindControl("chk_isrequired");
        //            chkBx.Checked = true;

        //        }
        //    }
        //    else
        //    {
        //        foreach (GridViewRow di in tbl_costing.Rows)
        //        {
        //            CheckBox chkBx = (CheckBox)di.FindControl("chk_isrequired");
        //            chkBx.Checked = false;

        //        }
        //    }
        //    upd_gridpanel.Update();
        //}

        protected void ddl_ourstyle_SelectionChanged(object sender, DropDownSelectionChangedEventArgs e)
        {

        }

        protected void btn_refresh_Click(object sender, EventArgs e)
        {
            RefreshAll();
        }
    }
}
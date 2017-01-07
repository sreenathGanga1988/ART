using ArtWebApp.DataModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArtWebApp.Merchandiser
{
    public partial class AtcEdit : System.Web.UI.Page
    {
        DBTransaction.AtcTransaction atctran = new DBTransaction.AtcTransaction();
        BLL.ourstyleData ourstyledata = null;
        BLL.AtcData atcdata = null;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

      
        protected void Btn_atc_Click(object sender, EventArgs e)
        {
           
            fillcontrols(int.Parse(cmb_atc.SelectedValue.ToString()));
         
          
        }

        public void fillcontrols(int atcid)
        {
            FillAtcmasterdata(atcid);
            fillOurStyleData(atcid);
        }

        

        public void FillAtcmasterdata(int atcid)
        {
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {

                var q = from atcmstr in enty.AtcMasters
                        where atcmstr.AtcId == atcid
                        select atcmstr;

                foreach (var element in q)
                {
                    txt_merchandiser.Text = element.MerchandiserName.ToString().Trim();
                    dtp_finishdate.Value = DateTime.Parse(element.FinishDate.ToString());
                    dtp_housedate.Value = DateTime.Parse(element.HouseDate.ToString());
                    dtp_shipStartdate.Value = DateTime.Parse(element.ShipDate.ToString());
                    cmb_country.SelectedValue = element.Country_ID.ToString();
                    txt_stylenum.Text = element.NoofStyles.ToString();
                    lbl_stylenum.Text = element.NoofStyles.ToString();
                    Session["BuyerId"] = element.Buyer_ID;
                }



            }
        }



        public Boolean validationControl()
        {
            Boolean Success = false;
            if(int.Parse(lbl_stylenum.Text)>int.Parse(txt_stylenum.Text))
            {
                MessageBoxShow("Cannot Reduce the Number of Style");
            }
            else
            {
                Success = true;
            }
            return Success;
        }

        public void MessageBoxShow(String Msg)
        {    


         
            ClientScript.RegisterStartupScript(this.GetType(), "Art", "alert('" + Msg + "');", true);
        }
        
        //fill ourstyle data
        public void fillOurStyleData(int atcid)
        {
            tbl_atcdetail.DataSource = atctran.GetOurStyleDetails(atcid);
            tbl_atcdetail.DataBind();
            
        }
        protected void Btn_addetails_Click(object sender, EventArgs e)
        {
            UpdateOurstyledata();
           
        }


        public void UpdateOurstyledata()
        {
            foreach (GridViewRow row in tbl_atcdetail.Rows)
            {
                ourstyledata = new BLL.ourstyleData();
                ourstyledata.Ourstyleid = int.Parse(row.Cells[0].Text.ToString());
                ourstyledata.Atcid = int.Parse(row.Cells[1].Text.ToString());

                ourstyledata.Ourstylenum = row.Cells[2].Text.ToString();


                DropDownList buyerstyledrp = (DropDownList)row.Cells[0]
                                                         .FindControl("ddl_Buyerstyle");
                DropDownList garmentcategory = (DropDownList)row.Cells[0]
                                                       .FindControl("ddl_catid");
                TextBox txtqty = (TextBox)row.Cells[0].FindControl("txtQty");
                TextBox fobtxt = (TextBox)row.Cells[0].FindControl("txtfob");


                ourstyledata.BuyerStyle1 = buyerstyledrp.SelectedItem.Text;

                String Garmentcategory = garmentcategory.SelectedItem.Text; ;

                ourstyledata.Catid = int.Parse(garmentcategory.SelectedValue.ToString());
               
                ourstyledata.Fob = decimal.Parse(fobtxt.Text);

               Boolean isfobchanged= ourstyledata.UpdateOurStyle(ourstyledata);

              
               
            }
            ArtWebApp.Controls.Messagebox.MessgeboxUpdate(Messaediv, "sucess", "Our Style Details are updated Please revise the costings to avoid Wrong BOM");
            MessageBoxShow("Our Style Details Updated");
        }
        

        protected void Button1_Click(object sender, EventArgs e)
        {
            fillcontrols(int.Parse(cmb_atc.SelectedValue.ToString()));
        }

        
        protected void btn_update_Click(object sender, EventArgs e)
        {
            if(validationControl())
            {
                atcdata = new BLL.AtcData();
                atcdata.Atcid = int.Parse(cmb_atc.SelectedValue.ToString());
                atcdata.Countryid= int.Parse(cmb_country.SelectedValue.ToString());
                atcdata.AtcNum1 = cmb_atc.SelectedItem.Text.Trim();
                atcdata.Merchandisername = txt_merchandiser.Text.Trim();
                atcdata.Finshdate1 = DateTime.Parse(dtp_finishdate.Value.ToString());
                atcdata.InHouseDate = DateTime.Parse(dtp_housedate.Value.ToString());
                atcdata.ShipDate1 = DateTime.Parse(dtp_shipStartdate.Value.ToString());
                atcdata.Noofstyles= int.Parse(txt_stylenum.Text.ToString());
                atcdata. UpdatemasterData(atcdata);
                atcdata.createOurStyle(atcdata);
                MessageBoxShow(" Details Updated for " + atcdata.AtcNum1);
                fillOurStyleData(atcdata.Atcid);
            }
        }





      
        protected void tbl_atcdetail_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList ddl_Buyerstyle = (e.Row.FindControl("ddl_Buyerstyle") as DropDownList);
                DropDownList ddl_catid = (e.Row.FindControl("ddl_catid") as DropDownList);
                //ddl_Buyerstyle.DataBind();
                //ddl_catid.DataBind();

                try
                {
                    string style = (e.Row.FindControl("lbl_Buyerstyle") as Label).Text;
                    ddl_Buyerstyle.Items.FindByText(style).Selected = true;
                }
                catch (Exception)
                {

                }
                try
                {
                    string catid = (e.Row.FindControl("lbl_catid") as Label).Text.Trim(); 
                    ddl_catid.Items.FindByValue(catid).Selected = true;
                }
                catch (Exception)
                {

                }
            }
        }

        protected void tbl_atcdetail_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = tbl_atcdetail.Rows[index];
            if (e.CommandName == "Update")
            {



                ourstyledata = new BLL.ourstyleData();
                ourstyledata.Ourstyleid = int.Parse(row.Cells[0].Text.ToString());
                ourstyledata.Atcid = int.Parse(row.Cells[1].Text.ToString());

                ourstyledata.Ourstylenum = row.Cells[2].Text.ToString();


                DropDownList buyerstyledrp = (DropDownList)row.Cells[0]
                                                         .FindControl("ddl_Buyerstyle");
                DropDownList garmentcategory = (DropDownList)row.Cells[0]
                                                       .FindControl("ddl_catid");
                TextBox txtqty = (TextBox)row.Cells[0].FindControl("txtQty");
                TextBox fobtxt = (TextBox)row.Cells[0].FindControl("txtfob");


                ourstyledata.BuyerStyle1 = buyerstyledrp.SelectedItem.Text;

                String Garmentcategory = garmentcategory.SelectedItem.Text; ;

                ourstyledata.Catid = int.Parse(garmentcategory.SelectedValue.ToString());

                ourstyledata.Fob = decimal.Parse(fobtxt.Text);

                Boolean isfobchanged = ourstyledata.UpdateOurStyle(ourstyledata);

               if( isfobchanged==true)
                {

                }







            }
            else if (e.CommandName == "ShowDropDown")
            {
                
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ArtWebApp.BLL;
using System.Globalization;

namespace ArtWebApp.Sampling
{
    public partial class FabricEntrynew : System.Web.UI.Page
    {
        private object fabentry;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void clearData()
        {

            txt_merchname.Text = "";
            txt_awbnum.Text = "";
           
            txt_color.Text = "";
            txt_date.Text = "";
            txt_discription.Text = "";
            txt_fabricswatch.Text = "";
            txt_qty.Text = "";
            
            txt_supref.Text = "";
            txt_unit.Text = "";
            txt_weg.Text = "";
            txt_width.Text = "";



        }



        public string InsertFabEntry()
        {
            String SamplingFab_PK = "";
            String msg = "";

            BLL.InventoryBLL.FabricEntryData fabentry = new BLL.InventoryBLL.FabricEntryData();


            fabentry.Merch_Name = txt_merchname.Text;
            fabentry.Description = txt_discription.Text;
            fabentry.Color = txt_color.Text;
            fabentry.Qty = txt_qty.Text;
            fabentry.Width = txt_width.Text;
            fabentry.Unit = txt_unit.Text;
            fabentry.AwbNum = txt_awbnum.Text;
            string s = DateTime.Parse(Request.Form[txt_date.UniqueID].ToString()).ToString("dd/MMM/yyyy", CultureInfo.InvariantCulture);
            fabentry.Date = DateTime.Parse(s);
            fabentry.SuperRef = drp_supplier.SelectedItem.Text.Trim();
            fabentry.Supplier_PK = int.Parse(drp_supplier.SelectedValue.ToString());
           

            fabentry.AwbNum = txt_awbnum.Text;
            fabentry.Weight = txt_weg.Text;
           

            SamplingFab_PK= fabentry.insertfabricdata();

            msg = "FabEntry # : " + SamplingFab_PK + " is generated Sucessfully";

            string Msg = "alert('" + msg + " ')";
            ScriptManager.RegisterClientScriptBlock((btn_submit as Control), this.GetType(), "alert", Msg, true);
            return msg;
        }

        protected void btn_submit_Click(object sender, EventArgs e)

        {
            //try
            //{
              
                String msg = InsertFabEntry();

               
                MessgeboxUpdate1("sucess", msg);
                GridView1.DataBind();
            //}
            //catch(Exception exp)
            //{



            //}



            
        }

        public void MessgeboxUpdate1(String Messagetype, String Messg)
        {
            if (Messagetype == "sucess")
            {
                Messaediv.Attributes["class"] = "success";
                Messaediv.InnerText = Messg;
            }
            else
            {
                Messaediv.Attributes["class"] = "error-message";
                Messaediv.InnerText = Messg;
            }
        }
    }
}





            //       MessageBoxShow(msg);

           
        


        //protected void btn_submit_Click(object sender, EventArgs e)
        //{
        //    //BLL.InventoryBLL.FabricEntryData fabentry = new BLL.InventoryBLL.FabricEntryData();

        //    //fabentry.Merch_Name = txt_merchname.Text;
        //    //fabentry.Description = txt_discription.Text;
        //    //fabentry.Color = txt_color.Text;
        //    //fabentry.Qty = txt_qty.Text;
        //    //fabentry.Width = txt_width.Text;
        //    //fabentry.Unit = txt_unit.Text;
        //    //fabentry.AwbNum = txt_awbnum.Text;
        //    //string s = DateTime.Parse(Request.Form[txt_date.UniqueID].ToString()).ToString("dd/MMM/yyyy", CultureInfo.InvariantCulture);
        //    //fabentry.Date = DateTime.Parse(s);

        //    //fabentry.SuperRef = txt_color.Text;
           
        //    //fabentry.AwbNum = txt_awbnum.Text;
        //    //fabentry.Weight = txt_weg.Text;
        //    //fabentry.Code = txt_code.Text;

        //    //fabentry.InsertFabricData();
        //    //clearData();



           


        //    Response.Write("<script>alert('Submitted Successfully');</script>");
        //}
    

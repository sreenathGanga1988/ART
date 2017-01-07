using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArtWebApp.Inventory.Inventory_Requests
{
    public partial class WrongDocument : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {

        }

        protected void Button2_Click(object sender, EventArgs e)
        {

        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            
        }

        protected void Button4_Click(object sender, EventArgs e)
        {

        }

        protected void Button1_Click1(object sender, EventArgs e)
        {
            ArtWebApp.BLL.InventoryBLL.WrongInventoryDcumentData spdata = new BLL.InventoryBLL.WrongInventoryDcumentData();

            try
            {
                spdata.LoadCombo(drp_doctype.SelectedItem.Text, drp_docnumber);
            }
            catch (Exception)
            {

              
            }
           
        }

        protected void Btn_submit_Click(object sender, EventArgs e)
        {
            InsertDOdata();
        }




        public void InsertDOdata()
        {

            BLL.InventoryBLL.WrongInventoryDcumentData dodata = new BLL.InventoryBLL.WrongInventoryDcumentData();
            dodata.DocumentType = drp_doctype.SelectedItem.Text;
            dodata.Explanation = txt_exp.Text.Trim();
            dodata.User_PK = int.Parse ( drp_user.SelectedValue.ToString ());
            dodata.DocumentNumber = drp_docnumber.SelectedItem.Text; ;
           
            dodata.ActionRequired = txt_actnreq.Text.Trim ();
           
            dodata.AddedDate = DateTime.Now;
            dodata.AddedBy = Session["Username"].ToString();
            String donum = dodata.insertMissingInventoryRequest();

           
            String msg = donum + " Submitted Sucessfully";


            ArtWebApp.Controls.Messagebox.MessgeboxUpdate(Messaediv, "sucess", msg);


        }







    }
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArtWebApp.Inventory
{
    public partial class SalesAndLoan : System.Web.UI.Page
    {
        String POtype="";
      
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ShowBom_Click(object sender, EventArgs e)
        {
            DBTransaction.InventoryTransaction.InventoryTransaction invtra = new DBTransaction.InventoryTransaction.InventoryTransaction();

            DataTable dt = invtra.GetAtcInventoryInALoc(int.Parse(cmb_atc.SelectedValue.ToString()), int.Parse(Session["UserLoc_pk"].ToString()));
            tbl_InverntoryDetails.DataSource = dt;
            tbl_InverntoryDetails.DataBind();
            UpdatePanel2.Update();

            ViewState["InventoryData"] = dt;
        }



        public int IsMultipleRowselected(GridView tbldata, String checkboxname)
        {

            int selectedrowscount = 0;
            foreach (GridViewRow di in tbldata.Rows)
            {
                CheckBox chkBx = (CheckBox)di.FindControl(checkboxname);

                if (chkBx != null && chkBx.Checked)
                {

                    //get the uniqueID of that row
                    selectedrowscount = selectedrowscount + 1;
                }
            }



            return selectedrowscount;
        }

        protected void btn_loan_Click(object sender, EventArgs e)
        {
            int selectedfromincentoryitem = IsMultipleRowselected(tbl_InverntoryDetails, "chk_select");
           
            lbl_message.Text = "";
            if (selectedfromincentoryitem == 0)
            {
                lbl_message.Text = "Select the item from which transfer should happen";
            }
           
            else if ( selectedfromincentoryitem > 1)
            {
                lbl_message.Text = "Only One  item  should be selected at a time for loan";

            }
            else if ( selectedfromincentoryitem == 1)
            {
                POtype = "Loan";
                InsertLoan();
            }
           
        }

        





    public void InsertLoan()
{
    DataTable myDS = (DataTable)ViewState["InventoryData"];

    //create a new datatable to store New Selected Items
    DataTable dt = new DataTable();
    foreach (DataColumn dc in myDS.Columns)
    {
        dc.ReadOnly = false;
    }
    //clone the schme of the Orginal Datatble to new one
    dt = myDS.Clone();

    //Loop through each gridviewrows to find the Rows where checkbox column is checked
    foreach (GridViewRow di in tbl_InverntoryDetails.Rows)
    {
        CheckBox chkBx = (CheckBox)di.FindControl("chk_select");

        if (chkBx != null && chkBx.Checked)
        {

            //get the uniqueID of that row
            int inventoryitemPk = int.Parse((di.FindControl("lblInventoryItem_PK") as Label).Text);

            //create a new datarow for dt
            DataRow dr = dt.NewRow();

            //query the orginal datatable and put it in new data row
            dr = myDS.Select("InventoryItem_PK=" + inventoryitemPk + "").Single();//Error occurs here
            // dr.BeginEdit();
            TextBox txt_transferqty = (di.FindControl("txt_deliveryQty") as TextBox);
            dr["TransferQty"] = (txt_transferqty.Text.Trim() == "") ? 0 : float.Parse(txt_transferqty.Text);

            //import the datarow to datatable
            dt.ImportRow(dr);

        }
    }

    Session["ItemforTransfer"] = dt;


    if (dt.Rows.Count != 0)
    {
        if (POtype.Trim() == "Loan")
        {
            Response.Redirect("~/Inventory/LoanTransfer.aspx");
        }
        else if (POtype.Trim() == "Transfer")
        {
            Response.Redirect("~/Inventory/TransferToGtock.aspx");
        }
        else if (POtype.Trim() == "LoanRepayment")
        {
            Response.Redirect("~/Inventory/LoanRepay.aspx");
        }
    }
}

        protected void btn_gstock_Click(object sender, EventArgs e)
        {
            POtype = "Transfer";
            InsertLoan();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            int selectedfromincentoryitem = IsMultipleRowselected(tbl_InverntoryDetails, "chk_select");

            lbl_message.Text = "";
            if (selectedfromincentoryitem == 0)
            {
                lbl_message.Text = "Select the item from which transfer should happen";
            }

            else if (selectedfromincentoryitem > 1)
            {
                lbl_message.Text = "Only One  item  should be selected at a time for loan";

            }
            else if (selectedfromincentoryitem == 1)
            {
                POtype = "LoanRepayment";
                InsertLoan();
            }
        }
    }
}
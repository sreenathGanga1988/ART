using ArtWebApp.DBTransaction;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArtWebApp.Inventory
{
    public partial class LoanRepay : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {


            if (!IsPostBack)
            {
                Session["TemplatePk"] = 0;
                DataTable dt = new DataTable();
                dt = (DataTable)Session["ItemforTransfer"];
                tbl_InverntoryDetails.DataSource = dt;
                tbl_InverntoryDetails.DataBind();
            }
        }

        protected void ShowBom_Click(object sender, EventArgs e)
        {
            if (!Session["TemplatePk"].Equals(null) && Session["TemplatePk"].ToString().Trim() != "0")
            {
              BLL.InventoryBLL.LoanTransfer loantrns = new BLL.InventoryBLL.LoanTransfer();
               
                DataTable dt = loantrns.GetFromIITDetailsOfALoan(int.Parse(drp_loan.SelectedValue.ToString()));
                tbl_bom.DataSource = dt;
                tbl_bom.DataBind();
                UpdatePanel2.Update();
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Art", "alert('Select a Item from Loan From');", true);
            }


        }

        protected void chk_select_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chkbox = (CheckBox)sender;
            GridViewRow currentRow = (GridViewRow)chkbox.Parent.Parent;
            int rowindex = 0;
            rowindex = currentRow.RowIndex;

            Session["TemplatePk"] = int.Parse((tbl_InverntoryDetails.Rows[rowindex].FindControl("lbl_templatepk") as Label).Text);
            Session["fromSkuDet_PK"] = int.Parse((tbl_InverntoryDetails.Rows[rowindex].FindControl("lbl_fromSkuDet_Pk") as Label).Text);
            Session["fromIITPk"] = int.Parse((tbl_InverntoryDetails.Rows[rowindex].FindControl("lblInventoryItem_PK") as Label).Text);
            fillLoanCombo();
        }



        public void fillLoanCombo()
        {  BLL.InventoryBLL.LoanTransfer loantrns = new BLL.InventoryBLL.LoanTransfer();
        drp_loan.DataSource = loantrns.getLoanofaSKU(int.Parse(Session["fromSkuDet_PK"].ToString()));
        drp_loan.DataValueField = "Loan_pk";
        drp_loan.DataTextField = "LoanNum";
        drp_loan.DataBind();
        upd_loan.Update();
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

        protected void btn_loanTransfer_Click(object sender, EventArgs e)
        {
            int selectedfromincentoryitem = IsMultipleRowselected(tbl_InverntoryDetails, "chk_select");
            int selectedtoinventory = IsMultipleRowselected(tbl_bom, "chk_select");
            lbl_message.Text = "";
            if (selectedfromincentoryitem == 0)
            {
                lbl_message.Text = "Select the item from which transfer should happen";
            }
            else if (selectedtoinventory == 0)
            {
                lbl_message.Text = "Select the item to   which transfer should happen";
            }
            else if (selectedtoinventory > 1 || selectedfromincentoryitem > 1)
            {
                lbl_message.Text = "Only One  to and from item  should be selected at a time";

            }
            else if (selectedtoinventory == 1 || selectedfromincentoryitem == 1)
            {
                if (checkdatagridValue(tbl_InverntoryDetails, "lbl_onhandQty", "txt_deliveryQty"))
                {

                    insertselecteddetails();
                }
            }
        }





        public void insertselecteddetails()
        {
            BLL.InventoryBLL.LoanTransfer loantrns = new BLL.InventoryBLL.LoanTransfer();
            BLL.InventoryBLL.LoanTransferData lndata = new BLL.InventoryBLL.LoanTransferData();

            loantrns.LoanActionType = "LoanReturn";



            foreach (GridViewRow di in tbl_bom.Rows)
            {
                CheckBox chkBx = (CheckBox)di.FindControl("chk_select");

                if (chkBx != null && chkBx.Checked)
                {
                    lndata.ToSkuDet_PK = int.Parse((di.FindControl("lbl_fromSkuDet_Pk") as Label).Text);

                }
            }


            foreach (GridViewRow di in tbl_InverntoryDetails.Rows)
            {
                CheckBox chkBx = (CheckBox)di.FindControl("chk_select");


                if (chkBx != null && chkBx.Checked)
                {

                    lndata.LoanQty = Decimal.Parse((di.FindControl("txt_deliveryQty") as TextBox).Text);
                    lndata.FromSkudet_PK = int.Parse((di.FindControl("lbl_fromSkuDet_Pk") as Label).Text);
                    lndata.FromIIT_Pk = int.Parse((di.FindControl("lblInventoryItem_PK") as Label).Text);
                    lndata.UnitPrice = Decimal.Parse((di.FindControl("lbl_fromcurate") as Label).Text); ;

                }

            }

            loantrns.insertinvenloanmst(lndata);

            ClientScript.RegisterStartupScript(this.GetType(), "Art", "alert('Loan Application Submitted Successfully');", true);

        }







        public Boolean checkdatagridValue(GridView tblgrid, String lbl_Qty1, String txt_Qty2)
        {

            Boolean isQtyok = true;
            for (int i = 0; i < tblgrid.Rows.Count; i++)
            {
                GridViewRow currentRow = tblgrid.Rows[i];
                CheckBox chkBx = (CheckBox)currentRow.FindControl("chk_select");

                if (chkBx != null && chkBx.Checked)
                {
                    try
                    {

                        float AllowedQty = float.Parse(((tblgrid.Rows[i].FindControl(lbl_Qty1) as Label).Text.ToString()));
                        float Enterqty = float.Parse(((tblgrid.Rows[i].FindControl(txt_Qty2) as TextBox).Text.ToString()));
                        if (!QuantityValidator.ISFloatQuantityLesser(AllowedQty, Enterqty))
                        {
                            isQtyok = false;
                            (tblgrid.Rows[i].FindControl(txt_Qty2) as TextBox).BackColor = System.Drawing.Color.Red;


                        }
                        else
                        {
                            (tblgrid.Rows[i].FindControl(txt_Qty2) as TextBox).BackColor = System.Drawing.Color.White;
                        }

                    }
                    catch (Exception)
                    {
                        isQtyok = false;
                        (tblgrid.Rows[i].FindControl(txt_Qty2) as TextBox).BackColor = System.Drawing.Color.Red;

                    }
                }







            }
            return isQtyok;
        }





    }
}
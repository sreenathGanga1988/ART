using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ArtWebApp.BLL.ProcurementBLL;
namespace ArtWebApp.Merchandiser
{
    public partial class Ro : System.Web.UI.Page
    {
        DBTransaction.ProcurementTransaction potrans = new DBTransaction.ProcurementTransaction();
        DataTable dt = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                dt = (DataTable)Session["ItemforPO"];
                tbl_Podetails.DataSource = dt;
                tbl_Podetails.DataBind();

            }
        }

        protected void tbl_Podetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void ShowBom_Click(object sender, EventArgs e)
        {
           // DataTable dt = potrans.GetDetailforRO(int.Parse(cmb_atc.SelectedValue.ToString()), int.Parse(Session["TemplatePk"].ToString ()));
            DBTransaction.InventoryTransaction.InventoryTransaction invtra = new DBTransaction.InventoryTransaction.InventoryTransaction();

            DataTable dt = invtra.GetAtcTemplateInLocation(int.Parse(cmb_atc.SelectedValue.ToString()), int.Parse(Session["TemplatePk"].ToString()),int.Parse(cmb_warehouse.SelectedValue.ToString ()));
            tbl_InverntoryDetails.DataSource = dt;
            tbl_InverntoryDetails.DataBind();
            UpdatePanel2.Update();
        }

        protected void chk_select_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chkbox = (CheckBox)sender;
            GridViewRow currentRow = (GridViewRow)chkbox.Parent.Parent;
            int rowindex = 0;
            rowindex = currentRow.RowIndex;

            Session["TemplatePk"] = int.Parse((tbl_Podetails.Rows[rowindex].FindControl("lbl_templatepk") as Label).Text);

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            int selectedrowsoftoitem=IsMultipleRowselected(tbl_Podetails,"chk_select");
            int seletedfromitems = IsMultipleRowselected(tbl_InverntoryDetails,"chk_selectitem");
            lbl_message.Text = "";
            if(selectedrowsoftoitem==0)
            {
                lbl_message.Text = "Select the item to which transfer should happen";
            }
            else if (seletedfromitems==0)
            {
                lbl_message.Text = "Select the item from  which transfer should happen";
            }
            else if(!IsRateOK())
            {
                lbl_message.Text = "The Unitprices Didnt Match Change the price in costing";
            }
            else
            {
                getRowDetails();

            }
        }

        public void MessgeboxUpdate(String Messagetype, String Messg)
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



        public void getRowDetails()
        {
            RequestOrderMasterData rmstr = new RequestOrderMasterData();
            RoDetailsData  rddet=new RoDetailsData ();

            
            
            int toskudetpk = 0;
            foreach (GridViewRow di in tbl_Podetails.Rows)
            {
                CheckBox chkBx = (CheckBox)di.FindControl("chk_select");

                if (chkBx != null && chkBx.Checked)
                {
                 toskudetpk=   int.Parse((di.FindControl("lbl_skudet_pk") as Label).Text);
                    
                }
            }

            List<RoDetailsData> rk = new List<RoDetailsData>();
            foreach (GridViewRow di in tbl_InverntoryDetails.Rows)
            {
                CheckBox chkBx = (CheckBox)di.FindControl("chk_selectitem");
                

                 if (chkBx != null && chkBx.Checked)
                 {
                     
                     rddet = new RoDetailsData();
                    
                     rddet.ToSkuDet_PK = toskudetpk;
                     rddet.Qty = Decimal.Parse((di.FindControl("txt_deliveryQty") as TextBox).Text);
                     rddet.InventoryItem_PK= int.Parse((di.FindControl("lblInventoryItem_PK") as Label).Text);
                     rddet.UnitPrice = Decimal.Parse((di.FindControl("lbl_fromcurate") as Label).Text); ;
                     rddet.FromSkuDet_PK = int.Parse((di.FindControl("lbl_fromSkuDet_Pk") as Label).Text); ;
                     rk.Add(rddet);
                 }

            }

            rmstr.RoDetailsDataCollection = rk;
            rmstr.ToSkuDet_PK = toskudetpk;
            rmstr.Location_Pk = int.Parse(cmb_warehouse.SelectedValue.ToString());
      String ro= rmstr.insertRowmaterial(rmstr);
       string msg="Ro# '"+ro+"' Generated Successfully";
       MessgeboxUpdate("sucess", msg);
        }


        public int IsMultipleRowselected(GridView tbldata,String checkboxname)
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

        protected void btn_stockro_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Merchandiser/RO/STockRO.aspx");
        }



        public Boolean IsRateOK()
        {
            int skuuompk = 0;
            int newuompk = 0;

            float skucurate = 0;
            float newcurate = 0;
            Boolean israteok = true;
            foreach (GridViewRow di in tbl_Podetails.Rows)
            {
                CheckBox chkBx = (CheckBox)di.FindControl("chk_select");

                if (chkBx != null && chkBx.Checked)
                {
                    skuuompk = int.Parse((di.FindControl("lbl_altuomPK") as Label).Text);
                    skucurate = float.Parse((di.FindControl("lbl_costunitrate") as Label).Text);

                }
            }



            foreach (GridViewRow di in tbl_InverntoryDetails.Rows)
            {
                CheckBox chkBx = (CheckBox)di.FindControl("chk_selectitem");


                if (chkBx != null && chkBx.Checked)
                {
                 

                  
                        newuompk = int.Parse((di.FindControl("lbl_newUomPK") as Label).Text);
                        newcurate = float.Parse((di.FindControl("lbl_fromcurate") as Label).Text);

                        if (skuuompk != 0 && skucurate != 0 && newuompk != 0 && newcurate != 0)
                        {
                        if (ConvertCurrencyAndUOM(newuompk, skuuompk, skucurate, newcurate))
                        {
                          

                        }
                        else
                        {
                            israteok = false;

                        }
                    }

                }
            }

            return israteok;


        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="uomPK"> skudetuom</param>
        /// <param name="auomPk">inventory uom</param>
        /// <param name="cuunitrate"> skuunitrate</param>
        /// <param name="newunitrate">inventoryunitrate</param>

        public Boolean ConvertCurrencyAndUOM(int uomPK, int auomPk,  float cuunitrate, float newunitrate)
        {


            Boolean isok = false;






 






           
            float operendforUOM = 1;
            String operatorusedforUOM = "*";

            if (uomPK == auomPk)
            {
                operendforUOM = 1;
                operatorusedforUOM = "*";

                if(cuunitrate>= newunitrate)
                {
                    isok = true;
                }

            }
            else
            {
                DataTable dt = potrans.getAltuomdata(auomPk, uomPK);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {

                        operendforUOM = float.Parse(dt.Rows[0]["Conv_fact"].ToString());
                        operatorusedforUOM = dt.Rows[0]["Operator"].ToString().Trim();
                        if (operatorusedforUOM == "/")
                        {



                            newunitrate = newunitrate / operendforUOM;
                        }
                        else if (operatorusedforUOM == "*")
                        {


                            newunitrate = newunitrate * operendforUOM;
                        }

                        if (cuunitrate >= newunitrate)
                        {
                            isok = true;
                        }
                    }
                }


            }





       






            return isok;


        }





    }
}
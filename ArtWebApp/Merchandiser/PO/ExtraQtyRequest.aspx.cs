using ArtWebApp.DBTransaction;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArtWebApp.Merchandiser.PO
{
    public partial class ExtraQtyRequest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DBTransaction.BOMTransaction bomtrans = new BOMTransaction();
             

            }

        }





      

     

        protected void ShowBom_Click(object sender, EventArgs e)
        {
          
           
            ShowBOM();
            Session["atcid"] = int.Parse(cmb_atc.SelectedValue.ToString());

          
        }


        public void ShowBOM()
        {

            BLL.MerchandsingBLL.BOMData bmdata = new BLL.MerchandsingBLL.BOMData();
            System.Data.DataTable BomData = bmdata.ShowBOM(int.Parse(cmb_atc.SelectedValue.ToString()));

            if (BomData.Rows.Count <= 0)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Art", "alert('No BOM Available');", true);
            }
            else
            {
                tbl_bom.DataSource = BomData;
                tbl_bom.DataBind();
                Upd_maingrid.Update();
            }
        }







        protected void tbl_bom_RowCommand1(object sender, GridViewCommandEventArgs e)
        {

            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = tbl_bom.Rows[index];
           



        }



   









 




       
      

        protected void Button2_Click(object sender, EventArgs e)
        {
            
            BLL.MerchandsingBLL.BOMData bmdata = new BLL.MerchandsingBLL.BOMData();
            DataTable dt = bmdata.ShowBOM(int.Parse(cmb_atc.SelectedValue.ToString()));
            tbl_bom.DataSource = dt;
            tbl_bom.DataBind();
            Upd_maingrid.Update();

           
        }

        protected void tbl_bom_DataBound(object sender, EventArgs e)
        {
            for (int i = tbl_bom.Rows.Count - 1; i > 0; i--)
            {
                GridViewRow row = tbl_bom.Rows[i];
                GridViewRow previousRow = tbl_bom.Rows[i - 1];
                for (int j = 1; j < 3; j++)
                {
                    if (row.Cells[j].Text == previousRow.Cells[j].Text)
                    {
                        if (previousRow.Cells[j].RowSpan == 0)
                        {
                            if (row.Cells[j].RowSpan == 0)
                            {
                                previousRow.Cells[j].RowSpan += 2;
                            }
                            else
                            {
                                previousRow.Cells[j].RowSpan = row.Cells[j].RowSpan + 1;
                            }
                            row.Cells[j].Visible = false;
                        }
                    }
                }
            }
        }

        protected void Btn_extraRequest_Click(object sender, EventArgs e)
        {
            BLL.ProcurementBLL.ExtraBOMActions WrongPOAction = new BLL.ProcurementBLL.ExtraBOMActions();
            String num = "";

            BLL.ProcurementBLL.ExtraBOMMasterData wrngmdata = new BLL.ProcurementBLL.ExtraBOMMasterData();
         
            wrngmdata.MerchandiserName = txt_merchand.Text.Trim();
            wrngmdata.Explanation = txtarea.Text.Trim();
            wrngmdata.AddedBY = Session["Username"].ToString().Trim();
            wrngmdata.AddedDate = DateTime.Now;
            wrngmdata.Atcid = int.Parse(cmb_atc.SelectedValue.ToString());
            wrngmdata.IsApproved = "N";

            WrongPOAction.ExtraBOMDetailDataCollection = GetWrongPODetailsData();

            WrongPOAction.extrabommasterdata= wrngmdata;
            num = WrongPOAction.insertEBOMdata();



            tbl_bom.DataSource = null;
            tbl_bom.DataBind();
            String msg = "Wrong Po  " + num + "Adjust Request Sucessfully submitted";


            ArtWebApp.Controls.Messagebox.MessgeboxUpdate(Messaediv, "sucess", msg);
        }



        public List<BLL.ProcurementBLL.ExtraBOMDetailData> GetWrongPODetailsData()
        {


            List<BLL.ProcurementBLL.ExtraBOMDetailData> rk = new List<BLL.ProcurementBLL.ExtraBOMDetailData>();
            for (int i = 0; i < tbl_bom.Rows.Count; i++)
            {


                CheckBox chkBx = (CheckBox)(tbl_bom.Rows[i].FindControl("chk_select"));

                if (chkBx != null && chkBx.Checked)
                {
                    int skudet_pk = int.Parse(((tbl_bom.Rows[i].FindControl("lbl_skudetpk") as Label).Text.ToString()));
                  

                    decimal poqty = decimal.Parse(((tbl_bom.Rows[i].FindControl("txt_extraqty") as TextBox).Text.ToString()));


                    BLL.ProcurementBLL.ExtraBOMDetailData pddetails = new BLL.ProcurementBLL.ExtraBOMDetailData();
                    pddetails.Skudet_PK = skudet_pk;
                    pddetails.Qty = poqty;

                   


                    rk.Add(pddetails);

                }



            }




            return rk;


        }










    
}
}
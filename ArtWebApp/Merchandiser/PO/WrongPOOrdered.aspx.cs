using ArtWebApp.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ArtWebApp.BLL;
namespace ArtWebApp.Merchandiser.PO
{
    public partial class WrongPOOrdered : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillAtcCombo();
            }
        }


        public void FillAtcCombo()
        {
            using (ArtEntitiesnew entty = new ArtEntitiesnew())
            {
                entty.Configuration.AutoDetectChangesEnabled = false;
                var q = from atcorder in entty.AtcMasters
                        select new
                        {
                            name = atcorder.AtcNum,
                            pk = atcorder.AtcId
                        };

                // Create a table from the query.
                drp_Atc.DataSource = q.ToList();
                drp_Atc.DataBind();




            }
        }

        public void FillPOCombo(int atcid)
        {
            using (ArtEntitiesnew entty = new ArtEntitiesnew())
            {
                var q = from ponmbr in entty.ProcurementMasters
                        where ponmbr.AtcId == atcid
                        select new
                        {
                            name = ponmbr.PONum,
                            pk = ponmbr.PO_Pk
                        };

                drp_PO.DataSource = q.ToList();
                drp_PO.DataBind();




            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (drp_Atc.SelectedItem.Value != null)
                {
                    FillPOCombo(int.Parse(drp_Atc.SelectedValue.ToString()));
                }
            }
            catch (Exception exp)
            {

                Response.Write(exp.ToString());
            }
        }




        public void fillPoData(int popk)
        {
            BLL.MerchandsingBLL.ProcurementBLL.ProcurementMasterData pmmstrdata = new BLL.MerchandsingBLL.ProcurementBLL.ProcurementMasterData();

            pmmstrdata = pmmstrdata.GetPodata(popk);

          

            tbl_podetails.DataSource = pmmstrdata.POdetails;

            tbl_podetails.DataBind();

        }



        //public String UpdateWrongPodata()
        //{

            
        //}



        public void updateStatus(String msg)
        {
            lbl_mssg.Text = msg;

        }

        protected void drp_currency_SelectionChanged(object sender, Infragistics.Web.UI.ListControls.DropDownSelectionChangedEventArgs e)
        {


        }





        protected void Button2_Click(object sender, EventArgs e)
        {
            fillPoData(int.Parse(drp_PO.SelectedItem.Value.ToString()));
        }

       



        public float convdatagenerator(int currencypk)
        {
            float convvalue = 1;
            DBTransaction.ProcurementTransaction pctrans = new DBTransaction.ProcurementTransaction(); ;
            convvalue = pctrans.Getconversionfact(currencypk);
            return convvalue;
        }



        protected void Btn_submit_Click(object sender, EventArgs e)
        {
            BLL.ProcurementBLL.WrongPOActions WrongPOAction = new BLL.ProcurementBLL.WrongPOActions();
            String num = "";

            BLL.ProcurementBLL.WrongPOData wrngmdata = new BLL.ProcurementBLL.WrongPOData();
            wrngmdata.PO_PK = int.Parse(drp_PO.SelectedValue.ToString());
            wrngmdata.MerchandiserName = txt_merchand.Text.Trim();
            wrngmdata.Explanation = txtarea.Text.Trim();
            wrngmdata.AddedBY = Session["Username"].ToString().Trim();
            wrngmdata.AddedDate = DateTime.Now; 
            wrngmdata.L1ApprovedBY = "N"; ;
            wrngmdata.L1ApprovedBY = "N";
            wrngmdata.ApprovedBY = "N";
            wrngmdata.isapproved = "N";

            WrongPOAction.WrongDetailsDataCollection = GetWrongPODetailsData();

            WrongPOAction.wrngdata = wrngmdata;
            num= WrongPOAction.insertWrongPOdata();



            tbl_podetails.DataSource = null;
            tbl_podetails.DataBind();
            String msg = "Wrong Po  "+num+"Adjust Request Sucessfully submitted";


        ArtWebApp.Controls.Messagebox.MessgeboxUpdate(Messaediv, "sucess", msg);
        }



        


        protected void tbl_podetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            DropDownList ddl_supcolor = (e.Row.FindControl("ddl_Supcolor") as DropDownList);
            DropDownList ddl_supSize = (e.Row.FindControl("ddl_SupSize") as DropDownList);



            try
            {
                Label supcolor = (e.Row.FindControl("lbl_suppliercolor") as Label);
                string itemcolor = supcolor.Text.Trim();
                ddl_supcolor.Items.FindByText(itemcolor).Selected = true;
                supcolor.Visible = false;
            }
            catch (Exception)
            {

            }
            try
            {
                Label supsize = (e.Row.FindControl("lbl_suppliersize") as Label);
                string itemsize = supsize.Text.Trim();
                ddl_supSize.Items.FindByText(itemsize).Selected = true;
                supsize.Visible = false;
            }
            catch (Exception)
            {

            }
        }

        /// <summary>
        /// gets Podetails from gridview
        /// </summary>
        /// <returns></returns>

        public List<BLL.ProcurementBLL.WrongPODetailData> GetWrongPODetailsData()
        {


            List<BLL.ProcurementBLL.WrongPODetailData> rk = new List<BLL.ProcurementBLL.WrongPODetailData>();
            for (int i = 0; i < tbl_podetails.Rows.Count; i++)
            {


                CheckBox chkBx = (CheckBox)(tbl_podetails.Rows[i].FindControl("chk_select"));

                if (chkBx != null && chkBx.Checked)
                {
                    int skudet_pk = int.Parse(((tbl_podetails.Rows[i].FindControl("lbl_skudetpk") as Label).Text.ToString()));
                    int podet_pk = int.Parse(((tbl_podetails.Rows[i].FindControl("lbl_podetpk") as Label).Text.ToString()));

                    decimal poqty = decimal.Parse(((tbl_podetails.Rows[i].FindControl("txt_poQty") as TextBox).Text.ToString()));


                    BLL.ProcurementBLL.WrongPODetailData pddetails = new BLL.ProcurementBLL.WrongPODetailData();
                    pddetails.skudet_pk = skudet_pk;
                    pddetails.qty = poqty;

                    pddetails.podet_pk = podet_pk;


                    rk.Add(pddetails);

                }


                  
            }




            return rk;


        }


        protected void tbl_podetails_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }
    }
}
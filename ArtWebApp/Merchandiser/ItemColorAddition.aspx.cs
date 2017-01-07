using ArtWebApp.DBTransaction;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArtWebApp.Merchandiser
{
    public partial class ItemColorAddition : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                DBTransaction.BOMTransaction bomtrans = new BOMTransaction();
                DataTable TempColordata = bomtrans.GetTemplatecolor();

                DataTable Temsizedata = bomtrans.GetTemplateSize();
                 fillColoroftemplate(TempColordata);
                 fillSizeoftemplate(Temsizedata);

            }
        }

        protected void ShowBom_Click(object sender, EventArgs e)
        {
            BLL.MerchandsingBLL.SKUManager skumgr = new BLL.MerchandsingBLL.SKUManager();         
            DataTable BomData =skumgr.GetAllSKUDetails(int.Parse(cmb_atc.SelectedValue.ToString()));          
           
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



        /// <summary>
        /// fill itemcolor
        /// </summary>
        /// <param name="temppk"></param>
        /// <param name="drpcolor"></param>
        public void fillColoroftemplate(DataTable dt)
        {


            drp_color.DataSource = dt;
            drp_color.DataTextField = "TemplateColor";
            drp_color.DataValueField = "TemplateColor";
            drp_color.DataBind();


        }


        /// <summary>
        /// fill itemsize
        /// </summary>
        /// <param name="temppk"></param>
        /// <param name="drpsize"></param>
        public void fillSizeoftemplate(DataTable dt)
        {


            drp_size.DataSource = dt;
            drp_size.DataTextField = "TemplateSize";
            drp_size.DataValueField = "TemplateSize";
      




            drp_size.DataBind();


        }



        public void updatecolor()
        {
            for (int i = tbl_bom.Rows.Count - 1; i > 0; i--)
            {
                GridViewRow row = tbl_bom.Rows[i];
                CheckBox chkBx = (CheckBox)row.FindControl("chk_select");
                if (chkBx.Checked == true)
                {
                    int skudetpk = int.Parse((row.FindControl("lbl_skudetpk") as Label).Text);
                    DBTransaction.BOMTransaction bomtrans = new BOMTransaction();
                    String itemcolor = drp_color.SelectedItem.Text;

                    //if (!bomtrans.isPOGiven(skudetpk))
                    //{


                        bomtrans.updateitemcolor(itemcolor, skudetpk);


                    //}

                }
            }
        }
        public void updateSize()
        {
            for (int i = tbl_bom.Rows.Count - 1; i > 0; i--)
            {
                GridViewRow row = tbl_bom.Rows[i];
                CheckBox chkBx = (CheckBox)row.FindControl("chk_select");
                if (chkBx.Checked == true)
                {
                    int skudetpk = int.Parse((row.FindControl("lbl_skudetpk") as Label).Text);
                    DBTransaction.BOMTransaction bomtrans = new BOMTransaction();
                    String itemsize = drp_size.SelectedItem.Text;

                  

                        bomtrans.updateitemSize(itemsize,skudetpk);


                    

                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            updatecolor();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            updateSize();
        }



    }
}
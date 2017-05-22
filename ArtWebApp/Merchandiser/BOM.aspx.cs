using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ArtWebApp.DBTransaction;
using System.Data;
using System.Collections;
using System.Diagnostics;
namespace ArtWebApp.Merchandiser
{
    public partial class BOM : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                DBTransaction.BOMTransaction bomtrans = new BOMTransaction();
                DataTable  TempColordata=bomtrans.GetTemplatecolor();

                DataTable Temsizedata = bomtrans.GetTemplateSize();

                ViewState["TempColordata"]=TempColordata;
                ViewState["Temsizedata"] = Temsizedata;

            }

        }






        public void GeneratePO1(String POtype)
        {
            BLL.MerchandsingBLL.BOMData bmdata = new BLL.MerchandsingBLL.BOMData();
            ArrayList skulist = new ArrayList();
            int itemdetaislnotavailable = 0;
            //Loop through each gridviewrows to find the Rows where checkbox column is checked
            foreach (GridViewRow di in tbl_bom.Rows)
            {
                CheckBox chkBx = (CheckBox)di.FindControl("chk_select");

                if (chkBx != null && chkBx.Checked)
                {

                    //get the uniqueID of that row
                    int skudetpk = int.Parse((di.FindControl("lbl_skudetpk") as Label).Text);

                    float balanceqty = float.Parse((di.FindControl("lbl_skudetpk") as Label).Text);


                    string lbl_isCD = (di.FindControl("lbl_isCD") as Label).Text;
                    string lbl_isSd = (di.FindControl("lbl_isSd") as Label).Text;
                    string lbl_itemsize = (di.FindControl("lbl_itemsize") as Label).Text;
                    string lbl_itemcolor = (di.FindControl("lbl_itemcolor") as Label).Text;
                    
                    if(lbl_isCD.Trim ()=="Y"&& lbl_itemcolor.Trim()=="")
                    {
                        itemdetaislnotavailable++;
                    }
                    else if(lbl_isSd.Trim() == "Y" && lbl_itemsize.Trim() == "")
                    {
                        itemdetaislnotavailable++;
                    }
                    else
                    {
                        skulist.Add(skudetpk);
                    }



                  

                    






                }
            }

            // DataTable dt=bmdata.GetBOMOfSpecificData (skulist);

            if (itemdetaislnotavailable > 0)
            {
                String Msg = itemdetaislnotavailable + " Items out of selected Doesnot consist item details and will not be considered for  "+ POtype;

                ClientScript.RegisterStartupScript(this.GetType(), "Art", "alert('" + Msg + "');", true);

            }

            if (skulist.Count>0)
            {
                DataTable dt = bmdata.ShowPOdetails(skulist, int.Parse(Session["atcid"].ToString()));

                Session["ItemforPO"] = dt;


                if (dt.Rows.Count != 0)
                {
                    if (POtype.Trim() == "AutoPO")
                    {
                        Response.Redirect("~/Merchandiser/PO/Procurement.aspx");
                        //   Response.Redirect("~/Merchandiser/PO/POCalBack.aspx");
                    }
                    else if (POtype.Trim() == "RO")
                    {
                        Response.Redirect("~/Merchandiser/RO/RO.aspx");
                    }
                    else if (POtype.Trim() == "FOC")
                    {
                        
                        Response.Redirect("~/Merchandiser/PO/FOCPO.aspx");
                    }
                    else if (POtype.Trim() == "AppendPO")
                    {
                        
                        Response.Redirect("~/Merchandiser/PO/POAppend.aspx");
                    }
                }

            }


        
        }










        protected void btn_PO_Click(object sender, EventArgs e)
        {
            Session["atcid"] = int.Parse(cmb_atc.SelectedValue.ToString());
            GeneratePO1("AutoPO");
        }


        /// <summary>
        /// fill itemcolor
        /// </summary>
        /// <param name="temppk"></param>
        /// <param name="drpcolor"></param>
        public void fillColoroftemplate(DataTable dt,  DropDownList drpcolor)
        {


            drpcolor.DataSource = dt;
            drpcolor.DataValueField = "TemplateColor";
            drpcolor.DataTextField = "TemplateColor";
            drpcolor.DataBind();


        }


        /// <summary>
        /// fill itemsize
        /// </summary>
        /// <param name="temppk"></param>
        /// <param name="drpsize"></param>
        public void fillSizeoftemplate(DataTable dt, DropDownList drpsize)
        {


            drpsize.DataSource = dt;
            drpsize.DataValueField = "TemplateSize";
            drpsize.DataTextField = "TemplateSize";
            drpsize.DataBind();


        }


        public void loadItemcolor()
        {

        }

        public void loadItemsize()
        {

        }

        protected void ShowBom_Click(object sender, EventArgs e)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            ShowBOM();
         //   Session["atcid"] = int.Parse(cmb_atc.SelectedValue.ToString());

            stopwatch.Stop();
            long elapsed_time = stopwatch.ElapsedMilliseconds;
        }


        public void ShowBOM()
        {           

            BLL.MerchandsingBLL.BOMData bmdata = new BLL.MerchandsingBLL.BOMData();
            DataTable BomData = bmdata.ShowBOM(int.Parse(cmb_atc.SelectedValue.ToString()));          
           
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
            if (e.CommandName == "UploadItemcolor")
            {
               

                
                CheckBox chkBx = (CheckBox)row.FindControl("chk_select");
                if (chkBx.Checked == true)
                {
                    int skudetpk = int.Parse((row.FindControl("lbl_skudetpk") as Label).Text);
                    DBTransaction.BOMTransaction bomtrans = new BOMTransaction();

                    if (!bomtrans.isPOGiven(skudetpk))
                    {
                        DropDownList drp_color = (row.FindControl("drp_color") as DropDownList);
                        DropDownList drp_size = (row.FindControl("drp_size") as DropDownList);
                       
                        bomtrans.updateitemcolorandSize(drp_color, drp_size, skudetpk);
                       
                        
                    }

                }
            }
            else if (e.CommandName == "ShowDropDown")
            {
                showdropdown(row);
            }



        }



        public void showdropdown(GridViewRow currentRow)
        {
            String isCD = (currentRow.FindControl("lbl_isCD") as Label).Text.Trim();
            String isSD = (currentRow.FindControl("lbl_isSd") as Label).Text.Trim();
            String isCM = (currentRow.FindControl("lbl_iscm") as Label).Text.Trim();



            CheckBox chkBx = (CheckBox)currentRow.FindControl("chk_select");

            if (chkBx.Checked == true)
            {
                int skudetpk = int.Parse((currentRow.FindControl("lbl_skudetpk") as Label).Text);
              
                DBTransaction.BOMTransaction bomtrans = new BOMTransaction();

                if (!bomtrans.isPOGiven(skudetpk))
                {

                    DataTable TempColordata = (DataTable)ViewState["TempColordata"];
                    DataTable Temsizedata = (DataTable)ViewState["Temsizedata"];

                    DropDownList drp_color = (currentRow.FindControl("drp_color") as DropDownList);
                    DropDownList drp_size = (currentRow.FindControl("drp_size") as DropDownList);

                    if (isCD == "Y")
                    {


                        fillColoroftemplate(TempColordata, drp_color);
                        drp_color.Visible = true;
                        drp_color.Items.Insert(0, "");
                       
                       
                    }
                    if (isSD == "Y")
                    {
                        fillSizeoftemplate(Temsizedata,  drp_size);
                        drp_size.Visible = true;
                        drp_size.Items.Insert(0, "");
                    }
                    if (isCM == "Y")
                    {
                       fillColoroftemplate(TempColordata,  drp_color);
                        fillSizeoftemplate(Temsizedata, drp_size);
                       
                        drp_color.Visible = true;
                        drp_size.Visible = true;
                        drp_color.Items.Insert(0, "");
                        drp_size.Items.Insert(0, "");

                    }


                    //commented o 25-7-2015
                    //  Upd_maingrid.Update();
                    updateRowUpdatepanels(currentRow);
                }
            }
            
        }




        public void updateRowUpdatepanels(GridViewRow currentRow)
        {
            UpdatePanel upd_size = (currentRow.FindControl("upd_size") as UpdatePanel);
            UpdatePanel upd_color = (currentRow.FindControl("upd_color") as UpdatePanel);




            upd_size.Update();
            upd_color.Update();
          


        }





        protected void btn_RO_Click(object sender, EventArgs e)
        {
            if (IsMultipleRowselected())
            {
                Session["atcid"] = int.Parse(cmb_atc.SelectedValue.ToString());
                GeneratePO1("RO");
            }else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Art", "alert('Only one item can be selected for  RO at once');", true);
            }
        }




        public Boolean IsMultipleRowselected()
        {
            Boolean issinglecolselected = false;
            int selectedrowscount = 0;
            foreach (GridViewRow di in tbl_bom.Rows)
            {
                CheckBox chkBx = (CheckBox)di.FindControl("chk_select");

                if (chkBx != null && chkBx.Checked)
                {

                    //get the uniqueID of that row
                    selectedrowscount = selectedrowscount + 1;
                }
            }

            if(selectedrowscount==1)
            {
                issinglecolselected = true;
           }

            return issinglecolselected;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //Stopwatch stopWatch = new Stopwatch();
            //stopWatch.Start();
            //ArtWebApp.DBTransaction.SkuCreator skucrtr = new ArtWebApp.DBTransaction.SkuCreator();
            //skucrtr.CreateSkUDetails(int.Parse(cmb_atc.SelectedValue.ToString()));
            //stopWatch.Stop();

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            ArtWebApp.BLL.MerchandsingBLL.SKUdetailGenerator.CreateSkUDetailsNew(int.Parse(cmb_atc.SelectedValue.ToString()));
            //skucrtr.CreateSkUDetails(int.Parse(cmb_atc.SelectedValue.ToString()));
            stopWatch.Stop();






            // Get the elapsed time as a TimeSpan value.
            TimeSpan ts = stopWatch.Elapsed;
            string asd = ts.TotalSeconds.ToString ();
            Session["atcid"] = int.Parse(cmb_atc.SelectedValue.ToString());
            ShowBOM();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            BLL.MerchandsingBLL.BOMData bmdata = new BLL.MerchandsingBLL.BOMData();
            DataTable dt = bmdata.ShowBOM(int.Parse(cmb_atc.SelectedValue.ToString()));
            tbl_bom.DataSource = dt;
            tbl_bom.DataBind();
            Upd_maingrid.Update();

            stopwatch.Stop();
          long  elapsed_time = stopwatch.ElapsedMilliseconds;
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

        protected void Button2_Click1(object sender, EventArgs e)
        {
            Session["atcid"] = int.Parse(cmb_atc.SelectedValue.ToString());
            GeneratePO1("FOC");
        }

        protected void btn_addtoPO_Click(object sender, EventArgs e)
        {
            Session["atcid"] = int.Parse(cmb_atc.SelectedValue.ToString());
            GeneratePO1("AppendPO");
        }
    }
}
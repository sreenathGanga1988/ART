using Infragistics.Web.UI.ListControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArtWebApp.Merchandiser.Atc_Chart
{
    public partial class MerchandiserEta : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                drp_rcpt.DataBind();
            }
        }


        protected void btn_confrmsup_Click(object sender, EventArgs e)
        {
            try
            {
                recieptdata.SelectCommand = "SELECT [DocNum], [Doc_Pk] FROM [DocMaster] WHERE (IsCompleted = 'N') AND (Supplier_PK = " + int.Parse(drp_supplier.SelectedValue.ToString()) + ") AND ( Supplier_PK=" + int.Parse(drp_supplier.SelectedValue.ToString()) + ")";
                drp_rcpt.DataBind();
            }
            catch (Exception)
            {

            }
        }



        protected void btn_savercpt_Click(object sender, EventArgs e)
        {
            String msg = InsertDocRecieptmasterData();
            lbl_errordisplayer.Text = msg;
            drp_rcpt.DataBind();
         
        }




        protected void btn_confirmRcpt_Click(object sender, EventArgs e)
        {
            hdn_rcptnum.Value = drp_rcpt.SelectedValue.ToString();
            BLL.MerchandsingBLL.DocumentReceiptdata rcptmstrdata = new  BLL.MerchandsingBLL.DocumentReceiptdata();
            rcptmstrdata = rcptmstrdata.getdocedit(int.Parse (drp_rcptmstr.SelectedValue.ToString()));



            txta_remark.InnerText = rcptmstrdata.Remark;
            txt_boe.Text = rcptmstrdata.BOENum;
            txt_container.Text = rcptmstrdata.ContainerNum;
            dtp_deliverydate.Value = rcptmstrdata.ETADate;




        }



        /// <summary>
        /// insert the Docreciept
        /// </summary>

        public String InsertDocRecieptmasterData()
        {
            String rcptnum = "";
            BLL.MerchandsingBLL.DocumentReceiptdata rcptmstrdata = new BLL.MerchandsingBLL.DocumentReceiptdata();

            rcptmstrdata.Doc_Pk = int.Parse(drp_rcptmstr.SelectedValue.ToString());
            rcptmstrdata.ContainerNum = txt_container.Text.Trim();
            rcptmstrdata.Supplier_PK = int.Parse(drp_supplier.SelectedValue.ToString());
            rcptmstrdata.BOENum = txt_boe.Text.Trim();
            rcptmstrdata.ETADate = DateTime.Parse(dtp_deliverydate.Value.ToString());
            rcptmstrdata.Remark = txta_remark.Value.ToString();
            rcptmstrdata.InhouseDate = DateTime.Parse(dtp_deliverydate.Value.ToString());
            rcptmstrdata.AddedBy = Session["Username"].ToString().Trim();
            rcptmstrdata.IsCompleted = "N";
            rcptmstrdata.Location_PK = int.Parse(Session["UserLoc_pk"].ToString());

            rcptnum = rcptmstrdata.UpdateReciptMstr();


            String msg = "Doc # : " + drp_rcpt.SelectedItem.Text + " is Updated Sucessfully";
            

            return msg;
        }
















        protected void btn_saveMrn_Click(object sender, EventArgs e)
        {
            InsertPodetails();
            lbl_errordisplayer.Text = "Details Added";
        }


      

  
      

        protected void Button1_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow di in tbl_Podetails.Rows)
            {
                CheckBox chkBx = (CheckBox)di.FindControl("chk_select");

                if (chkBx != null && chkBx.Checked)
                {
                    Infragistics.Web.UI.EditorControls.WebDatePicker wbdt = di.FindControl("wdp_etadate") as Infragistics.Web.UI.EditorControls.WebDatePicker;


                    wbdt.Date = DateTime.Parse(dtp_eta.Date.ToString());
                }
            }
            upd_grid.Update();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow di in tbl_Podetails.Rows)
            {
                CheckBox chkBx = (CheckBox)di.FindControl("chk_select");

                if (chkBx != null && chkBx.Checked)
                {
                    TextBox wbdt = di.FindControl("txt_do") as TextBox;


                    wbdt.Text = txt_deliverynote.Text.Trim();
                }
            }
            upd_grid.Update();
        }

        protected void chk_selectAll_CheckedChanged(object sender, EventArgs e)
        {
            selectall();
        }


        public void selectall()
        {
            if (chk_selectAll.Checked == true)
            {
                foreach (GridViewRow di in tbl_Podetails.Rows)
                {
                    CheckBox chkBx = (CheckBox)di.FindControl("chk_select");
                    chkBx.Checked = true;

                }
            }
            else
            {
                foreach (GridViewRow di in tbl_Podetails.Rows)
                {
                    CheckBox chkBx = (CheckBox)di.FindControl("chk_select");
                    chkBx.Checked = false;

                }
            }
            upd_grid.Update();
        }



        public List<BLL.MerchandsingBLL.DocPodetaildata> GetPODetailsData()
        {

            List<BLL.MerchandsingBLL.DocPodetaildata> rk = new List<BLL.MerchandsingBLL.DocPodetaildata>();
            foreach (GridViewRow di in tbl_Podetails.Rows)
            {
                CheckBox chkBx = (CheckBox)di.FindControl("chk_select");

                if (chkBx != null && chkBx.Checked)
                {


                    int DocDet_Pk = int.Parse(((di.FindControl("lbl_DocDet_Pk") as Label).Text.ToString()));
                    decimal recieptqty = decimal.Parse((di.FindControl("txt_qty") as TextBox).Text.ToString());
                    String invnum = (di.FindControl("txt_do") as TextBox).Text;
                    Infragistics.Web.UI.EditorControls.WebDatePicker wbdt = di.FindControl("wdp_etadate") as Infragistics.Web.UI.EditorControls.WebDatePicker;
                    BLL.MerchandsingBLL.DocPodetaildata podetdata = new BLL.MerchandsingBLL.DocPodetaildata();

                    podetdata.Doc_Pk = int.Parse(drp_rcpt.SelectedItem.Value.ToString());
                    podetdata.DocDet_Pk = DocDet_Pk;
                    podetdata.Qty = recieptqty;
                    podetdata.InvNum = invnum;
                    //try
                    //{
                    //    podetdata.ETADate = wbdt.Date;
                    //}
                    //catch (Exception)
                    //{

                       
                    //}

                    podetdata.AddedDate = DateTime.Now;
                    podetdata.AddedBy = Session["Username"].ToString().Trim();
                    rk.Add(podetdata);
                }
            }



            return rk;


        }

        

        protected void Button3_Click(object sender, EventArgs e)
        {
            BLL.MerchandsingBLL.DocumentReceiptdata rcptmstrdata = new BLL.MerchandsingBLL.DocumentReceiptdata();
            AddPODATA();
            dtp_eta.Value = rcptmstrdata.geteta(int.Parse(drp_rcpt.SelectedValue.ToString()));
            upd_eta.Update();
        }

        public void AddPODATA()
        {

            ArtWebApp.BLL.MerchandsingBLL.AtcChartBLL atcbll = new BLL.MerchandsingBLL.AtcChartBLL();
            tbl_Podetails.DataSource = atcbll.GetDetailsofADOC(int.Parse(drp_rcpt.SelectedValue.ToString()));
            tbl_Podetails.DataBind();
            upd_grid.Update();
        }

        protected void dtp_eta_ValueChanged(object sender, Infragistics.Web.UI.EditorControls.TextEditorValueChangedEventArgs e)
        {

        }


        public void InsertPodetails()
        {
            BLL.MerchandsingBLL.DocumentReceiptdata rcptmstrdata = new BLL.MerchandsingBLL.DocumentReceiptdata();
            rcptmstrdata.DocumentDetailsDataCollection = GetPODetailsData();
            rcptmstrdata.UpdatePoEtaData();
        }

    }
}
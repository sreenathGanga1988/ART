using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArtWebApp.Merchandiser.ASQ.Packing
{
    public partial class SolidSizeColorPackingInstruction : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                tbl_podetails.DataSource = (DataTable)Session["mstrdata"];
                tbl_podetails.DataBind();
            }

           

        }

        protected void tbl_podetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row in tbl_podetails.Rows)
            {
                CheckBox chkBx = (CheckBox)row.FindControl("chk_select");

                if (chkBx != null && chkBx.Checked)
                {
                   


                    int lbl_popackid = int.Parse((row.FindControl("lbl_POPackId") as Label).Text);
                    int lbl_ourstyleid = int.Parse((row.FindControl("lbl_ourstyleid") as Label).Text);
                    int atcid = int.Parse((row.FindControl("lbl_atcid") as Label).Text);
                    int txt_totalctnnew = int.Parse((row.FindControl("txt_totalctn") as TextBox).Text);
                    int txt_pcperctnnew = int.Parse((row.FindControl("txt_pcperctn") as TextBox).Text);
                    int txt_totalqty = int.Parse((row.FindControl("txt_totalqty") as TextBox).Text);
                    string lbl_color = (row.FindControl("lbl_color") as Label).Text;
                    string lbl_size = (row.FindControl("lbl_size") as Label).Text;




        decimal txt_length = decimal.Parse((row.FindControl("txt_length") as TextBox).Text);
                    decimal txt_width = decimal.Parse((row.FindControl("txt_width") as TextBox).Text);
                    decimal txt_height = decimal.Parse((row.FindControl("txt_height") as TextBox).Text);
                    decimal txt_NNWeight = decimal.Parse((row.FindControl("txt_NNWeight") as TextBox).Text);
                    decimal txt_Netweight = decimal.Parse((row.FindControl("txt_Netweight") as TextBox).Text);
                    decimal txt_gross = decimal.Parse((row.FindControl("txt_gross") as TextBox).Text);
                    string drp_weightuom = (row.FindControl("drp_weightuom") as DropDownList).SelectedValue.ToString();
                    string drp_NetUOM = (row.FindControl("drp_NetUOM") as DropDownList).SelectedValue.ToString();















                    ArtWebApp.BLL.MerchandsingBLL.PackingListMasterBLL pdata = new ArtWebApp.BLL.MerchandsingBLL.PackingListMasterBLL();

                    pdata.Atc_ID = atcid;

                    pdata.CtnDimension = "NA";
                    pdata.NoofCTN = txt_totalctnnew;
                    pdata.PCPerCtn = txt_pcperctnnew;
                    pdata.PcPerPolybag = 0;
                    pdata.PackingInstruction = "NA";
                    pdata.Length = txt_length;
                    pdata.Width = txt_width;
                    pdata.Height = txt_height;
                    pdata.NetWeight = txt_Netweight;
                    pdata.NNWeight = txt_NNWeight;
                    pdata.Grossweight = txt_gross;
                    pdata.WeightUOM = drp_weightuom;
                    pdata.CtnUOM = drp_NetUOM;
                








                    List<BLL.MerchandsingBLL.PackingListdetailDataBLL> rk = new List<BLL.MerchandsingBLL.PackingListdetailDataBLL>();
                    BLL.MerchandsingBLL.PackingListdetailDataBLL pkdet = new BLL.MerchandsingBLL.PackingListdetailDataBLL();


                    pkdet.POPackId = lbl_popackid;
                    pkdet.OurStyleID = lbl_ourstyleid;
                    pkdet.Atcid = atcid;
                    pkdet.SizeName = lbl_size;
                    pkdet.ColorName= lbl_color;
                    pkdet.TotalQty = txt_totalqty;
                    pkdet.NoofCTN = txt_totalctnnew;
                    pkdet.PcperCtn = txt_pcperctnnew;
                    rk.Add(pkdet);

                    pdata.PackingListdetailDataDataCollection = rk;
                    pdata.insertPackinglistMaster();




                }
            }

            tbl_podetails.DataSource = null;
            tbl_podetails.DataBind();
            String msg = " ASQ Details Added Successfully ";
            ArtWebApp.Controls.Messagebox.MessgeboxUpdate(Messaediv, "sucess", msg);
        }

        protected void btn_pcperctn_Click(object sender, EventArgs e)
        {

        }
    }
}
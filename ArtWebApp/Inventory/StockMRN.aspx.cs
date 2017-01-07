using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ArtWebApp.BLL;

namespace ArtWebApp.Inventory
{
    public partial class StockMRN : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// insert the reciept
        /// </summary>

        public void InsertRecieptmasterData()
        {
            String rcptnum = "";
            BLL.InventoryBLL.StockRecieptMasterData srcptmstrdata = new BLL.InventoryBLL.StockRecieptMasterData();


            srcptmstrdata.ContainerNum = txt_container.Text.Trim();
            srcptmstrdata.Supplier_PK = int.Parse(drp_supplier.SelectedValue.ToString());
            srcptmstrdata.BOENum = txt_container.Text.Trim();
            srcptmstrdata.Deliverydate = DateTime.Parse(dtp_deliverydate.Value.ToString());
            srcptmstrdata.Remark = txta_remark.Value.ToString();
            srcptmstrdata.InhouseDate = DateTime.Parse(dtp_deliverydate.Value.ToString());
            srcptmstrdata.AddedBy = Session["Username"].ToString().Trim();
            srcptmstrdata.IsCompleted = "N";
            srcptmstrdata.RecptLocation_PK = int.Parse(Session["UserLoc_pk"].ToString());

            rcptnum = srcptmstrdata.InsertReciptMstr(srcptmstrdata);


            String msg = "Reciept # : " + rcptnum + " is generated Sucessfully";
            MessageBoxShow(msg);
        }
        public void MessageBoxShow(String msg)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "Art", "alert('" + msg + "');", true);
        }
        protected void btn_savercpt_Click(object sender, EventArgs e)
        {
            InsertRecieptmasterData();
            drp_rcpt.DataBind();
        }

        protected void btn_confirmRcpt_Click(object sender, EventArgs e)
        {
            hdn_rcptnum.Value = drp_rcpt.SelectedValue.ToString();
           podata.DataBind();
            drp_Po.DataBind();
        }

        protected void btn_confirmPO_Click(object sender, EventArgs e)
        {
            BLL.ProcurementBLL.StockPODetailsdata spdata = new BLL.ProcurementBLL.StockPODetailsdata();
            tbl_Podetails.DataSource = spdata.GetSpoData(int.Parse(drp_Po.SelectedValue.ToString()));
            tbl_Podetails.DataBind();
        }

        protected void btn_saveMrn_Click(object sender, EventArgs e)
        {
            insertmrn();
        }





        public void insertmrn()
        {
            String mrnum = "";
            BLL.InventoryBLL.StockPOreceipt prrrcpt = new BLL.InventoryBLL.StockPOreceipt();
            prrrcpt.smrnmstrdata = getSMRNMasterData();
            prrrcpt.StockMRNDetailsDataCollection = GetMRnDetailsData();

            mrnum = prrrcpt.InsertSMRNData(prrrcpt);


            String msg = "MRN # : " + mrnum + " is generated Sucessfully";


            MessageBoxShow(msg);

        }


        public BLL.InventoryBLL.StockMRNMasterData getSMRNMasterData()
        {

            BLL.InventoryBLL.StockMRNMasterData smrnmstr = new BLL.InventoryBLL.StockMRNMasterData();

            smrnmstr.DoNumber = txt_deliverynote.Text;
            smrnmstr.AddedDate = DateTime.Now;
            smrnmstr.AddedBY = Session["Username"].ToString().Trim();
            smrnmstr.SPo_PK = int.Parse(drp_Po.SelectedValue.ToString());
            smrnmstr.Location_Pk = int.Parse(Session["UserLoc_pk"].ToString());
            smrnmstr.Reciept_Pk = int.Parse(drp_rcpt.SelectedValue.ToString());

            return smrnmstr;
        }
        public List<BLL.InventoryBLL.StockMRNDetailsData> GetMRnDetailsData()
        {
            BLL.InventoryBLL.StockMRNDetailsData mrndetdata = null;
            List<BLL.InventoryBLL.StockMRNDetailsData> rk = new List<BLL.InventoryBLL.StockMRNDetailsData>();
            foreach (GridViewRow di in tbl_Podetails.Rows)
            {
                CheckBox chkBx = (CheckBox)di.FindControl("chk_select");

                if (chkBx != null && chkBx.Checked)
                {
                    int SPODetails_PK = int.Parse(((di.FindControl("lbl_SPODetails_PK") as Label).Text.ToString()));
                    int SPO_PK = int.Parse(drp_Po.SelectedItem.Value.ToString());
                    int Template_PK = int.Parse(((di.FindControl("lbl_Template_PK") as Label).Text.ToString()));

                    String composition = ((di.FindControl("lbl_composition") as Label).Text).Trim();
                    String construct = ((di.FindControl("lbl_construct") as Label).Text).Trim();
                    String color = ((di.FindControl("lbl_color") as Label).Text).Trim();
                    String width = ((di.FindControl("lbl_width") as Label).Text).Trim();
                    String size = ((di.FindControl("lbl_size") as Label).Text).Trim();
                    String weight = ((di.FindControl("lbl_weight") as Label).Text).Trim();
                    Decimal unitprice = Decimal.Parse(((di.FindControl("lbl_unitprice") as Label).Text.ToString()));
                    String uomcode = ((di.FindControl("lbl_UOM") as Label).Text.ToString());

                    Decimal recieptqty = decimal.Parse((di.FindControl("txt_reciept") as TextBox).Text.ToString());
                    decimal extraqty = decimal.Parse((di.FindControl("txt_extra") as TextBox).Text.ToString());

                    mrndetdata = new BLL.InventoryBLL.StockMRNDetailsData();





                    mrndetdata.SPODetails_PK = SPODetails_PK;
                    mrndetdata.SPO_PK = SPO_PK;
                    mrndetdata.Template_PK = Template_PK;
                    mrndetdata.Unitprice = unitprice;
                    mrndetdata.UOMCode = uomcode;
                    mrndetdata.ReceivedQty = recieptqty;
                    mrndetdata.ExtraQty = extraqty;

                    mrndetdata.Composition = composition;
                    mrndetdata.Construct = construct;
                    mrndetdata.TemplateColor = color;
                    mrndetdata.TemplateSize = size;
                    mrndetdata.TemplateWidth = width;
                    mrndetdata.TemplateWeight = weight;


                    rk.Add(mrndetdata);
                }
            }



            return rk;


        }

        protected void btn_searchReciept_Click(object sender, EventArgs e)
        {
            recieptdata.SelectCommand = "SELECT [StockRecieptNum], [SReciept_Pk] FROM [StockRecieptMaster] WHERE (IsCompleted = 'N') AND (RecptLocation_PK = " + int.Parse(Session["UserLoc_pk"].ToString()) + ") AND ( Supplier_PK=" + int.Parse(drp_supplier.SelectedValue.ToString()) + ")";
            drp_rcpt.DataBind();
            Upd_rxptdrop.Update();

        }
    }
}
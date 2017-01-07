using ArtWebApp.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArtWebApp.Inventory.Fabric_Transaction
{
    public partial class MRNRollEntry : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_confirmAtc_Click(object sender, EventArgs e)
        {

        }

        protected void btn_atc_Click(object sender, EventArgs e)
        {
            try
            {
                if (drp_atc.SelectedValue != null)
                {
                    FillPOCombo(int.Parse(drp_atc.SelectedValue.ToString()));
                }
            }
            catch (Exception exp)
            {


            }
        }




        public void FillPOCombo(int atcid)
        {
            using (ArtEntitiesnew entty = new ArtEntitiesnew())
            {
                var q = from ponmbr in entty.ProcurementMasters
                        where ponmbr.AtcId == atcid && ponmbr.POType == "F"
                        select new
                        {
                            name = ponmbr.PONum,
                            pk = ponmbr.PO_Pk
                        };

                ddl_po.DataSource = q.ToList();
                ddl_po.DataValueField = "pk";
                ddl_po.DataTextField = "name";
                ddl_po.DataBind();
                upd_po.Update();



            }
        }

        public void FillMRNCombo(int poid)
        {
            using (ArtEntitiesnew entty = new ArtEntitiesnew())
            {
                var q = from ponmbr in entty.MrnMasters
                        where ponmbr.Po_PK == poid
                        select new
                        {
                            name = ponmbr.MrnNum,
                            pk = ponmbr.Mrn_PK
                        };

                drp_mrn.DataSource = q.ToList();
                drp_mrn.DataValueField = "pk";
                drp_mrn.DataTextField = "name";
                drp_mrn.DataBind();
                upd_mrn.Update();



            }
        }

        protected void btn_po_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddl_po.SelectedValue != null)
                {
                    FillMRNCombo(int.Parse(ddl_po.SelectedValue.ToString()));
                }
            }
            catch (Exception exp)
            {


            }
        }




        public void FillFabricombo(int mrn_pk)
        {

            drp_color.DataSource = BLL.InventoryBLL.RollTransactionBLL.getfabricdetailsofMRN(mrn_pk);

            drp_color.DataValueField = "MrnDet_PK";
            drp_color.DataTextField = "ItemDescription";
            drp_color.DataBind();
            upd_color.Update();



        }






        protected void btn_mrn_Click(object sender, EventArgs e)
        {
            try
            {
                if (drp_mrn.SelectedValue != null)
                {
                    FillFabricombo(int.Parse(drp_mrn.SelectedValue.ToString()));
                }
            }
            catch (Exception exp)
            {


            }
        }

        protected void btn_color_Click(object sender, EventArgs e)
        {
            tbl_InverntoryDetails.DataSource = BLL.InventoryBLL.RollTransactionBLL.CreateRollRows(int.Parse(TextBox1.Text));
            tbl_InverntoryDetails.DataBind();
            upd_grid.Update();
        }






        public List<BLL.InventoryBLL.FabricRollmasterDataDetails> GetRollDetailsData()
        {

            List<BLL.InventoryBLL.FabricRollmasterDataDetails> rk = new List<BLL.InventoryBLL.FabricRollmasterDataDetails>();
            for (int i = 0; i < tbl_InverntoryDetails.Rows.Count; i++)
            {

                String txt_rollnum = ((tbl_InverntoryDetails.Rows[i].FindControl("txt_rollnum") as TextBox).Text.ToString());
                //Decimal txt_qty = Decimal.Parse((tbl_InverntoryDetails.Rows[i].FindControl("txt_qty") as TextBox).Text.ToString());
                String txt_remark = ((tbl_InverntoryDetails.Rows[i].FindControl("txt_remark") as TextBox).Text.ToString());
                String lbl_uom = ((tbl_InverntoryDetails.Rows[i].FindControl("lbl_uom") as Label).Text.ToString());
                String txt_syard = ((tbl_InverntoryDetails.Rows[i].FindControl("txt_syard") as TextBox).Text.ToString());
                String txt_sshrinkage = ((tbl_InverntoryDetails.Rows[i].FindControl("txt_sshrinkage") as TextBox).Text.ToString());
                String txt_sWidth = ((tbl_InverntoryDetails.Rows[i].FindControl("txt_sWidth") as TextBox).Text.ToString());
                String txt_Sshade = ((tbl_InverntoryDetails.Rows[i].FindControl("txt_Sshade") as TextBox).Text.ToString());
                String txt_sgsm = ((tbl_InverntoryDetails.Rows[i].FindControl("txt_sgsm") as TextBox).Text.ToString());
                

                BLL.InventoryBLL.FabricRollmasterDataDetails rolldata = new BLL.InventoryBLL.FabricRollmasterDataDetails();


                rolldata.MRnDet_PK = int.Parse(drp_color.SelectedValue.ToString());
                rolldata.RollNum = txt_rollnum;
                rolldata.Qty = Decimal.Parse( txt_syard.ToString ());
                rolldata.UOM = lbl_uom;
                rolldata.Remark = txt_remark;
                rolldata.SShrink = txt_sshrinkage;
                rolldata.SYard = txt_syard;
                rolldata.SShade = txt_Sshade;
                rolldata.SWidth = txt_sWidth;
                rolldata.AShrink ="";
                rolldata.AShade ="";
                rolldata.AWidth ="";
                rolldata.AYard = "";
                rolldata.SGSM = txt_sgsm;
                rolldata.AGSM = "";

                 rk.Add(rolldata);
            }




            return rk;


        }


        public BLL.InventoryBLL.RollInventoryData getmstrdetails()
        {
            BLL.InventoryBLL.RollInventoryData rlinvdata = new BLL.InventoryBLL.RollInventoryData();

            rlinvdata.Addeddate = DateTime.Now;
            rlinvdata.DocumentNum = drp_mrn.Text.Trim();
            rlinvdata.AddedVia = "MR";
            rlinvdata.AddedBy = Session["Username"].ToString().Trim(); ;
            rlinvdata.Location_Pk = int.Parse(Session["UserLoc_pk"].ToString().Trim());



            return rlinvdata;

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            BLL.InventoryBLL.FabricRollEntryMRN mrnrolldata = new BLL.InventoryBLL.FabricRollEntryMRN();
            mrnrolldata.rollinvdata = getmstrdetails();
            mrnrolldata.Rolldatacollection = GetRollDetailsData();
            mrnrolldata.insertMrnRollData();
            tbl_InverntoryDetails.DataSource = null;
            tbl_InverntoryDetails.DataBind();
            upd_grid.Update();
            
            
        }









    }
}
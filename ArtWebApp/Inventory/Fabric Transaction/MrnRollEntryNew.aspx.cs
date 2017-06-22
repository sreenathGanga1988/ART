using ArtWebApp.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArtWebApp.Inventory.Fabric_Transaction
{
    public partial class MrnRollEntryNew : System.Web.UI.Page
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

        public void FillAsnCombo()
        {
            BLL.InventoryBLL.InspectionData fbrolldet = new BLL.InventoryBLL.InspectionData();

            drp_asn.DataSource = fbrolldet.GetDocumentnumberByMRNDETPK(int.Parse(drp_color.SelectedValue.ToString()));
            drp_asn.DataValueField = "pk";
            drp_asn.DataTextField = "name";
            drp_asn.DataBind();
            UPD_ASN.Update();

           


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
        {int lctnpk = int.Parse(Session["UserLoc_pk"].ToString().Trim());

            using (ArtEntitiesnew entty = new ArtEntitiesnew())
            {
                var q = from ponmbr in entty.MrnMasters
                        where ponmbr.Po_PK == poid && ponmbr.Location_Pk== lctnpk
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







        public void FillMRNQTY(int mrndetpk)
        {
            using (ArtEntitiesnew entty = new ArtEntitiesnew())
            {
                var q = from ponmbr in entty.MrnDetails join uommstr in entty.UOMMasters on ponmbr.Uom_PK equals uommstr.Uom_PK
                        where ponmbr.MrnDet_PK == mrndetpk
                        select new
                        {
                            recieved = ponmbr.ReceiptQty +ponmbr.ExtraQty,
                            pk = uommstr.UomName
                        };

               foreach (var element in q)
                {
                    lbl_mrnQty.Text = element.recieved.ToString();
                    lbl_UOM.Text = element.pk.ToString();
                    upd_qty.Update();
                }
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
            BLL.InventoryBLL.FabricRollmasterDataDetails fbrolldet = new BLL.InventoryBLL.FabricRollmasterDataDetails();

            FillMRNQTY(int.Parse(drp_color.SelectedValue.ToString()));

            tbl_InverntoryDetails.DataSource = fbrolldet.getRollDetailsofASNandMrnDetpk(int.Parse(drp_asn.SelectedValue.ToString()), int.Parse(drp_color.SelectedValue.ToString()), int.Parse(ddl_po.SelectedValue.ToString()));
            tbl_InverntoryDetails.DataBind();
            upd_grid.Update();
        }






        public List<BLL.InventoryBLL.FabricRollmasterDataDetails> GetRollDetailsData()
        {

           

            List<BLL.InventoryBLL.FabricRollmasterDataDetails> rk = new List<BLL.InventoryBLL.FabricRollmasterDataDetails>();
            
                for (int i = 0; i < tbl_InverntoryDetails.Rows.Count; i++)
                {
                CheckBox chkBx = (CheckBox)tbl_InverntoryDetails.Rows[i].FindControl("chk_select");

                if (chkBx != null && chkBx.Checked)
                {
                    String lbl_rollpk = ((tbl_InverntoryDetails.Rows[i].FindControl("lbl_rollpk") as Label).Text.ToString());


                    BLL.InventoryBLL.FabricRollmasterDataDetails rolldata = new BLL.InventoryBLL.FabricRollmasterDataDetails();


                    rolldata.MRnDet_PK = int.Parse(drp_color.SelectedValue.ToString());
                    rolldata.Roll_PK = int.Parse(lbl_rollpk);
                    rk.Add(rolldata);
                }



            }
            return rk;


        }


        public BLL.InventoryBLL.RollInventoryData getmstrdetails()
        {
            BLL.InventoryBLL.RollInventoryData rlinvdata = new BLL.InventoryBLL.RollInventoryData();

            rlinvdata.Addeddate = DateTime.Now;
            rlinvdata.DocumentNum = drp_mrn.SelectedItem.ToString().Trim();
            rlinvdata.AddedVia = "MR";
            rlinvdata.AddedBy = Session["Username"].ToString().Trim(); ;
            rlinvdata.Location_Pk = int.Parse(Session["UserLoc_pk"].ToString().Trim());



            return rlinvdata;

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            BLL.InventoryBLL.FabricRollEntryMRN mrnrolldata = new BLL.InventoryBLL.FabricRollEntryMRN();
            //mrnrolldata.rollinvdata = getmstrdetails();
            mrnrolldata.Rolldatacollection = GetRollDetailsData();
         
            mrnrolldata.UpdateRollMRNDetails();
            tbl_InverntoryDetails.DataSource = null;
            tbl_InverntoryDetails.DataBind();
            upd_grid.Update();


        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (drp_mrn.SelectedValue != null)
                {
                    FillAsnCombo();
                }
            }
            catch (Exception exp)
            {


            }
            
        }
    }
}
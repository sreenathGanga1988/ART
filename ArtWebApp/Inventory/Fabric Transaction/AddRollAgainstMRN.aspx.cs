using ArtWebApp.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArtWebApp.Inventory.Fabric_Transaction
{
    public partial class AddRollAgainstMRN : System.Web.UI.Page
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

            drp_asn.DataSource = fbrolldet.GetDocumentnumberByMRNDETPKSupplier(int.Parse(drp_color.SelectedValue.ToString()));
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
        {
            int lctnpk = int.Parse(Session["UserLoc_pk"].ToString().Trim());

            using (ArtEntitiesnew entty = new ArtEntitiesnew())
            {
                var q = from ponmbr in entty.MrnMasters
                        where ponmbr.Po_PK == poid && ponmbr.Location_Pk == lctnpk
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
                entty.Configuration.AutoDetectChangesEnabled = false;
                float alreadyreceived = 0;

                var q = from ponmbr in entty.MrnDetails
                        join uommstr in entty.UOMMasters on ponmbr.Uom_PK equals uommstr.Uom_PK
                        where ponmbr.MrnDet_PK == mrndetpk
                        select new
                        {
                            recieved = ponmbr.ReceiptQty + ponmbr.ExtraQty,
                            pk = uommstr.UomName
                        };

                foreach (var element in q)
                {
                    lbl_mrnQty.Text = element.recieved.ToString();
                    lbl_UOM.Text = element.pk.ToString();
                    upd_qty.Update();
                }
                upd_mrn.Update();



                var alreadyrecievedQty = from fab in entty.FabricRollmasters
                                        
                                         where fab.MRnDet_PK == mrndetpk
                                         select new
                                         {
                                             fab.SYard
                                         };
                foreach (var element in alreadyrecievedQty)
                {

                    alreadyreceived += float.Parse(element.SYard.ToString());
                }


                lbl_alreadyadded.Text = alreadyreceived.ToString();
                upd_alreadyAdded.Update();

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
                CheckBox chkBx = (CheckBox)tbl_InverntoryDetails.Rows[i].FindControl("Chk_select");

                if (chkBx != null && chkBx.Checked)
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
                    String Lotnum = ((tbl_InverntoryDetails.Rows[i].FindControl("txt_lot") as TextBox).Text.ToString());
                    String txt_sweight = ((tbl_InverntoryDetails.Rows[i].FindControl("txt_sweight") as TextBox).Text.ToString());
                    BLL.InventoryBLL.FabricRollmasterDataDetails rolldata = new BLL.InventoryBLL.FabricRollmasterDataDetails();

                    rolldata.PO_PK = int.Parse(ddl_po.SelectedValue.ToString());
                    rolldata.MRnDet_PK = int.Parse(drp_color.SelectedValue.ToString());
                    // rolldata.SkuDet_PK = 0;

                    rolldata.RollNum = txt_rollnum;
                    rolldata.Qty = Decimal.Parse(txt_syard.ToString());
                    rolldata.UOM = lbl_uom;
                    rolldata.Remark = txt_remark;
                    rolldata.SShrink = txt_sshrinkage;
                    rolldata.SYard = txt_syard;
                    rolldata.SShade = txt_Sshade;
                    rolldata.SWidth = txt_sWidth;
                    rolldata.AShrink = "";
                    rolldata.AShade = "";
                    rolldata.AWidth = "";
                    rolldata.AYard = "0";
                    rolldata.SGSM = txt_sgsm;
                    rolldata.Sweight = txt_sweight;
                    rolldata.Lotnum = Lotnum;
                    rolldata.SUpplierDoc_PK = int.Parse(drp_asn.SelectedValue.ToString());


                    rk.Add(rolldata);
                }


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
            mrnrolldata.MRNNUM = drp_mrn.SelectedItem.Text.Trim();
           // mrnrolldata.rollinvdata = getmstrdetails();
            mrnrolldata.Rolldatacollection = GetRollDetailsData();
            mrnrolldata.insertSupplierRollData();
            tbl_InverntoryDetails.DataSource = null;
            tbl_InverntoryDetails.DataBind();
            upd_grid.Update();

            // ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "calculateforKnit()", true);
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

        protected void btn_addroll_Click(object sender, EventArgs e)
        {
            tbl_InverntoryDetails.DataSource = BLL.InventoryBLL.RollTransactionBLL.CreateRollRows(int.Parse(TextBox1.Text));
            tbl_InverntoryDetails.DataBind();
            upd_grid.Update();
        }

        protected void chk_knitreg_CheckedChanged(object sender, EventArgs e)
        {
            checkboxchanged();
        }

        protected void chk_knitTubular_CheckedChanged(object sender, EventArgs e)
        {
            checkboxchanged();
        }


        public void checkboxchanged()
        {
            if (chk_knitreg.Checked == true || chk_knitreg.Checked == true)
            {
                for (int i = 0; i < tbl_InverntoryDetails.Rows.Count; i++)
                {
                   
                                         


                    TextBox txt_syard = tbl_InverntoryDetails.Rows[i].FindControl("txt_syard") as TextBox;
                    TextBox txt_sgsm = tbl_InverntoryDetails.Rows[i].FindControl("txt_sgsm") as TextBox;
                    TextBox txt_sweight = tbl_InverntoryDetails.Rows[i].FindControl("txt_sweight") as TextBox;

                    txt_syard.ReadOnly = false;
                    txt_gsm.Enabled = true;
                    txt_sweight.Enabled = true;


                }

                upd_grid.Update();
            }
            else
            {
                for (int i = 0; i < tbl_InverntoryDetails.Rows.Count; i++)
                {




                    TextBox txt_syard = tbl_InverntoryDetails.Rows[i].FindControl("txt_syard") as TextBox;
                    TextBox txt_sgsm = tbl_InverntoryDetails.Rows[i].FindControl("txt_sgsm") as TextBox;
                    TextBox txt_sweight = tbl_InverntoryDetails.Rows[i].FindControl("txt_sweight") as TextBox;

                    txt_syard.Enabled = true;
                    txt_gsm.Enabled = false;
                    txt_sweight.Enabled = false;


                }
                upd_grid.Update();
            }
            }
    }
}
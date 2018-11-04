using ArtWebApp.DataModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Services;
using System.Web.UI.WebControls;

namespace ArtWebApp.Production.Cutting
{
    public partial class LaysheetRollEditor : System.Web.UI.Page
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
            List<decimal?> list = Session["ApprovedLocationlist"] as List<decimal?>;
            using (ArtEntitiesnew entty = new ArtEntitiesnew())
            {
                var q = from atcorder in entty.AtcMasters
                        select new
                        {
                            name = atcorder.AtcNum,
                            pk = atcorder.AtcId
                        };

                // Create a table from the query.


                drp_atc.DataSource = q.ToList();
                drp_atc.DataBind();
                upd_atc.Update();

                var q1 = from order in entty.LocationMasters
                         where order.LocType == "F" && list.Contains(order.Location_PK)
                         select new
                         {
                             name = order.LocationName,
                             pk = order.Location_PK
                         };
                drp_fact.DataSource = q1.ToList();
                drp_fact.DataBind();

                UPD_FACT.Update();
            }
        }

        public void FillOurStyleCombo(int atcid)
        {
            using (ArtEntitiesnew entty = new ArtEntitiesnew())
            {
                var q = from ponmbr in entty.AtcDetails
                        where ponmbr.AtcId == atcid
                        select new
                        {
                            name = ponmbr.OurStyle,
                            pk = ponmbr.OurStyleID
                        };

                drp_ourstyle.DataSource = q.ToList();
                drp_ourstyle.DataBind();
                upd_ourstyle.Update();



            }
        }

        public void FillAllcutorder(int ourstyleid, int toloc)
        {


            using (ArtEntitiesnew entty = new ArtEntitiesnew())
            {
                var q = from ponmbr in entty.CutOrderMasters
                        where ponmbr.OurStyleID == ourstyleid && ponmbr.ToLoc == toloc
                        select new
                        {
                            name = ponmbr.Cut_NO,

                            //  name=ponmbr.CostingCount,
                            pk = ponmbr.CutID
                        };


                drp_cutorder.DataSource = q.ToList();
                drp_cutorder.DataBind();
                upd_cutorder.Update();



            }

        }

        public void Filllaysheetroll(int cutid)
        {
            using (ArtEntitiesnew entty = new ArtEntitiesnew())
            {

                var sizedetails = (from ponmbr in entty.LaySheetRollMasters
                                   where ponmbr.CutID == cutid
                                   select new
                                   {
                                       ponmbr.LaysheetRollmaster_Pk,
                                       ponmbr.LayRollRef
                                   }).Distinct();

                drp_cutRoll.DataSource = sizedetails.ToList();
                drp_cutRoll.DataBind();
                upd_layroll.Update();



            }
        }
        protected void btn_atc_Click(object sender, EventArgs e)
        {
            FillOurStyleCombo(int.Parse(drp_atc.SelectedValue.ToString()));
        }

        protected void btn_cutorder_Click(object sender, EventArgs e)
        {
            Filllaysheetroll(int.Parse(drp_cutorder.SelectedValue.ToString()));
        }

        protected void btn_showroll_Click(object sender, EventArgs e)
        {
            BLL.ProductionBLL.LaysheetBLL lblldata = new BLL.ProductionBLL.LaysheetBLL();
            DataTable dt = lblldata.getRollSelectedAgainstALaysheetroll(drp_cutRoll.SelectedItem.Text);


            DataTable newdataroll = lblldata.getRollofaCutorderNotlayed(int.Parse(drp_cutorder.SelectedValue.ToString()), int.Parse(drp_fact.SelectedValue.ToString()));

            tbl_AlreadyRollDetails.DataSource = dt;
            tbl_AlreadyRollDetails.DataBind();
            upd_alreadygridgrid.Update();

            tbl_RollDetails.DataSource = newdataroll;
            tbl_RollDetails.DataBind();
            upd_grid.Update();

            
        }

        protected void btn_OURSTYLE_Click(object sender, EventArgs e)
        {
            FillAllcutorder(int.Parse(drp_ourstyle.SelectedValue.ToString().ToString()), int.Parse(drp_fact.SelectedValue.ToString().ToString()));
        }



        [WebMethod]
        public static string Deletelaysheetrollysnc(int Planid)
        {

            BLL.ProductionBLL.LaysheetMasterData lblmstr = new BLL.ProductionBLL.LaysheetMasterData();

          
            return lblmstr.DeleteLaysheetRoll(Planid);
        }

        protected void btn_addroll_Click(object sender, EventArgs e)
        {
            string msg = "";
            BLL.ProductionBLL.LaysheetMasterData lblmstr = new BLL.ProductionBLL.LaysheetMasterData();
            lblmstr.AddedDate = DateTime.Now;
            lblmstr.cutid = int.Parse(drp_cutorder.SelectedValue.ToString());
            lblmstr.cutnum = drp_cutorder.SelectedItem.Text;
            lblmstr.LaysheetRollmaster_Pk = int.Parse(drp_cutRoll.SelectedValue.ToString());
      


            lblmstr.LaysheetDetaolsDataCollection = LSDetailsData();

        String    num = lblmstr.InsertLaySheetRollRollOnly();

            msg = num + " is generated Successfully";
            tbl_RollDetails.DataSource = null;
            tbl_RollDetails.DataBind();
            ArtWebApp.Controls.Messagebox.MessgeboxUpdate(Messaediv, "sucess", msg);
        }





        public List<BLL.ProductionBLL.LaysheetDetaolsData> LSDetailsData()
        {

            List<BLL.ProductionBLL.LaysheetDetaolsData> rk = new List<BLL.ProductionBLL.LaysheetDetaolsData>();


            foreach (GridViewRow di in tbl_RollDetails.Rows)
            {
                CheckBox chkBx = (CheckBox)di.FindControl("chk_select");

                if (chkBx != null && chkBx.Checked)
                {

                    int lbl_rollpk = int.Parse(((di.FindControl("lbl_rollpk") as Label).Text.ToString()));
                    int sequence_no= int.Parse(((di.FindControl("txt_sequence") as TextBox).Text.ToString()));
                    Decimal lbl_ayard = Decimal.Parse(((di.FindControl("lbl_ayard") as Label).Text.ToString()));
                    String rollstatus = ((di.FindControl("lbl_rollstatus") as Label).Text.ToString());
                    CheckBox chk_cutable = (di.FindControl("chk_cutable") as CheckBox);


                    BLL.ProductionBLL.LaysheetDetaolsData lsdetdata = new BLL.ProductionBLL.LaysheetDetaolsData();

                    lsdetdata.RollStatus = rollstatus;
                    lsdetdata.Roll_PK = lbl_rollpk;
                    lsdetdata.RollAyard = lbl_ayard;
                    lsdetdata.sequence_no = sequence_no;
                    rk.Add(lsdetdata);
                }
            }
            return rk;


        }




    }
}
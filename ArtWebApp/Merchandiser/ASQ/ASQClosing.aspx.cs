using ArtWebApp.DataModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArtWebApp.Merchandiser.ASQ
{
    public partial class ASQClosing : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }

        protected void buttonAtc_Click(object sender, EventArgs e)
        {
            fillcontrol();
        }
        public void fillcontrol()
        {
            int atcid = int.Parse(cmb_atc.SelectedValue.ToString());

            ArtEntitiesnew enty = new ArtEntitiesnew();
            var PoQuery = from pckmst in enty.PoPackMasters
                          where pckmst.AtcId == atcid
                          select new
                          {
                              name = pckmst.PoPacknum + " " + pckmst.BuyerPO,
                              pk = pckmst.PoPackId
                          };




            drp_popack.DataSource = PoQuery.ToList();
            drp_popack.DataBind();





            //showAllPoPackATC();
        }
        protected void cmb_atc_SelectedIndexChanged(object sender, EventArgs e)
        {

        }





    

       





      

        protected void tbl_podata_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        public void validatepotype()
        {
            int k = 0;
            foreach (GridViewRow row in tbl_podata.Rows)
            {





                CheckBox chkBx = (CheckBox)row.FindControl("chk_select");
                if (chkBx.Checked == true)
                {


                }
            }
        }

        protected void buttonAtc0_Click(object sender, EventArgs e)
        {
            ArrayList popaklist = new ArrayList();
            List<Infragistics.Web.UI.ListControls.DropDownItem> items = drp_popack.SelectedItems;
            foreach (Infragistics.Web.UI.ListControls.DropDownItem item in items)
            {

                int popackid = int.Parse(item.Value.ToString());
                popaklist.Add(popackid);
            }


            if (popaklist.Count > 0 && popaklist != null)
            {

                BLL.MerchandsingBLL.AllocationBLL pkmstrdata = new BLL.MerchandsingBLL.AllocationBLL();

                tbl_podata.DataSource = pkmstrdata.GetPOPACKMasterShippedforPoClosure(popaklist);
                //  tbl_podata.DataSource = asqshuffle.GetAllPOPackDataofStyleandPopack(int.Parse(drp_ourstyle.SelectedValue.ToString()), popaklist);
                tbl_podata.DataBind();
                // updgrid2.Update();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

            List<BLL.POPackDetailData> rk = new List<BLL.POPackDetailData>();

            foreach (GridViewRow di in tbl_podata.Rows)
            {
                CheckBox chkBx = (CheckBox)di.FindControl("chk_select");

                if (chkBx != null && chkBx.Checked)
                {
                    int ourstyleid = int.Parse(((di.FindControl("lbl_OurStyleID") as Label).Text.ToString()));
                    int popackid = int.Parse(((di.FindControl("lbl_popackid") as Label).Text.ToString()));
                 
                    BLL.POPackDetailData deldet = new BLL.POPackDetailData();
                    deldet.Ourstyleid = ourstyleid;
                    deldet.PoPackId = popackid;
                
                    rk.Add(deldet);
                }
            }

            BLL.PoPackMasterData pcmstrdata = new BLL.PoPackMasterData();
            pcmstrdata.POPackDetailDataCollection = rk;
            pcmstrdata.ShortCloseASQ();
            tbl_podata.DataSource = null;
            tbl_podata.DataBind();

            String msg = " Asq Short Closed Successfully ";

            ArtWebApp.Controls.Messagebox.MessgeboxUpdate(Messaediv, "sucess", msg);
        }

        protected void btn_showallasq_Click(object sender, EventArgs e)
        {
            tbl_podata.DataSource = allPodatasorce;
            //  tbl_podata.DataSource = asqshuffle.GetAllPOPackDataofStyleandPopack(int.Parse(drp_ourstyle.SelectedValue.ToString()), popaklist);
            tbl_podata.DataBind();
        }

        protected void tbl_podata_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

          //      int ourstyleid = int.Parse((e.Row.FindControl("lbl_ourstyleid") as Label).Text);
                AjaxControlToolkit.CalendarExtender txtcalender = (e.Row.FindControl("dtp_deliverydate_CalendarExtender") as AjaxControlToolkit.CalendarExtender);
                Label lbl_handoverdate = (e.Row.FindControl("lbl_handoverdate") as Label);
                CheckBox chkBx = (CheckBox)e.Row.FindControl("chk_select");
              txtcalender.SelectedDate = DateTime.Parse( lbl_handoverdate.Text);
              //if(DateTime.Parse(lbl_handoverdate.Text)> DateTime.Parse("15 March 2017"))
              //  {
              //      chkBx.Enabled = false;
              //  }



            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            updatePOPackmaster();
        }


        public void updatePOPackmaster()
        {
            foreach (GridViewRow di in tbl_podata.Rows)
            {
                
                   CheckBox chkBx = (CheckBox)di.FindControl("chk_select");
                TextBox dtp_deliverydate = (TextBox)di.FindControl("dtp_deliverydate");
                if (chkBx != null && chkBx.Checked)
                {
                
                    string s = DateTime.Parse(Request.Form[dtp_deliverydate.UniqueID].ToString()).ToString("dd/MMM/yyyy", CultureInfo.InvariantCulture);

                    DateTime deliverydate = DateTime.Parse(((Label)di.FindControl("lbl_deliveryDate")).Text);

                    if(deliverydate<= DateTime.Parse(s))
                    {
                        int popackid = int.Parse(((di.FindControl("lbl_popackid") as Label).Text.ToString()));
                        BLL.PoPackMasterData pomstrdata = new BLL.PoPackMasterData();

                        pomstrdata.PoPackId = popackid;

                        pomstrdata.HandoverDate = DateTime.Parse(s);

                        pomstrdata.updatePOpAckHD(pomstrdata);

                    }



                }
            }

            tbl_podata.DataSource = null;
            tbl_podata.DataBind();

            String msg = "HD Updated Successfully ";

            ArtWebApp.Controls.Messagebox.MessgeboxUpdate(Messaediv, "sucess", msg);
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            //            allPodatasorce.SelectCommand = @"SELECT        PoPackId, PoPacknum, BuyerPO, OurStyle, BuyerStyle, POQty, ShipedQty, OurStyleID, FirstDeliveryDate, DeliveryDate, HandoverDate
            //FROM(SELECT        PoPackMaster.PoPackId, PoPackMaster.PoPacknum, PoPackMaster.BuyerPO, AtcDetails.OurStyle, AtcDetails.BuyerStyle, SUM(POPackDetails.PoQty) AS POQty, ISNULL
            //                             ((SELECT        SUM(ShipmentHandOverDetails.ShippedQty) AS Expr1
            //                                 FROM            ShipmentHandOverDetails INNER JOIN
            //                                                          JobContractDetail ON ShipmentHandOverDetails.JobContractDetail_pk = JobContractDetail.JobContractDetail_pk
            //                                 GROUP BY JobContractDetail.PoPackID, JobContractDetail.OurStyleID
            //                                 HAVING(JobContractDetail.PoPackID = PoPackMaster.PoPackId) AND(JobContractDetail.OurStyleID = POPackDetails.OurStyleID)), 0) AS ShipedQty, AtcDetails.OurStyleID, PoPackMaster.FirstDeliveryDate, 
            //                         PoPackMaster.DeliveryDate, PoPackMaster.AtcId, PoPackMaster.HandoverDate, MAX(POPackDetails.IsShortClosed) AS Expr1
            //FROM PoPackMaster INNER JOIN
            //                         POPackDetails ON PoPackMaster.PoPackId = POPackDetails.POPackId INNER JOIN
            //                         AtcDetails ON POPackDetails.OurStyleID = AtcDetails.OurStyleID
            //GROUP BY PoPackMaster.PoPackId, PoPackMaster.PoPacknum, PoPackMaster.BuyerPO, AtcDetails.OurStyle, AtcDetails.BuyerStyle, POPackDetails.OurStyleID, AtcDetails.OurStyleID, PoPackMaster.FirstDeliveryDate, 
            //                         PoPackMaster.DeliveryDate, PoPackMaster.AtcId, PoPackMaster.HandoverDate
            //HAVING(PoPackMaster.HandoverDate < GETDATE()) AND(MIN(POPackDetails.IsShortClosed) = N'N') ) AS tt
            //WHERE(POQty - ShipedQty > 0)


            //Union





            //SELECT        PoPackId, PoPacknum, BuyerPO, OurStyle, BuyerStyle, POQty, ShipedQty, OurStyleID, FirstDeliveryDate, DeliveryDate, HandoverDate
            //FROM(SELECT        PoPackMaster.PoPackId, PoPackMaster.PoPacknum, PoPackMaster.BuyerPO, AtcDetails.OurStyle, AtcDetails.BuyerStyle, SUM(POPackDetails.PoQty) AS POQty, ISNULL
            //                             ((SELECT        SUM(ShipmentHandOverDetails.ShippedQty) AS Expr1
            //                                 FROM            ShipmentHandOverDetails INNER JOIN
            //                                                          JobContractDetail ON ShipmentHandOverDetails.JobContractDetail_pk = JobContractDetail.JobContractDetail_pk
            //                                 GROUP BY JobContractDetail.PoPackID, JobContractDetail.OurStyleID
            //                                 HAVING(JobContractDetail.PoPackID = PoPackMaster.PoPackId) AND(JobContractDetail.OurStyleID = POPackDetails.OurStyleID)), 0) AS ShipedQty, AtcDetails.OurStyleID, PoPackMaster.FirstDeliveryDate, 
            //                         PoPackMaster.DeliveryDate, PoPackMaster.AtcId, PoPackMaster.HandoverDate
            //FROM            PoPackMaster INNER JOIN
            //                         POPackDetails ON PoPackMaster.PoPackId = POPackDetails.POPackId INNER JOIN
            //                         AtcDetails ON POPackDetails.OurStyleID = AtcDetails.OurStyleID
            //GROUP BY PoPackMaster.PoPackId, PoPackMaster.PoPacknum, PoPackMaster.BuyerPO, AtcDetails.OurStyle, AtcDetails.BuyerStyle, POPackDetails.OurStyleID, AtcDetails.OurStyleID, PoPackMaster.FirstDeliveryDate, 
            //                         PoPackMaster.DeliveryDate, PoPackMaster.AtcId, PoPackMaster.HandoverDate
            //HAVING(PoPackMaster.HandoverDate > GETDATE())
            //                          ) AS tt
            //WHERE(POQty - ShipedQty > 0) and(ShipedQty != 0)";


            allPodatasorce.SelectCommand = @"logistics";


            tbl_podata.DataSource = allPodatasorce;
            //  tbl_podata.DataSource = asqshuffle.GetAllPOPackDataofStyleandPopack(int.Parse(drp_ourstyle.SelectedValue.ToString()), popaklist);
            tbl_podata.DataBind();

        }
    }
}
using ArtWebApp.DataModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArtWebApp.Production.ShipmentHandOver
{
    public partial class ShipementHandoverfromAtcWorld : System.Web.UI.Page
    {
         protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Btn_showJC_Click(object sender, EventArgs e)
        {
            try
            {
                LoadShipmentDO(int.Parse(drp_factory.SelectedValue.ToString()));
            }
            catch (Exception)
            {


            }
        }



        public void LoadShipmentDO(int factid)
        {
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {
                //var q = from jcdata in enty.ATCWorldToArtShipDatas
                //        where jcdata.ArtLocation_PK == factid
                //        select new
                //        {
                //            name = jcdata.SDONo,
                //            pk = jcdata.SDONo.ToString()
                //        };

               var result = (from p in enty.ATCWorldToArtShipDatas
                                      where p.ArtLocation_PK == factid
                             select new 
                                      {
                                          name = p.SDONo,
                                      }).Distinct().ToList();
                drp_SDO.DataSource = result.ToList();
                drp_SDO.DataBind();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            ArrayList Sdolist = new ArrayList();
            List<Infragistics.Web.UI.ListControls.DropDownItem> items = drp_SDO.SelectedItems;
            foreach (Infragistics.Web.UI.ListControls.DropDownItem item in items)
            {

                String popackid = item.Text.ToString();
                Sdolist.Add(popackid);
            }


            if (Sdolist.Count > 0 && Sdolist != null)
            {

                string condition = "";

                for (int i = 0; i < Sdolist.Count; i++)
                {
                    if (i == 0)
                    {
                        condition = condition + "Where  ATCWorldToArtShipData.SDONo='" + Sdolist[i].ToString().Trim()+"'";
                    }
                    else
                    {
                        condition = condition + "  or ATCWorldToArtShipData.SDONo='" + Sdolist[i].ToString().Trim() + "'";
                    }



                }


                BLL.ProductionBLL.ShipmentHandOverMasterData SHPMSTR = new BLL.ProductionBLL.ShipmentHandOverMasterData();

                tbl_podetails.DataSource = SHPMSTR.GetSDOData(condition,int.Parse (drp_factory.SelectedValue.ToString ()));
                tbl_podetails.DataBind();
            }
        }

        protected void btn_submitShipment_Click(object sender, EventArgs e)
        {
            string msg = "";
            String num = "";
            BLL.ProductionBLL.ShipmentHandOverMasterData SHPMSTR = new BLL.ProductionBLL.ShipmentHandOverMasterData();
            SHPMSTR.LocationPK_pk = int.Parse(drp_factory.SelectedValue.ToString());
            SHPMSTR.ShipmentHandOverMasterDataCollection = GetShipmentHandOverMasterData();

            num = SHPMSTR.insertShipmentHandOverWithSDO(SHPMSTR);

            msg = "Shipment HandOver # : " + num + " is generated Sucessfully";
            tbl_podetails.DataSource = null;
            tbl_podetails.DataBind();
            MessgeboxUpdate("sucess", msg);
        }

        public List<BLL.ProductionBLL.ShipmentHandOverData> GetShipmentHandOverMasterData()
        {

            List<BLL.ProductionBLL.ShipmentHandOverData> rk = new List<BLL.ProductionBLL.ShipmentHandOverData>();


            foreach (GridViewRow di in tbl_podetails.Rows)
            {
                

                        
                  

                    int lbl_OurStyleID = int.Parse(((di.FindControl("lbl_OurStyleID") as Label).Text.ToString()));

                    int lbl_POPackId = int.Parse(((di.FindControl("lbl_POPackId") as Label).Text.ToString()));
                int lblProductionArtLocation_PK = int.Parse(((di.FindControl("lblProductionArtLocation_PK") as Label).Text.ToString()));

                string lbl_SDONo = (di.FindControl("lbl_SDONo") as Label).Text.ToString().Trim();
                    int lbl_qty = int.Parse(((di.FindControl("lbl_qty") as Label).Text.ToString()));


                DateTime lbl_ShipmentDate= DateTime.Parse(((di.FindControl("lbl_ShipmentDate") as Label).Text.ToString()));







                BLL.ProductionBLL.ShipmentHandOverData shpdet = new BLL.ProductionBLL.ShipmentHandOverData();
                    shpdet.Popackid = lbl_POPackId;
                    shpdet.OurStyleId = lbl_OurStyleID;
                    shpdet.SDO = lbl_SDONo;
                    shpdet.ShipmenthandOverdate = DateTime.Parse(shipdate.Value.ToString());
                    shpdet.ShippedQty = int.Parse(lbl_qty.ToString());
                  shpdet.ProducedLctn_PK = int.Parse(lblProductionArtLocation_PK.ToString ());
                    shpdet.AddedBy = Session["Username"].ToString().Trim();
                    shpdet.AddedDate = DateTime.Now;
                shpdet.ShipmentDate = lbl_ShipmentDate;
                    rk.Add(shpdet);
               
            }
            return rk;


        }


        public void MessgeboxUpdate(String Messagetype, String Messg)
        {
            if (Messagetype == "sucess")
            {
                Messaediv.Attributes["class"] = "success";
                Messaediv.InnerText = Messg;
            }
            else
            {

            }
        }
        //Variable to hold the total value
        int totalvalue = 0;
        protected void tbl_podetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //Check if the current row is datarow or not
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //Add the value of column
                totalvalue += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "ShipQty"));
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                //Find the control label in footer 
                Label lblamount = (Label)e.Row.FindControl("lblTotalValue");
                //Assign the total value to footer label control
                lblamount.Text = "Total Qty is : " + totalvalue.ToString();
            }
        }
    }
}
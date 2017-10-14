using ArtWebApp.BLL.ProductionBLL;
using ArtWebApp.DataModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArtWebApp.Production.Cutting
{
    public partial class RejectionGarmentFab : System.Web.UI.Page
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
                         where order.LocType == "F"
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
        protected void btn_atc_Click(object sender, EventArgs e)
        {
            FillOurStyleCombo(int.Parse(drp_atc.SelectedValue.ToString()));
        }

        protected void btn_OURSTYLE_Click(object sender, EventArgs e)
        {
            DataTable dt = RejectionPanelFunction.GetPendingRejectionRequest(int.Parse(drp_ourstyle.SelectedItem.Value.ToString()), int.Parse(drp_fact.SelectedItem.Value.ToString()));


            tbl_podetails.DataSource = dt;
            tbl_podetails.DataBind();
            upd_grid.Update();
         }
        public void insertmrn()
        {
            String mrnum = "";
            RejectReqMasterData prrrcpt = new RejectReqMasterData();
            prrrcpt.Location_PK = int.Parse(drp_fact.SelectedItem.Value.ToString());
            prrrcpt.AtcID = int.Parse(drp_atc.SelectedItem.Value.ToString());
            prrrcpt.RejectReqDetailsDataCollection = GetMRnDetailsData();

            mrnum = prrrcpt.InsertFullgarmentRejectionExtraRequest();


            String msg = "Full garment rejeion req  # :   " + mrnum + "  is generated Sucessfully";


            tbl_podetails.DataSource = null;
            tbl_podetails.DataBind();


            MessageBoxShow(msg);

        }

        public void MessageBoxShow(String msg)
        {
            MessgeboxUpdate("sucess", msg);
            ClientScript.RegisterStartupScript(this.GetType(), "Art", "alert('" + msg + "');", true);
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
                Messaediv.Attributes["class"] = "error-message ";
                Messaediv.InnerText = Messg;
            }
         
        }
        public List<RejectReqDetailsData> GetMRnDetailsData()
        {

            List<RejectReqDetailsData> rk = new List<RejectReqDetailsData>();
            foreach (GridViewRow di in tbl_podetails.Rows)
            {
                CheckBox chkBx = (CheckBox)di.FindControl("chk_select");

                if (chkBx != null && chkBx.Checked)
                {
                    int lbl_RejFabReqID = int.Parse(((di.FindControl("lbl_RejFabReqID") as Label).Text.ToString()));
                 
                    decimal txt_allowed = decimal.Parse((di.FindControl("txt_allowed") as TextBox).Text.ToString());


                    RejectReqDetailsData mrndetdata = new RejectReqDetailsData ();

                    mrndetdata.RejFabReqID = lbl_RejFabReqID;
                    mrndetdata.AllowedQty = txt_allowed;
                 
                  
                  
                    rk.Add(mrndetdata);
                }
            }



            return rk;


        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            insertmrn();
        }
    }
}
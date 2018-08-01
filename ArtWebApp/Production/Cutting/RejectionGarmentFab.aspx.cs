using ArtWebApp.BLL.ProductionBLL;
using ArtWebApp.DataModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using ArtWebApp.Areas.Inventory;
using System.Web.UI.WebControls;
using System.Collections;

namespace ArtWebApp.Production.Cutting
{
    public partial class RejectionGarmentFab : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillAtcCombo();
                Fillbodymaster();
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
        protected void Chk_select_CheckedChanged1(object sender, EventArgs e)
        {
            try
            {
                if (drp_ourstyle.SelectedItem.Value != null)
                {
                    RejectReqMasterData prrrcpt = new RejectReqMasterData();
                    InventoryRepo inve = new InventoryRepo();

                    CheckBox chkbox = (CheckBox)sender;
                    GridViewRow currentRow = chkbox.ClosestContainer<GridViewRow>();

                    if (chkbox.Checked == true)
                    {
                        DataTable dt = inve.GetBodyParts();
                        UpdatePanel upd_fabrication = (currentRow.FindControl("upd_fabrication") as UpdatePanel);
                        
                        DropDownList drp_cut = (currentRow.FindControl("dd_fabrication") as DropDownList);
                        drp_cut.DataSource = dt;
                        drp_cut.DataTextField = "BodyPartName";
                        drp_cut.DataValueField = "BodyPart_PK";
                        drp_cut.DataBind();

                        upd_fabrication.Update();
                        drp_cut.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select Cut#"));
                    }

                }
            }
            catch (Exception)
            {

                MessgeboxUpdate("error", "Location Not Selected");
            }
        }
        protected void btn_atc_Click(object sender, EventArgs e)
        {
            FillOurStyleCombo(int.Parse(drp_atc.SelectedValue.ToString()));
        }


        public ArrayList Getbodymasterlist()
        {
            ArrayList bodymasterlist = new ArrayList();
            List<Infragistics.Web.UI.ListControls.DropDownItem> items = drp_bodypart.SelectedItems;
            foreach (Infragistics.Web.UI.ListControls.DropDownItem item in items)
            {

                int bodypk = int.Parse(item.Value.ToString());
                String part = item.Text.ToString();
                bodymasterlist.Add(part);
            }
            return bodymasterlist;
        }



        public void Fillbodymaster()
        {
            using (ArtEntitiesnew entty = new ArtEntitiesnew())
            {
                entty.Configuration.AutoDetectChangesEnabled = false;
                var q = from atcorder in entty.BodyPartMasters
                        select new
                        {
                            name = atcorder.BodyPartName,
                            pk = atcorder.BodyPart_PK
                        };


                drp_bodypart.DataSource = q.ToList();
                drp_bodypart.DataBind();
                //// Create a table from the query.
                //drp_Atc.DataSource = q.ToList();
                //drp_Atc.DataBind();




            }
        }



        protected void btn_OURSTYLE_Click(object sender, EventArgs e)
        {
            InventoryRepo inve = new InventoryRepo();
            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            dt1.Columns.Add("Fabreqid");
            dt1.Columns.Add("Fabreqno");
            dt1.Columns.Add("RejFabReqID");
            dt1.Columns.Add("DepartmentName");
            dt1.Columns.Add("ReqQty");
            dt1.Columns.Add("ColorName");
            dt1.Columns.Add("OurStyle");
            dt1.Columns.Add("LocationName");
            dt1.Columns.Add("Allowedfabric");
            dt1.Columns.Add("IsApproved");
            dt1.Columns.Add("OurStyleID");
            dt1.Columns.Add("Location_PK");
            dt1.Columns.Add("parts");
            dt1.Columns.Add("Reqdate");



            ArrayList LocArraylist = Getbodymasterlist();
            if (LocArraylist.Count > 0 && LocArraylist != null)
            {
                for (int i = 0; i < LocArraylist.Count; i++)
                {
                    dt = RejectionPanelFunction.GetPendingRejectionRequest(int.Parse(drp_ourstyle.SelectedItem.Value.ToString()), int.Parse(drp_fact.SelectedItem.Value.ToString()), LocArraylist[i].ToString());
                    foreach(DataRow row in dt.Rows)
                    {
                        DataRow row1 = dt1.NewRow();
                        row1["Fabreqid"] = row["Fabreqid"];
                        row1["Fabreqno"] = row["Fabreqno"];
                        row1["RejFabReqID"] = row["RejFabReqID"];
                        row1["DepartmentName"] = row["DepartmentName"];
                        row1["ReqQty"] = row["ReqQty"];
                        row1["ColorName"] = row["ColorName"];
                        row1["OurStyle"] = row["OurStyle"];
                        row1["LocationName"] = row["LocationName"];
                        row1["Allowedfabric"] = row["Allowedfabric"];
                        row1["IsApproved"] = row["IsApproved"];
                        row1["OurStyleID"] = row["OurStyleID"];
                        row1["Location_PK"] = row["Location_PK"];
                        row1["parts"] = row["parts"];
                        row1["Reqdate"] = row["Reqdate"];
                        try
                        {
                            dt1.Rows.Add(row1);
                        }
                        catch (Exception exp)
                        {

                            throw;
                        }
                    }
                   
                   
                    
                }
                    

            }
            tbl_podetails.DataSource = dt1;
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
                    decimal lbl_reqQty = decimal.Parse((di.FindControl("lbl_reqQty") as Label).Text.ToString());
                    
                    RejectReqDetailsData mrndetdata = new RejectReqDetailsData ();

                    mrndetdata.RejFabReqID = lbl_RejFabReqID;
                    mrndetdata.AllowedQty = txt_allowed;
                    mrndetdata.GarmentQty = lbl_reqQty;
                    


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
using ArtWebApp.DataModels;
using ArtWebApp.DBTransaction;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ArtWebApp.BLL.InventoryBLL;
using System.Collections;

namespace ArtWebApp.Inventory.DeliveryOrder
{
    public partial class DeliveryOrderFabricToFactoryRollWise : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                filltowarehouses(int.Parse(Session["UserLoc_pk"].ToString()));
                
               



            }
        }

        protected void btn_confirmAtc_Click(object sender, EventArgs e)
        {
            DeliveryOrdertransaction dotran = new DeliveryOrdertransaction();
            DataTable dt = dotran.GetStockItemDetails(int.Parse(cmb_atc.SelectedValue.ToString()), int.Parse(Session["UserLoc_pk"].ToString()), "Fabric");
            tbl_InverntoryDetails.DataSource = dt;
            tbl_InverntoryDetails.DataBind();
            DataTable table = new DataTable();
            table.Columns.Add("InventoryItem_PK", typeof(int));
            table.Columns.Add("roll_Pk", typeof(int));
            table.Columns.Add("Cutid", typeof(int));
            table.Columns.Add("ayard", typeof(decimal));

            ViewState["Rolldata"] = table;

            btn_saveDO.Enabled = true;
            UpdatePanel1.Update();
        }


        public void filltowarehouses(int location_pk)
        {
            using (ArtEntitiesnew entty = new ArtEntitiesnew())
            {
                var q = from order in entty.LocationMasters
                        join fwm in entty.FactWareLinkMasters on order.Location_PK equals fwm.ToLoc_PK
                        where fwm.FromLoc_pk == location_pk
                        select new
                        {
                            name = order.LocationName,
                            pk = order.Location_PK
                        };
                var q1 = from order in entty.DeliveryMethodMasters
                         select new
                         {
                             name = order.DeliveryMethod,
                             pk = order.Deliverymethod_Pk
                         };
                // Create a table from the query.
                drp_ToWarehouse.DataSource = q.ToList();
                drp_ToWarehouse.DataValueField = "pk";
                drp_ToWarehouse.DataTextField = "name";
                drp_ToWarehouse.DataBind();

                drp_deliverymode.DataSource = q1.ToList();
                drp_deliverymode.DataValueField = "pk";
                drp_deliverymode.DataTextField = "name";
                drp_deliverymode.DataBind();
                // Bind the table to a System.Windows.Forms.BindingSource object, 
                // which acts as a proxy for a System.Windows.Forms.DataGridView object.

            }
        }

        protected void btn_saveDO_Click(object sender, EventArgs e)
        {
            if (CheckdatagridValue(tbl_InverntoryDetails, "lbl_balacetocut", "txt_deliveryQty"))
            {
                if (CheckdatagridValue(tbl_InverntoryDetails, "lbl_OnhandQty", "txt_deliveryQty"))
                {
                    btn_saveDO.Enabled = false;
                    string msg = InsertDOdata();
                    MessgeboxUpdate("sucess", msg);
                }

            }

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
                Messaediv.Attributes["class"] = "error-message";
                Messaediv.InnerText = Messg;
            }
        }
        protected void chk_select_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (drp_ToWarehouse.SelectedItem.Value != null)
                {
                    BLL.CutOrderBLL.CutOrderData cdata = new BLL.CutOrderBLL.CutOrderData();
                    CheckBox chkbox = (CheckBox)sender;
                    GridViewRow currentRow = chkbox.ClosestContainer<GridViewRow>();

                    if (chkbox.Checked == true)
                    {



                        int iipk = int.Parse((currentRow.FindControl("lblInventoryItem_PK") as Label).Text);
                        DataTable dt = cdata.GetCutOrderData(iipk, int.Parse(drp_ToWarehouse.SelectedItem.Value.ToString()));
                        UpdatePanel upd_cutorder = (currentRow.FindControl("upd_cutorder") as UpdatePanel);
                        DropDownList drp_cut = (currentRow.FindControl("ddl_cutorder") as DropDownList);
                        drp_cut.DataSource = dt;
                        drp_cut.DataTextField = "Cut_NO";
                        drp_cut.DataValueField = "CutID";
                        drp_cut.DataBind();
                        upd_cutorder.Update();
                        drp_cut.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select Cut#"));
                    }

                }
            }
            catch (Exception)
            {

                MessgeboxUpdate("error", "Location Not Selected");
            }
        }



        public String InsertDOdata()
        {



            BLL.InventoryBLL.DeliveryOrder dodata = new BLL.InventoryBLL.DeliveryOrder();

            dodata.Domstrdata = GetDoMasterData();
            dodata.DeliveryOrderDetailsDataCollection = GetDeliveryOrderDetailsData();
            dodata.DeliveryRollDetailsData = (DataTable)(ViewState["Rolldata"]);
            String donum = dodata.insertFactoryFabricROLLDO(dodata);
            tbl_InverntoryDetails.DataSource = null;
            tbl_InverntoryDetails.DataBind();

            String msg = "DO # : " + donum + " is generated Successfully";

            ViewState["Rolldata"] = null;

            return msg;
        
        }




        public BLL.InventoryBLL.DeliveryOrderMasterData GetDoMasterData()
        {
            BLL.InventoryBLL.DeliveryOrderMasterData domstr = new BLL.InventoryBLL.DeliveryOrderMasterData();
            domstr.AtcID = int.Parse(cmb_atc.SelectedValue.ToString()); ;
            domstr.AddedDate = DateTime.Now;
            domstr.ContainerNumber = txt_containernum.Text;
            domstr.DeliveryDate = DateTime.Parse(dtp_dodate.Value.ToString());
            domstr.BoeNum = txt_containernum.Text;
            domstr.AddedBy = Session["Username"].ToString().Trim();
            domstr.ToLocation_PK = int.Parse(drp_ToWarehouse.SelectedValue.ToString()); ;
            domstr.FromLocation_PK = int.Parse(Session["UserLoc_pk"].ToString());
            domstr.DoType = "WF";

            return domstr;
        }

        public List<BLL.InventoryBLL.DeliveryOrderDetailsData> GetDeliveryOrderDetailsData()
        {

            List<BLL.InventoryBLL.DeliveryOrderDetailsData> rk = new List<BLL.InventoryBLL.DeliveryOrderDetailsData>();


            foreach (GridViewRow di in tbl_InverntoryDetails.Rows)
            {
                CheckBox chkBx = (CheckBox)di.FindControl("chk_select");

                if (chkBx != null && chkBx.Checked)
                {
                    int cutid = 0;
                    int item_pk = int.Parse(((di.FindControl("lblInventoryItem_PK") as Label).Text.ToString()));
                    decimal deliveryqty = decimal.Parse(((di.FindControl("txt_deliveryQty") as TextBox).Text.ToString()));

                    cutid = int.Parse(((di.FindControl("ddl_cutorder") as DropDownList).SelectedValue.ToString()));

                    BLL.InventoryBLL.DeliveryOrderDetailsData deldet = new BLL.InventoryBLL.DeliveryOrderDetailsData();
                    deldet.InventoryItem_PK = item_pk;
                    deldet.DeliveryQty = deliveryqty;
                    deldet.Cutid = cutid;
                    rk.Add(deldet);
                }
            }
            return rk;


        }









        protected void ddl_cutorder_SelectedIndexChanged(object sender, EventArgs e)
        {
            BLL.CutOrderBLL.CutOrderData cdata = new BLL.CutOrderBLL.CutOrderData();
            DropDownList dll_cutorder = (DropDownList)sender;
            GridViewRow currentRow = dll_cutorder.ClosestContainer<GridViewRow>();
            Label lbl_balacetocut = currentRow.FindControl("lbl_balacetocut") as Label;
            UpdatePanel upd_balacetocut = currentRow.FindControl("upd_balacetocut") as UpdatePanel;
            if (dll_cutorder.Text != "Select Cut#")
            {
                int cutid = int.Parse(dll_cutorder.SelectedValue.ToString());

                int balqty = cdata.GetbalanceQty(cutid);

                lbl_balacetocut.Text = balqty.ToString();
                upd_balacetocut.Update();
                UpdatePanel1.Update();

            }

        }






        public Boolean CheckdatagridValue(GridView tblgrid, String lbl_Qty1, String txt_Qty2)
        {

            Boolean isQtyok = true;
            for (int i = 0; i < tblgrid.Rows.Count; i++)
            {
                GridViewRow currentRow = tblgrid.Rows[i];
                CheckBox chkBx = (CheckBox)currentRow.FindControl("chk_select");

                if (chkBx != null && chkBx.Checked)
                {
                    try
                    {

                        float AllowedQty = float.Parse(((tblgrid.Rows[i].FindControl(lbl_Qty1) as Label).Text.ToString()));
                        float Enterqty = float.Parse(((tblgrid.Rows[i].FindControl(txt_Qty2) as TextBox).Text.ToString()));
                        if (!QuantityValidator.ISFloatQuantityLesser(AllowedQty, Enterqty))
                        {
                            isQtyok = false;
                            (tblgrid.Rows[i].FindControl(txt_Qty2) as TextBox).BackColor = System.Drawing.Color.Red;


                        }
                        else
                        {
                            (tblgrid.Rows[i].FindControl(txt_Qty2) as TextBox).BackColor = System.Drawing.Color.White;
                        }

                    }
                    catch (Exception)
                    {
                        isQtyok = false;
                        (tblgrid.Rows[i].FindControl(txt_Qty2) as TextBox).BackColor = System.Drawing.Color.Red;

                    }
                }







            }
            return isQtyok;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

        }

        protected void ServerButton_Click(object sender, EventArgs e)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "key", "launchModal();", true);
        }

        protected void tbl_InverntoryDetails_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = tbl_InverntoryDetails.Rows[index];
            if (e.CommandName == "ShowRoll")
            {






                CheckBox chkBx = (CheckBox)row.FindControl("chk_select");
                if (chkBx.Checked == true)
                {
                    int InventoryItem_PK = int.Parse((row.FindControl("lblInventoryItem_PK") as Label).Text);
                    Session["InventoryItem_PK"] = InventoryItem_PK;

                    try
                    {
                        
                        int cutid = int.Parse(((row.FindControl("ddl_cutorder") as DropDownList).SelectedValue.ToString()));
                        Session["cutid"] = cutid;




                        Boolean isexeptionalAtc = false;
                        using (ArtEntitiesnew entty = new ArtEntitiesnew())
                        {

                            int atcid = int.Parse(cmb_atc.SelectedValue.ToString());


                            if (!entty.ExceptionAtcLists.Any(f =>f.AtcID == atcid ))
                            {
                                isexeptionalAtc = false;
                            }
                            else
                            {
                                isexeptionalAtc = true;
                            }

                            }
                        DataTable dt = new DataTable();
                        if (isexeptionalAtc)
                        {
                             dt = BLL.InventoryBLL.RollTransactionBLL.getFabricRollofAItemPKandCutorder(InventoryItem_PK, cutid,int.Parse(Session["UserLoc_pk"].ToString ()));

                        }
                        else
                        {
                          dt = BLL.InventoryBLL.RollTransactionBLL.getFabricRollofAItemPKandCutorderAgainstCutPlan(InventoryItem_PK, cutid, int.Parse(Session["UserLoc_pk"].ToString()));

                        }



                        if (dt.Rows.Count > 0)
                        {
                            DataView view = new DataView(dt);
                            DataTable shadetable = view.ToTable(true, "ShadeGroup");
                            drp_shade.DataSource = shadetable;
                          
                            drp_shade.DataBind();
                           

                            ViewState["rolldatafordo"] = null;


                            ViewState["rolldatafordo"] = dt;



                            if(dt.Rows.Count>0)
                            {
                                String shrinkagegrpe = dt.Rows[0]["ShrinkageGroup"].ToString();
                                String WidthGroup = dt.Rows[0]["WidthGroup"].ToString();
                                String MarkerType = dt.Rows[0]["MarkerType"].ToString();


                                lbl_shringagegroup.Text = shrinkagegrpe;
                                lbl_markerType.Text = MarkerType;
                                lbl_widthgroup.Text = WidthGroup;
                            }
                           


                            tbl_rolldata.DataSource = dt;
                            tbl_rolldata.DataBind();
                            Upd_roll.Update();
                            upd_shade.Update();
                            ModalPanel.Visible = true;
                        }
                        else
                        {
                            ModalPanel.Visible = false;
                            MessgeboxUpdate("error", "No Roll Data Found");
                        }

                    }
                    catch (Exception)
                    {

                       
                    }





                }



            }
            else if (e.CommandName == "DeleteRoll")
            {
                DataTable dt = (DataTable)ViewState["Rolldata"];
                int InventoryItem_PK = int.Parse((row.FindControl("lblInventoryItem_PK") as Label).Text);
                var rows = dt.Select("InventoryItem_PK ="+ InventoryItem_PK.ToString ());
                foreach (var drow in rows)
                    drow.Delete();

                dt.AcceptChanges();

                (row.FindControl("lblInventoryItem_PK") as Label).Text = "0";
            }
        }

        protected void btn_confirmRolls_Click(object sender, EventArgs e)
        {

            GetRollDetailsData();

            fillrollsum();
           
            tbl_rolldata.DataSource = null;
            tbl_rolldata.DataBind();
            Upd_roll.Update();
            ModalPanel.Visible = false;

        }


      





        public void fillrollsum()
        {
            foreach (GridViewRow di in tbl_InverntoryDetails.Rows)
            {
                int InventoryItem_PK = int.Parse((di.FindControl("lblInventoryItem_PK") as Label).Text);

                if (InventoryItem_PK== int.Parse(Session["InventoryItem_PK"].ToString()))
                {
                    Label lbl_RollYard = di.FindControl("lbl_RollYard") as Label;
                    UpdatePanel upd_RollYard = di.FindControl("upd_RollYard") as UpdatePanel;


                    DataTable dt = (DataTable)ViewState["Rolldata"];


                    object rollsum = dt.Compute("Sum(Ayard)", "");


                    lbl_RollYard.Text = rollsum.ToString();



                    upd_RollYard.Update();
                }
            }
        }




        public void GetRollDetailsData()
        {



            DataTable dt = (DataTable)ViewState["Rolldata"];

            if(dt==null)
            {
                dt = new DataTable();
            }


            foreach (GridViewRow di in tbl_rolldata.Rows)
            {
                CheckBox chkBx = (CheckBox)di.FindControl("chk_select");

                if (chkBx != null && chkBx.Checked)
                {
                  
               
                    int  roll_PK = int.Parse(((di.FindControl("lbl_Roll_PK") as Label).Text.ToString()));
                    decimal AYard = decimal.Parse(((di.FindControl("lbl_AYard") as Label).Text.ToString()));
                     int  cutid = int.Parse(Session["cutid"].ToString());
                    int InventoryItem_PK = int.Parse(Session["InventoryItem_PK"].ToString());
               

                    //BLL.InventoryBLL.DeliveryRollDetails deldet = new BLL.InventoryBLL.DeliveryRollDetails();
                    //deldet.InventoryItem_PK = InventoryItem_PK;
                    //deldet.roll_Pk = roll_PK;
                    //deldet.Cutid = cutid;
                    //deldet.ayard = AYard;
                    //rk.Add(deldet);

                    
                    dt.Rows.Add(InventoryItem_PK, roll_PK, cutid, AYard);
                 

                }
            }
            ViewState["Rolldata"] = dt;


        }

        protected void tbl_rolldata_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            tbl_rolldata.DataSource = null;
            tbl_rolldata.DataBind();
            Upd_roll.Update();
            ModalPanel.Visible = false;
        }

        protected void Button1_Click1(object sender, EventArgs e)
        {

            DataTable dt = (DataTable)ViewState["rolldatafordo"];


            ArrayList shadegroup = new ArrayList();
            List<Infragistics.Web.UI.ListControls.DropDownItem> items = drp_shade.SelectedItems;
            foreach (Infragistics.Web.UI.ListControls.DropDownItem item in items)
            {

                String popackid = item.Value.ToString();
                shadegroup.Add(popackid);
            }


            if (shadegroup.Count > 0 && shadegroup != null)
            {
                string condition = "";
                for (int i = 0; i < shadegroup.Count; i++)
                {



                    if (i == 0)
                    {
                        condition = condition + " ShadeGroup='" + shadegroup[i].ToString().Trim() + "'";
                    }
                    else
                    {
                        condition = condition + "  or  ShadeGroup='" + shadegroup[i].ToString().Trim() + "'";
                    }



                }
                dt = dt.Select(condition).CopyToDataTable();
               
            }



            tbl_rolldata.DataSource = dt;
            tbl_rolldata.DataBind();
        }

      
    }

}





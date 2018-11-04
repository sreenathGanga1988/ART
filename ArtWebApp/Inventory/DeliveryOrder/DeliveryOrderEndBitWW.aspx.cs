using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ArtWebApp.DBTransaction;
using System.Data;
using ArtWebApp.DataModels;

namespace ArtWebApp.Inventory.DeliveryOrder
{
    public partial class DeliveryOrderEndBitWW : System.Web.UI.Page
    {
        private ArtEntitiesnew enty = new ArtEntitiesnew();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                filltowarehouses();
                txt_weight.Text = "0";
            }

        }

        public void filltowarehouses()
        {
            using (ArtEntitiesnew entty = new ArtEntitiesnew())
            {
                var q = from order in entty.LocationMasters
                        where order.LocType == "W"
                        select new
                        {
                            name = order.LocationName,
                            pk = order.Location_PK
                        };
                
                // Create a table from the query.
                drp_toWarehouse .DataSource = q.ToList();
                drp_toWarehouse.DataValueField = "pk";
                drp_toWarehouse.DataTextField = "name";
                drp_toWarehouse.DataBind();

            }
        }
        protected void btn_confirmAtc_Click(object sender, EventArgs e)
        {
            DeliveryOrdertransaction dotran = new DeliveryOrdertransaction();
            DataTable dt = new DataTable();
            if (chk_oldendbit.Checked == true)
            {
                 dt = dotran.GetOldEndBitStockDetails(int.Parse(Session["UserLoc_pk"].ToString()));
            }
            else
            {
                dt = dotran.GetEndBitStockDetails(int.Parse(Session["UserLoc_pk"].ToString()));
            }
                tbl_InverntoryDetails.DataSource = dt;
                tbl_InverntoryDetails.DataBind();
                if (dt.Rows.Count >0)
                {
                    decimal totalEndbit = dt.AsEnumerable().Sum(row => row.Field<decimal>("OnhandQty"));
                    tbl_InverntoryDetails.FooterRow.Cells[13].Text = totalEndbit.ToString("N2");
                }

                DataTable table = new DataTable();
                table.Columns.Add("Skudet_pk", typeof(int));
                table.Columns.Add("roll_Pk", typeof(int));
                table.Columns.Add("OnhandQty", typeof(decimal));

                ViewState["Rolldata"] = table;

            

            btn_saveDO.Enabled = true;
        }

        protected void btn_confirminvdet_Click(object sender, EventArgs e)
        {
            lbl_ebdbit.Text = "0";


            lbl_ebdbit.Text = calculategridQty().ToString();
            upd_endbit.Update();
        }
        public float calculategridQty()
        {
            float Invqty = 0;
            for (int i = 0; i < tbl_InverntoryDetails.Rows.Count; i++)
            {
                GridViewRow currentRow = tbl_InverntoryDetails.Rows[i];
                CheckBox chkBx = (CheckBox)currentRow.FindControl("chk_select");

                if (chkBx != null && chkBx.Checked)
                {
                    try
                    {
                        Invqty = Invqty + float.Parse(((tbl_InverntoryDetails.Rows[i].FindControl("txt_deliveryQty") as TextBox).Text.ToString()));
                    }
                    catch (Exception)
                    {

                    }
                }
            }
            return Invqty;
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
        protected void btn_saveDO_Click(object sender, EventArgs e)
        {
            String msg = "";
            decimal weight= decimal.Parse(txt_weight.Text.ToString());
            DateTime dodate = DateTime.Parse(dtp_dodate.Value.ToString());
            int fromloc= int.Parse(Session["UserLoc_pk"].ToString());
            int toloc= int.Parse(drp_toWarehouse.SelectedValue.ToString());
            if (weight <= 0)
            {
                MessgeboxUpdate("Error", "Please Check Weight" );
            }
            
            //else if (dodate < DateTime.Now)
            //{
            //    MessgeboxUpdate("Error", "Please Check DO Date");
            //}
            else if(fromloc ==toloc)
            {
                MessgeboxUpdate("Error", "Please Select Different Location to Transfer");
            }
            else
            {
                btn_saveDO.Enabled = false;
                msg = InsertDOdata();
                tbl_InverntoryDetails.DataSource = null;
                tbl_InverntoryDetails.DataBind();
                MessgeboxUpdate("sucess", "EndBit Do is Generated   " + msg);
            }
            
            


        }
        public string InsertDOdata()
        {
            String Donum = "";
            

            EndbitDoMaster dodata = new EndbitDoMaster();
            int toloc = int.Parse(drp_toWarehouse.SelectedValue.ToString());
            dodata.FromLocation = int.Parse(Session["UserLoc_pk"].ToString());
            dodata.ToLocation = int.Parse(drp_toWarehouse.SelectedValue.ToString()); 
            dodata.TotalWeight = decimal.Parse(txt_weight.Text.ToString());
            dodata.TotalYds = decimal.Parse(lbl_ebdbit.Text.ToString());
            dodata.DeliveryDate = DateTime.Parse(dtp_dodate.Value.ToString());
            dodata.AddedDate = DateTime.Now;
            dodata.AddedBy = Session["Username"].ToString().Trim();
            enty.EndbitDoMasters.Add(dodata);
            enty.SaveChanges();
            Donum = dodata.DoNum = CodeGenerator.GetUniqueCode("END", Session["lOC_Code"].ToString().Trim(), int.Parse(dodata.Do_pk.ToString()));
            foreach (GridViewRow di in tbl_InverntoryDetails.Rows)
            {
                CheckBox chkBx = (CheckBox)di.FindControl("chk_select");

                if (chkBx != null && chkBx.Checked)
                {
                    EndbitDoDetail dodet = new EndbitDoDetail();
                    int inv_pk = 0;
                    int endbit_pk = int.Parse(((di.FindControl("lblEndbit_pk") as Label).Text.ToString()));
                    int roll_pk = int.Parse(((di.FindControl("lblRoll_PK") as Label).Text.ToString()));
                    int skudet_pk = int.Parse(((di.FindControl("lblSkuDet_PK") as Label).Text.ToString()));
                    Decimal endbit = decimal.Parse(((di.FindControl("lblOnhandQty") as Label).Text.ToString()));
                    Decimal txt_deliveryQty = decimal.Parse(((di.FindControl("txt_deliveryQty") as TextBox).Text.ToString()));
                    Decimal weight = decimal.Parse(txt_weight.Text.ToString());
                    dodet.Do_pk = dodata.Do_pk;
                    dodet.Roll_pk = roll_pk;
                    dodet.Skudet_pk = skudet_pk;
                    dodet.Endbit = txt_deliveryQty;
                    dodet.weight = weight;
                    enty.EndbitDoDetails.Add(dodet);
                    enty.SaveChanges();
                    var q = from end in enty.EndbitInventories where end.Endbit_Pk == endbit_pk select end;
                    foreach(var element in q)
                    {
                        element.DeliveredQty = txt_deliveryQty;
                        element.OnHandQty = element.OnHandQty- txt_deliveryQty;
                        if (chk_oldendbit.Checked == true)
                        {
                            inv_pk = int.Parse(element.Inventoryitem_pk.ToString());
                        }                        
                    }
                    dodet.Inventoryitem_pk = inv_pk;
                    EndbitInventory endbitInventory = new EndbitInventory();
                    endbitInventory.Roll_Pk = roll_pk;
                    endbitInventory.Skudet_pk = skudet_pk;
                    endbitInventory.Location_pk = int.Parse(drp_toWarehouse.SelectedValue.ToString()); 
                    endbitInventory.OnHandQty = txt_deliveryQty;
                    endbitInventory.ReceivedQty = txt_deliveryQty;
                    endbitInventory.Inventoryitem_pk = inv_pk;
                    endbitInventory.DeliveredQty = 0;
                    endbitInventory.RefNum = Donum;
                    enty.EndbitInventories.Add(endbitInventory);
                    enty.SaveChanges();
                }
            }
            enty.SaveChanges();

            return Donum;
        }
    }
}
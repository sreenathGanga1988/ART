using ArtWebApp.DataModels;
using ArtWebApp.DBTransaction;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArtWebApp.Inventory.Sales
{
    
    public partial class ExternalSalesEndbit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                filltowarehouses();
            }

        }
        public void filltowarehouses()
        {
            using (ArtEntitiesnew entty = new ArtEntitiesnew())
            {
                var q = from order in entty.LocalBuyerMasters
                        select new
                        {
                            name = order.LocalBuyerName,
                            pk = order.LocalBuyer_PK
                        };
                var q1 = from order in entty.DeliveryMethodMasters
                         select new
                         {
                             name = order.DeliveryMethod,
                             pk = order.Deliverymethod_Pk
                         };
                // Create a table from the query.
                drp_toWarehouse.DataSource = q.ToList();
                drp_toWarehouse.DataBind();

                drp_deliverymode.DataSource = q1.ToList();
                drp_deliverymode.DataBind();

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
            if (dt.Rows.Count>0)
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

            btn_saveDO.Enabled = false;
            string msg = InsertDOdata();
            tbl_InverntoryDetails.DataSource = null;
            tbl_InverntoryDetails.DataBind();
            MessgeboxUpdate("sucess", "EndBit Do is Generated   " + msg);


        }
        public string InsertDOdata()
        {
            String Donum = "";
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {

                EndbitSalesMaster trnmstr = new EndbitSalesMaster();

                trnmstr.SalesDate = DateTime.Parse(dtp_dodate.Value.ToString()); 

                trnmstr.FromLocation_PK = int.Parse(Session["UserLoc_pk"].ToString());
                trnmstr.ToLocation_PK = int.Parse(drp_toWarehouse.SelectedValue.ToString());
                trnmstr.ContainerNumber = txt_containernum.Text;
                trnmstr.Deliverymethod_Pk = int.Parse(drp_deliverymode.SelectedValue.ToString());
                trnmstr.SalesDODate = DateTime.Now;
                trnmstr.ISApproved = "N";
                trnmstr.DoType = "External";
                trnmstr.AddedBy = HttpContext.Current.Session["Username"].ToString().Trim();
                trnmstr.AddedDate = DateTime.Now;                
                trnmstr.IsDebited = "N";
                trnmstr.Weight = decimal.Parse(txt_weight.Text.ToString());
                trnmstr.Price= decimal.Parse(txt_price.Text.ToString());
                enty.EndbitSalesMasters.Add(trnmstr);
                enty.SaveChanges();
                
                Donum = trnmstr.SalesDONum= CodeGenerator.GetUniqueCode("ESDO", Session["lOC_Code"].ToString().Trim(), int.Parse(trnmstr.SalesDO_PK.ToString()));

                foreach (GridViewRow di in tbl_InverntoryDetails.Rows)
                {
                    CheckBox chkBx = (CheckBox)di.FindControl("chk_select");

                    if (chkBx != null && chkBx.Checked)
                    {
                        EndbitSalesDetail sinvdetdb = new EndbitSalesDetail();

                        int endbit_pk = int.Parse(((di.FindControl("lblEndbit_pk") as Label).Text.ToString()));
                        int roll_pk = int.Parse(((di.FindControl("lblRoll_PK") as Label).Text.ToString()));
                        int skudet_pk = int.Parse(((di.FindControl("lblSkuDet_PK") as Label).Text.ToString()));
                        Decimal endbit = decimal.Parse(((di.FindControl("lblOnhandQty") as Label).Text.ToString()));
                        Decimal txt_deliveryQty = decimal.Parse(((di.FindControl("txt_deliveryQty") as TextBox).Text.ToString()));
                        Decimal weight = decimal.Parse(txt_weight.Text.ToString());

                        sinvdetdb.SalesDO_PK = trnmstr.SalesDO_PK;
                        sinvdetdb.Endbit_PK = endbit_pk;
                        sinvdetdb.DeliveryQty = txt_deliveryQty;
                        sinvdetdb.CuRate = decimal.Parse(txt_price.Text.ToString());
                        sinvdetdb.IsInvoiced = false;
                        enty.EndbitSalesDetails.Add(sinvdetdb);

                        var q = from end in enty.EndbitInventories where end.Endbit_Pk == endbit_pk select end;
                        foreach(var element in q)
                        {
                            element.DeliveredQty = txt_deliveryQty;
                            element.OnHandQty = element.OnHandQty - txt_deliveryQty;
                        }

                    }
                }
                enty.SaveChanges();
            }
            return Donum;
        }
    }
}
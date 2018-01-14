using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ArtWebApp.DataModels;
namespace ArtWebApp.Merchandiser.PO
{
    public partial class InventoryMisplacementExtra : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                load_inventory_misplacement();
            }
           
        }

        public void load_inventory_misplacement() {
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {
                var q = from inventrymstr in enty.InventoryMissingRequests
                        where inventrymstr.Level1Approval != null && inventrymstr.IsApproved == "N"
                        select inventrymstr;

                drp_inventory_misplacement.DataSource = q.ToList();
                drp_inventory_misplacement.DataBind();

            }

        }

        protected void Button1_Click(object sender, EventArgs e)
        {

            int misaappk = int.Parse(drp_inventory_misplacement.SelectedValue.ToString());

            decimal totalvalue = 0;
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {

                var q = (from inventoryMissingDetail in enty.InventoryMissingDetails join
                        inventorymstr in enty.InventoryMasters on inventoryMissingDetail.InventoryItem_PK equals inventorymstr.InventoryItem_PK
                        where inventoryMissingDetail.MisplaceApp_PK == misaappk
                        select new {inventorymstr.CURate, inventoryMissingDetail.Qty }).ToList();
                foreach(var element in q)
                {
                    totalvalue = totalvalue + (decimal.Parse(element.CURate.ToString()) * decimal.Parse(element.Qty.ToString()));

                       

                }

            }

            txt_productvalue.Text = totalvalue.ToString();
            txt_moq.Text = "0";
            txt_freight.Text = "0";

            }

        protected void txt_productvalue_TextChanged(object sender, EventArgs e)
        {
            calculatetotal();
        }

        protected void TextBox2_TextChanged(object sender, EventArgs e)
        {
            calculatetotal();
        }

        protected void TextBox3_TextChanged(object sender, EventArgs e)
        {
            calculatetotal();
        }

        public void calculatetotal()
        {
            txt_total.Text = (decimal.Parse(txt_productvalue.Text.ToString()) + decimal.Parse(txt_moq.Text.ToString()) + decimal.Parse(txt_freight.Text.ToString())).ToString();


            upd_total.Update();
        }

        protected void btn_save_Click(object sender, EventArgs e)
        {
            int misaappk = int.Parse(drp_inventory_misplacement.SelectedValue.ToString());
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {
                var q = from inventrymstr in enty.InventoryMissingRequests
                        where inventrymstr.MisplaceApp_pk==misaappk
                        select inventrymstr;

                foreach(var element in q)
                {
                    element.MoqValue = decimal.Parse(txt_moq.Text);
                    element.Freightvalue = decimal.Parse(txt_freight.Text);
                   

                }

                enty.SaveChanges();

            }
        }
    }




}
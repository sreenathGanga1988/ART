using ArtWebApp.DataModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArtWebApp.Inventory.Fabric_Transaction
{
    public partial class RollSplitterForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                int rollpk = int.Parse(Request.QueryString["rollpk"].ToString ());
                //Session["Cut_PKreport"] = Cut_PK;
                
                filldata(rollpk);
            }
        }




        public void filldata(int rollpk)
        {



            using (ArtEntitiesnew entty = new ArtEntitiesnew())
            {
                var q = from ponmbr in entty.FabricRollmasters
                        where ponmbr.Roll_PK == rollpk
                        select new { ponmbr.Roll_PK, ponmbr.RollNum, ponmbr.AYard, ponmbr.SYard, ponmbr.IsSaved };
                foreach(var element in q)
                {
                    lbl_rollpk.Text = element.Roll_PK.ToString();
                    lbl_rollnum.Text = element.RollNum.ToString();
                    lbl_ayardage.Text = element.AYard.ToString();
                    lbl_preqad.Text = element.IsSaved.ToString();
                    lbl_syard.Text = element.SYard.ToString();
                    lbl_balance.Text = element.SYard.ToString();
                }

             



            }

















          


                }

        protected void Button1_Click(object sender, EventArgs e)
        {
            tbl_InverntoryDetails.DataSource = BLL.InventoryBLL.RollTransactionBLL.CreateRollRows(int.Parse(txt_noofroll.Text));
            tbl_InverntoryDetails.DataBind();
        }

        protected void btn_saveRolls_Click(object sender, EventArgs e)
        {
            BLL.InventoryBLL.FabricRollEntryMRN mrnrolldata = new BLL.InventoryBLL.FabricRollEntryMRN();
            int oldrollpk = int.Parse (lbl_rollpk. Text. ToString ());
            Decimal oldrollyard = Decimal.Parse(lbl_balance.Text.ToString());
            mrnrolldata.Rolldatacollection = GetRollDetailsData();
            mrnrolldata.SplitSupplierRollData(oldrollpk, oldrollyard);
            tbl_InverntoryDetails.DataSource = null;
            tbl_InverntoryDetails.DataBind();
            
        }


        public List<BLL.InventoryBLL.FabricRollmasterDataDetails> GetRollDetailsData()
        {

            List<BLL.InventoryBLL.FabricRollmasterDataDetails> rk = new List<BLL.InventoryBLL.FabricRollmasterDataDetails>();
            for (int i = 0; i < tbl_InverntoryDetails.Rows.Count; i++)
            {

                String txt_rollnum = ((tbl_InverntoryDetails.Rows[i].FindControl("txt_rollnum") as TextBox).Text.ToString());
             
                String txt_remark = ((tbl_InverntoryDetails.Rows[i].FindControl("txt_remark") as TextBox).Text.ToString());
               
                String txt_syard = ((tbl_InverntoryDetails.Rows[i].FindControl("txt_syard") as TextBox).Text.ToString());
           
              

                BLL.InventoryBLL.FabricRollmasterDataDetails rolldata = new BLL.InventoryBLL.FabricRollmasterDataDetails();           
                rolldata.RollNum = txt_rollnum;
                rolldata.Qty = Decimal.Parse(txt_syard.ToString());
                rolldata.Remark = txt_remark;        
                rolldata.AYard = "";             
                rk.Add(rolldata);
            }




            return rk;


        }

    }
}
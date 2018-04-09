using ArtWebApp.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArtWebApp.Production.JobContractNew
{
    public partial class JObContractNew : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

      

       

        protected void btn_JCSubmit_Click(object sender, EventArgs e)
        {
            if (Decimal.Parse(txt_cmcost.Text) <= Decimal.Parse(Lbl_LockedCM.Text))
            {

                if (Decimal.Parse(txt_cmcost.Text) <= Decimal.Parse(txt_approvecost.Text))
                {
                    string msg = InsertJobContractdata();
                    tbl_podetails.DataSource = null;
                    tbl_podetails.DataBind();

                    MessgeboxUpdate("success", msg);
                }
                else
                {
                    string msg = "Entered CM is greater than Approved CM Please Revise the Costing";
                    ArtWebApp.Controls.Messagebox.MessgeboxUpdate(Messaediv, "fails", msg);
                    //  MessgeboxUpdate("sucess", msg);
                }
            }
            else
            {
                string msg = "Entered CM is greater than Locked CM Please Locked CM";
                ArtWebApp.Controls.Messagebox.MessgeboxUpdate(Messaediv, "fails", msg);
                //  MessgeboxUpdate("sucess", msg);
            }
        }


        public BLL.ProductionBLL.JobContractMasterData GetJCMasterData()
        {
            BLL.ProductionBLL.JobContractMasterData jcmstrdata = new BLL.ProductionBLL.JobContractMasterData();
            jcmstrdata.AtcID = int.Parse(cmb_atc.SelectedValue.ToString());
            jcmstrdata.Location_Pk = int.Parse(drp_factory.SelectedValue.ToString());
            jcmstrdata.AddedBy = Session["Username"].ToString().Trim();
            jcmstrdata.OurStylenum = cmb_ourstyle.SelectedItem.Text;
            jcmstrdata.Atcnum = cmb_atc.SelectedItem.Text;
            
            jcmstrdata.AddedDate = DateTime.Now;
            jcmstrdata.remark = txt_remark.Text;
            jcmstrdata.Ourstyleid= int.Parse(cmb_ourstyle.SelectedValue.ToString());
            jcmstrdata.CMCost = decimal.Parse(txt_cmcost.Text);
            return jcmstrdata;
        }


        public String InsertJobContractdata()
        {
            String msg = "";
            BLL.ProductionBLL.JobContractData jcdata = new BLL.ProductionBLL.JobContractData();

            jcdata.JCmstrdata = GetJCMasterData();
          
            String jcnum = jcdata.insertJObContractNewMaster();


            msg = "JobContract # : " + jcnum + " is generated Sucessfully";



            return msg;

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


        Decimal Qty = 0;
        protected void tbl_podetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
               
                    Decimal lbl_Qty = int.Parse((e.Row.FindControl("lbl_Qty") as Label).Text);

                Qty = Qty + lbl_Qty;

                lbl_totalQty.Text = Qty.ToString();

            }
        }

        protected void btn_showPO_Click(object sender, EventArgs e)
        {

        }

        protected void btn_showPO0_Click(object sender, EventArgs e)
        {
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {
             int   Ourstyleid = int.Parse(cmb_ourstyle.SelectedValue.ToString());
                int Location_Pk = int.Parse(drp_factory.SelectedValue.ToString());

                int jbid = 0;
                String jcnum = "";
                Decimal cm = 0;


                var costingCM = enty.StyleCostingComponentDetails.Where(u => u.StyleCostingMaster.OurStyleID == Ourstyleid && u.StyleCostingMaster.IsApproved=="A"&&u.CostingComponentMaster.CostComp_PK==3).Select(u => u.CompValue).FirstOrDefault();
                txt_approvecost.Text = costingCM.ToString();

                var lockedcm= enty.AtcDetails.Where(u => u.OurStyleID == Ourstyleid ).Select(u => u.LockedCM).FirstOrDefault();

                Lbl_LockedCM.Text = lockedcm.ToString();




                var q = (from jbmstr in enty.JobContractMasters
                         where jbmstr.OurStyleID == Ourstyleid && jbmstr.Location_Pk == Location_Pk
                         select jbmstr).ToList();
                foreach(var element in q)
                {
                    jbid = int.Parse(element.JobContract_pk.ToString());
                    jcnum = element.JOBContractNUM.ToString();
                    cm = Decimal.Parse(element.CM.ToString());



                }


                txt_cmcost.Text = cm.ToString ();
                lbl_jcnum.Text = jcnum.ToString();




                tbl_podetails.DataSource = Podata;
                tbl_podetails.DataBind();
            }
       }

        protected void tbl_podetails_DataBound(object sender, EventArgs e)
        {
            
            
        }
    }
}
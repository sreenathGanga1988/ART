using ArtWebApp.DBTransaction;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArtWebApp.Production.JobContractNew
{
    public partial class JobContractOthersNew : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                fillOptionalComponents();
            }
        }

        protected void btn_showPO_Click(object sender, EventArgs e)
        {
         

        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            BLL.ProductionBLL.JobContractDetailData  pkmstrdata = new BLL.ProductionBLL.JobContractDetailData();

              tbl_podetails.DataSource = pkmstrdata.GetJobContractdetailofAtcandLocation(int.Parse (cmb_ourstyle.SelectedValue.ToString ()),int.Parse (drp_factory.SelectedValue.ToString ()));
             tbl_podetails.DataBind();
              upd_grid.Update();
           
        }

        protected void btn_JCSubmit_Click(object sender, EventArgs e)
        {
            string msg = InsertDOdata();
            tbl_podetails.DataSource = null;
            tbl_podetails.DataBind();
            upd_grid.Update();
            MessgeboxUpdate("sucess", msg);
            upd_msg.Update();
        }


        public BLL.ProductionBLL.JobContractMasterData GetJCMasterData()
        {
            BLL.ProductionBLL.JobContractMasterData jcmstrdata = new BLL.ProductionBLL.JobContractMasterData();
            jcmstrdata.AtcID = int.Parse(cmb_atc.SelectedValue.ToString());
            jcmstrdata.OurStyleID = int.Parse(cmb_ourstyle.SelectedValue.ToString());
            
            jcmstrdata.Location_Pk = int.Parse(drp_factory.SelectedValue.ToString());
            jcmstrdata.AddedBy = Session["Username"].ToString().Trim();
            jcmstrdata.AddedDate = DateTime.Now;

            return jcmstrdata;
        }


        public String InsertDOdata()
        {
            String msg = "";
            BLL.ProductionBLL.JobContractData jcdata = new BLL.ProductionBLL.JobContractData();

            jcdata.JCmstrdata = GetJCMasterData();
            jcdata.JobContractDetailDataCollection = GetJobContractDetailData();
            String jcnum = jcdata.insertOtherJObContract(jcdata);


            msg = "JobContract # : " + jcnum + " is generated Sucessfully";



            return msg;

        }



        public List<BLL.ProductionBLL.JobContractDetailData> GetJobContractDetailData()
        {

            List<BLL.ProductionBLL.JobContractDetailData> rk = new List<BLL.ProductionBLL.JobContractDetailData>();


            foreach (GridViewRow di in tbl_podetails.Rows)
            {
                CheckBox chkBx = (CheckBox)di.FindControl("chk_select");

                if (chkBx != null && chkBx.Checked)
                {
                    int ourstyleid = int.Parse(cmb_ourstyle.SelectedValue.ToString());
                  
                    decimal JCnewwash = decimal.Parse(((di.FindControl("txt_washing") as TextBox).Text.ToString()));
                    decimal JCnewemb = decimal.Parse(((di.FindControl("txt_emb") as TextBox).Text.ToString()));
                    decimal JCnewfaclog = decimal.Parse(((di.FindControl("txt_factorylogistic") as TextBox).Text.ToString()));
                    decimal JCnewcmblog = decimal.Parse(((di.FindControl("txt_cmplogistic") as TextBox).Text.ToString()));

                    decimal dryprocess = decimal.Parse(((di.FindControl("txt_dryprocess") as TextBox).Text.ToString()));
                    decimal fabcomission = decimal.Parse(((di.FindControl("txt_fabcomission") as TextBox).Text.ToString()));
                    decimal garcomision = decimal.Parse(((di.FindControl("txt_garcomission") as TextBox).Text.ToString()));
                    decimal printing = decimal.Parse(((di.FindControl("txt_printing") as TextBox).Text.ToString()));


                    BLL.ProductionBLL.JobContractDetailData deldet = new BLL.ProductionBLL.JobContractDetailData();
                    deldet.OurStyleID = ourstyleid;
           






                    deldet.Washvalue = Decimal.Parse(JCnewwash.ToString());
                    deldet.Embriodaryvalue = Decimal.Parse(JCnewemb.ToString());
                    deldet.factorylogic = Decimal.Parse(JCnewfaclog.ToString());
                    deldet.cmblogic = Decimal.Parse(JCnewcmblog.ToString());
                    deldet.DryProcess = Decimal.Parse(dryprocess.ToString());
                    deldet.FabComission = Decimal.Parse(fabcomission.ToString());
                    deldet.GarCommision = Decimal.Parse(garcomision.ToString());
                    deldet.printing = Decimal.Parse(printing.ToString());
                    

                    rk.Add(deldet);
                }
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

        public void fillOptionalComponents()
        {
            CostingTransaction csttrans = new CostingTransaction();
            DataTable dtoptcomb = csttrans.GetOptionalCostingComponents();
            drp_optionalcomb.DataSource = dtoptcomb;
            udp_optionalcombo.Update();
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            disabletextbox();
            List<Infragistics.Web.UI.ListControls.DropDownItem> items = drp_optionalcomb.SelectedItems;
            foreach (Infragistics.Web.UI.ListControls.DropDownItem item in items)
            {

                String Componentname = item.Text.Trim();

                if (Componentname == "WASH")
                {
                    enabletextbox("txt_washing");
                }
                else if (Componentname == "DRY PROCESS")
                {
                    enabletextbox("txt_dryprocess");
                }
                else if (Componentname == "FAB COMMISION")
                {
                    enabletextbox("txt_fabcomission");
                }
                else if (Componentname == "GARMENT COMMMISION")
                {
                    enabletextbox("txt_garcomission");
                }
                else if (Componentname == "COMPANY LOGISTICS")
                {
                    enabletextbox("txt_cmplogistic");
                }
                else if (Componentname == "FACTORY LOGISTICS")
                {
                    enabletextbox("txt_factorylogistic");
                }
                else if (Componentname == "EMBROIDERY")
                {
                    enabletextbox("txt_emb");
                }
                else if (Componentname == "PRINTING")
                {
                    enabletextbox("txt_printing");
                }
            }
            

            upd_grid.Update();
        }

        public void enabletextbox(string textname)
        {
            foreach (GridViewRow di in tbl_podetails.Rows)
            {
                TextBox txt = (TextBox)di.FindControl(textname);

                txt.Enabled = true;
            }
        }

        public void disabletextbox()
        {
            foreach (GridViewRow di in tbl_podetails.Rows)
            {
                ((TextBox)di.FindControl("txt_washing")).Enabled = false;
                ((TextBox)di.FindControl("txt_dryprocess")).Enabled = false;
                ((TextBox)di.FindControl("txt_fabcomission")).Enabled = false;
                ((TextBox)di.FindControl("txt_garcomission")).Enabled = false;
                ((TextBox)di.FindControl("txt_cmplogistic")).Enabled = false;
                ((TextBox)di.FindControl("txt_factorylogistic")).Enabled = false;
                ((TextBox)di.FindControl("txt_emb")).Enabled = false;
                ((TextBox)di.FindControl("txt_printing")).Enabled = false;

                


            }

        }



    }
}
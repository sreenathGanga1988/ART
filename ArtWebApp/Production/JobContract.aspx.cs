using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArtWebApp.Production
{
    public partial class JobContract : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_showPO_Click(object sender, EventArgs e)
        {
            BLL.PoPackMasterData pkmstrdata = new BLL.PoPackMasterData();
            drp_popack.DataSource=pkmstrdata.GetAllPOPackData(int.Parse (cmb_atc.SelectedValue.ToString())) ;
            drp_popack.DataBind();
 
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            ArrayList popaklist = new ArrayList();
            List<Infragistics.Web.UI.ListControls.DropDownItem> items = drp_popack.SelectedItems;
            foreach (Infragistics.Web.UI.ListControls.DropDownItem item in items)
            {
               
                int popackid = int.Parse(item.Value.ToString());
                popaklist.Add(popackid);
            }

            
            if(popaklist.Count>0 && popaklist !=null)
            {
                BLL.PoPackMasterData pkmstrdata = new BLL.PoPackMasterData();

                tbl_podetails.DataSource = pkmstrdata.GetPOPACKDetailsofList(popaklist);
                tbl_podetails.DataBind();
            }
        }

        protected void btn_JCSubmit_Click(object sender, EventArgs e)
        {
            if (checkdatagridValue(tbl_podetails, "lbl_apprcm", "txt_cm"))
            {
                string msg = InsertDOdata();
                tbl_podetails.DataSource = null;
                tbl_podetails.DataBind();

                MessgeboxUpdate("sucess", msg);
            }
            else
            {
                string msg = "Entered CM is greater than Approved CM Please Revise the Costing";
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
            jcmstrdata.AddedDate = DateTime.Now;

            return jcmstrdata;
        }


        public String InsertDOdata()
        {
            String msg = "";
            BLL.ProductionBLL.JobContractData jcdata = new BLL.ProductionBLL.JobContractData();

            jcdata.JCmstrdata = GetJCMasterData();
            jcdata.JobContractDetailDataCollection = GetJobContractDetailData();
            String jcnum = jcdata.insertJObContract(jcdata);


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
                    int ourstyleid = int.Parse(((di.FindControl("lbl_OurStyleID") as Label).Text.ToString()));
                    int popackid = int.Parse(((di.FindControl("lbl_popackid") as Label).Text.ToString()));
                    decimal JCnewcm = decimal.Parse(((di.FindControl("txt_cm") as TextBox).Text.ToString()));
                    BLL.ProductionBLL.JobContractDetailData deldet = new BLL.ProductionBLL.JobContractDetailData();
                    deldet.OurStyleID = ourstyleid;
                    deldet.PoPackID = popackid;
                    deldet.CMvalue = float.Parse ( JCnewcm.ToString ());
                    rk.Add(deldet);
                }
            }
            return rk;


        }





        public Boolean checkdatagridValue(GridView tblgrid, String lbl_Qty1, String txt_Qty2)
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

        protected void tbl_podetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                String lbl_location = (e.Row.FindControl("lbl_jc") as Label).Text;
                if (lbl_location.Trim() == "NA")
                {
                    
                }
                else
                {
                    CheckBox chklist = (e.Row.FindControl("chk_select") as CheckBox);
                    chklist.Checked = false;
                    chklist.Enabled = false;

                }
            }
        }
    }
}
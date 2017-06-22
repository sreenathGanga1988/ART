using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ArtWebApp.BLL;
using ArtWebApp.DataModels;
using System.Data;
using System.Threading.Tasks;

namespace ArtWebApp.Merchandiser.PO
{
    public partial class IPOMultiCreator : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                String Selectionstring = Request.QueryString["selectionid"].ToString();
                Filldetails(Selectionstring);

              
            }

            
        }

        protected void drp_currency_DataBound(object sender, EventArgs e)
        {

        }

       



        protected void btn_AddSpo_Click(object sender, EventArgs e)
        {
            InsertSpoMaster();
        }



        public void InsertSpoMaster()
        {
            BLL.ProcurementBLL.StockPOMasterdata spodata = new BLL.ProcurementBLL.StockPOMasterdata();
            spodata.Supplier_Pk = int.Parse(drp_supplier.SelectedValue.ToString());
            spodata.DeliveryDate = DateTime.Parse(dtp_deliverydate.Value.ToString());
            spodata.DeliveryTerms_Pk = int.Parse(drp_deliveryterm.SelectedValue.ToString());
            spodata.DeliveryMethod_Pk = int.Parse(drp_deliverymethod.SelectedValue.ToString());
            spodata.PaymentTermID = int.Parse(drp_paymentterm.SelectedValue.ToString());
            spodata.PO_value = 0;
            spodata.Location_PK = int.Parse(drp_deliverydestination.SelectedValue.ToString());
            spodata.CurrencyID = int.Parse(drp_currency.SelectedValue.ToString());
            try
            {
                spodata.Remark = txt_remark.InnerText.Trim();
            }
            catch (Exception)
            {


            }

            String sPO = spodata.InsertSpoMasterData(spodata);



            String msg = sPO + "Created Successfully";


            MessgeboxUpdate("sucess", msg);


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

        public void InsertSpoDetails()
        {
            for (int i = 0; i < tbl_SpoEnterData.Rows.Count; i++)
            {

                String Suppliercolor = ((tbl_SpoEnterData.Rows[i].FindControl("ddl_Supcolor") as DropDownList).SelectedItem.Text.Trim());

                String SupplierSize = ((tbl_SpoEnterData.Rows[i].FindControl("ddl_SupSize") as DropDownList).SelectedItem.Text.Trim());

                String lbl_balaqty = ((tbl_SpoEnterData.Rows[i].FindControl("lbl_balaqty") as TextBox).Text.Trim());
                String txt_qty = ((tbl_SpoEnterData.Rows[i].FindControl("txt_qty") as TextBox).Text.Trim());


                String ddl_altuom = ((tbl_SpoEnterData.Rows[i].FindControl("ddl_AltUOM") as DropDownList).SelectedValue.ToString().Trim());


                if (Decimal.Parse(lbl_balaqty) >= Decimal.Parse(txt_qty.ToString()))
                {
                    BLL.ProcurementBLL.StockPODetailsdata spdetdata = new BLL.ProcurementBLL.StockPODetailsdata();
                    spdetdata.SPO_PK = int.Parse(Session["spo_Pk"].ToString());
                    spdetdata.Template_PK = int.Parse((tbl_SpoEnterData.Rows[i].FindControl("drp_templateforComp") as DropDownList).SelectedValue.ToString());
                    spdetdata.TemplateColor = (tbl_SpoEnterData.Rows[i].FindControl("drp_itemcolor") as DropDownList).SelectedItem.Text.ToString().Trim();
                    spdetdata.TemplateSize = (tbl_SpoEnterData.Rows[i].FindControl("drp_itemsize") as DropDownList).SelectedItem.Text.ToString().Trim();
                    spdetdata.TemplateWeight = (tbl_SpoEnterData.Rows[i].FindControl("drp_weight") as DropDownList).SelectedItem.Text.ToString().Trim();
                    spdetdata.TemplateWidth = (tbl_SpoEnterData.Rows[i].FindControl("drp_width") as DropDownList).SelectedItem.Text.ToString().Trim();
                    spdetdata.Composition = (tbl_SpoEnterData.Rows[i].FindControl("drp_composition") as DropDownList).SelectedItem.Text.ToString().Trim();
                    spdetdata.Construct = (tbl_SpoEnterData.Rows[i].FindControl("drp_construction") as DropDownList).SelectedItem.Text.ToString().Trim();
                    spdetdata.Unitprice = Decimal.Parse((tbl_SpoEnterData.Rows[i].FindControl("txt_unitPrice") as TextBox).Text.ToString());
                    spdetdata.POQty = Decimal.Parse((tbl_SpoEnterData.Rows[i].FindControl("txt_qty") as TextBox).Text.ToString());
                    spdetdata.Uom_PK = int.Parse((tbl_SpoEnterData.Rows[i].FindControl("drp_UOM") as DropDownList).SelectedValue.ToString());


                    try
                    {
                        spdetdata.oodoPo_PK = int.Parse((tbl_SpoEnterData.Rows[i].FindControl("lbl_poid") as TextBox).Text);
                        spdetdata.oodoPolineid = int.Parse((tbl_SpoEnterData.Rows[i].FindControl("POLineID") as TextBox).Text);
                    }
                    catch (Exception)
                    {


                    }
                    spdetdata.InsertSpoDetails(spdetdata);
                }

                else
                {

                    String Msg = " Cannot Create SPO greater than IPO Balance";

                    ClientScript.RegisterStartupScript(this.GetType(), "Art", "alert('" + Msg + "');", true);
                }

            }
        }
        protected void btn_addItems_Click(object sender, EventArgs e)
        {
            InsertSpoDetails();
            UpdatePanel3.Update();
            GridView1.DataBind();
           // clearcontrol();
        }

        public void Filldetails(string Selectionstring)
        {
            DataTable dt = QueryFunctions.ReturnQueryResultDatatable(@"SELECT        POId, POLineID, Description, IPO, SPO_PK, Odoo_UOM as UOM, OdooLocation, Qty, OrderedQty,(Qty-OrderedQty) as BalanceQty
FROM(SELECT        POId, POLineID, Description, PONum AS IPO, SPO_PK, Odoo_UOM, OdooLocation, Qty, ISNULL
                                                        ((SELECT        SUM(POQty) AS Expr1
                                                            FROM            StocPOForODOO
                                                            WHERE(POId = ODOOGPOMaster.POId) AND(POLineID = ODOOGPOMaster.POLineID)), 0) AS OrderedQty
                          FROM            ODOOGPOMaster
                          WHERE(POLineID IN("+ Selectionstring + "))) AS tt");

            tbl_SpoEnterData.DataSource = dt;
            tbl_SpoEnterData.DataBind();

        }


        protected void Button2_Click(object sender, EventArgs e)
        {

        }

   

        public void fillcontrils(int temmplatepk,GridViewRow currentRow)
        {
           

            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {


                var color =  from tempcol in enty.TemplateColors
                            where tempcol.Template_PK == temmplatepk
                            select tempcol;

                var size = from tempsize in enty.TemplateSizes
                           where tempsize.Template_PK == temmplatepk
                           select tempsize;

                var comp = from tempcomp in enty.TemplateCompositions
                           where tempcomp.Template_Pk == temmplatepk
                           select tempcomp;

                var Constr = from tempcons in enty.TemplateConstructions
                             where tempcons.Template_Pk == temmplatepk
                             select tempcons;

                var Width = from tmpwidth in enty.TemplateWidths
                            where tmpwidth.Template_Pk == temmplatepk
                            select tmpwidth;

                var tmpweight = from tmpwght in enty.TemplateWeights
                                where tmpwght.Template_Pk == temmplatepk
                                select tmpwght;


                DropDownList drp_composition = (currentRow.FindControl("drp_composition") as DropDownList);
                DropDownList drp_construction = (currentRow.FindControl("drp_construction") as DropDownList);
                DropDownList drp_itemsize = (currentRow.FindControl("drp_itemsize") as DropDownList);
                DropDownList drp_weight = (currentRow.FindControl("drp_weight") as DropDownList);
                DropDownList drp_width = (currentRow.FindControl("drp_width") as DropDownList);
                DropDownList drp_itemcolor = (currentRow.FindControl("drp_itemcolor") as DropDownList);
                
                try
                {
                    drp_composition.DataSource = comp.ToList();
                    drp_composition.DataValueField = "TemplateCom_Pk";
                    drp_composition.DataTextField = "Composition";

                    drp_composition.DataBind();

                }
                catch (Exception)
                {


                }

                try
                {
                    drp_construction.DataSource = Constr.ToList();
                    drp_construction.DataValueField = "TemplateCon_Pk";
                    drp_construction.DataTextField = "Construct";
                    drp_construction.DataBind();

                }
                catch (Exception)
                {


                }


                try
                {
                    drp_itemsize.DataSource = size.ToList();
                    drp_itemsize.DataValueField = "TemplateSize_PK";
                    drp_itemsize.DataTextField = "TemplateSize1";
                    drp_itemsize.DataBind();


                }
                catch (Exception exp)
                {


                }

                try
                {
                    drp_weight.DataSource = tmpweight.ToList();
                    drp_weight.DataTextField = "Weight";
                    drp_weight.DataValueField = "TemplateWeight_Pk";
                    drp_weight.DataBind();

                }
                catch (Exception)
                {


                }
                try
                {
                    drp_width.DataSource = Width.ToList();
                    drp_width.DataValueField = "Width";
                    drp_width.DataTextField = "TemplateWidth_Pk";
                    drp_width.DataBind();

                }
                catch (Exception)
                {


                }

                try
                {
                    drp_itemcolor.DataSource = color.ToList();
                    drp_itemcolor.DataValueField = "TemplateColor_PK";
                    drp_itemcolor.DataTextField = "TemplateColor1";
                    drp_itemcolor.DataBind();

                }
                catch (Exception)
                {


                }

                drp_itemcolor.Items.Insert(0, new ListItem(""));
                drp_width.Items.Insert(0, new ListItem(""));
                drp_weight.Items.Insert(0, new ListItem(""));
                drp_itemsize.Items.Insert(0, new ListItem(""));
                drp_construction.Items.Insert(0, new ListItem(""));
                 drp_composition.Items.Insert(0, new ListItem(""));


                UpdatePanel upd_itemcolor = (currentRow.FindControl("upd_itemcolor") as UpdatePanel);
                UpdatePanel upd_width = (currentRow.FindControl("upd_width") as UpdatePanel);
                UpdatePanel upd_weight = (currentRow.FindControl("upd_weight") as UpdatePanel);
                UpdatePanel upd_itemsize = (currentRow.FindControl("upd_itemsize") as UpdatePanel);
                UpdatePanel upd_construction = (currentRow.FindControl("upd_construction") as UpdatePanel);
                UpdatePanel upd_composition = (currentRow.FindControl("upd_composition") as UpdatePanel);

                upd_itemcolor.Update();
                upd_width.Update();
                upd_weight.Update();
                upd_itemsize.Update();
                upd_construction.Update();
               upd_composition.Update();
            
            }


        }


        protected void Button3_Click(object sender, EventArgs e)
        {
            Session["spo_pk"] = int.Parse(drp_spo.SelectedValue.ToString());
            String msg = drp_spo.SelectedItem.Text + " Selected for Update";
            Spodata.DataBind();

            MessgeboxUpdate("sucess", msg);
            GridView1.DataBind();
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
           
        }

        protected void Button5_Click(object sender, EventArgs e)
        {
        }

        protected void cmb_itemgroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList cmb_itemgroup = (DropDownList)sender;


            GridViewRow currentRow = cmb_itemgroup.ClosestContainer<GridViewRow>();

            DropDownList drp_templateforComp = (currentRow.FindControl("drp_templateforComp") as DropDownList);
            UpdatePanel Upd_templateforComp = (currentRow.FindControl("Upd_templateforComp") as UpdatePanel);
            if (currentRow != null)
            {
                using (ArtEntitiesnew enty = new ArtEntitiesnew())
                {
                    int itemgroupid = int.Parse(cmb_itemgroup.SelectedValue.ToString());

                    var temp = from tmplatmstr in enty.Template_Master
                               where tmplatmstr.ItemGroup_PK == itemgroupid
                               select tmplatmstr;


                    drp_templateforComp.DataSource = temp.ToList();
                    drp_templateforComp.DataValueField = "Template_Pk";
                    drp_templateforComp.DataTextField = "Description";

                    drp_templateforComp.DataBind();
                    Upd_templateforComp.Update();


                }
            }
             
        }

        protected void drp_templateforComp_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList drp_template = (DropDownList)sender;


            GridViewRow currentRow = drp_template.ClosestContainer<GridViewRow>();

            int templatepk = int.Parse(drp_template.SelectedValue.ToString());
            if (currentRow != null)
            {

               fillcontrils(templatepk, currentRow); 

               
            }
                
        }
    }
}
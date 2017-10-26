using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ArtWebApp.BLL;
using ArtWebApp.DataModels;
using ArtWebApp.BLL.MerchandsingBLL.ProcurementBLL;

namespace ArtWebApp.Merchandiser
{
    public partial class StockPO : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            

        }

        protected void drp_currency_DataBound(object sender, EventArgs e)
        {

        }

        //protected void drp_templateforComp_ValueChanged(object sender, Infragistics.Web.UI.ListControls.DropDownValueChangedEventArgs e)
        //{
        //    hdn_template_PK.Value = drp_templateforComp.SelectedValue.ToString();
        //    drp_weight.DataBind();
        //    drp_itemcolor.DataBind();
        //    drp_itemSize.DataBind();
        //    drp_itemWeight.DataBind();
        //    drp_itemWidth.DataBind();
        //    drp_UOM.DataBind();
        //    drp_composition.DataBind();
        //    drp_construction.DataBind();
        //}

       

    

        protected void btn_AddSpo_Click(object sender, EventArgs e)
        {
            InsertSpoMaster(); 
        }



        public void InsertSpoMaster()
        {
            BLL.ProcurementBLL.StockPOMasterdata spodata = new BLL.ProcurementBLL.StockPOMasterdata();
            spodata.Supplier_Pk = int.Parse(drp_supplier.SelectedValue.ToString());
            spodata.DeliveryDate = DateTime.Parse ( dtp_deliverydate.Value.ToString ());
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

           String sPO=      spodata.InsertSpoMasterData(spodata);



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
            BLL.ProcurementBLL.StockPODetailsdata spdetdata = new BLL.ProcurementBLL.StockPODetailsdata();
            spdetdata.SPO_PK = int.Parse(Session["spo_Pk"].ToString());
            spdetdata.Template_PK = int.Parse(drp_templateforComp.SelectedValue.ToString());
            spdetdata.TemplateColor = drp_itemcolor.SelectedItem.Text.ToString().Trim ();
            spdetdata.TemplateSize = drp_itemsize.SelectedItem.Text.ToString().Trim();
            spdetdata.TemplateWeight=drp_weight.SelectedItem.Text.ToString().Trim();
            spdetdata.TemplateWidth = drp_width.SelectedItem.Text.ToString().Trim();
            spdetdata.Composition = drp_composition.SelectedItem.Text.ToString().Trim();
            spdetdata.Construct = drp_construction.SelectedItem.Text.ToString().Trim();
            spdetdata.Unitprice=Decimal.Parse (txt_unitPrice .Text.ToString ());
            spdetdata.POQty=Decimal.Parse (txt_qty .Text.ToString ());
            spdetdata.Uom_PK = int.Parse(drp_UOM.SelectedValue.ToString());


           
            spdetdata.InsertSpoDetails(spdetdata);
        }

        protected void btn_addItems_Click(object sender, EventArgs e)
        {
            InsertSpoDetails();
            UpdatePanel3.Update();
            GridView1.DataBind();
            clearcontrol();
        }

      public void clearcontrol()
        {
            txt_qty.Text = "";
            txt_unitPrice.Text = "";
            UpdatePanel2.Update();
         
        }
      

        protected void Button2_Click(object sender, EventArgs e)
        {
         
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            
            fillcontrils(int.Parse(drp_templateforComp.SelectedValue.ToString()));
          
          
          
          

        }

        protected void Button2_Click1(object sender, EventArgs e)
        {
            filltemplate();
        }



        public void filltemplate()
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
                UpdatePanel4.Update();

                  
            }
        }

        public void fillcontrils(int temmplatepk)
        {
            ;
          

            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {


                var color = from tempcol in enty.TemplateColors
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
                //drp_itemcolor.SelectedIndex = -1; drp_width.SelectedIndex = -1; drp_weight.SelectedIndex = -1; drp_itemsize.SelectedIndex = -1; drp_construction.SelectedIndex = -1; drp_composition.SelectedIndex = -1;
                UpdatePanel5.Update();
            UpdatePanel6.Update();
            UpdatePanel7.Update();
            UpdatePanel8.Update();
            UpdatePanel9.Update();
                
            }


        }
        public void fillspomasterdata()
        {
            int spopk = int.Parse(drp_spo.SelectedValue.ToString());
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {
                var q = from spmstr in enty.StockPOMasters
                        where spmstr.SPO_Pk == spopk
                        select spmstr;

                foreach (var element in q)
                {
                    drp_supplier.SelectedValue = element.Supplier_Pk.ToString();
                    drp_deliverymethod.SelectedValue = element.DeliveryMethod_Pk.ToString();
                    drp_currency.SelectedValue = element.CurrencyID.ToString();
                    drp_paymentterm.SelectedValue = element.PaymentTermID.ToString();
                    drp_deliverydestination.SelectedValue = element.Location_PK.ToString();
                    drp_deliveryterm.SelectedValue = element.DeliveryTerms_Pk.ToString();
                    dtp_deliverydate.Date = DateTime.Parse(element.DeliveryDate.ToString());
                    try
                    {
                        txt_remark.InnerText = element.Remark.ToString();
                    }
                    catch (Exception)
                    {


                    }

                }

            }
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            Session["spo_pk"] = int.Parse(drp_spo.SelectedValue.ToString());
            String msg = drp_spo.SelectedItem.Text + " Selected for Update";


            MessgeboxUpdate("sucess", msg);
            fillspomasterdata();
            GridView1.DataBind();
            Spodata.DataBind();
            UpdatePanel3.Update();
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
        }

        protected void drp_supplier_SelectedIndexChanged(object sender, EventArgs e)
        {
            ProcurementMasterData procurementMasterData = new ProcurementMasterData();

            drp_paymentterm.SelectedValue = procurementMasterData.GetSupplierPaymentFixed(int.Parse(drp_supplier.SelectedItem.Value.ToString())).ToString();
            upd_paymentterm.Update();
        }
    }
}
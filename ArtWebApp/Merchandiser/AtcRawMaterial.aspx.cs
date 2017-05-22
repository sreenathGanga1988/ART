using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Infragistics.Web.UI.ListControls;
using System.Data;
using ArtWebApp.DataModels;
public partial class Merchandiser_AtcRawMaterial : System.Web.UI.Page
{ArtWebApp.DBTransaction.PoPackTransaction potran = null;
ArtWebApp.DBTransaction.SkuCreator skucrtr = null;
    DataTable dt = new DataTable();
    DataSet data = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }
    protected void cmb_atc_SelectionChanged(object sender, Infragistics.Web.UI.ListControls.DropDownSelectionChangedEventArgs e)
    {
       
    }
    protected void buttonAtc_Click(object sender, EventArgs e)
    {
       // GetGridFilled();
        HiddenField1.Value = cmb_atc.SelectedValue.ToString();
        skucrtr = new ArtWebApp.DBTransaction.SkuCreator();
         data = skucrtr.getattributesforAtc(int.Parse (cmb_atc.SelectedValue.ToString()));
        
         ViewState["ViewStateDateSet"] = data;
         tbl_skumaster.DataBind();
      //   upd_rawmaterial.Update();
         upd_skugrid.Update();
       
    }


    public void AddRawMaterialmaster()
    {

        using (ArtEntitiesnew enty = new ArtEntitiesnew())
        {
          

             List<DropDownItem> items = cmb_item.SelectedItems;
             foreach (DropDownItem item in items)
             {
                 int atcid=int.Parse(cmb_atc.SelectedValue.ToString ());
                 int templatepk=int.Parse (item.Value.ToString ());
                 string templatename = item.Text.Trim();
                 var templatecode = enty.Template_Master.Where(u => u.Template_PK == templatepk).Select(u => u.TemplateCode ).FirstOrDefault();
                 int templatecount = 0;
                 
                 if (!enty.AtcRawMaterialMasters.Any(f => f.Template_PK == templatepk && f.Atc_id == atcid && f.IsGD == "N"))
                 {

                     AtcRawMaterialMaster stsz = new AtcRawMaterialMaster();



                     stsz.TempCode = templatecode.ToString();
                     stsz.Template_PK = templatepk;
                     stsz.Atc_id = atcid;
                     stsz.TemplateName = templatename;
                     stsz.TemplateCount = templatecount;
                    stsz.IsGD = "N";
                     enty.AtcRawMaterialMasters.Add(stsz);
                     

                 }
             }
             enty.SaveChanges();
        }
    }
    public void AddGDRawMaterialmaster()
    {

        using (ArtEntitiesnew enty = new ArtEntitiesnew())
        {


            List<DropDownItem> items = cmb_item0.SelectedItems;
            foreach (DropDownItem item in items)
            {
                int atcid = int.Parse(cmb_atc.SelectedValue.ToString());
                int templatepk = int.Parse(item.Value.ToString());
                string templatename = item.Text.Trim();
                var templatecode = enty.Template_Master.Where(u => u.Template_PK == templatepk).Select(u => u.TemplateCode).FirstOrDefault();
                int templatecount = 0;

                if (!enty.AtcRawMaterialMasters.Any(f => f.Template_PK == templatepk && f.Atc_id == atcid && f.IsGD == "Y"))
                {

                    AtcRawMaterialMaster stsz = new AtcRawMaterialMaster();



                    stsz.TempCode = templatecode.ToString().Trim() + "GD";
                    stsz.Template_PK = templatepk;
                    stsz.Atc_id = atcid;
                    stsz.TemplateName = templatename;
                    stsz.TemplateCount = templatecount;
                    stsz.IsGD = "Y";
                    enty.AtcRawMaterialMasters.Add(stsz);


                }
                else
                {
                    var existingrawmaterialcount = (from atcmstr in enty.AtcRawMaterialMasters
                                                    where atcmstr.Atc_id == atcid && atcmstr.Template_PK == templatepk && atcmstr.IsGD == "Y"

                                                    select atcmstr).Count();
                    AtcRawMaterialMaster stsz = new AtcRawMaterialMaster();



                    stsz.TempCode = templatecode.ToString().Trim() +"G"+(int.Parse ( existingrawmaterialcount.ToString ())+1).ToString () + "GD";
                    stsz.Template_PK = templatepk;
                    stsz.Atc_id = atcid;
                    stsz.TemplateName = templatename;
                    stsz.TemplateCount = templatecount;
                    stsz.IsGD = "Y";
                    enty.AtcRawMaterialMasters.Add(stsz);
                }
            }
            enty.SaveChanges();
        }
    }

   
    /// <summary>
    /// get the rawmaterialmaster from database
    /// </summary>
    public void GetGridFilled()
    {
         if(cmb_atc.SelectedValue !=null)

        potran=new ArtWebApp.DBTransaction.PoPackTransaction ();
        DataTable dt = potran.GetAtcRawmaterialMaster(int.Parse(cmb_atc.SelectedValue.ToString()));
     
         WebDataGrid1.DataSource = dt;
         WebDataGrid1.DataBind();
        
    }
    
    
    protected void cmb_item_SelectionChanged(object sender, DropDownSelectionChangedEventArgs e)
    {
        
    }

    protected void btn_item_Click(object sender, EventArgs e)
    {
        AddRawMaterialmaster();
        tbl_skumaster.DataBind();
        WebDataGrid1.DataBind();
        upd_rawmaterial.Update();
        upd_skugrid.Update();
        if (tbl_skumaster.Rows.Count > 0)
        {
           
        }
        else
        {
           
        }
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {

    }

   
    /// <summary>
    /// generate SKUmaster
    /// </summary>
    public void generateSKU()
    {
        skucrtr = new ArtWebApp.DBTransaction.SkuCreator();
        skucrtr.CreateSkuMaster(int.Parse(cmb_atc.SelectedValue.ToString()));
        tbl_skumaster.DataBind();
    }

    public void UpdateSkuMaster()
    {
        using (ArtEntitiesnew enty = new ArtEntitiesnew())
        {
            for (int i = 0; i < tbl_skumaster.Rows.Count; i++)
            {
                int skuid = int.Parse(tbl_skumaster.Rows[i].Cells[0].Text.ToString());

                //if(skuid==40245)
                //{
                //    int k = 0;
                //}

                DropDownList ddl_comp = (tbl_skumaster.Rows[i].FindControl("ddl_comp") as DropDownList);
                DropDownList ddl_con = (tbl_skumaster.Rows[i].FindControl("ddl_con") as DropDownList);
                DropDownList ddl_body = (tbl_skumaster.Rows[i].FindControl("ddl_body") as DropDownList);

                DropDownList ddl_AltUOM = (tbl_skumaster.Rows[i].FindControl("ddl_AltUOM") as DropDownList);
                string AltUOM = ddl_AltUOM.SelectedItem.ToString().ToString().Trim();
                Label lbl_uompk = (tbl_skumaster.Rows[i].FindControl("lbl_uompk") as Label);
                int uompk = int.Parse(lbl_uompk.Text);
                int altuompk = uompk;

                if (AltUOM == "Select AltUOM"||AltUOM == "")
                {
                    altuompk = uompk;
                }
                
                else
                {
                    int tempatluompk = int.Parse(ddl_AltUOM.SelectedValue.ToString());

                    if (tempatluompk == uompk)
                    {
                        altuompk = uompk;
                    }

                    else
                    {
                        altuompk = tempatluompk;
                        uompk = tempatluompk;
                    }

                }

                string Composition = ddl_comp.SelectedItem.ToString().Trim();
                string Construction = ddl_con.SelectedItem.ToString().Trim();

                var wastagepercentage = (tbl_skumaster.Rows[i].FindControl("txt_Wastage") as TextBox).Text;
                var ordermin = (tbl_skumaster.Rows[i].FindControl("txt_ordermin") as TextBox).Text;
               
                if(wastagepercentage.ToString ().Trim ()=="")
                {
                    wastagepercentage = "0";
                }
                if (ordermin.ToString().Trim() == "")
                {
                    
                    ordermin = "0";
                }

                var rate = (tbl_skumaster.Rows[i].FindControl("txt_rate") as TextBox).Text;
                string width = (tbl_skumaster.Rows[i].FindControl("ddl_width") as DropDownList).SelectedItem.ToString().ToString().Trim();
                string weight = (tbl_skumaster.Rows[i].FindControl("ddl_weight") as DropDownList).SelectedItem.ToString().ToString().Trim();
                String chk_isCD = ((tbl_skumaster.Rows[i].FindControl("chk_isCD") as CheckBox).Checked == true ? "Y" : "N");
                String chk_isSD = ((tbl_skumaster.Rows[i].FindControl("chk_isSD") as CheckBox).Checked == true ? "Y" : "N");
                String chk_isCommon = ((tbl_skumaster.Rows[i].FindControl("chk_isCommon") as CheckBox).Checked == true ? "Y" : "N");
                String chk_isisGD = ((tbl_skumaster.Rows[i].FindControl("chk_isisGD") as CheckBox).Checked == true ? "Y" : "N");


                var q = from skumst in enty.SkuRawMaterialMasters
                        where skumst.Sku_Pk == skuid
                        select skumst;

                foreach (var element in q)
                {
                    element.Composition = (Composition == "Select Composition" ? "" : Composition);
                    element.Construction = (Construction == "Select Construction" ? "" : Construction);
                    element.Weight = (weight == "Select Weight" ? "" : weight);
                    element.Width = (width == "Select Width" ? "" : width);
                    element.IsCD = chk_isCD;
                    element.IsSD = chk_isSD;
                    element.AltUom_pk = altuompk;
                    element.Uom_PK = uompk;
                    element.isCommon = chk_isCommon;
                    element.IsGD = chk_isisGD;
                    element.Rate = decimal.Parse(rate.ToString());
                    element.WastagePercentage = decimal.Parse(wastagepercentage);
                    element.OrderMin = decimal.Parse(ordermin.ToString ().Trim ());
                    element.BodyPartName= ddl_body.SelectedItem.ToString();
                }

            }
            enty.SaveChanges();
        }
    }

  

   

    protected void tbl_skumaster_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int templatepk = int.Parse((e.Row.FindControl("lbl_templatepk") as Label).Text);
            DataTable constructiondata = new DataTable();
            DataTable Compositedata = new DataTable();
            DataTable widthdata = new DataTable();
            DataTable weightdata = new DataTable();
            DataTable AltUOMData = new DataTable();

            DataSet myDS = (DataSet)ViewState["ViewStateDateSet"];
            try
            {
                Compositedata = myDS.Tables[0].Select("Template_pk =" + templatepk + "").CopyToDataTable();
            }
            catch (Exception)
            {


            }
            try
            {

                constructiondata = myDS.Tables[1].Select("Template_pk =" + templatepk + "").CopyToDataTable();

            }
            catch (Exception)
            {


            }
            try
            {
                widthdata  = myDS.Tables[2].Select("Template_pk =" + templatepk + "").CopyToDataTable();
            }
            catch (Exception)
            {


            }
            try
            {

                weightdata = myDS.Tables[3].Select("Template_pk =" + templatepk + "").CopyToDataTable();

            }
            catch (Exception)
            {


            }
            try
            {

             //   AltUOMData = myDS.Tables[4].Select("Template_pk =" + templatepk + "").CopyToDataTable();

            }
            catch (Exception)
            {


            }

            //Find the DropDownList in the Row
            DropDownList ddl_comp = (e.Row.FindControl("ddl_comp") as DropDownList);
            ddl_comp.DataSource = Compositedata;
            ddl_comp.DataTextField = "Composition";
            ddl_comp.DataValueField = "TemplateCom_Pk";
            ddl_comp.DataBind();
            DropDownList ddl_con = (e.Row.FindControl("ddl_con") as DropDownList);
            ddl_con.DataSource = constructiondata;
            ddl_con.DataTextField = "Construct";
            ddl_con.DataValueField = "TemplateCon_Pk";
            ddl_con.DataBind();
            DropDownList ddl_width= (e.Row.FindControl("ddl_width") as DropDownList);
            ddl_width.DataSource = widthdata ;
            ddl_width.DataTextField = "Width";
            ddl_width.DataValueField = "TemplateWidth_Pk";
            ddl_width.DataBind();
            DropDownList ddl_weight = (e.Row.FindControl("ddl_weight") as DropDownList);
            ddl_weight.DataSource = weightdata;
            ddl_weight.DataTextField = "Weight";
            ddl_weight.DataValueField = "TemplateWeight_Pk";
            ddl_weight.DataBind();

            DropDownList ddl_AltUOM = (e.Row.FindControl("ddl_AltUOM") as DropDownList);
            //ddl_AltUOM.DataSource = AltUOMData;
            //ddl_AltUOM.DataTextField = "UomCode";
            //ddl_AltUOM.DataValueField = "Uom_PK";
            //ddl_AltUOM.DataBind();



            
            //Add Default Item in the DropDownList
            ddl_comp.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select Composition"));
            ddl_con.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select Construction"));
            ddl_width.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select Width"));
            ddl_weight.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select Weight"));
            ddl_AltUOM.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select AltUOM"));
            //Select the Country of Customer in DropDownList
            try
            {
                string comp1 = (e.Row.FindControl("lbl_com") as Label).Text;
                ddl_comp.Items.FindByText(comp1).Selected = true;
            }
            catch (Exception)
            {

            }
            try
            {
                string comp2 = (e.Row.FindControl("lbl_con") as Label).Text;
                ddl_con.Items.FindByText(comp2).Selected = true;
            }
            catch (Exception)
            {

            }
            try
            {
                string comp3 = (e.Row.FindControl("lbl_width") as Label).Text;
                ddl_width.Items.FindByText(comp3).Selected = true;
            }
            catch (Exception)
            {

            }
            try
            {
                string comp4 = (e.Row.FindControl("lbl_weight") as Label).Text;
                ddl_weight.Items.FindByText(comp4).Selected = true;
            }
            catch (Exception)
            {

            }
            try
            {
                string comp5 = (e.Row.FindControl("lbl_altuompk") as Label).Text;
                ddl_AltUOM.Items.FindByValue(comp5).Selected = true;
            }
            catch (Exception)
            {

            }
            //Checkbox Ticking and unticking
            try
            {
                string iscd = (e.Row.FindControl("lbl_iscd") as Label).Text;
                string issd = (e.Row.FindControl("lbl_isSD") as Label).Text;
                string iscommon = (e.Row.FindControl("lbl_isCommon") as Label).Text;
                string isGD= (e.Row.FindControl("lbl_isGD") as Label).Text;

                CheckBox chk_iscd = (e.Row.FindControl("chk_isCD") as CheckBox);
                CheckBox chk_issd = (e.Row.FindControl("chk_isSD") as CheckBox);
                CheckBox chk_iscommon = (e.Row.FindControl("chk_isCommon") as CheckBox);
                CheckBox chk_isisGD = (e.Row.FindControl("chk_isisGD") as CheckBox);
                
                if (iscd.Trim() == "Y")
                {
                    chk_iscd.Checked = true;
                }
                else
                {
                    chk_iscd.Checked = false;
                }

                if (issd.Trim() == "Y")
                {
                    chk_issd.Checked = true;
                }
                else
                {
                    chk_issd.Checked = false;
                }

                if (isGD.Trim() == "Y")
                {
                    chk_isisGD.Checked = true;
                }
                else
                {
                    chk_isisGD.Checked = false;
                }

                if (iscommon.Trim() == "Y")
                {
                    chk_iscommon.Checked = true;
                }
                else
                {
                    chk_iscommon.Checked = false;
                }
            }
            catch (Exception)
            {


            }

            try
            {
                DropDownList ddl_body = (e.Row.FindControl("ddl_body") as DropDownList);
                string lbl_body = (e.Row.FindControl("lbl_body") as Label).Text;
                ddl_body.Items.FindByText(lbl_body).Selected = true;
            }
            catch (Exception)
            {


            }


        }
    }

    protected void Btn_updatesku_Click(object sender, EventArgs e)
    {
        UpdateSkuMaster();
       // ClientScript.RegisterStartupScript(this.GetType(), "Art", "alert('SKU Updated');", true);
        MessgeboxUpdate("sucess", "SKU Updated");
        Messaediv.InnerText = "SKU Updated";
    }

    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }

    protected void btn_generatesku_Click(object sender, EventArgs e)
    {
        generateSKU();
        upd_skugrid.Update();
        
    }

    public void MessgeboxUpdate(String Messagetype, String Messg)
    {
        if (Messagetype == "sucess")
        {
            Messaediv.Attributes["class"] = "success";

        }
        else
        {

        }
    }

    protected void btn_item0_Click(object sender, EventArgs e)
    {
        AddGDRawMaterialmaster();
        tbl_skumaster.DataBind();
        upd_rawmaterial.Update();
        upd_skugrid.Update();
        if (tbl_skumaster.Rows.Count > 0)
        {

        }
        else
        {

        }
    }
}
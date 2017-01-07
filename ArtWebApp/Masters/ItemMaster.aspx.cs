using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ArtWebApp.DataModels;
public partial class Masters_ItemMaster : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        String navtype = Request.QueryString["navtype"];

        string v = Request.QueryString["navtype"];
        if (navtype == "Item Group")
        {
            MultiView1.ActiveViewIndex = 0;
        }
        else if (navtype == "Item Master")
        {
            MultiView1.ActiveViewIndex = 1;
        }
        else if (navtype == "Item Composition")
        {
            MultiView1.ActiveViewIndex = 2;
        }
        else if (navtype == "Item Construction")
        {
            MultiView1.ActiveViewIndex = 3;
        }
        else if (navtype == "Item Weight")
        {
            MultiView1.ActiveViewIndex = 4;
        }
        else if (navtype == "Item Width")
        {
            MultiView1.ActiveViewIndex = 5;
        }
        else if (navtype == "Item Color")
        {
            MultiView1.ActiveViewIndex = 6;
        }
        else if (navtype == "Item Size")
        {
            MultiView1.ActiveViewIndex =7;
        }
        else if (navtype == "Item UOM")
        {
            MultiView1.ActiveViewIndex = 8;
        }
       
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        AddTemplateMaster();
    }


    public void AddTemplateMaster()
    {
        using (ArtEntitiesnew enty = new ArtEntitiesnew())
        {
            if (!enty.Template_Master.Any(f => f.TemplateCode.Trim() == txt_itemcode.Text.Trim() || f.Description.Trim() == txt_itemname.Text.Trim()))
            {
                Template_Master tmpmstr = new Template_Master();
                tmpmstr.TemplateCode = txt_itemcode.Text.Trim();
                tmpmstr.Description = txt_itemname.Text.Trim();
                tmpmstr.ItemGroup_PK = int.Parse(cmb_itemgroup.SelectedValue.ToString());
                tmpmstr.HCCode = txt_hscode.Text.Trim();
                tmpmstr.Wastage = int.Parse(txt_wastage.Text.Trim());
                tmpmstr.Uom_PK = int.Parse(cmb_uom.SelectedValue.ToString());
                enty.Template_Master.Add(tmpmstr);

                enty.SaveChanges();
            }
        }
    }
    public void AddTemplateComposition()
    {
        using (ArtEntitiesnew enty = new ArtEntitiesnew())
        {
            int templatepk = int.Parse(drp_templateforComp.SelectedValue.ToString());

            if (!enty.TemplateCompositions.Any(f => f.Template_Pk == templatepk && f.Composition.Trim () == txt_Composition.Text.Trim()))
            {
                TemplateComposition tmpcomp = new TemplateComposition();
                tmpcomp.Composition = txt_Composition.Text.Trim();
                tmpcomp.Template_Pk = templatepk;

                enty.TemplateCompositions.Add(tmpcomp);
                enty.SaveChanges();

            }
        }
    }

    public void AddTemplateConstruction()
    {
        using (ArtEntitiesnew enty = new ArtEntitiesnew())
        {
            int templatepk = int.Parse(drp_templateforcon.SelectedValue.ToString());

            if (!enty.TemplateConstructions.Any(f => f.Template_Pk == templatepk && f.Construct == txt_construction.Text.Trim()))
            {
                TemplateConstruction tmpcon = new TemplateConstruction();
                tmpcon.Construct = txt_construction.Text.Trim();
                tmpcon.Template_Pk = templatepk;

                enty.TemplateConstructions.Add(tmpcon);
                enty.SaveChanges();

            }
        }
    }
    public void AddTemplateWeight()
    {
        using (ArtEntitiesnew enty = new ArtEntitiesnew())
        {
            int templatepk = int.Parse(drp_templateweight.SelectedValue.ToString());

            if (!enty.TemplateWeights.Any(f => f.Template_Pk == templatepk && f.Weight == txt_weight.Text.Trim()))
            {
                TemplateWeight tmpcon = new TemplateWeight();
                tmpcon.Weight = txt_weight.Text.Trim();
                tmpcon.Template_Pk = templatepk;

                enty.TemplateWeights.Add(tmpcon);
                enty.SaveChanges();

            }
        }
    }
    public void AddTemplateWidth()
    {
        using (ArtEntitiesnew enty = new ArtEntitiesnew())
        {
            int templatepk = int.Parse(drp_templatewidth.SelectedValue.ToString());

            if (!enty.TemplateWeights.Any(f => f.Template_Pk == templatepk && f.Weight == txt_width.Text.Trim()))
            {
                TemplateWidth tmpcon = new TemplateWidth();
                tmpcon.Width = txt_width.Text.Trim();
                tmpcon.Template_Pk = templatepk;

                enty.TemplateWidths.Add(tmpcon);
                enty.SaveChanges();

            }
        }
    }
    public void AddTemplateColor()
    {
        using (ArtEntitiesnew enty = new ArtEntitiesnew())
        {
            int templatepk = int.Parse(drp_Color.SelectedValue.ToString());

            if (!enty.TemplateColors.Any(f => f.Template_PK == templatepk && f.TemplateColor1.Trim () == txt_color.Text.Trim()))
            {
                TemplateColor tmpcon = new TemplateColor();
                tmpcon.TemplateColor1 = txt_color.Text.Trim();
                tmpcon.Template_PK = templatepk;

                enty.TemplateColors.Add(tmpcon);
                enty.SaveChanges();

            }
        }
    }
    public void AddTemplateSize()
    {
        using (ArtEntitiesnew enty = new ArtEntitiesnew())
        {
            int templatepk = int.Parse(drp_Size.SelectedValue.ToString());

            if (!enty.TemplateSizes.Any(f => f.Template_PK == templatepk && f.TemplateSize1.Trim () == txt_size.Text.Trim()))
            {
                TemplateSize tmpcon = new TemplateSize();
                tmpcon.TemplateSize1 = txt_size.Text.Trim();
                tmpcon.Template_PK = templatepk;

                enty.TemplateSizes.Add(tmpcon);
                enty.SaveChanges();

            }
        }
    }

    protected void btn_SaveComp_Click(object sender, EventArgs e)
    {
        AddTemplateComposition();
    }

    protected void btn_SaveCon_Click(object sender, EventArgs e)
    {
        AddTemplateConstruction();

    }

    protected void btn_weight_Click(object sender, EventArgs e)
    {
        AddTemplateWeight();
    }

    protected void btn_width_Click(object sender, EventArgs e)
    {
        AddTemplateWidth();
    }

    protected void Btn_Color_Click(object sender, EventArgs e)
    {
        AddTemplateColor();

    }

    protected void btn_size_Click(object sender, EventArgs e)
    {
        AddTemplateSize();
    }
}
using ArtWebApp.DataModels;
using Infragistics.Web.UI.ListControls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Merchandiser_Styledetails : System.Web.UI.Page
{
    int atc_id = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack )
        {
            try
            {

                atc_id = int.Parse(Request.QueryString["atcid"]);
                ViewState.Add("AtcID", atc_id);
            }
            catch (Exception)
            {
            }
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {

    }
    protected void Btn_addSize_Click(object sender, EventArgs e)
    {
        AddSize();
    }
    protected void btn_AddColor_Click(object sender, EventArgs e)
    {
        
        AddColor();
    }


    public void AddSize()
    {
        List<DropDownItem> items = drp_size.SelectedItems;
        foreach (DropDownItem item in items)
        {
            String SizeCode = item.Value.ToString().Trim ();
            string SizeName = item.Text.Trim();
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {
                if (!enty.StyleSizes.Any(f => f.SizeName.Trim () == SizeName && f.OurStyle == drp_ourstyle.SelectedItem.Text.ToString()))
                {

                    StyleSize stsz = new StyleSize();
                    int ourstyleid = int.Parse(drp_ourstyle.SelectedValue.ToString());
                    stsz.OurStyle = drp_ourstyle.SelectedItem.Text.ToString();
                    stsz.OurStyleID = int.Parse(drp_ourstyle.SelectedValue.ToString());


                    stsz.SizeName = SizeName;
                    stsz.SizeCode = SizeCode;
                    stsz.AtcId = int.Parse(cmb_atc.SelectedValue.ToString ());

                    enty.StyleSizes.Add(stsz);
                    enty.SaveChanges();
                    grd_stylesize.DataBind();
                }
            }
        }
    }



    /// <summary>
    /// Add color to the Stylecolor
    /// </summary>
    public void AddColor()
    { List<DropDownItem> items = drp_color.SelectedItems;
    foreach (DropDownItem item in items)
    {
        String GarmentColorCode = item.Value.ToString().Trim();
        string GarmentColor = item.Text.Trim();
        using (ArtEntitiesnew enty = new ArtEntitiesnew())
        {
            if (!enty.StyleColors.Any(f => f.GarmentColorCode.Trim() == GarmentColorCode && f.OurStyle == drp_ourstyle.SelectedItem.Text.ToString()))
            {
                StyleColor clr = new StyleColor();



                int ourstyleid = int.Parse(Session["ourstyleID"].ToString());
                clr.OurStyle = drp_ourstyle.SelectedItem.Text.ToString();
                clr.OurStyleID = int.Parse(drp_ourstyle.SelectedValue.ToString());
                clr.GarmentColor = GarmentColor;
                clr.GarmentColorCode = GarmentColorCode;
                clr.AtcId = int.Parse(cmb_atc .SelectedValue.ToString ());

                enty.StyleColors.Add(clr);
                enty.SaveChanges();
                //  ColorDetailsData.DataBind();
                grd_stylecolor.DataBind();
            }
        }
    }
    }
    protected void WebDropDown2_SelectionChanged(object sender, Infragistics.Web.UI.ListControls.DropDownSelectionChangedEventArgs e)
    {

    }
    protected void drp_ourstyle_SelectionChanged(object sender, Infragistics.Web.UI.ListControls.DropDownSelectionChangedEventArgs e)
    {
       
    }
    protected void Button1_Click1(object sender, EventArgs e)
    {
        Session["ourstyleID"] = int.Parse(drp_ourstyle.SelectedValue.ToString());
        grd_stylecolor.DataBind();
        grd_stylesize.DataBind();
    }

    protected void BTN_ATC_Click(object sender, EventArgs e)
    {
        int atcid = int.Parse(cmb_atc.SelectedValue.ToString());

        using (ArtEntitiesnew enty = new ArtEntitiesnew())
        {
            var Ourstylequery = from ourstyle in enty.AtcDetails
                                where ourstyle.AtcId == atcid
                                select new
                                {
                                    name = ourstyle.OurStyle,
                                    pk = ourstyle.OurStyleID
                                };
            drp_ourstyle.DataSource = Ourstylequery.ToList();
            drp_ourstyle.DataBind();
        }
    }
  


    protected void grd_stylesize_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int index = Convert.ToInt32(e.CommandArgument);
        
        
        int lbl_stylesizeid = int.Parse((grd_stylecolor.Rows[index].FindControl("lbl_stylesizeid") as Label).Text);
        int lbl_ourstyleid = int.Parse((grd_stylecolor.Rows[index].FindControl("lbl_ourstyleid") as Label).Text);

        String lbl_sizename = (grd_stylecolor.Rows[index].FindControl("lbl_sizename") as Label).Text;
        String lbl_sizecode = (grd_stylecolor.Rows[index].FindControl("lbl_sizecode") as Label).Text;

        DeleteSizecode(lbl_ourstyleid, lbl_sizename, lbl_sizecode, lbl_stylesizeid);
        grd_stylesize.DataBind();
    }

    protected void grd_stylecolor_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int index = Convert.ToInt32(e.CommandArgument);

       int lbl_stylecolorid = int.Parse((grd_stylecolor.Rows[index].FindControl("lbl_stylecolorid") as Label).Text);

        int lbl_ourstyleid = int.Parse((grd_stylecolor.Rows[index].FindControl("lbl_ourstyleid") as Label).Text);
        String lbl_GarmentColor = (grd_stylecolor.Rows[index].FindControl("lbl_GarmentColor") as Label).Text;
        String lbl_GarmentColorCode = (grd_stylecolor.Rows[index].FindControl("lbl_GarmentColorCode") as Label).Text;

        DeleteColorcode(lbl_ourstyleid, lbl_GarmentColor, lbl_GarmentColorCode, lbl_stylecolorid);
        grd_stylecolor.DataBind();

    }





    /// <summary>
    /// delete sizecode if POpacknotemade
    /// </summary>
    /// <param name="ourstyleid"></param>
    /// <param name="sizename"></param>
    /// <param name="sizecode"></param>
    /// <param name="stylesizeid"></param>
    public void DeleteSizecode( int ourstyleid ,string sizename,string sizecode, int stylesizeid)
    {
        using (ArtEntitiesnew enty = new ArtEntitiesnew())
        {

            if (!enty.POPackDetails.Any(f => f.OurStyleID == ourstyleid && f.SizeName.Trim() == sizename.Trim() && f.SIzeCode.Trim() == sizecode.Trim()))
            {


               



                var q = from ponmbr in enty.StyleSizes
                        where ponmbr.StyleSizeID == stylesizeid
                        select ponmbr;


                foreach (var element in q)
                {
                    enty.StyleSizes.Remove(element);

                }
            }
            else
            {
                String Msg = " Cannot Delete Size  As ASQ is made for this Size";

                ClientScript.RegisterStartupScript(this.GetType(), "Art", "alert('" + Msg + "');", true);
            }

            enty.SaveChanges();
        }
    }





    /// <summary>
    /// delete colorcode if POpacknotemade
    /// </summary>
    /// <param name="ourstyleid"></param>
    /// <param name="sizename"></param>
    /// <param name="sizecode"></param>
    /// <param name="stylesizeid"></param>
    public void DeleteColorcode(int ourstyleid, string colorname, string colorcode, int stylecolorid)
    {
        using (ArtEntitiesnew enty = new ArtEntitiesnew())
        {

            if (!enty.POPackDetails.Any(f => f.OurStyleID == ourstyleid && f.ColorName.Trim() == colorname.Trim() && f.ColorCode.Trim() == colorcode.Trim()))
            {






                var q = from ponmbr in enty.StyleColors
                        where ponmbr.StyleColorid == stylecolorid
                        select ponmbr;


                foreach (var element in q)
                {
                    enty.StyleColors.Remove(element);

                }
            }
            else
            {
                String Msg = " Cannot Delete Color  As ASQ is made for this Color";

                ClientScript.RegisterStartupScript(this.GetType(), "Art", "alert('" + Msg + "');", true);
            }

            enty.SaveChanges();
        }
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        if(grd_stylesize.Rows.Count>0)
        {
            foreach (GridViewRow di in grd_stylesize.Rows)
            {
                Label lbl_stylesizeid = (di.FindControl("lbl_stylesizeid") as Label);
                TextBox txt_order = (di.FindControl("txt_order") as TextBox);
                int stylesizeid = int.Parse(lbl_stylesizeid.Text. ToString());
                using (ArtEntitiesnew enty = new ArtEntitiesnew())
                {
                    var q = from ponmbr in enty.StyleSizes
                            where ponmbr.StyleSizeID == stylesizeid
                            select ponmbr;


                    foreach (var element in q)
                    {
                        element.Orderof = int.Parse(txt_order.Text);

                    }

                    enty.SaveChanges();
                }

                }
            }
    }

    protected void Button3_Click(object sender, EventArgs e)
    {
        if (grd_stylesize0.Rows.Count > 0)
        {
            foreach (GridViewRow di in grd_stylesize.Rows)
            {
                Label lbl_stylesizeid = (di.FindControl("lbl_stylesizeid0") as Label);
                TextBox txt_order = (di.FindControl("txt_order0") as TextBox);
                int stylesizeid = int.Parse(lbl_stylesizeid.Text.ToString());
                using (ArtEntitiesnew enty = new ArtEntitiesnew())
                {
                    var q = from ponmbr in enty.StyleSizes
                            where ponmbr.StyleSizeID == stylesizeid
                            select ponmbr;


                    foreach (var element in q)
                    {
                        element.Orderof = int.Parse(txt_order.Text);

                    }

                    enty.SaveChanges();
                }

            }
        }
    }
}
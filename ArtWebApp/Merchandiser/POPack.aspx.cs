using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ArtWebApp.DataModels;
using System.Data;
using ArtWebApp.BLL;
public partial class Merchandiser_POPack : System.Web.UI.Page
{
    ArtWebApp.DBTransaction.PoPackTransaction potran = new ArtWebApp.DBTransaction.PoPackTransaction();
    ArtWebApp.BLL.PoPackMasterData pmdata = new PoPackMasterData();
    int rowindex = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

        }
        else
        {
            if (Convert.ToString(ViewState["Generated"]) == "true")
                FillPoDetails();

        }
    }




    protected void buttonAtc_Click(object sender, EventArgs e)
    {
        Session["atcid"] = int.Parse(cmb_atc.SelectedValue.ToString());

        fillcontrol();
        ViewState["sizecolordata"] = null;
    }





    public void fillcontrol()
    {
        int atcid = int.Parse(Session["atcid"].ToString());

        ArtEntitiesnew enty = new ArtEntitiesnew();
        var PoQuery = from pckmst in enty.PoPackMasters
                      where pckmst.AtcId == atcid
                      select new
                      {
                          name = pckmst.PoPacknum,
                          pk = pckmst.PoPackId
                      };

        var Ourstylequery = from ourstyle in enty.AtcDetails
                            where ourstyle.AtcId == atcid
                            select new
                            {
                                name = ourstyle.OurStyle,
                                pk = ourstyle.OurStyleID
                            };


        cmb_po.DataSource = PoQuery.ToList();
        cmb_po.DataBind();

        cmb_ourstyle.DataSource = Ourstylequery.ToList();
        cmb_ourstyle.DataBind();



        //showAllPoPackATC();
    }





    public void FillPoDetails()
    {
        GrdDynamic.DataSource = null;
        GrdDynamic.Columns.Clear();
        DataTable dt = new DataTable();


        string lbl_iscutable = ArtWebApp.BLL.popackupdater.IsASQCutable(int.Parse(Session["OurStyleid"].ToString()), int.Parse(Session["PoPackid"].ToString()));


        if (lbl_iscutable.Trim() == "Y")
        {
            lbl_postatus.Text = "Cuttable";
        }
        else
        {
            lbl_postatus.Text = "Open";
        }

        ViewState["sizecolordata"] = null;



        if (ViewState["sizecolordata"] == null)
        {

            dt = pmdata.createdatatable(int.Parse(Session["OurStyleid"].ToString()), int.Parse(Session["PoPackid"].ToString()));
            ViewState["sizecolordata"] = dt;
        }
        else
        {
            dt = (DataTable)ViewState["sizecolordata"];
        }


        //    GenerateTable(dt.Columns.Count, dt.Rows.Count,dt);

        foreach (DataColumn col in dt.Columns)
        {
            //Declare the bound field and allocate memory for the bound field.
            TemplateField bfield = new TemplateField();

            //Initalize the DataField value.
            bfield.HeaderTemplate = new ArtWebApp.Controls.GridViewTemplate(ListItemType.Header, col.ColumnName);

            //Initialize the HeaderText field value.
            bfield.ItemTemplate = new ArtWebApp.Controls.GridViewTemplate(ListItemType.Item, col.ColumnName);

            //Add the newly created bound field to the GridView.
            GrdDynamic.Columns.Add(bfield);
        }


        rowindex = dt.Rows.Count;
        GrdDynamic.DataSource = dt;

        GrdDynamic.DataBind();
        fillgeidwithvalue(dt);
    }





    public void fillgeidwithvalue(DataTable dt)
    {
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            for (int j = 0; j < dt.Columns.Count; j++)
            {
                String txtsizename = "txt_" + dt.Columns[j].ColumnName;
                String ColorName = GetColorname(i);
                (GrdDynamic.Rows[i].FindControl(txtsizename) as TextBox).Text = dt.Rows[i][j].ToString();
            }
        }
    }





    public float getnewvalue(DataTable dt)
    {
        float sum = 0;

        
      for(int i=0;i<dt.Rows.Count-1;i++)
        {
            for (int j = 1; j < dt.Columns.Count - 1; j++)
            {
                sum += float.Parse(dt.Rows[i][j].ToString());
            }
        }

        return sum;
    }


    protected void btn_savePoPack_Click(object sender, EventArgs e)
    {
        


        if (lbl_postatus.Text=="Open")
        {
            ArtWebApp.BLL.POPackDetailData pdata = new POPackDetailData();
            pdata.POPackdetcollection = LoadDatatable();
            pdata.PoPackId = int.Parse(cmb_po.SelectedValue.ToString());
            pdata.Ourstyleid = int.Parse(Session["OurStyleid"].ToString());

            int lastrow = pdata.POPackdetcollection.Rows.Count - 1;
            int lastcolcount = pdata.POPackdetcollection.Columns.Count-1;

            //  float newqty = float.Parse(pdata.POPackdetcollection.Rows[lastrow][lastcolcount].ToString ());

            float newqty = getnewvalue(pdata.POPackdetcollection);
            if (VaildateASQwithProjection(pdata.PoPackId, pdata.Ourstyleid, newqty, int.Parse (lbl_projection.Text)))  
            {
                pdata.insertPOPackDetails(pdata);
                pdata.UpdateAllocatedData(pdata.PoPackId, pdata.Ourstyleid);
                String Msg = " ASQ Details is generated Successfully";

                ClientScript.RegisterStartupScript(this.GetType(), "Art", "alert('" + Msg + "');", true);
            }

          
        }
        else
        {
            String Msg = " Cannot Update ASQ Marked As Cutable";

            ClientScript.RegisterStartupScript(this.GetType(), "Art", "alert('" + Msg + "');", true);
        }
      
    }




    public Boolean VaildateASQwithProjection(int popackid,int ourstyleid, float newvalue , int projqty)
    {
        Boolean isok = false;
        string oldtotal = lbl_ASQQTY.Text;
        

        float newtotal = float.Parse(oldtotal.ToString()) + float.Parse(newvalue.ToString());

        if(newtotal> projqty)
        {
            isok = false;

            String Msg = " Cannot Update ASQ Qty Greater than Projection";

            ClientScript.RegisterStartupScript(this.GetType(), "Art", "alert('" + Msg + "');", true);
        }
        else
        {
            isok = true;
        }

        return isok;
    }


    public DataTable LoadDatatable()
    {
        DataTable dt = new DataTable();
        if (ViewState["sizecolordata"] != null)
        {

            dt = (DataTable)ViewState["sizecolordata"];

            for (int i = 0; i < dt.Rows.Count-1; i++)
            {
                String ColorName = GetColorname(i);


                for (int j = 1; j < dt.Columns.Count-1; j++)
                {
                    String txtsizename = "txt_" + dt.Columns[j].ColumnName;

                    String poqty = GetSizename(i, txtsizename);

                    try
                    {
                        dt.Rows[i][j] = float.Parse(poqty);
                    }
                    catch (Exception)
                    {

                        dt.Rows[i][j] = 0;
                    };
                }


            }

        }

        return dt;
    }

    public String GetColorname(int i)
    {
        String Colorname = (GrdDynamic.Rows[i].FindControl("txt_color") as TextBox).Text.ToString().Trim();

        return Colorname;
    }

    public String GetSizename(int i, String txtname)
    {
        String Qty = "0";

        try
        {
            Qty = (GrdDynamic.Rows[i].FindControl(txtname) as TextBox).Text.ToString().Trim();
        }
        catch (Exception)
        {


        }

        return Qty;
    }

    protected void btn_hidenewPo_Click(object sender, EventArgs e)
    {

    }


    public void validationcontrol()
    {
        Boolean isvalidated = true;
        String atcval = "";
        String PoValue = "";
        String Ourstyle = "";
        if (cmb_atc.SelectedValue != null && cmb_atc.SelectedValue.ToString().Trim() != "")
        {
            atcval = "Select a Atc";
        }
        else if (cmb_po.SelectedValue != null && cmb_po.SelectedValue.ToString().Trim() != "")
        {
            PoValue = "Select a PO";
        }
        else if (cmb_ourstyle.SelectedValue != null && cmb_ourstyle.SelectedValue.ToString().Trim() != "")
        {
            Ourstyle = "Select a Ourstyle";
        }
        else
        {
            isvalidated = true;
        }
        lbl_errordisplayer.Text = atcval + Environment.NewLine + PoValue + Environment.NewLine + Ourstyle;
    }

    protected void btn_Showpanel_Click(object sender, EventArgs e)
    {
        if (cmb_atc.SelectedValue != null && cmb_atc.SelectedValue.ToString().Trim() != "")
        {

            Response.Redirect("~/Merchandiser/AddPOPack.aspx?atcid=" + cmb_atc.SelectedValue.ToString() + "&atcnum=" + cmb_atc.SelectedItem.Text.ToString() + "");
        }
        else
        {
            lbl_errordisplayer.Text = "Select Atc";
        }
    }



    protected void GrdDynamic_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            try
            {
                TextBox color = (e.Row.FindControl("txt_color") as TextBox);
                color.Enabled = false;
                //  color.CssClass = "txt_color";
            }
            catch (Exception)
            {


            }
            try
            {
                TextBox color = (e.Row.FindControl("txt_ColorTotal") as TextBox);
                color.Enabled = false;
                // color.CssClass = "txt_ColorTotal";
            }
            catch (Exception)
            {


            }
            try
            {
                TextBox color = (e.Row.FindControl("txt_SizeTotal") as TextBox);
                color.Enabled = false;
            }
            catch (Exception)
            {


            }
            try
            {
                if (e.Row.RowIndex == rowindex - 1)
                {
                    e.Row.Enabled = false;
                    e.Row.CssClass = "SizeTotal";

                }


            }
            catch (Exception)
            {


            }
        }
    }

    protected void btn_confirmOurstyle_Click(object sender, EventArgs e)
    {
        Session["OurStyleid"] = int.Parse(cmb_ourstyle.SelectedValue.ToString());

        string projqty = ArtWebApp.BLL.popackupdater.OurStyleProjectionQty(int.Parse(cmb_ourstyle.SelectedValue.ToString()));

        String allasqqty= ArtWebApp.BLL.popackupdater.ASQQtyOtherthanGivenASQ(int.Parse(cmb_ourstyle.SelectedValue.ToString()), int.Parse(cmb_po.SelectedValue.ToString()));
        lbl_projection.Text = projqty;
        lbl_ASQQTY.Text = allasqqty;

        if (validateAtcAndOurstyle())
        {

            if (validateAtcAndOurstyle())
            {
                FillPoDetails();

                UpdatePanel2.Update();
                ViewState["Generated"] = "true";
            }

            ViewState["sizecolordata"] = null;
        }
    }



    private void GenerateTable(int colsCount, int rowsCount, DataTable dt)
    {

        //Creat the Table and Add it to the Page
        //Table table = new Table();
        //table.ID = "Table1";
        //Page.Form.Controls.Add(table);

        for (int i = 0; i < colsCount; i++)
        {


        }


        // Now iterate through the table and add your controls 
        for (int i = 0; i < rowsCount; i++)
        {
            TableRow row = new TableRow();
            for (int j = 0; j < colsCount; j++)
            {
                TableCell cell = new TableCell();
                TextBox tb = new TextBox();

                // Set a unique ID for each TextBox added
                tb.ID = "TextBoxRow_" + i + "Col_" + j;
                // Add the control to the TableCell
                cell.Controls.Add(tb);
                //cell.Text = dt.Rows[i][j].ToString().Trim();
                // Add the TableCell to the TableRow
                row.Cells.Add(cell);
            }

            // Add the TableRow to the Table
            Table1.Rows.Add(row);
        }
    }

    protected void Btn_showPO_Click(object sender, EventArgs e)
    {
        Session["PoPackid"] = int.Parse(cmb_po.SelectedValue.ToString());
    }

    public Boolean validateAtcAndOurstyle()
    {
        if (Session["Ourstyleid"].ToString().Trim() == "")
        {
            lbl_errordisplayer.Text = "Confirm Ourstyle";
            CustomValidator1.IsValid = false;
            CustomValidator1.ErrorMessage = lbl_errordisplayer.Text;
        }
        else if (Session["PoPackid"].ToString().Trim() == "")
        {
            lbl_errordisplayer.Text = "Confirm PoPackid";
            CustomValidator1.IsValid = false;
            CustomValidator1.ErrorMessage = lbl_errordisplayer.Text;
        }
        return CustomValidator1.IsValid;
    }

    protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
    {

    }



    //protected void cmb_ourstyle_DataBound(object sender, EventArgs e)
    //{
    //    cmb_ourstyle.SelectedIndex = 0;
    //}

    protected void cmb_po_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["PoPackid"] = int.Parse(cmb_po.SelectedValue.ToString());
    }

    protected void cmb_atc_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    //protected void cmb_ourstyle_ValueChanged(object sender, Infragistics.Web.UI.ListControls.DropDownValueChangedEventArgs e)
    //{
    //    Session["OurStyleid"] = int.Parse(cmb_ourstyle.SelectedValue.ToString());
    //}

    //protected void cmb_po_ValueChanged(object sender, Infragistics.Web.UI.ListControls.DropDownValueChangedEventArgs e)
    //{
    //    Session["PoPackid"] = int.Parse(cmb_po.SelectedValue.ToString());
    //}

    //protected void cmb_po_SelectionChanged(object sender, Infragistics.Web.UI.ListControls.DropDownSelectionChangedEventArgs e)
    //{

    //}







}
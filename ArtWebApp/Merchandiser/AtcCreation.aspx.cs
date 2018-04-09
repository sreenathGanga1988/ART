using ArtWebApp.DataModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ArtWebApp.DBTransaction;
public partial class Merchandiser_AtcCreation : System.Web.UI.Page
{
    AtcTransaction atctran = new AtcTransaction();
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        AddAtcentry();
    }

    /// <summary>
    /// insert the basic Atc Details Without Atcnumber
    /// </summary>
    public void AddAtcentry()
    {
        using (ArtEntitiesnew enty = new ArtEntitiesnew())
        {
            AtcMaster mstr = new AtcMaster();
            mstr.Buyer_ID = int.Parse(cmb_Buyer.SelectedValue.ToString());
            mstr.Country_ID = int.Parse(cmb_country.SelectedValue.ToString());
            mstr.FinishDate = dtp_finishdate.Date;
            mstr.HouseDate = dtp_housedate.Date;
            mstr.MerchandiserName = drp_merchandiser.SelectedItem.Text.Trim();
            mstr.SeasonName = drp_season.SelectedItem.Text.Trim();
            mstr.NoofStyles = int.Parse(txt_stylenum.Text);
            mstr.ShipDate = dtp_shipStartdate.Date;
            mstr.IsClosed = "N";
            mstr.IsCompleted = "N";
            mstr.AddedBy = Session["Username"].ToString().Trim(); 
            mstr.AddedDate = DateTime.Now;
            mstr.ProjectionQty = int.Parse(txt_projqty.Text);
            mstr.IsProjectionApproved = "N";
            mstr.ProductionCountryID= int.Parse(cmb_prodcountry.SelectedValue.ToString());
            mstr.IsAtcWord = "Y";
            enty.AtcMasters.Add(mstr);     
                     
           
            String atcnum = CreateAtcnum( int.Parse(cmb_Buyer.SelectedValue.ToString()));
            

            mstr.AtcNum = atcnum.Trim();

            enty.SaveChanges();
             int atcid = int.Parse(mstr.AtcId.ToString());
            lbl_atcid.Text  = atcid.ToString();
            
         
            if(int.Parse(txt_stylenum.Text)>0)
            {
                if (GridView1.Rows.Count < 1)
                {

                    DataTable dt = createdatatable(int.Parse(txt_stylenum.Text), atcnum, atcid, dtp_shipStartdate.Date);

                   atctran. insertourstyle(dt);


                   GridView1.DataSource = atctran.GetOurStyleDetails(atcid);
                    GridView1.DataBind();
                    Btn_addetails.Visible = true;
                  
                }
            }

            String Msg = "Atc # : " + atcnum + " is generated Successfully";


           
            ClientScript.RegisterStartupScript(this.GetType(), "Art", "alert('" + Msg + "');", true);
        }

    }


    /// <summary>
    /// create the Atcnumber and Atcid Based on the 
    /// </summary>
    /// <param name="atcid"></param>
    /// <param name="buyerid"></param>
    public String  CreateAtcnum( int buyerid)
    {
        String atcnum = "";
        using (ArtEntitiesnew enty = new ArtEntitiesnew())
        {
            var count = (from o in enty.AtcMasters 
                         
                         select o).Count();

            //from buyer master u where u.buyerid = buyerid select prefix
            var prefix = enty.BuyerMasters.Where(u => u.BuyerID == buyerid).Select(u => u.Prefix).FirstOrDefault();

            atcnum = prefix.ToString().Trim() +(int.Parse(count.ToString())+6000).ToString();
        }

        return atcnum;
    }




    public DataTable  createdatatable(int totalstylenum,String AtcNum,int atcid,DateTime shipstartdate)
    { 
        String ourstyle="";
        DataTable table = new DataTable();
        table.Columns.Add("OurstyleID", typeof(int));
        table.Columns.Add("atcid", typeof(int));
       
        table.Columns.Add("OurStyle", typeof(string));
        table.Columns.Add("BuyerStyle", typeof(string));
        table.Columns.Add("Qty", typeof(float ));
        table.Columns.Add("Fob", typeof(float));
        table.Columns.Add("Catid", typeof(float));
        table.Columns.Add("MinutesperGarment", typeof(float));
        table.Columns.Add("MerchantPCD", typeof(DateTime));
        for (int i=1;i<totalstylenum+1 ;i++)
        {
            if(i<9)
            {
                ourstyle = "0" + i.ToString() + AtcNum;
            }
            else
            {
                ourstyle = i.ToString() + AtcNum;
            }
            table.Rows.Add(i, atcid, ourstyle, "Edit ", 0.0, 0.0,0.0,0.0, shipstartdate);
        }
        return table;
    }
    protected void Gridview1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void WebDataGrid1_DataBound(object sender, EventArgs e)
    {
       
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        insertOurstyle();
        String Msg = "Style Details Updated Successfully";



        ClientScript.RegisterStartupScript(this.GetType(), "Art", "alert('" + Msg + "');", true);

    }

    public void insertOurstyle()
    {
        foreach (GridViewRow row in GridView1 .Rows)
        {
            int outstyleid=int.Parse ( row.Cells[0].Text .ToString());
            int atcid = int.Parse(row.Cells[1].Text.ToString());

            String Ourstyle = row.Cells[2].Text.ToString();


            DropDownList buyerstyledrp = (DropDownList)row.Cells[0]
                                                     .FindControl("Mydrop");
            DropDownList garmentcategory = (DropDownList)row.Cells[0]
                                                   .FindControl("DropDownList1");
            TextBox txtqty = (TextBox)row.Cells[0].FindControl("txtQty");
            TextBox fobtxt = (TextBox)row.Cells[0].FindControl("txtfob");


            string buyerstyle = buyerstyledrp.SelectedItem .Text.Trim();

            String Garmentcategory = garmentcategory.SelectedItem.Text.Trim();

            int catid = int.Parse(garmentcategory.SelectedValue .ToString());
           // int  qty = int.Parse(txtqty.Text);
            decimal fob = decimal.Parse(fobtxt.Text);



            using (ArtEntitiesnew enty = new ArtEntitiesnew())
          {

              if (!enty.AtcDetails.Any(f => f.OurStyle.Trim() == Ourstyle.Trim() && f.AtcId == atcid))
              {

                  AtcDetail atcdet = new AtcDetail();
                  atcdet.AtcId = atcid;
                  atcdet.OurStyle = Ourstyle.Trim();
                  atcdet.BuyerStyle = buyerstyle.ToString().Trim();
                  atcdet.Quantity = 0;
                  atcdet.FOB = fob;
                  atcdet.CategoryID = 0;
                  atcdet.IsInterChangable = "N";
                  atcdet.IsCMLocked = "N";
                  enty.AtcDetails.Add(atcdet);

                  enty.SaveChanges();
              }
          }
          Btn_addetails.Visible = false;

        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
       
    }
    protected void Button2_Click1(object sender, EventArgs e)
    {
        Response.Redirect(string.Format("~/Merchandiser/Styledetails.aspx?atcid={0}", lbl_atcid.Text ));
    }
}
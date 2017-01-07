using ArtWebApp.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArtWebApp.BLL.InventoryBLL
{
    public partial class SessionChecker : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Label1.Text = Application["OnlineUsers"].ToString();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
         //   CopyCostingFromOneStyletoAnother(1,2);

            for (int i = 0; i < Session.Contents.Count; i++)
            {
                Response.Write(Session.Keys[i] + " - " + Session[i] + "<br />");
            }
        }



        //public int getOldStyleApprovedCostingPK(int fromstyle)
        //{
        //    int costingpk = 0;
        //    using (ArtEntitiesnew enty = new ArtEntitiesnew())
        //    {


        //        var costingpkc = (from o in enty.StyleCostingMasters
        //                          where o.IsApproved == "A" && o.OurStyleID == fromstyle
        //                          select o.Costing_PK).Max();


        //        costingpk = int.Parse(costingpkc.ToString());
        //    }

        //    return costingpk;
        //}



        //public void CopyCostingFromOneStyletoAnother(int oldcostingpk, int  newcostingpk)
        //{
           

        //    using (ArtEntitiesnew enty = new ArtEntitiesnew())
        //    {              



        //        var q = from oldcostingdata in enty.StyleCostingDetails
        //                where oldcostingdata.Costing_PK == oldcostingpk
        //                select oldcostingdata;

        //        foreach (var existingcostingdata in  q)
        //        {

        //            StyleCostingDetail detail = new StyleCostingDetail();
        //            detail.Costing_PK = newcostingpk;
        //            detail.Sku_PK = existingcostingdata.Sku_PK;
        //            detail.Consumption =existingcostingdata.Consumption;
        //            detail.IsRequired = existingcostingdata.IsRequired;
        //            detail.PriceperDozen = existingcostingdata.PriceperDozen;
        //            detail.Priceperpc = existingcostingdata.Priceperpc;
        //            detail.Rate = existingcostingdata.Rate;
        //            enty.StyleCostingDetails.Add(detail);                 


        //        }





        //    }





        //}


        //public int insertcostingmaster(int ourstyleid)
        //{
        //    int stylecostingpk = 0;
        //    using (ArtEntitiesnew enty = new ArtEntitiesnew())
        //    {
        //        var count = (from o in enty.StyleCostingMasters
        //                     where o.OurStyleID == ourstyleid

        //                     select o).Count();

        //        StyleCostingMaster scmstr = new StyleCostingMaster();
        //        scmstr.OurStyleID = ourstyleid;
        //        scmstr.CreatedBy = HttpContext.Current.Session["Username"].ToString();
        //        scmstr.CostingCount = int.Parse(count.ToString()) + 1;
        //        scmstr.IsApproved = "N";
        //        scmstr.IsAccountable = "N";
        //        scmstr.IsLast = "Y";
        //        scmstr.CreatedDate = DateTime.Now;
        //        scmstr.FOB = 0;
        //        scmstr.TotalCost = 0;
        //        scmstr.Margin = 0;
        //        scmstr.MarginValue = 0;
        //        enty.StyleCostingMasters.Add(scmstr);
        //        enty.SaveChanges();

        //        stylecostingpk = int.Parse(scmstr.Costing_PK.ToString());
        //        MarkOtherCostingOld(ourstyleid,stylecostingpk);
        //    }

        //    return stylecostingpk;



        //}


        //public void MarkOtherCostingOld(int ourstyleid, int costing_pk)
        //{
        //    using (ArtEntitiesnew enty = new ArtEntitiesnew())
        //    {
        //        var datatounapprove = from cstingmstr in enty.StyleCostingMasters
        //                              where cstingmstr.Costing_PK != costing_pk && cstingmstr.OurStyleID == ourstyleid
        //                              select cstingmstr;

        //        foreach (var element in datatounapprove)
        //        {
        //            element.IsLast = "N";
        //        }

        //        enty.SaveChanges();
        //    }
        //}
    }
}
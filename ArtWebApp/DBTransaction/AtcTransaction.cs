using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ArtWebApp.DataModels;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using ArtWebApp.BLL;
namespace ArtWebApp.DBTransaction
{
    public class AtcTransaction
    {
        String connStr = ConfigurationManager.ConnectionStrings["ArtConnectionString"].ConnectionString;
        /// <summary>
        /// Fill the combo box with the active Atcs
        /// </summary>
        /// <param name="drpCombo"></param>
        public void FillAtcDropDown(DropDownList drpCombo)
        {

            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {

                var q = from atcmstr in enty.AtcMasters
                        select new
                {
                    name = atcmstr.AtcNum,
                    pk = atcmstr.AtcId
                };

                drpCombo.DataSource = q.ToList();
                drpCombo.DataBind();

            }

        }

        public void fillOurStyle(DropDownList drpCombo, int atcid)
        {
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {

                var q = from atcmstr in enty.AtcDetails
                        select new
                        {
                            name = atcmstr.OurStyle,
                            pk = atcmstr.OurStyleID
                        };

                drpCombo.DataSource = q.ToList();
                drpCombo.DataBind();

            }
        }


        public void insertourstyle(DataTable dt)
        {

            if (dt.Rows.Count > 0)
            {
                using (ArtEntitiesnew enty = new ArtEntitiesnew())
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        int outstyleid = int.Parse(dt.Rows[i]["OurStyleId"].ToString());
                        int atcid = int.Parse(dt.Rows[i]["atcid"].ToString());

                        String Ourstyle = dt.Rows[i]["Ourstyle"].ToString();
                        int catid = int.Parse(dt.Rows[i]["Catid"].ToString());
                        int qty = 0;
                        decimal fob = int.Parse (dt.Rows[i]["Fob"].ToString());
                        
                        DateTime pcd= DateTime.Parse(dt.Rows[i]["MerchantPCD"].ToString());
                        if (!enty.AtcDetails.Any(f => f.OurStyle.Trim() == Ourstyle.Trim() && f.AtcId == atcid))
                        {
                            AtcDetail atcdet = new AtcDetail();
                            atcdet.AtcId = atcid;
                            atcdet.OurStyle = Ourstyle.Trim();
                            atcdet.BuyerStyle = "";
                            atcdet.Quantity = qty;
                            atcdet.FOB = fob;
                            atcdet.CategoryID = 0;
                            atcdet.MerchantPCD = pcd;
                            enty.AtcDetails.Add(atcdet);
                        }
                        else
                        {


                        }
                    }

                    enty.SaveChanges();
                }

            }


        }
        public DataTable GetOurStyleDetails(int atcid)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();


                SqlCommand cmd = new SqlCommand(@"SELECT        OurStyleID, AtcId, OurStyle, BuyerStyle, FOB, CategoryID,Quantity, MinutesperGarment, MerchantPCD
FROM            AtcDetails
WHERE        (AtcId = @Param1)", con);


                cmd.Parameters.AddWithValue("@Param1", atcid);

                SqlDataReader rdr = cmd.ExecuteReader();

                dt.Load(rdr);



            }
            return dt;
        }

        public void UpdatemasterData(BLL.AtcData atcdata)
        {
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {

                var q = from atcmstr in enty.AtcMasters
                        where atcmstr.AtcId == atcdata.Atcid
                        select atcmstr;

                foreach (var element in q)
                {
                    element.MerchandiserName = atcdata.Merchandisername.Trim();
                    element.FinishDate = atcdata.Finshdate1;
                    element.HouseDate = atcdata.InHouseDate;
                    element.ShipDate = atcdata.ShipDate1;
                    element.NoofStyles = atcdata.Noofstyles;
                }
                enty.SaveChanges();


            }
        }









        public void ForwardOurstyle(int OurStyleApproval_PK)
        {
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {

                var q = from atcmstr in enty.AtcDetailApprovals
                        where atcmstr.OurStyleApproval_PK == OurStyleApproval_PK
                        select atcmstr;
                foreach (var element in q)
                {
                    element.IsForwarded = "Y";
                    element.ForwardedBY = HttpContext.Current.Session["Username"].ToString().Trim();
                }

                enty.SaveChanges();
            }
        }



        public void ForwardAtcProjection(int atcApprove_PK)
        {
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {

                var q = from atcmstr in enty.AtcApprovals
                        where atcmstr.AtcApproval_PK == atcApprove_PK
                        select atcmstr;
                foreach (var element in q)
                {
                    element.IsForwarded = "Y";
                    element.ForwardedBY = HttpContext.Current.Session["Username"].ToString().Trim();
                }

                enty.SaveChanges();
            }
        }




        public void ApproveOurStyle(int costingPK)
        {

            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {

                // gOTTHE OURSTYLE ID
                var Ourstyledata = (from o in enty.AtcDetailApprovals
                                    where o.OurStyleApproval_PK == costingPK

                                    select o.OurStyleID).FirstOrDefault();
                int ourstyleid = int.Parse(Ourstyledata.ToString());


                Decimal qty = 0;

              
                var datatoapprove = from cstingmstr in enty.AtcDetailApprovals
                                    where cstingmstr.OurStyleApproval_PK == costingPK && cstingmstr.OurStyleID == ourstyleid
                                    select cstingmstr;

                foreach (var element123 in datatoapprove)
                {



                    if (!enty.AtcDetailApprovals.Any(f => f.OurStyleID == ourstyleid && f.IsApproved == "A"))
                    {
                        element123.IsFirst = "Y";
                    }
                    else
                    {
                        element123.IsFirst = "N";
                    }

                    element123.IsApproved = "A";
                  
                    element123.ApprovedBY = HttpContext.Current.Session["Username"].ToString();
                    element123.ApprovedDate = DateTime.Now;
                    qty = Decimal.Parse( element123.Quantity.ToString ());
                }



                var datatounapprove = from atcdet in enty.AtcDetails
                                      where atcdet.OurStyleID == ourstyleid
                                      select atcdet;
                //mARKED ALL UNAPPROVED
                foreach (var element in datatounapprove)
                {
                    element.Quantity = qty;

                }





                enty.SaveChanges();
            }


        }


        public void ApproveAtcProjection(int atcApprove_PK)
        {

            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {

                // gOTTHE OURSTYLE ID
                var Ourstyledata = (from o in enty.AtcApprovals
                                    where o.AtcApproval_PK == atcApprove_PK

                                    select o.AtcId).FirstOrDefault();
                int atcid = int.Parse(Ourstyledata.ToString());


                Decimal qty = 0;


                var datatoapprove = from cstingmstr in enty.AtcApprovals
                                    where cstingmstr.AtcApproval_PK == atcApprove_PK && cstingmstr.AtcId == atcid
                                    select cstingmstr;

                foreach (var element123 in datatoapprove)
                {



                    if (!enty.AtcApprovals.Any(f => f.AtcId == atcid && f.IsApproved == "A"))
                    {
                        element123.IsFirst = "Y";
                    }
                    else
                    {
                        element123.IsFirst = "N";
                    }

                    element123.IsApproved = "A";

                    element123.ApprovedBY = HttpContext.Current.Session["Username"].ToString();
                    element123.ApprovedDate = DateTime.Now;
                    qty = Decimal.Parse(element123.Quantity.ToString());
                }



                var datatounapprove = from atcdet in enty.AtcMasters
                                      where atcdet.AtcId == atcid
                                      select atcdet;
                //mARKED ALL UNAPPROVED
                foreach (var element in datatounapprove)
                {
                    element.ProjectionQty = qty;

                }





                enty.SaveChanges();
            }


        }


    }
}
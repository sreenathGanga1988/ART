using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtWebApp.DBTransaction
{
    public class PoPackTransaction
    {
        String connStr = ConfigurationManager.ConnectionStrings["ArtConnectionString"].ConnectionString;
        public DataTable GetPoPackDetails(int popackid)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();


                SqlCommand cmd = new SqlCommand(@"SELECT        POPackDetails .PoPack_Detail_PK, POPackDetails .POPackId, AtcDetails.OurStyle, POPackDetails .ColorName, POPackDetails .ColorCode, POPackDetails .SizeName, POPackDetails .SIzeCode,
                          POPackDetails .PoQty AS POQty, POPackDetails .OurStyleID
FROM            POPackDetails  INNER JOIN
                         AtcDetails ON POPackDetails .OurStyleID = AtcDetails.OurStyleID
WHERE        (POPackDetails .POPackId = @Param1)", con);


                cmd.Parameters.AddWithValue("@Param1", popackid);

                SqlDataReader rdr = cmd.ExecuteReader();

                dt.Load(rdr);



            }
            return dt;
        }
        /// <summary>
        /// Get the Rawmaterialmaster count of atc
        /// </summary>
        /// <param name="atcid"></param>
        /// <returns></returns>
        public DataTable GetAtcRawmaterialMaster(int atcid)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();


                SqlCommand cmd = new SqlCommand(@"SELECT        AtcRaw_PK, Template_PK, Atc_id, TemplateName, TempCode, TemplateCount
FROM            AtcRawMaterialMaster 
WHERE        (Atc_id = @Param1)", con);


                cmd.Parameters.AddWithValue("@Param1", atcid);

                SqlDataReader rdr = cmd.ExecuteReader();

                dt.Load(rdr);



            }
            return dt;
        }

        /// <summary>
        /// get All Atc PoPack
        /// </summary>
        /// <param name="atcid"></param>
        /// <returns></returns>
        public DataTable GetAllAtcPoPack(int atcid)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();


                SqlCommand cmd = new SqlCommand(@"SELECT        POPackDetails.PoPack_Detail_PK, POPackDetails.POPackId, AtcDetails.OurStyle, POPackDetails.ColorName, POPackDetails.ColorCode, POPackDetails.SizeName, POPackDetails.SIzeCode, 
                         POPackDetails.PoQty AS POQty, POPackDetails.AddedBy, POPackDetails.AddedDate
FROM            POPackDetails INNER JOIN
                         AtcDetails ON POPackDetails.OurStyleID = AtcDetails.OurStyleID
WHERE        (AtcDetails.AtcId = @Param1)", con);


                cmd.Parameters.AddWithValue("@Param1", atcid);

                SqlDataReader rdr = cmd.ExecuteReader();

                dt.Load(rdr);



            }
            return dt;
        }

        /// <summary>
        /// Get the Buyer PoPack of a Atc
        /// 
        /// </summary>
        /// <param name="atcid"></param>
        /// <returns></returns>

        public DataTable GetAllBuyerPoPack(int atcid)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();


                SqlCommand cmd = new SqlCommand(@"SELECT        PoPackMaster.BuyerPO + ' / ' + PoPackMaster.PoPacknum AS POnum, PoPackMaster.PoPackId
FROM            PoPackMaster INNER JOIN
                         POPackDetails ON PoPackMaster.PoPackId = POPackDetails.POPackId INNER JOIN
                         AtcDetails ON POPackDetails.OurStyleID = AtcDetails.OurStyleID
GROUP BY PoPackMaster.BuyerPO + ' / ' + PoPackMaster.PoPacknum, PoPackMaster.PoPackId, PoPackMaster.AtcId
HAVING        (PoPackMaster.AtcId = @Param1)", con);


                cmd.Parameters.AddWithValue("@Param1", atcid);

                SqlDataReader rdr = cmd.ExecuteReader();

                dt.Load(rdr);



            }
            return dt;
        }

        //get the po mentioned in the query
        public DataTable getPodetails(String Qry)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();




                SqlCommand cmd = new SqlCommand(Qry, con);


                SqlDataReader rdr = cmd.ExecuteReader();

                dt.Load(rdr);



            }
            return dt;
        }



        /// <summary>
        /// get
        /// </summary>
        /// <param name="atcid"></param>
        /// <param name="ourstyleid"></param>
        /// <returns></returns>
        public DataTable GetPOPackDetailsofaStyle(int popackid, int ourstyleid)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();


                SqlCommand cmd = new SqlCommand(@"SELECT        POPackDetails.PoPack_Detail_PK, POPackDetails.POPackId, POPackDetails.OurStyleID, POPackDetails.ColorName, POPackDetails.ColorCode, POPackDetails.SizeName, POPackDetails.SIzeCode, 
                         POPackDetails.PoQty, 0 AS newQty, 0 AS AdjustedQty, (PoPackMaster.PoPacknum+'/ '+ PoPackMaster.BuyerPO) as ASQ
FROM            POPackDetails INNER JOIN
                         PoPackMaster ON POPackDetails.POPackId = PoPackMaster.PoPackId
WHERE        (PoPackMaster.PoPackId = @Param1) AND (OurStyleID = @Param2)", con);


                cmd.Parameters.AddWithValue("@Param1", popackid);
                cmd.Parameters.AddWithValue("@Param2", ourstyleid);


                SqlDataReader rdr = cmd.ExecuteReader();

                dt.Load(rdr);



            }
            return dt;
        }




        public DataTable GetPOPACKDetailsofList(int ourstyleid, ArrayList Popackdetlist)
        {
            DataTable dt = new DataTable();
            string condition = "And (";

            for (int i = 0; i < Popackdetlist.Count; i++)
            {
                if (i == 0)
                {
                    condition = condition + " POPackDetails.POPackId=" + Popackdetlist[i].ToString().Trim();
                }
                else
                {
                    condition = condition + "  or POPackDetails.POPackId=" + Popackdetlist[i].ToString().Trim();
                }



            }
            condition = condition + ")";

            if (condition != "And ()")
            {
                String query = @"SELECT        POPackDetails.PoPack_Detail_PK, POPackDetails.POPackId, POPackDetails.OurStyleID, POPackDetails.ColorName, POPackDetails.ColorCode, POPackDetails.SizeName, POPackDetails.SIzeCode, 
                         POPackDetails.PoQty, 0 AS newQty, 0 AS AdjustedQty, (PoPackMaster.PoPacknum+'/ '+ PoPackMaster.BuyerPO) as ASQ
FROM            POPackDetails INNER JOIN
                         PoPackMaster ON POPackDetails.POPackId = PoPackMaster.PoPackId where (OurStyleID = " + ourstyleid + ") " + condition;
                DBTransaction.PoPackTransaction pktrans = new DBTransaction.PoPackTransaction();
                dt = pktrans.getPodetails(query);
            }
            return dt;

        }








    }
}
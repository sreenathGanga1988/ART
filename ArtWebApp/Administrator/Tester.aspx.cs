using ArtWebApp.DataModelAtcWorld;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArtWebApp.Administrator
{
    public partial class Tester : System.Web.UI.Page
    {
      
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            doaction();
        }



        public void doaction()
        {

            DataTable Sizeonart = GetArtData();
            DataTable sizeonAtcwold = GetAtcWorldData();

            for (int i = 0; i < Sizeonart.Rows.Count; i++)
            {
                int stylesizeid = int.Parse(Sizeonart.Rows[i]["StyleSizeID"].ToString());
                string ourstyleid = Sizeonart.Rows[i]["StyleSizeID"].ToString().Trim();
                string OurStyle = Sizeonart.Rows[i]["OurStyle"].ToString().Trim();
                string SizeName = Sizeonart.Rows[i]["SizeName"].ToString().Trim();
                string SizeCode = Sizeonart.Rows[i]["SizeCode"].ToString().Trim();
                string AtcId = Sizeonart.Rows[i]["AtcId"].ToString().Trim();
                string Orderof = Sizeonart.Rows[i]["Orderof"].ToString().Trim();
                try
                {
                    DataTable newresult = sizeonAtcwold.Select("StyleSizeID=" + stylesizeid.ToString()).CopyToDataTable();
                                    


                    string atcwordourstyleid = sizeonAtcwold.Rows[0]["StyleSizeID"].ToString().Trim();
                    string atcwordOurStyle = sizeonAtcwold.Rows[0]["OurStyle"].ToString().Trim();
                    string atcwordSizeName = sizeonAtcwold.Rows[0]["SizeName"].ToString().Trim();
                    string atcwordSizeCode = sizeonAtcwold.Rows[0]["SizeCode"].ToString().Trim();
                    string atcwordAtcId = sizeonAtcwold.Rows[0]["AtcId"].ToString().Trim();
                    string atcwordOrderof = sizeonAtcwold.Rows[0]["Orderof"].ToString().Trim();

                    if (newresult.Rows.Count != 0)
                    {
                     if(ourstyleid== atcwordourstyleid && OurStyle== atcwordOurStyle && SizeName == atcwordSizeName && SizeCode == atcwordSizeCode && AtcId == atcwordAtcId && Orderof == atcwordOrderof)
                        {
                            using (AtcWorldEntities enty = new ArtWebApp.DataModelAtcWorld.AtcWorldEntities())
                            {
                                var q = from stymstr in enty.StyleSizeMasters
                                        where stymstr.StyleSizeID == stylesizeid
                                        select stymstr;
                                foreach(var element in q)
                                {
                                    element.OurStyle = OurStyle.ToString();
                                    element.OurStyleID = int.Parse(ourstyleid.ToString());
                                 

                                    element.SizeName = SizeName;
                                    element.SizeCode = SizeCode;
                                    element.AtcId = int.Parse(AtcId.ToString());
                                    element.Orderof = int.Parse(Orderof.ToString());
                                }

                                enty.SaveChanges();
                            }

                            }
                        else
                        {

                        }
                    }
                }
                catch (Exception)
                {
                    using (AtcWorldEntities enty = new ArtWebApp.DataModelAtcWorld.AtcWorldEntities())
                    {
                        if (!enty.StyleSizeMasters.Any(f => f.StyleSizeID == stylesizeid))
                        {
                            StyleSizeMaster stsz = new StyleSizeMaster();
                            stsz.OurStyleID = int.Parse(ourstyleid.ToString());
                            stsz.OurStyle = OurStyle.ToString();
                            stsz.StyleSizeID = stylesizeid;


                            stsz.SizeName = SizeName;
                            stsz.SizeCode = SizeCode;
                            stsz.AtcId = int.Parse(AtcId.ToString());
                            stsz.Orderof = int.Parse(Orderof.ToString());
                            enty.StyleSizeMasters.Add(stsz);
                           
                        }
                        else
                        {
                          
                        }


                        enty.SaveChanges();
                    }

                }
            }

        }



        public DataTable GetAtcWorldData()
        {


            DateTime today = DateTime.Now.Date;
            DataTable datafromart = new DataTable();

            String Datepend = "";
                     
                String q3 = @"SELECT   StyleSizeMasterID,StyleSizeID, AtcId, OurStyle, OurStyleID, SizeName, SizeCode, isnull(Orderof, 0)  as Orderof
FROM StyleSizeMaster";



                //  cmd.CommandText = Query1;
     
            



           
            return ReturnQueryResultDatatablefromAtcWorld(q3);
        }




        public DataTable GetArtData()
        {


            DateTime today = DateTime.Now.Date;
            DataTable datafromart = new DataTable();

          
          
              
              


                String q3 = @"SELECT        StyleSizeID, AtcId, OurStyle, OurStyleID, SizeName, SizeCode, isnull(Orderof, 0) as Orderof
FROM StyleSize";



                //  cmd.CommandText = Query1;
             

        return ReturnQueryResultDatatablefromArt(q3);



            
         
        }



        public  DataTable ReturnQueryResultDatatablefromArt(String Qry)
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ArtConnectionString"].ConnectionString.ToString()))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = Qry;
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = con;
                    con.Open();

                    SqlDataReader rdr = cmd.ExecuteReader();

                    dt.Load(rdr);
                }
            }

            return dt;
        }

        public DataTable ReturnQueryResultDatatablefromAtcWorld(String Qry)
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AtcWorldConnectionString"].ConnectionString.ToString()))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = Qry;
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = con;
                    con.Open();

                    SqlDataReader rdr = cmd.ExecuteReader();

                    dt.Load(rdr);
                }
            }

            return dt;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArtWebApp.Sampling
{
    public partial class SampleWithoutATC : System.Web.UI.Page
    {
        string strconstring = System.Configuration.ConfigurationManager.ConnectionStrings["ArtConnectionString"].ConnectionString.ToString();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {


            }

        }







        protected void Button1_Click(object sender, EventArgs e)
        {

            Insertcuttingtickert();

        }




        public void Insertcuttingtickert()
        {
            using (SqlConnection con = new SqlConnection(strconstring))
            {
                con.Open();

                string query1 = @"insert into CuttingDetails(ATCNum,Buyer,Buyer_style
            ,Movex_style,Fabric_type,width,Description,Merchandiser,Designer,
original_sample,spec,style_diagram,pattern,PocketIn,InterLinin,MainLable,CareLable,
SizeLable,Joker,TapeLable,Thread,Button,Zippernick,Lined,Others,ButtonHoles,
WashingInstruction,size1,size2,size3,size4,qty1,qty2,qty3,qty4,Total,development,
Proto,firstsample,secondsample,thirdsample,sizeset,styling,mtl,design,costing,
booking,master)values(@param1,@param2,@param3,@param4,@param5,@param6,@param7,
@param8,@param9,@param10,@param11,@param12,@param13,@param14,@param15,@param16,
@param17,@param18,@param19,@param20,@param21,@param22,@param23,@param24,@param25,@param26,@param27,@param28,@param29,@param30,@param31,@param32,
@param33,@param34,@param35,@param36,@param37,@param38,@param39,@param40,@param41,@param42,@param43,@param44,@param45,@param46,@param47,@param48);SELECT @@IDENTITY";


                SqlCommand cmd1 = new SqlCommand(query1, con);

                cmd1.Parameters.AddWithValue("@param1", txt_atcnum.Text.ToString());
                cmd1.Parameters.AddWithValue("@param2", txt_buyer.Text.ToString());
                cmd1.Parameters.AddWithValue("@param3", txt_buyerstyle.Text.ToString());
                cmd1.Parameters.AddWithValue("@param4", txt_movexstyle.Text);
                cmd1.Parameters.AddWithValue("@param5", txt_fabric.Text);
                cmd1.Parameters.AddWithValue("@param6", txt_width.Text);
                cmd1.Parameters.AddWithValue("@param7", txt_description.Text);
                cmd1.Parameters.AddWithValue("@param8", drp_merchandiser.Text.ToString());

                cmd1.Parameters.AddWithValue("@param9", drp_designer.Text.ToString());
                cmd1.Parameters.AddWithValue("@param10", drp_originalsample.Text.ToString());
                cmd1.Parameters.AddWithValue("@param11", drp_spec.Text.ToString());
                cmd1.Parameters.AddWithValue("@param12", drp_stylediagram.Text.ToString());

                cmd1.Parameters.AddWithValue("@param13", drp_pattern.Text.ToString());



                cmd1.Parameters.AddWithValue("@param14", drp_pocketin.Text.ToString().Trim());

                cmd1.Parameters.AddWithValue("@param15", drp_interlinin.Text.ToString().Trim());
                cmd1.Parameters.AddWithValue("@param16", drp_mainlable.Text.ToString().Trim());
                cmd1.Parameters.AddWithValue("@param17", drp_carelable.Text.ToString().Trim());
                cmd1.Parameters.AddWithValue("@param18", drp_sizelable.Text.ToString().Trim());
                cmd1.Parameters.AddWithValue("@param19", drp_joker.Text.ToString().Trim());
                cmd1.Parameters.AddWithValue("@param20", drp_tapelable.Text.ToString().Trim());
                cmd1.Parameters.AddWithValue("@param21", drp_thread.Text.ToString());
                cmd1.Parameters.AddWithValue("@param22", drp_button.Text.ToString().Trim());
                cmd1.Parameters.AddWithValue("@param23", drp_zippernick.Text.ToString().Trim());
                cmd1.Parameters.AddWithValue("@param24", drp_lined.Text.ToString().Trim());
                cmd1.Parameters.AddWithValue("@param25", drp_others.Text.ToString().Trim());

                cmd1.Parameters.AddWithValue("@param26", drp_buttonholes.Text.ToString().Trim());

                cmd1.Parameters.AddWithValue("@param27", txt_washinginstruction.Text.Trim());

                cmd1.Parameters.AddWithValue("@param28", txt_size1.Text.Trim());
                cmd1.Parameters.AddWithValue("@param29", txt_size2.Text.Trim());
                cmd1.Parameters.AddWithValue("@param30", txt_size3.Text.Trim());
                cmd1.Parameters.AddWithValue("@param31", txt_size4.Text.Trim());

                cmd1.Parameters.AddWithValue("@param32", txt_qty1.Text.Trim());
                cmd1.Parameters.AddWithValue("@param33", txt_qty2.Text.Trim());
                cmd1.Parameters.AddWithValue("@param34", txt_qty3.Text.Trim());
                cmd1.Parameters.AddWithValue("@param35", txt_qty4.Text.Trim());
                cmd1.Parameters.AddWithValue("@param36", txt_total.Text.Trim());
                cmd1.Parameters.AddWithValue("@param48", txt_master.Text.Trim());


                if (chk_development.Checked == true)
                {

                    cmd1.Parameters.AddWithValue("@param37", true);



                }
                else
                {

                    cmd1.Parameters.AddWithValue("@param37", false);

                }

                if (chk_proto.Checked == true)
                {

                    cmd1.Parameters.AddWithValue("@param38", true);

                }
                else
                {

                    cmd1.Parameters.AddWithValue("@param38", false);

                }




                if (chk_1stfitsample.Checked == true)
                {

                    cmd1.Parameters.AddWithValue("@param39", true);

                }
                else
                {

                    cmd1.Parameters.AddWithValue("@param39", false);

                }



                if (chk_2ndfitsample.Checked == true)
                {

                    cmd1.Parameters.AddWithValue("@param40", true);

                }
                else
                {

                    cmd1.Parameters.AddWithValue("@param40", false);

                }




                if (chk_3rdfitsample.Checked == true)
                {

                    cmd1.Parameters.AddWithValue("@param41", true);

                }
                else
                {

                    cmd1.Parameters.AddWithValue("@param41", false);

                }



                if (chk_sizeset.Checked == true)
                {

                    cmd1.Parameters.AddWithValue("@param42", true);

                }
                else
                {

                    cmd1.Parameters.AddWithValue("@param42", false);

                }


                if (chk_styling.Checked == true)
                {

                    cmd1.Parameters.AddWithValue("@param43", true);

                }
                else
                {

                    cmd1.Parameters.AddWithValue("@param43", false);

                }




                if (chk_mtl.Checked == true)
                {

                    cmd1.Parameters.AddWithValue("@param44", true);

                }
                else
                {

                    cmd1.Parameters.AddWithValue("@param44", false);

                }



                if (chk_design.Checked == true)
                {

                    cmd1.Parameters.AddWithValue("@param45", true);

                }
                else
                {

                    cmd1.Parameters.AddWithValue("@param45", false);

                }


                if (chk_costing.Checked == true)
                {

                    cmd1.Parameters.AddWithValue("@param46", true);

                }
                else
                {

                    cmd1.Parameters.AddWithValue("@param46", false);

                }

                if (chk_booking.Checked == true)
                {

                    cmd1.Parameters.AddWithValue("@param47", true);

                }
                else
                {

                    cmd1.Parameters.AddWithValue("@param47", false);

                }






                int result = int.Parse(cmd1.ExecuteScalar().ToString());

                String msg = "";



                if (result > 0)
                {
                    msg = "Cut details Updated Successsfully Against Cutid " + result.ToString();
                    ArtWebApp.Controls.Messagebox.MessgeboxUpdate(Messaediv, "sucess", msg);

                    Session["ID"] = result.ToString();


                }
                else
                {
                    msg = "Record not Updated successfully";
                    ArtWebApp.Controls.Messagebox.MessgeboxUpdate(Messaediv, "sucess", msg);


                }




                con.Close();
            }
        }





        public void caltotalqty()
        {

            int qty1, qty2, qty3, qty4, total;

            qty1 = Convert.ToInt32(txt_qty1.Text.ToString());
            qty2 = Convert.ToInt32(txt_qty2.Text.ToString());
            qty3 = Convert.ToInt32(txt_qty3.Text.ToString());
            qty4 = Convert.ToInt32(txt_qty4.Text.ToString());

            total = qty1 + qty2 + qty3 + qty4;

            txt_total.Text = total.ToString();


        }




        protected void txt_qty1_TextChanged(object sender, EventArgs e)
        {
            caltotalqty();

        }
        protected void txt_qty2_TextChanged(object sender, EventArgs e)
        {
            caltotalqty();
        }
        protected void txt_qty3_TextChanged(object sender, EventArgs e)
        {
            caltotalqty();
        }
        protected void txt_qty4_TextChanged(object sender, EventArgs e)
        {
            caltotalqty();
        }

        protected void btn_print_Click(object sender, EventArgs e)
        {
            Response.Redirect(@"\Reports\General Reports\GeneralReportform.aspx?navtype=CuttingTicket&&ID=" + Session["ID"].ToString().Trim());
        }
    }

}
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
    public partial class CuttingTicketEdit : System.Web.UI.Page
    {
        string strconstring = System.Configuration.ConfigurationManager.ConnectionStrings["ArtConnectionString"].ConnectionString.ToString();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {

                fillcutid();
            }

        }


        public void fillcutid()
        {

            SqlConnection con = new SqlConnection(strconstring);
            con.Open();

            SqlCommand cmd = new SqlCommand("Select CutID from CuttingDetails", con);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {

                drp_fillcutid.DataSource = dt;
                drp_fillcutid.DataTextField = "CutID";
                drp_fillcutid.DataValueField = "CutID";

                drp_fillcutid.DataBind();



            }

        }




       


   


        public void Updatecuttingticket()
        {
            int  cutid = int.Parse (drp_fillcutid.SelectedValue.  ToString ());
            using (SqlConnection con = new SqlConnection(strconstring))
            {
                con.Open();

                SqlCommand cmd1 = new SqlCommand("update  CuttingDetails set ATCNum=@ATCNum,Buyer=@Buyer,Buyer_style=@Buyer_style,Movex_style=@Movex_style,development=@development,Proto=@Proto,firstsample=@firstsample,secondsample=@secondsample,thirdsample=@thirdsample,sizeset=@sizeset,styling=@styling,mtl=@mtl,design=@design,costing=@costing,booking=@booking,Fabric_type=@Fabric_type,width=@width,Description=@Description,Merchandiser=@Merchandiser,Designer=@Designer,original_sample=@original_sample,spec=@spec,style_diagram=@style_diagram,pattern=@pattern,size1=@size1,size2=@size2,size3=@size3,size4=@size4,qty1=@qty1,qty2=@qty2,qty3=@qty3,qty4=@qty4,Total=@Total,PocketIn=@PocketIn,InterLinin=@InterLinin,MainLable=@MainLable,CareLable=@CareLable,SizeLable=@SizeLable,Joker=@Joker,TapeLable=@TapeLable,Thread=@Thread,Button=@Button,Zippernick=@Zippernick,Lined=@Lined,Others=@Others,ButtonHoles=@ButtonHoles,WashingInstruction=@WashingInstruction,master=@master where CutID=" + cutid, con);

                cmd1.Parameters.AddWithValue("@ATCNum", drp_atcno.Text.ToString());
                cmd1.Parameters.AddWithValue("@Buyer", drp_buyer.Text.ToString());
                cmd1.Parameters.AddWithValue("@Buyer_style", drp_buyerstyle.Text.ToString());
                cmd1.Parameters.AddWithValue("@Movex_style", txt_movexstyle.Text.ToString());
                cmd1.Parameters.AddWithValue("@Fabric_type", txt_fabric.Text);
                cmd1.Parameters.AddWithValue("@width", txt_width.Text);
                cmd1.Parameters.AddWithValue("@Description", txt_description.Text);
                cmd1.Parameters.AddWithValue("@Merchandiser", drp_merchandiser.Text.ToString());

                cmd1.Parameters.AddWithValue("@Designer", drp_designer.Text.ToString());
                cmd1.Parameters.AddWithValue("@original_sample", drp_originalsample.Text.ToString());
                cmd1.Parameters.AddWithValue("@spec", drp_spec.Text.ToString());
                cmd1.Parameters.AddWithValue("@style_diagram", drp_stylediagram.Text.ToString());

                cmd1.Parameters.AddWithValue("@pattern", drp_pattern.Text.ToString());



                cmd1.Parameters.AddWithValue("@PocketIn", drp_pocketin.Text.ToString().Trim());

                cmd1.Parameters.AddWithValue("@InterLinin", drp_interlinin.Text.ToString().Trim());
                cmd1.Parameters.AddWithValue("@MainLable", drp_mainlable.Text.ToString().Trim());
                cmd1.Parameters.AddWithValue("@CareLable", drp_carelable.Text.ToString().Trim());
                cmd1.Parameters.AddWithValue("@SizeLable", drp_sizelable.Text.ToString().Trim());
                cmd1.Parameters.AddWithValue("@Joker", drp_joker.Text.ToString().Trim());
                cmd1.Parameters.AddWithValue("@TapeLable", drp_tapelable.Text.ToString().Trim());
                cmd1.Parameters.AddWithValue("@Thread", drp_thread.Text.ToString());
                cmd1.Parameters.AddWithValue("@Button", drp_button.Text.ToString().Trim());
                cmd1.Parameters.AddWithValue("@Zippernick", drp_zippernick.Text.ToString().Trim());
                cmd1.Parameters.AddWithValue("@Lined", drp_lined.Text.ToString().Trim());
                cmd1.Parameters.AddWithValue("@Others", drp_others.Text.ToString().Trim());

                cmd1.Parameters.AddWithValue("@ButtonHoles", drp_buttonholes.Text.ToString().Trim());

                cmd1.Parameters.AddWithValue("@WashingInstruction", txt_washinginstruction.Text.Trim());

                cmd1.Parameters.AddWithValue("@size1", txt_size1.Text.Trim());
                cmd1.Parameters.AddWithValue("@size2", txt_size2.Text.Trim());
                cmd1.Parameters.AddWithValue("@size3", txt_size3.Text.Trim());
                cmd1.Parameters.AddWithValue("@size4", txt_size4.Text.Trim());

                cmd1.Parameters.AddWithValue("@qty1", txt_qty1.Text.Trim());
                cmd1.Parameters.AddWithValue("@qty2", txt_qty2.Text.Trim());
                cmd1.Parameters.AddWithValue("@qty3", txt_qty3.Text.Trim());
                cmd1.Parameters.AddWithValue("@qty4", txt_qty4.Text.Trim());
                cmd1.Parameters.AddWithValue("@Total", txt_total.Text.Trim());
                cmd1.Parameters.AddWithValue("@master", txt_master.Text.Trim());


                if (chk_development.Checked == true)
                {
                    cmd1.Parameters.AddWithValue("@development", true);
                }
                else
                {
                    cmd1.Parameters.AddWithValue("@development", false);
                }

                if (chk_proto.Checked == true)
                {
                    cmd1.Parameters.AddWithValue("@Proto", true);
                }
                else
                {
                    cmd1.Parameters.AddWithValue("@Proto", false);
                }

                if (chk_1stfitsample.Checked == true)
                {
                    cmd1.Parameters.AddWithValue("@firstsample", true);
                }
                else
                {
                    cmd1.Parameters.AddWithValue("@firstsample", false);
                }

                if (chk_2ndfitsample.Checked == true)
                {
                    cmd1.Parameters.AddWithValue("@secondsample", true);
                }
                else
                {
                    cmd1.Parameters.AddWithValue("@secondsample", false);
                }

                if (chk_3rdfitsample.Checked == true)
                {
                    cmd1.Parameters.AddWithValue("@thirdsample", true);
                }
                else
                {
                    cmd1.Parameters.AddWithValue("@thirdsample", false);
                }

                if (chk_sizeset.Checked == true)
                {
                    cmd1.Parameters.AddWithValue("@sizeset", true);
                }
                else
                {

                    cmd1.Parameters.AddWithValue("@sizeset", false);

                }


                if (chk_styling.Checked == true)
                {

                    cmd1.Parameters.AddWithValue("@styling", true);

                }
                else
                {

                    cmd1.Parameters.AddWithValue("@styling", false);

                }




                if (chk_mtl.Checked == true)
                {

                    cmd1.Parameters.AddWithValue("@mtl", true);

                }
                else
                {

                    cmd1.Parameters.AddWithValue("@mtl", false);

                }



                if (chk_design.Checked == true)
                {

                    cmd1.Parameters.AddWithValue("@design", true);

                }
                else
                {

                    cmd1.Parameters.AddWithValue("@design", false);

                }


                if (chk_costing.Checked == true)
                {

                    cmd1.Parameters.AddWithValue("@costing", true);

                }
                else
                {

                    cmd1.Parameters.AddWithValue("@costing", false);

                }

                if (chk_booking.Checked == true)
                {

                    cmd1.Parameters.AddWithValue("@booking", true);

                }
                else
                {

                    cmd1.Parameters.AddWithValue("@booking", false);

                }






                int result = cmd1.ExecuteNonQuery();





                String msg = "";


                if (result > 0)
                {
                    msg = "Cut details Updated Successsfully Against Cutid ";
                    ArtWebApp.Controls.Messagebox.MessgeboxUpdate(Messaediv, "sucess", msg);




                }
                else
                {
                    msg = "Record not Updated successfully";
                    ArtWebApp.Controls.Messagebox.MessgeboxUpdate(Messaediv, "sucess", msg);


                }



                con.Close();
            }
        }



        public void fillcontrols()
        {

            string cutid = drp_fillcutid.SelectedValue;




            SqlConnection con = new SqlConnection(strconstring);
            con.Open();

            SqlCommand cmd = new SqlCommand("Select * from CuttingDetails where CutID=" + cutid, con);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            System.Data.DataTable dt = new System.Data.DataTable();

            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {


                drp_atcno.SelectedItem.Text = dt.Rows[0]["ATCNum"].ToString();
                drp_buyer.SelectedItem.Text = dt.Rows[0]["Buyer"].ToString();

                drp_buyerstyle.SelectedItem.Text = dt.Rows[0]["Buyer_style"].ToString();

                //txt_movexstyle.Text = dt.Rows[0]["Movex_style"].ToString();
                txt_fabric.Text = dt.Rows[0]["Fabric_type"].ToString();
                txt_width.Text = dt.Rows[0]["width"].ToString();
                txt_description.Text = dt.Rows[0]["Description"].ToString();
                drp_merchandiser.SelectedItem.Text = dt.Rows[0]["Merchandiser"].ToString();
                drp_designer.SelectedItem.Text = dt.Rows[0]["Designer"].ToString();

                drp_originalsample.SelectedItem.Text = dt.Rows[0]["original_sample"].ToString();
                drp_spec.SelectedItem.Text = dt.Rows[0]["spec"].ToString();

                drp_stylediagram.SelectedItem.Text = dt.Rows[0]["style_diagram"].ToString();

                drp_pattern.SelectedItem.Text = dt.Rows[0]["pattern"].ToString();



                txt_size1.Text = dt.Rows[0]["size1"].ToString();

                txt_size2.Text = dt.Rows[0]["size2"].ToString();
                txt_size3.Text = dt.Rows[0]["size3"].ToString();
                txt_size4.Text = dt.Rows[0]["size4"].ToString();

                txt_qty1.Text = dt.Rows[0]["qty1"].ToString();

                txt_qty2.Text = dt.Rows[0]["qty2"].ToString();
                txt_qty3.Text = dt.Rows[0]["qty3"].ToString();
                txt_qty4.Text = dt.Rows[0]["qty4"].ToString();
                txt_total.Text = dt.Rows[0]["Total"].ToString();

                drp_pocketin.SelectedItem.Text = dt.Rows[0]["PocketIn"].ToString();

                drp_interlinin.SelectedItem.Text = dt.Rows[0]["InterLinin"].ToString();

                drp_mainlable.SelectedItem.Text = dt.Rows[0]["MainLable"].ToString();
                drp_carelable.SelectedItem.Text = dt.Rows[0]["CareLable"].ToString();
                drp_sizelable.SelectedItem.Text = dt.Rows[0]["SizeLable"].ToString();
                drp_tapelable.SelectedItem.Text = dt.Rows[0]["TapeLable"].ToString();
                drp_button.SelectedItem.Text = dt.Rows[0]["Button"].ToString();
                drp_zippernick.SelectedItem.Text = dt.Rows[0]["Zippernick"].ToString();
                drp_lined.SelectedItem.Text = dt.Rows[0]["Lined"].ToString();
                drp_others.SelectedItem.Text = dt.Rows[0]["Others"].ToString();
                drp_buttonholes.SelectedItem.Text = dt.Rows[0]["ButtonHoles"].ToString();
                drp_thread.SelectedItem.Text = dt.Rows[0]["Thread"].ToString();

                txt_washinginstruction.Text = dt.Rows[0]["WashingInstruction"].ToString();

                if (dt.Rows[0]["development"].ToString() == "0")
                {

                    chk_development.Checked = true;

                }
                else
                {

                    chk_development.Checked = false;

                }







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
        protected void drp_fillcutid_SelectedIndexChanged(object sender, EventArgs e)
        {

            fillcontrols();


        }
        protected void btn_update_Click(object sender, EventArgs e)
        {


            Updatecuttingticket();
        }

        protected void btn_print_Click(object sender, EventArgs e)
        {
            Response.Redirect(@"\Reports\General Reports\GeneralReportform.aspx?navtype=CuttingTicket&&ID=" + Session["ID"].ToString().Trim());
        }
    }

}
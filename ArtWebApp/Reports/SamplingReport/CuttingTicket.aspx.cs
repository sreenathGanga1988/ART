using ArtWebApp.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArtWebApp.Reports.SamplingReport
{
    public partial class CuttingTicket : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {


            String costpk = Request.QueryString["ID"];
            int cutid = int.Parse(costpk);

            getcuttingticketdetails(cutid);




        }


        public void getcuttingticketdetails(int cutid)
        {
            using (ArtEntitiesnew entty = new ArtEntitiesnew())
            {
                var q = from skudet in entty.CuttingDetails
                        where skudet.CutID == cutid
                        select skudet;
                foreach (var element in q)
                {





                    lbl_buttons.Text = element.Button;
                    lbl_width.Text = element.width;
                    lbl_description.Text = element.Description;
                    lbl_merch.Text = element.Merchandiser;
                    lbl_master.Text = element.master;
                    lbl_originalsample.Text = element.original_sample;
                    lbl_styledaigram.Text = element.style_diagram;
                    lbl_spec.Text = element.spec;
                    lbl_pattern.Text = element.pattern;
                    lbl_size1.Text = element.size1;
                    lbl_size2.Text = element.size2;
                    lbl_size3.Text = element.size3;
                    lbl_size4.Text = element.size4;
                    lbl_totalqty.Text = element.Total;
                    lbl_pocketing.Text = element.PocketIn;
                    lbl_interlinin.Text = element.InterLinin;
                    lbl_mainlable.Text =  (element.MainLable.ToString ().Trim()=="Select")? "": element.MainLable;
                    lbl_carelable.Text = (element.CareLable.ToString().Trim() == "Select") ? "" : element.CareLable;
                    lbl_sizelable.Text =  (element.SizeLable.ToString().Trim() == "Select") ? "" : element.SizeLable;
                    lbl_joker.Text =(element.Joker.ToString().Trim() == "Select") ? "" : element.Joker;
                    lbl_tapelable.Text =  (element.TapeLable.ToString().Trim() == "Select") ? "" : element.TapeLable;
                    lbl_thread.Text =  (element.Thread.ToString().Trim() == "Select") ? "" : element.Thread;
                    lbl_zippernickel.Text = (element.Zippernick.ToString().Trim() == "Select") ? "" : element.Zippernick;
                    lbl_lined.Text = (element.Lined.ToString().Trim() == "Select") ? "" : element.Lined;
                    lbl_buttonholes.Text =  (element.ButtonHoles.ToString().Trim() == "Select") ? "" : element.ButtonHoles;
                    lbl_washinginstruction.Text =  (element.WashingInstruction.ToString().Trim() == "Select") ? "" : element.WashingInstruction;
                    lbl_totalqty.Text =  (element.Total.ToString().Trim() == "Select") ? "" : element.Total;



                    if (element.development.ToString() == "1")
                    {
                        chk_development.Checked = true;
                    }
                    else
                    {
                        chk_development.Checked = false;
                    }




                    if (element.firstsample.ToString() == "1")
                    {
                        chk_1stfitsample.Checked = true;
                    }
                    else
                    {
                        chk_1stfitsample.Checked = false;
                    }


                    if (element.secondsample.ToString() == "1")
                    {
                        chk_2ndfitsample.Checked = true;
                    }
                    else
                    {
                        chk_2ndfitsample.Checked = false;
                    }




                    if (element.thirdsample.ToString() == "1")
                    {
                        chk_3rdfitsample.Checked = true;
                    }
                    else
                    {
                        chk_3rdfitsample.Checked = false;
                    }




                    if (element.Proto.ToString() == "1")
                    {
                        chk_proto.Checked = true;
                    }
                    else
                    {
                        chk_proto.Checked = false;
                    }


                    if (element.sizeset.ToString() == "1")
                    {
                        chk_sizeset.Checked = true;
                    }
                    else
                    {
                        chk_sizeset.Checked = false;
                    }


                    //if (element.PPSample.ToString() == "1")
                    //{
                    //    chk_ppsample.Checked = true;
                    //}
                    //else
                    //{
                    //    chk_ppsample.Checked = false;
                    //}



                    if (element.PhotoSample.ToString() == "1")
                    {
                        chk_photosample.Checked = true;
                    }
                    else
                    {
                        chk_photosample.Checked = false;
                    }



                    if (element.styling.ToString() == "1")
                    {
                        chk_styling.Checked = true;
                    }
                    else
                    {
                        chk_styling.Checked = false;
                    }



                    if (element.mtl.ToString() == "1")
                    {
                        chk_mtl.Checked = true;
                    }
                    else
                    {
                        chk_mtl.Checked = false;
                    }


                    if (element.costing.ToString() == "1")
                    {
                        chk_costing.Checked = true;
                    }
                    else
                    {
                        chk_costing.Checked = false;
                    }


                    if (element.booking.ToString() == "1")
                    {
                        chk_booking.Checked = true;
                    }
                    else
                    {
                        chk_booking.Checked = false;
                    }


                    //if(element.development.ToString ()=="1")
                    //{
                    //    chk_development.Checked = true;
                    //}
                    //else
                    //{
                    //    chk_development.Checked = false;
                    //}
                    //chk_1stfitsample.Text = element.firstsample;
                    //chk_2ndfitsample.Text = element.secondsample;
                    //chk_3rdfitsample.Text = element.thirdsample;
                    //chk_proto.Text = element.Proto;
                    //chk_sizeset.Text = element.sizeset;
                    //chk_ppsample.Text = element.PPSample;
                    //chk_photosample.Text = element.PhotoSample;
                    //chk_styling.Text = element.styling;
                    //chk_mtl.Text = element.mtl;
                    //chk_costing.Text = element.costing;
                    //chk_booking.Text = element.booking;
                   



















                }

            }
        }


    }
}
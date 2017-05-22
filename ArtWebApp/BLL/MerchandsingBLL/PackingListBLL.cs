using ArtWebApp.DataModelAtcWorld;
using ArtWebApp.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArtWebApp.BLL.MerchandsingBLL
{
    public class PackingListMasterBLL
    {

        public int PackingMaster_PK { get; set; }
        public int Atc_ID { get; set; }
        public int NoofCTN { get; set; }

        public int PCPerCtn { get; set; }
        public string CtnDimension { get; set; }
        public string AddedBy { get; set; }
        public DateTime AddedDate { get; set; }
        public string IsConfirmed { get; set; }

        public decimal PcPerPolybag { get; set; }
        public decimal Length { get; set; }
        public decimal Width { get; set; }
        public decimal Height { get; set; }
        public decimal NetWeight { get; set; }
        public decimal NNWeight { get; set; }
        public decimal Grossweight { get; set; }
        public string WeightUOM { get; set; }
        public string CtnUOM { get; set; }

        public string PackingInstruction { get; set; }
    
        public List<PackingListdetailDataBLL> PackingListdetailDataDataCollection { get; set; }


        public String CreateAtcnum(int atcid)
        {
            String popacknum = "";
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {
                var count = (from o in enty.PackingListMasters
                             where o.Atc_ID == atcid

                             select o).Count();

                //from buyer master u where u.buyerid = buyerid select prefix
                var atcnum = enty.AtcMasters.Where(u => u.AtcId == atcid).Select(u => u.AtcNum).FirstOrDefault();

                popacknum = "PL" + atcnum.ToString().Trim() + "-" + count.ToString();

            }

            return popacknum;
        }


        public string insertPackinglistMaster()
        {
            string asqshuffle = "";

            int newpackpk = 0;

            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {
                PackingListMaster pkmstr = new PackingListMaster();

                pkmstr.Atc_ID = this.Atc_ID;
                pkmstr.NoofCTN = this.NoofCTN;
                pkmstr.PCPerCtn = this.PCPerCtn;
                pkmstr.CtnDimension = this.CtnDimension;

                pkmstr.IsConfirmed = "N";
                pkmstr.AddedBy = HttpContext.Current.Session["Username"].ToString();
                pkmstr.AddedDate = DateTime.Now;
                pkmstr.Remark = this.PackingInstruction;
                pkmstr.PcPerPolybag = this.PcPerPolybag;






                pkmstr.Length = this.Length;
                pkmstr.Width = this.Width;
                pkmstr.Height = this.Height;
                pkmstr.NetWeight = this.NetWeight;
                pkmstr.NNWeight = this.NNWeight;
                pkmstr.Grossweight = this.Grossweight;
                pkmstr.WeightUOM = this.WeightUOM;
                pkmstr.CtnUOM = this.CtnUOM;






                enty.PackingListMasters.Add(pkmstr);

                enty.SaveChanges();

                asqshuffle = pkmstr.PackListNum = CreateAtcnum(this.Atc_ID);

                using (DataModelAtcWorld.AtcWorldEntities enttty = new DataModelAtcWorld.AtcWorldEntities())
                {

                    PackingListMasterAtcPro atcpro = new DataModelAtcWorld.PackingListMasterAtcPro();

                    atcpro.Atc_ID = this.Atc_ID;
                    atcpro.NoofCTN = this.NoofCTN;
                    atcpro.PCPerCtn = this.PCPerCtn;
                    atcpro.CtnDimension = this.CtnDimension;

                    atcpro.IsConfirmed = "N";
                    atcpro.AddedBy = HttpContext.Current.Session["Username"].ToString();
                    atcpro.AddedDate = DateTime.Now;
                    atcpro.PackListNum = asqshuffle;


                    atcpro.Length = this.Length;
                    atcpro.Width = this.Width;
                    atcpro.Height = this.Height;
                    atcpro.NetWeight = this.NetWeight;
                    atcpro.NNWeight = this.NNWeight;
                    atcpro.Grossweight = this.Grossweight;
                    atcpro.WeightUOM = this.WeightUOM;
                    atcpro.CtnUOM = this.CtnUOM;

                    enttty.PackingListMasterAtcProes.Add(atcpro);
                    enttty.SaveChanges();

                    newpackpk = int.Parse(atcpro.PackingMaster_PK.ToString());
                }





                foreach (PackingListdetailDataBLL rdet in this.PackingListdetailDataDataCollection)
                {


                    Decimal totalpc = Decimal.Parse(rdet.TotalQty.ToString()) * Decimal.Parse(this.NoofCTN.ToString());


                    PackingListDetail pkdet = new DataModels.PackingListDetail();

                    pkdet.PackingMaster_PK = pkmstr.PackingMaster_PK;
                    pkdet.POPackId = rdet.POPackId;
                    pkdet.OurStyleID = rdet.OurStyleID;
                    pkdet.ColorName = rdet.ColorName;
                    pkdet.SizeName = rdet.SizeName;
                    pkdet.PcperCtn = rdet.TotalQty;
                    pkdet.TotalQty = totalpc;

                    var popackdetialpk = enty.POPackDetails.Where(u => u.ColorName == rdet.ColorName && u.SizeName == rdet.SizeName && u.POPackId == rdet.POPackId && u.OurStyleID == rdet.OurStyleID).Select(u => u.PoPack_Detail_PK).FirstOrDefault();

                    pkdet.PoPack_Detail_PK = int.Parse(popackdetialpk.ToString());
                    enty.PackingListDetails.Add(pkdet);

                    using (DataModelAtcWorld.AtcWorldEntities enttty = new DataModelAtcWorld.AtcWorldEntities())
                    {
                        PackingListDetailsAtc Propkdet1 = new PackingListDetailsAtc();


                        Propkdet1.PackingMaster_PK = newpackpk;
                        Propkdet1.POPackId = rdet.POPackId;
                        Propkdet1.OurStyleID = rdet.OurStyleID;
                        Propkdet1.ColorName = rdet.ColorName;
                        Propkdet1.SizeName = rdet.SizeName;
                        Propkdet1.PcperCtn = rdet.TotalQty;
                        Propkdet1.TotalQty = totalpc;
                        Propkdet1.PoPack_Detail_PK = int.Parse(popackdetialpk.ToString());
                        enttty.PackingListDetailsAtcs.Add(Propkdet1);
                        enttty.SaveChanges();
                    }

                }


                enty.SaveChanges();

            }

            return asqshuffle;


        }




        public string insertPackinglistMasterforSolidColor()
        {
            string asqshuffle = "";

            int newpackpk = 0;

            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {


                using (DataModelAtcWorld.AtcWorldEntities enttty = new DataModelAtcWorld.AtcWorldEntities())
                {

                    foreach (PackingListdetailDataBLL rdet in this.PackingListdetailDataDataCollection)
                    {



                        PackingListMaster pkmstr = new PackingListMaster();

                        pkmstr.Atc_ID = this.Atc_ID;
                        pkmstr.NoofCTN = rdet.NoofCTN;
                        pkmstr.PCPerCtn = rdet.PcperCtn;
                        pkmstr.CtnDimension = this.CtnDimension;

                        pkmstr.IsConfirmed = "N";
                        pkmstr.AddedBy = HttpContext.Current.Session["Username"].ToString();
                        pkmstr.AddedDate = DateTime.Now;
                        pkmstr.Remark = this.PackingInstruction;
                        pkmstr.PcPerPolybag = this.PcPerPolybag;
                        enty.PackingListMasters.Add(pkmstr);

                        enty.SaveChanges();

                        asqshuffle = pkmstr.PackListNum = CreateAtcnum(this.Atc_ID);

                        PackingListMasterAtcPro atcpro = new DataModelAtcWorld.PackingListMasterAtcPro();

                        atcpro.Atc_ID = this.Atc_ID;
                        pkmstr.NoofCTN = rdet.NoofCTN;
                        pkmstr.PCPerCtn = rdet.PcperCtn;
                        atcpro.CtnDimension = this.CtnDimension;

                        atcpro.IsConfirmed = "N";
                        atcpro.AddedBy = HttpContext.Current.Session["Username"].ToString();
                        atcpro.AddedDate = DateTime.Now;
                        atcpro.PackListNum = asqshuffle;
                        enttty.PackingListMasterAtcProes.Add(atcpro);
                        enttty.SaveChanges();

                        newpackpk = int.Parse(atcpro.PackingMaster_PK.ToString());

                        Decimal totalpc = Decimal.Parse(rdet.TotalQty.ToString()) * Decimal.Parse(rdet.NoofCTN.ToString());


                        PackingListDetail pkdet = new DataModels.PackingListDetail();

                        pkdet.PackingMaster_PK = pkmstr.PackingMaster_PK;
                        pkdet.POPackId = rdet.POPackId;
                        pkdet.OurStyleID = rdet.OurStyleID;
                        pkdet.ColorName = rdet.ColorName;
                        pkdet.SizeName = rdet.SizeName;
                        pkdet.PcperCtn = rdet.TotalQty;
                        pkdet.TotalQty = totalpc;

                        var popackdetialpk = enty.POPackDetails.Where(u => u.ColorName == rdet.ColorName && u.SizeName == rdet.SizeName && u.POPackId == rdet.POPackId && u.OurStyleID == rdet.OurStyleID).Select(u => u.PoPack_Detail_PK).FirstOrDefault();

                        pkdet.PoPack_Detail_PK = int.Parse(popackdetialpk.ToString());
                        enty.PackingListDetails.Add(pkdet);

                        PackingListDetailsAtc Propkdet1 = new PackingListDetailsAtc();

                        Propkdet1.PackingMaster_PK = newpackpk;
                        Propkdet1.POPackId = rdet.POPackId;
                        Propkdet1.OurStyleID = rdet.OurStyleID;
                        Propkdet1.ColorName = rdet.ColorName;
                        Propkdet1.SizeName = rdet.SizeName;
                        Propkdet1.PcperCtn = rdet.TotalQty;
                        Propkdet1.TotalQty = totalpc;
                        Propkdet1.PoPack_Detail_PK = int.Parse(popackdetialpk.ToString());
                        enttty.PackingListDetailsAtcs.Add(Propkdet1);
                        enttty.SaveChanges();


                    }


                    enty.SaveChanges();

                }

                return asqshuffle;


            }



        }

    }
    public class PackingListdetailDataBLL
    {
     

        public int PackingDetails_PK { get; set; }
        public int PackingMaster_PK { get; set; }
        public int PoPack_Detail_PK { get; set; }
        public int POPackId { get; set; }
        public int OurStyleID { get; set; }
        public string ColorName { get; set; }
        public string SizeName { get; set; }
        public int PcperCtn { get; set; }
            public int NoofCTN { get; set; }
            public int Atcid { get; set; }
        public int TotalQty { get; set; }
    }
}
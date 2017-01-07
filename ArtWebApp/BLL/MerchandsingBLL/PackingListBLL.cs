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
        


        public List<PackingListdetailDataBLL> PackingListdetailDataDataCollection { get; set; }
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

                enty.PackingListMasters.Add(pkmstr);

                enty.SaveChanges();

                asqshuffle = pkmstr.PackListNum = "PL" + pkmstr.PackingMaster_PK.ToString().PadLeft(6, '0');

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
                   enttty.PackingListMasterAtcProes.Add(atcpro);
                    enttty.SaveChanges();

                    newpackpk = int.Parse ( atcpro.PackingMaster_PK.ToString ());
                }





                    foreach (PackingListdetailDataBLL rdet in this.PackingListdetailDataDataCollection)
                    {




                        PackingListDetail pkdet = new DataModels.PackingListDetail();

                    pkdet.PackingMaster_PK = pkmstr.PackingMaster_PK;
                    pkdet.POPackId = rdet.POPackId;
                        pkdet.OurStyleID = rdet.OurStyleID;
                        pkdet.ColorName = rdet.ColorName;
                        pkdet.SizeName = rdet.SizeName;
                        pkdet.PcperCtn = rdet.PcperCtn;
                        pkdet.TotalQty = rdet.TotalQty;

                        var popackdetialpk = enty.POPackDetails.Where(u => u.ColorName == rdet.ColorName && u.SizeName == rdet.SizeName && u.POPackId == rdet.POPackId && u.OurStyleID == rdet.OurStyleID).Select(u => u.PoPack_Detail_PK).FirstOrDefault();

                        pkdet.PoPack_Detail_PK = int.Parse(popackdetialpk.ToString());
                        enty.PackingListDetails.Add(pkdet);

                    using (DataModelAtcWorld.AtcWorldEntities enttty = new DataModelAtcWorld.AtcWorldEntities())
                    {
                       PackingListDetailsAtc   Propkdet1 = new PackingListDetailsAtc();


                        Propkdet1.PackingMaster_PK = newpackpk;
                        Propkdet1.POPackId = rdet.POPackId;
                        Propkdet1.OurStyleID = rdet.OurStyleID;
                        Propkdet1.ColorName = rdet.ColorName;
                        Propkdet1.SizeName = rdet.SizeName;
                        Propkdet1.PcperCtn = rdet.PcperCtn;
                        Propkdet1.TotalQty = rdet.TotalQty;
                        Propkdet1.PoPack_Detail_PK = int.Parse(popackdetialpk.ToString());
                        enttty.PackingListDetailsAtcs.Add(Propkdet1);
                        enttty.SaveChanges();
                            }

                }
          

                enty.SaveChanges();

            }

            return asqshuffle;


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
        public int Atcid { get; set; }
        public int TotalQty { get; set; }
    }
}
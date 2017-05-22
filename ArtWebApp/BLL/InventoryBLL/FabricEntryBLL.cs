using ArtWebApp.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArtWebApp.BLL.InventoryBLL
{
  

    public class FabricEntryData
    {
        public string Merch_Name { get; set; }

        public string Description { get; set; }

        public string Color { get; set; }
        public string Qty { get; set; }
        public string Width { get; set; }

        public string Unit { get; set; }
        public string AWBnum { get; set; }
        public string SuperRef { get; set; }
        public DateTime Date { get; set; }
        public string AwbNum { get; set; }
        public string Weight { get; set; }

        public string Code { get; set; }

        public int Supplier_PK { get; set; }

        public String insertfabricdata()
        {
            String numbertoreturn = "";
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {
                SampleFabricEntryMaster samp = new SampleFabricEntryMaster();
                samp.AwbNum = this.AwbNum;
           
                samp.Color = this.Color;
                samp.Date = this.Date;
                samp.Description = this.Description;
                samp.Merch_Name = this.Merch_Name;
                samp.Qty = this.Qty;
                samp.SuperRef = this.SuperRef;



                enty.SampleFabricEntryMasters.Add(samp);
                enty.SaveChanges();

                var prefix = enty.SamSupplierMasters.Where(u => u.SampSupplierID == this.Supplier_PK).Select(u => u.SamSupCode).FirstOrDefault();

                numbertoreturn = prefix.ToString().Trim() + (samp.SamplingFab_PK.ToString()).ToString();

                samp.Code = numbertoreturn;
                enty.SaveChanges();





            }

            return numbertoreturn;
        }

    }
}
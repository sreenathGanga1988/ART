using ArtWebApp.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArtWebApp.BLL.MerchandsingBLL
{
    public class FreightChargeBLL
    {


        public void InsertFreightcharges(FreightRequestMaster freightRequestMaste)
        {
            using (ArtEntitiesnew entty = new ArtEntitiesnew())
            {
                entty.FreightRequestMasters.Add(freightRequestMaste);


            }
         }


        }
}
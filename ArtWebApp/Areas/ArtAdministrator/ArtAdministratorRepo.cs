using ArtWebApp.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ArtWebApp.DataModelAtcWorld;
using System.Threading.Tasks;

namespace ArtWebApp.Areas.ArtAdministrator
{
    public static class ArtAdministratorRepo
    {


        public static void UpdateCostperminute()
        {

            using (AtcWorldEntities atcenty = new AtcWorldEntities())
            {
                using (ArtEntitiesnew artenty = new DataModels.ArtEntitiesnew())
                {

                    var q = (from loctation in atcenty.FactoryCostMasters
                             select loctation).ToList();

                 
                foreach(var element in q)
                {

                        var artLocation = from loctation in artenty.LocationMasters
                                          where loctation.Location_PK == element.LocationMaster_tbl.ArtLocation_PK
                                          select loctation;

                        foreach(var artloc in artLocation)
                        {


                            artloc.CostPerMinute = element.CostMinute / 100;


                        }




                }

                    artenty.SaveChanges();

            }


            }

        }





        









    }








}
using ArtWebApp.Areas.CuttingMVC.Models;
using ArtWebApp.DataModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ArtWebApp.Areas
{
    public class CuttingRepository
    {


        public void EditCutplanMaster(CutplanViewModel cutplanViewModel)
        {

            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {
                var q = from cutplanmaster in enty.CutPlanMasters
                        where cutplanmaster.CutPlan_PK == cutplanViewModel.cutPlanMaster.CutPlan_PK
                        select cutplanmaster;
                foreach(var ctmstr in q)
                {
                    ctmstr.CutType = cutplanViewModel.cutPlanMaster.CutType;
                    ctmstr.MarkerMade = cutplanViewModel.cutPlanMaster.MarkerMade;
                    ctmstr.Fabrication = cutplanViewModel.cutPlanMaster.Fabrication;
                }
                var q1 = from ctpolnmarker in enty.CutPlanMarkerTypes
                         where ctpolnmarker.CutPlan_PK == cutplanViewModel.cutPlanMaster.CutPlan_PK
                         select ctpolnmarker;
                foreach (var ctmstr in q1)
                {

                    if (cutplanViewModel.CutPlanMarkerTypes.Contains(ctmstr.CutPlanmarkerTypeName))
                    {

                    }
                    else
                    {
                        enty.CutPlanMarkerTypes.Remove(ctmstr);
                    }
                }
                foreach (var element in cutplanViewModel.CutPlanMarkerTypes)
                {
                    if (!enty.CutPlanMarkerTypes.Any(f => f.CutPlan_PK == cutplanViewModel.cutPlanMaster.CutPlan_PK && f.CutPlanmarkerTypeName == element))
                    {
                        CutPlanMarkerType cutPlanMarkerType = new CutPlanMarkerType();
                        cutPlanMarkerType.CutPlan_PK = cutplanViewModel.cutPlanMaster.CutPlan_PK;
                        cutPlanMarkerType.CutPlanmarkerTypeName = element;
                        enty.CutPlanMarkerTypes.Add(cutPlanMarkerType);
                    }

                 }

                

                    enty.SaveChanges();

            }



          }


        public DataTable CutplanASQNotPresent(int cutplanpk)
        {
            DataTable dt = new DataTable();




            SqlCommand cmd = new SqlCommand(@" SELECT        PoPackMaster.PoPacknum, PoPackMaster.PoPackId
FROM            POPackDetails INNER JOIN
                         CutPlanMaster ON POPackDetails.ColorCode = CutPlanMaster.ColorName AND POPackDetails.OurStyleID = CutPlanMaster.OurStyleID INNER JOIN
                         PoPackMaster ON POPackDetails.POPackId = PoPackMaster.PoPackId AND CutPlanMaster.Location_PK = PoPackMaster.ExpectedLocation_PK
WHERE        (CutPlanMaster.CutPlan_PK = @cutplanpk)
GROUP BY PoPackMaster.PoPacknum, PoPackMaster.PoPackId ");


            cmd.Parameters.AddWithValue("@cutplanpk", cutplanpk);






            return QueryFunctions.ReturnQueryResultDatatable(cmd); ;
        }

       
    }
}
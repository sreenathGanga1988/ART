using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArtWebApp.Production.Cutting
{
    public partial class CuttingDashBoardBoot : System.Web.UI.Page
    {



        protected void Page_Load(object sender, EventArgs e)
        {
           

            if (!IsPostBack)
            {


                if (Session["UserLoc_pk"].ToString() == "6")
                {

                }
                else
                {

                    int userloc_pk = int.Parse(Session["UserLoc_pk"].ToString());





                    PendingApproval.SelectCommand = @"SELECT        CutPlanMaster.CutPlanNUM, AtcDetails.OurStyle, CutPlanMaster.ColorName, CutPlanMaster.FabDescription, LocationMaster.LocationName, CutPlanMaster.MarkerType, CutPlanMaster.IsApproved, 
                         UserMaster.UserLoc_PK
FROM            CutPlanMaster INNER JOIN
                         AtcDetails ON CutPlanMaster.OurStyleID = AtcDetails.OurStyleID INNER JOIN
                         LocationMaster ON CutPlanMaster.Location_PK = LocationMaster.Location_PK INNER JOIN
                         UserMaster ON CutPlanMaster.AddedBy = UserMaster.UserName
WHERE(CutPlanMaster.IsApproved = N'N') AND (UserMaster.UserLoc_PK = " + userloc_pk + ")";



                    PendingPattern.SelectCommand = @"SELECT        CutPlanMaster.CutPlanNUM, AtcDetails.OurStyle, CutPlanMaster.ColorName, CutPlanMaster.FabDescription, LocationMaster.LocationName, CutPlanMaster.MarkerType, CutPlanMaster.IsApproved, 
                         CutPlanMaster.IsRatioAdded, CutPlanMaster.AddedBy, CutPlanMaster.IsPatternAdded, UserMaster.UserLoc_PK
FROM            CutPlanMaster INNER JOIN
                         AtcDetails ON CutPlanMaster.OurStyleID = AtcDetails.OurStyleID INNER JOIN
                         LocationMaster ON CutPlanMaster.Location_PK = LocationMaster.Location_PK INNER JOIN
                         UserMaster ON CutPlanMaster.AddedBy = UserMaster.UserName
WHERE(CutPlanMaster.IsApproved = N'Y') AND(CutPlanMaster.IsPatternAdded = N'N') AND (UserMaster.UserLoc_PK = " + userloc_pk + ")";



                    PendingCutOrder.SelectCommand = @"SELECT        CutPlanMaster.CutPlanNUM, AtcDetails.OurStyle, CutPlanMaster.ColorName, CutPlanMaster.FabDescription, LocationMaster.LocationName, CutPlanMaster.MarkerType, CutPlanMaster.IsApproved, 
                         CutPlanMaster.IsRatioAdded, CutPlanMaster.AddedBy, CutPlanMaster.IsPatternAdded, CutPlanMaster.IsCutorderGiven, UserMaster.UserLoc_PK
FROM            CutPlanMaster INNER JOIN
                         AtcDetails ON CutPlanMaster.OurStyleID = AtcDetails.OurStyleID INNER JOIN
                         LocationMaster ON CutPlanMaster.Location_PK = LocationMaster.Location_PK INNER JOIN
                         UserMaster ON CutPlanMaster.AddedBy = UserMaster.UserName
WHERE(CutPlanMaster.IsApproved = N'Y') AND(CutPlanMaster.IsPatternAdded = N'Y') AND(CutPlanMaster.IsCutorderGiven = 'N') AND (UserMaster.UserLoc_PK = " + userloc_pk + ")";




                    if (userloc_pk == 2)
                    {
                        userloc_pk = 8;
                    }
                    else if (userloc_pk == 4)
                    {
                        userloc_pk = 12;
                    }
                    else if (userloc_pk == 10)
                    {
                        userloc_pk = 11;
                    }
                    else if (userloc_pk == 13)
                    {
                        userloc_pk = 14;
                    }


                    PendingCutplan.SelectCommand = @"
                    SELECT OurStyleID, PoPackId, AtcNum, OurStyle, PoPacknum, LocationName, SUM(PoQty)AS PoQty, ExpectedLocation_PK, SUM(CutplanQty) AS CutplanQty, SUM(PoQty - CutplanQty) AS BalanceQty, HandoverDate,
                         CutplanQty AS Expr1, PoQty AS Expr2
FROM(SELECT        POPackDetails.OurStyleID, PoPackMaster.PoPackId, AtcMaster.AtcNum, AtcDetails.OurStyle, PoPackMaster.PoPacknum, LocationMaster.LocationName, POPackDetails.PoQty,
                         PoPackMaster.ExpectedLocation_PK, ISNULL
                             ((SELECT        SUM(CutPlanASQDetails.CutQty) AS Expr1
                                 FROM            CutPlanASQDetails INNER JOIN
                                                          CutPlanMaster ON CutPlanASQDetails.CutPlan_PK = CutPlanMaster.CutPlan_PK
                                 GROUP BY CutPlanMaster.Location_PK, CutPlanASQDetails.PoPack_Detail_PK
                                 HAVING(CutPlanMaster.Location_PK = PoPackMaster.ExpectedLocation_PK) AND(CutPlanASQDetails.PoPack_Detail_PK = POPackDetails.PoPack_Detail_PK)), 0) AS CutplanQty,
                         PoPackMaster.HandoverDate, POPackDetails.PoPack_Detail_PK
FROM            PoPackMaster INNER JOIN
                         POPackDetails ON PoPackMaster.PoPackId = POPackDetails.POPackId INNER JOIN
                         AtcMaster ON PoPackMaster.AtcId = AtcMaster.AtcId INNER JOIN
                         LocationMaster ON PoPackMaster.ExpectedLocation_PK = LocationMaster.Location_PK INNER JOIN
                         AtcDetails ON POPackDetails.OurStyleID = AtcDetails.OurStyleID
GROUP BY PoPackMaster.PoPackId, PoPackMaster.PoPacknum, AtcMaster.AtcNum, LocationMaster.LocationName, POPackDetails.PoQty, POPackDetails.OurStyleID, AtcDetails.OurStyle,
                         PoPackMaster.ExpectedLocation_PK, PoPackMaster.HandoverDate, POPackDetails.PoPack_Detail_PK
HAVING(PoPackMaster.HandoverDate > CONVERT(DATETIME, '2017-06-01 00:00:00', 102)) AND(PoPackMaster.ExpectedLocation_PK =  " + userloc_pk + @")) AS tt
GROUP BY OurStyleID, PoPackId, AtcNum, OurStyle, PoPacknum, LocationName, ExpectedLocation_PK, CutplanQty, HandoverDate, PoQty
HAVING(SUM(PoQty - CutplanQty) > 0)";














                    PendingApproval.DataBind();
                    PendingCutOrder.DataBind();
                    PendingPattern.DataBind();
                    PendingCutOrder.DataBind();

                    GridView6.DataBind();
                    GridView5.DataBind();

                }


            }

        }


        
    }
}
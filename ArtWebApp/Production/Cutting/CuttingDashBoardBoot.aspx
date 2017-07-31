<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CuttingDashBoardBoot.aspx.cs" Inherits="ArtWebApp.Production.Cutting.CuttingDashBoardBoot" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../../Scripts/jquery-3.1.1.js"></script>
    <link href="../../css/bootstrap.min.css" rel="stylesheet" />
    <script src="../../Scripts/bootstrap.min.js"></script>

    <script src="https://cdn.datatables.net/1.10.15/js/jquery.dataTables.min.js"></script>
<script src="https://cdn.datatables.net/1.10.15/js/dataTables.bootstrap.min.js"></script>
   

</head>
<body>
    <form id="form1" runat="server">
            <script type="text/javascript">

        //$(document).ready(function () {
        //    $('.table').DataTable();
        //});

    </script>
<div class="container">
  <h2>Cutting DashBoard</h2>
  <p>Click on the button to toggle between showing and hiding content.</p>
  <button type="button" class="btn btn-info" data-toggle="collapse" data-target="#PendingApprovalDiv">Pending Approval</button>
  <div id="PendingApprovalDiv" class="collapse in" >
       <asp:GridView ID="GridView4" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered " datasourceid="PendingApproval" Font-Size="Smaller" HeaderStyle-CssClass="header" PagerStyle-CssClass="pager" RowStyle-CssClass="rows">
                                <Columns>
                                    <asp:BoundField DataField="CutPlanNUM" HeaderText="CutPlan#" SortExpression="CutPlanNUM" />
                                    <asp:BoundField DataField="OurStyle" HeaderText="OurStyle" SortExpression="OurStyle" />
                                    <asp:BoundField DataField="ColorName" HeaderText="ColorName" SortExpression="ColorName" />
                                    <asp:BoundField DataField="FabDescription" HeaderText="FabDescription" SortExpression="FabDescription" />
                                    <asp:BoundField DataField="LocationName" HeaderText="Location" SortExpression="LocationName" />
                                    <asp:BoundField DataField="MarkerType" HeaderText="MarkerType" SortExpression="MarkerType" />
                                </Columns>
                                <HeaderStyle CssClass="header" />
                                <PagerStyle CssClass="pager" />
                                <RowStyle CssClass="rows" />
                            </asp:GridView>     <asp:SqlDataSource ID="PendingApproval" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT        CutPlanMaster.CutPlanNUM, AtcDetails.OurStyle, CutPlanMaster.ColorName, CutPlanMaster.FabDescription, LocationMaster.LocationName, CutPlanMaster.MarkerType, CutPlanMaster.IsApproved
FROM            CutPlanMaster INNER JOIN
                         AtcDetails ON CutPlanMaster.OurStyleID = AtcDetails.OurStyleID INNER JOIN
                         LocationMaster ON CutPlanMaster.Location_PK = LocationMaster.Location_PK
WHERE        (CutPlanMaster.IsApproved = N'N') AND (CutPlanMaster.IsRollAdded = N'Y')"></asp:SqlDataSource>
  </div>

    <button type="button" class="btn btn-info" data-toggle="collapse" data-target="#PendingpatternDiv">Pending Pattern</button>
  <div id="PendingpatternDiv" class="collapse in" >
         <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered" DataSourceID="PendingPattern" Font-Size="Smaller" HeaderStyle-CssClass="header" PagerStyle-CssClass="pager" RowStyle-CssClass="rows">
                                <Columns>
                                    <asp:BoundField DataField="CutPlanNUM" HeaderText="CutPlan#" SortExpression="CutPlanNUM" />
                                    <asp:BoundField DataField="OurStyle" HeaderText="OurStyle" SortExpression="OurStyle" />
                                    <asp:BoundField DataField="ColorName" HeaderText="ColorName" SortExpression="ColorName" />
                                    <asp:BoundField DataField="FabDescription" HeaderText="FabDescription" SortExpression="FabDescription" />
                                    <asp:BoundField DataField="LocationName" HeaderText="LocationName" SortExpression="LocationName" />
                                    <asp:BoundField DataField="MarkerType" HeaderText="MarkerType" SortExpression="MarkerType" />
                                </Columns>
                                <HeaderStyle CssClass="header" />
                                <PagerStyle CssClass="pager" />
                                <RowStyle CssClass="rows" />
                            </asp:GridView>

                            <asp:SqlDataSource ID="PendingPattern" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT        CutPlanMaster.CutPlanNUM, AtcDetails.OurStyle, CutPlanMaster.ColorName, CutPlanMaster.FabDescription, LocationMaster.LocationName, CutPlanMaster.MarkerType, CutPlanMaster.IsApproved, 
                         CutPlanMaster.IsRatioAdded, CutPlanMaster.AddedBy, CutPlanMaster.IsPatternAdded
FROM            CutPlanMaster INNER JOIN
                         AtcDetails ON CutPlanMaster.OurStyleID = AtcDetails.OurStyleID INNER JOIN
                         LocationMaster ON CutPlanMaster.Location_PK = LocationMaster.Location_PK
WHERE        (CutPlanMaster.IsApproved = N'Y') AND (CutPlanMaster.IsPatternAdded = N'N')">
                            </asp:SqlDataSource>
  </div>




       <button type="button" class="btn btn-info" data-toggle="collapse" data-target="#PendingCutorderDiv">Pending Cutorder</button>
  <div id="PendingCutorderDiv" class="collapse in" >
          <asp:GridView ID="GridView5" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered" datasourceid="PendingCutOrder" Font-Size="Smaller" HeaderStyle-CssClass="header" PagerStyle-CssClass="pager" RowStyle-CssClass="rows">
                                <Columns>
                                    <asp:BoundField DataField="CutPlanNUM" HeaderText="CutPlanNUM" SortExpression="CutPlanNUM" />
                                    <asp:BoundField DataField="OurStyle" HeaderText="OurStyle" SortExpression="OurStyle" />
                                    <asp:BoundField DataField="ColorName" HeaderText="ColorName" SortExpression="ColorName" />
                                    <asp:BoundField DataField="FabDescription" HeaderText="FabDescription" SortExpression="FabDescription" />
                                    <asp:BoundField DataField="LocationName" HeaderText="LocationName" SortExpression="LocationName" />
                                    <asp:BoundField DataField="MarkerType" HeaderText="MarkerType" SortExpression="MarkerType" />
                                </Columns>
                                <HeaderStyle CssClass="header" />
                                <PagerStyle CssClass="pager" />
                                <RowStyle CssClass="rows" />
                            </asp:GridView>
                            <asp:SqlDataSource ID="PendingCutOrder" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT        CutPlanMaster.CutPlanNUM, AtcDetails.OurStyle, CutPlanMaster.ColorName, CutPlanMaster.FabDescription, LocationMaster.LocationName, CutPlanMaster.MarkerType, CutPlanMaster.IsApproved, 
                         CutPlanMaster.IsRatioAdded, CutPlanMaster.AddedBy, CutPlanMaster.IsPatternAdded, CutPlanMaster.IsCutorderGiven
FROM            CutPlanMaster INNER JOIN
                         AtcDetails ON CutPlanMaster.OurStyleID = AtcDetails.OurStyleID INNER JOIN
                         LocationMaster ON CutPlanMaster.Location_PK = LocationMaster.Location_PK
WHERE        (CutPlanMaster.IsApproved = N'Y') AND (CutPlanMaster.IsPatternAdded = N'Y') AND (CutPlanMaster.IsCutorderGiven = 'N')"></asp:SqlDataSource>
                              

                            
  </div>




           <button type="button" class="btn btn-info" data-toggle="collapse" data-target="#PendingASQDiv">Pending ASQ for Cutplan</button>
  <div id="PendingASQDiv" class="collapse in" >
                       
        <asp:GridView ID="GridView6" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered" DataKeyNames="PoPackId" DataSourceID="PendingCutplan" Font-Size="Smaller" HeaderStyle-CssClass="header" PagerStyle-CssClass="pager" RowStyle-CssClass="rows">
                                <Columns>
                                    <asp:BoundField DataField="LocationName" HeaderText="Location" SortExpression="LocationName" />
                                    <asp:BoundField DataField="AtcNum" HeaderText="AtcNum" SortExpression="AtcNum" />
                                    <asp:BoundField DataField="OurStyle" HeaderText="OurStyle" SortExpression="OurStyle" />
                                    <asp:BoundField DataField="PoPacknum" HeaderText="PoPacknum" SortExpression="PoPacknum" />
                                    <asp:BoundField DataField="HandoverDate" HeaderText="HandoverDate" SortExpression="HandoverDate" />
                                    <asp:BoundField DataField="PoQty" HeaderText="PoQty" SortExpression="PoQty" />
                                    <asp:BoundField DataField="CutplanQty" HeaderText="CutplanQty" ReadOnly="True" SortExpression="CutplanQty" />
                                    <asp:BoundField DataField="BalanceQty" HeaderText="BalanceQty" ReadOnly="True" SortExpression="BalanceQty" />
                                </Columns>
                                <HeaderStyle CssClass="header" />
                                <PagerStyle CssClass="pager" />
                                <RowStyle CssClass="rows" />
                            </asp:GridView>
                            <asp:SqlDataSource ID="PendingCutplan" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT OurStyleID, PoPackId, AtcNum, OurStyle, PoPacknum, LocationName, ExpectedLocation_PK, SUM(PoQty - CutplanQty) AS BalanceQty, HandoverDate, CutplanQty, PoQty FROM (SELECT POPackDetails.OurStyleID, PoPackMaster.PoPackId, AtcMaster.AtcNum, AtcDetails.OurStyle, PoPackMaster.PoPacknum, LocationMaster.LocationName, POPackDetails.PoQty, PoPackMaster.ExpectedLocation_PK, ISNULL((SELECT SUM(CutPlanASQDetails.CutQty) AS Expr1 FROM CutPlanASQDetails INNER JOIN CutPlanMaster ON CutPlanASQDetails.CutPlan_PK = CutPlanMaster.CutPlan_PK GROUP BY CutPlanMaster.Location_PK, CutPlanASQDetails.PoPack_Detail_PK HAVING (CutPlanMaster.Location_PK = PoPackMaster.ExpectedLocation_PK) AND (CutPlanASQDetails.PoPack_Detail_PK = POPackDetails.PoPack_Detail_PK)), 0) AS CutplanQty, PoPackMaster.HandoverDate, POPackDetails.PoPack_Detail_PK FROM PoPackMaster INNER JOIN POPackDetails ON PoPackMaster.PoPackId = POPackDetails.POPackId INNER JOIN AtcMaster ON PoPackMaster.AtcId = AtcMaster.AtcId INNER JOIN LocationMaster ON PoPackMaster.ExpectedLocation_PK = LocationMaster.Location_PK INNER JOIN AtcDetails ON POPackDetails.OurStyleID = AtcDetails.OurStyleID GROUP BY PoPackMaster.PoPackId, PoPackMaster.PoPacknum, AtcMaster.AtcNum, LocationMaster.LocationName, POPackDetails.PoQty, POPackDetails.OurStyleID, AtcDetails.OurStyle, PoPackMaster.ExpectedLocation_PK, PoPackMaster.HandoverDate, POPackDetails.PoPack_Detail_PK HAVING (PoPackMaster.HandoverDate &gt; CONVERT (DATETIME, '2017-06-01 00:00:00', 102))) AS tt GROUP BY OurStyleID, PoPackId, AtcNum, OurStyle, PoPacknum, LocationName, ExpectedLocation_PK, HandoverDate, CutplanQty, PoQty HAVING (SUM(PoQty - CutplanQty) &gt; 0)">
                            </asp:SqlDataSource>
                            
  </div>







</div>
    </form>
</body>
</html>

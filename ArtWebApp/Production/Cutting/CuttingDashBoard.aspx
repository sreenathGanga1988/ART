<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="CuttingDashBoard.aspx.cs" Inherits="ArtWebApp.Production.Cutting.CuttingDashBoard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
           .mydatagrid

	{

	    width: 80%;
	    border: solid 2px black;

	    min-width: 80%;

	}

	.header

	{
	    background-color: #646464;

	    font-family: Arial;

	    color: White;

	    border: none 0px transparent;

	    height: 25px;

	    text-align: center;

	    font-size: 16px;

	}

	 

	.rows

	{

	    background-color: #fff;
	    font-family: Arial;
	    font-size: 14px;

	    color: #000;

	    min-height: 25px;

	    text-align: left;

	    border: none 0px transparent;

	}

	.rows:hover

	{

	    background-color: #ff8000;

	    font-family: Arial;

	    color: #fff;

	    text-align: left;

	}

	.selectedrow

	{

	    background-color: #ff8000;

	    font-family: Arial;

	    color: #fff;

	    font-weight: bold;

	    text-align: left;

	}

	.mydatagrid a /** FOR THE PAGING ICONS  **/

	{

	    background-color: Transparent;

	    padding: 5px 5px 5px 5px;

	    color: #fff;

	    text-decoration: none;

	    font-weight: bold;

	}

	 

	.mydatagrid a:hover /** FOR THE PAGING ICONS  HOVER STYLES**/

	{

	    background-color: #000;

	    color: #fff;

	}

	.mydatagrid span /** FOR THE PAGING ICONS CURRENT PAGE INDICATOR **/

	{

	    background-color: #c9c9c9;

	    color: #000;

	    padding: 5px 5px 5px 5px;

	}

	.pager

	{

	    background-color: #646464;

	    font-family: Arial;

	    color: White;

	    height: 30px;

	    text-align: left;

	}

	 

	.mydatagrid td

	{

	    padding: 5px;

	}

	.mydatagrid th

	{

	    padding: 5px;

	}
    </style>
    <link href="../../css/style.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="FullTable">
        <tr>
            <td class="RedHeadding">cutting Dash Board</td>
        </tr>
        <tr>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>
                <table class="DataEntryTable">
                    <tr>
                        <td class="RedHeadding">
                            Cut Plans Pending Approval</td>
                        <td class="RedHeadding">&nbsp;Pending Pattern</td>
                    </tr>
                    <tr>
                        <td class="smallgridtable">
                            <asp:GridView ID="GridView4" runat="server" AllowPaging="True" AutoGenerateColumns="False" CssClass="mydatagrid" datasourceid="PendingApproval" Font-Size="Smaller" HeaderStyle-CssClass="header" PagerStyle-CssClass="pager" RowStyle-CssClass="rows">
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
                            </asp:GridView>
                            <asp:SqlDataSource ID="PendingApproval" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT        CutPlanMaster.CutPlanNUM, AtcDetails.OurStyle, CutPlanMaster.ColorName, CutPlanMaster.FabDescription, LocationMaster.LocationName, CutPlanMaster.MarkerType, CutPlanMaster.IsApproved
FROM            CutPlanMaster INNER JOIN
                         AtcDetails ON CutPlanMaster.OurStyleID = AtcDetails.OurStyleID INNER JOIN
                         LocationMaster ON CutPlanMaster.Location_PK = LocationMaster.Location_PK
WHERE        (CutPlanMaster.IsApproved = N'N')"></asp:SqlDataSource>
                            
                            
                        </td>
                        <td class="smallgridtable">
                            
                            <asp:GridView ID="GridView2" runat="server" AllowPaging="True" AutoGenerateColumns="False" CssClass="mydatagrid" DataSourceID="PendingPattern" Font-Size="Smaller" HeaderStyle-CssClass="header" PagerStyle-CssClass="pager" RowStyle-CssClass="rows">
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

                        </td>
                    </tr>
                    <tr>
                        <td class="RedHeadding">Cut order not given</td>
                        <td class="RedHeadding">Cut Plans Pending Approval</td>
                    </tr>
                    <tr>
                        <td class="smallgridtable">
                            <asp:GridView ID="GridView5" runat="server" AllowPaging="True" AutoGenerateColumns="False" CssClass="mydatagrid" datasourceid="PendingCutOrder" Font-Size="Smaller" HeaderStyle-CssClass="header" PagerStyle-CssClass="pager" RowStyle-CssClass="rows">
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
                        </td>
                        <td>
                            
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>



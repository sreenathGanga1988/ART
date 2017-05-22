<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="InventoryDashBoard.aspx.cs" Inherits="ArtWebApp.Inventory.InventoryDashBoard" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register Assembly="Infragistics35.Web.v12.1, Version=12.1.20121.2236, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">







    <style type="text/css">

        
                .headerclass
        {
 background: #fcfcfc;
border-top-color: #c8c8c8;
border-left-color: #c8c8c8;
border-bottom-color: #179bd7;
border-width: 0 0 0 1px;
border-style: solid;
height: 40px;
padding: 0 .5em;
text-overflow: ellipsis;
white-space: nowrap;
text-align: left;
color:black;
font-weight: bold;
font-size: 14px;



line-height: 29px;
margin: -7px;
padding: 0 .7em;
text-align: left;
white-space: nowrap;

        }
   rowcell {
    border-width: 1px 0 0 1px;
    padding: .7em;
    line-height: 14px;
    white-space: nowrap;
    width: auto;
    vertical-align: middle;
}
.rowcell {
    border-width: 1px 0 0 1px;
    padding: .7em;
    line-height: 14px;
    white-space: nowrap;
    width: auto;
    vertical-align: middle;
    color:black;
}
     
      


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
    <link href="../css/style.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="FullTable">
        <tr>
            <td class="RedHeadding">QA&nbsp; Dash Board</td>
        </tr>
        <tr>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>
                <table class="DataEntryTable">
                    <tr>
                        <td class="RedHeadding">
                            ROLLS Pending MRN Mapping(All Location)</td>
                        <td class="RedHeadding">fabric mrn without roll (location)&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="smallgridtable">
                            <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AutoGenerateColumns="False" CssClass="mydatagrid" DataSourceID="RollsWithoutMRN" Font-Size="Smaller" HeaderStyle-CssClass="header" OnPageIndexChanging="GridView1_PageIndexChanging" PagerStyle-CssClass="pager" RowStyle-CssClass="rows">
                                <Columns>
                                    <asp:BoundField DataField="AtcNum" HeaderText="AtcNum" SortExpression="AtcNum" />
                                    <asp:BoundField DataField="itemDescription" HeaderText="itemDescription" SortExpression="itemDescription" ReadOnly="True" />
                                    <asp:BoundField DataField="SupplierDocnum" HeaderText="SupplierDocnum" SortExpression="SupplierDocnum" />
                                    <asp:BoundField DataField="AtracotrackingNum" HeaderText="AtracotrackingNum" SortExpression="AtracotrackingNum" />
                                    <asp:BoundField DataField="SupplierName" HeaderText="SupplierName" SortExpression="SupplierName" />
                                    <asp:BoundField DataField="SupplierETA" HeaderText="SupplierETA" SortExpression="SupplierETA" />
                                    <asp:BoundField DataField="Rollspending" HeaderText="Rollspending" ReadOnly="True" SortExpression="Rollspending" />
                                </Columns>

<HeaderStyle CssClass="header"></HeaderStyle>

<PagerStyle CssClass="pager"></PagerStyle>

<RowStyle CssClass="rows"></RowStyle>
                            </asp:GridView>
                            
                            <asp:SqlDataSource ID="RollsWithoutMRN" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT        AtcMaster.AtcNum, SkuRawMaterialMaster.RMNum + ' ' + ISNULL(SkuRawMaterialMaster.Composition, ' ') + ' ' + ISNULL(SkuRawMaterialMaster.Construction, ' ') + ' ' + ISNULL(SkuRawMaterialMaster.Weight, ' ') 
                         + ' ' + ISNULL(SkuRawMaterialMaster.Width, ' ') + ' ' + ISNULL(SkuRawmaterialDetail.ItemColor, ' ') AS itemDescription, SupplierDocumentMaster.SupplierDocnum, SupplierDocumentMaster.AtracotrackingNum, 
                         SupplierMaster.SupplierName, SupplierDocumentMaster.SupplierETA, COUNT(FabricRollmaster.Roll_PK) AS Rollspending
FROM            Template_Master INNER JOIN
                         FabricRollmaster INNER JOIN
                         SkuRawmaterialDetail INNER JOIN
                         SkuRawMaterialMaster ON SkuRawmaterialDetail.Sku_PK = SkuRawMaterialMaster.Sku_Pk ON FabricRollmaster.SkuDet_PK = SkuRawmaterialDetail.SkuDet_PK ON 
                         Template_Master.Template_PK = SkuRawMaterialMaster.Template_pk INNER JOIN
                         SupplierDocumentMaster ON FabricRollmaster.SupplierDoc_pk = SupplierDocumentMaster.SupplierDoc_pk INNER JOIN
                         SupplierMaster ON SupplierDocumentMaster.Supplier_pk = SupplierMaster.Supplier_PK INNER JOIN
                         AtcMaster ON SkuRawMaterialMaster.Atc_id = AtcMaster.AtcId
GROUP BY Template_Master.ItemGroup_PK, SkuRawMaterialMaster.RMNum + ' ' + ISNULL(SkuRawMaterialMaster.Composition, ' ') + ' ' + ISNULL(SkuRawMaterialMaster.Construction, ' ') 
                         + ' ' + ISNULL(SkuRawMaterialMaster.Weight, ' ') + ' ' + ISNULL(SkuRawMaterialMaster.Width, ' ') + ' ' + ISNULL(SkuRawmaterialDetail.ItemColor, ' '), SupplierDocumentMaster.SupplierDocnum, 
                         FabricRollmaster.MRnDet_PK, SupplierDocumentMaster.AtracotrackingNum, SupplierMaster.SupplierName, SupplierDocumentMaster.SupplierETA, AtcMaster.AtcNum
HAVING        (Template_Master.ItemGroup_PK = 1) AND (FabricRollmaster.MRnDet_PK = 0) AND (SupplierDocumentMaster.SupplierETA &gt; CONVERT(DATETIME, '2017-01-01 00:00:00', 102))"></asp:SqlDataSource>
                           
                            <asp:SqlDataSource ID="mrnPendingroll" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT        tt.Mrn_PK, tt.MrnNum, tt.PONum, tt.AddedBY, tt.AddedDate, tt.itemDescription, RollInventoryMaster.DocumentNum
FROM            (SELECT        MrnMaster.Mrn_PK, MrnMaster.MrnNum, MrnMaster.AddedBY, MrnMaster.AddedDate, SkuRawMaterialMaster.RMNum + ' ' + ISNULL(SkuRawMaterialMaster.Composition, N' ') 
                                                    + ' ' + ISNULL(SkuRawMaterialMaster.Construction, N' ') + ' ' + ISNULL(SkuRawMaterialMaster.Weight, N' ') + ' ' + ISNULL(SkuRawMaterialMaster.Width, N' ') 
                                                    + ' ' + ISNULL(SkuRawmaterialDetail.ItemColor, N' ') AS itemDescription, MrnMaster.Location_Pk, ProcurementMaster.PONum
                          FROM            Template_Master INNER JOIN
                                                    MrnMaster INNER JOIN
                                                    SkuRawMaterialMaster INNER JOIN
                                                    SkuRawmaterialDetail ON SkuRawMaterialMaster.Sku_Pk = SkuRawmaterialDetail.Sku_PK INNER JOIN
                                                    MrnDetails ON SkuRawmaterialDetail.SkuDet_PK = MrnDetails.SkuDet_PK ON MrnMaster.Mrn_PK = MrnDetails.Mrn_PK ON 
                                                    Template_Master.Template_PK = SkuRawMaterialMaster.Template_pk INNER JOIN
                                                    ProcurementMaster ON MrnMaster.Po_PK = ProcurementMaster.PO_Pk
                          WHERE        (Template_Master.ItemGroup_PK = 1) AND (MrnMaster.AddedDate &gt; N'01 january 2017') AND (MrnMaster.Location_Pk = @Param1)) AS tt LEFT OUTER JOIN
                         RollInventoryMaster ON tt.MrnNum = RollInventoryMaster.DocumentNum WHERE        (RollInventoryMaster.DocumentNum IS NULL)">
                                <SelectParameters>
                                    <asp:SessionParameter DefaultValue="0" Name="Param1" SessionField="UserLoc_pk" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                        </td>
                        <td class="smallgridtable">
                            <ig:WebDataGrid ID="Webdatagrid" runat="server" AutoGenerateColumns="False" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" CellSpacing="1" ClientIDMode="Static" DataMember="DefaultView" DataSourceID="mrnPendingroll" DefaultColumnWidth="120px" Font-Bold="True" ForeColor="#000099"  HeaderCaptionCssClass="headerclass" Height="100%" ItemCssClass="rowcell" Width="100%">
                                <Columns>
                                    <ig:BoundDataField DataFieldName="Mrn_PK" Key="Mrn_PK">
                                        <Header Text="Mrn_PK">
                                        </Header>
                                    </ig:BoundDataField>
                                    <ig:BoundDataField DataFieldName="MrnNum" Key="MrnNum">
                                        <Header Text="MrnNum">
                                        </Header>
                                    </ig:BoundDataField>
                                    <ig:BoundDataField DataFieldName="PONum" Key="PONum">
                                        <Header Text="PONum">
                                        </Header>
                                    </ig:BoundDataField>
                                    <ig:BoundDataField DataFieldName="AddedBY" Key="AddedBY">
                                        <Header Text="AddedBY">
                                        </Header>
                                    </ig:BoundDataField>
                                    <ig:BoundDataField DataFieldName="AddedDate" Key="AddedDate">
                                        <Header Text="AddedDate">
                                        </Header>
                                    </ig:BoundDataField>
                                    <ig:BoundDataField  DataFieldName="itemDescription" Key="itemDescription">
                                        <Header Text="itemDescription">
                                        </Header>
                                    </ig:BoundDataField>
                                    <ig:BoundDataField DataFieldName="DocumentNum" Key="DocumentNum">
                                        <Header Text="DocumentNum">
                                        </Header>
                                    </ig:BoundDataField>
                                </Columns>
        <behaviors>
           <ig:Filtering FilterType="ExcelStyleFilter">
            </ig:Filtering>
           
            <ig:Paging PageSize="25">
            </ig:Paging>
            <ig:Sorting>
            </ig:Sorting>
      
            <ig:SummaryRow>
                <ColumnSummaries>
                    
                </ColumnSummaries>
            </ig:SummaryRow>
      
        </behaviors>
</ig:WebDataGrid></td>
                    </tr>
                    <tr>
                        <td class="RedHeadding">ro without rolls</td>
                        <td class="RedHeadding">Loan pending Return</td>
                    </tr>
                    <tr>
                        <td class="smallgridtable">
                            <asp:GridView ID="GridView3" runat="server" AllowPaging="True" AutoGenerateColumns="False" PageSize="25" CssClass="mydatagrid" Font-Size="Smaller" HeaderStyle-CssClass="header" OnPageIndexChanging="GridView2_PageIndexChanging" PagerStyle-CssClass="pager" RowStyle-CssClass="rows">
                                <Columns>
                                  <asp:BoundField DataField="Containernum" HeaderText="Ref" SortExpression="Containernum" />
                                    <asp:BoundField DataField="AtracotrackingNum" HeaderText="ASN" SortExpression="AtracotrackingNum" />
                                    <asp:BoundField DataField="AtcNum" HeaderText="Atc" SortExpression="AtcNum" />
                                    <asp:BoundField DataField="itemDescription" HeaderText="itemDescription" SortExpression="itemDescription" ReadOnly="True" />
                                    <asp:BoundField DataField="SupplierDocnum" HeaderText="Inv#" SortExpression="SupplierDocnum" />
                                    <asp:BoundField DataField="PONum" HeaderText="PO" SortExpression="PONum" />
                                    <asp:BoundField DataField="PendingRolls" HeaderText="Pending Rolls" ReadOnly="True" SortExpression="PendingRolls" />
                                </Columns>
                                <HeaderStyle CssClass="header" />
                                <PagerStyle CssClass="pager" />
                                <RowStyle CssClass="rows" />
                            </asp:GridView>
                            <asp:SqlDataSource ID="PendingGrouping" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT        SupplierDocumentMaster.Containernum, SupplierDocumentMaster.AtracotrackingNum, AtcMaster.AtcNum, SkuRawMaterialMaster.RMNum + ' ' + ISNULL(SkuRawMaterialMaster.Composition, ' ') 
                         + ' ' + ISNULL(SkuRawMaterialMaster.Construction, ' ') + ' ' + ISNULL(SkuRawMaterialMaster.Weight, ' ') + ' ' + ISNULL(SkuRawMaterialMaster.Width, ' ') + ' ' + ISNULL(SkuRawmaterialDetail.ItemColor, ' ') 
                         + ' ' + ISNULL(SkuRawmaterialDetail.ItemSize, ' ') + ' ' + ISNULL(ProcurementDetails.SupplierSize, ' ') + ' ' + ISNULL(ProcurementDetails.SupplierColor, ' ') AS itemDescription, 
                         SupplierDocumentMaster.SupplierDocnum, ProcurementMaster.PONum, count (FabricRollmaster.Roll_PK) AS PendingRolls
FROM            SkuRawmaterialDetail INNER JOIN
                         SkuRawMaterialMaster ON SkuRawmaterialDetail.Sku_PK = SkuRawMaterialMaster.Sku_Pk INNER JOIN
                         FabricRollmaster INNER JOIN
                         SupplierDocumentMaster ON FabricRollmaster.SupplierDoc_pk = SupplierDocumentMaster.SupplierDoc_pk ON SkuRawmaterialDetail.SkuDet_PK = FabricRollmaster.SkuDet_PK INNER JOIN
                         ProcurementDetails ON FabricRollmaster.podet_pk = ProcurementDetails.PODet_PK INNER JOIN
                         ProcurementMaster ON ProcurementDetails.PO_Pk = ProcurementMaster.PO_Pk INNER JOIN
                         AtcMaster ON SkuRawMaterialMaster.Atc_id = AtcMaster.AtcId
WHERE        (FabricRollmaster.IsSaved &lt;&gt; N'N') AND (FabricRollmaster.IsApproved &lt;&gt; N'N')  AND ((FabricRollmaster.WidthGroup IS NULL) OR (FabricRollmaster.ShadeGroup IS NULL) OR 
                         (FabricRollmaster.ShrinkageGroup IS NULL))AND (SupplierDocumentMaster.SupplierETA &gt; CONVERT(DATETIME, '2016-12-20 00:00:00', 102))
GROUP BY SupplierDocumentMaster.Containernum, SupplierDocumentMaster.AtracotrackingNum, AtcMaster.AtcNum, SkuRawMaterialMaster.RMNum + ' ' + ISNULL(SkuRawMaterialMaster.Composition, ' ') 
                         + ' ' + ISNULL(SkuRawMaterialMaster.Construction, ' ') + ' ' + ISNULL(SkuRawMaterialMaster.Weight, ' ') + ' ' + ISNULL(SkuRawMaterialMaster.Width, ' ') + ' ' + ISNULL(SkuRawmaterialDetail.ItemColor, ' ') 
                         + ' ' + ISNULL(SkuRawmaterialDetail.ItemSize, ' ') + ' ' + ISNULL(ProcurementDetails.SupplierSize, ' ') + ' ' + ISNULL(ProcurementDetails.SupplierColor, ' '), SupplierDocumentMaster.SupplierDocnum, 
                         ProcurementMaster.PONum"></asp:SqlDataSource>
                        </td>
                        <td>
                            <asp:GridView ID="GridView4" runat="server" AllowPaging="True" AutoGenerateColumns="False" PageSize="25" CssClass="mydatagrid" datasourceid="loanpending" Font-Size="Smaller" HeaderStyle-CssClass="header" OnPageIndexChanging="GridView2_PageIndexChanging" PagerStyle-CssClass="pager" RowStyle-CssClass="rows" DataKeyNames="Loan_PK">
                                <Columns>
                                    <asp:BoundField DataField="LoanNum" HeaderText="LoanNum" SortExpression="LoanNum" />
                                    <asp:BoundField DataField="AtcNum" HeaderText="AtcNum" SortExpression="AtcNum" />
                                    <asp:BoundField DataField="FromITEM" HeaderText="FromITEM" SortExpression="FromITEM" ReadOnly="True" />
                                    <asp:BoundField DataField="LoanQty" HeaderText="LoanQty" SortExpression="LoanQty" />
                                    <asp:BoundField DataField="LocationName" HeaderText="LocationName" SortExpression="LocationName" />
                                    <asp:BoundField DataField="ReturnQty" HeaderText="ReturnQty" ReadOnly="True" SortExpression="ReturnQty" />
                                    <asp:BoundField DataField="IsApproved" HeaderText="IsApproved" SortExpression="IsApproved" />
                                </Columns>
                                <HeaderStyle CssClass="header" />
                                <PagerStyle CssClass="pager" />
                                <RowStyle CssClass="rows" />
                            </asp:GridView>
                            <asp:SqlDataSource ID="loanpending" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT        Loan_PK, LoanNum, FromITEM, LoanQty, UnitPrice, LoanType, ToSkuDet_PK, Location_PK, LocationName, AddedBY, AddedDate, ApprovedDate, AtcNum, FromSkudet_PK, ReturnQty, IsApproved
FROM            (SELECT        tt.Loan_PK, tt.LoanNum, tt.FromITEM, tt.LoanQty, tt.UnitPrice, tt.LoanType, tt.ToSkuDet_PK, tt.Location_PK, tt.LocationName, tt.AddedBY, tt.AddedDate, tt.ApprovedDate, tt.AtcNum, tt.FromSkudet_PK, 
                                                    ISNULL(SUM(InventoryLoanMaster_1.LoanQty), 0) AS ReturnQty, tt.IsApproved
                          FROM            (SELECT        InventoryLoanMaster.Loan_PK, InventoryLoanMaster.LoanNum, ISNULL(SkuRawMaterialMaster_1.RMNum, ' ') + ' ' + ISNULL(SkuRawMaterialMaster_1.Composition, ' ') 
                                                                              + ' ' + ISNULL(SkuRawMaterialMaster_1.Construction, ' ') + ' ' + ISNULL(SkuRawMaterialMaster_1.Weight, ' ') + ' ' + ISNULL(SkuRawMaterialMaster_1.Width, ' ') 
                                                                              + ' ' + ISNULL(SkuRawmaterialDetail_1.ItemColor, ' ') + ' ' + ISNULL(SkuRawmaterialDetail_1.ItemSize, ' ') AS FromITEM, InventoryLoanMaster.LoanQty, InventoryLoanMaster.UnitPrice, 
                                                                              InventoryLoanMaster.LoanType, InventoryLoanMaster.ToSkuDet_PK, InventoryLoanMaster.Location_PK, LocationMaster.LocationName, InventoryLoanMaster.AddedBY, 
                                                                              InventoryLoanMaster.AddedDate, InventoryLoanMaster.IsApproved, InventoryLoanMaster.IsDeleted, InventoryLoanMaster.ApprovedBy, InventoryLoanMaster.DeletedBy, 
                                                                              InventoryLoanMaster.DeletedDate, InventoryLoanMaster.ApprovedDate, AtcMaster.AtcNum, InventoryLoanMaster.FromSkudet_PK
                                                    FROM            SkuRawMaterialMaster AS SkuRawMaterialMaster_1 INNER JOIN
                                                                              SkuRawmaterialDetail AS SkuRawmaterialDetail_1 ON SkuRawMaterialMaster_1.Sku_Pk = SkuRawmaterialDetail_1.Sku_PK INNER JOIN
                                                                              InventoryMaster ON SkuRawmaterialDetail_1.SkuDet_PK = InventoryMaster.SkuDet_Pk INNER JOIN
                                                                              InventoryLoanMaster ON InventoryMaster.InventoryItem_PK = InventoryLoanMaster.FromIIT_Pk INNER JOIN
                                                                              LocationMaster ON InventoryLoanMaster.Location_PK = LocationMaster.Location_PK INNER JOIN
                                                                              AtcMaster ON SkuRawMaterialMaster_1.Atc_id = AtcMaster.AtcId
                                                    WHERE        (InventoryLoanMaster.LoanType = N'Loan')) AS tt LEFT OUTER JOIN
                                                    InventoryLoanMaster AS InventoryLoanMaster_1 ON tt.ToSkuDet_PK = InventoryLoanMaster_1.FromSkudet_PK AND tt.FromSkudet_PK = InventoryLoanMaster_1.ToSkuDet_PK
                          GROUP BY tt.Loan_PK, tt.LoanNum, tt.FromITEM, tt.LoanQty, tt.UnitPrice, tt.LoanType, tt.ToSkuDet_PK, tt.Location_PK, tt.LocationName, tt.AddedBY, tt.AddedDate, tt.ApprovedDate, tt.AtcNum, tt.FromSkudet_PK, 
                                                    tt.IsApproved) AS TTT
WHERE        (LoanQty &lt;&gt; ReturnQty) AND (Location_PK = @Param1)">
                                <SelectParameters>
                                    <asp:SessionParameter Name="Param1" SessionField="UserLoc_pk" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>


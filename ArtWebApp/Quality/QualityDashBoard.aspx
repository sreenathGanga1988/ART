<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="QualityDashBoard.aspx.cs" Inherits="ArtWebApp.Quality.QualityDashBoard" %>
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
                            Pending validation </td>
                        <td class="RedHeadding">&nbsp;Pending INSPECTION</td>
                    </tr>
                    <tr>
                        <td class="smallgridtable">
                            <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AutoGenerateColumns="False" CssClass="mydatagrid" DataSourceID="PendingValidation" Font-Size="Smaller" HeaderStyle-CssClass="header" OnPageIndexChanging="GridView1_PageIndexChanging" PagerStyle-CssClass="pager" RowStyle-CssClass="rows">
                                <Columns>
                                    <asp:BoundField DataField="Containernum" HeaderText="Ref" SortExpression="Containernum" />
                                    <asp:BoundField DataField="AtracotrackingNum" HeaderText="ASN" SortExpression="AtracotrackingNum" />
                                    <asp:BoundField DataField="AtcNum" HeaderText="Atc" SortExpression="AtcNum" />
                                    <asp:BoundField DataField="itemDescription" HeaderText="itemDescription" SortExpression="itemDescription" ReadOnly="True" />
                                    <asp:BoundField DataField="SupplierDocnum" HeaderText="Inv#" SortExpression="SupplierDocnum" />
                                    <asp:BoundField DataField="PONum" HeaderText="PO" SortExpression="PONum" />
                                    <asp:BoundField DataField="PendingRolls" HeaderText="Pending Rolls" ReadOnly="True" SortExpression="PendingRolls" />
                                </Columns>

<HeaderStyle CssClass="header"></HeaderStyle>

<PagerStyle CssClass="pager"></PagerStyle>

<RowStyle CssClass="rows"></RowStyle>
                            </asp:GridView>
                            
                            <asp:SqlDataSource ID="PendingValidation" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT        SupplierDocumentMaster.Containernum, SupplierDocumentMaster.AtracotrackingNum, AtcMaster.AtcNum, SkuRawMaterialMaster.RMNum + ' ' + ISNULL(SkuRawMaterialMaster.Composition, ' ') 
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
WHERE        (FabricRollmaster.IsSaved = 'N') AND (SupplierDocumentMaster.SupplierETA &gt; CONVERT(DATETIME, '2016-12-20 00:00:00', 102))
GROUP BY SupplierDocumentMaster.Containernum, SupplierDocumentMaster.AtracotrackingNum, AtcMaster.AtcNum, SkuRawMaterialMaster.RMNum + ' ' + ISNULL(SkuRawMaterialMaster.Composition, ' ') 
                         + ' ' + ISNULL(SkuRawMaterialMaster.Construction, ' ') + ' ' + ISNULL(SkuRawMaterialMaster.Weight, ' ') + ' ' + ISNULL(SkuRawMaterialMaster.Width, ' ') + ' ' + ISNULL(SkuRawmaterialDetail.ItemColor, ' ') 
                         + ' ' + ISNULL(SkuRawmaterialDetail.ItemSize, ' ') + ' ' + ISNULL(ProcurementDetails.SupplierSize, ' ') + ' ' + ISNULL(ProcurementDetails.SupplierColor, ' '), SupplierDocumentMaster.SupplierDocnum, 
                         ProcurementMaster.PONum"></asp:SqlDataSource>
                           
                            <asp:SqlDataSource ID="PendingInspection" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT        SupplierDocumentMaster.Containernum, SupplierDocumentMaster.AtracotrackingNum, AtcMaster.AtcNum, SkuRawMaterialMaster.RMNum + ' ' + ISNULL(SkuRawMaterialMaster.Composition, ' ') 
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
WHERE        (FabricRollmaster.IsSaved &lt;&gt; N'N') AND (FabricRollmaster.IsApproved &lt;&gt; N'Y') AND (SupplierDocumentMaster.SupplierETA &gt; CONVERT(DATETIME, '2016-12-20 00:00:00', 102))
GROUP BY SupplierDocumentMaster.Containernum, SupplierDocumentMaster.AtracotrackingNum, AtcMaster.AtcNum, SkuRawMaterialMaster.RMNum + ' ' + ISNULL(SkuRawMaterialMaster.Composition, ' ') 
                         + ' ' + ISNULL(SkuRawMaterialMaster.Construction, ' ') + ' ' + ISNULL(SkuRawMaterialMaster.Weight, ' ') + ' ' + ISNULL(SkuRawMaterialMaster.Width, ' ') + ' ' + ISNULL(SkuRawmaterialDetail.ItemColor, ' ') 
                         + ' ' + ISNULL(SkuRawmaterialDetail.ItemSize, ' ') + ' ' + ISNULL(ProcurementDetails.SupplierSize, ' ') + ' ' + ISNULL(ProcurementDetails.SupplierColor, ' '), SupplierDocumentMaster.SupplierDocnum, 
                         ProcurementMaster.PONum"></asp:SqlDataSource>
                        </td>
                        <td class="smallgridtable">
                            <asp:GridView ID="GridView2" runat="server" AllowPaging="True" AutoGenerateColumns="False" CssClass="mydatagrid" DataSourceID="PendingInspection" Font-Size="Smaller" HeaderStyle-CssClass="header" OnPageIndexChanging="GridView2_PageIndexChanging" PagerStyle-CssClass="pager" RowStyle-CssClass="rows">
                                <Columns>
                                    <asp:BoundField DataField="Containernum" HeaderText="Ref" SortExpression="Containernum" />
                                    <asp:BoundField DataField="AtracotrackingNum" HeaderText="ASN" SortExpression="AtracotrackingNum" />
                                    <asp:BoundField DataField="AtcNum" HeaderText="Atc" SortExpression="AtcNum" />
                                    <asp:BoundField DataField="itemDescription" HeaderText="itemDescription" ReadOnly="True" SortExpression="itemDescription" />
                                    <asp:BoundField DataField="SupplierDocnum" HeaderText="Inv#" SortExpression="SupplierDocnum" />
                                    <asp:BoundField DataField="PONum" HeaderText="PO#" SortExpression="PONum" />
                                    <asp:BoundField DataField="PendingRolls" HeaderText="PendingRolls" ReadOnly="True" SortExpression="PendingRolls" />
                                </Columns>
                                <HeaderStyle CssClass="header" />
                                <PagerStyle CssClass="pager" />
                                <RowStyle CssClass="rows" />
                            </asp:GridView></td>
                    </tr>
                    <tr>
                        <td class="RedHeadding">Pending GROUPING</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="GridView3" runat="server" AllowPaging="True" AutoGenerateColumns="False" CssClass="mydatagrid" DataSourceID="PendingGrouping" Font-Size="Smaller" HeaderStyle-CssClass="header" OnPageIndexChanging="GridView2_PageIndexChanging" PagerStyle-CssClass="pager" RowStyle-CssClass="rows">
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
                        <td>&nbsp;</td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>


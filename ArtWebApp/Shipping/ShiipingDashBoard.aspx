<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="ShiipingDashBoard.aspx.cs" Inherits="ArtWebApp.Shipping.ShiipingDashBoard" %>
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
            <td class="RedHeadding">Shipping Dash Board</td>
        </tr>
        <tr>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>
                <table class="DataEntryTable">
                    <tr>
                        <td class="RedHeadding">
                            Pending ADN International</td>
                        <td class="RedHeadding">&nbsp;Pending AW(international)&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="smallgridtable">
                            <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AutoGenerateColumns="False" CssClass="mydatagrid" DataSourceID="PendingADN" Font-Size="Smaller" HeaderStyle-CssClass="header" OnPageIndexChanging="GridView1_PageIndexChanging" PagerStyle-CssClass="pager" RowStyle-CssClass="rows">
                                <Columns>
                                    <asp:BoundField DataField="DocNum" HeaderText="ADN" SortExpression="DocNum" />
                                    <asp:BoundField DataField="Invoicenumber" HeaderText="Invoice" SortExpression="Invoicenumber" />
                                    <asp:BoundField DataField="SupplierName" HeaderText="Supplier" SortExpression="SupplierName" />
                                    <asp:BoundField DataField="BOENum" HeaderText="Reference" SortExpression="BOENum" />
                                    <asp:BoundField DataField="ETADate" DataFormatString="{0:MM/dd/yyyy}" HeaderText="ETADate" SortExpression="ETADate" />
                                </Columns>
                            </asp:GridView>
                            
                            <asp:SqlDataSource ID="PendingADN" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT DocMaster.DocNum, DocMaster.ContainerNum AS Invoicenumber, SupplierMaster.SupplierName, DocMaster.BOENum, DocMaster.ETADate FROM DocMaster INNER JOIN SupplierMaster ON DocMaster.Supplier_PK = SupplierMaster.Supplier_PK LEFT OUTER JOIN ShippingDocumentDetails ON DocMaster.Doc_Pk = ShippingDocumentDetails.Doc_Pk WHERE (DocMaster.ADNType = 'IntlSupplier') AND (ShippingDocumentDetails.ShippingDet_PK IS NULL) AND (DocMaster.ETADate &gt; CONVERT (DATETIME, '2016-12-20 00:00:00', 102))"></asp:SqlDataSource>
                           
                            <asp:SqlDataSource ID="PendingAWData" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT        DeliveryOrderMaster.ContainerNumber, DeliveryOrderMaster.DeliveryDate, LocationMaster.LocationName, COUNT(DeliveryOrderMaster.DO_PK) AS dOCUMENTS
FROM            DeliveryOrderMaster INNER JOIN
                         LocationMaster ON DeliveryOrderMaster.ToLocation_PK = LocationMaster.Location_PK LEFT OUTER JOIN
                         ShippingDocumentDODetails ON DeliveryOrderMaster.DO_PK = ShippingDocumentDODetails.DO_PK
WHERE        (DeliveryOrderMaster.DONum LIKE 'AWATRW%')
GROUP BY DeliveryOrderMaster.ContainerNumber, DeliveryOrderMaster.DeliveryDate, LocationMaster.LocationName, ShippingDocumentDODetails.ShippingDocumentDO_PK
HAVING        (DeliveryOrderMaster.DeliveryDate &gt; CONVERT(DATETIME, '2016-12-20 00:00:00', 102)) AND (ShippingDocumentDODetails.ShippingDocumentDO_PK IS NULL)"></asp:SqlDataSource>
                        </td>
                        <td class="smallgridtable">
                            <asp:GridView ID="GridView2" runat="server" AllowPaging="True" AutoGenerateColumns="False" CssClass="mydatagrid" DataSourceID="PendingAWData" Font-Size="Smaller" HeaderStyle-CssClass="header" OnPageIndexChanging="GridView2_PageIndexChanging" PagerStyle-CssClass="pager" RowStyle-CssClass="rows" OnRowCommand="GridView2_RowCommand">
                                <Columns>
                                    <asp:BoundField DataField="ContainerNumber" HeaderText="Rference" SortExpression="ContainerNumber" />
                                    <asp:BoundField DataField="DeliveryDate" DataFormatString="{0:MM/dd/yyyy}" HeaderText="DeliveryDate" SortExpression="DeliveryDate" />
                                    <asp:BoundField DataField="LocationName" HeaderText="TO" SortExpression="LocationName" />
                                      <asp:BoundField DataField="dOCUMENTS" HeaderText="No of Doc" SortExpression="dOCUMENTS" />
                                    
                                    <asp:ButtonField CommandName="Show" Text="Show" />
                                </Columns>
                                <HeaderStyle CssClass="header" />
                                <PagerStyle CssClass="pager" />
                                <RowStyle CssClass="rows" />
                            </asp:GridView></td>
                    </tr>
                    <tr>
                        <td class="RedHeadding">Pending Invoicing</td>
                        <td class="RedHeadding">Pending Sales DO To Book</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="GridView3" runat="server" AllowPaging="True" AutoGenerateColumns="False" CssClass="mydatagrid" DataSourceID="PendingInvoicing" Font-Size="Smaller" HeaderStyle-CssClass="header" OnPageIndexChanging="GridView2_PageIndexChanging" PagerStyle-CssClass="pager" RowStyle-CssClass="rows">
                                <Columns>
                                    <asp:BoundField DataField="ShipmentHandOverCode" HeaderText="Shipment#" SortExpression="ShipmentHandOverCode" />
                                    <asp:BoundField DataField="LocationName" HeaderText="Location" SortExpression="LocationName" />
                                    <asp:BoundField DataField="ShipmentHandOverDate" HeaderText="Date" SortExpression="ShipmentHandOverDate" DataFormatString="{0:MM/dd/yyyy}"  />
                                    <asp:BoundField DataField="ShippedQty" HeaderText="Shipped Qty" ReadOnly="True" SortExpression="ShippedQty" />
                                    <asp:BoundField DataField="InvoiceQty" HeaderText="Invoice Qty" ReadOnly="True" SortExpression="InvoiceQty" />
                                    <asp:BoundField DataField="Diff" HeaderText="Diff" ReadOnly="True" SortExpression="Diff" />
                                </Columns>
                                <HeaderStyle CssClass="header" />
                                <PagerStyle CssClass="pager" />
                                <RowStyle CssClass="rows" />
                            </asp:GridView>
                            <asp:SqlDataSource ID="PendingInvoicing" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT        ShipmentHandOverCode, LocationName, ShipmentHandOverDate, ShippedQty, InvoiceQty ,(ShippedQty-InvoiceQty) as Diff
FROM            (SELECT        ShipmentHandOverMaster.ShipmentHandOverCode, ShipmentHandOverMaster.IsCompleted, SUM(ShipmentHandOverDetails.ShippedQty) AS ShippedQty, ShipmentHandOverDetails.ShipmentHandOverDate, 
                         LocationMaster.LocationName  ,isnull((
SELECT        SUM(InvoiceDetail.InvoiceQty) 
FROM            InvoiceDetail INNER JOIN
                         ShipmentHandOverDetails ON InvoiceDetail.ShipmentHandOver_PK = ShipmentHandOverDetails.ShipmentHandOver_PK
WHERE        (ShipmentHandOverDetails.ShipmentHandMaster_PK = ShipmentHandOverMaster.ShipmentHandMaster_PK)),0) as InvoiceQty
FROM            ShipmentHandOverDetails INNER JOIN
                         ShipmentHandOverMaster ON ShipmentHandOverDetails.ShipmentHandMaster_PK = ShipmentHandOverMaster.ShipmentHandMaster_PK INNER JOIN
                         LocationMaster ON ShipmentHandOverMaster.Location_Pk = LocationMaster.Location_PK
GROUP BY ShipmentHandOverMaster.ShipmentHandMaster_PK, ShipmentHandOverMaster.ShipmentHandOverCode, ShipmentHandOverMaster.IsCompleted, ShipmentHandOverDetails.ShipmentHandOverDate, LocationMaster.LocationName
HAVING        (ShipmentHandOverMaster.IsCompleted = N'N')) AS tt
WHERE        (ShippedQty &lt;&gt; InvoiceQty)
"></asp:SqlDataSource>
                        </td>
                        <td><asp:GridView ID="GridView4" runat="server" AllowPaging="True" AutoGenerateColumns="False" CssClass="mydatagrid" DataSourceID="PendingSalesDO" Font-Size="Smaller" HeaderStyle-CssClass="header" PageSize="25" OnPageIndexChanging="GridView2_PageIndexChanging" PagerStyle-CssClass="pager" RowStyle-CssClass="rows">
                                <Columns>
                                    <asp:BoundField DataField="SDONo" HeaderText="SDONo" SortExpression="SDONo" />
                                    <asp:BoundField DataField="AtcNum" HeaderText="AtcNum" SortExpression="AtcNum" />
                                    <asp:BoundField DataField="OurStyle" HeaderText="OurStyle" SortExpression="OurStyle" />
                                    <asp:BoundField DataField="BuyerStyle" HeaderText="BuyerStyle" SortExpression="BuyerStyle" />
                                    <asp:BoundField DataField="ShipQty" HeaderText="Shipped Qty" ReadOnly="True" SortExpression="ShipQty" />
                                </Columns>
                                <HeaderStyle CssClass="header" />
                                <PagerStyle CssClass="pager" />
                                <RowStyle CssClass="rows" />
                            </asp:GridView>
                            <asp:SqlDataSource ID="PendingSalesDO" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT        ATCWorldToArtShipData.SDONo, ATCWorldToArtShipData.OurStyleId, ATCWorldToArtShipData.BuyerStyle, SUM(ATCWorldToArtShipData.ShipQty) AS ShipQty, AtcDetails.OurStyle, AtcMaster.AtcNum
FROM            ATCWorldToArtShipData INNER JOIN
                         AtcDetails ON ATCWorldToArtShipData.OurStyleId = AtcDetails.OurStyleID INNER JOIN
                         AtcMaster ON AtcDetails.AtcId = AtcMaster.AtcId
WHERE        (ATCWorldToArtShipData.IsBooked IS NULL)
GROUP BY ATCWorldToArtShipData.SDONo, ATCWorldToArtShipData.OurStyleId, ATCWorldToArtShipData.BuyerStyle, AtcDetails.OurStyle, AtcMaster.AtcNum
ORDER BY ATCWorldToArtShipData.OurStyleId
"></asp:SqlDataSource></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>

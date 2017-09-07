<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AccountsDashBoardBoot.aspx.cs" Inherits="ArtWebApp.Accounts.AccountsDashBoardBoot" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    
    <script src="../Scripts/jquery-3.1.1.js"></script>
    <script src="../Scripts/bootstrap.min.js"></script>
    <link href="../css/bootstrap.min.css" rel="stylesheet" />
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
  <h2>Accounts DashBoard</h2>
  <p>Click on the button to toggle between showing and hiding content.</p>
  <button type="button" class="btn btn-info" data-toggle="collapse" data-target="#PendingApprovalDiv">Pending General Po for Payable</button>
  <div id="PendingApprovalDiv" class="collapse in" >
       <asp:GridView ID="GridView4" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered " datasourceid="PendingSpotoInvoice" Font-Size="Smaller" HeaderStyle-CssClass="header" PagerStyle-CssClass="pager" RowStyle-CssClass="rows" DataKeyNames="SPO_Pk">
                                <Columns>
                                    <asp:BoundField DataField="SPO_Pk" HeaderText="SPO_Pk" SortExpression="SPO_Pk" InsertVisible="False" ReadOnly="True" />
                                    <asp:BoundField DataField="SupplierName" HeaderText="SupplierName" SortExpression="SupplierName" />
                                    <asp:BoundField DataField="SPONum" HeaderText="SPONum" SortExpression="SPONum" />
                                    <asp:BoundField DataField="POQty" HeaderText="POQty" SortExpression="POQty" ReadOnly="True" />
                                    <asp:BoundField DataField="ReceivedQty" HeaderText="ReceivedQty" SortExpression="ReceivedQty" ReadOnly="True" />
                                    <asp:BoundField DataField="ExtraQty" HeaderText="ExtraQty" SortExpression="ExtraQty" ReadOnly="True" />
                                    <asp:BoundField DataField="MRNDate" HeaderText="MRNDate" ReadOnly="True" SortExpression="MRNDate" />
                                    <asp:BoundField DataField="InvoicedQty" HeaderText="InvoicedQty" ReadOnly="True" SortExpression="InvoicedQty" />
                                    <asp:BoundField DataField="BancetoInvoice" HeaderText="BancetoInvoice" ReadOnly="True" SortExpression="BancetoInvoice" />
                                </Columns>
                                <HeaderStyle CssClass="header" />
                                <PagerStyle CssClass="pager" />
                                <RowStyle CssClass="rows" />
                            </asp:GridView>     
       <asp:SqlDataSource ID="PendingSpotoInvoice" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="


SELECT        SPO_Pk, SupplierName, SPONum, POQty, ReceivedQty, ExtraQty, MRNDate, InvoicedQty, ReceivedQty - InvoicedQty AS BancetoInvoice
FROM            (SELECT        StockPOMaster.SPO_Pk, StockPOMaster.SPONum, ISNULL(SUM(StockPODetails_1.POQty), 0) AS POQty, ISNULL(SUM(StockMRNDetails.ReceivedQty), 0) AS ReceivedQty, SupplierMaster.SupplierName, 
                         MAX(StockMrnMaster.AddedDate) AS MRNDate, ISNULL(SUM(StockMRNDetails.ExtraQty), 0) AS ExtraQty, ISNULL
                             ((SELECT        SUM(SupplierStockInvoiceDetail.InvoiceQty) AS Expr1
                                 FROM            StockPODetails INNER JOIN
                                                          SupplierStockInvoiceDetail ON StockPODetails.SPODetails_PK = SupplierStockInvoiceDetail.SPODetails_PK AND 
                                                          StockPODetails.SPODetails_PK = SupplierStockInvoiceDetail.SPODetails_PK
                                 GROUP BY StockPODetails.SPO_PK
                                 HAVING        (StockPODetails.SPO_PK = StockPOMaster.SPO_Pk)), 0) AS InvoicedQty, StockPOMaster.IsApproved, StockPOMaster.MarkCompleted
FROM            StockPOMaster INNER JOIN
                         StockPODetails AS StockPODetails_1 ON StockPOMaster.SPO_Pk = StockPODetails_1.SPO_PK INNER JOIN
                         StockMRNDetails ON StockPOMaster.SPO_Pk = StockMRNDetails.SPO_PK INNER JOIN
                         StockMrnMaster ON StockPOMaster.SPO_Pk = StockMrnMaster.SPo_PK INNER JOIN
                         SupplierMaster ON StockPOMaster.Supplier_Pk = SupplierMaster.Supplier_PK
GROUP BY StockPOMaster.SPO_Pk, StockPOMaster.SPONum, SupplierMaster.SupplierName, StockPOMaster.IsApproved, StockPOMaster.MarkCompleted
HAVING        (StockPOMaster.IsApproved = N'Y') AND (StockPOMaster.MarkCompleted = N'N')) AS tt
WHERE        (ReceivedQty - InvoicedQty &gt; 0)"></asp:SqlDataSource>
  </div>

    <button type="button" class="btn btn-info" data-toggle="collapse" data-target="#PendingpatternDiv">Pending PO for Payable</button>
  <div id="PendingpatternDiv" class="collapse in" >
         <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered" DataSourceID="PendingPoforinvoicing" Font-Size="Smaller" HeaderStyle-CssClass="header" PagerStyle-CssClass="pager" RowStyle-CssClass="rows" DataKeyNames="PO_Pk">
                                <Columns>
                                    <asp:BoundField DataField="PO_Pk" HeaderText="PO_Pk" SortExpression="PO_Pk" InsertVisible="False" ReadOnly="True" />
                                    <asp:BoundField DataField="PONum" HeaderText="PONum" SortExpression="PONum" />
                                    <asp:BoundField DataField="SupplierName" HeaderText="SupplierName" SortExpression="SupplierName" />
                                    <asp:BoundField DataField="POQty" HeaderText="POQty" SortExpression="POQty" ReadOnly="True" />
                                    <asp:BoundField DataField="ReceiptQty" HeaderText="ReceiptQty" SortExpression="ReceiptQty" ReadOnly="True" />
                                    <asp:BoundField DataField="invoiced" HeaderText="invoiced" SortExpression="invoiced" ReadOnly="True" />
                                    <asp:BoundField DataField="LastMRNDATE" HeaderText="LastMRNDATE" ReadOnly="True" SortExpression="LastMRNDATE" />
                                    <asp:BoundField DataField="BalToInvoice" HeaderText="BalToInvoice" ReadOnly="True" SortExpression="BalToInvoice" />
                                    <asp:BoundField DataField="PaymentTermCode" HeaderText="PaymentTermCode" SortExpression="PaymentTermCode" />
                                </Columns>
                                <HeaderStyle CssClass="header" />
                                <PagerStyle CssClass="pager" />
                                <RowStyle CssClass="rows" />
                            </asp:GridView>

                            <asp:SqlDataSource ID="PendingPoforinvoicing" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT        TT.PO_Pk, TT.PONum, SupplierMaster.SupplierName, TT.POQty, TT.ReceiptQty, TT.invoiced, TT.LastMRNDATE, TT.ReceiptQty - TT.invoiced AS BalToInvoice, PaymentTermMaster.PaymentTermCode
FROM            (SELECT        ProcurementMaster.PO_Pk, SUM(ProcurementDetails_1.POQty) AS POQty, ISNULL
                                                        ((SELECT        SUM(MrnDetails.ReceiptQty) AS Expr1
                                                            FROM            MrnDetails INNER JOIN
                                                                                     ProcurementDetails ON MrnDetails.PODet_PK = ProcurementDetails.PODet_PK
                                                            WHERE        (ProcurementDetails.PO_Pk = ProcurementMaster.PO_Pk)), 0) AS ReceiptQty, ISNULL
                                                        ((SELECT        SUM(SupplierInvoiceDetail.InvoiceQty) AS Expr1
                                                            FROM            SupplierInvoiceDetail INNER JOIN
                                                                                     ProcurementDetails AS ProcurementDetails_2 ON SupplierInvoiceDetail.PODet_PK = ProcurementDetails_2.PODet_PK
                                                            GROUP BY ProcurementDetails_2.PO_Pk
                                                            HAVING        (ProcurementDetails_2.PO_Pk = ProcurementMaster.PO_Pk)), 0) AS invoiced,
                                                        (SELECT        MAX(MrnMaster.AddedDate) AS Expr1
                                                          FROM            MrnMaster INNER JOIN
                                                                                    MrnDetails AS MrnDetails_1 ON MrnMaster.Mrn_PK = MrnDetails_1.Mrn_PK
                                                          GROUP BY MrnMaster.Po_PK
                                                          HAVING         (MrnMaster.Po_PK = ProcurementMaster.PO_Pk)) AS LastMRNDATE, ProcurementMaster.IsNormal, ProcurementMaster.IsApproved, ProcurementMaster.IsDeleted, 
                                                    ProcurementMaster.MarkCompleted, ProcurementMaster.PONum, ProcurementMaster.Supplier_Pk, ProcurementMaster.PaymentTermID
                          FROM            ProcurementMaster INNER JOIN
                                                    ProcurementDetails AS ProcurementDetails_1 ON ProcurementMaster.PO_Pk = ProcurementDetails_1.PO_Pk
                          GROUP BY ProcurementMaster.PO_Pk, ProcurementMaster.IsNormal, ProcurementMaster.IsApproved, ProcurementMaster.IsDeleted, ProcurementMaster.MarkCompleted, ProcurementMaster.PONum, 
                                                    ProcurementMaster.Supplier_Pk, ProcurementMaster.PaymentTermID
                          HAVING         (ProcurementMaster.IsNormal = N'Y') AND (ProcurementMaster.IsApproved = N'Y') AND (ProcurementMaster.IsDeleted = N'N') AND (ProcurementMaster.Supplier_Pk <> 1113)AND (ProcurementMaster.MarkCompleted = N'N')) AS TT INNER JOIN
                         SupplierMaster ON TT.Supplier_Pk = SupplierMaster.Supplier_PK INNER JOIN
                         PaymentTermMaster ON TT.PaymentTermID = PaymentTermMaster.PaymentTermID
WHERE        (TT.ReceiptQty - TT.invoiced &gt; 0)">
                            </asp:SqlDataSource>
  </div>




       <button type="button" class="btn btn-info" data-toggle="collapse" data-target="#PendingCutorderDiv">Pending service Po for Posting</button>
  <div id="PendingCutorderDiv" class="collapse in" >
          <asp:GridView ID="GridView5" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered" datasourceid="PendingServicePO" Font-Size="Smaller" HeaderStyle-CssClass="header" PagerStyle-CssClass="pager" RowStyle-CssClass="rows" DataKeyNames="ServicePO_PK">
                                <Columns>
                                    <asp:BoundField DataField="ServicePO_PK" HeaderText="ServicePO_PK" SortExpression="ServicePO_PK" InsertVisible="False" ReadOnly="True" />
                                    <asp:BoundField DataField="ServicePOnumber" HeaderText="ServicePOnumber" SortExpression="ServicePOnumber" />
                                    <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description" />
                                    <asp:BoundField DataField="DebitFrom" HeaderText="DebitFrom" SortExpression="DebitFrom" />
                                    <asp:BoundField DataField="DebitName" HeaderText="DebitName" SortExpression="DebitName" />
                                    <asp:BoundField DataField="ServiceType" HeaderText="ServiceType" SortExpression="ServiceType" />
                                    <asp:BoundField DataField="Amount" HeaderText="Amount" SortExpression="Amount" />
                                    <asp:BoundField DataField="CurrencyCode" HeaderText="CurrencyCode" SortExpression="CurrencyCode" />
                                    <asp:BoundField DataField="IsApproved" HeaderText="IsApproved" SortExpression="IsApproved" />
                                </Columns>
                                <HeaderStyle CssClass="header" />
                                <PagerStyle CssClass="pager" />
                                <RowStyle CssClass="rows" />
                            </asp:GridView>
                            <asp:SqlDataSource ID="PendingServicePO" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT        ServicePOMaster.ServicePO_PK, ServicePOMaster.ServicePOnumber, ServicePOMaster.Description, ServicePOMaster.DebitFrom, ServicePOMaster.DebitName, ServiceTypeMaster.ServiceType, 
                         ServicePOMaster.Amount, CurrencyMaster.CurrencyCode, ServicePOMaster.IsApproved
FROM            ServicePOMaster INNER JOIN
                         ServiceTypeMaster ON ServicePOMaster.ServiceType_Pk = ServiceTypeMaster.ServiceType_Pk INNER JOIN
                         CurrencyMaster ON ServicePOMaster.CurrencyID = CurrencyMaster.CurrencyID
WHERE        (ServicePOMaster.IsApproved = N'Y') AND (ServicePOMaster.IsPosted = N'N')"></asp:SqlDataSource>
                              

                            
  </div>




           <button type="button" class="btn btn-info" data-toggle="collapse" data-target="#PendingASQDiv">Pending Local External Sales for Posting</button>
  <div id="PendingASQDiv" class="collapse in" >
                       
        <asp:GridView ID="GridView6" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered" DataKeyNames="SalesDO_PK" DataSourceID="ExternalPendingSales" Font-Size="Smaller" HeaderStyle-CssClass="header" PagerStyle-CssClass="pager" RowStyle-CssClass="rows">
                                <Columns>
                                    <asp:BoundField DataField="SalesDO_PK" HeaderText="SalesDO_PK" SortExpression="SalesDO_PK" InsertVisible="False" ReadOnly="True" />
                                    <asp:BoundField DataField="SalesDONum" HeaderText="SalesDONum" SortExpression="SalesDONum" />
                                    <asp:BoundField DataField="SalesDate" HeaderText="SalesDate" SortExpression="SalesDate" />
                                    <asp:BoundField DataField="SalesDODate" HeaderText="SalesDODate" SortExpression="SalesDODate" />
                                    <asp:BoundField DataField="FromLocation" HeaderText="FromLocation" SortExpression="FromLocation" />
                                    <asp:BoundField DataField="Buyer" HeaderText="Buyer" SortExpression="Buyer" />
                                </Columns>
                                <HeaderStyle CssClass="header" />
                                <PagerStyle CssClass="pager" />
                                <RowStyle CssClass="rows" />
                            </asp:GridView>
                            <asp:SqlDataSource ID="ExternalPendingSales" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT        InventorySalesMaster.SalesDO_PK, InventorySalesMaster.SalesDONum, InventorySalesMaster.SalesDate, InventorySalesMaster.SalesDODate, LocationMaster.LocationName AS FromLocation, 
                         LocalBuyerMaster.LocalBuyerName AS Buyer
FROM            InventorySalesMaster INNER JOIN
                         LocationMaster ON InventorySalesMaster.FromLocation_PK = LocationMaster.Location_PK INNER JOIN
                         LocalBuyerMaster ON InventorySalesMaster.ToLocation_PK = LocalBuyerMaster.LocalBuyer_PK
WHERE        (InventorySalesMaster.DoType = N'External') AND (InventorySalesMaster.IsDebited = N'N')">
                            </asp:SqlDataSource>
                            
  </div>


           <button type="button" class="btn btn-info" data-toggle="collapse" data-target="#PendingRatioDiv">Pending Internal Sales For Debiting</button>
  <div id="PendingRatioDiv" class="collapse in" >
                       
   <asp:GridView ID="PendingRatioGrid" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered" Font-Size="Smaller" HeaderStyle-CssClass="header" PagerStyle-CssClass="pager" RowStyle-CssClass="rows" DataKeyNames="SalesDO_PK" DataSourceID="InternalSalespending">
                                <Columns>
                                    <asp:BoundField DataField="SalesDO_PK" HeaderText="SalesDO_PK" SortExpression="SalesDO_PK" InsertVisible="False" ReadOnly="True" />
                                    <asp:BoundField DataField="SalesDONum" HeaderText="SalesDONum" SortExpression="SalesDONum" />
                                    <asp:BoundField DataField="SalesDate" HeaderText="SalesDate" SortExpression="SalesDate" />
                                    <asp:BoundField DataField="SalesDODate" HeaderText="SalesDODate" SortExpression="SalesDODate" />
                                    <asp:BoundField DataField="FromLocation" HeaderText="FromLocation" SortExpression="FromLocation" />
                                    <asp:BoundField DataField="ToLocation" HeaderText="ToLocation" SortExpression="ToLocation" />
                                    <asp:BoundField DataField="TotalValueinUSD" HeaderText="TotalValueinUSD" ReadOnly="True" SortExpression="TotalValueinUSD" />
                                </Columns>
                                <HeaderStyle CssClass="header" />
                                <PagerStyle CssClass="pager" />
                                <RowStyle CssClass="rows" />
                            </asp:GridView>
                            <asp:SqlDataSource ID="InternalSalespending" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT        InventorySalesMaster.SalesDO_PK, InventorySalesMaster.SalesDONum, InventorySalesMaster.SalesDate, InventorySalesMaster.SalesDODate, LocationMaster.LocationName AS FromLocation, 
                         LocationMaster_1.LocationName AS ToLocation, SUM(InventorySalesDetail.DeliveryQty * InventorySalesDetail.CuRate) AS TotalValueinUSD 
FROM            InventorySalesMaster INNER JOIN
                         LocationMaster ON InventorySalesMaster.FromLocation_PK = LocationMaster.Location_PK INNER JOIN
                         LocationMaster AS LocationMaster_1 ON InventorySalesMaster.FromLocation_PK = LocationMaster_1.Location_PK INNER JOIN
                         InventorySalesDetail ON InventorySalesMaster.SalesDO_PK = InventorySalesDetail.SalesDO_PK
WHERE        (InventorySalesMaster.DoType = N'Internal') AND (InventorySalesMaster.IsDebited = N'N')
GROUP BY InventorySalesMaster.SalesDO_PK, InventorySalesMaster.SalesDONum, InventorySalesMaster.SalesDate, InventorySalesMaster.SalesDODate, LocationMaster.LocationName, LocationMaster_1.LocationName"></asp:SqlDataSource>
                              
  </div>


    
           <button type="button" class="btn btn-info" data-toggle="collapse" data-target="#PendingRollDiv">Pending Debit note For Posting</button>
  <div id="PendingRollDiv" class="collapse in" >
                       
   <asp:GridView ID="PendingRollGrid" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered" Font-Size="Smaller" HeaderStyle-CssClass="header" PagerStyle-CssClass="pager" RowStyle-CssClass="rows">
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
                            <asp:SqlDataSource ID="PendingRoll" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT        CutPlanMaster.CutPlanNUM, AtcDetails.OurStyle, CutPlanMaster.ColorName, CutPlanMaster.FabDescription, LocationMaster.LocationName, CutPlanMaster.MarkerType, CutPlanMaster.IsApproved, 
                         CutPlanMaster.IsRatioAdded, CutPlanMaster.AddedBy, CutPlanMaster.IsPatternAdded, CutPlanMaster.IsCutorderGiven
FROM            CutPlanMaster INNER JOIN
                         AtcDetails ON CutPlanMaster.OurStyleID = AtcDetails.OurStyleID INNER JOIN
                         LocationMaster ON CutPlanMaster.Location_PK = LocationMaster.Location_PK
WHERE        (CutPlanMaster.IsRatioAdded = N'Y') AND (CutPlanMaster.IsRollAdded = N'N') "></asp:SqlDataSource>
                              
  </div>



</div>
    </form>
</body>
</html>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GPODashBoard.aspx.cs" Inherits="ArtWebApp.Merchandiser.PO.GPODashBoard" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
       <script src="../../Scripts/jquery-3.1.1.js"></script>
    <script src="../../Scripts/bootstrap.min.js"></script>
 
    <link href="../../css/bootstrap.min.css" rel="stylesheet" />    
    <script src="https://cdn.datatables.net/1.10.15/js/jquery.dataTables.min.js"></script>
<script src="https://cdn.datatables.net/1.10.15/js/dataTables.bootstrap.min.js"></script>

</head>
<body>
    <form id="form1" runat="server">
      
            <div class="container">
  <h2>Accounts DashBoard</h2>
  <p>Click on the button to toggle between showing and hiding content.</p>
  <button type="button" class="btn btn-info" data-toggle="collapse" data-target="#PendingApprovalDiv">Pending General Po for PO</button>
  <div id="PendingApprovalDiv" class="collapse in" >
  <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="mydatagrid" DataSourceID="PendingOrder" Font-Size="Smaller" HeaderStyle-CssClass="header" OnPageIndexChanging="GridView1_PageIndexChanging" PagerStyle-CssClass="pager" RowStyle-CssClass="rows" PageSize="25" ShowFooter="True">
                                <Columns>
                                      <asp:TemplateField>  
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="checkAll" runat ="server" onclick="checkAll(this)"/>
                                    </HeaderTemplate>                                 
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chk_select" runat="server" onclick="Check_Click(this)"/>
                                    </ItemTemplate>
                                </asp:TemplateField> 
                                       <asp:BoundField DataField="POId" HeaderText="POId" SortExpression="POId" />
                                      <asp:TemplateField HeaderText="POLineID" SortExpression="POLineID">
                                        
                                          <ItemTemplate>
                                              <asp:Label ID="lbl_polineid" runat="server" Text='<%# Bind("POLineID") %>'></asp:Label>
                                          </ItemTemplate>
                                      </asp:TemplateField>
                               
                                    <asp:BoundField DataField="PONum" HeaderText="PONum" SortExpression="PONum" />
                                    <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description" />
                                    <asp:BoundField DataField="PO_Date" HeaderText="PO_Date" SortExpression="PO_Date" />
                                    <asp:BoundField DataField="Qty" HeaderText="Qty" SortExpression="Qty" />
                                    <asp:BoundField DataField="Odoo_UOM" HeaderText="Odoo_UOM" SortExpression="Odoo_UOM" />
                                    <asp:BoundField DataField="OrderedQty" HeaderText="OrderedQty" ReadOnly="True" SortExpression="OrderedQty" />
                                    <asp:BoundField DataField="Balance" HeaderText="Balance" ReadOnly="True" SortExpression="Balance" />
                                    <asp:BoundField DataField="PendingFor" HeaderText="PendingFor" ReadOnly="True" SortExpression="PendingFor" />
                                    <asp:ButtonField CommandName="Cancel" HeaderText="OrderNow" ShowHeader="True" Text="OrderNow" />
                                </Columns>

<HeaderStyle CssClass="header"></HeaderStyle>

<PagerStyle CssClass="pager"></PagerStyle>

<RowStyle CssClass="rows"></RowStyle>
                            </asp:GridView>    
       <asp:SqlDataSource ID="PendingOrder" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT        PONum, POId, Description, Qty, PO_Date, Odoo_UOM, OrderedQty, Qty - OrderedQty AS Balance, DATEDIFF(day, PO_Date, GETDATE()) AS PendingFor, POLineID
FROM            (SELECT        ODOOGPOMaster.PONum, ODOOGPOMaster.POId, ODOOGPOMaster.Description, ODOOGPOMaster.Qty, ODOOGPOMaster.PO_Date, ODOOGPOMaster.Odoo_UOM, 
                                                    ISNULL(SUM(StocPOForODOO.POQty), 0) AS OrderedQty, ODOOGPOMaster.POLineID
                          FROM            ODOOGPOMaster LEFT OUTER JOIN
                                                    StocPOForODOO ON ODOOGPOMaster.POId = StocPOForODOO.POId AND ODOOGPOMaster.POLineID = StocPOForODOO.POLineID
                          GROUP BY ODOOGPOMaster.PONum, ODOOGPOMaster.POId, ODOOGPOMaster.Description, ODOOGPOMaster.Qty, ODOOGPOMaster.PO_Date, ODOOGPOMaster.Odoo_UOM, ODOOGPOMaster.POLineID) 
                         AS tt
WHERE        (Qty - OrderedQty &gt; 0)"></asp:SqlDataSource>
  </div>

    <button type="button" class="btn btn-info" data-toggle="collapse" data-target="#PendingpatternDiv">Pending PO for Receipt</button>
  <div id="PendingpatternDiv" class="collapse in" >
      
                                <asp:GridView ID="tbl_pendingtoreceive" runat="server" AutoGenerateColumns="False" CssClass="mydatagrid" DataKeyNames="SPO_Pk" DataSourceID="PendingtoReceive" Font-Size="Smaller" HeaderStyle-CssClass="header" OnPageIndexChanging="GridView1_PageIndexChanging" PagerStyle-CssClass="pager" PageSize="20" RowStyle-CssClass="rows" ShowFooter="True">
                                    <Columns>
                                        <asp:BoundField DataField="SPO_Pk" HeaderText="SPO_Pk" InsertVisible="False" ReadOnly="True" SortExpression="SPO_Pk" />
                                        <asp:BoundField DataField="SupplierName" HeaderText="SupplierName" SortExpression="SupplierName" />
                                        <asp:BoundField DataField="SPONum" HeaderText="SPONum" SortExpression="SPONum" />
                                        <asp:BoundField DataField="itemDescription" HeaderText="itemDescription" ReadOnly="True" SortExpression="itemDescription" />
                                        <asp:BoundField DataField="Unitprice" HeaderText="Unitprice" SortExpression="Unitprice" />
                                        <asp:BoundField DataField="POQty" HeaderText="POQty" ReadOnly="True" SortExpression="POQty" />
                                        <asp:BoundField DataField="ReceivedQty" HeaderText="ReceivedQty" ReadOnly="True" SortExpression="ReceivedQty" />
                                        <asp:BoundField DataField="ExtraQty" HeaderText="ExtraQty" ReadOnly="True" SortExpression="ExtraQty" />
                                        <asp:BoundField DataField="BalanceToReceive" HeaderText="BalanceToReceive" ReadOnly="True" SortExpression="BalanceToReceive" />
                                    </Columns>
                                    <HeaderStyle CssClass="header" />
                                    <PagerStyle CssClass="pager" />
                                    <RowStyle CssClass="rows" />
                                </asp:GridView>
                                <asp:SqlDataSource ID="PendingtoReceive" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT        SPO_Pk, SupplierName, SPONum, itemDescription, Unitprice, POQty, ReceivedQty, ExtraQty, POQty - ReceivedQty AS BalanceToReceive
FROM            (SELECT        StockPOMaster.SPO_Pk, StockPOMaster.SPONum, ISNULL(Template_Master.Description, N'') + ISNULL(StockPODetails.Composition, N'') + ISNULL(StockPODetails.Construct, N'') 
                                                    + ISNULL(StockPODetails.TemplateColor, N'') + ISNULL(StockPODetails.TemplateWidth, N'') + ISNULL(StockPODetails.TemplateWeight, N'') AS itemDescription, StockPODetails.Unitprice, 
                                                    SUM(StockPODetails.POQty) AS POQty, SUM(StockMRNDetails.ReceivedQty) AS ReceivedQty, SUM(StockMRNDetails.ExtraQty) AS ExtraQty, SupplierMaster.SupplierName
                          FROM            StockPOMaster INNER JOIN
                                                    StockPODetails ON StockPOMaster.SPO_Pk = StockPODetails.SPO_PK INNER JOIN
                                                    StockMRNDetails ON StockPODetails.SPODetails_PK = StockMRNDetails.SPODetails_PK INNER JOIN
                                                    SupplierMaster ON StockPOMaster.Supplier_Pk = SupplierMaster.Supplier_PK INNER JOIN
                                                    Template_Master ON StockPODetails.Template_PK = Template_Master.Template_PK INNER JOIN
                                                    UserMaster ON StockPOMaster.AddedBy = UserMaster.UserName
                          GROUP BY StockPOMaster.SPO_Pk, StockPOMaster.SPONum, StockPODetails.Composition, StockPODetails.Construct, StockPODetails.TemplateColor, StockPODetails.TemplateWidth, 
                                                    StockPODetails.TemplateWeight, StockPODetails.Unitprice, SupplierMaster.SupplierName, Template_Master.Description, Template_Master.ItemGroup_PK, StockPOMaster.AddedBy, 
                                                    UserMaster.Department_PK
                         ) AS tt
WHERE        (POQty - ReceivedQty &gt; 0)">
                                    
                                </asp:SqlDataSource>


  </div>







</div>
       
    </form>
</body>
</html>

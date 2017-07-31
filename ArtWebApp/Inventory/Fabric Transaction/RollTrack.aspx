<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RollTrack.aspx.cs" Inherits="ArtWebApp.Inventory.Fabric_Transaction.RollTrack" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

      <script src="../../Scripts/jquery-3.1.1.min.js"></script>  
  <link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/v/dt/dt-1.10.15/datatables.min.css"/>

    <script type="text/javascript" src="https://cdn.datatables.net/v/dt/dt-1.10.15/datatables.min.js"></script>
<script type="text/javascript" src="https://cdn.datatables.net/v/dt/dt-1.10.15/datatables.min.js"></script>

    <script src="../../Scripts/jquery.table2excel.js"></script>
    <script src="../../JQuery/ExporttoExcel.js"></script>
<script type="text/javascript" charset="utf-8">
   
  
       
    $(document).ready(function () {
     



        $('#<%=GridView1.ClientID%>').dataTable({
            aLengthMenu: [
                [25, 50, 100, 200, -1],
                [25, 50, 100, 200, "All"]
            ],
            iDisplayLength: -1
        });
    });

  </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    <div>
        <table class="DataEntryTable">
        <tr>
            <td class="NormalTD">Enter Any Unique ID</td>
            <td class="NormalTD">
                <asp:TextBox ID="txt_pk" runat="server"></asp:TextBox>
            </td>
            <td class="NormalTD">
                <asp:Button ID="Button1" runat="server" Text="I Entered SKU " OnClick="Button1_Click" />
            </td>
            <td class="NormalTD">
                <asp:Button ID="Button2" runat="server" Text="I Entered ASN" OnClick="Button2_Click" />
            </td>
            <td class="NormalTD"></td>
            <td class="NormalTD">
                <asp:Button ID="Button3" runat="server" OnClick="Button3_Click" Text="I Entered RollPK" />
            </td>
        </tr>
        <tr>
            <td class="NormalTD">&nbsp;</td>
            <td class="NormalTD">&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
    </table>
    </div>
 
    <div>
        <asp:GridView ID="GridView1" CssClass="mydatagrid" runat="server"  AutoGenerateColumns="False"  BackColor="#CCCCCC" BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellPadding="4" CellSpacing="2" DataKeyNames="Roll_PK" ForeColor="Black" ShowFooter="True">
            <Columns>
                <asp:BoundField DataField="Roll_PK" HeaderText="Roll_PK" InsertVisible="False" ReadOnly="True" SortExpression="Roll_PK" />
                <asp:BoundField DataField="RollNum" HeaderText="RollNum" SortExpression="RollNum" />
                  <asp:BoundField DataField="itemDescription" HeaderText="itemDescription" SortExpression="itemDescription" />
                
                <asp:BoundField DataField="ASN" HeaderText="ASN" ReadOnly="True" SortExpression="ASN" />
                <asp:BoundField DataField="LocationName" HeaderText="LocationName" SortExpression="LocationName" />
                <asp:BoundField DataField="DocumentNum" HeaderText="DocumentNum" SortExpression="DocumentNum" />
                <asp:BoundField DataField="AddedVia" HeaderText="AddedVia" SortExpression="AddedVia" />
                <asp:BoundField DataField="DeliveredVia" HeaderText="DeliveredVia" SortExpression="DeliveredVia" />
                <asp:BoundField DataField="AYard" HeaderText="AYard" SortExpression="AYard" />
                <asp:BoundField DataField="MarkerType" HeaderText="MarkerType" SortExpression="MarkerType" />
                <asp:BoundField DataField="WidthGroup" HeaderText="WidthGroup" SortExpression="WidthGroup" />
                <asp:BoundField DataField="ShadeGroup" HeaderText="ShadeGroup" SortExpression="ShadeGroup" />
                <asp:BoundField DataField="ShrinkageGroup" HeaderText="ShrinkageGroup" SortExpression="ShrinkageGroup" />
                <asp:BoundField DataField="IsCut" HeaderText="IsCut" SortExpression="IsCut" />
                <asp:BoundField DataField="IsPresent" HeaderText="IsPresent" SortExpression="IsPresent" />
                <asp:BoundField DataField="Location_Pk" HeaderText="Location_Pk" SortExpression="Location_Pk" />
                <asp:BoundField DataField="SkuDet_PK" HeaderText="SkuDet_PK" SortExpression="SkuDet_PK" />
                <asp:BoundField DataField="IsDelivered" HeaderText="IsDelivered" SortExpression="IsDelivered" />
                 <asp:BoundField DataField="LaysheetNUM" HeaderText="LaysheetNUM" SortExpression="LaysheetNUM" />
                <asp:BoundField DataField="CutPlanNUM" HeaderText="CutPlanNUM" SortExpression="CutPlanNUM" />
            </Columns>
            <FooterStyle BackColor="#CCCCCC" />
            <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Left" />
            <RowStyle BackColor="White" />
            <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F1F1F1" />
            <SortedAscendingHeaderStyle BackColor="#808080" />
            <SortedDescendingCellStyle BackColor="#CAC9C9" />
            <SortedDescendingHeaderStyle BackColor="#383838" />
        </asp:GridView>

        <asp:SqlDataSource ID="RollDatasource" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT        FabricRollmaster.Roll_PK, FabricRollmaster.RollNum, LocationMaster.LocationName, RollInventoryMaster.DocumentNum, RollInventoryMaster.AddedVia, RollInventoryMaster.DeliveredVia, 
                         FabricRollmaster.AYard, FabricRollmaster.MarkerType, FabricRollmaster.WidthGroup, FabricRollmaster.ShadeGroup, FabricRollmaster.ShrinkageGroup, FabricRollmaster.IsCut, RollInventoryMaster.IsPresent, 
                         RollInventoryMaster.Location_Pk, FabricRollmaster.SkuDet_PK, FabricRollmaster.IsDelivered, SupplierDocumentMaster.SupplierDocnum+'/'+SupplierDocumentMaster.AtracotrackingNum as ASN
FROM            FabricRollmaster INNER JOIN
                         RollInventoryMaster ON FabricRollmaster.Roll_PK = RollInventoryMaster.Roll_PK INNER JOIN
                         LocationMaster ON RollInventoryMaster.Location_Pk = LocationMaster.Location_PK INNER JOIN
                         SupplierDocumentMaster ON FabricRollmaster.SupplierDoc_pk = SupplierDocumentMaster.SupplierDoc_pk
WHERE        (FabricRollmaster.SkuDet_PK = @param1)">
            <SelectParameters>
                <asp:ControlParameter ControlID="txt_pk" Name="param1" PropertyName="Text" />
            </SelectParameters>
        </asp:SqlDataSource>

    </div>
       
    </div>
    </form>
</body>
</html>

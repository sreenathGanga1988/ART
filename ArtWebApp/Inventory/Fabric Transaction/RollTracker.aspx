<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="RollTracker.aspx.cs" Inherits="ArtWebApp.Inventory.Fabric_Transaction.RollTracker" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
       
    </style>
    <link href="../../css/style.css" rel="stylesheet" />
   <link rel="stylesheet" href="https://cdn.datatables.net/1.10.13/css/jquery.dataTables.min.css" />
<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.1.1/jquery.min.js"></script>
    <script type="text/javascript" src="http://cdn.datatables.net/1.10.13/js/jquery.dataTables.min.js"></script>

     <script type="text/javascript" charset="utf-8">
         //$(document).ready(function () {
         //    debugger;
         //    $("#GridView1").prepend($("<thead></thead>").append($(this).find("tr:first"))).DataTable();
         //});
         $(document).ready(function () {
             $('#<%=GridView1.ClientID%>').DataTable({
                 initComplete: function () {
                     this.api().columns().every(function () {
                         var column = this;
                         var select = $('<select><option value=""></option></select>')
                             .appendTo($(column.footer()).empty())
                             .on('change', function () {
                                 var val = $.fn.dataTable.util.escapeRegex(
                                     $(this).val()
                                 );

                                 column
                                     .search(val ? '^' + val + '$' : '', true, false)
                                     .draw();
                             });

                         column.data().unique().sort().each(function (d, j) {
                             select.append('<option value="' + d + '">' + d + '</option>')
                         });
                     });
                 }
             });
         });
        <%-- $(function(){

    $('#<%=GridView1.ClientID%>').dataTable();
});--%>

        </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

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
            <td class="NormalTD"></td>
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
        <asp:GridView ID="GridView1" CssClass="gvv" runat="server"  AutoGenerateColumns="False" BackColor="#CCCCCC" BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellPadding="4" CellSpacing="2" DataKeyNames="Roll_PK" ForeColor="Black" ShowFooter="True">
            <Columns>
                <asp:BoundField DataField="Roll_PK" HeaderText="Roll_PK" InsertVisible="False" ReadOnly="True" SortExpression="Roll_PK" />
                <asp:BoundField DataField="RollNum" HeaderText="RollNum" SortExpression="RollNum" />
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
       
</asp:Content>

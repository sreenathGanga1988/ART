<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="ExternalSalesInvoiceing.aspx.cs" Inherits="ArtWebApp.Shipping.Externalsales.ExternalSalesInvoiceing" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link href="../../css/style.css" rel="stylesheet" />
 
          <script type="text/javascript" >

      
              function calculatesumofyardage() {
                  var gridView = document.getElementById("<%= tbl_podata.ClientID %>");
                  var total = 0;
                   for (var i = 1; i < gridView.rows.length - 1; i++) {
                       var itemtotal = 0
                       //textbox where user enter  new curate
                       var txt_agreedcurate = gridView.rows[i].getElementsByClassName("txt_agreedcurate")[0];

                       //label where delivery qty is available
                       var lbl_deliveryQty = gridView.rows[i].getElementsByClassName("lbl_deliveryQty")[0];

                       //label where total agreed shouldbe show
                       var lbl_totalagreed = gridView.rows[i].getElementsByClassName("lbl_totalagreed")[0];

                

                     


                       itemtotal = parseFloat(txt_agreedcurate.value) * parseFloat(lbl_deliveryQty.innerHTML);

                       lbl_totalagreed.innerHTML = itemtotal;

                       total = total + parseFloat(itemtotal);
                   }
                   var txt_totalagreedFooter = document.getElementsByClassName("txt_totalagreedFooter")[0];
                   txt_totalagreedFooter.innerHTML = total;
               }



    </script>
    <style type="text/css">
        .auto-style1 {
            height: 27px;
            width: 27px;
        }
    </style>
    </asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="RedHeaddingdIV">

        &nbsp;DEBIT NOTES TO FACTORY AGAINST SALES DO FROM HO</div>
    <div>

        <table class="DataEntryTable">
            <tr>
                <td class="NormalTD">Buyer</td>
                <td class="NormalTD">
                    <ucc:DropDownListChosen ID="drp_ToWarehouse" runat="server" DataTextField="name" DataValueField="pk" DisableSearchThreshold="10" TextField="name" ValueField="pk" Width="200px">
                    </ucc:DropDownListChosen>
                </td>
           
                <td class="NormalTD">
                    <asp:Button ID="btn_Buyer" runat="server" OnClick="btn_Buyer_Click" Text="Show Sdo" />
                </td>
                <td class="NormalTD">&nbsp;</td>
                 <td class="auto-style1">
                     
                </td>
                 <td class="NormalTD">&nbsp;</td>
                 <td class="NormalTD">&nbsp;</td>
            </tr>
            <tr>
                <td class="NormalTD">SDO</td>
                 <td class="NormalTD">
                     <ucc:DropDownListChosen ID="dro_sdo" runat="server" DataTextField="name" DataValueField="pk" DisableSearchThreshold="10" TextField="name" ValueField="pk" Width="200px">
                     </ucc:DropDownListChosen>
                </td>
               
               <td class="NormalTD">
                   <asp:Button ID="btn_details" runat="server" OnClick="btn_details_Click" Text="Show Details" />
                </td>
              <td class="NormalTD">
                  &nbsp;</td>
              <td class="auto-style1">
                  &nbsp;</td>
              <td class="NormalTD">
                  &nbsp;</td>
              <td class="NormalTD">
                  
                  </td>
            </tr>
            <tr>
         <td class="NormalTD">&nbsp;</td>
                            <td class="NormalTD">
                                &nbsp;</td>
                           
                            <td class="SearchButtonTD">&nbsp;</td>
                            <td>
                                &nbsp;</td>
              <td class="auto-style1">&nbsp;</td>
              <td class="NormalTD">&nbsp;</td>
              <td class="NormalTD">&nbsp;</td>
            </tr>
        </table>

    </div>
    <div>

        <asp:GridView ID="tbl_podata" runat="server" AutoGenerateColumns="False" ShowFooter="True" BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4" DataKeyNames="SalesDODet_PK" Font-Size="Smaller" style="font-size: small; font-family: Calibri; font-weight: 400;" Width="100%" OnRowDataBound="tbl_podata_RowDataBound">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:TemplateField HeaderText="SalesDODet_PK" InsertVisible="False" SortExpression="SalesDODet_PK">
                 
                    <ItemTemplate>
                        <asp:Label ID="lbl_SalesDODet_PK" runat="server" Text='<%# Bind("SalesDODet_PK") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Description" HeaderText="Description" ReadOnly="True" SortExpression="Description" />
                <asp:BoundField DataField="CuRate" HeaderText="CuRate" SortExpression="CuRate" />
                <asp:TemplateField HeaderText="DeliveryQty" SortExpression="DeliveryQty">
                   
                    <ItemTemplate>
                        <asp:Label ID="lbl_deliveryQty"  CssClass="lbl_deliveryQty" runat="server" Text='<%# Bind("DeliveryQty") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                  <asp:TemplateField HeaderText="TotalValue" SortExpression="TotalValue">
                   <FooterTemplate>
                                                  <asp:Label ID="txt_actualtotal" CssClass="txt_actualTotal" runat="server"></asp:Label>

                                              </FooterTemplate>
                      <ItemTemplate>
                          <asp:Label ID="Label1" runat="server" Text='<%# Bind("TotalValue") %>'></asp:Label>
                      </ItemTemplate>
                </asp:TemplateField>
                  <asp:TemplateField HeaderText="Agreed value" SortExpression="CuRate">
               
                    <ItemTemplate>
                        <asp:TextBox ID="txt_agreedcurate" CssClass="txt_agreedcurate" onchange=" calculatesumofyardage()()" runat="server" Text='<%# Bind("CuRate") %>'></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>

                  <asp:TemplateField HeaderText="Agreed Total" SortExpression="TotalValue">
                   <FooterTemplate>
                                                  <asp:Label ID="txt_totalagreedFooter" CssClass="txt_totalagreedFooter" runat="server"></asp:Label>

                                              </FooterTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lbl_totalagreed" CssClass="lbl_totalagreed" runat="server" Text='<%# Bind("TotalValue") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                  <asp:BoundField DataField="SalesDONum" HeaderText="SalesDONum" SortExpression="SalesDONum" /> 
                <asp:BoundField DataField="SalesDate" HeaderText="SalesDate" SortExpression="SalesDate" />
                <asp:BoundField DataField="Remark" HeaderText="Remark" SortExpression="Remark" />
                <asp:BoundField DataField="FromLocation" HeaderText="FromLocation" SortExpression="FromLocation" />
                <asp:BoundField DataField="SalesDODate" HeaderText="SalesDODate" SortExpression="SalesDODate" />
                <asp:BoundField DataField="ContainerNumber" HeaderText="ContainerNumber" SortExpression="ContainerNumber" />
                <asp:BoundField DataField="DoType" HeaderText="DoType" SortExpression="DoType" />
                <asp:BoundField DataField="ToLocation" HeaderText="ToLocation" SortExpression="ToLocation" />
            </Columns>
            <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
            <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="#FFFFCC" />
            <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
            <RowStyle BackColor="White" ForeColor="#330099" />
            <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
            <SortedAscendingCellStyle BackColor="#FEFCEB" />
            <SortedAscendingHeaderStyle BackColor="#AF0101" />
            <SortedDescendingCellStyle BackColor="#F6F0C0" />
            <SortedDescendingHeaderStyle BackColor="#7E0000" />
        </asp:GridView>
        <asp:SqlDataSource ID="SalesDOData" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>"
            SelectCommand="SELECT Template_Master.Description + '' + StockInventoryMaster.Composition + '' + StockInventoryMaster.Construct + '' + StockInventoryMaster.TemplateColor + '' + StockInventoryMaster.TemplateSize + '' + StockInventoryMaster.TemplateWidth + '' + StockInventoryMaster.TemplateWeight AS Description, StockInventoryMaster.Unitprice, ISNULL(StockInventoryMaster.Refnum, '') + '/' + ISNULL(StockInventoryMaster.ParentRef, '') AS Refnum, StockInventoryMaster.ReceivedVia, InventorySalesMaster.AddedBy, InventorySalesMaster.AddedDate, InventorySalesDetail.SalesDO_PK, InventorySalesDetail.CuRate, InventorySalesDetail.DeliveryQty, InventorySalesMaster.SalesDONum, InventorySalesMaster.SalesDate, InventorySalesDetail.Remark, LocationMaster_1.LocationName AS FromLocation, InventorySalesMaster.SalesDODate, InventorySalesMaster.ContainerNumber, InventorySalesMaster.BoeNum, InventorySalesMaster.ISApproved, InventorySalesMaster.ApprovedBY, InventorySalesMaster.ApprovedDate, InventorySalesMaster.DoType, LocalBuyerMaster.LocalBuyerName AS ToLocation, InventorySalesDetail.SalesDODet_PK FROM Template_Master INNER JOIN StockInventoryMaster ON Template_Master.Template_PK = StockInventoryMaster.Template_PK INNER JOIN InventorySalesDetail INNER JOIN InventorySalesMaster ON InventorySalesDetail.SalesDO_PK = InventorySalesMaster.SalesDO_PK ON StockInventoryMaster.SInventoryItem_PK = InventorySalesDetail.SInventoryItem_PK INNER JOIN LocationMaster AS LocationMaster_1 ON InventorySalesMaster.FromLocation_PK = LocationMaster_1.Location_PK INNER JOIN LocalBuyerMaster ON InventorySalesMaster.ToLocation_PK = LocalBuyerMaster.LocalBuyer_PK">
            
        </asp:SqlDataSource>

    </div>
       <div> <asp:Button ID="btn_sumbit" runat="server" OnClick="btn_sumbit_Click" Text="Create Debit Note" /></div>
        <div id="Messaediv" runat="server">
                 


                           <asp:Label ID="lbl_msg" runat="server" Text="*"></asp:Label>


                     
               </div>
</asp:Content>

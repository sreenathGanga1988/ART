<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="ProcurementApproval.aspx.cs" Inherits="ArtWebApp.Approvals.ProcurementApproval" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
      
    
    <link href="../css/style.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     

    <div>


        <asp:MultiView ID="MultiView1" runat="server">
            <asp:View ID="POview" runat="server">
                <table class="DataEntryTable">
        <tr>
            <td class="RedHeadding">AUTO PO Approval</td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="tbl_podata" runat="server" AutoGenerateColumns="False" DataKeyNames="PO_pk" DataSourceID="SqlDataSource1" OnRowCommand="tbl_podata_RowCommand" BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2" OnRowDataBound="tbl_podata_RowDataBound">
                    <Columns>
                         <asp:TemplateField HeaderImageUrl="~/Image/tick.jpg">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chk_select" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        <asp:BoundField DataField="PO_PK" HeaderText="PO_PK" InsertVisible="False" ReadOnly="True" SortExpression="PO_PK" />
                        <asp:BoundField DataField="PONum" HeaderText="PONum" SortExpression="PONum" />
                        <asp:BoundField DataField="AtcNum" HeaderText="AtcNum" SortExpression="AtcNum" />
                        <asp:BoundField DataField="SupplierName" HeaderText="SupplierName" ReadOnly="True" SortExpression="SupplierName" />
                        <asp:BoundField DataField="AddedDate" HeaderText="PODate" SortExpression="AddedDate" />
                        <asp:BoundField DataField="POValue" HeaderText="POValue" SortExpression="POValue" />
                        <asp:BoundField DataField="CurrencyCode" HeaderText="CurrencyCode" SortExpression="CurrencyCode" />
                        <asp:BoundField DataField="AddedBy" HeaderText="CreatedBy" SortExpression="AddedBy" />
                        <asp:ButtonField ButtonType="Button" CommandName="Approve" HeaderText="Approve" Text="Approve" Visible="False" />
                        <asp:ButtonField CommandName="Reject" HeaderText="Delete" Text="Delete" />
                        <asp:ButtonField CommandName="Show" HeaderText="Show" Text="Show" />
                         <asp:BoundField DataField="Isforwarded" HeaderText="Isforwarded" />
                    </Columns>
                    <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
                    <HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" />
                    <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
                    <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
                    <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#FFF1D4" />
                    <SortedAscendingHeaderStyle BackColor="#B95C30" />
                    <SortedDescendingCellStyle BackColor="#F1E5CE" />
                    <SortedDescendingHeaderStyle BackColor="#93451F" />
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td>
                            <asp:Button ID="btn_approveAll" runat="server" OnClick="btn_approveAll_Click" Text="Approve All selected" />
            </td>
        </tr>
                    <tr>
                        <td>
                            <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Forward Selected Pos For Management Approval" />
                        </td>
                    </tr>
        <tr>
            <td class="auto-style7">
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT        ProcurementMaster.PO_Pk, ProcurementMaster.PONum, AtcMaster.AtcNum, SupplierMaster.SupplierName, CurrencyMaster.CurrencyCode, SUM(ProcurementDetails.POQty * ProcurementDetails.POUnitRate) 
                         AS POValue, ProcurementMaster.AddedDate, ProcurementMaster.AddedBy, ProcurementMaster.IsDeleted, AtcMaster.MerchandiserName, COUNT(POApproval.ForwardedBy) AS Isforwarded
FROM            ProcurementMaster INNER JOIN
                         SupplierMaster ON ProcurementMaster.Supplier_Pk = SupplierMaster.Supplier_PK INNER JOIN
                         AtcMaster ON ProcurementMaster.AtcId = AtcMaster.AtcId INNER JOIN
                         CurrencyMaster ON ProcurementMaster.CurrencyID = CurrencyMaster.CurrencyID INNER JOIN
                         ProcurementDetails ON ProcurementMaster.PO_Pk = ProcurementDetails.PO_Pk LEFT OUTER JOIN
                         POApproval ON ProcurementMaster.PO_Pk = POApproval.PO_PK
GROUP BY ProcurementMaster.PO_Pk, ProcurementMaster.PONum, AtcMaster.AtcNum, SupplierMaster.SupplierName, CurrencyMaster.CurrencyCode, ProcurementMaster.AddedDate, ProcurementMaster.AddedBy, 
                         ProcurementMaster.IsApproved, ProcurementMaster.IsDeleted, AtcMaster.MerchandiserName
HAVING        (ProcurementMaster.IsApproved = N'N') AND (ProcurementMaster.IsDeleted <> N'Y') "></asp:SqlDataSource>
            </td>
        </tr>
    </table>
            </asp:View>
              <asp:View ID="SPOView" runat="server">

                  <table class="DataEntryTable">
        <tr>
            <td class="RedHeadding">gENERALPO Approval</td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="tbl_generalpo" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource2" OnRowCommand="tbl_generalpo_RowCommand" BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2">
                    <Columns>
                         <asp:TemplateField HeaderImageUrl="~/Image/tick.jpg">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chk_select" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        <asp:BoundField DataField="SPO_Pk" HeaderText="SPO_PK" InsertVisible="False" ReadOnly="True" SortExpression="PO_PK" />
                        <asp:BoundField DataField="SPONum" HeaderText="PONum" SortExpression="PONum" />
                        <asp:BoundField DataField="SupplierName" HeaderText="SupplierName" ReadOnly="True" SortExpression="SupplierName" />
                        <asp:BoundField DataField="AddedDate" HeaderText="PODate" SortExpression="AddedDate" />
                        <asp:BoundField DataField="POvalue" HeaderText="POValue" SortExpression="POValue" />
                        <asp:BoundField DataField="CurrencyCode" HeaderText="CurrencyCode" SortExpression="CurrencyCode" />
                        <asp:BoundField DataField="AddedBy" HeaderText="CreatedBy" SortExpression="AddedBy" />
                        <asp:ButtonField ButtonType="Button" CommandName="Approve" HeaderText="Approve" Text="Approve" Visible="False" />
                        <asp:ButtonField CommandName="Reject" HeaderText="Delete" Text="Delete" />
                        <asp:ButtonField CommandName="Show" HeaderText="Show" Text="Show" />
                    </Columns>
                    <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
                    <HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" />
                    <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
                    <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
                    <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#FFF1D4" />
                    <SortedAscendingHeaderStyle BackColor="#B95C30" />
                    <SortedDescendingCellStyle BackColor="#F1E5CE" />
                    <SortedDescendingHeaderStyle BackColor="#93451F" />
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td>
                            <asp:Button ID="btn_spoapproval" runat="server"  Text="Approve All selected" OnClick="btn_spoapproval_Click" />
            </td>
        </tr>
                      <tr>
                          <td><asp:Button ID="btn_forwardspo" runat="server"  Text="Forward Selected Pos For Management Approval" OnClick="btn_forwardspo_Click" /></td>
                      </tr>
        <tr>
            <td class="auto-style7">
                <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT StockPOMaster.SPO_Pk, StockPOMaster.SPONum, SupplierMaster.SupplierName, StockPOMaster.DeliveryDate, StockPOMaster.AddedBy, StockPOMaster.AddedDate, SUM(StockPODetails.Unitprice * StockPODetails.POQty) AS POvalue, CurrencyMaster.CurrencyCode FROM StockPOMaster INNER JOIN StockPODetails ON StockPOMaster.SPO_Pk = StockPODetails.SPO_PK INNER JOIN SupplierMaster ON StockPOMaster.Supplier_Pk = SupplierMaster.Supplier_PK INNER JOIN CurrencyMaster ON StockPOMaster.CurrencyID = CurrencyMaster.CurrencyID GROUP BY StockPOMaster.SPO_Pk, StockPOMaster.SPONum, SupplierMaster.SupplierName, StockPOMaster.DeliveryDate, StockPOMaster.AddedBy, StockPOMaster.AddedDate, CurrencyMaster.CurrencyCode, StockPOMaster.IsApproved HAVING (StockPOMaster.IsApproved = N'N')"></asp:SqlDataSource>
            </td>
        </tr>
    </table>
            </asp:View>
              <asp:View ID="RO" runat="server">

                  <table class="DataEntryTable">
        <tr>
            <td class="RedHeadding">rO Approval</td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="tbl_ro" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource3" OnRowCommand="tbl_ro_RowCommand" BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2" DataKeyNames="RO_Pk">
                    <Columns>
                         <asp:TemplateField HeaderImageUrl="~/Image/tick.jpg">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chk_select" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        <asp:BoundField DataField="RO_Pk" HeaderText="RO_Pk" InsertVisible="False" ReadOnly="True" SortExpression="RO_Pk" />
                        <asp:BoundField DataField="RONum" HeaderText="RONum" SortExpression="RONum" />
                        <asp:BoundField DataField="FRMATC" HeaderText="FRMATC" SortExpression="FRMATC" />
                        <asp:BoundField DataField="TOATC" HeaderText="TOATC" SortExpression="TOATC" />
                        <asp:BoundField DataField="DESCRIPTION" HeaderText="DESCRIPTION" SortExpression="DESCRIPTION" ReadOnly="True" />
                        <asp:BoundField DataField="Qty" HeaderText="Qty" SortExpression="Qty" />
                        <asp:BoundField DataField="POVALUE" HeaderText="POVALUE" SortExpression="POVALUE" ReadOnly="True" />
                         <asp:BoundField DataField="UOM" HeaderText="UOM" SortExpression="UOM" />
                         <asp:BoundField DataField="LocationName" HeaderText="LocationName" SortExpression="LocationName" />
                         <asp:BoundField DataField="LocationAddress" HeaderText="LocationAddress" SortExpression="LocationAddress" />
                         <asp:BoundField DataField="IsForwarded" HeaderText="IsForwarded" SortExpression="IsForwarded" />
                          <asp:ButtonField ButtonType="Button" CommandName="Approve" HeaderText="Approve" Text="Approve" Visible="False" />
                        <asp:ButtonField CommandName="Reject" HeaderText="Delete" Text="Delete" />
                        <asp:ButtonField CommandName="Show" HeaderText="Show" Text="Show" />
                    </Columns>
                    <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
                    <HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" />
                    <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
                    <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
                    <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#FFF1D4" />
                    <SortedAscendingHeaderStyle BackColor="#B95C30" />
                    <SortedDescendingCellStyle BackColor="#F1E5CE" />
                    <SortedDescendingHeaderStyle BackColor="#93451F" />
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td>
                            <asp:Button ID="btn_ro" runat="server"  Text="Approve All selected" OnClick="btn_ro_Click" />
            </td>
        </tr>
                      <tr>
                          <td><asp:Button ID="btn_forwardro" runat="server"  Text="Forward Selected Ros For Management Approval" OnClick="btn_forwardro_Click" /></td>
                      </tr>
        <tr>
            <td class="auto-style7">
                <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT       RO_Pk,  RONum, FRMATC, TOATC,    (ISNULL( FRMTEMP,'')+''+ISNULL( FRMCOMP,'')+''+ISNULL( FRMCONS,'')+''+ISNULL( FRMWEIG,'')+''+ISNULL( FRMITEMCOLOR,'')+''+ISNULL( FRMSUPPCOLOR,'')+''+ISNULL( FRMITEMSIZE,'')+''+ISNULL( FRMSUPPSIZE,'')) AS DESCRIPTION, Qty,  (QTY*RATE)AS POVALUE, UOM,  
                         LocationName, LocationAddress, IsForwarded
FROM            (SELECT        RequestOrderMaster.RONum, AtcMaster.AtcNum AS FRMATC, AtcMaster_1.AtcNum AS TOATC, Template_Master.Description AS TOTEMP, Template_Master_1.Description AS FRMTEMP, RequestOrderDetails.Qty, 
                         SkuRawMaterialMaster.Composition AS FRMCOMP, SkuRawMaterialMaster.Construction AS FRMCONS, SkuRawMaterialMaster.Weight AS FRMWEIG, SkuRawMaterialMaster.Width AS FROMWID, 
                         SkuRawmaterialDetail.ItemColor AS FRMITEMCOLOR, SkuRawmaterialDetail.SupplierColor AS FRMSUPPCOLOR, SkuRawmaterialDetail.ItemSize AS FRMITEMSIZE, 
                         SkuRawmaterialDetail.SupplierSize AS FRMSUPPSIZE, RequestOrderDetails.CUnitPrice AS RATE, UOMMaster.UomName AS UOM, SkuRawMaterialMaster_1.Composition, SkuRawMaterialMaster_1.Construction, 
                         LocationMaster.LocationName, LocationMaster.LocationAddress, RequestOrderMaster.RO_Pk, RequestOrderMaster.IsApproved, RequestOrderMaster.IsForwarded
FROM            SkuRawmaterialDetail INNER JOIN
                         RequestOrderMaster INNER JOIN
                         RequestOrderDetails ON RequestOrderMaster.RO_Pk = RequestOrderDetails.RO_Pk ON SkuRawmaterialDetail.SkuDet_PK = RequestOrderDetails.FromSkuDet_PK INNER JOIN
                         SkuRawmaterialDetail AS SkuRawmaterialDetail_1 ON RequestOrderDetails.ToSkuDet_PK = SkuRawmaterialDetail_1.SkuDet_PK INNER JOIN
                         SkuRawMaterialMaster ON SkuRawmaterialDetail.Sku_PK = SkuRawMaterialMaster.Sku_Pk INNER JOIN
                         SkuRawMaterialMaster AS SkuRawMaterialMaster_1 ON SkuRawmaterialDetail_1.Sku_PK = SkuRawMaterialMaster_1.Sku_Pk INNER JOIN
                         AtcMaster ON SkuRawMaterialMaster.Atc_id = AtcMaster.AtcId INNER JOIN
                         AtcMaster AS AtcMaster_1 ON SkuRawMaterialMaster_1.Atc_id = AtcMaster_1.AtcId INNER JOIN
                         Template_Master ON SkuRawMaterialMaster.Template_pk = Template_Master.Template_PK INNER JOIN
                         Template_Master AS Template_Master_1 ON SkuRawMaterialMaster_1.Template_pk = Template_Master_1.Template_PK INNER JOIN
                         InventoryMaster ON RequestOrderDetails.InventoryItem_PK = InventoryMaster.InventoryItem_PK INNER JOIN
                         LocationMaster ON InventoryMaster.Location_PK = LocationMaster.Location_PK INNER JOIN
                         UOMMaster ON InventoryMaster.Uom_Pk = UOMMaster.Uom_PK
GROUP BY RequestOrderMaster.RONum, RequestOrderMaster.CreatedDate, RequestOrderMaster.AddedBy, AtcMaster.AtcNum, AtcMaster_1.AtcNum, Template_Master.Description, Template_Master_1.Description, 
                         RequestOrderDetails.Qty, SkuRawMaterialMaster.Composition, SkuRawMaterialMaster.Construction, SkuRawMaterialMaster.Weight, SkuRawMaterialMaster.Width, SkuRawmaterialDetail.ItemColor, 
                         SkuRawmaterialDetail.SupplierColor, SkuRawmaterialDetail.ItemSize, SkuRawmaterialDetail.SupplierSize, RequestOrderDetails.CUnitPrice, UOMMaster.UomName, SkuRawMaterialMaster_1.Composition, 
                         SkuRawMaterialMaster_1.Construction, SkuRawMaterialMaster_1.Weight, SkuRawMaterialMaster_1.Width, SkuRawmaterialDetail_1.ItemColor, SkuRawmaterialDetail_1.SupplierColor, 
                         SkuRawmaterialDetail_1.ItemSize, SkuRawmaterialDetail_1.SupplierSize, LocationMaster.LocationName, LocationMaster.LocationAddress, RequestOrderMaster.RO_Pk, RequestOrderMaster.IsApproved, 
                         RequestOrderMaster.IsForwarded
                          HAVING         (RequestOrderMaster.IsApproved = N'N')) AS TT"></asp:SqlDataSource>
            </td>
        </tr>
    </table>
            </asp:View>

              
              <asp:View ID="SRO" runat="server">
                  <table class="DataEntryTable">
        <tr>
            <td class="RedHeadding">rO Approval</td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="tbl_sro" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource4" BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2" DataKeyNames="SRO_Pk">
                    <Columns>
                         <asp:TemplateField HeaderImageUrl="~/Image/tick.jpg">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chk_select" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        <asp:BoundField DataField="SRO_Pk" HeaderText="SRO_Pk" InsertVisible="False" ReadOnly="True" SortExpression="SRO_Pk" />
                        <asp:BoundField DataField="RONum" HeaderText="RONum" SortExpression="RONum" />
                        <asp:BoundField DataField="dESCRIPTION" HeaderText="dESCRIPTION" SortExpression="dESCRIPTION" ReadOnly="True" />
                        <asp:BoundField DataField="Qty" HeaderText="Qty" SortExpression="Qty" />
                        <asp:BoundField DataField="Povalue" HeaderText="Povalue" SortExpression="Povalue" ReadOnly="True" />
                        <asp:BoundField DataField="fromLocation" HeaderText="fromLocation" SortExpression="fromLocation" ReadOnly="True" />
                        <asp:BoundField DataField="ToSkuDet_PK" HeaderText="ToSkuDet_PK" SortExpression="ToSkuDet_PK" />
                         <asp:BoundField DataField="IsForwarded" HeaderText="IsForwarded" SortExpression="IsForwarded" />
                         <asp:BoundField DataField="IsApproved" HeaderText="IsApproved" SortExpression="IsApproved" />
                    </Columns>
                    <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
                    <HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" />
                    <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
                    <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
                    <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#FFF1D4" />
                    <SortedAscendingHeaderStyle BackColor="#B95C30" />
                    <SortedDescendingCellStyle BackColor="#F1E5CE" />
                    <SortedDescendingHeaderStyle BackColor="#93451F" />
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td>
                            <asp:Button ID="btn_stockro" runat="server"  Text="Approve All selected" OnClick="btn_stockro_Click" />
            </td>
        </tr>
                      <tr>
                          <td><asp:Button ID="btn_forwardsro" runat="server"  Text="Forward Selected Ros For Management Approval" OnClick="btn_forwardsro_Click" /></td>
                      </tr>
        <tr>
            <td class="auto-style7">
                <asp:SqlDataSource ID="SqlDataSource4" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT        RequestOrderStockMaster.SRO_Pk, RequestOrderStockMaster.RONum, ISNULL(SkuRawMaterialMaster.RMNum, '') + ' ' + ISNULL(SkuRawMaterialMaster.Composition, '') 
                         + ' ' + ISNULL(SkuRawMaterialMaster.Construction, '') + ' ' + ISNULL(SkuRawmaterialDetail.SupplierColor, '') + ' ' + ISNULL(SkuRawmaterialDetail.SupplierSize, '') AS dESCRIPTION, RequestOrderStockDetails.Qty, 
                         RequestOrderStockDetails.Qty * RequestOrderStockDetails.CUnitPrice AS Povalue, LocationMaster.LocationName + '  GStock ' AS fromLocation, RequestOrderStockDetails.ToSkuDet_PK, 
                         RequestOrderStockMaster.IsForwarded, RequestOrderStockMaster.IsApproved
FROM            SkuRawMaterialMaster INNER JOIN
                         SkuRawmaterialDetail ON SkuRawMaterialMaster.Sku_Pk = SkuRawmaterialDetail.Sku_PK INNER JOIN
                         RequestOrderStockDetails ON SkuRawmaterialDetail.SkuDet_PK = RequestOrderStockDetails.ToSkuDet_PK INNER JOIN
                         StockInventoryMaster ON RequestOrderStockDetails.SInventoryItem_PK = StockInventoryMaster.SInventoryItem_PK INNER JOIN
                         LocationMaster ON StockInventoryMaster.Location_Pk = LocationMaster.Location_PK INNER JOIN
                         RequestOrderStockMaster ON RequestOrderStockDetails.SRO_Pk = RequestOrderStockMaster.SRO_Pk
WHERE        (RequestOrderStockMaster.IsApproved = 'N')"></asp:SqlDataSource>
            </td>
        </tr>
    </table>
            </asp:View>




            <asp:View ID="WrongPO" runat="server">
                  <table class="DataEntryTable">
        <tr>
            <td class="RedHeadding">wrong&nbsp; po item Approval</td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource5" BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2" DataKeyNames="WrongPO_Pk">
                    <Columns>
                        <asp:TemplateField HeaderImageUrl="~/Image/tick.jpg">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chk_select" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                         <asp:BoundField DataField="WrongPO_Pk" HeaderText="WrongPO_Pk" InsertVisible="False" ReadOnly="True" SortExpression="WrongPO_Pk" />
                         <asp:BoundField DataField="Reqnum" HeaderText="Reqnum" SortExpression="Reqnum" />
                         <asp:BoundField DataField="MerchandiserName" HeaderText="MerchandiserName" SortExpression="MerchandiserName" />
                         <asp:BoundField DataField="AtcNum" HeaderText="AtcNum" SortExpression="AtcNum" />
                        <asp:BoundField DataField="PONum" HeaderText="PONum" SortExpression="PONum" />
                        <asp:BoundField DataField="Qty" HeaderText="Qty" SortExpression="Qty" ReadOnly="True" />
                        <asp:BoundField DataField="POvalue" HeaderText="POvalue" SortExpression="POvalue" ReadOnly="True" />
                        <asp:BoundField DataField="Explanation" HeaderText="Explanation" SortExpression="Explanation" />
                         <asp:BoundField DataField="IsApproved" HeaderText="IsApproved" SortExpression="IsApproved" />
                         <asp:BoundField DataField="Isforwarded" HeaderText="Isforwarded" SortExpression="Isforwarded" />
                    </Columns>
                    <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
                    <HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" />
                    <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
                    <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
                    <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#FFF1D4" />
                    <SortedAscendingHeaderStyle BackColor="#B95C30" />
                    <SortedDescendingCellStyle BackColor="#F1E5CE" />
                    <SortedDescendingHeaderStyle BackColor="#93451F" />
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td>
                            <asp:Button ID="btn_wrongpo" runat="server"  Text="Approve All selected" OnClick="btn_wrongpo_Click" />
            </td>
        </tr>
                      <tr>
                          <td><asp:Button ID="btn_forwardwrongpo" runat="server"  Text="Forward Selected Ros For Management Approval" OnClick="btn_forwardwrongpo_Click" /></td>
                      </tr>
        <tr>
            <td class="auto-style7">
                <asp:SqlDataSource ID="SqlDataSource5" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT        WrongPOMaster.WrongPO_Pk, WrongPOMaster.Reqnum, WrongPOMaster.MerchandiserName, AtcMaster.AtcNum, ProcurementMaster.PONum, SUM(ProcurementDetails.POQty) AS Qty, 
                         SUM(CAST(ProcurementDetails.POQty * ProcurementDetails.POUnitRate AS decimal(18, 2))) AS POvalue, WrongPOMaster.Explanation, WrongPOMaster.IsApproved, WrongPOMaster.Isforwarded
FROM            ProcurementMaster INNER JOIN
                         ProcurementDetails ON ProcurementMaster.PO_Pk = ProcurementDetails.PO_Pk INNER JOIN
                         SkuRawmaterialDetail ON ProcurementDetails.SkuDet_PK = SkuRawmaterialDetail.SkuDet_PK INNER JOIN
                         SkuRawMaterialMaster ON SkuRawmaterialDetail.Sku_PK = SkuRawMaterialMaster.Sku_Pk INNER JOIN
                         UOMMaster ON ProcurementDetails.Uom_PK = UOMMaster.Uom_PK INNER JOIN
                         Template_Master ON SkuRawMaterialMaster.Template_pk = Template_Master.Template_PK INNER JOIN
                         WrongPODetail ON ProcurementDetails.PODet_PK = WrongPODetail.Podet_PK INNER JOIN
                         WrongPOMaster ON ProcurementMaster.PO_Pk = WrongPOMaster.PO_PK AND WrongPODetail.WrongPO_Pk = WrongPOMaster.WrongPO_Pk INNER JOIN
                         AtcMaster ON SkuRawMaterialMaster.Atc_id = AtcMaster.AtcId
GROUP BY WrongPOMaster.MerchandiserName, WrongPOMaster.Explanation, WrongPOMaster.IsApproved, WrongPOMaster.Reqnum, WrongPOMaster.WrongPO_Pk, ProcurementMaster.PONum, 
                         WrongPOMaster.Isforwarded, AtcMaster.AtcNum
HAVING        (WrongPOMaster.IsApproved = N'N')"></asp:SqlDataSource>
            </td>
        </tr>
    </table>
            </asp:View>



            <asp:View ID="View1" runat="server">
                  <table class="DataEntryTable">
        <tr>
            <td class="RedHeadding">&nbsp;extra bom Approval</td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="tbl_extrabom" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource6" BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2" DataKeyNames="ExtraBOM_PK">
                    <Columns>
                         <asp:TemplateField HeaderImageUrl="~/Image/tick.jpg">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chk_select" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                         <asp:BoundField DataField="ExtraBOM_PK" HeaderText="ExtraBOM_PK" InsertVisible="False" ReadOnly="True" SortExpression="ExtraBOM_PK" />
                         <asp:BoundField DataField="Reqnum" HeaderText="Reqnum" SortExpression="Reqnum" />
                         <asp:BoundField DataField="AtcNum" HeaderText="AtcNum" SortExpression="AtcNum" />
                         <asp:BoundField DataField="MerchandiserName" HeaderText="MerchandiserName" SortExpression="MerchandiserName" />
                        <asp:BoundField DataField="Explanation" HeaderText="Explanation" SortExpression="Explanation" />
                        <asp:BoundField DataField="ExtraValue" HeaderText="ExtraValue" SortExpression="ExtraValue" ReadOnly="True" />
                        <asp:BoundField DataField="Currency" HeaderText="Currency" SortExpression="Currency" ReadOnly="True" />
                        <asp:BoundField DataField="AddedBY" HeaderText="AddedBY" SortExpression="AddedBY" />
                         <asp:BoundField DataField="AddedDate" HeaderText="AddedDate" SortExpression="AddedDate" />
                         <asp:BoundField DataField="Isforwarded" HeaderText="Isforwarded" SortExpression="Isforwarded" />
                    </Columns>
                    <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
                    <HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" />
                    <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
                    <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
                    <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#FFF1D4" />
                    <SortedAscendingHeaderStyle BackColor="#B95C30" />
                    <SortedDescendingCellStyle BackColor="#F1E5CE" />
                    <SortedDescendingHeaderStyle BackColor="#93451F" />
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td>
                            <asp:Button ID="btn__approveextrabom" runat="server"  Text="Approve All selected" OnClick="btn__approveextrabom_Click1"  />
            </td>
        </tr>
                      <tr>
                          <td><asp:Button ID="btn_forwardextrabom" runat="server"  Text="Forward Selected Ros For Management Approval" OnClick="btn_forwardextrabom_Click" /></td>
                      </tr>
        <tr>
            <td class="auto-style7">
                <asp:SqlDataSource ID="SqlDataSource6" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT        ExtraBOM_PK, Reqnum, AtcNum, MerchandiserName, Explanation, SUM(ExtraValue) AS ExtraValue, 'USD' as Currency, AddedBY, AddedDate, Isforwarded
FROM            (SELECT        ExtraBOMRequestMaster.ExtraBOM_PK, ExtraBOMRequestMaster.Reqnum, ExtraBOMRequestMaster.AddedBY, ExtraBOMRequestMaster.AddedDate, ExtraBOMRequestMaster.MerchandiserName, 
                                                    ExtraBOMRequestMaster.Explanation, ExtraBOMRequestMaster.IsApproved, ExtraBOMRequestDetail.Skudet_PK, ExtraBOMRequestMaster.Isforwarded, SkuRawmaterialDetail.UnitRate, 
                                                    ExtraBOMRequestDetail.Qty * SkuRawmaterialDetail.UnitRate AS ExtraValue, AtcMaster.AtcNum
                          FROM            ExtraBOMRequestMaster INNER JOIN
                                                    ExtraBOMRequestDetail ON ExtraBOMRequestMaster.ExtraBOM_PK = ExtraBOMRequestDetail.ExtraBOM_PK INNER JOIN
                                                    SkuRawmaterialDetail ON ExtraBOMRequestDetail.Skudet_PK = SkuRawmaterialDetail.SkuDet_PK INNER JOIN
                                                    SkuRawMaterialMaster ON SkuRawmaterialDetail.Sku_PK = SkuRawMaterialMaster.Sku_Pk INNER JOIN
                                                    AtcMaster ON SkuRawMaterialMaster.Atc_id = AtcMaster.AtcId
                          WHERE        (ExtraBOMRequestMaster.IsApproved = N'N')) AS tt
GROUP BY ExtraBOM_PK, Reqnum, AtcNum, MerchandiserName, Explanation, AddedBY, AddedDate, Isforwarded"></asp:SqlDataSource>
            </td>
        </tr>
    </table>
            </asp:View>


        </asp:MultiView>


    </div>

</asp:Content>

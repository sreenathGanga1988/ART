<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="LocalCostingApproval.aspx.cs" Inherits="ArtWebApp.Approvals.LocalCostingApproval" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../css/style.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:MultiView ID="MultiView1" runat="server">
        <asp:View ID="CostingView" runat="server">
            <table class="auto-style1">
        <tr>
            <td class="RedHeaddingdIV">Departmental Costing Approval</td>
        </tr>
        
        <tr>
            <td>
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="Costing_PK" DataSourceID="SqlDataSource1" OnRowCommand="GridView1_RowCommand" BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2" OnRowDataBound="GridView1_RowDataBound">
                    <Columns>
                         <asp:TemplateField HeaderImageUrl="~/Image/tick.jpg">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chk_select" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        <asp:BoundField DataField="Costing_PK" HeaderText="Costing_PK" InsertVisible="False" ReadOnly="True" SortExpression="Costing_PK" />
                        <asp:BoundField DataField="OurStyle" HeaderText="OurStyle" SortExpression="OurStyle" />
                        <asp:BoundField DataField="BuyerStyle" HeaderText="BuyerStyle" SortExpression="BuyerStyle" />
                        <asp:BoundField DataField="CreatedBy" HeaderText="CreatedBy" SortExpression="CreatedBy" />
                        <asp:BoundField DataField="CreatedDate" HeaderText="CreatedDate" SortExpression="CreatedDate" />
                        <asp:BoundField DataField="CostingCount" HeaderText="CostingCount" SortExpression="CostingCount" />

                        <asp:BoundField DataField="FOB" HeaderText="FOB" SortExpression="FOB" />
                        <asp:BoundField DataField="MarginValue" HeaderText="MarginValue" SortExpression="MarginValue" />
                        <asp:BoundField DataField="Margin" HeaderText="Margin" SortExpression="Margin" />

                         
                        <asp:BoundField DataField="IsApplicable" HeaderText="IsApplicable" SortExpression="IsApplicable" Visible="False" />
                        <asp:ButtonField ButtonType="Button" CommandName="Approve" HeaderText="Approve" Text="Approve" />
                        <asp:ButtonField CommandName="Reject" HeaderText="Reject" Text="Reject" />
                        <asp:ButtonField CommandName="Show" HeaderText="Show" Text="Show" />
                         <asp:BoundField DataField="IsFowarded" HeaderText="IsFowarded" SortExpression="IsFowarded" />
                        
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
                <table class="auto-style7">
                    <tr>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>
                            <asp:Button ID="btn_approveAll" runat="server" OnClick="btn_approveAll_Click" Text="Approve All selected" />
                        </td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td colspan="2">
                            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Forward Costing For Management Approval" />
                        </td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                </table>
            </td>
        </tr>

        <tr>
            <td>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT        StyleCostingMaster.Costing_PK, AtcDetails.OurStyle, AtcDetails.BuyerStyle, 000.000 AS Cost, StyleCostingMaster.CreatedBy, StyleCostingMaster.CreatedDate, StyleCostingMaster.ApprovedBy, 
                         StyleCostingMaster.ApprovedDate, StyleCostingMaster.CostingCount, StyleCostingMaster.IsFowarded, StyleCostingMaster.IsApplicable, StyleCostingMaster.IsSubmitted, StyleCostingMaster.IsAccountable, 
                         StyleCostingMaster.FOB, StyleCostingMaster.MarginValue, StyleCostingMaster.Margin, StyleCostingMaster.IsLocalApproval
FROM            StyleCostingMaster INNER JOIN
                         AtcDetails ON StyleCostingMaster.OurStyleID = AtcDetails.OurStyleID
WHERE        (StyleCostingMaster.IsApproved = N'N') AND (StyleCostingMaster.IsSubmitted = N'Y') AND (StyleCostingMaster.IsLast = N'Y') AND (StyleCostingMaster.IsLocalApproval = N'Y')
ORDER BY StyleCostingMaster.Costing_PK DESC"></asp:SqlDataSource>
            </td>
        </tr>

    </table>

        </asp:View>
          <asp:View ID="ROView" runat="server">
              <div class="RedHeaddingdIV">
                  Departmental RO Approval
              </div>
              <div>
                  <asp:GridView ID="tbl_ro" runat="server" AutoGenerateColumns="False"  BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2" DataKeyNames="RO_Pk" DataSourceID="SqlDataSource3">
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
                  <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" 
                      SelectCommand="SELECT        RO_Pk, RONum, FRMATC, TOATC, ISNULL(FRMTEMP, '') + '' + ISNULL(FRMCOMP, '') + '' + ISNULL(FRMCONS, '') + '' + ISNULL(FRMWEIG, '') + '' + ISNULL(FRMITEMCOLOR, '') + '' + ISNULL(FRMSUPPCOLOR, '') 
                         + '' + ISNULL(FRMITEMSIZE, '') + '' + ISNULL(FRMSUPPSIZE, '') AS DESCRIPTION, Qty, Qty * RATE AS POVALUE, UOM, LocationName, LocationAddress, IsForwarded
FROM            (SELECT        RequestOrderMaster.RONum, AtcMaster.AtcNum AS FRMATC, AtcMaster_1.AtcNum AS TOATC, Template_Master.Description AS TOTEMP, Template_Master_1.Description AS FRMTEMP, RequestOrderDetails.Qty, 
                         SkuRawMaterialMaster.Composition AS FRMCOMP, SkuRawMaterialMaster.Construction AS FRMCONS, SkuRawMaterialMaster.Weight AS FRMWEIG, SkuRawMaterialMaster.Width AS FROMWID, 
                         SkuRawmaterialDetail.ItemColor AS FRMITEMCOLOR, SkuRawmaterialDetail.SupplierColor AS FRMSUPPCOLOR, SkuRawmaterialDetail.ItemSize AS FRMITEMSIZE, 
                         SkuRawmaterialDetail.SupplierSize AS FRMSUPPSIZE, RequestOrderDetails.CUnitPrice AS RATE, UOMMaster.UomName AS UOM, SkuRawMaterialMaster_1.Composition, SkuRawMaterialMaster_1.Construction, 
                         LocationMaster.LocationName, LocationMaster.LocationAddress, RequestOrderMaster.RO_Pk, RequestOrderMaster.IsApproved, RequestOrderMaster.IsForwarded, AtcMaster.MerchandiserName
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
                         RequestOrderMaster.IsForwarded, AtcMaster.MerchandiserName
HAVING        (RequestOrderMaster.IsApproved = N'N') ) AS TT
WHERE        (FRMATC = TOATC)"></asp:SqlDataSource>
              </div>
              <div>
                     <asp:Button ID="btn_ro" runat="server"  Text="Approve All selected" OnClick="btn_ro_Click" />
              </div>
        </asp:View>
          <asp:View ID="View3" runat="server"></asp:View>

    </asp:MultiView>
    
</asp:Content>
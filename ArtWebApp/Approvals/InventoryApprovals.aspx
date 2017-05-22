<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="InventoryApprovals.aspx.cs" Inherits="ArtWebApp.Approvals.InventoryApprovals" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
       
        .auto-style1 {
            height: 239px;
        }
       
    </style>
    <link href="../css/style.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:MultiView ID="MultiView1" runat="server">
        <asp:View ID="View1" runat="server">
            <table class="FullTable">
            <tr>
                <td class="RedHeadding">&nbsp;<strong>Ro Approval&nbsp;</strong></td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="tbl_ROApproval" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4" ShowHeaderWhenEmpty="True" style="font-size: small; font-family: Calibri; font-weight: 400;" Width="100%" DataSourceID="rodatasource" DataKeyNames="RO_Pk" OnRowCommand="tbl_ROApproval_RowCommand">
                        <Columns>
                            <asp:TemplateField HeaderImageUrl="~/Image/tick.jpg">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chk_select" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Ro_pk" HeaderText="Ro_pk" />
                            <asp:BoundField DataField="ronum" HeaderText="Ro#" />
                            <asp:BoundField DataField="Fromitem" HeaderText="From" />
                            <asp:BoundField DataField="Toitem" HeaderText="To" />
                            <asp:BoundField DataField="Qty" HeaderText="Qty" />
                            <asp:BoundField DataField="CUnitPrice" HeaderText="CUnitPrice" />
                            <asp:ButtonField ButtonType="Button" CommandName="Approve" HeaderText="Approve" Text="Approve" />
                        <asp:ButtonField CommandName="Reject" HeaderText="Reject" Text="Reject" />
                        <asp:ButtonField CommandName="Show" HeaderText="Show" Text="Show" />
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
                </td>
            </tr>
            <tr>
                <td>
                    <asp:SqlDataSource ID="rodatasource" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT        RequestOrderMaster.RO_Pk, RequestOrderMaster.RONum, RequestOrderMaster.IsApproved, SkuRawMaterialMaster.RMNum AS FromItem, SkuRawMaterialMaster_1.RMNum AS ToItem, RequestOrderDetails.Qty, 
                         RequestOrderDetails.CUnitPrice
FROM            RequestOrderMaster INNER JOIN
                         RequestOrderDetails ON RequestOrderMaster.RO_Pk = RequestOrderDetails.RO_Pk INNER JOIN
                         SkuRawmaterialDetail ON RequestOrderDetails.FromSkuDet_PK = SkuRawmaterialDetail.SkuDet_PK INNER JOIN
                         SkuRawMaterialMaster ON SkuRawmaterialDetail.Sku_PK = SkuRawMaterialMaster.Sku_Pk INNER JOIN
                         SkuRawmaterialDetail AS SkuRawmaterialDetail_1 ON RequestOrderDetails.ToSkuDet_PK = SkuRawmaterialDetail_1.SkuDet_PK INNER JOIN
                         SkuRawMaterialMaster AS SkuRawMaterialMaster_1 ON SkuRawmaterialDetail_1.Sku_PK = SkuRawMaterialMaster_1.Sku_Pk
WHERE        (RequestOrderMaster.IsApproved = N'N')"></asp:SqlDataSource>
                </td>
            </tr>
        </table>
        </asp:View>

             <asp:View ID="View2" runat="server">
            <table class="FullTable">
            <tr>
                <td class="RedHeadding"><strong>Loan&nbsp; Approval&nbsp;</strong></td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="tbl_loanApproval" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4" DataSourceID="loandatasourcee" ShowHeaderWhenEmpty="True" style="font-size: small; font-family: Calibri; font-weight: 400;" Width="100%" DataKeyNames="Loan_pk" OnRowCommand="tbl_loanApproval_RowCommand">
                        <Columns>
                            <asp:TemplateField HeaderImageUrl="~/Image/tick.jpg">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chk_select" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Loan_PK" HeaderText="Loan_PK" />
                             <asp:BoundField DataField="LoanNum" HeaderText="LoanNum" />
                            <asp:BoundField DataField="Fromitem" HeaderText="From" />
                            <asp:BoundField DataField="Toitem" HeaderText="To" />
                            <asp:BoundField DataField="LoanQty" HeaderText="LoanQty" />
                            <asp:BoundField DataField="UnitPrice" HeaderText="UnitPrice" />
                            <asp:ButtonField ButtonType="Button" CommandName="Approve" HeaderText="Approve" Text="Approve" Visible="False" />
                            <asp:ButtonField CommandName="Reject" HeaderText="Reject" Text="Reject" />
                            <asp:ButtonField CommandName="Show" HeaderText="Show" Text="Show" />
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
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="Button1" runat="server" OnClick="btn_approveloan" Text="Approve Loan" />
                    <asp:Button ID="Button2" runat="server" OnClick="btn_rejectloan" Text="Reject Loan" />
                </td>
            </tr>
                <tr>
                    <td>
                        <asp:SqlDataSource ID="loandatasourcee" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT      
                          InventoryLoanMaster.Loan_PK, SkuRawMaterialMaster.RMNum AS FromItem, SkuRawMaterialMaster_1.RMNum AS ToItem, InventoryLoanMaster.LoanQty, InventoryLoanMaster.UnitPrice, 
                         InventoryLoanMaster.IsApproved, InventoryLoanMaster.AddedBY, InventoryLoanMaster.LoanNum
FROM            SkuRawMaterialMaster INNER JOIN
                         SkuRawmaterialDetail ON SkuRawMaterialMaster.Sku_Pk = SkuRawmaterialDetail.Sku_PK INNER JOIN
                         InventoryLoanMaster ON SkuRawmaterialDetail.SkuDet_PK = InventoryLoanMaster.FromSkudet_PK INNER JOIN
                         SkuRawmaterialDetail AS SkuRawmaterialDetail_1 ON InventoryLoanMaster.ToSkuDet_PK = SkuRawmaterialDetail_1.SkuDet_PK INNER JOIN
                         SkuRawMaterialMaster AS SkuRawMaterialMaster_1 ON SkuRawmaterialDetail_1.Sku_PK = SkuRawMaterialMaster_1.Sku_Pk
WHERE        (InventoryLoanMaster.IsApproved = N'N')"></asp:SqlDataSource>
                    </td>
                </tr>
        </table>
        </asp:View>

             <asp:View ID="View3" runat="server">
            <table class="FullTable">
            <tr>
                <td class="RedHeadding"><strong>Transfer To Gstock Approval</strong></td>
            </tr>
            <tr>
                <td><asp:GridView ID="tbl_transfer" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4" DataSourceID="transferData" ShowHeaderWhenEmpty="True" style="font-size: small; font-family: Calibri; font-weight: 400;" Width="100%" DataKeyNames="TransferToGSTock_PK">
                        <Columns>  <asp:TemplateField HeaderImageUrl="~/Image/tick.jpg">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chk_select" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="TransferToGSTock_PK" HeaderText="TransferToGSTock_PK" InsertVisible="False" ReadOnly="True" SortExpression="TransferToGSTock_PK" />
                            <asp:BoundField DataField="TransferNumber" HeaderText="TransferNumber" SortExpression="TransferNumber" />
                            <asp:BoundField DataField="LocationName" HeaderText="LocationName" SortExpression="LocationName" />
                            <asp:BoundField DataField="AtcNum" HeaderText="AtcNum" SortExpression="AtcNum" />
                            <asp:BoundField DataField="TransferValue" HeaderText="TransferValue" ReadOnly="True" SortExpression="TransferValue" />
                            <asp:BoundField DataField="AddedBy" HeaderText="AddedBy" SortExpression="AddedBy" />
                            <asp:BoundField DataField="IsApproved" HeaderText="IsApproved" SortExpression="IsApproved" />
                            <asp:BoundField DataField="CreatedDate" HeaderText="CreatedDate" SortExpression="CreatedDate" />
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
                    </asp:GridView></td>
            </tr>
            <tr>
                <td>
                    <td>
                    <asp:Button ID="Button3" runat="server"  Text="Approve Transfer to Gstock" OnClick="Button3_Click" />
                </td></td>
            </tr>
                <tr>
                    <td>
                        <asp:SqlDataSource ID="transferData" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT TransferToGstockMaster.TransferToGSTock_PK, TransferToGstockMaster.TransferNumber, LocationMaster.LocationName, AtcMaster.AtcNum, SUM(TransferToGstockDetails.ReceivedQty * TransferToGstockDetails.NewUnitprice) AS TransferValue, TransferToGstockMaster.AddedBy, TransferToGstockMaster.IsApproved, TransferToGstockMaster.CreatedDate FROM TransferToGstockMaster INNER JOIN TransferToGstockDetails ON TransferToGstockMaster.TransferToGSTock_PK = TransferToGstockDetails.TransferToGSTock_PK INNER JOIN SkuRawmaterialDetail ON TransferToGstockDetails.FromSkudet_PK = SkuRawmaterialDetail.SkuDet_PK INNER JOIN SkuRawMaterialMaster ON SkuRawmaterialDetail.Sku_PK = SkuRawMaterialMaster.Sku_Pk INNER JOIN AtcMaster ON SkuRawMaterialMaster.Atc_id = AtcMaster.AtcId INNER JOIN LocationMaster ON TransferToGstockMaster.Location_Pk = LocationMaster.Location_PK GROUP BY TransferToGstockMaster.TransferToGSTock_PK, AtcMaster.AtcNum, TransferToGstockMaster.AddedBy, LocationMaster.LocationName, TransferToGstockMaster.IsApproved, TransferToGstockMaster.CreatedDate, TransferToGstockMaster.TransferNumber HAVING (TransferToGstockMaster.IsApproved = N'N')"></asp:SqlDataSource>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                </tr>
        </table>
        </asp:View>

        <asp:View ID="View4" runat="server">
            <table class="FullTable">
            <tr>
                <td class="RedHeadding"><strong>iNVENTORY mISpLACED</strong></td>
            </tr>
            <tr>
                <td class="auto-style1"><asp:GridView ID="tbl_misPlaced" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4" DataSourceID="InventoryMisPlaced" ShowHeaderWhenEmpty="True" style="font-size: small; font-family: Calibri; font-weight: 400;" Width="100%" DataKeyNames="MisplaceApp_pk">
                        <Columns>  
                            <asp:TemplateField HeaderImageUrl="~/Image/tick.jpg">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chk_select" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="MisplaceApp_pk" HeaderText="MisplaceApp_pk" InsertVisible="False" ReadOnly="True" SortExpression="MisplaceApp_pk" />
                            <asp:BoundField DataField="reqnum" HeaderText="reqnum" SortExpression="reqnum" />
                            <asp:BoundField DataField="LocationName" HeaderText="LocationName" SortExpression="LocationName" />
                            <asp:BoundField DataField="AtcNum" HeaderText="AtcNum" SortExpression="AtcNum" />
                            <asp:BoundField DataField="MisplaceDate" HeaderText="MisplaceDate" SortExpression="MisplaceDate" />
                            <asp:BoundField DataField="Explanation" HeaderText="Explanation" SortExpression="Explanation" />
                            <asp:BoundField DataField="AddedBy" HeaderText="AddedBy" SortExpression="AddedBy" />
                            <asp:BoundField DataField="IsApproved" HeaderText="IsApproved" SortExpression="IsApproved" />
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
                    </asp:GridView></td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="Button4" runat="server" OnClick="Button4_Click" Text="Approve Missplaced Inventory Request and Forward" />
                    <td>
                    
                </td></td>
            </tr>
                <tr>
                    <td>
                        <asp:SqlDataSource ID="InventoryMisPlaced" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT InventoryMissingRequest.MisplaceApp_pk, InventoryMissingRequest.reqnum, LocationMaster.LocationName, AtcMaster.AtcNum, InventoryMissingRequest.MisplaceDate, InventoryMissingRequest.Explanation, InventoryMissingRequest.AddedBy, InventoryMissingRequest.IsApproved FROM InventoryMissingRequest INNER JOIN LocationMaster ON InventoryMissingRequest.FromLctn_pk = LocationMaster.Location_PK INNER JOIN AtcMaster ON InventoryMissingRequest.Atc_id = AtcMaster.AtcId WHERE (InventoryMissingRequest.Level1Approval = N'N')"></asp:SqlDataSource>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                </tr>
        </table>
        </asp:View>
        
    </asp:MultiView>
</asp:Content>

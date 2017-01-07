<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="ROTransferIn.aspx.cs" Inherits="ArtWebApp.Inventory.ROTransferIn" %>
<%@ Register assembly="Infragistics35.Web.v12.1, Version=12.1.20121.2236, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.ListControls" tagprefix="ig" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">

    </style>
 <link href="../../css/style.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="FullTable">
        <tr>
            <td class="SUBRedHeadding">ro receipt(rr)</td>
        </tr>
        <tr>
            <td>
                <table class="DataEntryTable">
                    <tr>
                        <td class="NormalTD">Ro#</td>
                        <td class="NormalTD">
                           

                            <ucc:DropDownListChosen ID="drp_ro" Width="200px"  runat="server" DataSourceID="Rodata" DataTextField="RONum" DataValueField="RO_PK" DisableSearchThreshold="10" Height="16px"   >
                                        </ucc:DropDownListChosen>
                        </td>
                        <td>
                            <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="S" />
                        </td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style9">&nbsp;</td>
                        <td class="auto-style3">
                            &nbsp;</td>
                        <td>
                               
                                <asp:Label ID="lbl_errordisplayer" runat="server" Text="*" Font-Italic="True" ForeColor="#FF3300"></asp:Label>
                               
                                </td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr class="gridtable">
            <td>
                            <asp:GridView ID="tbl_Podetails" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4" ShowHeaderWhenEmpty="True" style="font-size: small; font-family: Calibri" Width="90%" DataKeyNames="RODet_Pk">
                                <Columns>
                                    <asp:TemplateField>
                                       
                                        <ItemTemplate>
                                            <asp:CheckBox ID="Chk_select" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="RODet_Pk" InsertVisible="False" SortExpression="RODet_Pk">
                                      
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_rodetPk" runat="server" Text='<%# Bind("RODet_Pk") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="RMNum" HeaderText="RMNum" SortExpression="RMNum" />
                                    <asp:BoundField DataField="Composition" HeaderText="Composition" SortExpression="Composition" />
                                    <asp:BoundField DataField="Construction" HeaderText="Construction" SortExpression="Construction" />
                                    <asp:BoundField DataField="SupplierColor" HeaderText="SupplierColor" SortExpression="SupplierColor" />
                                    <asp:BoundField DataField="SupplierSize" HeaderText="SupplierSize" SortExpression="SupplierSize" />
                                    <asp:TemplateField HeaderText="Qty" SortExpression="Qty">
                                       
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_txtQty" runat="server" Text='<%# Bind("Qty") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="CUnitPrice" SortExpression="CUnitPrice">
                                        
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_cunitrate" runat="server" Text='<%# Bind("CUnitPrice") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="FromSkuDet_PK">
                                     
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_FromSkuDet_PK" runat="server" Text='<%# Bind("FromSkuDet_PK") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ToSkuDet_PK">
                                       
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_ToSkuDet_PK" runat="server" Text='<%# Bind("ToSkuDet_PK") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="InventoryItem_PK">
                                      
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_InventoryItem_PK" runat="server" Text='<%# Bind("InventoryItem_PK") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
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
                            <asp:Button ID="Btn_TransferRO" runat="server" Text="Process Request Order Transaction" OnClick="Btn_TransferRO_Click" />
                        </td>
        </tr>
        <tr>
            <td>
                <asp:SqlDataSource ID="Rodata" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT RONum, RO_Pk, IsCompleted FROM RequestOrderMaster WHERE (IsApproved = N'Y') AND (IsCompleted = N'N') ORDER BY RONum DESC">
                </asp:SqlDataSource>
                <asp:SqlDataSource ID="rodetailsdata" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT RequestOrderDetails.RODet_Pk, SkuRawMaterialMaster.RMNum, SkuRawMaterialMaster.Composition, SkuRawMaterialMaster.Construction, SkuRawmaterialDetail.SupplierColor, SkuRawmaterialDetail.SupplierSize, RequestOrderDetails.Qty, RequestOrderDetails.CUnitPrice FROM SkuRawMaterialMaster INNER JOIN SkuRawmaterialDetail ON SkuRawMaterialMaster.Sku_Pk = SkuRawmaterialDetail.Sku_PK INNER JOIN RequestOrderDetails ON SkuRawmaterialDetail.SkuDet_PK = RequestOrderDetails.FromSkuDet_PK WHERE (RequestOrderDetails.RO_Pk = @param1)">
                    <SelectParameters>
                        <asp:SessionParameter DefaultValue="" Name="param1" SessionField="Ro_pk" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </td>
        </tr>
    </table>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="StockRoTransferIn.aspx.cs" Inherits="ArtWebApp.Inventory.RO_Transfer.StockRoTransferIn" %>
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
                        <td class="NormalTD">SRo#</td>
                        <td class="NormalTD">
                           

                            <ucc:DropDownListChosen ID="drp_ro" Width="200px"  runat="server" DataSourceID="rodetailsdata" DataTextField="RONum" DataValueField="SRO_Pk" DisableSearchThreshold="10" Height="16px"   >
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
                                    <asp:TemplateField HeaderText="From">
                                     
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_FromSkuDet_PK" runat="server" Text='<%# Bind("fromLocation") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ToSkuDet_PK">
                                       
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_ToSkuDet_PK" runat="server" Text='<%# Bind("ToSkuDet_PK") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="InventoryItem_PK">
                                      
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_InventoryItem_PK" runat="server" Text='<%# Bind("SInventoryItem_PK") %>'></asp:Label>
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
                <asp:SqlDataSource ID="Rodata" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" 
                    SelectCommand="SELECT SRO_Pk, RONum, IsApproved, Iscompleted FROM RequestOrderStockMaster WHERE (IsApproved = N'Y') AND
                    (Iscompleted = N'N')">

                </asp:SqlDataSource>

             


                <asp:SqlDataSource ID="rodetailsdata" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" 
                    
                    
                    SelectCommand="SELECT RONum, SRO_Pk, IsCompleted, Location_PK FROM RequestOrderStockMaster WHERE (IsApproved = N'Y') AND (IsCompleted = N'N') AND (Location_PK = @Param1) ORDER BY RONum DESC
">
                    <SelectParameters>
                        <asp:SessionParameter DefaultValue="" Name="param1" SessionField="UserLoc_pk" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </td>
        </tr>
    </table>
</asp:Content>

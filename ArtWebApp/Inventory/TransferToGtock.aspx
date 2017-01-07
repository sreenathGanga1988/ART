<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="TransferToGtock.aspx.cs" Inherits="ArtWebApp.Inventory.TransferToGtock" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
      
    </style>
    <link href="../css/style.css" rel="stylesheet" />
    <script src="../JQuery/GridJQuery.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="FullTable">
        <tr>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>
                                &nbsp;</td>
        </tr>
        <tr>
            <td >
                <asp:UpdatePanel ID="udp_grid"  UpdateMode="Conditional" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="tbl_InverntoryDetails" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4" ShowHeaderWhenEmpty="True" style="font-size: small; font-family: Calibri; font-weight: 400;" Width="100%">
                            <Columns>
                                <asp:TemplateField HeaderText="II_PK">
                                    <ItemTemplate>
                                        <asp:Label ID="lblInventoryItem_PK" runat="server" Text='<%# Bind("InventoryItem_PK") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="SkuDet_Pk">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_fromSkuDet_Pk" runat="server" Text='<%# Bind("SkuDet_Pk") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Template_pk">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_templatepk" runat="server" Text='<%# Bind("Template_pk") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="RMNum">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_rmnum" runat="server" Text='<%# Bind("RMNum") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Description">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_description" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ItemColor">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_itemcolor" runat="server" Text='<%# Bind("ItemColor") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ItemSize">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_itemsize" runat="server" Text='<%# Bind("ItemSize") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="SupplierColor">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_suppliercolor" runat="server" Text='<%# Bind("SupplierColor") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Suppliersize">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_suppliersize" runat="server" Text='<%# Bind("Suppliersize") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="CURate">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_fromcurate" runat="server" Text='<%# Bind("CURate") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="UOM">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_uom" runat="server" Text='<%# Bind("UOMCode") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="OnhandQty" HeaderText="OnhandQty" />
                                <asp:TemplateField HeaderText="Qty">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txt_deliveryQty" runat="server" Text='<%# Bind("TransferQty") %>'></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txt_deliveryQty" ErrorMessage="Transfer Qty Has to Be Numeric" Font-Bold="True" ForeColor="#FF3300" ValidationExpression="^[\d.]+$">*</asp:RegularExpressionValidator>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="NewRate" SortExpression="CUrate">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txt_newrate" runat="server" Text='<%# Bind("CUrate") %>'></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txt_newrate" ErrorMessage="rate has to Be Numeric" Font-Bold="True" ForeColor="#FF3300" ValidationExpression="^[\d.]+$">*</asp:RegularExpressionValidator>
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
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td >
                <asp:UpdatePanel ID="UpdatePanel1"  UpdateMode="Conditional" runat="server">
                    <ContentTemplate>
                        <asp:Button ID="btn_confirmtransfer" runat="server" Text="Confirm Transfer" OnClick="btn_confirmtransfer_Click" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>&nbsp;</td>
        </tr>
    </table>
</asp:Content>

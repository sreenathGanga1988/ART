<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="SalesAndLoan.aspx.cs" Inherits="ArtWebApp.Inventory.SalesAndLoan" %>
<%@ Register assembly="Infragistics35.Web.v12.1, Version=12.1.20121.2236, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.ListControls" tagprefix="ig" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        



      



        </style>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="FullTable">
        <tr class="RedHeadding">
            <td >Internal Transfers</td>
        </tr>
        <tr>
            <td>
                <table class="DataEntryTable">
                    <tr>
                        <td >&nbsp;Atc #</td>
                        <td >
                             
                   
               <ucc:DropDownListChosen ID="cmb_atc"   runat="server" DataSourceID="SqlDataSource1" DataTextField="AtcNum" DataValueField="AtcId" Width="200px" >
                                        </ucc:DropDownListChosen>
                               
                                </td>
                        <td >
                                    <asp:Button ID="ShowBom" runat="server"  Text="S" OnClick="ShowBom_Click" CssClass="auto-style13" Height="20px" Width="29px"  />
                                </td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td ></td>
                        <td >
                             
               
                               
                                <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Loan return" />
                             
               
                               
                                </td>
                        <td >
                                    <asp:Button ID="btn_loan" runat="server" style="margin-bottom: 0px" Text="Loan" Width="112px" OnClick="btn_loan_Click" />
                                </td>
                        <td >
                            <asp:Button ID="btn_gstock" runat="server" Text="Transfer To  GStock" OnClick="btn_gstock_Click" />
                        </td>
                        <td >&nbsp;</td>
                        <td ></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr class="error-message">
            <td >
                <asp:Label ID="lbl_message" runat="server" Text="*" ForeColor="Red"></asp:Label>
            </td>
        </tr>
        <tr class="gridtable">
            <td>
                               <asp:UpdatePanel ID="UpdatePanel2"  UpdateMode="Conditional" runat="server">
                    <ContentTemplate>
                                <asp:GridView ID="tbl_InverntoryDetails" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" style="font-size: small; font-family: Calibri; font-weight: 400;" Width="100%" BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4">
                                    <Columns>
                                        <asp:TemplateField HeaderImageUrl="~/Image/tick.jpg">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chk_select" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
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
                                        <asp:BoundField DataField="RMNum" HeaderText="RMNum" />
                                        <asp:BoundField DataField="Description" HeaderText="Description" />
                                        <asp:BoundField DataField="ItemColor" HeaderText="ItemColor" />
                                        <asp:BoundField DataField="ItemSize" HeaderText="ItemSize" />
                                        <asp:BoundField DataField="SupplierColor" HeaderText="SupplierColor" />
                                        <asp:BoundField DataField="Suppliersize" HeaderText="Suppliersize" />
                                        <asp:TemplateField HeaderText="CURate">
                                           
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_fromcurate" runat="server" Text='<%# Bind("CURate") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="UOMCode" HeaderText="UOM" />
                                        <asp:BoundField DataField="OnhandQty" HeaderText="OnhandQty" />
                                        <asp:TemplateField HeaderText="Qty">
                                           
                                            <ItemTemplate>
                                                <asp:TextBox ID="txt_deliveryQty" runat="server" Text='<%# Bind("TransferQty") %>'></asp:TextBox>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txt_deliveryQty" ErrorMessage="Transfer Qty Has to Be Numeric" Font-Bold="True" ValidationExpression="^[\d.]+$" ForeColor="#FF3300">*</asp:RegularExpressionValidator>
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
            <td ></td>
        </tr>
        <tr>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" 
                    SelectCommand="SELECT DISTINCT AtcNum, AtcId FROM AtcMaster WHERE (IsClosed = N'N') ORDER BY AtcNum, AtcId">
                </asp:SqlDataSource>
                    
               
                               
                                </td>
        </tr>
    </table>
</asp:Content>

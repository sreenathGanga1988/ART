<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="LoanRepay.aspx.cs" Inherits="ArtWebApp.Inventory.LoanRepay" %>
<%@ Register assembly="Infragistics35.Web.v12.1, Version=12.1.20121.2236, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.ListControls" tagprefix="ig" %>
<%@ Register assembly="DropDownChosen" namespace="CustomDropDown" tagprefix="ucc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        




       
        

        .hidden {
            display: none;
        }


        </style>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="FullTable">
        <tr>
            <td class="SUBRedHeadding"><strong>Loan repay From</strong></td>
        </tr>
        <tr class="gridtable">
            <td>
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <asp:GridView ID="tbl_InverntoryDetails" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4" ShowHeaderWhenEmpty="True" style="font-size: small; font-family: Calibri; font-weight: 400;" Width="100%">
                                            <Columns>
                                                <asp:TemplateField HeaderImageUrl="~/Image/tick.jpg">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chk_select" runat="server" AutoPostBack="true" OnCheckedChanged="chk_select_CheckedChanged" />
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
                                                <asp:TemplateField HeaderText="Template_pk">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbl_templatepk" runat="server" Text='<%# Bind("Template_pk") %>'></asp:Label>
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
                                                <asp:TemplateField HeaderText="OnhandQty">
                                            
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_onhandQty" runat="server" Text='<%# Bind("OnhandQty") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Qty">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txt_deliveryQty" runat="server" Text='<%# Bind("TransferQty") %>'></asp:TextBox>
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txt_deliveryQty" ErrorMessage="Transfer Qty Has to Be Numeric" Font-Bold="True" ForeColor="#FF3300" ValidationExpression="^[\d.]+$">*</asp:RegularExpressionValidator>
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
        <tr class="error-message">
            <td>
                <asp:Label ID="lbl_message" runat="server" Text="*" ForeColor="Red"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="SUBRedHeadding"><strong>Loan repay TO</strong></td>
        </tr>
        <tr>
            <td>
                <table class="DataEntryTable">
                    <tr>
                        <td class="auto-style11">&nbsp;loan #</td>
                        <td class="auto-style12">
                             
                          
                            <asp:UpdatePanel ID="upd_loan" UpdateMode="Conditional" runat="server">
                                <ContentTemplate>

                                      <ucc:dropdownlistchosen ID="drp_loan" Width="120px" runat="server">
                                            </ucc:dropdownlistchosen>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td class="auto-style15">
                                    <asp:Button ID="ShowBom" runat="server"  Text="S" OnClick="ShowBom_Click" CssClass="auto-style13" Height="20px" Width="29px"  />
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
                       
                          <asp:UpdatePanel ID="UpdatePanel2"  UpdateMode="Conditional" runat="server">
                    <ContentTemplate>
                                <asp:GridView ID="tbl_bom" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" style="font-size: small; font-family: Calibri; font-weight: 400;" Width="100%" BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4">
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
                                        <asp:TemplateField HeaderText="Loan Qty">
                                           
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
               </asp:UpdatePanel></td>
        </tr>
        <tr class="ButtonTR">
            <td>
                <asp:Button ID="btn_loanTransfer" runat="server" OnClick="btn_loanTransfer_Click" Text="Transfer Item(Loan Return)" />
            </td>
        </tr>
        <tr>
            <td>
                <div id="Messaediv" runat="server">
                 


                           <asp:Label ID="lbl_msg" runat="server" Text="*"></asp:Label>


                     
               </div></td>
        </tr>
    </table>
</asp:Content>

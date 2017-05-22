<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="AtcprojectionApproval.aspx.cs" Inherits="ArtWebApp.Approvals.AtcprojectionApproval" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../css/style.css" rel="stylesheet" />
    <script src="../JQuery/GridJQuery.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  
    <div class="FullTable">

        <table class="DataEntryTable">
                <tr class="RedHeadding">
                    <td>ATC Projection Approval</td>
                </tr>
                <tr>
                    <td>
                        <asp:GridView ID="tbl_podetails" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4" DataKeyNames="AtcApproval_PK" DataSourceID="OurStyleData" Font-Size="Large" style="font-size: x-small; font-family: Calibri" Width="100%" OnRowDataBound="tbl_podetails_RowDataBound">
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="checkAll" runat="server" onclick="checkAll(this)" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chk_select" runat="server" onclick="Check_Click(this)" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                                  <asp:BoundField DataField="AtcApproval_PK" HeaderText="AtcApproval_PK" SortExpression="AtcApproval_PK" />
                                   <asp:TemplateField HeaderText="Atcid">
                                  
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_atcid" runat="server" Text='<%# Bind("atcid") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                            
                                <asp:BoundField DataField="AtcNum" HeaderText="AtcNum" SortExpression="AtcNum" />
                                <asp:BoundField DataField="BuyerName" HeaderText="BuyerName" SortExpression="BuyerName" />
                                 <asp:BoundField DataField="IntialQty" HeaderText="IntialQty" SortExpression="IntialQty" />
                                 <asp:BoundField DataField="Quantity" HeaderText="Quantity" SortExpression="Quantity" />
                              


                                        
                                <asp:TemplateField HeaderText="OurStyle">
                                    <ItemTemplate>
                                        <asp:GridView ID="tbl_atcdetail" runat="server" AutoGenerateColumns="False" BackColor="#CCCCCC" BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellPadding="4" CellSpacing="2" ForeColor="Black">
                                            <Columns>
                                                <asp:BoundField DataField="OurStyle" HeaderText="OurStyle" SortExpression="OurStyle" />
                                                <asp:BoundField DataField="BuyerStyle" HeaderText="BuyerStyle" SortExpression="BuyerStyle" />
                                                <asp:BoundField DataField="Quantity" HeaderText="Quantity" SortExpression="Quantity" />
                                                <asp:BoundField DataField="FOB" HeaderText="FOB" SortExpression="FOB" />
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
                                        <br />
                                    </ItemTemplate>
                                </asp:TemplateField>
                              


                                        
                            </Columns>
                            <FooterStyle BackColor="#FFFFCC" ForeColor="#000066" />
                            <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="#FFFFCC" />
                            <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
                            <RowStyle BackColor="White" ForeColor="#330099" />
                            <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Black" />
                            <SortedAscendingCellStyle BackColor="#FEFCEB" />
                            <SortedAscendingHeaderStyle BackColor="#AF0101" />
                            <SortedDescendingCellStyle BackColor="#F6F0C0" />
                            <SortedDescendingHeaderStyle BackColor="#7E0000" />
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:SqlDataSource ID="OurStyleData" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT        AtcApproval_1.AtcApproval_PK, AtcMaster.AtcNum, ISNULL
                             ((SELECT        SUM(Quantity) AS Expr1
                                 FROM            AtcApproval
                                 WHERE        (IsFirst = N'Y') AND (AtcId = AtcMaster.AtcId)),  AtcMaster.ProjectionQty) AS IntialQty, AtcApproval_1.Quantity, AtcApproval_1.IsForwarded, AtcApproval_1.IsApproved, BuyerMaster.BuyerName, AtcMaster.AtcId
FROM            AtcMaster INNER JOIN
                         AtcApproval AS AtcApproval_1 ON AtcMaster.AtcId = AtcApproval_1.AtcId INNER JOIN
                         BuyerMaster ON AtcMaster.Buyer_ID = BuyerMaster.BuyerID
WHERE        (AtcApproval_1.IsApproved = N'N')"></asp:SqlDataSource>
                        <table class="DataEntryTable">
                            <tr>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>
                                    <asp:Button ID="btn_approveourStyle" runat="server" OnClick="btn_approveourStyle_Click" Text="Approve All selected" />
                                </td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                                <td colspan="2">
                                    <asp:Button ID="btn_forwardourstylw" runat="server" OnClick="btn_forwardourstylw_Click" Text="Forward Costing For Management Approval" />
                                </td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
    </div>
      
            
      




</asp:Content>


<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="CostingApproval.aspx.cs" Inherits="ArtWebApp.Approvals.CostingApproval" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../css/style.css" rel="stylesheet" />
    <style type="text/css">
        .auto-style8 {
            width: 100%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    

    <asp:MultiView ID="MultiView1" runat="server">

        <asp:View ID="View1" runat="server">
            <table class="auto-style1">
        <tr class="RedHeadding">
            <td>Costing&nbsp; Approval</td>
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
                <table class="DataEntryTable">
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
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT StyleCostingMaster.Costing_PK, AtcDetails.OurStyle, AtcDetails.BuyerStyle, 000.000 AS Cost, StyleCostingMaster.CreatedBy, StyleCostingMaster.CreatedDate, StyleCostingMaster.ApprovedBy, StyleCostingMaster.ApprovedDate, StyleCostingMaster.CostingCount,  StyleCostingMaster.IsFowarded,StyleCostingMaster.IsApplicable, StyleCostingMaster.IsSubmitted, StyleCostingMaster.IsAccountable, StyleCostingMaster.FOB, StyleCostingMaster.MarginValue, StyleCostingMaster.Margin FROM StyleCostingMaster INNER JOIN AtcDetails ON StyleCostingMaster.OurStyleID = AtcDetails.OurStyleID WHERE (StyleCostingMaster.IsApproved = N'N') AND (StyleCostingMaster.IsSubmitted = N'Y') AND (StyleCostingMaster.IsLast= N'Y') order by StyleCostingMaster.Costing_PK desc"></asp:SqlDataSource>
            </td>
        </tr>

    </table>
        </asp:View>
        <asp:View ID="View2" runat="server">
            <table class="auto-style8">
                <tr class="RedHeadding">
                    <td>OurStyle Approval</td>
                </tr>
                <tr>
                    <td>
                        <asp:GridView ID="tbl_podetails" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4" DataKeyNames="OurStyleApproval_PK" DataSourceID="OurStyleData" Font-Size="Large" style="font-size: x-small; font-family: Calibri" Width="100%">
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="checkAll" runat="server" onclick="checkAll(this)" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chk_select" runat="server" onclick="Check_Click(this)" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                                  <asp:BoundField DataField="OurStyleApproval_PK" HeaderText="OurStyleApproval_PK" SortExpression="OurStyleApproval_PK" />

                                <asp:TemplateField HeaderText="OurStyleID" InsertVisible="False" SortExpression="OurStyleID" ItemStyle-CssClass="hidden" ControlStyle-CssClass="hidden" FooterStyle-CssClass="hidden">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_OurStyleID" runat="server" Text='<%# Bind("OurStyleID") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="AtcId" HeaderText="AtcId" SortExpression="AtcId" ItemStyle-CssClass="hidden" ControlStyle-CssClass="hidden" FooterStyle-CssClass="hidden" />
                                <asp:BoundField DataField="OurStyle" HeaderText="OurStyle" SortExpression="OurStyle" />
                                <asp:BoundField DataField="BuyerStyle" HeaderText="BuyerStyle" SortExpression="BuyerStyle" />
                                <asp:BoundField DataField="FOB" HeaderText="FOB" SortExpression="FOB" />
                                <asp:TemplateField HeaderText="IntialQty" SortExpression="IntialQty">
                                    <ItemTemplate>
                                        <asp:Label ID="txt_IntialQty" runat="server" Text='<%# Bind("IntialQty") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="RevisedQty" SortExpression="RevisedQty">
                                    <ItemTemplate>
                                        <asp:Label ID="txt_qty" runat="server" Text='<%# Bind("Quantity") %>'></asp:Label>
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
                        <asp:SqlDataSource ID="OurStyleData" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT    AtcDetailApproval.OurStyleApproval_PK,  AtcDetails.OurStyleID, AtcDetails.OurStyle, AtcDetails.BuyerStyle, ISNULL((
SELECT      sum(  Quantity)
FROM            AtcDetailApproval
WHERE        (IsFirst = N'Y') AND (OurStyleID = AtcDetailApproval.OurStyleID)),0) AS IntialQty, AtcDetails.AtcId, AtcDetailApproval.Quantity, AtcDetails.FOB, AtcDetailApproval.IsForwarded, 
                         AtcDetailApproval.IsApproved
FROM            AtcDetails INNER JOIN
                         AtcDetailApproval ON AtcDetails.OurStyleID = AtcDetailApproval.OurStyleID
WHERE         (AtcDetailApproval.IsApproved = N'N')"></asp:SqlDataSource>
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
        </asp:View>
    </asp:MultiView>




</asp:Content>

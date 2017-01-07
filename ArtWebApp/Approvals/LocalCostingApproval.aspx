<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="LocalCostingApproval.aspx.cs" Inherits="ArtWebApp.Approvals.LocalCostingApproval" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="auto-style1">
        <tr>
            <td>&nbsp;</td>
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
</asp:Content>
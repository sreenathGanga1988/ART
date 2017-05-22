<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="InventoryTracker.aspx.cs" Inherits="ArtWebApp.Reports.Inventoryreport.InventoryTracker" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:MultiView ID="MultiView1" runat="server">

        <asp:View ID="View1" runat="server">
            <table class="auto-style1">
        <tr class="RedHeadding">
            <td>Loan Tracker</td>
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
                            &nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td colspan="2">
                            &nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                </table>
            </td>
        </tr>

        <tr>
            <td>
                &nbsp;</td>
        </tr>

    </table>
        </asp:View>
        <asp:View ID="View2" runat="server">
            <table class="auto-style8">
                <tr class="RedHeadding">
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td>
                        <table class="DataEntryTable">
                            <tr>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                                <td colspan="2">
                                    &nbsp;</td>
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

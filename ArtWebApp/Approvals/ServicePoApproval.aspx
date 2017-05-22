<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="ServicePoApproval.aspx.cs" Inherits="ArtWebApp.Approvals.ServicePoApproval" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table>
                <tr>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>
                        <asp:GridView ID="tbl_servicePO" runat="server" AutoGenerateColumns="False" DataKeyNames="ServicePO_PK" OnRowCommand="tbl_servicePO_RowCommand" BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2">
                            <Columns>
                                 <asp:TemplateField HeaderImageUrl="~/Image/tick.jpg">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chk_select" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                                <asp:BoundField DataField="ServicePO_PK" HeaderText="ServicePO_PK" InsertVisible="False" ReadOnly="True" SortExpression="ServicePO_PK" />
                                <asp:BoundField DataField="ServicePOnumber" HeaderText="ServicePOnumber" SortExpression="ServicePOnumber" />
                                <asp:BoundField DataField="DebitFrom" HeaderText="DebitFrom" HtmlEncode="False" SortExpression="DebitFrom" />
                                <asp:BoundField DataField="DebitName" HeaderText="DebitName" ReadOnly="True" SortExpression="DebitName" />                             
                                <asp:BoundField DataField="ServiceType" HeaderText="ServiceType" SortExpression="ServiceType" />
                                <asp:BoundField DataField="Amount" HeaderText="Amount" SortExpression="Amount" />
                                <asp:BoundField DataField="CurrencyCode" HeaderText="CurrencyCode" SortExpression="CurrencyCode" />
                                <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description" />
                                <asp:BoundField DataField="AddedDate" HeaderText="AddedDate" SortExpression="AddedDate" />
                                <asp:BoundField DataField="AddedBy" HeaderText="AddedBy" SortExpression="AddedBy" />
                                
                                <asp:ButtonField ButtonType="Button" CommandName="Approve" HeaderText="Approve" Text="Approve" />
                                <asp:ButtonField CommandName="Reject" HeaderText="Delete" Text="Delete" />
                                <asp:ButtonField CommandName="Show" HeaderText="Show" Text="Show" />
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

                          <asp:Button ID="btn_approveAll" runat="server" OnClick="btn_approveAll_Click" Text="Approve All selected Service PO" />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

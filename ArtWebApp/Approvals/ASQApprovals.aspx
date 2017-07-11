<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="ASQApprovals.aspx.cs" Inherits="ArtWebApp.Approvals.ASQApprovals" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>

        <asp:GridView ID="tbl_asqShuffledetails" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4" ShowHeaderWhenEmpty="True" style="font-size: small; font-family: Calibri; font-weight: 400;" Width="100%" DataSourceID="AsqShuffleDatasource" DataKeyNames="AsqShuffle_PK">
                        <Columns>
                               <asp:TemplateField HeaderImageUrl="~/Image/tick.jpg">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chk_select" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:BoundField DataField="AsqShuffle_PK" HeaderText="AsqShuffle_PK" SortExpression="AsqShuffle_PK" />
                               
                            <asp:BoundField DataField="PoPacknum" HeaderText="PoPacknum" SortExpression="PoPacknum" />
                            <asp:BoundField DataField="OurStyle" HeaderText="OurStyle" SortExpression="OurStyle" />
                            <asp:BoundField DataField="AddedQty" HeaderText="AddedQty" ReadOnly="True" SortExpression="AddedQty" />
                            <asp:BoundField DataField="AsqShuffleNum" HeaderText="AsqShuffleNum" SortExpression="AsqShuffleNum" />
                            <asp:BoundField DataField="ASQShuffleGroup" HeaderText="ASQShuffleGroup" SortExpression="ASQShuffleGroup" />
                            <asp:BoundField DataField="AddedBY" HeaderText="AddedBY" SortExpression="AddedBY" />
                            <asp:BoundField DataField="AddedDate" HeaderText="AddedDate" SortExpression="AddedDate" />
                            <asp:BoundField DataField="IsApproved" HeaderText="IsApproved" SortExpression="IsApproved" />
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

    </div>

    <div>




        <asp:Button ID="btn_asqShuffle" runat="server" OnClick="btn_asqShuffle_Click" Text="Approve AsqShuffle" />




    </div>
          
    <asp:SqlDataSource ID="AsqShuffleDatasource" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT        ASQShuffleMaster.AsqShuffle_PK, PoPackMaster.PoPacknum, AtcDetails.OurStyle, SUM(ASQShuffleDetails.AddedQty) AS AddedQty, ASQShuffleMaster.AsqShuffleNum, ASQShuffleMaster.ASQShuffleGroup, 
                         ASQShuffleMaster.AddedBY, ASQShuffleMaster.AddedDate, ASQShuffleMaster.IsApproved
FROM            PoPackMaster INNER JOIN
                         ASQShuffleMaster INNER JOIN
                         ASQShuffleDetails ON ASQShuffleMaster.AsqShuffle_PK = ASQShuffleDetails.AsqShuffle_PK ON PoPackMaster.PoPackId = ASQShuffleMaster.FromPOPackID INNER JOIN
                         AtcDetails ON ASQShuffleMaster.OurStyleID = AtcDetails.OurStyleID INNER JOIN
                         AtcMaster ON AtcDetails.AtcId = AtcMaster.AtcId
GROUP BY ASQShuffleMaster.AsqShuffle_PK, PoPackMaster.PoPacknum, AtcDetails.OurStyle, ASQShuffleMaster.AsqShuffleNum, ASQShuffleMaster.ASQShuffleGroup, ASQShuffleMaster.AddedBY, 
                         ASQShuffleMaster.AddedDate, ASQShuffleMaster.IsApproved
HAVING        (ASQShuffleMaster.IsApproved = N'N')"></asp:SqlDataSource>
</asp:Content>

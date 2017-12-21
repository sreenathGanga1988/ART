<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="CutOrderClose.aspx.cs" Inherits="ArtWebApp.Production.CutOrder.CutOrderClose" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../../JQuery/GridJQuery.js"></script>
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
    </style>
    <link href="../../css/style.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="RedHeaddingdIV">Cut Order Close</div>
    <div>
        <table class="auto-style1">
            <tr>
                <td>From</td>
                <td>
                    <asp:TextBox ID="dtp_deliverydate" runat="server" Width="180px"></asp:TextBox>
                    <asp:CalendarExtender ID="dtp_deliverydate_CalendarExtender" runat="server" Enabled="True" Format="dd/MMM/yyyy" TargetControlID="dtp_deliverydate" />
                </td>
                <td>To </td>
                <td>
                    <asp:TextBox ID="dtp_deliverydate0" runat="server" Width="180px"></asp:TextBox>
                    <asp:CalendarExtender ID="dtp_deliverydate_CalendarExtender0" runat="server" Enabled="True" Format="dd/MMM/yyyy" TargetControlID="dtp_deliverydate0" />
                </td>
                <td>Location</td>
                <td>
                    <ucc:DropDownListChosen ID="DropDownList1" runat="server" DataSourceID="locationmaster" DataTextField="LocationName" DataValueField="Location_PK" DisableSearchThreshold="10">
                    </ucc:DropDownListChosen>
                    <asp:SqlDataSource ID="locationmaster" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT [Location_PK], [LocationName] FROM [LocationMaster]"></asp:SqlDataSource>
                </td>
                <td>
                    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="S" />
                </td>
            </tr>
        </table>
    </div>
    <div>

        
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"  DataKeyNames="CutID" DataSourceID="SqlDataSource1" BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4" ShowFooter="True" ShowHeaderWhenEmpty="True" style="font-size: small; font-family: Calibri; font-weight: 400;" Width="100%">
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>
                    <asp:TemplateField>  
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="checkAll" runat ="server" onclick="checkAll(this)"/>
                                    </HeaderTemplate>                                 
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chk_select" runat="server" onclick="Check_Click(this)"/>
                                    </ItemTemplate>
                                </asp:TemplateField>    
                <asp:TemplateField HeaderText="CutID" InsertVisible="False" SortExpression="CutID">
                  
                    <ItemTemplate>
                        <asp:Label ID="lbl_cutid" runat="server" Text='<%# Bind("CutID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Cut_NO" HeaderText="Cut_NO" SortExpression="Cut_NO" />
                <asp:BoundField DataField="FabQty" HeaderText="FabQty" SortExpression="FabQty" />
                <asp:BoundField DataField="CutQty" HeaderText="CutQty" SortExpression="CutQty" />
                <asp:BoundField DataField="Color" HeaderText="Color" SortExpression="Color" />
                 <asp:BoundField DataField="LocationName" HeaderText="LocationName" SortExpression="LocationName" />
                 <asp:BoundField DataField="IsClosed" HeaderText="IsClosed" SortExpression="IsClosed" />
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
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT        CutOrderMaster.CutID, CutOrderMaster.Cut_NO, CutOrderMaster.FabQty, CutOrderMaster.CutQty, CutOrderMaster.Color, LocationMaster.LocationName, CutOrderMaster.IsClosed, LocationMaster.Location_PK, 
                         CutOrderMaster.CutOrderDate
FROM            CutOrderMaster INNER JOIN
                         LocationMaster ON CutOrderMaster.ToLoc = LocationMaster.Location_PK
WHERE        (CutOrderMaster.IsClosed = N'N') AND (LocationMaster.Location_PK = @Locpk) AND (CutOrderMaster.CutOrderDate BETWEEN @fromdate AND @todate)">
            <SelectParameters>
                <asp:ControlParameter ControlID="DropDownList1" Name="Locpk" PropertyName="SelectedValue" />
                <asp:ControlParameter ControlID="dtp_deliverydate" Name="fromdate" PropertyName="Text" />
                <asp:ControlParameter ControlID="dtp_deliverydate0" Name="todate" PropertyName="Text" />
            </SelectParameters>
        </asp:SqlDataSource>
    </div>
    <div>
        <asp:Button ID="btn_close" runat="server" CausesValidation="False" OnClick="btn_close_Click" Text="Close" />
    </div>
    <div>
    </div>
</asp:Content>

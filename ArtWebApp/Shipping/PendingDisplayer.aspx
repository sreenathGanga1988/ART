<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="PendingDisplayer.aspx.cs" Inherits="ArtWebApp.Shipping.PendingDisplayer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2" DataSourceID="pendingawsource">
        <Columns>
            <asp:BoundField DataField="ContainerNumber" HeaderText="ContainerNumber" SortExpression="ContainerNumber" />
            <asp:BoundField DataField="DeliveryDate" HeaderText="DeliveryDate" SortExpression="DeliveryDate" />
            <asp:BoundField DataField="DONum" HeaderText="DONum" SortExpression="DONum" />
            <asp:BoundField DataField="AddedBy" HeaderText="AddedBy" SortExpression="AddedBy" />
            <asp:BoundField DataField="AddedDate" HeaderText="AddedDate" SortExpression="AddedDate" />
            <asp:BoundField DataField="AtcNum" HeaderText="AtcNum" SortExpression="AtcNum" />
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
    <asp:SqlDataSource ID="pendingawsource" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT DeliveryOrderMaster.ContainerNumber, DeliveryOrderMaster.DeliveryDate, DeliveryOrderMaster.DONum, DeliveryOrderMaster.AddedBy, DeliveryOrderMaster.AddedDate, AtcMaster.AtcNum FROM DeliveryOrderMaster INNER JOIN AtcMaster ON DeliveryOrderMaster.AtcID = AtcMaster.AtcId LEFT OUTER JOIN ShippingDocumentDODetails ON DeliveryOrderMaster.DO_PK = ShippingDocumentDODetails.DO_PK WHERE (DeliveryOrderMaster.DONum LIKE 'AWATRW%') GROUP BY DeliveryOrderMaster.ContainerNumber, DeliveryOrderMaster.DeliveryDate, ShippingDocumentDODetails.ShippingDocumentDO_PK, DeliveryOrderMaster.DONum, DeliveryOrderMaster.AddedBy, DeliveryOrderMaster.AddedDate, AtcMaster.AtcNum HAVING (DeliveryOrderMaster.DeliveryDate &gt; CONVERT (DATETIME, '2016-12-20 00:00:00', 102)) AND (ShippingDocumentDODetails.ShippingDocumentDO_PK IS NULL) AND (DeliveryOrderMaster.ContainerNumber = @Param1)">
        <SelectParameters>
            <asp:QueryStringParameter Name="Param1" QueryStringField="refnum" />
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>

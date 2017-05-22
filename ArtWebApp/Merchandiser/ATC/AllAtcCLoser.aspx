<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="AllAtcCLoser.aspx.cs" Inherits="ArtWebApp.Merchandiser.ATC.AllAtcCLoser" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../css/style.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="FullTable">
        <table class="DataEntryTable">
            <tr>
                <td class="auto-style8">
                    <asp:UpdatePanel ID="upd_main" runat="server">
                        <ContentTemplate>
                            <%-- <ig:WebDropDown ID="cmb_ourstyle" runat="server" Width="189px" TextField="name"
        DropDownContainerHeight="300px" EnableDropDownAsChild="false"
        DropDownContainerWidth="200px" DropDownAnimationType="EaseOut" EnablePaging="True"
        PageSize="12" Height="22px" ValueField="pk" CurrentValue="Select OurStyle" AutoPostBack="True" OnDataBound="cmb_ourstyle_DataBound" OnValueChanged="cmb_ourstyle_ValueChanged">
                                            <DropDownItemBinding TextField="name" ValueField="pk" />
                                        </ig:WebDropDown>--%>
                            <asp:GridView ID="tbl_podata" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4" DataKeyNames="AtcId" DataSourceID="AllAtc" Font-Size="Smaller" style="font-size: small; font-family: Calibri; font-weight: 400;" Width="100%">
                                <AlternatingRowStyle BackColor="White" />
                                <Columns>
                                    <asp:BoundField DataField="AtcId" HeaderText="AtcId" InsertVisible="False" ReadOnly="True" SortExpression="AtcId" />
                                    <asp:BoundField DataField="AtcNum" HeaderText="AtcNum" SortExpression="AtcNum" />
                                    <asp:BoundField DataField="BuyerName" HeaderText="BuyerName" SortExpression="BuyerName" />
                                    <asp:BoundField DataField="NoofStyles" HeaderText="NoofStyles" SortExpression="NoofStyles" />
                                    <asp:TemplateField HeaderText="PendingASQ" SortExpression="PendingASQ">
                                        
                                        <ItemTemplate>
                                            <asp:HyperLink ID="Label1" runat="server" Font-Underline="true" Text='<%# Bind("PendingASQ") %>'></asp:HyperLink>
                                        </ItemTemplate>
                                    </asp:TemplateField>
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
                            </asp:GridView>        <asp:LinkButton ID="lnkFake" runat="server"></asp:LinkButton>
                         <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="lnkFake" CancelControlID="btnClose" 


 


PopupControlID="Panel1" DropShadow="True">


 


</asp:ModalPopupExtender>


 


<asp:Panel ID="Panel1" runat="server" CssClass="modalPopup" align="center" style = "display:none">

      <asp:UpdatePanel ID="upd_subgrid"   UpdateMode="Conditional" runat="server">
                     <ContentTemplate>
   <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4" ShowHeaderWhenEmpty="True" style="font-size: small; font-family: Calibri" Width="400px" ShowFooter="True">
                                <Columns>

                                    
                                    <asp:BoundField DataField="MrnNum" HeaderText="MrnNum" />
                                    <asp:BoundField DataField="AddedDate" HeaderText="AddedDate"  DataFormatString="{0:MM/dd/yyyy}"/>
                                    <asp:BoundField DataField="ReceiptQty" HeaderText="ReceiptQty" />
                                    <asp:BoundField DataField="ExtraQty" HeaderText="ExtraQty" />
                                                           
                                    
                                    

                                   
                                </Columns>
                                <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" Font-Bold="true" />
                                <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="#FFFFCC" />
                                <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
                                <RowStyle BackColor="White" ForeColor="#330099" />
                                <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
                                <SortedAscendingCellStyle BackColor="#FEFCEB" />
                                <SortedAscendingHeaderStyle BackColor="#AF0101" />
                                <SortedDescendingCellStyle BackColor="#F6F0C0" />
                                <SortedDescendingHeaderStyle BackColor="#7E0000" />
                            </asp:GridView> <br />
    <asp:Button ID="btnClose" runat="server" Text="Close" />
                          </ContentTemplate>
                            </asp:UpdatePanel>
</asp:Panel>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td class="NormalTD">
                    <table class="DataEntryTable">
                        <tr>
                            <td>&nbsp;</td>
                            <td>
                                <asp:Button ID="btn_closeatc" runat="server" Enabled="False" OnClick="Button1_Click" Text="Close Atc" />
                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <div id="Messaediv" runat="server">
                                    <asp:Label ID="lbl_msg" runat="server" Text="*"></asp:Label>
                                </div>
                            </td>
                            <td>&nbsp;</td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <asp:SqlDataSource ID="AllAtc" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT        AtcMaster.AtcId, AtcMaster.AtcNum, BuyerMaster.BuyerName, AtcMaster.NoofStyles,
                             (SELECT        COUNT(PoPackId) AS Expr1
                               FROM            (SELECT        PoPackMaster.PoPackId, SUM(POPackDetails.PoQty) AS POQty, ISNULL
                                                                                       ((SELECT        SUM(ShipmentHandOverDetails.ShippedQty) AS Expr1
                                                                                           FROM            ShipmentHandOverDetails INNER JOIN
                                                                                                                    JobContractDetail ON ShipmentHandOverDetails.JobContractDetail_pk = JobContractDetail.JobContractDetail_pk
                                                                                           GROUP BY JobContractDetail.PoPackID, JobContractDetail.OurStyleID
                                                                                           HAVING        (JobContractDetail.PoPackID = PoPackMaster.PoPackId) AND (JobContractDetail.OurStyleID = POPackDetails.OurStyleID)), 0) AS ShipedQty, 
                                                                                   PoPackMaster.FirstDeliveryDate, PoPackMaster.HandoverDate, MAX(POPackDetails.IsShortClosed) AS Expr1
                                                         FROM            PoPackMaster INNER JOIN
                                                                                   POPackDetails ON PoPackMaster.PoPackId = POPackDetails.POPackId
                                                         GROUP BY PoPackMaster.PoPackId, POPackDetails.OurStyleID, PoPackMaster.FirstDeliveryDate, PoPackMaster.HandoverDate, PoPackMaster.AtcId
                                                         HAVING         (PoPackMaster.AtcId = AtcMaster.AtcId) AND (MAX(POPackDetails.IsShortClosed) &lt;&gt; N'Y')) AS tt
                               WHERE        (POQty - ShipedQty &gt; 0)) AS PendingASQ, AtcMaster.IsClosed
FROM            AtcMaster INNER JOIN
                         BuyerMaster ON AtcMaster.Buyer_ID = BuyerMaster.BuyerID
ORDER BY AtcMaster.AtcId"></asp:SqlDataSource>
        <br />
    </div>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="VoucherReports.aspx.cs" Inherits="ArtWebApp.Reports.AccountsReport.VoucherReports" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
    

    </style>
    <link href="../../css/style.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="FullTable">
    <tr>
        <td>
             <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>

            <table class="DataEntryTable">
                <tr>
                    <td class="RedHeadding" colspan="12">Voucher&nbsp; Reports</td>
                </tr>
                <tr>
                    <td >Voucher Type</td>
                    <td >
                        <ucc:DropDownListChosen ID="drp_vouchertype" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownListChosen1_SelectedIndexChanged" Width="200px">
                            <asp:ListItem>PUR</asp:ListItem>
                            <asp:ListItem>INV</asp:ListItem>
                            <asp:ListItem>SPUR</asp:ListItem>
                        </ucc:DropDownListChosen>
                    </td>
                    <td>
                        &nbsp;</td>
                    <td>&nbsp;</td>
                    <td >&nbsp;</td>
                    <td >&nbsp;</td>
                    <td >&nbsp;</td>
                    <td >&nbsp;</td>
                    <td>&nbsp;</td>
                    <td >&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>Voucher Number</td>
                    <td>
                        <ucc:DropDownListChosen ID="drp_voucher" runat="server" Width="200px" DataTextField="name" DataValueField="pk">
                        </ucc:DropDownListChosen>
                    </td>
                    <td>
                        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Show" />
                    </td>
                    <td>
                        
                    </td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td >&nbsp;</td>
                    <td >
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>&nbsp;</td>
                    <td >&nbsp;</td>
                    <td >&nbsp;</td>
                    <td >&nbsp;</td>
                    <td >&nbsp;</td>
                    <td>&nbsp;</td>
                    <td >&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
            </table>


                       </ContentTemplate>

            </asp:UpdatePanel>

        </td>
    </tr>
    <tr>
        <td class="ReportViewSection">
            <asp:UpdatePanel ID="upl_rpt" runat="server">
                <ContentTemplate>
                    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="100%">
                    </rsweb:ReportViewer>
                </ContentTemplate>

            </asp:UpdatePanel>

        </td>
    </tr>
    <tr>
        <td><asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" 
                    SelectCommand="SELECT DISTINCT AtcNum, AtcId FROM AtcMaster WHERE (IsClosed = N'N') ORDER BY AtcNum, AtcId">
                </asp:SqlDataSource>
            <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT        AtcId, AtcNum, 0.0 as WFValue,0.0 as WFFabricvalue,
0.0 as WFTrimsValue,0.0 as PackedQty,0.0 as PackedQtyValue,0.0 as PackedQtyFabricvalue,
0.0 as PackedQtyTrimsValue,0.0 as PackedQtyProcessValue,0.0 as InvoicedQty,0.0 as InvoicedQtyValue,0.0 as InvoicedQtyFabricvalue,
0.0 as InvoicedQtyTrimsValue,0.0 as InvoicedQtyProcessValue ,0.0 as WIPWFValue,0.0 as WIPFGValue,0.0 as WIPInvoicedValue
FROM            AtcMaster "></asp:SqlDataSource>
        </td>
    </tr>
</table>
</asp:Content>
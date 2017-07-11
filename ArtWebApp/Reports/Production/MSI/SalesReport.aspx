<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="SalesReport.aspx.cs" Inherits="ArtWebApp.Reports.Production.MSI.SalesReport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="DataEntryTable">
        <tr>
            <td class="RedHeadding" colspan="12">Sales/Shipment Report</td>
        </tr>
        <tr>
            <td>From </td>
            <td>
                <ig:WebDatePicker ID="dtp_from" runat="server">
                </ig:WebDatePicker>
            </td>
            <td>To </td>
            <td>
                <ig:WebDatePicker ID="dtp_to" runat="server">
                </ig:WebDatePicker>
            </td>
            <td>
                <asp:Button ID="btn_sales" runat="server" Text="Sales Report" OnClick="btn_sales_Click" />
            </td>
            <td>
                <asp:Button ID="btn_Shipmenthandover" runat="server" Text="SHO Report" ToolTip="Shows shipment handover within a period" OnClick="btn_Shipmenthandover_Click" />
            </td>
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
                <td colspan="12" class="ReportViewSection">
            <asp:UpdatePanel ID="upl_rpt" runat="server">
                <ContentTemplate>
                    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="100%">
                        <LocalReport ReportPath="Reports\RDLC\APO.rdlc">
                        </LocalReport>
                    </rsweb:ReportViewer>
                </ContentTemplate>

            </asp:UpdatePanel>

        </td>

            
        </tr>
    </table>
</asp:Content>

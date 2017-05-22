<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="WIPReport.aspx.cs" Inherits="ArtWebApp.Reports.AccountsReport.WIPReport" %>

<%@ Register assembly="Infragistics35.Web.v12.1, Version=12.1.20121.2236, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.ListControls" tagprefix="ig" %>
<%--<%@ Register assembly="Microsoft.ReportViewer.WebForms" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>--%>
<%--<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>--%>

<%--<%@ Register assembly="Microsoft.ReportViewer.WebForms" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
 


    <link href="../../css/style.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="FullTable">
    <tr>
        <td class="DataEntryTable">
             <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>

            <table class="DataEntryTable">
                <tr>
                    <td class="RedHeadding" colspan="12">WIP&nbsp; Reports</td>
                </tr>
                <tr>
                    <td >Upto</td>
                    <td >
                       <ig:WebDatePicker ID="dtp_to" runat="server">
                        </ig:WebDatePicker></td>
                    <td class="NormalTD">
                        &nbsp;</td>
                   <td class="NormalTD"></td>
                    <td class="NormalTD">
                        <asp:Button ID="Button2" runat="server" Font-Size="XX-Small" OnClick="Button2_Click" Text="Show  All Atc Costing Data" />
                    </td>
                    <td class="NormalTD"></td>
                    <td class="NormalTD"></td>
                    <td class="NormalTD"></td>
                   <td class="NormalTD"></td>
                    <td class="NormalTD"></td>
                   <td class="NormalTD"></td>
                   <td class="NormalTD"></td>
                </tr>
                <tr>
                    <td class="NormalTD">Atc #</td>
                    <td class="NormalTD">
                        <ig:WebDropDown ID="cmb_atc" runat="server" CurrentValue="Select Atc" DataSourceID="SqlDataSource1" DropDownAnimationType="EaseInOut" EnableClosingDropDownOnSelect="False" EnableMultipleSelection="True" Font-Names="Calibri" Font-Size="X-Small" Height="24px" TextField="AtcNum" ValueField="AtcId" Width="190px">
                            <Items>
                                <ig:DropDownItem Selected="False" Text="DropDown Item" Value="">
                                </ig:DropDownItem>
                            </Items>
                            <DropDownItemBinding TextField="AtcNum" ValueField="AtcId" />
                        </ig:WebDropDown>
                    </td>
                    <td class="NormalTD">
                        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Show  wip" Width="112px" Font-Size="XX-Small" style="height: 21px" />
                    </td>
                    <td class="NormalTD">
                        
                    </td>
                   <td class="NormalTD">
                       <asp:Button ID="Button3" runat="server" Font-Size="XX-Small" Text="Show  Selected  Atc Costing Data" />
                    </td>
                   <td class="NormalTD"></td>
                   <td class="NormalTD"></td>
                   <td class="NormalTD"></td>
                   <td class="NormalTD"></td>
                   <td class="NormalTD"></td>
                   <td class="NormalTD"></td>
                   <td class="NormalTD"></td>
                </tr>
                <tr>
                    <td class="NormalTD"></td>
                    <td class="NormalTD"></td>
                    <td class="NormalTD"></td>
                    <td class="NormalTD"></td>
                    <td class="NormalTD"></td>
                    <td class="NormalTD"></td>
                    <td class="NormalTD"></td>
                    <td class="NormalTD"></td>
                    <td class="NormalTD"></td>
                    <td class="NormalTD"></td>
                    <td class="NormalTD"></td>
                    <td class="NormalTD"></td>
                </tr>
                <tr>
                    <td class="NormalTD"></td>
                    <td >
                        &nbsp;</td>
                    <td class="NormalTD">
                        &nbsp;</td>
                   <td class="NormalTD"></td>
                    <td class="NormalTD"></td>
                    <td class="NormalTD"></td>
                    <td class="NormalTD"></td>
                    <td class="NormalTD"></td>
                   <td class="NormalTD"></td>
                    <td class="NormalTD"></td>
                   <td class="NormalTD"></td>
                   <td class="NormalTD"></td>
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
                    <rsweb:ReportViewer ID="ReportViewer1" runat="server"  Width="100%">
                    </rsweb:ReportViewer>
                </ContentTemplate>

            </asp:UpdatePanel>

        </td>
    </tr>
    <tr>
        <td class="NormalTD"><asp:SqlDataSource ID="SqlDataSource1" runat="server" 
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


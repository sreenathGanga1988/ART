<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="ShippingImportReports.aspx.cs" Inherits="ArtWebApp.Reports.ShippingReport.ShippingImportReports" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
    

    </style>
    <link href="../../css/style.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="FullTable">
    <tr>
        <td>
             <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>

            <table class="DataEntryTable">
                <tr>
                    <td class="RedHeadding" colspan="12">Shipping&nbsp; Reports</td>
                </tr>
                <tr>
                    <td class="NormalTD" >IMP #</t>
                    <td class="NormalTD" >
                        <ucc:DropDownListChosen ID="drp_imp" runat="server" Width="180px" DataTextField="name" DataValueField="pk">
                        </ucc:DropDownListChosen>
                    </td>
                    <td class="NormalTD">
                        <asp:Button ID="btn_showPO" runat="server" OnClick="btn_showPO_Click" Text="Show IMPDetails" Width="160px" ToolTip="Show All the Details against the seleted IMP" Font-Size="Smaller" />
                    </td>
                    <td class="NormalTD">&nbsp;</td>
                    <td >&nbsp;</td>
                    <td >&nbsp;</td>
                    <td >&nbsp;</td>
                    <td >&nbsp;</td>
                    <td class="NormalTD">&nbsp;</td>
                    <td >&nbsp;</td>
                    <td class="NormalTD">&nbsp;</td>
                    <td class="NormalTD">&nbsp;</td>
                </tr>
                <tr>
                   <td class="NormalTD">Doc#</td>
                    <td class="NormalTD"><ucc:DropDownListChosen ID="drp_doc" runat="server" Width="180px" DataTextField="name" DataValueField="pk">
                        </ucc:DropDownListChosen></td>
                    <td class="NormalTD"><asp:Button ID="btn_showDoc" runat="server"  Text="Show DOC" Width="160px" ToolTip="Show All the  details against the seleted DOC" Font-Size="Smaller" OnClick="btn_showDoc_Click" Height="21px" /></td>
                    <td class="NormalTD">&nbsp;</td>
                    <td class="NormalTD">&nbsp;</td>
                    <td class="NormalTD">&nbsp;</td>
                    <td class="NormalTD">&nbsp;</td>
                    <td class="NormalTD">&nbsp;</td>
                    <td class="NormalTD">&nbsp;</td>
                    <td class="NormalTD">&nbsp;</td>
                    <td class="NormalTD">&nbsp;</td>
                    <td class="NormalTD">&nbsp;</td>
                </tr>
                <tr>
                    <td  class="RedHeadding" colspan="12">Courier Reports</td>
                </tr>
                <tr>
                    <td class="NormalTD">From </td>
                    <td class="NormalTD">  <ig:WebDatePicker ID="dtp_fromdate" runat="server" Height="23px" Width="200px">
                                    </ig:WebDatePicker></td>
                    <td class="NormalTD">To</td>
                    <td class="NormalTD">  <ig:WebDatePicker ID="dtp_todate" runat="server" Height="23px" Width="200px">
                                    </ig:WebDatePicker></td>
                    <td class="NormalTD">
                        <asp:Button ID="Button1" runat="server" Text="Show Courier Data" OnClick="Button1_Click" />
                    </td>
                   <td class="NormalTD">&nbsp;</td>
                    <td class="NormalTD">&nbsp;</td>
                    <td class="NormalTD">&nbsp;</td>
                    <td class="NormalTD">&nbsp;</td>
                    <td class="NormalTD">&nbsp;</td>
                    <td class="NormalTD">&nbsp;</td>
                    <td class="NormalTD">&nbsp;</td>
                </tr>
            </table>


                       </ContentTemplate>

            </asp:UpdatePanel>

        </td>
    </tr>
    <tr>
        <td class="ReportViewSection">
            <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="100%">
            </rsweb:ReportViewer>
        </td>
    </tr>
    <tr>
        <td>&nbsp;</td>
    </tr>
</table>
</asp:Content>


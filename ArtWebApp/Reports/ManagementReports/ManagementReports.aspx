<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="ManagementReports.aspx.cs" Inherits="ArtWebApp.Reports.ManagementReports.ManagementReports" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
     
       
     
    .auto-style8 {
        height: 27px;
    }
     
       
     
    </style>
    <link href="../../css/style.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <table class="DataEntryTable">
        <tr>
            <td class="NormalTD">&nbsp;</td>
            <td class="NormalTD">&nbsp;</td>
            <td class="NormalTD">&nbsp;</td>
            <td class="NormalTD">&nbsp;</td>
            <td class="NormalTD">&nbsp;</td>
            <td class="NormalTD">&nbsp;</td>
        </tr>
        <tr>
            <td class="NormalTD">
                <asp:Button ID="Button1" runat="server" Font-Size="Smaller" Text="Show Profitability Report" ToolTip="Show the Profitability of All Atc" OnClick="Button1_Click" />
            </td>
            <td class="NormalTD">&nbsp;</td>
            <td class="NormalTD">&nbsp;</td>
            <td class="NormalTD">&nbsp;</td>
            <td class="NormalTD">&nbsp;</td>
            <td class="NormalTD">&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style8" colspan="6">
                AtcWise Reports
            </td>
        </tr>
        <tr>
            <td class="NormalTD">Atc:</td>
            <td class="NormalTD">
                <ig:WebDropDown ID="drp_Atc" runat="server" DropDownAnimationType="EaseOut" DropDownContainerHeight="300px" DropDownContainerWidth="200px" EnableClosingDropDownOnSelect="False" EnableDropDownAsChild="false" EnableMultipleSelection="True" Height="21px" PageSize="12" TextField="name" ValueField="pk" Width="156px">
                    <DropDownItemBinding TextField="name" ValueField="pk" />
                </ig:WebDropDown>
            </td>
            <td class="NormalTD">&nbsp;</td>
            <td class="NormalTD">&nbsp;</td>
            <td class="NormalTD">&nbsp;</td>
            <td class="NormalTD">&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style8" colspan="6">
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

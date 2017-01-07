<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="DailyReceiptReport.aspx.cs" Inherits="ArtWebApp.Reports.DailyReceiptReport" %>
<%@ Register assembly="Infragistics35.Web.v12.1, Version=12.1.20121.2236, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.ListControls" tagprefix="ig" %>
<%@ Register assembly="Infragistics35.Web.v12.1, Version=12.1.20121.2236, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.GridControls" tagprefix="ig1" %>
<%@ Register assembly="Infragistics35.Web.v12.1, Version=12.1.20121.2236, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.EditorControls" tagprefix="ig" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
 
</style>
     <link href="../css/style.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="FullTable">
    <tr>
        <td>&nbsp;</td>
    </tr>
    <tr>
        <td>
            <table class="DataEntryTable">
                <tr>
                    <td class="NormalTD">Location :</td>
                    <td class="NormalTD">
                       

                         <ucc:DropDownListChosen ID="drp_ToWarehouse" runat="server" DataTextField="name" DataValueField="pk" Width="200px">
                                    </ucc:DropDownListChosen>
                    </td>
                    <td class="NormalTD">Date</td>
                    <td class="NormalTD">
                        <ig:WebDatePicker ID="WebDatePicker1" runat="server">
                        </ig:WebDatePicker>
                    </td>
                    <td class="NormalTD">
                        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Show" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td class="ReportViewSection">
            <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="100%">
            </rsweb:ReportViewer>
        </td>
    </tr>
</table>
</asp:Content>

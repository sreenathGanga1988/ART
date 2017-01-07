<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="GeneralReportform.aspx.cs" Inherits="ArtWebApp.Reports.General_Reports.GeneralReportform" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Height="100%" Width="100%">
</rsweb:ReportViewer>
</asp:Content>

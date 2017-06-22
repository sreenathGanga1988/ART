<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="Tester.aspx.cs" Inherits="ArtWebApp.Administrator.Tester" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style1 {
            font-weight: bold;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <strong>    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Send StyleSize" CssClass="auto-style1" Height="338px" Width="379px" />
    </strong>
<asp:SqlDataSource ID="SqlDataSource1" runat="server"></asp:SqlDataSource>
</asp:Content>

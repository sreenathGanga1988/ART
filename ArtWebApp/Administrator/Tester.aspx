<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="Tester.aspx.cs" Async="true" Inherits="ArtWebApp.Administrator.Tester" %>
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


     <strong>    <asp:Button ID="btn_getethoipiadata" runat="server"  Text="Get Ethipoia Shipment Data" CssClass="auto-style1" Height="338px" Width="379px" OnClick="btn_getethoipiadata_Click" />
    </strong>

     <strong>    <asp:Button ID="btn_laysheetdatatokenya" runat="server"  Text="Send laysheet data to Kenya" CssClass="auto-style1" Height="338px" Width="379px" OnClick="btn_laysheetdatatokenya_Click" />
    </strong>
    
     <strong>    <asp:Button ID="btn_productiondata" runat="server"  Text="Get productionData as of date" CssClass="auto-style1" Height="338px" Width="379px" OnClick="btn_productiondata_Click"  />
    </strong>
<asp:SqlDataSource ID="SqlDataSource1" runat="server"></asp:SqlDataSource>
<asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
</asp:Content>

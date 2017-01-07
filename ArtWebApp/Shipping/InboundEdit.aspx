<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="InboundEdit.aspx.cs" Inherits="ArtWebApp.Shipping.InboundEdit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../css/style.css" rel="stylesheet" />
    <style type="text/css">
    
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="FullTable">
        <tr>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td><div>

                <table class="DataEntryTable">
                    <tr>
                        <td class="NormalTD">EXP#:</td>
                        <td class="NormalTD">
                            <ucc:DropDownListChosen ID="drp_doc" runat="server" DataTextField="ShipingDoc_PK" DataValueField="ShipDocNum" Width="200px" DataSourceID="Expdatasource" DisableSearchThreshold="10">
                            </ucc:DropDownListChosen>
                            <asp:SqlDataSource ID="Expdatasource" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT DISTINCT [ShipingDoc_PK], [ShipDocNum] FROM [ShippingDocumentMaster] ORDER BY [ShipDocNum] DESC"></asp:SqlDataSource>
                        </td>
                        <td class="SearchButtonTD">
                            <asp:Button ID="btn_show" runat="server" OnClick="btn_show_Click" Text="S" />
                        </td>
                    </tr>
                    <tr>
                         <td class="NormalTD">Doc#</td>
                        <td class="NormalTD">
                            <ig:WebDropDown ID="drp_rcpt" runat="server" EnableClosingDropDownOnSelect="False" EnableMultipleSelection="True" TextField="name" ValueField="pk" Width="200px">
                                <DropDownItemBinding TextField="name" ValueField="pk" />
                            </ig:WebDropDown>
                         </td>
                        <td class="SearchButtonTD">
                            <asp:Button ID="Button1" runat="server" Text="Add" />
                         </td>
                    </tr>
                </table>

                </div></td>
        </tr>
        <tr>
            <td>&nbsp;</td>
        </tr>
    </table>
</asp:Content>

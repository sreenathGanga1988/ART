<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="InventoryMisplacementExtra.aspx.cs" Inherits="ArtWebApp.Merchandiser.PO.InventoryMisplacementExtra" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style2 {
            height: 27px;
            width: 204px;
        }
    </style>
    <link href="../../css/style.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="DataEntryTable">
        <tr>
            <td class="NormalTD">&nbsp;</td>
            <td class="auto-style2">&nbsp;</td>
            <td class="NormalTD">&nbsp;</td>
            <td class="NormalTD">&nbsp;</td>
            <td class="NormalTD">&nbsp;</td>
            <td class="NormalTD">&nbsp;</td>
        </tr>
        <tr>
            <td class="NormalTD">Misplacement#</td>
            <td class="auto-style2">
                <ucc:DropDownListChosen ID="drp_inventory_misplacement" runat="server" DataTextField="reqnum" DataValueField="MisplaceApp_pk" Width="100%">
                </ucc:DropDownListChosen>
            </td>
            <td class="NormalTD">
                <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="S" />
            </td>
            <td class="NormalTD">&nbsp;</td>
            <td class="NormalTD">&nbsp;</td>
            <td class="NormalTD">&nbsp;</td>
        </tr>
        <tr>
            <td class="NormalTD">Misplacement Value</td>
            <td class="auto-style2">
                <asp:TextBox ID="txt_productvalue" runat="server" OnTextChanged="txt_productvalue_TextChanged"></asp:TextBox>
            </td>
            <td class="NormalTD">&nbsp;</td>
            <td class="NormalTD">&nbsp;</td>
            <td class="NormalTD">&nbsp;</td>
            <td class="NormalTD">&nbsp;</td>
        </tr>
        <tr>
            <td class="NormalTD">MOQ Value</td>
            <td class="auto-style2">
              
                <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Conditional" runat="server">
                    <ContentTemplate>
                          <asp:TextBox ID="txt_moq" runat="server" AutoPostBack="True" OnTextChanged="TextBox2_TextChanged"></asp:TextBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td class="NormalTD">&nbsp;</td>
            <td class="NormalTD">&nbsp;</td>
            <td class="NormalTD">&nbsp;</td>
            <td class="NormalTD">&nbsp;</td>
        </tr>
        <tr>
            <td class="NormalTD">Freight Charge</td>
            <td class="auto-style2">
               
                <asp:UpdatePanel ID="UpdatePanel3" UpdateMode="Conditional" runat="server">
                    <ContentTemplate>
                         <asp:TextBox ID="txt_freight" runat="server" AutoPostBack="True" OnTextChanged="TextBox3_TextChanged"></asp:TextBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td class="NormalTD">&nbsp;</td>
            <td class="NormalTD">&nbsp;</td>
            <td class="NormalTD">&nbsp;</td>
            <td class="NormalTD">&nbsp;</td>
        </tr>
        <tr>
            <td class="NormalTD">Total</td>
            <td class="auto-style2">
               
                <asp:UpdatePanel ID="upd_total" UpdateMode="Conditional"  runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txt_total" runat="server"></asp:TextBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td class="NormalTD">&nbsp;</td>
            <td class="NormalTD">&nbsp;</td>
            <td class="NormalTD">&nbsp;</td>
            <td class="NormalTD">&nbsp;</td>
        </tr>
        <tr>
            <td class="NormalTD">&nbsp;</td>
            <td class="auto-style2">
               
                <asp:Button ID="btn_save" runat="server" OnClick="btn_save_Click" Text="Submit" />
            </td>
            <td class="NormalTD">&nbsp;</td>
            <td class="NormalTD">&nbsp;</td>
            <td class="NormalTD">&nbsp;</td>
            <td class="NormalTD">&nbsp;</td>
        </tr>
    </table>
</asp:Content>

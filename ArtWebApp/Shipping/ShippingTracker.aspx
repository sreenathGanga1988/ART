<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="ShippingTracker.aspx.cs" Inherits="ArtWebApp.Shipping.ShippingTracker" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
       
    </style>
    <link href="../css/style.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="FullTable">
        <tr>
            <td><div>

                <table class="DataEntryTable">
                    <tr>
                        <td class="RedHeadding" colspan="2"><strong>eXPORT dOCUMENT tRACKER</strong></td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>ADN#:</td>
                        <td>
                            <asp:TextBox ID="txt_adn" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Button ID="BTN" runat="server" Text="s" OnClick="BTN_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td>wr#</td>
                        <td>
                            <asp:TextBox ID="txt_do" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Button ID="Button2" runat="server" Text="s" OnClick="Button2_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td>rEFNUM</td>
                        <td>
                            <asp:TextBox ID="txt_ref" runat="server" OnTextChanged="TextBox3_TextChanged"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Button ID="Button3" runat="server" Text="s" OnClick="Button3_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:GridView ID="GridView1" runat="server">
                            </asp:GridView>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                </table>

                </div></td>
        </tr>
        <tr>
            <td>WR#</td>
        </tr>
        <tr>
            <td>EXP</td>
        </tr>
    </table>
</asp:Content>

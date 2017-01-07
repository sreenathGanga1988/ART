<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="StyleCostingCopy.aspx.cs" Inherits="ArtWebApp.Merchandiser.StyleCostingCopy" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../css/style.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="FullTable">
        <tr>
            <td class="RedHeadding">Style costing copy Between ourstyle of same atc</td>
        </tr>
        <tr>
            <td>
                <table class="DataEntryTable">
                    <tr>
                        <td class="DataEntryTable">Atc # :</td>
                        <td class="NormalTD">
                            <ucc:DropDownListChosen ID="ddl_atc" runat="server" DataSourceID="AtcSource" DataTextField="AtcNum" DataValueField="AtcId" DisableSearchThreshold="10" Width="200px">
                            </ucc:DropDownListChosen>
                        </td>
                        <td>
                        <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="S" />
                        </td>
                        <td class="NormalTD">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="NormalTD">From OurStyle : </td>
                        <td class="NormalTD">
                            <ucc:DropDownListChosen ID="ddl_frmourstyle" runat="server" DataSourceID="ourstylesource" DataTextField="OurStyle" DataValueField="OurStyleID" DisableSearchThreshold="10" Width="200px">
                            </ucc:DropDownListChosen>
                        </td>
                        <td>To OurStyle : </td>
                        <td class="NormalTD">
                            <ucc:DropDownListChosen ID="ddl_toourstyle" runat="server" DataSourceID="ourstylesource" DataTextField="OurStyle" DataValueField="OurStyleID" DisableSearchThreshold="10" Width="200px">
                            </ucc:DropDownListChosen>
                        </td>
                        <td>
                            <asp:Button ID="btn_CopyCosting" runat="server" Text="Copy Rawmaterials" OnClick="btn_CopyCosting_Click" />
                        </td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lbl_costingid" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
        </tr>
        <tr class="RedHeadding">
            <td>raw material copy between different atc</td>
        </tr>
        <tr>
            <td><table class="DataEntryTable">
                    <tr>
                        <td class="DataEntryTable">from Atc # :</td>
                        <td class="NormalTD">
                            <ucc:DropDownListChosen ID="drp_frmatc" runat="server" DataSourceID="AtcSource" DataTextField="AtcNum" DataValueField="AtcId" DisableSearchThreshold="10" Width="200px">
                            </ucc:DropDownListChosen>
                        </td>
                        <td>
                        <asp:Button ID="Button1" runat="server" OnClick="Button2_Click" Text="S" />
                        </td>
                        <td class="NormalTD">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="NormalTD">to Atc : </td>
                        <td class="NormalTD">
                            <ucc:DropDownListChosen ID="drp_toatc" runat="server" DataSourceID="AtcSource" DataTextField="AtcNum" DataValueField="AtcId" DisableSearchThreshold="10" Width="200px">
                            </ucc:DropDownListChosen>
                        </td>
                        <td>&nbsp;</td>
                        <td class="NormalTD">
                            &nbsp;</td>
                        <td>
                            <asp:Button ID="btn_atccost" runat="server" Text="Copy Rawmaterials" OnClick="btn_atccost_Click" />
                        </td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="NormalTD">make sure the to atc doesnt have rawmaterials added</td>
                        <td class="NormalTD">
                            &nbsp;</td>
                        <td>&nbsp;</td>
                        <td class="NormalTD">
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                </table></td>
        </tr>
        <tr>
            <td><asp:SqlDataSource ID="AtcSource" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT AtcId, AtcNum, IsClosed FROM AtcMaster WHERE (IsClosed = N'N')"></asp:SqlDataSource>
                        <asp:HiddenField ID="hdf_atcid" runat="server" />
                        <asp:SqlDataSource ID="ourstylesource" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT [OurStyleID], [OurStyle] FROM [AtcDetails] WHERE ([AtcId] = @AtcId)">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="hdf_atcid" DefaultValue="0" Name="AtcId" PropertyName="Value" Type="Decimal" />
                            </SelectParameters>
                        </asp:SqlDataSource></td>
        </tr>
    </table>
</asp:Content>

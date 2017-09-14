<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="InventoryMisplaced.aspx.cs" Inherits="ArtWebApp.Reports.Inventoryreport.InventoryMisplaced" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../css/style.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="FullTable">
    <tr>
        <td>
             <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>

            <table class="DataEntryTable">
                <tr>
                    <td class="RedHeadding" colspan="12">Inventory MisplacedReports</td>
                </tr>
                <tr>
                    <td class="NormalTD">Factory</td>
                    <td  class="NormalTD">
                        <ucc:DropDownListChosen ID="drp_fromWarehouse" runat="server" Width="200px">
                        </ucc:DropDownListChosen>
                        
                    </td>
                    <td class="NormalTD">
                        &nbsp;</td>
                    <td class ="NormalTD">
                        <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Show All Misplacement of Factory" />
                    </td>
                    <td  class="NormalTD">&nbsp;</td>
                    <td class="NormalTD" >&nbsp;</td>
                    <td  class="NormalTD">&nbsp;</td>
                    <td class="NormalTD" >&nbsp;</td>
                    <td class="NormalTD">&nbsp;</td>
                    <td class="NormalTD" >&nbsp;</td>
                    <td class="NormalTD">&nbsp;</td>
                    <td class="NormalTD">&nbsp;</td>
                </tr>
         
               
                <tr>
                    <td class="NormalTD">Atc</td>
                    <td class="NormalTD">
                        <asp:UpdatePanel ID="upd_atc" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <ucc:DropDownListChosen ID="cmb_atc0" runat="server" DataSourceID="SqlDataSource1" DataTextField="AtcNum" DataValueField="AtcId" Height="17px" Width="200px">
                                        </ucc:DropDownListChosen>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td class="NormalTD">&nbsp;</td>
                    <td class="NormalTD">
                        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Show All Misplacement of Atc" />
                    </td>
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
                    <td class="NormalTD">Inventory Misplaced req# </td>
                    <td class="NormalTD">
                        <ucc:DropDownListChosen ID="dll_reg" runat="server" DataSourceID="MisplacedReq" DataTextField="reqnum" DataValueField="MisplaceApp_pk" DisableSearchThreshold="10" Width="200px">
                        </ucc:DropDownListChosen>
                    </td>
                    <td class="NormalTD">&nbsp;</td>
                    <td class="NormalTD">
                        <asp:Button ID="btn_showRO" runat="server" OnClick="btn_showpo_Click" Text="Show  Report" ToolTip="Show the " />
                    </td>
                    <td class="NormalTD">
                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT DISTINCT AtcNum, AtcId FROM AtcMaster WHERE (IsClosed = N'N') ORDER BY AtcNum, AtcId"></asp:SqlDataSource>
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
    <tr>
        <td><asp:SqlDataSource ID="MisplacedReq" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT reqnum, MisplaceApp_pk FROM InventoryMissingRequest">
                        </asp:SqlDataSource></td>
    </tr>
</table>
</asp:Content>


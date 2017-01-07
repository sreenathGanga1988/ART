<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="RoReports.aspx.cs" Inherits="ArtWebApp.Reports.Inventoryreport.RoReports" %>
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
                    <td class="RedHeadding" colspan="12">stock Receipt Reports</td>
                </tr>
                <tr>
                    <td class="NormalTD">RO #
                    </td>
                    <td  class="NormalTD">
                        <ucc:DropDownListChosen ID="drp_spo" runat="server" DataSourceID="ROdatasource" DataTextField="RONum" DataValueField="RO_Pk" DisableSearchThreshold="10" Width="200px">
                        </ucc:DropDownListChosen></td>
                    <td class="NormalTD">
                        &nbsp;</td>
                    <td class ="NormalTD">
                        <asp:Button ID="btn_showRO" runat="server" OnClick="btn_showpo_Click" Text="Show  RO Report" />
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
                    <td  class="NormalTD">SRO#</td>
                    <td class="NormalTD">
                        <ucc:DropDownListChosen ID="drp_smrn" runat="server" DataSourceID="SROData" DataTextField="RONum" DataValueField="SRO_Pk" DisableSearchThreshold="10" Width="200px">
                        </ucc:DropDownListChosen>
                    </td>
                    <td  class="NormalTD">
                        <asp:Button ID="btn_showmrn" runat="server" Font-Size="Smaller" Text="Show SRO" OnClick="btn_showmrn_Click" />
                    </td>
                    <td  class="NormalTD">&nbsp;</td>
                    <td  class="NormalTD">&nbsp;</td>
                    <td  class="NormalTD">&nbsp;</td>
                    <td  class="NormalTD">&nbsp;</td>
                    <td class="NormalTD" >&nbsp;</td>
                    <td  class="NormalTD">&nbsp;</td>
                    <td  class="NormalTD">&nbsp;</td>
                    <td  class="NormalTD">&nbsp;</td>
                    <td>&nbsp;</td>
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
        <td><asp:SqlDataSource ID="ROdatasource" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT [RONum], [RO_Pk] FROM [RequestOrderMaster]"></asp:SqlDataSource>
            <asp:SqlDataSource ID="SROData" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT [SRO_Pk], [RONum] FROM [RequestOrderStockMaster]">
            </asp:SqlDataSource>
        </td>
    </tr>
</table>
</asp:Content>


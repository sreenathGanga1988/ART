<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="SalesandLoanReport.aspx.cs" Inherits="ArtWebApp.Reports.Inventoryreport.SalesandLoanReport" %>
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
                    <td class="auto-style7">Internal sales #
                    </td>
                    <td  class="NormalTD">
                        </td>
                    <td class="NormalTD">
                        <ucc:DropDownListChosen ID="drp_sdo" runat="server"  DisableSearchThreshold="10" Width="200px" DataSourceID="SqlDataSource1" DataTextField="SalesDONum" DataValueField="SalesDO_PK">
                        </ucc:DropDownListChosen></td>
                    <td class ="NormalTD">
                        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Show Sales DO" />
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
                    <td  class="auto-style7">Inventory Loan #</td>
                    <td class="NormalTD">
                        &nbsp;</td>
                    <td  class="NormalTD">
                        <ucc:DropDownListChosen ID="drp_loan" runat="server" DataSourceID="SqlDataSource2" DataTextField="LoanNum" DataValueField="Loan_PK" DisableSearchThreshold="10" Width="200px">
                        </ucc:DropDownListChosen>
                    </td>
                    <td  class="NormalTD">
                        <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Show Loan Report" />
                    </td>
                    <td  class="NormalTD">&nbsp;</td>
                    <td  class="NormalTD">&nbsp;</td>
                    <td  class="NormalTD">&nbsp;</td>
                    <td class="NormalTD" >&nbsp;</td>
                    <td  class="NormalTD">&nbsp;</td>
                    <td  class="NormalTD">&nbsp;</td>
                    <td  class="NormalTD">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
         
               
                <tr>
                    <td class="auto-style7">&nbsp;</td>
                    <td class="NormalTD">
                        &nbsp;</td>
                    <td class="NormalTD">&nbsp;</td>
                    <td class="NormalTD">&nbsp;</td>
                    <td class="NormalTD">&nbsp;</td>
                    <td class="NormalTD">&nbsp;</td>
                    <td class="NormalTD">&nbsp;</td>
                    <td class="NormalTD">&nbsp;</td>
                    <td class="NormalTD">&nbsp;</td>
                    <td class="NormalTD">&nbsp;</td>
                    <td class="NormalTD">&nbsp;</td>
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
        <td>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT [SalesDO_PK], [SalesDONum] FROM [InventorySalesMaster]"></asp:SqlDataSource>
            <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT DISTINCT [Loan_PK], [LoanNum] FROM [InventoryLoanMaster] ORDER BY [Loan_PK]"></asp:SqlDataSource>
            <br />
        </td>
    </tr>
</table>
</asp:Content>


<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="JobContractReport.aspx.cs" Inherits="ArtWebApp.Reports.MerchandiserReport.JobContractReport" %>
<%--<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>--%>

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
                    <td class="RedHeadding" colspan="12">Job contract Reports</td>
                </tr>
                <tr>
                    <td class="NormalTD">Atc&nbsp; #
                    </td>
                    <td  class="NormalTD">
                        <ucc:DropDownListChosen ID="drp_spo" runat="server" DataSourceID="atcdatasource" DataTextField="AtcNum" DataValueField="AtcId" DisableSearchThreshold="10" Width="200px">
                        </ucc:DropDownListChosen></td>
                    <td class="NormalTD">
                        <asp:Button ID="btn_cnfrmpo" runat="server" Text="S" Font-Size="Smaller" OnClick="Button1_Click" />
                    </td>
                    <td class ="NormalTD">
                        &nbsp;</td>
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
                    <td  class="NormalTD">JobContract&nbsp; #</td>
                    <td class="NormalTD">
                        <ucc:DropDownListChosen ID="drp_jc" runat="server" DataSourceID="jcdata" DataTextField="JOBContractNUM" DataValueField="JobContract_pk" DisableSearchThreshold="10" Width="200px">
                        </ucc:DropDownListChosen>
                    </td>
                    <td  class="NormalTD">
                        <asp:Button ID="btn_showjc" runat="server" Font-Size="Smaller" OnClick="btn_showjc_Click" Text="Show JC" />
                    </td>
                    <td  class="NormalTD"><asp:Button ID="btn_jcnew" runat="server" Font-Size="Smaller" Text="Show JC New" OnClick="btn_jcnew_Click" /></td>
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
        <td><asp:SqlDataSource ID="atcdatasource" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT [AtcId], [AtcNum] FROM [AtcMaster]"></asp:SqlDataSource>
            <asp:HiddenField ID="hdnatc_Pk" runat="server" />
            <asp:SqlDataSource ID="jcdata" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT [JobContract_pk], [JOBContractNUM] FROM [JobContractMaster] WHERE ([AtcID] = @AtcID)">
                <SelectParameters>
                    <asp:ControlParameter ControlID="hdnatc_Pk" Name="AtcID" PropertyName="Value" Type="Decimal" />
                </SelectParameters>
            </asp:SqlDataSource>
        </td>
    </tr>
</table>
</asp:Content>


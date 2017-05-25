<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="StockMrnReport.aspx.cs" Inherits="ArtWebApp.Reports.Inventoryreport.StockMrnReport" %>
<%--<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>--%>
<%@ Register assembly="Infragistics35.Web.v12.1, Version=12.1.20121.2236, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.ListControls" tagprefix="ig" %>
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
                    <td class="NormalTD">Spo #
                    </td>
                    <td  class="NormalTD">
                        <ucc:DropDownListChosen ID="drp_spo" runat="server" DataSourceID="Spodatasource" DataTextField="SPONum" DataValueField="SPO_Pk" DisableSearchThreshold="10" Width="200px">
                        </ucc:DropDownListChosen></td>
                    <td class="NormalTD">
                        <asp:Button ID="btn_cnfrmpo" runat="server" Text="S" Font-Size="Smaller" OnClick="Button1_Click" />
                    </td>
                    <td class ="NormalTD">
                        <asp:Button ID="btn_showpo" runat="server" OnClick="btn_showpo_Click" Text="Show  PO" />
                    </td>
                    <td  class="NormalTD">
                        <asp:Button ID="btn_salesdo" runat="server" OnClick="btn_salesdo_Click" Text="Sales DO" />
                    </td>
                    <td class="NormalTD" >&nbsp;</td>
                    <td  class="NormalTD">&nbsp;</td>
                    <td class="NormalTD" >&nbsp;</td>
                    <td class="NormalTD">&nbsp;</td>
                    <td class="NormalTD" >&nbsp;</td>
                    <td class="NormalTD">&nbsp;</td>
                    <td class="NormalTD">&nbsp;</td>
                </tr>
                <tr>
                    <td  class="NormalTD">Smrn&nbsp; #</td>
                    <td class="NormalTD">
                        <ucc:DropDownListChosen ID="drp_smrn" runat="server" DataSourceID="smrdata" DataTextField="SMrnNum" DataValueField="SMrn_PK" DisableSearchThreshold="10" Width="200px">
                        </ucc:DropDownListChosen>
                    </td>
                    <td  class="NormalTD">
                        <asp:Button ID="btn_showstockro" runat="server" Font-Size="Smaller" OnClick="btn_showstockro_Click" Text="Show MRN" />
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
         
               
                <tr>
                    <td class="NormalTD">sdo</td>
                    <td class="NormalTD">
                        <ucc:DropDownListChosen ID="drp_sdo" runat="server" DataSourceID="sdodatasource" DataTextField="SDONum" DataValueField="SDO_PK" DisableSearchThreshold="10" Width="200px">
                        </ucc:DropDownListChosen></td>
                    <td class="NormalTD"><asp:Button ID="btn_sdo" runat="server" Font-Size="Smaller"  Text="Show SDO" OnClick="btn_sdo_Click" /></td>
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
         
               
                <tr>
                    <td class="NormalTD">Sales DO</td>
                    <td class="NormalTD">&nbsp;</td>
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
        <td><asp:SqlDataSource ID="Spodatasource" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT [SPO_Pk], [SPONum] FROM [StockPOMaster]"></asp:SqlDataSource>
            <asp:HiddenField ID="hdnSpo_Pk" runat="server" />
            <asp:SqlDataSource ID="smrdata" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT [SMrn_PK], [SMrnNum] FROM [StockMrnMaster] WHERE ([SPo_PK] = @SPo_PK)">
                <SelectParameters>
                    <asp:ControlParameter ControlID="hdnSpo_Pk" DefaultValue="0" Name="SPo_PK" PropertyName="Value" Type="Decimal" />
                </SelectParameters>
            </asp:SqlDataSource>
            <asp:SqlDataSource ID="sdodatasource" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT [SDONum], [SDO_PK] FROM [DeliveryOrderStockMaster]">
            </asp:SqlDataSource>
        </td>
    </tr>
</table>
</asp:Content>


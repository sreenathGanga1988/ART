﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="SamplingFabReport.aspx.cs" Inherits="ArtWebApp.Reports.SamplingReport.SamplingFabReport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../css/style.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="FullTable">
    <tr>
       <td class="NormalTD">
             <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>

            <table class="DataEntryTable">
                <tr>
                    <td class="RedHeadding" colspan="12">Inventory MisplacedReports</td>
                </tr>
                <tr>
                    <td class="NormalTD">Fabric Code
                    </td>
                    <td  class="NormalTD">
                        <ucc:DropDownListChosen ID="dll_reg" runat="server" DataSourceID="SqlDataSource1" DataTextField="Code" DataValueField="SamplingFab_PK" DisableSearchThreshold="10" Width="200px">
                        </ucc:DropDownListChosen>
                        
                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT [SamplingFab_PK], [Code] FROM [SampleFabricEntryMaster]"></asp:SqlDataSource>
                        
                    </td>
                    <td class="NormalTD">
                        </td>
                    <td class ="NormalTD">
                        <asp:Button ID="btn_showRO" runat="server" OnClick="btn_showpo_Click" Text="Show  Report" ToolTip="Show the " />
                    </td>
                    <td  class="NormalTD"></td>
                    <td class="NormalTD" ></td>
                    <td  class="NormalTD"></td>
                    <td class="NormalTD" ></td>
                    <td class="NormalTD"></td>
                    <td class="NormalTD" ></td>
                    <td class="NormalTD"></td>
                    <td class="NormalTD"></td>
                </tr>
         
               
                <tr>
                    <td class="NormalTD">&nbsp;</td>
                    <td class="NormalTD">&nbsp;</td>
                    <td class="NormalTD">&nbsp;</td>
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
                        <LocalReport ReportPath="Reports\RDLC\SamplingFabricEntry.rdlc">
                        </LocalReport>
                    </rsweb:ReportViewer>
                </ContentTemplate>

            </asp:UpdatePanel>

        </td>
    </tr>
    <tr>
       <td class="NormalTD"><asp:SqlDataSource ID="MisplacedReq" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT [WrongPO_Pk], [Reqnum] FROM [WrongPOMaster]">
                        </asp:SqlDataSource>
            <asp:SqlDataSource ID="Extrabom" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT [ExtraBOM_PK], [Reqnum] FROM [ExtraBOMRequestMaster] ORDER BY [ExtraBOM_PK] DESC"></asp:SqlDataSource>
        </td>
    </tr>
</table>
</asp:Content>

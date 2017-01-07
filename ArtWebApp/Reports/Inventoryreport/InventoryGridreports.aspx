<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="InventoryGridreports.aspx.cs" Inherits="ArtWebApp.Reports.Inventoryreport.InventoryGridreports" %>
<%--<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>--%>
<%@ Register assembly="Infragistics35.Web.v12.1, Version=12.1.20121.2236, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.ListControls" tagprefix="ig" %>
<%@ Register assembly="Infragistics35.Web.v12.1, Version=12.1.20121.2236, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.GridControls" tagprefix="ig" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
    

        .NormalTD {
            height: 23px;
        }
    

    </style>
    <link href="../../css/style.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="FullTable">
    <tr>
        <td>
           

            <table class="DataEntryTable">
                <tr>
                    <td class="RedHeadding" colspan="12">Grid&nbsp; Reports</td>
                </tr>
                <tr>
                    <td colspan="12" >&nbsp;Total Report&nbsp;</td>
                </tr>
         
                <tr>
                    <td >&nbsp;</td>
                    <td >
                        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="MRN Pending POs" Font-Size="Smaller" />
                    </td>
                    <td>
                        <asp:Button ID="Button3" runat="server" OnClick="Button3_Click" Text="Goods In Transit" Font-Size="Smaller" />
                    </td>
                    <td>
                       
                        

                                <asp:Button ID="Button2" runat="server" Text="Export" OnClick="Button2_Click" Font-Size="Smaller" />

                           
                    </td>
                    <td >
                        <ig:WebExcelExporter ID="WebExcelExporter1" runat="server" DownloadName="Export">
                        </ig:WebExcelExporter>
                    </td>
                    <td ><asp:Button ID="btn_popending" runat="server"  Text="Approval Pending POs" Font-Size="Smaller" OnClick="btn_popending_Click" /></td>
                    <td >&nbsp;</td>
                    <td >&nbsp;</td>
                    <td>&nbsp;</td>
                    <td >&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
         
                <tr>
                    <td class="NormalTD" ></td>
                    <td colspan="11" class="NormalTD" >
                        Atc/ Location reports</td>
                </tr>
         
                <tr>
                    <td >&nbsp;</td>
                    <td >
                        Atc&nbsp;
                    </td>
                    <td>
                        <ucc:DropDownListChosen ID="ddl_atc" Width="100%"  runat="server"   >
                                        </ucc:DropDownListChosen></td>
                    <td>
                       
                        

                                &nbsp;</td>
                    <td >
                        &nbsp;</td>
                    <td >&nbsp;</td>
                    <td >&nbsp;</td>
                    <td >&nbsp;</td>
                    <td>&nbsp;</td>
                    <td >&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
         
                <tr>
                    <td >&nbsp;</td>
                    <td >
                        Location</td>
                    <td>
                        <ucc:DropDownListChosen ID="ddl_location" Width="100%"  runat="server"   >
                                        </ucc:DropDownListChosen></td>
                    <td>
                       
                        

                                <asp:Button ID="btn_fabpo" runat="server" Font-Size="Smaller" Text="Show Fabric PO of Atc " OnClick="btn_fabpo_Click" />
                    </td>
                    <td >
                        <asp:Button ID="btn_trimofatc" runat="server" Font-Size="Smaller" Text="Show Trim PO of Atc " OnClick="btn_trimofatc_Click" /></td>
                    <td >&nbsp;</td>
                    <td >&nbsp;</td>
                    <td >&nbsp;</td>
                    <td>&nbsp;</td>
                    <td >&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
         
            </table>


                   

        </td>
    </tr>
    <tr>
        <td class="smallgridtable">



            <asp:UpdatePanel ID="upl_rpt" runat="server">
                <ContentTemplate>
                    <ig:WebDataGrid ID="WebDataGrid1" runat="server" Height="350px" Width="100%" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" DefaultColumnWidth="100px">
                        <Behaviors>
                            <ig:Filtering>
                            </ig:Filtering>
                            <ig:Sorting>
                            </ig:Sorting>
                            <ig:ColumnResizing>
                            </ig:ColumnResizing>
                            <ig:ColumnMoving>
                            </ig:ColumnMoving>
                            <ig:SummaryRow>
                            </ig:SummaryRow>
                        </Behaviors>
                    </ig:WebDataGrid>
                </ContentTemplate>

            </asp:UpdatePanel>

        </td>
    </tr>
   
</table>
</asp:Content>



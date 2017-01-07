<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="PurchaseReports.aspx.cs" Inherits="ArtWebApp.Reports.MerchandiserReport.PurchaseReports" %>
<%--<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>--%>
<%@ Register assembly="Infragistics35.Web.v12.1, Version=12.1.20121.2236, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.ListControls" tagprefix="ig" %>
<%@ Register assembly="DropDownChosen" namespace="CustomDropDown" tagprefix="ucc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
    

  
    

    .auto-style8 {
        height: 27px;
    }
    

  
    

    </style>
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
                    <td class="RedHeadding" colspan="12">Inventory Reports</td>
                </tr>
                <tr>
                    <td class="NormalTD" colspan="12" >&nbsp;<strong>Purchase Reports&nbsp;</strong></td>
                </tr>
                <tr>
                    <td>
                        Mrn # from</td>
                    <td>
                         <ucc:DropDownListChosen ID="ddl_Frommrn" Width="200px"  runat="server"   >
                                        </ucc:DropDownListChosen></td>
                    <td class="NormalTD">
                        &nbsp;</td>
                    <td>mrn # to </td>
                    <td> <ucc:DropDownListChosen ID="ddl_tomrn" Width="200px"  runat="server"   >
                                        </ucc:DropDownListChosen></td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>
                        <asp:Button ID="Button1" runat="server" Text="Show MRN Purchase" OnClick="Button1_Click" Font-Size="Smaller" />
                    </td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="NormalTD" colspan="12">&nbsp;<strong>Atc - location&nbsp; Reports</strong></td>
                </tr>
                <tr>
                    <td>Atc</td>
                    <td>
                        <ig:WebDropDown ID="drp_Atc" runat="server" DropDownAnimationType="EaseOut" DropDownContainerHeight="300px" DropDownContainerWidth="200px" EnableClosingDropDownOnSelect="False" EnableDropDownAsChild="false" EnableMultipleSelection="True" Height="21px" PageSize="12" TextField="name" ValueField="pk" Width="156px">
                            <DropDownItemBinding TextField="name" ValueField="pk" />
                        </ig:WebDropDown>
                    </td>
                    <td class="auto-style8" colspan="2">
                        &nbsp;</td>
                    <td>
                        <asp:Button ID="Button2" runat="server" Font-Size="Smaller" OnClick="Button2_Click" Text="Show Purchase Report of Atc" />
                    </td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="NormalTD" >&nbsp;</td>
                    <td class="NormalTD">
                        &nbsp;</td>
                    <td class="NormalTD">
                        &nbsp;</td>
                    <td class="NormalTD" >
                        &nbsp;</td>
                    <td class="NormalTD" >&nbsp;</td>
                    <td class="NormalTD" >&nbsp;</td>
                    <td class="NormalTD" ></td>
                    <td class="NormalTD" ></td>
                    <td class="NormalTD" ></td>
                    <td class="NormalTD" ></td>
                    <td class="NormalTD" ></td>
                    <td class="NormalTD"></td>
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
        <td>&nbsp;</td>
    </tr>
</table>
</asp:Content>



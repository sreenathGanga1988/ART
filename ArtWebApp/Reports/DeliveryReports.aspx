<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="DeliveryReports.aspx.cs" Inherits="ArtWebApp.Reports.DeliveryReports" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
   

  
    </style>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
       <table class="FullTable">
    <tr>
        <td>


            <table class="DataEntryTable">
                <tr>
                    <td class="RedHeadding" colspan="11">Delivery Reports</td>
                </tr>
                <tr>
                    <td class="NormalTD">Atc #</td>
                    <td class="NormalTD">

                              

                         <ucc:DropDownListChosen ID="drp_Atc" runat="server" DataTextField="name" DataValueField="pk" Width="200px">
                                    </ucc:DropDownListChosen>
                    </td>
                    <td>
                        <asp:Button ID="Btn_show" runat="server" OnClick="Btn_show_Click" Text="Show All DO" Font-Size="Smaller"  />
                    </td>
                    <td class="NormalTD">

                        &nbsp;</td>
                    <td class="NormalTD">


                        &nbsp;</td>
                    <td class="NormalTD">&nbsp;</td>
                    <td class="NormalTD">

                        
                        &nbsp;</td>
                    <td>


                        &nbsp;</td>
                    <td>&nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                          &nbsp;</td>
                </tr>
                <tr>
                    <td class="NormalTD">DO</td>
                    <td class="NormalTD">


                        <ucc:DropDownListChosen ID="drp_do" runat="server" DataTextField="name" DataValueField="pk" Width="200px">
                                    </ucc:DropDownListChosen>

                    </td>
                    <td class="NormalTD">


            <asp:Button ID="Btn_showdDO" runat="server" Text="Show Selected DO" OnClick="Btn_showdDO_Click" CssClass="test-class" Font-Size="Smaller"  />
                    </td>
                    <td class="NormalTD">

                        <asp:Button ID="Btn_showDOR" runat="server"  OnClick="Btn_showDOR_Click" Text="Show DOR" ToolTip="Show DOR of Selected  DO in DOR Drop Down" Font-Size="Smaller" />
                    </td>
                    <td class="NormalTD">

                        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="DO Inspection Form" ToolTip="Show Physical Inspection Report" Font-Size="Smaller" />
                    </td>
                    <td class="NormalTD"></td>
                    <td class="NormalTD">

                        
                    </td>
                    <td class="NormalTD">


                    </td>
                    <td class="NormalTD"></td>
                    <td class="NormalTD">
                    </td>
                    <td class="NormalTD">
                    </td>
                </tr>
                <tr>
                    <td class="NormalTD">DOR#</td>
                    <td class="NormalTD">

                               
                         <ucc:DropDownListChosen ID="drp_rcpt" runat="server" DataTextField="name" DataValueField="pk" Width="200px">
                                    </ucc:DropDownListChosen>
                    </td>
                    <td>


            <asp:Button ID="Btn_showDORrpt" runat="server" Text="Show Selected DOR" OnClick="Btn_showDORrpt_Click" Font-Size="Smaller" ToolTip="Displays the selected  DOR Report" />
                    </td>
                    <td class="NormalTD">

                        
                    </td>
                    <td class="NormalTD">


                        &nbsp;</td>
                    <td class="NormalTD">&nbsp;</td>
                    <td class="NormalTD">

                        
                        &nbsp;</td>
                    <td>


                        &nbsp;</td>
                    <td>&nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                          &nbsp;</td>
                </tr>
                <tr>
                    <td class="NormalTD">&nbsp;</td>
                    <td class="NormalTD">

                        &nbsp;</td>
                    <td>&nbsp;</td>
                    <td class="NormalTD">

                        &nbsp;</td>
                    <td class="NormalTD">


                        &nbsp;</td>
                    <td class="NormalTD">&nbsp;</td>
                    <td class="NormalTD">

                        
                        &nbsp;</td>
                    <td>


                        &nbsp;</td>
                    <td>&nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                          &nbsp;</td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td>
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

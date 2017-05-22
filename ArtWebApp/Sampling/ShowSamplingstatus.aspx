<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="ShowSamplingstatus.aspx.cs" Inherits="ArtWebApp.Sampling.ShowSamplingstatus" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
   

  
    .auto-style1 {
        font-size: xx-small;
    }
   

  
    </style>
    <link href="../css/style.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
       <table class="FullTable">
    <tr>
        <td>


            <table class="DataEntryTable">
                <tr>
                    <td class="RedHeadding" colspan="11">Sampoling Status report</td>
                </tr>
                <tr>
                    <td class="NormalTD">Master</td>
                    <td class="NormalTD">

                              

                         <ucc:DropDownListChosen ID="drp_master" runat="server" DataTextField="PaternMasterName" DataValueField="PatternMasterID" Width="200px" DataSourceID="Masters" DisableSearchThreshold="10">
                                    </ucc:DropDownListChosen>
                         <asp:SqlDataSource ID="Masters" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT [PaternMasterName], [PatternMasterID] FROM [PatternMaster]"></asp:SqlDataSource>
                    </td>
                    <td>
                        &nbsp;</td>
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
                    <td class="NormalTD">From date</td>
                    <td class="NormalTD">


                        <ig:WebDatePicker ID="dtp_fromdate" runat="server">
                        </ig:WebDatePicker>

                    </td>
                    <td class="NormalTD">


                        &nbsp;</td>
                    <td class="NormalTD">

                        &nbsp;</td>
                    <td class="NormalTD">

                        &nbsp;</td>
                    <td class="NormalTD">   &nbsp;</td>
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
                    <td class="NormalTD">To date</td>
                    <td class="NormalTD">

                               
                         <ig:WebDatePicker ID="dtp_todate" runat="server">
                         </ig:WebDatePicker>
                    </td>
                    <td>


                        <asp:Button ID="Button1" runat="server" CssClass="auto-style1" OnClick="Button1_Click" Text="Show Details of Master" />
                    </td>
                    <td class="NormalTD">

                        
                        <asp:Button ID="Button2" runat="server" CssClass="auto-style1" OnClick="Button2_Click" Text="Show Data for all Master in period" />

                        
                    </td>
                    <td class="NormalTD">


                        <asp:Button ID="Button3" runat="server" CssClass="auto-style1" OnClick="Button3_Click" Text="Show All Data" />
                    </td>
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

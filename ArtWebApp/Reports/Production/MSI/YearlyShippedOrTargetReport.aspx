﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="YearlyShippedOrTargetReport.aspx.cs" Inherits="ArtWebApp.Reports.Production.MSI.YearlyShippedOrTargetReport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
      <link href="../../../css/style.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate> 
<div class="FullTable">
        <table class="FullTable">
        <tr  class="RedHeadding">
            <td style="color: #FFFFFF; text-align: center; background-color: #990000">MonthWi Target/Shipped Report</td>
        </tr>
        <tr>
            <td >



                 <table >
                        <tr>
                            <td class="auto-style1" colspan="4">

                                   &nbsp;</td>
                            </tr>

                        

                        <tr>
                            <td class="NormalTD">

                                Year</td>
                            <td class="NormalTD">
                                 <ucc:DropDownListChosen ID="cmb_year" runat="server" DisableSearchThreshold="10" Width="200px">
                                     <asp:ListItem>2017</asp:ListItem>
                                     <asp:ListItem>2018</asp:ListItem>
                                     <asp:ListItem>2019</asp:ListItem>
                                     <asp:ListItem>2020</asp:ListItem>
                                 </ucc:DropDownListChosen>
                    
                
                            </td>
                            <td class="SearchButtonTD">
                                 
                                
                     
                                
                            </td>
                            <td>
                               
                                &nbsp;</td>
                            </tr>

                        

                        

                        

                        

                        

                        

                        

                        

                        

                        

                        

                        

                        

                        

                        <tr>
                            <td class="NormalTD">Month</td>
                            <td class="NormalTD">
                                <ucc:DropDownListChosen ID="cmb_Month" runat="server" DisableSearchThreshold="10" Width="200px">
                                    <asp:ListItem Value="1">January</asp:ListItem>
                                    <asp:ListItem Value="2">February</asp:ListItem>
                                    <asp:ListItem Value="3">March</asp:ListItem>
                                    <asp:ListItem Value="4">April</asp:ListItem>
                                    <asp:ListItem Value="5">May</asp:ListItem>
                                    <asp:ListItem Value="6">June</asp:ListItem>
                                        <asp:ListItem Value="7">July</asp:ListItem>
                                    <asp:ListItem Value="8">August</asp:ListItem>
                                    <asp:ListItem Value="9">September</asp:ListItem>
                                    <asp:ListItem Value="10">October</asp:ListItem>
                                      <asp:ListItem Value="11">Novemeber</asp:ListItem>
                                     <asp:ListItem Value="12">December</asp:ListItem>
                                </ucc:DropDownListChosen>
                            </td>
                            <td class="SearchButtonTD">
                                <asp:Button ID="S" runat="server" OnClick="Button3_Click1" Text="S" />
                              
                            </td>
                            <td>&nbsp;</td>
                        </tr>

                        

                        

                        

                        

                        

                        

                        

                        

                        

                        

                        

                        

                        

                        

                </table>

                
               
               
            </td>
        </tr>
        
       
        <tr>
                <td class="ReportViewSection">
            <asp:UpdatePanel ID="upl_rpt" runat="server">
                <ContentTemplate>
                    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="100%">
                    
                        <LocalReport ReportPath="Reports\RDLC\MonthlyReport.rdlc">
                        </LocalReport>
                    
                    </rsweb:ReportViewer>
                </ContentTemplate>

            </asp:UpdatePanel>

        </td>
        </tr>
    </table>
    </div>

<div>
        <table class="DataEntryTable">
                    <tr>
                      
                        <td class="auto-style8"><asp:UpdatePanel ID="upd_main" runat="server">
                                    <ContentTemplate>
                     

                                    </ContentTemplate>
                                </asp:UpdatePanel></td>
                        
                    </tr>
                   
                    
                    <tr>
                        <td class="NormalTD">
                           
                   
                        </td>
                    
                    </tr>
                   
                    
                </table>
                    
        <br />
                    
    </div>

                                    </ContentTemplate>
                                </asp:UpdatePanel>
</asp:Content>

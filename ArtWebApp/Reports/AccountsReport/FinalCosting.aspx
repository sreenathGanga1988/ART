<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="FinalCosting.aspx.cs" Inherits="ArtWebApp.Reports.AccountsReport.FinalCosting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    

  
    <link href="../../css/style.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    

 

        <table class="DataEntryTable">
        <tr>
            <td >&nbsp;</td>
            <td class="auto-style7" >&nbsp;</td>
            <td class="auto-style7" >&nbsp;</td>
            <td>&nbsp;</td>
            <td >&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="NormalTD"  >Atc # : </td>
            <td class="auto-style8" >
                <asp:UpdatePanel ID="Upd_atc" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <ucc:DropDownListChosen ID="drp_Atc" runat="server" Width="200px">
                        </ucc:DropDownListChosen>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td class="NormalTD" >
                <asp:UpdatePanel ID="UpdatePanel5" UpdateMode="Conditional" runat="server">
                    <ContentTemplate>
                        <asp:Button ID="Button4" runat="server" Text="S" OnClick="Button4_Click" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td class="NormalTD"><asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
                </asp:UpdatePanel></td>
            <td class="NormalTD" >
                </td>
            <td class="NormalTD">
                &nbsp;</td>
            <td class="NormalTD"></td>
            <td class="NormalTD"></td>
        </tr>
      
        <tr>
            <td  class="ReportViewSection" colspan="8" >
                <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="100%">
                </rsweb:ReportViewer>
            </td>
        </tr>
      
    </table>
    
  



    
</asp:Content>


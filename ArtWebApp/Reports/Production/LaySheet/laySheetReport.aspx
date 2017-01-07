<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="laySheetReport.aspx.cs" Inherits="ArtWebApp.Reports.Production.LaySheet.laySheetReport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
    <link href="../../../css/style.css" rel="stylesheet" />
 
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    

 

        <table class="FullTable">
        <tr>
            <td  class="RedHeadding" colspan="8" >LASYSHEET&nbsp; rEPORTS</td>
        </tr>
        <tr>
            <td class="NormalTD"  >LAY SHEET # : </td>
            <td class="NormalTD" >
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
                    <ContentTemplate>
                        <asp:Button ID="btn_showApproved" runat="server" Text="Show All Cutorder" Width="190px" OnClick="Button1_Click" Font-Size="Smaller" />
                    </ContentTemplate>
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


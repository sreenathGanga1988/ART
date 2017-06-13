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
            <td class="NormalTD"  >ATC#:</td>
            <td class="NormalTD" >
                <asp:UpdatePanel ID="Upd_atc0" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <ucc:DropDownListChosen ID="drp_Atc" runat="server" Width="200px" DataSourceID="AtcDataSource" DataTextField="AtcNum" DataValueField="AtcId" DisableSearchThreshold="10">
                        </ucc:DropDownListChosen>
                        <asp:SqlDataSource ID="AtcDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT [AtcId], [AtcNum] FROM [AtcMaster]"></asp:SqlDataSource>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td class="NormalTD" >
                <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:Button ID="Button5" runat="server" OnClick="Button4_Click" Text="S" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td class="NormalTD">&nbsp;</td>
            <td class="NormalTD" >
                <asp:Button ID="Button8" runat="server" Font-Size="Smaller" OnClick="Button8_Click" Text="Show laysheet pending of All location" ToolTip="Show laysheet pending of All location All Atc" />
            </td>
            <td class="NormalTD">
                &nbsp;</td>
            <td class="NormalTD">&nbsp;</td>
            <td class="NormalTD">&nbsp;</td>
        </tr>
      
        <tr>
            <td class="NormalTD"  >Manul Laysheet #</td>
            <td class="NormalTD" >
                <asp:UpdatePanel ID="Upd_laysheetroll" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <ucc:DropDownListChosen ID="drp_laysheetroll" runat="server" Width="200px">
                        </ucc:DropDownListChosen>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td class="NormalTD" >
                &nbsp;</td>
            <td class="NormalTD">
                <asp:Button ID="Button7" runat="server" Text="Print laysheet form" Font-Size="Smaller" OnClick="Button7_Click"  />
            </td>
            <td class="NormalTD" >
                &nbsp;</td>
            <td class="NormalTD">
                &nbsp;</td>
            <td class="NormalTD">&nbsp;</td>
            <td class="NormalTD">&nbsp;</td>
        </tr>
      
        <tr>
            <td class="NormalTD"  >LAY SHEET # : </td>
            <td class="NormalTD" >
                 <asp:UpdatePanel ID="Upd_loc" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <ucc:DropDownListChosen ID="drp_laysheet" runat="server" Width="200px">
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
                <asp:Button ID="Button6" runat="server" OnClick="Button6_Click" Text="Show Printable Laysheet" />
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


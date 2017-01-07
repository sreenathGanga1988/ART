<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="CutOrder.aspx.cs" Inherits="ArtWebApp.Reports.Production.CutOrderReport.CutOrder" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
    <link href="../../../css/style.css" rel="stylesheet" />
 
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    

 

        <table class="DataEntryTable">
        <tr>
            <td >&nbsp;</td>
        <td class="NormalTD">&nbsp;</td>
        <td class="NormalTD">&nbsp;</td>
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
            <td >OurStyle # : </td>
        <td class="NormalTD">
                <asp:UpdatePanel ID="Upd_ourstyle" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <ucc:DropDownListChosen ID="drp_ourstyle" runat="server" Width="200px">
                        </ucc:DropDownListChosen>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        <td class="NormalTD">
                
            </td>
            <td>
                &nbsp;</td>
            <td >
                
            </td>
            <td>
                &nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td >FABRIC</td>
            <td class="NormalTD" colspan="2" >
               <asp:UpdatePanel ID="UPD_COLOR" UpdateMode="Conditional" runat="server">
                   <ContentTemplate>
                         <ucc:DropDownListChosen ID="ddl_color" runat="server" Width="400px">
                </ucc:DropDownListChosen>
                   </ContentTemplate>
                 
                </asp:UpdatePanel></td>
            <td>
                &nbsp;</td>
            <td >
                
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td >Cut Order # </td>
        <td class="NormalTD">
                <asp:UpdatePanel ID="Upd_costing" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <ucc:DropDownListChosen ID="drp_costingpk" runat="server" Width="200px" DataTextField="name" DataValueField="pk">
                        </ucc:DropDownListChosen>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        <td class="NormalTD">
                <asp:Button ID="Button5" runat="server" Text="Show Cut Order Report" OnClick="Button5_Click" Font-Size="Smaller" />
            </td>
            <td>&nbsp;</td>
            <td >&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
      
        <tr>
            <td  class="ReportViewSection" colspan="8" >
                <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="100%">
                </rsweb:ReportViewer>
            </td>
        </tr>
      
    </table>
    
  



    
</asp:Content>



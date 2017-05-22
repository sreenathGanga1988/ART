<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="CutOrderReport.aspx.cs" Inherits="ArtWebApp.Reports.AtcCrystalReporter" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
 
    <link href="../css/style.css" rel="stylesheet" />
    
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
                <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:Button ID="btn_showApprovedcutplan" runat="server" Font-Size="Smaller" OnClick="btn_showApprovedcutplan_Click" Text="Show Approved Cutplan" ToolTip="Show All Cut Plan  of Selected Atc Approved By QAD " Width="190px" />
                    </ContentTemplate>
                </asp:UpdatePanel>
                </td>
            <td class="NormalTD">
                <asp:Button ID="btn_showApprovedcutplan0" runat="server" Font-Size="Smaller" Text="Show Cutplan History" ToolTip="Show All Cut Plan  and Cutorder Details of Atc" Width="190px" OnClick="btn_showApprovedcutplan0_Click" />
            </td>
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
                <asp:Button ID="Button7" runat="server" Font-Size="Smaller" OnClick="Button7_Click" Text="CutOrder summary of a Fabric of a style" ToolTip="Show the CutOrder Summary of Selected fabric against the selected oursstyle" />
            </td>
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
            <td>
                <asp:Button ID="Button8" runat="server" OnClick="Button8_Click" Text="Button" />
            </td>
            <td >&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
      
        <tr>
            <td >cut plan #</td>
        <td class="NormalTD">
                <asp:UpdatePanel ID="upd_cutplan" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <ucc:DropDownListChosen ID="drp_cutplan" runat="server" DataTextField="name" DataValueField="pk" Width="200px">
                        </ucc:DropDownListChosen>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        <td class="NormalTD">
                <asp:Button ID="btn_showCutplam" runat="server" Font-Size="Smaller" OnClick="btn_showCutplam_Click" Text="Show Cut Plan Report" />
            </td>
            <td>
                &nbsp;</td>
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


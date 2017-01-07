<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="ReporterPage.aspx.cs" Inherits="ArtWebApp.Reports.ReporterPage" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../css/style.css" rel="stylesheet" />
    <style type="text/css">
    
        .auto-style8 {
            height: 27px;
        }
    
    </style>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <table class="DataEntryTable">
        <tr>
            <td class="RedHeadding" colspan="8">Costing reports</td>
        </tr>
        <tr>
            <td class="NormalTD">Atc # : </td>
            <td class="NormalTD">
                <asp:UpdatePanel ID="Upd_atc" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        
                        <ucc:DropDownListChosen ID="drp_Atc" runat="server" DataTextField="name" DataValueField="pk" Width="200px">
                                    </ucc:DropDownListChosen>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td class="NormalTD">
                <asp:UpdatePanel ID="UpdatePanel5" UpdateMode="Conditional" runat="server">
                    <ContentTemplate>
                        <asp:Button ID="Button4" runat="server" Text="S" OnClick="Button4_Click" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td>&nbsp;</td>
            <td class="NormalTD">
                &nbsp;</td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel3"  UpdateMode="Conditional" runat="server">
                    <ContentTemplate>
                        <asp:Button ID="Button3" runat="server" Font-Size="XX-Small" OnClick="Button3_Click" Text="Show  Selected  Atc Costing Data" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="NormalTD">OurStyle # : </td>
            <td class="NormalTD">
                <asp:UpdatePanel ID="Upd_ourstyle" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        

                            <ucc:DropDownListChosen ID="drp_ourstyle" runat="server" DataTextField="name" DataValueField="pk" Width="200px">
                                    </ucc:DropDownListChosen>
                        
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td class="NormalTD">
                <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
                    <ContentTemplate>
                        <asp:Button ID="btn_showApproved" runat="server" Text="Show Approved Costing" Width="190px" OnClick="Button1_Click" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td>
                &nbsp;</td>
            <td class="NormalTD">
                <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Conditional" runat="server">
                    <ContentTemplate>
                        <asp:Button ID="btn_showAllSubmitted" runat="server" Text="Show Submitted Costing" Width="185px" OnClick="btn_showAllSubmitted_Click" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel4" UpdateMode="Conditional" runat="server">
                    <ContentTemplate>
                        <asp:Button ID="ShowAll" runat="server" Text="Show All Costing" OnClick="ShowAll_Click" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="NormalTD">Costing #</td>
            <td class="NormalTD">
                <asp:UpdatePanel ID="Upd_costing" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                      
                        
                            <ucc:DropDownListChosen ID="drp_costingpk" runat="server" DataTextField="name" DataValueField="pk" Width="200px">
                                    </ucc:DropDownListChosen>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td class="NormalTD">
                <asp:Button ID="Button5" runat="server" Text="Show" OnClick="Button5_Click" />
            </td>
            <td>&nbsp;</td>
            <td class="NormalTD">&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="ReportViewSection" colspan="8"><asp:UpdatePanel ID="upl_rpt" runat="server">
                <ContentTemplate>
                    <rsweb:ReportViewer ID="ReportViewer1" runat="server"  Width="100%">
                    </rsweb:ReportViewer>
                </ContentTemplate>

            </asp:UpdatePanel></td>
        </tr>
        <tr>
            <td class="NormalTD">&nbsp;</td>
            <td class="NormalTD">&nbsp;</td>
            <td class="NormalTD">&nbsp;</td>
            <td>&nbsp;</td>
            <td class="NormalTD">&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
    </table>
    
</asp:Content>

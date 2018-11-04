<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="MRNReport.aspx.cs" Inherits="ArtWebApp.Reports.Inventoryreport.MRNReport" %>
<%--<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>--%>
<%@ Register assembly="Infragistics35.Web.v12.1, Version=12.1.20121.2236, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.ListControls" tagprefix="ig" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
    

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
                    <td class="RedHeadding" colspan="12">Receipt Reports</td>
                </tr>
                <tr>
                    <td >Atc #</td>
                    <td >
                        <ucc:DropDownListChosen ID="drp_Atc" runat="server" Width="180px">
                        </ucc:DropDownListChosen>
                    </td>
                    <td>
                        <asp:Button ID="btn_showPO" runat="server" OnClick="btn_showPO_Click" Text="Get All PO" Width="160px" ToolTip="Shows All the PO of the Atc in PO Dropdown" Font-Size="Smaller" />
                    </td>
                    <td><asp:Button ID="btn_showAtcMRN" runat="server" Text="Get All MRN of ATC" OnClick="btn_showAtcMRN_Click" ToolTip="Show All MRN of ATC in MRN DropDown" Font-Size="Smaller" /></td>
                    <td >&nbsp;</td>
                    <td >&nbsp;</td>
                    <td >&nbsp;</td>
                    <td >&nbsp;</td>
                    <td>&nbsp;</td>
                    <td >&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td >Supplier #</td>
                    <td>
                        &nbsp;</td>
                    <td class="auto-style20">
                        &nbsp;</td>
                    <td >&nbsp;</td>
                    <td >&nbsp;</td>
                    <td >&nbsp;</td>
                    <td >&nbsp;</td>
                    <td >&nbsp;</td>
                    <td >&nbsp;</td>
                    <td >&nbsp;</td>
                    <td >&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td >Po#</td>
                    <td >
                        <ucc:DropDownListChosen ID="drp_PO" runat="server" Width="180px">
                        </ucc:DropDownListChosen>
                    </td>
                    <td >
                        <asp:Button ID="Btn_showReceipt" runat="server" OnClick="Btn_showReceipt_Click" Text="Show PO Receipt " Width="163px" ToolTip="Show All PO Receipt in Reciept DropDown" Font-Size="Smaller" />
                    </td>
                    <td ><asp:Button ID="btn_showPhysical" runat="server" Text="Physical Inspection Sheet " Width="212px" OnClick="btn_showPhysical_Click" ToolTip="Display the Physical Inspection Report of PO" Font-Size="Smaller" /></td>
                    <td ></td>
                    <td></td>
                    <td ></td>
                    <td ></td>
                    <td ></td>
                    <td ></td>
                    <td ></td>
                    <td ></td>
                </tr>
                <tr>
                    <td >Receipt#</td>
                    <td >
                        <ucc:DropDownListChosen ID="drp_rcpt" runat="server" Width="180px">
                        </ucc:DropDownListChosen>
                    </td>
                    <td>
                        <asp:Button ID="Btn_showRcptReport" runat="server" OnClick="Btn_showRcptReport_Click" Text="Show Receipt Report" Width="163px" ToolTip="Display Reciept Report" Font-Size="Smaller" />
                    </td>
                    <td>
                        <asp:Button ID="Btn_showallmrn" runat="server" OnClick="Btn_showallmrn_Click" Text="Get All MRN" ToolTip="Shows All the MRN of Selected Receipt in MRN DropDown" Font-Size="Smaller" />
                    </td>
                    <td >&nbsp;</td>
                    <td >&nbsp;</td>
                    <td >&nbsp;</td>
                    <td >&nbsp;</td>
                    <td>&nbsp;</td>
                    <td c>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td >Mrn#</td>
                    <td >
                        <ucc:DropDownListChosen ID="drp_mrn" runat="server" Width="180px">
                        </ucc:DropDownListChosen>
                    </td>
                    <td >
                        <asp:Button ID="Btn_mrnshowreport" runat="server" OnClick="Btn_mrnshowreport_Click" Text="Show MRN Report" Width="157px" ToolTip="Display selected MRN Report" Font-Size="Smaller" />
                    </td>
                    <td >
                        &nbsp;</td>
                    <td ></td>
                    <td ></td>
                    <td ></td>
                    <td ></td>
                    <td ></td>
                    <td ></td>
                    <td ></td>
                    <td ></td>
                </tr>
                <tr>
                    <td >MCR NUM</td>
                    <td >
                        <asp:DropDownList ID="drp_mcrno" runat="server" Height="25px" Width="177px">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Button ID="btn_mcr" runat="server" Text="Show MCR " Width="155px"  OnClick="btn_mcr_click"/>
                    </td>
                    <td>&nbsp;</td>
                    <td >&nbsp;</td>
                    <td >&nbsp;</td>
                    <td >&nbsp;</td>
                    <td >&nbsp;</td>
                    <td>&nbsp;</td>
                    <td >&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
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


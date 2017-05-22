<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="ProductionReports.aspx.cs" Inherits="ArtWebApp.Reports.Production.ProductionReports" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
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
                    <td class="RedHeadding" colspan="12">&nbsp;production Reports</td>
                </tr>
                <tr>
                    <td class="NormalTD" >Atc #</td>
                    <td class="NormalTD" >
                        <ucc:DropDownListChosen ID="drp_Atc" runat="server" Width="180px">
                        </ucc:DropDownListChosen>
                    </td>
                    <td class="NormalTD">
                        <asp:Button ID="btn_atc" runat="server" OnClick="btn_atc_Click" Text="Show Production Report" Font-Size="Smaller"  Width="168px" ToolTip="Display the locationwise production report of Atc" />
                    </td>
                    <td class="NormalTD"><asp:Button ID="btn_jc" runat="server" OnClick="btn_jc_Click" Text="Show JOB Contract" Font-Size="Smaller"  Width="168px" ToolTip="Show the Jobcontract of the selected Atc in Job Contract dropdown" /></td>
                    <td class="NormalTD" >
                        <asp:Button ID="btn_jc0" runat="server" Font-Size="Smaller" OnClick="btn_jc0_Click" Text="Show ShipmentHandOver" ToolTip="Show the Shipment handover of the selected Atc in  Shipment handover dropdown" Width="168px" />
                    </td>
                    <td class="NormalTD" ></td>
                    <td class="NormalTD" ></td>
                    <td class="NormalTD" ></td>
                    <td class="NormalTD"></td>
                    <td class="NormalTD" ></td>
                    <td class="NormalTD"></td>
                    <td class="NormalTD"></td>
                </tr>
                <tr>
                    <td >Shipment HandOver #</td>
                    <td >
                        <ucc:DropDownListChosen ID="drp_shipmentHandover" runat="server" Width="180px">
                        </ucc:DropDownListChosen>
                    </td>
                    <td >
                        <asp:Button ID="Btn_showshipmentHandover" runat="server" OnClick="Btn_showshipmentHandover_Click" Text="Show Shipment Report" Width="163px" ToolTip="Show Selected Shipment Handover Report " Font-Size="Smaller" /></td>
                    <td >&nbsp;</td>
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
                    <td class="NormalTD" >dATE</td>
                    <td class="NormalTD" >
                        <asp:TextBox ID="dtp_deliverydate" runat="server" Width="180px"></asp:TextBox>


                                  <asp:CalendarExtender ID="dtp_deliverydate_CalendarExtender" runat="server" Enabled="True" TargetControlID="dtp_deliverydate">
                        </asp:CalendarExtender>


                                  </td>
                    <td class="NormalTD">
                        </td>
                    <td class="NormalTD">
                        </td>
                    <td class="NormalTD" ></td>
                    <td class="NormalTD" ></td>
                    <td class="NormalTD" ></td>
                    <td class="NormalTD" ></td>
                    <td class="NormalTD"></td>
                    <td c class="NormalTD"></td>
                    <td class="NormalTD"></td>
                    <td class="NormalTD"></td>
                </tr>
                <tr>
                    <td >Job Contract(CM)</td>
                    <td >
                      <ucc:DropDownListChosen ID="drp_jc" runat="server" Width="180px">
                        </ucc:DropDownListChosen></td>
                    <td >
                        <asp:Button ID="btn_showjc" runat="server" OnClick="btn_showjc_Click" Text="Show JCM Report" Width="163px" ToolTip="Show Selected  JCM Report " Font-Size="Smaller" /></td>
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
                    <td >JOB Contract (others)</td>
                    <td >
                        <ucc:DropDownListChosen ID="drp_jcothers" runat="server" Width="180px">
                        </ucc:DropDownListChosen></td>
                    <td>
                        <asp:Button ID="btn_jcothers" runat="server"  Text="Show JC other Report" Width="163px" ToolTip="Show Selected  JCM Report " Font-Size="Smaller" OnClick="btn_jcothers_Click" /></td>
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


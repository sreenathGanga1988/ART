<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="InventoryReports.aspx.cs" Inherits="ArtWebApp.Reports.Inventoryreport.InventoryReports" %>
<%--<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>--%>
<%@ Register assembly="Infragistics35.Web.v12.1, Version=12.1.20121.2236, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.ListControls" tagprefix="ig" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
    

   
    

        .auto-style8 {
            width: 200px;
        }
    

   
    

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
                    <td  class="RedHeadding" colspan="6">Inventory Reports</td>
                </tr>
                <tr>
                    <td  class="NormalTD" colspan="6" >&nbsp;<strong>Total Reports&nbsp;</strong></td>
                </tr>
                <tr>
                    <td class="NormalTD">
                        <asp:Button ID="btn_showtotalInventory" runat="server" Font-Size="Smaller" OnClick="btn_showAtcMRN_Click" Text="Show  Total  Inventory of All Location" Width="274px" />
                    </td>
                    <td class="NormalTD">
                        <asp:Button ID="Button1" runat="server" Font-Size="Smaller" OnClick="Button1_Click" Text="Show Total Trim Inventory" ToolTip="Show the Trim Inventory of  All  Location" Width="204px" />
                    </td >
                    <td class="NormalTD">
                        <asp:Button ID="Button2" runat="server" Font-Size="Smaller" OnClick="Button2_Click" Text="Show  Total Fab Inventory" ToolTip="Show the Fab Inventory All Location" Width="183px" />
                    </td>
                    <td class="NormalTD"><asp:Button ID="Button3" runat="server" Font-Size="Smaller"  Text="Show  Goods in transit" ToolTip="Show theGoods in transit" Width="183px" OnClick="Button3_Click1" /></td>
                    <td class="NormalTD">
                        <asp:Button ID="Button5" runat="server" Font-Size="Smaller"  Text="Show Goods in Transit of all Location (Linear)" ToolTip="Show Goods in Transit of All Location (table)" Width="199px" OnClick="Button5_Click" />
                    </td>
                    <td class="NormalTD">&nbsp;</td>
                    
                </tr>
                <tr>
                    <td colspan="6">&nbsp;<strong>Atc - location&nbsp; Reports</strong></td>
                </tr>
                <tr>
                    <td class="NormalTD" rowspan="2">Atc #</td>
                    <td class="NormalTD"rowspan="2">
                        <ig:WebDropDown ID="drp_Atc" runat="server" DropDownAnimationType="EaseOut" DropDownContainerHeight="300px" DropDownContainerWidth="200px" EnableDropDownAsChild="false" EnableMultipleSelection="True" Height="21px" PageSize="12" TextField="name" ValueField="pk" Width="156px" EnableClosingDropDownOnSelect="False">
                            <DropDownItemBinding TextField="name" ValueField="pk" />
                        </ig:WebDropDown>
                    </td>
                    <td class="NormalTD">
                        <asp:Button ID="btn_showPO" runat="server" OnClick="btn_showPO_Click" Text="Show ATC Inventory " ToolTip="Show Inventory of a ATC In All Location" Width="155px" Font-Size="Smaller" />
                    </td>
                    <td class="NormalTD">
                        <asp:Button ID="btn_trimatc" runat="server" OnClick="btn_trimatc_Click" Text="Show ATC  Trim Inventory " ToolTip="Show  Trim Inventory of a ATC In All Location" Width="155px" Font-Size="Smaller" />
                    </td>
                    <td class="NormalTD"><asp:Button ID="btn_fabatc" runat="server" OnClick="btn_fabatc_Click" Text="Show ATC Fabric Inventory " ToolTip="Show Fabric Inventory of a ATC In All Location" Width="155px" Font-Size="Smaller" /></td>
                    <td class="NormalTD">
                        <asp:Button ID="btn_atcgdtrnst" runat="server" Font-Size="Smaller" OnClick="btn_atcgdtrnst_Click" Text="Show Goods inTransist (Atcwise)" ToolTip="Show Fabric Inventory of a ATC In All Location" Width="208px" />
                    </td>
                  
                </tr>
                <tr>
                    <td class="NormalTD">&nbsp;</td>
                    <td class="NormalTD">
                        <asp:Button ID="btn_atctrimwithZeroBalance" runat="server" OnClick="btn_atctrimwithZeroBalance_Click" Text="Show ATC  Trim Inventory (Onhand Zero)" ToolTip="Show  Trim Inventory of a ATC In All Location with onhand Qty Zero Also" Width="195px" Font-Size="Smaller" />
                    </td>
                    <td class="NormalTD"><asp:Button ID="Button8" runat="server" OnClick="Button8_Click" Text="Show ATC Fabric Inventory " ToolTip="Show Fabric Inventory of a ATC In All Location  with onhand Qty Zero Also" Width="200px" Font-Size="Smaller" /></td>
  
                    <td class="NormalTD">&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style8" rowspan="2" >Location :</td>
                    <td class="auto-style8" rowspan="2">
                        <ig:WebDropDown ID="drp_ToWarehouse" runat="server" Width="189px" TextField="name"
        DropDownContainerHeight="300px" EnableDropDownAsChild="false"
        DropDownContainerWidth="200px" DropDownAnimationType="EaseOut" EnablePaging="True"
        PageSize="12" Height="22px" ValueField="pk" EnableMultipleSelection="True" EnableClosingDropDownOnSelect="False">
                            <DropDownItemBinding TextField="name" ValueField="pk" />
                        </ig:WebDropDown></td>
                   <td class="NormalTD">
                        <asp:Button ID="btn_showlocationInventory" runat="server" OnClick="btn_showlocationInventory_Click" Text="Show Inventory of location" ToolTip="Show Inventory of all Atc In Selected Location" Width="199px" Font-Size="Smaller" />
                    </td>
                  <td class="NormalTD">
                        <asp:Button ID="btn_showlocTrims" runat="server" OnClick="btn_showlocTrims_Click" Text="Show Trim Inventory of location" ToolTip="Show Trim  Inventory of all ATC In Selected Location" Width="199px" Font-Size="Smaller" />
                    </td>
                    
                    <td class="NormalTD"  ><asp:Button ID="btn_showLocFabric" runat="server" OnClick="btn_showLocFabric_Click" Text="Show Fab Inventory of location" ToolTip="Show Fab Inventory of all ATC In Selected Location" Width="199px" Font-Size="Smaller" /></td>
                  <td class="NormalTD"><asp:Button ID="btn_showgstock" runat="server" Text="Show Stock Inventory of location" ToolTip="Show Stock Inventory  In Selected Location" Width="208px" Font-Size="Smaller" OnClick="btn_showgstock_Click" /></td>
                 
                </tr>
                <tr>
                   <td class="NormalTD">
                        <asp:Button ID="btn_showAtcLocInventory" runat="server" Font-Size="Smaller" OnClick="btn_showAtcLocInventory_Click" Text="Show ATC Inventory of location" ToolTip="Show Inventory of Selected ATC In Selected Location" Width="199px" />
                    </td>
                   <td  class="NormalTD">
                        <asp:Button ID="btn_showAtcLocInventory0" runat="server" Font-Size="Smaller" OnClick="btn_showAtcLocInventory0_Click" Text="Show ATC Trim Inventory of location" ToolTip="Show Trim  Inventory of Selected ATC In Selected Location" Width="199px" />
                    </td>
                   <td  class="NormalTD">
                        <asp:Button ID="btn_showAtcLocInventory1" runat="server" Font-Size="Smaller" OnClick="btn_showAtcLocInventory1_Click" Text="Show ATC Fab Inventory of location" ToolTip="Show Fab Inventory of Selected ATC In Selected Location" Width="199px" />
                    </td>
                   <td  class="NormalTD"><asp:Button ID="Button4" runat="server" Font-Size="Smaller" OnClick="Button4_Click" Text="Show Goods in Transit of a Location" ToolTip="Show Goods in Transit of a Location" Width="210px" /></td>
                  
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


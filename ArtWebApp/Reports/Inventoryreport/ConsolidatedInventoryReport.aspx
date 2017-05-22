<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="ConsolidatedInventoryReport.aspx.cs" Inherits="ArtWebApp.Reports.Inventoryreport.ConsolidatedInventoryReport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../css/style.css" rel="stylesheet" /> 
    <style type="text/css">
       
        .auto-style1 {
            font-size: small;
        }
        .auto-style2 {
            height: 27px;
            width: 200px;
            font-size: small;
        }
       
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table>
        <tr>
            <td>
                <div>
                    <table  class="DataEntryTable">
                     <tr>
                    <td class="NormalTD" rowspan="3">Atc #</td>
                    <td class="NormalTD"rowspan="3">
                        <ig:WebDropDown ID="drp_Atc" runat="server" DropDownAnimationType="EaseOut" DropDownContainerHeight="300px" DropDownContainerWidth="200px" EnableDropDownAsChild="false" EnableMultipleSelection="True" Height="21px" PageSize="12" TextField="name" ValueField="pk" Width="156px" EnableClosingDropDownOnSelect="False">
                            <DropDownItemBinding TextField="name" ValueField="pk" />
                        </ig:WebDropDown>
                    </td>
                         <td>&nbsp;</td>
                    <td class="NormalTD">
                        &nbsp;</td>
                  
                </tr>
                     <tr>
                         <td class="auto-style1">Upload Atc list from Excel</td>
                    <td class="NormalTD">
                        <asp:FileUpload ID="FileUpload1" runat="server" />
                         </td>
                         <td>
                             <asp:Button ID="Button1" runat="server"  Text="Fetch" OnClick="Button1_Click" />
                         </td>
                  
                </tr>
                <tr>
                    <td class="auto-style2">Uploaded Atc list</td>
                </tr>
                <tr>
                    <td class="auto-style8" rowspan="3" >Location :</td>
                    <td class="auto-style8" rowspan="3">
                        <ig:WebDropDown ID="drp_ToWarehouse" runat="server" Width="189px" TextField="name"
        DropDownContainerHeight="300px" EnableDropDownAsChild="false"
        DropDownContainerWidth="200px" DropDownAnimationType="EaseOut" EnablePaging="True"
        PageSize="12" Height="22px" ValueField="pk" EnableMultipleSelection="True" EnableClosingDropDownOnSelect="False">
                            <DropDownItemBinding TextField="name" ValueField="pk" />
                        </ig:WebDropDown></td>
                   <td class="auto-style2">
                        <asp:Button ID="btn_showAtcLocInventory" runat="server" Font-Size="Smaller" OnClick="btn_showAtcLocInventory_Click" Text="Show ATC Inventory of location" ToolTip="Show Inventory of Selected ATC In Selected Location" Width="199px" />
                        </td>
                 
                </tr>
                <tr>
                   <td class="auto-style2">
                        <asp:Button ID="btn_showAtcLocTrimInventory" runat="server" Font-Size="Smaller" OnClick="btn_showAtcLocTrimInventory_Click" Text="Show ATC Trim Inventory of location" ToolTip="Show Trim  Inventory of Selected ATC In Selected Location" Width="199px" />
                        </td>
                 
                </tr>
                <tr>
                   <td class="auto-style2"">
                        
                       <asp:Button ID="btn_showAtcLocfabInventory" runat="server" Font-Size="Smaller" OnClick="btn_showAtcLocfabInventory_Click" Text="Show ATC Fab Inventory of location" ToolTip="Show Fab Inventory of Selected ATC In Selected Location" Width="199px" />
                        
                    </td>
                  
                </tr>
                    </table>
                </div>
            </td>
        </tr>
        <tr>
            <td><div>
                <asp:UpdatePanel ID="upl_rpt" runat="server">
                <ContentTemplate>
                    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="100%">
                        <LocalReport ReportPath="Reports\RDLC\APO.rdlc">
                        </LocalReport>
                    </rsweb:ReportViewer>
                </ContentTemplate>

            </asp:UpdatePanel>


                </div></td>
        </tr>
        <tr>
            <td>&nbsp;</td>
        </tr>
    </table>
</asp:Content>

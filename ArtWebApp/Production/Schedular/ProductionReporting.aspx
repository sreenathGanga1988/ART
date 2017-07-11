<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="ProductionReporting.aspx.cs" Inherits="ArtWebApp.Production.Schedular.ProductionReporting" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <table class="DataEntryTable">
        <tr>
            <td class="RedHeadding" colspan="12">Production Reporting</td>
        </tr>
        <tr>
            <td> </td>
            <td>
                &nbsp;</td>
            <td>To </td>
            <td>
                <ig:WebDatePicker ID="dtp_to" runat="server">
                </ig:WebDatePicker>
            </td>
            <td>
                
            </td>
            <td>
                &nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>Upload Atc Number from Excel</td>
            <td>
                <asp:FileUpload ID="FileUpload1" runat="server" />
            </td>
            <td>
                <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Fetch" />
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>Atc</td>
            <td>
                <ig:WebDropDown ID="drp_Atc" runat="server" DropDownAnimationType="EaseOut" DropDownContainerHeight="300px" DropDownContainerWidth="200px" EnableClosingDropDownOnSelect="False" EnableDropDownAsChild="false" EnableMultipleSelection="True" Height="21px" PageSize="12" TextField="name" ValueField="pk" Width="156px">
                    <DropDownItemBinding TextField="name" ValueField="pk" />
                </ig:WebDropDown>
            </td>
            <td>
                <asp:Button ID="btn_fetchData" runat="server" OnClick="btn_sales_Click" Text="Fetch Production Data" />
            </td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
                <td colspan="12" class="ReportViewSection">
            
                    <asp:GridView ID="GridView1" runat="server"></asp:GridView>
        </td>
            

            
        </tr>
    </table>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="Merchandiser_Styledetails" Codebehind="Styledetails.aspx.cs" %>

<%@ Register assembly="Infragistics35.Web.v12.1, Version=12.1.20121.2236, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.LayoutControls" tagprefix="ig" %>

<%@ Register assembly="Infragistics35.Web.v12.1, Version=12.1.20121.2236, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.GridControls" tagprefix="ig1" %>
<%@ Register assembly="Infragistics35.Web.v12.1, Version=12.1.20121.2236, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.ListControls" tagprefix="ig" %>

<%@ Register assembly="Infragistics35.Web.v12.1, Version=12.1.20121.2236, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.NavigationControls" tagprefix="ig2" %>

<%@ Register assembly="Infragistics35.Web.v12.1, Version=12.1.20121.2236, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.ListControls" tagprefix="ig3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .auto-style1 {
            width: 476%;
        }
    .auto-style3 {
            width: 200px;
        }
    .auto-style5 {
        }
    .NormalTD {
            width: 202px;
        }


.igdd_ControlArea
{
	border:solid 1px #BBBBBB;
	table-layout: fixed;
}


.igdd_ValueDisplay
{
	background-color:Transparent;
	font-weight:normal;
	font-size:10pt;
	font-family: Verdana, Arial, Helvetica, sans-serif;
	border-width:0px;
	width: 100%;
}
.igdd_DropDownButton
{
	width: 17px;
	z-index: 9999;
}

.igdd_DropDownListContainer
{
	background-color:White;
	border:solid 1px #BBBBBB;
	float: left;
}


.igdd_DropDownList
{
	background-color:White;
	font-size:10pt;
	font-family: Verdana, Arial, Helvetica, sans-serif;
	margin:0px;
	padding:1px;
}


      
        .auto-style8 {
            height: 27px;
        width: 204px;
    }
     
        .auto-style13 {
            width: 3px;
        }


      

        .auto-style21 {
            font-family: Calibri;
            font-size: small;
        }
       
<td >

    </style><link href="../css/style.css" rel="stylesheet" />
    <script type="text/javascript" id="igClientScript">

 function load() {
            var wdd = $find('<%=drp_ourstyle .ClientID %>');
            wdd.get_selectedItems();
        }

</script>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server" >
    <table class="DataEntryTable">
                    <tr>
                        <td class="RedHeadding" colspan="3">STYLE DETAILS</td>
                    </tr>
                    <tr>
                        <td class="NormalTD">Atc # </td>
                        <td class="NormalTD" >
                             
                 
                    
                  <ucc:DropDownListChosen ID="cmb_atc" runat="server" DataSourceID="atcdata" DataTextField="AtcNum" DataValueField="AtcId" DisableSearchThreshold="10" Width="200px">
                            </ucc:DropDownListChosen>
                               
                        </td>
                        <td class="SearchButtonTD">
                            <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                <ContentTemplate>
                                    <asp:Button ID="BTN_ATC" runat="server" CssClass="auto-style21" OnClick="BTN_ATC_Click" Text="S" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td class="NormalTD">OurStyle </td>
                        <td class="NormalTD" >
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                <ContentTemplate>
                                   

                                      <ucc:DropDownListChosen ID="drp_ourstyle" runat="server" DataTextField="name" DataValueField="pk" DisableSearchThreshold="10" Width="200px">
                            </ucc:DropDownListChosen>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td class="SearchButtonTD" >
                            <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                <ContentTemplate>
                                    <asp:Button ID="Button1" runat="server" CssClass="auto-style21" OnClick="Button1_Click1" Text="S" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td class="NormalTD">New Size :</td>
                        <td class="NormalTD" >
                            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                <ContentTemplate>
                                    <ig:WebDropDown ID="drp_size" runat="server" CurrentValue="Select Size" DataSourceID="SqlDataSource2" EnableClosingDropDownOnSelect="False" EnableMultipleSelection="True" OnSelectionChanged="WebDropDown2_SelectionChanged" TextField="SizeName" ValueField="SizeCode" Width="200px">
                                        <dropdownitembinding textfield="SizeName" valuefield="SizeCode" />
                                    </ig:WebDropDown>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td class="SearchButtonTD" >
                            <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                                <ContentTemplate>
                                    <asp:Button ID="Btn_addSize" runat="server" CssClass="auto-style21" OnClick="Btn_addSize_Click" Text="Add Size" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td class="NormalTD">New Color :</td>
                        <td class="NormalTD" >
                            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                <ContentTemplate>
                                    <ig:WebDropDown ID="drp_color" runat="server" CurrentValue="Select Color" DataSourceID="SqlDataSource3" EnableClosingDropDownOnSelect="False" EnableMultipleSelection="True" TextField="ColorName" ValueField="ColorCode" Width="200px">
                                        <DropDownItemBinding TextField="ColorName" ValueField="ColorCode" />
                                    </ig:WebDropDown>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td class="SearchButtonTD" >
                            <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                <ContentTemplate>
                                    <asp:Button ID="btn_AddColor" runat="server" CssClass="auto-style21" OnClick="btn_AddColor_Click" Text="Add Color" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" >&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style27">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <asp:GridView ID="grd_stylecolor" runat="server" AllowSorting="True" AutoGenerateColumns="False" BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4" DataKeyNames="StyleColorid" DataSourceID="ColorDetailsData" style="font-size: small; font-family: Calibri" Width="100%" OnRowCommand="grd_stylecolor_RowCommand">
                                        <Columns>
                                            <asp:TemplateField HeaderText="StyleColorid" InsertVisible="False" SortExpression="StyleColorid">
                                               
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_stylecolorid" runat="server" Text='<%# Bind("StyleColorid") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="AtcId" HeaderText="AtcId" SortExpression="AtcId" Visible="False" />
                                            <asp:TemplateField HeaderText="OurStyleID" SortExpression="OurStyleID">
                                               
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_ourstyleid" runat="server" Text='<%# Bind("OurStyleID") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="GarmentColorCode" SortExpression="GarmentColorCode">
                                               
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_GarmentColorCode" runat="server" Text='<%# Bind("GarmentColorCode") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="GarmentColor" SortExpression="GarmentColor">
                                               
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_GarmentColor" runat="server" Text='<%# Bind("GarmentColor") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="OurStyle" HeaderText="OurStyle" SortExpression="OurStyle" />
                                             <asp:ButtonField CommandName="Delete" Text="Delete" />
                                        </Columns>
                                        <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
                                        <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="#FFFFCC" />
                                        <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
                                        <RowStyle BackColor="White" ForeColor="#330099" />
                                        <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
                                        <SortedAscendingCellStyle BackColor="#FEFCEB" />
                                        <SortedAscendingHeaderStyle BackColor="#AF0101" />
                                        <SortedDescendingCellStyle BackColor="#F6F0C0" />
                                        <SortedDescendingHeaderStyle BackColor="#7E0000" />
                                    </asp:GridView>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td class="NormalTD">
                            &nbsp;</td>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <ContentTemplate>
                                    <asp:GridView ID="grd_stylesize" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4" DataKeyNames="StyleSizeID" DataSourceID="SizeDetails" style="font-size: small; font-family: Calibri" Width="100%" OnRowCommand="grd_stylesize_RowCommand">
                                        <Columns>
                                            <asp:TemplateField HeaderText="StyleSizeID" SortExpression="StyleSizeID">
                                              
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_stylesizeid" runat="server" Text='<%# Bind("StyleSizeID") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                              <asp:TemplateField HeaderText="OurStyleID" SortExpression="OurStyleID">
                                               
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_ourstyleid" runat="server" Text='<%# Bind("OurStyleID") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="OurStyle" HeaderText="OurStyle" SortExpression="OurStyle" />
                                            <asp:TemplateField HeaderText="SizeCode" SortExpression="SizeCode">
                                               
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_sizecode" runat="server" Text='<%# Bind("SizeCode") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="SizeName" SortExpression="SizeName">
                                             
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_sizename" runat="server" Text='<%# Bind("SizeName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:ButtonField CommandName="Delete" Text="Delete" />
                                        </Columns>
                                        <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
                                        <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="#FFFFCC" />
                                        <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
                                        <RowStyle BackColor="White" ForeColor="#330099" />
                                        <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
                                        <SortedAscendingCellStyle BackColor="#FEFCEB" />
                                        <SortedAscendingHeaderStyle BackColor="#AF0101" />
                                        <SortedDescendingCellStyle BackColor="#F6F0C0" />
                                        <SortedDescendingHeaderStyle BackColor="#7E0000" />
                                    </asp:GridView>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
       <table class="FullTable">
        <tr>
            <td class="auto-style3">
                &nbsp;
                <table class="auto-style1">
                    <tr>
                        <td class="auto-style7">&nbsp;</td>
                        <td class="auto-style8">
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    </table>
            </td>
        </tr>
        <tr>
            <td class="FullTable">
                <table class="DataEntryTable">
                    <tr>
                        <td class="NormalTD">
                            <asp:SqlDataSource ID="atcdata" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT DISTINCT [AtcNum], [AtcId] FROM [AtcMaster] ORDER BY [AtcNum], [AtcId]"></asp:SqlDataSource>
                        </td>
                        <td class="NormalTD">
                            &nbsp;</td>
                        <td class="NormalTD">
                            &nbsp;</td>
                        <td class="NormalTD">&nbsp;</td>
                        <td class="NormalTD">
                            &nbsp;</td>
                        <td class="NormalTD"></td>
                    </tr>
                    <tr>
                        <td class="NormalTD">
                            <asp:HiddenField ID="HiddenField1" runat="server" />
                        </td>
                        <td class="NormalTD">
                            &nbsp;</td>
                        <td class="NormalTD">
                            &nbsp;</td>
                        <td class="NormalTD">&nbsp;</td>
                        <td class="NormalTD">&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style5">
                            <asp:SqlDataSource ID="ColorDetailsData" runat="server" ConflictDetection="CompareAllValues" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" OldValuesParameterFormatString="original_{0}" SelectCommand="SELECT StyleColorid, AtcId, OurStyleID, GarmentColorCode, GarmentColor, OurStyle FROM StyleColor WHERE (OurStyleID = @OurStyle) ORDER BY OurStyleID, GarmentColorCode">
                                <SelectParameters>
                                    <asp:SessionParameter DefaultValue="0" Name="OurStyle" SessionField="ourstyleID" Type="String" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                        </td>
                        <td class="NormalTD">
                <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT [SizeCode], [SizeName] FROM [SizeMaster] ORDER BY [SizeCode]"></asp:SqlDataSource>
                        </td>
                        <td class="auto-style13">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td class="NormalTD">
                        
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="NormalTD">
                <asp:SqlDataSource ID="SizeDetails" runat="server" ConflictDetection="CompareAllValues" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" OldValuesParameterFormatString="original_{0}" SelectCommand="SELECT OurStyle, SizeName, SizeCode, StyleSizeID, OurStyleID FROM StyleSize WHERE (OurStyleID = @OurStyleID)">
                    <SelectParameters>
                        <asp:SessionParameter DefaultValue="0" Name="OurStyleID" SessionField="OurstyleId" Type="Decimal" />
                    </SelectParameters>
                </asp:SqlDataSource>
                        </td>
                        <td class="NormalTD">
                        <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT [ColorCode], [ColorName] FROM [ColorMaster]">
                </asp:SqlDataSource>
                            </td>
                        <td class="NormalTD"></td>
                        <td class="NormalTD"></td>
                        <td class="NormalTD">
                               
                
                    
               
                               
                            </td>
                        <td class="NormalTD"></td>
                    </tr>
                    
                </table>
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
        </tr>
    </table>
</asp:Content>


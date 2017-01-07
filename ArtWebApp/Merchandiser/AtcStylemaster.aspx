<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="AtcStylemaster.aspx.cs" Inherits="ArtWebApp.Merchandiser.AtcStylemaster" %>
<%@ Register assembly="Infragistics35.Web.v12.1, Version=12.1.20121.2236, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.ListControls" tagprefix="ig" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style9 {
            width: 139px;
        }
        .auto-style10 {
            height: 23px;
            width: 139px;
        }


.igdd_ControlArea
{
	border:solid 1px #BBBBBB;
	table-layout: fixed;
}


.igdd_ControlArea
{
	border:solid 1px #BBBBBB;
	table-layout: fixed;
}


.igdd_ControlArea
{
	border:solid 1px #BBBBBB;
	table-layout: fixed;
}


.igdd_ControlArea
{
	border:solid 1px #BBBBBB;
	table-layout: fixed;
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
.igdd_ValueDisplay
{
	background-color:Transparent;
	font-weight:normal;
	font-size:10pt;
	font-family: Verdana, Arial, Helvetica, sans-serif;
	border-width:0px;
	width: 100%;
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
.igdd_ValueDisplay
{
	background-color:Transparent;
	font-weight:normal;
	font-size:10pt;
	font-family: Verdana, Arial, Helvetica, sans-serif;
	border-width:0px;
	width: 100%;
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

.igdd_DropDownButton
{
	width: 17px;
	z-index: 9999;
}

.igdd_DropDownButton
{
	width: 17px;
	z-index: 9999;
}

.igdd_DropDownButton
{
	width: 17px;
	z-index: 9999;
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


.igdd_DropDownListContainer
{
	background-color:White;
	border:solid 1px #BBBBBB;
	float: left;
}


.igdd_DropDownListContainer
{
	background-color:White;
	border:solid 1px #BBBBBB;
	float: left;
}


.igdd_DropDownListContainer
{
	background-color:White;
	border:solid 1px #BBBBBB;
	float: left;
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


.igdd_DropDownList
{
	background-color:White;
	font-size:10pt;
	font-family: Verdana, Arial, Helvetica, sans-serif;
	margin:0px;
	padding:1px;
}


.igdd_DropDownList
{
	background-color:White;
	font-size:10pt;
	font-family: Verdana, Arial, Helvetica, sans-serif;
	margin:0px;
	padding:1px;
}


.igdd_DropDownList
{
	background-color:White;
	font-size:10pt;
	font-family: Verdana, Arial, Helvetica, sans-serif;
	margin:0px;
	padding:1px;
}


.igdd_DropDownList
{
	background-color:White;
	font-size:10pt;
	font-family: Verdana, Arial, Helvetica, sans-serif;
	margin:0px;
	padding:1px;
}


    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="auto-style1">
        <tr>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style7">
                <table class="auto-style1">
                    <tr>
                        <td class="auto-style9">Atc # </td>
                        <td class="auto-style3">
                             
                    <ig:WebDropDown ID="cmb_atc" runat="server" Width="200px" 
                    DataSourceID="SqlDataSource1" TextField="Atcnum" ValueField="AtcId" 
                                      >
                    <DropDownItemBinding TextField="Atcnum" ValueField="AtcId" />
                </ig:WebDropDown>
                    
               
                               
                            </td>
                        <td>
                                 
                    <asp:Button ID="buttonAtc" runat="server" Text="S" Height="26px" OnClick="buttonAtc_Click" />
                     
                            </td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style10">OurStyle</td>
                        <td class="auto-style6">
                        <ig:WebDropDown ID="cmb_ourstyle" runat="server" Width="196px" TextField="name"
        DropDownContainerHeight="300px" EnableDropDownAsChild="false"
        DropDownContainerWidth="200px" DropDownAnimationType="EaseOut" EnablePaging="True"
        PageSize="12" Height="22px" ValueField="pk" CurrentValue="Select OurStyle">
                            <DropDownItemBinding TextField="name" ValueField="pk" />
                        </ig:WebDropDown>
                        </td>
                        <td class="auto-style7">
                                 
                    <asp:Button ID="buttonAtc0" runat="server" Text="S" Height="26px" OnClick="buttonAtc_Click" />
                     
                            </td>
                        <td class="auto-style7">&nbsp;</td>
                        <td class="auto-style7">&nbsp;</td>
                        <td class="auto-style7">&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style9">&nbsp;</td>
                        <td class="auto-style3">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <table class="auto-style1">
                            <tr>
                                <td>
                                    <asp:CheckBoxList ID="CheckBoxList1" runat="server" CellSpacing="1" DataSourceID="Colordata" DataTextField="ColorName" DataValueField="ColorCode" Font-Italic="False" RepeatColumns="200" Width="100%">
                                    </asp:CheckBoxList>
                                </td>
                                <td>
                                    <asp:CheckBoxList ID="CheckBoxList2" runat="server" Width="100%">
                                    </asp:CheckBoxList>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td>
                               
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" 
                    SelectCommand="SELECT DISTINCT [AtcNum], [AtcId] FROM [AtcMaster] ORDER BY [AtcNum], [AtcId]">
                </asp:SqlDataSource>
                    
               
                               
           <asp:SqlDataSource ID="Colordata" runat="server" ConflictDetection="CompareAllValues" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" DeleteCommand="DELETE FROM [ColorMaster] WHERE [ColorId] = @original_ColorId AND (([ColorCode] = @original_ColorCode) OR ([ColorCode] IS NULL AND @original_ColorCode IS NULL)) AND (([ColorName] = @original_ColorName) OR ([ColorName] IS NULL AND @original_ColorName IS NULL))" InsertCommand="INSERT INTO [ColorMaster] ([ColorCode], [ColorName]) VALUES (@ColorCode, @ColorName)" OldValuesParameterFormatString="original_{0}" SelectCommand="SELECT DISTINCT ColorName, ColorCode FROM ColorMaster ORDER BY ColorName, ColorCode" UpdateCommand="UPDATE [ColorMaster] SET [ColorCode] = @ColorCode, [ColorName] = @ColorName WHERE [ColorId] = @original_ColorId AND (([ColorCode] = @original_ColorCode) OR ([ColorCode] IS NULL AND @original_ColorCode IS NULL)) AND (([ColorName] = @original_ColorName) OR ([ColorName] IS NULL AND @original_ColorName IS NULL))">
               <DeleteParameters>
                   <asp:Parameter Name="original_ColorId" Type="Decimal" />
                   <asp:Parameter Name="original_ColorCode" Type="String" />
                   <asp:Parameter Name="original_ColorName" Type="String" />
               </DeleteParameters>
               <InsertParameters>
                   <asp:Parameter Name="ColorCode" Type="String" />
                   <asp:Parameter Name="ColorName" Type="String" />
               </InsertParameters>
               <UpdateParameters>
                   <asp:Parameter Name="ColorCode" Type="String" />
                   <asp:Parameter Name="ColorName" Type="String" />
                   <asp:Parameter Name="original_ColorId" Type="Decimal" />
                   <asp:Parameter Name="original_ColorCode" Type="String" />
                   <asp:Parameter Name="original_ColorName" Type="String" />
               </UpdateParameters>
           </asp:SqlDataSource>
                    
               
                               
                            </td>
        </tr>
    </table>
</asp:Content>

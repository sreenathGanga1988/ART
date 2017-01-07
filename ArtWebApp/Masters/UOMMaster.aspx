<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="UOMMaster.aspx.cs" Inherits="ArtWebApp.Masters.UOMMaster" %>
<%@ Register assembly="Infragistics35.Web.v12.1, Version=12.1.20121.2236, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.GridControls" tagprefix="ig" %>
<%@ Register assembly="Infragistics35.Web.v12.1, Version=12.1.20121.2236, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.ListControls" tagprefix="ig" %>
<%@ Register assembly="Infragistics35.Web.v12.1, Version=12.1.20121.2236, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.ListControls" tagprefix="ig1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">

        .auto-style9 {
            width: 133px;
        }
        .auto-style10 {
            width: 169px;
        }
    .auto-style11 {
        height: 30px;
    }
    
.igg_HeaderCaption
{
	font-size: 12px;
	padding: 3px 0 3px 8px;
	overflow: hidden;
	text-align:left;
	border-top:1px solid #a0c3de;
    border-right:1px solid #5593c3;
    border-bottom:1px solid #5593c3;
    border-left:1px solid #a0c3de;
}

.igg_HeaderCaption
{
	font-size: 12px;
	padding: 3px 0 3px 8px;
	overflow: hidden;
	text-align:left;
	border-top:1px solid #a0c3de;
    border-right:1px solid #5593c3;
    border-bottom:1px solid #5593c3;
    border-left:1px solid #a0c3de;
}

.igg_HeaderCaption
{
	font-size: 12px;
	padding: 3px 0 3px 8px;
	overflow: hidden;
	text-align:left;
	border-top:1px solid #a0c3de;
    border-right:1px solid #5593c3;
    border-bottom:1px solid #5593c3;
    border-left:1px solid #a0c3de;
}


.igg_FilterButton
{
	background-color:Transparent;
	border-style:solid;
	border-width:0px;
	height: 18px;
}


.igg_FilterButton
{
	background-color:Transparent;
	border-style:solid;
	border-width:0px;
	height: 18px;
}


.igg_FilterButton
{
	background-color:Transparent;
	border-style:solid;
	border-width:0px;
	height: 18px;
}


    .auto-style12 {
        width: 100%;
    }
    .auto-style13 {
    }
    .auto-style14 {
        width: 219px;
    }
    .auto-style15 {
        width: 147px;
    }


    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:MultiView ID="MultiView1" runat="server">
        <asp:View ID="View1" runat="server">
            <table class="auto-style1">
                <tr>
                    <td>
                        <asp:FormView ID="FormView1" runat="server" DataKeyNames="Uom_PK" DataSourceID="uommstr">
                            <InsertItemTemplate>
                                UomCode:
                                <asp:TextBox ID="UomCodeTextBox" runat="server" Text='<%# Bind("UomCode") %>' />
                                <br />
                                UomName:
                                <asp:TextBox ID="UomNameTextBox" runat="server" Text='<%# Bind("UomName") %>' />
                                <br />
                                UomType:
                                <asp:TextBox ID="UomTypeTextBox" runat="server" Text='<%# Bind("UomType") %>' />
                                <br />
                                <asp:LinkButton ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert" Text="Insert" />
                                &nbsp;<asp:LinkButton ID="InsertCancelButton" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel" />
                            </InsertItemTemplate>
                            <ItemTemplate>
                                &nbsp;<asp:LinkButton ID="NewButton" runat="server" CausesValidation="False" CommandName="New" Text="New" />
                            </ItemTemplate>
                        </asp:FormView>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style7">
                        <asp:GridView ID="GridView2" runat="server" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="Uom_PK" DataSourceID="uommstr" CellPadding="4" ForeColor="#333333" GridLines="None" Height="364px">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <Columns>
                                <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
                                <asp:BoundField DataField="Uom_PK" HeaderText="Uom_PK" InsertVisible="False" ReadOnly="True" SortExpression="Uom_PK" />
                                <asp:BoundField DataField="UomCode" HeaderText="UomCode" SortExpression="UomCode" />
                                <asp:BoundField DataField="UomName" HeaderText="UomName" SortExpression="UomName" />
                                <asp:BoundField DataField="UomType" HeaderText="UomType" SortExpression="UomType" />
                            </Columns>
                            <EditRowStyle BackColor="#999999" />
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <SortedAscendingCellStyle BackColor="#E9E7E2" />
                            <SortedAscendingHeaderStyle BackColor="#506C8C" />
                            <SortedDescendingCellStyle BackColor="#FFFDF8" />
                            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:SqlDataSource ID="uommstr" runat="server" ConflictDetection="CompareAllValues" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" DeleteCommand="DELETE FROM [UOMMaster] WHERE [Uom_PK] = @original_Uom_PK AND (([UomCode] = @original_UomCode) OR ([UomCode] IS NULL AND @original_UomCode IS NULL)) AND (([UomName] = @original_UomName) OR ([UomName] IS NULL AND @original_UomName IS NULL)) AND (([UomType] = @original_UomType) OR ([UomType] IS NULL AND @original_UomType IS NULL))" InsertCommand="INSERT INTO [UOMMaster] ([UomCode], [UomName], [UomType]) VALUES (@UomCode, @UomName, @UomType)" OldValuesParameterFormatString="original_{0}" SelectCommand="SELECT [Uom_PK], [UomCode], [UomName], [UomType] FROM [UOMMaster] ORDER BY [UomCode], [UomName]" UpdateCommand="UPDATE [UOMMaster] SET [UomCode] = @UomCode, [UomName] = @UomName, [UomType] = @UomType WHERE [Uom_PK] = @original_Uom_PK AND (([UomCode] = @original_UomCode) OR ([UomCode] IS NULL AND @original_UomCode IS NULL)) AND (([UomName] = @original_UomName) OR ([UomName] IS NULL AND @original_UomName IS NULL)) AND (([UomType] = @original_UomType) OR ([UomType] IS NULL AND @original_UomType IS NULL))">
                            <DeleteParameters>
                                <asp:Parameter Name="original_Uom_PK" Type="Decimal" />
                                <asp:Parameter Name="original_UomCode" Type="String" />
                                <asp:Parameter Name="original_UomName" Type="String" />
                                <asp:Parameter Name="original_UomType" Type="String" />
                            </DeleteParameters>
                            <InsertParameters>
                                <asp:Parameter Name="UomCode" Type="String" />
                                <asp:Parameter Name="UomName" Type="String" />
                                <asp:Parameter Name="UomType" Type="String" />
                            </InsertParameters>
                            <UpdateParameters>
                                <asp:Parameter Name="UomCode" Type="String" />
                                <asp:Parameter Name="UomName" Type="String" />
                                <asp:Parameter Name="UomType" Type="String" />
                                <asp:Parameter Name="original_Uom_PK" Type="Decimal" />
                                <asp:Parameter Name="original_UomCode" Type="String" />
                                <asp:Parameter Name="original_UomName" Type="String" />
                                <asp:Parameter Name="original_UomType" Type="String" />
                            </UpdateParameters>
                        </asp:SqlDataSource>
                    </td>
                </tr>
            </table>
        </asp:View>
        <asp:View ID="View2" runat="server">
            <table class="auto-style1">
                <tr>
                    <td>
                        <table class="auto-style1">
                            <tr>
                                <td class="auto-style9">Base UOM</td>
                                <td>
                                   
                                    <ig:WebDropDown ID="drp_baseuom" runat="server" DataSourceID="BaseUOM" Width="200px" TextField="UomCode" ValueField="Uom_PK">
                                        <DropDownItemBinding TextField="UomCode" ValueField="Uom_PK" />
                                    </ig:WebDropDown>
                                   
                                </td>
                                <td class="auto-style10">AltUOM</td>
                                <td>
                                   
                                    <ig:WebDropDown ID="drp_altuom" runat="server" DataSourceID="CommonUom" Width="200px" TextField="UomCode" ValueField="Uom_PK">
                                        <DropDownItemBinding TextField="UomCode" ValueField="Uom_PK" />
                                    </ig:WebDropDown>
                                   
                                </td>
                                <td>Operator</td>
                                <td>
                                    <asp:DropDownList ID="drp_op" runat="server">
                                        <asp:ListItem>/</asp:ListItem>
                                        <asp:ListItem>*</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>Conv factor: </td>
                                <td>
                                    
                                    <asp:TextBox ID="txt_convfact" runat="server"></asp:TextBox>
                                    
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style11">
                        <asp:Button ID="Button1" runat="server" Text="Save" OnClick="Button1_Click" />
                    </td>
                </tr>
                <tr>
                    <td class="auto-style11">
                        <ig:WebDataGrid ID="dgv_altuom" runat="server" AutoGenerateColumns="False" DataSourceID="convfact" Height="350px" Width="835px">
                            <Columns>
                                <ig:BoundDataField DataFieldName="AltUom_ID" Key="AltUom_ID">
                                    <Header Text="AltUom_ID">
                                    </Header>
                                </ig:BoundDataField>
                                <ig:BoundDataField DataFieldName="UomCode" Key="UomCode">
                                    <Header Text="UomCode">
                                    </Header>
                                </ig:BoundDataField>
                                <ig:BoundDataField DataFieldName="Operator" Key="Operator">
                                    <Header Text="Operator">
                                    </Header>
                                </ig:BoundDataField>
                                <ig:BoundDataField DataFieldName="Conv_fact" Key="Conv_fact">
                                    <Header Text="Conv_fact">
                                    </Header>
                                </ig:BoundDataField>
                                <ig:BoundDataField DataFieldName="Equals" Key="Equals">
                                    <Header Text="Equals">
                                    </Header>
                                </ig:BoundDataField>
                            </Columns>
                            <Behaviors>
                                <ig:EditingCore>
                                    <Behaviors>
                                        <ig:CellEditing>
                                        </ig:CellEditing>
                                        <ig:RowAdding>
                                        </ig:RowAdding>
                                    </Behaviors>
                                </ig:EditingCore>
                                <ig:Filtering>
                                </ig:Filtering>
                            </Behaviors>
                        </ig:WebDataGrid>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:SqlDataSource ID="BaseUOM" runat="server" ConflictDetection="CompareAllValues" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" OldValuesParameterFormatString="original_{0}" SelectCommand="SELECT Uom_PK, UomCode FROM UOMMaster WHERE (UomType = N'BASE') ORDER BY UomCode, UomName"></asp:SqlDataSource>
                        <asp:SqlDataSource ID="convfact" runat="server" ConflictDetection="CompareAllValues" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" OldValuesParameterFormatString="original_{0}" SelectCommand="SELECT AltUOMMaster.AltUom_ID, UOMMaster.UomCode, AltUOMMaster.Operator, AltUOMMaster.Conv_fact, UOMMaster_1.UomCode AS Equals FROM AltUOMMaster INNER JOIN UOMMaster ON AltUOMMaster.Uom_PK = UOMMaster.Uom_PK INNER JOIN UOMMaster AS UOMMaster_1 ON AltUOMMaster.AltUom_PK = UOMMaster_1.Uom_PK"></asp:SqlDataSource>
                        <br />
                        <asp:SqlDataSource ID="CommonUom" runat="server" ConflictDetection="CompareAllValues" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" OldValuesParameterFormatString="original_{0}" SelectCommand="SELECT Uom_PK, UomCode FROM UOMMaster WHERE (UomType = N'COMMON') ORDER BY UomCode, UomName"></asp:SqlDataSource>
                    </td>
                </tr>
            </table>
        </asp:View>
        <asp:View ID="View3" runat="server">
            
            <table class="auto-style12">
                <tr>
                    <td colspan="6" style="text-align: center; font-weight: 700">PO Exchange Rate</td>
                </tr>
                <tr>
                    <td class="auto-style13">Currency&nbsp; </td>
                    <td class="auto-style14">
                        <ig1:WebDropDown ID="drp_currency" runat="server" CurrentValue="USD" DataSourceID="currencydata" Height="23px" TextField="CurrencyCode" ValueField="CurrencyID" Width="200px"><dropdownitembinding textfield="CurrencyCode" valuefield="CurrencyID" /></ig1:WebDropDown>
                    </td>
                    <td>/</td>
                    <td class="auto-style15">
                        <asp:TextBox ID="txt_rate" runat="server"></asp:TextBox>
                    </td>
                    <td>=</td>
                    <td>1 USD</td>
                </tr>
                <tr>
                    <td class="auto-style13">
                        <asp:Button ID="Btn_exrate" runat="server" OnClick="Btn_exrate_Click" Text="Save" />
                    </td>
                    <td class="auto-style14">&nbsp;</td>
                    <td>&nbsp;</td>
                    <td class="auto-style15">&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style13" colspan="6">
                        <ig:WebDataGrid ID="dgv_porate" runat="server" AutoGenerateColumns="False" DataSourceID="conversiondata" Height="350px" Width="835px">
                            <Columns>
                                <ig:BoundDataField DataFieldName="CurrencyCode" Key="CurrencyCode">
                                    <Header Text="CurrencyCode">
                                    </Header>
                                </ig:BoundDataField>
                                <ig:BoundDataField DataFieldName="Operator" Key="Operator">
                                    <Header Text="Operator">
                                    </Header>
                                </ig:BoundDataField>
                                <ig:BoundDataField DataFieldName="Convrate" Key="Convrate">
                                    <Header Text="Convrate">
                                    </Header>
                                </ig:BoundDataField>
                                <ig:BoundDataField DataFieldName="result" Key="result">
                                    <Header Text="result">
                                    </Header>
                                </ig:BoundDataField>
                                <ig:BoundDataField DataFieldName="ExDate" Key="ExDate">
                                    <Header Text="ExDate">
                                    </Header>
                                </ig:BoundDataField>
                                <ig:BoundDataField DataFieldName="AddedBy" Key="AddedBy">
                                    <Header Text="AddedBy">
                                    </Header>
                                </ig:BoundDataField>
                            </Columns>
                            <Behaviors>
                                <ig:EditingCore>
                                    <Behaviors>
                                        <ig:CellEditing>
                                        </ig:CellEditing>
                                        <ig:RowAdding>
                                        </ig:RowAdding>
                                    </Behaviors>
                                </ig:EditingCore>
                                <ig:Filtering>
                                </ig:Filtering>
                            </Behaviors>
                        </ig:WebDataGrid>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style13">
                        <asp:SqlDataSource ID="currencydata" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" ProviderName="<%$ ConnectionStrings:ArtConnectionString.ProviderName %>" SelectCommand="SELECT * FROM [CurrencyMaster] ORDER BY [CurrencyCode], [CurrencyID]"></asp:SqlDataSource>
                        <br />
                        <br />
                        <asp:SqlDataSource ID="conversiondata" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT        CurrencyMaster.CurrencyCode ,'/' as Operator, POCurrExRate.Convrate,'= 1 USD' as result, POCurrExRate.ExDate, POCurrExRate.AddedBy
FROM            POCurrExRate INNER JOIN
                         CurrencyMaster ON POCurrExRate.CurrencyID = CurrencyMaster.CurrencyID"></asp:SqlDataSource>
                    </td>
                    <td class="auto-style14">&nbsp;</td>
                    <td>&nbsp;</td>
                    <td class="auto-style15">&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
            </table>
            
        </asp:View>
    </asp:MultiView>
</asp:Content>

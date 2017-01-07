<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="Masters_CountryCreation" Codebehind="CountryCreation.aspx.cs" %>

<%@ Register assembly="Infragistics35.Web.v12.1, Version=12.1.20121.2236, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.GridControls" tagprefix="ig" %>

<%@ Register assembly="Infragistics35.Web.v12.1, Version=12.1.20121.2236, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.LayoutControls" tagprefix="ig" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
    .auto-style1 {
        width: 100%;
    }
</style>
    <script type="text/javascript" id="igClientScript">


      
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table class="auto-style1">
    <tr>
        <td>
            <ig:WebTab ID="WebTab1" runat="server" Height="95%" Width="100%" SelectedIndex="1">
                <tabs>
                    <ig:ContentTabItem runat="server" Text="Country">
                        <Template>
                            <div>
                                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConflictDetection="CompareAllValues" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" DeleteCommand="DELETE FROM [CountryMaster] WHERE [CountryID] = @original_CountryID AND (([ShortName] = @original_ShortName) OR ([ShortName] IS NULL AND @original_ShortName IS NULL)) AND (([Description] = @original_Description) OR ([Description] IS NULL AND @original_Description IS NULL)) AND (([FactoryStores] = @original_FactoryStores) OR ([FactoryStores] IS NULL AND @original_FactoryStores IS NULL))" InsertCommand="INSERT INTO [CountryMaster] ([ShortName], [Description], [FactoryStores]) VALUES (@ShortName, @Description, @FactoryStores)" OldValuesParameterFormatString="original_{0}" SelectCommand="SELECT [CountryID], [ShortName], [Description], [FactoryStores] FROM [CountryMaster]" UpdateCommand="UPDATE [CountryMaster] SET [ShortName] = @ShortName, [Description] = @Description, [FactoryStores] = @FactoryStores WHERE [CountryID] = @original_CountryID AND (([ShortName] = @original_ShortName) OR ([ShortName] IS NULL AND @original_ShortName IS NULL)) AND (([Description] = @original_Description) OR ([Description] IS NULL AND @original_Description IS NULL)) AND (([FactoryStores] = @original_FactoryStores) OR ([FactoryStores] IS NULL AND @original_FactoryStores IS NULL))">
                                    <DeleteParameters>
                                        <asp:Parameter Name="original_CountryID" Type="Decimal" />
                                        <asp:Parameter Name="original_ShortName" Type="String" />
                                        <asp:Parameter Name="original_Description" Type="String" />
                                        <asp:Parameter Name="original_FactoryStores" Type="String" />
                                    </DeleteParameters>
                                    <InsertParameters>
                                        <asp:Parameter Name="ShortName" Type="String" />
                                        <asp:Parameter Name="Description" Type="String" />
                                        <asp:Parameter Name="FactoryStores" Type="String" />
                                    </InsertParameters>
                                    <UpdateParameters>
                                        <asp:Parameter Name="ShortName" Type="String" />
                                        <asp:Parameter Name="Description" Type="String" />
                                        <asp:Parameter Name="FactoryStores" Type="String" />
                                        <asp:Parameter Name="original_CountryID" Type="Decimal" />
                                        <asp:Parameter Name="original_ShortName" Type="String" />
                                        <asp:Parameter Name="original_Description" Type="String" />
                                        <asp:Parameter Name="original_FactoryStores" Type="String" />
                                    </UpdateParameters>
                                </asp:SqlDataSource>
                                <ig:WebDataGrid ID="WebDataGrid1" runat="server" DataSourceID="SqlDataSource1" Height="350px" Width="100%">
                                    <Behaviors>
                                        <ig:Selection CellClickAction="Row" RowSelectType="Single">
                                        </ig:Selection>
                                        <ig:RowSelectors>
                                        </ig:RowSelectors>
                                        <ig:EditingCore>
                                            <Behaviors>
                                                <ig:CellEditing>
                                                </ig:CellEditing>
                                                <ig:RowAdding>
                                                </ig:RowAdding>
                                                <ig:RowDeleting />
                                            </Behaviors>
                                        </ig:EditingCore>
                                        <ig:Filtering>
                                        </ig:Filtering>
                                        <ig:Paging>
                                        </ig:Paging>
                                    </Behaviors>
                                </ig:WebDataGrid>
                                <br />
                            </div>
                        </Template>
                    </ig:ContentTabItem>
                    <ig:ContentTabItem runat="server" Text="Currency">
                        <Template>
                            <div>
                                <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConflictDetection="CompareAllValues" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" DeleteCommand="DELETE FROM [CurrencyMaster] WHERE [CurrencyID] = @original_CurrencyID AND (([CurrencyCode] = @original_CurrencyCode) OR ([CurrencyCode] IS NULL AND @original_CurrencyCode IS NULL)) AND (([CurrencyName] = @original_CurrencyName) OR ([CurrencyName] IS NULL AND @original_CurrencyName IS NULL))" InsertCommand="INSERT INTO [CurrencyMaster] ([CurrencyCode], [CurrencyName]) VALUES (@CurrencyCode, @CurrencyName)" OldValuesParameterFormatString="original_{0}" SelectCommand="SELECT CurrencyID, CurrencyCode, CurrencyName FROM CurrencyMaster" UpdateCommand="UPDATE [CurrencyMaster] SET [CurrencyCode] = @CurrencyCode, [CurrencyName] = @CurrencyName WHERE [CurrencyID] = @original_CurrencyID AND (([CurrencyCode] = @original_CurrencyCode) OR ([CurrencyCode] IS NULL AND @original_CurrencyCode IS NULL)) AND (([CurrencyName] = @original_CurrencyName) OR ([CurrencyName] IS NULL AND @original_CurrencyName IS NULL))">
                                    <DeleteParameters>
                                        <asp:Parameter Name="original_CurrencyID" Type="Decimal" />
                                        <asp:Parameter Name="original_CurrencyCode" Type="String" />
                                        <asp:Parameter Name="original_CurrencyName" Type="String" />
                                    </DeleteParameters>
                                    <InsertParameters>
                                        <asp:Parameter Name="CurrencyCode" Type="String" />
                                        <asp:Parameter Name="CurrencyName" Type="String" />
                                    </InsertParameters>
                                    <UpdateParameters>
                                        <asp:Parameter Name="CurrencyCode" Type="String" />
                                        <asp:Parameter Name="CurrencyName" Type="String" />
                                        <asp:Parameter Name="original_CurrencyID" Type="Decimal" />
                                        <asp:Parameter Name="original_CurrencyCode" Type="String" />
                                        <asp:Parameter Name="original_CurrencyName" Type="String" />
                                    </UpdateParameters>
                                </asp:SqlDataSource>
                                <ig:WebDataGrid ID="WebDataGrid2" runat="server" DataSourceID="SqlDataSource2" Height="350px" Width="100%">
                                    <Behaviors>
                                        <ig:Selection CellClickAction="Row" RowSelectType="Single">
                                        </ig:Selection>
                                        <ig:RowSelectors>
                                        </ig:RowSelectors>
                                        <ig:EditingCore>
                                            <Behaviors>
                                                <ig:CellEditing>
                                                </ig:CellEditing>
                                                <ig:RowAdding>
                                                </ig:RowAdding>
                                                <ig:RowDeleting />
                                            </Behaviors>
                                        </ig:EditingCore>
                                        <ig:Filtering>
                                        </ig:Filtering>
                                        <ig:Paging>
                                        </ig:Paging>
                                    </Behaviors>
                                </ig:WebDataGrid>
                                <br />
                            </div>
                        </Template>
                    </ig:ContentTabItem>
                </tabs>
            </ig:WebTab>
            <br />
        </td>
    </tr>
    <tr>
        <td>&nbsp;</td>
    </tr>
</table>
</asp:Content>


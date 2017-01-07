<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="Types.aspx.cs" Inherits="ArtWebApp.Masters.Types" %>
<%@ Register assembly="Infragistics35.Web.v12.1, Version=12.1.20121.2236, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.GridControls" tagprefix="ig" %>
<%@ Register assembly="Infragistics35.Web.v12.1, Version=12.1.20121.2236, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.ListControls" tagprefix="ig" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../css/style.css" rel="stylesheet" />
  </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:MultiView ID="MultiView1" runat="server">
        <asp:View ID="View1" runat="server">
            <table class="FullTable">
                <tr>
                    <td class="RedHeadding">LOCATION TYPE</td>
                </tr>
                <tr>
                    <td class="FullTable">
                        <ig:WebDataGrid ID="WebDataGrid1" runat="server" AutoGenerateColumns="False" DataSourceID="Wharehousetypedata" Height="350px" Width="400px">
                            <Columns>
                                <ig:BoundDataField DataFieldName="LocationType_Pk" Key="LocationType_Pk">
                                    <Header Text="LocationType_Pk">
                                    </Header>
                                </ig:BoundDataField>
                                <ig:BoundDataField DataFieldName="LocationType" Key="LocationType">
                                    <Header Text="LocationType">
                                    </Header>
                                </ig:BoundDataField>
                                <ig:BoundDataField DataFieldName="TypeFor" Key="TypeFor">
                                    <Header Text="TypeFor">
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
                        <asp:SqlDataSource ID="Wharehousetypedata" runat="server" ConflictDetection="CompareAllValues" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" DeleteCommand="DELETE FROM [LocationType] WHERE [LocationType_Pk] = @original_LocationType_Pk AND (([LocationType] = @original_LocationType) OR ([LocationType] IS NULL AND @original_LocationType IS NULL)) AND (([TypeFor] = @original_TypeFor) OR ([TypeFor] IS NULL AND @original_TypeFor IS NULL))" InsertCommand="INSERT INTO [LocationType] ([LocationType], [TypeFor]) VALUES (@LocationType, @TypeFor)" OldValuesParameterFormatString="original_{0}" SelectCommand="SELECT * FROM [LocationType]" UpdateCommand="UPDATE [LocationType] SET [LocationType] = @LocationType, [TypeFor] = @TypeFor WHERE [LocationType_Pk] = @original_LocationType_Pk AND (([LocationType] = @original_LocationType) OR ([LocationType] IS NULL AND @original_LocationType IS NULL)) AND (([TypeFor] = @original_TypeFor) OR ([TypeFor] IS NULL AND @original_TypeFor IS NULL))">
                            <DeleteParameters>
                                <asp:Parameter Name="original_LocationType_Pk" Type="Decimal" />
                                <asp:Parameter Name="original_LocationType" Type="String" />
                                <asp:Parameter Name="original_TypeFor" Type="String" />
                            </DeleteParameters>
                            <InsertParameters>
                                <asp:Parameter Name="LocationType" Type="String" />
                                <asp:Parameter Name="TypeFor" Type="String" />
                            </InsertParameters>
                            <UpdateParameters>
                                <asp:Parameter Name="LocationType" Type="String" />
                                <asp:Parameter Name="TypeFor" Type="String" />
                                <asp:Parameter Name="original_LocationType_Pk" Type="Decimal" />
                                <asp:Parameter Name="original_LocationType" Type="String" />
                                <asp:Parameter Name="original_TypeFor" Type="String" />
                            </UpdateParameters>
                        </asp:SqlDataSource>
                    </td>
                </tr>
            </table>
        </asp:View>
        <asp:View ID="View2" runat="server">
            
        </asp:View>
        <asp:View ID="View3" runat="server">






            <table class="FullTable">
                            
                            <tr>
                                <td class="RedHeadding" colspan="8">SEASON</td>
                            </tr>
                                        <tr>
                                            <td class="auto-style9">YEAR</td>
                                            <td class="auto-style9">
                                                <ig:WebDropDown ID="drp_year" runat="server" Width="200px">
                                                    <Items>
                                                        <ig:DropDownItem Selected="False" Text="2016" Value="2016">
                                                        </ig:DropDownItem>
                                                        <ig:DropDownItem Selected="False" Text="2017" Value="2017">
                                                        </ig:DropDownItem>
                                                        <ig:DropDownItem Selected="False" Text="2018" Value="2018">
                                                        </ig:DropDownItem>
                                                        <ig:DropDownItem Selected="False" Text="2019" Value="2019">
                                                        </ig:DropDownItem>
                                                        <ig:DropDownItem Selected="False" Text="2020" Value="2020">
                                                        </ig:DropDownItem>
                                                        <ig:DropDownItem Selected="False" Text="2021" Value="2021">
                                                        </ig:DropDownItem>
                                                        <ig:DropDownItem Selected="False" Text="2022" Value="2022">
                                                        </ig:DropDownItem>
                                                        <ig:DropDownItem Selected="False" Text="2023" Value="2023">
                                                        </ig:DropDownItem>
                                                        <ig:DropDownItem Selected="False" Text="2024" Value="2024">
                                                        </ig:DropDownItem>
                                                        <ig:DropDownItem Selected="False" Text="2025" Value="2025">
                                                        </ig:DropDownItem>
                                                        <ig:DropDownItem Selected="False" Text="2026" Value="2026">
                                                        </ig:DropDownItem>
                                                        <ig:DropDownItem Selected="False" Text="2027" Value="2027">
                                                        </ig:DropDownItem>
                                                    </Items>
                                                </ig:WebDropDown>
                                            </td>
                                            <td class="auto-style9">SEASON</td>
                                            <td class="auto-style9">
                                                <ig:WebDropDown ID="drp_season" runat="server" Width="200px" DataSourceID="SEASONDATA" TextField="SeasonTypeName" ValueField="SeasonType_PK">
                                                    <Items>
                                                        <ig:DropDownItem Selected="False" Text="SPRING" Value="SPRING">
                                                        </ig:DropDownItem>
                                                        <ig:DropDownItem Selected="False" Text="SUMMER" Value="SUMMER">
                                                        </ig:DropDownItem>
                                                        <ig:DropDownItem Selected="False" Text="FALL" Value="FALL">
                                                        </ig:DropDownItem>
                                                        <ig:DropDownItem Selected="False" Text="HOLIDAY" Value="HOLIDAY">
                                                        </ig:DropDownItem>
                                                    </Items>
                                                    <DropDownItemBinding TextField="SeasonTypeName" ValueField="SeasonType_PK" />
                                                </ig:WebDropDown>
                                            </td>
                                            <td class="auto-style9">
                                                <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Save" />
                                            </td>
                                            <td class="auto-style9"></td>
                                            <td class="auto-style12"></td>
                                            <td class="auto-style11"></td>
                                        </tr>
                                        <tr>
                                            <td class="auto-style9" colspan="5">
                                                <ig:WebDataGrid ID="WebDataGrid2" runat="server" AutoGenerateColumns="False" DataSourceID="Seassonmasterdata" Height="350px" Width="400px">
                                                    <Columns>
                                                        <ig:BoundDataField DataFieldName="Season_PK" Key="Season_PK">
                                                            <Header Text="Season_PK">
                                                            </Header>
                                                        </ig:BoundDataField>
                                                        <ig:BoundDataField DataFieldName="SeasonName" Key="SeasonName">
                                                            <Header Text="SeasonName">
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
                                            <td>&nbsp;</td>
                                            <td class="auto-style13">&nbsp;</td>
                                            <td class="auto-style7">&nbsp;</td>
                                        </tr>
                            <tr>
                                <td class="auto-style9" colspan="5">
                                    <asp:SqlDataSource ID="Seassonmasterdata" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT [Season_PK], [SeasonName] FROM [SeasonMaster]"></asp:SqlDataSource>
                                    <asp:SqlDataSource ID="SEASONDATA" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT [SeasonType_PK], [SeasonTypeName] FROM [SeasonType]"></asp:SqlDataSource>
                                </td>
                                <td>&nbsp;</td>
                                <td class="auto-style13">&nbsp;</td>
                                <td class="auto-style7">&nbsp;</td>
                            </tr>
                        </table>





         
        </asp:View>

        <asp:View ID="View4" runat="server">






            <table class="FullTable">
                            
                            <tr>
                                <td class="RedHeadding" colspan="8">SEASON TYPE</td>
                            </tr>
                                        <tr class="DataEntryTable">
                                            <td class="auto-style10">sEASON tYPE</td>
                                            <td class="auto-style9">
                                                
                                            </td>
                                            <td class="auto-style9">&nbsp;</td>
                                            <td class="auto-style9">
                                                &nbsp;</td>
                                            <td class="auto-style9">
                                                &nbsp;</td>
                                            <td class="auto-style9"></td>
                                            <td class="auto-style12"></td>
                                            <td class="auto-style11"></td>
                                        </tr>
                                        <tr>
                                            <td class="auto-style9" colspan="5">
                                                <ig:WebDataGrid ID="WebDataGrid3" runat="server" AutoGenerateColumns="False" DataSourceID="SEASONTYPEDATA" Height="350px" Width="400px">
                                                    <Columns>
                                                        <ig:BoundDataField DataFieldName="SeasonType_PK" Key="SeasonType_PK">
                                                            <Header Text="SeasonType_PK">
                                                            </Header>
                                                        </ig:BoundDataField>
                                                        <ig:BoundDataField DataFieldName="SeasonTypeName" Key="SeasonTypeName">
                                                            <Header Text="SeasonTypeName">
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
                                            <td>&nbsp;</td>
                                            <td class="auto-style13">&nbsp;</td>
                                            <td class="auto-style7">&nbsp;</td>
                                        </tr>
                            <tr>
                                <td class="auto-style9" colspan="5">
                                    <asp:SqlDataSource ID="SEASONTYPEDATA" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT [SeasonType_PK], [SeasonTypeName] FROM [SeasonType]"></asp:SqlDataSource>
                                </td>
                                <td>&nbsp;</td>
                                <td class="auto-style13">&nbsp;</td>
                                <td class="auto-style7">&nbsp;</td>
                            </tr>
                        </table>





         
        </asp:View>

<asp:View ID="View5" runat="server">






            <table class="FullTable">
                            
                            <tr>
                                <td class="RedHeadding" colspan="8">Container</td>
                            </tr>
                                        <tr class="DataEntryTable">
                                            <td class="auto-style10">&nbsp;</td>
                                            <td class="auto-style9">
                                                
                                            </td>
                                            <td class="auto-style9">&nbsp;</td>
                                            <td class="auto-style9">
                                                &nbsp;</td>
                                            <td class="auto-style9">
                                                &nbsp;</td>
                                            <td class="auto-style9"></td>
                                            <td class="auto-style12"></td>
                                            <td class="auto-style11"></td>
                                        </tr>
                                        <tr>
                                            <td class="auto-style9" colspan="5">
                                                <ig:WebDataGrid ID="WebDataGrid4" runat="server" AutoGenerateColumns="False" DataSourceID="containerData" Height="350px" Width="400px">
                                                    <Columns>
                                                        <ig:BoundDataField DataFieldName="Container_PK" Key="Container_PK">
                                                            <Header Text="Container_PK">
                                                            </Header>
                                                        </ig:BoundDataField>
                                                        <ig:BoundDataField DataFieldName="ContainerNumer" Key="ContainerNumer">
                                                            <Header Text="ContainerNumer">
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
                                            <td>&nbsp;</td>
                                            <td class="auto-style13">&nbsp;</td>
                                            <td class="auto-style7">&nbsp;</td>
                                        </tr>
                            <tr>
                                <td class="auto-style9" colspan="5">
                                    <asp:SqlDataSource ID="containerData" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT * FROM [ContainerMaster]" ConflictDetection="CompareAllValues" DeleteCommand="DELETE FROM [ContainerMaster] WHERE [Container_PK] = @original_Container_PK AND (([ContainerNumer] = @original_ContainerNumer) OR ([ContainerNumer] IS NULL AND @original_ContainerNumer IS NULL))" InsertCommand="INSERT INTO [ContainerMaster] ([ContainerNumer],[IsCompleted]) VALUES (@ContainerNumer,'N')" OldValuesParameterFormatString="original_{0}" UpdateCommand="UPDATE [ContainerMaster] SET [ContainerNumer] = @ContainerNumer WHERE [Container_PK] = @original_Container_PK AND (([ContainerNumer] = @original_ContainerNumer) OR ([ContainerNumer] IS NULL AND @original_ContainerNumer IS NULL))">
                                        <DeleteParameters>
                                            <asp:Parameter Name="original_Container_PK" Type="Decimal" />
                                            <asp:Parameter Name="original_ContainerNumer" Type="String" />
                                        </DeleteParameters>
                                        <InsertParameters>
                                            <asp:Parameter Name="ContainerNumer" Type="String" />
                                        </InsertParameters>
                                        <UpdateParameters>
                                            <asp:Parameter Name="ContainerNumer" Type="String" />
                                            <asp:Parameter Name="original_Container_PK" Type="Decimal" />
                                            <asp:Parameter Name="original_ContainerNumer" Type="String" />
                                        </UpdateParameters>
                                    </asp:SqlDataSource>
                                </td>
                                <td>&nbsp;</td>
                                <td class="auto-style13">&nbsp;</td>
                                <td class="auto-style7">&nbsp;</td>
                            </tr>
                        </table>





         
        </asp:View>

    </asp:MultiView>
    <
</asp:Content>

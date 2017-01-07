<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="Masters_GarmentMaster" Codebehind="GarmentMaster.aspx.cs" %>

<%@ Register assembly="Infragistics35.Web.v12.1, Version=12.1.20121.2236, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.LayoutControls" tagprefix="ig" %>
<%@ Register assembly="Infragistics35.Web.v12.1, Version=12.1.20121.2236, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.GridControls" tagprefix="ig1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="height: 10%">
           <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConflictDetection="CompareAllValues" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" DeleteCommand="DELETE FROM [ColorMaster] WHERE [ColorId] = @original_ColorId AND (([ColorCode] = @original_ColorCode) OR ([ColorCode] IS NULL AND @original_ColorCode IS NULL)) AND (([ColorName] = @original_ColorName) OR ([ColorName] IS NULL AND @original_ColorName IS NULL))" InsertCommand="INSERT INTO [ColorMaster] ([ColorCode], [ColorName]) VALUES (@ColorCode, @ColorName)" OldValuesParameterFormatString="original_{0}" SelectCommand="SELECT * FROM [ColorMaster] ORDER BY [ColorCode], [ColorName]" UpdateCommand="UPDATE [ColorMaster] SET [ColorCode] = @ColorCode, [ColorName] = @ColorName WHERE [ColorId] = @original_ColorId AND (([ColorCode] = @original_ColorCode) OR ([ColorCode] IS NULL AND @original_ColorCode IS NULL)) AND (([ColorName] = @original_ColorName) OR ([ColorName] IS NULL AND @original_ColorName IS NULL))">
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
    </div>
    <div style="height: 90%">
         <ig:WebTab ID="WebTab1" runat="server" Height="100%" Width="100%">
    <tabs>
        <ig:ContentTabItem runat="server" Text="Color">
            <Template>
                <ig1:WebDataGrid ID="WebDataGrid3" runat="server" Height="100%" Width="100%" AutoGenerateColumns="False" DataSourceID="SqlDataSource1">
                    <Columns>
                        <ig1:BoundDataField DataFieldName="ColorId" Key="ColorId">
                            <Header Text="ColorId">
                            </Header>
                        </ig1:BoundDataField>
                        <ig1:BoundDataField DataFieldName="ColorCode" Key="ColorCode">
                            <Header Text="ColorCode">
                            </Header>
                        </ig1:BoundDataField>
                        <ig1:BoundDataField DataFieldName="ColorName" Key="ColorName">
                            <Header Text="ColorName">
                            </Header>
                        </ig1:BoundDataField>
                    </Columns>
                    <Behaviors>
                        <ig1:EditingCore>
                            <Behaviors>
                                <ig1:RowAdding Alignment="Top">
                                    <EditModeActions MouseClick="Single" />
                                </ig1:RowAdding>
                                <ig1:CellEditing>
                                </ig1:CellEditing>
                            </Behaviors>
                        </ig1:EditingCore>
                        <ig1:Sorting>
                        </ig1:Sorting>
                        <ig1:Filtering>
                        </ig1:Filtering>
                    </Behaviors>
                </ig1:WebDataGrid>
             
            </Template>
        </ig:ContentTabItem>
        <ig:ContentTabItem runat="server" Text="Category">
            <Template>
                <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConflictDetection="CompareAllValues" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" DeleteCommand="DELETE FROM [GarmentCategory] WHERE [CategoryID] = @original_CategoryID AND (([CategoryName] = @original_CategoryName) OR ([CategoryName] IS NULL AND @original_CategoryName IS NULL))" InsertCommand="INSERT INTO [GarmentCategory] ([CategoryName]) VALUES (@CategoryName)" OldValuesParameterFormatString="original_{0}" SelectCommand="SELECT * FROM [GarmentCategory]" UpdateCommand="UPDATE [GarmentCategory] SET [CategoryName] = @CategoryName WHERE [CategoryID] = @original_CategoryID AND (([CategoryName] = @original_CategoryName) OR ([CategoryName] IS NULL AND @original_CategoryName IS NULL))">
                    <DeleteParameters>
                        <asp:Parameter Name="original_CategoryID" Type="Decimal" />
                        <asp:Parameter Name="original_CategoryName" Type="String" />
                    </DeleteParameters>
                    <InsertParameters>
                        <asp:Parameter Name="CategoryName" Type="String" />
                    </InsertParameters>
                    <UpdateParameters>
                        <asp:Parameter Name="CategoryName" Type="String" />
                        <asp:Parameter Name="original_CategoryID" Type="Decimal" />
                        <asp:Parameter Name="original_CategoryName" Type="String" />
                    </UpdateParameters>
                </asp:SqlDataSource>
                <ig1:WebDataGrid ID="WebDataGrid4" runat="server" DataSourceID="SqlDataSource2" Height="100%" Width="100%">
                    <Behaviors>
                        <ig1:EditingCore>
                            <Behaviors>
                                <ig1:RowAdding>
                                </ig1:RowAdding>
                            </Behaviors>
                        </ig1:EditingCore>
                        <ig1:Sorting>
                        </ig1:Sorting>
                        <ig1:Paging>
                        </ig1:Paging>
                        <ig1:Filtering>
                        </ig1:Filtering>
                    </Behaviors>
                </ig1:WebDataGrid>
            </Template>
        </ig:ContentTabItem>
        <ig:ContentTabItem runat="server" Text="Size">
            <Template>
                <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConflictDetection="CompareAllValues" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" DeleteCommand="DELETE FROM [SizeMaster] WHERE [SizeID] = @original_SizeID AND (([SizeCode] = @original_SizeCode) OR ([SizeCode] IS NULL AND @original_SizeCode IS NULL)) AND (([SizeName] = @original_SizeName) OR ([SizeName] IS NULL AND @original_SizeName IS NULL))" InsertCommand="INSERT INTO [SizeMaster] ([SizeCode], [SizeName]) VALUES (@SizeCode, @SizeName)" OldValuesParameterFormatString="original_{0}" SelectCommand="SELECT * FROM [SizeMaster]" UpdateCommand="UPDATE [SizeMaster] SET [SizeCode] = @SizeCode, [SizeName] = @SizeName WHERE [SizeID] = @original_SizeID AND (([SizeCode] = @original_SizeCode) OR ([SizeCode] IS NULL AND @original_SizeCode IS NULL)) AND (([SizeName] = @original_SizeName) OR ([SizeName] IS NULL AND @original_SizeName IS NULL))">
                    <DeleteParameters>
                        <asp:Parameter Name="original_SizeID" Type="Decimal" />
                        <asp:Parameter Name="original_SizeCode" Type="String" />
                        <asp:Parameter Name="original_SizeName" Type="String" />
                    </DeleteParameters>
                    <InsertParameters>
                        <asp:Parameter Name="SizeCode" Type="String" />
                        <asp:Parameter Name="SizeName" Type="String" />
                    </InsertParameters>
                    <UpdateParameters>
                        <asp:Parameter Name="SizeCode" Type="String" />
                        <asp:Parameter Name="SizeName" Type="String" />
                        <asp:Parameter Name="original_SizeID" Type="Decimal" />
                        <asp:Parameter Name="original_SizeCode" Type="String" />
                        <asp:Parameter Name="original_SizeName" Type="String" />
                    </UpdateParameters>
                </asp:SqlDataSource>
                <ig1:WebDataGrid ID="WebDataGrid5" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource3" Height="350px" Width="400px">
                    <Columns>
                        <ig1:BoundDataField DataFieldName="SizeID" Key="SizeID">
                            <Header Text="SizeID">
                            </Header>
                        </ig1:BoundDataField>
                        <ig1:BoundDataField DataFieldName="SizeCode" Key="SizeCode">
                            <Header Text="SizeCode">
                            </Header>
                        </ig1:BoundDataField>
                        <ig1:BoundDataField DataFieldName="SizeName" Key="SizeName">
                            <Header Text="SizeName">
                            </Header>
                        </ig1:BoundDataField>
                    </Columns>
                    <Behaviors>
                        <ig1:EditingCore>
                            <Behaviors>
                                <ig1:RowAdding>
                                </ig1:RowAdding>
                                <ig1:RowDeleting />
                                <ig1:CellEditing>
                                </ig1:CellEditing>
                            </Behaviors>
                        </ig1:EditingCore>
                        <ig1:Filtering>
                        </ig1:Filtering>
                        <ig1:Paging>
                        </ig1:Paging>
                        <ig1:Sorting>
                        </ig1:Sorting>
                    </Behaviors>
                </ig1:WebDataGrid>
                <br />
            </Template>
        </ig:ContentTabItem>
    </tabs>
</ig:WebTab>
    </div>

   
</asp:Content>


<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="Masters_PaymentMode" Codebehind="PaymentMode.aspx.cs" %>

<%@ Register assembly="Infragistics35.Web.v12.1, Version=12.1.20121.2236, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.LayoutControls" tagprefix="ig" %>

<%@ Register assembly="Infragistics35.Web.v12.1, Version=12.1.20121.2236, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.GridControls" tagprefix="ig" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .auto-style1 {
            width: 100%;
            height: 338px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  
    <div style="height: 348px">
        <table class="auto-style1">
            <tr>
                <td></td>
            </tr>
            <tr>
                <td>
    
    
    
    
    
    
    
    
      <ig:WebTab ID="WebTab1" runat="server" Height="95%" Width="100%" SelectedIndex="1">
        <tabs>
            <ig:ContentTabItem runat="server" Text="Payment Mode">
                <Template>

                    <div>
                        <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT * FROM [PaymentModeMaster]" ConflictDetection="CompareAllValues" DeleteCommand="DELETE FROM [PaymentModeMaster] WHERE [PaymentModeID] = @original_PaymentModeID AND (([PaymentModeCode] = @original_PaymentModeCode) OR ([PaymentModeCode] IS NULL AND @original_PaymentModeCode IS NULL)) AND (([PaymentModeDescription] = @original_PaymentModeDescription) OR ([PaymentModeDescription] IS NULL AND @original_PaymentModeDescription IS NULL))" InsertCommand="INSERT INTO [PaymentModeMaster] ([PaymentModeCode], [PaymentModeDescription]) VALUES (@PaymentModeCode, @PaymentModeDescription)" OldValuesParameterFormatString="original_{0}" UpdateCommand="UPDATE [PaymentModeMaster] SET [PaymentModeCode] = @PaymentModeCode, [PaymentModeDescription] = @PaymentModeDescription WHERE [PaymentModeID] = @original_PaymentModeID AND (([PaymentModeCode] = @original_PaymentModeCode) OR ([PaymentModeCode] IS NULL AND @original_PaymentModeCode IS NULL)) AND (([PaymentModeDescription] = @original_PaymentModeDescription) OR ([PaymentModeDescription] IS NULL AND @original_PaymentModeDescription IS NULL))">
                            <DeleteParameters>
                                <asp:Parameter Name="original_PaymentModeID" Type="Decimal" />
                                <asp:Parameter Name="original_PaymentModeCode" Type="String" />
                                <asp:Parameter Name="original_PaymentModeDescription" Type="String" />
                            </DeleteParameters>
                            <InsertParameters>
                                <asp:Parameter Name="PaymentModeCode" Type="String" />
                                <asp:Parameter Name="PaymentModeDescription" Type="String" />
                            </InsertParameters>
                            <UpdateParameters>
                                <asp:Parameter Name="PaymentModeCode" Type="String" />
                                <asp:Parameter Name="PaymentModeDescription" Type="String" />
                                <asp:Parameter Name="original_PaymentModeID" Type="Decimal" />
                                <asp:Parameter Name="original_PaymentModeCode" Type="String" />
                                <asp:Parameter Name="original_PaymentModeDescription" Type="String" />
                            </UpdateParameters>
                        </asp:SqlDataSource>
                    </div>

                    <div>  <ig:WebDataGrid ID="WebDataGrid1" runat="server" Height="350px" Width="100%" AutoGenerateColumns="False" DataSourceID="SqlDataSource2" >
                        <Columns>
                            <ig:BoundDataField DataFieldName="PaymentModeID" Key="PaymentModeID">
                                <Header Text="PaymentModeID">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="PaymentModeCode" Key="PaymentModeCode">
                                <Header Text="PaymentModeCode">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="PaymentModeDescription" Key="PaymentModeDescription">
                                <Header Text="PaymentModeDescription">
                                </Header>
                            </ig:BoundDataField>
                        </Columns>
                        <Behaviors>
                            <ig:EditingCore>
                                <Behaviors>
                                    <ig:RowAdding>
                                    </ig:RowAdding>
                                    <ig:CellEditing>
                                    </ig:CellEditing>
                                    <ig:RowDeleting />
                                    <ig:RowEditingTemplate>
                                    </ig:RowEditingTemplate>
                                </Behaviors>
                            </ig:EditingCore>
                            <ig:Selection CellClickAction="Row" RowSelectType="Single">
                            </ig:Selection>
                            <ig:RowSelectors>
                            </ig:RowSelectors>
                        </Behaviors>
                    </ig:WebDataGrid></div>
                  
                    
                    
                    <br />
                </Template>
            </ig:ContentTabItem>
            <ig:ContentTabItem runat="server" Text="Payment Term">
                <Template>
                    <div>
                        <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConflictDetection="CompareAllValues" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" DeleteCommand="DELETE FROM [PaymentTermMaster] WHERE [PaymentTermID] = @original_PaymentTermID AND (([PaymentTermCode] = @original_PaymentTermCode) OR ([PaymentTermCode] IS NULL AND @original_PaymentTermCode IS NULL)) AND (([PaymentCodeDescription] = @original_PaymentCodeDescription) OR ([PaymentCodeDescription] IS NULL AND @original_PaymentCodeDescription IS NULL))" InsertCommand="INSERT INTO [PaymentTermMaster] ([PaymentTermCode], [PaymentCodeDescription]) VALUES (@PaymentTermCode, @PaymentCodeDescription)" OldValuesParameterFormatString="original_{0}" SelectCommand="SELECT * FROM [PaymentTermMaster]" UpdateCommand="UPDATE [PaymentTermMaster] SET [PaymentTermCode] = @PaymentTermCode, [PaymentCodeDescription] = @PaymentCodeDescription WHERE [PaymentTermID] = @original_PaymentTermID AND (([PaymentTermCode] = @original_PaymentTermCode) OR ([PaymentTermCode] IS NULL AND @original_PaymentTermCode IS NULL)) AND (([PaymentCodeDescription] = @original_PaymentCodeDescription) OR ([PaymentCodeDescription] IS NULL AND @original_PaymentCodeDescription IS NULL))">
                            <DeleteParameters>
                                <asp:Parameter Name="original_PaymentTermID" Type="Decimal" />
                                <asp:Parameter Name="original_PaymentTermCode" Type="String" />
                                <asp:Parameter Name="original_PaymentCodeDescription" Type="String" />
                            </DeleteParameters>
                            <InsertParameters>
                                <asp:Parameter Name="PaymentTermCode" Type="String" />
                                <asp:Parameter Name="PaymentCodeDescription" Type="String" />
                            </InsertParameters>
                            <UpdateParameters>
                                <asp:Parameter Name="PaymentTermCode" Type="String" />
                                <asp:Parameter Name="PaymentCodeDescription" Type="String" />
                                <asp:Parameter Name="original_PaymentTermID" Type="Decimal" />
                                <asp:Parameter Name="original_PaymentTermCode" Type="String" />
                                <asp:Parameter Name="original_PaymentCodeDescription" Type="String" />
                            </UpdateParameters>
                        </asp:SqlDataSource>
                        <ig:WebDataGrid ID="WebDataGrid2" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource3" Height="350px" Width="95%">
                            <Columns>
                                <ig:BoundDataField DataFieldName="PaymentTermID" Key="PaymentTermID">
                                    <Header Text="PaymentTermID">
                                    </Header>
                                </ig:BoundDataField>
                                <ig:BoundDataField DataFieldName="PaymentTermCode" Key="PaymentTermCode">
                                    <Header Text="PaymentTermCode">
                                    </Header>
                                </ig:BoundDataField>
                                <ig:BoundDataField DataFieldName="PaymentCodeDescription" Key="PaymentCodeDescription">
                                    <Header Text="PaymentCodeDescription">
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
                                        <ig:RowDeleting />
                                    </Behaviors>
                                </ig:EditingCore>
                                <ig:Selection CellClickAction="Row" RowSelectType="Single">
                                </ig:Selection>
                                <ig:RowSelectors>
                                </ig:RowSelectors>
                                <ig:Filtering>
                                </ig:Filtering>
                                <ig:Paging>
                                </ig:Paging>
                            </Behaviors>
                        </ig:WebDataGrid>
                    </div>
                </Template>
            </ig:ContentTabItem>
        </tabs>
    </ig:WebTab>
                </td>
            </tr>
        </table>

    </div>
    
    
    
    
    
    
    
    
      </asp:Content>


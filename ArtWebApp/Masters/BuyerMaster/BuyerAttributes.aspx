<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="BuyerAttributes.aspx.cs" Inherits="ArtWebApp.Masters.BuyerAttributes" %>
<%@ Register assembly="Infragistics35.Web.v12.1, Version=12.1.20121.2236, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.GridControls" tagprefix="ig" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
      
    </style>
    <link href="../../css/style.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>

        <asp:MultiView ID="MultiView1" runat="server">
            <asp:View ID="View1" runat="server">
                <table class="FullTable">
                    <tr>
                        <td class="RedHeadding"><strong>&nbsp; &nbsp;Channels</strong></td>
                    </tr>
                    <tr class="gridtable">
                        <td>
                            <ig:WebDataGrid ID="WebDataGrid1" runat="server" WIDTH="100%" AutoGenerateColumns="False" DataKeyFields="ChannelID" DataSourceID="ChannelData" >
                                <Columns>
                                    <ig:BoundDataField DataFieldName="ChannelID" Key="ChannelID">
                                        <Header Text="ChannelID">
                                        </Header>
                                    </ig:BoundDataField>
                                    <ig:BoundDataField DataFieldName="ChannelName" Key="ChannelName">
                                        <Header Text="ChannelName">
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
                            <asp:SqlDataSource ID="ChannelData" runat="server" ConflictDetection="CompareAllValues" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" DeleteCommand="DELETE FROM [ChannelMaster] WHERE [ChannelID] = @original_ChannelID AND (([ChannelName] = @original_ChannelName) OR ([ChannelName] IS NULL AND @original_ChannelName IS NULL))" InsertCommand="INSERT INTO [ChannelMaster] ([ChannelName]) VALUES (@ChannelName)" OldValuesParameterFormatString="original_{0}" SelectCommand="SELECT * FROM [ChannelMaster]" UpdateCommand="UPDATE [ChannelMaster] SET [ChannelName] = @ChannelName WHERE [ChannelID] = @original_ChannelID AND (([ChannelName] = @original_ChannelName) OR ([ChannelName] IS NULL AND @original_ChannelName IS NULL))">
                                <DeleteParameters>
                                    <asp:Parameter Name="original_ChannelID" Type="Decimal" />
                                    <asp:Parameter Name="original_ChannelName" Type="Object" />
                                </DeleteParameters>
                                <InsertParameters>
                                    <asp:Parameter Name="ChannelName" Type="Object" />
                                </InsertParameters>
                                <UpdateParameters>
                                    <asp:Parameter Name="ChannelName" Type="Object" />
                                    <asp:Parameter Name="original_ChannelID" Type="Decimal" />
                                    <asp:Parameter Name="original_ChannelName" Type="Object" />
                                </UpdateParameters>
                            </asp:SqlDataSource>
                        </td>
                    </tr>
                </table>
            </asp:View>

             <asp:View ID="View2" runat="server">
                 <table class="FullTable">
                    <tr>
                        <td class="RedHeadding">&nbsp;&nbsp;&nbsp;&nbsp; <strong>Buyer Destination&nbsp;</strong></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:FormView ID="FormView1" runat="server" DataKeyNames="BuyerDestination_PK" DataSourceID="Destinationdata" BackColor="LightGoldenrodYellow" BorderColor="Tan" BorderWidth="1px" CellPadding="2" ForeColor="Black">
                                <EditRowStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
                                <FooterStyle BackColor="Tan" />
                                <HeaderStyle BackColor="Tan" Font-Bold="True" />
                                <InsertItemTemplate>
                                    BuyerDestination:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:TextBox ID="BuyerDestinationTextBox" runat="server" Text='<%# Bind("BuyerDestination") %>' />
                                    <br />
                                    <br />
                                    BuyerID:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:DropDownList ID="DropDownList1" runat="server" DataSourceID="Buyerdata" DataTextField="BuyerName" DataValueField="BuyerID" SelectedValue='<%# Bind("BuyerID", "{0:N}") %>'>
                                    </asp:DropDownList>
                                    <br />
                                    <br />
                                    <asp:LinkButton ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert" Text="Insert" />
                                    &nbsp;<asp:LinkButton ID="InsertCancelButton" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel" />
                                </InsertItemTemplate>
                                <ItemTemplate>
                                    &nbsp;<asp:LinkButton ID="NewButton" runat="server" CausesValidation="False" CommandName="New" Text="New" />
                                </ItemTemplate>
                                <PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" HorizontalAlign="Center" />
                            </asp:FormView>
                        </td>
                    </tr>
                     <tr class="gridtable" >
                         <td>
                             <ig:WebDataGrid ID="WebDataGrid2" runat="server" AutoGenerateColumns="False" DataSourceID="Destinationdata" >
                                 <Columns>
                                     <ig:BoundDataField DataFieldName="BuyerDestination_PK" Key="BuyerDestination_PK">
                                         <Header Text="BuyerDestination_PK">
                                         </Header>
                                     </ig:BoundDataField>
                                     <ig:BoundDataField DataFieldName="BuyerName" Key="BuyerName">
                                         <Header Text="BuyerName">
                                         </Header>
                                     </ig:BoundDataField>
                                     <ig:BoundDataField DataFieldName="BuyerDestination" Key="BuyerDestination">
                                         <Header Text="BuyerDestination">
                                         </Header>
                                     </ig:BoundDataField>
                                 </Columns>
                                 <Behaviors>
                                     <ig:EditingCore>
                                         <Behaviors>
                                             <ig:CellEditing>
                                             </ig:CellEditing>
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
                             <asp:SqlDataSource ID="Destinationdata" runat="server" ConflictDetection="CompareAllValues" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" DeleteCommand="DELETE FROM [BuyerDestinationMaster] WHERE [BuyerDestination_PK] = @original_BuyerDestination_PK AND (([BuyerDestination] = @original_BuyerDestination) OR ([BuyerDestination] IS NULL AND @original_BuyerDestination IS NULL)) AND (([BuyerID] = @original_BuyerID) OR ([BuyerID] IS NULL AND @original_BuyerID IS NULL))" InsertCommand="INSERT INTO [BuyerDestinationMaster] ([BuyerDestination], [BuyerID]) VALUES (@BuyerDestination, @BuyerID)" OldValuesParameterFormatString="original_{0}" SelectCommand="SELECT BuyerDestinationMaster.BuyerDestination_PK, BuyerMaster.BuyerName, BuyerDestinationMaster.BuyerDestination FROM BuyerDestinationMaster INNER JOIN BuyerMaster ON BuyerDestinationMaster.BuyerID = BuyerMaster.BuyerID ORDER BY BuyerDestinationMaster.BuyerDestination, BuyerDestinationMaster.BuyerID" UpdateCommand="UPDATE [BuyerDestinationMaster] SET [BuyerDestination] = @BuyerDestination, [BuyerID] = @BuyerID WHERE [BuyerDestination_PK] = @original_BuyerDestination_PK AND (([BuyerDestination] = @original_BuyerDestination) OR ([BuyerDestination] IS NULL AND @original_BuyerDestination IS NULL)) AND (([BuyerID] = @original_BuyerID) OR ([BuyerID] IS NULL AND @original_BuyerID IS NULL))">
                                 <DeleteParameters>
                                     <asp:Parameter Name="original_BuyerDestination_PK" Type="Decimal" />
                                     <asp:Parameter Name="original_BuyerDestination" Type="String" />
                                     <asp:Parameter Name="original_BuyerID" Type="Decimal" />
                                 </DeleteParameters>
                                 <InsertParameters>
                                     <asp:Parameter Name="BuyerDestination" Type="String" />
                                     <asp:Parameter Name="BuyerID" Type="Decimal" />
                                 </InsertParameters>
                                 <UpdateParameters>
                                     <asp:Parameter Name="BuyerDestination" Type="String" />
                                     <asp:Parameter Name="BuyerID" Type="Decimal" />
                                     <asp:Parameter Name="original_BuyerDestination_PK" Type="Decimal" />
                                     <asp:Parameter Name="original_BuyerDestination" Type="String" />
                                     <asp:Parameter Name="original_BuyerID" Type="Decimal" />
                                 </UpdateParameters>
                             </asp:SqlDataSource>
                         </td>
                     </tr>
                </table>
            </asp:View>

             <asp:View ID="View3" runat="server">
                 <table class="FullTable">
                    <tr>
                        <td class="RedHeadding">Buyer Style</td>
                    </tr>

                      <tr>
                        <td><asp:FormView ID="FormView3" runat="server" DataKeyNames="BuyerStyleID" DataSourceID="BuyerStyle1">
                                
                                
                                <InsertItemTemplate>
                                    BuyerStyle:
                                    <asp:TextBox ID="BuyerStyleTextBox" runat="server" Text='<%# Bind("BuyerStyle") %>' />
                                    <br />
                                    ArtBuyerID:
                                   BuyerID:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:DropDownList ID="DropDownList1" runat="server" DataSourceID="Buyerdata" DataTextField="BuyerName" DataValueField="BuyerID" SelectedValue='<%# Bind("BuyerID", "{0:N}") %>'>
                                    </asp:DropDownList>
                                    <br />
                                    <asp:LinkButton ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert" Text="Insert" />
                                    &nbsp;<asp:LinkButton ID="InsertCancelButton" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel" />
                                </InsertItemTemplate>
                                <ItemTemplate>
                                 
                                    &nbsp;<asp:LinkButton ID="NewButton" runat="server" CausesValidation="False" CommandName="New" Text="New" />
                                </ItemTemplate>
                            </asp:FormView></td>
                    </tr>

                    <tr class="gridtable">
                        <td>
                            <ig:WebDataGrid ID="WebDataGrid3" runat="server" AutoGenerateColumns="False" DataSourceID="BuyerStyle1" >
                                <Columns>
                                    <ig:BoundDataField DataFieldName="BuyerStyleID" Key="BuyerStyleID">
                                        <Header Text="BuyerStyleID">
                                        </Header>
                                    </ig:BoundDataField>
                                    <ig:BoundDataField DataFieldName="BuyerStyle" Key="BuyerStyle">
                                        <Header Text="BuyerStyle">
                                        </Header>
                                    </ig:BoundDataField>
                                    <ig:BoundDataField DataFieldName="BuyerID" Key="BuyerID">
                                        <Header Text="BuyerID">
                                        </Header>
                                    </ig:BoundDataField>
                                </Columns>
                                <Behaviors>
                                    <ig:EditingCore>
                                        <Behaviors>
                                            <ig:CellEditing>
                                            </ig:CellEditing>
                                        </Behaviors>
                                    </ig:EditingCore>
                                    <ig:Filtering>
                                    </ig:Filtering>
                                </Behaviors>
                            </ig:WebDataGrid>
                        </td>
                    </tr>
                     <tr>
                         <td>&nbsp;</td>
                     </tr>
                </table>
            </asp:View>
             <asp:View ID="View4" runat="server">
                 <table class="FullTable">
                    <tr>
                        <td class="RedHeadding">Buyer PO</td>
                    </tr>

                      <tr>
                        <td><asp:FormView ID="FormView2" runat="server" DataKeyNames="BuyerPO_PK" DataSourceID="BuyerPOData">
                                
                                
                                <InsertItemTemplate>
                                    BuyerPO:
                                    <asp:TextBox ID="BuyerPOTextBox" runat="server" Text='<%# Bind("BuyerPO") %>' />
                                    <br />
                                    <br />
                                    Buyer:
                                    <asp:DropDownList ID="DropDownList1" runat="server" DataSourceID="Buyerdata" DataTextField="BuyerName" DataValueField="BuyerID" SelectedValue='<%# Bind("BuyerID", "{0:N}") %>'>
                                    </asp:DropDownList>
                                    <br />
                                    <br />
                                    <asp:LinkButton ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert" Text="Insert" />
&nbsp;<asp:LinkButton ID="InsertCancelButton" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel" />
                                </InsertItemTemplate>
                                <ItemTemplate>
                                    &nbsp;<asp:LinkButton ID="NewButton" runat="server" CausesValidation="False" CommandName="New" Text="New" />
                                </ItemTemplate>
                            </asp:FormView></td>
                    </tr>

                    <tr class="gridtable">
                        <td>
                            <ig:WebDataGrid ID="WebDataGrid4" runat="server" AutoGenerateColumns="False" DataSourceID="BuyerPOData" >
                                <Columns>
                                    <ig:BoundDataField DataFieldName="BuyerPO_PK" Key="BuyerPO_PK">
                                        <Header Text="BuyerPO_PK">
                                        </Header>
                                    </ig:BoundDataField>
                                    <ig:BoundDataField DataFieldName="BuyerName" Key="BuyerName">
                                        <Header Text="BuyerName">
                                        </Header>
                                    </ig:BoundDataField>
                                    <ig:BoundDataField DataFieldName="BuyerPO" Key="BuyerPO">
                                        <Header Text="BuyerPO">
                                        </Header>
                                    </ig:BoundDataField>
                                </Columns>
                                <Behaviors>
                                    <ig:EditingCore>
                                        <Behaviors>
                                            <ig:CellEditing>
                                            </ig:CellEditing>
                                        </Behaviors>
                                    </ig:EditingCore>
                                    <ig:Filtering>
                                    </ig:Filtering>
                                </Behaviors>
                            </ig:WebDataGrid>
                        </td>
                    </tr>
                     <tr>
                         <td>&nbsp;</td>
                     </tr>
                </table>
            </asp:View>
             <asp:View ID="View5" runat="server">
                 <asp:SqlDataSource ID="Buyerdata" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT [BuyerID], [BuyerName] FROM [BuyerMaster]"></asp:SqlDataSource>
                 <asp:SqlDataSource ID="BuyerStyle1" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" 
                     SelectCommand="SELECT BuyerStyleID, BuyerStyle, BuyerID FROM BuyerStyleMaster" DeleteCommand="DELETE FROM [BuyerStyleMaster] WHERE [BuyerStyleID] = @original_BuyerStyleID" 
                     InsertCommand="INSERT INTO [BuyerStyleMaster] ([BuyerStyle], [BuyerID]) VALUES (@BuyerStyle, @BuyerID)" OldValuesParameterFormatString="original_{0}" 
                     UpdateCommand="UPDATE [BuyerStyleMaster] SET [BuyerStyle] = @BuyerStyle, [BuyerID] = @BuyerID WHERE [BuyerStyleID] = @original_BuyerStyleID">
                     
                     <InsertParameters>
                         <asp:Parameter Name="BuyerStyle" Type="String" />
                         <asp:Parameter Name="BuyerID" Type="Decimal" />
                     </InsertParameters>
                     
                 </asp:SqlDataSource>
                 <asp:SqlDataSource ID="BuyerPOData" runat="server" ConflictDetection="CompareAllValues" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" DeleteCommand="DELETE FROM [BuyerPOMaster] WHERE [BuyerPO_PK] = @original_BuyerPO_PK AND (([BuyerPO] = @original_BuyerPO) OR ([BuyerPO] IS NULL AND @original_BuyerPO IS NULL)) AND (([BuyerID] = @original_BuyerID) OR ([BuyerID] IS NULL AND @original_BuyerID IS NULL))" InsertCommand="INSERT INTO BuyerPOMaster(BuyerPO, BuyerID) VALUES (@BuyerPO, @BuyerID)" OldValuesParameterFormatString="original_{0}" SelectCommand="SELECT BuyerPOMaster.BuyerPO_PK, BuyerPOMaster.BuyerPO, BuyerPOMaster.BuyerID, BuyerMaster.BuyerName FROM BuyerPOMaster INNER JOIN BuyerMaster ON BuyerPOMaster.BuyerID = BuyerMaster.BuyerID" UpdateCommand="UPDATE [BuyerPOMaster] SET [BuyerPO] = @BuyerPO, [BuyerID] = @BuyerID WHERE [BuyerPO_PK] = @original_BuyerPO_PK AND (([BuyerPO] = @original_BuyerPO) OR ([BuyerPO] IS NULL AND @original_BuyerPO IS NULL)) AND (([BuyerID] = @original_BuyerID) OR ([BuyerID] IS NULL AND @original_BuyerID IS NULL))">
                     <DeleteParameters>
                         <asp:Parameter Name="original_BuyerPO_PK" Type="Decimal" />
                         <asp:Parameter Name="original_BuyerPO" Type="String" />
                         <asp:Parameter Name="original_BuyerID" Type="Decimal" />
                     </DeleteParameters>
                     <InsertParameters>
                         <asp:Parameter Name="BuyerPO" Type="String" />
                         <asp:Parameter Name="BuyerID" Type="Decimal" />
                     </InsertParameters>
                     <UpdateParameters>
                         <asp:Parameter Name="BuyerPO" Type="String" />
                         <asp:Parameter Name="BuyerID" Type="Decimal" />
                         <asp:Parameter Name="original_BuyerPO_PK" Type="Decimal" />
                         <asp:Parameter Name="original_BuyerPO" Type="String" />
                         <asp:Parameter Name="original_BuyerID" Type="Decimal" />
                     </UpdateParameters>
                 </asp:SqlDataSource>
                 <br />
            </asp:View>

        </asp:MultiView>

    </div>
</asp:Content>

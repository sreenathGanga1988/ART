<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="ShipmentClosing.aspx.cs" Inherits="ArtWebApp.Shipping.ShipmentClosing" %>
<%@ Register assembly="Infragistics35.Web.v12.1, Version=12.1.20121.2236, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.ListControls" tagprefix="ig"  %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../css/style.css" rel="stylesheet" />
   
    <script src="../JQuery/GridJQuery.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>


     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
         <ContentTemplate>

             <table class="DataEntryTable">
        <tr class="RedHeadding">
            <td class="NormalTD" colspan="4">Close Shipment Handover</td>
        </tr>
                 <tr>
                     <td class="NormalTD">Factory</td>
                     <td class="NormalTD">
                         <ucc:DropDownListChosen ID="drp_factory" runat="server" DataSourceID="FactorydataSource" DataTextField="LocationName" DataValueField="Location_PK" DisableSearchThreshold="10" Width="200px">
                         </ucc:DropDownListChosen>
                     </td>
                     </td>
                     <td class="auto-style7">
                         <asp:Button ID="btn_showPO" runat="server" OnClick="btn_showPO_Click" Text="S" />
                     </td>
                     <td class="auto-style7"></td>
                 </tr>
      
                 <tr>
                     <td class="NormalTD"><asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                            <ContentTemplate>
                                                <asp:CheckBox ID="chk_selectAll" runat="server" AutoPostBack="True" OnCheckedChanged="chk_selectAll_CheckedChanged" Text="Select All" />
                                            </ContentTemplate>
                                        </asp:UpdatePanel></td>
                     <td class="NormalTD">&nbsp;</td>
                     <td class="auto-style7">&nbsp;</td>
                     <td class="auto-style7">&nbsp;</td>
                 </tr>
      
    </table>
         </ContentTemplate>
     </asp:UpdatePanel>


 </div>
    <div class="gridtable">

         <asp:UpdatePanel ID="upd_grid" UpdateMode="Conditional" runat="server">
         <ContentTemplate>

        <asp:GridView ID="tbl_podetails" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4" style="font-size: x-small; font-family: Calibri" Width="100%" Font-Size="Large" DataKeyNames="ShipmentHandMaster_PK">
                            <Columns> 
                                <asp:TemplateField>  
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="checkAll" runat ="server" onclick="checkAll(this)"/>
                                    </HeaderTemplate>                                 
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chk_select" runat="server" onclick="Check_Click(this)"/>
                                    </ItemTemplate>
                                </asp:TemplateField>     
                                <asp:TemplateField HeaderText="ShipmentHandMaster_PK" InsertVisible="False" SortExpression="ShipmentHandMaster_PK">
                                    
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_shipemtnDandover" runat="server" Text='<%# Bind("ShipmentHandMaster_PK") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="ShipmentHandOverCode" HeaderText="ShipmentHandOverCode" SortExpression="ShipmentHandOverCode" />
            </Columns>
            <FooterStyle BackColor="#FFFFCC" ForeColor="#000066" />
                            <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="#FFFFCC" />
                            <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
                            <RowStyle BackColor="White" ForeColor="#330099" />
                            <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Black" />
                            <SortedAscendingCellStyle BackColor="#FEFCEB" />
                            <SortedAscendingHeaderStyle BackColor="#AF0101" />
                            <SortedDescendingCellStyle BackColor="#F6F0C0" />
                            <SortedDescendingHeaderStyle BackColor="#7E0000" />
                        </asp:GridView>
              </ContentTemplate>
     </asp:UpdatePanel>

    </div>


    <div class="DataEntryTable">


        <asp:Button ID="btn_Submit" runat="server" Text="Submit" OnClick="btn_JCSubmit_Click" style="height: 26px" />


    </div>

       <div id="Messaediv" runat="server">
                 


                           <asp:Label ID="lbl_msg" runat="server" Text="*"></asp:Label>


                     
               </div>
    <asp:SqlDataSource ID="FactorydataSource" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT [Location_PK], [LocationName] FROM [LocationMaster] WHERE ([LocType] = @LocType)">
                    <SelectParameters>
                        <asp:Parameter DefaultValue="F" Name="LocType" Type="String" />
                    </SelectParameters>
                </asp:SqlDataSource>


                <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT [ShipmentHandMaster_PK], [ShipmentHandOverCode] FROM [ShipmentHandOverMaster]"></asp:SqlDataSource>


                </asp:Content>

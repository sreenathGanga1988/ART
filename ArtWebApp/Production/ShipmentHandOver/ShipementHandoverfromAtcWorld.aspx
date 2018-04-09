<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="ShipementHandoverfromAtcWorld.aspx.cs" Inherits="ArtWebApp.Production.ShipmentHandOver.ShipementHandoverfromAtcWorld" %>
<%@ Register assembly="Infragistics35.Web.v12.1, Version=12.1.20121.2236, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.ListControls" tagprefix="ig" %>
<%@ Register assembly="Infragistics35.Web.v12.1, Version=12.1.20121.2236, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.EditorControls" tagprefix="ig" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
    <style type="text/css">
     
    </style>
    
    <link href="../../css/style.css" rel="stylesheet" />
    <script src="../../JQuery/Validator.js"></script>

    <script src="../../JQuery/GridJQuery.js"></script>
    <script lang="javascript" type="text/javascript">
      
     
        </script>

    
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>


     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
         <ContentTemplate>

             <table class="DataEntryTable">
        <tr class="RedHeadding">
            <td class="NormalTD" colspan="4"> Shipment Handover against AtcWorld Shipment DO</td>
        </tr>
        <tr>
            <td class="NormalTD">Factory</td>
             <td class="NormalTD">
                 <ucc:DropDownListChosen ID="drp_factory" runat="server" DataSourceID="FactorydataSource" DataTextField="LocationName" DataValueField="Location_PK" DisableSearchThreshold="10" Width="200px">
                 </ucc:DropDownListChosen>
            </td>
             <td class="NormalTD">
                 <asp:Button ID="Btn_showJC" runat="server" Text="S" OnClick="Btn_showJC_Click" />
            </td>
             <td class="NormalTD"></td>
        </tr>
        <tr>
             <td class="NormalTD">
                 Atc World SDO</td>
            <td class="NormalTD">
                <ig:WebDropDown ID="drp_SDO" runat="server" EnableMultipleSelection="True" EnableClosingDropDownOnSelect="False" TextField="Name" ValueField="pk" Width="200px">
                    <DropDownItemBinding TextField="Name" ValueField="Name" />
                </ig:WebDropDown>
            </td>
             <td class="NormalTD">
                 <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="S" />
            </td>
             <td class="NormalTD">
            </td>
        </tr>
                 <tr>
                     <td class="NormalTD">Shipment date</td>
                     <td class="NormalTD">
                        

                         <ig:WebDatePicker ID="shipdate" runat="server">
                         </ig:WebDatePicker>
                        

                     </td>
                     <td class="NormalTD">&nbsp;</td>
                     <td class="NormalTD">&nbsp;</td>
                 </tr>
                 <tr>
                     <td class="NormalTD">&nbsp;</td>
                     <td class="NormalTD">&nbsp;</td>
                     <td class="NormalTD">&nbsp;</td>
                     <td class="NormalTD">&nbsp;</td>
                 </tr>
    </table>
         </ContentTemplate>
     </asp:UpdatePanel>


 </div>

     <div class="gridtable">

         <asp:UpdatePanel ID="UpdatePanel2" runat="server">
         <ContentTemplate>

        <asp:GridView ID="tbl_podetails" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4" style="font-size: x-small; font-family: Calibri" Width="100%" Font-Size="Large" ShowFooter="True" DataKeyNames="OurStyleID" OnRowDataBound="tbl_podetails_RowDataBound">
                            <Columns>
                                <asp:TemplateField HeaderText="OurStyleID" InsertVisible="False" SortExpression="OurStyleID">
                                  
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_OurStyleID" runat="server" Text='<%# Bind("OurStyleID") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="POPackId" SortExpression="POPackId">
                                   
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_POPackId" runat="server" Text='<%# Bind("POPackId") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="AtcNum" HeaderText="AtcNum" SortExpression="AtcNum" />
                                <asp:BoundField DataField="OurStyle" HeaderText="OurStyle" SortExpression="OurStyle" />
                                <asp:BoundField DataField="BuyerStyle" HeaderText="BuyerStyle" SortExpression="BuyerStyle" />
                                <asp:BoundField DataField="PoPacknum" HeaderText="PoPacknum" SortExpression="PoPacknum" />
                                <asp:BoundField DataField="BuyerPO" HeaderText="BuyerPO" SortExpression="BuyerPO" />
                                  <asp:BoundField DataField="LocationName" HeaderText="Produced In" SortExpression="LocationName" />
                                <asp:TemplateField HeaderText="SDONo" SortExpression="SDONo">
                                  
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_SDONo" runat="server" Text='<%# Bind("SDONo") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ShipQty" SortExpression="ShipQty">
                                   
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_qty" runat="server" Text='<%# Bind("ShipQty") %>'></asp:Label>
                                    </ItemTemplate>
                                        <FooterTemplate>
                                <asp:Label runat="server" ID="lblTotalValue" ></asp:Label>
                            </FooterTemplate>
                                </asp:TemplateField>

                                 <asp:TemplateField HeaderText="lctn_PK" SortExpression="lctn_PK">
                                   
                                    <ItemTemplate>
                                        <asp:Label ID="lblProductionArtLocation_PK" runat="server" Text='<%# Bind("ProductionArtLocation") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                  <asp:TemplateField HeaderText="ShipmentDate" SortExpression="ShipmentDate">
                                   
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_ShipmentDate" runat="server" Text='<%# Bind("ShipmentDate") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="CMPerPc" SortExpression="CMPerPc">
                                   
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_CMPerPc" runat="server" Text='<%# Bind("CMPerPc") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                               

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
              <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT       AtcDetails.OurStyleID, POPackDetails.POPackId,  AtcMaster.AtcNum, AtcDetails.OurStyle, AtcDetails.BuyerStyle, PoPackMaster.PoPacknum, PoPackMaster.BuyerPO, ATCWorldToArtShipData.SDONo, SUM(ATCWorldToArtShipData.ShipQty) AS ShipQty 
                        
FROM            POPackDetails INNER JOIN
                         PoPackMaster ON POPackDetails.POPackId = PoPackMaster.PoPackId INNER JOIN
                         ATCWorldToArtShipData ON POPackDetails.PoPack_Detail_PK = ATCWorldToArtShipData.PoPack_Detail_PK INNER JOIN
                         AtcDetails ON POPackDetails.OurStyleID = AtcDetails.OurStyleID INNER JOIN
                         AtcMaster ON AtcDetails.AtcId = AtcMaster.AtcId
						 where ATCWorldToArtShipData.SDONo='SDO-000000054'
GROUP BY AtcMaster.AtcNum, AtcDetails.OurStyle, AtcDetails.BuyerStyle, PoPackMaster.PoPacknum, PoPackMaster.BuyerPO, ATCWorldToArtShipData.SDONo, AtcDetails.OurStyleID, POPackDetails.POPackId"></asp:SqlDataSource>
              </ContentTemplate>
     </asp:UpdatePanel>

    </div>

    <div>



        <asp:Button ID="btn_submitShipment" runat="server" Text="Submit" OnClientClick="return CheckBoxSelectionValidation()" OnClick="btn_submitShipment_Click" />



    </div>
    <div id="Messaediv" runat="server">
                 


                           <asp:Label ID="lbl_msg" runat="server" Text="*"></asp:Label>


                     
               </div>
    <asp:SqlDataSource ID="FactorydataSource" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT [Location_PK], [LocationName] FROM [LocationMaster] WHERE ([LocType] = @LocType)">
                    <SelectParameters>
                        <asp:Parameter DefaultValue="F" Name="LocType" Type="String" />
                    </SelectParameters>
                </asp:SqlDataSource>
       
</asp:Content>


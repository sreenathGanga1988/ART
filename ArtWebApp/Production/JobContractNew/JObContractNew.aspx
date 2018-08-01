<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="JObContractNew.aspx.cs" Inherits="ArtWebApp.Production.JobContractNew.JObContractNew" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../css/style.css" rel="stylesheet" />

    <script src="../JQuery/GridJQuery.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>


     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
         <ContentTemplate>

             <table class="DataEntryTable">
        <tr class="RedHeadding">
            <td colspan="4">JOB Contract CM Based on Allocation</td>
            <td>&nbsp;</td>
        </tr>
                 <tr>
                     <td class="NormalTD">Factory</td>
                     <td class="NormalTD">
                         <ucc:DropDownListChosen ID="drp_factory" runat="server" DataSourceID="FactorydataSource" DataTextField="LocationName" DataValueField="Location_PK" DisableSearchThreshold="10" Width="200px">
                         </ucc:DropDownListChosen>
                     </td>
                     <td class="SearchButtonTD"></td>
                     <td class="NormalTD"></td>
                     <td class="NormalTD">&nbsp;</td>
                 </tr>
        <tr>
            <td class="NormalTD">atc</td>
             <td class="NormalTD" >
                 <ucc:DropDownListChosen ID="cmb_atc" runat="server" DataSourceID="SqlDataSource1" DataTextField="AtcNum" DataValueField="AtcId" DisableSearchThreshold="10" Width="200px">
                 </ucc:DropDownListChosen>
            </td>
             <td class="SearchButtonTD">
                 <asp:Button ID="btn_showPO" runat="server" Text="S" OnClick="btn_showPO_Click" />
            </td>
             <td class="NormalTDauto-style7"></td>
            <td class="NormalTDauto-style7">&nbsp;</td>
        </tr>
        
                 <tr>
                     <td class="NormalTD">OurStyle </td>
                     <td class="NormalTD">
                         <ucc:DropDownListChosen ID="cmb_ourstyle" runat="server" DataSourceID="SqlDataSource2" DataTextField="OurStyle" DataValueField="OurStyleID" DisableSearchThreshold="10" Width="200px">
                         </ucc:DropDownListChosen>
                     </td>
                     <td class="SearchButtonTD">
                           <asp:UpdatePanel ID="UpdatePanel3" runat="server">
         <ContentTemplate>
                         <asp:Button ID="btn_showPO0" runat="server" Text="S" OnClick="btn_showPO0_Click" />
                          </ContentTemplate>
     </asp:UpdatePanel>   </td>
                     <td class="NormalTDauto-style7">Locked CM</td>
                     <td class="NormalTDauto-style7">
                         <asp:Label ID="Lbl_LockedCM" runat="server" Text="0"></asp:Label>
                     </td>
                 </tr>
        
                 <tr>
                     <td class="NormalTD">CM /PC</td>
                     <td class="NormalTD">
                         <asp:TextBox ID="txt_cmcost" runat="server">0</asp:TextBox>
                     </td>
                     <td class="SearchButtonTD">&nbsp;</td>
                     <td class="NormalTDauto-style7">Appr CM</td>
                     <td class="NormalTDauto-style7">
                         <asp:TextBox ID="txt_approvecost" runat="server"></asp:TextBox>
                     </td>
                 </tr>
        
                 <tr>
                     <td class="NormalTD">Remark</td>
                     <td class="NormalTD">
                         <asp:TextBox ID="txt_remark" runat="server" Height="45px" TextMode="MultiLine" Width="184px"></asp:TextBox>
                     </td>
                     <td class="SearchButtonTD">&nbsp;</td>
                     <td>&nbsp;</td>
                     <td>&nbsp;</td>
                 </tr>
        <tr>
                     <td>
                         &nbsp;</td>
              <td class="NormalTD">
                         <asp:Label ID="lbl_jcnum" runat="server" Text="NA"></asp:Label>
                     </td>
                     <td class="SearchButtonTD">Qty</td>
            <td >
                JC<asp:Label ID="lbl_totalQty" runat="server" Text="0"></asp:Label>
                </td>
                     <td>&nbsp;</td>
        </tr>
                 <tr>
                     <td colspan="5">&nbsp;

                         
    <div class="DataEntryTable">


        <asp:Button ID="btn_JCSubmit" runat="server" Text="Submit" OnClick="btn_JCSubmit_Click" style="height: 26px" />


    </div>

       <div id="Messaediv" runat="server">
                 


                           <asp:Label ID="lbl_msg" runat="server" Text="*"></asp:Label>


                     
               </div>

                     </td>
                 </tr>
                 <tr>
                     <td colspan="5">&nbsp;</td>
                 </tr>
    </table>
         </ContentTemplate>
     </asp:UpdatePanel>


 </div>
    <div class="gridtable">

         <asp:UpdatePanel ID="UpdatePanel2" runat="server">
         <ContentTemplate>

        <asp:GridView ID="tbl_podetails" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4" style="font-size: x-small; font-family: Calibri" Width="100%" Font-Size="Large" OnRowDataBound="tbl_podetails_RowDataBound" OnDataBound="tbl_podetails_DataBound">
                            <Columns>      
                <asp:BoundField DataField="PoPackId" HeaderText="PoPackId" InsertVisible="False" ReadOnly="True" SortExpression="PoPackId" />
                <asp:BoundField DataField="PoPacknum" HeaderText="PoPacknum" SortExpression="PoPacknum" />
                <asp:BoundField DataField="BuyerPO" HeaderText="BuyerPO" SortExpression="BuyerPO" />
                <asp:BoundField DataField="OurStyle" HeaderText="OurStyle" SortExpression="OurStyle" />
                                <asp:BoundField DataField="BuyerStyle" HeaderText="BuyerStyle" SortExpression="BuyerStyle" />
                                <asp:BoundField DataField="BuyerDestination" HeaderText="BuyerDestination" SortExpression="BuyerDestination" />
                                <asp:BoundField DataField="ChannelName" HeaderText="ChannelName" SortExpression="ChannelName" />
                                <asp:BoundField DataField="SeasonName" HeaderText="SeasonName" SortExpression="SeasonName" />
                                <asp:TemplateField HeaderText="PoQty" SortExpression="PoQty">
                                  
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_Qty" runat="server" Text='<%# Bind("PoQty") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="FirstDeliveryDate" HeaderText="FirstDeliveryDate" SortExpression="FirstDeliveryDate" />
                                <asp:BoundField DataField="HandoverDate" HeaderText="HandoverDate" SortExpression="HandoverDate" />
                                <asp:BoundField DataField="LocationName" HeaderText="LocationName" SortExpression="LocationName" />
                                <asp:BoundField DataField="DeliveryDate" HeaderText="DeliveryDate" SortExpression="DeliveryDate" />
                                <asp:BoundField DataField="Location_PK" HeaderText="Location_PK" InsertVisible="False" ReadOnly="True" SortExpression="Location_PK" Visible="False" />
                                <asp:BoundField DataField="OurStyleID" HeaderText="OurStyleID" SortExpression="OurStyleID" Visible="False" />
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


    <asp:SqlDataSource ID="FactorydataSource" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT [Location_PK], [LocationName] FROM [LocationMaster] WHERE ([LocType] = @LocType)">
                    <SelectParameters>
                        <asp:Parameter DefaultValue="F" Name="LocType" Type="String" />
                    </SelectParameters>
                </asp:SqlDataSource>


                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT [AtcNum], [AtcId] FROM [AtcMaster]"></asp:SqlDataSource>
            
<asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT [OurStyleID], [OurStyle] FROM [AtcDetails] WHERE ([AtcId] = @AtcId)">
    <SelectParameters>
        <asp:ControlParameter ControlID="cmb_atc" Name="AtcId" PropertyName="SelectedValue" Type="Decimal" />
    </SelectParameters>
</asp:SqlDataSource>
<asp:SqlDataSource ID="Podata" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT        PoPackMaster.PoPackId, PoPackMaster.PoPacknum, PoPackMaster.BuyerPO, AtcDetails.OurStyle, AtcDetails.BuyerStyle, BuyerDestinationMaster.BuyerDestination, ChannelMaster.ChannelName, 
                         PoPackMaster.SeasonName, POPackDetails.PoQty, PoPackMaster.FirstDeliveryDate, PoPackMaster.HandoverDate, LocationMaster.LocationName, PoPackMaster.DeliveryDate, LocationMaster.Location_PK, 
                         POPackDetails.OurStyleID
FROM            PoPackMaster INNER JOIN
                         POPackDetails ON PoPackMaster.PoPackId = POPackDetails.POPackId INNER JOIN
                         LocationMaster ON PoPackMaster.ExpectedLocation_PK = LocationMaster.Location_PK INNER JOIN
                         BuyerDestinationMaster ON PoPackMaster.BuyerDestination_PK = BuyerDestinationMaster.BuyerDestination_PK INNER JOIN
                         ChannelMaster ON PoPackMaster.ChannelID = ChannelMaster.ChannelID INNER JOIN
                         AtcDetails ON POPackDetails.OurStyleID = AtcDetails.OurStyleID
WHERE        (POPackDetails.OurStyleID = @OurStyleID)">
    <SelectParameters>
        <asp:ControlParameter ControlID="drp_factory" Name="Location_PK" PropertyName="SelectedValue" />
        <asp:ControlParameter ControlID="cmb_ourstyle" Name="OurStyleID" PropertyName="SelectedValue" />
    </SelectParameters>
</asp:SqlDataSource>
<br />
            
</asp:Content>


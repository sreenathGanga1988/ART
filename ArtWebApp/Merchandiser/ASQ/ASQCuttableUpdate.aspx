<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="ASQCuttableUpdate.aspx.cs" Inherits="ArtWebApp.Merchandiser.ASQ.ASQCuttableUpdate" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
 

  
    
 
  
    <link href="../../css/style.css" rel="stylesheet" />
  <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script src="../../JQuery/GridJQuery.js">
   
    <script type="text/javascript">

        


     </script>
    <style type="text/css">
       body
{
    margin: 0;
    padding: 0;
    font-family: Arial;
}
.modal
{
    position: fixed;
    z-index: 999;
    height: 100%;
    width: 100%;
    top: 0;
    background-color: Black;
    filter: alpha(opacity=60);
    opacity: 0.6;
    -moz-opacity: 0.8;
}
.center
{
    z-index: 1000;
    margin: 300px auto;
    padding: 10px;
    width: 130px;
    background-color: White;
    border-radius: 10px;
    filter: alpha(opacity=100);
    opacity: 1;
    -moz-opacity: 1;
}
.center img
{
    height: 128px;
    width: 128px;
}

   
      
       
        .smalltable {
            width: 50px;
        }
       
      
       
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    
    
     
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate> 
<div class="FullTable">
        <table class="FullTable">
        <tr  class="RedHeadding">
            <td style="color: #FFFFFF; text-align: center; background-color: #990000">asq ALLOCATION</td>
        </tr>
        <tr>
            <td >



                 <table >
                        <tr>
                            <td class="NormalTD">

                                   Atc# 
                
                            </td>
                            <td class="NormalTD">
                               
                   
                    
               <ucc:DropDownListChosen ID="cmb_atc" runat="server" DataSourceID="SqlDataSource1" DataTextField="AtcNum" DataValueField="AtcId" DisableSearchThreshold="10" Width="200px" >
                            </ucc:DropDownListChosen>
                               
                            </td>
                            <td class="SearchButtonTD">
                                 
                                <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                    <ContentTemplate>
                                        <asp:Button ID="buttonAtc" runat="server" Text="S" Height="26px" OnClick="buttonAtc_Click" />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                     
                            </td>
                            <td>
                               
                                <asp:Button ID="btn_showallasq" runat="server" OnClick="btn_showallasq_Click" Text="Show All ASQ" />
                            </td>
                            </tr>

                        

                        <tr>
                            <td class="NormalTD">

                                PO Pack </td>
                            <td class="NormalTD">
                                 <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                     <ContentTemplate>
                                         <ig:WebDropDown ID="drp_popack" runat="server" EnableClosingDropDownOnSelect="False" EnableMultipleSelection="True" TextField="POnum" ValueField="PoPackId" Width="200px">
                                                <DropDownItemBinding TextField="name" ValueField="pk" />
                                            </ig:WebDropDown>
                                         
                                     </ContentTemplate>
                                 </asp:UpdatePanel>
                    
                
                            </td>
                            <td class="SearchButtonTD">
                                 
                                
                     
                                <asp:Button ID="buttonAtc0" runat="server" Height="26px" Text="S" OnClick="buttonAtc0_Click" />
                            </td>
                            <td>
                               
                                &nbsp;</td>
                            </tr>

                        

                        

                        

                        

                        

                        

                        

                        

                        

                        

                        

                        

                        

                        

                </table>

                
               
               
            </td>
        </tr>
        
       
        <tr>
            <td>
                
            </td>
        </tr>
    </table>
    </div>

<div>
        <table class="DataEntryTable">
                    <tr>
                      
                        <td class="auto-style8"><asp:UpdatePanel ID="upd_main" runat="server">
                                    <ContentTemplate>
                                       <%-- <ig:WebDropDown ID="cmb_ourstyle" runat="server" Width="189px" TextField="name"
        DropDownContainerHeight="300px" EnableDropDownAsChild="false"
        DropDownContainerWidth="200px" DropDownAnimationType="EaseOut" EnablePaging="True"
        PageSize="12" Height="22px" ValueField="pk" CurrentValue="Select OurStyle" AutoPostBack="True" OnDataBound="cmb_ourstyle_DataBound" OnValueChanged="cmb_ourstyle_ValueChanged">
                                            <DropDownItemBinding TextField="name" ValueField="pk" />
                                        </ig:WebDropDown>--%>

                                        <asp:GridView ID="tbl_podata" 
                                            Width="100%" 
                                            runat="server"
                                             AutoGenerateColumns="False" 
                                         
                                             DataKeyNames="PoPackId"
                                             OnRowDataBound="tbl_podata_RowDataBound"
                                             style="font-size: small; font-family: Calibri; font-weight: 400;" BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4" OnRowCommand="tbl_podata_RowCommand" OnSelectedIndexChanged="tbl_podata_SelectedIndexChanged" Font-Size="Smaller">
                                            <AlternatingRowStyle BackColor="White" />
                                            <Columns>
                                               
                                               <asp:TemplateField>  
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="checkAll" runat ="server" onclick="checkAll(this)"/>
                                    </HeaderTemplate>                                 
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chk_select" runat="server" onclick="Check_Click(this)"/>
                                    </ItemTemplate>
                                </asp:TemplateField>    
                                               
                                                <asp:TemplateField HeaderText="IDS" >
                                                    <ItemTemplate>
                                                        <table class="tittlebar">
                                                            <tr>
                                                                <td>POPAckid</td>
                                                                <td> <asp:Label ID="lbl_popackid" runat="server" Text='<%# Bind("PoPackId") %>'></asp:Label></td>
                                                            </tr>
                                                            <tr>
                                                                <td>Outstyleid</td>
                                                                <td> <asp:Label ID="lbl_ourstyleid" runat="server" Text='<%# Bind("OurStyleID") %>'></asp:Label></td>
                                                            </tr>
                                                            <tr>
                                                                <td>atcid</td>
                                                                <td>  <asp:Label ID="lbl_atcid" runat="server" Text='<%# Bind("AtcId") %>'></asp:Label></td>
                                                            </tr>
                                                             <tr>
                                                                <td>atcnum</td>
                                                                <td>  <asp:Label ID="lbl_atcnum" runat="server" Text='<%# Bind("AtcNum") %>'></asp:Label></td>
                                                            </tr>

                                                             <tr>
                                                                <td>CategoryID</td>
                                                                <td> <asp:Label ID="lbl_CategoryID" runat="server" Text='<%# Bind("CategoryID") %>'></asp:Label></td>
                                                            </tr>
                                                            <tr>
                                                                <td>ChannelID</td>
                                                                <td> <asp:Label ID="lbl_ChannelID" runat="server" Text='<%# Bind("ChannelID") %>'></asp:Label></td>
                                                            </tr>
                                                            <tr>
                                                                <td>BuyerDestination_PK</td>
                                                                <td>  <asp:Label ID="lbl_BuyerDestination_PK" runat="server" Text='<%# Bind("BuyerDestination_PK") %>'></asp:Label></td>
                                                            </tr>
                                                             <tr>
                                                                <td>Season_PK</td>
                                                                <td>  <asp:Label ID="lbl_Season_PK" runat="server" Text='<%# Bind("Season_PK") %>'></asp:Label></td>
                                                            </tr>
                                                             <tr>
                                                                <td>BuyerName</td>
                                                                <td>  <asp:Label ID="lbl_BuyerName" runat="server" Text='<%# Bind("BuyerName") %>'></asp:Label></td>
                                                            </tr>
                                                             <tr>
                                                                <td>BuyerID</td>
                                                                <td>  <asp:Label ID="lbl_BuyerID" runat="server" Text='<%# Bind("BuyerID") %>'></asp:Label></td>
                                                            </tr>





                                                        </table>
                                                    </ItemTemplate>
                                                <HeaderStyle CssClass="hidden" />
                                                    <ItemStyle CssClass="hidden" />
                                                        <ControlStyle Width="200px" />
                                                            <FooterStyle Width="200px" />
                                                            <HeaderStyle Width="200px" />
                                                            <ItemStyle Width="200px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Allocated Factory" >
                                                    <ItemTemplate>
                                                        <table class="tittlebar">
                                                             <tr>
                                                                
                                                                <td> <asp:Label ID="lbl_location" runat="server" Text='<%# Bind("LocationName") %>'></asp:Label></td>
                                                            </tr>
                                                            <tr>
                                                                
                                                                <td> <asp:DropDownList ID="drp_loc" runat="server" DataSourceID="FACTORYDATASOURCE" DataTextField="LocationName" DataValueField="Location_PK" Font-Size="Smaller" ></asp:DropDownList></td>
                                                            </tr>
                                                            
                                                        </table>
                                                    </ItemTemplate>
                                                        <ControlStyle Width="200px" />
                                                            <FooterStyle Width="200px" />
                                                            <HeaderStyle Width="200px" />
                                                            <ItemStyle Width="200px" />
                                                </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="ASQ Details" >
                                                    <ItemTemplate>
                                                        <table class="tittlebar">
                                                             <tr>
                                                                <td>ASQ</td>
                                                                <td> <asp:Label ID="lbl_asq" runat="server" Text='<%# Bind("ASQ") %>'></asp:Label></td>
                                                            </tr>
                                                           
                                                            <tr>
                                                                <td>BuyerPO</td>
                                                                <td> <asp:Label ID="lbl_buyerpo" runat="server" Text='<%# Bind("BuyerPO") %>'></asp:Label></td>
                                                            </tr>
                                                            <tr>
                                                                <td>BuyerStyle</td>
                                                                <td>  <asp:Label ID="lbl_buyerstyle" runat="server" Text='<%# Bind("BuyerStyle") %>'></asp:Label></td>
                                                            </tr>
                                                             <tr>
                                                                <td>OurStyle</td>
                                                                <td>  <asp:Label ID="lbl_ourstyle" runat="server" Text='<%# Bind("OurStyle") %>'></asp:Label></td>
                                                            </tr>

                                                              <tr>
                                                                <td>Season</td>
                                                                <td>  <asp:Label ID="lbl_season" runat="server" Text='<%# Bind("SeasonName") %>'></asp:Label></td>
                                                            </tr>
                                                             <tr>
                                                                <td>Deliverydate</td>
                                                                <td>  <asp:Label ID="lbl_deliverydate" runat="server" Text='<%# Bind("DeliveryDate") %>'></asp:Label></td>
                                                            </tr>
                                                             <tr>
                                                                <td>Destination#</td>
                                                                <td> <asp:Label ID="lbl_destination" runat="server" Text='<%# Bind("BuyerDestination") %>'></asp:Label></td>
                                                            </tr>
                                                              <tr>
                                                                <td>CategoryName</td>
                                                                <td> <asp:Label ID="lbl_CategoryName" runat="server" Text='<%# Bind("CategoryName") %>'></asp:Label></td>
                                                            </tr>

  <tr>
                                                                <td>ChannelName</td>
                                                                <td> <asp:Label ID="lbl_ChannelName" runat="server" Text='<%# Bind("ChannelName") %>'></asp:Label></td>
                                                            </tr>

                                                             <tr>
                                                                <td>Cutable</td>
                                                                <td> 
                                                                     <asp:Label ID="lbl_iscutable" runat="server" ></asp:Label>
                                                            

                                                                </td>
                                                            </tr>
                                                              <tr>
                                                                <td>TYPE</td>
                                                                <td> 
                                                                     
                                                                   
                                                                    
                                                                    

                                                                </td>
                                                            </tr>

                                                        </table>
                                                    </ItemTemplate>
                                                        <ControlStyle Width="200px" />
                                                            <FooterStyle Width="200px" />
                                                            <HeaderStyle Width="200px" />
                                                            <ItemStyle Width="200px" />
                                                </asp:TemplateField>
<asp:TemplateField HeaderText="ASQType">
                                                    <ItemTemplate>
                                                       <%-- <table class="smalltable">
                                                            <tr>
                                                                  <td><asp:RadioButton ID="rbt_reg" runat="server" ValidationGroup="a" Text="Reg" /></td>
                                                            </tr>
                                                            <tr>
                                                               <td><asp:RadioButton ID="rbt_can" runat="server" ValidationGroup="a" Text="Can" /></td>
                                                            </tr>
                                                            <tr>
                                                                 <td><asp:RadioButton ID="rbt_ecom" runat="server" ValidationGroup="a" Text="ECom" /></td>
                                                            </tr>
                                                            <tr>
                                                                <td><asp:RadioButton ID="rbt_intl" runat="server" ValidationGroup="a" Text="Intl" /></td>
                                                            </tr>
                                                        </table>--%>
                                                        <asp:RadioButtonList ID="rbt_potype" runat="server" 
    RepeatDirection="Horizontal"
    RepeatLayout="Flow" >
    <asp:ListItem Selected="true" Text="Reg" Value="Reg" />
    <asp:ListItem Selected="false" Text="Can" Value="Can" />
    <asp:ListItem Selected="false" Text="ECom" Value="ECom" />
    <asp:ListItem Selected="false" Text="Intl" Value="Intl" />
</asp:RadioButtonList>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Details" SortExpression="Details">
                                                          
                                                            <ItemTemplate>



                                                               
                            <asp:UpdatePanel ID="upd_table"  runat="server">
                                            <ContentTemplate>
                            
                            <asp:Panel ID="panel1" runat="server" ViewStateMode="Enabled">
                                <asp:Table ID="Table1" runat="server" ViewStateMode="Enabled" Width="400px">
                                </asp:Table>
                            </asp:Panel>
                                                
                                                </ContentTemplate>
                                        </asp:UpdatePanel>



                                                               
                                                            </ItemTemplate>
                                                            
                                                            <ControlStyle Width="400px" />
                                                            <FooterStyle Width="400px" />
                                                            <HeaderStyle Width="400px" />
                                                            <ItemStyle Font-Size="Smaller" Width="400px" />
                                                            
                                                        </asp:TemplateField>
                                                        
                                                      
                                                       
                                                
                                                        
                                                      
                                                       
                                            </Columns>
                                        <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
                                    <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="#FFFFCC" />
                                    <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
                                    <RowStyle BackColor="White" ForeColor="#330099" />
                                    <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
                                    <SortedAscendingCellStyle BackColor="#FEFCEB" />
                                    <SortedAscendingHeaderStyle BackColor="#AF0101" />
                                    <SortedDescendingCellStyle BackColor="#F6F0C0" />
                                    <SortedDescendingHeaderStyle BackColor="#7E0000" />
                                        </asp:GridView>

                                    </ContentTemplate>
                                </asp:UpdatePanel></td>
                        
                    </tr>
                   
                    
                    <tr>
                        <td class="NormalTD">
                           
                            <table class="DataEntryTable">
                                <tr>
                                    <td>&nbsp;</td>
                                    <td>
                                        <asp:Button ID="btn_marknonCuttable" runat="server" OnClick="Button1_Click" Text="MARK nOT cUTABLE" Visible="False" />
                                    </td>
                                    <td>
                                        <asp:Button ID="btn_updatekenya" runat="server"  Text="Update kenya" OnClick="btn_updatekenya_Click" /></td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        
                                                <div id="Messaediv" runat="server">
                                                    <asp:Label ID="lbl_msg" runat="server" Text="*"></asp:Label>
                                                </div>
                                          
                                    </td>
                                    <td>&nbsp;</td>
                                </tr>
                            </table>
                           
                        </td>
                    
                    </tr>
                   
                    
                </table>
         <asp:SqlDataSource ID="FACTORYDATASOURCE" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT [Location_PK], [LocationName] FROM [LocationMaster] WHERE ([LocType] = @LocType)">
                                <SelectParameters>
                                    <asp:Parameter DefaultValue="F" Name="LocType" Type="String" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                               <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT StyleColor.StyleColorid, StyleSize.StyleSizeID, StyleColor.AtcId, StyleColor.OurStyleID, StyleColor.OurStyle, StyleColor.GarmentColorCode, StyleColor.GarmentColor, StyleSize.SizeCode, StyleSize.SizeName, 000000 AS POQty FROM StyleColor INNER JOIN StyleSize ON StyleColor.AtcId = StyleSize.AtcId AND StyleColor.OurStyleID = StyleSize.OurStyleID WHERE (StyleColor.OurStyleID = @Param1)">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="ourstylehiden" DefaultValue="0" Name="Param1" PropertyName="Value" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                            <asp:HiddenField ID="ourstylehiden" runat="server" />
                            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT DISTINCT [AtcNum], [AtcId] FROM [AtcMaster] ORDER BY [AtcNum], [AtcId]"></asp:SqlDataSource>
                            <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString3 %>" 
                                SelectCommand="SELECT PoPackMaster.PoPacknum + ' / ' + PoPackMaster.BuyerPO AS ASQ, PoPackMaster.PoPacknum, PoPackMaster.BuyerPO, PoPackMaster.PoPackId, POPackDetails.OurStyleID, AtcDetails.OurStyle, AtcDetails.BuyerStyle, PoPackMaster.AtcId, PoPackMaster.IsCutable, POPackDetails.IsPackable, CAST(PoPackMaster.DeliveryDate AS date) AS DeliveryDate, PoPackMaster.SeasonName, ISNULL((SELECT DISTINCT LocationMaster.LocationName FROM ASQAllocationMaster INNER JOIN LocationMaster ON ASQAllocationMaster.Locaion_PK = LocationMaster.Location_PK WHERE (ASQAllocationMaster.POPackID = PoPackMaster.PoPackId) AND (ASQAllocationMaster.Qty&gt;0)  AND (ASQAllocationMaster.OurStyleId = POPackDetails.OurStyleID)), N'NA') AS LocationName, ChannelMaster.ChannelName, BuyerDestinationMaster.BuyerDestination, AtcMaster.AtcNum, GarmentCategory.CategoryName, GarmentCategory.CategoryID, ChannelMaster.ChannelID, BuyerDestinationMaster.BuyerDestination_PK, SeasonMaster.Season_PK, BuyerMaster.BuyerID, BuyerMaster.BuyerName FROM PoPackMaster INNER JOIN POPackDetails ON PoPackMaster.PoPackId = POPackDetails.POPackId INNER JOIN AtcDetails ON POPackDetails.OurStyleID = AtcDetails.OurStyleID INNER JOIN ChannelMaster ON PoPackMaster.ChannelID = ChannelMaster.ChannelID INNER JOIN BuyerDestinationMaster ON PoPackMaster.BuyerDestination_PK = BuyerDestinationMaster.BuyerDestination_PK INNER JOIN AtcMaster ON AtcDetails.AtcId = AtcMaster.AtcId INNER JOIN GarmentCategory ON AtcDetails.CategoryID = GarmentCategory.CategoryID INNER JOIN SeasonMaster ON PoPackMaster.SeasonName = SeasonMaster.SeasonName INNER JOIN BuyerMaster ON AtcMaster.Buyer_ID = BuyerMaster.BuyerID GROUP BY PoPackMaster.PoPacknum + ' / ' + PoPackMaster.BuyerPO, PoPackMaster.PoPacknum, PoPackMaster.BuyerPO, PoPackMaster.PoPackId, POPackDetails.OurStyleID, AtcDetails.OurStyle, AtcDetails.BuyerStyle, PoPackMaster.AtcId, PoPackMaster.IsCutable, POPackDetails.IsPackable, PoPackMaster.DeliveryDate, PoPackMaster.SeasonName, ChannelMaster.ChannelName, BuyerDestinationMaster.BuyerDestination, AtcMaster.AtcNum, GarmentCategory.CategoryName, GarmentCategory.CategoryID, ChannelMaster.ChannelID, BuyerDestinationMaster.BuyerDestination_PK, SeasonMaster.Season_PK, BuyerMaster.BuyerID, BuyerMaster.BuyerName HAVING (PoPackMaster.AtcId = @Param1) ORDER BY PoPackMaster.PoPackId">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="cmb_atc" Name="Param1" PropertyName="SelectedValue" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                    
        <br />
                    
    </div>

                                    </ContentTemplate>
                                </asp:UpdatePanel>


           <%--<asp:UpdateProgress ID="PageUpdateProgress" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="0" DynamicLayout="true">
                                <ProgressTemplate>
                                   <div class="modal">
        <div class="center">
          <img  src="../../Image/loader.gif" style="position: relative; top: 45%;" > </img>
        </div>
    </div>
                                     
                                       
                                        
                                </ProgressTemplate>
                            </asp:UpdateProgress>--%>
</asp:Content>


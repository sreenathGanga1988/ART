<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ASQMasterReport.aspx.cs" EnableEventValidation="false" Inherits="ArtWebApp.Reports.MerchandiserReport.ASQMasterReport" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <title></title>
    <link href="../../css/style.css" rel="stylesheet" />
      <style type="text/css">

          .A4
          {
              width: 600px;
            height:100%;
           border-left-style: solid;
            border-left-width: 2px;
            border-right-style: solid;
            border-right-width: 2px;
            border-top-style: solid;
            border-top-width: 2px;
            border-bottom-style: solid;
            border-bottom-width: 2px;
            
          }
          .Details{
              padding-top:20px;
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

   
      








          </style>
    <script src="../../JQuery/GridJQuery.js"></script>
    
   <script src='http://code.jquery.com/jquery-latest.min.js' type='text/javascript'></script>
    <script src="../../Scripts/jquery.table2excel.js"></script>
    <script src="../../JQuery/ExporttoExcel.js"></script>
    <script >

        function fnExcelReport() {
            debugger
            var tab_text = "<table border='2px'><tr bgcolor='#87AFC6'>";
            var textRange; var j = 0;
            tab = document.getElementsByClassName('Headernewtable')[0]; // id of table


            for (j = 0 ; j < tab.rows.length ; j++) {
                tab_text = tab_text + tab.rows[j].innerHTML + "</tr>";
                //tab_text=tab_text+"</tr>";
            }

            tab_text = tab_text + "</table>";
            tab_text = tab_text.replace(/<A[^>]*>|<\/A>/g, "");//remove if u want links in your table
            tab_text = tab_text.replace(/<img[^>]*>/gi, ""); // remove if u want images in your table
          

            var ua = window.navigator.userAgent;
            var msie = ua.indexOf("MSIE ");

            if (msie > 0 || !!navigator.userAgent.match(/Trident.*rv\:11\./))      // If Internet Explorer
            {
                txtArea1.document.open("txt/html", "replace");
                txtArea1.document.write(tab_text);
                txtArea1.document.close();
                txtArea1.focus();
                sa = txtArea1.document.execCommand("SaveAs", true, "Say Thanks to Sumit.xls");
            }
            else                 //other browser not tested on IE 11
                sa = window.open('data:application/vnd.ms-excel,' + encodeURIComponent(tab_text));


            return (sa);
        }


    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Button ID="Button4" runat="server" OnClick="Button1_Click" Text="Export to Excel" Font-Size="Smaller" />
         <asp:Button ID="btnExport" runat="server" OnClientClick="fnExcelReport();" Text="Export to Summary to Excel" Font-Size="Smaller" />
        
    <div class="A4"><asp:ScriptManager ID="ScriptManager1" runat="server">
                                </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
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
                               
                        <asp:UpdatePanel ID="upd_atc" UpdateMode="Conditional" runat="server">
                                    <ContentTemplate>
                    
               <ucc:DropDownListChosen ID="cmb_atc" runat="server"  DataTextField="AtcNum" DataValueField="AtcId" DisableSearchThreshold="10" Width="200px" >
                            </ucc:DropDownListChosen>
                                 </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                            <td class="SearchButtonTD">
                                 
                                <asp:UpdatePanel ID="UpdatePanel6" UpdateMode="Conditional" runat="server">
                                    <ContentTemplate>
                                        <asp:Button ID="buttonAtc" runat="server" Text="S" Height="26px" OnClick="buttonAtc_Click" ToolTip="Select Atc" />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                     
                            </td>
                            <td>
                                    <asp:UpdatePanel ID="upd_asq" UpdateMode="Conditional" runat="server">
                                    <ContentTemplate>
                                <asp:Button ID="btn_showallasq" runat="server" OnClick="btn_showallasq_Click" Text="Show All ASQ" ToolTip="Show All the ASQ of Selected Atc" Font-Size="Smaller" />
                                </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                            <td>&nbsp;</td>
                            </tr>

                        

                        <tr>
                            <td class="NormalTD">

                                PO Pack </td>
                            <td class="NormalTD">
                                 <asp:UpdatePanel ID="upd_popack" UpdateMode="Conditional" runat="server">
                                     <ContentTemplate>
                                         <ig:WebDropDown ID="drp_popack" runat="server" EnableClosingDropDownOnSelect="False" EnableMultipleSelection="True" TextField="POnum" ValueField="PoPackId" Width="200px">
                                                <DropDownItemBinding TextField="name" ValueField="pk" />
                                            </ig:WebDropDown>
                                         
                                     </ContentTemplate>
                                 </asp:UpdatePanel>
                    
                
                            </td>
                            <td class="SearchButtonTD">
                                 
                                
                          <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                    <ContentTemplate>
                                <asp:Button ID="buttonAtc0" runat="server" Height="26px" Text="S" OnClick="buttonAtc0_Click" ToolTip="Show ASQ report of Selected ASQ" />
                          </ContentTemplate>
                                </asp:UpdatePanel>  </td>
                            <td>
                               
                                &nbsp;</td>
                            <td>&nbsp;</td>
                            </tr>


                        <tr>
                            <td class="NormalTD">Style </td>
                            <td class="NormalTD">
                                     <asp:UpdatePanel ID="upd_style"  UpdateMode="Conditional" runat="server">
                                    <ContentTemplate>
                                <ig:WebDropDown ID="drp_style" runat="server" EnableClosingDropDownOnSelect="False" EnableMultipleSelection="True" TextField="Style" ValueField="OurStyleID" Width="200px">
                                    <DropDownItemBinding TextField="Style" ValueField="OurStyleID" />
                                </ig:WebDropDown>
                           </ContentTemplate>
                                </asp:UpdatePanel> </td>
                            <td class="SearchButtonTD">
                                     <asp:UpdatePanel ID="UpdatePanel9" UpdateMode="Conditional" runat="server">
                                    <ContentTemplate>
                                <asp:Button ID="Button1" runat="server" OnClick="Button1_Click1" Text="S" ToolTip="Shows All the ASQ of Selected Style of Selected ATC" />
                          </ContentTemplate>
                                </asp:UpdatePanel>  </td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="NormalTD">Season</td>
                            <td class="NormalTD">
                                     <asp:UpdatePanel ID="upd_season" UpdateMode="Conditional" runat="server">
                                    <ContentTemplate>
                                <ig:WebDropDown ID="drp_season" runat="server" EnableClosingDropDownOnSelect="False" EnableMultipleSelection="True" TextField="SeasonName" ValueField="SeasonName" Width="200px">
                                    <DropDownItemBinding TextField="SeasonName" ValueField="SeasonName" />
                                </ig:WebDropDown>
                                
                          </ContentTemplate>
                                </asp:UpdatePanel>  </td>
                            <td class="SearchButtonTD">
                                     <asp:UpdatePanel ID="UpdatePanel8" UpdateMode="Conditional" runat="server">
                                    <ContentTemplate>
                                <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="S" ToolTip="Show ASQ Report of Selected Season ASQ of Selected Atc" />
                         </ContentTemplate>
                                </asp:UpdatePanel>   </td>
                            
                            <td>&nbsp;</td>
                        </tr>


                        <tr>
                            <td class="NormalTD">Factory</td>
                            <td class="NormalTD">
                                     <asp:UpdatePanel ID="upd_fact" UpdateMode="Conditional" runat="server">
                                    <ContentTemplate>
                                <ig:WebDropDown ID="drp_fact" runat="server" EnableClosingDropDownOnSelect="False"  EnableMultipleSelection="True" TextField="LocationName" ValueField="Location_PK" Width="200px" DisplayMode="DropDownList">
                                    
                                    <DropDownItemBinding TextField="LocationName" ValueField="Location_PK" />
                                </ig:WebDropDown>
                              
                          </ContentTemplate>
                                </asp:UpdatePanel>  </td>
                            <td class="SearchButtonTD">
                                  <asp:UpdatePanel ID="UpdatePanel12" UpdateMode="Conditional" runat="server">
                                    <ContentTemplate>
                                <asp:Button ID="Button3" runat="server" OnClick="Button3_Click" Text="S" ToolTip="Show ASQ Report For Selected Atc in Selected Location" />
                           </ContentTemplate>
                                </asp:UpdatePanel> </td>
                            <td>
                                <asp:Button ID="Button5" runat="server" Font-Size="Smaller" OnClick="Button5_Click" Text="Season And Factory" />
                            </td>
                            <td><asp:Button ID="Button6" runat="server" Font-Size="Smaller" Text="Style And Factory" OnClick="Button6_Click" /></td>
                        </tr>


                        <tr>
                            <td class="NormalTD">
                                    <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Conditional" runat="server">
                                    <ContentTemplate>
                                
                                         </ContentTemplate>
                                </asp:UpdatePanel>
                                
                            </td>
                            <td class="NormalTD">&nbsp;</td>
                            <td class="SearchButtonTD">&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
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
  </ContentTemplate>
                                </asp:UpdatePanel>
<div >
        <table class="DataEntryTable">
                    <tr>
                      
                        <td >
                            <%--  <asp:UpdateProgress ID="PageUpdateProgress" runat="server" AssociatedUpdatePanelID="upd_main" DisplayAfter="0" DynamicLayout="true">
                                <ProgressTemplate>
                                    <div class="modal">
                                        <div class="center">
                                            <img src="../../Image/loader.gif" style="position: relative; top: 45%;"> </img>
                                        </div>
                                    </div>
                                </ProgressTemplate>
                            </asp:UpdateProgress>--%>
                        </td>
                        
                    </tr>
                   
                    
                    <tr>
                      
                        <td >
                            
                            
                          
                            
                            
                            <asp:UpdatePanel ID="upd_main" UpdateMode="Conditional" runat="server">
                                    <ContentTemplate>
                         
                                        <asp:GridView ID="tbl_podata" 
                                            Width="100%" 
                                            runat="server"
                                             AutoGenerateColumns="False" 
                                         
                                             DataKeyNames="PoPackId"
                                             OnRowDataBound="tbl_podata_RowDataBound"
                                             style="font-size: small; font-family: Calibri; font-weight: 400;" BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4" OnRowCommand="tbl_podata_RowCommand" OnSelectedIndexChanged="tbl_podata_SelectedIndexChanged" Font-Size="Smaller" CssClass="mydatagrid">
                                            <AlternatingRowStyle BackColor="White" />
                                            <Columns>
                                    
                                               
                                                <asp:TemplateField HeaderText="IDS"  HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" ControlStyle-CssClass="hidden" FooterStyle-CssClass="hidden" >
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
                                               
                                                    <ControlStyle CssClass="hidden" />
                                                    <FooterStyle CssClass="hidden" />
                                                    <HeaderStyle CssClass="hidden" />
                                                    <ItemStyle CssClass="hidden" />
                                               
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Allocated Factory" >
                                                    <ItemTemplate>
                                                        <table class="tittlebar">
                                                             <tr>
                                                               <td>Factory</td>
                                                                <td> <asp:Label ID="lbl_location" runat="server" Text='<%# Bind("LocationName") %>'></asp:Label></td>
                                                            </tr>
                                                           
                                                            <tr>
                                                                <td>Cutable</td>
                                                                <td> 
                                                                     <asp:Label ID="lbl_iscutable" Text='<%# Bind("IsCutable") %>' runat="server" ></asp:Label>
                                                            

                                                                </td>
                                                            </tr>
                                                             <tr>
                                                                <td>BuyerPO</td>
                                                                <td> <asp:Label ID="lbl_buyerpo" runat="server" Text='<%# Bind("BuyerPO") %>'></asp:Label></td>
                                                            </tr>
                                                             <tr>
                                                                <td>Packing Instruction</td>
                                                                <td> 
                                                                     <asp:TextBox ID="lbl_pack" Text='<%# Bind("PackingInstruction") %>'  Wrap="true"  Font-Size="Smaller" ReadOnly="true" TextMode="MultiLine"  BorderStyle="None" BorderWidth="0" runat="server" ></asp:TextBox>
                                                            

                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td> Allocated</td>
                                                                <td> 
                                                                     <asp:Label ID="lbl_allocated"  runat="server" ></asp:Label>
                                                            

                                                                </td>
                                                            </tr>
                                                              <tr>
                                                                <td> Handover Date</td>
                                                                <td> 
                                                                     <asp:Label ID="Label1" Text='<%# Bind("HandoverDate", "{0:MM/d/yyyy}") %>' runat="server" ></asp:Label>
                                                            

                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ItemTemplate>
                                                       
                                                    <ControlStyle Width="200px" />
                                                    <FooterStyle Width="200px" Wrap="True" />
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
                                                                <td>  <asp:Label ID="lbl_deliverydate" runat="server" Text='<%# Bind("DeliveryDate", "{0:MMM/d/yyyy}") %>'></asp:Label></td>
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

                                                             
                                                             

                                                        </table>
                                                    </ItemTemplate>
                                               
                                                </asp:TemplateField>

                                                     <asp:TemplateField HeaderText="Details" SortExpression="Details">
                                                          
                                                            <ItemTemplate>



                                                               
                            <asp:UpdatePanel ID="upd_table"  runat="server">
                                            <ContentTemplate>
                            
                            <asp:Panel ID="panel1" runat="server" ViewStateMode="Enabled">
                                <asp:Table ID="Table1" runat="server" ViewStateMode="Enabled" Width="400px" BorderStyle="Solid">
                                </asp:Table>
                            </asp:Panel>
                                                
                                                </ContentTemplate>
                                        </asp:UpdatePanel>



                                                               
                                                            </ItemTemplate>
                                                            
                                                          
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
                           
                            <asp:UpdatePanel ID="upd_smalltable" UpdateMode="Conditional" runat="server">
                                        <ContentTemplate>
                                            <asp:Panel ID="mpanel1" runat="server" ViewStateMode="Enabled">
                                        <asp:Table ID="mTable1"  CssClass="mTable1" runat="server" ViewStateMode="Enabled" Width="400px">
                                        </asp:Table>
                                    </asp:Panel>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                           
                        </td>
                    
                    </tr>
                   <tr>
                       <td>
<iframe id="txtArea1" style="display:none"></iframe>
    
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
                            <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" 
                                SelectCommand="SELECT        ASQ, PoPacknum, BuyerPO, PoPackId, OurStyleID, OurStyle, BuyerStyle, AtcId, IsCutable, IsPackable, DeliveryDate, SeasonName, ChannelName, BuyerDestination, AtcNum, CategoryName, CategoryID, 
                         ChannelID, BuyerDestination_PK, Season_PK, BuyerID, BuyerName, PackingInstruction, ExpectedLocation_PK, LocationName, HandoverDate
FROM            (SELECT        TOP (100) PERCENT PoPackMaster.PoPacknum + ' / ' + PoPackMaster.BuyerPO AS ASQ, PoPackMaster.PoPacknum, PoPackMaster.BuyerPO, PoPackMaster.PoPackId, POPackDetails.OurStyleID, 
                                                    AtcDetails.OurStyle, AtcDetails.BuyerStyle, PoPackMaster.AtcId, MAX(POPackDetails.IsCutable) AS IsCutable, POPackDetails.IsPackable, CAST(PoPackMaster.DeliveryDate AS date) AS DeliveryDate, 
                                                    PoPackMaster.SeasonName, ISNULL
                                                        ((SELECT DISTINCT LocationMaster.LocationName
                                                            FROM            ASQAllocationMaster INNER JOIN
                                                                                     LocationMaster ON ASQAllocationMaster.Locaion_PK = LocationMaster.Location_PK
                                                            WHERE        (ASQAllocationMaster.PoPackId = PoPackMaster.PoPackId) AND (ASQAllocationMaster.OurStyleId = POPackDetails.OurStyleID)), LocationMaster_1.LocationName) AS LocationName, 
                                                    ChannelMaster.ChannelName, BuyerDestinationMaster.BuyerDestination, AtcMaster.AtcNum, GarmentCategory.CategoryName, GarmentCategory.CategoryID, ChannelMaster.ChannelID, 
                                                    BuyerDestinationMaster.BuyerDestination_PK, SeasonMaster.Season_PK, BuyerMaster.BuyerID, BuyerMaster.BuyerName, PoPackMaster.PackingInstruction, PoPackMaster.ExpectedLocation_PK, 
                                                    PoPackMaster.HandoverDate
                          FROM            PoPackMaster INNER JOIN
                                                    POPackDetails ON PoPackMaster.PoPackId = POPackDetails.POPackId INNER JOIN
                                                    AtcDetails ON POPackDetails.OurStyleID = AtcDetails.OurStyleID INNER JOIN
                                                    ChannelMaster ON PoPackMaster.ChannelID = ChannelMaster.ChannelID INNER JOIN
                                                    BuyerDestinationMaster ON PoPackMaster.BuyerDestination_PK = BuyerDestinationMaster.BuyerDestination_PK INNER JOIN
                                                    AtcMaster ON AtcDetails.AtcId = AtcMaster.AtcId INNER JOIN
                                                    GarmentCategory ON AtcDetails.CategoryID = GarmentCategory.CategoryID INNER JOIN
                                                    SeasonMaster ON PoPackMaster.SeasonName = SeasonMaster.SeasonName INNER JOIN
                                                    BuyerMaster ON AtcMaster.Buyer_ID = BuyerMaster.BuyerID LEFT OUTER JOIN
                                                    LocationMaster AS LocationMaster_1 ON PoPackMaster.ExpectedLocation_PK = LocationMaster_1.Location_PK
                          GROUP BY PoPackMaster.PoPacknum + ' / ' + PoPackMaster.BuyerPO, PoPackMaster.PoPacknum, PoPackMaster.BuyerPO, PoPackMaster.PoPackId, POPackDetails.OurStyleID, AtcDetails.OurStyle, 
                                                    AtcDetails.BuyerStyle, PoPackMaster.AtcId, POPackDetails.IsPackable, PoPackMaster.DeliveryDate, PoPackMaster.SeasonName, ChannelMaster.ChannelName, 
                                                    BuyerDestinationMaster.BuyerDestination, AtcMaster.AtcNum, GarmentCategory.CategoryName, GarmentCategory.CategoryID, ChannelMaster.ChannelID, 
                                                    BuyerDestinationMaster.BuyerDestination_PK, SeasonMaster.Season_PK, BuyerMaster.BuyerID, BuyerMaster.BuyerName, PoPackMaster.PackingInstruction, PoPackMaster.ExpectedLocation_PK, 
                                                    LocationMaster_1.LocationName, PoPackMaster.HandoverDate
                          HAVING         (PoPackMaster.AtcId = @Param1) AND (SUM(POPackDetails.PoQty) &gt; 0)
                          ORDER BY PoPackMaster.PoPackId) AS tttt">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="cmb_atc" Name="Param1" PropertyName="SelectedValue" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                    <asp:SqlDataSource ID="SeasonDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT distinct  SeasonName FROM PoPackMaster WHERE (AtcId = @Param1)">
                                    <SelectParameters>
                                        <asp:ControlParameter ControlID="cmb_atc" Name="Param1" PropertyName="SelectedValue" />
                                    </SelectParameters>
                                </asp:SqlDataSource>
                                <asp:SqlDataSource ID="StyledataSource" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT (OurStyle+'/'+ BuyerStyle) as Style , OurStyleID, AtcId FROM AtcDetails WHERE (AtcId = @Param1)">
                                    <SelectParameters>
                                        <asp:ControlParameter ControlID="cmb_atc" Name="Param1" PropertyName="SelectedValue" />
                                    </SelectParameters>
                                </asp:SqlDataSource> 
     <asp:SqlDataSource ID="FactoryData" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT DISTINCT LocationMaster.LocationName, LocationMaster.Location_PK, PoPackMaster.AtcId FROM PoPackMaster INNER JOIN LocationMaster ON PoPackMaster.ExpectedLocation_PK = LocationMaster.Location_PK WHERE (PoPackMaster.AtcId = @Param1)">
                                    <SelectParameters>
                                        <asp:ControlParameter ControlID="cmb_atc" Name="Param1" PropertyName="SelectedValue" />
                                    </SelectParameters>
                                </asp:SqlDataSource>
        <br />
                    
    </div>

                                  


           
    </div>
    </form>
</body>
</html>

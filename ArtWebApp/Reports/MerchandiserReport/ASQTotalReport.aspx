<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="ASQTotalReport.aspx.cs" Inherits="ArtWebApp.Reports.MerchandiserReport.ASQTotalReport" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register Assembly="Infragistics35.Web.v12.1, Version=12.1.20121.2236, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
              .headerclass
        {
 background: #fcfcfc;
border-top-color: #c8c8c8;
border-left-color: #c8c8c8;
border-bottom-color: #179bd7;
border-width: 0 0 0 1px;
border-style: solid;
height: 40px;
padding: 0 .5em;
text-overflow: ellipsis;
white-space: nowrap;
text-align: left;
color:black;
font-weight: bold;
font-size: 14px;



line-height: 29px;
margin: -7px;
padding: 0 .7em;
text-align: left;
white-space: nowrap;

        }
   
.rowcell {
    border-width: 1px 0 0 1px;
    padding: .7em;
    line-height: 14px;
    white-space: nowrap;
    width: auto;
    vertical-align: middle;
    color:black;
}
     
      
    </style>

      <script type="text/javascript">

          function CheckChangedCountry(e) {
              debugger;
            var value="";
            var wdd = $find('<%= drp_country.ClientID %>');
            for (var i = 0; i < wdd.get_items().getLength(); i++) {

                if (e.target.checked) {
                 value+= wdd.get_items().getItem(i).get_text()+", ";
                    wdd.get_items().getItem(i).select(false);
                }
                else {
                    wdd.get_items().getItem(i).unselect(false);
                }
            
            }

            wdd.set_currentValue(value,true);
            wdd.closeDropDown();
        }
function CheckChangedFactory(e) {
              debugger;
            var value="";
            var wdd = $find('<%= drp_factory.ClientID %>');
            for (var i = 0; i < wdd.get_items().getLength(); i++) {

                if (e.target.checked) {
                 value+= wdd.get_items().getItem(i).get_text()+", ";
                    wdd.get_items().getItem(i).select(false);
                }
                else {
                    wdd.get_items().getItem(i).unselect(false);
                }
            
            }

            wdd.set_currentValue(value,true);
            wdd.closeDropDown();
}


function CheckChangedBuyer(e) {
              debugger;
            var value="";
            var wdd = $find('<%= drp_buyer.ClientID %>');
            for (var i = 0; i < wdd.get_items().getLength(); i++) {

                if (e.target.checked) {
                 value+= wdd.get_items().getItem(i).get_text()+", ";
                    wdd.get_items().getItem(i).select(false);
                }
                else {
                    wdd.get_items().getItem(i).unselect(false);
                }
            
            }

            wdd.set_currentValue(value,true);
            wdd.closeDropDown();
}


          function CheckChangedATC(e) {
              debugger;
            var value="";
            var wdd = $find('<%= drp_atc.ClientID %>');
            for (var i = 0; i < wdd.get_items().getLength(); i++) {

                if (e.target.checked) {
                 value+= wdd.get_items().getItem(i).get_text()+", ";
                    wdd.get_items().getItem(i).select(false);
                }
                else {
                    wdd.get_items().getItem(i).unselect(false);
                }
            
            }

            wdd.set_currentValue(value,true);
            wdd.closeDropDown();
}
          




    </script>

  
    <link href="../../css/style.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="RedHeaddingdIV">

        ASQ STATUS REPORT</div>
    <div>

        

        <table class="FullTable">
            <tr>
               <td class="NormalTD">Country</td>
               <td class="NormalTD">
                    <asp:UpdatePanel ID="upd_item0" UpdateMode="Conditional"  runat="server">
                        <ContentTemplate>
                            <ig:WebDropDown ID="drp_country" runat="server" CurrentValue="Select Country" EnableMultipleSelection="true" EnableClosingDropDownOnSelect="False"  TextField="Description" ValueField="Template_pk" Width="200px" AutoPostBack="True" OnSelectionChanged="drp_country_SelectionChanged">
                                <Items>
                                   
                                    <ig:DropDownItem Selected="False" Text="Egypt" Value="6">
                                    </ig:DropDownItem>
                                    <ig:DropDownItem Selected="False" Text="Ethiopia" Value="29">
                                    </ig:DropDownItem>
                                    <ig:DropDownItem Selected="False" Text="Kenya" Value="9">
                                    </ig:DropDownItem>
                                </Items>
                                <HeaderTemplate>
                                     <input type="checkbox" id="chkBox" onchange="return CheckChangedCountry(event);" />Select All
                                </HeaderTemplate>
                                <DropDownItemBinding TextField="Description" ValueField="Template_pk" />
                            </ig:WebDropDown>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
                <td class="SearchButtonTD">
                     <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional"  runat="server">
                        <ContentTemplate>
                    <asp:Button ID="BTN_CTRY" runat="server" Text="s" OnClick="BTN_CTRY_Click" />
                            </ContentTemplate>
                    </asp:UpdatePanel>

                </td>
                <td class="NormalTD">&nbsp;</td>
                <td class="SearchButtonTD">&nbsp;</td>
            </tr>
            <tr>
               <td class="NormalTD">Factory</td>
               <td class="NormalTD">
                    <asp:UpdatePanel ID="upd_factory" UpdateMode="Conditional" runat="server">
                        <ContentTemplate>
                            <ig:WebDropDown ID="drp_factory" runat="server" CurrentValue="Select Factory" EnableClosingDropDownOnSelect="False" EnableMultipleSelection="True" TextField="Name" ValueField="PK" Width="200px" OnSelectionChanged="drp_factory_SelectionChanged">
                                 <HeaderTemplate>
                                     <input type="checkbox" id="chkBox" onchange="return CheckChangedFactory(event);" />Select All
                                </HeaderTemplate>
                                <DropDownItemBinding TextField="Name" ValueField="PK" />

                            </ig:WebDropDown>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
                <td class="SearchButtonTD">
                     <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Conditional" runat="server">
                        <ContentTemplate>
                    <asp:Button ID="btn_fact" runat="server" Text="s" OnClick="btn_fact_Click" />
                            </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
                <td class="NormalTD">&nbsp;</td>
                <td class="SearchButtonTD">&nbsp;</td>
            </tr>
            <tr>
               <td class="NormalTD">Buyer</td>
               <td class="NormalTD">
                    <asp:UpdatePanel ID="upd_buyer"  UpdateMode="Conditional" runat="server">
                        <ContentTemplate>
                            <ig:WebDropDown ID="drp_buyer" runat="server" CurrentValue="Select Buyer" DataSourceID="BuyerDatasource" EnableClosingDropDownOnSelect="False" EnableMultipleSelection="True" TextField="Name" ValueField="PK" Width="200px">
                               <HeaderTemplate>
                                     <input type="checkbox" id="chkBox" onchange="return CheckChangedBuyer(event);" />Select All
                                </HeaderTemplate>   <DropDownItemBinding TextField="BuyerName" ValueField="BuyerID" />
                            </ig:WebDropDown>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
                <td class="SearchButtonTD">
                     <asp:UpdatePanel ID="UpdatePanel3"  UpdateMode="Conditional" runat="server">
                        <ContentTemplate>
                    <asp:Button ID="btn_buyer" runat="server" Text="s" OnClick="btn_buyer_Click1" />
                            </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
                <td class="NormalTD">
                    <ig:WebExcelExporter ID="WebExcelExporter1" runat="server">
                    </ig:WebExcelExporter>
                </td>
                <td class="SearchButtonTD">&nbsp;</td>
            </tr>
            <tr>
               <td class="NormalTD">Atc</td>
             
               <td class="NormalTD">
                    <asp:UpdatePanel ID="upd_atc"  UpdateMode="Conditional" runat="server">
                        <ContentTemplate>
                            <ig:WebDropDown ID="drp_atc" runat="server" CurrentValue="Select Atc" EnableClosingDropDownOnSelect="False" EnableMultipleSelection="True"  TextField="Name" ValueField="PK" Width="200px" OnSelectionChanged="drp_atc_SelectionChanged">
                               <HeaderTemplate>
                                     <input type="checkbox" id="chkBox" onchange="return CheckChangedATC(event);" />Select All
                                </HeaderTemplate>                             
                                
                                 <DropDownItemBinding TextField="Name" ValueField="PK" />
                            </ig:WebDropDown>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
                <td class="SearchButtonTD">
                     <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                    </asp:UpdatePanel>
                </td>
                <td class="NormalTD">
                    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Open  All ASQ Report of Selected Atc" />
                </td>
                <td class="SearchButtonTD">&nbsp;</td>
               <td class="NormalTD"></td>
            </tr>
            <tr>
               <td class="NormalTD">HD Between </td>
                <td class="NormalTD">
                    <asp:TextBox ID="dtp_deliverydate" runat="server" Width="180px"></asp:TextBox>
                    <asp:CalendarExtender ID="dtp_deliverydate_CalendarExtender" runat="server" Enabled="True" Format="dd/MMM/yyyy" TargetControlID="dtp_deliverydate">
                                    </asp:CalendarExtender>
                </td>
                <td class="SearchButtonTD">And </td>
                <td class="NormalTD">
                    <asp:TextBox ID="dtp_toHD" runat="server" Width="180px"></asp:TextBox>
                    <asp:CalendarExtender ID="dtp_deliverydate_CalendarExtender0" runat="server" Enabled="True" Format="dd/MMM/yyyy" TargetControlID="dtp_toHD">
                                    </asp:CalendarExtender>
                </td>
                <td class="SearchButtonTD">
                    <asp:Button ID="btn_hD" runat="server"  Text="s" OnClick="btn_hD_Click" />
                </td>
            </tr>
            <tr>
               <td class="NormalTD">
                   <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Export to Excel" />
                </td>
                <td class="NormalTD">
                    &nbsp;</td>
                <td class="SearchButtonTD">&nbsp;</td>
                <td class="NormalTD">
                    &nbsp;</td>
                <td class="SearchButtonTD">
                    &nbsp;</td>
            </tr>
        </table>

        

    </div>
    <div>
        <asp:SqlDataSource ID="ASQData" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT        TTT.PoPackId, TTT.OurStyleID, TTT.SeasonName, TTT.BuyerName, TTT.AtcNum, TTT.PoPacknum, TTT.BuyerPO, TTT.OurStyle, TTT.BuyerStyle, TTT.POQty, TTT.ShipedQty, TTT.Balance, TTT.FirstDeliveryDate, 
                         TTT.HandoverDate, TTT.DeliveryDate, TTT.LocationName, TTT.Iscuttable,ISNULL( SUM(ProductionStatusAtcWorld_Tbl.CutQty),0) AS CutQty, isnull( SUM(ProductionStatusAtcWorld_Tbl.SewingQty),0) AS SewingQty, 
                       isnull(   SUM(ProductionStatusAtcWorld_Tbl.SortingQty),0) AS SortingQty, isnull( SUM(ProductionStatusAtcWorld_Tbl.WashQty),0) AS WashQty, isnull( SUM(ProductionStatusAtcWorld_Tbl.FinishQty),0) AS FinishQty, TTT.IsShortClosed
FROM            (SELECT        tt.PoPackId, tt.OurStyleID, tt.SeasonName, BuyerMaster.BuyerName, AtcMaster.AtcNum, tt.PoPacknum, tt.BuyerPO, tt.OurStyle, tt.BuyerStyle, tt.POQty, tt.ShipedQty, tt.POQty - tt.ShipedQty AS Balance,
                                                     tt.FirstDeliveryDate, tt.HandoverDate, tt.DeliveryDate,
                                                        (SELECT DISTINCT LocationMaster.LocationName
                                                          FROM            ASQAllocationMaster INNER JOIN
                                                                                    LocationMaster ON ASQAllocationMaster.Locaion_PK = LocationMaster.Location_PK
                                                          WHERE        (ASQAllocationMaster.PoPackId = tt.PoPackId) AND (ASQAllocationMaster.OurStyleId = tt.OurStyleID)) AS LocationName, ISNULL(tt.IsCutable, 'N') AS Iscuttable, tt.IsShortClosed
                          FROM            (SELECT        PoPackMaster.PoPackId, PoPackMaster.PoPacknum, PoPackMaster.BuyerPO, AtcDetails.OurStyle, AtcDetails.BuyerStyle, SUM(POPackDetails_1.PoQty) AS POQty, ISNULL
                                                                                  ((SELECT        SUM(ShipmentHandOverDetails.ShippedQty) AS Expr1
                                                                                      FROM            ShipmentHandOverDetails INNER JOIN
                                                                                                               JobContractDetail ON ShipmentHandOverDetails.JobContractDetail_pk = JobContractDetail.JobContractDetail_pk
                                                                                      GROUP BY JobContractDetail.PoPackID, JobContractDetail.OurStyleID
                                                                                      HAVING        (JobContractDetail.PoPackID = PoPackMaster.PoPackId) AND (JobContractDetail.OurStyleID = POPackDetails_1.OurStyleID)), 0) AS ShipedQty, AtcDetails.OurStyleID, 
                                                                              PoPackMaster.FirstDeliveryDate, PoPackMaster.DeliveryDate, PoPackMaster.AtcId, PoPackMaster.HandoverDate, MAX(POPackDetails_1.IsCutable) AS IsCutable, 
                                                                              PoPackMaster.SeasonName, MAX(POPackDetails_1.IsShortClosed) AS IsShortClosed
                                                    FROM            PoPackMaster INNER JOIN
                                                                              POPackDetails AS POPackDetails_1 ON PoPackMaster.PoPackId = POPackDetails_1.POPackId INNER JOIN
                                                                              AtcDetails ON POPackDetails_1.OurStyleID = AtcDetails.OurStyleID
            WHERE (PoPackMaster.AtcId = 1)
                                                    GROUP BY PoPackMaster.PoPackId, PoPackMaster.PoPacknum, PoPackMaster.BuyerPO, AtcDetails.OurStyle, AtcDetails.BuyerStyle, POPackDetails_1.OurStyleID, AtcDetails.OurStyleID, 
                                                                              PoPackMaster.FirstDeliveryDate, PoPackMaster.DeliveryDate, PoPackMaster.AtcId, PoPackMaster.HandoverDate, PoPackMaster.SeasonName) AS tt INNER JOIN
                                                    AtcMaster ON tt.AtcId = AtcMaster.AtcId INNER JOIN
                                                    BuyerMaster ON AtcMaster.Buyer_ID = BuyerMaster.BuyerID
                        ) AS TTT LEFT OUTER JOIN
                         ProductionStatusAtcWorld_Tbl ON TTT.PoPackId = ProductionStatusAtcWorld_Tbl.POPackID AND TTT.OurStyleID = ProductionStatusAtcWorld_Tbl.OurStyleId
GROUP BY TTT.PoPackId, TTT.OurStyleID, TTT.SeasonName, TTT.BuyerName, TTT.AtcNum, TTT.PoPacknum, TTT.BuyerPO, TTT.OurStyle, TTT.BuyerStyle, TTT.POQty, TTT.ShipedQty, TTT.Balance, TTT.FirstDeliveryDate, 
                         TTT.HandoverDate, TTT.DeliveryDate, TTT.LocationName, TTT.Iscuttable, TTT.IsShortClosed">
        </asp:SqlDataSource>
            <ig:WebDataGrid ID="WebDataGrid1" runat="server" AutoGenerateColumns="False" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" CellSpacing="1" ClientIDMode="Static" DataMember="DefaultView" DefaultColumnWidth="120px" Font-Bold="True" ForeColor="#000099" HeaderCaptionCssClass="headerclass" Height="100%" ItemCssClass="rowcell" DataSourceID="ASQData">
                <Columns>
                    <ig:BoundDataField DataFieldName="PoPackId" Key="PoPackId">
                        <Header Text="PoPackId">
                        </Header>
                    </ig:BoundDataField>
                    <ig:BoundDataField DataFieldName="OurStyleID" Key="OurStyleID">
                        <Header Text="OurStyleID">
                        </Header>
                    </ig:BoundDataField>
                    <ig:BoundDataField DataFieldName="SeasonName" Key="SeasonName">
                        <Header Text="SeasonName">
                        </Header>
                    </ig:BoundDataField>
                    <ig:BoundDataField DataFieldName="BuyerName" Key="BuyerName">
                        <Header Text="BuyerName">
                        </Header>
                    </ig:BoundDataField>
                    <ig:BoundDataField DataFieldName="AtcNum" Key="AtcNum">
                        <Header Text="AtcNum">
                        </Header>
                    </ig:BoundDataField>
                    <ig:BoundDataField DataFieldName="PoPacknum" Key="PoPacknum">
                        <Header Text="PoPacknum">
                        </Header>
                    </ig:BoundDataField>
                    <ig:BoundDataField DataFieldName="BuyerPO" Key="BuyerPO">
                        <Header Text="BuyerPO">
                        </Header>
                    </ig:BoundDataField>
                    <ig:BoundDataField DataFieldName="OurStyle" Key="OurStyle">
                        <Header Text="OurStyle">
                        </Header>
                    </ig:BoundDataField>
                    <ig:BoundDataField DataFieldName="BuyerStyle" Key="BuyerStyle">
                        <Header Text="BuyerStyle">
                        </Header>
                    </ig:BoundDataField>
                    <ig:BoundDataField DataFieldName="POQty" Key="POQty">
                        <Header Text="POQty">
                        </Header>
                    </ig:BoundDataField>
                    <ig:BoundDataField DataFieldName="ShipedQty" Key="ShipedQty">
                        <Header Text="ShipedQty">
                        </Header>
                    </ig:BoundDataField>
                    <ig:BoundDataField DataFieldName="Balance" Key="Balance">
                        <Header Text="Balance">
                        </Header>
                    </ig:BoundDataField>
                    <ig:BoundDataField DataFieldName="FirstDeliveryDate" Key="FirstDeliveryDate">
                        <Header Text="FirstDeliveryDate">
                        </Header>
                    </ig:BoundDataField>
                    <ig:BoundDataField DataFieldName="HandoverDate" Key="HandoverDate">
                        <Header Text="HandoverDate">
                        </Header>
                    </ig:BoundDataField>
                    <ig:BoundDataField DataFieldName="DeliveryDate" Key="DeliveryDate">
                        <Header Text="DeliveryDate">
                        </Header>
                    </ig:BoundDataField>
                    <ig:BoundDataField DataFieldName="LocationName" Key="LocationName">
                        <Header Text="LocationName">
                        </Header>
                    </ig:BoundDataField>
                    <ig:BoundDataField DataFieldName="Iscuttable" Key="Iscuttable">
                        <Header Text="Iscuttable">
                        </Header>
                    </ig:BoundDataField>
                    <ig:BoundDataField DataFieldName="CutQty" Key="CutQty">
                        <Header Text="CutQty">
                        </Header>
                    </ig:BoundDataField>
                    <ig:BoundDataField DataFieldName="SewingQty" Key="SewingQty">
                        <Header Text="SewingQty">
                        </Header>
                    </ig:BoundDataField>
                    <ig:BoundDataField DataFieldName="SortingQty" Key="SortingQty">
                        <Header Text="SortingQty">
                        </Header>
                    </ig:BoundDataField>
                    <ig:BoundDataField DataFieldName="WashQty" Key="WashQty">
                        <Header Text="WashQty">
                        </Header>
                    </ig:BoundDataField>
                    <ig:BoundDataField DataFieldName="FinishQty" Key="FinishQty">
                        <Header Text="FinishQty">
                        </Header>
                    </ig:BoundDataField>
                    <ig:BoundDataField DataFieldName="IsShortClosed" Key="IsShortClosed">
                        <Header Text="IsShortClosed">
                        </Header>
                    </ig:BoundDataField>
                </Columns>
        <behaviors>
            <ig:Filtering FilterType="ExcelStyleFilter">
            </ig:Filtering>
            <ig:SummaryRow>
                <columnsummaries>
                    <ig:ColumnSummaryInfo ColumnKey="POQty">
                        <summaries>
                            <ig:Summary CustomSummaryName="Sum" SummaryType="Sum" />
                         
                        </summaries>
                    </ig:ColumnSummaryInfo>
                    <ig:ColumnSummaryInfo ColumnKey="ShipedQty">
                        <summaries>
                            <ig:Summary CustomSummaryName="Sum" SummaryType="Sum" />
                        </summaries>
                    </ig:ColumnSummaryInfo>
                </columnsummaries>
            </ig:SummaryRow>
            <ig:Paging PageSize="25">
            </ig:Paging>
            <ig:Sorting>
            </ig:Sorting>
            <ig:ColumnMoving>
            </ig:ColumnMoving>
        </behaviors>
</ig:WebDataGrid>
            <asp:SqlDataSource ID="BuyerDatasource" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT [BuyerID], [BuyerName] FROM [BuyerMaster]"></asp:SqlDataSource>
            <asp:SqlDataSource ID="atcdatasource" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT [AtcId] as PK, [AtcNum]  as name FROM [AtcMaster]"></asp:SqlDataSource>
            <br />
    
    </div>

    </asp:Content>

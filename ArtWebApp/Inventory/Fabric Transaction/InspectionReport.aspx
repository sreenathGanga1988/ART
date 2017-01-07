<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="InspectionReport.aspx.cs" Inherits="ArtWebApp.Inventory.Fabric_Transaction.InspectionReport" %>
<%@ Register assembly="DropDownChosen" namespace="CustomDropDown" tagprefix="ucc" %>
<%@ Register assembly="Infragistics35.Web.v12.1, Version=12.1.20121.2236, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.GridControls" tagprefix="ig" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../css/style.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  
        <table class="DataEntryTable">
            <tr>
                <td class="RedHeadding" colspan="4">


                    iNSPECTION REPORT</td>
            </tr>
            <tr>
                <td class="NormalTD">


                    aTC # :


                </td>
                <td class="NormalTD">


                   <asp:UpdatePanel ID="upd_atc"  runat="server">
                                                <ContentTemplate>
                                                <ucc:DropDownListChosen ID="drp_atc" runat="server" Height="25px" Width="170px" DataSourceID="atcdata" DataTextField="AtcNum" DataValueField="AtcId" DisableSearchThreshold="10" style="text-align: left" OnSelectedIndexChanged="drp_atc_SelectedIndexChanged" AutoPostBack="True">
                        </ucc:DropDownListChosen>
                                                   </ContentTemplate>
                                            </asp:UpdatePanel>


                </td>
                <td class="NormalTD">


                    <asp:Button ID="Button1" runat="server" Text="S" OnClick="Button1_Click" />


                </td>
                <td class="NormalTD">


                </td>
            </tr>
            <tr>
                <td class="NormalTD" colspan="4">
 <asp:UpdatePanel ID="upd_grid"   runat="server">
                            <ContentTemplate>
                                <ig:WebDataGrid ID="tbl_InverntoryDetails" runat="server" AutoGenerateColumns="False" Height="350px" Width="100%" BorderColor="Blue" BorderStyle="Solid" BorderWidth="1px" CellSpacing="1" DefaultColumnWidth="100px">
                        <Columns>
                            <ig:BoundDataField DataFieldName="Roll_PK" Key="Roll_PK">
                                <Header Text="Roll_PK">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="RollNum" Key="RollNum">
                                <Header Text="RollNum">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="MrnNum" Key="MrnNum">
                                <Header Text="MrnNum">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="Qty" Key="Qty">
                                <Header Text="Qty">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="UOM" Key="UOM">
                                <Header Text="UOM">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="SShrink" Key="SShrink">
                                <Header Text="SShrink">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="SYard" Key="SYard">
                                <Header Text="SYard">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="SShade" Key="SShade">
                                <Header Text="SShade">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="SWidth" Key="SWidth">
                                <Header Text="SWidth">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="AShrink" Key="AShrink">
                                <Header Text="AShrink">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="AShade" Key="AShade">
                                <Header Text="AShade">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="AWidth" Key="AWidth">
                                <Header Text="AWidth">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="AYard" Key="AYard">
                                <Header Text="AYard">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="IsSaved" Key="IsSaved">
                                <Header Text="IsSaved">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="IsApproved" Key="IsApproved">
                                <Header Text="IsApproved">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="IsAcceptable" Key="IsAcceptable">
                                <Header Text="IsAcceptable">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="MarkerType" Key="MarkerType">
                                <Header Text="MarkerType">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="PONum" Key="PONum">
                                <Header Text="PONum">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="SupplierName" Key="SupplierName">
                                <Header Text="SupplierName">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="AtcNum" Key="AtcNum">
                                <Header Text="AtcNum">
                                </Header>
                            </ig:BoundDataField>
                        </Columns>
                        <Behaviors>
                            <ig:Filtering>
                            </ig:Filtering>
                        </Behaviors>
                    </ig:WebDataGrid>

                                </ContentTemplate>

     </asp:UpdatePanel>

                    
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT FabricRollmaster.Roll_PK, FabricRollmaster.RollNum, MrnMaster.MrnNum, FabricRollmaster.Qty, FabricRollmaster.UOM, FabricRollmaster.SShrink, FabricRollmaster.SYard, FabricRollmaster.SShade, FabricRollmaster.SWidth, FabricRollmaster.AShrink, FabricRollmaster.AShade, FabricRollmaster.AWidth, FabricRollmaster.AYard, FabricRollmaster.IsSaved, FabricRollmaster.IsApproved, FabricRollmaster.IsAcceptable, FabricRollmaster.MarkerType, ProcurementMaster.PONum, SupplierMaster.SupplierName, AtcMaster.AtcNum, AtcMaster.AtcId FROM MrnDetails INNER JOIN MrnMaster ON MrnDetails.Mrn_PK = MrnMaster.Mrn_PK INNER JOIN FabricRollmaster ON MrnDetails.MrnDet_PK = FabricRollmaster.MRnDet_PK INNER JOIN ProcurementMaster ON MrnMaster.Po_PK = ProcurementMaster.PO_Pk INNER JOIN SupplierMaster ON ProcurementMaster.Supplier_Pk = SupplierMaster.Supplier_PK INNER JOIN AtcMaster ON ProcurementMaster.AtcId = AtcMaster.AtcId WHERE (AtcMaster.AtcId = @Param1)">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="drp_atc" DefaultValue="0" Name="Param1" PropertyName="SelectedValue" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </td>
            </tr>
            <tr>
                <td class="NormalTD">


                    &nbsp;</td>
                <td class="NormalTD">


                    &nbsp;</td>
                <td class="NormalTD">


                    &nbsp;</td>
                <td class="NormalTD">


                    &nbsp;</td>
            </tr>
        </table>

        <asp:SqlDataSource ID="atcdata" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" 
                    SelectCommand="SELECT DISTINCT AtcNum, AtcId FROM AtcMaster WHERE (IsClosed = N'N') ORDER BY AtcNum, AtcId">
                </asp:SqlDataSource>
   
</asp:Content>

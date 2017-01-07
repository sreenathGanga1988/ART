<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="PendingReports.aspx.cs" Inherits="ArtWebApp.Reports.Qualityreports.PendingReports" %>
<%@ Register assembly="Infragistics35.Web.v12.1, Version=12.1.20121.2236, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.GridControls" tagprefix="ig" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
    

    </style>
    <link href="../../css/style.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="FullTable">
    <tr>
        <td>
             <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>

            <table class="DataEntryTable">
                <tr>
                    <td class="RedHeadding" colspan="12">asn&nbsp; Reports</td>
                </tr>
                <tr>
                <td class="NormalTD">


                    aTC # :


                </td>
                <td class="NormalTD">


                   <asp:UpdatePanel ID="upd_atc"  runat="server">
                                                <ContentTemplate>
                                                <ucc:DropDownListChosen ID="drp_atc" runat="server" Height="25px" Width="170px" DataSourceID="atcdata" DataTextField="AtcNum" DataValueField="AtcId" DisableSearchThreshold="10" style="text-align: left" >
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
                    <td class="NormalTD"  >
                        supplier invoice /ASN #:</td>
                    <td class="NormalTD" >
                             
                              <asp:UpdatePanel ID="UPD_ASN" UpdateMode="Conditional" runat="server">
                                                <ContentTemplate>
                                                <ucc:DropDownListChosen ID="drp_asn" runat="server" Height="25px" Width="170px" DisableSearchThreshold="10">
                        </ucc:DropDownListChosen>
                                                   </ContentTemplate>
                                            </asp:UpdatePanel>
                       </td>
                    <td class="NormalTD"  >
                     <asp:UpdatePanel ID="upd_btn" UpdateMode="Conditional" runat="server">
                                                <ContentTemplate>
                       <asp:Button ID="btn_Asn" runat="server" Text="S" Width="33px"  CssClass="auto-style10" OnClick="btn_Asn_Click" /></td>
                     </ContentTemplate>
                                            </asp:UpdatePanel>  </td>
                     </td>
                    <td class="NormalTD"  >
                        </td>
                </tr>
                
                <tr>
                    <td class="NormalTD" >
                             
                       &nbsp;</td>
                    <td class="NormalTD"  >
                        Fabric Details :<asp:UpdatePanel ID="upd_color" UpdateMode="Conditional" runat="server">
                                                <ContentTemplate>
                                                <ucc:DropDownListChosen ID="drp_color" runat="server" Height="25px" Width="200px" DisableSearchThreshold="10">
                        </ucc:DropDownListChosen>
                                                   </ContentTemplate>
                                            </asp:UpdatePanel>
                    </td>
                    <td class="NormalTD"  >
                         <asp:UpdatePanel ID="upd_btn_po" UpdateMode="Conditional" runat="server">
                                                <ContentTemplate>
                       <asp:Button ID="btn_fabric" runat="server" Text="S" Width="33px"  CssClass="auto-style10" OnClick="btn_fabric_Click1" /></td>
                     </ContentTemplate>
                                            </asp:UpdatePanel>  
                        </td>
                    <td class="NormalTD"  >
                    </td>
                    <td class="NormalTD"  >
                        </td>
                </tr>
                <tr>
                    <td class="NormalTD">
                        <asp:Button ID="Button2" runat="server" Text="Inspection Pending" OnClick="Button2_Click" />
                    </td>
                    <td class="NormalTD">
                        <asp:Button ID="Button3" runat="server" Text="Validation Pending" OnClick="Button3_Click" />
                    </td>
                    <td class="NormalTD">&nbsp;</td>
                    <td class="NormalTD">&nbsp;</td>
                    <td class="NormalTD">&nbsp;</td>
                </tr>
            </table>


                       </ContentTemplate>

            </asp:UpdatePanel>

        </td>
    </tr>
    <tr>
        <td class="ReportViewSection">
            <ig:WebDataGrid ID="WebDataGrid1" DataKeyFields="Roll_PK" runat="server" Height="50%" Width="100%" CellSpacing="1" AutoGenerateColumns="False" DataSourceID="SqlDataSource2">
                        <Columns>
                            <ig:BoundDataField DataFieldName="AtcNum" Key="AtcNum" Width="45px">
                                <Header Text="AtcNum">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="itemDescription" Key="itemDescription" Width="300px">
                                <Header Text="itemDescription">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="RollNum" Key="RollNum" Width="80px">
                                <Header Text="RollNum">
                                </Header>
                            </ig:BoundDataField>
                             <ig:BoundDataField DataFieldName="SupplierDocnum" Key="SupplierDocnum" Width="100px">
                                <Header Text="SupplierDocnum">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="Remark" Key="Remark" Width="80px">
                                <Header Text="Remark">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="SShrink" Key="SShrink" Width="40px">
                                <Header Text="SShrink">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="AShrink" Key="AShrink" Width="40px">
                                <Header Text="AShrink">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="SYard" Key="SYard" Width="40px">
                                <Header Text="SYard">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="AYard" Key="AYard" Width="40px">
                                <Header Text="AYard">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="SShade" Key="SShade" Width="40px">
                                <Header Text="SShade">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="AShade" Key="AShade" Width="40px">
                                <Header Text="AShade">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="SWidth" Key="SWidth" Width="40px">
                                <Header Text="SWidth">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="AWidth" Key="AWidth" Width="40px">
                                <Header Text="AWidth">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="SGsm" Key="SGsm" Width="40px">
                                <Header Text="SGsm">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="AGsm" Key="AGsm" Width="40px">
                                <Header Text="AGsm">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="SWeight" Key="SWeight" Width="40px">
                                <Header Text="SWeight">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="LOTnum" Key="LOTnum" Width="40px">
                                <Header Text="LOTnum">
                                </Header>
                            </ig:BoundDataField>
                           
                            <ig:BoundDataField DataFieldName="PONum" Key="PONum" Width="40px">
                                <Header Text="PONum">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="IsSaved" Key="IsSaved" Width="10px">
                                <Header Text="IsSaved">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="IsApproved" Key="IsApproved" Width="10px">
                                <Header Text="IsApproved">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="IsAcceptable" Key="IsAcceptable" Width="40px">
                                <Header Text="IsAcceptable">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="MarkerType" Key="MarkerType" Width="40px">
                                <Header Text="MarkerType">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="WidthGroup" Key="WidthGroup" Width="40px">
                                <Header Text="WidthGroup">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="ShadeGroup" Key="ShadeGroup" Width="40px">
                                <Header Text="ShadeGroup">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="ShrinkageGroup" Key="ShrinkageGroup" Width="40px">
                                <Header Text="ShrinkageGroup">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="TotalDefect" Key="TotalDefect" Width="40px">
                                <Header Text="TotalDefect">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="TotalDefecton100" Key="TotalDefecton100" Width="40px">
                                <Header Text="TotalDefecton100">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="TotalPoint" Key="TotalPoint" Width="40px">
                                <Header Text="TotalPoint">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="TotalPointon100yard" Key="TotalPointon100yard" Width="40px">
                                <Header Text="TotalPointon100yard">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="Roll_PK" Key="Roll_PK">
                                <Header Text="Roll_PK">
                                </Header>
                            </ig:BoundDataField>
                        </Columns>
                        <Behaviors>
                            <ig:Filtering>
                            </ig:Filtering>
                        </Behaviors>
                    </ig:WebDataGrid>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT        AtcMaster.AtcNum, ISNULL(SkuRawMaterialMaster.Composition, ' ') + ' ' + ISNULL(SkuRawMaterialMaster.Construction, ' ') + ' ' + ISNULL(SkuRawMaterialMaster.Weight, ' ') 
                         + ' ' + ISNULL(SkuRawMaterialMaster.Width, ' ') + ' ' + ISNULL(SkuRawmaterialDetail.ItemColor, ' ') + ' ' + ISNULL(SkuRawmaterialDetail.ItemSize, ' ') + ' ' + ISNULL(ProcurementDetails.SupplierSize, ' ') 
                         + ' ' + ISNULL(ProcurementDetails.SupplierColor, ' ') AS itemDescription, FabricRollmaster.RollNum, FabricRollmaster.Remark, FabricRollmaster.SShrink, FabricRollmaster.AShrink, FabricRollmaster.SYard, 
                         FabricRollmaster.AYard, FabricRollmaster.SShade, FabricRollmaster.AShade, FabricRollmaster.SWidth, FabricRollmaster.AWidth, FabricRollmaster.SGsm, FabricRollmaster.AGsm, FabricRollmaster.SWeight, 
                         FabricRollmaster.LOTnum, SupplierDocumentMaster.SupplierDocnum, ProcurementMaster.PONum, FabricRollmaster.IsSaved, FabricRollmaster.IsApproved, FabricRollmaster.IsAcceptable, 
                         FabricRollmaster.MarkerType, FabricRollmaster.WidthGroup, FabricRollmaster.ShadeGroup, FabricRollmaster.ShrinkageGroup, FabricRollmaster.TotalDefect, FabricRollmaster.TotalDefecton100, 
                         FabricRollmaster.TotalPoint, FabricRollmaster.TotalPointon100yard, FabricRollmaster.Roll_PK
FROM            SkuRawmaterialDetail INNER JOIN
                         SkuRawMaterialMaster ON SkuRawmaterialDetail.Sku_PK = SkuRawMaterialMaster.Sku_Pk INNER JOIN
                         FabricRollmaster INNER JOIN
                         SupplierDocumentMaster ON FabricRollmaster.SupplierDoc_pk = SupplierDocumentMaster.SupplierDoc_pk ON SkuRawmaterialDetail.SkuDet_PK = FabricRollmaster.SkuDet_PK INNER JOIN
                         ProcurementDetails ON FabricRollmaster.podet_pk = ProcurementDetails.PODet_PK INNER JOIN
                         ProcurementMaster ON ProcurementDetails.PO_Pk = ProcurementMaster.PO_Pk INNER JOIN
                         AtcMaster ON SkuRawMaterialMaster.Atc_id = AtcMaster.AtcId"></asp:SqlDataSource>
        </td>
    </tr>
    <tr>
        <td><asp:SqlDataSource ID="atcdata" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" 
                    SelectCommand="SELECT DISTINCT AtcNum, AtcId FROM AtcMaster WHERE (IsClosed = N'N') ORDER BY AtcNum, AtcId">
                </asp:SqlDataSource>
            <asp:HiddenField ID="HiddenField1" runat="server" />
            <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="GetQualityPendingReport_SP" SelectCommandType="StoredProcedure">
                <SelectParameters>
                    <asp:ControlParameter ControlID="HiddenField1" DefaultValue="0" Name="Pending" PropertyName="Value" Type="String" />
                </SelectParameters>
            </asp:SqlDataSource>
        </td>
    </tr>
</table>
</asp:Content>


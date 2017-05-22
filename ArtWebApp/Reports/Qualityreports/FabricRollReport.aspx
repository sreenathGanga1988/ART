<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FabricRollReport.aspx.cs" Inherits="ArtWebApp.Reports.Qualityreports.FabricRollReport" %>

<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
      <link rel="stylesheet" href="http://cdn.syncfusion.com/15.1.0.33/js/web/flat-azure/ej.web.all.min.css" /> 
    <link href="../../css/style.css" rel="stylesheet" />
    <!-- Essential Studio for JavaScript  script references --> 
    <script src="https://code.jquery.com/jquery-3.1.1.min.js"></script> 
    <script src="http://cdn.syncfusion.com/js/assets/external/jsrender.min.js"></script> 
    <script src="http://cdn.syncfusion.com/15.1.0.33/js/web/ej.web.all.min.js"> </script>
    .MyGridClass .rgDataDiv
{
height : auto !important ;
}
</head>
<body>
    <form id="form1" runat="server">
        
       
          <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div>
        <div>
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
                                                <ucc:DropDownListChosen ID="drp_atc" runat="server" Height="25px" Width="170px" DataSourceID="atcdata" DataTextField="AtcNum" DataValueField="AtcId" DisableSearchThreshold="10" style="text-align: left">
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
                <td class="NormalTD">


                    Report name</td>
                <td class="NormalTD">


                    <asp:UpdatePanel ID="upd_atc0" runat="server">
                        <ContentTemplate>
                            <ucc:DropDownListChosen ID="drp_atc0" runat="server" DataSourceID="atcdata" DataTextField="AtcNum" DataValueField="AtcId" DisableSearchThreshold="10" Height="25px" style="text-align: left" Width="170px">
                            </ucc:DropDownListChosen>
                        </ContentTemplate>
                    </asp:UpdatePanel>


                </td>
                <td class="NormalTD">


                    &nbsp;</td>
                <td class="NormalTD">


                    &nbsp;</td>
            </tr>
            <tr>
                <td class="NormalTD" colspan="4">
 

                    
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand=" SELECT        FabricRollmaster.Roll_PK, FabricRollmaster.RollNum, FabricRollmaster.Qty, FabricRollmaster.UOM, FabricRollmaster.Remark, FabricRollmaster.SShrink, FabricRollmaster.SYard, FabricRollmaster.SShade, 
                         FabricRollmaster.SWidth, FabricRollmaster.AShrink, FabricRollmaster.AShade, FabricRollmaster.AWidth, FabricRollmaster.AYard, ISNULL(FabricRollmaster.SGsm, '') AS SGsm, ISNULL(FabricRollmaster.AGsm, '') 
                         AS AGsm, ISNULL(SkuRawMaterialMaster.Composition, ' ') + ' ' + ISNULL(SkuRawMaterialMaster.Construction, ' ') + ' ' + ISNULL(SkuRawMaterialMaster.Weight, ' ') + ' ' + ISNULL(SkuRawMaterialMaster.Width, ' ') 
                         + ' ' + ISNULL(SkuRawmaterialDetail.ItemColor, ' ') + ' ' + ISNULL(SkuRawmaterialDetail.ItemSize, ' ') + ' ' + ISNULL(ProcurementDetails.SupplierSize, ' ') + ' ' + ISNULL(ProcurementDetails.SupplierColor, ' ') 
                         AS itemDescription, SupplierDocumentMaster.SupplierDocnum, SupplierDocumentMaster.AtracotrackingNum, ISNULL(FabricRollmaster.IsAcceptable, '') AS IsAcceptable, ISNULL(FabricRollmaster.MarkerType, '') 
                         AS MarkerType, ISNULL(FabricRollmaster.WidthGroup, '') AS WidthGroup, ISNULL(FabricRollmaster.ShadeGroup, '') AS ShadeGroup, ISNULL(FabricRollmaster.ShrinkageGroup, '') AS ShrinkageGroup, 
                         ISNULL(FabricRollmaster.TotalDefect, '') AS TotalDefect, ISNULL(FabricRollmaster.TotalDefecton100, '') AS TotalDefection100, ISNULL(FabricRollmaster.TotalPoint, '') AS TotalPoint, 
                         ISNULL(FabricRollmaster.TotalPointon100yard, '') AS TotalPointon100yard, ProcurementMaster.PONum, AtcMaster.AtcNum, SupplierMaster.SupplierName
FROM            SkuRawMaterialMaster INNER JOIN
                         SkuRawmaterialDetail ON SkuRawMaterialMaster.Sku_Pk = SkuRawmaterialDetail.Sku_PK INNER JOIN
                         FabricRollmaster ON SkuRawmaterialDetail.SkuDet_PK = FabricRollmaster.SkuDet_PK INNER JOIN
                         ProcurementDetails ON FabricRollmaster.podet_pk = ProcurementDetails.PODet_PK INNER JOIN
                         SupplierDocumentMaster ON FabricRollmaster.SupplierDoc_pk = SupplierDocumentMaster.SupplierDoc_pk INNER JOIN
                         ProcurementMaster ON ProcurementDetails.PO_Pk = ProcurementMaster.PO_Pk INNER JOIN
                         AtcMaster ON ProcurementMaster.AtcId = AtcMaster.AtcId INNER JOIN
                         SupplierMaster ON SupplierDocumentMaster.Supplier_pk = SupplierMaster.Supplier_PK
WHERE        (SkuRawMaterialMaster.Atc_id =@atc_id)">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="drp_atc" Name="atc_id" PropertyName="SelectedValue" />
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
  </div>
    <div>
        
             
    </div>
        

        <asp:SqlDataSource ID="atcdata" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" 
                    SelectCommand="SELECT DISTINCT AtcNum, AtcId FROM AtcMaster WHERE (IsClosed = N'N') ORDER BY AtcNum, AtcId">
                </asp:SqlDataSource>
    </div>
    </form>
</body>
</html>

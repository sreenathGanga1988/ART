<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AtcTransactionreport.aspx.cs" Inherits="ArtWebApp.Reports.Inventoryreport.AtcTransactionreport" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../css/style.css" rel="stylesheet" />
    </head>
<body>
    <form id="form1" runat="server">
      <div class="FullTable"> <asp:UpdatePanel ID="upd_buttons" UpdateMode="Conditional" ChildrenAsTriggers="false" runat="server">
                                 <ContentTemplate>
                        <table class="DataEntryTable">
                            <tr>
                                <td class="RedHeaddingdIV" colspan="6" >
                                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                                    </asp:ScriptManager>
                                    ATC Transaction</td>
                            </tr>
                              <tr>
                                  <td class="NormalTD">ATC # : </td>
                                  <td class="NormalTD">
                                      <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                          <ContentTemplate>
                                              <ucc:DropDownListChosen ID="cmb_atc" runat="server" DataSourceID="SqlDataSource1" DataTextField="AtcNum" DataValueField="AtcId" Height="17px" Width="200px">
                                              </ucc:DropDownListChosen>
                                          </ContentTemplate>
                                      </asp:UpdatePanel>
                                  </td>
                                  <td class="SearchButtonTD">
                                      <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                          <ContentTemplate>
                                              <asp:Button ID="ShowBom" runat="server" Height="23px" OnClick="ShowBom_Click" Text="S" Width="34px" />
                                          </ContentTemplate>
                                      </asp:UpdatePanel>
                                  </td>
                                  <td class="NormalTD">&nbsp;</td>
                                  <td class="NormalTD">
                                      &nbsp;</td>
                                  <td></td>
                            </tr>
                              <tr>
                                <td >RMNUM</td>
                               
                                      <td class="NormalTD">
                                          <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                              <ContentTemplate>
                                                  <ig:WebDropDown ID="drp_rmnum" runat="server" BorderStyle="None" EnableClosingDropDownOnSelect="False" EnableMultipleSelection="True" TextField="RMNum" ValueField="Sku_pk" Width="200px">
                                                      <DropDownItemBinding TextField="RMNum" ValueField="Sku_pk" />
                                                  </ig:WebDropDown>
                                              </ContentTemplate>
                                          </asp:UpdatePanel>
                                      </td>
                                      <td class="SearchButtonTD">
                                          <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                              <ContentTemplate>
                                                  <asp:Button ID="ShowRawmaterialBOM" runat="server" Height="23px" OnClick="ShowRawmaterialBOM_Click" Text="S" Width="34px" />
                                              </ContentTemplate>
                                          </asp:UpdatePanel>
                                      </td>
                                      <td class="NormalTD">&nbsp;</td>
                                      <td class="NormalTD">
                                         
                                          &nbsp;</td>
                                      <td></td>
                                 
                            </tr>
                            
                            <tr>
                                <td>&nbsp;</td>
                                <td class="NormalTD" colspan="3">
                                    &nbsp;</td>
                                
                                <td class="NormalTD">&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                            
                        </table>

                        </ContentTemplate> </asp:UpdatePanel></div>

    <div ><table class="gridtable" >
                            <tr>
                                <td><asp:CheckBox ID="chk_f" Text=" OnhandQty F only" runat="server" /></td>
                                <td><asp:CheckBox ID="chk_W" Text="OnhandQty W only" runat="server" /></td>
                                <td><asp:CheckBox ID="chk_ct" Text="Show Cutorder" runat="server"  /></td>
                                  <td><asp:CheckBox ID="chk_doc" Text="Show ADN" runat="server"  /></td>
                                  <td><asp:CheckBox ID="chk_remark" Text="Show Remark" runat="server"  /></td>
                                <td>
                                    
                                    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Export to Excel" />
                                </td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                        </table></div>
    <div class="FullTable">

        


        <table class="auto-style12">
            <tr>
                 <td style="width: 100%"><div id="grid" style="overflow:auto" >
                            <asp:UpdatePanel ID="Upd_maingrid" UpdateMode="Conditional" ChildrenAsTriggers="false" runat="server">
                                 <ContentTemplate>
                       
                        <asp:GridView ID="tbl_bom" runat="server" AutoGenerateColumns="False"
                             BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" 
                            CellPadding="4" style="font-size: x-small; font-family: Calibri" Width="1033px" 
                            Font-Size="Large" DataKeyNames="SkuDet_PK" OnDataBound="tbl_bom_DataBound1" OnRowDataBound="tbl_bom_RowDataBound" >
                            <Columns>                               
                               
                                <asp:BoundField DataField="RMNum" HeaderText="RMNum" SortExpression="RMNum" />
                                <asp:BoundField DataField="Description" HeaderText="Description" ReadOnly="True" SortExpression="Description" />
                               
                                <asp:BoundField DataField="ColorName" HeaderText="Color Name" SortExpression="ColorName" />
                                  <asp:BoundField DataField="SizeName" HeaderText="Size Name" SortExpression="SizeName" />
                                <asp:BoundField DataField="ItemColor" HeaderText="Item Color" SortExpression="ItemColor" />
                                <asp:BoundField DataField="ItemSize" HeaderText="Item Size" SortExpression="ItemSize" />
                                <asp:BoundField DataField="UomCode" HeaderText="Uom" SortExpression="UomCode" />
                                  <asp:TemplateField HeaderText="Onhand Details">
                                     <ItemTemplate>
                                         <asp:GridView ID="tbl_onhand" runat="server" AutoGenerateColumns="False" BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2" Font-Size="Smaller">
                                             <Columns>
                                                 <asp:BoundField DataField="LocationPrefix" HeaderText="Store" />
                                               
                                                 <asp:BoundField DataField="OnhandQty" HeaderText="Onhand Qty" />
                                          
                                             </Columns>
                                             <FooterStyle BackColor="#F7DFB5" ForeColor="Black" />
                                             <HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" />
                                             <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
                                             <RowStyle BackColor="#FFF7E7" ForeColor="Black" />
                                             <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
                                             <SortedAscendingCellStyle BackColor="#FFF1D4" />
                                             <SortedAscendingHeaderStyle BackColor="#B95C30" />
                                             <SortedDescendingCellStyle BackColor="#F1E5CE" />
                                             <SortedDescendingHeaderStyle BackColor="#93451F" />
                                         </asp:GridView>
                                     </ItemTemplate>
                                 </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Cutorder Details">
                                     <ItemTemplate>
                                         <asp:GridView ID="tbl_cutorder" runat="server" AutoGenerateColumns="False" BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2" Font-Size="Smaller">
                                             <Columns>
                                                 <asp:BoundField DataField="LocationPrefix" HeaderText="Store" />
                                                <asp:BoundField DataField="Cut_NO" HeaderText="Cut_NO" />
                                                 <asp:BoundField DataField="FabQty" HeaderText="FabQty" />
                                          
                                             </Columns>
                                             <FooterStyle BackColor="#F7DFB5" ForeColor="Black" />
                                             <HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" />
                                             <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
                                             <RowStyle BackColor="#FFF7E7" ForeColor="Black" />
                                             <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
                                             <SortedAscendingCellStyle BackColor="#FFF1D4" />
                                             <SortedAscendingHeaderStyle BackColor="#B95C30" />
                                             <SortedDescendingCellStyle BackColor="#F1E5CE" />
                                             <SortedDescendingHeaderStyle BackColor="#93451F" />
                                         </asp:GridView>
                                     </ItemTemplate>
                                 </asp:TemplateField>



                                                                <asp:TemplateField HeaderText="ADN Details">
                                     <ItemTemplate>
                                         <asp:GridView ID="tbl_ADN" runat="server" AutoGenerateColumns="False" BackColor="#DEBA84" 
                                             BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2" Font-Size="Smaller">
                                             <Columns>
                                                 <asp:BoundField DataField="DocNum" HeaderText="ADN" />
                                                <asp:BoundField DataField="ContainerNum" HeaderText="ContainerNum" />
                                                 <asp:BoundField DataField="PONum" HeaderText="PONum" />
                                                 <asp:BoundField DataField="Qty" HeaderText="Qty" />
                                          
                                             </Columns>
                                             <FooterStyle BackColor="#F7DFB5" ForeColor="Black" />
                                             <HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" />
                                             <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
                                             <RowStyle BackColor="#FFF7E7" ForeColor="Black" />
                                             <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
                                             <SortedAscendingCellStyle BackColor="#FFF1D4" />
                                             <SortedAscendingHeaderStyle BackColor="#B95C30" />
                                             <SortedDescendingCellStyle BackColor="#F1E5CE" />
                                             <SortedDescendingHeaderStyle BackColor="#93451F" />
                                         </asp:GridView>
                                     </ItemTemplate>
                                 </asp:TemplateField>
                                  <asp:TemplateField HeaderText="PO Details">
                                     <ItemTemplate>
                                         <asp:GridView ID="tbl_PO" runat="server" AutoGenerateColumns="False" BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2" Font-Size="Smaller">
                                             <Columns>
                                                 <asp:BoundField DataField="PONum" HeaderText="PONum" />
                                               
                                                 <asp:BoundField DataField="POQty" HeaderText="POQty" />
                                                 <asp:BoundField DataField="UomCode" HeaderText="UomCode" />
                                             </Columns>
                                             <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
                                             <HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" />
                                             <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
                                             <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
                                             <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
                                             <SortedAscendingCellStyle BackColor="#FFF1D4" />
                                             <SortedAscendingHeaderStyle BackColor="#B95C30" />
                                             <SortedDescendingCellStyle BackColor="#F1E5CE" />
                                             <SortedDescendingHeaderStyle BackColor="#93451F" />
                                         </asp:GridView>
                                     </ItemTemplate>
                                 </asp:TemplateField>

                                        <asp:TemplateField HeaderText="MRN Details">
                                     <ItemTemplate>
                                         <asp:GridView ID="tbl_MRN" runat="server" AutoGenerateColumns="False" BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2" Font-Size="Smaller">
                                             <Columns>
                                                 <asp:BoundField DataField="MrnNum" HeaderText="MrnNum" />
                                                <asp:BoundField DataField="PONum" HeaderText="PONum" />
                                               
                                                 <asp:BoundField DataField="ReceiptQty" HeaderText="ReceiptQty" />
                                                 <asp:BoundField DataField="ExtraQty" HeaderText="ExtraQty" />
                                                 <asp:BoundField DataField="UomCode" HeaderText="UomCode" />
                                                  <asp:BoundField DataField="LocationPrefix" HeaderText="At" />
                                             </Columns>
                                             <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
                                             <HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" />
                                             <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
                                             <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
                                             <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
                                             <SortedAscendingCellStyle BackColor="#FFF1D4" />
                                             <SortedAscendingHeaderStyle BackColor="#B95C30" />
                                             <SortedDescendingCellStyle BackColor="#F1E5CE" />
                                             <SortedDescendingHeaderStyle BackColor="#93451F" />
                                         </asp:GridView>
                                     </ItemTemplate>
                                 </asp:TemplateField>
                                     <asp:TemplateField HeaderText="DOR Details">
                                     <ItemTemplate>
                                         <asp:GridView ID="tbl_DOR" runat="server" AutoGenerateColumns="False" BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2" Font-Size="Smaller">
                                             <Columns>
                                                 <asp:BoundField DataField="DORNum" HeaderText="DORNum" />
                                                <asp:BoundField DataField="ReceivedQty" HeaderText="ReceivedQty" />
                                               
                                                 <asp:BoundField DataField="UomCode" HeaderText="UomCode" />
                                                 <asp:BoundField DataField="LocationPrefix" HeaderText="At" />
                                                 <asp:BoundField DataField="DONum" HeaderText="Against" />
                                              
                                             </Columns>
                                             <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
                                             <HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" />
                                             <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
                                             <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
                                             <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
                                             <SortedAscendingCellStyle BackColor="#FFF1D4" />
                                             <SortedAscendingHeaderStyle BackColor="#B95C30" />
                                             <SortedDescendingCellStyle BackColor="#F1E5CE" />
                                             <SortedDescendingHeaderStyle BackColor="#93451F" />
                                         </asp:GridView>
                                     </ItemTemplate>
                                 </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Loan Details">
                                     <ItemTemplate>
                                         <asp:GridView ID="tbl_loan" runat="server" AutoGenerateColumns="False" BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2" Font-Size="Smaller">
                                             <Columns>
                                                 <asp:BoundField DataField="Refnum" HeaderText="Refnum" />
                                                <asp:BoundField DataField="ReceivedQty" HeaderText="ReceivedQty" />
                                               
                                                 <asp:BoundField DataField="LocationPrefix" HeaderText="LocationPrefix" />
                                                
                                             </Columns>
                                             <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
                                             <HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" />
                                             <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
                                             <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
                                             <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
                                             <SortedAscendingCellStyle BackColor="#FFF1D4" />
                                             <SortedAscendingHeaderStyle BackColor="#B95C30" />
                                             <SortedDescendingCellStyle BackColor="#F1E5CE" />
                                             <SortedDescendingHeaderStyle BackColor="#93451F" />
                                         </asp:GridView>
                                     </ItemTemplate>
                                 </asp:TemplateField>


                                <asp:TemplateField HeaderText="DO Details">
                                     <ItemTemplate>
                                         <asp:GridView ID="tbl_DO" runat="server" AutoGenerateColumns="False" BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2" Font-Size="Smaller">
                                             <Columns>
                                                 <asp:BoundField DataField="DONum" HeaderText="DONum" />
                                                <asp:BoundField DataField="DoType" HeaderText="DoType" />
                                                   <asp:BoundField DataField="frmLocationPrefix" HeaderText="From" />
                                                   <asp:BoundField DataField="DeliveryQty" HeaderText="Qty" />
                   
                                                    <asp:BoundField DataField="TOLocationPrefix" HeaderText="To" />
                                                 
                                                                       
                                                 
                                                
                                             </Columns>
                                             <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
                                             <HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" />
                                             <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
                                             <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
                                             <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
                                             <SortedAscendingCellStyle BackColor="#FFF1D4" />
                                             <SortedAscendingHeaderStyle BackColor="#B95C30" />
                                             <SortedDescendingCellStyle BackColor="#F1E5CE" />
                                             <SortedDescendingHeaderStyle BackColor="#93451F" />
                                         </asp:GridView>
                                     </ItemTemplate>
                                 </asp:TemplateField>

                                   <asp:TemplateField HeaderText="Loan Out">
                                     <ItemTemplate>
                                         <asp:GridView ID="tbl_LoanOut" runat="server" AutoGenerateColumns="False" BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2" Font-Size="Smaller">
                                             <Columns>
                                                 <asp:BoundField DataField="LoanNum" HeaderText="LoanNum" />
                                                <asp:BoundField DataField="LoanQty" HeaderText="LoanQty" />
                                                   <asp:BoundField DataField="LocationPrefix" HeaderText="From" />
                                                   <asp:BoundField DataField="IsApproved" HeaderText="Approved" />
                  
                                                 
                                                                       
                                                 
                                                
                                             </Columns>
                                             <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
                                             <HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" />
                                             <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
                                             <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
                                             <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
                                             <SortedAscendingCellStyle BackColor="#FFF1D4" />
                                             <SortedAscendingHeaderStyle BackColor="#B95C30" />
                                             <SortedDescendingCellStyle BackColor="#F1E5CE" />
                                             <SortedDescendingHeaderStyle BackColor="#93451F" />
                                         </asp:GridView>
                                     </ItemTemplate>
                                 </asp:TemplateField>


                                <asp:TemplateField HeaderText="RO Out">
                                     <ItemTemplate>
                                         <asp:GridView ID="tbl_ROOut" runat="server" AutoGenerateColumns="False" BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2" Font-Size="Smaller">
                                             <Columns>
                                                 <asp:BoundField DataField="RONum" HeaderText="RONum" />
                                                <asp:BoundField DataField="Qty" HeaderText="Qty" />
                                                   <asp:BoundField DataField="LocationPrefix" HeaderText="From" />
                                                   <asp:BoundField DataField="IsApproved" HeaderText="Approved" />
                  
                                                 
                                                                       
                                                 
                                                
                                             </Columns>
                                             <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
                                             <HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" />
                                             <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
                                             <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
                                             <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
                                             <SortedAscendingCellStyle BackColor="#FFF1D4" />
                                             <SortedAscendingHeaderStyle BackColor="#B95C30" />
                                             <SortedDescendingCellStyle BackColor="#F1E5CE" />
                                             <SortedDescendingHeaderStyle BackColor="#93451F" />
                                         </asp:GridView>
                                     </ItemTemplate>
                                 </asp:TemplateField>

                                <asp:TemplateField HeaderText="SkuDet_PK" InsertVisible="False" SortExpression="SkuDet_PK">
                                   
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_skudetpk" runat="server" Text='<%# Bind("SkuDet_PK") %>'></asp:Label>
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


                         </ContentTemplate> </asp:UpdatePanel>
                        </div></td>
            </tr>
        </table>

        


        
    </div>

    
     
    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT        SkuRawmaterialDetail.SkuDet_PK, SkuRawMaterialMaster.RMNum, 
                         SkuRawMaterialMaster.Composition + ' ' + SkuRawMaterialMaster.Construction + ' ' + SkuRawMaterialMaster.Weight + ' ' + SkuRawMaterialMaster.Width AS Description, SkuRawmaterialDetail.ColorCode, 
                         SkuRawmaterialDetail.SizeCode, SkuRawmaterialDetail.ItemColor, SkuRawmaterialDetail.SupplierColor, SkuRawmaterialDetail.ItemSize, SkuRawmaterialDetail.SupplierSize, ISNULL
                             ((SELECT        MAX(StyleCostingDetails.Rate) AS Expr1
                                 FROM            StyleCostingDetails INNER JOIN
                                                          StyleCostingMaster ON StyleCostingDetails.Costing_PK = StyleCostingMaster.Costing_PK
                                 GROUP BY StyleCostingDetails.Sku_PK, StyleCostingMaster.IsApproved
                                 HAVING        (StyleCostingMaster.IsApproved = N'A') AND (StyleCostingDetails.Sku_PK = SkuRawMaterialMaster.Sku_Pk)), SkuRawmaterialDetail.UnitRate) AS UnitRate, ISNULL
                             ((SELECT        MAX(StyleCostingDetails_1.Consumption) AS Expr1
                                 FROM            StyleCostingDetails AS StyleCostingDetails_1 INNER JOIN
                                                          StyleCostingMaster AS StyleCostingMaster_1 ON StyleCostingDetails_1.Costing_PK = StyleCostingMaster_1.Costing_PK
                                 GROUP BY StyleCostingDetails_1.Sku_PK, StyleCostingMaster_1.IsApproved
                                 HAVING        (StyleCostingMaster_1.IsApproved = N'A') AND (StyleCostingDetails_1.Sku_PK = SkuRawMaterialMaster.Sku_Pk)), 0) AS Consumption, SkuRawmaterialDetail.RqdQty, 0000 AS PoIssuedQty, 
                         0000 AS BalanceQty, SkuRawMaterialMaster.Uom_PK, SkuRawMaterialMaster.AltUom_pk, SkuRawMaterialMaster.Atc_id, SkuRawMaterialMaster.isCommon, SkuRawMaterialMaster.IsCD, 
                         SkuRawMaterialMaster.IsSD, SkuRawMaterialMaster.IsGD, UOMMaster.UomCode, SkuRawMaterialMaster.Template_pk, ISNULL(SkuRawMaterialMaster.OrderMin, 0) AS OrderMin, 
                         Template_Master.ItemGroup_PK, SizeMaster.SizeName, 00 AS GarmentQty, ColorMaster.ColorName, SkuRawMaterialMaster.WastagePercentage
FROM            SkuRawmaterialDetail INNER JOIN
                         SkuRawMaterialMaster ON SkuRawmaterialDetail.Sku_PK = SkuRawMaterialMaster.Sku_Pk INNER JOIN
                         UOMMaster ON SkuRawMaterialMaster.AltUom_pk = UOMMaster.Uom_PK INNER JOIN
                         Template_Master ON SkuRawMaterialMaster.Template_pk = Template_Master.Template_PK INNER JOIN
                         ColorMaster ON SkuRawmaterialDetail.ColorCode = ColorMaster.ColorCode LEFT OUTER JOIN
                         SizeMaster ON SkuRawmaterialDetail.SizeCode = SizeMaster.SizeCode
WHERE        (SkuRawMaterialMaster.Atc_id = 1)
ORDER BY Template_Master.ItemGroup_PK, SkuRawMaterialMaster.RMNum"></asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" 
                    SelectCommand="SELECT DISTINCT AtcNum, AtcId FROM AtcMaster WHERE (IsClosed = N'N') ORDER BY AtcNum, AtcId">
                </asp:SqlDataSource>
<%--       <asp:UpdateProgress ID="PageUpdateProgress" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="0" DynamicLayout="true">
                                <ProgressTemplate>
                                   <div class="modal">
        <div class="center">
          <img  src="../../Image/loader.gif" style="position: relative; top: 45%;" > </img>
        </div>
    </div>
                                     
                                       
                                        
                                </ProgressTemplate>
                            </asp:UpdateProgress>--%>
    </form>
</body>
</html>

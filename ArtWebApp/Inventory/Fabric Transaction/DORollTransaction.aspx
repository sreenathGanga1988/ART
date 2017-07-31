<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="DORollTransaction.aspx.cs" Inherits="ArtWebApp.Inventory.Fabric_Transaction.DORollTransaction" %>
<%@ Register assembly="Infragistics35.Web.v12.1, Version=12.1.20121.2236, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.ListControls" tagprefix="ig" %>
<%@ Register assembly="Infragistics35.Web.v12.1, Version=12.1.20121.2236, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.EditorControls" tagprefix="ig" %>
<%@ Register assembly="DropDownChosen" namespace="CustomDropDown" tagprefix="ucc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="../../JQuery/GridJQuery.js"></script>
    <link href="../../css/style.css" rel="stylesheet" />
    <style type="text/css">
        .smallCell {
            height: 27px;
            width: 35px;
            font-size: xx-small;
            font-weight: 700;
        }
      
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    

    
        <table class="FullTable">

            <tr>
                <td>
<table class="DataEntryTable">
                <tr>
                    <td class="RedHeadding" colspan="5">
                        &nbsp;&nbsp;&nbsp;&nbsp;DO ROlls</td>
                    <td class="RedHeadding">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td">
                        
                    </td>
                    <td class="NormalTD" >
                          
                        Atc</td>
                    <td class="NormalTD"  >
                         

                          <asp:UpdatePanel ID="upd_atc" UpdateMode="Conditional" runat="server">
                                                <ContentTemplate>
                                                <ucc:DropDownListChosen ID="drp_atc" runat="server" Height="25px" Width="170px" DataSourceID="atcdata" DataTextField="AtcNum" DataValueField="AtcId" DisableSearchThreshold="10">
                        </ucc:DropDownListChosen>
                                                   </ContentTemplate>
                                            </asp:UpdatePanel>

                    </td>
                    <td class="SearchButtonTD" >
                          <asp:UpdatePanel ID="upd_btn_atc" UpdateMode="Conditional" runat="server">
                                                <ContentTemplate>
                       <asp:Button ID="btn_atc" runat="server" Text="S" Width="33px"  CssClass="auto-style10" OnClick="btn_atc_Click" /></td>
                     </ContentTemplate>
                                            </asp:UpdatePanel>   <td class="NormalTD" >
                        &nbsp;</td>
                    <td></td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="NormalTD" >
                        DO # :
                    </td>
                    <td class="NormalTD" >
                             
                      <asp:UpdatePanel ID="upd_do" UpdateMode="Conditional" runat="server">
                                                <ContentTemplate>
                                                <ucc:DropDownListChosen ID="ddl_do" runat="server" Height="25px" Width="170px" DisableSearchThreshold="10">
                        </ucc:DropDownListChosen>
                                                   </ContentTemplate>
                                            </asp:UpdatePanel>
                        </td>
                    <td class="SearchButtonTD" >
                         <asp:UpdatePanel ID="upd_btn_do" UpdateMode="Conditional" runat="server">
                                                <ContentTemplate>
                       <asp:Button ID="btn_do" runat="server" Text="S" Width="33px"  CssClass="auto-style10" OnClick="btn_do_Click"  /></td>
                     </ContentTemplate>
                                            </asp:UpdatePanel>  </td>
                    <td class="NormalTD" >
                        </td>
                    <td class="NormalTD" >
                        </td>
                    <td class="NormalTD" >
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="NormalTD" >
                        Fabric Details :
                    </td>
                    <td class="NormalTD" >
                       <asp:UpdatePanel ID="upd_color" UpdateMode="Conditional" runat="server">
                                                <ContentTemplate>
                                                <ucc:DropDownListChosen ID="drp_color" runat="server" Height="25px" Width="170px" DisableSearchThreshold="10">
                        </ucc:DropDownListChosen>
                                                   </ContentTemplate>
                                            </asp:UpdatePanel></td>
                    
                    <td class="SearchButtonTD" >
                          <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
                                                <ContentTemplate>
                       <asp:Button ID="Button2" runat="server" Text="S" Width="33px"  CssClass="auto-style10" OnClick="Button2_Click"  /></td>
                     </ContentTemplate>
                                            </asp:UpdatePanel> 
                    </td>
                    <td class="NormalTD" >
                        &nbsp;</td>
                    <td class="NormalTD" >
                         &nbsp;</td>
                    <td class="NormalTD" >
                         &nbsp;</td>
                </tr>
    <td colspan="5">

        <Table class="smallgridtable">
        <tr>
                    <td class="smallCell" >
                        <strong>Delivered Yards</strong></td>
                    <td class="smallCell" >
                        <asp:Label ID="lbl_DeliveryQty" runat="server" style="font-size: small; font-weight: 700" Text="0"></asp:Label>
                    </td>
                    
                    <td class="smallCell" >
                         Delivery uom</td>
                    
                    <td class="smallCell" >
                         <asp:Label ID="lbl_DeliveryUOM" runat="server" style="font-size: small; font-weight: 700" Text="0"></asp:Label>
                    </td>
                    
                    <td class="smallCell" >
                         <strong>Already Added yards</strong></td>
                    <td class="smallCell" >
                        <asp:Label ID="lbl_AlreadyAddedYArd" runat="server" style="font-size: small; font-weight: 700" Text="0"></asp:Label>
                        </td>
                    <td class="smallCell">Already Added KGS</td>
                    <td class="smallCell"><asp:Label ID="lbl_alreadyAddedKGS" runat="server" style="font-size: small; font-weight: 700" Text="0"></asp:Label>
                       </td>
                    <td class="smallCell" >
                         <strong>Balance to Add</strong></td>
                    <td class="smallCell" >
                         <asp:Label ID="lbl_balancetoAdd" runat="server" style="font-size: small; font-weight: 700" Text="0"></asp:Label>
                    </td>
                </tr>
    </Table>
    </td>

    
                
                <tr>
                    <td class="gridtable" colspan="5">
                        <asp:UpdatePanel ID="upd_grid"   UpdateMode="Conditional" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="tbl_inventory" runat="server" AutoGenerateColumns="False" BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2" DataKeyNames="Roll_PK,RollInventory_PK">
                                    <Columns>       <asp:TemplateField>  
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="checkAll" runat ="server" onclick="checkAll(this)"/>
                                    </HeaderTemplate>                                 
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chk_select" runat="server" onclick="Check_Click(this)"/>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Roll_PK" InsertVisible="False" SortExpression="Roll_PK">
                                          
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_rollPk" runat="server" Text='<%# Bind("Roll_PK") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="RI_PK" InsertVisible="False" SortExpression="RollInventory_PK">
                                         
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_RollInventory_PK" runat="server" Text='<%# Bind("RollInventory_PK") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="RollNum" HeaderText="RollNum" SortExpression="RollNum" />
                                        <asp:BoundField DataField="DocumentNum" HeaderText="DocumentNum" SortExpression="DocumentNum" />
                                        <asp:BoundField DataField="WidthGroup" HeaderText="WidthGroup" SortExpression="WidthGroup" />
                                        <asp:BoundField DataField="ShadeGroup" HeaderText="ShadeGroup" SortExpression="ShadeGroup" />
                                        <asp:BoundField DataField="ShrinkageGroup" HeaderText="ShrinkageGroup" SortExpression="ShrinkageGroup" />
                                        <asp:BoundField DataField="IsPresent" HeaderText="IsPresent" SortExpression="IsPresent" />
                                        <asp:BoundField DataField="AYard" HeaderText="AYard" SortExpression="AYard" />
                                        <asp:BoundField DataField="Location_Pk" HeaderText="Location_Pk" SortExpression="Location_Pk" />
                                        <asp:BoundField DataField="IsDelivered" HeaderText="IsDelivered" SortExpression="IsDelivered" />
                                        <asp:BoundField DataField="SkuDet_PK" HeaderText="SkuDet_PK" SortExpression="SkuDet_PK" />
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
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td class="gridtable">
                        &nbsp;</td>
                </tr>
                <tr class="ButtonTR">
                    <td class="NormalTD" >
                        <asp:Button ID="Button1" runat="server" Text="Save Roll Data" OnClick="Button1_Click" />
                    </td>
                    <td class="NormalTD" >
                        &nbsp;</td>
                    <td class="auto-style7" >
                        &nbsp;</td>
                    <td class="NormalTD" >
                        &nbsp;</td>
                    <td class="NormalTD" >
                        &nbsp;</td>
                    <td class="NormalTD" >
                        &nbsp;</td>
                </tr>
            </table>

                </td>
            </tr>
        </table>
      
            
        
   
    <div class="footer">
        
                <asp:SqlDataSource ID="atcdata" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" 
                    SelectCommand="SELECT DISTINCT AtcNum, AtcId FROM AtcMaster WHERE (IsClosed = N'N') ORDER BY AtcNum, AtcId">
                </asp:SqlDataSource>
                    
               
                               
                <asp:SqlDataSource ID="rolldatasource" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT        FabricRollmaster.Roll_PK, RollInventoryMaster.RollInventory_PK, FabricRollmaster.RollNum, RollInventoryMaster.DocumentNum, FabricRollmaster.WidthGroup, FabricRollmaster.ShadeGroup, 
                         FabricRollmaster.ShrinkageGroup, RollInventoryMaster.IsPresent, FabricRollmaster.AYard, RollInventoryMaster.Location_Pk, FabricRollmaster.IsDelivered, FabricRollmaster.SkuDet_PK
FROM            FabricRollmaster INNER JOIN
                         RollInventoryMaster ON FabricRollmaster.Roll_PK = RollInventoryMaster.Roll_PK
WHERE        (RollInventoryMaster.IsPresent = N'Y') AND (RollInventoryMaster.Location_Pk = @Param1) AND (FabricRollmaster.IsDelivered = N'N') AND (FabricRollmaster.SkuDet_PK = @Param2)">
                    <SelectParameters>
                        <asp:Parameter Name="Param1" />
                        <asp:Parameter Name="Param2" />
                    </SelectParameters>
                </asp:SqlDataSource>
                <br />
                    
               
                               
    </div>

    
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="PreQadApproval.aspx.cs" Inherits="ArtWebApp.Inventory.Fabric_Transaction.PreQadApproval" %>
<%@ Register assembly="DropDownChosen" namespace="CustomDropDown" tagprefix="ucc" %>
<%@ Register assembly="Infragistics35.Web.v12.1, Version=12.1.20121.2236, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.GridControls" tagprefix="ig" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../css/style.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  
        <table class="DataEntryTable">
            <tr>
                <td class="RedHeadding" colspan="4">


                    pre qad iNSPECTION Approval</td>
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
                <td class="smallgridtable" colspan="4">
 <asp:UpdatePanel ID="upd_grid"   runat="server">
     <ContentTemplate>
         <asp:GridView ID="tbl_InverntoryDetails" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" style="font-size: small; font-family: Calibri; font-weight: 400;" Width="100%" BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4">
                                    <Columns>
                                           <asp:TemplateField>
                                       
                                        <ItemTemplate>
                                            <asp:CheckBox ID="Chk_select" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                           <asp:TemplateField HeaderText="Roll_PK" InsertVisible="False" SortExpression="Roll_PK">
                                              
                                               <ItemTemplate>
                                                   <asp:Label ID="lbl_rollpk" runat="server" Text='<%# Bind("Roll_PK") %>'></asp:Label>
                                               </ItemTemplate>
                                           </asp:TemplateField>
                                        <asp:BoundField DataField="RollNum" HeaderText="RollNum" SortExpression="RollNum" />
                                        <asp:BoundField DataField="MrnNum" HeaderText="MrnNum" SortExpression="MrnNum" />
                                        <asp:BoundField DataField="Qty" HeaderText="Qty" SortExpression="Qty" />
                                        <asp:BoundField DataField="UOM" HeaderText="UOM" SortExpression="UOM" />
                                        <asp:BoundField DataField="SShrink" HeaderText="SShrink" SortExpression="SShrink" />
                                        <asp:BoundField DataField="SYard" HeaderText="SYard" SortExpression="SYard" />
                                        <asp:BoundField DataField="SShade" HeaderText="SShade" SortExpression="SShade" />
                                        <asp:BoundField DataField="SWidth" HeaderText="SWidth" SortExpression="SWidth" />
                                        <asp:BoundField DataField="AShrink" HeaderText="AShrink" SortExpression="AShrink" />
                                        <asp:BoundField DataField="AShade" HeaderText="AShade" SortExpression="AShade" />
                                        <asp:BoundField DataField="AWidth" HeaderText="AWidth" SortExpression="AWidth" />
                                        <asp:BoundField DataField="AYard" HeaderText="AYard" SortExpression="AYard" />
                                        <asp:BoundField DataField="IsAcceptable" HeaderText="IsAcceptable" SortExpression="IsAcceptable" />
                                        <asp:BoundField DataField="MarkerType" HeaderText="MarkerType" SortExpression="MarkerType" />
                                        <asp:BoundField DataField="IsSaved" HeaderText="IsSaved" SortExpression="IsSaved" />
                                        <asp:BoundField DataField="IsApproved" HeaderText="IsApproved" SortExpression="IsApproved" />
                                        <asp:BoundField DataField="PONum" HeaderText="PONum" SortExpression="PONum" />
                                        <asp:BoundField DataField="SupplierName" HeaderText="SupplierName" SortExpression="SupplierName" />
                                        <asp:BoundField DataField="AtcNum" HeaderText="AtcNum" SortExpression="AtcNum" />
                                        <asp:BoundField DataField="AtcId" HeaderText="AtcId" InsertVisible="False" ReadOnly="True" SortExpression="AtcId" Visible="False" />
                                    
                                      <asp:TemplateField HeaderText="Acceptable">
                                            
                                            <ItemTemplate>
                                                 <asp:DropDownList ID="drp_acceptable" runat="server">
                                                     <asp:ListItem Selected="True">Yes</asp:ListItem>
                                                     <asp:ListItem>No</asp:ListItem>
                                                 </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="MarkerType">
                                           
                                            <ItemTemplate>
                                                <asp:DropDownList ID="drp_markerType" runat="server">
                                                    <asp:ListItem Selected="True">REG</asp:ListItem>
                                                    <asp:ListItem>RSV</asp:ListItem>
                                                    <asp:ListItem>CSV</asp:ListItem>
                                                </asp:DropDownList>
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


                    <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Approve Inspection" />
                </td>
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


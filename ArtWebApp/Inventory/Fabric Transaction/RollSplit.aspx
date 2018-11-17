<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="RollSplit.aspx.cs" Inherits="ArtWebApp.Inventory.Fabric_Transaction.RollSplit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link href="../../css/style.css" rel="stylesheet" />
    <script src="../../JQuery/GridJQuery.js"></script>
    <script type="text/javascript" >

      


        function Onselection(objref)
        {
            Check_Click(objref)
            GetSumofSelectedLabelinFooterTextbox('DataEntry', 'lbl_supplieryard', 'txt_syardfooter');
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    

    
        <table class="FullTable">

            <tr>
                <td>
<table class="DataEntryTable">
                <tr>
                    <td class="RedHeadding" colspan="5">
                        &nbsp;&nbsp;&nbsp;&nbsp; MRN ROlls Details</td>
                </tr>
                <tr>
                    <td">
                       
                    </td>
                    <td >
                          
                        Atc

                    </td>
                    <td class="NormalTD"  >
                         

                          <asp:UpdatePanel ID="upd_atc" UpdateMode="Conditional" runat="server">
                                                <ContentTemplate>
                                                <ucc:DropDownListChosen ID="drp_atc" runat="server" Height="25px" Width="170px" DataSourceID="atcdata" DataTextField="AtcNum" DataValueField="AtcId" DisableSearchThreshold="10">
                        </ucc:DropDownListChosen>
                                                   </ContentTemplate>
                                            </asp:UpdatePanel>

                    </td>
                    <td >
                          <asp:UpdatePanel ID="upd_btn_atc" UpdateMode="Conditional" runat="server">
                                                <ContentTemplate>
                       <asp:Button ID="btn_atc" runat="server" Text="S" Width="33px"  CssClass="NormalTD" OnClick="btn_atc_Click" /></td>
                     </ContentTemplate>
                                            </asp:UpdatePanel>   </td >
                       
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
                    <td class="NormalTD" >
                         <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Conditional" runat="server">
                                                <ContentTemplate>
                       <asp:Button ID="Button2" runat="server" Text="S" Width="33px"  CssClass="NormalTD"  OnClick="Button2_Click"          /></td>
                     </ContentTemplate>
                                            </asp:UpdatePanel>  
                    </td>
                    <td class="NormalTD" >
                        </td>
                    <td class="NormalTD" >
                         </td>
                </tr>
                
                    <td >
                        
                        MRNQTY</td>
                    <td class="NormalTD" >
                        <asp:UpdatePanel ID="upd_qty" UpdateMode="Conditional" runat="server">
                            <ContentTemplate>
                                <asp:Label ID="lbl_mrnQty" runat="server" Text="0"></asp:Label>
                                <asp:Label ID="lbl_UOM" runat="server"></asp:Label>
                            </ContentTemplate>
                        </asp:UpdatePanel></td>
                    <td class="NormalTD" >
                        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Split Roll (Select only one Roll)" />
                </td>
                    <td >
                        &nbsp;</td>
                    <td >
                         &nbsp;</td>
                </tr>
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
                                        <asp:BoundField DataField="MarkerType" HeaderText="MarkerType" SortExpression="MarkerType" />
                                        <asp:BoundField DataField="IsPresent" HeaderText="IsPresent" SortExpression="IsPresent" />
                                        <asp:TemplateField HeaderText="AYard" SortExpression="AYard">
                                           
                                           
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_ayard" runat="server" Text='<%# Bind("AYard") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
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
                </tr>
                <tr class="ButtonTR">
                    <td >
                        
                    </td>
                    <td class="NormalTD" >
                        &nbsp;</td>
                    <td class="NormalTD" >
                        &nbsp;</td>
                    <td >
                        &nbsp;</td>
                    <td >
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
                    
               
                               
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT [Roll_PK], [RollNum], [SShrink], [SYard], [SShade], [SWidth], [SGsm] FROM [FabricRollmaster]"></asp:SqlDataSource>
                    
               
                               
    </div>

    
</asp:Content>


<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="ManualGrouping.aspx.cs" Inherits="ArtWebApp.Quality.ManualGrouping" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../css/style.css" rel="stylesheet" />
    <style type="text/css">
        
    </style>

     <script type="text/javascript">
     



         function CheckBoxSelectionValidation() {
             debugger;
             var gridView = document.getElementById("<%= tbl_InverntoryDetails.ClientID %>");

             for (var i = 1; i < gridView.rows.length; i++) {
                 var count = 0;
                 var chkConfirm = gridView.rows[i].cells[0].getElementsByTagName('input')[0];
                
                 var txtwidth = gridView.rows[i].cells[19].getElementsByTagName('input')[0];
                 var txtshade = gridView.rows[i].cells[20].getElementsByTagName('input')[0];
                 var txtshrnk = gridView.rows[i].cells[21].getElementsByTagName('input')[0];
                 if (chkConfirm.checked) {
                     if (txtwidth.value == "" || txtshade.value == "" || txtshrnk.value == "") {
                         gridView.rows[i].style.backgroundColor = "red";
                         txtwidth.focus();
                         
                       return false;
                     }
                 } 
             }

           
         }
    </script> 
<script type="text/javascript"  src="../JQuery/GridJQuery.js"></script>

     

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  
        <table class="DataEntryTable">
            <tr>
                <td class="RedHeadding" colspan="4">


                    Fabric Grouping</td>
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
                    <td class="NormalTD"  >
                        Fabric Details :
                    </td>
                    <td class="NormalTD" >
                             
                       <asp:UpdatePanel ID="upd_color" UpdateMode="Conditional" runat="server">
                                                <ContentTemplate>
                                                <ucc:DropDownListChosen ID="drp_color" runat="server" Height="25px" Width="200px" DisableSearchThreshold="10">
                        </ucc:DropDownListChosen>
                                                   </ContentTemplate>
                                            </asp:UpdatePanel></td>
                    <td class="NormalTD"  >
                         <asp:UpdatePanel ID="upd_btn_po" UpdateMode="Conditional" runat="server">
                                                <ContentTemplate>
                       <asp:Button ID="btn_fabric" runat="server" Text="S" Width="33px"  CssClass="auto-style10" OnClick="btn_fabric_Click1" /></td>
                     </ContentTemplate>
                                            </asp:UpdatePanel>  </td>
                    <td class="NormalTD"  >
                        </td>
                    <td class="NormalTD"  >
                        </td>
                </tr>


                <tr>
                    <td class="NormalTD"  >
                        Width :
                    </td>
                    <td class="NormalTD" >
                             
                       <asp:UpdatePanel ID="upd_width" UpdateMode="Conditional" runat="server">
                                                <ContentTemplate>
                                         


<ig:WebDropDown ID="drp_cutwidth" runat="server" Width="200px" EnableMultipleSelection="True" TextField="name" ValueField="pk" EnableClosingDropDownOnSelect="False" >
                      <DropDownItemBinding TextField="AWidth" ValueField="AWidth" />
                  </ig:WebDropDown>

                                                   </ContentTemplate>
                                            </asp:UpdatePanel></td>
                    <td class="NormalTD"  >
                         <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Conditional" runat="server">
                                                <ContentTemplate>
                       <asp:Button ID="btn_width" runat="server" Text="S" Width="33px"  CssClass="auto-style10" OnClick="btn_width_Click" /></td>
                     </ContentTemplate>
                                            </asp:UpdatePanel>  </td>
                    <td class="NormalTD"  >
                        </td>
                    <td class="NormalTD"  >
                        </td>
                </tr>


                 <tr>
                    <td class="NormalTD"  >
                        Shrinkage :
                    </td>
                    <td class="NormalTD" >
                             
                       <asp:UpdatePanel ID="upd_shrinkage" UpdateMode="Conditional" runat="server">
                                                <ContentTemplate>
                                           
                                                    <ig:WebDropDown ID="drp_shrinkage" runat="server" Width="200px" EnableMultipleSelection="True" TextField="name" ValueField="pk" EnableClosingDropDownOnSelect="False" >
                      <DropDownItemBinding TextField="AShrink" ValueField="AShrink" />
                  </ig:WebDropDown>


                                                   </ContentTemplate>
                                            </asp:UpdatePanel></td>
                    <td class="NormalTD"  >
                         <asp:UpdatePanel ID="UpdatePanel5" UpdateMode="Conditional" runat="server">
                                                <ContentTemplate>
                       <asp:Button ID="btn_shrinkage" runat="server" Text="S" Width="33px"  CssClass="auto-style10" OnClick="btn_shrinkage_Click" /></td>
                     </ContentTemplate>
                                            </asp:UpdatePanel>  </td>
                    <td class="NormalTD"  >
                        </td>
                    <td class="NormalTD"  >
                        </td>
                </tr>
               <tr>
                    <td class="NormalTD"  >
                        shade:
                    </td>
                    <td class="NormalTD" >
                             
                       <asp:UpdatePanel ID="upd_shade" UpdateMode="Conditional" runat="server">
                                                <ContentTemplate>
                                             

                                                    <ig:WebDropDown ID="drp_shade" runat="server" Width="200px" EnableMultipleSelection="True" TextField="name" ValueField="pk" EnableClosingDropDownOnSelect="False" >
                      <DropDownItemBinding TextField="AShade" ValueField="AShade" />
                  </ig:WebDropDown>

                                                   </ContentTemplate>
                                            </asp:UpdatePanel></td>
                    <td class="NormalTD"  >
                         <asp:UpdatePanel ID="UpdatePanel7" UpdateMode="Conditional" runat="server">
                                                <ContentTemplate>
                       <asp:Button ID="btn_shade" runat="server" Text="S" Width="33px"  CssClass="auto-style10" OnClick="btn_shade_Click" /></td>
                     </ContentTemplate>
                                            </asp:UpdatePanel>  </td>
                    <td class="NormalTD"  >
                        </td>
                    <td class="NormalTD"  >
                        </td>
                </tr>
                <tr>
                    <td class="NormalTD" colspan="5" >
                       <table style="border: thin double #C0C0C0; line-height: normal; vertical-align: middle;  text-align: center; white-space: normal; word-spacing: normal; letter-spacing: normal; background-color: #99CCFF; position: relative; width: 100%;" >
                            


                            <tr>
                                <td class="NormalTD">
                                    
                                    
                                    <strong>Quick Fill uick Fill </strong></td>
                            </tr>



                            <tr>
                                <td>
                                    


                                    <div>

                                           <asp:UpdatePanel ID="UpdatePanel3" UpdateMode="Conditional" runat="server">
                                                <ContentTemplate>
                     <asp:CheckBox ID="chk_select" runat="server" Text="Select all" AutoPostBack="True" OnCheckedChanged="chk_selectall_CheckedChanged" />
                     </ContentTemplate>
                                            </asp:UpdatePanel>
                                    </div>
                                </td>
                            </tr>



                            



                            <tr>
                                <td class="NormalTD">
                                   
                                    
                                    
                                    <table class="tittlebar">
                                        <tr>
                                            <td class="NormalTD">
                                                <asp:TextBox ID="txt_widthgroup" Placeholder="Enter Marker Width Group" runat="server"></asp:TextBox>
                                            </td>
                                            <td class="NormalTD">
                                                <asp:Button ID="btn_copywidth" runat="server" Text="apply Marker width " OnClick="btn_copywidth_Click" />
                                            </td>
                                            <td class="NormalTD"><asp:TextBox ID="txt_shrinkgroup" Placeholder="Enter  Marker Shrinkage Group"  runat="server"></asp:TextBox></td>
                                          <td class="NormalTD"><asp:Button ID="btn_copyshrinkage" runat="server" Text="Apply Marker shrinkage" OnClick="btn_copyshrinkage_Click" /></td>
                                             <td class="NormalTD"><asp:TextBox ID="txt_shadegroup" Placeholder="Enter Marker Shade Group" runat="server"></asp:TextBox></td>
                                             <td class="NormalTD"><asp:Button ID="btn_copyshade" runat="server" Text="Apply Marker Shade" OnClick="btn_copyshade_Click" />
                                                   <td class="NormalTD"><asp:Button ID="btn_inheritshade" runat="server" Text="Copy Marker Shade" OnClick="btn_inheritshade_Click"  />
                                            
                                                 
                                            </td>
                                        </tr>
                                    </table>
                            </tr>
                        </table> 
                    </td>
                </tr>






            <tr>
                <td class="smallgridtable" colspan="4">
 <asp:UpdatePanel ID="upd_grid" UpdateMode="Conditional"  runat="server">
     <ContentTemplate>
         <asp:GridView ID="tbl_InverntoryDetails" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" style="font-size: small; font-family: Calibri; font-weight: 400;" Width="100%" BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4">
                                    <Columns>
                                          <asp:TemplateField>
                                       
                                     
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="checkAll"  runat ="server" onclick="checkAll(this)"/>
                                    </HeaderTemplate>                                 
                                    <ItemTemplate>
                                        <asp:CheckBox ID="Chk_select"  runat="server" onclick="Check_Click(this)"/>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                        <asp:TemplateField HeaderText="RPK" InsertVisible="False" SortExpression="Roll_PK" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                                         
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_rollpk" runat="server" Text='<%# Bind("Roll_PK") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle CssClass="hidden" />
                                            <ItemStyle CssClass="hidden" />
                                        </asp:TemplateField>
                                           
                                           <asp:BoundField DataField="AtcNum" HeaderText="Atc#" SortExpression="AtcNum" />
                                           <asp:BoundField DataField="SupplierName" HeaderText="Supplier" SortExpression="SupplierName" />
                                         <asp:BoundField DataField="PONum" HeaderText="PO#" SortExpression="PONum" />
                                        <asp:BoundField DataField="Lotnum" HeaderText="Lot#" SortExpression="Lotnum" />
                                        <asp:BoundField DataField="RollNum" HeaderText="Roll#" SortExpression="RollNum" />
                                        <asp:BoundField DataField="Qty" HeaderText="Qty" SortExpression="Qty" >
                                           </asp:BoundField>
                                        <asp:BoundField DataField="UOM" HeaderText="UOM" SortExpression="UOM" />
                                        <asp:BoundField DataField="SShrink" HeaderText="SShrink" SortExpression="SShrink" />
                                        <asp:TemplateField HeaderText="AShrink" SortExpression="AShrink">
                                          
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_ashrink" runat="server" Text='<%# Bind("AShrink") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="SYard" HeaderText="SYard" SortExpression="SYard" />
                                           <asp:BoundField DataField="AYard" HeaderText="AYard" SortExpression="AYard" />
                                        <asp:BoundField DataField="SShade" HeaderText="SShade" SortExpression="SShade" />
                                        <asp:TemplateField HeaderText="AShade" SortExpression="AShade">
                                            
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_ashade" runat="server" Text='<%# Bind("AShade") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="SWidth" HeaderText="SWidth" SortExpression="SWidth" />
                                           <asp:TemplateField HeaderText="AWidth" SortExpression="AWidth">
                                              
                                               <ItemTemplate>
                                                   <asp:Label ID="lbl_awidth" runat="server" Text='<%# Bind("AWidth") %>'></asp:Label>
                                               </ItemTemplate>
                                        </asp:TemplateField>
                                                                    
                                          
                                         
                                          
                                           <asp:BoundField DataField="SGsm" HeaderText="SGsm" SortExpression="SGsm" />
                                           <asp:BoundField DataField="AGsm" HeaderText="AGsm" SortExpression="AGsm" />
                                           <asp:TemplateField HeaderText="Marker Width" SortExpression="WidthGroup">
                                              
                                               <ItemTemplate>
                                                   <asp:TextBox ID="txt_widthgroup"  Width="70px" runat="server" Text='<%# Bind("WidthGroup") %>'></asp:TextBox>
                                               </ItemTemplate>
                                        </asp:TemplateField>
                                           <asp:TemplateField HeaderText="Marker Shade" SortExpression="ShadeGroup">
                                              
                                               <ItemTemplate>
                                                   <asp:TextBox ID="txt_shadegroup" runat="server" Text='<%# Bind("ShadeGroup") %>'></asp:TextBox>
                                               </ItemTemplate>
                                        </asp:TemplateField>
                                           <asp:TemplateField HeaderText="Marker Shrinkage" SortExpression="ShrinkageGroup">
                                              
                                               <ItemTemplate>
                                                   <asp:TextBox ID="txt_lblshrinkagegroup" runat="server" Text='<%# Bind("ShrinkageGroup") %>'></asp:TextBox>
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


                    &nbsp;</td>
                <td class="NormalTD">


                    &nbsp;</td>
                <td class="NormalTD">

                    &nbsp;</td>
                <td class="NormalTD">


                    </td>
            </tr>
            <tr>
                <td class="NormalTD" colspan="4">


                    <asp:Button ID="Button3" runat="server" OnClientClick="return CheckBoxSelectionValidation()" OnClick="Button3_Click" Text="Update Group" Width="200px" />
                </td>
            </tr>
            <tr>
                <td class="NormalTD" colspan="4">
                    <div id="Messaediv" runat="server">
                 


                           <asp:Label ID="lbl_msg" runat="server" Text="*"></asp:Label>


                     
                           <asp:Button ID="Button4" runat="server" OnClientClick="CheckBoxSelectionValidation()" Text="Button" OnClick="Button4_Click2"   />


                     
               </div>

                    &nbsp;</td>
            </tr>
        </table>

        <asp:SqlDataSource ID="atcdata" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" 
                    SelectCommand="SELECT DISTINCT AtcNum, AtcId FROM AtcMaster WHERE (IsClosed = N'N') ORDER BY AtcNum, AtcId">
                </asp:SqlDataSource>
   
</asp:Content>


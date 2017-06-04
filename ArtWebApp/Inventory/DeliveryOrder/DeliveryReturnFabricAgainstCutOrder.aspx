<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="DeliveryReturnFabricAgainstCutOrder.aspx.cs" Inherits="ArtWebApp.Inventory.DeliveryOrder.DeliveryReturnFabricAgainstCutOrder" %>
<%@ Register assembly="Infragistics35.Web.v12.1, Version=12.1.20121.2236, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.ListControls" tagprefix="ig" %>
<%@ Register assembly="Infragistics35.Web.v12.1, Version=12.1.20121.2236, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.EditorControls" tagprefix="ig" %>
<%@ Register assembly="DropDownChosen" namespace="CustomDropDown" tagprefix="ucc" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link href="../../css/style.css" rel="stylesheet" />
    <script src="../../JQuery/GridJQuery.js"></script>
   <script type="text/javascript">
    
     
      
      function initDropDown(sender,args)
      {
        sender.behavior.set_zIndex(200000);
      }
  
    
       function Onselection(objref) {

           Check_Click(objref)
           calculatesumofyardage();
       }

       function OnSelectAllClick(objref) {

           
           checkAll(objref)

           calculatesumofyardage();
       }

      
      function initDropDown(sender,args)
      {
        sender.behavior.set_zIndex(200000);
      }
    



        function calculatesumofyardage()
        {
            var gridView = document.getElementById("<%= tbl_rolldata.ClientID %>");
            var sum = 0
            for (var i = 1; i < gridView.rows.length-1; i++)
            {
                var chkConfirm = gridView.rows[i].cells[0].getElementsByTagName('input')[0];
                if (chkConfirm.checked)
                {
                    var lbl_yard = gridView.rows[i].getElementsByClassName("lbl_yard")[0];

                    sum = sum + parseFloat(lbl_yard.innerHTML);
                }

            } 
            var totalyardfooter = document.getElementsByClassName("totalyardfooter")[0];
            totalyardfooter.value = sum;
        }



   
    </script>

    <style type="text/css">
        .NormalTD {
            font-size: small;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     
        <table class="FullTable">

            <tr>
                <td>
<table class="DataEntryTable">
                <tr>
                    <td class="RedHeadding" colspan="9">
                        wf transfer note(fabric)</td>
                </tr>
                <tr>
                    <td class="NormalTD">
                        Order No</td>
                    <td class="NormalTD">
                             
                
                    
                 <ucc:DropDownListChosen ID="cmb_atc" runat="server" DataSourceID="SqlDataSource1" DataTextField="AtcNum" DataValueField="AtcId" DisableSearchThreshold="10" Width="200px">
                        </ucc:DropDownListChosen>
                               
                    </td>
                   <td class="NormalTD">
                        <asp:Button ID="btn_confirmAtc" runat="server" Text="S" Width="33px" OnClick="btn_confirmAtc_Click" CssClass="NormalTD0" />
                    </td>
                    <td class="NormalTD">
                        D O Date:</td>
                    <td class="NormalTD">
                        <ig:WebDatePicker ID="dtp_dodate" runat="server" Height="26px" Width="191px">
                        </ig:WebDatePicker>
                    </td>
                    <td class="NormalTD">
                        &nbsp;</td>
                    <td>
                        
                    </td>
                    <td c>
                        &nbsp;</td>
                    <td class="NormalTD">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td  >
                        Remark</td>
                    <td  >
                             
                        <asp:TextBox ID="txt_BOE_no" runat="server" Height="20px" Width="164px" CssClass="NormalTD0"></asp:TextBox></td>
                   <td class="NormalTD">
                        </td>
                    <td  >
                        req&nbsp; nO : </td>
                    <td  >
                        <asp:TextBox ID="txt_containernum" runat="server" Height="20px" Width="164px" CssClass="NormalTD0"></asp:TextBox></td>
                    <td  >
                        </td>
                    <td class="NormalTD">
                        </td>
                    <td class="NormalTD">
                        </td>
                    <td class="NormalTD">
                        </td>
                </tr>
                <tr>
                    <td class="NormalTD">
                        To</td>
                    <td class="NormalTD">
                         <ucc:DropDownListChosen ID="drp_ToWarehouse" runat="server" Width="200px">
                        </ucc:DropDownListChosen>
                    </td>
                   
                   <td class="NormalTD">
                        &nbsp;</td>
                    
                    <td class="NormalTD">
                        mode : </td>
                    <td class="NormalTD">
                        
                       <ucc:DropDownListChosen ID="drp_deliverymode" runat="server" Width="200px">
                        </ucc:DropDownListChosen>
                        
                    </td>
                    <td class="NormalTD">
                        &nbsp;</td>
                    <td class="NormalTD">
                         
                    </td>
                     <td class="NormalTD">
                         &nbsp;</td>
                    <td class="NormalTD">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="gridtable" colspan="9">
                        <asp:UpdatePanel ID="UpdatePanel1"  UpdateMode="Conditional"   runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="tbl_InverntoryDetails" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" style="font-size: small; font-family: Calibri; font-weight: 400;" Width="100%" BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4" DataKeyNames="InventoryItem_PK" OnRowCommand="tbl_InverntoryDetails_RowCommand">
                                    <Columns>
                                        <asp:TemplateField HeaderImageUrl="~/Image/tick.jpg">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chk_select" runat="server" AutoPostBack="True" OnCheckedChanged="chk_select_CheckedChanged" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="II_PK">
                                            <ItemTemplate>
                                                <asp:Label ID="lblInventoryItem_PK" runat="server" Text='<%# Bind("InventoryItem_PK") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="RMNum" HeaderText="RMNum" />
                                        <asp:BoundField DataField="Description" HeaderText="Description" />
                                        <asp:BoundField DataField="ItemColor" HeaderText="ItemColor" />
                                        <asp:BoundField DataField="ItemSize" HeaderText="ItemSize" />
                                        <asp:BoundField DataField="SupplierColor" HeaderText="SupColor" />
                                        <asp:BoundField DataField="Suppliersize" HeaderText="SupSize" />
                                        <asp:BoundField DataField="UOMCode" HeaderText="UOM" />
                                        <asp:BoundField DataField="Refnum" HeaderText="Rcvd Via" />
                                        <asp:BoundField DataField="ReceivedQty" HeaderText="RcvdQty" />
                                        <asp:BoundField DataField="DeliveredQty" HeaderText="DeliveredQty" />
                                           <asp:BoundField DataField="TotalOnhand" HeaderText="Total Onhand" />
                                          <asp:TemplateField HeaderText="Blocked Qty">
                                             
                                              <ItemTemplate>
                                                 
                                                  <asp:LinkButton ID="lnkbtn_mrn" Text='<%# Bind("BlockedQty") %>' runat="server" ToolTip="Qty blocked by pending Ro or Loan"></asp:LinkButton>
                                              </ItemTemplate>
                                          </asp:TemplateField>
                                        <asp:TemplateField HeaderText="OnhandQty">
                                           
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_OnhandQty" runat="server" Text='<%# Bind("OnhandQty") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="CutOrder">
                                        
                                        <ItemTemplate>
                                           
                                            
                                            <asp:UpdatePanel ID="upd_cutorder" UpdateMode="Conditional" runat="server">
                                                <ContentTemplate>
                                                    <ucc:DropDownListChosen  ID="ddl_cutorder"  Width="130px" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddl_cutorder_SelectedIndexChanged">
                                            </ucc:DropDownListChosen>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </ItemTemplate>
                                    
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="BalCutQty">
                                           
                                            <ItemTemplate>

                                                <asp:UpdatePanel ID="upd_balacetocut" UpdateMode="Conditional" runat="server">
                                                <ContentTemplate>
                                                      <asp:Label ID="lbl_balacetocut" runat="server"></asp:Label>
                                                      </ContentTemplate>
                                            </asp:UpdatePanel>
                                              
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                         <asp:TemplateField HeaderText="RollYard">
                                           
                                            <ItemTemplate>

                                                <asp:UpdatePanel ID="upd_RollYard" UpdateMode="Conditional" runat="server">
                                                <ContentTemplate>
                                                      <asp:Label ID="lbl_RollYard"  Text="0" runat="server"></asp:Label>
                                                      </ContentTemplate>
                                            </asp:UpdatePanel>
                                              
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="DeliveryQty">
                                           
                                            <ItemTemplate>
                                                 <asp:UpdatePanel ID="upd_deliveryQty" UpdateMode="Conditional" runat="server">
                                                <ContentTemplate>
                                                <asp:TextBox ID="txt_deliveryQty" runat="server" Height="17px" Width="67px"></asp:TextBox>
                                                   </ContentTemplate>
                                            </asp:UpdatePanel>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                       
                                       
                                           <asp:ButtonField  Text="Add  Roll" CommandName="ShowRoll" />
                                        <asp:ButtonField  Text="Delete  Roll" CommandName="DeleteRoll" />
                                       
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
                    </td>
                </tr>
                <tr class="ButtonTR">
                    <td colspan="9" >
                   

             

 <asp:UpdatePanel ID="Upd_roll"  UpdateMode="Conditional"  runat="server">
                            <ContentTemplate>
    <asp:Panel ID="ModalPanel" runat="server" Visible="false">
             <table class="FullTable">
                 <tr>
                     <td>
                         
                         
                         Availbale Fabric Rolls</td>
                 </tr>
                 <tr class="DataEntryTable">
                     <td>
    
  
     
                <table  >
                    <tr>
                        <td>ShadeGroup</td>
                        <td class="NormalTD">   <asp:UpdatePanel ID="upd_shade" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <ig:WebDropDown ID="drp_shade" runat="server" EnableMultipleSelection="True" TextField="name" ValueField="pk" Width="200px" EnableDropDownAsChild="false">
                    <ClientEvents Initialize="initDropDown" />

                    <DropDownItemBinding TextField="ShadeGroup" ValueField="ShadeGroup" />
                </ig:WebDropDown>
                            </ContentTemplate>
        </asp:UpdatePanel>
                </td>



                        <td><asp:Button ID="Button1" runat="server" Text="S" OnClick="Button1_Click1" /></td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                      
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                       
                    </tr>
                       <tr>
                        <td class="NormalTD">Shrinkage group</td>
                         <td class="NormalTD">
                             <strong>
                             <asp:Label ID="lbl_shringagegroup" runat="server" Text="0"></asp:Label>
                             </strong>
                           </td>
                      <td class="NormalTD">Width group</td>
                       <td class="NormalTD"><strong>
                           <asp:Label ID="lbl_widthgroup" runat="server" Text="0"></asp:Label>
                           </strong></td>
                        <td class="NormalTD">MarkerType</td>
                        <td class="NormalTD"><strong>
                            <asp:Label ID="lbl_markerType" runat="server" Text="0"></asp:Label>
                            </strong></td>
                      
                    </tr>
                </table>

                        
   
</td>
                 </tr>
                 <tr>
                     <td>
                         
                         <asp:GridView ID="tbl_rolldata" runat="server" AutoGenerateColumns="False"  ShowFooter="true" ShowHeaderWhenEmpty="True" style="font-size: small; font-family: Calibri; font-weight: 400;" Width="100%" BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4" DataKeyNames="Roll_PK" OnSelectedIndexChanged="tbl_rolldata_SelectedIndexChanged">
                                    <Columns>
                                       



                                          <asp:TemplateField  ControlStyle-Width="10px" HeaderStyle-Width="10px" FooterStyle-Width="10px">
                                                    <HeaderTemplate>
                                                        <asp:CheckBox ID="checkAll" runat="server" onclick="OnSelectAllClick(this)" />
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chk_select" runat="server" onclick="Onselection(this)" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>   




                                        <asp:TemplateField HeaderText="Roll_PK">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_Roll_PK" runat="server" Text='<%# Bind("Roll_PK") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                  
                                        <asp:BoundField DataField="RollNum" HeaderText="RollNum" SortExpression="RollNum" />
                                        <asp:BoundField DataField="ASN" HeaderText="ASN" ReadOnly="True" SortExpression="ASN" />
                                        <asp:BoundField DataField="PONum" HeaderText="PONum" SortExpression="PONum" />
                                        <asp:BoundField DataField="itemDescription" HeaderText="itemDescription" ReadOnly="True" SortExpression="itemDescription" />
                                         <asp:BoundField DataField="AWidth" HeaderText="AWidth" SortExpression="AWidth" />
                                         <asp:BoundField DataField="AShrink" HeaderText="AShrink" SortExpression="AShrink" />
                                         <asp:BoundField DataField="AShade" HeaderText="AShade" SortExpression="AShade" />
                                         <asp:BoundField DataField="SWeight" HeaderText="SWeight" SortExpression="SWeight" />                                        
                                        <asp:BoundField DataField="WidthGroup" HeaderText="WidthGroup" SortExpression="WidthGroup" />
                                        <asp:BoundField DataField="ShadeGroup" HeaderText="ShadeGroup" SortExpression="ShadeGroup" />
                                        <asp:BoundField DataField="ShrinkageGroup" HeaderText="ShrinkageGroup" SortExpression="ShrinkageGroup" />
                                        
                                         <asp:TemplateField HeaderText="AYard">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_AYard" runat="server"  CssClass="lbl_yard" Text='<%# Bind("AYard") %>'></asp:Label>
                                            </ItemTemplate>
                                             <FooterTemplate>
                                                  <asp:TextBox ID="txt_totalyard" CssClass="totalyardfooter" Width="70px"  runat="server"></asp:TextBox>

                                              </FooterTemplate>
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
                         </td>
                 </tr>
                 <tr>
                     <td>
                         <asp:Button ID="btn_confirmRolls" runat="server" OnClick="btn_confirmRolls_Click" Text="Confirm" />
                         <asp:Button ID="btn_cancel" runat="server" OnClick="btn_cancel_Click" Text="Cancel" />
                     </td>
                 </tr>
             </table>
          </asp:Panel>
             </ContentTemplate>
                        </asp:UpdatePanel>



       </td>
                </tr>

                <tr class="ButtonTR">
                    <td class="NormalTD">
                        &nbsp;</td>
                    <td class="NormalTD">
                        &nbsp;</td>
                    <td class="auto-style7" >

                        
                    </td>
                    <td class="NormalTD">
                        &nbsp;</td>
                    <td class="NormalTD">
                        <asp:Button ID="btn_saveDO" runat="server" OnClick="btn_saveDO_Click" Text="Save DO" style="height: 26px" />
                    </td>
                    <td class="NormalTD">
                        &nbsp;</td>
                    <td class="NormalTD">
                        &nbsp;</td>
                    <td class="NormalTD">
                        &nbsp;</td>
                    <td class="NormalTD">
                        &nbsp;</td>
                </tr>

    <tr >
                    <td colspan="9" >

                           <div id="Messaediv" runat="server">
                 


                           <asp:Label ID="lbl_msg" runat="server" Text="*"></asp:Label>


                     
               </div>
                        </td>
                </tr>

    <tr >
                    <td colspan="9" >
                    
         
         
     
</td>
                </tr>
            </table>

                </td>
            </tr>
        </table>
      
            
        
   
    <div class="footer">
        
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" 
                    SelectCommand="SELECT DISTINCT AtcNum, AtcId FROM AtcMaster WHERE (IsClosed = N'N') ORDER BY AtcNum, AtcId">
                </asp:SqlDataSource>
                    
               
                               
                <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT        FabricRollmaster.Roll_PK, FabricRollmaster.RollNum, SupplierDocumentMaster.SupplierDocnum + ' /' + SupplierDocumentMaster.AtracotrackingNum AS ASN, ProcurementMaster.PONum, 
                         ISNULL(SkuRawMaterialMaster.Composition, ' ') + ' ' + ISNULL(SkuRawMaterialMaster.Construction, ' ') + ' ' + ISNULL(SkuRawMaterialMaster.Weight, ' ') + ' ' + ISNULL(SkuRawMaterialMaster.Width, ' ') 
                         + ' ' + ISNULL(ProcurementDetails.SupplierSize, ' ') + ' ' + ISNULL(ProcurementDetails.SupplierColor, ' ') AS itemDescription, FabricRollmaster.WidthGroup, FabricRollmaster.ShadeGroup, 
                         FabricRollmaster.ShrinkageGroup, FabricRollmaster.AYard, FabricRollmaster.SkuDet_PK
FROM            SkuRawMaterialMaster INNER JOIN
                         SkuRawmaterialDetail ON SkuRawMaterialMaster.Sku_Pk = SkuRawmaterialDetail.Sku_PK INNER JOIN
                         FabricRollmaster ON SkuRawmaterialDetail.SkuDet_PK = FabricRollmaster.SkuDet_PK INNER JOIN
                         ProcurementDetails ON FabricRollmaster.podet_pk = ProcurementDetails.PODet_PK INNER JOIN
                         SupplierDocumentMaster ON FabricRollmaster.SupplierDoc_pk = SupplierDocumentMaster.SupplierDoc_pk INNER JOIN
                         ProcurementMaster ON ProcurementDetails.PO_Pk = ProcurementMaster.PO_Pk INNER JOIN
                         AtcMaster ON ProcurementMaster.AtcId = AtcMaster.AtcId INNER JOIN
                         SupplierMaster ON SupplierDocumentMaster.Supplier_pk = SupplierMaster.Supplier_PK"></asp:SqlDataSource>
                <br />
                    
               
                               
    </div>
</asp:Content>


<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="AtcChart.aspx.cs" Inherits="ArtWebApp.Reports.MerchandiserReport.AtcChart" %>
<%@ Register assembly="Infragistics35.Web.v12.1, Version=12.1.20121.2236, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.ListControls" tagprefix="ig" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../css/style.css" rel="stylesheet" />
<style type="text/css">
body
{
    margin: 0;
    padding: 0;
    font-family: Arial;
}
.modal
{
    position: fixed;
    z-index: 999;
    height: 100%;
    width: 100%;
    top: 0;
    background-color: Black;
    filter: alpha(opacity=60);
    opacity: 0.6;
    -moz-opacity: 0.8;
}
.center
{
    z-index: 1000;
    margin: 300px auto;
    padding: 10px;
    width: 130px;
    background-color: White;
    border-radius: 10px;
    filter: alpha(opacity=100);
    opacity: 1;
    -moz-opacity: 1;
}
.center img
{
    height: 128px;
    width: 128px;
}

   

    .auto-style12 {
        width: 100%;
    }

   

  

   

    </style>
   <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
      <script type="text/javascript">

        
          $(document).ready(function () {


              $('[id*=btnGet]').click(function () {
                  debugger

                  var $checkedvalue = $("input:checkbox:not(:checked)")

                  var nonchecked_checkboxes = $("[id*=CheckBoxList1] input:checkbox:not(:checked)");

                  nonchecked_checkboxes.each(function () {
                      var $value = $(this).val();
                      $(".tbl_onhand tr:contains('" + $value + "')").hide();
                  });


                  var nonchecked_checkboxes = $("[id*=CheckBoxList1] input:checked");
                  nonchecked_checkboxes.each(function () {
                      var $value = $(this).val();
                      $(".tbl_onhand tr:contains('" + $value + "')").show();
                  });

                  calculateSumofOnHand();



                  return false
              });




          });


          function calculateSumofOnHand() {


              var onhandtablegroup = document.getElementsByClassName('tbl_onhand');

              for (var i = 0; i < onhandtablegroup.length; i++) {
                  var onhandtable = onhandtablegroup[i];


                  var lbl_OnhandQty = onhandtable.getElementsByClassName('lbl_OnhandQty');
                  //  var lbl_LocationPrefixTotal = onhandtable.getElementsByClassName('lbl_LocationPrefixTotal');
                  var lbl_onhandQtyTotalclass = onhandtable.getElementsByClassName('lbl_onhandQtyTotalclass');


                  var maingridrow = onhandtable.parentNode.parentNode.parentNode;


                  var RqdQty = maingridrow.getElementsByClassName('RqdQty')[0];
                  var lbl_pendingOnhand = maingridrow.getElementsByClassName('lbl_pendingOnhand')[0];
                  var OnhandQty = 0

                  for (var j = 0 ; j < lbl_OnhandQty.length; j++) {
                      var tr = lbl_OnhandQty[j].parentNode.parentNode;

                      if (tr.style.display != "none") {
                          OnhandQty = OnhandQty + parseFloat(lbl_OnhandQty[j].innerHTML);
                      }


                  }

                  // lbl_LocationPrefixTotal[0].innerHTML = OnhandQty.toString();
                  lbl_onhandQtyTotalclass[0].innerHTML = OnhandQty.toString();

                  var BaltoreceiveQty = parseFloat(RqdQty.innerHTML.toString()) - parseFloat(OnhandQty.toString());
                  lbl_pendingOnhand.innerHTML = BaltoreceiveQty.toString();
              }


          }


      </script>

   
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <%--  <div class=""> ATC CHART</div>--%>

     <div class="FullTable"> <asp:UpdatePanel ID="upd_buttons" UpdateMode="Conditional" ChildrenAsTriggers="false" runat="server">
                                 <ContentTemplate>
                        <table class="DataEntryTable">
                            <tr>
                                <td class="RedHeaddingdIV" colspan="6" >ATC CHART</td>
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
                                  <td class="NormalTD">pROJ qTY:</td>
                                  <td class="NormalTD">
                                      <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                                          <ContentTemplate>
                                              <asp:Label ID="lbl_qty" runat="server" Font-Size="Smaller" Text="0"></asp:Label>
                                          </ContentTemplate>
                                      </asp:UpdatePanel>
                                  </td>
                                 <td class="NormalTD"></td>
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
                                      <td class="NormalTD">pcd</td>
                                      <td class="NormalTD">
                                         
                                          <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                              <ContentTemplate>
                                                  <asp:Label ID="lbl_pcd" runat="server" Font-Size="Smaller" Text="0"></asp:Label>
                                              </ContentTemplate>
                                          </asp:UpdatePanel>
                                      </td>
                                     <td class="NormalTD"></td>
                                 
                            </tr>
                            
                            <tr>
                               <td class="NormalTD">&nbsp;</td>
                                <td class="NormalTD" colspan="3">
                                    &nbsp;</td>
                                
                                <td class="NormalTD">&nbsp;</td>
                               <td class="NormalTD">&nbsp;</td>
                            </tr>
                            
                        </table>

                        </ContentTemplate> </asp:UpdatePanel></div>

    <div >
        <table class="gridtable" >
                            <tr>
                               <td class="NormalTD"><asp:CheckBox ID="chk_f" Text=" OnhandQty F only" runat="server" /></td>
                               <td class="NormalTD"><asp:CheckBox ID="chk_W" Text="OnhandQty W only" runat="server" /></td>
                               <td class="NormalTD"><asp:CheckBox ID="chk_ct" Text="Show Cutorder" runat="server"  /></td>
                                 <td class="NormalTD"><asp:CheckBox ID="chk_doc" Text="Show ADN" runat="server"  /></td>
                                 <td class="NormalTD"><asp:CheckBox ID="chk_remark" Text="Show Remark" runat="server"  /></td>
                                 <td class="NormalTD"><asp:CheckBox ID="chk_rcpt" Text="Show Store Receipt" runat="server"  /></td>
                               <td class="NormalTD">
                                    
                                    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Export to Excel" />
                                </td>
                               <td class="NormalTD">&nbsp;</td>
                               <td class="NormalTD">&nbsp;</td>
                            </tr>
                            <tr>
                                <td colspan="8">
                                    <Table>
                                        <tr>
                                            <td>
                                                 <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                              <ContentTemplate>
                                                <asp:CheckBoxList ID="CheckBoxList1" runat="server" RepeatDirection="Horizontal">
                                        

                                        
                                    </asp:CheckBoxList>  </ContentTemplate> </asp:UpdatePanel></td>
                                             <td>
                                                 <asp:Button ID="btnGet" runat="server" CssClass="auto-style13" Text="Show" />
                                            </td>
                                        </tr>
                                    </Table>
                                    
                                   
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
                                <asp:BoundField DataField="UnitRate" HeaderText="Unit Rate" ReadOnly="True" SortExpression="UnitRate" />
                                <asp:BoundField DataField="GarmentQty" HeaderText="Garment Qty" ReadOnly="True" SortExpression="GarmentQty" />
                                <asp:BoundField DataField="Consumption" HeaderText="Consumption" ReadOnly="True" SortExpression="Consumption" />
                                <asp:BoundField DataField="WastagePercentage" HeaderText="Wastage %" SortExpression="WastagePercentage" />
                              
                                <asp:TemplateField HeaderText="RqdQty" SortExpression="PoIssuedQty">
                                  
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_RqdQty" runat="server" Text='<%# Bind("RqdQty") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="UomCode" HeaderText="Uom" SortExpression="UomCode" />
                                <asp:TemplateField HeaderText="Planned Qty" SortExpression="PlannedQty">
                                    
                                     <ItemTemplate>
                                         <asp:Label ID="lbl_plannedqty" runat="server" ></asp:Label>
                                     </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Planned Details" SortExpression="BalanceQty">
                                     <ItemTemplate>
                                         
                                                    <asp:GridView ID="tbl_eta" runat="server" AutoGenerateColumns="False" BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2" Font-Size="Smaller">
                                             <Columns>
                                                 <asp:BoundField DataField="Qty" HeaderText="Qty" />
                                                 <asp:BoundField DataField="ETADate" HeaderText="ETA" DataFormatString="{0:MM/dd/yyyy}"  />
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
                                <asp:TemplateField HeaderText="balance To Plan" SortExpression="BalanceQty">
                                    
                                     <ItemTemplate>
                                         <asp:Label ID="lbl_balplanqty" runat="server" Text="0"></asp:Label>
                                     </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Po Issued Qty" SortExpression="PoIssuedQty">
                                  
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_poissuedqty" runat="server" Text='<%# Bind("PoIssuedQty") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="PO Details">
                                     <ItemTemplate>
                                         <asp:GridView ID="tbl_PO" runat="server" AutoGenerateColumns="False" BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2" Font-Size="Smaller">
                                             <Columns>
                                                 <asp:BoundField DataField="PONum" HeaderText="PONum" />
                                               
                                                 <asp:BoundField DataField="POQty" HeaderText="POQty" />
                                                 <asp:BoundField DataField="UomCode" HeaderText="UomCode" />
                                                    <asp:BoundField DataField="SupplierName" HeaderText="SupplierName" />
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
                                <asp:BoundField DataField="BalanceQty" HeaderText="Balance Qty" ReadOnly="True" SortExpression="BalanceQty" />
                          


                                 
                          
                                 
                          
                                <asp:TemplateField HeaderText="ADN Details">
                                     <ItemTemplate>
                                         <asp:GridView ID="tbl_ADN" runat="server" AutoGenerateColumns="False" BackColor="#DEBA84" 
                                             BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2" Font-Size="Smaller">
                                             <Columns>
                                                 <asp:BoundField DataField="DocNum" HeaderText="ADN" />
                                                <asp:BoundField DataField="ContainerNum" HeaderText="Ref#" />
                                                 <asp:BoundField DataField="BOENum" HeaderText="Cont#" />
                                                 <asp:BoundField DataField="PONum" HeaderText="PONum" />
                                                 <asp:BoundField DataField="Qty" HeaderText="Qty" />
                                                  <asp:BoundField DataField="ExtraQty" HeaderText="Extra" />                                                 
                                                  <asp:BoundField DataField="ADNType" HeaderText="ADNType" />
                                        
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
                                 


                                 
                          

                                 <asp:TemplateField HeaderText="Shipping Details" >
                                     <ItemTemplate>
                                         <asp:GridView ID="tbl_shipping" runat="server" AutoGenerateColumns="False" BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2" Font-Size="Smaller">
                                             <Columns>
                                                 <asp:BoundField DataField="ShipperInv" HeaderText="Inv" />
                                                 <asp:BoundField DataField="ETA" HeaderText="ETA" DataFormatString="{0:MM/dd/yyyy}" />
                                                 <asp:BoundField DataField="Conatianer" HeaderText="Conatianer" />
                                                 <asp:BoundField DataField="Qty" HeaderText="Qty" />
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
                           
                                  
                                     <asp:TemplateField HeaderText="Onhand Details">
                                     <ItemTemplate>
                                         <asp:GridView ID="tbl_onhand" CssClass="tbl_onhand"  runat="server" AutoGenerateColumns="False" BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2" Font-Size="Smaller" ShowFooter="True" OnRowDataBound="tbl_onhand_RowDataBound">
                                             <Columns>
                                                

                                                 <asp:TemplateField HeaderText="LocationPrefix">
                                                   <ItemTemplate>
                                                         <asp:Label ID="lbl_LocationPrefix" CssClass="lbl_LocationPrefix" runat="server" Text='<%# Bind("LocationPrefix") %>'></asp:Label>
                                                     </ItemTemplate>
                                                       <FooterTemplate>
                                          <asp:Label ID="lbl_LocationPrefixTotal" CssClass="lbl_LocationPrefixTotal" runat="server" Text="0"></asp:Label>
                                      </FooterTemplate>
                                                 </asp:TemplateField>
                                          
                                                 <asp:TemplateField HeaderText="Onhand Qty">
                                                   <ItemTemplate>
                                                         <asp:Label ID="lbl_OnhandQty" CssClass="lbl_OnhandQty" runat="server" Text='<%# Bind("OnhandQty") %>'></asp:Label>
                                                     </ItemTemplate>
                                                       <FooterTemplate>
                                          <asp:Label ID="lbl_onhandQtyTotal" CssClass="lbl_onhandQtyTotalclass" runat="server" Text="0"></asp:Label>
                                      </FooterTemplate>
                                                 </asp:TemplateField>
                                          
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
                                  <asp:TemplateField HeaderText="Pending to Recieve">
                                   
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_pendingOnhand" CssClass="lbl_pendingOnhand" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Transist Details">
                                     <ItemTemplate>
                                         <asp:GridView ID="tbl_transist" runat="server" AutoGenerateColumns="False" BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2" Font-Size="Smaller" OnRowDataBound="tbl_transist_RowDataBound">
                                             <Columns>



                                                 <asp:BoundField DataField="LocationPrefix" HeaderText="TO" />
                                               <asp:BoundField DataField="DONum" HeaderText="From" />
                                                 <asp:TemplateField HeaderText="Transit Qty">
                                                        <FooterTemplate>
                                          <asp:Label ID="lbl_transistQtyTotal" runat="server" Text="0"></asp:Label>
                                      </FooterTemplate>
                                                     <ItemTemplate>
                                                         <asp:Label ID="Label1" runat="server" Text='<%# Bind("TransitQty") %>'></asp:Label>
                                                     </ItemTemplate>
                                                 </asp:TemplateField>
                                          
                                                 
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



                                                                


                                                                                   <asp:TemplateField HeaderText="Receipt Details">
                                     <ItemTemplate>
                                         <asp:GridView ID="tbl_Rcpt" runat="server" AutoGenerateColumns="False" BackColor="#DEBA84" 
                                             BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2" Font-Size="Smaller">
                                             <Columns>
                                                 <asp:BoundField DataField="MrnNum" HeaderText="Ref" />
                                                <asp:BoundField DataField="Qty" HeaderText="QTY" />
                                                 <asp:BoundField DataField="UomCode" HeaderText="UOM" />
                                                 <asp:BoundField DataField="PONum" HeaderText="Against" />
                                                  <asp:BoundField DataField="LocationPrefix" HeaderText="At" />
                                                 
                                          
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

                                <asp:TemplateField HeaderText="Remark">
                                     <ItemTemplate>
                                         <asp:GridView ID="tbl_Remark" runat="server" AutoGenerateColumns="False" BackColor="#DEBA84" 
                                             BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2" Font-Size="Smaller">
                                             <Columns>
                                                 <asp:BoundField DataField="Remark" HeaderText="Remark" />
                                                <asp:BoundField DataField="AddedDate" HeaderText="AddedDate"  DataFormatString="{0:MM/dd/yyyy}"  />
                                                 <asp:BoundField DataField="AddedBy" HeaderText="AddedBy" />
                                                 
                                          
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
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="DeliveryOrderFactory.aspx.cs" Inherits="ArtWebApp.Inventory.DeliveryOrderFactory" %>
<%@ Register assembly="Infragistics35.Web.v12.1, Version=12.1.20121.2236, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.ListControls" tagprefix="ig" %>
<%@ Register assembly="Infragistics35.Web.v12.1, Version=12.1.20121.2236, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.EditorControls" tagprefix="ig" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     


    <link href="../../css/style.css" rel="stylesheet" />
    <script src="../../JQuery/GridJQuery.js"></script>

      <script type="text/javascript">

     
          var submit = 0;
          function CheckDouble() {
              if (++submit > 1) {
                  alert('This sometimes takes a few seconds - please be patient.');
                  return false;
              }
          }
    



</script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     
        <table class="FullTable">

            <tr>
                <td class="NormalTD">
<table class="DataEntryTable">
                <tr>
                    <td class="RedHeadding" colspan="9">
                        wf transfernote(trims)</td>
                </tr>
                
                <tr>
                    <td class="NormalTD">
                        Order No</td>
                    <td class="NormalTD">
                             
                    <ucc:DropDownListChosen ID="cmb_atc" runat="server" DataSourceID="SqlDataSource1" DataTextField="AtcNum" DataValueField="AtcId" DisableSearchThreshold="10" Width="200px">
                        </ucc:DropDownListChosen>
                    
               
                               
                    </td>
                    <td >
                        <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Conditional" runat="server">
                            <ContentTemplate>
                                <asp:Button ID="btn_confirmAtc" runat="server" Text="S" Width="33px" OnClick="btn_confirmAtc_Click" CssClass="auto-style10" />
                                 </ContentTemplate>
                        </asp:UpdatePanel>
                         
                        
                    </td>
                    <td class="NormalTD">
                        D O Date:</td>
                    <td class="NormalTD">
                        <ig:WebDatePicker ID="dtp_dodate" runat="server" Height="26px" Width="191px">
                        </ig:WebDatePicker>
                    </td>
                    <td class="NormalTD">
                        &nbsp;</td>
                    <td class="NormalTD">
                        
                    </td>
                    <td c>
                        &nbsp;</td>
                    <td class="NormalTD">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="NormalTD">
                        do tYPE</td>
                    <td class="NormalTD">
                             
                        
                        <ucc:DropDownListChosen ID="cmb_suppliertype" Width="150px" runat="server">
                            <asp:ListItem Value="F">Fabric</asp:ListItem>
                            <asp:ListItem Value="T">Trims</asp:ListItem>
                             <asp:ListItem Value="SP">Spare Parts"</asp:ListItem>
                            <asp:ListItem Value="O">Others</asp:ListItem>
                             <asp:ListItem Value="S">Service</asp:ListItem>
                            
                        </ucc:DropDownListChosen>
                    </td>
                    <td class="NormalTD" >
                        &nbsp;</td>
                    <td class="NormalTD">
                        cONTAINER nO : </td>
                    <td class="NormalTD">
                        <asp:TextBox ID="txt_containernum" runat="server" Height="20px" Width="164px" CssClass="auto-style10"></asp:TextBox></td>
                    <td class="NormalTD">
                        &nbsp;</td>
                    <td class="NormalTD">
                        &nbsp;</td>
                    <td c>
                        &nbsp;</td>
                    <td class="NormalTD">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="NormalTD">
                        To</td>
                    <td class="NormalTD">
                        <ucc:DropDownListChosen ID="drp_ToWarehouse" runat="server" Width="200px">
                        </ucc:DropDownListChosen>
                    </td>
                   
                    <td  >
                        &nbsp;</td>
                    
                    <td class="NormalTD">
                        BOE No</td>
                    <td class="NormalTD">
                        <asp:TextBox ID="txt_BOE_no" runat="server" Height="20px" Width="164px" CssClass="auto-style10"></asp:TextBox>
                    </td>
                    <td class="NormalTD">
                        Mode:</td>
                    <td class="NormalTD">
                          <ucc:DropDownListChosen ID="drp_deliverymode" runat="server" Width="200px">
                        </ucc:DropDownListChosen>
                    </td>
                     <td class="NormalTD">
                         &nbsp;</td>
                    <td class="NormalTD">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="NormalTD">
                        Trims Type</td>
                    <td class="NormalTD">
                        <ucc:DropDownListChosen ID="DropDownListChosen1" Width="150px" runat="server">
                            <asp:ListItem Value="F">Sewing</asp:ListItem>
                            <asp:ListItem Value="T">Packing</asp:ListItem>
                        </ucc:DropDownListChosen></td>
                   
                    <td  >
                         <asp:UpdatePanel ID="UpdatePanel4" UpdateMode="Conditional" runat="server">
                            <ContentTemplate>
                                <asp:Button ID="btn_trimstype" runat="server" Text="S" Width="33px" OnClick="btn_trimstype_Click" CssClass="auto-style10" />
                                 </ContentTemplate>
                        </asp:UpdatePanel></td>
                    
                    <td class="NormalTD">
                        Issue No#</td>
                    <td class="NormalTD">
                        <ucc:DropDownListChosen ID="drp_issueno" runat="server" Width="200px">
                        </ucc:DropDownListChosen></td>
                    <td class="NormalTD">
                       <%-- <asp:UpdatePanel ID="UpdatePanel3" UpdateMode="Conditional" runat="server">
                            <ContentTemplate>
                                <asp:Button ID="btn_issueno" runat="server" Text="S" Width="33px" OnClick="btn_issueno_Click" CssClass="auto-style10" />
                                 </ContentTemplate>
                        </asp:UpdatePanel>--%>

                    </td>
                    <td class="NormalTD">
                          &nbsp;</td>
                     <td class="NormalTD">
                         &nbsp;</td>
                    <td class="NormalTD">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="gridtable" colspan="9">
                        <asp:UpdatePanel ID="upd_grid" UpdateMode="Conditional" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="tbl_InverntoryDetails" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" style="font-size: small; font-family: Calibri; font-weight: 400;" Width="100%" BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4">
                                    <Columns>
                                       
                                          <asp:TemplateField>  
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="checkAll" runat ="server" />
                                    </HeaderTemplate>                                 
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chk_select" runat="server" AutoPostBack="True"  OnCheckedChanged="chk_select_CheckedChanged" />
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
                                        <asp:BoundField DataField="SupplierColor" HeaderText="SupplierColor" />
                                        <asp:BoundField DataField="Suppliersize" HeaderText="Suppliersize" />
                                        <asp:BoundField DataField="UOMCode" HeaderText="UOM" />
                                        <asp:BoundField DataField="Refnum" HeaderText="Rcvd Via" />
                                        <asp:BoundField DataField="ReceivedQty" HeaderText="RecievedQty" />
                                        <asp:BoundField DataField="DeliveredQty" HeaderText="DeliveredQty" />
                                         <asp:BoundField DataField="TotalOnhand" HeaderText="Total Onhand" />
                                          <asp:TemplateField HeaderText="Blocked Qty">
                                             
                                              <ItemTemplate>
                                                 
                                                  <asp:LinkButton ID="lnkbtn_mrn" Text='<%# Bind("BlockedQty") %>' runat="server" OnClick="lnkbtn_mrn_Click" ToolTip="Qty blocked by pending Ro or Loan"></asp:LinkButton>
                                              </ItemTemplate>
                                          </asp:TemplateField>
                                        <asp:TemplateField HeaderText="OnhandQty">
                                            
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_onhandQty" runat="server" Text='<%# Bind("OnhandQty") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="CTINo">
                                        <ItemTemplate>
                                                                                       
                                            <asp:UpdatePanel ID="upd_cutorder" UpdateMode="Conditional" runat="server">
                                                <ContentTemplate>
                                                    <ucc:DropDownListChosen  ID="ddl_cutorder"  Width="130px" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddl_cutorder_SelectedIndexChanged">
                                            </ucc:DropDownListChosen>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </ItemTemplate>
                                    
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="AllowedQty">
                                           
                                            <ItemTemplate>

                                                <asp:UpdatePanel ID="upd_allowedqty" UpdateMode="Conditional" runat="server">
                                                <ContentTemplate>
                                                      <asp:Label ID="lbl_allowedqty" runat="server"></asp:Label>
                                                      </ContentTemplate>
                                            </asp:UpdatePanel>
                                              
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="DelQty">
                                           
                                            <ItemTemplate>

                                                <asp:UpdatePanel ID="upd_delqty" UpdateMode="Conditional" runat="server">
                                                <ContentTemplate>
                                                      <asp:Label ID="lbl_delqty" runat="server"></asp:Label>
                                                      </ContentTemplate>
                                            </asp:UpdatePanel>
                                              
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="BalQty">
                                           
                                            <ItemTemplate>

                                                <asp:UpdatePanel ID="upd_balacetocut" UpdateMode="Conditional" runat="server">
                                                <ContentTemplate>
                                                      <asp:Label ID="lbl_balacetocut" runat="server"></asp:Label>
                                                      </ContentTemplate>
                                            </asp:UpdatePanel>
                                              
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        
                                        
                                        
                                        <asp:TemplateField HeaderText="DeliveryQty">
                                           
                                            <ItemTemplate>
                                                <asp:TextBox ID="txt_deliveryQty" Text="0" onkeypress="return isNumberKey(event,this)" runat="server"></asp:TextBox>
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
                                                       <asp:LinkButton ID="lnkFake" runat="server"></asp:LinkButton>
                         <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="lnkFake" CancelControlID="btnClose" 


 


PopupControlID="Panel1" DropShadow="True">


 


</asp:ModalPopupExtender>


 


<asp:Panel ID="Panel1" runat="server" CssClass="modalPopup" align="center" style = "display:none">

      <asp:UpdatePanel ID="upd_subgrid"   UpdateMode="Conditional" runat="server">
                     <ContentTemplate>
   <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4" ShowHeaderWhenEmpty="True" style="font-size: small; font-family: Calibri" Width="400px" ShowFooter="True">
                                <Columns>

                                    
                                    <asp:BoundField DataField="dOCNUM" HeaderText="Ref" />
                                   
                                    <asp:BoundField DataField="Qty" HeaderText="Qty" />
                                 
                                                           
                                    
                                    

                                   
                                </Columns>
                                <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" Font-Bold="true" />
                                <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="#FFFFCC" />
                                <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
                                <RowStyle BackColor="White" ForeColor="#330099" />
                                <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
                                <SortedAscendingCellStyle BackColor="#FEFCEB" />
                                <SortedAscendingHeaderStyle BackColor="#AF0101" />
                                <SortedDescendingCellStyle BackColor="#F6F0C0" />
                                <SortedDescendingHeaderStyle BackColor="#7E0000" />
                            </asp:GridView> <br />
    <asp:Button ID="btnClose" runat="server" Text="Close" />
                          </ContentTemplate>
                            </asp:UpdatePanel>
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
                    <td class="NormalTD">
                        &nbsp;</td>
                    <td class="NormalTD">
                        &nbsp;</td>
                    <td class="NormalTD">
                        <asp:Button ID="btn_saveDO" runat="server" OnClick="btn_saveDO_Click" OnClientClick="return CheckDouble();"  Text="Save DO" style="height: 26px" />
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
                <tr class="ButtonTR">
                    <td colspan="7" >
                         <div id="Messaediv" runat="server">
                 


                           <asp:Label ID="lbl_msg" runat="server" Text="*"></asp:Label>


                     
               </div></td>
                    <td class="NormalTD">
                        &nbsp;</td>
                    <td class="NormalTD">
                        &nbsp;</td>
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
                    
               
                               
    </div>
</asp:Content>

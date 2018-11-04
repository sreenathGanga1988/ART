<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="DeliveryOrderCountry.aspx.cs" Inherits="ArtWebApp.Inventory.DeliveryOrderCountry" %>
<%@ Register assembly="Infragistics35.Web.v12.1, Version=12.1.20121.2236, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.ListControls" tagprefix="ig" %>
<%@ Register assembly="Infragistics35.Web.v12.1, Version=12.1.20121.2236, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.EditorControls" tagprefix="ig" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--    <style type="text/css">
      
        
      
    </style>--%>
    
    <link href="../../css/style.css" rel="stylesheet" />
    <script src="../../JQuery/GridJQuery.js"></script>

   
    <script type="text/javascript" >

        function calculateSum()
        {       
            
           
            GetSumofSelectedTextboxinFooterTextbox('dataentry', 'IntegerTextbox', 'txt_deliveryfooter');
        }


        function Onselection(objref)
        {
            Check_Click(objref)
            GetSumofSelectedTextboxinFooterTextbox('dataentry', 'IntegerTextbox', 'txt_deliveryfooter');
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
                        transfer notes(Fabric &amp; Trims)(ww)</td>
                </tr>
                <tr>
                    <td class="NormalTD">
                        Order No</td>
                    <td class="NormalTD">
                             
                        <ucc:DropDownListChosen ID="cmb_atc" runat="server" DataSourceID="SqlDataSource1" DataTextField="AtcNum" DataValueField="AtcId" DisableSearchThreshold="10" Width="200px" OnSelectedIndexChanged="cmb_atc_SelectedIndexChanged">
                        </ucc:DropDownListChosen>
                    
               
                               
                    </td>
                    <td class="NormalTD" >
                        <asp:Button ID="btn_confirmAtc" runat="server" Text="S" Width="33px" OnClick="btn_confirmAtc_Click" CssClass="auto-style10" />
                    </td>
                    <td class="NormalTD">
                        D O Date:</td>
                    <td  class="NormalTD" >
                        <ig:WebDatePicker ID="dtp_dodate" runat="server" Height="26px" Width="191px">
                        </ig:WebDatePicker>
                    </td>
                    <td class="NormalTD">
                        &nbsp;LOCALVEHICLE No:</td>
                    <td class="NormalTD">
                        <asp:TextBox ID="txt_containernum" runat="server" Height="20px" Width="164px" CssClass="auto-style10"></asp:TextBox>
                    </td>
                    <td c>
                        EXP REF</td>
                    <td class="NormalTD">
                        <ucc:DropDownListChosen ID="drp_expref" runat="server" Width="200px" DataSourceID="exprefdata" DataTextField="ContainerNumer" DataValueField="Container_PK" DisableSearchThreshold="10">
                        </ucc:DropDownListChosen>
                    </td>
                </tr>
                <tr>
                    <td class="NormalTD">
                        From</td>
                    <td class="NormalTD">
                        <ucc:DropDownListChosen ID="drp_fromWarehouse" runat="server" Width="200px">
                        </ucc:DropDownListChosen>
                    </td>
                    <td class="auto-style7" >
                        &nbsp;</td>
                    <td class="NormalTD">
                        To</td>
                    <td class="NormalTD">
                        <ucc:DropDownListChosen ID="drp_ToWarehouse" runat="server" Width="200px">
                        </ucc:DropDownListChosen>
                    </td>
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
                </tr>
                <tr>
                    <td class="NormalTD">
                        REMARKS</td>
                    <td class="NormalTD">
                        <asp:TextBox ID="txt_remarks" runat="server" Height="20px" Width="164px" CssClass="auto-style10"></asp:TextBox></td>
                    <td class="auto-style7" >
                        &nbsp;</td>
                    <td class="NormalTD">
                        &nbsp;</td>
                    <td class="NormalTD">
                        &nbsp;</td>
                    <td class="NormalTD">
                        &nbsp;</td>
                    <td class="NormalTD">
                        &nbsp;</td>
                    <td class="NormalTD">
                        &nbsp;</td>
                    <td class="NormalTD">
                        

                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="gridtable" colspan="9">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="tbl_InverntoryDetails" CssClass="dataentry" ShowFooter="True" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" style="font-size: small; font-family: Calibri; font-weight: 400;" Width="100%" BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4">
                                    <Columns>
                            

                                          <asp:TemplateField>  
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="checkAll" runat ="server" onclick="checkAll(this)"/>
                                    </HeaderTemplate>                                 
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chk_select" runat="server" onclick="Onselection(this)"/>
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
                                        <asp:BoundField DataField="ReceivedQty" HeaderText="RecievedQty" />
                                        <asp:BoundField DataField="DeliveredQty" HeaderText="DeliveredQty" />
                                         <asp:BoundField DataField="TotalOnhand" HeaderText="Total Onhand" />
                                          <asp:TemplateField HeaderText="Blocked Qty">
                                             
                                              <ItemTemplate>
                                                 
                                                  <asp:LinkButton ID="lnkbtn_mrn" Text='<%# Bind("BlockedQty") %>' runat="server" OnClick="lnkbtn_mrn_Click">Show MRN</asp:LinkButton>
                                              </ItemTemplate>
                                          </asp:TemplateField>
                                        <asp:TemplateField HeaderText="OnhandQty">
                                            
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_onhandQty" runat="server" Text='<%# Bind("OnhandQty") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="DeliveryQty">
                                           
                                            <ItemTemplate>
                                                <asp:TextBox ID="txt_deliveryQty" CssClass="IntegerTextbox" Text="0" onkeypress="return isNumberKey(event,this)"  OnChange="calculateSum()" runat="server"></asp:TextBox>
                                            </ItemTemplate>
                                                   <FooterTemplate>
                                                     <asp:TextBox ID="txt_deliveryfooter" Width="50px" CssClass="txt_deliveryfooter" runat="server"></asp:TextBox>
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
                    <td class="auto-style7" >
                        &nbsp;</td>
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
                <tr class="ButtonTR">
                    <td colspan="9" >
                       <div id="Messaediv" runat="server">
                 


                           <asp:Label ID="lbl_msg" runat="server" Text="*"></asp:Label>


                     
               </div></td>
                </tr>
            </table>

                </td>
            </tr>
        </table>
      
            
        
   
    <div class="footer">
        
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" 
                    SelectCommand="SELECT DISTINCT AtcNum, AtcId FROM AtcMaster WHERE (IsClosed = N'N') AND (IsMCRDone IS NULL) ORDER BY AtcNum, AtcId">
                </asp:SqlDataSource>
                    
               
                               
                <asp:SqlDataSource ID="exprefdata" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT [Container_PK], [ContainerNumer] FROM [ContainerMaster] WHERE ([IsCompleted] = @IsCompleted)">
                    <SelectParameters>
                        <asp:Parameter DefaultValue="N" Name="IsCompleted" Type="String" />
                    </SelectParameters>
                </asp:SqlDataSource>
                    
               
                               
    </div>

    
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="SupplierInvoicing.aspx.cs" Inherits="ArtWebApp.Accounts.SupplierInvoicing" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../css/style.css" rel="stylesheet" />
    <script src="../JQuery/GridJQuery.js"></script>
    <style type="text/css">
    .modalBackground
    {
        background-color: Black;
        filter: alpha(opacity=90);
        opacity: 0.8;
    }
    .modalPopup
    {
        background-color: #FFFFFF;
        border-width: 3px;
        border-style: solid;
        border-color: black;
        padding-top: 10px;
        padding-left: 10px;
        width: 400px;
        height: 200px;
    }
        .auto-style1 {
            height: 27px;
            width: 76px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  
         <div class="RedHeaddingdIV">
            PAYABLE PO VOUCHER
        </div>
    <div>
        <table >
                    <tr>
                        <td class="NormalTD">Supplier :</td>
                        <td class="NormalTD"><asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                 <ContentTemplate>
                                     <ucc:DropDownListChosen ID="drp_supplier" runat="server" DataSourceID="supplierdata" DataTextField="SupplierName" DataValueField="Supplier_PK" DisableSearchThreshold="10">
                                     </ucc:DropDownListChosen>
                                 </ContentTemplate>
                             </asp:UpdatePanel></td>
                        <td class="NormalTD">
                            <asp:Button ID="Button1"   runat="server" Text="S" OnClick="Button1_Click" style="width: 23px" />
                        </td>
                        <td class="NormalTD">&nbsp;</td>
                        <td class="NormalTD">&nbsp;</td>
                        <td class="NormalTD">&nbsp;</td>
                        <td class="NormalTD">&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="NormalTD">Atc # :</td>
                        <td class="NormalTD">
                            <asp:UpdatePanel ID="upd_atc"  UpdateMode="Conditional" runat="server">
                                 <ContentTemplate>
                            
                            <ig:WebDropDown ID="cmb_atc"  runat="server" CurrentValue="Select Atc" DropDownAnimationType="EaseInOut" Font-Names="Calibri" Font-Size="X-Small" Height="24px" TextField="AtcNum" ValueField="AtcId" Width="190px" EnableMultipleSelection="True" EnableClosingDropDownOnSelect="False">
                                  <Items>
                                      <ig:DropDownItem Selected="False" Text="DropDown Item" Value="">
                                      </ig:DropDownItem>
                                  </Items>
                                  <DropDownItemBinding TextField="AtcNum" ValueField="AtcId" />
                              </ig:WebDropDown>
                                    </ContentTemplate>
                             </asp:UpdatePanel>  
                                     </td>
                        <td class="NormalTD"><asp:Button ID="Button2"   runat="server" Text="S" OnClick="Button2_Click" style="width: 23px" /></td>
                        <td class="NormalTD">&nbsp;</td>
                        <td class="NormalTD">&nbsp;</td>
                        <td class="NormalTD">&nbsp;</td>
                        <td class="NormalTD">&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="NormalTD">PO Num :</td>
                        <td class="NormalTD">                                        <asp:UpdatePanel ID="udp_drppo" UpdateMode="Conditional" runat="server">
                                            <ContentTemplate>
                                                <ig:WebDropDown ID="drp_po" runat="server" EnableMultipleSelection="True" Height="23px" style="height: 23px" TextField="Ponum" ValueField="PO_Pk" Width="190px" CurrentValue="Select PO" EnableClosingDropDownOnSelect="False">
                                                    <DropDownItemBinding TextField="Ponum" ValueField="PO_Pk" />
                                                </ig:WebDropDown>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                        <td class="NormalTD"><asp:Button ID="Button3"   runat="server" Text="S"  style="width: 23px" OnClick="Button3_Click" /></td>
                        <td class="NormalTD">&nbsp;</td>
                        <td class="NormalTD">&nbsp;</td>
                        <td class="NormalTD">&nbsp;</td>
                        <td class="NormalTD">&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="NormalTD">&nbsp;</td>
                        <td class="NormalTD">                                        
                            <asp:CheckBox ID="chk_advance" runat="server" Text="Advance" />
                                    </td>
                        <td class="NormalTD">&nbsp;</td>
                        <td class="NormalTD">&nbsp;</td>
                        <td class="NormalTD">&nbsp;</td>
                        <td class="NormalTD">&nbsp;</td>
                        <td class="NormalTD">&nbsp;</td>
                    </tr>
                    
                </table>

    </div>
    
    <table class="FullTable">

   

        <tr>
            <td>
                
            </td>
        </tr>
        <tr class="smallgridtable">
            <td>
                 <asp:UpdatePanel ID="upd_Grid"   UpdateMode="Conditional" runat="server">
                     <ContentTemplate>
                         <asp:GridView ID="tbl_Podetails" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4" ShowHeaderWhenEmpty="True" style="font-size: small; font-family: Calibri" Width="90%" ShowFooter="True" OnSelectedIndexChanged="tbl_Podetails_SelectedIndexChanged">
                                <Columns>

                                     <asp:TemplateField>  
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="checkAll" runat ="server" onclick="checkAll(this)"/>
                                    </HeaderTemplate>                                 
                                    <ItemTemplate>
                                        <asp:CheckBox ID="Chk_select" runat="server" onclick="Check_Click(this)"/>
                                    </ItemTemplate>
                                </asp:TemplateField>    

                                  
                                    <asp:TemplateField HeaderText="PoDet_PK" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_podet_pk" runat="server" Text='<%# Bind("PoDet_PK") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="hidden" />
                                        <ItemStyle CssClass="hidden" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-CssClass="hidden" HeaderText="SkuDet_pk" ItemStyle-CssClass="hidden">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_skudet_pk" runat="server" Text='<%# Bind("SkuDet_pk") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="hidden" />
                                        <ItemStyle CssClass="hidden" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Ponum" HeaderText="PO#" />
                                    <asp:BoundField DataField="RMNum" HeaderText="RMNum" />
                                    <asp:BoundField DataField="Description" HeaderText="Description" />
                                    <asp:BoundField DataField="ItemColor" HeaderText="ItemColor" />
                                    <asp:BoundField DataField="ItemSize" HeaderText="ItemSize" />
                                    <asp:BoundField DataField="SupplierColor" HeaderText="S Color" />
                                    <asp:BoundField DataField="Suppliersize" HeaderText="S Size" />                                  
                                    
                                     <asp:TemplateField HeaderText="POQty">
                                        
                                         <ItemTemplate>
                                             <asp:Label ID="Label2" runat="server" Width="70px"  Text='<%# Bind("POQty") %>'></asp:Label>
                                         </ItemTemplate>
         <%--                                  <FooterTemplate>
         <asp:TextBox ID="lbl_footerPOQTY" runat="server" Text="0" Enabled="false" >0</asp:TextBox>
    </FooterTemplate>--%>
                                         <FooterStyle BackColor="#CCFFFF" />
                                     </asp:TemplateField>
                                     <asp:BoundField DataField="UOMCode" HeaderText="UOM" />

                                    <asp:TemplateField HeaderText="UnitRate" >
                                        
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_porate"  runat="server" Text='<%# Bind("POUnitRate") %>'></asp:Label>
                                        </ItemTemplate>

                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Currency">
                                       
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_Currency" runat="server" Text='<%# Bind("CurrencyCode") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Rcvd Qty">
                                        
                                         <ItemTemplate>
                                             <asp:Label ID="Label1" runat="server" Width="70px"  Text='<%# Bind("ReceivedQty") %>'></asp:Label>
                                         </ItemTemplate>
                                         
                                         
      <%--                                   <FooterTemplate>
         <asp:TextBox ID="lbl_RcvdQty" runat="server" Text="0" Enabled="false" >0</asp:TextBox>
    </FooterTemplate>--%>
                                     </asp:TemplateField>
                                    <asp:BoundField DataField="BalToRecv" HeaderText="BalToRecv" /> 
                                    <asp:BoundField DataField="InvQty" HeaderText="InvQty" />
                                     
                                     <asp:TemplateField HeaderText="BaltoINV">
                                       
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_balncetoinvqty" runat="server" Text='<%# Bind("BaltoINV") %>'></asp:Label>
                                        </ItemTemplate>
                                        <%--  <FooterTemplate>
         <asp:TextBox ID="lbl_footerbalncetoinvqty" runat="server" Text="0" Enabled="false" >0</asp:TextBox>
    </FooterTemplate>--%>
                                         <FooterStyle BackColor="#CCFFFF" />
                                    </asp:TemplateField>

                                    
                                       <asp:TemplateField HeaderText="InvoiceQty">
                                       
                                        <ItemTemplate>
                                            <asp:TextBox ID="txt_invQty" runat="server" Width="70px" Text='<%# Bind("BaltoINV") %>'></asp:TextBox>
                                        </ItemTemplate>

                                           <HeaderStyle Width="70px" />
                                    </asp:TemplateField>


                                   
                                    <asp:BoundField DataField="ExtraQty" HeaderText="Extra Qty" >
                                     <HeaderStyle Width="70px" />
                                     <ItemStyle Width="70px" />
                                        <ControlStyle Width="70px" />
                                        <FooterStyle Width="70px" />
                                     </asp:BoundField>
                                    <asp:BoundField DataField="ExtraPer" HeaderText="ExtraPer">
                                      <HeaderStyle Width="70px" />
                                     <ItemStyle Width="70px" />
                                     </asp:BoundField>
                                    <asp:BoundField DataField="LastMRNDATE" HeaderText="Last MRN" DataFormatString="{0:MM/dd/yyyy}"  />
                                   
                                     <asp:TemplateField HeaderText="Show MRN">
                                         <ItemTemplate>
                                             <asp:LinkButton ID="lnkbtn_mrn" runat="server" OnClick="lnkbtn_mrn_Click">Show MRN</asp:LinkButton>
                                         </ItemTemplate>
                                     </asp:TemplateField>

                                   
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

                                    
                                    <asp:BoundField DataField="MrnNum" HeaderText="MrnNum" />
                                    <asp:BoundField DataField="AddedDate" HeaderText="AddedDate"  DataFormatString="{0:MM/dd/yyyy}"/>
                                    <asp:BoundField DataField="ReceiptQty" HeaderText="ReceiptQty" />
                                    <asp:BoundField DataField="ExtraQty" HeaderText="ExtraQty" />
                                                           
                                    
                                    

                                   
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
         <tr >
            <td>
                <asp:UpdatePanel ID="upd_confirmbtn" UpdateMode="Conditional" runat="server">
                                <ContentTemplate>
                <asp:Button ID="btn_confirminvdet" runat="server" Text="Confirm Invoice Details" OnClick="btn_confirminvdet_Click" />
                                    </ContentTemplate>
                            </asp:UpdatePanel>
             </td>
      
        </tr>
         <tr >
            <td class="DataEntryTable">
                <table class="tittlebar">
                    <tr>
                        <td class="NormalTD">Invoice Value</td>
                        <td class="NormalTD">
                            
                            <asp:UpdatePanel ID="upd_invvalue" UpdateMode="Conditional" runat="server">
                                <ContentTemplate>
                                    <asp:Label ID="lbl_invValue" runat="server" Text="0"></asp:Label>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td class="NormalTD">Invoice currency</td>
                        <td class="NormalTD">
                            
                           <asp:UpdatePanel ID="upd_currency" UpdateMode="Conditional" runat="server">
                                <ContentTemplate>
                                 

                                       <ucc:DropDownListChosen ID="drp_currency" runat="server" DataSourceID="currencydata" DataTextField="CurrencyCode" DataValueField="CurrencyID" DisableSearchThreshold="10" Width="200px">
                                     </ucc:DropDownListChosen>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td class="auto-style1">Invoice Qty</td>
                        <td class="NormalTD">
                            
                            <asp:UpdatePanel ID="Upd_invoiceqty" UpdateMode="Conditional" runat="server">
                                <ContentTemplate>
                                    <asp:Label ID="lbl_invqty" runat="server" Text="0"></asp:Label>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                         <td class="NormalTD">Remark</td>
                        <td class="NormalTD">
                            <asp:TextBox ID="txt_remark" runat="server"></asp:TextBox>
                         </td>
                        <td class="NormalTD">Invoice Date</td>
                        <td class="NormalTD">
                            <ig:WebDatePicker ID="dtp_invdate" runat="server">
                         </ig:WebDatePicker></td>
                        <td class="auto-style1">&nbsp;</td>
                        <td class="NormalTD">&nbsp;</td>
                    </tr>
                    <tr>
                         <td class="NormalTD">Account date</td>
                        <td class="NormalTD">
                            <ig:WebDatePicker ID="dtp_accountdate" runat="server">
                         </ig:WebDatePicker></td>
                        <td class="NormalTD">supplier inv#</td>
                        <td class="NormalTD">
                            <asp:TextBox ID="txt_suppinvoicenum" runat="server"></asp:TextBox>
                         </td>
                        <td class="auto-style1">&nbsp;</td>
                        <td class="NormalTD">&nbsp;</td>
                    </tr>
                </table>
             </td>
      
        </tr>
         <tr >
            <td>
                <asp:Button ID="Button4" runat="server" Text="Book Invoice" OnClick="Button4_Click" style="height: 26px" />
             </td>
      
        </tr>
         <tr >
            <td>
                <div id="Messaediv" runat="server">
                 


                           <asp:Label ID="lbl_msg" runat="server" Text="*"></asp:Label>


                     
               </div></td>
      
        </tr>
    </table>
        <asp:SqlDataSource ID="supplierdata" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT [SupplierName], [Supplier_PK] FROM [SupplierMaster] ORDER BY [SupplierName]"></asp:SqlDataSource>
        <asp:SqlDataSource ID="currencydata" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT * FROM [CurrencyMaster] ORDER BY [CurrencyCode], [CurrencyID]" ProviderName="<%$ ConnectionStrings:ArtConnectionString.ProviderName %>"></asp:SqlDataSource>
      <asp:SqlDataSource ID="BankDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT [Bank_PK], [BankName] FROM [BankMaster]"></asp:SqlDataSource>
</asp:Content>

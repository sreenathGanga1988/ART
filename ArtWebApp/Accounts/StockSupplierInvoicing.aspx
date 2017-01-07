<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="StockSupplierInvoicing.aspx.cs" Inherits="ArtWebApp.Accounts.StockSupplierInvoicing" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../css/style.css" rel="stylesheet" />
    <script src="../JQuery/GridJQuery.js"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="FullTable">
        <tr>
            <td class="RedHeadding">stock po Payable voucher</td>
        </tr>
        <tr>
            <td>
                <table class="DataEntryTable">
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
                        <td class="NormalTD">sPO Num :</td>
                        <td class="NormalTD">                                        <asp:UpdatePanel ID="udp_drppo" UpdateMode="Conditional" runat="server">
                                            <ContentTemplate>
                                                <ig:WebDropDown ID="drp_po" runat="server" EnableMultipleSelection="True" Height="23px" style="height: 23px" TextField="name" ValueField="pk" Width="190px"  EnableClosingDropDownOnSelect="False" CurrentValue="Select PO">
                                                    <DropDownItemBinding TextField="name" ValueField="pk" />
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
                        <td class="NormalTD"><asp:CheckBox ID="chk_advance" runat="server" Text="Advance" /></td>
                        <td class="NormalTD">                                        &nbsp;</td>
                        <td class="NormalTD">&nbsp;</td>
                        <td class="NormalTD">&nbsp;</td>
                        <td class="NormalTD">&nbsp;</td>
                        <td class="NormalTD">&nbsp;</td>
                        <td class="NormalTD">&nbsp;</td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr class="smallgridtable">
            <td>
                 <asp:UpdatePanel ID="upd_Grid"   UpdateMode="Conditional" runat="server">
                     <ContentTemplate>
                         <asp:GridView ID="tbl_Podetails" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4" ShowHeaderWhenEmpty="True" style="font-size: small; font-family: Calibri" Width="90%" DataKeyNames="SPODetails_PK" ShowFooter="True">
                                <Columns>
                                      <asp:TemplateField>  
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="checkAll" runat ="server" onclick="checkAll(this)"/>
                                    </HeaderTemplate>                                 
                                    <ItemTemplate>
                                        <asp:CheckBox ID="Chk_select" runat="server" onclick="Check_Click(this)"/>
                                    </ItemTemplate>
                                </asp:TemplateField>    
                                    <asp:TemplateField HeaderText="SPODetails_PK" InsertVisible="False" SortExpression="SPODetails_PK">
                                      
                                        <ItemTemplate>
                                           <asp:Label ID="lbl_podet_pk" runat="server" Text='<%# Eval("SPODetails_PK") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="SPONum" HeaderText="SPONum" SortExpression="SPONum" />
                                    <asp:BoundField DataField="ItemDescription" HeaderText="ItemDescription" ReadOnly="True" SortExpression="ItemDescription" />
                                    <asp:TemplateField HeaderText="POQty" SortExpression="POQty">
                                        
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_poQty" runat="server" Text='<%# Bind("POQty") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="UomCode" HeaderText="UomCode" SortExpression="UomCode" />
                                    <asp:TemplateField HeaderText="Unitprice" SortExpression="Unitprice">
                                      
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_porate" runat="server" Text='<%# Bind("Unitprice") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="CurrencyCode" SortExpression="CurrencyCode">
                                       
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_Currency" runat="server" Text='<%# Bind("CurrencyCode") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="RecievedQty" HeaderText="RecievedQty" SortExpression="RecievedQty" ReadOnly="True" />                                  
                                     <asp:BoundField DataField="BalancetoReceiveQty" HeaderText="Bal To Recv" />
                                    <asp:BoundField DataField="InvoicedQty" HeaderText="InvoicedQty" />
                                     <asp:TemplateField HeaderText="BalanceQty" SortExpression="BalanceQty">
                                        
                                         <ItemTemplate>
                                             <asp:Label ID="lbl_balncetoinvqty" runat="server" Text='<%# Bind("BalanceQty") %>'></asp:Label>
                                         </ItemTemplate>
                                         <FooterTemplate>
         <asp:TextBox ID="lbl_footerbalncetoinvqty" runat="server" Text="0" Enabled="false" >0</asp:TextBox>
    </FooterTemplate>
                                         <FooterStyle BackColor="#CCFFFF" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="INVQty" SortExpression="BalanceQty">
                                      
                                        <ItemTemplate>
                                            <asp:TextBox ID="txt_invQty" runat="server"  Text='<%# Bind("BalanceQty") %>' ></asp:TextBox>
                                        </ItemTemplate>
                                       
                                    </asp:TemplateField>
                                   


                                   
                                </Columns>
                                <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" BorderStyle="Solid" HorizontalAlign="Center" />
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
                        <td class="NormalTD">&nbsp;</td>
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
                        <td class="NormalTD">&nbsp;</td>
                    </tr>
                </table>
             </td>
      
        </tr>
         <tr >
            <td>
                <asp:Button ID="Button4" runat="server" Text="Book Invoice" OnClick="Button4_Click" />
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


<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="PurchaseHelper.aspx.cs" Inherits="ArtWebApp.Merchandiser.PurchaseHelper" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../css/style.css" rel="stylesheet" />
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
        .auto-style2 {
            background-color: #FF9966;
        }
        .auto-style3 {
            background-color: #99FF99;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="FullTable">
        <tr>
            <td class="RedHeadding">ITEM/PURCHASE TRACKER&nbsp;</td>
        </tr>
        <tr>
            <td><div>

                 <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                <table class="DataEntryTable">
                    <tr>
                        <td class="NormalTD">Item Description</td>
                        <td class="NormalTD">
                            <asp:TextBox ID="txt_itemdescription" runat="server" Font-Size="Smaller" Width="100%"></asp:TextBox>
                        </td>
                        <td class="SearchButtonTD">
                             <asp:UpdatePanel ID="upd_btn1" UpdateMode="Conditional" runat="server">
                                        <ContentTemplate>
                            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="S" />
                                              </ContentTemplate>
                                    </asp:UpdatePanel>
                        </td>
                        <td class="NormalTD">&nbsp;</td>
                        <td class="NormalTD">&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="NormalTD">&nbsp;</td>
                        <td class="NormalTD">&nbsp;</td>
                        <td class="SearchButtonTD">&nbsp;</td>
                        <td class="NormalTD">&nbsp;</td>
                        <td class="NormalTD">&nbsp;</td>
                    </tr>
                    <tr>
                      <td class="NormalTD">&nbsp;</td>
                        <td class="NormalTD">&nbsp;</td>
                        <td class="SearchButtonTD">&nbsp;</td>
                        <td class="NormalTD">&nbsp;</td>
                        <td class="NormalTD">&nbsp;</td>
                    </tr>
                </table>

   </ContentTemplate>
                                    </asp:UpdatePanel>
                </div></td>
        </tr>
        <tr>
            <td>
                <div>


                    <table class="DataEntryTable">
                    <tr class="RedHeadding">
                        <td class="gridtable"><strong>Templates Found</strong></td>
                        <td class="gridtable"><strong>Composition found</strong></td>
                        <td class="gridtable"><strong>cONSTRUCTION FOUND</strong></td>
                    </tr>
                    <tr>
                        <td >
                            <div class="gridtable">

                           
                             <asp:UpdatePanel ID="upd_templategrid" UpdateMode="Conditional" runat="server">
                                        <ContentTemplate>
                           

                                             <asp:GridView ID="tbl_template" runat="server" CellPadding="4" DataKeyNames="Template_PK" ForeColor="#333333" GridLines="None" AllowPaging="True" AutoGenerateColumns="False" OnRowCommand="tbl_template_RowCommand">
                                <AlternatingRowStyle BackColor="White" />
                                                  <Columns>
                                                      <asp:TemplateField HeaderText="Template_PK" SortExpression="Template_PK">
                                                          
                                                          <ItemTemplate>
                                                              <asp:Label ID="lbl_templatepk" runat="server" Text='<%# Bind("Template_PK") %>'></asp:Label>
                                                          </ItemTemplate>
                                                          <ControlStyle CssClass="hidden" />
                                                          <HeaderStyle CssClass="hidden" />
                                                          <ItemStyle CssClass="hidden" />
                                                      </asp:TemplateField>
                                    <asp:BoundField DataField="TemplateCode" HeaderText="Code" SortExpression="TemplateCode" />
                                    <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description" />
                               
                                                <asp:ButtonField Text="Show Purchase History"  CommandName="ShowPoHistory"  />
                                            </Columns>
                                <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                                <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                                <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                                <SortedAscendingCellStyle BackColor="#FDF5AC" />
                                <SortedAscendingHeaderStyle BackColor="#4D0000" />
                                <SortedDescendingCellStyle BackColor="#FCF6C0" />
                                <SortedDescendingHeaderStyle BackColor="#820000" />
                            </asp:GridView>  
                                               </ContentTemplate>
                                    </asp:UpdatePanel>
                                 </div>
                        </td>
                        <td >
                            <div class="gridtable">

                                <asp:UpdatePanel ID="upd_compositiongrid" UpdateMode="Conditional" runat="server">
                                        <ContentTemplate>
                            <asp:GridView ID="tbl_comp" runat="server" CellPadding="4" ForeColor="#333333" DataKeyNames="TemplateCom_Pk" GridLines="None" AllowPaging="True" AutoGenerateColumns="False" OnRowCommand="tbl_comp_RowCommand">
                                <AlternatingRowStyle BackColor="White" />
                                 <Columns>   
                                     <asp:TemplateField HeaderText="TemplateCom_Pk" SortExpression="TemplateCom_Pk">
                                       
                                         <ItemTemplate>
                                             <asp:Label ID="lbl_TemplateCom_Pk" runat="server" Text='<%# Bind("TemplateCom_Pk") %>'></asp:Label>
                                         </ItemTemplate>
                                         <ControlStyle CssClass="hidden" />
                                         <HeaderStyle CssClass="hidden" />
                                         <ItemStyle CssClass="hidden" />
                                     </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Template_PK" SortExpression="Template_PK">
                                       
                                         <ItemTemplate>
                                             <asp:Label ID="lbl_templatepk" runat="server" Text='<%# Bind("Template_PK") %>'></asp:Label>
                                         </ItemTemplate>
                                         <ControlStyle CssClass="hidden" />
                                         <HeaderStyle CssClass="hidden" />
                                         <ItemStyle CssClass="hidden" />
                                     </asp:TemplateField>
                                     
                                    <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description" />
                                     <asp:TemplateField HeaderText="Composition" SortExpression="Composition">
                                     
                                         <ItemTemplate>
                                             <asp:Label ID="lbl_composition" runat="server" Text='<%# Bind("Composition") %>'></asp:Label>
                                         </ItemTemplate>
                                     </asp:TemplateField>
                                   <asp:ButtonField Text="Show Purchase History" CommandName="ShowPoHistory" />
                                            </Columns>
                                <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                                <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                                <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                                <SortedAscendingCellStyle BackColor="#FDF5AC" />
                                <SortedAscendingHeaderStyle BackColor="#4D0000" />
                                <SortedDescendingCellStyle BackColor="#FCF6C0" />
                                <SortedDescendingHeaderStyle BackColor="#820000" />
                            </asp:GridView>  </ContentTemplate>
                                    </asp:UpdatePanel>

                            </div>
                              </td>
                        <td >

                            <div class="gridtable">
                                 <asp:UpdatePanel ID="upd_constructiongrid" UpdateMode="Conditional" runat="server">
                                        <ContentTemplate><asp:GridView ID="tbl_con" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" AllowPaging="True">
                                <AlternatingRowStyle BackColor="White" />
                                            <Columns>
                                                <asp:ButtonField Text="Show Purchase History" />
                                            </Columns>
                                <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                                <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                                <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                                <SortedAscendingCellStyle BackColor="#FDF5AC" />
                                <SortedAscendingHeaderStyle BackColor="#4D0000" />
                                <SortedDescendingCellStyle BackColor="#FCF6C0" />
                                <SortedDescendingHeaderStyle BackColor="#820000" />
                            </asp:GridView>  </ContentTemplate>
                                    </asp:UpdatePanel>

                            </div>
                             </td>
                    </tr>
                </table>

                </div>
                
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>
                <asp:Panel ID="Panel1" runat="server">
                    <asp:UpdatePanel ID="upd_pricedetail" UpdateMode="Conditional" runat="server">
                                        <ContentTemplate>

                    <div>

                        <table class="auto-style1">
                            <tr>
                                <td>NO OF PURCHASE WITHIN 1 YEAR</td>
                                <td>
                                    <asp:Label ID="Label2" runat="server" Text="0"></asp:Label>
                                </td>
                                <td>TOTAL PURCHASE VALUE(usd)</td>
                                <td><asp:Label ID="Label3" runat="server" Text="0"></asp:Label></td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                    <table class="auto-style1">
                                        <tr>
                                            <td class="auto-style2" colspan="2">hIGHEST </td>
                                            <td>&nbsp;</td>
                                            <td class="auto-style3" colspan="3">LOWEST</td>
                                        </tr>
                                        <tr>
                                            <td class="auto-style2">hIGHEST UNIT PRICE</td>
                                            <td class="auto-style2">
                                                <asp:Label ID="lbl_maxunitprice" runat="server" Text="0"></asp:Label>
                                            </td>
                                            <td>&nbsp;</td>
                                            <td class="auto-style3">lOWEST UNIT PRICE</td>
                                            <td class="auto-style3">
                                                <asp:Label ID="lbl_minunitprice" runat="server" Text="0"></asp:Label>
                                            </td>
                                            <td class="auto-style3">&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td class="auto-style2">sUPPLIER</td>
                                            <td class="auto-style2">
                                                <asp:Label ID="lbl_maxsuppier" runat="server" Text="0"></asp:Label>
                                            </td>
                                            <td>&nbsp;</td>
                                            <td class="auto-style3">SUPPLIER</td>
                                            <td class="auto-style3">
                                                <asp:Label ID="lbl_minsupplier" runat="server" Text="0"></asp:Label>
                                            </td>
                                            <td class="auto-style3">&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td class="auto-style2">PAYMENT TERM</td>
                                            <td class="auto-style2">
                                                <asp:Label ID="lbl_maxpaymentterm" runat="server" Text="0"></asp:Label>
                                            </td>
                                            <td>&nbsp;</td>
                                            <td class="auto-style3">PAYMENT TERM</td>
                                            <td class="auto-style3">
                                                <asp:Label ID="lbl_minpaymentterm" runat="server" Text="0"></asp:Label>
                                            </td>
                                            <td class="auto-style3">&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td class="auto-style2">aDDED dATE</td>
                                            <td class="auto-style2">
                                                <asp:Label ID="lbl_maxaDDEDDATE" runat="server" Text="0"></asp:Label>
                                            </td>
                                            <td>&nbsp;</td>
                                            <td class="auto-style3">ADDED DATE</td>
                                            <td class="auto-style3">
                                                <asp:Label ID="lbl_minaddedate" runat="server" Text="0"></asp:Label>
                                            </td>
                                            <td class="auto-style3">&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td class="auto-style2">Qty</td>
                                            <td class="auto-style2">
                                                <asp:Label ID="lbl_maxqty" runat="server" Text="0"></asp:Label>
                                            </td>
                                            <td>&nbsp;</td>
                                            <td class="auto-style3">QTY</td>
                                            <td class="auto-style3">
                                                <asp:Label ID="lbl_minqty" runat="server" Text="0"></asp:Label>
                                            </td>
                                            <td class="auto-style3">&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td class="auto-style2">UOM</td>
                                            <td class="auto-style2">
                                                <asp:Label ID="lbl_maxuom" runat="server" Text="0"></asp:Label>
                                            </td>
                                            <td>&nbsp;</td>
                                            <td class="auto-style3">UOM</td>
                                            <td class="auto-style3">
                                                <asp:Label ID="lbl_minuom" runat="server" Text="0"></asp:Label>
                                            </td>
                                            <td class="auto-style3">&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td class="auto-style2">Delivery term</td>
                                            <td class="auto-style2">
                                                <asp:Label ID="lbl_maxdeliveryterm" runat="server" Text="0"></asp:Label>
                                            </td>
                                            <td>&nbsp;</td>
                                            <td class="auto-style3">Delivery term</td>
                                            <td class="auto-style3">
                                                <asp:Label ID="lbl_mindeliveryterm" runat="server" Text="0"></asp:Label>
                                            </td>
                                            <td class="auto-style3">&nbsp;</td>
                                        </tr>
                                    </table>
                                </td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                        </table>

                    </div>
                      <div>
                          <asp:GridView ID="tbl_podetails" runat="server" AllowPaging="True" AutoGenerateColumns="False" CssClass="mydatagrid"  Font-Size="Smaller" HeaderStyle-CssClass="header" PagerStyle-CssClass="pager" RowStyle-CssClass="rows" >
                                
                              <Columns>
                                  <asp:BoundField DataField="SPONum" HeaderText="SPONum" SortExpression="SPONum" />
                                  <asp:BoundField DataField="SupplierName" HeaderText="SupplierName" SortExpression="SupplierName" />
                                  <asp:BoundField DataField="AddedDate" HeaderText="AddedDate" SortExpression="AddedDate"  DataFormatString="{0:MM/dd/yyyy}" />
                                  <asp:BoundField DataField="DeliveryDate" HeaderText="DeliveryDate" SortExpression="DeliveryDate"  DataFormatString="{0:MM/dd/yyyy}" />
                                  <asp:BoundField DataField="Construct" HeaderText="Construct" SortExpression="Construct" />
                                  <asp:BoundField DataField="Composition" HeaderText="Composition" SortExpression="Composition" />
                                  <asp:BoundField DataField="POQty" HeaderText="POQty" SortExpression="POQty" />
                                  <asp:BoundField DataField="UomCode" HeaderText="UomCode" SortExpression="UomCode" />
                                  <asp:BoundField DataField="IsApproved" HeaderText="IsApproved" SortExpression="IsApproved" />
                                  <asp:BoundField DataField="ApprovedBy" HeaderText="ApprovedBy" SortExpression="ApprovedBy" />
                                  <asp:BoundField DataField="CUrate" HeaderText="CUrate" SortExpression="CUrate" />
                                  <asp:BoundField DataField="TemplateSize" HeaderText="TemplateSize" SortExpression="TemplateSize" />
                                  <asp:BoundField DataField="TemplateWidth" HeaderText="TemplateWidth" SortExpression="TemplateWidth" />
                                  <asp:BoundField DataField="TemplateWeight" HeaderText="TemplateWeight" SortExpression="TemplateWeight" />
                                  <asp:BoundField DataField="PaymentCodeDescription" HeaderText="PaymentCodeDescription" SortExpression="PaymentCodeDescription" />
                                  <asp:BoundField DataField="DeliveryTerm" HeaderText="DeliveryTerm" SortExpression="DeliveryTerm" />
                              </Columns>
                              <HeaderStyle CssClass="header" />
                              <PagerStyle CssClass="pager" />
                              <RowStyle CssClass="rows" />
                                
                            </asp:GridView>
                          <asp:SqlDataSource ID="podetailsdata" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT        StockPOMaster.SPO_Pk, StockPOMaster.SPONum, StockPOMaster.AddedDate, StockPOMaster.ApprovedBy, StockPOMaster.DeliveryDate, StockPOMaster.IsApproved, UOMMaster.UomCode, 
                         StockPODetails.POQty, StockPODetails.CUrate, StockPODetails.TemplateColor, StockPODetails.TemplateSize, StockPODetails.TemplateWidth, StockPODetails.TemplateWeight, StockPODetails.Composition, 
                         StockPODetails.Construct, StockPODetails.SPODetails_PK, SupplierMaster.SupplierName, StockPODetails.Template_PK, PaymentTermMaster.PaymentCodeDescription, DeliveryTermMaster.DeliveryTerm, 
                         TemplateComposition.TemplateCom_Pk
FROM            StockPODetails INNER JOIN
                         StockPOMaster ON StockPODetails.SPO_PK = StockPOMaster.SPO_Pk INNER JOIN
                         UOMMaster ON StockPODetails.Uom_PK = UOMMaster.Uom_PK INNER JOIN
                         SupplierMaster ON StockPOMaster.Supplier_Pk = SupplierMaster.Supplier_PK INNER JOIN
                         PaymentTermMaster ON StockPOMaster.PaymentTermID = PaymentTermMaster.PaymentTermID INNER JOIN
                         DeliveryTermMaster ON StockPOMaster.DeliveryTerms_Pk = DeliveryTermMaster.DeliveryTerms_Pk INNER JOIN
                         TemplateComposition ON StockPODetails.Composition = TemplateComposition.Composition"></asp:SqlDataSource>
                    </div>
                                             </ContentTemplate>
                                    </asp:UpdatePanel>
                </asp:Panel>
            </td>
        </tr>
    </table>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="IPOMultiCreator.aspx.cs" Inherits="ArtWebApp.Merchandiser.PO.IPOMultiCreator" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        
      
    </style>
    <link href="../../css/style.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="FullTable">

        <div class="DataEntryTable">
                     <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
                                <ContentTemplate>
                                    <table>
                                        <tr>
                                            <td class="RedHeadding" colspan="7">general<strong> P.O. (iNTERNATIONAL&nbsp; Purchase)</strong></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label1" runat="server" Text="SPO"></asp:Label>
                                            </td>
                                            <td>
                                         
                                                <ucc:DropDownListChosen ID="drp_spo" runat="server" DataSourceID="spodatasource" DataTextField="SPONum" DataValueField="SPO_Pk" DisableSearchThreshold="10" Width="200px" >
                            </ucc:DropDownListChosen>
                                            </td>
                                            <td>
                                                <asp:Button ID="Button3" runat="server" Font-Size="Smaller" OnClick="Button3_Click" Text="S" Height="21px" Width="21px" />
                                            </td>
                                            <td aria-invalid="grammar">&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td rowspan="5">REMARK</td>
                                            <td rowspan="5">
                                                <textarea id="txt_remark"  runat="server" class="auto-style36" name="S1"></textarea></td>
                                        </tr>
                                        <tr>
                                            <td>Supplier</td>
                                            <td>
                                               
                                                  <ucc:DropDownListChosen ID="drp_supplier" runat="server" DataSourceID="supplierdata" DataTextField="SupplierName" DataValueField="Supplier_PK" DisableSearchThreshold="10" Width="200px" >
                            </ucc:DropDownListChosen>
                                            </td>
                                            <td></td>
                                            <td aria-invalid="grammar">Delivery Date :</td>
                                            <td>
                                                <ig:WebDatePicker ID="dtp_deliverydate" runat="server" Height="23px" Width="200px">
                                                </ig:WebDatePicker>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Delivery Terms :</td>
                                            <td>
                                           

                                                  <ucc:DropDownListChosen ID="drp_deliveryterm" runat="server" DataSourceID="deliveryterm" DataTextField="DeliveryTerm" DataValueField="Deliveryterms_pk" DisableSearchThreshold="10" Width="200px" >
                            </ucc:DropDownListChosen>
                                            </td>
                                            <td></td>
                                            <td>Delivery Destination :</td>
                                            <td>
                                                <ucc:DropDownListChosen ID="drp_deliverydestination" runat="server" DataSourceID="Wharehousedata" DataTextField="LocationName" DataValueField="Location_PK" Width="200px">
                                                </ucc:DropDownListChosen>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Delivery Method :</td>
                                            <td>
                                               

                                                  <ucc:DropDownListChosen ID="drp_deliverymethod" runat="server" DataSourceID="DeliveryMethodData" DataTextField="DeliveryMethod" DataValueField="DeliveryMethod_pk" DisableSearchThreshold="10" Width="200px" >
                            </ucc:DropDownListChosen>

                                            </td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;Currency :</td>
                                            <td>
                                               
                                                <ucc:DropDownListChosen ID="drp_currency" runat="server" DataSourceID="currencydata" DataTextField="CurrencyCode" DataValueField="CurrencyID" DisableSearchThreshold="10" Width="200px" >
                            </ucc:DropDownListChosen>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Payment Term :</td>
                                            <td>
                                               

                                                <ucc:DropDownListChosen ID="drp_paymentterm" runat="server" DataSourceID="Paymenttermdata" DataTextField="PaymentTermCode" DataValueField="PaymentTermID" DisableSearchThreshold="10" Width="200px" >
                            </ucc:DropDownListChosen>
                                            </td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>
                                                <asp:Button ID="btn_AddSpo" runat="server" OnClick="btn_AddSpo_Click" Text="Save" />
                                            </td>
                                            <td>
                                                &nbsp;</td>
                                        </tr>



                                        <tr>
                                            <td colspan="5"><div id="Messaediv" runat="server">
                 


                           <asp:Label ID="lbl_msg" runat="server" Text="*"></asp:Label>


                     
               </div></td>
                                        </tr>



                                    </table>
                                    </table>

                                </ContentTemplate>
                            </asp:UpdatePanel>

        </div>

        <div class="DataEntryTable">



             <asp:UpdatePanel ID="UpdatePanel4" UpdateMode="Conditional" runat="server">
                                <ContentTemplate>
                                     <asp:GridView ID="tbl_SpoEnterData" runat="server" AutoGenerateColumns="False" BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2">
            <Columns>
                <asp:TemplateField HeaderText="POID" >
                   
                    <ItemTemplate>
                        <asp:Label ID="lbl_poid" runat="server" Text='<%# Bind("POID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="POLineID">
                  
                    <ItemTemplate>
                        <asp:Label ID="lbl_polineid" runat="server" Text='<%# Bind("POLineID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="IPO" HeaderText="IPO" />
                <asp:BoundField DataField="Description" HeaderText="Description" />
                <asp:TemplateField HeaderText="BalanceQty">
                   
                    <ItemTemplate>
                        <asp:Label ID="lbl_balaqty" runat="server" Text='<%# Bind("BalanceQty") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="UOM" HeaderText="UOM" />
                  <asp:TemplateField HeaderText="Item Group">
                   
                    <ItemTemplate>
                        <ucc:DropDownListChosen ID="cmb_itemgroup" runat="server" DataSourceID="itemgroup" AutoPostBack="true" DataTextField="ItemGroupName" DataValueField="ItemGroupID" Width="200px" OnSelectedIndexChanged="cmb_itemgroup_SelectedIndexChanged">
                                                </ucc:DropDownListChosen>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Template">
                   
                    <ItemTemplate>
                        <asp:UpdatePanel ID="Upd_templateforComp"  runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <ucc:DropDownListChosen ID="drp_templateforComp" AutoPostBack="true" runat="server" OnSelectedIndexChanged="drp_templateforComp_SelectedIndexChanged">
                                                        </ucc:DropDownListChosen>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                    </ItemTemplate>
                </asp:TemplateField>
              
              
                <asp:TemplateField HeaderText="Composition">
                 
                    <ItemTemplate>
                         <asp:UpdatePanel ID="upd_composition" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <ucc:DropDownListChosen ID="drp_composition" runat="server" >
                                                        </ucc:DropDownListChosen>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                    </ItemTemplate>
                </asp:TemplateField>


                  <asp:TemplateField HeaderText="construction">
                   
                    <ItemTemplate>
                     <asp:UpdatePanel ID="upd_construction" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                       

                                                          <ucc:DropDownListChosen ID="drp_construction" runat="server">
                                             </ucc:DropDownListChosen>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                    </ItemTemplate>
                </asp:TemplateField>





                <asp:TemplateField HeaderText="ItemColor" >
                  
                    <ItemTemplate>
                       <asp:UpdatePanel ID="upd_itemcolor" UpdateMode="Conditional" runat="server">
                                                    <ContentTemplate>
                                                       
                                                     <ucc:DropDownListChosen ID="drp_itemcolor"  runat="server">       <asp:ListItem Selected="True"></asp:ListItem>
                                             </ucc:DropDownListChosen>
                                                        
                                                          </ContentTemplate>
                                                </asp:UpdatePanel>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Item Size">
                 
                    <ItemTemplate>
                          <asp:UpdatePanel ID="upd_itemsize" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        
                                                          <ucc:DropDownListChosen ID="drp_itemsize"  runat="server" >
                                                                     <asp:ListItem Selected="True"></asp:ListItem>
                                             </ucc:DropDownListChosen>

                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Weight">
                   
                    <ItemTemplate>
                        <asp:UpdatePanel ID="upd_weight" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <ucc:DropDownListChosen ID="drp_weight" runat="server" >
                                                            <asp:ListItem Selected="True"></asp:ListItem>
                                                        </ucc:DropDownListChosen>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                    </ItemTemplate>
                </asp:TemplateField>

                
                <asp:TemplateField HeaderText="Width">
                   
                    <ItemTemplate>
                        <asp:UpdatePanel ID="upd_width" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                         <ucc:DropDownListChosen ID="drp_width" runat="server" >
                                                           <asp:ListItem Selected="True"></asp:ListItem>
                                                </ucc:DropDownListChosen>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Unit Price">
                   
                    <ItemTemplate>
                        <asp:TextBox ID="txt_unitPrice"  Width="50px" runat="server"  ></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Qty">
                  
                    <ItemTemplate  >
                        <asp:TextBox ID="txt_qty" Width="50px" runat="server"   ></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="UOM">
                 
                    <ItemTemplate>
                          <ucc:DropDownListChosen ID="drp_UOM" runat="server" DataSourceID="UOMdata" DataTextField="UomName" DataValueField="Uom_PK">
                                                </ucc:DropDownListChosen>
                    </ItemTemplate>
                </asp:TemplateField>
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



       






    </div>


    </div>


    <table class="FullTable">
        <tr>

            <td>


                <table class="DataEntryTable">
                    <tr>
                        <td>


                   
                        </td>
                    </tr>
                    <tr class="DataEntryTable">
                        <td>
                            <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Conditional" runat="server" ChildrenAsTriggers="False">
                                <ContentTemplate>
                                    <table>
                                        <tr>
                                            <td class="RedHeadding" colspan="8">Add Details</td>
                                        </tr>
                                        
                                        
                                      
                                       
                                     
                                       
                                        
                                        <tr>
                                            <td class="auto-style9">&nbsp;</td>
                                            <td class="auto-style42">&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td class="auto-style28">
                                                <asp:Button ID="btn_addItems" runat="server" Text="Add Item" OnClick="btn_addItems_Click" ValidationGroup="detail" />
                                            </td>
                                            <td class="auto-style40">&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td class="auto-style9">
                                                &nbsp;</td>
                                            <td class="auto-style42">
                                                &nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td class="auto-style28">&nbsp;</td>
                                            <td class="auto-style40">&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td class="auto-style9">&nbsp;</td>
                                            <td class="auto-style42">&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td class="auto-style28" colspan="4">
                                                &nbsp;</td>
                                        </tr>
                                    </table>

                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        </tr>

                    <tr class="DataEntryTable">
                        <td>
                            <table class="gridtable">
                                <tr>
                                    <td>
                                        <asp:UpdatePanel ID="UpdatePanel3" UpdateMode="Conditional" runat="server">
                                            <ContentTemplate>
                                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" style="font-size: small; font-family: Calibri; font-weight: 400;" Width="100%" BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4" DataKeyNames="SPODetails_PK" DataSourceID="Spodata">
                                    <Columns>
                                        <asp:TemplateField HeaderText="SPOD_PK" InsertVisible="False" SortExpression="SPODetails_PK">
                                            <EditItemTemplate>
                                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("SPODetails_PK") %>'></asp:Label>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("SPODetails_PK") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="SPO_PK" HeaderText="SPO_PK" SortExpression="SPO_PK" />
                                        <asp:BoundField DataField="Template_PK" HeaderText="Template_PK" SortExpression="Template_PK" />
                                        <asp:BoundField DataField="Composition" HeaderText="Composition" SortExpression="Composition" />
                                        <asp:BoundField DataField="Construct" HeaderText="Construct" SortExpression="Construct" />
                                        <asp:BoundField DataField="TemplateColor" HeaderText="TemplateColor" SortExpression="TemplateColor" />
                                        <asp:BoundField DataField="TemplateSize" HeaderText="TemplateSize" SortExpression="TemplateSize" />
                                        <asp:BoundField DataField="TemplateWidth" HeaderText="TemplateWidth" SortExpression="TemplateWidth" />
                                        <asp:BoundField DataField="TemplateWeight" HeaderText="TemplateWeight" SortExpression="TemplateWeight" />
                                        <asp:BoundField DataField="Unitprice" HeaderText="Unitprice" SortExpression="Unitprice" />
                                        <asp:BoundField DataField="POQty" HeaderText="POQty" SortExpression="POQty" />
                                        <asp:BoundField DataField="Uom_PK" HeaderText="Uom_PK" SortExpression="Uom_PK" />
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
                            </table>
                        </td>
                    </tr>
                </table>




            </td>
        </tr>

    </table>




    





    <asp:SqlDataSource ID="supplierdata" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT [SupplierName], [Supplier_PK] FROM [SupplierMaster] ORDER BY [SupplierName]"></asp:SqlDataSource>
    <asp:HiddenField ID="convfact" runat="server" Value="1" />
    <asp:SqlDataSource ID="currencydata" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" ProviderName="<%$ ConnectionStrings:ArtConnectionString.ProviderName %>" SelectCommand="SELECT * FROM [CurrencyMaster] ORDER BY [CurrencyCode], [CurrencyID]"></asp:SqlDataSource>
    <asp:SqlDataSource ID="PaymentMode" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT [PaymentModeID], [PaymentModeCode] FROM [PaymentModeMaster] ORDER BY [PaymentModeCode]"></asp:SqlDataSource>
    <asp:SqlDataSource ID="Paymenttermdata" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" OldValuesParameterFormatString="original_{0}" SelectCommand="SELECT [PaymentTermCode], [PaymentTermID] FROM [PaymentTermMaster] ORDER BY [PaymentTermCode]"></asp:SqlDataSource>
    <asp:SqlDataSource ID="Wharehousedata" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT LocationName, Location_PK FROM LocationMaster WHERE (LocType = N'W')"></asp:SqlDataSource>
    <asp:SqlDataSource ID="deliveryterm" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT [DeliveryTerm], [DeliveryTerms_Pk] FROM [DeliveryTermMaster]"></asp:SqlDataSource>
    <asp:SqlDataSource ID="Spodata" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT * FROM [StockPODetails] WHERE ([SPO_PK] = @SPO_PK) ORDER BY [Composition]" ConflictDetection="CompareAllValues" DeleteCommand="DELETE FROM [StockPODetails] WHERE [SPODetails_PK] = @original_SPODetails_PK AND (([SPO_PK] = @original_SPO_PK) OR ([SPO_PK] IS NULL AND @original_SPO_PK IS NULL)) AND (([Template_PK] = @original_Template_PK) OR ([Template_PK] IS NULL AND @original_Template_PK IS NULL)) AND (([Composition] = @original_Composition) OR ([Composition] IS NULL AND @original_Composition IS NULL)) AND (([Construct] = @original_Construct) OR ([Construct] IS NULL AND @original_Construct IS NULL)) AND (([TemplateColor] = @original_TemplateColor) OR ([TemplateColor] IS NULL AND @original_TemplateColor IS NULL)) AND (([TemplateSize] = @original_TemplateSize) OR ([TemplateSize] IS NULL AND @original_TemplateSize IS NULL)) AND (([TemplateWidth] = @original_TemplateWidth) OR ([TemplateWidth] IS NULL AND @original_TemplateWidth IS NULL)) AND (([TemplateWeight] = @original_TemplateWeight) OR ([TemplateWeight] IS NULL AND @original_TemplateWeight IS NULL)) AND (([Unitprice] = @original_Unitprice) OR ([Unitprice] IS NULL AND @original_Unitprice IS NULL)) AND (([POQty] = @original_POQty) OR ([POQty] IS NULL AND @original_POQty IS NULL)) AND (([Uom_PK] = @original_Uom_PK) OR ([Uom_PK] IS NULL AND @original_Uom_PK IS NULL))" InsertCommand="INSERT INTO [StockPODetails] ([SPODetails_PK], [SPO_PK], [Template_PK], [Composition], [Construct], [TemplateColor], [TemplateSize], [TemplateWidth], [TemplateWeight], [Unitprice], [POQty], [Uom_PK]) VALUES (@SPODetails_PK, @SPO_PK, @Template_PK, @Composition, @Construct, @TemplateColor, @TemplateSize, @TemplateWidth, @TemplateWeight, @Unitprice, @POQty, @Uom_PK)" OldValuesParameterFormatString="original_{0}" UpdateCommand="UPDATE [StockPODetails] SET [SPO_PK] = @SPO_PK, [Template_PK] = @Template_PK, [Composition] = @Composition, [Construct] = @Construct, [TemplateColor] = @TemplateColor, [TemplateSize] = @TemplateSize, [TemplateWidth] = @TemplateWidth, [TemplateWeight] = @TemplateWeight, [Unitprice] = @Unitprice, [POQty] = @POQty, [Uom_PK] = @Uom_PK WHERE [SPODetails_PK] = @original_SPODetails_PK AND (([SPO_PK] = @original_SPO_PK) OR ([SPO_PK] IS NULL AND @original_SPO_PK IS NULL)) AND (([Template_PK] = @original_Template_PK) OR ([Template_PK] IS NULL AND @original_Template_PK IS NULL)) AND (([Composition] = @original_Composition) OR ([Composition] IS NULL AND @original_Composition IS NULL)) AND (([Construct] = @original_Construct) OR ([Construct] IS NULL AND @original_Construct IS NULL)) AND (([TemplateColor] = @original_TemplateColor) OR ([TemplateColor] IS NULL AND @original_TemplateColor IS NULL)) AND (([TemplateSize] = @original_TemplateSize) OR ([TemplateSize] IS NULL AND @original_TemplateSize IS NULL)) AND (([TemplateWidth] = @original_TemplateWidth) OR ([TemplateWidth] IS NULL AND @original_TemplateWidth IS NULL)) AND (([TemplateWeight] = @original_TemplateWeight) OR ([TemplateWeight] IS NULL AND @original_TemplateWeight IS NULL)) AND (([Unitprice] = @original_Unitprice) OR ([Unitprice] IS NULL AND @original_Unitprice IS NULL)) AND (([POQty] = @original_POQty) OR ([POQty] IS NULL AND @original_POQty IS NULL)) AND (([Uom_PK] = @original_Uom_PK) OR ([Uom_PK] IS NULL AND @original_Uom_PK IS NULL))">
        <DeleteParameters>
            <asp:Parameter Name="original_SPODetails_PK" Type="Decimal" />
            <asp:Parameter Name="original_SPO_PK" Type="Decimal" />
            <asp:Parameter Name="original_Template_PK" Type="Decimal" />
            <asp:Parameter Name="original_Composition" Type="String" />
            <asp:Parameter Name="original_Construct" Type="String" />
            <asp:Parameter Name="original_TemplateColor" Type="String" />
            <asp:Parameter Name="original_TemplateSize" Type="String" />
            <asp:Parameter Name="original_TemplateWidth" Type="String" />
            <asp:Parameter Name="original_TemplateWeight" Type="String" />
            <asp:Parameter Name="original_Unitprice" Type="Decimal" />
            <asp:Parameter Name="original_POQty" Type="Decimal" />
            <asp:Parameter Name="original_Uom_PK" Type="Decimal" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="SPODetails_PK" Type="Decimal" />
            <asp:Parameter Name="SPO_PK" Type="Decimal" />
            <asp:Parameter Name="Template_PK" Type="Decimal" />
            <asp:Parameter Name="Composition" Type="String" />
            <asp:Parameter Name="Construct" Type="String" />
            <asp:Parameter Name="TemplateColor" Type="String" />
            <asp:Parameter Name="TemplateSize" Type="String" />
            <asp:Parameter Name="TemplateWidth" Type="String" />
            <asp:Parameter Name="TemplateWeight" Type="String" />
            <asp:Parameter Name="Unitprice" Type="Decimal" />
            <asp:Parameter Name="POQty" Type="Decimal" />
            <asp:Parameter Name="Uom_PK" Type="Decimal" />
        </InsertParameters>
        <SelectParameters>
            <asp:SessionParameter DefaultValue="0" Name="SPO_PK" SessionField="spo_pk" Type="Decimal" />
        </SelectParameters>
        <UpdateParameters>
            <asp:Parameter Name="SPO_PK" Type="Decimal" />
            <asp:Parameter Name="Template_PK" Type="Decimal" />
            <asp:Parameter Name="Composition" Type="String" />
            <asp:Parameter Name="Construct" Type="String" />
            <asp:Parameter Name="TemplateColor" Type="String" />
            <asp:Parameter Name="TemplateSize" Type="String" />
            <asp:Parameter Name="TemplateWidth" Type="String" />
            <asp:Parameter Name="TemplateWeight" Type="String" />
            <asp:Parameter Name="Unitprice" Type="Decimal" />
            <asp:Parameter Name="POQty" Type="Decimal" />
            <asp:Parameter Name="Uom_PK" Type="Decimal" />
            <asp:Parameter Name="original_SPODetails_PK" Type="Decimal" />
            <asp:Parameter Name="original_SPO_PK" Type="Decimal" />
            <asp:Parameter Name="original_Template_PK" Type="Decimal" />
            <asp:Parameter Name="original_Composition" Type="String" />
            <asp:Parameter Name="original_Construct" Type="String" />
            <asp:Parameter Name="original_TemplateColor" Type="String" />
            <asp:Parameter Name="original_TemplateSize" Type="String" />
            <asp:Parameter Name="original_TemplateWidth" Type="String" />
            <asp:Parameter Name="original_TemplateWeight" Type="String" />
            <asp:Parameter Name="original_Unitprice" Type="Decimal" />
            <asp:Parameter Name="original_POQty" Type="Decimal" />
            <asp:Parameter Name="original_Uom_PK" Type="Decimal" />
        </UpdateParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="DeliveryMethodData" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT [Deliverymethod_Pk], [DeliveryMethod] FROM [DeliveryMethodMaster]"></asp:SqlDataSource>
    <asp:SqlDataSource ID="itemgroup" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT [ItemGroupName], [ItemGroupID] FROM [ItemGroupMaster] ORDER BY [ItemGroupName], [ItemGroupID]"></asp:SqlDataSource>
    <asp:SqlDataSource ID="spodatasource" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT SPO_Pk, SPONum, IsApproved FROM StockPOMaster WHERE (IsApproved = N'N')"></asp:SqlDataSource>
    <br />
    <asp:SqlDataSource ID="UOMdata" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT Uom_PK, UomName FROM UOMMaster"></asp:SqlDataSource>
    <br />


</asp:Content>



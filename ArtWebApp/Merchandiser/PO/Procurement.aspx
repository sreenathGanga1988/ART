<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="Procurement.aspx.cs" Inherits="ArtWebApp.Merchandiser.Procurement" %>
<%@ Register assembly="Infragistics35.Web.v12.1, Version=12.1.20121.2236, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.ListControls" tagprefix="ig" %>
<%@ Register assembly="Infragistics35.Web.v12.1, Version=12.1.20121.2236, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.GridControls" tagprefix="ig1" %>
<%@ Register assembly="Infragistics35.Web.v12.1, Version=12.1.20121.2236, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.EditorControls" tagprefix="ig" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">

       
       

       
        .hidden {
            display: none;
        }

     


      

        </style>
    <link href="../../css/style.css" rel="stylesheet" />
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1"  runat="server">
    <table class="FullTable">
        <tr>
           <td class="NormalTD">
                <asp:UpdatePanel ID="Upd_Main" UpdateMode="Conditional"  ChildrenAsTriggers="false" runat="server">
                    <ContentTemplate>
                        <table class="DataEntryTable">
                            <tr>
                                <td class="RedHeadding" colspan="7"><strong>SUPPLIER P.O.</strong></td>
                            </tr>
                            <tr>
                                <td >Supplier</td>
                                <td >
                                    <ucc:DropDownListChosen ID="drp_supplier" runat="server" DataSourceID="supplierdata" DataTextField="SupplierName" DataValueField="Supplier_PK" DisableSearchThreshold="10">
                                    </ucc:DropDownListChosen>
                                </td>
                                <td ></td>
                                <td >Delivery Date :</td>
                                <td >
                                     

<asp:TextBox ID="dtp_deliverydate" runat="server" Width="180px"></asp:TextBox>


                                    <asp:CalendarExtender ID="dtp_deliverydate_CalendarExtender" runat="server" Enabled="True" Format="dd/MMM/yyyy" TargetControlID="dtp_deliverydate" >
                                    </asp:CalendarExtender>


                                </td>
                                <td  rowspan="5">REMARK</td>
                                <td  rowspan="5">
                                    <asp:TextBox ID="txtarea" runat="server" Height="47px" Width="171px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td >Delivery Terms :</td>
                                <td >
                                    <ucc:DropDownListChosen ID="drp_deliveryterm" runat="server" DataSourceID="deliveryterm" DataTextField="DeliveryTerm" DataValueField="DeliveryTerms_Pk" DisableSearchThreshold="10" Width="200px">
                                    </ucc:DropDownListChosen>
                                </td>
                                <td ></td>
                                <td >Delivery Destination :</td>
                                <td >
                                    <ucc:DropDownListChosen ID="drp_deliverydestination" runat="server" DataSourceID="Wharehousedata" DataTextField="LocationName" DataValueField="Location_PK" DisableSearchThreshold="10" Width="200px">
                                    </ucc:DropDownListChosen>
                                </td>
                            </tr>
                            <tr>
                                <td >Delivery Method :</td>
                                <td >
                                    <ucc:DropDownListChosen ID="drp_deliverymethod" runat="server" DataSourceID="DeliveryMethodData" DataTextField="DeliveryMethod" DataValueField="Deliverymethod_Pk" DisableSearchThreshold="10" Width="200px">
                                    </ucc:DropDownListChosen>
                                </td>
                                <td >&nbsp;</td>
                                <td >&nbsp;Currency :</td>
                                <td >
                                    <asp:UpdatePanel ID="upd_currency" UpdateMode="Conditional"  runat="server">
                    <ContentTemplate>
                                 
                         <ucc:DropDownListChosen ID="drp_currency" runat="server" DataSourceID="currencydata" DataTextField="CurrencyCode" DataValueField="CurrencyID"   DisableSearchThreshold="10" Width="200px" AutoPostBack="True" OnSelectedIndexChanged="drp_currency_SelectedIndexChanged">
                                    </ucc:DropDownListChosen>
                         </ContentTemplate>
                </asp:UpdatePanel>
                                   
                                </td>
                            </tr>
                            <tr>
                                <td >
                                    Payment Term</td>
                                <td ><ucc:DropDownListChosen ID="drp_paymentterm" runat="server" DataSourceID="Paymenttermdata" DataTextField="PaymentCodeDescription" DataValueField="PaymentTermID" DisableSearchThreshold="10" Width="200px">
                                    </ucc:DropDownListChosen>
                                </td>
                                <td >&nbsp;</td>
                                <td >Po Type :</td>
                                <td >
                                    
                                    <ucc:DropDownListChosen ID="cmb_suppliertype" runat="server" Width="200px">
                                        <asp:ListItem Value="F">Fabric</asp:ListItem>
                                        <asp:ListItem Value="T">Trims</asp:ListItem>
                                    </ucc:DropDownListChosen>
                                </td>
                            </tr>
                            <tr>
                                <td>Freight type</td>
                                <td>
                                    <ucc:DropDownListChosen ID="drp_freightChargetype" runat="server" Width="200px">
                                        <asp:ListItem>No Charges</asp:ListItem>
                                        <asp:ListItem>Sea</asp:ListItem>
                                        <asp:ListItem>Air</asp:ListItem>
                                        <asp:ListItem>Courier</asp:ListItem>
                                    </ucc:DropDownListChosen>
                                </td>
                                <td>&nbsp;&nbsp;</td>
                                <td>freight charge</td>
                                <td>
                                    <asp:TextBox ID="txt_freightcharges" runat="server">0</asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td >&nbsp;</td>
                                <td ></td>
                                <td ></td>
                                <td c>
                                    
                                </td>
                                <td ></td>
                            </tr>
                            <caption>
                                
                            </caption>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr class="error-message">
           <td class="NormalTD">
                  <asp:UpdatePanel ID="upd_label" UpdateMode="Conditional"  runat="server">
                    <ContentTemplate>
                <asp:Label ID="lbl_mssg" runat="server" Text="*"></asp:Label>

                          </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>

        <tr>
           <td class="NormalTD">
                <asp:UpdatePanel ID="upd_detail" UpdateMode="Conditional" ChildrenAsTriggers="false"   runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="tbl_Podetails" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4" OnRowDataBound="tbl_Podetails_RowDataBound" style="font-size: small; font-family: Calibri; margin-bottom: 0px;" Width="100%">
                            <Columns>
                                <asp:TemplateField HeaderStyle-CssClass="hidden" HeaderText="SkuDet_pk" ItemStyle-CssClass="hidden">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_skudet_pk" runat="server" Text='<%# Bind("SkuDet_pk") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="hidden" />
                                    <ItemStyle CssClass="hidden" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="RMNum" HeaderText="RMNum" />
                                <asp:BoundField DataField="Description" HeaderText="Description" />
                                 <asp:BoundField DataField="SizeName" HeaderText="Size" />
                                <asp:TemplateField HeaderText="OurColor">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_itemcolor" runat="server" Text='<%# Bind("ItemColor") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="OurSize">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_itemsize" runat="server" Text='<%# Bind("ItemSize") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Balance Qty ">
                                   
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_balqty" runat="server" Text='<%# Bind("BalanceQty") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="UOM">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_UOMCode" runat="server" Text='<%# Bind("UOMCode") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-CssClass="hidden" HeaderText="UOM_pk" ItemStyle-CssClass="hidden">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_uomPK" runat="server" Text='<%# Bind("UOM_pk") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="hidden" />
                                    <ItemStyle CssClass="hidden" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Supplier Color">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddl_Supcolor" runat="server" DataSourceID="colordata" DataTextField="TemplateColor" DataValueField="TemplateColor" Width="100%">
                                        </asp:DropDownList>

                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Supplier Size">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddl_SupSize" runat="server" DataSourceID="Sizedata" DataTextField="TemplateSize" DataValueField="TemplateSize" Width="100%">
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                   <asp:TemplateField HeaderText="AltUOM" SortExpression="AltUOM_pk">

                                        <ItemTemplate>
                                            <asp:Label ID="lbl_altuompk" runat="server" Text='<%# Bind("AltUom_pk") %>' Visible="False"></asp:Label>
                                            <asp:UpdatePanel ID="Upd_ddl_AltUOM" UpdateMode="Conditional"  runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="ddl_AltUOM" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddl_AltUOM_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                <asp:TemplateField HeaderText="PoQty">
                                    <ItemTemplate>
                                        <asp:UpdatePanel ID="Upd_txt_poQty" UpdateMode="Conditional"  runat="server">
                                            <ContentTemplate>
                                        <asp:TextBox ID="txt_poQty" runat="server" Height="16px" Text='<%# Bind("BalanceQty") %>' Width="63px" AutoPostBack="True" OnTextChanged="txt_poQty_TextChanged"></asp:TextBox>
                                  </ContentTemplate>
                                        </asp:UpdatePanel>  </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="BalQty ALTUOM">
                                   
                                    <ItemTemplate>
                                        <asp:UpdatePanel ID="Upd_lbl_BalQtyinALTUOM" UpdateMode="Conditional"  runat="server">
                                            <ContentTemplate>
                                                <asp:Label ID="lbl_BalQtyinALTUOM" runat="server" Text='<%# Bind("BalanceQty") %>'></asp:Label>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="UnitRate">
                                    <ItemTemplate>
                                        <asp:UpdatePanel ID="Upd_txt_unitrate" UpdateMode="Conditional"  runat="server">
                                            <ContentTemplate>
                                                <asp:TextBox ID="txt_unitrate" runat="server" Height="16px" OnTextChanged="txt_unitrate_TextChanged" Text='<%# Bind("Unitrate") %>' Width="63px"></asp:TextBox>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="CURate" SortExpression="Unitrate">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_costunitrate" runat="server" Text='<%# Bind("Unitrate") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="OUnitrate">
                                    
                                    <ItemTemplate>
                                        <asp:UpdatePanel ID="upd_lbl_supunitrate" UpdateMode="Conditional" runat="server">
                                            <ContentTemplate>
                                                <asp:Label ID="lbl_supunitrate" runat="server" Text='<%# Bind("Unitrate") %>'></asp:Label>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
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
            </td>
        </tr>

        <tr class="SmallSearchButton">
           <td class="NormalTD">
                
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        <asp:Button ID="Btn_submit" runat="server" Text="Submit" Height="25px" OnClick="Btn_submit_Click" style="font-size: small; font-family: Calibri; text-align: center" />
                    </ContentTemplate>
                </asp:UpdatePanel>

            </td>
        </tr>

        
    </table>
   

               
                <asp:SqlDataSource ID="supplierdata" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT [SupplierName], [Supplier_PK] FROM [SupplierMaster] ORDER BY [SupplierName]"></asp:SqlDataSource>
                                <asp:HiddenField ID="convfact" runat="server" Value="1" />
                                <asp:SqlDataSource ID="currencydata" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT * FROM [CurrencyMaster] ORDER BY [CurrencyCode], [CurrencyID]" ProviderName="<%$ ConnectionStrings:ArtConnectionString.ProviderName %>"></asp:SqlDataSource>
                                <asp:SqlDataSource ID="PaymentMode" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT [PaymentModeID], [PaymentModeCode] FROM [PaymentModeMaster] ORDER BY [PaymentModeCode]"></asp:SqlDataSource>
                    <asp:SqlDataSource ID="Paymenttermdata" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" OldValuesParameterFormatString="original_{0}" SelectCommand="SELECT PaymentTermID, PaymentCodeDescription FROM PaymentTermMaster ORDER BY PaymentTermCode">
                    </asp:SqlDataSource>
                    <asp:SqlDataSource ID="Wharehousedata" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT LocationName, Location_PK FROM LocationMaster WHERE (LocType = N'W')"></asp:SqlDataSource>
                    <asp:SqlDataSource ID="colordata" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT DISTINCT TemplateColor FROM TemplateColor GROUP BY TemplateColor"></asp:SqlDataSource>
                    <asp:SqlDataSource ID="Sizedata" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT DISTINCT TemplateSize FROM TemplateSize GROUP BY TemplateSize"></asp:SqlDataSource>
                <asp:SqlDataSource ID="deliveryterm" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT [DeliveryTerm], [DeliveryTerms_Pk] FROM [DeliveryTermMaster]"></asp:SqlDataSource>
                <asp:SqlDataSource ID="DeliveryMethodData" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT [Deliverymethod_Pk], [DeliveryMethod] FROM [DeliveryMethodMaster]"></asp:SqlDataSource>
    <br />

             
</asp:Content>

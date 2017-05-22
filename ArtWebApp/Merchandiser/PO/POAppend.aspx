<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="POAppend.aspx.cs" Inherits="ArtWebApp.Merchandiser.PO.POAppend" %>
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
            <td>
                <asp:UpdatePanel ID="Upd_Main" UpdateMode="Conditional"  ChildrenAsTriggers="false" runat="server">
                    <ContentTemplate>
                        <table class="DataEntryTable">
                            <tr>
                                <td class="RedHeadding" colspan="7"><strong>SUPPLIER P.O.</strong></td>
                            </tr>
                            <tr>
                                <td >PO</td>
                                <td >
                                    <ucc:DropDownListChosen ID="drp_po" runat="server" DataSourceID="PoSource" DataTextField="PONum" DataValueField="PO_Pk" DisableSearchThreshold="10">
                                    </ucc:DropDownListChosen>
                                    
                                </td>
                                <td >
                                    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Button" />
                                </td>
                                <td >&nbsp;</td>
                                <td >
                                     

                                    &nbsp;</td>
                                <td  rowspan="5">&nbsp;</td>
                                <td  rowspan="5">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td >Currency</td>
                                <td >
                                    <asp:UpdatePanel ID="upd_currency" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <ucc:DropDownListChosen ID="drp_currency" runat="server" AutoPostBack="True" DataSourceID="currencydata" DataTextField="CurrencyCode" DataValueField="CurrencyID" DisableSearchThreshold="10" OnSelectedIndexChanged="drp_currency_SelectedIndexChanged" Width="200px">
                                            </ucc:DropDownListChosen>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td ></td>
                                <td >&nbsp;</td>
                                <td >
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td >&nbsp;</td>
                                <td >
                                    &nbsp;</td>
                                <td >&nbsp;</td>
                                <td >&nbsp;</td>
                                <td >
                                    
                                   
                                </td>
                            </tr>
                            <tr>
                                <td >&nbsp;</td>
                                <td >
                                    &nbsp;</td>
                                <td >&nbsp;</td>
                                <td >&nbsp;</td>
                                <td >
                                    
                                    &nbsp;</td>
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
            <td>
                  <asp:UpdatePanel ID="upd_label" UpdateMode="Conditional"  runat="server">
                    <ContentTemplate>
                <asp:Label ID="lbl_mssg" runat="server" Text="*"></asp:Label>

                          </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>

        <tr>
            <td>
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
            <td>
                <asp:Button ID="Btn_submit" runat="server" Text="Submit" Height="25px" OnClick="Btn_submit_Click" style="font-size: small; font-family: Calibri; text-align: center" />
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        
                    </ContentTemplate>
                </asp:UpdatePanel>

            </td>
        </tr>

        
    </table>
   

               
                                <asp:HiddenField ID="convfact" runat="server" Value="1" />
                                <asp:SqlDataSource ID="currencydata" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT * FROM [CurrencyMaster] ORDER BY [CurrencyCode], [CurrencyID]" ProviderName="<%$ ConnectionStrings:ArtConnectionString.ProviderName %>"></asp:SqlDataSource>
                    <asp:SqlDataSource ID="colordata" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT DISTINCT TemplateColor FROM TemplateColor GROUP BY TemplateColor"></asp:SqlDataSource>
                    <asp:SqlDataSource ID="Sizedata" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT DISTINCT TemplateSize FROM TemplateSize GROUP BY TemplateSize"></asp:SqlDataSource>
                <asp:SqlDataSource ID="PoSource" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT PO_Pk, PONum, IsApproved, AtcId FROM ProcurementMaster WHERE (IsApproved = N'N') AND (AtcId = @Param1)">
                    <SelectParameters>
                        <asp:SessionParameter Name="Param1" SessionField="atcid" />
                    </SelectParameters>
    </asp:SqlDataSource>
    <br />

             
</asp:Content>


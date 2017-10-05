<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="ProcuremntEdit.aspx.cs" Inherits="ArtWebApp.Merchandiser.ProcuremntEdit" %>
<%@ Register assembly="Infragistics35.Web.v12.1, Version=12.1.20121.2236, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.ListControls" tagprefix="ig" %>
<%@ Register assembly="Infragistics35.Web.v12.1, Version=12.1.20121.2236, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.GridControls" tagprefix="ig1" %>
<%@ Register assembly="Infragistics35.Web.v12.1, Version=12.1.20121.2236, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.EditorControls" tagprefix="ig" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">    
       

       
        .hidden {
            display: none;
        }


        .auto-style1 {
            width: 30px;
        }


        </style>
    <link href="../../css/style.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="FullTable">
       
        <tr>
            <td> <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <table class="DataEntryTable">
                            <tr>
                                <td class="RedHeadding" colspan="6">SUPPLIER P.O&nbsp; EDITING</td>
                            </tr>
                            <tr>
                                <td >aTC#</td>
                                <td >
                                   <%-- <ig:WebDropDown ID="drp_Atc" runat="server" DropDownAnimationType="EaseOut" DropDownContainerHeight="300px" DropDownContainerWidth="200px" EnableDropDownAsChild="false" Height="21px" PageSize="12" TextField="name" ValueField="pk" Width="156px">
                                        <DropDownItemBinding TextField="name" ValueField="pk" />
                                    </ig:WebDropDown>--%>


                                    <ucc:DropDownListChosen ID="drp_Atc" runat="server"  DataTextField="name" DataValueField="pk" Height="17px" Width="200px">
                                    </ucc:DropDownListChosen>
                                </td>
                                <td class="auto-style1" >
                                    <asp:Button ID="Button1" runat="server" Text="S" OnClick="Button1_Click" />
                                </td>
                                <td >po#</td>
                                <td >
                                   


                                    <ucc:DropDownListChosen ID="drp_PO" runat="server"  DataTextField="name" DataValueField="pk" DisableSearchThreshold="10" Width="200px">
                                    </ucc:DropDownListChosen>







                                </td>
                                <td >
                                    <asp:Button ID="Button2" runat="server" Text="S" OnClick="Button2_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td >Supplier</td>
                                <td >
                                     <ucc:DropDownListChosen ID="drp_supplier" runat="server" DataSourceID="supplierdata" DataTextField="SupplierName" DataValueField="Supplier_PK" DisableSearchThreshold="10" Width="200px">
                                    </ucc:DropDownListChosen>
                                </td>
                                <td class="auto-style1" ></td>
                                <td aria-invalid="grammar" class="auto-style20">Delivery Date :</td>
                                <td >
                                    <ig:WebDatePicker ID="dtp_deliverydate" runat="server" Height="23px" Width="200px">
                                    </ig:WebDatePicker>
                                </td>
                                <td >&nbsp;</td>
                            </tr>
                            <tr>
                                <td >Delivery Terms :</td>
                                <td >
                                       <ucc:DropDownListChosen ID="drp_deliveryterm" runat="server" DataSourceID="deliveryterm" DataTextField="DeliveryTerm" DataValueField="DeliveryTerms_Pk" DisableSearchThreshold="10" Width="200px">
                                    </ucc:DropDownListChosen>
                                </td>
                                <td class="auto-style1" ></td>
                                <td >Delivery Destination :</td>
                                <td >
                                      <ucc:DropDownListChosen ID="drp_deliverydestination" runat="server" DataSourceID="Wharehousedata" DataTextField="LocationName" DataValueField="Location_PK" DisableSearchThreshold="10" Width="200px">
                                    </ucc:DropDownListChosen>
                                </td>
                                <td >&nbsp;</td>
                            </tr>
                            <tr>
                                <td >Delivery Method :</td>
                                <td >
                                     <ucc:DropDownListChosen ID="drp_deliverymethod" runat="server" DataSourceID="DeliveryMethodData" DataTextField="DeliveryMethod" DataValueField="Deliverymethod_Pk" DisableSearchThreshold="10" Width="200px">
                                    </ucc:DropDownListChosen>
                                </td>
                                <td class="auto-style1" >&nbsp;</td>
                                <td >&nbsp;Currency :</td>
                                <td >
                                    <ucc:DropDownListChosen ID="drp_currency" runat="server" DataSourceID="currencydata" DataTextField="CurrencyCode" DataValueField="CurrencyID"  DisableSearchThreshold="10" Width="200px" AutoPostBack="True" OnSelectedIndexChanged="drp_currency_SelectedIndexChanged">
                                    </ucc:DropDownListChosen>
                                </td>
                                <td >&nbsp;</td>
                            </tr>
                            <tr>
                                <td >Payment Term :</td>
                                <td >
                                     <ucc:DropDownListChosen ID="drp_paymentterm" runat="server" DataSourceID="Paymenttermdata" DataTextField="PaymentTermCode" DataValueField="PaymentTermID" DisableSearchThreshold="10" Width="200px">
                                    </ucc:DropDownListChosen>
                                </td>
                                <td class="auto-style1" >&nbsp;</td>
                                <td >po tYPE :</td>
                                <td >
                                    <ucc:DropDownListChosen ID="drp_potype" runat="server" Width="200px">
                                        <asp:ListItem Value="F">Fabric</asp:ListItem>
                                        <asp:ListItem Value="T">Trims</asp:ListItem>
                                    </ucc:DropDownListChosen>
                                </td>
                                <td >&nbsp;</td>
                            </tr>
                            <tr>
                                <td>Remark</td>
                                <td><asp:TextBox ID="txtarea" runat="server" Height="47px" Width="171px"></asp:TextBox></td>
                                <td class="auto-style1">&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
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
                                <td >&nbsp;</td>
                                <td class="auto-style1" >&nbsp;</td>
                                <td >
                                    <asp:Label ID="lbl_mssg" runat="server" Text="*"></asp:Label>
                                </td>
                                <td >&nbsp;</td>
                                <td >&nbsp;</td>
                            </tr>
                            <caption>
                            </caption>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel></td>
        </tr>
        <tr>
            <td class="gridtable">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="tbl_podetails" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" DataKeyNames="PODet_PK" OnRowDataBound="tbl_podetails_RowDataBound" OnRowCommand="tbl_podetails_RowCommand">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:TemplateField HeaderText="PDPK" InsertVisible="False" SortExpression="PODet_PK">
                                  
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_podetpk" runat="server" Text='<%# Bind("PODet_PK") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="SDPK" InsertVisible="False" SortExpression="SkuDet_PK">
                                    
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_skudetpk" runat="server" Text='<%# Bind("SkuDet_PK") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="RMNum" HeaderText="RMNum" SortExpression="RMNum" />
                                <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="ItemName" />
                                <asp:BoundField DataField="ItemColor" HeaderText="ItemColor" ReadOnly="True" SortExpression="ItemColor" />
                                <asp:BoundField DataField="ItemSize" HeaderText="ItemSize" ReadOnly="True" SortExpression="ItemSize" />
                                <asp:TemplateField HeaderText="SupplierColor" SortExpression="SupplierColor">
                                   
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_suppliercolor" runat="server" Text='<%# Bind("SupplierColor") %>'></asp:Label>
                                     <asp:DropDownList ID="ddl_Supcolor" runat="server" DataSourceID="colordata" DataTextField="TemplateColor" DataValueField="TemplateColor" Width="100%">
                                        </asp:DropDownList>
                                    
                                    
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="SupplierSize" SortExpression="SupplierSize">
                                   
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_suppliersize" runat="server" Text='<%# Bind("SupplierSize") %>'></asp:Label>
                                         <asp:DropDownList ID="ddl_SupSize" runat="server" DataSourceID="Sizedata" DataTextField="TemplateSize" DataValueField="TemplateSize" Width="100%">
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="POQty" SortExpression="POQty">
                                  
                                    <ItemTemplate>
                                          <asp:TextBox ID="txt_poqty" runat="server" Text='<%# Bind("POQty") %>' Height="16px" Width="60px"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="UomCode" HeaderText="Uom" SortExpression="UomCode" />
                                <asp:TemplateField HeaderText="UnitRate" SortExpression="POUnitRate">
                                  
                                    <ItemTemplate>
                                        <asp:TextBox ID="txt_unitrate" runat="server" Height="16px"   Width="50px" Text='<%# Bind("POUnitRate") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="CURate">
                                     <ItemTemplate>
                                        <asp:TextBox ID="txt_curate" Height="16px"   Width="50px"  runat="server" Text='<%# Bind("CURate") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                  <asp:ButtonField CommandName="Delete" HeaderText="Show" Text="Delete" />
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
            </td>
        </tr>
         <tr class="SmallSearchButton">
            <td>

                <asp:UpdatePanel ID="UpdatePanel3" ChildrenAsTriggers="true" runat="server">
                    <ContentTemplate>
                        <asp:Button ID="Btn_submit" runat="server" Text="Update" Height="25px" OnClick="Btn_submit_Click" style="font-size: small; font-family: Calibri; text-align: center" />
                    </ContentTemplate>
                </asp:UpdatePanel>

            </td>
        </tr>
        <tr>
            <td>
         
              <div id="Messaediv" runat="server">
                 


                           <asp:Label ID="lbl_msg" runat="server" Text="*"></asp:Label>


                     
               </div>

               
                <asp:SqlDataSource ID="supplierdata" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT [SupplierName], [Supplier_PK] FROM [SupplierMaster] ORDER BY [SupplierName]"></asp:SqlDataSource>
                                <asp:HiddenField ID="convfact" runat="server" Value="1" />
                                <asp:SqlDataSource ID="currencydata" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT * FROM [CurrencyMaster] ORDER BY [CurrencyCode], [CurrencyID]" ProviderName="<%$ ConnectionStrings:ArtConnectionString.ProviderName %>"></asp:SqlDataSource>
                                <asp:SqlDataSource ID="PaymentMode" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT [PaymentModeID], [PaymentModeCode] FROM [PaymentModeMaster] ORDER BY [PaymentModeCode]"></asp:SqlDataSource>
                    <asp:SqlDataSource ID="Paymenttermdata" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" OldValuesParameterFormatString="original_{0}" SelectCommand="SELECT PaymentTermCode, PaymentTermID FROM PaymentTermMaster ORDER BY PaymentTermCode">
                    </asp:SqlDataSource>
                    <asp:SqlDataSource ID="Wharehousedata" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT LocationName, Location_PK FROM LocationMaster WHERE (LocType = N'W')"></asp:SqlDataSource>
                    <asp:SqlDataSource ID="colordata" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT DISTINCT TemplateColor FROM TemplateColor GROUP BY TemplateColor"></asp:SqlDataSource>
                    <asp:SqlDataSource ID="Sizedata" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT DISTINCT TemplateSize FROM TemplateSize GROUP BY TemplateSize"></asp:SqlDataSource>
                <asp:SqlDataSource ID="deliveryterm" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT [DeliveryTerm], [DeliveryTerms_Pk] FROM [DeliveryTermMaster]"></asp:SqlDataSource>
                <asp:SqlDataSource ID="DeliveryMethodData" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT [Deliverymethod_Pk], [DeliveryMethod] FROM [DeliveryMethodMaster]"></asp:SqlDataSource>
            </td>
        </tr>
    </table>
</asp:Content>

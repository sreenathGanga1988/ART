<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="StockRO.aspx.cs" Inherits="ArtWebApp.Merchandiser.StockRO" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">


       
       

       
        .hidden {
            display: none;
        }

     


       

        .auto-style9 {
            height: 24px;
        }
        .auto-style10 {
            width: 182px;
        }


.igdd_ControlArea
{
	border:solid 1px #BBBBBB;
	table-layout: fixed;
}


.igdd_ControlArea
{
	border:solid 1px #BBBBBB;
	table-layout: fixed;
}


.igdd_ControlArea
{
	border:solid 1px #BBBBBB;
	table-layout: fixed;
}


.igdd_ControlArea
{
	border:solid 1px #BBBBBB;
	table-layout: fixed;
}


.igdd_ValueDisplay
{
	background-color:Transparent;
	font-weight:normal;
	font-size:10pt;
	font-family: Verdana, Arial, Helvetica, sans-serif;
	border-width:0px;
	width: 100%;
}
.igdd_ValueDisplay
{
	background-color:Transparent;
	font-weight:normal;
	font-size:10pt;
	font-family: Verdana, Arial, Helvetica, sans-serif;
	border-width:0px;
	width: 100%;
}
.igdd_ValueDisplay
{
	background-color:Transparent;
	font-weight:normal;
	font-size:10pt;
	font-family: Verdana, Arial, Helvetica, sans-serif;
	border-width:0px;
	width: 100%;
}
.igdd_ValueDisplay
{
	background-color:Transparent;
	font-weight:normal;
	font-size:10pt;
	font-family: Verdana, Arial, Helvetica, sans-serif;
	border-width:0px;
	width: 100%;
}
.igdd_DropDownButton
{
	width: 17px;
	z-index: 9999;
}

.igdd_DropDownButton
{
	width: 17px;
	z-index: 9999;
}

.igdd_DropDownButton
{
	width: 17px;
	z-index: 9999;
}

.igdd_DropDownButton
{
	width: 17px;
	z-index: 9999;
}

.igdd_DropDownListContainer
{
	background-color:White;
	border:solid 1px #BBBBBB;
	float: left;
}


.igdd_DropDownListContainer
{
	background-color:White;
	border:solid 1px #BBBBBB;
	float: left;
}


.igdd_DropDownListContainer
{
	background-color:White;
	border:solid 1px #BBBBBB;
	float: left;
}


.igdd_DropDownListContainer
{
	background-color:White;
	border:solid 1px #BBBBBB;
	float: left;
}


.igdd_DropDownList
{
	background-color:White;
	font-size:10pt;
	font-family: Verdana, Arial, Helvetica, sans-serif;
	margin:0px;
	padding:1px;
}


.igdd_DropDownList
{
	background-color:White;
	font-size:10pt;
	font-family: Verdana, Arial, Helvetica, sans-serif;
	margin:0px;
	padding:1px;
}


.igdd_DropDownList
{
	background-color:White;
	font-size:10pt;
	font-family: Verdana, Arial, Helvetica, sans-serif;
	margin:0px;
	padding:1px;
}


.igdd_DropDownList
{
	background-color:White;
	font-size:10pt;
	font-family: Verdana, Arial, Helvetica, sans-serif;
	margin:0px;
	padding:1px;
}


        .auto-style16 {
            width: 79px;
        }
        .auto-style17 {
            width: 135px;
        }
        .auto-style18 {
            width: 226px;
        }
        .auto-style13 {
            font-size: small;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="auto-style1">
        <tr>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td><strong>Request Order Transfer For&nbsp; </strong> </td>
        </tr>
        <tr>
            <td>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="tbl_Podetails" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4" OnRowDataBound="tbl_Podetails_RowDataBound" style="font-size: small; font-family: Calibri" Width="100%">
                            <Columns>
                                <asp:TemplateField HeaderImageUrl="~/Image/tick.jpg">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chk_select" runat="server" AutoPostBack="True" OnCheckedChanged="chk_select_CheckedChanged" />
                                </ItemTemplate>
                            </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-CssClass="hidden" HeaderText="SkuDet_pk" ItemStyle-CssClass="hidden">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_skudet_pk" runat="server" Text='<%# Bind("SkuDet_pk") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="hidden" />
                                    <ItemStyle CssClass="hidden" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Template_pk">
                                    
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_templatepk" runat="server" Text='<%# Bind("Template_pk") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="RMNum" HeaderText="RMNum" />
                                <asp:BoundField DataField="Description" HeaderText="Description" />
                                <asp:TemplateField HeaderStyle-CssClass="hidden" HeaderText="ItemColor" ItemStyle-CssClass="hidden">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_itemcolor" runat="server" Text='<%# Bind("ItemColor") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="hidden" />
                                    <ItemStyle CssClass="hidden" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-CssClass="hidden" HeaderText="ItemSize" ItemStyle-CssClass="hidden">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_itemsize" runat="server" Text='<%# Bind("ItemSize") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="hidden" />
                                    <ItemStyle CssClass="hidden" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="SupplierColor" HeaderText="SupplierColor" />
                                <asp:BoundField DataField="SupplierSize" HeaderText="SupplierSize" />
                                <asp:TemplateField HeaderText="UOM">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_UOMCode" runat="server" Text='<%# Bind("UOMCode") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-CssClass="hidden" HeaderText="AUOM" ItemStyle-CssClass="hidden">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_altuomPK" runat="server" Text='<%# Bind("AltUOM_pk") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="hidden" />
                                    <ItemStyle CssClass="hidden" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="BalanceQty" HeaderText="BalanceQty" />
                                <asp:TemplateField HeaderText="CURate" SortExpression="Unitrate">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_costunitrate" runat="server" Text='<%# Bind("Unitrate") %>'></asp:Label>
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
        <tr>
            <td class="auto-style9">
                <asp:Label ID="lbl_message" runat="server" Text="Label"></asp:Label>
            </td>
        </tr>
        <tr>
            <td><strong>Request Order Transfer From&nbsp; Atc</strong></td>
        </tr>
        <tr>
            <td>
                <table class="auto-style1">
                    <tr>
                        <td class="auto-style10">WareHouse</td>
                        <td class="auto-style18">
                             
                            <ucc:DropDownListChosen ID="cmb_warehouse" runat="server" DataSourceID="Wharehousedata" DataTextField="LocationName" DataValueField="Location_PK" Height="16px" Width="210px">
                            </ucc:DropDownListChosen>
                    
               
                               
                        </td>
                        <td class="auto-style16">
                                    &nbsp;</td>
                        <td class="auto-style17">
                             
                            &nbsp;</td>
                        <td>
                                    <asp:Button ID="ShowBom" runat="server"  Text="S" CssClass="auto-style13" Height="20px" Width="29px" OnClick="ShowBom_Click"  />
                        </td>
                        <td>
                            &nbsp;</td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td> <asp:UpdatePanel ID="UpdatePanel2"  UpdateMode="Conditional" runat="server">
                <ContentTemplate>
                    <asp:GridView ID="tbl_InverntoryDetails" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" style="font-size: small; font-family: Calibri; font-weight: 400;" Width="100%" BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4" DataKeyNames="SInventoryItem_PK" >
                                    <Columns>
                                        <asp:TemplateField HeaderImageUrl="~/Image/tick.jpg">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chk_selectitem" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                          <asp:TemplateField HeaderText="II_PK">
                                            <ItemTemplate>
                                                <asp:Label ID="lblInventoryItem_PK" runat="server" Text='<%# Bind("SInventoryItem_PK") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        
                                        <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description" />
                                        <asp:BoundField DataField="Composition" HeaderText="Composition" SortExpression="Composition" />
                                        <asp:BoundField DataField="Construct" HeaderText="Construct" SortExpression="Construct" />
                                        <asp:BoundField DataField="TemplateColor" HeaderText="TemplateColor" SortExpression="TemplateColor" />
                                        <asp:BoundField DataField="TemplateSize" HeaderText="TemplateSize" SortExpression="TemplateSize" />
                                        <asp:BoundField DataField="width" HeaderText="width" ReadOnly="True" SortExpression="width" />
                                        <asp:TemplateField HeaderText="Unitprice" SortExpression="Unitprice">
                                         
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_fromrate" runat="server" Text='<%# Bind("CuRate") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                           <asp:BoundField DataField="UomName" HeaderText="UomName" SortExpression="UomName" />
                                        <asp:BoundField DataField="ReceivedQty" HeaderText="ReceivedQty" SortExpression="ReceivedQty" />
                                       
                                     
                                         <asp:TemplateField HeaderText="OnhandQty">
                                            
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_onhandQty" runat="server" Text='<%# Bind("OnHandQty") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="ROQty" SortExpression="deliveryqty">
                                           
                                            <ItemTemplate>
                                                <asp:TextBox ID="txt_deliveryQty" runat="server" Text='<%# Bind("deliveryqty") %>'></asp:TextBox>
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
        <tr>
            <td>
        
                <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Generate  RO" />
                    
               
                               
            </td>
        </tr>
        <tr>
            <td>
        
               <div id="Messaediv" runat="server">
                 


                           <asp:Label ID="lbl_msg" runat="server" Text="*"></asp:Label>


                     
               </div> </td>
        </tr>
        <tr>
            <td>
        
                <asp:SqlDataSource ID="atcdata" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" 
                    SelectCommand="SELECT DISTINCT AtcNum, AtcId FROM AtcMaster WHERE (IsClosed = N'N') ORDER BY AtcNum, AtcId">
                </asp:SqlDataSource>
                    
               
                               
                    <asp:SqlDataSource ID="Wharehousedata" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT LocationName, Location_PK FROM LocationMaster WHERE (LocType = N'W')"></asp:SqlDataSource>
                    
               
                               
            </td>
        </tr>
    </table>
</asp:Content>

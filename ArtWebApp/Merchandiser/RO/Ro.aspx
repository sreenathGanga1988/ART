﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="Ro.aspx.cs" Inherits="ArtWebApp.Merchandiser.Ro" %>
<%@ Register assembly="Infragistics35.Web.v12.1, Version=12.1.20121.2236, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.ListControls" tagprefix="ig" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../css/style.css" rel="stylesheet" />
    <style type="text/css">


       
       

       
     
     


       

        .auto-style9 {
            height: 24px;
        }
        .auto-style10 {
            width: 182px;
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
            <td class="SUBRedHeadding"><strong>Request Order Transfer For&nbsp; </strong> </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
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
            <td class="SUBRedHeadding"><strong>Request Order Transfer From </strong> </td>
        </tr>
        <tr>
            <td>
                <table class="auto-style1">
                    <tr>
                        <td class="auto-style10">WareHouse</td>
                        <td class="auto-style18">
                             
                            <ucc:DropDownListChosen ID="cmb_warehouse" runat="server" DataSourceID="Wharehousedata"  DataTextField="LocationName" DataValueField="Location_PK" Height="16px" Width="210px">
                            </ucc:DropDownListChosen>
                    
               
                               
                        </td>
                        <td class="auto-style16">
                                    Atc # : 
                                </td>
                        <td class="auto-style17">
                             
                            <ucc:DropDownListChosen ID="cmb_atc" runat="server" DataSourceID="atcdata" DataTextField="AtcNum" DataValueField="AtcId" Height="18px" Width="132px">
                            </ucc:DropDownListChosen>
                    
               
                               
                        </td>
                        <td>
                                    <asp:Button ID="ShowBom" runat="server"  Text="S" CssClass="auto-style13" Height="20px" Width="29px" OnClick="ShowBom_Click"  />
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td> <asp:UpdatePanel ID="UpdatePanel2"  UpdateMode="Conditional" runat="server">
                    <ContentTemplate>
                                <asp:GridView ID="tbl_InverntoryDetails" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" style="font-size: small; font-family: Calibri; font-weight: 400;" Width="100%" BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4">
                                    <Columns>
                                        <asp:TemplateField HeaderImageUrl="~/Image/tick.jpg">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chk_selectitem" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="II_PK">
                                            <ItemTemplate>
                                                <asp:Label ID="lblInventoryItem_PK" runat="server" Text='<%# Bind("InventoryItem_PK") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="SkuDet_Pk">
                                             <ItemTemplate>
                                                <asp:Label ID="lbl_fromSkuDet_Pk" runat="server" Text='<%# Bind("SkuDet_Pk") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="RMNum" HeaderText="RMNum" />
                                        <asp:BoundField DataField="Description" HeaderText="Description" />
                                        <asp:BoundField DataField="ItemColor" HeaderText="ItemColor" />
                                        <asp:BoundField DataField="ItemSize" HeaderText="ItemSize" />
                                        <asp:BoundField DataField="SupplierColor" HeaderText="SupplierColor" />
                                        <asp:BoundField DataField="Suppliersize" HeaderText="Suppliersize" />
                                        <asp:TemplateField HeaderText="CURate">
                                           
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_fromcurate" runat="server" Text='<%# Bind("CURate") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="UOMCode" HeaderText="UOM" />
                                         <asp:BoundField DataField="TotalOnhand" HeaderText="Total Onhand" />
                                             <asp:BoundField DataField="BlockedQty" HeaderText="Blocked Qty" />
                                        <asp:TemplateField HeaderText="OnhandQty">
                                          
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_onhandQty" runat="server" Text='<%# Bind("OnhandQty") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ROQty">
                                           
                                            <ItemTemplate>
                                                <asp:TextBox ID="txt_deliveryQty" runat="server"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Uom_Pk">
                                         
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_newUomPK" runat="server" Text='<%# Bind("Uom_Pk") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        

                                        <asp:BoundField DataField="Refnum" HeaderText="Refnum" />
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
        
                <asp:Button ID="btn_stockro" runat="server" OnClick="btn_stockro_Click" Text="RO from Gstock" />
            </td>
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

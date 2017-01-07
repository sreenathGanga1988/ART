<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="ItemColorAddition.aspx.cs" Inherits="ArtWebApp.Merchandiser.ItemColorAddition" %>
<%@ Register assembly="Infragistics35.Web.v12.1, Version=12.1.20121.2236, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.ListControls" tagprefix="ig" %>
<%@ Register assembly="DropDownChosen" namespace="CustomDropDown" tagprefix="ucc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../css/style.css" rel="stylesheet" type="text/css" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      &nbsp;<table class="FullTable" style="font-family: Calibri">
                <tr class="RedHeadding">
                    <td >ITEM COLOR/SIZE</td>
                </tr>
                <tr>
                    <td>
                        
                        <table class="DataEntryTable">
                            <tr>
                                <td class="NormalTD" >ATC # : </td>
                                <td class="NormalTD" >
                             
                    <ig:webdropdown ID="cmb_atc" runat="server" Width="200px" 
                    DataSourceID="SqlDataSource1" TextField="Atcnum" ValueField="AtcId" Font-Names="Calibri" Font-Size="Small" Height="22px" style="font-size: x-small">
                    <DropDownItemBinding TextField="Atcnum" ValueField="AtcId" />
                </ig:webdropdown>
                    
               
                               
                                </td>
                                <td class="NormalTD" >
                                    <asp:Button ID="ShowBom" runat="server"  Text="S" OnClick="ShowBom_Click"  Height="23px" Width="34px"  />
                                </td>
                                <td class="NormalTD" >
                                    </td>
                                <td class="NormalTD" ></td>
                                <td class="NormalTD" ></td>
                            </tr>
                            <tr>
                                <td  class="NormalTD" >Item color</td>
                                <td >
                                     <ucc:DropDownListChosen  ID="drp_color"  Width="170px" runat="server">
                                            </ucc:DropDownListChosen>
                                </td>
                                <td >
                                   
                                    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Update ItemColor" />
                                </td>
                                <td >
                                   
                                    &nbsp;</td>
                                <td >&nbsp;</td>
                                <td >&nbsp;</td>
                            </tr>
                            <tr>
                                <td class="NormalTD" >Item Size</td>
                                <td class="NormalTD" >
                                     <ucc:DropDownListChosen  ID="drp_size"  Width="170px" runat="server">
                                            </ucc:DropDownListChosen>
                                </td>
                                <td class="NormalTD" >
                                   
                                    <asp:Button ID="Button2" runat="server" Text="Update Item Size" OnClick="Button2_Click" />
                                   
                                    </td>
                                <td class="NormalTD" >
                                   
                                    </td>
                                <td class="NormalTD" ></td>
                                <td class="NormalTD" ></td>
                            </tr>
                        </table>

                        
                    </td>
                </tr>
                <tr>
                    <td style="width: 100%">
                        <div id="grid"style="overflow:auto" >
                            <asp:UpdatePanel ID="Upd_maingrid" UpdateMode="Conditional" ChildrenAsTriggers="false" runat="server">
                    <ContentTemplate>
                       
                        <asp:GridView ID="tbl_bom" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4" style="font-size: x-small; font-family: Calibri" Width="1033px" Font-Size="Large">
                            <Columns>                               
                                <asp:TemplateField>                                   
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chk_select" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:BoundField DataField="RMNum" HeaderText="ITEM NUMBER" SortExpression="RMNum" />
                                <asp:BoundField DataField="Description" HeaderText="DESCRIPTION" />
                                <asp:TemplateField HeaderText="COLOR CODE">
                                   
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_colorcode" runat="server" Text='<%# Bind("ColorCode") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="SIZE CODE">
                                    
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_sizecode" runat="server" Text='<%# Bind("SizeCode") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ITEM COLOR">
                                    
                                    <ItemTemplate>
                                       
                                        <asp:Label ID="lbl_itemcolor" runat="server" Text='<%# Bind("ItemColor") %>'></asp:Label>
                                       
                                               
                                          
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ITEM SIZE">
                                        <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("ItemSize") %>'></asp:Label>
                                             
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:BoundField DataField="UomCode" HeaderText="UOM" />
                              

                                <asp:TemplateField HeaderText="CM" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" SortExpression="isCommon">
                                  
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_iscm" runat="server" Text='<%# Bind("isCommon") %>'></asp:Label>
                                    </ItemTemplate>

<HeaderStyle CssClass="hidden"></HeaderStyle>

<ItemStyle CssClass="hidden"></ItemStyle>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="SD" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden"  SortExpression="IsSD">
                                   
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_isSd" runat="server" Text='<%# Bind("IsSD") %>'></asp:Label>
                                    </ItemTemplate>

<HeaderStyle CssClass="hidden"></HeaderStyle>

<ItemStyle CssClass="hidden"></ItemStyle>
                                </asp:TemplateField>

                                  <asp:TemplateField HeaderText="CD" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden"  SortExpression="IsCD">
                                  
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_isCD" runat="server" Text='<%# Bind("IsCD") %>'></asp:Label>
                                    </ItemTemplate>

<HeaderStyle CssClass="hidden"></HeaderStyle>

<ItemStyle CssClass="hidden"></ItemStyle>
                                </asp:TemplateField>





                                <asp:TemplateField HeaderText="SkuDet_PK" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" >
                                   
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_skudetpk" runat="server" Text='<%# Bind("SkuDet_PK") %>'></asp:Label>
                                    </ItemTemplate>

<HeaderStyle CssClass="hidden"></HeaderStyle>

<ItemStyle CssClass="hidden"></ItemStyle>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Template_PK"  ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden"  >
                                   
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_templatepk" runat="server" Text='<%# Bind("Template_PK") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="hidden" />
                                    <ItemStyle CssClass="hidden" />
                                </asp:TemplateField>


                                

                            </Columns>
                            <FooterStyle BackColor="#FFFFCC" ForeColor="#000066" />
                            <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="#FFFFCC" />
                            <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
                            <RowStyle BackColor="White" ForeColor="#330099" />
                            <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Black" />
                            <SortedAscendingCellStyle BackColor="#FEFCEB" />
                            <SortedAscendingHeaderStyle BackColor="#AF0101" />
                            <SortedDescendingCellStyle BackColor="#F6F0C0" />
                            <SortedDescendingHeaderStyle BackColor="#7E0000" />
                        </asp:GridView>


                         </ContentTemplate>
                                </asp:UpdatePanel>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" 
                    SelectCommand="SELECT DISTINCT AtcNum, AtcId FROM AtcMaster WHERE (IsClosed = N'N') ORDER BY AtcNum, AtcId">
                </asp:SqlDataSource>
                    
               
                               
                            
               
                               
                                </td>
                </tr>
            </table>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="ExtraQtyRequest.aspx.cs" Inherits="ArtWebApp.Merchandiser.PO.ExtraQtyRequest" %>
<%@ Register assembly="Infragistics35.Web.v12.1, Version=12.1.20121.2236, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.ListControls" tagprefix="ig" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">





        .hidden {
            display: none;
        }


      

    </style>
    <link href="../../css/style.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      &nbsp;<table class="FullTable" style="font-family: Calibri">
                <tr class="RedHeadding">
                    <td >extra BILL OF MATERIAL request</td>
                </tr>
                <tr>
                    <td>
                        
                        <table class="DataEntryTable">
                            <tr>
                                <td >ATC # : </td>
                                <td >
                             
                                    <ucc:DropDownListChosen ID="cmb_atc" runat="server" DataSourceID="SqlDataSource1" DataTextField="AtcNum" DataValueField="AtcId" Height="17px" Width="200px">
                                    </ucc:DropDownListChosen>
                    
               
                               
                                </td>
                                <td >
                                    
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <asp:Button ID="ShowBom" runat="server"  Text="S" OnClick="ShowBom_Click"  Height="23px" Width="34px"  />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td >
                                    &nbsp;</td>
                                <td >QTY :</td>
                                <td >&nbsp;</td>
                            </tr>
                            <tr>
                                <td >
                                    explanation</td>
                                <td ><asp:TextBox ID="txtarea" runat="server" Height="47px" Width="171px"></asp:TextBox>
                                </td>
                                <td >&nbsp;</td>
                                <td>&nbsp;</td>
                                <td >
                                    &nbsp;</td>
                                <td >&nbsp;</td>
                            </tr>

                              <tr>
                                <td>Merchandiser</td>
                                <td>
                                    <asp:TextBox ID="txt_merchand" runat="server"></asp:TextBox>
                                </td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td colspan="5" ><div id="Messaediv" runat="server">
                 


                           <asp:Label ID="lbl_msg" runat="server" Text="*"></asp:Label>


                     
               </div></td>
                                <td >&nbsp;</td>
                            </tr>



                        </table>

                        
                    </td>
                </tr>
                <tr>
                    <td style="width: 100%">
                        <div id="grid"style="overflow:auto" >
                            <asp:UpdatePanel ID="Upd_maingrid" UpdateMode="Conditional" ChildrenAsTriggers="false" runat="server">
                    <ContentTemplate>
                       
                        <asp:GridView ID="tbl_bom" runat="server" AutoGenerateColumns="False" OnRowCommand="tbl_bom_RowCommand1" BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4" style="font-size: x-small; font-family: Calibri" Width="1033px" Font-Size="Large" OnDataBound="tbl_bom_DataBound">
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
                                <asp:TemplateField HeaderText="SIZE CODE" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" >
                                    
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_sizecode" runat="server" Text='<%# Bind("SizeCode") %>'></asp:Label>
                                         
                                    </ItemTemplate>
                                </asp:TemplateField>

                                 <asp:TemplateField HeaderText="SIZE">
                                    
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_sizecname" runat="server" Text='<%# Bind("SizeName") %>'></asp:Label>
                                         
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
                              

                                <asp:BoundField DataField="UnitRate" HeaderText="UNIT RATE" />

                                 <asp:TemplateField HeaderText="OrderMin">
                                     
                                     <ItemTemplate>
                                         <asp:Label ID="lbl_ordermin" runat="server" Text='<%# Bind("OrderMin") %>'></asp:Label>
                                     </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="RQD QTY">
                                    
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_rqdqty" runat="server" Text='<%# Bind("RqdQty") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="P.O. ISSUE QTY">
                                   
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_PoIssuedQty" runat="server" Text='<%# Bind("PoIssuedQty") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="BALANCE QTY">
                                   
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_balanceqty" runat="server" Text='<%# Bind("BalanceQty") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="eXTRA QTY rEQUIRED">
                                   
                                    <ItemTemplate>
                                        <asp:TextBox ID="txt_extraqty" runat="server" Text="0" ></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
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





                                <asp:BoundField DataField="AltUOM_pk" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden"  HeaderText="ALT UOM" >
<HeaderStyle CssClass="hidden"></HeaderStyle>

<ItemStyle CssClass="hidden"></ItemStyle>
                                </asp:BoundField>
                              
                               
                                <asp:TemplateField HeaderText="UOM_pk "  ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden"  >
                                  
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_uompk" runat="server" Text='<%# Bind("uom_pk") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="hidden" />
                                    <ItemStyle CssClass="hidden" />
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
                
                    
               
                               
                            
               
                               
                                <asp:Button ID="Btn_extraRequest" runat="server" OnClick="Btn_extraRequest_Click" Text="Generate Extra Request" />
                
                    
               
                               
                            
               
                               
                                </td>
                </tr>
                <tr>
                    <td><asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" 
                    SelectCommand="SELECT DISTINCT AtcNum, AtcId FROM AtcMaster WHERE (IsClosed = N'N') ORDER BY AtcNum, AtcId">
                </asp:SqlDataSource>
                        &nbsp;</td>
                </tr>
            </table>
</asp:Content>

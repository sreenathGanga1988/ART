<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="BOM.aspx.cs" Inherits="ArtWebApp.Merchandiser.BOM" %>
<%@ Register assembly="Infragistics35.Web.v12.1, Version=12.1.20121.2236, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.ListControls" tagprefix="ig" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">





        .hidden {
            display: none;
        }


      
        .HeaderFreez
{
position:relative ;
top:expression(this.offsetParent.scrollTop);
z-index: 10;
}


      

    </style>
    <link href="../css/style.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      &nbsp;<table class="FullTable" style="font-family: Calibri">
                <tr class="RedHeadding">
                    <td >BILL OF MATERIAL</td>
                </tr>
                <tr>
                    <td>
                        
                        <table class="DataEntryTable">
                            <tr>
                                <td >ATC # : </td>
                                <td >
                             
                                    <ucc:DropDownListChosen ID="cmb_atc" runat="server" DataSourceID="SqlDataSource1" DataTextField="AtcNum" DataValueField="AtcId" Height="17px" Width="200px" OnSelectedIndexChanged="cmb_atc_SelectedIndexChanged">
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
                                    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Regenerate BOM" Font-Bold="True" Font-Size="Smaller" />
                                </td>
                                <td >QTY :</td>
                                <td >&nbsp;</td>
                            </tr>
                            <tr>
                                <td >
                                    <asp:Button ID="btn_addtoPO" runat="server" Font-Bold="True" Font-Size="Smaller" OnClick="btn_addtoPO_Click" Text="Add to Existing PO" />
                                </td>
                                <td >
                                    <asp:Button ID="Button2" runat="server" Text="Generate Foc PO" OnClick="Button2_Click1" Font-Bold="True" Font-Size="Smaller" />
                                </td>
                                <td >
                                   
                                    <asp:Button ID="btn_PO" runat="server"  Text="Generate PO" OnClick="btn_PO_Click" CssClass="auto-style13" Height="22px" Font-Bold="True" Font-Size="Smaller" />
                                   
                                </td>
                                <td >
                                   
                                    <asp:Button ID="btn_RO" runat="server"  Text="Request  RO" OnClick="btn_RO_Click" CssClass="auto-style13" Height="22px" Font-Bold="True" Font-Size="Smaller" />
                                   
                                </td>
                                <td >&nbsp;</td>
                                <td >&nbsp;</td>
                            </tr>
                            <tr>
                                <td colspan="6" >
                                <asp:UpdateProgress ID="upProgClaimantSearch" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                                    <ProgressTemplate>
            <img src="../Image/ProgressCircle.gif" alt="" class="auto-style8" />
                                    </ProgressTemplate>
                        </asp:UpdateProgress></td>
                               
                            </tr>
                        </table>

                        
                    </td>
                </tr>
                <tr>
                    <td style="width: 100%">
                        <div id="grid"style="overflow:auto" >
                            <asp:UpdatePanel ID="Upd_maingrid" UpdateMode="Conditional" ChildrenAsTriggers="false" runat="server">
                    <ContentTemplate>
                       
                        <asp:GridView ID="tbl_bom" runat="server" HeaderStyle-CssClass="HeaderFreez" AutoGenerateColumns="False" OnRowCommand="tbl_bom_RowCommand1" BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4" style="font-size: x-small; font-family: Calibri" Width="1033px" Font-Size="Large" OnDataBound="tbl_bom_DataBound">
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
                                        <asp:UpdatePanel ID="upd_color" UpdateMode="Conditional" runat="server">
                                            <ContentTemplate>
                                                 <asp:DropDownList ID="drp_color" runat="server" Font-Names="Calibri" Font-Size="X-Small" Height="16px"  Width="100%" Visible="False">
                                                </asp:DropDownList>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                               
                                          
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ITEM SIZE">
                                        <ItemTemplate>
                                        <asp:Label ID="lbl_itemsize" runat="server" Text='<%# Bind("ItemSize") %>'></asp:Label>
                                               <asp:UpdatePanel ID="upd_size"  UpdateMode="Conditional" runat="server">
                                            <ContentTemplate>
                                        <asp:DropDownList ID="drp_size" Width="100%" runat="server"  Font-Names="Calibri" Font-Size="X-Small" Height="16px" Visible="False">
                                        </asp:DropDownList>
                                                 </ContentTemplate>
                                        </asp:UpdatePanel>
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
                              
                                <asp:ButtonField ButtonType="Image" HeaderImageUrl="~/Image/upload.jpg" ImageUrl="~/Image/tick.jpg" Text="U" CommandName="UploadItemcolor" />
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


                                

                                <asp:ButtonField  Text="Show Drop" CommandName="ShowDropDown" />
                              
                           
                       
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

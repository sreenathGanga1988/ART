<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="InventoryMisplaced.aspx.cs" Inherits="ArtWebApp.Inventory.Inventory_Requests.InventoryMisplaced" %>
<%@ Register assembly="Infragistics35.Web.v12.1, Version=12.1.20121.2236, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.ListControls" tagprefix="ig" %>
<%@ Register assembly="Infragistics35.Web.v12.1, Version=12.1.20121.2236, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.EditorControls" tagprefix="ig" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    
    <link href="../../css/style.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
        <table class="FullTable">

            <tr>
                <td>
<table class="DataEntryTable">
                <tr>
                    <td class="RedHeadding" colspan="9">
                        Goods Misplaced(Fabric &amp; Trims)</td>
                </tr>
                <tr>
                    <td class="NormalTD">
                        FROM</td>
                    <td class="NormalTD">
                        <ucc:DropDownListChosen ID="drp_fromWarehouse" runat="server" Width="200px">
                        </ucc:DropDownListChosen>     
                        &nbsp;</td>
                    <td class="NormalTD" >
                        &nbsp;</td>
                    <td >
                        &nbsp;</td>
                    <td >
                        &nbsp;</td>
                    <td >
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td c>
                        &nbsp;</td>
                    <td >
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="NormalTD">
                        Order No</td>
                    <td class="NormalTD">
                             
                        <ucc:DropDownListChosen ID="cmb_atc" runat="server" DataSourceID="SqlDataSource1" DataTextField="AtcNum" DataValueField="AtcId" DisableSearchThreshold="10" Width="200px">
                        </ucc:DropDownListChosen>
                    
               
                               
                    </td>
                    <td class="NormalTD" >
                        <asp:Button ID="btn_confirmAtc" runat="server" Text="S" Width="33px" OnClick="btn_confirmAtc_Click" CssClass="auto-style10" />
                    </td>
                    <td >
                        &nbsp;Date:</td>
                    <td >
                        <ig:WebDatePicker ID="dtp_dodate" runat="server" Height="26px" Width="191px">
                        </ig:WebDatePicker>
                    </td>
                    <td >
                        Explanation:</td>
                    <td>
                        <asp:TextBox ID="txt_explanation" runat="server" Height="20px" Width="164px" CssClass="auto-style10"></asp:TextBox>
                    </td>
                    <td c>
                        &nbsp;</td>
                    <td >
                        &nbsp;</td>
                </tr>
                <tr>
                    <td >
                        &nbsp;</td>
                    <td >
                        
                    </td>
                    <td class="NormalTD" >
                        &nbsp;</td>
                    <td >
                        &nbsp;</td>
                    <td >
                        &nbsp;</td>
                    <td >
                        &nbsp;</td>
                    <td >
                         &nbsp;</td>
                </tr>
                <tr>
                    <td class="gridtable" colspan="9">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="tbl_InverntoryDetails" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" style="font-size: small; font-family: Calibri; font-weight: 400;" Width="100%" BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4">
                                    <Columns>
                                        <asp:TemplateField HeaderImageUrl="~/Image/tick.jpg">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chk_select" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="II_PK">
                                            <ItemTemplate>
                                                <asp:Label ID="lblInventoryItem_PK" runat="server" Text='<%# Bind("InventoryItem_PK") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="RMNum" HeaderText="RMNum" />
                                        <asp:BoundField DataField="Description" HeaderText="Description" />
                                        <asp:BoundField DataField="ItemColor" HeaderText="ItemColor" />
                                        <asp:BoundField DataField="ItemSize" HeaderText="ItemSize" />
                                        <asp:BoundField DataField="SupplierColor" HeaderText="S Color" />
                                        <asp:BoundField DataField="Suppliersize" HeaderText="S Size" />
                                        <asp:BoundField DataField="UOMCode" HeaderText="UOM" />
                                        <asp:BoundField DataField="ReceivedQty" HeaderText="RecievedQty" />
                                        <asp:BoundField DataField="DeliveredQty" HeaderText="DeliveredQty" />
                                        <asp:TemplateField HeaderText="OnhandQty">
                                            
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_onhandQty" runat="server" Text='<%# Bind("OnhandQty") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="MissPlacedQty">
                                           
                                            <ItemTemplate>
                                                <asp:TextBox ID="txt_deliveryQty" runat="server"></asp:TextBox>
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
                <tr class="ButtonTR">
                    <td >
                        &nbsp;</td>
                    <td >
                        &nbsp;</td>
                    <td class="auto-style7" >
                        &nbsp;</td>
                    <td >
                        &nbsp;</td>
                    <td >
                        <asp:Button ID="btn_saveDO" runat="server" OnClick="btn_saveDO_Click" Text="Submit Request" style="height: 26px" />
                    </td>
                    <td >
                        &nbsp;</td>
                    <td >
                        &nbsp;</td>
                    <td >
                        &nbsp;</td>
                    <td >
                        &nbsp;</td>
                </tr>
                <tr class="ButtonTR">
                    <td colspan="9" >
                       <div id="Messaediv" runat="server">
                 


                           <asp:Label ID="lbl_msg" runat="server" Text="*"></asp:Label>


                     
               </div></td>
                </tr>
            </table>

                </td>
            </tr>
        </table>
      
            
        
   
    <div class="footer">
        
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" 
                    SelectCommand="SELECT DISTINCT AtcNum, AtcId FROM AtcMaster WHERE (IsClosed = N'N') ORDER BY AtcNum, AtcId">
                </asp:SqlDataSource>
                    
               
                               
    </div>

    
</asp:Content>

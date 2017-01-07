<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="DeliveryOrderReceipt.aspx.cs" Inherits="ArtWebApp.Inventory.DeliveryOrderReceipt" %>
<%@ Register assembly="Infragistics35.Web.v12.1, Version=12.1.20121.2236, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.ListControls" tagprefix="ig" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
  
    <link href="../../css/style.css" rel="stylesheet" />
    <script src="../../JQuery/GridJQuery.js"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="FullTable">
    <tr>
        <td class="RedHeadding"> Transfer Reciept(wr/fr)</td>
    </tr>
    <tr>
        <td>
            <table class="DataEntryTable">
                <tr>
                    <td class="NormalTD" >DO #</td>
                    <td >
                             
                  
                    
                 <ucc:dropdownlistchosen ID="cmb_do" runat="server"  TextField="name" ValueField="pk" Width="200px" DataSourceID="DoData" DataTextField="DoNum" DataValueField="Do_pk" DisableSearchThreshold="10">
                                        
                             
                    </ucc:dropdownlistchosen>
                               
                    </td>
                    <td  class="NormalTD">
                        <asp:Button ID="btn_DO" runat="server" OnClick="btn_DO_Click" Text="S" Width="23px" />
                    </td>
                    <td>
                        &nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td  class="NormalTD" >
                             
                                <asp:Label ID="lbl_errordisplayer" runat="server" Text="*" Font-Italic="True" ForeColor="#FF3300"></asp:Label>
                               
                                </td>
                    <td>
                        EXP# :</td>
                    <td>
                        <asp:Label ID="lbl_expnum" runat="server" Text="NA"></asp:Label>
                    </td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:GridView ID="tbl_InverntoryDetails" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4" ShowHeaderWhenEmpty="True" style="font-size: small; font-family: Calibri; font-weight: 400;" Width="100%">
                        <Columns>
                               <asp:TemplateField>  
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="checkAll" runat ="server" onclick="checkAll(this)"/>
                                    </HeaderTemplate>                                 
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chk_select" runat="server" onclick="Check_Click(this)"/>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            <asp:TemplateField HeaderText="II_PK">
                                <ItemTemplate>
                                    <asp:Label ID="lblInventoryItem_PK" runat="server" Text='<%# Bind("InventoryItem_PK") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="DD_PK">
                                
                                <ItemTemplate>
                                    <asp:Label ID="lbl_dodetpk" runat="server" Text='<%# Bind("DODet_PK") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="RMNum" HeaderText="RMNum" />
                            <asp:BoundField DataField="Description" HeaderText="Description" />
                            <asp:BoundField DataField="ItemColor" HeaderText="ItemColor" />
                            <asp:BoundField DataField="ItemSize" HeaderText="ItemSize" />
                            <asp:BoundField DataField="SupplierColor" HeaderText="SupplierColor" />
                            <asp:BoundField DataField="Suppliersize" HeaderText="Suppliersize" />
                            <asp:BoundField DataField="UOMCode" HeaderText="UOM" />
                            <asp:BoundField DataField="DeliveryQty" HeaderText="DeliveryQty" />
                            <asp:BoundField DataField="ReceivedQty" HeaderText="ReceivedQty" />
                            <asp:TemplateField HeaderText="BalanceQty">
                                
                                <ItemTemplate>
                                    <asp:Label ID="lbl_balanceqty" runat="server" Text='<%# Bind("BalanceQty") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Receipt">                               
                                <ItemTemplate>
                                    <asp:TextBox ID="txt_receiptQty" onkeypress="return isNumberKey(event,this)"  runat="server" Text='<%# Bind("BalanceQty") %>'></asp:TextBox>
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
                <asp:Button ID="btn_saveDOR" runat="server" OnClick="btn_saveDO_Click" Text="Save DOR" style="margin-left: 315px" Height="26px" />
        </td>
    </tr>
    <tr>
        <td>
           <div id="Messaediv" runat="server">
                 


                           <asp:Label ID="lbl_msg" runat="server" Text="*"></asp:Label>


                     
               </div></td>
    </tr>
    <tr>
        <td>
            <asp:SqlDataSource ID="DoData" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT DISTINCT [DONum], [DO_PK] FROM [DeliveryOrderMaster] WHERE ([ToLocation_PK] = @ToLocation_PK)">
                <SelectParameters>
                    <asp:SessionParameter DefaultValue="0" Name="ToLocation_PK" SessionField="UserLoc_pk" Type="Decimal" />
                </SelectParameters>
            </asp:SqlDataSource>
        </td>
    </tr>
</table>
</asp:Content>

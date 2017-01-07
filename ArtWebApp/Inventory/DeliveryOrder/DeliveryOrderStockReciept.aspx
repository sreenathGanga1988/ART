<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="DeliveryOrderStockReciept.aspx.cs" Inherits="ArtWebApp.Inventory.DeliveryOrder.DeliveryOrderStockReciept" %>
<%@ Register assembly="DropDownChosen" namespace="CustomDropDown" tagprefix="ucc" %>
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
                    <td class="NormalTD">DO #</td>
                    <td class="NormalTD">
                             <ucc:dropdownlistchosen ID="cmb_do" runat="server"  TextField="DoNum" ValueField="Do_pk" Width="200px" DataSourceID="DoData" DataTextField="SDONum" DataValueField="SDO_PK" DisableSearchThreshold="10">
                                        
                             
                    </ucc:dropdownlistchosen>
               
                               
                    </td>
                    <td class="NormalTD">
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
                    <td >
                             
                                <asp:Label ID="lbl_errordisplayer" runat="server" Text="*" Font-Italic="True" ForeColor="#FF3300"></asp:Label>
                               
                                </td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
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
                                        <asp:CheckBox ID="Chk_select" runat="server" onclick="Check_Click(this)"/>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                       
                                    <asp:TemplateField HeaderText="SII_PK">
                                        <ItemTemplate>
                                            <asp:Label ID="lblInventoryItem_PK" runat="server" Text='<%# Bind("SInventoryItem_PK") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                             <asp:TemplateField HeaderText="SII_PK">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_dodetpk" runat="server" Text='<%# Bind("SDODet_PK") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                           
                            <asp:BoundField DataField="Description" HeaderText="Description" ReadOnly="True" SortExpression="Description" />
                            <asp:BoundField DataField="TemplateSize" HeaderText="TemplateSize" SortExpression="TemplateSize" />
                            <asp:BoundField DataField="TemplateWidth" HeaderText="TemplateWidth" SortExpression="TemplateWidth" />
                            <asp:BoundField DataField="TemplateWeight" HeaderText="TemplateWeight" SortExpression="TemplateWeight" />
                            <asp:BoundField DataField="ReceivedQty" HeaderText="ReceivedQty" SortExpression="ReceivedQty" />
                           
                            <asp:BoundField DataField="DeliveredQty" HeaderText="DeliveredQty" ReadOnly="True" SortExpression="DeliveredQty" />
                              <asp:TemplateField HeaderText="BalanceQty">
                                
                                <ItemTemplate>
                                    <asp:Label ID="lbl_balanceqty" runat="server" Text='<%# Bind("OnHandQty") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Receipt">                               
                                <ItemTemplate>
                                    <asp:TextBox ID="txt_receiptQty" onkeypress="return isNumberKey(event,this)"  runat="server" Text='<%# Bind("OnHandQty") %>'></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:BoundField DataField="UomCode" HeaderText="UomCode" SortExpression="UomCode" />
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
            <asp:SqlDataSource ID="DoData" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT DISTINCT  SDO_PK, SDONum
FROM            DeliveryOrderStockMaster
WHERE        (ToLocation_PK = @ToLocation_PK)">
                <SelectParameters>
                    <asp:SessionParameter DefaultValue="0" Name="ToLocation_PK" SessionField="UserLoc_pk" Type="Decimal" />
                </SelectParameters>
            </asp:SqlDataSource>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT StockInventoryMaster.SInventoryItem_PK ,  DeliveryOrderStockDetails.SDODet_PK,  isnull(  Template_Master.Description ,' ')+' '+ isnull(   StockInventoryMaster.Composition,' ')+' '+isnull(  StockInventoryMaster.Construct ,' ')+' '+isnull(   StockInventoryMaster.TemplateColor,'') as Description, StockInventoryMaster.TemplateSize, 
                         StockInventoryMaster.TemplateWidth, StockInventoryMaster.TemplateWeight, StockInventoryMaster.ReceivedQty, StockInventoryMaster.OnHandQty,isnull( StockInventoryMaster.DeliveredQty,0) as DeliveredQty, 
                         StockInventoryMaster.Uom_PK, UOMMaster.UomCode
FROM            DeliveryOrderStockMaster INNER JOIN
                         DeliveryOrderStockDetails ON DeliveryOrderStockMaster.SDO_PK = DeliveryOrderStockDetails.SDO_PK INNER JOIN
                         StockInventoryMaster ON DeliveryOrderStockDetails.SInventoryItem_PK = StockInventoryMaster.SInventoryItem_PK INNER JOIN
                         Template_Master ON StockInventoryMaster.Template_PK = Template_Master.Template_PK INNER JOIN
                         UOMMaster ON StockInventoryMaster.Uom_PK = UOMMaster.Uom_PK"></asp:SqlDataSource>
        </td>
    </tr>
</table>
</asp:Content>

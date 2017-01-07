<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="BankAdvice.aspx.cs" Inherits="ArtWebApp.Shipping.LC.BankAdvice" %>
<%@ Register assembly="Infragistics35.Web.v12.1, Version=12.1.20121.2236, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.ListControls" tagprefix="ig" %>
<%@ Register assembly="Infragistics35.Web.v12.1, Version=12.1.20121.2236, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.EditorControls" tagprefix="ig" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"><link href="../../css/style.css" rel="stylesheet" />
    <style type="text/css">
     
    </style>
    </asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="DataEntryTable">
        <tr>
            <td>
                <table class="tittlebar">
                    <tr>
                        <td class="RedHeadding" colspan="5">Bank Advice</td>
                       
                        <td class="RedHeadding">&nbsp;</td>
                       
                    </tr>
                    
                    <tr>
                        <td  class="NormalTD">Supplier : </td>
                        <td  >
                            <ig:WebDropDown ID="drp_supplier" runat="server" DataSourceID="supplierdata" Height="23px" style="height: 23px" TextField="SupplierName" ValueField="Supplier_PK" Width="200px">
                                         <DropDownItemBinding TextField="SupplierName" ValueField="Supplier_PK" />
                                     </ig:WebDropDown></td>
                        <td>
                            <asp:Button ID="btn_showlc" runat="server" Text="S" OnClick="btn_showlc_Click" />
                        </td>
                        <td  class="NormalTD">LC num # : </td>
                        <td>
                             <ig:WebDropDown ID="drp_lc" runat="server" Width="200px" TextField="Name" ValueField="pk" >
                     <DropDownItemBinding TextField="Name" ValueField="pk" />  </ig:WebDropDown>
                        </td>
                        
                        <td>
                            <asp:Button ID="button_showLCData" runat="server" Text="S" OnClick="button_showLCData_Click" /></td>
                        
                    </tr>
                    </table>
            </td>
        </tr>
        <tr>
            <td>  <div id="Messaediv" runat="server">
                 


                           <asp:Label ID="lbl_msg" runat="server" Text="*"></asp:Label>


                     
               </div></td>
        </tr>
        <tr class="gridtable">
            <td>       <asp:UpdatePanel ID="UpdatePanel2" runat="server">
         <ContentTemplate>

        <asp:GridView ID="tbl_podetails" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4" style="font-size: x-small; font-family: Calibri" Width="100%" Font-Size="Large" DataKeyNames="LCDet_PK">
                            <Columns>

                                <asp:TemplateField>                                   
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chk_select" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="LCDet_PK" InsertVisible="False" SortExpression="LCDet_PK">
                                    
                                    <ItemTemplate>
                                         <asp:Label ID="lbl_lcdetpk" runat="server" Text='<%# Eval("LCDet_PK") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>


                                   
                                <asp:BoundField DataField="LCNum" HeaderText="LCNum" SortExpression="LCNum" />
                                <asp:BoundField DataField="POType" HeaderText="POType" SortExpression="POType" />
                                <asp:BoundField DataField="ATCnum" HeaderText="ATCnum" SortExpression="ATCnum" />
                                <asp:BoundField DataField="PONUM" HeaderText="PONUM" SortExpression="PONUM" />
                                 <asp:BoundField DataField="LCValue" HeaderText="LCValue" SortExpression="LCValue" />
                                <asp:BoundField DataField="InvoiceNUM" HeaderText="InvoiceNUM" SortExpression="InvoiceNUM" />
                                <asp:BoundField DataField="SupplierName" HeaderText="SupplierName" SortExpression="SupplierName" />
                                <asp:BoundField DataField="BankName" HeaderText="BankName" SortExpression="BankName" />
                                <asp:TemplateField HeaderText="AdviceValue" SortExpression="AdviceValue">
                                  
                                    <ItemTemplate>
                                      <asp:TextBox ID="txt_lcvalue" runat="server" ></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="AdviceInvoiceNUM" SortExpression="InvoiceNUM">
                                    
                                    <ItemTemplate>
                                        <asp:TextBox ID="txt_invoicenum" runat="server" ></asp:TextBox>
                                    </ItemTemplate>
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
</td></tr><tr><td>

              <asp:Button ID="btn_submitbankadvice" runat="server" Text="Update LC" OnClick="btn_submitbankadvice_Click" />

              </td></tr>
        </table>
     <asp:SqlDataSource ID="supplierdata" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT [SupplierName], [Supplier_PK] FROM [SupplierMaster] ORDER BY [SupplierName]"></asp:SqlDataSource>
    </asp:Content>
